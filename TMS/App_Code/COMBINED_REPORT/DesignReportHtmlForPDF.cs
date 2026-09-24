using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

using System.Text;
//using System.Net.Mime;
//using iTextSharp.tool.xml.pipeline.css;
//using iTextSharp.tool.xml;
//using iTextSharp.tool.xml.pipeline.html;
//using iTextSharp.tool.xml.pipeline.end;
//using iTextSharp.tool.xml.parser;
//using System.Xml;
//using iTextSharp.tool.xml.css;
using System.Data;


public class DesignReportHtmlForPDF
{
    public string GetHtmlForPDF(DataTable dt, string JOBNo)
    {
        try
        {
            string SRNo = string.Empty;
            string jobNo = string.Empty;
            string category = string.Empty;
            string description = string.Empty;
            string UOM = string.Empty;
            string quantity = string.Empty;
            string reqdDateByProjectTeam = string.Empty;
            string isPlanned = string.Empty;
            string plannedStartDateByDesignTeam = string.Empty;
            string plannedCompletionDateByDesignTeam = string.Empty;
            string drawingNo = string.Empty;
            string drawingRevNo = string.Empty;
            string status = string.Empty;
            string expectedCompletionDate = string.Empty;
            string responsibleDesignEngg = string.Empty;
            string postingStatus = string.Empty;
            string totalHoursSpent = string.Empty;
            string applicableForProduction = string.Empty;
            string isRevised = string.Empty;





            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.Append("<h1 class='headerStyle'><u>Design Report</u></h1>\n");
            sb.Append("<h4 class='header2' align='center'><u>JOB No : " + JOBNo + " On " + DateTime.Now.ToString("dd-MMM-yyyy") + "</u></h4>\n");

            sb.Append("<table class='tblsuitems'  style='margin-top:20px'>\n");
            sb.Append("<tr class='trsubitems'>\n");

            sb.Append("<th class='tdleft'>Sr No</th>\n");
            sb.Append("<th class='tdleft'>JOB No</th>\n");
            sb.Append("<th class='tdleft'>Category</th>\n");
            sb.Append("<th class='tdleft'>Description</th>\n");
            sb.Append("<th class='tdleft'>UOM</th>\n");
            sb.Append("<th class='tdright'>Quantity</th>\n");
            sb.Append("<th class='tdleft'>Reqd. Date By Project Team</th>\n");
            sb.Append("<th class='tdleft'>Is Planned</th>\n");
            sb.Append("<th class='tdleft'>Planned Start Date By Design Team</th>\n");
            sb.Append("<th class='tdleft'>Planned Completion Date By Design Team</th>\n");
            sb.Append("<th class='tdleft'>Drawing No</th>\n");
            sb.Append("<th class='tdleft'>Drawing Rev No</th>\n");
            sb.Append("<th class='tdleft'>Status</th>\n");
            sb.Append("<th class='tdleft'>Expected Completion Date</th>\n");
            sb.Append("<th class='tdleft'>Responsible Design Engg</th>\n");
            sb.Append("<th class='tdleft'>Posting Status</th>\n");
            sb.Append("<th class='tdleft'>Total Hours Spent</th>\n");
            sb.Append("<th class='tdleft'>Applicable For Production</th>\n");
            sb.Append("<th class='tdleft'>Is Revised</th>\n");

            sb.Append("</tr>\n");

            foreach (DataRow dr in dt.Rows)
            {
                SRNo = string.Empty;
                jobNo = string.Empty;
                category = string.Empty;
                description = string.Empty;
                UOM = string.Empty;
                quantity = string.Empty;
                reqdDateByProjectTeam = string.Empty;
                isPlanned = string.Empty;
                plannedStartDateByDesignTeam = string.Empty;
                plannedCompletionDateByDesignTeam = string.Empty;
                drawingNo = string.Empty;
                drawingRevNo = string.Empty;
                status = string.Empty;
                expectedCompletionDate = string.Empty;
                responsibleDesignEngg = string.Empty;
                postingStatus = string.Empty;
                totalHoursSpent = string.Empty;
                applicableForProduction = string.Empty;
                isRevised = string.Empty;


                if (!string.IsNullOrEmpty(Convert.ToString(dr["SR_NO"])))
                {
                    SRNo = Convert.ToString(dr["SR_NO"]).Trim();
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                {
                    jobNo = Convert.ToString(dr["JOB_NO"]).Trim();
                    if (jobNo.Contains("&"))
                        jobNo = jobNo.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["CATEGORY"])))
                {
                    category = Convert.ToString(dr["CATEGORY"]).Trim();
                    if (category.Contains("&"))
                        category = category.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                {
                    description = Convert.ToString(dr["DESCRIPTION"]).Trim();
                    if (description.Contains("&"))
                        description = description.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
                {
                    UOM = Convert.ToString(dr["UOM"]).Trim();
                    if (UOM.Contains("&"))
                        UOM = UOM.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["QUANTITY"])))
                {
                    quantity = Convert.ToString(dr["QUANTITY"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"])))
                {
                    reqdDateByProjectTeam = Convert.ToString(dr["REQD_DATE_BY_PROJECT_TEAM"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["IS_PLANNED"])))
                {
                    isPlanned = Convert.ToString(dr["IS_PLANNED"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["PLANNED_START_DATE_BY_DESIGN_TEAM"])))
                {
                    plannedStartDateByDesignTeam = Convert.ToString(dr["PLANNED_START_DATE_BY_DESIGN_TEAM"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"])))
                {
                    plannedCompletionDateByDesignTeam = Convert.ToString(dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"]);
                }


                if (!string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
                {
                    drawingNo = Convert.ToString(dr["DRAWING_NO"]).Trim();
                    if (drawingNo.Contains("&"))
                        drawingNo = drawingNo.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_REV_NO"])))
                {
                    drawingRevNo = Convert.ToString(dr["DRAWING_REV_NO"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["STATUS"])))
                {
                    status = Convert.ToString(dr["STATUS"]).Trim();
                    if (status.Contains("&"))
                        status = status.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["EXPECTED_COMPLETION_DATE"])))
                {
                    expectedCompletionDate = Convert.ToString(dr["EXPECTED_COMPLETION_DATE"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                {
                    responsibleDesignEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);
                    if (responsibleDesignEngg.Contains("&"))
                        responsibleDesignEngg = responsibleDesignEngg.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["POSTING_STATUS"])))
                {
                    postingStatus = Convert.ToString(dr["POSTING_STATUS"]).Trim();
                    if (postingStatus.Contains("&"))
                        postingStatus = postingStatus.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["TOTAL_HOURS_SPENT"])))
                {
                    totalHoursSpent = Convert.ToString(dr["TOTAL_HOURS_SPENT"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["APPLICABLE_FOR_PRODUCTION"])))
                {
                    applicableForProduction = Convert.ToString(dr["APPLICABLE_FOR_PRODUCTION"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["IS_REVISED"])))
                {
                    isRevised = Convert.ToString(dr["IS_REVISED"]);
                }


                sb.Append("<tr>\n");

                sb.Append("<td class='tdleft'>" + SRNo + "</td>\n");
                sb.Append("<td class='tdleft'>" + jobNo + "</td>\n");
                sb.Append("<td class='tdleft'>" + category + "</td>\n");
                sb.Append("<td class='tdleft'>" + description + "</td>\n");
                sb.Append("<td class='tdleft'>" + UOM + "</td>\n");
                sb.Append("<td class='tdright'>" + quantity + "</td>\n");
                sb.Append("<td class='tdleft'>" + reqdDateByProjectTeam + "</td>\n");
                sb.Append("<td class='tdleft'>" + isPlanned + "</td>\n");
                sb.Append("<td class='tdleft'>" + plannedStartDateByDesignTeam + "</td>\n");
                sb.Append("<td class='tdleft'>" + plannedCompletionDateByDesignTeam + "</td>\n");
                sb.Append("<td class='tdleft'>" + drawingNo + "</td>\n");
                sb.Append("<td class='tdleft'>" + drawingRevNo + "</td>\n");
                sb.Append("<td class='tdleft'>" + status + "</td>\n");
                sb.Append("<td class='tdleft'>" + expectedCompletionDate + "</td>\n");
                sb.Append("<td class='tdleft'>" + responsibleDesignEngg + "</td>\n");
                sb.Append("<td class='tdleft'>" + postingStatus + "</td>\n");
                sb.Append("<td class='tdleft'>" + totalHoursSpent + "</td>\n");
                sb.Append("<td class='tdleft'>" + applicableForProduction + "</td>\n");
                sb.Append("<td class='tdleft'>" + isRevised + "</td>\n");

                sb.Append("</tr>\n");
            }

            sb.Append("</table>\n");

            htmlText = sb.ToString();
            return htmlText;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}