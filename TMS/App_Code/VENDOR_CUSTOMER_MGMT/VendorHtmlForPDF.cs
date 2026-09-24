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


public class VendorHtmlForPDF
{


    #region Variables

    string vendorName = string.Empty;
    string vendorCode = string.Empty;
    string status = string.Empty;
    string category = string.Empty;
    string MSMEStatus = string.Empty;
    //string gstin = string.Empty;
    string pan = string.Empty;
    string creditDays = string.Empty;
    string creditLimit = string.Empty;
    string isAssociated = string.Empty;
    string isIsoCertified = string.Empty;
    string isTechnicalDetailsReceived = string.Empty;
    string isVisitByQa = string.Empty;
    string isGovernment = string.Empty;
    string isOneTime = string.Empty;
    string responsible = string.Empty;
    string vendorType = string.Empty;
    string itemCategory = string.Empty;
    string itemSubcategory = string.Empty;
    private string Address1;
    private string Address2;
    private string Address3;
    private string State;
    private string Email;
    private string City;
    private string Country;
    private string Phone;
    private string IsDefault;

    #endregion


    public string GetHtmlForPDF(int vendorId, DataSet dsDetails)
    {
        try
        {
            vendorName = string.Empty;
            vendorCode = string.Empty;
            status = string.Empty;
            category = string.Empty;
            MSMEStatus = string.Empty;
            //gstin = string.Empty;
            pan = string.Empty;
            creditDays = string.Empty;
            creditLimit = string.Empty;
            isAssociated = string.Empty;
            isIsoCertified = string.Empty;
            isTechnicalDetailsReceived = string.Empty;
            isVisitByQa = string.Empty;
            isGovernment = string.Empty;
            responsible = string.Empty;
            vendorType = string.Empty;
            itemCategory = string.Empty;
            itemSubcategory = string.Empty;

            DataTable dtVendorDetails = new DataTable();
            DataTable dtBillingAddress = new DataTable();
            DataTable dtContactPerson = new DataTable();
            DataTable dtBankDetails = new DataTable();
            DataTable dtSignatorys = new DataTable();
            DataTable dtRevisions = new DataTable();
            //DataTable dtDeclaration = new DataTable();

            if (dsDetails.Tables[0].Rows.Count > 0 && dsDetails.Tables[0] != null)
                dtVendorDetails = dsDetails.Tables[0];

            if (dsDetails.Tables[1].Rows.Count > 0 && dsDetails.Tables[1] != null)
                dtBillingAddress = dsDetails.Tables[1];

            if (dsDetails.Tables[2].Rows.Count > 0 && dsDetails.Tables[2] != null)
                dtContactPerson = dsDetails.Tables[2];

            if (dsDetails.Tables[3].Rows.Count > 0 && dsDetails.Tables[3] != null)
                dtBankDetails = dsDetails.Tables[3];

            if (dsDetails.Tables[4].Rows.Count > 0 && dsDetails.Tables[4] != null)
                dtSignatorys = dsDetails.Tables[4];

            //if (dsDetails.Tables[5].Rows.Count > 0 && dsDetails.Tables[5] != null)
            //    dtDeclaration = dsDetails.Tables[5];

            if (dsDetails.Tables[6].Rows.Count > 0 && dsDetails.Tables[6] != null)
                dtRevisions = dsDetails.Tables[6];


            if (dtVendorDetails.Rows.Count > 0)
            {
                DataRow dr0 = dtVendorDetails.Rows[0];

                if (dr0["NAME"] != DBNull.Value) vendorName = Convert.ToString(dr0["NAME"]).Trim();
                if (dr0["CODE"] != DBNull.Value) vendorCode = Convert.ToString(dr0["CODE"]).Trim();
                if (dr0["STATUS"] != DBNull.Value) status = Convert.ToString(dr0["STATUS"]).Trim();
                if (dr0["CATEGORY"] != DBNull.Value) category = Convert.ToString(dr0["CATEGORY"]).Trim();
                if (dr0["MSME_STATUS"] != DBNull.Value) MSMEStatus = Convert.ToString(dr0["MSME_STATUS"]).Trim();
                //if (dr0["GSTIN"] != DBNull.Value) gstin = Convert.ToString(dr0["GSTIN"]).Trim();
                if (dr0["PAN"] != DBNull.Value) pan = Convert.ToString(dr0["PAN"]).Trim();
                if (dr0["CREDIT_DAYS"] != DBNull.Value) creditDays = Convert.ToString(dr0["CREDIT_DAYS"]).Trim();
                if (dr0["CREDIT_LIMIT"] != DBNull.Value) creditLimit = Convert.ToString(dr0["CREDIT_LIMIT"]).Trim();
                if (dr0["IS_ASSOCIATED"] != DBNull.Value) isAssociated = Convert.ToString(dr0["IS_ASSOCIATED"]).Trim();
                if (dr0["IS_ISO_CERTIFIED"] != DBNull.Value) isIsoCertified = Convert.ToString(dr0["IS_ISO_CERTIFIED"]).Trim();
                if (dr0["IS_TECHNICAL_DETAILS_RECEIVED"] != DBNull.Value) isTechnicalDetailsReceived = Convert.ToString(dr0["IS_TECHNICAL_DETAILS_RECEIVED"]).Trim();
                if (dr0["IS_VISIT_BY_QA"] != DBNull.Value) isVisitByQa = Convert.ToString(dr0["IS_VISIT_BY_QA"]).Trim();
                if (dr0["IS_GOVERNMENT"] != DBNull.Value) isGovernment = Convert.ToString(dr0["IS_GOVERNMENT"]).Trim();
                if (dr0["IS_ONE_TIME"] != DBNull.Value) isOneTime = Convert.ToString(dr0["IS_ONE_TIME"]).Trim();
                if (dr0["RESPONSIBLE"] != DBNull.Value) responsible = Convert.ToString(dr0["RESPONSIBLE"]).Trim();
                if (dr0["VENDOR_TYPE"] != DBNull.Value) vendorType = Convert.ToString(dr0["VENDOR_TYPE"]).Trim();
                if (dr0["ITEM_CATEGORY"] != DBNull.Value) itemCategory = Convert.ToString(dr0["ITEM_CATEGORY"]).Trim();
                if (dr0["ITEM_SUBCATEGORY"] != DBNull.Value) itemSubcategory = Convert.ToString(dr0["ITEM_SUBCATEGORY"]).Trim();

            }

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            string applicationStatus = "[New]";
            string revisionFor = "";
            if (dtRevisions != null && dtRevisions.Rows.Count > 0)
            {
                applicationStatus = "[Revised]";

                foreach (DataRow dr in dtRevisions.Rows)
                {
                    revisionFor += dr["NAME"] + " | ";
                }

                revisionFor = revisionFor.Trim().TrimEnd('|');
            }
                
            

            sb.Append("<h2 class='headerStyle'>VENDOR FORM " + applicationStatus + "</h2>\n");

            sb.Append("<hr />\n");
            sb.Append("<table class='tblheader'>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Vendor Name:</td>\n");
            sb.Append("<td class='td2header'><b>{#vendorName#}</b></td>\n");

            sb.Append("<td class='td1header'>Category</td>\n");
            sb.Append("<td class='td2header'>{#category#}</td>\n");

            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>PAN:</td>\n");
            sb.Append("<td class='td2header'><b>{#pan#}</b></td>\n");

            //sb.Append("<td class='td1header'>GSTIn:</td>\n");
            //sb.Append("<td class='td2header'><b>{#gstin#}</b></td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");
            sb.Append("<td class='td1header'>Credit Days:</td>\n");
            sb.Append("<td class='td2header'>{#creditDays#}</td>\n");

            sb.Append("<td class='td1header'>Credit Limit:</td>\n");
            sb.Append("<td class='td2header'>{#creditLimit#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("<tr>\n");

            sb.Append("<td class='td1header'>MSME Status:</td>\n");
            sb.Append("<td class='td2header'>{#MSMEStatus#}</td>\n");

            sb.Append("<td class='td1header'>Is IsoCertified?</td>\n");
            sb.Append("<td class='td2header'>{#isIsoCertified#}</td>\n");

            sb.Append("</tr>\n");

            sb.Append("<tr>\n");

            sb.Append("<td class='td1header'>Is TechnicalDetailsReceived?</td>\n");
            sb.Append("<td class='td2header'>{#isTechnicalDetailsReceived#}</td>\n");

            sb.Append("<td class='td1header'>Is VisitByQa?</td>\n");
            sb.Append("<td class='td2header'>{#isVisitByQa#}</td>\n");

            sb.Append("</tr>\n");

            sb.Append("<tr>\n");

            sb.Append("<td class='td1header'>Responsible:</td>\n");
            sb.Append("<td class='td2header'>{#responsible#}</td>\n");

            sb.Append("<td class='td1header'>Vendor Type:</td>\n");
            sb.Append("<td class='td2header'>{#vendorType#}</td>\n");

            sb.Append("</tr>\n");

            sb.Append("<tr>\n");

            sb.Append("<td class='td1header'>Item Category:</td>\n");
            sb.Append("<td class='td2header'>{#itemCategory#}</td>\n");

            sb.Append("<td class='td1header'>Item Subcategory:</td>\n");
            sb.Append("<td class='td2header'>{#itemSubcategory#}</td>\n");

            sb.Append("</tr>\n");


            sb.Append("<tr>\n");

            sb.Append("<td class='td1header'>Is Government?</td>\n");
            sb.Append("<td class='td2header'>{#isGovernment#}</td>\n");

            sb.Append("<td class='td1header'>Is One Time Vendor?:</td>\n");
            sb.Append("<td class='td2header'>{#isOneTime#}</td>\n");

            sb.Append("</tr>\n");



            sb.Append("<tr>\n");

            sb.Append("<td class='td1header'>Current Status:</td>\n");
            sb.Append("<td class='td2header'>{#status#}</td>\n");

            sb.Append("</tr>\n");



            sb.Append("<tr>\n");

            sb.Append("<td class='td1header' colspan='2'>Do you have any relation work in our company as employee?</td>\n");
            sb.Append("<td class='td2header'>{#isAssociated#}</td>\n");
            sb.Append("</tr>\n");


            sb.Append("<tr>\n");

            sb.Append("<td class='td1header'>Revision For:</td>\n");
            sb.Append("<td class='td2header'  colspan='4'>{#revisionFor#}</td>\n");
            sb.Append("</tr>\n");

            sb.Append("</table>\n");
            sb.Append("<hr />\n");
            //}


            if (!string.IsNullOrEmpty(vendorCode))
                sb.Replace("{#vendorName#}", vendorName + " [" + vendorCode + "]");
            else
                sb.Replace("{#vendorName#}", vendorName);

            sb.Replace("{#category#}", category);
            sb.Replace("{#MSMEStatus#}", MSMEStatus);
            //sb.Replace("{#gstin#}", gstin);
            sb.Replace("{#pan#}", pan);
            sb.Replace("{#creditDays#}", creditDays);
            sb.Replace("{#creditLimit#}", creditLimit);
            sb.Replace("{#isAssociated#}", isAssociated);
            sb.Replace("{#isIsoCertified#}", isIsoCertified);
            sb.Replace("{#isTechnicalDetailsReceived#}", isTechnicalDetailsReceived);
            sb.Replace("{#isVisitByQa#}", isVisitByQa);
            sb.Replace("{#isGovernment#}", isGovernment);
            sb.Replace("{#isOneTime#}", isOneTime);
            sb.Replace("{#responsible#}", responsible);
            sb.Replace("{#vendorType#}", vendorType);

            sb.Replace("{#itemCategory#}", itemCategory);
            sb.Replace("{#itemSubcategory#}", itemSubcategory);

            sb.Replace("{#status#}", status);

            sb.Replace("{#revisionFor#}", revisionFor);


            //BILLING ADDRESS
            if (dtBillingAddress.Rows.Count > 0)
            {
                sb.Append("<h3 class='header2'>Billing Address</h3>\n");

                if (dtBillingAddress.Rows.Count > 1)
                {
                    sb.Append("<table class='tblsubitems'>\n");

                    sb.Append("<tr class='trsubitems'>\n");

                    sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
                    sb.Append("<th class='tdtag'>GSTIN</th>\n");
                    sb.Append("<th class='tddesc'>Address1</th>\n");
                    sb.Append("<th class='tddesc'>Address2</th>\n");
                    sb.Append("<th class='tddesc'>Address3</th>\n");
                    sb.Append("<th class='tddesc'>City</th>\n");
                    sb.Append("<th class='tddesc'>State</th>\n");
                    sb.Append("<th class='tddesc'>Country</th>\n");
                    sb.Append("<th class='tdtag'>PinCode</th>\n");
                    sb.Append("<th class='tdtag'>Phone</th>\n");
                    sb.Append("<th class='tdtag'>Email</th>\n");
                    sb.Append("<th class='tdtag'>Is Default?</th>\n");
                    sb.Append("</tr>\n");

                    foreach (DataRow dr in dtBillingAddress.Rows)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
                        sb.Append("<td class='tdtag'>{#GSTIN#}</td>\n");
                        sb.Append("<td class='tddesc'>{#Address1#}</td>\n");
                        sb.Append("<td class='tddesc'>{#Address2#}</td>\n");
                        sb.Append("<td class='tddesc'>{#Address3#}</td>\n");
                        sb.Append("<td class='tddesc'>{#City#}</td>\n");
                        sb.Append("<td class='tddesc'>{#State#}</td>\n");
                        sb.Append("<td class='tddesc'>{#Country#}</td>\n");
                        sb.Append("<td class='tdtag'>{#PinCode#}</td>\n");
                        sb.Append("<td class='tdtag'>{#Phone#}</td>\n");
                        sb.Append("<td class='tdtag'>{#Email#}</td>\n");
                        sb.Append("<td class='tdtag'>{#IsDefault#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#SRNo#}", Convert.ToString(dr["SR_NO"]));
                        sb.Replace("{#GSTIN#}", Convert.ToString(dr["GSTIN"]));
                        sb.Replace("{#Address1#}", Convert.ToString(dr["ADDRESS_LINE1"]));
                        sb.Replace("{#Address2#}", Convert.ToString(dr["ADDRESS_LINE2"]));
                        sb.Replace("{#Address3#}", Convert.ToString(dr["ADDRESS_LINE3"]));
                        sb.Replace("{#City#}", Convert.ToString(dr["CITY"]));
                        sb.Replace("{#State#}", Convert.ToString(dr["STATE"]));
                        sb.Replace("{#Country#}", Convert.ToString(dr["COUNTRY"]));
                        sb.Replace("{#PinCode#}", Convert.ToString(dr["PIN_CODE"]));
                        sb.Replace("{#Phone#}", Convert.ToString(dr["PHONE"]));
                        sb.Replace("{#Email#}", Convert.ToString(dr["EMAIL"]));

                        if (Convert.ToInt32(dr["IS_DEFAULT"]) > 0)
                            sb.Replace("{#IsDefault#}", "YES");
                        else sb.Replace("{#IsDefault#}", "NO");

                    }
                    sb.Append("</table>\n");
                    sb.Append("<hr />\n");
                }
                else
                {
                    sb.Append("<table class='tblheader'>\n");

                    sb.Append("<tr>\n");
                    sb.Append("<td class='td1header'>Address1:</td>\n");
                    sb.Append("<td class='td2header' colspan='3'><b>{#Address1#}</b></td>\n");

                    sb.Append("</tr>\n");


                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>Address2</td>\n");
                    sb.Append("<td class='td2header' colspan='3'>{#Address2#}</td>\n");

                    sb.Append("</tr>\n");


                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>Address3:</td>\n");
                    sb.Append("<td class='td2header' colspan='3'>{#Address3#}</td>\n");

                    sb.Append("</tr>\n");


                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>GSTIN:</td>\n");
                    sb.Append("<td class='td2header'><b>{#GSTIN#}</b></td>\n");

                    sb.Append("<td class='td1header'>State:</td>\n");
                    sb.Append("<td class='td2header'>{#State#}</td>\n");

                    sb.Append("</tr>\n");


                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>City:</td>\n");
                    sb.Append("<td class='td2header'>{#City#}</td>\n");

                    sb.Append("<td class='td1header'>Country:</td>\n");
                    sb.Append("<td class='td2header'>{#Country#}</td>\n");

                    sb.Append("</tr>\n");


                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>Phone:</td>\n");
                    sb.Append("<td class='td2header'>{#Phone#}</td>\n");

                    sb.Append("<td class='td1header'>Email</td>\n");
                    sb.Append("<td class='td2header'>{#Email#}</td>\n");

                    sb.Append("</tr>\n");

                    sb.Append("</table>\n");
                    sb.Append("<hr />\n");

                    DataRow dr = dtBillingAddress.Rows[0];

                    sb.Replace("{#Address1#}", Convert.ToString(dr["ADDRESS_LINE1"]));
                    sb.Replace("{#Address2#}", Convert.ToString(dr["ADDRESS_LINE2"]));
                    sb.Replace("{#Address3#}", Convert.ToString(dr["ADDRESS_LINE3"]));
                    sb.Replace("{#GSTIN#}", Convert.ToString(dr["GSTIN"]));
                    sb.Replace("{#City#}", Convert.ToString(dr["CITY"]));
                    sb.Replace("{#State#}", Convert.ToString(dr["STATE"]));
                    sb.Replace("{#Country#}", Convert.ToString(dr["COUNTRY"]));
                    sb.Replace("{#Phone#}", Convert.ToString(dr["PHONE"]));
                    sb.Replace("{#Email#}", Convert.ToString(dr["EMAIL"]));
                }
            }


            //CONTACT PERSON
            if (dtContactPerson != null && dtContactPerson.Rows.Count > 0)
            {
                sb.Append("<h3 class='header2'>Contact Person</h3>\n");

                if (dtContactPerson.Rows.Count > 1)
                {
                    sb.Append("<table class='tblsubitems'>\n");

                    sb.Append("<tr class='trsubitems'>\n");

                    sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
                    sb.Append("<th class='tddesc'>Name</th>\n");
                    sb.Append("<th class='tdtag'>Mobile</th>\n");
                    sb.Append("<th class='tdtag'>Phone</th>\n");
                    sb.Append("<th class='tdtag'>Email</th>\n");
                    sb.Append("<th class='tdtag'>Is Default?</th>\n");
                    sb.Append("</tr>\n");

                    foreach (DataRow dr in dtContactPerson.Rows)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
                        sb.Append("<td class='tddesc'>{#Name#}</td>\n");
                        sb.Append("<td class='tdtag'>{#Mobile#}</td>\n");
                        sb.Append("<td class='tdtag'>{#Phone#}</td>\n");
                        sb.Append("<td class='tdtag'>{#Email#}</td>\n");
                        sb.Append("<td class='tdtag'>{#IsDefault#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#SRNo#}", Convert.ToString(dr["SR_NO"]));
                        sb.Replace("{#Name#}", Convert.ToString(dr["NAME"]));
                        sb.Replace("{#Mobile#}", Convert.ToString(dr["MOBILE_NO"]));
                        sb.Replace("{#Phone#}", Convert.ToString(dr["PHONE"]));
                        sb.Replace("{#Email#}", Convert.ToString(dr["EMAIL"]));

                        if (Convert.ToInt32(dr["IS_DEFAULT"]) > 0)
                            sb.Replace("{#IsDefault#}", "YES");
                        else sb.Replace("{#IsDefault#}", "NO");

                    }
                    sb.Append("</table>\n");
                    sb.Append("<hr />\n");
                }
                else
                {
                    sb.Append("<table class='tblheader'>\n");

                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>Name:</td>\n");
                    sb.Append("<td class='td2header'><b>{#Name#}</b></td>\n");

                    sb.Append("<td class='td1header'>Mobile</td>\n");
                    sb.Append("<td class='td2header'>{#Mobile#}</td>\n");

                    sb.Append("</tr>\n");


                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>Phone:</td>\n");
                    sb.Append("<td class='td2header'>{#Phone#}</td>\n");

                    sb.Append("<td class='td1header'>Email:</td>\n");
                    sb.Append("<td class='td2header'>{#Email#}</td>\n");

                    sb.Append("</tr>\n");


                    sb.Append("</table>\n");
                    sb.Append("<hr />\n");

                    DataRow dr = dtContactPerson.Rows[0];

                    sb.Replace("{#Name#}", Convert.ToString(dr["NAME"]));
                    sb.Replace("{#Mobile#}", Convert.ToString(dr["MOBILE_NO"]));
                    sb.Replace("{#Phone#}", Convert.ToString(dr["PHONE"]));
                    sb.Replace("{#Email#}", Convert.ToString(dr["EMAIL"]));

                }
            }


            //BANK DETAILS
            if (dtBankDetails != null && dtBankDetails.Rows.Count > 0)
            {
                sb.Append("<h3 class='header2'>Bank Details</h3>\n");

                if (dtBankDetails.Rows.Count > 1)
                {
                    sb.Append("<table class='tblsubitems'>\n");

                    sb.Append("<tr class='trsubitems'>\n");

                    sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
                    sb.Append("<th class='tddesc'>Bank Name</th>\n");
                    sb.Append("<th class='tddesc'>Branch</th>\n");
                    sb.Append("<th class='tdtag'>SWIFT Code</th>\n");
                    sb.Append("<th class='tdtag'>Account No.</th>\n");
                    sb.Append("<th class='tdtag'>RTGS / IFSC</th>\n");
                    sb.Append("<th class='tdtag'>ISBN</th>\n");
                    sb.Append("<th class='tdtag'>Is Default?</th>\n");
                    sb.Append("</tr>\n");

                    foreach (DataRow dr in dtBankDetails.Rows)
                    {
                        sb.Append("<tr>\n");
                        sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
                        sb.Append("<td class='tddesc'>{#BankName#}</td>\n");
                        sb.Append("<td class='tddesc'>{#Branch#}</td>\n");
                        sb.Append("<td class='tdtag'>{#SWIFTCode#}</td>\n");
                        sb.Append("<td class='tdtag'>{#AccountNo#}</td>\n");
                        sb.Append("<td class='tdtag'>{#RTGSIFSC#}</td>\n");
                        sb.Append("<td class='tdtag'>{#ISBN#}</td>\n");
                        sb.Append("<td class='tdtag'>{#IsDefault#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#SRNo#}", Convert.ToString(dr["SR_NO"]));
                        sb.Replace("{#BankName#}", Convert.ToString(dr["BANK_NAME"]));
                        sb.Replace("{#Branch#}", Convert.ToString(dr["BRANCH"]));
                        sb.Replace("{#SWIFTCode#}", Convert.ToString(dr["SWIFT_CODE"]));
                        sb.Replace("{#AccountNo#}", Convert.ToString(dr["ACCOUNT_NUMBER"]));
                        sb.Replace("{#RTGSIFSC#}", Convert.ToString(dr["RTGS_OR_IFSC_CODE"]));
                        sb.Replace("{#ISBN#}", Convert.ToString(dr["ISBN"]));

                        if (Convert.ToInt32(dr["IS_DEFAULT"]) > 0)
                            sb.Replace("{#IsDefault#}", "YES");
                        else sb.Replace("{#IsDefault#}", "NO");

                    }
                    sb.Append("</table>\n");
                    //sb.Append("<hr />\n");
                }
                else
                {
                    sb.Append("<table class='tblheader'>\n");

                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>Bank Name:</td>\n");
                    sb.Append("<td class='td2header'><b>{#BankName#}</b></td>\n");

                    sb.Append("<td class='td1header'>Branch</td>\n");
                    sb.Append("<td class='td2header'>{#Branch#}</td>\n");

                    sb.Append("</tr>\n");



                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>Account No.:</td>\n");
                    sb.Append("<td class='td2header'>{#AccountNo#}</td>\n");

                    sb.Append("<td class='td1header'>RTGS / IFSC:</td>\n");
                    sb.Append("<td class='td2header'>{#RTGSIFSC#}</td>\n");

                    sb.Append("</tr>\n");



                    sb.Append("<tr>\n");

                    sb.Append("<td class='td1header'>SWIFT Code:</td>\n");
                    sb.Append("<td class='td2header'>{#SWIFTCode#}</td>\n");

                    sb.Append("<td class='td1header'>ISBN:</td>\n");
                    sb.Append("<td class='td2header'>{#ISBN#}</td>\n");

                    sb.Append("</tr>\n");

                    sb.Append("</table>\n");
                    //sb.Append("<hr />\n");

                    DataRow dr = dtBankDetails.Rows[0];

                    sb.Replace("{#BankName#}", Convert.ToString(dr["BANK_NAME"]));
                    sb.Replace("{#Branch#}", Convert.ToString(dr["BRANCH"]));
                    sb.Replace("{#SWIFTCode#}", Convert.ToString(dr["SWIFT_CODE"]));
                    sb.Replace("{#AccountNo#}", Convert.ToString(dr["ACCOUNT_NUMBER"]));
                    sb.Replace("{#RTGSIFSC#}", Convert.ToString(dr["RTGS_OR_IFSC_CODE"]));
                    sb.Replace("{#ISBN#}", Convert.ToString(dr["ISBN"]));

                }
            }


            //SIGNATORIES DETAILS
            if (dtSignatorys != null && dtSignatorys.Rows.Count > 0)
            {
                sb.Append("<h3 class='header2'>Signatorys</h3>\n");

                sb.Append("<table class='tblsubitems'>\n");

                sb.Append("<tr class='trsubitems'>\n");

                sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
                sb.Append("<th class='tdtag'>Status</th>\n");
                sb.Append("<th class='tddesc'>Action By</th>\n");
                sb.Append("<th class='tddesc'>Action On</th>\n");
                sb.Append("<th class='tddesc'>Remarks</th>\n");
                sb.Append("</tr>\n");

                foreach (DataRow dr in dtSignatorys.Rows)
                {
                    sb.Append("<tr>\n");
                    sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
                    sb.Append("<td class='tdtag'>{#Status#}</td>\n");
                    sb.Append("<td class='tddesc'>{#ActionBy#}</td>\n");
                    sb.Append("<td class='tddesc'>{#ActionOn#}</td>\n");
                    sb.Append("<td class='tddesc'>{#Remarks#}</td>\n");
                    sb.Append("</tr>\n");

                    sb.Replace("{#SRNo#}", Convert.ToString(dr["SR_NO"]));
                    sb.Replace("{#Status#}", Convert.ToString(dr["STATUS"]));
                    sb.Replace("{#ActionBy#}", Convert.ToString(dr["ACTION_BY"]));
                    sb.Replace("{#ActionOn#}", Convert.ToString(dr["ACTION_ON"]));
                    sb.Replace("{#Remarks#}", Convert.ToString(dr["REMARKS"]));
                }
                sb.Append("</table>\n");
                sb.Append("<hr />\n");

            }



            //if (dtDeclaration != null && dtDeclaration.Rows.Count > 0)
            //{
            //    DataRow drD = dtDeclaration.Rows[0];

            //    sb.Append("<hr />\n");
            //    sb.Append("<h3 class='header2'>Declaration in case of change in Bank account  or Contact details</h3>\n");

            //    sb.Append("<table class='tblheader'>\n");


            //    sb.Append("<tr>\n");

            //    sb.Append("<td class='td1header'>Declaration:</td>\n");
            //    sb.Append("<td class='td2header' colspan='3'>{#declaration#}</td>\n");

            //    sb.Append("</tr>\n");




            //    sb.Append("<tr>\n");

            //    sb.Append("<td class='td1header'>Contact Person:</td>\n");
            //    sb.Append("<td class='td2header'>{#contactPerson#}</td>\n");

            //    sb.Append("<td class='td1header'>Contact No.</td>\n");
            //    sb.Append("<td class='td2header'>{#contactNo#}</td>\n");

            //    sb.Append("</tr>\n");


            //    sb.Append("<tr>\n");

            //    sb.Append("<td class='td1header'>Contact Date:</td>\n");
            //    sb.Append("<td class='td2header'>{#contactDate#}</td>\n");

            //    sb.Append("<td class='td1header'>Contacted By</td>\n");
            //    sb.Append("<td class='td2header'>{#contactedBy#}</td>\n");

            //    sb.Append("</tr>\n");


            //    sb.Append("</table>\n");

            //    sb.Replace("{#declaration#}", Convert.ToString(drD["DECLARATION"]));
            //    sb.Replace("{#contactPerson#}", Convert.ToString(drD["CONTACT_PERSON"]));
            //    sb.Replace("{#contactNo#}", Convert.ToString(drD["CONTACT_NO"]));
            //    sb.Replace("{#contactDate#}", Convert.ToString(drD["CONTACT_DATE"]));
            //    sb.Replace("{#contactedBy#}", Convert.ToString(drD["CONTACTED_BY"]));
            //}








            //if (vendorId > 0)
            //{
            //    if (dtSignatorys != null && dtSignatorys.Rows.Count > 0)
            //    {

            //        sb.Append("<hr class='hrsignatories' />\n");
            //        //sb.Append("<fieldset class='pdffieldset'>\n");
            //        //sb.Append("<legend class='pdflegend'>Requested</legend>\n");
            //        //sb.Append("<table class='tblsignatories'>\n");


            //        DataRow[] drCre = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.Created_1);
            //        DataRow[] drRev = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.Revised_9);
            //        DataRow[] drAm = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.Amendment_2);
            //        DataRow[] drAmd = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.Amended_3);
            //        DataRow[] drChk = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.Checked_4);
            //        DataRow[] drProcApp = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.PROCHODApproved_5);
            //        DataRow[] drAccChk = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.AccountsChecked_6);
            //        DataRow[] drFinApp = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.FinalApproved_7);
            //        DataRow[] drReg = dtSignatorys.Select("STATUS_FID = " + (int)StatusAndTypes.EnumStatus.Registered_8);

            //        #region REQUESTED[========================]

            //        if (drCre != null)
            //        {
            //            sb.Append("<h3 class='header2'>Requested</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>Requested By</th>\n");
            //            sb.Append("<th class='tddesc'>Requested On</th>\n");
            //            sb.Append("<th class='tddesc'>Requested Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drCre)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RequestedBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RequestedOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RequestedRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#RequestedBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#RequestedOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#RequestedRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        #region REVISED[========================]

            //        if (drRev != null && drRev.Count() > 0)
            //        {
            //            sb.Append("<h3 class='header2'>Revisions</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>Revised By</th>\n");
            //            sb.Append("<th class='tddesc'>Revised On</th>\n");
            //            sb.Append("<th class='tddesc'>Revised Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drRev)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RevisedBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RevisedOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RevisedRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#RevisedBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#RevisedOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#RevisedRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        #region AMENDMENT[========================]

            //        if (drAm != null && drAm.Count()>0)
            //        {
            //            sb.Append("<h3 class='header2'>Amendments</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>Sent to Amendment By</th>\n");
            //            sb.Append("<th class='tddesc'>Sent to Amendment On</th>\n");
            //            sb.Append("<th class='tddesc'>Sent to Amendment Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drAm)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AmendmentBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AmendmentOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AmendmentRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#AmendmentBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#AmendmentOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#AmendmentRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        #region AMENDED[========================]

            //        if (drAmd != null && drAmd.Count() > 0)
            //        {
            //            sb.Append("<h3 class='header2'>Amended</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>Amended By</th>\n");
            //            sb.Append("<th class='tddesc'>Amended On</th>\n");
            //            sb.Append("<th class='tddesc'>Amended Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drAmd)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AmendedBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AmendedOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AmendedRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#AmendedBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#AmendedOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#AmendedRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        #region CHECKED[========================]

            //        if (drChk != null && drChk.Count() > 0)
            //        {
            //            sb.Append("<h3 class='header2'>HOD Approved</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>HOD Approved By</th>\n");
            //            sb.Append("<th class='tddesc'>HOD Approved On</th>\n");
            //            sb.Append("<th class='tddesc'>HOD Approved Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drChk)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#HODApprovedBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#HODApprovedOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#HODApprovedRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#HODApprovedBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#HODApprovedOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#HODApprovedRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        #region PROC HOD APPROVED[========================]

            //        if (drProcApp != null && drProcApp.Count() > 0)
            //        {
            //            sb.Append("<h3 class='header2'>HOD Approved</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>HOD Approved By</th>\n");
            //            sb.Append("<th class='tddesc'>HOD Approved On</th>\n");
            //            sb.Append("<th class='tddesc'>HOD Approved Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drProcApp)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#HODApprovedBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#HODApprovedOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#HODApprovedRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#HODApprovedBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#HODApprovedOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#HODApprovedRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        #region ACCOUNTS CHECKED[========================]

            //        if (drAccChk != null && drAccChk.Count() > 0)
            //        {
            //            sb.Append("<h3 class='header2'>Accounts Level-1 Approved</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>Approved By</th>\n");
            //            sb.Append("<th class='tddesc'>Approved On</th>\n");
            //            sb.Append("<th class='tddesc'>Approved Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drAccChk)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AL1ApprovedBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AL1ApprovedOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AL1ApprovedRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#AL1ApprovedBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#AL1ApprovedOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#AL1ApprovedRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        #region FINAL APPROVED[========================]

            //        if (drFinApp != null && drFinApp.Count() > 0)
            //        {
            //            sb.Append("<h3 class='header2'>Accounts Level-2 Approved</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>Approved By</th>\n");
            //            sb.Append("<th class='tddesc'>Approved On</th>\n");
            //            sb.Append("<th class='tddesc'>Approved Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drFinApp)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AL2ApprovedBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AL2ApprovedOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#AL2ApprovedRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#AL2ApprovedBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#AL2ApprovedOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#AL2ApprovedRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        #region REGISTERED[========================]

            //        if (drReg != null && drReg.Count() > 0)
            //        {
            //            sb.Append("<h3 class='header2'>Register</h3>\n");
            //            sb.Append("<table class='tblsubitems'>\n");
            //            sb.Append("<tr class='trsubitems'>\n");
            //            sb.Append("<th class='tdsrno'>Sr.No.</th>\n");
            //            sb.Append("<th class='tddesc'>Registered By</th>\n");
            //            sb.Append("<th class='tddesc'>Registered On</th>\n");
            //            sb.Append("<th class='tddesc'>Registered Remarks</th>\n");
            //            sb.Append("</tr>\n");

            //            int count = 0;
            //            foreach (DataRow dr in drReg)
            //            {
            //                count++;
            //                sb.Append("<tr>\n");
            //                sb.Append("<td class='tdsrno'>{#SRNo#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RegisteredBy#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RegisteredOn#}</td>\n");
            //                sb.Append("<td class='tddesc'>{#RegisteredRemarks#}</td>\n");
            //                sb.Append("</tr>\n");

            //                sb.Replace("{#SRNo#}", Convert.ToString(count));
            //                sb.Replace("{#RegisteredBy#}", Convert.ToString(dr["ACTION_BY"]));
            //                sb.Replace("{#RegisteredOn#}", Convert.ToString(dr["ACTION_ON"]));
            //                sb.Replace("{#RegisteredRemarks#}", Convert.ToString(dr["REMARKS"]));
            //            }
            //            sb.Append("</table>\n");

            //        }

            //        #endregion


            //        //sb.Append("</table>\n");
            //        //sb.Append("</fieldset>\n");

            //    }
            //}






            htmlText = sb.ToString();
            return htmlText;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}

