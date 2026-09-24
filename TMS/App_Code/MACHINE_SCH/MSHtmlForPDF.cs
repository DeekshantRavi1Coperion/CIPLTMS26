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


public class MSHtmlForPDF
{
    string assignedDrawingCode = string.Empty;
    string unitName = string.Empty;
    string categoryName = string.Empty;
    string jobNo = string.Empty;
    string drawingNo = string.Empty;
    string equipmentNo = string.Empty;
    string tagNo = string.Empty;
    string itemName = string.Empty;
    string itemDetail = string.Empty;
    string expectedDateOfCompByPlanning = string.Empty;
    string status = string.Empty;
    string quantity = "0";



    int srNo = 0;
    string activity = string.Empty;
    string machine = string.Empty;
    string workerName = string.Empty;
    string scheduledDate = string.Empty;
    string scheduledFrom = string.Empty;
    string scheduledTo = string.Empty;

    int assignedByID = 0;
    string assignedBy = string.Empty;
    string assignedOn = string.Empty;
    string assignedRemarks = string.Empty;

    int reAssignedByID = 0;
    string reAssignedBy = string.Empty;
    string reAssignedOn = string.Empty;
    string reAssignedRemarks = string.Empty;

    int acceptedByID = 0;
    string acceptedBy = string.Empty;
    string acceptedOn = string.Empty;
    string acceptedRemarks = string.Empty;

    int rejectedByID = 0;
    string rejectedBy = string.Empty;
    string rejectedOn = string.Empty;
    string rejectedRemarks = string.Empty;

    int scheduledByID = 0;
    string scheduledBy = string.Empty;
    string scheduledOn = string.Empty;
    string scheduledRemarks = string.Empty;

    int completedByID = 0;
    string completionStatus = string.Empty;
    string completedBy = string.Empty;
    string completedOn = string.Empty;
    string completedRemarks = string.Empty;

    int inspectedByID = 0;
    string inspectedBy = string.Empty;
    string inspectedOn = string.Empty;
    int totalAcceptedQuantity = 0;
    int totalRejectedQuantity = 0;

    int qaAcceptedQuantity = 0;
    string qaAcceptedRemarks = string.Empty;
    int qaRejectedQuantity = 0;
    string qaRejectedRemarks = string.Empty;


    int qaAcceptedByID = 0;
    string qaAcceptedBy = string.Empty;
    string qaAcceptedOn = string.Empty;


    int qaRejectedByID = 0;
    string qaRejectedBy = string.Empty;
    string qaRejectedOn = string.Empty;


    int reScheduledByID = 0;
    string reScheduledBy = string.Empty;
    string reScheduledOn = string.Empty;
    string reScheduledRemarks = string.Empty;

    public string GetHtmlForPDF(DataTable dtPrimaryDetails, DataTable dtScheduledDetails, DataTable dtInspectionDetails)
    {
        try
        {
            assignedDrawingCode = string.Empty;
            unitName = string.Empty;
            categoryName = string.Empty;
            jobNo = string.Empty;
            drawingNo = string.Empty;
            equipmentNo = string.Empty;
            tagNo = string.Empty;
            itemName = string.Empty;
            itemDetail = string.Empty;
            expectedDateOfCompByPlanning = string.Empty;
            status = string.Empty;
            quantity = "0";

            assignedByID = 0;
            assignedBy = string.Empty;
            assignedOn = string.Empty;
            assignedRemarks = string.Empty;

            acceptedByID = 0;
            acceptedBy = string.Empty;
            acceptedOn = string.Empty;
            acceptedRemarks = string.Empty;

            rejectedByID = 0;
            rejectedBy = string.Empty;
            rejectedOn = string.Empty;
            rejectedRemarks = string.Empty;

            reAssignedByID = 0;
            reAssignedBy = string.Empty;
            reAssignedOn = string.Empty;
            reAssignedRemarks = string.Empty;

            scheduledByID = 0;
            scheduledBy = string.Empty;
            scheduledOn = string.Empty;
            scheduledRemarks = string.Empty;

            inspectedByID = 0;
            inspectedBy = string.Empty;
            inspectedOn = string.Empty;
            totalAcceptedQuantity = 0;
            totalRejectedQuantity = 0;

            qaAcceptedQuantity = 0;
            qaAcceptedRemarks = string.Empty;
            qaRejectedQuantity = 0;
            qaRejectedRemarks = string.Empty;

            qaAcceptedByID = 0;
            qaAcceptedBy = string.Empty;
            qaAcceptedOn = string.Empty;


            qaRejectedByID = 0;
            qaRejectedBy = string.Empty;
            qaRejectedOn = string.Empty;


            reScheduledByID = 0;
            reScheduledBy = string.Empty;
            reScheduledOn = string.Empty;
            reScheduledRemarks = string.Empty;

            completedByID = 0;
            completedBy = string.Empty;
            completedOn = string.Empty;
            completedRemarks = string.Empty;

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (dtPrimaryDetails.Rows.Count > 0)
            {
                DataRow dr0 = dtPrimaryDetails.Rows[0];

                assignedDrawingCode = Convert.ToString(dr0["ASSIGNED_DRAWING_CODE"]).Trim();
                unitName = Convert.ToString(dr0["UNIT_NAME"]).Trim();

                if (!string.IsNullOrEmpty(Convert.ToString(dr0["CATEGORY_NAME"])))
                    categoryName = Convert.ToString(dr0["CATEGORY_NAME"]);
                else categoryName = "";

                jobNo = Convert.ToString(dr0["JOB_NO"]).Trim();
                drawingNo = Convert.ToString(dr0["DRAWING_NO"]).Trim();
                equipmentNo = Convert.ToString(dr0["EQUIPMENT"]).Trim();
                tagNo = Convert.ToString(dr0["TAG_NO"]).Trim();
                itemName = Convert.ToString(dr0["ITEM_NAME"]).Trim();
                itemDetail = Convert.ToString(dr0["ITEM_DETAIL"]).Trim();
                expectedDateOfCompByPlanning = Convert.ToString(dr0["EXPECTED_DATE_OF_COMP_BY_PLANNING"]).Trim();
                status = Convert.ToString(dr0["STATUS_NAME"]).Trim();
                quantity = Convert.ToString(dr0["ALLOCATED_QUANTITY"]).Trim();


                assignedByID = Convert.ToInt32(dr0["ASSIGNED_BY_ID"]);
                assignedBy = Convert.ToString(dr0["ASSIGNED_BY"]);
                assignedOn = Convert.ToString(dr0["ASSIGNED_ON"]);
                assignedRemarks = Convert.ToString(dr0["ASSIGNED_REMARKS"]);

                reAssignedByID = Convert.ToInt32(dr0["RE_ASSIGNED_BY_ID"]);
                reAssignedBy = Convert.ToString(dr0["RE_ASSIGNED_BY"]);
                reAssignedOn = Convert.ToString(dr0["RE_ASSIGNED_ON"]);
                reAssignedRemarks = Convert.ToString(dr0["RE_ASSIGNED_REMARKS"]);

                acceptedByID = Convert.ToInt32(dr0["ACCEPTED_BY_ID"]);
                acceptedBy = Convert.ToString(dr0["ACCEPTED_BY"]);
                acceptedOn = Convert.ToString(dr0["ACCEPTED_ON"]);
                acceptedRemarks = Convert.ToString(dr0["ACCEPTED_REMARKS"]);

                rejectedByID = Convert.ToInt32(dr0["REJECTED_BY_ID"]);
                rejectedBy = Convert.ToString(dr0["REJECTED_BY"]);
                rejectedOn = Convert.ToString(dr0["REJECTED_ON"]);
                rejectedRemarks = Convert.ToString(dr0["REJECTED_REMARKS"]);

                scheduledByID = Convert.ToInt32(dr0["SCHEDULED_BY_ID"]);
                scheduledBy = Convert.ToString(dr0["SCHEDULED_BY"]);
                scheduledOn = Convert.ToString(dr0["SCHEDULED_ON"]);
                scheduledRemarks = Convert.ToString(dr0["SCHEDULED_REMARKS"]);

                inspectedByID = Convert.ToInt32(dr0["INSPECTED_BY_ID"]);
                inspectedBy = Convert.ToString(dr0["INSPECTED_BY"]);
                inspectedOn = Convert.ToString(dr0["INSPECTED_ON"]);
                totalAcceptedQuantity = Convert.ToInt32(dr0["TOTAL_QA_ACCEPTED_QUANTITY"]);
                totalRejectedQuantity = Convert.ToInt32(dr0["TOTAL_QA_REJECTED_QUANTITY"]);

                reScheduledByID = Convert.ToInt32(dr0["RE_SCHEDULED_BY_ID"]);
                reScheduledBy = Convert.ToString(dr0["RE_SCHEDULED_BY"]);
                reScheduledOn = Convert.ToString(dr0["RE_SCHEDULED_ON"]);
                reScheduledRemarks = Convert.ToString(dr0["RE_SCHEDULED_REMARKS"]);

                completedByID = Convert.ToInt32(dr0["COMPLETED_BY_ID"]);
                completedBy = Convert.ToString(dr0["COMPLETED_BY"]);
                completedOn = Convert.ToString(dr0["COMPLETED_ON"]);
                completedRemarks = Convert.ToString(dr0["COMPLETED_REMARKS"]);


                sb.Append("<h2 class='headerStyle'>MACHINE SCHEDULING</h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Unit:</td>\n");
                sb.Append("<td class='td2header'>{#unitName#}</td>\n");
                sb.Append("<td class='td1header'>Assigned Code:</td>\n");
                sb.Append("<td class='td2header'><b>{#assignedDrawingCode#}</b></td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>JOB Number:</td>\n");
                sb.Append("<td class='td2header'>{#jobNo#}</td>\n");
                sb.Append("<td class='td1header'>Drawing Number:</td>\n");
                sb.Append("<td class='td2header'><b>{#drawingNo#}</b></td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Equipment Number:</td>\n");
                sb.Append("<td class='td2header'>{#equipmentNo#}</td>\n");
                sb.Append("<td class='td1header'>Tag No:</td>\n");
                sb.Append("<td class='td2header'>{#tagNo#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Item Name:</td>\n");
                sb.Append("<td class='td2header'>{#itemName#}</td>\n");
                sb.Append("<td class='td1header'>Quantity:</td>\n");
                sb.Append("<td class='td2header'>{#quantity#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Item Detail:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#itemDetail#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>ED Of Completion By Planning/Production:</td>\n");
                sb.Append("<td class='td2header'>{#expectedDateOfCompByPlanning#}</td>\n");
                sb.Append("<td class='td1header'>Status:</td>\n");
                sb.Append("<td class='td2header'>{#status#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Category:</td>\n");
                sb.Append("<td class='td2header'>{#categoryName#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("</table>\n");
                sb.Append("<hr />\n");

                if (dtScheduledDetails.Rows.Count > 0)
                {
                    sb.Append("<h3 class='header2'>Scheduling Details</h3>\n");
                    sb.Append("<table class='tblsuitems'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
                    sb.Append("<th class='tdtag'>Activity</th>\n");
                    sb.Append("<th class='tddesc'>Machine</th>\n");

                    sb.Append("<th class='tddesc'>Worker</th>\n");
                    sb.Append("<th class='tddesc'>Scheduled Date</th>\n");

                    sb.Append("<th class='tdtag'>Scheduled From</th>\n");
                    sb.Append("<th class='tdtag'>Scheduled To</th>\n");
                    sb.Append("<th class='tdquantity'>Quantity</th>\n");
                    sb.Append("<th class='tddesc'>Scheduled Remarks</th>\n");

                    //sb.Append("<th class='tddesc'>Completion Status</th>\n");
                    //sb.Append("<th class='tddesc'>Completed By</th>\n");
                    //sb.Append("<th class='tddesc'>Completed On</th>\n");
                    //sb.Append("<th class='tddesc'>Completed Remarks</th>\n");

                    sb.Append("</tr>\n");

                    foreach (DataRow drs in dtScheduledDetails.Rows)
                    {
                        srNo++;
                        activity = string.Empty;
                        machine = string.Empty;
                        workerName = string.Empty;
                        scheduledDate = string.Empty;
                        scheduledFrom = string.Empty;
                        scheduledTo = string.Empty;

                        //completedByID = 0;
                        //completionStatus = string.Empty;
                        //completedBy = string.Empty;
                        //completedOn = string.Empty;
                        //completedRemarks = string.Empty;

                        activity = Convert.ToString(drs["ACTIVITY_NAME"]);
                        machine = Convert.ToString(drs["MACHINE_NAME"]);
                        workerName = Convert.ToString(drs["WORKER_NAME"]);
                        scheduledDate = Convert.ToString(drs["SCHEDULED_DATE"]);
                        scheduledFrom = Convert.ToString(drs["SCHEDULED_FROM"]);
                        scheduledTo = Convert.ToString(drs["SCHEDULED_TO"]);

                        //completionStatus = Convert.ToString(drs["COMPLETION_STATUS"]);
                        //completedBy = Convert.ToString(drs["COMPLETED_BY"]);
                        //completedOn = Convert.ToString(drs["COMPLETED_ON"]);
                        //completedRemarks = Convert.ToString(drs["COMPLETED_REMARKS"]);


                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsrno'>{#srNo#}</td>\n");
                        sb.Append("<td class='tdtag'>{#activity#}</td>\n");
                        sb.Append("<td class='tddesc'>{#machine#}</td>\n");

                        sb.Append("<td class='tdtag'>{#workerName#}</td>\n");
                        sb.Append("<td class='tdtag'>{#scheduledDate#}</td>\n");

                        sb.Append("<td class='tdtag'>{#scheduledFrom#}</td>\n");
                        sb.Append("<td class='tdtag'>{#scheduledTo#}</td>\n");
                        sb.Append("<td class='tdquantity'>{#Quantity#}</td>\n");
                        sb.Append("<td class='tddesc'>{#scheduledRemarks#}</td>\n");

                        //sb.Append("<td class='tddesc'>{#completionStatus#}</td>\n");
                        //sb.Append("<td class='tddesc'>{#completedBy#}</td>\n");
                        //sb.Append("<td class='tddesc'>{#completedOn#}</td>\n");
                        //sb.Append("<td class='tddesc'>{#completedRemarks#}</td>\n");

                        sb.Append("</tr>\n");

                        sb.Replace("{#srNo#}", Convert.ToString(srNo));
                        sb.Replace("{#activity#}", Convert.ToString(activity));
                        sb.Replace("{#machine#}", Convert.ToString(machine));

                        sb.Replace("{#workerName#}", Convert.ToString(workerName));
                        sb.Replace("{#scheduledDate#}", Convert.ToString(scheduledDate));
                        sb.Replace("{#scheduledFrom#}", Convert.ToString(scheduledFrom));

                        sb.Replace("{#scheduledTo#}", Convert.ToString(scheduledTo));
                        sb.Replace("{#Quantity#}", Convert.ToString(quantity));

                        if (!string.IsNullOrEmpty(reScheduledRemarks))
                            sb.Replace("{#scheduledRemarks#}", Convert.ToString(reScheduledRemarks));
                        else sb.Replace("{#scheduledRemarks#}", Convert.ToString(scheduledRemarks));
                        
                        
                    }


                    sb.Append("</table>\n");
                    sb.Append("<hr class='hrsignatories' />\n");
                }


                if (assignedByID > 0)
                {
                    sb.Append("<h3 class='header2'>Signatories</h3>\n");
                    sb.Append("<hr />\n");


                    sb.Append("<fieldset class='pdffieldset'>\n");
                    sb.Append("<legend class='pdflegend'>Assigned</legend>\n");
                    sb.Append("<table class='tblsignatories'>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'><b><u>Assigned</u></b></td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='3'>Assigned Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#assignedRemarks#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Assigned By:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#assignedBy#}</td>\n");
                    sb.Append("<td class='tdsignatories1'>Assigned On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#assignedOn#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                    sb.Append("<hr class='hrsignatories' />\n");

                    if (rejectedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Rejected</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><b><u>Rejected</u></b></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#rejectedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#rejectedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#rejectedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");
                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (reAssignedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Re Assigned</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><b><u>Re-Assigned</u></b></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Re Assigned Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#reAssignedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Re Assigned By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#reAssignedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Re Assigned On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#reAssignedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");
                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (acceptedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Accepted</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");


                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><b><u>Accepted</u></b></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#acceptedRemarks#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#acceptedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#acceptedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");
                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (scheduledByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Scheduled</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><b><u>Scheduled</u></b></td>\n");
                        sb.Append("</tr>\n");



                        //sb.Append("<tr>\n");
                        //sb.Append("<td colspan='3'>Scheduled Remarks:</td>\n");
                        //sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        //sb.Append("</tr>\n");

                        //sb.Append("<tr>\n");
                        //sb.Append("<td colspan='4'>{#scheduledRemarks#}</td>\n");
                        //sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Scheduled By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#scheduledBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Scheduled On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#scheduledOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");
                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (reScheduledByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Re-Scheduled</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><b><u>Re-Scheduled</u></b></td>\n");
                        sb.Append("</tr>\n");

                        //sb.Append("<tr>\n");
                        //sb.Append("<td colspan='3'>Re-Scheduled Remarks:</td>\n");
                        //sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        //sb.Append("</tr>\n");

                        //sb.Append("<tr>\n");
                        //sb.Append("<td colspan='4'>{#reScheduledRemarks#}</td>\n");
                        //sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Re-Scheduled By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#reScheduledBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Re-Scheduled On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#reScheduledOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");
                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (completedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Completion</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><b><u>Completion</u></b></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Completion Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#completedRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Completed By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#completedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Completed On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#completedOn#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");
                        sb.Append("<hr class='hrsignatories' />\n");
                    }



                    if (inspectedByID > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Quality Inspection</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><b><u>Quality Inspection</u></b></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

                        foreach (DataRow dri in dtInspectionDetails.Rows)
                        {


                            qaAcceptedQuantity = Convert.ToInt32(dri["ACCEPTED_QUANTITY"]);
                            qaAcceptedByID = Convert.ToInt32(dri["INSPECTED_BY_ID"]);
                            qaAcceptedBy = Convert.ToString(dri["INSPECTED_BY"]);
                            qaAcceptedOn = Convert.ToString(dri["INSPECTED_ON"]);
                            qaAcceptedRemarks = Convert.ToString(dri["ACCEPTED_REMARKS"]);


                            qaRejectedQuantity = Convert.ToInt32(dri["REJECTED_QUANTITY"]);
                            qaRejectedByID = Convert.ToInt32(dri["INSPECTED_BY_ID"]);
                            qaRejectedBy = Convert.ToString(dri["INSPECTED_BY"]);
                            qaRejectedOn = Convert.ToString(dri["INSPECTED_ON"]);
                            qaRejectedRemarks = Convert.ToString(dri["REJECTED_REMARKS"]);


                            if (qaAcceptedQuantity > 0)
                            {
                                sb.Append("<tr>\n");
                                sb.Append("<td colspan='3'>Accepted Remarks:</td>\n");
                                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                                sb.Append("</tr>\n");

                                sb.Append("<tr>\n");
                                sb.Append("<td colspan='4'>{#qaAcceptedRemarks#}</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td class='tdsignatories1'>Accepted Quantity:</td>\n");
                                sb.Append("<td class='tdsignatories2'>{#acceptedQuantity#}</td>\n");
                                sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
                                sb.Append("<td class='tdsignatories2'>{#qaAcceptedOn#}</td>\n");
                                sb.Append("</tr>\n");
                            }

                            if (qaRejectedQuantity > 0)
                            {
                                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                                sb.Append("<td colspan='3'>&nbsp;</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td colspan='4'>{#qaRejectedRemarks#}</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("<tr>\n");
                                sb.Append("<td class='tdsignatories1'>Rejected Quantity:</td>\n");
                                sb.Append("<td class='tdsignatories2'>{#rejectedQuantity#}</td>\n");
                                sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                                sb.Append("<td class='tdsignatories2'>{#qaRejectedOn#}</td>\n");
                                sb.Append("</tr>\n");
                                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                            }


                            sb.Replace("{#qaAcceptedRemarks#}", Convert.ToString(qaAcceptedRemarks));
                            sb.Replace("{#acceptedQuantity#}", Convert.ToString(qaAcceptedQuantity));
                            sb.Replace("{#qaAcceptedOn#}", Convert.ToString(qaAcceptedOn));


                            sb.Replace("{#qaRejectedRemarks#}", Convert.ToString(qaRejectedRemarks));
                            sb.Replace("{#rejectedQuantity#}", Convert.ToString(qaRejectedQuantity));
                            sb.Replace("{#qaRejectedOn#}", Convert.ToString(qaRejectedOn));

                        }



                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><hr class='hrsignatories' /></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");                        
                        sb.Append("<td class='tdsignatories1'>Total Accepted Quantity:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#totalAcceptedQuantity#}</td>\n"); 
                        sb.Append("<td class='tdsignatories1'>Total Rejected Quantity:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#totalRejectedQuantity#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Inspected By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#inspectedBy#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Inspected On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#inspectedOn#}</td>\n"); 
                        sb.Append("</tr>\n");

                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");
                        sb.Append("<hr class='hrsignatories' />\n");

                    }


                    //if (qaRejectedByID > 0)
                    //{
                    //    sb.Append("<fieldset class='pdffieldset'>\n");
                    //    sb.Append("<legend class='pdflegend'>Quality Rejected</legend>\n");
                    //    sb.Append("<table class='tblsignatories'>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td colspan='3'>Quality Rejected Remarks:</td>\n");
                    //    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td colspan='4'>{#qaRejectedRemarks#}</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                    //    sb.Append("<td class='tdsignatories2'>{#qaRejectedBy#}</td>\n");
                    //    sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                    //    sb.Append("<td class='tdsignatories2'>{#qaRejectedOn#}</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("</table>\n");
                    //    sb.Append("</fieldset>\n");
                    //    sb.Append("<hr class='hrsignatories' />\n");
                    //}




                    //if (qaAcceptedByID > 0)
                    //{
                    //    sb.Append("<fieldset class='pdffieldset'>\n");
                    //    sb.Append("<legend class='pdflegend'>Quality Accepted</legend>\n");
                    //    sb.Append("<table class='tblsignatories'>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td colspan='3'>Quality Accepted Remarks:</td>\n");
                    //    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td colspan='4'>{#qaAcceptedRemarks#}</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                    //    sb.Append("<tr>\n");
                    //    sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
                    //    sb.Append("<td class='tdsignatories2'>{#qaAcceptedBy#}</td>\n");
                    //    sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
                    //    sb.Append("<td class='tdsignatories2'>{#qaAcceptedOn#}</td>\n");
                    //    sb.Append("</tr>\n");
                    //    sb.Append("</table>\n");
                    //    sb.Append("</fieldset>\n");
                    //    sb.Append("<hr class='hrsignatories' />\n");
                    //}
                }



                sb.Replace("{#assignedDrawingCode#}", Convert.ToString(assignedDrawingCode));
                sb.Replace("{#unitName#}", Convert.ToString(unitName));
                sb.Replace("{#categoryName#}", Convert.ToString(categoryName));
                sb.Replace("{#jobNo#}", Convert.ToString(jobNo));
                sb.Replace("{#drawingNo#}", Convert.ToString(drawingNo));
                sb.Replace("{#equipmentNo#}", Convert.ToString(equipmentNo));
                sb.Replace("{#tagNo#}", Convert.ToString(tagNo));
                sb.Replace("{#itemName#}", Convert.ToString(itemName));
                sb.Replace("{#itemDetail#}", Convert.ToString(itemDetail));
                sb.Replace("{#expectedDateOfCompByPlanning#}", Convert.ToString(expectedDateOfCompByPlanning));
                sb.Replace("{#status#}", Convert.ToString(status));
                sb.Replace("{#quantity#}", Convert.ToString(quantity));
                sb.Replace("{#assignedBy#}", Convert.ToString(assignedBy));
                sb.Replace("{#assignedOn#}", Convert.ToString(assignedOn));
                sb.Replace("{#assignedRemarks#}", Convert.ToString(assignedRemarks));
                sb.Replace("{#acceptedBy#}", Convert.ToString(acceptedBy));
                sb.Replace("{#acceptedOn#}", Convert.ToString(acceptedOn));
                sb.Replace("{#acceptedRemarks#}", Convert.ToString(acceptedRemarks));

                sb.Replace("{#rejectedBy#}", Convert.ToString(rejectedBy));
                sb.Replace("{#rejectedOn#}", Convert.ToString(rejectedOn));
                sb.Replace("{#rejectedRemarks#}", Convert.ToString(rejectedRemarks));

                sb.Replace("{#reAssignedBy#}", Convert.ToString(reAssignedBy));
                sb.Replace("{#reAssignedOn#}", Convert.ToString(reAssignedOn));
                sb.Replace("{#reAssignedRemarks#}", Convert.ToString(reAssignedRemarks));

                sb.Replace("{#scheduledBy#}", Convert.ToString(scheduledBy));
                sb.Replace("{#scheduledOn#}", Convert.ToString(scheduledOn));
                sb.Replace("{#scheduledRemarks#}", Convert.ToString(scheduledRemarks));


                sb.Replace("{#inspectedBy#}", Convert.ToString(inspectedBy));
                sb.Replace("{#inspectedOn#}", Convert.ToString(inspectedOn));
                sb.Replace("{#totalAcceptedQuantity#}", Convert.ToString(totalAcceptedQuantity));
                sb.Replace("{#totalRejectedQuantity#}", Convert.ToString(totalRejectedQuantity));


                sb.Replace("{#reScheduledBy#}", Convert.ToString(reScheduledBy));
                sb.Replace("{#reScheduledOn#}", Convert.ToString(reScheduledOn));
                sb.Replace("{#reScheduledRemarks#}", Convert.ToString(reScheduledRemarks));


                sb.Replace("{#completedBy#}", Convert.ToString(completedBy));
                sb.Replace("{#completedOn#}", Convert.ToString(completedOn));
                sb.Replace("{#completedRemarks#}", Convert.ToString(completedRemarks));
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