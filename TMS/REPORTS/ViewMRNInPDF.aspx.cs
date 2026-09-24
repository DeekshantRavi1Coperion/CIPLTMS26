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
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class REPORTS_ViewMRNInPDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ViewDetail();
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
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=MRN_" + "lblTourSanctionNo" + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 50f, 0f);//(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        catch (Exception ex)
        {
            //
        }
    }
}