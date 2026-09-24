<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="VouchersDirecoryPathList.aspx.cs" Inherits="VOUCHER_AUTH_VouchersDirecoryPathList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script src="../../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" />
    <link href="../../../Styles/Site.css" rel="stylesheet" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>


    <script type="text/javascript">
        function ClearAllFilters() {

            document.getElementById('<%=ddlVucherTypeToS.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlEmployeeToS.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtDirectoryPathAS.ClientID %>').value = "";

            return false;
        }

    </script>



    <script type="text/javascript">

        function ValidatePath() {
            var Value = document.getElementById('<%=txtDirectoryPathAS.ClientID %>').value;
            if (Value == '') {
                document.getElementById('<%=txtDirectoryPathAS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDirectoryPathAS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {
            var check = true;

            if (ValidatePath()) {
                check = false;
            }


            if (check) {
                if (confirm("Would you save path?")) {
                    document.getElementById('<%=hdAttachment1ConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdAttachment1ConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }


    </script>




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

    <script type="text/javascript">

        window.onload = function () {

            var currentPosX = document.getElementById("<%=hdScrollPositionX.ClientID%>").value;
            var currentPosY = document.getElementById("<%=hdScrollPositionY.ClientID%>").value;

            //var strCook = document.cookie;
            var strCookX = currentPosX;
            var strCookY = currentPosY;


            document.getElementById("<%=gridContainer.ClientID%>").scrollLeft = strCookX;
            document.getElementById("<%=gridContainer.ClientID%>").scrollTop = strCookY;

        }

        function SetDivPosition() {
            var intX = document.getElementById("<%=gridContainer.ClientID%>").scrollLeft;
            var intY = document.getElementById("<%=gridContainer.ClientID%>").scrollTop;

            document.getElementById("<%=hdScrollPositionX.ClientID%>").value = intX
            document.getElementById("<%=hdScrollPositionY.ClientID%>").value = intY
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdAttachment1ConfirmValue" runat="server" Value="0" />
    <%-- <asp:UpdatePanel ID="uppanel" runat="server">
        <contenttemplate>--%>
    <fieldset style="width: 100%; margin-top:20px;">
        <legend style="text-align: center;">Vouchers Directory Path List

        </legend>
        <div style="margin-top: 5px; width: 100%;" align="center">
            <div style="margin-top: 5px; width: 70%;">

                <table width="90%" align="center">

                    <tr>
                        
                        <td>Employee:</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlEmployeeToS" runat="server" Width="100%" Height="23px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        

                        <td>Voucher Type:</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlVucherTypeToS" runat="server" Width="100%" Height="23px">
                            </asp:DropDownList>
                        </td>

                        <td>&nbsp;</td>
                        <td>Path:</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtDirectoryPathToS" runat="server" Width="100%">
                            </asp:TextBox>
                        </td>


                        <td>&nbsp;</td>
                        
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 45%;" align="right">
                                        <asp:ImageButton ID="imgBtnClearAllFilters" runat="server"
                                            ImageUrl="~/Images/NEWICONS/clear1.png"
                                            Width="40px" Height="40px"
                                            ToolTip="Clear Filters"
                                            OnClientClick="return ClearAllFilters();" />
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td>&nbsp;</td>

                        <td>
                            <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                                Text="Search"
                                OnClick="btnSearch_Click" />
                        </td>
                        <td>&nbsp;</td>

                        <td>
                            <asp:Button ID="btnAddPath" CssClass="button" Width="100%" runat="server"
                                Text="Add Path"
                                OnClick="btnAddPath_Click" />
                        </td>

                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="3">
                            <div align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>

                </table>

            </div>
            <div style="margin-right: 30px; width: 65%;">
                <div align="center">
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">
                            <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                        </legend>


                        <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
                        <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

                        <%-- <div style='overflow-y: auto; overflow-y: auto; width: 100%; height: 635px; border: 1px solid lightgray;'>--%>
                        <div id="gridContainer" runat="server" style='overflow-y: auto; overflow-x: auto; width: 100%; height: 500px; border: 1px solid lightgray;'
                            onscroll="SetDivPosition()">
                            <asp:GridView ID="gvDirectoryList"
                                runat="server"
                                AutoGenerateColumns="false"
                                CellPadding="4"
                                CssClass="myGrid"
                                ForeColor="#333333" GridLines="Both" Width="100%"
                                HorizontalAlign="Center"
                                OnRowCommand="gvDirectoryList_RowCommand"
                                OnRowDataBound="gvDirectoryList_RowDataBound">

                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl No."
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">

                                        <ItemTemplate>
                                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("SR_NO") %>' />
                                            <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                                            <asp:Label ID="lblVoucherTypeID" runat="server" Text='<%# Eval("TYPE_FID") %>' Visible="false" />
                                            <asp:Label ID="lblEmpRecordID" runat="server" Text='<%# Eval("EMP_RECORD_FID") %>' Visible="false" />
                                            <asp:Label ID="lblFuser" runat="server" Text='<%# Eval("FUSER") %>' Visible="false" />
                                            <asp:Label ID="lblDirectoryPath" runat="server" Text='<%# Eval("DIRECTORY_PATH") %>' Visible="false" />
                                            <asp:Label ID="lblVoucherType" runat="server" Text='<%# Eval("VOUCHER_TYPE") %>' Visible="false" />
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("NAME") %>' Visible="false" />

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField
                                        HeaderText="Edit"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnEdit"
                                                CommandArgument="UPDATE_PATH"
                                                runat="server"
                                                ImageUrl="~/Images/Icons/yes3.png"
                                                Height="35px"
                                                Width="35px"
                                                ToolTip="Edit Path" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="NAME" HeaderText="User Name" />
                                    <asp:BoundField DataField="FUSER" HeaderText="FACT User Name" />
                                    <asp:BoundField DataField="VOUCHER_TYPE" HeaderText="Voucher Type" />
                                    <asp:BoundField DataField="DIRECTORY_PATH" HeaderText="Directory Path" />

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
        </div>
    </fieldset>



    <%-- SHOW & AUTHORIZE VOUCHER START--%>
    <asp:Button ID="btnShowAuthorizeVoucher" runat="server" Style="display: none" />
    <asp:ModalPopupExtender
        ID="mpeAuthorizeVoucher"
        runat="server"
        TargetControlID="btnShowAuthorizeVoucher"
        BehaviorID="mpeAuthorizeVoucherBID"
        PopupControlID="pnlViewAuthorizeVoucherPopup"
        CancelControlID="imgBtnCancelAuthorizeVoucher"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewAuthorizeVoucherPopup" runat="server" CssClass="popup-edit">

        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAuthorizeVoucher" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <fieldset style="width: 97%; margin-left: 1%; margin-top: 10px;">
            <legend style="text-align: center;">Update Directory Path</legend>

            <table width="90%" align="center">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>FUSER:</td>
                    <td>
                        <asp:TextBox ID="txtFuserAS" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Name:</td>
                    <td>
                        <asp:TextBox ID="txtNameAS" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Voucher Type:</td>
                    <td>
                        <asp:TextBox ID="txtVoucherTypeAS" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Path:</td>
                    <td>
                        <asp:TextBox ID="txtDirectoryPathAS" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnUpdateDirectoryPath" runat="server" Width="100%"
                            Text="Update Voucher Path"
                            CssClass="button"
                            OnClick="btnUpdateDirectoryPath_Click"
                            OnClientClick="return ValidateAll();" />
                    </td>
                </tr>

            </table>
        </fieldset>
    </asp:Panel>
    <%-- SHOW & AUTHORIZE VOUCHER END--%>

    <%-- </contenttemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
