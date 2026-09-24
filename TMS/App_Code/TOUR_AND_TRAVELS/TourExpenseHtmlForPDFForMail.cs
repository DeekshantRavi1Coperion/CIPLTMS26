using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Net.Mime;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;
using System.Xml;
using iTextSharp.tool.xml.css;
using System.Data;


public class TourExpenseHtmlForPDFForMail
{
    #region Variables

    string tourExpenseNo = string.Empty;
    string tourSanctionNo = string.Empty;
    string employeeName = string.Empty;
    string employeeCode = string.Empty;
    string startDate = string.Empty;
    string endDate = string.Empty;
    string customerName = string.Empty;
    string employeeDesignation = string.Empty;
    string placeOfVisit = string.Empty;
    string jobNo = string.Empty;
    int statusId = 0;
    string statusName = string.Empty;
    string airTicketAmount = string.Empty;
    string hotelAmount = string.Empty;
    string taxiAmount = string.Empty;
    string othersAmount = string.Empty;
    string advanceAmount = string.Empty;
    string totalAmount = string.Empty;
    string currencyCode = string.Empty;

    string voucherNumber = string.Empty;
    string voucherDate = string.Empty;

    string invoiceNumber = string.Empty;
    string invoiceDate = string.Empty;

    int createdById = 0;
    string createdBy = string.Empty;
    string createdByEmailId = string.Empty;
    string createdOn = string.Empty;
    string createdRemarks = string.Empty;

    int closedById = 0;
    string closedBy = string.Empty;
    string closedByEmailId = string.Empty;
    string closedOn = string.Empty;
    string closedRemarks = string.Empty;



    #endregion


    public string GetHtmlForPDF(DataTable dtDetails)
    {
        try
        {
            tourExpenseNo = string.Empty;
            tourSanctionNo = string.Empty;
            employeeName = string.Empty;
            employeeCode = string.Empty;
            startDate = string.Empty;
            endDate = string.Empty;
            customerName = string.Empty;
            employeeDesignation = string.Empty;
            placeOfVisit = string.Empty;
            jobNo = string.Empty;
            statusId = 0;
            statusName = string.Empty;
            airTicketAmount = string.Empty;
            hotelAmount = string.Empty;
            taxiAmount = string.Empty;
            othersAmount = string.Empty;
            advanceAmount = string.Empty;
            totalAmount = string.Empty;
            currencyCode = string.Empty;

            voucherNumber = string.Empty;
            voucherDate = string.Empty;

            invoiceNumber = string.Empty;
            invoiceDate = string.Empty;

            createdById = 0;
            createdBy = string.Empty;
            createdByEmailId = string.Empty;
            createdOn = string.Empty;
            createdRemarks = string.Empty;
            closedById = 0;
            closedBy = string.Empty;
            closedByEmailId = string.Empty;
            closedOn = string.Empty;
            closedRemarks = string.Empty;


            if (dtDetails.Rows.Count > 0)
            {
                DataRow dr = dtDetails.Rows[0];

                if (dr["TOUR_EXPENSE_NO"] != DBNull.Value) tourExpenseNo = Convert.ToString(dr["TOUR_EXPENSE_NO"]);
                if (dr["TOUR_SANCTION_NO"] != DBNull.Value) tourSanctionNo = Convert.ToString(dr["TOUR_SANCTION_NO"]);
                if (dr["EMPLOYEE_NAME"] != DBNull.Value) employeeName = Convert.ToString(dr["EMPLOYEE_NAME"]);
                if (dr["EMPLOYEE_CODE"] != DBNull.Value) employeeCode = Convert.ToString(dr["EMPLOYEE_CODE"]);
                if (dr["START_DATE"] != DBNull.Value) startDate = Convert.ToString(dr["START_DATE"]);
                if (dr["END_DATE"] != DBNull.Value) endDate = Convert.ToString(dr["END_DATE"]);
                if (dr["CUSTOMER_NAME"] != DBNull.Value) customerName = Convert.ToString(dr["CUSTOMER_NAME"]);
                if (dr["EMPLOYEE_DESIGNATION"] != DBNull.Value) employeeDesignation = Convert.ToString(dr["EMPLOYEE_DESIGNATION"]);
                if (dr["PLACE_OF_VISIT"] != DBNull.Value) placeOfVisit = Convert.ToString(dr["PLACE_OF_VISIT"]);
                if (dr["JOB_NO"] != DBNull.Value) jobNo = Convert.ToString(dr["JOB_NO"]);

                if (dr["STATUS_ID"] != DBNull.Value) statusId = Convert.ToInt32(dr["STATUS_ID"]);
                if (dr["STATUS_NAME"] != DBNull.Value) statusName = Convert.ToString(dr["STATUS_NAME"]);

                if (dr["AIR_TICKET_AMOUNT"] != DBNull.Value) airTicketAmount = Convert.ToString(dr["AIR_TICKET_AMOUNT"]);
                if (dr["HOTEL_AMOUNT"] != DBNull.Value) hotelAmount = Convert.ToString(dr["HOTEL_AMOUNT"]);
                if (dr["TAXI_AMOUNT"] != DBNull.Value) taxiAmount = Convert.ToString(dr["TAXI_AMOUNT"]);
                if (dr["OTHERS_AMOUNT"] != DBNull.Value) othersAmount = Convert.ToString(dr["OTHERS_AMOUNT"]);
                if (dr["ADVANCE_AMOUNT"] != DBNull.Value) advanceAmount = Convert.ToString(dr["ADVANCE_AMOUNT"]);
                if (dr["TOTAL_AMOUNT"] != DBNull.Value) totalAmount = Convert.ToString(dr["TOTAL_AMOUNT"]);
                if (dr["CURRENCY_CODE"] != DBNull.Value) currencyCode = Convert.ToString(dr["CURRENCY_CODE"]);

                if (dr["VOUCHER_NO"] != DBNull.Value) voucherNumber = Convert.ToString(dr["VOUCHER_NO"]);
                if (dr["VOUCHER_DATE"] != DBNull.Value) voucherDate = Convert.ToString(dr["VOUCHER_DATE"]);

                if (dr["INVOICE_NO"] != DBNull.Value) invoiceNumber = Convert.ToString(dr["INVOICE_NO"]);
                if (dr["INVOICE_DATE"] != DBNull.Value) invoiceDate = Convert.ToString(dr["INVOICE_DATE"]);

                if (dr["CREATED_BY_ID"] != DBNull.Value) createdById = Convert.ToInt32(dr["CREATED_BY_ID"]);
                if (dr["CREATED_BY"] != DBNull.Value) createdBy = Convert.ToString(dr["CREATED_BY"]);
                if (dr["CREATED_BY_EMAIL_ID"] != DBNull.Value) createdByEmailId = Convert.ToString(dr["CREATED_BY_EMAIL_ID"]);
                if (dr["CREATED_ON"] != DBNull.Value) createdOn = Convert.ToString(dr["CREATED_ON"]);
                if (dr["CREATED_REMARKS"] != DBNull.Value) createdRemarks = Convert.ToString(dr["CREATED_REMARKS"]);

                if (dr["CLOSED_BY_ID"] != DBNull.Value) closedById = Convert.ToInt32(dr["CLOSED_BY_ID"]);
                if (dr["CLOSED_BY"] != DBNull.Value) closedBy = Convert.ToString(dr["CLOSED_BY"]);
                if (dr["CLOSED_BY_EMAIL_ID"] != DBNull.Value) closedByEmailId = Convert.ToString(dr["CLOSED_BY_EMAIL_ID"]);
                if (dr["CLOSED_ON"] != DBNull.Value) closedOn = Convert.ToString(dr["CLOSED_ON"]);
                if (dr["CLOSED_REMARKS"] != DBNull.Value) closedRemarks = Convert.ToString(dr["CLOSED_REMARKS"]);

            }

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.Append("<h2 class='headerStyle'>Tour Expense [{#tourExpenseNo#}]</h2>\n");
            sb.Append("<hr />\n");
            sb.Append("<table class='tblheader'>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Tour Sanction No.:</td>\n");
            sb.Append("<td class='td2header'>{#tourSanctionNo#}</td>\n");
            sb.Append("<td class='td1header'>Type Of Trip:</td>\n");
            sb.Append("<td class='td2header'>Non- reimbursable</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Emplyoee Name:</td>\n");
            sb.Append("<td class='td2header'>{#employeeName#}</td>\n");
            sb.Append("<td class='td1header'>Employee ID:</td>\n");
            sb.Append("<td class='td2header'>{#employeeCode#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Designation:</td>\n");
            sb.Append("<td class='td2header' colspan='3'>{#employeeDesignation#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Start Date Of Tour:</td>\n");
            sb.Append("<td class='td2header'>{#startDate#}</td>\n");
            sb.Append("<td class='td1header'>End Date Of Tour:</td>\n");
            sb.Append("<td class='td2header'>{#endDate#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Place Of Visit:</td>\n");
            sb.Append("<td class='td2header' colspan='3'>{#placeOfVisit#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Job/Enquiry No.:</td>\n");
            sb.Append("<td class='td2header'>{#jobNo#}</td>\n");
            sb.Append("<td class='td1header'>Status:</td>\n");
            sb.Append("<td class='td2header'>{#statusName#}</td>\n");
            sb.Append("</tr>\n");

            if (statusId == (int)TandTAllStatus.EnumTourExpenseStatus.Closed && closedById > 0)
            {
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Voucher Number:</td>\n");
                sb.Append("<td class='td2header'>{#voucherNumber#}</td>\n");
                sb.Append("<td class='td1header'>Voucher Date:</td>\n");
                sb.Append("<td class='td2header'>{#voucherDate#}</td>\n");
                sb.Append("</tr>\n");
            }



            //sb.Append("<tr>\n");
            //sb.Append("<td class='td1header'>Invoice Number:</td>\n");
            //sb.Append("<td class='td2header'>{#invoiceNumber#}</td>\n");
            //sb.Append("<td class='td1header'>Invoice Date:</td>\n");
            //sb.Append("<td class='td2header'>{#invoiceDate#}</td>\n");
            //sb.Append("</tr>\n");

            sb.Append("</table>\n");
            sb.Append("<hr />\n");
            sb.Append("<h3 class='header2'>Epenses</h3>\n");
            sb.Append("<table class='tblsuitems'>\n");
            sb.Append("<tr class='trsubitems'>\n");
            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            sb.Append("<th class='tdquantity'>Air Ticket</th>\n");
            sb.Append("<th class='tdquantity'>Hotel</th>\n");
            sb.Append("<th class='tdquantity'>Taxi</th>\n");
            sb.Append("<th class='tdquantity'>Others</th>\n");
            sb.Append("<th class='tdquantity'>Advance</th>\n");
            sb.Append("<th class='tdquantity'>Total</th>\n");
            sb.Append("<th class='tdquantity'>Currency</th>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='tdsrno'>1</td>\n");
            sb.Append("<td class='tdquantity'>{#airTicketAmount#}</td>\n");
            sb.Append("<td class='tdquantity'>{#hotelAmount#}</td>\n");
            sb.Append("<td class='tdquantity'>{#taxiAmount#}</td>\n");
            sb.Append("<td class='tdquantity'>{#othersAmount#}</td>\n");
            sb.Append("<td class='tdquantity'>{#advanceAmount#}</td>\n");
            sb.Append("<td class='tdquantity'>{#totalAmount#}</td>\n");
            sb.Append("<td class='tdquantity'>{#currencyCode#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("</table>\n");

            sb.Append("<h3 class='header2'>Signatories</h3>\n");
            sb.Append("<hr />\n");
            sb.Append("<fieldset class='pdffieldset'>\n");
            sb.Append("<legend class='pdflegend'>Created</legend>\n");
            sb.Append("<table class='tblsignatories'>\n");
            sb.Append("<tr>\n");
            sb.Append("<td colspan='3'>Remarks:</td>\n");
            sb.Append("<td colspan='3'>&nbsp;</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td colspan='4'>{#createdRemarks#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='tdsignatories1'>Created By:</td>\n");
            sb.Append("<td class='tdsignatories2'>{#createdBy#}</td>\n");
            sb.Append("<td class='tdsignatories1'>Created On:</td>\n");
            sb.Append("<td class='tdsignatories2'>{#createdOn#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("</table>\n");
            sb.Append("</fieldset>\n");

            sb.Append("<hr />\n");

            if (statusId == (int)TandTAllStatus.EnumTourExpenseStatus.Closed && closedById > 0)
            {
                sb.Append("<fieldset class='pdffieldset'>\n");
                sb.Append("<legend class='pdflegend'>Closed</legend>\n");
                sb.Append("<table class='tblsignatories'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='3'>Closed Remarks:</td>\n");
                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>{#closedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='tdsignatories1'>Closed By:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#closedBy#}</td>\n");
                sb.Append("<td class='tdsignatories1'>Closed On:</td>\n");
                sb.Append("<td class='tdsignatories2'>{#closedOn#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("</fieldset>\n");
                sb.Append("<hr />\n");
            }

            sb.Replace("{#tourExpenseNo#}", tourExpenseNo);
            sb.Replace("{#tourSanctionNo#}", tourSanctionNo);
            sb.Replace("{#employeeName#}", employeeName);
            sb.Replace("{#employeeCode#}", employeeCode);
            sb.Replace("{#startDate#}", startDate);
            sb.Replace("{#endDate#}", endDate);
            sb.Replace("{#customerName#}", customerName);
            sb.Replace("{#employeeDesignation#}", employeeDesignation);
            sb.Replace("{#placeOfVisit#}", placeOfVisit);
            sb.Replace("{#jobNo#}", jobNo);
            sb.Replace("{#statusName#}", statusName);

            sb.Replace("{#voucherNumber#}", voucherNumber);
            sb.Replace("{#voucherDate#}", voucherDate);

            //sb.Replace("{#invoiceNumber#}", invoiceNumber);
            //sb.Replace("{#invoiceDate#}", invoiceDate);

            sb.Replace("{#airTicketAmount#}", airTicketAmount);
            sb.Replace("{#hotelAmount#}", hotelAmount);
            sb.Replace("{#taxiAmount#}", taxiAmount);
            sb.Replace("{#othersAmount#}", othersAmount);
            sb.Replace("{#advanceAmount#}", advanceAmount);
            sb.Replace("{#totalAmount#}", totalAmount);
            sb.Replace("{#currencyCode#}", currencyCode);

            sb.Replace("{#createdBy#}", createdBy);
            sb.Replace("{#createdByEmailId#}", createdByEmailId);
            sb.Replace("{#createdOn#}", createdOn);
            sb.Replace("{#createdRemarks#}", createdRemarks);

            sb.Replace("{#closedBy#}", closedBy);
            sb.Replace("{#closedByEmailId#}", closedByEmailId);
            sb.Replace("{#closedOn#}", closedOn);
            sb.Replace("{#closedRemarks#}", closedRemarks);


            htmlText = sb.ToString();
            return htmlText;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}