using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using System.Xml.Linq;
using System.Threading;

public partial class TOUR_AND_TRAVELS_TOUR_TourInformationListNew : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsTourStatus = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsTourList = new DataSet();
    DataTable dtTeamMembers = new DataTable();
    //string startDateSearch = string.Empty;
    //string endDateSearch = string.Empty;
    string tourNoSearch = string.Empty;
    string sanctionNoSearch = string.Empty;
    int tourStatusIDSearch = 0;
    int empRecordID = 0;
    int teamMemberID = 0;
    string teamMembers = string.Empty;
    int departmentID = 0;
    double advanceAmount = 0;
    int advanceAmtcurrencyID = 0;
    string advanceAmtcurrencyCode = string.Empty;




    DataSet dsLocalTravelType = new DataSet();
    DataSet dsTripType = new DataSet();
    DataSet dsTravelMode = new DataSet();
    DataSet dsSegmentType = new DataSet();
    DataSet dsVisitType = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsCountry = new DataSet();
    DataSet dsTourBasedOn = new DataSet();
    DataSet dsTourInitiative = new DataSet();


    //int actID = 0;
    //int tourID = 0;
    //int empRecordID = 0;
    //int tourStatusID = 0;
    //string tourStatus = string.Empty;

    string startDate = string.Empty;
    string endDate = string.Empty;
    string custVendName = string.Empty;
    string placeOfVisit = string.Empty;
    int countryOfVisit = 0;
    /// <summary>
    /// /
    /// </summary>
    int tourBasedOnID = 0;
    int tourInitiativeID = 0;
    /// <summary>
    /// 
    /// </summary>
    int purposeOfVisitID = 0;
    string jobNo = string.Empty;
    int busSegmentID = 0;
    string busSegmentOther = string.Empty;
    int modeOfTavelID = 0;
    string modeOfTavelOther = string.Empty;
    int tripTypeID = 0;
    int localTravellingID = 0;
    string localTravellingOther = string.Empty;
    double expectedExpenditure = 0;
    int expectedExpenditureCurrencyID = 0;
    double advanceRequired = 0;
    int advanceRequiredCurrencyID = 0;
    string remarks = string.Empty;

    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;

    string subject = string.Empty;
    string fileName = string.Empty;
    bool isMailSend = false;
    string tourNo = string.Empty;
    string sanctionNo = string.Empty;
    string tourStatus = string.Empty;
    int tourStatusID = 0;
    int travelModeID = 0;

    string createdBy = string.Empty;
    string createdByEmail = string.Empty;
    string createdOn = string.Empty;
    string createdRemarks = string.Empty;


    string hodApprovedBy = string.Empty;
    string hodApprovedByEmail = string.Empty;
    string hodApprovedOn = string.Empty;
    string hodApprovedRemarks = string.Empty;

    string deletedBy = string.Empty;
    string deletedByEmail = string.Empty;
    string deletedOn = string.Empty;
    string deletedRemarks = string.Empty;

    string cancelledBy = string.Empty;
    string cancelledByEmail = string.Empty;
    string cancelledOn = string.Empty;
    string cancelledRemarks = string.Empty;

    string finalApprovedBy = string.Empty;
    string finalApprovedByEmail = string.Empty;
    string finalApprovedOn = string.Empty;
    string finalApprovedRemarks = string.Empty;

    int finalApprovarEmpRecordID = 0;
    string finalApprovarUN = string.Empty;
    string finalApprovarPWD = string.Empty;
    string finalApprovarEmail = string.Empty;
    string finalApprovarName = string.Empty;

    string hrTeamEmail = string.Empty;

    string accTeamEmail = string.Empty;
    string accTeamName = string.Empty;

    double advanceAmt = 0;
    string advanceAmtCurrency = string.Empty;

    string accAcvnaceFCEmail = string.Empty;

    DataSet dsUserInfo = new DataSet();

    string fileUploadPassportCopyFileName = string.Empty;
    Byte[] fileUploadPassportCopyBytes = null;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdConfirmValue.Value = "0";


                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                //Session["CHK_ACC"] = null;
                Session["TOUR_LIST"] = null;
                BindTourStatus();
                BindEmployee();

                BindVisitType();
                BindSegmentType();
                BindTravelMode();
                BindTripType();
                BindLocalTravelType();
                BindCurrency();
                BindCountry();
                BindTourBasedOn();
                BindTourInitiative();

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tourno"])))
                    txtTourNo.Text = Convert.ToString(Request.QueryString["tourno"]);
                else
                    txtTourNo.Text = string.Empty;

                GetTourList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetTourList();
    }

    protected void gvTourList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                int tourID = 0;
                if (Convert.ToString(e.CommandArgument) == "STATUS" ||
                    Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "VIEWPASSPORT" ||
                    Convert.ToString(e.CommandArgument) == "SEND_MAIL" ||
                    Convert.ToString(e.CommandArgument) == "ADVICE" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "CANCEL" ||
                         Convert.ToString(e.CommandArgument) == "APPROVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblTourID = gvTourList.Rows[rowindex].FindControl("lblTourID") as Label;
                Label lblTourNO = gvTourList.Rows[rowindex].FindControl("lblTourNO") as Label;
                Label lblTourSanctionNo = gvTourList.Rows[rowindex].FindControl("lblTourSanctionNo") as Label;

                Label lblEmpRecordID = gvTourList.Rows[rowindex].FindControl("lblEmpRecordID") as Label;
                Label lblTeamLeaderID = gvTourList.Rows[rowindex].FindControl("lblTeamLeaderID") as Label;
                

                Label lblEmployeeName = gvTourList.Rows[rowindex].FindControl("lblEmployeeName") as Label;
                Label lblEmpEmailID = gvTourList.Rows[rowindex].FindControl("lblEmpEmailID") as Label;
                Label lblEmployeeID = gvTourList.Rows[rowindex].FindControl("lblEmployeeID") as Label;
                Label lblUnitID = gvTourList.Rows[rowindex].FindControl("lblUnitID") as Label;

                Label lblDesignation = gvTourList.Rows[rowindex].FindControl("lblDesignation") as Label;
                Label lblStartDate = gvTourList.Rows[rowindex].FindControl("lblStartDate") as Label;
                Label lblEndDate = gvTourList.Rows[rowindex].FindControl("lblEndDate") as Label;
                Label lblCustVendName = gvTourList.Rows[rowindex].FindControl("lblCustVendName") as Label;
                Label lblPlaceOfVisit = gvTourList.Rows[rowindex].FindControl("lblPlaceOfVisit") as Label;

                // to find how this works
                ///
                Label lblCountryOfVisitID = gvTourList.Rows[rowindex].FindControl("lblCountryOfVisitID") as Label;
                Label lblTourBasedOnID = gvTourList.Rows[rowindex].FindControl("lblTourBasedOnID") as Label;
                Label lblTourInitiativeID = gvTourList.Rows[rowindex].FindControl("lblTourInitiativeID") as Label;
                ///

                Label lblPurposeOfVisitTypeId = gvTourList.Rows[rowindex].FindControl("lblPurposeOfVisitTypeId") as Label;
                Label lblJobNo = gvTourList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblBusSegmentID = gvTourList.Rows[rowindex].FindControl("lblBusSegmentID") as Label;
                Label lblBusSegment = gvTourList.Rows[rowindex].FindControl("lblBusSegment") as Label;
                Label lblTraveModeID = gvTourList.Rows[rowindex].FindControl("lblTraveModeID") as Label;
                Label lblTravelMode = gvTourList.Rows[rowindex].FindControl("lblTravelMode") as Label;
                Label lblTripTypeID = gvTourList.Rows[rowindex].FindControl("lblTripTypeID") as Label;
                Label lblLocalTravelTypeID = gvTourList.Rows[rowindex].FindControl("lblLocalTravelTypeID") as Label;
                Label lblLocalTravelType = gvTourList.Rows[rowindex].FindControl("lblLocalTravelType") as Label;
                Label lblExpenditureAmt = gvTourList.Rows[rowindex].FindControl("lblExpenditureAmt") as Label;
                Label lblExpenditureCurrencyID = gvTourList.Rows[rowindex].FindControl("lblExpenditureCurrencyID") as Label;


                Label lblAdvanceAmt = gvTourList.Rows[rowindex].FindControl("lblAdvanceAmt") as Label;
                Label lblAdvanceCurrencyID = gvTourList.Rows[rowindex].FindControl("lblAdvanceCurrencyID") as Label;
                Label lblRemarks = gvTourList.Rows[rowindex].FindControl("lblRemarks") as Label;
                Label lblTourStatusID = gvTourList.Rows[rowindex].FindControl("lblTourStatusID") as Label;
                //Label lblTourStatus = gvTourList.Rows[rowindex].FindControl("lblTourStatus") as Label;
                Label lblIsAdviceGenerated = gvTourList.Rows[rowindex].FindControl("lblIsAdviceGenerated") as Label;

                Label lblCretedByAccNo = gvTourList.Rows[rowindex].FindControl("lblCretedByAccNo") as Label;
                Label lblCretedByIFSC = gvTourList.Rows[rowindex].FindControl("lblCretedByIFSC") as Label;

                ImageButton imgBtnGenerateAdvice = gvTourList.Rows[rowindex].FindControl("imgBtnGenerateAdvice") as ImageButton;

                tourID = Convert.ToInt32(lblTourID.Text);
                ViewState["TOUR_ID"] = Convert.ToInt32(lblTourID.Text);
                ViewState["TOUR_NO"] = Convert.ToString(lblTourNO.Text);
                ViewState["EMP_RECORD_ID"] = Convert.ToInt32(lblEmpRecordID.Text);
                ViewState["TRAVEL_MODE_ID"] = Convert.ToInt32(lblTraveModeID.Text);
                ViewState["TOUR_BASED_ON_ID"] = Convert.ToInt32(lblTourBasedOnID.Text);
                ViewState["TEAM_LEADER_ID"] = Convert.ToInt32(lblTeamLeaderID.Text);

                lblLegend.Text = "Tour Information [" + lblTourNO.Text + "]";
                BindEmployee();
                ddlEmployee.SelectedValue = Convert.ToString(lblEmpRecordID.Text);
                txtEmployeeID.Text = Convert.ToString(lblEmployeeID.Text);
                txtDesignation.Text = Convert.ToString(lblDesignation.Text);
                txtStartDate.Text = Convert.ToString(lblStartDate.Text);
                txtEndDate.Text = Convert.ToString(lblEndDate.Text);
                txtCustVendName.Text = Convert.ToString(lblCustVendName.Text);
                txtPlaceOfVisit.Text = Convert.ToString(lblPlaceOfVisit.Text);

                //////////////

                if (Convert.ToInt32(lblCountryOfVisitID.Text) > 0)
                {
                    ddlCountryOfVisit.SelectedValue = Convert.ToString(lblCountryOfVisitID.Text);
                }
                else
                {
                    ddlCountryOfVisit.SelectedIndex = 0;
                }



                if (Convert.ToInt32(lblTourBasedOnID.Text) > 0)
                {
                    ddlTourBasedOn.SelectedValue = Convert.ToString(lblTourBasedOnID.Text);
                }
                else
                {
                    ddlTourBasedOn.SelectedIndex = 0;
                }

                ///////////////

                if (Convert.ToInt32(lblTourInitiativeID.Text) > 0)
                {
                    ddlTourInitiative.SelectedValue = Convert.ToString(lblTourInitiativeID.Text);
                }
                else
                {
                    ddlTourInitiative.SelectedIndex = 0;
                }




                ddlPurposeOfVisit.SelectedValue = Convert.ToString(lblPurposeOfVisitTypeId.Text);
                txtJobInqNo.Text = Convert.ToString(lblJobNo.Text);

                ddlBusinessSegment.SelectedValue = Convert.ToString(lblBusSegmentID.Text);
                if (lblBusSegmentID.Text == "12" || lblBusSegment.Text == "Other")
                    txtBusinessSegment.Text = Convert.ToString(lblBusSegment.Text);

                if (Convert.ToInt32(lblTraveModeID.Text) != 1)
                    ddlModeOfTravel.SelectedValue = Convert.ToString(lblTraveModeID.Text);

                if (lblTraveModeID.Text == "4" || lblTravelMode.Text == "Other")
                    txtModeOfTravel.Text = Convert.ToString(lblTravelMode.Text);

                ddlTypeOfTrip.SelectedValue = Convert.ToString(lblTripTypeID.Text);

                ddlLocalTravelling.SelectedValue = Convert.ToString(lblLocalTravelTypeID.Text);
                if (lblLocalTravelTypeID.Text == "5" || lblLocalTravelType.Text == "Other")
                    txtLocalTravelling.Text = Convert.ToString(lblLocalTravelType.Text);

                txtExpenditureAmt.Text = Convert.ToString(lblExpenditureAmt.Text);
                ddlExpenditureCurrency.SelectedValue = Convert.ToString(lblExpenditureCurrencyID.Text);
                txtAdvanceAmt.Text = Convert.ToString(lblAdvanceAmt.Text);
                ddlAdvanceCurrency.SelectedValue = Convert.ToString(lblAdvanceCurrencyID.Text);

                txtRemarks.Text = string.Empty;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.New)
                    {
                        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.New;//1
                        ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.New;

                        txtRemarks.Text = Convert.ToString(lblRemarks.Text);
                        EnableControls();
                        btnSubmit.Text = "Update Tour Information";
                        this.ModalPopupExtender1.Show();
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "STATUS" ||
                         Convert.ToString(e.CommandArgument) == "APPROVE")
                {
                    //New
                    if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.New)
                    {
                        if (Convert.ToInt32(lblTeamLeaderID.Text) == (int)TandTAllStatus.EnumOthers.FinalApproverID)
                        {
                            ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.FinalApprove; ;
                            ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.FinalApproved;
                        }
                        else
                        {
                            ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.HODApprove; ;
                            ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.HODApproved;
                        }

                       

                        btnSubmit.Text = "Approve Tour Information";
                    }

                    //Hod Approved
                    else if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                    {
                        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.FinalApprove;
                        ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.FinalApproved;

                        btnSubmit.Text = "Approve Tour Information";
                    }

                    //Mgmt Approved
                    //else if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved)
                    //{
                    //    ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.FinalApprove;
                    //    ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.FinalApproved;

                    //    btnSubmit.Text = "Approve Tour Information";
                    //}



                    DisableControls();
                    this.ModalPopupExtender1.Show();
                }
                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.New)
                    {
                        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.Delete; //3;
                        ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.Deleted;


                        btnSubmit.Text = "Delete Tour Information";
                    }
                    else
                    {
                        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.Cancel; //4;
                        ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.Cancelled;

                        btnSubmit.Text = "Cancel Tour Information";
                    }

                    DisableControls();
                    this.ModalPopupExtender1.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "ADVICE")
                {
                    string csv = string.Empty;
                    imgBtnGenerateAdvice.Visible = false;
                    if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) && !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                    {
                        imgBtnGenerateAdvice.Visible = true;
                        csv = GenerateAdviceCSV(Convert.ToString(lblEmployeeName.Text), Convert.ToString(lblTourSanctionNo.Text),
                                                Convert.ToString(lblAdvanceAmt.Text), Convert.ToString(lblEmpEmailID.Text),
                                                Convert.ToString(lblCretedByAccNo.Text), Convert.ToString(lblCretedByIFSC.Text),
                                                Convert.ToInt32(lblUnitID.Text));
                    }

                    if (!string.IsNullOrEmpty(csv))
                    {
                        int isGenerated = 0;
                        DataSet dsCheckForAdviceGenerated = new DataSet();
                        dsCheckForAdviceGenerated = objTourAndTravels.CheckForAdviceGenerated(tourID);

                        if (dsCheckForAdviceGenerated.Tables.Count > 0 && dsCheckForAdviceGenerated.Tables[0].Rows.Count > 0)
                        {
                            isGenerated = Convert.ToInt32(dsCheckForAdviceGenerated.Tables[0].Rows[0]["IS_ADVICE_GENERATED"]);
                            if (isGenerated == 0)
                                ToCSVNew01(csv, tourID, Convert.ToString(lblTourSanctionNo.Text));
                            else
                                ExceptionMessage("Advice is already generated for :" + Convert.ToString(lblTourSanctionNo.Text));
                        }
                        imgBtnGenerateAdvice.Visible = false;
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "VIEWPASSPORT")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblEmpRecordID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                {
                    int mailActID = 0;
                    TourSendMail tsm = new TourSendMail();
                    string value = tsm.SendMail(tourID);

                    if (!string.IsNullOrEmpty(value))
                    {
                        if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.New)
                        {
                            mailActID = (int)TandTAllStatus.EnumTourAct.New;
                        }
                        else if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                        {
                            mailActID = (int)TandTAllStatus.EnumTourAct.HODApprove;
                        }
                        //else if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved)
                        //{
                        //    mailActID = (int)TandTAllStatus.EnumTourAct.MgmtApprove;
                        //}
                        else if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
                        {
                            mailActID = (int)TandTAllStatus.EnumTourAct.FinalApprove;
                        }
                        else if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.Deleted)
                        {
                            mailActID = (int)TandTAllStatus.EnumTourAct.Delete;
                        }
                        else if (Convert.ToInt32(lblTourStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                        {
                            mailActID = (int)TandTAllStatus.EnumTourAct.Cancel;
                        }

                        int val = objTourAndTravels.UpdateTourInfoMailStatus(mailActID, tourID, Convert.ToInt32(Session["CHK_ACC"]));
                        SuccessMessage("Mail sent successfully.");
                        GetTourList();
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    ModalPopupExtender4.Show();
                    iframeViewTourInformationInPDF.Attributes.Add("src", "TourInfoInPDFNew.aspx?tourid=" + Convert.ToString(lblTourID.Text) + "&tourno=" + Convert.ToString(lblTourNO.Text));
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

    protected void gvTourList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                Label lblTourNo = (Label)e.Row.FindControl("lblTourNo");
                Label lblStatus = (Label)e.Row.FindControl("lblTourStatus");
                Label lblStatusID = (Label)e.Row.FindControl("lblTourStatusID");
                Label lblEmpRecordID = (Label)e.Row.FindControl("lblEmpRecordID");
                Label lblTeamLeaderID = (Label)e.Row.FindControl("lblTeamLeaderID");
                Label lblAdvanceAmt = (Label)e.Row.FindControl("lblAdvanceAmt");
                Label lblAdvanceCurrencyID = (Label)e.Row.FindControl("lblAdvanceCurrencyID");
                Label lblTraveModeID = (Label)e.Row.FindControl("lblTraveModeID");
                Label lblTourBasedOnID = (Label)e.Row.FindControl("lblTourBasedOnID");
                Label lblMgmtHODEmpRecordID = (Label)e.Row.FindControl("lblMgmtHODEmpRecordID");
                Label lblFinalHODEmpRecordID = (Label)e.Row.FindControl("lblFinalHODEmpRecordID");


                Label lblIsApprovalMailSent = (Label)e.Row.FindControl("lblIsApprovalMailSent");
                Label lblIsApprovedMailSent = (Label)e.Row.FindControl("lblIsApprovedMailSent");
                Label lblIsDeletedMailSent = (Label)e.Row.FindControl("lblIsDeletedMailSent");
                Label lblIsCancelledMailSent = (Label)e.Row.FindControl("lblIsCancelledMailSent");

                Label lblIsMgmtApprovedMailSent = (Label)e.Row.FindControl("lblIsMgmtApprovedMailSent");
                Label lblIsFinalApprovedMailSent = (Label)e.Row.FindControl("lblIsFinalApprovedMailSent");


                Label lblIsSentToSettlement = (Label)e.Row.FindControl("lblIsSentToSettlement");
                Label lblIsAdviceGenerated = (Label)e.Row.FindControl("lblIsAdviceGenerated");

                Label lblCretedByAccNo = (Label)e.Row.FindControl("lblCretedByAccNo");
                Label lblCretedByIFSC = (Label)e.Row.FindControl("lblCretedByIFSC");

                ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");
                Label lblPassportCopyName = (Label)e.Row.FindControl("lblPassportCopyName");
                ImageButton btnPassportCopy = (ImageButton)e.Row.FindControl("btnPassportCopy");

                ImageButton imgBtnGenerateAdvice = (ImageButton)e.Row.FindControl("imgBtnGenerateAdvice");
                ImageButton imgBtnSendMail = (ImageButton)e.Row.FindControl("imgBtnSendMail");


                if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) &&
                    !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                    ViewState["CHK_ACC"] = 1;
                else
                    ViewState["CHK_ACC"] = 0;


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

                Button btnCancel = (Button)e.Row.FindControl("btnCancel");
                Button btnApprove = (Button)e.Row.FindControl("btnApprove");

                string status = lblStatus.Text;
                int statusID = Convert.ToInt32(lblStatusID.Text);

                int travelmodeId =  Convert.ToInt32(lblTraveModeID.Text);
                
                int tourBasedOnId02 = Convert.ToInt32(lblTourBasedOnID.Text);



                imgProperties.Visible = false;
                btnCancel.Visible = false;
                btnApprove.Visible = false;
                imgStatus.Enabled = false;
                imgBtnGenerateAdvice.Visible = false;
                imgBtnSendMail.Visible = false;

                departmentID = Convert.ToInt32(Session["DEPARTMENT_ID"]);
                int empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                if (statusID == (int)TandTAllStatus.EnumTourStatus.New)//status == "New" && 
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/New02.png";
                    imgStatus.ToolTip = "New";

                    if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                        empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                    {
                        imgProperties.Visible = true;
                        imgProperties.ToolTip = "Edit Tour-: " + lblTourNo.Text;
                    }

                    if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                        Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                        empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                    {
                        //btnCancel.Visible = true;
                        //btnCancel.Text = "Delete";
                        //btnCancel.ToolTip = "Delete Tour-: " + lblTourNo.Text;


                        btnCancel.Visible = true;
                        
                        if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                        {
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Tour-: " + lblTourNo.Text;
                        }
                        else if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID)
                        {
                            btnCancel.Text = "Delete";
                            btnCancel.ToolTip = "Delete Tour-: " + lblTourNo.Text;
                        }
                        else
                        {
                            btnCancel.Text = "Delete";
                            btnCancel.ToolTip = "Delete Tour-: " + lblTourNo.Text;
                        }


                    }

                    if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                        empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                    {
                        imgStatus.Enabled = true;
                        btnApprove.Visible = true;
                        btnApprove.ToolTip = "Approve Tour-: " + lblTourNo.Text;
                    }


                    if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID &&
                        Convert.ToInt32(lblIsApprovalMailSent.Text) == 0)
                    {
                        imgBtnSendMail.Visible = true;
                        imgBtnSendMail.ToolTip = "Send Approval Mail";
                    }
                }

                #region hod approved history

                //else if (statusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)//status == "Approved" && 
                //{
                //    imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                //    imgStatus.ToolTip = "HOD Approved";
                //    imgProperties.Visible = false;
                //    imgStatus.Enabled = false;

                //    //if (Convert.ToInt32(lblTraveModeID.Text) != (int)TandTAllStatus.EnumTravelMode.AirBusiness)
                //    //{
                //    //    if (empRecordID == (int)TandTAllStatus.EnumOthers.AccountPerson &&
                //    //        Convert.ToDouble(lblAdvanceAmt.Text) > 0 &&
                //    //        Convert.ToInt32(lblAdvanceCurrencyID.Text) == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
                //    //        Convert.ToInt32(lblIsAdviceGenerated.Text) == 0)
                //    //    {
                //    //        imgBtnGenerateAdvice.Visible = false;
                //    //        if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) &&
                //    //            !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                //    //        {
                //    //            imgBtnGenerateAdvice.Visible = true;
                //    //            imgBtnGenerateAdvice.ToolTip = "Generate Advice";
                //    //        }
                //    //    }
                //    //}

                //    if ((Convert.ToInt32(lblTraveModeID.Text) == (int)TandTAllStatus.EnumTravelMode.AirBusiness) ||
                //        (Convert.ToInt32(lblTourBasedOnID.Text) == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
                //    {
                //        //if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID ||
                //        //Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                //        //empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                //        //{
                //        //    btnCancel.Visible = true;
                //        //    btnCancel.Text = "Delete";
                //        //    btnCancel.ToolTip = "Delete Tour-: " + lblTourNo.Text;
                //        //}

                //        if (Convert.ToInt32(lblFinalHODEmpRecordID.Text) == empRecordID ||
                //        empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                //        {
                //            imgStatus.Enabled = true;
                //            btnApprove.Visible = true;
                //            btnApprove.ToolTip = "Approve Tour-: " + lblTourNo.Text;
                //            btnCancel.Visible = true;
                //            btnCancel.Text = "Delete";
                //            btnCancel.ToolTip = "Delete Tour-: " + lblTourNo.Text;
                //        }
                //    }
                //    else
                //    {
                //        if (empRecordID == (int)TandTAllStatus.EnumOthers.AccountPerson &&
                //           Convert.ToDouble(lblAdvanceAmt.Text) > 0 &&
                //           Convert.ToInt32(lblAdvanceCurrencyID.Text) == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
                //           Convert.ToInt32(lblIsAdviceGenerated.Text) == 0)
                //        {
                //            imgBtnGenerateAdvice.Visible = false;
                //            if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) &&
                //                !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                //            {
                //                imgBtnGenerateAdvice.Visible = true;
                //                imgBtnGenerateAdvice.ToolTip = "Generate Advice";
                //            }
                //        }
                //    }



                //    if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID)
                //    {
                //        if (Convert.ToInt32(lblIsApprovedMailSent.Text) == 0)
                //        {
                //            imgBtnSendMail.Visible = true;
                //            imgBtnSendMail.ToolTip = "Send Approved Mail";
                //        }

                //        if (Convert.ToInt32(lblIsSentToSettlement.Text) == 0)
                //        {
                //            btnCancel.Visible = true;
                //        }
                //    }
                //}
                #endregion

                else if (statusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                    imgStatus.ToolTip = "HOD Approved";
                    imgProperties.Visible = false;
                    imgStatus.Enabled = false;



                    if ((Convert.ToInt32(lblTraveModeID.Text) == (int)TandTAllStatus.EnumTravelMode.AirBusiness) ||
                       (Convert.ToInt32(lblTourBasedOnID.Text) == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
                    {
                        if (Convert.ToInt32(lblFinalHODEmpRecordID.Text) == empRecordID ||
                            empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                        {
                            imgStatus.Enabled = true;
                            btnApprove.Visible = true;
                            btnApprove.ToolTip = "Approve Tour-: " + lblTourNo.Text;
                            btnCancel.Visible = true;
                            //btnCancel.Text = "Delete";
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Tour-: " + lblTourNo.Text;
                        }
                    }
                    else
                    {
                        if (empRecordID == (int)TandTAllStatus.EnumOthers.AccountPerson &&
                           Convert.ToDouble(lblAdvanceAmt.Text) > 0 &&
                           Convert.ToInt32(lblAdvanceCurrencyID.Text) == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
                           Convert.ToInt32(lblIsAdviceGenerated.Text) == 0)
                        {
                            imgBtnGenerateAdvice.Visible = false;
                            if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) &&
                                !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                            {
                                imgBtnGenerateAdvice.Visible = true;
                                imgBtnGenerateAdvice.ToolTip = "Generate Advice";
                            }
                        }
                    }



                    if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                    {
                        //if (Convert.ToInt32(lblTeamLeaderID.Text) == 0)
                        //    lblIsApprovedMailSent.Text
                        if (Convert.ToInt32(lblIsApprovedMailSent.Text) == 0)
                        {
                            imgBtnSendMail.Visible = true;
                            imgBtnSendMail.ToolTip = "Send Approved Mail";
                        }

                        if (Convert.ToInt32(lblIsSentToSettlement.Text) == 0)
                        {
                            btnCancel.Visible = true;
                        }
                    }






                    //if (Convert.ToInt32(lblFinalHODEmpRecordID.Text) == empRecordID ||
                    //    empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)

                    //{
                    //    imgStatus.Enabled = true;
                    //    btnApprove.Visible = true;
                    //    btnApprove.ToolTip = "Approve Tour-: " + lblTourNo.Text;
                    //    btnCancel.Visible = true;
                    //    btnCancel.Text = "Delete";
                    //    btnCancel.ToolTip = "Delete Tour-: " + lblTourNo.Text;
                    //}



                    //if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                    //{
                    //    if (Convert.ToInt32(lblIsApprovedMailSent.Text) == 0)
                    //    {
                    //        imgBtnSendMail.Visible = true;
                    //        imgBtnSendMail.ToolTip = "Send Approved Mail";
                    //    }

                    //    if (Convert.ToInt32(lblIsSentToSettlement.Text) == 0)
                    //    {
                    //        btnCancel.Visible = true;
                    //    }
                    //}
                }

                //else if (statusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved)
                //{
                    
                //    imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                //    imgStatus.ToolTip = "Mgmt HOD Approved";
                //    imgProperties.Visible = false;
                //    imgStatus.Enabled = false;

                //    if ((Convert.ToInt32(lblTraveModeID.Text) == (int)TandTAllStatus.EnumTravelMode.AirBusiness) ||
                //        (Convert.ToInt32(lblTourBasedOnID.Text) == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
                //    {
                //        if (Convert.ToInt32(lblFinalHODEmpRecordID.Text) == empRecordID ||
                //            empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                //        {
                //            imgStatus.Enabled = true;
                //            btnApprove.Visible = true;
                //            btnApprove.ToolTip = "Approve Tour-: " + lblTourNo.Text;
                //            btnCancel.Visible = true;
                //            btnCancel.Text = "Delete";
                //            btnCancel.ToolTip = "Delete Tour-: " + lblTourNo.Text;
                //        }
                //    }
                //    else
                //    {
                //        if (empRecordID == (int)TandTAllStatus.EnumOthers.AccountPerson &&
                //           Convert.ToDouble(lblAdvanceAmt.Text) > 0 &&
                //           Convert.ToInt32(lblAdvanceCurrencyID.Text) == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
                //           Convert.ToInt32(lblIsAdviceGenerated.Text) == 0)
                //        {
                //            imgBtnGenerateAdvice.Visible = false;
                //            if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) &&
                //                !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                //            {
                //                imgBtnGenerateAdvice.Visible = true;
                //                imgBtnGenerateAdvice.ToolTip = "Generate Advice";
                //            }
                //        }
                //    }


                //    if (Convert.ToInt32(lblMgmtHODEmpRecordID.Text) == empRecordID)
                //    {
                //        if (Convert.ToInt32(lblIsMgmtApprovedMailSent.Text) == 0)
                //        {
                //            imgBtnSendMail.Visible = true;
                //            imgBtnSendMail.ToolTip = "Send Approved Mail";
                //        }

                //        if (Convert.ToInt32(lblIsSentToSettlement.Text) == 0)
                //        {
                //            btnCancel.Visible = true;
                //        }
                //    }
                //}


                else if (statusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved || statusID == (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer)
                {

                    if(tourBasedOnId02 == 2 || travelmodeId == 5)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Approved02.jpg";
                        imgStatus.ToolTip = "Final Approved";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                    }
                    else
                    {
                        //imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Approved02.jpg";
                        imgStatus.ToolTip = "HOD Final Approved";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                    }

                    



                    //imgStatus.ImageUrl = "~/Images/NEWICONS/Approved02.jpg";
                    //imgStatus.ToolTip = "Final Approved";
                    //imgProperties.Visible = false;
                    //imgStatus.Enabled = false;

                    //if (Convert.ToInt32(lblTraveModeID.Text) == (int)TandTAllStatus.EnumTravelMode.AirBusiness)
                    //{
                    //    if (empRecordID == (int)TandTAllStatus.EnumOthers.AccountPerson &&
                    //        Convert.ToDouble(lblAdvanceAmt.Text) > 0 &&
                    //        Convert.ToInt32(lblAdvanceCurrencyID.Text) == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
                    //        Convert.ToInt32(lblIsAdviceGenerated.Text) == 0)
                    //    {
                    //        imgBtnGenerateAdvice.Visible = false;
                    //        if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) &&
                    //            !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                    //        {
                    //            imgBtnGenerateAdvice.Visible = true;
                    //            imgBtnGenerateAdvice.ToolTip = "Generate Advice";
                    //        }
                    //    }
                    //}

                    if (empRecordID == (int)TandTAllStatus.EnumOthers.AccountPerson &&
                            Convert.ToDouble(lblAdvanceAmt.Text) > 0 &&
                            Convert.ToInt32(lblAdvanceCurrencyID.Text) == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
                            Convert.ToInt32(lblIsAdviceGenerated.Text) == 0)
                    {
                        imgBtnGenerateAdvice.Visible = false;
                        if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) &&
                            !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                        {
                            imgBtnGenerateAdvice.Visible = true;
                            imgBtnGenerateAdvice.ToolTip = "Generate Advice";
                        }
                    }


                    //if (Convert.ToInt32(lblFinalHODEmpRecordID.Text) == empRecordID)
                    //{


                    if (tourBasedOnId02 == 2 || travelmodeId == 5)
                    {
                        if (Convert.ToInt32(lblFinalHODEmpRecordID.Text) == empRecordID)
                        {
                            if (Convert.ToInt32(lblIsFinalApprovedMailSent.Text) == 0)
                            {
                                imgBtnSendMail.Visible = true;
                                imgBtnSendMail.ToolTip = "Send Approved Mail";
                            }

                            if (Convert.ToInt32(lblIsSentToSettlement.Text) == 0)
                            {
                                btnCancel.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                        {
                            if (Convert.ToInt32(lblIsFinalApprovedMailSent.Text) == 0)
                            {
                                imgBtnSendMail.Visible = true;
                                imgBtnSendMail.ToolTip = "Send Approved Mail";
                            }

                            if (Convert.ToInt32(lblIsSentToSettlement.Text) == 0)
                            {
                                btnCancel.Visible = true;
                            }
                        }
                    }

                        
                    //if (Convert.ToInt32(lblFinalHODEmpRecordID.Text) == empRecordID)
                    //{
                    //    if (Convert.ToInt32(lblIsFinalApprovedMailSent.Text) == 0)
                    //    {
                    //        imgBtnSendMail.Visible = true;
                    //        imgBtnSendMail.ToolTip = "Send Approved Mail";
                    //    }

                    //    if (Convert.ToInt32(lblIsSentToSettlement.Text) == 0)
                    //    {
                    //        btnCancel.Visible = true;
                    //    }
                    //}
                }
                else if (statusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved)
                {

                    if (tourBasedOnId02 == 2 || travelmodeId == 5)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Approved02.jpg";
                        imgStatus.ToolTip = "Final Approved";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                    }
                    else
                    {
                        //imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                        imgStatus.ImageUrl = "~/Images/NEWICONS/Approved02.jpg";
                        imgStatus.ToolTip = "HOD Final Approved";
                        imgProperties.Visible = false;
                        imgStatus.Enabled = false;
                    }


                }

                else if (statusID == (int)TandTAllStatus.EnumTourStatus.Deleted)//status == "Deleted" && 
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Deleted02.png";
                    imgStatus.ToolTip = "Deleted";
                    imgStatus.Enabled = false;
                    imgProperties.Visible = false;

                    if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID &&
                        Convert.ToInt32(lblIsDeletedMailSent.Text) == 0)
                    {
                        imgBtnSendMail.Visible = true;
                        imgBtnSendMail.ToolTip = "Send Deleted Mail";
                    }
                }

                else if (statusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)//status == "Cancelled" && 
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Cancelled03.png";
                    imgStatus.ToolTip = "Cancelled";
                    imgStatus.Enabled = false;
                    imgProperties.Visible = false;

                    if (Convert.ToInt32(lblIsCancelledMailSent.Text) == 0)
                    {
                        imgBtnSendMail.Visible = true;
                        imgBtnSendMail.ToolTip = "Send Cancelled Mail";
                    }
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

    protected void btnAddNewTourInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TOUR/TourInformation.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)TandTAllStatus.EnumTourAct.New)
                UpdateTourInformation();
            else
                UpdateTourStatus();
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindTourStatus()
    {
        try
        {

            dsTourStatus = objTourAndTravels.GetTourStatus();
            if (dsTourStatus.Tables.Count > 0 && dsTourStatus.Tables[0].Rows.Count > 0)
            {
                ddlTourStatus.DataSource = dsTourStatus.Tables[0];
                ddlTourStatus.DataTextField = "TOUR_STATUS_NAME";
                ddlTourStatus.DataValueField = "TOUR_STATUS_ID";
                ddlTourStatus.DataBind();
                ddlTourStatus.Items.Insert(0, "All");
                ddlTourStatus.SelectedIndex = 0;

                //if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tourno"])))
                //    ddlTourStatus.SelectedIndex = 0;
                //else
                //    ddlTourStatus.SelectedIndex = 1;
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
            if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 4 ||
                Convert.ToInt32(Session["DEPARTMENT_ID"]) == 2 ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == 117 ||
                Convert.ToString(Session["USER_TYPE"]) == "A")
                dsEmployee = objTourAndTravels.GetEmployeeForTravel(0);
            else
                dsEmployee = objTourAndTravels.GetEmployeeForTravel(Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
                //ddlEmployee.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);


                ddlEmployeeName.DataSource = dsEmployee.Tables[0];
                ddlEmployeeName.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeName.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeName.DataBind();
                ddlEmployeeName.Items.Insert(0, "Select");
                ddlEmployeeName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTourList()
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

            if (!string.IsNullOrEmpty(txtTourNo.Text))
                tourNoSearch = txtTourNo.Text.Trim();
            else
                tourNoSearch = string.Empty;

            if (!string.IsNullOrEmpty(txtSanctionNo.Text))
                sanctionNoSearch = txtSanctionNo.Text.Trim();
            else
                sanctionNoSearch = string.Empty;


            if (ddlTourStatus.SelectedIndex > 0)
                tourStatusIDSearch = Convert.ToInt32(ddlTourStatus.SelectedValue);
            else
                tourStatusIDSearch = 0;


            //country of visit

            //if (ddlTourStatus.SelectedIndex > 0)
            //    countryOfVisit = Convert.ToInt32(ddlTourStatus.SelectedValue);
            //else
            //   countryOfVisit = 0;



            if (ddlEmployeeName.SelectedIndex > 0)
            {
                teamMemberID = Convert.ToInt32(ddlEmployeeName.SelectedValue);
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

            dsTourList = objTourAndTravels.GetTourList(startDate, endDate, tourNoSearch, sanctionNoSearch, tourStatusIDSearch,
                                                        empRecordID, teamMemberID, teamMembers);
            if (dsTourList.Tables.Count > 0 && dsTourList.Tables[0].Rows.Count > 0)
            {
                Session["TOUR_LIST"] = dsTourList.Tables[0];
                gvTourList.DataSource = dsTourList.Tables[0];
                gvTourList.DataBind();
            }
            else
            {
                Session["TOUR_LIST"] = null;
                gvTourList.DataSource = null;
                gvTourList.DataBind();
            }
            lblRecords.Text = "Records[" + dsTourList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }




    private void BindVisitType()
    {
        try
        {
            dsVisitType = objTourAndTravels.GetPrimaryDetails("sp_get_visit_type");
            if (dsVisitType.Tables.Count > 0 && dsVisitType.Tables[0].Rows.Count > 0)
            {
                ddlPurposeOfVisit.DataSource = dsVisitType.Tables[0];
                ddlPurposeOfVisit.DataTextField = "VISIT_TYPE";
                ddlPurposeOfVisit.DataValueField = "VISIT_TYPE_ID";
                ddlPurposeOfVisit.DataBind();
                ddlPurposeOfVisit.Items.Insert(0, "Select");
                ddlPurposeOfVisit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindSegmentType()
    {
        try
        {
            dsSegmentType = objTourAndTravels.GetPrimaryDetails("sp_get_bus_segment");
            if (dsSegmentType.Tables.Count > 0 && dsSegmentType.Tables[0].Rows.Count > 0)
            {
                ddlBusinessSegment.DataSource = dsSegmentType.Tables[0];
                ddlBusinessSegment.DataTextField = "BUS_SEGMENT";
                ddlBusinessSegment.DataValueField = "BUS_SEGMENT_ID";
                ddlBusinessSegment.DataBind();
                ddlBusinessSegment.Items.Insert(0, "Select");
                ddlBusinessSegment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTravelMode()
    {
        try
        {
            dsTravelMode = objTourAndTravels.GetPrimaryDetails("sp_get_travel_mode");
            if (dsTravelMode.Tables.Count > 0 && dsTravelMode.Tables[0].Rows.Count > 0)
            {
                ddlModeOfTravel.DataSource = dsTravelMode.Tables[0];
                ddlModeOfTravel.DataTextField = "TRAVEL_MODE";
                ddlModeOfTravel.DataValueField = "TRAVEL_MODE_ID";
                ddlModeOfTravel.DataBind();
                ddlModeOfTravel.Items.Insert(0, "Select");
                ddlModeOfTravel.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTripType()
    {
        try
        {
            dsTripType = objTourAndTravels.GetPrimaryDetails("sp_get_trip_type");
            if (dsTripType.Tables.Count > 0 && dsTripType.Tables[0].Rows.Count > 0)
            {
                ddlTypeOfTrip.DataSource = dsTripType.Tables[0];
                ddlTypeOfTrip.DataTextField = "TRIP_TYPE";
                ddlTypeOfTrip.DataValueField = "TRIP_TYPE_ID";
                ddlTypeOfTrip.DataBind();
                ddlTypeOfTrip.Items.Insert(0, "Select");
                ddlTypeOfTrip.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLocalTravelType()
    {
        try
        {
            dsLocalTravelType = objTourAndTravels.GetPrimaryDetails("sp_get_local_travel_type");
            if (dsLocalTravelType.Tables.Count > 0 && dsLocalTravelType.Tables[0].Rows.Count > 0)
            {
                ddlLocalTravelling.DataSource = dsLocalTravelType.Tables[0];
                ddlLocalTravelling.DataTextField = "LOCAL_TRAVEL_TYPE";
                ddlLocalTravelling.DataValueField = "LOCAL_TRAVEL_TYPE_ID";
                ddlLocalTravelling.DataBind();
                ddlLocalTravelling.Items.Insert(0, "Select");
                ddlLocalTravelling.SelectedIndex = 0;
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
                ddlExpenditureCurrency.DataSource = dsCurrency.Tables[0];
                ddlExpenditureCurrency.DataTextField = "CURRENCY_CODE";
                ddlExpenditureCurrency.DataValueField = "CURRENCY_ID";
                ddlExpenditureCurrency.DataBind();
                ddlExpenditureCurrency.SelectedValue = "68";

                ddlAdvanceCurrency.DataSource = dsCurrency.Tables[0];
                ddlAdvanceCurrency.DataTextField = "CURRENCY_CODE";
                ddlAdvanceCurrency.DataValueField = "CURRENCY_ID";
                ddlAdvanceCurrency.DataBind();
                ddlAdvanceCurrency.SelectedValue = "68";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCountry()
    {
        try
        {
            dsCountry = objTourAndTravels.GetPrimaryDetails("sp_get_country");
            if (dsCountry.Tables.Count > 0 && dsCountry.Tables[0].Rows.Count > 0)
            {
                ddlCountryOfVisit.DataSource = dsCountry.Tables[0];
                ddlCountryOfVisit.DataTextField = "COUNTRY_NAME";
                ddlCountryOfVisit.DataValueField = "COUNTRY_ID";
                ddlCountryOfVisit.DataBind();
                ddlCountryOfVisit.SelectedValue = "68";

                //ddlAdvanceCurrency.DataSource = dsCountry.Tables[0];
                //ddlAdvanceCurrency.DataTextField = "CURRENCY_CODE";
                //ddlAdvanceCurrency.DataValueField = "CURRENCY_ID";
                //ddlAdvanceCurrency.DataBind();
                //ddlAdvanceCurrency.SelectedValue = "68";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTourBasedOn()
    {
        try
        {
            dsTourBasedOn = objTourAndTravels.GetPrimaryDetails("sp_get_tour_Based_On");
            if (dsTourBasedOn.Tables.Count > 0 && dsTourBasedOn.Tables[0].Rows.Count > 0)
            {
                ddlTourBasedOn.DataSource = dsTourBasedOn.Tables[0];
                ddlTourBasedOn.DataTextField = "NAME";
                ddlTourBasedOn.DataValueField = "ID";
                ddlTourBasedOn.DataBind();
                ddlTourBasedOn.Items.Insert(0, "Select");
                ddlTourBasedOn.SelectedIndex = 0;


            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTourInitiative()
    {
        try
        {
            dsTourInitiative = objTourAndTravels.GetPrimaryDetails("sp_get_tour_initiative_types");
            if (dsTourInitiative.Tables.Count > 0 && dsTourInitiative.Tables[0].Rows.Count > 0)
            {
                ddlTourInitiative.DataSource = dsTourInitiative.Tables[0];
                ddlTourInitiative.DataTextField = "INITIATIVE_NAME";
                ddlTourInitiative.DataValueField = "INITIATIVE_ID";
                ddlTourInitiative.DataBind();
                ddlTourInitiative.Items.Insert(0, "Not Applicable");
                ddlTourInitiative.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateTourInformation()
    {
        try
        {
            int tourID = Convert.ToInt32(ViewState["TOUR_ID"]);
            string tourNo = Convert.ToString(ViewState["TOUR_NO"]);
            int empRecordID = 0;

            lblDateMsg.Visible = false;
            lblDateMsg.Text = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;

            if (!string.IsNullOrEmpty(hdStartDate.Value))
                startDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(hdEndDate.Value))
                endDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtCustVendName.Text))
                custVendName = txtCustVendName.Text;
            else
                custVendName = string.Empty;

            if (!string.IsNullOrEmpty(txtPlaceOfVisit.Text))
                placeOfVisit = txtPlaceOfVisit.Text;
            else
                placeOfVisit = string.Empty;

            if (ddlPurposeOfVisit.SelectedIndex > 0)
                purposeOfVisitID = Convert.ToInt32(ddlPurposeOfVisit.SelectedValue);
            else
                purposeOfVisitID = 0;

            if (!string.IsNullOrEmpty(txtJobInqNo.Text))
                jobNo = txtJobInqNo.Text;
            else
                jobNo = string.Empty;


            if (ddlBusinessSegment.SelectedIndex > 0)
            {
                busSegmentID = Convert.ToInt32(ddlBusinessSegment.SelectedValue);
                busSegmentOther = string.Empty;
                if (ddlBusinessSegment.SelectedItem.Text == "Other")
                {
                    if (!string.IsNullOrEmpty(txtBusinessSegment.Text))
                        busSegmentOther = Convert.ToString(txtBusinessSegment.Text);
                    else
                        busSegmentOther = string.Empty;
                }
            }
            else
            {
                busSegmentID = 0;
                busSegmentOther = string.Empty;
            }

            if (ddlModeOfTravel.SelectedIndex > 0)
            {
                modeOfTavelID = Convert.ToInt32(ddlModeOfTravel.SelectedValue);
                modeOfTavelOther = string.Empty;
                if (ddlModeOfTravel.SelectedItem.Text == "Other")
                {
                    if (!string.IsNullOrEmpty(txtModeOfTravel.Text))
                        modeOfTavelOther = Convert.ToString(txtModeOfTravel.Text);
                    else
                        modeOfTavelOther = string.Empty;
                }
            }
            else
            {
                modeOfTavelID = 0;
                modeOfTavelOther = string.Empty;
            }

            if (ddlTypeOfTrip.SelectedIndex > 0)
                tripTypeID = Convert.ToInt32(ddlTypeOfTrip.SelectedValue);
            else
                tripTypeID = 0;

            if (ddlLocalTravelling.SelectedIndex > 0)
            {
                localTravellingID = Convert.ToInt32(ddlLocalTravelling.SelectedValue);
                localTravellingOther = string.Empty;
                if (ddlLocalTravelling.SelectedItem.Text == "Other")
                {
                    if (!string.IsNullOrEmpty(txtLocalTravelling.Text))
                        localTravellingOther = Convert.ToString(txtLocalTravelling.Text);
                    else
                        localTravellingOther = string.Empty;
                }
            }
            else
            {
                localTravellingID = 0;
                localTravellingOther = string.Empty;
            }


            if (!string.IsNullOrEmpty(txtExpenditureAmt.Text))
                expectedExpenditure = Convert.ToDouble(txtExpenditureAmt.Text);
            else
                expectedExpenditure = 0;

            if (ddlExpenditureCurrency.SelectedIndex > 0)
                expectedExpenditureCurrencyID = Convert.ToInt32(ddlExpenditureCurrency.SelectedValue);
            else
                expectedExpenditureCurrencyID = 0;


            if (!string.IsNullOrEmpty(txtAdvanceAmt.Text))
                advanceRequired = Convert.ToDouble(txtAdvanceAmt.Text);
            else
                advanceRequired = 0;


            if (ddlAdvanceCurrency.SelectedIndex > 0)
                advanceRequiredCurrencyID = Convert.ToInt32(ddlAdvanceCurrency.SelectedValue);
            else
                advanceRequiredCurrencyID = 0;


            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;
            else
                remarks = string.Empty;


            //////////


            //if (!string.IsNullOrEmpty(ddlCountryOfVisit.SelectedValue))
            //    countryOfVisit = ddlCountryOfVisit.SelectedValue;
            //else
            //   countryOfVisit = string.Empty;


            if (ddlCountryOfVisit.SelectedIndex > 0)
                countryOfVisit = Convert.ToInt32(ddlCountryOfVisit.SelectedValue);
            else
                countryOfVisit = 0;

            if (ddlTourBasedOn.SelectedIndex > 0)
                tourBasedOnID = Convert.ToInt32(ddlTourBasedOn.SelectedValue);
            else
                tourBasedOnID = 0;
            ////////////

            if (ddlTourInitiative.SelectedIndex > 0)
                tourInitiativeID = Convert.ToInt32(ddlTourInitiative.SelectedValue);
            else
                tourInitiativeID = 0;




            int value = objTourAndTravels.InsertTourInformation(tourID, empRecordID, startDate, endDate,
                                                            custVendName, placeOfVisit,
                                                            purposeOfVisitID, jobNo,
                                                            busSegmentID, busSegmentOther,
                                                            modeOfTavelID, modeOfTavelOther,
                                                            tripTypeID, localTravellingID, localTravellingOther,
                                                            expectedExpenditure, expectedExpenditureCurrencyID,
                                                            advanceRequired, advanceRequiredCurrencyID, remarks,
                                                            fileUploadPassportCopyFileName, fileUploadPassportCopyBytes,
                                                            Convert.ToInt32(Session["EMP_RECORD_ID"]), countryOfVisit,
                                                            tourBasedOnID, tourInitiativeID);



            if (value > 0)
            {
                TourSendMail tsm = new TourSendMail();
                //int sendMailValue = tsm.SendMail(tourID);
                string sendMailValue = tsm.SendMail(tourID);
               
                if (!string.IsNullOrEmpty(sendMailValue))
                {
                    SuccessMessage("Tour No. '" + tourNo + "' updated and mail sent successfully!");
                    GetTourList();
                }
                else
                {
                    SuccessMessage("Tour No. '" + tourNo + "' updated successfully!");
                    GetTourList(); 
                }
            }
            else
                ExceptionMessage("Please try again!");

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    //private void UpdateTourStatus()
    //{
    //    try
    //    {
    //        int actID = Convert.ToInt32(ViewState["ACT_ID"]);
    //        int tourID = Convert.ToInt32(ViewState["TOUR_ID"]);
    //        string tourNO = Convert.ToString(ViewState["TOUR_NO"]);
    //        int empRecordID = Convert.ToInt32(ViewState["EMP_RECORD_ID"]);
    //        int tourStatusID = Convert.ToInt32(ViewState["TOUR_STATUS_ID"]);
    //        int travelModeID = Convert.ToInt32(ViewState["TRAVEL_MODE_ID"]);

    //        string remarks = string.Empty;

    //        isMailSend = false;

    //        if (actID == (int)TandTAllStatus.EnumTourAct.HODApprove)
    //        {
    //            isMailSend = true;
    //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.HODApproved;
    //        }

    //        if (actID == (int)TandTAllStatus.EnumTourAct.MgmtApprove)
    //        {
    //            isMailSend = true;
    //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved;
    //        }

    //        else if (actID == (int)TandTAllStatus.EnumTourAct.FinalApprove)
    //        {
    //            isMailSend = true;
    //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.FinalApproved;
    //        }

    //        //else if (actID == (int)TandTAllStatus.EnumTourAct.Delete)
    //        //{
    //        //    tourStatusID = (int)TandTAllStatus.EnumTourStatus.Deleted;
    //        //    if (empRecordID != 0)
    //        //    {
    //        //        if (empRecordID != Convert.ToInt32(Session["EMP_RECORD_ID"]))
    //        //        {
    //        //            isMailSend = true;
    //        //        }
    //        //        else
    //        //        {
    //        //            int val = objTourAndTravels.UpdateTourInfoMailStatus(actID
    //        //                                                               , tourID
    //        //                                                               , Convert.ToInt32(Session["CHK_ACC"]));
    //        //        }
    //        //    }
    //        //}
    //        else if (actID == (int)TandTAllStatus.EnumTourAct.Delete)
    //        {
    //            isMailSend = true;
    //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.Deleted;
    //        }

    //        else if (actID == (int)TandTAllStatus.EnumTourAct.Cancel)//4)
    //        {
    //            isMailSend = true;
    //            tourStatusID = (int)TandTAllStatus.EnumTourStatus.Cancelled; //4;
    //        }

    //        if (!string.IsNullOrEmpty(txtRemarks.Text))
    //            remarks = txtRemarks.Text;
    //        else
    //            remarks = string.Empty;

    //        int value = objTourAndTravels.UpdateTourStatus(tourID
    //                                                        , tourStatusID
    //                                                        , actID
    //                                                        , remarks
    //                                                        , Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //        if (value > 0)
    //        {
    //            if (isMailSend)
    //            {
    //                TourSendMail tsm = new TourSendMail();
    //                int sendMailValue = tsm.SendMail(tourID);

    //                string mailMessage = "";

    //                if (sendMailValue > 0)
    //                {
    //                    mailMessage = "and mail sent successfully!";
    //                    int val = objTourAndTravels.UpdateTourInfoMailStatus(actID
    //                                                                       , tourID
    //                                                                       , Convert.ToInt32(Session["CHK_ACC"]));
    //                }
    //                else
    //                {
    //                    mailMessage = "successfully, please resend e - mail from Tour Information List!";
    //                }

    //                string approvedMsg = string.Empty;
    //                if (!string.IsNullOrEmpty(sanctionNo))
    //                    approvedMsg = "Tour Information No.: '" + tourNO + "' approved and Tour Sanction No. : '" + sanctionNo + "' generated " + mailMessage;
    //                else
    //                    approvedMsg = "Tour Information No.: '" + tourNO + "' approved " + mailMessage;


    //                if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
    //                        tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //                {
    //                    SuccessMessage(approvedMsg);
    //                }
    //                else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
    //                {
    //                    SuccessMessage("Tour Information No.: '" + tourNO + "' deleted " + mailMessage);
    //                }
    //                else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
    //                {
    //                    SuccessMessage("Tour Information No.: '" + tourNO + "' cancelled " + mailMessage);
    //                }

    //            }
    //            else
    //            {
    //                if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)// && actID == 3)
    //                    SuccessMessage("Tour Information No.: '" + tourNO + "' deleted successfully.");

    //                else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)// && actID == 4)
    //                    SuccessMessage("Tour Information No.: '" + tourNO + "' cancelled successfully.");
    //            }

    //            GetTourList();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //    }
    //}

    private string GenerateAdviceCSV(string beneficiaryName, string tourSanctionNo, string amount, string emailAdd1,
                                        string beneBankAccount, string beneBankIFSCBANKCode, int unitID)
    {
        string csvTxt = string.Empty;

        string transactionType = string.Empty;
        string referenceNumber = string.Empty;
        string drAccountNo = string.Empty;
        string paymentNarration = string.Empty;
        //string beneficiaryName = string.Empty;
        string beneAdd1 = string.Empty;
        string beneAdd2 = string.Empty;
        string beneAdd3 = string.Empty;
        string paymentLocation = string.Empty;
        string chequeNo = string.Empty;
        string valueDate = string.Empty;
        //string amount = string.Empty;
        string printBranchLocation = string.Empty;
        //string emailAdd1 = string.Empty;
        string emailAdd2 = string.Empty;
        string emailAdd3 = string.Empty;
        string freeText = string.Empty;
        string nA1 = string.Empty;
        string nA2 = string.Empty;
        string nA3 = string.Empty;
        string nA4 = string.Empty;
        string nA5 = string.Empty;
        //string beneBankAccount = string.Empty;
        //string beneBankIFSCBANKCode = string.Empty;
        string nA6 = string.Empty;
        string deliverTo = string.Empty;
        string orderingPartyName = string.Empty;
        string orderingPartyAdd1 = string.Empty;
        string orderingPartyAdd2 = string.Empty;
        string orderingPartyAdd3 = string.Empty;
        string orderingPartyAccount = string.Empty;
        string bankName = string.Empty;
        string bankToBankInfo = string.Empty;

        try
        {
            string csv = string.Empty;


            if (Convert.ToDouble(amount) <= 200000)
                transactionType = "NEFT";
            else
                transactionType = "RTGS";

            referenceNumber = "HSBC";

            //A35
            if (unitID == 1)
                drAccountNo = "'499391803001";
            //Delhi
            else if (unitID == 2)
                drAccountNo = "'499391803005";
            //SEZ
            else if (unitID == 3)
                drAccountNo = "'499391803004";
            //GNU       
            else if (unitID == 4)
                drAccountNo = "'499391803001";

            paymentNarration = string.Empty;

            beneAdd1 = string.Empty;
            beneAdd2 = string.Empty;
            beneAdd3 = string.Empty;
            paymentLocation = string.Empty;
            chequeNo = string.Empty;
            valueDate = DateTime.Now.ToString("dd/MM/yyyy");
            printBranchLocation = string.Empty;
            emailAdd2 = string.Empty;
            emailAdd3 = string.Empty;
            freeText = tourSanctionNo;
            nA1 = string.Empty;
            nA2 = string.Empty;
            nA3 = string.Empty;
            nA4 = string.Empty;
            nA5 = string.Empty;
            nA6 = string.Empty;
            deliverTo = string.Empty;
            orderingPartyName = "COPERION IDEAL";
            orderingPartyAdd1 = "PRIVATE LIMITED";
            orderingPartyAdd2 = "A35 SECTOR 64";
            orderingPartyAdd3 = "NOIDA";
            orderingPartyAccount = "201307";
            bankName = string.Empty;
            bankToBankInfo = string.Empty;


            csv += "COPERION IDEAL PRIVATE LIMITED,";
            csv += "\r\n";
            //"Transaction_Type,Reference_Number,Dr_Account_No,Payment_Narration,Beneficiary_Name,Bene_Add_1,Bene_Add_2,Bene_Add_3,Payment_Location,Cheque_No,Value_Date,Amount,Print_Branch_Location,Email_Add_1,Email_Add_2,Email_Add_3,Free_Text,NA,NA,NA,NA,NA,Bene_Bank_Account_#,Bene_Bank_IFSC/BANK_Code,NA,Deliver_To,Ordering_Party_Name,Ordering_Party_Add1,Ordering_Party_Add2,Ordering_Party_Add3,Ordering_party_Account,Bank_Name,Bank_to_Bank_Info,";
            csv += "Transaction Type,Reference Number,Dr Account No,Payment Narration,Beneficiary Name,Bene Add 1,Bene Add 2,Bene Add 3,Payment Location,Cheque No,Value Date,Amount,Print Branch Location,Email Add-1,Email Add-2,Email Add-3,FreeText,NA,NA,NA,NA,NA,Bene Bank Account #,Bene Bank IFSC /  BANK Code,NA,Deliver To,Ordering Party Name,Ordering_Party Add1,Ordering_Party Add2,Ordering_Party Add3,Ordering_party_Account,BANK_NAME,Bank_to_Bank_Info,";
            csv += "\r\n";

            csv += transactionType + "," + referenceNumber + "," + drAccountNo + "," +
                            paymentNarration + "," + beneficiaryName + "," + beneAdd1 + "," + beneAdd2 + "," + beneAdd3 + "," + paymentLocation + "," + chequeNo + "," + valueDate + "," + amount + "," +
                            printBranchLocation + "," + emailAdd1 + "," + emailAdd2 + "," + emailAdd3 + "," + freeText + "," + nA1 + "," + nA2 + "," + nA3 + "," + nA4 + "," + nA5 + "," +
                            beneBankAccount + "," + beneBankIFSCBANKCode + "," + nA6 + "," + deliverTo + "," + orderingPartyName + "," +
                            orderingPartyAdd1 + "," + orderingPartyAdd2 + "," + orderingPartyAdd3 + "," + orderingPartyAccount + "," + bankName + "," + bankToBankInfo + ",";



            if (!string.IsNullOrEmpty(csv))
            {
                csvTxt = csv;
            }
            else
            {
                csvTxt = string.Empty;
            }
        }
        catch (Exception)
        {
            csvTxt = string.Empty;
        }
        return csvTxt;
    }

    //private int SendMailNew(int tourID)
    //{
    //    int sendVal = 0;
    //    try
    //    {
    //        string csvTxt = string.Empty;


    //        int tlEmpRecordID = 0;
    //        string userName = string.Empty;
    //        string password = string.Empty;
    //        string tlName = string.Empty;
    //        string tlEmail = string.Empty;
    //        string fromDate = string.Empty;
    //        string toDate = string.Empty;
    //        string customeName = string.Empty;
    //        string placeOfvisit = string.Empty;
    //        string travelMode = string.Empty;

    //        string jobNo = string.Empty;
    //        int unitID = 0;
    //        int isAdviceGenerated = 0;
    //        int tourBasedOnID = 0;

    //        string purposeOfVisit = string.Empty;
    //        string typeOfTrip = string.Empty;
    //        string modeOfTravel = string.Empty;
    //        string expectedExpenditure = string.Empty;
    //        string expectedExpenditureCurr = string.Empty;
    //        string advanceRequired = string.Empty;
    //        string advanceRequiredCurr = string.Empty;

    //        string createdByAccNo = string.Empty;
    //        string createdByIFSC = string.Empty;

    //        if (!string.IsNullOrEmpty(txtAdvanceAmt.Text))
    //            advanceAmount = Convert.ToDouble(txtAdvanceAmt.Text);
    //        else
    //            advanceAmount = 0;


    //        advanceAmtcurrencyID = Convert.ToInt32(ddlAdvanceCurrency.SelectedValue);
    //        advanceAmtcurrencyCode = Convert.ToString(ddlAdvanceCurrency.Text);



    //        dsUserInfo = objTourAndTravels.GetRequesterInfo(tourID);
    //        if (dsUserInfo.Tables.Count > 0)
    //        {
    //            if (dsUserInfo.Tables[0].Rows.Count > 0)
    //            {
    //                DataRow dr0 = dsUserInfo.Tables[0].Rows[0];

    //                tourNo = Convert.ToString(dr0["TOUR_NO"]);
    //                sanctionNo = Convert.ToString(dr0["TOUR_SANCTION_NO"]);
    //                tourStatus = Convert.ToString(dr0["TOUR_STATUS_NAME"]);
    //                tourStatusID = Convert.ToInt32(dr0["TOUR_STATUS_ID"]);
    //                travelModeID = Convert.ToInt32(dr0["TRAVEL_MODE_ID"]);
    //                tourBasedOnID = Convert.ToInt32(dr0["CUSTOMER_BASED_ON_ID"]);

    //                fromDate = Convert.ToDateTime(dr0["START_DATE"]).ToString("dd-MMM-yyyy");
    //                toDate = Convert.ToDateTime(dr0["END_DATE"]).ToString("dd-MMM-yyyy");
    //                customeName = Convert.ToString(dr0["CUST_VEND_NAME"]);
    //                placeOfvisit = Convert.ToString(dr0["PLACE_OF_VISIT"]);
    //                travelMode = Convert.ToString(dr0["TRAVEL_MODE"]);
    //                jobNo = Convert.ToString(dr0["JOB_NO"]);

    //                purposeOfVisit = Convert.ToString(dr0["VISIT_TYPE"]);
    //                typeOfTrip = Convert.ToString(dr0["TRIP_TYPE"]);
    //                modeOfTravel = Convert.ToString(dr0["TRAVEL_MODE"]);
    //                expectedExpenditure = Convert.ToString(dr0["EXPENDITURE_AMT"]);
    //                expectedExpenditureCurr = Convert.ToString(dr0["EXPENDITURE_CURRENCY"]);
    //                advanceRequired = Convert.ToString(dr0["ADVANCE_AMT"]);
    //                advanceRequiredCurr = Convert.ToString(dr0["CURRENCY"]);

    //                createdBy = Convert.ToString(dr0["CREATED_BY"]);
    //                createdByEmail = Convert.ToString(dr0["CREATED_BY_EMAIL"]);
    //                if (dr0["CREATED_ON"] != DBNull.Value)
    //                    createdOn = Convert.ToDateTime(dr0["CREATED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                createdRemarks = Convert.ToString(dr0["CREATED_REMARKS"]);

    //                if (dr0["CREATED_BY_ACC"] != DBNull.Value)
    //                    createdByAccNo = Convert.ToString(dr0["CREATED_BY_ACC"]);

    //                if (dr0["CREATED_BY_IFSC"] != DBNull.Value)
    //                    createdByIFSC = Convert.ToString(dr0["CREATED_BY_IFSC"]);


    //                hodApprovedBy = Convert.ToString(dr0["APPROVED_BY"]);
    //                if (dr0["APPROVED_ON"] != DBNull.Value)
    //                    hodApprovedOn = Convert.ToDateTime(dr0["APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                hodApprovedByEmail = Convert.ToString(dr0["APPROVED_BY_EMAIL"]);
    //                hodApprovedRemarks = Convert.ToString(dr0["APPROVED_REMARKS"]);

    //                finalApprovedBy = Convert.ToString(dr0["FINAL_APPROVED_BY"]);
    //                if (dr0["FINAL_APPROVED_ON"] != DBNull.Value)
    //                    finalApprovedOn = Convert.ToDateTime(dr0["FINAL_APPROVED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                finalApprovedByEmail = Convert.ToString(dr0["FINAL_APPROVED_BY_EMAIL"]);
    //                finalApprovedRemarks = Convert.ToString(dr0["FINAL_APPROVED_REMARKS"]);


    //                tlEmpRecordID = Convert.ToInt32(dr0["TEAMLEADER_EMP_RECORD_ID"]);
    //                userName = Convert.ToString(dr0["TEAMLEADER_USER_NAME"]);
    //                password = Convert.ToString(dr0["TEAMLEADER_PASSWORD"]);
    //                tlName = Convert.ToString(dr0["TEAMLEADER_NAME"]);
    //                tlEmail = Convert.ToString(dr0["TEAMLEADER_EMAIL"]);


    //                if (dr0["ADVANCE_AMT"] != DBNull.Value)
    //                {
    //                    //advanceAmt = Convert.ToString(dr0["ADVANCE_AMT"]);
    //                    //advanceAmtCurrency = Convert.ToString(dr0["CURRENCY"]);

    //                    advanceAmt = Convert.ToDouble(dr0["ADVANCE_AMT"]);
    //                    advanceAmtcurrencyCode = Convert.ToString(dr0["CURRENCY_CODE"]);
    //                    advanceAmtCurrency = Convert.ToString(dr0["CURRENCY"]);
    //                    advanceAmtcurrencyID = Convert.ToInt32(dr0["ADVANCE_CURRENCY"]);

    //                }

    //                deletedBy = Convert.ToString(dr0["DELETED_BY"]);
    //                if (dr0["DELETED_ON"] != DBNull.Value)
    //                    deletedOn = Convert.ToDateTime(dr0["DELETED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                deletedByEmail = Convert.ToString(dr0["DELETED_BY_EMAIL"]);
    //                deletedRemarks = Convert.ToString(dr0["DELETED_REMARKS"]);

    //                cancelledBy = Convert.ToString(dr0["CANCELLED_BY"]);
    //                if (dr0["CANCELLED_ON"] != DBNull.Value)
    //                    cancelledOn = Convert.ToDateTime(dr0["CANCELLED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
    //                cancelledByEmail = Convert.ToString(dr0["CANCELLED_BY_EMAIL"]);
    //                cancelledRemarks = Convert.ToString(dr0["CANCELLED_REMARKS"]);

    //                unitID = Convert.ToInt32(dr0["UNIT_ID"]);
    //                isAdviceGenerated = Convert.ToInt32(dr0["IS_ADVICE_GENERATED"]);
    //            }

    //            //HR Team
    //            if (dsUserInfo.Tables[1].Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in dsUserInfo.Tables[1].Rows)
    //                {
    //                    hrTeamEmail += ";" + Convert.ToString(dr["HR_EMAIL"]);
    //                }
    //                //hrTeamEmail = hrTeamEmail.TrimStart(',');
    //            }

    //            //Acc Team
    //            if (dsUserInfo.Tables[2].Rows.Count > 0)
    //            {
    //                accTeamName = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["ACC_NAME"]);
    //                accTeamEmail = Convert.ToString(dsUserInfo.Tables[2].Rows[0]["ACC_EMAIL"]);
    //            }

    //            //Senthil And Pawan Sharma
    //            if (dsUserInfo.Tables[3].Rows.Count > 0)
    //            {
    //                //accAcvnaceFCEmail = Convert.ToString(dsUserInfo.Tables[3].Rows[0]["EMAIL_ID"]);
    //                foreach (DataRow dr in dsUserInfo.Tables[3].Rows)
    //                {
    //                    accAcvnaceFCEmail += ";" + Convert.ToString(dr["EMAIL_ID"]);
    //                }
    //                //accAcvnaceFCEmail = accAcvnaceFCEmail.TrimStart(',');
    //            }

    //            if (dsUserInfo.Tables[5].Rows.Count > 0)
    //            {
    //                //For final approval

    //                DataRow dr0 = dsUserInfo.Tables[5].Rows[0];

    //                finalApprovarEmpRecordID = Convert.ToInt32(dr0["EMP_RECORD_ID"]);
    //                finalApprovarUN = Convert.ToString(dr0["UN"]);
    //                finalApprovarPWD = Convert.ToString(dr0["PWD"]);
    //                finalApprovarName = Convert.ToString(dr0["EMPLOYEE_NAME"]);
    //                finalApprovarEmail = Convert.ToString(dr0["EMAIL_ID"]);
    //            }
    //        }
    //        else
    //        {
    //            sendVal = 0;
    //        }


    //        string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);
    //        string approveHref = string.Empty;
    //        string approveLink = string.Empty;

    //        string deleteHref = string.Empty;
    //        string deleteLink = string.Empty;

    //        if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.New)//|| tourStatus == "New"
    //        {
    //            from = createdByEmail;
    //            to = tlEmail;
    //            bcc = createdByEmail;
    //            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/01ReqToHodTourMail.htm";
    //            subject = "Tour No.: '" + tourNo + "' Created On: " + createdOn;


    //            approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=2" + "&tourstatusid=" + tourStatusID + "&ud=" + userName + "&pd=" + password + "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";
    //            approveHref = "<a href=" + approveLink + ">Approve Tour request</a>";

    //            deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=3" + "&tourstatusid=" + tourStatusID + "&ud=" + userName + "&pd=" + password + "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";
    //            deleteHref = "<a href=" + deleteLink + ">Delete Tour request</a>";


    //        }

    //        else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)//|| tourStatus == "Approved"
    //        {

    //            if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
    //            {
    //                from = hodApprovedByEmail;
    //                to = finalApprovarEmail;
    //                ////
    //                if (!(Convert.ToInt32(advanceAmount) > 0))
    //                {
    //                    advanceRequiredCurr = "";
    //                }

    //                /////

    //                    approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=6&tourstatusid=" + tourStatusID + "&ud=" + finalApprovarUN + "&pd=" + finalApprovarPWD + "&emprecordid=" + Convert.ToString(finalApprovarEmpRecordID) + "'";
    //                approveHref = "<a href=" + approveLink + ">Approve Tour request</a>";

    //                deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=3&tourstatusid=" + tourStatusID + "&ud=" + finalApprovarUN + "&pd=" + finalApprovarPWD + "&emprecordid=" + Convert.ToString(finalApprovarEmpRecordID) + "'";
    //                deleteHref = "<a href=" + deleteLink + ">Delete Tour request</a>";


    //                //mail will go sonja
    //                fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/01ReqToFinalHodTourMail.htm";
    //                subject = "Tour No.: '" + tourNo + "' Approved On: " + hodApprovedOn;
    //            }
    //            else
    //            {
    //                if (Convert.ToInt32(advanceAmount) > 0)
    //                {
    //                    from = hodApprovedByEmail;
    //                    bcc = hodApprovedByEmail;
    //                    //68 for INR in DB
    //                    if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
    //                    {
    //                        to = accTeamEmail;
    //                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourMail.htm";
    //                    }
    //                    else
    //                    {
    //                        to = accAcvnaceFCEmail;
    //                        fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourMailForeignTour.htm";
    //                    }
    //                    cc = hrTeamEmail + "," + createdByEmail;
    //                    subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
    //                }
    //                else
    //                {
    //                    from = hodApprovedByEmail;
    //                    to = createdByEmail;
    //                    cc = hrTeamEmail;
    //                    bcc = hodApprovedByEmail;
    //                    ////
    //                    advanceRequiredCurr = ""; 
    //                    /////

    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";
    //                    subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
    //                }
    //            }

    //        }


    //        else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //        {
    //            if (Convert.ToInt32(advanceAmt) > 0)
    //            {
    //                from = finalApprovedByEmail;
    //                bcc = finalApprovedByEmail;

    //                //68 for INR in DB
    //                if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
    //                {
    //                    to = accTeamEmail;
    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToAccTourFinalMail.htm";
    //                }
    //                else
    //                {
    //                    to = accAcvnaceFCEmail;
    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/03HODToSenthilTourFinalMailForeignTour.htm";
    //                }
    //                cc = hodApprovedByEmail + ";" + hrTeamEmail + ";" + createdByEmail;
    //                subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;
    //            }
    //            else
    //            {
    //                //from = finalApprovedByEmail;
    //                //to = createdByEmail;
    //                //cc = hodApprovedByEmail + ";" + hrTeamEmail;
    //                //bcc = finalApprovedByEmail;

    //                //fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";
    //                //subject = "Tour Sanction No. " + sanctionNo + " generated on: " + hodApprovedOn;


    //                from = finalApprovedByEmail;
    //                to = createdByEmail;
    //                cc = hodApprovedByEmail + ";" + hrTeamEmail;
    //                bcc = finalApprovedByEmail;
    //                subject = "Tour Sanction No. " + sanctionNo + " generated on: " + finalApprovedOn;

    //                if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
    //                {


    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourFinalMail.htm";

    //                }
    //                else
    //                {

    //                    fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/02HODToReqTourMail.htm";


    //                }



    //            }

    //        }


    //        else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)//|| tourStatus == "Deleted"
    //        {
    //            from = deletedByEmail;
    //            to = createdByEmail;
    //            bcc = deletedByEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/05DeleteTourInfoMail.htm";
    //            subject = "Tour Information No. " + tourNo + " Deleted On: " + deletedOn;
    //        }

    //        else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)//|| tourStatus == "Cancelled"
    //        {
    //            from = cancelledByEmail;
    //            to = hodApprovedByEmail;
    //            cc = hrTeamEmail + ";" + accTeamEmail;
    //            bcc = cancelledByEmail;
    //            if (advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR)
    //                cc = hrTeamEmail + ";" + accTeamEmail;
    //            else
    //                cc = hrTeamEmail + ";" + accAcvnaceFCEmail;

    //            fileName = "~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/04CancelTourInfoMail.htm";
    //            subject = "Tour Information No. " + tourNo + " Cancelled On: " + cancelledOn;
    //        }



    //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //        SmtpClient SmtpServer = new SmtpClient();
    //        mail.From = new MailAddress(from);


    //        //2024-02-14
    //        if ((travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness) || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer))
    //        {
    //            if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //            {
    //                AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
    //            }
    //        }
    //        else
    //        {
    //            if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
    //            {
    //                AttachAdviceToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
    //            }
    //        }


    //        ////////
    //        //if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
    //        //    tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved)
    //        //{
    //        //    if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved && 
    //        //        (travelModeID != (int)TandTAllStatus.EnumTravelMode.AirBusiness && (tourBasedOnID != (int)TandTAllStatus.EnumTourBasedOn.NonCustomer)))
    //        //    {
    //        //        AttachAdvideToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
    //        //    }
    //        //    else if (tourStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved && 
    //        //        (travelModeID == (int)TandTAllStatus.EnumTravelMode.AirBusiness || (tourBasedOnID == (int)TandTAllStatus.EnumTourBasedOn.NonCustomer)))
    //        //    {
    //        //        AttachAdvideToMail(isAdviceGenerated, createdByIFSC, createdByAccNo, unitID, mail);
    //        //    }
    //        //}

    //        //if (tourStatusID == 2 || tourStatus == "Approved")
    //        //{
    //        //    if (Convert.ToInt32(advanceAmount) > 0 && advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
    //        //        isAdviceGenerated == 0 &&
    //        //        !string.IsNullOrEmpty(Convert.ToString(createdByAccNo)) &&
    //        //        !string.IsNullOrEmpty(Convert.ToString(createdByIFSC)))
    //        //    {
    //        //        csvTxt = GenerateAdviceCSV(createdBy, sanctionNo, Convert.ToString(advanceAmount), createdByEmail, createdByAccNo, createdByIFSC, unitID);
    //        //        MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(csvTxt));
    //        //        Attachment attachment = new Attachment(stream, new ContentType("text/csv"));

    //        //        string[] strTxt = sanctionNo.Split('/');
    //        //        string sanctionNoTxt = string.Empty;
    //        //        foreach (string item in strTxt)
    //        //        {
    //        //            sanctionNoTxt += item + "_";
    //        //        }
    //        //        if (!string.IsNullOrEmpty(sanctionNoTxt))
    //        //            attachment.Name = "Payment_Advice_" + sanctionNoTxt.TrimEnd('_') + ".CSV";
    //        //        else
    //        //            attachment.Name = "Payment_Advice.CSV";


    //        //        mail.Attachments.Add(attachment);
    //        //    }
    //        //}

    //        if (!string.IsNullOrEmpty(to))
    //        {
    //            string[] strTo = to.Split(';');
    //            foreach (string item in strTo)
    //            {
    //                if (!string.IsNullOrEmpty(item))
    //                    mail.To.Add(item);
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(cc))
    //        {
    //            string[] strCC = cc.Split(';');
    //            foreach (string item in strCC)
    //            {
    //                if (!string.IsNullOrEmpty(item))
    //                    mail.CC.Add(item);
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(bcc))
    //        {
    //            string[] strBCC = bcc.Split(';');
    //            foreach (string item in strBCC)
    //            {
    //                if (!string.IsNullOrEmpty(item))
    //                    mail.Bcc.Add(item);
    //            }
    //        }

    //        mail.Subject = subject;
    //        mail.IsBodyHtml = true;

    //        string body = string.Empty;
    //        using (StreamReader reader = new StreamReader(Server.MapPath(fileName)))
    //        {
    //            body = reader.ReadToEnd();
    //        }

    //        body = body.Replace("{#tourno#}", tourNo);
    //        body = body.Replace("{#sanctionno#}", sanctionNo);

    //        body = body.Replace("{#createdby#}", createdBy);
    //        body = body.Replace("{#createdon#}", createdOn);
    //        body = body.Replace("{#requesterremarks#}", createdRemarks);

    //        body = body.Replace("{#fromdate#}", fromDate);
    //        body = body.Replace("{#todate#}", toDate);
    //        body = body.Replace("{#customername#}", customeName);
    //        body = body.Replace("{#place#}", placeOfvisit);
    //        body = body.Replace("{#jobno#}", jobNo);

    //        body = body.Replace("{#purposeofvisit#}", purposeOfVisit);
    //        body = body.Replace("{#typeoftrip#}", typeOfTrip);
    //        body = body.Replace("{#modeOfTravel#}", modeOfTravel);
    //        body = body.Replace("{#expectedexpenditure#}", expectedExpenditure);
    //        body = body.Replace("{#expectedexpenditurecurr#}", expectedExpenditureCurr);
    //        body = body.Replace("{#advancerequired#}", advanceRequired);
    //        body = body.Replace("{#advancerequiredcurr#}", advanceRequiredCurr);

    //        body = body.Replace("{#approvedby#}", hodApprovedBy);
    //        body = body.Replace("{#approvedon#}", hodApprovedOn);
    //        body = body.Replace("{#approvedremarks#}", hodApprovedRemarks);

    //        body = body.Replace("{#finalapprovedby#}", finalApprovedBy);
    //        body = body.Replace("{#finalapprovedon#}", finalApprovedOn);
    //        body = body.Replace("{#finalapprovedremarks#}", finalApprovedRemarks);

    //        body = body.Replace("{#accname#}", accTeamName);
    //        body = body.Replace("{#advanceamount#}", Convert.ToString(advanceAmt));
    //        body = body.Replace("{#advancecurrency#}", advanceAmtCurrency);

    //        body = body.Replace("{#deletedby#}", deletedBy);
    //        body = body.Replace("{#deletedon#}", deletedOn);
    //        body = body.Replace("{#deletedremark#}", deletedRemarks);

    //        body = body.Replace("{#cancelledby#}", cancelledBy);
    //        body = body.Replace("{#cancelledon#}", cancelledOn);
    //        body = body.Replace("{#cancelledremark#}", cancelledRemarks);




    //        body = body.Replace("{#approvelink#}", approveHref);
    //        body = body.Replace("{#deletelink#}", deleteHref);

    //        mail.Body = body;

    //        SmtpServer.Host = "eusmtp.hi.corp";
    //        SmtpServer.Port = 25;
    //        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

    //        try
    //        {
    //            SmtpServer.Send(mail);
    //            sendVal = 1;
    //        }
    //        catch (Exception ex)
    //        {
    //            string exMsg = ex.ToString();
    //            if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
    //                sendVal = 1;

    //            else
    //                sendVal = 0;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        sendVal = 0;
    //    }
    //    return sendVal;
    //}

    //private void AttachAdviceToMail(int isAdviceGenerated
    //                              , string createdByIFSC
    //                              , string createdByAccNo
    //                              , int unitID
    //                              , System.Net.Mail.MailMessage mail)
    //{
    //    string csvTxt = string.Empty;

    //    if (Convert.ToInt32(advanceAmt) > 0 && advanceAmtcurrencyID == (int)TandTAllStatus.EnumOthers.CurrencyINR &&
    //                isAdviceGenerated == 0 && !string.IsNullOrEmpty(Convert.ToString(createdByAccNo)) &&
    //                !string.IsNullOrEmpty(Convert.ToString(createdByIFSC))
    //               )
    //    {
    //        csvTxt = GenerateAdviceCSV(createdBy, sanctionNo, Convert.ToString(advanceAmt), createdByEmail, createdByAccNo, createdByIFSC, unitID);
    //        MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(csvTxt));
    //        Attachment attachment = new Attachment(stream, new ContentType("text/csv"));


    //        string[] strTxt = sanctionNo.Split('/');
    //        string sanctionNoTxt = string.Empty;
    //        foreach (string item in strTxt)
    //        {
    //            sanctionNoTxt += item + "_";
    //        }
    //        if (!string.IsNullOrEmpty(sanctionNoTxt))
    //            attachment.Name = "Payment_Advice_" + sanctionNoTxt.TrimEnd('_') + ".CSV";
    //        else
    //            attachment.Name = "Payment_Advice.CSV";

    //        mail.Attachments.Add(attachment);
    //    }
    //}

    private void UpdateTourStatus()
    {
        try
        {
            int actID = Convert.ToInt32(ViewState["ACT_ID"]);
            int tourID = Convert.ToInt32(ViewState["TOUR_ID"]);
            string tourNO = Convert.ToString(ViewState["TOUR_NO"]);
            int empRecordID = Convert.ToInt32(ViewState["EMP_RECORD_ID"]);
            int tourStatusID = Convert.ToInt32(ViewState["TOUR_STATUS_ID"]);
            int travelModeID = Convert.ToInt32(ViewState["TRAVEL_MODE_ID"]);
            int tourBasedOnID = Convert.ToInt32(ViewState["TOUR_BASED_ON_ID"]);
            int currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int teamLeaderID = Convert.ToInt32(ViewState["TEAM_LEADER_ID"]);   //Session["TEAMLEADER_ID"]

            string remarks = string.Empty;
            if (!string.IsNullOrEmpty(txtRemarks.Text)) remarks = txtRemarks.Text;
            

            int insertVal = 0;
            int sentMailVal = 0;
            int retStatusID = 0;
            string tourSanctionNumber = "";

            TourAndTravelStatusUpdate objUT = new TourAndTravelStatusUpdate();
            string returnVal = objUT.UpdateTourStatus(actID,
                                                      tourID, 
                                                      //tourNO, 
                                                      empRecordID, 
                                                      tourStatusID, 
                                                      travelModeID,
                                                      tourBasedOnID,
                                                      remarks, 
                                                      teamLeaderID, 
                                                      currentUserID, 
                                                      0);
            insertVal = Convert.ToInt32(returnVal.Split(':')[0]);
            sentMailVal = Convert.ToInt32(returnVal.Split(':')[1]);

            if (returnVal.Length > 50)
            {
                ExceptionMessage(returnVal);
                return;
            }
            else
            {
                insertVal = Convert.ToInt32(returnVal.Split(':')[0]);
                sentMailVal = Convert.ToInt32(returnVal.Split(':')[1]);
                retStatusID = Convert.ToInt32(returnVal.Split(':')[2]);
                tourSanctionNumber = Convert.ToString(returnVal.Split(':')[3]);
            }

            string mailMessage = "";
            if (sentMailVal > 0)
            {
                mailMessage = "and mail sent successfully!";
            }
            else
            {
                mailMessage = "successfully, please resend e - mail from Tour Information List!";
            }

            string approvedMsg = string.Empty;
            if (!string.IsNullOrEmpty(tourSanctionNumber))
                approvedMsg = "Tour Information No.: '" + tourNO + "' approved and Tour Sanction No. : '" + tourSanctionNumber + "' generated " + mailMessage;
            else
                approvedMsg = "Tour Information No.: '" + tourNO + "' approved " + mailMessage;

            if (insertVal > 0)
            {
                if (sentMailVal > 0)
                {
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved ||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved||
                            retStatusID == (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer)
                    {
                        SuccessMessage(approvedMsg);
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    {
                        if (currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Tour Information No.: '" + tourNO + "' rejected " + mailMessage);
                        }
                        else
                        {
                            SuccessMessage("Tour Information No.: '" + tourNO + "' deleted " + mailMessage);
                        }

                           // SuccessMessage("Tour Information No.: '" + tourNO + "' deleted " + mailMessage);
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                    {
                        SuccessMessage("Tour Information No.: '" + tourNO + "' cancelled " + mailMessage);
                    }
                }
                else
                {
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved ||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.MgmtHODApproved ||
                        retStatusID == (int)TandTAllStatus.EnumTourStatus.FinalApproved||
                         retStatusID == (int)TandTAllStatus.EnumTourStatus.UKVApproveByParzer)
                    {
                        SuccessMessage(approvedMsg + "successfully, please resend e-mail from Tour Information List!");
                    }
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    {
                        SuccessMessage("Tour Information No.: '" + tourNO + "' deleted successfully.");
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                    {
                        SuccessMessage("Tour Information No.: '" + tourNO + "' cancelled successfully.");
                    }
                }
                GetTourList();
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
        }
    }

    private void ToCSVNew01(string csv, int tourID, string tourSanctionNo)
    {

        try
        {
            string fileName = string.Empty;

            string[] strTxt = tourSanctionNo.Split('/');
            string sanctionNoTxt = string.Empty;
            foreach (string item in strTxt)
            {
                sanctionNoTxt += item + "_";
            }
            if (!string.IsNullOrEmpty(sanctionNoTxt))
                fileName = "Payment_Advice_" + sanctionNoTxt.TrimEnd('_');
            else
                fileName = "Payment_Advice";

            HttpResponse response = HttpContext.Current.Response;

            response.Clear();
            response.Buffer = true;
            response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            response.Charset = "";
            response.ContentType = "application/text";
            response.Output.Write(csv);
            objTourAndTravels.UpdateTourInfoMailStatus((int)TandTAllStatus.EnumTourAct.ApprovedAndUpdateAdviceGenerated
                                                      , tourID
                                                      , Convert.ToInt32(Session["CHK_ACC"]));

            response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            response.End();
        }
        catch (Exception)
        {
            //
        }
    }

    private void DisableControls()
    {
        txtStartDate.Enabled = false;
        imgbtnStartDate.Enabled = false;
        txtEndDate.Enabled = false;
        imgbtnEndDate.Enabled = false;
        txtCustVendName.Enabled = false;
        txtPlaceOfVisit.Enabled = false;
        ddlCountryOfVisit.Enabled = false;
        ddlTourBasedOn.Enabled = false;
        ddlTourInitiative.Enabled = false;
        ddlPurposeOfVisit.Enabled = false;
        txtJobInqNo.Enabled = false;
        ddlBusinessSegment.Enabled = false;
        ddlModeOfTravel.Enabled = false;
        ddlTypeOfTrip.Enabled = false;
        ddlLocalTravelling.Enabled = false;
        txtExpenditureAmt.Enabled = false;
        ddlExpenditureCurrency.Enabled = false;
        txtAdvanceAmt.Enabled = false;
        ddlAdvanceCurrency.Enabled = false;
    }

    private void EnableControls()
    {
        txtStartDate.Enabled = true;
        imgbtnStartDate.Enabled = true;
        txtEndDate.Enabled = true;
        imgbtnEndDate.Enabled = true;
        txtCustVendName.Enabled = true;
        txtPlaceOfVisit.Enabled = true;
        ddlCountryOfVisit.Enabled = true;
        ddlTourBasedOn.Enabled = true;
        ddlTourInitiative.Enabled = true;
        ddlPurposeOfVisit.Enabled = true;
        txtJobInqNo.Enabled = true;
        ddlBusinessSegment.Enabled = true;
        ddlModeOfTravel.Enabled = true;
        ddlTypeOfTrip.Enabled = true;
        ddlLocalTravelling.Enabled = true;
        txtExpenditureAmt.Enabled = true;
        ddlExpenditureCurrency.Enabled = true;
        txtAdvanceAmt.Enabled = true;
        ddlAdvanceCurrency.Enabled = true;
    }

    private void ViewAttachedFilesNew01(int empRecordID)
    {
        try
        {
            byte[] bytes = null;
            DataTable dsTourInfo = (DataTable)Session["TOUR_LIST"];
            string fileName = string.Empty;
            string extn = string.Empty;
            foreach (DataRow dr in dsTourInfo.Select("EMP_RECORD_ID='" + empRecordID + "'"))
            {
                fileName = Convert.ToString(dr["PASSPORT_COPY_NAME"]);
                bytes = (byte[])dr["PASSPORT_COPY_DOC"];
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