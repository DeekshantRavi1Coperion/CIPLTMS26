using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.IO;

public partial class REPORTS_FACT_LOG_FactLogReportDeptWise : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Reports objReports = new BAL.Reports();
    DataSet dsLogReport = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string department = string.Empty;
    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["FINANCE"] = null;
                Session["PURCHASE"] = null;
                Session["STORE"] = null;
                Session["CFO_EMAIL"] = null;
                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;

                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                GetLogReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLogReport();

        btnSendFinanceLogReportMail.Visible = false;
        btnSendPurchaseLogReportMail.Visible = false;
        btnSendStoreLogReportMail.Visible = false;

        if (ddlDepartment.SelectedIndex > 0)
        {
            if (ddlDepartment.SelectedItem.Text == "FINANCE")
            {
                btnSendFinanceLogReportMail.Visible = true;
                gvPurchaseLogReport.DataSource = null;
                gvPurchaseLogReport.DataBind();
                gvStoreLogReport.DataSource = null;
                gvStoreLogReport.DataBind();
            }


            if (ddlDepartment.SelectedItem.Text == "PURCHASE")
            {
                btnSendPurchaseLogReportMail.Visible = true;
                gvFinanceLogReport.DataSource = null;
                gvFinanceLogReport.DataBind();
                gvStoreLogReport.DataSource = null;
                gvStoreLogReport.DataBind();
            }

            if (ddlDepartment.SelectedItem.Text == "STORE")
            {
                btnSendStoreLogReportMail.Visible = true;
                gvFinanceLogReport.DataSource = null;
                gvFinanceLogReport.DataBind();
                gvPurchaseLogReport.DataSource = null;
                gvPurchaseLogReport.DataBind();
            }
        }
        else
        {
            btnSendFinanceLogReportMail.Visible = true;
            btnSendPurchaseLogReportMail.Visible = true;
            btnSendStoreLogReportMail.Visible = true;
        }
    }

    protected void gvFinanceLogReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvPurchaseLogReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvStoreLogReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetLogReport();
    }

    protected void btnSendFinanceLogReportMail_Click(object sender, EventArgs e)
    {
        int value = SendMail(1);
        if (value > 0)
        {
            SuccessMessage("Mail sent successfully from finance..!");
        }
    }

    protected void btnSendPurchaseLogReportMail_Click(object sender, EventArgs e)
    {
        int value = SendMail(2);
        if (value > 0)
        {
            SuccessMessage("Mail sent successfully from purchase..!");
        }
    }

    protected void btnSendStoreLogReportMail_Click(object sender, EventArgs e)
    {
        int value = SendMail(3);
        if (value > 0)
        {
            SuccessMessage("Mail sent successfully of store..!");
        }
    }

    #endregion


    #region METHODS[=========================]

    private void GetLogReport()
    {
        try
        {

            dsDBDetails = (DataSet)Session["DB_DETAILS"];
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
            {
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
                lblFromDate.Text = Convert.ToDateTime(hdStartDateSearch.Value).ToString("dd-MMM-yyyy");
            }
            else
            {
                fromDate = string.Empty;
                lblFromDate.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
            {
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
                lblToDate.Text = Convert.ToDateTime(hdEndDateSearch.Value).ToString("dd-MMM-yyyy");
            }
            else
            {
                toDate = string.Empty;
                lblToDate.Text = string.Empty;
            }

            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }
            else
            {
                return;
            }

            if (ddlDepartment.SelectedIndex > 0)
            {
                if (ddlDepartment.SelectedItem.Text == "FINANCE")
                    department = "FINANCE";

                if (ddlDepartment.SelectedItem.Text == "PURCHASE")
                    department = "PURCHASE";

                if (ddlDepartment.SelectedItem.Text == "STORE")
                    department = "STORE";
            }
            else
            {
                department = string.Empty;
            }

            dsLogReport = objReports.GetFactLogReport(fromDate, toDate, department, dbNameA35, dbNameDLH, dbNameGNU, dbNameSEZ);
            if (dsLogReport.Tables.Count > 0)
            {
                if (ddlDepartment.SelectedIndex > 0)
                {
                    if (dsLogReport.Tables[0].Rows.Count > 0)
                    {
                        if (ddlDepartment.SelectedItem.Text == "FINANCE")
                        {
                            Session["FINANCE"] = dsLogReport.Tables[0];
                            gvFinanceLogReport.DataSource = dsLogReport.Tables[0];
                            gvFinanceLogReport.DataBind();
                        }

                        if (ddlDepartment.SelectedItem.Text == "PURCHASE")
                        {
                            Session["PURCHASE"] = dsLogReport.Tables[0];
                            gvPurchaseLogReport.DataSource = dsLogReport.Tables[0];
                            gvPurchaseLogReport.DataBind();
                        }

                        if (ddlDepartment.SelectedItem.Text == "STORE")
                        {
                            Session["STORE"] = dsLogReport.Tables[0];
                            gvStoreLogReport.DataSource = dsLogReport.Tables[0];
                            gvStoreLogReport.DataBind();
                        }
                    }
                    else if (dsLogReport.Tables[1].Rows.Count > 0)
                    {
                        Session["CFO_EMAIL"] = dsLogReport.Tables[1];
                    }
                    else
                    {
                        Session["FINANCE"] = null;
                        Session["PURCHASE"] = null;
                        Session["STORE"] = null;
                        Session["CFO_EMAIL"] = null;

                        gvFinanceLogReport.DataSource = null;
                        gvFinanceLogReport.DataBind();
                        gvPurchaseLogReport.DataSource = null;
                        gvPurchaseLogReport.DataBind();
                        gvStoreLogReport.DataSource = null;
                        gvStoreLogReport.DataBind();
                    }
                }
                else
                {
                    if (dsLogReport.Tables[0].Rows.Count > 0)
                    {
                        Session["FINANCE"] = dsLogReport.Tables[0];
                        gvFinanceLogReport.DataSource = dsLogReport.Tables[0];
                        gvFinanceLogReport.DataBind();
                    }
                    else
                    {
                        Session["FINANCE"] = null;
                        gvFinanceLogReport.DataSource = null;
                        gvFinanceLogReport.DataBind();
                    }

                    if (dsLogReport.Tables[1].Rows.Count > 0)
                    {
                        Session["PURCHASE"] = dsLogReport.Tables[1];
                        gvPurchaseLogReport.DataSource = dsLogReport.Tables[1];
                        gvPurchaseLogReport.DataBind();
                    }
                    else
                    {
                        Session["PURCHASE"] = null;
                        gvPurchaseLogReport.DataSource = null;
                        gvPurchaseLogReport.DataBind();
                    }

                    if (dsLogReport.Tables[2].Rows.Count > 0)
                    {
                        Session["STORE"] = dsLogReport.Tables[2];
                        gvStoreLogReport.DataSource = dsLogReport.Tables[2];
                        gvStoreLogReport.DataBind();
                    }
                    else
                    {
                        Session["STORE"] = null;
                        gvStoreLogReport.DataSource = null;
                        gvStoreLogReport.DataBind();
                    }
                    if (dsLogReport.Tables[3].Rows.Count > 0)
                    {
                        Session["CFO_EMAIL"] = dsLogReport.Tables[3];
                    }
                    else
                    {
                        Session["CFO_EMAIL"] = null;
                    }
                }
            }
            else
            {
                Session["FINANCE"] = null;
                Session["PURCHASE"] = null;
                Session["STORE"] = null;
                Session["CFO_EMAIL"] = null;

                gvFinanceLogReport.DataSource = null;
                gvFinanceLogReport.DataBind();
                gvPurchaseLogReport.DataSource = null;
                gvPurchaseLogReport.DataBind();
                gvStoreLogReport.DataSource = null;
                gvStoreLogReport.DataBind();
            }

            lblFinanceLogReportRecords.Text = "Records[" + gvFinanceLogReport.Rows.Count + "]";
            lblPurchaseLogReportRecords.Text = "Records[" + gvPurchaseLogReport.Rows.Count + "]";
            lblStoreLogReportRecords.Text = "Records[" + gvStoreLogReport.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int SendMail(int typeID)
    {
        try
        {
            DataTable dtLogDetail = new DataTable();
            DataTable dtCFOEmail = new DataTable();
            from = string.Empty;
            to = string.Empty;
            cc = string.Empty;
            string body = string.Empty;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();

            mail.Subject = "Fact Log Report From: " + lblFromDate.Text + " To: " + lblToDate.Text;

            string bodyTxt = string.Empty;
            string innerTxt = string.Empty;
            string footerText = "</table>";
            if (typeID == 1)
            {
                dtLogDetail = (DataTable)Session["FINANCE"];
                string headerTxt = "<p>&nbsp;</p><p>The following is FACT log report.</p><br /><table style='width: 90%' border='1' cellpadding='10' cellspacing='0'><tr><th align='left'>User</th><th align='right'>PI [Purchase Invoice]</th><th align='right'>SI [Sale Invoice]</th><th align='right'>JV [Journal Voucher]</th><th align='right'>DB_NOTE [Dabit Note]</th><th align='right'>CR_NOTE [Credit Note]</th><th align='right'>DB_ADJ [Debit Adj.]</th><th align='right'>CR_ADJ [Credit Adj.]</th></tr>";
                if (dtLogDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLogDetail.Rows)
                    {
                        innerTxt += "<tr> <td align='left'>" + Convert.ToString(dr["FUSER"]) + "</td> <td align='right'>" + Convert.ToString(dr["PI"]) + "</td> <td align='right'>" + Convert.ToString(dr["SI"]) + "</td> <td align='right'>" + Convert.ToString(dr["JV"]) + "</td> <td align='right'>" + Convert.ToString(dr["DB_NOTE"]) + "</td> <td align='right'>" + Convert.ToString(dr["CR_NOTE"]) + "</td> <td align='right'>" + Convert.ToString(dr["DB_ADJ"]) + "</td> <td align='right'>" + Convert.ToString(dr["CR_ADJ"]) + "</td> </tr>";
                    }
                }
                bodyTxt = headerTxt + innerTxt + footerText;
            }
            else if (typeID == 2)
            {
                dtLogDetail = (DataTable)Session["PURCHASE"];
                string headerTxt = "<p>&nbsp;</p><p>The following is FACT log report.</p><br /><table style='width: 90%' border='1' cellpadding='10' cellspacing='0'><tr><th align='left'>User</th><th align='right'>PO [Purchase Order]</th></tr>";
                if (dtLogDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLogDetail.Rows)
                    {
                        innerTxt += "<tr> <td align='left'>" + Convert.ToString(dr["FUSER"]) + "</td> <td align='right'>" + Convert.ToString(dr["PO"]) + "</td> </tr>";
                    }
                }
                bodyTxt = headerTxt + innerTxt + footerText;
            }
            else if (typeID == 3)
            {
                dtLogDetail = (DataTable)Session["STORE"];
                string headerTxt = "<p>&nbsp;</p><p>The following is FACT log report.</p><br /><table style='width: 90%' border='1' cellpadding='10' cellspacing='0'><tr><th align='left'>User</th><th align='right'>MRN [Material Recript Note]</th><th align='right'>ISS [Issue]</th><th align='right'>FG [Finish Good]</th></tr>";
                if (dtLogDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLogDetail.Rows)
                    {
                        innerTxt += "<tr> <td align='left'>" + Convert.ToString(dr["FUSER"]) + "</td> <td align='right'>" + Convert.ToString(dr["MRN"]) + "</td> <td align='right'>" + Convert.ToString(dr["ISS"]) + "</td> <td align='right'>" + Convert.ToString(dr["FG"]) + "</td> </tr>";
                    }
                }
                bodyTxt = headerTxt + innerTxt + footerText;
            }

            from = "ithelpdesk@coperion.com";
            mail.From = new MailAddress(from);

            if (dtLogDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtLogDetail.Rows)
                {
                    if (dr["EMAIL"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMAIL"])))
                    {
                        to += Convert.ToString(dr["EMAIL"]);
                        mail.To.Add(Convert.ToString(dr["EMAIL"]));
                    }
                }
            }

            //to = "trinetra-nand.upadhyay@coperion.com";
            //mail.To.Add(to);

            dtCFOEmail = (DataTable)Session["CFO_EMAIL"];
            if (dtCFOEmail.Rows.Count > 0)
            {
                if (typeID == 1)
                {
                    foreach (DataRow dr in dtCFOEmail.Rows)
                    {
                        if (dr["EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMAIL_ID"])))
                        {
                            cc += Convert.ToString(dr["EMAIL_ID"]) + ";";
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in dtCFOEmail.Select("EMP_RECORD_ID <>'5'"))
                    {
                        if (dr["EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMAIL_ID"])))
                            cc = Convert.ToString(dr["EMAIL_ID"]);
                        else
                            cc = string.Empty;
                    }
                }
            }

            if (!string.IsNullOrEmpty(cc))
            {
                string[] strCC = cc.TrimEnd(';').Split(';');
                foreach (string item in strCC)
                {
                    if (!string.IsNullOrEmpty(item))
                        mail.CC.Add(item);
                }
            }

            mail.IsBodyHtml = true;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/REPORTS/FACT_LOG/FactLogReportMail.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{#bodytable#}", bodyTxt);
            mail.Body = body;

            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                if (!string.IsNullOrEmpty(to))
                {
                    SmtpServer.Send(mail);
                    return 1;
                }
                else
                {
                    return 0;
                }
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
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
        }
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
