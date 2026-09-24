using System;
using System.Text;
using System.Data;


public class CBVouchersHtmlForPDF
{
    #region Variables

    string VoucherNo = string.Empty;
    string VoucherDate = string.Empty;
    string VoucherCreatedBy = string.Empty;
    string VoucherType = string.Empty;
    string Unit = string.Empty;

    int AuthorizedById = 0;
    string AuthorizedBy = string.Empty;
    string AuthorizedOn = string.Empty;
    string AuthorizedRemarks = string.Empty;

    int ApprovedById = 0;
    string ApprovedBy = string.Empty;
    string ApprovedOn = string.Empty;
    string ApprovedRemarks = string.Empty;
    //private string type = string.Empty;
    private string ReceiptAmountFC = string.Empty;
    private string PaymentAmountFC = string.Empty;
    private string ReceiptAmountINR = string.Empty;
    //private string PaidToOrReceivedFrom = string.Empty;
    private string DOCClass = string.Empty;
    private string PaymentAmountINR = string.Empty;
    private string NetAmount = "0";

    private string CurrencyDesc = string.Empty;
    private string CurrencyRate = "0";

    private string INRBasicAmount = "0";
    private string INROtherAmount = "0";

    private string FCBasicAmount = "0";
    private string FCOtherAmount = "0";

    #endregion


    public string GetHtmlForPDF(DataSet dsDetails)
    {
        try
        {
            if (dsDetails.Tables.Count > 0 && dsDetails.Tables[0].Rows.Count > 0)
            {
                DataRow dr0 = dsDetails.Tables[0].Rows[0];

                if (dr0["VOUCHER_NO"] != DBNull.Value) VoucherNo = Convert.ToString(dr0["VOUCHER_NO"]).Trim();
                if (dr0["VOUCHER_DATE"] != DBNull.Value) VoucherDate = Convert.ToString(dr0["VOUCHER_DATE"]).Trim();

                //if (dr0["TYPE"] != DBNull.Value) type = Convert.ToString(dr0["TYPE"]).Trim();
                if (dr0["DOC_CLASS"] != DBNull.Value) DOCClass = Convert.ToString(dr0["DOC_CLASS"]).Trim();
                if (dr0["CURRENCY_DESC"] != DBNull.Value) CurrencyDesc = Convert.ToString(dr0["CURRENCY_DESC"]).Trim();

                if (dr0["RECEIPT_AMOUNT_FC"] != DBNull.Value) ReceiptAmountFC = Convert.ToString(dr0["RECEIPT_AMOUNT_FC"]).Trim();
                if (dr0["PAYMENT_AMOUNT_FC"] != DBNull.Value) PaymentAmountFC = Convert.ToString(dr0["PAYMENT_AMOUNT_FC"]).Trim();
                if (dr0["RECEIPT_AMOUNT_INR"] != DBNull.Value) ReceiptAmountINR = Convert.ToString(dr0["RECEIPT_AMOUNT_INR"]).Trim();
                if (dr0["PAYMENT_AMOUNT_INR"] != DBNull.Value) PaymentAmountINR = Convert.ToString(dr0["PAYMENT_AMOUNT_INR"]).Trim();
                //if (dr0["PAID_TO_OR_RECEIVED_FROM"] != DBNull.Value) PaidToOrReceivedFrom = Convert.ToString(dr0["PAID_TO_OR_RECEIVED_FROM"]).Trim();
                if (dr0["VOUCHER_TYPE"] != DBNull.Value) VoucherType = Convert.ToString(dr0["VOUCHER_TYPE"]).Trim();
                if (dr0["VOUCHER_CREATED_BY"] != DBNull.Value) VoucherCreatedBy = Convert.ToString(dr0["VOUCHER_CREATED_BY"]).Trim();
                if (dr0["UNIT_NAME"] != DBNull.Value) Unit = Convert.ToString(dr0["UNIT_NAME"]).Trim();

                if (dr0["AUTHORIZED_BY_ID"] != DBNull.Value) AuthorizedById = Convert.ToInt32(dr0["AUTHORIZED_BY_ID"]);
                if (dr0["AUTHORIZED_BY"] != DBNull.Value) AuthorizedBy = Convert.ToString(dr0["AUTHORIZED_BY"]).Trim();
                if (dr0["AUTHORIZED_ON"] != DBNull.Value) AuthorizedOn = Convert.ToString(dr0["AUTHORIZED_ON"]).Trim();
                if (dr0["AUTHORIZED_REMARKS"] != DBNull.Value) AuthorizedRemarks = Convert.ToString(dr0["AUTHORIZED_REMARKS"]);

                if (dr0["APPROVED_BY_ID"] != DBNull.Value) ApprovedById = Convert.ToInt32(dr0["APPROVED_BY_ID"]);
                if (dr0["APPROVED_BY"] != DBNull.Value) ApprovedBy = Convert.ToString(dr0["APPROVED_BY"]).Trim();
                if (dr0["APPROVED_ON"] != DBNull.Value) ApprovedOn = Convert.ToString(dr0["APPROVED_ON"]).Trim();
                if (dr0["APPROVED_REMARKS"] != DBNull.Value) ApprovedRemarks = Convert.ToString(dr0["APPROVED_REMARKS"]).Trim();
            }

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();


            sb.Append("<h2 class='headerStyle'><u>CASH BANK VOUCHER COPY</u></h2>\n");
            sb.Append("<hr />\n");
            sb.Append("<table class='tblheader'>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Voucher No.:</td>\n");
            sb.Append("<td class='td2header'><b>{#VoucherNo#}</b></td>\n");
            sb.Append("<td class='td1header'>Voucher Date:</td>\n");
            sb.Append("<td class='td2header'>{#VoucherDate#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            //sb.Append("<td class='td1header'>Type:</td>\n");
            //sb.Append("<td class='td2header'><b>{#type#}</b></td>\n");
            sb.Append("<td class='td1header'>DOC Class:</td>\n");
            sb.Append("<td class='td2header'><b>{#DOCClass#}</b></td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Unit:</td>\n");
            sb.Append("<td class='td2header'>{#Unit#}</td>\n");
            sb.Append("<td class='td1header'>Currency Desc:</td>\n");
            sb.Append("<td class='td2header'>{#CurrencyDesc#}</td>\n");
            sb.Append("</tr>\n");


            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Receipt Amount FC:</td>\n");
            sb.Append("<td class='td2header'>{#ReceiptAmountFC#}</td>\n");
            sb.Append("<td class='td1header'>Payment Amount FC:</td>\n");
            sb.Append("<td class='td2header'>{#PaymentAmountFC#}</td>\n");
            sb.Append("</tr>\n");


            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Receipt Amount INR:</td>\n");
            sb.Append("<td class='td2header'>{#ReceiptAmountINR#}</td>\n");
            sb.Append("<td class='td1header'>Payment Amount INR:</td>\n");
            sb.Append("<td class='td2header'>{#PaymentAmountINR#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            //sb.Append("<td class='td1header'>Paid To Or Received From:</td>\n");
            //sb.Append("<td class='td2header'>{#PaidToOrReceivedFrom#}</td>\n");
            sb.Append("<td class='td1header'>Voucher Created By:</td>\n");
            sb.Append("<td class='td2header'><b>{#VoucherCreatedBy#}</b></td>\n");
            sb.Append("</tr>\n");

            sb.Append("</table>\n");

            sb.Replace("{#VoucherNo#}", VoucherNo);
            sb.Replace("{#VoucherDate#}", VoucherDate);
            //sb.Replace("{#type#}", type);
            sb.Replace("{#DOCClass#}", DOCClass);
            sb.Replace("{#ReceiptAmountFC#}", ReceiptAmountFC);
            sb.Replace("{#PaymentAmountFC#}", PaymentAmountFC);
            sb.Replace("{#ReceiptAmountINR#}", ReceiptAmountINR);
            sb.Replace("{#PaymentAmountINR#}", PaymentAmountINR);
            //sb.Replace("{#PaidToOrReceivedFrom#}", PaidToOrReceivedFrom);
            sb.Replace("{#CurrencyDesc#}", CurrencyDesc);

            sb.Replace("{#VoucherCreatedBy#}", VoucherCreatedBy);
            sb.Replace("{#Unit#}", Unit);


            //VOUCHER DETAILS START[===========================]

            int srNo = 0;

            if (dsDetails.Tables[1].Rows.Count > 0)
            {
                sb.Append("<h3 class='header2'>Voucher Details</h3>\n");

                sb.Append("<table class='tblsubitems'>\n");

                sb.Append("<tr class='trsubitems'>\n");

                sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
                sb.Append("<th class='tdtag'>Class</th>\n");
                sb.Append("<th class='tdtag'>GL Code</th>\n");
                sb.Append("<th class='tddesc'>GL Description</th>\n");
                sb.Append("<th class='tdtag'>Currency</th>\n");
                sb.Append("<th class='tdquantity'>Receipt Amount FC</th>\n");
                sb.Append("<th class='tdquantity'>Payment Amount FC</th>\n");
                sb.Append("<th class='tdquantity'>Receipt Amount INR</th>\n");
                sb.Append("<th class='tdquantity'>Payment Amount INR</th>\n");
                sb.Append("<th class='tdtag'>Cheque</th>\n");
                sb.Append("<th class='tdtag'>Cheque Dated</th>\n");
                sb.Append("<th class='tddesc'>Narration</th>\n");

                sb.Append("</tr>\n");

                foreach (DataRow dr in dsDetails.Tables[1].Rows)
                {
                    srNo++;
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
                    sb.Append("<td class='tdtag'>{#Class#}</td>\n");
                    sb.Append("<td class='tdtag'>{#GLCode#}</td>\n");
                    sb.Append("<td class='tddesc'>{#GLDescription#}</td>\n");
                    sb.Append("<td class='tdtag'>{#CurrencyDesc#}</td>\n");
                    sb.Append("<td class='tdquantity'>{#ReceiptAmountFC#}</td>\n");
                    sb.Append("<td class='tdquantity'>{#PaymentAmountFC#}</td>\n");
                    sb.Append("<td class='tdquantity'>{#ReceiptAmountINR#}</td>\n");
                    sb.Append("<td class='tdquantity'>{#PaymentAmountINR#}</td>\n");
                    sb.Append("<td class='tdtag'>{#Cheque#}</td>\n");
                    sb.Append("<td class='tdtag'>{#ChequeDated#}</td>\n");
                    sb.Append("<td class='tddesc'>{#Narration#}</td>\n");

                    sb.Append("</tr>\n");


                    sb.Replace("{#SRNo#}", Convert.ToString(srNo));
                    sb.Replace("{#Class#}", Convert.ToString(dr["DOC_CLASS"]));
                    sb.Replace("{#GLCode#}", Convert.ToString(dr["GLCODE"]));
                    sb.Replace("{#GLDescription#}", Convert.ToString(dr["GL_DESCRIPTION"]));
                    sb.Replace("{#CurrencyDesc#}", Convert.ToString(dr["CURRENCY_DESC"]));
                    sb.Replace("{#ReceiptAmountFC#}", Convert.ToString(dr["RECEIPT_AMOUNT_FC"]));
                    sb.Replace("{#PaymentAmountFC#}", Convert.ToString(dr["PAYMENT_AMOUNT_FC"]));
                    sb.Replace("{#ReceiptAmountINR#}", Convert.ToString(dr["RECEIPT_AMOUNT_INR"]));
                    sb.Replace("{#PaymentAmountINR#}", Convert.ToString(dr["PAYMENT_AMOUNT_INR"]));
                    sb.Replace("{#Cheque#}", Convert.ToString(dr["CHEQUE_OR_REFERENCE_NO"]));
                    sb.Replace("{#ChequeDated#}", Convert.ToString(dr["CHEQUE_DATED"]));
                    sb.Replace("{#Narration#}", Convert.ToString(dr["NARRATION"]));

                }
                sb.Append("</table>\n");
            }


            //VOUCHER DETAILS END[===========================]


            //SIGNATORIES DETAILS START[===========================]

            sb.Append("<hr class='hrsignatories' />\n");
            //sb.Append("<h3 class='header'>Signatories</h3>\n");
            sb.Append("<h4 class='headerStyle'><u>Signatories</u></h4>\n");

            if (AuthorizedById > 0)
            {
                sb.Append("<hr class='hrsignatories' />\n");
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Authorized</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Authorized Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#AuthorizedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Authorized By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#AuthorizedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Authorized On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#AuthorizedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");

                sb.Replace("{#AuthorizedRemarks#}", AuthorizedRemarks);
                sb.Replace("{#AuthorizedBy#}", AuthorizedBy);
                sb.Replace("{#AuthorizedOn#}", AuthorizedOn);

            }

            if (dsDetails.Tables[2].Rows.Count > 0)
            {
                srNo = 0;

                sb.Append("<h3 class='header2'>Reauthorization Details</h3>\n");

                sb.Append("<table class='tblsubitems'>\n");

                sb.Append("<tr class='trsubitems'>\n");

                sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
                sb.Append("<th class='tdtag'>Sent To Reauthorization By</th>\n");
                sb.Append("<th class='tdtag'>Sent To Reauthorization On</th>\n");
                sb.Append("<th class='tdtag'>Reauthorize By</th>\n");
                sb.Append("<th class='tdtag'>Reauthorize On</th>\n");
                
                sb.Append("</tr>\n");

                foreach (DataRow dr in dsDetails.Tables[1].Rows)
                {
                    srNo++;
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
                    sb.Append("<td class='tdtag'>{#SentToReauthorizationBy#}</td>\n");
                    sb.Append("<td class='tdtag'>{#SentToReauthorizationOn#}</td>\n");
                    sb.Append("<td class='tddesc'>{#SentToReauthorizationRemarks#}</td>\n");
                    sb.Append("<td class='tdtag'>{#ReauthorizeBy#}</td>\n");
                    sb.Append("<td class='tdtag'>{#ReauthorizeOn#}</td>\n");
                    sb.Append("<td class='tddesc'>{#ReauthorizRemarks#}</td>\n");

                    sb.Append("</tr>\n");

                    sb.Replace("{#SRNo#}", Convert.ToString(srNo));
                    sb.Replace("{#SentToReauthorizationBy#}", Convert.ToString(dr["SEND_TO_REAUTHORIZATION_BY"]));
                    sb.Replace("{#SentToReauthorizationOn#}", Convert.ToString(dr["SEND_TO_REAUTHORIZATION_ON"]));
                    sb.Replace("{#SentToReauthorizationRemarks#}", Convert.ToString(dr["SEND_TO_REAUTHORIZATION_REMARKS"]));
                    
                    sb.Replace("{#ReauthorizeBy#}", Convert.ToString(dr["REAUTHORIZED_BY"]));
                    sb.Replace("{#ReauthorizeOn#}", Convert.ToString(dr["REAUTHORIZED_ON"]));
                    sb.Replace("{#ReauthorizRemarks#}", Convert.ToString(dr["REAUTHORIZED_REMARKS"]));

                }
                sb.Append("</table>\n");
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

