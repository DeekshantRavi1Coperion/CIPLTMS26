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
public class LOTHtmlForPDF
{

    string companyName = string.Empty;
    string LOTMainItem = string.Empty;
    string LOTMainSubItem = string.Empty;
    string customerName = string.Empty;
    string custCode = string.Empty;
    string jobNo = string.Empty;
    string productionNo = string.Empty;
    string TFNo = string.Empty;
    string poNo = string.Empty;
    string LOTDate = string.Empty;
    string itemName = string.Empty;
    string impNotes = string.Empty;

    int statusID = 0;
    int PEApprovedByID = 0;
    string PEApprovedBy = string.Empty;
    string PEApprovedOn = string.Empty;
    string PEApprovedRemarks = string.Empty;

    int PMApprovedByID = 0;
    string PMApprovedBy = string.Empty;
    string PMApprovedOn = string.Empty;
    string PMApprovedRemarks = string.Empty;

    int acceptedByID = 0;
    string acceptedBy = string.Empty;
    string acceptedOn = string.Empty;
    string acceptedRemarks = string.Empty;

    int amendmentCount = 0;

    int sendToAmendmentByID = 0;
    string sendToAmendmentBy = string.Empty;
    string sendToAmendmentOn = string.Empty;
    string sendToAmendmentRemarks = string.Empty;

    int amendedByID = 0;
    string amendedBy = string.Empty;
    string amendedOn = string.Empty;
    string amendedRemarks = string.Empty;

    int PEAmendedApprovedByID = 0;
    string PEAmendedApprovedBy = string.Empty;
    string PEAmendedApprovedOn = string.Empty;
    string PEAmendedApprovedRemarks = string.Empty;

    int PMAmendedApprovedByID = 0;
    string PMAmendedApprovedBy = string.Empty;
    string PMAmendedApprovedOn = string.Empty;
    string PMAmendedApprovedRemarks = string.Empty;

    int amendedAcceptedByID = 0;
    string amendedAcceptedBy = string.Empty;
    string amendedAcceptedOn = string.Empty;
    string amendedAcceptedRemarks = string.Empty;

    int completedByID = 0;
    string completedBy = string.Empty;
    string completedOn = string.Empty;
    string completedRemarks = string.Empty;

    public LOTHtmlForPDF()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    //public string GetHtmlForPDF(DataTable dtLOT, DataTable dtSubitems)
    //{
    //    try
    //    {
    //        companyName = string.Empty;
    //        LOTMainItem = string.Empty;
    //        LOTMainSubItem = string.Empty;
    //        customerName = string.Empty;
    //        custCode = string.Empty;
    //        jobNo = string.Empty;
    //        TFNo = string.Empty;
    //        poNo = string.Empty;
    //        LOTDate = string.Empty;
    //        itemName = string.Empty;
    //        impNotes = string.Empty;
    //        statusID = 0;

    //        PEApprovedByID = 0;
    //        PEApprovedBy = string.Empty;
    //        PEApprovedOn = string.Empty;
    //        PEApprovedRemarks = string.Empty;

    //        PMApprovedByID = 0;
    //        PMApprovedBy = string.Empty;
    //        PMApprovedOn = string.Empty;
    //        PMApprovedRemarks = string.Empty;

    //        acceptedByID = 0;
    //        acceptedBy = string.Empty;
    //        acceptedOn = string.Empty;
    //        acceptedRemarks = string.Empty;

    //        amendmentCount = 0;

    //        sendToAmendmentByID = 0;
    //        sendToAmendmentBy = string.Empty;
    //        sendToAmendmentOn = string.Empty;
    //        sendToAmendmentRemarks = string.Empty;

    //        PEAmendedApprovedByID = 0;
    //        PEAmendedApprovedBy = string.Empty;
    //        PEAmendedApprovedOn = string.Empty;
    //        PEAmendedApprovedRemarks = string.Empty;

    //        PMAmendedApprovedByID = 0;
    //        PMAmendedApprovedBy = string.Empty;
    //        PMAmendedApprovedOn = string.Empty;
    //        PMAmendedApprovedRemarks = string.Empty;

    //        amendedAcceptedByID = 0;
    //        amendedAcceptedBy = string.Empty;
    //        amendedAcceptedOn = string.Empty;
    //        amendedAcceptedRemarks = string.Empty;

    //        completedByID = 0;
    //        completedBy = string.Empty;
    //        completedOn = string.Empty;
    //        completedRemarks = string.Empty;

    //        if (dtLOT.Rows.Count > 0)
    //        {
    //            companyName = Convert.ToString(dtLOT.Rows[0]["UNIT_NAME"]).Trim();
    //            LOTMainItem = Convert.ToString(dtLOT.Rows[0]["LOT_MAIN_ITEM"]).Trim();
    //            LOTMainSubItem = Convert.ToString(dtLOT.Rows[0]["LOT_MAIN_SUB_ITEM"]).Trim();
    //            customerName = Convert.ToString(dtLOT.Rows[0]["CUSTOMER_NAME"]).Trim();
    //            custCode = Convert.ToString(dtLOT.Rows[0]["CUSTOMER_CODE"]).Trim();
    //            jobNo = Convert.ToString(dtLOT.Rows[0]["JOB_NO"]).Trim();
    //            TFNo = Convert.ToString(dtLOT.Rows[0]["TF_NO"]).Trim();
    //            poNo = Convert.ToString(dtLOT.Rows[0]["PO_NO"]).Trim();
    //            LOTDate = Convert.ToString(dtLOT.Rows[0]["DATE"]);
    //            itemName = Convert.ToString(dtLOT.Rows[0]["ITEM_NAME"]).Trim();
    //            impNotes = Convert.ToString(dtLOT.Rows[0]["IMP_NOTES"]).Trim();

    //            statusID = Convert.ToInt32(dtLOT.Rows[0]["STATUS_ID"]);

    //            PEApprovedByID = Convert.ToInt32(dtLOT.Rows[0]["PE_APPROVED_BY_ID"]);
    //            PEApprovedBy = Convert.ToString(dtLOT.Rows[0]["PE_APPROVED_BY"]);
    //            PEApprovedOn = Convert.ToString(dtLOT.Rows[0]["PE_APPROVED_ON"]);
    //            PEApprovedRemarks = Convert.ToString(dtLOT.Rows[0]["PE_APPROVED_REMARKS"]).Trim();

    //            PMApprovedByID = Convert.ToInt32(dtLOT.Rows[0]["PM_APPROVED_BY_ID"]);
    //            PMApprovedBy = Convert.ToString(dtLOT.Rows[0]["PM_APPROVED_BY"]);
    //            PMApprovedOn = Convert.ToString(dtLOT.Rows[0]["PM_APPROVED_ON"]);
    //            PMApprovedRemarks = Convert.ToString(dtLOT.Rows[0]["PM_APPROVED_REMARKS"]).Trim();

    //            acceptedByID = Convert.ToInt32(dtLOT.Rows[0]["ACCEPTED_BY_ID"]);
    //            acceptedBy = Convert.ToString(dtLOT.Rows[0]["ACCEPTED_BY"]);
    //            acceptedOn = Convert.ToString(dtLOT.Rows[0]["ACCEPTED_ON"]);
    //            acceptedRemarks = Convert.ToString(dtLOT.Rows[0]["ACCEPTED_REMARKS"]).Trim();

    //            amendmentCount = Convert.ToInt32(dtLOT.Rows[0]["AMENDMENT_COUNT"]);

    //            sendToAmendmentByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDMENT_BY_ID"]);
    //            sendToAmendmentBy = Convert.ToString(dtLOT.Rows[0]["AMENDMENT_BY"]);
    //            sendToAmendmentOn = Convert.ToString(dtLOT.Rows[0]["AMENDMENT_ON"]);
    //            sendToAmendmentRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDMENT_REMARKS"]).Trim();

    //            amendedByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDED_BY_ID"]);
    //            amendedBy = Convert.ToString(dtLOT.Rows[0]["AMENDED_BY"]);
    //            amendedOn = Convert.ToString(dtLOT.Rows[0]["AMENDED_ON"]);
    //            amendedRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDED_REMARKS"]).Trim();

    //            PEAmendedApprovedByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDED_PE_APPROVED_BY_ID"]);
    //            PEAmendedApprovedBy = Convert.ToString(dtLOT.Rows[0]["AMENDED_PE_APPROVED_BY"]);
    //            PEAmendedApprovedOn = Convert.ToString(dtLOT.Rows[0]["AMENDED_PE_APPROVED_ON"]);
    //            PEAmendedApprovedRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDED_PE_APPROVED_REMARKS"]).Trim();

    //            PMAmendedApprovedByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDED_PM_APPROVED_BY_ID"]);
    //            PMAmendedApprovedBy = Convert.ToString(dtLOT.Rows[0]["AMENDED_PM_APPROVED_BY"]);
    //            PMAmendedApprovedOn = Convert.ToString(dtLOT.Rows[0]["AMENDED_PM_APPROVED_ON"]);
    //            PMAmendedApprovedRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDED_PM_APPROVED_REMARKS"]).Trim();

    //            amendedAcceptedByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDED_ACCEPTED_BY_ID"]);
    //            amendedAcceptedBy = Convert.ToString(dtLOT.Rows[0]["AMENDED_ACCEPTED_BY"]);
    //            amendedAcceptedOn = Convert.ToString(dtLOT.Rows[0]["AMENDED_ACCEPTED_ON"]);
    //            amendedAcceptedRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDED_ACCEPTED_REMARKS"]).Trim();

    //            completedByID = Convert.ToInt32(dtLOT.Rows[0]["COMPLETED_BY_ID"]);
    //            completedBy = Convert.ToString(dtLOT.Rows[0]["COMPLETED_BY"]);
    //            completedOn = Convert.ToString(dtLOT.Rows[0]["COMPLETED_ON"]);
    //            completedRemarks = Convert.ToString(dtLOT.Rows[0]["COMPLETED_REMARKS"]).Trim();
    //        }

    //        string htmlText = string.Empty;
    //        htmlText = string.Empty;
    //        StringBuilder sb = new StringBuilder();

    //        sb.Append("<h2 class='headerStyle'>TRANSMITTAL TO FACTORY</h2>\n");
    //        sb.Append("<hr />\n");
    //        sb.Append("<table class='tblheader'>\n");
    //        sb.Append("<tr>\n");
    //        sb.Append("<td class='td1header'>Company:</td>\n");
    //        sb.Append("<td class='td2header'>{#Company#}</td>\n");
    //        sb.Append("<td class='td1header'>LOT For:</td>\n");
    //        sb.Append("<td class='td2header'>{#LOTMainItem#}</td>\n");
    //        sb.Append("</tr>\n");
    //        sb.Append("<tr>\n");
    //        sb.Append("<td class='td1header'>Customer Name:</td>\n");
    //        sb.Append("<td class='td2header' colspan='3'>{#customerName#}</td>\n");
    //        sb.Append("</tr>\n");
    //        sb.Append("<tr>\n");
    //        sb.Append("<td class='td1header'>JOB Number:</td>\n");
    //        sb.Append("<td class='td2header'>{#JOBNo#}</td>\n");
    //        sb.Append("<td class='td1header'>TF Number:</td>\n");
    //        sb.Append("<td class='td2header'><b>{#TFNo#}</b></td>\n");
    //        sb.Append("</tr>\n");
    //        sb.Append("<tr>\n");
    //        sb.Append("<td class='td1header'>PO Number:</td>\n");
    //        sb.Append("<td class='td2header'>{#PONo#}</td>\n");
    //        sb.Append("<td class='td1header'>Date:</td>\n");
    //        sb.Append("<td class='td2header'>{#Date#}</td>\n");
    //        sb.Append("</tr>\n");
    //        sb.Append("<tr>\n");
    //        sb.Append("<td class='td1header'>Item:</td>\n");
    //        sb.Append("<td class='td2header' colspan='3'>{#item#}</td>\n");
    //        sb.Append("</tr>\n");
    //        sb.Append("</table>\n");
    //        sb.Append("<hr />\n");
    //        //}

    //        //SUBITEM DETAILS
    //        if (dtSubitems.Rows.Count > 0)
    //        {
    //            sb.Append("<h3 class='header2'>Subitems</h3>\n");
    //            sb.Append("<table class='tblsuitems'>\n");
    //            sb.Append("<tr class='trsubitems'>\n");
    //            sb.Append("<th class='td1subitems'>Sr.No.</th>\n");
    //            sb.Append("<th class='td2subitems'>Description</th>\n");
    //            sb.Append("<th class='td4subitems'>Drg./Doc.No.</th>\n");
    //            sb.Append("<th class='td3subitems'>Rev.</th>\n");
    //            sb.Append("<th class='td3subitems'>Cat.</th>\n");
    //            sb.Append("<th class='td3subitems'>Copies</th>\n");
    //            sb.Append("</tr>\n");

    //            foreach (DataRow dr in dtSubitems.Rows)
    //            {
    //                sb.Append("<tr>\n");
    //                sb.Append("<td class='td1subitems'>{#SRNo#}</td>\n");
    //                sb.Append("<td class='td2subitems'>{#decription#}</td>\n");
    //                sb.Append("<td class='td4subitems'>{#DRGNo#}</td>\n");
    //                sb.Append("<td class='td3subitems'>{#REVNo#}</td>\n");
    //                sb.Append("<td class='td3subitems'>{#category#}</td>\n");
    //                sb.Append("<td class='td3subitems'>{#nOffCopies#}</td>\n");
    //                sb.Append("</tr>\n");


    //                sb.Replace("{#SRNo#}", Convert.ToString(dr["SR_NO"]));
    //                sb.Replace("{#decription#}", Convert.ToString(dr["DESCRIPTION"]));
    //                sb.Replace("{#DRGNo#}", Convert.ToString(dr["DRG_NO"]));
    //                sb.Replace("{#REVNo#}", Convert.ToString(dr["REV_NO"]));
    //                sb.Replace("{#category#}", Convert.ToString(dr["CATEGORY_ID"]));
    //                sb.Replace("{#nOffCopies#}", Convert.ToString(dr["NO_OF_COPIES"]));
    //            }
    //            sb.Append("</table>\n");
    //        }


    //        sb.Append("<h3 class='header2'>Category (For Factory)</h3>\n");
    //        sb.Append("<table class='tblcategory'>\n");
    //        sb.Append("<tr>\n");
    //        sb.Append("<th class='td1category'>1</th>\n");
    //        sb.Append("<td class='td2category'>Fabrication</td>\n");
    //        sb.Append("<th class='td1category'>2</th>\n");
    //        sb.Append("<td class='td2category'>Inspection</td>\n");
    //        sb.Append("<th class='td1category'>3</th>\n");
    //        sb.Append("<td class='td2category'>Information</td>\n");
    //        sb.Append("</tr>\n");
    //        sb.Append("</table>\n");


    //        sb.Append("<h3 class='headerimpnotes'>Important Notes</h3>\n");
    //        sb.Append("<hr />\n");
    //        sb.Append("<table class='tblimpnotes'>\n");
    //        sb.Append("<tr>\n");
    //        sb.Append("<td class='tdimpnotes'>{#impNotes#}</td>\n");
    //        sb.Append("</tr>\n");
    //        sb.Append("</table>\n");
    //        sb.Append("<hr />\n");


    //        //if (amendmentCount > 0 && !string.IsNullOrEmpty(amendedRemarks))
    //        if (amendmentCount > 0 && amendedByID > 0)
    //        {
    //            sb.Append("<table class='tblsignatories'>\n");
    //            sb.Append("<tr class='trsignatoriessigns'>\n");
    //            sb.Append("<td class='tdsignatories1'>Amended By:</td>\n");
    //            sb.Append("<td class='tdsignatories2'>{#amendedby#}</td>\n");
    //            sb.Append("<td class='tdsignatories1'>Amended On:</td>\n");
    //            sb.Append("<td class='tdsignatories2'>{#amendedon#}</td>\n");
    //            sb.Append("</tr>\n");

    //            sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //            sb.Append("<tr>\n");
    //            sb.Append("<td>Remarks:</td>\n");
    //            sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //            sb.Append("</tr>\n");
    //            sb.Append("<tr>\n");
    //            sb.Append("<td colspan='4'>{#amendedremarks#}</td>\n");
    //            sb.Append("</tr>\n");

    //            sb.Append("</table>\n");

    //            sb.Append("<hr class='hrsignatories' />\n");
    //        }






    //        //SIGNATORIES DETAILS
    //        if (statusID > 1)
    //        {
    //            sb.Append("<h3 class='header2'>Signatories</h3>\n");
    //            sb.Append("<hr />\n");



    //            // PROJECT ENGINEER APPROVED START--------------------------------
    //            sb.Append("<table class='tblsignatories'>\n");
    //            if (PEApprovedByID > 0)
    //            {
    //                sb.Append("<tr class='trsignatoriessigns'>\n");
    //                sb.Append("<td class='tdsignatories1'>Approved By:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#peApprovedBy#} [Project Engineer]</td>\n");
    //                sb.Append("<td class='tdsignatories1'>Approved On:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#peApprovedOn#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr>\n");
    //                sb.Append("<td>Remarks:</td>\n");
    //                sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //                sb.Append("</tr>\n");
    //                sb.Append("<tr>\n");
    //                sb.Append("<td colspan='4'>{#peApprovedRemarks#}</td>\n");
    //                sb.Append("</tr>\n");
    //            }

    //            if (PEAmendedApprovedByID > 0)
    //            {
    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr class='trsignatoriessigns'>\n");
    //                sb.Append("<td class='tdsignatories1'>Amended Approved By:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#peAmendedApprovedBy#} [Project Engineer]</td>\n");
    //                sb.Append("<td class='tdsignatories1'>Amended Approved On:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#peAmendedApprovedOn#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr>\n");
    //                sb.Append("<td>Remarks:</td>\n");
    //                sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //                sb.Append("</tr>\n");
    //                sb.Append("<tr>\n");
    //                sb.Append("<td colspan='4'>{#peAmendedApprovedRemarks#}</td>\n");
    //                sb.Append("</tr>\n");
    //            }

    //            sb.Append("<tr>\n");
    //            sb.Append("<td colspan='4'>&nbsp;</td>\n");
    //            sb.Append("</tr>\n");

    //            sb.Append("</table>\n");
    //            // PROJECT ENGINEER APPROVED END----------------------------------




    //            sb.Append("<hr class='hrsignatories'/>\n");




    //            // PROJECT MANAGER APPROVED START---------------------------------
    //            sb.Append("<table class='tblsignatories'>\n");

    //            if (PMApprovedByID > 0)
    //            {
    //                sb.Append("<tr class='trsignatoriessigns'>\n");
    //                sb.Append("<td class='tdsignatories1'>Approved By:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#pmApprovedBy#} [Project Manager]</td>\n");
    //                sb.Append("<td class='tdsignatories1'>Approved On:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#pmApprovedOn#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr>\n");
    //                sb.Append("<td>Remarks:</td>\n");
    //                sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //                sb.Append("</tr>\n");
    //                sb.Append("<tr>\n");
    //                sb.Append("<td colspan='4'>{#pmApprovedRemarks#}</td>\n");
    //                sb.Append("</tr>\n");
    //            }
    //            if (PMAmendedApprovedByID > 0)
    //            {
    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr class='trsignatoriessigns'>\n");
    //                sb.Append("<td class='tdsignatories1'>Amended Approved By:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#pmAmendedApprovedBy#} [Project Manager]</td>\n");
    //                sb.Append("<td class='tdsignatories1'>Amended Approved On:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#pmAmendedApprovedOn#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr>\n");
    //                sb.Append("<td>Remarks:</td>\n");
    //                sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //                sb.Append("</tr>\n");
    //                sb.Append("<tr>\n");
    //                sb.Append("<td colspan='4'>{#pmAmendedApprovedRemarks#}</td>\n");
    //                sb.Append("</tr>\n");

    //            }

    //            sb.Append("<tr>\n");
    //            sb.Append("<td colspan='4'>&nbsp;</td>\n");
    //            sb.Append("</tr>\n");

    //            sb.Append("</table>\n");
    //            // PROJECT MANAGER APPROVED END-----------------------------------



    //            sb.Append("<hr class='hrsignatories' />\n");



    //            // ACCEPTED START---------------------------------
    //            sb.Append("<table class='tblsignatories'>\n");

    //            if (acceptedByID > 0)
    //            {
    //                sb.Append("<tr class='trsignatoriessigns'>\n");
    //                sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#acceptedBy#} [Project Manager]</td>\n");
    //                sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#acceptedOn#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr>\n");
    //                sb.Append("<td>Remarks:</td>\n");
    //                sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //                sb.Append("</tr>\n");
    //                sb.Append("<tr>\n");
    //                sb.Append("<td colspan='4'>{#acceptedRemarks#}</td>\n");
    //                sb.Append("</tr>\n");
    //            }
    //            if (amendedAcceptedByID > 0)
    //            {
    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr class='trsignatoriessigns'>\n");
    //                sb.Append("<td class='tdsignatories1'>Amended Accepted By:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#amendedAcceptedBy#} [Project Manager]</td>\n");
    //                sb.Append("<td class='tdsignatories1'>Amended Accepted On:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#amendedAcceptedOn#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr>\n");
    //                sb.Append("<td>Remarks:</td>\n");
    //                sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //                sb.Append("</tr>\n");
    //                sb.Append("<tr>\n");
    //                sb.Append("<td colspan='4'>{#amendedAcceptedRemarks#}</td>\n");
    //                sb.Append("</tr>\n");

    //            }

    //            sb.Append("<tr>\n");
    //            sb.Append("<td colspan='4'>&nbsp;</td>\n");
    //            sb.Append("</tr>\n");

    //            sb.Append("</table>\n");
    //            // ACCEPTED END-----------------------------------



    //            sb.Append("<hr class='hrsignatories' />\n");



    //            // AMENDMENT START------------------------------------------------

    //            if (amendmentCount > 0 && sendToAmendmentByID > 0)
    //            {
    //                sb.Append("<table class='tblsignatories'>\n");

    //                sb.Append("<tr class='trsignatoriessigns'>\n");
    //                sb.Append("<td class='tdsignatories1'>Send To Amendment By:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#sendToAmenmentBy#}</td>\n");
    //                sb.Append("<td class='tdsignatories1'>Send To Amendment On:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#sendToAmenmentOn#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr>\n");
    //                sb.Append("<td>Remarks:</td>\n");
    //                sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //                sb.Append("</tr>\n");
    //                sb.Append("<tr>\n");
    //                sb.Append("<td colspan='4'>{#sendToAmenmentRemarks#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("</table>\n");

    //                sb.Append("<hr class='hrsignatories' />\n");
    //            }

    //            // AMENDMENT END--------------------------------------------------

    //            // COMPLETED START------------------------------------------------

    //            if (completedByID > 0)
    //            {
    //                sb.Append("<table class='tblsignatories'>\n");

    //                sb.Append("<tr class='trsignatoriessigns'>\n");
    //                sb.Append("<td class='tdsignatories1'>Completed By:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#completedBy#} [Production Manager]</td>\n");
    //                sb.Append("<td class='tdsignatories1'>Completed On:</td>\n");
    //                sb.Append("<td class='tdsignatories2'>{#completedOn#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

    //                sb.Append("<tr>\n");
    //                sb.Append("<td>Remarks:</td>\n");
    //                sb.Append("<td colspan='3'>&nbsp;</td>\n");
    //                sb.Append("</tr>\n");
    //                sb.Append("<tr>\n");
    //                sb.Append("<td colspan='4'>{#completedRemarks#}</td>\n");
    //                sb.Append("</tr>\n");

    //                sb.Append("</table>\n");
    //                sb.Append("<hr class='hrsignatories'/>\n");
    //            }

    //            // AMENDMENT END--------------------------------------------------
    //        }


    //        sb.Replace("{#Company#}", companyName);
    //        sb.Replace("{#LOTMainItem#}", (LOTMainItem + " - [" + LOTMainSubItem + "]"));
    //        sb.Replace("{#customerName#}", ("[" + custCode + "] - " + customerName));
    //        sb.Replace("{#JOBNo#}", jobNo);
    //        sb.Replace("{#TFNo#}", TFNo);
    //        sb.Replace("{#PONo#}", poNo);
    //        sb.Replace("{#Date#}", LOTDate);
    //        sb.Replace("{#item#}", itemName);
    //        sb.Replace("{#impNotes#}", impNotes);

    //        sb.Replace("{#peApprovedBy#}", PEApprovedBy);
    //        sb.Replace("{#peApprovedOn#}", PEApprovedOn);
    //        sb.Replace("{#peApprovedRemarks#}", PEApprovedRemarks);

    //        sb.Replace("{#pmApprovedBy#}", PMApprovedBy);
    //        sb.Replace("{#pmApprovedOn#}", PMApprovedOn);
    //        sb.Replace("{#pmApprovedRemarks#}", PMApprovedRemarks);

    //        sb.Replace("{#acceptedBy#}", acceptedBy);
    //        sb.Replace("{#acceptedOn#}", acceptedOn);
    //        sb.Replace("{#acceptedRemarks#}", acceptedRemarks);

    //        sb.Replace("{#sendToAmenmentBy#}", sendToAmendmentBy);
    //        sb.Replace("{#sendToAmenmentOn#}", sendToAmendmentOn);
    //        sb.Replace("{#sendToAmenmentRemarks#}", sendToAmendmentRemarks);

    //        sb.Replace("{#amendedby#}", amendedBy);
    //        sb.Replace("{#amendedon#}", amendedOn);
    //        sb.Replace("{#amendedremarks#}", amendedRemarks);

    //        sb.Replace("{#peAmendedApprovedBy#}", PEAmendedApprovedBy);
    //        sb.Replace("{#peAmendedApprovedOn#}", PEAmendedApprovedOn);
    //        sb.Replace("{#peAmendedApprovedRemarks#}", PEAmendedApprovedRemarks);

    //        sb.Replace("{#pmAmendedApprovedBy#}", PMAmendedApprovedBy);
    //        sb.Replace("{#pmAmendedApprovedOn#}", PMAmendedApprovedOn);
    //        sb.Replace("{#pmAmendedApprovedRemarks#}", PMAmendedApprovedRemarks);

    //        sb.Replace("{#amendedAcceptedBy#}", amendedAcceptedBy);
    //        sb.Replace("{#amendedAcceptedOn#}", amendedAcceptedOn);
    //        sb.Replace("{#amendedAcceptedRemarks#}", amendedAcceptedRemarks);

    //        sb.Replace("{#completedBy#}", completedBy);
    //        sb.Replace("{#completedOn#}", completedOn);
    //        sb.Replace("{#completedRemarks#}", completedRemarks);


    //        htmlText = sb.ToString();
    //        return htmlText;
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //}




    public string GetHtmlForPDF(DataTable dtLOT, DataTable dtSubitems)
    {
        try
        {
            companyName = string.Empty;
            LOTMainItem = string.Empty;
            LOTMainSubItem = string.Empty;
            customerName = string.Empty;
            custCode = string.Empty;
            jobNo = string.Empty;
            TFNo = string.Empty;
            poNo = string.Empty;
            LOTDate = string.Empty;
            itemName = string.Empty;
            impNotes = string.Empty;
            statusID = 0;

            PEApprovedByID = 0;
            PEApprovedBy = string.Empty;
            PEApprovedOn = string.Empty;
            PEApprovedRemarks = string.Empty;

            PMApprovedByID = 0;
            PMApprovedBy = string.Empty;
            PMApprovedOn = string.Empty;
            PMApprovedRemarks = string.Empty;

            acceptedByID = 0;
            acceptedBy = string.Empty;
            acceptedOn = string.Empty;
            acceptedRemarks = string.Empty;

            amendmentCount = 0;

            sendToAmendmentByID = 0;
            sendToAmendmentBy = string.Empty;
            sendToAmendmentOn = string.Empty;
            sendToAmendmentRemarks = string.Empty;

            PEAmendedApprovedByID = 0;
            PEAmendedApprovedBy = string.Empty;
            PEAmendedApprovedOn = string.Empty;
            PEAmendedApprovedRemarks = string.Empty;

            PMAmendedApprovedByID = 0;
            PMAmendedApprovedBy = string.Empty;
            PMAmendedApprovedOn = string.Empty;
            PMAmendedApprovedRemarks = string.Empty;

            amendedAcceptedByID = 0;
            amendedAcceptedBy = string.Empty;
            amendedAcceptedOn = string.Empty;
            amendedAcceptedRemarks = string.Empty;

            completedByID = 0;
            completedBy = string.Empty;
            completedOn = string.Empty;
            completedRemarks = string.Empty;

            if (dtLOT.Rows.Count > 0)
            {
                TFNo = Convert.ToString(dtLOT.Rows[0]["TF_NO"]).Trim();
                companyName = Convert.ToString(dtLOT.Rows[0]["UNIT_NAME"]).Trim();
                LOTDate = Convert.ToString(dtLOT.Rows[0]["DATE"]);
                custCode = Convert.ToString(dtLOT.Rows[0]["CUSTOMER_CODE"]).Trim();
                customerName = Convert.ToString(dtLOT.Rows[0]["CUSTOMER_NAME"]).Trim();
                jobNo = Convert.ToString(dtLOT.Rows[0]["JOB_NO"]).Trim();
                productionNo = Convert.ToString(dtLOT.Rows[0]["PRODUCTION_NO"]).Trim();
                poNo = Convert.ToString(dtLOT.Rows[0]["PO_NO"]).Trim();
                itemName = Convert.ToString(dtLOT.Rows[0]["ITEM_NAME"]).Trim();
                impNotes = Convert.ToString(dtLOT.Rows[0]["IMP_NOTES"]).Trim();


                //LOTMainItem = Convert.ToString(dtLOT.Rows[0]["LOT_MAIN_ITEM"]).Trim();
                //LOTMainSubItem = Convert.ToString(dtLOT.Rows[0]["LOT_MAIN_SUB_ITEM"]).Trim();



                #region MyRegion

                //statusID = Convert.ToInt32(dtLOT.Rows[0]["STATUS_ID"]);

                //PEApprovedByID = Convert.ToInt32(dtLOT.Rows[0]["PE_APPROVED_BY_ID"]);
                //PEApprovedBy = Convert.ToString(dtLOT.Rows[0]["PE_APPROVED_BY"]);
                //PEApprovedOn = Convert.ToString(dtLOT.Rows[0]["PE_APPROVED_ON"]);
                //PEApprovedRemarks = Convert.ToString(dtLOT.Rows[0]["PE_APPROVED_REMARKS"]).Trim();

                //PMApprovedByID = Convert.ToInt32(dtLOT.Rows[0]["PM_APPROVED_BY_ID"]);
                //PMApprovedBy = Convert.ToString(dtLOT.Rows[0]["PM_APPROVED_BY"]);
                //PMApprovedOn = Convert.ToString(dtLOT.Rows[0]["PM_APPROVED_ON"]);
                //PMApprovedRemarks = Convert.ToString(dtLOT.Rows[0]["PM_APPROVED_REMARKS"]).Trim();

                //acceptedByID = Convert.ToInt32(dtLOT.Rows[0]["ACCEPTED_BY_ID"]);
                //acceptedBy = Convert.ToString(dtLOT.Rows[0]["ACCEPTED_BY"]);
                //acceptedOn = Convert.ToString(dtLOT.Rows[0]["ACCEPTED_ON"]);
                //acceptedRemarks = Convert.ToString(dtLOT.Rows[0]["ACCEPTED_REMARKS"]).Trim();

                //amendmentCount = Convert.ToInt32(dtLOT.Rows[0]["AMENDMENT_COUNT"]);

                //sendToAmendmentByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDMENT_BY_ID"]);
                //sendToAmendmentBy = Convert.ToString(dtLOT.Rows[0]["AMENDMENT_BY"]);
                //sendToAmendmentOn = Convert.ToString(dtLOT.Rows[0]["AMENDMENT_ON"]);
                //sendToAmendmentRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDMENT_REMARKS"]).Trim();

                //amendedByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDED_BY_ID"]);
                //amendedBy = Convert.ToString(dtLOT.Rows[0]["AMENDED_BY"]);
                //amendedOn = Convert.ToString(dtLOT.Rows[0]["AMENDED_ON"]);
                //amendedRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDED_REMARKS"]).Trim();

                //PEAmendedApprovedByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDED_PE_APPROVED_BY_ID"]);
                //PEAmendedApprovedBy = Convert.ToString(dtLOT.Rows[0]["AMENDED_PE_APPROVED_BY"]);
                //PEAmendedApprovedOn = Convert.ToString(dtLOT.Rows[0]["AMENDED_PE_APPROVED_ON"]);
                //PEAmendedApprovedRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDED_PE_APPROVED_REMARKS"]).Trim();

                //PMAmendedApprovedByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDED_PM_APPROVED_BY_ID"]);
                //PMAmendedApprovedBy = Convert.ToString(dtLOT.Rows[0]["AMENDED_PM_APPROVED_BY"]);
                //PMAmendedApprovedOn = Convert.ToString(dtLOT.Rows[0]["AMENDED_PM_APPROVED_ON"]);
                //PMAmendedApprovedRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDED_PM_APPROVED_REMARKS"]).Trim();

                //amendedAcceptedByID = Convert.ToInt32(dtLOT.Rows[0]["AMENDED_ACCEPTED_BY_ID"]);
                //amendedAcceptedBy = Convert.ToString(dtLOT.Rows[0]["AMENDED_ACCEPTED_BY"]);
                //amendedAcceptedOn = Convert.ToString(dtLOT.Rows[0]["AMENDED_ACCEPTED_ON"]);
                //amendedAcceptedRemarks = Convert.ToString(dtLOT.Rows[0]["AMENDED_ACCEPTED_REMARKS"]).Trim();

                //completedByID = Convert.ToInt32(dtLOT.Rows[0]["COMPLETED_BY_ID"]);
                //completedBy = Convert.ToString(dtLOT.Rows[0]["COMPLETED_BY"]);
                //completedOn = Convert.ToString(dtLOT.Rows[0]["COMPLETED_ON"]);
                //completedRemarks = Convert.ToString(dtLOT.Rows[0]["COMPLETED_REMARKS"]).Trim();

                #endregion
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
            sb.Append("<td class='td1header'>Production No.:</td>\n");
            sb.Append("<td class='td2header'>{#ProductionNo#}</td>\n");
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
                sb.Append("<th class='tddesc'>Description</th>\n");
                sb.Append("<th class='tdtag'>Tag No.</th>\n");
                sb.Append("<th class='tdlotfor'>LOT For</th>\n");
                sb.Append("<th class='tddrgno'>Drg./Doc.No.</th>\n");
                sb.Append("<th class='tdrev'>Rev.</th>\n");
                sb.Append("<th class='tdcate'>Cat.</th>\n");
                sb.Append("<th class='tdquantity'>Quantity</th>\n");
                sb.Append("</tr>\n");

                foreach (DataRow dr in dtSubitems.Rows)
                {
                    srNo++;
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
                    sb.Append("<td class='tddesc'>{#subitemdesc#}</td>\n");
                    sb.Append("<td class='tdtag'>{#tagno#}</td>\n");
                    sb.Append("<td class='tdlotfor'>{#lotmainitem#}</td>\n");
                    sb.Append("<td class='tddrgno'>{#drawingno#}</td>\n");
                    sb.Append("<td class='tdrev'>{#revisionno#}</td>\n");
                    sb.Append("<td class='tdcate'>{#category#}</td>\n");
                    sb.Append("<td class='tdquantity'>{#quantity#}</td>\n");
                    sb.Append("</tr>\n");


                    sb.Replace("{#SRNo#}", Convert.ToString(srNo));
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




            #region MyRegion


            ////if (amendmentCount > 0 && !string.IsNullOrEmpty(amendedRemarks))
            //if (amendmentCount > 0 && amendedByID > 0)
            //{
            //    sb.Append("<table class='tblsignatories'>\n");
            //    sb.Append("<tr class='trsignatoriessigns'>\n");
            //    sb.Append("<td class='tdsignatories1'>Amended By:</td>\n");
            //    sb.Append("<td class='tdsignatories2'>{#amendedby#}</td>\n");
            //    sb.Append("<td class='tdsignatories1'>Amended On:</td>\n");
            //    sb.Append("<td class='tdsignatories2'>{#amendedon#}</td>\n");
            //    sb.Append("</tr>\n");

            //    sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

            //    sb.Append("<tr>\n");
            //    sb.Append("<td>Remarks:</td>\n");
            //    sb.Append("<td colspan='3'>&nbsp;</td>\n");
            //    sb.Append("</tr>\n");
            //    sb.Append("<tr>\n");
            //    sb.Append("<td colspan='4'>{#amendedremarks#}</td>\n");
            //    sb.Append("</tr>\n");

            //    sb.Append("</table>\n");

            //    sb.Append("<hr class='hrsignatories' />\n");
            //}



            //SIGNATORIES DETAILS
            //if (statusID > 1)
            //{
            //    sb.Append("<h3 class='header2'>Signatories</h3>\n");
            //    sb.Append("<hr />\n");



            //    // PROJECT ENGINEER APPROVED START--------------------------------
            //    sb.Append("<table class='tblsignatories'>\n");
            //    if (PEApprovedByID > 0)
            //    {
            //        sb.Append("<tr class='trsignatoriessigns'>\n");
            //        sb.Append("<td class='tdsignatories1'>Approved By:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#peApprovedBy#} [Project Engineer]</td>\n");
            //        sb.Append("<td class='tdsignatories1'>Approved On:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#peApprovedOn#}</td>\n");
            //        sb.Append("</tr>\n");

            //        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

            //        sb.Append("<tr>\n");
            //        sb.Append("<td>Remarks:</td>\n");
            //        sb.Append("<td colspan='3'>&nbsp;</td>\n");
            //        sb.Append("</tr>\n");
            //        sb.Append("<tr>\n");
            //        sb.Append("<td colspan='4'>{#peApprovedRemarks#}</td>\n");
            //        sb.Append("</tr>\n");
            //    }

            //    if (PEAmendedApprovedByID > 0)
            //    {
            //        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

            //        sb.Append("<tr class='trsignatoriessigns'>\n");
            //        sb.Append("<td class='tdsignatories1'>Amended Approved By:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#peAmendedApprovedBy#} [Project Engineer]</td>\n");
            //        sb.Append("<td class='tdsignatories1'>Amended Approved On:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#peAmendedApprovedOn#}</td>\n");
            //        sb.Append("</tr>\n");

            //        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

            //        sb.Append("<tr>\n");
            //        sb.Append("<td>Remarks:</td>\n");
            //        sb.Append("<td colspan='3'>&nbsp;</td>\n");
            //        sb.Append("</tr>\n");
            //        sb.Append("<tr>\n");
            //        sb.Append("<td colspan='4'>{#peAmendedApprovedRemarks#}</td>\n");
            //        sb.Append("</tr>\n");
            //    }

            //    sb.Append("<tr>\n");
            //    sb.Append("<td colspan='4'>&nbsp;</td>\n");
            //    sb.Append("</tr>\n");

            //    sb.Append("</table>\n");
            //    // PROJECT ENGINEER APPROVED END----------------------------------




            //    sb.Append("<hr class='hrsignatories'/>\n");




            //    // PROJECT MANAGER APPROVED START---------------------------------
            //    sb.Append("<table class='tblsignatories'>\n");

            //    if (PMApprovedByID > 0)
            //    {
            //        sb.Append("<tr class='trsignatoriessigns'>\n");
            //        sb.Append("<td class='tdsignatories1'>Approved By:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#pmApprovedBy#} [Project Manager]</td>\n");
            //        sb.Append("<td class='tdsignatories1'>Approved On:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#pmApprovedOn#}</td>\n");
            //        sb.Append("</tr>\n");

            //        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

            //        sb.Append("<tr>\n");
            //        sb.Append("<td>Remarks:</td>\n");
            //        sb.Append("<td colspan='3'>&nbsp;</td>\n");
            //        sb.Append("</tr>\n");
            //        sb.Append("<tr>\n");
            //        sb.Append("<td colspan='4'>{#pmApprovedRemarks#}</td>\n");
            //        sb.Append("</tr>\n");
            //    }
            //    if (PMAmendedApprovedByID > 0)
            //    {
            //        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

            //        sb.Append("<tr class='trsignatoriessigns'>\n");
            //        sb.Append("<td class='tdsignatories1'>Amended Approved By:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#pmAmendedApprovedBy#} [Project Manager]</td>\n");
            //        sb.Append("<td class='tdsignatories1'>Amended Approved On:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#pmAmendedApprovedOn#}</td>\n");
            //        sb.Append("</tr>\n");

            //        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

            //        sb.Append("<tr>\n");
            //        sb.Append("<td>Remarks:</td>\n");
            //        sb.Append("<td colspan='3'>&nbsp;</td>\n");
            //        sb.Append("</tr>\n");
            //        sb.Append("<tr>\n");
            //        sb.Append("<td colspan='4'>{#pmAmendedApprovedRemarks#}</td>\n");
            //        sb.Append("</tr>\n");

            //    }

            //    sb.Append("<tr>\n");
            //    sb.Append("<td colspan='4'>&nbsp;</td>\n");
            //    sb.Append("</tr>\n");

            //    sb.Append("</table>\n");

            //    // PROJECT MANAGER APPROVED END-----------------------------------



            //    sb.Append("<hr class='hrsignatories' />\n");



            //    // AMENDMENT START------------------------------------------------

            //    if (amendmentCount > 0 && sendToAmendmentByID > 0)
            //    {
            //        sb.Append("<table class='tblsignatories'>\n");

            //        sb.Append("<tr class='trsignatoriessigns'>\n");
            //        sb.Append("<td class='tdsignatories1'>Send To Amendment By:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#sendToAmenmentBy#}</td>\n");
            //        sb.Append("<td class='tdsignatories1'>Send To Amendment On:</td>\n");
            //        sb.Append("<td class='tdsignatories2'>{#sendToAmenmentOn#}</td>\n");
            //        sb.Append("</tr>\n");

            //        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");

            //        sb.Append("<tr>\n");
            //        sb.Append("<td>Remarks:</td>\n");
            //        sb.Append("<td colspan='3'>&nbsp;</td>\n");
            //        sb.Append("</tr>\n");
            //        sb.Append("<tr>\n");
            //        sb.Append("<td colspan='4'>{#sendToAmenmentRemarks#}</td>\n");
            //        sb.Append("</tr>\n");

            //        sb.Append("</table>\n");

            //        sb.Append("<hr class='hrsignatories' />\n");
            //    }

            //    // AMENDMENT END--------------------------------------------------

            //}

            #endregion

            sb.Replace("{#TFNo#}", TFNo);
            sb.Replace("{#ProductionNo#}", productionNo);
            sb.Replace("{#Company#}", companyName);
            sb.Replace("{#Date#}", LOTDate);
            sb.Replace("{#customerName#}", ("[" + custCode + "] - " + customerName));
            sb.Replace("{#JOBNo#}", jobNo);
            sb.Replace("{#PONo#}", poNo);
            sb.Replace("{#item#}", itemName);
            sb.Replace("{#impNotes#}", impNotes);



            #region MyRegion

            //sb.Replace("{#LOTMainItem#}", (LOTMainItem + " - [" + LOTMainSubItem + "]"));

            //sb.Replace("{#peApprovedBy#}", PEApprovedBy);
            //sb.Replace("{#peApprovedOn#}", PEApprovedOn);
            //sb.Replace("{#peApprovedRemarks#}", PEApprovedRemarks);

            //sb.Replace("{#pmApprovedBy#}", PMApprovedBy);
            //sb.Replace("{#pmApprovedOn#}", PMApprovedOn);
            //sb.Replace("{#pmApprovedRemarks#}", PMApprovedRemarks);

            //sb.Replace("{#acceptedBy#}", acceptedBy);
            //sb.Replace("{#acceptedOn#}", acceptedOn);
            //sb.Replace("{#acceptedRemarks#}", acceptedRemarks);

            //sb.Replace("{#sendToAmenmentBy#}", sendToAmendmentBy);
            //sb.Replace("{#sendToAmenmentOn#}", sendToAmendmentOn);
            //sb.Replace("{#sendToAmenmentRemarks#}", sendToAmendmentRemarks);

            //sb.Replace("{#amendedby#}", amendedBy);
            //sb.Replace("{#amendedon#}", amendedOn);
            //sb.Replace("{#amendedremarks#}", amendedRemarks);

            //sb.Replace("{#peAmendedApprovedBy#}", PEAmendedApprovedBy);
            //sb.Replace("{#peAmendedApprovedOn#}", PEAmendedApprovedOn);
            //sb.Replace("{#peAmendedApprovedRemarks#}", PEAmendedApprovedRemarks);

            //sb.Replace("{#pmAmendedApprovedBy#}", PMAmendedApprovedBy);
            //sb.Replace("{#pmAmendedApprovedOn#}", PMAmendedApprovedOn);
            //sb.Replace("{#pmAmendedApprovedRemarks#}", PMAmendedApprovedRemarks);

            //sb.Replace("{#amendedAcceptedBy#}", amendedAcceptedBy);
            //sb.Replace("{#amendedAcceptedOn#}", amendedAcceptedOn);
            //sb.Replace("{#amendedAcceptedRemarks#}", amendedAcceptedRemarks);

            //sb.Replace("{#completedBy#}", completedBy);
            //sb.Replace("{#completedOn#}", completedOn);
            //sb.Replace("{#completedRemarks#}", completedRemarks);

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