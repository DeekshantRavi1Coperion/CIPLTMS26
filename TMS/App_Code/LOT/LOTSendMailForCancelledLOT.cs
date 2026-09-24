using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using BAL;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;


public class LOTSendMailForCancelledLOT
{

    #region VARIABLES[===================]


    GetLOTMailType objGetLOTMailType = new GetLOTMailType();

    //GetLOTNextStatus objGetLOTNextStatus = new GetLOTNextStatus();
    LOTHtmlForPDFApproval objLOTHtmlForPDFApproval = new LOTHtmlForPDFApproval();
    LOTMailInfoProperties objLOTMailInfoProperties = new LOTMailInfoProperties();
    Project objProject = new Project();
    DataSet dsMailInfo = new DataSet();

    int currentStatusID = 0;
    //int nextStatusID = 0;
    int returnVal = 0;
    bool isCCAllowed = false;
    int mailType = 0;
    int LOTMainSubitemID = 0;
    int newStatusID = 0;
    int recordNo = 0;
    int LOTTFSubitemID = 0;

    int cancelledByID = 0;
    string cancelledByName = string.Empty;
    string cancelledByEmail = string.Empty;

    int createdByID = 0;
    string createdByName = string.Empty;
    string createdByEmail = string.Empty;

    int amendedByID = 0;
    string amendedByName = string.Empty;
    string amendedByEmail = string.Empty;

    int PEID = 0;
    string PEName = string.Empty;
    string PEEmail = string.Empty;
    string PEUD = string.Empty;
    string PEPD = string.Empty;

    int PMID = 0;
    string PMName = string.Empty;
    string PMEmail = string.Empty;
    string PMUD = string.Empty;
    string PMPD = string.Empty;


    int planningMngrID = 0;
    string planningMngrIDs = string.Empty;
    string planningMngrName = string.Empty;
    string planningMngrEmail = string.Empty;
    string planningMngrEmailCC = string.Empty;

    int prodMngrID = 0;
    string prodMngrName = string.Empty;
    string prodMngrEmail = string.Empty;
    string prodMngrEmailCC = string.Empty;


    string qualityMngrName = string.Empty;
    string qualityNames = string.Empty;
    string qualityEmails = string.Empty;

    //string planningMngrName = string.Empty;
    //string planningNames = string.Empty;
    //string planningEmails = string.Empty;

    int approvedByID = 0;
    string approvedByName = string.Empty;
    string approvedByEmail = string.Empty;

    int planningAcceptedByID = 0;
    string planningAcceptedByName = string.Empty;
    string planningAcceptedByEmail = string.Empty;

    int forwardedByID = 0;
    string forwardedByName = string.Empty;
    string forwardedByEmail = string.Empty;

    int acceptedByID = 0;
    string acceptedByName = string.Empty;
    string acceptedByEmail = string.Empty;

    int qualityAcceptedByID = 0;
    string qualityAcceptedByName = string.Empty;
    string qualityAcceptedByEmail = string.Empty;



    int sendToIntlInspectionByID = 0;
    string sendToIntlInspectionByName = string.Empty;
    string sendToIntlInspectionByEmail = string.Empty;

    int qAIntlInspectionAcceptedByID = 0;
    string qAIntlInspectionAcceptedByName = string.Empty;
    string qAIntlInspectionAcceptedByEmail = string.Empty;

    int qAIntlInspectionNotAcceptedByID = 0;
    string qAIntlInspectionNotAcceptedByName = string.Empty;
    string qAIntlInspectionNotAcceptedByEmail = string.Empty;

    int sentToFinalInspByID = 0;
    string sentToFinalInspByName = string.Empty;
    string sentToFinalInspByEmail = string.Empty;

    int sentToReworkByID = 0;
    string sentToReworkByName = string.Empty;
    string sentToReworkByEmail = string.Empty;


    int amendmentCount = 0;

    int amendmentByID = 0;
    string amendmentByName = string.Empty;
    string amendmentByEmail = string.Empty;

    int prodAmendmentByID = 0;
    string prodAmendmentByName = string.Empty;
    string prodAmendmentByEmail = string.Empty;

    int amendedApprovedByID = 0;
    string amendedApprovedByName = string.Empty;
    string amendedApprovedByEmail = string.Empty;

    int planningAmendedAcceptedByID = 0;
    string planningAmendedAcceptedByName = string.Empty;
    string planningAmendedAcceptedByEmail = string.Empty;

    int forwardedAmendedByID = 0;
    string forwardedAmendedByName = string.Empty;
    string forwardedAmendedByEmail = string.Empty;

    int amendedAcceptedByID = 0;
    string amendedAcceptedByName = string.Empty;
    string amendedAcceptedByEmail = string.Empty;

    int qualityAmendedAcceptedByID = 0;
    string qualityAmendedAcceptedByName = string.Empty;
    string qualityAmendedAcceptedByEmail = string.Empty;


    int completedByID = 0;
    string completedByName = string.Empty;
    string completedByEmail = string.Empty;



    string urlTxt = string.Empty;
    string href = string.Empty;
    string link = string.Empty;

    string PEApproveHref = string.Empty;
    string PEApproveLink = string.Empty;

    //string PEAmendmentHref = string.Empty;
    //string PEAmendmentLink = string.Empty;

    string PMApproveHref = string.Empty;
    string PMApproveLink = string.Empty;

    //string PMAmendmentHref = string.Empty;
    //string PMAmendmentLink = string.Empty;

    string fileName = string.Empty;
    string body = string.Empty;
    string subject = string.Empty;
    string from = string.Empty;
    string fromName = string.Empty;
    string to = string.Empty;
    string toName = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;

    bool isPlannigMailSent = false;
    bool isQualityMailSent = false;

    DataTable dtQuantityDetails = new DataTable();
    #endregion

    public int SendEmailCancelledLOT(int LOTTFID, string TFNo, int unitID, string remarks)
    {
        try
        {
            #region Datatables

            DataTable dtSi = new DataTable();

            #region dsSi COLUMNS

            dtSi.Columns.Add("RECORD_NO", typeof(int));
            dtSi.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
            dtSi.Columns.Add("STATUS_ID", typeof(int));
            //dtSi.Columns.Add("NEXT_STATUS_ID", typeof(int));
            dtSi.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtSi.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            dtSi.Columns.Add("SUBITEM_DESC", typeof(string));
            dtSi.Columns.Add("TAG_NO", typeof(string));
            dtSi.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
            dtSi.Columns.Add("LOT_MAIN_ITEM", typeof(string));
            dtSi.Columns.Add("DRAWING_NO", typeof(string));
            dtSi.Columns.Add("REVISION_NO", typeof(int));
            dtSi.Columns.Add("CATEGORY", typeof(string));
            dtSi.Columns.Add("QUANTITY", typeof(int));
            dtSi.Columns.Add("PRODUCTION_MNGR_ID", typeof(int));
            dtSi.Columns.Add("PRODUCTION_MNGR_NAME", typeof(string));

            dtSi.Columns.Add("CREATED_BY_ID", typeof(int));
            dtSi.Columns.Add("CREATED_BY", typeof(string));
            dtSi.Columns.Add("CREATED_ON", typeof(string));

            dtSi.Columns.Add("APPROVED_BY_ID", typeof(int));
            dtSi.Columns.Add("APPROVED_BY", typeof(string));
            dtSi.Columns.Add("APPROVED_ON", typeof(string));
            dtSi.Columns.Add("APPROVED_REMARKS", typeof(string));

            dtSi.Columns.Add("PLANNING_ACCEPTED_BY_ID", typeof(int));
            dtSi.Columns.Add("PLANNING_ACCEPTED_BY", typeof(string));
            dtSi.Columns.Add("PLANNING_ACCEPTED_ON", typeof(string));
            dtSi.Columns.Add("PLANNING_ACCEPTED_REMARKS", typeof(string));

            dtSi.Columns.Add("FORWARDED_BY_ID", typeof(int));
            dtSi.Columns.Add("FORWARDED_BY", typeof(string));
            dtSi.Columns.Add("FORWARDED_ON", typeof(string));
            dtSi.Columns.Add("FORWARDED_REMARKS", typeof(string));

            dtSi.Columns.Add("ACCEPTED_BY_ID", typeof(int));
            dtSi.Columns.Add("ACCEPTED_BY", typeof(string));
            dtSi.Columns.Add("ACCEPTED_ON", typeof(string));
            dtSi.Columns.Add("ACCEPTED_REMARKS", typeof(string));

            dtSi.Columns.Add("QUALITY_ACCEPTED_BY_ID", typeof(int));
            dtSi.Columns.Add("QUALITY_ACCEPTED_BY", typeof(string));
            dtSi.Columns.Add("QUALITY_ACCEPTED_ON", typeof(string));
            dtSi.Columns.Add("QUALITY_ACCEPTED_REMARKS", typeof(string));

            dtSi.Columns.Add("SENT_TO_INTL_INSP_BY_ID", typeof(int));
            dtSi.Columns.Add("SENT_TO_INTL_INSP_BY", typeof(string));
            dtSi.Columns.Add("SENT_TO_INTL_INSP_ON", typeof(string));
            dtSi.Columns.Add("SENT_TO_INTL_INSP_REMARKS", typeof(string));

            dtSi.Columns.Add("QA_INTL_INSP_ACCEPTED_BY_ID", typeof(int));
            dtSi.Columns.Add("QA_INTL_INSP_ACCEPTED_BY", typeof(string));
            dtSi.Columns.Add("QA_INTL_INSP_ACCEPTED_ON", typeof(string));
            dtSi.Columns.Add("QA_INTL_INSP_ACCEPTED_REMARKS", typeof(string));

            dtSi.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_BY_ID", typeof(int));
            dtSi.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_BY", typeof(string));
            dtSi.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_ON", typeof(string));
            dtSi.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_REMARKS", typeof(string));



            dtSi.Columns.Add("SENT_TO_FINAL_INSP_BY_ID", typeof(int));
            dtSi.Columns.Add("SENT_TO_FINAL_INSP_BY", typeof(string));
            dtSi.Columns.Add("SENT_TO_FINAL_INSP_ON", typeof(string));
            dtSi.Columns.Add("SENT_TO_FINAL_INSP_REMARKS", typeof(string));


            dtSi.Columns.Add("SENT_TO_REWORK_BY_ID", typeof(int));
            dtSi.Columns.Add("SENT_TO_REWORK_BY", typeof(string));
            dtSi.Columns.Add("SENT_TO_REWORK_ON", typeof(string));
            dtSi.Columns.Add("SENT_TO_REWORK_REMARKS", typeof(string));



            dtSi.Columns.Add("AMENDMENT_COUNT", typeof(int));
            dtSi.Columns.Add("AMENDMENT_BY_ID", typeof(int));
            dtSi.Columns.Add("AMENDMENT_BY", typeof(string));
            dtSi.Columns.Add("AMENDMENT_ON", typeof(string));
            dtSi.Columns.Add("AMENDMENT_REMARKS", typeof(string));

            dtSi.Columns.Add("PROD_AMENDMENT_BY_ID", typeof(int));
            dtSi.Columns.Add("PROD_AMENDMENT_BY", typeof(string));
            dtSi.Columns.Add("PROD_AMENDMENT_ON", typeof(string));
            dtSi.Columns.Add("PROD_AMENDMENT_REMARKS", typeof(string));


            dtSi.Columns.Add("AMENDED_BY_ID", typeof(int));
            dtSi.Columns.Add("AMENDED_BY", typeof(string));
            dtSi.Columns.Add("AMENDED_ON", typeof(string));
            dtSi.Columns.Add("AMENDED_REMARKS", typeof(string));

            dtSi.Columns.Add("AMENDED_APPROVED_BY_ID", typeof(int));
            dtSi.Columns.Add("AMENDED_APPROVED_BY", typeof(string));
            dtSi.Columns.Add("AMENDED_APPROVED_ON", typeof(string));
            dtSi.Columns.Add("AMENDED_APPROVED_REMARKS", typeof(string));

            dtSi.Columns.Add("AMENDED_PLANNING_ACCEPTED_BY_ID", typeof(int));
            dtSi.Columns.Add("AMENDED_PLANNING_ACCEPTED_BY", typeof(string));
            dtSi.Columns.Add("AMENDED_PLANNING_ACCEPTED_ON", typeof(string));
            dtSi.Columns.Add("AMENDED_PLANNING_ACCEPTED_REMARKS", typeof(string));

            dtSi.Columns.Add("AMENDED_FORWARDED_BY_ID", typeof(int));
            dtSi.Columns.Add("AMENDED_FORWARDED_BY", typeof(string));
            dtSi.Columns.Add("AMENDED_FORWARDED_ON", typeof(string));
            dtSi.Columns.Add("AMENDED_FORWARDED_REMARKS", typeof(string));

            dtSi.Columns.Add("AMENDED_ACCEPTED_BY_ID", typeof(int));
            dtSi.Columns.Add("AMENDED_ACCEPTED_BY", typeof(string));
            dtSi.Columns.Add("AMENDED_ACCEPTED_ON", typeof(string));
            dtSi.Columns.Add("AMENDED_ACCEPTED_REMARKS", typeof(string));

            dtSi.Columns.Add("AMENDED_QUALITY_ACCEPTED_BY_ID", typeof(int));
            dtSi.Columns.Add("AMENDED_QUALITY_ACCEPTED_BY", typeof(string));
            dtSi.Columns.Add("AMENDED_QUALITY_ACCEPTED_ON", typeof(string));
            dtSi.Columns.Add("AMENDED_QUALITY_ACCEPTED_REMARKS", typeof(string));



            dtSi.Columns.Add("COMPLETED_BY_ID", typeof(int));
            dtSi.Columns.Add("COMPLETED_BY", typeof(string));
            dtSi.Columns.Add("COMPLETED_ON", typeof(string));
            dtSi.Columns.Add("COMPLETED_REMARKS", typeof(string));

            #endregion


            #endregion

            returnVal = 0;
            LOTMainSubitemID = 0;
            newStatusID = 0;
            createdByID = 0;
            approvedByID = 0;
            PEID = 0;
            PMID = 0;
            prodMngrID = 0;
            currentStatusID = 0;
            //nextStatusID = 0;
            LOTTFSubitemID = 0;


            dsMailInfo = objProject.GetCancelledLOTMailInfo(LOTTFID);

            if (dsMailInfo.Tables.Count > 0)
            {
                PEID = Convert.ToInt32(dsMailInfo.Tables[0].Rows[0]["PE_ID"]);
                PMID = Convert.ToInt32(dsMailInfo.Tables[0].Rows[0]["PM_ID"]);
            }

            foreach (DataRow d2 in dsMailInfo.Tables[2].Rows)
            {
                currentStatusID = Convert.ToInt32(d2["STATUS_ID"]);

                createdByID = Convert.ToInt32(d2["CREATED_BY_ID"]);
                approvedByID = Convert.ToInt32(d2["APPROVED_BY_ID"]);
                amendmentCount = Convert.ToInt32(d2["AMENDMENT_COUNT"]);
                amendedByID = Convert.ToInt32(d2["AMENDED_BY_ID"]);
                amendedApprovedByID = Convert.ToInt32(d2["AMENDED_APPROVED_BY_ID"]);


                DataRow drs = dtSi.NewRow();

                #region DTSI Rows

                drs["RECORD_NO"] = recordNo;
                drs["LOT_TF_SUBITEM_ID"] = d2["LOT_TF_SUBITEM_ID"];
                drs["STATUS_ID"] = d2["STATUS_ID"];
                drs["PRODUCTION_ORDER_NO"] = d2["PRODUCTION_ORDER_NO"];
                drs["EXPECTED_COMPLETION_DATE"] = d2["EXPECTED_COMPLETION_DATE"];
                drs["SUBITEM_DESC"] = d2["SUBITEM_DESC"];
                drs["TAG_NO"] = d2["TAG_NO"];
                drs["LOT_MAIN_SUBITEM_ID"] = d2["LOT_MAIN_SUBITEM_ID"];
                drs["LOT_MAIN_ITEM"] = Convert.ToString(d2["LOT_MAIN_ITEM"] + " [" + d2["LOT_MAIN_SUBITEM"] + "]");
                drs["DRAWING_NO"] = d2["DRAWING_NO"];
                drs["REVISION_NO"] = d2["REVISION_NO"];
                drs["CATEGORY"] = d2["CATEGORY"];
                drs["QUANTITY"] = d2["QUANTITY"];
                drs["PRODUCTION_MNGR_ID"] = d2["PRODUCTION_MNGR_ID"];
                drs["PRODUCTION_MNGR_NAME"] = d2["PRODUCTION_MNGR_NAME"];
                drs["CREATED_BY_ID"] = d2["CREATED_BY_ID"];
                drs["CREATED_BY"] = d2["CREATED_BY"];
                drs["CREATED_ON"] = d2["CREATED_ON"];

                drs["APPROVED_BY_ID"] = d2["APPROVED_BY_ID"];
                drs["APPROVED_BY"] = d2["APPROVED_BY"];
                drs["APPROVED_ON"] = d2["APPROVED_ON"];
                drs["APPROVED_REMARKS"] = d2["APPROVED_REMARKS"];

                drs["PLANNING_ACCEPTED_BY_ID"] = d2["PLANNING_ACCEPTED_BY_ID"];
                drs["PLANNING_ACCEPTED_BY"] = d2["PLANNING_ACCEPTED_BY"];
                drs["PLANNING_ACCEPTED_ON"] = d2["PLANNING_ACCEPTED_ON"];
                drs["PLANNING_ACCEPTED_REMARKS"] = d2["PLANNING_ACCEPTED_REMARKS"];

                drs["FORWARDED_BY_ID"] = d2["FORWARDED_BY_ID"];
                drs["FORWARDED_BY"] = d2["FORWARDED_BY"];
                drs["FORWARDED_ON"] = d2["FORWARDED_ON"];
                drs["FORWARDED_REMARKS"] = d2["FORWARDED_REMARKS"];

                drs["ACCEPTED_BY_ID"] = d2["ACCEPTED_BY_ID"];
                drs["ACCEPTED_BY"] = d2["ACCEPTED_BY"];
                drs["ACCEPTED_ON"] = d2["ACCEPTED_ON"];
                drs["ACCEPTED_REMARKS"] = d2["ACCEPTED_REMARKS"];

                drs["QUALITY_ACCEPTED_BY_ID"] = d2["QUALITY_ACCEPTED_BY_ID"];
                drs["QUALITY_ACCEPTED_BY"] = d2["QUALITY_ACCEPTED_BY"];
                drs["QUALITY_ACCEPTED_ON"] = d2["QUALITY_ACCEPTED_ON"];
                drs["QUALITY_ACCEPTED_REMARKS"] = d2["QUALITY_ACCEPTED_REMARKS"];

                drs["SENT_TO_INTL_INSP_BY_ID"] = d2["SENT_TO_INTL_INSP_BY_ID"];
                drs["SENT_TO_INTL_INSP_BY"] = d2["SENT_TO_INTL_INSP_BY"];
                drs["SENT_TO_INTL_INSP_ON"] = d2["SENT_TO_INTL_INSP_ON"];
                drs["SENT_TO_INTL_INSP_REMARKS"] = d2["SENT_TO_INTL_INSP_REMARKS"];
                drs["QA_INTL_INSP_ACCEPTED_BY_ID"] = d2["QA_INTL_INSP_ACCEPTED_BY_ID"];
                drs["QA_INTL_INSP_ACCEPTED_BY"] = d2["QA_INTL_INSP_ACCEPTED_BY"];
                drs["QA_INTL_INSP_ACCEPTED_ON"] = d2["QA_INTL_INSP_ACCEPTED_ON"];
                drs["QA_INTL_INSP_ACCEPTED_REMARKS"] = d2["QA_INTL_INSP_ACCEPTED_REMARKS"];

                drs["QA_INTL_INSP_NOT_ACCEPTED_BY_ID"] = d2["QA_INTL_INSP_NOT_ACCEPTED_BY_ID"];
                drs["QA_INTL_INSP_NOT_ACCEPTED_BY"] = d2["QA_INTL_INSP_NOT_ACCEPTED_BY"];
                drs["QA_INTL_INSP_NOT_ACCEPTED_ON"] = d2["QA_INTL_INSP_NOT_ACCEPTED_ON"];
                drs["QA_INTL_INSP_NOT_ACCEPTED_REMARKS"] = d2["QA_INTL_INSP_NOT_ACCEPTED_REMARKS"];


                drs["SENT_TO_FINAL_INSP_BY_ID"] = d2["SENT_TO_FINAL_INSP_BY_ID"];
                drs["SENT_TO_FINAL_INSP_BY"] = d2["SENT_TO_FINAL_INSP_BY"];
                drs["SENT_TO_FINAL_INSP_ON"] = d2["SENT_TO_FINAL_INSP_ON"];
                drs["SENT_TO_FINAL_INSP_REMARKS"] = d2["SENT_TO_FINAL_INSP_REMARKS"];

                drs["SENT_TO_REWORK_BY_ID"] = d2["SENT_TO_REWORK_BY_ID"];
                drs["SENT_TO_REWORK_BY"] = d2["SENT_TO_REWORK_BY"];
                drs["SENT_TO_REWORK_ON"] = d2["SENT_TO_REWORK_ON"];
                drs["SENT_TO_REWORK_REMARKS"] = d2["SENT_TO_REWORK_REMARKS"];





                drs["AMENDMENT_COUNT"] = d2["AMENDMENT_COUNT"];
                drs["AMENDMENT_BY_ID"] = d2["AMENDMENT_BY_ID"];
                drs["AMENDMENT_BY"] = d2["AMENDMENT_BY"];
                drs["AMENDMENT_ON"] = d2["AMENDMENT_ON"];
                drs["AMENDMENT_REMARKS"] = d2["AMENDMENT_REMARKS"];

                drs["PROD_AMENDMENT_BY_ID"] = d2["PROD_AMENDMENT_BY_ID"];
                drs["PROD_AMENDMENT_BY"] = d2["PROD_AMENDMENT_BY"];
                drs["PROD_AMENDMENT_ON"] = d2["PROD_AMENDMENT_ON"];
                drs["PROD_AMENDMENT_REMARKS"] = d2["PROD_AMENDMENT_REMARKS"];

                drs["AMENDED_BY_ID"] = d2["AMENDED_BY_ID"];
                drs["AMENDED_BY"] = d2["AMENDED_BY"];
                drs["AMENDED_ON"] = d2["AMENDED_ON"];
                drs["AMENDED_REMARKS"] = d2["AMENDED_REMARKS"];

                drs["AMENDED_APPROVED_BY_ID"] = d2["AMENDED_APPROVED_BY_ID"];
                drs["AMENDED_APPROVED_BY"] = d2["AMENDED_APPROVED_BY"];
                drs["AMENDED_APPROVED_ON"] = d2["AMENDED_APPROVED_ON"];
                drs["AMENDED_APPROVED_REMARKS"] = d2["AMENDED_APPROVED_REMARKS"];

                drs["AMENDED_PLANNING_ACCEPTED_BY_ID"] = d2["AMENDED_PLANNING_ACCEPTED_BY_ID"];
                drs["AMENDED_PLANNING_ACCEPTED_BY"] = d2["AMENDED_PLANNING_ACCEPTED_BY"];
                drs["AMENDED_PLANNING_ACCEPTED_ON"] = d2["AMENDED_PLANNING_ACCEPTED_ON"];
                drs["AMENDED_PLANNING_ACCEPTED_REMARKS"] = d2["AMENDED_PLANNING_ACCEPTED_REMARKS"];

                drs["AMENDED_FORWARDED_BY_ID"] = d2["AMENDED_FORWARDED_BY_ID"];
                drs["AMENDED_FORWARDED_BY"] = d2["AMENDED_FORWARDED_BY"];
                drs["AMENDED_FORWARDED_ON"] = d2["AMENDED_FORWARDED_ON"];
                drs["AMENDED_FORWARDED_REMARKS"] = d2["AMENDED_FORWARDED_REMARKS"];

                drs["AMENDED_ACCEPTED_BY_ID"] = d2["AMENDED_ACCEPTED_BY_ID"];
                drs["AMENDED_ACCEPTED_BY"] = d2["AMENDED_ACCEPTED_BY"];
                drs["AMENDED_ACCEPTED_ON"] = d2["AMENDED_ACCEPTED_ON"];
                drs["AMENDED_ACCEPTED_REMARKS"] = d2["AMENDED_ACCEPTED_REMARKS"];

                drs["AMENDED_QUALITY_ACCEPTED_BY_ID"] = d2["AMENDED_QUALITY_ACCEPTED_BY_ID"];
                drs["AMENDED_QUALITY_ACCEPTED_BY"] = d2["AMENDED_QUALITY_ACCEPTED_BY"];
                drs["AMENDED_QUALITY_ACCEPTED_ON"] = d2["AMENDED_QUALITY_ACCEPTED_ON"];
                drs["AMENDED_QUALITY_ACCEPTED_REMARKS"] = d2["AMENDED_QUALITY_ACCEPTED_REMARKS"];

                drs["COMPLETED_BY_ID"] = d2["COMPLETED_BY_ID"];
                drs["COMPLETED_BY"] = d2["COMPLETED_BY"];
                drs["COMPLETED_ON"] = d2["COMPLETED_ON"];
                drs["COMPLETED_REMARKS"] = d2["COMPLETED_REMARKS"];

                #endregion

                dtSi.Rows.Add(drs);
            }


            if (dsMailInfo.Tables[8].Rows.Count > 0)
            {
                foreach (DataRow dr8 in dsMailInfo.Tables[8].Rows)
                {
                    bcc += Convert.ToString(dr8["EMAIL_ID"]) + ";";
                }

                if (!string.IsNullOrEmpty(bcc))
                    bcc = bcc.TrimEnd(';');
            }

            isPlannigMailSent = false;
            isQualityMailSent = false;

            if (dsMailInfo.Tables[9].Rows.Count > 0)
            {
                dtQuantityDetails = dsMailInfo.Tables[9];
            }
            else dtQuantityDetails = null;


            LOTMainSubitemID = Convert.ToInt32(dtSi.Rows[0]["LOT_MAIN_SUBITEM_ID"]);
            objLOTMailInfoProperties = GetMailInfo(dsMailInfo, LOTMainSubitemID, unitID);
            CallSendMailFunction(LOTTFID, TFNo, unitID, dsMailInfo.Tables[0], dtSi, objLOTMailInfoProperties, remarks, bcc, dtQuantityDetails);

            return returnVal;
        }

        catch (Exception ex)
        {
            returnVal = 0;
            return returnVal;
        }
    }




    private void CallSendMailFunction(int LOTTFID, string TFNo, int unitID, DataTable dtLOTDetails, DataTable dtSubitems,
                                        LOTMailInfoProperties objLOTMailInfoProperties, string remarks, string bcc, DataTable dtQuantityDetails)
    {
        try
        {

            #region Variables

            mailType = 0;
            currentStatusID = 0;

            cancelledByID = 0;
            cancelledByName = string.Empty;
            cancelledByEmail = string.Empty;

            createdByName = string.Empty;
            createdByEmail = string.Empty;

            amendedByID = 0;
            amendedByName = string.Empty;
            amendedByEmail = string.Empty;

            PEID = 0;
            PEName = string.Empty;
            PEEmail = string.Empty;
            PEUD = string.Empty;
            PEPD = string.Empty;

            PMID = 0;
            PMName = string.Empty;
            PMEmail = string.Empty;
            PMUD = string.Empty;
            PMPD = string.Empty;

            planningMngrID = 0;
            planningMngrIDs = string.Empty;
            planningMngrName = string.Empty;
            planningMngrEmail = string.Empty;
            planningMngrEmailCC = string.Empty;

            prodMngrID = 0;
            prodMngrName = string.Empty;
            prodMngrEmail = string.Empty;
            prodMngrEmailCC = string.Empty;

            approvedByID = 0;
            approvedByName = string.Empty;
            approvedByEmail = string.Empty;

            acceptedByID = 0;
            acceptedByName = string.Empty;
            acceptedByEmail = string.Empty;

            sendToIntlInspectionByID = 0;
            sendToIntlInspectionByName = string.Empty;
            sendToIntlInspectionByEmail = string.Empty;

            qAIntlInspectionAcceptedByID = 0;
            qAIntlInspectionAcceptedByName = string.Empty;
            qAIntlInspectionAcceptedByEmail = string.Empty;

            qAIntlInspectionNotAcceptedByID = 0;
            qAIntlInspectionNotAcceptedByName = string.Empty;
            qAIntlInspectionNotAcceptedByEmail = string.Empty;


            sentToFinalInspByID = 0;
            sentToFinalInspByName = string.Empty;
            sentToFinalInspByEmail = string.Empty;

            sentToReworkByID = 0;
            sentToReworkByName = string.Empty;
            sentToReworkByEmail = string.Empty;

            amendmentCount = 0;

            amendmentByID = 0;
            amendmentByName = string.Empty;
            amendmentByEmail = string.Empty;

            prodAmendmentByID = 0;
            prodAmendmentByName = string.Empty;
            prodAmendmentByEmail = string.Empty;

            amendedApprovedByID = 0;
            amendedApprovedByName = string.Empty;
            amendedApprovedByEmail = string.Empty;

            amendedAcceptedByID = 0;
            amendedAcceptedByName = string.Empty;
            amendedAcceptedByEmail = string.Empty;

            completedByID = 0;
            completedByName = string.Empty;
            completedByEmail = string.Empty;


            qualityAcceptedByID = 0;
            qualityAcceptedByName = string.Empty;
            qualityAcceptedByEmail = string.Empty;

            planningAcceptedByID = 0;
            planningAcceptedByName = string.Empty;
            planningAcceptedByEmail = string.Empty;

            qualityAmendedAcceptedByID = 0;
            qualityAmendedAcceptedByName = string.Empty;
            qualityAmendedAcceptedByEmail = string.Empty;

            planningAmendedAcceptedByID = 0;
            planningAmendedAcceptedByName = string.Empty;
            planningAmendedAcceptedByEmail = string.Empty;


            urlTxt = string.Empty;
            href = string.Empty;
            link = string.Empty;

            PEApproveHref = string.Empty;
            PEApproveLink = string.Empty;

            PMApproveHref = string.Empty;
            PMApproveLink = string.Empty;

            fileName = string.Empty;
            body = string.Empty;
            subject = string.Empty;
            from = string.Empty;
            fromName = string.Empty;
            to = string.Empty;
            toName = string.Empty;
            cc = string.Empty;

            #endregion


            if (objLOTMailInfoProperties != null)
            {
                if (Convert.ToInt32(objLOTMailInfoProperties.statusID) > 0)
                    currentStatusID = Convert.ToInt32(objLOTMailInfoProperties.statusID);


                if (Convert.ToInt32(objLOTMailInfoProperties.CreatedByID) > 0)
                    createdByID = Convert.ToInt32(objLOTMailInfoProperties.CreatedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.CreatedByName)))
                    createdByName = Convert.ToString(objLOTMailInfoProperties.CreatedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.CreatedByEmail)))
                    createdByEmail = Convert.ToString(objLOTMailInfoProperties.CreatedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.ApprovedByID) > 0)
                    approvedByID = Convert.ToInt32(objLOTMailInfoProperties.ApprovedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ApprovedByName)))
                    approvedByName = Convert.ToString(objLOTMailInfoProperties.ApprovedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ApprovedByEmail)))
                    approvedByEmail = Convert.ToString(objLOTMailInfoProperties.ApprovedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.PlanningAcceptedByID) > 0)
                    planningAcceptedByID = Convert.ToInt32(objLOTMailInfoProperties.PlanningAcceptedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PlanningAcceptedByName)))
                    planningAcceptedByName = Convert.ToString(objLOTMailInfoProperties.PlanningAcceptedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PlanningAcceptedByEmail)))
                    planningAcceptedByEmail = Convert.ToString(objLOTMailInfoProperties.PlanningAcceptedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.ForwardedByID) > 0)
                    forwardedByID = Convert.ToInt32(objLOTMailInfoProperties.ForwardedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ForwardedByName)))
                    forwardedByName = Convert.ToString(objLOTMailInfoProperties.ForwardedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ForwardedByEmail)))
                    forwardedByEmail = Convert.ToString(objLOTMailInfoProperties.ForwardedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.AcceptedByID) > 0)
                    acceptedByID = Convert.ToInt32(objLOTMailInfoProperties.AcceptedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AcceptedByName)))
                    acceptedByName = Convert.ToString(objLOTMailInfoProperties.AcceptedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AcceptedByEmail)))
                    acceptedByEmail = Convert.ToString(objLOTMailInfoProperties.AcceptedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.QualityAcceptedByID) > 0)
                    qualityAcceptedByID = Convert.ToInt32(objLOTMailInfoProperties.QualityAcceptedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QualityAcceptedByName)))
                    qualityAcceptedByName = Convert.ToString(objLOTMailInfoProperties.QualityAcceptedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QualityAcceptedByEmail)))
                    qualityAcceptedByEmail = Convert.ToString(objLOTMailInfoProperties.QualityAcceptedByEmail);



                if (Convert.ToInt32(objLOTMailInfoProperties.AmendmentCount) > 0)
                    amendmentCount = Convert.ToInt32(objLOTMailInfoProperties.AmendmentCount);

                if (Convert.ToInt32(objLOTMailInfoProperties.AmendmentByID) > 0)
                    amendmentByID = Convert.ToInt32(objLOTMailInfoProperties.AmendmentByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AmendmentByName)))
                    amendmentByName = Convert.ToString(objLOTMailInfoProperties.AmendmentByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AmendmentByEmail)))
                    amendmentByEmail = Convert.ToString(objLOTMailInfoProperties.AmendmentByEmail);



                if (Convert.ToInt32(objLOTMailInfoProperties.ProdAmendmentByID) > 0)
                    prodAmendmentByID = Convert.ToInt32(objLOTMailInfoProperties.ProdAmendmentByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ProdAmendmentByName)))
                    prodAmendmentByName = Convert.ToString(objLOTMailInfoProperties.ProdAmendmentByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ProdAmendmentByEmail)))
                    prodAmendmentByEmail = Convert.ToString(objLOTMailInfoProperties.ProdAmendmentByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.AmendedByID) > 0)
                    amendedByID = Convert.ToInt32(objLOTMailInfoProperties.AmendedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AmendedByName)))
                    amendedByName = Convert.ToString(objLOTMailInfoProperties.AmendedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AmendedByEmail)))
                    amendedByEmail = Convert.ToString(objLOTMailInfoProperties.AmendedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.AmendedApprovedByID) > 0)
                    amendedApprovedByID = Convert.ToInt32(objLOTMailInfoProperties.AmendedApprovedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AmendedApprovedByName)))
                    amendedApprovedByName = Convert.ToString(objLOTMailInfoProperties.AmendedApprovedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AmendedApprovedByEmail)))
                    amendedApprovedByEmail = Convert.ToString(objLOTMailInfoProperties.AmendedApprovedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.PlanningAmendedAcceptedByID) > 0)
                    planningAmendedAcceptedByID = Convert.ToInt32(objLOTMailInfoProperties.PlanningAmendedAcceptedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PlanningAmendedAcceptedByName)))
                    planningAmendedAcceptedByName = Convert.ToString(objLOTMailInfoProperties.PlanningAmendedAcceptedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PlanningAmendedAcceptedByEmail)))
                    planningAmendedAcceptedByEmail = Convert.ToString(objLOTMailInfoProperties.PlanningAmendedAcceptedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.ForwardedAmendedByID) > 0)
                    forwardedAmendedByID = Convert.ToInt32(objLOTMailInfoProperties.ForwardedAmendedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ForwardedAmendedByName)))
                    forwardedAmendedByName = Convert.ToString(objLOTMailInfoProperties.ForwardedAmendedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ForwardedAmendedByEmail)))
                    forwardedAmendedByEmail = Convert.ToString(objLOTMailInfoProperties.ForwardedAmendedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.AmendedAcceptedByID) > 0)
                    amendedAcceptedByID = Convert.ToInt32(objLOTMailInfoProperties.AmendedAcceptedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AmendedAcceptedByName)))
                    amendedAcceptedByName = Convert.ToString(objLOTMailInfoProperties.AmendedAcceptedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.AmendedAcceptedByEmail)))
                    amendedAcceptedByEmail = Convert.ToString(objLOTMailInfoProperties.AmendedAcceptedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.QualityAmendedAcceptedByID) > 0)
                    qualityAmendedAcceptedByID = Convert.ToInt32(objLOTMailInfoProperties.QualityAmendedAcceptedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QualityAmendedAcceptedByName)))
                    qualityAmendedAcceptedByName = Convert.ToString(objLOTMailInfoProperties.QualityAmendedAcceptedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QualityAmendedAcceptedByEmail)))
                    qualityAmendedAcceptedByEmail = Convert.ToString(objLOTMailInfoProperties.QualityAmendedAcceptedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.SendToIntlInspectionByID) > 0)
                    sendToIntlInspectionByID = Convert.ToInt32(objLOTMailInfoProperties.SendToIntlInspectionByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.SendToIntlInspectionByName)))
                    sendToIntlInspectionByName = Convert.ToString(objLOTMailInfoProperties.SendToIntlInspectionByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.SendToIntlInspectionByEmail)))
                    sendToIntlInspectionByEmail = Convert.ToString(objLOTMailInfoProperties.SendToIntlInspectionByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.QAIntlInspectionAcceptedByID) > 0)
                    qAIntlInspectionAcceptedByID = Convert.ToInt32(objLOTMailInfoProperties.QAIntlInspectionAcceptedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QAIntlInspectionAcceptedByName)))
                    qAIntlInspectionAcceptedByName = Convert.ToString(objLOTMailInfoProperties.QAIntlInspectionAcceptedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QAIntlInspectionAcceptedByEmail)))
                    qAIntlInspectionAcceptedByEmail = Convert.ToString(objLOTMailInfoProperties.QAIntlInspectionAcceptedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByID) > 0)
                    qAIntlInspectionNotAcceptedByID = Convert.ToInt32(objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByName)))
                    qAIntlInspectionNotAcceptedByName = Convert.ToString(objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByEmail)))
                    qAIntlInspectionNotAcceptedByEmail = Convert.ToString(objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByEmail);



                if (Convert.ToInt32(objLOTMailInfoProperties.SentToFinalInspByID) > 0)
                    sentToFinalInspByID = Convert.ToInt32(objLOTMailInfoProperties.SentToFinalInspByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.SentToFinalInspByName)))
                    sentToFinalInspByName = Convert.ToString(objLOTMailInfoProperties.SentToFinalInspByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.SentToFinalInspByEmail)))
                    sentToFinalInspByEmail = Convert.ToString(objLOTMailInfoProperties.SentToFinalInspByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.SentToReworkByID) > 0)
                    sentToReworkByID = Convert.ToInt32(objLOTMailInfoProperties.SentToReworkByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.SentToReworkByName)))
                    sentToReworkByName = Convert.ToString(objLOTMailInfoProperties.SentToReworkByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.SentToReworkByEmail)))
                    sentToReworkByEmail = Convert.ToString(objLOTMailInfoProperties.SentToReworkByEmail);




                if (Convert.ToInt32(objLOTMailInfoProperties.CompletedByID) > 0)
                    completedByID = Convert.ToInt32(objLOTMailInfoProperties.CompletedByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.CompletedByName)))
                    completedByName = Convert.ToString(objLOTMailInfoProperties.CompletedByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.CompletedByEmail)))
                    completedByEmail = Convert.ToString(objLOTMailInfoProperties.CompletedByEmail);


                if (Convert.ToInt32(objLOTMailInfoProperties.PEID) > 0)
                    PEID = Convert.ToInt32(objLOTMailInfoProperties.PEID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PEName)))
                    PEName = Convert.ToString(objLOTMailInfoProperties.PEName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PEEmail)))
                    PEEmail = Convert.ToString(objLOTMailInfoProperties.PEEmail);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PEUD)))
                    PEUD = Convert.ToString(objLOTMailInfoProperties.PEUD);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PEPD)))
                    PEPD = Convert.ToString(objLOTMailInfoProperties.PEPD);


                if (Convert.ToInt32(objLOTMailInfoProperties.PMID) > 0)
                    PMID = Convert.ToInt32(objLOTMailInfoProperties.PMID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PMName)))
                    PMName = Convert.ToString(objLOTMailInfoProperties.PMName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PMEmail)))
                    PMEmail = Convert.ToString(objLOTMailInfoProperties.PMEmail);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PMUD)))
                    PMUD = Convert.ToString(objLOTMailInfoProperties.PMUD);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PMPD)))
                    PMPD = Convert.ToString(objLOTMailInfoProperties.PMPD);


                if (Convert.ToInt32(objLOTMailInfoProperties.ProdMngrID) > 0)
                    prodMngrID = Convert.ToInt32(objLOTMailInfoProperties.ProdMngrID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ProdMngrName)))
                    prodMngrName = Convert.ToString(objLOTMailInfoProperties.ProdMngrName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ProdMngrEmail)))
                    prodMngrEmail = Convert.ToString(objLOTMailInfoProperties.ProdMngrEmail);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.ProdMngrEmailCC)))
                    prodMngrEmailCC = Convert.ToString(objLOTMailInfoProperties.ProdMngrEmailCC);


                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QualityMngrNames)))
                    qualityNames = Convert.ToString(objLOTMailInfoProperties.QualityMngrNames);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.QualityMngrEmails)))
                    qualityEmails = Convert.ToString(objLOTMailInfoProperties.QualityMngrEmails);



                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.planningMngrIDs)))
                    planningMngrIDs = Convert.ToString(objLOTMailInfoProperties.planningMngrIDs);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PlanningMngrNames)))
                    planningMngrName = Convert.ToString(objLOTMailInfoProperties.PlanningMngrNames);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PlanningMngrEmails)))
                    planningMngrEmail = Convert.ToString(objLOTMailInfoProperties.PlanningMngrEmails);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.PlanningMngrEmailsCC)))
                    planningMngrEmailCC = Convert.ToString(objLOTMailInfoProperties.PlanningMngrEmailsCC);


                if (Convert.ToInt32(objLOTMailInfoProperties.CancelledByID) > 0)
                    cancelledByID = Convert.ToInt32(objLOTMailInfoProperties.CancelledByID);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.CancelledByName)))
                    cancelledByName = Convert.ToString(objLOTMailInfoProperties.CancelledByName);

                if (!string.IsNullOrEmpty(Convert.ToString(objLOTMailInfoProperties.CancelledByEmail)))
                    cancelledByEmail = Convert.ToString(objLOTMailInfoProperties.CancelledByEmail);

            }

            int mailValue = 0;
            mailType = Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.CancellationMail);

            fileName = string.Empty;
            body = string.Empty;
            subject = string.Empty;
            from = string.Empty;
            fromName = string.Empty;
            to = string.Empty;
            toName = string.Empty;
            cc = string.Empty;

            urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
            link = "'" + urlTxt + "/Login.aspx?tfno=" + TFNo + "'&unitid=" + unitID + "'";


            fileName = "~/PROJECT/LOT/EMAIL_FORMATS/19LOTCancellationMail.htm";
            subject = "Cancelled TF No.: " + TFNo;


            int amendmentByProdFlag = 0;

            foreach (DataRow dra in dtSubitems.Rows)
            {
                if (Convert.ToInt32(dra["PROD_AMENDMENT_BY_ID"]) > 0)
                {
                    amendmentByProdFlag = 1;
                    break;
                }
            }

            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
            {
                if (PEID == PMID)
                {
                    // To PM--------------------------------------
                    from = cancelledByEmail;
                    fromName = cancelledByName;
                    to = createdByEmail;
                    toName = createdByName;
                    cc = PEEmail + ";" + PMEmail;
                    bcc = cancelledByEmail;

                    mailValue = SendMail(TFNo, from, fromName, to, toName, cc, bcc, subject, fileName, dtLOTDetails, dtSubitems, remarks, dtQuantityDetails);
                    //--------------------------------------------
                }
                else
                {
                    // To PE------------------------------------
                    from = cancelledByEmail;
                    fromName = cancelledByName;
                    to = PEEmail;
                    toName = createdByName;
                    cc = PEEmail + ";" + PMEmail;
                    bcc = cancelledByEmail;

                    mailValue = SendMail(TFNo, from, fromName, to, toName, cc, bcc, subject, fileName, dtLOTDetails, dtSubitems, remarks, dtQuantityDetails);
                    //--------------------------------------------




                    // To PM--------------------------------------
                    from = cancelledByEmail;
                    fromName = cancelledByName;
                    to = PMEmail;
                    toName = createdByName;
                    cc = PEEmail + ";" + PMEmail;
                    bcc = cancelledByEmail;

                    mailValue = SendMail(TFNo, from, fromName, to, toName, cc, bcc, subject, fileName, dtLOTDetails, dtSubitems, remarks, dtQuantityDetails);
                    //---------------------------------------------

                }
            }



            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
            {
                from = cancelledByEmail;
                fromName = cancelledByName;
                to = createdByEmail;
                toName = createdByName;
                bcc = cancelledByEmail;

                if (!string.IsNullOrEmpty(planningMngrEmail))
                {
                    string[] strPlanningEmails = planningMngrEmail.Split(';');
                    string[] strPlanningNames = planningMngrName.Split(';');

                    for (int i = 0; i < strPlanningEmails.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(strPlanningEmails[i])))
                            cc += strPlanningEmails[i] + ";";
                    }
                }


                if (!string.IsNullOrEmpty(planningMngrEmailCC))
                {
                    string[] strPlanningEmailsCC = planningMngrEmailCC.Split(';');

                    for (int j = 0; j < strPlanningEmailsCC.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(strPlanningEmailsCC[j])))
                            cc += strPlanningEmailsCC[j] + ";";
                    }
                }


                if (amendmentByProdFlag == 0)
                {
                    if (!string.IsNullOrEmpty(cc))
                        cc += PEEmail + ";" + PMEmail;
                }
                else
                {
                    if (!string.IsNullOrEmpty(cc))
                        cc += PEEmail + ";" + PMEmail + ";" + prodMngrEmail;
                }

                if (!string.IsNullOrEmpty(cc))
                    cc = cc.TrimEnd(';');

                mailValue = SendMail(TFNo, from, fromName, to, toName, cc, bcc, subject, fileName, dtLOTDetails, dtSubitems, remarks, dtQuantityDetails);

            }



            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
            {
                from = cancelledByEmail;
                fromName = cancelledByName;
                to = createdByEmail;
                toName = createdByName;
                bcc = cancelledByEmail;

                if (!string.IsNullOrEmpty(planningMngrEmail))
                {
                    string[] strPlanningEmails = planningMngrEmail.Split(';');
                    string[] strPlanningNames = planningMngrName.Split(';');

                    for (int i = 0; i < strPlanningEmails.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(strPlanningEmails[i])))
                            cc += strPlanningEmails[i] + ";";
                    }
                }


                if (!string.IsNullOrEmpty(planningMngrEmailCC))
                {
                    string[] strPlanningEmailsCC = planningMngrEmailCC.Split(';');

                    for (int j = 0; j < strPlanningEmailsCC.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(strPlanningEmailsCC[j])))
                            cc += strPlanningEmailsCC[j] + ";";
                    }
                }

                if (!string.IsNullOrEmpty(to))
                    to = to.TrimEnd(';');

                if (!string.IsNullOrEmpty(cc))
                    cc += PEEmail + ";" + PMEmail;

                if (!string.IsNullOrEmpty(cc))
                    cc = cc.TrimEnd(';');

                mailValue = SendMail(TFNo, from, fromName, to, toName, cc, bcc, subject, fileName, dtLOTDetails, dtSubitems, remarks, dtQuantityDetails);
            }




            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted) ||
                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
            {
                from = cancelledByEmail;
                fromName = cancelledByName;
                to = createdByEmail;
                toName = createdByName;
                bcc = cancelledByEmail;



                if (!string.IsNullOrEmpty(planningMngrEmail))
                {
                    string[] strPlanningEmails = planningMngrEmail.Split(';');
                    string[] strPlanningNames = planningMngrName.Split(';');

                    for (int i = 0; i < strPlanningEmails.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(strPlanningEmails[i])))
                            cc += strPlanningEmails[i] + ";";
                    }
                }


                if (!string.IsNullOrEmpty(planningMngrEmailCC))
                {
                    string[] strPlanningEmailsCC = planningMngrEmailCC.Split(';');

                    for (int j = 0; j < strPlanningEmailsCC.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(strPlanningEmailsCC[j])))
                            cc += strPlanningEmailsCC[j] + ";";
                    }
                }






                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                    currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                {
                    if (!string.IsNullOrEmpty(cc))
                        cc += PEEmail + ";" + PMEmail;
                }
                else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                         currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                {
                    if (!string.IsNullOrEmpty(cc))
                        cc += PEEmail + ";" + PMEmail + ";" + prodMngrEmail;
                }

                if (!string.IsNullOrEmpty(cc))
                    cc = cc.TrimEnd(';');

                mailValue = SendMail(TFNo, from, fromName, to, toName, cc, bcc, subject, fileName, dtLOTDetails, dtSubitems, remarks, dtQuantityDetails);

            }
        }
        catch (Exception)
        {
            //
        }
    }




    private int SendMail(string TFNo, string from, string fromName, string to, string toName, string cc, string bcc, string subject, string fileName,
                         DataTable dtLOTDetails, DataTable dtSubitem, string remarks, DataTable dtQuantityDetails)
    {

        returnVal = 0;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        if (mailType != Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.NotApplicable) && !string.IsNullOrEmpty(to))
        {
            if (!string.IsNullOrEmpty(subject))
                mail.Subject = subject;

            if (!string.IsNullOrEmpty(from))
                mail.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(to))
            {
                to = to.TrimEnd(';');
                string items = string.Empty;
                string[] strTo = to.Split(';');
                foreach (string item in strTo)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (!items.Contains(item))
                        {
                            items += item + ";";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(items))
                    items = items.TrimEnd(';');

                string[] strToNew = items.Split(';');

                foreach (string item in strToNew)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.To.Add(item);
                    }
                }
            }

            if (!string.IsNullOrEmpty(cc))
            {
                cc = cc.TrimEnd(';');
                string items = string.Empty;
                string[] strCC = cc.Split(';');

                foreach (string item in strCC)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (!items.Contains(item))
                        {
                            items += item + ";";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(items))
                    items = items.TrimEnd(';');

                string[] strCCNew = items.Split(';');

                foreach (string item in strCCNew)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.CC.Add(item);
                    }
                }
            }

            if (!string.IsNullOrEmpty(bcc))
            {
                bcc = bcc.TrimEnd(';');
                string items = string.Empty;
                string[] strBCC = bcc.Split(';');
                foreach (string item in strBCC)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (!items.Contains(item))
                        {
                            items += item + ";";
                        }
                    }
                }


                if (!string.IsNullOrEmpty(items))
                    items = items.TrimEnd(';');

                string[] strBCCNew = items.Split(';');
                foreach (string item in strBCCNew)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.Bcc.Add(item);
                    }
                }
            }

            mail.IsBodyHtml = true;

            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }


            body = body.Replace("{#toname#}", toName);
            body = body.Replace("{#cancellationremarks#}", remarks);
            body = body.Replace("{#fromname#}", fromName);


            mail.Body = body;

            byte[] bytes = GetPDFBytes(dtLOTDetails, dtSubitem, Convert.ToInt32(LOTAllStatusAndTypes.EnumPDFType.Mail), dtQuantityDetails);
            mail.Attachments.Add(new Attachment(new MemoryStream(bytes), TFNo + ".pdf"));

            try
            {
                if (!string.IsNullOrEmpty(to))
                {
                    SmtpServer.Send(mail);
                    returnVal = 1;
                }
                else
                    returnVal = 0;
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    returnVal = 1;

                else
                    returnVal = 0;
            }
        }
        else
            returnVal = 0;

        return returnVal;

    }

    public byte[] GetPDFBytes(DataTable dtLOT, DataTable dtSubitem, int PDFType, DataTable dtQuantityDetails)
    {
        try
        {
            byte[] pdfBytes;
            //DataSet ds = new DataSet();
            //DataTable dtSubitems = new DataTable();

            //if (Session["dsJobNo"] != null)
            //    ds = (DataSet)Session["dsJobNo"];
            //else
            //    ds = GetJOBData();

            //if (Session["dtSubitem"] != null)
            //    dtSubitems = (DataTable)Session["dtSubitem"];

            //var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));
            var cssText = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Styles/LOT.css"));


            string htmltxt = objLOTHtmlForPDFApproval.GetHtmlForPDF(dtLOT, dtSubitem, dtQuantityDetails, null, PDFType);

            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(htmltxt))
            {
                sb.Append("<html>\n");
                sb.Append("<body>\n");

                sb.Append(htmltxt + "\n");

                sb.Append("</body>\n");
                sb.Append("</html>\n");
            }

            var html = sb.ToString();

            //string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";            
            string imagePath = System.Web.Hosting.HostingEnvironment.MapPath("\\Images\\COPERION") + "\\logo2.png";

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
            img.Alignment = Element.ALIGN_LEFT;
            img.ScaleToFit(180f, 250f);


            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4);
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                document.Add(img);
                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                {
                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                    }
                }

                document.Close();
                pdfBytes = memoryStream.GetBuffer();

                return pdfBytes;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public LOTMailInfoProperties GetMailInfo(DataSet dsMailInfo, int LOTMainSubitemID, int unitID)
    {
        try
        {

            string prodManagerCC = string.Empty;

            string qualityManagerNames = string.Empty;
            string qualityManagerEmails = string.Empty;

            string planningManagerIDs = string.Empty;
            string planningManagerNames = string.Empty;
            string planningManagerEmails = string.Empty;
            string planningManagerEmailsCC = string.Empty;

            if (dsMailInfo.Tables[0].Rows.Count > 0)
            {
                //if (dsMailInfo.Tables[0].Rows[0]["CREATED_BY_ID"] != DBNull.Value)
                //    objLOTMailInfoProperties.CreatedByID = Convert.ToInt32(dsMailInfo.Tables[0].Rows[0]["CREATED_BY_ID"]);

                //if (dsMailInfo.Tables[0].Rows[0]["CREATED_BY"] != DBNull.Value)
                //    objLOTMailInfoProperties.CreatedByName = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["CREATED_BY"]);

                //if (dsMailInfo.Tables[0].Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value)
                //    objLOTMailInfoProperties.CreatedByEmail = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["CREATED_BY_EMAIL"]);

                if (dsMailInfo.Tables[0].Rows[0]["PE_ID"] != DBNull.Value)
                    objLOTMailInfoProperties.PEID = Convert.ToInt32(dsMailInfo.Tables[0].Rows[0]["PE_ID"]);

                if (dsMailInfo.Tables[0].Rows[0]["PE_NAME"] != DBNull.Value)
                    objLOTMailInfoProperties.PEName = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PE_NAME"]);

                if (dsMailInfo.Tables[0].Rows[0]["PE_EMAIL"] != DBNull.Value)
                    objLOTMailInfoProperties.PEEmail = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PE_EMAIL"]);

                if (dsMailInfo.Tables[0].Rows[0]["PE_UD"] != DBNull.Value)
                    objLOTMailInfoProperties.PEUD = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PE_UD"]);

                if (dsMailInfo.Tables[0].Rows[0]["PE_PD"] != DBNull.Value)
                    objLOTMailInfoProperties.PEPD = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PE_PD"]);


                if (dsMailInfo.Tables[0].Rows[0]["PM_ID"] != DBNull.Value)
                    objLOTMailInfoProperties.PMID = Convert.ToInt32(dsMailInfo.Tables[0].Rows[0]["PM_ID"]);

                if (dsMailInfo.Tables[0].Rows[0]["PM_NAME"] != DBNull.Value)
                    objLOTMailInfoProperties.PMName = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PM_NAME"]);

                if (dsMailInfo.Tables[0].Rows[0]["PM_EMAIL"] != DBNull.Value)
                    objLOTMailInfoProperties.PMEmail = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PM_EMAIL"]);

                if (dsMailInfo.Tables[0].Rows[0]["PM_UD"] != DBNull.Value)
                    objLOTMailInfoProperties.PMUD = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PM_UD"]);

                if (dsMailInfo.Tables[0].Rows[0]["PM_PD"] != DBNull.Value)
                    objLOTMailInfoProperties.PMPD = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PM_PD"]);




                if (dsMailInfo.Tables[0].Rows[0]["CANCELLED_BY_ID"] != DBNull.Value)
                    objLOTMailInfoProperties.CancelledByID = Convert.ToInt32(dsMailInfo.Tables[0].Rows[0]["CANCELLED_BY_ID"]);

                if (dsMailInfo.Tables[0].Rows[0]["CANCELLED_BY"] != DBNull.Value)
                    objLOTMailInfoProperties.CancelledByName = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["CANCELLED_BY"]);

                if (dsMailInfo.Tables[0].Rows[0]["CANCELLED_BY_EMAIL"] != DBNull.Value)
                    objLOTMailInfoProperties.CancelledByEmail = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["CANCELLED_BY_EMAIL"]);


            }


            if (dsMailInfo.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dr in dsMailInfo.Tables[2].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubitemID + "'"))//AND RECORD_NO='" + recordNo + "
                {

                    //int partialStatusID, int partialQuantityFlag

                    //foreach (DataRow dr10 in dsMailInfo.Tables[10].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]) + ""))
                    //{
                    //    count++;

                    //}

                    //if (count == 0)
                    //{
                    if (dr["STATUS_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.statusID = Convert.ToInt32(dr["STATUS_ID"]);
                    //}



                    if (dr["CREATED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.CreatedByID = Convert.ToInt32(dr["CREATED_BY_ID"]);

                    if (dr["CREATED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.CreatedByName = Convert.ToString(dr["CREATED_BY"]);

                    if (dr["CREATED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.CreatedByEmail = Convert.ToString(dr["CREATED_BY_EMAIL"]);


                    if (dr["APPROVED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.ApprovedByID = Convert.ToInt32(dr["APPROVED_BY_ID"]);

                    if (dr["APPROVED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.ApprovedByName = Convert.ToString(dr["APPROVED_BY"]);

                    if (dr["APPROVED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.ApprovedByEmail = Convert.ToString(dr["APPROVED_BY_EMAIL"]);


                    if (dr["PLANNING_ACCEPTED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.PlanningAcceptedByID = Convert.ToInt32(dr["PLANNING_ACCEPTED_BY_ID"]);

                    if (dr["PLANNING_ACCEPTED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.PlanningAcceptedByName = Convert.ToString(dr["PLANNING_ACCEPTED_BY"]);

                    if (dr["PLANNING_ACCEPTED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.PlanningAcceptedByEmail = Convert.ToString(dr["PLANNING_ACCEPTED_BY_EMAIL"]);


                    if (dr["FORWARDED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.ForwardedByID = Convert.ToInt32(dr["FORWARDED_BY_ID"]);

                    if (dr["FORWARDED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.ForwardedByName = Convert.ToString(dr["FORWARDED_BY"]);

                    if (dr["FORWARDED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.ForwardedByEmail = Convert.ToString(dr["FORWARDED_BY_EMAIL"]);



                    if (dr["ACCEPTED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.AcceptedByID = Convert.ToInt32(dr["ACCEPTED_BY_ID"]);

                    if (dr["ACCEPTED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.AcceptedByName = Convert.ToString(dr["ACCEPTED_BY"]);

                    if (dr["ACCEPTED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.AcceptedByEmail = Convert.ToString(dr["ACCEPTED_BY_EMAIL"]);


                    if (dr["QUALITY_ACCEPTED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.QualityAcceptedByID = Convert.ToInt32(dr["QUALITY_ACCEPTED_BY_ID"]);

                    if (dr["QUALITY_ACCEPTED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.QualityAcceptedByName = Convert.ToString(dr["QUALITY_ACCEPTED_BY"]);

                    if (dr["QUALITY_ACCEPTED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.QualityAcceptedByEmail = Convert.ToString(dr["QUALITY_ACCEPTED_BY_EMAIL"]);







                    if (dr["SENT_TO_INTL_INSP_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.SendToIntlInspectionByID = Convert.ToInt32(dr["SENT_TO_INTL_INSP_BY_ID"]);

                    if (dr["SENT_TO_INTL_INSP_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.SendToIntlInspectionByName = Convert.ToString(dr["SENT_TO_INTL_INSP_BY"]);

                    if (dr["SENT_TO_INTL_INSP_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.SendToIntlInspectionByEmail = Convert.ToString(dr["SENT_TO_INTL_INSP_BY_EMAIL"]);



                    if (dr["QA_INTL_INSP_ACCEPTED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.QAIntlInspectionAcceptedByID = Convert.ToInt32(dr["QA_INTL_INSP_ACCEPTED_BY_ID"]);

                    if (dr["QA_INTL_INSP_ACCEPTED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.QAIntlInspectionAcceptedByName = Convert.ToString(dr["QA_INTL_INSP_ACCEPTED_BY"]);

                    if (dr["QA_INTL_INSP_ACCEPTED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.QAIntlInspectionAcceptedByEmail = Convert.ToString(dr["QA_INTL_INSP_ACCEPTED_BY_EMAIL"]);



                    if (dr["QA_INTL_INSP_NOT_ACCEPTED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByID = Convert.ToInt32(dr["QA_INTL_INSP_NOT_ACCEPTED_BY_ID"]);

                    if (dr["QA_INTL_INSP_NOT_ACCEPTED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByName = Convert.ToString(dr["QA_INTL_INSP_NOT_ACCEPTED_BY"]);

                    if (dr["QA_INTL_INSP_NOT_ACCEPTED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.QAIntlInspectionNotAcceptedByEmail = Convert.ToString(dr["QA_INTL_INSP_NOT_ACCEPTED_BY_EMAIL"]);





                    if (dr["SENT_TO_FINAL_INSP_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.SentToFinalInspByID = Convert.ToInt32(dr["SENT_TO_FINAL_INSP_BY_ID"]);

                    if (dr["SENT_TO_FINAL_INSP_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.SentToFinalInspByName = Convert.ToString(dr["SENT_TO_FINAL_INSP_BY"]);

                    if (dr["SENT_TO_FINAL_INSP_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.SentToFinalInspByEmail = Convert.ToString(dr["SENT_TO_FINAL_INSP_BY_EMAIL"]);



                    if (dr["SENT_TO_REWORK_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.SentToReworkByID = Convert.ToInt32(dr["SENT_TO_REWORK_BY_ID"]);

                    if (dr["SENT_TO_REWORK_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.SentToReworkByName = Convert.ToString(dr["SENT_TO_REWORK_BY"]);

                    if (dr["SENT_TO_REWORK_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.SentToReworkByEmail = Convert.ToString(dr["SENT_TO_REWORK_BY_EMAIL"]);








                    if (dr["AMENDMENT_COUNT"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendmentCount = Convert.ToInt32(dr["AMENDMENT_COUNT"]);


                    if (dr["AMENDMENT_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendmentByID = Convert.ToInt32(dr["AMENDMENT_BY_ID"]);

                    if (dr["AMENDMENT_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendmentByName = Convert.ToString(dr["AMENDMENT_BY"]);

                    if (dr["AMENDMENT_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendmentByEmail = Convert.ToString(dr["AMENDMENT_BY_EMAIL"]);


                    if (dr["PROD_AMENDMENT_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.ProdAmendmentByID = Convert.ToInt32(dr["PROD_AMENDMENT_BY_ID"]);

                    if (dr["PROD_AMENDMENT_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.ProdAmendmentByName = Convert.ToString(dr["PROD_AMENDMENT_BY"]);

                    if (dr["PROD_AMENDMENT_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.ProdAmendmentByEmail = Convert.ToString(dr["PROD_AMENDMENT_BY_EMAIL"]);



                    if (dr["AMENDED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedByID = Convert.ToInt32(dr["AMENDED_BY_ID"]);

                    if (dr["AMENDED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedByName = Convert.ToString(dr["AMENDED_BY"]);

                    if (dr["AMENDED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedByEmail = Convert.ToString(dr["AMENDED_BY_EMAIL"]);


                    if (dr["AMENDED_APPROVED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedApprovedByID = Convert.ToInt32(dr["AMENDED_APPROVED_BY_ID"]);

                    if (dr["AMENDED_APPROVED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedApprovedByName = Convert.ToString(dr["AMENDED_APPROVED_BY"]);

                    if (dr["AMENDED_APPROVED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedApprovedByEmail = Convert.ToString(dr["AMENDED_APPROVED_BY_EMAIL"]);


                    if (dr["AMENDED_PLANNING_ACCEPTED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.PlanningAmendedAcceptedByID = Convert.ToInt32(dr["AMENDED_PLANNING_ACCEPTED_BY_ID"]);

                    if (dr["AMENDED_PLANNING_ACCEPTED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.PlanningAmendedAcceptedByName = Convert.ToString(dr["AMENDED_PLANNING_ACCEPTED_BY"]);

                    if (dr["AMENDED_PLANNING_ACCEPTED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.PlanningAmendedAcceptedByEmail = Convert.ToString(dr["AMENDED_PLANNING_ACCEPTED_BY_EMAIL"]);


                    if (dr["AMENDED_FORWARDED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.ForwardedAmendedByID = Convert.ToInt32(dr["AMENDED_FORWARDED_BY_ID"]);

                    if (dr["AMENDED_FORWARDED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.ForwardedAmendedByName = Convert.ToString(dr["AMENDED_FORWARDED_BY"]);

                    if (dr["AMENDED_FORWARDED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.ForwardedAmendedByEmail = Convert.ToString(dr["AMENDED_FORWARDED_BY_EMAIL"]);



                    if (dr["AMENDED_ACCEPTED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedAcceptedByID = Convert.ToInt32(dr["AMENDED_ACCEPTED_BY_ID"]);

                    if (dr["AMENDED_ACCEPTED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedAcceptedByName = Convert.ToString(dr["AMENDED_ACCEPTED_BY"]);

                    if (dr["AMENDED_ACCEPTED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.AmendedAcceptedByEmail = Convert.ToString(dr["AMENDED_ACCEPTED_BY_EMAIL"]);


                    if (dr["AMENDED_QUALITY_ACCEPTED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.QualityAmendedAcceptedByID = Convert.ToInt32(dr["AMENDED_QUALITY_ACCEPTED_BY_ID"]);

                    if (dr["AMENDED_QUALITY_ACCEPTED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.QualityAmendedAcceptedByName = Convert.ToString(dr["AMENDED_QUALITY_ACCEPTED_BY"]);

                    if (dr["AMENDED_QUALITY_ACCEPTED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.QualityAmendedAcceptedByEmail = Convert.ToString(dr["AMENDED_QUALITY_ACCEPTED_BY_EMAIL"]);







                    if (dr["COMPLETED_BY_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.CompletedByID = Convert.ToInt32(dr["COMPLETED_BY_ID"]);

                    if (dr["COMPLETED_BY"] != DBNull.Value)
                        objLOTMailInfoProperties.CompletedByName = Convert.ToString(dr["COMPLETED_BY"]);

                    if (dr["COMPLETED_BY_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.CompletedByEmail = Convert.ToString(dr["COMPLETED_BY_EMAIL"]);




                    if (dr["PRODUCTION_MNGR_ID"] != DBNull.Value)
                        objLOTMailInfoProperties.ProdMngrID = Convert.ToInt32(dr["PRODUCTION_MNGR_ID"]);

                    if (dr["PRODUCTION_MNGR_NAME"] != DBNull.Value)
                        objLOTMailInfoProperties.ProdMngrName = Convert.ToString(dr["PRODUCTION_MNGR_NAME"]);

                    if (dr["PRODUCTION_MNGR_EMAIL"] != DBNull.Value)
                        objLOTMailInfoProperties.ProdMngrEmail = Convert.ToString(dr["PRODUCTION_MNGR_EMAIL"]);


                    break;
                }
            }



            //if (dsMailInfo.Tables[3].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dsMailInfo.Tables[3].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubitemID + "'"))
            //    {
            //        if (dr["PRODUCTION_MNGR_ID"] != DBNull.Value)
            //            objLOTMailInfoProperties.ProdMngrID = Convert.ToInt32(dr["PRODUCTION_MNGR_ID"]);

            //        if (dr["PRODUCTION_MNGR_NAME"] != DBNull.Value)
            //            objLOTMailInfoProperties.ProdMngrName = Convert.ToString(dr["PRODUCTION_MNGR_NAME"]);

            //        if (dr["PRODUCTION_MNGR_EMAIL"] != DBNull.Value)
            //            objLOTMailInfoProperties.ProdMngrEmail = Convert.ToString(dr["PRODUCTION_MNGR_EMAIL"]);
            //    }
            //}


            if (dsMailInfo.Tables[4].Rows.Count > 0)
            {
                foreach (DataRow dr in dsMailInfo.Tables[4].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubitemID + "'"))
                {
                    if (dr["PRODUCTION_MNGR_CC_EMAIL"] != DBNull.Value)
                        prodManagerCC += Convert.ToString(dr["PRODUCTION_MNGR_CC_EMAIL"]) + ";";
                }

                if (unitID == 1)
                {
                    if (dsMailInfo.Tables[11].Rows[0]["EMAIL_ID"] != DBNull.Value)
                        prodManagerCC += Convert.ToString(dsMailInfo.Tables[11].Rows[0]["EMAIL_ID"]) + ";";
                }

                if (!string.IsNullOrEmpty(prodManagerCC))
                    prodManagerCC = prodManagerCC.TrimEnd(';');

                if (!string.IsNullOrEmpty(prodManagerCC))
                    objLOTMailInfoProperties.ProdMngrEmailCC = prodManagerCC;
            }


            if (dsMailInfo.Tables[5].Rows.Count > 0)
            {
                foreach (DataRow dr in dsMailInfo.Tables[5].Rows)
                {
                    if (dr["QUALITY_MNGR_NAME"] != DBNull.Value)
                        qualityManagerNames += Convert.ToString(dr["QUALITY_MNGR_NAME"]) + ";";

                    if (dr["QUALITY_MNGR_EMAIL"] != DBNull.Value)
                        qualityManagerEmails += Convert.ToString(dr["QUALITY_MNGR_EMAIL"]) + ";";
                }

                if (!string.IsNullOrEmpty(qualityManagerNames))
                    qualityManagerNames = qualityManagerNames.TrimEnd(';');

                if (!string.IsNullOrEmpty(qualityManagerEmails))
                    qualityManagerEmails = qualityManagerEmails.TrimEnd(';');


                if (!string.IsNullOrEmpty(qualityManagerNames))
                    objLOTMailInfoProperties.QualityMngrNames = qualityManagerNames;

                if (!string.IsNullOrEmpty(qualityManagerEmails))
                    objLOTMailInfoProperties.QualityMngrEmails = qualityManagerEmails;

            }

            if (dsMailInfo.Tables[6].Rows.Count > 0)
            {
                foreach (DataRow dr in dsMailInfo.Tables[6].Rows)
                {
                    if (dr["ACCEPTER_ID"] != DBNull.Value)
                        planningManagerIDs += Convert.ToString(dr["ACCEPTER_ID"]) + ";";

                    if (dr["PLANNING_MNGR_NAME"] != DBNull.Value)
                        planningManagerNames += Convert.ToString(dr["PLANNING_MNGR_NAME"]) + ";";

                    if (dr["PLANNING_MNGR_EMAIL"] != DBNull.Value)
                        planningManagerEmails += Convert.ToString(dr["PLANNING_MNGR_EMAIL"]) + ";";
                }

                if (!string.IsNullOrEmpty(planningManagerNames))
                    planningManagerNames = planningManagerNames.TrimEnd(';');

                if (!string.IsNullOrEmpty(planningManagerEmails))
                    planningManagerEmails = planningManagerEmails.TrimEnd(';');


                if (!string.IsNullOrEmpty(planningManagerNames))
                    objLOTMailInfoProperties.PlanningMngrNames = planningManagerNames;

                if (!string.IsNullOrEmpty(planningManagerEmails))
                    objLOTMailInfoProperties.PlanningMngrEmails = planningManagerEmails;
            }


            if (dsMailInfo.Tables[10].Rows.Count > 0)
            {
                foreach (DataRow dr in dsMailInfo.Tables[10].Rows)
                {
                    if (dr["ACCEPTER_ID"] != DBNull.Value)
                        planningManagerIDs += Convert.ToString(dr["ACCEPTER_ID"]) + ";";

                    if (dr["PLANNING_MNGR_EMAIL"] != DBNull.Value)
                        planningManagerEmailsCC += Convert.ToString(dr["PLANNING_MNGR_EMAIL"]) + ";";
                }

                if (!string.IsNullOrEmpty(planningManagerEmailsCC))
                    planningManagerEmailsCC = planningManagerEmailsCC.TrimEnd(';');


                if (!string.IsNullOrEmpty(planningManagerEmailsCC))
                    objLOTMailInfoProperties.PlanningMngrEmailsCC = planningManagerEmailsCC;
            }

            if (!string.IsNullOrEmpty(planningManagerIDs))
                planningManagerIDs = planningManagerIDs.TrimEnd(';');

            if (!string.IsNullOrEmpty(planningManagerIDs))
                objLOTMailInfoProperties.planningMngrIDs = planningManagerIDs;




            return objLOTMailInfoProperties;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}