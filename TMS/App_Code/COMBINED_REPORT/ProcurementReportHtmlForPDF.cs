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


public class ProcurementReportHtmlForPDF
{
    public string GetHtmlForPDF(DataTable dt, string JOBNo)
    {
        try
        {
            string PONo = string.Empty;
            string PODate = string.Empty;
            string PODeliveryDate = string.Empty;
            string vendorName = string.Empty;
            string itemName = string.Empty;
            string quantity = string.Empty;
            string UOM = string.Empty;
            string POValueInr = string.Empty;
            string jobNo = string.Empty;
            string budget = string.Empty;
            string followUpBy = string.Empty;
            string location = string.Empty;
            string POFirstItem = string.Empty;
            string enggApprovalStatus = string.Empty;
            string presentStatus = string.Empty;
            string EDOfInspComp = string.Empty;
            string postingStatus = string.Empty;
            string lastStatus = string.Empty;
            string lastEdOfInspComp = string.Empty;
            string lastFollowUpDate = string.Empty;


            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            //sb.Append("<h2 class='header' align='center' style='font-family:Arial'><u>Procurement Report Of " + DateTime.Now.ToString("dd-MMM-yyyy") + "</u></h2>\n");
            //sb.Append("<br />\n");
            //sb.Append("<h2 class='header' align='center' style='font-family:Arial'><u>JOB No. [" + JOBNo + "]</u></h2>\n");

            sb.Append("<h1 class='headerStyle'><u>Procurement Report</u></h1>\n");
            sb.Append("<h4 class='header2' align='center'><u>JOB No : " + JOBNo + " On " + DateTime.Now.ToString("dd-MMM-yyyy") + "</u></h4>\n");

            sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");

            sb.Append("<tr class='trsubitems'>\n");

            sb.Append("<th class='tdleft'>PO No</th>\n");
            sb.Append("<th class='tdleft'>PO Date</th>\n");
            sb.Append("<th class='tdleft'>PO Delivery Date</th>\n");
            sb.Append("<th class='tdleft'>Vendor Name</th>\n");
            sb.Append("<th class='tdleft'>Item Name</th>\n");
            sb.Append("<th class='tdright'>Quantity</th>\n");
            sb.Append("<th class='tdleft'>UOM</th>\n");
            sb.Append("<th class='tdright'>PO Value(INR)</th>\n");
            sb.Append("<th class='tdleft'>JOB No</th>\n");
            sb.Append("<th class='tdright'>Budget</th>\n");
            sb.Append("<th class='tdleft'>Follow Up By</th>\n");
            sb.Append("<th class='tdleft'>Location</th>\n");
            sb.Append("<th class='tdleft'>PO First Item</th>\n");
            sb.Append("<th class='tdleft'>Engg. Approval Status</th>\n");
            sb.Append("<th class='tdleft'>Present Status</th>\n");
            sb.Append("<th class='tdleft'>ED Of Insp/Comp</th>\n");
            sb.Append("<th class='tdleft'>Posting Status</th>\n");
            sb.Append("<th class='tdleft'>Last Status</th>\n");
            sb.Append("<th class='tdleft'>Last Ed Of Insp/Comp</th>\n");
            sb.Append("<th class='tdleft'>Last Follow Up Date</th>\n");
            sb.Append("</tr>\n");

            foreach (DataRow dr in dt.Rows)
            {
                PONo = string.Empty;
                PODate = string.Empty;
                PODeliveryDate = string.Empty;
                vendorName = string.Empty;
                itemName = string.Empty;
                quantity = string.Empty;
                UOM = string.Empty;
                POValueInr = string.Empty;
                jobNo = string.Empty;
                budget = string.Empty;
                followUpBy = string.Empty;
                location = string.Empty;
                POFirstItem = string.Empty;
                enggApprovalStatus = string.Empty;
                presentStatus = string.Empty;
                EDOfInspComp = string.Empty;
                postingStatus = string.Empty;
                lastStatus = string.Empty;
                lastEdOfInspComp = string.Empty;
                lastFollowUpDate = string.Empty;


                if (!string.IsNullOrEmpty(Convert.ToString(dr["PO_NO"])))
                {
                    PONo = Convert.ToString(dr["PO_NO"]).Trim();
                    if (PONo.Contains("&"))
                        PONo = PONo.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["PO_DATE"])))
                {
                    PODate = Convert.ToString(dr["PO_DATE"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["PO_DELIVERY_DATE"])))
                {
                    PODeliveryDate = Convert.ToString(dr["PO_DELIVERY_DATE"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["VENDOR_NAME"])))
                {
                    vendorName = Convert.ToString(dr["VENDOR_NAME"]).Trim();
                    if (vendorName.Contains("&"))
                        vendorName = vendorName.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["ITEM_NAME"])))
                {
                    itemName = Convert.ToString(dr["ITEM_NAME"]).Trim();
                    if (itemName.Contains("&"))
                        itemName = itemName.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["QUANTITY"])))
                {
                    quantity = Convert.ToString(dr["QUANTITY"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
                {
                    UOM = Convert.ToString(dr["UOM"]).Trim();
                    if (UOM.Contains("&"))
                        UOM = UOM.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["PO_VALUE_INR"])))
                {
                    POValueInr = Convert.ToString(dr["PO_VALUE_INR"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                {
                    jobNo = Convert.ToString(dr["JOB_NO"]).Trim();
                    if (jobNo.Contains("&"))
                        jobNo = jobNo.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["BUDGET"])))
                {
                    budget = Convert.ToString(dr["BUDGET"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["FOLLOW_UP_BY"])))
                {
                    followUpBy = Convert.ToString(dr["FOLLOW_UP_BY"]).Trim();
                    if (followUpBy.Contains("&"))
                        followUpBy = followUpBy.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["LOCATION"])))
                {
                    location = Convert.ToString(dr["LOCATION"]);
                    if (location.Contains("&"))
                        location = location.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["PO_FIRST_ITEM"])))
                {
                    POFirstItem = Convert.ToString(dr["PO_FIRST_ITEM"]).Trim();
                    if (POFirstItem.Contains("&"))
                        POFirstItem = POFirstItem.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["ENGG_APPROVAL_STATUS"])))
                {
                    enggApprovalStatus = Convert.ToString(dr["ENGG_APPROVAL_STATUS"]).Trim();
                    if (enggApprovalStatus.Contains("&"))
                        enggApprovalStatus = enggApprovalStatus.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["PRESENT_STATUS"])))
                {
                    presentStatus = Convert.ToString(dr["PRESENT_STATUS"]).Trim();
                    if (presentStatus.Contains("&"))
                        presentStatus = presentStatus.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["ED_OF_INSP/COMP"])))
                {
                    EDOfInspComp = Convert.ToString(dr["ED_OF_INSP/COMP"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["POSTING_STATUS"])))
                {
                    postingStatus = Convert.ToString(dr["POSTING_STATUS"]).Trim();
                    if (postingStatus.Contains("&"))
                        postingStatus = postingStatus.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["LAST_STATUS"])))
                {
                    lastStatus = Convert.ToString(dr["LAST_STATUS"]).Trim();
                    if (lastStatus.Contains("&"))
                        lastStatus = lastStatus.Replace("&", " And ");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["LAST_ED_OF_INSP/COMP"])))
                {
                    lastEdOfInspComp = Convert.ToString(dr["LAST_ED_OF_INSP/COMP"]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["LAST_FOLLOW_UP_DATE"])))
                {
                    lastFollowUpDate = Convert.ToString(dr["LAST_FOLLOW_UP_DATE"]);
                }


                sb.Append("<tr>\n");
                sb.Append("<td class='tdleft'>" + PONo + "</td>\n");
                sb.Append("<td class='tdleft'>" + PODate + "</td>\n");
                sb.Append("<td class='tdleft'>" + PODeliveryDate + "</td>\n");
                sb.Append("<td class='tdleft'>" + vendorName + "</td>\n");
                sb.Append("<td class='tdleft'>" + itemName + "</td>\n");
                sb.Append("<td class='tdright'>" + quantity + "</td>\n");
                sb.Append("<td class='tdleft'>" + UOM + "</td>\n");
                sb.Append("<td class='tdright'>" + POValueInr + "</td>\n");
                sb.Append("<td class='tdleft'>" + jobNo + "</td>\n");
                sb.Append("<td class='tdright'>" + budget + "</td>\n");
                sb.Append("<td class='tdleft'>" + followUpBy + "</td>\n");
                sb.Append("<td class='tdleft'>" + location + "</td>\n");
                sb.Append("<td class='tdleft'>" + POFirstItem + "</td>\n");
                sb.Append("<td class='tdleft'>" + enggApprovalStatus + "</td>\n");
                sb.Append("<td class='tdleft'>" + presentStatus + "</td>\n");
                sb.Append("<td class='tdleft'>" + EDOfInspComp + "</td>\n");
                sb.Append("<td class='tdleft'>" + postingStatus + "</td>\n");
                sb.Append("<td class='tdleft'>" + lastStatus + "</td>\n");
                sb.Append("<td class='tdleft'>" + lastEdOfInspComp + "</td>\n");
                sb.Append("<td class='tdleft'>" + lastFollowUpDate + "</td>\n");
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