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


public class LOTHtmlForPDFApproval2
{

    string companyName = string.Empty;
    string customerName = string.Empty;
    string custCode = string.Empty;
    string jobNo = string.Empty;
    string TFNo = string.Empty;
    string productionNo = string.Empty;
    string poNo = string.Empty;
    string LOTDate = string.Empty;
    string itemName = string.Empty;
    string impNotes = string.Empty;

    int statusID = 0;
    int PEID = 0;
    int PMID = 0;

    string LOTItemName = string.Empty;

    int approvedByID = 0;
    string approvedBy = string.Empty;
    string approvedOn = string.Empty;
    string approvedRemarks = string.Empty;


    int productionAcceptedByID = 0;
    string productionAcceptedBy = string.Empty;
    string productionAcceptedOn = string.Empty;
    string productionAcceptedRemarks = string.Empty;

    int qualityAcceptedByID = 0;
    string qualityAcceptedBy = string.Empty;
    string qualityAcceptedOn = string.Empty;
    string qualityAcceptedRemarks = string.Empty;

    int planningAcceptedByID = 0;
    string planningAcceptedBy = string.Empty;
    string planningAcceptedOn = string.Empty;
    string planningAcceptedRemarks = string.Empty;


    int amendmentCount = 0;

    int sendToAmendmentByID = 0;
    string sendToAmendmentBy = string.Empty;
    string sendToAmendmentOn = string.Empty;
    string sendToAmendmentRemarks = string.Empty;

    int amendedByID = 0;
    string amendedBy = string.Empty;
    string amendedOn = string.Empty;
    string amendedRemarks = string.Empty;

    int amendedApprovedByID = 0;
    string amendedApprovedBy = string.Empty;
    string amendedApprovedOn = string.Empty;
    string amendedApprovedRemarks = string.Empty;


    int productionAmendedAcceptedByID = 0;
    string productionAmendedAcceptedBy = string.Empty;
    string productionAmendedAcceptedOn = string.Empty;
    string productionAmendedAcceptedRemarks = string.Empty;

    int qualityAmendedAcceptedByID = 0;
    string qualityAmendedAcceptedBy = string.Empty;
    string qualityAmendedAcceptedOn = string.Empty;
    string qualityAmendedAcceptedRemarks = string.Empty;

    int planningAmendedAcceptedByID = 0;
    string planningAmendedAcceptedBy = string.Empty;
    string planningAmendedAcceptedOn = string.Empty;
    string planningAmendedAcceptedRemarks = string.Empty;



    int completedByID = 0;
    string completedBy = string.Empty;
    string completedOn = string.Empty;
    string completedRemarks = string.Empty;


    public string GetHtmlForPDF(DataTable dtLOT, DataTable dtSubitems, int PDFType)
    {
        try
        {
            companyName = string.Empty;
            customerName = string.Empty;
            custCode = string.Empty;
            jobNo = string.Empty;
            TFNo = string.Empty;
            poNo = string.Empty;
            LOTDate = string.Empty;
            itemName = string.Empty;
            impNotes = string.Empty;
            statusID = 0;
            PEID = 0;
            PMID = 0;

            if (dtLOT.Rows.Count > 0)
            {
                TFNo = Convert.ToString(dtLOT.Rows[0]["TF_NO"]).Trim();
                companyName = Convert.ToString(dtLOT.Rows[0]["UNIT_NAME"]).Trim();
                LOTDate = Convert.ToString(dtLOT.Rows[0]["DATE"]);
                custCode = Convert.ToString(dtLOT.Rows[0]["CUSTOMER_CODE"]).Trim();
                customerName = Convert.ToString(dtLOT.Rows[0]["CUSTOMER_NAME"]).Trim();
                jobNo = Convert.ToString(dtLOT.Rows[0]["JOB_NO"]).Trim();
                poNo = Convert.ToString(dtLOT.Rows[0]["PO_NO"]).Trim();
                itemName = Convert.ToString(dtLOT.Rows[0]["ITEM_NAME"]).Trim();
                impNotes = Convert.ToString(dtLOT.Rows[0]["IMP_NOTES"]).Trim();
                PEID = Convert.ToInt32(dtLOT.Rows[0]["PE_ID"]);
                PMID = Convert.ToInt32(dtLOT.Rows[0]["PM_ID"]);

            }

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.Append("<h2 class='headerStyle'>TRANSMITTAL TO FACTORY</h2>\n");
            sb.Append("<hr />\n");
            sb.Append("<table class='tblheader'>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Company:</td>\n");
            sb.Append("<td class='td2header'>{#Company#}</td>\n");

            sb.Append("<td class='td1header'>&nbsp;</td>\n");
            sb.Append("<td class='td2header'>&nbsp;</td>\n");

            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Customer Name:</td>\n");
            sb.Append("<td class='td2header' colspan='3'>{#customerName#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>JOB Number:</td>\n");
            sb.Append("<td class='td2header'>{#JOBNo#}</td>\n");
            sb.Append("<td class='td1header'>TF Number:</td>\n");
            sb.Append("<td class='td2header'><b>{#TFNo#}</b></td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Customer PO Number:</td>\n");
            sb.Append("<td class='td2header'>{#PONo#}</td>\n");
            sb.Append("<td class='td1header'>Date:</td>\n");
            sb.Append("<td class='td2header'>{#Date#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Item:</td>\n");
            sb.Append("<td class='td2header' colspan='3'>{#item#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("</table>\n");
            sb.Append("<hr />\n");
            //}

            int srNo = 0;

            //SUBITEM DETAILS
            if (dtSubitems.Rows.Count > 0)
            {
                sb.Append("<h3 class='header2'>Subitems</h3>\n");
                sb.Append("<table class='tblsuitems'>\n");

                sb.Append("<tr class='trsubitems'>\n");

                sb.Append("<th class='tdsrno'>Sr.No.</th>\n");

                sb.Append("<th class='tdpono'>Production Order No.</th>\n");
                sb.Append("<th class='tdexpdateno'>Expected Completion Date</th>\n");

                sb.Append("<th class='tddesc'>Description</th>\n");
                sb.Append("<th class='tdtag'>Tag No.</th>\n");
                sb.Append("<th class='tdlotfor'>LOT For</th>\n");
                sb.Append("<th class='tddrgno'>Drg./Doc.No.</th>\n");
                sb.Append("<th class='tdrev'>Rev.</th>\n");
                sb.Append("<th class='tdcate'>Cat.</th>\n");
                sb.Append("<th class='tdquantity'>Qty.</th>\n");
                sb.Append("</tr>\n");

                foreach (DataRow dr in dtSubitems.Rows)
                {
                    srNo++;
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");

                    sb.Append("<td class='tdsrno'>{#ProductionOrderNo#}</td>\n");
                    sb.Append("<td class='tdsrno'>{#ExpectedCompletionDate#}</td>\n");

                    sb.Append("<td class='tddesc'>{#subitemdesc#}</td>\n");
                    sb.Append("<td class='tdtag'>{#tagno#}</td>\n");
                    sb.Append("<td class='tdlotfor'>{#lotmainitem#}</td>\n");
                    sb.Append("<td class='tddrgno'>{#drawingno#}</td>\n");
                    sb.Append("<td class='tdrev'>{#revisionno#}</td>\n");
                    sb.Append("<td class='tdcate'>{#category#}</td>\n");
                    sb.Append("<td class='tdquantity'>{#quantity#}</td>\n");
                    sb.Append("</tr>\n");


                    sb.Replace("{#SRNo#}", Convert.ToString(srNo));

                    sb.Replace("{#ProductionOrderNo#}", Convert.ToString(dr["PRODUCTION_ORDER_NO"]));
                    sb.Replace("{#ExpectedCompletionDate#}", Convert.ToString(dr["EXPECTED_COMPLETION_DATE"]));

                    sb.Replace("{#subitemdesc#}", Convert.ToString(dr["SUBITEM_DESC"]));
                    sb.Replace("{#tagno#}", Convert.ToString(dr["TAG_NO"]));
                    sb.Replace("{#lotmainitem#}", Convert.ToString(dr["LOT_MAIN_ITEM"]));
                    sb.Replace("{#drawingno#}", Convert.ToString(dr["DRAWING_NO"]));
                    sb.Replace("{#revisionno#}", Convert.ToString(dr["REVISION_NO"]));
                    sb.Replace("{#category#}", Convert.ToString(dr["CATEGORY"]));
                    sb.Replace("{#quantity#}", Convert.ToString(dr["QUANTITY"]));
                }
                sb.Append("</table>\n");
            }


            sb.Append("<h3 class='header2'>Category (For Factory)</h3>\n");
            sb.Append("<table class='tblcategory'>\n");
            sb.Append("<tr>\n");
            sb.Append("<th class='td1category'>1</th>\n");
            sb.Append("<td class='td2category'>Fabrication</td>\n");
            sb.Append("<th class='td1category'>2</th>\n");
            sb.Append("<td class='td2category'>Inspection</td>\n");
            sb.Append("<th class='td1category'>3</th>\n");
            sb.Append("<td class='td2category'>Information</td>\n");
            sb.Append("</tr>\n");
            sb.Append("</table>\n");


            sb.Append("<h3 class='headerimpnotes'>Important Notes</h3>\n");
            sb.Append("<hr />\n");
            sb.Append("<table class='tblimpnotes'>\n");
            sb.Append("<tr>\n");
            sb.Append("<td class='tdimpnotes'>{#impNotes#}</td>\n");
            sb.Append("</tr>\n");
            sb.Append("</table>\n");
            sb.Append("<hr />\n");

            //SIGNATORIES DETAILS START[=========================]
            foreach (DataRow dr in dtSubitems.Rows)
            {
                approvedByID = 0;
                approvedBy = string.Empty;
                approvedOn = string.Empty;
                approvedRemarks = string.Empty;

                amendmentCount = 0;

                sendToAmendmentByID = 0;
                sendToAmendmentBy = string.Empty;
                sendToAmendmentOn = string.Empty;
                sendToAmendmentRemarks = string.Empty;

                amendedApprovedByID = 0;
                amendedApprovedBy = string.Empty;
                amendedApprovedOn = string.Empty;
                amendedApprovedRemarks = string.Empty;


                approvedByID = Convert.ToInt32(dr["APPROVED_BY_ID"]);
                approvedBy = Convert.ToString(dr["APPROVED_BY"]);
                approvedOn = Convert.ToString(dr["APPROVED_ON"]);
                approvedRemarks = Convert.ToString(dr["APPROVED_REMARKS"]);

                amendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);

                sendToAmendmentByID = Convert.ToInt32(dr["AMENDMENT_BY_ID"]);
                sendToAmendmentBy = Convert.ToString(dr["AMENDMENT_BY"]);
                sendToAmendmentOn = Convert.ToString(dr["AMENDMENT_ON"]);
                sendToAmendmentRemarks = Convert.ToString(dr["AMENDMENT_REMARKS"]);

                amendedApprovedByID = Convert.ToInt32(dr["AMENDED_APPROVED_BY_ID"]);
                amendedApprovedBy = Convert.ToString(dr["AMENDED_APPROVED_BY"]);
                amendedApprovedOn = Convert.ToString(dr["AMENDED_APPROVED_ON"]);
                amendedApprovedRemarks = Convert.ToString(dr["AMENDED_APPROVED_REMARKS"]);


                if (approvedByID > 0 || amendedApprovedByID > 0)
                {
                    sb.Append("<h3 class='header2'>Signatories</h3>\n");
                    sb.Append("<hr />\n");
                }

                sb.Append("<table class='tblsignatories'>\n");
                if (approvedByID > 0)
                {
                    sb.Append("<tr>\n");
                    sb.Append("<td>Approved Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#approvedRemarks#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Approved By:</td>\n");

                    if (approvedByID == PEID)
                        sb.Append("<td class='tdsignatories2'>{#approvedBy#} [Project Engineer]</td>\n");
                    else if (approvedByID == PMID)
                        sb.Append("<td class='tdsignatories2'>{#approvedBy#} [Project Manager]</td>\n");
                    else
                        sb.Append("<td class='tdsignatories2'>{#approvedBy#}</td>\n");

                    sb.Append("<td class='tdsignatories1'>Approved On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#approvedOn#}</td>\n");
                    sb.Append("</tr>\n");
                }

                if (amendedApprovedByID > 0)
                {
                    sb.Append("<tr>\n");
                    sb.Append("<td>Amnd. Approved Remarks:</td>\n");
                    sb.Append("<td colspan='3'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>{#amendedApprovedRemarks#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsignatories1'>Amnd. Approved By:</td>\n");

                    if (amendedApprovedByID == PEID)
                        sb.Append("<td class='tdsignatories2'>{#amendedApprovedBy#} [Project Engineer]</td>\n");
                    else if (amendedApprovedByID == PMID)
                        sb.Append("<td class='tdsignatories2'>{#amendedApprovedBy#} [Project Manager]</td>\n");
                    else
                        sb.Append("<td class='tdsignatories2'>{#amendedApprovedBy#}</td>\n");

                    sb.Append("<td class='tdsignatories1'>Amnd. Approved On:</td>\n");
                    sb.Append("<td class='tdsignatories2'>{#amendedApprovedOn#}</td>\n");
                    sb.Append("</tr>\n");
                }


                if (amendmentCount > 0)
                {
                    if (sendToAmendmentByID == PEID || sendToAmendmentByID == PMID)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td>Send To Amenment Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#sendToAmendmentRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Send To Amendment By:</td>\n");

                        if (sendToAmendmentByID == PEID)
                            sb.Append("<td class='tdsignatories2'>{#sendToAmendmentBy#} [Project Engineer]</td>\n");
                        else if (sendToAmendmentByID == PMID)
                            sb.Append("<td class='tdsignatories2'>{#sendToAmendmentBy#} [Project Manager]</td>\n");
                        else
                            sb.Append("<td class='tdsignatories2'>{#sendToAmendmentBy#}</td>\n");

                        sb.Append("<td class='tdsignatories1'>Send To Amendment On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#sendToAmendmentOn#}</td>\n");
                        sb.Append("</tr>\n");

                    }
                }

                sb.Replace("{#approvedRemarks#}", approvedRemarks);
                sb.Replace("{#approvedBy#}", approvedBy);
                sb.Replace("{#approvedOn#}", approvedOn);
                sb.Replace("{#amendedApprovedRemarks#}", amendedApprovedRemarks);
                sb.Replace("{#amendedApprovedBy#}", amendedApprovedBy);
                sb.Replace("{#amendedApprovedOn#}", amendedApprovedOn);
                sb.Replace("{#sendToAmendmentRemarks#}", sendToAmendmentRemarks);
                sb.Replace("{#sendToAmendmentBy#}", sendToAmendmentBy);
                sb.Replace("{#sendToAmendmentOn#}", sendToAmendmentOn);

                sb.Append("<tr>\n");
                sb.Append("<td colspan='4'>&nbsp;</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");

                break;
            }



            string LOTMainsubitemIDs = string.Empty;

            foreach (DataRow dr in dtSubitems.Rows)
            {
                if (!LOTMainsubitemIDs.Contains(Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"])))
                    LOTMainsubitemIDs += Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"]) + ",";
            }

            if (!string.IsNullOrEmpty(LOTMainsubitemIDs))
                LOTMainsubitemIDs = LOTMainsubitemIDs.TrimEnd(',');

            string[] strLOTMainsubitemIDs = LOTMainsubitemIDs.Split(',');

            foreach (string item in strLOTMainsubitemIDs)
            {

                foreach (DataRow dr in dtSubitems.Select("LOT_MAIN_SUBITEM_ID='" + Convert.ToInt32(item) + "'"))
                {
                    LOTItemName = string.Empty;

                    productionAcceptedByID = 0;
                    productionAcceptedBy = string.Empty;
                    productionAcceptedOn = string.Empty;
                    productionAcceptedRemarks = string.Empty;

                    productionAmendedAcceptedByID = 0;
                    productionAmendedAcceptedBy = string.Empty;
                    productionAmendedAcceptedOn = string.Empty;
                    productionAmendedAcceptedRemarks = string.Empty;


                    planningAcceptedByID = 0;
                    planningAcceptedBy = string.Empty;
                    planningAcceptedOn = string.Empty;
                    planningAcceptedRemarks = string.Empty;

                    planningAmendedAcceptedByID = 0;
                    planningAmendedAcceptedBy = string.Empty;
                    planningAmendedAcceptedOn = string.Empty;
                    planningAmendedAcceptedRemarks = string.Empty;


                    qualityAcceptedByID = 0;
                    qualityAcceptedBy = string.Empty;
                    qualityAcceptedOn = string.Empty;
                    qualityAcceptedRemarks = string.Empty;

                    qualityAmendedAcceptedByID = 0;
                    qualityAmendedAcceptedBy = string.Empty;
                    qualityAmendedAcceptedOn = string.Empty;
                    qualityAmendedAcceptedRemarks = string.Empty;


                    completedByID = 0;
                    completedBy = string.Empty;
                    completedOn = string.Empty;
                    completedRemarks = string.Empty;

                    amendmentCount = 0;

                    sendToAmendmentByID = 0;
                    sendToAmendmentBy = string.Empty;
                    sendToAmendmentOn = string.Empty;
                    sendToAmendmentRemarks = string.Empty;

                    LOTItemName = Convert.ToString(dr["LOT_MAIN_ITEM"]);

                    productionAcceptedByID = Convert.ToInt32(dr["ACCEPTED_BY_ID"]);
                    productionAcceptedBy = Convert.ToString(dr["ACCEPTED_BY"]);
                    productionAcceptedOn = Convert.ToString(dr["ACCEPTED_ON"]);
                    productionAcceptedRemarks = Convert.ToString(dr["ACCEPTED_REMARKS"]);

                    productionAmendedAcceptedByID = Convert.ToInt32(dr["AMENDED_ACCEPTED_BY_ID"]);
                    productionAmendedAcceptedBy = Convert.ToString(dr["AMENDED_ACCEPTED_BY"]);
                    productionAmendedAcceptedOn = Convert.ToString(dr["AMENDED_ACCEPTED_ON"]);
                    productionAmendedAcceptedRemarks = Convert.ToString(dr["AMENDED_ACCEPTED_REMARKS"]);


                    planningAcceptedByID = Convert.ToInt32(dr["PLANNING_ACCEPTED_BY_ID"]);
                    planningAcceptedBy = Convert.ToString(dr["PLANNING_ACCEPTED_BY"]);
                    planningAcceptedOn = Convert.ToString(dr["PLANNING_ACCEPTED_ON"]);
                    planningAcceptedRemarks = Convert.ToString(dr["PLANNING_ACCEPTED_REMARKS"]);

                    planningAmendedAcceptedByID = Convert.ToInt32(dr["AMENDED_PLANNING_ACCEPTED_BY_ID"]);
                    planningAmendedAcceptedBy = Convert.ToString(dr["AMENDED_PLANNING_ACCEPTED_BY"]);
                    planningAmendedAcceptedOn = Convert.ToString(dr["AMENDED_PLANNING_ACCEPTED_ON"]);
                    planningAmendedAcceptedRemarks = Convert.ToString(dr["AMENDED_PLANNING_ACCEPTED_REMARKS"]);


                    qualityAcceptedByID = Convert.ToInt32(dr["QUALITY_ACCEPTED_BY_ID"]);
                    qualityAcceptedBy = Convert.ToString(dr["QUALITY_ACCEPTED_BY"]);
                    qualityAcceptedOn = Convert.ToString(dr["QUALITY_ACCEPTED_ON"]);
                    qualityAcceptedRemarks = Convert.ToString(dr["QUALITY_ACCEPTED_REMARKS"]);

                    qualityAmendedAcceptedByID = Convert.ToInt32(dr["AMENDED_QUALITY_ACCEPTED_BY_ID"]);
                    qualityAmendedAcceptedBy = Convert.ToString(dr["AMENDED_QUALITY_ACCEPTED_BY"]);
                    qualityAmendedAcceptedOn = Convert.ToString(dr["AMENDED_QUALITY_ACCEPTED_ON"]);
                    qualityAmendedAcceptedRemarks = Convert.ToString(dr["AMENDED_QUALITY_ACCEPTED_REMARKS"]);


                    completedByID = Convert.ToInt32(dr["COMPLETED_BY_ID"]);
                    completedBy = Convert.ToString(dr["COMPLETED_BY"]);
                    completedOn = Convert.ToString(dr["COMPLETED_ON"]);
                    completedRemarks = Convert.ToString(dr["COMPLETED_REMARKS"]);

                    amendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);

                    sendToAmendmentByID = Convert.ToInt32(dr["AMENDMENT_BY_ID"]);
                    sendToAmendmentBy = Convert.ToString(dr["AMENDMENT_BY"]);
                    sendToAmendmentOn = Convert.ToString(dr["AMENDMENT_ON"]);
                    sendToAmendmentRemarks = Convert.ToString(dr["AMENDMENT_REMARKS"]);

                    //sb.Append("<hr class='hrsignatories' />\n");
                    sb.Append("<table class='tblsignatories'>\n");


                    if (productionAcceptedByID > 0 || planningAcceptedByID > 0 || qualityAcceptedByID > 0 ||
                        productionAmendedAcceptedByID > 0 || planningAmendedAcceptedByID > 0 || qualityAmendedAcceptedByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'><hr class='hrsignatories' /></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Item:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#LOTItemName#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#LOTItemName#}", LOTItemName);
                    }

                    if (productionAcceptedByID > 0)
                    {


                        sb.Append("<tr>\n");
                        sb.Append("<td>Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#productionAcceptedRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#productionAcceptedBy#} [Production Manager][Production]</td>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#productionAcceptedOn#}</td>\n");
                        sb.Append("</tr>\n");


                        sb.Replace("{#productionAcceptedBy#}", productionAcceptedBy);
                        sb.Replace("{#productionAcceptedOn#}", productionAcceptedOn);
                        sb.Replace("{#productionAcceptedRemarks#}", productionAcceptedRemarks);

                    }

                    if (productionAmendedAcceptedByID > 0)
                    {


                        sb.Append("<tr>\n");
                        sb.Append("<td>Amnd. Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#productionAmendedAcceptedRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amnd. Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#productionAmendedAcceptedBy#} [Production Manager]</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amnd. Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#productionAmendedAcceptedOn#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#productionAmendedAcceptedBy#}", productionAmendedAcceptedBy);
                        sb.Replace("{#productionAmendedAcceptedOn#}", productionAmendedAcceptedOn);
                        sb.Replace("{#productionAmendedAcceptedRemarks#}", productionAmendedAcceptedRemarks);

                    }

                    if (planningAcceptedByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='2'><hr class='hrsignatories' /></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td>Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#planningAcceptedRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#planningAcceptedBy#} [Planning]</td>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#planningAcceptedOn#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#planningAcceptedBy#}", planningAcceptedBy);
                        sb.Replace("{#planningAcceptedOn#}", planningAcceptedOn);
                        sb.Replace("{#planningAcceptedRemarks#}", planningAcceptedRemarks);


                    }

                    if (planningAmendedAcceptedByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td>Amnd. Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#planningAmendedAcceptedRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amnd. Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#planningAmendedAcceptedBy#} [Planning]</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amnd. Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#planningAmendedAcceptedOn#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#planningAmendedAcceptedBy#}", planningAmendedAcceptedBy);
                        sb.Replace("{#planningAmendedAcceptedOn#}", planningAmendedAcceptedOn);
                        sb.Replace("{#planningAmendedAcceptedRemarks#}", planningAmendedAcceptedRemarks);

                    }

                    if (qualityAcceptedByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='2'><hr class='hrsignatories' /></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td>Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#qualityAcceptedRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#qualityAcceptedBy#} [Quality]</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amnd. Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#qualityAcceptedOn#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#qualityAcceptedBy#}", qualityAcceptedBy);
                        sb.Replace("{#qualityAcceptedOn#}", qualityAcceptedOn);
                        sb.Replace("{#qualityAcceptedRemarks#}", qualityAcceptedRemarks);


                    }

                    if (qualityAmendedAcceptedByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td>Amnd. Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#qualityAmendedAcceptedRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Amnd. Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#qualityAmendedAcceptedBy#} [Quality]</td>\n");
                        sb.Append("<td class='tdsignatories1'>Amnd. Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#qualityAmendedAcceptedOn#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#qualityAmendedAcceptedBy#}", qualityAmendedAcceptedBy);
                        sb.Replace("{#qualityAmendedAcceptedOn#}", qualityAmendedAcceptedOn);
                        sb.Replace("{#qualityAmendedAcceptedRemarks#}", qualityAmendedAcceptedRemarks);

                    }

                    if (completedByID > 0)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='2'><hr class='hrsignatories' /></td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td>Completed Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#completedRemarks#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Completed By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#completedBy#} [Production Manager]</td>\n");
                        sb.Append("<td class='tdsignatories1'>Completed On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#completedOn#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#completedBy#}", completedBy);
                        sb.Replace("{#completedOn#}", completedOn);
                        sb.Replace("{#completedRemarks#}", completedRemarks);

                    }

                    if (amendmentCount > 0)
                    {
                        if (sendToAmendmentByID != PEID && sendToAmendmentByID != PMID)
                        {
                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='2'><hr class='hrsignatories' /></td>\n");
                            sb.Append("</tr>\n");

                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");

                            sb.Append("<tr>\n");
                            sb.Append("<td>Send To Amenment Remarks:</td>\n");
                            sb.Append("<td colspan='3'>&nbsp;</td>\n");
                            sb.Append("</tr>\n");

                            sb.Append("<tr>\n");
                            sb.Append("<td colspan='4'>{#sendToAmendmentRemarks#}</td>\n");
                            sb.Append("</tr>\n");

                            sb.Append("<tr>\n");
                            sb.Append("<td class='tdsignatories1'>Send To Amendment By:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#sendToAmendmentBy#}=</td>\n");
                            sb.Append("<td class='tdsignatories1'>Send To Amendment On:</td>\n");
                            sb.Append("<td class='tdsignatories2'>{#sendToAmendmentOn#}</td>\n");
                            sb.Append("</tr>\n");

                            sb.Replace("{#sendToAmendmentBy#}", sendToAmendmentBy);
                            sb.Replace("{#sendToAmendmentOn#}", sendToAmendmentOn);
                            sb.Replace("{#sendToAmendmentRemarks#}", sendToAmendmentRemarks);
                        }
                    }

                    //sb.Append("<tr>\n");
                    //sb.Append("<td colspan='4'><hr class='hrsignatories' /></td>\n");
                    //sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td colspan='4'>&nbsp;</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");

                    break;
                }
            }
            //SIGNATORIES DETAILS END[===========================]

            sb.Replace("{#TFNo#}", TFNo);
            sb.Replace("{#ProductionNo#}", productionNo);
            sb.Replace("{#Company#}", companyName);
            sb.Replace("{#Date#}", LOTDate);
            sb.Replace("{#customerName#}", ("[" + custCode + "] - " + customerName));
            sb.Replace("{#JOBNo#}", jobNo);
            sb.Replace("{#PONo#}", poNo);
            sb.Replace("{#item#}", itemName);
            sb.Replace("{#impNotes#}", impNotes);

            htmlText = sb.ToString();
            return htmlText;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}