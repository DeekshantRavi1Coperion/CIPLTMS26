using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using BAL;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class TOUR_AND_TRAVELS_TRAVEL_TravelStatementSummaryList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsTourStatus = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsTourList = new DataSet();
    DataTable dtTeamMembers = new DataTable();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string sanctionNo = string.Empty;
    int statusID = 0;
    int empRecordID = 0;
    int teamMemberID = 0;
    string teamMembers = string.Empty;
    int departmentID = 0;
    string userTYpe = string.Empty;


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
    string adjustedAmt = string.Empty;
    int adjustedAmtCurrencyID = 0;
    int isTourCostRec = 0;
    string remarks = string.Empty;

    //int tatementStatusID = 0;

    string invoiceNumber = string.Empty;
    string invoiceDate = string.Empty;
    double invoiceAmount = 0;

    double disallowedAmount = 0;
    int disallowedAmountCurrencyID = 0;
    string reasonForDisallowing = string.Empty;

    string dnNo = string.Empty;
    string amountDated = string.Empty;
    double finalAmount = 0;
    int finalAmountCurrencyID = 0;
    //bool isMailSend = false;
    int amendmentCount = 0;
    string settlementType = string.Empty;

    DataSet dsCurrency = new DataSet();
    DataTable dsTravelStatementDetails = new DataTable();
    DataSet dsUserInfo = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdUpdationFlag.Value = "0";
                hdConfirmValue.Value = "0";

                Session["dsTravelStatementDetails"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");


                BindStatus();
                BindEmployee();
                //BindCurrency();

                ViewState["EMP_RECORD_ID"] = Session["EMP_RECORD_ID"];
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["sanctionno"])))
                    txtSanctionNo.Text = Convert.ToString(Request.QueryString["sanctionno"]);
                else
                    txtSanctionNo.Text = string.Empty;

                GetTravelStatementList();
            }
        }
        else
        {
            Session["dsTravelStatementDetails"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetTravelStatementList();
    }

    protected void btnAddNewTravelStmt_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/TravelStatementOne.aspx");
    }

    protected void gvTravelList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                //Reset();
                hdUpdationFlag.Value = "0";

                pnlMsg.Visible = false;
                int statementID = 0;
                int rowindex = 0;

                if (
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL"                    
                )
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                

                Label lblStatementID = gvTravelList.Rows[rowindex].FindControl("lblStatementID") as Label;
                Label lblSanctionNo = gvTravelList.Rows[rowindex].FindControl("lblSanctionNo") as Label;
                Label lblTourID = gvTravelList.Rows[rowindex].FindControl("lblTourID") as Label;
                Label lblCurrentStatusID = gvTravelList.Rows[rowindex].FindControl("lblCurrentStatusID") as Label;
                Label lblCurrentStatus = gvTravelList.Rows[rowindex].FindControl("lblCurrentStatus") as Label;
                Label lblAdvanceAmt = gvTravelList.Rows[rowindex].FindControl("lblAdvanceAmt") as Label;
                Label lblEmpRecordID = gvTravelList.Rows[rowindex].FindControl("lblEmpRecordID") as Label;
                Label lblEmployeeName = gvTravelList.Rows[rowindex].FindControl("lblEmployeeName") as Label;
                Label lblTeamLeaderID = gvTravelList.Rows[rowindex].FindControl("lblTeamLeaderID") as Label;
                Label lblAmendmentCount = gvTravelList.Rows[rowindex].FindControl("lblAmendmentCount") as Label;

                Label lblAddAttachment1 = gvTravelList.Rows[rowindex].FindControl("lblAddAttachment1") as Label;
                Label lblAddAttachment2 = gvTravelList.Rows[rowindex].FindControl("lblAddAttachment2") as Label;



                statementID = Convert.ToInt32(lblStatementID.Text);
                ViewState["STATEMENT_ID"] = Convert.ToInt32(lblStatementID.Text);
                ViewState["SANCTION_NO"] = Convert.ToString(lblSanctionNo.Text);
                ViewState["TOUR_ID"] = Convert.ToInt32(lblTourID.Text);
                ViewState["CURRENT_STATUS_ID"] = Convert.ToInt32(lblCurrentStatusID.Text);
                ViewState["CURRENT_STATUS"] = Convert.ToString(lblCurrentStatus.Text);
                ViewState["EMP_RECORD_ID"] = Convert.ToInt32(lblEmpRecordID.Text);
                ViewState["TEAMLEADER_ID"] = Convert.ToInt32(lblTeamLeaderID.Text);
                ViewState["AMENDMENT_COUNT"] = Convert.ToInt32(lblAmendmentCount.Text);

                //btnAmendment.Visible = false;
                //pnlAttachments.Visible = false;
                //pnlViewAttachments.Visible = false;

                //pnlTravellerRemarks.Visible = false;
                //pnlApprovedRemarks.Visible = false;
                //pnlCheckedRemarksHR.Visible = false;
                //pnlCheckedRemarksAcc.Visible = false;
                //pnlAccPassAmdRemarks.Visible = false;

                //pnlAcc.Visible = false;
                //pnlInvoiceBooking.Visible = false;

                int stID = Convert.ToInt32(lblCurrentStatusID.Text);
                string st = Convert.ToString(lblCurrentStatus.Text);

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                    ViewAttachedFilesNew01(statementID, "ATTACHMENT1");

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                    ViewAttachedFilesNew01(statementID, "ATTACHMENT2");

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                    ViewAttachedFilesNew01(statementID, "ATTACHMENT3");

                else if (Convert.ToString(e.CommandArgument) == "VIEWPASSPORT")
                {
                    ViewPassportFile(Convert.ToInt32(lblEmpRecordID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    ModalPopupExtender4.Show();
                    iframeViewTravelStatementInPDF.Attributes.Add("src", "TravelStatementInPDFNew.aspx?travelstatementid=" + statementID);
                }

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

    protected void gvTravelList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                Label lblStatementNo = (Label)e.Row.FindControl("lblStatementNo");
                Label lblSanctionNo = (Label)e.Row.FindControl("lblSanctionNo");
                Label lblCurrentStatus = (Label)e.Row.FindControl("lblCurrentStatus");
                Label lblCurrentStatusID = (Label)e.Row.FindControl("lblCurrentStatusID");
                Label lblEmpRecordID = (Label)e.Row.FindControl("lblEmpRecordID");
                Label lblTeamLeaderID = (Label)e.Row.FindControl("lblTeamLeaderID");
                Label lblAdvanceAmt = (Label)e.Row.FindControl("lblAdvanceAmt");
                Label lblAmendmentCount = (Label)e.Row.FindControl("lblAmendmentCount");
                Label lblTripTypeID = (Label)e.Row.FindControl("lblTripTypeID");

                Label lblIsApprovalMailSent = (Label)e.Row.FindControl("lblIsApprovalMailSent");
                Label lblIsApprovedMailSent = (Label)e.Row.FindControl("lblIsApprovedMailSent");
                Label lblIsCheckedMailSent = (Label)e.Row.FindControl("lblIsCheckedMailSent");
                Label lblIsPassedMailSent = (Label)e.Row.FindControl("lblIsPassedMailSent");
                Label lblIsSettledMailSent = (Label)e.Row.FindControl("lblIsSettledMailSent");
                Label lblIsAmendmentMailSent = (Label)e.Row.FindControl("lblIsAmendmentMailSent");

                Label lblIsAmendedMailSent = (Label)e.Row.FindControl("lblIsAmendedMailSent");
                Label lblIsAmendedApprovedMailSent = (Label)e.Row.FindControl("lblIsAmendedApprovedMailSent");
                Label lblIsAmendedCehckedMailSent = (Label)e.Row.FindControl("lblIsAmendedCehckedMailSent");
                Label lblIsAmendedPassedMailSent = (Label)e.Row.FindControl("lblIsAmendedPassedMailSent");

                Label lblVisitRptSummaryOneName = (Label)e.Row.FindControl("lblVisitRptSummaryOneName");
                ImageButton btnVisitRptSummaryOneName = (ImageButton)e.Row.FindControl("btnVisitRptSummaryOneName");
                Label lblVisitRptSummaryTwoName = (Label)e.Row.FindControl("lblVisitRptSummaryTwoName");
                ImageButton btnVisitRptSummaryTwoName = (ImageButton)e.Row.FindControl("btnVisitRptSummaryTwoName");
                Label lblVisitRptSummaryThreeName = (Label)e.Row.FindControl("lblVisitRptSummaryThreeName");
                ImageButton btnVisitRptSummaryThreeName = (ImageButton)e.Row.FindControl("btnVisitRptSummaryThreeName");

                //Label lblAddAttachment1 = (Label)e.Row.FindControl("lblAddAttachment1");
                //Label lblAddAttachment2 = (Label)e.Row.FindControl("lblAddAttachment2");
                //ImageButton imgBtnAddAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAddAttachment1");
                //ImageButton imgBtnAddAttachment2 = (ImageButton)e.Row.FindControl("imgBtnAddAttachment2");

                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                //ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");
                //ImageButton imgbtnSendMail = (ImageButton)e.Row.FindControl("imgbtnSendMail");
                //Button btnApproval = (Button)e.Row.FindControl("btnApproval");

                //imgProperties.Visible = false;
                //btnApproval.Visible = false;
                //imgbtnSendMail.Visible = false;
                imgStatus.Enabled = false;


                #region PASSPORT COPIES

                Label lblPassportCopyName = (Label)e.Row.FindControl("lblPassportCopyName");
                ImageButton btnPassportCopy = (ImageButton)e.Row.FindControl("btnPassportCopy");

                if (!string.IsNullOrEmpty(lblPassportCopyName.Text))
                {
                    btnPassportCopy.Visible = true;
                    btnPassportCopy.ToolTip = lblPassportCopyName.Text;
                }
                else
                {
                    btnPassportCopy.Visible = false;
                    btnPassportCopy.ToolTip = string.Empty;
                }

                string VisitRptSummaryOneNameExtn = string.Empty;
                string VisitRptSummaryTwoNameExtn = string.Empty;
                string VisitRptSummaryThreeNameExtn = string.Empty;

                if (!string.IsNullOrEmpty(lblVisitRptSummaryOneName.Text))
                {
                    VisitRptSummaryOneNameExtn = Convert.ToString(lblVisitRptSummaryOneName.Text).Split('.').Last();
                    if (VisitRptSummaryOneNameExtn == "jpg" || VisitRptSummaryOneNameExtn == "jepg" || VisitRptSummaryOneNameExtn == "bmp" || VisitRptSummaryOneNameExtn == "png" || VisitRptSummaryOneNameExtn == "gif" || VisitRptSummaryOneNameExtn == "JPG" || VisitRptSummaryOneNameExtn == "JPEG" || VisitRptSummaryOneNameExtn == "BMP" || VisitRptSummaryOneNameExtn == "PNG" || VisitRptSummaryOneNameExtn == "GIF")
                    {
                        btnVisitRptSummaryOneName.ImageUrl = "~/Images/imgicon1.png";
                        btnVisitRptSummaryOneName.ToolTip = lblVisitRptSummaryOneName.Text;
                    }
                    else if (VisitRptSummaryOneNameExtn == "pdf" || VisitRptSummaryOneNameExtn == "PDF")
                    {
                        btnVisitRptSummaryOneName.ImageUrl = "~/Images/pdficon1.png";
                        btnVisitRptSummaryOneName.ToolTip = lblVisitRptSummaryOneName.Text;
                    }
                }
                else
                {
                    btnVisitRptSummaryOneName.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblVisitRptSummaryTwoName.Text))
                {
                    VisitRptSummaryTwoNameExtn = Convert.ToString(lblVisitRptSummaryTwoName.Text).Split('.').Last();
                    if (VisitRptSummaryTwoNameExtn == "jpg" || VisitRptSummaryTwoNameExtn == "jepg" || VisitRptSummaryTwoNameExtn == "bmp" || VisitRptSummaryTwoNameExtn == "png" || VisitRptSummaryTwoNameExtn == "gif" || VisitRptSummaryTwoNameExtn == "JPG" || VisitRptSummaryTwoNameExtn == "JPEG" || VisitRptSummaryTwoNameExtn == "BMP" || VisitRptSummaryTwoNameExtn == "PNG" || VisitRptSummaryTwoNameExtn == "GIF")
                    {
                        btnVisitRptSummaryTwoName.ImageUrl = "~/Images/imgicon1.png";
                        btnVisitRptSummaryTwoName.ToolTip = lblVisitRptSummaryTwoName.Text;
                    }
                    else if (VisitRptSummaryTwoNameExtn == "pdf" || VisitRptSummaryTwoNameExtn == "PDF")
                    {
                        btnVisitRptSummaryTwoName.ImageUrl = "~/Images/pdficon1.png";
                        btnVisitRptSummaryTwoName.ToolTip = lblVisitRptSummaryTwoName.Text;
                    }
                }
                else
                {
                    btnVisitRptSummaryTwoName.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblVisitRptSummaryThreeName.Text))
                {
                    VisitRptSummaryThreeNameExtn = Convert.ToString(lblVisitRptSummaryThreeName.Text).Split('.').Last();
                    if (VisitRptSummaryThreeNameExtn == "jpg" || VisitRptSummaryThreeNameExtn == "jepg" || VisitRptSummaryThreeNameExtn == "bmp" || VisitRptSummaryThreeNameExtn == "png" || VisitRptSummaryThreeNameExtn == "gif" || VisitRptSummaryThreeNameExtn == "JPG" || VisitRptSummaryThreeNameExtn == "JPEG" || VisitRptSummaryThreeNameExtn == "BMP" || VisitRptSummaryThreeNameExtn == "PNG" || VisitRptSummaryThreeNameExtn == "GIF")
                    {
                        btnVisitRptSummaryThreeName.ImageUrl = "~/Images/imgicon1.png";
                        btnVisitRptSummaryThreeName.ToolTip = lblVisitRptSummaryThreeName.Text;
                    }
                    else if (VisitRptSummaryThreeNameExtn == "pdf" || VisitRptSummaryThreeNameExtn == "PDF")
                    {
                        btnVisitRptSummaryThreeName.ImageUrl = "~/Images/pdficon1.png";
                        btnVisitRptSummaryThreeName.ToolTip = lblVisitRptSummaryThreeName.Text;
                    }
                }
                else
                {
                    btnVisitRptSummaryThreeName.Visible = false;
                }

                #endregion


                #region Additional Attachments


                string addAttachment1Extn = string.Empty;
                string addAttachment2Extn = string.Empty;

                //if (!string.IsNullOrEmpty(lblAddAttachment1.Text))
                //{
                //    addAttachment1Extn = Convert.ToString(lblAddAttachment1.Text).Split('.').Last();
                //    if (addAttachment1Extn == "jpg" || addAttachment1Extn == "jepg" || addAttachment1Extn == "bmp" ||
                //        addAttachment1Extn == "png" || addAttachment1Extn == "gif" || addAttachment1Extn == "JPG" ||
                //        addAttachment1Extn == "JPEG" || addAttachment1Extn == "BMP" || addAttachment1Extn == "PNG" ||
                //        addAttachment1Extn == "GIF")
                //    {
                //        imgBtnAddAttachment1.ImageUrl = "~/Images/imgicon1.png";
                //        imgBtnAddAttachment1.ToolTip = lblAddAttachment1.Text;
                //    }
                //    else if (addAttachment1Extn == "pdf" || addAttachment1Extn == "PDF")
                //    {
                //        imgBtnAddAttachment1.ImageUrl = "~/Images/pdficon1.png";
                //        imgBtnAddAttachment1.ToolTip = lblAddAttachment1.Text;
                //    }
                //}

                //if (!string.IsNullOrEmpty(lblAddAttachment2.Text))
                //{
                //    addAttachment2Extn = Convert.ToString(lblAddAttachment2.Text).Split('.').Last();
                //    if (addAttachment2Extn == "jpg" || addAttachment2Extn == "jepg" || addAttachment2Extn == "bmp" ||
                //        addAttachment2Extn == "png" || addAttachment2Extn == "gif" || addAttachment2Extn == "JPG" ||
                //        addAttachment2Extn == "JPEG" || addAttachment2Extn == "BMP" || addAttachment2Extn == "PNG" ||
                //        addAttachment2Extn == "GIF")
                //    {
                //        imgBtnAddAttachment2.ImageUrl = "~/Images/imgicon1.png";
                //        imgBtnAddAttachment2.ToolTip = lblAddAttachment2.Text;
                //    }
                //    else if (addAttachment2Extn == "pdf" || addAttachment2Extn == "PDF")
                //    {
                //        imgBtnAddAttachment2.ImageUrl = "~/Images/pdficon1.png";
                //        imgBtnAddAttachment2.ToolTip = lblAddAttachment2.Text;
                //    }
                //}



                #endregion





                int tripTypeID = Convert.ToInt32(lblTripTypeID.Text);
                string currentStatus = lblCurrentStatus.Text;
                int currentStatusID = Convert.ToInt32(lblCurrentStatusID.Text);


                departmentID = Convert.ToInt32(Session["DEPARTMENT_ID"]);
                userTYpe = Convert.ToString(Session["USER_TYPE"]);
                int empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                int tsApprovalID = Convert.ToInt32(Session["TS_APPROVAL_BY"]);

                //1 for all, 2 for hod, 3 for hr, 4 for passed_by, 5 for settlement_by

                //New
                if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.New) || currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.New))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/New02.png";
                    imgStatus.ToolTip = "New";

                    //if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID)
                    //{
                    //    imgProperties.Visible = true;
                    //    imgProperties.ToolTip = "Edit Tour Sanction No.-: " + lblSanctionNo.Text;

                    //    if (Convert.ToInt32(lblIsApprovalMailSent.Text) == 0)
                    //    {
                    //        imgbtnSendMail.Visible = true;
                    //        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                    //            imgbtnSendMail.ToolTip = "Send Amended Approval Mail.";
                    //        else
                    //            imgbtnSendMail.ToolTip = "Send Approval Mail.";
                    //    }
                    //}

                    //if (Convert.ToInt32(lblTeamLeaderID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]) || (userTYpe == "A" && Convert.ToInt32(Session["EMP_RECORD_ID"])==117))
                    //if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID || tsApprovalID == 1)
                    //{
                    //    imgStatus.Enabled = true;
                    //    btnApproval.Visible = true;
                    //    btnApproval.ToolTip = "Approve Tour Sanction No.-: " + lblSanctionNo.Text;
                    //}
                }


                //Approved
                //else if ((currentStatus == "Approved" || currentStatusID == 2) || (currentStatus == "Amended-Approved" || currentStatusID == 9))
                else if ((currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Approved) ||
                          currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Approved)) ||
                         (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AmendedApproved) ||
                          currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedApproved)))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                    if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Approved) ||
                        currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Approved))
                    {
                        imgStatus.ToolTip = "Approved";
                        //if (empRecordID == 15 || empRecordID == 16 || empRecordID == 260 || empRecordID == 117)                        
                        //if (tsApprovalID == 3 || tsApprovalID == 1)
                        //{
                        //    imgStatus.Enabled = true;
                        //    btnApproval.Visible = true;
                        //    btnApproval.Text = "Check";
                        //    btnApproval.ToolTip = "Check Tour Sanction No.-: " + lblSanctionNo.Text;
                        //}
                        //if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                        //{
                        //    if (Convert.ToInt32(lblIsApprovedMailSent.Text) == 0)
                        //    {
                        //        imgbtnSendMail.Visible = true;
                        //        imgbtnSendMail.ToolTip = "Send Approved Mail.";
                        //    }
                        //}
                    }
                    else if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AmendedApproved) ||
                             currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedApproved))
                    {
                        imgStatus.ToolTip = "Amended-Approved";
                        //if (empRecordID == 15 || empRecordID == 16 || empRecordID == 260 || empRecordID == 117)
                        //if (tsApprovalID == 3 || tsApprovalID == 1)
                        //{
                        //    imgStatus.Enabled = true;
                        //    btnApproval.Visible = true;
                        //    btnApproval.Text = "Check Amended";
                        //    btnApproval.ToolTip = "Check Amended Tour Sanction No.-: " + lblSanctionNo.Text;
                        //}
                        //if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                        //{
                        //    if (Convert.ToInt32(lblIsAmendedApprovedMailSent.Text) == 0)
                        //    {
                        //        imgbtnSendMail.Visible = true;
                        //        imgbtnSendMail.ToolTip = "Send Amended Approved Mail.";
                        //    }
                        //}
                    }
                }

                //Checked
                //else if ((currentStatus == "Checked" || currentStatusID == 3) || (currentStatus == "Amended-Checked" || currentStatusID == 10))
                else if ((currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Checked) ||
                          currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Checked)) ||
                         (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AmendedChecked) ||
                          currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedChecked)))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Checked02.png";
                    if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Checked) ||
                        currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Checked))
                    {
                        imgStatus.ToolTip = "HR - Checked";
                        //if (empRecordID == 116)
                        /* if (tsApprovalID == 5 || tsApprovalID == 1 || tsApprovalID == 10)*///if (tsApprovalID == 4 || tsApprovalID == 1)
                        //if (tsApprovalID == 5 || tsApprovalID == 1 || tsApprovalID == 10)
                        //{
                        //    imgStatus.Enabled = true;
                        //    btnApproval.Visible = true;
                        //    btnApproval.Text = "Check";
                        //    btnApproval.ToolTip = "Check Tour Sanction No.-: " + lblSanctionNo.Text;
                        //}

                        ////if (empRecordID == 15 || empRecordID == 16 || empRecordID == 117)
                        //if (tsApprovalID == 3 || tsApprovalID == 1)
                        //{
                        //    if (Convert.ToInt32(lblIsCheckedMailSent.Text) == 0)
                        //    {
                        //        imgbtnSendMail.Visible = true;
                        //        imgbtnSendMail.ToolTip = "Send Checked Mail.";
                        //    }
                        //}
                    }
                    else if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AmendedChecked) ||
                             currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedChecked))
                    {
                        imgStatus.ToolTip = "HR - Amended-Checked";
                        //if (empRecordID == 116 || empRecordID == 117)
                        //if (tsApprovalID == 5 || tsApprovalID == 1) //(tsApprovalID == 4 || tsApprovalID == 1)
                        //if (tsApprovalID == 5 || tsApprovalID == 1 || tsApprovalID == 10)
                        //{
                        //    imgStatus.Enabled = true;
                        //    btnApproval.Visible = true;
                        //    btnApproval.Text = "Check Amended";
                        //    btnApproval.ToolTip = "Check Amended Tour Sanction No.-: " + lblSanctionNo.Text;
                        //}

                        ////if (empRecordID == 15 || empRecordID == 16)
                        //if (tsApprovalID == 3 || tsApprovalID == 1)
                        //{
                        //    if (Convert.ToInt32(lblIsAmendedCehckedMailSent.Text) == 0)
                        //    {
                        //        imgbtnSendMail.Visible = true;
                        //        imgbtnSendMail.ToolTip = "Send Amended Checked Mail.";
                        //    }
                        //}
                    }
                }





                //Account Checked
                else if ((currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AccChecked) ||
                          currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AccChecked)) ||
                         (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AmendedAccChecked) ||
                          currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedAccChecked)))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/amended.png";
                    if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AccChecked) ||
                        currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AccChecked))
                    {
                        imgStatus.ToolTip = "Account Checked";
                        //if (empRecordID == 116)
                        //if (tsApprovalID == 4 || tsApprovalID == 1)
                        //{
                        //    imgStatus.Enabled = true;
                        //    btnApproval.Visible = true;
                        //    btnApproval.Text = "Pass";
                        //    btnApproval.ToolTip = "Pass/Send To Amendment Tour Sanction No.-: " + lblSanctionNo.Text;
                        //}

                        ////if (tsApprovalID == 5 || tsApprovalID == 1)
                        //if (tsApprovalID == 5 || tsApprovalID == 1 || tsApprovalID == 10)
                        //{
                        //    if (Convert.ToInt32(lblIsCheckedMailSent.Text) == 0)
                        //    {
                        //        imgbtnSendMail.Visible = true;
                        //        imgbtnSendMail.ToolTip = "Send Checked Mail.";
                        //    }
                        //}
                    }
                    else if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AmendedAccChecked) ||
                             currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedAccChecked))
                    {
                        imgStatus.ToolTip = "Amended Account Checked";
                        //if (empRecordID == 116 || empRecordID == 117)
                        //if (tsApprovalID == 4 || tsApprovalID == 1)
                        //{
                        //    imgStatus.Enabled = true;
                        //    btnApproval.Visible = true;
                        //    btnApproval.Text = "Pass Amended";
                        //    btnApproval.ToolTip = "Pass/Send To Amendment Amended Tour Sanction No.-: " + lblSanctionNo.Text;
                        //}

                        ////if (empRecordID == 15 || empRecordID == 16)
                        ////if (tsApprovalID == 5 || tsApprovalID == 1)
                        //if (tsApprovalID == 5 || tsApprovalID == 1 || tsApprovalID == 10)
                        //{
                        //    if (Convert.ToInt32(lblIsAmendedCehckedMailSent.Text) == 0)
                        //    {
                        //        imgbtnSendMail.Visible = true;
                        //        imgbtnSendMail.ToolTip = "Send Amended Checked Mail.";
                        //    }
                        //}
                    }
                }



                //Passed
                //else if ((currentStatus == "Passed" && currentStatusID == 4) || (currentStatus == "Amended-Passed" && currentStatusID == 11))
                else if ((currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Passed) ||
                          currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Passed)) ||
                         (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AmendedPassed) ||
                          currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedPassed)))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Passed02.png";
                    if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Passed) ||
                        currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Passed))
                    {
                        imgStatus.ToolTip = "Passed";
                        //if (empRecordID == 3 || empRecordID == 117)
                        //if (tsApprovalID == 5 || tsApprovalID == 1)
                        //if (tsApprovalID == 5 || tsApprovalID == 1 || tsApprovalID == 10)
                        //{
                        //    //imgStatus.Enabled = true;
                        //    //btnApproval.Visible = true;
                        //    //btnApproval.Text = "Settle";
                        //    //btnApproval.ToolTip = "Settle Tour Sanction No.-: " + lblSanctionNo.Text;

                        //    //txtDNNo.Enabled = true;
                        //    ////txtFinalAmount.Enabled = true;
                        //    //imgBtnAccDated.Enabled = true;
                        //}

                        //if (empRecordID == 116 || empRecordID == 117)
                        //if (tsApprovalID == 4 || tsApprovalID == 1)
                        //{
                        //    if (Convert.ToInt32(lblIsPassedMailSent.Text) == 0)
                        //    {
                        //        imgbtnSendMail.Visible = true;
                        //        imgbtnSendMail.ToolTip = "Send Passed Mail.";
                        //    }
                        //}
                    }
                    else if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.AmendedPassed) || currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedPassed))
                    {
                        imgStatus.ToolTip = "Amended-Passed";
                        //if (empRecordID == 3 || empRecordID == 117)
                        //if (tsApprovalID == 5 || tsApprovalID == 1 || tsApprovalID == 10)
                        //{
                        //    imgStatus.Enabled = true;
                        //    btnApproval.Visible = true;
                        //    btnApproval.Text = "Settle Amended";
                        //    btnApproval.ToolTip = "Settle Amended Tour Sanction No.-: " + lblSanctionNo.Text;

                        //    //txtDNNo.Enabled = true;
                        //    ////txtFinalAmount.Enabled = true;
                        //    //imgBtnAccDated.Enabled = true;
                        //}

                        //if (empRecordID == 116 || empRecordID == 117)
                        //if (tsApprovalID == 4 || tsApprovalID == 1)
                        //{
                        //    if (Convert.ToInt32(lblIsAmendedPassedMailSent.Text) == 0)
                        //    {
                        //        imgbtnSendMail.Visible = true;
                        //        imgbtnSendMail.ToolTip = "Send Amended Passed Mail.";
                        //    }
                        //}
                    }
                }




                //Settled
                //else if (currentStatus == "Settled" && currentStatusID == 5)
                else if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Settled) ||
                         currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Settled))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Settled01.png";
                    imgStatus.ToolTip = "Settled";

                    //if (empRecordID == 3 || empRecordID == 117)
                    //if (tsApprovalID == 5 || tsApprovalID == 1 || tsApprovalID == 10)
                    //{
                    //    if (Convert.ToInt32(lblIsSettledMailSent.Text) == 0)
                    //    {
                    //        imgbtnSendMail.Visible = true;

                    //        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                    //            imgbtnSendMail.ToolTip = "Send Amended Settled Mail.";
                    //        else
                    //            imgbtnSendMail.ToolTip = "Send Settled Mail.";
                    //    }
                    //}


                    if (tsApprovalID == (int)TandTAllStatus.EnumTravelApprovalTypes.InvoiceBooked ||
                        tsApprovalID == (int)TandTAllStatus.EnumTravelApprovalTypes.All)
                    {
                        //imgStatus.Enabled = true;

                        //if (tripTypeID == (int)TandTAllStatus.EnumTripType.Reimbursable)
                        //{
                        //    btnApproval.Visible = true;
                        //}

                        //btnApproval.Text = "Book Invoice";
                        //btnApproval.ToolTip = "Book Invoice for Tour Sanction No.-: " + lblSanctionNo.Text;

                        //txtDNNo.Enabled = true;
                        ////txtFinalAmount.Enabled = true;
                        //imgBtnAccDated.Enabled = true;
                    }
                }

                //Amendment
                //else if (currentStatus == "Amendment" && currentStatusID == 7)
                else if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Amendment) ||
                         currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amendment))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Amendment01.png";
                    imgStatus.ToolTip = "Amendment";

                    //if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID)
                    //{
                    //    imgStatus.Enabled = true;
                    //    btnApproval.Visible = true;
                    //    btnApproval.Text = "Amend";
                    //    btnApproval.ToolTip = "Submit amendment of Tour Sanction No.-: " + lblSanctionNo.Text + " to HOD";
                    //}

                    ////if (empRecordID == 116 || empRecordID == 117)
                    //if (tsApprovalID == 4 || tsApprovalID == 1)
                    //{
                    //    if (Convert.ToInt32(lblIsAmendmentMailSent.Text) == 0)
                    //    {
                    //        imgbtnSendMail.Visible = true;
                    //        imgbtnSendMail.ToolTip = "Send Amendment Mail.";
                    //    }
                    //}
                }

                //Amended
                //else if (currentStatus == "Amended" && currentStatusID == 8)
                else if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.Amended) ||
                         currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amended))
                {
                    //imgProperties.Visible = false;
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Amended01.png";
                    imgStatus.ToolTip = "Amended";
                    //if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID)
                    //{
                    //    imgStatus.Enabled = true;
                    //    btnApproval.Visible = true;
                    //    btnApproval.Text = "Edit Amended";
                    //    btnApproval.ToolTip = "Edit Amended Tour Sanction No.-: " + lblSanctionNo.Text;

                    //    if (Convert.ToInt32(lblIsAmendedMailSent.Text) == 0)
                    //    {
                    //        imgbtnSendMail.Visible = true;
                    //        imgbtnSendMail.ToolTip = "Send Amended Mail.";
                    //    }
                    //}
                    //else if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                    //{
                    //    imgStatus.Enabled = true;
                    //    btnApproval.Visible = true;
                    //    btnApproval.Text = "Approve Amendment";
                    //    btnApproval.ToolTip = "Approve Tour Sanction No.-: " + lblSanctionNo.Text;
                    //}
                }



                //Invoice Booked
                else if (currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.InvoiceBooked) ||
                         currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.InvoiceBooked))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Booked.png";
                    imgStatus.ToolTip = "Invoice Booked";

                    //if (empRecordID == 3 || empRecordID == 117)
                    //if (tsApprovalID == (int)TandTAllStatus.EnumTravelApprovalTypes.InvoiceBooked)
                    //{
                    //    if (Convert.ToInt32(lblIsSettledMailSent.Text) == 0)
                    //    {
                    //        imgbtnSendMail.Visible = true;

                    //        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                    //            imgbtnSendMail.ToolTip = "Send Invoice Booked Mail for Amended Travel Statement.";
                    //        else
                    //            imgbtnSendMail.ToolTip = "Send Invoice Booked Mail.";
                    //    }
                    //}
                }




                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnViewAttachment1_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["STATEMENT_ID"]), "ATTACHMENT1");
        //ModalPopupExtender1.Show();
    }

    protected void btnViewAttachment2_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["STATEMENT_ID"]), "ATTACHMENT2");
        //ModalPopupExtender1.Show();
    }

    protected void btnViewAttachment3_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["STATEMENT_ID"]), "ATTACHMENT3");
        //ModalPopupExtender1.Show();
    }


    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    int statementID = Convert.ToInt32(ViewState["STATEMENT_ID"]);
    //    string sanctionNo = Convert.ToString(ViewState["SANCTION_NO"]);
    //    int tourID = Convert.ToInt32(ViewState["TOUR_ID"]);
    //    int amentmentCount = Convert.ToInt32(ViewState["AMENDMENT_COUNT"]);
    //    int currentStatusID = Convert.ToInt32(ViewState["CURRENT_STATUS_ID"]);
    //    string currentStatus = Convert.ToString(ViewState["CURRENT_STATUS"]);
    //    int newStatusID = Convert.ToInt32(ViewState["NEW_STATUS_ID"]);
    //    //int actID = Convert.ToInt32(ViewState["ACT_ID"]);


    //    //if (Convert.ToInt32(hdConfirmValue.Value) > 0)
    //    //{
    //    //    if ((currentStatusID == 1 || currentStatus == "New") && actID == 1)
    //    //    {
    //    //        UpdatedTravelStatement(statementID, tourID);
    //    //    }
    //    //    else if (newStatusID == 8 && actID == 8 && amentmentCount > 0)
    //    //    {
    //    //        UpdateTravelStatementAmendment(statementID, newStatusID);
    //    //    }
    //    //    else
    //    //    {
    //    //        UpdateTravelStatementStatus(amentmentCount, statementID, newStatusID, actID, sanctionNo);
    //    //    }

    //    //    Reset();
    //    //    GetTravelStatementList();
    //    //}


    //    if (Convert.ToInt32(hdConfirmValue.Value) > 0)
    //    {
    //        if ((currentStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.New) ||
    //            currentStatus == Convert.ToString(TandTAllStatus.EnumTravelStatus.New)) && Convert.ToInt32(hdUpdationFlag.Value) > 0)
    //        {
    //            UpdatedTravelStatement(statementID, tourID);
    //        }
    //        else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amended) && amentmentCount > 0)
    //        {
    //            UpdateTravelStatementAmendment(statementID, newStatusID);
    //        }
    //        else
    //        {
    //            UpdateTravelStatementStatus(amentmentCount, statementID, newStatusID, sanctionNo);
    //        }

    //        Reset();
    //        GetTravelStatementList();
    //    }

    //}

    //protected void btnAmendment_Click(object sender, EventArgs e)
    //{
    //    //ViewState["ACT_ID"] = 7;
    //    ViewState["NEW_STATUS_ID"] = Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amendment);

    //    int statementID = Convert.ToInt32(ViewState["STATEMENT_ID"]);
    //    string sanctionNo = Convert.ToString(ViewState["SANCTION_NO"]);
    //    int amentmentCount = Convert.ToInt32(ViewState["AMENDMENT_COUNT"]);
    //    int newStatusID = Convert.ToInt32(ViewState["NEW_STATUS_ID"]);
    //    int actID = Convert.ToInt32(ViewState["ACT_ID"]);

    //    UpdateTravelStatementStatus(amentmentCount, statementID, newStatusID, sanctionNo);
    //    GetTravelStatementList();
    //    Reset();
    //}

    //protected void SaveAdditionalAttachments_Click(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(hdAdditionalAttachmentConfirmValue.Value) > 0)
    //    {
    //        SaveAdditionalAttachments(Convert.ToInt32(ViewState["STATEMENT_ID"]), Convert.ToInt32(ViewState["ATTACHMENT_SEQ_NO"]));
    //    }
    //    else
    //    {
    //        mpeAddAdditionalAttachments.Show();
    //        ExceptionMessageAddAttachments("Please select attachment and try again..");
    //        return;
    //    }
    //}

    #endregion


    #region METHODS[=======================]

    private void BindStatus()
    {
        try
        {

            dsTourStatus = objTourAndTravels.GetTravelStatementStatus();
            if (dsTourStatus.Tables.Count > 0 && dsTourStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsTourStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;

                //if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["sanctionno"])))
                //    ddlStatus.SelectedIndex = 0;

                //else if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 4 && (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 15 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 16))
                //    ddlStatus.SelectedIndex = 2;

                //else if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 2 && Convert.ToInt32(Session["EMP_RECORD_ID"]) == 116)
                //    ddlStatus.SelectedIndex = 3;

                //else if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 2 && Convert.ToInt32(Session["EMP_RECORD_ID"]) == 3)
                //    ddlStatus.SelectedIndex = 4;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEmployee()
    {
        try
        {
            int currentEmpRecordId = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int departmentId = Convert.ToInt32(Session["DEPARTMENT_ID"]);
            string userType = Convert.ToString(Session["USER_TYPE"]);

            if (departmentId == 4 ||
                departmentId == 2 ||
                currentEmpRecordId == 117 ||
                userType == "A")
                dsEmployee = objTourAndTravels.GetEmployeeForTravel(0);
            else
            {
                dsEmployee = objTourAndTravels.GetEmployeeForTravel(currentEmpRecordId);
            }
                

            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    //private void BindCurrency()
    //{
    //    try
    //    {
    //        dsCurrency = objTourAndTravels.GetPrimaryDetails("sp_get_currency_list");
    //        if (dsCurrency.Tables.Count > 0 && dsCurrency.Tables[0].Rows.Count > 0)
    //        {
    //            ddlTotalCurrency.DataSource = dsCurrency.Tables[0];
    //            ddlTotalCurrency.DataTextField = "CURRENCY_CODE";
    //            ddlTotalCurrency.DataValueField = "CURRENCY_ID";
    //            ddlTotalCurrency.DataBind();
    //            ddlTotalCurrency.SelectedValue = "68";

    //            ddlAdvanceObtainedCurreny.DataSource = dsCurrency.Tables[0];
    //            ddlAdvanceObtainedCurreny.DataTextField = "CURRENCY_CODE";
    //            ddlAdvanceObtainedCurreny.DataValueField = "CURRENCY_ID";
    //            ddlAdvanceObtainedCurreny.DataBind();
    //            ddlAdvanceObtainedCurreny.SelectedValue = "68";


    //            ddlDisallowedAmountCurrency.DataSource = dsCurrency.Tables[0];
    //            ddlDisallowedAmountCurrency.DataTextField = "CURRENCY_CODE";
    //            ddlDisallowedAmountCurrency.DataValueField = "CURRENCY_ID";
    //            ddlDisallowedAmountCurrency.DataBind();
    //            ddlDisallowedAmountCurrency.SelectedValue = "68";

    //            ddlBillPassedAmountCurrency.DataSource = dsCurrency.Tables[0];
    //            ddlBillPassedAmountCurrency.DataTextField = "CURRENCY_CODE";
    //            ddlBillPassedAmountCurrency.DataValueField = "CURRENCY_ID";
    //            ddlBillPassedAmountCurrency.DataBind();
    //            ddlBillPassedAmountCurrency.SelectedValue = "68";

    //            ddlFinalAmountCurrency.DataSource = dsCurrency.Tables[0];
    //            ddlFinalAmountCurrency.DataTextField = "CURRENCY_CODE";
    //            ddlFinalAmountCurrency.DataValueField = "CURRENCY_ID";
    //            ddlFinalAmountCurrency.DataBind();
    //            ddlFinalAmountCurrency.SelectedValue = "68";


    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //
    //    }
    //}

    private void GetTravelStatementList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtSanctionNo.Text))
                sanctionNo = txtSanctionNo.Text.Trim();
            else
                sanctionNo = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusID = 0;

            if (ddlEmployee.SelectedIndex > 0)
            {
                teamMemberID = Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            else
            {
                teamMemberID = 0;
                if (Session["TEAMMEMBERS"] != null)
                {
                    dtTeamMembers = (DataTable)Session["TEAMMEMBERS"];
                    foreach (DataRow dr in dtTeamMembers.Rows)
                    {
                        teamMembers += "," + Convert.ToString(dr["EMP_RECORD_ID"]);
                    }
                    teamMembers = Convert.ToString(Session["EMP_RECORD_ID"]) + "," + teamMembers.TrimStart(',');
                }
                else
                {
                    teamMembers = Convert.ToString(Session["EMP_RECORD_ID"]);
                }
            }

            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsTourList = objTourAndTravels.GetTravelStatementList(startDate, endDate, sanctionNo, statusID, empRecordID, teamMemberID, teamMembers);

            if (dsTourList.Tables.Count > 0 && dsTourList.Tables[0].Rows.Count > 0)
            {
                Session["dsTravelStatementDetails"] = dsTourList.Tables[0];
                gvTravelList.DataSource = dsTourList.Tables[0];
                gvTravelList.DataBind();
            }
            else
            {
                Session["dsTravelStatementDetails"] = null;
                gvTravelList.DataSource = null;
                gvTravelList.DataBind();
            }
            lblRecords.Text = "Records[" + dsTourList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }




   

    private void ViewAttachedFilesNew01(int travelStatementID, string fileType)
    {
        try
        {
            //byte[] bytes = null;
            DataTable dsTravelStatementDetails = (DataTable)Session["dsTravelStatementDetails"];
            string fileName = Convert.ToString(dsTravelStatementDetails.Select("STATEMENT_ID='" + travelStatementID + "'"));
            string extn = string.Empty;
            foreach (DataRow dr in dsTravelStatementDetails.Select("STATEMENT_ID='" + travelStatementID + "'"))
            {
                if (fileType == "ATTACHMENT1")
                {
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_ONE_NAME"]);
                    //bytes = (byte[])dr["VISIT_RPT_SUMMARY_ONE_DOC"];
                }
                else if (fileType == "ATTACHMENT2")
                {
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_TWO_NAME"]);
                    //bytes = (byte[])dr["VISIT_RPT_SUMMARY_TWO_DOC"];
                }
                else if (fileType == "ATTACHMENT3")
                {
                    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_THREE_NAME"]);
                    //bytes = (byte[])dr["VISIT_RPT_SUMMARY_THREE_DOC"];
                }


                else if (fileType == "ADD_ATTACHMENT1")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT1_NAME"]);
                    //bytes = (byte[])dr["ATTACHMENT1_DOC"];
                }
                else if (fileType == "ADD_ATTACHMENT2")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT2_NAME"]);
                    //bytes = (byte[])dr["ATTACHMENT2_DOC"];
                }
                else if (fileType == "ADD_ATTACHMENT3")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT3_NAME"]);
                    //bytes = (byte[])dr["ATTACHMENT3_DOC"];
                }
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" ||
                    extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewAttachedImageFile.ashx?travelStatementID=" + travelStatementID + "&fileType=" + fileType;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?travelStatementID=" + travelStatementID + "&fileType=" + fileType);
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

    //private void EnableControls()
    //{
    //    ddlVisitSummaryAttached.Enabled = true;
    //    txtAirfare.Enabled = true;
    //    txtMobile.Enabled = true;
    //    txtLodging.Enabled = true;
    //    txtTips.Enabled = true;
    //    txtMeals.Enabled = true;
    //    txtVisaFee.Enabled = true;
    //    txtGroundTransport.Enabled = true;
    //    txtDailyAllowance.Enabled = true;
    //    txtEntertainment.Enabled = true;
    //    txtOther.Enabled = true;
    //    txtGifts.Enabled = true;
    //    //ddlTotalCurrency.Enabled = true;
    //    //ddlAdjustmentAmtCurrency.Enabled = true;
    //    //ddlTourCostRecoverable.Enabled = true;
    //}

    //private void DisableControls()
    //{
    //    ddlVisitSummaryAttached.Enabled = false;
    //    txtAirfare.Enabled = false;
    //    txtMobile.Enabled = false;
    //    txtLodging.Enabled = false;
    //    txtTips.Enabled = false;
    //    txtMeals.Enabled = false;
    //    txtVisaFee.Enabled = false;
    //    txtGroundTransport.Enabled = false;
    //    txtDailyAllowance.Enabled = false;
    //    txtEntertainment.Enabled = false;
    //    txtOther.Enabled = false;
    //    txtGifts.Enabled = false;
    //    ddlTotalCurrency.Enabled = false;
    //    //ddlAdjustmentAmtCurrency.Enabled = false;
    //    ddlTourCostRecoverable.Enabled = false;
    //}

    //private void GetTravelStatmentDetails(int statementID)
    //{
    //    try
    //    {
    //        dsTravelStatementDetails = (DataTable)Session["dsTravelStatementDetails"];
    //        foreach (DataRow dr in dsTravelStatementDetails.Select("STATEMENT_ID='" + statementID + "'"))
    //        {
    //            if (dr["TOUR_SANCTION_NO"] != DBNull.Value)
    //                lblLegend.Text = "Tour Sanction No. [" + Convert.ToString(dr["TOUR_SANCTION_NO"]) + "] Detail";
    //            else
    //                lblLegend.Text = "Travel Statement Detail";

    //            if (dr["EMPLOYEE_ID"] != DBNull.Value)
    //                txtEmployeeID.Text = Convert.ToString(dr["EMPLOYEE_ID"]);
    //            else
    //                txtEmployeeID.Text = string.Empty;

    //            if (dr["EMPLOYEE_NAME"] != DBNull.Value)
    //                txtEmployeeName.Text = Convert.ToString(dr["EMPLOYEE_NAME"]);
    //            else
    //                txtEmployeeName.Text = string.Empty;

    //            if (dr["DESIGNATION"] != DBNull.Value)
    //                txtDesignation.Text = Convert.ToString(dr["DESIGNATION"]);
    //            else
    //                txtDesignation.Text = string.Empty;

    //            if (dr["START_DATE"] != DBNull.Value)
    //                txtStartDate.Text = Convert.ToString(dr["START_DATE"]);
    //            else
    //                txtStartDate.Text = string.Empty;

    //            if (dr["END_DATE"] != DBNull.Value)
    //                txtEndDate.Text = Convert.ToString(dr["END_DATE"]);
    //            else
    //                txtEndDate.Text = string.Empty;

    //            txtDays.Text = (Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text)).Days.ToString();

    //            if (dr["CUST_VEND_NAME"] != DBNull.Value)
    //                txtCustVendName.Text = Convert.ToString(dr["CUST_VEND_NAME"]);
    //            else
    //                txtCustVendName.Text = string.Empty;

    //            if (dr["PLACE_OF_VISIT"] != DBNull.Value)
    //                txtPlaceOfVisit.Text = Convert.ToString(dr["PLACE_OF_VISIT"]);
    //            else
    //                txtPlaceOfVisit.Text = string.Empty;

    //            if (dr["VISIT_TYPE"] != DBNull.Value)
    //                txtPurposeOfVisit.Text = Convert.ToString(dr["VISIT_TYPE"]);
    //            else
    //                txtPurposeOfVisit.Text = string.Empty;

    //            if (dr["JOB_NO"] != DBNull.Value)
    //                txtJobInqNo.Text = Convert.ToString(dr["JOB_NO"]);
    //            else
    //                txtJobInqNo.Text = string.Empty;

    //            if (dr["BUS_SEGMENT"] != DBNull.Value)
    //                txtBusinessSegment.Text = Convert.ToString(dr["BUS_SEGMENT"]);
    //            else
    //                txtBusinessSegment.Text = string.Empty;

    //            if (dr["AIRFARE_AMT"] != DBNull.Value)
    //                txtAirfare.Text = Convert.ToString(dr["AIRFARE_AMT"]);
    //            else
    //                txtAirfare.Text = string.Empty;

    //            if (dr["TELEPHONE_MOBILE_AMT"] != DBNull.Value)
    //                txtMobile.Text = Convert.ToString(dr["TELEPHONE_MOBILE_AMT"]);
    //            else
    //                txtMobile.Text = string.Empty;

    //            if (dr["LODGING_AMT"] != DBNull.Value)
    //                txtLodging.Text = Convert.ToString(dr["LODGING_AMT"]);
    //            else
    //                txtLodging.Text = string.Empty;

    //            if (dr["TIPS_AMT"] != DBNull.Value)
    //                txtTips.Text = Convert.ToString(dr["TIPS_AMT"]);
    //            else
    //                txtTips.Text = string.Empty;

    //            if (dr["MEALS_AMT"] != DBNull.Value)
    //                txtMeals.Text = Convert.ToString(dr["MEALS_AMT"]);
    //            else
    //                txtMeals.Text = string.Empty;

    //            if (dr["VISAFEE_AMT"] != DBNull.Value)
    //                txtVisaFee.Text = Convert.ToString(dr["VISAFEE_AMT"]);
    //            else
    //                txtVisaFee.Text = string.Empty;

    //            if (dr["GROUND_TRANSPORT_AMT"] != DBNull.Value)
    //                txtGroundTransport.Text = Convert.ToString(dr["GROUND_TRANSPORT_AMT"]);
    //            else
    //                txtGroundTransport.Text = string.Empty;

    //            if (dr["DAILY_ALLOWANCE_AMT"] != DBNull.Value)
    //                txtDailyAllowance.Text = Convert.ToString(dr["DAILY_ALLOWANCE_AMT"]);
    //            else
    //                txtDailyAllowance.Text = string.Empty;

    //            if (dr["ENTERTAINMENT_AMT"] != DBNull.Value)
    //                txtEntertainment.Text = Convert.ToString(dr["ENTERTAINMENT_AMT"]);
    //            else
    //                txtEntertainment.Text = string.Empty;

    //            if (dr["OTHER_AMT"] != DBNull.Value)
    //                txtOther.Text = Convert.ToString(dr["OTHER_AMT"]);
    //            else
    //                txtOther.Text = string.Empty;

    //            if (dr["GIFTS_AMT"] != DBNull.Value)
    //                txtGifts.Text = Convert.ToString(dr["GIFTS_AMT"]);
    //            else
    //                txtGifts.Text = string.Empty;

    //            if (dr["GIFTS_AMT"] != DBNull.Value)
    //                txtGifts.Text = Convert.ToString(dr["GIFTS_AMT"]);
    //            else
    //                txtGifts.Text = string.Empty;

    //            if (dr["TOTAL_AMT"] != DBNull.Value)
    //            {
    //                hdTotal.Value = Convert.ToString(dr["TOTAL_AMT"]);
    //                txtTotal.Text = Convert.ToString(dr["TOTAL_AMT"]);
    //                txtBillPassedAmount.Text = Convert.ToString(dr["TOTAL_AMT"]);
    //            }
    //            else
    //            {
    //                hdTotal.Value = string.Empty;
    //                txtTotal.Text = string.Empty;
    //                txtBillPassedAmount.Text = "0.00";
    //            }



    //            if (dr["TOTAL_AMT_CURRENCY_ID"] != DBNull.Value)
    //                ddlTotalCurrency.SelectedValue = Convert.ToString(dr["TOTAL_AMT_CURRENCY_ID"]);

    //            if (dr["ADVANCE_AMT"] != DBNull.Value)
    //                txtAdvanceObtained.Text = Convert.ToString(dr["ADVANCE_AMT"]);
    //            else
    //                txtAdvanceObtained.Text = string.Empty;

    //            txtDisallowedAmount.Text = "0";
    //            txtReasonForDisallowing.Text = string.Empty;

    //            chkRecoverable.Checked = false;
    //            chkPayable.Checked = false;

    //            if (Convert.ToDouble(txtAdvanceObtained.Text) > Convert.ToDouble(txtBillPassedAmount.Text))
    //            {
    //                txtFinalAmount.Text = Convert.ToString(Convert.ToDouble(txtAdvanceObtained.Text) - Convert.ToDouble(txtBillPassedAmount.Text));
    //                chkRecoverable.Checked = true;
    //            }
    //            else if (Convert.ToDouble(txtAdvanceObtained.Text) < Convert.ToDouble(txtBillPassedAmount.Text))
    //            {
    //                chkPayable.Checked = true;
    //                txtFinalAmount.Text = Convert.ToString(Convert.ToDouble(txtBillPassedAmount.Text) - Convert.ToDouble(txtAdvanceObtained.Text));
    //            }
    //            else
    //            {
    //                txtFinalAmount.Text = "0.00";
    //            }

    //            //if (dr["ADJUSTED_AMT"] != DBNull.Value)
    //            //{
    //            //    hdAdjustmentAmt.Value = Convert.ToString(dr["ADJUSTED_AMT"]);
    //            //    txtAdjustmentAmt.Text = Convert.ToString(dr["ADJUSTED_AMT"]);
    //            //}
    //            //else
    //            //{
    //            //    hdAdjustmentAmt.Value = string.Empty;
    //            //    txtAdjustmentAmt.Text = string.Empty;
    //            //}

    //            //if (dr["ADJUSTED_AMT_CURRENCY_ID"] != DBNull.Value)
    //            //    ddlAdjustmentAmtCurrency.SelectedValue = Convert.ToString(dr["ADJUSTED_AMT_CURRENCY_ID"]);

    //            if (dr["IS_COST_RECOVERABLE"] != DBNull.Value)
    //            {
    //                if (Convert.ToString(dr["IS_COST_RECOVERABLE"]) == "YES")
    //                    ddlTourCostRecoverable.SelectedValue = "1";
    //                else
    //                    ddlTourCostRecoverable.SelectedValue = "0";
    //            }

    //            //if (Convert.ToDouble(hdAdjustmentAmt.Value) == 0)
    //            //{
    //            //    txtAdjustmentAmtType.Text = string.Empty;
    //            //}
    //            //else if (Convert.ToDouble(hdAdjustmentAmt.Value) < 0)
    //            //{
    //            //    txtAdjustmentAmtType.Text = "Payable";
    //            //}
    //            //else
    //            //{
    //            //    txtAdjustmentAmtType.Text = "Recoverable";
    //            //}

    //            if (dr["CREATED_REMARKS"] != DBNull.Value)
    //                txtTravellerRemarks.Text = Convert.ToString(dr["CREATED_REMARKS"]);

    //            if (dr["APPROVED_REMARKS"] != DBNull.Value)
    //                txtApprovedRemarks.Text = Convert.ToString(dr["APPROVED_REMARKS"]);

    //            if (dr["CHECKED_REMARKS"] != DBNull.Value)
    //                txtCheckedRemarksHR.Text = Convert.ToString(dr["CHECKED_REMARKS"]);

    //            if (dr["CHECKED_REMARKS"] != DBNull.Value)
    //                txtCheckedRemarksAcc.Text = Convert.ToString(dr["ACC_CHECKED_REMARKS"]);

    //            if (dr["PASSED_OR_AMD_REMARKS"] != DBNull.Value)
    //                txtAccPassAmdRemarks.Text = Convert.ToString(dr["PASSED_OR_AMD_REMARKS"]);

    //            //ADVANCE_CURRENCY

    //            ddlAdvanceObtainedCurreny.SelectedValue = Convert.ToString(dr["ADVANCE_CURRENCY"]);
    //            ddlTotalCurrency.SelectedValue = Convert.ToString(dr["ADVANCE_CURRENCY"]);
    //            //ddlAdjustmentAmtCurrency.SelectedValue = Convert.ToString(dr["ADVANCE_CURRENCY"]);
    //            ddlFinalAmountCurrency.SelectedValue = Convert.ToString(dr["ADVANCE_CURRENCY"]);


    //            //ATTACHMENTS

    //            string attachment1Txt = string.Empty;
    //            string attachment1Extn = string.Empty;

    //            if (dr["VISIT_RPT_SUMMARY_ONE_NAME"] != DBNull.Value)
    //                attachment1Txt = Convert.ToString(dr["VISIT_RPT_SUMMARY_ONE_NAME"]);

    //            if (!string.IsNullOrEmpty(attachment1Txt))
    //            {
    //                txtViewAttachment1.Text = attachment1Txt;
    //                btnViewAttachment1.Visible = true;
    //                attachment1Extn = Convert.ToString(attachment1Txt).Split('.').Last();
    //                if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
    //                {

    //                    btnViewAttachment1.ImageUrl = "~/Images/imgicon1.png";
    //                    btnViewAttachment1.ToolTip = attachment1Txt;
    //                }
    //                else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
    //                {
    //                    btnViewAttachment1.ImageUrl = "~/Images/pdficon1.png";
    //                    btnViewAttachment1.ToolTip = attachment1Txt;
    //                }
    //            }
    //            else
    //            {
    //                txtViewAttachment1.Text = string.Empty;
    //                btnViewAttachment1.Visible = false;
    //            }


    //            string attachment2Txt = string.Empty;
    //            string attachment2Extn = string.Empty;

    //            if (dr["VISIT_RPT_SUMMARY_TWO_NAME"] != DBNull.Value)
    //                attachment2Txt = Convert.ToString(dr["VISIT_RPT_SUMMARY_TWO_NAME"]);

    //            if (!string.IsNullOrEmpty(attachment2Txt))
    //            {
    //                txtViewAttachment2.Text = attachment2Txt;
    //                btnViewAttachment2.Visible = true;
    //                attachment2Extn = Convert.ToString(attachment2Txt).Split('.').Last();
    //                if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
    //                {
    //                    btnViewAttachment2.ImageUrl = "~/Images/imgicon1.png";
    //                    btnViewAttachment2.ToolTip = attachment2Txt;
    //                }
    //                else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
    //                {
    //                    btnViewAttachment2.ImageUrl = "~/Images/pdficon1.png";
    //                    btnViewAttachment2.ToolTip = attachment2Txt;
    //                }
    //            }
    //            else
    //            {
    //                txtViewAttachment2.Text = string.Empty;
    //                btnViewAttachment2.Visible = false;
    //            }


    //            string attachment3Txt = string.Empty;
    //            string attachment3Extn = string.Empty;

    //            if (dr["VISIT_RPT_SUMMARY_THREE_NAME"] != DBNull.Value)
    //                attachment3Txt = Convert.ToString(dr["VISIT_RPT_SUMMARY_THREE_NAME"]);

    //            if (!string.IsNullOrEmpty(attachment3Txt))
    //            {
    //                txtViewAttachment3.Text = attachment3Txt;
    //                btnViewAttachment3.Visible = true;
    //                attachment3Extn = Convert.ToString(attachment3Txt).Split('.').Last();
    //                if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" || attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
    //                {
    //                    btnViewAttachment3.ImageUrl = "~/Images/imgicon1.png";
    //                    btnViewAttachment3.ToolTip = attachment3Txt;
    //                }
    //                else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
    //                {
    //                    btnViewAttachment3.ImageUrl = "~/Images/pdficon1.png";
    //                    btnViewAttachment3.ToolTip = attachment3Txt;
    //                }
    //            }
    //            else
    //            {
    //                txtViewAttachment3.Text = string.Empty;
    //                btnViewAttachment3.Visible = false;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //    }
    //}

    private void ViewPassportFile(int empRecordID)
    {
        try
        {
            //byte[] bytes = null;
            DataTable dsTravelStatementInfo = (DataTable)Session["dsTravelStatementDetails"];
            string fileName = string.Empty;
            string extn = string.Empty;
            foreach (DataRow dr in dsTravelStatementInfo.Select("EMP_RECORD_ID='" + empRecordID + "'"))
            {
                fileName = Convert.ToString(dr["PASSPORT_COPY_NAME"]);
                //bytes = (byte[])dr["PASSPORT_COPY_DOC"];
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewPassportImgFileNew.ashx?emprecordid=" + empRecordID;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewPassportPDFFileNew.aspx?emprecordid=" + empRecordID);
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




    //private void UpdatedTravelStatement(int travelStatementID, int tourID)
    //{
    //    try
    //    {
    //        string sanctionNo = Convert.ToString(ViewState["SANCTION_NO"]);

    //        Byte[] visitRptSummaryOneBytes = null;
    //        if (fileUploadVisitSummary1.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadVisitSummary1.PostedFile.FileName))
    //            {
    //                visitRptSummaryOneFileName = fileUploadVisitSummary1.PostedFile.FileName;
    //                visitRptSummaryOneBytes = GetFileBytes(fileUploadVisitSummary1.PostedFile.FileName, fileUploadVisitSummary1.PostedFile.InputStream);
    //            }
    //            else
    //            {
    //                visitRptSummaryOneFileName = string.Empty;
    //                visitRptSummaryOneBytes = null;
    //            }
    //        }
    //        else
    //        {
    //            visitRptSummaryOneFileName = string.Empty;
    //            visitRptSummaryOneBytes = null;
    //        }


    //        Byte[] visitRptSummaryTwoBytes = null;
    //        if (fileUploadVisitSummary2.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadVisitSummary2.PostedFile.FileName))
    //            {
    //                visitRptSummaryTwoFileName = fileUploadVisitSummary2.PostedFile.FileName;
    //                visitRptSummaryTwoBytes = GetFileBytes(fileUploadVisitSummary2.PostedFile.FileName, fileUploadVisitSummary2.PostedFile.InputStream);
    //            }
    //            else
    //            {
    //                visitRptSummaryTwoFileName = string.Empty;
    //                visitRptSummaryTwoBytes = null;
    //            }
    //        }
    //        else
    //        {
    //            visitRptSummaryTwoFileName = string.Empty;
    //            visitRptSummaryTwoBytes = null;
    //        }

    //        Byte[] visitRptSummaryThreeBytes = null;
    //        if (fileUploadVisitSummary3.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadVisitSummary3.PostedFile.FileName))
    //            {
    //                visitRptSummaryThreeFileName = fileUploadVisitSummary3.PostedFile.FileName;
    //                visitRptSummaryThreeBytes = GetFileBytes(fileUploadVisitSummary3.PostedFile.FileName, fileUploadVisitSummary3.PostedFile.InputStream);
    //            }
    //            else
    //            {
    //                visitRptSummaryThreeFileName = string.Empty;
    //                visitRptSummaryThreeBytes = null;
    //            }
    //        }
    //        else
    //        {
    //            visitRptSummaryThreeFileName = string.Empty;
    //            visitRptSummaryThreeBytes = null;
    //        }

    //        if (!string.IsNullOrEmpty(txtAirfare.Text))
    //            airfareAmt = Convert.ToDouble(txtAirfare.Text);
    //        else
    //            airfareAmt = 0;

    //        if (!string.IsNullOrEmpty(txtMobile.Text))
    //            telephoneMobAmt = Convert.ToDouble(txtMobile.Text);
    //        else
    //            telephoneMobAmt = 0;

    //        if (!string.IsNullOrEmpty(txtLodging.Text))
    //            lodgingAmt = Convert.ToDouble(txtLodging.Text);
    //        else
    //            lodgingAmt = 0;

    //        if (!string.IsNullOrEmpty(txtTips.Text))
    //            tipsAmt = Convert.ToDouble(txtTips.Text);
    //        else
    //            tipsAmt = 0;

    //        if (!string.IsNullOrEmpty(txtMeals.Text))
    //            mealsAmt = Convert.ToDouble(txtMeals.Text);
    //        else
    //            mealsAmt = 0;

    //        if (!string.IsNullOrEmpty(txtVisaFee.Text))
    //            visaFeeAmt = Convert.ToDouble(txtVisaFee.Text);
    //        else
    //            visaFeeAmt = 0;

    //        if (!string.IsNullOrEmpty(txtGroundTransport.Text))
    //            groundTransAmt = Convert.ToDouble(txtGroundTransport.Text);
    //        else
    //            groundTransAmt = 0;

    //        if (!string.IsNullOrEmpty(txtDailyAllowance.Text))
    //            dailyAllowanceAmt = Convert.ToDouble(txtDailyAllowance.Text);
    //        else
    //            dailyAllowanceAmt = 0;

    //        if (!string.IsNullOrEmpty(txtEntertainment.Text))
    //            entertainmentAmt = Convert.ToDouble(txtEntertainment.Text);
    //        else
    //            entertainmentAmt = 0;

    //        if (!string.IsNullOrEmpty(txtOther.Text))
    //            otherAmt = Convert.ToDouble(txtOther.Text);
    //        else
    //            otherAmt = 0;

    //        if (!string.IsNullOrEmpty(txtGifts.Text))
    //            giftsAmt = Convert.ToDouble(txtGifts.Text);
    //        else
    //            giftsAmt = 0;

    //        if (!string.IsNullOrEmpty(hdTotal.Value))
    //            totalAmt = Convert.ToDouble(hdTotal.Value);
    //        else
    //            totalAmt = 0;

    //        if (ddlTotalCurrency.SelectedIndex > 0)
    //            totalAmtCurrencyID = Convert.ToInt32(ddlTotalCurrency.SelectedValue);
    //        else
    //            totalAmtCurrencyID = 0;

    //        //if (!string.IsNullOrEmpty(hdAdjustmentAmt.Value))
    //        //    adjustedAmt = Convert.ToString(hdAdjustmentAmt.Value);
    //        //else
    //        //    adjustedAmt = string.Empty;


    //        //if (ddlAdjustmentAmtCurrency.SelectedIndex > 0)
    //        //    adjustedAmtCurrencyID = Convert.ToInt32(ddlAdjustmentAmtCurrency.SelectedValue);
    //        //else
    //        //    adjustedAmtCurrencyID = 0;


    //        if (ddlTourCostRecoverable.SelectedIndex > 0)
    //            isTourCostRec = Convert.ToInt32(ddlTourCostRecoverable.SelectedValue);
    //        else
    //            isTourCostRec = 0;

    //        remarks = txtRemarks.Text;
    //        int value = objTourAndTravels.InsertUpdateTravelStatement(travelStatementID, tourID,
    //                                                        visitRptSummaryOneFileName, visitRptSummaryOneBytes,
    //                                                        visitRptSummaryTwoFileName, visitRptSummaryTwoBytes,
    //                                                        visitRptSummaryThreeFileName, visitRptSummaryThreeBytes,
    //                                                        airfareAmt, telephoneMobAmt, lodgingAmt, tipsAmt, mealsAmt,
    //                                                        visaFeeAmt, groundTransAmt, dailyAllowanceAmt, entertainmentAmt,
    //                                                        otherAmt, giftsAmt, totalAmt, totalAmtCurrencyID, adjustedAmt, adjustedAmtCurrencyID, isTourCostRec,
    //                                                        remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //        if (value > 0)
    //        {
    //            SuccessMessage("Sanction No. " + sanctionNo + " updated successfully.");


    //            int sendMailValue = SendMailNew01(travelStatementID);
    //            if (sendMailValue > 0)
    //            {
    //                UpdateTravelStatementMailStatus(travelStatementID, 1);
    //                SuccessMessage("Sanction no. " + Convert.ToString(ViewState["SANCTION_NO"]) + " submitted successfully and mail sent.");
    //            }
    //            else
    //                SuccessMessage("Sanction no. " + Convert.ToString(ViewState["SANCTION_NO"]) + " submitted successfully, please send amendment mail from Travel Statement List.");


    //        }
    //        else
    //        {
    //            ExceptionMessage("Please try again!");
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    //private void UpdateTravelStatementAmendment(int statementID, int newStatusID)
    //{
    //    try
    //    {
    //        Byte[] visitRptSummaryOneBytes = null;
    //        if (fileUploadVisitSummary1.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadVisitSummary1.PostedFile.FileName))
    //            {
    //                visitRptSummaryOneFileName = fileUploadVisitSummary1.PostedFile.FileName;
    //                visitRptSummaryOneBytes = GetFileBytes(fileUploadVisitSummary1.PostedFile.FileName, fileUploadVisitSummary1.PostedFile.InputStream);
    //            }
    //            else
    //            {
    //                visitRptSummaryOneFileName = string.Empty;
    //                visitRptSummaryOneBytes = null;
    //            }
    //        }
    //        else
    //        {
    //            visitRptSummaryOneFileName = string.Empty;
    //            visitRptSummaryOneBytes = null;
    //        }


    //        Byte[] visitRptSummaryTwoBytes = null;
    //        if (fileUploadVisitSummary2.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadVisitSummary2.PostedFile.FileName))
    //            {
    //                visitRptSummaryTwoFileName = fileUploadVisitSummary2.PostedFile.FileName;
    //                visitRptSummaryTwoBytes = GetFileBytes(fileUploadVisitSummary2.PostedFile.FileName, fileUploadVisitSummary2.PostedFile.InputStream);
    //            }
    //            else
    //            {
    //                visitRptSummaryTwoFileName = string.Empty;
    //                visitRptSummaryTwoBytes = null;
    //            }
    //        }
    //        else
    //        {
    //            visitRptSummaryTwoFileName = string.Empty;
    //            visitRptSummaryTwoBytes = null;
    //        }

    //        Byte[] visitRptSummaryThreeBytes = null;
    //        if (fileUploadVisitSummary3.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadVisitSummary3.PostedFile.FileName))
    //            {
    //                visitRptSummaryThreeFileName = fileUploadVisitSummary3.PostedFile.FileName;
    //                visitRptSummaryThreeBytes = GetFileBytes(fileUploadVisitSummary3.PostedFile.FileName, fileUploadVisitSummary3.PostedFile.InputStream);
    //            }
    //            else
    //            {
    //                visitRptSummaryThreeFileName = string.Empty;
    //                visitRptSummaryThreeBytes = null;
    //            }
    //        }
    //        else
    //        {
    //            visitRptSummaryThreeFileName = string.Empty;
    //            visitRptSummaryThreeBytes = null;
    //        }

    //        if (!string.IsNullOrEmpty(txtAirfare.Text))
    //            airfareAmt = Convert.ToDouble(txtAirfare.Text);
    //        else
    //            airfareAmt = 0;

    //        if (!string.IsNullOrEmpty(txtMobile.Text))
    //            telephoneMobAmt = Convert.ToDouble(txtMobile.Text);
    //        else
    //            telephoneMobAmt = 0;

    //        if (!string.IsNullOrEmpty(txtLodging.Text))
    //            lodgingAmt = Convert.ToDouble(txtLodging.Text);
    //        else
    //            lodgingAmt = 0;

    //        if (!string.IsNullOrEmpty(txtTips.Text))
    //            tipsAmt = Convert.ToDouble(txtTips.Text);
    //        else
    //            tipsAmt = 0;

    //        if (!string.IsNullOrEmpty(txtMeals.Text))
    //            mealsAmt = Convert.ToDouble(txtMeals.Text);
    //        else
    //            mealsAmt = 0;

    //        if (!string.IsNullOrEmpty(txtVisaFee.Text))
    //            visaFeeAmt = Convert.ToDouble(txtVisaFee.Text);
    //        else
    //            visaFeeAmt = 0;

    //        if (!string.IsNullOrEmpty(txtGroundTransport.Text))
    //            groundTransAmt = Convert.ToDouble(txtGroundTransport.Text);
    //        else
    //            groundTransAmt = 0;

    //        if (!string.IsNullOrEmpty(txtDailyAllowance.Text))
    //            dailyAllowanceAmt = Convert.ToDouble(txtDailyAllowance.Text);
    //        else
    //            dailyAllowanceAmt = 0;

    //        if (!string.IsNullOrEmpty(txtEntertainment.Text))
    //            entertainmentAmt = Convert.ToDouble(txtEntertainment.Text);
    //        else
    //            entertainmentAmt = 0;

    //        if (!string.IsNullOrEmpty(txtOther.Text))
    //            otherAmt = Convert.ToDouble(txtOther.Text);
    //        else
    //            otherAmt = 0;

    //        if (!string.IsNullOrEmpty(txtGifts.Text))
    //            giftsAmt = Convert.ToDouble(txtGifts.Text);
    //        else
    //            giftsAmt = 0;

    //        if (!string.IsNullOrEmpty(hdTotal.Value))
    //            totalAmt = Convert.ToDouble(hdTotal.Value);
    //        else
    //            totalAmt = 0;

    //        if (ddlTotalCurrency.SelectedIndex > 0)
    //            totalAmtCurrencyID = Convert.ToInt32(ddlTotalCurrency.SelectedValue);
    //        else
    //            totalAmtCurrencyID = 0;

    //        //if (!string.IsNullOrEmpty(hdAdjustmentAmt.Value))
    //        //    adjustedAmt = Convert.ToString(hdAdjustmentAmt.Value);
    //        //else
    //        //    adjustedAmt = string.Empty;


    //        //if (ddlAdjustmentAmtCurrency.SelectedIndex > 0)
    //        //    adjustedAmtCurrencyID = Convert.ToInt32(ddlAdjustmentAmtCurrency.SelectedValue);
    //        //else
    //        //    adjustedAmtCurrencyID = 0;


    //        if (ddlTourCostRecoverable.SelectedIndex > 0)
    //            isTourCostRec = Convert.ToInt32(ddlTourCostRecoverable.SelectedValue);
    //        else
    //            isTourCostRec = 0;

    //        remarks = txtRemarks.Text;

    //        int value = objTourAndTravels.UpdateTravelStatementAmendment(statementID, newStatusID,
    //                                                        visitRptSummaryOneFileName, visitRptSummaryOneBytes,
    //                                                        visitRptSummaryTwoFileName, visitRptSummaryTwoBytes,
    //                                                        visitRptSummaryThreeFileName, visitRptSummaryThreeBytes,
    //                                                        airfareAmt, telephoneMobAmt, lodgingAmt, tipsAmt, mealsAmt,
    //                                                        visaFeeAmt, groundTransAmt, dailyAllowanceAmt, entertainmentAmt,
    //                                                        otherAmt, giftsAmt, totalAmt, totalAmtCurrencyID, adjustedAmt,
    //                                                        adjustedAmtCurrencyID, isTourCostRec, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //        if (value > 0)
    //        {
    //            int sendMailValue = SendMailNew01(statementID);
    //            if (sendMailValue > 0)
    //            {
    //                UpdateTravelStatementMailStatus(statementID, newStatusID);
    //                SuccessMessage("Amendment of sanction no. " + Convert.ToString(ViewState["SANCTION_NO"]) + " submitted successfully and mail sent.");
    //            }
    //            else
    //                SuccessMessage("Amendment of sanction no. " + Convert.ToString(ViewState["SANCTION_NO"]) + " submitted successfully, please send amendment mail from Travel Statement List.");
    //        }
    //        else
    //        {
    //            ExceptionMessage("Please try again!");
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    //private void UpdateTravelStatementStatus(int amendmentCount, int statementID, int newStatusID, string sanctionNo)
    //{
    //    try
    //    {
    //        amendmentCount = amendmentCount + 1;

    //        if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 2 && Convert.ToInt32(Session["EMP_RECORD_ID"]) == 3)
    //        {
    //            if (!string.IsNullOrEmpty(txtDisallowedAmount.Text))
    //                disallowedAmount = Convert.ToDouble(txtDisallowedAmount.Text);
    //            else
    //                disallowedAmount = 0;

    //            if (ddlDisallowedAmountCurrency.SelectedIndex > 0)
    //                disallowedAmountCurrencyID = Convert.ToInt32(ddlDisallowedAmountCurrency.SelectedValue);
    //            else disallowedAmountCurrencyID = 0;

    //            if (!string.IsNullOrEmpty(txtReasonForDisallowing.Text))
    //                reasonForDisallowing = Convert.ToString(txtReasonForDisallowing.Text);
    //            else
    //                reasonForDisallowing = string.Empty;

    //            if (!string.IsNullOrEmpty(txtDNNo.Text))
    //                dnNo = Convert.ToString(txtDNNo.Text);
    //            else
    //                dnNo = string.Empty;

    //            if (hdAccDated.Value != string.Empty)
    //                amountDated = Convert.ToDateTime(hdAccDated.Value).ToString("yyyy-MM-dd");
    //            else
    //                amountDated = string.Empty;

    //            if (!string.IsNullOrEmpty(txtFinalAmount.Text))
    //                finalAmount = Convert.ToDouble(txtFinalAmount.Text);
    //            else
    //                finalAmount = 0;

    //            if (chkPayable.Checked || chkRecoverable.Checked)
    //            {
    //                if (chkPayable.Checked)
    //                    settlementType = "P";
    //                else if (chkRecoverable.Checked)
    //                    settlementType = "R";
    //            }
    //            else
    //                settlementType = string.Empty;

    //            finalAmountCurrencyID = Convert.ToInt32(ddlFinalAmountCurrency.SelectedValue);
    //        }
    //        else
    //        {
    //            dnNo = string.Empty;
    //            finalAmount = 0;
    //            amountDated = string.Empty;
    //            finalAmountCurrencyID = 0;
    //        }

    //        if (!string.IsNullOrEmpty(txtInvoiceNumber.Text))
    //            invoiceNumber = txtInvoiceNumber.Text.Trim().ToUpper();
    //        else invoiceNumber = string.Empty;

    //        if (!string.IsNullOrEmpty(hdInvoiceDate.Value))
    //            invoiceDate = Convert.ToDateTime(hdInvoiceDate.Value).ToString("yyyy-MM-dd");
    //        else invoiceDate = string.Empty;

    //        if (!string.IsNullOrEmpty(txtInvoiceAmount.Text))
    //            invoiceAmount = Convert.ToDouble(txtInvoiceAmount.Text);
    //        else invoiceAmount = 0;

    //        int value = objTourAndTravels.UpdateTravelStatementStatus(statementID, newStatusID,
    //                                                              disallowedAmount, disallowedAmountCurrencyID, reasonForDisallowing,
    //                                                              dnNo, amountDated, finalAmount, finalAmountCurrencyID, txtRemarks.Text,
    //                                                              amendmentCount, settlementType,
    //                                                              invoiceNumber, invoiceDate, invoiceAmount,
    //                                                              Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //        int sendMailValue = 0;
    //        if (value > 0)
    //        {
    //            sendMailValue = SendMailNew01(statementID);
    //            if (sendMailValue > 0)
    //            {
    //                UpdateTravelStatementMailStatus(statementID, newStatusID);
    //                //Approved
    //                if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Approved))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " approved successfully and mail sent.");

    //                //Checked
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Checked))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " checked successfully and mail sent.");

    //                //Passed
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Passed))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " passed successfully and mail sent.");

    //                //Settled
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Settled))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " settled successfully and mail sent.");

    //                //Amendment
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amendment))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " sent to amendment successfully and mail sent.");

    //                //Amended
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amended))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " amended successfully and mail sent.");

    //                //Amended-Approved
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedApproved))
    //                    SuccessMessage("Amended Tour Sanction No.: " + sanctionNo + " approved successfully and mail sent.");

    //                //Amended-Checked
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedChecked))
    //                    SuccessMessage("Amended Tour Sanction No.: " + sanctionNo + " checked successfully and mail sent.");

    //                //Amended-Passed
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedPassed))
    //                    SuccessMessage("Amended Tour Sanction No.: " + sanctionNo + " passed successfully and mail sent.");

    //                //Invoice Booked
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.InvoiceBooked))
    //                    SuccessMessage("Invoice Booked for Tour Sanction No.: " + sanctionNo + " successfully and mail sent.");
    //            }
    //            else
    //            {
    //                //Approved
    //                if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Approved))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " approved successfully, please send approved mail from Travel Statement List.");

    //                //Checked
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Checked))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " checked successfully, please send checked mail from Travel Statement List.");

    //                //Passed
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Passed))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " passed successfully, please send passed mail from Travel Statement List.");

    //                //Settled
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Settled))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " settled successfully, please send settled mail from Travel Statement List.");

    //                //Amendment
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amendment))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " sent to amendment successfully, please send amedment mail from Travel Statement List.");

    //                //Amended
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amended))
    //                    SuccessMessage("Tour Sanction No.: " + sanctionNo + " amended successfully, please send amended mail from Travel Statement List.");

    //                //Amended-Approved
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedApproved))
    //                    SuccessMessage("Amended Tour Sanction No.: " + sanctionNo + " approved successfully, please send amended mail from Travel Statement List.");

    //                //Amended-Checked
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedChecked))
    //                    SuccessMessage("Amended Tour Sanction No.: " + sanctionNo + " checked successfully, please send amended mail from Travel Statement List.");

    //                //Amended-Passed
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedPassed))
    //                    SuccessMessage("Amended Tour Sanction No.: " + sanctionNo + " passed successfully, please send amended mail from Travel Statement List.");

    //                //Invoice Booked
    //                else if (newStatusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.InvoiceBooked))
    //                    SuccessMessage("Invoice Booked for Tour Sanction No.: " + sanctionNo + " successfully, please send invoice booked mail from Travel Statement List.");


    //            }
    //        }
    //        else
    //        {
    //            ExceptionMessage("Please try again!");
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}



    //private void SaveAdditionalAttachments(int statementID, int seqNo)
    //{
    //    try
    //    {
    //        Byte[] additionalAttachmentFileBytes = null;
    //        string additionalAttachmentFileName = string.Empty;
    //        string additionalAttachmentRemarks = string.Empty;


    //        if (uploadFileAdditionalAttachment.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(uploadFileAdditionalAttachment.PostedFile.FileName))
    //            {
    //                string[] str = uploadFileAdditionalAttachment.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                additionalAttachmentFileName = str[str.Length - 1];
    //                additionalAttachmentFileName = additionalAttachmentFileName.Replace(" ", "");
    //                additionalAttachmentFileName = additionalAttachmentFileName.Replace("(", "");
    //                additionalAttachmentFileName = additionalAttachmentFileName.Replace(")", "");
    //                additionalAttachmentFileBytes = GetFileBytes(uploadFileAdditionalAttachment.PostedFile.FileName, uploadFileAdditionalAttachment.PostedFile.InputStream);
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(txtRemarksInCAD.Text)))
    //            additionalAttachmentRemarks = Convert.ToString(txtRemarksInCAD.Text);


    //        int value = objTourAndTravels.SaveAdditionalAttachments
    //                        (statementID,
    //                        additionalAttachmentFileName,
    //                        additionalAttachmentFileBytes,
    //                        seqNo,
    //                        additionalAttachmentRemarks,
    //                        Convert.ToInt32(Session["EMP_RECORD_ID"]));

    //        if (value > 0)
    //        {
    //            SuccessMessage("Additional attachment saved successfully.");
    //            GetTravelStatementList();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}


    //private void Reset()
    //{
    //    txtEmployeeName.Text = string.Empty;
    //    txtEmployeeID.Text = string.Empty;
    //    txtDesignation.Text = string.Empty;
    //    txtStartDate.Text = string.Empty;
    //    txtEndDate.Text = string.Empty;
    //    txtDays.Text = string.Empty;
    //    txtCustVendName.Text = string.Empty;
    //    txtPlaceOfVisit.Text = string.Empty;
    //    txtPurposeOfVisit.Text = string.Empty;
    //    txtJobInqNo.Text = string.Empty;
    //    txtBusinessSegment.Text = string.Empty;
    //    ddlVisitSummaryAttached.SelectedIndex = 0;
    //    txtAirfare.Text = string.Empty;
    //    txtMobile.Text = string.Empty;
    //    txtLodging.Text = string.Empty;
    //    txtTips.Text = string.Empty;
    //    txtMeals.Text = string.Empty;
    //    txtVisaFee.Text = string.Empty;
    //    txtGroundTransport.Text = string.Empty;
    //    txtDailyAllowance.Text = string.Empty;
    //    txtEntertainment.Text = string.Empty;
    //    txtOther.Text = string.Empty;
    //    txtGifts.Text = string.Empty;
    //    txtTotal.Text = string.Empty;
    //    txtAdvanceObtained.Text = string.Empty;
    //    txtDisallowedAmount.Text = string.Empty;
    //    txtBillPassedAmount.Text = string.Empty;
    //    txtReasonForDisallowing.Text = string.Empty;
    //    //txtAdjustmentAmt.Text = string.Empty;
    //    ddlTourCostRecoverable.SelectedIndex = 0;
    //    //txtAdjustmentAmtType.Text = string.Empty;
    //    txtDNNo.Text = string.Empty;
    //    txtFinalAmount.Text = "0.00";
    //    hdAccDated.Value = string.Empty;
    //    txtAccDated.Text = string.Empty;
    //    txtRemarks.Text = string.Empty;
    //}

    //private int SendMailNew01(int statementID)
    //{
    //    try
    //    {
    //        string sanctionNo = string.Empty;
    //        string statementNo = string.Empty;

    //        string startDateOfTour = string.Empty;
    //        string endDateOfTour = string.Empty;
    //        string placeOfVisit = string.Empty;
    //        string visitType = string.Empty;
    //        string jobNo = string.Empty;
    //        string businessSegment = string.Empty;
    //        string travelMode = string.Empty;
    //        int tripTypeId = 0;
    //        string tripType = string.Empty;
    //        string localTravelType = string.Empty;

    //        string expectedExpenditureAmount = string.Empty;
    //        string expectedExpenditureCurrency = string.Empty;

    //        string totalExpenditureAmount = string.Empty;
    //        string totalExpenditureCurrency = string.Empty;

    //        string advanceAmount = string.Empty;
    //        string advanceCurrency = string.Empty;

    //        string status = string.Empty;
    //        int statusID = 0;

    //        string subject = string.Empty;
    //        string from = string.Empty;
    //        string to = string.Empty;
    //        string cc = string.Empty;
    //        string bcc = string.Empty;
    //        string fileName = string.Empty;

    //        string createdBy = string.Empty;
    //        string createdByEmail = string.Empty;
    //        string createdOn = string.Empty;
    //        string createdRemark = string.Empty;

    //        string approvedBy = string.Empty;
    //        string approvedByEmail = string.Empty;
    //        string approvedOn = string.Empty;
    //        string approvedRemark = string.Empty;

    //        string checkedBy = string.Empty;
    //        string checkedByEmail = string.Empty;
    //        string checkedOn = string.Empty;
    //        string checkedRemark = string.Empty;

    //        string accCheckedBy = string.Empty;
    //        string accCheckedByEmail = string.Empty;
    //        string accCheckedOn = string.Empty;
    //        string accCheckedRemark = string.Empty;

    //        string passedBy = string.Empty;
    //        string passedByEmail = string.Empty;
    //        string passedOn = string.Empty;
    //        string passedRemark = string.Empty;

    //        string settledBy = string.Empty;
    //        string settledEmail = string.Empty;
    //        string settledOn = string.Empty;
    //        string settledRemark = string.Empty;


    //        string amendedText = string.Empty;
    //        string invoiceNumber = string.Empty;
    //        string invoiceDate = string.Empty;

    //        string invoiceBookedBy = string.Empty;
    //        string invoiceBookedEmail = string.Empty;
    //        string invoiceBookedOn = string.Empty;
    //        string invoiceBookedRemark = string.Empty;

    //        int amendmentCount = 0;
    //        string amendmentBy = string.Empty;
    //        string amendmentByEmail = string.Empty;
    //        string amendmentOn = string.Empty;
    //        string amendmentRemark = string.Empty;

    //        string amendedBy = string.Empty;
    //        string amendedByEmail = string.Empty;
    //        string amendedOn = string.Empty;
    //        string amendedRemark = string.Empty;



    //        string amendedApprovedBy = string.Empty;
    //        string amendedApprovedOn = string.Empty;
    //        string amendedApprovedByEmail = string.Empty;
    //        string amendedApprovedRemark = string.Empty;

    //        string amendedCheckedBy = string.Empty;
    //        string amendedCheckedOn = string.Empty;
    //        string amendedCheckedByEmail = string.Empty;
    //        string amendedCheckedRemark = string.Empty;

    //        string amendedAccCheckedBy = string.Empty;
    //        string amendedAccCheckedByEmail = string.Empty;
    //        string amendedAccCheckedOn = string.Empty;
    //        string amendedAccCheckedRemark = string.Empty;

    //        string amendedPassedBy = string.Empty;
    //        string amendedPassedOn = string.Empty;
    //        string amendedPassedByEmail = string.Empty;
    //        string amendedPassedRemark = string.Empty;

    //        string cancelledBy = string.Empty;
    //        string cancelledByEmail = string.Empty;
    //        string cancelledOn = string.Empty;
    //        string cancelledRemarks = string.Empty;


    //        string hrTeamEmail = string.Empty;

    //        string accTeamName = string.Empty;
    //        string accTeamEmail = string.Empty;

    //        string accTeam2Name = string.Empty;
    //        string accTeam2Email = string.Empty;


    //        string passedByTeamName = string.Empty;
    //        string passedByTeamEmail = string.Empty;

    //        string href = string.Empty;
    //        string link = string.Empty;
    //        string hrefTxt = string.Empty;

    //        string tlUserName = string.Empty;
    //        string tlPassword = string.Empty;

    //        string tlName = string.Empty;
    //        string tlEmail = string.Empty;

    //        dsUserInfo = objTourAndTravels.GetTravelRequesterInfo(statementID);

    //        if (dsUserInfo.Tables.Count > 0)
    //        {
    //            if (dsUserInfo.Tables[0].Rows.Count > 0)
    //            {
    //                if (dsUserInfo.Tables[0].Rows[0]["STATEMENT_ID"] != DBNull.Value)
    //                    statementID = Convert.ToInt32(dsUserInfo.Tables[0].Rows[0]["STATEMENT_ID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"] != DBNull.Value)
    //                    sanctionNo = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["STATEMENT_NO"] != DBNull.Value)
    //                    statementNo = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["STATEMENT_NO"]);





    //                if (dsUserInfo.Tables[0].Rows[0]["START_DATE_OF_TRAVEL"] != DBNull.Value)
    //                    startDateOfTour = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["START_DATE_OF_TRAVEL"]);
    //                else startDateOfTour = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["END_DATE_OF_TRAVEL"] != DBNull.Value)
    //                    endDateOfTour = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["END_DATE_OF_TRAVEL"]);
    //                else endDateOfTour = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["PLACE_OF_VISIT"] != DBNull.Value)
    //                    placeOfVisit = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["PLACE_OF_VISIT"]);
    //                else placeOfVisit = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["VISIT_TYPE"] != DBNull.Value)
    //                    visitType = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["VISIT_TYPE"]);
    //                else visitType = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value)
    //                    jobNo = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["JOB_NO"]);
    //                else jobNo = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["BUS_SEGMENT"] != DBNull.Value)
    //                    businessSegment = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["BUS_SEGMENT"]);
    //                else businessSegment = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["TRAVEL_MODE"] != DBNull.Value)
    //                    travelMode = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["TRAVEL_MODE"]);
    //                else travelMode = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["TRIP_TYPE_ID"] != DBNull.Value)
    //                    tripTypeId = Convert.ToInt32(dsUserInfo.Tables[0].Rows[0]["TRIP_TYPE_ID"]);
    //                else tripTypeId = 0;

    //                if (dsUserInfo.Tables[0].Rows[0]["TRIP_TYPE"] != DBNull.Value)
    //                    tripType = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["TRIP_TYPE"]);
    //                else tripType = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["LOCAL_TRAVEL_TYPE"] != DBNull.Value)
    //                    localTravelType = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["LOCAL_TRAVEL_TYPE"]);
    //                else localTravelType = string.Empty;

    //                //if (dsUserInfo.Tables[0].Rows[0]["EXPENDITURE_AMT"] != DBNull.Value)
    //                //    expenditureAmount = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EXPENDITURE_AMT"]);
    //                //else expenditureAmount = string.Empty;

    //                //if (dsUserInfo.Tables[0].Rows[0]["EXPENDITURE_CURRENCY"] != DBNull.Value)
    //                //    expenditureCurrency = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EXPENDITURE_CURRENCY"]);
    //                //else expenditureCurrency = string.Empty;


    //                if (dsUserInfo.Tables[0].Rows[0]["EXPECTED_EXPENDITURE_AMT"] != DBNull.Value)
    //                    expectedExpenditureAmount = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EXPECTED_EXPENDITURE_AMT"]);
    //                else expectedExpenditureAmount = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["EXPECTED_EXPENDITURE_CURRENCY"] != DBNull.Value)
    //                    expectedExpenditureCurrency = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EXPECTED_EXPENDITURE_CURRENCY"]);
    //                else expectedExpenditureCurrency = string.Empty;


    //                if (dsUserInfo.Tables[0].Rows[0]["EXPENDITURE_AMT"] != DBNull.Value)
    //                    totalExpenditureAmount = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EXPENDITURE_AMT"]);
    //                else totalExpenditureAmount = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["EXPENDITURE_CURRENCY"] != DBNull.Value)
    //                    totalExpenditureCurrency = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EXPENDITURE_CURRENCY"]);
    //                else totalExpenditureCurrency = string.Empty;


    //                if (dsUserInfo.Tables[0].Rows[0]["ADVANCE_AMT"] != DBNull.Value)
    //                    advanceAmount = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["ADVANCE_AMT"]);
    //                else advanceAmount = string.Empty;

    //                if (dsUserInfo.Tables[0].Rows[0]["ADVANCE_CURRENCY"] != DBNull.Value)
    //                    advanceCurrency = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["ADVANCE_CURRENCY"]);
    //                else advanceCurrency = string.Empty;





    //                if (dsUserInfo.Tables[0].Rows[0]["STATUS_NAME"] != DBNull.Value)
    //                    status = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["STATUS_NAME"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["STATUS_ID"] != DBNull.Value)
    //                    statusID = Convert.ToInt32(dsUserInfo.Tables[0].Rows[0]["STATUS_ID"]);

    //                ViewState["NEW_STATUS_ID"] = statusID;

    //                if (dsUserInfo.Tables[0].Rows[0]["TL_USER_NAME"] != DBNull.Value)
    //                    tlUserName = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["TL_USER_NAME"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["TL_PASSWORD"] != DBNull.Value)
    //                    tlPassword = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["TL_PASSWORD"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["TL_NAME"] != DBNull.Value)
    //                    tlName = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["TL_NAME"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["TL_EMAIL"] != DBNull.Value)
    //                    tlEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["TL_EMAIL"]);



    //                if (dsUserInfo.Tables[0].Rows[0]["EMPLOYEE_NAME"] != DBNull.Value)
    //                    createdBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EMPLOYEE_NAME"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["EMP_EMAIL"] != DBNull.Value)
    //                    createdByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["EMP_EMAIL"]);


    //                if (dsUserInfo.Tables[0].Rows[0]["CREATED_ON"] != DBNull.Value)
    //                    createdOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["CREATED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["CREATED_REMARKS"] != DBNull.Value)
    //                    createdRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["CREATED_REMARKS"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["APPROVED_BY"] != DBNull.Value)
    //                    approvedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["APPROVED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["APPROVED_BY_EMAIL"] != DBNull.Value)
    //                    approvedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["APPROVED_BY_EMAIL"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["APPROVED_ON"] != DBNull.Value)
    //                    approvedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["APPROVED_REMARKS"] != DBNull.Value)
    //                    approvedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["APPROVED_REMARKS"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["CHECKED_BY"] != DBNull.Value)
    //                    checkedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["CHECKED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["CHECKED_ON"] != DBNull.Value)
    //                    checkedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["CHECKED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["CHECKED_BY_EMAILID"] != DBNull.Value)
    //                    checkedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["CHECKED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["CHECKED_REMARKS"] != DBNull.Value)
    //                    checkedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["CHECKED_REMARKS"]);




    //                if (dsUserInfo.Tables[0].Rows[0]["ACC_CHECKED_BY"] != DBNull.Value)
    //                    accCheckedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["ACC_CHECKED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["ACC_CHECKED_ON"] != DBNull.Value)
    //                    accCheckedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["ACC_CHECKED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["ACC_CHECKED_BY_EMAILID"] != DBNull.Value)
    //                    accCheckedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["ACC_CHECKED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["ACC_CHECKED_REMARKS"] != DBNull.Value)
    //                    accCheckedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["ACC_CHECKED_REMARKS"]);






    //                if (dsUserInfo.Tables[0].Rows[0]["PASSED_BY"] != DBNull.Value)
    //                    passedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["PASSED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["PASSED_ON"] != DBNull.Value)
    //                    passedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["PASSED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["PASSED_BY_EMAILID"] != DBNull.Value)
    //                    passedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["PASSED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["PASSED_REMARKS"] != DBNull.Value)
    //                    passedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["PASSED_REMARKS"]);


    //                if (dsUserInfo.Tables[0].Rows[0]["SETTLED_BY"] != DBNull.Value)
    //                    settledBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["SETTLED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["SETTLED_ON"] != DBNull.Value)
    //                    settledOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["SETTLED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["SETTLED_BY_EMAILID"] != DBNull.Value)
    //                    settledEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["SETTLED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["SETTLED_REMARKS"] != DBNull.Value)
    //                    settledRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["SETTLED_REMARKS"]);



    //                if (dsUserInfo.Tables[0].Rows[0]["INVOICE_NUMBER"] != DBNull.Value)
    //                    invoiceNumber = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["INVOICE_NUMBER"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["INVOICE_DATE"] != DBNull.Value)
    //                    invoiceDate = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["INVOICE_DATE"]);




    //                if (dsUserInfo.Tables[0].Rows[0]["INVOICE_BOOKED_BY"] != DBNull.Value)
    //                    invoiceBookedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["INVOICE_BOOKED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["INVOICE_BOOKED_ON"] != DBNull.Value)
    //                    invoiceBookedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["INVOICE_BOOKED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["INVOICE_BOOKED_BY_EMAILID"] != DBNull.Value)
    //                    invoiceBookedEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["INVOICE_BOOKED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["INVOICE_BOOKED_REMARKS"] != DBNull.Value)
    //                    invoiceBookedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["INVOICE_BOOKED_REMARKS"]);



    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDMENT_COUNT"] != DBNull.Value)
    //                    amendmentCount = Convert.ToInt32(dsUserInfo.Tables[0].Rows[0]["AMENDMENT_COUNT"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDMENT_BY"] != DBNull.Value)
    //                    amendmentBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDMENT_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDMENT_ON"] != DBNull.Value)
    //                    amendmentOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["AMENDMENT_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDMENT_BY_EMAILID"] != DBNull.Value)
    //                    amendmentByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDMENT_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDMENT_REMARKS"] != DBNull.Value)
    //                    amendmentRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDMENT_REMARKS"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_BY"] != DBNull.Value)
    //                    amendedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_ON"] != DBNull.Value)
    //                    amendedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["AMENDED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_BY_EMAILID"] != DBNull.Value)
    //                    amendedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_REMARKS"] != DBNull.Value)
    //                    amendedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_REMARKS"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_BY"] != DBNull.Value)
    //                    amendedApprovedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_ON"] != DBNull.Value)
    //                    amendedApprovedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_BY_EMAILID"] != DBNull.Value)
    //                    amendedApprovedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_REMARKS"] != DBNull.Value)
    //                    amendedApprovedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_APPROVED_REMARKS"]);



    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_CHECKED_BY"] != DBNull.Value)
    //                    amendedCheckedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_CHECKED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_CHECKED_ON"] != DBNull.Value)
    //                    amendedCheckedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["AMENDED_CHECKED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_CHECKED_BY_EMAILID"] != DBNull.Value)
    //                    amendedCheckedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_CHECKED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_CHECKED_REMARKS"] != DBNull.Value)
    //                    amendedCheckedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_CHECKED_REMARKS"]);



    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_ACC_CHECKED_BY"] != DBNull.Value)
    //                    amendedAccCheckedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_ACC_CHECKED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_ACC_CHECKED_ON"] != DBNull.Value)
    //                    amendedAccCheckedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["AMENDED_ACC_CHECKED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_ACC_BY_EMAILID"] != DBNull.Value)
    //                    amendedAccCheckedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_ACC_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_ACC_CHECKED_REMARKS"] != DBNull.Value)
    //                    amendedAccCheckedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_ACC_CHECKED_REMARKS"]);






    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_PASSED_BY"] != DBNull.Value)
    //                    amendedPassedBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_PASSED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_PASSED_ON"] != DBNull.Value)
    //                    amendedPassedOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["AMENDED_PASSED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_PASSED_BY_EMAILID"] != DBNull.Value)
    //                    amendedPassedByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_PASSED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["AMENDED_PASSED_REMARKS"] != DBNull.Value)
    //                    amendedPassedRemark = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["AMENDED_PASSED_REMARKS"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["CANCELLED_BY"] != DBNull.Value)
    //                    cancelledBy = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["CANCELLED_BY"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["CANCELLED_BY_EMAILID"] != DBNull.Value)
    //                    cancelledByEmail = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["CANCELLED_BY_EMAILID"]);

    //                if (dsUserInfo.Tables[0].Rows[0]["CANCELLED_ON"] != DBNull.Value)
    //                    cancelledOn = Convert.ToDateTime(dsUserInfo.Tables[0].Rows[0]["CANCELLED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

    //                if (dsUserInfo.Tables[0].Rows[0]["CANCELLED_REMARKS"] != DBNull.Value)
    //                    cancelledRemarks = Convert.ToString(dsUserInfo.Tables[0].Rows[0]["CANCELLED_REMARKS"]);

    //            }
    //            if (dsUserInfo.Tables[1].Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in dsUserInfo.Tables[1].Rows)
    //                {
    //                    hrTeamEmail += "," + Convert.ToString(dr["HR_EMAIL"]);
    //                }
    //                hrTeamEmail = hrTeamEmail.TrimStart(',');
    //            }

    //            if (dsUserInfo.Tables[2].Rows.Count > 0)
    //            {
    //                passedByTeamName = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["PASSED_BY_NAME"]);
    //                passedByTeamEmail = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["PASSED_BY_EMAIL"]);
    //            }

    //            if (dsUserInfo.Tables[3].Rows.Count > 0)
    //            {
    //                //accTeamName = Convert.ToString(dsUserInfo.Tables[3].Rows[0]["ACC_NAME"]);
    //                //accTeamEmail = Convert.ToString(dsUserInfo.Tables[3].Rows[0]["ACC_EMAIL"]);

    //                //foreach (DataRow dr3 in dsUserInfo.Tables[3].Select("TS_APPROVAL_BY=" + (int)TandTAllStatus.EnumTravelApprovalTypes.Settled))
    //                foreach (DataRow dr3 in dsUserInfo.Tables[3].Select("TS_APPROVAL_BY=" + (int)TandTAllStatus.EnumTravelApprovalTypes.Settled))
    //                {
    //                    accTeamName = Convert.ToString(dr3["ACC_NAME"]);
    //                    accTeamEmail += Convert.ToString(dr3["ACC_EMAIL"]) + ",";
    //                }

    //                foreach (DataRow dr3 in dsUserInfo.Tables[3].Select("TS_APPROVAL_BY=" + (int)TandTAllStatus.EnumTravelApprovalTypes.InvoiceBooked))
    //                {
    //                    accTeam2Name = Convert.ToString(dr3["ACC_NAME"]);
    //                    accTeam2Email += Convert.ToString(dr3["ACC_EMAIL"]) + ",";


    //                    accTeamName = Convert.ToString(dr3["ACC_NAME"]);
    //                    accTeamEmail += Convert.ToString(dr3["ACC_EMAIL"]) + ","; //added for multiple approvals
    //                }

    //                if (!string.IsNullOrEmpty(accTeamEmail))
    //                    accTeamEmail = accTeamEmail.TrimEnd(',');

    //                if (!string.IsNullOrEmpty(accTeam2Email))
    //                    accTeam2Email = accTeam2Email.TrimEnd(',');

    //            }
    //        }

    //        string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);

    //        link = "'" + urlTxt + "/Login.aspx?sanctionno=" + sanctionNo + "'";

    //        //New
    //        //if (statusID == 1)
    //        if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.New))
    //        {
    //            from = createdByEmail;
    //            to = tlEmail;// + "," + createdByEmail;
    //            bcc = createdByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/01ApprovalTravelStatementMail.htm";
    //            subject = "Approval of Travel Statement, Sanction No.: " + sanctionNo;
    //            link = "'" + urlTxt + "/TOUR_AND_TRAVELS/TRAVEL/UpdateTravelStatementStatusThree.aspx?statementid=" + statementID + "&actid=2&ud=" + tlUserName + "&pd=" + tlPassword + "'";
    //            href = "<a href=" + link + ">Approve Travel Statement</a>";
    //        }

    //        //Approved
    //        //else if (statusID == 2)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Approved))
    //        {
    //            from = approvedByEmail;
    //            to = hrTeamEmail;
    //            cc = createdByEmail;
    //            bcc = approvedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/02CheckTravelStatementMail.htm";
    //            subject = "Approval for Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Check Travel Statement</a>";
    //        }


    //        //Checked
    //        //else if (statusID == 3)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Checked))
    //        {
    //            from = checkedByEmail;
    //            to = accTeamEmail;
    //            cc = createdByEmail;
    //            bcc = checkedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/13AccCheckTravelStatementMail.htm";
    //            subject = "Approval for Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Check/Amend Travel Statement</a>";
    //        }

    //        //Acc-Checked
    //        //else if (statusID == 13)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AccChecked))
    //        {
    //            from = accCheckedByEmail;
    //            to = passedByTeamEmail;
    //            cc = createdByEmail + "," + checkedByEmail;
    //            bcc = accCheckedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/03PassTravelStatementMail.htm";
    //            subject = "Approval for Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Pass/Amend Travel Statement</a>";
    //        }

    //        //Passed
    //        //else if (statusID == 4)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Passed))
    //        {
    //            from = passedByEmail;
    //            to = accTeamEmail;
    //            cc = hrTeamEmail + "," + approvedByEmail + "," + createdByEmail;
    //            bcc = passedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/04SettleTravelStatementMail.htm";
    //            subject = "Settlement for Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Settle Travel Statement</a>";
    //        }

    //        //Amendment
    //        //else if (statusID == 7)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amendment))
    //        {
    //            from = amendmentByEmail;
    //            to = createdByEmail;
    //            cc = approvedByEmail;//+ "," + amendmentByEmail;
    //            bcc = amendmentByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/06AmendmentTravelStatementMail.htm";
    //            subject = "Amendment for Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Amend Travel Statement</a>";
    //        }

    //        //Ammended: and sent to hod to approve
    //        //else if (statusID == 8)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Amended))
    //        {
    //            from = amendedByEmail;
    //            to = tlEmail;// + "," + amendedByEmail;
    //            bcc = amendedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/07AmendedTravelStatementMail.htm";
    //            subject = "Approval for amended Travel Statement, Sanction No.: " + sanctionNo;

    //            link = "'" + urlTxt + "/TOUR_AND_TRAVELS/TRAVEL/UpdateTravelStatementStatusThree.aspx?statementid=" + statementID + "&actid=9&ud=" + tlUserName + "&pd=" + tlPassword + "'";
    //            href = "<a href=" + link + ">Approve Amended Travel Statement</a>";
    //        }

    //        //Amended-Approved
    //        //else if (statusID == 9)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedApproved))
    //        {
    //            from = amendedApprovedByEmail;
    //            to = hrTeamEmail;
    //            cc = amendedByEmail;// passedByTeamEmail;
    //            bcc = amendedApprovedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/02CheckAmendedTravelStatementMail.htm";
    //            subject = "Approval for amended Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Check Amended Travel Statement</a>";
    //        }

    //        //Amended-Checked
    //        //else if (statusID == 10)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedChecked))
    //        {
    //            from = amendedCheckedByEmail;
    //            to = accTeamEmail;
    //            cc = amendedByEmail + "," + amendedApprovedByEmail;
    //            bcc = amendedCheckedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/14AccCheckAmendedTravelStatementMail.htm";
    //            subject = "Approval for amended Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Check Amended Travel Statement</a>";
    //        }


    //        //Amended-Acc-Checked
    //        //else if (statusID == 13)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedAccChecked))
    //        {
    //            from = amendedAccCheckedByEmail;
    //            to = passedByTeamEmail;
    //            cc = amendedByEmail + "," + amendedCheckedByEmail;
    //            bcc = amendedAccCheckedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/03PassAmendedTravelStatementMail.htm";
    //            subject = "Approval for amended Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Pass/Amend Travel Statement</a>";
    //        }



    //        //Amended-Passed
    //        //else if (statusID == 11)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.AmendedPassed))
    //        {
    //            from = amendedPassedByEmail;
    //            to = accTeamEmail;
    //            cc = hrTeamEmail + "," + amendmentByEmail + "," + amendedByEmail;// + "," + amendedPassedByEmail;
    //            bcc = amendedPassedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/04SettleAmendedTravelStatementMail.htm";
    //            subject = "Settlement for amended Travel Statement, Sanction No.: " + sanctionNo;
    //            href = "<a href=" + link + ">Settle Amended Travel Statement</a>";
    //        }

    //        //Settled
    //        //else if (statusID == 5)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.Settled))
    //        {
    //            from = settledEmail;
    //            to = createdByEmail;

    //            cc = approvedByEmail;// + "," + settledEmail;

    //            if (tripTypeId == (int)TandTAllStatus.EnumTripType.Reimbursable)
    //                cc = cc + "," + accTeam2Email;

    //            bcc = settledEmail;

    //            if (amendmentCount > 0)
    //            {
    //                fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/08SettledAmendedTravelStatementEmail.htm";
    //                subject = "Settled Amended Sanction No.: " + sanctionNo;
    //            }
    //            else
    //            {
    //                fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/08SettledTravelStatementEmail.htm";
    //                subject = "Settled Sanction No.: " + sanctionNo;
    //            }
    //        }

    //        //Settled
    //        //else if (statusID == 12)
    //        else if (statusID == Convert.ToInt32(TandTAllStatus.EnumTravelStatus.InvoiceBooked))
    //        {
    //            from = invoiceBookedEmail;
    //            to = createdByEmail;
    //            cc = approvedByEmail + "," + accTeam2Email + "," + settledEmail + "," + settledEmail;
    //            bcc = invoiceBookedEmail;

    //            if (amendmentCount > 0)
    //                amendedText = "amended";


    //            fileName = "~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/09InvoiceBookedEmail.htm";
    //            subject = "Invoice Booked for Sanction No.: " + sanctionNo;
    //        }


    //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //        SmtpClient SmtpServer = new SmtpClient();
    //        mail.From = new MailAddress(from);


    //        if (!string.IsNullOrEmpty(to))
    //        {
    //            string[] strTo = to.Split(',');
    //            foreach (string item in strTo)
    //            {
    //                mail.To.Add(item);
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(cc))
    //        {
    //            string[] strCC = cc.Split(',');
    //            foreach (string item in strCC)
    //            {
    //                mail.CC.Add(item);
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(bcc))
    //        {
    //            string[] strBCC = bcc.Split(',');
    //            foreach (string item in strBCC)
    //            {
    //                mail.Bcc.Add(item);
    //            }
    //        }

    //        mail.Subject = subject;
    //        mail.IsBodyHtml = true;

    //        string body = string.Empty;
    //        using (StreamReader reader = new StreamReader(Server.MapPath(fileName)))
    //        {
    //            body = reader.ReadToEnd();
    //        }

    //        body = body.Replace("{#sanctionno#}", sanctionNo);

    //        body = body.Replace("{#startdateoftour#}", startDateOfTour);
    //        body = body.Replace("{#enddateoftour#}", endDateOfTour);
    //        body = body.Replace("{#placeofvisit#}", placeOfVisit);
    //        body = body.Replace("{#visittype#}", visitType);
    //        body = body.Replace("{#jobno#}", jobNo);
    //        body = body.Replace("{#businesssegment#}", businessSegment);
    //        body = body.Replace("{#travelmode#}", travelMode);
    //        body = body.Replace("{#triptype#}", tripType);
    //        body = body.Replace("{#localtraveltype#}", localTravelType);

    //        //body = body.Replace("{#expenditureamount#}", expenditureAmount);
    //        //body = body.Replace("{#expenditurecurrency#}", expenditureCurrency);

    //        //body = body.Replace("{#expenditureamount#}", expenditureAmount);
    //        //body = body.Replace("{#expenditurecurrency#}", expenditureCurrency);

    //        body = body.Replace("{#expectedexpenditureamount#}", expectedExpenditureAmount);
    //        body = body.Replace("{#expectedexpenditurecurrency#}", expectedExpenditureCurrency);

    //        body = body.Replace("{#totalexpenditureamount#}", totalExpenditureAmount);
    //        body = body.Replace("{#totalexpenditurecurrency#}", totalExpenditureCurrency);


    //        body = body.Replace("{#advanceamount#}", advanceAmount);
    //        body = body.Replace("{#advancecurrency#}", advanceCurrency);


    //        body = body.Replace("{#createdby#}", createdBy);
    //        body = body.Replace("{#createdon#}", createdOn);
    //        body = body.Replace("{#createdremark#}", createdRemark);

    //        body = body.Replace("{#approvedby#}", approvedBy);
    //        body = body.Replace("{#approvedon#}", approvedOn);
    //        body = body.Replace("{#approvedremark#}", approvedRemark);


    //        body = body.Replace("{#checkedby#}", checkedBy);
    //        body = body.Replace("{#checkedon#}", checkedOn);
    //        body = body.Replace("{#checkedremark#}", checkedRemark);

    //        body = body.Replace("{#acccheckedby#}", accCheckedBy);
    //        body = body.Replace("{#acccheckedon#}", accCheckedOn);
    //        body = body.Replace("{#acccheckedremark#}", accCheckedRemark);

    //        body = body.Replace("{#passedby#}", passedBy);
    //        body = body.Replace("{#passedon#}", passedOn);
    //        body = body.Replace("{#passedbyteamname#}", passedByTeamName);
    //        body = body.Replace("{#passedremark#}", passedRemark);

    //        body = body.Replace("{#amendmentby#}", amendmentBy);
    //        body = body.Replace("{#amendmenton#}", amendmentOn);
    //        body = body.Replace("{#amendmentremark#}", amendmentRemark);

    //        body = body.Replace("{#amendedby#}", amendedBy);
    //        body = body.Replace("{#amendedon#}", amendedOn);
    //        body = body.Replace("{#amendedremark#}", amendedRemark);

    //        body = body.Replace("{#amendedapprovedby#}", amendedApprovedBy);
    //        body = body.Replace("{#amendedapprovedon#}", amendedApprovedOn);
    //        body = body.Replace("{#amendedapprovedremark#}", amendedApprovedRemark);

    //        body = body.Replace("{#amendedcheckedby#}", amendedCheckedBy);
    //        body = body.Replace("{#amendedcheckedon#}", amendedCheckedOn);
    //        body = body.Replace("{#amendedcheckedremark#}", amendedCheckedRemark);

    //        body = body.Replace("{#amendedacccheckedby#}", amendedAccCheckedBy);
    //        body = body.Replace("{#amendedacccheckedon#}", amendedAccCheckedOn);
    //        body = body.Replace("{#amendedacccheckedremark#}", amendedAccCheckedRemark);

    //        body = body.Replace("{#amendedpassedby#}", amendedPassedBy);
    //        body = body.Replace("{#amendedpassedon#}", amendedPassedOn);
    //        body = body.Replace("{#amendedpassedremark#}", amendedPassedRemark);

    //        body = body.Replace("{#accteamname#}", accTeamName);
    //        body = body.Replace("{#accteam2name#}", accTeam2Name);

    //        if (tripTypeId == (int)TandTAllStatus.EnumTripType.Reimbursable)
    //            body = body.Replace("{#invoiceBookingBodyText#}", "@" + accTeam2Name + ": Please book customer invoice in FACT and update the same in TMS");
    //        else
    //            body = body.Replace("{#invoiceBookingBodyText#}", "");


    //        body = body.Replace("{#settledby#}", settledBy);
    //        body = body.Replace("{#settledon#}", settledOn);
    //        body = body.Replace("{#settledremark#}", settledRemark);


    //        body = body.Replace("{#isamendedtext#}", invoiceNumber);
    //        body = body.Replace("{#invoiceNumber#}", invoiceNumber);
    //        body = body.Replace("{#invoiceDate#}", invoiceDate);
    //        body = body.Replace("{#invoiceBookedby#}", invoiceBookedBy);
    //        body = body.Replace("{#invoiceBookedon#}", invoiceBookedOn);
    //        body = body.Replace("{#invoiceBookedremark#}", invoiceBookedRemark);




    //        body = body.Replace("{#link#}", href);
    //        mail.Body = body;

    //        SmtpServer.Host = "eusmtp.hi.corp";
    //        SmtpServer.Port = 25;
    //        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

    //        try
    //        {
    //            SmtpServer.Send(mail);
    //            return 1;
    //        }
    //        catch (Exception ex)
    //        {
    //            string exMsg = ex.ToString();
    //            if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
    //                return 1;

    //            else
    //                return 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return 0;
    //    }
    //}

    //private void UpdateTravelStatementMailStatus(int statementID, int newStatusID)
    //{
    //    //int mailActID = 0;
    //    ////APPROVAL
    //    //if (newStatusID == 1)
    //    //    mailActID = 1;

    //    ////APPROVED
    //    //else if (newStatusID == 2)
    //    //    mailActID = 2;

    //    ////CHECKED
    //    //else if (newStatusID == 3)
    //    //    mailActID = 3;

    //    ////PASSED
    //    //else if (newStatusID == 4)
    //    //    mailActID = 4;

    //    ////SETTLED
    //    //else if (newStatusID == 5)
    //    //    mailActID = 5;

    //    ////AMENDMENT
    //    //else if (newStatusID == 7)
    //    //    mailActID = 7;

    //    ////AMENDED-APPROVAL
    //    //else if (newStatusID == 8)
    //    //    mailActID = 8;

    //    ////AMENDED-APPROVED
    //    //else if (newStatusID == 9)
    //    //    mailActID = 9;

    //    ////AMENDED-CHECKED
    //    //else if (newStatusID == 10)
    //    //    mailActID = 10;

    //    ////AMENDED-PASSED
    //    //else if (newStatusID == 11)
    //    //    mailActID = 11;

    //    int val = objTourAndTravels.UpdateTravelStatementMailStatus(statementID, newStatusID);
    //}

    //private void SuccessMessage(string message)
    //{
    //    pnlMsg.Visible = true;
    //    lblMsg.Text = message;
    //    lblMsg.ForeColor = System.Drawing.Color.Green;
    //}

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    //private void ExceptionMessageAddAttachments(string message)
    //{
    //    pnlAdditonalAttachmentDetail.Visible = true;
    //    lblAdditonalAttachmentDetail.Text = message;
    //    lblAdditonalAttachmentDetail.ForeColor = System.Drawing.Color.Red;
    //}

    #endregion

}