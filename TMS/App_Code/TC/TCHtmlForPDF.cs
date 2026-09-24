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


public class TCHtmlForPDF
{

    string PONo = string.Empty;
    string PODate = string.Empty;
    string VendorName = string.Empty;
    string VendorCode = string.Empty;
    string JOBNo = string.Empty;
    string Unit = string.Empty;
    string ItemName = string.Empty;
    string ItemCode = string.Empty;
    string IsCompleted = string.Empty;
    string CompletedRemarks = string.Empty;
    string StatusAll = string.Empty;

    string Status1 = string.Empty;
    string FileName1 = string.Empty;
    string AssignedTo1 = string.Empty;
    string IdentidficationReference1 = string.Empty;

    int tcRecordID1 = 0;
    int UploadedByID1 = 0;
    string UploadedRemarks1 = string.Empty;
    string UploadedBy1 = string.Empty;
    string UploadedOn1 = string.Empty;

    int AcceptedByID1 = 0;
    string AcceptedRemarks1 = string.Empty;
    string AcceptedBy1 = string.Empty;
    string AcceptedOn1 = string.Empty;

    int RejectedByID1 = 0;
    string RejectedRemarks1 = string.Empty;
    string RejectedBy1 = string.Empty;
    string RejectedOn1 = string.Empty;


    string Status2 = string.Empty;
    string FileName2 = string.Empty;
    string AssignedTo2 = string.Empty;
    string IdentidficationReference2 = string.Empty;

    int tcRecordID2 = 0;
    int UploadedByID2 = 0;
    string UploadedRemarks2 = string.Empty;
    string UploadedBy2 = string.Empty;
    string UploadedOn2 = string.Empty;

    int AcceptedByID2 = 0;
    string AcceptedRemarks2 = string.Empty;
    string AcceptedBy2 = string.Empty;
    string AcceptedOn2 = string.Empty;

    int RejectedByID2 = 0;
    string RejectedRemarks2 = string.Empty;
    string RejectedBy2 = string.Empty;
    string RejectedOn2 = string.Empty;

    string Status3 = string.Empty;
    string FileName3 = string.Empty;
    string AssignedTo3 = string.Empty;
    string IdentidficationReference3 = string.Empty;

    int tcRecordID3 = 0;
    int UploadedByID3 = 0;
    string UploadedRemarks3 = string.Empty;
    string UploadedBy3 = string.Empty;
    string UploadedOn3 = string.Empty;

    int AcceptedByID3 = 0;
    string AcceptedRemarks3 = string.Empty;
    string AcceptedBy3 = string.Empty;
    string AcceptedOn3 = string.Empty;

    int RejectedByID3 = 0;
    string RejectedRemarks3 = string.Empty;
    string RejectedBy3 = string.Empty;
    string RejectedOn3 = string.Empty;




    public string GetHtmlForPDF(DataTable dtTC)
    {
        try
        {
            PONo = string.Empty;
            PODate = string.Empty;
            VendorName = string.Empty;
            VendorCode = string.Empty;
            JOBNo = string.Empty;
            Unit = string.Empty;
            ItemName = string.Empty;
            ItemCode = string.Empty;
            IsCompleted = string.Empty;
            CompletedRemarks = string.Empty;
            StatusAll = string.Empty;

            Status1 = string.Empty;
            FileName1 = string.Empty;
            AssignedTo1 = string.Empty;
            IdentidficationReference1 = string.Empty;

            tcRecordID1 = 0;
            UploadedByID1 = 0;
            UploadedRemarks1 = string.Empty;
            UploadedBy1 = string.Empty;
            UploadedOn1 = string.Empty;

            AcceptedByID1 = 0;
            AcceptedRemarks1 = string.Empty;
            AcceptedBy1 = string.Empty;
            AcceptedOn1 = string.Empty;

            RejectedByID1 = 0;
            RejectedRemarks1 = string.Empty;
            RejectedBy1 = string.Empty;
            RejectedOn1 = string.Empty;

            Status2 = string.Empty;
            FileName2 = string.Empty;
            AssignedTo2 = string.Empty;
            IdentidficationReference2 = string.Empty;

            tcRecordID2 = 0;
            UploadedByID2 = 0;
            UploadedRemarks2 = string.Empty;
            UploadedBy2 = string.Empty;
            UploadedOn2 = string.Empty;

            AcceptedByID2 = 0;
            AcceptedRemarks2 = string.Empty;
            AcceptedBy2 = string.Empty;
            AcceptedOn2 = string.Empty;

            RejectedByID2 = 0;
            RejectedRemarks2 = string.Empty;
            RejectedBy2 = string.Empty;
            RejectedOn2 = string.Empty;

            Status3 = string.Empty;
            FileName3 = string.Empty;
            AssignedTo3 = string.Empty;
            IdentidficationReference3 = string.Empty;

            tcRecordID3 = 0;
            UploadedByID3 = 0;
            UploadedRemarks3 = string.Empty;
            UploadedBy3 = string.Empty;
            UploadedOn3 = string.Empty;

            AcceptedByID3 = 0;
            AcceptedRemarks3 = string.Empty;
            AcceptedBy3 = string.Empty;
            AcceptedOn3 = string.Empty;

            RejectedByID3 = 0;
            RejectedRemarks3 = string.Empty;
            RejectedBy3 = string.Empty;
            RejectedOn3 = string.Empty;


            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (dtTC.Rows.Count > 0)
            {
                if (dtTC.Rows[0]["PO_NO"] != DBNull.Value)
                    PONo = Convert.ToString(dtTC.Rows[0]["PO_NO"]).Trim();

                if (dtTC.Rows[0]["PO_DATE"] != DBNull.Value)
                    PODate = Convert.ToString(dtTC.Rows[0]["PO_DATE"]).Trim();

                if (dtTC.Rows[0]["VENDOR_CODE"] != DBNull.Value)
                    VendorName = Convert.ToString(dtTC.Rows[0]["VENDOR_CODE"]).Trim();

                if (dtTC.Rows[0]["VENDOR_NAME"] != DBNull.Value)
                    VendorCode = Convert.ToString(dtTC.Rows[0]["VENDOR_NAME"]).Trim();

                if (dtTC.Rows[0]["JOB_NO"] != DBNull.Value)
                    JOBNo = Convert.ToString(dtTC.Rows[0]["JOB_NO"]).Trim();

                if (dtTC.Rows[0]["UNIT_NAME"] != DBNull.Value)
                    Unit = Convert.ToString(dtTC.Rows[0]["UNIT_NAME"]).Trim();

                if (dtTC.Rows[0]["ITEM_NAME"] != DBNull.Value)
                    ItemName = Convert.ToString(dtTC.Rows[0]["ITEM_NAME"]).Trim();

                if (dtTC.Rows[0]["ITEM_CODE"] != DBNull.Value)
                    ItemCode = Convert.ToString(dtTC.Rows[0]["ITEM_CODE"]).Trim();

                if (dtTC.Rows[0]["IS_COMPLETED"] != DBNull.Value)
                    IsCompleted = Convert.ToString(dtTC.Rows[0]["IS_COMPLETED"]).Trim();

                if (dtTC.Rows[0]["IS_COMPLETED_REMARKS"] != DBNull.Value)
                    CompletedRemarks = Convert.ToString(dtTC.Rows[0]["IS_COMPLETED_REMARKS"]).Trim();

                if (dtTC.Rows[0]["STATUS_NAME"] != DBNull.Value)
                    StatusAll = Convert.ToString(dtTC.Rows[0]["STATUS_NAME"]).Trim();

                if (dtTC.Rows[0]["TC_RECORD_ID1"] != DBNull.Value)
                    tcRecordID1 = Convert.ToInt32(dtTC.Rows[0]["TC_RECORD_ID1"]);

                if (dtTC.Rows[0]["STATUS_NAME1"] != DBNull.Value)
                    Status1 = Convert.ToString(dtTC.Rows[0]["STATUS_NAME1"]).Trim();

                if (dtTC.Rows[0]["TC_ATTACHMENT_NAME1"] != DBNull.Value)
                    FileName1 = Convert.ToString(dtTC.Rows[0]["TC_ATTACHMENT_NAME1"]).Trim();

                if (dtTC.Rows[0]["ASSIGNED_TO1"] != DBNull.Value)
                    AssignedTo1 = Convert.ToString(dtTC.Rows[0]["ASSIGNED_TO1"]).Trim();

                if (dtTC.Rows[0]["IDENTIFICATION_REFERENCE_NO1"] != DBNull.Value)
                    IdentidficationReference1 = Convert.ToString(dtTC.Rows[0]["IDENTIFICATION_REFERENCE_NO1"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_BY_ID1"] != DBNull.Value)
                    UploadedByID1 = Convert.ToInt32(dtTC.Rows[0]["UPLOADED_BY_ID1"]);

                if (dtTC.Rows[0]["UPLOADED_REMARKS1"] != DBNull.Value)
                    UploadedRemarks1 = Convert.ToString(dtTC.Rows[0]["UPLOADED_REMARKS1"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_BY1"] != DBNull.Value)
                    UploadedBy1 = Convert.ToString(dtTC.Rows[0]["UPLOADED_BY1"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_ON1"] != DBNull.Value)
                    UploadedOn1 = Convert.ToString(dtTC.Rows[0]["UPLOADED_ON1"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_BY_ID1"] != DBNull.Value)
                    AcceptedByID1 = Convert.ToInt32(dtTC.Rows[0]["ACCEPTED_BY_ID1"]);

                if (dtTC.Rows[0]["ACCEPTED_REMARKS1"] != DBNull.Value)
                    AcceptedRemarks1 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_REMARKS1"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_BY1"] != DBNull.Value)
                    AcceptedBy1 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_BY1"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_ON1"] != DBNull.Value)
                    AcceptedOn1 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_ON1"]).Trim();

                if (dtTC.Rows[0]["REJECTED_BY_ID1"] != DBNull.Value)
                    RejectedByID1 = Convert.ToInt32(dtTC.Rows[0]["REJECTED_BY_ID1"]);

                if (dtTC.Rows[0]["REJECTED_REMARKS1"] != DBNull.Value)
                    RejectedRemarks1 = Convert.ToString(dtTC.Rows[0]["REJECTED_REMARKS1"]).Trim();

                if (dtTC.Rows[0]["REJECTED_BY1"] != DBNull.Value)
                    RejectedBy1 = Convert.ToString(dtTC.Rows[0]["REJECTED_BY1"]).Trim();

                if (dtTC.Rows[0]["REJECTED_ON1"] != DBNull.Value)
                    RejectedOn1 = Convert.ToString(dtTC.Rows[0]["REJECTED_ON1"]).Trim();

                if (dtTC.Rows[0]["TC_RECORD_ID2"] != DBNull.Value)
                    tcRecordID2 = Convert.ToInt32(dtTC.Rows[0]["TC_RECORD_ID2"]);

                if (dtTC.Rows[0]["STATUS_NAME2"] != DBNull.Value)
                    Status2 = Convert.ToString(dtTC.Rows[0]["STATUS_NAME2"]).Trim();

                if (dtTC.Rows[0]["TC_ATTACHMENT_NAME2"] != DBNull.Value)
                    FileName2 = Convert.ToString(dtTC.Rows[0]["TC_ATTACHMENT_NAME2"]).Trim();

                if (dtTC.Rows[0]["ASSIGNED_TO2"] != DBNull.Value)
                    AssignedTo2 = Convert.ToString(dtTC.Rows[0]["ASSIGNED_TO2"]).Trim();

                if (dtTC.Rows[0]["IDENTIFICATION_REFERENCE_NO2"] != DBNull.Value)
                    IdentidficationReference2 = Convert.ToString(dtTC.Rows[0]["IDENTIFICATION_REFERENCE_NO2"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_BY_ID2"] != DBNull.Value)
                    UploadedByID2 = Convert.ToInt32(dtTC.Rows[0]["UPLOADED_BY_ID2"]);

                if (dtTC.Rows[0]["UPLOADED_REMARKS2"] != DBNull.Value)
                    UploadedRemarks2 = Convert.ToString(dtTC.Rows[0]["UPLOADED_REMARKS2"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_BY2"] != DBNull.Value)
                    UploadedBy2 = Convert.ToString(dtTC.Rows[0]["UPLOADED_BY2"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_ON2"] != DBNull.Value)
                    UploadedOn2 = Convert.ToString(dtTC.Rows[0]["UPLOADED_ON2"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_BY_ID2"] != DBNull.Value)
                    AcceptedByID2 = Convert.ToInt32(dtTC.Rows[0]["ACCEPTED_BY_ID2"]);

                if (dtTC.Rows[0]["ACCEPTED_REMARKS2"] != DBNull.Value)
                    AcceptedRemarks2 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_REMARKS2"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_BY2"] != DBNull.Value)
                    AcceptedBy2 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_BY2"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_ON2"] != DBNull.Value)
                    AcceptedOn2 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_ON2"]).Trim();

                if (dtTC.Rows[0]["REJECTED_BY_ID2"] != DBNull.Value)
                    RejectedByID2 = Convert.ToInt32(dtTC.Rows[0]["REJECTED_BY_ID2"]);

                if (dtTC.Rows[0]["REJECTED_REMARKS2"] != DBNull.Value)
                    RejectedRemarks2 = Convert.ToString(dtTC.Rows[0]["REJECTED_REMARKS2"]).Trim();

                if (dtTC.Rows[0]["REJECTED_BY2"] != DBNull.Value)
                    RejectedBy2 = Convert.ToString(dtTC.Rows[0]["REJECTED_BY2"]).Trim();

                if (dtTC.Rows[0]["REJECTED_ON2"] != DBNull.Value)
                    RejectedOn2 = Convert.ToString(dtTC.Rows[0]["REJECTED_ON2"]).Trim();

                if (dtTC.Rows[0]["TC_RECORD_ID3"] != DBNull.Value)
                    tcRecordID3 = Convert.ToInt32(dtTC.Rows[0]["TC_RECORD_ID3"]);

                if (dtTC.Rows[0]["STATUS_NAME3"] != DBNull.Value)
                    Status3 = Convert.ToString(dtTC.Rows[0]["STATUS_NAME3"]).Trim();

                if (dtTC.Rows[0]["TC_ATTACHMENT_NAME3"] != DBNull.Value)
                    FileName3 = Convert.ToString(dtTC.Rows[0]["TC_ATTACHMENT_NAME3"]).Trim();

                if (dtTC.Rows[0]["ASSIGNED_TO3"] != DBNull.Value)
                    AssignedTo3 = Convert.ToString(dtTC.Rows[0]["ASSIGNED_TO3"]).Trim();

                if (dtTC.Rows[0]["IDENTIFICATION_REFERENCE_NO3"] != DBNull.Value)
                    IdentidficationReference3 = Convert.ToString(dtTC.Rows[0]["IDENTIFICATION_REFERENCE_NO3"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_BY_ID3"] != DBNull.Value)
                    UploadedByID3 = Convert.ToInt32(dtTC.Rows[0]["UPLOADED_BY_ID3"]);

                if (dtTC.Rows[0]["UPLOADED_REMARKS3"] != DBNull.Value)
                    UploadedRemarks3 = Convert.ToString(dtTC.Rows[0]["UPLOADED_REMARKS3"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_BY3"] != DBNull.Value)
                    UploadedBy3 = Convert.ToString(dtTC.Rows[0]["UPLOADED_BY3"]).Trim();

                if (dtTC.Rows[0]["UPLOADED_ON3"] != DBNull.Value)
                    UploadedOn3 = Convert.ToString(dtTC.Rows[0]["UPLOADED_ON3"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_BY_ID3"] != DBNull.Value)
                    AcceptedByID3 = Convert.ToInt32(dtTC.Rows[0]["ACCEPTED_BY_ID3"]);

                if (dtTC.Rows[0]["ACCEPTED_REMARKS3"] != DBNull.Value)
                    AcceptedRemarks3 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_REMARKS3"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_BY3"] != DBNull.Value)
                    AcceptedBy3 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_BY3"]).Trim();

                if (dtTC.Rows[0]["ACCEPTED_ON3"] != DBNull.Value)
                    AcceptedOn3 = Convert.ToString(dtTC.Rows[0]["ACCEPTED_ON3"]).Trim();

                if (dtTC.Rows[0]["REJECTED_BY_ID3"] != DBNull.Value)
                    RejectedByID3 = Convert.ToInt32(dtTC.Rows[0]["REJECTED_BY_ID3"]);

                if (dtTC.Rows[0]["REJECTED_REMARKS3"] != DBNull.Value)
                    RejectedRemarks3 = Convert.ToString(dtTC.Rows[0]["REJECTED_REMARKS3"]).Trim();

                if (dtTC.Rows[0]["REJECTED_BY3"] != DBNull.Value)
                    RejectedBy3 = Convert.ToString(dtTC.Rows[0]["REJECTED_BY3"]).Trim();

                if (dtTC.Rows[0]["REJECTED_ON3"] != DBNull.Value)
                    RejectedOn3 = Convert.ToString(dtTC.Rows[0]["REJECTED_ON3"]).Trim();






                sb.Append("<h2 class='headerStyle'>TC(s) Detail</h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>PO No:</td>\n");
                sb.Append("<td class='td2header'>{#PONo#}</td>\n");
                sb.Append("<td class='td1header'>PO Date:</td>\n");
                sb.Append("<td class='td2header'>{#PODate#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Vendor Name:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#VendorName#} - [{#VendorCode#}]</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>JOB No:</td>\n");
                sb.Append("<td class='td2header'>{#JOBNo#}</td>\n");
                sb.Append("<td class='td1header'>Unit:</td>\n");
                sb.Append("<td class='td2header'>{#Unit#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Item:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#ItemName#} - [{#ItemCode#}]</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Is Completed?:</td>\n");
                sb.Append("<td class='td2header'>{#IsCompleted#}</td>\n");
                sb.Append("<td class='td1header'>Status All:</td>\n");
                sb.Append("<td class='td2header'>{#StatusAll#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Completed Remarks:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#CompletedRemarks#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("</table>\n");
                sb.Append("<hr />\n");

                if (tcRecordID1 > 0)
                {
                    sb.Append("<h3 class='header2'><u>TC-01</u></h3>\n");
                    sb.Append("<table class='tblheader'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Status:</td>\n");
                    sb.Append("<td class='td2header'>{#Status1#}</td>\n");
                    sb.Append("<td class='td1header'>File Name:</td>\n");
                    sb.Append("<td class='td2header'>{#FileName1#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Assigned To:</td>\n");
                    sb.Append("<td class='td2header'>{#AssignedTo1#}</td>\n");
                    sb.Append("<td class='td1header'>Identidfication Reference:</td>\n");
                    sb.Append("<td class='td2header'>{#IdentidficationReference1#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");

                    sb.Append("<hr class='hrsignatories' />\n");

                    if (UploadedByID1 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Uploaded</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Uploaded Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#UploadedRemarks1#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Uploaded By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#UploadedBy1#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Uploaded On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#UploadedOn1#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (AcceptedByID1 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Accepted</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#AcceptedRemarks1#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#AcceptedBy1#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#AcceptedOn1#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (RejectedByID1 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Rejected</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#RejectedRemarks1#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#RejectedBy1#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#RejectedOn1#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");
                    }
                }


                if (tcRecordID2 > 0)
                {
                    sb.Append("<h3 class='header2'><u>TC-02</u></h3>\n");
                    sb.Append("<table class='tblheader'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Status:</td>\n");
                    sb.Append("<td class='td2header'>{#Status2#}</td>\n");
                    sb.Append("<td class='td1header'>File Name:</td>\n");
                    sb.Append("<td class='td2header'>{#FileName2#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Assigned To:</td>\n");
                    sb.Append("<td class='td2header'>{#AssignedTo2#}</td>\n");
                    sb.Append("<td class='td1header'>Identidfication Reference:</td>\n");
                    sb.Append("<td class='td2header'>{#IdentidficationReference2#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");

                    sb.Append("<hr class='hrsignatories' />\n");

                    if (UploadedByID2 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Uploaded</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Uploaded Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#UploadedRemarks2#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Uploaded By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#UploadedBy2#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Uploaded On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#UploadedOn2#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (AcceptedByID2 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Accepted</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#AcceptedRemarks2#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#AcceptedBy2#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#AcceptedOn2#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (RejectedByID2 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Rejected</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#RejectedRemarks2#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#RejectedBy2#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#RejectedOn2#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");
                    }
                }

                if (tcRecordID3 > 0)
                {
                    sb.Append("<h3 class='header2'><u>TC-03</u></h3>\n");
                    sb.Append("<table class='tblheader'>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Status:</td>\n");
                    sb.Append("<td class='td2header'>{#Status3#}</td>\n");
                    sb.Append("<td class='td1header'>File Name:</td>\n");
                    sb.Append("<td class='td2header'>{#FileName3#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Assigned To:</td>\n");
                    sb.Append("<td class='td2header'>{#AssignedTo3#}</td>\n");
                    sb.Append("<td class='td1header'>Identidfication Reference:</td>\n");
                    sb.Append("<td class='td2header'>{#IdentidficationReference3#}</td>\n");
                    sb.Append("</tr>\n");
                    sb.Append("</table>\n");

                    sb.Append("<hr class='hrsignatories' />\n");

                    if (UploadedByID3 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Uploaded</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Uploaded Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#UploadedRemarks3#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Uploaded By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#UploadedBy3#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Uploaded On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#UploadedOn3#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (AcceptedByID3 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Accepted</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Accepted Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#AcceptedRemarks3#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#AcceptedBy3#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Accepted On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#AcceptedOn3#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");

                        sb.Append("<hr class='hrsignatories' />\n");
                    }

                    if (RejectedByID3 > 0)
                    {
                        sb.Append("<fieldset class='pdffieldset'>\n");
                        sb.Append("<legend class='pdflegend'>Rejected</legend>\n");
                        sb.Append("<table class='tblsignatories'>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='3'>Rejected Remarks:</td>\n");
                        sb.Append("<td colspan='3'>&nbsp;</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td colspan='4'>{#RejectedRemarks3#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("<tr><td colspan='4'>&nbsp;</td></tr>\n");
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsignatories1'>Rejected By:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#RejectedBy3#}</td>\n");
                        sb.Append("<td class='tdsignatories1'>Rejected On:</td>\n");
                        sb.Append("<td class='tdsignatories2'>{#RejectedOn3#}</td>\n");
                        sb.Append("</tr>\n");
                        sb.Append("</table>\n");
                        sb.Append("</fieldset>\n");
                        sb.Append("<hr class='hrsignatories' />\n");
                    }
                }



                sb.Replace("{#PONo#}", PONo);
                sb.Replace("{#PODate#}", PODate);
                sb.Replace("{#VendorName#}", VendorName);
                sb.Replace("{#VendorCode#}", VendorCode);
                sb.Replace("{#JOBNo#}", JOBNo);
                sb.Replace("{#Unit#}", Unit);
                sb.Replace("{#ItemName#}", ItemName);
                sb.Replace("{#ItemCode#}", ItemCode);
                sb.Replace("{#IsCompleted#}", IsCompleted);
                sb.Replace("{#CompletedRemarks#}", CompletedRemarks);
                sb.Replace("{#StatusAll#}", StatusAll);
                sb.Replace("{#Status1#}", Status1);
                sb.Replace("{#FileName1#}", FileName1);
                sb.Replace("{#AssignedTo1#}", AssignedTo1);
                sb.Replace("{#IdentidficationReference1#}", IdentidficationReference1);
                sb.Replace("{#UploadedRemarks1#}", UploadedRemarks1);
                sb.Replace("{#UploadedBy1#}", UploadedBy1);
                sb.Replace("{#UploadedOn1#}", UploadedOn1);                
                sb.Replace("{#AcceptedRemarks1#}", AcceptedRemarks1);
                sb.Replace("{#AcceptedBy1#}", AcceptedBy1);
                sb.Replace("{#AcceptedOn1#}", AcceptedOn1);                
                sb.Replace("{#RejectedRemarks1#}", RejectedRemarks1);
                sb.Replace("{#RejectedBy1#}", RejectedBy1);
                sb.Replace("{#RejectedOn1#}", RejectedOn1);                
                sb.Replace("{#Status2#}", Status2);
                sb.Replace("{#FileName2#}", FileName2);
                sb.Replace("{#AssignedTo2#}", AssignedTo2);
                sb.Replace("{#IdentidficationReference2#}", IdentidficationReference2);                
                sb.Replace("{#UploadedRemarks2#}", UploadedRemarks2);
                sb.Replace("{#UploadedBy2#}", UploadedBy2);
                sb.Replace("{#UploadedOn2#}", UploadedOn2);                
                sb.Replace("{#AcceptedRemarks2#}", AcceptedRemarks2);
                sb.Replace("{#AcceptedBy2#}", AcceptedBy2);
                sb.Replace("{#AcceptedOn2#}", AcceptedOn2);                
                sb.Replace("{#RejectedRemarks2#}", RejectedRemarks2);
                sb.Replace("{#RejectedBy2#}", RejectedBy2);
                sb.Replace("{#RejectedOn2#}", RejectedOn2);                
                sb.Replace("{#Status3#}", Status3);
                sb.Replace("{#FileName3#}", FileName3);
                sb.Replace("{#AssignedTo3#}", AssignedTo3);
                sb.Replace("{#IdentidficationReference3#}", IdentidficationReference3);                
                sb.Replace("{#UploadedRemarks3#}", UploadedRemarks3);
                sb.Replace("{#UploadedBy3#}", UploadedBy3);
                sb.Replace("{#UploadedOn3#}", UploadedOn3);                
                sb.Replace("{#AcceptedRemarks3#}", AcceptedRemarks3);
                sb.Replace("{#AcceptedBy3#}", AcceptedBy3);
                sb.Replace("{#AcceptedOn3#}", AcceptedOn3);               
                sb.Replace("{#RejectedRemarks3#}", RejectedRemarks3);
                sb.Replace("{#RejectedBy3#}", RejectedBy3);
                sb.Replace("{#RejectedOn3#}", RejectedOn3);
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