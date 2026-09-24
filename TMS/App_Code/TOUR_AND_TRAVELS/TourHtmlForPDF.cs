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
public class TourHtmlForPDF
{
    string tourNo = string.Empty;
    string reqNumber = string.Empty;
    string tourSanctionNo = string.Empty;
    string emplyoeeName = string.Empty;
    string employeeID = string.Empty;
    string designation = string.Empty;
    string startDateOfTour = string.Empty;
    string endDateOfTour = string.Empty;
    string nameOfCustomerVendor = string.Empty;
    string placeOfVisit = string.Empty;
    /// 
    string countryOfVisit = string.Empty;
    string tourBasedOn = string.Empty;
    string tourIniative = string.Empty;
    /// 

    string purposeOfVisit = string.Empty;
    string jobEnquiryNo = string.Empty;
    string businessSegment = string.Empty;
    string modeOfTravel = string.Empty;
    string typeOfTrip = string.Empty;
    string inCaseOfLocalTravelling = string.Empty;
    string expectedExpenditureOfTheTrip = string.Empty;
    string expectedExpenditureOfTheTripCurrency = string.Empty;
    string advanceRequired = string.Empty;
    string advanceRequiredCurrency = string.Empty;

    int createdByID = 0;
    string createdRemarks = string.Empty;
    string createdBy = string.Empty;
    string createdOn = string.Empty;

    int hodApprovedByID = 0;
    string hodApprovedRemarks = string.Empty;
    string hodApprovedBy = string.Empty;
    string hodApprovedOn = string.Empty;

    int mgmtHodApprovedByID = 0;
    string mgmtHodApprovedRemarks = string.Empty;
    string mgmtHodApprovedBy = string.Empty;
    string mgmtHodApprovedOn = string.Empty;

    ///
    /// 
    int finalApprovedByID = 0;
    string finalApprovedRemarks = string.Empty;
    string finalApprovedBy = string.Empty;
    string finalApprovedOn = string.Empty;
    /// </summary>
    int deletedByID = 0;
    string deletedRemarks = string.Empty;
    string deletedBy = string.Empty;
    string deletedOn = string.Empty;

    int cancelledByID = 0;
    string cancelledRemarks = string.Empty;
    string cancelledBy = string.Empty;
    string cancelledOn = string.Empty;


         string  AdvanceTaken = string.Empty;
         string  AdvanceTakenCurrency = string.Empty;
         string  AdditionalAdvanceRequired = string.Empty;
         string  AdditionalAdvanceRequiredCurrency = string.Empty;

     string requestId = string.Empty;

    string advanceTaken = string.Empty;
    string  advanceTakenCurrency = string.Empty;

    string AddAdvanceRequiredCurrency = string.Empty;
    string  AddAdvanceRequired = string.Empty;
    int teamleaderID = 0;


    public TourHtmlForPDF()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public string GetHtmlForPDF(DataTable dtTourDetail)
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
            ///
            countryOfVisit = string.Empty;
            tourBasedOn = string.Empty;
            tourIniative = string.Empty;
            /////
            purposeOfVisit = string.Empty;
            jobEnquiryNo = string.Empty;
            businessSegment = string.Empty;
            modeOfTravel = string.Empty;
            typeOfTrip = string.Empty;
            inCaseOfLocalTravelling = string.Empty;
            expectedExpenditureOfTheTrip = string.Empty;
            expectedExpenditureOfTheTripCurrency = string.Empty;
            advanceRequired = string.Empty;
            advanceRequiredCurrency = string.Empty;

            createdByID = 0;
            createdRemarks = string.Empty;
            createdBy = string.Empty;
            createdOn = string.Empty;

            hodApprovedByID = 0;
            hodApprovedRemarks = string.Empty;
            hodApprovedBy = string.Empty;
            hodApprovedOn = string.Empty;

            mgmtHodApprovedByID = 0;
            mgmtHodApprovedRemarks = string.Empty;
            mgmtHodApprovedBy = string.Empty;
            mgmtHodApprovedOn = string.Empty;

            finalApprovedByID = 0;
            finalApprovedRemarks = string.Empty;
            finalApprovedBy = string.Empty;
            finalApprovedOn = string.Empty;

            deletedByID = 0;
            deletedRemarks = string.Empty;
            deletedBy = string.Empty;
            deletedOn = string.Empty;

            cancelledByID = 0;
            cancelledRemarks = string.Empty;
            cancelledBy = string.Empty;
            cancelledOn = string.Empty;
            int teamleaderID = 0;


            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();







            if (dtTourDetail.Rows.Count > 0)
            {
                DataRow dr = dtTourDetail.Rows[0];

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



                /////
                if (dr["COUNTRY_OF_VISIT"] != DBNull.Value)
                    countryOfVisit = Convert.ToString(dr["COUNTRY_OF_VISIT"]);

                if (dr["TOUR_BASED_ON"] != DBNull.Value)
                    tourBasedOn = Convert.ToString(dr["TOUR_BASED_ON"]);

                if (dr["INITIATIVE_NAME"] != DBNull.Value)
                    tourIniative = Convert.ToString(dr["INITIATIVE_NAME"]);

                /////

                if (dr["VISIT_TYPE"] != DBNull.Value)
                    purposeOfVisit = Convert.ToString(dr["VISIT_TYPE"]);

                if (dr["JOB_NO"] != DBNull.Value)
                    jobEnquiryNo = Convert.ToString(dr["JOB_NO"]);

                if (dr["BUS_SEGMENT"] != DBNull.Value)
                    businessSegment = Convert.ToString(dr["BUS_SEGMENT"]);

                if (dr["TRAVEL_MODE"] != DBNull.Value)
                    modeOfTravel = Convert.ToString(dr["TRAVEL_MODE"]);

                if (dr["TRIP_TYPE"] != DBNull.Value)
                    typeOfTrip = Convert.ToString(dr["TRIP_TYPE"]);

                if (dr["LOCAL_TRAVEL_TYPE"] != DBNull.Value)
                    inCaseOfLocalTravelling = Convert.ToString(dr["LOCAL_TRAVEL_TYPE"]);

                if (dr["EXPENDITURE_AMT"] != DBNull.Value)
                    expectedExpenditureOfTheTrip = Convert.ToString(dr["EXPENDITURE_AMT"]);

                if (dr["EXPENDITURE_CURRENCY"] != DBNull.Value)
                    expectedExpenditureOfTheTripCurrency = Convert.ToString(dr["EXPENDITURE_CURRENCY"]);

                if (dr["ADVANCE_AMT"] != DBNull.Value)
                    advanceRequired = Convert.ToString(dr["ADVANCE_AMT"]);

                if (dr["ADVANCE_CURRENCY"] != DBNull.Value)
                    advanceRequiredCurrency = Convert.ToString(dr["ADVANCE_CURRENCY"]);




                if (dr["CREATED_BY_ID"] != DBNull.Value)
                    createdByID = Convert.ToInt32(dr["CREATED_BY_ID"]);

                if (dr["CREATED_REMARKS"] != DBNull.Value)
                    createdRemarks = Convert.ToString(dr["CREATED_REMARKS"]);

                if (dr["CREATED_BY"] != DBNull.Value)
                    createdBy = Convert.ToString(dr["CREATED_BY"]);

                if (dr["CREATED_ON"] != DBNull.Value)
                    createdOn = Convert.ToString(dr["CREATED_ON"]);


                if (dr["APPROVED_BY_ID"] != DBNull.Value)
                    hodApprovedByID = Convert.ToInt32(dr["APPROVED_BY_ID"]);

                if (dr["APPROVED_REMARKS"] != DBNull.Value)
                    hodApprovedRemarks = Convert.ToString(dr["APPROVED_REMARKS"]);

                if (dr["APPROVED_BY"] != DBNull.Value)
                    hodApprovedBy = Convert.ToString(dr["APPROVED_BY"]);

                if (dr["APPROVED_ON"] != DBNull.Value)
                    hodApprovedOn = Convert.ToString(dr["APPROVED_ON"]);


                if (dr["MGMT_APPROVED_BY_ID"] != DBNull.Value)
                    mgmtHodApprovedByID = Convert.ToInt32(dr["MGMT_APPROVED_BY_ID"]);

                if (dr["MGMT_APPROVED_REMARKS"] != DBNull.Value)
                    mgmtHodApprovedRemarks = Convert.ToString(dr["MGMT_APPROVED_REMARKS"]);

                if (dr["MGMT_APPROVED_BY"] != DBNull.Value)
                    mgmtHodApprovedBy = Convert.ToString(dr["MGMT_APPROVED_BY"]);

                if (dr["MGMT_APPROVED_ON"] != DBNull.Value)
                    mgmtHodApprovedOn = Convert.ToString(dr["MGMT_APPROVED_ON"]);


                ///////////////
                ///

                if (dr["FINAL_APPROVED_BY_ID"] != DBNull.Value)
                    finalApprovedByID = Convert.ToInt32(dr["FINAL_APPROVED_BY_ID"]);

                if (dr["FINAL_APPROVED_REMARKS"] != DBNull.Value)
                    finalApprovedRemarks = Convert.ToString(dr["FINAL_APPROVED_REMARKS"]);

                if (dr["FINAL_APPROVED_BY"] != DBNull.Value)
                    finalApprovedBy = Convert.ToString(dr["FINAL_APPROVED_BY"]);

                if (dr["FINAL_APPROVED_ON"] != DBNull.Value)
                    finalApprovedOn = Convert.ToString(dr["FINAL_APPROVED_ON"]);
                //////////////////


                if (dr["DELETED_BY_ID"] != DBNull.Value)
                    deletedByID = Convert.ToInt32(dr["DELETED_BY_ID"]);

                if (dr["DELETED_REMARKS"] != DBNull.Value)
                    deletedRemarks = Convert.ToString(dr["DELETED_REMARKS"]);

                if (dr["DELETED_BY"] != DBNull.Value)
                    deletedBy = Convert.ToString(dr["DELETED_BY"]);

                if (dr["DELETED_ON"] != DBNull.Value)
                    deletedOn = Convert.ToString(dr["DELETED_ON"]);


                if (dr["CANCELLED_BY_ID"] != DBNull.Value)
                    cancelledByID = Convert.ToInt32(dr["CANCELLED_BY_ID"]);

                if (dr["CANCELLED_REMARKS"] != DBNull.Value)
                    cancelledRemarks = Convert.ToString(dr["CANCELLED_REMARKS"]);

                if (dr["CANCELLED_BY"] != DBNull.Value)
                    cancelledBy = Convert.ToString(dr["CANCELLED_BY"]);

                if (dr["CANCELLED_ON"] != DBNull.Value)
                    cancelledOn = Convert.ToString(dr["CANCELLED_ON"]);

                if (dr["TEAMLEADER_ID"] != DBNull.Value)
                    teamleaderID = Convert.ToInt32(dr["TEAMLEADER_ID"]);



                sb.Append("<h2 class='headerStyle'>Tour Information</h2>\n");
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
                ///////
                ///
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Country Of Visit:</td>\n");
                sb.Append("<td class='td2header' >{#countryOfVisit#}</td>\n");
                //sb.Append("</tr>\n");

                sb.Append("<td class='td1header'>Tour Based On:</td>\n");
                sb.Append("<td class='td2header' >{#tourBasedOn#}</td>\n");
                sb.Append("</tr>\n");

                ////////



                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Purpose Of Visit:</td>\n");
                sb.Append("<td class='td2header'>{#purposeOfVisit#}</td>\n");
                sb.Append("<td class='td1header'>Job/Enquiry No.:</td>\n");
                sb.Append("<td class='td2header'>{#jobEnquiryNo#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Business Segment:</td>\n");
                sb.Append("<td class='td2header'>{#businessSegment#}</td>\n");
                sb.Append("<td class='td1header'>Mode Of Travel:</td>\n");
                sb.Append("<td class='td2header'>{#modeOfTravel#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Type Of Trip:</td>\n");
                sb.Append("<td class='td2header'>{#typeOfTrip#}</td>\n");
                sb.Append("<td class='td1header'>In Case Of Local Travelling:</td>\n");
                sb.Append("<td class='td2header'>{#inCaseOfLocalTravelling#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Expected Expenditure Of The Trip:</td>\n");
                sb.Append("<td class='td2header'>{#expectedExpenditureOfTheTrip#} : {#expectedExpenditureOfTheTripCurrency#}</td>\n");
                sb.Append("<td class='td1header'>Advance Required:</td>\n");
                sb.Append("<td class='td2header'>{#advanceRequired#} : {#advanceRequiredCurrency#}</td>\n");
                sb.Append("</tr>\n");


                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Tour Iniative:</td>\n");
                sb.Append("<td class='td2header'>{#tourIniative#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("</table>\n");
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

                ////
                sb.Replace("{#countryOfVisit#}", countryOfVisit);
                sb.Replace("{#tourBasedOn#}", tourBasedOn);
                /////
                sb.Replace("{#purposeOfVisit#}", purposeOfVisit);
                sb.Replace("{#jobEnquiryNo#}", jobEnquiryNo);
                sb.Replace("{#businessSegment#}", businessSegment);
                sb.Replace("{#modeOfTravel#}", modeOfTravel);
                sb.Replace("{#typeOfTrip#}", typeOfTrip);
                sb.Replace("{#inCaseOfLocalTravelling#}", inCaseOfLocalTravelling);
                sb.Replace("{#expectedExpenditureOfTheTrip#}", expectedExpenditureOfTheTrip);
                sb.Replace("{#expectedExpenditureOfTheTripCurrency#}", expectedExpenditureOfTheTripCurrency);
                sb.Replace("{#advanceRequired#}", advanceRequired);
                sb.Replace("{#advanceRequiredCurrency#}", advanceRequiredCurrency);
                sb.Replace("{#tourIniative#}", tourIniative);

                if (createdByID > 0)
                {
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

                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#createdRemarks#}", createdRemarks);
                    sb.Replace("{#createdBy#}", createdBy);
                    sb.Replace("{#createdOn#}", createdOn);


                    if (hodApprovedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>HOD Approved</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>HOD Approved Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#approvedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>HOD Approved By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#approvedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>HOD Approved On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#approvedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#approvedRemarks#}", hodApprovedRemarks);
                        sb.Replace("{#approvedBy#}", hodApprovedBy);
                        sb.Replace("{#approvedOn#}", hodApprovedOn);
                    }
                    ////////////////////////////////////////////////////////
                    if (finalApprovedByID > 0 && (hodApprovedByID == 0))
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Final Approved</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Final Approved Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#finalApprovedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Final Approved By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#finalapprovedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Final Approved On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#finalApprovedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#finalApprovedRemarks#}", finalApprovedRemarks);
                        sb.Replace("{#finalapprovedBy#}", finalApprovedBy);
                        sb.Replace("{#finalApprovedOn#}", finalApprovedOn);
                    }
                    /////////////////////////////////////////////////////////

                    //if (mgmtHodApprovedByID > 0)
                    //{
                    //    sb.Append("<fieldset class='pdffieldset'>\n");
                    //    sb.Append("<legend class='pdflegend'>Mgmt. HOD Approved</legend>\n");
                    //    sb.Append("<table class='tblsignatories'>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td colspan='3'>Mgmt. HOD Approved Remarks:</td>\n");
                    //    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td colspan='4'>{#mgmtHodApprovedRemarks#}</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td class='tdsignatories1'>Mgmt. HOD Approved By:</td>\n");
                    //    sb.Append("<td class='tdsignatories2'>{#mgmtHodApprovedBy#}</td>\n");
                    //    sb.Append("<td class='tdsignatories1'>Mgmt. HOD Approved On:</td>\n");
                    //    sb.Append("<td class='tdsignatories2'>{#mgmtHodApprovedOn#}</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("</table>\n");
                    //    sb.Append("</fieldset>\n");

                    //    sb.Append("<hr class='hrsignatories' />\n");

                    //    sb.Replace("{#mgmtHodApprovedRemarks#}", mgmtHodApprovedRemarks);
                    //    sb.Replace("{#mgmtHodApprovedBy#}", mgmtHodApprovedBy);
                    //    sb.Replace("{#mgmtHodApprovedOn#}", mgmtHodApprovedOn);
                    //}

                    /////////////////////////////////////////////////////////////////////////////////

                    if ((hodApprovedByID > 0) && (finalApprovedByID > 0))
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Final Approved</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'> Final Approved Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#finalApprovedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Final Approved By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#finalApprovedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Final Approved On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#finalApprovedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#finalApprovedRemarks#}", finalApprovedRemarks);
                        sb.Replace("{#finalApprovedBy#}", finalApprovedBy);
                        sb.Replace("{#finalApprovedOn#}", finalApprovedOn);
                    }

                    if (deletedByID > 0)
                    {

                        if (deletedByID == teamleaderID)
                        {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>Rejected</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#deletedRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#deletedRemarks#}", deletedRemarks);
                            sb.Replace("{#deletedBy#}", deletedBy);
                            sb.Replace("{#deletedOn#}", deletedOn);
                        }
                        else
                        {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>Deleted</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'>Deleted Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#deletedRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Deleted By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>Deleted On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#deletedRemarks#}", deletedRemarks);
                            sb.Replace("{#deletedBy#}", deletedBy);
                            sb.Replace("{#deletedOn#}", deletedOn);
                        }
                        
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


    public string AddAdvanceGetHtmlForPDF(DataTable dtRequestDetail)
    {
        try
        {

            requestId = string.Empty;
            reqNumber = string.Empty;
            tourNo = string.Empty;
            tourSanctionNo = string.Empty;
            emplyoeeName = string.Empty;
            employeeID = string.Empty;
            designation = string.Empty;
            startDateOfTour = string.Empty;
            endDateOfTour = string.Empty;
            nameOfCustomerVendor = string.Empty;
            placeOfVisit = string.Empty;
            
            AdvanceTaken = string.Empty;
            AdvanceTakenCurrency = string.Empty;
            AdditionalAdvanceRequired = string.Empty;
            AdditionalAdvanceRequiredCurrency = string.Empty;

            createdByID = 0;
            createdRemarks = string.Empty;
            createdBy = string.Empty;
            createdOn = string.Empty;

            hodApprovedByID = 0;
            hodApprovedRemarks = string.Empty;
            hodApprovedBy = string.Empty;
            hodApprovedOn = string.Empty;

           
            deletedByID = 0;
            deletedRemarks = string.Empty;
            deletedBy = string.Empty;
            deletedOn = string.Empty;

            cancelledByID = 0;
            cancelledRemarks = string.Empty;
            cancelledBy = string.Empty;
            cancelledOn = string.Empty;


            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();







            if (dtRequestDetail.Rows.Count > 0)
            {
                DataRow dr = dtRequestDetail.Rows[0];

                if (dr["TOUR_SANCTION_NO"] != DBNull.Value)
                    tourSanctionNo = Convert.ToString(dr["TOUR_SANCTION_NO"]);

                if (dr["TOUR_NO"] != DBNull.Value)
                    tourNo = Convert.ToString(dr["TOUR_NO"]);

                if (dr["REQUEST_ID"] != DBNull.Value)
                    requestId = Convert.ToString(dr["REQUEST_ID"]);


                if (dr["REQ_NO"] != DBNull.Value)
                    reqNumber = Convert.ToString(dr["REQ_NO"]);

                

                if (dr["EMPLOYEE_NAME"] != DBNull.Value)
                    emplyoeeName = Convert.ToString(dr["EMPLOYEE_NAME"]);

                if (dr["EMPLOYEE_ID"] != DBNull.Value)
                    employeeID = Convert.ToString(dr["EMPLOYEE_ID"]);

                

                if (dr["START_DATE"] != DBNull.Value)
                    startDateOfTour = Convert.ToString(dr["START_DATE"]);

                if (dr["END_DATE"] != DBNull.Value)
                    endDateOfTour = Convert.ToString(dr["END_DATE"]);

                if (dr["CUST_VEND_NAME"] != DBNull.Value)
                    nameOfCustomerVendor = Convert.ToString(dr["CUST_VEND_NAME"]);

                if (dr["PLACE_OF_VISIT"] != DBNull.Value)
                    placeOfVisit = Convert.ToString(dr["PLACE_OF_VISIT"]);




                if (dr["ADVANCE_AMT"] != DBNull.Value)
                    advanceTaken = Convert.ToString(dr["ADVANCE_AMT"]);

                if (dr["ADVANCE_CURRENCY"] != DBNull.Value)
                    advanceTakenCurrency = Convert.ToString(dr["ADVANCE_CURRENCY"]);

                if (dr["ADVANCE_AMT"] != DBNull.Value)
                    AddAdvanceRequired = Convert.ToString(dr["ADDITIONAL_REQUESTED_AMT"]);

                if (dr["ADVANCE_CURRENCY"] != DBNull.Value)
                    AddAdvanceRequiredCurrency = Convert.ToString(dr["ADDITIONAL_REQUESTED_ADVANCE_CURRENCY"]);




                if (dr["CREATED_BY_ID"] != DBNull.Value)
                    createdByID = Convert.ToInt32(dr["CREATED_BY_ID"]);

                if (dr["TEAMLEADER_ID"] != DBNull.Value)
                    teamleaderID = Convert.ToInt32(dr["TEAMLEADER_ID"]);

                if (dr["REMARKS"] != DBNull.Value)
                    createdRemarks = Convert.ToString(dr["REMARKS"]);

                if (dr["CREATED_BY"] != DBNull.Value)
                    createdBy = Convert.ToString(dr["CREATED_BY"]);

                if (dr["CREATED_ON"] != DBNull.Value)
                    createdOn = Convert.ToString(dr["CREATED_ON"]);


                if (dr["APPROVED_BY_ID"] != DBNull.Value)
                    hodApprovedByID = Convert.ToInt32(dr["APPROVED_BY_ID"]);

                if (dr["HOD_APPROVED_REMARKS"] != DBNull.Value)
                    hodApprovedRemarks = Convert.ToString(dr["HOD_APPROVED_REMARKS"]);

                if (dr["APPROVED_BY"] != DBNull.Value)
                    hodApprovedBy = Convert.ToString(dr["APPROVED_BY"]);

                if (dr["HOD_APPROVED_ON"] != DBNull.Value)
                    hodApprovedOn = Convert.ToString(dr["HOD_APPROVED_ON"]);



                if (dr["APPROVED_BY_ID"] != DBNull.Value)
                    deletedByID = Convert.ToInt32(dr["DELETED_BY_ID"]);

                if (dr["DELETED_REMARKS"] != DBNull.Value)
                    deletedRemarks = Convert.ToString(dr["DELETED_REMARKS"]);

                if (dr["DELETED_BY"] != DBNull.Value)
                    deletedBy = Convert.ToString(dr["DELETED_BY"]);

                if (dr["DELETED_ON"] != DBNull.Value)
                    deletedOn = Convert.ToString(dr["DELETED_ON"]);


                if (dr["CANCELLED_BY_ID"] != DBNull.Value)
                    cancelledByID = Convert.ToInt32(dr["CANCELLED_BY_ID"]);

                if (dr["CANCELLED_REMARKS"] != DBNull.Value)
                    cancelledRemarks = Convert.ToString(dr["CANCELLED_REMARKS"]);

                if (dr["CANCELLED_BY"] != DBNull.Value)
                    cancelledBy = Convert.ToString(dr["CANCELLED_BY"]);

                if (dr["CANCELLED_ON"] != DBNull.Value)
                    cancelledOn = Convert.ToString(dr["CANCELLED_ON"]);



                sb.Append("<h2 class='headerStyle'>Additional Advance Required Information</h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");
                sb.Append("<tr>\n");
                
                sb.Append("<td class='td1header'>Tour No.:</td>\n");
                sb.Append("<td class='td2header'>{#tourNo#}</td>\n");
                sb.Append("<td class='td1header'>Tour Sanction No.:</td>\n");
                sb.Append("<td class='td2header'>{#tourSanctionNo#}</td>\n");
                sb.Append("</tr>\n");

                //sb.Append("<tr>\n");
                //sb.Append("<td class='td1header'>Request ID.:</td>\n");
                //sb.Append("<td class='td2header'>{#requestId#}</td>\n");
                //sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Request Number:</td>\n");
                sb.Append("<td class='td2header'>{#reqNumber#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Emplyoee Name:</td>\n");
                sb.Append("<td class='td2header'>{#emplyoeeName#}</td>\n");
                sb.Append("<td class='td1header'>Employee ID:</td>\n");
                sb.Append("<td class='td2header'>{#employeeID#}</td>\n");
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
                sb.Append("<td class='td1header'>Advance Taken:</td>\n");
                sb.Append("<td class='td2header'>{#AdvanceTaken#}  {#advanceTakenCurrency#}</td>\n");
                sb.Append("<td class='td1header'>Additional Advance Required:</td>\n");
                sb.Append("<td class='td2header'>{#advanceRequired#} : {#advanceRequiredCurrency#}</td>\n");
                sb.Append("</tr>\n");


                sb.Append("</table>\n");
                sb.Append("<hr />\n");

                sb.Replace("{#requestId#}", requestId);

                sb.Replace("{#reqNumber#}", reqNumber);
                sb.Replace("{#tourNo#}", tourNo);
                sb.Replace("{#tourSanctionNo#}", tourSanctionNo);
                sb.Replace("{#emplyoeeName#}", emplyoeeName);
                sb.Replace("{#employeeID#}", employeeID);
                sb.Replace("{#designation#}", designation);
                sb.Replace("{#startDateOfTour#}", startDateOfTour);
                sb.Replace("{#endDateOfTour#}", endDateOfTour);
                sb.Replace("{#nameOfCustomerVendor#}", nameOfCustomerVendor);
                sb.Replace("{#placeOfVisit#}", placeOfVisit);

                
                sb.Replace("{#AdvanceTaken#}", advanceTaken);
                sb.Replace("{#advanceTakenCurrency#}", advanceTakenCurrency);
                sb.Replace("{#advanceRequired#}", AddAdvanceRequired);
                sb.Replace("{#advanceRequiredCurrency#}", AddAdvanceRequiredCurrency);
               

                if (createdByID > 0)
                {
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

                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#createdRemarks#}", createdRemarks);
                    sb.Replace("{#createdBy#}", createdBy);
                    sb.Replace("{#createdOn#}", createdOn);


                    if (hodApprovedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>HOD Approved</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>HOD Approved Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#approvedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>HOD Approved By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#approvedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>HOD Approved On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#approvedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");

                        sb.Replace("{#approvedRemarks#}", hodApprovedRemarks);
                        sb.Replace("{#approvedBy#}", hodApprovedBy);
                        sb.Replace("{#approvedOn#}", hodApprovedOn);
                    }
                   


                    if (deletedByID > 0)
                    {
                        

                        if (deletedByID == teamleaderID)
                        {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>Rejected</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#deletedRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#deletedRemarks#}", deletedRemarks);
                            sb.Replace("{#deletedBy#}", deletedBy);
                            sb.Replace("{#deletedOn#}", deletedOn);

                        }
                        else
                        {
                            sb.Append("<fieldset class='pdffieldset'>\n");
                            sb.Append("<legend class='pdflegend'>Deleted</legend>\n");
                            sb.Append("<table class='tblsignatories'>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='3'>Deleted Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#deletedRemarks#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Deleted By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedBy#}</td>\n");
                            sb.Append("<td class='tdsignatories1'>Deleted On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#deletedOn#}</td>\n");
                            sb.Append("</tr>\n");
                            sb.Append("</table>\n");
                            sb.Append("</fieldset>\n");

                            sb.Append("<hr class='hrsignatories' />\n");

                            sb.Replace("{#deletedRemarks#}", deletedRemarks);
                            sb.Replace("{#deletedBy#}", deletedBy);
                            sb.Replace("{#deletedOn#}", deletedOn);

                        }




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