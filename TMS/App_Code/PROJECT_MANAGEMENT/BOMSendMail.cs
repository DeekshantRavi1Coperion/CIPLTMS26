using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for BOMSendMail
/// </summary>
public class BOMSendMail
{

    BOMHtmlForPDF objBOMHtmlForPDF = new BOMHtmlForPDF();
    BAL.PROJECT_MANAGEMENT.ProjectManagement objProjM = new BAL.PROJECT_MANAGEMENT.ProjectManagement();
    DataSet dsBomInfo = new DataSet();

    DataTable dtBomHeader = new DataTable();
    DataTable dtBomLine = new DataTable();
    DataTable dtMailDesc = new DataTable();


    private int _statusId;
    private int _emailTypeId;
    private string _status;

    private string _createdBy;
    private string _createdByEmail;

    private string _approvedBy;
    private string _approvedByEmail;

    private string _sentToAmendmentBy;
    private string _sentToAmendmentByEmail;
    private string _sentToAmendmentRemarks;

    private string _amendedBy;
    private string _amendedByEmail;

    private string _amendedApprovedBy;
    private string _amendedApprovedByEmail;

    private string _cancelledBy;
    private string _cancelledByEmail;

    private string _peEmail;
    private string _pmEmail;

    private string _from;
    private string _fromName;
    private string _to;
    private string _toName;
    private string _cc;
    private string _bcc;
    private string _subject;
    private string _body;

    private int _bomId;
    private string _mrNo;


    private string _fileName;
    private string _currentDateTime;


    #region Properties


    public int BomId
    {
        get
        {
            return _bomId;
        }

        set
        {
            _bomId = value;
        }
    }

    public string PeEmail
    {
        get
        {
            return _peEmail;
        }

        set
        {
            _peEmail = value;
        }
    }

    public string PmEmail
    {
        get
        {
            return _pmEmail;
        }

        set
        {
            _pmEmail = value;
        }
    }



    public string FromName
    {
        get
        {
            return _fromName;
        }

        set
        {
            _fromName = value;
        }
    }


    public string From
    {
        get
        {
            return _from;
        }

        set
        {
            _from = value;
        }
    }

    public string To
    {
        get
        {
            return _to;
        }

        set
        {
            _to = value;
        }
    }

    public string ToName
    {
        get
        {
            return _toName;
        }

        set
        {
            _toName = value;
        }
    }

    public string Cc
    {
        get
        {
            return _cc;
        }

        set
        {
            _cc = value;
        }
    }

    public string Bcc
    {
        get
        {
            return _bcc;
        }

        set
        {
            _bcc = value;
        }
    }

    public string Subject
    {
        get
        {
            return _subject;
        }

        set
        {
            _subject = value;
        }
    }

    public string Body
    {
        get
        {
            return _body;
        }

        set
        {
            _body = value;
        }
    }





    public int StatusId
    {
        get
        {
            return _statusId;
        }

        set
        {
            _statusId = value;
        }
    }

    public int EmailTypeId
    {
        get
        {
            return _emailTypeId;
        }

        set
        {
            _emailTypeId = value;
        }
    }

    public string MrNo
    {
        get
        {
            return _mrNo;
        }

        set
        {
            _mrNo = value;
        }
    }

    public string Status
    {
        get
        {
            return _status;
        }

        set
        {
            _status = value;
        }
    }



    public string CreatedBy
    {
        get
        {
            return _createdBy;
        }

        set
        {
            _createdBy = value;
        }
    }

    public string CreatedByEmail
    {
        get
        {
            return _createdByEmail;
        }

        set
        {
            _createdByEmail = value;
        }
    }



    public string ApprovedBy
    {
        get
        {
            return _approvedBy;
        }

        set
        {
            _approvedBy = value;
        }
    }

    public string ApprovedByEmail
    {
        get
        {
            return _approvedByEmail;
        }

        set
        {
            _approvedByEmail = value;
        }
    }



    public string SentToAmendmentBy
    {
        get
        {
            return _sentToAmendmentBy;
        }

        set
        {
            _sentToAmendmentBy = value;
        }
    }

    public string SentToAmendmentByEmail
    {
        get
        {
            return _sentToAmendmentByEmail;
        }

        set
        {
            _sentToAmendmentByEmail = value;
        }
    }

    public string SentToAmendmentRemarks
    {
        get
        {
            return _sentToAmendmentRemarks;
        }

        set
        {
            _sentToAmendmentRemarks = value;
        }
    }





    public string AmendedBy
    {
        get
        {
            return _amendedBy;
        }

        set
        {
            _amendedBy = value;
        }
    }

    public string AmendedByEmail
    {
        get
        {
            return _amendedByEmail;
        }

        set
        {
            _amendedByEmail = value;
        }
    }



    public string AmendedApprovedBy
    {
        get
        {
            return _amendedApprovedBy;
        }

        set
        {
            _amendedApprovedBy = value;
        }
    }

    public string AmendedApprovedByEmail
    {
        get
        {
            return _amendedApprovedByEmail;
        }

        set
        {
            _amendedApprovedByEmail = value;
        }
    }

    public string FileName
    {
        get
        {
            return _fileName;
        }

        set
        {
            _fileName = value;
        }
    }

    public string CurrentDateTime
    {
        get
        {
            _currentDateTime = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
            return _currentDateTime;
        }
    }

    public string CancelledBy
    {
        get
        {
            return _cancelledBy;
        }

        set
        {
            _cancelledBy = value;
        }
    }

    public string CancelledByEmail
    {
        get
        {
            return _cancelledByEmail;
        }

        set
        {
            _cancelledByEmail = value;
        }
    }





    #endregion



    public BOMSendMail(int BomPid)
    {
        BomId = BomPid;
    }

    public bool InitializeSendMail()
    {
        bool check = false;

        dsBomInfo = objProjM.GetBomMailInfo(BomId);
        if (dsBomInfo.Tables.Count == 0 && dsBomInfo.Tables[0].Rows.Count == 0) return false;

        dtBomHeader = dsBomInfo.Tables[0];
        dtBomLine = dsBomInfo.Tables[1];

        check = ProcessSendMail(dtBomHeader, dtBomLine);

        return check;
    }

    private bool ProcessSendMail(DataTable dtBomHeader, DataTable dtBomLine)
    {
        bool isGood = true;
        bool isBad = false;


        DataRow dr0 = dtBomHeader.Rows[0];

        MrNo = Convert.ToString(dr0["MR_NO"]);

        StatusId = Convert.ToInt32(dr0["STATUS_FID"]);

        CreatedBy = Convert.ToString(dr0["CREATED_BY"]);
        CreatedByEmail = Convert.ToString(dr0["CREATED_BY_EMAIL"]);

        ApprovedBy = Convert.ToString(dr0["APPROVED_BY"]);
        ApprovedByEmail = Convert.ToString(dr0["APPROVED_BY_EMAIL"]);

        SentToAmendmentBy = Convert.ToString(dr0["AMENDMENT_BY"]);
        SentToAmendmentByEmail = Convert.ToString(dr0["AMENDMENT_BY_EMAIL"]);
        SentToAmendmentRemarks = Convert.ToString(dr0["AMENDMENT_REMARKS"]);

        AmendedBy = Convert.ToString(dr0["AMENDED_BY"]);
        AmendedByEmail = Convert.ToString(dr0["AMENDED_BY_EMAIL"]);

        AmendedApprovedBy = Convert.ToString(dr0["AMENDED_APPROVED_BY"]);
        AmendedApprovedByEmail = Convert.ToString(dr0["AMENDED_APPROVED_BY_EMAIL"]);

        CancelledBy = Convert.ToString(dr0["CANCELLED_BY"]);
        CancelledByEmail = Convert.ToString(dr0["CANCELLED_BY_EMAIL"]);


        PeEmail = Convert.ToString(dr0["PE_EMAIL"]);
        PmEmail = Convert.ToString(dr0["PM_EMAIL"]);


        EmailTypeId = GetMailType(StatusId);


        if (StatusId == (int)BOMAllTypeEnums.EnumStatus.New)
        {
            From = CreatedByEmail;
            To = PeEmail + ";" + PmEmail;
            Bcc = CreatedByEmail;

            FromName = CreatedBy;
            ToName = "Team";

            Subject = "BOM: Released Bom with Mr No. '" + MrNo + "' for approval On: " + CurrentDateTime;
            FileName = "~/PROJECT_MANAGEMENT/EMAIL_FORMATS/01ApprovalMail.htm";

            //SendMail(MrNo, FromName, From, To, ToName, Cc, Bcc, Subject, FileName, dtBomHeader, dtBomLine);


        }

        else if (StatusId == (int)BOMAllTypeEnums.EnumStatus.Approved)
        {
            From = ApprovedByEmail;
            To = CreatedByEmail;
            Bcc = ApprovedByEmail + ";" + PeEmail + ";" + PmEmail;

            FromName = ApprovedBy;
            ToName = CreatedBy;

            Subject = "BOM: Released Bom with Mr No. '" + MrNo + "' Approved On: " + CurrentDateTime;
            FileName = "~/PROJECT_MANAGEMENT/EMAIL_FORMATS/02ApprovedMail.htm";

            //SendMail(MrNo, FromName, From, To, ToName, Cc, Bcc, Subject, FileName, dtBomHeader, dtBomLine);
        }

        else if (StatusId == (int)BOMAllTypeEnums.EnumStatus.Amendment)
        {
            From = SentToAmendmentByEmail;
            To = CreatedByEmail;
            Bcc = SentToAmendmentByEmail + ";" + PeEmail + ";" + PmEmail;

            FromName = SentToAmendmentBy;
            ToName = CreatedBy;

            Subject = "BOM: Released Bom with Mr No. '" + MrNo + "' Sent To Amendment On: " + CurrentDateTime;
            FileName = "~/PROJECT_MANAGEMENT/EMAIL_FORMATS/03AmendmentMail.htm";

            //SendMail(MrNo, FromName, From, To, ToName, Cc, Bcc, Subject, FileName, dtBomHeader, dtBomLine);
        }

        else if (StatusId == (int)BOMAllTypeEnums.EnumStatus.Amended)
        {
            From = AmendedByEmail;
            To = PeEmail + ";" + PmEmail;
            Bcc = AmendedByEmail;

            FromName = AmendedBy;
            ToName = "Team";

            Subject = "BOM: Released Bom with Mr No. '" + MrNo + "' Amended On: " + CurrentDateTime;
            FileName = "~/PROJECT_MANAGEMENT/EMAIL_FORMATS/04AmendededMail.htm";

            //SendMail(MrNo, FromName, From, To, ToName, Cc, Bcc, Subject, FileName, dtBomHeader, dtBomLine);
        }

        else if (StatusId == (int)BOMAllTypeEnums.EnumStatus.AmendedApproved)
        {
            From = AmendedApprovedByEmail;
            To = CreatedByEmail;
            Bcc = AmendedApprovedByEmail + ";" + PeEmail + ";" + PmEmail;

            FromName = AmendedApprovedBy;
            ToName = CreatedBy;

            Subject = "BOM: Released Bom with Mr No. '" + MrNo + "' Amended Approved On: " + CurrentDateTime;
            FileName = "~/PROJECT_MANAGEMENT/EMAIL_FORMATS/05AmendedApprovedMail.htm";

            //SendMail(MrNo, FromName, From, To, ToName, Cc, Bcc, Subject, FileName, dtBomHeader, dtBomLine);
        }

        else if (StatusId == (int)BOMAllTypeEnums.EnumStatus.Cancelled)
        {
            From = CancelledByEmail;
            To = CreatedByEmail;
            Cc = PeEmail + ";" + PmEmail;
            Bcc = CancelledByEmail;

            FromName = CancelledBy;
            ToName = CreatedBy;

            Subject = "BOM: Released Bom with Mr No. '" + MrNo + "' Cancelled On: " + CurrentDateTime;
            FileName = "~/PROJECT_MANAGEMENT/EMAIL_FORMATS/06CancelledMail.htm";

            //SendMail(MrNo, FromName, From, To, ToName, Cc, Bcc, Subject, FileName, dtBomHeader, dtBomLine);
        }

        int val = SendMail(dtBomHeader, dtBomLine);

        if (val > 0) return isGood;

        return isBad;

    }



    private int GetMailType(int statusId)
    {
        int mailTypeId = 0;

        if (statusId == (int)BOMAllTypeEnums.EnumStatus.New)
            mailTypeId = (int)BOMAllTypeEnums.EnumMailType.ApprovalMail;
        else if (statusId == (int)BOMAllTypeEnums.EnumStatus.Approved)
            mailTypeId = (int)BOMAllTypeEnums.EnumMailType.ApprovedMail;
        else if (statusId == (int)BOMAllTypeEnums.EnumStatus.Amendment)
            mailTypeId = (int)BOMAllTypeEnums.EnumMailType.AmendmentMail;
        else if (statusId == (int)BOMAllTypeEnums.EnumStatus.Amended)
            mailTypeId = (int)BOMAllTypeEnums.EnumMailType.AmendedMail;
        else if (statusId == (int)BOMAllTypeEnums.EnumStatus.AmendedApproved)
            mailTypeId = (int)BOMAllTypeEnums.EnumMailType.AmendedApprovedMail;

        return mailTypeId;
    }


    private int SendMail(DataTable dtBomHeader, DataTable dtBomLine)
    {
        int returnVal = 0;

        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        //urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
        //link = "'" + urlTxt + "/Login.aspx?dmsstatusid=" + statusID + "'";

        if (!string.IsNullOrEmpty(From))
            mail.From = new MailAddress(From);

        mail.Subject = Subject;



        //To
        if (!string.IsNullOrEmpty(To))
        {
            To = To.TrimEnd(';');
            string items = string.Empty;
            string[] strTo = To.Split(';');

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


            //To = To.TrimEnd(';');
            //string[] strTo = To.Split(';');
            //foreach (string item in strTo)
            //{
            //    if (!string.IsNullOrEmpty(item))
            //    {
            //        mail.To.Add(item);
            //    }
            //}
        }
        else
        {
            returnVal = 0;
            return returnVal;
        }


        //CC
        if (!string.IsNullOrEmpty(Cc))
        {
            Cc = Cc.TrimEnd(';');
            string items = string.Empty;
            string[] strCC = Cc.Split(';');

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


        //BCC
        if (!string.IsNullOrEmpty(Bcc))
        {
            Bcc = Bcc.TrimEnd(';');
            string items = string.Empty;
            string[] strBCC = Bcc.Split(';');
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

        using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(FileName)))
        {
            Body = reader.ReadToEnd();
        }


        Body = Body.Replace("{#fromName#}", FromName);
        Body = Body.Replace("{#toName#}", ToName);
        Body = Body.Replace("{#mrNo#}", MrNo);
        Body = Body.Replace("{#amendmentReason#}", SentToAmendmentRemarks);


        mail.Body = Body;

        byte[] bytes = GetPDFBytes(dtBomHeader, dtBomLine);

        if (bytes != null)
        {
            mail.Attachments.Add(new Attachment(new MemoryStream(bytes), MrNo + ".pdf"));
        }

        try
        {
            if (!string.IsNullOrEmpty(To))
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

    public byte[] GetPDFBytes(DataTable dtBomHeader, DataTable dtBomLine)
    {
        try
        {
            byte[] pdfBytes;
            var cssText = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Styles/LOT.css"));


            string htmltxt = objBOMHtmlForPDF.GetHtmlForPDF(dtBomHeader, dtBomLine);

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