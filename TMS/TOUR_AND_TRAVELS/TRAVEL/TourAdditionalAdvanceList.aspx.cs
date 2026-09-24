using System;
using System.Collections.Generic;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TOUR_AND_TRAVELS_TRAVEL_TourAdditionalAdvanceList : System.Web.UI.Page
{


    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataTable dtTeamMembers = new DataTable();
    DataSet dsTourStatus = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsTourList = new DataSet();
    DataSet dsCurrency = new DataSet();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string tourNoSearch = string.Empty;
    string sanctionNoSearch = string.Empty;
    int tourStatusIDSearch = 0;
    int empRecordID = 0;
    int teamMemberID = 0;
    string teamMembers = string.Empty;
    string custVendName = string.Empty;
    string placeOfVisit = string.Empty;
    int expectedExpenditureCurrencyID = 0;
    double expectedExpenditure = 0;
    int advanceRequiredCurrencyID = 0;
    double advanceRequired = 0;
    string remarks = string.Empty;
    string sanctionNo = string.Empty;

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
                BindCurrency();

                //BindVisitType();
                //BindSegmentType();
                //BindTravelMode();
                //BindTripType();
                //BindLocalTravelType();
                //BindCurrency();
                //BindCountry();
                //BindTourBasedOn();
                //BindTourInitiative();

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["requestid"])))
                    txtTourNo.Text = Convert.ToString(Request.QueryString["tourno"]);
                else
                    txtTourNo.Text = string.Empty;

                GetAddAdvanceRequestList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindTourStatus()
    {
        try
        {

            dsTourStatus = objTourAndTravels.GetAddAdvanceRequestStatus();
            if (dsTourStatus.Tables.Count > 0 && dsTourStatus.Tables[0].Rows.Count > 0)
            {
                ddlTourStatus.DataSource = dsTourStatus.Tables[0];
                ddlTourStatus.DataTextField = "ADD_ADVANCE_STATUS_NAME";
                ddlTourStatus.DataValueField = "ADD_ADVANCE_STATUS_ID";
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


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetAddAdvanceRequestList();
    }


    private void GetAddAdvanceRequestList()
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

            dsTourList = objTourAndTravels.GetAdditionalAdvList(startDate, endDate, tourNoSearch, sanctionNoSearch, tourStatusIDSearch,
                                                        empRecordID, teamMemberID, teamMembers);
            if (dsTourList.Tables.Count > 0 && dsTourList.Tables[0].Rows.Count > 0)
            {
                Session["ADDITIONAL_ADVANCE_REQUEST_LIST"] = dsTourList.Tables[0];
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


    protected void gvTourList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                int requestID = 0;

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

                
                Label lblTourNO = gvTourList.Rows[rowindex].FindControl("lblTourNO") as Label;
                Label lblTourSanctionNo = gvTourList.Rows[rowindex].FindControl("lblTourSanctionNo") as Label;
                Label lblRequestID = gvTourList.Rows[rowindex].FindControl("lblRequestID") as Label;

                Label lblRequestNumber = gvTourList.Rows[rowindex].FindControl("lblRequestNo") as Label;

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

                
                Label lblAdvanceAmt = gvTourList.Rows[rowindex].FindControl("lblAdvanceTaken") as Label;
                Label lblAdvanceAmtCurrencyID = gvTourList.Rows[rowindex].FindControl("lblAdvanceTakenCurrId") as Label;


                Label lblAddAdvReq = gvTourList.Rows[rowindex].FindControl("lblAdditionalRequestedAmt") as Label;
                Label lblAddAdvReqCurrencyID = gvTourList.Rows[rowindex].FindControl("lblAdditionalRequestedAmtCurrencyId") as Label;
                Label lblRemarks = gvTourList.Rows[rowindex].FindControl("lblRemarks") as Label;

                Label lblRequestRemarks = gvTourList.Rows[rowindex].FindControl("lblRemarks") as Label;

                Label lblRequestStatusID = gvTourList.Rows[rowindex].FindControl("lblRequestStatusID") as Label;
                //Label lblTourStatus = gvTourList.Rows[rowindex].FindControl("lblTourStatus") as Label;
                //Label lblIsAdviceGenerated = gvTourList.Rows[rowindex].FindControl("lblIsAdviceGenerated") as Label;

                Label lblCretedByAccNo = gvTourList.Rows[rowindex].FindControl("lblCretedByAccNo") as Label;
                Label lblCretedByIFSC = gvTourList.Rows[rowindex].FindControl("lblCretedByIFSC") as Label;

                ImageButton imgBtnGenerateAdvice = gvTourList.Rows[rowindex].FindControl("imgBtnGenerateAdvice") as ImageButton;

               
                ViewState["TOUR_NO"] = Convert.ToString(lblTourNO.Text);
                ViewState["EMP_RECORD_ID"] = Convert.ToInt32(lblEmpRecordID.Text);
                ViewState["TEAM_LEADER_ID"] = Convert.ToInt32(lblTeamLeaderID.Text);
                ViewState["REQUEST_ID"] = Convert.ToInt32(lblRequestID.Text);
                ViewState["REQUEST_NUMBER"] = Convert.ToString(lblRequestNumber.Text);
                ViewState["REQUEST_STATUS_ID"] = Convert.ToInt32(lblRequestStatusID.Text);

                lblLegend.Text = "Additional Advance Request for Tour No. [" + lblTourNO.Text + "]";
                BindEmployee();
                ddlEmployee.SelectedValue = Convert.ToString(lblEmpRecordID.Text);
                txtEmployeeID.Text = Convert.ToString(lblEmployeeID.Text);
                txtDesignation.Text = Convert.ToString(lblDesignation.Text);
                txtTourSanctionNo.Text = Convert.ToString(lblTourSanctionNo.Text);
                txtStartDate.Text = Convert.ToString(lblStartDate.Text);
                txtEndDate.Text = Convert.ToString(lblEndDate.Text);
                txtCustVendName.Text = Convert.ToString(lblCustVendName.Text);
                txtPlaceOfVisit.Text = Convert.ToString(lblPlaceOfVisit.Text);
                txtRemarks.Text = Convert.ToString(lblRemarks.Text);
                txtRequestRemarks.Text = Convert.ToString(lblRequestRemarks.Text);

                //////////////




                txtExpenditureAmt.Text = Convert.ToString(lblAdvanceAmt.Text);
                ddlExpenditureCurrency.SelectedValue = Convert.ToString(lblAdvanceAmtCurrencyID.Text);
                txtAdvanceAmt.Text = Convert.ToString(lblAddAdvReq.Text);
                ddlAdvanceCurrency.SelectedValue = Convert.ToString(lblAddAdvReqCurrencyID.Text);

                txtRemarks.Text = string.Empty;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (Convert.ToInt32(lblRequestStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.New)//Convert.ToString(lblTourStatus.Text) == "New" || 
                    {
                        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.New;//1
                        ViewState["REQUEST_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.New;

                        txtRemarks.Text = Convert.ToString(lblRemarks.Text);
                        //EnableControls();
                        btnSubmit.Text = "Update Additional Advance Request";
                        this.ModalPopupExtender1.Show();
                    }
                }
                else if (Convert.ToString(e.CommandArgument) == "STATUS" ||
                         Convert.ToString(e.CommandArgument) == "APPROVE")
                {
                    if (Convert.ToInt32(lblRequestStatusID.Text) == (int)TandTAllStatus.EnumTourAdditionalAdvance.New)
                    {
                        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.HODApprove; ;
                        ViewState["REQUEST_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.HODApproved;

                    }

                    btnSubmit.Text = "Approve Additional Advance Request";


                    DisableControls();
                    this.ModalPopupExtender1.Show();
                }
                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    if (Convert.ToInt32(lblRequestStatusID.Text) == (int)TandTAllStatus.EnumTourStatus.New)
                    {
                        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.Delete; //3;
                        ViewState["REQUEST_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.Deleted;


                        btnSubmit.Text = "Delete Request Information";
                    }
                    //else
                    //{
                    //    ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourAct.Cancel; //4;
                    //    ViewState["REQUEST_STATUS_ID"] = (int)TandTAllStatus.EnumTourStatus.Cancelled;

                    //    btnSubmit.Text = "Cancel Tour Information";
                    //}

                    DisableControls();
                    this.ModalPopupExtender1.Show();
                }
                else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    ModalPopupExtender4.Show();
                    iframeViewTourInformationInPDF.Attributes.Add("src", "AddAdvanceRequestInfoPDF.aspx?requestid=" + Convert.ToString(lblRequestID.Text) + "&tourno=" + Convert.ToString(lblTourNO.Text));
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

    private void DisableControls()
    {
        txtStartDate.Enabled = false;
        imgbtnStartDate.Enabled = false;
        txtEndDate.Enabled = false;
        imgbtnEndDate.Enabled = false;
        txtCustVendName.Enabled = false;
        txtPlaceOfVisit.Enabled = false;
      
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
       
        txtExpenditureAmt.Enabled = true;
        ddlExpenditureCurrency.Enabled = true;
        txtAdvanceAmt.Enabled = true;
        ddlAdvanceCurrency.Enabled = true;
    }


    protected void gvTourList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                Label lblTourNo = (Label)e.Row.FindControl("lblTourNO");
               
                Label lblRequestID = (Label)e.Row.FindControl("lblRequestID");
                Label lblTourSanctionNo = (Label)e.Row.FindControl("lblTourSanctionNo");
                Label lblEmpRecordID = (Label)e.Row.FindControl("lblEmpRecordID");
                Label lblEmployeeName = (Label)e.Row.FindControl("lblEmployeeName");
                Label lblEmployeeID = (Label)e.Row.FindControl("lblEmployeeID");
                Label lblEmpEmailID = (Label)e.Row.FindControl("lblEmpEmailID");
                Label lblUnitID = (Label)e.Row.FindControl("lblUnitID");
                Label lblDesignation = (Label)e.Row.FindControl("lblDesignation");
                Label lblStartDate = (Label)e.Row.FindControl("lblStartDate");
                Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
                Label lblCustVendName = (Label)e.Row.FindControl("lblCustVendName");
                Label lblPlaceOfVisit = (Label)e.Row.FindControl("lblPlaceOfVisit");
                Label lblAdvanceTaken = (Label)e.Row.FindControl("lblAdvanceTaken");
                Label lblAdvanceTakenCurr = (Label)e.Row.FindControl("lblAdvanceTakenCurr");
                Label lblAdvanceTakenCurrId = (Label)e.Row.FindControl("lblAdvanceTakenCurrId");

                Label lblAdditionalRequestedAmt = (Label)e.Row.FindControl("lblAdditionalRequestedAmt");
                Label lblAdditionalRequestedAmtCurrency = (Label)e.Row.FindControl("lblAdditionalRequestedAmtCurrency");
                Label lblAdditionalRequestedAmtCurrencyId = (Label)e.Row.FindControl("lblAdditionalRequestedAmtCurrencyId");

                Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");

                Label lblRequestStatusID = (Label)e.Row.FindControl("lblRequestStatusID");
                Label lblRequestStatusName = (Label)e.Row.FindControl("lblRequestStatusName");
                

                Label lblTeamLeaderID = (Label)e.Row.FindControl("lblTeamLeaderID");

                Label lblIsMailSentToHOD = (Label)e.Row.FindControl("lblIsMailSentToHOD");

                Label lblIsMailSentToAccounts = (Label)e.Row.FindControl("lblIsMailSentToHOD");

                Label lblIsCancelledMailSent = (Label)e.Row.FindControl("lblIsMailSentToHOD");


                Label lblCretedByAccNo = (Label)e.Row.FindControl("lblCretedByAccNo");
                Label lblCretedByIFSC = (Label)e.Row.FindControl("lblCretedByIFSC");

                Label lblAdvanceAmt = (Label)e.Row.FindControl("lblAdvanceAmt");
                Label lblAdvanceCurrencyID = (Label)e.Row.FindControl("lblAdvanceCurrencyID");
                Label lblTraveModeID = (Label)e.Row.FindControl("lblTraveModeID");
                Label lblTourBasedOnID = (Label)e.Row.FindControl("lblTourBasedOnID");
                Label lblMgmtHODEmpRecordID = (Label)e.Row.FindControl("lblMgmtHODEmpRecordID");
                Label lblFinalHODEmpRecordID = (Label)e.Row.FindControl("lblFinalHODEmpRecordID");


              

                ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");
                

                ImageButton imgBtnGenerateAdvice = (ImageButton)e.Row.FindControl("imgBtnGenerateAdvice");
                ImageButton imgBtnSendMail = (ImageButton)e.Row.FindControl("imgBtnSendMail");


                if (!string.IsNullOrEmpty(Convert.ToString(lblCretedByAccNo.Text)) &&
                    !string.IsNullOrEmpty(Convert.ToString(lblCretedByIFSC.Text)))
                    ViewState["CHK_ACC"] = 1;
                else
                    ViewState["CHK_ACC"] = 0;




                string status = lblRequestStatusName.Text;
                int statusID = Convert.ToInt32(lblRequestStatusID.Text);

                Button btnCancel = (Button)e.Row.FindControl("btnCancel");
                Button btnApprove = (Button)e.Row.FindControl("btnApprove");

                imgProperties.Visible = false;
                btnCancel.Visible = false;
                btnApprove.Visible = false;
                imgStatus.Enabled = false;
                //imgBtnGenerateAdvice.Visible = false;
                //imgBtnSendMail.Visible = false;

                
                int empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                if (statusID == (int)TandTAllStatus.EnumTourAdditionalAdvance.New)//status == "New" && 
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
                        btnCancel.Visible = true;
                        //btnCancel.Text = "Delete";
                        //btnCancel.ToolTip = "Delete Request-: " + lblRequestID.Text;

                        if(Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID)
                        {
                            btnCancel.Text = "Reject";
                            btnCancel.ToolTip = "Reject Request-: " + lblRequestID.Text;
                        }
                        else if(Convert.ToInt32(lblEmpRecordID.Text) == empRecordID)
                        {
                            btnCancel.Text = "Delete";
                            btnCancel.ToolTip = "Delete Request-: " + lblRequestID.Text;
                        }
                        else
                        {
                            btnCancel.Text = "Delete";
                            btnCancel.ToolTip = "Delete Request-: " + lblRequestID.Text;
                        }


                    }

                    if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                        empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                    {
                        imgStatus.Enabled = true;
                        btnApprove.Visible = true;
                        btnApprove.ToolTip = "Approve Request-: " + lblRequestID.Text;
                    }


                    //if (Convert.ToInt32(lblEmpRecordID.Text) == empRecordID &&
                    //    Convert.ToInt32(lblIsApprovalMailSent.Text) == 0)
                    //{
                    //    imgBtnSendMail.Visible = true;
                    //    imgBtnSendMail.ToolTip = "Send Approval Mail";
                    //}
                }



                else if (statusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                    imgStatus.ToolTip = "HOD Approved";
                    imgProperties.Visible = false;
                    imgStatus.Enabled = false;

                    if (Convert.ToInt32(lblTeamLeaderID.Text) == empRecordID ||
                        empRecordID == (int)TandTAllStatus.EnumOthers.AdminPerson)
                    {
                        //btnCancel.Visible = true;
                        btnCancel.Text = "Cancel";
                        btnCancel.ToolTip = "Cancel Request-: " + lblRequestID.Text;
                    }




                }


                else if (statusID == (int)TandTAllStatus.EnumTourStatus.Deleted)//status == "Deleted" && 
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Deleted02.png";
                    imgStatus.ToolTip = "Deleted";
                    imgStatus.Enabled = false;
                    imgProperties.Visible = false;

                    
                }

                else if (statusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)//status == "Cancelled" && 
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Cancelled03.png";
                    imgStatus.ToolTip = "Cancelled";
                    imgStatus.Enabled = false;
                    imgProperties.Visible = false;

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




    private void UpdateTourInformation(string requestId)
    {
        try
        {
            int tourID = Convert.ToInt32(ViewState["TOUR_ID"]);
            string tourNo = Convert.ToString(ViewState["TOUR_NO"]);
            int empRecordID = 0;
            int requestID = 0;
            requestID = Convert.ToInt32(requestId);

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


            if (!string.IsNullOrEmpty(txtTourSanctionNo.Text))
                sanctionNo = txtTourSanctionNo.Text;
            else
                sanctionNo = string.Empty;



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




            int value = objTourAndTravels.InsertAddAdvanceInformation(1,sanctionNo, tourNo, custVendName, placeOfVisit,
                                                               startDate, endDate,
                                                             expectedExpenditure, expectedExpenditureCurrencyID,
                                                            advanceRequired, advanceRequiredCurrencyID, remarks,
                                                            Convert.ToInt32(Session["EMP_RECORD_ID"])
                                                            );

            AdditionalAdvanceSendMail tsm = new AdditionalAdvanceSendMail();
            string sendMailValue = tsm.SendMail(requestID);

            if (value > 0)
            {
                if(!string.IsNullOrEmpty(sendMailValue))
                {

                    SuccessMessage("Additional Advance Request for '" + tourNo + "' updated & mail sent successfully");
                    GetAddAdvanceRequestList();

                }
                else
                {

                    SuccessMessage("Additional Advance Request for '" + tourNo + "' updated successfully");
                    GetAddAdvanceRequestList();

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)TandTAllStatus.EnumTourAct.New)
        //    UpdateTourInformation();

        if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)TandTAllStatus.EnumTourAct.New)
            UpdateTourInformation(ViewState["REQUEST_ID"].ToString());
        else
        {
            UpdateAdditionalAdvanceRequestStatus();
        }
        // UpdateTourStatus();

    }

    private void UpdateAdditionalAdvanceRequestStatus()
    {
        try
        {
            int actID = Convert.ToInt32(ViewState["ACT_ID"]);
            int requestID = Convert.ToInt32(ViewState["REQUEST_ID"]);
            string reqNO = Convert.ToString(ViewState["REQUEST_NUMBER"]);
            string tourNO = Convert.ToString(ViewState["TOUR_NO"]);
            int empRecordID = Convert.ToInt32(ViewState["EMP_RECORD_ID"]);
           // int tourStatusID = Convert.ToInt32(ViewState["TOUR_STATUS_ID"]);
            
            int currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int teamLeaderID = Convert.ToInt32(ViewState["TEAM_LEADER_ID"]);   //Session["TEAMLEADER_ID"]
            
            int requestStatusID = Convert.ToInt32(ViewState["REQUEST_STATUS_ID"]);

            string remarks = string.Empty;
            if (!string.IsNullOrEmpty(txtRemarks.Text)) remarks = txtRemarks.Text;


            int insertVal = 0;
            int sentMailVal = 0;
            int retStatusID = 0;
            string tourSanctionNumber = "";

            AddAdvStatusUpdate objUT = new AddAdvStatusUpdate();
            string returnVal = objUT.UpdateAddAdvanceStatus(actID,
                                                      requestID,
                                                      requestStatusID,
                                                      remarks,
                                                      teamLeaderID,
                                                      currentUserID,
                                                      0);
            //insertVal = Convert.ToInt32(returnVal.Split(':')[0]);
            //sentMailVal = Convert.ToInt32(returnVal.Split(':')[1]);

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
                //sentMailVal = Convert.ToInt32(returnVal);
                //insertVal = Convert.ToInt32(returnVal);
                //retStatusID = Convert.ToInt32(returnVal);
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
                approvedMsg = "Additional Advance Request No.: '" + reqNO + "' approved  '" + mailMessage;
            else
                approvedMsg = "Additional Advance Request No.: '" + reqNO + "' approved " + mailMessage;

            if (insertVal > 0)
            {
                if (sentMailVal > 0)
                {
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved )
                    {
                        SuccessMessage(approvedMsg);
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    {
                       
                        if(currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Additional Advance Request No. : '" + reqNO + "' rejected " + mailMessage);
                        }
                        else
                        {
                            SuccessMessage("Additional Advance Request No. : '" + reqNO + "' deleted " + mailMessage);
                        }

                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                    {
                        SuccessMessage("Additional Advance Request No. : '" + reqNO + "' cancelled " + mailMessage);
                    }
                }
                else
                {
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                    {
                        SuccessMessage(approvedMsg + "successfully, please resend e-mail from Tour Information List!");
                    }
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    {


                        if (currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Additional Advance Request No. : '" + reqNO + "' rejected successfully.");
                        }
                        else
                        {
                            SuccessMessage("Additional Advance Request No. : '" + reqNO + "' deleted successfully.");
                        }
                        //SuccessMessage("Additional Advance Request No. : '" + reqNO + "' deleted successfully.");
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                    {
                        SuccessMessage("Additional Advance Request No. : '" + reqNO + "' cancelled successfully.");
                    }
                }
                GetAddAdvanceRequestList(); 
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


    protected void btnAddNewTourInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/AddTourAdvAmt.aspx");
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