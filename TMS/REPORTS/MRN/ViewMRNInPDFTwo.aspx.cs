using BAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class REPORTS_MRN_ViewMRNInPDFTwo : System.Web.UI.Page
{
    BAL.Reports objReports = new Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsDBDetails = new DataSet();
    DataSet ds = new DataSet();

    string poNo = string.Empty;
    string poDate = string.Empty;
    string qty = string.Empty;
    string vendorName = string.Empty;
    string description = string.Empty;

    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewDetail();
        }
    }

    private void ViewDetail()
    {
        try
        {
            dsDBDetails = objCommon.GetDBDetails();

            if (Request.QueryString["mrn"] != null)
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);

                    }
                }


                StringBuilder sb = new StringBuilder();
                int count = 0;
                ds = objReports.GetMRNDetailForPDF(dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU, Convert.ToString(Request.QueryString["mrn"]));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Request.QueryString["pono"] != null)
                        poNo = Convert.ToString(Request.QueryString["pono"]);
                    else
                        poNo = string.Empty;

                    if (Request.QueryString["podate"] != null)
                        poDate = Convert.ToString(Request.QueryString["podate"]);
                    else
                        poDate = string.Empty;

                    if (Request.QueryString["qty"] != null)
                        qty = Convert.ToString(Request.QueryString["qty"]);
                    else
                        qty = string.Empty;

                    if (Request.QueryString["vendorname"] != null)
                        vendorName = Convert.ToString(Request.QueryString["vendorname"]);
                    else
                        vendorName = string.Empty;

                    string tblCss = "style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;";
                    string thCss = "style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center; background-color: #4CAF50; color: white;";
                    string tdCss = "style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;";


                    sb.Append("<table " + tblCss + "'>");

                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 100%;'>");
                    sb.Append("<table " + tblCss + "'>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 297px; text-align:center;background-color: cornflowerblue; color: white;' rowspan='2'><h2>coperion</h2></td>");
                    sb.Append("<th " + thCss + " width: 513x;' colspan='3' rowspan='2'><h3>INCOMING INSPECTION REPORT</h3></th>");
                    sb.Append("<td " + tdCss + "'>FMT NO.: </td>");
                    sb.Append("<td " + tdCss + "'>FT/QA/02/00</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + "'>Rev.:00</td>");
                    sb.Append("<td " + tdCss + "'>Date: 17.05.2018</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 297px;'>PO NO./DATE:</td>");
                    sb.Append("<td " + tdCss + " width: 149px;'>" + poNo + "/" + poDate + "</td>");
                    sb.Append("<td " + tdCss + " width: 187px;'>VENDOR NAME:</td>");
                    sb.Append("<td " + tdCss + " width: 312px;' colspan='3'>" + vendorName + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 297px;'>QTY.:</td>");
                    sb.Append("<td " + tdCss + " width: 149px;'></td>");
                    sb.Append("<td " + tdCss + " width: 187px;'>PART NAME:</td>");
                    sb.Append("<td " + tdCss + "' colspan='3'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 297px;'>SAMPLE SIZE:DIMENSIONAL:</td>");
                    sb.Append("<td " + tdCss + " width: 149px;'>");
                    sb.Append("<input type='text' style='width: 99%;' /></td>");
                    sb.Append("<td " + tdCss + " width: 187px;'>INSPECTION DATE:</td>");
                    sb.Append("<td " + tdCss + "' colspan='3'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 297px;'>VISUAL:</td>");
                    sb.Append("<td " + tdCss + " width: 149px;'>");
                    sb.Append("<input type='text' style='width: 99%;' /></td>");
                    sb.Append("<td " + tdCss + " width: 187px;'>MRN NO.:</td>");
                    sb.Append("<td " + tdCss + "' colspan='3'>" + Convert.ToString(Request.QueryString["mrn"]) + "</td>");
                    sb.Append("</tr></table></td></tr>");

                    sb.Append("<tr><td " + tdCss + " width: 100%;' colspan='5'><hr /></td></tr>");

                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 100%;'>");
                    sb.Append("<table " + tblCss + "'>");
                    sb.Append("<tr>");
                    sb.Append("<th " + thCss + " width: 60px;'>Sr. No</th>");
                    sb.Append("<th " + thCss + " width: 546px;'>Characterstic</th>");
                    sb.Append("<th " + thCss + " width: 324px;'>Ovservation</th>");
                    sb.Append("<th " + thCss + " width: 108px;'>Status</th>");
                    sb.Append("<th " + thCss + "'>Remarks</th>");
                    sb.Append("</tr>");


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        count++;
                        if (dr["PRODCODE"] != DBNull.Value)
                        {

                            if (dr["DESCRIPT"] != DBNull.Value)
                                description = Convert.ToString(dr["DESCRIPT"]);
                            else
                                description = string.Empty;

                            sb.Append("<tr>");
                            sb.Append("<td " + tdCss + "'>" + count + "</td>");
                            sb.Append("<td " + tdCss + "'>" + description + "</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>Rating</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>Visual Check</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>Total Quantity Checked</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr><td " + tdCss + "' colspan='5'><hr /></td></tr>");

                        }
                    }

                    sb.Append("</table></td></tr>");

                    sb.Append("<tr><td " + tdCss + " width: 100%;' colspan='5'><hr /></td></tr>");

                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 100%;'>");
                    sb.Append("<table " + tblCss + "'>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " height: 35px; width: 89px;'>Disposition</td>");
                    sb.Append("<td " + tdCss + " width: 14px; height: 35px;'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + " height: 35px;'>Accepted</td>");
                    sb.Append("<td " + tdCss + " height: 35px;'>Accepted U/D</td>");
                    sb.Append("<td " + tdCss + " height: 35px;'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + " height: 35px; text-align: center;' colspan='3'>Rejected</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 89px;'>Quantity</td>");
                    sb.Append("<td " + tdCss + " width: 14px;'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Segrigration</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Rework</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Return to Source</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " background-color: #4CAF50; color: white;' colspan='8'>Note: Forward the inspection report to H.O.D.(Q.A.) in case of non-conformance with inspection standard and copy to purchase for supplier evealuation.</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 103px;' colspan='2'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "' colspan='3' rowspan='2'>Comments:-</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 103px; text-align: center;' colspan='2'>Inspected By</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Date</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>H.O.D.(Q.A.)</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Date</td>");
                    sb.Append("</tr></table></td></tr></table>");

                    ltTable.Text = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void ViewDetailNew()
    {
        try
        {
            dsDBDetails = objCommon.GetDBDetails();

            if (Request.QueryString["mrn"] != null)
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);

                    }
                }


                StringBuilder sb = new StringBuilder();
                int count = 0;
                ds = objReports.GetMRNDetailForPDF(dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU, Convert.ToString(Request.QueryString["mrn"]));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Request.QueryString["pono"] != null)
                        poNo = Convert.ToString(Request.QueryString["pono"]);
                    else
                        poNo = string.Empty;

                    if (Request.QueryString["podate"] != null)
                        poDate = Convert.ToString(Request.QueryString["podate"]);
                    else
                        poDate = string.Empty;

                    if (Request.QueryString["qty"] != null)
                        qty = Convert.ToString(Request.QueryString["qty"]);
                    else
                        qty = string.Empty;

                    if (Request.QueryString["vendorname"] != null)
                        vendorName = Convert.ToString(Request.QueryString["vendorname"]);
                    else
                        vendorName = string.Empty;

                    sb.Append("<table style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;'><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;'><table style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;'><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 217px;' rowspan='2'><input type='image' src='../../Images/COPERION/coperion_logo.jpg' style='width: 220px;height: 69px;' /></td><th style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center; background-color: #4CAF50; color: white;width: 492px;' colspan='3' rowspan='2' ><h3>INCOMING INSPECTION REPORT</h3></th><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>FMT NO.: </td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>FT/QA/02/00</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Rev.:00</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Date: 17.05.2018</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 217px;'>PO NO./DATE:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 74px;' >DATE</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 187px;'>VENDOR NAME:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 312px;' colspan='3'>VENDOR NAME:</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 217px;'>QTY.:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 74px;' >QTY</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 187px;'>PART NAME:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='3'>PART NAME:</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 217px;'>SAMPLE SIZE:DIMENSIONAL:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 74px;' ><input type='text' style='width: 99%;' /></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 187px;'>INSPECTION DATE:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='3'>INSPECTION DATE:</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 217px;' >VISUAL:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 74px;' ><input type='text' style='width: 99%;' /></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 187px;'>MRN NO.:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='3'>MRN NO.:</td></tr></table></td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;' colspan='5'><hr /></td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;'><table style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;'><tr><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 60px;'>Sr. No</th><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 546px;'>Characterstic</th><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 324px;'>Ovservation</th><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 108px;'>Status</th><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white;'>Remarks</th></tr>");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        count++;
                        if (dr["PRODCODE"] != DBNull.Value)
                        {

                            if (dr["DESCRIPT"] != DBNull.Value)
                                description = Convert.ToString(dr["DESCRIPT"]);
                            else
                                description = string.Empty;

                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>" + count + "</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>" + Convert.ToString(dr["PRODCODE"]) + "</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");
                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>" + description + "</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");
                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Rating</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");
                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Visual Check</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");
                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Total Qty. Checked</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");

                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='5'><hr /></td></tr>");

                        }
                    }
                    sb.Append("</table></td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='5'><hr /></td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'><table style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;'><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;width: 89px;'>Disposition</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 14px;height: 35px;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;'>Accepted</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;'>Accepted U/D</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;text-align: center;'colspan='3'>Rejected</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 89px;'>Quantity</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 14px;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Segrigration</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Rework</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Return to Source</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; background-color: #4CAF50; color: white;' colspan='8'>Note: Forward the inspection report to H.O.D.(Q.A.) in case of non-conformance with inspection standard and copy to purchase for supplier evealuation.</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 103px;' colspan='2'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='3' rowspan='2'>Comments:-</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 103px;text-align: center;' colspan='2'>Inspected By</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Date</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>H.O.D.(Q.A.)</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Date</td></tr></table></td></tr></table>");


                    ltTable.Text = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GeneratePDF();


            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=TravelStatement_" + "lblTourSanctionNo.Text" + ".pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //this.Page.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 50f, 0f);//(PageSize.A4, 10f, 10f, 100f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();
        }
        catch (Exception ex)
        {
            //
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    private void GeneratePDF()
    {
        byte[] bytesArray = null;
        string html = GetHTMLString();
        using (var ms = new MemoryStream())
        {
            using (var document = new Document())
            {
                using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
                {
                    document.Open();
                    using (var strReader = new StringReader(html))
                    {
                        //Set factories
                        HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
                        htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                        //Set css
                        ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                        //cssResolver.AddCssFile(System.Web.HttpContext.Current.Server.MapPath("~/Styles/viewpdftablecss.css"), true);
                        //Export
                        IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));
                        var worker = new XMLWorker(pipeline, true);
                        var xmlParse = new XMLParser(true, worker);
                        xmlParse.Parse(strReader);
                        xmlParse.Flush();
                    }
                    document.Close();
                }
            }
            bytesArray = ms.ToArray();
        }

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=MRN_" + "lblTourSanctionNo" + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BufferOutput = true;
        Response.BinaryWrite(bytesArray);
        Response.End();
    }

    private string GetHTMLString()
    {
        string htmlText = string.Empty;
        try
        {
            dsDBDetails = objCommon.GetDBDetails();

            if (Request.QueryString["mrn"] != null)
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);

                    }
                }


                StringBuilder sb = new StringBuilder();
                int count = 0;
                ds = objReports.GetMRNDetailForPDF(dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU, Convert.ToString(Request.QueryString["mrn"]));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Request.QueryString["pono"] != null)
                        poNo = Convert.ToString(Request.QueryString["pono"]);
                    else
                        poNo = string.Empty;

                    if (Request.QueryString["podate"] != null)
                        poDate = Convert.ToString(Request.QueryString["podate"]);
                    else
                        poDate = string.Empty;

                    if (Request.QueryString["qty"] != null)
                        qty = Convert.ToString(Request.QueryString["qty"]);
                    else
                        qty = string.Empty;

                    if (Request.QueryString["vendorname"] != null)
                        vendorName = Convert.ToString(Request.QueryString["vendorname"]);
                    else
                        vendorName = string.Empty;



                    string tblCss = "style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;";
                    string thCss = "style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center; background-color: #4CAF50; color: white;";
                    string tdCss = "style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;";


                    sb.Append("<table " + tblCss + "'>");

                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 100%;'>");
                    sb.Append("<table " + tblCss + "'>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 209px; text-align:center;background-color: cornflowerblue; color: white;' rowspan='2'><h2>coperion</h2></td>");
                    sb.Append("<th " + thCss + " width: 431x;' colspan='3' rowspan='2'><h3>INCOMING INSPECTION REPORT</h3></th>");
                    sb.Append("<td " + tdCss + "'>FMT NO.: </td>");
                    sb.Append("<td " + tdCss + "'>FT/QA/02/00</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + "'>Rev.:00</td>");
                    sb.Append("<td " + tdCss + "'>Date: 17.05.2018</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 209px;'>PO NO./DATE:</td>");
                    sb.Append("<td " + tdCss + " width: 200px;'>" + poNo + "/" + poDate + "</td>");
                    sb.Append("<td " + tdCss + " width: 200px;'>VENDOR NAME:</td>");
                    sb.Append("<td " + tdCss + " width: 312px;' colspan='3'>" + vendorName + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 209px;'>QTY.:</td>");
                    sb.Append("<td " + tdCss + " width: 200px;'></td>");
                    sb.Append("<td " + tdCss + " width: 200px;'>PART NAME:</td>");
                    sb.Append("<td " + tdCss + "' colspan='3'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 209px;'>SAMPLE SIZE:DIMENSIONAL:</td>");
                    sb.Append("<td " + tdCss + " width: 200px;'>");
                    sb.Append("<input type='text' style='width: 99%;' /></td>");
                    sb.Append("<td " + tdCss + " width: 200px;'>INSPECTION DATE:</td>");
                    sb.Append("<td " + tdCss + "' colspan='3'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 209px;'>VISUAL:</td>");
                    sb.Append("<td " + tdCss + " width: 200px;'>");
                    sb.Append("<input type='text' style='width: 99%;' /></td>");
                    sb.Append("<td " + tdCss + " width: 200px;'>MRN NO.:</td>");
                    sb.Append("<td " + tdCss + "' colspan='3'>" + Convert.ToString(Request.QueryString["mrn"]) + "</td>");
                    sb.Append("</tr></table></td></tr>");

                    sb.Append("<tr><td " + tdCss + " width: 100%;' colspan='5'><hr /></td></tr>");

                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 100%;'>");
                    sb.Append("<table " + tblCss + "'>");
                    sb.Append("<tr>");
                    sb.Append("<th " + thCss + " width: 60px;'>Sr. No</th>");
                    sb.Append("<th " + thCss + " width: 546px;'>Characterstic</th>");
                    sb.Append("<th " + thCss + " width: 324px;'>Ovservation</th>");
                    sb.Append("<th " + thCss + " width: 108px;'>Status</th>");
                    sb.Append("<th " + thCss + "'>Remarks</th>");
                    sb.Append("</tr>");


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        count++;
                        if (dr["PRODCODE"] != DBNull.Value)
                        {

                            if (dr["DESCRIPT"] != DBNull.Value)
                                description = Convert.ToString(dr["DESCRIPT"]);
                            else
                                description = string.Empty;

                            sb.Append("<tr>");
                            sb.Append("<td " + tdCss + "'>" + count + "</td>");
                            sb.Append("<td " + tdCss + "'>" + description + "</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>Rating</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>Visual Check</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>Total Quantity Checked</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr><td " + tdCss + "' colspan='5'><hr /></td></tr>");

                        }
                    }

                    sb.Append("</table></td></tr>");

                    sb.Append("<tr><td " + tdCss + " width: 100%;' colspan='5'><hr /></td></tr>");

                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 100%;'>");
                    sb.Append("<table " + tblCss + "'>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " height: 35px; width: 89px;'>Disposition</td>");
                    sb.Append("<td " + tdCss + " width: 14px; height: 35px;'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + " height: 35px;'>Accepted</td>");
                    sb.Append("<td " + tdCss + " height: 35px;'>Accepted U/D</td>");
                    sb.Append("<td " + tdCss + " height: 35px;'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + " height: 35px; text-align: center;' colspan='3'>Rejected</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 89px;'>Quantity</td>");
                    sb.Append("<td " + tdCss + " width: 14px;'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Segrigration</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Rework</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Return to Source</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " background-color: #4CAF50; color: white;' colspan='8'>Note: Forward the inspection report to H.O.D.(Q.A.) in case of non-conformance with inspection standard and copy to purchase for supplier evealuation.</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 103px;' colspan='2'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "'>&nbsp;</td>");
                    sb.Append("<td " + tdCss + "' colspan='3' rowspan='2'>Comments:-</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td " + tdCss + " width: 103px; text-align: center;' colspan='2'>Inspected By</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Date</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>H.O.D.(Q.A.)</td>");
                    sb.Append("<td " + tdCss + " text-align: center;'>Date</td>");
                    sb.Append("</tr></table></td></tr></table>");



                    htmlText = sb.ToString();
                }
            }
            return htmlText;
        }
        catch (Exception ex)
        {
            return htmlText;
        }
    }

    private string GetHTMLStringNew()
    {
        string htmlText = string.Empty;
        try
        {
            dsDBDetails = objCommon.GetDBDetails();

            if (Request.QueryString["mrn"] != null)
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);

                    }
                }


                StringBuilder sb = new StringBuilder();
                int count = 0;
                ds = objReports.GetMRNDetailForPDF(dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU, Convert.ToString(Request.QueryString["mrn"]));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Request.QueryString["pono"] != null)
                        poNo = Convert.ToString(Request.QueryString["pono"]);
                    else
                        poNo = string.Empty;

                    if (Request.QueryString["podate"] != null)
                        poDate = Convert.ToString(Request.QueryString["podate"]);
                    else
                        poDate = string.Empty;

                    if (Request.QueryString["qty"] != null)
                        qty = Convert.ToString(Request.QueryString["qty"]);
                    else
                        qty = string.Empty;

                    if (Request.QueryString["vendorname"] != null)
                        vendorName = Convert.ToString(Request.QueryString["vendorname"]);
                    else
                        vendorName = string.Empty;

                    sb.Append("<table style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;'><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;'><table style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;'><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 217px;' rowspan='2'><input type='image' src='../../Images/COPERION/coperion_logo.jpg' style='width: 220px;height: 69px;' /></td><th style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; text-align: center; background-color: #4CAF50; color: white;width: 492px;' colspan='3' rowspan='2' ><h3>INCOMING INSPECTION REPORT</h3></th><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>FMT NO.: </td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>FT/QA/02/00</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Rev.:00</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Date: 17.05.2018</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 217px;'>PO NO./DATE:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 74px;' >DATE</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 187px;'>VENDOR NAME:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 312px;' colspan='3'>VENDOR NAME:</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 217px;'>QTY.:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 74px;' >QTY</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 187px;'>PART NAME:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='3'>PART NAME:</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 217px;'>SAMPLE SIZE:DIMENSIONAL:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 74px;' ><input type='text' style='width: 99%;' /></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 187px;'>INSPECTION DATE:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='3'>INSPECTION DATE:</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;width: 217px;' >VISUAL:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 74px;' ><input type='text' style='width: 99%;' /></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 187px;'>MRN NO.:</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='3'>MRN NO.:</td></tr></table></td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;' colspan='5'><hr /></td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 100%;'><table style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;'><tr><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 60px;'>Sr. No</th><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 546px;'>Characterstic</th><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 324px;'>Ovservation</th><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white; width: 108px;'>Status</th><th style='padding-top: 12px; padding-bottom: 12px; text-align: left; background-color: #4CAF50; color: white;'>Remarks</th></tr>");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        count++;
                        if (dr["PRODCODE"] != DBNull.Value)
                        {

                            if (dr["DESCRIPT"] != DBNull.Value)
                                description = Convert.ToString(dr["DESCRIPT"]);
                            else
                                description = string.Empty;

                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>" + count + "</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>" + Convert.ToString(dr["PRODCODE"]) + "</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");
                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>" + description + "</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");
                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Rating</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");
                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Visual Check</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");
                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>Total Qty. Checked</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'></td></tr>");

                            sb.Append("<tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='5'><hr /></td></tr>");

                        }
                    }
                    sb.Append("</table></td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='5'><hr /></td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'><table style='font-family: Trebuchet MS, Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;'><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;width: 89px;'>Disposition</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 14px;height: 35px;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;'>Accepted</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;'>Accepted U/D</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; height: 35px;text-align: center;'colspan='3'>Rejected</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 89px;'>Quantity</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 14px;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Segrigration</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Rework</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Return to Source</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; background-color: #4CAF50; color: white;' colspan='8'>Note: Forward the inspection report to H.O.D.(Q.A.) in case of non-conformance with inspection standard and copy to purchase for supplier evealuation.</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 103px;' colspan='2'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;'>&nbsp;</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;' colspan='3' rowspan='2'>Comments:-</td></tr><tr><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt; width: 103px;text-align: center;' colspan='2'>Inspected By</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Date</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>H.O.D.(Q.A.)</td><td style='border: 1px solid #ddd; padding: 5px; font-size: 10pt;text-align: center;'>Date</td></tr></table></td></tr></table>");



                    htmlText = sb.ToString();
                }
            }
            return htmlText;
        }
        catch (Exception ex)
        {
            return htmlText;
        }
    }
}