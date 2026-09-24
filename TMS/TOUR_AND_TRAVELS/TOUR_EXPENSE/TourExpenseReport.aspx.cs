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

public partial class TOUR_AND_TRAVELS_TOUR_TourExpenseReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    TourExpenseSendMail objTourExpenseSendMail = new TourExpenseSendMail();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsSanctionNo = new DataSet();
    DataSet dsTourExpensStatus = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsTourExpenseList = new DataSet();
    DataTable dtTeamMembers = new DataTable();
    //string startDateSearch = string.Empty;
    //string endDateSearch = string.Empty;
    string tourExpenseNoSearch = string.Empty;
    string sanctionNoSearch = string.Empty;
    int statusIDSearch = 0;
    int empRecordID = 0;
    int teamMemberID = 0;
    string teamMembers = string.Empty;
    int departmentID = 0;
    double advanceAmount = 0;
    int advanceAmtcurrencyID = 0;
    string advanceAmtcurrencyCode = string.Empty;


    private string _employeeName = "";
    private string _tourSanctionNumber = "";

    private int tourID = 0;
    private string attachment1FileName = string.Empty;
    private string attachment2FileName = string.Empty;
    private double airTicketAmt = 0;
    private double hotelAmt = 0;
    private double taxiAmt = 0;
    private double othersAmt = 0;
    private double totalAmt = 0;
    private int totalAmtCurrencyID = 0;
    private string remarks = string.Empty;

    DataSet dsLocalTravelType = new DataSet();
    DataSet dsTripType = new DataSet();
    DataSet dsTravelMode = new DataSet();
    DataSet dsSegmentType = new DataSet();
    DataSet dsVisitType = new DataSet();
    DataSet dsCurrency = new DataSet();

    //int actID = 0;
    //int tourID = 0;
    //int empRecordID = 0;
    //int tourStatusID = 0;
    //string tourStatus = string.Empty;

    string startDate = string.Empty;
    string endDate = string.Empty;
    string custVendName = string.Empty;
    string placeOfVisit = string.Empty;
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
    //string remarks = string.Empty;

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

    string createdBy = string.Empty;
    string createdByEmail = string.Empty;
    string createdOn = string.Empty;
    string createdRemarks = string.Empty;


    string approvedBy = string.Empty;
    string approvedByEmail = string.Empty;
    string approvedOn = string.Empty;
    string approvedRemarks = string.Empty;

    string deletedBy = string.Empty;
    string deletedByEmail = string.Empty;
    string deletedOn = string.Empty;
    string deletedRemarks = string.Empty;

    string cancelledBy = string.Empty;
    string cancelledByEmail = string.Empty;
    string cancelledOn = string.Empty;
    string cancelledRemarks = string.Empty;

    string hrTeamEmail = string.Empty;

    string accTeamEmail = string.Empty;
    string accTeamName = string.Empty;

    string advanceAmt = string.Empty;
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

                //hdInvoiceDateToU.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtInvoiceDateToU.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                Session["CHK_ACC"] = null;
                Session["TOUR_EXPENSE_LIST"] = null;
                BindExpenseStatus();
                BindEmployee();
                //BindCurrency();

                //if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tourno"])))
                //    txtTourNo.Text = Convert.ToString(Request.QueryString["tourno"]);
                //else
                //    txtTourNo.Text = string.Empty;

                GetTourExpenseList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void chkClearUnclearDates_CheckedChanged(object sender, EventArgs e)
    {
        if (chkClearUnclearDates.Checked)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
            txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

            var endDate = startDate.AddMonths(1).AddDays(-1);
            hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
            txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
        }
        else
        {
            hdStartDateSearch.Value = "";
            txtStartDateSearch.Text = "";

            hdEndDateSearch.Value = "";
            txtEndDateSearch.Text = "";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetTourExpenseList();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTourExpenseList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["TOUR_EXPENSE_LIST"];
            ToCSVNew01(dt);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }



    //protected void btnGetTourSanctionNo_Click(object sender, EventArgs e)
    //{
    //    mpeTourSanctionNo.Show();
    //    GetTourSanctionDetail();
    //}

    //protected void btnbtnSearchTourSanctionNo_Click(object sender, EventArgs e)
    //{
    //    GetTourSanctionDetail();
    //    mpeTourSanctionNo.Show();
    //}



    protected void gvTourExpenseList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                int tourID = 0;
                if (Convert.ToString(e.CommandArgument) == "STATUS" ||
                    Convert.ToString(e.CommandArgument) == "VIEWFILE1" ||
                    Convert.ToString(e.CommandArgument) == "VIEWFILE2" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "CLOSE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordId = gvTourExpenseList.Rows[rowindex].FindControl("lblRecordId") as Label;
                Label lblTourExpenseNo = gvTourExpenseList.Rows[rowindex].FindControl("lblTourExpenseNo") as Label;
                Label lblTourID = gvTourExpenseList.Rows[rowindex].FindControl("lblTourID") as Label;

                Label lblTourNo = gvTourExpenseList.Rows[rowindex].FindControl("lblTourNo") as Label;
                Label lblTourSanctionNo = gvTourExpenseList.Rows[rowindex].FindControl("lblTourSanctionNo") as Label;
                Label lblEmployeeName = gvTourExpenseList.Rows[rowindex].FindControl("lblEmployeeName") as Label;
                Label lblEmployeeCode = gvTourExpenseList.Rows[rowindex].FindControl("lblEmployeeCode") as Label;
                Label lblStartDate = gvTourExpenseList.Rows[rowindex].FindControl("lblStartDate") as Label;
                Label lblEndDate = gvTourExpenseList.Rows[rowindex].FindControl("lblEndDate") as Label;
                Label lblCustomerName = gvTourExpenseList.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblEmployeeDesignation = gvTourExpenseList.Rows[rowindex].FindControl("lblEmployeeDesignation") as Label;
                Label lblPlaceOfVisit = gvTourExpenseList.Rows[rowindex].FindControl("lblPlaceOfVisit") as Label;
                Label lblVisitType = gvTourExpenseList.Rows[rowindex].FindControl("lblVisitType") as Label;
                Label lblJobNo = gvTourExpenseList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblStatusID = gvTourExpenseList.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblStatusName = gvTourExpenseList.Rows[rowindex].FindControl("lblStatusName") as Label;
                Label lblAirTicketAmount = gvTourExpenseList.Rows[rowindex].FindControl("lblAirTicketAmount") as Label;
                Label lblHotelAmount = gvTourExpenseList.Rows[rowindex].FindControl("lblHotelAmount") as Label;
                Label lblTaxiAmount = gvTourExpenseList.Rows[rowindex].FindControl("lblTaxiAmount") as Label;
                Label lblOthersAmount = gvTourExpenseList.Rows[rowindex].FindControl("lblOthersAmount") as Label;
                Label lblAdvanceAmount = gvTourExpenseList.Rows[rowindex].FindControl("lblAdvanceAmount") as Label;
                Label lblTotalAmount = gvTourExpenseList.Rows[rowindex].FindControl("lblTotalAmount") as Label;
                Label lblCurrencyId = gvTourExpenseList.Rows[rowindex].FindControl("lblCurrencyId") as Label;
                Label lblCurrencyCode = gvTourExpenseList.Rows[rowindex].FindControl("lblCurrencyCode") as Label;
                Label lblRemarks = gvTourExpenseList.Rows[rowindex].FindControl("lblRemarks") as Label;
                Label lblCreatedBy = gvTourExpenseList.Rows[rowindex].FindControl("lblCreatedBy") as Label;

                ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordId.Text);
                //ViewState["TOUR_EXPENSE_NO"] = Convert.ToString(lblTourExpenseNo.Text);
                //ViewState["TOUR_ID_EX"] = lblTourID.Text;

                //lblLegend.Text = "Tour Expense [" + lblTourExpenseNo.Text + "]";

                //txtTourSanctionNoToU.Text = Convert.ToString(lblTourSanctionNo.Text);
                //txtTourNoToU.Text = Convert.ToString(lblTourNo.Text);
                //txtEmployeeNameToU.Text = Convert.ToString(lblEmployeeName.Text);
                //txtEmployeeIDToU.Text = Convert.ToString(lblEmployeeCode.Text);
                //txtStartDateToU.Text = Convert.ToString(lblStartDate.Text);
                //txtEndDateToU.Text = Convert.ToString(lblEndDate.Text);
                //txtDaysToU.Text = (Convert.ToDateTime(lblEndDate.Text) - Convert.ToDateTime(lblStartDate.Text)).TotalDays.ToString();

                //txtCustVendNameToU.Text = Convert.ToString(lblCustomerName.Text);
                //txtPlaceOfVisitToU.Text = Convert.ToString(lblPlaceOfVisit.Text);
                //txtPurposeOfVisitToU.Text = Convert.ToString(lblVisitType.Text);
                //txtJobInqNoToU.Text = Convert.ToString(lblJobNo.Text);

                //txtAirTicketToU.Text = Convert.ToString(lblAirTicketAmount.Text);
                //txtHotelToU.Text = Convert.ToString(lblHotelAmount.Text);
                //txtTaxiToU.Text = Convert.ToString(lblTaxiAmount.Text);
                //txtOthersToU.Text = Convert.ToString(lblOthersAmount.Text);
                //txtTotalToU.Text = Convert.ToString(lblTotalAmount.Text);
                //ddlTotalCurrencyToU.SelectedValue = Convert.ToString(lblCurrencyId.Text);
                //txtAdvanceObtainedToU.Text = Convert.ToString(lblAdvanceAmount.Text);
                //ddlAdvanceObtainedCurrenyToU.SelectedValue = Convert.ToString(lblCurrencyId.Text);
                //txtRemarksToU.Text = Convert.ToString(lblRemarks.Text);

                //txtClosedRemarksToU.Text = string.Empty;

                //pnlUploadAttachments.Visible = false;
                //pnlClosedRemarks.Visible = false;

                //btnSubmit.Visible = false;
                //btnUpdateStatus.Visible = false;

                //if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                //{
                //    pnlUploadAttachments.Visible = true;
                //    btnSubmit.Visible = true;
                //    txtAirTicketToU.Enabled = true;
                //    txtHotelToU.Enabled = true;
                //    txtTaxiToU.Enabled = true;
                //    txtOthersToU.Enabled = true;

                //    if (Convert.ToString(lblStatusName.Text) == "Open" || Convert.ToInt32(lblStatusID.Text) == 1)
                //    {
                //        if (Convert.ToInt32(lblCreatedBy.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                //        {
                //            ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourExpenseActions.Update;
                //            ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourExpenseStatus.Open;
                //            btnSubmit.Text = "Update Tour Expense";

                //            this.ModalPopupExtender1.Show();
                //        }
                //    }
                //}
                //else if (Convert.ToString(e.CommandArgument) == "CLOSE")
                //{
                //    btnUpdateStatus.Visible = true;
                //    txtAirTicketToU.Enabled = false;
                //    txtHotelToU.Enabled = false;
                //    txtTaxiToU.Enabled = false;
                //    txtOthersToU.Enabled = false;
                //    txtRemarksToU.Enabled = false;
                //    pnlClosedRemarks.Visible = true;

                //    if (Convert.ToString(lblStatusName.Text) == "Open" || Convert.ToInt32(lblStatusID.Text) == 1)
                //    {
                //        ViewState["ACT_ID"] = (int)TandTAllStatus.EnumTourExpenseActions.Close;
                //        ViewState["TOUR_STATUS_ID"] = (int)TandTAllStatus.EnumTourExpenseStatus.Closed;

                //        btnSubmit.Text = "Close Tour Expense";
                //    }
                //    this.ModalPopupExtender1.Show();
                //}


                if (Convert.ToString(e.CommandArgument) == "VIEWFILE1")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblRecordId.Text), (int)TandTAllStatus.EnumTourExpenseFiles.File1);
                }

                else if (Convert.ToString(e.CommandArgument) == "VIEWFILE2")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblRecordId.Text), (int)TandTAllStatus.EnumTourExpenseFiles.File1);
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                {
                    //int mailActID = 0;
                    //int value = objTourExpenseSendMail.ProcessAndSendMail();
                    //if (value > 0)
                    //{
                    //    if (Convert.ToString(lblTourStatus.Text) == "New" ||
                    //        Convert.ToInt32(lblTourStatusID.Text) == 1)
                    //        mailActID = 1;
                    //    else if (Convert.ToString(lblTourStatus.Text) == "Approved" ||
                    //             Convert.ToInt32(lblTourStatusID.Text) == 2)
                    //        mailActID = 2;
                    //    else if (Convert.ToString(lblTourStatus.Text) == "Deleted" ||
                    //             Convert.ToInt32(lblTourStatusID.Text) == 3)
                    //        mailActID = 3;
                    //    else if (Convert.ToString(lblTourStatus.Text) == "Cancelled" ||
                    //             Convert.ToInt32(lblTourStatusID.Text) == 4)
                    //        mailActID = 4;

                    //    int val = objTourAndTravels.UpdateTourInfoMailStatus(mailActID, tourID, Convert.ToInt32(Session["CHK_ACC"]));
                    //    SuccessMessage("Mail sent successfully.");
                    //    GetTourExpenseList();
                    //}
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    ModalPopupExtender4.Show();
                    iframeViewTourInformationInPDF.Attributes.Add("src", "TourExpenseInPDF.aspx?recordid=" + Convert.ToString(lblRecordId.Text) + "&tourexpenseno=" + Convert.ToString(lblTourExpenseNo.Text));
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

    protected void gvTourExpenseList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                //Label lblTourExpenseNo = (Label)e.Row.FindControl("lblTourExpenseNo");
                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                //Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");
                //Label lblTeamLeaderID = (Label)e.Row.FindControl("lblTeamLeaderID");
                //Label lblAdvanceAmt = (Label)e.Row.FindControl("lblAdvanceAmt");
                //Label lblAdvanceCurrencyID = (Label)e.Row.FindControl("lblAdvanceCurrencyID");
                //Label lblClosingPersonID = (Label)e.Row.FindControl("lblClosingPersonID");

                //Label lblIsOpenMailSent = (Label)e.Row.FindControl("lblIsOpenMailSent");
                //Label lblIsClosedMailSent = (Label)e.Row.FindControl("lblIsClosedMailSent");

                //ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");

                Label lblAttachment1FileName = (Label)e.Row.FindControl("lblAttachment1FileName");
                ImageButton btnAttachment1File = (ImageButton)e.Row.FindControl("btnAttachment1File");

                Label lblAttachment2FileName = (Label)e.Row.FindControl("lblAttachment2FileName");
                ImageButton btnAttachment2File = (ImageButton)e.Row.FindControl("btnAttachment2File");

                btnAttachment1File.Visible = false;
                btnAttachment1File.ToolTip = string.Empty;
                btnAttachment2File.Visible = false;
                btnAttachment2File.ToolTip = string.Empty;

                if (!string.IsNullOrEmpty(lblAttachment1FileName.Text))
                {
                    btnAttachment1File.Visible = true;
                    btnAttachment1File.ToolTip = lblAttachment1FileName.Text;
                }

                if (!string.IsNullOrEmpty(lblAttachment2FileName.Text))
                {
                    btnAttachment2File.Visible = true;
                    btnAttachment2File.ToolTip = lblAttachment2FileName.Text;
                }


                string status = lblStatusName.Text;
                int statusID = Convert.ToInt32(lblStatusID.Text);


                imgStatus.Enabled = false;
                int empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                if (status == "Open" && statusID == (int)TandTAllStatus.EnumTourExpenseStatus.Open)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/open1.png";
                    imgStatus.ToolTip = "Open";
                }

                if (status == "Close" && statusID == (int)TandTAllStatus.EnumTourExpenseStatus.Closed)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Closed03.png";
                    imgStatus.ToolTip = "Closed";
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

    //protected void gvTourSanctionNo_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (Session["EMP_RECORD_ID"] != null)
    //        {
    //            int rowindex = 0;
    //            GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
    //            rowindex = rowSelect.RowIndex;

    //            Label lblTourId = gvTourSanctionNo.Rows[rowindex].FindControl("lblTourId") as Label;
    //            Label lblTourNo = gvTourSanctionNo.Rows[rowindex].FindControl("lblTourNo") as Label;
    //            Label lblTourSanctionNo = gvTourSanctionNo.Rows[rowindex].FindControl("lblTourSanctionNo") as Label;
    //            Label lblEmployeeName = gvTourSanctionNo.Rows[rowindex].FindControl("lblEmployeeName") as Label;
    //            Label lblEmployeeId = gvTourSanctionNo.Rows[rowindex].FindControl("lblEmployeeId") as Label;
    //            Label lblDesignation = gvTourSanctionNo.Rows[rowindex].FindControl("lblDesignation") as Label;
    //            Label lblStartDate = gvTourSanctionNo.Rows[rowindex].FindControl("lblStartDate") as Label;
    //            Label lblEndDate = gvTourSanctionNo.Rows[rowindex].FindControl("lblEndDate") as Label;
    //            Label lblCustVendName = gvTourSanctionNo.Rows[rowindex].FindControl("lblCustVendName") as Label;
    //            Label lblPlaceOfVisit = gvTourSanctionNo.Rows[rowindex].FindControl("lblPlaceOfVisit") as Label;
    //            Label lblVisitType = gvTourSanctionNo.Rows[rowindex].FindControl("lblVisitType") as Label;
    //            Label lblJobNo = gvTourSanctionNo.Rows[rowindex].FindControl("lblJobNo") as Label;
    //            Label lblBusSegment = gvTourSanctionNo.Rows[rowindex].FindControl("lblBusSegment") as Label;
    //            Label lblAdvanceAmt = gvTourSanctionNo.Rows[rowindex].FindControl("lblAdvanceAmt") as Label;
    //            Label lblAdvanceCurrencyID = gvTourSanctionNo.Rows[rowindex].FindControl("lblAdvanceCurrencyID") as Label;
    //            Label lblCurrencyCode = gvTourSanctionNo.Rows[rowindex].FindControl("lblCurrencyCode") as Label;

    //            ViewState["TOUR_ID"] = lblTourId.Text;

    //            if (!string.IsNullOrEmpty(lblTourSanctionNo.Text))
    //                txtTourSanctionNoToU.Text = lblTourSanctionNo.Text;

    //            if (!string.IsNullOrEmpty(lblTourNo.Text))
    //                txtTourNoToU.Text = lblTourNo.Text;

    //            if (!string.IsNullOrEmpty(lblEmployeeName.Text))
    //                txtEmployeeNameToU.Text = lblEmployeeName.Text;

    //            if (!string.IsNullOrEmpty(lblEmployeeId.Text))
    //                txtEmployeeIDToU.Text = lblEmployeeId.Text;

    //            txtStartDateToU.Text = Convert.ToDateTime(lblStartDate.Text).ToString("dd-MMM-yyyy");
    //            txtEndDateToU.Text = Convert.ToDateTime(lblEndDate.Text).ToString("dd-MMM-yyyy");
    //            txtDaysToU.Text = (Convert.ToDateTime(lblEndDate.Text) - Convert.ToDateTime(lblStartDate.Text)).TotalDays.ToString();

    //            if (!string.IsNullOrEmpty(lblCustVendName.Text))
    //                txtCustVendNameToU.Text = lblCustVendName.Text;

    //            if (!string.IsNullOrEmpty(lblPlaceOfVisit.Text))
    //                txtPlaceOfVisitToU.Text = lblPlaceOfVisit.Text;

    //            if (!string.IsNullOrEmpty(lblVisitType.Text))
    //                txtPurposeOfVisitToU.Text = lblVisitType.Text;

    //            if (!string.IsNullOrEmpty(lblJobNo.Text))
    //                txtJobInqNoToU.Text = lblJobNo.Text;

    //            if (!string.IsNullOrEmpty(lblAdvanceAmt.Text))
    //                txtAdvanceObtainedToU.Text = lblAdvanceAmt.Text;
    //        }
    //        else
    //        {
    //            Response.Redirect("~/Login.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionUpdateMessage(ex.ToString());
    //        return;
    //    }
    //}


    //protected void btnAddNewTourExpense_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/TOUR_AND_TRAVELS/TOUR/TourExpense.aspx");
    //}

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    UpdateTourExpense();
    //}

    //protected void btnUpdateStatus_Click(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(hdConfirmUpdateStatusValue.Value) > 0)
    //    {
    //        UpdateTourStatus();
    //    }
    //}

    #endregion


    #region METHODS[=======================]

    private void BindExpenseStatus()
    {
        try
        {

            dsTourExpensStatus = objTourAndTravels.GetTourExpenseStatus();
            if (dsTourExpensStatus.Tables.Count > 0 && dsTourExpensStatus.Tables[0].Rows.Count > 0)
            {
                ddlExpenseStatus.DataSource = dsTourExpensStatus.Tables[0];
                ddlExpenseStatus.DataTextField = "STATUS_NAME";
                ddlExpenseStatus.DataValueField = "STATUS_ID";
                ddlExpenseStatus.DataBind();
                ddlExpenseStatus.Items.Insert(0, "All");
                ddlExpenseStatus.SelectedIndex = 0;
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
            dsEmployee = objTourAndTravels.GetEmployeeForTravel(0);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployeeName.DataSource = dsEmployee.Tables[0];
                ddlEmployeeName.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeName.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeName.DataBind();
                ddlEmployeeName.Items.Insert(0, "All");
                ddlEmployeeName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTourExpenseList()
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

            if (!string.IsNullOrEmpty(txtTourExpenseNo.Text))
                tourExpenseNoSearch = txtTourExpenseNo.Text.Trim();
            else
                tourExpenseNoSearch = string.Empty;

            if (!string.IsNullOrEmpty(txtSanctionNo.Text))
                sanctionNoSearch = txtSanctionNo.Text.Trim();
            else
                sanctionNoSearch = string.Empty;


            if (ddlExpenseStatus.SelectedIndex > 0)
                statusIDSearch = Convert.ToInt32(ddlExpenseStatus.SelectedValue);
            else
                statusIDSearch = 0;

            if (ddlEmployeeName.SelectedIndex > 0)
            {
                empRecordID = Convert.ToInt32(ddlEmployeeName.SelectedValue);
            }
            else empRecordID = 0;

            dsTourExpenseList = objTourAndTravels.GetTourExpenseReport(startDate, endDate, tourExpenseNoSearch
                                                                   , sanctionNoSearch, statusIDSearch, empRecordID);

            if (dsTourExpenseList.Tables.Count > 0 && dsTourExpenseList.Tables[0].Rows.Count > 0)
            {
                Session["TOUR_EXPENSE_LIST"] = dsTourExpenseList.Tables[0];
                gvTourExpenseList.DataSource = dsTourExpenseList.Tables[0];
                gvTourExpenseList.DataBind();
            }
            else
            {
                Session["TOUR_EXPENSE_LIST"] = null;
                gvTourExpenseList.DataSource = null;
                gvTourExpenseList.DataBind();
            }
            lblRecords.Text = "Records[" + dsTourExpenseList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    //private void GetTourSanctionDetail()
    //{
    //    try
    //    {
    //        _employeeName = "";
    //        _tourSanctionNumber = "";

    //        if (!string.IsNullOrEmpty(txtEmployeeNameToG.Text))
    //            _employeeName = txtEmployeeNameToG.Text;

    //        if (!string.IsNullOrEmpty(txtTourSanctionNoToG.Text))
    //            _tourSanctionNumber = txtTourSanctionNoToG.Text;

    //        dsSanctionNo = objTourAndTravels.GetSanctionNo(_tourSanctionNumber, _employeeName);

    //        if (dsSanctionNo.Tables.Count > 0 && dsSanctionNo.Tables[0].Rows.Count > 0)
    //        {
    //            gvTourSanctionNo.DataSource = dsSanctionNo.Tables[0];
    //            gvTourSanctionNo.DataBind();
    //        }
    //        else
    //        {
    //            gvTourSanctionNo.DataSource = null;
    //            gvTourSanctionNo.DataBind();
    //        }
    //        lblTourSanctionNoRecords.Text = "Records[" + dsSanctionNo.Tables[0].Rows.Count + "]";
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    //private void BindCurrency()
    //{
    //    try
    //    {
    //        dsCurrency = objTourAndTravels.GetPrimaryDetails("sp_get_currency_list");
    //        if (dsCurrency.Tables.Count > 0 && dsCurrency.Tables[0].Rows.Count > 0)
    //        {
    //            ddlTotalCurrencyToU.DataSource = dsCurrency.Tables[0];
    //            ddlTotalCurrencyToU.DataTextField = "CURRENCY_CODE";
    //            ddlTotalCurrencyToU.DataValueField = "CURRENCY_ID";
    //            ddlTotalCurrencyToU.DataBind();
    //            ddlTotalCurrencyToU.SelectedValue = "68";

    //            ddlAdvanceObtainedCurrenyToU.DataSource = dsCurrency.Tables[0];
    //            ddlAdvanceObtainedCurrenyToU.DataTextField = "CURRENCY_CODE";
    //            ddlAdvanceObtainedCurrenyToU.DataValueField = "CURRENCY_ID";
    //            ddlAdvanceObtainedCurrenyToU.DataBind();
    //            ddlAdvanceObtainedCurrenyToU.SelectedValue = "68";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //
    //    }
    //}

    //private void UpdateTourExpense()
    //{
    //    try
    //    {
    //        int recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
    //        tourID = Convert.ToInt32(ViewState["TOUR_ID_EX"]);
    //        airTicketAmt = 0;
    //        hotelAmt = 0;
    //        taxiAmt = 0;
    //        othersAmt = 0;
    //        totalAmt = 0;
    //        totalAmtCurrencyID = 0;
    //        string remarks = string.Empty;

    //        Byte[] attachment1Bytes = null;
    //        if (file1UploadToU.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(file1UploadToU.PostedFile.FileName))
    //            {
    //                attachment1FileName = file1UploadToU.PostedFile.FileName;
    //                attachment1Bytes = GetFileBytes(file1UploadToU.PostedFile.FileName, file1UploadToU.PostedFile.InputStream);
    //            }
    //            else
    //            {
    //                attachment1FileName = string.Empty;
    //                attachment1Bytes = null;
    //            }
    //        }
    //        else
    //        {
    //            attachment1FileName = string.Empty;
    //            attachment1Bytes = null;
    //        }


    //        Byte[] attachment2Bytes = null;
    //        if (file2UploadToU.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(file2UploadToU.PostedFile.FileName))
    //            {
    //                attachment2FileName = file2UploadToU.PostedFile.FileName;
    //                attachment2Bytes = GetFileBytes(file2UploadToU.PostedFile.FileName, file2UploadToU.PostedFile.InputStream);
    //            }
    //            else
    //            {
    //                attachment2FileName = string.Empty;
    //                attachment2Bytes = null;
    //            }
    //        }
    //        else
    //        {
    //            attachment2FileName = string.Empty;
    //            attachment2Bytes = null;
    //        }

    //        if (!string.IsNullOrEmpty(txtAirTicketToU.Text))
    //            airTicketAmt = Convert.ToDouble(txtAirTicketToU.Text);

    //        if (!string.IsNullOrEmpty(txtHotelToU.Text))
    //            hotelAmt = Convert.ToDouble(txtHotelToU.Text);

    //        if (!string.IsNullOrEmpty(txtTaxiToU.Text))
    //            taxiAmt = Convert.ToDouble(txtTaxiToU.Text);

    //        if (!string.IsNullOrEmpty(txtOthersToU.Text))
    //            othersAmt = Convert.ToDouble(txtOthersToU.Text);

    //        if (!string.IsNullOrEmpty(hdTotalToU.Value))
    //            totalAmt = Convert.ToDouble(hdTotalToU.Value);

    //        if (ddlTotalCurrencyToU.SelectedIndex > 0)
    //            totalAmtCurrencyID = Convert.ToInt32(ddlTotalCurrencyToU.SelectedValue);

    //        if (!string.IsNullOrEmpty(txtRemarksToU.Text))
    //            remarks = txtRemarksToU.Text;

    //        string retValue = objTourAndTravels.InsertUpdateTravelExpense(recordID
    //                                                              , tourID
    //                                                              , airTicketAmt
    //                                                              , hotelAmt
    //                                                              , taxiAmt
    //                                                              , othersAmt
    //                                                              , totalAmt
    //                                                              , totalAmtCurrencyID
    //                                                              , remarks
    //                                                              , attachment1FileName, attachment1Bytes
    //                                                              , attachment2FileName, attachment2Bytes
    //                                                              , Convert.ToInt32(Session["EMP_RECORD_ID"]));

    //        int isertedValue = 0;
    //        string tourExpenseNo = string.Empty;

    //        if (!string.IsNullOrEmpty(retValue))
    //        {
    //            tourExpenseNo = Convert.ToString(ViewState["TOUR_EXPENSE_NO"]);

    //            int mailSentValue = objTourExpenseSendMail.ProcessAndSendMail(recordID
    //                                                                         , (int)TandTAllStatus.EnumTourExpenseMailType.OpenMail
    //                                                                         , (int)TandTAllStatus.EnumTourExpenseStatus.Open);
    //            if (mailSentValue > 0)
    //            {
    //                int val = objTourAndTravels.UpdateTourExpenseMailStatus(recordID, (int)TandTAllStatus.EnumTourExpenseActions.Update);
    //                SuccessMessage("Tour Expense No. " + tourExpenseNo + " added and mail sent successfully.");
    //            }
    //            else
    //            {
    //                SuccessMessage("Tour Expense No. " + tourExpenseNo + " added successfully, please send mail from Expense List.");
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

    //private byte[] GetFileBytes(string fileName, Stream stream)
    //{
    //    Byte[] GSTbytes = null;
    //    #region
    //    try
    //    {
    //        string GSTFilePath = fileName;
    //        string GSTFileName = Path.GetFileName(GSTFilePath);
    //        string GSText = Path.GetExtension(GSTFileName);
    //        string GSTContentType = String.Empty;
    //        switch (GSText)
    //        {
    //            case ".jpg":
    //                GSTContentType = "image/jpg";
    //                break;
    //            case ".jpeg":
    //                GSTContentType = "image/jpeg";
    //                break;
    //            case ".bmp":
    //                GSTContentType = "image/bmp";
    //                break;
    //            case ".png":
    //                GSTContentType = "image/png";
    //                break;
    //            case ".gif":
    //                GSTContentType = "image/gif";
    //                break;
    //            case ".pdf":
    //                GSTContentType = "application/pdf";
    //                break;
    //            case ".JPG":
    //                GSTContentType = "image/JPG";
    //                break;
    //            case ".JPEG":
    //                GSTContentType = "image/JPEG";
    //                break;
    //            case ".BMP":
    //                GSTContentType = "image/BMP";
    //                break;
    //            case ".PNG":
    //                GSTContentType = "image/PNG";
    //                break;
    //            case ".GIF":
    //                GSTContentType = "image/GIF";
    //                break;
    //            case ".PDF":
    //                GSTContentType = "application/PDF";
    //                break;
    //        }
    //        Stream GSTfs = null;
    //        BinaryReader GSTbr = null;
    //        if (GSTContentType != String.Empty)
    //        {
    //            try
    //            {
    //                GSTfs = stream;
    //                GSTfs.Position = 0;
    //                GSTbr = new BinaryReader(GSTfs);
    //                GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //        else
    //        {
    //            ExceptionMessage("File format not recognised. Upload Image/PDF formats");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //    }
    //    #endregion
    //    return GSTbytes;
    //}


    //private void UpdateTourStatus()
    //{
    //    try
    //    {
    //        int actID = Convert.ToInt32(ViewState["ACT_ID"]);
    //        int recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
    //        string tourExpenseNo = Convert.ToString(ViewState["TOUR_EXPENSE_NO"]);
    //        string remarks = string.Empty;
    //        string invoiceNo = string.Empty;
    //        string invoiceDate = string.Empty;

    //        if (!string.IsNullOrEmpty(txtInvoiceNumberToU.Text))
    //            invoiceNo = txtInvoiceNumberToU.Text;

    //        if (!string.IsNullOrEmpty(hdInvoiceDateToU.Value))
    //            invoiceDate = Convert.ToDateTime(hdInvoiceDateToU.Value).ToString("yyyy-MM-dd");

    //        if (!string.IsNullOrEmpty(txtClosedRemarksToU.Text))
    //            remarks = txtClosedRemarksToU.Text;


    //        int value = objTourAndTravels.UpdateTourExpenseStatus(recordID
    //                                                            , (int)TandTAllStatus.EnumTourExpenseStatus.Closed
    //                                                            , invoiceNo
    //                                                            , invoiceDate
    //                                                            , remarks
    //                                                            , Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //        if (value > 0)
    //        {
    //            int sendMailValue = objTourExpenseSendMail.ProcessAndSendMail(recordID
    //                                                                           , (int)TandTAllStatus.EnumTourExpenseMailType.ClosedMail
    //                                                                           , (int)TandTAllStatus.EnumTourExpenseStatus.Closed);
    //            if (sendMailValue > 0)
    //            {
    //                int val = objTourAndTravels.UpdateTourExpenseMailStatus(recordID, actID);
    //                SuccessMessage("Tour Expense No.: '" + tourExpenseNo + "' closed and mail sent successfully.");
    //            }
    //            else
    //            {
    //                SuccessMessage("Tour Information No.: '" + tourExpenseNo + "' closed successfully, please resend closed email from Tour Expenes List.");
    //            }

    //            GetTourExpenseList();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //    }
    //}

    //private string GenerateAdviceCSV(string beneficiaryName, string tourSanctionNo, string amount, string emailAdd1,
    //                                    string beneBankAccount, string beneBankIFSCBANKCode, int unitID)
    //{
    //    string csvTxt = string.Empty;

    //    string transactionType = string.Empty;
    //    string referenceNumber = string.Empty;
    //    string drAccountNo = string.Empty;
    //    string paymentNarration = string.Empty;
    //    //string beneficiaryName = string.Empty;
    //    string beneAdd1 = string.Empty;
    //    string beneAdd2 = string.Empty;
    //    string beneAdd3 = string.Empty;
    //    string paymentLocation = string.Empty;
    //    string chequeNo = string.Empty;
    //    string valueDate = string.Empty;
    //    //string amount = string.Empty;
    //    string printBranchLocation = string.Empty;
    //    //string emailAdd1 = string.Empty;
    //    string emailAdd2 = string.Empty;
    //    string emailAdd3 = string.Empty;
    //    string freeText = string.Empty;
    //    string nA1 = string.Empty;
    //    string nA2 = string.Empty;
    //    string nA3 = string.Empty;
    //    string nA4 = string.Empty;
    //    string nA5 = string.Empty;
    //    //string beneBankAccount = string.Empty;
    //    //string beneBankIFSCBANKCode = string.Empty;
    //    string nA6 = string.Empty;
    //    string deliverTo = string.Empty;
    //    string orderingPartyName = string.Empty;
    //    string orderingPartyAdd1 = string.Empty;
    //    string orderingPartyAdd2 = string.Empty;
    //    string orderingPartyAdd3 = string.Empty;
    //    string orderingPartyAccount = string.Empty;
    //    string bankName = string.Empty;
    //    string bankToBankInfo = string.Empty;

    //    try
    //    {
    //        string csv = string.Empty;


    //        if (Convert.ToDouble(amount) <= 200000)
    //            transactionType = "NEFT";
    //        else
    //            transactionType = "RTGS";

    //        referenceNumber = "HSBC";

    //        //A35
    //        if (unitID == 1)
    //            drAccountNo = "'499391803001";
    //        //Delhi
    //        else if (unitID == 2)
    //            drAccountNo = "'499391803005";
    //        //SEZ
    //        else if (unitID == 3)
    //            drAccountNo = "'499391803004";
    //        //GNU       
    //        else if (unitID == 4)
    //            drAccountNo = "'499391803003";

    //        paymentNarration = string.Empty;

    //        beneAdd1 = string.Empty;
    //        beneAdd2 = string.Empty;
    //        beneAdd3 = string.Empty;
    //        paymentLocation = string.Empty;
    //        chequeNo = string.Empty;
    //        valueDate = DateTime.Now.ToString("dd/MM/yyyy");
    //        printBranchLocation = string.Empty;
    //        emailAdd2 = string.Empty;
    //        emailAdd3 = string.Empty;
    //        freeText = tourSanctionNo;
    //        nA1 = string.Empty;
    //        nA2 = string.Empty;
    //        nA3 = string.Empty;
    //        nA4 = string.Empty;
    //        nA5 = string.Empty;
    //        nA6 = string.Empty;
    //        deliverTo = string.Empty;
    //        orderingPartyName = "COPERION IDEAL";
    //        orderingPartyAdd1 = "PRIVATE LIMITED";
    //        orderingPartyAdd2 = "A35 SECTOR 64";
    //        orderingPartyAdd3 = "NOIDA";
    //        orderingPartyAccount = "201307";
    //        bankName = string.Empty;
    //        bankToBankInfo = string.Empty;


    //        csv += "COPERION IDEAL PRIVATE LIMITED,";
    //        csv += "\r\n";
    //        //"Transaction_Type,Reference_Number,Dr_Account_No,Payment_Narration,Beneficiary_Name,Bene_Add_1,Bene_Add_2,Bene_Add_3,Payment_Location,Cheque_No,Value_Date,Amount,Print_Branch_Location,Email_Add_1,Email_Add_2,Email_Add_3,Free_Text,NA,NA,NA,NA,NA,Bene_Bank_Account_#,Bene_Bank_IFSC/BANK_Code,NA,Deliver_To,Ordering_Party_Name,Ordering_Party_Add1,Ordering_Party_Add2,Ordering_Party_Add3,Ordering_party_Account,Bank_Name,Bank_to_Bank_Info,";
    //        csv += "Transaction Type,Reference Number,Dr Account No,Payment Narration,Beneficiary Name,Bene Add 1,Bene Add 2,Bene Add 3,Payment Location,Cheque No,Value Date,Amount,Print Branch Location,Email Add-1,Email Add-2,Email Add-3,FreeText,NA,NA,NA,NA,NA,Bene Bank Account #,Bene Bank IFSC /  BANK Code,NA,Deliver To,Ordering Party Name,Ordering_Party Add1,Ordering_Party Add2,Ordering_Party Add3,Ordering_party_Account,BANK_NAME,Bank_to_Bank_Info,";
    //        csv += "\r\n";

    //        csv += transactionType + "," + referenceNumber + "," + drAccountNo + "," +
    //                        paymentNarration + "," + beneficiaryName + "," + beneAdd1 + "," + beneAdd2 + "," + beneAdd3 + "," + paymentLocation + "," + chequeNo + "," + valueDate + "," + amount + "," +
    //                        printBranchLocation + "," + emailAdd1 + "," + emailAdd2 + "," + emailAdd3 + "," + freeText + "," + nA1 + "," + nA2 + "," + nA3 + "," + nA4 + "," + nA5 + "," +
    //                        beneBankAccount + "," + beneBankIFSCBANKCode + "," + nA6 + "," + deliverTo + "," + orderingPartyName + "," +
    //                        orderingPartyAdd1 + "," + orderingPartyAdd2 + "," + orderingPartyAdd3 + "," + orderingPartyAccount + "," + bankName + "," + bankToBankInfo + ",";



    //        if (!string.IsNullOrEmpty(csv))
    //        {
    //            csvTxt = csv;
    //        }
    //        else
    //        {
    //            csvTxt = string.Empty;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        csvTxt = string.Empty;
    //    }
    //    return csvTxt;
    //}


    //private void ToCSVNew01(string csv, int tourID, string tourSanctionNo)
    //{

    //    try
    //    {
    //        string fileName = string.Empty;

    //        string[] strTxt = tourSanctionNo.Split('/');
    //        string sanctionNoTxt = string.Empty;
    //        foreach (string item in strTxt)
    //        {
    //            sanctionNoTxt += item + "_";
    //        }
    //        if (!string.IsNullOrEmpty(sanctionNoTxt))
    //            fileName = "Payment_Advice_" + sanctionNoTxt.TrimEnd('_');
    //        else
    //            fileName = "Payment_Advice";

    //        HttpResponse response = HttpContext.Current.Response;

    //        response.Clear();
    //        response.Buffer = true;
    //        response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
    //        response.Charset = "";
    //        response.ContentType = "application/text";
    //        response.Output.Write(csv);
    //        objTourAndTravels.UpdateTourInfoMailStatus(5, tourID, Convert.ToInt32(Session["CHK_ACC"]));

    //        response.Flush();
    //        HttpContext.Current.ApplicationInstance.CompleteRequest();
    //        response.End();
    //    }
    //    catch (Exception)
    //    {
    //        //
    //    }
    //}


    private void ViewAttachedFilesNew01(int recordID, int fileType)
    {
        try
        {
            //byte[] bytes = null;
            DataTable dsTourInfo = (DataTable)Session["TOUR_EXPENSE_LIST"];
            string fileName = string.Empty;
            string extn = string.Empty;
            foreach (DataRow dr in dsTourInfo.Select("RECORD_ID='" + recordID + "'"))
            {
                if (fileType == (int)TandTAllStatus.EnumTourExpenseFiles.File1)
                {
                    fileName = Convert.ToString(dr["ATTACHMENT1_FILE_NAME"]);
                    //bytes = (byte[])dr["PASSPORT_COPY_DOC"];
                }
                else if (fileType == (int)TandTAllStatus.EnumTourExpenseFiles.File2)
                {
                    fileName = Convert.ToString(dr["ATTACHMENT2_FILE_NAME"]);
                    //bytes = (byte[])dr["PASSPORT_COPY_DOC"];
                }
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" ||
                    extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewExpenseImgFile.ashx?recordid=" + recordID + "&filetypeid=" + fileType;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewExpensePDFFile.aspx?recordid=" + recordID + "&filetypeid=" + fileType);
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


    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName == "RECORD_ID" ||
                    column.ColumnName == "STATUS_ID" ||
                    column.ColumnName == "ATTACHMENT1_FILE_NAME" ||
                    column.ColumnName == "ATTACHMENT2_FILE_NAME")
                {
                    //
                }
                else
                {
                    csv += column.ColumnName + ',';
                }
                
            }
            csv += "\r\n";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName == "RECORD_ID" ||
                    column.ColumnName == "STATUS_ID" ||
                    column.ColumnName == "ATTACHMENT1_FILE_NAME" ||
                    column.ColumnName == "ATTACHMENT2_FILE_NAME")
                    {
                        //
                    }
                    else
                    {
                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                    }
                }
                csv += "\r\n";
            }

            string fileName = "Tour_Expense_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
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