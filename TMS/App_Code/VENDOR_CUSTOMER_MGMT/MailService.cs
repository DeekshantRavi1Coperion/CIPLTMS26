using System;
//using System.Collections.Generic;
using System.Data;
using System.IO;
//using System.Linq;
using System.Net.Mail;
using System.Text;
//using System.Web;
using BAL;
//using System.Web.UI;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;


public class MailService
{

    #region VARIABLES[===================]


    GetLOTMailType objGetLOTMailType = new GetLOTMailType();

    //GetLOTNextStatus objGetLOTNextStatus = new GetLOTNextStatus();
    BAL.VendorCustomerManagement objVCM = new VendorCustomerManagement();
    VendorHtmlForPDF objVendorHtmlForPDF = new VendorHtmlForPDF();
    //LOTMailInfoProperties objLOTMailInfoProperties = new LOTMailInfoProperties();
    //Project objProject = new Project();
    //DataSet dsMailInfo = new DataSet();

    //int isTransferred = 0;

    //int oldTransferredLotTfIdCreatedById = 0;
    //string oldTransferredLotTfIdCreatedBy = "";
    //string oldTransferredLotTfIdCreatedByEmail = "";

    //int currentStatusID = 0;
    ////int nextStatusID = 0;
    int returnVal = 0;
    //bool isCCAllowed = false;
    //int mailType = 0;
    //string LOTMainItem = "";
    //int LOTMainItemID = 0;
    //int LOTMainSubitemID = 0;
    //int newStatusID = 0;
    //int recordNo = 0;
    //int LOTTFSubitemID = 0;

    //int createdByID = 0;
    //string createdByName = string.Empty;
    //string createdByEmail = string.Empty;

    //int amendedByID = 0;
    //string amendedByName = string.Empty;
    //string amendedByEmail = string.Empty;

    //int PEID = 0;
    //string PEName = string.Empty;
    //string PEEmail = string.Empty;
    //string PEUD = string.Empty;
    //string PEPD = string.Empty;

    //int PMID = 0;
    //string PMName = string.Empty;
    //string PMEmail = string.Empty;
    //string PMUD = string.Empty;
    //string PMPD = string.Empty;


    //int planningMngrID = 0;
    //string planningMngrIDs = string.Empty;
    //string planningMngrName = string.Empty;
    //string planningMngrEmail = string.Empty;
    //string planningMngrEmailCC = string.Empty;

    //int prodMngrID = 0;
    //string prodMngrName = string.Empty;
    //string prodMngrEmail = string.Empty;
    //string prodMngrEmailCC = string.Empty;


    //string qualityMngrName = string.Empty;
    //string qualityNames = string.Empty;
    //string qualityEmails = string.Empty;

    //string planningMngrName = string.Empty;
    //string planningNames = string.Empty;
    //string planningEmails = string.Empty;

    //int approvedByID = 0;
    //string approvedByName = string.Empty;
    //string approvedByEmail = string.Empty;

    //int planningAcceptedByID = 0;
    //string planningAcceptedByName = string.Empty;
    //string planningAcceptedByEmail = string.Empty;

    //int forwardedByID = 0;
    //string forwardedByName = string.Empty;
    //string forwardedByEmail = string.Empty;

    //int acceptedByID = 0;
    //string acceptedByName = string.Empty;
    //string acceptedByEmail = string.Empty;

    //int qualityAcceptedByID = 0;
    //string qualityAcceptedByName = string.Empty;
    //string qualityAcceptedByEmail = string.Empty;



    //int sendToIntlInspectionByID = 0;
    //string sendToIntlInspectionByName = string.Empty;
    //string sendToIntlInspectionByEmail = string.Empty;

    //int qAIntlInspectionAcceptedByID = 0;
    //string qAIntlInspectionAcceptedByName = string.Empty;
    //string qAIntlInspectionAcceptedByEmail = string.Empty;

    //int qAIntlInspectionNotAcceptedByID = 0;
    //string qAIntlInspectionNotAcceptedByName = string.Empty;
    //string qAIntlInspectionNotAcceptedByEmail = string.Empty;

    //int sentToFinalInspByID = 0;
    //string sentToFinalInspByName = string.Empty;
    //string sentToFinalInspByEmail = string.Empty;

    //int sentToReworkByID = 0;
    //string sentToReworkByName = string.Empty;
    //string sentToReworkByEmail = string.Empty;


    //int amendmentCount = 0;

    //int amendmentByID = 0;
    //string amendmentByName = string.Empty;
    //string amendmentByEmail = string.Empty;

    //int prodAmendmentByID = 0;
    //string prodAmendmentByName = string.Empty;
    //string prodAmendmentByEmail = string.Empty;

    //int amendedApprovedByID = 0;
    //string amendedApprovedByName = string.Empty;
    //string amendedApprovedByEmail = string.Empty;

    //int planningAmendedAcceptedByID = 0;
    //string planningAmendedAcceptedByName = string.Empty;
    //string planningAmendedAcceptedByEmail = string.Empty;

    //int forwardedAmendedByID = 0;
    //string forwardedAmendedByName = string.Empty;
    //string forwardedAmendedByEmail = string.Empty;

    //int amendedAcceptedByID = 0;
    //string amendedAcceptedByName = string.Empty;
    //string amendedAcceptedByEmail = string.Empty;

    //int qualityAmendedAcceptedByID = 0;
    //string qualityAmendedAcceptedByName = string.Empty;
    //string qualityAmendedAcceptedByEmail = string.Empty;


    //int completedByID = 0;
    //string completedByName = string.Empty;
    //string completedByEmail = string.Empty;

    //string storePersonName = string.Empty;
    //string storePersonEmail = string.Empty;
    //string valvesAdditionalEmailCC = string.Empty;

    int signPid = 0;

    string responsibleEmail = string.Empty;

    //string PEApproveHref = string.Empty;
    //string PEApproveLink = string.Empty;

    //string PEAmendmentHref = string.Empty;
    //string PEAmendmentLink = string.Empty;

    //string PMApproveHref = string.Empty;
    //string PMApproveLink = string.Empty;

    //string PMAmendmentHref = string.Empty;
    //string PMAmendmentLink = string.Empty;



    string vendorCode = string.Empty;
    string vendorName = string.Empty;

    string fileName = string.Empty;
    string salutation = string.Empty;
    string closingLine = string.Empty;
    string body = string.Empty;
    string amendmentRemarks = string.Empty;
    string subject = string.Empty;
    string fromName = string.Empty;
    string from = string.Empty;
    string toName = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;
    string PAN = string.Empty;
    string checker = string.Empty;
    string checkerEmail = string.Empty;



    //bool isPlannigMailSent = false;
    //bool isQualityMailSent = false;

    //DataTable dtQuantityDetails = new DataTable();
    //DataTable dtProductionOrderNumberOfOldTransferredLOT = new DataTable();
    //DataTable dtQuantityList = new DataTable();
    //DataTable dtProductionStatusList = new DataTable();

    #endregion



    public int ProcessMail(int pid, int statusId)
    {
        try
        {

            #region Variables

            signPid = 0;
            string urlTxt = string.Empty;
            string href = string.Empty;
            string mlink = string.Empty;

            vendorCode = string.Empty;
            vendorName = string.Empty;

            fileName = string.Empty;
            salutation = string.Empty;
            closingLine = string.Empty;
            body = string.Empty;
            subject = string.Empty;
            fromName = string.Empty;
            from = string.Empty;
            to = string.Empty;
            toName = string.Empty;
            cc = string.Empty;
            bcc = string.Empty;
            //link = string.Empty;
            responsibleEmail = string.Empty;
            checker = string.Empty;
            checkerEmail = string.Empty;

            #endregion


            DataSet dsMailInfo = new DataSet();
            dsMailInfo = objVCM.GetMailInfo(pid, statusId);
            DataRow dr0 = dsMailInfo.Tables[0].Rows[0];
            DataTable dtApprovers = dsMailInfo.Tables[1];
            //DataTable dtDoc = dsMailInfo.Tables[2];

            signPid = Convert.ToInt32(dr0["SIGNATORY_PID"]);
            PAN = Convert.ToString(dr0["PAN"]);

            salutation = Convert.ToString(dr0["SALUTATION"]);
            closingLine = Convert.ToString(dr0["CLOSING_LINE"]);

            vendorCode = Convert.ToString(dr0["CODE"]);
            vendorName = Convert.ToString(dr0["NAME"]);

            if (!string.IsNullOrEmpty(vendorCode))
                vendorName = vendorName + " [" + vendorCode + "]";

            body = Convert.ToString(dr0["BODY_LINE"]).Replace("#VENDOR_NAME#", vendorName);
            amendmentRemarks = Convert.ToString(dr0["AMENDMENT_REMARKS"]);

            subject = Convert.ToString(dr0["SUBJECT"]).Replace("#VENDOR_NAME#", vendorName);
            cc = Convert.ToString(dr0["CC"]);
            bcc = Convert.ToString(dr0["BCC"]);
            //link = Convert.ToString(dr0["LINK"]);
            responsibleEmail = Convert.ToString(dr0["RESPOINSIBLE_EMAIL"]);

            checker = Convert.ToString(dr0["CHECKER"]);
            checkerEmail = Convert.ToString(dr0["CHECKER_EMAIL"]);


            urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
            mlink = "'" + urlTxt + "/VENDOR_CUSTOMER_MGMT/VENDOR/VendorsList.aspx?PAN=" + PAN + "&name=" + vendorName + "'";


            //Get Mail Info
            if (statusId == (int)StatusAndTypes.EnumStatus.Created_1)
            {
                href = "<a href=" + mlink + ">Check & Approve Vendor</a>";

                fromName = Convert.ToString(dr0["CREATED_BY"]);
                from = Convert.ToString(dr0["CREATED_BY_EMAIL"]);
                toName = checker;  // "Sir";

                //foreach (DataRow dr in dtApprovers.Select("APPROVER_TYPE_FID ="+ (int)StatusAndTypes.EnumApproverType.Checker_1))
                //{
                //    to += dr["APPROVER_EMAIL"] + ";";
                //}

                to = checkerEmail;

                cc = Convert.ToString(dr0["CREATED_BY_EMAIL"]) + ";" +
                     responsibleEmail;
            }
            else if (statusId == (int)StatusAndTypes.EnumStatus.Amendment_2)
            {
                href = "<a href=" + mlink + ">Amend Vendor</a>";

                fromName = Convert.ToString(dr0["SENT_TO_AMENDMENT_BY"]);
                from = Convert.ToString(dr0["SENT_TO_AMENDMENT_BY_EMAIL"]);
                toName = Convert.ToString(dr0["CREATED_BY"]);
                to = Convert.ToString(dr0["CREATED_BY_EMAIL"]);
                cc = Convert.ToString(dr0["SENT_TO_AMENDMENT_BY_EMAIL"]) + ";" +
                     responsibleEmail;
            }
            else if (statusId == (int)StatusAndTypes.EnumStatus.Amended_3)
            {
                href = "<a href=" + mlink + ">Check & Approve Vendor</a>";

                fromName = Convert.ToString(dr0["AMENDED_BY"]);
                from = Convert.ToString(dr0["AMENDED_BY_EMAIL"]);
                toName = checker;// "Sir";
                                 //to = Convert.ToString(dr0["SENT_TO_AMENDMENT_BY_EMAIL"]);

                //foreach (DataRow dr in dtApprovers.Select("APPROVER_TYPE_FID =" + (int)StatusAndTypes.EnumApproverType.Checker_1 ))
                //{
                //    to += dr["APPROVER_EMAIL"] + ";";
                //}

                to = checkerEmail;

                //cc = Convert.ToString(dr0["CREATED_BY_EMAIL"]) + ";" +
                //     responsibleEmail;
                cc = Convert.ToString(dr0["AMENDED_BY_EMAIL"]) + ";" +
                     responsibleEmail;
            }
            else if (statusId == (int)StatusAndTypes.EnumStatus.Checked_4)
            {
                href = "<a href=" + mlink + ">Check & Approve Vendor</a>";

                fromName = Convert.ToString(dr0["CHECKED_BY"]);
                from = Convert.ToString(dr0["CHECKED_BY_EMAIL"]);
                toName = "Team";

                //Procurement Head
                foreach (DataRow dr in dtApprovers.Select("APPROVER_TYPE_FID = " + (int)StatusAndTypes.EnumApproverType.ProcurementHead_2))
                {
                    to += dr["APPROVER_EMAIL"] + ";";
                }
                cc = Convert.ToString(dr0["CHECKED_BY_EMAIL"]) + ";" +
                     responsibleEmail;
            }
            else if (statusId == (int)StatusAndTypes.EnumStatus.PROCHODApproved_5)
            {
                href = "<a href=" + mlink + ">Check & Approve Vendor</a>";

                fromName = Convert.ToString(dr0["PROC_HOD_APPROVED_BY"]);
                from = Convert.ToString(dr0["PROC_HOD_APPROVED_BY_EMAIL"]);
                toName = "Team";

                //Accounts Checkers
                foreach (DataRow dr in dtApprovers.Select("APPROVER_TYPE_FID = " + (int)StatusAndTypes.EnumApproverType.AccountsChecker_3))
                {
                    to += dr["APPROVER_EMAIL"] + ";";
                }
                cc = Convert.ToString(dr0["CHECKED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["PROC_HOD_APPROVED_BY_EMAIL"]) + ";" +
                     responsibleEmail;
            }
            else if (statusId == (int)StatusAndTypes.EnumStatus.AccountsChecked_6)
            {
                href = "<a href=" + mlink + ">Check & Approve Vendor</a>";

                fromName = Convert.ToString(dr0["ACCOUNTS_CHECKED_BY"]);
                from = Convert.ToString(dr0["ACCOUNTS_CHECKED_BY_EMAIL"]);
                toName = "Team";

                //Final Approver
                foreach (DataRow dr in dtApprovers.Select("APPROVER_TYPE_FID = " + (int)StatusAndTypes.EnumApproverType.FinalApprover_4))
                {
                    to += dr["APPROVER_EMAIL"] + ";";
                }
                cc = Convert.ToString(dr0["CHECKED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["PROC_HOD_APPROVED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["ACCOUNTS_CHECKED_BY_EMAIL"]) + ";" +
                     responsibleEmail;

            }
            else if (statusId == (int)StatusAndTypes.EnumStatus.FinalApproved_7)
            {
                href = "<a href=" + mlink + ">Register Vendor</a>";

                fromName = Convert.ToString(dr0["FINAL_APPROVED_BY"]);
                from = Convert.ToString(dr0["FINAL_APPROVED_BY_EMAIL"]);
                toName = "Team";

                //IT Team
                foreach (DataRow dr in dtApprovers.Select("APPROVER_TYPE_FID = " + (int)StatusAndTypes.EnumApproverType.ITTeam_5))
                {
                    to += dr["APPROVER_EMAIL"] + ";";
                }
                cc = Convert.ToString(dr0["CHECKED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["PROC_HOD_APPROVED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["ACCOUNTS_CHECKED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["FINAL_APPROVED_BY_EMAIL"]) + ";" +
                     responsibleEmail;
            }
            else if (statusId == (int)StatusAndTypes.EnumStatus.Registered_8)
            {
                fromName = Convert.ToString(dr0["REGISTERED_BY"]);
                from = Convert.ToString(dr0["REGISTERED_BY_EMAIL"]);
                toName = Convert.ToString(dr0["CREATED_BY"]);
                to = Convert.ToString(dr0["CREATED_BY_EMAIL"]);

                cc = Convert.ToString(dr0["CHECKED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["PROC_HOD_APPROVED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["ACCOUNTS_CHECKED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["FINAL_APPROVED_BY_EMAIL"]) + ";" +
                     Convert.ToString(dr0["REGISTERED_BY_EMAIL"]) + ";" +
                     responsibleEmail;
            }
            else if (statusId == (int)StatusAndTypes.EnumStatus.Revised_9)
            {
                href = "<a href=" + mlink + ">Check & Approve Revised Vendor</a>";

                fromName = Convert.ToString(dr0["REVISED_BY"]);
                from = Convert.ToString(dr0["REVISED_BY_EMAIL"]);

                toName = checker;// "Sir";
                //foreach (DataRow dr in dtApprovers.Select("APPROVER_TYPE_FID = " + (int)StatusAndTypes.EnumApproverType.Checker_1 ))
                //{
                //    to += dr["APPROVER_EMAIL"] + ";";
                //}
                to = checkerEmail;

                cc = Convert.ToString(dr0["REVISED_BY_EMAIL"]) + ";" +
                     responsibleEmail;
            }


            int mailValue = 0;

            mailValue = SendMail(pid
                        , vendorName
                        , from
                        , to
                        , cc
                        , bcc
                        , subject
                        , salutation
                        , body
                        , amendmentRemarks
                        , closingLine
                        , href
                        , null);
            //, dtDoc);


            if (mailValue > 0)
            {
                return signPid;
            }

            return 0;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }


    private int SendMail(int pid
                        , string vendorName
                        , string from
                        , string to
                        , string cc
                        , string bcc
                        , string subject
                        , string salutation
                        , string bodyLine
                        , string amendmentRemarks
                        , string closingLine
                        , string href
                        , DataTable dtDoc
        )
    {

        //to = "deekshant.ravi@coperion.com";
        //cc = "deekshant.ravi@coperion.com";
        //bcc = "deekshant.ravi@coperion.com";

        //DataView dv = dtSubitem.DefaultView;
        //dv.Sort = "LOT_TF_SUBITEM_ID";
        //dtSubitem = dv.ToTable();

        returnVal = 0;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();


        if (!string.IsNullOrEmpty(to))
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
            fileName = "~/VENDOR_CUSTOMER_MGMT/VENDOR/EMailFormat.htm";
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#salutation#}", salutation);
            body = body.Replace("{#fromName#}", fromName);
            body = body.Replace("{#bodyLine#}", bodyLine);
            body = body.Replace("{#amendmentRemarks#}", amendmentRemarks);

            body = body.Replace("{#closingLine#}", closingLine);
            body = body.Replace("{#toName#}", toName);

            body = body.Replace("{#link#}", href);

            mail.Body = body;

            //byte[] bytes = GetPDFBytes(pid);
            //if (bytes != null)
            //{
            //    mail.Attachments.Add(new Attachment(new MemoryStream(bytes), vendorName + ".pdf"));
            //}

            //if (dtDoc.Rows.Count > 0 && dtDoc != null)
            //{
            //    foreach (DataRow drd in dtDoc.Rows)
            //    {
            //        byte[] docBytes = (byte[])drd["DOC"];
            //        string docName = Convert.ToString(drd["DOC_NAME"]);
            //        mail.Attachments.Add(new Attachment(new MemoryStream(docBytes), docName));
            //    }
            //}

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

    public byte[] GetPDFBytes(int pid)
    {
        try
        {
            DataSet dsDetails = new DataSet();
            byte[] pdfBytes;
            var cssText = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Styles/LOT.css"));

            dsDetails = objVCM.GetVendorForPDF(pid);
            string htmltxt = objVendorHtmlForPDF.GetHtmlForPDF(pid, dsDetails);

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

}