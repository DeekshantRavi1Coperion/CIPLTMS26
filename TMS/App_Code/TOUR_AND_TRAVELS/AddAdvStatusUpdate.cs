using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AddAdvStatusUpdate
/// </summary>
public class AddAdvStatusUpdate
{
    private const string _NoValue = "0:0";
    private const string _InsertValue = "1:0";
    private const string _InsertAndSentMailValue = "1:1";
    TourAndTravels objTourAndTravels = new TourAndTravels();
    string IsApproved = string.Empty;


    public AddAdvStatusUpdate()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string UpdateAddAdvanceStatus(int actID,
                                 int requestID,
                                 int requestStatusID,
                                 string remarks,
                                 int teamLeaderID,
                                 int currentUserID,
                                 int accountCheckerID)
    {
        string returnVal = _NoValue; //"0:0";
        try
        {
            bool isMailSend = false;
            //string AddAdvSanctionNo = "";


            
            if (actID == (int)TandTAllStatus.EnumTourAct.New)
            {
                isMailSend = true;
                actID = (int)TandTAllStatus.EnumTourAct.HODApprove;
                requestStatusID = (int)TandTAllStatus.EnumTourStatus.HODApproved;

                
            }

            if (actID == (int)TandTAllStatus.EnumTourAct.HODApprove)
            {
                isMailSend = true;
                //actID = (int)TandTAllStatus.EnumTourAct.HODApprove;
                requestStatusID = (int)TandTAllStatus.EnumTourStatus.HODApproved;


            }

            if (actID == (int)TandTAllStatus.EnumTourAct.Delete)
            {
                isMailSend = true;
                requestStatusID = (int)TandTAllStatus.EnumTourStatus.Deleted;
            }

            if (actID == (int)TandTAllStatus.EnumTourAct.Cancel)
            {
                isMailSend = true;
                requestStatusID = (int)TandTAllStatus.EnumTourStatus.Cancelled;
            }
                                            

            string value = objTourAndTravels.UpdateAddAdvStatusString(requestID
                                                            , requestStatusID
                                                            , actID
                                                            , remarks
                                                            , currentUserID);// Convert.ToInt32(Session["EMP_RECORD_ID"]));
            int val = 0;

            if (!string.IsNullOrEmpty(value))
            {
                IsApproved = Convert.ToString(value.Split(':')[1]);
                returnVal = _InsertValue + ":" + requestStatusID + ":" + IsApproved;
                if (isMailSend)
                {
                    AdditionalAdvanceSendMail tsm = new AdditionalAdvanceSendMail();
                    //int sendMailValue = tsm.SendMail(tourID);
                    string sendMailValue = tsm.SendMail(requestID);

                    if (!string.IsNullOrEmpty(sendMailValue))
                    {

                         val = objTourAndTravels.UpdateAddAdvInfoMailStatus(actID
                                                                           , requestID
                                                                           , accountCheckerID);

                        returnVal = _InsertAndSentMailValue + ":" + requestStatusID + ":" + IsApproved;
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