using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;

public partial class Login : System.Web.UI.Page
{

    #region VARIABLES[===========================]

    BAL.Common objCommon = new BAL.Common();

    #endregion


    #region EVENTS[==============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckEmployeeLogedIn();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        ValidateAndLogin();
    }

    protected void chkForgetPassword_CheckedChanged(object sender, EventArgs e)
    {

        txtEmployeeID.Enabled = false;
        txtUserNameNew.Enabled = false;

        if (chkForgetPassword.Checked)
            pnlForgetPassword.Visible = true;
        else
            pnlForgetPassword.Visible = false;

        if (rdEmployeeID.Checked)
            txtEmployeeID.Enabled = true;

        if (rdUserName.Checked)
            txtUserNameNew.Enabled = true;

    }

    protected void btnSendPassword_Click(object sender, EventArgs e)
    {
        SendPassword();

        rdUserName.Checked = true;
        rdEmployeeID.Checked = false;

        txtUserNameNew.Enabled = true;
        txtEmployeeID.Enabled = false;

        txtUserNameNew.Text = string.Empty;
        txtEmployeeID.Text = string.Empty;
    }

    #endregion


    #region METHODS[=============================]

    private void CheckEmployeeLogedIn()
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["sanctionno"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
        {
            Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/TravelStatementListNewOne.aspx?sanctionno=" + Convert.ToString(Request.QueryString["sanctionno"]));
        }
        else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tourno"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
        {
            Response.Redirect("~/TOUR_AND_TRAVELS/TOUR/TourInformationListNew.aspx?tourno=" + Convert.ToString(Request.QueryString["tourno"]));
        }
        else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tfno"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
        {
            //Response.Redirect("~/PROJECT/LOT/LOTTransmittalFactoryList.aspx?tfno=" + Convert.ToString(Request.QueryString["tfno"]));
            Response.Redirect("~/PROJECT/LOT/LOTTransmittalFactoryList.aspx?tfno=" + Convert.ToString(Request.QueryString["tfno"]) + "&unitid=" + Convert.ToString(Request.QueryString["unitid"]));
        }
        else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["dmsstatusid"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
        {
            Response.Redirect("~/PROJECT/DMS/DesignList.aspx?dmsstatusid=" + Convert.ToString(Request.QueryString["dmsstatusid"]));

            //if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Manager))
            //{
            //    Response.Redirect("~/PROJECT/DESIGN_MGMT/DesignListAssignmentView.aspx?drawingno=" + Convert.ToString(Request.QueryString["drawingno"]));
            //}
            //else if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.DesignChecker))
            //{
            //    Response.Redirect("~/PROJECT/DESIGN_MGMT/DesignListCheckingView.aspx?drawingno=" + Convert.ToString(Request.QueryString["drawingno"]));
            //}
            //else if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
            //{
            //    Response.Redirect("~/PROJECT/DESIGN_MGMT/DesignListDesignView.aspx?drawingno=" + Convert.ToString(Request.QueryString["drawingno"]));
            //}
            //else
            //{
            //    Response.Redirect("~/PROJECT/DESIGN_MGMT/DesignListProjectView.aspx?drawingno=" + Convert.ToString(Request.QueryString["drawingno"]));
            //}
        }
        else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["PAN"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
        {
            Response.Redirect("~/VENDOR_CUSTOMER_MGMT/VENDOR/VendorsList.aspx?PAN=" + Convert.ToString(Request.QueryString["PAN"]) + "&name=" + Convert.ToString(Request.QueryString["name"]));
        }
        else
        {
            SetFocus(txtUserName);
            Session["EMP_RECORD_ID"] = null;
            Session["EMPLOYEE_NAME"] = null;
            Session["UNIT_ID"] = null;
            Session["USER_NAME"] = null;
            Session["PASSWORD"] = null;
            Session["EMAIL_ID"] = null;
            Session["USER_TYPE"] = null;
            Session["IS_TEAMLEADER"] = null;
            Session["TEAMLEADER_ID"] = null;
            Session["TEAMMEMBERS"] = null;
            Session["DEPARTMENT_ID"] = null;
            Session["TIMESHEET_DEPT_ID"] = null;
            Session["LOGIN_TIME"] = null;
            Session["TS_APPROVAL_BY"] = null;
            Session["DESIGN_RESPONSIBLE_ENGG_ID"] = null;
            Session["DESIGN_CHECKER_ID"] = null;
            Session["PO_PIVOT_GROUP_APPROVER_ID"] = null;
            Session["PUNCH_IN"] = null;
            Session["CHK_ACC"] = null;
        }
    }

    private void ValidateAndLogin()
    {
        try
        {
            DataSet ds = new DataSet();
            string userName = string.Empty;
            string password = string.Empty;
            userName = Convert.ToString(txtUserName.Text).Trim();
            password = Convert.ToString(txtPassword.Text).Trim();
            ds = objCommon.ValidateAndLogin(userName, password);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["EMP_RECORD_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["EMP_RECORD_ID"]);
                Session["EMPLOYEE_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                Session["UNIT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["UNIT_ID"]);
                Session["USER_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_NAME"]);
                Session["PASSWORD"] = Convert.ToString(ds.Tables[0].Rows[0]["PASSWORD"]);
                Session["EMAIL_ID"] = Convert.ToString(ds.Tables[0].Rows[0]["EMAIL_ID"]);
                Session["USER_TYPE"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_TYPE"]);
                Session["IS_TEAMLEADER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["IS_TEAMLEADER"]);
                Session["TEAMLEADER_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TEAMLEADER_ID"]);
                Session["DEPARTMENT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]);
                Session["TIMESHEET_DEPT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TIMESHEET_DEPT_ID"]);
                Session["TS_APPROVAL_BY"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TS_APPROVAL_BY"]);
                Session["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DESIGN_RESPONSIBLE_ENGG_ID"]);
                Session["DESIGN_CHECKER_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DESIGN_CHECKER_ID"]);
                Session["PO_PIVOT_GROUP_APPROVER_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["PO_PIVOT_GROUP_APPROVER_ID"]);
                Session["PUNCH_IN"] = Convert.ToString(ds.Tables[0].Rows[0]["PUNCH_IN"]);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    Session["TEAMMEMBERS"] = ds.Tables[1];
                }

                if (ds.Tables.Count > 0)
                {
                    Session["USERINFO"] = ds;
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    DataRow dr4 = ds.Tables[4].Rows[0];
                    Session["CHK_ACC"] = Convert.ToInt32(dr4["ACC_EMP_RECORD_ID"]);
                }


                //if (Convert.ToInt32(Request.QueryString["statementid"]) > 0)
                //    Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/UpdateTravelStatementStatusNewOne.aspx?statementid=" + Convert.ToInt32(Request.QueryString["statementid"]));
                //else
                //    Response.Redirect("~/Default.aspx");


                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["sanctionno"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
                {
                    Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/TravelStatementListNewOne.aspx?sanctionno=" + Convert.ToString(Request.QueryString["sanctionno"]));
                }
                else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tourno"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
                {
                    Response.Redirect("~/TOUR_AND_TRAVELS/TOUR/TourInformationListNew.aspx?tourno=" + Convert.ToString(Request.QueryString["tourno"]));
                }
                else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tfno"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
                {
                    //Response.Redirect("~/PROJECT/LOT/LOTTransmittalFactoryList.aspx?tfno=" + Convert.ToString(Request.QueryString["tfno"]));
                    Response.Redirect("~/PROJECT/LOT/LOTTransmittalFactoryList.aspx?tfno=" + Convert.ToString(Request.QueryString["tfno"]) + "&unitid=" + Convert.ToString(Request.QueryString["unitid"]));
                }
                else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["dmsstatusid"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
                {
                    Response.Redirect("~/PROJECT/DMS/DesignList.aspx?dmsstatusid=" + Convert.ToString(Request.QueryString["dmsstatusid"]));

                    //if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Manager))
                    //{
                    //    Response.Redirect("~/PROJECT/DESIGN_MGMT/DesignListAssignmentView.aspx?drawingno=" + Convert.ToString(Request.QueryString["drawingno"]));
                    //}
                    //else if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.DesignChecker))
                    //{
                    //    Response.Redirect("~/PROJECT/DESIGN_MGMT/DesignListCheckingView.aspx?drawingno=" + Convert.ToString(Request.QueryString["drawingno"]));
                    //}
                    //else if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                    //{
                    //    Response.Redirect("~/PROJECT/DESIGN_MGMT/DesignListDesignView.aspx?drawingno=" + Convert.ToString(Request.QueryString["drawingno"]));
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/PROJECT/DESIGN_MGMT/DesignListProjectView.aspx?drawingno=" + Convert.ToString(Request.QueryString["drawingno"]));
                    //}
                }
                else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["PAN"])) && Convert.ToInt32(Session["EMP_RECORD_ID"]) > 0)
                {
                    Response.Redirect("~/VENDOR_CUSTOMER_MGMT/VENDOR/VendorsList.aspx?PAN=" + Convert.ToString(Request.QueryString["PAN"]) + "&name=" + Convert.ToString(Request.QueryString["name"]));
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                pnlMsg.Visible = true;
                lblMsg.Text = "Username or password is invalid!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SendPassword()
    {
        try
        {
            DataSet dsEmpPassDetails = new DataSet();

            string employeeID = string.Empty;
            string employeeName = string.Empty;
            string userName = string.Empty;
            string emailID = string.Empty;
            string password = string.Empty;


            string userNametxt = string.Empty;
            string employeeIDtxt = string.Empty;


            if (!string.IsNullOrEmpty(txtUserNameNew.Text))
                userNametxt = txtUserNameNew.Text;
            else
                userNametxt = string.Empty;


            if (!string.IsNullOrEmpty(txtEmployeeID.Text))
                employeeIDtxt = txtEmployeeID.Text;
            else
                employeeIDtxt = string.Empty;


            dsEmpPassDetails = objCommon.GetEmployeePassDetail(userNametxt, employeeIDtxt);
            if (dsEmpPassDetails.Tables.Count > 0 && dsEmpPassDetails.Tables[0].Rows.Count > 0)
            {
                if (dsEmpPassDetails.Tables[0].Rows[0]["EMPLOYEE_ID"] != DBNull.Value)
                    employeeID = Convert.ToString(dsEmpPassDetails.Tables[0].Rows[0]["EMPLOYEE_ID"]);

                if (dsEmpPassDetails.Tables[0].Rows[0]["EMPLOYEE_NAME"] != DBNull.Value)
                    employeeName = Convert.ToString(dsEmpPassDetails.Tables[0].Rows[0]["EMPLOYEE_NAME"]);

                if (dsEmpPassDetails.Tables[0].Rows[0]["USER_NAME"] != DBNull.Value)
                    userName = Convert.ToString(dsEmpPassDetails.Tables[0].Rows[0]["USER_NAME"]);

                if (dsEmpPassDetails.Tables[0].Rows[0]["PASSWORD"] != DBNull.Value)
                    password = Convert.ToString(dsEmpPassDetails.Tables[0].Rows[0]["PASSWORD"]);

                if (dsEmpPassDetails.Tables[0].Rows[0]["EMAIL_ID"] != DBNull.Value)
                    emailID = Convert.ToString(dsEmpPassDetails.Tables[0].Rows[0]["EMAIL_ID"]);

                if (!string.IsNullOrEmpty(emailID))
                {
                    int mailValue = SendMail(employeeID, employeeName, userName, password, emailID);
                    if (mailValue > 0)
                    {
                        SuccessMessage("Password sent to registered email-id");
                        return;
                    }
                    else
                    {
                        ExceptionMessage("Please try again!");
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("Email ID is not registered, please contact to IT team.");
                    return;
                }
            }
            else
            {
                ExceptionMessage("Please enter correct Employee ID");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int SendMail(string employeeID, string employeeName, string userName, string password, string emailID)
    {
        try
        {
            string from = string.Empty;
            string to = string.Empty;

            from = "IThelpdesk@coperion.com";
            to = emailID;


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(to))
            {
                string[] strTo = to.Split(';');
                foreach (string item in strTo)
                {
                    mail.To.Add(item);
                }
            }

            mail.Subject = "TMS Login Credentials";
            mail.IsBodyHtml = true;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/ADMIN/ForgetPassword.htm")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#employeeid#}", employeeID);
            body = body.Replace("{#emploeename#}", employeeName);
            body = body.Replace("{#username#}", userName);
            body = body.Replace("{#password#}", password);

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
        lblMsg.ForeColor = System.Drawing.Color.White;
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
