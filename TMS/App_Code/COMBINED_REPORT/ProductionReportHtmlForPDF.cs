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


public class ProductionReportHtmlForPDF
{


    public string GetHtmlForPDF(DataTable dt,string JOBNo)
    {
        try
        {
            string SRNo = string.Empty;
            string JobNo = string.Empty;
            string LOTDate = string.Empty;
            string productionOrderNo = string.Empty;
            string productionOrderDate = string.Empty;
            string productionOrderDeliveryDate = string.Empty;
            string productCode = string.Empty;
            string equipmentOrItem = string.Empty;
            string UOM = string.Empty;
            string quantity = string.Empty;
            string drawingNo = string.Empty;
            string presentStatus = string.Empty;
            string presentPercOfWorkDone = string.Empty;
            string EDOfInspComp = string.Empty;
            string lastStatus = string.Empty;
            string lastPercOfWorkDone = string.Empty;
            string lastEdOfInspComp = string.Empty;
            string unit = string.Empty;



            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.Append("<h1 class='headerStyle'><u>Production Report</u></h1>\n");
            sb.Append("<h4 class='header2' align='center'><u>JOB No : " + JOBNo + " On " + DateTime.Now.ToString("dd-MMM-yyyy") + "</u></h4>\n");

            sb.Append("<table class='tblsuitems'  style='margin-top:20px'>\n");
            sb.Append("<tr class='trsubitems'>\n");

            sb.Append("<th class='tdleft'>Sr No</th>\n");
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
            sb.Append("<th class='tdleft'>ED Of Insp Comp</th>\n");
            sb.Append("<th class='tdleft'>Last Status</th>\n");
            sb.Append("<th class='tdright'>Last (%) Of Work Done</th>\n");
            sb.Append("<th class='tdleft'>Last ED Of Insp Comp</th>\n");
            sb.Append("<th class='tdleft'>Unit</th>\n");
            sb.Append("</tr>\n");

            foreach (DataRow dr in dt.Rows)
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

                sb.Append("<td class='tdleft'>" + SRNo + "</td>\n");
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
                sb.Append("<td class='tdleft'>" + EDOfInspComp + "</td>\n");
                sb.Append("<td class='tdleft'>" + lastStatus + "</td>\n");
                sb.Append("<td class='tdright'>" + lastPercOfWorkDone + "</td>\n");
                sb.Append("<td class='tdleft'>" + lastEdOfInspComp + "</td>\n");
                sb.Append("<td class='tdleft'>" + unit + "</td>\n");

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