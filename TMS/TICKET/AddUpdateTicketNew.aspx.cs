using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;

public partial class TICKET_AddUpdateTicketNew : System.Web.UI.Page
{

    #region VARIABLES[===============]

    DataSet dsDepartment = new DataSet();
    DataSet dsTicketType = new DataSet();
    BAL.Ticket objTicket = new BAL.Ticket();

    int ticketTypeId = 0;
    string attachmentOneFileName = string.Empty;
    string ticketDescription = string.Empty;

    DataSet dsTicket = new DataSet();

    #endregion


    #region EVENTS[==================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                SetFocus(ddlTicketType);
                GetTicketType();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    int createdval = 0;
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (createdval == 0)
        {
            createdval = CreateTicket();
        }
    }

    protected void btnTicketList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TICKET/TicketList.aspx");
    }

    #endregion


    #region METHODS[=================]

    private void GetTicketType()
    {
        try
        {
            dsTicketType = objTicket.GetTicketType();
            if (dsTicketType.Tables.Count > 0 && dsTicketType.Tables[0].Rows.Count > 0)
            {
                ddlTicketType.DataSource = dsTicketType.Tables[0];
                ddlTicketType.DataTextField = "TICKET_TYPE_NAME";
                ddlTicketType.DataValueField = "TICKET_TYPE_ID";
                ddlTicketType.DataBind();
                ddlTicketType.Items.Insert(0, "Select");
                ddlTicketType.SelectedIndex = 1;

            }
            else
            {
                ddlTicketType.DataSource = null;
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

    private int CreateTicket()
    {
        try
        {
            ticketTypeId = Convert.ToInt32(ddlTicketType.SelectedValue);

            Byte[] attachmentOneBytes = null;
            if (fileUploadAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment1.PostedFile.FileName))
                {
                    attachmentOneFileName = fileUploadAttachment1.PostedFile.FileName;
                    attachmentOneBytes = GetFileBytes(fileUploadAttachment1.PostedFile.FileName, fileUploadAttachment1.PostedFile.InputStream);
                }
                else
                {
                    attachmentOneFileName = string.Empty;
                    attachmentOneBytes = null;
                }
            }
            else
            {
                attachmentOneFileName = string.Empty;
                attachmentOneBytes = null;
            }

            ticketDescription = txtTicketDescription.Text;

            int value = objTicket.CreateAndUpdateTicket(0, ticketTypeId, attachmentOneFileName, attachmentOneBytes,
                                        ticketDescription, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                pnlMsg.Visible = true;
                int sentMailValue = 0;
                dsTicket = objTicket.GetTicketByTicketID(value);
                if (dsTicket.Tables.Count > 0)
                {
                    sentMailValue = SendMailNew();
                    if (sentMailValue > 0)
                        SuccessMessage("Ticket No.: " + dsTicket.Tables[0].Rows[0]["TICKET_NO"] + " created and mail sent.");
                    else
                        SuccessMessage("Ticket No.: " + dsTicket.Tables[0].Rows[0]["TICKET_NO"] + " created");
                }
                SuccessMessage("Ticket No.: " + dsTicket.Tables[0].Rows[0]["TICKET_NO"] + " created");

                ddlTicketType.SelectedIndex = 0;
                txtTicketDescription.Text = string.Empty;

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
