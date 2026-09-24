<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateStatusFCVendorPayment.aspx.cs"
    Inherits="FINANCE_FC_PAYMENT_UpdateStatusFCVendorPayment" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CIPLTMS- Approve/Send to Amendment Paymentt Request</title>
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

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <style type="text/css">
        .textboxtagno {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxleft {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxcenter {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxright {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </ajax:ToolkitScriptManager>
        <asp:HiddenField ID="hdConfirmValue" runat="server" />
        <asp:HiddenField ID="hdUpdationFlag" runat="server" />

        <%--<asp:UpdatePanel runat="server" ID="uppanel">
            <ContentTemplate>--%>
        <%--<div align="center" style="margin-top: 30px;">--%>


        <div style='margin-top: 50px;' align="center">
            <fieldset style="width: 1000px;">
                <legend style="text-align: center;">
                    <asp:Label ID="lblLOTDetailsLegendTxt" runat="server"></asp:Label>Update Status Of Payment Request
                </legend>

                <div style='overflow-y: scroll; overflow-x: hidden; width: 100%; height: 770px; border: 1px solid lightgray;'>

                    <table width="950px" style="margin: 20px;">
                        <tr>
                            <td>&nbsp;</td>
                            <td colspan="4" align="center">
                                <asp:Panel ID="pnlUpdateStatus" Visible="false" runat="server" Height="50px">
                                    <asp:Label ID="lblUpdateStatusMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                                </asp:Panel>
                            </td>
                        </tr>




                        <tr>
                            <td style="width: 15%;">Payment Request No.:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtPaymentRequestNoUS" runat="server" Width="100%" Enabled="false" />
                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 15%;">Job No. / Project No.:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtJOBNoUS" runat="server" Width="100%" Enabled="false" />
                            </td>

                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            
                            <td style="width: 15%;">Vendor Name:</td>
                            <td colspan="5">
                                <table>
                                    <tr>
                                        <td style="width:80%;">
                                            <asp:TextBox ID="txtVendorNameUS" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width:20%;">
                                              <asp:TextBox ID="txtVendorCodeUS" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                    </tr>

                                </table>
                              
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td>Vendor Address:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtVendorAddressUS" runat="server" Width="100%" Rows="3" Enabled="false" />
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="width: 15%;">CID PO No. to Vendor:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtPONoUS" runat="server" Width="100%" Enabled="false" />

                            </td>

                            <td>&nbsp;</td>

                            <td style="width: 15%;">Total PO Amount:</td>
                            <td style="width: 30%;">

                                <table width="100%">
                                    <tr>
                                        <td style="width: 70%">
                                            <asp:TextBox ID="txtTotalPOAmountUS" runat="server" Width="100%" Enabled="false" Text="0.000" />
                                        </td>

                                        <td style="width: 30%">
                                            <asp:TextBox ID="txtPOAmountCurrencyUS" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="width: 15%;">Requested Amount:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtReleasedAmountUS" runat="server" Width="100%" Enabled="false" Text="0.000" />
                            </td>

                            <td>&nbsp;</td>

                            <td style="width: 15%;">Payment Release Cut-Off Date:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtCutOffDateUS" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="width: 15%;">Type of Payment:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtPaymentTypeUS" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>

                            <td style="width: 15%;">Vendor Invoice No.:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtVendorInvoiceNoUS" runat="server" Width="100%" Enabled="false" />

                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 15%;">Vendor Invoice Date:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtVendorInvoiceDateUS" runat="server" Width="100%" Enabled="false" />
                            </td>

                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <asp:Panel ID="pnlViewInvoiceFile" runat="server" Visible="false">
                            <tr>
                                <td style="width: 15%;">View Invoice Fiile:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblInvoiceFileUS" runat="server" Font-Bold="True" Font-Size="Large" />
                                    <asp:ImageButton ID="imgBtnViewInvoiceFile" Height="30px" Width="30px" runat="server"
                                        ImageUrl="~/Images/pdficon4.png" OnClick="imgBtnViewInvoiceFile_Click" />

                                </td>
                            </tr>
                        </asp:Panel>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>

                            <td style="width: 15%;">Bank Name:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtVendorBankNameUS" runat="server" Width="100%" Enabled="false" />

                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 15%;">Bank Branch:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtBankVendorBranchUS" runat="server" Width="100%" Enabled="false" />
                            </td>

                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>

                            <td style="width: 15%;">Swift Code:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtVendorSwiftCodeUS" runat="server" Width="100%" Enabled="false" />
                            </td>

                            <td>&nbsp;</td>

                            <td style="width: 15%;">Account Number:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtBankAccountNumberUS" runat="server" Width="100%" Enabled="false" />
                            </td>

                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>

                            <td style="width: 15%;">Created / Amended Remarks:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtCreatedOrAmendedRemarksUS" runat="server" Width="100%" TextMode="MultiLine"
                                    Rows="2" Enabled="false" />
                            </td>

                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">Remarks
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtApprovedOrAmendedApprovedRemarksUS" runat="server" Width="100%" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>


                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnUpdateSatus" runat="server" Width="100%" Text="Approve Request" CssClass="button"
                                    OnClick="btnUpdateSatus_Click" />
                            </td>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <asp:Button ID="btnAmendment" runat="server" Width="100%" Text="Send To Amendmentt" CssClass="button"
                                    OnClick="btnAmendment_Click" />
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td colspan="5">
                                <asp:Button ID="btnViewList" runat="server" Width="100%" Text="View Payments List" CssClass="button"
                                    OnClick="btnViewList_Click" />
                            </td>

                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                </div>
            </fieldset>
        </div>

        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>


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

        <script type="text/Javascript">
            function preventInput(event) {
                if (event.which != 9) {
                    event.preventDefault();
                }
            }
        </script>

    </form>
</body>
</html>
