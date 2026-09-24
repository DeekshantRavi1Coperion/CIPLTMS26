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
public class DMSHtmlForPDFForMail
{
    int recordID = 0;
    string jobNo = string.Empty;
    string designCategory = string.Empty;
    string description = string.Empty;
    string UOM = string.Empty;
    string quantity = string.Empty;
    string reqdDateByProjectTeam = string.Empty;
    string plannedStartDateByDesignTeam = string.Empty;
    string plannedCompletionDateByDesignTeam = string.Empty;
    string drawingNo = string.Empty;

    string clientDrawingNo = string.Empty;
    string contractorDrawingNo = string.Empty;

    string documentLink = string.Empty;
    string drawingLink = string.Empty;
    string drawingRevNo = string.Empty;
    string workingStatus = string.Empty;
    string spentHours = string.Empty;
    string expectedCompletionDate = string.Empty;
    string designResponsibleEngg = string.Empty;
    string designChecker = string.Empty;
    int statusID = 0;
    string status = string.Empty;
    string isRevised = string.Empty;
    int amendmentCount = 0;
    int amendmentFlag = 0;
    int editedFlag = 0;
    string remarks = string.Empty;


    public DMSHtmlForPDFForMail()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public string GetHtmlForPDF(DataTable dtDrawingDetail, int createdByID, int designEnggID, int designCheckerID, int statusID, string JOBNo)
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
            plannedStartDateByDesignTeam = string.Empty;
            plannedCompletionDateByDesignTeam = string.Empty;
            drawingNo = string.Empty;

            clientDrawingNo = string.Empty;
            contractorDrawingNo = string.Empty;

            documentLink = string.Empty;
            drawingLink = string.Empty;
            drawingRevNo = string.Empty;
            workingStatus = string.Empty;
            spentHours = string.Empty;
            expectedCompletionDate = string.Empty;
            designResponsibleEngg = string.Empty;
            designChecker = string.Empty;
            status = string.Empty;
            isRevised = string.Empty;
            amendmentCount = 0;
            amendmentFlag = 0;
            editedFlag = 0;
            remarks = string.Empty;

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();



            if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
            {
                if (dtDrawingDetail.Rows.Count > 0)
                {

                    sb.Append("<h2 class='header2' align='center'>Drawing(s) Detail of JOB No.- {#JOBNoHeader#}</h2>\n");
                    sb.Append("<hr />\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trDMS'>\n");
                    sb.Append("<th class='tdJOBNoDMS'>JOB No.</th>\n");
                    sb.Append("<th class='tdDrawingNoDMS'>Drawing No.</th>\n");

                    sb.Append("<th class='tdDrawingNoDMS'>Client Drawing No.</th>\n");
                    sb.Append("<th class='tdDrawingNoDMS'>Contractor Drawing No.</th>\n");

                    sb.Append("<th class='tdDescriptionDMS'>Description</th>\n");
                    sb.Append("<th class='tdRevNoDMS'>Revision No.</th>\n");
                    sb.Append("<th class='tdCategoryDMS'>Category</th>\n");
                    sb.Append("<th class='tdUOMDMS'>UOM</th>\n");
                    sb.Append("<th class='tdQuantityDMS'>Quantity</th>\n");
                    sb.Append("<th class='tdRDProjectTeamDMS'>Reqd Date By Project Team</th>\n");
                    sb.Append("<th class='tdStatusDMS'>Status</th>\n");
                    sb.Append("<th class='tdRemarksDMS'>Remarks</th>\n");
                    sb.Append("</tr>\n");

                    sb.Replace("{#JOBNoHeader#}", JOBNo);


                    foreach (DataRow dr in dtDrawingDetail.Rows)
                    {
                        if (dr["RECORD_ID"] != DBNull.Value && Convert.ToInt32(dr["RECORD_ID"]) > 0)
                            recordID = Convert.ToInt32(dr["RECORD_ID"]);

                        if (dr["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                            jobNo = Convert.ToString(dr["JOB_NO"]);

                        if (dr["DESIGN_CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CATEGORY"])))
                            designCategory = Convert.ToString(dr["DESIGN_CATEGORY"]);

                        if (dr["DESCRIPTION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                            description = Convert.ToString(dr["DESCRIPTION"]);

                        if (dr["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
                            UOM = Convert.ToString(dr["UOM"]);

                        if (dr["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["QUANTITY"])))
                            quantity = Convert.ToString(dr["QUANTITY"]);

                        if (dr["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"])))
                            reqdDateByProjectTeam = Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"]);


                        if (dr["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
                            drawingNo = Convert.ToString(dr["DRAWING_NO"]);

                        if (dr["CLIENT_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CLIENT_DRAWING_NO"])))
                            clientDrawingNo = Convert.ToString(dr["CLIENT_DRAWING_NO"]);

                        if (dr["CONTRACTOR_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CONTRACTOR_DRAWING_NO"])))
                            contractorDrawingNo = Convert.ToString(dr["CONTRACTOR_DRAWING_NO"]);


                        if (dr["DRAWING_REV_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_REV_NO"])))
                            drawingRevNo = Convert.ToString(dr["DRAWING_REV_NO"]);


                        if (dr["STATUS_ID"] != DBNull.Value && Convert.ToInt32(dr["STATUS_ID"]) > 0)
                            statusID = Convert.ToInt32(dr["STATUS_ID"]);

                        if (dr["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["STATUS"])))
                            status = Convert.ToString(dr["STATUS"]);


                        if (dr["AMENDMENT_COUNT"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_COUNT"]) > 0)
                            amendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);


                        if (dr["AMENDMENT_FLAG"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_FLAG"]) > 0)
                            amendmentFlag = Convert.ToInt32(dr["AMENDMENT_FLAG"]);

                        if (dr["EDITED_FLAG"] != DBNull.Value && Convert.ToInt32(dr["EDITED_FLAG"]) > 0)
                            editedFlag = Convert.ToInt32(dr["EDITED_FLAG"]);


                        if (amendmentCount == 0)
                        {
                            if (dr["REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])))
                                remarks = Convert.ToString(dr["REMARKS"]);
                        }
                        else
                        {
                            if (dr["AMENDED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AMENDED_REMARKS"])))
                                remarks = Convert.ToString(dr["AMENDED_REMARKS"]);

                            if (amendmentFlag > 0)
                            {
                                if (dr["SENT_TO_AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SENT_TO_AMENDMENT_REMARKS"])))
                                    remarks = Convert.ToString(dr["SENT_TO_AMENDMENT_REMARKS"]);
                            }

                            if (editedFlag > 0)
                            {
                                if (dr["REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])))
                                    remarks = Convert.ToString(dr["REMARKS"]);
                            }
                        }


                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdJOBNoDMS'>{#jobNo#}</td>\n");
                        sb.Append("<td class='tdDrawingNoDMS'>{#drawingNo#}</td>\n");
                        
                        sb.Append("<td class='tdDrawingNoDMS'>{#clientDrawingNo#}</td>\n");
                        sb.Append("<td class='tdDrawingNoDMS'>{#contractorDrawingNo#}</td>\n");

                        sb.Append("<td class='tdDescriptionDMS'>{#description#}</td>\n");
                        sb.Append("<td class='tdRevNoDMS'>{#drawingRevNo#}</td>\n");
                        sb.Append("<td class='tdCategoryDMS'>{#designCategory#}</td>\n");
                        sb.Append("<td class='tdUOMDMS'>{#UOM#}</td>\n");
                        sb.Append("<td class='tdQuantityDMS'>{#quantity#}</td>\n");
                        sb.Append("<td class='tdRDProjectTeamDMS'>{#reqdDateByProjectTeam#}</td>\n");
                        sb.Append("<td class='tdStatusDMS'>{#status#}</td>\n");
                        sb.Append("<td class='tdRemarksDMS'>{#Remarks#}</td>\n");
                        sb.Append("</tr>\n");


                        sb.Replace("{#jobNo#}", jobNo);
                        sb.Replace("{#drawingNo#}", drawingNo);


                        sb.Replace("{#clientDrawingNo#}", clientDrawingNo);
                        sb.Replace("{#contractorDrawingNo#}", contractorDrawingNo);


                        sb.Replace("{#description#}", description);
                        sb.Replace("{#drawingRevNo#}", drawingRevNo);
                        sb.Replace("{#designCategory#}", designCategory);
                        sb.Replace("{#UOM#}", UOM);
                        sb.Replace("{#quantity#}", quantity);
                        sb.Replace("{#reqdDateByProjectTeam#}", reqdDateByProjectTeam);
                        sb.Replace("{#status#}", status);
                        sb.Replace("{#Remarks#}", remarks);

                    }

                    sb.Append("</table>\n");
                }
            }

            else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
            {
                if (dtDrawingDetail.Rows.Count > 0)
                {
                    sb.Append("<h2 class='header2' align='center'>Drawing(s) Detail of JOB No.- {#JOBNoHeader#}</h2>\n");
                    sb.Append("<hr />\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");

                    sb.Append("<tr class='trDMS'>\n");
                    sb.Append("<th class='tdJOBNoDMS'>JOB No.</th>\n");
                    sb.Append("<th class='tdDrawingNoDMS'>Drawing No.</th>\n");

                    sb.Append("<th class='tdDrawingNoDMS'>Client Drawing No.</th>\n");
                    sb.Append("<th class='tdDrawingNoDMS'>Contractor Drawing No.</th>\n");

                    sb.Append("<th class='tdDescriptionDMS'>Description</th>\n");
                    sb.Append("<th class='tdRevNoDMS'>Revision No.</th>\n");
                    sb.Append("<th class='tdCategoryDMS'>Category</th>\n");
                    sb.Append("<th class='tdUOMDMS'>UOM</th>\n");
                    sb.Append("<th class='tdQuantityDMS'>Quantity</th>\n");
                    sb.Append("<th class='tdRDProjectTeamDMS'>Reqd Date By Project Team</th>\n");
                    sb.Append("<th class='tdEDCompletionDMS'>Expected Completion Date</th>\n");
                    sb.Append("<th class='tdPDDesignTeamDMS'>Planned Start Date by Design Team</th>\n");
                    sb.Append("<th class='tdPDDesignTeamDMS'>Planned Completion Date by Design Team</th>\n");
                    sb.Append("<th class='tdStatusDMS'>Status</th>\n");
                    sb.Append("<th class='tdRemarksDMS'>Remarks</th>\n");
                    sb.Append("</tr>\n");

                    sb.Replace("{#JOBNoHeader#}", JOBNo);

                    foreach (DataRow dr in dtDrawingDetail.Select("DESIGN_RESPONSIBLE_ENGG_ID='" + designEnggID + "'"))
                    {
                        if (dr["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                            jobNo = Convert.ToString(dr["JOB_NO"]);

                        if (dr["DESIGN_CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CATEGORY"])))
                            designCategory = Convert.ToString(dr["DESIGN_CATEGORY"]);

                        if (dr["DESCRIPTION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                            description = Convert.ToString(dr["DESCRIPTION"]);

                        if (dr["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
                            UOM = Convert.ToString(dr["UOM"]);

                        if (dr["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["QUANTITY"])))
                            quantity = Convert.ToString(dr["QUANTITY"]);

                        if (dr["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"])))
                            reqdDateByProjectTeam = Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"]);

                        if (dr["PLANNED_START_DATE_BY_DESIGN_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PLANNED_START_DATE_BY_DESIGN_TEAM"])))
                            plannedStartDateByDesignTeam = Convert.ToString(dr["PLANNED_START_DATE_BY_DESIGN_TEAM"]);

                        if (dr["EXPECTED_COMPLETION_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EXPECTED_COMPLETION_DATE"])))
                            expectedCompletionDate = Convert.ToString(dr["EXPECTED_COMPLETION_DATE"]);

                        if (dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"])))
                            plannedCompletionDateByDesignTeam = Convert.ToString(dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"]);

                        if (dr["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
                            drawingNo = Convert.ToString(dr["DRAWING_NO"]);

                        if (dr["CLIENT_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CLIENT_DRAWING_NO"])))
                            clientDrawingNo = Convert.ToString(dr["CLIENT_DRAWING_NO"]);

                        if (dr["CONTRACTOR_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CONTRACTOR_DRAWING_NO"])))
                            contractorDrawingNo = Convert.ToString(dr["CONTRACTOR_DRAWING_NO"]);


                        if (dr["DRAWING_REV_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_REV_NO"])))
                            drawingRevNo = Convert.ToString(dr["DRAWING_REV_NO"]);


                        if (dr["STATUS_ID"] != DBNull.Value && Convert.ToInt32(dr["STATUS_ID"]) > 0)
                            statusID = Convert.ToInt32(dr["STATUS_ID"]);

                        if (dr["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["STATUS"])))
                            status = Convert.ToString(dr["STATUS"]);



                        if (dr["AMENDMENT_COUNT"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_COUNT"]) > 0)
                            amendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);



                        if (dr["AMENDMENT_FLAG"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_FLAG"]) > 0)
                            amendmentFlag = Convert.ToInt32(dr["AMENDMENT_FLAG"]);

                        if (dr["EDITED_FLAG"] != DBNull.Value && Convert.ToInt32(dr["EDITED_FLAG"]) > 0)
                            editedFlag = Convert.ToInt32(dr["EDITED_FLAG"]);


                        if (amendmentCount == 0)
                        {
                            if (dr["OPEN_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["OPEN_REMARKS"])))
                                remarks = Convert.ToString(dr["OPEN_REMARKS"]);
                        }
                        else
                        {
                            if (dr["AMENDED_OPEN_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AMENDED_OPEN_REMARKS"])))
                                remarks = Convert.ToString(dr["AMENDED_OPEN_REMARKS"]);


                            if (amendmentFlag > 0)
                            {
                                if (dr["SENT_TO_AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SENT_TO_AMENDMENT_REMARKS"])))
                                    remarks = Convert.ToString(dr["SENT_TO_AMENDMENT_REMARKS"]);
                            }

                            if (editedFlag > 0)
                            {
                                if (dr["OPEN_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["OPEN_REMARKS"])))
                                    remarks = Convert.ToString(dr["OPEN_REMARKS"]);
                            }
                        }

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdJOBNoDMS'>{#jobNo#}</td>\n");
                        sb.Append("<td class='tdDrawingNoDMS'>{#drawingNo#}</td>\n");

                        sb.Append("<td class='tdDrawingNoDMS'>{#clientDrawingNo#}</td>\n");
                        sb.Append("<td class='tdDrawingNoDMS'>{#contractorDrawingNo#}</td>\n");

                        sb.Append("<td class='tdDescriptionDMS'>{#description#}</td>\n");
                        sb.Append("<td class='tdRevNoDMS'>{#drawingRevNo#}</td>\n");
                        sb.Append("<td class='tdCategoryDMS'>{#designCategory#}</td>\n");
                        sb.Append("<td class='tdUOMDMS'>{#UOM#}</td>\n");
                        sb.Append("<td class='tdQuantityDMS'>{#quantity#}</td>\n");
                        sb.Append("<td class='tdRDProjectTeamDMS'>{#reqdDateByProjectTeam#}</td>\n");
                        sb.Append("<td class='tdEDCompletionDMS'>{#expectedCompletionDate#}</td>\n");
                        sb.Append("<td class='tdPDDesignTeamDMS'>{#plannedStartDateByDesignTeam#}</td>\n");
                        sb.Append("<td class='tdPDDesignTeamDMS'>{#plannedCompletionDateByDesignTeam#}</td>\n");
                        sb.Append("<td class='tdStatusDMS'>{#status#}</td>\n");
                        sb.Append("<td class='tdRemarksDMS'>{#Remarks#}</td>\n");
                        sb.Append("</tr>\n");


                        sb.Replace("{#jobNo#}", jobNo);
                        sb.Replace("{#drawingNo#}", drawingNo);

                        sb.Replace("{#clientDrawingNo#}", clientDrawingNo);
                        sb.Replace("{#contractorDrawingNo#}", contractorDrawingNo);

                        sb.Replace("{#designCategory#}", designCategory);
                        sb.Replace("{#description#}", description);
                        sb.Replace("{#UOM#}", UOM);
                        sb.Replace("{#quantity#}", quantity);
                        sb.Replace("{#reqdDateByProjectTeam#}", reqdDateByProjectTeam);
                        sb.Replace("{#expectedCompletionDate#}", expectedCompletionDate);
                        sb.Replace("{#plannedStartDateByDesignTeam#}", plannedStartDateByDesignTeam);
                        sb.Replace("{#plannedCompletionDateByDesignTeam#}", plannedCompletionDateByDesignTeam);
                        sb.Replace("{#drawingRevNo#}", drawingRevNo);
                        sb.Replace("{#status#}", status);
                        sb.Replace("{#Remarks#}", remarks);
                    }

                    sb.Append("</table>\n");
                }
            }

            else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
            {
                if (dtDrawingDetail.Rows.Count > 0)
                {
                    sb.Append("<h2 class='header2' align='center'>Drawing(s) Detail of JOB No.- {#JOBNoHeader#}</h2>\n");
                    sb.Append("<hr />\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");

                    sb.Append("<tr class='trDMS'>\n");
                    sb.Append("<th class='tdJOBNoDMS'>JOB No.</th>\n");
                    sb.Append("<th class='tdDrawingNoDMS'>Drawing No.</th>\n");

                    sb.Append("<th class='tdDrawingNoDMS'>Client Drawing No.</th>\n");
                    sb.Append("<th class='tdDrawingNoDMS'>Contractor Drawing No.</th>\n");

                    sb.Append("<th class='tdDescriptionDMS'>Description</th>\n");
                    sb.Append("<th class='tdRevNoDMS'>Revision No.</th>\n");
                    sb.Append("<th class='tdCategoryDMS'>Category</th>\n");
                    sb.Append("<th class='tdUOMDMS'>UOM</th>\n");
                    sb.Append("<th class='tdQuantityDMS'>Quantity</th>\n");
                    sb.Append("<th class='tdRDProjectTeamDMS'>Reqd Date By Project Team</th>\n");
                    sb.Append("<th class='tdPDDesignTeamDMS'>Planned Start Date by Design Team</th>\n");
                    sb.Append("<th class='tdPDDesignTeamDMS'>Planned Completion Date by Design Team</th>\n");
                    sb.Append("<th class='tdDocumentLinkDMS'>Document Link</th>\n");
                    sb.Append("<th class='tdDocumentLinkDMS'>Drawing Link</th>\n");
                    sb.Append("<th class='tdDesignEnggDMS'>Design Responsible Engg</th>\n");
                    sb.Append("<th class='tdEDCompletionDMS'>Expected Completion Date</th>\n");
                    //sb.Append("<th class='tdWorkingStatusDMS'>Working Status</th>\n");
                    //sb.Append("<th class='tdHourSpentDMS'>Spent Hours</th>\n");
                    sb.Append("<th class='tdStatusDMS'>Status</th>\n");
                    sb.Append("<th class='tdRemarksDMS'>Remarks</th>\n");
                    sb.Append("</tr>\n");

                    sb.Replace("{#JOBNoHeader#}", JOBNo);

                    foreach (DataRow dr in dtDrawingDetail.Select("DESIGN_CHECKER_ID='" + designCheckerID + "'"))
                    {

                        if (dr["RECORD_ID"] != DBNull.Value && Convert.ToInt32(dr["RECORD_ID"]) > 0)
                            recordID = Convert.ToInt32(dr["RECORD_ID"]);

                        if (dr["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                            jobNo = Convert.ToString(dr["JOB_NO"]);

                        if (dr["DESIGN_CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CATEGORY"])))
                            designCategory = Convert.ToString(dr["DESIGN_CATEGORY"]);

                        if (dr["DESCRIPTION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                            description = Convert.ToString(dr["DESCRIPTION"]);

                        if (dr["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
                            UOM = Convert.ToString(dr["UOM"]);

                        if (dr["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["QUANTITY"])))
                            quantity = Convert.ToString(dr["QUANTITY"]);

                        if (dr["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"])))
                            reqdDateByProjectTeam = Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"]);

                        if (dr["PLANNED_START_DATE_BY_DESIGN_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PLANNED_START_DATE_BY_DESIGN_TEAM"])))
                            plannedStartDateByDesignTeam = Convert.ToString(dr["PLANNED_START_DATE_BY_DESIGN_TEAM"]);

                        if (dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"])))
                            plannedCompletionDateByDesignTeam = Convert.ToString(dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"]);

                        if (dr["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
                            drawingNo = Convert.ToString(dr["DRAWING_NO"]);

                        if (dr["CLIENT_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CLIENT_DRAWING_NO"])))
                            clientDrawingNo = Convert.ToString(dr["CLIENT_DRAWING_NO"]);

                        if (dr["CONTRACTOR_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CONTRACTOR_DRAWING_NO"])))
                            contractorDrawingNo = Convert.ToString(dr["CONTRACTOR_DRAWING_NO"]);


                        if (dr["DOCUMENT_LINK"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DOCUMENT_LINK"])))
                            documentLink = Convert.ToString(dr["DOCUMENT_LINK"]);

                        if (dr["DRAWING_LINK"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_LINK"])))
                            drawingLink = Convert.ToString(dr["DRAWING_LINK"]);

                        if (dr["DRAWING_REV_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_REV_NO"])))
                            drawingRevNo = Convert.ToString(dr["DRAWING_REV_NO"]);

                        if (dr["WORKING_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["WORKING_STATUS"])))
                            workingStatus = Convert.ToString(dr["WORKING_STATUS"]);

                        if (dr["EXPECTED_COMPLETION_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EXPECTED_COMPLETION_DATE"])))
                            expectedCompletionDate = Convert.ToString(dr["EXPECTED_COMPLETION_DATE"]);

                        if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                            designResponsibleEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                        if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                            designResponsibleEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                        if (dr["STATUS_ID"] != DBNull.Value && Convert.ToInt32(dr["STATUS_ID"]) > 0)
                            statusID = Convert.ToInt32(dr["STATUS_ID"]);

                        if (dr["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["STATUS"])))
                            status = Convert.ToString(dr["STATUS"]);

                        if (dr["IS_REVISED"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["IS_REVISED"])))
                            isRevised = Convert.ToString(dr["IS_REVISED"]);

                        if (dr["AMENDMENT_COUNT"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_COUNT"]) > 0)
                            amendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);


                        if (dr["AMENDMENT_FLAG"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_FLAG"]) > 0)
                            amendmentFlag = Convert.ToInt32(dr["AMENDMENT_FLAG"]);

                        if (dr["EDITED_FLAG"] != DBNull.Value && Convert.ToInt32(dr["EDITED_FLAG"]) > 0)
                            editedFlag = Convert.ToInt32(dr["EDITED_FLAG"]);


                        if (amendmentCount == 0)
                        {
                            if (dr["SENT_TO_CHECKING_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SENT_TO_CHECKING_REMARKS"])))
                                remarks = Convert.ToString(dr["SENT_TO_CHECKING_REMARKS"]);
                        }
                        else
                        {
                            if (dr["AMENDED_SENT_TO_CHECKING_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AMENDED_SENT_TO_CHECKING_REMARKS"])))
                                remarks = Convert.ToString(dr["AMENDED_SENT_TO_CHECKING_REMARKS"]);
                        }

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdJOBNoDMS'>{#jobNo#}</td>\n");
                        sb.Append("<td class='tdDrawingNoDMS'>{#drawingNo#}</td>\n");

                        sb.Append("<td class='tdDrawingNoDMS'>{#clientDrawingNo#}</td>\n");
                        sb.Append("<td class='tdDrawingNoDMS'>{#contractorDrawingNo#}</td>\n");

                        sb.Append("<td class='tdDescriptionDMS'>{#description#}</td>\n");
                        sb.Append("<td class='tdRevNoDMS'>{#drawingRevNo#}</td>\n");
                        sb.Append("<td class='tdCategoryDMS'>{#designCategory#}</td>\n");
                        sb.Append("<td class='tdUOMDMS'>{#UOM#}</td>\n");
                        sb.Append("<td class='tdQuantityDMS'>{#quantity#}</td>\n");
                        sb.Append("<td class='tdRDProjectTeamDMS'>{#reqdDateByProjectTeam#}</td>\n");
                        sb.Append("<td class='tdPDDesignTeamDMS'>{#plannedStartDateByDesignTeam#}</td>\n");
                        sb.Append("<td class='tdPDDesignTeamDMS'>{#plannedCompletionDateByDesignTeam#}</td>\n");
                        sb.Append("<td class='tdDocumentLinkDMS'>{#documentLink#}</td>\n");
                        sb.Append("<td class='tdDocumentLinkDMS'>{#drawingLink#}</td>\n");
                        sb.Append("<td class='tdDesignEnggDMS'>{#designResponsibleEngg#}</td>\n");
                        sb.Append("<td class='tdEDCompletionDMS'>{#expectedCompletionDate#}</td>\n");
                        //sb.Append("<td class='tdWorkingStatusDMS'>{#workingstatus#}</td>\n");
                        //sb.Append("<td class='tdHourSpentDMS'>{#spenthours#}</td>\n");
                        sb.Append("<td class='tdStatusDMS'>{#status#}</td>\n");
                        sb.Append("<td class='tdRemarksDMS'>{#Remarks#}</td>\n");
                        sb.Append("</tr>\n");


                        sb.Replace("{#drawingNo#}", drawingNo);

                        sb.Replace("{#clientDrawingNo#}", clientDrawingNo);
                        sb.Replace("{#contractorDrawingNo#}", contractorDrawingNo);

                        sb.Replace("{#jobNo#}", jobNo);
                        sb.Replace("{#designCategory#}", designCategory);
                        sb.Replace("{#description#}", description);
                        sb.Replace("{#UOM#}", UOM);
                        sb.Replace("{#quantity#}", quantity);
                        sb.Replace("{#reqdDateByProjectTeam#}", reqdDateByProjectTeam);
                        sb.Replace("{#plannedStartDateByDesignTeam#}", plannedStartDateByDesignTeam);
                        sb.Replace("{#plannedCompletionDateByDesignTeam#}", plannedCompletionDateByDesignTeam);
                        sb.Replace("{#documentLink#}", documentLink);
                        sb.Replace("{#drawingLink#}", drawingLink);
                        sb.Replace("{#isRevised#}", isRevised);
                        sb.Replace("{#drawingRevNo#}", drawingRevNo);
                        sb.Replace("{#designResponsibleEngg#}", designResponsibleEngg);
                        sb.Replace("{#expectedCompletionDate#}", expectedCompletionDate);
                        sb.Replace("{#status#}", status);
                        //sb.Replace("{#workingstatus#}", workingStatus);
                        //sb.Replace("{#designChecker#}", designChecker);
                        //sb.Replace("{#spenthours#}", spentHours);
                        sb.Replace("{#Remarks#}", remarks);
                    }

                    sb.Append("</table>\n");
                }
            }

            else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
            {
                if (dtDrawingDetail.Rows.Count > 0)
                {
                    sb.Append("<h2 class='header2' align='center'>Drawing(s) Detail of JOB No.- {#JOBNoHeader#}</h2>\n");
                    sb.Append("<hr />\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");

                    sb.Append("<tr class='trDMS'>\n");
                    sb.Append("<th class='tdJOBNoDMS'>JOB No.</th>\n");
                    sb.Append("<th class='tdDrawingNoDMS'>Drawing No.</th>\n");

                    sb.Append("<th class='tdDrawingNoDMS'>Client Drawing No.</th>\n");
                    sb.Append("<th class='tdDrawingNoDMS'>Contractor Drawing No.</th>\n");

                    sb.Append("<th class='tdDescriptionDMS'>Description</th>\n");
                    sb.Append("<th class='tdRevNoDMS'>Revision No.</th>\n");
                    sb.Append("<th class='tdCategoryDMS'>Category</th>\n");
                    sb.Append("<th class='tdUOMDMS'>UOM</th>\n");
                    sb.Append("<th class='tdQuantityDMS'>Quantity</th>\n");
                    sb.Append("<th class='tdRDProjectTeamDMS'>Reqd Date By Project Team</th>\n");
                    sb.Append("<th class='tdPDDesignTeamDMS'>Planned Start Date by Design Team</th>\n");
                    sb.Append("<th class='tdPDDesignTeamDMS'>Planned Completion Date by Design Team</th>\n");
                    sb.Append("<th class='tdDesignEnggDMS'>Design Responsible Engg</th>\n");
                    sb.Append("<th class='tdEDCompletionDMS'>Expected Completion Date</th>\n");
                    sb.Append("<th class='tdWorkingStatusDMS'>Working Status</th>\n");
                    sb.Append("<th class='tdStatusDMS'>Status</th>\n");
                    sb.Append("<th class='tdRemarksDMS'>Remarks</th>\n");
                    sb.Append("</tr>\n");

                    sb.Replace("{#JOBNoHeader#}", JOBNo);

                    foreach (DataRow dr in dtDrawingDetail.Select("DESIGN_RESPONSIBLE_ENGG_ID='" + designEnggID + "'"))
                    {
                        if (dr["RECORD_ID"] != DBNull.Value && Convert.ToInt32(dr["RECORD_ID"]) > 0)
                            recordID = Convert.ToInt32(dr["RECORD_ID"]);

                        if (dr["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                            jobNo = Convert.ToString(dr["JOB_NO"]);

                        if (dr["DESIGN_CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CATEGORY"])))
                            designCategory = Convert.ToString(dr["DESIGN_CATEGORY"]);

                        if (dr["DESCRIPTION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                            description = Convert.ToString(dr["DESCRIPTION"]);

                        if (dr["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
                            UOM = Convert.ToString(dr["UOM"]);

                        if (dr["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["QUANTITY"])))
                            quantity = Convert.ToString(dr["QUANTITY"]);

                        if (dr["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"])))
                            reqdDateByProjectTeam = Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"]);

                        if (dr["PLANNED_START_DATE_BY_DESIGN_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PLANNED_START_DATE_BY_DESIGN_TEAM"])))
                            plannedStartDateByDesignTeam = Convert.ToString(dr["PLANNED_START_DATE_BY_DESIGN_TEAM"]);

                        if (dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"])))
                            plannedCompletionDateByDesignTeam = Convert.ToString(dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"]);

                        if (dr["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
                            drawingNo = Convert.ToString(dr["DRAWING_NO"]);

                        if (dr["CLIENT_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CLIENT_DRAWING_NO"])))
                            clientDrawingNo = Convert.ToString(dr["CLIENT_DRAWING_NO"]);

                        if (dr["CONTRACTOR_DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CONTRACTOR_DRAWING_NO"])))
                            contractorDrawingNo = Convert.ToString(dr["CONTRACTOR_DRAWING_NO"]);

                        if (dr["DOCUMENT_LINK"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DOCUMENT_LINK"])))
                            documentLink = Convert.ToString(dr["DOCUMENT_LINK"]);

                        if (dr["DRAWING_REV_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_REV_NO"])))
                            drawingRevNo = Convert.ToString(dr["DRAWING_REV_NO"]);

                        if (dr["WORKING_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["WORKING_STATUS"])))
                            workingStatus = Convert.ToString(dr["WORKING_STATUS"]);

                        if (dr["EXPECTED_COMPLETION_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EXPECTED_COMPLETION_DATE"])))
                            expectedCompletionDate = Convert.ToString(dr["EXPECTED_COMPLETION_DATE"]);

                        if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                            designResponsibleEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                        if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                            designResponsibleEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                        if (dr["STATUS_ID"] != DBNull.Value && Convert.ToInt32(dr["STATUS_ID"]) > 0)
                            statusID = Convert.ToInt32(dr["STATUS_ID"]);

                        if (dr["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["STATUS"])))
                            status = Convert.ToString(dr["STATUS"]);

                        if (dr["IS_REVISED"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["IS_REVISED"])))
                            isRevised = Convert.ToString(dr["IS_REVISED"]);

                        if (dr["AMENDMENT_COUNT"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_COUNT"]) > 0)
                            amendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);


                        if (dr["AMENDMENT_FLAG"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_FLAG"]) > 0)
                            amendmentFlag = Convert.ToInt32(dr["AMENDMENT_FLAG"]);

                        if (dr["EDITED_FLAG"] != DBNull.Value && Convert.ToInt32(dr["EDITED_FLAG"]) > 0)
                            editedFlag = Convert.ToInt32(dr["EDITED_FLAG"]);


                        if (amendmentCount == 0)
                        {
                            if (dr["CHECKED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CHECKED_REMARKS"])))
                                remarks = Convert.ToString(dr["CHECKED_REMARKS"]);
                        }
                        else
                        {
                            if (dr["AMENDED_CHECKED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AMENDED_CHECKED_REMARKS"])))
                                remarks = Convert.ToString(dr["AMENDED_CHECKED_REMARKS"]);
                        }

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdJOBNoDMS'>{#jobNo#}</td>\n");
                        sb.Append("<td class='tdDrawingNoDMS'>{#drawingNo#}</td>\n");

                        sb.Append("<td class='tdDrawingNoDMS'>{#clientDrawingNo#}</td>\n");
                        sb.Append("<td class='tdDrawingNoDMS'>{#contractorDrawingNo#}</td>\n");

                        sb.Append("<td class='tdDescriptionDMS'>{#description#}</td>\n");
                        sb.Append("<td class='tdRevNoDMS'>{#drawingRevNo#}</td>\n");
                        sb.Append("<td class='tdCategoryDMS'>{#designCategory#}</td>\n");
                        sb.Append("<td class='tdUOMDMS'>{#UOM#}</td>\n");
                        sb.Append("<td class='tdQuantityDMS'>{#quantity#}</td>\n");
                        sb.Append("<td class='tdRDProjectTeamDMS'>{#reqdDateByProjectTeam#}</td>\n");
                        sb.Append("<td class='tdPDDesignTeamDMS'>{#plannedStartDateByDesignTeam#}</td>\n");
                        sb.Append("<td class='tdPDDesignTeamDMS'>{#plannedCompletionDateByDesignTeam#}</td>\n");
                        sb.Append("<td class='tdDesignEnggDMS'>{#designResponsibleEngg#}</td>\n");
                        sb.Append("<td class='tdEDCompletionDMS'>{#expectedCompletionDate#}</td>\n");
                        sb.Append("<td class='tdWorkingStatusDMS'>{#workingstatus#}</td>\n");
                        sb.Append("<td class='tdStatusDMS'>{#status#}</td>\n");
                        sb.Append("<td class='tdRemarksDMS'>{#Remarks#}</td>\n");
                        sb.Append("</tr>\n");


                        sb.Replace("{#drawingNo#}", drawingNo);

                        sb.Replace("{#clientDrawingNo#}", clientDrawingNo);
                        sb.Replace("{#contractorDrawingNo#}", contractorDrawingNo);

                        sb.Replace("{#jobNo#}", jobNo);
                        sb.Replace("{#designCategory#}", designCategory);
                        sb.Replace("{#description#}", description);
                        sb.Replace("{#UOM#}", UOM);
                        sb.Replace("{#quantity#}", quantity);
                        sb.Replace("{#reqdDateByProjectTeam#}", reqdDateByProjectTeam);
                        sb.Replace("{#plannedStartDateByDesignTeam#}", plannedStartDateByDesignTeam);
                        sb.Replace("{#plannedCompletionDateByDesignTeam#}", plannedCompletionDateByDesignTeam);
                        sb.Replace("{#isRevised#}", isRevised);
                        sb.Replace("{#drawingRevNo#}", drawingRevNo);
                        sb.Replace("{#designResponsibleEngg#}", designResponsibleEngg);
                        sb.Replace("{#expectedCompletionDate#}", expectedCompletionDate);
                        sb.Replace("{#status#}", status);
                        sb.Replace("{#workingstatus#}", workingStatus);
                        sb.Replace("{#designChecker#}", designChecker);
                        sb.Replace("{#spenthours#}", spentHours);
                        sb.Replace("{#Remarks#}", remarks);
                    }

                    sb.Append("</table>\n");
                }
            }

            //else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Closed))
            //{
            //    if (dtDrawingDetail.Rows.Count > 0)
            //    {
            //        sb.Append("<h4 class='header2' align='center'>Drawing Detail</h4>\n");
            //        sb.Append("<hr />\n");

            //        sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");

            //        sb.Append("<tr class='trDMS'>\n");
            //        sb.Append("<th class='tdJOBNoDMS'>JOB No.</th>\n");
            //        sb.Append("<th class='tdDrawingNoDMS'>Drawing No.</th>\n");
            //        sb.Append("<th class='tdDescriptionDMS'>Description</th>\n");
            //        sb.Append("<th class='tdRevNoDMS'>Revision No.</th>\n");
            //        sb.Append("<th class='tdCategoryDMS'>Category</th>\n");
            //        sb.Append("<th class='tdUOMDMS'>UOM</th>\n");
            //        sb.Append("<th class='tdQuantityDMS'>Quantity</th>\n");
            //        sb.Append("<th class='tdRDProjectTeamDMS'>Reqd Date By Project Team</th>\n");
            //        sb.Append("<th class='tdStatusDMS'>Status</th>\n");
            //        sb.Append("<th class='tdRemarksDMS'>Remarks</th>\n");
            //        sb.Append("</tr>\n");


            //        foreach (DataRow dr in dtDrawingDetail.Rows)
            //        {
            //            if (dr["RECORD_ID"] != DBNull.Value && Convert.ToInt32(dr["RECORD_ID"]) > 0)
            //                recordID = Convert.ToInt32(dr["RECORD_ID"]);

            //            if (dr["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
            //                jobNo = Convert.ToString(dr["JOB_NO"]);

            //            if (dr["DESIGN_CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CATEGORY"])))
            //                designCategory = Convert.ToString(dr["DESIGN_CATEGORY"]);

            //            if (dr["DESCRIPTION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
            //                description = Convert.ToString(dr["DESCRIPTION"]);

            //            if (dr["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
            //                UOM = Convert.ToString(dr["UOM"]);

            //            if (dr["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["QUANTITY"])))
            //                quantity = Convert.ToString(dr["QUANTITY"]);

            //            if (dr["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"])))
            //                reqdDateByProjectTeam = Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"]);


            //            if (dr["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
            //                drawingNo = Convert.ToString(dr["DRAWING_NO"]);


            //            if (dr["DRAWING_REV_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_REV_NO"])))
            //                drawingRevNo = Convert.ToString(dr["DRAWING_REV_NO"]);


            //            if (dr["STATUS_ID"] != DBNull.Value && Convert.ToInt32(dr["STATUS_ID"]) > 0)
            //                statusID = Convert.ToInt32(dr["STATUS_ID"]);

            //            if (dr["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["STATUS"])))
            //                status = Convert.ToString(dr["STATUS"]);


            //            if (dr["AMENDMENT_COUNT"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_COUNT"]) > 0)
            //                amendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);


            //            if (dr["AMENDMENT_FLAG"] != DBNull.Value && Convert.ToInt32(dr["AMENDMENT_FLAG"]) > 0)
            //                amendmentFlag = Convert.ToInt32(dr["AMENDMENT_FLAG"]);

            //            if (dr["EDITED_FLAG"] != DBNull.Value && Convert.ToInt32(dr["EDITED_FLAG"]) > 0)
            //                editedFlag = Convert.ToInt32(dr["EDITED_FLAG"]);


            //            if (amendmentCount == 0)
            //            {
            //                if (dr["REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])))
            //                    remarks = Convert.ToString(dr["REMARKS"]);
            //            }
            //            else
            //            {
            //                if (dr["AMENDED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AMENDED_REMARKS"])))
            //                    remarks = Convert.ToString(dr["AMENDED_REMARKS"]);

            //                if (amendmentFlag > 0)
            //                {
            //                    if (dr["SENT_TO_AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SENT_TO_AMENDMENT_REMARKS"])))
            //                        remarks = Convert.ToString(dr["SENT_TO_AMENDMENT_REMARKS"]);
            //                }

            //                if (editedFlag > 0)
            //                {
            //                    if (dr["REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])))
            //                        remarks = Convert.ToString(dr["REMARKS"]);
            //                }
            //            }


            //            sb.Append("<tr>\n");
            //            sb.Append("<td class='tdJOBNoDMS'>{#jobNo#}</td>\n");
            //            sb.Append("<td class='tdDrawingNoDMS'>{#drawingNo#}</td>\n");
            //            sb.Append("<td class='tdDescriptionDMS'>{#description#}</td>\n");
            //            sb.Append("<td class='tdRevNoDMS'>{#drawingRevNo#}</td>\n");
            //            sb.Append("<td class='tdCategoryDMS'>{#designCategory#}</td>\n");
            //            sb.Append("<td class='tdUOMDMS'>{#UOM#}</td>\n");
            //            sb.Append("<td class='tdQuantityDMS'>{#quantity#}</td>\n");
            //            sb.Append("<td class='tdRDProjectTeamDMS'>{#reqdDateByProjectTeam#}</td>\n");
            //            sb.Append("<td class='tdStatusDMS'>{#status#}</td>\n");
            //            sb.Append("<td class='tdRemarksDMS'>{#Remarks#}</td>\n");
            //            sb.Append("</tr>\n");


            //            sb.Replace("{#jobNo#}", jobNo);
            //            sb.Replace("{#drawingNo#}", drawingNo);
            //            sb.Replace("{#description#}", description);
            //            sb.Replace("{#drawingRevNo#}", drawingRevNo);
            //            sb.Replace("{#designCategory#}", designCategory);
            //            sb.Replace("{#UOM#}", UOM);
            //            sb.Replace("{#quantity#}", quantity);
            //            sb.Replace("{#reqdDateByProjectTeam#}", reqdDateByProjectTeam);
            //            sb.Replace("{#status#}", status);
            //            sb.Replace("{#Remarks#}", remarks);

            //        }

            //        sb.Append("</table>\n");
            //    }
            //}

            htmlText = sb.ToString();
            return htmlText;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}