using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TOUR_AND_TRAVELS_TRAVEL_TravelStatementOne : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsSanctionNo = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsTourInformaion = new DataSet();
    DataSet dsTravelStmtInfo = new DataSet();


    int tourID = 0;
    string visitRptSummaryOneFileName = string.Empty;
    string visitRptSummaryTwoFileName = string.Empty;
    string visitRptSummaryThreeFileName = string.Empty;
    double airfareAmt = 0;
    double telephoneMobAmt = 0;
    double lodgingAmt = 0;
    double tipsAmt = 0;
    double mealsAmt = 0;
    double visaFeeAmt = 0;
    double groundTransAmt = 0;
    double dailyAllowanceAmt = 0;
    double entertainmentAmt = 0;
    double otherAmt = 0;
    double giftsAmt = 0;
    double totalAmt = 0;
    int totalAmtCurrencyID = 0;
    //string adjustedAmt = string.Empty;
    //int adjustedAmtCurrencyID = 0;
    int isTourCostRec = 0;
    string remarks = string.Empty;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdConfirmValue.Value = "0";

                txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                BindSanctionNo();
                BindCurrency();
                txtDays.Text = (Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text)).Days.ToString();
                if (Convert.ToInt32(Request.QueryString["statementid"]) > 0)
                {
                    btnSubmit.Text = "Update";
                    GetTravelStatmentDetails();
                }
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void ddlSanctionNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
        pnlExceptionMsg.Visible = false;
        pnlSuccessMsg.Visible = false;
        if (ddlSanctionNo.SelectedIndex > 0)
        {
            dsTourInformaion = objTourAndTravels.GetTourInformaionBySanctionNo(Convert.ToString(ddlSanctionNo.SelectedItem.Text));
            if (dsTourInformaion.Tables.Count > 0 && dsTourInformaion.Tables[0].Rows.Count > 0)
            {
                txtTourNo.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["TOUR_NO"]);
                txtEmployeeName.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                txtEmployeeID.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["EMPLOYEE_ID"]);
                txtDesignation.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["DESIGNATION"]);
                txtStartDate.Text = Convert.ToDateTime(dsTourInformaion.Tables[0].Rows[0]["START_DATE"]).ToString("dd-MMM-yyyy");
                txtEndDate.Text = Convert.ToDateTime(dsTourInformaion.Tables[0].Rows[0]["END_DATE"]).ToString("dd-MMM-yyyy");
                txtCustVendName.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["CUST_VEND_NAME"]);
                txtPlaceOfVisit.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["PLACE_OF_VISIT"]);
                txtPurposeOfVisit.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["VISIT_TYPE"]);
                txtJobInqNo.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["JOB_NO"]);
                txtBusinessSegment.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["BUS_SEGMENT"]);
                txtAdvanceObtained.Text = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["ADVANCE_AMT"]);

                ddlAdvanceObtainedCurreny.SelectedValue = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["ADVANCE_CURRENCY"]);
                ddlTotalCurrency.SelectedValue = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["ADVANCE_CURRENCY"]);
                //ddlAdjustmentAmtCurrency.SelectedValue = Convert.ToString(dsTourInformaion.Tables[0].Rows[0]["ADVANCE_CURRENCY"]);

                txtDays.Text = (Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text)).Days.ToString();

                if (Convert.ToInt32(dsTourInformaion.Tables[0].Rows[0]["TRIP_TYPE_ID"]) == 1)
                    ddlTourCostRecoverable.SelectedValue = "1";
                else
                    ddlTourCostRecoverable.SelectedValue = "0";
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            InsertTravelStatement();
            BindSanctionNo();
        }
    }



    protected void btnTourList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/TravelStatementListNewOne.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void GetTravelStatmentDetails()
    {
        try
        {
            ddlSanctionNo.Enabled = false;
            DataTable dsTravelStatementDetails = (DataTable)Session["dsTravelStatementDetails"];
            foreach (DataRow dr in dsTravelStatementDetails.Select("STATEMENT_ID='" + Convert.ToInt32(Request.QueryString["statementid"]) + "'"))
            {
                lblTravelStatementNo.Text = "[" + Convert.ToString(dr["STATEMENT_NO"]) + "]";

                if (dr["TOUR_ID"] != DBNull.Value)
                    ddlSanctionNo.SelectedValue = Convert.ToString(dr["TOUR_ID"]);
                else
                    ddlSanctionNo.SelectedValue = "0";

                if (dr["EMPLOYEE_NAME"] != DBNull.Value)
                    txtEmployeeName.Text = Convert.ToString(dr["EMPLOYEE_NAME"]);
                else
                    txtEmployeeName.Text = string.Empty;

                if (dr["EMPLOYEE_ID"] != DBNull.Value)
                    txtEmployeeID.Text = Convert.ToString(dr["EMPLOYEE_ID"]);
                else
                    txtEmployeeID.Text = string.Empty;

                if (dr["TOUR_NO"] != DBNull.Value)
                    txtTourNo.Text = Convert.ToString(dr["TOUR_NO"]);
                else
                    txtTourNo.Text = string.Empty;

                if (dr["DESIGNATION"] != DBNull.Value)
                    txtDesignation.Text = Convert.ToString(dr["DESIGNATION"]);
                else
                    txtDesignation.Text = string.Empty;

                if (dr["START_DATE"] != DBNull.Value)
                    txtStartDate.Text = Convert.ToString(dr["START_DATE"]);
                else
                    txtStartDate.Text = string.Empty;

                if (dr["END_DATE"] != DBNull.Value)
                    txtEndDate.Text = Convert.ToString(dr["END_DATE"]);
                else
                    txtEndDate.Text = string.Empty;

                txtDays.Text = (Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text)).Days.ToString();

                if (dr["CUST_VEND_NAME"] != DBNull.Value)
                    txtCustVendName.Text = Convert.ToString(dr["CUST_VEND_NAME"]);
                else
                    txtCustVendName.Text = string.Empty;

                if (dr["PLACE_OF_VISIT"] != DBNull.Value)
                    txtPlaceOfVisit.Text = Convert.ToString(dr["PLACE_OF_VISIT"]);
                else
                    txtPlaceOfVisit.Text = string.Empty;

                if (dr["VISIT_TYPE"] != DBNull.Value)
                    txtPurposeOfVisit.Text = Convert.ToString(dr["VISIT_TYPE"]);
                else
                    txtPurposeOfVisit.Text = string.Empty;

                if (dr["JOB_NO"] != DBNull.Value)
                    txtJobInqNo.Text = Convert.ToString(dr["JOB_NO"]);
                else
                    txtJobInqNo.Text = string.Empty;

                if (dr["BUS_SEGMENT"] != DBNull.Value)
                    txtBusinessSegment.Text = Convert.ToString(dr["BUS_SEGMENT"]);
                else
                    txtBusinessSegment.Text = string.Empty;

                if (dr["AIRFARE_AMT"] != DBNull.Value)
                    txtAirfare.Text = Convert.ToString(dr["AIRFARE_AMT"]);
                else
                    txtAirfare.Text = string.Empty;

                if (dr["TELEPHONE_MOBILE_AMT"] != DBNull.Value)
                    txtMobile.Text = Convert.ToString(dr["TELEPHONE_MOBILE_AMT"]);
                else
                    txtMobile.Text = string.Empty;

                if (dr["LODGING_AMT"] != DBNull.Value)
                    txtLodging.Text = Convert.ToString(dr["LODGING_AMT"]);
                else
                    txtLodging.Text = string.Empty;

                if (dr["TIPS_AMT"] != DBNull.Value)
                    txtTips.Text = Convert.ToString(dr["TIPS_AMT"]);
                else
                    txtTips.Text = string.Empty;

                if (dr["MEALS_AMT"] != DBNull.Value)
                    txtMeals.Text = Convert.ToString(dr["MEALS_AMT"]);
                else
                    txtMeals.Text = string.Empty;

                if (dr["VISAFEE_AMT"] != DBNull.Value)
                    txtVisaFee.Text = Convert.ToString(dr["VISAFEE_AMT"]);
                else
                    txtVisaFee.Text = string.Empty;

                if (dr["GROUND_TRANSPORT_AMT"] != DBNull.Value)
                    txtGroundTransport.Text = Convert.ToString(dr["GROUND_TRANSPORT_AMT"]);
                else
                    txtGroundTransport.Text = string.Empty;

                if (dr["DAILY_ALLOWANCE_AMT"] != DBNull.Value)
                    txtDailyAllowance.Text = Convert.ToString(dr["DAILY_ALLOWANCE_AMT"]);
                else
                    txtDailyAllowance.Text = string.Empty;

                if (dr["ENTERTAINMENT_AMT"] != DBNull.Value)
                    txtEntertainment.Text = Convert.ToString(dr["ENTERTAINMENT_AMT"]);
                else
                    txtEntertainment.Text = string.Empty;

                if (dr["OTHER_AMT"] != DBNull.Value)
                    txtOther.Text = Convert.ToString(dr["OTHER_AMT"]);
                else
                    txtOther.Text = string.Empty;

                if (dr["GIFTS_AMT"] != DBNull.Value)
                    txtGifts.Text = Convert.ToString(dr["GIFTS_AMT"]);
                else
                    txtGifts.Text = string.Empty;

                if (dr["GIFTS_AMT"] != DBNull.Value)
                    txtGifts.Text = Convert.ToString(dr["GIFTS_AMT"]);
                else
                    txtGifts.Text = string.Empty;

                if (dr["TOTAL_AMT"] != DBNull.Value)
                    txtTotal.Text = Convert.ToString(dr["TOTAL_AMT"]);
                else
                    txtTotal.Text = string.Empty;

                if (dr["TOTAL_AMT_CURRENCY"] != DBNull.Value)
                    ddlTotalCurrency.SelectedValue = Convert.ToString(dr["TOTAL_AMT_CURRENCY"]);

                if (dr["ADVANCE_AMT"] != DBNull.Value)
                    txtAdvanceObtained.Text = Convert.ToString(dr["ADVANCE_AMT"]);
                else
                    txtAdvanceObtained.Text = string.Empty;

                //if (dr["ADVANCE_ADJUSTED_AMT"] != DBNull.Value)
                //    txtAdjustmentAmt.Text = Convert.ToString(dr["ADVANCE_ADJUSTED_AMT"]);
                //else
                //    txtAdjustmentAmt.Text = string.Empty;

                if (dr["IS_COST_RECOVERABLE"] != DBNull.Value)
                    if (Convert.ToString(dr["IS_COST_RECOVERABLE"]) == "YES")
                        ddlTourCostRecoverable.SelectedValue = "1";
                    else
                        ddlTourCostRecoverable.SelectedValue = "0";
                //else
                //txtAdjustmentAmt.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void BindSanctionNo()
    {
        try
        {
            if (Convert.ToInt32(Request.QueryString["statementid"]) > 0)
            {
                dsSanctionNo = objTourAndTravels.GetSanctionNo(Convert.ToInt32(Session["EMP_RECORD_ID"]), 1);
            }
            else
            {
                dsSanctionNo = objTourAndTravels.GetSanctionNo(Convert.ToInt32(Session["EMP_RECORD_ID"]), 0);
            }
            if (dsSanctionNo.Tables.Count > 0 && dsSanctionNo.Tables[0].Rows.Count > 0)
            {
                ddlSanctionNo.DataSource = dsSanctionNo.Tables[0];
                ddlSanctionNo.DataTextField = "TOUR_SANCTION_NO";
                ddlSanctionNo.DataValueField = "TOUR_ID";
                ddlSanctionNo.DataBind();
                ddlSanctionNo.Items.Insert(0, "Select");
                ddlSanctionNo.SelectedIndex = 0;
            }
            else
            {
                ddlSanctionNo.DataSource = null;
                ddlSanctionNo.Items.Clear();
                ddlSanctionNo.Items.Insert(0, "Select");
                ddlSanctionNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCurrency()
    {
        try
        {
            dsCurrency = objTourAndTravels.GetPrimaryDetails("sp_get_currency_list");
            if (dsCurrency.Tables.Count > 0 && dsCurrency.Tables[0].Rows.Count > 0)
            {
                ddlTotalCurrency.DataSource = dsCurrency.Tables[0];
                ddlTotalCurrency.DataTextField = "CURRENCY_CODE";
                ddlTotalCurrency.DataValueField = "CURRENCY_ID";
                ddlTotalCurrency.DataBind();
                ddlTotalCurrency.SelectedValue = "68";

                //ddlAdjustmentAmtCurrency.DataSource = dsCurrency.Tables[0];
                //ddlAdjustmentAmtCurrency.DataTextField = "CURRENCY_CODE";
                //ddlAdjustmentAmtCurrency.DataValueField = "CURRENCY_ID";
                //ddlAdjustmentAmtCurrency.DataBind();
                //ddlAdjustmentAmtCurrency.SelectedValue = "68";

                ddlAdvanceObtainedCurreny.DataSource = dsCurrency.Tables[0];
                ddlAdvanceObtainedCurreny.DataTextField = "CURRENCY_CODE";
                ddlAdvanceObtainedCurreny.DataValueField = "CURRENCY_ID";
                ddlAdvanceObtainedCurreny.DataBind();
                ddlAdvanceObtainedCurreny.SelectedValue = "68";
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void Reset()
    {
        try
        {
            txtTourNo.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtEmployeeID.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCustVendName.Text = string.Empty;
            txtPlaceOfVisit.Text = string.Empty;
            txtPurposeOfVisit.Text = string.Empty;
            txtJobInqNo.Text = string.Empty;
            txtBusinessSegment.Text = string.Empty;
            ddlVisitSummaryAttached.SelectedIndex = 0;

            txtAirfare.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtLodging.Text = string.Empty;
            txtTips.Text = string.Empty;
            txtMeals.Text = string.Empty;
            txtVisaFee.Text = string.Empty;
            txtGroundTransport.Text = string.Empty;
            txtDailyAllowance.Text = string.Empty;
            txtEntertainment.Text = string.Empty;
            txtOther.Text = string.Empty;
            txtGifts.Text = string.Empty;
            txtTotal.Text = "0.00";
            hdTotal.Value = "0";

            txtAdvanceObtained.Text = "0.00";
            //txtAdjustmentAmt.Text = "0";
            //hdAdjustmentAmt.Value = "0";
            //txtAdjustmentAmtType.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void InsertTravelStatement()
    {
        try
        {
            if (ddlSanctionNo.SelectedIndex > 0)
                tourID = Convert.ToInt32(ddlSanctionNo.SelectedValue);

            Byte[] visitRptSummaryOneBytes = null;
            if (fileUploadVisitSummary1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadVisitSummary1.PostedFile.FileName))
                {
                    visitRptSummaryOneFileName = fileUploadVisitSummary1.PostedFile.FileName;
                    visitRptSummaryOneBytes = GetFileBytes(fileUploadVisitSummary1.PostedFile.FileName, fileUploadVisitSummary1.PostedFile.InputStream);
                }
                else
                {
                    visitRptSummaryOneFileName = string.Empty;
                    visitRptSummaryOneBytes = null;
                }
            }
            else
            {
                visitRptSummaryOneFileName = string.Empty;
                visitRptSummaryOneBytes = null;
            }


            Byte[] visitRptSummaryTwoBytes = null;
            if (fileUploadVisitSummary2.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadVisitSummary2.PostedFile.FileName))
                {
                    visitRptSummaryTwoFileName = fileUploadVisitSummary2.PostedFile.FileName;
                    visitRptSummaryTwoBytes = GetFileBytes(fileUploadVisitSummary2.PostedFile.FileName, fileUploadVisitSummary2.PostedFile.InputStream);
                }
                else
                {
                    visitRptSummaryTwoFileName = string.Empty;
                    visitRptSummaryTwoBytes = null;
                }
            }
            else
            {
                visitRptSummaryTwoFileName = string.Empty;
                visitRptSummaryTwoBytes = null;
            }

            Byte[] visitRptSummaryThreeBytes = null;
            if (fileUploadVisitSummary3.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadVisitSummary3.PostedFile.FileName))
                {
                    visitRptSummaryThreeFileName = fileUploadVisitSummary3.PostedFile.FileName;
                    visitRptSummaryThreeBytes = GetFileBytes(fileUploadVisitSummary3.PostedFile.FileName, fileUploadVisitSummary3.PostedFile.InputStream);
                }
                else
                {
                    visitRptSummaryThreeFileName = string.Empty;
                    visitRptSummaryThreeBytes = null;
                }
            }
            else
            {
                visitRptSummaryThreeFileName = string.Empty;
                visitRptSummaryThreeBytes = null;
            }

            if (!string.IsNullOrEmpty(txtAirfare.Text))
                airfareAmt = Convert.ToDouble(txtAirfare.Text);
            else
                airfareAmt = 0;

            if (!string.IsNullOrEmpty(txtMobile.Text))
                telephoneMobAmt = Convert.ToDouble(txtMobile.Text);
            else
                telephoneMobAmt = 0;

            if (!string.IsNullOrEmpty(txtLodging.Text))
                lodgingAmt = Convert.ToDouble(txtLodging.Text);
            else
                lodgingAmt = 0;

            if (!string.IsNullOrEmpty(txtTips.Text))
                tipsAmt = Convert.ToDouble(txtTips.Text);
            else
                tipsAmt = 0;

            if (!string.IsNullOrEmpty(txtMeals.Text))
                mealsAmt = Convert.ToDouble(txtMeals.Text);
            else
                mealsAmt = 0;

            if (!string.IsNullOrEmpty(txtVisaFee.Text))
                visaFeeAmt = Convert.ToDouble(txtVisaFee.Text);
            else
                visaFeeAmt = 0;

            if (!string.IsNullOrEmpty(txtGroundTransport.Text))
                groundTransAmt = Convert.ToDouble(txtGroundTransport.Text);
            else
                groundTransAmt = 0;

            if (!string.IsNullOrEmpty(txtDailyAllowance.Text))
                dailyAllowanceAmt = Convert.ToDouble(txtDailyAllowance.Text);
            else
                dailyAllowanceAmt = 0;

            if (!string.IsNullOrEmpty(txtEntertainment.Text))
                entertainmentAmt = Convert.ToDouble(txtEntertainment.Text);
            else
                entertainmentAmt = 0;

            if (!string.IsNullOrEmpty(txtOther.Text))
                otherAmt = Convert.ToDouble(txtOther.Text);
            else
                otherAmt = 0;

            if (!string.IsNullOrEmpty(txtGifts.Text))
                giftsAmt = Convert.ToDouble(txtGifts.Text);
            else
                giftsAmt = 0;

            if (!string.IsNullOrEmpty(hdTotal.Value))
                totalAmt = Convert.ToDouble(hdTotal.Value);
            else
                totalAmt = 0;

            if (ddlTotalCurrency.SelectedIndex > 0)
                totalAmtCurrencyID = Convert.ToInt32(ddlTotalCurrency.SelectedValue);
            else
                totalAmtCurrencyID = 0;

            //if (!string.IsNullOrEmpty(hdAdjustmentAmt.Value))
            //    adjustedAmt = Convert.ToString(hdAdjustmentAmt.Value);
            //else
            //    adjustedAmt = string.Empty;


            //if (ddlAdjustmentAmtCurrency.SelectedIndex > 0)
            //    adjustedAmtCurrencyID = Convert.ToInt32(ddlAdjustmentAmtCurrency.SelectedValue);
            //else
            //    adjustedAmtCurrencyID = 0;


            if (ddlTourCostRecoverable.SelectedIndex > 0)
                isTourCostRec = Convert.ToInt32(ddlTourCostRecoverable.SelectedValue);
            else
                isTourCostRec = 0;

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;
            else
                remarks = string.Empty;

            int value = objTourAndTravels.InsertUpdateTravelStatement(0, tourID,
                                                            visitRptSummaryOneFileName, visitRptSummaryOneBytes,
                                                            visitRptSummaryTwoFileName, visitRptSummaryTwoBytes,
                                                            visitRptSummaryThreeFileName, visitRptSummaryThreeBytes,
                                                            airfareAmt, telephoneMobAmt, lodgingAmt, tipsAmt, mealsAmt,
                                                            visaFeeAmt, groundTransAmt, dailyAllowanceAmt, entertainmentAmt,
                                                            otherAmt, giftsAmt, totalAmt, totalAmtCurrencyID, "0", 0, isTourCostRec,
                                                            remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                int mailSentValue = SendMailNew(value);
                if (mailSentValue > 0)
                {
                    int val = objTourAndTravels.UpdateTravelStatementMailStatus(value, 1);
                    SuccessMessage("Travel Statement No. " + lblTravelStatementNo.Text + " added successfully and mail sent to HOD.");
                }
                else
                {
                    SuccessMessage("Travel Statement No. " + lblTravelStatementNo.Text + " added successfully, please send approval mail from Travel Statement List.");
                }
                Reset();
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private byte[] GetFileBytes(string fileName, Stream stream)
    {
        Byte[] GSTbytes = null;
        #region
        try
        {
            string GSTFilePath = fileName;
            string GSTFileName = Path.GetFileName(GSTFilePath);
            string GSText = Path.GetExtension(GSTFileName);
            string GSTContentType = String.Empty;
            switch (GSText)
            {
                case ".jpg":
                    GSTContentType = "image/jpg";
                    break;
                case ".jpeg":
                    GSTContentType = "image/jpeg";
                    break;
                case ".bmp":
                    GSTContentType = "image/bmp";
                    break;
                case ".png":
                    GSTContentType = "image/png";
                    break;
                case ".gif":
                    GSTContentType = "image/gif";
                    break;
                case ".pdf":
                    GSTContentType = "application/pdf";
                    break;
                case ".JPG":
                    GSTContentType = "image/JPG";
                    break;
                case ".JPEG":
                    GSTContentType = "image/JPEG";
                    break;
                case ".BMP":
                    GSTContentType = "image/BMP";
                    break;
                case ".PNG":
                    GSTContentType = "image/PNG";
                    break;
                case ".GIF":
                    GSTContentType = "image/GIF";
                    break;
                case ".PDF":
                    GSTContentType = "application/PDF";
                    break;
            }
            Stream GSTfs = null;
            BinaryReader GSTbr = null;
            if (GSTContentType != String.Empty)
            {
                try
                {
                    GSTfs = stream;
                    GSTfs.Position = 0;
                    GSTbr = new BinaryReader(GSTfs);
                    GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }

    private int SendMailNew(int travelStatementID)
    {
        try
        {
            string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);
            int tourid = 0;


            string startDateOfTour = string.Empty;
            string endDateOfTour = string.Empty;
            string placeOfVisit = string.Empty;
            string visitType = string.Empty;
            string jobNo = string.Empty;
            string businessSegment = string.Empty;
            string travelMode = string.Empty;
            string tripType = string.Empty;
            string localTravelType = string.Empty;
            
            string expectedExpenditureAmount = string.Empty;
            string expectedExpenditureCurrency = string.Empty;

            string totalExpenditureAmount = string.Empty;
            string totalExpenditureCurrency = string.Empty;

            string advanceAmount = string.Empty;
            string advanceCurrency = string.Empty;

            int statusid = 0;
            string from = string.Empty;
            string to = string.Empty;
            string cc = string.Empty;
            string sanctionNo = string.Empty;
            string createdOn = string.Empty;
            string reqName = string.Empty;
            string reqEmail = string.Empty;
            string reqRemarks = string.Empty;
            string tlEmail = string.Empty;

            string superAdminEmail = string.Empty;

            string hrdEmail = string.Empty;

            int tlEmpRecordID = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string href = string.Empty;
            string link = string.Empty;

            dsTravelStmtInfo = objTourAndTravels.GetTravelStatementNoInfo(travelStatementID);
            if (dsTravelStmtInfo.Tables.Count > 0)
            {
                if (dsTravelStmtInfo.Tables[0].Rows.Count > 0)
                {
                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TOUR_ID"] != DBNull.Value)
                        tourid = Convert.ToInt32(dsTravelStmtInfo.Tables[0].Rows[0]["TOUR_ID"]);
                    else tourid = 0;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"] != DBNull.Value)
                        sanctionNo = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"]);
                    else sanctionNo = string.Empty;




                    if (dsTravelStmtInfo.Tables[0].Rows[0]["START_DATE_OF_TRAVEL"] != DBNull.Value)
                        startDateOfTour = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["START_DATE_OF_TRAVEL"]);
                    else startDateOfTour = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["END_DATE_OF_TRAVEL"] != DBNull.Value)
                        endDateOfTour = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["END_DATE_OF_TRAVEL"]);
                    else endDateOfTour = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["PLACE_OF_VISIT"] != DBNull.Value)
                        placeOfVisit = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["PLACE_OF_VISIT"]);
                    else placeOfVisit = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["VISIT_TYPE"] != DBNull.Value)
                        visitType = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["VISIT_TYPE"]);
                    else visitType = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value)
                        jobNo = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["JOB_NO"]);
                    else jobNo = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["BUS_SEGMENT"] != DBNull.Value)
                        businessSegment = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["BUS_SEGMENT"]);
                    else businessSegment = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TRAVEL_MODE"] != DBNull.Value)
                        travelMode = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TRAVEL_MODE"]);
                    else travelMode = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TRIP_TYPE"] != DBNull.Value)
                        tripType = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TRIP_TYPE"]);
                    else tripType = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["LOCAL_TRAVEL_TYPE"] != DBNull.Value)
                        localTravelType = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["LOCAL_TRAVEL_TYPE"]);
                    else localTravelType = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EXPECTED_EXPENDITURE_AMT"] != DBNull.Value)
                        expectedExpenditureAmount = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EXPECTED_EXPENDITURE_AMT"]);
                    else expectedExpenditureAmount = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EXPECTED_EXPENDITURE_CURRENCY"] != DBNull.Value)
                        expectedExpenditureCurrency = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EXPECTED_EXPENDITURE_CURRENCY"]);
                    else expectedExpenditureCurrency = string.Empty;


                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EXPENDITURE_AMT"] != DBNull.Value)
                        totalExpenditureAmount = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EXPENDITURE_AMT"]);
                    else totalExpenditureAmount = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EXPENDITURE_CURRENCY"] != DBNull.Value)
                        totalExpenditureCurrency = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EXPENDITURE_CURRENCY"]);
                    else totalExpenditureCurrency = string.Empty;



                    if (dsTravelStmtInfo.Tables[0].Rows[0]["ADVANCE_AMT"] != DBNull.Value)
                        advanceAmount = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["ADVANCE_AMT"]);
                    else advanceAmount = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["ADVANCE_CURRENCY"] != DBNull.Value)
                        advanceCurrency = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["ADVANCE_CURRENCY"]);
                    else advanceCurrency = string.Empty;




                    if (dsTravelStmtInfo.Tables[0].Rows[0]["STATUS_ID"] != DBNull.Value)
                        statusid = Convert.ToInt32(dsTravelStmtInfo.Tables[0].Rows[0]["STATUS_ID"]);
                    else statusid = 0;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["CREATED_ON"] != DBNull.Value)
                        createdOn = Convert.ToDateTime(dsTravelStmtInfo.Tables[0].Rows[0]["CREATED_ON"]).ToString("dd-MMM-yyyy HH:mm:ss tt");
                    else createdOn = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EMPLOYEE_NAME"] != DBNull.Value)
                        reqName = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                    else reqName = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EMP_EMAIL"] != DBNull.Value)
                        reqEmail = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EMP_EMAIL"]);
                    else reqEmail = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["CREATED_REMARKS"] != DBNull.Value)
                        reqRemarks = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["CREATED_REMARKS"]);
                    else reqRemarks = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_USERNAME"] != DBNull.Value)
                        userName = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_USERNAME"]);
                    else userName = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_PASSWORD"] != DBNull.Value)
                        password = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_PASSWORD"]);
                    else password = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_RECORD_ID"] != DBNull.Value)
                        tlEmpRecordID = Convert.ToInt32(dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_RECORD_ID"]);
                    else tlEmpRecordID = 0;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_EMAIL"] != DBNull.Value)
                        tlEmail = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_EMAIL"]);
                    else tlEmail = string.Empty;
                }

                if (dsTravelStmtInfo.Tables[1].Rows.Count > 0)
                {
                    hrdEmail = Convert.ToString(dsTravelStmtInfo.Tables[1].Rows[0]["EMAIL_ID"]);
                }

                if (dsTravelStmtInfo.Tables[2].Rows.Count > 0)
                {
                    superAdminEmail = Convert.ToString(dsTravelStmtInfo.Tables[2].Rows[0]["SA_EMAIL"]);
                }
            }

            from = reqEmail;
            to = tlEmail;
            cc = superAdminEmail;

            if (tlEmpRecordID == 8)
            {
                to = hrdEmail;
            }


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(to))
            {
                string[] strTo = tlEmail.Split(',');
                foreach (string item in strTo)
                {
                    mail.To.Add(item);
                }
            }

            //if (!string.IsNullOrEmpty(cc))
            //{
            //    string[] strCC = cc.Split(',');
            //    foreach (string item in strCC)
            //    {
            //        mail.CC.Add(item);
            //    }
            //}

            mail.Subject = "Approval of Travel Statement, Sanction No.: " + sanctionNo;
            mail.IsBodyHtml = true;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/01ApprovalTravelStatementMail.htm")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#sanctionno#}", sanctionNo);


            body = body.Replace("{#startdateoftour#}", startDateOfTour);
            body = body.Replace("{#enddateoftour#}", endDateOfTour);
            body = body.Replace("{#placeofvisit#}", placeOfVisit);
            body = body.Replace("{#visittype#}", visitType);
            body = body.Replace("{#jobno#}", jobNo);
            body = body.Replace("{#businesssegment#}", businessSegment);
            body = body.Replace("{#travelmode#}", travelMode);
            body = body.Replace("{#triptype#}", tripType);
            body = body.Replace("{#localtraveltype#}", localTravelType);
            
            body = body.Replace("{#expectedexpenditureamount#}", expectedExpenditureAmount);
            body = body.Replace("{#expectedexpenditurecurrency#}", expectedExpenditureCurrency);

            body = body.Replace("{#totalexpenditureamount#}", totalExpenditureAmount);
            body = body.Replace("{#totalexpenditurecurrency#}", totalExpenditureCurrency);

            body = body.Replace("{#advanceamount#}", advanceAmount);
            body = body.Replace("{#advancecurrency#}", advanceCurrency);


            body = body.Replace("{#createdon#}", createdOn);
            body = body.Replace("{#createdby#}", reqName);
            body = body.Replace("{#createdremark#}", remarks);

            link = "'" + urlTxt + "/TOUR_AND_TRAVELS/TRAVEL/UpdateTravelStatementStatusThree.aspx?statementid=" + travelStatementID + "&actid=2&ud=" + userName + "&pd=" + password + "'";

            //link = "'" + urlTxt + "/Login.aspx?sanctionno=" + sanctionNo + "'";
            href = "<a href=" + link + ">Approve Travel Statement</a>";

            body = body.Replace("{#link#}", href);
            mail.Body = body;

            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                SmtpServer.Send(mail);
                return 1;
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    return 1;

                else
                    return 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
        }
    }

    private void SuccessMessage(string message)
    {
        pnlSuccessMsg.Visible = true;
        lblSuccessMsg.Text = message;
        lblSuccessMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlExceptionMsg.Visible = true;
        lblExceptionMsg.Text = message;
        lblExceptionMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
