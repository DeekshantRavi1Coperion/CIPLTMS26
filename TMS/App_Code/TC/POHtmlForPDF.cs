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


public class POHtmlForPDF
{

    string PONo = string.Empty;
    string PODate = string.Empty;
    string vendorCode = string.Empty;
    string vendorName = string.Empty;
    string address = string.Empty;
    string city = string.Empty;
    string state = string.Empty;
    string country = string.Empty;
    string gstStateCode = string.Empty;
    string GSTIN = string.Empty;
    string pinCode = string.Empty;
    string telephoneNo = string.Empty;
    string fax = string.Empty;
    string mobileNo = string.Empty;
    string whatsAppNo = string.Empty;
    string emailID = string.Empty;
    string totalAmount = string.Empty;

    int srNo = 0;
    string itemCode = string.Empty;
    string itemName = string.Empty;
    string additionalDesc = string.Empty;
    string UOM = string.Empty;
    string quantity = string.Empty;
    string rate = string.Empty;
    string amount = string.Empty;
    string deliveryDate = string.Empty;


    public string GetHtmlForPDF(DataTable dtPO, DataTable dtPODetails)
    {
        try
        {
            PONo = string.Empty;
            PODate = string.Empty;
            vendorCode = string.Empty;
            vendorName = string.Empty;
            address = string.Empty;
            city = string.Empty;
            state = string.Empty;
            country = string.Empty;
            gstStateCode = string.Empty;
            GSTIN = string.Empty;
            pinCode = string.Empty;
            telephoneNo = string.Empty;
            fax = string.Empty;
            mobileNo = string.Empty;
            whatsAppNo = string.Empty;
            emailID = string.Empty;
            totalAmount = string.Empty;

            string htmlText = string.Empty;
            htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (dtPO.Rows.Count > 0)
            {
                PONo = Convert.ToString(dtPO.Rows[0]["PO_NO"]).Trim();
                PODate = Convert.ToString(dtPO.Rows[0]["PO_DATE"]).Trim();
                vendorCode = Convert.ToString(dtPO.Rows[0]["VENDOR_CODE"]);
                vendorName = Convert.ToString(dtPO.Rows[0]["VENDOR_NAME"]).Trim();
                address = Convert.ToString(dtPO.Rows[0]["ADDRESS"]).Trim();
                city = Convert.ToString(dtPO.Rows[0]["CITY"]).Trim();
                state = Convert.ToString(dtPO.Rows[0]["STATE"]).Trim();
                country = Convert.ToString(dtPO.Rows[0]["COUNTRY"]).Trim();
                gstStateCode = Convert.ToString(dtPO.Rows[0]["GSTATECODE"]);
                GSTIN = Convert.ToString(dtPO.Rows[0]["GSTIN"]);
                pinCode = Convert.ToString(dtPO.Rows[0]["PINCODE"]);
                telephoneNo = Convert.ToString(dtPO.Rows[0]["TELEPHONE"]);
                fax = Convert.ToString(dtPO.Rows[0]["FAX"]);
                mobileNo = Convert.ToString(dtPO.Rows[0]["MOBPHONE"]);
                whatsAppNo = Convert.ToString(dtPO.Rows[0]["WHATSAPPPHONE"]);
                emailID = Convert.ToString(dtPO.Rows[0]["EMAIL"]);
                totalAmount = Convert.ToString(dtPO.Rows[0]["TOTAL_AMOUNT"]);
                
                sb.Append("<h2 class='headerStyle'>PURCHASE ORDER</h2>\n");
                sb.Append("<hr />\n");
                sb.Append("<table class='tblheader'>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>PO No:</td>\n");
                sb.Append("<td class='td2header'>{#PONo#}</td>\n");
                sb.Append("<td class='td1header'>PO Date:</td>\n");
                sb.Append("<td class='td2header'>{#PODate#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Vendor Name:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#vendorName#} - [{#vendorCode#}]</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Address:</td>\n");
                sb.Append("<td class='td2header' colspan='3'>{#address#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>City:</td>\n");
                sb.Append("<td class='td2header'>{#city#}</td>\n");
                sb.Append("<td class='td1header'>State:</td>\n");
                sb.Append("<td class='td2header'>{#state#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Country:</td>\n");
                sb.Append("<td class='td2header'>{#country#}</td>\n");
                sb.Append("<td class='td1header'>GSTIN:</td>\n");
                sb.Append("<td class='td2header'>{#GSTIN#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>PIN Code:</td>\n");
                sb.Append("<td class='td2header'>{#pinCode#}</td>\n");
                sb.Append("<td class='td1header'>Telephone No.:</td>\n");
                sb.Append("<td class='td2header'>{#telephoneNo#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Mobile No:</td>\n");
                sb.Append("<td class='td2header'>{#mobileNo#}</td>\n");
                sb.Append("<td class='td1header'>WhatsApp No:</td>\n");
                sb.Append("<td class='td2header'>{#whatsAppNo#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>FAX:</td>\n");
                sb.Append("<td class='td2header'>{#fax#}</td>\n");
                sb.Append("<td class='td1header'>Email:</td>\n");
                sb.Append("<td class='td2header'>{#emailID#}</td>\n");
                sb.Append("</tr>\n");
                sb.Append("<tr>\n");
                sb.Append("<td class='td1header'>Total Amount:</td>\n");
                sb.Append("<td class='td2header'>{#totalAmount#}</td>\n");                
                sb.Append("</tr>\n");
                sb.Append("</table>\n");


                sb.Replace("{#PONo#}", PONo);
                sb.Replace("{#PODate#}", PODate);
                sb.Replace("{#vendorCode#}", vendorCode);
                sb.Replace("{#vendorName#}", vendorName);
                sb.Replace("{#address#}", address);
                sb.Replace("{#city#}", city);
                sb.Replace("{#state#}", state);
                sb.Replace("{#country#}", country);
                sb.Replace("{#gstStateCode#}", gstStateCode);
                sb.Replace("{#GSTIN#}", GSTIN);
                sb.Replace("{#pinCode#}", pinCode);
                sb.Replace("{#telephoneNo#}", telephoneNo);
                sb.Replace("{#fax#}", fax);
                sb.Replace("{#mobileNo#}", mobileNo);
                sb.Replace("{#whatsAppNo#}", whatsAppNo);
                sb.Replace("{#emailID#}", emailID);
                sb.Replace("{#totalAmount#}", totalAmount);



                if (dtPODetails.Rows.Count > 0)
                {
                    sb.Append("<hr />\n");                    
                    sb.Append("<h3 class='header2'>Items Detail</h3>\n");                    

                    sb.Append("<table class='tblsuitems'>\n");

                    sb.Append("<tr class='trsubitems'>\n");
                    sb.Append("<th class='td1subitems'>Sr.No.</th>\n");
                    sb.Append("<th class='tdtag'>Item Code</th>\n");
                    sb.Append("<th class='tdtag'>Item Name</th>\n");
                    sb.Append("<th class='td4subitems'>Add. Desc</th>\n");
                    sb.Append("<th class='tdcate'>UOM</th>\n");
                    sb.Append("<th class='tdquantity'>Qty.</th>\n");
                    sb.Append("<th class='tdquantity'>Rate</th>\n");
                    sb.Append("<th class='td3subitems'>Amount</th>\n");
                    sb.Append("<th class='tdtag'>Delivery Date</th>\n");
                    sb.Append("</tr>\n");

                    foreach (DataRow dr in dtPODetails.Rows)
                    {
                        srNo = 0;
                        itemCode = string.Empty;
                        itemName = string.Empty;
                        additionalDesc = string.Empty;
                        UOM = string.Empty;
                        quantity = string.Empty;
                        rate = string.Empty;
                        amount = string.Empty;
                        deliveryDate = string.Empty;


                        srNo = Convert.ToInt32(dr["SR_NO"]);
                        itemCode = Convert.ToString(dr["ITEM_CODE"]).Trim();
                        itemName = Convert.ToString(dr["ITEM_NAME"]);
                        additionalDesc = Convert.ToString(dr["ADDITIONAL_DESC"]).Trim();
                        UOM = Convert.ToString(dr["UOM"]).Trim();
                        quantity = Convert.ToString(dr["QUANTITY"]).Trim();
                        rate = Convert.ToString(dr["RATE"]).Trim();
                        amount = Convert.ToString(dr["AMOUNT"]).Trim();
                        deliveryDate = Convert.ToString(dr["DELIVERY_DATE"]).Trim();

                        sb.Append("<tr>\n");
                        sb.Append("<td class='td1subitems'>{#srNo#}</td>\n");
                        sb.Append("<td class='tdtag'>{#itemCode#}</td>\n");
                        sb.Append("<td class='tdtag'>{#itemName#}</td>\n");
                        sb.Append("<td class='td4subitems'>{#additionalDesc#}</td>\n");
                        sb.Append("<td class='tdcate'>{#UOM#}</td>\n");
                        sb.Append("<td class='tdquantity'>{#quantity#}</td>\n");
                        sb.Append("<td class='tdquantity'>{#rate#}</td>\n");
                        sb.Append("<td class='td3subitems'>{#amount#}</td>\n");
                        sb.Append("<td class='tdtag'>{#deliveryDate#}</td>\n");
                        sb.Append("</tr>\n");

                        sb.Replace("{#srNo#}", Convert.ToString(srNo));
                        sb.Replace("{#itemCode#}", itemCode);
                        sb.Replace("{#itemName#}", itemName);
                        sb.Replace("{#additionalDesc#}", additionalDesc);
                        sb.Replace("{#UOM#}", UOM);
                        sb.Replace("{#quantity#}", quantity);
                        sb.Replace("{#rate#}", rate);
                        sb.Replace("{#amount#}", amount);
                        sb.Replace("{#deliveryDate#}", deliveryDate);

                    }

                    sb.Append("</table>\n");
                    sb.Append("<hr />\n");
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