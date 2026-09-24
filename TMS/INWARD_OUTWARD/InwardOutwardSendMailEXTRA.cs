using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InwardOutwardSendMail
/// </summary>
public class InwardOutwardSendMail
{
    DataSet dsReqInfo = new DataSet();
    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();

    string requestNo = String.Empty;
    int  isInternationalInward = 0;
    string jobNo = String.Empty;
    string vendorName = String.Empty;
    string vendorLocation = String.Empty;
    string vendorEmail = String.Empty;
    string vendorContact = String.Empty;

    string deliveryPickupLocation = String.Empty;
    string internationalModeOfTransport = String.Empty;
    string internationalIsConStuffingPossible = String.Empty;
    string incoterms = String.Empty;
    string deliveryTerm = String.Empty;
    string noOfTrucks = String.Empty;
    string noOfContainers = String.Empty;
    string dateOfPickup = String.Empty;
    string typeOfConsignment = String.Empty;
    //packingListDoc = Convert.ToString(dr0["PACKING_LIST_DOC"]);
    //subVendorInvoice = Convert.ToString(dr0["SUB_VENDOR_INVOICE"]);
    int qtyTruck14 = 0;
    int qtyTruck17 = 0;
    int qtyTruck19 = 0;
    int qtyTruck22 = 0;
    int qtyTruck24 = 0;
    int qtyTruck32 = 0;
    int qtyTruck40 =0;
    int qtyLowBed = 0;
    int qtyODC = 0;
    int qtyCon20 = 0;
    int qtyCon40 = 0;
    int qtyCont40HC = 0;
    int qtyFR = 0;
    int isDeleted = 0;
    int createdBy = 0;
    string createdOn = String.Empty;
    int modifiedBy = 0;
    string modifiedOn = String.Empty;
    int teamleaderId = 0;
    string teamleaderName = String.Empty;
    int reqStatusId = 0;

    #endregion


    public InwardOutwardSendMail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string SendInwardOutwardMail(int requestID)
    {
        //int sendVal = 0;

        string sendVal = "";

        try
        {
            dsReqInfo = GetRequesterInfo(requestID);
            if (dsReqInfo.Tables.Count > 0 && dsReqInfo.Tables[0].Rows.Count > 0)
            {
                DataRow dr0 = dsReqInfo.Tables[0].Rows[0];

                requestNo = Convert.ToString(dr0["REQ_ID"]);
                isInternationalInward = Convert.ToInt32(dr0["IS_INTERNATIONAL_INWARD"]);
                jobNo = Convert.ToString(dr0["JOB_NO"]);
                vendorName = Convert.ToString(dr0["VENDOR_NAME"]);
                vendorLocation = Convert.ToString(dr0["VENDOR_LOCATION"]);
                vendorEmail = Convert.ToString(dr0["VENDOR_EMAIL"]);
                vendorContact = Convert.ToString(dr0["VENDOR_CONTACT"]);

                deliveryPickupLocation = Convert.ToString(dr0["DELIVERY_PICKUP_LOCATION"]);
                internationalModeOfTransport = Convert.ToString(dr0["INTERNATIONAL_MODE_OF_TRANSPORT"]);
                internationalIsConStuffingPossible = Convert.ToString(dr0["INTERNATIONAL_IS_CON_STUFFING_POSSIBLE"]);
                incoterms = Convert.ToString(dr0["INCOTERMS"]);
                deliveryTerm = Convert.ToString(dr0["DELIVERY_TERM"]);
                noOfTrucks = Convert.ToString(dr0["NO_OF_TRUCKS"]);
                noOfContainers = Convert.ToString(dr0["NO_OF_CONTAINERS"]);
                dateOfPickup = Convert.ToString(dr0["DATE_OF_PICKUP"]);
                typeOfConsignment = Convert.ToString(dr0["TYPE_OF_CONSIGNMENT"]);
                //packingListDoc = Convert.ToString(dr0["PACKING_LIST_DOC"]);
                //subVendorInvoice = Convert.ToString(dr0["SUB_VENDOR_INVOICE"]);
                qtyTruck14 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_14'"]);
                qtyTruck17 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_17"]);
                qtyTruck19 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_19"]);
                qtyTruck22 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_22"]);
                qtyTruck24 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_24"]);
                qtyTruck32 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_32"]);
                qtyTruck40 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_40"]);
                qtyLowBed = Convert.ToInt32(dr0["QTY_OF_LOW_BED"]);
                qtyODC = Convert.ToInt32(dr0["QTY_OF_ODC"]);
                qtyCon20 = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_20'"]);
                qtyCon40 = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_40'"]);
                qtyCont40HC = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_40'HC"]);
                qtyFR = Convert.ToInt32(dr0["QTY_OF_FR"]);
                isDeleted = Convert.ToInt32(dr0["IS_DELETED"]);
                createdBy = Convert.ToInt32(dr0["CREATED_BY"]);
                createdOn = Convert.ToString(dr0["CREATED_ON"]);
                modifiedBy = Convert.ToInt32(dr0["MODIFIED_BY"]);
                modifiedOn = Convert.ToString(dr0["MODIFIED_ON"]);
                teamleaderId = Convert.ToInt32(dr0["TEAMLEADER_ID"]);
                teamleaderName = Convert.ToString(dr0["TEAMLEADER_NAME"]);
                reqStatusId = 0;


            }
            else
            {
                sendVal = "";
            }


            //string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);
            string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);

            //New
            if (reqStatusId == (int)TandTAllStatus.EnumTourStatus.New)
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






                                    }
            }


           

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
                    cc = hrTeamEmail;
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
    public DataSet GetRequesterInfo(int requestID)
    {
        DataSet ds = new DataSet();
        string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "sp_get_req_details_for_inward_mail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        cmd.Parameters.AddWithValue("@requestid", requestID);

        da.Fill(ds);
        if (ds != null)
            return ds;

        return null;
    }
}


