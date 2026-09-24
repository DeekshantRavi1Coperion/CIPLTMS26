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
public class DMSHtmlForPDF
{
    int recordID = 0;
    string jobNo = string.Empty;
    string designCategory = string.Empty;
    string description = string.Empty;
    string UOM = string.Empty;
    string quantity = string.Empty;
    string reqdDateByProjectTeam = string.Empty;
    //string isPlanned = string.Empty;
    string plannedStartDateByDesignTeam = string.Empty;
    string plannedCompletionDateByDesignTeam = string.Empty;
    string drawingNo = string.Empty;

    string clientDrawingNo = string.Empty;
    string contractorDrawingNo = string.Empty;

    string documentLink = string.Empty;
    string drawingRevNo = string.Empty;
    string workingStatus = string.Empty;
    string expectedCompletionDate = string.Empty;
    string designResponsibleEngg = string.Empty;
    string designChecker = string.Empty;
    int statusID = 0;
    string status = string.Empty;
    string applicableForProduction = string.Empty;
    string isRevised = string.Empty;
    int amendmentCount = 0;

    int createdByID = 0;
    string createdBy = string.Empty;
    string createdOn = string.Empty;
    string remarks = string.Empty;

    int openByID = 0;
    string openBy = string.Empty;
    string openOn = string.Empty;
    string openRemarks = string.Empty;


    int sentToCheckingByID = 0;
    string sentToCheckingBy = string.Empty;
    string sentToCheckingOn = string.Empty;
    string sentToCheckingRemarks = string.Empty;

    int checkedByID = 0;
    string checkedBy = string.Empty;
    string checkedOn = string.Empty;
    string checkedRemarks = string.Empty;

    int closedByID = 0;
    string closedBy = string.Empty;
    string closedOn = string.Empty;
    string closedRemarks = string.Empty;

    int sentToAmendmentByID = 0;
    string sentToAmendmentBy = string.Empty;
    string sentToAmendmentOn = string.Empty;
    string sentToAmendmentRemarks = string.Empty;

    int amendedByID = 0;
    string amendedBy = string.Empty;
    string amendedOn = string.Empty;
    string amendedRemarks = string.Empty;

    int amendedOpenByID = 0;
    string amendedOpenBy = string.Empty;
    string amendedOpenOn = string.Empty;
    string amendedOpenRemarks = string.Empty;

    int amendedSentToCheckingByID = 0;
    string amendedSentToCheckingBy = string.Empty;
    string amendedSentToCheckingOn = string.Empty;
    string amendedSentToCheckingRemarks = string.Empty;

    int amendedCheckedByID = 0;
    string amendedCheckedBy = string.Empty;
    string amendedCheckedOn = string.Empty;
    string amendedCheckedRemarks = string.Empty;

    public DMSHtmlForPDF()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public string GetHtmlForPDF(DataTable dtDrawingDetail)
    {
        try
        {
            recordID = 0;
            jobNo = string.Empty;
            designCategory = string.Empty;
            description = string.Empty;
            UOM = string.Empty;
            quantity = string.Empty;
            reqdDateByProjectTeam = string.Empty;
            //isPlanned = string.Empty;
            plannedStartDateByDesignTeam = string.Empty;
            plannedCompletionDateByDesignTeam = string.Empty;

            drawingNo = string.Empty;

            clientDrawingNo = string.Empty;
            contractorDrawingNo = string.Empty;

            documentLink = string.Empty;
            drawingRevNo = string.Empty;
            workingStatus = string.Empty;
            expectedCompletionDate = string.Empty;
            designResponsibleEngg = string.Empty;
            designChecker = string.Empty;
            statusID = 0;
            status = string.Empty;
            applicableForProduction = string.Empty;
            isRevised = string.Empty;
            amendmentCount = 0;

            createdByID = 0;
            createdBy = string.Empty;
            createdOn = string.Empty;
            remarks = string.Empty;

            openByID = 0;
            openBy = string.Empty;
            openOn = string.Empty;
            openRemarks = string.Empty;


            sentToCheckingByID = 0;
            sentToCheckingBy = string.Empty;
            sentToCheckingOn = string.Empty;
            sentToCheckingRemarks = string.Empty;

            checkedByID = 0;
            checkedBy = string.Empty;
            checkedOn = string.Empty;
            checkedRemarks = string.Empty;

            closedByID = 0;
            closedBy = string.Empty;
            closedOn = string.Empty;
            closedRemarks = string.Empty;


            sentToAmendmentByID = 0;
            sentToAmendmentBy = string.Empty;
            sentToAmendmentOn = string.Empty;
            sentToAmendmentRemarks = string.Empty;

            amendedByID = 0;
            amendedBy = string.Empty;
            amendedOn = string.Empty;
            amendedRemarks = string.Empty;

            amendedOpenByID = 0;
            amendedOpenBy = string.Empty;
            amendedOpenOn = string.Empty;
            amendedOpenRemarks = string.Empty;

            amendedSentToCheckingByID = 0;
            amendedSentToCheckingBy = string.Empty;
            amendedSentToCheckingOn = string.Empty;
            amendedSentToCheckingRemarks = string.Empty;

            amendedCheckedByID = 0;
            amendedCheckedBy = string.Empty;
            amendedCheckedOn = string.Empty;
            amendedCheckedRemarks = string.Empty;

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (dtDrawingDetail.Rows.Count > 0)
            {

                if (dtDrawingDetail.Rows[0]["RECORD_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["RECORD_ID"]) > 0)
                    recordID = Convert.ToInt32(dtDrawingDetail.Rows[0]["RECORD_ID"]);

                if (dtDrawingDetail.Rows[0]["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["JOB_NO"])))
                    jobNo = Convert.ToString(dtDrawingDetail.Rows[0]["JOB_NO"]);

                if (dtDrawingDetail.Rows[0]["DESIGN_CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["DESIGN_CATEGORY"])))
                    designCategory = Convert.ToString(dtDrawingDetail.Rows[0]["DESIGN_CATEGORY"]);

                if (dtDrawingDetail.Rows[0]["DESCRIPTION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["DESCRIPTION"])))
                    description = Convert.ToString(dtDrawingDetail.Rows[0]["DESCRIPTION"]);

                if (dtDrawingDetail.Rows[0]["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["UOM"])))
                    UOM = Convert.ToString(dtDrawingDetail.Rows[0]["UOM"]);

                if (dtDrawingDetail.Rows[0]["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["QUANTITY"])))
                    quantity = Convert.ToString(dtDrawingDetail.Rows[0]["QUANTITY"]);

                if (dtDrawingDetail.Rows[0]["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["REQD_DATE_BY_PROJECT_TEAM"])))
                    reqdDateByProjectTeam = Convert.ToString(dtDrawingDetail.Rows[0]["REQD_DATE_BY_PROJECT_TEAM"]);

                if (dtDrawingDetail.Rows[0]["PLANNED_START_DATE_BY_DESIGN_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["PLANNED_START_DATE_BY_DESIGN_TEAM"])))
                    plannedStartDateByDesignTeam = Convert.ToString(dtDrawingDetail.Rows[0]["PLANNED_START_DATE_BY_DESIGN_TEAM"]);

                if (dtDrawingDetail.Rows[0]["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"])))
                    plannedCompletionDateByDesignTeam = Convert.ToString(dtDrawingDetail.Rows[0]["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"]);

                if (dtDrawingDetail.Rows[0]["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["DRAWING_NO"])))
                    drawingNo = Convert.ToString(dtDrawingDetail.Rows[0]["DRAWING_NO"]);


                if (dtDrawingDetail.Rows[0]["CLIENT_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CLIENT_DRAWING_NO"])))
                    clientDrawingNo = Convert.ToString(dtDrawingDetail.Rows[0]["CLIENT_DRAWING_NO"]);

                if (dtDrawingDetail.Rows[0]["CONTRACTOR_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CONTRACTOR_DRAWING_NO"])))
                    contractorDrawingNo = Convert.ToString(dtDrawingDetail.Rows[0]["CONTRACTOR_DRAWING_NO"]);


                if (dtDrawingDetail.Rows[0]["DOCUMENT_LINK"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["DOCUMENT_LINK"])))
                    documentLink = Convert.ToString(dtDrawingDetail.Rows[0]["DOCUMENT_LINK"]);

                if (dtDrawingDetail.Rows[0]["DRAWING_REV_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["DRAWING_REV_NO"])))
                    drawingRevNo = Convert.ToString(dtDrawingDetail.Rows[0]["DRAWING_REV_NO"]);

                if (dtDrawingDetail.Rows[0]["WORKING_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["WORKING_STATUS"])))
                    workingStatus = Convert.ToString(dtDrawingDetail.Rows[0]["WORKING_STATUS"]);

                if (dtDrawingDetail.Rows[0]["EXPECTED_COMPLETION_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["EXPECTED_COMPLETION_DATE"])))
                    expectedCompletionDate = Convert.ToString(dtDrawingDetail.Rows[0]["EXPECTED_COMPLETION_DATE"]);

                if (dtDrawingDetail.Rows[0]["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["DESIGN_RESPONSIBLE_ENGG"])))
                    designResponsibleEngg = Convert.ToString(dtDrawingDetail.Rows[0]["DESIGN_RESPONSIBLE_ENGG"]);

                if (dtDrawingDetail.Rows[0]["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["DESIGN_RESPONSIBLE_ENGG"])))
                    designResponsibleEngg = Convert.ToString(dtDrawingDetail.Rows[0]["DESIGN_RESPONSIBLE_ENGG"]);

                if (dtDrawingDetail.Rows[0]["STATUS_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["STATUS_ID"]) > 0)
                    statusID = Convert.ToInt32(dtDrawingDetail.Rows[0]["STATUS_ID"]);

                if (dtDrawingDetail.Rows[0]["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["STATUS"])))
                    status = Convert.ToString(dtDrawingDetail.Rows[0]["STATUS"]);

                if (dtDrawingDetail.Rows[0]["APPLICABLE_FOR_PRODUCTION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["APPLICABLE_FOR_PRODUCTION"])))
                    applicableForProduction = Convert.ToString(dtDrawingDetail.Rows[0]["APPLICABLE_FOR_PRODUCTION"]);

                if (dtDrawingDetail.Rows[0]["IS_REVISED"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["IS_REVISED"])))
                    isRevised = Convert.ToString(dtDrawingDetail.Rows[0]["IS_REVISED"]);

                if (dtDrawingDetail.Rows[0]["AMENDMENT_COUNT"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDMENT_COUNT"]) > 0)
                    amendmentCount = Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDMENT_COUNT"]);



                if (dtDrawingDetail.Rows[0]["CREATED_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["CREATED_BY_ID"]) > 0)
                    createdByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["CREATED_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CREATED_BY"])))
                    createdBy = Convert.ToString(dtDrawingDetail.Rows[0]["CREATED_BY"]);

                if (dtDrawingDetail.Rows[0]["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CREATED_ON"])))
                    createdOn = Convert.ToString(dtDrawingDetail.Rows[0]["CREATED_ON"]);

                if (dtDrawingDetail.Rows[0]["REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["REMARKS"])))
                    remarks = Convert.ToString(dtDrawingDetail.Rows[0]["REMARKS"]);



                if (dtDrawingDetail.Rows[0]["OPEN_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["OPEN_BY_ID"]) > 0)
                    openByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["OPEN_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["OPEN_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["OPEN_BY"])))
                    openBy = Convert.ToString(dtDrawingDetail.Rows[0]["OPEN_BY"]);

                if (dtDrawingDetail.Rows[0]["OPEN_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["OPEN_ON"])))
                    openOn = Convert.ToString(dtDrawingDetail.Rows[0]["OPEN_ON"]);

                if (dtDrawingDetail.Rows[0]["OPEN_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["OPEN_REMARKS"])))
                    openRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["OPEN_REMARKS"]);



                if (dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_BY_ID"]) > 0)
                    sentToCheckingByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_BY"])))
                    sentToCheckingBy = Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_BY"]);

                if (dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_ON"])))
                    sentToCheckingOn = Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_ON"]);

                if (dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_REMARKS"])))
                    sentToCheckingRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_CHECKING_REMARKS"]);



                if (dtDrawingDetail.Rows[0]["CHECKED_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["CHECKED_BY_ID"]) > 0)
                    checkedByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["CHECKED_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["CHECKED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CHECKED_BY"])))
                    checkedBy = Convert.ToString(dtDrawingDetail.Rows[0]["CHECKED_BY"]);

                if (dtDrawingDetail.Rows[0]["CHECKED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CHECKED_ON"])))
                    checkedOn = Convert.ToString(dtDrawingDetail.Rows[0]["CHECKED_ON"]);

                if (dtDrawingDetail.Rows[0]["CHECKED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CHECKED_REMARKS"])))
                    checkedRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["CHECKED_REMARKS"]);



                if (dtDrawingDetail.Rows[0]["CLOSED_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["CLOSED_BY_ID"]) > 0)
                    closedByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["CLOSED_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["CLOSED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CLOSED_BY"])))
                    closedBy = Convert.ToString(dtDrawingDetail.Rows[0]["CLOSED_BY"]);

                if (dtDrawingDetail.Rows[0]["CLOSED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CLOSED_ON"])))
                    closedOn = Convert.ToString(dtDrawingDetail.Rows[0]["CLOSED_ON"]);

                if (dtDrawingDetail.Rows[0]["CLOSED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["CLOSED_REMARKS"])))
                    closedRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["CLOSED_REMARKS"]);





                if (dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_BY_ID"]) > 0)
                    sentToAmendmentByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_BY"])))
                    sentToAmendmentBy = Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_BY"]);

                if (dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_ON"])))
                    sentToAmendmentOn = Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_ON"]);

                if (dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_REMARKS"])))
                    sentToAmendmentRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["SENT_TO_AMENDMENT_REMARKS"]);



                if (dtDrawingDetail.Rows[0]["AMENDED_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDED_BY_ID"]) > 0)
                    amendedByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDED_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_BY"])))
                    amendedBy = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_BY"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_ON"])))
                    amendedOn = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_ON"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_REMARKS"])))
                    amendedRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_REMARKS"]);


                if (dtDrawingDetail.Rows[0]["AMENDED_OPEN_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDED_OPEN_BY_ID"]) > 0)
                    amendedOpenByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDED_OPEN_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_OPEN_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_OPEN_BY"])))
                    amendedOpenBy = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_OPEN_BY"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_OPEN_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_OPEN_ON"])))
                    amendedOpenOn = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_OPEN_ON"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_OPEN_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_OPEN_REMARKS"])))
                    amendedOpenRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_OPEN_REMARKS"]);


                if (dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_BY_ID"]) > 0)
                    amendedSentToCheckingByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_BY"])))
                    amendedSentToCheckingBy = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_BY"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_ON"])))
                    amendedSentToCheckingOn = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_ON"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_REMARKS"])))
                    amendedSentToCheckingRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_SENT_TO_CHECKING_REMARKS"]);


                if (dtDrawingDetail.Rows[0]["AMENDED_CHECKED_BY_ID"] != DBNull.Value && Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDED_CHECKED_BY_ID"]) > 0)
                    amendedCheckedByID = Convert.ToInt32(dtDrawingDetail.Rows[0]["AMENDED_CHECKED_BY_ID"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_CHECKED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_CHECKED_BY"])))
                    amendedCheckedBy = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_CHECKED_BY"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_CHECKED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_CHECKED_ON"])))
                    amendedCheckedOn = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_CHECKED_ON"]);

                if (dtDrawingDetail.Rows[0]["AMENDED_CHECKED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_CHECKED_REMARKS"])))
                    amendedCheckedRemarks = Convert.ToString(dtDrawingDetail.Rows[0]["AMENDED_CHECKED_REMARKS"]);


                sb.Append("<h2 class='headerStyle'><u>DRAWING {#drawingNo#} DETAILS</u></h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>JOB No:</td>\n");
                sb.Append("<td class='td2header'>{#jobNo#}</td>\n");
                sb.Append("<td class='td1header'>Design Category:</td>\n");
                sb.Append("<td class='td2header'>{#designCategory#}</td>\n");
                sb.Append("</tr>\n");

                
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Client Drawing No:</td>\n");
                sb.Append("<td class='td2header'>{#clientDrawingNo#}</td>\n");
                sb.Append("<td class='td1header'>Contractor Drawing No:</td>\n");
                sb.Append("<td class='td2header'>{#contractorDrawingNo#}</td>\n");
                sb.Append("</tr>\n");


                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Description:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#description#}</td>\n");
                sb.Append("</tr>\n");
                
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>UOM:</td>\n");
                sb.Append("<td class='td2header'>{#UOM#}</td>\n");
                sb.Append("<td class='td1header'>Quantity:</td>\n");
                sb.Append("<td class='td2header'>{#quantity#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Required Date by Project Team:</td>\n");
                sb.Append("<td class='td2header'>{#reqdDateByProjectTeam#}</td>\n");
                //sb.Append("<td class='td1header'>Is Planned?:</td>\n");
                //sb.Append("<td class='td2header'>{#isPlanned#}</td>\n");
                sb.Append("</tr>\n");
                
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Planned Start Date by Design Team:</td>\n");
                sb.Append("<td class='td2header'>{#plannedStartDateByDesignTeam#}</td>\n");
                sb.Append("<td class='td1header'>Planned Completion Date by Design Team:</td>\n");
                sb.Append("<td class='td2header'>{#plannedCompletionDateByDesignTeam#}</td>\n");
                sb.Append("</tr>\n");
                
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Document Link:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#documentLink#}</td>\n");
                sb.Append("</tr>\n");
                
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Is Revised?:</td>\n");
                sb.Append("<td class='td2header'>{#isRevised#}</td>\n");
                sb.Append("<td class='td1header'>Drawing Revision No:</td>\n");
                sb.Append("<td class='td2header'>{#drawingRevNo#}</td>\n");
                sb.Append("</tr>\n");
                
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Design Responsible Engineer:</td>\n");
                sb.Append("<td class='td2header'>{#designResponsibleEngg#}</td>\n");
                sb.Append("<td class='td1header'>Expected Completion Date:</td>\n");
                sb.Append("<td class='td2header'>{#expectedCompletionDate#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Design Checker:</td>\n");
                sb.Append("<td class='td2header'>{#designChecker#}</td>\n");
                sb.Append("<td class='td1header'>Status:</td>\n");
                sb.Append("<td class='td2header'>{#Status#}</td>\n");
                sb.Append("</tr>\n");
                
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Working Status:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#workingstatus#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("<hr />\n");

                sb.Replace("{#drawingNo#}", drawingNo);
                sb.Replace("{#jobNo#}", jobNo);

                sb.Replace("{#clientDrawingNo#}", clientDrawingNo);
                sb.Replace("{#contractorDrawingNo#}", contractorDrawingNo);

                sb.Replace("{#designCategory#}", designCategory);
                sb.Replace("{#description#}", description);
                sb.Replace("{#UOM#}", UOM);
                sb.Replace("{#quantity#}", quantity);
                sb.Replace("{#reqdDateByProjectTeam#}", reqdDateByProjectTeam);
                //sb.Replace("{#isPlanned#}", isPlanned);
                sb.Replace("{#plannedStartDateByDesignTeam#}", plannedStartDateByDesignTeam);
                sb.Replace("{#plannedCompletionDateByDesignTeam#}", plannedCompletionDateByDesignTeam);
                sb.Replace("{#documentLink#}", documentLink);
                sb.Replace("{#isRevised#}", isRevised);
                sb.Replace("{#drawingRevNo#}", drawingRevNo);
                sb.Replace("{#designResponsibleEngg#}", designResponsibleEngg);
                sb.Replace("{#expectedCompletionDate#}", expectedCompletionDate);
                sb.Replace("{#designChecker#}", designChecker);
                sb.Replace("{#Status#}", status);
                sb.Replace("{#workingstatus#}", workingStatus);




                sb.Append("<h3 class='header2'>Signatories</h3>\n");

                if (createdByID > 0)
                {
                    sb.Append("<hr />\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Created/Amended</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Created Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#remarks#}</td>\n");
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

                        sb.Replace("{#amendedRemarks#}", amendedRemarks);
                        sb.Replace("{#amendedBy#}", amendedBy);
                        sb.Replace("{#amendedOn#}", amendedOn);
                    }


                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");

                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#remarks#}", remarks);
                    sb.Replace("{#createdBy#}", createdBy);
                    sb.Replace("{#createdOn#}", createdOn);
                }




                if (openByID > 0 || amendedOpenByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Open/Amended Open</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");

                    if (openByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Open Remarks:</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#openRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Open By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#openBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Open On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#openOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }

                    if (amendedOpenByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Amended Open Remarks:</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#amendedOpenRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Open By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedOpenBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Open On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedOpenOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }





                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#openRemarks#}", openRemarks);
                    sb.Replace("{#openBy#}", openBy);
                    sb.Replace("{#openOn#}", openOn);

                    sb.Replace("{#amendedOpenRemarks#}", amendedOpenRemarks);
                    sb.Replace("{#amendedOpenBy#}", amendedOpenBy);
                    sb.Replace("{#amendedOpenOn#}", amendedOpenOn);
                }


                if (amendmentCount > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Amendment</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Sent To Amendment Remarks:</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#sentToAmendmentRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Sent To Amendment By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#sentToAmendmentBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Sent To Amendment On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#sentToAmendmentOn#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Amendment Counts:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#amendmentCount#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");

                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#sentToAmendmentRemarks#}", sentToAmendmentRemarks);
                    sb.Replace("{#sentToAmendmentBy#}", sentToAmendmentBy);
                    sb.Replace("{#sentToAmendmentOn#}", sentToAmendmentOn);
                    sb.Replace("{#amendmentCount#}", Convert.ToString(amendmentCount));

                    //if (amendedOpenByID > 0)
                    //{
                    //    sb.Append("<fieldset class='pdffieldset'>\n");
                    //    sb.Append("<legend class='pdflegend'>Amended</legend>\n");
                    //    sb.Append("<table class='tblsignatories'>\n");

                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td colspan='3'>Amended Open Remarks:</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td colspan='4'>{#amendedOpenRemarks#}</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td class='tdsignatories1'>Amended Open By:</td>\n");
                    //    sb.Append("<td class='tdsignatories2'>{#amendedOpenBy#}</td>\n");
                    //    sb.Append("<td class='tdsignatories1'>Amended Open On:</td>\n");
                    //    sb.Append("<td class='tdsignatories2'>{#amendedOpenOn#}</td>\n");
                    //    sb.Append("</tr>\n");

                    //    sb.Append("</table>\n");
                    //    sb.Append("</fieldset>\n");

                    //    sb.Append("<hr class='hrsignatories' />\n");

                    //    sb.Replace("{#amendedOpenRemarks#}", amendedOpenRemarks);
                    //    sb.Replace("{#amendedOpenBy#}", amendedOpenBy);
                    //    sb.Replace("{#amendedOpenOn#}", amendedOpenOn);
                    //}

                }


                if (sentToCheckingByID > 0 || amendedSentToCheckingByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Sent for Checking/Amended Sent for Checking</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");

                    if (sentToCheckingByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Sent for Checking Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#sentToCheckingRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Sent for Checking By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#sentToCheckingBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Sent for Checking On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#sentToCheckingOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }


                    if (amendedSentToCheckingByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Amended Sent for Checking Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#amendedSentToCheckingRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Sent for Checking By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedSentToCheckingBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amended Sent for Checking On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#amendedSentToCheckingOn#}</td>\n");
                        sb.Append("</tr>\n");
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");

                    sb.Append("<hr class='hrsignatories' />\n");

                    sb.Replace("{#sentToCheckingRemarks#}", sentToCheckingRemarks);
                    sb.Replace("{#sentToCheckingBy#}", sentToCheckingBy);
                    sb.Replace("{#sentToCheckingOn#}", sentToCheckingOn);

                    sb.Replace("{#amendedSentToCheckingRemarks#}", amendedSentToCheckingRemarks);
                    sb.Replace("{#amendedSentToCheckingBy#}", amendedSentToCheckingBy);
                    sb.Replace("{#amendedSentToCheckingOn#}", amendedSentToCheckingOn);
                }


                if (checkedByID > 0 || amendedCheckedByID > 0)
                {
                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Checked/Amended Checked</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");

                    if (checkedByID > 0)
                    {
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
                    }

                    if (amendedCheckedByID > 0)
                    {
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

                //if (closedByID > 0)
                //{
                //    sb.Append("<fieldset class='pdffieldset'>\n");
                //    sb.Append("<legend class='pdflegend'>Closed</legend>\n");
                //    sb.Append("<table class='tblsignatories'>\n");
                //    sb.Append("<tr>\n");
                //    sb.Append("<td colspan='3'>Closed Remarks:</td>\n");
                //    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                //    sb.Append("</tr>\n");
                //    sb.Append("<tr>\n");
                //    sb.Append("<td colspan='4'>{#closedRemarks#}</td>\n");
                //    sb.Append("</tr>\n");
                //    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                //    sb.Append("<tr>\n");
                //    sb.Append("<td class='tdsignatories1'>Closed By:</td>\n");
                //    sb.Append("<td class='tdsignatories2'>{#closedBy#}</td>\n");
                //    sb.Append("<td class='tdsignatories1'>Closed On:</td>\n");
                //    sb.Append("<td class='tdsignatories2'>{#closedOn#}</td>\n");
                //    sb.Append("</tr>\n");
                //    sb.Append("</table>\n");
                //    sb.Append("</fieldset>\n");

                //    sb.Append("<hr class='hrsignatories' />\n");

                //    sb.Replace("{#closedRemarks#}", closedRemarks);
                //    sb.Replace("{#closedBy#}", closedBy);
                //    sb.Replace("{#closedOn#}", closedOn);
                //}

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