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

public partial class TOUR_AND_TRAVELS_TOUR_UpdateTourStatusNewOne : System.Web.UI.Page
{

    #region VARIABLES[=============]

    DataSet dsUserInfo = new DataSet();
    //DataSet dsTourInfo = new DataSet();

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Common objCommon = new BAL.Common();

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

    int createdByID = 0;

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

    protected void btnTourList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TOUR/TourInformationListNew.aspx?tourno=" + Convert.ToString(Request.QueryString["tourno"]));
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
            int tourID = Convert.ToInt32(Request.QueryString["tourid"]);
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

                DataSet dsTourSanctionno = objTourAndTravels.GetTourSanctionNoStatusIDByTourID(tourID);

                int statusID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["STATUS_ID"]);
                int travelModeID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["TRAVEL_MODE_ID"]);
                //////////////////////////////////////////////////////////////////////
                int tourBasedOnID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["CUSTOMER_BASED_ON_ID"]);

                createdByID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["EMP_RECORD_ID"]);
                int hodApprovedByID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["APPROVED_BY"]);
                int mgmtApprovedByID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["MGMT_APPROVED_BY"]);
                int finalApprovedByID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["FINAL_APPROVED_BY"]);




                int teamLeaderID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["TEAMLEADER_ID"]);
                int mgmtApproverID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["MGMT_HOD_RECORD_ID"]);
                int finalApproverID = Convert.ToInt32(dsTourSanctionno.Tables[0].Rows[0]["FINAL_HOD_RECORD_ID"]);

                if (statusID == (int)TandTAllStatus.EnumTourStatus.New)
                {

                    if (travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness || tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(dsTourSanctionno.Tables[0].Rows[0]["TOUR_SANCTION_NO"]))
                           && Convert.ToInt32(Session["EMP_RECORD_ID"]) == teamLeaderID)
                        {
                            int actID = Convert.ToInt32(Request.QueryString["actid"]);
                            UpdateTourStatus(actID);
                        }
                        else
                        {
                            SuccessMessage("This tour is already final approved by MP.");
                        }

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(dsTourSanctionno.Tables[0].Rows[0]["TOUR_SANCTION_NO"]))
                           && Convert.ToInt32(Session["EMP_RECORD_ID"]) == teamLeaderID)
                        {
                            int actID = Convert.ToInt32(Request.QueryString["actid"]);
                            UpdateTourStatus(actID);
                        }
                        else
                        {
                            SuccessMessage("This tour is already approved by HOD.");
                        }
                    }

                    
                }

                //else if (statusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                //{
                //    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == mgmtApproverID)
                //    {
                //        int actID = Convert.ToInt32(Request.QueryString["actid"]);
                //        UpdateTourStatus(actID);
                //    }
                //    else
                //    {
                //        SuccessMessage("This tour is already approved by HOD.");
                //    }
                //}

                //else if (statusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved)
                //{
                //    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == finalApproverID)
                //    {
                //        if (travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness || tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer)
                //        {
                //            int actID = Convert.ToInt32(Request.QueryString["actid"]);
                //            UpdateTourStatus(actID);
                //        }
                //        else
                //        {
                //            SuccessMessage("This tour is already approved.");
                //        }
                //    }
                //    else
                //    {
                //        SuccessMessage("This tour is already approved.");
                //    }
                //}

                else if (statusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                {
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == finalApproverID)
                    {
                        if (travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness || tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer)
                        {
                            int actID = Convert.ToInt32(Request.QueryString["actid"]);
                            UpdateTourStatus(actID);
                        }
                        else
                        {
                            SuccessMessage("This tour is already approved.");
                        }
                    }
                    else
                    {
                        SuccessMessage("This tour is already approved.");
                    }
                }

                else if (statusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
                {
                    SuccessMessage("This tour is already approved.");
                }

                else if (statusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                {
                    SuccessMessage("This tour is already deleted.");
                }

                else if (statusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                {
                    SuccessMessage("This tour is already cancelled.");
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

    //private void UpdateTourStatus(int actID)
    //{
    //    try
    //    {
    //        int tourID = Convert.ToInt32(Request.QueryString["tourid"]);
    //        string tourNO = Convert.ToString(Request.QueryString["tourno"]);
    //        int empRecordID = Convert.ToInt32(Request.QueryString["emprecordid"]);
    //        int tourStatusID = Convert.ToInt32(Request.QueryString["tourstatusid"]);
    //        string remarks = string.Empty;

    //        if (actID == (int)TandTAllStatus.EnumTourAct.HODApprove) 
    //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.HODApproved;
    //        else if (actID == (int)TandTAllStatus.EnumTourAct.FinalApprove) 
    //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.FinalApproved;
    //        else if (actID == (int)TandTAllStatus.EnumTourAct.Delete) 
    //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.Deleted;

    //        //else if (actID == 3) tourStatusID = 3;


    //        TourAndTravelStatusUpdate objUT = new TourAndTravelStatusUpdate();
    //        objUT.UpdateTourStatus(actID)



    //        int value = objTourAndTravels.UpdateTourStatus(tourID, tourStatusID, actID, remarks, empRecordID);
    //        if (value > 0)
    //        {
    //            DataSet dsTourSanctionno = objTourAndTravels.GetTourSanctionNoStatusIDByTourID(tourID);
    //            int travelModeId = 0;
    //            string tourSanctionNumber = "";

    //            DataRow dr0 = dsTourSanctionno.Tables[0].Rows[0];

    //            travelModeId = Convert.ToInt32(dr0["TRAVEL_MODE_ID"]);
    //            if (!string.IsNullOrEmpty(Convert.ToString(dr0["TOUR_SANCTION_NO"])) && dr0["TOUR_SANCTION_NO"] != DBNull.Value)
    //                tourSanctionNumber = Convert.ToString(dr0["TOUR_SANCTION_NO"]);

    //            TourSendMail tsm = new TourSendMail();
    //            int sendMailValue = tsm.SendMail(tourID);

    //            string approvedMsg = string.Empty;
    //            if (!string.IsNullOrEmpty(tourSanctionNumber))
    //            {
    //                approvedMsg = "Tour Information No.: '" + tourNO + "' approved and Tour Sanction No. : '" + tourSanctionNumber + "' generated ";
    //            }
    //            else
    //            {
    //                approvedMsg = "Tour Information No.: '" + tourNO + "' approved ";
    //            }

    //            if (sendMailValue > 0)
    //            {
    //                if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
    //                    tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //                {
    //                    SuccessMessage(approvedMsg + "and mail sent successfully!");
    //                }
    //                else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
    //                {
    //                    SuccessMessage("Tour Information No.: '" + tourNO + "' deleted and mail sent successfully!");
    //                }
    //            }
    //            else
    //            {
    //                if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
    //                    tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //                {
    //                    SuccessMessage(approvedMsg + "successfully, please resend e-mail from Tour Information List!");
    //                }
    //                else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
    //                {
    //                    SuccessMessage("Tour Information No.: '" + tourNO + "' deleted successfully, please resend e-mail from Tour Information List.");
    //                }
    //            }


    //            //if (sendMailValue > 0)
    //            //{
    //            //    int val = objTourAndTravels.UpdateTourInfoMailStatus(actID, tourID, Convert.ToInt32(ViewState["CHK_ACC"]));

    //            //    if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
    //            //    {
    //            //        if (travelModeId != (int)TandTAllStatus.EnumTravelMode.AirBusiness)
    //            //            SuccessMessage("Tour Information No.: '" + tourNO + "' approved and Tour Sanction No. : '" + tourSanctionNumber + "' generated and mail sent successfully.");
    //            //        else
    //            //            SuccessMessage("Tour Information No.: '" + tourNO + "' approved and mail sent successfully .");
    //            //    }
    //            //    else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //            //    {
    //            //        if (travelModeId == (int)TandTAllStatus.EnumTravelMode.AirBusiness)
    //            //            SuccessMessage("Tour Information No.: '" + tourNO + "' approved and Tour Sanction No. : '" + tourSanctionNumber + "' generated and mail sent successfully.");
    //            //        else
    //            //            SuccessMessage("Tour Information No.: '" + tourNO + "' approved and mail sent successfully.");
    //            //    }
    //            //    else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
    //            //    {
    //            //        SuccessMessage("Tour Information No.: '" + tourNO + "' deleted and mail sent successfully.");
    //            //    }
    //            //}
    //            //else
    //            //{
    //            //    if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
    //            //    {
    //            //        if (travelModeId != (int)TandTAllStatus.EnumTravelMode.AirBusiness)
    //            //            SuccessMessage("Tour Information No.: '" + tourNO + "' approved and Tour Sanction No. : '" + tourSanctionNumber + "' generated successfully, please resend e-mail from Tour Information List.");
    //            //        else
    //            //            SuccessMessage("Tour Information No.: '" + tourNO + "' approved successfully, please resend e-mail from Tour Information List.");
    //            //    }
    //            //    else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //            //    {
    //            //        if (travelModeId == (int)TandTAllStatus.EnumTravelMode.AirBusiness)
    //            //            SuccessMessage("Tour Information No.: '" + tourNO + "' approved and Tour Sanction No. : '" + tourSanctionNumber + "' generated successfully, please resend e-mail from Tour Information List.");
    //            //        else
    //            //            SuccessMessage("Tour Information No.: '" + tourNO + "' approved successfully, please resend e-mail from Tour Information List.");
    //            //    }
    //            //    else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
    //            //    {
    //            //        SuccessMessage("Tour Information No.: '" + tourNO + "' deleted successfully, please resend e-mail from Tour Information List.");
    //            //    }
    //            //}
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //    }
    //}

    private void UpdateTourStatus(int actID)
    {
        try
        {
            int tourID = Convert.ToInt32(Request.QueryString["tourid"]);
            string tourNO = Convert.ToString(Request.QueryString["tourno"]);
            //string tourSanctionNumber = Convert.ToString(Request.QueryString["toursanctionnumber"]);
            //int empRecordID = Convert.ToInt32(Request.QueryString["emprecordid"]);
            int tourStatusID = Convert.ToInt32(Request.QueryString["tourstatusid"]);
            int travelModeID = Convert.ToInt32(Request.QueryString["travelmodeid"]);
            int tourBasedOnID = Convert.ToInt32(Request.QueryString["tourbasedonid"]);
            int currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int teamLeaderID = Convert.ToInt32(Request.QueryString["tlemprecordid"]); //Convert.ToInt32(Session["TEAMLEADER_ID"]);
            string remarks = string.Empty;

            string approvedMsg = string.Empty;

            int insertVal = 0;
            int sentMailVal = 0;
            int retStatusID = 0;
            string tourSanctionNumber = "";

            TourAndTravelStatusUpdate objUT = new TourAndTravelStatusUpdate();
            string returnVal = objUT.UpdateTourStatus(actID,
                                                      tourID,
                                                      //tourNO, 
                                                      createdByID,
                                                      tourStatusID,
                                                      travelModeID,
                                                      tourBasedOnID,
                                                      remarks,
                                                      teamLeaderID,
                                                      currentUserID,
                                                      0);

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
                tourSanctionNumber = Convert.ToString(returnVal.Split(':')[3]);
            }

            if (!string.IsNullOrEmpty(tourSanctionNumber))
            {
                approvedMsg = "Tour Information No.: '" + tourNO + "' approved and Tour Sanction No. : '" + tourSanctionNumber + "' generated ";
            }
            else
            {
                approvedMsg = "Tour Information No.: '" + tourNO + "' approved ";
            }
            if (insertVal > 0)
            {
                if (sentMailVal > 0)
                {
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved ||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer)
                    {
                        SuccessMessage(approvedMsg + "and mail sent successfully!");
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    {

                        if (currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Tour Information No.: '" + tourNO + "' rejected and mail sent successfully!");
                        }
                        else
                        {
                            SuccessMessage("Tour Information No.: '" + tourNO + "' rejected and mail sent successfully!");
                        }
                            //SuccessMessage("Tour Information No.: '" + tourNO + "' deleted and mail sent successfully!");
                    }
                }
                else
                {
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved ||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer)
                    {
                        SuccessMessage(approvedMsg + "successfully, please resend e-mail from Tour Information List!");
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    {
                        SuccessMessage("Tour Information No.: '" + tourNO + "' deleted successfully, please resend e-mail from Tour Information List.");
                    }
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

    //private string GenerateAdviceCSV(string beneficiaryName, string tourSanctionNo, string amount, string emailAdd1, string beneBankAccount,
    //                                    string beneBankIFSCBANKCode, int unitID)
    //{
    //    string csvTxt = string.Empty;

    //    string transactionType = string.Empty;
    //    string referenceNumber = string.Empty;
    //    string drAccountNo = string.Empty;
    //    string paymentNarration = string.Empty;
    //    //string beneficiaryName = string.Empty;
    //    string beneAdd1 = string.Empty;
    //    string beneAdd2 = string.Empty;
    //    string beneAdd3 = string.Empty;
    //    string paymentLocation = string.Empty;
    //    string chequeNo = string.Empty;
    //    string valueDate = string.Empty;
    //    //string amount = string.Empty;
    //    string printBranchLocation = string.Empty;
    //    //string emailAdd1 = string.Empty;
    //    string emailAdd2 = string.Empty;
    //    string emailAdd3 = string.Empty;
    //    string freeText = string.Empty;
    //    string nA1 = string.Empty;
    //    string nA2 = string.Empty;
    //    string nA3 = string.Empty;
    //    string nA4 = string.Empty;
    //    string nA5 = string.Empty;
    //    //string beneBankAccount = string.Empty;
    //    //string beneBankIFSCBANKCode = string.Empty;
    //    string nA6 = string.Empty;
    //    string deliverTo = string.Empty;
    //    string orderingPartyName = string.Empty;
    //    string orderingPartyAdd1 = string.Empty;
    //    string orderingPartyAdd2 = string.Empty;
    //    string orderingPartyAdd3 = string.Empty;
    //    string orderingPartyAccount = string.Empty;
    //    string bankName = string.Empty;
    //    string bankToBankInfo = string.Empty;

    //    try
    //    {
    //        string csv = string.Empty;

    //        if (Convert.ToDouble(amount) <= 200000)
    //            transactionType = "NEFT";
    //        else
    //            transactionType = "RTGS";

    //        referenceNumber = "HSBC";

    //        //A35
    //        if (unitID == 1)
    //            drAccountNo = "'499391803001";
    //        //Delhi
    //        else if (unitID == 2)
    //            drAccountNo = "'499391803005";
    //        //SEZ
    //        else if (unitID == 3)
    //            drAccountNo = "'499391803004";
    //        //GNU       
    //        else if (unitID == 4)
    //            drAccountNo = "'499391803003";

    //        paymentNarration = string.Empty;

    //        beneAdd1 = string.Empty;
    //        beneAdd2 = string.Empty;
    //        beneAdd3 = string.Empty;
    //        paymentLocation = string.Empty;
    //        chequeNo = string.Empty;
    //        valueDate = DateTime.Now.ToString("dd/MM/yyyy");
    //        printBranchLocation = string.Empty;
    //        emailAdd2 = string.Empty;
    //        emailAdd3 = string.Empty;
    //        freeText = tourSanctionNo;
    //        nA1 = string.Empty;
    //        nA2 = string.Empty;
    //        nA3 = string.Empty;
    //        nA4 = string.Empty;
    //        nA5 = string.Empty;
    //        nA6 = string.Empty;
    //        deliverTo = string.Empty;
    //        orderingPartyName = "COPERION IDEAL";
    //        orderingPartyAdd1 = "PRIVATE LIMITED";
    //        orderingPartyAdd2 = "A35 SECTOR 64";
    //        orderingPartyAdd3 = "NOIDA";
    //        orderingPartyAccount = "201307";
    //        bankName = string.Empty;
    //        bankToBankInfo = string.Empty;


    //        csv += "COPERION IDEAL PRIVATE LIMITED,";
    //        csv += "\r\n";
    //        //"Transaction_Type,Reference_Number,Dr_Account_No,Payment_Narration,Beneficiary_Name,Bene_Add_1,Bene_Add_2,Bene_Add_3,Payment_Location,Cheque_No,Value_Date,Amount,Print_Branch_Location,Email_Add_1,Email_Add_2,Email_Add_3,Free_Text,NA,NA,NA,NA,NA,Bene_Bank_Account_#,Bene_Bank_IFSC/BANK_Code,NA,Deliver_To,Ordering_Party_Name,Ordering_Party_Add1,Ordering_Party_Add2,Ordering_Party_Add3,Ordering_party_Account,Bank_Name,Bank_to_Bank_Info,";
    //        csv += "Transaction Type,Reference Number,Dr Account No,Payment Narration,Beneficiary Name,Bene Add 1,Bene Add 2,Bene Add 3,Payment Location,Cheque No,Value Date,Amount,Print Branch Location,Email Add-1,Email Add-2,Email Add-3,FreeText,NA,NA,NA,NA,NA,Bene Bank Account #,Bene Bank IFSC /  BANK Code,NA,Deliver To,Ordering Party Name,Ordering_Party Add1,Ordering_Party Add2,Ordering_Party Add3,Ordering_party_Account,BANK_NAME,Bank_to_Bank_Info,";
    //        csv += "\r\n";

    //        csv += transactionType + "," + referenceNumber + "," + drAccountNo + "," +
    //                        paymentNarration + "," + beneficiaryName + "," + beneAdd1 + "," + beneAdd2 + "," + beneAdd3 + "," + paymentLocation + "," + chequeNo + "," + valueDate + "," + amount + "," +
    //                        printBranchLocation + "," + emailAdd1 + "," + emailAdd2 + "," + emailAdd3 + "," + freeText + "," + nA1 + "," + nA2 + "," + nA3 + "," + nA4 + "," + nA5 + "," +
    //                        beneBankAccount + "," + beneBankIFSCBANKCode + "," + nA6 + "," + deliverTo + "," + orderingPartyName + "," +
    //                        orderingPartyAdd1 + "," + orderingPartyAdd2 + "," + orderingPartyAdd3 + "," + orderingPartyAccount + "," + bankName + "," + bankToBankInfo + ",";



    //        if (!string.IsNullOrEmpty(csv))
    //        {
    //            csvTxt = csv;
    //        }
    //        else
    //        {
    //            csvTxt = string.Empty;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        csvTxt = string.Empty;
    //    }
    //    return csvTxt;
    //}

    //private int SendMailNew(int actID, int tourID)
    //{
    //    int sendVal = 0;
    //    try
    //    {
    //        string csvTxt = string.Empty;
    //        string fromDate = string.Empty;
    //        string toDate = string.Empty;
    //        string customeName = string.Empty;
    //        string placeOfvisit = string.Empty;
    //        string jobNo = string.Empty;
    //        string travelMode = string.Empty;

    //        string purposeOfVisit = string.Empty;
    //        string typeOfTrip = string.Empty;
    //        string modeOfTravel = string.Empty;
    //        string expectedExpenditure = string.Empty;
    //        string expectedExpenditureCurr = string.Empty;
    //        string advanceRequired = string.Empty;
    //        string advanceRequiredCurr = string.Empty;



    //        int unitID = 0;
    //        int isAdviceGenerated = 0;

    //        string createdByAccNo = string.Empty;
    //        string createdByIFSC = string.Empty;

    //        dsUserInfo = objTourAndTravels.GetRequesterInfo(tourID);
    //        if (dsUserInfo.Tables.Count > 0)
    //        {
    //            if (dsUserInfo.Tables[0].Rows.Count > 0)
    //            {
    //                DataRow dr0 = dsUserInfo.Tables[0].Rows[0];


    //                tourNo = Convert.ToString(dr0["TOUR_NO"]);
    //                sanctionNo = Convert.ToString(dr0["TOUR_SANCTION_NO"]);
    //                tourStatus = Convert.ToString(dr0["TOUR_STATUS_NAME"]);
    //                tourStatusID = Convert.ToInt32(dr0["TOUR_STATUS_ID"]);
    //                travelModeID = Convert.ToInt32(dr0["TRAVEL_MODE_ID"]);

    //                fromDate = Convert.ToDateTime(dr0["START_DATE"]).ToString("dd-MMM-yyyy");
    //                toDate = Convert.ToDateTime(dr0["END_DATE"]).ToString("dd-MMM-yyyy");
    //                customeName = Convert.ToString(dr0["CUST_VEND_NAME"]);
    //                placeOfvisit = Convert.ToString(dr0["PLACE_OF_VISIT"]);
    //                tourBasedOnID = Convert.ToInt32(dr0["CUSTOMER_BASED_ON_ID"]);

    //                travelMode = Convert.ToString(dr0["TRAVEL_MODE"]);
    //                jobNo = Convert.ToString(dr0["JOB_NO"]);
    //                //////
    //                purposeOfVisit = Convert.ToString(dr0["VISIT_TYPE"]);
    //                //////
    //                typeOfTrip = Convert.ToString(dr0["TRIP_TYPE"]);
    //                modeOfTravel = Convert.ToString(dr0["TRAVEL_MODE"]);
    //                expectedExpenditure = Convert.ToString(dr0["EXPENDITURE_AMT"]);
    //                expectedExpenditureCurr = Convert.ToString(dr0["EXPENDITURE_CURRENCY"]);
    //                advanceRequired = Convert.ToString(dr0["ADVANCE_AMT"]);
    //                advanceRequiredCurr = Convert.ToString(dr0["CURRENCY"]);



    //                createdBy = Convert.ToString(dr0["CREATED_BY"]);
    //                createdByEmail = Convert.ToString(dr0["CREATED_BY_EMAIL"]);
    //                if (dr0["CREATED_ON"] != DBNull.Value)
    //                    createdOn = Convert.ToDateTime(dr0["CREATED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                createdRemarks = Convert.ToString(dr0["CREATED_REMARKS"]);

    //                if (dr0["CREATED_BY_ACC"] != DBNull.Value)
    //                {
    //                    createdByAccNo = Convert.ToString(dr0["CREATED_BY_ACC"]);
    //                    ViewState["CHK_ACC"] = 1;
    //                }
    //                else
    //                    ViewState["CHK_ACC"] = 0;

    //                if (dr0["CREATED_BY_IFSC"] != DBNull.Value)
    //                {
    //                    createdByIFSC = Convert.ToString(dr0["CREATED_BY_IFSC"]);
    //                    ViewState["CHK_ACC"] = 1;
    //                }
    //                else
    //                    ViewState["CHK_ACC"] = 0;

    //                hodApprovedBy = Convert.ToString(dr0["APPROVED_BY"]);
    //                if (dr0["APPROVED_ON"] != DBNull.Value)
    //                    hodApprovedOn = Convert.ToDateTime(dr0["APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                hodApprovedByEmail = Convert.ToString(dr0["APPROVED_BY_EMAIL"]);
    //                hodApprovedRemarks = Convert.ToString(dr0["APPROVED_REMARKS"]);


    //                finalApprovedBy = Convert.ToString(dr0["FINAL_APPROVED_BY"]);
    //                if (dr0["FINAL_APPROVED_ON"] != DBNull.Value)
    //                    finalApprovedOn = Convert.ToDateTime(dr0["FINAL_APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                finalApprovedByEmail = Convert.ToString(dr0["FINAL_APPROVED_BY_EMAIL"]);
    //                finalApprovedRemarks = Convert.ToString(dr0["FINAL_APPROVED_REMARKS"]);



    //                if (dr0["ADVANCE_AMT"] != DBNull.Value)
    //                {
    //                    advanceAmt = Convert.ToDouble(dr0["ADVANCE_AMT"]);
    //                    advanceAmtcurrencyCode = Convert.ToString(dr0["CURRENCY_CODE"]);
    //                    advanceAmtCurrency = Convert.ToString(dr0["CURRENCY"]);
    //                    advanceAmtcurrencyID = Convert.ToInt32(dr0["ADVANCE_CURRENCY"]);
    //                }


    //                deletedBy = Convert.ToString(dr0["DELETED_BY"]);
    //                if (dr0["DELETED_ON"] != DBNull.Value)
    //                    deletedOn = Convert.ToDateTime(dr0["DELETED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                deletedByEmail = Convert.ToString(dr0["DELETED_BY_EMAIL"]);
    //                deletedRemarks = Convert.ToString(dr0["DELETED_REMARKS"]);

    //                unitID = Convert.ToInt32(dr0["UNIT_ID"]);
    //                isAdviceGenerated = Convert.ToInt32(dr0["IS_ADVICE_GENERATED"]);
    //            }

    //            //HR Team
    //            if (dsUserInfo.Tables[1].Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in dsUserInfo.Tables[1].Rows)
    //                {
    //                    hrTeamEmail += ";" + Convert.ToString(dr["HR_EMAIL"]);
    //                }
    //                //hrTeamEmail = hrTeamEmail.TrimStart(';');
    //            }

    //            //Acc Team
    //            if (dsUserInfo.Tables[2].Rows.Count > 0)
    //            {
    //                accTeamName = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["ACC_NAME"]);
    //                accTeamEmail = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["ACC_EMAIL"]);
    //            }

    //            //Senthil and Pawan Sharma
    //            if (dsUserInfo.Tables[3].Rows.Count > 0)
    //            {
    //                //accAcvnaceFCEmail = Convert.ToString(dsUserInfo.Tables[3].Rows[0]["EMAIL_ID"]);
    //                foreach (DataRow dr in dsUserInfo.Tables[3].Rows)
    //                {
    //                    accAcvnaceFCEmail += ";" + Convert.ToString(dr["EMAIL_ID"]);
    //                }
    //                //accAcvnaceFCEmail = accAcvnaceFCEmail.TrimStart(';');
    //            }

    //            if (dsUserInfo.Tables[5].Rows.Count > 0)
    //            {
    //                //For final approval

    //                DataRow dr0 = dsUserInfo.Tables[5].Rows[0];

    //                finalApprovarEmpRecordID = Convert.ToInt32(dr0["EMP_RECORD_ID"]);
    //                finalApprovarUN = Convert.ToString(dr0["UN"]);
    //                finalApprovarPWD = Convert.ToString(dr0["PWD"]);
    //                finalApprovarName = Convert.ToString(dr0["EMPLOYEE_NAME"]);
    //                finalApprovarEmail = Convert.ToString(dr0["EMAIL_ID"]);
    //            }
    //        }
    //        else
    //        {
    //            sendVal = 0;
    //        }

    //        string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
    //        string approveHref = string.Empty;
    //        string approveLink = string.Empty;

    //        string deleteHref = string.Empty;
    //        string deleteLink = string.Empty;
    //        // ///////
    //        if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
    //        {
    //            if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
    //            {
    //                from = hodApprovedByEmail;
    //                to = finalApprovarEmail;
    //                if (!(Convert.ToInt32(advanceAmt) > 0))
    //                {
    //                    advanceRequiredCurr = "";
    //                }


    //                approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=6&tourstatusid=" + tourStatusID + "&ud=" + finalApprovarUN + "&pd=" + finalApprovarPWD + "&emprecordid=" + Convert.ToString(finalApprovarEmpRecordID) + "'";
    //                approveHref = "<a href=" + approveLink + ">Approve Tour request</a>";

    //                deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=3&tourstatusid=" + tourStatusID + "&ud=" + finalApprovarUN + "&pd=" + finalApprovarPWD + "&emprecordid=" + Convert.ToString(finalApprovarEmpRecordID) + "'";
    //                deleteHref = "<a href=" + deleteLink + ">Delete Tour request</a>";


    //                //mail will go sonja
    //                fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/01ReqToFinalHodTourMail.htm";
    //                subject = "Tour No.: '" + tourNo + "' Approved On: " + hodApprovedOn;
    //            }
    //            else
    //            {
    //                if (Convert.ToInt32(advanceAmt) > 0)
    //                {
    //                    from = hodApprovedByEmail;
    //                    bcc = hodApprovedByEmail;
    //                    //68 for INR in DB
    //                    if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
    //                    {
    //                        to = accTeamEmail;
    //                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourMail.htm";
    //                    }
    //                    else
    //                    {
    //                        to = accAcvnaceFCEmail;
    //                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourMailForeignTour.htm";
    //                    }
    //                    cc = hrTeamEmail + ";" + createdByEmail;
    //                    subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
    //                }
    //                else
    //                {
    //                    from = hodApprovedByEmail;
    //                    to = createdByEmail;
    //                    cc = hrTeamEmail;
    //                    bcc = hodApprovedByEmail;
    //                    advanceRequiredCurr = "";

    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";
    //                    subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
    //                }
    //            }
    //        }

    //        else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //        {
    //            if (Convert.ToInt32(advanceAmt) > 0)
    //            {
    //                from = finalApprovedByEmail;
    //                bcc = finalApprovedByEmail;

    //                //68 for INR in DB
    //                if (advanceAmtcurrencyID == 68)
    //                {
    //                    to = accTeamEmail;
    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourFinalMail.htm";
    //                }
    //                else
    //                {
    //                    to = accAcvnaceFCEmail;
    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourFinalMailForeignTour.htm";
    //                }
    //                cc = hodApprovedByEmail + ";" + hrTeamEmail + ";" + createdByEmail;
    //                subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
    //            }
    //            else
    //            {

    //                from = finalApprovedByEmail;
    //                to = createdByEmail;
    //                cc = hodApprovedByEmail + ";" + hrTeamEmail;
    //                bcc = finalApprovedByEmail;
    //                subject = "Tour Sanction No. " + sanctionNo + " generated on: " + finalApprovedOn;

    //                if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
    //                {
    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourFinalMail.htm";

    //                }
    //                else
    //                {

    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";


    //                }


    //            }

    //        }

    //        else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
    //        {
    //            from = deletedByEmail;
    //            to = createdByEmail;
    //            bcc = deletedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/05DeleteTourInfoMail.htm";
    //            subject = "Tour Information No. " + tourNo + " Deleted On: " + deletedOn;
    //        }

    //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //        SmtpClient SmtpServer = new SmtpClient();
    //        mail.From = new MailAddress(from);


    //        //2024-02-14
    //        if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
    //        {
    //            if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //            {
    //                AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
    //            }
    //        }
    //        else
    //        {
    //            if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
    //            {
    //                AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
    //            }
    //        }


    //        /////
    //        //if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
    //        //    tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //        //{
    //        //    if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved &&
    //        //        (travelModeID != (int)TandTAllStatus.EnumTravelMode.AirBusiness && (tourBasedOnID != (int)TandTAllStatus.EnumTourBasedOn.NonCustomer)))
    //        //    {
    //        //        AttachAdvideToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
    //        //    }
    //        //    else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved && 
    //        //        (travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer)))
    //        //    {
    //        //        AttachAdvideToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
    //        //    }
    //        //}

    //        #region MyRegion

    //        //if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved && travelModeID != (int)TandTAllStatus.EnumTravelMode.AirBusiness)
    //        //{
    //        //    //AttachAdvideToMail();

    //        //    if (Convert.ToInt32(advanceAmt) > 0 && advanceAmtcurrencyID == 68 &&
    //        //        isAdviceGenerated == 0 && !string.IsNullOrEmpty(Convert.ToString(createdByAccNo)) &&
    //        //        !string.IsNullOrEmpty(Convert.ToString(createdByIFSC))
    //        //       )
    //        //    {
    //        //        csvTxt = GenerateAdviceCSV(createdBy
    //        //                                 , sanctionNo
    //        //                                 , Convert.ToString(advanceAmt)
    //        //                                 , createdByEmail
    //        //                                 , createdByAccNo
    //        //                                 , createdByIFSC
    //        //                                 , unitID
    //        //                                 );

    //        //        MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(csvTxt));
    //        //        Attachment attachment = new Attachment(stream, new ContentType("text/csv"));


    //        //        string[] strTxt = sanctionNo.Split('/');
    //        //        string sanctionNoTxt = string.Empty;
    //        //        foreach (string item in strTxt)
    //        //        {
    //        //            sanctionNoTxt += item + "_";
    //        //        }
    //        //        if (!string.IsNullOrEmpty(sanctionNoTxt))
    //        //            attachment.Name = "Payment_Advice_" + sanctionNoTxt.TrimEnd('_') + ".CSV";
    //        //        else
    //        //            attachment.Name = "Payment_Advice.CSV";

    //        //        mail.Attachments.Add(attachment);
    //        //    }
    //        //}

    //        //else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved && travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness)
    //        //{
    //        //    //AttachAdvideToMail();

    //        //    if (Convert.ToInt32(advanceAmt) > 0 && advanceAmtcurrencyID == 68 &&
    //        //        isAdviceGenerated == 0 && !string.IsNullOrEmpty(Convert.ToString(createdByAccNo)) &&
    //        //        !string.IsNullOrEmpty(Convert.ToString(createdByIFSC))
    //        //       )
    //        //    {
    //        //        csvTxt = GenerateAdviceCSV(createdBy
    //        //                                 , sanctionNo
    //        //                                 , Convert.ToString(advanceAmt)
    //        //                                 , createdByEmail
    //        //                                 , createdByAccNo
    //        //                                 , createdByIFSC
    //        //                                 , unitID
    //        //                                 );

    //        //        MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(csvTxt));
    //        //        Attachment attachment = new Attachment(stream, new ContentType("text/csv"));


    //        //        string[] strTxt = sanctionNo.Split('/');
    //        //        string sanctionNoTxt = string.Empty;
    //        //        foreach (string item in strTxt)
    //        //        {
    //        //            sanctionNoTxt += item + "_";
    //        //        }
    //        //        if (!string.IsNullOrEmpty(sanctionNoTxt))
    //        //            attachment.Name = "Payment_Advice_" + sanctionNoTxt.TrimEnd('_') + ".CSV";
    //        //        else
    //        //            attachment.Name = "Payment_Advice.CSV";

    //        //        mail.Attachments.Add(attachment);
    //        //    }
    //        //}
    //        #endregion

    //        if (!string.IsNullOrEmpty(to))
    //        {
    //            string[] strTo = to.Split(';');
    //            foreach (string item in strTo)
    //            {
    //                if (!string.IsNullOrEmpty(item))
    //                    mail.To.Add(item);
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(cc))
    //        {
    //            string[] strCC = cc.Split(';');
    //            foreach (string item in strCC)
    //            {
    //                if (!string.IsNullOrEmpty(item))
    //                    mail.CC.Add(item);
    //            }
    //        }

    //        mail.Subject = subject;
    //        mail.IsBodyHtml = true;

    //        string body = string.Empty;
    //        using (StreamReader reader = new StreamReader(Server.MapPath(fileName)))
    //        {
    //            body = reader.ReadToEnd();
    //        }

    //        body = body.Replace("{#tourno#}", tourNo);
    //        body = body.Replace("{#sanctionno#}", sanctionNo);

    //        body = body.Replace("{#createdby#}", createdBy);
    //        body = body.Replace("{#createdon#}", createdOn);
    //        body = body.Replace("{#requesterremarks#}", createdRemarks);

    //        body = body.Replace("{#fromdate#}", fromDate);
    //        body = body.Replace("{#todate#}", toDate);
    //        body = body.Replace("{#customername#}", customeName);
    //        body = body.Replace("{#place#}", placeOfvisit);
    //        body = body.Replace("{#jobno#}", jobNo);

    //        body = body.Replace("{#purposeofvisit#}", purposeOfVisit);
    //        body = body.Replace("{#typeoftrip#}", typeOfTrip);
    //        body = body.Replace("{#modeOfTravel#}", modeOfTravel);
    //        body = body.Replace("{#expectedexpenditure#}", expectedExpenditure);
    //        body = body.Replace("{#expectedexpenditurecurr#}", expectedExpenditureCurr);
    //        body = body.Replace("{#advancerequired#}", advanceRequired);
    //        body = body.Replace("{#advancerequiredcurr#}", advanceRequiredCurr);

    //        body = body.Replace("{#approvedby#}", hodApprovedBy);
    //        body = body.Replace("{#approvedon#}", hodApprovedOn);
    //        body = body.Replace("{#approvedremarks#}", hodApprovedRemarks);

    //        body = body.Replace("{#finalapprovedby#}", finalApprovedBy);
    //        body = body.Replace("{#finalapprovedon#}", finalApprovedOn);
    //        body = body.Replace("{#finalapprovedremarks#}", finalApprovedRemarks);


    //        body = body.Replace("{#accname#}", accTeamName);
    //        body = body.Replace("{#advanceamount#}", Convert.ToString(advanceAmt));
    //        body = body.Replace("{#advancecurrency#}", advanceAmtCurrency);

    //        body = body.Replace("{#deletedby#}", deletedBy);
    //        body = body.Replace("{#deletedon#}", deletedOn);
    //        body = body.Replace("{#deletedremark#}", deletedRemarks);

    //        body = body.Replace("{#approvelink#}", approveHref);
    //        body = body.Replace("{#deletelink#}", deleteHref);

    //        mail.Body = body;

    //        SmtpServer.Host = "eusmtp.hi.corp";
    //        SmtpServer.Port = 25;
    //        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

    //        try
    //        {
    //            SmtpServer.Send(mail);
    //            sendVal = 1;
    //        }
    //        catch (Exception ex)
    //        {
    //            string exMsg = ex.ToString();
    //            if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
    //                sendVal = 1;

    //            else
    //                sendVal = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sendVal = 0;
    //    }
    //    return sendVal;
    //}



    //private void AttachAdviceToMail(int isAdviceGenerated
    //                              , string createdByIFSC
    //                              , string createdByAccNo
    //                              , int unitID
    //                              , System.Net.Mail.MailMessage mail)
    //{
    //    string csvTxt = string.Empty;

    //    if (Convert.ToInt32(advanceAmt) > 0 && advanceAmtcurrencyID == 68 &&
    //                isAdviceGenerated == 0 && !string.IsNullOrEmpty(Convert.ToString(createdByAccNo)) &&
    //                !string.IsNullOrEmpty(Convert.ToString(createdByIFSC))
    //               )
    //    {
    //        csvTxt = GenerateAdviceCSV(createdBy, sanctionNo, Convert.ToString(advanceAmt), createdByEmail, createdByAccNo, createdByIFSC, unitID);
    //        MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(csvTxt));
    //        Attachment attachment = new Attachment(stream, new ContentType("text/csv"));


    //        string[] strTxt = sanctionNo.Split('/');
    //        string sanctionNoTxt = string.Empty;
    //        foreach (string item in strTxt)
    //        {
    //            sanctionNoTxt += item + "_";
    //        }
    //        if (!string.IsNullOrEmpty(sanctionNoTxt))
    //            attachment.Name = "Payment_Advice_" + sanctionNoTxt.TrimEnd('_') + ".CSV";
    //        else
    //            attachment.Name = "Payment_Advice.CSV";

    //        mail.Attachments.Add(attachment);
    //    }
    //}

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