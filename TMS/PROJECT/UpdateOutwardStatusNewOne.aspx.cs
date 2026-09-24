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
using System.Text;
using System.Net.Mime;


public partial class PROJECT_UpdateOutwardStatusNewOne : System.Web.UI.Page
{
    #region VARIABLES[=============]

    DataSet dsUserInfo = new DataSet();
    //DataSet dsTourInfo = new DataSet();

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Common objCommon = new BAL.Common();

    BAL.InwardOutward objectInward = new BAL.InwardOutward();


    int advanceAmtcurrencyID = 0;
    string advanceAmtcurrencyCode = string.Empty;
    int generateTSNumber = 0;

    string tourNo = string.Empty;
    string sanctionNo = string.Empty;
    string tourStatus = string.Empty;
    int tourStatusID = 0;
    /// //
    int tourBasedOnID = 0;
    int travelModeID = 0;

    string createdBy = string.Empty;
    string createdByID = string.Empty;
    string createdByEmail = string.Empty;
    string createdOn = string.Empty;
    string createdRemarks = string.Empty;

    string hodApprovedBy = string.Empty;
    string hodApprovedByEmail = string.Empty;
    string hodApprovedOn = string.Empty;
    string hodApprovedRemarks = string.Empty;

    string finalApprovedBy = string.Empty;
    string finalApprovedByEmail = string.Empty;
    string finalApprovedOn = string.Empty;
    string finalApprovedRemarks = string.Empty;

    string deletedBy = string.Empty;
    string deletedByEmail = string.Empty;
    string deletedOn = string.Empty;
    string deletedRemarks = string.Empty;

    string cancelledBy = string.Empty;
    string cancelledByEmail = string.Empty;
    string cancelledOn = string.Empty;
    string cancelledRemarks = string.Empty;


    int finalApprovarEmpRecordID = 0;
    string finalApprovarUN = string.Empty;
    string finalApprovarPWD = string.Empty;
    string finalApprovarEmail = string.Empty;
    string finalApprovarName = string.Empty;


    string hrTeamEmail = string.Empty;

    string accTeamEmail = string.Empty;
    string accTeamName = string.Empty;

    double advanceAmt = 0;
    string advanceAmtCurrency = string.Empty;
    string accAcvnaceFCEmail = string.Empty;

    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;

    string subject = string.Empty;
    string fileName = string.Empty;
    bool isMailSend = false;

    int approvedByID = 0;
    int deletedByID = 0;
    int cancelledByID = 0;

    //int createdByID = 0;

    #endregion


    #region EVENTS[================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["CHK_ACC"] = null;
            ValidateAndLoginAndApproveDelete();
        }
    }

    protected void btnInwardOutwardList_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/TOUR_AND_TRAVELS/TOUR/TourInformationListNew.aspx?tourno=" + Convert.ToString(Request.QueryString["tourno"]));
        Response.Redirect("~/PROJECT/OutwardProductionList.aspx");
    }

    #endregion


    #region METHODS[===============]

    private void ValidateAndLoginAndApproveDelete()

    {
        try
        {
            DataSet ds = new DataSet();
            string userName = string.Empty;
            string password = string.Empty;
            int outwardID = Convert.ToInt32(Request.QueryString["outwardid"]);
            userName = Convert.ToString(Request.QueryString["ud"]);
            password = Convert.ToString(Request.QueryString["pd"]);
            ds = objCommon.ValidateAndLogin(userName, password);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ModalPopupExtender1.Show();

                Session["EMP_RECORD_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["EMP_RECORD_ID"]);
                Session["EMPLOYEE_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                Session["USER_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_NAME"]);
                Session["PASSWORD"] = Convert.ToString(ds.Tables[0].Rows[0]["PASSWORD"]);
                Session["EMAIL_ID"] = Convert.ToString(ds.Tables[0].Rows[0]["EMAIL_ID"]);
                Session["USER_TYPE"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_TYPE"]);
                Session["IS_TEAMLEADER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["IS_TEAMLEADER"]);
                Session["TEAMLEADER_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TEAMLEADER_ID"]);
                Session["DEPARTMENT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]);
                Session["TIMESHEET_DEPT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TIMESHEET_DEPT_ID"]);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    Session["TEAMMEMBERS"] = ds.Tables[1];
                }

                if (ds.Tables.Count > 0)
                {
                    Session["USERINFO"] = ds;
                }

                DataSet dsFinalConfNo = objectInward.GetFinalConfNoStatusIDByReqID(outwardID,0);

                int statusID = Convert.ToInt32(dsFinalConfNo.Tables[0].Rows[0]["STATUS_ID"]);
                int createdByID = Convert.ToInt32(dsFinalConfNo.Tables[0].Rows[0]["CREATED_BY"]);
                int hodApprovedByID = Convert.ToInt32(dsFinalConfNo.Tables[0].Rows[0]["APPROVED_BY"]);
                int logisticsConfirmedById = Convert.ToInt32(dsFinalConfNo.Tables[0].Rows[0]["FINAL_APPROVED_BY"]);

                int teamLeaderID = Convert.ToInt32(dsFinalConfNo.Tables[0].Rows[0]["TEAMLEADER_ID"]);

                if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.New)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(dsFinalConfNo.Tables[0].Rows[0]["FINAL_CONFIRMATION_NUMBER"]))
                       && Convert.ToInt32(Session["EMP_RECORD_ID"]) == teamLeaderID)
                    {
                        int actID = Convert.ToInt32(Request.QueryString["actid"]);
                        UpdateInwardOutwardStatus(actID);
                    }
                    else
                    {
                        SuccessMessage("This inward request is already final approved by MP.");
                    }

                }

                else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved)
                {
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 10 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 7
                        || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 99)
                    {
                        int actID = Convert.ToInt32(Request.QueryString["actid"]);
                        UpdateInwardOutwardStatus(actID);
                    }
                    else
                    {
                        SuccessMessage("This inward request is already approved.");
                    }
                }

                else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                {
                    SuccessMessage("This inward request is already approved.");
                }

                else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                {
                    SuccessMessage("This inward request is already deleted.");
                }

                else if (statusID == (int)EnumInwardOutward.EnumINOUTStatus.Cancelled)
                {
                    SuccessMessage("This inward request is already cancelled.");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateInwardOutwardStatus(int actID)
    {
        try
        {
            int reqID = Convert.ToInt32(Request.QueryString["outwardid"]);
            string outwardno = Convert.ToString(Request.QueryString["outwardno"]);
            //string tourSanctionNumber = Convert.ToString(Request.QueryString["toursanctionnumber"]);

            int empRecordID = Convert.ToInt32(Request.QueryString["emprecordid"]);
            int inwardstatusid = Convert.ToInt32(Request.QueryString["outwardstatusid"]);
            int currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int teamLeaderID = Convert.ToInt32(Request.QueryString["tlemprecordid"]); //Convert.ToInt32(Session["TEAMLEADER_ID"]);
            string remarks = string.Empty;

            string approvedMsg = string.Empty;

            int insertVal = 0;
            int sentMailVal = 0;
            int retStatusID = 0;
            string inwardConfirmationNumber = "";

            //TourAndTravelStatusUpdate objUT = new TourAndTravelStatusUpdate();
            InwardOutwardStatusUpdate objIO = new InwardOutwardStatusUpdate();
            byte[] podAttachment1 = new byte[2]; // This will contain {0x00, 0x00}

            byte[] txtPODAttachment2byte = new byte[2]; // This will contain {0x00, 0x00}



            string returnVal = objIO.UpdateInwardOutwardStatus(actID,
                                                      reqID,
                                                      //tourNO, 
                                                      empRecordID,
                                                      inwardstatusid,
                                                      remarks,
                                                      teamLeaderID,
                                                      currentUserID,
                                                      " ",
                                                      " ",
                                                      " ",
                                                      podAttachment1,
                                                      " ",
                                                      " ",
                                                      " ",
                                                      txtPODAttachment2byte,
                                                      " ",
                                                      " ",
                                                      " ",
                                                      " ",
                                                      " "
                                                      );

            if (returnVal.Length > 50)
            {
                ExceptionMessage(returnVal);
                return;
            }
            else
            {
                insertVal = Convert.ToInt32(returnVal.Split(':')[0]);
                sentMailVal = Convert.ToInt32(returnVal.Split(':')[1]);
                retStatusID = Convert.ToInt32(returnVal.Split(':')[2]);
                inwardConfirmationNumber = Convert.ToString(returnVal.Split(':')[3]);
            }

            if (!string.IsNullOrEmpty(inwardConfirmationNumber))
            {
                approvedMsg = "Outward No.'" + outwardno + "' approved and Tour Sanction No. : '" + inwardConfirmationNumber + "' generated ";
            }
            else
            {
                approvedMsg = "Outward No.: '" + outwardno + "' approved ";
            }
            if (insertVal > 0)
            {
                if (sentMailVal > 0)
                {
                    if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved)
                    {
                        SuccessMessage(approvedMsg + "and mail sent successfully!");
                    }
                    else if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                    {
                        SuccessMessage("Inward Outward No.: '" + outwardno + "' confirmed and mail sent successfully!");

                    }
                    else if (retStatusID == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                    {

                        if (currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Tour Information No.: '" + outwardno + "' rejected and mail sent successfully!");
                        }
                        else
                        {
                            SuccessMessage("Tour Information No.: '" + outwardno + "' rejected and mail sent successfully!");
                        }
                        //SuccessMessage("Tour Information No.: '" + tourNO + "' deleted and mail sent successfully!");
                    }
                }
                else
                {
                    //if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
                    //    retStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved ||
                    //    retStatusID == (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer)
                    //{
                    //    SuccessMessage(approvedMsg + "successfully, please resend e-mail from Tour Information List!");
                    //}
                    //else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    //{
                    //    SuccessMessage("Tour Information No.: '" + tourNO + "' deleted successfully, please resend e-mail from Tour Information List.");
                    //}
                }
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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