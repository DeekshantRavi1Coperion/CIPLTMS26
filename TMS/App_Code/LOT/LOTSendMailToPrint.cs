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


public class LOTSendMailToPrint
{

    #region VARIABLES[===================]

    Project objProject = new Project();
    DataSet dsMailInfo = new DataSet();


    int returnVal = 0;
    int prodMngrID = 0;
    string prodMngrName = string.Empty;
    string prodMngrEmail = string.Empty;
    string prodMngrEmailCC = string.Empty;







    string printerEmail = string.Empty;

    #endregion

    public int SendEmailLOT(int LOTTFID, string LOTTFSubItemIDs)
    {
        try
        {
            returnVal = 0;
            prodMngrID = 0;
            prodMngrName = string.Empty;
            prodMngrEmail = string.Empty;
            printerEmail = string.Empty;

            dsMailInfo = objProject.GetJOBMailInfoToPrint(LOTTFID, LOTTFSubItemIDs);
            if (dsMailInfo.Tables.Count > 0)
            {
                if (dsMailInfo.Tables[1].Rows.Count > 0)
                {
                    prodMngrID = Convert.ToInt32(dsMailInfo.Tables[1].Rows[0]["PRODUCTION_MNGR_ID"]);
                    prodMngrEmail = Convert.ToString(dsMailInfo.Tables[1].Rows[0]["PRODUCTION_MNGR_EMAIL"]);
                }

                if (dsMailInfo.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsMailInfo.Tables[2].Select("MANAGER_ID='" + prodMngrID + "'"))
                    {
                        printerEmail= Convert.ToString(dr["PRINTER_EMAIL"]);
                    }                    
                }

                SendMail(prodMngrEmail, printerEmail, dsMailInfo.Tables[0]);
            }
            else
                returnVal = 0;

            return returnVal;
        }

        catch (Exception ex)
        {
            returnVal = 0;
            return returnVal;
        }
    }




    private int SendMail(string from, string to, DataTable dtDrawings)
    {

        
        returnVal = 0;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();


        mail.Subject = "";

        if (!string.IsNullOrEmpty(from))
            mail.From = new MailAddress(from);

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

        string cc = "trinetra-nand.upadhyay@coperion.com;deekshant.ravi@coperion.com";
        if (!string.IsNullOrEmpty(cc))
        {
            cc = cc.TrimEnd(';');
            string[] strCC = cc.Split(';');
            foreach (string item in strCC)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.CC.Add(item);
                }
            }
        }



        mail.Body = "";

        if (dtDrawings.Rows.Count > 0)
        {
            foreach (DataRow dr in dtDrawings.Rows)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT1_NAME"])) && dr["SI_ATTACHMENT1_DOC"] != null)
                {
                    byte[] bytes1 = (byte[])dr["SI_ATTACHMENT1_DOC"];
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes1), Convert.ToString(dr["SI_ATTACHMENT1_NAME"]) + ".pdf"));
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT2_NAME"])) && dr["SI_ATTACHMENT2_DOC"] != null)
                {
                    byte[] bytes2 = (byte[])dr["SI_ATTACHMENT2_DOC"];
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes2), Convert.ToString(dr["SI_ATTACHMENT2_NAME"]) + ".pdf"));
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT3_NAME"])) && dr["SI_ATTACHMENT3_DOC"] != null)
                {
                    byte[] bytes3 = (byte[])dr["SI_ATTACHMENT3_DOC"];
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes3), Convert.ToString(dr["SI_ATTACHMENT3_NAME"]) + ".pdf"));
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT4_NAME"])) && dr["SI_ATTACHMENT4_DOC"] != null)
                {
                    byte[] bytes4 = (byte[])dr["SI_ATTACHMENT4_DOC"];
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes4), Convert.ToString(dr["SI_ATTACHMENT4_NAME"]) + ".pdf"));
                }
            }
        }

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


        return returnVal;

    }
}