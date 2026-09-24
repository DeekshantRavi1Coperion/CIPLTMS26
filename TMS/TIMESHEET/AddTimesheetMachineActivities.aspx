<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddTimesheetMachineActivities.aspx.cs" Inherits="TIMESHEET_AddTimesheetMachineActivities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tour Information</title>

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ValidateActivity() {
            var Activity = document.getElementById('<%=txtActivity.ClientID %>').value;
            if (Activity == '') {
                document.getElementById('<%=txtActivity.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtActivity.ClientID %>').style.borderColor = "";
                return false;
            }
        }


    </script>

    <script type="text/javascript">
        function ValidateAll() {
            var check = true;

            if (ValidateActivity()) {
                check = false;
            }


            return check;
        }

        function ConfirmSaving() {

            if (confirm("Would you like to save activity?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>

            <div class="form-entry-container">
                <fieldset class="form-card">
                    <legend>
                        <asp:Label ID="lblLegend" runat="server" Text="Add Activity"></asp:Label>
                    </legend>

                    <div class="form-grid form-grid-2">

                        <label>Activity</label>
                        <asp:TextBox ID="txtActivity" runat="server"
                            CssClass="form-control" />

                    </div>
                </fieldset>

                <div class="full-width button-group">
                    <asp:Button ID="btnSubmit"
                        runat="server"
                        Text="Save"
                        CssClass="button"
                        OnClientClick="return ValidateAll();"
                        OnClick="btnSubmit_Click" Width="50%" />

                    <asp:Button
                        ID="btnActivityList"
                        CssClass="button"
                        runat="server"
                        Text="Activities List"
                        PostBackUrl="~/TIMESHEET/TimesheetMachineActivitiesList.aspx" Width="50%" />
                </div>

                <div class="full-width">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="30px">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                    </asp:Panel>
                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
