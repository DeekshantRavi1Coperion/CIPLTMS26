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
/// Summary description for HRTicketSendMail
/// </summary>
public class HRTicketSendMail
{
    HRTHtmlForPDF objHRTHtmlForPDF = new HRTHtmlForPDF();
    BAL.HR.Ticket objTicket = new BAL.HR.Ticket();
    DataSet dsMailInfo = new DataSet();

    DataTable dtTicketDetails = new DataTable();
    DataTable dtMailRecp = new DataTable();
    DataTable dtMailDesc = new DataTable();
    DataTable dtStatusDetails = new DataTable();


    private int _typeId;
    private int _statusId;
    private int _emailTypeId;

    private string _ticketNumber;
    private string _status;
    private string _ticketType;
    private string _ticketSubtype;

    private int _hodId;
    private string _uid;
    private string _pwd;
    private string _hodName;
    private string _hodEmail;

    private string _createdBy;
    private string _createdOn;
    private string _createdRemarks;

    private int _allocatedToId;
    private string _allocatedTo;
    private string _allocatedToEmail;

    private string _allocatedBy;
    private string _allocatedOn;
    private string _allocatedRemarks;

    private string _closedBy;
    private string _closedOn;
    private string _closedRemarks;

    private string _cancelledBy;
    private string _cancelledOn;
    private string _cancelledRemarks;

    private string _createdByEmail;
    private string _allocatedByEmail;
    private string _closedByEmail;
    private string _cancelledByEmail;


    private string _from;
    private string _fromName;
    private string _to;
    private string _toName;
    private string _cc;
    private string _bcc;
    private string _subject;
    private string _body;


    private int _ticketId;
    private string _salutation;
    private string _bodyLine;
    private string _bodyTable;
    private string _url;
    private string _closing;
    private string _signature;
    
    private string _docFileName1;
    private string _docFileExtn1;
    private Byte[] _attachment1;

    #region Properties

    public string Salutation
    {
        get
        {
            return _salutation;
        }

        set
        {
            _salutation = value;
        }
    }

    public string Body
    {
        get
        {
            return _bodyLine;
        }

        set
        {
            _bodyLine = value;
        }
    }

    public string Url
    {
        get
        {
            return _url;
        }

        set
        {
            _url = value;
        }
    }

    public string Closing
    {
        get
        {
            return _closing;
        }

        set
        {
            _closing = value;
        }
    }

    public string Signature
    {
        get
        {
            return _signature;
        }

        set
        {
            _signature = value;
        }
    }

    public int TicketId
    {
        get
        {
            return _ticketId;
        }

        set
        {
            _ticketId = value;
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

    public string BodyTable
    {
        get
        {
            return _bodyTable;
        }

        set
        {
            _bodyTable = value;
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

    public string CreatedOn
    {
        get
        {
            return _createdOn;
        }

        set
        {
            _createdOn = value;
        }
    }

    public string CreatedRemarks
    {
        get
        {
            return _createdRemarks;
        }

        set
        {
            _createdRemarks = value;
        }
    }

    public string AllocatedBy
    {
        get
        {
            return _allocatedBy;
        }

        set
        {
            _allocatedBy = value;
        }
    }

    public string AllocatedOn
    {
        get
        {
            return _allocatedOn;
        }

        set
        {
            _allocatedOn = value;
        }
    }

    public string AllocatedRemarks
    {
        get
        {
            return _allocatedRemarks;
        }

        set
        {
            _allocatedRemarks = value;
        }
    }

    public string ClosedBy
    {
        get
        {
            return _closedBy;
        }

        set
        {
            _closedBy = value;
        }
    }

    public string ClosedOn
    {
        get
        {
            return _closedOn;
        }

        set
        {
            _closedOn = value;
        }
    }

    public string ClosedRemarks
    {
        get
        {
            return _closedRemarks;
        }

        set
        {
            _closedRemarks = value;
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

    public string CancelledOn
    {
        get
        {
            return _cancelledOn;
        }

        set
        {
            _cancelledOn = value;
        }
    }

    public string CancelledRemarks
    {
        get
        {
            return _cancelledRemarks;
        }

        set
        {
            _cancelledRemarks = value;
        }
    }

    public int TypeId
    {
        get
        {
            return _typeId;
        }

        set
        {
            _typeId = value;
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

    public string TicketNumber
    {
        get
        {
            return _ticketNumber;
        }

        set
        {
            _ticketNumber = value;
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

    public string TicketType
    {
        get
        {
            return _ticketType;
        }

        set
        {
            _ticketType = value;
        }
    }

    public string TicketSubtype
    {
        get
        {
            return _ticketSubtype;
        }

        set
        {
            _ticketSubtype = value;
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

    public string AllocatedByEmail
    {
        get
        {
            return _allocatedByEmail;
        }

        set
        {
            _allocatedByEmail = value;
        }
    }

    public string ClosedByEmail
    {
        get
        {
            return _closedByEmail;
        }

        set
        {
            _closedByEmail = value;
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

    public string Body1
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

    public string Uid
    {
        get
        {
            return _uid;
        }

        set
        {
            _uid = value;
        }
    }

    public string Pwd
    {
        get
        {
            return _pwd;
        }

        set
        {
            _pwd = value;
        }
    }

    public int HodId
    {
        get
        {
            return _hodId;
        }

        set
        {
            _hodId = value;
        }
    }

    public int AllocatedToId
    {
        get
        {
            return _allocatedToId;
        }

        set
        {
            _allocatedToId = value;
        }
    }

    public string AllocatedTo
    {
        get
        {
            return _allocatedTo;
        }

        set
        {
            _allocatedTo = value;
        }
    }

    public string AllocatedToEmail
    {
        get
        {
            return _allocatedToEmail;
        }

        set
        {
            _allocatedToEmail = value;
        }
    }

    public string HodEmail
    {
        get
        {
            return _hodEmail;
        }

        set
        {
            _hodEmail = value;
        }
    }

    public string HodName
    {
        get
        {
            return _hodName;
        }

        set
        {
            _hodName = value;
        }
    }

    public byte[] Attachment1
    {
        get
        {
            return _attachment1;
        }

        set
        {
            _attachment1 = value;
        }
    }

    public string DocFileName1
    {
        get
        {
            return _docFileName1;
        }

        set
        {
            _docFileName1 = value;
        }
    }

    public string DocFileExtn1
    {
        get
        {
            return _docFileExtn1;
        }

        set
        {
            _docFileExtn1 = value;
        }
    }

    #endregion




    public HRTicketSendMail(int ticektId)
    {
        TicketId = ticektId;
    }

    public bool InitializeSendMail()
    {
        bool check = false;

        dsMailInfo = objTicket.GetHrTicketMailInfo(TicketId);
        if (dsMailInfo.Tables.Count == 0 && dsMailInfo.Tables[0].Rows.Count == 0) return false;

        dtTicketDetails = dsMailInfo.Tables[0];
        dtMailRecp = dsMailInfo.Tables[1];
        dtMailDesc = dsMailInfo.Tables[2];
        dtStatusDetails = dsMailInfo.Tables[3];

        check = ProcessSendMail(dtTicketDetails, dtMailRecp, dtMailDesc, dtStatusDetails);

        return check;
    }

    //, DataTable dtMailRecp
    //, DataTable dtMailDesc

    private bool ProcessSendMail(DataTable dtTicketDetails, DataTable dtMailRecp, DataTable dtMailDesc, DataTable dtStatusDetails)
    {
        bool isGood = true;
        bool isBad = false;


        DataRow dr0 = dtTicketDetails.Rows[0];

        TicketNumber = Convert.ToString(dr0["TICKET_NUMBER"]);

        TypeId = Convert.ToInt32(dr0["TICKET_TYPE_FID"]);
        StatusId = Convert.ToInt32(dr0["STATUS_FID"]);

        CreatedBy = Convert.ToString(dr0["CREATED_BY"]);
        CreatedByEmail = Convert.ToString(dr0["CREATED_BY_EMAIL"]);

        AllocatedBy = Convert.ToString(dr0["ALLOCATED_BY"]);
        AllocatedByEmail = Convert.ToString(dr0["ALLOCATED_BY_EMAIL"]);

        ClosedBy = Convert.ToString(dr0["CLOSED_BY"]);
        ClosedByEmail = Convert.ToString(dr0["CLOSED_BY_EMAIL"]);

        CancelledBy = Convert.ToString(dr0["CANCELLED_BY"]);
        CancelledByEmail = Convert.ToString(dr0["CANCELLED_BY_EMAIL"]);

        AllocatedToId = Convert.ToInt32(dr0["ALLOCATED_TO_ID"]);
        AllocatedTo = Convert.ToString(dr0["ALLOCATED_TO"]);
        AllocatedToEmail = Convert.ToString(dr0["ALLOCATED_TO_EMAIL"]);

        if (dr0["DOC_NAME1"] != DBNull.Value)
        {
            DocFileName1 = Convert.ToString(dr0["FILE_NAME1"]);

            if (Convert.ToString(dr0["EXTN"]).Contains("."))
                DocFileExtn1 = Convert.ToString(dr0["EXTN"]).TrimStart('.').Trim();
            else DocFileExtn1 = Convert.ToString(dr0["EXTN"]);

            Attachment1 = (byte[])dr0["DOC_NAME1"];
        }
        else
        {
            DocFileName1 = "";
            DocFileExtn1 = "";
            Attachment1 = null;
        }

        HodId = Convert.ToInt32(dtTicketDetails.Rows[0]["HOD_ID"]);
        Uid = Convert.ToString(dtTicketDetails.Rows[0]["HOD_UID"]);
        Pwd = Convert.ToString(dtTicketDetails.Rows[0]["HOD_PWD"]);
        HodName = Convert.ToString(dtTicketDetails.Rows[0]["HOD_NAME"]);
        HodEmail = Convert.ToString(dtTicketDetails.Rows[0]["HOD_EMAIL"]);

        MailRecp objMailRecp;
        MailDescriptions objMailDescriptions;

        EmailTypeId = GetMailType(TypeId, StatusId);

        //objMailRecp = GetMailRecp(EmailTypeId, CreatedByEmail, CreatedBy, false);
        //objMailDescriptions = GetMailDescriptions(EmailTypeId, false);

        if (StatusId == (int)HRTicketEnums.EnumStatus.Open)
        {
            FromName = CreatedBy;
            From = CreatedByEmail;

            objMailRecp = new MailRecp();
            if (TypeId == (int)HRTicketEnums.EnumType.HR)
            {
                objMailRecp.To = HodEmail;
                objMailRecp.ToName = HodName;
                objMailRecp.Cc = "";
                objMailRecp.Bcc = "";


                string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
                string approveLink = "'" + urlTxt + "/HR/TICKET/UpdateTicketStatus.aspx?ticketid=" + TicketId + "&actid=2&ud=" + Uid + "&pd=" + Pwd + "&emprecordid=" + Convert.ToString(HodId) + "'";
                string approveHref = "<a href=" + approveLink + ">Allocate Ticket</a>";

                objMailDescriptions = new MailDescriptions();

                objMailDescriptions.Subject = "HRD Ticket: " + TicketNumber;
                objMailDescriptions.Salutation = "Dear";
                objMailDescriptions.Body = "Please find attached created ticket and allocate.";
                objMailDescriptions.Url = approveHref;
                objMailDescriptions.Closing = "Regards";
                objMailDescriptions.Signature = CreatedBy;

                SendMail(FromName, From, dtTicketDetails, objMailRecp, objMailDescriptions, dtStatusDetails);



                FromName = "IT Team";
                From = CreatedByEmail;

                objMailRecp = new MailRecp();

                objMailRecp.To = CreatedByEmail;
                objMailRecp.ToName = CreatedBy;
                objMailRecp.Cc = "";
                objMailRecp.Bcc = "";


                objMailDescriptions = new MailDescriptions();

                objMailDescriptions.Subject = "HRD Ticket: " + TicketNumber;
                objMailDescriptions.Salutation = "Dear";
                objMailDescriptions.Body = "Please find attached ticket.";
                objMailDescriptions.Url = "";
                objMailDescriptions.Closing = "Regards";
                objMailDescriptions.Signature = "IT Team";

                SendMail(FromName, From, dtTicketDetails, objMailRecp, objMailDescriptions, dtStatusDetails);

            }
            else if (TypeId == (int)HRTicketEnums.EnumType.Admin)
            {
                foreach (DataRow drM in dtMailRecp.Select("EMAIL_TYPE_FID='" + (int)HRTicketEnums.EnumMailType.AdminOpen + "'"))
                {
                    if (drM["TO"] != DBNull.Value)
                        objMailRecp.To += Convert.ToString(drM["TO"]) + ";";
                }

                if (!string.IsNullOrEmpty(objMailRecp.To))
                    objMailRecp.To = objMailRecp.To.TrimEnd(';');

                objMailRecp.ToName = "Team";
                objMailRecp.Cc = "";
                objMailRecp.Bcc = "";



                objMailDescriptions = new MailDescriptions();

                objMailDescriptions.Subject = "HRD Ticket: " + TicketNumber;
                objMailDescriptions.Salutation = "Dear";
                objMailDescriptions.Body = "Please find attached created ticket and close.";
                objMailDescriptions.Url = "";
                objMailDescriptions.Closing = "Regards";
                objMailDescriptions.Signature = CreatedBy;

                SendMail(FromName, From, dtTicketDetails, objMailRecp, objMailDescriptions, dtStatusDetails);
            }





            //if (TypeId == (int)HRTicketEnums.EnumType.HR)
            //{
            //    //Send mail to createdby

            //    FromName = "IT Team";
            //    From = CreatedByEmail;

            //    //objMailRecp = GetMailRecp(EmailTypeId, CreatedByEmail, CreatedBy, true);                
            //    //objMailDescriptions = GetMailDescriptions(EmailTypeId, true);

            //    objMailRecp = new MailRecp();

            //    objMailRecp.To = CreatedByEmail;
            //    objMailRecp.ToName = CreatedBy;
            //    objMailRecp.Cc = "";
            //    objMailRecp.Bcc = "";


            //    objMailDescriptions = new MailDescriptions();

            //    objMailDescriptions.Subject = "HRD Ticket: " + TicketNumber;
            //    objMailDescriptions.Salutation = "Dear";
            //    objMailDescriptions.Body = "Please find attached ticket.";
            //    objMailDescriptions.Url = "";
            //    objMailDescriptions.Closing = "Regards";
            //    objMailDescriptions.Signature = "IT Team";

            //    SendMail(FromName, From, dtTicketDetails, objMailRecp, objMailDescriptions);
            //}
        }

        else if (StatusId == (int)HRTicketEnums.EnumStatus.Allocated)
        {
            FromName = AllocatedBy;
            From = AllocatedByEmail;


            objMailRecp = new MailRecp();

            objMailRecp.To = AllocatedToEmail;
            objMailRecp.ToName = AllocatedTo;
            objMailRecp.Cc = "";
            objMailRecp.Bcc = "";


            objMailDescriptions = new MailDescriptions();

            objMailDescriptions.Subject = "HRD Ticket: " + TicketNumber;
            objMailDescriptions.Salutation = "Dear";
            objMailDescriptions.Body = "Please find attached created ticket and close.";
            objMailDescriptions.Url = "";
            objMailDescriptions.Closing = "Regards";
            objMailDescriptions.Signature = AllocatedBy;

            SendMail(FromName, From, dtTicketDetails, objMailRecp, objMailDescriptions, dtStatusDetails);
        }

        else if (StatusId == (int)HRTicketEnums.EnumStatus.Closed)
        {
            FromName = ClosedBy;
            From = ClosedByEmail;


            objMailRecp = new MailRecp();

            objMailRecp.To = CreatedByEmail;
            objMailRecp.ToName = CreatedBy;
            objMailRecp.Cc = "";
            objMailRecp.Bcc = ClosedByEmail;


            objMailDescriptions = new MailDescriptions();

            objMailDescriptions.Subject = "HRD Ticket: " + TicketNumber;
            objMailDescriptions.Salutation = "Dear";
            objMailDescriptions.Body = "Please find attached closed ticket.";
            objMailDescriptions.Url = "";
            objMailDescriptions.Closing = "Regards";
            objMailDescriptions.Signature = ClosedBy;

            SendMail(FromName, From, dtTicketDetails, objMailRecp, objMailDescriptions, dtStatusDetails);
        }

        else if (StatusId == (int)HRTicketEnums.EnumStatus.Cancelled)
        {
            FromName = CancelledBy;
            From = CancelledByEmail;


            objMailRecp = new MailRecp();

            objMailRecp.To = CreatedByEmail;
            objMailRecp.ToName = CreatedBy;
            objMailRecp.Cc = "";
            objMailRecp.Bcc = CancelledByEmail;


            objMailDescriptions = new MailDescriptions();

            objMailDescriptions.Subject = "HRD Ticket: " + TicketNumber;
            objMailDescriptions.Salutation = "Dear";
            objMailDescriptions.Body = "Please find attached cancelled ticket.";
            objMailDescriptions.Url = "";
            objMailDescriptions.Closing = "Regards";
            objMailDescriptions.Signature = CancelledBy;

            SendMail(FromName, From, dtTicketDetails, objMailRecp, objMailDescriptions, dtStatusDetails);
        }

        return isGood;
    }

    private MailRecp GetMailRecp(int emailTypeId, string createdByEmail, string createdBy, bool checkCreatedBy)
    {
        MailRecp objMailRecp = new MailRecp();

        foreach (DataRow drM in dtMailRecp.Select("EMAIL_TYPE_FID='" + emailTypeId + "'"))
        {

            if (StatusId == (int)HRTicketEnums.EnumStatus.Open)
            {
                if (checkCreatedBy)
                {
                    objMailRecp.To = createdByEmail;
                    objMailRecp.ToName = createdBy;
                    objMailRecp.Cc = "";
                    objMailRecp.Bcc = "";
                }
                else
                {
                    if (drM["TO"] != DBNull.Value)
                        objMailRecp.To += Convert.ToString(drM["TO"]) + ";";

                    if (drM["TO_NAME"] != DBNull.Value)
                        objMailRecp.ToName += Convert.ToString(drM["TO_NAME"]) + ";";

                    if (drM["CC"] != DBNull.Value)
                        objMailRecp.Cc += Convert.ToString(drM["CC"]) + ";";

                    if (drM["BCC"] != DBNull.Value)
                        objMailRecp.Bcc += Convert.ToString(drM["BCC"]) + ";";



                    if (!string.IsNullOrEmpty(objMailRecp.To))
                        objMailRecp.To = objMailRecp.To.TrimEnd(';');

                    if (!string.IsNullOrEmpty(objMailRecp.ToName))
                        objMailRecp.ToName = objMailRecp.ToName.TrimEnd(';');

                    if (!string.IsNullOrEmpty(objMailRecp.Cc))
                        objMailRecp.Cc = objMailRecp.Cc.TrimEnd(';');

                    if (!string.IsNullOrEmpty(objMailRecp.Bcc))
                        objMailRecp.Bcc = objMailRecp.Bcc.TrimEnd(';');
                }
            }
            else if (StatusId == (int)HRTicketEnums.EnumStatus.Allocated)
            {
                objMailRecp.To = AllocatedToEmail;
                objMailRecp.ToName = AllocatedTo;
                objMailRecp.Cc = "";
                objMailRecp.Bcc = AllocatedByEmail;
            }
            else if (StatusId == (int)HRTicketEnums.EnumStatus.Closed)
            {
                objMailRecp.To = createdByEmail;
                objMailRecp.ToName = CreatedBy;
                objMailRecp.Cc = "";
                objMailRecp.Bcc = ClosedByEmail;
            }
            else if (StatusId == (int)HRTicketEnums.EnumStatus.Cancelled)
            {
                objMailRecp.To = createdByEmail;
                objMailRecp.ToName = CreatedBy;
                objMailRecp.Cc = "";
                objMailRecp.Bcc = CancelledByEmail;
            }

        }

        return objMailRecp;
    }

    private MailDescriptions GetMailDescriptions(int emailTypeId, bool checkCreatedBy)
    {
        MailDescriptions mailDescriptions = new MailDescriptions();

        foreach (DataRow drM in dtMailDesc.Select("EMAIL_TYPE_FID='" + emailTypeId + "'"))
        {
            mailDescriptions.Url = "";

            if (!checkCreatedBy)
            {
                string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);

                //approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?ticketid=" + TicketId + "&tourno=" + tourNo + "&actid=2&tourstatusid=" + tourStatusID + "&ud=" + userName + "&pd=" + password + "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";
                string approveLink = "'" + urlTxt + "/HR/TICKET/UpdateTicketStatus.aspx?ticketid=" + TicketId + "&actid=2&ud=" + Uid + "&pd=" + Pwd + "&emprecordid=" + Convert.ToString(HodId) + "'";
                string approveHref = "<a href=" + approveLink + ">Allocate Ticket</a>";

                mailDescriptions.Subject = "HRD Ticket: " + TicketNumber; //Convert.ToString(drM["SUBJECT"]);
                mailDescriptions.Salutation = Convert.ToString(drM["SALUTATION"]);

                if (TypeId == (int)HRTicketEnums.EnumType.HR)
                    mailDescriptions.Body = "Please find attached ticket and allocate to HR Team."; //Convert.ToString(drM["BODY"]);
                else
                    mailDescriptions.Body = "Please find attached ticket and close";

                mailDescriptions.Url = approveHref;// Convert.ToString(drM["URL"]);
                mailDescriptions.Closing = Convert.ToString(drM["CLOSING"]);
                mailDescriptions.Signature = Convert.ToString(drM["SIGNATURE"]);
            }
            else
            {
                mailDescriptions.Subject = "HRD Ticket: " + TicketNumber; //Convert.ToString(drM["SUBJECT"]);
                mailDescriptions.Salutation = Convert.ToString(drM["SALUTATION"]);

                //mailDescriptions.Body = Convert.ToString(drM["BODY"]);
                mailDescriptions.Body = "Please find attached created ticket.";

                mailDescriptions.Url = "";
                mailDescriptions.Closing = Convert.ToString(drM["CLOSING"]);
                mailDescriptions.Signature = Convert.ToString(drM["SIGNATURE"]);
            }
        }

        return mailDescriptions;
    }

    private int GetMailType(int typeId, int statusId)
    {
        int mailTypeId = 0;

        if (typeId == (int)HRTicketEnums.EnumType.HR)
        {
            if (statusId == (int)HRTicketEnums.EnumStatus.Open)
                mailTypeId = (int)HRTicketEnums.EnumMailType.HROpen;
            else if (statusId == (int)HRTicketEnums.EnumStatus.Allocated)
                mailTypeId = (int)HRTicketEnums.EnumMailType.HRAllocated;
            else if (statusId == (int)HRTicketEnums.EnumStatus.Closed)
                mailTypeId = (int)HRTicketEnums.EnumMailType.HRClosed;
            else if (statusId == (int)HRTicketEnums.EnumStatus.Cancelled)
                mailTypeId = (int)HRTicketEnums.EnumMailType.HRCancelled;
        }
        else if (typeId == (int)HRTicketEnums.EnumType.Admin)
        {
            if (statusId == (int)HRTicketEnums.EnumStatus.Open)
                mailTypeId = (int)HRTicketEnums.EnumMailType.AdminOpen;
            else if (statusId == (int)HRTicketEnums.EnumStatus.Closed)
                mailTypeId = (int)HRTicketEnums.EnumMailType.AdminClosed;
            else if (statusId == (int)HRTicketEnums.EnumStatus.Cancelled)
                mailTypeId = (int)HRTicketEnums.EnumMailType.AdminCancelled;
        }


        return mailTypeId;
    }



    private int SendMail(string fromName, string from
                       , DataTable dtTicketDetails
                       , MailRecp objMailRecp
                       , MailDescriptions objMailDescriptions
                       , DataTable dtStatusDetails)
    {
        int returnVal = 0;

        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        //urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
        //link = "'" + urlTxt + "/Login.aspx?dmsstatusid=" + statusID + "'";

        if (!string.IsNullOrEmpty(from))
            mail.From = new MailAddress(from);

        mail.Subject = objMailDescriptions.Subject;


        string to = "";
        string cc = "";
        string bcc = "";
        string fileName = "~/HR/TICKET/EMAIL_FORMATS/TicketMail.htm";

        if (!string.IsNullOrEmpty(objMailRecp.To))
        {
            to = objMailRecp.To.TrimEnd(';');
            string[] strTo = to.Split(';');
            foreach (string item in strTo)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.To.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(objMailRecp.Cc))
        {
            cc = objMailRecp.Cc.TrimEnd(';');
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

        if (!string.IsNullOrEmpty(objMailRecp.Bcc))
        {
            bcc = objMailRecp.Bcc.TrimEnd(';');
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
            Body = reader.ReadToEnd();
        }

        if (Attachment1 != null)
        {
            //mail.Attachments.Add(new Attachment(new MemoryStream(Attachment1), "Attachemnt1_" + TicketNumber + ".pdf"));

            mail.Attachments.Add(new Attachment(new MemoryStream(Attachment1), "Attachemnt1_" + TicketNumber + "." + DocFileExtn1));
        }


        Body = Body.Replace("{#Salutation#}", objMailDescriptions.Salutation);
        Body = Body.Replace("{#ToName#}", objMailRecp.ToName);
        Body = Body.Replace("{#Body#}", objMailDescriptions.Body);
        Body = Body.Replace("{#Url#}", objMailDescriptions.Url);
        Body = Body.Replace("{#Closing#}", objMailDescriptions.Closing);
        Body = Body.Replace("{#Signature#}", fromName);


        mail.Body = Body;

        byte[] bytes = GetPDFBytes(dtTicketDetails);

        if (bytes != null)
        {
            string ticketNumber = Convert.ToString(dtTicketDetails.Rows[0]["TICKET_NUMBER"]);
            mail.Attachments.Add(new Attachment(new MemoryStream(bytes), ticketNumber + ".pdf"));
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


    public byte[] GetPDFBytes(DataTable dtTicketDetails)
    {
        try
        {
            byte[] pdfBytes;
            var cssText = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Styles/LOT.css"));


            string htmltxt = objHRTHtmlForPDF.GetHtmlForPDF(dtTicketDetails, dtStatusDetails);

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