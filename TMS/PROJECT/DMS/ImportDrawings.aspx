<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportDrawings.aspx.cs"
    Inherits="PROJECT_DMS_ImportDrawings" Title="Import Design List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
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

    <style type="text/css">
        .textboxcenter {
            width: 100%;
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
            width: 100%;
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textboxleft {
            width: 100%;
            padding: 5px 5px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
            /*background-color: palegreen;*/
        }
    </style>

    <script type="text/javascript" language="javascript">

        function ValidatefileUploadDrawingFile() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadDrawingFile = document.getElementById('<%=fileUploadDrawingFile.ClientID %>').value;


            fileUploadDrawingFile = fileUploadDrawingFile.split(" ").join("")
            fileUploadDrawingFile = fileUploadDrawingFile.split("(").join("")
            fileUploadDrawingFile = fileUploadDrawingFile.split(")").join("")

            var divfileUploadDrawingFile = document.getElementById("divfileUploadDrawingFile");
            var lblfileUploadDrawingFile = document.getElementById('<%=lblfileUploadDrawingFile.ClientID %>');

            if (fileUploadDrawingFile == '') {
                document.getElementById('<%=fileUploadDrawingFile.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadDrawingFile.style.display = "block";
                lblfileUploadDrawingFile.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadDrawingFile.toLowerCase())) {
                    document.getElementById('<%=fileUploadDrawingFile.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadDrawingFile.style.display = "block";
                    lblfileUploadDrawingFile.innerHTML = "Please choose only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadDrawingFile.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadDrawingFile.style.display = "none";
                    lblfileUploadDrawingFile.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidateAllDrawingFile() {
            var check = true;

            if (ValidatefileUploadDrawingFile()) { return false; }

            return check;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 60%">
            <legend style="text-align: center;">Import Design List</legend>

            <table width="100%">
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnGetFormat" CssClass="button" Width="100%" runat="server"
                            Text="Download Format" OnClick="btnGetFormat_Click" />
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">Browse:</td>
                    <td colspan="4">
                        <asp:FileUpload ID="fileUploadDrawingFile" runat="server" Width="100%" Height="29px"
                            BorderStyle="Groove" onblur="return ValidatefileUploadDrawingFile();" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnGetDrawingFile" CssClass="button" Width="100%" runat="server"
                            Text="Get Detail" OnClientClick="return ValidateAllDrawingFile();" OnClick="btnGetDrawingFile_Click" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnImport" CssClass="button" Width="100%" runat="server"
                            Text="Import Design List" OnClick="btnImport_Click" OnClientClick="Confirm();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnDrawingList" CssClass="button" Width="100%" runat="server"
                            Text="View Design List" OnClick="btnDrawingList_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                    <td>
                        <div id="divfileUploadDrawingFile" style="display: none;">
                            <asp:Label ID="lblfileUploadDrawingFile" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                    <td>&nbsp;
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
        <fieldset style="width: 60%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>
            <div id="gridContainer" style='overflow-y: scroll; overflow-x: scroll; width: 100%; height: 430px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvDesignDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvDesignDetails_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>

                        <asp:TemplateField HeaderText="JOB_No.">
                            <ItemTemplate>
                                <asp:Label ID="lblDuplicateFlag" runat="server" Text='<%# Eval("DUPLICATE_FLAG") %>' Visible="false" />
                                <asp:Label ID="lblDrawingID" runat="server" Text='<%# Eval("DRAWING_ID") %>' Visible="false" />                                
                                <asp:Label ID="lblJOBNo" runat="server" Text='<%# Eval("JOB_NO") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Drawing_No.">
                            <ItemTemplate>
                                <asp:Label ID="lblDrawingNo" runat="server" Text='<%# Eval("DRAWING_NO") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Client_Drawing_No.">
                            <ItemTemplate>
                                <asp:Label ID="lblClientDrawingNo" runat="server" Text='<%# Eval("CLIENT_DRAWING_NO") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Contractor_Drawing_No.">
                            <ItemTemplate>
                                <asp:Label ID="lblContractorDrawingNo" runat="server" Text='<%# Eval("CONTRACTOR_DRAWING_NO") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("DESCRIPTION") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>'
                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rqd_Date_By_Project_Team">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRqdDateByProjectTeam" runat="server" Text='<%# Eval("REQD_DATE_BY_PROJECT_TEAM") %>'
                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
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

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
