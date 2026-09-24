using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;

public partial class HR_TICKET_AddUpdateTicket : System.Web.UI.Page
{

    #region VARIABLES[===============]


    DataSet dsTicketType = new DataSet();
    DataSet dsTicketSubtype = new DataSet();
    BAL.HR.Ticket objTicket = new BAL.HR.Ticket();

    int createdval = 0;
    int _currentUserId = 0;
    int _ticketTypeId = 0;
    int _ticketSubtypeId = 0;
    string _attachmentOneFileName = string.Empty;
    string _attachmentTwoFileName = string.Empty;
    string _attachmentThreeFileName = string.Empty;
    string _ticketDescription = string.Empty;
    string _remarks = string.Empty;

    DataSet dsTicket = new DataSet();

    #endregion


    #region PROPERTIES

    public int TicketTypeId
    {
        get
        {
            _ticketTypeId = Convert.ToInt32(ddlTicketType.SelectedValue);
            return _ticketTypeId;
        }

        set
        {
            _ticketTypeId = value;
        }
    }

    public int TicketSubtypeId
    {
        get
        {
            if (ddlTicketSubtype.SelectedIndex > 0)
                _ticketSubtypeId = Convert.ToInt32(ddlTicketSubtype.SelectedValue);
            else _ticketSubtypeId = 0;

            return _ticketSubtypeId;
        }

        set
        {
            _ticketSubtypeId = value;
        }
    }

    public string AttachmentOneFileName
    {
        get
        {
            return _attachmentOneFileName;
        }

        set
        {
            _attachmentOneFileName = value;
        }
    }

    public string AttachmentTwoFileName
    {
        get
        {
            return _attachmentTwoFileName;
        }

        set
        {
            _attachmentTwoFileName = value;
        }
    }

    public string AttachmentThreeFileName
    {
        get
        {
            return _attachmentThreeFileName;
        }

        set
        {
            _attachmentThreeFileName = value;
        }
    }


    public string TicketDescription
    {
        get
        {
            _ticketDescription = txtTicketDescription.Text;
            return _ticketDescription;
        }

        set
        {
            _ticketDescription = value;
        }
    }

    public int CurrentUserId
    {
        get
        {
            _currentUserId = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            return _currentUserId;
        }

        set
        {
            _currentUserId = value;
        }
    }

    public string Remarks
    {
        get
        {
            if (!string.IsNullOrEmpty(txtTicketRemarks.Text))
                _remarks = txtTicketRemarks.Text;
            else _remarks = "";

            return _remarks;
        }

        set
        {
            _remarks = value;
        }
    }

    #endregion


    #region EVENTS[==================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                CurrentUserId = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                SetFocus(ddlTicketType);

                GetTicketType();
                GetBlankTicketSubtype();
                ddlTicketType.SelectedIndex = 1;
                ddlTicketType_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    protected void ddlTicketType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBlankTicketSubtype();

        if (ddlTicketType.SelectedIndex > 0)
            GetTicketSubtype(Convert.ToInt32(ddlTicketType.SelectedValue));
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (createdval == 0)
        {
            createdval = CreateTicket();
        }
    }

    protected void btnTicketList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HR/TICKET/TicketList.aspx");
    }

    #endregion


    #region METHODS[=================]

    private void GetTicketType()
    {
        try
        {
            dsTicketType = objTicket.GetTicketType();

            if (dsTicketType.Tables.Count > 0 &&
                dsTicketType.Tables[0].Rows.Count > 0)
            {
                ddlTicketType.DataSource = dsTicketType.Tables[0];
                ddlTicketType.DataTextField = "NAME";
                ddlTicketType.DataValueField = "PID";
                ddlTicketType.DataBind();
                ddlTicketType.Items.Insert(0, "Select");
                ddlTicketType.SelectedIndex = 0;

            }
            else
            {
                ddlTicketType.DataSource = null;
                ddlTicketType.Items.Clear();
                ddlTicketType.Items.Insert(0, "Select");
                ddlTicketType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTicketSubtype(int ticketTypeId)
    {
        try
        {
            dsTicketSubtype = objTicket.GetTicketSubtype(ticketTypeId);

            if (dsTicketSubtype.Tables.Count > 0 &&
                dsTicketSubtype.Tables[0].Rows.Count > 0)
            {
                ddlTicketSubtype.DataSource = dsTicketSubtype.Tables[0];
                ddlTicketSubtype.DataTextField = "NAME";
                ddlTicketSubtype.DataValueField = "PID";
                ddlTicketSubtype.DataBind();
                ddlTicketSubtype.Items.Insert(0, "Select");
                ddlTicketSubtype.SelectedIndex = 0;
            }
            else
                GetBlankTicketSubtype();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetBlankTicketSubtype()
    {
        ddlTicketSubtype.DataSource = null;
        ddlTicketSubtype.Items.Clear();
        ddlTicketSubtype.Items.Insert(0, "Select");
        ddlTicketSubtype.SelectedIndex = 0;
    }


    private int CreateTicket()
    {
        try
        {
            Byte[] attachmentOneBytes = null;
            Byte[] attachmentTwoBytes = null;
            Byte[] attachmentThreeBytes = null;

            if (fileUploadAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment1.PostedFile.FileName))
                {
                    AttachmentOneFileName = fileUploadAttachment1.PostedFile.FileName;
                    attachmentOneBytes = GetFileBytes(fileUploadAttachment1.PostedFile.FileName, fileUploadAttachment1.PostedFile.InputStream);
                }
                else
                {
                    AttachmentOneFileName = string.Empty;
                    attachmentOneBytes = null;
                }
            }
            else
            {
                AttachmentOneFileName = string.Empty;
                attachmentOneBytes = null;
            }

            if(fileUploadAttachment2.HasFile)
            {
                if(!string.IsNullOrEmpty(fileUploadAttachment2.PostedFile.FileName))
                {
                    AttachmentTwoFileName = fileUploadAttachment2.PostedFile.FileName;
                    attachmentTwoBytes = GetFileBytes(fileUploadAttachment2.PostedFile.FileName,fileUploadAttachment2.PostedFile.InputStream);
                }
                else
                {
                    AttachmentTwoFileName = string.Empty;
                    attachmentTwoBytes = null;
                }
            }
            else
            {
                AttachmentTwoFileName = string.Empty;
                attachmentTwoBytes = null;
            }

            if(fileUploadAttachment3.HasFile)
            {
                if(!string.IsNullOrEmpty(fileUploadAttachment3.PostedFile.FileName))
                {
                    AttachmentThreeFileName = fileUploadAttachment3.PostedFile.FileName;
                    attachmentThreeBytes = GetFileBytes(fileUploadAttachment3.PostedFile.FileName,fileUploadAttachment3.PostedFile.InputStream);
                }
                else
                {
                    AttachmentThreeFileName = string.Empty;
                    attachmentThreeBytes = null;
                }
            }
            else
            {
                AttachmentThreeFileName = string.Empty;
                attachmentThreeBytes = null;
            }


            int insertedId = 0;
            bool sentMailValue = false;
            string ticketNumber = string.Empty;

            string retValue = objTicket.CreateAndUpdateTicket
                    (0
                    , TicketTypeId
                    , TicketSubtypeId
                    , AttachmentOneFileName
                    , attachmentOneBytes
                    , AttachmentTwoFileName
                    , attachmentTwoBytes
                    , AttachmentThreeFileName
                    , attachmentThreeBytes
                    , TicketDescription
                    , Remarks
                    , CurrentUserId);

            if (!string.IsNullOrEmpty(retValue))
            {
                insertedId = Convert.ToInt32(retValue.Split(':')[0]);
                ticketNumber = Convert.ToString(retValue.Split(':')[1]);

                if (insertedId == 0) return 0;

                HRTicketSendMail objSendMail = new HRTicketSendMail(insertedId);
                sentMailValue = objSendMail.InitializeSendMail();

                if (sentMailValue)
                    SuccessMessage("Ticket No.: " + ticketNumber + " created and mail sent successfully.");
                else
                    SuccessMessage("Ticket No.: " + ticketNumber + " created successfully.");

                Reset();

                return 1;
            }
            else
            {
                ExceptionMessage("Please try again");
                return 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
        }
    }

    private void Reset()
    {
        ddlTicketType.SelectedIndex = 0;
        GetBlankTicketSubtype();
        txtTicketDescription.Text = string.Empty;
        txtTicketRemarks.Text = string.Empty;
    }

    //private byte[] GetFileBytes(string fileName, Stream stream)
    //{
    //    Byte[] GSTbytes = null;
    //    #region
    //    try
    //    {
    //        string GSTFilePath = fileName;
    //        string GSTFileName = Path.GetFileName(GSTFilePath);
    //        string GSText = Path.GetExtension(GSTFileName);
    //        string GSTContentType = String.Empty;
    //        switch (GSText)
    //        {
    //            case ".jpg":
    //                GSTContentType = "image/jpg";
    //                break;
    //            case ".jpeg":
    //                GSTContentType = "image/jpeg";
    //                break;
    //            case ".bmp":
    //                GSTContentType = "image/bmp";
    //                break;
    //            case ".png":
    //                GSTContentType = "image/png";
    //                break;
    //            case ".gif":
    //                GSTContentType = "image/gif";
    //                break;
    //            case ".pdf":
    //                GSTContentType = "application/pdf";
    //                break;
    //            case ".JPG":
    //                GSTContentType = "image/JPG";
    //                break;
    //            case ".JPEG":
    //                GSTContentType = "image/JPEG";
    //                break;
    //            case ".BMP":
    //                GSTContentType = "image/BMP";
    //                break;
    //            case ".PNG":
    //                GSTContentType = "image/PNG";
    //                break;
    //            case ".GIF":
    //                GSTContentType = "image/GIF";
    //                break;
    //            case ".PDF":
    //                GSTContentType = "application/PDF";
    //                break;

    //        }
    //        Stream GSTfs = null;
    //        BinaryReader GSTbr = null;
    //        if (GSTContentType != String.Empty)
    //        {
    //            try
    //            {
    //                GSTfs = stream;
    //                GSTfs.Position = 0;
    //                GSTbr = new BinaryReader(GSTfs);
    //                GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //        else
    //        {
    //            ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //    }
    //    #endregion
    //    return GSTbytes;
    //}

    private byte[] GetFileBytes(string fileName, Stream stream)
    {
        Byte[] GSTbytes = null;
        #region
        try
        {
            string GSTFilePath = fileName;
            string GSTFileName = Path.GetFileName(GSTFilePath);
            string GSText = Path.GetExtension(GSTFileName);
            string GSTContentType = String.Empty;
            switch (GSText)
            {
                case ".jpg":
                    GSTContentType = "image/jpg";
                    break;
                case ".jpeg":
                    GSTContentType = "image/jpeg";
                    break;
                case ".bmp":
                    GSTContentType = "image/bmp";
                    break;
                case ".png":
                    GSTContentType = "image/png";
                    break;
                case ".gif":
                    GSTContentType = "image/gif";
                    break;
                case ".pdf":
                    GSTContentType = "application/pdf";
                    break;
                case ".xls":
                    GSTContentType = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    GSTContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".JPG":
                    GSTContentType = "image/JPG";
                    break;
                case ".JPEG":
                    GSTContentType = "image/JPEG";
                    break;
                case ".BMP":
                    GSTContentType = "image/BMP";
                    break;
                case ".PNG":
                    GSTContentType = "image/PNG";
                    break;
                case ".GIF":
                    GSTContentType = "image/GIF";
                    break;
                case ".PDF":
                    GSTContentType = "application/PDF";
                    break;
                case ".XLS":
                    GSTContentType = "application/vnd.ms-excel";
                    break;
                case ".XLSX":
                    GSTContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".eml":
                    GSTContentType = "message/rfc822";   // Standard email MIME type
                    break;

                case ".msg":
                    GSTContentType = "application/vnd.ms-outlook"; // Outlook email file
                    break;

                case ".EML":
                    GSTContentType = "message/rfc822";
                    break;

                case ".doc":
                    GSTContentType = "application/msword";
                    break;

                case ".docx":
                    GSTContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                case ".DOC":
                    GSTContentType = "application/msword";
                    break;

                case ".DOCX":
                    GSTContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                case ".MSG":
                    GSTContentType = "application/vnd.ms-outlook";
                    break;


            }
            Stream GSTfs = null;
            BinaryReader GSTbr = null;
            if (GSTContentType != String.Empty)
            {
                try
                {
                    GSTfs = stream;
                    GSTfs.Position = 0;
                    GSTbr = new BinaryReader(GSTfs);
                    GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }

    private int SendMailNew()
    {
        int returnvalue = 0;
        try
        {
            string msg = string.Empty;
            string ticketno = string.Empty;
            int tickettypeid = 0;
            string tickettype = string.Empty;
            string createddate = string.Empty;
            string createdby = string.Empty;
            string createdbyemail = string.Empty;
            string description = string.Empty;

            string hrteamemail = string.Empty;
            string itteamemail = string.Empty;

            string fromEmail = string.Empty;
            string toEmail = string.Empty;
            string ccEmail = string.Empty;


            if (dsTicket.Tables.Count > 0)
            {
                if (dsTicket.Tables[0].Rows.Count > 0)
                {
                    ticketno = Convert.ToString(dsTicket.Tables[0].Rows[0]["TICKET_NO"]);
                    tickettypeid = Convert.ToInt32(dsTicket.Tables[0].Rows[0]["TICKET_TYPE_ID"]);
                    tickettype = Convert.ToString(dsTicket.Tables[0].Rows[0]["TICKET_TYPE_NAME"]);

                    createddate = Convert.ToString(dsTicket.Tables[0].Rows[0]["CREATED_ON"]);
                    createdby = Convert.ToString(dsTicket.Tables[0].Rows[0]["CREATED_BY"]);
                    createdbyemail = Convert.ToString(dsTicket.Tables[0].Rows[0]["CREATED_BY_EMAIL"]);
                    description = Convert.ToString(dsTicket.Tables[0].Rows[0]["DESCRIPTION"]);
                }



                if (dsTicket.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTicket.Tables[1].Rows)
                    {
                        itteamemail += Convert.ToString(dr["IT_EMAIL_ID"]) + ";";
                    }
                    itteamemail = itteamemail.TrimEnd(';');
                }

                if (dsTicket.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTicket.Tables[2].Rows)
                    {
                        hrteamemail += Convert.ToString(dr["HR_EMAIL_ID"]) + ";";
                    }
                    hrteamemail = hrteamemail.TrimEnd(';');
                }

                fromEmail = createdbyemail;

                if (tickettypeid == 12 && tickettype == "HR-Software")
                {
                    toEmail = hrteamemail;
                    //ccEmail = itteamemail + ";ruchi.verma@coperion.com;" + createdbyemail;
                    ccEmail = createdbyemail + ";ruchi.verma@coperion.com;";
                    // ccEmail = createdbyemail + ";deekshant.ravi@coperion.com;";
                }
                else
                {
                    toEmail = itteamemail;
                    ccEmail = createdbyemail + ";ruchi.verma@coperion.com;";
                    //ccEmail = createdbyemail + ";deekshant.ravi@coperion.com;";
                }


                if (!string.IsNullOrEmpty(toEmail))
                    toEmail = toEmail.TrimEnd(';');

                if (!string.IsNullOrEmpty(ccEmail))
                    ccEmail = ccEmail.TrimEnd(';');

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                mail.From = new MailAddress(fromEmail);

                if (!string.IsNullOrEmpty(toEmail))
                {
                    string[] strTo = toEmail.Split(';');
                    foreach (string item in strTo)
                    {
                        if (!string.IsNullOrEmpty(item))
                            mail.To.Add(item);
                    }
                }
                else
                {
                    returnvalue = 0;
                }

                if (!string.IsNullOrEmpty(ccEmail))
                {
                    string[] strCC = ccEmail.Split(';');
                    foreach (string item in strCC)
                    {
                        if (!string.IsNullOrEmpty(item))
                            mail.CC.Add(item);
                    }
                }

                mail.Subject = "Ticket Reference #.: '" + ticketno + "' Created On: " + createddate;
                mail.IsBodyHtml = true;

                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/TICKET/EMAIL_FORMATS/NewTicketMail.htm")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{#TicketNo#}", ticketno);
                body = body.Replace("{#TicketType#}", tickettype);
                body = body.Replace("{#CreatedDate#}", createddate);
                body = body.Replace("{#CreatedBy#}", createdby);
                body = body.Replace("{#Description#}", description);
                mail.Body = body;

                SmtpServer.Host = "eusmtp.hi.corp";
                SmtpServer.Port = 25;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                try
                {
                    SmtpServer.Send(mail);
                    return 1;
                }
                catch (Exception ex)
                {
                    string exMsg = ex.ToString();
                    if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                        return 1;

                    else
                        return 0;
                }
            }
        }
        catch (Exception ex)
        {
            returnvalue = 0;
        }
        return returnvalue;
    }

    private void SuccessMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion



}
