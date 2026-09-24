<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddOpsContigencyType.aspx.cs" Inherits="FINOPS_AddOpsContigencyType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tour Information</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ValidateContigencyTypeName() {
            var contigencyTypeName = document.getElementById('<%=txtContigencyTypeName.ClientID %>').value;
            if (contigencyTypeName == '') {
                document.getElementById('<%=txtContigencyTypeName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContigencyTypeName.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;

            if (ValidateContigencyTypeName()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to add contigency type?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 100px;">
                <fieldset style="width: 50%;">
                    <legend style="text-align: center;">
                        Add Ops Contigency Type</legend>
                    <table width="60%">
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                   Contigency Type:
                                </p>
                                <p>
                                    <asp:TextBox ID="txtContigencyTypeName" runat="server" Width="100%" onblur="return ValidateContigencyTypeName();" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClientClick="return ValidateAll();"
                                                OnClick="btnSubmit_Click" Width="100%" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnContigencyTypeList" CssClass="button" runat="server" Text="Contigency Type List" OnClick="btnContigencyTypeList_Click"
                                                Width="100%" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center"></td>
                        </tr>
                        <tr>
                            <td align="center"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
