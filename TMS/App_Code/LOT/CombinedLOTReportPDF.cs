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


public class CombinedLOTReportPDF
{
    public string GetHtmlForPDF(DataTable dtReport)
    {
        try
        {
            #region VARIABLES[=============]

            string TFNo = string.Empty;
            string Unit = string.Empty;
            string LotDate = string.Empty;
            string CustomerName = string.Empty;
            string JobNo = string.Empty;
            string PONo = string.Empty;
            string ItemName = string.Empty;
            string CreatedBy = string.Empty;
            string CreatedOn = string.Empty;
            string ImpNotes = string.Empty;
            string Status = string.Empty;
            string ProdOrderNo = string.Empty;
            string ProdOrderDate = string.Empty;
            string EEDate = string.Empty;
            string ProdCode = string.Empty;
            string ProdDesc = string.Empty;
            string UOM = string.Empty;
            string SubitemDesc = string.Empty;
            string TagNo = string.Empty;
            string MainItem = string.Empty;
            string DrawingNo = string.Empty;
            string RevisionNo = string.Empty;
            string Category = string.Empty;
            string IsPartOfProdStatus = string.Empty;
            string Quantity = string.Empty;
            string IntlInspQty = string.Empty;
            string ItemAcceptedQty = string.Empty;
            string PE = string.Empty;
            string PM = string.Empty;
            string ProductionManager = string.Empty;
            string QualityManager = string.Empty;
            string PlanningManager = string.Empty;

            bool approvedNextPage = false;
            bool planningAcceptedNextPage = false;
            bool planningForwardNextPage = false;
            bool acceptedNextPage = false;
            bool QaAcceptedNextPage = false;
            bool IINextPage = false;
            bool QIANextPage = false;


            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            #endregion

            if (dtReport.Rows.Count > 0)
            {

                approvedNextPage = false;
                planningAcceptedNextPage = false;
                planningForwardNextPage = false;
                acceptedNextPage = false;
                QaAcceptedNextPage = false;
                IINextPage = false;
                QIANextPage = false;

                sb.Append("<h1 class='headerStyle'><u>COMBINED STATUS-WISE LOT REPORT</u></h1>\n");

                int SRNo = 0;
                int newCount = 0;
                int approvedCount = 0;
                int planningAcceptedCount = 0;
                int planningForwardCount = 0;
                int acceptedCount = 0;
                int qaAcceptedCount = 0;
                int internalinspCount = 0;
                int qaItemAcceptedCount = 0;

                #region NEW[===================]

                SRNo = 0;
                foreach (DataRow dr in dtReport.Rows)
                {

                    if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                        Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        newCount++;
                    }
                }


                //New/Amended
                if (newCount > 0)
                {
                    sb.Append("<h1 class='headerStyle'>Pending on Project Manager/Project Engineer -For LOT Approval</h1>\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsr'>Sr</th>\n");
                    sb.Append("<th class='tdtfno'>TF No.</th>\n");
                    sb.Append("<th class='tdunit'>Unit</th>\n");
                    sb.Append("<th class='tdlotdate'>LOT Date</th>\n");
                    sb.Append("<th class='tdcustomer'>Customer</th>\n");
                    sb.Append("<th class='tdjobno'>JOB No</th>\n");
                    sb.Append("<th class='tdpono'>PO No</th>\n");
                    sb.Append("<th class='tditem'>Item</th>\n");
                    sb.Append("<th class='tdcreatedby'>Created By</th>\n");
                    sb.Append("<th class='tdcreatedon'>Created On</th>\n");
                    sb.Append("<th class='tdimpnotes'>Imp Notes</th>\n");
                    sb.Append("<th class='tdprodorderno'>Prod Order No</th>\n");
                    sb.Append("<th class='tdprodorderdate'>Prod Order Date</th>\n");
                    sb.Append("<th class='tdecdate'>EC Date</th>\n");
                    sb.Append("<th class='tdprodcode'>Prod Code</th>\n");
                    sb.Append("<th class='tdproddesc'>Prod Desc</th>\n");
                    sb.Append("<th class='tduom'>UOM</th>\n");
                    sb.Append("<th class='tdsubitemdesc'>Subitem Desc</th>\n");
                    sb.Append("<th class='tdtagno'>Tag No</th>\n");
                    sb.Append("<th class='tdmainitem'>Main Item</th>\n");
                    sb.Append("<th class='tddrawingno'>Drawing No</th>\n");
                    sb.Append("<th class='tdrevno'>Rev No</th>\n");
                    sb.Append("<th class='tdcategory'>Category</th>\n");
                    sb.Append("<th class='tdispartofprod'>Is Part Of Prod </th>\n");
                    sb.Append("<th class='tdqty'>Quantity</th>\n");
                    sb.Append("<th class='tdpe'>Project Engg.</th>\n");
                    sb.Append("<th class='tdpm'>Project Manager</th>\n");
                    sb.Append("</tr>\n");

                    foreach (DataRow drn in dtReport.Rows)
                    {
                        if (Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                            Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                        {
                            TFNo = string.Empty;
                            Unit = string.Empty;
                            LotDate = string.Empty;
                            CustomerName = string.Empty;
                            JobNo = string.Empty;
                            PONo = string.Empty;
                            ItemName = string.Empty;
                            CreatedBy = string.Empty;
                            CreatedOn = string.Empty;
                            ImpNotes = string.Empty;
                            Status = string.Empty;
                            ProdOrderNo = string.Empty;
                            ProdOrderDate = string.Empty;
                            EEDate = string.Empty;
                            ProdCode = string.Empty;
                            ProdDesc = string.Empty;
                            UOM = string.Empty;
                            SubitemDesc = string.Empty;
                            TagNo = string.Empty;
                            MainItem = string.Empty;
                            DrawingNo = string.Empty;
                            RevisionNo = string.Empty;
                            Category = string.Empty;
                            IsPartOfProdStatus = string.Empty;
                            Quantity = string.Empty;
                            IntlInspQty = string.Empty;
                            ItemAcceptedQty = string.Empty;
                            PE = string.Empty;
                            PM = string.Empty;
                            ProductionManager = string.Empty;
                            QualityManager = string.Empty;
                            PlanningManager = string.Empty;


                            SRNo++;

                            if (drn["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TF_NO"])))
                                TFNo = Convert.ToString(drn["TF_NO"]);
                            if (drn["UNIT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UNIT"])))
                                Unit = Convert.ToString(drn["UNIT"]);
                            if (drn["LOT_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["LOT_DATE"])))
                                LotDate = Convert.ToString(drn["LOT_DATE"]);
                            if (drn["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CUSTOMER_NAME"])))
                                CustomerName = Convert.ToString(drn["CUSTOMER_NAME"]);
                            if (drn["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["JOB_NO"])))
                                JobNo = Convert.ToString(drn["JOB_NO"]);
                            if (drn["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PO_NO"])))
                                PONo = Convert.ToString(drn["PO_NO"]);
                            if (drn["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_NAME"])))
                                ItemName = Convert.ToString(drn["ITEM_NAME"]);
                            if (drn["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_BY"])))
                                CreatedBy = Convert.ToString(drn["CREATED_BY"]);
                            if (drn["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_ON"])))
                                CreatedOn = Convert.ToString(drn["CREATED_ON"]);
                            if (drn["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IMP_NOTES"])))
                                ImpNotes = Convert.ToString(drn["IMP_NOTES"]);
                            if (drn["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["STATUS"])))
                                Status = Convert.ToString(drn["STATUS"]);
                            if (drn["PROD_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_NO"])))
                                ProdOrderNo = Convert.ToString(drn["PROD_ORDER_NO"]);
                            if (drn["PROD_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_DATE"])))
                                ProdOrderDate = Convert.ToString(drn["PROD_ORDER_DATE"]);
                            if (drn["EC_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["EC_DATE"])))
                                EEDate = Convert.ToString(drn["EC_DATE"]);
                            if (drn["PROD_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_CODE"])))
                                ProdCode = Convert.ToString(drn["PROD_CODE"]);
                            if (drn["PROD_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_DESC"])))
                                ProdDesc = Convert.ToString(drn["PROD_DESC"]);
                            if (drn["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UOM"])))
                                UOM = Convert.ToString(drn["UOM"]);
                            if (drn["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["SUBITEM_DESC"])))
                                SubitemDesc = Convert.ToString(drn["SUBITEM_DESC"]);
                            if (drn["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TAG_NO"])))
                                TagNo = Convert.ToString(drn["TAG_NO"]);
                            if (drn["MAIN_ITEM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["MAIN_ITEM"])))
                                MainItem = Convert.ToString(drn["MAIN_ITEM"]);
                            if (drn["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DRAWING_NO"])))
                                DrawingNo = Convert.ToString(drn["DRAWING_NO"]);
                            if (drn["REVISION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["REVISION_NO"])))
                                RevisionNo = Convert.ToString(drn["REVISION_NO"]);
                            if (drn["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CATEGORY"])))
                                Category = Convert.ToString(drn["CATEGORY"]);
                            if (drn["IS_PART_OF_PROD_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IS_PART_OF_PROD_STATUS"])))
                                IsPartOfProdStatus = Convert.ToString(drn["IS_PART_OF_PROD_STATUS"]);
                            if (drn["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUANTITY"])))
                                Quantity = Convert.ToString(drn["QUANTITY"]);
                            if (drn["INTL_INSP_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["INTL_INSP_QTY"])))
                                IntlInspQty = Convert.ToString(drn["INTL_INSP_QTY"]);
                            if (drn["ITEM_ACCEPTED_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_ACCEPTED_QTY"])))
                                ItemAcceptedQty = Convert.ToString(drn["ITEM_ACCEPTED_QTY"]);
                            if (drn["PE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PE"])))
                                PE = Convert.ToString(drn["PE"]);
                            if (drn["PM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PM"])))
                                PM = Convert.ToString(drn["PM"]);
                            if (drn["PRODUCTION_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PRODUCTION_MANAGER"])))
                                ProductionManager = Convert.ToString(drn["PRODUCTION_MANAGER"]);
                            if (drn["QUALITY_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUALITY_MANAGER"])))
                                QualityManager = Convert.ToString(drn["QUALITY_MANAGER"]);



                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsr'>" + SRNo + "</td>\n");
                            sb.Append("<td class='tdtfno'>" + TFNo + "</td>\n");
                            sb.Append("<td class='tdunit'>" + Unit + "</td>\n");
                            sb.Append("<td class='tdlotdate'>" + LotDate + "</td>\n");
                            sb.Append("<td class='tdcustomer'>" + CustomerName + "</td>\n");
                            sb.Append("<td class='tdjobno'>" + JobNo + "</td>\n");
                            sb.Append("<td class='tdpono'>" + PONo + "</td>\n");
                            sb.Append("<td class='tditem'>" + ItemName + "</td>\n");
                            sb.Append("<td class='tdcreatedby'>" + CreatedBy + "</td>\n");
                            sb.Append("<td class='tdcreatedon'>" + CreatedOn + "</td>\n");
                            sb.Append("<td class='tdimpnotes'>" + ImpNotes + "</td>\n");
                            sb.Append("<td class='tdprodorderno'>" + ProdOrderNo + "</td>\n");
                            sb.Append("<td class='tdprodorderdate'>" + ProdOrderDate + "</td>\n");
                            sb.Append("<td class='tdecdate'>" + EEDate + "</td>\n");
                            sb.Append("<td class='tdprodcode'>" + ProdCode + "</td>\n");
                            sb.Append("<td class='tdproddesc'>" + ProdDesc + "</td>\n");
                            sb.Append("<td class='tduom'>" + UOM + "</td>\n");
                            sb.Append("<td class='tdsubitemdesc'>" + SubitemDesc + "</td>\n");
                            sb.Append("<td class='tdtagno'>" + TagNo + "</td>\n");
                            sb.Append("<td class='tdmainitem'>" + MainItem + "</td>\n");
                            sb.Append("<td class='tddrawingno'>" + DrawingNo + "</td>\n");
                            sb.Append("<td class='tdrevno'>" + RevisionNo + "</td>\n");
                            sb.Append("<td class='tdcategory'>" + Category + "</td>\n");
                            sb.Append("<td class='tdispartofprod'>" + IsPartOfProdStatus + "</td>\n");
                            sb.Append("<td class='tdqty'>" + Quantity + "</td>\n");
                            sb.Append("<td class='tdpe'>" + PE + "</td>\n");
                            sb.Append("<td class='tdpm'>" + PM + "</td>\n");
                            sb.Append("</tr>\n");
                        }
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                }

                #endregion


                #region APPROVED[==============]

                SRNo = 0;
                foreach (DataRow dr in dtReport.Rows)
                {
                    if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                        Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        approvedCount++;
                    }
                }

                if (approvedCount > 0)
                {
                    if (newCount > 0)
                    {
                        approvedNextPage = true;
                    }

                    if (approvedNextPage)
                    {
                        sb.Append("<div style='page-break-after:always;'></div>\n");
                    }

                    sb.Append("<h1 class='headerStyle'>Pending on Planning Team -For LOT Acceptance</h1>\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsr'>Sr</th>\n");
                    sb.Append("<th class='tdtfno'>TF No.</th>\n");
                    sb.Append("<th class='tdunit'>Unit</th>\n");
                    sb.Append("<th class='tdlotdate'>LOT Date</th>\n");
                    sb.Append("<th class='tdcustomer'>Customer</th>\n");
                    sb.Append("<th class='tdjobno'>JOB No</th>\n");
                    sb.Append("<th class='tdpono'>PO No</th>\n");
                    sb.Append("<th class='tditem'>Item</th>\n");
                    sb.Append("<th class='tdcreatedby'>Created By</th>\n");
                    sb.Append("<th class='tdcreatedon'>Created On</th>\n");
                    sb.Append("<th class='tdimpnotes'>Imp Notes</th>\n");
                    sb.Append("<th class='tdprodorderno'>Prod Order No</th>\n");
                    sb.Append("<th class='tdprodorderdate'>Prod Order Date</th>\n");
                    sb.Append("<th class='tdecdate'>EC Date</th>\n");
                    sb.Append("<th class='tdprodcode'>Prod Code</th>\n");
                    sb.Append("<th class='tdproddesc'>Prod Desc</th>\n");
                    sb.Append("<th class='tduom'>UOM</th>\n");
                    sb.Append("<th class='tdsubitemdesc'>Subitem Desc</th>\n");
                    sb.Append("<th class='tdtagno'>Tag No</th>\n");
                    sb.Append("<th class='tdmainitem'>Main Item</th>\n");
                    sb.Append("<th class='tddrawingno'>Drawing No</th>\n");
                    sb.Append("<th class='tdrevno'>Rev No</th>\n");
                    sb.Append("<th class='tdcategory'>Category</th>\n");
                    sb.Append("<th class='tdispartofprod'>Is Part Of Prod </th>\n");
                    sb.Append("<th class='tdqty'>Quantity</th>\n");
                    sb.Append("<th class='tdpm'>Planning Manager</th>\n");
                    sb.Append("</tr>\n");


                    foreach (DataRow drn in dtReport.Rows)
                    {
                        if (Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                            Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                        {
                            TFNo = string.Empty;
                            Unit = string.Empty;
                            LotDate = string.Empty;
                            CustomerName = string.Empty;
                            JobNo = string.Empty;
                            PONo = string.Empty;
                            ItemName = string.Empty;
                            CreatedBy = string.Empty;
                            CreatedOn = string.Empty;
                            ImpNotes = string.Empty;
                            Status = string.Empty;
                            ProdOrderNo = string.Empty;
                            ProdOrderDate = string.Empty;
                            EEDate = string.Empty;
                            ProdCode = string.Empty;
                            ProdDesc = string.Empty;
                            UOM = string.Empty;
                            SubitemDesc = string.Empty;
                            TagNo = string.Empty;
                            MainItem = string.Empty;
                            DrawingNo = string.Empty;
                            RevisionNo = string.Empty;
                            Category = string.Empty;
                            IsPartOfProdStatus = string.Empty;
                            Quantity = string.Empty;
                            IntlInspQty = string.Empty;
                            ItemAcceptedQty = string.Empty;
                            PE = string.Empty;
                            PM = string.Empty;
                            ProductionManager = string.Empty;
                            QualityManager = string.Empty;
                            PlanningManager = string.Empty;



                            SRNo++;

                            if (drn["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TF_NO"])))
                                TFNo = Convert.ToString(drn["TF_NO"]);
                            if (drn["UNIT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UNIT"])))
                                Unit = Convert.ToString(drn["UNIT"]);
                            if (drn["LOT_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["LOT_DATE"])))
                                LotDate = Convert.ToString(drn["LOT_DATE"]);
                            if (drn["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CUSTOMER_NAME"])))
                                CustomerName = Convert.ToString(drn["CUSTOMER_NAME"]);
                            if (drn["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["JOB_NO"])))
                                JobNo = Convert.ToString(drn["JOB_NO"]);
                            if (drn["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PO_NO"])))
                                PONo = Convert.ToString(drn["PO_NO"]);
                            if (drn["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_NAME"])))
                                ItemName = Convert.ToString(drn["ITEM_NAME"]);
                            if (drn["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_BY"])))
                                CreatedBy = Convert.ToString(drn["CREATED_BY"]);
                            if (drn["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_ON"])))
                                CreatedOn = Convert.ToString(drn["CREATED_ON"]);
                            if (drn["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IMP_NOTES"])))
                                ImpNotes = Convert.ToString(drn["IMP_NOTES"]);
                            if (drn["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["STATUS"])))
                                Status = Convert.ToString(drn["STATUS"]);
                            if (drn["PROD_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_NO"])))
                                ProdOrderNo = Convert.ToString(drn["PROD_ORDER_NO"]);
                            if (drn["PROD_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_DATE"])))
                                ProdOrderDate = Convert.ToString(drn["PROD_ORDER_DATE"]);
                            if (drn["EC_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["EC_DATE"])))
                                EEDate = Convert.ToString(drn["EC_DATE"]);
                            if (drn["PROD_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_CODE"])))
                                ProdCode = Convert.ToString(drn["PROD_CODE"]);
                            if (drn["PROD_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_DESC"])))
                                ProdDesc = Convert.ToString(drn["PROD_DESC"]);
                            if (drn["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UOM"])))
                                UOM = Convert.ToString(drn["UOM"]);
                            if (drn["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["SUBITEM_DESC"])))
                                SubitemDesc = Convert.ToString(drn["SUBITEM_DESC"]);
                            if (drn["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TAG_NO"])))
                                TagNo = Convert.ToString(drn["TAG_NO"]);
                            if (drn["MAIN_ITEM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["MAIN_ITEM"])))
                                MainItem = Convert.ToString(drn["MAIN_ITEM"]);
                            if (drn["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DRAWING_NO"])))
                                DrawingNo = Convert.ToString(drn["DRAWING_NO"]);
                            if (drn["REVISION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["REVISION_NO"])))
                                RevisionNo = Convert.ToString(drn["REVISION_NO"]);
                            if (drn["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CATEGORY"])))
                                Category = Convert.ToString(drn["CATEGORY"]);
                            if (drn["IS_PART_OF_PROD_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IS_PART_OF_PROD_STATUS"])))
                                IsPartOfProdStatus = Convert.ToString(drn["IS_PART_OF_PROD_STATUS"]);
                            if (drn["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUANTITY"])))
                                Quantity = Convert.ToString(drn["QUANTITY"]);
                            if (drn["INTL_INSP_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["INTL_INSP_QTY"])))
                                IntlInspQty = Convert.ToString(drn["INTL_INSP_QTY"]);
                            if (drn["ITEM_ACCEPTED_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_ACCEPTED_QTY"])))
                                ItemAcceptedQty = Convert.ToString(drn["ITEM_ACCEPTED_QTY"]);
                            if (drn["PE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PE"])))
                                PE = Convert.ToString(drn["PE"]);
                            if (drn["PM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PM"])))
                                PM = Convert.ToString(drn["PM"]);
                            if (drn["PRODUCTION_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PRODUCTION_MANAGER"])))
                                ProductionManager = Convert.ToString(drn["PRODUCTION_MANAGER"]);
                            if (drn["QUALITY_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUALITY_MANAGER"])))
                                QualityManager = Convert.ToString(drn["QUALITY_MANAGER"]);
                            if (drn["PLANNING_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PLANNING_MANAGER"])))
                                PlanningManager = Convert.ToString(drn["PLANNING_MANAGER"]);


                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsr'>" + SRNo + "</td>\n");
                            sb.Append("<td class='tdtfno'>" + TFNo + "</td>\n");
                            sb.Append("<td class='tdunit'>" + Unit + "</td>\n");
                            sb.Append("<td class='tdlotdate'>" + LotDate + "</td>\n");
                            sb.Append("<td class='tdcustomer'>" + CustomerName + "</td>\n");
                            sb.Append("<td class='tdjobno'>" + JobNo + "</td>\n");
                            sb.Append("<td class='tdpono'>" + PONo + "</td>\n");
                            sb.Append("<td class='tditem'>" + ItemName + "</td>\n");
                            sb.Append("<td class='tdcreatedby'>" + CreatedBy + "</td>\n");
                            sb.Append("<td class='tdcreatedon'>" + CreatedOn + "</td>\n");
                            sb.Append("<td class='tdimpnotes'>" + ImpNotes + "</td>\n");
                            sb.Append("<td class='tdprodorderno'>" + ProdOrderNo + "</td>\n");
                            sb.Append("<td class='tdprodorderdate'>" + ProdOrderDate + "</td>\n");
                            sb.Append("<td class='tdecdate'>" + EEDate + "</td>\n");
                            sb.Append("<td class='tdprodcode'>" + ProdCode + "</td>\n");
                            sb.Append("<td class='tdproddesc'>" + ProdDesc + "</td>\n");
                            sb.Append("<td class='tduom'>" + UOM + "</td>\n");
                            sb.Append("<td class='tdsubitemdesc'>" + SubitemDesc + "</td>\n");
                            sb.Append("<td class='tdtagno'>" + TagNo + "</td>\n");
                            sb.Append("<td class='tdmainitem'>" + MainItem + "</td>\n");
                            sb.Append("<td class='tddrawingno'>" + DrawingNo + "</td>\n");
                            sb.Append("<td class='tdrevno'>" + RevisionNo + "</td>\n");
                            sb.Append("<td class='tdcategory'>" + Category + "</td>\n");
                            sb.Append("<td class='tdispartofprod'>" + IsPartOfProdStatus + "</td>\n");
                            sb.Append("<td class='tdqty'>" + Quantity + "</td>\n");
                            sb.Append("<td class='tdprodm'>" + PlanningManager + "</td>\n");
                            sb.Append("</tr>\n");

                        }
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                }

                #endregion


                #region PLANNING ACCEPTED[==============]

                SRNo = 0;
                foreach (DataRow dr in dtReport.Rows)
                {
                    if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                        Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        planningAcceptedCount++;
                    }
                }

                if (planningAcceptedCount > 0)
                {
                    if (newCount > 0 || approvedCount > 0)
                    {
                        planningAcceptedNextPage = true;
                    }

                    if (planningAcceptedNextPage)
                    {
                        sb.Append("<div style='page-break-after:always;'></div>\n");
                    }

                    sb.Append("<h1 class='headerStyle'>Pending on Planning Team -For LOT Forward</h1>\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsr'>Sr</th>\n");
                    sb.Append("<th class='tdtfno'>TF No.</th>\n");
                    sb.Append("<th class='tdunit'>Unit</th>\n");
                    sb.Append("<th class='tdlotdate'>LOT Date</th>\n");
                    sb.Append("<th class='tdcustomer'>Customer</th>\n");
                    sb.Append("<th class='tdjobno'>JOB No</th>\n");
                    sb.Append("<th class='tdpono'>PO No</th>\n");
                    sb.Append("<th class='tditem'>Item</th>\n");
                    sb.Append("<th class='tdcreatedby'>Created By</th>\n");
                    sb.Append("<th class='tdcreatedon'>Created On</th>\n");
                    sb.Append("<th class='tdimpnotes'>Imp Notes</th>\n");
                    sb.Append("<th class='tdprodorderno'>Prod Order No</th>\n");
                    sb.Append("<th class='tdprodorderdate'>Prod Order Date</th>\n");
                    sb.Append("<th class='tdecdate'>EC Date</th>\n");
                    sb.Append("<th class='tdprodcode'>Prod Code</th>\n");
                    sb.Append("<th class='tdproddesc'>Prod Desc</th>\n");
                    sb.Append("<th class='tduom'>UOM</th>\n");
                    sb.Append("<th class='tdsubitemdesc'>Subitem Desc</th>\n");
                    sb.Append("<th class='tdtagno'>Tag No</th>\n");
                    sb.Append("<th class='tdmainitem'>Main Item</th>\n");
                    sb.Append("<th class='tddrawingno'>Drawing No</th>\n");
                    sb.Append("<th class='tdrevno'>Rev No</th>\n");
                    sb.Append("<th class='tdcategory'>Category</th>\n");
                    sb.Append("<th class='tdispartofprod'>Is Part Of Prod </th>\n");
                    sb.Append("<th class='tdqty'>Quantity</th>\n");
                    sb.Append("<th class='tdpm'>Planning Manager</th>\n");
                    sb.Append("</tr>\n");


                    foreach (DataRow drn in dtReport.Rows)
                    {
                        if (Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                            Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                        {
                            TFNo = string.Empty;
                            Unit = string.Empty;
                            LotDate = string.Empty;
                            CustomerName = string.Empty;
                            JobNo = string.Empty;
                            PONo = string.Empty;
                            ItemName = string.Empty;
                            CreatedBy = string.Empty;
                            CreatedOn = string.Empty;
                            ImpNotes = string.Empty;
                            Status = string.Empty;
                            ProdOrderNo = string.Empty;
                            ProdOrderDate = string.Empty;
                            EEDate = string.Empty;
                            ProdCode = string.Empty;
                            ProdDesc = string.Empty;
                            UOM = string.Empty;
                            SubitemDesc = string.Empty;
                            TagNo = string.Empty;
                            MainItem = string.Empty;
                            DrawingNo = string.Empty;
                            RevisionNo = string.Empty;
                            Category = string.Empty;
                            IsPartOfProdStatus = string.Empty;
                            Quantity = string.Empty;
                            IntlInspQty = string.Empty;
                            ItemAcceptedQty = string.Empty;
                            PE = string.Empty;
                            PM = string.Empty;
                            ProductionManager = string.Empty;
                            QualityManager = string.Empty;
                            PlanningManager = string.Empty;



                            SRNo++;

                            if (drn["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TF_NO"])))
                                TFNo = Convert.ToString(drn["TF_NO"]);
                            if (drn["UNIT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UNIT"])))
                                Unit = Convert.ToString(drn["UNIT"]);
                            if (drn["LOT_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["LOT_DATE"])))
                                LotDate = Convert.ToString(drn["LOT_DATE"]);
                            if (drn["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CUSTOMER_NAME"])))
                                CustomerName = Convert.ToString(drn["CUSTOMER_NAME"]);
                            if (drn["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["JOB_NO"])))
                                JobNo = Convert.ToString(drn["JOB_NO"]);
                            if (drn["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PO_NO"])))
                                PONo = Convert.ToString(drn["PO_NO"]);
                            if (drn["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_NAME"])))
                                ItemName = Convert.ToString(drn["ITEM_NAME"]);
                            if (drn["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_BY"])))
                                CreatedBy = Convert.ToString(drn["CREATED_BY"]);
                            if (drn["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_ON"])))
                                CreatedOn = Convert.ToString(drn["CREATED_ON"]);
                            if (drn["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IMP_NOTES"])))
                                ImpNotes = Convert.ToString(drn["IMP_NOTES"]);
                            if (drn["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["STATUS"])))
                                Status = Convert.ToString(drn["STATUS"]);
                            if (drn["PROD_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_NO"])))
                                ProdOrderNo = Convert.ToString(drn["PROD_ORDER_NO"]);
                            if (drn["PROD_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_DATE"])))
                                ProdOrderDate = Convert.ToString(drn["PROD_ORDER_DATE"]);
                            if (drn["EC_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["EC_DATE"])))
                                EEDate = Convert.ToString(drn["EC_DATE"]);
                            if (drn["PROD_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_CODE"])))
                                ProdCode = Convert.ToString(drn["PROD_CODE"]);
                            if (drn["PROD_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_DESC"])))
                                ProdDesc = Convert.ToString(drn["PROD_DESC"]);
                            if (drn["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UOM"])))
                                UOM = Convert.ToString(drn["UOM"]);
                            if (drn["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["SUBITEM_DESC"])))
                                SubitemDesc = Convert.ToString(drn["SUBITEM_DESC"]);
                            if (drn["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TAG_NO"])))
                                TagNo = Convert.ToString(drn["TAG_NO"]);
                            if (drn["MAIN_ITEM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["MAIN_ITEM"])))
                                MainItem = Convert.ToString(drn["MAIN_ITEM"]);
                            if (drn["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DRAWING_NO"])))
                                DrawingNo = Convert.ToString(drn["DRAWING_NO"]);
                            if (drn["REVISION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["REVISION_NO"])))
                                RevisionNo = Convert.ToString(drn["REVISION_NO"]);
                            if (drn["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CATEGORY"])))
                                Category = Convert.ToString(drn["CATEGORY"]);
                            if (drn["IS_PART_OF_PROD_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IS_PART_OF_PROD_STATUS"])))
                                IsPartOfProdStatus = Convert.ToString(drn["IS_PART_OF_PROD_STATUS"]);
                            if (drn["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUANTITY"])))
                                Quantity = Convert.ToString(drn["QUANTITY"]);
                            if (drn["INTL_INSP_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["INTL_INSP_QTY"])))
                                IntlInspQty = Convert.ToString(drn["INTL_INSP_QTY"]);
                            if (drn["ITEM_ACCEPTED_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_ACCEPTED_QTY"])))
                                ItemAcceptedQty = Convert.ToString(drn["ITEM_ACCEPTED_QTY"]);
                            if (drn["PE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PE"])))
                                PE = Convert.ToString(drn["PE"]);
                            if (drn["PM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PM"])))
                                PM = Convert.ToString(drn["PM"]);
                            if (drn["PRODUCTION_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PRODUCTION_MANAGER"])))
                                ProductionManager = Convert.ToString(drn["PRODUCTION_MANAGER"]);
                            if (drn["QUALITY_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUALITY_MANAGER"])))
                                QualityManager = Convert.ToString(drn["QUALITY_MANAGER"]);
                            if (drn["PLANNING_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PLANNING_MANAGER"])))
                                PlanningManager = Convert.ToString(drn["PLANNING_MANAGER"]);


                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsr'>" + SRNo + "</td>\n");
                            sb.Append("<td class='tdtfno'>" + TFNo + "</td>\n");
                            sb.Append("<td class='tdunit'>" + Unit + "</td>\n");
                            sb.Append("<td class='tdlotdate'>" + LotDate + "</td>\n");
                            sb.Append("<td class='tdcustomer'>" + CustomerName + "</td>\n");
                            sb.Append("<td class='tdjobno'>" + JobNo + "</td>\n");
                            sb.Append("<td class='tdpono'>" + PONo + "</td>\n");
                            sb.Append("<td class='tditem'>" + ItemName + "</td>\n");
                            sb.Append("<td class='tdcreatedby'>" + CreatedBy + "</td>\n");
                            sb.Append("<td class='tdcreatedon'>" + CreatedOn + "</td>\n");
                            sb.Append("<td class='tdimpnotes'>" + ImpNotes + "</td>\n");
                            sb.Append("<td class='tdprodorderno'>" + ProdOrderNo + "</td>\n");
                            sb.Append("<td class='tdprodorderdate'>" + ProdOrderDate + "</td>\n");
                            sb.Append("<td class='tdecdate'>" + EEDate + "</td>\n");
                            sb.Append("<td class='tdprodcode'>" + ProdCode + "</td>\n");
                            sb.Append("<td class='tdproddesc'>" + ProdDesc + "</td>\n");
                            sb.Append("<td class='tduom'>" + UOM + "</td>\n");
                            sb.Append("<td class='tdsubitemdesc'>" + SubitemDesc + "</td>\n");
                            sb.Append("<td class='tdtagno'>" + TagNo + "</td>\n");
                            sb.Append("<td class='tdmainitem'>" + MainItem + "</td>\n");
                            sb.Append("<td class='tddrawingno'>" + DrawingNo + "</td>\n");
                            sb.Append("<td class='tdrevno'>" + RevisionNo + "</td>\n");
                            sb.Append("<td class='tdcategory'>" + Category + "</td>\n");
                            sb.Append("<td class='tdispartofprod'>" + IsPartOfProdStatus + "</td>\n");
                            sb.Append("<td class='tdqty'>" + Quantity + "</td>\n");
                            sb.Append("<td class='tdprodm'>" + PlanningManager + "</td>\n");
                            sb.Append("</tr>\n");

                        }
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                }

                #endregion



                #region FORWARDED[==============]

                SRNo = 0;
                foreach (DataRow dr in dtReport.Rows)
                {
                    if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                        Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        planningForwardCount++;
                    }
                }

                if (planningForwardCount > 0)
                {
                    if (newCount > 0 || approvedCount > 0 || planningAcceptedCount > 0)
                    {
                        planningForwardNextPage = true;
                    }

                    if (planningForwardNextPage)
                    {
                        sb.Append("<div style='page-break-after:always;'></div>\n");
                    }

                    sb.Append("<h1 class='headerStyle'>Pending on Production Manager -For LOT Acceptance</h1>\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsr'>Sr</th>\n");
                    sb.Append("<th class='tdtfno'>TF No.</th>\n");
                    sb.Append("<th class='tdunit'>Unit</th>\n");
                    sb.Append("<th class='tdlotdate'>LOT Date</th>\n");
                    sb.Append("<th class='tdcustomer'>Customer</th>\n");
                    sb.Append("<th class='tdjobno'>JOB No</th>\n");
                    sb.Append("<th class='tdpono'>PO No</th>\n");
                    sb.Append("<th class='tditem'>Item</th>\n");
                    sb.Append("<th class='tdcreatedby'>Created By</th>\n");
                    sb.Append("<th class='tdcreatedon'>Created On</th>\n");
                    sb.Append("<th class='tdimpnotes'>Imp Notes</th>\n");
                    sb.Append("<th class='tdprodorderno'>Prod Order No</th>\n");
                    sb.Append("<th class='tdprodorderdate'>Prod Order Date</th>\n");
                    sb.Append("<th class='tdecdate'>EC Date</th>\n");
                    sb.Append("<th class='tdprodcode'>Prod Code</th>\n");
                    sb.Append("<th class='tdproddesc'>Prod Desc</th>\n");
                    sb.Append("<th class='tduom'>UOM</th>\n");
                    sb.Append("<th class='tdsubitemdesc'>Subitem Desc</th>\n");
                    sb.Append("<th class='tdtagno'>Tag No</th>\n");
                    sb.Append("<th class='tdmainitem'>Main Item</th>\n");
                    sb.Append("<th class='tddrawingno'>Drawing No</th>\n");
                    sb.Append("<th class='tdrevno'>Rev No</th>\n");
                    sb.Append("<th class='tdcategory'>Category</th>\n");
                    sb.Append("<th class='tdispartofprod'>Is Part Of Prod </th>\n");
                    sb.Append("<th class='tdqty'>Quantity</th>\n");
                    sb.Append("<th class='tdpm'>Production Manager</th>\n");
                    sb.Append("</tr>\n");


                    foreach (DataRow drn in dtReport.Rows)
                    {
                        if (Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                            Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                        {
                            TFNo = string.Empty;
                            Unit = string.Empty;
                            LotDate = string.Empty;
                            CustomerName = string.Empty;
                            JobNo = string.Empty;
                            PONo = string.Empty;
                            ItemName = string.Empty;
                            CreatedBy = string.Empty;
                            CreatedOn = string.Empty;
                            ImpNotes = string.Empty;
                            Status = string.Empty;
                            ProdOrderNo = string.Empty;
                            ProdOrderDate = string.Empty;
                            EEDate = string.Empty;
                            ProdCode = string.Empty;
                            ProdDesc = string.Empty;
                            UOM = string.Empty;
                            SubitemDesc = string.Empty;
                            TagNo = string.Empty;
                            MainItem = string.Empty;
                            DrawingNo = string.Empty;
                            RevisionNo = string.Empty;
                            Category = string.Empty;
                            IsPartOfProdStatus = string.Empty;
                            Quantity = string.Empty;
                            IntlInspQty = string.Empty;
                            ItemAcceptedQty = string.Empty;
                            PE = string.Empty;
                            PM = string.Empty;
                            ProductionManager = string.Empty;
                            QualityManager = string.Empty;
                            PlanningManager = string.Empty;



                            SRNo++;

                            if (drn["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TF_NO"])))
                                TFNo = Convert.ToString(drn["TF_NO"]);
                            if (drn["UNIT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UNIT"])))
                                Unit = Convert.ToString(drn["UNIT"]);
                            if (drn["LOT_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["LOT_DATE"])))
                                LotDate = Convert.ToString(drn["LOT_DATE"]);
                            if (drn["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CUSTOMER_NAME"])))
                                CustomerName = Convert.ToString(drn["CUSTOMER_NAME"]);
                            if (drn["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["JOB_NO"])))
                                JobNo = Convert.ToString(drn["JOB_NO"]);
                            if (drn["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PO_NO"])))
                                PONo = Convert.ToString(drn["PO_NO"]);
                            if (drn["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_NAME"])))
                                ItemName = Convert.ToString(drn["ITEM_NAME"]);
                            if (drn["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_BY"])))
                                CreatedBy = Convert.ToString(drn["CREATED_BY"]);
                            if (drn["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_ON"])))
                                CreatedOn = Convert.ToString(drn["CREATED_ON"]);
                            if (drn["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IMP_NOTES"])))
                                ImpNotes = Convert.ToString(drn["IMP_NOTES"]);
                            if (drn["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["STATUS"])))
                                Status = Convert.ToString(drn["STATUS"]);
                            if (drn["PROD_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_NO"])))
                                ProdOrderNo = Convert.ToString(drn["PROD_ORDER_NO"]);
                            if (drn["PROD_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_DATE"])))
                                ProdOrderDate = Convert.ToString(drn["PROD_ORDER_DATE"]);
                            if (drn["EC_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["EC_DATE"])))
                                EEDate = Convert.ToString(drn["EC_DATE"]);
                            if (drn["PROD_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_CODE"])))
                                ProdCode = Convert.ToString(drn["PROD_CODE"]);
                            if (drn["PROD_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_DESC"])))
                                ProdDesc = Convert.ToString(drn["PROD_DESC"]);
                            if (drn["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UOM"])))
                                UOM = Convert.ToString(drn["UOM"]);
                            if (drn["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["SUBITEM_DESC"])))
                                SubitemDesc = Convert.ToString(drn["SUBITEM_DESC"]);
                            if (drn["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TAG_NO"])))
                                TagNo = Convert.ToString(drn["TAG_NO"]);
                            if (drn["MAIN_ITEM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["MAIN_ITEM"])))
                                MainItem = Convert.ToString(drn["MAIN_ITEM"]);
                            if (drn["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DRAWING_NO"])))
                                DrawingNo = Convert.ToString(drn["DRAWING_NO"]);
                            if (drn["REVISION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["REVISION_NO"])))
                                RevisionNo = Convert.ToString(drn["REVISION_NO"]);
                            if (drn["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CATEGORY"])))
                                Category = Convert.ToString(drn["CATEGORY"]);
                            if (drn["IS_PART_OF_PROD_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IS_PART_OF_PROD_STATUS"])))
                                IsPartOfProdStatus = Convert.ToString(drn["IS_PART_OF_PROD_STATUS"]);
                            if (drn["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUANTITY"])))
                                Quantity = Convert.ToString(drn["QUANTITY"]);
                            if (drn["INTL_INSP_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["INTL_INSP_QTY"])))
                                IntlInspQty = Convert.ToString(drn["INTL_INSP_QTY"]);
                            if (drn["ITEM_ACCEPTED_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_ACCEPTED_QTY"])))
                                ItemAcceptedQty = Convert.ToString(drn["ITEM_ACCEPTED_QTY"]);
                            if (drn["PE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PE"])))
                                PE = Convert.ToString(drn["PE"]);
                            if (drn["PM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PM"])))
                                PM = Convert.ToString(drn["PM"]);
                            if (drn["PRODUCTION_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PRODUCTION_MANAGER"])))
                                ProductionManager = Convert.ToString(drn["PRODUCTION_MANAGER"]);
                            if (drn["QUALITY_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUALITY_MANAGER"])))
                                QualityManager = Convert.ToString(drn["QUALITY_MANAGER"]);
                            if (drn["PLANNING_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PLANNING_MANAGER"])))
                                PlanningManager = Convert.ToString(drn["PLANNING_MANAGER"]);


                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsr'>" + SRNo + "</td>\n");
                            sb.Append("<td class='tdtfno'>" + TFNo + "</td>\n");
                            sb.Append("<td class='tdunit'>" + Unit + "</td>\n");
                            sb.Append("<td class='tdlotdate'>" + LotDate + "</td>\n");
                            sb.Append("<td class='tdcustomer'>" + CustomerName + "</td>\n");
                            sb.Append("<td class='tdjobno'>" + JobNo + "</td>\n");
                            sb.Append("<td class='tdpono'>" + PONo + "</td>\n");
                            sb.Append("<td class='tditem'>" + ItemName + "</td>\n");
                            sb.Append("<td class='tdcreatedby'>" + CreatedBy + "</td>\n");
                            sb.Append("<td class='tdcreatedon'>" + CreatedOn + "</td>\n");
                            sb.Append("<td class='tdimpnotes'>" + ImpNotes + "</td>\n");
                            sb.Append("<td class='tdprodorderno'>" + ProdOrderNo + "</td>\n");
                            sb.Append("<td class='tdprodorderdate'>" + ProdOrderDate + "</td>\n");
                            sb.Append("<td class='tdecdate'>" + EEDate + "</td>\n");
                            sb.Append("<td class='tdprodcode'>" + ProdCode + "</td>\n");
                            sb.Append("<td class='tdproddesc'>" + ProdDesc + "</td>\n");
                            sb.Append("<td class='tduom'>" + UOM + "</td>\n");
                            sb.Append("<td class='tdsubitemdesc'>" + SubitemDesc + "</td>\n");
                            sb.Append("<td class='tdtagno'>" + TagNo + "</td>\n");
                            sb.Append("<td class='tdmainitem'>" + MainItem + "</td>\n");
                            sb.Append("<td class='tddrawingno'>" + DrawingNo + "</td>\n");
                            sb.Append("<td class='tdrevno'>" + RevisionNo + "</td>\n");
                            sb.Append("<td class='tdcategory'>" + Category + "</td>\n");
                            sb.Append("<td class='tdispartofprod'>" + IsPartOfProdStatus + "</td>\n");
                            sb.Append("<td class='tdqty'>" + Quantity + "</td>\n");
                            sb.Append("<td class='tdprodm'>" + ProductionManager + "</td>\n");
                            sb.Append("</tr>\n");

                        }
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                }

                #endregion



                #region ACCEPTED[==============]

                SRNo = 0;
                foreach (DataRow dr in dtReport.Rows)
                {
                    if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                        Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        acceptedCount++;
                    }
                }

                if (acceptedCount > 0)
                {
                    if (newCount > 0 || approvedCount > 0 || planningAcceptedCount > 0 || planningForwardCount > 0)
                    {
                        acceptedNextPage = true;
                    }

                    if (acceptedNextPage == true)
                    {
                        sb.Append("<div style='page-break-after:always;'></div>\n");
                    }

                    sb.Append("<h1 class='headerStyle'>Pending on Quality Manager -For LOT Acceptance</h1>\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsr'>Sr</th>\n");
                    sb.Append("<th class='tdtfno'>TF No.</th>\n");
                    sb.Append("<th class='tdunit'>Unit</th>\n");
                    sb.Append("<th class='tdlotdate'>LOT Date</th>\n");
                    sb.Append("<th class='tdcustomer'>Customer</th>\n");
                    sb.Append("<th class='tdjobno'>JOB No</th>\n");
                    sb.Append("<th class='tdpono'>PO No</th>\n");
                    sb.Append("<th class='tditem'>Item</th>\n");
                    sb.Append("<th class='tdcreatedby'>Created By</th>\n");
                    sb.Append("<th class='tdcreatedon'>Created On</th>\n");
                    sb.Append("<th class='tdimpnotes'>Imp Notes</th>\n");
                    sb.Append("<th class='tdprodorderno'>Prod Order No</th>\n");
                    sb.Append("<th class='tdprodorderdate'>Prod Order Date</th>\n");
                    sb.Append("<th class='tdecdate'>EC Date</th>\n");
                    sb.Append("<th class='tdprodcode'>Prod Code</th>\n");
                    sb.Append("<th class='tdproddesc'>Prod Desc</th>\n");
                    sb.Append("<th class='tduom'>UOM</th>\n");
                    sb.Append("<th class='tdsubitemdesc'>Subitem Desc</th>\n");
                    sb.Append("<th class='tdtagno'>Tag No</th>\n");
                    sb.Append("<th class='tdmainitem'>Main Item</th>\n");
                    sb.Append("<th class='tddrawingno'>Drawing No</th>\n");
                    sb.Append("<th class='tdrevno'>Rev No</th>\n");
                    sb.Append("<th class='tdcategory'>Category</th>\n");
                    sb.Append("<th class='tdispartofprod'>Is Part Of Prod </th>\n");
                    sb.Append("<th class='tdqty'>Quantity</th>\n");
                    sb.Append("<th class='tdpm'>Quality Manager</th>\n");
                    sb.Append("</tr>\n");


                    foreach (DataRow drn in dtReport.Rows)
                    {
                        if (Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                            Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                        {
                            TFNo = string.Empty;
                            Unit = string.Empty;
                            LotDate = string.Empty;
                            CustomerName = string.Empty;
                            JobNo = string.Empty;
                            PONo = string.Empty;
                            ItemName = string.Empty;
                            CreatedBy = string.Empty;
                            CreatedOn = string.Empty;
                            ImpNotes = string.Empty;
                            Status = string.Empty;
                            ProdOrderNo = string.Empty;
                            ProdOrderDate = string.Empty;
                            EEDate = string.Empty;
                            ProdCode = string.Empty;
                            ProdDesc = string.Empty;
                            UOM = string.Empty;
                            SubitemDesc = string.Empty;
                            TagNo = string.Empty;
                            MainItem = string.Empty;
                            DrawingNo = string.Empty;
                            RevisionNo = string.Empty;
                            Category = string.Empty;
                            IsPartOfProdStatus = string.Empty;
                            Quantity = string.Empty;
                            IntlInspQty = string.Empty;
                            ItemAcceptedQty = string.Empty;
                            PE = string.Empty;
                            PM = string.Empty;
                            ProductionManager = string.Empty;
                            QualityManager = string.Empty;



                            SRNo++;

                            if (drn["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TF_NO"])))
                                TFNo = Convert.ToString(drn["TF_NO"]);
                            if (drn["UNIT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UNIT"])))
                                Unit = Convert.ToString(drn["UNIT"]);
                            if (drn["LOT_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["LOT_DATE"])))
                                LotDate = Convert.ToString(drn["LOT_DATE"]);
                            if (drn["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CUSTOMER_NAME"])))
                                CustomerName = Convert.ToString(drn["CUSTOMER_NAME"]);
                            if (drn["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["JOB_NO"])))
                                JobNo = Convert.ToString(drn["JOB_NO"]);
                            if (drn["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PO_NO"])))
                                PONo = Convert.ToString(drn["PO_NO"]);
                            if (drn["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_NAME"])))
                                ItemName = Convert.ToString(drn["ITEM_NAME"]);
                            if (drn["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_BY"])))
                                CreatedBy = Convert.ToString(drn["CREATED_BY"]);
                            if (drn["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_ON"])))
                                CreatedOn = Convert.ToString(drn["CREATED_ON"]);
                            if (drn["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IMP_NOTES"])))
                                ImpNotes = Convert.ToString(drn["IMP_NOTES"]);
                            if (drn["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["STATUS"])))
                                Status = Convert.ToString(drn["STATUS"]);
                            if (drn["PROD_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_NO"])))
                                ProdOrderNo = Convert.ToString(drn["PROD_ORDER_NO"]);
                            if (drn["PROD_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_DATE"])))
                                ProdOrderDate = Convert.ToString(drn["PROD_ORDER_DATE"]);
                            if (drn["EC_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["EC_DATE"])))
                                EEDate = Convert.ToString(drn["EC_DATE"]);
                            if (drn["PROD_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_CODE"])))
                                ProdCode = Convert.ToString(drn["PROD_CODE"]);
                            if (drn["PROD_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_DESC"])))
                                ProdDesc = Convert.ToString(drn["PROD_DESC"]);
                            if (drn["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UOM"])))
                                UOM = Convert.ToString(drn["UOM"]);
                            if (drn["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["SUBITEM_DESC"])))
                                SubitemDesc = Convert.ToString(drn["SUBITEM_DESC"]);
                            if (drn["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TAG_NO"])))
                                TagNo = Convert.ToString(drn["TAG_NO"]);
                            if (drn["MAIN_ITEM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["MAIN_ITEM"])))
                                MainItem = Convert.ToString(drn["MAIN_ITEM"]);
                            if (drn["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DRAWING_NO"])))
                                DrawingNo = Convert.ToString(drn["DRAWING_NO"]);
                            if (drn["REVISION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["REVISION_NO"])))
                                RevisionNo = Convert.ToString(drn["REVISION_NO"]);
                            if (drn["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CATEGORY"])))
                                Category = Convert.ToString(drn["CATEGORY"]);
                            if (drn["IS_PART_OF_PROD_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IS_PART_OF_PROD_STATUS"])))
                                IsPartOfProdStatus = Convert.ToString(drn["IS_PART_OF_PROD_STATUS"]);
                            if (drn["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUANTITY"])))
                                Quantity = Convert.ToString(drn["QUANTITY"]);
                            if (drn["INTL_INSP_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["INTL_INSP_QTY"])))
                                IntlInspQty = Convert.ToString(drn["INTL_INSP_QTY"]);
                            if (drn["ITEM_ACCEPTED_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_ACCEPTED_QTY"])))
                                ItemAcceptedQty = Convert.ToString(drn["ITEM_ACCEPTED_QTY"]);
                            if (drn["PE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PE"])))
                                PE = Convert.ToString(drn["PE"]);
                            if (drn["PM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PM"])))
                                PM = Convert.ToString(drn["PM"]);
                            if (drn["PRODUCTION_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PRODUCTION_MANAGER"])))
                                ProductionManager = Convert.ToString(drn["PRODUCTION_MANAGER"]);
                            if (drn["QUALITY_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUALITY_MANAGER"])))
                                QualityManager = Convert.ToString(drn["QUALITY_MANAGER"]);

                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsr'>" + SRNo + "</td>\n");
                            sb.Append("<td class='tdtfno'>" + TFNo + "</td>\n");
                            sb.Append("<td class='tdunit'>" + Unit + "</td>\n");
                            sb.Append("<td class='tdlotdate'>" + LotDate + "</td>\n");
                            sb.Append("<td class='tdcustomer'>" + CustomerName + "</td>\n");
                            sb.Append("<td class='tdjobno'>" + JobNo + "</td>\n");
                            sb.Append("<td class='tdpono'>" + PONo + "</td>\n");
                            sb.Append("<td class='tditem'>" + ItemName + "</td>\n");
                            sb.Append("<td class='tdcreatedby'>" + CreatedBy + "</td>\n");
                            sb.Append("<td class='tdcreatedon'>" + CreatedOn + "</td>\n");
                            sb.Append("<td class='tdimpnotes'>" + ImpNotes + "</td>\n");
                            sb.Append("<td class='tdprodorderno'>" + ProdOrderNo + "</td>\n");
                            sb.Append("<td class='tdprodorderdate'>" + ProdOrderDate + "</td>\n");
                            sb.Append("<td class='tdecdate'>" + EEDate + "</td>\n");
                            sb.Append("<td class='tdprodcode'>" + ProdCode + "</td>\n");
                            sb.Append("<td class='tdproddesc'>" + ProdDesc + "</td>\n");
                            sb.Append("<td class='tduom'>" + UOM + "</td>\n");
                            sb.Append("<td class='tdsubitemdesc'>" + SubitemDesc + "</td>\n");
                            sb.Append("<td class='tdtagno'>" + TagNo + "</td>\n");
                            sb.Append("<td class='tdmainitem'>" + MainItem + "</td>\n");
                            sb.Append("<td class='tddrawingno'>" + DrawingNo + "</td>\n");
                            sb.Append("<td class='tdrevno'>" + RevisionNo + "</td>\n");
                            sb.Append("<td class='tdcategory'>" + Category + "</td>\n");
                            sb.Append("<td class='tdispartofprod'>" + IsPartOfProdStatus + "</td>\n");
                            sb.Append("<td class='tdqty'>" + Quantity + "</td>\n");
                            sb.Append("<td class='tdqm'>" + QualityManager + "</td>\n");
                            sb.Append("</tr>\n");

                        }
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                }

                #endregion


                #region QA-ACCEPTED[===========]

                SRNo = 0;
                foreach (DataRow dr in dtReport.Rows)
                {
                    if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                        Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted) ||
                        Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted))
                    {
                        qaAcceptedCount++;
                    }
                }

                if (qaAcceptedCount > 0)
                {
                    if (newCount > 0 || approvedCount > 0 || planningAcceptedCount > 0 || planningForwardCount > 0 || acceptedCount > 0)
                    {
                        //approvedNextPage = true;
                        //acceptedNextPage = true;
                        QaAcceptedNextPage = true;
                    }

                    if (QaAcceptedNextPage == true)
                    {
                        sb.Append("<div style='page-break-after:always;'></div>\n");
                    }

                    sb.Append("<h1 class='headerStyle'>Pending on Production Manager -To Send Items for Internal Inspection</h1>\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsr'>Sr</th>\n");
                    sb.Append("<th class='tdtfno'>TF No.</th>\n");
                    sb.Append("<th class='tdunit'>Unit</th>\n");
                    sb.Append("<th class='tdlotdate'>LOT Date</th>\n");
                    sb.Append("<th class='tdcustomer'>Customer</th>\n");
                    sb.Append("<th class='tdjobno'>JOB No</th>\n");
                    sb.Append("<th class='tdpono'>PO No</th>\n");
                    sb.Append("<th class='tditem'>Item</th>\n");
                    sb.Append("<th class='tdcreatedby'>Created By</th>\n");
                    sb.Append("<th class='tdcreatedon'>Created On</th>\n");
                    sb.Append("<th class='tdimpnotes'>Imp Notes</th>\n");
                    sb.Append("<th class='tdprodorderno'>Prod Order No</th>\n");
                    sb.Append("<th class='tdprodorderdate'>Prod Order Date</th>\n");
                    sb.Append("<th class='tdecdate'>EC Date</th>\n");
                    sb.Append("<th class='tdprodcode'>Prod Code</th>\n");
                    sb.Append("<th class='tdproddesc'>Prod Desc</th>\n");
                    sb.Append("<th class='tduom'>UOM</th>\n");
                    sb.Append("<th class='tdsubitemdesc'>Subitem Desc</th>\n");
                    sb.Append("<th class='tdtagno'>Tag No</th>\n");
                    sb.Append("<th class='tdmainitem'>Main Item</th>\n");
                    sb.Append("<th class='tddrawingno'>Drawing No</th>\n");
                    sb.Append("<th class='tdrevno'>Rev No</th>\n");
                    sb.Append("<th class='tdcategory'>Category</th>\n");
                    sb.Append("<th class='tdispartofprod'>Is Part Of Prod </th>\n");
                    sb.Append("<th class='tdqty'>Quantity</th>\n");
                    sb.Append("<th class='tdpm'>Prod Manager</th>\n");
                    sb.Append("</tr>\n");


                    foreach (DataRow drn in dtReport.Rows)
                    {
                        if (Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                            Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted) ||
                            Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted))
                        {
                            TFNo = string.Empty;
                            Unit = string.Empty;
                            LotDate = string.Empty;
                            CustomerName = string.Empty;
                            JobNo = string.Empty;
                            PONo = string.Empty;
                            ItemName = string.Empty;
                            CreatedBy = string.Empty;
                            CreatedOn = string.Empty;
                            ImpNotes = string.Empty;
                            Status = string.Empty;
                            ProdOrderNo = string.Empty;
                            ProdOrderDate = string.Empty;
                            EEDate = string.Empty;
                            ProdCode = string.Empty;
                            ProdDesc = string.Empty;
                            UOM = string.Empty;
                            SubitemDesc = string.Empty;
                            TagNo = string.Empty;
                            MainItem = string.Empty;
                            DrawingNo = string.Empty;
                            RevisionNo = string.Empty;
                            Category = string.Empty;
                            IsPartOfProdStatus = string.Empty;
                            Quantity = string.Empty;
                            IntlInspQty = string.Empty;
                            ItemAcceptedQty = string.Empty;
                            PE = string.Empty;
                            PM = string.Empty;
                            ProductionManager = string.Empty;
                            QualityManager = string.Empty;



                            SRNo++;

                            if (drn["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TF_NO"])))
                                TFNo = Convert.ToString(drn["TF_NO"]);
                            if (drn["UNIT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UNIT"])))
                                Unit = Convert.ToString(drn["UNIT"]);
                            if (drn["LOT_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["LOT_DATE"])))
                                LotDate = Convert.ToString(drn["LOT_DATE"]);
                            if (drn["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CUSTOMER_NAME"])))
                                CustomerName = Convert.ToString(drn["CUSTOMER_NAME"]);
                            if (drn["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["JOB_NO"])))
                                JobNo = Convert.ToString(drn["JOB_NO"]);
                            if (drn["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PO_NO"])))
                                PONo = Convert.ToString(drn["PO_NO"]);
                            if (drn["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_NAME"])))
                                ItemName = Convert.ToString(drn["ITEM_NAME"]);
                            if (drn["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_BY"])))
                                CreatedBy = Convert.ToString(drn["CREATED_BY"]);
                            if (drn["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_ON"])))
                                CreatedOn = Convert.ToString(drn["CREATED_ON"]);
                            if (drn["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IMP_NOTES"])))
                                ImpNotes = Convert.ToString(drn["IMP_NOTES"]);
                            if (drn["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["STATUS"])))
                                Status = Convert.ToString(drn["STATUS"]);
                            if (drn["PROD_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_NO"])))
                                ProdOrderNo = Convert.ToString(drn["PROD_ORDER_NO"]);
                            if (drn["PROD_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_DATE"])))
                                ProdOrderDate = Convert.ToString(drn["PROD_ORDER_DATE"]);
                            if (drn["EC_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["EC_DATE"])))
                                EEDate = Convert.ToString(drn["EC_DATE"]);
                            if (drn["PROD_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_CODE"])))
                                ProdCode = Convert.ToString(drn["PROD_CODE"]);
                            if (drn["PROD_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_DESC"])))
                                ProdDesc = Convert.ToString(drn["PROD_DESC"]);
                            if (drn["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UOM"])))
                                UOM = Convert.ToString(drn["UOM"]);
                            if (drn["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["SUBITEM_DESC"])))
                                SubitemDesc = Convert.ToString(drn["SUBITEM_DESC"]);
                            if (drn["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TAG_NO"])))
                                TagNo = Convert.ToString(drn["TAG_NO"]);
                            if (drn["MAIN_ITEM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["MAIN_ITEM"])))
                                MainItem = Convert.ToString(drn["MAIN_ITEM"]);
                            if (drn["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DRAWING_NO"])))
                                DrawingNo = Convert.ToString(drn["DRAWING_NO"]);
                            if (drn["REVISION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["REVISION_NO"])))
                                RevisionNo = Convert.ToString(drn["REVISION_NO"]);
                            if (drn["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CATEGORY"])))
                                Category = Convert.ToString(drn["CATEGORY"]);
                            if (drn["IS_PART_OF_PROD_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IS_PART_OF_PROD_STATUS"])))
                                IsPartOfProdStatus = Convert.ToString(drn["IS_PART_OF_PROD_STATUS"]);
                            if (drn["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUANTITY"])))
                                Quantity = Convert.ToString(drn["QUANTITY"]);
                            if (drn["INTL_INSP_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["INTL_INSP_QTY"])))
                                IntlInspQty = Convert.ToString(drn["INTL_INSP_QTY"]);
                            if (drn["ITEM_ACCEPTED_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_ACCEPTED_QTY"])))
                                ItemAcceptedQty = Convert.ToString(drn["ITEM_ACCEPTED_QTY"]);
                            if (drn["PE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PE"])))
                                PE = Convert.ToString(drn["PE"]);
                            if (drn["PM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PM"])))
                                PM = Convert.ToString(drn["PM"]);
                            if (drn["PRODUCTION_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PRODUCTION_MANAGER"])))
                                ProductionManager = Convert.ToString(drn["PRODUCTION_MANAGER"]);
                            if (drn["QUALITY_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUALITY_MANAGER"])))
                                QualityManager = Convert.ToString(drn["QUALITY_MANAGER"]);



                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsr'>" + SRNo + "</td>\n");
                            sb.Append("<td class='tdtfno'>" + TFNo + "</td>\n");
                            sb.Append("<td class='tdunit'>" + Unit + "</td>\n");
                            sb.Append("<td class='tdlotdate'>" + LotDate + "</td>\n");
                            sb.Append("<td class='tdcustomer'>" + CustomerName + "</td>\n");
                            sb.Append("<td class='tdjobno'>" + JobNo + "</td>\n");
                            sb.Append("<td class='tdpono'>" + PONo + "</td>\n");
                            sb.Append("<td class='tditem'>" + ItemName + "</td>\n");
                            sb.Append("<td class='tdcreatedby'>" + CreatedBy + "</td>\n");
                            sb.Append("<td class='tdcreatedon'>" + CreatedOn + "</td>\n");
                            sb.Append("<td class='tdimpnotes'>" + ImpNotes + "</td>\n");
                            sb.Append("<td class='tdprodorderno'>" + ProdOrderNo + "</td>\n");
                            sb.Append("<td class='tdprodorderdate'>" + ProdOrderDate + "</td>\n");
                            sb.Append("<td class='tdecdate'>" + EEDate + "</td>\n");
                            sb.Append("<td class='tdprodcode'>" + ProdCode + "</td>\n");
                            sb.Append("<td class='tdproddesc'>" + ProdDesc + "</td>\n");
                            sb.Append("<td class='tduom'>" + UOM + "</td>\n");
                            sb.Append("<td class='tdsubitemdesc'>" + SubitemDesc + "</td>\n");
                            sb.Append("<td class='tdtagno'>" + TagNo + "</td>\n");
                            sb.Append("<td class='tdmainitem'>" + MainItem + "</td>\n");
                            sb.Append("<td class='tddrawingno'>" + DrawingNo + "</td>\n");
                            sb.Append("<td class='tdrevno'>" + RevisionNo + "</td>\n");
                            sb.Append("<td class='tdcategory'>" + Category + "</td>\n");
                            sb.Append("<td class='tdispartofprod'>" + IsPartOfProdStatus + "</td>\n");
                            sb.Append("<td class='tdqty'>" + Quantity + "</td>\n");
                            sb.Append("<td class='tdprodm'>" + ProductionManager + "</td>\n");
                            sb.Append("</tr>\n");

                        }
                    }


                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                }

                #endregion


                #region INTL-INSP[=============]

                SRNo = 0;
                foreach (DataRow dr in dtReport.Rows)
                {
                    if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection))
                    {
                        internalinspCount++;
                    }
                }

                if (internalinspCount > 0)
                {
                    if (newCount > 0 || approvedCount > 0 || planningAcceptedCount > 0 || planningForwardCount > 0 || qaAcceptedCount > 0)
                    {
                        //approvedNextPage = true;
                        //acceptedNextPage = true;
                        //QaAcceptedNextPage = true;
                        IINextPage = true;
                    }

                    if (IINextPage == true)
                    {
                        sb.Append("<div style='page-break-after:always;'></div>\n");
                    }

                    sb.Append("<h1 class='headerStyle'>Pending on Quality Manager -To Accept Item(s) for Inspection</h1>\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");

                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsr'>Sr</th>\n");
                    sb.Append("<th class='tdtfno'>TF No.</th>\n");
                    sb.Append("<th class='tdunit'>Unit</th>\n");
                    sb.Append("<th class='tdlotdate'>LOT Date</th>\n");
                    sb.Append("<th class='tdcustomer'>Customer</th>\n");
                    sb.Append("<th class='tdjobno'>JOB No</th>\n");
                    sb.Append("<th class='tdpono'>PO No</th>\n");
                    sb.Append("<th class='tditem'>Item</th>\n");
                    sb.Append("<th class='tdcreatedby'>Created By</th>\n");
                    sb.Append("<th class='tdcreatedon'>Created On</th>\n");
                    sb.Append("<th class='tdimpnotes'>Imp Notes</th>\n");
                    sb.Append("<th class='tdprodorderno'>Prod Order No</th>\n");
                    sb.Append("<th class='tdprodorderdate'>Prod Order Date</th>\n");
                    sb.Append("<th class='tdecdate'>EC Date</th>\n");
                    sb.Append("<th class='tdprodcode'>Prod Code</th>\n");
                    sb.Append("<th class='tdproddesc'>Prod Desc</th>\n");
                    sb.Append("<th class='tduom'>UOM</th>\n");
                    sb.Append("<th class='tdsubitemdesc'>Subitem Desc</th>\n");
                    sb.Append("<th class='tdtagno'>Tag No</th>\n");
                    sb.Append("<th class='tdmainitem'>Main Item</th>\n");
                    sb.Append("<th class='tddrawingno'>Drawing No</th>\n");
                    sb.Append("<th class='tdrevno'>Rev No</th>\n");
                    sb.Append("<th class='tdcategory'>Category</th>\n");
                    sb.Append("<th class='tdispartofprod'>Is Part Of Prod </th>\n");
                    sb.Append("<th class='tdqty'>Quantity</th>\n");
                    sb.Append("<th class='tdiiqty'>Intl Insp Qty</th>\n");
                    sb.Append("<th class='tdqiaqty'>Item Accepted Qty</th>\n");
                    sb.Append("<th class='tdprodm'>Prod Manager</th>\n");
                    sb.Append("<th class='tdqm'>Quality Manager</th>\n");
                    sb.Append("</tr>\n");


                    foreach (DataRow drn in dtReport.Rows)
                    {
                        if (Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection))
                        {
                            TFNo = string.Empty;
                            Unit = string.Empty;
                            LotDate = string.Empty;
                            CustomerName = string.Empty;
                            JobNo = string.Empty;
                            PONo = string.Empty;
                            ItemName = string.Empty;
                            CreatedBy = string.Empty;
                            CreatedOn = string.Empty;
                            ImpNotes = string.Empty;
                            Status = string.Empty;
                            ProdOrderNo = string.Empty;
                            ProdOrderDate = string.Empty;
                            EEDate = string.Empty;
                            ProdCode = string.Empty;
                            ProdDesc = string.Empty;
                            UOM = string.Empty;
                            SubitemDesc = string.Empty;
                            TagNo = string.Empty;
                            MainItem = string.Empty;
                            DrawingNo = string.Empty;
                            RevisionNo = string.Empty;
                            Category = string.Empty;
                            IsPartOfProdStatus = string.Empty;
                            Quantity = string.Empty;
                            IntlInspQty = string.Empty;
                            ItemAcceptedQty = string.Empty;
                            PE = string.Empty;
                            PM = string.Empty;
                            ProductionManager = string.Empty;
                            QualityManager = string.Empty;



                            SRNo++;

                            if (drn["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TF_NO"])))
                                TFNo = Convert.ToString(drn["TF_NO"]);
                            if (drn["UNIT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UNIT"])))
                                Unit = Convert.ToString(drn["UNIT"]);
                            if (drn["LOT_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["LOT_DATE"])))
                                LotDate = Convert.ToString(drn["LOT_DATE"]);
                            if (drn["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CUSTOMER_NAME"])))
                                CustomerName = Convert.ToString(drn["CUSTOMER_NAME"]);
                            if (drn["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["JOB_NO"])))
                                JobNo = Convert.ToString(drn["JOB_NO"]);
                            if (drn["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PO_NO"])))
                                PONo = Convert.ToString(drn["PO_NO"]);
                            if (drn["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_NAME"])))
                                ItemName = Convert.ToString(drn["ITEM_NAME"]);
                            if (drn["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_BY"])))
                                CreatedBy = Convert.ToString(drn["CREATED_BY"]);
                            if (drn["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_ON"])))
                                CreatedOn = Convert.ToString(drn["CREATED_ON"]);
                            if (drn["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IMP_NOTES"])))
                                ImpNotes = Convert.ToString(drn["IMP_NOTES"]);
                            if (drn["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["STATUS"])))
                                Status = Convert.ToString(drn["STATUS"]);
                            if (drn["PROD_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_NO"])))
                                ProdOrderNo = Convert.ToString(drn["PROD_ORDER_NO"]);
                            if (drn["PROD_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_DATE"])))
                                ProdOrderDate = Convert.ToString(drn["PROD_ORDER_DATE"]);
                            if (drn["EC_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["EC_DATE"])))
                                EEDate = Convert.ToString(drn["EC_DATE"]);
                            if (drn["PROD_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_CODE"])))
                                ProdCode = Convert.ToString(drn["PROD_CODE"]);
                            if (drn["PROD_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_DESC"])))
                                ProdDesc = Convert.ToString(drn["PROD_DESC"]);
                            if (drn["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UOM"])))
                                UOM = Convert.ToString(drn["UOM"]);
                            if (drn["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["SUBITEM_DESC"])))
                                SubitemDesc = Convert.ToString(drn["SUBITEM_DESC"]);
                            if (drn["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TAG_NO"])))
                                TagNo = Convert.ToString(drn["TAG_NO"]);
                            if (drn["MAIN_ITEM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["MAIN_ITEM"])))
                                MainItem = Convert.ToString(drn["MAIN_ITEM"]);
                            if (drn["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DRAWING_NO"])))
                                DrawingNo = Convert.ToString(drn["DRAWING_NO"]);
                            if (drn["REVISION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["REVISION_NO"])))
                                RevisionNo = Convert.ToString(drn["REVISION_NO"]);
                            if (drn["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CATEGORY"])))
                                Category = Convert.ToString(drn["CATEGORY"]);
                            if (drn["IS_PART_OF_PROD_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IS_PART_OF_PROD_STATUS"])))
                                IsPartOfProdStatus = Convert.ToString(drn["IS_PART_OF_PROD_STATUS"]);
                            if (drn["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUANTITY"])))
                                Quantity = Convert.ToString(drn["QUANTITY"]);
                            if (drn["INTL_INSP_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["INTL_INSP_QTY"])))
                                IntlInspQty = Convert.ToString(drn["INTL_INSP_QTY"]);
                            if (drn["ITEM_ACCEPTED_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_ACCEPTED_QTY"])))
                                ItemAcceptedQty = Convert.ToString(drn["ITEM_ACCEPTED_QTY"]);
                            if (drn["PE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PE"])))
                                PE = Convert.ToString(drn["PE"]);
                            if (drn["PM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PM"])))
                                PM = Convert.ToString(drn["PM"]);
                            if (drn["PRODUCTION_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PRODUCTION_MANAGER"])))
                                ProductionManager = Convert.ToString(drn["PRODUCTION_MANAGER"]);
                            if (drn["QUALITY_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUALITY_MANAGER"])))
                                QualityManager = Convert.ToString(drn["QUALITY_MANAGER"]);


                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsr'>" + SRNo + "</td>\n");
                            sb.Append("<td class='tdtfno'>" + TFNo + "</td>\n");
                            sb.Append("<td class='tdunit'>" + Unit + "</td>\n");
                            sb.Append("<td class='tdlotdate'>" + LotDate + "</td>\n");
                            sb.Append("<td class='tdcustomer'>" + CustomerName + "</td>\n");
                            sb.Append("<td class='tdjobno'>" + JobNo + "</td>\n");
                            sb.Append("<td class='tdpono'>" + PONo + "</td>\n");
                            sb.Append("<td class='tditem'>" + ItemName + "</td>\n");
                            sb.Append("<td class='tdcreatedby'>" + CreatedBy + "</td>\n");
                            sb.Append("<td class='tdcreatedon'>" + CreatedOn + "</td>\n");
                            sb.Append("<td class='tdimpnotes'>" + ImpNotes + "</td>\n");
                            sb.Append("<td class='tdprodorderno'>" + ProdOrderNo + "</td>\n");
                            sb.Append("<td class='tdprodorderdate'>" + ProdOrderDate + "</td>\n");
                            sb.Append("<td class='tdecdate'>" + EEDate + "</td>\n");
                            sb.Append("<td class='tdprodcode'>" + ProdCode + "</td>\n");
                            sb.Append("<td class='tdproddesc'>" + ProdDesc + "</td>\n");
                            sb.Append("<td class='tduom'>" + UOM + "</td>\n");
                            sb.Append("<td class='tdsubitemdesc'>" + SubitemDesc + "</td>\n");
                            sb.Append("<td class='tdtagno'>" + TagNo + "</td>\n");
                            sb.Append("<td class='tdmainitem'>" + MainItem + "</td>\n");
                            sb.Append("<td class='tddrawingno'>" + DrawingNo + "</td>\n");
                            sb.Append("<td class='tdrevno'>" + RevisionNo + "</td>\n");
                            sb.Append("<td class='tdcategory'>" + Category + "</td>\n");
                            sb.Append("<td class='tdispartofprod'>" + IsPartOfProdStatus + "</td>\n");
                            sb.Append("<td class='tdqty'>" + Quantity + "</td>\n");
                            sb.Append("<td class='tdiiqty'>" + IntlInspQty + "</td>\n");
                            sb.Append("<td class='tdqiaqty'>" + ItemAcceptedQty + "</td>\n");
                            sb.Append("<td class='tdprodm'>" + ProductionManager + "</td>\n");
                            sb.Append("<td class='tdqm'>" + QualityManager + "</td>\n");
                            sb.Append("</tr>\n");
                        }
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                }

                #endregion


                #region ITEM-ACCEPTED[=========]

                SRNo = 0;
                foreach (DataRow dr in dtReport.Rows)
                {
                    if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                    {
                        qaItemAcceptedCount++;
                    }
                }

                if (qaItemAcceptedCount > 0)
                {
                    if (newCount > 0 || approvedCount > 0 || planningAcceptedCount > 0 || planningForwardCount > 0 || internalinspCount > 0)
                    {                        
                        QIANextPage = true;
                    }

                    if (QIANextPage == true)
                    {
                        sb.Append("<div style='page-break-after:always;'></div>\n");
                    }

                    sb.Append("<h1 class='headerStyle'>Pending on Quality Manager -For Item(s) Completion</h1>\n");
                    sb.Append("<fieldset class='pdffieldset'>\n");
                   
                    sb.Append("<table class='tblsuitems'  style='margin-top:20px; ; border-collapse:collapse;'>\n");
                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdsr'>Sr</th>\n");
                    sb.Append("<th class='tdtfno'>TF No.</th>\n");
                    sb.Append("<th class='tdunit'>Unit</th>\n");
                    sb.Append("<th class='tdlotdate'>LOT Date</th>\n");
                    sb.Append("<th class='tdcustomer'>Customer</th>\n");
                    sb.Append("<th class='tdjobno'>JOB No</th>\n");
                    sb.Append("<th class='tdpono'>PO No</th>\n");
                    sb.Append("<th class='tditem'>Item</th>\n");
                    sb.Append("<th class='tdcreatedby'>Created By</th>\n");
                    sb.Append("<th class='tdcreatedon'>Created On</th>\n");
                    sb.Append("<th class='tdimpnotes'>Imp Notes</th>\n");
                    sb.Append("<th class='tdprodorderno'>Prod Order No</th>\n");
                    sb.Append("<th class='tdprodorderdate'>Prod Order Date</th>\n");
                    sb.Append("<th class='tdecdate'>EC Date</th>\n");
                    sb.Append("<th class='tdprodcode'>Prod Code</th>\n");
                    sb.Append("<th class='tdproddesc'>Prod Desc</th>\n");
                    sb.Append("<th class='tduom'>UOM</th>\n");
                    sb.Append("<th class='tdsubitemdesc'>Subitem Desc</th>\n");
                    sb.Append("<th class='tdtagno'>Tag No</th>\n");
                    sb.Append("<th class='tdmainitem'>Main Item</th>\n");
                    sb.Append("<th class='tddrawingno'>Drawing No</th>\n");
                    sb.Append("<th class='tdrevno'>Rev No</th>\n");
                    sb.Append("<th class='tdcategory'>Category</th>\n");
                    sb.Append("<th class='tdispartofprod'>Is Part Of Prod </th>\n");
                    sb.Append("<th class='tdqty'>Quantity</th>\n");
                    sb.Append("<th class='tdiiqty'>Intl Insp Qty</th>\n");
                    sb.Append("<th class='tdqiaqty'>Item Accepted Qty</th>\n");
                    sb.Append("<th class='tdprodm'>Prod Manager</th>\n");
                    sb.Append("<th class='tdqm'>Quality Manager</th>\n");
                    sb.Append("</tr>\n");


                    foreach (DataRow drn in dtReport.Rows)
                    {
                        if (Convert.ToInt32(drn["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                        {
                            TFNo = string.Empty;
                            Unit = string.Empty;
                            LotDate = string.Empty;
                            CustomerName = string.Empty;
                            JobNo = string.Empty;
                            PONo = string.Empty;
                            ItemName = string.Empty;
                            CreatedBy = string.Empty;
                            CreatedOn = string.Empty;
                            ImpNotes = string.Empty;
                            Status = string.Empty;
                            ProdOrderNo = string.Empty;
                            ProdOrderDate = string.Empty;
                            EEDate = string.Empty;
                            ProdCode = string.Empty;
                            ProdDesc = string.Empty;
                            UOM = string.Empty;
                            SubitemDesc = string.Empty;
                            TagNo = string.Empty;
                            MainItem = string.Empty;
                            DrawingNo = string.Empty;
                            RevisionNo = string.Empty;
                            Category = string.Empty;
                            IsPartOfProdStatus = string.Empty;
                            Quantity = string.Empty;
                            IntlInspQty = string.Empty;
                            ItemAcceptedQty = string.Empty;
                            PE = string.Empty;
                            PM = string.Empty;
                            ProductionManager = string.Empty;
                            QualityManager = string.Empty;



                            SRNo++;

                            if (drn["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TF_NO"])))
                                TFNo = Convert.ToString(drn["TF_NO"]);
                            if (drn["UNIT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UNIT"])))
                                Unit = Convert.ToString(drn["UNIT"]);
                            if (drn["LOT_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["LOT_DATE"])))
                                LotDate = Convert.ToString(drn["LOT_DATE"]);
                            if (drn["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CUSTOMER_NAME"])))
                                CustomerName = Convert.ToString(drn["CUSTOMER_NAME"]);
                            if (drn["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["JOB_NO"])))
                                JobNo = Convert.ToString(drn["JOB_NO"]);
                            if (drn["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PO_NO"])))
                                PONo = Convert.ToString(drn["PO_NO"]);
                            if (drn["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_NAME"])))
                                ItemName = Convert.ToString(drn["ITEM_NAME"]);
                            if (drn["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_BY"])))
                                CreatedBy = Convert.ToString(drn["CREATED_BY"]);
                            if (drn["CREATED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CREATED_ON"])))
                                CreatedOn = Convert.ToString(drn["CREATED_ON"]);
                            if (drn["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IMP_NOTES"])))
                                ImpNotes = Convert.ToString(drn["IMP_NOTES"]);
                            if (drn["STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["STATUS"])))
                                Status = Convert.ToString(drn["STATUS"]);
                            if (drn["PROD_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_NO"])))
                                ProdOrderNo = Convert.ToString(drn["PROD_ORDER_NO"]);
                            if (drn["PROD_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_ORDER_DATE"])))
                                ProdOrderDate = Convert.ToString(drn["PROD_ORDER_DATE"]);
                            if (drn["EC_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["EC_DATE"])))
                                EEDate = Convert.ToString(drn["EC_DATE"]);
                            if (drn["PROD_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_CODE"])))
                                ProdCode = Convert.ToString(drn["PROD_CODE"]);
                            if (drn["PROD_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PROD_DESC"])))
                                ProdDesc = Convert.ToString(drn["PROD_DESC"]);
                            if (drn["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["UOM"])))
                                UOM = Convert.ToString(drn["UOM"]);
                            if (drn["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["SUBITEM_DESC"])))
                                SubitemDesc = Convert.ToString(drn["SUBITEM_DESC"]);
                            if (drn["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["TAG_NO"])))
                                TagNo = Convert.ToString(drn["TAG_NO"]);
                            if (drn["MAIN_ITEM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["MAIN_ITEM"])))
                                MainItem = Convert.ToString(drn["MAIN_ITEM"]);
                            if (drn["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DRAWING_NO"])))
                                DrawingNo = Convert.ToString(drn["DRAWING_NO"]);
                            if (drn["REVISION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["REVISION_NO"])))
                                RevisionNo = Convert.ToString(drn["REVISION_NO"]);
                            if (drn["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["CATEGORY"])))
                                Category = Convert.ToString(drn["CATEGORY"]);
                            if (drn["IS_PART_OF_PROD_STATUS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["IS_PART_OF_PROD_STATUS"])))
                                IsPartOfProdStatus = Convert.ToString(drn["IS_PART_OF_PROD_STATUS"]);
                            if (drn["QUANTITY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUANTITY"])))
                                Quantity = Convert.ToString(drn["QUANTITY"]);
                            if (drn["INTL_INSP_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["INTL_INSP_QTY"])))
                                IntlInspQty = Convert.ToString(drn["INTL_INSP_QTY"]);
                            if (drn["ITEM_ACCEPTED_QTY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ITEM_ACCEPTED_QTY"])))
                                ItemAcceptedQty = Convert.ToString(drn["ITEM_ACCEPTED_QTY"]);
                            if (drn["PE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PE"])))
                                PE = Convert.ToString(drn["PE"]);
                            if (drn["PM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PM"])))
                                PM = Convert.ToString(drn["PM"]);
                            if (drn["PRODUCTION_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PRODUCTION_MANAGER"])))
                                ProductionManager = Convert.ToString(drn["PRODUCTION_MANAGER"]);
                            if (drn["QUALITY_MANAGER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["QUALITY_MANAGER"])))
                                QualityManager = Convert.ToString(drn["QUALITY_MANAGER"]);


                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsr'>" + SRNo + "</td>\n");
                            sb.Append("<td class='tdtfno'>" + TFNo + "</td>\n");
                            sb.Append("<td class='tdunit'>" + Unit + "</td>\n");
                            sb.Append("<td class='tdlotdate'>" + LotDate + "</td>\n");
                            sb.Append("<td class='tdcustomer'>" + CustomerName + "</td>\n");
                            sb.Append("<td class='tdjobno'>" + JobNo + "</td>\n");
                            sb.Append("<td class='tdpono'>" + PONo + "</td>\n");
                            sb.Append("<td class='tditem'>" + ItemName + "</td>\n");
                            sb.Append("<td class='tdcreatedby'>" + CreatedBy + "</td>\n");
                            sb.Append("<td class='tdcreatedon'>" + CreatedOn + "</td>\n");
                            sb.Append("<td class='tdimpnotes'>" + ImpNotes + "</td>\n");
                            sb.Append("<td class='tdprodorderno'>" + ProdOrderNo + "</td>\n");
                            sb.Append("<td class='tdprodorderdate'>" + ProdOrderDate + "</td>\n");
                            sb.Append("<td class='tdecdate'>" + EEDate + "</td>\n");
                            sb.Append("<td class='tdprodcode'>" + ProdCode + "</td>\n");
                            sb.Append("<td class='tdproddesc'>" + ProdDesc + "</td>\n");
                            sb.Append("<td class='tduom'>" + UOM + "</td>\n");
                            sb.Append("<td class='tdsubitemdesc'>" + SubitemDesc + "</td>\n");
                            sb.Append("<td class='tdtagno'>" + TagNo + "</td>\n");
                            sb.Append("<td class='tdmainitem'>" + MainItem + "</td>\n");
                            sb.Append("<td class='tddrawingno'>" + DrawingNo + "</td>\n");
                            sb.Append("<td class='tdrevno'>" + RevisionNo + "</td>\n");
                            sb.Append("<td class='tdcategory'>" + Category + "</td>\n");
                            sb.Append("<td class='tdispartofprod'>" + IsPartOfProdStatus + "</td>\n");
                            sb.Append("<td class='tdqty'>" + Quantity + "</td>\n");
                            sb.Append("<td class='tdiiqty'>" + IntlInspQty + "</td>\n");
                            sb.Append("<td class='tdqiaqty'>" + ItemAcceptedQty + "</td>\n");
                            sb.Append("<td class='tdprodm'>" + ProductionManager + "</td>\n");
                            sb.Append("<td class='tdqm'>" + QualityManager + "</td>\n");
                            sb.Append("</tr>\n");

                        }
                    }

                    sb.Append("</table>\n");
                    sb.Append("</fieldset>\n");
                }

                #endregion

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