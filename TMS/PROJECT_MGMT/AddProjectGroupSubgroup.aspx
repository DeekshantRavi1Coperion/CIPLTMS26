<%@ Page Title="Add New MR" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddProjectGroupSubgroup.aspx.cs" Inherits="PROJECT_MGMT_AddProjectGroupSubgroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
<link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
       window.onload=function(){
        var div=document.getElementById("dvScroll");
        var div_position=document.getElementById("div_position");
        var position=parseInt('<%=Request.Form["div_position"] %>');
            if(isNaN(position))
            {
                position=0;
            }
            div.scrollTop=position;
            div.onscroll=function(){
                div_position.value=div.scrollTop;
            };
       };
    </script>

    <script type="text/javascript">

        function ValidateProjectNo() {
            var ProjectNo = document.getElementById('<%=ddlProjectNo.ClientID %>').selectedIndex;
            if (ProjectNo== '' || ProjectNo== '0') {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                        
        function ValidateGroupName() {
            var GroupName= document.getElementById('<%=txtGroupName.ClientID %>').value;
            if (GroupName== '') {
                document.getElementById('<%=txtGroupName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtGroupName.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                
        function ValidateAll() {
            var check = true;
            if (ValidateProjectNo()) {return false;}
            if (ValidateGroupName()) {return false;}                      
            return check;
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
            <legend style="text-align: center;">Add New MR</legend>
            <table width="100%">
                <tr>
                    <td align="right">
                        Project No:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProjectNo" runat="server" Width="100%" Height="25px" onblur="return ValidateProjectNo();"
                            OnSelectedIndexChanged="ddlProjectNo_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Group Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtGroupName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnAddNewMR" CssClass="button" Width="100%" runat="server" Text="Add New MR"
                            OnClick="btnAddNewMR_Click" OnClientClick="return ValidateAll();" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server" Text="Save MR"
                            OnClick="btnSave_Click" />
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
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div id="dvScroll" style='overflow: scroll; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>
                        <asp:GridView ID="gvSubgroupList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvSubgroupList_RowDataBound"
                            OnRowCommand="gvSubgroupList_RowCommand">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="REMOVE" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SERIAL_NO") %>' Visible="false" />
                                        <asp:Label ID="lblGroupID" runat="server" Text='<%# Eval("GROUP_ID") %>' Visible="false" />
                                        <asp:ImageButton ID="imgBtnDelete" CommandArgument="REMOVE" ToolTip="Remove" runat="server"
                                            ImageUrl="~/Images/Cancelled01.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PROJECT_NO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectNo" runat="server" Text='<%# Eval("PROJECT_NO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GROUP_NAME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GROUP_NAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SUBGROUP_NAME">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSubgroupName" runat="server" Width="100%" Text='<%# Eval("SUBGROUP_NAME") %>' />
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
            <input type="hidden" id="div_position" name="div_position" />
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
