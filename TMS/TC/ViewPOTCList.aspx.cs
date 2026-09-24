using ICSharpCode.SharpZipLib.Zip;
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

public partial class TC_ViewPOTCList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.TC objTC = new BAL.TC();
    TCSendMail objTCSendMail = new TCSendMail();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsPOList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsDBDetails = new DataSet();
    DataSet dsTeams = new DataSet();
    DataSet dsStatus = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;

    int currentUserID = 0;
    int poRecordID = 0;
    int statusID = 0;


    int poTC1RecordID = 0;
    int poTC2RecordID = 0;
    int poTC3RecordID = 0;

    int poTC1StatusID = 0;
    int poTC2StatusID = 0;
    int poTC3StatusID = 0;

    string poNo = string.Empty;
    string poDate = string.Empty;
    string vendorCode = string.Empty;
    string vendorName = string.Empty;
    string JOBNo = string.Empty;
    int unitID = 0;
    string itemCode = string.Empty;
    string itemName = string.Empty;
    string identificationReferenceNo = string.Empty;

    string TC1remarks = string.Empty;
    string TC2remarks = string.Empty;
    string TC3remarks = string.Empty;

    int TC1TeamRecordID = 0;
    int TC2TeamRecordID = 0;
    int TC3TeamRecordID = 0;

    int TC1SeqNo = 0;
    int TC2SeqNo = 0;
    int TC3SeqNo = 0;

    int excludeCIDF = 0;
    int excludeEngineeringService = 0;

    int teamID = 0;

    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    Byte[] TC1FileBytes = null;
    Byte[] TC2FileBytes = null;
    Byte[] TC3FileBytes = null;

    string TC1File = string.Empty;
    string TC2File = string.Empty;
    string TC3File = string.Empty;

    int removedTC1ID = 0;
    int removedTC2ID = 0;
    int removedTC3ID = 0;

    string TC1IdentificationReferenceNo = string.Empty;
    string TC2IdentificationReferenceNo = string.Empty;
    string TC3IdentificationReferenceNo = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdUploadConfirmValue.Value = "0";
                hdUpdateStatausConfirmValue.Value = "0";

                Session["dtPODetailList"] = null;
                Session["dtPOList"] = null;
                Session["dtTcCheckers"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindUnit();
                BindTeams();
                BindTeamToUpload(0);
                BindStatus();
                GetPOList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetPOList();
    }

    protected void gvTCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                Label lblCreatedByID = (Label)e.Row.FindControl("lblCreatedByID");


                Label lblTCAttachment1 = (Label)e.Row.FindControl("lblTCAttachment1");
                Label lblTCAttachment2 = (Label)e.Row.FindControl("lblTCAttachment2");
                Label lblTCAttachment3 = (Label)e.Row.FindControl("lblTCAttachment3");

                Button btnCheck = (Button)e.Row.FindControl("btnCheck");
                Button btnEdit = (Button)e.Row.FindControl("btnEdit");
                ImageButton imgBtnSendMail = (ImageButton)e.Row.FindControl("imgBtnSendMail");

                Label lblTCStatusID1 = (Label)e.Row.FindControl("lblTCStatusID1");
                Label lblTCStatusID2 = (Label)e.Row.FindControl("lblTCStatusID2");
                Label lblTCStatusID3 = (Label)e.Row.FindControl("lblTCStatusID3");

                Label lblAssignedToID1 = (Label)e.Row.FindControl("lblAssignedToID1");
                Label lblAssignedToID2 = (Label)e.Row.FindControl("lblAssignedToID2");
                Label lblAssignedToID3 = (Label)e.Row.FindControl("lblAssignedToID3");

                Label lblAssignedTo1 = (Label)e.Row.FindControl("lblAssignedTo1");
                Label lblAssignedTo2 = (Label)e.Row.FindControl("lblAssignedTo2");
                Label lblAssignedTo3 = (Label)e.Row.FindControl("lblAssignedTo3");

                Label lblUploadedByIDTC1 = (Label)e.Row.FindControl("lblUploadedByIDTC1");
                Label lblUploadedByIDTC2 = (Label)e.Row.FindControl("lblUploadedByIDTC2");
                Label lblUploadedByIDTC3 = (Label)e.Row.FindControl("lblUploadedByIDTC3");

                Label lblAcceptedByIDTC1 = (Label)e.Row.FindControl("lblAcceptedByIDTC1");
                Label lblAcceptedByIDTC2 = (Label)e.Row.FindControl("lblAcceptedByIDTC2");
                Label lblAcceptedByIDTC3 = (Label)e.Row.FindControl("lblAcceptedByIDTC3");

                Label lblRejectedByIDTC1 = (Label)e.Row.FindControl("lblRejectedByIDTC1");
                Label lblRejectedByIDTC2 = (Label)e.Row.FindControl("lblRejectedByIDTC2");
                Label lblRejectedByIDTC3 = (Label)e.Row.FindControl("lblRejectedByIDTC3");

                Label lblIsAcceptedMailSentTC1 = (Label)e.Row.FindControl("lblIsAcceptedMailSentTC1");
                Label lblIsAcceptedMailSentTC2 = (Label)e.Row.FindControl("lblIsAcceptedMailSentTC2");
                Label lblIsAcceptedMailSentTC3 = (Label)e.Row.FindControl("lblIsAcceptedMailSentTC3");

                Label lblIsRejectedMailSentTC1 = (Label)e.Row.FindControl("lblIsRejectedMailSentTC1");
                Label lblIsRejectedMailSentTC2 = (Label)e.Row.FindControl("lblIsRejectedMailSentTC2");
                Label lblIsRejectedMailSentTC3 = (Label)e.Row.FindControl("lblIsRejectedMailSentTC3");

                CheckBox chkIsCompleted = (CheckBox)e.Row.FindControl("chkIsCompleted");
                Label lblIsCompleted = (Label)e.Row.FindControl("lblIsCompleted");

                Label lblIsCompletedRemarks = (Label)e.Row.FindControl("lblIsCompletedRemarks");
                if (Convert.ToInt32(lblIsCompleted.Text) > 0)
                {
                    chkIsCompleted.Checked = true;
                }
                else
                {
                    chkIsCompleted.Checked = false;
                }

                btnCheck.Visible = false;
                btnEdit.Visible = false;
                imgBtnSendMail.Visible = false;

                DataTable dtt = new DataTable();
                if (Session["dtTcCheckers"] != null)
                    dtt = (DataTable)Session["dtTcCheckers"];

                #region Open

                if (Convert.ToInt32(lblTCStatusID1.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(lblUploadedByIDTC1.Text) == currentUserID)
                    {
                        btnEdit.Visible = true;
                    }

                    //if (Convert.ToInt32(lblAssignedToID1.Text) == currentUserID)
                    //{
                    //    btnCheck.Visible = true;
                    //}

                    foreach (DataRow dr in dtt.Select("TC_DEPARTMENT_ID='" + Convert.ToInt32(lblAssignedToID1.Text) + "' AND EMP_RECORD_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                    {
                        btnCheck.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblTCStatusID2.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(lblUploadedByIDTC2.Text) == currentUserID)
                    {
                        btnEdit.Visible = true;
                    }

                    //if (Convert.ToInt32(lblAssignedToID2.Text) == currentUserID)
                    //{
                    //    btnCheck.Visible = true;
                    //}

                    foreach (DataRow dr in dtt.Select("TC_DEPARTMENT_ID='" + Convert.ToInt32(lblAssignedToID2.Text) + "' AND EMP_RECORD_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                    {
                        btnCheck.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblTCStatusID3.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(lblUploadedByIDTC3.Text) == currentUserID)
                    {
                        btnEdit.Visible = true;
                    }

                    //if (Convert.ToInt32(lblAssignedToID3.Text) == currentUserID)
                    //{
                    //    btnCheck.Visible = true;
                    //}

                    foreach (DataRow dr in dtt.Select("TC_DEPARTMENT_ID='" + Convert.ToInt32(lblAssignedToID3.Text) + "' AND EMP_RECORD_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                    {
                        btnCheck.Visible = true;
                    }
                }

                #endregion


                #region Accepted

                if (Convert.ToInt32(lblTCStatusID1.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                {
                    if (Convert.ToInt32(lblAcceptedByIDTC1.Text) == currentUserID)
                    {
                        if (Convert.ToInt32(lblIsAcceptedMailSentTC1.Text) == 0)
                            imgBtnSendMail.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblTCStatusID2.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                {
                    if (Convert.ToInt32(lblAcceptedByIDTC2.Text) == currentUserID)
                    {
                        if (Convert.ToInt32(lblIsAcceptedMailSentTC2.Text) == 0)
                            imgBtnSendMail.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblTCStatusID3.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                {
                    if (Convert.ToInt32(lblAcceptedByIDTC3.Text) == currentUserID)
                    {
                        if (Convert.ToInt32(lblIsAcceptedMailSentTC3.Text) == 0)
                            imgBtnSendMail.Visible = true;
                    }
                }

                #endregion


                #region Rejected

                if (Convert.ToInt32(lblTCStatusID1.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                {

                    if (Convert.ToInt32(lblUploadedByIDTC1.Text) == currentUserID)
                    {
                        btnEdit.Visible = true;
                    }

                    if (Convert.ToInt32(lblRejectedByIDTC1.Text) == currentUserID)
                    {
                        if (Convert.ToInt32(lblIsRejectedMailSentTC1.Text) == 0)
                            imgBtnSendMail.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblTCStatusID2.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                {
                    if (Convert.ToInt32(lblUploadedByIDTC2.Text) == currentUserID)
                    {
                        btnEdit.Visible = true;
                    }

                    if (Convert.ToInt32(lblRejectedByIDTC2.Text) == currentUserID)
                    {
                        if (Convert.ToInt32(lblIsRejectedMailSentTC2.Text) == 0)
                            imgBtnSendMail.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblTCStatusID3.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                {
                    if (Convert.ToInt32(lblUploadedByIDTC3.Text) == currentUserID)
                    {
                        btnEdit.Visible = true;
                    }

                    if (Convert.ToInt32(lblRejectedByIDTC3.Text) == currentUserID)
                    {
                        if (Convert.ToInt32(lblIsRejectedMailSentTC3.Text) == 0)
                            imgBtnSendMail.Visible = true;
                    }
                }

                #endregion


                if (Convert.ToInt32(lblTCStatusID1.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected) ||
                    Convert.ToInt32(lblTCStatusID2.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected) ||
                    Convert.ToInt32(lblTCStatusID3.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                    }
                }


                TextBox txtStatus = (TextBox)e.Row.FindControl("txtStatus");
                string statusTooltip = string.Empty;
                string assignedtoTooltip = string.Empty;

                txtStatus.Visible = false;
                if (!string.IsNullOrEmpty(Convert.ToString(txtStatus.Text)))
                {
                    txtStatus.Visible = true;
                    statusTooltip = "O- Open,\n" + "A- Accepted,\n" + "R- Rejected\n\n";

                    if (!string.IsNullOrEmpty(Convert.ToString(lblAssignedTo1.Text)))
                    {
                        assignedtoTooltip = assignedtoTooltip + "TC-1 Assienged To: " + Convert.ToString(lblAssignedTo1.Text) + "\n";
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblAssignedTo2.Text)))
                    {
                        assignedtoTooltip = assignedtoTooltip + "TC-2 Assienged To: " + Convert.ToString(lblAssignedTo2.Text) + "\n";
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblAssignedTo3.Text)))
                    {
                        assignedtoTooltip = assignedtoTooltip + "TC-3 Assienged To: " + Convert.ToString(lblAssignedTo3.Text) + "\n";
                    }

                    if (!string.IsNullOrEmpty(assignedtoTooltip))
                        txtStatus.ToolTip = statusTooltip + assignedtoTooltip;
                    else
                        txtStatus.ToolTip = statusTooltip;
                }


                ImageButton imgBtnTCAttachment1 = (ImageButton)e.Row.FindControl("imgBtnTCAttachment1");
                ImageButton imgBtnTCAttachment2 = (ImageButton)e.Row.FindControl("imgBtnTCAttachment2");
                ImageButton imgBtnTCAttachment3 = (ImageButton)e.Row.FindControl("imgBtnTCAttachment3");

                imgBtnTCAttachment1.Visible = false;
                imgBtnTCAttachment2.Visible = false;
                imgBtnTCAttachment3.Visible = false;

                if (!string.IsNullOrEmpty(Convert.ToString(lblTCAttachment1.Text)))
                {
                    imgBtnTCAttachment1.Visible = true;
                    imgBtnTCAttachment1.ToolTip = lblTCAttachment1.Text;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblTCAttachment2.Text)))
                {
                    imgBtnTCAttachment2.Visible = true;
                    imgBtnTCAttachment2.ToolTip = lblTCAttachment2.Text;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblTCAttachment3.Text)))
                {
                    imgBtnTCAttachment3.Visible = true;
                    imgBtnTCAttachment3.ToolTip = lblTCAttachment3.Text;
                }





                if (Convert.ToInt32(lblTCStatusID2.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(lblUploadedByIDTC2.Text) == currentUserID)
                    {
                        btnEdit.Visible = true;
                    }

                    if (Convert.ToInt32(lblAssignedToID2.Text) == currentUserID)
                    {
                        btnCheck.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblTCStatusID3.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(lblUploadedByIDTC3.Text) == currentUserID)
                    {
                        btnEdit.Visible = true;
                    }

                    if (Convert.ToInt32(lblAssignedToID3.Text) == currentUserID)
                    {
                        btnCheck.Visible = true;
                    }
                }

            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i > 0)
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

    protected void gvTCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                BindStatusToUpdate();

                hdIsCompletedRemarks.Value = "";
                hdUploadedFlag.Value = "0";
                hdUpdateStatusFlag.Value = "0";
                hdTC1SeqNo.Value = "0";
                hdTC2SeqNo.Value = "0";
                hdTC3SeqNo.Value = "0";

                hdRemovedTC1ID.Value = "0";
                hdRemovedTC2ID.Value = "0";
                hdRemovedTC3ID.Value = "0";

                hdTC1ToUpdateStatusFlag.Value = "0";
                hdTC2ToUpdateStatusFlag.Value = "0";
                hdTC3ToUpdateStatusFlag.Value = "0";

                ViewState["RECORD_ID"] = 0;
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "CHECK_TC" ||
                    Convert.ToString(e.CommandArgument) == "EDIT")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewPO")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }



                Label lblPORecordID = gvTCList.Rows[rowindex].FindControl("lblPORecordID") as Label;

                Label lblTCRecordID1 = gvTCList.Rows[rowindex].FindControl("lblTCRecordID1") as Label;
                Label lblTCRecordID2 = gvTCList.Rows[rowindex].FindControl("lblTCRecordID2") as Label;
                Label lblTCRecordID3 = gvTCList.Rows[rowindex].FindControl("lblTCRecordID3") as Label;

                Label lblTCStatusID1 = gvTCList.Rows[rowindex].FindControl("lblTCStatusID1") as Label;
                Label lblTCStatusID2 = gvTCList.Rows[rowindex].FindControl("lblTCStatusID2") as Label;
                Label lblTCStatusID3 = gvTCList.Rows[rowindex].FindControl("lblTCStatusID3") as Label;

                Label lblTCAttachment1 = gvTCList.Rows[rowindex].FindControl("lblTCAttachment1") as Label;
                Label lblTCAttachment2 = gvTCList.Rows[rowindex].FindControl("lblTCAttachment2") as Label;
                Label lblTCAttachment3 = gvTCList.Rows[rowindex].FindControl("lblTCAttachment3") as Label;

                Label lblAssignedToID1 = gvTCList.Rows[rowindex].FindControl("lblAssignedToID1") as Label;
                Label lblAssignedToID2 = gvTCList.Rows[rowindex].FindControl("lblAssignedToID2") as Label;
                Label lblAssignedToID3 = gvTCList.Rows[rowindex].FindControl("lblAssignedToID3") as Label;

                Label lblUploadedByIDTC1 = gvTCList.Rows[rowindex].FindControl("lblUploadedByIDTC1") as Label;
                Label lblUploadedByIDTC2 = gvTCList.Rows[rowindex].FindControl("lblUploadedByIDTC2") as Label;
                Label lblUploadedByIDTC3 = gvTCList.Rows[rowindex].FindControl("lblUploadedByIDTC3") as Label;

                Label lblAcceptedByIDTC1 = gvTCList.Rows[rowindex].FindControl("lblAcceptedByIDTC1") as Label;
                Label lblAcceptedByIDTC2 = gvTCList.Rows[rowindex].FindControl("lblAcceptedByIDTC2") as Label;
                Label lblAcceptedByIDTC3 = gvTCList.Rows[rowindex].FindControl("lblAcceptedByIDTC3") as Label;

                Label lblRejectedByIDTC1 = gvTCList.Rows[rowindex].FindControl("lblRejectedByIDTC1") as Label;
                Label lblRejectedByIDTC2 = gvTCList.Rows[rowindex].FindControl("lblRejectedByIDTC2") as Label;
                Label lblRejectedByIDTC3 = gvTCList.Rows[rowindex].FindControl("lblRejectedByIDTC3") as Label;

                Label lblTCSeqNo1 = gvTCList.Rows[rowindex].FindControl("lblTCSeqNo1") as Label;
                Label lblTCSeqNo2 = gvTCList.Rows[rowindex].FindControl("lblTCSeqNo2") as Label;
                Label lblTCSeqNo3 = gvTCList.Rows[rowindex].FindControl("lblTCSeqNo3") as Label;

                Label lblTC1IdentificationReferenceNo = gvTCList.Rows[rowindex].FindControl("lblTC1IdentificationReferenceNo") as Label;
                Label lblTC2IdentificationReferenceNo = gvTCList.Rows[rowindex].FindControl("lblTC2IdentificationReferenceNo") as Label;
                Label lblTC3IdentificationReferenceNo = gvTCList.Rows[rowindex].FindControl("lblTC3IdentificationReferenceNo") as Label;


                Label lblUploadedRemarks1 = gvTCList.Rows[rowindex].FindControl("lblUploadedRemarks1") as Label;
                Label lblUploadedRemarks2 = gvTCList.Rows[rowindex].FindControl("lblUploadedRemarks2") as Label;
                Label lblUploadedRemarks3 = gvTCList.Rows[rowindex].FindControl("lblUploadedRemarks3") as Label;

                Label lblAcceptedRemarks1 = gvTCList.Rows[rowindex].FindControl("lblAcceptedRemarks1") as Label;
                Label lblAcceptedRemarks2 = gvTCList.Rows[rowindex].FindControl("lblAcceptedRemarks2") as Label;
                Label lblAcceptedRemarks3 = gvTCList.Rows[rowindex].FindControl("lblAcceptedRemarks3") as Label;

                Label lblRejectedRemarks1 = gvTCList.Rows[rowindex].FindControl("lblRejectedRemarks1") as Label;
                Label lblRejectedRemarks2 = gvTCList.Rows[rowindex].FindControl("lblRejectedRemarks2") as Label;
                Label lblRejectedRemarks3 = gvTCList.Rows[rowindex].FindControl("lblRejectedRemarks3") as Label;

                Label lblIsCompleted = gvTCList.Rows[rowindex].FindControl("lblIsCompleted") as Label;
                Label lblIsCompletedRemarks = gvTCList.Rows[rowindex].FindControl("lblIsCompletedRemarks") as Label;

                Label lblPONo = gvTCList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblPODate = gvTCList.Rows[rowindex].FindControl("lblPODate") as Label;
                Label lblVendorCode = gvTCList.Rows[rowindex].FindControl("lblVendorCode") as Label;
                Label lblVendorName = gvTCList.Rows[rowindex].FindControl("lblVendorName") as Label;
                Label lblJOBNo = gvTCList.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblPOFirstItemName = gvTCList.Rows[rowindex].FindControl("lblPOFirstItemName") as Label;
                Label lblPOFirstItemCode = gvTCList.Rows[rowindex].FindControl("lblPOFirstItemCode") as Label;
                Label lblUnitID = gvTCList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblUnit = gvTCList.Rows[rowindex].FindControl("lblUnit") as Label;


                chkIsCompleted.Enabled = false;
                chkIsCompleted.Checked = false;


                trTC1.Visible = false;
                trTC2.Visible = false;
                trTC3.Visible = false;

                trTC1RemarksAndStatus.Visible = false;
                trTC2RemarksAndStatus.Visible = false;
                trTC3RemarksAndStatus.Visible = false;

                if (Convert.ToString(e.CommandArgument) == "CHECK_TC" ||
                    Convert.ToString(e.CommandArgument) == "EDIT")
                {
                    ViewState["RECORD_ID"] = Convert.ToInt32(lblPORecordID.Text);

                    //ViewState["TC_RECORD_ID1"] = Convert.ToInt32(lblTCRecordID1.Text);
                    //ViewState["TC_RECORD_ID2"] = Convert.ToInt32(lblTCRecordID2.Text);
                    //ViewState["TC_RECORD_ID3"] = Convert.ToInt32(lblTCRecordID3.Text);

                    hdTC1RecordID.Value = Convert.ToString(lblTCRecordID1.Text);
                    hdTC2RecordID.Value = Convert.ToString(lblTCRecordID2.Text);
                    hdTC3RecordID.Value = Convert.ToString(lblTCRecordID3.Text);

                    ViewState["TC_ASSIGNED_TO_ID1"] = Convert.ToInt32(lblAssignedToID1.Text);
                    ViewState["TC_ASSIGNED_TO_ID2"] = Convert.ToInt32(lblAssignedToID2.Text);
                    ViewState["TC_ASSIGNED_TO_ID3"] = Convert.ToInt32(lblAssignedToID3.Text);

                    ViewState["TC_ASSIGNED_TO_ID1"] = Convert.ToInt32(lblAssignedToID1.Text);
                    ViewState["TC_ASSIGNED_TO_ID2"] = Convert.ToInt32(lblAssignedToID2.Text);
                    ViewState["TC_ASSIGNED_TO_ID3"] = Convert.ToInt32(lblAssignedToID3.Text);

                    hdTC1SeqNo.Value = Convert.ToString(lblTCSeqNo1.Text);
                    hdTC2SeqNo.Value = Convert.ToString(lblTCSeqNo2.Text);
                    hdTC3SeqNo.Value = Convert.ToString(lblTCSeqNo3.Text);




                    txtPONoToUpload.Text = Convert.ToString(lblPONo.Text);
                    txtPODateToUpload.Text = Convert.ToString(lblPODate.Text);
                    txtVendorCodeToUpload.Text = Convert.ToString(lblVendorCode.Text);
                    txtVendorNameToUpload.Text = Convert.ToString(lblVendorName.Text);
                    txtJOBNoToUpload.Text = Convert.ToString(lblJOBNo.Text);
                    txtItemCodeToUpload.Text = Convert.ToString(lblPOFirstItemCode.Text);
                    txtItemNameToUpload.Text = Convert.ToString(lblPOFirstItemName.Text);

                    ViewState["UNIT_ID"] = Convert.ToInt32(lblUnitID.Text);
                    BindTeamToUpload(Convert.ToInt32(lblUnitID.Text));

                    txtUnitToUpload.Text = Convert.ToString(lblUnit.Text);

                    if (Convert.ToInt32(lblIsCompleted.Text) > 0)
                    {
                        chkIsCompleted.Checked = true;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblIsCompletedRemarks.Text)))
                    {
                        txtIscompletedRemarks.Text = Convert.ToString(lblIsCompletedRemarks.Text);
                    }
                    else
                    {
                        txtIscompletedRemarks.Text = string.Empty;
                    }

                    imgBtnStatus1.Visible = false;
                    imgBtnStatus2.Visible = false;
                    imgBtnStatus3.Visible = false;

                    txtTC1RemarksToUpload.Text = string.Empty;
                    txtTC2RemarksToUpload.Text = string.Empty;
                    txtTC3RemarksToUpload.Text = string.Empty;

                    txtTC1RemarksToUpload.Enabled = false;
                    txtTC2RemarksToUpload.Enabled = false;
                    txtTC3RemarksToUpload.Enabled = false;

                    ddlTC1TeamToUpload.Enabled = false;
                    ddlTC2TeamToUpload.Enabled = false;
                    ddlTC3TeamToUpload.Enabled = false;

                    txtTC1UploadedRemarks.Enabled = false;
                    txtTC2UploadedRemarks.Enabled = false;
                    txtTC3UploadedRemarks.Enabled = false;

                    txtTC1IdentificationReferenceNo.Enabled = false;
                    txtTC2IdentificationReferenceNo.Enabled = false;
                    txtTC3IdentificationReferenceNo.Enabled = false;

                    imgBtnViewTC1ToView.Visible = true;
                    imgBtnViewTC2ToView.Visible = true;
                    imgBtnViewTC3ToView.Visible = true;

                    pnlViewTC1.Visible = true;
                    pnlViewTC2.Visible = true;
                    pnlViewTC3.Visible = true;

                    imgBtnRemoveTC1ToView.Visible = false;
                    imgBtnRemoveTC2ToView.Visible = false;
                    imgBtnRemoveTC3ToView.Visible = false;

                    pnlUploadTC1ToUpload.Visible = false;
                    pnlUploadTC2ToUpload.Visible = false;
                    pnlUploadTC3ToUpload.Visible = false;


                    lblAttachOrViewTxtTC1.Text = "Attach ";
                    lblAttachOrViewTxtTC2.Text = "Attach ";
                    lblAttachOrViewTxtTC3.Text = "Attach ";

                    ddlStatusTC1.Visible = false;
                    ddlStatusTC1.Visible = false;
                    ddlStatusTC1.Visible = false;

                    ddlStatusTC1.Enabled = false;
                    ddlStatusTC1.Enabled = false;
                    ddlStatusTC1.Enabled = false;


                    ddlStatusTC2.Visible = false;
                    ddlStatusTC2.Visible = false;
                    ddlStatusTC2.Visible = false;

                    ddlStatusTC2.Enabled = false;
                    ddlStatusTC2.Enabled = false;
                    ddlStatusTC2.Enabled = false;


                    ddlStatusTC3.Visible = false;
                    ddlStatusTC3.Visible = false;
                    ddlStatusTC3.Visible = false;

                    ddlStatusTC3.Enabled = false;
                    ddlStatusTC3.Enabled = false;
                    ddlStatusTC3.Enabled = false;

                    if (Convert.ToInt32(lblAssignedToID1.Text) > 0)
                        ddlTC1TeamToUpload.SelectedValue = Convert.ToString(lblAssignedToID1.Text);
                    else
                        ddlTC1TeamToUpload.SelectedIndex = 0;

                    if (Convert.ToInt32(lblAssignedToID2.Text) > 0)
                        ddlTC2TeamToUpload.SelectedValue = Convert.ToString(lblAssignedToID2.Text);
                    else
                        ddlTC2TeamToUpload.SelectedIndex = 0;

                    if (Convert.ToInt32(lblAssignedToID3.Text) > 0)
                        ddlTC3TeamToUpload.SelectedValue = Convert.ToString(lblAssignedToID3.Text);
                    else
                        ddlTC3TeamToUpload.SelectedIndex = 0;



                    txtTC1IdentificationReferenceNo.Text = Convert.ToString(lblTC1IdentificationReferenceNo.Text);
                    txtTC2IdentificationReferenceNo.Text = Convert.ToString(lblTC2IdentificationReferenceNo.Text);
                    txtTC3IdentificationReferenceNo.Text = Convert.ToString(lblTC3IdentificationReferenceNo.Text);


                    txtTC1UploadedRemarks.Text = Convert.ToString(lblUploadedRemarks1.Text);
                    txtTC2UploadedRemarks.Text = Convert.ToString(lblUploadedRemarks2.Text);
                    txtTC3UploadedRemarks.Text = Convert.ToString(lblUploadedRemarks3.Text);


                    DataTable dtt = new DataTable();
                    if (Session["dtTcCheckers"] != null)
                        dtt = (DataTable)Session["dtTcCheckers"];

                    #region Open

                    if (Convert.ToInt32(lblTCStatusID1.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                    {
                        txtTC1RemarksToUpload.Text = Convert.ToString(lblUploadedRemarks1.Text);
                        imgBtnStatus1.Visible = true;
                        imgBtnStatus1.ImageUrl = "~/Images/NEWICONS/open3.png";
                        imgBtnStatus1.ToolTip = "Open";

                        if (currentUserID == Convert.ToInt32(lblUploadedByIDTC1.Text))
                        {
                            imgBtnRemoveTC1ToView.Visible = true;

                            ddlTC1TeamToUpload.Enabled = true;
                            txtTC1UploadedRemarks.Enabled = true;
                            txtTC1IdentificationReferenceNo.Enabled = true;

                        }

                        //if (currentUserID == Convert.ToInt32(lblAssignedToID1.Text))
                        //{
                        //    trTC1.Visible = true;
                        //    hdTC1ToUpdateStatusFlag.Value = "1";
                        //    txtTC1RemarksToUpload.Text = string.Empty;
                        //    txtTC1RemarksToUpload.Enabled = true;

                        //    ddlStatusTC1.Visible = true;
                        //    ddlStatusTC1.Enabled = true;
                        //}

                        foreach (DataRow dr in dtt.Select("TC_DEPARTMENT_ID='" + Convert.ToInt32(lblAssignedToID1.Text) + "' AND EMP_RECORD_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                        {
                            trTC1.Visible = true;
                            hdTC1ToUpdateStatusFlag.Value = "1";
                            txtTC1RemarksToUpload.Text = string.Empty;
                            txtTC1RemarksToUpload.Enabled = true;

                            ddlStatusTC1.Visible = true;
                            ddlStatusTC1.Enabled = true;
                        }

                    }

                    if (Convert.ToInt32(lblTCStatusID2.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                    {
                        txtTC2RemarksToUpload.Text = Convert.ToString(lblUploadedRemarks2.Text);
                        imgBtnStatus2.Visible = true;
                        imgBtnStatus2.ImageUrl = "~/Images/NEWICONS/open3.png";
                        imgBtnStatus2.ToolTip = "Open";

                        if (currentUserID == Convert.ToInt32(lblUploadedByIDTC2.Text))
                        {
                            imgBtnRemoveTC2ToView.Visible = true;

                            ddlTC2TeamToUpload.Enabled = true;
                            txtTC2UploadedRemarks.Enabled = true;
                            txtTC2IdentificationReferenceNo.Enabled = true;
                        }

                        //if (currentUserID == Convert.ToInt32(lblAssignedToID2.Text))
                        //{
                        //    trTC2.Visible = true;
                        //    hdTC2ToUpdateStatusFlag.Value = "1";
                        //    txtTC2RemarksToUpload.Text = string.Empty;
                        //    txtTC2RemarksToUpload.Enabled = true;

                        //    ddlStatusTC2.Visible = true;
                        //    ddlStatusTC2.Enabled = true;
                        //}


                        foreach (DataRow dr in dtt.Select("TC_DEPARTMENT_ID='" + Convert.ToInt32(lblAssignedToID2.Text) + "' AND EMP_RECORD_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                        {
                            trTC2.Visible = true;
                            hdTC2ToUpdateStatusFlag.Value = "1";
                            txtTC2RemarksToUpload.Text = string.Empty;
                            txtTC2RemarksToUpload.Enabled = true;

                            ddlStatusTC2.Visible = true;
                            ddlStatusTC2.Enabled = true;
                        }

                    }

                    if (Convert.ToInt32(lblTCStatusID3.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                    {
                        txtTC3RemarksToUpload.Text = Convert.ToString(lblUploadedRemarks3.Text);
                        imgBtnStatus3.Visible = true;
                        imgBtnStatus3.ImageUrl = "~/Images/NEWICONS/open3.png";
                        imgBtnStatus3.ToolTip = "Open";

                        if (currentUserID == Convert.ToInt32(lblUploadedByIDTC3.Text))
                        {
                            imgBtnRemoveTC3ToView.Visible = true;

                            ddlTC3TeamToUpload.Enabled = true;
                            txtTC3UploadedRemarks.Enabled = true;
                            txtTC3IdentificationReferenceNo.Enabled = true;
                        }

                        //if (currentUserID == Convert.ToInt32(lblAssignedToID3.Text))
                        //{
                        //    trTC3.Visible = true;
                        //    hdTC3ToUpdateStatusFlag.Value = "1";
                        //    txtTC3RemarksToUpload.Text = string.Empty;
                        //    txtTC3RemarksToUpload.Enabled = true;

                        //    ddlStatusTC3.Visible = true;
                        //    ddlStatusTC3.Enabled = true;
                        //}

                        foreach (DataRow dr in dtt.Select("TC_DEPARTMENT_ID='" + Convert.ToInt32(lblAssignedToID3.Text) + "' AND EMP_RECORD_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                        {
                            trTC3.Visible = true;
                            hdTC3ToUpdateStatusFlag.Value = "1";
                            txtTC3RemarksToUpload.Text = string.Empty;
                            txtTC3RemarksToUpload.Enabled = true;

                            ddlStatusTC3.Visible = true;
                            ddlStatusTC3.Enabled = true;
                        }

                    }


                    #endregion

                    #region Accepted

                    if (Convert.ToInt32(lblTCStatusID1.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                    {
                        trTC1.Visible = true;
                        imgBtnRemoveTC1ToView.Visible = false;
                        txtTC1RemarksToUpload.Text = Convert.ToString(lblAcceptedRemarks1.Text);
                        imgBtnStatus1.Visible = true;
                        imgBtnStatus1.ImageUrl = "~/Images/NEWICONS/accepted21.png";
                        imgBtnStatus1.ToolTip = "Accepted";

                        if (!string.IsNullOrEmpty(Convert.ToString(lblTCStatusID1.Text)))
                            ddlStatusTC1.SelectedValue = Convert.ToString(lblTCStatusID1.Text);
                        else
                            ddlStatusTC1.SelectedIndex = 0;

                        ddlStatusTC1.Visible = true;
                    }

                    if (Convert.ToInt32(lblTCStatusID2.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                    {
                        trTC2.Visible = true;
                        imgBtnRemoveTC2ToView.Visible = false;
                        txtTC2RemarksToUpload.Text = Convert.ToString(lblAcceptedRemarks2.Text);
                        imgBtnStatus2.Visible = true;
                        imgBtnStatus2.ImageUrl = "~/Images/NEWICONS/accepted21.png";
                        imgBtnStatus2.ToolTip = "Accepted";

                        if (!string.IsNullOrEmpty(Convert.ToString(lblTCStatusID2.Text)))
                            ddlStatusTC2.SelectedValue = Convert.ToString(lblTCStatusID2.Text);
                        else
                            ddlStatusTC2.SelectedIndex = 0;

                        ddlStatusTC2.Visible = true;
                    }

                    if (Convert.ToInt32(lblTCStatusID3.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                    {
                        trTC3.Visible = true;
                        imgBtnRemoveTC3ToView.Visible = false;
                        txtTC3RemarksToUpload.Text = Convert.ToString(lblAcceptedRemarks3.Text);
                        imgBtnStatus3.Visible = true;
                        imgBtnStatus3.ImageUrl = "~/Images/NEWICONS/accepted21.png";
                        imgBtnStatus3.ToolTip = "Accepted";

                        if (!string.IsNullOrEmpty(Convert.ToString(lblTCStatusID3.Text)))
                            ddlStatusTC3.SelectedValue = Convert.ToString(lblTCStatusID3.Text);
                        else
                            ddlStatusTC3.SelectedIndex = 0;

                        ddlStatusTC3.Visible = true;
                    }


                    #endregion

                    #region Rejected

                    if (Convert.ToInt32(lblTCStatusID1.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                    {
                        trTC1.Visible = true;
                        txtTC1RemarksToUpload.Text = Convert.ToString(lblRejectedRemarks1.Text);
                        imgBtnStatus1.Visible = true;
                        imgBtnStatus1.ImageUrl = "~/Images/Icons/no2.png";
                        imgBtnStatus1.ToolTip = "Rejected";

                        if (currentUserID == Convert.ToInt32(lblUploadedByIDTC1.Text))
                        {
                            imgBtnRemoveTC1ToView.Visible = true;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(lblTCStatusID1.Text)))
                            ddlStatusTC1.SelectedValue = Convert.ToString(lblTCStatusID1.Text);
                        else
                            ddlStatusTC1.SelectedIndex = 0;

                        ddlStatusTC1.Visible = true;
                    }

                    if (Convert.ToInt32(lblTCStatusID2.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                    {
                        trTC2.Visible = true;
                        txtTC2RemarksToUpload.Text = Convert.ToString(lblRejectedRemarks2.Text);
                        imgBtnStatus2.Visible = true;
                        imgBtnStatus2.ImageUrl = "~/Images/Icons/no2.png";
                        imgBtnStatus2.ToolTip = "Rejected";

                        if (currentUserID == Convert.ToInt32(lblUploadedByIDTC2.Text))
                        {
                            imgBtnRemoveTC2ToView.Visible = true;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(lblTCStatusID2.Text)))
                            ddlStatusTC2.SelectedValue = Convert.ToString(lblTCStatusID2.Text);
                        else
                            ddlStatusTC2.SelectedIndex = 0;

                        ddlStatusTC2.Visible = true;
                    }

                    if (Convert.ToInt32(lblTCStatusID3.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                    {
                        trTC3.Visible = true;
                        txtTC3RemarksToUpload.Text = Convert.ToString(lblRejectedRemarks3.Text);
                        imgBtnStatus3.Visible = true;
                        imgBtnStatus3.ImageUrl = "~/Images/Icons/no2.png";
                        imgBtnStatus3.ToolTip = "Rejected";

                        if (currentUserID == Convert.ToInt32(lblUploadedByIDTC3.Text))
                        {
                            imgBtnRemoveTC3ToView.Visible = true;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(lblTCStatusID3.Text)))
                            ddlStatusTC3.SelectedValue = Convert.ToString(lblTCStatusID3.Text);
                        else
                            ddlStatusTC3.SelectedIndex = 0;

                        ddlStatusTC3.Visible = true;
                    }


                    #endregion


                    if (Convert.ToInt32(lblTCStatusID1.Text) == 0)
                    {
                        txtTC1RemarksToUpload.Enabled = true;
                        ddlTC1TeamToUpload.Enabled = true;
                        txtTC1IdentificationReferenceNo.Enabled = true;
                        txtTC1UploadedRemarks.Enabled = true;
                    }

                    if (Convert.ToInt32(lblTCStatusID2.Text) == 0)
                    {
                        txtTC2RemarksToUpload.Enabled = true;
                        ddlTC2TeamToUpload.Enabled = true;
                        txtTC2IdentificationReferenceNo.Enabled = true;
                        txtTC2UploadedRemarks.Enabled = true;
                    }

                    if (Convert.ToInt32(lblTCStatusID3.Text) == 0)
                    {
                        txtTC3RemarksToUpload.Enabled = true;
                        ddlTC3TeamToUpload.Enabled = true;
                        txtTC3IdentificationReferenceNo.Enabled = true;
                        txtTC3UploadedRemarks.Enabled = true;
                    }

                    ViewState["TC_REMARKS1"] = txtTC1RemarksToUpload.Text;
                    ViewState["TC_REMARKS2"] = txtTC2RemarksToUpload.Text;
                    ViewState["TC_REMARKS3"] = txtTC3RemarksToUpload.Text;

                    ViewState["IDENTIFICATION_REFERENCE_NO1"] = lblTC1IdentificationReferenceNo.Text;
                    ViewState["IDENTIFICATION_REFERENCE_NO2"] = lblTC2IdentificationReferenceNo.Text;
                    ViewState["IDENTIFICATION_REFERENCE_NO3"] = lblTC3IdentificationReferenceNo.Text;

                    string attachment1Extn = string.Empty;
                    string attachment2Extn = string.Empty;
                    string attachment3Extn = string.Empty;

                    if (!string.IsNullOrEmpty(lblTCAttachment1.Text))
                    {
                        hdTC1ToView.Value = "1";
                        hdFileTC1ToUpload.Value = "0";

                        pnlViewTC1.Visible = true;
                        pnlUploadTC1ToUpload.Visible = false;

                        imgBtnViewTC1ToView.Visible = true;

                        attachment1Extn = Convert.ToString(lblTCAttachment1.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewTC1ToView.ImageUrl = "~/Images/imgicon1.png";
                            txtTC1ToView.Text = lblTCAttachment1.Text;
                            txtTC1ToView.ToolTip = lblTCAttachment1.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewTC1ToView.ImageUrl = "~/Images/pdficon1.png";
                            txtTC1ToView.Text = lblTCAttachment1.Text;
                            txtTC1ToView.ToolTip = lblTCAttachment1.Text;
                        }
                    }
                    else
                    {
                        hdTC1ToView.Value = "0";
                        hdFileTC1ToUpload.Value = "1";

                        pnlViewTC1.Visible = false;
                        pnlUploadTC1ToUpload.Visible = true;

                        imgBtnViewTC1ToView.Visible = false;
                        imgBtnUndofileTC1ToUpload.Visible = false;
                    }


                    if (!string.IsNullOrEmpty(lblTCAttachment2.Text))
                    {
                        hdTC2ToView.Value = "1";
                        hdFileTC2ToUpload.Value = "0";

                        pnlViewTC2.Visible = true;
                        pnlUploadTC2ToUpload.Visible = false;

                        imgBtnViewTC2ToView.Visible = true;

                        attachment2Extn = Convert.ToString(lblTCAttachment2.Text).Split('.').Last();
                        if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
                        {
                            imgBtnViewTC2ToView.ImageUrl = "~/Images/imgicon1.png";
                            txtTC2ToView.Text = lblTCAttachment2.Text;
                            txtTC2ToView.ToolTip = lblTCAttachment2.Text;
                        }
                        else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
                        {
                            imgBtnViewTC2ToView.ImageUrl = "~/Images/pdficon1.png";
                            txtTC2ToView.Text = lblTCAttachment2.Text;
                            txtTC2ToView.ToolTip = lblTCAttachment2.Text;
                        }
                    }
                    else
                    {
                        hdTC2ToView.Value = "0";
                        hdFileTC2ToUpload.Value = "1";

                        pnlViewTC2.Visible = false;
                        pnlUploadTC2ToUpload.Visible = true;

                        imgBtnViewTC2ToView.Visible = false;
                        imgBtnUndofileTC2ToUpload.Visible = false;
                    }


                    if (!string.IsNullOrEmpty(lblTCAttachment3.Text))
                    {
                        hdTC3ToView.Value = "1";
                        hdFileTC3ToUpload.Value = "0";

                        pnlViewTC3.Visible = true;
                        pnlUploadTC3ToUpload.Visible = false;

                        imgBtnViewTC3ToView.Visible = true;

                        attachment3Extn = Convert.ToString(lblTCAttachment3.Text).Split('.').Last();
                        if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" || attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
                        {
                            imgBtnViewTC3ToView.ImageUrl = "~/Images/imgicon1.png";
                            txtTC3ToView.Text = lblTCAttachment3.Text;
                            txtTC3ToView.ToolTip = lblTCAttachment3.Text;
                        }
                        else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
                        {
                            imgBtnViewTC3ToView.ImageUrl = "~/Images/pdficon1.png";
                            txtTC3ToView.Text = lblTCAttachment3.Text;
                            txtTC3ToView.ToolTip = lblTCAttachment3.Text;
                        }
                    }
                    else
                    {
                        hdTC3ToView.Value = "0";
                        hdFileTC3ToUpload.Value = "1";

                        pnlViewTC3.Visible = false;
                        pnlUploadTC3ToUpload.Visible = true;

                        imgBtnViewTC3ToView.Visible = false;
                        imgBtnUndofileTC3ToUpload.Visible = false;
                    }


                    if (Convert.ToString(e.CommandArgument) == "CHECK_TC")
                    {
                        lblTCHeadingText.Text = "Accept/Reject TC";
                        imgBtnRemoveTC1ToView.Visible = false;
                        imgBtnRemoveTC2ToView.Visible = false;
                        imgBtnRemoveTC3ToView.Visible = false;
                        btnUpload.Visible = false;
                        btnUpdateStatus.Visible = true;

                        trTC1RemarksAndStatus.Visible = true;
                        trTC2RemarksAndStatus.Visible = true;
                        trTC3RemarksAndStatus.Visible = true;
                    }

                    if (Convert.ToString(e.CommandArgument) == "EDIT")
                    {
                        trTC1.Visible = true;
                        trTC2.Visible = true;
                        trTC3.Visible = true;

                        lblTCHeadingText.Text = "Update TC";
                        btnUpload.Visible = true;
                        btnUpdateStatus.Visible = false;
                        chkIsCompleted.Enabled = true;
                    }

                    mpeDetail.Show();
                }


                if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    mpeViewInPDF.Show();
                    iframeViewTCDetailsInPDF.Attributes.Add("src", "TCInPDF.aspx?recordid=" + Convert.ToInt32(lblPORecordID.Text) + "");
                }


                else if (Convert.ToString(e.CommandArgument) == "ViewPO")
                {
                    ModalPopupExtender4.Show();
                    iframeViewPOInPDF.Attributes.Add("src", "POInPDF.aspx?PONo=" + Convert.ToString(lblPONo.Text).Trim().ToUpper() + "&unitid=" + Convert.ToInt32(lblUnitID.Text) + "");
                }


                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                    ViewDrawingFiles(Convert.ToInt32(lblPORecordID.Text), Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC1), Convert.ToString(lblTCAttachment1.Text).Trim());

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                    ViewDrawingFiles(Convert.ToInt32(lblPORecordID.Text), Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC2), Convert.ToString(lblTCAttachment2.Text).Trim());

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                    ViewDrawingFiles(Convert.ToInt32(lblPORecordID.Text), Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC3), Convert.ToString(lblTCAttachment3.Text).Trim());
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





    protected void imgBtnViewTC1ToView_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(ViewState["RECORD_ID"]), Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC1), txtTC1ToView.Text.Trim());
        mpeDetail.Show();
    }

    protected void imgBtnViewTC2ToView_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(ViewState["RECORD_ID"]), Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC2), txtTC2ToView.Text.Trim());
        mpeDetail.Show();
    }

    protected void imgBtnViewTC3ToView_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(ViewState["RECORD_ID"]), Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC3), txtTC3ToView.Text.Trim());
        mpeDetail.Show();
    }


    protected void imgBtnRemoveTC1ToView_Click(object sender, ImageClickEventArgs e)
    {
        hdRemovedTC1ID.Value = Convert.ToString(hdTC1RecordID.Value);
        hdUploadedFlag.Value = "0";
        hdTC1ToView.Value = "0";
        hdFileTC1ToUpload.Value = "1";

        pnlViewTC1.Visible = false;
        pnlUploadTC1ToUpload.Visible = true;
        imgBtnUndofileTC1ToUpload.Visible = true;

        ddlTC1TeamToUpload.Enabled = true;
        txtTC1UploadedRemarks.Enabled = true;
        txtTC1IdentificationReferenceNo.Enabled = true;

        lblAttachOrViewTxtTC1.Text = "Attach ";

        mpeDetail.Show();
    }

    protected void imgBtnRemoveTC2ToView_Click(object sender, ImageClickEventArgs e)
    {
        hdRemovedTC2ID.Value = Convert.ToString(hdTC2RecordID.Value);
        hdUploadedFlag.Value = "0";
        hdTC2ToView.Value = "0";
        hdFileTC2ToUpload.Value = "1";

        pnlViewTC2.Visible = false;
        pnlUploadTC2ToUpload.Visible = true;
        imgBtnUndofileTC2ToUpload.Visible = true;

        ddlTC2TeamToUpload.Enabled = true;
        //txtTC2RemarksToUpload.Enabled = true;
        txtTC2UploadedRemarks.Enabled = true;
        txtTC2IdentificationReferenceNo.Enabled = true;

        lblAttachOrViewTxtTC2.Text = "Attach ";

        mpeDetail.Show();
    }

    protected void imgBtnRemoveTC3ToView_Click(object sender, ImageClickEventArgs e)
    {
        hdRemovedTC3ID.Value = Convert.ToString(hdTC3RecordID.Value);
        hdUploadedFlag.Value = "0";
        hdTC3ToView.Value = "0";
        hdFileTC3ToUpload.Value = "1";

        pnlViewTC3.Visible = false;
        pnlUploadTC3ToUpload.Visible = true;
        imgBtnUndofileTC3ToUpload.Visible = true;

        ddlTC3TeamToUpload.Enabled = true;
        //txtTC3RemarksToUpload.Enabled = true;
        txtTC3UploadedRemarks.Enabled = true;
        txtTC3IdentificationReferenceNo.Enabled = true;

        lblAttachOrViewTxtTC3.Text = "Attach ";

        mpeDetail.Show();
    }



    protected void imgBtnUndofileTC1ToUpload_Click(object sender, ImageClickEventArgs e)
    {
        hdRemovedTC1ID.Value = "0";
        hdUploadedFlag.Value = "0";
        hdTC1ToView.Value = "1";
        hdFileTC1ToUpload.Value = "0";

        pnlViewTC1.Visible = true;
        pnlUploadTC1ToUpload.Visible = false;



        ddlTC1TeamToUpload.SelectedValue = Convert.ToString(ViewState["TC_ASSIGNED_TO_ID1"]);
        txtTC1RemarksToUpload.Text = Convert.ToString(ViewState["TC_REMARKS1"]);
        txtTC1IdentificationReferenceNo.Text = Convert.ToString(ViewState["IDENTIFICATION_REFERENCE_NO1"]);

        ddlTC1TeamToUpload.Enabled = false;
        //txtTC1RemarksToUpload.Enabled = false;
        txtTC1UploadedRemarks.Enabled = false;
        txtTC1IdentificationReferenceNo.Enabled = false;

        lblAttachOrViewTxtTC1.Text = "View ";

        mpeDetail.Show();
    }

    protected void imgBtnUndofileTC2ToUpload_Click(object sender, ImageClickEventArgs e)
    {
        hdRemovedTC2ID.Value = "0";
        hdUploadedFlag.Value = "0";
        hdTC2ToView.Value = "1";
        hdFileTC2ToUpload.Value = "0";

        pnlViewTC2.Visible = true;
        pnlUploadTC2ToUpload.Visible = false;

        ddlTC2TeamToUpload.SelectedValue = Convert.ToString(ViewState["TC_ASSIGNED_TO_ID2"]);
        txtTC2RemarksToUpload.Text = Convert.ToString(ViewState["TC_REMARKS2"]);
        txtTC2IdentificationReferenceNo.Text = Convert.ToString(ViewState["IDENTIFICATION_REFERENCE_NO2"]);

        ddlTC2TeamToUpload.Enabled = false;
        //txtTC2RemarksToUpload.Enabled = false;
        txtTC2UploadedRemarks.Enabled = false;
        txtTC2IdentificationReferenceNo.Enabled = false;

        lblAttachOrViewTxtTC2.Text = "View ";

        mpeDetail.Show();
    }

    protected void imgBtnUndofileTC3ToUpload_Click(object sender, ImageClickEventArgs e)
    {
        hdRemovedTC3ID.Value = "0";
        hdUploadedFlag.Value = "0";
        hdTC3ToView.Value = "1";
        hdFileTC3ToUpload.Value = "0";

        pnlViewTC3.Visible = true;
        pnlUploadTC3ToUpload.Visible = false;

        ddlTC3TeamToUpload.SelectedValue = Convert.ToString(ViewState["TC_ASSIGNED_TO_ID3"]);
        txtTC3RemarksToUpload.Text = Convert.ToString(ViewState["TC_REMARKS3"]);
        txtTC3IdentificationReferenceNo.Text = Convert.ToString(ViewState["IDENTIFICATION_REFERENCE_NO3"]);

        ddlTC3TeamToUpload.Enabled = false;
        //txtTC3RemarksToUpload.Enabled = false;
        txtTC3UploadedRemarks.Enabled = false;
        txtTC3IdentificationReferenceNo.Enabled = false;

        lblAttachOrViewTxtTC3.Text = "View ";

        mpeDetail.Show();
    }




    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdUploadConfirmValue.Value) > 0)
        {
            UploadTC();
        }
    }

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdUpdateStatausConfirmValue.Value) > 0)
        {
            UpdateTCStatus();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTCList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtPOList"];
            ExportToExcel(dt);
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        if (gvTCList.Rows.Count > 0)
        {
            DownloadTCInZip();
        }
    }

    #endregion


    #region METHODS[=========================]

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTeams()
    {
        try
        {
            dsTeams = objTC.GetAssingedTo(0);
            if (dsTeams.Tables.Count > 0 && dsTeams.Tables[0].Rows.Count > 0)
            {
                ddlTeam.DataSource = dsTeams.Tables[0];
                ddlTeam.DataTextField = "TC_DEPARTMENT";
                ddlTeam.DataValueField = "TC_DEPARTMENT_ID";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, "Select");
                ddlTeam.SelectedIndex = 0;

                //int count = 0;
                //foreach (DataRow dr in dsTeams.Tables[0].Select("EMP_RECORD_ID='" + Convert.ToString(Session["EMP_RECORD_ID"]) + "'"))
                //{
                //    count++;
                //}
                //if (count > 0)
                //{
                //    ddlEmployee.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                //}
                //else
                //{
                //    ddlEmployee.SelectedIndex = 0;
                //}

                ddlTeam.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTeamToUpload(int unitID)
    {
        try
        {
            //unitID = Convert.ToInt32(ViewState["UNIT_ID"]);
            dsTeams = objTC.GetAssingedTo(unitID);

            if (dsTeams.Tables.Count > 0 && dsTeams.Tables[0].Rows.Count > 0)
            {
                ddlTC1TeamToUpload.DataSource = dsTeams.Tables[0];
                ddlTC1TeamToUpload.DataTextField = "TC_DEPARTMENT";
                ddlTC1TeamToUpload.DataValueField = "TC_DEPARTMENT_ID";
                ddlTC1TeamToUpload.DataBind();
                ddlTC1TeamToUpload.Items.Insert(0, "Select");
                ddlTC1TeamToUpload.SelectedIndex = 0;


                ddlTC2TeamToUpload.DataSource = dsTeams.Tables[0];
                ddlTC2TeamToUpload.DataTextField = "TC_DEPARTMENT";
                ddlTC2TeamToUpload.DataValueField = "TC_DEPARTMENT_ID";
                ddlTC2TeamToUpload.DataBind();
                ddlTC2TeamToUpload.Items.Insert(0, "Select");
                ddlTC2TeamToUpload.SelectedIndex = 0;


                ddlTC3TeamToUpload.DataSource = dsTeams.Tables[0];
                ddlTC3TeamToUpload.DataTextField = "TC_DEPARTMENT";
                ddlTC3TeamToUpload.DataValueField = "TC_DEPARTMENT_ID";
                ddlTC3TeamToUpload.DataBind();
                ddlTC3TeamToUpload.Items.Insert(0, "Select");
                ddlTC3TeamToUpload.SelectedIndex = 0;

            }
            else
            {
                ddlTC1TeamToUpload.Items.Clear();
                ddlTC1TeamToUpload.Items.Insert(0, "Select");
                ddlTC1TeamToUpload.SelectedIndex = 0;

                ddlTC2TeamToUpload.Items.Clear();
                ddlTC2TeamToUpload.Items.Insert(0, "Select");
                ddlTC2TeamToUpload.SelectedIndex = 0;

                ddlTC3TeamToUpload.Items.Clear();
                ddlTC3TeamToUpload.Items.Insert(0, "Select");
                ddlTC3TeamToUpload.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatus()
    {
        try
        {
            dsStatus = objTC.GetTCStatusList(0);
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatusToUpdate()
    {
        try
        {
            dsStatus = objTC.GetTCStatusList(1);
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatusTC1.DataSource = dsStatus.Tables[0];
                ddlStatusTC1.DataTextField = "STATUS_NAME";
                ddlStatusTC1.DataValueField = "STATUS_ID";
                ddlStatusTC1.DataBind();
                ddlStatusTC1.Items.Insert(0, "Select Status");
                ddlStatusTC1.SelectedIndex = 0;

                ddlStatusTC2.DataSource = dsStatus.Tables[0];
                ddlStatusTC2.DataTextField = "STATUS_NAME";
                ddlStatusTC2.DataValueField = "STATUS_ID";
                ddlStatusTC2.DataBind();
                ddlStatusTC2.Items.Insert(0, "Select Status");
                ddlStatusTC2.SelectedIndex = 0;

                ddlStatusTC3.DataSource = dsStatus.Tables[0];
                ddlStatusTC3.DataTextField = "STATUS_NAME";
                ddlStatusTC3.DataValueField = "STATUS_ID";
                ddlStatusTC3.DataBind();
                ddlStatusTC3.Items.Insert(0, "Select Status");
                ddlStatusTC3.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetPOList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusID = 0;

            if (!string.IsNullOrEmpty(txtPONo.Text))
                poNo = txtPONo.Text.ToUpper();
            else
                poNo = string.Empty;

            if (!string.IsNullOrEmpty(txtVendorName.Text))
                vendorName = txtVendorName.Text;
            else
                vendorName = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text.ToUpper();
            else
                JOBNo = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);
            else
                unitID = 0;

            if (!string.IsNullOrEmpty(txtItemCode.Text))
                itemCode = txtItemCode.Text;
            else
                itemCode = string.Empty;

            if (!string.IsNullOrEmpty(txtItemName.Text))
                itemName = txtItemName.Text;
            else
                itemName = string.Empty;

            if (ddlTeam.SelectedIndex > 0)
                teamID = Convert.ToInt32(ddlTeam.SelectedValue);
            else
                teamID = 0;

            if (!string.IsNullOrEmpty(txtIdentificationRefrernceNo.Text))
                identificationReferenceNo = txtIdentificationRefrernceNo.Text;
            else
                identificationReferenceNo = string.Empty;

            dsPOList = objTC.GetPOTCList(fromDate, toDate, statusID, poNo, vendorName, unitID, JOBNo, itemCode, itemName, teamID, identificationReferenceNo);

            if (dsPOList.Tables.Count > 0 && dsPOList.Tables[0].Rows.Count > 0)
            {
                Session["dtPOList"] = dsPOList.Tables[0];
                Session["dtTcCheckers"] = dsPOList.Tables[1];
                gvTCList.DataSource = dsPOList.Tables[0];
                gvTCList.DataBind();
            }
            else
            {
                Session["dtPOList"] = null;
                Session["dtTcCheckers"] = null;
                gvTCList.DataSource = null;
                gvTCList.DataBind();
            }
            lblRecords.Text = "Records[" + dsPOList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateTCStatus()
    {
        try
        {
            poNo = string.Empty;
            poRecordID = 0;

            poTC1RecordID = 0;
            poTC2RecordID = 0;
            poTC3RecordID = 0;

            poTC1StatusID = 0;
            poTC2StatusID = 0;
            poTC3StatusID = 0;

            TC1remarks = string.Empty;
            TC2remarks = string.Empty;
            TC3remarks = string.Empty;

            string poTcRecordIDs = string.Empty;

            poNo = txtPONoToUpload.Text;

            if (Convert.ToInt32(ViewState["RECORD_ID"]) > 0)
                poRecordID = Convert.ToInt32(ViewState["RECORD_ID"]);


            if (ddlStatusTC1.SelectedIndex > 0 && ddlStatusTC1.Enabled == true)
            {
                poTC1RecordID = Convert.ToInt32(hdTC1RecordID.Value);
                poTC1StatusID = Convert.ToInt32(ddlStatusTC1.SelectedValue);
                TC1remarks = txtTC1RemarksToUpload.Text;

                poTcRecordIDs += poTC1RecordID + ",";
            }

            if (ddlStatusTC2.SelectedIndex > 0 && ddlStatusTC2.Enabled == true)
            {
                poTC2RecordID = Convert.ToInt32(hdTC2RecordID.Value);
                poTC2StatusID = Convert.ToInt32(ddlStatusTC2.SelectedValue);
                TC2remarks = txtTC2RemarksToUpload.Text;
                poTcRecordIDs += poTC2RecordID + ",";
            }

            if (ddlStatusTC3.SelectedIndex > 0 && ddlStatusTC3.Enabled == true)
            {
                poTC3RecordID = Convert.ToInt32(hdTC3RecordID.Value);
                poTC3StatusID = Convert.ToInt32(ddlStatusTC3.SelectedValue);
                TC3remarks = txtTC3RemarksToUpload.Text;
                poTcRecordIDs += poTC3RecordID + ",";
            }

            if (!string.IsNullOrEmpty(poTcRecordIDs))
                poTcRecordIDs = poTcRecordIDs.TrimEnd(',');

            int value = 1;
            value = objTC.UpdateTCStatus(poRecordID,
                                            poTC1RecordID, poTC1StatusID, TC1remarks,
                                            poTC2RecordID, poTC2StatusID, TC2remarks,
                                            poTC3RecordID, poTC3StatusID, TC3remarks,
                                            Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                int sendMailValue = objTCSendMail.ProcessAndSendMail(poRecordID, poTcRecordIDs);
                if (sendMailValue > 0)
                {
                    //int mailStatusUpdateValue = objTC.UpdateTCMailStatus(poRecordID, poTcRecordIDs, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    SuccessMessage("Status updated of TC(s) for PO No. '" + poNo + "' and mail sent successfully...!!!");
                }
                else
                {
                    SuccessMessage("Status updated of TC(s) for PO No. '" + poNo + "' successfully...!!!");
                }

                GetPOList();
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

    private void UploadTC()
    {
        try
        {

            int isCompleted = 0;
            string isCompletedRemarks = string.Empty;

            DataTable dtDocs = new DataTable();
            dtDocs.Columns.Add("FILE_NAME", typeof(string));
            dtDocs.Columns.Add("FILE_BYTES", typeof(byte[]));
            dtDocs.Columns.Add("REMARKS", typeof(string));
            dtDocs.Columns.Add("ASSIGNED_TO_ID", typeof(int));
            dtDocs.Columns.Add("TC_SEQ_NO", typeof(int));
            dtDocs.Columns.Add("IDENTIFICATION_REFERENCE_NO", typeof(string));

            poRecordID = 0;
            poNo = string.Empty;
            poDate = string.Empty;
            vendorCode = string.Empty;
            vendorName = string.Empty;
            JOBNo = string.Empty;
            unitID = 0;
            itemCode = string.Empty;
            itemName = string.Empty;
            isCompleted = 0;
            isCompletedRemarks = string.Empty;

            poTC1RecordID = 0;
            poTC2RecordID = 0;
            poTC3RecordID = 0;

            TC1remarks = string.Empty;
            TC2remarks = string.Empty;
            TC3remarks = string.Empty;

            TC1TeamRecordID = 0;
            TC2TeamRecordID = 0;
            TC3TeamRecordID = 0;

            TC1FileBytes = null;
            TC2FileBytes = null;
            TC3FileBytes = null;

            TC1File = string.Empty;
            TC2File = string.Empty;
            TC3File = string.Empty;

            TC1SeqNo = 0;
            TC2SeqNo = 0;
            TC3SeqNo = 0;

            removedTC1ID = 0;
            removedTC2ID = 0;
            removedTC3ID = 0;

            TC1IdentificationReferenceNo = string.Empty;
            TC2IdentificationReferenceNo = string.Empty;
            TC3IdentificationReferenceNo = string.Empty;

            if (Convert.ToInt32(ViewState["RECORD_ID"]) > 0)
                poRecordID = Convert.ToInt32(ViewState["RECORD_ID"]);

            if (!string.IsNullOrEmpty(txtPONoToUpload.Text))
                poNo = txtPONoToUpload.Text.ToUpper();

            if (!string.IsNullOrEmpty(txtPODateToUpload.Text))
                poDate = Convert.ToDateTime(txtPODateToUpload.Text).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtVendorCodeToUpload.Text))
                vendorCode = txtVendorCodeToUpload.Text.ToUpper();

            if (!string.IsNullOrEmpty(txtVendorNameToUpload.Text))
                vendorName = txtVendorNameToUpload.Text;

            if (!string.IsNullOrEmpty(txtJOBNoToUpload.Text))
                JOBNo = txtJOBNoToUpload.Text.ToUpper();

            if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0)
                unitID = Convert.ToInt32(ViewState["UNIT_ID"]);

            if (!string.IsNullOrEmpty(txtItemCodeToUpload.Text))
                itemCode = txtItemCodeToUpload.Text.ToUpper();

            if (!string.IsNullOrEmpty(txtItemNameToUpload.Text))
                itemName = txtItemNameToUpload.Text.ToUpper();



            if (chkIsCompleted.Checked)
            {
                isCompleted = 1;

                if (!string.IsNullOrEmpty(txtIscompletedRemarks.Text))
                    isCompletedRemarks = txtIscompletedRemarks.Text;
            }



            for (int i = 1; i <= 3; i++)
            {
                if (i == 1)
                {
                    DataRow dr1 = dtDocs.NewRow();

                    if (fileTC1ToUpload.HasFile)
                    {
                        removedTC1ID = 0;

                        if (!string.IsNullOrEmpty(fileTC1ToUpload.PostedFile.FileName))
                        {
                            string[] str = fileTC1ToUpload.PostedFile.FileName.Split('\\');
                            int length = str.Length;
                            TC1File = str[str.Length - 1];
                            TC1FileBytes = GetFileBytes(fileTC1ToUpload.PostedFile.FileName, fileTC1ToUpload.PostedFile.InputStream);
                        }

                        if (!string.IsNullOrEmpty(txtTC1UploadedRemarks.Text))
                            TC1remarks = txtTC1UploadedRemarks.Text;

                        if (Convert.ToInt32(ddlTC1TeamToUpload.SelectedIndex) > 0)
                            TC1TeamRecordID = Convert.ToInt32(ddlTC1TeamToUpload.SelectedValue);

                        if (!string.IsNullOrEmpty(Convert.ToString(txtTC1IdentificationReferenceNo.Text)))
                            TC1IdentificationReferenceNo = Convert.ToString(txtTC1IdentificationReferenceNo.Text).TrimEnd(',').TrimEnd(' ');


                        if (poRecordID == 0)
                        {
                            if (TC1FileBytes != null)
                            {
                                dr1["FILE_NAME"] = TC1File;
                                dr1["FILE_BYTES"] = TC1FileBytes;
                                dr1["REMARKS"] = TC1remarks;
                                dr1["ASSIGNED_TO_ID"] = TC1TeamRecordID;
                                dr1["TC_SEQ_NO"] = i;
                                dr1["IDENTIFICATION_REFERENCE_NO"] = TC1IdentificationReferenceNo;

                                dtDocs.Rows.Add(dr1);
                            }
                        }
                        else
                        {
                            poTC1RecordID = Convert.ToInt32(ViewState["TC_RECORD_ID1"]);
                            TC1SeqNo = i;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(hdRemovedTC1ID.Value) > 0)
                        {
                            removedTC1ID = Convert.ToInt32(hdRemovedTC1ID.Value);
                        }
                        else
                        {

                            if (poRecordID > 0)
                            {
                                poTC1RecordID = Convert.ToInt32(hdTC1RecordID.Value);
                                TC1SeqNo = i;
                            }

                            if (!string.IsNullOrEmpty(txtTC1UploadedRemarks.Text))
                                TC1remarks = txtTC1UploadedRemarks.Text;

                            if (Convert.ToInt32(ddlTC1TeamToUpload.SelectedIndex) > 0)
                                TC1TeamRecordID = Convert.ToInt32(ddlTC1TeamToUpload.SelectedValue);

                            if (!string.IsNullOrEmpty(Convert.ToString(txtTC1IdentificationReferenceNo.Text)))
                                TC1IdentificationReferenceNo = Convert.ToString(txtTC1IdentificationReferenceNo.Text).TrimEnd(',').TrimEnd(' ');
                        }
                    }
                }

                else if (i == 2)
                {
                    DataRow dr2 = dtDocs.NewRow();

                    if (fileTC2ToUpload.HasFile)
                    {
                        removedTC2ID = 0;

                        if (!string.IsNullOrEmpty(fileTC2ToUpload.PostedFile.FileName))
                        {
                            string[] str = fileTC2ToUpload.PostedFile.FileName.Split('\\');
                            int length = str.Length;
                            TC2File = str[str.Length - 1];
                            TC2FileBytes = GetFileBytes(fileTC2ToUpload.PostedFile.FileName, fileTC2ToUpload.PostedFile.InputStream);
                        }

                        if (!string.IsNullOrEmpty(txtTC2UploadedRemarks.Text))
                            TC2remarks = txtTC2UploadedRemarks.Text;

                        if (Convert.ToInt32(ddlTC2TeamToUpload.SelectedIndex) > 0)
                            TC2TeamRecordID = Convert.ToInt32(ddlTC2TeamToUpload.SelectedValue);

                        if (!string.IsNullOrEmpty(Convert.ToString(txtTC2IdentificationReferenceNo.Text)))
                            TC2IdentificationReferenceNo = Convert.ToString(txtTC2IdentificationReferenceNo.Text).TrimEnd(',').TrimEnd(' ');

                        if (poRecordID == 0)
                        {
                            if (TC2FileBytes != null)
                            {
                                dr2["FILE_NAME"] = TC2File;
                                dr2["FILE_BYTES"] = TC2FileBytes;
                                dr2["REMARKS"] = TC2remarks;
                                dr2["ASSIGNED_TO_ID"] = TC2TeamRecordID;
                                dr2["TC_SEQ_NO"] = i;
                                dr2["IDENTIFICATION_REFERENCE_NO"] = TC2IdentificationReferenceNo;

                                dtDocs.Rows.Add(dr2);
                            }
                        }
                        else
                        {
                            poTC2RecordID = Convert.ToInt32(ViewState["TC_RECORD_ID2"]);
                            TC2SeqNo = i;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(hdRemovedTC2ID.Value) > 0)
                        {
                            removedTC2ID = Convert.ToInt32(hdRemovedTC2ID.Value);
                        }
                        else
                        {
                            if (poRecordID > 0)
                            {
                                poTC2RecordID = Convert.ToInt32(hdTC2RecordID.Value);
                                TC2SeqNo = i;
                            }

                            if (!string.IsNullOrEmpty(txtTC2UploadedRemarks.Text))
                                TC2remarks = txtTC2UploadedRemarks.Text;

                            if (Convert.ToInt32(ddlTC2TeamToUpload.SelectedIndex) > 0)
                                TC2TeamRecordID = Convert.ToInt32(ddlTC2TeamToUpload.SelectedValue);

                            if (!string.IsNullOrEmpty(Convert.ToString(txtTC2IdentificationReferenceNo.Text)))
                                TC2IdentificationReferenceNo = Convert.ToString(txtTC2IdentificationReferenceNo.Text).TrimEnd(',').TrimEnd(' ');
                        }
                    }
                }

                else if (i == 3)
                {
                    DataRow dr3 = dtDocs.NewRow();

                    if (fileTC3ToUpload.HasFile)
                    {
                        removedTC3ID = 0;

                        if (!string.IsNullOrEmpty(fileTC3ToUpload.PostedFile.FileName))
                        {
                            string[] str = fileTC3ToUpload.PostedFile.FileName.Split('\\');
                            int length = str.Length;
                            TC3File = str[str.Length - 1];
                            TC3FileBytes = GetFileBytes(fileTC3ToUpload.PostedFile.FileName, fileTC3ToUpload.PostedFile.InputStream);
                        }


                        if (!string.IsNullOrEmpty(txtTC3UploadedRemarks.Text))
                            TC3remarks = txtTC3UploadedRemarks.Text;

                        if (Convert.ToInt32(ddlTC3TeamToUpload.SelectedIndex) > 0)
                            TC3TeamRecordID = Convert.ToInt32(ddlTC3TeamToUpload.SelectedValue);

                        if (!string.IsNullOrEmpty(Convert.ToString(txtTC3IdentificationReferenceNo.Text)))
                            TC3IdentificationReferenceNo = Convert.ToString(txtTC3IdentificationReferenceNo.Text).TrimEnd(',').TrimEnd(' ');


                        if (poRecordID == 0)
                        {
                            if (TC3FileBytes != null)
                            {
                                dr3["FILE_NAME"] = TC3File;
                                dr3["FILE_BYTES"] = TC3FileBytes;
                                dr3["REMARKS"] = TC3remarks;
                                dr3["ASSIGNED_TO_ID"] = TC3TeamRecordID;
                                dr3["TC_SEQ_NO"] = i;
                                dr3["IDENTIFICATION_REFERENCE_NO"] = TC3IdentificationReferenceNo;

                                dtDocs.Rows.Add(dr3);
                            }
                        }
                        else
                        {
                            poTC3RecordID = Convert.ToInt32(ViewState["TC_RECORD_ID3"]);
                            TC3SeqNo = i;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(hdRemovedTC3ID.Value) > 0)
                        {
                            removedTC3ID = Convert.ToInt32(hdRemovedTC3ID.Value);
                        }
                        else
                        {
                            if (poRecordID > 0)
                            {
                                poTC3RecordID = Convert.ToInt32(hdTC3RecordID.Value);
                                TC3SeqNo = i;
                            }

                            if (!string.IsNullOrEmpty(txtTC3UploadedRemarks.Text))
                                TC3remarks = txtTC3UploadedRemarks.Text;

                            if (Convert.ToInt32(ddlTC3TeamToUpload.SelectedIndex) > 0)
                                TC3TeamRecordID = Convert.ToInt32(ddlTC3TeamToUpload.SelectedValue);

                            if (!string.IsNullOrEmpty(Convert.ToString(txtTC3IdentificationReferenceNo.Text)))
                                TC3IdentificationReferenceNo = Convert.ToString(txtTC3IdentificationReferenceNo.Text).TrimEnd(',').TrimEnd(' ');
                        }
                    }
                }
            }
            int value = 0;

            if (poRecordID > 0)
            {
                value = objTC.UploadNewTCToExistingPO(poRecordID,
                                       poTC1RecordID, TC1File, TC1FileBytes, TC1remarks, TC1TeamRecordID, TC1SeqNo, removedTC1ID, TC1IdentificationReferenceNo,
                                       poTC2RecordID, TC2File, TC2FileBytes, TC2remarks, TC2TeamRecordID, TC2SeqNo, removedTC2ID, TC2IdentificationReferenceNo,
                                       poTC3RecordID, TC3File, TC3FileBytes, TC3remarks, TC3TeamRecordID, TC3SeqNo, removedTC3ID, TC3IdentificationReferenceNo,
                                       isCompleted, isCompletedRemarks,
                                       Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }
            else
            {
                value = objTC.UploadNewTCToNewPO(poNo, poDate, vendorCode, vendorName, JOBNo, unitID, itemCode, itemName, dtDocs, isCompleted, isCompletedRemarks,
                                                 Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }

            if (value > 0)
            {
                SuccessMessage("TC uploaded for PO '" + poNo + "' successfully...!!!");
                GetPOList();
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


    private void ViewDrawingFiles(int poRecordID, string fileType, string fileName)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                ExportTCFile(poRecordID, fileType);
            }
            else
            {
                ExceptionMessage("File Doesn't exist!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportTCFile(int poRecordID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();

            DataTable dt = new DataTable();
            if (Session["dtPOList"] != null)
                dt = (DataTable)Session["dtPOList"];
            else
            {
                dsFiles = objTC.GetLOTDrawingFiles(poRecordID);

                if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                {
                    dt = dsFiles.Tables[0];
                }
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Select("PO_RECORD_ID='" + poRecordID + "'"))
                {
                    if (fileType == Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC1))
                    {
                        bytes = (byte[])dr["TC_ATTACHMENT_DOC1"];
                        fileName = Convert.ToString(dr["TC_ATTACHMENT_NAME1"]);
                    }
                    else if (fileType == Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC2))
                    {
                        bytes = (byte[])dr["TC_ATTACHMENT_DOC2"];
                        fileName = Convert.ToString(dr["TC_ATTACHMENT_NAME2"]);
                    }
                    else if (fileType == Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC3))
                    {
                        bytes = (byte[])dr["TC_ATTACHMENT_DOC3"];
                        fileName = Convert.ToString(dr["TC_ATTACHMENT_NAME3"]);
                    }
                }

                if (bytes != null)
                {
                    string[] stringParts = fileName.Split(new char[] { '.' });
                    string strType = stringParts[1];
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName.Replace(",", " &"));
                    Response.ContentType = strType;
                    Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.End();
                }
            }

        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("Timeout expired"))
            {
                ExceptionMessage("The process of downloading is too longer, please try again...!!!");
                return;
            }
            else
            {
                ExceptionMessage(ex.ToString());
                return;
            }
        }
    }

    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count - 1; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;


                    rowTxt = rowTxt.Replace(',', ' ');
                    rowTxt = rowTxt.TrimEnd('\r', ' ');
                    rowTxt = rowTxt.TrimEnd('\n', ' ');
                    rowTxt = rowTxt.Replace('\r', ' ');
                    rowTxt = rowTxt.Replace('\n', ' ');

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "Procurement_Status-PO_List_For_TC_Upload_" + DateTime.Now.ToString("dd_MMM_yyyy");
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






    private byte[] GetBytes(int poRecordID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();

            DataTable dt = new DataTable();
            if (Session["dtPOList"] != null)
                dt = (DataTable)Session["dtPOList"];
            else
            {
                dsFiles = objTC.GetLOTDrawingFiles(poRecordID);

                if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                {
                    dt = dsFiles.Tables[0];
                }
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Select("PO_RECORD_ID='" + poRecordID + "'"))
                {
                    if (fileType == Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC1))
                    {
                        bytes = (byte[])dr["TC_ATTACHMENT_DOC1"];
                        fileName = Convert.ToString(dr["TC_ATTACHMENT_NAME1"]);
                    }
                    else if (fileType == Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC2))
                    {
                        bytes = (byte[])dr["TC_ATTACHMENT_DOC2"];
                        fileName = Convert.ToString(dr["TC_ATTACHMENT_NAME2"]);
                    }
                    else if (fileType == Convert.ToString(TCAllStatusAndTypes.EnumTCType.TC3))
                    {
                        bytes = (byte[])dr["TC_ATTACHMENT_DOC3"];
                        fileName = Convert.ToString(dr["TC_ATTACHMENT_NAME3"]);
                    }
                }
            }
            return bytes;
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    private void DownloadTCInZip()
    {
        try
        {

            string zipFileName = "MyZipfiles.zip";
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", zipFileName);
            //byte[] buffer = GetBytes(1, "TC2");
            byte[] buffer = new byte[4096];
            ZipOutputStream zipOutputStream = new ZipOutputStream(Response.OutputStream);

            zipOutputStream.SetLevel(3);
            try
            {
                DirectoryInfo DI = new DirectoryInfo(Server.MapPath("/DownloadFolder"));
                foreach (var i in DI.GetFiles())
                {
                    Stream fs = File.OpenRead(i.FullName);
                    ZipEntry zipEntry = new ZipEntry(ZipEntry.CleanName(i.Name));
                    zipEntry.Size = fs.Length;
                    zipOutputStream.PutNextEntry(zipEntry);
                    int count = fs.Read(buffer, 0, buffer.Length);

                    while (count > 0)
                    {
                        zipOutputStream.Write(buffer, 0, count);
                        count = fs.Read(buffer, 0, buffer.Length);
                        if (!Response.IsClientConnected)
                        {
                            break;
                        }
                        Response.Flush();
                    }
                    fs.Close();
                }
                zipOutputStream.Close();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

                throw;
            }

            //foreach (GridViewRow gr in gvTCList.Rows)
            //{
            //    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
            //    if (chkSelect.Checked)
            //    {

            //    }
            //}
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion    
}
