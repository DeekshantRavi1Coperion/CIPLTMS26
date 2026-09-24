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


public class TourExpenseSendMail
{

    #region VARIABLES[===================]

    TourAndTravels objTourAndTravels = new TourAndTravels();
    DataSet dsMailInfo = new DataSet();
    DataTable dtMailInfo = new DataTable();
    TourExpenseHtmlForPDFForMail objTourExpenseHtmlForPDF = new TourExpenseHtmlForPDFForMail();
    DataSet dsDetail = new DataSet();

    int returnVal = 0;
    string tourExpenseNo = string.Empty;
    string createdBy = string.Empty;
    string createdByEmail = string.Empty;
    string createdRemarks = string.Empty;

    string closigPersonByEmail = string.Empty;


    int    bookedByID = 0;
    string bookedBy = string.Empty;
    string bookedOn = string.Empty;
    string bookedByEmail = string.Empty;
    string bookedRemarks = string.Empty;


    int closedByID = 0;
    string closedBy = string.Empty;
    string closedOn = string.Empty;
    string closedByEmail = string.Empty;
    string closedRemarks = string.Empty;

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

    public int ProcessAndSendMail(int recordID, int statusID)//int mailTypeID,
    {
        returnVal = 0;
        tourExpenseNo = string.Empty;
        fileName = string.Empty;
        urlTxt = string.Empty;
        link = string.Empty;
        href = string.Empty;

        dsMailInfo = objTourAndTravels.GetTourExpenseMailInfo(recordID);
        if (dsMailInfo.Tables.Count > 0 && dsMailInfo.Tables[0].Rows.Count > 0)
        {
            dtMailInfo = dsMailInfo.Tables[0];

            createdBy = string.Empty;
            createdByEmail = string.Empty;
            createdRemarks = string.Empty;

            closigPersonByEmail = string.Empty;

            bookedByID = 0;
            bookedBy = string.Empty;
            bookedOn = string.Empty;
            bookedByEmail = string.Empty;
            bookedRemarks = string.Empty;

            closedByID = 0;
            closedBy = string.Empty;
            closedOn = string.Empty;
            closedByEmail = string.Empty;
            closedRemarks = string.Empty;

            //urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
            //link = "'" + urlTxt + "/Login.aspx?dmsstatusid=" + statusID + "'";

            if (dtMailInfo.Rows.Count > 0)
            {
                DataRow dr = dtMailInfo.Rows[0];

                if (dr["TOUR_EXPENSE_NO"] != DBNull.Value)
                    tourExpenseNo = Convert.ToString(dr["TOUR_EXPENSE_NO"]);

                if (dr["CREATED_BY"] != DBNull.Value)
                    createdBy = Convert.ToString(dr["CREATED_BY"]);

                if (dr["CREATED_BY_EMAIL_ID"] != DBNull.Value)
                    createdByEmail = Convert.ToString(dr["CREATED_BY_EMAIL_ID"]);

                if (dr["BOOKED_BY"] != DBNull.Value)
                    bookedBy = Convert.ToString(dr["BOOKED_BY"]);

                if (dr["BOOKED_BY_EMAIL_ID"] != DBNull.Value)
                    bookedByEmail = Convert.ToString(dr["BOOKED_BY_EMAIL_ID"]);


                if (dr["CLOSED_BY"] != DBNull.Value)
                    closedBy = Convert.ToString(dr["CLOSED_BY"]);

                if (dr["CLOSED_BY_EMAIL_ID"] != DBNull.Value)
                    closedByEmail = Convert.ToString(dr["CLOSED_BY_EMAIL_ID"]);



                if (statusID == (int)TandTAllStatus.EnumTourExpenseStatus.Open)
                {
                    subject = "Tour Expense released on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                    fileName = "/TOUR_AND_TRAVELS/TOUR_EXPENSE/EMAIL_FORMATS/01TourExpenseOpenMail.htm";

                    if (dsMailInfo.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dsMailInfo.Tables[1].Rows)
                        {
                            closigPersonByEmail += dr1["EMAIL_ID"] + ";";
                        }
                    }

                    if (!string.IsNullOrEmpty(closigPersonByEmail))
                        closigPersonByEmail = closigPersonByEmail.TrimEnd(';');


                    if (dsMailInfo.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dsMailInfo.Tables[2].Rows)
                        {
                            cc += dr2["EMAIL_ID"] + ";";
                        }
                    }


                    from = createdByEmail;
                    fromName = createdBy;
                    to = closigPersonByEmail;
                    cc += createdByEmail;
                }

                else if (statusID == (int)TandTAllStatus.EnumTourExpenseStatus.Booked)
                {
                    subject = "Tour Expense closed on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                    fileName = "/TOUR_AND_TRAVELS/TOUR_EXPENSE/EMAIL_FORMATS/02TourExpenseBookedMail.htm";

                    if (dsMailInfo.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dsMailInfo.Tables[1].Rows)
                        {
                            closigPersonByEmail += dr1["EMAIL_ID"] + ";";
                        }
                    }

                    if (!string.IsNullOrEmpty(closigPersonByEmail))
                        closigPersonByEmail = closigPersonByEmail.TrimEnd(';');

                    if (dsMailInfo.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dsMailInfo.Tables[2].Rows)
                        {
                            cc += dr2["EMAIL_ID"] + ";";
                        }
                    }

                    from = bookedByEmail;
                    fromName = bookedBy;
                    to = createdByEmail;
                    toName = createdBy;
                    cc += bookedByEmail + ";" + closigPersonByEmail;
                }

                else if (statusID == (int)TandTAllStatus.EnumTourExpenseStatus.Closed)
                {
                    subject = "Tour Expense closed on :" + DateTime.Now.ToString("dd-MMM-yyyy");
                    fileName = "/TOUR_AND_TRAVELS/TOUR_EXPENSE/EMAIL_FORMATS/03TourExpenseClosedMail.htm";

                    if (dsMailInfo.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dsMailInfo.Tables[1].Rows)
                        {
                            closigPersonByEmail += dr1["EMAIL_ID"] + ";";
                        }
                    }

                    if (!string.IsNullOrEmpty(closigPersonByEmail))
                        closigPersonByEmail = closigPersonByEmail.TrimEnd(';');

                    if (dsMailInfo.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dsMailInfo.Tables[2].Rows)
                        {
                            cc += dr2["EMAIL_ID"] + ";";
                        }
                    }

                    from = closedByEmail;
                    fromName = closedBy;
                    to = createdByEmail;
                    toName = createdBy;
                    cc += closedByEmail + ";" + closigPersonByEmail;
                }

                returnVal = SendMail(from, fromName, to, toName, cc, bcc, subject
                                   , fileName, dtMailInfo, tourExpenseNo);
            }
        }
        else returnVal = 0;

        return returnVal;
    }

    private int SendMail(string from, string fromName, string to, string toName
                        , string cc, string bcc, string subject, string fileName
                        , DataTable dtMailInfoForAttachment, string tourExpenseNo)
    {
        returnVal = 0;

        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        //urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
        //link = "'" + urlTxt + "/Login.aspx?dmsstatusid=" + statusID + "'";



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
            body = body.Replace("{#toname#}", "All");

        if (!string.IsNullOrEmpty(fromName))
            body = body.Replace("{#fromname#}", fromName);
        else
            body = body.Replace("{#fromname#}", "IT Team");


        mail.Body = body;

        byte[] bytes = GetPDFBytes(dtMailInfoForAttachment);
        mail.Attachments.Add(new Attachment(new MemoryStream(bytes), tourExpenseNo + ".pdf"));


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

    public byte[] GetPDFBytes(DataTable dtMailInfo)
    {

        try
        {
            byte[] pdfBytes = null;
            var cssText = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Styles/LOT.css"));
            string htmltxt = objTourExpenseHtmlForPDF.GetHtmlForPDF(dtMailInfo);

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