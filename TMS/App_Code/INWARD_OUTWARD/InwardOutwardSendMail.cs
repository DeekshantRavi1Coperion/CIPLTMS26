using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;



public class InwardOutwardSendMail
{

    public InwardOutwardSendMail()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();

    DataSet dsUserInfo = new DataSet();

    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;

    int inwardId = 0;
    string inwardNo = string.Empty;
    int inwardStatusId = 0;
    int isInternationalInward = 0;
    string jobNo = string.Empty;
    string vendorName = string.Empty;
    string vendorLocation = string.Empty;
    string vendorLocation2 = string.Empty;
    string vendorEmail = string.Empty;
    string delpickLocation = string.Empty;
    string internationalModeOfTransport = string.Empty;
    int isConStuffingPossible = 0;
    string incoterms = string.Empty;
    string deliveryTerm = string.Empty;
    int noOfTrucks = 0;
    int noOfContainers = 0;
    string pickupDate = string.Empty;
    string typeOfConsignment = string.Empty;
    int qtyTrucks14 = 0;
    int qtyTrucks17 = 0;
    int qtyTrucks19 = 0;
    int qtyTrucks22 = 0;
    int qtyTrucks24 = 0;
    int qtyTrucks32 = 0;
    int qtyTrucks40 = 0;
    int qtyLowBed = 0;
    int qtyODC = 0;
    int qtyContainers20 = 0;
    int qtyContainers40 = 0;
    int qtyContainers40HC = 0;
    int qtyOfFR = 0;
    int isDeleted = 0;
    int createdById = 0;
    string createdOn = string.Empty;
    int modifiedById = 0;
    string modifiedOn = string.Empty;
    int isApprovalMailSent = 0;
    int isApprovedMailSent = 0;
    int isDeletedMailSent = 0;
    int isCancelledMailSent = 0;
    int finalApprovedById = 0;
    string finalApprovedOn = string.Empty;
    string finalApprovedRemarks = string.Empty;
    string isFinalApprovedMailSent = string.Empty;


    string subject = string.Empty;
    string fileName = string.Empty;
    string tourNo = string.Empty;
    string sanctionNo = string.Empty;
    string tourStatus = string.Empty;
    int tourStatusID = 0;
    int travelModeID = 0;

    string createdBy = string.Empty;
    string createdByEmail = string.Empty;
    //string createdOn = string.Empty;
    string createdRemarks = string.Empty;


    string mgmtHodApprovedBy = string.Empty;
    string mgmtHodApprovedByEmail = string.Empty;
    string mgmtHodApprovedOn = string.Empty;
    string mgmtHodApprovedRemarks = string.Empty;

    string hodApprovedBy = string.Empty;
    string hodApprovedByEmail = string.Empty;
    string hodApprovedOn = string.Empty;
    string hodApprovedRemarks = string.Empty;

    //int createdById = 0;
    int deletedById = 0;
    string deletedBy = string.Empty;
    string deletedByEmail = string.Empty;
    string deletedOn = string.Empty;
    string deletedRemarks = string.Empty;

    int cancelledById = 0;
    string cancelledBy = string.Empty;
    string cancelledByEmail = string.Empty;
    string cancelledOn = string.Empty;
    string cancelledRemarks = string.Empty;

    string finalApprovedBy = string.Empty;
    string finalApprovedByEmail = string.Empty;
    //string finalApprovedOn = string.Empty;
    //string finalApprovedRemarks = string.Empty;

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
    string typeOfInward = string.Empty;
    string tlName = string.Empty;
    string tlEmail = string.Empty;

    int tlEmpRecordID = 0;
    string tlUserName = string.Empty;
    string tlPwd = string.Empty;

    string approveHref = string.Empty;
    string approveLink = string.Empty;

    string deleteHref = string.Empty;
    string deleteLink = string.Empty;

    string logisticsEmail = string.Empty;
    string finalConfirmationNumber = string.Empty;

    string transporterName = string.Empty;
    string transporterEmail = string.Empty;
    string transporterContactNumber = string.Empty;
    string vehiclePlacementDate = string.Empty;

    string domLRNo = string.Empty;
    string domLRDate = string.Empty;
    string domLRTruckNo = string.Empty;
    string intFcrBl = string.Empty;
    string intFcrBlDate = string.Empty;
    string intContainerNo = string.Empty;
    string acknowledgedByName = string.Empty;
    string acknowledgedByEmail = string.Empty;
    string acknowledgedOn = string.Empty;
    string acknowledgedRemarks = string.Empty;
    string closedByName = string.Empty;
    string closedByEmail = string.Empty;
    string closedOn = string.Empty;
    string closedRemarks = string.Empty;


    private string sendVal;
    int currentuser = 0;


    #endregion

    public string SendInwardMail(int inwardoutwardrequestID)
    {
        try
        {               
            dsUserInfo = GetInwardOutwardInfo(inwardoutwardrequestID);
            
            if (dsUserInfo.Tables.Count > 0 && dsUserInfo.Tables[0].Rows.Count > 0)
            {
                DataRow dr0 = dsUserInfo.Tables[0].Rows[0];

                inwardId = Convert.ToInt32(dr0["INWARD_ID"]);
                inwardNo = Convert.ToString(dr0["INWARD_NO"]);
                typeOfInward = Convert.ToString(dr0["TYPE_OF_INWARD"]);
                inwardStatusId = Convert.ToInt32(dr0["STATUS_ID"]);
                isInternationalInward = Convert.ToInt32(dr0["IS_INTERNATIONAL_INWARD"]);
                jobNo = Convert.ToString(dr0["JOB_NO"]);
                vendorName = Convert.ToString(dr0["VENDOR_NAME"]);
                vendorLocation = Convert.ToString(dr0["VENDOR_LOCATION"]);
                vendorLocation2 = Convert.ToString(dr0["VENDOR_LOCATION_2"]);
                vendorEmail = Convert.ToString(dr0["VENDOR_EMAIL"]);
                delpickLocation = Convert.ToString(dr0["DELIVERY_PICKUP_LOCATION"]);
                internationalModeOfTransport = Convert.ToString(dr0["INTERNATIONAL_MODE_OF_TRANSPORT"]);
                isConStuffingPossible = Convert.ToInt32(dr0["INTERNATIONAL_IS_CON_STUFFING_POSSIBLE"]);
                incoterms = Convert.ToString(dr0["INCOTERMS"]);
                deliveryTerm = Convert.ToString(dr0["DELIVERY_TERM"]);
                noOfTrucks = Convert.ToInt32(dr0["NO_OF_TRUCKS"]);
                noOfContainers = Convert.ToInt32(dr0["NO_OF_CONTAINERS"]);
                pickupDate = Convert.ToDateTime(dr0["DATE_OF_PICKUP"]).ToString("dd-MMM-yyyy");
                typeOfConsignment = Convert.ToString(dr0["TYPE_OF_CONSIGNMENT"]);
                qtyTrucks14 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_14'"]);
                qtyTrucks17 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_17'"]);
                qtyTrucks19 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_19'"]);
                qtyTrucks22 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_22'"]);
                qtyTrucks24 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_24'"]);
                qtyTrucks32 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_32'"]);
                qtyTrucks40 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_40'"]);
                qtyLowBed = Convert.ToInt32(dr0["QTY_OF_LOW_BED"]);
                qtyODC = Convert.ToInt32(dr0["QTY_OF_ODC"]);
                qtyContainers20 = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_20'"]);
                qtyContainers40 = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_40'"]);
                qtyContainers40HC = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_40'HC"]);
                qtyOfFR = Convert.ToInt32(dr0["QTY_OF_FR"]);
                isDeleted = Convert.ToInt32(dr0["IS_DELETED"]);
                createdBy = Convert.ToString(dr0["CREATED_BY_NAME"]);
                createdById = Convert.ToInt32(dr0["CREATED_BY"]);
                createdOn = Convert.ToDateTime(dr0["CREATED_ON"]).ToString("dd-MMM-yyyy");
                //modifiedById = Convert.ToInt32(dr0["MODIFIED_BY"]);
                hodApprovedBy = Convert.ToString(dr0["TEAMLEADER_NAME"]);
                //hodApprovedOn = Convert.ToDateTime(dr0["HOD_APPROVED_ON"]).ToString("dd-MMM-yyyy");
                hodApprovedOn = Convert.ToString(dr0["HOD_APPROVED_ON"]);
                modifiedOn = Convert.ToDateTime(dr0["CREATED_ON"]).ToString("dd-MMM-yyyy");
                isApprovalMailSent = Convert.ToInt32(dr0["IS_APPROVAL_MAIL_SENT"]);
                isApprovedMailSent = Convert.ToInt32(dr0["IS_APPROVED_MAIL_SENT"]);
                isDeletedMailSent = Convert.ToInt32(dr0["IS_DELETED_MAIL_SENT"]);
                isCancelledMailSent = Convert.ToInt32(dr0["IS_CANCELLED_MAIL_SENT"]);
                finalApprovedById = Convert.ToInt32(dr0["FINAL_APPROVED_BY"]);
                finalApprovarName = Convert.ToString(dr0["FINAL_APPROVED_BY_NAME"]);
                finalApprovarEmail = Convert.ToString(dr0["FINAL_APPROVED_BY_EMAIL"]);
                //finalApprovedOn = Convert.ToDateTime(dr0["FINAL_APPROVED_ON"]).ToString("dd-MMM-yyyy");

                finalApprovedOn = Convert.ToString(dr0["FINAL_APPROVED_ON"]);
                finalApprovedRemarks = Convert.ToString(dr0["FINAL_APPROVED_REMARKS"]);
                isFinalApprovedMailSent = Convert.ToString(dr0["IS_FINAL_APPROVED_MAIL_SENT"]);
                createdByEmail = Convert.ToString(dr0["CREATED_BY_EMAIL_ID"]);
                tlEmail = Convert.ToString(dr0["TEAMLEADER_EMAIL_ID"]);
                tlName = Convert.ToString(dr0["TEAMLEADER_NAME"]); 
                tlEmpRecordID = Convert.ToInt32(dr0["TEAMLEADER_ID"]); 
                tlUserName = Convert.ToString(dr0["TEAMLEADER_USERNAME"]); 
                tlPwd = Convert.ToString(dr0["TEAMLEADER_PASSWORD"]);
                deletedById = Convert.ToInt32(dr0["DELETED_BY"]);
                deletedBy = Convert.ToString(dr0["DELETED_BY_NAME"]);
                deletedByEmail = Convert.ToString(dr0["DELETED_BY_EMAIL"]);
                deletedOn = Convert.ToString(dr0["DELETED_ON"]);
                deletedRemarks = Convert.ToString(dr0["DELETED_REMARKS"]);
                cancelledById = Convert.ToInt32(dr0["CANCELLED_BY"]);
                cancelledBy = Convert.ToString(dr0["CANCELLED_BY_NAME"]);
                cancelledByEmail = Convert.ToString(dr0["CANCELLED_BY_EMAIL"]);
                cancelledOn = Convert.ToString(dr0["CANCELLED_ON"]);
                cancelledRemarks = Convert.ToString(dr0["CANCELLED_REMARKS"]);
                createdRemarks = Convert.ToString(dr0["CREATED_REMARKS"]);
                hodApprovedRemarks = Convert.ToString(dr0["APPROVED_REMARKS"]);

                domLRNo = Convert.ToString(dr0["LR_NO"]);
                domLRDate = Convert.ToString(dr0["LR_DATE"]);
                domLRTruckNo = Convert.ToString(dr0["TRUCK_NO"]);
                intFcrBl = Convert.ToString(dr0["FCR_BL"]);
                intFcrBlDate = Convert.ToString(dr0["FCR_BL_DATE"]);
                intContainerNo = Convert.ToString(dr0["CONTAINER_NO"]);

                finalConfirmationNumber = Convert.ToString(dr0["FINAL_CONFIRMATION_NUMBER"]);
                transporterName = Convert.ToString(dr0["TRANSPORTER_NAME"]);
                transporterEmail =  Convert.ToString(dr0["TRANSPORTER_EMAIL"]);
                transporterContactNumber = Convert.ToString(dr0["TRANSPORTER_CONTACT_NUMBER"]);
                vehiclePlacementDate = Convert.ToString(dr0["VEHICLE_PLACEMENT_DATE"]);
                acknowledgedByName = Convert.ToString(dr0["ACKNOWLEDGED_BY_NAME"]);
                acknowledgedByEmail = Convert.ToString(dr0["ACKNOWLEDGED_BY_EMAIL"]);
                acknowledgedOn = Convert.ToString(dr0["ACKNOWLEDGED_ON"]);
                acknowledgedRemarks = Convert.ToString(dr0["ACKNOWLEDGED_REMARKS"]);
                closedByName = Convert.ToString(dr0["CLOSED_BY_NAME"]);
                closedByEmail = Convert.ToString(dr0["CLOSED_BY_EMAIL"]);
                closedOn = Convert.ToString(dr0["CLOSED_ON"]);
                closedRemarks = Convert.ToString(dr0["CLOSED_REMARKS"]);

                if (dsUserInfo.Tables.Count > 0 && dsUserInfo.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in dsUserInfo.Tables[1].Rows)
                    {
                        if (row[2] != DBNull.Value)
                        {
                            logisticsEmail += row[2].ToString() + ",";
                        }
                    }
                    if (logisticsEmail.EndsWith(","))
                    {
                        logisticsEmail = logisticsEmail.TrimEnd(',');
                    }
                }


                string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);

                bool chkFinalApproval = false;

                if (inwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.New)
                {
                    from = createdByEmail;
                    to = tlEmail;
                    bcc = createdByEmail;
                    fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ReqToHODInwardDomestic.html";
                    subject = "Inward Req No.: '" + inwardNo + "' Created On: " + createdOn;

                    int actId = 2;

                    approveLink = "'" + urlTxt + "/PROJECT/UpdateInwardOutwardStatusNewOne.aspx?inwardid=" 
                        + inwardId + "&inwardno=" + inwardNo + "&actid=" + actId + "&inwardstatusid=" + inwardStatusId +
                        "&isInternationalInward=" + isInternationalInward +
                        "&ud=" + tlUserName + "&pd=" + tlPwd + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) + 
                        "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";

                    approveHref = "<a href=" + approveLink + ">Approve Inward request</a>";

                    deleteLink = "'" + urlTxt + "/PROJECT/UpdateInwardOutwardStatusNewOne.aspx?inwardid="
                        + inwardId + "&inwardno=" + inwardNo + "&actid= 3" + "&inwardstatusid=" + inwardStatusId +
                        "&isInternationalInward=" + isInternationalInward +
                        "&ud=" + tlUserName + "&pd=" + tlPwd + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) +
                        "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";

                    deleteHref = "<a href=" + deleteLink + ">Reject Inward request</a>";
                }
                else if (inwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.HODApproved)
                {
                    from = tlEmail;
                    to = logisticsEmail;
                    bcc = tlEmail;
                    cc = createdByEmail;
                    fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ReqToLogisticsDomestic.html";
                    subject = "Inward Req No.: '" + inwardNo + "' Approved On: " + hodApprovedOn;

                    int actId = 6;

                    approveLink = "'" + urlTxt + "/Login.aspx?inwardid="
                        + inwardId + "&inwardno=" + inwardNo + "&actid=" + actId + "&inwardstatusid=" + inwardStatusId +
                        "&isInternationalInward=" + isInternationalInward + "'";

                    //approveLink = "'" + urlTxt + "/PROJECT/UpdateInwardOutwardStatusNewOne.aspx?inwardid="
                    //    + inwardId + "&inwardno=" + inwardNo + "&actid=" + actId + "&inwardstatusid=" + inwardStatusId +
                    //    "&isInternationalInward=" + isInternationalInward + "'";

                    //approveLink = "'" + urlTxt + "/PROJECT/UpdateInwardOutwardStatusNewOne.aspx?inwardid="
                    //    + inwardId + "&inwardno=" + inwardNo + "&actid=" + actId + "&inwardstatusid=" + inwardStatusId +
                    //    "&isInternationalInward=" + isInternationalInward +
                    //    "&ud=" + tlUserName + "&pd=" + tlPwd + "&tlemprecordid=" + Convert.ToString(tlEmpRecordID) +
                    //    "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";

                    approveHref = "<a href=" + approveLink + ">Confirm & Submit Inward request</a>";

                    deleteLink = "'" + urlTxt + "/PROJECT/UpdateInwardOutwardStatusNewOne.aspx?inwardid="
                        + inwardId + "&inwardno=" + inwardNo + "&actid= 3" + "&inwardstatusid=" + inwardStatusId +
                        "&isInternationalInward=" + isInternationalInward + "'";

                    deleteHref = "<a href=" + deleteLink + ">Reject Inward request</a>";
                }
                else if (inwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                {
                    from = acknowledgedByEmail;
                    to = createdByEmail;
                    cc = tlEmail;
                    if (isInternationalInward == 1)
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/AcByLogisticsInternational.html";
                    }
                    else
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/AcByLogisticsDomestic.html";
                    }
                    
                    subject = "Inward Req No.: '" + inwardNo + "' Acknowledged On: " + acknowledgedOn;

                }
                else if (inwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.Closed)
                {
                    from = closedByEmail;
                    to = createdByEmail;
                    cc = tlEmail;
                    if (isInternationalInward == 1)
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ClosedByLogisticsInt.html";
                    }
                    else
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ClosedByLogisticsDomestic.html";
                    }
                    subject = "Inward Req No.: '" + inwardNo + "' Closed On: " + closedOn;

                }
                else if (inwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                {
                    from = finalApprovarEmail;
                    to = createdByEmail+";"+tlEmail;
                    bcc = finalApprovarEmail;


                    if (finalApprovarEmail == "senthil.kumar@coperion.com")
                    {
                        cc = "rajiv.kumar@coperion.com;Davkinandan.Sharma@coperion.com;pawan.kumar@coperion.com;jayanth.sagar.external@coperion.com";
                    }
                    else if (finalApprovarEmail == "rajiv.kumar@coperion.com")
                    {
                        cc = "senthil.kumar@coperion.com;Davkinandan.Sharma@coperion.com;pawan.kumar@coperion.com;jayanth.sagar.external@coperion.com";
                    }
                    else if (finalApprovarEmail == "Davkinandan.Sharma@coperion.com")
                    {
                        cc = "senthil.kumar@coperion.com;rajiv.kumar@coperion.com;pawan.kumar@coperion.com;jayanth.sagar.external@coperion.com";
                    }
                    else if (finalApprovarEmail == "jayanth.sagar.external@coperion.com")
                    {
                        cc = "senthil.kumar@coperion.com;rajiv.kumar@coperion.com;Davkinandan.Sharma@coperion.com;pawan.kumar@coperion.com";
                    }

                    if (isInternationalInward == 1)
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogistics.html";
                    }
                    else
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogisticsDomestic.html";
                    }
                    
                    subject = "Inward Req No.: '" + inwardNo + "' has been completed On: " + finalApprovedOn;


                   
                }
                else if (inwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                {
                   

                    if (deletedById == createdById)
                    {
                        from = deletedByEmail;
                        to = createdByEmail;
                        bcc = deletedByEmail;
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/DeleteByCreator.html";
                        subject = "Inward Req No.: '" + inwardNo + "' has been deleted On: " + deletedOn;
                    }
                    else
                    {
                        from = deletedByEmail;
                        to = createdByEmail;
                        bcc = deletedByEmail;

                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/DeleteByManager.html";
                        subject = "Inward Req No.: '" + inwardNo + "' has been deleted On: " + deletedOn;
                    }



                }
                else if (inwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.Cancelled)
                {


                    if (cancelledById == tlEmpRecordID)
                    {
                        from = cancelledByEmail;
                        to = createdByEmail;
                        bcc = cancelledByEmail;
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/DeleteByManager.html";
                        subject = "Inward Req No.: '" + inwardNo + "' has been cancelled On: " + cancelledOn;
                    }
                    else
                    {
                        from = cancelledByEmail;
                        to = createdByEmail;
                        bcc = cancelledByEmail;

                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/CancelledByLog.html";
                        subject = "Inward Req No.: '" + inwardNo + "' has been cancelled On: " + cancelledOn;
                    }



                }

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                mail.From = new MailAddress(from);

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

                body = body.Replace("{#inwardno#}", inwardNo);
                body = body.Replace("{#typeofinwardoutward#}", typeOfInward);

                body = body.Replace("{#createdby#}", createdBy);
                body = body.Replace("{#createdon#}", createdOn);
                body = body.Replace("{#requesterremarks#}", createdRemarks);

                
                body = body.Replace("{#jobnumber#}", jobNo);
                body = body.Replace("{#vendorname#}", vendorName);

                if(vendorLocation2 != "")
                {
                    body = body.Replace("{#vendorlocation#}", vendorLocation2);
                }
                else
                {
                    body = body.Replace("{#vendorlocation#}", vendorLocation);
                }
                
                body = body.Replace("{#deliverypickuplocation#}", delpickLocation);
                body = body.Replace("{#datepickup#}", pickupDate);
                body = body.Replace("{#approvedby#}", hodApprovedBy);
                body = body.Replace("{#approvedon#}", hodApprovedOn);
                body = body.Replace("{#approvedremarks#}", hodApprovedRemarks);
                body = body.Replace("{#logisticsapprovedby#}", finalApprovarName);
                body = body.Replace("{#logisticsapprovedon#}", finalApprovedOn);
                body = body.Replace("{#logisticsapprovedremarks#}", finalApprovedRemarks);
                body = body.Replace("{#deletedby#}", deletedBy);
                body = body.Replace("{#deletedon#}", deletedOn);
                body = body.Replace("{#deletedremark#}", deletedRemarks);
                body = body.Replace("{#cancelledby#}", cancelledBy);
                body = body.Replace("{#cancelledon#}", cancelledOn);
                body = body.Replace("{#cancelledremark#}", cancelledRemarks);
                body = body.Replace("{#approvelink#}", approveHref);
                body = body.Replace("{#deletelink#}", deleteHref);
                body = body.Replace("{#finalconfirmationnumber#}", finalConfirmationNumber);

                body = body.Replace("{#fcrbl#}", intFcrBl);
                body = body.Replace("{#fcrbldate#}", intFcrBlDate);
                body = body.Replace("{#lrno#}", domLRNo);
                body = body.Replace("{#lrdate#}", domLRDate);
                body = body.Replace("{#transportername#}", transporterName);
                body = body.Replace("{#transporteremail#}", transporterEmail);
                body = body.Replace("{#transportercontact#}", transporterContactNumber);
                body = body.Replace("{#vehicleplacementdate#}", vehiclePlacementDate);

                body = body.Replace("{#acknowledgedby#}", acknowledgedByName);
                body = body.Replace("{#acknowledgedremarks#}", acknowledgedRemarks);
                body = body.Replace("{#acknowledgedon#}", acknowledgedOn);
                body = body.Replace("{#closedby#}", closedByName);
                body = body.Replace("{#closedon#}", closedOn);
                body = body.Replace("{#closedremarks#}", closedRemarks);

                mail.Body = body;

                SmtpServer.Host = "eusmtp.hi.corp";
                SmtpServer.Port = 25;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;


                try
                {
                    SmtpServer.Send(mail);
                    //sendVal = 1;
                    sendVal = inwardNo;
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
           
            }
        catch (Exception)
        {
            sendVal = "";
        }
        return sendVal;
    }

    public string SendOutwardMail(int outwardrequestID)
    {
        try
        {
            dsUserInfo = GetOutwardInfo(outwardrequestID);
            int outwardId = 0;
            string outwardNo = string.Empty;
            string typeOfOutward = string.Empty;
            int outwardStatusId = 0;
            int isInternationalOutward = 0;
            string vendorNamePickup = string.Empty;
            string vendorLocationPickup = string.Empty;
            string vendorEmailPickup = string.Empty;
            string finalConfirmationNumber = string.Empty;
            string hodApprovedRemarks = string.Empty;

            string respersonA35a = string.Empty;
            string respersonA35b = string.Empty;
            string respersonGNUa = string.Empty;

            if (dsUserInfo.Tables.Count > 0 && dsUserInfo.Tables[0].Rows.Count > 0)
            {
                DataRow dr0 = dsUserInfo.Tables[0].Rows[0];

                outwardId = Convert.ToInt32(dr0["OUTWARD_ID"]);
                outwardNo = Convert.ToString(dr0["OUTWARD_NO"]);
                typeOfOutward = Convert.ToString(dr0["TYPE_OF_OUTWARD"]);
                outwardStatusId = Convert.ToInt32(dr0["STATUS_ID"]);
                isInternationalOutward = Convert.ToInt32(dr0["IS_INTERNATIONAL_OUTWARD"]);
                finalConfirmationNumber = Convert.ToString(dr0["FINAL_CONFIRMATION_NUMBER"]);
                jobNo = Convert.ToString(dr0["JOB_NO"]);
                vendorName = Convert.ToString(dr0["CLIENT_NAME"]);
                vendorLocation = Convert.ToString(dr0["CLIENT_LOCATION"]);
                vendorLocation2 = Convert.ToString(dr0["CLIENT_LOCATION_2"]);
                vendorEmail = Convert.ToString(dr0["CLIENT_EMAIL"]);
                delpickLocation = Convert.ToString(dr0["DELIVERY_PICKUP_LOCATION"]);
                internationalModeOfTransport = Convert.ToString(dr0["INTERNATIONAL_MODE_OF_TRANSPORT"]);
                isConStuffingPossible = Convert.ToInt32(dr0["INTERNATIONAL_IS_CON_STUFFING_POSSIBLE"]);
                incoterms = Convert.ToString(dr0["INCOTERMS"]);
                deliveryTerm = Convert.ToString(dr0["DELIVERY_TERM"]);
                noOfTrucks = Convert.ToInt32(dr0["NO_OF_TRUCKS"]);
                noOfContainers = Convert.ToInt32(dr0["NO_OF_CONTAINERS"]);
                pickupDate = Convert.ToDateTime(dr0["DATE_OF_PICKUP"]).ToString("dd-MMM-yyyy");
                typeOfConsignment = Convert.ToString(dr0["TYPE_OF_CONSIGNMENT"]);
               
                vendorNamePickup = Convert.ToString(dr0["VENDOR_NAME_PICKUP"]);
                vendorLocationPickup = Convert.ToString(dr0["VENDOR_LOCATION_PICKUP"]);
                vendorEmailPickup = Convert.ToString(dr0["VENDOR_EMAIL_PICKUP"]);
                //qtyTrucks14 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_14'"]);
                //qtyTrucks17 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_17'"]);
                //qtyTrucks19 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_19'"]);
                //qtyTrucks22 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_22'"]);
                //qtyTrucks24 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_24'"]);
                //qtyTrucks32 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_32'"]);
                //qtyTrucks40 = Convert.ToInt32(dr0["QTY_OF_TRUCKS_40'"]);
                //qtyLowBed = Convert.ToInt32(dr0["QTY_OF_LOW_BED"]);
                //qtyODC = Convert.ToInt32(dr0["QTY_OF_ODC"]);
                //qtyContainers20 = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_20'"]);
                //qtyContainers40 = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_40'"]);
                //qtyContainers40HC = Convert.ToInt32(dr0["QTY_OF_CONTAINERS_40'HC"]);
                //qtyOfFR = Convert.ToInt32(dr0["QTY_OF_FR"]);
                isDeleted = Convert.ToInt32(dr0["IS_DELETED"]);
                createdBy = Convert.ToString(dr0["CREATED_BY_NAME"]);
                createdById = Convert.ToInt32(dr0["CREATED_BY"]);
                createdOn = Convert.ToDateTime(dr0["CREATED_ON"]).ToString("dd-MMM-yyyy");
                //modifiedById = Convert.ToInt32(dr0["MODIFIED_BY"]);
                hodApprovedBy = Convert.ToString(dr0["TEAMLEADER_NAME"]);
                //hodApprovedOn = Convert.ToDateTime(dr0["HOD_APPROVED_ON"]).ToString("dd-MMM-yyyy");
                hodApprovedOn = Convert.ToString(dr0["HOD_APPROVED_ON"]);
                modifiedOn = Convert.ToDateTime(dr0["CREATED_ON"]).ToString("dd-MMM-yyyy");
                isApprovalMailSent = Convert.ToInt32(dr0["IS_APPROVAL_MAIL_SENT"]);
                isApprovedMailSent = Convert.ToInt32(dr0["IS_APPROVED_MAIL_SENT"]);
                isDeletedMailSent = Convert.ToInt32(dr0["IS_DELETED_MAIL_SENT"]);
                isCancelledMailSent = Convert.ToInt32(dr0["IS_CANCELLED_MAIL_SENT"]);
                finalApprovedById = Convert.ToInt32(dr0["FINAL_APPROVED_BY"]);
                finalApprovarName = Convert.ToString(dr0["FINAL_APPROVED_BY_NAME"]);
                finalApprovarEmail = Convert.ToString(dr0["FINAL_APPROVED_BY_EMAIL"]);
                //finalApprovedOn = Convert.ToDateTime(dr0["FINAL_APPROVED_ON"]).ToString("dd-MMM-yyyy");
                finalApprovedOn = Convert.ToString(dr0["FINAL_APPROVED_ON"]);
                finalApprovedRemarks = Convert.ToString(dr0["FINAL_APPROVED_REMARKS"]);
                isFinalApprovedMailSent = Convert.ToString(dr0["IS_FINAL_APPROVED_MAIL_SENT"]);
                createdByEmail = Convert.ToString(dr0["CREATED_BY_EMAIL_ID"]);
                tlEmail = Convert.ToString(dr0["TEAMLEADER_EMAIL_ID"]);
                tlName = Convert.ToString(dr0["TEAMLEADER_NAME"]);
                tlEmpRecordID = Convert.ToInt32(dr0["TEAMLEADER_ID"]);
                tlUserName = Convert.ToString(dr0["TEAMLEADER_USERNAME"]);
                tlPwd = Convert.ToString(dr0["TEAMLEADER_PASSWORD"]);
                deletedById = Convert.ToInt32(dr0["DELETED_BY"]);
                deletedBy = Convert.ToString(dr0["DELETED_BY_NAME"]);
                deletedByEmail = Convert.ToString(dr0["DELETED_BY_EMAIL"]);
                deletedOn = Convert.ToString(dr0["DELETED_ON"]);
                deletedRemarks = Convert.ToString(dr0["DELETED_REMARKS"]);
                cancelledById = Convert.ToInt32(dr0["CANCELLED_BY"]);
                cancelledBy = Convert.ToString(dr0["CANCELLED_BY_NAME"]);
                cancelledByEmail = Convert.ToString(dr0["CANCELLED_BY_EMAIL"]);
                cancelledOn = Convert.ToString(dr0["CANCELLED_ON"]);
                cancelledRemarks = Convert.ToString(dr0["CANCELLED_REMARKS"]);
                createdRemarks = Convert.ToString(dr0["CREATED_REMARKS"]);
                hodApprovedRemarks = Convert.ToString(dr0["APPROVED_REMARKS"]);
                transporterName = Convert.ToString(dr0["TRANSPORTER_NAME"]);
                transporterEmail = Convert.ToString(dr0["TRANSPORTER_EMAIL"]);
                transporterContactNumber = Convert.ToString(dr0["TRANSPORTER_CONTACT_NUMBER"]);
                vehiclePlacementDate = Convert.ToString(dr0["VEHICLE_PLACEMENT_DATE"]);
                acknowledgedByName = Convert.ToString(dr0["ACKNOWLEDGED_BY_NAME"]);
                acknowledgedByEmail = Convert.ToString(dr0["ACKNOWLEDGED_BY_EMAIL"]);
                acknowledgedOn = Convert.ToString(dr0["ACKNOWLEDGED_ON"]);
                acknowledgedRemarks = Convert.ToString(dr0["ACKNOWLEDGED_REMARKS"]);
                closedByName = Convert.ToString(dr0["CLOSED_BY_NAME"]);
                closedByEmail = Convert.ToString(dr0["CLOSED_BY_EMAIL"]);
                closedOn = Convert.ToString(dr0["CLOSED_ON"]);
                closedRemarks = Convert.ToString(dr0["CLOSED_REMARKS"]);
                domLRNo = Convert.ToString(dr0["LR_NO"]);
                domLRDate = Convert.ToString(dr0["LR_DATE"]);
                domLRTruckNo = Convert.ToString(dr0["TRUCK_NO"]);
                intFcrBl = Convert.ToString(dr0["FCR_BL"]);
                intFcrBlDate = Convert.ToString(dr0["FCR_BL_DATE"]);
                intContainerNo = Convert.ToString(dr0["CONTAINER_NO"]);

                if (dsUserInfo.Tables.Count > 0 && dsUserInfo.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in dsUserInfo.Tables[1].Rows)
                    {
                        if (row[2] != DBNull.Value)
                        {
                            logisticsEmail += row[2].ToString() + ",";
                        }
                    }
                    if (logisticsEmail.EndsWith(","))
                    {
                        logisticsEmail = logisticsEmail.TrimEnd(',');
                    }
                }

                if (dsUserInfo.Tables.Count > 0 && dsUserInfo.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in dsUserInfo.Tables[2].Rows)
                    {
                        if (row[1] != DBNull.Value)
                        {
                            respersonA35a += row[1].ToString() + ",";
                        }
                        if (respersonA35a.EndsWith(","))
                        {
                            respersonA35a = respersonA35a.TrimEnd(',');
                        }
                    }
                    
                }
                if (dsUserInfo.Tables.Count > 0 && dsUserInfo.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow row in dsUserInfo.Tables[3].Rows)
                    {
                        if (row[1] != DBNull.Value)
                        {
                            respersonA35b += row[1].ToString() + ",";
                        }
                        if (respersonA35b.EndsWith(","))
                        {
                            respersonA35b = respersonA35b.TrimEnd(',');
                        }
                    }

                }
                if (dsUserInfo.Tables.Count > 0 && dsUserInfo.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow row in dsUserInfo.Tables[4].Rows)
                    {
                        if (row[1] != DBNull.Value)
                        {
                            respersonGNUa += row[1].ToString() + ",";
                        }
                        if (respersonGNUa.EndsWith(","))
                        {
                            respersonGNUa = respersonGNUa.TrimEnd(',');
                        }
                    }

                }
                string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);

                bool chkFinalApproval = false;

                if (outwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.New)
                {
                    from = createdByEmail;
                    to = logisticsEmail;
                    //bcc = tlEmail;
                    bcc = createdByEmail;
                    cc = tlEmail;
                    fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ReqToLogisticsDomesticOutward.html";
                    subject = "Outward Req No.: '" + outwardNo + "' Created On: " + createdOn;

                }
                else if (outwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.Acknowledged)
                {
                    from = acknowledgedByEmail;
                    to = createdByEmail;
                    cc = tlEmail;
                    if (isInternationalInward == 1)
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/AcByLogisticsInternationalOutward.html";
                    }
                    else
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/AcByLogisticsDomesticOutward.html";
                    }

                    subject = "Outward Req No.: '" + outwardNo + "' Acknowledged On: " + acknowledgedOn;

                }
                else if (outwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.Closed)
                {
                    from = closedByEmail;
                    to = createdByEmail;
                    cc = tlEmail;
                    if (isInternationalOutward == 1)
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ClosedByLogisticsIntOutward.html";
                    }
                    else
                    {
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ClosedByLogisticsDomesticOutward.html";
                    }
                    subject = "Outward Req No.: '" + outwardNo + "' Closed On: " + closedOn;

                }
                else if (outwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.LogisticsConfirmed)
                {
                    from = finalApprovarEmail;
                    to = createdByEmail + ";" + tlEmail;
                    bcc = finalApprovarEmail;


                    if (finalApprovarEmail == "senthil.kumar@coperion.com")
                    {
                        cc = "rajiv.kumar@coperion.com;Davkinandan.Sharma@coperion.com;pawan.kumar@coperion.com;jayanth.sagar.external@coperion.com";
                    }
                    else if (finalApprovarEmail == "rajiv.kumar@coperion.com")
                    {
                        cc = "senthil.kumar@coperion.com;Davkinandan.Sharma@coperion.com;pawan.kumar@coperion.com;jayanth.sagar.external@coperion.com";
                    }
                    else if (finalApprovarEmail == "Davkinandan.Sharma@coperion.com")
                    {
                        cc = "senthil.kumar@coperion.com;rajiv.kumar@coperion.com;pawan.kumar@coperion.com;jayanth.sagar.external@coperion.com";
                    }
                    else if (finalApprovarEmail == "jayanth.sagar.external@coperion.com")
                    {
                        cc = "senthil.kumar@coperion.com;rajiv.kumar@coperion.com;Davkinandan.Sharma@coperion.com;pawan.kumar@coperion.com";
                    }

                    //fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogisticsOutward.html";
                    subject = "Outward Req No.: '" + outwardNo + "' has been confirmed On: " + finalApprovedOn;

                    if (isInternationalInward == 1)
                    {
                        if (delpickLocation == "GNU")
                        {
                            cc += ";rambeer.saini@coperion.com";
                            fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogisticsOutwardGNU.html";
                        }
                        else if (delpickLocation == "A35")
                        {
                            cc += ";rajan.chopra@coperion.com;akhileshkumar.yadav@coperion.com";
                            fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogisticsOutwardA35.html";
                        }
                        else
                        {
                            fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogisticsOutward.html";
                        }
                        
                    }
                    else
                    {
                        if (delpickLocation == "GNU")
                        {
                            cc += ";rambeer.saini@coperion.com";
                            fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogisticsDomesticOutwardGNU.html";
                        }
                        else if (delpickLocation == "A35")
                        {
                            cc += ";rajan.chopra@coperion.com;akhileshkumar.yadav@coperion.com";
                            fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogisticsDomesticOutwardA35.html";
                        }
                        else
                        {
                            fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/ConfirmationByLogisticsDomesticOutward.html";
                        }
                        
                    }

                }
                else if (outwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.Deleted)
                {


                    if (deletedById == createdById)
                    {
                        from = deletedByEmail;
                        to = createdByEmail;
                        bcc = deletedByEmail;
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/DeleteByCreatorOutward.html";
                        subject = "Outward Req No.: '" + outwardNo + "' has been deleted On: " + deletedOn;
                    }
                    else
                    {
                        from = deletedByEmail;
                        to = createdByEmail;
                        bcc = deletedByEmail;

                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/DeleteByManagerOutward.html";
                        subject = "Outward Req No.: '" + outwardNo + "' has been deleted On: " + deletedOn;
                    }



                }
                else if (outwardStatusId == (int)EnumInwardOutward.EnumINOUTStatus.Cancelled)
                {


                    if (cancelledById == tlEmpRecordID)
                    {
                        from = cancelledByEmail;
                        to = createdByEmail;
                        bcc = cancelledByEmail;
                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/DeleteByManagerOutward.html";
                        subject = "Outward Req No.: '" + outwardNo + "' has been cancelled On: " + cancelledOn;
                    }
                    else
                    {
                        from = cancelledByEmail;
                        to = createdByEmail;
                        bcc = cancelledByEmail;

                        fileName = "~/INWARD_OUTWARD/IN_OUT_EMAIL_FORMATS/CancelledByLogOutward.html";
                        subject = "Outward Req No.: '" + outwardNo + "' has been cancelled On: " + cancelledOn;
                    }



                }

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                mail.From = new MailAddress(from);

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
                    string[] strBCC = bcc.Split(';');
                    foreach (var item in strBCC)
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

                body = body.Replace("{#outwardno#}", outwardNo);
                body = body.Replace("{#typeofoutward#}", typeOfOutward);
                body = body.Replace("{#typeofinwardoutward#}", typeOfOutward);
              
                body = body.Replace("{#createdby#}", createdBy);
                body = body.Replace("{#createdon#}", createdOn);
                body = body.Replace("{#requesterremarks#}", createdRemarks);


                body = body.Replace("{#jobnumber#}", jobNo);
                body = body.Replace("{#vendorname#}", vendorName);
                //body = body.Replace("{#vendorlocation#}", vendorLocation);
                if (vendorLocation2 != "")
                {
                    body = body.Replace("{#vendorlocation#}", vendorLocation2);
                }
                else
                {
                    body = body.Replace("{#vendorlocation#}", vendorLocation);
                }
                body = body.Replace("{#deliverypickuplocation#}", delpickLocation);
                body = body.Replace("{#datepickup#}", pickupDate);


                body = body.Replace("{#approvedby#}", hodApprovedBy);
                body = body.Replace("{#approvedon#}", hodApprovedOn);
                body = body.Replace("{#approvedremarks#}", hodApprovedRemarks);


                body = body.Replace("{#logisticsapprovedby#}", finalApprovarName);
                body = body.Replace("{#logisticsapprovedon#}", finalApprovedOn);
                body = body.Replace("{#logisticsapprovedremarks#}", finalApprovedRemarks);
                

                body = body.Replace("{#deletedby#}", deletedBy);
                body = body.Replace("{#deletedon#}", deletedOn);
                body = body.Replace("{#deletedremark#}", deletedRemarks);

                body = body.Replace("{#cancelledby#}", cancelledBy);
                body = body.Replace("{#cancelledon#}", cancelledOn);
                body = body.Replace("{#cancelledremark#}", cancelledRemarks);

                body = body.Replace("{#approvelink#}", approveHref);
                body = body.Replace("{#deletelink#}", deleteHref);
                body = body.Replace("{#finalconfirmationnumber#}", finalConfirmationNumber);
                body = body.Replace("{#fcrbl#}", intFcrBl);
                body = body.Replace("{#fcrbldate#}", intFcrBlDate);
                body = body.Replace("{#intContainerNo#}", intContainerNo);
                body = body.Replace("{#lrno#}", domLRNo);
                body = body.Replace("{#lrdate#}", domLRDate);
                body = body.Replace("{#domLRTruckNo#}", domLRTruckNo);
                body = body.Replace("{#transportername#}", transporterName);
                body = body.Replace("{#transporteremail#}", transporterEmail);
                body = body.Replace("{#transportercontact#}", transporterContactNumber);
                body = body.Replace("{#vehicleplacementdate#}", vehiclePlacementDate);

                body = body.Replace("{#acknowledgedby#}", acknowledgedByName);
                body = body.Replace("{#acknowledgedremarks#}", acknowledgedRemarks);
                body = body.Replace("{#acknowledgedon#}", acknowledgedOn);
                body = body.Replace("{#closedby#}", closedByName);
                body = body.Replace("{#closedon#}", closedOn);
                body = body.Replace("{#closedremarks#}", closedRemarks);
                body = body.Replace("{#respersonA35a#}", respersonA35a);
                body = body.Replace("{#respersonA35b#}", respersonA35b);
                body = body.Replace("{#respersonGNUa#}", respersonGNUa);


                mail.Body = body;

                SmtpServer.Host = "eusmtp.hi.corp";
                SmtpServer.Port = 25;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;


                try
                {
                    SmtpServer.Send(mail);
                    //sendVal = 1;
                    sendVal = outwardNo;
                }
                catch (Exception ex)
                {
                    string exMsg = ex.ToString();
                    if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                        sendVal = outwardNo;

                    else
                        sendVal = "";
                }



            }

        }
        catch (Exception)
        {
            sendVal = "";
        }
        return sendVal;
    }

    public DataSet GetInwardOutwardInfo( int inwardOutwardReqId)
    {

        DataSet ds = new DataSet();
        string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        SqlCommand cmd = new SqlCommand();
       // cmd.CommandText = "sp_get_req_details_for_inward_mail";
        cmd.CommandText = "sp_get_req_details_for_inward_mail_01";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        cmd.Parameters.AddWithValue("@requestid", inwardOutwardReqId);

        da.Fill(ds);
        if (ds != null)
            return ds;

        return null;

    }

    public DataSet GetOutwardInfo(int OutwardReqId)
    {

        DataSet ds = new DataSet();
        string cipltmsconnectionstring = ConnectionString.GetCiplTMSCommonConn();
        SqlConnection con = new SqlConnection(cipltmsconnectionstring);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "sp_get_req_details_for_outward_mail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        cmd.Parameters.AddWithValue("@requestid", OutwardReqId);

        da.Fill(ds);
        if (ds != null)
            return ds;

        return null;

    }


}