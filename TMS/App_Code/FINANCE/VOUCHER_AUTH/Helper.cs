using System;
using System.Text;
using System.Data;
using BAL;
using System.Net.Mail;
using System.IO;
using Ionic.Zip;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

public class Helper
{
    #region Variables


    string fileName = string.Empty;
    string from = string.Empty;
    string to = string.Empty;
    string toName = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;
    string salutation = string.Empty;
    string subject = string.Empty;
    string body = string.Empty;
    string bodyTable = string.Empty;
    string closingLine = string.Empty;
    string regards = string.Empty;

    VouchersAuthorization objVA = new VouchersAuthorization();

    public bool ProcessSendMail(int pid, int typeId)
    {
        bool sentMail = false;

        fileName = string.Empty;
        from = string.Empty;
        to = string.Empty;
        toName = string.Empty;
        cc = string.Empty;
        bcc = string.Empty;
        salutation = string.Empty;
        subject = string.Empty;
        body = string.Empty;
        bodyTable = string.Empty;
        closingLine = string.Empty;
        regards = string.Empty;

        DataSet dsDetails = new DataSet();
        dsDetails = objVA.GetVoucherDetailsForEmail(pid, typeId);
        if (dsDetails.Tables.Count > 0 && dsDetails.Tables[0].Rows.Count > 0)
        {
            sentMail = SendMail(dsDetails.Tables[0]);
        }


        return sentMail;
    }




    private bool SendMail(DataTable dtDetails)
    {
        bool returnVal = false;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();


        DataRow dr = dtDetails.Rows[0];

        fileName = "~/VOUCHER_AUTH/ReauthMail.htm";
        from = Convert.ToString(dr["FROM"]);
        to = Convert.ToString(dr["TO"]);
        toName = Convert.ToString(dr["TO_NAME"]);
        cc = Convert.ToString(dr["CC"]);
        bcc = Convert.ToString(dr["BCC"]);
        //salutation = Convert.ToString(dr["TO_NAME_WITH_SALUTATION"]);
        subject = Convert.ToString(dr["SUBJECT"]);


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

        body = body.Replace("{#Salutation#}", Convert.ToString(dtDetails.Rows[0]["TO_NAME_WITH_SALUTATION"]));
        body = body.Replace("{#BodyLine#}", Convert.ToString(dtDetails.Rows[0]["BODY"]));
        body = body.Replace("{#VoucherNo#}", Convert.ToString(dtDetails.Rows[0]["VOUCHER_NO"]));
        body = body.Replace("{#VoucherDate#}", Convert.ToString(dtDetails.Rows[0]["VOUCHER_DATE"]));
        body = body.Replace("{#VoucherType#}", Convert.ToString(dtDetails.Rows[0]["VOUCHER_TYPE"]));

        body = body.Replace("{#AuthorizedBy#}", Convert.ToString(dtDetails.Rows[0]["TO_NAME"]));
        body = body.Replace("{#AuthorizedOn#}", Convert.ToString(dtDetails.Rows[0]["AUTHORIZED_ON"]));
        body = body.Replace("{#AuthorizedRemarks#}", Convert.ToString(dtDetails.Rows[0]["AUTHORIZED_REMARKS"]));


        body = body.Replace("{#SentToReauthorizationBy#}", Convert.ToString(dtDetails.Rows[0]["SENT_TO_REAUTHORIZATION_BY"]));
        body = body.Replace("{#SentToReauthorizationOn#}", Convert.ToString(dtDetails.Rows[0]["SENT_TO_REAUTHORIZATION_ON"]));
        body = body.Replace("{#SentToReauthorizationRemarks#}", Convert.ToString(dtDetails.Rows[0]["SENT_TO_REAUTHORIZATION_REMARKS"]));

        body = body.Replace("{#Unit#}", Convert.ToString(dtDetails.Rows[0]["UNIT_NAME"]));

        body = body.Replace("{#ClosingLine#}", Convert.ToString(dtDetails.Rows[0]["CLOSING_LINE"]));
        body = body.Replace("{#Regards#}", Convert.ToString(dtDetails.Rows[0]["REGARDS"]));


        mail.Body = body;

        try
        {
            if (!string.IsNullOrEmpty(to))
            {
                SmtpServer.Send(mail);
                returnVal = true;
            }
            else
                returnVal = false;
        }
        catch (Exception ex)
        {
            string exMsg = ex.ToString();
            if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                returnVal = true;

            else
                returnVal = false;
        }


        return returnVal;

    }




    //protected void ExportZipWithPDFCopy(int Pid, string voucherNo, int voucherTypeId, string challanNo, byte[] pdfCopy, string pdfCopyName)
    //{
    //    using (ZipFile zip = new ZipFile())
    //    {
    //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;

    //        DataSet dsAttachedFiles = objVA.GetVoucherDOCS(Pid, voucherTypeId, challanNo);

    //        int count = 0;

    //        if (dsAttachedFiles.Tables.Count > 0)
    //        {
    //            if (dsAttachedFiles.Tables[0].Rows.Count > 0)
    //            {
    //                foreach (DataRow row in dsAttachedFiles.Tables[0].Rows)
    //                {
    //                    count++;
    //                    string name = count + Convert.ToString(row["FILE_NAME"]);
    //                    byte[] bytes = (byte[])row["FILE_BYTES"];
    //                    zip.AddEntry(name, bytes);
    //                }
    //            }



    //            if (dsAttachedFiles.Tables[1].Rows.Count > 0)
    //            {
    //                foreach (DataRow row in dsAttachedFiles.Tables[1].Rows)
    //                {
    //                    count++;
    //                    string name = count + Convert.ToString(row["FILE_NAME"]);
    //                    byte[] bytes = (byte[])row["FILE_BYTES"];
    //                    zip.AddEntry(name, bytes);
    //                }
    //            }

    //        }

    //        if (count > 0)
    //        {
    //            if (pdfCopy != null)
    //            {
    //                zip.AddEntry(pdfCopyName, pdfCopy);
    //            }

    //            Response.Clear();
    //            Response.BufferOutput = false;
    //            string zipName = String.Format(voucherNo + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
    //            Response.ContentType = "application/zip";
    //            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
    //            zip.Save(Response.OutputStream);
    //            Response.End();
    //        }
    //    }
    //}


    //public byte[] GeneratePDF(int Pid, string voucherNo)
    //{
    //    try
    //    {
    //        string fileName = string.Empty;
    //        byte[] pdf;
    //        var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

    //        string htmlTxt = string.Empty;
    //        StringBuilder sb = new StringBuilder();

    //        htmlTxt = GetPDFDetailAndReturnHTML(Pid, voucherNo);

    //        if (!string.IsNullOrEmpty(htmlTxt))
    //        {
    //            if (!string.IsNullOrEmpty(voucherNo))
    //                fileName = Convert.ToString(voucherNo);
    //            else
    //                fileName = "Voucher_" + DateTime.Now.ToString("dd-MMM-yyyy");

    //            sb.Append("<html>\n");
    //            sb.Append("<body>\n");
    //            sb.Append(htmlTxt + "\n");
    //            sb.Append("</body>\n");
    //            sb.Append("</html>\n");
    //        }

    //        var html = sb.ToString();
    //        if (!string.IsNullOrEmpty(Convert.ToString(html)))
    //        {
    //            string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
    //            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
    //            img.Alignment = Element.ALIGN_LEFT;
    //            img.ScaleToFit(180f, 250f);


    //            using (var memoryStream = new MemoryStream())
    //            {
    //                var document = new Document(PageSize.A4);
    //                var writer = PdfWriter.GetInstance(document, memoryStream);
    //                document.Open();
    //                document.Add(img);
    //                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
    //                {
    //                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
    //                    {
    //                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
    //                    }
    //                }

    //                document.Close();
    //                pdf = memoryStream.GetBuffer();

    //                return pdf;
    //            }
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //}


    public List<FileBytes> ExportByetes(int Pid, string voucherNo, int voucherTypeId, string challanNo)
    {
        
        List<FileBytes> fileBytes = new List<FileBytes>();

        DataSet dsAttachedFiles = objVA.GetVoucherDOCS(Pid, voucherTypeId, challanNo);

        int count = 0;

        if (dsAttachedFiles.Tables.Count > 0)
        {
            if (dsAttachedFiles.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsAttachedFiles.Tables[0].Rows)
                {
                    count++;
                    string name = count + Convert.ToString(row["FILE_NAME"]).Replace('(', ' ').Replace(')', ' ').Replace('-', ' ');
                    byte[] bytes = (byte[])row["FILE_BYTES"];

                    //zip.AddEntry(name, bytes);
                    //fileByte.mainBytes = bytes;

                    FileBytes fileByte = new FileBytes();

                    fileByte.mainFileName = name;
                    fileByte.mainBytes = bytes;

                    fileBytes.Add(fileByte);

                }
            }

            if (dsAttachedFiles.Tables.Count > 2)
            {
                if (dsAttachedFiles.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in dsAttachedFiles.Tables[1].Rows)
                    {
                        count++;
                        string name = count + Convert.ToString(row["FILE_NAME"]).Replace('(', ' ').Replace('(', ' ').Replace('-', ' ');
                        byte[] bytes = (byte[])row["FILE_BYTES"];

                        FileBytes fileByte = new FileBytes();

                        fileByte.supportingFileName = name;
                        fileByte.supportingBytes = bytes;

                        fileBytes.Add(fileByte);
                    }
                }
            }

        }

        return fileBytes;
    }

    #endregion


    //send mail


}

