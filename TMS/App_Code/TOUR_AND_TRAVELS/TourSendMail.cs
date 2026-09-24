using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for TourSendMail
/// </summary>
public class TourSendMail
{
    public TourSendMail()
    {

    }

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();

    //double advanceAmount = 0;
    int advanceAmtcurrencyID = 0;
    string advanceAmtcurrencyCode = string.Empty;
    string jobNo = string.Empty;

    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;

    string subject = string.Empty;
    string fileName = string.Empty;
    string tourNo = string.Empty;
    string sanctionNo = string.Empty;
    string tourStatus = string.Empty;
    int tourStatusID = 0;
    int travelModeID = 0;

    string createdBy = string.Empty;
    string createdByEmail = string.Empty;
    string createdOn = string.Empty;
    string createdRemarks = string.Empty;


    string mgmtHodApprovedBy = string.Empty;
    string mgmtHodApprovedByEmail = string.Empty;
    string mgmtHodApprovedOn = string.Empty;
    string mgmtHodApprovedRemarks = string.Empty;

    string hodApprovedBy = string.Empty;
    string hodApprovedByEmail = string.Empty;
    string hodApprovedOn = string.Empty;
    string hodApprovedRemarks = string.Empty;

    int createdById = 0;
    int deletedById = 0;
    string deletedBy = string.Empty;
    string deletedByEmail = string.Empty;
    string deletedOn = string.Empty;
    string deletedRemarks = string.Empty;

    string cancelledBy = string.Empty;
    string cancelledByEmail = string.Empty;
    string cancelledOn = string.Empty;
    string cancelledRemarks = string.Empty;

    string finalApprovedBy = string.Empty;
    string finalApprovedByEmail = string.Empty;
    string finalApprovedOn = string.Empty;
    string finalApprovedRemarks = string.Empty;

    int finalApprovarEmpRecordID = 0;
    string finalApprovarUN = string.Empty;
    string finalApprovarPWD = string.Empty;
    string finalApprovarEmail = string.Empty;
    string finalApprovarName = string.Empty;

    int mgmtApprovarEmpRecordID = 0;
    string mgmtApprovarUN = string.Empty;
    string mgmtApprovarPWD = string.Empty;
    string mgmtApprovarEmail = string.Empty;
    string mgmtApprovarName = string.Empty;

    string hrTeamEmail = string.Empty;

    string accTeamEmail = string.Empty;
    string accTeamName = string.Empty;

    double advanceAmt = 0;
    string advanceAmtCurrency = string.Empty;
    string accAcvnaceFCEmail = string.Empty;
    DataSet dsUserInfo = new DataSet();


    string fromDate = string.Empty;
    string toDate = string.Empty;
    string customeName = string.Empty;
    string placeOfvisit = string.Empty;
    string travelMode = string.Empty;
    string purposeOfVisit = string.Empty;
    string typeOfTrip = string.Empty;
    string modeOfTravel = string.Empty;
    string expectedExpenditure = string.Empty;
    string expectedExpenditureCurr = string.Empty;
    string advanceRequired = string.Empty;
    string advanceRequiredCurr = string.Empty;

    string tlName = string.Empty;
    string tlEmail = string.Empty;

    int tlEmpRecordID = 0;
    string tlUserName = string.Empty;
    string tlPwd = string.Empty;

    string approveHref = string.Empty;
    string approveLink = string.Empty;

    string deleteHref = string.Empty;
    string deleteLink = string.Empty;

    int unitID = 0;
    int isAdviceGenerated = 0;
    int tourBasedOnID = 0;

    string createdByAccNo = string.Empty;
    string createdByIFSC = string.Empty;



    string countryOfVisit = string.Empty;
    string tourBasedOn = string.Empty;

    #endregion



    public string SendMail(int tourID)
    {
        //int sendVal = 0;

        string sendVal = "";

        try
        {
            dsUserInfo = objTourAndTravels.GetRequesterInfo(tourID);
            if (dsUserInfo.Tables.Count > 0 && dsUserInfo.Tables[0].Rows.Count > 0)
            {
                DataRow dr0 = dsUserInfo.Tables[0].Rows[0];

                tourNo = Convert.ToString(dr0["TOUR_NO"]);
                sanctionNo = Convert.ToString(dr0["TOUR_SANCTION_NO"]);
                tourStatus = Convert.ToString(dr0["TOUR_STATUS_NAME"]);
                tourStatusID = Convert.ToInt32(dr0["TOUR_STATUS_ID"]);

                travelModeID = Convert.ToInt32(dr0["TRAVEL_MODE_ID"]);
                tourBasedOnID = Convert.ToInt32(dr0["CUSTOMER_BASED_ON_ID"]);

                fromDate = Convert.ToDateTime(dr0["START_DATE"]).ToString("dd-MMM-yyyy");
                toDate = Convert.ToDateTime(dr0["END_DATE"]).ToString("dd-MMM-yyyy");
                customeName = Convert.ToString(dr0["CUST_VEND_NAME"]);
                placeOfvisit = Convert.ToString(dr0["PLACE_OF_VISIT"]);
                travelMode = Convert.ToString(dr0["TRAVEL_MODE"]);
                jobNo = Convert.ToString(dr0["JOB_NO"]);

                purposeOfVisit = Convert.ToString(dr0["VISIT_TYPE"]);
                typeOfTrip = Convert.ToString(dr0["TRIP_TYPE"]);
                modeOfTravel = Convert.ToString(dr0["TRAVEL_MODE"]);
                expectedExpenditure = Convert.ToString(dr0["EXPENDITURE_AMT"]);
                expectedExpenditureCurr = Convert.ToString(dr0["EXPENDITURE_CURRENCY"]);
                advanceRequired = Convert.ToString(dr0["ADVANCE_AMT"]);
                advanceRequiredCurr = Convert.ToString(dr0["CURRENCY"]);
                countryOfVisit = Convert.ToString(dr0["COUNTRY_OF_VISIT"]);
                tourBasedOn = Convert.ToString(dr0["CUSTOMER_BASED_ON"]);



                //if (!(Convert.ToInt32(advanceAmt) > 0))
                //{
                //    advanceRequiredCurr = "";
                //}

                createdBy = Convert.ToString(dr0["CREATED_BY"]);
                createdByEmail = Convert.ToString(dr0["CREATED_BY_EMAIL"]);
                if (dr0["CREATED_ON"] != DBNull.Value)
                    createdOn = Convert.ToDateTime(dr0["CREATED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                createdRemarks = Convert.ToString(dr0["CREATED_REMARKS"]);

                if (dr0["CREATED_BY_ACC"] != DBNull.Value)
                    createdByAccNo = Convert.ToString(dr0["CREATED_BY_ACC"]);

                if (dr0["CREATED_BY_IFSC"] != DBNull.Value)
                    createdByIFSC = Convert.ToString(dr0["CREATED_BY_IFSC"]);

                hodApprovedBy = Convert.ToString(dr0["APPROVED_BY"]);
                if (dr0["APPROVED_ON"] != DBNull.Value)
                    hodApprovedOn = Convert.ToDateTime(dr0["APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                hodApprovedByEmail = Convert.ToString(dr0["APPROVED_BY_EMAIL"]);
                hodApprovedRemarks = Convert.ToString(dr0["APPROVED_REMARKS"]);



                mgmtHodApprovedBy = Convert.ToString(dr0["MGMT_APPROVED_BY"]);
                if (dr0["MGMT_APPROVED_ON"] != DBNull.Value)
                    mgmtHodApprovedOn = Convert.ToDateTime(dr0["MGMT_APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                mgmtHodApprovedByEmail = Convert.ToString(dr0["MGMT_APPROVED_BY_EMAIL"]);
                mgmtHodApprovedRemarks = Convert.ToString(dr0["MGMT_APPROVED_REMARKS"]);



                finalApprovedBy = Convert.ToString(dr0["FINAL_APPROVED_BY"]);
                if (dr0["FINAL_APPROVED_ON"] != DBNull.Value)
                    finalApprovedOn = Convert.ToDateTime(dr0["FINAL_APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                finalApprovedByEmail = Convert.ToString(dr0["FINAL_APPROVED_BY_EMAIL"]);
                finalApprovedRemarks = Convert.ToString(dr0["FINAL_APPROVED_REMARKS"]);

                tlEmpRecordID = Convert.ToInt32(dr0["TEAMLEADER_EMP_RECORD_ID"]);
                tlUserName = Convert.ToString(dr0["TEAMLEADER_USER_NAME"]);
                tlPwd = Convert.ToString(dr0["TEAMLEADER_PASSWORD"]);
                tlName = Convert.ToString(dr0["TEAMLEADER_NAME"]);
                tlEmail = Convert.ToString(dr0["TEAMLEADER_EMAIL"]);


                if (dr0["ADVANCE_AMT"] != DBNull.Value)
                {
                    advanceAmt = Convert.ToDouble(dr0["ADVANCE_AMT"]);
                    advanceAmtcurrencyCode = Convert.ToString(dr0["CURRENCY_CODE"]);
                    advanceAmtCurrency = Convert.ToString(dr0["CURRENCY"]);
                    advanceAmtcurrencyID = Convert.ToInt32(dr0["ADVANCE_CURRENCY"]);
                }

                deletedBy = Convert.ToString(dr0["DELETED_BY"]);
                if (dr0["DELETED_ON"] != DBNull.Value)
                    deletedOn = Convert.ToDateTime(dr0["DELETED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                deletedByEmail = Convert.ToString(dr0["DELETED_BY_EMAIL"]);
                deletedRemarks = Convert.ToString(dr0["DELETED_REMARKS"]);

                cancelledBy = Convert.ToString(dr0["CANCELLED_BY"]);
                if (dr0["CANCELLED_ON"] != DBNull.Value)
                    cancelledOn = Convert.ToDateTime(dr0["CANCELLED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                cancelledByEmail = Convert.ToString(dr0["CANCELLED_BY_EMAIL"]);
                cancelledRemarks = Convert.ToString(dr0["CANCELLED_REMARKS"]);

                unitID = Convert.ToInt32(dr0["UNIT_ID"]);
                isAdviceGenerated = Convert.ToInt32(dr0["IS_ADVICE_GENERATED"]);

                createdById = Convert.ToInt32(dr0["EMP_RECORD_ID"]);
                //deletedById = Convert.ToInt32(dr0["DELETED_BY_ID"]);

                if (dr0["DELETED_BY_ID"] != DBNull.Value)
                {
                     deletedById = Convert.ToInt32(dr0["DELETED_BY_ID"]);
                }
                else
                {
                    deletedById = 0;
                }


                //HR Team
                if (dsUserInfo.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsUserInfo.Tables[1].Rows)
                    {
                        hrTeamEmail += ";" + Convert.ToString(dr["HR_EMAIL"]);
                    }
                    //hrTeamEmail = hrTeamEmail.TrimStart(',');
                }

                //Acc Team
                if (dsUserInfo.Tables[2].Rows.Count > 0)
                {
                    accTeamName = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["ACC_NAME"]);
                    accTeamEmail = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["ACC_EMAIL"]);
                }

                //Senthil And Pawan Sharma
                if (dsUserInfo.Tables[3].Rows.Count > 0)
                {
                    //accAcvnaceFCEmail = Convert.ToString(dsUserInfo.Tables[3].Rows[0]["EMAIL_ID"]);
                    foreach (DataRow dr in dsUserInfo.Tables[3].Rows)
                    {
                        accAcvnaceFCEmail += ";" + Convert.ToString(dr["EMAIL_ID"]);
                    }
                    //accAcvnaceFCEmail = accAcvnaceFCEmail.TrimStart(',');
                }

                if (dsUserInfo.Tables[5].Rows.Count > 0)
                {
                    //For final approval
                    DataRow dr5 = dsUserInfo.Tables[5].Rows[0];

                    finalApprovarEmpRecordID = Convert.ToInt32(dr5["EMP_RECORD_ID"]);
                    finalApprovarUN = Convert.ToString(dr5["UN"]);
                    finalApprovarPWD = Convert.ToString(dr5["PWD"]);
                    finalApprovarName = Convert.ToString(dr5["EMPLOYEE_NAME"]);
                    finalApprovarEmail = Convert.ToString(dr5["EMAIL_ID"]);
                }

                if (dsUserInfo.Tables[6].Rows.Count > 0)
                {
                    //For Mgmt approval
                    DataRow dr6 = dsUserInfo.Tables[6].Rows[0];

                    mgmtApprovarEmpRecordID = Convert.ToInt32(dr6["EMP_RECORD_ID"]);
                    mgmtApprovarUN = Convert.ToString(dr6["UN"]);
                    mgmtApprovarPWD = Convert.ToString(dr6["PWD"]);
                    mgmtApprovarName = Convert.ToString(dr6["EMPLOYEE_NAME"]);
                    mgmtApprovarEmail = Convert.ToString(dr6["EMAIL_ID"]);
                }
            }
            else
            {
                sendVal = "";
            }


            //string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);
            string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);

            bool chkFinalApproval = false;
            if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) ||
                 (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
            {
                chkFinalApproval = true;
            }



            //New
            if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.New)
            {
                from = createdByEmail;
                to = tlEmail;
                bcc = createdByEmail;
                fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/01ReqToHodTourMail.htm";
                subject = "Tour No.: '" + tourNo + "' Created On: " + createdOn;


                int actId = 2;

                if (tlEmpRecordID == (int)TandTAllStatus.EnumOthers.FinalApproverID)
                {
                    actId = 6;
                }
               

                approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=" + actId + "&tourstatusid=" + tourStatusID + "&travelmodeid=" + travelModeID + "&tourbasedonid=" + tourBasedOnID + "&toursanctionnumber=" + sanctionNo + "&ud=" + tlUserName + "&pd=" + tlPwd + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) + "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";
                approveHref = "<a href=" + approveLink + ">Approve Tour request</a>";

                deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo +
                    "&actid=3" + "&tourstatusid=" + tourStatusID + "&travelmodeid=" + travelModeID + "&tourbasedonid=" + tourBasedOnID + 
                    "&toursanctionnumber=" + sanctionNo + "&ud=" + tlUserName + "&pd=" + tlPwd + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) +
                    "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";
                deleteHref = "<a href=" + deleteLink + ">Reject Tour request</a>";
            }

            //HOD Approved
            if ((tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)) // hod ko approve karna hai
            {
                if (chkFinalApproval)
                {
                    from = hodApprovedByEmail;
                    to = finalApprovarEmail;

                    //bcc = "minni.gupta@coperion.com;

                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/01ReqToFinalHodTourMail.htm";
                    subject = "Tour No.: '" + tourNo + "' Created On: " + createdOn;

                    approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=6&tourstatusid=" + tourStatusID + "&travelmodeid=" + travelModeID + "&tourbasedonid=" + tourBasedOnID + "&toursanctionnumber=" + sanctionNo + "&ud=" + finalApprovarUN + "&pd=" + finalApprovarPWD + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) + "&emprecordid=" + Convert.ToString(finalApprovarEmpRecordID) + "'";
                    approveHref = "<a href=" + approveLink + ">Approve Tour request</a>";

                    deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=3&tourstatusid=" + tourStatusID + "&travelmodeid=" + travelModeID + "&tourbasedonid=" + tourBasedOnID + "&toursanctionnumber=" + sanctionNo + "&ud=" + finalApprovarUN + "&pd=" + finalApprovarPWD + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) + "&emprecordid=" + Convert.ToString(finalApprovarEmpRecordID) + "'";
                    //deleteHref = "<a href=" + deleteLink + ">Delete Tour request</a>";
                    deleteHref = "<a href=" + deleteLink + ">Reject Tour request</a>";
                }
                else
                {

                    if (Convert.ToInt32(advanceAmt) > 0)
                    {

                        from = hodApprovedByEmail;
                        bcc = hodApprovedByEmail;
                        //68 for INR in DB
                        if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
                        {
                            to = accTeamEmail;
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourMail.htm";
                        }
                        else
                        {
                            to = accAcvnaceFCEmail;
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourMailForeignTour.htm";
                        }
                        cc = hrTeamEmail + "," + createdByEmail;                                                  
                        subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
                    }
                    else
                    {
                        from = hodApprovedByEmail;
                        to = createdByEmail;
                        cc = hrTeamEmail;
                        bcc = hodApprovedByEmail;

                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";
                        subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
                    }











                    //from = hodApprovedByEmail;
                    //to = mgmtApprovarEmail;
                    ////bcc = createdByEmail;
                    //fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/01ReqToMgmtHodTourMail.htm";
                    //subject = "Tour No.: '" + tourNo + "' Created On: " + createdOn;


                    //approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=7" + "&tourstatusid=" + tourStatusID + "&travelmodeid=" + travelModeID + "&tourbasedonid=" + tourBasedOnID + "&toursanctionnumber=" + sanctionNo + "&ud=" + mgmtApprovarUN + "&pd=" + mgmtApprovarPWD + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) + "&emprecordid=" + Convert.ToString(mgmtApprovarEmpRecordID) + "'";
                    //approveHref = "<a href=" + approveLink + ">Approve Tour request</a>";

                    //deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=3" + "&tourstatusid=" + tourStatusID + "&travelmodeid=" + travelModeID + "&tourbasedonid=" + tourBasedOnID + "&toursanctionnumber=" + sanctionNo + "&ud=" + mgmtApprovarUN + "&pd=" + mgmtApprovarPWD + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) + "&emprecordid=" + Convert.ToString(mgmtApprovarEmpRecordID) + "'";
                    //deleteHref = "<a href=" + deleteLink + ">Delete Tour request</a>";
                }
            }



            //Mgmt Approved
            //else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved)
            //{

            //    if (chkFinalApproval)
            //    {
            //        from = mgmtHodApprovedByEmail;
            //        to = finalApprovarEmail;

            //        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/01ReqToFinalHodTourMail.htm";
            //        subject = "Tour No.: '" + tourNo + "' Created On: " + createdOn;

            //        approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=6&tourstatusid=" + tourStatusID + "&travelmodeid=" + travelModeID + "&tourbasedonid=" + tourBasedOnID + "&toursanctionnumber=" + sanctionNo + "&ud=" + finalApprovarUN + "&pd=" + finalApprovarPWD + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) + "&emprecordid=" + Convert.ToString(finalApprovarEmpRecordID) + "'";
            //        approveHref = "<a href=" + approveLink + ">Approve Tour request</a>";

            //        deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=3&tourstatusid=" + tourStatusID + "&travelmodeid=" + travelModeID + "&tourbasedonid=" + tourBasedOnID + "&toursanctionnumber=" + sanctionNo + "&ud=" + finalApprovarUN + "&pd=" + finalApprovarPWD + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) + "&emprecordid=" + Convert.ToString(finalApprovarEmpRecordID) + "'";
            //        deleteHref = "<a href=" + deleteLink + ">Delete Tour request</a>";
            //    }
            //    else
            //    {
            //        if (Convert.ToInt32(advanceAmt) > 0)
            //        {
            //            from = mgmtHodApprovedByEmail;
            //            bcc = hodApprovedByEmail;
            //            //68 for INR in DB
            //            if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
            //            {
            //                to = accTeamEmail;
            //                fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourMail.htm";
            //            }
            //            else
            //            {
            //                to = accAcvnaceFCEmail;
            //                fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourMailForeignTour.htm";
            //            }
            //            cc = hrTeamEmail + "," + createdByEmail;
            //            subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
            //        }
            //        else
            //        {
            //            from = mgmtHodApprovedByEmail;
            //            to = createdByEmail;
            //            cc = hrTeamEmail;
            //            bcc = hodApprovedByEmail;

            //            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";
            //            subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
            //        }
            //    }

            //}

            //FinalApproved
            else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
            {
                if (Convert.ToInt32(advanceAmt) > 0)
                {
                    from = finalApprovedByEmail;
                    bcc = finalApprovedByEmail;

                    


                    //68 for INR in DB
                    if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
                    {
                        to = accTeamEmail;
                        //fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourFinalMail.htm";
                        if (chkFinalApproval)
                        {
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourFinalMail.htm";
                        }
                        else 
                        {
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourMail.htm";
                        }
                    }
                    else
                    {
                        to = accAcvnaceFCEmail;
                        //fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourFinalMailForeignTour.htm";
                        if (chkFinalApproval)
                        {
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourFinalMailForeignTour.htm";
                        }
                        else
                        {
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourMailForeignTour.htm";
                        }


                    }
                    cc = hodApprovedByEmail + ";" + hrTeamEmail + ";" + createdByEmail;
                    subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
                }
                else
                {
                    from = finalApprovedByEmail;
                    to = createdByEmail;
                    //cc = hodApprovedByEmail + ";" + hrTeamEmail;
                    cc =  hrTeamEmail;
                    bcc = finalApprovedByEmail;
                    subject = "Tour Sanction No. " + sanctionNo + " generated on: " + finalApprovedOn;

                    if (chkFinalApproval)
                    {
                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourFinalMail.htm";
                    }
                    else
                    {
                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";
                    }
                }

            }


           else if ((tourStatusID == (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer))
            {

                if (Convert.ToInt32(advanceAmt) > 0)
                {


                    if (chkFinalApproval)
                    {
                        
                        from = finalApprovedByEmail;//(Markus Parzer)
                        bcc = finalApprovedByEmail;
                        hodApprovedBy = finalApprovedBy;
                        hodApprovedRemarks = finalApprovedRemarks;
                        hodApprovedOn = finalApprovedOn;

                        //68 for INR in DB
                        if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
                        {
                            to = accTeamEmail;
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourMail.htm";
                        }
                        else
                        {
                            to = accAcvnaceFCEmail;
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourFinalMailForeignTour.htm";
                        }
                        //cc = hodApprovedByEmail + ";" + hrTeamEmail + ";" + createdByEmail;
                        cc = hrTeamEmail + ";" + createdByEmail;
                        subject = "Tour Sanction No. " + sanctionNo + " generated on: " + finalApprovedOn;


                    }
                    else
                    {
                        //hodApprovedByEmail = finalApprovedByEmail;
                        from = finalApprovedByEmail;//(Markus Parzer)
                        bcc = finalApprovedByEmail;
                        hodApprovedBy = finalApprovedBy;
                        hodApprovedRemarks = finalApprovedRemarks;
                        hodApprovedOn = finalApprovedOn;

                        //68 for INR in DB
                        if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
                        {
                            to = accTeamEmail;
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourFinalMail.htm";
                        }
                        else
                        {
                            to = accAcvnaceFCEmail;
                            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourFinalMailForeignTour.htm";
                        }
                        //cc = hodApprovedByEmail + ";" + hrTeamEmail + ";" + createdByEmail;
                        cc = hrTeamEmail + ";" + createdByEmail;
                        subject = "Tour Sanction No. " + sanctionNo + " generated on: " + finalApprovedOn;



                    }

                }
                else
                {

                    from = finalApprovedByEmail;
                    to = createdByEmail;
                    //cc = hodApprovedByEmail + ";" + hrTeamEmail;
                    cc = hrTeamEmail;
                    bcc = finalApprovedByEmail;
                    subject = "Tour Sanction No. " + sanctionNo + " generated on: " + finalApprovedOn;
                    hodApprovedBy = finalApprovedBy;
                    hodApprovedRemarks = finalApprovedRemarks;
                    hodApprovedOn = finalApprovedOn;

                    if (chkFinalApproval)
                    {
                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourFinalMail.htm";
                    }
                    else
                    {
                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";
                    }
                }
            }


            //Deleted
            else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
            {
                //from = deletedByEmail;
                ////to = hodApprovedByEmail;  //createdByEmail;
                //to = createdByEmail;
                //bcc = deletedByEmail;

                //fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/05DeleteTourInfoMail.htm";
                //subject = "Tour Information No. " + tourNo + " Deleted On: " + deletedOn;

                if (deletedById == createdById)
                {
                    from = deletedByEmail;
                    //to = hodApprovedByEmail;  //createdByEmail;
                    to = createdByEmail;
                    bcc = deletedByEmail;

                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/05DeleteTourInfoMail.htm";
                    subject = "Tour Information No. " + tourNo + " Deleted On: " + deletedOn;
                }
                else
                {
                    from = deletedByEmail;
                    //to = hodApprovedByEmail;  //createdByEmail;
                    to = createdByEmail;
                    bcc = deletedByEmail;

                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/05RejectTourInfoMail.htm";
                    subject = "Tour Information No. " + tourNo + " Rejected On: " + deletedOn;
                }



            }

            //Cancelled
            else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
            {
                from = cancelledByEmail;
                to = createdByEmail;  //hodApprovedByEmail;
                cc = hrTeamEmail + ";" + accTeamEmail + ";" + hodApprovedByEmail;
                bcc = cancelledByEmail;
                if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
                    cc = hrTeamEmail + ";" + accTeamEmail;
                else
                    cc = hrTeamEmail + ";" + accAcvnaceFCEmail;

                fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/04CancelTourInfoMail.htm";
                subject = "Tour Information No. " + tourNo + " Cancelled On: " + cancelledOn;
            }


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);


            //2024-02-14
            //if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
            //{
            //    if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
            //    {
            //        AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
            //    }
            //}
            //else
            //{
            //    if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
            //    {
            //        AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
            //    }
            //}

            //if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved)
            //{
            //    if (!chkFinalApproval)
            //    {
            //        AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
            //    }
            //}

            if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
            {
                if (!chkFinalApproval)
                {
                    AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
                }
            }

            if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
            {
                AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
            }

            if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer)
            {
                AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
            }

            //if (chkFinalApproval)
            //{
            //    if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
            //    {
            //        AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
            //    }
            //}
            //else
            //{
            //    if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved ||
            //        tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
            //    {
            //        AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
            //    }

            //}






            ////////
            if (!(Convert.ToInt32(advanceAmt) > 0))
            {
                advanceRequiredCurr = "";
            }

            //////

            //if (!string.IsNullOrEmpty(to))
            //{
            //    string[] strTo = to.Split(';');
            //    foreach (string item in strTo)
            //    {
            //        if (!string.IsNullOrEmpty(item))
            //            mail.To.Add(item);
            //    }
            //}

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

            //if (!string.IsNullOrEmpty(cc))
            //{
            //    string[] strCC = cc.Split(';');
            //    foreach (string item in strCC)
            //    {
            //        if (!string.IsNullOrEmpty(item))
            //            mail.CC.Add(item);
            //    }
            //}

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


            //if (!string.IsNullOrEmpty(bcc))
            //{
            //    string[] strBCC = bcc.Split(';');
            //    foreach (string item in strBCC)
            //    {
            //        if (!string.IsNullOrEmpty(item))
            //            mail.Bcc.Add(item);
            //    }
            //}

            if (!string.IsNullOrEmpty(bcc))
            {
                bcc = bcc.TrimEnd(';');
                string[] strBCC = bcc.Split(';');
                foreach (string item in strBCC)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.Bcc.Add(item);
                    }
                }
            }



            mail.Subject = subject;
            mail.IsBodyHtml = true;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#tourno#}", tourNo);
            body = body.Replace("{#sanctionno#}", sanctionNo);

            body = body.Replace("{#createdby#}", createdBy);
            body = body.Replace("{#createdon#}", createdOn);
            body = body.Replace("{#requesterremarks#}", createdRemarks);

            body = body.Replace("{#fromdate#}", fromDate);
            body = body.Replace("{#todate#}", toDate);
            body = body.Replace("{#customername#}", customeName);
            body = body.Replace("{#place#}", placeOfvisit);
            body = body.Replace("{#jobno#}", jobNo);

            body = body.Replace("{#countryofvisit#}", countryOfVisit);
            body = body.Replace("{#tourbasedon#}", tourBasedOn);


            body = body.Replace("{#purposeofvisit#}", purposeOfVisit);
            body = body.Replace("{#typeoftrip#}", typeOfTrip);
            body = body.Replace("{#modeOfTravel#}", modeOfTravel);
            body = body.Replace("{#expectedexpenditure#}", expectedExpenditure);
            body = body.Replace("{#expectedexpenditurecurr#}", expectedExpenditureCurr);
            body = body.Replace("{#advancerequired#}", advanceRequired);
            body = body.Replace("{#advancerequiredcurr#}", advanceRequiredCurr);

            body = body.Replace("{#approvedby#}", hodApprovedBy);
            body = body.Replace("{#approvedon#}", hodApprovedOn);
            body = body.Replace("{#approvedremarks#}", hodApprovedRemarks);

            body = body.Replace("{#mgmtHodApprovedby#}", mgmtHodApprovedBy);
            body = body.Replace("{#mgmtHodApprovedon#}", mgmtHodApprovedOn);
            body = body.Replace("{#mgmtHodApprovedremarks#}", mgmtHodApprovedRemarks);



            body = body.Replace("{#finalapprovedby#}", finalApprovedBy);
            body = body.Replace("{#finalapprovedon#}", finalApprovedOn);
            body = body.Replace("{#finalapprovedremarks#}", finalApprovedRemarks);

            body = body.Replace("{#accname#}", accTeamName);
            body = body.Replace("{#advanceamount#}", Convert.ToString(advanceAmt));
            body = body.Replace("{#advancecurrency#}", advanceAmtCurrency);

            body = body.Replace("{#deletedby#}", deletedBy);
            body = body.Replace("{#deletedon#}", deletedOn);
            body = body.Replace("{#deletedremark#}", deletedRemarks);

            body = body.Replace("{#cancelledby#}", cancelledBy);
            body = body.Replace("{#cancelledon#}", cancelledOn);
            body = body.Replace("{#cancelledremark#}", cancelledRemarks);

            body = body.Replace("{#approvelink#}", approveHref);
            body = body.Replace("{#deletelink#}", deleteHref);

            mail.Body = body;

            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                SmtpServer.Send(mail);
                //sendVal = 1;
                sendVal = tourNo;
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    sendVal = tourNo;

                else
                    sendVal = "";
            }
        }
        catch (Exception)
        {
            sendVal = "";
           
        }
        return sendVal;
    }

    private void AttachAdviceToMail(int isAdviceGenerated
                                , string createdByIFSC
                                , string createdByAccNo
                                , int unitID
                                , System.Net.Mail.MailMessage mail)
    {
        string csvTxt = string.Empty;

        if (Convert.ToInt32(advanceAmt) > 0 && advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
                    isAdviceGenerated == 0 && !string.IsNullOrEmpty(Convert.ToString(createdByAccNo)) &&
                    !string.IsNullOrEmpty(Convert.ToString(createdByIFSC))
                   )
        {
            csvTxt = GenerateAdviceCSV(createdBy, sanctionNo, Convert.ToString(advanceAmt), createdByEmail, createdByAccNo, createdByIFSC, unitID);
            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(csvTxt));
            Attachment attachment = new Attachment(stream, new ContentType("text/csv"));


            string[] strTxt = sanctionNo.Split('/');
            string sanctionNoTxt = string.Empty;
            foreach (string item in strTxt)
            {
                sanctionNoTxt += item + "_";
            }
            if (!string.IsNullOrEmpty(sanctionNoTxt))
                attachment.Name = "Payment_Advice_" + sanctionNoTxt.TrimEnd('_') + ".CSV";
            else
                attachment.Name = "Payment_Advice.CSV";

            mail.Attachments.Add(attachment);
        }
    }

    private string GenerateAdviceCSV(string beneficiaryName, string tourSanctionNo, string amount,
                                     string emailAdd1, string beneBankAccount, string beneBankIFSCBANKCode,
                                     int unitID)
    {
        string csvTxt = string.Empty;

        try
        {
            string csv = string.Empty;
            string transactionType = string.Empty;
            string referenceNumber = string.Empty;
            string drAccountNo = string.Empty;

            if (Convert.ToDouble(amount) <= 200000)
                transactionType = "NEFT";
            else
                transactionType = "RTGS";

            referenceNumber = "HSBC";

            //A35
            if (unitID == 1)
                drAccountNo = "'499391803001";
            //Delhi
            else if (unitID == 2)
                drAccountNo = "'499391803005";
            //SEZ
            else if (unitID == 3)
                drAccountNo = "'499391803004";
            //GNU       
            else if (unitID == 4)
                drAccountNo = "'499391803001";

            string paymentNarration = string.Empty;
            string beneAdd1 = string.Empty;
            string beneAdd2 = string.Empty;
            string beneAdd3 = string.Empty;
            string paymentLocation = string.Empty;
            string chequeNo = string.Empty;
            string valueDate = DateTime.Now.ToString("dd/MM/yyyy");
            string printBranchLocation = string.Empty;
            string emailAdd2 = string.Empty;
            string emailAdd3 = string.Empty;
            string freeText = tourSanctionNo;
            string nA1 = string.Empty;
            string nA2 = string.Empty;
            string nA3 = string.Empty;
            string nA4 = string.Empty;
            string nA5 = string.Empty;
            string nA6 = string.Empty;
            string deliverTo = string.Empty;
            string orderingPartyName = "COPERION IDEAL";
            string orderingPartyAdd1 = "PRIVATE LIMITED";
            string orderingPartyAdd2 = "A35 SECTOR 64";
            string orderingPartyAdd3 = "NOIDA";
            string orderingPartyAccount = "201307";
            string bankName = string.Empty;
            string bankToBankInfo = string.Empty;


            csv += "COPERION IDEAL PRIVATE LIMITED,";
            csv += "\r\n";
            //"Transaction_Type,Reference_Number,Dr_Account_No,Payment_Narration,Beneficiary_Name,Bene_Add_1,Bene_Add_2,Bene_Add_3,Payment_Location,Cheque_No,Value_Date,Amount,Print_Branch_Location,Email_Add_1,Email_Add_2,Email_Add_3,Free_Text,NA,NA,NA,NA,NA,Bene_Bank_Account_#,Bene_Bank_IFSC/BANK_Code,NA,Deliver_To,Ordering_Party_Name,Ordering_Party_Add1,Ordering_Party_Add2,Ordering_Party_Add3,Ordering_party_Account,Bank_Name,Bank_to_Bank_Info,";
            csv += "Transaction Type,Reference Number,Dr Account No,Payment Narration,Beneficiary Name,Bene Add 1,Bene Add 2,Bene Add 3,Payment Location,Cheque No,Value Date,Amount,Print Branch Location,Email Add-1,Email Add-2,Email Add-3,FreeText,NA,NA,NA,NA,NA,Bene Bank Account #,Bene Bank IFSC /  BANK Code,NA,Deliver To,Ordering Party Name,Ordering_Party Add1,Ordering_Party Add2,Ordering_Party Add3,Ordering_party_Account,BANK_NAME,Bank_to_Bank_Info,";
            csv += "\r\n";

            csv += transactionType + "," + referenceNumber + "," + drAccountNo + "," +
                            paymentNarration + "," + beneficiaryName + "," + beneAdd1 + "," + beneAdd2 + "," + beneAdd3 + "," + paymentLocation + "," + chequeNo + "," + valueDate + "," + amount + "," +
                            printBranchLocation + "," + emailAdd1 + "," + emailAdd2 + "," + emailAdd3 + "," + freeText + "," + nA1 + "," + nA2 + "," + nA3 + "," + nA4 + "," + nA5 + "," +
                            beneBankAccount + "," + beneBankIFSCBANKCode + "," + nA6 + "," + deliverTo + "," + orderingPartyName + "," +
                            orderingPartyAdd1 + "," + orderingPartyAdd2 + "," + orderingPartyAdd3 + "," + orderingPartyAccount + "," + bankName + "," + bankToBankInfo + ",";



            if (!string.IsNullOrEmpty(csv))
            {
                csvTxt = csv;
            }
            else
            {
                csvTxt = string.Empty;
            }
        }
        catch (Exception)
        {
            csvTxt = string.Empty;
        }
        return csvTxt;
    }

}