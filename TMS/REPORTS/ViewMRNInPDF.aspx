<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewMRNInPDF.aspx.cs"
    EnableEventValidation="false" Inherits="REPORTS_ViewMRNInPDF" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="../../viewpdftablecss.css" rel="stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="imgBtnExport" runat="server" ImageUrl="~/Images/pdficon3.png"
                        OnClick="imgBtnExport_Click" Width="30px" Height="30px" ToolTip="Save as PDF" />
                </td>
            </tr>
        </table>
        <table style="width: 100%; border-collapse: collapse;" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td>
                    <img src="../Images/COPERION/coperion.jpg" width="300px" height="150px" /></td>
                <td>
                    <h2>INCOMING INSPECTION REPORT</h2>
                </td>
                <td>
                    <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
                        <tr>
                            <td>FMT NO.: </td>
                            <td>FT/QA/02/00</td>
                        </tr>
                        <tr>
                            <td>Rev.:00</td>
                            <td>Date: 17.05.2018</td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width: 100%; border-collapse: collapse;" border="1" cellpadding="2" cellspacing="0">
                        <tr>
                            <td>PO NO./DATE</td>
                            <td>SDFSDF</td>
                            <td>VENDOR NAME:</td>
                            <td>
                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>QTY.</td>
                            <td>10000</td>
                            <td>PART NAME:</td>
                            <td>SSDFSDFFDDF</td>

                        </tr>
                        <tr>
                            <td>SAMPLE SIZE:DIMENSIONAL</td>
                            <td>
                                <input type="text" /></td>
                            <td>INSPECTION DATE:</td>
                            <td>SDFD</td>
                        </tr>
                        <tr>
                            <td>VISUAL</td>
                            <td>
                                <input type="text" /></td>
                            <td>MRN NO.:</td>
                            <td>SDFD</td>
                        </tr>
                    </table>

                </td>

            </tr>
            <tr>
                <td colspan="3">
                    <table style="width: 100%; border-collapse: collapse;" border="1" cellpadding="2" cellspacing="0">
                        <tr>
                            <th>Sr. No.</th>
                            <th>Characterstic</th>
                            <th>Ovservation</th>
                            <th>Status</th>
                            <th>Remarks</th>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td>sdf</td>
                            <td>sdf</td>
                            <td>sdf</td>
                            <td>sdf</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width: 100%; border-collapse: collapse;" border="1" cellpadding="2" cellspacing="0">
                        <tr>
                            <td>Disposition</td>
                            <td></td>
                            <td>Accepted</td>
                            <td>Accepted U/D</td>
                            <td></td>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td>Quantity</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Segrigration</td>
                            <td>Rework</td>
                            <td>Return to Source</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">Note: Forward the inspection report to H.O.D.(Q.A.) in case of 
                    non-conformance with inspection standard and copy to purchase for supplier evealuation.
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width: 100%; border-collapse: collapse;" border="1" cellpadding="2" cellspacing="0">
                        <tr>
                            <td colspan="4">
                                <table style="width: 100%; border-collapse: collapse;" border="1" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Inspected By</td>
                                        <td>Date</td>
                                        <td>H.O.D.(Q.A.)</td>
                                        <td>Date</td>
                                    </tr>
                                </table>
                            </td>

                            <td>Comments:-
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
