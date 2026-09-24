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

/// <summary>
/// Summary description for LOTHtmlForPDF
/// </summary>
public class TravelHtmlForPDF
{
    string tourNo = string.Empty;
    string tourSanctionNo = string.Empty;
    string emplyoeeName = string.Empty;
    string employeeID = string.Empty;
    string designation = string.Empty;
    string startDateOfTour = string.Empty;
    string endDateOfTour = string.Empty;
    string nameOfCustomerVendor = string.Empty;
    string placeOfVisit = string.Empty;
    string purposeOfVisit = string.Empty;
    string jobEnquiryNo = string.Empty;
    string businessSegment = string.Empty;

    string airfare = string.Empty;
    string telephoneMobile = string.Empty;
    string lodging = string.Empty;
    string tips = string.Empty;
    string meals = string.Empty;
    string visaFee = string.Empty;
    string groundTransport = string.Empty;
    string dailyAllowance = string.Empty;
    string entertainment = string.Empty;
    string other = string.Empty;
    string gifts = string.Empty;
    string tourCostRecoverable = string.Empty;

    string advanceObtained = string.Empty;
    string totalSubmittedBillAmount = string.Empty;
    string disallowedAmount = string.Empty;
    string billPassedAmount = string.Empty;
    string setteledAmount = string.Empty;

    string advanceObtainedCurrency = string.Empty;
    string totalSubmittedBillAmountCurrency = string.Empty;
    string disallowedAmountCurrency = string.Empty;
    string billPassedAmountCurrency = string.Empty;
    string setteledAmountCurrency = string.Empty;

    string reasonforDisallowing = string.Empty;
    string settlementType = string.Empty;
    string voucherNo = string.Empty;
    string voucherDate = string.Empty;

    int createdByID = 0;
    string createdRemarks = string.Empty;
    string createdBy = string.Empty;
    string createdOn = string.Empty;

    int amendedByID = 0;
    string amendedRemarks = string.Empty;
    string amendedBy = string.Empty;
    string amendedOn = string.Empty;

    int cancelledByID = 0;
    string cancelledRemarks = string.Empty;
    string cancelledBy = string.Empty;
    string cancelledOn = string.Empty;

    int approvedByID = 0;
    string approvedRemarks = string.Empty;
    string approvedBy = string.Empty;
    string approvedOn = string.Empty;

    int amendedApprovedByID = 0;
    string amendedApprovedRemarks = string.Empty;
    string amendedApprovedBy = string.Empty;
    string amendedApprovedOn = string.Empty;

    int checkedByID = 0;
    string checkedRemarks = string.Empty;
    string checkedBy = string.Empty;
    string checkedOn = string.Empty;

    int accCheckedByID = 0;
    string accCheckedRemarks = string.Empty;
    string accCheckedBy = string.Empty;
    string accCheckedOn = string.Empty;

    int amendedCheckedByID = 0;
    string amendedCheckedRemarks = string.Empty;
    string amendedCheckedBy = string.Empty;
    string amendedCheckedOn = string.Empty;

    int amendedAccCheckedByID = 0;
    string amendedAccCheckedRemarks = string.Empty;
    string amendedAccCheckedBy = string.Empty;
    string amendedAccCheckedOn = string.Empty;

    int passedByID = 0;
    string passedRemarks = string.Empty;
    string passedBy = string.Empty;
    string passedOn = string.Empty;

    int amendedPassedByID = 0;
    string amendedPassedRemarks = string.Empty;
    string amendedPassedBy = string.Empty;
    string amendedPassedOn = string.Empty;
    int amendmentCount = 0;

    int sendToAmenmentByID = 0;
    string sendToAmenmentRemarks = string.Empty;
    string sendToAmendmentBy = string.Empty;
    string sendToAmendmentOn = string.Empty;

    int settledByID = 0;
    string settledRemarks = string.Empty;
    string settledBy = string.Empty;
    string settledOn = string.Empty;

    string invoiceNumber = string.Empty;
    string invoiceDate = string.Empty;
    string invoiceAmount = string.Empty;
    int invoiceBookedByID = 0;
    string invoiceBookedRemarks = string.Empty;
    string invoiceBookedBy = string.Empty;
    string invoiceBookedOn = string.Empty;



    public TravelHtmlForPDF()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public string GetHtmlForPDF(DataTable dtTravelDetail)
    {
        try
        {
            tourNo = string.Empty;
            tourSanctionNo = string.Empty;
            emplyoeeName = string.Empty;
            employeeID = string.Empty;
            designation = string.Empty;
            startDateOfTour = string.Empty;
            endDateOfTour = string.Empty;
            nameOfCustomerVendor = string.Empty;
            placeOfVisit = string.Empty;
            purposeOfVisit = string.Empty;
            jobEnquiryNo = string.Empty;
            businessSegment = string.Empty;

            airfare = string.Empty;
            telephoneMobile = string.Empty;
            lodging = string.Empty;
            tips = string.Empty;
            meals = string.Empty;
            visaFee = string.Empty;
            groundTransport = string.Empty;
            dailyAllowance = string.Empty;
            entertainment = string.Empty;
            other = string.Empty;
            gifts = string.Empty;
            tourCostRecoverable = string.Empty;

            advanceObtained = string.Empty;
            totalSubmittedBillAmount = string.Empty;
            disallowedAmount = string.Empty;
            billPassedAmount = string.Empty;
            setteledAmount = string.Empty;

            advanceObtainedCurrency = string.Empty;
            totalSubmittedBillAmountCurrency = string.Empty;
            disallowedAmountCurrency = string.Empty;
            billPassedAmountCurrency = string.Empty;
            setteledAmountCurrency = string.Empty;

            reasonforDisallowing = string.Empty;
            settlementType = string.Empty;
            voucherNo = string.Empty;
            voucherDate = string.Empty;

            createdByID = 0;
            createdRemarks = string.Empty;
            createdBy = string.Empty;
            createdOn = string.Empty;

            amendedByID = 0;
            amendedRemarks = string.Empty;
            amendedBy = string.Empty;
            amendedOn = string.Empty;

            cancelledByID = 0;
            cancelledRemarks = string.Empty;
            cancelledBy = string.Empty;
            cancelledOn = string.Empty;

            approvedByID = 0;
            approvedRemarks = string.Empty;
            approvedBy = string.Empty;
            approvedOn = string.Empty;

            amendedApprovedByID = 0;
            amendedApprovedRemarks = string.Empty;
            amendedApprovedBy = string.Empty;
            amendedApprovedOn = string.Empty;

            checkedByID = 0;
            checkedRemarks = string.Empty;
            checkedBy = string.Empty;
            checkedOn = string.Empty;

            accCheckedByID = 0;
            accCheckedRemarks = string.Empty;
            accCheckedBy = string.Empty;
            accCheckedOn = string.Empty;

            amendedCheckedByID = 0;
            amendedCheckedRemarks = string.Empty;
            amendedCheckedBy = string.Empty;
            amendedCheckedOn = string.Empty;

            amendedAccCheckedByID = 0;
            amendedAccCheckedRemarks = string.Empty;
            amendedAccCheckedBy = string.Empty;
            amendedAccCheckedOn = string.Empty;



            passedByID = 0;
            passedRemarks = string.Empty;
            passedBy = string.Empty;
            passedOn = string.Empty;

            amendedPassedByID = 0;
            amendedPassedRemarks = string.Empty;
            amendedPassedBy = string.Empty;
            amendedPassedOn = string.Empty;

            amendmentCount = 0;

            sendToAmenmentByID = 0;
            sendToAmenmentRemarks = string.Empty;
            sendToAmendmentBy = string.Empty;
            sendToAmendmentOn = string.Empty;

            settledByID = 0;
            settledRemarks = string.Empty;
            settledBy = string.Empty;
            settledOn = string.Empty;


            invoiceNumber = string.Empty;
            invoiceDate = string.Empty;
            invoiceAmount = string.Empty;

            invoiceBookedByID = 0;
            invoiceBookedRemarks = string.Empty;
            invoiceBookedBy = string.Empty;
            invoiceBookedOn = string.Empty;


            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (dtTravelDetail.Rows.Count > 0)
            {
                DataRow dr = dtTravelDetail.Rows[0];

                if (dr["TOUR_SANCTION_NO"] != DBNull.Value)
                    tourSanctionNo = Convert.ToString(dr["TOUR_SANCTION_NO"]);

                if (dr["TOUR_NO"] != DBNull.Value)
                    tourNo = Convert.ToString(dr["TOUR_NO"]);

                if (dr["EMPLOYEE_NAME"] != DBNull.Value)
                    emplyoeeName = Convert.ToString(dr["EMPLOYEE_NAME"]);

                if (dr["EMPLOYEE_ID"] != DBNull.Value)
                    employeeID = Convert.ToString(dr["EMPLOYEE_ID"]);

                if (dr["DESIGNATION"] != DBNull.Value)
                    designation = Convert.ToString(dr["DESIGNATION"]);

                if (dr["START_DATE"] != DBNull.Value)
                    startDateOfTour = Convert.ToString(dr["START_DATE"]);

                if (dr["END_DATE"] != DBNull.Value)
                    endDateOfTour = Convert.ToString(dr["END_DATE"]);

                if (dr["CUST_VEND_NAME"] != DBNull.Value)
                    nameOfCustomerVendor = Convert.ToString(dr["CUST_VEND_NAME"]);

                if (dr["PLACE_OF_VISIT"] != DBNull.Value)
                    placeOfVisit = Convert.ToString(dr["PLACE_OF_VISIT"]);

                if (dr["VISIT_TYPE"] != DBNull.Value)
                    purposeOfVisit = Convert.ToString(dr["VISIT_TYPE"]);

                if (dr["JOB_NO"] != DBNull.Value)
                    jobEnquiryNo = Convert.ToString(dr["JOB_NO"]);

                if (dr["BUS_SEGMENT"] != DBNull.Value)
                    businessSegment = Convert.ToString(dr["BUS_SEGMENT"]);


                if (dr["AIRFARE_AMT"] != DBNull.Value)
                    airfare = Convert.ToString(dr["AIRFARE_AMT"]);

                if (dr["TELEPHONE_MOBILE_AMT"] != DBNull.Value)
                    telephoneMobile = Convert.ToString(dr["TELEPHONE_MOBILE_AMT"]);

                if (dr["LODGING_AMT"] != DBNull.Value)
                    lodging = Convert.ToString(dr["LODGING_AMT"]);

                if (dr["TIPS_AMT"] != DBNull.Value)
                    tips = Convert.ToString(dr["TIPS_AMT"]);

                if (dr["MEALS_AMT"] != DBNull.Value)
                    meals = Convert.ToString(dr["MEALS_AMT"]);

                if (dr["VISAFEE_AMT"] != DBNull.Value)
                    visaFee = Convert.ToString(dr["VISAFEE_AMT"]);

                if (dr["GROUND_TRANSPORT_AMT"] != DBNull.Value)
                    groundTransport = Convert.ToString(dr["GROUND_TRANSPORT_AMT"]);

                if (dr["DAILY_ALLOWANCE_AMT"] != DBNull.Value)
                    dailyAllowance = Convert.ToString(dr["DAILY_ALLOWANCE_AMT"]);

                if (dr["ENTERTAINMENT_AMT"] != DBNull.Value)
                    entertainment = Convert.ToString(dr["ENTERTAINMENT_AMT"]);

                if (dr["OTHER_AMT"] != DBNull.Value)
                    other = Convert.ToString(dr["OTHER_AMT"]);

                if (dr["GIFTS_AMT"] != DBNull.Value)
                    gifts = Convert.ToString(dr["GIFTS_AMT"]);

                if (dr["IS_COST_RECOVERABLE"] != DBNull.Value)
                    tourCostRecoverable = Convert.ToString(dr["IS_COST_RECOVERABLE"]);

                if (dr["ADVANCE_AMT"] != DBNull.Value)
                    advanceObtained = Convert.ToString(dr["ADVANCE_AMT"]);

                if (dr["TOTAL_AMT"] != DBNull.Value)
                    totalSubmittedBillAmount = Convert.ToString(dr["TOTAL_AMT"]);

                if (dr["DISALLOWED_AMT"] != DBNull.Value)
                    disallowedAmount = Convert.ToString(dr["DISALLOWED_AMT"]);

                if (dr["BILL_PASSED_AMT"] != DBNull.Value)
                    billPassedAmount = Convert.ToString(dr["BILL_PASSED_AMT"]);

                if (dr["FINAL_AMT"] != DBNull.Value)
                    setteledAmount = Convert.ToString(dr["FINAL_AMT"]);

                if (dr["ADV_CURRENCY"] != DBNull.Value)
                    advanceObtainedCurrency = Convert.ToString(dr["ADV_CURRENCY"]);

                if (dr["TOTAL_CURRENCY"] != DBNull.Value)
                    totalSubmittedBillAmountCurrency = Convert.ToString(dr["TOTAL_CURRENCY"]);

                if (dr["DISALLOWED_AMT_CURRENCY"] != DBNull.Value)
                    disallowedAmountCurrency = Convert.ToString(dr["DISALLOWED_AMT_CURRENCY"]);

                if (dr["BILL_PASSED_AMT_CURRENCY"] != DBNull.Value)
                    billPassedAmountCurrency = Convert.ToString(dr["BILL_PASSED_AMT_CURRENCY"]);

                if (dr["FINAL_AMT_CURRENCY"] != DBNull.Value)
                    setteledAmountCurrency = Convert.ToString(dr["FINAL_AMT_CURRENCY"]);

                if (dr["REASON_FOR_DISALLOWING"] != DBNull.Value)
                    reasonforDisallowing = Convert.ToString(dr["REASON_FOR_DISALLOWING"]);

                if (dr["SETTLEMENT_TYPE"] != DBNull.Value)
                    settlementType = Convert.ToString(dr["SETTLEMENT_TYPE"]);

                if (dr["DN_NO"] != DBNull.Value)
                    voucherNo = Convert.ToString(dr["DN_NO"]);

                if (dr["AMOUNT_DATED"] != DBNull.Value)
                    voucherDate = Convert.ToString(dr["AMOUNT_DATED"]);

                if (dr["CREATED_BY_ID"] != DBNull.Value)
                    createdByID = Convert.ToInt32(dr["CREATED_BY_ID"]);

                if (dr["CREATED_REMARKS"] != DBNull.Value)
                    createdRemarks = Convert.ToString(dr["CREATED_REMARKS"]);

                if (dr["CREATED_BY"] != DBNull.Value)
                    createdBy = Convert.ToString(dr["CREATED_BY"]);

                if (dr["CREATED_ON"] != DBNull.Value)
                    createdOn = Convert.ToString(dr["CREATED_ON"]);

                if (dr["AMENDED_BY_ID"] != DBNull.Value)
                    amendedByID = Convert.ToInt32(dr["AMENDED_BY_ID"]);

                if (dr["AMENDED_REMARKS"] != DBNull.Value)
                    amendedRemarks = Convert.ToString(dr["AMENDED_REMARKS"]);

                if (dr["AMENDED_BY"] != DBNull.Value)
                    amendedBy = Convert.ToString(dr["AMENDED_BY"]);

                if (dr["AMENDED_ON"] != DBNull.Value)
                    amendedOn = Convert.ToString(dr["AMENDED_ON"]);

                if (dr["CANCELLED_BY_ID"] != DBNull.Value)
                    cancelledByID = Convert.ToInt32(dr["CANCELLED_BY_ID"]);

                if (dr["CANCELLED_REMARKS"] != DBNull.Value)
                    cancelledRemarks = Convert.ToString(dr["CANCELLED_REMARKS"]);

                if (dr["CANCELLED_BY"] != DBNull.Value)
                    cancelledBy = Convert.ToString(dr["CANCELLED_BY"]);

                if (dr["CANCELLED_ON"] != DBNull.Value)
                    cancelledOn = Convert.ToString(dr["CANCELLED_ON"]);

                if (dr["APPROVED_BY_ID"] != DBNull.Value)
                    approvedByID = Convert.ToInt32(dr["APPROVED_BY_ID"]);

                if (dr["APPROVED_REMARKS"] != DBNull.Value)
                    approvedRemarks = Convert.ToString(dr["APPROVED_REMARKS"]);

                if (dr["APPROVED_BY"] != DBNull.Value)
                    approvedBy = Convert.ToString(dr["APPROVED_BY"]);

                if (dr["APPROVED_ON"] != DBNull.Value)
                    approvedOn = Convert.ToString(dr["APPROVED_ON"]);

                if (dr["AMENDED_APPROVED_BY_ID"] != DBNull.Value)
                    amendedApprovedByID = Convert.ToInt32(dr["AMENDED_APPROVED_BY_ID"]);

                if (dr["AMENDED_APPROVED_REMARKS"] != DBNull.Value)
                    amendedApprovedRemarks = Convert.ToString(dr["AMENDED_APPROVED_REMARKS"]);

                if (dr["AMENDED_APPROVED_BY"] != DBNull.Value)
                    amendedApprovedBy = Convert.ToString(dr["AMENDED_APPROVED_BY"]);

                if (dr["AMENDED_APPROVED_ON"] != DBNull.Value)
                    amendedApprovedOn = Convert.ToString(dr["AMENDED_APPROVED_ON"]);



                if (dr["CHECKED_BY_ID"] != DBNull.Value)
                    checkedByID = Convert.ToInt32(dr["CHECKED_BY_ID"]);

                if (dr["CHECKED_REMARKS"] != DBNull.Value)
                    checkedRemarks = Convert.ToString(dr["CHECKED_REMARKS"]);

                if (dr["CHECKED_BY"] != DBNull.Value)
                    checkedBy = Convert.ToString(dr["CHECKED_BY"]);

                if (dr["CHECKED_ON"] != DBNull.Value)
                    checkedOn = Convert.ToString(dr["CHECKED_ON"]);




                if (dr["ACC_CHECKED_BY_ID"] != DBNull.Value)
                    accCheckedByID = Convert.ToInt32(dr["ACC_CHECKED_BY_ID"]);

                if (dr["ACC_CHECKED_REMARKS"] != DBNull.Value)
                    accCheckedRemarks = Convert.ToString(dr["ACC_CHECKED_REMARKS"]);

                if (dr["ACC_CHECKED_BY"] != DBNull.Value)
                    accCheckedBy = Convert.ToString(dr["ACC_CHECKED_BY"]);

                if (dr["ACC_CHECKED_ON"] != DBNull.Value)
                    accCheckedOn = Convert.ToString(dr["ACC_CHECKED_ON"]);





                if (dr["AMENDED_CHECKED_BY_ID"] != DBNull.Value)
                    amendedCheckedByID = Convert.ToInt32(dr["AMENDED_CHECKED_BY_ID"]);

                if (dr["AMENDED_CHECKED_REMARKS"] != DBNull.Value)
                    amendedCheckedRemarks = Convert.ToString(dr["AMENDED_CHECKED_REMARKS"]);

                if (dr["AMENDED_CHECKED_BY"] != DBNull.Value)
                    amendedCheckedBy = Convert.ToString(dr["AMENDED_CHECKED_BY"]);

                if (dr["AMENDED_CHECKED_ON"] != DBNull.Value)
                    amendedCheckedOn = Convert.ToString(dr["AMENDED_CHECKED_ON"]);



                if (dr["AMENDED_ACC_CHECKED_BY_ID"] != DBNull.Value)
                    amendedAccCheckedByID = Convert.ToInt32(dr["AMENDED_ACC_CHECKED_BY_ID"]);

                if (dr["AMENDED_ACC_CHECKED_REMARKS"] != DBNull.Value)
                    amendedAccCheckedRemarks = Convert.ToString(dr["AMENDED_ACC_CHECKED_REMARKS"]);

                if (dr["AMENDED_ACC_CHECKED_BY"] != DBNull.Value)
                    amendedAccCheckedBy = Convert.ToString(dr["AMENDED_ACC_CHECKED_BY"]);

                if (dr["AMENDED_ACC_CHECKED_ON"] != DBNull.Value)
                    amendedAccCheckedOn = Convert.ToString(dr["AMENDED_ACC_CHECKED_ON"]);




                if (dr["PASSED_BY_ID"] != DBNull.Value)
                    passedByID = Convert.ToInt32(dr["PASSED_BY_ID"]);

                if (dr["PASSED_REMARKS"] != DBNull.Value)
                    passedRemarks = Convert.ToString(dr["PASSED_REMARKS"]);

                if (dr["PASSED_BY"] != DBNull.Value)
                    passedBy = Convert.ToString(dr["PASSED_BY"]);

                if (dr["PASSED_ON"] != DBNull.Value)
                    passedOn = Convert.ToString(dr["PASSED_ON"]);

                if (dr["AMENDED_PASSED_BY_ID"] != DBNull.Value)
                    amendedPassedByID = Convert.ToInt32(dr["AMENDED_PASSED_BY_ID"]);

                if (dr["AMENDED_PASSED_REMARKS"] != DBNull.Value)
                    amendedPassedRemarks = Convert.ToString(dr["AMENDED_PASSED_REMARKS"]);

                if (dr["AMENDED_PASSED_BY"] != DBNull.Value)
                    amendedPassedBy = Convert.ToString(dr["AMENDED_PASSED_BY"]);

                if (dr["AMENDED_PASSED_ON"] != DBNull.Value)
                    amendedPassedOn = Convert.ToString(dr["AMENDED_PASSED_ON"]);

                if (dr["AMENDMENT_COUNT"] != DBNull.Value)
                    amendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);

                if (dr["AMENDMENT_BY_ID"] != DBNull.Value)
                    sendToAmenmentByID = Convert.ToInt32(dr["AMENDMENT_BY_ID"]);

                if (dr["AMENDMENT_REMARKS"] != DBNull.Value)
                    sendToAmenmentRemarks = Convert.ToString(dr["AMENDMENT_REMARKS"]);

                if (dr["AMENDMENT_BY"] != DBNull.Value)
                    sendToAmendmentBy = Convert.ToString(dr["AMENDMENT_BY"]);

                if (dr["AMENDMENT_ON"] != DBNull.Value)
                    sendToAmendmentOn = Convert.ToString(dr["AMENDMENT_ON"]);

                if (dr["SETTLED_BY_ID"] != DBNull.Value)
                    settledByID = Convert.ToInt32(dr["SETTLED_BY_ID"]);

                if (dr["SETTLED_REMARKS"] != DBNull.Value)
                    settledRemarks = Convert.ToString(dr["SETTLED_REMARKS"]);

                if (dr["SETTLED_BY"] != DBNull.Value)
                    settledBy = Convert.ToString(dr["SETTLED_BY"]);

                if (dr["SETTLED_ON"] != DBNull.Value)
                    settledOn = Convert.ToString(dr["SETTLED_ON"]);




                if (dr["INVOICE_NUMBER"] != DBNull.Value)
                    invoiceNumber = Convert.ToString(dr["INVOICE_NUMBER"]);

                if (dr["INVOICE_DATE"] != DBNull.Value)
                    invoiceDate = Convert.ToString(dr["INVOICE_DATE"]);

                if (dr["INVOICE_AMOUNT"] != DBNull.Value)
                    invoiceAmount = Convert.ToString(dr["INVOICE_AMOUNT"]);



                if (dr["INVOICE_BOOKED_BY_ID"] != DBNull.Value)
                    invoiceBookedByID = Convert.ToInt32(dr["INVOICE_BOOKED_BY_ID"]);

                if (dr["INVOICE_BOOKED_REMARKS"] != DBNull.Value)
                    invoiceBookedRemarks = Convert.ToString(dr["INVOICE_BOOKED_REMARKS"]);

                if (dr["INVOICE_BOOKED_BY"] != DBNull.Value)
                    invoiceBookedBy = Convert.ToString(dr["INVOICE_BOOKED_BY"]);

                if (dr["INVOICE_BOOKED_ON"] != DBNull.Value)
                    invoiceBookedOn = Convert.ToString(dr["INVOICE_BOOKED_ON"]);



                sb.Append("<h2 class='headerStyle'>Travel Statement</h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Tour No.:</td>\n");
                sb.Append("<td class='td2header'>{#tourNo#}</td>\n");
                sb.Append("<td class='td1header'>Tour Sanction No.:</td>\n");
                sb.Append("<td class='td2header'>{#tourSanctionNo#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Emplyoee Name:</td>\n");
                sb.Append("<td class='td2header'>{#emplyoeeName#}</td>\n");
                sb.Append("<td class='td1header'>Employee ID:</td>\n");
                sb.Append("<td class='td2header'>{#employeeID#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Designation:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#designation#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Start Date Of Tour:</td>\n");
                sb.Append("<td class='td2header'>{#startDateOfTour#}</td>\n");
                sb.Append("<td class='td1header'>End Date Of Tour:</td>\n");
                sb.Append("<td class='td2header'>{#endDateOfTour#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Name Of Customer/Vendor:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#nameOfCustomerVendor#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Place Of Visit:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#placeOfVisit#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Purpose Of Visit:</td>\n");
                sb.Append("<td class='td2header'>{#purposeOfVisit#}</td>\n");
                sb.Append("<td class='td1header'>Job/Enquiry No.:</td>\n");
                sb.Append("<td class='td2header'>{#jobEnquiryNo#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Business Segment:</td>\n");
                sb.Append("<td class='td2header'>{#businessSegment#}</td>\n");

                sb.Append("<td class='td1header'>Tour Cost Recoverable:</td>\n");
                sb.Append("<td class='td2header'>{#tourCostRecoverable#}</td>\n");

                sb.Append("</tr>\n");


                if (invoiceBookedByID > 0)
                {
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Invoice Number:</td>\n");
                    sb.Append("<td class='td2header'>{#invoiceNumber#}</td>\n");

                    sb.Append("<td class='td1header'>Invoice Date:</td>\n");
                    sb.Append("<td class='td2header'>{#invoiceDate#}</td>\n");

                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Invoice Amount:</td>\n");
                    sb.Append("<td class='td2header'>{#invoiceAmount#}</td>\n");

                    sb.Append("</tr>\n");
                }



                sb.Append("</table>\n");

                sb.Append("<hr />\n");
                sb.Append("<h3 class='header2'>Expenses</h3>\n");
                sb.Append("<hr />\n");


                sb.Append("<table class='tblsuitems'>\n");
                sb.Append("<tr class='trsubitems'>\n");
                sb.Append("<th class='td1subitems'>Sr.No.</th>\n");
                sb.Append("<th class='td2subitems'>Description</th>\n");
                sb.Append("<th class='td3subitems'>Amount</th>\n");
                //sb.Append("<th class='td4subitems'>Currency</th>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>1</td>\n");
                sb.Append("<td class='td2subitems'>Airfare</td>\n");
                sb.Append("<td class='td3subitems'>{#airfare#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#advanceObtainedCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>2</td>\n");
                sb.Append("<td class='td2subitems'>Telephone/Mobile</td>\n");
                sb.Append("<td class='td3subitems'>{#telephoneMobile#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>3</td>\n");
                sb.Append("<td class='td2subitems'>Lodging</td>\n");
                sb.Append("<td class='td3subitems'>{#lodging#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>4</td>\n");
                sb.Append("<td class='td2subitems'>Tips</td>\n");
                sb.Append("<td class='td3subitems'>{#tips#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>5</td>\n");
                sb.Append("<td class='td2subitems'>Meals</td>\n");
                sb.Append("<td class='td3subitems'>{#meals#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>6</td>\n");
                sb.Append("<td class='td2subitems'>Visa Fee</td>\n");
                sb.Append("<td class='td3subitems'>{#visaFee#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>7</td>\n");
                sb.Append("<td class='td2subitems'>Ground Transport</td>\n");
                sb.Append("<td class='td3subitems'>{#groundTransport#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>8</td>\n");
                sb.Append("<td class='td2subitems'>Daily Allowance</td>\n");
                sb.Append("<td class='td3subitems'>{#dailyAllowance#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>9</td>\n");
                sb.Append("<td class='td2subitems'>Entertainment</td>\n");
                sb.Append("<td class='td3subitems'>{#entertainment#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");


                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>10</td>\n");
                sb.Append("<td class='td2subitems'>Other</td>\n");
                sb.Append("<td class='td3subitems'>{#other#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");


                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>11</td>\n");
                sb.Append("<td class='td2subitems'>Gifts</td>\n");
                sb.Append("<td class='td3subitems'>{#gifts#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>&nbsp;</td>\n");
                sb.Append("<td class='td2subitems'>Total</td>\n");
                sb.Append("<td class='td3subitems'>{#totalSubmittedBillAmount#}</td>\n");
                //sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("</table>\n");

                //sb.Append("<table class='tblheader'>\n");
                //sb.Append("<tr>\n");
                //sb.Append("<td class='td1header'>Airfare:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#airfare#}</td>\n");
                //sb.Append("<td class='td1header'>Telephone/Mobile:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#telephoneMobile#}</td>\n");
                //sb.Append("</tr>\n");
                //sb.Append("<tr>\n");
                //sb.Append("<td class='td1header'>Lodging:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#lodging#}</td>\n");
                //sb.Append("<td class='td1header'>Tips:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#tips#}</td>\n");
                //sb.Append("</tr>\n");
                //sb.Append("<tr>\n");
                //sb.Append("<td class='td1header'>Meals:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#meals#}</td>\n");
                //sb.Append("<td class='td1header'>Visa Fee:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#visaFee#}</td>\n");
                //sb.Append("</tr>\n");
                //sb.Append("<tr>\n");
                //sb.Append("<td class='td1header'>Ground Transport:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#groundTransport#}</td>\n");
                //sb.Append("<td class='td1header'>Daily Allowance:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#dailyAllowance#}</td>\n");
                //sb.Append("</tr>\n");
                //sb.Append("<tr>\n");
                //sb.Append("<td class='td1header'>Entertainment:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#entertainment#}</td>\n");
                //sb.Append("<td class='td1header'>Other:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#other#}</td>\n");
                //sb.Append("</tr>\n");
                //sb.Append("<tr>\n");
                //sb.Append("<td class='td1header'>Gifts:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#gifts#}</td>\n");
                //sb.Append("<td class='td1header'>Tour Cost Recoverable:</td>\n");
                //sb.Append("<td class='td2header' align='right'>{#tourCostRecoverable#}</td>\n");
                //sb.Append("</tr>\n");
                //sb.Append("</table>\n");


                sb.Append("<hr />\n");
                sb.Append("<h3 class='header2'>Expense Statement</h3>\n");
                sb.Append("<hr />\n");



                sb.Append("<table class='tblsuitems'>\n");
                sb.Append("<tr class='trsubitems'>\n");
                sb.Append("<th class='td1subitems'>Sr.No.</th>\n");
                sb.Append("<th class='td2subitems'>Description</th>\n");
                sb.Append("<th class='td3subitems'>Amount</th>\n");
                sb.Append("<th class='td4subitems'>Currency</th>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>1</td>\n");
                sb.Append("<td class='td2subitems'>Advance Obtained</td>\n");
                sb.Append("<td class='td3subitems'>{#advanceObtained#}</td>\n");
                sb.Append("<td class='td4subitems'>{#advanceObtainedCurrency#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1subitems'>2</td>\n");
                sb.Append("<td class='td2subitems'>Total Submitted Bill Amount</td>\n");
                sb.Append("<td class='td3subitems'>{#totalSubmittedBillAmount#}</td>\n");
                sb.Append("<td class='td4subitems'>{#totalSubmittedBillAmountCurrency#}</td>\n");
                sb.Append("</tr>\n");

                if (settledByID > 0)
                {
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1subitems'>3</td>\n");
                    sb.Append("<td class='td2subitems'>Disallowed Amount</td>\n");
                    sb.Append("<td class='td3subitems'>{#disallowedAmount#}</td>\n");
                    sb.Append("<td class='td4subitems'>{#disallowedAmountCurrency#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1subitems'>4</td>\n");
                    sb.Append("<td class='td2subitems'>Bill Passed Amount</td>\n");
                    sb.Append("<td class='td3subitems'>{#billPassedAmount#}</td>\n");
                    sb.Append("<td class='td4subitems'>{#billPassedAmountCurrency#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1subitems'>5</td>\n");
                    sb.Append("<td class='td2subitems'>Setteled Amount</td>\n");
                    sb.Append("<td class='td3subitems'>{#setteledAmount#}</td>\n");
                    sb.Append("<td class='td4subitems'>{#setteledAmountCurrency#}</td>\n");
                    sb.Append("</tr>\n");
                }

                sb.Append("</table>\n");

                if (settledByID > 0)
                {
                    sb.Append("<table class='tblheader'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Reason for Disallowing:</td>\n");
                    sb.Append("<td class='td2header' colspan='3'>{#reasonforDisallowing#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Settlement Type:</td>\n");
                    sb.Append("<td class='td2header'>{#settlementType#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Voucher No.:</td>\n");
                    sb.Append("<td class='td2header'>{#voucherNo#}</td>\n");
                    sb.Append("<td class='td1header'>Voucher Date:</td>\n");
                    sb.Append("<td class='td2header'>{#voucherDate#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                }

                sb.Append("<hr />\n");

                sb.Replace("{#tourNo#}", tourNo);
                sb.Replace("{#tourSanctionNo#}", tourSanctionNo);
                sb.Replace("{#emplyoeeName#}", emplyoeeName);
                sb.Replace("{#employeeID#}", employeeID);
                sb.Replace("{#designation#}", designation);
                sb.Replace("{#startDateOfTour#}", startDateOfTour);
                sb.Replace("{#endDateOfTour#}", endDateOfTour);
                sb.Replace("{#nameOfCustomerVendor#}", nameOfCustomerVendor);
                sb.Replace("{#placeOfVisit#}", placeOfVisit);
                sb.Replace("{#purposeOfVisit#}", purposeOfVisit);
                sb.Replace("{#jobEnquiryNo#}", jobEnquiryNo);
                sb.Replace("{#businessSegment#}", businessSegment);


                sb.Replace("{#invoiceNumber#}", invoiceNumber);
                sb.Replace("{#invoiceDate#}", invoiceDate);
                sb.Replace("{#invoiceAmount#}", invoiceAmount);

                sb.Replace("{#airfare#}", airfare);
                sb.Replace("{#telephoneMobile#}", telephoneMobile);
                sb.Replace("{#lodging#}", lodging);
                sb.Replace("{#tips#}", tips);
                sb.Replace("{#meals#}", meals);
                sb.Replace("{#visaFee#}", visaFee);
                sb.Replace("{#groundTransport#}", groundTransport);
                sb.Replace("{#dailyAllowance#}", dailyAllowance);
                sb.Replace("{#entertainment#}", entertainment);
                sb.Replace("{#other#}", other);
                sb.Replace("{#gifts#}", gifts);
                sb.Replace("{#tourCostRecoverable#}", tourCostRecoverable);

                sb.Replace("{#advanceObtained#}", advanceObtained);
                sb.Replace("{#totalSubmittedBillAmount#}", totalSubmittedBillAmount);
                sb.Replace("{#disallowedAmount#}", disallowedAmount);
                sb.Replace("{#billPassedAmount#}", billPassedAmount);
                sb.Replace("{#setteledAmount#}", setteledAmount);

                sb.Replace("{#advanceObtainedCurrency#}", advanceObtainedCurrency);
                sb.Replace("{#totalSubmittedBillAmountCurrency#}", totalSubmittedBillAmountCurrency);
                sb.Replace("{#disallowedAmountCurrency#}", disallowedAmountCurrency);
                sb.Replace("{#billPassedAmountCurrency#}", billPassedAmountCurrency);
                sb.Replace("{#setteledAmountCurrency#}", setteledAmountCurrency);


                sb.Replace("{#reasonforDisallowing#}", reasonforDisallowing);
                sb.Replace("{#settlementType#}", settlementType);
                sb.Replace("{#voucherNo#}", voucherNo);
                sb.Replace("{#voucherDate#}", voucherDate);




                if (createdByID > 0 || amendedByID > 0)
                {
                    sb.Append("<h3 class='header2'>Signatories</h3>\n");
                    sb.Append("<hr />\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Created/Amended</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Created Remarks:</td>\n");
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

                    if (amendedByID > 0)
                    {
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Amended Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#amendedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amended By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amended On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#createdRemarks#}", createdRemarks);
                    sb.Replace("{#createdBy#}", createdBy);
                    sb.Replace("{#createdOn#}", createdOn);
                    sb.Replace("{#amendedRemarks#}", amendedRemarks);
                    sb.Replace("{#amendedBy#}", amendedBy);
                    sb.Replace("{#amendedOn#}", amendedOn);
                }

                if (cancelledByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Cancelled</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Cancelled Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#cancelledRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Cancelled By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#cancelledBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Cancelled On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#cancelledOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#cancelledRemarks#}", cancelledRemarks);
                    sb.Replace("{#cancelledBy#}", cancelledBy);
                    sb.Replace("{#cancelledOn#}", cancelledOn);
                }

                if (approvedByID > 0 || amendedApprovedByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Approved/Amended Approved</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Approved Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#approvedRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Approved By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#approvedBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Approved On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#approvedOn#}</td>\n");
                    sb.Append("</tr>\n");

                    if (amendedApprovedByID > 0)
                    {
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Amended Approved Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#amendedApprovedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Approved By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedApprovedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Approved On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedApprovedOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#approvedRemarks#}", approvedRemarks);
                    sb.Replace("{#approvedBy#}", approvedBy);
                    sb.Replace("{#approvedOn#}", approvedOn);
                    sb.Replace("{#amendedApprovedRemarks#}", amendedApprovedRemarks);
                    sb.Replace("{#amendedApprovedBy#}", amendedApprovedBy);
                    sb.Replace("{#amendedApprovedOn#}", amendedApprovedOn);

                }

                if (checkedByID > 0 || amendedCheckedByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Checked/Amended Checked (HR)</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Checked Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#checkedRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Checked By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#checkedBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Checked On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#checkedOn#}</td>\n");
                    sb.Append("</tr>\n");

                    if (amendedCheckedByID > 0)
                    {
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Amended Checked Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#amendedCheckedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Checked By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedCheckedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Checked On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedCheckedOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#checkedRemarks#}", checkedRemarks);
                    sb.Replace("{#checkedBy#}", checkedBy);
                    sb.Replace("{#checkedOn#}", checkedOn);
                    sb.Replace("{#amendedCheckedRemarks#}", amendedCheckedRemarks);
                    sb.Replace("{#amendedCheckedBy#}", amendedCheckedBy);
                    sb.Replace("{#amendedCheckedOn#}", amendedCheckedOn);
                }




                if (checkedByID > 0 || amendedCheckedByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Checked/Amended Checked (Accounts)</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Checked Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#accCheckedRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Checked By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#accCheckedBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Checked On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#accCheckedOn#}</td>\n");
                    sb.Append("</tr>\n");

                    if (amendedCheckedByID > 0)
                    {
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Amended Checked Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#amendedAccCheckedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Checked By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedAccCheckedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Checked On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedAccCheckedOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#accCheckedRemarks#}", accCheckedRemarks);
                    sb.Replace("{#accCheckedBy#}", accCheckedBy);
                    sb.Replace("{#accCheckedOn#}", accCheckedOn);
                    sb.Replace("{#amendedAccCheckedRemarks#}", amendedAccCheckedRemarks);
                    sb.Replace("{#amendedAccCheckedBy#}", amendedAccCheckedBy);
                    sb.Replace("{#amendedAccCheckedOn#}", amendedAccCheckedOn);
                }














                if (passedByID > 0 || amendedPassedByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Passed/Amended Passed</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Passed Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#passedRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Passed By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#passedBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Passed On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#passedOn#}</td>\n");
                    sb.Append("</tr>\n");

                    if (amendedPassedByID > 0)
                    {
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Amended Passed Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#amendedPassedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Passed By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedPassedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Passed On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedPassedOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }

                    if (amendmentCount > 0)
                    {
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amendment Count:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendmentCount#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Send To Amenment Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#sendToAmenmentRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Send To Amendment By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#sendToAmendmentBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Send To Amendment On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#sendToAmendmentOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#passedRemarks#}", passedRemarks);
                    sb.Replace("{#passedBy#}", passedBy);
                    sb.Replace("{#passedOn#}", passedOn);
                    sb.Replace("{#amendedPassedRemarks#}", amendedPassedRemarks);
                    sb.Replace("{#amendedPassedBy#}", amendedPassedBy);
                    sb.Replace("{#amendedPassedOn#}", amendedPassedOn);
                    sb.Replace("{#amendmentCount#}", Convert.ToString(amendmentCount));
                    sb.Replace("{#sendToAmenmentRemarks#}", sendToAmenmentRemarks);
                    sb.Replace("{#sendToAmendmentBy#}", sendToAmendmentBy);
                    sb.Replace("{#sendToAmendmentOn#}", sendToAmendmentOn);
                }

                if (settledByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Settled</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Settled Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#settledRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Settled By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#settledBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Settled On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#settledOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr />\n");

                    sb.Replace("{#settledRemarks#}", settledRemarks);
                    sb.Replace("{#settledBy#}", settledBy);
                    sb.Replace("{#settledOn#}", settledOn);
                }

                if (invoiceBookedByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Invoice Booked</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Invoice Booked Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#invoiceBookedRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Invoice Booked By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#invoiceBookedBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Invoice Booked On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#invoiceBookedOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr />\n");

                    sb.Replace("{#invoiceBookedRemarks#}", invoiceBookedRemarks);
                    sb.Replace("{#invoiceBookedBy#}", invoiceBookedBy);
                    sb.Replace("{#invoiceBookedOn#}", invoiceBookedOn);
                }
            }

            htmlText = sb.ToString();
            return htmlText;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}