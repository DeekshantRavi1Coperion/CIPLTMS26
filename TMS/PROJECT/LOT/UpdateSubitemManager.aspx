<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateSubitemManager.aspx.cs" Inherits="PROJECT_LOT_UpdateSubitemManager" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="icon" href="../../Images/Icon04.png" />

    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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
</head>
<body>
    <form id="form1" runat="server">
        <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </ajax:ToolkitScriptManager>
        <asp:HiddenField ID="hdConfirmValue" runat="server" />
        <div align="center" style="margin-top: 20px;">
            <fieldset style="width: 60%;">
                <legend style="text-align: center;">Update LOT Project Approvers
                </legend>
                <table width="96%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="100%">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                            </asp:Panel>
                        </td>
                    </tr>                    
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Company:</td>
                        <td>
                            <asp:HiddenField ID="hdCompanyID" runat="server" />
                            <asp:TextBox ID="txtCompany" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Department:</td>
                        <td>
                            <asp:HiddenField ID="hdDepartmentID" runat="server" />
                            <asp:TextBox ID="txtDepartment" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <div align="center">
                                <fieldset style="width: 100%;">
                                    <legend style="text-align: center;">
                                        <asp:Label ID="lblSubitemsRecords" runat="server" Text="Subitems Records[0]" /></legend>
                                    <div style='overflow-y: scroll; overflow-x: scroll; width: 950px; height: 100px; border: 1px solid lightgray;'>
                                        <asp:GridView ID="gvSubItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center">
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLOTMainItemID" runat="server" Text='<%# Eval("LOT_MAIN_ITEM_ID") %>' Visible="false" />
                                                        <asp:Label ID="lblLOTMainItem" runat="server" Text='<%# Eval("LOT_MAIN_ITEM") %>' Visible="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLOTMainSubitemID" runat="server" Text='<%# Eval("LOT_MAIN_SUBITEM_ID") %>' Visible="false" />
                                                        <asp:Label ID="lblLOTMainSubitem" runat="server" Text='<%# Eval("LOT_MAIN_SUBITEM") %>' Visible="true" />
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
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Manager:</td>
                        <td>
                            <asp:DropDownList ID="ddlManager" runat="server" Width="100%" Height="26px" onblur="return ValidateManager();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>



        <script type="text/javascript">

            function ValidateCompany() {
                var Company = document.getElementById('<%=txtCompany.ClientID %>').value;
                if (Company== '') {
                    document.getElementById('<%=txtCompany.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtCompany.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateDepartment() {
                var Department = document.getElementById('<%=txtDepartment.ClientID %>').value;
                if (Department == '') {
                    document.getElementById('<%=txtDepartment.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtDepartment.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
           
            function ValidateManager() {
                var Manager = document.getElementById('<%=ddlManager.ClientID %>').selectedIndex;
                if (Manager == '' || Manager == 0) {
                    document.getElementById('<%=ddlManager.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlManager.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

        </script>


        <script type="text/javascript">

            function ValidateAll() {
                var check = true;

                if (ValidateCompany()) {
                    check = false;
                }

                if (ValidateDepartment()) {
                    check = false;
                }
                
                if (ValidateManager()) {
                    check = false;
                }

                if (check) {
                    if (confirm("Would you like to add manager(s)?")) {
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

    </form>
</body>
</html>
