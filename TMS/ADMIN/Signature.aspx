<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="Signature.aspx.cs" Inherits="ADMIN_Signature" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Employee Profile</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
            <div align="center" style="margin-top: 40px;">
                <fieldset style="width: 60%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblLegend" runat="server" Text="Add Signature File"></asp:Label></legend>
                    <table width="70%">
                         <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
               <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Employee Name :</td>
                            <td>
                                <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" Height="25px">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Upload Signature File:</td>
                            <td>
                                <asp:FileUpload ID="uploadSignFile" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                
                            <asp:Button ID="btnSave" runat="server" Width="40%" Text="Save" CssClass="button" style="margin-left: 80px;" OnClick ="BtnSave_Click" />

                                    
                            </td>
                            <td>&nbsp;</td>
                        </tr>

                    </table>


                </fieldset>
            </div>
 <%--       </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
