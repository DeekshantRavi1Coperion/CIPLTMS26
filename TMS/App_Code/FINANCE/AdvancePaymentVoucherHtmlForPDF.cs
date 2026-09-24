using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

using System.Text;
//using System.Net.Mime;
//using iTextSharp.tool.xml.pipeline.css;
//using iTextSharp.tool.xml;
//using iTextSharp.tool.xml.pipeline.html;
//using iTextSharp.tool.xml.pipeline.end;
//using iTextSharp.tool.xml.parser;
//using System.Xml;
//using iTextSharp.tool.xml.css;
using System.Data;

/// <summary>
/// Summary description for LOTHtmlForPDF
/// </summary>
public class AdvancePaymentVoucherHtmlForPDF
{
    int SrNo = 0;




    string PaymentRunNumber = string.Empty;
    string Unit = string.Empty;
    string RequestDate = string.Empty;
    string VendorCode = string.Empty;
    string VendorName = string.Empty;
    string BankName = string.Empty;
    string Branch = string.Empty;
    string RTGS_IFSCcode = string.Empty;
    string BankAccountNumber = string.Empty;
    string Status = string.Empty;
    string GeneratedBy = string.Empty;
    string GeneratedOn = string.Empty;
    string HODApprovedBy = string.Empty;
    string HODApprovedOn = string.Empty;
    string AccountsApprovedBy = string.Empty;
    string AccountsApprovedOn = string.Empty;
    string BAGeneratedBy = string.Empty;
    string BAGeneratedOn = string.Empty;
    string FACT_VoucherStatus = string.Empty;


    string FACT_VoucherNumber = string.Empty;
    string OrderNumber = string.Empty;
    string OrderDate = string.Empty;
    string ProjectNumber = string.Empty;
    string PaymentTerm1 = string.Empty;
    string PaymentTerm2 = string.Empty;
    string PaymentTerm3 = string.Empty;
    string PaymentTerm4 = string.Empty;
    string POCreatedBy = string.Empty;
    string PO_BasicValue = string.Empty;
    string RequestedAmount = string.Empty;
    string Approved_Amount = string.Empty;

    public AdvancePaymentVoucherHtmlForPDF()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetHtmlForPDF(DataSet dsVouchers, string paymentRunNumber, int vCount, string imgPath, int hiddenFlag)
    {
        try
        {
            SrNo = 0;
            Unit = string.Empty;
            RequestDate = string.Empty;
            VendorCode = string.Empty;
            VendorName = string.Empty;
            BankName = string.Empty;
            Branch = string.Empty;
            RTGS_IFSCcode = string.Empty;
            BankAccountNumber = string.Empty;
            Status = string.Empty;
            GeneratedBy = string.Empty;
            GeneratedOn = string.Empty;
            HODApprovedBy = string.Empty;
            HODApprovedOn = string.Empty;
            AccountsApprovedBy = string.Empty;
            AccountsApprovedOn = string.Empty;
            BAGeneratedBy = string.Empty;
            BAGeneratedOn = string.Empty;
            FACT_VoucherStatus = string.Empty;

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (dsVouchers.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsVouchers.Tables[0].Select("PaymentRunNumber='" + paymentRunNumber + "'"))
                {
                    if (dr["PaymentRunNumber"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PaymentRunNumber"])))
                        PaymentRunNumber = Convert.ToString(dr["PaymentRunNumber"]);

                    //if (dr["Unit"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Unit"])))
                    //    Unit = Convert.ToString(dr["Unit"]);

                    if (dr["RequestDate"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["RequestDate"])))
                        RequestDate = Convert.ToString(dr["RequestDate"]);

                    if (dr["VendorCode"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["VendorCode"])))
                        VendorCode = Convert.ToString(dr["VendorCode"]);

                    if (dr["VendorName"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["VendorName"])))
                        VendorName = Convert.ToString(dr["VendorName"]);

                    if (dr["BankName"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["BankName"])))
                        BankName = Convert.ToString(dr["BankName"]);

                    if (dr["Branch"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Branch"])))
                        Branch = Convert.ToString(dr["Branch"]);

                    if (dr["RTGS/IFSCcode"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["RTGS/IFSCcode"])))
                        RTGS_IFSCcode = Convert.ToString(dr["RTGS/IFSCcode"]);

                    if (dr["BankAccountNumber"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["BankAccountNumber"])))
                        BankAccountNumber = Convert.ToString(dr["BankAccountNumber"]);

                    if (dr["Status"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Status"])))
                        Status = Convert.ToString(dr["Status"]);

                    if (dr["FACT_VoucherStatus"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["FACT_VoucherStatus"])))
                        FACT_VoucherStatus = Convert.ToString(dr["FACT_VoucherStatus"]);
                }


                string src = string.Empty;
                //string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";

                if (vCount > 1)
                {
                    sb.Append("<div style='page-break-after:always;'></div>\n");
                }
                //<img src='../Images/COPERION/logo2.png'/>
                if (hiddenFlag == 0)
                {
                    sb.Append("<img src='{#src#}' style='height:50px;width:150px;'/>\n");
                }

                sb.Append("<h2 class='headerStyle'><u>ADVANCE VOUCHER HEADER</u></h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Unit:</td>\n");
                sb.Append("<td class='td2header'>{#Unit#}</td>\n");
                sb.Append("<td class='td1header'>PaymentRunNumber:</td>\n");
                sb.Append("<td class='td2header'>{#PaymentRunNumber#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Request Date:</td>\n");
                sb.Append("<td class='td2header'>{#RequestDate#}</td>\n");
                sb.Append("<td class='td1header'>Bank Name:</td>\n");
                sb.Append("<td class='td2header'>{#BankName#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Branch:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#Branch#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>RTGS/IFSC Code:</td>\n");
                sb.Append("<td class='td2header'>{#RTGS_IFSCcode#}</td>\n");
                sb.Append("<td class='td1header'>Bank Account Number:</td>\n");
                sb.Append("<td class='td2header'>{#BankAccountNumber#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Vendor Name:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#VendorName#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Status:</td>\n");
                sb.Append("<td class='td2header'>{#Status#}</td>\n");
                //sb.Append("<td class='td1header'>FACT Voucher Status:</td>\n");
                //sb.Append("<td class='td2header'>{#FACT_VoucherStatus#}</td>\n");
                sb.Append("</tr>\n");

                sb.Append("</table>\n");

                sb.Replace("{#src#}", imgPath);
                sb.Replace("{#PaymentRunNumber#}", PaymentRunNumber);
                sb.Replace("{#Unit#}", Unit);
                sb.Replace("{#RequestDate#}", RequestDate);
                sb.Replace("{#VendorCode#}", VendorCode);
                sb.Replace("{#VendorName#}", VendorName);
                sb.Replace("{#BankName#}", BankName);
                sb.Replace("{#Branch#}", Branch);
                sb.Replace("{#RTGS_IFSCcode#}", RTGS_IFSCcode);
                sb.Replace("{#BankAccountNumber#}", BankAccountNumber);
                sb.Replace("{#Status#}", Status);
                sb.Replace("{#FACT_VoucherStatus#}", FACT_VoucherStatus);

                sb.Append("<hr />\n");

                if (dsVouchers.Tables[1].Rows.Count > 0)
                {
                    sb.Append("<h3 class='headerStyle'><u>ADVANCE VOUCHER DETAIL</u></h3>\n");
                    sb.Append("<hr />\n");
                    sb.Append("<table class='tblsuitems'>\n");

                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Sr No.</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>FACT Voucher No.</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Order No.</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Order Date</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Project No.</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Payment Term1</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Payment Term2</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Payment Term3</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Payment Term4</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>PO Created By</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>PO Basic Value</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Requested Amount</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Approved Amount</th>\n");
                    sb.Append("</tr>\n");

                    foreach (DataRow dr in dsVouchers.Tables[1].Select("PaymentRunNumber='" + paymentRunNumber + "'"))
                    {
                        FACT_VoucherNumber = string.Empty;
                        OrderNumber = string.Empty;
                        OrderDate = string.Empty;
                        ProjectNumber = string.Empty;
                        PaymentTerm1 = string.Empty;
                        PaymentTerm2 = string.Empty;
                        PaymentTerm3 = string.Empty;
                        PaymentTerm4 = string.Empty;
                        POCreatedBy = string.Empty;
                        PO_BasicValue = string.Empty;
                        RequestedAmount = string.Empty;
                        Approved_Amount = string.Empty;

                        SrNo++;

                        if (dr["FACT_VoucherNumber"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["FACT_VoucherNumber"])))
                            FACT_VoucherNumber = Convert.ToString(dr["FACT_VoucherNumber"]);

                        if (dr["OrderNumber"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["OrderNumber"])))
                            OrderNumber = Convert.ToString(dr["OrderNumber"]);

                        if (dr["OrderDate"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["OrderDate"])))
                            OrderDate = Convert.ToString(dr["OrderDate"]);

                        if (dr["ProjectNumber"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["ProjectNumber"])))
                            ProjectNumber = Convert.ToString(dr["ProjectNumber"]);

                        if (dr["PaymentTerm1"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PaymentTerm1"])))
                            PaymentTerm1 = Convert.ToString(dr["PaymentTerm1"]);

                        if (dr["PaymentTerm2"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PaymentTerm2"])))
                            PaymentTerm2 = Convert.ToString(dr["PaymentTerm2"]);

                        if (dr["PaymentTerm3"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PaymentTerm3"])))
                            PaymentTerm3 = Convert.ToString(dr["PaymentTerm3"]);

                        if (dr["PaymentTerm4"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PaymentTerm4"])))
                            PaymentTerm4 = Convert.ToString(dr["PaymentTerm4"]);

                        if (dr["POCreatedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["POCreatedBy"])))
                            POCreatedBy = Convert.ToString(dr["POCreatedBy"]);

                        if (dr["PO_BasicValue"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PO_BasicValue"])))
                            PO_BasicValue = Convert.ToString(dr["PO_BasicValue"]);

                        if (dr["RequestedAmount"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["RequestedAmount"])))
                            RequestedAmount = Convert.ToString(dr["RequestedAmount"]);

                        if (dr["Approved_Amount"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Approved_Amount"])))
                            Approved_Amount = Convert.ToString(dr["Approved_Amount"]);


                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#SrNo#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#FACT_VoucherNumber#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#OrderNumber#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#OrderDate#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#ProjectNumber#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#PaymentTerm1#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#PaymentTerm2#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#PaymentTerm3#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#PaymentTerm4#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#POCreatedBy#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#PO_BasicValue#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#RequestedAmount#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#Approved_Amount#}</td>\n");


                        sb.Append("</tr>\n");

                        sb.Replace("{#SrNo#}", Convert.ToString(SrNo));
                        sb.Replace("{#FACT_VoucherNumber#}", FACT_VoucherNumber);
                        sb.Replace("{#OrderNumber#}", OrderNumber);
                        sb.Replace("{#OrderDate#}", OrderDate);
                        sb.Replace("{#ProjectNumber#}", ProjectNumber);
                        sb.Replace("{#PaymentTerm1#}", PaymentTerm1);
                        sb.Replace("{#PaymentTerm2#}", PaymentTerm2);
                        sb.Replace("{#PaymentTerm3#}", PaymentTerm3);
                        sb.Replace("{#PaymentTerm4#}", PaymentTerm4);
                        sb.Replace("{#POCreatedBy#}", POCreatedBy);
                        sb.Replace("{#PO_BasicValue#}", PO_BasicValue);
                        sb.Replace("{#RequestedAmount#}", RequestedAmount);
                        sb.Replace("{#Approved_Amount#}", Approved_Amount);

                    }
                }

                sb.Append("</table>\n");


                foreach (DataRow dr in dsVouchers.Tables[0].Select("PaymentRunNumber='" + paymentRunNumber + "'"))
                {
                    if (dr["GeneratedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["GeneratedBy"])))
                        GeneratedBy = Convert.ToString(dr["GeneratedBy"]);

                    if (dr["GeneratedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["GeneratedOn"])))
                        GeneratedOn = Convert.ToString(dr["GeneratedOn"]);

                    if (dr["HODApprovedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["HODApprovedBy"])))
                        HODApprovedBy = Convert.ToString(dr["HODApprovedBy"]);

                    if (dr["HODApprovedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["HODApprovedOn"])))
                        HODApprovedOn = Convert.ToString(dr["HODApprovedOn"]);

                    if (dr["AccountsApprovedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AccountsApprovedBy"])))
                        AccountsApprovedBy = Convert.ToString(dr["AccountsApprovedBy"]);

                    if (dr["AccountsApprovedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AccountsApprovedOn"])))
                        AccountsApprovedOn = Convert.ToString(dr["AccountsApprovedOn"]);

                    if (dr["BAGeneratedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["BAGeneratedBy"])))
                        BAGeneratedBy = Convert.ToString(dr["BAGeneratedBy"]);

                    if (dr["BAGeneratedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["BAGeneratedOn"])))
                        BAGeneratedOn = Convert.ToString(dr["BAGeneratedOn"]);


                    sb.Append("<h3 class='headerStyle'><u>SIGNATORIES</u></h3>\n");
                    sb.Append("<hr />\n");

                    sb.Append("<table class='tblsuitems'>\n");

                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Generated By (On)</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>HOD (On)</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Accounts Approved By (On)</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>BA Generated By (On)</th>\n");
                    sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#GeneratedBy#}</td>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#HODApprovedBy#}</td>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#AccountsApprovedBy#}</td>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#BAGeneratedBy#}</td>\n");
                    sb.Append("</tr>\n");

                    //sb.Append("<tr>\n");
                    //sb.Append("<td class='tdpaymentvoucher' style='width:50px;'></td>\n");
                    //sb.Append("<td class='tdpaymentvoucher' style='width:50px;'></td>\n");
                    //sb.Append("<td class='tdpaymentvoucher' style='width:50px;'></td>\n");
                    //sb.Append("<td class='tdpaymentvoucher' style='width:50px; text-align:right'>.</td>\n");
                    //sb.Append("</tr>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#GeneratedOn#}</td>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#HODApprovedOn#}</td>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#AccountsApprovedOn#}</td>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#BAGeneratedOn#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("</table>\n");

                    sb.Append("<hr />\n");
                    sb.Append("<P class='header2'><u>This is an electronically generated voucher, hence does not require a signature</u></P>\n");

                    sb.Replace("{#GeneratedBy#}", GeneratedBy);
                    sb.Replace("{#GeneratedOn#}", GeneratedOn);
                    sb.Replace("{#HODApprovedBy#}", HODApprovedBy);
                    sb.Replace("{#HODApprovedOn#}", HODApprovedOn);
                    sb.Replace("{#AccountsApprovedBy#}", AccountsApprovedBy);
                    sb.Replace("{#AccountsApprovedOn#}", AccountsApprovedOn);
                    sb.Replace("{#BAGeneratedBy#}", BAGeneratedBy);
                    sb.Replace("{#BAGeneratedOn#}", BAGeneratedOn);
                }
            }
            htmlText = sb.ToString();
            return htmlText;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}