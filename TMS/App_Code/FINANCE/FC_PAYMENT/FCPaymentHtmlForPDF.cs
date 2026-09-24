using System;
using System.Text;
using System.Data;


public class FCPaymentHtmlForPDF
{
    #region Variables
    int amendmentCount = 0;

    string PaymentRequestNo = string.Empty;
    string DateOfRequest = string.Empty;
    string StatusFid = string.Empty;
    string Status = string.Empty;
    string JobNo = string.Empty;
    string VendorCode = string.Empty;
    string VendorName = string.Empty;
    string VendorAddress = string.Empty;

    string PoNo = string.Empty;
    string PoAmount = string.Empty;
    string PoCurrency = string.Empty;

    string FinancialYear = string.Empty;
    string AmountToBeReleased = string.Empty;
    string PaymentType = string.Empty;
    string InvoiceNo = string.Empty;

    string InvoiceDate = string.Empty;
    string paymentReleaseCutOffDate = string.Empty;


    string BankName = string.Empty;
    string BankBranch = string.Empty;
    string SwiftCode = string.Empty;
    string BankAccountNo = string.Empty;
    string BankAdvice = string.Empty;
    string DateOfPaymentReleased = string.Empty;

    int CreatedById = 0;
    string CreatedBy = string.Empty;
    string CreatedOn = string.Empty;
    string CreatedRemarks = string.Empty;

    int CancelledById = 0;
    string CancelledBy = string.Empty;
    string CancelledOn = string.Empty;
    string CancelledRemarks = string.Empty;

    int ApprovedById = 0;
    string ApprovedBy = string.Empty;
    string ApprovedOn = string.Empty;
    string ApprovedRemarks = string.Empty;

    int AmendedById = 0;
    string AmendedBy = string.Empty;
    string AmendedOn = string.Empty;
    string AmendedRemarks = string.Empty;

    int ApprovedAmendedById = 0;
    string ApprovedAmendedBy = string.Empty;
    string ApprovedAmendedOn = string.Empty;
    string ApprovedAmendedRemarks = string.Empty;

    int SentToAmendmentById = 0;
    string SentToAmendmentBy = string.Empty;
    string SentToAmendmentOn = string.Empty;
    string SentToAmendmentRemarks = string.Empty;

    int PaymentReleasedById = 0;
    string PaymentReleasedBy = string.Empty;
    string PaymentReleasedOn = string.Empty;
    string PaymentReleasedRemarks = string.Empty;

    #endregion


    public string GetHtmlForPDF(DataTable dtPaymenttDetails)
    {
        try
        {
            if (dtPaymenttDetails.Rows.Count > 0)
            {
                DataRow dr0 = dtPaymenttDetails.Rows[0];

                if (dr0["PAYMENT_REQUEST_NO"] != DBNull.Value) PaymentRequestNo = Convert.ToString(dr0["PAYMENT_REQUEST_NO"]).Trim();
                if (dr0["DATE_OF_REQUEST"] != DBNull.Value) DateOfRequest = Convert.ToString(dr0["DATE_OF_REQUEST"]).Trim();
                if (dr0["STATUS_FID"] != DBNull.Value) StatusFid = Convert.ToString(dr0["STATUS_FID"]).Trim();
                if (dr0["STATUS"] != DBNull.Value) Status = Convert.ToString(dr0["STATUS"]).Trim();
                if (dr0["JOB_NO"] != DBNull.Value) JobNo = Convert.ToString(dr0["JOB_NO"]).Trim();
                if (dr0["VENDOR_CODE"] != DBNull.Value) VendorCode = Convert.ToString(dr0["VENDOR_CODE"]).Trim();
                if (dr0["VENDOR_NAME"] != DBNull.Value) VendorName = Convert.ToString(dr0["VENDOR_NAME"]).Trim();
                if (dr0["VENDOR_ADDRESS"] != DBNull.Value) VendorAddress = Convert.ToString(dr0["VENDOR_ADDRESS"]).Trim();

                if (dr0["PO_NO"] != DBNull.Value) PoNo = Convert.ToString(dr0["PO_NO"]).Trim();

                if (dr0["PO_AMOUNT"] != DBNull.Value) PoAmount = Convert.ToString(dr0["PO_AMOUNT"]).Trim();
                else PoAmount = "0.000";

                if (dr0["PO_AMOUNT_CURRENCY"] != DBNull.Value) PoCurrency = Convert.ToString(dr0["PO_AMOUNT_CURRENCY"]).Trim();

                if (dr0["FINANCIAL_YEAR"] != DBNull.Value) FinancialYear = Convert.ToString(dr0["FINANCIAL_YEAR"]).Trim();
                if (dr0["AMOUNT_TO_BE_RELEASED"] != DBNull.Value) AmountToBeReleased = Convert.ToString(dr0["AMOUNT_TO_BE_RELEASED"]).Trim();
                if (dr0["PAYMENT_TYPE"] != DBNull.Value) PaymentType = Convert.ToString(dr0["PAYMENT_TYPE"]).Trim();
                if (dr0["INVOICE_NO"] != DBNull.Value) InvoiceNo = Convert.ToString(dr0["INVOICE_NO"]).Trim();
                if (dr0["INVOICE_DATE"] != DBNull.Value) InvoiceDate = Convert.ToString(dr0["INVOICE_DATE"]).Trim();

                if (dr0["BANK_NAME"] != DBNull.Value) BankName = Convert.ToString(dr0["BANK_NAME"]).Trim();
                if (dr0["BANK_BRANCH"] != DBNull.Value) BankBranch = Convert.ToString(dr0["BANK_BRANCH"]).Trim();

                if (dr0["SWIFT_CODE"] != DBNull.Value) SwiftCode = Convert.ToString(dr0["SWIFT_CODE"]).Trim();
                if (dr0["BANK_ACCOUNT_NO"] != DBNull.Value) BankAccountNo = Convert.ToString(dr0["BANK_ACCOUNT_NO"]).Trim();
                if (dr0["CUT_OFF_DATE"] != DBNull.Value) paymentReleaseCutOffDate = Convert.ToString(dr0["CUT_OFF_DATE"]).Trim();
                if (dr0["BANK_ADVICE"] != DBNull.Value) BankAdvice = Convert.ToString(dr0["BANK_ADVICE"]).Trim();
                if (dr0["DATE_OF_PAYMENT_RELEASED"] != DBNull.Value) DateOfPaymentReleased = Convert.ToString(dr0["DATE_OF_PAYMENT_RELEASED"]).Trim();

                if (dr0["CREATED_BY_ID"] != DBNull.Value) CreatedById = Convert.ToInt32(dr0["CREATED_BY_ID"]);
                if (dr0["CREATED_BY"] != DBNull.Value) CreatedBy = Convert.ToString(dr0["CREATED_BY"]).Trim();
                if (dr0["CREATED_ON"] != DBNull.Value) CreatedOn = Convert.ToString(dr0["CREATED_ON"]).Trim();
                if (dr0["CREATED_REMARKS"] != DBNull.Value) CreatedRemarks = Convert.ToString(dr0["CREATED_REMARKS"]);

                if (dr0["CANCELLED_BY_ID"] != DBNull.Value) CancelledById = Convert.ToInt32(dr0["CANCELLED_BY_ID"]);
                if (dr0["CANCELLED_BY"] != DBNull.Value) CancelledBy = Convert.ToString(dr0["CANCELLED_BY"]).Trim();
                if (dr0["CANCELLED_ON"] != DBNull.Value) CancelledOn = Convert.ToString(dr0["CANCELLED_ON"]).Trim();
                if (dr0["CANCELLED_REMARKS"] != DBNull.Value) CancelledRemarks = Convert.ToString(dr0["CANCELLED_REMARKS"]);

                if (dr0["APPROVED_BY_ID"] != DBNull.Value) ApprovedById = Convert.ToInt32(dr0["APPROVED_BY_ID"]);
                if (dr0["APPROVED_BY"] != DBNull.Value) ApprovedBy = Convert.ToString(dr0["APPROVED_BY"]).Trim();
                if (dr0["APPROVED_ON"] != DBNull.Value) ApprovedOn = Convert.ToString(dr0["APPROVED_ON"]).Trim();
                if (dr0["APPROVED_REMARKS"] != DBNull.Value) ApprovedRemarks = Convert.ToString(dr0["APPROVED_REMARKS"]).Trim();

                if (dr0["AMENDED_BY_ID"] != DBNull.Value) AmendedById = Convert.ToInt32(dr0["AMENDED_BY_ID"]);
                if (dr0["AMENDED_BY"] != DBNull.Value) AmendedBy = Convert.ToString(dr0["AMENDED_BY"]).Trim();
                if (dr0["AMENDED_ON"] != DBNull.Value) AmendedOn = Convert.ToString(dr0["AMENDED_ON"]).Trim();
                if (dr0["AMENDED_REMARKS"] != DBNull.Value) AmendedRemarks = Convert.ToString(dr0["AMENDED_REMARKS"]).Trim();

                if (dr0["APPROVED_AMENDED_BY_ID"] != DBNull.Value) ApprovedAmendedById = Convert.ToInt32(dr0["APPROVED_AMENDED_BY_ID"]);
                if (dr0["APPROVED_AMENDED_BY"] != DBNull.Value) ApprovedAmendedBy = Convert.ToString(dr0["APPROVED_AMENDED_BY"]).Trim();
                if (dr0["APPROVED_AMENDED_ON"] != DBNull.Value) ApprovedAmendedOn = Convert.ToString(dr0["APPROVED_AMENDED_ON"]).Trim();
                if (dr0["APPROVED_AMENDED_REMARKS"] != DBNull.Value) ApprovedAmendedRemarks = Convert.ToString(dr0["APPROVED_AMENDED_REMARKS"]).Trim();

                if (dr0["SENT_TO_AMENDMENT_BY_ID"] != DBNull.Value) SentToAmendmentById = Convert.ToInt32(dr0["SENT_TO_AMENDMENT_BY_ID"]);
                if (dr0["SENT_TO_AMENDMENT_BY"] != DBNull.Value) SentToAmendmentBy = Convert.ToString(dr0["SENT_TO_AMENDMENT_BY"]).Trim();
                if (dr0["SENT_TO_AMENDMENT_ON"] != DBNull.Value) SentToAmendmentOn = Convert.ToString(dr0["SENT_TO_AMENDMENT_ON"]).Trim();
                if (dr0["SENT_TO_AMENDMENT_REMARKS"] != DBNull.Value) SentToAmendmentRemarks = Convert.ToString(dr0["SENT_TO_AMENDMENT_REMARKS"]).Trim();

                if (dr0["PAYMENT_RELEASED_BY_ID"] != DBNull.Value) PaymentReleasedById = Convert.ToInt32(dr0["PAYMENT_RELEASED_BY_ID"]);
                if (dr0["PAYMENT_RELEASED_BY"] != DBNull.Value) PaymentReleasedBy = Convert.ToString(dr0["PAYMENT_RELEASED_BY"]).Trim();
                if (dr0["PAYMENT_RELEASED_ON"] != DBNull.Value) PaymentReleasedOn = Convert.ToString(dr0["PAYMENT_RELEASED_ON"]).Trim();
                if (dr0["PAYMENT_RELEASED_REMARKS"] != DBNull.Value) PaymentReleasedRemarks = Convert.ToString(dr0["PAYMENT_RELEASED_REMARKS"]).Trim();

            }

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();



            sb.Append("<h2 class='headerStyle'><u>FC VENDOR PAYMENT</u></h2>\n");
            sb.Append("<hr />\n");
            sb.Append("<table class='tblheader'>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Payment Request No.:</td>\n");
            sb.Append("<td class='td2header'><b>{#PaymentRequestNo#}</b></td>\n");
            sb.Append("<td class='td1header'>Date Of Request:</td>\n");
            sb.Append("<td class='td2header'>{#DateOfRequest#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Vendor Name:</td>\n");
            sb.Append("<td class='td2header' colspan='3'>{#VendorName#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Vendor Address:</td>\n");
            sb.Append("<td class='td2header' colspan='3'>{#VendorAddress#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Status:</td>\n");
            sb.Append("<td class='td2header'><b>{#Status#}</b></td>\n");
            sb.Append("<td class='td1header'>JOB Number:</td>\n");
            sb.Append("<td class='td2header'>{#JOBNumber#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>PO Number:</td>\n");
            sb.Append("<td class='td2header'>{#PONumber#}</td>\n");
            sb.Append("<td class='td1header'>PO Amount:</td>\n");
            sb.Append("<td class='td2header'>{#POAmount#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Amount To Be Released:</td>\n");
            sb.Append("<td class='td2header'>{#AmountToBeReleased#}</td>\n");
            sb.Append("<td class='td1header'>Currency:</td>\n");
            sb.Append("<td class='td2header'>{#Currency#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Payment Type:</td>\n");
            sb.Append("<td class='td2header'>{#PaymentType#}</td>\n");
            sb.Append("<td class='td1header'>Invoice No.:</td>\n");
            sb.Append("<td class='td2header'>{#InvoiceNo#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Invoice Date:</td>\n");
            sb.Append("<td class='td2header'>{#InvoiceDate#}</td>\n");
            sb.Append("<td class='td1header'>Payment Release Cut-Off Date:</td>\n");
            sb.Append("<td class='td2header'>{#PaymentReleaseCutOffDate#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Bank Name:</td>\n");
            sb.Append("<td class='td2header'>{#BankName#}</td>\n");
            sb.Append("<td class='td1header'>Bank Branch:</td>\n");
            sb.Append("<td class='td2header'>{#BankBranch#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>SWIFT Code:</td>\n");
            sb.Append("<td class='td2header'>{#SWIFTCode#}</td>\n");
            sb.Append("<td class='td1header'>Bank Account No.:</td>\n");
            sb.Append("<td class='td2header'>{#BankAccountNo#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Bank Advice:</td>\n");
            sb.Append("<td class='td2header'>{#BankAdvice#}</td>\n");
            sb.Append("<td class='td1header'>Date Of Payment Released:</td>\n");
            sb.Append("<td class='td2header'>{#DateOfPaymentReleased#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("</table>\n");

            sb.Replace("{#PaymentRequestNo#}", PaymentRequestNo);
            sb.Replace("{#DateOfRequest#}", DateOfRequest);
            sb.Replace("{#VendorName#}", VendorName + " [" + VendorCode + "]");
            sb.Replace("{#VendorAddress#}", VendorAddress);
            sb.Replace("{#Status#}", Status);
            sb.Replace("{#JOBNumber#}", JobNo);
            sb.Replace("{#PONumber#}", PoNo);
            sb.Replace("{#POAmount#}", PoAmount);
            sb.Replace("{#AmountToBeReleased#}", AmountToBeReleased);
            sb.Replace("{#Currency#}", PoCurrency);

            sb.Replace("{#PaymentType#}", PaymentType);
            sb.Replace("{#InvoiceNo#}", InvoiceNo);
            sb.Replace("{#InvoiceDate#}", InvoiceDate);
            sb.Replace("{#PaymentReleaseCutOffDate#}", paymentReleaseCutOffDate);
            sb.Replace("{#BankName#}", BankName);

            sb.Replace("{#BankBranch#}", BankBranch);
            sb.Replace("{#SWIFTCode#}", SwiftCode);
            sb.Replace("{#BankAccountNo#}", BankAccountNo);
            sb.Replace("{#BankAdvice#}", BankAdvice);
            sb.Replace("{#DateOfPaymentReleased#}", DateOfPaymentReleased);


            //SIGNATORIES DETAILS START[===========================]

            sb.Append("<hr class='hrsignatories' />\n");
            sb.Append("<h3 class='header'>Signatories</h3>\n");

            if (CreatedById > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Created</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Created Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#CreatedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Created By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#CreatedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Created On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#CreatedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");

                sb.Replace("{#CreatedRemarks#}", CreatedRemarks);
                sb.Replace("{#CreatedBy#}", CreatedBy);
                sb.Replace("{#CreatedOn#}", CreatedOn);

            }


            if (CancelledById > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Cancelled</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Cancelled Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#CancelledRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Cancelled By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#CancelledBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Cancelled On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#CancelledOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");

                sb.Replace("{#CancelledRemarks#}", CancelledRemarks);
                sb.Replace("{#CancelledBy#}", CancelledBy);
                sb.Replace("{#CancelledOn#}", CancelledOn);

            }

            if (ApprovedById > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Approved</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Approved Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#ApprovedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Approved By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#ApprovedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Approved On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#ApprovedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");

                sb.Replace("{#ApprovedRemarks#}", ApprovedRemarks);
                sb.Replace("{#ApprovedBy#}", ApprovedBy);
                sb.Replace("{#ApprovedOn#}", ApprovedOn);

            }

            if (SentToAmendmentById > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Sent To Amendment</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Sent To Amendment Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#SentToAmendmentRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Sent To Amendment By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#SentToAmendmentBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Sent To Amendment On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#SentToAmendmentOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");

                sb.Replace("{#SentToAmendmentRemarks#}", SentToAmendmentRemarks);
                sb.Replace("{#SentToAmendmentBy#}", SentToAmendmentBy);
                sb.Replace("{#SentToAmendmentOn#}", SentToAmendmentOn);

            }

            if (AmendedById > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Amended</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Amended Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#AmendedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Amended By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#AmendedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Amended On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#AmendedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");

                sb.Replace("{#AmendedRemarks#}", AmendedRemarks);
                sb.Replace("{#AmendedBy#}", AmendedBy);
                sb.Replace("{#AmendedOn#}", AmendedOn);

            }

            if (ApprovedAmendedById > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Amended Approved</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Amended Approved Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#AmendedApprovedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Amended Approved By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#AmendedApprovedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Amended Approved On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#AmendedApprovedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");

                sb.Replace("{#AmendedApprovedRemarks#}", ApprovedAmendedRemarks);
                sb.Replace("{#AmendedApprovedBy#}", ApprovedAmendedBy);
                sb.Replace("{#AmendedApprovedOn#}", ApprovedAmendedOn);

            }

            if (PaymentReleasedById > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Payment Released</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Payment Released Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#PaymentReleasedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Payment Released By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#PaymentReleasedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Payment Released On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#PaymentReleasedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");
                sb.Append("<hr class='hrsignatories' />\n");

                sb.Replace("{#PaymentReleasedRemarks#}", PaymentReleasedRemarks);
                sb.Replace("{#PaymentReleasedBy#}", PaymentReleasedBy);
                sb.Replace("{#PaymentReleasedOn#}", PaymentReleasedOn);

            }

            //SIGNATORIES DETAILS END[===========================]


            htmlText = sb.ToString();
            return htmlText;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}

