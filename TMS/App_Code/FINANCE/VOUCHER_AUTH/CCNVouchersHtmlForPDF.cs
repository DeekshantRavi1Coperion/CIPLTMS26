using System;
using System.Text;
using System.Data;


public class CCNVouchersHtmlForPDF
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

    private string CustomerCode = string.Empty;
    private string CustomerName = string.Empty;
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

                if (dr0["CUSTOMER_CODE"] != DBNull.Value) CustomerCode = Convert.ToString(dr0["CUSTOMER_CODE"]).Trim();
                if (dr0["CUSTOMER_NAME"] != DBNull.Value) CustomerName = Convert.ToString(dr0["CUSTOMER_NAME"]).Trim();
                if (dr0["CURR_DESC"] != DBNull.Value) CurrencyDesc = Convert.ToString(dr0["CURR_DESC"]).Trim();
                if (dr0["CURR_RATE"] != DBNull.Value) CurrencyRate = Convert.ToString(dr0["CURR_RATE"]).Trim();

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


            sb.Append("<h2 class='headerStyle'><u>CUSTOMER CREDIT NOTE VOUCHER COPY</u></h2>\n");
            sb.Append("<hr />\n");
            sb.Append("<table class='tblheader'>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Voucher No.:</td>\n");
            sb.Append("<td class='td2header'><b>{#VoucherNo#}</b></td>\n");
            sb.Append("<td class='td1header'>Voucher Date:</td>\n");
            sb.Append("<td class='td2header'>{#VoucherDate#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Customer Code:</td>\n");
            sb.Append("<td class='td2header'><b>{#CustomerCode#}</b></td>\n");
            sb.Append("<td class='td1header'>Customer Name:</td>\n");
            sb.Append("<td class='td2header'>{#CustomerName#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Currency Desc:</td>\n");
            sb.Append("<td class='td2header'><b>{#CurrencyDesc#}</b></td>\n");
            sb.Append("<td class='td1header'>Currency Rate:</td>\n");
            sb.Append("<td class='td2header'>{#CurrencyRate#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>DOC Class:</td>\n");
            sb.Append("<td class='td2header'><b>{#DOCClass#}</b></td>\n");
            sb.Append("<td class='td1header'>Unit:</td>\n");
            sb.Append("<td class='td2header'>{#Unit#}</td>\n");
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
            sb.Replace("{#CustomerCode#}", CustomerCode);
            sb.Replace("{#CustomerName#}", CustomerName);            
            sb.Replace("{#DOCClass#}", DOCClass);
            sb.Replace("{#CurrencyDesc#}", CurrencyDesc);
            sb.Replace("{#CurrencyRate#}", CurrencyRate);
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
                sb.Append("<th class='tdtag'>GL Code</th>\n");
                sb.Append("<th class='tddesc'>GL Description</th>\n");
                sb.Append("<th class='tdtag'>Amount</th>\n");

                sb.Append("</tr>\n");

                foreach (DataRow dr in dsDetails.Tables[1].Rows)
                {
                    srNo++;
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
                    sb.Append("<td class='tdtag'>{#GLCode#}</td>\n");
                    sb.Append("<td class='tddesc'>{#GLDescription#}</td>\n");
                    sb.Append("<td class='tdtag'>{#Amount#}</td>\n");

                    sb.Append("</tr>\n");


                    sb.Replace("{#SRNo#}", Convert.ToString(srNo));
                    sb.Replace("{#GLCode#}", Convert.ToString(dr["GL_CODE"]));
                    sb.Replace("{#GLDescription#}", Convert.ToString(dr["GL_DESCRIPTION"]));
                    sb.Replace("{#Amount#}", Convert.ToString(dr["AMOUNT"]));

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
                sb.Append("<th class='tddesc'>Sent To Reauthorization Remarks</th>\n");
                sb.Append("<th class='tdtag'>Reauthorize By</th>\n");
                sb.Append("<th class='tdtag'>Reauthorize On</th>\n");
                sb.Append("<th class='tddesc'>Reauthorize Remarks</th>\n");

                sb.Append("</tr>\n");

                foreach (DataRow dr in dsDetails.Tables[2].Rows)
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

