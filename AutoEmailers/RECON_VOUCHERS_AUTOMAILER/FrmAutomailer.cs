using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RECON_VOUCHERS_AUTOMAILER
{
    public partial class FrmAutomailer : Form
    {
        string subject = string.Empty;
        string body = string.Empty;
        string bodyLine = string.Empty;
        string salutation = string.Empty;
        string closingLine = string.Empty;
        string regards = string.Empty;
        string content = string.Empty;


        string from = string.Empty;
        string to = string.Empty;
        string cc = string.Empty;
        string bcc = string.Empty;

        public FrmAutomailer()
        {
            InitializeComponent();
        }

        private void FrmAutomailer_Load(object sender, EventArgs e)
        {
            GetOTReportAndSendMail();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }


        private void GetOTReportAndSendMail()
        {
            try
            {
                DataSet dsReport = new DataSet();
                dsReport = GetDetails();

                string msg = string.Empty;
                if (dsReport.Tables.Count > 0)
                {
                    int val = CreateAttachmentsAndSendMail(dsReport);
                    if (val > 0)
                    {
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                SendErrorMail(ex.ToString(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                Application.Exit();
            }
            finally
            {
                Application.Exit();
            }
        }


        #region SEND_MAIL[==============]

        private int CreateAttachmentsAndSendMail(DataSet dsReports)
        {
            try
            {

                if (dsReports.Tables[1].Rows.Count == 0) return 0;

                from = dsReports.Tables[0].Rows[0]["FROM"].ToString();
                subject = dsReports.Tables[0].Rows[0]["SUBJECT"].ToString();
                salutation = dsReports.Tables[0].Rows[0]["SALUTATION"].ToString();
                closingLine = dsReports.Tables[0].Rows[0]["CLOSING_LINE"].ToString();
                regards = dsReports.Tables[0].Rows[0]["REGARDS"].ToString();
                bodyLine = dsReports.Tables[0].Rows[0]["BODY"].ToString();
                cc = dsReports.Tables[0].Rows[0]["CC"].ToString();
                bcc = dsReports.Tables[0].Rows[0]["BCC"].ToString();



                int sendMailValue = 0;
                int retVal = 0;
                DataTable dtContent = new DataTable();
                dtContent.Columns.Add("VOUCHER_TYPE", typeof(string));
                dtContent.Columns.Add("VOUCHER_COUNTS", typeof(int));

                foreach (DataRow item in dsReports.Tables[1].Rows)
                {

                    salutation = dsReports.Tables[0].Rows[0]["SALUTATION"].ToString() + " " + item["EMPLOYEE_NAME"].ToString() + ",";
                    
                    content = string.Empty;
                    dtContent.Rows.Clear();
                    
                    string factIserName = item["CREATED_BY"].ToString();
                    to = item["EMAIL_ID"].ToString();

                    foreach (DataRow drc in dsReports.Tables[2].Select("CREATED_BY = '" + factIserName + "'"))
                    {
                        DataRow dr = dtContent.NewRow();
                        dr["VOUCHER_TYPE"] = drc["VOUCHER_TYPE"];
                        dr["VOUCHER_COUNTS"] = drc["VOUCHER_COUNTS"];

                        dtContent.Rows.Add(dr);
                    }
                    content = GenerateHTML(dtContent);


                    to = to.TrimEnd(';');
                    cc = cc.TrimEnd(';');
                    bcc = bcc.TrimEnd(';');

                    if (dtContent != null && dtContent.Rows.Count > 0)
                    {
                        content = GenerateHTML(dtContent);
                        sendMailValue = SendMail
                           (
                              from
                            , subject
                            , bodyLine
                            , salutation
                            , closingLine
                            , regards
                            , to
                            , cc
                            , bcc
                            , content
                            );
                    }
                    else
                        retVal = 0;

                    if (sendMailValue > 0)
                        retVal = 1;                  
                }                

                return retVal;
            }
            catch (Exception ex)
            {
                SendErrorMail(ex.Message, DateTime.Now.ToString());
                return 0;

            }
        }


        public string GenerateHTML(DataTable dtReport)
        {
            // Create a StringBuilder to hold the HTML table
            StringBuilder htmlTable = new StringBuilder();

            // Start the table
            htmlTable.Append("<table border='1'style='padding: 10px;border-color: black;width: 80%;font-family: Calibri;border-collapse:collapse;'>");

            // Add header row
            htmlTable.Append("<tr style='background-color:cornflowerblue;color:white;font-size: 12px;'>");

            htmlTable.Append("<td width='40%'><b>Voucher Type</b></td>");
            htmlTable.Append("<td width='30%' style='text-align:right;'><b>Available Vouchers</b></td>");
            htmlTable.Append("</tr>");

            // Add rows dynamically
            foreach (DataRow dr in dtReport.Rows)
            {
                htmlTable.Append("<tr style='font-size: 12px;'>");

                htmlTable.Append($"<td>" + Convert.ToString(dr["VOUCHER_TYPE"]) + "</td>");
                htmlTable.Append($"<td style='text-align:right;'>" + Convert.ToString(dr["VOUCHER_COUNTS"]) + "</td>");

                htmlTable.Append("</tr>");
            }

            // End the table
            htmlTable.Append("</table>");

            // Convert StringBuilder to string
            string tableHtml = htmlTable.ToString();

            return tableHtml;
        }


        private int SendMail(
                             string from
                           , string subject
                           , string bodyLine
                           , string salutation
                           , string closingLine
                           , string regards
                           , string to
                           , string cc
                           , string bcc
                           , string content
            )
        {
            //string from = "ithelpdesk@coperion.com";
            int returnVal = 0;
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            MailMessage mail = new MailMessage();

            mail.Subject = subject;
            mail.From = new MailAddress(from);

            //to = "deekshant.ravi1@coperion.com";
            //cc = "deekshant.ravi1@coperion.com";
            //bcc = "deekshant.ravi1@coperion.com";

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
                string[] strCC = cc.Split(';');
                foreach (string item in strCC)
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
                string[] strBcc = bcc.Split(';');
                foreach (string item in strBcc)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.Bcc.Add(item);
                    }
                }
            }


            if (!string.IsNullOrEmpty(to))
            {
                mail.IsBodyHtml = true;
                StreamReader reader = new StreamReader(Application.StartupPath + @"\\EmailFormat.html");
                body = reader.ReadToEnd();

                body = body.Replace("{#salutation#}", salutation);
                body = body.Replace("{#bodyLine#}", bodyLine);
                body = body.Replace("{#content#}", content);
                body = body.Replace("{#closingLine#}", closingLine);
                body = body.Replace("{#regards#}", regards);

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
                        returnVal = 1;

                    else
                        returnVal = 0;
                }
            }
            else
                returnVal = 0;


            return returnVal;
        }


        private int SendErrorMail(string errorMsg, string date)
        {
            body = string.Empty;
            subject = string.Empty;
            from = string.Empty;
            to = string.Empty;


            subject = "Vouchers Mailer Error " + date;
            from = "ithelpdesk@coperion.com";

            int returnVal = 0;
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            MailMessage mail = new MailMessage();

            mail.Subject = subject;
            mail.From = new MailAddress(from);


            //to = "trinetra-nand.upadhyay@coperion.com;minni.gupta@coperion.com";
            to = "trinetra-nand.upadhyay@coperion.com;minni.gupta@coperion.com;deekshant.ravi1@coperion.com";
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

            if (!string.IsNullOrEmpty(to))
            {
                mail.IsBodyHtml = true;
                StreamReader reader = new StreamReader(Application.StartupPath + @"\\ErrorMail.html");
                body = reader.ReadToEnd();
            }
            else
                returnVal = 0;

            body = body.Replace("{#error#}", errorMsg);

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
                    returnVal = 1;

                else
                    returnVal = 0;
            }


            return returnVal;

        }

        #endregion




        #region BAL[====================]

        public DataSet GetDetails()
        {
            DataSet ds = new DataSet();
            string cipltmsconnectionstring = ConfigurationManager.ConnectionStrings["CIPLTMS"].ConnectionString;
            SqlConnection con = new SqlConnection(cipltmsconnectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbVoucher].[sp_get_recon_vouchers_for_email]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 600;

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null)
                return ds;

            return null;
        }



        #endregion
    }
}
