using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class COMPLAINT_LOG_ComplaintLogListNew : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.ComplaintLog objComplaintLog = new BAL.ComplaintLog();

    DataSet dsComplaintLogList = new DataSet();
    DataSet dsComplaintLogDetails = new DataSet();
    DataSet dsStatus = new DataSet();
    DataSet dsEmployee = new DataSet();
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string customerName = string.Empty;
    string complaintLogNo = string.Empty;
    int statusID = 0;
    int empRecordID = 0;

    DataSet dsCustomer = new DataSet();
    DataSet dsVendor = new DataSet();
    DataSet dsComplaintType = new DataSet();

    DataSet dsSanctionNo = new DataSet();

    BAL.Common objCommon = new BAL.Common();
    DataSet dsComplaintLog = new DataSet();
    DataSet dsResponsiblePerson = new DataSet();
    DataSet dsResponsibleDept = new DataSet();
    DataSet dsBusinessUnit = new DataSet();

    int complaintLogID = 0;
    int responsibleDepartment = 0;
    int responsiblePserson = 0;
    string respPersonLessonLearnt = string.Empty;
    string respDeptHODLessonLearnt = string.Empty;
    string serviceDeptHODLessonLearnt = string.Empty;

    int businessUnitID = 0;
    string complaintDescription = string.Empty;
    string manufacturerName = string.Empty;
    string proposedActions = string.Empty;
    string rootCause = string.Empty;
    string targetCompletionDate = string.Empty;
    string actualCompletionDate = string.Empty;


    string fileUploadVisitReportOneFileName = string.Empty;
    string fileUploadVisitReportTwoFileName = string.Empty;
    string fileUploadVisitReportThreeFileName = string.Empty;

    string attachmentOneFileName = string.Empty;
    string attachmentTwoFileName = string.Empty;
    string correctiveAction = string.Empty;

    int statementID = 0;
    string remarks = string.Empty;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dsComplaintLogList"] = null;
                BindStatus();
                BindEmployee();

                hdTargetCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtTargetCompletionDate.Text = hdTargetCompletionDate.Value.ToString();

                hdActualCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtActualCompletionDate.Text = hdActualCompletionDate.Value.ToString();

                BindDepartment();
                GetBusinessUnit();

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["complaintlogno"])))
                    txtComplaintLogNo.Text = Convert.ToString(Request.QueryString["complaintlogno"]);
                else
                    txtComplaintLogNo.Text = string.Empty;


                GetComplaintLogList();

                GetSanctionNo();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        lblMsg.Visible = false;
        GetComplaintLogList();
    }

    protected void gvComplaintLogList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                string queryString = string.Empty;
                int rowindex = 0;

                if (e.CommandArgument == "STATUS" || e.CommandArgument == "PROPERTIES" || e.CommandArgument == "ViewATTACHMENT1" || e.CommandArgument == "ViewATTACHMENT2" || e.CommandArgument == "ViewATTACHMENT3" || e.CommandArgument == "ViewATTACHMENT4" || e.CommandArgument == "SEND_MAIL" || e.CommandArgument == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                else if (e.CommandArgument == "ACTION")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblComplaintLogID = gvComplaintLogList.Rows[rowindex].FindControl("lblComplaintLogID") as Label;
                Label lblComplaintLogNo = gvComplaintLogList.Rows[rowindex].FindControl("lblComplaintLogNo") as Label;
                Label lblStatusID = gvComplaintLogList.Rows[rowindex].FindControl("lblStatusID") as Label;

                Label lblRespDeptID = gvComplaintLogList.Rows[rowindex].FindControl("lblRespDeptID") as Label;
                Label lblRespPersonID = gvComplaintLogList.Rows[rowindex].FindControl("lblRespPersonID") as Label;

                int complaintLogID = Convert.ToInt32(lblComplaintLogID.Text);
                int statusID = Convert.ToInt32(lblStatusID.Text);


                PublicValuesforComplaintLogList.complaintLogId = complaintLogID;
                PublicValuesforComplaintLogList.complaintLogNo = lblComplaintLogNo.Text;
                PublicValuesforComplaintLogList.statusID = statusID;

                if (e.CommandArgument == "PROPERTIES")
                {

                    btnSubmit.Visible = true;
                    hdPropertyValue.Value = "1";
                    //New
                    if (statusID == 1)
                    {
                        Response.Redirect("~/COMPLAINT_LOG/ComplaintLogNewOne.aspx?complaintlogid=" + complaintLogID);
                    }
                    //Update assigned-dept
                    else if (statusID == 2)
                    {
                        PublicValuesforComplaintLogList.actID = 2;
                        BindComplaintLogDetails(complaintLogID);

                        if (ddlResponsibleDepartment.SelectedValue == "8" || ddlResponsibleDepartment.SelectedValue == "9" || ddlResponsibleDepartment.SelectedValue == "18")
                        {
                            lblTargetDate.Visible = true;
                            hdTargetCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                            txtTargetCompletionDate.Text = hdTargetCompletionDate.Value.ToString();
                            txtTargetCompletionDate.Visible = true;
                            imgbtnTargetCompletionDate.Visible = true;
                        }
                        else
                        {
                            lblTargetDate.Visible = false;
                            hdTargetCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                            txtTargetCompletionDate.Text = hdTargetCompletionDate.Value.ToString();
                            txtTargetCompletionDate.Visible = false;
                            imgbtnTargetCompletionDate.Visible = false;
                        }


                        pnlResolve.Visible = false;
                        ddlResponsibleDepartment.Enabled = true;
                        ddlResponsiblePerson.Enabled = false;

                        pnlVisitReportSummary.Visible = false;
                        pnlAttachFiles.Visible = false;
                        pnlViewComplaintFiles.Visible = true;
                        pnlViewRelovedFiles.Visible = false;

                        pnlRespDeptHODLessonLearnt.Visible = false;
                        pnlServiceDeptHODLessonLearnt.Visible = false;

                        btnSubmit.Text = "Update Forwarded Department";
                        ModalPopupExtender1.Show();
                    }
                    //Person-Assigned
                    else if (statusID == 3)
                    {
                        PublicValuesforComplaintLogList.actID = 3;
                        BindComplaintLogDetails(complaintLogID);

                        ddlResponsibleDepartment.Enabled = false;
                        ddlResponsiblePerson.Enabled = true;

                        lblTargetDate.Visible = true;
                        txtTargetCompletionDate.Visible = true;
                        imgbtnTargetCompletionDate.Visible = true;

                        pnlResolve.Visible = false;
                        pnlVisitReportSummary.Visible = false;
                        pnlAttachFiles.Visible = false;

                        pnlViewComplaintFiles.Visible = true;
                        pnlViewRelovedFiles.Visible = false;

                        pnlRespDeptHODLessonLearnt.Visible = false;
                        pnlServiceDeptHODLessonLearnt.Visible = false;

                        btnSubmit.Text = "Update Assigned Person";
                        ModalPopupExtender1.Show();
                    }
                    //Resolved
                    else if (statusID == 4)
                    {
                        PublicValuesforComplaintLogList.actID = 4;
                        BindComplaintLogDetails(complaintLogID);

                        pnlNewComplaint.Visible = true;

                        pnlAssignment.Visible = true;
                        pnlAssignment.Enabled = false;
                        
                        pnlResolve.Visible = true;

                        pnlVisitReportSummary.Visible = true;
                        pnlAttachFiles.Visible = true;

                        pnlRespDeptHODLessonLearnt.Visible = false;
                        pnlServiceDeptHODLessonLearnt.Visible = false;

                        pnlViewComplaintFiles.Visible = true;
                        pnlViewRelovedFiles.Visible = false;
                        btnSubmit.Text = "Update Solution";
                        ModalPopupExtender1.Show();
                    }
                }
                else if (e.CommandArgument == "STATUS" || e.CommandArgument == "ACTION")
                {
                    hdPropertyValue.Value = "0";
                    //New   ->   (for Dept-Assignment)
                    if (statusID == 1)
                    {
                        PublicValuesforComplaintLogList.actID = 2;
                        BindComplaintLogDetails(complaintLogID);

                        ddlResponsibleDepartment.Enabled = true;
                        ddlResponsiblePerson.Enabled = false;

                        pnlVisitReportSummary.Visible = false;
                        pnlResolve.Visible = false;
                        pnlAttachFiles.Visible = false;

                        pnlViewComplaintFiles.Visible = true;
                        pnlViewRelovedFiles.Visible = false;

                        pnlRespDeptHODLessonLearnt.Visible = false;
                        pnlServiceDeptHODLessonLearnt.Visible = false;

                        btnSubmit.Text = "Forward Responsible Department";
                        ModalPopupExtender1.Show();
                    }
                    //Dept-Assigned ->  (for Person-Assignment)
                    else if (statusID == 2)
                    {
                        PublicValuesforComplaintLogList.actID = 3;
                        BindComplaintLogDetails(complaintLogID);

                        ddlResponsibleDepartment.Enabled = false;

                        lblTargetDate.Visible = true;
                        hdTargetCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                        txtTargetCompletionDate.Text = hdTargetCompletionDate.Value.ToString();
                        txtTargetCompletionDate.Visible = true;
                        imgbtnTargetCompletionDate.Visible = true;

                        ddlResponsiblePerson.Enabled = true;

                        pnlVisitReportSummary.Visible = false;
                        pnlResolve.Visible = false;
                        pnlAttachFiles.Visible = false;
                        pnlViewComplaintFiles.Visible = true;
                        pnlViewRelovedFiles.Visible = false;

                        pnlRespDeptHODLessonLearnt.Visible = false;
                        pnlServiceDeptHODLessonLearnt.Visible = false;

                        btnSubmit.Text = "Assign Responsible Person";
                        ModalPopupExtender1.Show();
                    }
                    //Person-Assigned   ->   (for-Solution)
                    else if (statusID == 3)
                    {
                        PublicValuesforComplaintLogList.actID = 4;
                        BindComplaintLogDetails(complaintLogID);

                        pnlNewComplaint.Visible = true;

                        pnlAssignment.Visible = true;
                        pnlAssignment.Enabled = false;

                        pnlResolve.Visible = true;
                        pnlVisitReportSummary.Visible = true;
                        pnlAttachFiles.Visible = true;

                        pnlViewComplaintFiles.Visible = true;
                        pnlViewRelovedFiles.Visible = false;

                        pnlRespDeptHODLessonLearnt.Visible = false;
                        pnlServiceDeptHODLessonLearnt.Visible = false;

                        btnSubmit.Text = "Submit Complaint Request Solution";
                        ModalPopupExtender1.Show();
                    }
                    //Resolved  ->   (for-Approval)
                    else if (statusID == 4)
                    {
                        PublicValuesforComplaintLogList.actID = 5;
                        BindComplaintLogDetails(complaintLogID);

                        pnlNewComplaint.Visible = true;

                        pnlAssignment.Visible = true;
                        pnlAssignment.Enabled = false;
                        imgbtnTargetCompletionDate.Visible = false;
                        imgBtnActualCompletionDate.Visible = false;

                        pnlResolve.Visible = true;
                        pnlResolve.Enabled = false;

                        pnlVisitReportSummary.Visible = false;
                        pnlAttachFiles.Visible = false;

                        pnlRespDeptHODLessonLearnt.Visible = true;
                        pnlServiceDeptHODLessonLearnt.Visible = false;

                        pnlRespDeptHODLessonLearnt.Visible = true;
                        pnlServiceDeptHODLessonLearnt.Visible = false;

                        pnlViewComplaintFiles.Visible = true;
                        pnlViewRelovedFiles.Visible = true;

                        if (Convert.ToInt32(lblRespDeptID.Text) == 8 || Convert.ToInt32(lblRespDeptID.Text) == 9 || Convert.ToInt32(lblRespDeptID.Text) == 18)
                            btnSubmit.Text = "Approve And Close Complaint Request";
                        else
                            btnSubmit.Text = "Approve Complaint Request";

                        ModalPopupExtender1.Show();

                    }
                    //Approved  ->   (for-closing)
                    else if (statusID == 5)
                    {
                        PublicValuesforComplaintLogList.actID = 6;
                        BindComplaintLogDetails(complaintLogID);

                        pnlNewComplaint.Visible = true;

                        pnlAssignment.Visible = true;
                        pnlAssignment.Enabled = false;
                        imgbtnTargetCompletionDate.Visible = false;
                        imgBtnActualCompletionDate.Visible = false;

                        pnlResolve.Visible = true;
                        pnlResolve.Enabled = false;

                        pnlVisitReportSummary.Visible = false;
                        pnlAttachFiles.Visible = false;

                        pnlRespDeptHODLessonLearnt.Visible = false;
                        pnlServiceDeptHODLessonLearnt.Visible = true;

                        pnlViewComplaintFiles.Visible = true;
                        pnlViewRelovedFiles.Visible = true;


                        if (Convert.ToInt32(lblRespDeptID.Text) == 8 || Convert.ToInt32(lblRespDeptID.Text) == 9 || Convert.ToInt32(lblRespDeptID.Text) == 18)
                            btnSubmit.Text = "Approve And Close Complaint Request";
                        else
                            btnSubmit.Text = "Close Complaint Request";

                        ModalPopupExtender1.Show();
                    }
                }

                else if (e.CommandArgument == "ViewATTACHMENT1")
                    ViewAttachedFilesNew01(complaintLogID, "ATTACHMENT1");

                else if (e.CommandArgument == "ViewATTACHMENT2")
                    ViewAttachedFilesNew01(complaintLogID, "ATTACHMENT2");

                else if (e.CommandArgument == "ViewATTACHMENT3")
                    ViewAttachedFilesNew01(complaintLogID, "ATTACHMENT3");

                else if (e.CommandArgument == "ViewATTACHMENT4")
                    ViewAttachedFilesNew01(complaintLogID, "ATTACHMENT4");




                else if (e.CommandArgument == "SEND_MAIL")
                {
                    int value = SendMail(complaintLogID);
                    if (value > 0)
                    {
                        int actID = 0;
                        if (statusID == 1)
                            actID = 1;
                        if (statusID == 2)
                            actID = 2;
                        if (statusID == 3)
                            actID = 3;
                        if (statusID == 4)
                            actID = 4;
                        if (statusID == 5)
                            actID = 5;
                        if (statusID == 6)
                            actID = 6;

                        int mailvalue = objComplaintLog.UpdateComplaintLogMailStatus(complaintLogID, actID);
                        if (mailvalue > 0)
                            SuccessMessage("Mail sent successfully.");

                        GetComplaintLogList();
                    }
                }

                else if (e.CommandArgument == "ViewDETAIL")
                {
                    ModalPopupExtender4.Show();
                    iframeViewComplaintLogInPDF.Attributes.Add("src", "ComplaintLogInPDFNew.aspx?complaintLogID=" + Convert.ToString(complaintLogID));
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

    protected void gvComplaintLogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");
                Button btnAction = (Button)e.Row.FindControl("btnAction");
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");

                Label lblComplaintLogID = (Label)e.Row.FindControl("lblComplaintLogID");
                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");
                Label lblRespPersonID = (Label)e.Row.FindControl("lblRespPersonID");

                int statusId = Convert.ToInt32(lblStatusID.Text);
                int createdBy = Convert.ToInt32(lblCreatedBy.Text);
                int serviceHodID = 0;
                int deptHODID = 0;
                int personID = Convert.ToInt32(lblRespPersonID.Text);


                Label lblRespDeptID = (Label)e.Row.FindControl("lblRespDeptID");
                Label lblIsNewMailSent = (Label)e.Row.FindControl("lblIsNewMailSent");
                Label lblIsDeptAssignedMailSent = (Label)e.Row.FindControl("lblIsDeptAssignedMailSent");
                Label lblIsPersonAssignedMailSent = (Label)e.Row.FindControl("lblIsPersonAssignedMailSent");
                Label lblIsResolvedMailSent = (Label)e.Row.FindControl("lblIsResolvedMailSent");
                Label lblIsApprovedMailSent = (Label)e.Row.FindControl("lblIsApprovedMailSent");
                Label lblIsClosedMailSent = (Label)e.Row.FindControl("lblIsClosedMailSent");
                ImageButton imgbtnSendMail = (ImageButton)e.Row.FindControl("imgbtnSendMail");

                #region ATTACHMENS

                Label lblAttachment1 = (Label)e.Row.FindControl("lblAttachment1");
                ImageButton btnAttachment1 = (ImageButton)e.Row.FindControl("btnAttachment1");

                Label lblAttachment2 = (Label)e.Row.FindControl("lblAttachment2");
                ImageButton btnAttachment2 = (ImageButton)e.Row.FindControl("btnAttachment2");

                Label lblAttachment3 = (Label)e.Row.FindControl("lblAttachment3");
                ImageButton btnAttachment3 = (ImageButton)e.Row.FindControl("btnAttachment3");

                Label lblAttachment4 = (Label)e.Row.FindControl("lblAttachment4");
                ImageButton btnAttachment4 = (ImageButton)e.Row.FindControl("btnAttachment4");

                string Attachment1Extn = string.Empty;
                string Attachment2Extn = string.Empty;

                string Attachment3Extn = string.Empty;
                string Attachment4Extn = string.Empty;

                if (!string.IsNullOrEmpty(lblAttachment1.Text))
                {
                    Attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
                    if (Attachment1Extn == "jpg" || Attachment1Extn == "jepg" || Attachment1Extn == "bmp" || Attachment1Extn == "png" || Attachment1Extn == "gif" || Attachment1Extn == "JPG" || Attachment1Extn == "JPEG" || Attachment1Extn == "BMP" || Attachment1Extn == "PNG" || Attachment1Extn == "GIF")
                    {
                        btnAttachment1.ImageUrl = "~/Images/imgicon1.png";
                        btnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                    else if (Attachment1Extn == "pdf" || Attachment1Extn == "PDF")
                    {
                        btnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                        btnAttachment1.ToolTip = lblAttachment1.Text;
                    }

                    else if (Attachment1Extn == "msg" || Attachment1Extn == "MSG")
                    {
                        btnAttachment1.ImageUrl = "~/Images/outlook.png";
                        btnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                }
                else
                {
                    btnAttachment1.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblAttachment2.Text))
                {
                    Attachment2Extn = Convert.ToString(lblAttachment2.Text).Split('.').Last();
                    if (Attachment2Extn == "jpg" || Attachment2Extn == "jepg" || Attachment2Extn == "bmp" || Attachment2Extn == "png" || Attachment2Extn == "gif" || Attachment2Extn == "JPG" || Attachment2Extn == "JPEG" || Attachment2Extn == "BMP" || Attachment2Extn == "PNG" || Attachment2Extn == "GIF")
                    {
                        btnAttachment2.ImageUrl = "~/Images/imgicon1.png";
                        btnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (Attachment2Extn == "pdf" || Attachment2Extn == "PDF")
                    {
                        btnAttachment2.ImageUrl = "~/Images/pdficon1.png";
                        btnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (Attachment2Extn == "msg" || Attachment2Extn == "MSG")
                    {
                        btnAttachment2.ImageUrl = "~/Images/outlook.png";
                        btnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                }
                else
                {
                    btnAttachment2.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblAttachment3.Text))
                {
                    Attachment3Extn = Convert.ToString(lblAttachment3.Text).Split('.').Last();
                    if (Attachment3Extn == "jpg" || Attachment3Extn == "jepg" || Attachment3Extn == "bmp" || Attachment3Extn == "png" || Attachment3Extn == "gif" || Attachment3Extn == "JPG" || Attachment3Extn == "JPEG" || Attachment3Extn == "BMP" || Attachment3Extn == "PNG" || Attachment3Extn == "GIF")
                    {
                        btnAttachment3.ImageUrl = "~/Images/imgicon1.png";
                        btnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                    else if (Attachment3Extn == "pdf" || Attachment3Extn == "PDF")
                    {
                        btnAttachment3.ImageUrl = "~/Images/pdficon1.png";
                        btnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                    else if (Attachment3Extn == "msg" || Attachment3Extn == "MSG")
                    {
                        btnAttachment3.ImageUrl = "~/Images/outlook.png";
                        btnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                }
                else
                {
                    btnAttachment3.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblAttachment4.Text))
                {
                    Attachment4Extn = Convert.ToString(lblAttachment4.Text).Split('.').Last();
                    if (Attachment4Extn == "jpg" || Attachment4Extn == "jepg" || Attachment4Extn == "bmp" || Attachment4Extn == "png" || Attachment4Extn == "gif" || Attachment4Extn == "JPG" || Attachment4Extn == "JPEG" || Attachment4Extn == "BMP" || Attachment4Extn == "PNG" || Attachment4Extn == "GIF")
                    {
                        btnAttachment4.ImageUrl = "~/Images/imgicon1.png";
                        btnAttachment4.ToolTip = lblAttachment4.Text;
                    }
                    else if (Attachment4Extn == "pdf" || Attachment4Extn == "PDF")
                    {
                        btnAttachment4.ImageUrl = "~/Images/pdficon1.png";
                        btnAttachment4.ToolTip = lblAttachment4.Text;
                    }
                }
                else
                {
                    btnAttachment4.Visible = false;
                }

                #endregion

                dsComplaintLogDetails = objComplaintLog.GetComplaintLogInfoNew(Convert.ToInt32(lblComplaintLogID.Text));
                if (dsComplaintLogDetails.Tables.Count > 0)
                {
                    //SERVICE-HEAD
                    if (dsComplaintLogDetails.Tables[1].Rows.Count > 0)
                    {
                        if (dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_EMP_RECORD_ID"] != DBNull.Value)
                            serviceHodID = Convert.ToInt32(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_EMP_RECORD_ID"]);
                    }

                    //Responsible-dept-hod
                    if (dsComplaintLogDetails.Tables[2].Rows.Count > 0)
                    {
                        if (dsComplaintLogDetails.Tables[2].Rows[0]["HOD_EMP_RECORD_ID"] != DBNull.Value)
                            deptHODID = Convert.ToInt32(dsComplaintLogDetails.Tables[2].Rows[0]["HOD_EMP_RECORD_ID"]);
                    }
                }

                imgProperties.Visible = false;
                btnAction.Visible = false;
                imgStatus.Enabled = false;


                //New
                if (statusId == 1)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/New02.png";
                    imgStatus.ToolTip = "New";
                    btnAction.Text = "Assign/Fowrard Dept";
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == createdBy)
                    {
                        imgProperties.Visible = true;
                        btnAction.Visible = false;
                        imgStatus.Enabled = false;

                        if (Convert.ToInt32(lblIsNewMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "New complaint request mail";
                        }
                    }
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == serviceHodID)
                    {
                        imgProperties.Visible = false;
                        btnAction.Visible = true;
                        imgStatus.Enabled = true;
                    }
                }
                //Assigned-Dept
                else if (statusId == 2)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Department01.png";
                    imgStatus.ToolTip = "Forwarded to Department";
                    btnAction.Text = "Assign Person";
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == serviceHodID)
                    {
                        imgProperties.Visible = true;
                        btnAction.Visible = false;
                        imgStatus.Enabled = false;

                        if (Convert.ToInt32(lblIsDeptAssignedMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "Dept. forwarded mail";
                        }
                    }
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == deptHODID)
                    {
                        imgProperties.Visible = false;
                        btnAction.Visible = true;
                        imgStatus.Enabled = true;
                    }
                }
                //Assigned-Person
                else if (statusId == 3)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Person01.png";
                    imgStatus.ToolTip = "Responsible Person Assigned";
                    btnAction.Text = "Submit Solution";
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == deptHODID)
                    {
                        imgProperties.Visible = true;
                        btnAction.Visible = false;
                        imgStatus.Enabled = false;

                        if (Convert.ToInt32(lblIsPersonAssignedMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "Responsible Person assignment mail";
                        }
                    }
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == personID)
                    {
                        imgProperties.Visible = false;
                        btnAction.Visible = true;
                        imgStatus.Enabled = true;
                    }
                }
                //Resolved
                else if (statusId == 4)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Resolved01.png";
                    imgStatus.ToolTip = "Complaint Request Resolved";

                    if (!string.IsNullOrEmpty(lblRespDeptID.Text))
                    {
                        if (Convert.ToInt32(lblRespDeptID.Text) == 8 || Convert.ToInt32(lblRespDeptID.Text) == 9 || Convert.ToInt32(lblRespDeptID.Text) == 18)
                            btnAction.Text = "Approve & Close";
                        else
                            btnAction.Text = "Approve";
                    }
                    else
                        btnAction.Text = "Approve";


                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == personID)
                    {
                        imgProperties.Visible = true;
                        btnAction.Visible = false;
                        imgStatus.Enabled = false;

                        if (Convert.ToInt32(lblIsResolvedMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "Submitted Solution mail";
                        }
                    }
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == deptHODID)
                    {
                        imgProperties.Visible = false;
                        btnAction.Visible = true;
                        imgStatus.Enabled = true;
                    }
                }
                //Approved
                else if (statusId == 5)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";

                    imgStatus.ToolTip = "Approved";

                    imgProperties.Visible = false;
                    btnAction.Visible = false;
                    imgStatus.Enabled = false;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == deptHODID)
                    {
                        if (Convert.ToInt32(lblIsApprovedMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "Approval Solution mail";
                        }
                    }
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == serviceHodID)
                    {
                        btnAction.Text = "Close";
                        imgProperties.Visible = false;
                        btnAction.Visible = true;
                        imgStatus.Enabled = true;
                    }
                }
                //Closed
                else if (statusId == 6)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Closed01.png";
                    imgStatus.ToolTip = "Closed";
                    imgProperties.Visible = false;
                    btnAction.Visible = false;
                    imgStatus.Enabled = false;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == serviceHodID)
                    {
                        if (Convert.ToInt32(lblIsClosedMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "Approval Solution mail";
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

    protected void gvComplaintLogList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComplaintLogList.PageIndex = e.NewPageIndex;
        GetComplaintLogList();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int actID = Convert.ToInt32(PublicValuesforComplaintLogList.actID);
        int statusID = PublicValuesforComplaintLogList.statusID;
        btnSubmit.Visible = false;


        if (Convert.ToInt32(hdPropertyValue.Value) == 1)
        {
            btnSubmit.Visible = true;
            UpdateComplaintLogStatus(PublicValuesforComplaintLogList.complaintLogId, PublicValuesforComplaintLogList.complaintLogNo, PublicValuesforComplaintLogList.statusID, PublicValuesforComplaintLogList.actID);
        }
        else
        {
            if (actID == 2)
            {
                if (statusID == 1)
                {
                    btnSubmit.Visible = true;
                    UpdateComplaintLogStatus(PublicValuesforComplaintLogList.complaintLogId, PublicValuesforComplaintLogList.complaintLogNo, PublicValuesforComplaintLogList.statusID, PublicValuesforComplaintLogList.actID);
                }
                else if (statusID == 2)
                {
                    SuccessMessage("Already assigned to responsible department.");
                }
                else if (statusID == 3)
                {
                    SuccessMessage("Already assigned to responsible person.");
                }
                else if (statusID == 4)
                {
                    SuccessMessage("Solution already submitted.");
                }
                else if (statusID == 5)
                {
                    SuccessMessage("Already approved.");
                }
                else if (statusID == 6)
                {
                    SuccessMessage("Already closed.");
                }
            }

            else if (actID == 3)
            {
                if (statusID == 2)
                {
                    btnSubmit.Visible = true;
                    UpdateComplaintLogStatus(PublicValuesforComplaintLogList.complaintLogId, PublicValuesforComplaintLogList.complaintLogNo, PublicValuesforComplaintLogList.statusID, PublicValuesforComplaintLogList.actID);
                }
                else if (statusID == 3)
                {
                    SuccessMessage("Already assigned to responsible person.");
                }
                else if (statusID == 4)
                {
                    SuccessMessage("Solution already submitted.");
                }
                else if (statusID == 5)
                {
                    SuccessMessage("Already approved.");
                }
                else if (statusID == 6)
                {
                    SuccessMessage("Already closed.");
                }
            }

            else if (actID == 4)
            {
                if (statusID == 3)
                {
                    btnSubmit.Visible = true;
                    UpdateComplaintLogStatus(PublicValuesforComplaintLogList.complaintLogId, PublicValuesforComplaintLogList.complaintLogNo, PublicValuesforComplaintLogList.statusID, PublicValuesforComplaintLogList.actID);
                }
                else if (statusID == 4)
                {
                    SuccessMessage("Solution already submmited.");
                }
                else if (statusID == 5)
                {
                    SuccessMessage("Already approved.");
                }
                else if (statusID == 6)
                {
                    SuccessMessage("Already closed.");
                }
            }

            else if (actID == 5)
            {
                if (statusID == 4)
                {
                    btnSubmit.Visible = true;
                    UpdateComplaintLogStatus(PublicValuesforComplaintLogList.complaintLogId, PublicValuesforComplaintLogList.complaintLogNo, PublicValuesforComplaintLogList.statusID, PublicValuesforComplaintLogList.actID);
                }
                else if (statusID == 5)
                {
                    SuccessMessage("Already approved.");
                }
                else if (statusID == 6)
                {
                    SuccessMessage("Already closed.");
                }
            }

            else if (actID == 6)
            {
                if (statusID == 5)
                {
                    btnSubmit.Visible = true;
                    UpdateComplaintLogStatus(PublicValuesforComplaintLogList.complaintLogId, PublicValuesforComplaintLogList.complaintLogNo, PublicValuesforComplaintLogList.statusID, PublicValuesforComplaintLogList.actID);
                }
                else if (statusID == 6)
                {
                    SuccessMessage("Already closed.");
                }
            }
        }

        GetComplaintLogList();
    }

    protected void btnViewAttachment1_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(PublicValuesforComplaintLogList.complaintLogId, "ATTACHMENT1");
        ModalPopupExtender1.Show();
    }

    protected void btnViewAttachment2_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(PublicValuesforComplaintLogList.complaintLogId, "ATTACHMENT2");
        ModalPopupExtender1.Show();
    }


    protected void btnViewAttachment3_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(PublicValuesforComplaintLogList.complaintLogId, "ATTACHMENT3");
        ModalPopupExtender1.Show();
    }

    protected void btnViewAttachment4_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(PublicValuesforComplaintLogList.complaintLogId, "ATTACHMENT4");
        ModalPopupExtender1.Show();
    }


    protected void ddlResponsibleDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        ddlResponsiblePerson.SelectedIndex = 0;
        ddlResponsiblePerson.Enabled = false;
        btnSubmit.Text = "Forward Responsible Department";
        if (ddlResponsibleDepartment.SelectedValue == "8" || ddlResponsibleDepartment.SelectedValue == "9" || ddlResponsibleDepartment.SelectedValue == "18")
        {
            BindResponsiblePerson();
            ddlResponsiblePerson.Enabled = true;

            lblTargetDate.Visible = true;
            hdTargetCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTargetCompletionDate.Text = hdTargetCompletionDate.Value.ToString();
            txtTargetCompletionDate.Visible = true;
            imgbtnTargetCompletionDate.Visible = true;

            btnSubmit.Text = "Forward to Department and Pserson";
        }
        else
        {
            lblTargetDate.Visible = false;
            hdTargetCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTargetCompletionDate.Text = hdTargetCompletionDate.Value.ToString();
            txtTargetCompletionDate.Visible = false;
            imgbtnTargetCompletionDate.Visible = false;
        }
    }

    protected void ddlSanctionNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        fileUploadVisitReport1.Enabled = false;
        fileUploadVisitReport2.Enabled = false;
        fileUploadVisitReport3.Enabled = false;
        if (ddlSanctionNo.SelectedIndex > 0)
        {
            fileUploadVisitReport1.Enabled = true;
            fileUploadVisitReport2.Enabled = true;
            fileUploadVisitReport3.Enabled = true;
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindStatus()
    {
        try
        {
            dsStatus = objComplaintLog.GetComplaingLogStatus();
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "COMPLAINT_LOG_STATUS_NAME";
                ddlStatus.DataValueField = "COMPLAINT_LOG_STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "Select");
                ddlStatus.SelectedIndex = 0;
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
            dsEmployee = objComplaintLog.GetEmployeeListForComplaintLog();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
                //ddlEmployee.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetSanctionNo()
    {
        try
        {
            dsSanctionNo = objComplaintLog.GetSanctionNo(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsSanctionNo.Tables.Count > 0 && dsSanctionNo.Tables[0].Rows.Count > 0)
            {
                ddlSanctionNo.DataSource = dsSanctionNo.Tables[0];
                ddlSanctionNo.DataTextField = "TOUR_SANCTION_NO";
                ddlSanctionNo.DataValueField = "STATEMENT_ID";
                ddlSanctionNo.DataBind();
                ddlSanctionNo.Items.Insert(0, "Select");
                ddlSanctionNo.SelectedIndex = 0;
            }
            else
            {
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

    private void GetComplaintLogList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDate.Value)))
                fromDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDate.Value)))
                toDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text.Trim();
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtComplaintLogNo.Text))
                complaintLogNo = txtComplaintLogNo.Text.Trim();
            else
                complaintLogNo = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusID = 0;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;



            dsComplaintLogList = objComplaintLog.GetComplaintLogListNew(fromDate, toDate, complaintLogNo, customerName, statusID, empRecordID);
            if (dsComplaintLogList.Tables.Count > 0 && dsComplaintLogList.Tables[0].Rows.Count > 0)
            {
                Session["dsComplaintLogList"] = dsComplaintLogList;
                gvComplaintLogList.DataSource = dsComplaintLogList.Tables[0];
                gvComplaintLogList.DataBind();
                lblRecords.Text = "Records[" + dsComplaintLogList.Tables[0].Rows.Count + "]";
            }
            else
            {
                Session["dsComplaintLogList"] = null;
                gvComplaintLogList.DataSource = null;
                gvComplaintLogList.DataBind();
                lblRecords.Text = "Records[0]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void ViewAttachedFilesNew01(int complaintLogID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            DataSet dsComplaintLog = (DataSet)Session["dsComplaintLogList"];
            string fileName = Convert.ToString(dsComplaintLog.Tables[0].Select("COMPLAINT_LOG_ID='" + complaintLogID + "'"));
            string extn = string.Empty;
            foreach (DataRow dr in dsComplaintLog.Tables[0].Select("COMPLAINT_LOG_ID='" + complaintLogID + "'"))
            {
                if (fileType == "ATTACHMENT1")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT_ONE"]);
                    bytes = (byte[])dr["ATTACHMENT_ONE_DOC"];
                }
                else if (fileType == "ATTACHMENT2")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT_TWO"]);
                    bytes = (byte[])dr["ATTACHMENT_TWO_DOC"];
                }
                else if (fileType == "ATTACHMENT3")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT_THREE"]);
                    bytes = (byte[])dr["ATTACHMENT_THREE_DOC"];
                }
                else if (fileType == "ATTACHMENT4")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT_FOUR"]);
                    bytes = (byte[])dr["ATTACHMENT_FOUR_DOC"];
                }
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewComplaintLogImageFile.ashx?complaintLogID=" + complaintLogID + "&fileType=" + fileType;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewComplaintLogPDFFile.aspx?complaintLogID=" + complaintLogID + "&fileType=" + fileType);
                    ModalPopupExtender3.Show();
                }

                else if (extn == "msg" || extn == "MSG")
                {
                    if (bytes != null)
                    {
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.AddHeader("content-disposition", "attachment:filename" + fileName);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();

                        //int value = SaveEmailFile(fileName, bytes);
                        //if (value > 0)
                        //{
                        //    SuccessMessage("Email saved successfully on desktop");
                        //    return;
                        //}                        
                    }
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

    private int SaveEmailFile(string fileName, byte[] bytes)
    {
        try
        {
            //File.WriteAllBytes("C://Users/Upadhyay-trinetran/Desktop" + fileName, bytes);


            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //if (!string.IsNullOrEmpty(pathUser))
            //{
            //    ExceptionMessage("1");
            //}
            //else
            //{
            //    ExceptionMessage("0");
            //}


            File.WriteAllBytes(pathUser + "\\" + fileName, bytes);
            string[] pathusertxt = pathUser.Split('\\');

            //ExceptionMessage(pathusertxt[0] + "-" + pathusertxt[1] + "-" + pathusertxt[2] + "-" + pathusertxt[3]);
            ExceptionMessage(pathusertxt[1]);
            return 1;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
        }
    }

    private int SaveEmailFileNew(string fileName, byte[] bytes)
    {
        try
        {
            //File.WriteAllBytes("C://Users/Upadhyay-trinetran/Desktop" + fileName, bytes);


            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //if (!string.IsNullOrEmpty(pathUser))
            //{
            //    ExceptionMessage("1");
            //}
            //else
            //{
            //    ExceptionMessage("0");
            //}


            File.WriteAllBytes(pathUser + "\\" + fileName, bytes);
            string[] pathusertxt = pathUser.Split('\\');

            //ExceptionMessage(pathusertxt[0] + "-" + pathusertxt[1] + "-" + pathusertxt[2] + "-" + pathusertxt[3]);
            ExceptionMessage(pathusertxt[1]);
            return 1;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
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





    private void BindComplaintLogDetails(int complaintLogID)
    {
        int statusId = 0;
        dsComplaintLog = objComplaintLog.GetComplaintLogInfoNew(complaintLogID);
        if (dsComplaintLog.Tables.Count > 0 && dsComplaintLog.Tables[0].Rows.Count > 0)
        {
            hdStatusID.Value = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["STATUS_ID"]);
            lblLegend.Text = "Complaint No. -[" + Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_LOG_NO"]) + "]";
            statusId = Convert.ToInt32(dsComplaintLog.Tables[0].Rows[0]["STATUS_ID"]);
            txtComplanitRcvdDate.Text = Convert.ToDateTime(dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_RECEIVED_ON"]).ToString("dd-MMM-yyyy");
            txtLocation.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["LOCATION"]);
            txtCustomerNameNew.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["CUSTOMER_NAME"]);
            txtCustomerCode.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["CUSTOMER_CODE"]);
            txtAddress.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ADDRESS"]);
            txtPlant.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["PLANT"]);
            txtJOBNo.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["JOB_NO"]);
            txtPONumber.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["PO_NO"]);
            txtItemName.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ITEM_NAME"]);
            txtModelNo.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["MODEL_NO"]);
            txtTypeOfService.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["SERVICE_TYPE"]);
            txtComplaintDescription.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_DESCRIPTION"]);

            if (dsComplaintLog.Tables[0].Rows[0]["RESP_PERSON_LESSON_LEARNT"] != DBNull.Value)
                txtRespPersonLessonLearnt.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["RESP_PERSON_LESSON_LEARNT"]);

            if (dsComplaintLog.Tables[0].Rows[0]["RESP_DEPT_HOD_LESSON_LEARNT"] != DBNull.Value)
                txtRespDeptHODLessonLearnt.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["RESP_DEPT_HOD_LESSON_LEARNT"]);

            if (dsComplaintLog.Tables[0].Rows[0]["SERVICE_DEPT_HOD_LESSON_LEARNT"] != DBNull.Value)
                txtServiceDeptHODLessonLearnt.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["SERVICE_DEPT_HOD_LESSON_LEARNT"]);


            if (dsComplaintLog.Tables[0].Rows[0]["RESPONSIBLE_DEPT_ID"] != DBNull.Value && Convert.ToInt32(dsComplaintLog.Tables[0].Rows[0]["RESPONSIBLE_DEPT_ID"]) > 0)
                ddlResponsibleDepartment.SelectedValue = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["RESPONSIBLE_DEPT_ID"]);

            BindResponsiblePerson();
            if (dsComplaintLog.Tables[0].Rows[0]["RESPONSIBLE_PERSON_ID"] != DBNull.Value && Convert.ToInt32(dsComplaintLog.Tables[0].Rows[0]["RESPONSIBLE_PERSON_ID"]) > 0)
                ddlResponsiblePerson.SelectedValue = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["RESPONSIBLE_PERSON_ID"]);
            else
                ddlResponsiblePerson.SelectedIndex = 0;

            if (dsComplaintLog.Tables[0].Rows[0]["BUSINESS_UNIT_ID"] != DBNull.Value && Convert.ToInt32(dsComplaintLog.Tables[0].Rows[0]["BUSINESS_UNIT_ID"]) > 0)
                ddlBusinessUnit.SelectedValue = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["BUSINESS_UNIT_ID"]);

            if (dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_DESCRIPTION"] != DBNull.Value)
                txtComplaintDescription.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_DESCRIPTION"]);

            if (dsComplaintLog.Tables[0].Rows[0]["MANUFACTURER_NAME"] != DBNull.Value)
                txtEquipmentManufacturerName.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["MANUFACTURER_NAME"]);

            if (dsComplaintLog.Tables[0].Rows[0]["PROPOSED_ACTIONS"] != DBNull.Value)
                txtProposedActions.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["PROPOSED_ACTIONS"]);

            if (dsComplaintLog.Tables[0].Rows[0]["ROOT_CAUSE"] != DBNull.Value)
                txtRootCause.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ROOT_CAUSE"]);

            if (dsComplaintLog.Tables[0].Rows[0]["TARGET_COMPLETION_DATE"] != DBNull.Value)
            {
                hdTargetCompletionDate.Value = Convert.ToDateTime(dsComplaintLog.Tables[0].Rows[0]["TARGET_COMPLETION_DATE"]).ToString("dd-MMM-yyyy");
                txtTargetCompletionDate.Text = Convert.ToString(hdTargetCompletionDate.Value);
            }

            if (dsComplaintLog.Tables[0].Rows[0]["ACTUAL_COMPLETION_DATE"] != DBNull.Value)
            {
                hdActualCompletionDate.Value = Convert.ToDateTime(dsComplaintLog.Tables[0].Rows[0]["ACTUAL_COMPLETION_DATE"]).ToString("dd-MMM-yyyy");
                txtActualCompletionDate.Text = Convert.ToString(hdActualCompletionDate.Value);
            }

            if (dsComplaintLog.Tables[0].Rows[0]["CORRECTIVE_ACTION"] != DBNull.Value)
                txtCorrectiveAction.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["CORRECTIVE_ACTION"]);



            string Attachment1Txt = string.Empty;
            string Attachment2Txt = string.Empty;
            string Attachment1Extn = string.Empty;
            string Attachment2Extn = string.Empty;

            string Attachment3Txt = string.Empty;
            string Attachment4Txt = string.Empty;
            string Attachment3Extn = string.Empty;
            string Attachment4Extn = string.Empty;

            if (dsComplaintLog.Tables[0].Rows[0]["ATTACHMENT_ONE"] != DBNull.Value)
                Attachment1Txt = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ATTACHMENT_ONE"]);

            if (dsComplaintLog.Tables[0].Rows[0]["ATTACHMENT_TWO"] != DBNull.Value)
                Attachment2Txt = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ATTACHMENT_TWO"]);

            if (dsComplaintLog.Tables[0].Rows[0]["ATTACHMENT_THREE"] != DBNull.Value)
                Attachment3Txt = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ATTACHMENT_THREE"]);

            if (dsComplaintLog.Tables[0].Rows[0]["ATTACHMENT_FOUR"] != DBNull.Value)
                Attachment4Txt = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ATTACHMENT_FOUR"]);

            if (!string.IsNullOrEmpty(Attachment1Txt))
            {
                btnViewAttachment1.Visible = true;
                txtViewAttachment1.Text = Attachment1Txt;
                Attachment1Extn = Convert.ToString(Attachment1Txt).Split('.').Last();
                if (Attachment1Extn == "jpg" || Attachment1Extn == "jepg" || Attachment1Extn == "bmp" || Attachment1Extn == "png" || Attachment1Extn == "gif" || Attachment1Extn == "JPG" || Attachment1Extn == "JPEG" || Attachment1Extn == "BMP" || Attachment1Extn == "PNG" || Attachment1Extn == "GIF")
                {
                    btnViewAttachment1.ImageUrl = "~/Images/imgicon1.png";
                    btnViewAttachment1.ToolTip = Attachment1Txt;
                }
                else if (Attachment1Extn == "pdf" || Attachment1Extn == "PDF")
                {
                    btnViewAttachment1.ImageUrl = "~/Images/pdficon1.png";
                    btnViewAttachment1.ToolTip = Attachment1Txt;
                }
            }
            else
            {
                txtViewAttachment1.Text = string.Empty;
                btnViewAttachment1.Visible = false;
            }

            if (!string.IsNullOrEmpty(Attachment2Txt))
            {
                btnViewAttachment2.Visible = true;
                txtViewAttachment2.Text = Attachment2Txt;
                Attachment2Extn = Convert.ToString(Attachment2Txt).Split('.').Last();
                if (Attachment2Extn == "jpg" || Attachment2Extn == "jepg" || Attachment2Extn == "bmp" || Attachment2Extn == "png" || Attachment2Extn == "gif" || Attachment2Extn == "JPG" || Attachment2Extn == "JPEG" || Attachment2Extn == "BMP" || Attachment2Extn == "PNG" || Attachment2Extn == "GIF")
                {
                    btnViewAttachment2.ImageUrl = "~/Images/imgicon1.png";
                    btnViewAttachment2.ToolTip = Attachment2Txt;
                }
                else if (Attachment2Extn == "pdf" || Attachment2Extn == "PDF")
                {
                    btnViewAttachment2.ImageUrl = "~/Images/pdficon1.png";
                    btnViewAttachment2.ToolTip = Attachment2Txt;
                }
            }
            else
            {
                txtViewAttachment2.Text = string.Empty;
                btnViewAttachment2.Visible = false;
            }

            if (!string.IsNullOrEmpty(Attachment3Txt))
            {
                btnViewAttachment3.Visible = true;
                txtViewAttachment3.Text = Attachment3Txt;
                Attachment3Extn = Convert.ToString(Attachment3Txt).Split('.').Last();
                if (Attachment3Extn == "jpg" || Attachment3Extn == "jepg" || Attachment3Extn == "bmp" || Attachment3Extn == "png" || Attachment3Extn == "gif" || Attachment3Extn == "JPG" || Attachment3Extn == "JPEG" || Attachment3Extn == "BMP" || Attachment3Extn == "PNG" || Attachment3Extn == "GIF")
                {
                    btnViewAttachment3.ImageUrl = "~/Images/imgicon1.png";
                    btnViewAttachment3.ToolTip = Attachment3Txt;
                }
                else if (Attachment3Extn == "pdf" || Attachment3Extn == "PDF")
                {
                    btnViewAttachment3.ImageUrl = "~/Images/pdficon1.png";
                    btnViewAttachment3.ToolTip = Attachment3Txt;
                }
            }
            else
            {
                txtViewAttachment3.Text = string.Empty;
                btnViewAttachment3.Visible = false;
            }

            if (!string.IsNullOrEmpty(Attachment4Txt))
            {
                btnViewAttachment4.Visible = true;
                txtViewAttachment4.Text = Attachment4Txt;
                Attachment4Extn = Convert.ToString(Attachment4Txt).Split('.').Last();
                if (Attachment4Extn == "jpg" || Attachment4Extn == "jepg" || Attachment4Extn == "bmp" || Attachment4Extn == "png" || Attachment4Extn == "gif" || Attachment4Extn == "JPG" || Attachment4Extn == "JPEG" || Attachment4Extn == "BMP" || Attachment4Extn == "PNG" || Attachment4Extn == "GIF")
                {
                    btnViewAttachment4.ImageUrl = "~/Images/imgicon1.png";
                    btnViewAttachment4.ToolTip = Attachment4Txt;
                }
                else if (Attachment4Extn == "pdf" || Attachment4Extn == "PDF")
                {
                    btnViewAttachment4.ImageUrl = "~/Images/pdficon1.png";
                    btnViewAttachment4.ToolTip = Attachment4Txt;
                }
            }
            else
            {
                txtViewAttachment4.Text = string.Empty;
                btnViewAttachment4.Visible = false;
            }
        }
    }

    private void BindDepartment()
    {
        try
        {
            dsResponsibleDept = objComplaintLog.GetDepartmentForComplaintLog();
            if (dsResponsibleDept.Tables.Count > 0 && dsResponsibleDept.Tables[0].Rows.Count > 0)
            {
                ddlResponsibleDepartment.DataSource = dsResponsibleDept.Tables[0];
                ddlResponsibleDepartment.DataTextField = "DEPARTMENT_NAME";
                ddlResponsibleDepartment.DataValueField = "DEPARTMENT_ID";
                ddlResponsibleDepartment.DataBind();
                ddlResponsibleDepartment.Items.Insert(0, "Select");
                ddlResponsibleDepartment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetBusinessUnit()
    {
        try
        {
            dsBusinessUnit = objComplaintLog.GetBusinessUnit();
            if (dsBusinessUnit.Tables.Count > 0 && dsBusinessUnit.Tables[0].Rows.Count > 0)
            {
                ddlBusinessUnit.DataSource = dsBusinessUnit.Tables[0];
                ddlBusinessUnit.DataTextField = "BUSINESS_UNIT";
                ddlBusinessUnit.DataValueField = "BUSINESS_UNIT_ID";
                ddlBusinessUnit.DataBind();
                ddlBusinessUnit.Items.Insert(0, "Select");
                ddlBusinessUnit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindResponsiblePerson()
    {
        try
        {
            if (ddlResponsibleDepartment.SelectedIndex > 0)
                dsResponsiblePerson = objCommon.GetEmployeeByDepartmentID(Convert.ToInt32(ddlResponsibleDepartment.SelectedValue));
            else
                dsResponsiblePerson = objCommon.GetEmployeeByDepartmentID(0);

            if (dsResponsiblePerson.Tables.Count > 0 && dsResponsiblePerson.Tables[0].Rows.Count > 0)
            {
                ddlResponsiblePerson.DataSource = dsResponsiblePerson.Tables[0];
                ddlResponsiblePerson.DataTextField = "EMPLOYEE_NAME";
                ddlResponsiblePerson.DataValueField = "EMP_RECORD_ID";
                ddlResponsiblePerson.DataBind();
                ddlResponsiblePerson.Items.Insert(0, "Select");
                ddlResponsiblePerson.SelectedIndex = 0;
                //if (responsiblePersonId > 0)
                //{
                //    ddlResponsiblePerson.SelectedValue = Convert.ToString(responsiblePersonId);
                //}
                //else
                //{
                //    ddlResponsiblePerson.SelectedIndex = 0;
                //}
            }
            else
            {
                ddlResponsiblePerson.Items.Clear();
                ddlResponsiblePerson.Items.Insert(0, "Select");
                ddlResponsiblePerson.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateComplaintLogStatus(int complaintLogId, string complaintLogNo, int statusID, int actID)
    {
        try
        {
            if (ddlResponsibleDepartment.SelectedIndex > 0)
                responsibleDepartment = Convert.ToInt32(ddlResponsibleDepartment.SelectedValue);
            else
                responsibleDepartment = 0;

            if (ddlResponsiblePerson.SelectedIndex > 0)
                responsiblePserson = Convert.ToInt32(ddlResponsiblePerson.SelectedValue);
            else
                responsiblePserson = 0;



            if (ddlBusinessUnit.SelectedIndex > 0)
                businessUnitID = Convert.ToInt32(ddlBusinessUnit.SelectedValue);
            else
                businessUnitID = 0;

            if (!string.IsNullOrEmpty(txtComplaintDescription.Text))
                complaintDescription = txtComplaintDescription.Text;
            else
                complaintDescription = string.Empty;

            if (!string.IsNullOrEmpty(txtEquipmentManufacturerName.Text))
                manufacturerName = txtEquipmentManufacturerName.Text;
            else
                manufacturerName = string.Empty;

            if (!string.IsNullOrEmpty(txtProposedActions.Text))
                proposedActions = txtProposedActions.Text;
            else
                proposedActions = string.Empty;


            if (!string.IsNullOrEmpty(txtRootCause.Text))
                rootCause = txtRootCause.Text;
            else
                rootCause = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdTargetCompletionDate.Value)))
                targetCompletionDate = Convert.ToDateTime(hdTargetCompletionDate.Value).ToString("yyyy-MM-dd");
            else
                targetCompletionDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdActualCompletionDate.Value)))
                actualCompletionDate = Convert.ToDateTime(hdActualCompletionDate.Value).ToString("yyyy-MM-dd");
            else
                actualCompletionDate = string.Empty;

            if (!string.IsNullOrEmpty(txtRespPersonLessonLearnt.Text))
                respPersonLessonLearnt = txtRespPersonLessonLearnt.Text;
            else
                respPersonLessonLearnt = string.Empty;


            if (!string.IsNullOrEmpty(txtRespPersonLessonLearnt.Text))
                respPersonLessonLearnt = txtRespPersonLessonLearnt.Text;
            else
                respPersonLessonLearnt = string.Empty;


            if (!string.IsNullOrEmpty(txtRespDeptHODLessonLearnt.Text))
                respDeptHODLessonLearnt = txtRespDeptHODLessonLearnt.Text;
            else
                respDeptHODLessonLearnt = string.Empty;


            if (!string.IsNullOrEmpty(txtCorrectiveAction.Text))
                correctiveAction = txtCorrectiveAction.Text;
            else
                correctiveAction = string.Empty;


            if (ddlSanctionNo.SelectedIndex > 0)
                statementID = Convert.ToInt32(ddlSanctionNo.SelectedValue);
            else
                statementID = 0;


            Byte[] fileUploadVisitReportOneBytes = null;
            if (fileUploadVisitReport1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadVisitReport1.PostedFile.FileName))
                {
                    fileUploadVisitReportOneFileName = fileUploadVisitReport1.PostedFile.FileName;
                    fileUploadVisitReportOneBytes = GetFileBytes(fileUploadVisitReport1.PostedFile.FileName, fileUploadVisitReport1.PostedFile.InputStream);
                }
                else
                {
                    fileUploadVisitReportOneFileName = string.Empty;
                    fileUploadVisitReportOneBytes = null;
                }
            }
            else
            {
                fileUploadVisitReportOneFileName = string.Empty;
                fileUploadVisitReportOneBytes = null;
            }

            Byte[] fileUploadVisitReportTwoBytes = null;
            if (fileUploadVisitReport2.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadVisitReport2.PostedFile.FileName))
                {
                    fileUploadVisitReportTwoFileName = fileUploadVisitReport2.PostedFile.FileName;
                    fileUploadVisitReportTwoBytes = GetFileBytes(fileUploadVisitReport2.PostedFile.FileName, fileUploadVisitReport2.PostedFile.InputStream);
                }
                else
                {
                    fileUploadVisitReportTwoFileName = string.Empty;
                    fileUploadVisitReportTwoBytes = null;
                }
            }
            else
            {
                fileUploadVisitReportTwoFileName = string.Empty;
                fileUploadVisitReportTwoBytes = null;
            }

            Byte[] fileUploadVisitReportThreeBytes = null;
            if (fileUploadVisitReport3.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadVisitReport3.PostedFile.FileName))
                {
                    fileUploadVisitReportThreeFileName = fileUploadVisitReport3.PostedFile.FileName;
                    fileUploadVisitReportThreeBytes = GetFileBytes(fileUploadVisitReport3.PostedFile.FileName, fileUploadVisitReport3.PostedFile.InputStream);
                }
                else
                {
                    fileUploadVisitReportThreeFileName = string.Empty;
                    fileUploadVisitReportThreeBytes = null;
                }
            }
            else
            {
                fileUploadVisitReportThreeFileName = string.Empty;
                fileUploadVisitReportThreeBytes = null;
            }


            Byte[] attachmentOneBytes = null;
            if (fileUploadAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment1.PostedFile.FileName))
                {
                    attachmentOneFileName = fileUploadAttachment1.PostedFile.FileName;
                    attachmentOneBytes = GetFileBytes(fileUploadAttachment1.PostedFile.FileName, fileUploadAttachment1.PostedFile.InputStream);
                }
                else
                {
                    attachmentOneFileName = string.Empty;
                    attachmentOneBytes = null;
                }
            }
            else
            {
                attachmentOneFileName = string.Empty;
                attachmentOneBytes = null;
            }

            Byte[] attachmentTwoBytes = null;
            if (fileUploadAttachment2.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment2.PostedFile.FileName))
                {
                    attachmentTwoFileName = fileUploadAttachment2.PostedFile.FileName;
                    attachmentTwoBytes = GetFileBytes(fileUploadAttachment2.PostedFile.FileName, fileUploadAttachment2.PostedFile.InputStream);
                }
                else
                {
                    attachmentTwoFileName = string.Empty;
                    attachmentTwoBytes = null;
                }
            }
            else
            {
                attachmentTwoFileName = string.Empty;
                attachmentTwoBytes = null;
            }


            int value = 0;
            int sendMailValue = 0;

            //Assign-Dept, assign-Person
            if (actID == 2 || actID == 3)
            {
                value = objComplaintLog.AssignComplaintLogNew(complaintLogId, actID, responsibleDepartment, responsiblePserson, targetCompletionDate, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    sendMailValue = SendMail(complaintLogId);

                    if (actID == 2)
                    {
                        if (sendMailValue > 0)
                        {
                            objComplaintLog.UpdateComplaintLogMailStatus(complaintLogId, actID);
                            if (responsibleDepartment == 8 || responsibleDepartment == 9 || responsibleDepartment == 18)
                            {
                                objComplaintLog.UpdateComplaintLogMailStatus(complaintLogId, 3);
                                SuccessMessage("Complaint Log No.: " + complaintLogNo + " assigned to responsible person successfully and mail sent.");
                            }
                            else
                                SuccessMessage("Complaint Log No.: " + complaintLogNo + " assigned to responsible department successfully and mail sent.");
                        }
                        else
                        {
                            if (responsibleDepartment == 8 || responsibleDepartment == 9 || responsibleDepartment == 18)
                                SuccessMessage("Complaint Log No.: " + complaintLogNo + " assigned to responsible person successfully, please send assignment mail from Complaint Log List.");
                            else
                                SuccessMessage("Complaint Log No.: " + complaintLogNo + " assigned to responsible department successfully, please send assignment mail from Complaint Log List.");
                        }
                    }
                    else
                    {
                        if (sendMailValue > 0)
                        {
                            objComplaintLog.UpdateComplaintLogMailStatus(complaintLogId, actID);
                            SuccessMessage("Complaint Log No.: " + complaintLogNo + " assigned to responsible person successfully and mail sent.");
                        }
                        else
                            SuccessMessage("Complaint Log No.: " + complaintLogNo + " assigned to responsible person successfully, please send assignment mail from Complaint Log List.");
                    }

                    Reset();
                }
                else
                {
                    ExceptionMessage("Please try again!");
                    return;
                }
            }

            //Resolve
            else if (actID == 4)
            {
                value = objComplaintLog.ResolveComplaintLog(complaintLogId, businessUnitID, complaintDescription, manufacturerName,
                                                                   proposedActions, rootCause,
                                                                   actualCompletionDate, respPersonLessonLearnt, correctiveAction,
                                                                   statementID,
                                                                   fileUploadVisitReportOneFileName, fileUploadVisitReportOneBytes,
                                                                   fileUploadVisitReportTwoFileName, fileUploadVisitReportTwoBytes,
                                                                   fileUploadVisitReportThreeFileName, fileUploadVisitReportThreeBytes,
                                                                   attachmentOneFileName, attachmentOneBytes,
                                                                   attachmentTwoFileName, attachmentTwoBytes,
                                                                   Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    SuccessMessage(".");
                    sendMailValue = SendMail(complaintLogId);
                    if (sendMailValue > 0)
                    {
                        objComplaintLog.UpdateComplaintLogMailStatus(complaintLogId, actID);
                        SuccessMessage("Complaint Log No.: " + complaintLogNo + " resolved successfully and mail sent.");
                    }
                    else
                        SuccessMessage("Complaint Log No.: " + complaintLogNo + " resolved successfully, please send assignment mail from Complaint Log List.");


                    Reset();
                }
                else
                {
                    ExceptionMessage("Please try again!");
                    return;
                }
            }

            //Approve, Close
            if (actID == 5 || actID == 6)
            {
                value = objComplaintLog.ApproveAndCloseComplaintLog(complaintLogId, actID, respDeptHODLessonLearnt, serviceDeptHODLessonLearnt, Convert.ToInt32(Session["EMP_RECORD_ID"]), responsibleDepartment);

                if (value > 0)
                {
                    sendMailValue = SendMail(complaintLogId);

                    if (actID == 5)
                    {
                        if (sendMailValue > 0)
                        {
                            objComplaintLog.UpdateComplaintLogMailStatus(complaintLogId, actID);

                            if (responsibleDepartment == 8 || responsibleDepartment == 9 || responsibleDepartment == 18)
                            {
                                objComplaintLog.UpdateComplaintLogMailStatus(complaintLogId, 6);
                                SuccessMessage("Complaint Log No.: " + complaintLogNo + " closed successfully and mail sent.");
                            }
                            else
                                SuccessMessage("Complaint Log No.: " + complaintLogNo + " approved successfully and mail sent.");
                        }
                        else
                        {
                            if (responsibleDepartment == 8 || responsibleDepartment == 9 || responsibleDepartment == 18)
                                SuccessMessage("Complaint Log No.: " + complaintLogNo + " closed successfully, please send assignment mail from Complaint Log List.");
                            else
                                SuccessMessage("Complaint Log No.: " + complaintLogNo + " approved successfully, please send assignment mail from Complaint Log List.");
                        }
                    }
                    else
                    {
                        if (sendMailValue > 0)
                        {
                            objComplaintLog.UpdateComplaintLogMailStatus(complaintLogId, actID);
                            SuccessMessage("Complaint Log No.: " + complaintLogNo + " closed successfully and mail sent.");
                        }
                        else
                            SuccessMessage("Complaint Log No.: " + complaintLogNo + " closed successfully, please send assignment mail from Complaint Log List.");
                    }

                    Reset();
                }
                else
                {
                    ExceptionMessage("Please try again!");
                    return;
                }
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

    private int SendMail(int complaintID)
    {
        try
        {
            string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);

            int statusId = 0;
            string complaintlogrcvddate = string.Empty;
            string location = string.Empty;
            string customerName = string.Empty;
            string address = string.Empty;
            string plant = string.Empty;
            string jobNo = string.Empty;
            string poNo = string.Empty;
            string itemName = string.Empty;
            string modelNo = string.Empty;
            string serviceType = string.Empty;

            string respPersonLessonLearnt = string.Empty;
            string respHodLessonLearnt = string.Empty;
            string serviceHodLessonLearnt = string.Empty;

            string businessUnit = string.Empty;
            string responsibleDept = string.Empty;
            int responsibleDeptID = 0;



            string subject = string.Empty;
            string from = string.Empty;
            string to = string.Empty;
            string cc = string.Empty;
            string fileName = string.Empty;

            //User          
            string createdBy = string.Empty;
            string createdOn = string.Empty;
            string createdByEmailId = string.Empty;


            //SErvice-Head
            int serviceHODEmpRecordID = 0;
            string serviceHODName = string.Empty;
            string serviceHODEmailId = string.Empty;
            string serviceHODUserName = string.Empty;
            string serviceHODPassword = string.Empty;


            //Respnsible Dept-HOD
            int deptHodEmpRecordID = 0;
            string deptHodName = string.Empty;

            string deptHodEmailId = string.Empty;
            string deptHodUserName = string.Empty;
            string deptHodPassword = string.Empty;

            //Person
            int responsiblePersonEmpRecordID = 0;
            string responsiblePersonName = string.Empty;

            string responsiblePersonEmailId = string.Empty;
            string responsiblePersonUserName = string.Empty;
            string responsiblePersonPassword = string.Empty;

            //Cosed
            string closedBy = string.Empty;
            string closedOn = string.Empty;
            string closedByEmailId = string.Empty;

            string assignedDeptBy = string.Empty;
            string assignedDeptOn = string.Empty;
            string assignedDeptByEmailID = string.Empty;

            string assignedPersonBy = string.Empty;
            string assignedPersonOn = string.Empty;
            string assignedPersonByEmailID = string.Empty;

            string resolvedBy = string.Empty;
            string resolvedOn = string.Empty;
            string resolvedByEmailId = string.Empty;

            string approvedBy = string.Empty;
            string approvedOn = string.Empty;
            string approvedByEmailId = string.Empty;

            string href = string.Empty;
            string link = string.Empty;

            dsComplaintLogDetails = objComplaintLog.GetComplaintLogInfoNew(complaintID);
            if (dsComplaintLogDetails.Tables.Count > 0)
            {
                if (dsComplaintLogDetails.Tables[0].Rows.Count > 0)
                {
                    complaintLogID = Convert.ToInt32(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_LOG_ID"]);
                    complaintLogNo = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_LOG_NO"]);
                    statusId = Convert.ToInt32(dsComplaintLogDetails.Tables[0].Rows[0]["STATUS_ID"]);


                    if (dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_RECEIVED_ON"] != DBNull.Value)
                        complaintlogrcvddate = Convert.ToDateTime(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_RECEIVED_ON"]).ToString("dd-MMM-yyyy");

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["LOCATION"] != DBNull.Value)
                        location = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["LOCATION"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["CUSTOMER_NAME"] != DBNull.Value)
                        customerName = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["CUSTOMER_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["ADDRESS"] != DBNull.Value)
                        address = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["ADDRESS"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["PLANT"] != DBNull.Value)
                        plant = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["PLANT"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value)
                        jobNo = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["JOB_NO"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["PO_NO"] != DBNull.Value)
                        poNo = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["PO_NO"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["ITEM_NAME"] != DBNull.Value)
                        itemName = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["ITEM_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["MODEL_NO"] != DBNull.Value)
                        modelNo = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["MODEL_NO"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["SERVICE_TYPE"] != DBNull.Value)
                        serviceType = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["SERVICE_TYPE"]);


                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESP_PERSON_LESSON_LEARNT"] != DBNull.Value)
                        respPersonLessonLearnt = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["RESP_PERSON_LESSON_LEARNT"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESP_DEPT_HOD_LESSON_LEARNT"] != DBNull.Value)
                        respHodLessonLearnt = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["RESP_DEPT_HOD_LESSON_LEARNT"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["SERVICE_DEPT_HOD_LESSON_LEARNT"] != DBNull.Value)
                        serviceHodLessonLearnt = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["SERVICE_DEPT_HOD_LESSON_LEARNT"]);


                    if (dsComplaintLogDetails.Tables[0].Rows[0]["BUSINESS_UNIT"] != DBNull.Value)
                        businessUnit = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["BUSINESS_UNIT"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["DEPARTMENT_NAME"] != DBNull.Value)
                        responsibleDept = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["DEPARTMENT_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_DEPT_ID"] != DBNull.Value)
                        responsibleDeptID = Convert.ToInt32(dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_DEPT_ID"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_EMP_RECORD_ID"] != DBNull.Value)
                        responsiblePersonEmpRecordID = Convert.ToInt32(dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_EMP_RECORD_ID"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_NAME"] != DBNull.Value)
                        responsiblePersonName = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_EMAIL_ID"] != DBNull.Value)
                        responsiblePersonEmailId = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_EMAIL_ID"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_USER_NAME"] != DBNull.Value)
                        responsiblePersonUserName = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_USER_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_PASSWORD"] != DBNull.Value)
                        responsiblePersonPassword = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["RESPONSIBLE_PERSON_PASSWORD"]);


                    if (dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_BY_NAME"] != DBNull.Value)
                        createdBy = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_BY_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_BY_EMAIL_ID"] != DBNull.Value)
                        createdByEmailId = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_BY_EMAIL_ID"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_ON"] != DBNull.Value)
                        createdOn = Convert.ToDateTime(dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_ON"]).ToString("dd-MMM-yyyy");



                    if (dsComplaintLogDetails.Tables[0].Rows[0]["DEPT_ASSIGNED_BY_NAME"] != DBNull.Value)
                        assignedDeptBy = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["DEPT_ASSIGNED_BY_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["DEPT_ASSIGNED_ON"] != DBNull.Value)
                        assignedDeptOn = Convert.ToDateTime(dsComplaintLogDetails.Tables[0].Rows[0]["DEPT_ASSIGNED_ON"]).ToString("dd-MMM-yyyy");

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["DEPT_ASSIGNED_BY_EMAIL_ID"] != DBNull.Value)
                        assignedDeptByEmailID = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["DEPT_ASSIGNED_BY_EMAIL_ID"]);




                    if (dsComplaintLogDetails.Tables[0].Rows[0]["PERSON_ASSIGNED_BY_NAME"] != DBNull.Value)
                        assignedPersonBy = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["PERSON_ASSIGNED_BY_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["PERSON_ASSIGNED_ON"] != DBNull.Value)
                        assignedPersonOn = Convert.ToDateTime(dsComplaintLogDetails.Tables[0].Rows[0]["PERSON_ASSIGNED_ON"]).ToString("dd-MMM-yyyy");

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["PERSON_ASSIGNED_BY_EMAIL_ID"] != DBNull.Value)
                        assignedPersonByEmailID = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["PERSON_ASSIGNED_BY_EMAIL_ID"]);



                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESOLVED_BY_NAME"] != DBNull.Value)
                        resolvedBy = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["RESOLVED_BY_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESOLVED_ON"] != DBNull.Value)
                        resolvedOn = Convert.ToDateTime(dsComplaintLogDetails.Tables[0].Rows[0]["RESOLVED_ON"]).ToString("dd-MMM-yyyy");

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["RESOLVED_BY_EMAIL_ID"] != DBNull.Value)
                        resolvedByEmailId = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["RESOLVED_BY_EMAIL_ID"]);


                    if (dsComplaintLogDetails.Tables[0].Rows[0]["APPROVED_BY_NAME"] != DBNull.Value)
                        approvedBy = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["APPROVED_BY_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["APPROVED_ON"] != DBNull.Value)
                        approvedOn = Convert.ToDateTime(dsComplaintLogDetails.Tables[0].Rows[0]["APPROVED_ON"]).ToString("dd-MMM-yyyy");

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["APPROVED_BY_EMAIL_ID"] != DBNull.Value)
                        approvedByEmailId = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["APPROVED_BY_EMAIL_ID"]);


                    if (dsComplaintLogDetails.Tables[0].Rows[0]["CLOSED_BY_NAME"] != DBNull.Value)
                        closedBy = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["CLOSED_BY_NAME"]);

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["CLOSED_ON"] != DBNull.Value)
                        closedOn = Convert.ToDateTime(dsComplaintLogDetails.Tables[0].Rows[0]["CLOSED_ON"]).ToString("dd-MMM-yyyy");

                    if (dsComplaintLogDetails.Tables[0].Rows[0]["CLOSED_BY_EMAIL_ID"] != DBNull.Value)
                        closedByEmailId = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["CLOSED_BY_EMAIL_ID"]);



                }
                //SERVICE-HEAD
                if (dsComplaintLogDetails.Tables[1].Rows.Count > 0)
                {
                    if (dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_EMP_RECORD_ID"] != DBNull.Value)
                        serviceHODEmpRecordID = Convert.ToInt32(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_EMP_RECORD_ID"]);

                    if (dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_NAME"] != DBNull.Value)
                        serviceHODName = Convert.ToString(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_NAME"]);

                    if (dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_EMAIL_ID"] != DBNull.Value)
                        serviceHODEmailId = Convert.ToString(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_EMAIL_ID"]);

                    if (dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_USER_NAME"] != DBNull.Value)
                        serviceHODUserName = Convert.ToString(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_USER_NAME"]);

                    if (dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_PASSWORD"] != DBNull.Value)
                        serviceHODPassword = Convert.ToString(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_PASSWORD"]);
                }

                //Responsible-dept-hod
                if (dsComplaintLogDetails.Tables[2].Rows.Count > 0)
                {
                    if (dsComplaintLogDetails.Tables[2].Rows[0]["HOD_EMP_RECORD_ID"] != DBNull.Value)
                        deptHodEmpRecordID = Convert.ToInt32(dsComplaintLogDetails.Tables[2].Rows[0]["HOD_EMP_RECORD_ID"]);

                    if (dsComplaintLogDetails.Tables[2].Rows[0]["HOD_NAME"] != DBNull.Value)
                        deptHodName = Convert.ToString(dsComplaintLogDetails.Tables[2].Rows[0]["HOD_NAME"]);

                    if (dsComplaintLogDetails.Tables[2].Rows[0]["HOD_EMAIL_ID"] != DBNull.Value)
                        deptHodEmailId = Convert.ToString(dsComplaintLogDetails.Tables[2].Rows[0]["HOD_EMAIL_ID"]);

                    if (dsComplaintLogDetails.Tables[2].Rows[0]["HOD_USER_NAME"] != DBNull.Value)
                        deptHodUserName = Convert.ToString(dsComplaintLogDetails.Tables[2].Rows[0]["HOD_USER_NAME"]);

                    if (dsComplaintLogDetails.Tables[2].Rows[0]["HOD_PASSWORD"] != DBNull.Value)
                        deptHodPassword = Convert.ToString(dsComplaintLogDetails.Tables[2].Rows[0]["HOD_PASSWORD"]);
                }
            }

            //Dept-Assigned
            if (statusId == 1)
            {
                subject = "Complaint Log No.: '" + complaintLogNo + "' created On: " + createdOn;
                fileName = "~/COMPLAINT_LOG/EMAIL_FORMATS/02AssignDeptMail.htm";
                link = "'" + urlTxt + "/COMPLAINT_LOG/UpdateComplaintLogStatusNewOne.aspx?complaintlogid=" + complaintLogID + "&complaintlogno=" + complaintLogNo + "&ud=" + serviceHODUserName + "&pd=" + serviceHODPassword + "&actid=2'";
                href = "<a href=" + link + ">Forward To Responsible Department</a>";

                from = createdByEmailId;
                to = serviceHODEmailId;
            }

            //Dept-Assigned
            if (statusId == 2)
            {
                subject = "Complaint Log No.: '" + complaintLogNo + "' forwarded to " + responsibleDept + " On: " + assignedDeptOn;
                fileName = "~/COMPLAINT_LOG/EMAIL_FORMATS/03AssignResponsiblePersonCLMail.htm";
                link = "'" + urlTxt + "/COMPLAINT_LOG/UpdateComplaintLogStatusNewOne.aspx?complaintlogid=" + complaintLogID + "&complaintlogno=" + complaintLogNo + "&ud=" + deptHodUserName + "&pd=" + deptHodPassword + "&actid=3'";
                href = "<a href=" + link + ">Assign To Responsible Person</a>";

                from = serviceHODEmailId;
                to = deptHodEmailId;
            }
            //Person-Assigned
            else if (statusId == 3)
            {
                subject = "Complaint Log No.: '" + complaintLogNo + "' assigned to responsible person On: " + assignedPersonOn;
                fileName = "~/COMPLAINT_LOG/EMAIL_FORMATS/04ResolveMail.htm";
                link = "'" + urlTxt + "/COMPLAINT_LOG/UpdateComplaintLogStatusNewOne.aspx?complaintlogid=" + complaintLogID + "&complaintlogno=" + complaintLogNo + "&ud=" + responsiblePersonUserName + "&pd=" + responsiblePersonPassword + "&actid=4'";
                href = "<a href=" + link + ">Submit Complaint Request Solution</a>";

                from = deptHodEmailId;
                to = responsiblePersonEmailId;
            }

            //Resolved
            else if (statusId == 4)
            {
                subject = "Complaint Log No.: '" + complaintLogNo + "' resolved On: " + resolvedOn;
                link = "'" + urlTxt + "/COMPLAINT_LOG/UpdateComplaintLogStatusNewOne.aspx?complaintlogid=" + complaintLogID + "&complaintlogno=" + complaintLogNo + "&ud=" + deptHodUserName + "&pd=" + deptHodPassword + "&actid=5'";

                if (responsibleDeptID == 8 || responsibleDeptID == 9 || responsibleDeptID == 18)
                {
                    fileName = "~/COMPLAINT_LOG/EMAIL_FORMATS/05ApprovalAndClosingCLMail.htm";
                    href = "<a href=" + link + ">Approve and Close Complaint Request</a>";
                }
                else
                {
                    fileName = "~/COMPLAINT_LOG/EMAIL_FORMATS/05ApprovalCLMail.htm";
                    href = "<a href=" + link + ">Approve Complaint Request</a>";
                }

                from = responsiblePersonEmailId;
                to = deptHodEmailId;
            }

            //Approved
            else if (statusId == 5)
            {
                subject = "Complaint Log No.: '" + complaintLogNo + "' approved On: " + approvedOn;
                link = "'" + urlTxt + "/COMPLAINT_LOG/UpdateComplaintLogStatusNewOne.aspx?complaintlogid=" + complaintLogID + "&complaintlogno=" + complaintLogNo + "&ud=" + serviceHODUserName + "&pd=" + serviceHODPassword + "&actid=6'";

                if (responsibleDeptID == 8 || responsibleDeptID == 9 || responsibleDeptID == 18)
                {
                    fileName = "~/COMPLAINT_LOG/EMAIL_FORMATS/05ApprovalAndClosingCLMail.htm";
                    href = "<a href=" + link + ">Approve and Close Complaint Request</a>";
                }
                else
                {
                    fileName = "~/COMPLAINT_LOG/EMAIL_FORMATS/06ClosingCLMail.htm";
                    href = "<a href=" + link + ">Close Complaint Request</a>";
                }

                from = deptHodEmailId;
                to = serviceHODEmailId;
            }

            //Closed
            else if (statusId == 6)
            {
                subject = "Complaint Log No.: '" + complaintLogNo + "' closed On: " + closedOn;
                fileName = "~/COMPLAINT_LOG/EMAIL_FORMATS/07ClosedCLMail.htm";

                from = serviceHODEmailId;
                to = createdByEmailId;
                cc = deptHodEmailId + ";" + resolvedByEmailId; //+";india.service@coperion.com";
            }

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(to))
            {
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
                string[] strCc = cc.Split(';');
                foreach (string item in strCc)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mail.CC.Add(item);
                    }
                }
            }

            mail.Subject = subject;
            mail.IsBodyHtml = true;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#complaintlogno#}", complaintLogNo);
            body = body.Replace("{#complaintrcvddate#}", complaintlogrcvddate);
            body = body.Replace("{#location#}", location);
            body = body.Replace("{#customername#}", customerName);
            body = body.Replace("{#address#}", address);
            body = body.Replace("{#plant#}", plant);
            body = body.Replace("{#jobno#}", jobNo);
            body = body.Replace("{#pono#}", poNo);
            body = body.Replace("{#itemname#}", itemName);
            body = body.Replace("{#modelno#}", modelNo);
            body = body.Replace("{#servicetype#}", serviceType);

            body = body.Replace("{#resppersonlessonlearnt#}", respPersonLessonLearnt);
            body = body.Replace("{#resphodlessonlearnt#}", respHodLessonLearnt);
            body = body.Replace("{#servicehodlessonlearnt#}", serviceHodLessonLearnt);

            body = body.Replace("{#responsibleperson#}", responsiblePersonName);
            body = body.Replace("{#businessunit#}", businessUnit);
            body = body.Replace("{#equipmentmanufacturername#}", txtEquipmentManufacturerName.Text);
            body = body.Replace("{#proposedactions#}", txtProposedActions.Text);
            body = body.Replace("{#rootcause#}", txtRootCause.Text);
            body = body.Replace("{#targetcompletiondate#}", Convert.ToDateTime(hdTargetCompletionDate.Value).ToString("dd-MMM-yyyy"));
            body = body.Replace("{#actualcompletiondate#}", Convert.ToDateTime(hdActualCompletionDate.Value).ToString("dd-MMM-yyyy"));
            body = body.Replace("{#correctiveaction#}", txtCorrectiveAction.Text);
            body = body.Replace("{#complaintdesc#}", txtComplaintDescription.Text);

            //TO ASSIGN-DEPT
            body = body.Replace("{#createdby#}", createdBy);
            body = body.Replace("{#createdon#}", createdOn);

            //TO ASSIGN-PERSON
            body = body.Replace("{#assigneddeptby#}", assignedDeptBy);
            body = body.Replace("{#assigneddepton#}", assignedDeptOn);
            body = body.Replace("{#respdepthodname#}", deptHodName);

            //TO RESOLVE
            body = body.Replace("{#assignedpersonby#}", assignedPersonBy);
            body = body.Replace("{#assignedpersonon#}", assignedPersonOn);

            //TO APPROVE
            body = body.Replace("{#approvedby#}", approvedBy);
            body = body.Replace("{#approvedon#}", approvedOn);

            //TO CLOSE
            body = body.Replace("{#resolvedby#}", resolvedBy);
            body = body.Replace("{#resolvedon#}", resolvedOn);
            body = body.Replace("{#servicedepthodname#}", serviceHODName);

            //TO REQUESTER
            body = body.Replace("{#closedby#}", closedBy);
            body = body.Replace("{#closedon#}", closedOn);

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

    private void Reset()
    {
        ddlResponsibleDepartment.SelectedIndex = 0;
        ddlResponsiblePerson.SelectedIndex = 0;
        ddlBusinessUnit.SelectedIndex = 0;
        txtEquipmentManufacturerName.Text = string.Empty;
        txtProposedActions.Text = string.Empty;
        txtRootCause.Text = string.Empty;
        hdTargetCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtTargetCompletionDate.Text = Convert.ToString(hdTargetCompletionDate.Value);

        hdActualCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtActualCompletionDate.Text = Convert.ToString(hdActualCompletionDate.Value);
        txtCorrectiveAction.Text = string.Empty;
        txtRespPersonLessonLearnt.Text = string.Empty;
        txtRespDeptHODLessonLearnt.Text = string.Empty;
        txtServiceDeptHODLessonLearnt.Text = string.Empty;
    }
    #endregion

}

public static class PublicValuesforComplaintLogList
{
    public static int actID = 0;
    public static int complaintLogId = 0;
    public static string complaintLogNo = string.Empty;
    public static int statusID = 0;
}