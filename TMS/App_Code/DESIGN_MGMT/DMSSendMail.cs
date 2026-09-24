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


public class DMSSendMail
{

    #region VARIABLES[===================]

    Project objProject = new Project();
    DataSet dsMailInfo = new DataSet();
    DataTable dtMailInfo = new DataTable();
    DMSHtmlForPDFForMail objDMSHtmlForPDF = new DMSHtmlForPDFForMail();
    DataSet dsDetail = new DataSet();

    int returnVal = 0;
    string jobNo = string.Empty;
    string drawingNo = string.Empty;

    int createdByID = 0;
    string createdBy = string.Empty;
    string createdOn = string.Empty;
    string createdByEmail = string.Empty;
    string createdRemarks = string.Empty;

    int modifiedByID = 0;
    string modifiedBy = string.Empty;
    string modifiedOn = string.Empty;
    string modifiedByEmail = string.Empty;
    string modifiedRemarks = string.Empty;

    int openByID = 0;
    string openBy = string.Empty;
    string openOn = string.Empty;
    string openByEmail = string.Empty;
    string openRemarks = string.Empty;

    int sentToCheckingByID = 0;
    string sentToCheckingBy = string.Empty;
    string sentToCheckingOn = string.Empty;
    string sentToCheckingByEmail = string.Empty;
    string sentToCheckingRemarks = string.Empty;

    int checkedByID = 0;
    string checkedBy = string.Empty;
    string checkedOn = string.Empty;
    string checkedByEmail = string.Empty;
    string checkedRemarks = string.Empty;

    int amendmentCount = 0;
    int amendmentFlag = 0;
    int editedFlag = 0;

    int sentToAmendmentByID = 0;
    string sentToAmendmentBy = string.Empty;
    string sentToAmendmentOn = string.Empty;
    string sentToAmendmentByEmail = string.Empty;
    string sentToAmendmentRemarks = string.Empty;

    int amendedByID = 0;
    string amendedBy = string.Empty;
    string amendedOn = string.Empty;
    string amendedByEmail = string.Empty;
    string amendedRemarks = string.Empty;

    int amendedOpenByID = 0;
    string amendedOpenBy = string.Empty;
    string amendedOpenOn = string.Empty;
    string amendedOpenByEmail = string.Empty;
    string amendedOpenRemarks = string.Empty;

    int amendedSentToCheckingByID = 0;
    string amendedSentToCheckingBy = string.Empty;
    string amendedSentToCheckingOn = string.Empty;
    string amendedSentToCheckingByEmail = string.Empty;
    string amendedSentToCheckingRemarks = string.Empty;

    int amendedCheckedByID = 0;
    string amendedCheckedBy = string.Empty;
    string amendedCheckedOn = string.Empty;
    string amendedCheckedByEmail = string.Empty;
    string amendedCheckedRemarks = string.Empty;

    int designRespeEnggID = 0;
    int designRespeEnggEmpRecordID = 0;
    string designRespeEngg = string.Empty;
    string designRespeEnggEmail = string.Empty;

    int designCheckerID = 0;
    string designChecker = string.Empty;
    string designCheckerEmail = string.Empty;

    int managerID = 0;
    string manager = string.Empty;
    string managerEmail = string.Empty;

    string managerCC = string.Empty;
    string managerCCEmail = string.Empty;


    int PEID = 0;
    string PEName = string.Empty;
    string PEEmail = string.Empty;

    int PMID = 0;
    string PMName = string.Empty;
    string PMEmail = string.Empty;


    string fileName = string.Empty;
    string urlTxt = string.Empty;
    string link = string.Empty;
    string href = string.Empty;


    string body = string.Empty;
    string subject = string.Empty;
    string from = string.Empty;
    string fromName = string.Empty;
    string to = string.Empty;
    string toName = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;


    #endregion

    public int ProcessAndSendMail(string drawingNos, int drawingID, int statusID, int mailTypeID)
    {
        returnVal = 0;

        fileName = string.Empty;
        urlTxt = string.Empty;
        link = string.Empty;
        href = string.Empty;

        string ncc = "";

        dsMailInfo = objProject.GetDMSMailInfo(drawingID, drawingNos);
        if (dsMailInfo.Tables.Count > 0 && dsMailInfo.Tables[0].Rows.Count > 0)
        {

            #region MyRegion

            //dtMailInfo.Columns.Add("RECORD_ID", typeof(int));
            //dtMailInfo.Columns.Add("JOB_NO", typeof(string));
            //dtMailInfo.Columns.Add("DESIGN_CATEGORY", typeof(string));
            //dtMailInfo.Columns.Add("DESCRIPTION", typeof(string));
            //dtMailInfo.Columns.Add("UOM", typeof(string));
            //dtMailInfo.Columns.Add("QUANTITY", typeof(string));
            //dtMailInfo.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
            //dtMailInfo.Columns.Add("PLANNED_START_DATE_BY_DESIGN_TEAM", typeof(string));
            //dtMailInfo.Columns.Add("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM", typeof(string));
            //dtMailInfo.Columns.Add("DRAWING_NO", typeof(string));
            //dtMailInfo.Columns.Add("DOCUMENT_LINK", typeof(string));
            //dtMailInfo.Columns.Add("DRAWING_LINK", typeof(string));
            //dtMailInfo.Columns.Add("DRAWING_REV_NO", typeof(string));
            //dtMailInfo.Columns.Add("WORKING_STATUS", typeof(string));
            //dtMailInfo.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            //dtMailInfo.Columns.Add("DESIGN_RESPONSIBLE_ENGG_ID", typeof(int));
            //dtMailInfo.Columns.Add("DESIGN_RESPONSIBLE_ENGG_EMP_RECORD_ID", typeof(int));
            //dtMailInfo.Columns.Add("DESIGN_RESPONSIBLE_ENGG", typeof(string));
            //dtMailInfo.Columns.Add("DESIGN_RESPONSIBLE_ENGG_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("DESIGN_CHECKER_ID", typeof(int));
            //dtMailInfo.Columns.Add("DESIGN_CHECKER", typeof(string));
            //dtMailInfo.Columns.Add("DESIGN_CHECKER_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("STATUS_ID", typeof(int));
            //dtMailInfo.Columns.Add("STATUS", typeof(string));
            //dtMailInfo.Columns.Add("APPLICABLE_FOR_PRODUCTION", typeof(string));
            //dtMailInfo.Columns.Add("REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("IS_REVISED", typeof(string));
            //dtMailInfo.Columns.Add("AMENDMENT_COUNT", typeof(string));
            //dtMailInfo.Columns.Add("AMENDMENT_FLAG", typeof(string));
            //dtMailInfo.Columns.Add("EDITED_FLAG", typeof(string));


            //dtMailInfo.Columns.Add("CREATED_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("CREATED_BY", typeof(string));
            //dtMailInfo.Columns.Add("CREATED_ON", typeof(string));
            //dtMailInfo.Columns.Add("CREATED_BY_EMAIL", typeof(string));

            //dtMailInfo.Columns.Add("MODIFIED_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("MODIFIED_BY", typeof(string));
            //dtMailInfo.Columns.Add("MODIFIED_ON", typeof(string));
            //dtMailInfo.Columns.Add("MODIFIED_BY_EMAIL", typeof(string));


            //dtMailInfo.Columns.Add("OPEN_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("OPEN_BY", typeof(string));
            //dtMailInfo.Columns.Add("OPEN_ON", typeof(string));
            //dtMailInfo.Columns.Add("OPEN_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("OPEN_BY_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("SENT_TO_CHECKING_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("SENT_TO_CHECKING_BY", typeof(string));
            //dtMailInfo.Columns.Add("SENT_TO_CHECKING_ON", typeof(string));
            //dtMailInfo.Columns.Add("SENT_TO_CHECKING_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("SENT_TO_CHECKING_BY_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("CHECKED_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("CHECKED_BY", typeof(string));
            //dtMailInfo.Columns.Add("CHECKED_ON", typeof(string));
            //dtMailInfo.Columns.Add("CHECKED_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("CHECKED_BY_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("CLOSED_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("CLOSED_BY", typeof(string));
            //dtMailInfo.Columns.Add("CLOSED_ON", typeof(string));
            //dtMailInfo.Columns.Add("CLOSED_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("CLOSED_BY_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("SENT_TO_AMENDMENT_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("SENT_TO_AMENDMENT_BY", typeof(string));
            //dtMailInfo.Columns.Add("SENT_TO_AMENDMENT_ON", typeof(string));
            //dtMailInfo.Columns.Add("SENT_TO_AMENDMENT_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("SENT_TO_AMENDMENT_BY_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("AMENDED_BY", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_ON", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_BY_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_OPEN_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("AMENDED_OPEN_BY", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_OPEN_ON", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_OPEN_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_OPEN_BY_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_SENT_TO_CHECKING_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("AMENDED_SENT_TO_CHECKING_BY", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_SENT_TO_CHECKING_ON", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_SENT_TO_CHECKING_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_SENT_TO_CHECKING_BY_EMAIL", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_CHECKED_BY_ID", typeof(int));
            //dtMailInfo.Columns.Add("AMENDED_CHECKED_BY", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_CHECKED_ON", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_CHECKED_REMARKS", typeof(string));
            //dtMailInfo.Columns.Add("AMENDED_CHECKED_BY_EMAIL", typeof(string));

            #endregion

            dtMailInfo = dsMailInfo.Tables[0].Clone();

            #region MyRegion

            drawingNo = string.Empty;
            jobNo = string.Empty;

            createdByID = 0;
            createdBy = string.Empty;
            createdOn = string.Empty;
            createdByEmail = string.Empty;
            createdRemarks = string.Empty;

            openByID = 0;
            openBy = string.Empty;
            openOn = string.Empty;
            openByEmail = string.Empty;
            openRemarks = string.Empty;

            sentToCheckingByID = 0;
            sentToCheckingBy = string.Empty;
            sentToCheckingOn = string.Empty;
            sentToCheckingByEmail = string.Empty;
            sentToCheckingRemarks = string.Empty;

            checkedByID = 0;
            checkedBy = string.Empty;
            checkedOn = string.Empty;
            checkedByEmail = string.Empty;
            checkedRemarks = string.Empty;

            amendmentCount = 0;
            amendmentFlag = 0;
            editedFlag = 0;

            sentToAmendmentByID = 0;
            sentToAmendmentBy = string.Empty;
            sentToAmendmentOn = string.Empty;
            sentToAmendmentByEmail = string.Empty;
            sentToAmendmentRemarks = string.Empty;

            amendedByID = 0;
            amendedBy = string.Empty;
            amendedOn = string.Empty;
            amendedByEmail = string.Empty;
            amendedRemarks = string.Empty;

            amendedOpenByID = 0;
            amendedOpenBy = string.Empty;
            amendedOpenOn = string.Empty;
            amendedOpenByEmail = string.Empty;
            amendedOpenRemarks = string.Empty;

            amendedSentToCheckingByID = 0;
            amendedSentToCheckingBy = string.Empty;
            amendedSentToCheckingOn = string.Empty;
            amendedSentToCheckingByEmail = string.Empty;
            amendedSentToCheckingRemarks = string.Empty;

            amendedCheckedByID = 0;
            amendedCheckedBy = string.Empty;
            amendedCheckedOn = string.Empty;
            amendedCheckedByEmail = string.Empty;
            amendedCheckedRemarks = string.Empty;

            designRespeEnggID = 0;
            designRespeEnggEmpRecordID = 0;
            designRespeEngg = string.Empty;
            designRespeEnggEmail = string.Empty;

            designCheckerID = 0;
            designChecker = string.Empty;
            designCheckerEmail = string.Empty;

            managerID = 0;
            manager = string.Empty;
            managerEmail = string.Empty;

            managerCC = string.Empty;
            managerCCEmail = string.Empty;


            PEID = 0;
            PEName = string.Empty;
            PEEmail = string.Empty;

            PMID = 0;
            PMName = string.Empty;
            PMEmail = string.Empty;

            #endregion

            urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
            link = "'" + urlTxt + "/Login.aspx?dmsstatusid=" + statusID + "'";



            if (dsMailInfo.Tables[7].Rows.Count > 0)
            {
                DataRow dr7 = dsMailInfo.Tables[7].Rows[0];
                ncc = Convert.ToString(dr7["EMAIL_ID"]);
            }


            if (dsMailInfo.Tables[6].Rows.Count > 0)
            {
                foreach (DataRow dr6 in dsMailInfo.Tables[6].Rows)
                {
                    createdByEmail = string.Empty;
                    openByEmail = string.Empty;
                    sentToCheckingByEmail = string.Empty;
                    checkedByEmail = string.Empty;
                    sentToAmendmentByEmail = string.Empty;
                    amendedByEmail = string.Empty;
                    amendedOpenByEmail = string.Empty;
                    amendedSentToCheckingByEmail = string.Empty;
                    amendedCheckedByEmail = string.Empty;
                    designRespeEnggEmail = string.Empty;
                    designCheckerEmail = string.Empty;
                    managerEmail = string.Empty;
                    managerCCEmail = string.Empty;

                    dtMailInfo.Rows.Clear();

                    foreach (DataRow dr0 in dsMailInfo.Tables[0].Select("JOB_NO='" + Convert.ToString(dr6["JOB_NO"]) + "'"))
                    {
                        DataRow drm = dtMailInfo.NewRow();

                        #region MyRegion

                        if (dr0["RECORD_ID"] != DBNull.Value)
                            drm["RECORD_ID"] = Convert.ToInt32(dr0["RECORD_ID"]);

                        if (dr0["JOB_NO"] != DBNull.Value)
                            drm["JOB_NO"] = Convert.ToString(dr0["JOB_NO"]);

                        if (dr0["DESIGN_CATEGORY"] != DBNull.Value)
                            drm["DESIGN_CATEGORY"] = Convert.ToString(dr0["DESIGN_CATEGORY"]);

                        if (dr0["DESCRIPTION"] != DBNull.Value)
                            drm["DESCRIPTION"] = Convert.ToString(dr0["DESCRIPTION"]);

                        if (dr0["UOM"] != DBNull.Value)
                            drm["UOM"] = Convert.ToString(dr0["UOM"]);

                        if (dr0["QUANTITY"] != DBNull.Value)
                            drm["QUANTITY"] = Convert.ToString(dr0["QUANTITY"]);

                        if (dr0["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value)
                            drm["REQD_DATE_BY_PROJECT_TEAM"] = Convert.ToString(dr0["REQD_DATE_BY_PROJECT_TEAM"]);

                        if (dr0["PLANNED_START_DATE_BY_DESIGN_TEAM"] != DBNull.Value)
                            drm["PLANNED_START_DATE_BY_DESIGN_TEAM"] = Convert.ToString(dr0["PLANNED_START_DATE_BY_DESIGN_TEAM"]);

                        if (dr0["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] != DBNull.Value)
                            drm["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = Convert.ToString(dr0["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"]);

                        if (dr0["DRAWING_NO"] != DBNull.Value)
                            drm["DRAWING_NO"] = Convert.ToString(dr0["DRAWING_NO"]);

                        if (dr0["CLIENT_DRAWING_NO"] != DBNull.Value)
                            drm["CLIENT_DRAWING_NO"] = Convert.ToString(dr0["CLIENT_DRAWING_NO"]);

                        if (dr0["CONTRACTOR_DRAWING_NO"] != DBNull.Value)
                            drm["CONTRACTOR_DRAWING_NO"] = Convert.ToString(dr0["CONTRACTOR_DRAWING_NO"]);

                        if (dr0["DOCUMENT_LINK"] != DBNull.Value)
                            drm["DOCUMENT_LINK"] = Convert.ToString(dr0["DOCUMENT_LINK"]);

                        if (dr0["DRAWING_LINK"] != DBNull.Value)
                            drm["DRAWING_LINK"] = Convert.ToString(dr0["DRAWING_LINK"]);

                        if (dr0["DRAWING_REV_NO"] != DBNull.Value)
                            drm["DRAWING_REV_NO"] = Convert.ToString(dr0["DRAWING_REV_NO"]);

                        if (dr0["WORKING_STATUS"] != DBNull.Value)
                            drm["WORKING_STATUS"] = Convert.ToString(dr0["WORKING_STATUS"]);

                        if (dr0["EXPECTED_COMPLETION_DATE"] != DBNull.Value)
                            drm["EXPECTED_COMPLETION_DATE"] = Convert.ToString(dr0["EXPECTED_COMPLETION_DATE"]);

                        if (dr0["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value)
                            drm["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(dr0["DESIGN_RESPONSIBLE_ENGG_ID"]);

                        if (dr0["DESIGN_RESPONSIBLE_ENGG_EMP_RECORD_ID"] != DBNull.Value)
                            drm["DESIGN_RESPONSIBLE_ENGG_EMP_RECORD_ID"] = Convert.ToInt32(dr0["DESIGN_RESPONSIBLE_ENGG_EMP_RECORD_ID"]);

                        if (dr0["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value)
                            drm["DESIGN_RESPONSIBLE_ENGG"] = Convert.ToString(dr0["DESIGN_RESPONSIBLE_ENGG"]);

                        if (dr0["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value)
                            drm["DESIGN_RESPONSIBLE_ENGG_EMAIL"] = Convert.ToString(dr0["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                        if (dr0["DESIGN_CHECKER_ID"] != DBNull.Value)
                            drm["DESIGN_CHECKER_ID"] = Convert.ToInt32(dr0["DESIGN_CHECKER_ID"]);

                        if (dr0["DESIGN_CHECKER"] != DBNull.Value)
                            drm["DESIGN_CHECKER"] = Convert.ToString(dr0["DESIGN_CHECKER"]);

                        if (dr0["DESIGN_CHECKER_EMAIL"] != DBNull.Value)
                            drm["DESIGN_CHECKER_EMAIL"] = Convert.ToString(dr0["DESIGN_CHECKER_EMAIL"]);

                        if (dr0["STATUS_ID"] != DBNull.Value)
                            drm["STATUS_ID"] = Convert.ToInt32(dr0["STATUS_ID"]);

                        if (dr0["STATUS"] != DBNull.Value)
                            drm["STATUS"] = Convert.ToString(dr0["STATUS"]);

                        if (dr0["APPLICABLE_FOR_PRODUCTION"] != DBNull.Value)
                            drm["APPLICABLE_FOR_PRODUCTION"] = Convert.ToString(dr0["APPLICABLE_FOR_PRODUCTION"]);

                        if (dr0["REMARKS"] != DBNull.Value)
                            drm["REMARKS"] = Convert.ToString(dr0["REMARKS"]);

                        if (dr0["IS_REVISED"] != DBNull.Value)
                            drm["IS_REVISED"] = Convert.ToString(dr0["IS_REVISED"]);

                        if (dr0["AMENDMENT_COUNT"] != DBNull.Value)
                            drm["AMENDMENT_COUNT"] = Convert.ToString(dr0["AMENDMENT_COUNT"]);

                        if (dr0["AMENDMENT_FLAG"] != DBNull.Value)
                            drm["AMENDMENT_FLAG"] = Convert.ToString(dr0["AMENDMENT_FLAG"]);

                        if (dr0["EDITED_FLAG"] != DBNull.Value)
                            drm["EDITED_FLAG"] = Convert.ToString(dr0["EDITED_FLAG"]);



                        if (dr0["CREATED_BY_ID"] != DBNull.Value)
                            drm["CREATED_BY_ID"] = Convert.ToInt32(dr0["CREATED_BY_ID"]);

                        if (dr0["CREATED_BY"] != DBNull.Value)
                            drm["CREATED_BY"] = Convert.ToString(dr0["CREATED_BY"]);

                        if (dr0["CREATED_ON"] != DBNull.Value)
                            drm["CREATED_ON"] = Convert.ToString(dr0["CREATED_ON"]);

                        if (dr0["CREATED_BY_EMAIL"] != DBNull.Value)
                            drm["CREATED_BY_EMAIL"] = Convert.ToString(dr0["CREATED_BY_EMAIL"]);



                        if (dr0["MODIFIED_BY_ID"] != DBNull.Value)
                            drm["MODIFIED_BY_ID"] = Convert.ToInt32(dr0["MODIFIED_BY_ID"]);

                        if (dr0["MODIFIED_BY"] != DBNull.Value)
                            drm["MODIFIED_BY"] = Convert.ToString(dr0["MODIFIED_BY"]);

                        if (dr0["MODIFIED_ON"] != DBNull.Value)
                            drm["MODIFIED_ON"] = Convert.ToString(dr0["MODIFIED_ON"]);

                        if (dr0["MODIFIED_BY_EMAIL"] != DBNull.Value)
                            drm["MODIFIED_BY_EMAIL"] = Convert.ToString(dr0["MODIFIED_BY_EMAIL"]);



                        if (dr0["OPEN_BY_ID"] != DBNull.Value)
                            drm["OPEN_BY_ID"] = Convert.ToInt32(dr0["OPEN_BY_ID"]);

                        if (dr0["OPEN_BY"] != DBNull.Value)
                            drm["OPEN_BY"] = Convert.ToString(dr0["OPEN_BY"]);

                        if (dr0["OPEN_ON"] != DBNull.Value)
                            drm["OPEN_ON"] = Convert.ToString(dr0["OPEN_ON"]);

                        if (dr0["OPEN_REMARKS"] != DBNull.Value)
                            drm["OPEN_REMARKS"] = Convert.ToString(dr0["OPEN_REMARKS"]);

                        if (dr0["OPEN_BY_EMAIL"] != DBNull.Value)
                            drm["OPEN_BY_EMAIL"] = Convert.ToString(dr0["OPEN_BY_EMAIL"]);

                        if (dr0["SENT_TO_CHECKING_BY_ID"] != DBNull.Value)
                            drm["SENT_TO_CHECKING_BY_ID"] = Convert.ToInt32(dr0["SENT_TO_CHECKING_BY_ID"]);

                        if (dr0["SENT_TO_CHECKING_BY"] != DBNull.Value)
                            drm["SENT_TO_CHECKING_BY"] = Convert.ToString(dr0["SENT_TO_CHECKING_BY"]);

                        if (dr0["SENT_TO_CHECKING_ON"] != DBNull.Value)
                            drm["SENT_TO_CHECKING_ON"] = Convert.ToString(dr0["SENT_TO_CHECKING_ON"]);

                        if (dr0["SENT_TO_CHECKING_REMARKS"] != DBNull.Value)
                            drm["SENT_TO_CHECKING_REMARKS"] = Convert.ToString(dr0["SENT_TO_CHECKING_REMARKS"]);

                        if (dr0["SENT_TO_CHECKING_BY_EMAIL"] != DBNull.Value)
                            drm["SENT_TO_CHECKING_BY_EMAIL"] = Convert.ToString(dr0["SENT_TO_CHECKING_BY_EMAIL"]);

                        if (dr0["CHECKED_BY_ID"] != DBNull.Value)
                            drm["CHECKED_BY_ID"] = Convert.ToInt32(dr0["CHECKED_BY_ID"]);

                        if (dr0["CHECKED_BY"] != DBNull.Value)
                            drm["CHECKED_BY"] = Convert.ToString(dr0["CHECKED_BY"]);

                        if (dr0["CHECKED_ON"] != DBNull.Value)
                            drm["CHECKED_ON"] = Convert.ToString(dr0["CHECKED_ON"]);

                        if (dr0["CHECKED_REMARKS"] != DBNull.Value)
                            drm["CHECKED_REMARKS"] = Convert.ToString(dr0["CHECKED_REMARKS"]);

                        if (dr0["CHECKED_BY_EMAIL"] != DBNull.Value)
                            drm["CHECKED_BY_EMAIL"] = Convert.ToString(dr0["CHECKED_BY_EMAIL"]);

                        if (dr0["CLOSED_BY_ID"] != DBNull.Value)
                            drm["CLOSED_BY_ID"] = Convert.ToInt32(dr0["CLOSED_BY_ID"]);

                        if (dr0["CLOSED_BY"] != DBNull.Value)
                            drm["CLOSED_BY"] = Convert.ToString(dr0["CLOSED_BY"]);

                        if (dr0["CLOSED_ON"] != DBNull.Value)
                            drm["CLOSED_ON"] = Convert.ToString(dr0["CLOSED_ON"]);

                        if (dr0["CLOSED_REMARKS"] != DBNull.Value)
                            drm["CLOSED_REMARKS"] = Convert.ToString(dr0["CLOSED_REMARKS"]);

                        if (dr0["CLOSED_BY_EMAIL"] != DBNull.Value)
                            drm["CLOSED_BY_EMAIL"] = Convert.ToString(dr0["CLOSED_BY_EMAIL"]);

                        if (dr0["SENT_TO_AMENDMENT_BY_ID"] != DBNull.Value)
                            drm["SENT_TO_AMENDMENT_BY_ID"] = Convert.ToInt32(dr0["SENT_TO_AMENDMENT_BY_ID"]);

                        if (dr0["SENT_TO_AMENDMENT_BY"] != DBNull.Value)
                            drm["SENT_TO_AMENDMENT_BY"] = Convert.ToString(dr0["SENT_TO_AMENDMENT_BY"]);

                        if (dr0["SENT_TO_AMENDMENT_ON"] != DBNull.Value)
                            drm["SENT_TO_AMENDMENT_ON"] = Convert.ToString(dr0["SENT_TO_AMENDMENT_ON"]);

                        if (dr0["SENT_TO_AMENDMENT_REMARKS"] != DBNull.Value)
                            drm["SENT_TO_AMENDMENT_REMARKS"] = Convert.ToString(dr0["SENT_TO_AMENDMENT_REMARKS"]);

                        if (dr0["SENT_TO_AMENDMENT_BY_EMAIL"] != DBNull.Value)
                            drm["SENT_TO_AMENDMENT_BY_EMAIL"] = Convert.ToString(dr0["SENT_TO_AMENDMENT_BY_EMAIL"]);

                        if (dr0["AMENDED_BY_ID"] != DBNull.Value)
                            drm["AMENDED_BY_ID"] = Convert.ToInt32(dr0["AMENDED_BY_ID"]);

                        if (dr0["AMENDED_BY"] != DBNull.Value)
                            drm["AMENDED_BY"] = Convert.ToString(dr0["AMENDED_BY"]);

                        if (dr0["AMENDED_ON"] != DBNull.Value)
                            drm["AMENDED_ON"] = Convert.ToString(dr0["AMENDED_ON"]);

                        if (dr0["AMENDED_REMARKS"] != DBNull.Value)
                            drm["AMENDED_REMARKS"] = Convert.ToString(dr0["AMENDED_REMARKS"]);

                        if (dr0["AMENDED_BY_EMAIL"] != DBNull.Value)
                            drm["AMENDED_BY_EMAIL"] = Convert.ToString(dr0["AMENDED_BY_EMAIL"]);

                        if (dr0["AMENDED_OPEN_BY_ID"] != DBNull.Value)
                            drm["AMENDED_OPEN_BY_ID"] = Convert.ToInt32(dr0["AMENDED_OPEN_BY_ID"]);

                        if (dr0["AMENDED_OPEN_BY"] != DBNull.Value)
                            drm["AMENDED_OPEN_BY"] = Convert.ToString(dr0["AMENDED_OPEN_BY"]);

                        if (dr0["AMENDED_OPEN_ON"] != DBNull.Value)
                            drm["AMENDED_OPEN_ON"] = Convert.ToString(dr0["AMENDED_OPEN_ON"]);

                        if (dr0["AMENDED_OPEN_REMARKS"] != DBNull.Value)
                            drm["AMENDED_OPEN_REMARKS"] = Convert.ToString(dr0["AMENDED_OPEN_REMARKS"]);

                        if (dr0["AMENDED_OPEN_BY_EMAIL"] != DBNull.Value)
                            drm["AMENDED_OPEN_BY_EMAIL"] = Convert.ToString(dr0["AMENDED_OPEN_BY_EMAIL"]);

                        if (dr0["AMENDED_SENT_TO_CHECKING_BY_ID"] != DBNull.Value)
                            drm["AMENDED_SENT_TO_CHECKING_BY_ID"] = Convert.ToInt32(dr0["AMENDED_SENT_TO_CHECKING_BY_ID"]);

                        if (dr0["AMENDED_SENT_TO_CHECKING_BY"] != DBNull.Value)
                            drm["AMENDED_SENT_TO_CHECKING_BY"] = Convert.ToString(dr0["AMENDED_SENT_TO_CHECKING_BY"]);

                        if (dr0["AMENDED_SENT_TO_CHECKING_ON"] != DBNull.Value)
                            drm["AMENDED_SENT_TO_CHECKING_ON"] = Convert.ToString(dr0["AMENDED_SENT_TO_CHECKING_ON"]);

                        if (dr0["AMENDED_SENT_TO_CHECKING_REMARKS"] != DBNull.Value)
                            drm["AMENDED_SENT_TO_CHECKING_REMARKS"] = Convert.ToString(dr0["AMENDED_SENT_TO_CHECKING_REMARKS"]);

                        if (dr0["AMENDED_SENT_TO_CHECKING_BY_EMAIL"] != DBNull.Value)
                            drm["AMENDED_SENT_TO_CHECKING_BY_EMAIL"] = Convert.ToString(dr0["AMENDED_SENT_TO_CHECKING_BY_EMAIL"]);

                        if (dr0["AMENDED_CHECKED_BY_ID"] != DBNull.Value)
                            drm["AMENDED_CHECKED_BY_ID"] = Convert.ToInt32(dr0["AMENDED_CHECKED_BY_ID"]);

                        if (dr0["AMENDED_CHECKED_BY"] != DBNull.Value)
                            drm["AMENDED_CHECKED_BY"] = Convert.ToString(dr0["AMENDED_CHECKED_BY"]);

                        if (dr0["AMENDED_CHECKED_ON"] != DBNull.Value)
                            drm["AMENDED_CHECKED_ON"] = Convert.ToString(dr0["AMENDED_CHECKED_ON"]);

                        if (dr0["AMENDED_CHECKED_REMARKS"] != DBNull.Value)
                            drm["AMENDED_CHECKED_REMARKS"] = Convert.ToString(dr0["AMENDED_CHECKED_REMARKS"]);

                        if (dr0["AMENDED_CHECKED_BY_EMAIL"] != DBNull.Value)
                            drm["AMENDED_CHECKED_BY_EMAIL"] = Convert.ToString(dr0["AMENDED_CHECKED_BY_EMAIL"]);




                        if (dr0["PE_ID"] != DBNull.Value)
                            drm["PE_ID"] = Convert.ToInt32(dr0["PE_ID"]);

                        if (dr0["PE_NAME"] != DBNull.Value)
                            drm["PE_NAME"] = Convert.ToString(dr0["PE_NAME"]);

                        if (dr0["PE_EMAIL_ID"] != DBNull.Value)
                            drm["PE_EMAIL_ID"] = Convert.ToString(dr0["PE_EMAIL_ID"]);


                        if (dr0["PM_ID"] != DBNull.Value)
                            drm["PM_ID"] = Convert.ToInt32(dr0["PM_ID"]);

                        if (dr0["PM_NAME"] != DBNull.Value)
                            drm["PM_NAME"] = Convert.ToString(dr0["PM_NAME"]);

                        if (dr0["PM_EMAIL_ID"] != DBNull.Value)
                            drm["PM_EMAIL_ID"] = Convert.ToString(dr0["PM_EMAIL_ID"]);


                        #endregion

                        dtMailInfo.Rows.Add(drm);
                    }


                    if (dtMailInfo.Rows.Count > 0)
                    {


                        DataTable dtEng = new DataTable();
                        dtEng.Columns.Add("DESIGN_RESPONSIBLE_ENGG_ID", typeof(int));

                        int count1 = 0;
                        foreach (DataRow dre in dtMailInfo.Rows)
                        {
                            if (dtEng.Rows.Count > 0)
                            {
                                count1 = 0;
                                foreach (DataRow dr0 in dtEng.Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                {
                                    count1++;
                                }

                                if (count1 == 0)
                                {
                                    DataRow dr = dtEng.NewRow();
                                    dr["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]);
                                    dtEng.Rows.Add(dr);
                                }

                            }
                            else
                            {
                                DataRow dr = dtEng.NewRow();
                                dr["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]);
                                dtEng.Rows.Add(dr);
                            }
                        }



                        DataTable dtChe = new DataTable();
                        dtChe.Columns.Add("DESIGN_CHECKER_ID", typeof(int));

                        int count2 = 0;
                        foreach (DataRow drch in dtMailInfo.Rows)
                        {
                            if (dtChe.Rows.Count > 0)
                            {
                                count2 = 0;
                                foreach (DataRow dr0 in dtChe.Select("DESIGN_CHECKER_ID='" + Convert.ToInt32(drch["DESIGN_CHECKER_ID"]) + "'"))
                                {
                                    count2++;
                                }

                                if (count2 == 0)
                                {
                                    DataRow dr = dtChe.NewRow();
                                    dr["DESIGN_CHECKER_ID"] = Convert.ToInt32(drch["DESIGN_CHECKER_ID"]);
                                    dtChe.Rows.Add(dr);
                                }

                            }
                            else
                            {
                                DataRow dr = dtChe.NewRow();
                                dr["DESIGN_CHECKER_ID"] = Convert.ToInt32(drch["DESIGN_CHECKER_ID"]);
                                dtChe.Rows.Add(dr);
                            }
                        }




                        if (dtMailInfo.Rows[0]["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["JOB_NO"])))
                            jobNo = Convert.ToString(dtMailInfo.Rows[0]["JOB_NO"]);

                        if (dsMailInfo.Tables[1].Rows.Count > 0)
                        {
                            if (dsMailInfo.Tables[1].Rows[0]["EMP_RECORD_ID"] != DBNull.Value && Convert.ToInt32(dsMailInfo.Tables[1].Rows[0]["EMP_RECORD_ID"]) > 0)
                                managerID = Convert.ToInt32(dsMailInfo.Tables[1].Rows[0]["EMP_RECORD_ID"]);

                            if (dsMailInfo.Tables[1].Rows[0]["EMPLOYEE_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsMailInfo.Tables[1].Rows[0]["EMPLOYEE_NAME"])))
                                manager = Convert.ToString(dsMailInfo.Tables[1].Rows[0]["EMPLOYEE_NAME"]);

                            if (dsMailInfo.Tables[1].Rows[0]["EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsMailInfo.Tables[1].Rows[0]["EMAIL_ID"])))
                                managerEmail = Convert.ToString(dsMailInfo.Tables[1].Rows[0]["EMAIL_ID"]);
                        }

                        if (dsMailInfo.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsMailInfo.Tables[2].Rows)
                            {
                                if (dr["EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMAIL_ID"])))
                                    managerCCEmail += Convert.ToString(dr["EMAIL_ID"]) + ";";
                            }

                            if (!string.IsNullOrEmpty(managerCCEmail))
                                managerCCEmail = managerCCEmail.TrimEnd(';');
                        }



                        if (dtMailInfo.Rows[0]["PE_ID"] != DBNull.Value && Convert.ToInt32(dtMailInfo.Rows[0]["PE_ID"]) > 0)
                            PEID = Convert.ToInt32(dtMailInfo.Rows[0]["PE_ID"]);

                        if (dtMailInfo.Rows[0]["PE_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["PE_NAME"])))
                            PEName = Convert.ToString(dtMailInfo.Rows[0]["PE_NAME"]);

                        if (dtMailInfo.Rows[0]["PE_EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["PE_EMAIL_ID"])))
                            PEEmail = Convert.ToString(dtMailInfo.Rows[0]["PE_EMAIL_ID"]);




                        if (dtMailInfo.Rows[0]["PM_ID"] != DBNull.Value && Convert.ToInt32(dtMailInfo.Rows[0]["PM_ID"]) > 0)
                            PMID = Convert.ToInt32(dtMailInfo.Rows[0]["PM_ID"]);

                        if (dtMailInfo.Rows[0]["PM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["PM_NAME"])))
                            PMName = Convert.ToString(dtMailInfo.Rows[0]["PM_NAME"]);

                        if (dtMailInfo.Rows[0]["PM_EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["PM_EMAIL_ID"])))
                            PMEmail = Convert.ToString(dtMailInfo.Rows[0]["PM_EMAIL_ID"]);




                        if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                        {
                            if (dtMailInfo.Rows[0]["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"])))
                                createdBy = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"]);

                            if (dtMailInfo.Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"])))
                                createdByEmail = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"]);




                            if (dtMailInfo.Rows[0]["MODIFIED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["MODIFIED_BY"])))
                                modifiedBy = Convert.ToString(dtMailInfo.Rows[0]["MODIFIED_BY"]);

                            if (dtMailInfo.Rows[0]["MODIFIED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["MODIFIED_BY_EMAIL"])))
                                modifiedByEmail = Convert.ToString(dtMailInfo.Rows[0]["MODIFIED_BY_EMAIL"]);




                            if (dtMailInfo.Rows[0]["AMENDED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["AMENDED_BY"])))
                                amendedBy = Convert.ToString(dtMailInfo.Rows[0]["AMENDED_BY"]);

                            if (dtMailInfo.Rows[0]["AMENDED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["AMENDED_BY_EMAIL"])))
                                amendedByEmail = Convert.ToString(dtMailInfo.Rows[0]["AMENDED_BY_EMAIL"]);


                            if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' generated on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/01GeneratedMail.htm";

                                from = createdByEmail;
                                fromName = createdBy;
                                to = managerEmail;
                                toName = manager;



                                if (PEID == PMID)
                                {
                                    cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                }
                                else
                                {
                                    cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                }



                                href = "<a href=" + link + ">Please Assign Drawing</a>";

                                returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                            }

                            else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedGeneratedMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' edited on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/08EditedGeneratedMail.htm";

                                from = modifiedByEmail;
                                fromName = modifiedBy;
                                to = managerEmail;
                                toName = manager;

                                if (PEID == PMID)
                                {
                                    cc = managerCCEmail + ";" + modifiedByEmail + ";" + createdByEmail + ";" + PMEmail;
                                }
                                else
                                {
                                    cc = managerCCEmail + ";" + modifiedByEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                }

                                //cc = managerCCEmail + ";" + modifiedByEmail + ";" + createdByEmail;

                                href = "<a href=" + link + ">Please Assign Edited Drawing</a>";

                                returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                            }

                            else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendmentMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' sent to amendment on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/11AmendmentMail.htm";
                                href = "<a href=" + link + ">Please Amend Drawing</a>";

                                if (dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY"])))
                                    sentToAmendmentBy = Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY"]);

                                if (dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY_EMAIL"])))
                                    sentToAmendmentByEmail = Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY_EMAIL"]);



                                foreach (DataRow dr in dsMailInfo.Tables[5].Rows)
                                {
                                    if (dr["CREATED_BY_ID"] != DBNull.Value && Convert.ToInt32(dr["CREATED_BY_ID"]) > 0)
                                        createdByID = Convert.ToInt32(dr["CREATED_BY_ID"]);

                                    if (dr["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CREATED_BY"])))
                                        createdBy = Convert.ToString(dr["CREATED_BY"]);

                                    if (dr["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CREATED_BY_EMAIL"])))
                                        createdByEmail = Convert.ToString(dr["CREATED_BY_EMAIL"]);


                                    from = sentToAmendmentByEmail;
                                    fromName = sentToAmendmentBy;
                                    to = createdByEmail;
                                    toName = createdBy;

                                    if (createdByID != designRespeEnggID)
                                    {
                                        //cc = managerEmail + ";" + designRespeEnggEmail;
                                        if (PEID == PMID)
                                        {
                                            cc = managerEmail + ";" + designRespeEnggEmail + ";" + PMEmail;
                                        }
                                        else
                                        {
                                            cc = managerEmail + ";" + designRespeEnggEmail + ";" + PMEmail + ";" + PEEmail;
                                        }
                                    }
                                    else
                                    {
                                        //cc = managerEmail + ";" + managerCCEmail + ";" + sentToAmendmentByEmail;
                                        if (PEID == PMID)
                                        {
                                            cc = managerEmail + ";" + managerCCEmail + ";" + sentToAmendmentByEmail + ";" + PMEmail;
                                        }
                                        else
                                        {
                                            cc = managerEmail + ";" + managerCCEmail + ";" + sentToAmendmentByEmail + ";" + PMEmail + ";" + PEEmail;
                                        }
                                    }

                                    if (from.Contains("neeraj.gupta@coperion.com"))
                                    {
                                        cc += ";anit.bhatia@coperion.com";
                                    };


                                    returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                }
                            }

                            else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendededMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' generated on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/13AmendededMail.htm";

                                from = amendedByEmail;
                                fromName = amendedBy;
                                to = managerEmail;
                                toName = manager;

                                //cc = managerCCEmail + ";" + createdByEmail;
                                if (PEID == PMID)
                                {
                                    cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                }
                                else
                                {
                                    cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                }

                                href = "<a href=" + link + ">Please Assign Drawing</a>";

                                returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                            }
                        }


                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                        {
                            if (dtMailInfo.Rows[0]["CREATED_BY_ID"] != DBNull.Value && Convert.ToInt32(dtMailInfo.Rows[0]["CREATED_BY_ID"]) > 0)
                                createdByID = Convert.ToInt32(dtMailInfo.Rows[0]["CREATED_BY_ID"]);

                            if (dtMailInfo.Rows[0]["AMENDMENT_COUNT"] != DBNull.Value && Convert.ToInt32(dtMailInfo.Rows[0]["AMENDMENT_COUNT"]) > 0)
                                amendmentCount = Convert.ToInt32(dtMailInfo.Rows[0]["AMENDMENT_COUNT"]);

                            if (dtMailInfo.Rows[0]["AMENDMENT_FLAG"] != DBNull.Value && Convert.ToInt32(dtMailInfo.Rows[0]["AMENDMENT_FLAG"]) > 0)
                                amendmentFlag = Convert.ToInt32(dtMailInfo.Rows[0]["AMENDMENT_FLAG"]);

                            if (dtMailInfo.Rows[0]["EDITED_FLAG"] != DBNull.Value && Convert.ToInt32(dtMailInfo.Rows[0]["EDITED_FLAG"]) > 0)
                                editedFlag = Convert.ToInt32(dtMailInfo.Rows[0]["EDITED_FLAG"]);


                            if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToHimselfMail) ||
                                mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToHimselfMail) ||
                                mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToHimselfMail) ||
                                mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToOtherMail) ||
                                mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToOtherMail) ||
                                mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToOtherMail))
                            {
                                foreach (DataRow dr in dsMailInfo.Tables[3].Rows)
                                {
                                    if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                        designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);


                                    if (dr["DESIGN_RESPONSIBLE_ENGG_EMP_RECORD_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_EMP_RECORD_ID"]) > 0)
                                        designRespeEnggEmpRecordID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_EMP_RECORD_ID"]);

                                    if (createdByID == designRespeEnggEmpRecordID)
                                    {
                                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToHimselfMail);

                                        if (amendmentCount > 0)
                                        {
                                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToHimselfMail);
                                        }

                                        if (editedFlag > 0)
                                        {
                                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToHimselfMail);
                                        }
                                    }
                                    else
                                    {
                                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToOtherMail);

                                        if (amendmentCount > 0)
                                        {
                                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToOtherMail);
                                        }

                                        if (editedFlag > 0)
                                        {
                                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToOtherMail);
                                        }
                                    }

                                    if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToHimselfMail))
                                    {
                                        subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' created on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                        fileName = "~/PROJECT/DMS/EMAIL_FORMATS/02OpenToHimselfMail.htm";


                                        if (dtMailInfo.Rows[0]["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"])))
                                            createdBy = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"]);

                                        if (dtMailInfo.Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"])))
                                            createdByEmail = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"]);


                                        if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                            designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                        from = createdByEmail;
                                        fromName = createdBy;
                                        to = managerEmail;
                                        toName = manager;

                                        //cc = managerCCEmail + ";" + createdByEmail;
                                        if (PEID == PMID)
                                        {
                                            cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                        }
                                        else
                                        {
                                            cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                        }

                                        if (designRespeEnggID == 9)//For neeraj gupta
                                        {
                                            cc = cc + ";" + ncc;
                                        }

                                        returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                    }

                                    else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToHimselfMail))
                                    {
                                        subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' edited on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                        fileName = "~/PROJECT/DMS/EMAIL_FORMATS/09EditedOpenToHimselfMail.htm";


                                        if (dtMailInfo.Rows[0]["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"])))
                                            createdBy = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"]);

                                        if (dtMailInfo.Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"])))
                                            createdByEmail = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"]);


                                        if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                            designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                        from = createdByEmail;
                                        fromName = createdBy;
                                        to = managerEmail;
                                        toName = manager;

                                        //cc = managerCCEmail + ";" + createdByEmail;
                                        if (PEID == PMID)
                                        {
                                            cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                        }
                                        else
                                        {
                                            cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                        }

                                        returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                    }

                                    else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToHimselfMail))
                                    {
                                        subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' amended on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                        fileName = "~/PROJECT/DMS/EMAIL_FORMATS/14AmendedOpenToHimselfMail.htm";

                                        if (dtMailInfo.Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"])))
                                            createdByEmail = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"]);

                                        if (dtMailInfo.Rows[0]["AMENDED_OPEN_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["AMENDED_OPEN_BY"])))
                                            amendedOpenBy = Convert.ToString(dtMailInfo.Rows[0]["AMENDED_OPEN_BY"]);

                                        if (dtMailInfo.Rows[0]["AMENDED_OPEN_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["AMENDED_OPEN_BY_EMAIL"])))
                                            amendedOpenByEmail = Convert.ToString(dtMailInfo.Rows[0]["AMENDED_OPEN_BY_EMAIL"]);


                                        if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                            designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                        from = amendedOpenByEmail;
                                        fromName = amendedOpenBy;
                                        to = managerEmail;
                                        toName = manager;

                                        //cc = managerCCEmail + ";" + createdByEmail;
                                        if (PEID == PMID)
                                        {
                                            cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                        }
                                        else
                                        {
                                            cc = managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                        }

                                        returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                    }

                                    else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToOtherMail))
                                    {
                                        subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' created on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                        fileName = "~/PROJECT/DMS/EMAIL_FORMATS/03OpenToOtherMail.htm";
                                        href = "<a href=" + link + ">Please create drawing and send for checking</a>";


                                        if (dtMailInfo.Rows[0]["CREATED_BY_ID"] != DBNull.Value && Convert.ToInt32(dtMailInfo.Rows[0]["CREATED_BY_ID"]) > 0)
                                            createdByID = Convert.ToInt32(dtMailInfo.Rows[0]["CREATED_BY_ID"]);

                                        if (dtMailInfo.Rows[0]["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"])))
                                            createdBy = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"]);

                                        if (dtMailInfo.Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"])))
                                            createdByEmail = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"]);

                                        if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                            designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                        if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                            designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                        if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                            designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                        from = createdByEmail;
                                        fromName = createdBy;
                                        to = designRespeEnggEmail;
                                        toName = designRespeEngg;

                                        //cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail;
                                        if (PEID == PMID)
                                        {
                                            cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                        }
                                        else
                                        {
                                            cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                        }

                                        returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                    }

                                    else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToOtherMail))
                                    {
                                        subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' edited on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                        fileName = "~/PROJECT/DMS/EMAIL_FORMATS/10EditedOpenToOtherMail.htm";
                                        href = "<a href=" + link + ">Please create drawing and send for checking</a>";

                                        if (dtMailInfo.Rows[0]["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"])))
                                            createdBy = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"]);

                                        if (dtMailInfo.Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"])))
                                            createdByEmail = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"]);


                                        if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                            designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                        if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                            designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                        if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                            designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);


                                        from = createdByEmail;
                                        fromName = createdBy;
                                        to = designRespeEnggEmail;
                                        toName = designRespeEngg;

                                        //cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail;
                                        if (PEID == PMID)
                                        {
                                            cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                        }
                                        else
                                        {
                                            cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                        }

                                        returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                    }

                                    else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToOtherMail))
                                    {
                                        subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' generated on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                        fileName = "~/PROJECT/DMS/EMAIL_FORMATS/15AmendedOpenToOtherMail.htm";
                                        href = "<a href=" + link + ">Please create drawing and send for checking</a>";

                                        if (dtMailInfo.Rows[0]["CREATED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"])))
                                            createdBy = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY"]);

                                        if (dtMailInfo.Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"])))
                                            createdByEmail = Convert.ToString(dtMailInfo.Rows[0]["CREATED_BY_EMAIL"]);

                                        if (dtMailInfo.Rows[0]["AMENDED_OPEN_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["AMENDED_OPEN_BY"])))
                                            amendedOpenBy = Convert.ToString(dtMailInfo.Rows[0]["AMENDED_OPEN_BY"]);

                                        if (dtMailInfo.Rows[0]["AMENDED_OPEN_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["AMENDED_OPEN_BY_EMAIL"])))
                                            amendedOpenByEmail = Convert.ToString(dtMailInfo.Rows[0]["AMENDED_OPEN_BY_EMAIL"]);


                                        if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                            designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                        if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                            designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                        if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                            designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                        from = amendedOpenByEmail;
                                        fromName = amendedOpenBy;
                                        to = designRespeEnggEmail;
                                        toName = designRespeEngg;

                                        //cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail;
                                        if (PEID == PMID)
                                        {
                                            cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                        }
                                        else
                                        {
                                            cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                        }

                                        returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                    }
                                }
                            }




                            if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AssignmentMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' assigned on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/04AssignmentMail.htm";
                                href = "<a href=" + link + ">Please create drawing and send for checking</a>";

                                foreach (DataRow drc in dtMailInfo.Rows)
                                {
                                    if (drc["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drc["CREATED_BY_EMAIL"])))
                                        createdByEmail += Convert.ToString(drc["CREATED_BY_EMAIL"]) + ";";
                                }

                                if (!string.IsNullOrEmpty(createdByEmail))
                                {
                                    createdByEmail = createdByEmail.TrimEnd(';');
                                }

                                //DataTable dtEng = new DataTable();
                                //dtEng.Columns.Add("DESIGN_RESPONSIBLE_ENGG_ID", typeof(int));

                                //int count = 0;
                                //foreach (DataRow dre in dtMailInfo.Rows)
                                //{
                                //    if (dtEng.Rows.Count > 0)
                                //    {
                                //        count = 0;
                                //        foreach (DataRow dr0 in dtEng.Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                //        {
                                //            count++;
                                //        }

                                //        if (count == 0)
                                //        {
                                //            DataRow dr = dtEng.NewRow();
                                //            dr["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]);
                                //            dtEng.Rows.Add(dr);
                                //        }

                                //    }
                                //    else
                                //    {
                                //        DataRow dr = dtEng.NewRow();
                                //        dr["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]);
                                //        dtEng.Rows.Add(dr);
                                //    }
                                //}

                                if (dtEng.Rows.Count > 0)
                                {
                                    foreach (DataRow dre in dtEng.Rows)
                                    {
                                        foreach (DataRow dr in dsMailInfo.Tables[3].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                        {
                                            if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                                designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                                designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                                designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);


                                            from = managerEmail;
                                            fromName = manager;
                                            to = designRespeEnggEmail;
                                            toName = designRespeEngg;

                                            //cc = createdByEmail + ";" + managerEmail + ";" + managerCCEmail;
                                            if (PEID == PMID)
                                            {
                                                cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                            }
                                            else
                                            {
                                                cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                            }

                                            if (designRespeEnggID == 9)//For neeraj gupta
                                            {
                                                cc = cc + ";" + ncc;
                                            }

                                            returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                        }
                                    }
                                }


                                //foreach (DataRow dr in dsMailInfo.Tables[3].Rows)
                                //{
                                //    if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                //        designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                //        designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                //        designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);


                                //    from = managerEmail;
                                //    fromName = manager;
                                //    to = designRespeEnggEmail;
                                //    toName = designRespeEngg;
                                //    cc = createdByEmail + ";" + managerEmail + ";" + managerCCEmail;

                                //    returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                //}
                            }

                            else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CorrectionMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' sent to amendment on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/12CorrectionMail.htm";
                                href = "<a href=" + link + ">Please Amend Drawing</a>";

                                if (dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY_EMAIL"])))
                                    sentToAmendmentByEmail = Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY_EMAIL"]);

                                if (dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY"])))
                                    sentToAmendmentBy = Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_AMENDMENT_BY"]);





                                if (dtEng.Rows.Count > 0)
                                {
                                    foreach (DataRow dre in dtEng.Rows)
                                    {
                                        foreach (DataRow dr in dsMailInfo.Tables[3].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                        {
                                            if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                                designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                                designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                                designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);


                                            from = sentToAmendmentByEmail;
                                            fromName = sentToAmendmentBy;
                                            to = designRespeEnggEmail;
                                            toName = designRespeEngg;

                                            //cc = managerEmail + ";" + managerCCEmail + ";" + sentToAmendmentByEmail;
                                            if (PEID == PMID)
                                            {
                                                cc = managerEmail + ";" + managerCCEmail + ";" + sentToAmendmentByEmail + ";" + PMEmail;
                                            }
                                            else
                                            {
                                                cc = managerEmail + ";" + managerCCEmail + ";" + sentToAmendmentByEmail + ";" + PMEmail + ";" + PEEmail;
                                            }

                                            returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                        }
                                    }
                                }



                                //foreach (DataRow dr in dsMailInfo.Tables[3].Rows)
                                //{
                                //    if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                //        designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                //        designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                //        designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);


                                //    from = sentToAmendmentByEmail;
                                //    fromName = sentToAmendmentBy;
                                //    to = designRespeEnggEmail;
                                //    toName = designRespeEngg;
                                //    cc = managerEmail + ";" + managerCCEmail + ";" + sentToAmendmentByEmail;

                                //    returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                //}
                            }

                            else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedAssignmentMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' assigned on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/16AmendedAssignmentMail.htm";
                                href = "<a href=" + link + ">Please create drawing and send for checking</a>";


                                foreach (DataRow drc in dtMailInfo.Rows)
                                {
                                    if (drc["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drc["CREATED_BY_EMAIL"])))
                                        createdByEmail += Convert.ToString(drc["CREATED_BY_EMAIL"]) + ";";
                                }

                                if (!string.IsNullOrEmpty(createdByEmail))
                                {
                                    createdByEmail = createdByEmail.TrimEnd(';');
                                }



                                if (dtEng.Rows.Count > 0)
                                {
                                    foreach (DataRow dre in dtEng.Rows)
                                    {
                                        foreach (DataRow dr in dsMailInfo.Tables[3].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                        {
                                            if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                                designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                                designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                                designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);


                                            from = managerEmail;
                                            fromName = manager;
                                            to = designRespeEnggEmail;
                                            toName = designRespeEngg;

                                            //cc = createdByEmail + ";" + managerEmail + ";" + managerCCEmail;
                                            if (PEID == PMID)
                                            {
                                                cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail;
                                            }
                                            else
                                            {
                                                cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail + ";" + PMEmail + ";" + PEEmail;
                                            }

                                            returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                        }
                                    }
                                }



                                //foreach (DataRow dr in dsMailInfo.Tables[3].Rows)
                                //{
                                //    if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                //        designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                //        designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                //        designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                //    from = managerEmail;
                                //    fromName = manager;
                                //    to = designRespeEnggEmail;
                                //    toName = designRespeEngg;
                                //    cc = createdByEmail + ";" + managerEmail + ";" + managerCCEmail;

                                //    returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                //}
                            }

                        }


                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                        {
                            if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CheckingMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' sent for checking on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/05CheckingMail.htm";
                                href = "<a href=" + link + ">Please Check Drawing</a>";

                                if (dtMailInfo.Rows[0]["SENT_TO_CHECKING_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_CHECKING_BY"])))
                                    sentToCheckingBy = Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_CHECKING_BY"]);

                                if (dtMailInfo.Rows[0]["SENT_TO_CHECKING_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_CHECKING_BY_EMAIL"])))
                                    sentToCheckingByEmail = Convert.ToString(dtMailInfo.Rows[0]["SENT_TO_CHECKING_BY_EMAIL"]);



                                if (dtChe.Rows.Count > 0)
                                {
                                    foreach (DataRow drc in dtChe.Rows)
                                    {
                                        foreach (DataRow dr in dsMailInfo.Tables[4].Select("DESIGN_CHECKER_ID='" + Convert.ToInt32(drc["DESIGN_CHECKER_ID"]) + "'"))
                                        {
                                            if (dr["DESIGN_CHECKER_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CHECKER_EMAIL"])))
                                                designCheckerEmail = Convert.ToString(dr["DESIGN_CHECKER_EMAIL"]);

                                            if (dr["DESIGN_CHECKER_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_CHECKER_ID"]) > 0)
                                                designCheckerID = Convert.ToInt32(dr["DESIGN_CHECKER_ID"]);

                                            if (dr["DESIGN_CHECKER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CHECKER"])))
                                                designChecker = Convert.ToString(dr["DESIGN_CHECKER"]);


                                            from = sentToCheckingByEmail;
                                            fromName = sentToCheckingBy;
                                            to = designCheckerEmail;
                                            toName = designChecker;

                                            //cc = managerEmail + ";" + sentToCheckingByEmail;
                                            if (PEID == PMID)
                                            {
                                                cc = managerEmail + ";" + sentToCheckingByEmail + ";" + PMEmail;
                                            }
                                            else
                                            {
                                                cc = managerEmail + ";" + sentToCheckingByEmail + ";" + PMEmail + ";" + PEEmail;
                                            }

                                            returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                        }
                                    }
                                }



                                //foreach (DataRow dr in dsMailInfo.Tables[4].Rows)
                                //{
                                //    if (dr["DESIGN_CHECKER_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CHECKER_EMAIL"])))
                                //        designCheckerEmail = Convert.ToString(dr["DESIGN_CHECKER_EMAIL"]);

                                //    if (dr["DESIGN_CHECKER_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_CHECKER_ID"]) > 0)
                                //        designCheckerID = Convert.ToInt32(dr["DESIGN_CHECKER_ID"]);

                                //    if (dr["DESIGN_CHECKER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CHECKER"])))
                                //        designChecker = Convert.ToString(dr["DESIGN_CHECKER"]);


                                //    from = sentToCheckingByEmail;
                                //    fromName = sentToCheckingBy;
                                //    to = designCheckerEmail;
                                //    toName = designChecker;
                                //    cc = managerEmail + ";" + sentToCheckingByEmail;

                                //    returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                //}
                            }

                            else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedCheckingMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' sent for checking on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/17AmendedCheckingMail.htm";
                                href = "<a href=" + link + ">Please Check Drawing</a>";

                                if (dtMailInfo.Rows[0]["AMENDED_SENT_TO_CHECKING_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["AMENDED_SENT_TO_CHECKING_BY"])))
                                    amendedSentToCheckingBy = Convert.ToString(dtMailInfo.Rows[0]["AMENDED_SENT_TO_CHECKING_BY"]);

                                if (dtMailInfo.Rows[0]["AMENDED_SENT_TO_CHECKING_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["AMENDED_SENT_TO_CHECKING_BY_EMAIL"])))
                                    amendedSentToCheckingByEmail = Convert.ToString(dtMailInfo.Rows[0]["AMENDED_SENT_TO_CHECKING_BY_EMAIL"]);



                                if (dtChe.Rows.Count > 0)
                                {
                                    foreach (DataRow drc in dtChe.Rows)
                                    {
                                        foreach (DataRow dr in dsMailInfo.Tables[4].Select("DESIGN_CHECKER_ID='" + Convert.ToInt32(drc["DESIGN_CHECKER_ID"]) + "'"))
                                        {
                                            if (dr["DESIGN_CHECKER_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CHECKER_EMAIL"])))
                                                designCheckerEmail = Convert.ToString(dr["DESIGN_CHECKER_EMAIL"]);

                                            if (dr["DESIGN_CHECKER_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_CHECKER_ID"]) > 0)
                                                designCheckerID = Convert.ToInt32(dr["DESIGN_CHECKER_ID"]);

                                            if (dr["DESIGN_CHECKER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CHECKER"])))
                                                designChecker = Convert.ToString(dr["DESIGN_CHECKER"]);


                                            from = amendedSentToCheckingByEmail;
                                            fromName = amendedSentToCheckingBy;
                                            to = designCheckerEmail;
                                            toName = designChecker;

                                            //cc = managerEmail + ";" + managerCCEmail + ";" + sentToCheckingByEmail;
                                            if (PEID == PMID)
                                            {
                                                cc = managerEmail + ";" + managerCCEmail + ";" + sentToCheckingByEmail + ";" + PMEmail;
                                            }
                                            else
                                            {
                                                cc = managerEmail + ";" + managerCCEmail + ";" + sentToCheckingByEmail + ";" + PMEmail + ";" + PEEmail;
                                            }

                                            returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                        }
                                    }
                                }




                                //foreach (DataRow dr in dsMailInfo.Tables[4].Rows)
                                //{
                                //    if (dr["DESIGN_CHECKER_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CHECKER_EMAIL"])))
                                //        designCheckerEmail = Convert.ToString(dr["DESIGN_CHECKER_EMAIL"]);

                                //    if (dr["DESIGN_CHECKER_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_CHECKER_ID"]) > 0)
                                //        designCheckerID = Convert.ToInt32(dr["DESIGN_CHECKER_ID"]);

                                //    if (dr["DESIGN_CHECKER"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_CHECKER"])))
                                //        designChecker = Convert.ToString(dr["DESIGN_CHECKER"]);

                                //    from = amendedSentToCheckingByEmail;
                                //    fromName = amendedSentToCheckingBy;
                                //    to = designCheckerEmail;
                                //    toName = designChecker;
                                //    cc = managerEmail + ";" + managerCCEmail + ";" + sentToCheckingByEmail;

                                //    returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                //}
                            }

                        }


                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                        {
                            if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CheckedMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' checked on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/06CheckedMail.htm";


                                if (dtMailInfo.Rows[0]["CHECKED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CHECKED_BY"])))
                                    checkedBy = Convert.ToString(dtMailInfo.Rows[0]["CHECKED_BY"]);

                                if (dtMailInfo.Rows[0]["CHECKED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dtMailInfo.Rows[0]["CHECKED_BY_EMAIL"])))
                                    checkedByEmail = Convert.ToString(dtMailInfo.Rows[0]["CHECKED_BY_EMAIL"]);


                                foreach (DataRow drc in dtMailInfo.Rows)
                                {
                                    if (drc["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drc["CREATED_BY_EMAIL"])))
                                        createdByEmail += Convert.ToString(drc["CREATED_BY_EMAIL"]) + ";";
                                }

                                if (!string.IsNullOrEmpty(createdByEmail))
                                {
                                    createdByEmail = createdByEmail.TrimEnd(';');
                                }


                                if (dtEng.Rows.Count > 0)
                                {
                                    foreach (DataRow dre in dtEng.Rows)
                                    {
                                        foreach (DataRow dr in dsMailInfo.Tables[3].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                        {
                                            if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                                designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                                designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                                designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                            from = checkedByEmail;
                                            fromName = checkedBy;
                                            to = designRespeEnggEmail;
                                            toName = designRespeEngg;

                                            if (!string.IsNullOrEmpty(createdByEmail))
                                                cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail;
                                            else
                                                cc = managerEmail + ";" + managerCCEmail;


                                            if (PEID == PMID)
                                            {
                                                cc += ";" + cc + ";" + PMEmail;
                                            }
                                            else
                                            {
                                                cc += ";" + cc + ";" + PMEmail + ";" + PEEmail;
                                            }

                                            returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                        }
                                    }
                                }



                                //foreach (DataRow dr in dsMailInfo.Tables[3].Rows)
                                //{
                                //    foreach (DataRow dr5 in dsMailInfo.Tables[5].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                //    {
                                //        if (dr5["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr5["CREATED_BY_EMAIL"])))
                                //        {
                                //            if (!createdByEmail.Contains(Convert.ToString(dr5["CREATED_BY_EMAIL"])))
                                //                createdByEmail += Convert.ToString(dr5["CREATED_BY_EMAIL"]) + ";";
                                //        }
                                //    }

                                //    if (!string.IsNullOrEmpty(createdByEmail))
                                //        createdByEmail = createdByEmail.TrimEnd(';');

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                //        designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                //        designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                //    if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                //        designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);

                                //    from = checkedByEmail;
                                //    fromName = checkedBy;
                                //    to = designRespeEnggEmail;
                                //    toName = designRespeEngg;

                                //    if (!string.IsNullOrEmpty(createdByEmail))
                                //        cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail;
                                //    else
                                //        cc = managerEmail + ";" + managerCCEmail;

                                //    returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                //}


                            }

                            else if (mailTypeID == Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedCheckedMail))
                            {
                                subject = "DMS Alert: Attached drawings of job No. '" + jobNo + "' checked on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                                fileName = "~/PROJECT/DMS/EMAIL_FORMATS/18AmendedCheckedMail.htm";


                                foreach (DataRow drc in dtMailInfo.Rows)
                                {
                                    if (drc["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drc["CREATED_BY_EMAIL"])))
                                        createdByEmail += Convert.ToString(drc["CREATED_BY_EMAIL"]) + ";";
                                }

                                if (!string.IsNullOrEmpty(createdByEmail))
                                {
                                    createdByEmail = createdByEmail.TrimEnd(';');
                                }




                                if (dtEng.Rows.Count > 0)
                                {
                                    foreach (DataRow dre in dtEng.Rows)
                                    {
                                        foreach (DataRow dr in dsMailInfo.Tables[3].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dre["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                        {
                                            if (dr["CREATED_BY_ID"] != DBNull.Value && Convert.ToInt32(dr["CREATED_BY_ID"]) > 0)
                                                createdByID = Convert.ToInt32(dr["CREATED_BY_ID"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                                designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                                designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                            if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                                designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);


                                            if (dr["CHECKED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CHECKED_BY"])))
                                                checkedBy = Convert.ToString(dr["CHECKED_BY"]);

                                            if (dr["CHECKED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CHECKED_BY_EMAIL"])))
                                                checkedByEmail = Convert.ToString(dr["CHECKED_BY_EMAIL"]);

                                            from = checkedByEmail;
                                            fromName = checkedBy;
                                            to = designRespeEnggEmail;
                                            toName = designRespeEngg;

                                            if (!string.IsNullOrEmpty(createdByEmail))
                                                cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail;
                                            else
                                                cc = managerEmail + ";" + managerCCEmail;

                                            if (PEID == PMID)
                                            {
                                                cc += ";" + cc + ";" + PMEmail;
                                            }
                                            else
                                            {
                                                cc += ";" + cc + ";" + PMEmail + ";" + PEEmail;
                                            }

                                            returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                        }
                                    }
                                }



                                //foreach (DataRow dr0 in dsMailInfo.Tables[5].Rows)
                                //{
                                //    foreach (DataRow dr in dtMailInfo.Select("CREATED_BY_ID='" + Convert.ToInt32(dr0["CREATED_BY_ID"]) + "'"))
                                //    {

                                //        foreach (DataRow dr5 in dsMailInfo.Tables[5].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                                //        {
                                //            if (dr5["CREATED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr5["CREATED_BY_EMAIL"])))
                                //            {
                                //                if (!createdByEmail.Contains(Convert.ToString(dr5["CREATED_BY_EMAIL"])))
                                //                    createdByEmail += Convert.ToString(dr5["CREATED_BY_EMAIL"]) + ";";
                                //            }
                                //        }

                                //        if (dr["CREATED_BY_ID"] != DBNull.Value && Convert.ToInt32(dr["CREATED_BY_ID"]) > 0)
                                //            createdByID = Convert.ToInt32(dr["CREATED_BY_ID"]);

                                //        if (dr["DESIGN_RESPONSIBLE_ENGG"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"])))
                                //            designRespeEngg = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG"]);

                                //        if (dr["DESIGN_RESPONSIBLE_ENGG_ID"] != DBNull.Value && Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                //            designRespeEnggID = Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);

                                //        if (dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"])))
                                //            designRespeEnggEmail = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_EMAIL"]);


                                //        if (dr["CHECKED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CHECKED_BY"])))
                                //            checkedBy = Convert.ToString(dr["CHECKED_BY"]);

                                //        if (dr["CHECKED_BY_EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CHECKED_BY_EMAIL"])))
                                //            checkedByEmail = Convert.ToString(dr["CHECKED_BY_EMAIL"]);

                                //        from = checkedByEmail;
                                //        fromName = checkedBy;
                                //        to = designRespeEnggEmail;
                                //        toName = designRespeEngg;

                                //        if (!string.IsNullOrEmpty(createdByEmail))
                                //            cc = managerEmail + ";" + managerCCEmail + ";" + createdByEmail;
                                //        else
                                //            cc = managerEmail + ";" + managerCCEmail;


                                //        returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, dtMailInfo, createdByID, designRespeEnggID, designCheckerID, statusID, href, jobNo);
                                //    }
                                //}
                            }
                        }
                    }
                }
            }


        }
        else
            returnVal = 0;


        return returnVal;
    }

    private int SendMail(string from, string fromName, string to, string toName, string cc, string bcc,
                         string subject, string fileName, DataTable dtMailInfoForAttachment, int createdByID, int designEnggID, int designCheckerID, int statusID, string href, string jobNo)
    {
        returnVal = 0;

        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
        link = "'" + urlTxt + "/Login.aspx?dmsstatusid=" + statusID + "'";



        if (!string.IsNullOrEmpty(from))
            mail.From = new MailAddress(from);


        if (!string.IsNullOrEmpty(subject))
            mail.Subject = subject;

        if (!string.IsNullOrEmpty(to))
        {
            to = to.TrimEnd(';');
            string[] strTo = to.Split(';');
            foreach (string item in strTo)
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

        body = body.Replace("{#managername#}", manager);
        body = body.Replace("{#drawingno#}", drawingNo);
        body = body.Replace("{#link#}", href);
        body = body.Replace("{#createdbyname#}", createdBy);
        body = body.Replace("{#designrespeengg#}", designRespeEngg);
        body = body.Replace("{#checkername#}", designChecker);
        body = body.Replace("{#checkedbyname#}", checkedBy);
        body = body.Replace("{#amendmentreason#}", sentToAmendmentRemarks);
        body = body.Replace("{#senttoamendmentname#}", sentToAmendmentBy);

        if (!string.IsNullOrEmpty(toName))
            body = body.Replace("{#toname#}", toName);
        else
            body = body.Replace("{#toname#}", "Sir/Madam");

        if (!string.IsNullOrEmpty(fromName))
            body = body.Replace("{#fromname#}", fromName);
        else
            body = body.Replace("{#fromname#}", "IT Team");


        mail.Body = body;

        byte[] bytes = GetPDFBytes(dtMailInfoForAttachment, createdByID, designEnggID, designCheckerID, statusID, jobNo);
        mail.Attachments.Add(new Attachment(new MemoryStream(bytes), jobNo + ".pdf"));


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
            {
                returnVal = 1;
                return returnVal;
            }

            else
            {
                returnVal = 0;
                return returnVal;
            }
        }

        return returnVal;
    }

    public byte[] GetPDFBytes(DataTable dtMailInfo, int createdByID, int designEnggID, int designCheckerID, int statusID, string JOBNo)
    {

        try
        {
            byte[] pdfBytes = null;
            var cssText = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Styles/LOT.css"));
            string htmltxt = objDMSHtmlForPDF.GetHtmlForPDF(dtMailInfo, createdByID, designEnggID, designCheckerID, statusID, JOBNo);

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

            if (!string.IsNullOrEmpty(Convert.ToString(html)))
            {
                string imagePath = System.Web.Hosting.HostingEnvironment.MapPath("\\Images\\COPERION") + "\\logo2.png";
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                img.Alignment = Element.ALIGN_LEFT;
                img.ScaleToFit(180f, 250f);


                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document(PageSize.A2);
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
                }
            }
            return pdfBytes;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}