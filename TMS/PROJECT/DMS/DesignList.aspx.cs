using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PROJECT_DMS_DesignList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    DMSSendMail objDMSSendMail = new DMSSendMail();
    BAL.Project objProject = new BAL.Project();

    DataSet dsUnit = new DataSet();
    DataSet dsUserTimeDetails = new DataSet();
    DataSet dtDesignDetailsList = new DataSet();
    DataSet dsDesignStatusSearch = new DataSet();
    DataSet dsDesignEngg = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsDesignChecker = new DataSet();
    DataSet dsDesignCategory = new DataSet();
    DataSet dsDesignJobNo = new DataSet();
    DataSet dsProjectJobNo = new DataSet();
    DataTable dtAction = new DataTable();
    DataTable dtDrawings = new DataTable();


    string jobNo = string.Empty;
    int jobUnitID = 0;
    int drawingRecordID = 0;
    int categoryID = 0;
    string isPlanned = string.Empty;
    string drawingNo = string.Empty;

    string clientDrawingNo = string.Empty;
    string contractorDrawingNo = string.Empty;

    int postingStatusID = 0;
    int datetTypeID = 0;
    string dateSign = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    int designCheckerID = 0;
    int responsibleEnggID = 0;
    int createdByID = 0;



    int srNo = 0;
    int amendmentCount = 0;
    string extendedRecordIDs = string.Empty;
    int recordID = 0;
    //string jobNo = string.Empty;
    //int categoryID = 0;
    string category = string.Empty;
    string description = string.Empty;
    string UOM = string.Empty;
    int quantity = 0;
    string reqdDateByProjectTeam = string.Empty;
    int isPlannedID = 0;
    //string isPlanned = string.Empty;
    string plannedStartDateByDesignTeam = string.Empty;
    string plannedCompletionDateByDesignTeam = string.Empty;
    //string drawingNo = string.Empty;
    string documentLink = string.Empty;
    int drawingRevNo = 0;
    string workingStatus = string.Empty;
    string expectedCompletionDate = string.Empty;
    int responsibleDesignEnggID = 0;
    int isDesignEnggFlag = 0;
    string responsibleDesignEngg = string.Empty;
    //int postingStatusID = 0;
    string postingStatus = string.Empty;
    int isApplicableForProduction = 0;
    string remarks = string.Empty;
    int isRevised = 0;
    int revisedRecordID = 0;

    int companyID = 0;
    string custCode = string.Empty;
    string poNo = string.Empty;

    int statusID = 0;



    //int datetTypeID = 0;
    //string dateSign = string.Empty;
    //string fromDate = string.Empty;
    //string toDate = string.Empty;    
    //int responsibleEnggID = 0;
    //int createdByID = 0;




    int empRecordID = 0;
    string projectCategory = string.Empty;
    string projectNumber = string.Empty;
    string jobNumber = string.Empty;

    string drawingCategory = string.Empty;
    string serialNumber = string.Empty;
    string size = string.Empty;
    string rev = string.Empty;
    int sheets = 0;
    string drawingID = string.Empty;
    int drawingTypeID = 0;

    string startTime = string.Empty;
    string endTime = string.Empty;
    string timeSpent = string.Empty;
    string title = string.Empty;
    string status = string.Empty;

    string timesheetDate = string.Empty;

    string fileName = string.Empty;
    string body = string.Empty;
    string subject = string.Empty;
    string from = string.Empty;
    string to = string.Empty;
    string toName = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;
    string mailSentDate = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtUnitList"] = null;
                Session["dtPMList"] = null;
                Session["dtTimesheet"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindCompany();
                BindDesignStatusSearch();
                BindDesignCategoryMainSearch();
                BindDesignCheckerSearch();
                BindRespinsibleEngSearch();
                BindCreatedBySearch();
                BindUpdateStatusList();


                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                {
                    hdDesignResponsibleEnggFlag.Value = "1";
                }
                else
                {
                    hdDesignResponsibleEnggFlag.Value = "0";
                }

                if (Request.QueryString["dmsstatusid"] != null && Convert.ToInt32(Request.QueryString["dmsstatusid"]) > 0)
                {
                    ddlPostingStatusMainSearch.SelectedValue = Convert.ToString(Request.QueryString["dmsstatusid"]);
                }

                GetDesignDetailList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetDesignDetailList();
    }

    protected void gvDesignDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int statusID = 0;
                int rowindex = 0;
                //int sendToAmendmentFlag = 0;

                hdConfirmValue.Value = "0";

                HidePanelDesignDetail();


                if (Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "ADD_TIMESHEET" ||
                    Convert.ToString(e.CommandArgument) == "ASSIGN" ||
                    Convert.ToString(e.CommandArgument) == "SEND_TO_CHECKING" ||
                    Convert.ToString(e.CommandArgument) == "CHECK" ||
                    Convert.ToString(e.CommandArgument) == "SEND_TO_AMENDMENT" ||
                    Convert.ToString(e.CommandArgument) == "SEND_TO_CORRECTION" ||
                    Convert.ToString(e.CommandArgument) == "CLOSE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                if (Convert.ToString(e.CommandArgument) == "VIEW_ADD_ATT" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_DETAIL" ||
                    Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                TextBox txtSrNo = gvDesignDetails.Rows[rowindex].FindControl("txtSrNo") as TextBox;
                Button btnViewDetail = gvDesignDetails.Rows[rowindex].FindControl("btnViewDetail") as Button;
                Label lblStatusID = gvDesignDetails.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblRecordID = gvDesignDetails.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblDrawingID = gvDesignDetails.Rows[rowindex].FindControl("lblDrawingID") as Label;

                Label lblJOBUnitID = gvDesignDetails.Rows[rowindex].FindControl("lblJOBUnitID") as Label;

                Label lblCategoryID = gvDesignDetails.Rows[rowindex].FindControl("lblCategoryID") as Label;
                Label lblIsRevised = gvDesignDetails.Rows[rowindex].FindControl("lblIsRevised") as Label;
                Label lblCreatedByID = gvDesignDetails.Rows[rowindex].FindControl("lblCreatedByID") as Label;
                Label lblOpenByID = gvDesignDetails.Rows[rowindex].FindControl("lblOpenByID") as Label;
                Label lblSendToCheckingByID = gvDesignDetails.Rows[rowindex].FindControl("lblSendToCheckingByID") as Label;
                Label lblCheckedByID = gvDesignDetails.Rows[rowindex].FindControl("lblCheckedByID") as Label;
                Label lblClosedByID = gvDesignDetails.Rows[rowindex].FindControl("lblClosedByID") as Label;
                Label lblAmendedOpenByID = gvDesignDetails.Rows[rowindex].FindControl("lblAmendedOpenByID") as Label;
                Label lblAmendedSendToCheckingByID = gvDesignDetails.Rows[rowindex].FindControl("lblAmendedSendToCheckingByID") as Label;
                Label lblAmendedCheckedByID = gvDesignDetails.Rows[rowindex].FindControl("lblAmendedCheckedByID") as Label;

                Label lblAmendmentCount = gvDesignDetails.Rows[rowindex].FindControl("lblAmendmentCount") as Label;
                Label lblAmendmentFlag = gvDesignDetails.Rows[rowindex].FindControl("lblAmendmentFlag") as Label;
                Label lblEditedFlag = gvDesignDetails.Rows[rowindex].FindControl("lblEditedFlag") as Label;
                Label lblIsGeneratedMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsGeneratedMailSent") as Label;
                Label lblIsOpenMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsOpenMailSent ") as Label;
                Label lblIsSendToCheckingMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsSendToCheckingMailSent") as Label;
                Label lblIsCheckedMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsCheckedMailSent") as Label;
                Label lblIsClosedMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsClosedMailSent") as Label;
                Label lblIsSendToAmendmentMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsSendToAmendmentMailSent") as Label;
                Label lblIsAmendedOpenMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsAmendedOpenMailSent") as Label;
                Label lblIsAmendedSendToCheckingMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsAmendedSendToCheckingMailSent") as Label;
                Label lblIsAmendedCheckedMailSent = gvDesignDetails.Rows[rowindex].FindControl("lblIsAmendedCheckedMailSent") as Label;

                Label lblDesignResponsibleEnggID = gvDesignDetails.Rows[rowindex].FindControl("lblDesignResponsibleEnggID") as Label;
                Label lblDesignResponsibleEnggEmpRecordID = gvDesignDetails.Rows[rowindex].FindControl("lblDesignResponsibleEnggEmpRecordID") as Label;
                Label lblDesignCheckerID = gvDesignDetails.Rows[rowindex].FindControl("lblDesignCheckerID") as Label;
                Label lblDesignCheckerEmpRecordID = gvDesignDetails.Rows[rowindex].FindControl("lblDesignCheckerEmpRecordID") as Label;

                ImageButton imgBtnSendMail = (ImageButton)gvDesignDetails.Rows[rowindex].FindControl("imgBtnSendMail");
                ImageButton imgStatus = (ImageButton)gvDesignDetails.Rows[rowindex].FindControl("imgStatus");
                Button btnEdit = gvDesignDetails.Rows[rowindex].FindControl("btnEdit") as Button;
                Label lblJOBNo = gvDesignDetails.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblDescription = gvDesignDetails.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblUOM = gvDesignDetails.Rows[rowindex].FindControl("lblUOM") as Label;
                TextBox txtQuantity = gvDesignDetails.Rows[rowindex].FindControl("txtQuantity") as TextBox;
                Label lblReqdDateByProjectTeam = gvDesignDetails.Rows[rowindex].FindControl("lblReqdDateByProjectTeam") as Label;

                TextBox txtPlannedStartDateByDesignTeam = gvDesignDetails.Rows[rowindex].FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
                ImageButton imgbtnPlannedStartDateByDesignTeam = gvDesignDetails.Rows[rowindex].FindControl("imgbtnPlannedStartDateByDesignTeam") as ImageButton;

                TextBox txtPlannedCompletionDateByDesignTeam = gvDesignDetails.Rows[rowindex].FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
                ImageButton imgbtnPlannedCompletionDateByDesignTeam = gvDesignDetails.Rows[rowindex].FindControl("imgbtnPlannedCompletionDateByDesignTeam") as ImageButton;

                Label lblDrawingNo = gvDesignDetails.Rows[rowindex].FindControl("lblDrawingNo") as Label;

                Label lblClientDrawingNo = gvDesignDetails.Rows[rowindex].FindControl("lblClientDrawingNo") as Label;
                Label lblContractorDrawingNo = gvDesignDetails.Rows[rowindex].FindControl("lblContractorDrawingNo") as Label;

                Label lblDocumentLink = gvDesignDetails.Rows[rowindex].FindControl("lblDocumentLink") as Label;

                Label lblAdditionalAttachmentName = gvDesignDetails.Rows[rowindex].FindControl("lblAdditionalAttachmentName") as Label;

                TextBox txtDrawingLink = gvDesignDetails.Rows[rowindex].FindControl("txtDrawingLink") as TextBox;

                TextBox txtDrawingRevNo = gvDesignDetails.Rows[rowindex].FindControl("txtDrawingRevNo") as TextBox;
                TextBox txtWorkingStatus = gvDesignDetails.Rows[rowindex].FindControl("txtWorkingStatus") as TextBox;

                TextBox txtExpectedCompletionDate = gvDesignDetails.Rows[rowindex].FindControl("txtExpectedCompletionDate") as TextBox;
                ImageButton imgbtnExpectedCompletionDate = gvDesignDetails.Rows[rowindex].FindControl("imgbtnExpectedCompletionDate") as ImageButton;

                TextBox txtTimeSpent = gvDesignDetails.Rows[rowindex].FindControl("txtTimeSpent") as TextBox;
                DropDownList ddlResponsibleDesignEngineer = gvDesignDetails.Rows[rowindex].FindControl("ddlResponsibleDesignEngineer") as DropDownList;
                DropDownList ddlDesignChecker = gvDesignDetails.Rows[rowindex].FindControl("ddlDesignChecker") as DropDownList;
                TextBox txtRemarks = gvDesignDetails.Rows[rowindex].FindControl("txtRemarks") as TextBox;
                Button btnAddTimesheet = gvDesignDetails.Rows[rowindex].FindControl("btnAddTimesheet") as Button;
                Button btnAssign = gvDesignDetails.Rows[rowindex].FindControl("btnAssign") as Button;
                Button btnSendToChecking = gvDesignDetails.Rows[rowindex].FindControl("btnSendToChecking") as Button;
                Button btnCheck = gvDesignDetails.Rows[rowindex].FindControl("btnCheck") as Button;
                Button btnSendToAmendment = gvDesignDetails.Rows[rowindex].FindControl("btnSendToAmendment") as Button;
                Button btnSendToCorrection = gvDesignDetails.Rows[rowindex].FindControl("btnSendToCorrection") as Button;
                //Button btnClose = gvDesignDetails.Rows[rowindex].FindControl("btnClose") as Button;


                ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);
                ViewState["AMENDMENT_FLAG"] = Convert.ToInt32(lblAmendmentFlag.Text);
                ViewState["AMENDMENT_COUNT"] = Convert.ToInt32(lblAmendmentCount.Text);
                ViewState["JOB_NO"] = Convert.ToString(lblJOBNo.Text);
                ViewState["WORKING_STATUS"] = Convert.ToString(txtWorkingStatus.Text);
                ViewState["REQD_DATE"] = Convert.ToString(lblReqdDateByProjectTeam.Text);
                ViewState["EXPECTED_COMPLETION_DATE"] = Convert.ToString(txtExpectedCompletionDate.Text);
                ViewState["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(lblDesignResponsibleEnggID.Text);
                ViewState["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text).Trim().ToUpper();
                hdDrawingID.Value = Convert.ToString(lblDrawingNo.Text).Trim().ToUpper();
                drawingNo = "'" + Convert.ToString(lblDrawingNo.Text).ToUpper().Trim() + "'";




                string expetedCompletionDate = string.Empty;



                if (Convert.ToString(e.CommandArgument) == "VIEW_ADD_ATT")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblRecordID.Text), "ATTACHMENT1", Convert.ToString(lblAdditionalAttachmentName.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    mpeViewInPDF.Show();
                    iframeViewDrawingDetailsInPDF.Attributes.Add("src", "DrawingDetailInPDF.aspx?recordID=" + Convert.ToInt32(lblRecordID.Text) + "");
                }


                else if (Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                {

                    int mailTypeID = 0;
                    statusID = Convert.ToInt32(lblStatusID.Text);

                    if (Convert.ToInt32(lblAmendmentCount.Text) == 0)
                    {
                        if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                        {
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);
                        }

                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                        {

                            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                                Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
                                Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil))
                            {
                                mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AssignmentMail);
                            }
                            else
                            {
                                if (Convert.ToInt32(lblDesignResponsibleEnggID.Text) == Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]))
                                {
                                    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToHimselfMail);
                                }
                                else
                                {
                                    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToOtherMail);
                                }
                            }
                        }

                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                        {
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CheckingMail);
                        }

                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                        {
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CheckedMail);
                        }

                    }
                    else
                    {
                        if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                        {
                            if (Convert.ToInt32(ViewState["AMENDMENT_FLAG"]) > 0)
                            {
                                mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendededMail);
                            }
                            else
                            {
                                mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedGeneratedMail);
                            }
                        }

                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                        {

                            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                                Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
                                Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil))
                            {
                                mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedAssignmentMail);
                            }
                            else
                            {
                                if (Convert.ToInt32(lblEditedFlag.Text) > 0)
                                {
                                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(ViewState["DESIGN_RESPONSIBLE_ENGG_ID"]))
                                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToHimselfMail);
                                    else
                                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToOtherMail);
                                }
                                else
                                {
                                    if (Convert.ToInt32(lblDesignResponsibleEnggID.Text) == Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]))
                                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToHimselfMail);
                                    else
                                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToOtherMail);
                                }
                            }



                            if (Convert.ToInt32(ViewState["AMENDMENT_FLAG"]) > 0)
                            {
                                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(ViewState["DESIGN_RESPONSIBLE_ENGG_ID"]))
                                    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToHimselfMail);
                                else
                                    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToOtherMail);
                            }
                            else
                            {
                                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(ViewState["DESIGN_RESPONSIBLE_ENGG_ID"]))
                                    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToHimselfMail);
                                else
                                    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToOtherMail);
                            }


                        }

                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                        {
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedCheckingMail);
                        }

                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                        {
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedCheckedMail);
                        }
                    }


                    int mailSentValue = objDMSSendMail.ProcessAndSendMail(drawingNo, Convert.ToInt32(lblRecordID.Text), statusID, mailTypeID);
                    if (mailSentValue > 0)
                    {
                        int mailSentStatusValue = objProject.UpdateDesignMailStatus(drawingNo, Convert.ToInt32(lblRecordID.Text), statusID,
                                                                                    Convert.ToInt32(lblAmendmentCount.Text), Convert.ToInt32(lblAmendmentFlag.Text),
                                                                                    Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    }


                    if (mailSentValue > 0)
                    {

                        SuccessMessage("Mail sent successfully.");
                    }
                    else
                    {
                        ExceptionMessage("Please try again...!!!");
                        return;
                    }

                    GetDesignDetailList();
                }


                else if (Convert.ToString(e.CommandArgument) == "ADD_TIMESHEET")
                {
                    ResetTimesheet();

                    DataTable dtTimesheet = new DataTable();
                    if (Session["dtTimesheet"] != null)
                        dtTimesheet = (DataTable)Session["dtTimesheet"];
                    else
                    {
                        DataSet ds = new DataSet();
                        ds = objProject.GetUserLastEndTime(Convert.ToInt32(Session["EMP_RECORD_ID"]), DateTime.Now.ToString("yyyy-MM-dd"));
                        if (ds.Tables.Count > 0)
                        {
                            Session["dtTimesheet"] = ds.Tables[0];
                            dtTimesheet = ds.Tables[0];
                        }
                        else
                        {
                            Session["dtTimesheet"] = null;
                        }
                    }
                    DataTable dtnew = new DataTable();

                    if (dtTimesheet.Rows.Count > 0)
                    {
                        dtnew.Columns.Add("ENTRY_DATE", typeof(string));
                        dtnew.Columns.Add("START_TIME", typeof(string));
                        dtnew.Columns.Add("END_TIME", typeof(string));
                        dtnew.Columns.Add("REMARKS", typeof(string));

                        foreach (DataRow dr in dtTimesheet.Select("EMP_RECORD_ID='" + Convert.ToInt32(lblDesignResponsibleEnggEmpRecordID.Text) + "' AND ENTRY_DATE='" + DateTime.Now.ToString("dd-MMM-yyyy") + "'"))
                        {
                            DataRow drn = dtnew.NewRow();
                            drn["ENTRY_DATE"] = dr["ENTRY_DATE"];
                            drn["START_TIME"] = dr["START_TIME"];
                            drn["END_TIME"] = dr["END_TIME"];
                            drn["REMARKS"] = dr["REMARKS"];
                            dtnew.Rows.Add(drn);
                        }

                        if (dtnew.Rows.Count > 0)
                        {
                            gvTimesheet.DataSource = dtnew;
                            gvTimesheet.DataBind();
                        }
                        else
                        {
                            gvTimesheet.DataSource = null;
                            gvTimesheet.DataBind();
                        }
                    }
                    else
                    {
                        gvTimesheet.DataSource = null;
                        gvTimesheet.DataBind();
                    }

                    lblTimehseetRerocds.Text = "Records[" + dtnew.Rows.Count + "]";

                    if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text)))
                    {
                        string txt = Convert.ToString(lblJOBNo.Text).Substring(0, 2);
                        dvDrawing.Style.Add("display", "none");
                        //if (txt == "OS" || txt == "OC" || txt == "OP" || txt == "OM" || txt == "OE")
                        //{
                        //    dvDrawing.Style.Add("display", "none");
                        //}
                        //else
                        //{
                        //    txtJOBNoInTimesheet.Text = Convert.ToString(lblJOBNo.Text);
                        //    dvDrawing.Style.Add("display", "block");
                        //}
                    }

                    bool check = true;

                    if (string.IsNullOrEmpty(txtWorkingStatus.Text))
                    {
                        txtWorkingStatus.Focus();
                        txtWorkingStatus.BackColor = System.Drawing.Color.LightPink;
                        ExceptionMessage("Please enter working status...!!!");
                        check = false;
                    }
                    else
                    {
                        txtWorkingStatus.BackColor = System.Drawing.Color.Transparent;
                    }


                    if (string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                    {
                        expetedCompletionDate = string.Empty;
                        txtExpectedCompletionDate.Focus();
                        txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightPink;
                        ExceptionMessage("Please enter expected completion date...!!!");
                        check = false;
                    }
                    else
                    {
                        expetedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                        txtExpectedCompletionDate.BackColor = System.Drawing.Color.White;
                    }


                    if (check)
                    {
                        mpeAddTimesheet.Show();
                    }

                    //if (!string.IsNullOrEmpty(txtWorkingStatus.Text))
                    //{
                    //    txtWorkingStatus.BackColor = System.Drawing.Color.Transparent;
                    //    mpeAddTimesheet.Show();
                    //}
                    //else
                    //{
                    //    txtWorkingStatus.Focus();
                    //    txtWorkingStatus.BackColor = System.Drawing.Color.LightPink;
                    //    ExceptionMessage("Please enter working status...!!!");
                    //    return;
                    //}


                    //if (string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                    //{
                    //    expetedCompletionDate = string.Empty;
                    //    txtExpectedCompletionDate.Focus();
                    //    txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightPink;                        
                    //    return;
                    //}
                    //else
                    //{
                    //    expetedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                    //    txtExpectedCompletionDate.BackColor = System.Drawing.Color.White;
                    //}

                }


                else if (Convert.ToString(e.CommandArgument) == "EDIT")
                {

                    ViewState["DRAWING_ID"] = Convert.ToInt32(lblDrawingID.Text);

                    ddlCompanyToEdit.SelectedValue = Convert.ToString(lblJOBUnitID.Text);

                    if (!string.IsNullOrEmpty(lblJOBNo.Text))
                        txtJOBNoToEdit.Text = lblJOBNo.Text.ToUpper();
                    else
                        txtJOBNoToEdit.Text = string.Empty;



                    BindDesignCategoryToEdit();
                    if (Convert.ToInt32(lblCategoryID.Text) > 0)
                        ddlCategoryToEdit.SelectedValue = Convert.ToString(lblCategoryID.Text);
                    else
                        ddlCategoryToEdit.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(lblDescription.Text))
                        txtDescriptionToEdit.Text = lblDescription.Text;
                    else
                        txtDescriptionToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblUOM.Text))
                        txtUOMToEdit.Text = lblUOM.Text.ToUpper().Trim();
                    else
                        txtUOMToEdit.Text = string.Empty;

                    if (Convert.ToInt32(txtQuantity.Text) > 0)
                        txtQuantityToEdit.Text = Convert.ToString(txtQuantity.Text);
                    else
                        txtQuantityToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblReqdDateByProjectTeam.Text))
                    {
                        hdDateByProjectTeamToEdit.Value = lblReqdDateByProjectTeam.Text;
                        txtDateByProjectTeamToEdit.Text = lblReqdDateByProjectTeam.Text;
                    }
                    else
                    {
                        hdDateByProjectTeamToEdit.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                        txtDateByProjectTeamToEdit.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }

                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                    {
                        txtDrawingNumberToEdit.Enabled = true;
                    }
                    else
                    {
                        txtDrawingNumberToEdit.Enabled = false;
                    }

                    if (!string.IsNullOrEmpty(lblDrawingNo.Text))
                        txtDrawingNumberToEdit.Text = lblDrawingNo.Text;
                    else
                        txtDrawingNumberToEdit.Text = string.Empty;




                    if (!string.IsNullOrEmpty(lblClientDrawingNo.Text))
                        txtClientDrawingNumberToEdit.Text = lblClientDrawingNo.Text;
                    else
                        txtClientDrawingNumberToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblContractorDrawingNo.Text))
                        txtContractorDrawingNumberToEdit.Text = lblContractorDrawingNo.Text;
                    else
                        txtContractorDrawingNumberToEdit.Text = string.Empty;



                    if (!string.IsNullOrEmpty(lblDocumentLink.Text))
                        txtDocumentLinkToEdit.Text = lblDocumentLink.Text.Trim();
                    else
                        txtDocumentLinkToEdit.Text = string.Empty;

                    ddlDrawingRevisioinNumberToEdit.SelectedValue = Convert.ToString(txtDrawingRevNo.Text);

                    if (!string.IsNullOrEmpty(txtWorkingStatus.Text))
                        txtWorkingStatusToEdit.Text = txtWorkingStatus.Text.Trim();
                    else
                        txtWorkingStatusToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(txtPlannedStartDateByDesignTeam.Text))
                    {
                        hdPlannedStartDateByDesignTeamToEdit.Value = Convert.ToString(txtPlannedStartDateByDesignTeam.Text);
                        txtPlannedStartDateByDesignTeamToEdit.Text = Convert.ToString(txtPlannedStartDateByDesignTeam.Text);
                    }
                    else
                    {
                        hdPlannedStartDateByDesignTeamToEdit.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                        txtPlannedStartDateByDesignTeamToEdit.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }

                    if (!string.IsNullOrEmpty(txtPlannedCompletionDateByDesignTeam.Text))
                    {
                        hdPlannedCompletionDateByDesignTeamToEdit.Value = Convert.ToString(txtPlannedStartDateByDesignTeam.Text);
                        txtPlannedCompletionDateByDesignTeamToEdit.Text = Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text);
                    }
                    else
                    {
                        hdPlannedCompletionDateByDesignTeamToEdit.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                        txtPlannedCompletionDateByDesignTeamToEdit.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }

                    if (!string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                    {
                        hdExpectedCompletionDateToEdit.Value = Convert.ToString(txtExpectedCompletionDate.Text);
                        txtExpectedCompletionDateToEdit.Text = Convert.ToString(txtExpectedCompletionDate.Text);
                    }
                    else
                    {
                        hdExpectedCompletionDateToEdit.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                        txtExpectedCompletionDateToEdit.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }


                    BindRespinsibleEngToEdit();
                    if (Convert.ToInt32(lblDesignResponsibleEnggID.Text) > 0)
                        ddlRespDesignEnggToEdit.SelectedValue = Convert.ToString(lblDesignResponsibleEnggID.Text);
                    else
                        ddlRespDesignEnggToEdit.SelectedIndex = 0;


                    txtRemarksToEdit.Text = string.Empty;

                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                    {
                        pnlDesign.Visible = true;
                    }
                    else
                    {
                        pnlDesign.Visible = false;
                    }

                    mpeDesignDetails.Show();

                }


                else if (Convert.ToString(e.CommandArgument) == "ASSIGN")
                {
                    if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
                    {
                        ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.White;
                        statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);

                        if (!string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                            expetedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                        else
                            expetedCompletionDate = string.Empty;

                        UpdateStatus(Convert.ToInt32(lblRecordID.Text), drawingNo, statusID, expetedCompletionDate,
                                    Convert.ToString(txtPlannedStartDateByDesignTeam.Text), Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text), Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue),
                                   "", 0, Convert.ToString(txtRemarks.Text), Convert.ToInt32(lblAmendmentCount.Text), 0);
                    }
                    else
                    {
                        ddlResponsibleDesignEngineer.Focus();
                        ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.LightPink;
                        ExceptionMessage("Please select responsible design engineer...!!!");
                        return;
                    }
                }


                else if (Convert.ToString(e.CommandArgument) == "SEND_TO_CHECKING")
                {
                    extendedRecordIDs = string.Empty;

                    bool check = true;

                    if (string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                    {
                        expetedCompletionDate = string.Empty;
                        txtExpectedCompletionDate.Focus();
                        txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                    }
                    else
                    {
                        expetedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                        txtExpectedCompletionDate.BackColor = System.Drawing.Color.White;
                    }

                    if (string.IsNullOrEmpty(txtDrawingLink.Text))
                    {
                        txtDrawingLink.Focus();
                        txtDrawingLink.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                    }
                    else
                    {
                        txtDrawingLink.BackColor = System.Drawing.Color.White;
                    }


                    if (string.IsNullOrEmpty(txtTimeSpent.Text))
                    {
                        txtTimeSpent.Focus();
                        txtTimeSpent.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                    }
                    else
                    {
                        txtTimeSpent.BackColor = System.Drawing.Color.Transparent;
                    }

                    if (ddlDesignChecker.SelectedIndex == 0)
                    {
                        ddlDesignChecker.Focus();
                        ddlDesignChecker.BackColor = System.Drawing.Color.LightPink;
                        check = false;
                    }
                    else
                    {
                        ddlDesignChecker.BackColor = System.Drawing.Color.White;
                    }




                    //if (ddlDesignChecker.SelectedIndex > 0)
                    if (check)
                    {
                        ddlDesignChecker.BackColor = System.Drawing.Color.White;
                        statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking);

                        int val = UpdateStatus(Convert.ToInt32(lblRecordID.Text), drawingNo, statusID, expetedCompletionDate,
                                              Convert.ToString(txtPlannedStartDateByDesignTeam.Text),
                                              Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text), Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue),
                                              Convert.ToString(txtDrawingLink.Text), Convert.ToInt32(ddlDesignChecker.SelectedValue), Convert.ToString(txtRemarks.Text),
                                              Convert.ToInt32(lblAmendmentCount.Text), 0);



                        //if (val > 0)
                        //{
                        //    extendedRecordIDs = Convert.ToString(lblRecordID.Text);

                        //    GetExtendedDrawingList(extendedRecordIDs);
                        //}
                    }
                    //else
                    //{
                    //    ddlDesignChecker.Focus();
                    //    ddlDesignChecker.BackColor = System.Drawing.Color.LightPink;
                    //    ExceptionMessage("Please select design checker...!!!");
                    //    return;
                    //}
                }


                else if (Convert.ToString(e.CommandArgument) == "CHECK")
                {
                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked);

                    if (!string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                        expetedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                    else
                        expetedCompletionDate = string.Empty;

                    UpdateStatus(Convert.ToInt32(lblRecordID.Text), drawingNo, statusID, expetedCompletionDate,
                                 "", "", 0, "", 0, Convert.ToString(txtRemarks.Text),
                                 Convert.ToInt32(lblAmendmentCount.Text), 0);
                }


                else if (Convert.ToString(e.CommandArgument) == "SEND_TO_AMENDMENT")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                    {
                        txtRemarks.BackColor = System.Drawing.Color.White;
                        statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);

                        if (!string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                            expetedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                        else
                            expetedCompletionDate = string.Empty;

                        UpdateStatus(Convert.ToInt32(lblRecordID.Text), drawingNo, statusID, expetedCompletionDate,
                                     "", "",
                                     Convert.ToInt32(lblDesignResponsibleEnggID.Text),
                                     "", 0, Convert.ToString(txtRemarks.Text),
                                     (Convert.ToInt32(lblAmendmentCount.Text) + 1), Convert.ToInt32(DMSAllStatusAndTypes.AmendmentType.Amendment));
                    }
                    else
                    {
                        txtRemarks.Focus();
                        txtRemarks.BackColor = System.Drawing.Color.LightPink;
                        ExceptionMessage("Please enter amendment remarks...!!!");
                        return;
                    }





                }


                else if (Convert.ToString(e.CommandArgument) == "SEND_TO_CORRECTION")
                {
                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);

                    if (!string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                        expetedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                    else
                        expetedCompletionDate = string.Empty;

                    if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
                    {
                        ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.White;
                        responsibleDesignEnggID = Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue);
                    }
                    else
                    {
                        ddlResponsibleDesignEngineer.Focus();
                        ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.LightPink;
                        responsibleDesignEnggID = 0;
                        ExceptionMessage("Please select responsible design engineer...!!!");
                        return;
                    }


                    if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                    {
                        txtRemarks.BackColor = System.Drawing.Color.White;
                        remarks = Convert.ToString(txtRemarks.Text);
                    }
                    else
                    {
                        txtRemarks.Focus();
                        txtRemarks.BackColor = System.Drawing.Color.LightPink;
                        remarks = string.Empty;
                        ExceptionMessage("Please amendment remarks...!!!");
                        return;
                    }


                    UpdateStatus(Convert.ToInt32(lblRecordID.Text), drawingNo, statusID, expetedCompletionDate,
                                     Convert.ToString(txtPlannedStartDateByDesignTeam.Text),
                                     Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text), responsibleDesignEnggID,
                                     "", 0, remarks,
                                     (Convert.ToInt32(lblAmendmentCount.Text) + 1), Convert.ToInt32(DMSAllStatusAndTypes.AmendmentType.Correction));

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

    protected void gvDesignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtSrNo = e.Row.FindControl("txtSrNo") as TextBox;
                Button btnViewDetail = e.Row.FindControl("btnViewDetail") as Button;
                Label lblStatusID = e.Row.FindControl("lblStatusID") as Label;

                Label lblPMID = e.Row.FindControl("lblPMID") as Label;

                Label lblRecordID = e.Row.FindControl("lblRecordID") as Label;
                Label lblIsRevised = e.Row.FindControl("lblIsRevised") as Label;
                Label lblCreatedByID = e.Row.FindControl("lblCreatedByID") as Label;
                Label lblOpenByID = e.Row.FindControl("lblOpenByID") as Label;
                Label lblSendToCheckingByID = e.Row.FindControl("lblSendToCheckingByID") as Label;
                Label lblCheckedByID = e.Row.FindControl("lblCheckedByID") as Label;
                Label lblClosedByID = e.Row.FindControl("lblClosedByID") as Label;
                Label lblAmendedOpenByID = e.Row.FindControl("lblAmendedOpenByID") as Label;
                Label lblAmendedSendToCheckingByID = e.Row.FindControl("lblAmendedSendToCheckingByID") as Label;
                Label lblAmendedCheckedByID = e.Row.FindControl("lblAmendedCheckedByID") as Label;

                Label lblSentToAmendmentByID = e.Row.FindControl("lblSentToAmendmentByID") as Label;
                Label lblAmendmentCount = e.Row.FindControl("lblAmendmentCount") as Label;
                Label lblAmendmentFlag = e.Row.FindControl("lblAmendmentFlag") as Label;
                Label lblCorrectionCount = e.Row.FindControl("lblCorrectionCount") as Label;

                Label lblIsGeneratedMailSent = e.Row.FindControl("lblIsGeneratedMailSent") as Label;
                Label lblIsOpenMailSent = e.Row.FindControl("lblIsOpenMailSent") as Label;
                Label lblIsSendToCheckingMailSent = e.Row.FindControl("lblIsSendToCheckingMailSent") as Label;
                Label lblIsCheckedMailSent = e.Row.FindControl("lblIsCheckedMailSent") as Label;
                Label lblIsClosedMailSent = e.Row.FindControl("lblIsClosedMailSent") as Label;

                Label lblIsSendToAmendmentMailSent = e.Row.FindControl("lblIsSendToAmendmentMailSent") as Label;

                Label lblIsAmendedMailSent = e.Row.FindControl("lblIsAmendedMailSent") as Label;
                Label lblIsAmendedOpenMailSent = e.Row.FindControl("lblIsAmendedOpenMailSent") as Label;
                Label lblIsAmendedSendToCheckingMailSent = e.Row.FindControl("lblIsAmendedSendToCheckingMailSent") as Label;
                Label lblIsAmendedCheckedMailSent = e.Row.FindControl("lblIsAmendedCheckedMailSent") as Label;

                Label lblDesignResponsibleEnggID = e.Row.FindControl("lblDesignResponsibleEnggID") as Label;
                Label lblDesignResponsibleEnggEmpRecordID = e.Row.FindControl("lblDesignResponsibleEnggEmpRecordID") as Label;
                Label lblDesignCheckerID = e.Row.FindControl("lblDesignCheckerID") as Label;
                Label lblDesignCheckerEmpRecordID = e.Row.FindControl("lblDesignCheckerEmpRecordID") as Label;

                ImageButton imgBtnSendMail = (ImageButton)e.Row.FindControl("imgBtnSendMail");
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                Button btnEdit = e.Row.FindControl("btnEdit") as Button;
                Label lblJOBNo = e.Row.FindControl("lblJOBNo") as Label;
                Label lblDescription = e.Row.FindControl("lblDescription") as Label;
                Label lblUOM = e.Row.FindControl("lblUOM") as Label;
                TextBox txtQuantity = e.Row.FindControl("txtQuantity") as TextBox;
                Label lblReqdDateByProjectTeam = e.Row.FindControl("lblReqdDateByProjectTeam") as Label;

                TextBox txtPlannedStartDateByDesignTeam = e.Row.FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
                ImageButton imgbtnPlannedStartDateByDesignTeam = e.Row.FindControl("imgbtnPlannedStartDateByDesignTeam") as ImageButton;

                TextBox txtPlannedCompletionDateByDesignTeam = e.Row.FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
                ImageButton imgbtnPlannedCompletionDateByDesignTeam = e.Row.FindControl("imgbtnPlannedCompletionDateByDesignTeam") as ImageButton;

                Label lblDrawingNo = e.Row.FindControl("lblDrawingNo") as Label;
                Label lblDocumentLink = e.Row.FindControl("lblDocumentLink") as Label;
                Label lblAdditionalAttachmentName = e.Row.FindControl("lblAdditionalAttachmentName") as Label;

                TextBox txtDrawingLink = e.Row.FindControl("txtDrawingLink") as TextBox;

                TextBox txtDrawingRevNo = e.Row.FindControl("txtDrawingRevNo") as TextBox;
                TextBox txtWorkingStatus = e.Row.FindControl("txtWorkingStatus") as TextBox;

                TextBox txtExpectedCompletionDate = e.Row.FindControl("txtExpectedCompletionDate") as TextBox;
                ImageButton imgbtnExpectedCompletionDate = e.Row.FindControl("imgbtnExpectedCompletionDate") as ImageButton;

                TextBox txtTimeSpent = e.Row.FindControl("txtTimeSpent") as TextBox;
                DropDownList ddlResponsibleDesignEngineer = e.Row.FindControl("ddlResponsibleDesignEngineer") as DropDownList;
                DropDownList ddlDesignChecker = e.Row.FindControl("ddlDesignChecker") as DropDownList;
                TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;
                Button btnAddTimesheet = e.Row.FindControl("btnAddTimesheet") as Button;
                Button btnAssign = e.Row.FindControl("btnAssign") as Button;
                Button btnSendToChecking = e.Row.FindControl("btnSendToChecking") as Button;
                Button btnCheck = e.Row.FindControl("btnCheck") as Button;
                Button btnSendToAmendment = e.Row.FindControl("btnSendToAmendment") as Button;
                Button btnSendToCorrection = e.Row.FindControl("btnSendToCorrection") as Button;
                //Button btnClose = e.Row.FindControl("btnClose") as Button;

                ImageButton btnViewAddAtt = e.Row.FindControl("btnViewAddAtt") as ImageButton;
                btnViewAddAtt.Visible = false;


                DataTable dt = new DataTable();
                if (Session["dtDesignDetailsList"] != null)
                    dt = (DataTable)Session["dtDesignDetailsList"];


                dsDesignEngg = objProject.GetDesignResponsibleEngg();
                if (dsDesignEngg.Tables.Count > 0 && dsDesignEngg.Tables[0].Rows.Count > 0)
                {
                    ddlResponsibleDesignEngineer.DataSource = dsDesignEngg.Tables[0];
                    ddlResponsibleDesignEngineer.DataTextField = "DESIGN_RESPONSIBLE_ENGG";
                    ddlResponsibleDesignEngineer.DataValueField = "DESIGN_RESPONSIBLE_ENGG_ID";
                    ddlResponsibleDesignEngineer.DataBind();
                    ddlResponsibleDesignEngineer.Items.Insert(0, "Select");
                }


                dsDesignChecker = objProject.GetDesignChecker();
                if (dsDesignChecker.Tables.Count > 0 && dsDesignChecker.Tables[0].Rows.Count > 0)
                {
                    ddlDesignChecker.DataSource = dsDesignChecker.Tables[0];
                    ddlDesignChecker.DataTextField = "DESIGN_CHECKER";
                    ddlDesignChecker.DataValueField = "DESIGN_CHECKER_ID";
                    ddlDesignChecker.DataBind();
                    ddlDesignChecker.Items.Insert(0, "Select");
                }


                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Select("SR_NO='" + txtSrNo.Text + "'"))
                    {
                        if (Convert.ToInt32(dr["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                            ddlResponsibleDesignEngineer.SelectedValue = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);
                        else
                            ddlResponsibleDesignEngineer.SelectedIndex = 0;

                        ddlDesignChecker.SelectedValue = Convert.ToString(dr["DESIGN_CHECKER_ID"]);
                    }
                }

                imgbtnPlannedStartDateByDesignTeam.Visible = false;
                imgbtnPlannedCompletionDateByDesignTeam.Visible = false;
                txtWorkingStatus.Enabled = false;
                imgbtnExpectedCompletionDate.Visible = false;
                txtTimeSpent.Enabled = false;
                txtDrawingLink.Enabled = false;


                imgBtnSendMail.Visible = false;
                imgStatus.Enabled = false;
                btnEdit.Visible = false;
                btnAddTimesheet.Visible = false;
                btnAssign.Visible = false;
                btnSendToChecking.Visible = false;
                btnCheck.Visible = false;
                btnSendToAmendment.Visible = false;
                btnSendToCorrection.Visible = false;
                //btnClose.Visible = false;

                ddlResponsibleDesignEngineer.Enabled = false;
                ddlDesignChecker.Enabled = false;

                txtQuantity.Enabled = false;
                txtDrawingRevNo.Enabled = false;


                //txtPlannedStartDateByDesignTeam.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtPlannedCompletionDateByDesignTeam.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtExpectedCompletionDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");


                if (!string.IsNullOrEmpty(Convert.ToString(lblAdditionalAttachmentName.Text)))
                {
                    btnViewAddAtt.Visible = true;
                }


                //Generated
                if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                {
                    if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/rednew.png";
                        imgStatus.ToolTip = "Amendment: Created by project/any user";
                    }
                    else
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/New05.png";
                        imgStatus.ToolTip = "Generated: Created by project/any user";
                    }

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblCreatedByID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblPMID.Text))
                    {
                        if (Convert.ToInt32(lblAmendmentCount.Text) > 0 && Convert.ToInt32(lblAmendmentFlag.Text) == 0)
                        {
                            if (Convert.ToInt32(lblIsAmendedMailSent.Text) == 0)
                                imgBtnSendMail.Visible = true;
                        }
                        else
                        {
                            if (Convert.ToInt32(lblIsGeneratedMailSent.Text) == 0)
                                imgBtnSendMail.Visible = true;
                        }

                        btnEdit.Visible = true;

                        if (Convert.ToInt32(lblAmendmentFlag.Text) > 0)
                        {
                            btnEdit.Text = "Amend";
                        }
                    }

                    txtPlannedStartDateByDesignTeam.Width = Unit.Percentage(100);
                    txtPlannedCompletionDateByDesignTeam.Width = Unit.Percentage(100);
                    txtExpectedCompletionDate.Width = Unit.Percentage(100);

                    txtPlannedStartDateByDesignTeam.Enabled = false;
                    txtPlannedCompletionDateByDesignTeam.Enabled = false;
                    txtExpectedCompletionDate.Enabled = false;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder))
                    {
                        if (Convert.ToInt32(lblAmendmentFlag.Text) == 0)
                        {
                            ddlResponsibleDesignEngineer.Enabled = true;
                            btnAssign.Visible = true;

                            txtPlannedStartDateByDesignTeam.Enabled = true;
                            txtPlannedCompletionDateByDesignTeam.Enabled = true;

                            txtPlannedStartDateByDesignTeam.Width = Unit.Percentage(80);
                            txtPlannedCompletionDateByDesignTeam.Width = Unit.Percentage(80);

                            imgbtnPlannedStartDateByDesignTeam.Visible = true;
                            imgbtnPlannedCompletionDateByDesignTeam.Visible = true;
                        }
                    }
                }


                //Open
                else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/open1.png";
                        imgStatus.ToolTip = "Amended: Open for design engineer";
                    }
                    else
                    {
                        imgStatus.ImageUrl = "~/Images/NEWICONS/blackopen.png";
                        imgStatus.ToolTip = "Open: Open for design engineer";
                    }

                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(lblDesignResponsibleEnggID.Text))
                    {
                        txtPlannedStartDateByDesignTeam.Width = Unit.Percentage(100);
                        txtPlannedCompletionDateByDesignTeam.Width = Unit.Percentage(100);
                        txtExpectedCompletionDate.Width = Unit.Percentage(80);

                        txtPlannedStartDateByDesignTeam.Enabled = false;
                        txtPlannedCompletionDateByDesignTeam.Enabled = false;
                        txtExpectedCompletionDate.Enabled = true;

                        txtDrawingLink.Enabled = true;

                        txtWorkingStatus.Enabled = true;
                        imgbtnExpectedCompletionDate.Visible = true;
                        btnAddTimesheet.Visible = true;
                        ddlDesignChecker.Enabled = true;
                        btnSendToChecking.Visible = true;


                        if (Convert.ToInt32(lblCreatedByID.Text) != Convert.ToInt32(lblDesignResponsibleEnggEmpRecordID.Text))
                        {
                            btnSendToAmendment.Visible = true;
                        }

                        if (Convert.ToInt32(lblCreatedByID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                        {
                            btnEdit.Visible = true;

                            if (Convert.ToInt32(lblAmendmentFlag.Text) > 0)
                            {
                                btnEdit.Text = "Amend";
                            }

                            if (Convert.ToInt32(lblAmendmentCount.Text) > 0 && Convert.ToInt32(lblAmendmentFlag.Text) == 0)
                            {
                                if (Convert.ToInt32(lblIsAmendedOpenMailSent.Text) == 0)
                                    imgBtnSendMail.Visible = true;
                            }
                            else
                            {
                                if (Convert.ToInt32(lblIsOpenMailSent.Text) == 0)
                                    imgBtnSendMail.Visible = true;
                            }
                        }
                    }

                    if ((Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder)) &&
                        Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) != Convert.ToInt32(lblDesignResponsibleEnggID.Text))
                    {

                        if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi))
                        {
                            if (Convert.ToInt32(lblAmendmentFlag.Text) == 0)
                            {
                                ddlResponsibleDesignEngineer.Enabled = true;
                                btnAssign.Visible = true;
                                btnAssign.Text = "Re-Assign";

                                txtPlannedStartDateByDesignTeam.Enabled = true;
                                txtPlannedCompletionDateByDesignTeam.Enabled = true;

                                txtPlannedStartDateByDesignTeam.Width = Unit.Percentage(80);
                                txtPlannedCompletionDateByDesignTeam.Width = Unit.Percentage(80);

                                imgbtnPlannedStartDateByDesignTeam.Visible = true;
                                imgbtnPlannedCompletionDateByDesignTeam.Visible = true;
                            }
                        }





                        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        {
                            if (Convert.ToInt32(lblAmendedOpenByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                                Convert.ToInt32(lblAmendedOpenByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                                Convert.ToInt32(lblAmendedOpenByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder))
                            {
                                if (Convert.ToInt32(lblIsAmendedOpenMailSent.Text) == 0)
                                    imgBtnSendMail.Visible = true;
                            }
                            else
                            {
                                if (Convert.ToInt32(lblSentToAmendmentByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                                    Convert.ToInt32(lblSentToAmendmentByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                                    Convert.ToInt32(lblSentToAmendmentByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder))
                                {
                                    if (Convert.ToInt32(lblIsSendToAmendmentMailSent.Text) == 0)
                                        imgBtnSendMail.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(lblOpenByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                                Convert.ToInt32(lblOpenByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                                Convert.ToInt32(lblOpenByID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder))
                            {
                                if (Convert.ToInt32(lblIsOpenMailSent.Text) == 0)
                                    imgBtnSendMail.Visible = true;
                            }
                        }


                        txtPlannedStartDateByDesignTeam.Width = Unit.Percentage(100);
                        txtPlannedCompletionDateByDesignTeam.Width = Unit.Percentage(100);
                        txtExpectedCompletionDate.Width = Unit.Percentage(100);

                        txtPlannedStartDateByDesignTeam.Enabled = false;
                        txtPlannedCompletionDateByDesignTeam.Enabled = false;
                        txtExpectedCompletionDate.Enabled = false;

                        txtDrawingLink.Enabled = true;

                        imgbtnPlannedStartDateByDesignTeam.Visible = false;
                        imgbtnPlannedCompletionDateByDesignTeam.Visible = false;
                        //txtWorkingStatus.Enabled = false;
                        imgbtnExpectedCompletionDate.Visible = false;
                        txtTimeSpent.Enabled = false;
                    }


                }


                //Checking
                else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                {
                    imgStatus.ImageUrl = "~/Images/LOT/edit5.png";
                    imgStatus.ToolTip = "Checking : Drawing sent for checking";


                    txtPlannedStartDateByDesignTeam.Width = Unit.Percentage(100);
                    txtPlannedCompletionDateByDesignTeam.Width = Unit.Percentage(100);
                    txtExpectedCompletionDate.Width = Unit.Percentage(100);

                    txtPlannedStartDateByDesignTeam.Enabled = false;
                    txtPlannedCompletionDateByDesignTeam.Enabled = false;
                    txtExpectedCompletionDate.Enabled = false;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblSendToCheckingByID.Text))
                    {
                        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        {
                            if (Convert.ToInt32(lblIsAmendedSendToCheckingMailSent.Text) == 0)
                                imgBtnSendMail.Visible = true;
                        }
                        else
                        {
                            if (Convert.ToInt32(lblIsSendToCheckingMailSent.Text) == 0)
                                imgBtnSendMail.Visible = true;
                        }
                    }

                    if (Convert.ToInt32(Session["DESIGN_CHECKER_ID"]) == Convert.ToInt32(lblDesignCheckerID.Text))
                    {
                        ddlResponsibleDesignEngineer.Enabled = true;
                        btnCheck.Visible = true;
                        btnSendToCorrection.Visible = true;
                    }
                }


                //Checked
                else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                {
                    imgStatus.ImageUrl = "~/Images/Icons/yes3.png";
                    imgStatus.ToolTip = "Checked : Drawing checked";

                    txtPlannedStartDateByDesignTeam.Width = Unit.Percentage(100);
                    txtPlannedCompletionDateByDesignTeam.Width = Unit.Percentage(100);
                    txtExpectedCompletionDate.Width = Unit.Percentage(100);

                    txtPlannedStartDateByDesignTeam.Enabled = false;
                    txtPlannedCompletionDateByDesignTeam.Enabled = false;
                    txtExpectedCompletionDate.Enabled = false;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblCheckedByID.Text))
                    {
                        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        {
                            if (Convert.ToInt32(lblIsAmendedCheckedMailSent.Text) == 0)
                                imgBtnSendMail.Visible = true;
                        }
                        else
                        {
                            if (Convert.ToInt32(lblIsCheckedMailSent.Text) == 0)
                                imgBtnSendMail.Visible = true;
                        }
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



    protected void ddlCompanyToEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }

    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
            Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
            Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
            Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
        {
            mpeJOBDetail.Show();
            GetDesignJOBDetail();
        }
        else
        {
            mpeJOBDetail.Show();
            GetProjectJOBDetail();
        }

        //if (ddlRespDesignEngg.SelectedIndex > 0)
        //{
        //    DataTable dt = new DataTable();
        //    if (Session["dtEmp"] != null)
        //    {
        //        dt = (DataTable)Session["dtEmp"];
        //    }

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(ddlRespDesignEngg.SelectedValue) + "'"))
        //        {
        //            if (Convert.ToInt32(dr["RESPONSIBLE_TYPE_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.RespEnggType.Design))
        //            {
        //                mpeJOBDetail.Show();
        //                GetDesignJOBDetail();
        //            }
        //            else
        //            {
        //                mpeProjectJOBDetail.Show();
        //                GetProjectJOBDetail();
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    mpeJOBDetail.Show();
        //    GetDesignJOBDetail();
        //}
    }

    protected void btnGetJOBNoToEdit_Click(object sender, EventArgs e)
    {
        txtDrawingNumberToEdit.Text = string.Empty;
        if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
            Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
            Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
            Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
        {
            mpeDesignDetails.Show();
            mpeJOBDetail.Show();
            GetDesignJOBDetail();
        }
        else
        {
            mpeDesignDetails.Show();
            mpeJOBDetail.Show();
            GetProjectJOBDetail();
        }
    }

    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
        {
            mpeJOBDetail.Show();
            GetDesignJOBDetail();
        }
        else
        {
            mpeJOBDetail.Show();
            GetProjectJOBDetail();
        }
    }

    protected void gvJOBDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblJOBUnitIDInJOBList = gvJOBDetail.Rows[rowindex].FindControl("lblJOBUnitIDInJOBList") as Label;
                Label lblJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNo") as Label;

                ddlCompanyToEdit.SelectedValue = Convert.ToString(lblJOBUnitIDInJOBList.Text);
                txtJOBNoToEdit.Text = Convert.ToString(lblJOBNo.Text).Trim();

                mpeDesignDetails.Show();
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




    protected void btnGetDrawingDetailsToEdit_Click(object sender, EventArgs e)
    {
        mpeDrawingDetail.Show();
        GetDrawingDetail();
        mpeDesignDetails.Show();
    }

    protected void gvDrawingDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblDrawingID = gvDrawingDetail.Rows[rowindex].FindControl("lblDrawingID") as Label;
                Label lblDrawingNo = gvDrawingDetail.Rows[rowindex].FindControl("lblDrawingNo") as Label;
                Label lblDescription = gvDrawingDetail.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblQuantity = gvDrawingDetail.Rows[rowindex].FindControl("lblQuantity") as Label;
                Label lblUOM = gvDrawingDetail.Rows[rowindex].FindControl("lblUOM") as Label;
                Label lblReqdDateByProjectTeam = gvDrawingDetail.Rows[rowindex].FindControl("lblReqdDateByProjectTeam") as Label;

                ViewState["DRAWING_ID"] = Convert.ToInt32(lblDrawingID.Text);
                txtDrawingNumberToEdit.Text = Convert.ToString(lblDrawingNo.Text).Trim();
                txtDescriptionToEdit.Text = Convert.ToString(lblDescription.Text).Trim();
                txtQuantityToEdit.Text = Convert.ToString(lblQuantity.Text).Trim();
                txtUOMToEdit.Text = Convert.ToString(lblUOM.Text).Trim();
                hdDateByProjectTeamToEdit.Value = Convert.ToString(lblReqdDateByProjectTeam.Text);
                txtDateByProjectTeamToEdit.Text = Convert.ToString(lblReqdDateByProjectTeam.Text);

                mpeDesignDetails.Show();
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






    protected void btnGetTimesheetJOBNo_Click(object sender, EventArgs e)
    {
        mpeJOBDetail.Show();
        GetDesignJOBDetail();
        txtDrawingIDInTimesheet.Text = hdDrawingID.Value;
    }

    protected void btnSubmitTimesheet_Click(object sender, EventArgs e)
    {
        SaveTimesheet();
        GetDesignDetailList();
    }


    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/AddDesign.aspx");
    }


    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        if (ddlUpdateStatus.SelectedIndex > 0)
        {
            UpdateStatusAll();
            GetDesignDetailList();
        }
        else
        {
            ExceptionMessage("Please select status...!!!");
            return;
        }
    }



    protected void btnUpdateDesignDetails_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            SaveDesignDetails();
        }
    }


    #endregion


    #region METHODS[=========================]

    private void BindCompany()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                Session["dtUnitList"] = dsUnit.Tables[0];
                ddlCompanySearch.DataSource = dsUnit.Tables[0];
                ddlCompanySearch.DataTextField = "UNIT_NAME";
                ddlCompanySearch.DataValueField = "UNIT_ID";
                ddlCompanySearch.DataBind();

                ddlCompanyToEdit.DataSource = dsUnit.Tables[0];
                ddlCompanyToEdit.DataTextField = "UNIT_NAME";
                ddlCompanyToEdit.DataValueField = "UNIT_ID";
                ddlCompanyToEdit.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindUpdateStatusList()
    {
        try
        {
            dtAction.Columns.Add("ACTION_ID", typeof(int));
            dtAction.Columns.Add("ACTION_NAME", typeof(string));

            if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil))
            {
                DataRow dr0 = dtAction.NewRow();
                dr0["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Select);
                dr0["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Select);

                DataRow dr1 = dtAction.NewRow();
                dr1["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Assign);
                dr1["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Assign);

                DataRow dr2 = dtAction.NewRow();
                dr2["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Checking);
                dr2["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Checking);

                DataRow dr3 = dtAction.NewRow();
                dr3["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Check);
                dr3["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Check);

                DataRow dr5 = dtAction.NewRow();
                dr5["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Amendment);
                dr5["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Amendment);

                DataRow dr6 = dtAction.NewRow();
                dr6["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Correction);
                dr6["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Correction);

                DataRow dr7 = dtAction.NewRow();
                dr7["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Re_Assign);
                dr7["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Re_Assign);

                dtAction.Rows.Add(dr0);
                dtAction.Rows.Add(dr1);
                dtAction.Rows.Add(dr2);
                dtAction.Rows.Add(dr3);
                dtAction.Rows.Add(dr5);
                dtAction.Rows.Add(dr6);
                dtAction.Rows.Add(dr7);
            }

            else if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0 && Convert.ToInt32(Session["DESIGN_CHECKER_ID"]) > 0)
            {
                DataRow dr0 = dtAction.NewRow();
                dr0["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Select);
                dr0["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Select);

                DataRow dr2 = dtAction.NewRow();
                dr2["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Checking);
                dr2["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Checking);

                DataRow dr5 = dtAction.NewRow();
                dr5["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Amendment);
                dr5["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Amendment);


                DataRow dr3 = dtAction.NewRow();
                dr3["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Check);
                dr3["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Check);

                DataRow dr6 = dtAction.NewRow();
                dr6["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Correction);
                dr6["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Correction);


                dtAction.Rows.Add(dr0);
                dtAction.Rows.Add(dr2);
                dtAction.Rows.Add(dr5);
                dtAction.Rows.Add(dr3);
                dtAction.Rows.Add(dr6);
            }

            else if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0 && Convert.ToInt32(Session["DESIGN_CHECKER_ID"]) == 0)
            {
                DataRow dr0 = dtAction.NewRow();
                dr0["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Select);
                dr0["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Select);

                DataRow dr2 = dtAction.NewRow();
                dr2["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Checking);
                dr2["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Checking);

                DataRow dr5 = dtAction.NewRow();
                dr5["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Amendment);
                dr5["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Amendment);

                dtAction.Rows.Add(dr0);
                dtAction.Rows.Add(dr2);
                dtAction.Rows.Add(dr5);
            }

            else if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0 && Convert.ToInt32(Session["DESIGN_CHECKER_ID"]) > 0)
            {
                DataRow dr0 = dtAction.NewRow();
                dr0["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Select);
                dr0["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Select);

                DataRow dr3 = dtAction.NewRow();
                dr3["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Check);
                dr3["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Check);

                DataRow dr6 = dtAction.NewRow();
                dr6["ACTION_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Correction);
                dr6["ACTION_NAME"] = Convert.ToString(DMSAllStatusAndTypes.Action.Send_To_Correction);

                dtAction.Rows.Add(dr0);
                dtAction.Rows.Add(dr3);
                dtAction.Rows.Add(dr6);
            }

            if (dtAction.Rows.Count > 0)
            {
                ddlUpdateStatus.DataSource = dtAction;
                ddlUpdateStatus.DataTextField = "ACTION_NAME";
                ddlUpdateStatus.DataValueField = "ACTION_ID";
                ddlUpdateStatus.DataBind();
                ddlUpdateStatus.SelectedIndex = 0;
            }
            else
            {
                ddlUpdateStatus.Items.Insert(0, "Select");
                ddlUpdateStatus.SelectedIndex = 0;


                ddlUpdateStatus.Visible = false;
                btnUpdateStatus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDesignStatusSearch()
    {
        try
        {
            dsDesignStatusSearch = objProject.GetDesignStatus();
            if (dsDesignStatusSearch.Tables.Count > 0 && dsDesignStatusSearch.Tables[0].Rows.Count > 0)
            {
                ddlPostingStatusMainSearch.DataSource = dsDesignStatusSearch.Tables[0];
                ddlPostingStatusMainSearch.DataTextField = "STATUS_NAME";
                ddlPostingStatusMainSearch.DataValueField = "STATUS_ID";
                ddlPostingStatusMainSearch.DataBind();
                ddlPostingStatusMainSearch.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDesignCategoryMainSearch()
    {
        try
        {
            dsDesignCategory = objProject.GetDesignCategoryList();
            if (dsDesignCategory.Tables.Count > 0 && dsDesignCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategoryMainSearch.DataSource = dsDesignCategory.Tables[0];
                ddlCategoryMainSearch.DataTextField = "DESIGN_CATEGORY";
                ddlCategoryMainSearch.DataValueField = "DESIGN_CATEGORY_ID";
                ddlCategoryMainSearch.DataBind();
                ddlCategoryMainSearch.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDesignCategoryToEdit()
    {
        try
        {
            dsDesignCategory = objProject.GetDesignCategoryList();
            if (dsDesignCategory.Tables.Count > 0 && dsDesignCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategoryToEdit.DataSource = dsDesignCategory.Tables[0];
                ddlCategoryToEdit.DataTextField = "DESIGN_CATEGORY";
                ddlCategoryToEdit.DataValueField = "DESIGN_CATEGORY_ID";
                ddlCategoryToEdit.DataBind();
                ddlCategoryToEdit.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindDesignCheckerSearch()
    {
        try
        {
            dsDesignChecker = objProject.GetDesignChecker();
            if (dsDesignChecker.Tables.Count > 0 && dsDesignChecker.Tables[0].Rows.Count > 0)
            {
                ddlDesignCheckerMainSearch.DataSource = dsDesignChecker.Tables[0];
                ddlDesignCheckerMainSearch.DataTextField = "DESIGN_CHECKER";
                ddlDesignCheckerMainSearch.DataValueField = "DESIGN_CHECKER_ID";
                ddlDesignCheckerMainSearch.DataBind();
                ddlDesignCheckerMainSearch.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindRespinsibleEngSearch()
    {
        try
        {
            dsDesignEngg = objProject.GetDesignResponsibleEngg();
            if (dsDesignEngg.Tables.Count > 0 && dsDesignEngg.Tables[0].Rows.Count > 0)
            {
                //Session["dtDesignEngg"] = dsDesignEngg.Tables[0];
                ddlRespDesignEnggMainSearch.DataSource = dsDesignEngg.Tables[0];
                ddlRespDesignEnggMainSearch.DataTextField = "DESIGN_RESPONSIBLE_ENGG";
                ddlRespDesignEnggMainSearch.DataValueField = "DESIGN_RESPONSIBLE_ENGG_ID";
                ddlRespDesignEnggMainSearch.DataBind();
                ddlRespDesignEnggMainSearch.Items.Insert(0, "All");


                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                    Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
                {
                    ddlRespDesignEnggMainSearch.SelectedIndex = 0;
                }
                else
                {
                    ddlRespDesignEnggMainSearch.SelectedValue = Convert.ToString(Session["DESIGN_RESPONSIBLE_ENGG_ID"]);
                    //ddlRespDesignEnggMainSearch.Enabled = false;
                }
            }
            else
            {
                //Session["dtDesignEngg"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindRespinsibleEngToEdit()
    {
        try
        {
            dsDesignEngg = objProject.GetDesignResponsibleEngg();
            if (dsDesignEngg.Tables.Count > 0 && dsDesignEngg.Tables[0].Rows.Count > 0)
            {
                ddlRespDesignEnggToEdit.DataSource = dsDesignEngg.Tables[0];
                ddlRespDesignEnggToEdit.DataTextField = "DESIGN_RESPONSIBLE_ENGG";
                ddlRespDesignEnggToEdit.DataValueField = "DESIGN_RESPONSIBLE_ENGG_ID";
                ddlRespDesignEnggToEdit.DataBind();
                ddlRespDesignEnggToEdit.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCreatedBySearch()
    {
        try
        {
            dsEmployee = objProject.GetEmployeesToAddApprover();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlCreatedByMainSearch.DataSource = dsEmployee.Tables[0];
                ddlCreatedByMainSearch.DataTextField = "EMPLOYEE_NAME";
                ddlCreatedByMainSearch.DataValueField = "EMP_RECORD_ID";
                ddlCreatedByMainSearch.DataBind();
                ddlCreatedByMainSearch.Items.Insert(0, "All");


                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
                {
                    ddlCreatedByMainSearch.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                    //ddlCreatedByMainSearch.Enabled = false;
                }

                //if (Convert.ToString(Session["USER_TYPE"]) == "A")
                //{
                //    ddlCreatedByMainSearch.Enabled = true;
                //}
                //else
                //{
                //    ddlCreatedByMainSearch.Enabled = false;
                //}
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetDesignDetailList()
    {
        try
        {
            datetTypeID = 0;
            dateSign = string.Empty;
            fromDate = string.Empty;
            toDate = string.Empty;
            categoryID = 0;
            jobNo = string.Empty;
            isPlanned = string.Empty;
            drawingNo = string.Empty;
            postingStatusID = 0;
            designCheckerID = 0;
            responsibleEnggID = 0;
            createdByID = 0;

            datetTypeID = Convert.ToInt32(ddlOnWhichDateMainSearch.SelectedValue);
            dateSign = Convert.ToString(ddlSignMainSearch.SelectedValue);


            if (chkSelectDates.Checked)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                    fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                    toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            }

            if (ddlCategoryMainSearch.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategoryMainSearch.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNoMainSearch.Text))
                jobNo = txtJOBNoMainSearch.Text.Trim().ToUpper();

            //if (ddlIsPlannedMainSearch.SelectedIndex > 0)
            //    isPlanned = Convert.ToString(ddlIsPlannedMainSearch.SelectedValue);
            //else
            //    isPlanned = string.Empty;

            if (!string.IsNullOrEmpty(txtDrawingNoMainSearch.Text))
                drawingNo = txtDrawingNoMainSearch.Text.Trim().ToUpper();

            if (ddlPostingStatusMainSearch.SelectedIndex > 0)
                postingStatusID = Convert.ToInt32(ddlPostingStatusMainSearch.SelectedValue);




            if (ddlDesignCheckerMainSearch.SelectedIndex > 0)
                designCheckerID = Convert.ToInt32(ddlDesignCheckerMainSearch.SelectedValue);

            if (ddlRespDesignEnggMainSearch.SelectedIndex > 0)
                responsibleEnggID = Convert.ToInt32(ddlRespDesignEnggMainSearch.SelectedValue);

            if (ddlCreatedByMainSearch.SelectedIndex > 0)
                createdByID = Convert.ToInt32(ddlCreatedByMainSearch.SelectedValue);

            dtDesignDetailsList = objProject.GetDesignDetailList(datetTypeID, dateSign, fromDate, toDate, categoryID, jobNo,
                                                                //isPlanned, 
                                                                drawingNo, postingStatusID, designCheckerID, responsibleEnggID, createdByID);

            if (dtDesignDetailsList.Tables.Count > 0 && dtDesignDetailsList.Tables[0].Rows.Count > 0)
            {
                Session["dtDesignDetailsList"] = dtDesignDetailsList.Tables[0];
                Session["dtTimesheet"] = dtDesignDetailsList.Tables[1];

                if (dtDesignDetailsList.Tables.Count > 0 && dtDesignDetailsList.Tables[2].Rows.Count > 0)
                {
                    Session["dtPMList"] = dtDesignDetailsList.Tables[2];
                }

                if (dtDesignDetailsList.Tables.Count > 0 && dtDesignDetailsList.Tables[2].Rows.Count > 0)
                {
                    Session["dtPMList"] = dtDesignDetailsList.Tables[2];
                }

                gvDesignDetails.DataSource = dtDesignDetailsList.Tables[0];
                gvDesignDetails.DataBind();
            }
            else
            {
                Session["dtDesignDetailsList"] = null;
                Session["dtTimesheet"] = null;
                Session["dtPMList"] = null;
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
            }
            lblRecords.Text = "Records[" + dtDesignDetailsList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }




    private void ViewDrawingFiles(int recordID, string fileType, string fileName)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                ExportDWGFile(recordID, fileType);
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

    private void ExportDWGFile(int recordID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();
            dsFiles = objProject.GetReviseDesignAddAttachmentFiles(recordID);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "ATTACHMENT1")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ADDITIONAL_ATTACHMENT_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ADDITIONAL_ATTACHMENT_NAME"]);
                }

                //else if (fileType == "ATTACHMENT2")
                //{
                //    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT2_DOC"];
                //    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT2_NAME"]);
                //}
                //else if (fileType == "ATTACHMENT3")
                //{
                //    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT3_DOC"];
                //    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT3_NAME"]);
                //}
                //else if (fileType == "ATTACHMENT4")
                //{
                //    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT4_DOC"];
                //    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT4_NAME"]);
                //}

                if (bytes != null)
                {
                    string[] stringParts = fileName.Split(new char[] { '.' });
                    string strType = stringParts[1];
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.ContentType = strType;
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
            }

        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("Timeout expired"))
            {
                ExceptionSubitemsMessage("The process of downloading is too longer, please try again...!!!");
                return;
            }
            else
            {
                throw;
            }
        }
    }

    private void Reset()
    {
        try
        {
            txtJOBNoToEdit.Text = string.Empty;
            txtDrawingNumberToEdit.Text = string.Empty;
            txtDescriptionToEdit.Text = string.Empty;
            txtQuantityToEdit.Text = string.Empty;
            txtUOMToEdit.Text = string.Empty;
            ddlCategoryToEdit.SelectedIndex = 0;
            txtDocumentLinkToEdit.Text = string.Empty;
            ddlDrawingRevisioinNumberToEdit.SelectedIndex = 0;

            mpeDesignDetails.Show();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataSet GetDesignJOBData()
    {
        try
        {
            int jobUnitID = 0;
            string jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;

            jobUnitID = Convert.ToInt32(ddlCompanySearch.SelectedValue);

            dsDesignJobNo = objProject.GetJOBDetailsForDesignDrawings(jobUnitID, jobNo);

            if (dsDesignJobNo.Tables.Count > 0)
            {
                return dsDesignJobNo;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void GetDesignJOBDetail()
    {
        try
        {
            dsDesignJobNo = GetDesignJOBData();
            if (dsDesignJobNo.Tables.Count > 0 && dsDesignJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsDesignJobNo.Tables[0];
                gvJOBDetail.DataBind();
            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvJOBDetail.DataSource = null;
                gvJOBDetail.DataBind();
            }
            lblJOBRecords.Text = "Records[" + gvJOBDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }


    private DataSet GetProjectJOBData()
    {
        try
        {
            int jobUnitID = 0;
            string jobNo = string.Empty;

            jobUnitID = Convert.ToInt32(ddlCompanySearch.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;

            dsProjectJobNo = objProject.GetJOBDetailsForProjectDrawings(jobUnitID, jobNo);

            if (dsProjectJobNo.Tables.Count > 0)
            {
                return dsProjectJobNo;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void GetProjectJOBDetail()
    {
        try
        {
            dsProjectJobNo = GetProjectJOBData();
            if (dsProjectJobNo.Tables.Count > 0 && dsProjectJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsProjectJobNo.Tables[0];
                gvJOBDetail.DataBind();

                if (dsProjectJobNo.Tables[1].Rows.Count > 0)
                {
                    Session["dtDrawingDetail"] = dsProjectJobNo.Tables[1];
                }
                else
                {
                    Session["dtDrawingDetail"] = null;
                }
            }
            else
            {
                Session["dtDrawingDetail"] = null;
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvJOBDetail.DataSource = null;
                gvJOBDetail.DataBind();
            }
            lblJOBRecords.Text = "Records[" + gvJOBDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }


    private void GetDrawingDetail()
    {
        try
        {
            jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoToEdit.Text))
                jobNo = txtJOBNoToEdit.Text;

            DataTable dt = new DataTable();
            if (Session["dtDrawingDetail"] != null)
                dt = (DataTable)Session["dtDrawingDetail"];


            dtDrawings.Columns.Add("DRAWING_ID", typeof(int));
            dtDrawings.Columns.Add("DRAWING_NO", typeof(string));
            dtDrawings.Columns.Add("DESCRIPTION", typeof(string));
            dtDrawings.Columns.Add("QUANTITY", typeof(string));
            dtDrawings.Columns.Add("UOM", typeof(string));
            dtDrawings.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));


            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Select("JOB_NO='" + jobNo + "'"))
                {
                    DataRow drd = dtDrawings.NewRow();

                    if (dr["DRAWING_ID"] != DBNull.Value)
                        drd["DRAWING_ID"] = dr["DRAWING_ID"];

                    if (dr["DRAWING_NO"] != DBNull.Value)
                        drd["DRAWING_NO"] = dr["DRAWING_NO"];

                    if (dr["DESCRIPTION"] != DBNull.Value)
                        drd["DESCRIPTION"] = dr["DESCRIPTION"];

                    if (dr["QUANTITY"] != DBNull.Value)
                        drd["QUANTITY"] = dr["QUANTITY"];

                    if (dr["UOM"] != DBNull.Value)
                        drd["UOM"] = dr["UOM"];

                    if (dr["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value)
                        drd["REQD_DATE_BY_PROJECT_TEAM"] = dr["REQD_DATE_BY_PROJECT_TEAM"];

                    dtDrawings.Rows.Add(drd);
                }
            }
            else
            {
                GetProjectJOBDetail();
            }


            if (dtDrawings.Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvDrawingDetail.DataSource = dtDrawings;
                gvDrawingDetail.DataBind();
            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvDrawingDetail.DataSource = null;
                gvDrawingDetail.DataBind();
            }
            lblDrawingRecords.Text = "Records[" + gvDrawingDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }



    private void SaveTimesheet()
    {
        try
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("EMP_RECORD_ID", typeof(int));
            dtt.Columns.Add("ENTRY_DATE", typeof(string));
            dtt.Columns.Add("START_TIME", typeof(string));
            dtt.Columns.Add("END_TIME", typeof(string));
            dtt.Columns.Add("TIME_SPENT", typeof(string));
            dtt.Columns.Add("TITLE", typeof(string));
            dtt.Columns.Add("REMARKS", typeof(string));
            dtt.Columns.Add("JOB_NUMBER", typeof(string));
            dtt.Columns.Add("DRAWING_ID", typeof(string));

            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            if (Convert.ToInt32(ddlProjectCategoryInTimesheet.SelectedIndex) > 0)
                projectCategory = Convert.ToString(ddlProjectCategoryInTimesheet.SelectedItem.Text);
            else projectCategory = string.Empty;

            if (Convert.ToInt32(ddlProjectNumberInTimesheet.SelectedIndex) > 0)
                projectNumber = Convert.ToString(ddlProjectNumberInTimesheet.SelectedItem.Text);
            else projectNumber = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["JOB_NO"])))
                jobNumber = Convert.ToString(ViewState["JOB_NO"]);

            if (ddlCategoryInTimesheet.SelectedIndex > 0)
                drawingCategory = Convert.ToString(ddlCategoryInTimesheet.SelectedItem.Text);
            else drawingCategory = string.Empty;

            if (ddlSerialNumberInTimesheet.SelectedIndex > 0)
                serialNumber = Convert.ToString(ddlSerialNumberInTimesheet.SelectedItem.Text);
            else serialNumber = string.Empty;

            if (ddlSizeInTimesheet.SelectedIndex > 0)
                size = Convert.ToString(ddlSizeInTimesheet.SelectedItem.Text);
            else size = string.Empty;

            if (ddlRevInTimesheet.SelectedIndex > 0)
                rev = Convert.ToString(ddlRevInTimesheet.SelectedItem.Text);
            else rev = string.Empty;

            if (!string.IsNullOrEmpty(txtSheetsInTimesheet.Text))
                sheets = Convert.ToInt32(txtSheetsInTimesheet.Text);
            else sheets = 0;

            //if (!string.IsNullOrEmpty(hdDrawingID.Value))
            //    drawingID = Convert.ToString(hdDrawingID.Value);
            //else drawingID = string.Empty;

            //if (string.IsNullOrEmpty(drawingID))
            //{
            //    if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DRAWING_NO"])))
            //        drawingID = Convert.ToString(ViewState["DRAWING_NO"]);
            //}

            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DRAWING_NO"])))
                drawingID = Convert.ToString(ViewState["DRAWING_NO"]);

            if (ddlTypeOfDrawingInTimesheet.SelectedIndex > 0)
                drawingTypeID = Convert.ToInt32(ddlTypeOfDrawingInTimesheet.SelectedValue);
            else drawingTypeID = 0;


            startTime = txtStartTimeInTimesheet.Text;
            endTime = txtEndTimeInTimesheet.Text;

            timeSpent = Convert.ToString(txtTimeSpentInTimesheet.Text);

            title = string.Empty; //txtTitle.Text;

            if (!string.IsNullOrEmpty(txtWorkDescriptionInTimesheet.Text))
                remarks = txtWorkDescriptionInTimesheet.Text;
            else
                remarks = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["WORKING_STATUS"])))
                status = Convert.ToString(ViewState["WORKING_STATUS"]);
            else status = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["EXPECTED_COMPLETION_DATE"])))
                expectedCompletionDate = Convert.ToDateTime(ViewState["EXPECTED_COMPLETION_DATE"]).ToString("yyyy-MM-dd");
            else expectedCompletionDate = string.Empty;



            int value = 0;
            string lastEndTime = string.Empty;
            string totalTimeSpent = string.Empty;
            bool checkForValidTime = false;
            string totalTimeSpentValue = string.Empty;
            DataTable dtTimesheet = new DataTable();

            timesheetDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (Session["dtTimesheet"] != null)
                dtTimesheet = (DataTable)Session["dtTimesheet"];
            else
            {
                DataSet ds = new DataSet();
                ds = objProject.GetUserLastEndTime(Convert.ToInt32(Session["EMP_RECORD_ID"]), timesheetDate);
                if (ds.Tables.Count > 0)
                    dtTimesheet = ds.Tables[0];
            }

            if (dtTimesheet.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTimesheet.Select("EMP_RECORD_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                {
                    DataRow dtr = dtt.NewRow();

                    dtr["EMP_RECORD_ID"] = Convert.ToInt32(dr["EMP_RECORD_ID"]);
                    dtr["ENTRY_DATE"] = Convert.ToString(dr["ENTRY_DATE"]);
                    dtr["START_TIME"] = Convert.ToString(dr["START_TIME"]);
                    dtr["END_TIME"] = Convert.ToString(dr["END_TIME"]);
                    dtr["TIME_SPENT"] = Convert.ToString(dr["TIME_SPENT"]);
                    dtr["TITLE"] = Convert.ToString(dr["TITLE"]);
                    dtr["REMARKS"] = Convert.ToString(dr["REMARKS"]);
                    dtr["JOB_NUMBER"] = Convert.ToString(dr["JOB_NUMBER"]);
                    dtr["DRAWING_ID"] = Convert.ToString(dr["DRAWING_ID"]);

                    dtt.Rows.Add(dtr);
                }
            }

            int counts = 0;

            if (dtt.Rows.Count > 0)
            {
                counts = Convert.ToInt32(dtt.Rows.Count);

                if (counts == 1)
                {
                    if (Convert.ToDateTime(endTime).TimeOfDay <= Convert.ToDateTime(dtt.Rows[0]["START_TIME"]).TimeOfDay)
                        checkForValidTime = true;
                    else if (Convert.ToDateTime(startTime).TimeOfDay >= Convert.ToDateTime(dtt.Rows[0]["END_TIME"]).TimeOfDay)
                        checkForValidTime = true;
                    else
                        checkForValidTime = false;
                }
                else
                {
                    for (int i = 0; i < counts; i++)
                    {
                        if (i == dtt.Rows.Count - 1)
                        {
                            if (Convert.ToDateTime(dtt.Rows[i]["END_TIME"]).TimeOfDay <= Convert.ToDateTime(startTime).TimeOfDay)
                            {
                                checkForValidTime = true;
                                break;
                            }
                            else
                                checkForValidTime = false;
                        }
                        else
                        {
                            if (Convert.ToDateTime(dtt.Rows[i]["END_TIME"]).TimeOfDay >= Convert.ToDateTime(startTime).TimeOfDay &&
                                                                Convert.ToDateTime(endTime).TimeOfDay <= Convert.ToDateTime(dtt.Rows[i + 1]["START_TIME"]).TimeOfDay)
                            {
                                checkForValidTime = true;
                                break;
                            }
                            else
                                checkForValidTime = false;
                        }
                    }
                }
            }
            else
            {
                checkForValidTime = true;
            }



            if (recordID > 0)
            {
                if (checkForValidTime)
                {
                    totalTimeSpentValue = GetTotalTimespent(timeSpent, totalTimeSpent);

                    if (string.IsNullOrEmpty(totalTimeSpentValue))
                    {
                        totalTimeSpentValue = "00:00";
                    }

                    value = objProject.InsertDesignTimesheet(recordID, empRecordID, projectCategory, projectNumber, jobNumber, drawingCategory,
                                                            serialNumber, size, rev, sheets, drawingID, drawingTypeID, startTime, endTime, timeSpent,
                                                            title, timesheetDate, remarks, status, expectedCompletionDate, totalTimeSpentValue, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (value > 0)
                    {

                        string edDate = string.Empty;
                        string rqdDate = string.Empty;


                        if (ViewState["REQD_DATE"] != null)
                        {
                            rqdDate = Convert.ToString(ViewState["REQD_DATE"]);
                        }

                        if (ViewState["EXPECTED_COMPLETION_DATE"] != null)
                        {
                            edDate = Convert.ToString(ViewState["EXPECTED_COMPLETION_DATE"]);
                        }


                        if (!string.IsNullOrEmpty(rqdDate) && !string.IsNullOrEmpty(edDate))
                        {
                            if (Convert.ToDateTime(edDate) > Convert.ToDateTime(rqdDate))
                            {
                                extendedRecordIDs += Convert.ToString(ViewState["RECORD_ID"]) + ",";
                            }
                        }


                        if (!string.IsNullOrEmpty(extendedRecordIDs))
                            extendedRecordIDs = extendedRecordIDs.TrimEnd(',');


                        if (!string.IsNullOrEmpty(extendedRecordIDs))
                        {
                            GetExtendedDrawingList(extendedRecordIDs);
                        }



                        ResetTimesheet();
                        SuccessMessage("Timesheet details added successfully");
                    }
                    else
                    {
                        ExceptionMessage("Please try again.");
                    }
                }
                else
                {
                    mpeAddTimesheet.Show();
                    ExceptionMessageTimesheet("Start time is should be greater than last end time!");
                }
            }
            else
            {
                ExceptionMessage("Please try again.");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private string GetTotalTimespent(string timeSpent, string totalTimeSpent)
    {
        try
        {
            if (string.IsNullOrEmpty(timeSpent))
                timeSpent = "00:00";

            if (string.IsNullOrEmpty(totalTimeSpent))
                totalTimeSpent = "00:00";


            int tH = Convert.ToInt32(timeSpent.Split(':')[0]);
            int tM = Convert.ToInt32(timeSpent.Split(':')[1]);
            int totaltM = tH * 60 + tM;

            int ttH = Convert.ToInt32(totalTimeSpent.Split(':')[0]);
            int ttM = Convert.ToInt32(totalTimeSpent.Split(':')[1]);
            int totalttM = ttH * 60 + ttM;

            int totalM = totaltM + totalttM;


            string h = string.Empty;
            string m = string.Empty;

            if (Convert.ToInt32(totalM / 60) < 10)
                h = "0" + Convert.ToString(totalM / 60);
            else h = Convert.ToString(totalM / 60);

            if (Convert.ToInt32(totalM % 60) < 10)
                m = "0" + Convert.ToString(totalM % 60);
            else m = Convert.ToString(totalM % 60);


            string totalTimeSpentValue = h + ":" + m;
            return totalTimeSpentValue;

        }
        catch (Exception)
        {

            throw;
        }
    }

    private void ResetTimesheet()
    {
        try
        {
            ddlProjectCategoryInTimesheet.SelectedIndex = 0;
            ddlProjectNumberInTimesheet.SelectedIndex = 0;
            ddlCategoryInTimesheet.SelectedIndex = 0;
            ddlSerialNumberInTimesheet.SelectedIndex = 0;
            ddlSizeInTimesheet.SelectedIndex = 0;
            ddlRevInTimesheet.SelectedIndex = 0;
            txtSheetsInTimesheet.Text = string.Empty;
            txtDrawingIDInTimesheet.Text = string.Empty;
            txtJOBNoInTimesheet.Text = string.Empty;
            ddlTypeOfDrawingInTimesheet.SelectedIndex = 0;
            ddlStartTimeHInTimesheet.SelectedIndex = 0;
            ddlStartTimeMInTimesheet.SelectedIndex = 0;
            txtStartTimeInTimesheet.Text = string.Empty;
            ddlEndTimeHInTimesheet.SelectedIndex = 0;
            ddlEndTimeMInTimesheet.SelectedIndex = 0;
            txtEndTimeInTimesheet.Text = string.Empty;
            txtTimeSpentInTimesheet.Text = string.Empty;
            txtWorkDescriptionInTimesheet.Text = string.Empty;

            gvTimesheet.DataSource = null;
            gvTimesheet.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int UpdateStatus(int recordID, string drawingNo, int statusID, string expectedCompletionDate,
                             string plannedStartDate, string plannedCompletedDate, int designResponsibleEnggID,
                             string drawingLink, int designCheckerID, string remarks, int amendmentCount, int sentToAmendementFlag)
    {
        try
        {
            int value = objProject.UpdateDesignStatus(recordID, statusID, expectedCompletionDate, plannedStartDate, plannedCompletedDate, designResponsibleEnggID,
                                                      drawingLink, designCheckerID, remarks, sentToAmendementFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                int mailTypeID = 0;

                if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                {
                    if (sentToAmendementFlag == 0)
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);
                    else
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendmentMail);
                }

                else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                {
                    if (sentToAmendementFlag == Convert.ToInt32(DMSAllStatusAndTypes.AmendmentType.Amendment))
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendmentMail);
                    else if (sentToAmendementFlag == Convert.ToInt32(DMSAllStatusAndTypes.AmendmentType.Correction))
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CorrectionMail);
                    else
                    {
                        if (amendmentCount == 0)
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AssignmentMail);
                        else
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedAssignmentMail);
                    }
                }

                else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                {
                    if (amendmentCount == 0)
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CheckingMail);
                    else
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedCheckingMail);
                }

                else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                {
                    if (amendmentCount == 0)
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CheckedMail);
                    else
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedCheckedMail);
                }

                //else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Closed))
                //{
                //    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.ClosedMail);
                //}


                //int mailSentValue = objDMSSendMail.SendMail(recordID, mailTypeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                //int mailSentValue = objDMSSendMail.ProcessAndSendMail("", recordID, statusID, mailTypeID);

                int mailSentValue = objDMSSendMail.ProcessAndSendMail(drawingNo, recordID, statusID, mailTypeID);
                if (mailSentValue > 0)
                {
                    //int mailSentStatusValue = objProject.UpdateDesignMailStatus(recordID, statusID, amendmentCount, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    int mailSentStatusValue = objProject.UpdateDesignMailStatus(drawingNo, recordID, statusID, amendmentCount, sentToAmendementFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                }

                if (mailSentValue > 0)
                {
                    if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                    {
                        if (amendmentCount == 0)
                            SuccessMessage("Design sent to amendment and mail sent successfully...!!!");
                        else
                            SuccessMessage("Amended design sent to amendment and mail sent successfully...!!!");
                    }
                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                    {
                        if (amendmentCount == 0)
                            SuccessMessage("Design assigned and mail sent successfully...!!!");
                        else
                            SuccessMessage("Amended design assigned and mail sent successfully...!!!");
                    }
                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                    {
                        if (amendmentCount == 0)
                            SuccessMessage("Design sent for checking and mail sent successfully...!!!");
                        else
                            SuccessMessage("Amended design sent for checking and mail sent successfully...!!!");
                    }
                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                    {
                        if (amendmentCount == 0)
                            SuccessMessage("Design checked and mail sent successfully...!!!");
                        else
                            SuccessMessage("Amended design checked and mail sent successfully...!!!");
                    }
                    //else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Closed))
                    //{
                    //    if (amendmentCount == 0)
                    //        SuccessMessage("Design closed and mail sent successfully...!!!");
                    //    else
                    //        SuccessMessage("Amended design closed and mail sent successfully...!!!");
                    //}
                }
                else
                {
                    if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                    {
                        if (amendmentCount == 0)
                            SuccessMessage("Design sent to amendment successfully, please go to list and send mail...!!!");
                        else
                            SuccessMessage("Amended design sent to amendment successfully, please go to list and send mail...!!!");
                    }
                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                    {
                        if (amendmentCount == 0)
                            SuccessMessage("Design assigned successfully, please go to list and send mail...!!!");
                        else
                            SuccessMessage("Amended design assigned successfully, please go to list and send mail...!!!");
                    }
                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                    {
                        if (amendmentCount == 0)
                            SuccessMessage("Design sent for checking successfully, please go to list and send mail...!!!");
                        else
                            SuccessMessage("Amended design sent for checking successfully, please go to list and send mail...!!!");
                    }
                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                    {
                        if (amendmentCount == 0)
                            SuccessMessage("Design checked successfully, please go to list and send mail...!!!");
                        else
                            SuccessMessage("Amended design checked successfully, please go to list and send mail...!!!");
                    }
                    //else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Closed))
                    //{
                    //    if (amendmentCount == 0)
                    //        SuccessMessage("Design closed successfully, please go to list and send mail...!!!");
                    //    else
                    //        SuccessMessage("Amended design closed successfully, please go to list and send mail...!!!");
                    //}
                }

                GetDesignDetailList();
                return value;
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
        }
    }


    private void UpdateStatusAll()
    {
        try
        {
            extendedRecordIDs = string.Empty;
            recordID = 0;
            statusID = 0;
            int currentStatusID = 0;
            //string plannedStartDate = string.Empty;
            //string plannedCompletedDate = string.Empty;
            int designResponsibleEnggID = 0;
            string drawingLink = string.Empty;
            int designCheckerID = 0;
            string remarks = string.Empty;
            int sentToAmendementFlag = 0;

            int count = 0;
            int savedCount = 0;
            int statusCount = 0;
            bool check = true;
            bool accessCheck = false;
            string drawingNos = string.Empty;
            string drawingIDs = string.Empty;

            if (gvDesignDetails.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlUpdateStatus.SelectedValue) == Convert.ToInt32(DMSAllStatusAndTypes.Action.Assign))
                {
                    currentStatusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);
                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);
                }

                else if (Convert.ToInt32(ddlUpdateStatus.SelectedValue) == Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Checking))
                {
                    currentStatusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);
                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking);
                }

                else if (Convert.ToInt32(ddlUpdateStatus.SelectedValue) == Convert.ToInt32(DMSAllStatusAndTypes.Action.Check))
                {
                    currentStatusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking);
                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked);
                }

                //else if (Convert.ToInt32(ddlUpdateStatus.SelectedValue) == Convert.ToInt32(DMSAllStatusAndTypes.Action.Close))
                //{
                //    currentStatusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked);
                //    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Closed);
                //}

                else if (Convert.ToInt32(ddlUpdateStatus.SelectedValue) == Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Amendment))
                {
                    currentStatusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);
                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);
                    sentToAmendementFlag = Convert.ToInt32(DMSAllStatusAndTypes.AmendmentType.Amendment);
                }

                else if (Convert.ToInt32(ddlUpdateStatus.SelectedValue) == Convert.ToInt32(DMSAllStatusAndTypes.Action.Send_To_Correction))
                {
                    currentStatusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking);
                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);
                    sentToAmendementFlag = Convert.ToInt32(DMSAllStatusAndTypes.AmendmentType.Correction);
                }

                else if (Convert.ToInt32(ddlUpdateStatus.SelectedValue) == Convert.ToInt32(DMSAllStatusAndTypes.Action.Re_Assign))
                {
                    currentStatusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);
                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);
                }

                count = 0;
                savedCount = 0;
                statusCount = 0;

                foreach (GridViewRow gr in gvDesignDetails.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                    Label lblStatusID = gr.FindControl("lblStatusID") as Label;
                    Label lblDesignResponsibleEnggEmpRecordID = gr.FindControl("lblDesignResponsibleEnggEmpRecordID") as Label;
                    Label lblDesignCheckerEmpRecordID = gr.FindControl("lblDesignCheckerEmpRecordID") as Label;
                    Label lblCreatedByID = gr.FindControl("lblCreatedByID") as Label;


                    if (chkSelect.Checked)
                    {
                        plannedStartDateByDesignTeam = string.Empty;
                        plannedCompletionDateByDesignTeam = string.Empty;
                        designResponsibleEnggID = 0;
                        designCheckerID = 0;
                        remarks = string.Empty;
                        check = true;
                        accessCheck = false;

                        count++;

                        if (Convert.ToInt32(lblStatusID.Text) == currentStatusID)
                        {

                            if (currentStatusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                            {
                                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder))
                                    accessCheck = true;
                                else
                                    accessCheck = false;
                            }

                            else if (currentStatusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                            {
                                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblDesignResponsibleEnggEmpRecordID.Text))
                                    accessCheck = true;
                                else
                                    accessCheck = false;
                            }

                            else if (currentStatusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                            {
                                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblDesignCheckerEmpRecordID.Text))
                                    accessCheck = true;
                                else
                                    accessCheck = false;
                            }

                            else if (currentStatusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                            {
                                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblCreatedByID.Text))
                                    accessCheck = true;
                                else
                                    accessCheck = false;
                            }


                            if (accessCheck)
                            {
                                statusCount++;

                                Label lblRecordID = gr.FindControl("lblRecordID") as Label;

                                Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;
                                Label lblReqdDateByProjectTeam = gr.FindControl("lblReqdDateByProjectTeam") as Label;
                                TextBox txtExpectedCompletionDate = gr.FindControl("txtExpectedCompletionDate") as TextBox;
                                TextBox txtPlannedStartDateByDesignTeam = gr.FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
                                TextBox txtPlannedCompletionDateByDesignTeam = gr.FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
                                DropDownList ddlResponsibleDesignEngineer = gr.FindControl("ddlResponsibleDesignEngineer") as DropDownList;
                                DropDownList ddlDesignChecker = gr.FindControl("ddlDesignChecker") as DropDownList;
                                TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;
                                TextBox txtDrawingLink = gr.FindControl("txtDrawingLink") as TextBox;

                                recordID = Convert.ToInt32(lblRecordID.Text);

                                if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedStartDateByDesignTeam.Text)))
                                    {
                                        plannedStartDateByDesignTeam = Convert.ToDateTime(txtPlannedStartDateByDesignTeam.Text).ToString("yyyy-MM-dd");
                                        txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                    }
                                    else
                                    {
                                        plannedStartDateByDesignTeam = string.Empty;
                                        check = false;
                                        txtPlannedStartDateByDesignTeam.Focus();
                                        txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                    }


                                    if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text)))
                                    {
                                        plannedCompletionDateByDesignTeam = Convert.ToDateTime(txtPlannedCompletionDateByDesignTeam.Text).ToString("yyyy-MM-dd");
                                        txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                    }
                                    else
                                    {
                                        plannedCompletionDateByDesignTeam = string.Empty;
                                        check = false;
                                        txtPlannedCompletionDateByDesignTeam.Focus();
                                        txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                    }


                                    if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
                                    {
                                        designResponsibleEnggID = Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue);
                                        ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.White;
                                    }
                                    else
                                    {
                                        designResponsibleEnggID = 0;
                                        check = false;
                                        ddlResponsibleDesignEngineer.Focus();
                                        ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.LightPink;
                                    }
                                }





                                else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                                {
                                    if (!string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
                                    {
                                        expectedCompletionDate = txtExpectedCompletionDate.Text;
                                        txtExpectedCompletionDate.BackColor = System.Drawing.Color.White;

                                        //if (Convert.ToDateTime(txtExpectedCompletionDate.Text) > Convert.ToDateTime(lblReqdDateByProjectTeam.Text))
                                        //{
                                        //    extendedRecordIDs += recordID + ",";
                                        //}
                                    }
                                    else
                                    {
                                        expectedCompletionDate = string.Empty;
                                        check = false;
                                        txtExpectedCompletionDate.Focus();
                                        txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightPink;
                                    }




                                    if (!string.IsNullOrEmpty(txtDrawingLink.Text))
                                    {
                                        drawingLink = txtDrawingLink.Text;
                                        txtDrawingLink.BackColor = System.Drawing.Color.White;
                                    }
                                    else
                                    {
                                        drawingLink = string.Empty;
                                        check = false;
                                        txtDrawingLink.Focus();
                                        txtDrawingLink.BackColor = System.Drawing.Color.LightPink;
                                    }


                                    if (ddlDesignChecker.SelectedIndex > 0)
                                    {
                                        designCheckerID = Convert.ToInt32(ddlDesignChecker.SelectedValue);
                                        ddlDesignChecker.BackColor = System.Drawing.Color.White;
                                    }
                                    else
                                    {
                                        designCheckerID = 0;
                                        check = false;
                                        ddlDesignChecker.Focus();
                                        ddlDesignChecker.BackColor = System.Drawing.Color.LightPink;
                                    }
                                }


                                else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open) && sentToAmendementFlag > 0)
                                {
                                    if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
                                    {
                                        designResponsibleEnggID = Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue);
                                        ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.White;
                                    }
                                    else
                                    {
                                        designResponsibleEnggID = 0;
                                        check = false;
                                        ddlResponsibleDesignEngineer.Focus();
                                        ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.LightPink;
                                    }
                                }


                                if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                                    remarks = Convert.ToString(txtRemarks.Text);
                                else remarks = string.Empty;



                                int saveDvalue = 0;
                                if (check)
                                {
                                    saveDvalue = objProject.UpdateDesignStatus(recordID, statusID, expectedCompletionDate,
                                                                               plannedStartDateByDesignTeam, plannedCompletionDateByDesignTeam, designResponsibleEnggID,
                                                                               drawingLink, designCheckerID, remarks, sentToAmendementFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                }


                                if (saveDvalue > 0)
                                {
                                    savedCount++;
                                    drawingNos += "'" + Convert.ToString(lblDrawingNo.Text).ToUpper().Trim() + "',";
                                    drawingIDs += recordID + ",";
                                }
                            }
                        }
                    }
                }






                if (count == 0)
                {
                    ExceptionMessage("Please select atleaset 1 row...!!!");
                    return;
                }

                if (statusCount == 0)
                {
                    if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                    {
                        ExceptionMessage("No selected data found in Generated status...!!!");
                        return;
                    }

                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                    {
                        ExceptionMessage("No selected data found in Open status...!!!");
                        return;
                    }

                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                    {
                        ExceptionMessage("No selected data found in Checking status...!!!");
                        return;
                    }

                    //else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Closed))
                    //{
                    //    ExceptionMessage("No selected data found in Checked status...!!!");
                    //    return;
                    //}
                }

                if (savedCount > 0)
                {

                    int mailTypeID = 0;

                    if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                    {
                        if (sentToAmendementFlag == 0)
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);
                        else
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendmentMail);
                    }



                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                    {
                        if (sentToAmendementFlag == Convert.ToInt32(DMSAllStatusAndTypes.AmendmentType.Amendment))
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendmentMail);
                        else if (sentToAmendementFlag == Convert.ToInt32(DMSAllStatusAndTypes.AmendmentType.Correction))
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CorrectionMail);
                        else
                        {
                            if (amendmentCount == 0)
                                mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AssignmentMail);
                            else
                                mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedAssignmentMail);
                        }
                    }
                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                    {
                        if (amendmentCount == 0)
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CheckingMail);
                        else
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedCheckingMail);
                    }
                    else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                    {
                        if (amendmentCount == 0)
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.CheckedMail);
                        else
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedCheckedMail);
                    }

                    if (!string.IsNullOrEmpty(drawingNos))
                        drawingNos = drawingNos.TrimEnd(',');


                    int mailSentValue = 0;
                    if (!string.IsNullOrEmpty(drawingNos))
                    {
                        mailSentValue = objDMSSendMail.ProcessAndSendMail(drawingNos, 0, statusID, mailTypeID);
                        if (mailSentValue > 0)
                        {
                            int mailSentStatusValue = objProject.UpdateDesignMailStatus(drawingNos, 0, statusID, 0, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        }
                    }



                    if (mailSentValue > 0)
                    {
                        if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage(savedCount + " design(s) sent to amendment and mail sent successfully...!!!");
                            else
                                SuccessMessage(savedCount + " amended design(s) sent to amendment and mail sent successfully...!!!");
                        }
                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage(savedCount + " design(s) assigned and mail sent successfully...!!!");
                            else
                                SuccessMessage(savedCount + " amended design(s) assigned and mail sent successfully...!!!");
                        }
                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage(savedCount + " design(s) sent for checking and mail sent successfully...!!!");
                            else
                                SuccessMessage(savedCount + " amended design(s) sent for checking and mail sent successfully...!!!");
                        }
                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage(savedCount + " design(s) checked and mail sent successfully...!!!");
                            else
                                SuccessMessage(savedCount + " amended design(s) checked and mail sent successfully...!!!");
                        }
                    }
                    else
                    {
                        if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage(savedCount + " design(s) sent to amendment successfully, please go to list and send mail...!!!");
                            else
                                SuccessMessage(savedCount + " amended design(s) sent to amendment successfully, please go to list and send mail...!!!");
                        }
                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage(savedCount + " design(s) assigned successfully, please go to list and send mail...!!!");
                            else
                                SuccessMessage(savedCount + " amended design(s) assigned successfully, please go to list and send mail...!!!");
                        }
                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage(savedCount + " design(s) sent for checking successfully, please go to list and send mail...!!!");
                            else
                                SuccessMessage(savedCount + " amended design(s) sent for checking successfully, please go to list and send mail...!!!");
                        }
                        else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage(savedCount + " design(s) checked successfully, please go to list and send mail...!!!");
                            else
                                SuccessMessage(savedCount + " amended design(s) design checked successfully, please go to list and send mail...!!!");
                        }
                    }


                    //if (!string.IsNullOrEmpty(extendedRecordIDs))
                    //    extendedRecordIDs = extendedRecordIDs.TrimEnd(',');

                    //if (!string.IsNullOrEmpty(extendedRecordIDs))
                    //    GetExtendedDrawingList(extendedRecordIDs);
                }
                else
                {
                    ExceptionMessage("Please try again...!!!");
                    return;
                }
            }
            else
            {
                ExceptionMessage("No data found...!!!");
                return;
            }

            ddlUpdateStatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetExtendedDrawingList(string extendedRecordIDs)
    {
        try
        {
            DataTable dtTempDrawingList = new DataTable();
            dtTempDrawingList.Columns.Add("JOB_NO", typeof(string));
            dtTempDrawingList.Columns.Add("SHORT_JOB_NO", typeof(string));
            dtTempDrawingList.Columns.Add("DESIGN_CATEGORY", typeof(string));
            dtTempDrawingList.Columns.Add("DESCRIPTION", typeof(string));
            dtTempDrawingList.Columns.Add("STATUS_NAME", typeof(string));
            dtTempDrawingList.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
            dtTempDrawingList.Columns.Add("PLANNED_START_DATE_BY_DESIGN_TEAM", typeof(string));
            dtTempDrawingList.Columns.Add("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM", typeof(string));
            dtTempDrawingList.Columns.Add("DRAWING_NO", typeof(string));
            dtTempDrawingList.Columns.Add("DRAWING_REV_NO", typeof(string));
            dtTempDrawingList.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            dtTempDrawingList.Columns.Add("DESIGN_RESPONSIBLE_ENGG", typeof(string));
            dtTempDrawingList.Columns.Add("DESIGN_CHECKER", typeof(string));
            dtTempDrawingList.Columns.Add("UNIT", typeof(string));

            if (gvDesignDetails.Rows.Count > 0)
            {

                string[] strExtendedRecordIDs = extendedRecordIDs.Split(',');
                foreach (string rID in strExtendedRecordIDs)
                {
                    foreach (GridViewRow gr in gvDesignDetails.Rows)
                    {

                        Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                        Label lblJOBNo = (Label)gr.FindControl("lblJOBNo");
                        Label lblShortJOBNo = (Label)gr.FindControl("lblShortJOBNo");
                        Label lblCategory = (Label)gr.FindControl("lblCategory");
                        Label lblDescription = (Label)gr.FindControl("lblDescription");
                        Label lblStatusName = (Label)gr.FindControl("lblStatusName");
                        Label lblReqdDateByProjectTeam = (Label)gr.FindControl("lblReqdDateByProjectTeam");
                        TextBox txtPlannedStartDateByDesignTeam = (TextBox)gr.FindControl("txtPlannedStartDateByDesignTeam");
                        TextBox txtPlannedCompletionDateByDesignTeam = (TextBox)gr.FindControl("txtPlannedCompletionDateByDesignTeam");
                        Label lblDrawingNo = (Label)gr.FindControl("lblDrawingNo");
                        TextBox txtDrawingRevNo = (TextBox)gr.FindControl("txtDrawingRevNo");
                        TextBox txtExpectedCompletionDate = (TextBox)gr.FindControl("txtExpectedCompletionDate");
                        Label lblDesignResponsibleEngg = (Label)gr.FindControl("lblDesignResponsibleEngg");
                        Label lblDesignChecker = (Label)gr.FindControl("lblDesignChecker");
                        Label lblJOBUnit = (Label)gr.FindControl("lblJOBUnit");

                        if (Convert.ToInt32(rID) == Convert.ToInt32(lblRecordID.Text))
                        {
                            DataRow drn1 = dtTempDrawingList.NewRow();

                            if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text)))
                                drn1["JOB_NO"] = Convert.ToString(lblJOBNo.Text);
                            else drn1["JOB_NO"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblShortJOBNo.Text)))
                                drn1["SHORT_JOB_NO"] = Convert.ToString(lblShortJOBNo.Text);
                            else drn1["SHORT_JOB_NO"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblCategory.Text)))
                                drn1["DESIGN_CATEGORY"] = Convert.ToString(lblCategory.Text);
                            else drn1["DESIGN_CATEGORY"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDescription.Text)))
                                drn1["DESCRIPTION"] = Convert.ToString(lblDescription.Text);
                            else drn1["DESCRIPTION"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblStatusName.Text)))
                                drn1["STATUS_NAME"] = Convert.ToString(lblStatusName.Text);
                            else drn1["STATUS_NAME"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblReqdDateByProjectTeam.Text)))
                                drn1["REQD_DATE_BY_PROJECT_TEAM"] = Convert.ToString(lblReqdDateByProjectTeam.Text);
                            else drn1["REQD_DATE_BY_PROJECT_TEAM"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedStartDateByDesignTeam.Text)))
                                drn1["PLANNED_START_DATE_BY_DESIGN_TEAM"] = Convert.ToString(txtPlannedStartDateByDesignTeam.Text);
                            else drn1["PLANNED_START_DATE_BY_DESIGN_TEAM"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text)))
                                drn1["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text);
                            else drn1["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text)))
                                drn1["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text);
                            else drn1["DRAWING_NO"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtDrawingRevNo.Text)))
                                drn1["DRAWING_REV_NO"] = Convert.ToString(txtDrawingRevNo.Text);
                            else drn1["DRAWING_REV_NO"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtExpectedCompletionDate.Text)))
                                drn1["EXPECTED_COMPLETION_DATE"] = Convert.ToString(txtExpectedCompletionDate.Text);
                            else drn1["EXPECTED_COMPLETION_DATE"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDesignResponsibleEngg.Text)))
                                drn1["DESIGN_RESPONSIBLE_ENGG"] = Convert.ToString(lblDesignResponsibleEngg.Text);
                            else drn1["DESIGN_RESPONSIBLE_ENGG"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDesignChecker.Text)))
                                drn1["DESIGN_CHECKER"] = Convert.ToString(lblDesignChecker.Text);
                            else drn1["DESIGN_CHECKER"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblJOBUnit.Text)))
                                drn1["UNIT"] = Convert.ToString(lblJOBUnit.Text);
                            else drn1["UNIT"] = string.Empty;

                            dtTempDrawingList.Rows.Add(drn1);
                        }
                    }
                }


                DataTable dtPMList = new DataTable();
                DataTable dtUnitList = new DataTable();


                if (Session["dtPMList"] != null)
                {
                    dtPMList = (DataTable)Session["dtPMList"];
                }

                if (Session["dtUnitList"] != null)
                {
                    dtUnitList = (DataTable)Session["dtUnitList"];
                }

                if (dtTempDrawingList.Rows.Count > 0)
                {
                    CreateAttachmentsAndSendMail(dtTempDrawingList, dtPMList, dtUnitList);
                }


                GetDesignDetailList();
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int CreateAttachmentsAndSendMail(DataTable dtDesignList, DataTable dtPMList, DataTable dtUnitList)
    {
        try
        {
            int sendMailValue = 0;
            int retVal = 0;
            string jobNo = string.Empty;
            string unit = string.Empty;
            string mrCreatedBy = string.Empty;
            Stream streamProcStmtProjectView = null;

            if (dtDesignList.Rows.Count > 0)
            {
                foreach (DataRow drul in dtUnitList.Rows)
                {
                    unit = Convert.ToString(drul["UNIT_NAME"]);
                    mrCreatedBy = Convert.ToString(drul["MR_CREATED_BY"]);
                    foreach (DataRow drpm in dtPMList.Select("UNIT_NAME='" + unit + "'"))
                    {
                        streamProcStmtProjectView = null;
                        body = string.Empty;
                        subject = string.Empty;
                        from = string.Empty;
                        to = string.Empty;
                        cc = string.Empty;
                        bcc = string.Empty;
                        toName = string.Empty;
                        mailSentDate = string.Empty;
                        from = "ithelpdesk@coperion.com";


                        to = Convert.ToString(drpm["EMAIL_ID"]);

                        if (mrCreatedBy == "LAKSHMI")
                        {
                            //foreach (var item in collection)
                            //{

                            //}
                        }


                        toName = Convert.ToString(drpm["PM"]);
                        jobNo = Convert.ToString(drpm["JOB_NO"]);


                        DataTable dtProc = new DataTable();
                        dtProc = dtDesignList.Clone();

                        foreach (DataRow dr2 in dtDesignList.Select("SHORT_JOB_NO='" + jobNo + "' AND UNIT='" + unit + "'"))
                        {
                            dtProc.ImportRow(dr2);
                        }

                        if (dtProc.Rows.Count > 0)
                        {

                            dtProc.Columns.Remove("SHORT_JOB_NO");

                            mailSentDate = DateTime.Now.ToString("dd-MMM-yyyy");
                            subject = "Alert of DMS Status (Job Number : " + jobNo + ")";

                            if (!string.IsNullOrEmpty(to))
                                to = to.TrimEnd(';');

                            if (!string.IsNullOrEmpty(cc))
                                cc = cc.TrimEnd(';');

                            if (!string.IsNullOrEmpty(bcc))
                                bcc = bcc.TrimEnd(';');

                            streamProcStmtProjectView = CreateAttachment(dtProc);

                            int chk = 0;
                            if (streamProcStmtProjectView == null)
                            {
                                chk = 0;
                            }
                            else
                            {
                                chk = 1;
                            }

                            if (chk > 0)
                            {
                                sendMailValue = SendMail(subject, from, to, toName, cc, bcc, streamProcStmtProjectView, mailSentDate, jobNo);
                            }
                        }
                    }
                }
            }
            else
                retVal = 0;

            if (sendMailValue > 0)
                retVal = 1;

            return retVal;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    private Stream CreateAttachment(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;

                    rowTxt = Convert.ToString(rowTxt).Replace("\n", " ");
                    rowTxt = Convert.ToString(rowTxt).Replace("\r", " ");
                    rowTxt = Convert.ToString(rowTxt).Replace(",", "") + ',';

                    csv += Convert.ToString(rowTxt);
                }
                csv += "\r\n";
            }


            byte[] byteArray = Encoding.ASCII.GetBytes(csv);
            MemoryStream stream = new MemoryStream(byteArray);

            return stream;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private int SendMail(string subject, string from, string to, string toName, string cc, string bcc,
                            Stream streamProcStmtProjectView,
                            string sentDate, string jobNo)
    {
        body = string.Empty;
        int returnVal = 0;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        mail.Subject = subject;
        mail.From = new MailAddress(from);

        if (!string.IsNullOrEmpty(to))
        {
            to = to.TrimEnd(';');
            string[] strTo = to.Split(';');
            foreach (string item in strTo)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.To.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(cc))
        {
            cc = cc.TrimEnd(';');
            string[] strCC = cc.Split(';');
            foreach (string item in strCC)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.CC.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(bcc))
        {
            bcc = bcc.TrimEnd(';');
            string[] strBcc = bcc.Split(';');
            foreach (string item in strBcc)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.Bcc.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(to))
        {
            if (streamProcStmtProjectView != null)
            {
                mail.Attachments.Add(new Attachment(streamProcStmtProjectView, "DMS_Report_" + sentDate + ".csv", "text/csv"));
            }


            mail.IsBodyHtml = true;

            fileName = "~/PROJECT/DMS/StatusReportMail.html";
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#toname#}", toName);
            body = body.Replace("{#jobno#}", jobNo);
            body = body.Replace("{#sentdate#}", sentDate);


            mail.Body = body;

            try
            {
                if (!string.IsNullOrEmpty(to))
                {
                    SmtpServer.Send(mail);
                    returnVal = 1;
                }
                else
                    returnVal = 0;
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    returnVal = 1;

                else
                    returnVal = 0;
            }
        }
        else
            returnVal = 0;


        return returnVal;
    }

    private void SaveDesignDetails()
    {
        try
        {
            recordID = 0;
            jobNo = string.Empty;
            jobUnitID = 0;
            drawingRecordID = 0;
            categoryID = 0;
            description = string.Empty;
            UOM = string.Empty;
            quantity = 0;
            reqdDateByProjectTeam = string.Empty;
            isPlannedID = 0;
            plannedStartDateByDesignTeam = string.Empty;
            plannedCompletionDateByDesignTeam = string.Empty;
            drawingNo = string.Empty;

            clientDrawingNo = string.Empty;
            contractorDrawingNo = string.Empty;

            documentLink = string.Empty;
            drawingRevNo = 0;
            workingStatus = string.Empty;
            expectedCompletionDate = string.Empty;
            responsibleDesignEnggID = 0;
            postingStatusID = 0;
            isApplicableForProduction = 0;
            remarks = string.Empty;

            isDesignEnggFlag = 0;

            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);

            if (!string.IsNullOrEmpty(txtJOBNoToEdit.Text))
                jobNo = txtJOBNoToEdit.Text.ToUpper();

            if (ddlCategoryToEdit.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategoryToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtDescriptionToEdit.Text))
                description = txtDescriptionToEdit.Text.Trim();

            if (!string.IsNullOrEmpty(txtUOMToEdit.Text))
                UOM = txtUOMToEdit.Text.ToUpper().Trim();

            if (!string.IsNullOrEmpty(txtQuantityToEdit.Text))
                quantity = Convert.ToInt32(txtQuantityToEdit.Text);

            if (!string.IsNullOrEmpty(txtDateByProjectTeamToEdit.Text))
                reqdDateByProjectTeam = Convert.ToDateTime(txtDateByProjectTeamToEdit.Text).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtDrawingNumberToEdit.Text))
                drawingNo = txtDrawingNumberToEdit.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtClientDrawingNumberToEdit.Text))
                clientDrawingNo = txtClientDrawingNumberToEdit.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtContractorDrawingNumberToEdit.Text))
                contractorDrawingNo = txtContractorDrawingNumberToEdit.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtDocumentLinkToEdit.Text))
                documentLink = txtDocumentLinkToEdit.Text.Trim();

            drawingRevNo = Convert.ToInt32(ddlDrawingRevisioinNumberToEdit.SelectedValue);


            if (!string.IsNullOrEmpty(txtWorkingStatusToEdit.Text))
                workingStatus = txtWorkingStatusToEdit.Text.Trim();

            if (!string.IsNullOrEmpty(txtRemarksToEdit.Text))
                remarks = txtRemarksToEdit.Text.Trim();

            jobUnitID = Convert.ToInt32(ddlCompanyToEdit.SelectedValue);

            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
            {
                statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);
                isDesignEnggFlag = 1;

                if (!string.IsNullOrEmpty(txtPlannedStartDateByDesignTeamToEdit.Text))
                    plannedStartDateByDesignTeam = Convert.ToDateTime(txtPlannedStartDateByDesignTeamToEdit.Text).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(txtPlannedCompletionDateByDesignTeamToEdit.Text))
                    plannedCompletionDateByDesignTeam = Convert.ToDateTime(txtPlannedCompletionDateByDesignTeamToEdit.Text).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(txtExpectedCompletionDateToEdit.Text))
                    expectedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDateToEdit.Text).ToString("yyyy-MM-dd");

                if (ddlRespDesignEnggToEdit.SelectedIndex > 0)
                    responsibleDesignEnggID = Convert.ToInt32(ddlRespDesignEnggToEdit.SelectedValue);
            }
            else
            {
                drawingRecordID = Convert.ToInt32(ViewState["DRAWING_ID"]);
                statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);
                isDesignEnggFlag = 0;
                plannedStartDateByDesignTeam = string.Empty;
                plannedCompletionDateByDesignTeam = string.Empty;
                plannedCompletionDateByDesignTeam = string.Empty;
                expectedCompletionDate = string.Empty;
                responsibleDesignEnggID = 0;
            }

            int value = 0;
            if (recordID > 0 && jobUnitID > 0)
            {
                value = objProject.UpdateDesignDetail(recordID, statusID, jobNo, jobUnitID, description, UOM, quantity, reqdDateByProjectTeam, categoryID,
                                                                   plannedStartDateByDesignTeam, plannedCompletionDateByDesignTeam,
                                                                   drawingNo, clientDrawingNo,contractorDrawingNo,
                                                                   drawingRecordID, documentLink, drawingRevNo, workingStatus,
                                                                   expectedCompletionDate, responsibleDesignEnggID,
                                                                   remarks, isDesignEnggFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }

            int mailTypeID = 0;
            int mailSentValue = 0;
            int mailValue = 0;

            if (value > 0)
            {
                if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                {
                    if (Convert.ToInt32(ViewState["AMENDMENT_FLAG"]) > 0)
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendededMail);
                    else
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedGeneratedMail);
                }
                else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(ViewState["AMENDMENT_FLAG"]) > 0)
                    {
                        if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(ViewState["DESIGN_RESPONSIBLE_ENGG_ID"]))
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToHimselfMail);
                        else
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.AmendedOpenToOtherMail);
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(ViewState["DESIGN_RESPONSIBLE_ENGG_ID"]))
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToHimselfMail);
                        else
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.EditedOpenToOtherMail);
                    }
                }

                mailSentValue = objDMSSendMail.ProcessAndSendMail(("'" + drawingNo + "'"), recordID, statusID, mailTypeID);
                if (mailSentValue > 0)
                {
                    //mailValue = objProject.UpdateMailSentStatus(recordID, statusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    mailValue = objProject.UpdateDesignMailStatus(("'" + drawingNo + "'"), recordID, statusID, Convert.ToInt32(ViewState["AMENDMENT_COUNT"]), 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                    {
                        if (Convert.ToInt32(ViewState["AMENDMENT_FLAG"]) > 0)
                        {
                            SuccessMessage("Design amended and mail sent successfully...!!!");
                        }
                        else
                        {
                            SuccessMessage("Design updated and mail sent successfully...!!!");
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ViewState["AMENDMENT_FLAG"]) > 0)
                        {
                            SuccessMessage("Design amended and mail sent successfully...!!!");
                        }
                        else
                        {
                            SuccessMessage("Design updated and mail sent successfully...!!!");
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                    {
                        if (Convert.ToInt32(ViewState["AMENDMENT_FLAG"]) > 0)
                        {
                            SuccessMessage("Design amended successfully, please go to list and send mail...!!!");
                        }
                        else
                        {
                            SuccessMessage("Design updated successfully, please go to list and send mail...!!!");
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ViewState["AMENDMENT_FLAG"]) > 0)
                        {
                            SuccessMessage("Design amended successfully, please go to list and send mail...!!!");
                        }
                        else
                        {
                            SuccessMessage("Design updated successfully, please go to list and send mail...!!!");
                        }
                    }
                }

                GetDesignDetailList();
                return;
            }
            else if (value < 0)
            {
                mpeDesignDetails.Show();
                ExceptionMessageDesignDetail("Drawing number already exists, please try with other drawing number...!!!");
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
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

    private void ExceptionMessageDesignDetail(string message)
    {
        pnlDesignDetail.Visible = true;
        lblDesignDetail.Text = message;
        lblDesignDetail.ForeColor = System.Drawing.Color.Red;
    }

    private void ExceptionMessageTimesheet(string message)
    {
        pnlMsgTimesheet.Visible = true;
        lblMsgTimesheet.Text = message;
        lblMsgTimesheet.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    private void HidePanelDesignDetail()
    {
        pnlDesignDetail.Visible = false;
        lblDesignDetail.Text = string.Empty;
    }

    private void ExceptionSubitemsMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
