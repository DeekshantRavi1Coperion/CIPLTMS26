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


public class TCSendMail
{

    #region VARIABLES[===================]

    TC objTC = new TC();
    DataSet dsMailInfo = new DataSet();
    DMSHtmlForPDFForMail objDMSHtmlForPDF = new DMSHtmlForPDFForMail();
    DataSet dsDetail = new DataSet();

    int returnVal = 0;
    string poNo = string.Empty;
    string poDate = string.Empty;
    string vendorCode = string.Empty;
    string vendorName = string.Empty;
    string tcName = string.Empty;
    string statusName = string.Empty;

    int uploadedByID = 0;
    string uploadedBy = string.Empty;
    string uploadedOn = string.Empty;
    string uploadedByEmail = string.Empty;
    string uploadedRemarks = string.Empty;

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

    string body = string.Empty;
    string subject = string.Empty;
    string from = string.Empty;
    string fromName = string.Empty;
    string to = string.Empty;
    string toName = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;
    string remarks = string.Empty;
    string fileName = string.Empty;

    #endregion

    public int ProcessAndSendMail(int poRecordID, string poTcRecordIDs)
    {
        returnVal = 0;

        dsMailInfo = objTC.GetPOTCMailInfo(poRecordID, poTcRecordIDs);
        if (dsMailInfo.Tables.Count > 0 && dsMailInfo.Tables[0].Rows.Count > 0)
        {
            poNo = string.Empty;
            poDate = string.Empty;
            vendorCode = string.Empty;
            vendorName = string.Empty;
            tcName = string.Empty;
            statusName = string.Empty;

            uploadedByID = 0;
            uploadedBy = string.Empty;
            uploadedOn = string.Empty;
            uploadedByEmail = string.Empty;
            uploadedRemarks = string.Empty;

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

            remarks = string.Empty;
            fileName = string.Empty;

            if (dsMailInfo.Tables.Count > 0)
            {
                if (dsMailInfo.Tables[0].Rows.Count > 0)
                {
                    if (dsMailInfo.Tables[0].Rows[0]["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PO_NO"])))
                        poNo = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PO_NO"]);

                    if (dsMailInfo.Tables[0].Rows[0]["PO_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PO_DATE"])))
                        poDate = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["PO_DATE"]);


                    if (dsMailInfo.Tables[0].Rows[0]["VENDOR_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsMailInfo.Tables[0].Rows[0]["VENDOR_CODE"])))
                        vendorCode = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["VENDOR_CODE"]);

                    if (dsMailInfo.Tables[0].Rows[0]["VENDOR_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsMailInfo.Tables[0].Rows[0]["VENDOR_NAME"])))
                        vendorName = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["VENDOR_NAME"]);
                }

                if (dsMailInfo.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsMailInfo.Tables[1].Rows)
                    {

                        if (dr["UPLOADED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UPLOADED_BY"])))
                            uploadedBy = Convert.ToString(dr["UPLOADED_BY"]);

                        if (dr["UPLOADED_BY_EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UPLOADED_BY_EMAIL_ID"])))
                            uploadedByEmail = Convert.ToString(dr["UPLOADED_BY_EMAIL_ID"]);

                        //if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                        //{
                        //    if (dr["ACCEPTED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["ACCEPTED_BY"])))
                        //        acceptedBy = Convert.ToString(dr["ACCEPTED_BY"]);

                        //    if (dr["ACCEPTED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["ACCEPTED_ON"])))
                        //        acceptedOn = Convert.ToString(dr["ACCEPTED_ON"]);

                        //    if (dr["ACCEPTED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["ACCEPTED_REMARKS"])))
                        //        acceptedRemarks = Convert.ToString(dr["ACCEPTED_REMARKS"]);

                        //    if (dr["ACCEPTED_BY_EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["ACCEPTED_BY_EMAIL_ID"])))
                        //        acceptedByEmail = Convert.ToString(dr["ACCEPTED_BY_EMAIL_ID"]);

                        //    if (dr["TC_ATTACHMENT_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["TC_ATTACHMENT_NAME"])))
                        //        tcName = Convert.ToString(dr["TC_ATTACHMENT_NAME"]);

                        //    if (dr["STATUS_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["STATUS_NAME"])))
                        //        statusName = Convert.ToString(dr["STATUS_NAME"]);


                        //    from = acceptedByEmail;
                        //    fromName = acceptedBy;
                        //    to = uploadedByEmail;
                        //    toName = uploadedBy;
                        //    subject = "Uploaded TC " + tcName + " accepted by QA on: " + acceptedOn;
                        //    fileName = "~/TC/EMAIL_FORMATS/01StatusMail.htm";

                        //    //returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject,
                        //    //                     poNo, poDate, vendorCode, vendorName, tcName, statusName, acceptedRemarks, fileName);
                        //}

                        if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                        {
                            if (dr["REJECTED_BY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REJECTED_BY"])))
                                rejectedBy = Convert.ToString(dr["REJECTED_BY"]);

                            if (dr["REJECTED_ON"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REJECTED_ON"])))
                                rejectedOn = Convert.ToString(dr["REJECTED_ON"]);

                            if (dr["REJECTED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REJECTED_REMARKS"])))
                                rejectedRemarks = Convert.ToString(dr["REJECTED_REMARKS"]);

                            if (dr["REJECTED_BY_EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REJECTED_BY_EMAIL_ID"])))
                                rejectedByEmail = Convert.ToString(dr["REJECTED_BY_EMAIL_ID"]);

                            if (dr["TC_ATTACHMENT_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["TC_ATTACHMENT_NAME"])))
                                tcName = Convert.ToString(dr["TC_ATTACHMENT_NAME"]);

                            if (dr["STATUS_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["STATUS_NAME"])))
                                statusName = Convert.ToString(dr["STATUS_NAME"]);


                            from = rejectedByEmail;
                            fromName = rejectedBy;
                            to = uploadedByEmail;
                            toName = uploadedBy;
                            bcc = rejectedByEmail;


                            foreach (DataRow dr2 in dsMailInfo.Tables[2].Select("STATUS_ID='" + Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected) + "'"))
                            {
                                foreach (DataRow dr3 in dsMailInfo.Tables[3].Select("TC_DEPARTMENT_ID='" + Convert.ToInt32(dr2["ASSIGNED_TO_ID"]) + "'"))
                                {
                                    if (Convert.ToString(dr3["EMAIL_ID"]) != rejectedByEmail)
                                    {
                                        cc += ";" + Convert.ToString(dr3["EMAIL_ID"]);
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(cc))
                                cc = cc.TrimStart(';');


                            subject = "Uploaded TC " + tcName + " rejected by QA on: " + rejectedOn;
                            fileName = "~/TC/EMAIL_FORMATS/02RejectedMail.htm";

                            returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject,
                                                poNo, poDate, vendorCode, vendorName, tcName, statusName, rejectedRemarks, fileName);
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
                         string subject,
                         string poNo, string poDate, string vendorCode, string vendorName, string tcName, string statusName, string remarks,
                         string fileName)
    {
        returnVal = 0;

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

        body = body.Replace("{#pono#}", poNo);
        body = body.Replace("{#podate#}", poDate);
        body = body.Replace("{#vendorcode#}", vendorCode);
        body = body.Replace("{#vendorname#}", vendorName);
        body = body.Replace("{#tcname#}", tcName);
        body = body.Replace("{#statusname#}", statusName);
        body = body.Replace("{#remarks#}", remarks);

        body = body.Replace("{#toname#}", toName);

        if (!string.IsNullOrEmpty(toName))
            body = body.Replace("{#toname#}", toName);
        else
            body = body.Replace("{#toname#}", "Sir/Madam");

        body = body.Replace("{#fromname#}", fromName);

        if (!string.IsNullOrEmpty(fromName))
            body = body.Replace("{#fromname#}", fromName);
        else
            body = body.Replace("{#fromname#}", "IT Team");

        mail.Body = body;

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
}