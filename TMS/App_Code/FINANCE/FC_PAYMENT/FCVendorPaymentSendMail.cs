using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using BAL;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;


public class FCVendorPaymentSendMail
{

    #region VARIABLES[===================]


    //GetLOTMailType objGetLOTMailType = new GetLOTMailType();

    //GetLOTNextStatus objGetLOTNextStatus = new GetLOTNextStatus();
    FCPaymentHtmlForPDF objFCPaymentHtmlForPDF = new FCPaymentHtmlForPDF();
    LOTMailInfoProperties objLOTMailInfoProperties = new LOTMailInfoProperties();
    Project objProject = new Project();
    DataSet _DsMailInfo = new DataSet();

    //int isTransferred = 0;

    //int oldTransferredLotTfIdCreatedById = 0;
    //string oldTransferredLotTfIdCreatedBy = "";
    //string oldTransferredLotTfIdCreatedByEmail = "";

    //int currentStatusID = 0;
    ////int nextStatusID = 0;
    //int returnVal = 0;
    //bool isCCAllowed = false;

    //string LOTMainItem = "";
    //int LOTMainItemID = 0;
    //int LOTMainSubitemID = 0;
    //int newStatusID = 0;
    //int recordNo = 0;
    //int LOTTFSubitemID = 0;


    int _MailTypeID = 0;

    //private int _CreatedByID = 0;
    private string _CreatedByName = string.Empty;
    private string _CreatedByEmail = string.Empty;
    private string _CreatedByRemarks = string.Empty;

    private string _CancelledByName = string.Empty;
    private string _CancelledByEmail = string.Empty;
    private string _CancelledByRemarks = string.Empty;

    private int _AmendedByID = 0;
    private string _AmendedByName = string.Empty;
    private string _AmendedByEmail = string.Empty;
    private string _AmendedByRemarks = string.Empty;


    //int PEID = 0;
    //string PEName = string.Empty;
    //string PEEmail = string.Empty;
    //string PEUD = string.Empty;
    //string PEPD = string.Empty;

    //int PMID = 0;
    //string PMName = string.Empty;
    //string PMEmail = string.Empty;
    //string PMUD = string.Empty;
    //string PMPD = string.Empty;


    //int planningMngrID = 0;
    //string planningMngrIDs = string.Empty;
    //string planningMngrName = string.Empty;
    //string planningMngrEmail = string.Empty;
    //string planningMngrEmailCC = string.Empty;

    //int prodMngrID = 0;
    //string prodMngrName = string.Empty;
    //string prodMngrEmail = string.Empty;
    //string prodMngrEmailCC = string.Empty;


    //string qualityMngrName = string.Empty;
    //string qualityNames = string.Empty;
    //string qualityEmails = string.Empty;

    //string planningMngrName = string.Empty;
    //string planningNames = string.Empty;
    //string planningEmails = string.Empty;

    //int approvedByID = 0;
    string _ApprovedByName = string.Empty;
    string _ApprovedByEmail = string.Empty;
    string _ApprovedByRemarks = string.Empty;

    //int planningAcceptedByID = 0;
    //string planningAcceptedByName = string.Empty;
    //string planningAcceptedByEmail = string.Empty;

    //int forwardedByID = 0;
    //string forwardedByName = string.Empty;
    //string forwardedByEmail = string.Empty;

    //int acceptedByID = 0;
    //string acceptedByName = string.Empty;
    //string acceptedByEmail = string.Empty;

    //int qualityAcceptedByID = 0;
    //string qualityAcceptedByName = string.Empty;
    //string qualityAcceptedByEmail = string.Empty;



    //int sendToIntlInspectionByID = 0;
    //string sendToIntlInspectionByName = string.Empty;
    //string sendToIntlInspectionByEmail = string.Empty;

    //int qAIntlInspectionAcceptedByID = 0;
    //string qAIntlInspectionAcceptedByName = string.Empty;
    //string qAIntlInspectionAcceptedByEmail = string.Empty;

    //int qAIntlInspectionNotAcceptedByID = 0;
    //string qAIntlInspectionNotAcceptedByName = string.Empty;
    //string qAIntlInspectionNotAcceptedByEmail = string.Empty;

    //int sentToFinalInspByID = 0;
    //string sentToFinalInspByName = string.Empty;
    //string sentToFinalInspByEmail = string.Empty;

    //int sentToReworkByID = 0;
    //string sentToReworkByName = string.Empty;
    //string sentToReworkByEmail = string.Empty;


    private int _AmendmentCount = 0;

    private int _AmendmentByID = 0;
    private string _AmendmentByName = string.Empty;
    private string _AmendmentByEmail = string.Empty;
    private string _AmendmentByRemarks = string.Empty;


    //int prodAmendmentByID = 0;
    //string prodAmendmentByName = string.Empty;
    //string prodAmendmentByEmail = string.Empty;

    int _AmendedApprovedByID = 0;
    string _AmendedApprovedByName = string.Empty;
    string _AmendedApprovedByEmail = string.Empty;
    string _AmendedApprovedByRemarks = string.Empty;


    //int planningAmendedAcceptedByID = 0;
    //string planningAmendedAcceptedByName = string.Empty;
    //string planningAmendedAcceptedByEmail = string.Empty;

    //int forwardedAmendedByID = 0;
    //string forwardedAmendedByName = string.Empty;
    //string forwardedAmendedByEmail = string.Empty;

    //int amendedAcceptedByID = 0;
    //string amendedAcceptedByName = string.Empty;
    //string amendedAcceptedByEmail = string.Empty;

    //int qualityAmendedAcceptedByID = 0;
    //string qualityAmendedAcceptedByName = string.Empty;
    //string qualityAmendedAcceptedByEmail = string.Empty;


    //int completedByID = 0;
    //string completedByName = string.Empty;
    //string completedByEmail = string.Empty;

    private int _PaymentReleasedByID = 0;
    private string _PaymentReleasedByName = string.Empty;
    private string _PaymentReleasedByEmail = string.Empty;
    private string _PaymentReleasedByRemarks = string.Empty;


    //string storePersonName = string.Empty;
    //string storePersonEmail = string.Empty;
    //string valvesAdditionalEmailCC = string.Empty;


    private string _URLTxt = string.Empty;
    private string _HREF = string.Empty;
    private string _Link = string.Empty;

    //string PEApproveHref = string.Empty;
    //string PEApproveLink = string.Empty;

    //string PEAmendmentHref = string.Empty;
    //string PEAmendmentLink = string.Empty;



    //string PMAmendmentHref = string.Empty;
    //string PMAmendmentLink = string.Empty;


    private string _ApprovalHREF = string.Empty;
    private string _ApprovalLink = string.Empty;

    private string _FileName = string.Empty;
    private string _Body = string.Empty;
    private string _Subject = string.Empty;
    private string _From = string.Empty;
    private string _To = string.Empty;
    private string _CC = string.Empty;
    private string _BCC = string.Empty;


    string _ToAPPROVEREmail = string.Empty;
    string _CCAPPROVEREmail = string.Empty;
    string _BCCAPPROVEREmail = string.Empty;

    string _ToRELEASEREmail = string.Empty;
    string _CCRELEASEREmail = string.Empty;
    string _BCCRELEASEREmail = string.Empty;

    //bool isPlannigMailSent = false;
    //bool isQualityMailSent = false;

    //DataTable dtQuantityDetails = new DataTable();
    //DataTable dtProductionOrderNumberOfOldTransferredLOT = new DataTable();
    //DataTable dtQuantityList = new DataTable();
    //DataTable dtProductionStatusList = new DataTable();

    #endregion


    BAL.FCVendorPayment objFCVendorPayment = new BAL.FCVendorPayment();

    private string _PaymentRequestNo;
    private int _StatusID;
    private string _DateOfRequest;
    private string _JobNo;
    private string _VendorCode;
    private string _VendorName;
    private string _VendorAddress;
    private string _PoNo;
    private decimal _AmountToBeReleased;
    private decimal _PoAmount;
    private string _Currency;

    private string _UID;
    private string _PWD;

    int _ReturnVal = 0;

    public int ProcessSendEmail(int PID, int statusIdSM)
    {
        try
        {
            _DsMailInfo = objFCVendorPayment.GetVendorPaymentDetailsForMail(PID);

            if (_DsMailInfo.Tables.Count > 0)
            {
                if (_DsMailInfo.Tables[0].Rows.Count > 0)
                {
                    if (_DsMailInfo.Tables[0].Rows[0]["PAYMENT_REQUEST_NO"] != DBNull.Value)
                        _PaymentRequestNo = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["PAYMENT_REQUEST_NO"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["STATUS_FID"] != DBNull.Value)
                        _StatusID = Convert.ToInt32(_DsMailInfo.Tables[0].Rows[0]["STATUS_FID"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["DATE_OF_REQUEST"] != DBNull.Value)
                        _DateOfRequest = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["DATE_OF_REQUEST"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value)
                        _JobNo = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["JOB_NO"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["VENDOR_CODE"] != DBNull.Value)
                        _VendorCode = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["VENDOR_CODE"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["VENDOR_NAME"] != DBNull.Value)
                        _VendorName = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["VENDOR_NAME"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["VENDOR_ADDRESS"] != DBNull.Value)
                        _VendorAddress = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["VENDOR_ADDRESS"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["PO_NO"] != DBNull.Value)
                        _PoNo = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["PO_NO"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["PO_AMOUNT"] != DBNull.Value)
                        _PoAmount = Convert.ToDecimal(_DsMailInfo.Tables[0].Rows[0]["PO_AMOUNT"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["PO_AMOUNT_CURRENCY"] != DBNull.Value)
                        _Currency = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["PO_AMOUNT_CURRENCY"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["AMOUNT_TO_BE_RELEASED"] != DBNull.Value)
                        _AmountToBeReleased = Convert.ToDecimal(_DsMailInfo.Tables[0].Rows[0]["AMOUNT_TO_BE_RELEASED"]);


                    if (_DsMailInfo.Tables[0].Rows[0]["CREATED_BY"] != DBNull.Value)
                        _CreatedByName = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["CREATED_BY"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["CREATED_BY_EMAIL"] != DBNull.Value)
                        _CreatedByEmail = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["CREATED_BY_EMAIL"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["CREATED_REMARKS"] != DBNull.Value)
                        _CreatedByRemarks = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["CREATED_REMARKS"]);



                    if (_DsMailInfo.Tables[0].Rows[0]["APPROVED_BY"] != DBNull.Value)
                        _ApprovedByName = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["APPROVED_BY"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["APPROVED_BY_EMAIL"] != DBNull.Value)
                        _ApprovedByEmail = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["APPROVED_BY_EMAIL"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["APPROVED_REMARKS"] != DBNull.Value)
                        _ApprovedByRemarks = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["APPROVED_REMARKS"]);



                    if (_DsMailInfo.Tables[0].Rows[0]["AMENDED_BY"] != DBNull.Value)
                        _AmendedByName = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["AMENDED_BY"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["AMENDED_BY_EMAIL"] != DBNull.Value)
                        _AmendedByEmail = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["AMENDED_BY_EMAIL"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["AMENDED_REMARKS"] != DBNull.Value)
                        _AmendedByRemarks = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["AMENDED_REMARKS"]);



                    if (_DsMailInfo.Tables[0].Rows[0]["APPROVED_AMENDED_BY"] != DBNull.Value)
                        _AmendedApprovedByName = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["APPROVED_AMENDED_BY"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["APPROVED_AMENDED_BY_EMAIL"] != DBNull.Value)
                        _AmendedApprovedByEmail = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["APPROVED_AMENDED_BY_EMAIL"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["APPROVED_AMENDED_REMARKS"] != DBNull.Value)
                        _AmendedApprovedByRemarks = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["APPROVED_AMENDED_REMARKS"]);



                    if (_DsMailInfo.Tables[0].Rows[0]["SENT_TO_AMENDMENT_BY"] != DBNull.Value)
                        _AmendmentByName = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["SENT_TO_AMENDMENT_BY"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["SENT_TO_AMENDMENT_BY_EMAIL"] != DBNull.Value)
                        _AmendmentByEmail = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["SENT_TO_AMENDMENT_BY_EMAIL"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["SENT_TO_AMENDMENT_REMARKS"] != DBNull.Value)
                        _AmendmentByRemarks = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["SENT_TO_AMENDMENT_REMARKS"]);



                    if (_DsMailInfo.Tables[0].Rows[0]["PAYMENT_RELEASED_BY"] != DBNull.Value)
                        _PaymentReleasedByName = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["PAYMENT_RELEASED_BY"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["PAYMENT_RELEASED_BY_EMAIL"] != DBNull.Value)
                        _PaymentReleasedByEmail = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["PAYMENT_RELEASED_BY_EMAIL"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["PAYMENT_RELEASED_REMARKS"] != DBNull.Value)
                        _PaymentReleasedByRemarks = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["PAYMENT_RELEASED_REMARKS"]);



                    if (_DsMailInfo.Tables[0].Rows[0]["CANCELLED_BY"] != DBNull.Value)
                        _CancelledByName = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["CANCELLED_BY"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["CANCELLED_BY_EMAIL"] != DBNull.Value)
                        _CancelledByEmail = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["CANCELLED_BY_EMAIL"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["CANCELLED_REMARKS"] != DBNull.Value)
                        _CancelledByRemarks = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["CANCELLED_REMARKS"]);



                    if (_DsMailInfo.Tables[0].Rows[0]["UID"] != DBNull.Value)
                        _UID = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["UID"]);

                    if (_DsMailInfo.Tables[0].Rows[0]["PWD"] != DBNull.Value)
                        _PWD = Convert.ToString(_DsMailInfo.Tables[0].Rows[0]["PWD"]);


                }




                if (_DsMailInfo.Tables[1].Rows.Count > 0)
                {
                    //APPROVER
                    foreach (var dr1 in _DsMailInfo.Tables[1].Select("IS_APPROVER = 1"))
                    {
                        if (Convert.ToInt32(dr1["TO"]) > 0)
                        {
                            _ToAPPROVEREmail += dr1["EMAIL_ID"].ToString() + ";";
                        }

                        if (Convert.ToInt32(dr1["CC"]) > 0)
                        {
                            _CCAPPROVEREmail += dr1["EMAIL_ID"].ToString() + ";";
                        }

                        if (Convert.ToInt32(dr1["BCC"]) > 0)
                        {
                            _BCCAPPROVEREmail += dr1["EMAIL_ID"].ToString() + ";";
                        }
                    }


                    _ToAPPROVEREmail = _ToAPPROVEREmail.TrimEnd(';');
                    _CCAPPROVEREmail = _CCAPPROVEREmail.TrimEnd(';');
                    _BCCAPPROVEREmail = _BCCAPPROVEREmail.TrimEnd(';');

                    //RELEASER
                    foreach (var dr1 in _DsMailInfo.Tables[1].Select("IS_RELEASER = 1"))
                    {
                        if (Convert.ToInt32(dr1["TO"]) > 0)
                        {
                            _ToRELEASEREmail += dr1["EMAIL_ID"].ToString() + ";";
                        }

                        if (Convert.ToInt32(dr1["CC"]) > 0)
                        {
                            _CCRELEASEREmail += dr1["EMAIL_ID"].ToString() + ";";
                        }

                        if (Convert.ToInt32(dr1["BCC"]) > 0)
                        {
                            _BCCRELEASEREmail += dr1["EMAIL_ID"].ToString() + ";";
                        }
                    }

                    _ToRELEASEREmail = _ToRELEASEREmail.TrimEnd(';');
                    _CCRELEASEREmail = _CCRELEASEREmail.TrimEnd(';');
                    _BCCRELEASEREmail = _BCCRELEASEREmail.TrimEnd(';');

                }
            }

            _ReturnVal = CallSendMailFunction(PID, statusIdSM);

            return _ReturnVal;
        }

        catch (Exception ex)
        {
            _ReturnVal = 0;
            return _ReturnVal;
        }
    }


    private int CallSendMailFunction(int PID, int statusIdSM)
    {
        int mailValue = 0;
        try
        {
            _URLTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);
            _Link = "'" + _URLTxt + "/Login.aspx?tfno=" + _PaymentRequestNo;//+ "'";

            GetMailTypeID(statusIdSM);

            if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.APPROVAL_MAIL)
            {
                _From = _CreatedByEmail;
                _To = _ToAPPROVEREmail;
                _CC = _CCAPPROVEREmail;
                _BCC = _BCCAPPROVEREmail;
                _Subject = "Approval Request for Payment Request No.: " + _PaymentRequestNo;
                _FileName = "~/FINANCE/FC_PAYMENT/EMAIL_FORMATS/01ApprovalEMAIL.htm";

                _ApprovalLink = "'" + _URLTxt + "/FINANCE/FC_PAYMENT/UpdateStatusFCVendorPayment.aspx?PID=" + PID + "&ud=" + _UID + "&pd=" + _PWD + "'";
                _ApprovalHREF = "<a href=" + _ApprovalLink + ">Approve/Send To Amendment LOT</a>";

            }
            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.APPROVED_MAIL)
            {
                _From = _ApprovedByEmail;
                _To = _ToRELEASEREmail;
                _CC = _CCRELEASEREmail + ";" + _CreatedByEmail;
                _BCC = _BCCRELEASEREmail;
                _Subject = "Release Payment Request No.: " + _PaymentRequestNo;
                _FileName = "~/FINANCE/FC_PAYMENT/EMAIL_FORMATS/02ApprovedEMAIL.htm";
            }
            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.AMENDMENT_MAIL)
            {
                _From = _AmendmentByEmail;
                _To = _CreatedByEmail;
                _Subject = "Amend Payment Request No.: " + _PaymentRequestNo;
                _FileName = "~/FINANCE/FC_PAYMENT/EMAIL_FORMATS/03SentToAmendmentEMAIL.htm";
            }
            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.AMENDED_APPROVAL_MAIL)
            {
                _From = _AmendedByEmail;
                _To = _ToAPPROVEREmail;
                _CC = _CCAPPROVEREmail;
                _BCC = _BCCAPPROVEREmail;
                _Subject = "Approve Amended Payment Request No.: " + _PaymentRequestNo;
                _FileName = "~/FINANCE/FC_PAYMENT/EMAIL_FORMATS/04AmendedEMAIL.htm";
            }
            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.AMENDED_APPROVED_MAIL)
            {
                _From = _AmendedApprovedByEmail;
                _To = _ToRELEASEREmail;
                _CC = _CCRELEASEREmail + ";" + _AmendedByEmail;
                _BCC = _BCCRELEASEREmail;
                _Subject = "Release Amended Payment Request No.: " + _PaymentRequestNo;
                _FileName = "~/FINANCE/FC_PAYMENT/EMAIL_FORMATS/05AmendedApprovedEMAIL.htm";
            }
            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.RELEASED_MAIL)
            {
                _From = _PaymentReleasedByEmail;
                _To = _CreatedByEmail;
                _CC = _ApprovedByEmail;
                _Subject = "Released Payment Request No.: " + _PaymentRequestNo;
                _FileName = "~/FINANCE/FC_PAYMENT/EMAIL_FORMATS/06PaymenttReleasedEMAIL.htm";
            }

            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.CANCELLED_MAIL)
            {
                _From = _CancelledByEmail;
                _To = _ToAPPROVEREmail;
                _CC = _CCAPPROVEREmail;
                _Subject = "Payment Request No.: " + _PaymentRequestNo + " is Cancelled";
                _FileName = "~/FINANCE/FC_PAYMENT/EMAIL_FORMATS/07PaymenttCancelledEMAIL.htm";
            }


            mailValue = SendMail();

            return mailValue;

        }
        catch (Exception)
        {
            return mailValue = 0;
        }
    }

    private void GetMailTypeID(int statusIdSM)
    {
        try
        {
            if (statusIdSM > 0)
            {
                _StatusID = statusIdSM;
            }


            if (_StatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.OPEN)
            {
                _MailTypeID = Convert.ToInt32(FCVendorPaymentStatusTypes.EnumEmailType.APPROVAL_MAIL);
            }

            else if (_StatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT)
            {
                _MailTypeID = Convert.ToInt32(FCVendorPaymentStatusTypes.EnumEmailType.AMENDMENT_MAIL);
            }

            else if (_StatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED)
            {
                _MailTypeID = Convert.ToInt32(FCVendorPaymentStatusTypes.EnumEmailType.AMENDED_APPROVAL_MAIL);
            }

            else if (_StatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED)
            {
                _MailTypeID = Convert.ToInt32(FCVendorPaymentStatusTypes.EnumEmailType.AMENDED_APPROVED_MAIL);
            }

            else if (_StatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED)
            {
                _MailTypeID = Convert.ToInt32(FCVendorPaymentStatusTypes.EnumEmailType.APPROVED_MAIL);
            }

            else if (_StatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.RELEASED)
            {
                _MailTypeID = Convert.ToInt32(FCVendorPaymentStatusTypes.EnumEmailType.RELEASED_MAIL);
            }
            else if (_StatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.CANCELLED)
            {
                _MailTypeID = Convert.ToInt32(FCVendorPaymentStatusTypes.EnumEmailType.CANCELLED_MAIL);
            }

        }
        catch (Exception)
        {

        }
    }

    private int SendMail()
    {

        //to = "deekshant.ravi@coperion.com";
        //cc = "deekshant.ravi@coperion.com";
        //bcc = "deekshant.ravi@coperion.com";

        //DataView dv = dtSubitem.DefaultView;
        //dv.Sort = "LOT_TF_SUBITEM_ID";
        //dtSubitem = dv.ToTable();

        int returnVal = 0;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();


        if (_MailTypeID != Convert.ToInt32(LOTAllStatusAndTypes.EnumEmailType.NotApplicable) && !string.IsNullOrEmpty(_To))
        {
            if (!string.IsNullOrEmpty(_Subject))
                mail.Subject = _Subject;

            if (!string.IsNullOrEmpty(_From))
                mail.From = new MailAddress(_From);

            if (!string.IsNullOrEmpty(_To))
            {
                _To = _To.TrimEnd(';');
                string items = string.Empty;
                string[] strTo = _To.Split(';');
                foreach (string item in strTo)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (!items.Contains(item))
                        {
                            items += item + ";";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(items))
                    items = items.TrimEnd(';');

                string[] strToNew = items.Split(';');

                foreach (string item in strToNew)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.To.Add(item);
                    }
                }
            }

            if (!string.IsNullOrEmpty(_CC))
            {
                _CC = _CC.TrimEnd(';');
                string items = string.Empty;
                string[] strCC = _CC.Split(';');

                foreach (string item in strCC)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (!items.Contains(item))
                        {
                            items += item + ";";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(items))
                    items = items.TrimEnd(';');

                string[] strCCNew = items.Split(';');

                foreach (string item in strCCNew)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.CC.Add(item);
                    }
                }
            }

            if (!string.IsNullOrEmpty(_BCC))
            {
                _BCC = _BCC.TrimEnd(';');
                string items = string.Empty;
                string[] strBCC = _BCC.Split(';');
                foreach (string item in strBCC)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (!items.Contains(item))
                        {
                            items += item + ";";
                        }
                    }
                }


                if (!string.IsNullOrEmpty(items))
                    items = items.TrimEnd(';');

                string[] strBCCNew = items.Split(';');
                foreach (string item in strBCCNew)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.Bcc.Add(item);
                    }
                }
            }


            mail.IsBodyHtml = true;

            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(_FileName)))
            {
                _Body = reader.ReadToEnd();
            }

            if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.APPROVAL_MAIL)
            {
                _Body = _Body.Replace("{#fromName#}", _CreatedByName);
                _Body = _Body.Replace("{#toName#}", "Sir");
                _Body = _Body.Replace("{#remarks#}", _CreatedByRemarks);
                _Body = _Body.Replace("{#link#}", _ApprovalHREF);
            }

            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.APPROVED_MAIL)
            {
                _Body = _Body.Replace("{#fromName#}", _ApprovedByName);
                _Body = _Body.Replace("{#toName#}", "Team");
                _Body = _Body.Replace("{#remarks#}", _ApprovedByRemarks);
            }

            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.AMENDMENT_MAIL)
            {
                _Body = _Body.Replace("{#fromName#}", _AmendmentByName);
                _Body = _Body.Replace("{#toName#}", _CreatedByName);
                _Body = _Body.Replace("{#remarks#}", _AmendmentByRemarks);
            }

            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.AMENDED_APPROVAL_MAIL)
            {
                _Body = _Body.Replace("{#fromName#}", _AmendedByName);
                _Body = _Body.Replace("{#toName#}", "Sir");
                _Body = _Body.Replace("{#remarks#}", _AmendedByRemarks);
            }

            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.AMENDED_APPROVED_MAIL)
            {
                _Body = _Body.Replace("{#fromName#}", _AmendedApprovedByName);
                _Body = _Body.Replace("{#toName#}", "Team");
                _Body = _Body.Replace("{#remarks#}", _AmendedApprovedByRemarks);
            }

            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.RELEASED_MAIL)
            {
                _Body = _Body.Replace("{#fromName#}", _PaymentReleasedByName);
                _Body = _Body.Replace("{#toName#}", _CreatedByName);
                _Body = _Body.Replace("{#remarks#}", _PaymentReleasedByRemarks);
            }

            else if (_MailTypeID == (int)FCVendorPaymentStatusTypes.EnumEmailType.CANCELLED_MAIL)
            {
                _Body = _Body.Replace("{#fromName#}", _CancelledByName);
                _Body = _Body.Replace("{#toName#}", "Sir");
                _Body = _Body.Replace("{#remarks#}", _CancelledByRemarks);
            }

            _Body = _Body.Replace("{#paymentRequestNo#}", _PaymentRequestNo);
            _Body = _Body.Replace("{#dateOfRequest#}", _DateOfRequest);
            _Body = _Body.Replace("{#jobNo#}", _JobNo);
            _Body = _Body.Replace("{#vendorCode#}", _VendorCode);
            _Body = _Body.Replace("{#vendorName#}", _VendorName);
            _Body = _Body.Replace("{#vendorAddress#}", _VendorAddress);
            _Body = _Body.Replace("{#poNo#}", _PoNo);
            _Body = _Body.Replace("{#poAmount#}", Convert.ToString(_PoAmount));
            _Body = _Body.Replace("{#currency#}", Convert.ToString(_Currency));
            _Body = _Body.Replace("{#amountToBeReleased#}", Convert.ToString(_AmountToBeReleased));


            mail.Body = _Body;

            byte[] bytes = GetPDFBytes(_DsMailInfo.Tables[2]);
            mail.Attachments.Add(new Attachment(new MemoryStream(bytes), _PaymentRequestNo + ".pdf"));


            //if (IRNFileBytes != null)
            //{
            //    mail.Attachments.Add(new Attachment(new MemoryStream(IRNFileBytes), "IRN_" + TFNo + ".pdf"));
            //}

            try
            {
                if (!string.IsNullOrEmpty(_To))
                {
                    SmtpServer.Send(mail);
                    returnVal = 1;
                }
                else
                    returnVal = 0;
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    returnVal = 1;

                else
                    returnVal = 0;
            }
        }
        else
            returnVal = 0;

        return returnVal;

    }

    public byte[] GetPDFBytes(DataTable dtPDFInfo)
    {
        try
        {
            byte[] pdfBytes;
            //DataSet ds = new DataSet();
            //DataTable dtSubitems = new DataTable();

            //if (Session["dsJobNo"] != null)
            //    ds = (DataSet)Session["dsJobNo"];
            //else
            //    ds = GetJOBData();

            //if (Session["dtSubitem"] != null)
            //    dtSubitems = (DataTable)Session["dtSubitem"];

            //var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

            var cssText = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Styles/LOT.css"));

            string htmltxt = objFCPaymentHtmlForPDF.GetHtmlForPDF(dtPDFInfo);

            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(htmltxt))
            {
                sb.Append("<html>\n");
                sb.Append("<body>\n");

                sb.Append(htmltxt + "\n");

                sb.Append("</body>\n");
                sb.Append("</html>\n");
            }

            var html = sb.ToString();

            //string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";            
            string imagePath = System.Web.Hosting.HostingEnvironment.MapPath("\\Images\\COPERION") + "\\logo2.png";

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
            img.Alignment = Element.ALIGN_LEFT;
            img.ScaleToFit(180f, 250f);


            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4);
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                document.Add(img);
                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                {
                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                    }
                }

                document.Close();
                pdfBytes = memoryStream.GetBuffer();

                return pdfBytes;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}