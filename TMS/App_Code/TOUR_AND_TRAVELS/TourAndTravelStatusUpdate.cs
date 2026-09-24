using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class TourAndTravelStatusUpdate
{
    TourAndTravels objTourAndTravels = new TourAndTravels();
    private const string _NoValue = "0:0";
    private const string _InsertValue = "1:0";
    private const string _InsertAndSentMailValue = "1:1";

    public TourAndTravelStatusUpdate()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string UpdateTourStatus(int actID,
                                  int tourID,
                                  //string tourNO,
                                  int createdByID,
                                  int tourStatusID,
                                  int travelModeID,
                                  int tourBasedOnID,
                                  string remarks,
                                  int teamLeaderID,
                                  int currentUserID,
                                  int accountCheckerID)
    {
        string returnVal = _NoValue; //"0:0";
        try
        {
            bool isMailSend = false;
            string tourSanctionNo = "";


            //if (teamLeaderID == Convert.ToInt32(TandTAllStatus.EnumOthers.FinalApproverID) &&
            //    createdByID == Convert.ToInt32(TandTAllStatus.EnumOthers.UKVEmpRecordID))//UKV's Tour
            //{
            //    if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) ||
            //                (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))

            //    {
            //        if(actID != 3 && actID != 4)
            //        {
            //            isMailSend = true;
            //            actID = (int)TandTAllStatus.EnumTourAct.UKVApproveByParzer;
            //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer;
            //        }
                 
            //    }
            //    else 
            //    {
            //        if (actID != 3 && actID != 4)
            //        {
            //            isMailSend = true;
            //            actID = (int)TandTAllStatus.EnumTourAct.UKVApproveByParzer;
            //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer;
            //        }
            //    }

                
            //}
           // else
            //{
                if (actID == (int)TandTAllStatus.EnumTourAct.HODApprove)
                {
                    isMailSend = true;
                    actID = (int)TandTAllStatus.EnumTourAct.HODApprove;
                    tourStatusID = (int)TandTAllStatus.EnumTourStatus.HODApproved;

                    //if (currentUserID == Convert.ToInt32(TandTAllStatus.EnumOthers.UKVEmpRecordID))
                    //{
                        if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) ||
                            (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
                        {
                            //actID = (int)TandTAllStatus.EnumTourAct.MgmtApprove;
                            //tourStatusID = (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved;

                            isMailSend = true;
                            actID = (int)TandTAllStatus.EnumTourAct.HODApprove;
                            tourStatusID = (int)TandTAllStatus.EnumTourStatus.HODApproved;
                        }
                        else
                        {
                            isMailSend = true;
                            actID = (int)TandTAllStatus.EnumTourAct.FinalApprove;
                            tourStatusID = (int)TandTAllStatus.EnumTourStatus.FinalApproved;
                        }
                    //y}
                }

                //else if (actID == (int)TandTAllStatus.EnumTourAct.MgmtApprove)
                //{
                //    isMailSend = true;
                //    tourStatusID = (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved;
                //}

                else if (actID == (int)TandTAllStatus.EnumTourAct.FinalApprove)
                {
                    isMailSend = true;
                    tourStatusID = (int)TandTAllStatus.EnumTourStatus.FinalApproved;
                }

            //}


            if (actID == (int)TandTAllStatus.EnumTourAct.Delete)
            {
                isMailSend = true;
                tourStatusID = (int)TandTAllStatus.EnumTourStatus.Deleted;
            }

            if (actID == (int)TandTAllStatus.EnumTourAct.Cancel)
            {
                isMailSend = true;
                tourStatusID = (int)TandTAllStatus.EnumTourStatus.Cancelled;
            }

            //int value = objTourAndTravels.UpdateTourStatus(tourID
            //                                                , tourStatusID
            //                                                , actID
            //                                                , remarks
            //                                                , currentUserID);// Convert.ToInt32(Session["EMP_RECORD_ID"]));

            string value = objTourAndTravels.UpdateTourStatusString(tourID
                                                            , tourStatusID
                                                            , actID
                                                            , remarks
                                                            , currentUserID);// Convert.ToInt32(Session["EMP_RECORD_ID"]));


            if (!string.IsNullOrEmpty(value))
            {
                tourSanctionNo = Convert.ToString(value.Split(':')[1]);
                returnVal = _InsertValue + ":" + tourStatusID + ":" + tourSanctionNo;
                if (isMailSend)
                {
                    TourSendMail tsm = new TourSendMail();
                    //int sendMailValue = tsm.SendMail(tourID);
                    string sendMailValue = tsm.SendMail(tourID);

                    if (!string.IsNullOrEmpty(sendMailValue))
                    {

                        int val = objTourAndTravels.UpdateTourInfoMailStatus(actID
                                                                           , tourID
                                                                           , accountCheckerID);

                        returnVal = _InsertAndSentMailValue + ":" + tourStatusID + ":" + tourSanctionNo;
                    }
                }
            }

            return returnVal;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

}