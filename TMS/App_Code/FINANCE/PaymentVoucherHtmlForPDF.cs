using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Net.Mime;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;
using System.Xml;
using iTextSharp.tool.xml.css;
using System.Data;

/// <summary>
/// Summary description for LOTHtmlForPDF
/// </summary>
public class PaymentVoucherHtmlForPDF
{

    int SrNo = 0;
    string Unit = string.Empty;
    string RequestDate = string.Empty;
    string VendorName = string.Empty;
    string BankName = string.Empty;
    string Branch = string.Empty;
    string RTGS_IFSCcode = string.Empty;
    string BankAccountNumber = string.Empty;

    string InvoiceNumber = string.Empty;
    string InvoiceDate = string.Empty;
    string InvoiceDueDate = string.Empty;
    string PartyBillNumber = string.Empty;
    string OrderNumber = string.Empty;
    string OrderDate = string.Empty;
    string InvoiceNetAmount = string.Empty;
    string OutstandingAmount = string.Empty;
    string PaymentAmount = string.Empty;
    string Approved_Amount = string.Empty;
    string Status = string.Empty;
    string FACT_VoucherStatus = string.Empty;
    string GeneratedBy = string.Empty;
    string GeneratedOn = string.Empty;
    string HOD = string.Empty;
    string HOD_ApprovedOn = string.Empty;
    string AccountsApprovedBy = string.Empty;
    string Accounts_ApprovedOn = string.Empty;
    string BAGeneratedBy = string.Empty;
    string Date_BAGenerated = string.Empty;

    public PaymentVoucherHtmlForPDF()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetHtmlForPDF(DataSet dsVouchers, string factVoucherNumber, int vCount, string imgPath, int hiddenFlag)
    {
        try
        {
            SrNo = 0;
            Unit = string.Empty;
            RequestDate = string.Empty;
            VendorName = string.Empty;
            BankName = string.Empty;
            Branch = string.Empty;
            RTGS_IFSCcode = string.Empty;
            BankAccountNumber = string.Empty;

            GeneratedBy = string.Empty;
            GeneratedOn = string.Empty;
            HOD = string.Empty;
            HOD_ApprovedOn = string.Empty;
            AccountsApprovedBy = string.Empty;
            Accounts_ApprovedOn = string.Empty;
            BAGeneratedBy = string.Empty;
            Date_BAGenerated = string.Empty;

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (dsVouchers.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsVouchers.Tables[0].Select("FACT_VoucherNumber='" + factVoucherNumber + "'"))
                {
                    //if (dr["Unit"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Unit"])))
                    //    Unit = Convert.ToString(dr["Unit"]);

                    if (dr["RequestDate"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["RequestDate"])))
                        RequestDate = Convert.ToString(dr["RequestDate"]);

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

                    if (dr["GeneratedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["GeneratedBy"])))
                        GeneratedBy = Convert.ToString(dr["GeneratedBy"]);

                    if (dr["GeneratedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["GeneratedOn"])))
                        GeneratedOn = Convert.ToString(dr["GeneratedOn"]);

                    if (dr["HOD"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["HOD"])))
                        HOD = Convert.ToString(dr["HOD"]);

                    if (dr["HOD_ApprovedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["HOD_ApprovedOn"])))
                        HOD_ApprovedOn = Convert.ToString(dr["HOD_ApprovedOn"]);

                    if (dr["AccountsApprovedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AccountsApprovedBy"])))
                        AccountsApprovedBy = Convert.ToString(dr["AccountsApprovedBy"]);

                    if (dr["Accounts_ApprovedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Accounts_ApprovedOn"])))
                        Accounts_ApprovedOn = Convert.ToString(dr["Accounts_ApprovedOn"]);

                    if (dr["BAGeneratedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["BAGeneratedBy"])))
                        BAGeneratedBy = Convert.ToString(dr["BAGeneratedBy"]);

                    if (dr["Date_BAGenerated"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Date_BAGenerated"])))
                        Date_BAGenerated = Convert.ToString(dr["Date_BAGenerated"]);
                }

                if (vCount > 1)
                {
                    sb.Append("<div style='page-break-after:always;'></div>\n");
                }
                //<img src='../Images/COPERION/logo2.png'/>
                if (hiddenFlag == 0)
                {
                    sb.Append("<img src='{#src#}' style='height:50px;width:150px;'/>\n");
                }

                sb.Append("<h2 class='headerStyle'><u>VOUCHER HEADER</u></h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Unit:</td>\n");
                sb.Append("<td class='td2header'>{#Unit#}</td>\n");
                sb.Append("<td class='td1header'>Fact Voucher Number:</td>\n");
                sb.Append("<td class='td2header'>{#FACT_VoucherNumber#}</td>\n");
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
                sb.Append("</table>\n");

                sb.Replace("{#src#}", imgPath);
                sb.Replace("{#Unit#}", Unit);
                sb.Replace("{#FACT_VoucherNumber#}", factVoucherNumber);
                sb.Replace("{#RequestDate#}", RequestDate);
                sb.Replace("{#VendorName#}", VendorName);
                sb.Replace("{#BankName#}", BankName);
                sb.Replace("{#Branch#}", Branch);
                sb.Replace("{#RTGS_IFSCcode#}", RTGS_IFSCcode);
                sb.Replace("{#BankAccountNumber#}", BankAccountNumber);

                sb.Append("<hr />\n");

                if (dsVouchers.Tables[1].Rows.Count > 0)
                {
                    sb.Append("<h3 class='headerStyle'><u>VOUCHER DETAIL</u></h3>\n");
                    sb.Append("<hr />\n");
                    sb.Append("<table class='tblsuitems'>\n");

                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Sr No.</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Invoice No.</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Invoice Date</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Invoice Due Date</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Party Bill No.</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Order No.</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Order Date</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Invoice Net Amount</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Outstanding Amount</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Payment Amount</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Approved Amount</th>\n");
                    sb.Append("<th class='tdpaymentvoucher'>Status</th>\n");
                    //sb.Append("<th class='tdpaymentvoucher'>FACT Voucher Status</th>\n");
                    sb.Append("</tr>\n");

                    foreach (DataRow dr in dsVouchers.Tables[1].Select("FACT_VoucherNumber='" + factVoucherNumber + "'"))
                    {
                        InvoiceNumber = string.Empty;
                        InvoiceDate = string.Empty;
                        InvoiceDueDate = string.Empty;
                        PartyBillNumber = string.Empty;
                        OrderNumber = string.Empty;
                        OrderDate = string.Empty;
                        InvoiceNetAmount = string.Empty;
                        OutstandingAmount = string.Empty;
                        PaymentAmount = string.Empty;
                        Approved_Amount = string.Empty;
                        Status = string.Empty;
                        FACT_VoucherStatus = string.Empty;

                        SrNo++;

                        if (dr["InvoiceNumber"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["InvoiceNumber"])))
                            InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);

                        if (dr["InvoiceDate"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["InvoiceDate"])))
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]);

                        if (dr["InvoiceDueDate"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["InvoiceDueDate"])))
                            InvoiceDueDate = Convert.ToString(dr["InvoiceDueDate"]);

                        if (dr["PartyBillNumber"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PartyBillNumber"])))
                            PartyBillNumber = Convert.ToString(dr["PartyBillNumber"]);

                        if (dr["OrderNumber"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["OrderNumber"])))
                            OrderNumber = Convert.ToString(dr["OrderNumber"]);

                        if (dr["OrderDate"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["OrderDate"])))
                            OrderDate = Convert.ToString(dr["OrderDate"]);

                        if (dr["InvoiceNetAmount"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["InvoiceNetAmount"])))
                            InvoiceNetAmount = Convert.ToString(dr["InvoiceNetAmount"]);

                        if (dr["OutstandingAmount"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["OutstandingAmount"])))
                            OutstandingAmount = Convert.ToString(dr["OutstandingAmount"]);

                        if (dr["PaymentAmount"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PaymentAmount"])))
                            PaymentAmount = Convert.ToString(dr["PaymentAmount"]);

                        if (dr["Approved_Amount"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Approved_Amount"])))
                            Approved_Amount = Convert.ToString(dr["Approved_Amount"]);

                        if (dr["Status"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Status"])))
                            Status = Convert.ToString(dr["Status"]);

                        //if (dr["FACT_VoucherStatus"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["FACT_VoucherStatus"])))
                        //    FACT_VoucherStatus = Convert.ToString(dr["FACT_VoucherStatus"]);


                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#SrNo#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#InvoiceNumber#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#InvoiceDate#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#InvoiceDueDate#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#PartyBillNumber#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#OrderNumber#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#OrderDate#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#InvoiceNetAmount#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#OutstandingAmount#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#PaymentAmount#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#Approved_Amount#}</td>\n");
                        sb.Append("<td class='tdpaymentvoucher'>{#Status#}</td>\n");
                        //sb.Append("<td class='tdpaymentvoucher'>{#FACT_VoucherStatus#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#SrNo#}", Convert.ToString(SrNo));
                        sb.Replace("{#InvoiceNumber#}", InvoiceNumber);
                        sb.Replace("{#InvoiceDate#}", InvoiceDate);
                        sb.Replace("{#InvoiceDueDate#}", InvoiceDueDate);
                        sb.Replace("{#PartyBillNumber#}", PartyBillNumber);
                        sb.Replace("{#OrderNumber#}", OrderNumber);
                        sb.Replace("{#OrderDate#}", OrderDate);
                        sb.Replace("{#InvoiceNetAmount#}", InvoiceNetAmount);
                        sb.Replace("{#OutstandingAmount#}", OutstandingAmount);
                        sb.Replace("{#PaymentAmount#}", PaymentAmount);
                        sb.Replace("{#Approved_Amount#}", Approved_Amount);
                        sb.Replace("{#Status#}", Status);
                        sb.Replace("{#FACT_VoucherStatus#}", FACT_VoucherStatus);
                    }
                }

                sb.Append("</table>\n");


                foreach (DataRow dr in dsVouchers.Tables[0].Select("FACT_VoucherNumber='" + factVoucherNumber + "'"))
                {


                    if (dr["GeneratedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["GeneratedBy"])))
                        GeneratedBy = Convert.ToString(dr["GeneratedBy"]);

                    if (dr["GeneratedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["GeneratedOn"])))
                        GeneratedOn = Convert.ToString(dr["GeneratedOn"]);

                    if (dr["HOD"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["HOD"])))
                        HOD = Convert.ToString(dr["HOD"]);

                    if (dr["HOD_ApprovedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["HOD_ApprovedOn"])))
                        HOD_ApprovedOn = Convert.ToString(dr["HOD_ApprovedOn"]);

                    if (dr["AccountsApprovedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["AccountsApprovedBy"])))
                        AccountsApprovedBy = Convert.ToString(dr["AccountsApprovedBy"]);

                    if (dr["Accounts_ApprovedOn"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Accounts_ApprovedOn"])))
                        Accounts_ApprovedOn = Convert.ToString(dr["Accounts_ApprovedOn"]);

                    if (dr["BAGeneratedBy"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["BAGeneratedBy"])))
                        BAGeneratedBy = Convert.ToString(dr["BAGeneratedBy"]);

                    if (dr["Date_BAGenerated"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["Date_BAGenerated"])))
                        Date_BAGenerated = Convert.ToString(dr["Date_BAGenerated"]);


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
                    sb.Append("<td class='tdpaymentvoucher'>{#HOD#}</td>\n");
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
                    sb.Append("<td class='tdpaymentvoucher'>{#HOD_ApprovedOn#}</td>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#Accounts_ApprovedOn#}</td>\n");
                    sb.Append("<td class='tdpaymentvoucher'>{#Date_BAGenerated#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Append("</table>\n");

                    sb.Append("<hr />\n");
                    sb.Append("<P class='header2'><u>This is an electronically generated voucher, hence does not require a signature</u></P>\n");

                                      
                    sb.Replace("{#GeneratedBy#}", GeneratedBy);
                    sb.Replace("{#GeneratedOn#}", GeneratedOn);
                    sb.Replace("{#HOD#}", HOD);
                    sb.Replace("{#HOD_ApprovedOn#}", HOD_ApprovedOn);
                    sb.Replace("{#AccountsApprovedBy#}", AccountsApprovedBy);
                    sb.Replace("{#Accounts_ApprovedOn#}", Accounts_ApprovedOn);
                    sb.Replace("{#BAGeneratedBy#}", BAGeneratedBy);
                    sb.Replace("{#Date_BAGenerated#}", Date_BAGenerated);
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