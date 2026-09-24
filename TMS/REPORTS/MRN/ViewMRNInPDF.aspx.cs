using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using BAL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;

public partial class REPORTS_MRN_ViewMRNInPDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ViewDetail();
            //GetANDBinDDetail();
        }
    }

    private void ViewDetail()
    {
        try
        {
            BAL.TourAndTravels objTourAndTravels = new TourAndTravels();
            DataSet ds = new DataSet();
            if (Request.QueryString["travelStatementID"] != null)
            {
                ds = objTourAndTravels.GetTravelStatementForPDF(Convert.ToInt32(Request.QueryString["travelStatementID"]));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //if (dr["TOUR_SANCTION_NO"] != DBNull.Value)
                        //    lblTourSanctionNo.Text = Convert.ToString(dr["TOUR_SANCTION_NO"]);

                        //if (dr["TOUR_NO"] != DBNull.Value)
                        //    lblTourNo.Text = Convert.ToString(dr["TOUR_NO"]);

                        //if (dr["EMPLOYEE_NAME"] != DBNull.Value)
                        //    lblEmplyoeeName.Text = Convert.ToString(dr["EMPLOYEE_NAME"]);

                        //if (dr["EMPLOYEE_ID"] != DBNull.Value)
                        //    lblEmployeeID.Text = Convert.ToString(dr["EMPLOYEE_ID"]);

                        //if (dr["DESIGNATION"] != DBNull.Value)
                        //    lblDesignation.Text = Convert.ToString(dr["DESIGNATION"]);

                        //if (dr["START_DATE"] != DBNull.Value)
                        //    lblStartDateOfTour.Text = Convert.ToDateTime(dr["START_DATE"]).ToString("dd-MMM-yyyy");

                        //if (dr["END_DATE"] != DBNull.Value)
                        //    lblEndDateOfTour.Text = Convert.ToDateTime(dr["END_DATE"]).ToString("dd-MMM-yyyy");

                        //if (dr["CUST_VEND_NAME"] != DBNull.Value)
                        //    lblCustVendName.Text = Convert.ToString(dr["CUST_VEND_NAME"]);

                        //if (dr["PLACE_OF_VISIT"] != DBNull.Value)
                        //    lblPlaceOfVisit.Text = Convert.ToString(dr["PLACE_OF_VISIT"]);

                        //if (dr["VISIT_TYPE"] != DBNull.Value)
                        //    lblPurposeOfVisit.Text = Convert.ToString(dr["VISIT_TYPE"]);

                        //if (dr["JOB_NO"] != DBNull.Value)
                        //    lblJobNo.Text = Convert.ToString(dr["JOB_NO"]);

                        //if (dr["BUS_SEGMENT"] != DBNull.Value)
                        //    lblBusinessSegment.Text = Convert.ToString(dr["BUS_SEGMENT"]);

                        //if (dr["AIRFARE_AMT"] != DBNull.Value)
                        //    lblAirfare.Text = Convert.ToString(dr["AIRFARE_AMT"]);

                        //if (dr["TELEPHONE_MOBILE_AMT"] != DBNull.Value)
                        //    lblTelephone.Text = Convert.ToString(dr["TELEPHONE_MOBILE_AMT"]);

                        //if (dr["LODGING_AMT"] != DBNull.Value)
                        //    lblLodging.Text = Convert.ToString(dr["LODGING_AMT"]);

                        //if (dr["TIPS_AMT"] != DBNull.Value)
                        //    lblTips.Text = Convert.ToString(dr["TIPS_AMT"]);

                        //if (dr["MEALS_AMT"] != DBNull.Value)
                        //    lblMeals.Text = Convert.ToString(dr["MEALS_AMT"]);

                        //if (dr["VISAFEE_AMT"] != DBNull.Value)
                        //    lblVisaFee.Text = Convert.ToString(dr["VISAFEE_AMT"]);

                        //if (dr["GROUND_TRANSPORT_AMT"] != DBNull.Value)
                        //    lblGroundTransport.Text = Convert.ToString(dr["GROUND_TRANSPORT_AMT"]);

                        //if (dr["DAILY_ALLOWANCE_AMT"] != DBNull.Value)
                        //    lblDailyAllowance.Text = Convert.ToString(dr["DAILY_ALLOWANCE_AMT"]);

                        //if (dr["ENTERTAINMENT_AMT"] != DBNull.Value)
                        //    lblEntertainment.Text = Convert.ToString(dr["ENTERTAINMENT_AMT"]);

                        //if (dr["OTHER_AMT"] != DBNull.Value)
                        //    lblOther.Text = Convert.ToString(dr["OTHER_AMT"]);

                        //if (dr["GIFTS_AMT"] != DBNull.Value)
                        //    lblGifts.Text = Convert.ToString(dr["GIFTS_AMT"]);

                        //if (dr["TOTAL_AMT"] != DBNull.Value)
                        //    lblTotal.Text = Convert.ToString(dr["TOTAL_AMT"]);

                        //if (dr["TOTAL_CURRENCY"] != DBNull.Value)
                        //    lblTotalCurrency.Text = Convert.ToString(dr["TOTAL_CURRENCY"]);

                        //if (dr["ADVANCE_AMT"] != DBNull.Value)
                        //    lblAdvancedObtained.Text = Convert.ToString(dr["ADVANCE_AMT"]);

                        //if (dr["ADV_CURRENCY"] != DBNull.Value)
                        //    lblAdvancedObtainedCurrency.Text = Convert.ToString(dr["ADV_CURRENCY"]);

                        //if (dr["ADJUSTED_AMT"] != DBNull.Value)
                        //{
                        //    lblAmountAdjustment.Text = Convert.ToString(dr["ADJUSTED_AMT"]);
                        //    if (Convert.ToDouble(lblAmountAdjustment.Text) < 0)
                        //        lblAdjustmentType.Text = "Payble";
                        //    else
                        //        lblAdjustmentType.Text = "Recoverable";
                        //}

                        //if (dr["ADJUSTED_AMT_CURRENCY"] != DBNull.Value)
                        //    lblAmountAdjustmentCurrency.Text = Convert.ToString(dr["ADJUSTED_AMT_CURRENCY"]);

                        //if (dr["IS_COST_RECOVERABLE"] != DBNull.Value)
                        //    lblTourCostRecoverable.Text = Convert.ToString(dr["IS_COST_RECOVERABLE"]);

                        //if (dr["CREATED_BY"] != DBNull.Value)
                        //    lblCreatedBy.Text = Convert.ToString(dr["CREATED_BY"]);

                        //if (dr["APPROVED_BY"] != DBNull.Value)
                        //    lblApprovedBy.Text = Convert.ToString(dr["APPROVED_BY"]);

                        //if (dr["CHECKED_BY"] != DBNull.Value)
                        //    lblCheckedBy.Text = Convert.ToString(dr["CHECKED_BY"]);

                        //if (dr["PASSED_BY"] != DBNull.Value)
                        //    lblPassedBy.Text = Convert.ToString(dr["PASSED_BY"]);



                        //if (dr["AMENDMENT_COUNT"] != DBNull.Value)
                        //    lblAmendmentCount.Text = Convert.ToString(dr["AMENDMENT_COUNT"]);
                        //else
                        //    lblAmendmentCount.Text = "0";

                        //if (dr["AMENDMENT_BY"] != DBNull.Value)
                        //    lblAmendmentBy.Text = Convert.ToString(dr["AMENDMENT_BY"]);

                        //if (dr["AMENDED_BY"] != DBNull.Value)
                        //    lblAmendedBy.Text = Convert.ToString(dr["AMENDED_BY"]);

                        //if (dr["AMENDED_APPROVED_BY"] != DBNull.Value)
                        //    lblAmendedApprovedBy.Text = Convert.ToString(dr["AMENDED_APPROVED_BY"]);

                        //if (dr["AMENDED_CHECKED_BY"] != DBNull.Value)
                        //    lblAmendedCheckedBy.Text = Convert.ToString(dr["AMENDED_CHECKED_BY"]);

                        //if (dr["AMENDED_PASSED_BY"] != DBNull.Value)
                        //    lblAmendedPassedBy.Text = Convert.ToString(dr["AMENDED_PASSED_BY"]);

                        //if (dr["SETTLED_BY"] != DBNull.Value)
                        //{
                        //    if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        //        lblAmendedSettledBy.Text = Convert.ToString(dr["SETTLED_BY"]);
                        //    else
                        //        lblSettledBy.Text = Convert.ToString(dr["SETTLED_BY"]);
                        //}


                        //if (dr["CREATED_ON"] != DBNull.Value)
                        //    lblCreatedOn.Text = Convert.ToDateTime(dr["CREATED_ON"]).ToString("dd-MMM-yyyy");

                        //if (dr["APPROVED_ON"] != DBNull.Value)
                        //    lblApprovedOn.Text = Convert.ToDateTime(dr["APPROVED_ON"]).ToString("dd-MMM-yyyy");

                        //if (dr["CHECKED_ON"] != DBNull.Value)
                        //    lblCheckedOn.Text = Convert.ToDateTime(dr["CHECKED_ON"]).ToString("dd-MMM-yyyy");

                        //if (dr["PASSED_ON"] != DBNull.Value)
                        //    lblPassedOn.Text = Convert.ToDateTime(dr["PASSED_ON"]).ToString("dd-MMM-yyyy");


                        //if (dr["AMENDMENT_ON"] != DBNull.Value)
                        //    lblAmendmentOn.Text = Convert.ToDateTime(dr["AMENDMENT_ON"]).ToString("dd-MMM-yyyy");

                        //if (dr["AMENDED_ON"] != DBNull.Value)
                        //    lblAmendedOn.Text = Convert.ToDateTime(dr["AMENDED_ON"]).ToString("dd-MMM-yyyy");

                        //if (dr["AMENDED_APPROVED_ON"] != DBNull.Value)
                        //    lblAmendedApprovedOn.Text = Convert.ToDateTime(dr["AMENDED_APPROVED_ON"]).ToString("dd-MMM-yyyy");

                        //if (dr["AMENDED_CHECKED_ON"] != DBNull.Value)
                        //    lblAmendedCheckedOn.Text = Convert.ToDateTime(dr["AMENDED_CHECKED_ON"]).ToString("dd-MMM-yyyy");

                        //if (dr["AMENDED_PASSED_ON"] != DBNull.Value)
                        //    lblAmendedPassedOn.Text = Convert.ToDateTime(dr["AMENDED_PASSED_ON"]).ToString("dd-MMM-yyyy");

                        //if (dr["SETTLED_ON"] != DBNull.Value)
                        //{
                        //    if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        //        lblAmendedSettledOn.Text = Convert.ToDateTime(dr["SETTLED_ON"]).ToString("dd-MMM-yyyy");
                        //    else
                        //        lblSettledOn.Text = Convert.ToDateTime(dr["SETTLED_ON"]).ToString("dd-MMM-yyyy");
                    }
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
            GetHTMLString();

            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=MRN_" + "lblTourSanctionNo" + ".pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            ////this.Page.RenderControl(hw);
            //pnlView.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);//(PageSize.A4, 10f, 10f, 100f, 0f);
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
                        cssResolver.AddCssFile(System.Web.HttpContext.Current.Server.MapPath("~/Styles/viewpdftablecss.css"), true);
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

    //private string GetANDBinDDetail()
    //{

    //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/Images/COPERION/"));
    //    FileInfo[] listfiles = dirInfo.GetFiles("*.*");

    //    HtmlGenericControl newDiv = new HtmlGenericControl("div");

    //    if (listfiles.Length > 0)
    //    {
    //        foreach (FileInfo file in listfiles)
    //        {
    //            if (file.Extension == ".jpg" || file.Extension == ".jpeg"
    //                    || file.Extension == ".png" || file.Extension == ".bmp" || file.Extension == ".gif")
    //            {
    //                HtmlImage img = new HtmlImage();


    //                img.Src = "~/Images/COPERION/" + file.Name;
    //                img.Width = 130;
    //                img.Height = 130;

    //                newDiv.Attributes.Add("style", "padding:5px 3px; margin:20px 3px; height:auto;");
    //                newDiv.Controls.Add(img);

    //                dvPDF.Controls.Add(newDiv);
    //            }
    //        }
    //    }

    //    string html = "<html><body> <form id='form2' runat='server'><table id='tblPDF'><tr><td rowspan='2' colspan='3'>" + dvPDF.InnerHtml + "</td></tr></table></form></ body></html>";
    //    return html;
    //}

    private string GetHTMLString()
    {
        HtmlImage img = new HtmlImage();
        //HtmlGenericControl img = new HtmlGenericControl();
        img.Attributes.Add("heignt", "100px");
        img.Attributes.Add("width", "200px");
        img.Attributes.Add("src", "~/Images/COPERION/coperion_logo.jpg");

        string dd = string.Empty;
        dd = img.ToString();
        string html = "<html><body><form id='form2' runat='server'><table id='tblPDF'><tr><td rowspan='2' colspan='3'>" + dd + "</td></tr></table></form></ body></html>";
        return html;
    }
}