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

public partial class TOUR_AND_TRAVELS_TOUR_TourInfoInPDFNew : System.Web.UI.Page
{
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
            BAL.TourAndTravels objTourAndTravels = new TourAndTravels();
            DataSet ds = new DataSet();
            if (Request.QueryString["tourID"] != null)
            {
                ds = objTourAndTravels.GetTourInformationForPDF(Convert.ToInt32(Request.QueryString["tourID"]));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["TOUR_SANCTION_NO"] != DBNull.Value)
                            lblTourSanctionNo.Text = Convert.ToString(dr["TOUR_SANCTION_NO"]);

                        if (dr["TOUR_NO"] != DBNull.Value)
                        {
                            lblTourNo.Text = Convert.ToString(dr["TOUR_NO"]);
                            PublicValuesfortipdf.tourNo = lblTourNo.Text;
                        }
                        if (dr["EMPLOYEE_NAME"] != DBNull.Value)
                            lblEmplyoeeName.Text = Convert.ToString(dr["EMPLOYEE_NAME"]);

                        if (dr["EMPLOYEE_ID"] != DBNull.Value)
                            lblEmployeeID.Text = Convert.ToString(dr["EMPLOYEE_ID"]);

                        if (dr["DESIGNATION"] != DBNull.Value)
                            lblDesignation.Text = Convert.ToString(dr["DESIGNATION"]);

                        if (dr["START_DATE"] != DBNull.Value)
                            lblStartDateOfTour.Text = Convert.ToDateTime(dr["START_DATE"]).ToString("dd-MMM-yyyy");

                        if (dr["END_DATE"] != DBNull.Value)
                            lblEndDateOfTour.Text = Convert.ToDateTime(dr["END_DATE"]).ToString("dd-MMM-yyyy");

                        if (dr["CUST_VEND_NAME"] != DBNull.Value)
                            lblCustVendName.Text = Convert.ToString(dr["CUST_VEND_NAME"]);

                        if (dr["PLACE_OF_VISIT"] != DBNull.Value)
                            lblPlaceOfVisit.Text = Convert.ToString(dr["PLACE_OF_VISIT"]);

                        if (dr["VISIT_TYPE"] != DBNull.Value)
                            lblPurposeOfVisit.Text = Convert.ToString(dr["VISIT_TYPE"]);

                        if (dr["JOB_NO"] != DBNull.Value)
                            lblJobNo.Text = Convert.ToString(dr["JOB_NO"]);

                        if (dr["BUS_SEGMENT"] != DBNull.Value)
                            lblBusinessSegment.Text = Convert.ToString(dr["BUS_SEGMENT"]);

                        if (dr["TRAVEL_MODE"] != DBNull.Value)
                            lblModeOfTravel.Text = Convert.ToString(dr["TRAVEL_MODE"]);

                        if (dr["TRIP_TYPE"] != DBNull.Value)
                            lblTypeOfTrip.Text = Convert.ToString(dr["TRIP_TYPE"]);

                        if (dr["LOCAL_TRAVEL_TYPE"] != DBNull.Value)
                            lblLocalTravelling.Text = Convert.ToString(dr["LOCAL_TRAVEL_TYPE"]);

                        if (dr["EXPENDITURE_AMT"] != DBNull.Value)
                            lblExpectedExpenditure.Text = Convert.ToString(dr["EXPENDITURE_AMT"]);

                        if (dr["EXPENDITURE_CURRENCY"] != DBNull.Value)
                            lblExpectedExpenditureCurrency.Text = Convert.ToString(dr["EXPENDITURE_CURRENCY"]);

                        if (dr["ADVANCE_AMT"] != DBNull.Value)
                            lblAdvanceRequired.Text = Convert.ToString(dr["ADVANCE_AMT"]);

                        if (dr["ADVANCE_CURRENCY"] != DBNull.Value)
                            lblAdvanceRequiredCurrency.Text = Convert.ToString(dr["ADVANCE_CURRENCY"]);

                        if (dr["CREATED_BY"] != DBNull.Value)
                            lblCreatedBy.Text = Convert.ToString(dr["CREATED_BY"]);

                        if (dr["APPROVED_BY"] != DBNull.Value)
                            lblApprovedBy.Text = Convert.ToString(dr["APPROVED_BY"]);

                        if (dr["DELETED_BY"] != DBNull.Value)
                            lblDeletedBy.Text = Convert.ToString(dr["DELETED_BY"]);

                        if (dr["CANCELLED_BY"] != DBNull.Value)
                            lblCancelledBy.Text = Convert.ToString(dr["CANCELLED_BY"]);

                        if (dr["CREATED_ON"] != DBNull.Value)
                            lblCreatedOn.Text = Convert.ToDateTime(dr["CREATED_ON"]).ToString("dd-MMM-yyyy");

                        if (dr["APPROVED_ON"] != DBNull.Value)
                            lblApprovedOn.Text = Convert.ToDateTime(dr["APPROVED_ON"]).ToString("dd-MMM-yyyy");

                        if (dr["DELETED_ON"] != DBNull.Value)
                            lblDeletedOn.Text = Convert.ToDateTime(dr["DELETED_ON"]).ToString("dd-MMM-yyyy");

                        if (dr["CANCELLED_ON"] != DBNull.Value)
                            lblCancelledOn.Text = Convert.ToDateTime(dr["CANCELLED_ON"]).ToString("dd-MMM-yyyy");

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
            Response.AddHeader("content-disposition", "attachment;filename=TourInformation_" + PublicValuesfortipdf.tourNo + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 50f, 0f);//(PageSize.A4, 10f, 10f, 100f, 0f);
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

public static class PublicValuesfortipdf
{
    public static string tourNo = string.Empty;
}
