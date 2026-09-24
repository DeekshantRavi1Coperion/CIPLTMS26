using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class InwardOutwardStatusUpdate
{
    InwardOutward objInwardAndOutward = new InwardOutward();
    InwardOutwardSendMail IOSM = new InwardOutwardSendMail();

    private const string _NoValue = "0:0";
    private const string _InsertValue = "1:0";
    private const string _InsertAndSentMailValue = "1:1";

    public string UpdateInwardOutwardStatus(int actID,
                                  int inwardOutwardID,
                                  //string tourNO,
                                  int createdByID,
                                  int reqStatusID,
                                  string remarks,
                                  int teamLeaderID,
                                  int currentUserID,
                                  string lrNoD,
                                  string lrDateD,
                                  string lrTruckNoD,
                                  byte[] podAttachment1,
                                  string fcrBrNoI,
                                  string fcrBrDateI,
                                  string fcrBrContainerNoI,
                                  byte[]  txtPODAttachment2byte,
                                  string stringConfirmRemarks,
                                  string transporterName,
                                  string transporterEmail,
                                  string transporterContactNum,
                                  string vehiclePlacementDate
                                  )
    {
        string returnVal = _NoValue; //"0:0";
        try
        {
            bool isMailSend = false;

            if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.HODApprove)
            {
                isMailSend = true;
                actID = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.HODApprove;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.HODApproved;

                //isMailSend = true;
                //actID = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.LogisticsConfirm;
                //reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed;


            }
            else if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Acknowledge)
            {
                isMailSend = true;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged;
            }
            else if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Close)
            {
                isMailSend = true;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.Closed;
            }

            else if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.LogisticsConfirm)
            {
                isMailSend = true;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed;
            }


            if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Delete)
            {
                isMailSend = true;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.Deleted;
            }

            if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Cancel)
            {
                isMailSend = true;
                reqStatusID = (int)TandTAllStatus.EnumTourStatus.Cancelled;
            }

            string value = objInwardAndOutward.UpdateInwardOutwardStatusString(inwardOutwardID
                                                            , reqStatusID
                                                            , actID
                                                            , remarks
                                                            , currentUserID
                                                            , lrNoD,
                                                               lrDateD,
                                                               lrTruckNoD,
                                                               podAttachment1,
                                                               fcrBrNoI,
                                                               fcrBrDateI,
                                                               fcrBrContainerNoI,
                                                               txtPODAttachment2byte,
                                                               stringConfirmRemarks,
                                                               transporterName,
                                                               transporterEmail,
                                                               transporterContactNum,
                                                               vehiclePlacementDate);// Convert.ToInt32(Session["EMP_RECORD_ID"]));


            if (!string.IsNullOrEmpty(value))
            {
                string InwOutConfirmationNo = string.Empty;

                InwOutConfirmationNo = Convert.ToString(value.Split(':')[1]);
                returnVal = _InsertValue + ":" + reqStatusID + ":" + InwOutConfirmationNo;
                if (isMailSend)
                {
                    TourSendMail tsm = new TourSendMail();
                    //int sendMailValue = tsm.SendMail(tourID);
                    string sendMailValue = IOSM.SendInwardMail(inwardOutwardID);

                    if (!string.IsNullOrEmpty(sendMailValue))
                    {

                        int val = objInwardAndOutward.UpdateInwardInfoMailStatus(actID
                                                                           , inwardOutwardID);

                        returnVal = _InsertAndSentMailValue + ":" + reqStatusID + ":" + InwOutConfirmationNo;
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


    public string UpdateOutwardStatus(int actID,
                                 int OutwardID,
                                 //string tourNO,
                                 int createdByID,
                                 int reqStatusID,
                                 string remarks,
                                 int teamLeaderID,
                                 int currentUserID,
                                 string lrNoD,
                                 string lrDateD,
                                 string lrInvoiceNo2C,
                                 string lrInvoiceDate2C,
                                 string lrTruckNoD,
                                 byte[] podAttachment1,
                                 string podAttachment1Name,
                                 string fcrBrNoI,
                                 string fcrBrDateI,
                                 string fcrBrInvoiceNo,
                                 string fcrBrInvoiceDate,
                                 string fcrBrContainerNoI,
                                 byte[] podAttachment2,
                                 string podAttachment2Name,
                                 string  stringConfirmRemarks,
                                 string transporterName,
                                 string transporterEmail,
                                 string transporterContactNum
                                 )
    {
        string returnVal = _NoValue; //"0:0";
        try
        {
            bool isMailSend = false;

            if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.HODApprove)
            {
                isMailSend = true;
                actID = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.HODApprove;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.HODApproved;

                //isMailSend = true;
                //actID = (int)EnumInwardOutward.EnumINOUTStatusForUpdate.LogisticsConfirm;
                //reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed;


            }

            else if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.LogisticsConfirm)
            {
                isMailSend = true;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed;
            }
            else if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Acknowledge)
            {
                isMailSend = true;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged;
            }
            else if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Close)
            {
                isMailSend = true;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.Closed;
            }

            if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Delete)
            {
                isMailSend = true;
                reqStatusID = (int)EnumInwardOutward.EnumINOUTStatus.Deleted;
            }

            if (actID == (int)EnumInwardOutward.EnumINOUTStatusForUpdate.Cancel)
            {
                isMailSend = true;
                reqStatusID = (int)TandTAllStatus.EnumTourStatus.Cancelled;
            }

          

            string value = objInwardAndOutward.UpdateOutwardStatusString(OutwardID
                                                            , reqStatusID
                                                            , actID
                                                            , remarks
                                                            , currentUserID
                                                            , lrNoD,
                                                               lrDateD,
                                                               lrInvoiceNo2C,
                                                               lrInvoiceDate2C,
                                                               lrTruckNoD,
                                                               podAttachment1,
                                                               podAttachment1Name,
                                                               fcrBrNoI,
                                                               fcrBrDateI,
                                                               fcrBrInvoiceNo,
                                                               fcrBrInvoiceDate,
                                                               fcrBrContainerNoI,
                                                               podAttachment2,
                                                               podAttachment1Name,
                                                               stringConfirmRemarks,
                                                               transporterName,
                                                               transporterEmail,
                                                               transporterContactNum
                                                               );// Convert.ToInt32(Session["EMP_RECORD_ID"]));


            if (!string.IsNullOrEmpty(value))
            {
                string InwOutConfirmationNo = string.Empty;

                InwOutConfirmationNo = Convert.ToString(value.Split(':')[1]);
                returnVal = _InsertValue + ":" + reqStatusID + ":" + InwOutConfirmationNo;
                if (isMailSend)
                {
                    TourSendMail tsm = new TourSendMail();
                    //int sendMailValue = tsm.SendMail(tourID);
                    string sendMailValue = IOSM.SendOutwardMail(OutwardID);

                    if (!string.IsNullOrEmpty(sendMailValue))
                    {

                        int val = objInwardAndOutward.UpdateOutwardInfoMailStatus(actID
                                                                           , OutwardID);

                        returnVal = _InsertAndSentMailValue + ":" + reqStatusID + ":" + InwOutConfirmationNo;
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