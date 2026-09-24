using System;
//using System.Collections.Generic;
using System.Data;
using System.IO;
//using System.Linq;
using System.Net.Mail;
using System.Text;
//using System.Web;
using BAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
//using System.Web.UI;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;


public class MSSendMail
{

    #region VARIABLES[===================]

    MSHtmlForPDF objMSHtmlForPDF = new MSHtmlForPDF();
    MachineScheduling objMS = new MachineScheduling();
    DataTable dtMailInfo = new DataTable();
    DMSHtmlForPDFForMail objDMSHtmlForPDF = new DMSHtmlForPDFForMail();
    DataSet dsDetail = new DataSet();

    int returnVal = 0;
    string jobNo = string.Empty;
    string drawingNo = string.Empty;

    int productionManagerID = 0;
    string productionManager = string.Empty;
    string productionManagerEmail = string.Empty;

    int assignedByID = 0;
    string assignedBy = string.Empty;
    string assignedOn = string.Empty;
    string assignedByEmail = string.Empty;
    string assignedRemarks = string.Empty;

    int reAssignedByID = 0;
    string reAssignedBy = string.Empty;
    string reAssignedOn = string.Empty;
    string reAssignedByEmail = string.Empty;
    string reAssignedRemarks = string.Empty;

    int scheduledByID = 0;
    string scheduledBy = string.Empty;
    string scheduledOn = string.Empty;
    string scheduledByEmail = string.Empty;
    string scheduledRemarks = string.Empty;

    int acceptedByID = 0;
    string acceptedBy = string.Empty;
    string acceptedOn = string.Empty;
    string acceptedByEmail = string.Empty;
    string acceptedRemarks = string.Empty;

    int rejectedByID = 0;
    string rejectedBy = string.Empty;
    string rejectedOn = string.Empty;
    string rejectedByEmail = string.Empty;
    string rejectedRemarks = string.Empty;

    int completedByID = 0;
    string completedBy = string.Empty;
    string completedOn = string.Empty;
    string completedByEmail = string.Empty;
    string completedRemarks = string.Empty;

    int inspectedByID = 0;
    string inspectedBy = string.Empty;
    string inspectedByEmail = string.Empty;
    string totalAcceptedQuantity = "0";

    //int qaAcceptedByID = 0;
    //string qaAcceptedBy = string.Empty;
    //string qaAcceptedOn = string.Empty;
    //string qaAcceptedByEmail = string.Empty;
    //string qaAcceptedRemarks = string.Empty;

    //int qaRejectedByID = 0;
    //string qaRejectedBy = string.Empty;
    //string qaRejectedOn = string.Empty;
    //string qaRejectedByEmail = string.Empty;
    //string qaRejectedRemarks = string.Empty;

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

    public int ProcessAndSendAssignedMail(DataSet dsMailInfo, DataTable dtMachineShopInc, int mailTypeID, string fromName, string fromEmail)
    {
        returnVal = 0;

        DataTable dtMailInfo = new DataTable();
        DataTable dtDistinctJobNo = new DataTable();

        fileName = string.Empty;
        urlTxt = string.Empty;
        link = string.Empty;
        href = string.Empty;


        urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
        link = "'" + urlTxt + "/Login.aspx'";

        if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.AssignedMail))
        {
            if (dsMailInfo.Tables.Count > 0)
            {
                if (dsMailInfo.Tables[0].Rows.Count > 0)
                {
                    dtMailInfo = dsMailInfo.Tables[0];
                }

                if (dsMailInfo.Tables[1].Rows.Count > 0)
                {
                    dtDistinctJobNo = dsMailInfo.Tables[1];
                }
            }
        }
        else
        {
            //
        }


        if (dtMailInfo.Rows.Count > 0 && dtDistinctJobNo.Rows.Count > 0)
        {
            if (dtMachineShopInc.Rows.Count > 0)
            {
                foreach (DataRow drm in dtMachineShopInc.Rows)
                {
                    from = fromEmail;
                    if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.AssignedMail))
                    {
                        to = Convert.ToString(drm["EMAIL_ID"]);
                        toName = Convert.ToString(drm["EMPLOYEE_NAME"]);

                        subject = "MS Alert: Drawings assigned to Machine Shop";
                        fileName = "~/PROJECT/MACHINE_SCH/EMAIL_FORMATS/01AssiegnedMail.htm";

                        href = "<a href=" + link + ">Accept/Reject</a>";
                    }

                    returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, href, dtMailInfo, dtDistinctJobNo, null, null, mailTypeID, "", "", "");
                }
            }
            else
                returnVal = 0;
        }
        else
            returnVal = 0;



        return returnVal;
    }


    public int ProcessAndSendAcceptanceMail(int recordID, string assignedDrawingCode, int mailTypeID, string drawingNo, string remarks)
    {
        returnVal = 0;

        DataTable dtMailInfo = new DataTable();
        DataTable dtScheudledDetails = new DataTable();
        DataTable dtInspectionDetails = new DataTable();

        fileName = string.Empty;
        urlTxt = string.Empty;
        link = string.Empty;
        href = string.Empty;


        urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
        link = "'" + urlTxt + "/Login.aspx'";


        dsDetail = objMS.GetMailInfo(recordID);
        if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
        {
            dtMailInfo = dsDetail.Tables[0];

            if (dsDetail.Tables[1].Rows.Count > 0)
            {
                dtScheudledDetails = dsDetail.Tables[1];
            }

            if (dsDetail.Tables[3].Rows.Count > 0)
            {
                dtInspectionDetails = dsDetail.Tables[3];
            }
            

            DataRow dr0 = dsDetail.Tables[0].Rows[0];

            int productionManagerID = 0;
            string productionManager = string.Empty;
            string productionManagerEmail = string.Empty;

            assignedByID = 0;
            assignedBy = string.Empty;
            assignedOn = string.Empty;
            assignedByEmail = string.Empty;
            assignedRemarks = string.Empty;

            reAssignedByID = 0;
            reAssignedBy = string.Empty;
            reAssignedOn = string.Empty;
            reAssignedByEmail = string.Empty;
            reAssignedRemarks = string.Empty;

            scheduledByID = 0;
            scheduledBy = string.Empty;
            scheduledOn = string.Empty;
            scheduledByEmail = string.Empty;
            scheduledRemarks = string.Empty;

            acceptedByID = 0;
            acceptedBy = string.Empty;
            acceptedOn = string.Empty;
            acceptedByEmail = string.Empty;
            acceptedRemarks = string.Empty;

            rejectedByID = 0;
            rejectedBy = string.Empty;
            rejectedOn = string.Empty;
            rejectedByEmail = string.Empty;
            rejectedRemarks = string.Empty;

            completedByID = 0;
            completedBy = string.Empty;
            completedOn = string.Empty;
            completedByEmail = string.Empty;
            completedRemarks = string.Empty;

            inspectedByID = 0;
            inspectedBy = string.Empty;
            inspectedByEmail = string.Empty;
            totalAcceptedQuantity = "0";

            //qaAcceptedByID = 0;
            //qaAcceptedBy = string.Empty;
            //qaAcceptedOn = string.Empty;
            //qaAcceptedByEmail = string.Empty;
            //qaAcceptedRemarks = string.Empty;

            //qaRejectedByID = 0;
            //qaRejectedBy = string.Empty;
            //qaRejectedOn = string.Empty;
            //qaRejectedByEmail = string.Empty;
            //qaRejectedRemarks = string.Empty;

            if (dr0["PRODUCTION_MANAGER_ID"] != DBNull.Value)
                productionManagerID = Convert.ToInt32(dr0["PRODUCTION_MANAGER_ID"]);

            if (dr0["PRODUCTION_MANAGER_NAME"] != DBNull.Value)
                productionManager = Convert.ToString(dr0["PRODUCTION_MANAGER_NAME"]);

            if (dr0["PRODUCTION_MANAGER_EMAIL_ID"] != DBNull.Value)
                productionManagerEmail = Convert.ToString(dr0["PRODUCTION_MANAGER_EMAIL_ID"]);


            if (dr0["ASSIGNED_BY_ID"] != DBNull.Value)
                assignedByID = Convert.ToInt32(dr0["ASSIGNED_BY_ID"]);

            if (dr0["ASSIGNED_BY"] != DBNull.Value)
                assignedBy = Convert.ToString(dr0["ASSIGNED_BY"]);

            if (dr0["ASSIGNED_ON"] != DBNull.Value)
                assignedOn = Convert.ToString(dr0["ASSIGNED_ON"]);

            if (dr0["ASSIGNED_REMARKS"] != DBNull.Value)
                assignedRemarks = Convert.ToString(dr0["ASSIGNED_REMARKS"]);

            if (dr0["ASSIGNED_BY_EMAIL"] != DBNull.Value)
                assignedByEmail = Convert.ToString(dr0["ASSIGNED_BY_EMAIL"]);


            if (dr0["RE_ASSIGNED_BY_ID"] != DBNull.Value)
                reAssignedByID = Convert.ToInt32(dr0["RE_ASSIGNED_BY_ID"]);

            if (dr0["RE_ASSIGNED_BY"] != DBNull.Value)
                reAssignedBy = Convert.ToString(dr0["RE_ASSIGNED_BY"]);

            if (dr0["RE_ASSIGNED_ON"] != DBNull.Value)
                reAssignedOn = Convert.ToString(dr0["RE_ASSIGNED_ON"]);

            if (dr0["RE_ASSIGNED_REMARKS"] != DBNull.Value)
                reAssignedRemarks = Convert.ToString(dr0["RE_ASSIGNED_REMARKS"]);

            if (dr0["RE_ASSIGNED_BY_EMAIL"] != DBNull.Value)
                reAssignedByEmail = Convert.ToString(dr0["RE_ASSIGNED_BY_EMAIL"]);


            if (dr0["ACCEPTED_BY_ID"] != DBNull.Value)
                acceptedByID = Convert.ToInt32(dr0["ACCEPTED_BY_ID"]);

            if (dr0["ACCEPTED_BY"] != DBNull.Value)
                acceptedBy = Convert.ToString(dr0["ACCEPTED_BY"]);

            if (dr0["ACCEPTED_ON"] != DBNull.Value)
                acceptedOn = Convert.ToString(dr0["ACCEPTED_ON"]);

            if (dr0["ACCEPTED_REMARKS"] != DBNull.Value)
                acceptedRemarks = Convert.ToString(dr0["ACCEPTED_REMARKS"]);

            if (dr0["ACCEPTED_BY_EMAIL"] != DBNull.Value)
                acceptedByEmail = Convert.ToString(dr0["ACCEPTED_BY_EMAIL"]);


            if (dr0["REJECTED_BY_ID"] != DBNull.Value)
                rejectedByID = Convert.ToInt32(dr0["REJECTED_BY_ID"]);

            if (dr0["REJECTED_BY"] != DBNull.Value)
                rejectedBy = Convert.ToString(dr0["REJECTED_BY"]);

            if (dr0["REJECTED_ON"] != DBNull.Value)
                rejectedOn = Convert.ToString(dr0["REJECTED_ON"]);

            if (dr0["REJECTED_REMARKS"] != DBNull.Value)
                rejectedRemarks = Convert.ToString(dr0["REJECTED_REMARKS"]);

            if (dr0["REJECTED_BY_EMAIL"] != DBNull.Value)
                rejectedByEmail = Convert.ToString(dr0["REJECTED_BY_EMAIL"]);


            if (dr0["SCHEDULED_BY_ID"] != DBNull.Value)
                scheduledByID = Convert.ToInt32(dr0["SCHEDULED_BY_ID"]);

            if (dr0["SCHEDULED_BY"] != DBNull.Value)
            {
                scheduledBy = Convert.ToString(dr0["SCHEDULED_BY"]);
                completedBy = Convert.ToString(dr0["SCHEDULED_BY"]);
            }

            if (dr0["SCHEDULED_ON"] != DBNull.Value)
                scheduledOn = Convert.ToString(dr0["SCHEDULED_ON"]);

            if (dr0["SCHEDULED_REMARKS"] != DBNull.Value)
                scheduledRemarks = Convert.ToString(dr0["SCHEDULED_REMARKS"]);

            if (dr0["SCHEDULED_BY_EMAIL"] != DBNull.Value)
            {
                scheduledByEmail = Convert.ToString(dr0["SCHEDULED_BY_EMAIL"]);
                completedByEmail = Convert.ToString(dr0["SCHEDULED_BY_EMAIL"]);
            }



            if (dr0["INSPECTED_BY_ID"] != DBNull.Value)
                inspectedByID = Convert.ToInt32(dr0["INSPECTED_BY_ID"]);

            if (dr0["INSPECTED_BY"] != DBNull.Value)
                inspectedBy = Convert.ToString(dr0["INSPECTED_BY"]);

            if (dr0["INSPECTED_BY_EMAIL"] != DBNull.Value)
                inspectedByEmail = Convert.ToString(dr0["INSPECTED_BY_EMAIL"]); 

            if (dr0["TOTAL_QA_ACCEPTED_QUANTITY"] != DBNull.Value)
                totalAcceptedQuantity = Convert.ToString(dr0["TOTAL_QA_ACCEPTED_QUANTITY"]);




            //if (dr0["QA_ACCEPTED_BY_ID"] != DBNull.Value)
            //    qaAcceptedByID = Convert.ToInt32(dr0["QA_ACCEPTED_BY_ID"]);

            //if (dr0["QA_ACCEPTED_BY"] != DBNull.Value)
            //    qaAcceptedBy = Convert.ToString(dr0["QA_ACCEPTED_BY"]);

            //if (dr0["QA_ACCEPTED_ON"] != DBNull.Value)
            //    qaAcceptedOn = Convert.ToString(dr0["QA_ACCEPTED_ON"]);

            //if (dr0["QA_ACCEPTED_REMARKS"] != DBNull.Value)
            //    qaAcceptedRemarks = Convert.ToString(dr0["QA_ACCEPTED_REMARKS"]);

            //if (dr0["QA_ACCEPTED_BY_EMAIL"] != DBNull.Value)
            //    qaAcceptedByEmail = Convert.ToString(dr0["QA_ACCEPTED_BY_EMAIL"]);



            //if (dr0["QA_REJECTED_BY_ID"] != DBNull.Value)
            //    qaRejectedByID = Convert.ToInt32(dr0["QA_REJECTED_BY_ID"]);

            //if (dr0["QA_REJECTED_BY"] != DBNull.Value)
            //    qaRejectedBy = Convert.ToString(dr0["QA_REJECTED_BY"]);

            //if (dr0["QA_REJECTED_ON"] != DBNull.Value)
            //    qaRejectedOn = Convert.ToString(dr0["QA_REJECTED_ON"]);

            //if (dr0["QA_REJECTED_REMARKS"] != DBNull.Value)
            //    qaRejectedRemarks = Convert.ToString(dr0["QA_REJECTED_REMARKS"]);

            //if (dr0["QA_REJECTED_BY_EMAIL"] != DBNull.Value)
            //    qaRejectedByEmail = Convert.ToString(dr0["QA_REJECTED_BY_EMAIL"]);





            if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.AcceptedMail))
            {
                from = acceptedByEmail;
                fromName = acceptedBy;

                to = assignedByEmail;
                toName = assignedBy;

                cc = acceptedByEmail;

                fileName = "~/PROJECT/MACHINE_SCH/EMAIL_FORMATS/02AcceptedMail.htm";
                subject = "MS Alert: Drawing accepted for Machine Scheduling";
            }

            else if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.RejectedMail))
            {
                from = rejectedByEmail;
                fromName = rejectedBy;

                to = assignedByEmail;
                toName = assignedBy;

                cc = rejectedByEmail;

                fileName = "~/PROJECT/MACHINE_SCH/EMAIL_FORMATS/03RejectedMail.htm";
                subject = "MS Alert: Drawing rejcected by Machine Shop";
            }

            else if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.ScheduledMail))
            {
                from = scheduledByEmail;
                fromName = scheduledBy;

                to = assignedByEmail;
                toName = assignedBy;

                cc = scheduledByEmail;

                fileName = "~/PROJECT/MACHINE_SCH/EMAIL_FORMATS/04ScheduledMail.htm";
                subject = "MS Alert: Drawing scheduled by Machine Shop";
            }

            else if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.CompletedMail))
            {
                from = completedByEmail;
                fromName = completedBy;

                //to quality
                int qualityCounts = 0;
                if (dsDetail.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr2 in dsDetail.Tables[2].Rows)
                    {
                        to += Convert.ToString(dr2["EMAIL_ID"]) + ";";
                        qualityCounts++;
                    }

                    if (qualityCounts == 1)
                    {
                        toName = Convert.ToString(dsDetail.Tables[2].Rows[0]["EMPLOYEE_NAME"]);
                    }
                    else
                    {
                        toName = "Team";
                    }
                }

                cc = assignedByEmail + ";" + scheduledByEmail + ";" + completedByEmail;

                fileName = "~/PROJECT/MACHINE_SCH/EMAIL_FORMATS/05CompletedMail.htm";
                subject = "MS Alert: Drawing completed by Machine Shop";
            }

            else if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.QA_AcceptedMail))
            {
                from = inspectedByEmail;
                fromName = inspectedBy;

                to = assignedByEmail;
                toName = assignedBy;

                cc = productionManagerEmail + ";" + scheduledByEmail + ";" + inspectedByEmail;

                fileName = "~/PROJECT/MACHINE_SCH/EMAIL_FORMATS/06QAAcceptedMail.htm";
                subject = "MS Alert: Inspected and Accepted by Quality";
            }

            else if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.QA_RejectedMail))
            {
                from = inspectedByEmail;
                fromName = inspectedBy;

                to = scheduledByEmail;
                toName = scheduledBy;

                cc = productionManagerEmail + ";" + assignedByEmail + ";" + inspectedByEmail;

                fileName = "~/PROJECT/MACHINE_SCH/EMAIL_FORMATS/07QARejectedMail.htm";
                subject = "MS Alert: Inspected and Rejected by Quality";
            }

            SendMail(from, fromName, to, toName, cc, bcc, subject, fileName, href, dtMailInfo, null, dtScheudledDetails, dtInspectionDetails, mailTypeID, assignedDrawingCode, drawingNo, remarks);
        }
        else
            returnVal = 0;



        return returnVal;
    }


    private int SendMail(string from, string fromName, string to, string toName, string cc, string bcc, string subject, string fileName, string href,
                         DataTable dtMailInfo,
                         DataTable dtDistinctJobNo,
                         DataTable dtScheduledDtails,
                         DataTable dtInspectionDetails,
                         int mailTypeID, string assignedDrawingCode,
                         string drawingNo, string remarks)
    {
        returnVal = 0;

        Stream streamData = null;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();



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

        body = body.Replace("{#link#}", href);

        if (!string.IsNullOrEmpty(toName))
            body = body.Replace("{#toname#}", toName);
        else
            body = body.Replace("{#toname#}", "Sir/Madam");

        if (!string.IsNullOrEmpty(fromName))
            body = body.Replace("{#fromname#}", fromName);
        else
            body = body.Replace("{#fromname#}", "IT Team");

        body = body.Replace("{#drawingno#}", drawingNo);
        body = body.Replace("{#assigneddrawingcode#}", assignedDrawingCode);
        body = body.Replace("{#remarks#}", remarks);


        mail.Body = body;

        if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.AssignedMail))
        {
            jobNo = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("UNIT_NAME", typeof(string));
            dt.Columns.Add("JOB_NO", typeof(string));
            dt.Columns.Add("DRAWING_NO", typeof(string));
            dt.Columns.Add("EQUIPMENT", typeof(string));
            dt.Columns.Add("ITEM_NAME", typeof(string));
            dt.Columns.Add("ITEM_DETAIL", typeof(string));
            dt.Columns.Add("EXPECTED_DATE_OF_COMP_BY_PLANNING", typeof(string));
            dt.Columns.Add("ALLOCATED_QUANTITY", typeof(string));
            dt.Columns.Add("REMARKS", typeof(string));

            foreach (DataRow drj in dtDistinctJobNo.Rows)
            {
                dt.Rows.Clear();
                jobNo = Convert.ToString(drj["JOB_NO"]);
                foreach (DataRow dri in dtMailInfo.Select("JOB_NO='" + jobNo + "'"))
                {
                    DataRow drn = dt.NewRow();
                    drn["UNIT_NAME"] = Convert.ToString(dri["UNIT_NAME"]);
                    drn["JOB_NO"] = Convert.ToString(dri["JOB_NO"]);
                    drn["DRAWING_NO"] = Convert.ToString(dri["DRAWING_NO"]);
                    drn["EQUIPMENT"] = Convert.ToString(dri["EQUIPMENT"]);
                    drn["ITEM_NAME"] = Convert.ToString(dri["ITEM_NAME"]);
                    drn["ITEM_DETAIL"] = Convert.ToString(dri["ITEM_DETAIL"]);
                    drn["EXPECTED_DATE_OF_COMP_BY_PLANNING"] = Convert.ToString(dri["EXPECTED_DATE_OF_COMP_BY_PLANNING"]);
                    drn["ALLOCATED_QUANTITY"] = Convert.ToString(dri["ALLOCATED_QUANTITY"]);
                    drn["REMARKS"] = Convert.ToString(dri["REMARKS"]);
                    dt.Rows.Add(drn);
                }

                if (dt.Rows.Count > 0)
                {
                    streamData = CreateAttachment(dt);
                    if (streamData != null)
                    {
                        mail.Attachments.Add(new Attachment(streamData, jobNo + "_Assigned_Drawing_List_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv", "text/csv"));
                    }
                }
            }
        }
        else
        {
            byte[] bytes = GetPDFBytes(dtMailInfo, dtScheduledDtails, dtInspectionDetails);
            if (bytes != null)
            {
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), assignedDrawingCode + ".pdf"));
            }
        }
        //else if (mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.AcceptedMail) ||
        //         mailTypeID == Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.RejectedMail))
        //{
        //    byte[] bytes = GetPDFBytes(dtMailInfo, dtScheduledDtails);
        //    if (bytes != null)
        //    {
        //        mail.Attachments.Add(new Attachment(new MemoryStream(bytes), assignedDrawingCode + ".pdf"));
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

    private Stream CreateAttachment(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;

                    rowTxt = Convert.ToString(rowTxt).Replace("\n", " ");
                    rowTxt = Convert.ToString(rowTxt).Replace("\r", " ");
                    rowTxt = Convert.ToString(rowTxt).Replace(",", "") + ',';

                    csv += Convert.ToString(rowTxt);
                }
                csv += "\r\n";
            }


            byte[] byteArray = Encoding.ASCII.GetBytes(csv);
            MemoryStream stream = new MemoryStream(byteArray);

            return stream;
        }
        catch (Exception)
        {
            return null;
        }
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

    public byte[] GetPDFBytes(DataTable dtPrimaryDetails, DataTable dtScheduledDetails, DataTable dtInspectionDetails)
    {
        try
        {
            byte[] pdfBytes;
            var cssText = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Styles/LOT.css"));


            string htmltxt = objMSHtmlForPDF.GetHtmlForPDF(dtPrimaryDetails, dtScheduledDetails, dtInspectionDetails);

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