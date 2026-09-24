<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewMRNInPDF.aspx.cs"
    Inherits="REPORTS_MRN_ViewMRNInPDF" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/html2canvas.min.js" type="text/javascript"></script>
    <script src="../../Scripts/pdfmake.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Export() {
            html2canvas(document.getElementById('tblPDF'), {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download("MRN.pdf");
                    alert("Admit Card Downloading Started");
                }
            });
        }
    </script>
    <style type="text/css">
        .auto-style2 {
            width: 147px;
        }

        .auto-style3 {
            width: 145px;
        }

        .auto-style5 {
            width: 226px;
        }

        .auto-style7 {
            width: 188px;
        }

        .auto-style8 {
            width: 415px;
        }

        .auto-style9 {
            width: 141px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">

        <table style="font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;">
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">
                    <asp:ImageButton ID="imgBtnExport" runat="server" ImageUrl="~/Images/pdficon3.png"
                        OnClick="imgBtnExport_Click" Width="30px" Height="30px" ToolTip="Save as PDF" />
                </td>
            </tr>
        </table>



        <table style="font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;">
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;">
                    <table style="font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;">
                        <tr>
                            <td style="width:188px;"  rowspan="2">coperion</td>
                            <td style="width:415px;"  rowspan="2">
                                <h3>INCOMING INSPECTION REPORT</h3>
                            </td>
                            <td class="auto-style9">FMT NO.:</td>
                            <td style="width:226px;">FT/QA/02/00</td>
                        </tr>
                        <tr>
                            <td class="auto-style9">Rev.:00</td>
                            <td style="width:226px;">Date: 17.05.2018</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;" colspan="5">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;">
                    <table style="font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;">

                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width:147px;">PO NO./DATE:</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;width:145px;">DATE</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 187px;">VENDOR NAME:</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 312px;" colspan="3">VENDOR NAME:</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width:147px;">QTY.:</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width:147px;">QTY</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 187px;">PART NAME:</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;" colspan="3">PART NAME:</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width:147px;">SAMPLE SIZE:DIMENSIONAL:</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width:147px;">
                                <input type="text" style="width: 99%;" /></td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 187px;">INSPECTION DATE:</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;" colspan="3">INSPECTION DATE:</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width:147px;" >VISUAL:</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width:147px;" >
                                <input type="text" style="width: 99%;" /></td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 187px;">MRN NO.:</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;" colspan="3">MRN NO.:</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;" colspan="5">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;">
                    <table style="font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;">
                        <tr>
                            <th style="padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 60px;">Sr. No</th>
                            <th style="padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 546px;">Characterstic</th>
                            <th style="padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 324px;">Ovservation</th>
                            <th style="padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 108px;">Status</th>
                            <th style="padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white;">Remarks</th>
                        </tr>







                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">1</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">prod desc</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px;">Rating</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px;">Visual Check</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px;">Total Quantity Checked</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;" colspan="5">
                                <hr />
                            </td>
                        </tr>








                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;" colspan="5">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;">
                    <table style="font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;">
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px; width: 89px;">Disposition</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 14px; height: 35px;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;">Accepted</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;">Accepted U/D</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px; text-align: center;" colspan="3">Rejected</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 89px;">Quantity</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 14px;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center;">Segrigration</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center;">Rework</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center;">Return to Source</td>

                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; background-color: #4CAF50; color: white;" colspan="8">Note: Forward the inspection report to H.O.D.(Q.A.) in case of non-conformance with inspection standard and copy to purchase for supplier evealuation.</td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 103px;" colspan="2">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">&nbsp;</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;" colspan="3" rowspan="2">Comments:-</td>

                        </tr>
                        <tr>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 103px; text-align: center;" colspan="2">Inspected By</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center;">Date</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center;">H.O.D.(Q.A.)</td>
                            <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center;">Date</td>

                        </tr>
                    </table>
                </td>
            </tr>
        </table>




    </form>
</body>
</html>

