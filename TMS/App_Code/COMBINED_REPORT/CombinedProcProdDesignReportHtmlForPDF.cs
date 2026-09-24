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


public class CombinedProcProdDesignReportHtmlForPDF
{
    public string GetHtmlForPDF(DataTable dtProc, DataTable dtProd, DataTable dtDesign, string JOBNo,
                                string totalPOValueINR, string totalProductQuantity, string totalSpentHours, string type)
    {
        try
        {
            #region VARIABLES[=============]

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
            string POStatus = string.Empty;
            string postingStatus = string.Empty;
            string lastStatus = string.Empty;
            string lastEdOfInspComp = string.Empty;
            string lastFollowUpDate = string.Empty;

            string SRNo = string.Empty;
            string JobNo = string.Empty;
            string LOTDate = string.Empty;
            string productionOrderNo = string.Empty;
            string productionOrderDate = string.Empty;
            string productionOrderDeliveryDate = string.Empty;
            string productCode = string.Empty;
            string equipmentOrItem = string.Empty;
            //string UOM = string.Empty;
            //string quantity = string.Empty;
            string drawingNo = string.Empty;
            //string presentStatus = string.Empty;
            string presentPercOfWorkDone = string.Empty;
            //string EDOfInspComp = string.Empty;
            //string lastStatus = string.Empty;
            string lastPercOfWorkDone = string.Empty;
            //string lastEdOfInspComp = string.Empty;
            string unit = string.Empty;

            //string SRNo = string.Empty;
            //string jobNo = string.Empty;
            string category = string.Empty;
            string description = string.Empty;
            //string UOM = string.Empty;
            //string quantity = string.Empty;
            string reqdDateByProjectTeam = string.Empty;
            string isPlanned = string.Empty;
            string plannedStartDateByDesignTeam = string.Empty;
            string plannedCompletionDateByDesignTeam = string.Empty;
            //string drawingNo = string.Empty;
            string drawingRevNo = string.Empty;
            string status = string.Empty;
            string expectedCompletionDate = string.Empty;
            string responsibleDesignEngg = string.Empty;
            //string postingStatus = string.Empty;
            string totalHoursSpent = string.Empty;
            string applicableForProduction = string.Empty;
            string isRevised = string.Empty;



            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            #endregion



            #region PROCUREMENT[===========]

            if (dtProc != null && dtProc.Rows.Count > 0)
            {

                sb.Append("<h1 class='headerStyle'>" + type + " Procurement Report</h1>\n");
                sb.Append("<h4 class='header2' align='center'>JOB No : " + JOBNo + " On " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h4>\n");

                sb.Append("<h3 class='headerleft'>Total PO Value [INR] : " + totalPOValueINR + "</h3>\n");
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
                sb.Append("<th class='tdleft'>PO Status</th>\n");
                sb.Append("<th class='tdleft'>Last Status</th>\n");
                sb.Append("<th class='tdleft'>Last Ed Of Insp/Comp</th>\n");
                sb.Append("<th class='tdleft'>Last Follow Up Date</th>\n");
                sb.Append("</tr>\n");

                foreach (DataRow dr in dtProc.Rows)
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
                    POStatus = string.Empty;
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

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PO_STATUS"])))
                    {
                        POStatus = Convert.ToString(dr["PO_STATUS"]);
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
                    sb.Append("<td class='tdleft'>" + POStatus + "</td>\n");
                    sb.Append("<td class='tdleft'>" + lastStatus + "</td>\n");
                    sb.Append("<td class='tdleft'>" + lastEdOfInspComp + "</td>\n");
                    sb.Append("<td class='tdleft'>" + lastFollowUpDate + "</td>\n");
                    sb.Append("</tr>\n");
                }

                sb.Append("</table>\n");

            }

            #endregion



            #region PRODUCTION[============]

            if (dtProd != null && dtProd.Rows.Count > 0)
            {
                //sb.Append("<br />\n");
                //sb.Append("<br />\n");
                sb.Append("<div style='page-break-after:always;'></div>\n");
                sb.Append("<h1 class='headerStyle'>" + type + " Production Report</h1>\n");
                sb.Append("<h4 class='header2' align='center'>JOB No : " + JOBNo + " On " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h4>\n");

                sb.Append("<h3 class='headerleft'>Total Product Quantity : " + totalProductQuantity + "</h3>\n");
                sb.Append("<table class='tblsuitems'  style='margin-top:20px'>\n");
                sb.Append("<tr class='trsubitems'>\n");

                sb.Append("<th class='tdright'>Sr No</th>\n");
                sb.Append("<th class='tdleft'>JOB No</th>\n");
                sb.Append("<th class='tdleft'>LOT Date</th>\n");
                sb.Append("<th class='tdleft'>Production Order No</th>\n");
                sb.Append("<th class='tdleft'>Production Order Date</th>\n");
                sb.Append("<th class='tdleft'>Production Order Delivery Date</th>\n");
                sb.Append("<th class='tdleft'>Product Code</th>\n");
                sb.Append("<th class='tdleft'>Equipment/Item</th>\n");
                sb.Append("<th class='tdleft'>UOM</th>\n");
                sb.Append("<th class='tdright'>Quantity</th>\n");
                sb.Append("<th class='tdleft'>Drawing No</th>\n");
                sb.Append("<th class='tdleft'>Present Status</th>\n");
                sb.Append("<th class='tdright'>Present (%) Of Work Done</th>\n");
                sb.Append("<th class='tdleft'>Posting Status</th>\n");
                sb.Append("<th class='tdleft'>ED Of Insp Comp</th>\n");
                sb.Append("<th class='tdleft'>Last Status</th>\n");
                sb.Append("<th class='tdright'>Last (%) Of Work Done</th>\n");
                sb.Append("<th class='tdleft'>Last ED Of Insp Comp</th>\n");
                sb.Append("<th class='tdleft'>Unit</th>\n");
                sb.Append("</tr>\n");

                foreach (DataRow dr in dtProd.Rows)
                {
                    SRNo = string.Empty;
                    JobNo = string.Empty;
                    LOTDate = string.Empty;
                    productionOrderNo = string.Empty;
                    productionOrderDate = string.Empty;
                    productionOrderDeliveryDate = string.Empty;
                    productCode = string.Empty;
                    equipmentOrItem = string.Empty;
                    UOM = string.Empty;
                    quantity = string.Empty;
                    drawingNo = string.Empty;
                    presentStatus = string.Empty;
                    presentPercOfWorkDone = string.Empty;
                    postingStatus = string.Empty;
                    EDOfInspComp = string.Empty;
                    lastStatus = string.Empty;
                    lastPercOfWorkDone = string.Empty;
                    lastEdOfInspComp = string.Empty;
                    unit = string.Empty;


                    if (!string.IsNullOrEmpty(Convert.ToString(dr["SR_NO"])))
                    {
                        SRNo = Convert.ToString(dr["SR_NO"]).Trim();
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                    {
                        JobNo = Convert.ToString(dr["JOB_NO"]);
                        if (JobNo.Contains("&"))
                            JobNo = JobNo.Replace("&", " And ");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["LOT_DATE"])))
                    {
                        LOTDate = Convert.ToString(dr["LOT_DATE"]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PRODUCTION_ORDER_NO"])))
                    {
                        productionOrderNo = Convert.ToString(dr["PRODUCTION_ORDER_NO"]).Trim();
                        if (productionOrderNo.Contains("&"))
                            productionOrderNo = productionOrderNo.Replace("&", " And ");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PRODUCTION_ORDER_DATE"])))
                    {
                        productionOrderDate = Convert.ToString(dr["PRODUCTION_ORDER_DATE"]).Trim();
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PRODUCTION_ORDER_DELIVERY_DATE"])))
                    {
                        productionOrderDeliveryDate = Convert.ToString(dr["PRODUCTION_ORDER_DELIVERY_DATE"]).Trim();
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PRODUCT_CODE"])))
                    {
                        productCode = Convert.ToString(dr["PRODUCT_CODE"]).Trim();
                        if (productCode.Contains("&"))
                            productCode = productCode.Replace("&", " And ");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["EQUIPMENT/ITEM"])))
                    {
                        equipmentOrItem = Convert.ToString(dr["EQUIPMENT/ITEM"]).Trim();
                        if (equipmentOrItem.Contains("&"))
                            equipmentOrItem = equipmentOrItem.Replace("&", " And ");
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


                    if (!string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
                    {
                        drawingNo = Convert.ToString(dr["DRAWING_NO"]);
                        if (drawingNo.Contains("&"))
                            drawingNo = drawingNo.Replace("&", " And ");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PRESENT_STATUS"])))
                    {
                        presentStatus = Convert.ToString(dr["PRESENT_STATUS"]);
                        if (presentStatus.Contains("&"))
                            presentStatus = presentStatus.Replace("&", " And ");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PRESENT_PERC_OF_WORK_DONE"])))
                    {
                        presentPercOfWorkDone = Convert.ToString(dr["PRESENT_PERC_OF_WORK_DONE"]).Trim();
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["POSTING_STATUS"])))
                    {
                        postingStatus = Convert.ToString(dr["POSTING_STATUS"]).Trim();
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["ED_OF_INSP_COMP"])))
                    {
                        EDOfInspComp = Convert.ToString(dr["ED_OF_INSP_COMP"]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["LAST_STATUS"])))
                    {
                        lastStatus = Convert.ToString(dr["LAST_STATUS"]).Trim();
                        if (lastStatus.Contains("&"))
                            lastStatus = lastStatus.Replace("&", " And ");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["LAST_PERC_OF_WORK_DONE"])))
                    {
                        lastPercOfWorkDone = Convert.ToString(dr["LAST_PERC_OF_WORK_DONE"]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["LAST_ED_OF_INSP_COMP"])))
                    {
                        lastEdOfInspComp = Convert.ToString(dr["LAST_ED_OF_INSP_COMP"]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["UNIT"])))
                    {
                        unit = Convert.ToString(dr["UNIT"]);
                        if (unit.Contains("&"))
                            unit = unit.Replace("&", " And ");
                    }

                    sb.Append("<tr>\n");

                    sb.Append("<td class='tdright'>" + SRNo + "</td>\n");
                    sb.Append("<td class='tdleft'>" + JobNo + "</td>\n");
                    sb.Append("<td class='tdleft'>" + LOTDate + "</td>\n");
                    sb.Append("<td class='tdleft'>" + productionOrderNo + "</td>\n");
                    sb.Append("<td class='tdleft'>" + productionOrderDate + "</td>\n");
                    sb.Append("<td class='tdleft'>" + productionOrderDeliveryDate + "</td>\n");
                    sb.Append("<td class='tdleft'>" + productCode + "</td>\n");
                    sb.Append("<td class='tdleft'>" + equipmentOrItem + "</td>\n");
                    sb.Append("<td class='tdleft'>" + UOM + "</td>\n");
                    sb.Append("<td class='tdright'>" + quantity + "</td>\n");
                    sb.Append("<td class='tdleft'>" + drawingNo + "</td>\n");
                    sb.Append("<td class='tdleft'>" + presentStatus + "</td>\n");
                    sb.Append("<td class='tdright'>" + presentPercOfWorkDone + "</td>\n");
                    sb.Append("<td class='tdleft'>" + postingStatus + "</td>\n");
                    sb.Append("<td class='tdleft'>" + EDOfInspComp + "</td>\n");
                    sb.Append("<td class='tdleft'>" + lastStatus + "</td>\n");
                    sb.Append("<td class='tdright'>" + lastPercOfWorkDone + "</td>\n");
                    sb.Append("<td class='tdleft'>" + lastEdOfInspComp + "</td>\n");
                    sb.Append("<td class='tdleft'>" + unit + "</td>\n");

                    sb.Append("</tr>\n");
                }

                sb.Append("</table>\n");

            }

            #endregion



            #region DESIGN[================]

            if (dtDesign != null && dtDesign.Rows.Count > 0)
            {
                //sb.Append("<br />\n");
                //sb.Append("<br />\n");
                sb.Append("<div style='page-break-after:always;'></div>\n");
                sb.Append("<h1 class='headerStyle'>" + type + " Design Report</h1>\n");
                sb.Append("<h4 class='header2' align='center'>JOB No : " + JOBNo + " On " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h4>\n");

                sb.Append("<h3 class='headerleft'>Total Spent Hours : " + totalSpentHours + " Hours </h3>\n");
                sb.Append("<table class='tblsuitems'  style='margin-top:20px'>\n");
                sb.Append("<tr class='trsubitems'>\n");

                sb.Append("<th class='tdright'>Sr No</th>\n");
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
                sb.Append("<th class='tdright'>Drawing Rev No</th>\n");
                sb.Append("<th class='tdleft'>Status</th>\n");
                sb.Append("<th class='tdleft'>Expected Completion Date</th>\n");
                sb.Append("<th class='tdleft'>Responsible Design Engg</th>\n");
                sb.Append("<th class='tdleft'>Posting Status</th>\n");
                sb.Append("<th class='tdleft'>Total Hours Spent</th>\n");
                sb.Append("<th class='tdleft'>Applicable For Production</th>\n");
                sb.Append("<th class='tdleft'>Is Revised</th>\n");

                sb.Append("</tr>\n");

                foreach (DataRow dr in dtDesign.Rows)
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

                    sb.Append("<td class='tdright'>" + SRNo + "</td>\n");
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
                    sb.Append("<td class='tdright'>" + drawingRevNo + "</td>\n");
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

            }

            #endregion


            htmlText = sb.ToString();
            return htmlText;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}