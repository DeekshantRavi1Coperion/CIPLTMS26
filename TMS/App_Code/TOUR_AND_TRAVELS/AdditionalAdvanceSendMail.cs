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
/// Summary description for AdditionalAdvanceSendMail
/// </summary>
public class AdditionalAdvanceSendMail
{
    public AdditionalAdvanceSendMail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsTravelInfo = new DataSet();

    //int empSess = Convert.ToInt32(Session["EMP_RECORD_ID"]);

    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;

    string tourNo = string.Empty;
    string sanctionNo = string.Empty;
    string custVendName = string.Empty;
    double advanceAmt = 0;
    string advanceAmtCurrency = string.Empty;

    double reqAddAdvanceAmt = 0;
    string placeOfvisit = string.Empty;
    string reqAddAdvanceAmtCurrency = string.Empty;
    int empId = 0;
    string empName = string.Empty;
    string empEmail = string.Empty;
    int teamleaderId = 0;
    string teamleaderName = string.Empty;
    string teamleaderEmail = string.Empty;
    string startDate = string.Empty;
    string endDate = string.Empty;

    string requestedOn = string.Empty;

    string hodApprovedBy = string.Empty;
    string hodApprovedOn = string.Empty;

    string remarks = string.Empty;

    string cancelledRemarks = string.Empty;
    string deletedRemarks = string.Empty;
    string HodApprovedByRemarks = string.Empty;

    int requestStatusId = 0;
    int reqAddAdvanceAmtCurrencyID = 0;
    string requestStatusName = string.Empty;

    string accTeamName = string.Empty;

    string accTeamEmail = string.Empty;

    string accAcvnaceFCEmail = string.Empty;


    string subject = string.Empty;
    string fileName = string.Empty;
    string approveHref = string.Empty;
    string approveLink = string.Empty;

    string deleteHref = string.Empty;
    string deleteLink = string.Empty;


    int deletedByID  = 0;
    string deletedBy = string.Empty;
    string deletedByEmail = string.Empty;
    string deletedOn = string.Empty;
   

    string cancelledBy = string.Empty;
    string cancelledByEmail = string.Empty;
    string cancelledOn = string.Empty;
    

    string teamleaderUserName = string.Empty;

    string teamleaderPwd = string.Empty;
    string hodApprovedRemarks = string.Empty;


    int actId = 0;
    int isAdviceGenerated = 0;
    string createdByAccNo = string.Empty;
    string createdByIFSC = string.Empty;
    int unitId = 0;

    string reqNo = string.Empty;

    #endregion


    public string SendMail(int RequestID)
    {
        string sendVal = "";

        try
        {
            dsTravelInfo = objTourAndTravels.GetTravellerInfo(RequestID);
            if (dsTravelInfo.Tables.Count > 0 && dsTravelInfo.Tables[0].Rows.Count > 0)
            {
                DataRow dr0 = dsTravelInfo.Tables[0].Rows[0];

                reqNo = Convert.ToString(dr0["REQ_NO"]);
                tourNo = Convert.ToString(dr0["TOUR_NO"]);
                sanctionNo = Convert.ToString(dr0["TOUR_SANCTION_NO"]);
                deletedByID = Convert.ToInt32(dr0["DELETED_BY"]);
                empId = Convert.ToInt32(dr0["EMP_RECORD_ID"]);
                empName = Convert.ToString(dr0["EMPLOYEE_NAME"]);
                empEmail = Convert.ToString(dr0["EMPLOYEE_EMAIL"]);
                teamleaderId = Convert.ToInt32(dr0["TEAMLEADER_ID"]);
                teamleaderName = Convert.ToString(dr0["TEAMLEADER_NAME"]);
                teamleaderEmail = Convert.ToString(dr0["TEAMLEADER_EMAIL_ID"]);
                teamleaderUserName = Convert.ToString(dr0["TEAMLEADER_USERNAME"]);
                teamleaderPwd = Convert.ToString(dr0["TEAMLEADER_PASSWORD"]);
                
                startDate = Convert.ToString(dr0["START_DATE"]);
                endDate = Convert.ToString(dr0["END_DATE"]);
                advanceAmtCurrency = Convert.ToString(dr0["ADVANCE_CURRENCY"]);
                advanceAmt = Convert.ToDouble(dr0["ADVANCE_TAKEN"]);
                reqAddAdvanceAmt = Convert.ToDouble(dr0["ADDITIONAL_REQUESTED_AMT"]);
                reqAddAdvanceAmtCurrency = Convert.ToString(dr0["ADDITIONAL_REQUESTED_AMT_CURRENCY"]);
                reqAddAdvanceAmtCurrencyID = Convert.ToInt32(dr0["ADDITIONAL_REQUESTED_AMT_CURRENCY_ID"]);
                remarks = Convert.ToString(dr0["REMARKS"]);

                HodApprovedByRemarks = Convert.ToString(dr0["HOD_APPROVED_REMARKS"]);
                cancelledRemarks = Convert.ToString(dr0["CANCELLED_REMARKS"]);
                deletedRemarks = Convert.ToString(dr0["DELETED_REMARKS"]); 

                 requestStatusId = Convert.ToInt32(dr0["REQUEST_STATUS_ID"]);
                requestStatusName = Convert.ToString(dr0["ADD_ADVANCE_STATUS_NAME"]);
                requestedOn = Convert.ToString(dr0["CREATED_ON"]);
                hodApprovedBy = Convert.ToString(dr0["HOD_APPROVED_BY"]);
                hodApprovedOn = Convert.ToString(dr0["HOD_APPROVED_ON"]);
                custVendName = Convert.ToString(dr0["CUST_VEND_NAME"]);
                placeOfvisit =  Convert.ToString(dr0["PLACE_OF_VISIT"]);
                deletedOn = Convert.ToString(dr0["DELETED_ON"]);
                deletedByEmail = Convert.ToString(dr0["DELETED_BY_EMAIL_ID"]);
                deletedBy = Convert.ToString(dr0["DELETED_BY_NAME"]);

                cancelledOn = Convert.ToString(dr0["CANCELLED_ON"]);

                unitId = Convert.ToInt32(dr0["UNIT_ID"]);
                if (dr0["CREATED_BY_ACC"] != DBNull.Value)
                    createdByAccNo = Convert.ToString(dr0["CREATED_BY_ACC"]);

                if (dr0["CREATED_BY_IFSC"] != DBNull.Value)
                    createdByIFSC = Convert.ToString(dr0["CREATED_BY_IFSC"]);




                if (dsTravelInfo.Tables[1].Rows.Count > 0)
                {
                    accTeamName = Convert.ToString(dsTravelInfo.Tables[1].Rows[0]["ACC_NAME"]);
                    accTeamEmail = Convert.ToString(dsTravelInfo.Tables[1].Rows[0]["ACC_EMAIL"]);
                }

                //Senthil And Pawan Sharma
                if (dsTravelInfo.Tables[2].Rows.Count > 0)
                {
                    //accAcvnaceFCEmail = Convert.ToString(dsUserInfo.Tables[3].Rows[0]["EMAIL_ID"]);
                    foreach (DataRow dr in dsTravelInfo.Tables[2].Rows)
                    {
                        accAcvnaceFCEmail += ";" + Convert.ToString(dr["EMAIL_ID"]);
                    }
                    //accAcvnaceFCEmail = accAcvnaceFCEmail.TrimStart(',');
                }



            }
            else
            {
                sendVal = "";
            }

            string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);

            if (requestStatusId == 1)//new
            {
                from = empEmail;
                to = teamleaderEmail;
                bcc = empEmail;
                fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/AdditionalAdvanceReq.html";
                subject = "Additional Advance Request : '" + reqNo + "' Created On: " + requestedOn;

                actId = 1;

                approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TRAVEL/UpdateAddAdvanceStatusNewOne.aspx?requestid="
                    + RequestID + "&tourno=" + tourNo + "&actid=" + 2 + "&requeststatusid=" + 2 +
                    "&ud=" + teamleaderUserName + "&pd=" + teamleaderPwd + "&tlemprecordid=" + Convert.ToString(teamleaderId) +
                    "&emprecordid=" + Convert.ToString(teamleaderId) + "'";

                approveHref = "<a href=\"" + approveLink + "\">Approve</a>";

                deleteLink = urlTxt + "/TOUR_AND_TRAVELS/TRAVEL/UpdateAddAdvanceStatusNewOne.aspx?requestid=" + 4;



                deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TRAVEL/UpdateAddAdvanceStatusNewOne.aspx?requestid=" + RequestID + "&tourno=" + tourNo +
                    "&actid=3" + "&requeststatusid=" + 3 + "&ud=" + teamleaderUserName + "&pd=" + teamleaderPwd + "&tlemprecordid=" + Convert.ToString(teamleaderId) +
                    "&emprecordid=" + Convert.ToString(teamleaderId) + "'";

                deleteHref = "<a href=\"" + deleteLink + "\">Reject</a>";


            }
            else if(requestStatusId == 2)//HOD Approve
            {
                if (Convert.ToInt32(reqAddAdvanceAmt) > 0)
                {

                    from = teamleaderEmail;
                    bcc = teamleaderEmail;
                  
                    //68 for INR in DB
                    if (reqAddAdvanceAmtCurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
                    {
                        to = accTeamEmail;
                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/AddAdvHODToAccTourMail.htm";
                    }
                    else
                    {
                        to = accAcvnaceFCEmail;
                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/AddAdvHODToSenthilTourMailForeignTour.htm";
                    }
                    cc =  empEmail;
                    //cc = hrTeamEmail + "," + empEmail;
                    //subject = "Additional Advance Request : '" + reqNo + '"' + " approved on: " + hodApprovedOn;
                    subject = "Additional Advance Request : '" + reqNo + "' approved on: " + hodApprovedOn;

                }



            }
            else if (requestStatusId == (int)TandTAllStatus.EnumTourStatus.Deleted)
            {
                //deletedByEmail = teamleaderEmail;
                if(deletedByID == empId)
                {
                    deletedBy = empName;

                    from = deletedByEmail;
                    //to = hodApprovedByEmail;  //createdByEmail;
                    to = empEmail;
                    bcc = deletedByEmail;

                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/AddAdvDeleteRequestInfoMail.htm";
                    subject = "Additional Advance Request : '" + reqNo + "' deleted on: " + deletedOn;


                }
                else
                {
                    from = deletedByEmail;
                    //to = hodApprovedByEmail;  //createdByEmail;
                    to = empEmail;
                    bcc = deletedByEmail;

                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/AddAdvRejectRequestInfoMail.htm";
                    subject = "Additional Advance Request : '" + reqNo + "' rejected on: " + deletedOn;
                }
                
                

            }

            //Cancelled
            else if (requestStatusId == (int)TandTAllStatus.EnumTourStatus.Cancelled)
            {
                deletedByEmail = teamleaderEmail;
                deletedBy = teamleaderName;
                from = deletedByEmail;
                //to = hodApprovedByEmail;  //createdByEmail;
                to = empEmail;
                bcc = deletedByEmail;

                fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/AddAdvCancelRequestInfoMail.htm";
                subject = "Additional Advance Request : '" + reqNo + "' cancelled on: " + cancelledOn;
                
            }

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);



            if (requestStatusId == (int)TandTAllStatus.EnumTourStatus.HODApproved && (reqAddAdvanceAmtCurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)) 
            {
                isAdviceGenerated = 0;
                AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitId, mail);

            }


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



            body = body.Replace("{#customername#}", custVendName);

            body = body.Replace("{#place#}", placeOfvisit);

            body = body.Replace("{#tourno#}", tourNo);
            body = body.Replace("{#toursanctionno#}", sanctionNo);
            body = body.Replace("{#travellername#}", empName);
            body = body.Replace("{#customername#}", custVendName);
            body = body.Replace("{#place#}", placeOfvisit);

            body = body.Replace("{#startdate#}", startDate);


            body = body.Replace("{#enddate#}", endDate);

            body = body.Replace("{#advancetaken#}", advanceAmt.ToString());
            body = body.Replace("{#advancetakencurr#}", advanceAmtCurrency);

            body = body.Replace("{#requestedAddAdvance#}", reqAddAdvanceAmt.ToString());
            body = body.Replace("{#requestedAddAdvancecurr#}", reqAddAdvanceAmtCurrency);

            body = body.Replace("{#requesterremarks#}", remarks);

            body = body.Replace("{#createdby#}", empName);
            body = body.Replace("{#createdon#}", requestedOn);
            body = body.Replace("{#approvelink#}", approveHref);
            body = body.Replace("{#deletelink#}", deleteHref);
            body = body.Replace("{#approvedon#}", hodApprovedOn);
            //body = body.Replace("{#remarks#}", remarks);
            

            body = body.Replace("{#approvedby#}", hodApprovedBy);
      
            //body = body.Replace("{#approvedremarks#}", hodApprovedRemarks);


            body = body.Replace("{#accname#}", accTeamName);
            body = body.Replace("{#advanceamount#}", Convert.ToString(advanceAmt));
            body = body.Replace("{#advancecurrency#}", advanceAmtCurrency);

            body = body.Replace("{#deletedby#}", deletedBy);
            body = body.Replace("{#deletedon#}", deletedOn);
            body = body.Replace("{#deletedremark#}", deletedRemarks);

            body = body.Replace("{#cancelledby#}", cancelledBy);
            body = body.Replace("{#cancelledon#}", cancelledOn);
            body = body.Replace("{#cancelledremark#}", cancelledRemarks);

            body = body.Replace("{#approvedremarks#}", HodApprovedByRemarks);



            mail.Body = body;

            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                SmtpServer.Send(mail);
                //sendVal = 1;
                //sendVal = RequestID.ToString();
                sendVal = reqNo.ToString();
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    //sendVal = RequestID.ToString(); 
                    sendVal = reqNo.ToString();

                else
                    sendVal = "";
            }
        }
        catch (Exception ex )
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

        if (Convert.ToInt32(reqAddAdvanceAmt) > 0 && reqAddAdvanceAmtCurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
                    isAdviceGenerated == 0 && !string.IsNullOrEmpty(Convert.ToString(createdByAccNo)) &&
                    !string.IsNullOrEmpty(Convert.ToString(createdByIFSC))
                   )
        {
            csvTxt = GenerateAdviceCSV(empName, sanctionNo, Convert.ToString(reqAddAdvanceAmt), empEmail, createdByAccNo, createdByIFSC, unitID);
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