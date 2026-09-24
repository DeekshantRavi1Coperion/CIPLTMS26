using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PROJECT_DMS_ReviseDesignList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DMSSendMail objDMSSendMail = new DMSSendMail();
    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsUnit = new DataSet();
    //DataSet dsUserTimeDetails = new DataSet();
    DataSet dtDesignDetailsList = new DataSet();
    //DataSet dsDesignStatusSearch = new DataSet();
    DataSet dsDesignEngg = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsDesignChecker = new DataSet();
    DataSet dsDesignCategory = new DataSet();
    DataSet dsDesignJobNo = new DataSet();
    DataSet dsProjectJobNo = new DataSet();
    //DataTable dtAction = new DataTable();
    DataTable dtDrawings = new DataTable();


    string jobNo = string.Empty;
    int jobUnitID = 0;
    int categoryID = 0;
    //string isPlanned = string.Empty;
    string drawingNo = string.Empty;

    string clientDrawingNo = string.Empty;
    string contractorDrawingNo = string.Empty;

    //int postingStatusID = 0;
    int datetTypeID = 0;
    string dateSign = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    //int designCheckerID = 0;
    int responsibleEnggID = 0;
    int createdByID = 0;



    int srNo = 0;
    int amendmentCount = 0;
    int recordID = 0;
    int drawingID = 0;
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

    string additionalAttachmentFile = string.Empty;
    Byte[] additionalAttachmentFileBytes = null;

    //int datetTypeID = 0;
    //string dateSign = string.Empty;
    //string fromDate = string.Empty;
    //string toDate = string.Empty;    
    //int responsibleEnggID = 0;
    //int createdByID = 0;




    //int empRecordID = 0;
    //string projectCategory = string.Empty;
    //string projectNumber = string.Empty;
    //string jobNumber = string.Empty;

    //string drawingCategory = string.Empty;
    //string serialNumber = string.Empty;
    //string size = string.Empty;
    //string rev = string.Empty;
    //int sheets = 0;
    //int drawingTypeID = 0;

    //string startTime = string.Empty;
    //string endTime = string.Empty;
    //string timeSpent = string.Empty;
    //string title = string.Empty;
    //string status = string.Empty;

    //string timesheetDate = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindCompany();
                BindDesignCategoryMainSearch();
                BindCreatedBySearch();
                BindRespinsibleEngSearch();

                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                {
                    hdDesignResponsibleEnggFlag.Value = "1";
                }
                else
                {
                    hdDesignResponsibleEnggFlag.Value = "0";
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
                int rowindex = 0;
                HidePanelDesignDetail();

                if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_ADD_ATT" ||
                    Convert.ToString(e.CommandArgument) == "REVISE")
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

                TextBox txtDrawingLink = gvDesignDetails.Rows[rowindex].FindControl("txtDrawingLink") as TextBox;

                TextBox txtDrawingRevNo = gvDesignDetails.Rows[rowindex].FindControl("txtDrawingRevNo") as TextBox;
                TextBox txtWorkingStatus = gvDesignDetails.Rows[rowindex].FindControl("txtWorkingStatus") as TextBox;

                TextBox txtExpectedCompletionDate = gvDesignDetails.Rows[rowindex].FindControl("txtExpectedCompletionDate") as TextBox;
                ImageButton imgbtnExpectedCompletionDate = gvDesignDetails.Rows[rowindex].FindControl("imgbtnExpectedCompletionDate") as ImageButton;

                TextBox txtTimeSpent = gvDesignDetails.Rows[rowindex].FindControl("txtTimeSpent") as TextBox;
                DropDownList ddlResponsibleDesignEngineer = gvDesignDetails.Rows[rowindex].FindControl("ddlResponsibleDesignEngineer") as DropDownList;
                DropDownList ddlDesignChecker = gvDesignDetails.Rows[rowindex].FindControl("ddlDesignChecker") as DropDownList;
                TextBox txtRemarks = gvDesignDetails.Rows[rowindex].FindControl("txtRemarks") as TextBox;
                Label lblOldRevNo = gvDesignDetails.Rows[rowindex].FindControl("lblOldRevNo") as Label;

                Label lblAdditionalAttachmentName = gvDesignDetails.Rows[rowindex].FindControl("lblAdditionalAttachmentName") as Label;

                ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);
                ViewState["AMENDMENT_FLAG"] = Convert.ToInt32(lblAmendmentFlag.Text);
                ViewState["AMENDMENT_COUNT"] = Convert.ToInt32(lblAmendmentCount.Text);
                ViewState["JOB_NO"] = Convert.ToString(lblJOBNo.Text);
                ViewState["WORKING_STATUS"] = Convert.ToString(txtWorkingStatus.Text);
                ViewState["EXPECTED_COMPLETION_DATE"] = Convert.ToString(txtExpectedCompletionDate.Text);
                ViewState["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(lblDesignResponsibleEnggID.Text);
                ViewState["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text).Trim().ToUpper();
                drawingNo = "'" + Convert.ToString(lblDrawingNo.Text).ToUpper().Trim() + "'";




                if (Convert.ToString(e.CommandArgument) == "VIEW_ADD_ATT")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblRecordID.Text), "ATTACHMENT1", Convert.ToString(lblAdditionalAttachmentName.Text));
                }


                if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    mpeViewInPDF.Show();
                    iframeViewDrawingDetailsInPDF.Attributes.Add("src", "DrawingDetailInPDF.aspx?recordID=" + Convert.ToInt32(lblRecordID.Text) + "");
                }

                else if (Convert.ToString(e.CommandArgument) == "REVISE")
                {
                    hdConfirmValue.Value = "0";
                    hdOldRevNo.Value = Convert.ToString(lblOldRevNo.Text);

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


                    ddlDrawingRevisioinNumberToEdit.SelectedValue = Convert.ToString(Convert.ToInt32(lblOldRevNo.Text) + 1);

                    string txt = string.Empty;
                    if (Convert.ToInt32(ddlDrawingRevisioinNumberToEdit.SelectedValue) < 10)
                        txt = "0" + Convert.ToString(ddlDrawingRevisioinNumberToEdit.SelectedValue);
                    else
                        txt = Convert.ToString(ddlDrawingRevisioinNumberToEdit.SelectedValue);

                    string txts = string.Empty;
                    txtDrawingNumberToEdit.Text = lblDrawingNo.Text;
                    int indx = txtDrawingNumberToEdit.Text.LastIndexOf('.');

                    int len = txtDrawingNumberToEdit.Text.Length;

                    if ((indx + 1) < len)
                    {
                        txts = txtDrawingNumberToEdit.Text.Substring((txtDrawingNumberToEdit.Text.LastIndexOf('.') + 1), (len - indx - 1));
                    }
                    else
                    {
                        txts = string.Empty;
                    }

                    if (string.IsNullOrEmpty(txts))
                    {
                        if (!string.IsNullOrEmpty(lblDrawingNo.Text))
                        {
                            if (lblDrawingNo.Text.EndsWith("."))
                                txtDrawingNumberToEdit.Text = lblDrawingNo.Text + txt;
                            else
                                txtDrawingNumberToEdit.Text = lblDrawingNo.Text + "." + txt;
                        }
                    }
                    else
                    {
                        if (txts.Length == 2)
                        {
                            if (!string.IsNullOrEmpty(lblDrawingNo.Text))
                            {
                                txtDrawingNumberToEdit.Text = lblDrawingNo.Text.Substring(0, indx + 1) + txt;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(lblDrawingNo.Text))
                            {
                                if (lblDrawingNo.Text.EndsWith("."))
                                    txtDrawingNumberToEdit.Text = lblDrawingNo.Text + txt;
                                else
                                    txtDrawingNumberToEdit.Text = lblDrawingNo.Text + "." + txt;
                            }
                        }
                    }





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

                Label lblDesignResponsibleEnggID = e.Row.FindControl("lblDesignResponsibleEnggID") as Label;
                Label lblDesignResponsibleEnggEmpRecordID = e.Row.FindControl("lblDesignResponsibleEnggEmpRecordID") as Label;
                Label lblDesignCheckerID = e.Row.FindControl("lblDesignCheckerID") as Label;
                Label lblDesignCheckerEmpRecordID = e.Row.FindControl("lblDesignCheckerEmpRecordID") as Label;

                ImageButton imgBtnSendMail = (ImageButton)e.Row.FindControl("imgBtnSendMail");
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

                TextBox txtDrawingLink = e.Row.FindControl("txtDrawingLink") as TextBox;

                TextBox txtDrawingRevNo = e.Row.FindControl("txtDrawingRevNo") as TextBox;
                TextBox txtWorkingStatus = e.Row.FindControl("txtWorkingStatus") as TextBox;

                TextBox txtExpectedCompletionDate = e.Row.FindControl("txtExpectedCompletionDate") as TextBox;
                ImageButton imgbtnExpectedCompletionDate = e.Row.FindControl("imgbtnExpectedCompletionDate") as ImageButton;

                TextBox txtTimeSpent = e.Row.FindControl("txtTimeSpent") as TextBox;
                DropDownList ddlResponsibleDesignEngineer = e.Row.FindControl("ddlResponsibleDesignEngineer") as DropDownList;
                DropDownList ddlDesignChecker = e.Row.FindControl("ddlDesignChecker") as DropDownList;
                TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;

                ImageButton imgBtnRevise = e.Row.FindControl("imgBtnRevise") as ImageButton;

                Label lblAdditionalAttachmentName = e.Row.FindControl("lblAdditionalAttachmentName") as Label;

                ImageButton btnViewAddAtt = e.Row.FindControl("btnViewAddAtt") as ImageButton;
                btnViewAddAtt.Visible = false;

                DataTable dt = new DataTable();
                if (Session["dtDesignDetailsList"] != null)
                    dt = (DataTable)Session["dtDesignDetailsList"];


                if (!string.IsNullOrEmpty(Convert.ToString(lblAdditionalAttachmentName.Text)))
                {
                    btnViewAddAtt.Visible = true;
                }


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


                ddlResponsibleDesignEngineer.Enabled = false;
                ddlDesignChecker.Enabled = false;

                txtQuantity.Enabled = false;
                txtDrawingRevNo.Enabled = false;
                imgBtnRevise.Visible = false;

                if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                {
                    txtPlannedStartDateByDesignTeam.Width = Unit.Percentage(100);
                    txtPlannedCompletionDateByDesignTeam.Width = Unit.Percentage(100);
                    txtExpectedCompletionDate.Width = Unit.Percentage(100);

                    txtPlannedStartDateByDesignTeam.Enabled = false;
                    txtPlannedCompletionDateByDesignTeam.Enabled = false;
                    txtExpectedCompletionDate.Enabled = false;

                    if (Convert.ToInt32(lblCreatedByID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                        Convert.ToInt32(lblPMID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                    {
                        imgBtnRevise.Visible = true;
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


    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/AddDesign.aspx");
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

                int count = 0;
                foreach (DataRow dr in dsEmployee.Tables[0].Select("EMP_RECORD_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                {
                    count++;
                }

                if (count > 0)
                {
                    ddlCreatedByMainSearch.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                }
                else
                {
                    ddlCreatedByMainSearch.SelectedIndex = 0;
                }
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
                ddlRespDesignEnggMainSearch.DataSource = dsDesignEngg.Tables[0];
                ddlRespDesignEnggMainSearch.DataTextField = "DESIGN_RESPONSIBLE_ENGG";
                ddlRespDesignEnggMainSearch.DataValueField = "DESIGN_RESPONSIBLE_ENGG_ID";
                ddlRespDesignEnggMainSearch.DataBind();
                ddlRespDesignEnggMainSearch.Items.Insert(0, "All");
                ddlRespDesignEnggMainSearch.SelectedIndex = 0;
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
            drawingNo = string.Empty;
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

            if (!string.IsNullOrEmpty(txtDrawingNoMainSearch.Text))
                drawingNo = txtDrawingNoMainSearch.Text.Trim().ToUpper();

            if (ddlRespDesignEnggMainSearch.SelectedIndex > 0)
                responsibleEnggID = Convert.ToInt32(ddlRespDesignEnggMainSearch.SelectedValue);

            if (ddlCreatedByMainSearch.SelectedIndex > 0)
                createdByID = Convert.ToInt32(ddlCreatedByMainSearch.SelectedValue);

            dtDesignDetailsList = objProject.GetDesignDetailListForRevision(datetTypeID, dateSign, fromDate, toDate, categoryID, jobNo,
                                                                            drawingNo, responsibleEnggID, createdByID);

            if (dtDesignDetailsList.Tables.Count > 0 && dtDesignDetailsList.Tables[0].Rows.Count > 0)
            {
                Session["dtDesignDetailsList"] = dtDesignDetailsList.Tables[0];
                gvDesignDetails.DataSource = dtDesignDetailsList.Tables[0];
                gvDesignDetails.DataBind();
            }
            else
            {
                Session["dtDesignDetailsList"] = null;
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


            dtDrawings.Columns.Add("DRAWING_ID", typeof(string));
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

    private void SaveDesignDetails()
    {
        try
        {
            recordID = 0;
            drawingID = 0;
            jobNo = string.Empty;
            jobUnitID = 0;
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
            isApplicableForProduction = 0;
            remarks = string.Empty;

            additionalAttachmentFile = string.Empty;
            additionalAttachmentFileBytes = null;

            isDesignEnggFlag = 0;

            if (ViewState["RECORD_ID"] != null)
                recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            else
                recordID = 0;

            //if (ViewState["DRAWING_ID"] != null)
            //    drawingID = Convert.ToInt32(ViewState["DRAWING_ID"]);
            //else
            //    drawingID = 0;

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


            if (uploadFileAddAttachment.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAddAttachment.PostedFile.FileName))
                {
                    string[] str = uploadFileAddAttachment.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    additionalAttachmentFile = str[str.Length - 1];
                    additionalAttachmentFileBytes = GetFileBytes(uploadFileAddAttachment.PostedFile.FileName, uploadFileAddAttachment.PostedFile.InputStream);
                }
            }

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
                statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);

                if (ViewState["DRAWING_ID"] != null)
                    drawingID = Convert.ToInt32(ViewState["DRAWING_ID"]);


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
                value = objProject.ReviseDesignDetail(recordID, drawingID, statusID, jobNo, jobUnitID, description, UOM, quantity, reqdDateByProjectTeam, categoryID,
                                                      plannedStartDateByDesignTeam, plannedCompletionDateByDesignTeam,
                                                      drawingNo, clientDrawingNo, contractorDrawingNo,
                                                      documentLink, drawingRevNo,
                                                      workingStatus, expectedCompletionDate, responsibleDesignEnggID, remarks, isDesignEnggFlag,
                                                      additionalAttachmentFile, additionalAttachmentFileBytes,
                                                      Convert.ToInt32(Session["EMP_RECORD_ID"]));
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
                    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);
                }
                else if (statusID == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == Convert.ToInt32(ViewState["DESIGN_RESPONSIBLE_ENGG_ID"]))
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToHimselfMail);
                    else
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToOtherMail);
                }

                mailSentValue = objDMSSendMail.ProcessAndSendMail(("'" + drawingNo + "'"), value, statusID, mailTypeID);
                if (mailSentValue > 0)
                {
                    mailValue = objProject.UpdateDesignMailStatus(("'" + drawingNo + "'"), value, statusID, Convert.ToInt32(ViewState["AMENDMENT_COUNT"]), 0,
                                                                  Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    SuccessMessage("Design revised and mail sent successfully...!!!");
                }
                else
                {
                    SuccessMessage("Design revised successfully, please go to list and send mail...!!!");
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
                case ".dxf":
                    GSTContentType = "application/dxf";
                    break;
                case ".DXF":
                    GSTContentType = "application/DXF";
                    break;
                case ".dwg":
                    GSTContentType = "application/dwg";
                    break;
                case ".DWG":
                    GSTContentType = "application/DWG";
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
                ExceptionMessage("GST File format not recognised. Upload Image/PDF/DXF/DWG formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
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
