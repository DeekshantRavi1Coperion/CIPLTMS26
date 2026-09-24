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
using System.IO;
using System.Net.Mail;

public partial class TOUR_AND_TRAVELS_TRAVEL_UpdateTravelStatementStatusThree : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();

    //string visitRptSummaryOneFileName = string.Empty;
    //string visitRptSummaryTwoFileName = string.Empty;
    //string visitRptSummaryThreeFileName = string.Empty;

    string remarks = string.Empty;
    string dnNo = string.Empty;
    string amountDated = string.Empty;
    double finalAmount = 0;
    int finalAmountCurrencyID = 0;

    DataSet dsCurrency = new DataSet();
    DataSet dsTravelStatementDetails = new DataSet();
    DataSet dsUserInfo = new DataSet();

    string userName = string.Empty;
    string password = string.Empty;

    BAL.Common objCommon = new BAL.Common();

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Request.QueryString["statementid"]) > 0)
        {
            Session["dsTravelStatementDetails"] = null;
            ValidateAndLoginAndApprove();
            if (!IsPostBack)
            {
                hdConfirmValue.Value = "0";
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnViewAttachment1_Click(object sender, ImageClickEventArgs e)
    {
        int statementID = Convert.ToInt32(Request.QueryString["statementid"]);
        if (statementID > 0)
        {
            ViewAttachedFilesNew01(statementID, "ATTACHMENT1");
        }
    }

    protected void btnViewAttachment2_Click(object sender, ImageClickEventArgs e)
    {
        int statementID = Convert.ToInt32(Request.QueryString["statementid"]);
        if (statementID > 0)
        {
            ViewAttachedFilesNew01(statementID, "ATTACHMENT2");
        }
    }

    protected void btnViewAttachment3_Click(object sender, ImageClickEventArgs e)
    {
        int statementID = Convert.ToInt32(Request.QueryString["statementid"]);
        if (statementID > 0)
        {
            ViewAttachedFilesNew01(statementID, "ATTACHMENT3");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            int statementID = Convert.ToInt32(Request.QueryString["statementid"]);
            DataTable dt = (DataTable)Session["dsTravelStatementDetails"];
            if (dt.Rows.Count > 0)
            {
                btnSubmit.Visible = false;
                int statusID = Convert.ToInt32(dt.Rows[0]["STATUS_ID"]);
                string sanctionNo = Convert.ToString(dt.Rows[0]["TOUR_SANCTION_NO"]);

                if (statusID == 1 || statusID == 8)
                {
                    btnSubmit.Visible = true;
                    UpdateTravelStatementStatus(statementID, statusID, sanctionNo);
                }
                else if (statusID == 2)
                {
                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " already approved.");
                }
                else if (statusID == 3)
                {
                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " already checked.");
                }
                else if (statusID == 4)
                {
                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " already passed.");
                }
                else if (statusID == 5)
                {
                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " already settled.");
                }
                else if (statusID == 7)
                {
                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " sent to amendment.");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }

    protected void btnTravelStatementDetails_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dsTravelStatementDetails"];
        if (dt.Rows.Count > 0)
        {
            string sanctionNo = Convert.ToString(dt.Rows[0]["TOUR_SANCTION_NO"]);
            Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/TravelStatementListNewTwo.aspx?sanctionno=" + sanctionNo);
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    #endregion


    #region METHODS[=====================]

    private void ValidateAndLoginAndApprove()
    {
        try
        {
            DataSet ds = new DataSet();
            string userName = string.Empty;
            string password = string.Empty;
            int tourID = Convert.ToInt32(Request.QueryString["tourid"]);
            userName = Convert.ToString(Request.QueryString["ud"]).Trim();
            password = Convert.ToString(Request.QueryString["pd"]).Trim();

            ds = objCommon.ValidateAndLogin(userName, password);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["EMP_RECORD_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["EMP_RECORD_ID"]);
                Session["EMPLOYEE_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                Session["USER_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_NAME"]);
                Session["PASSWORD"] = Convert.ToString(ds.Tables[0].Rows[0]["PASSWORD"]);
                Session["EMAIL_ID"] = Convert.ToString(ds.Tables[0].Rows[0]["EMAIL_ID"]);
                Session["USER_TYPE"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_TYPE"]);
                Session["IS_TEAMLEADER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["IS_TEAMLEADER"]);
                Session["TEAMLEADER_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TEAMLEADER_ID"]);
                Session["DEPARTMENT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]);
                Session["TIMESHEET_DEPT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TIMESHEET_DEPT_ID"]);
                Session["TS_APPROVAL_BY"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TS_APPROVAL_BY"]);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    Session["TEAMMEMBERS"] = ds.Tables[1];
                }

                if (ds.Tables.Count > 0)
                {
                    Session["USERINFO"] = ds;
                }

                BindTravelStatementDetails();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTravelStatementDetails()
    {
        try
        {
            btnSubmit.Visible = false;
            int statusID = 0;
            int empRecordID = 0;
            int tlEmpRecordID = 0;

            int statementID = Convert.ToInt32(Request.QueryString["statementid"]);

            dsTravelStatementDetails = objTourAndTravels.GetTravelStatementDetailsByStatementID(statementID);

            if (dsTravelStatementDetails.Tables.Count > 0)
            {
                Session["dsTravelStatementDetails"] = dsTravelStatementDetails.Tables[0];

                if (dsTravelStatementDetails.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsTravelStatementDetails.Tables[0].Rows[0];

                    statusID = Convert.ToInt32(dr["STATUS_ID"]);
                    empRecordID = Convert.ToInt32(dr["EMP_RECORD_ID"]);
                    tlEmpRecordID = Convert.ToInt32(dr["TEAMLEADER_RECORD_ID"]);

                    if (dr["TOUR_SANCTION_NO"] != DBNull.Value)
                        lblLegend.Text = "Tour Sanction No. [" + Convert.ToString(dr["TOUR_SANCTION_NO"]) + "] Detail";
                    else
                        lblLegend.Text = "Travel Statement Detail";

                    if (dr["EMPLOYEE_ID"] != DBNull.Value)
                        txtEmployeeID.Text = Convert.ToString(dr["EMPLOYEE_ID"]);
                    else
                        txtEmployeeID.Text = string.Empty;

                    if (dr["EMPLOYEE_NAME"] != DBNull.Value)
                        txtEmployeeName.Text = Convert.ToString(dr["EMPLOYEE_NAME"]);
                    else
                        txtEmployeeName.Text = string.Empty;

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


                    string attachment1Txt = string.Empty;
                    string attachment1Extn = string.Empty;

                    if (dr["VISIT_RPT_SUMMARY_ONE_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["VISIT_RPT_SUMMARY_ONE_NAME"])))
                        attachment1Txt = Convert.ToString(dr["VISIT_RPT_SUMMARY_ONE_NAME"]);

                    if (!string.IsNullOrEmpty(attachment1Txt))
                    {
                        txtViewAttachment1.Text = attachment1Txt;
                        btnViewAttachment1.Visible = true;
                        attachment1Extn = Convert.ToString(attachment1Txt).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {

                            btnViewAttachment1.ImageUrl = "~/Images/imgicon1.png";
                            btnViewAttachment1.ToolTip = attachment1Txt;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            btnViewAttachment1.ImageUrl = "~/Images/pdficon1.png";
                            btnViewAttachment1.ToolTip = attachment1Txt;
                        }
                    }
                    else
                    {
                        txtViewAttachment1.Text = string.Empty;
                        btnViewAttachment1.Visible = false;
                    }

                    string attachment2Txt = string.Empty;
                    string attachment2Extn = string.Empty;

                    if (dr["VISIT_RPT_SUMMARY_TWO_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["VISIT_RPT_SUMMARY_TWO_NAME"])))
                        attachment2Txt = Convert.ToString(dr["VISIT_RPT_SUMMARY_TWO_NAME"]);

                    if (!string.IsNullOrEmpty(attachment2Txt))
                    {
                        txtViewAttachment2.Text = attachment2Txt;
                        btnViewAttachment2.Visible = true;
                        attachment2Extn = Convert.ToString(attachment2Txt).Split('.').Last();
                        if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
                        {
                            btnViewAttachment2.ImageUrl = "~/Images/imgicon1.png";
                            btnViewAttachment2.ToolTip = attachment2Txt;
                        }
                        else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
                        {
                            btnViewAttachment2.ImageUrl = "~/Images/pdficon1.png";
                            btnViewAttachment2.ToolTip = attachment2Txt;
                        }
                    }
                    else
                    {
                        txtViewAttachment2.Text = string.Empty;
                        btnViewAttachment2.Visible = false;
                    }

                    string attachment3Txt = string.Empty;
                    string attachment3Extn = string.Empty;

                    if (dr["VISIT_RPT_SUMMARY_THREE_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["VISIT_RPT_SUMMARY_THREE_NAME"])))
                        attachment3Txt = Convert.ToString(dr["VISIT_RPT_SUMMARY_THREE_NAME"]);

                    if (!string.IsNullOrEmpty(attachment3Txt))
                    {
                        txtViewAttachment3.Text = attachment3Txt;
                        btnViewAttachment3.Visible = true;
                        attachment3Extn = Convert.ToString(attachment3Txt).Split('.').Last();
                        if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" || attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
                        {
                            btnViewAttachment3.ImageUrl = "~/Images/imgicon1.png";
                            btnViewAttachment3.ToolTip = attachment3Txt;
                        }
                        else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
                        {
                            btnViewAttachment3.ImageUrl = "~/Images/pdficon1.png";
                            btnViewAttachment3.ToolTip = attachment3Txt;
                        }
                    }
                    else
                    {
                        txtViewAttachment3.Text = string.Empty;
                        btnViewAttachment3.Visible = false;
                    }

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
                    {
                        hdTotal.Value = Convert.ToString(dr["TOTAL_AMT"]);
                        txtTotal.Text = Convert.ToString(dr["TOTAL_AMT"]);
                    }
                    else
                    {
                        hdTotal.Value = "0";
                        txtTotal.Text = string.Empty;
                    }

                    if (dr["TOTAL_AMT_CURRENCY_CODE"] != DBNull.Value)
                        txtTotalCurrency.Text = Convert.ToString(dr["TOTAL_AMT_CURRENCY_CODE"]);

                    if (dr["ADVANCE_AMT"] != DBNull.Value)
                        txtAdvanceObtained.Text = Convert.ToString(dr["ADVANCE_AMT"]);
                    else
                        txtAdvanceObtained.Text = string.Empty;

                    if (dr["ADVANCE_CURRENCY_CODE"] != DBNull.Value)
                        txtAdvancAmtCurrency.Text = Convert.ToString(dr["ADVANCE_CURRENCY_CODE"]);
                    else
                        txtAdvancAmtCurrency.Text = string.Empty;


                    if (dr["ADJUSTED_AMT"] != DBNull.Value)
                    {
                        hdAdjustmentAmt.Value = Convert.ToString(dr["ADJUSTED_AMT"]);
                        txtAdjustmentAmt.Text = Convert.ToString(dr["ADJUSTED_AMT"]);
                    }
                    else
                    {
                        hdAdjustmentAmt.Value = "0";
                        txtAdjustmentAmt.Text = string.Empty;
                    }

                    if (dr["ADJUSTED_AMT_CURRENCY_CODE"] != DBNull.Value)
                        txtAdjustmentAmtCurrency.Text = Convert.ToString(dr["ADJUSTED_AMT_CURRENCY_CODE"]);

                    if (dr["IS_COST_RECOVERABLE"] != DBNull.Value)
                    {
                        if (Convert.ToString(dr["IS_COST_RECOVERABLE"]) == "YES")
                            ddlTourCostRecoverable.SelectedValue = "1";
                        else
                            ddlTourCostRecoverable.SelectedValue = "0";
                    }



                    if (Convert.ToInt32(hdAdjustmentAmt.Value) == 0)
                    {
                        txtAdjustmentAmtType.Text = string.Empty;
                    }
                    else if (Convert.ToInt32(hdAdjustmentAmt.Value) < 0)
                    {
                        txtAdjustmentAmtType.Text = "Payable";
                    }
                    else
                    {
                        txtAdjustmentAmtType.Text = "Recoverable";
                    }

                    if (dr["CREATED_REMARKS"] != DBNull.Value)
                        txtTravellerRemarks.Text = Convert.ToString(dr["CREATED_REMARKS"]);


                    if (statusID == 1 || statusID == 8)
                    {
                        btnSubmit.Visible = true;
                    }
                    else if (statusID == 2)
                    {
                        SuccessMessage("Tour Sanction No.: " + Convert.ToString(dr["TOUR_SANCTION_NO"]) + " already approved.");
                    }
                    else if (statusID == 3)
                    {
                        SuccessMessage("Tour Sanction No.: " + Convert.ToString(dr["TOUR_SANCTION_NO"]) + " already checked.");
                    }
                    else if (statusID == 4)
                    {
                        SuccessMessage("Tour Sanction No.: " + Convert.ToString(dr["TOUR_SANCTION_NO"]) + " already passed.");
                    }
                    else if (statusID == 5)
                    {
                        SuccessMessage("Tour Sanction No.: " + Convert.ToString(dr["TOUR_SANCTION_NO"]) + " already settled.");
                    }
                    else if (statusID == 7)
                    {
                        SuccessMessage("Tour Sanction No.: " + Convert.ToString(dr["TOUR_SANCTION_NO"]) + " sent to amendment.");
                    }
                    else if (statusID == 9)
                    {
                        SuccessMessage("Amended Tour Sanction No.: " + Convert.ToString(dr["TOUR_SANCTION_NO"]) + " already approved.");
                    }
                    else if (statusID == 10)
                    {
                        SuccessMessage("Amended Tour Sanction No.: " + Convert.ToString(dr["TOUR_SANCTION_NO"]) + " sent to checked.");
                    }
                    else if (statusID == 11)
                    {
                        SuccessMessage("Amended Tour Sanction No.: " + Convert.ToString(dr["TOUR_SANCTION_NO"]) + " sent to passed.");
                    }
                }
            }
            else
            {
                Session["dsTravelStatementDetails"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void ViewAttachedFilesNew01(int travelStatementID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            DataTable dsTravelStatementDetails = (DataTable)Session["dsTravelStatementDetails"];
            string fileName = Convert.ToString(dsTravelStatementDetails.Select("STATEMENT_ID='" + travelStatementID + "'"));
            string extn = string.Empty;
            foreach (DataRow dr in dsTravelStatementDetails.Select("STATEMENT_ID='" + travelStatementID + "'"))
            {
                if (fileType == "ATTACHMENT1")
                {
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_ONE_NAME"]);
                    bytes = (byte[])dr["VISIT_RPT_SUMMARY_ONE_DOC"];
                }
                else if (fileType == "ATTACHMENT2")
                {
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_TWO_NAME"]);
                    bytes = (byte[])dr["VISIT_RPT_SUMMARY_TWO_DOC"];
                }
                else if (fileType == "ATTACHMENT3")
                {
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_THREE_NAME"]);
                    bytes = (byte[])dr["VISIT_RPT_SUMMARY_THREE_DOC"];
                }
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewImageFile.ashx?travelStatementID=" + travelStatementID + "&fileType=" + fileType;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewPDFFile.aspx?travelStatementID=" + travelStatementID + "&fileType=" + fileType);
                    this.ModalPopupExtender3.Show();
                }
            }
            else
            {
                ExceptionMessage("File Doesn't exist!");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
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

    private void UpdateTravelStatementStatus(int statementID, int oldStatusID, string sanctionNo)//bool chkPass, 
    {
        try
        {
            int newStatusID = 0;
            int newActID = 0;

            string invoiceNumber = string.Empty;
            string invoiceDate = string.Empty;
            double invoiceAmount = 0;

            //For Approval
            if (oldStatusID == 1)
            {
                newActID = 2;
                newStatusID = 2;
            }
            //For Amended Approval
            else if (oldStatusID == 8)
            {
                newActID = 9;
                newStatusID = 9;
            }
            int amendmentCount = 0;

            DataTable dt = (DataTable)Session["dsTravelStatementDetails"];
            if (dt.Rows.Count > 0)
            {
                amendmentCount = Convert.ToInt32(dt.Rows[0]["AMENDMENT_COUNT"]) + 1;
            }

            //if (!string.IsNullOrEmpty(txtInvoiceNumber.Text))
            //    invoiceNumber = txtInvoiceNumber.Text.Trim().ToUpper();
            //else invoiceNumber = string.Empty;

            //if (!string.IsNullOrEmpty(hdInvoiceDate.Value))
            //    invoiceDate = Convert.ToDateTime(hdInvoiceDate.Value).ToString("yyyy-MM-dd");
            //else invoiceDate = string.Empty;

            //if (!string.IsNullOrEmpty(txtInvoiceAmount.Text))
            //    invoiceAmount = Convert.ToDouble(txtInvoiceAmount.Text);
            //else invoiceAmount = 0;


            //int value = objTourAndTravels.UpdateTravelStatementStatus(statementID, newStatusID, newActID, dnNo, amountDated, finalAmount, finalAmountCurrencyID, 
            //                                                          txtRemarks.Text, amendmentCount, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            //int value = objTourAndTravels.UpdateTravelStatementStatus(statementID, newStatusID, 0, 0, "", dnNo, amountDated, finalAmount, finalAmountCurrencyID, txtRemarks.Text,
            //                                                          amendmentCount, "", Convert.ToInt32(Session["EMP_RECORD_ID"]));


            int value = objTourAndTravels.UpdateTravelStatementStatus(statementID, newStatusID, 0, 0, "", dnNo, amountDated
                                                                  , finalAmount, finalAmountCurrencyID
                                                                  , txtRemarks.Text, amendmentCount, ""
                                                                  , invoiceNumber, invoiceDate, invoiceAmount
                                                                  , Convert.ToInt32(Session["EMP_RECORD_ID"]));
            int sendMailValue = 0;
            if (value > 0)
            {
                sendMailValue = SendMailNew01(statementID);

                if (sendMailValue > 0)
                {
                    int val = 0;
                    if (amendmentCount > 0)
                        val = objTourAndTravels.UpdateTravelStatementMailStatus(statementID, 9);
                    else
                        val = objTourAndTravels.UpdateTravelStatementMailStatus(statementID, 2);

                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " approved successfully and mail sent.");
                }
                else
                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " approved successfully, please send approved mail from Travel Statement List.");

                btnSubmit.Visible = false;
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

    private int SendMailNew01(int statementID)
    {
        try
        {
            string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);

            string sanctionNo = string.Empty;
            string statementNo = string.Empty;




            string status = string.Empty;
            int statusID = 0;
            int amendmentCount = 0;

            string subject = string.Empty;
            string from = string.Empty;
            string to = string.Empty;
            string cc = string.Empty;
            string bcc = string.Empty;
            string fileName = string.Empty;

            string createdBy = string.Empty;
            string createdByEmail = string.Empty;
            string createdRemark = string.Empty;

            string approvedBy = string.Empty;
            string approvedByEmail = string.Empty;
            string approvedOn = string.Empty;
            string approvedRemark = string.Empty;

            string amendedBy = string.Empty;
            string amendedByEmail = string.Empty;
            string amendedOn = string.Empty;
            string amendedRemark = string.Empty;

            string amendedApprovedBy = string.Empty;
            string amendedApprovedOn = string.Empty;
            string amendedApprovedByEmail = string.Empty;
            string amendedApprovedRemark = string.Empty;

            string hrTeamEmail = string.Empty;
            string passedByTeamEmail = string.Empty;

            string href = string.Empty;
            string link = string.Empty;

            dsUserInfo = objTourAndTravels.GetTravelRequesterInfo(Convert.ToInt32(Request.QueryString["statementid"]));

            if (dsUserInfo.Tables.Count > 0)
            {
                if (dsUserInfo.Tables[0].Rows.Count > 0)
                {
                    if (dsUserInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"] != DBNull.Value)
                        sanctionNo = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"]);
                    else sanctionNo = string.Empty;
                    if (dsUserInfo.Tables[0].Rows[0]["STATEMENT_NO"] != DBNull.Value)
                        statementNo = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["STATEMENT_NO"]);
                    else statementNo = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["STATUS_NAME"] != DBNull.Value)
                        status = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["STATUS_NAME"]);
                    else status = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["STATUS_ID"] != DBNull.Value)
                        statusID = Convert.ToInt32(dsUserInfo.Tables[0].Rows[0]["STATUS_ID"]);
                    else statusID = 0;

                    if (dsUserInfo.Tables[0].Rows[0]["EMPLOYEE_NAME"] != DBNull.Value)
                        createdBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                    else createdBy = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["EMP_EMAIL"] != DBNull.Value)
                        createdByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EMP_EMAIL"]);
                    else createdByEmail = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["CREATED_REMARKS"] != DBNull.Value)
                        createdRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["CREATED_REMARKS"]);
                    else createdRemark = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["APPROVED_BY"] != DBNull.Value)
                        approvedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["APPROVED_BY"]);
                    else approvedBy = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["APPROVED_BY_EMAIL"] != DBNull.Value)
                        approvedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["APPROVED_BY_EMAIL"]);
                    else approvedByEmail = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["APPROVED_ON"] != DBNull.Value)
                        approvedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    else approvedOn = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["APPROVED_REMARKS"] != DBNull.Value)
                        approvedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["APPROVED_REMARKS"]);
                    else approvedRemark = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["AMENDMENT_COUNT"] != DBNull.Value)
                        amendmentCount = Convert.ToInt32(dsUserInfo.Tables[0].Rows[0]["AMENDMENT_COUNT"]);
                    else amendmentCount = 0;

                    if (dsUserInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"] != DBNull.Value)
                        amendedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_BY"]);
                    else amendedBy = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["AMENDED_ON"] != DBNull.Value)
                        amendedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["AMENDED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    else amendedOn = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["AMENDED_BY_EMAILID"] != DBNull.Value)
                        amendedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_BY_EMAILID"]);
                    else amendedByEmail = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["AMENDED_REMARKS"] != DBNull.Value)
                        amendedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_REMARKS"]);
                    else amendedRemark = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_BY"] != DBNull.Value)
                        amendedApprovedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_BY"]);
                    else amendedApprovedBy = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_ON"] != DBNull.Value)
                        amendedApprovedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    else amendedApprovedOn = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_BY_EMAILID"] != DBNull.Value)
                        amendedApprovedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_BY_EMAILID"]);
                    else amendedApprovedByEmail = string.Empty;

                    if (dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_REMARKS"] != DBNull.Value)
                        amendedApprovedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_REMARKS"]);
                    else amendedApprovedRemark = string.Empty;
                }
                if (dsUserInfo.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsUserInfo.Tables[1].Rows)
                    {
                        hrTeamEmail += "," + Convert.ToString(dr["HR_EMAIL"]);
                    }
                    hrTeamEmail = hrTeamEmail.TrimStart(',');
                }

                if (dsUserInfo.Tables[2].Rows.Count > 0)
                {
                    passedByTeamEmail = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["PASSED_BY_EMAIL"]);
                }
            }

            link = "'" + urlTxt + "/Login.aspx?sanctionno=" + sanctionNo + "'";

            //Approved
            if (statusID == 2)
            {
                from = approvedByEmail;
                to = hrTeamEmail;
                cc = createdByEmail + "," + passedByTeamEmail;
                bcc = approvedByEmail;

                fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/02CheckTravelStatementMail.htm";
                subject = "Approval for Travel Statement, Sanction No.: " + sanctionNo;
                href = "<a href=" + link + ">Check Travel Statement</a>";
            }
            else if (statusID == 9)
            {
                from = approvedByEmail;
                to = hrTeamEmail;
                cc = createdByEmail + "," + passedByTeamEmail;
                bcc = approvedByEmail;

                fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/02CheckAmendedTravelStatementMail.htm";
                subject = "Approval for amended Travel Statement, Sanction No.: " + sanctionNo;
                href = "<a href=" + link + ">Check Amended Travel Statement</a>";
            }

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);


            if (!string.IsNullOrEmpty(to))
            {
                string[] strTo = to.Split(',');
                foreach (string item in strTo)
                {
                    mail.To.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(cc))
            {
                string[] strCC = cc.Split(',');
                foreach (string item in strCC)
                {
                    mail.CC.Add(item);
                }
            }

            mail.Subject = subject;
            mail.IsBodyHtml = true;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#sanctionno#}", sanctionNo);

            body = body.Replace("{#createdby#}", createdBy);
            body = body.Replace("{#createdremark#}", createdRemark);
            body = body.Replace("{#approvedby#}", approvedBy);
            body = body.Replace("{#approvedon#}", approvedOn);
            body = body.Replace("{#approvedremark#}", approvedRemark);

            body = body.Replace("{#amendedby#}", amendedBy);
            body = body.Replace("{#amendedon#}", amendedOn);
            body = body.Replace("{#amendedremark#}", amendedRemark);

            body = body.Replace("{#amendedapprovedby#}", amendedApprovedBy);
            body = body.Replace("{#amendedapprovedon#}", amendedApprovedOn);
            body = body.Replace("{#amendedapprovedremark#}", amendedApprovedRemark);


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
                ExceptionMessage(ex.ToString());
                return 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
        }
    }

    private void EnableControls()
    {
        txtAirfare.Enabled = true;
        txtMobile.Enabled = true;
        txtLodging.Enabled = true;
        txtTips.Enabled = true;
        txtMeals.Enabled = true;
        txtVisaFee.Enabled = true;
        txtGroundTransport.Enabled = true;
        txtDailyAllowance.Enabled = true;
        txtEntertainment.Enabled = true;
        txtOther.Enabled = true;
        txtGifts.Enabled = true;
    }

    private void DisableControls()
    {
        txtAirfare.Enabled = false;
        txtMobile.Enabled = false;
        txtLodging.Enabled = false;
        txtTips.Enabled = false;
        txtMeals.Enabled = false;
        txtVisaFee.Enabled = false;
        txtGroundTransport.Enabled = false;
        txtDailyAllowance.Enabled = false;
        txtEntertainment.Enabled = false;
        txtOther.Enabled = false;
        txtGifts.Enabled = false;
    }

    private void SuccessMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}


