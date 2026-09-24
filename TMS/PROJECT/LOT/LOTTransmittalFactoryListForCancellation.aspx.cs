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
using System.Text;
using iTextSharp.tool.xml;

using System.Net.Mime;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;
using System.Xml;
using iTextSharp.tool.xml.css;
using System.Management;
//using Spire.Pdf;
using System.Drawing.Printing;
using System.Web.Services;

public partial class PROJECT_LOT_LOTTransmittalFactoryListForCancellation : System.Web.UI.Page
{

    #region VARIABLES[=========================]

    LOTSendMailForCancelledLOT objLOTSendMailForCancelledLOT = new LOTSendMailForCancelledLOT();
    LOTPrintDocument objLOTPrintDocument = new LOTPrintDocument();
    LOTSendMailToPrint objLOTSendMailToPrint = new LOTSendMailToPrint();
    GetLOTMailTypeAndStatus objGetLOTMailTypeAndStatus = new GetLOTMailTypeAndStatus();
    LOTMailTypeAndStatusProperties objLOTMailTypeAndStatusProperties = new LOTMailTypeAndStatusProperties();
    GetLOTApproverStatus objGetLOTApproverStatus = new GetLOTApproverStatus();

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsSubitemsSI = new DataSet();
    DataSet dsSubitems = new DataSet();
    DataSet dsLOTMainSubItems = new DataSet();
    DataSet dsLOTList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsLOTfor = new DataSet();
    DataSet dsLOTStatus = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string LOTTFNo = string.Empty;
    int statusID = 0;
    int unitID = 0;
    int LOTMainItemID = 0;
    int LOTMainSubitemID = 0;
    string LOTMainSubitemIDs = string.Empty;
    int companyID = 0;
    int LOTTFID = 0;
    string jobNo = string.Empty;
    string customerName = string.Empty;
    string TFNo = string.Empty;
    string remarks = string.Empty;
    int isUpdateOrAmend = 0;
    int isAmend = 0;
    int newStatusID = 0;
    int createdByID = 0;
    int productionManagerID = 0;
    int amendmentByID = 0;
    int prodAmendmentByID = 0;
    int amendedByID = 0;
    int PEID = 0;
    int PMID = 0;
    string productionOrderDate = string.Empty;
    string productCode = string.Empty;
    string productDesc = string.Empty;
    string UOM = string.Empty;
    string isPartOfProductionStatus = string.Empty;
    int isPartOfProductionStatusID = 0;
    int approvedByID = 0;
    int prodAcceptedByID = 0;
    int sentToIntlInspectionByID = 0;
    int sentToFinalInspectionByID = 0;
    int qaIntlInspAcceptedByID = 0;
    int qaIntlInspNotAcceptedByID = 0;
    int completedByID = 0;
    int sentToReworkByID = 0;
    int amendmentCount = 0;
    int sentToamendmentByID = 0;
    int amendedApprovedByID = 0;
    int amendedAcceptedByID = 0;
    int qualityAcceptedByID = 0;
    int planningAcceptedByID = 0;
    int forwardedByID = 0;
    int amendedQualityAcceptedByID = 0;
    int amendedPlanningAcceptedByID = 0;
    int amendedForwardedByID = 0;
    int nextStatusID = 0;
    int currentStatusID = 0;
    DataTable dtSubitem = new DataTable();
    DataSet dsLOTTFDetails = new DataSet();
    DataSet dsLOTCategoryForFactory = new DataSet();
    DataSet dsProductionOrderNo = new DataSet();
    int revisionNo = 0;
    string categoryID = string.Empty;
    string category = string.Empty;
    int insertSubitemFlag = 0;
    string updateSubitemQuery = string.Empty;
    string custCode = string.Empty;
    string poNo = string.Empty;
    string LOTDate = string.Empty;
    string itemName = string.Empty;
    string LOTMainItem = string.Empty;
    string impNotes = string.Empty;
    int PEApproverID = 0;
    int PMApproverID = 0;
    DataTable dtTemp = new DataTable();
    DataTable dtTempAttachments = new DataTable();
    DataTable dtAttachments = new DataTable();
    DataTable dtSubitemToAdd = new DataTable();
    DataSet dsLOTMainItems = new DataSet();
    DataSet dsJobApprovers = new DataSet();
    Byte[] attachment1FileBytes = null;
    Byte[] attachment2FileBytes = null;
    Byte[] attachment3FileBytes = null;
    Byte[] attachment4FileBytes = null;
    string attachment1File = string.Empty;
    string attachment2File = string.Empty;
    string attachment3File = string.Empty;
    string attachment4File = string.Empty;
    Byte[] IRNAttachmentFileBytes = null;
    string IRNAttachmentFile = string.Empty;

    Byte[] standardDrawingFileBytes = null;
    string standardDrawingFile = string.Empty;
    string standardDrawingRemarks = string.Empty;
    int standardDrawingRemoveID = 0;

    string tableName = string.Empty;
    int LOTTFSubitemID = 0;
    string LOTTFSubitemIDs = string.Empty;
    string productionOrderNo = string.Empty;
    string expectedCompletionDate = string.Empty;
    string subitemDesc = string.Empty;
    string tagNo = string.Empty;
    string LOTMainSubItem = string.Empty;
    int LOTMainSubItemID = 0;
    string LOTMainSubItemIDs = string.Empty;
    string removedLOTTFSubItemIDs = string.Empty;
    string drgNo = string.Empty;
    int totalQuantity = 0;
    int quantity = 0;
    int actedQuantity = 0;
    int remainingQuantity = 0;
    int prodMngrID = 0;
    int acceptedByID = 0;
    int actedTotalQuantity = 0;

    int partialQuantityFlagOld = 0;
    int partialStatusIDOld = 0;
    int partialQuantityFlag = 0;
    int partialStatusID = 0;

    string SiDrawing1File = string.Empty;
    string SiDrawing2File = string.Empty;
    string SiDrawing3File = string.Empty;
    string SiDrawing4File = string.Empty;
    Byte[] SiDrawing1FileBytes = null;
    Byte[] SiDrawing2FileBytes = null;
    Byte[] SiDrawing3FileBytes = null;
    Byte[] SiDrawing4FileBytes = null;

    #endregion


    #region EVENTS[============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            HideUpdatePanel();
            if (!IsPostBack)
            {
                ViewState["ALL_BYTES"] = null;

                //txtTFNo.Text = "OPIGNU2021053-GNU-0001";

                ViewState["DOCS"] = "";
                hdConfirmValue.Value = "0";


                Session["dtQualityPersonList"] = null;
                Session["dsLOTList"] = null;
                Session["dsSubitemsSI"] = null;

                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;

                BindCompany();

                BindStatus();
                BindLOTFor();

                Session["dsApprovers"] = null;
                Session["dtProdMngr"] = null;

                GetLOTList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetLOTList();
    }

    protected void btnCancelledReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/CancelledLOTTransmittalFactoryReport.aspx");
    }

    

    protected void ddlLOTMainItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLOTMainItems.SelectedIndex > 0)
        {
            BindLOTMainSubItems(Convert.ToInt32(ddlLOTMainItems.SelectedValue));
        }
        else
        {
            ddlLOTMainSubitems.Items.Clear();
            ddlLOTMainSubitems.Items.Insert(0, "All");
            ddlLOTMainSubitems.SelectedIndex = 0;
        }
    }

    protected void gvLOTTFList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                ViewState["ALL_BYTES"] = null;
                currentStatusID = 0;
                createdByID = 0;
                productionManagerID = 0;
                LOTTFID = 0;
                companyID = 0;

                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewSubitemDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblLOTTFID = gvLOTTFList.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblTFNo = gvLOTTFList.Rows[rowindex].FindControl("lblTFNo") as Label;
                Label lblUnitID = gvLOTTFList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblLOTDate = gvLOTTFList.Rows[rowindex].FindControl("lblLOTDate") as Label;
                Label lblCustomerCode = gvLOTTFList.Rows[rowindex].FindControl("lblCustomerCode") as Label;
                Label lblCustomerName = gvLOTTFList.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblJOBNo = gvLOTTFList.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblProductionNo = gvLOTTFList.Rows[rowindex].FindControl("lblProductionNo") as Label;
                Label lblPONo = gvLOTTFList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblItemName = gvLOTTFList.Rows[rowindex].FindControl("lblItemName") as Label;
                Label lblImpNotes = gvLOTTFList.Rows[rowindex].FindControl("lblImpNotes") as Label;
                Label lblJobPEID = gvLOTTFList.Rows[rowindex].FindControl("lblJobPEID") as Label;
                Label lblJobPMID = gvLOTTFList.Rows[rowindex].FindControl("lblJobPMID") as Label;
                Label lblcreatedByID = gvLOTTFList.Rows[rowindex].FindControl("lblcreatedByID") as Label;




                Label lblAttachment1 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment2") as Label;
                Label lblAttachment3 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment3") as Label;
                Label lblAttachment4 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment4") as Label;

                Label lblStandardDrawing = gvLOTTFList.Rows[rowindex].FindControl("lblStandardDrawing") as Label;
                Label lblStandardDrawingRemarks = gvLOTTFList.Rows[rowindex].FindControl("lblStandardDrawingRemarks") as Label;

                string standardDrawingExtn = string.Empty;

                LOTTFID = Convert.ToInt32(lblLOTTFID.Text);
                companyID = Convert.ToInt32(lblUnitID.Text);


                txtTFNoSI.Text = lblTFNo.Text;
                txtJOBNoSI.Text = lblJOBNo.Text;


                ViewState["LOTTFID"] = LOTTFID;
                ViewState["UNIT_ID"] = Convert.ToInt32(lblUnitID.Text);
                ViewState["JOB_NO"] = Convert.ToString(lblJOBNo.Text);
                ViewState["TF_NO"] = Convert.ToString(lblTFNo.Text);

                ViewState["CREATED_BY_ID"] = Convert.ToInt32(lblcreatedByID.Text);
                ViewState["PE_ID"] = Convert.ToInt32(lblJobPEID.Text);
                ViewState["PM_ID"] = Convert.ToInt32(lblJobPMID.Text);


                if (Session["dsLOTList"] != null)
                    dsLOTList = (DataSet)Session["dsLOTList"];

                LOTMainSubitemIDs = string.Empty;
                LOTTFSubitemIDs = string.Empty;


                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                    ViewDrawingFiles(LOTTFID, "DRAWING1", Convert.ToString(lblAttachment1.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                    ViewDrawingFiles(LOTTFID, "DRAWING2", Convert.ToString(lblAttachment2.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                    ViewDrawingFiles(LOTTFID, "DRAWING3", Convert.ToString(lblAttachment3.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                    ViewDrawingFiles(LOTTFID, "DRAWING4", Convert.ToString(lblAttachment4.Text).Trim(), 0);


                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    Session["UpdateSubitemStatusClicked"] = false;
                    Session["MultipleClicked"] = false;

                    HideSubitemsPanel();
                    Reset();

                    BindLOTTFDetailsToEdit(LOTTFID);
                    mpeUpdateLOT.Show();
                }


                //Send missed mail
                #region MyRegion

                //else if (Convert.ToString(e.CommandArgument) == "SEND_APPROVAL_MAIL")
                //{
                //    int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                //    int partialStatusID = 0;
                //    int amendmentByProdFlag = 0;
                //    int qaIntlInspFlag = 0;

                //    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + LOTTFID + "'"))
                //    {
                //        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                //        {
                //            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                //            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                //            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                //            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                //            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                //            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                //            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                //            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                //            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);

                //            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);

                //            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                //            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                //            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                //            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                //            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                //            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                //            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                //            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                //            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                //            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                //            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                //            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                //            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                //            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                //            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                //            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);

                //            //New
                //            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                //                currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                //            {
                //                if (currentLoginID == createdByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                //                if (currentLoginID == amendedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                //            }



                //            //Amendment
                //            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                //            {
                //                if (currentLoginID == amendmentByID && currentLoginID != productionManagerID)
                //                {
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                //                    amendmentByProdFlag = 0;
                //                    if (dr1["AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr1["AMENDMENT_REMARKS"])))
                //                        remarks = Convert.ToString(dr1["AMENDMENT_REMARKS"]);
                //                }

                //                if (currentLoginID == amendmentByID && currentLoginID == productionManagerID)
                //                {
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                //                    amendmentByProdFlag = 1;
                //                    if (dr1["PROD_AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr1["PROD_AMENDMENT_REMARKS"])))
                //                        remarks = Convert.ToString(dr1["PROD_AMENDMENT_REMARKS"]);
                //                }
                //            }



                //            //Approved
                //            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                //                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))//acceptance
                //            {
                //                if (currentLoginID == approvedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                //                if (currentLoginID == amendedApprovedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                //            }



                //            //Planning Accepted
                //            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                //                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                //            {
                //                if (currentLoginID == planningAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                //                if (currentLoginID == amendedPlanningAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                //            }



                //            //Planning Forwarded
                //            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                //                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                //            {
                //                if (currentLoginID == forwardedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                //                if (currentLoginID == amendedForwardedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                //            }



                //            //Production-Accepted
                //            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                //                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                //            {
                //                if (currentLoginID == prodAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                //                if (currentLoginID == amendedAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                //            }



                //            //QA-LOT Accepted
                //            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                //                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                //            {
                //                if (currentLoginID == qualityAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                //                if (currentLoginID == amendedQualityAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                //                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                //            }

                //            //Complete
                //            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                //            {
                //                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                //                {
                //                    //Complete
                //                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_COMPLETED_MAIL_SENT='0'"))
                //                    {
                //                        if (currentLoginID == Convert.ToInt32(dr3["COMPLETED_BY"]))
                //                        {
                //                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";

                //                            if (dr3["IRN_ATTACHMENT_DOC"] != DBNull.Value)
                //                            {
                //                                IRNAttachmentFileBytes = (byte[])dr3["IRN_ATTACHMENT_DOC"];
                //                            }
                //                            else
                //                            {
                //                                IRNAttachmentFileBytes = null;
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }

                //    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                //        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                //    int sendMailValue = 0;
                //    //sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, lblTFNo.Text, companyID, remarks, qaIntlInspFlag, 0, IRNAttachmentFileBytes, partialQuantityFlag, amendmentByProdFlag);

                //    if (sendMailValue > 0)
                //    {
                //        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, currentStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                //        SuccessMessage("Mail sent successfully.");
                //        GetLOTList();
                //    }
                //}

                #endregion



                //View LOT in pdf
                else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    int count = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr0["PE_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                            Convert.ToInt32(dr0["PM_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                            Convert.ToInt32(dr0["CREATED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                        {
                            foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                            {
                                string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                                if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                                {
                                    LOTTFSubitemIDs += LOTTFSubitemID + ",";
                                }
                            }
                        }
                        else
                        {

                            foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))// AND IS_PART_OF_PRODUCTION_STATUS_REPORT_ID='1'
                            {
                                count++;

                                string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                                if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                                {
                                    LOTTFSubitemIDs += LOTTFSubitemID + ",";
                                }
                            }
                        }
                    }

                    if (count == 0)
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                        {
                            string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                            if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                            {
                                LOTTFSubitemIDs += LOTTFSubitemID + ",";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    ModalPopupExtender4.Show();
                    iframeViewTravelStatementInPDF.Attributes.Add("src", "LOTTransmittalFactoryInPDF.aspx?LOTTFID=" + LOTTFID + "&LOTTFSubitemIDs=" + LOTTFSubitemIDs + "&pdfType=" + (int)LOTAllStatusAndTypes.EnumPDFType.List);
                }


                else if (Convert.ToString(e.CommandArgument) == "ViewSubitemDETAIL")
                {
                    BindLOTSubitemDetails(LOTTFID, "", "");
                    mpeSubitemDetail.Show();
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

    protected void gvLOTTFList_RowDataBound(object sender, GridViewRowEventArgs e)
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
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                LOTMainSubitemIDs = string.Empty;
                PEID = 0;
                PMID = 0;
                companyID = 0;

                //1 get lot tf if
                int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                Label lblLOTTFID = (Label)e.Row.FindControl("lblLOTTFID");
                Label lblUnitID = (Label)e.Row.FindControl("lblUnitID");
                Label lblcreatedByID = (Label)e.Row.FindControl("lblcreatedByID");
                Label lblJobPEID = (Label)e.Row.FindControl("lblJobPEID");
                Label lblJobPMID = (Label)e.Row.FindControl("lblJobPMID");
                TextBox txtStatus = (TextBox)e.Row.FindControl("txtStatus");

                if (Session["dsLOTList"] != null)
                    dsLOTList = (DataSet)Session["dsLOTList"];


                #region STATUS

                int totalQuantity = 0;

                int newCount = 0;
                int approvedCount = 0;

                int planningAcceptedCount = 0;
                int planningForwardedCount = 0;

                int prodAcceptedCount = 0;
                int QALOTAcceptedCount = 0;
                int intlInspectionCount = 0;
                int QAItemAcceptedCount = 0;
                int QQAItemNotAcceptedCount = 0;

                int finalInspectionCount = 0;
                int reworkCount = 0;

                int completeCount = 0;
                int amendmentCount = 0;
                int amendedCount = 0;
                int amndApprovedCount = 0;

                int amndPlanningAcceptedCount = 0;
                int amndPlanningForwardedCount = 0;

                int amndProdAcceptedCount = 0;
                int amndQALOTAcceptedCount = 0;




                int intlInspectionCountC = 0;
                int QAItemAcceptedCountC = 0;
                int QQAItemNotAcceptedCountC = 0;
                int finalInspectionCountC = 0;
                int reworkCountC = 0;
                int completeCountC = 0;



                int IsCancellationMailSent = 0;

                statusID = 0;

                if (dsLOTList.Tables.Count > 0 && dsLOTList.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                        {
                            txtStatus.Text = "NEW,";
                            txtStatus.ToolTip = "New";
                        }
                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                        {
                            txtStatus.Text = "APP";
                            txtStatus.ToolTip = "Approved by PE/PM";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                        {
                            txtStatus.Text = "PLAC";
                            txtStatus.ToolTip = "Accepted by Planning";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                        {
                            txtStatus.Text = "FWD";
                            txtStatus.ToolTip = "Forwarded by Planning";
                        }


                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                        {
                            txtStatus.Text = "ACC,";
                            txtStatus.ToolTip = "Accepted by Production,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                        {
                            txtStatus.Text = "QACC,";
                            txtStatus.ToolTip = "LOT Accepted by Quality,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                        {
                            txtStatus.Text = "AMM,";
                            txtStatus.ToolTip = "In Amendment,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                        {
                            txtStatus.Text = "AMD,";
                            txtStatus.ToolTip = "Amended,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                        {
                            txtStatus.Text = "AMDAPP,";
                            txtStatus.ToolTip = "Amended Approved by PE/PM,\n";
                        }


                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                        {
                            txtStatus.Text = "AMDPLAC,";
                            txtStatus.ToolTip = "Amended Accepted By Planning,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                        {
                            txtStatus.Text = "AMDFWD,";
                            txtStatus.ToolTip = "Amended Forwarded By Planning,\n";
                        }


                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                        {
                            txtStatus.Text = "AMDACC,";
                            txtStatus.ToolTip = "Amended Accepted By Production,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                        {
                            txtStatus.Text = "AMDQACC,";
                            txtStatus.ToolTip = "Amended LOT Accepted By Quality,\n";
                        }




                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection))
                        {
                            txtStatus.Text = "FIT(Insp),";
                            txtStatus.ToolTip = "Sent To Fitup Inspection,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                        {
                            txtStatus.Text = "QIACC,";
                            txtStatus.ToolTip = "Items Accepted By Quality,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted))
                        {
                            txtStatus.Text = "QINACC,";
                            txtStatus.ToolTip = "Items Not Accepted By Quality,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection))
                        {
                            txtStatus.Text = "FIN(Insp),";
                            txtStatus.ToolTip = "Sent To Final Inspection,\n";
                        }

                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework))
                        {
                            txtStatus.Text = "RW,";
                            txtStatus.ToolTip = "Sent To Rework,\n";
                        }


                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                        {
                            txtStatus.Text = "COM";
                            txtStatus.ToolTip = "Completed by Quality after Final Inspection";
                        }


                        else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                        {
                            totalQuantity = 0;
                            intlInspectionCount = 0;
                            QAItemAcceptedCount = 0;
                            QQAItemNotAcceptedCount = 0;
                            finalInspectionCount = 0;
                            reworkCount = 0;
                            completeCount = 0;

                            foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                            {
                                if (dr5["QUANTITY"] != DBNull.Value && Convert.ToInt32(dr5["QUANTITY"]) > 0)
                                    totalQuantity += Convert.ToInt32(dr5["QUANTITY"]);

                                if (dr5["INTERNAL_INSPECTION"] != DBNull.Value && Convert.ToInt32(dr5["INTERNAL_INSPECTION"]) > 0)
                                    intlInspectionCount += Convert.ToInt32(dr5["INTERNAL_INSPECTION"]);

                                if (dr5["QA_ITEM_ACCEPTED"] != DBNull.Value && Convert.ToInt32(dr5["QA_ITEM_ACCEPTED"]) > 0)
                                    QAItemAcceptedCount += Convert.ToInt32(dr5["QA_ITEM_ACCEPTED"]);

                                if (dr5["QA_ITEM_NOT_ACCEPTED"] != DBNull.Value && Convert.ToInt32(dr5["QA_ITEM_NOT_ACCEPTED"]) > 0)
                                    QQAItemNotAcceptedCount += Convert.ToInt32(dr5["QA_ITEM_NOT_ACCEPTED"]);


                                if (dr5["IN_FINAL_INSPECTION"] != DBNull.Value && Convert.ToInt32(dr5["IN_FINAL_INSPECTION"]) > 0)
                                    finalInspectionCount += Convert.ToInt32(dr5["IN_FINAL_INSPECTION"]);

                                if (dr5["IN_REWORK"] != DBNull.Value && Convert.ToInt32(dr5["IN_REWORK"]) > 0)
                                    reworkCount += Convert.ToInt32(dr5["IN_REWORK"]);


                                if (dr5["COMPLETE"] != DBNull.Value && Convert.ToInt32(dr5["COMPLETE"]) > 0)
                                    completeCount += Convert.ToInt32(dr5["COMPLETE"]);
                            }
                        }
                    }

                    if (intlInspectionCount > 0)
                    {
                        txtStatus.Text += intlInspectionCount + "-FIT(Insp),";
                        txtStatus.ToolTip += intlInspectionCount + "-Sent To Fitup Inspection By Production,\n";
                    }

                    if (QAItemAcceptedCount > 0)
                    {
                        txtStatus.Text += QAItemAcceptedCount + "-QAA,";
                        txtStatus.ToolTip += QAItemAcceptedCount + "-Accepted by Quality for Fitup Inspection,\n";
                    }

                    if (QQAItemNotAcceptedCount > 0)
                    {
                        txtStatus.Text += QQAItemNotAcceptedCount + "-QAN,";
                        txtStatus.ToolTip += QQAItemNotAcceptedCount + "-Not Accepted by Quality for Fitup Inspection,\n";
                    }


                    if (finalInspectionCount > 0)
                    {
                        txtStatus.Text += finalInspectionCount + "-FIN(Insp),";
                        txtStatus.ToolTip += finalInspectionCount + "-Sent To Final Inspection By Production,\n";
                    }

                    if (reworkCount > 0)
                    {
                        txtStatus.Text += reworkCount + "-RW,";
                        txtStatus.ToolTip += reworkCount + "-Sent To Rework By Quality,\n";
                    }

                    if (completeCount > 0)
                    {
                        txtStatus.Text += completeCount + "-COM,";
                        txtStatus.ToolTip += completeCount + "-Completed by Quality after Final Inspection,\n";
                    }

                    txtStatus.Text = txtStatus.Text.TrimEnd(',');
                    txtStatus.ToolTip = txtStatus.ToolTip.TrimEnd('\n').TrimEnd(',');
                    txtStatus.ForeColor = System.Drawing.Color.White;
                }

                #endregion



                //Button btnSendCancellationMail = (Button)e.Row.FindControl("btnSendCancellationMail");
                //btnSendCancellationMail.Visible = false;

                companyID = Convert.ToInt32(lblUnitID.Text);
                PEID = Convert.ToInt32(lblJobPEID.Text);
                PMID = Convert.ToInt32(lblJobPMID.Text);


                //foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                //{
                //    foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                //    {
                //        IsCancellationMailSent = 0;
                //        IsCancellationMailSent = Convert.ToInt32(dr1["IS_CANCELLATION_MAIL_SENT"]);

                //        if (IsCancellationMailSent == 0)
                //        {
                //            btnSendCancellationMail.Visible = true;
                //        }
                //        else
                //        {
                //            btnSendCancellationMail.Visible = false;
                //        }
                //    }
                //}



                #region ATTACHMENTS

                string attachment1Extn = string.Empty;
                string attachment2Extn = string.Empty;
                string attachment3Extn = string.Empty;
                string attachment4Extn = string.Empty;


                Label lblAttachment1 = (Label)e.Row.FindControl("lblAttachment1");
                Label lblAttachment2 = (Label)e.Row.FindControl("lblAttachment2");
                Label lblAttachment3 = (Label)e.Row.FindControl("lblAttachment3");
                Label lblAttachment4 = (Label)e.Row.FindControl("lblAttachment4");


                ImageButton imgBtnAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAttachment1");
                ImageButton imgBtnAttachment2 = (ImageButton)e.Row.FindControl("imgBtnAttachment2");
                ImageButton imgBtnAttachment3 = (ImageButton)e.Row.FindControl("imgBtnAttachment3");
                ImageButton imgBtnAttachment4 = (ImageButton)e.Row.FindControl("imgBtnAttachment4");



                imgBtnAttachment1.Visible = false;
                imgBtnAttachment2.Visible = false;
                imgBtnAttachment3.Visible = false;
                imgBtnAttachment4.Visible = false;


                imgBtnAttachment1.ToolTip = string.Empty;
                imgBtnAttachment2.ToolTip = string.Empty;
                imgBtnAttachment3.ToolTip = string.Empty;
                imgBtnAttachment4.ToolTip = string.Empty;




                if (!string.IsNullOrEmpty(lblAttachment1.Text))
                {
                    imgBtnAttachment1.Visible = true;
                    imgBtnAttachment1.ToolTip = lblAttachment1.Text;

                    attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
                    if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                    {
                        imgBtnAttachment1.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                    else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                    {
                        imgBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                    else if (attachment1Extn == "dxf" || attachment1Extn == "DXF")
                    {
                        imgBtnAttachment1.ImageUrl = "~/Images/LOT/dxf.png";
                        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                    else if (attachment1Extn == "dwg" || attachment1Extn == "DWG")
                    {
                        imgBtnAttachment1.ImageUrl = "~/Images/LOT/dwg.png";
                        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                }
                else
                {
                    imgBtnAttachment1.Visible = false;
                }




                if (!string.IsNullOrEmpty(lblAttachment2.Text))
                {
                    imgBtnAttachment2.Visible = true;
                    imgBtnAttachment2.ToolTip = lblAttachment2.Text;

                    attachment2Extn = Convert.ToString(lblAttachment2.Text).Split('.').Last();
                    if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "dxf" || attachment2Extn == "DXF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/LOT/dxf.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "dwg" || attachment2Extn == "DWG")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/LOT/dwg.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                }
                else
                {
                    imgBtnAttachment2.Visible = false;
                }




                if (!string.IsNullOrEmpty(lblAttachment3.Text))
                {
                    imgBtnAttachment3.Visible = true;
                    imgBtnAttachment3.ToolTip = lblAttachment3.Text;

                    attachment3Extn = Convert.ToString(lblAttachment3.Text).Split('.').Last();
                    if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" || attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
                    {
                        imgBtnAttachment3.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                    else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
                    {
                        imgBtnAttachment3.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                }
                else
                {
                    imgBtnAttachment3.Visible = false;
                }


                if (!string.IsNullOrEmpty(lblAttachment4.Text))
                {
                    imgBtnAttachment4.Visible = true;
                    imgBtnAttachment4.ToolTip = lblAttachment4.Text;

                    attachment4Extn = Convert.ToString(lblAttachment4.Text).Split('.').Last();
                    if (attachment4Extn == "jpg" || attachment4Extn == "jepg" || attachment4Extn == "bmp" || attachment4Extn == "png" || attachment4Extn == "gif" || attachment4Extn == "JPG" || attachment4Extn == "JPEG" || attachment4Extn == "BMP" || attachment4Extn == "PNG" || attachment4Extn == "GIF")
                    {
                        imgBtnAttachment4.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment4.ToolTip = lblAttachment4.Text;
                    }
                    else if (attachment4Extn == "pdf" || attachment4Extn == "PDF")
                    {
                        imgBtnAttachment4.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment4.ToolTip = lblAttachment4.Text;
                    }
                }
                else
                {
                    imgBtnAttachment4.Visible = false;
                }


                #endregion
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }




    protected void gvSubitemsSI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4" ||
                    Convert.ToString(e.CommandArgument) == "ViewIRNAttachment")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblLOTTFID = gvSubitemsSI.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblLOTTFSubitemID = gvSubitemsSI.Rows[rowindex].FindControl("lblLOTTFSubitemID") as Label;
                Label lblAttachment1 = gvSubitemsSI.Rows[rowindex].FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = gvSubitemsSI.Rows[rowindex].FindControl("lblAttachment2") as Label;
                Label lblAttachment3 = gvSubitemsSI.Rows[rowindex].FindControl("lblAttachment3") as Label;
                Label lblAttachment4 = gvSubitemsSI.Rows[rowindex].FindControl("lblAttachment4") as Label;
                Label lblIRNAttachment = gvSubitemsSI.Rows[rowindex].FindControl("lblIRNAttachment") as Label;



                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "DRAWING1", Convert.ToString(lblAttachment1.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                    mpeSubitemDetail.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "DRAWING2", Convert.ToString(lblAttachment2.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                    mpeSubitemDetail.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "DRAWING3", Convert.ToString(lblAttachment3.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                    mpeSubitemDetail.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "DRAWING4", Convert.ToString(lblAttachment4.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                    mpeSubitemDetail.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewIRNAttachment")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "IRN", Convert.ToString(lblIRNAttachment.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                    mpeSubitemDetail.Show();
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

    protected void gvSubitemsSI_RowDataBound(object sender, GridViewRowEventArgs e)
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

                gvSubitemsSI.Columns[15].Visible = false;
                gvSubitemsSI.Columns[16].Visible = false;
                gvSubitemsSI.Columns[17].Visible = false;


                DataTable dtRemainingQuantity = new DataTable();
                dtRemainingQuantity.Columns.Add("REMAINING_QUANTITY", typeof(int));

                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                Label lblLOTMainSubitemID = (Label)e.Row.FindControl("lblLOTMainSubitemID");
                Label lblProdManagerID = (Label)e.Row.FindControl("lblProdManagerID");
                Label lblLOTTFSubitemID = (Label)e.Row.FindControl("lblLOTTFSubitemID");

                TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                //TextBox txtRemainingQuantity = (TextBox)e.Row.FindControl("txtRemainingQuantity");
                Label lblRemainingQuantity = (Label)e.Row.FindControl("lblRemainingQuantity");
                DropDownList ddlRemainingQuantity = (DropDownList)e.Row.FindControl("ddlRemainingQuantity");


                Label lblQuantity = (Label)e.Row.FindControl("lblQuantity");

                Label lblInternalInspQty = (Label)e.Row.FindControl("lblInternalInspQty");
                Label lblQAItemAcceptedQty = (Label)e.Row.FindControl("lblQAItemAcceptedQty");
                Label lblQAItemNotAcceptedQty = (Label)e.Row.FindControl("lblQAItemNotAcceptedQty");
                Label lblCompleteQty = (Label)e.Row.FindControl("lblCompleteQty");

                TextBox txtStatus = (TextBox)e.Row.FindControl("txtStatus");


                for (int i = 1; i <= Convert.ToInt32(lblRemainingQuantity.Text); i++)
                {
                    DataRow dr = dtRemainingQuantity.NewRow();
                    dr["REMAINING_QUANTITY"] = i;
                    dtRemainingQuantity.Rows.Add(dr);
                }

                if (dtRemainingQuantity.Rows.Count > 0)
                {
                    ddlRemainingQuantity.DataSource = dtRemainingQuantity;
                    ddlRemainingQuantity.DataValueField = "REMAINING_QUANTITY";
                    ddlRemainingQuantity.DataTextField = "REMAINING_QUANTITY";
                    ddlRemainingQuantity.DataBind();
                    ddlRemainingQuantity.SelectedValue = Convert.ToString(lblRemainingQuantity.Text);
                }


                statusID = Convert.ToInt32(lblStatusID.Text);

                #region ATTACHMENTS                

                string attachment1Extn = string.Empty;
                string attachment2Extn = string.Empty;
                string attachment3Extn = string.Empty;
                string attachment4Extn = string.Empty;

                Label lblAttachment1 = (Label)e.Row.FindControl("lblAttachment1");
                Label lblAttachment2 = (Label)e.Row.FindControl("lblAttachment2");
                Label lblAttachment3 = (Label)e.Row.FindControl("lblAttachment3");
                Label lblAttachment4 = (Label)e.Row.FindControl("lblAttachment4");

                ImageButton imgBtnAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAttachment1");
                ImageButton imgBtnAttachment2 = (ImageButton)e.Row.FindControl("imgBtnAttachment2");
                ImageButton imgBtnAttachment3 = (ImageButton)e.Row.FindControl("imgBtnAttachment3");
                ImageButton imgBtnAttachment4 = (ImageButton)e.Row.FindControl("imgBtnAttachment4");

                imgBtnAttachment1.Visible = false;
                imgBtnAttachment2.Visible = false;
                imgBtnAttachment3.Visible = false;
                imgBtnAttachment4.Visible = false;

                imgBtnAttachment1.ToolTip = string.Empty;
                imgBtnAttachment2.ToolTip = string.Empty;
                imgBtnAttachment3.ToolTip = string.Empty;
                imgBtnAttachment4.ToolTip = string.Empty;




                if (!string.IsNullOrEmpty(lblAttachment1.Text))
                {
                    imgBtnAttachment1.Visible = true;
                    imgBtnAttachment1.ToolTip = lblAttachment1.Text;

                    attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
                    if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                    {
                        imgBtnAttachment1.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                    else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                    {
                        imgBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                }
                else
                {
                    imgBtnAttachment1.Visible = false;
                }


                if (!string.IsNullOrEmpty(lblAttachment2.Text))
                {
                    imgBtnAttachment2.Visible = true;
                    imgBtnAttachment2.ToolTip = lblAttachment2.Text;

                    attachment2Extn = Convert.ToString(lblAttachment2.Text).Split('.').Last();
                    if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "dxf" || attachment2Extn == "DXF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/LOT/dxf.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "dwg" || attachment2Extn == "DWG")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/LOT/dwg.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                }
                else
                {
                    imgBtnAttachment2.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblAttachment3.Text))
                {
                    imgBtnAttachment3.Visible = true;
                    imgBtnAttachment3.ToolTip = lblAttachment3.Text;

                    attachment3Extn = Convert.ToString(lblAttachment3.Text).Split('.').Last();
                    if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" || attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
                    {
                        imgBtnAttachment3.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                    else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
                    {
                        imgBtnAttachment3.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                }
                else
                {
                    imgBtnAttachment3.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblAttachment4.Text))
                {
                    imgBtnAttachment4.Visible = true;
                    imgBtnAttachment4.ToolTip = lblAttachment4.Text;

                    attachment4Extn = Convert.ToString(lblAttachment4.Text).Split('.').Last();
                    if (attachment4Extn == "jpg" || attachment4Extn == "jepg" || attachment4Extn == "bmp" || attachment4Extn == "png" || attachment4Extn == "gif" || attachment4Extn == "JPG" || attachment4Extn == "JPEG" || attachment4Extn == "BMP" || attachment4Extn == "PNG" || attachment4Extn == "GIF")
                    {
                        imgBtnAttachment4.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment4.ToolTip = lblAttachment4.Text;
                    }
                    else if (attachment4Extn == "pdf" || attachment4Extn == "PDF")
                    {
                        imgBtnAttachment4.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment4.ToolTip = lblAttachment4.Text;
                    }
                }
                else
                {
                    imgBtnAttachment4.Visible = false;
                }



                string IRNAttachmentExtn = string.Empty;
                Label lblIRNAttachment = (Label)e.Row.FindControl("lblIRNAttachment");
                ImageButton imgBtnIRNAttachment = (ImageButton)e.Row.FindControl("imgBtnIRNAttachment");
                imgBtnIRNAttachment.Visible = false;
                imgBtnIRNAttachment.ToolTip = string.Empty;

                if (!string.IsNullOrEmpty(lblIRNAttachment.Text))
                {
                    imgBtnIRNAttachment.Visible = true;
                    imgBtnIRNAttachment.ToolTip = lblIRNAttachment.Text;

                    IRNAttachmentExtn = Convert.ToString(lblIRNAttachment.Text).Split('.').Last();
                    if (IRNAttachmentExtn == "jpg" || IRNAttachmentExtn == "jepg" || IRNAttachmentExtn == "bmp" || IRNAttachmentExtn == "png" || IRNAttachmentExtn == "gif" || IRNAttachmentExtn == "JPG" || IRNAttachmentExtn == "JPEG" || IRNAttachmentExtn == "BMP" || IRNAttachmentExtn == "PNG" || IRNAttachmentExtn == "GIF")
                    {
                        imgBtnIRNAttachment.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnIRNAttachment.ToolTip = lblIRNAttachment.Text;
                    }
                    else if (IRNAttachmentExtn == "pdf" || IRNAttachmentExtn == "PDF")
                    {
                        imgBtnIRNAttachment.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnIRNAttachment.ToolTip = lblIRNAttachment.Text;
                    }
                }
                else
                {
                    imgBtnIRNAttachment.Visible = false;
                }

                #endregion


                #region CATEGORY

                string txt = string.Empty;
                string categoryTxt = string.Empty;

                Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
                Label lblCategory = (Label)e.Row.FindControl("lblCategory");


                if (!string.IsNullOrEmpty(lblCategoryID.Text))
                {
                    string[] srtCategoryID = lblCategoryID.Text.Split(',');
                    foreach (string i in srtCategoryID)
                    {
                        if (Convert.ToInt32(i) > 0)
                        {
                            if (Convert.ToInt32(i) == 1)
                                txt = "Fabrication";
                            else if (Convert.ToInt32(i) == 2)
                                txt = "Inspection";
                            else if (Convert.ToInt32(i) == 3)
                                txt = "Information";
                        }

                        categoryTxt += txt + ",";
                    }
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    categoryTxt = categoryTxt.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    lblCategory.Text = categoryTxt;
                }

                #endregion


                #region STATUS

                if (Session["dsSubitemsSI"] != null)
                    dsSubitems = (DataSet)Session["dsSubitemsSI"];

                int count1 = 0;
                int totalQuantity = 0;
                int intlInspectionCount = 0;

                int finalInspectionCount = 0;
                int reworkCount = 0;

                int QAItemAcceptedCount = 0;
                int QQAItemNotAcceptedCount = 0;
                int completeCount = 0;
                int amndmntCount = 0;
                int isPartOfProductionID = 0;

                foreach (DataRow dr0 in dsSubitems.Tables[0].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(lblLOTTFSubitemID.Text) + "'"))
                {
                    if (dr0["AMENDMENT_COUNT"] != DBNull.Value)
                        amndmntCount = Convert.ToInt32(dr0["AMENDMENT_COUNT"]);
                    else amndmntCount = 0;

                    if (dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] != DBNull.Value)
                        isPartOfProductionID = Convert.ToInt32(dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);
                    else isPartOfProductionID = 0;

                    //if (dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] != DBNull.Value && Convert.ToInt32(dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                    //{
                    if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                    {
                        count1++;
                        txtStatus.Text = "NEW,";
                        txtStatus.ToolTip = "New";
                    }
                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                    {
                        count1++;
                        txtStatus.Text = "APP";
                        txtStatus.ToolTip = "Approved by PE/PM";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                    {
                        count1++;
                        txtStatus.Text = "PLAC";
                        txtStatus.ToolTip = "Accepted by Planning";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                    {
                        count1++;
                        txtStatus.Text = "FWD";
                        txtStatus.ToolTip = "Forwarded by Planning";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                    {
                        count1++;
                        txtStatus.Text = "ACC,";
                        txtStatus.ToolTip = "Accepted by Production,\n";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                    {
                        count1++;
                        txtStatus.Text = "QACC,";
                        txtStatus.ToolTip = "LOT Accepted by Quality,\n";
                    }


                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                    {
                        count1++;
                        txtStatus.Text = "AMM,";
                        txtStatus.ToolTip = "In Amendment,\n";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                    {
                        count1++;
                        txtStatus.Text = "AMD,";
                        txtStatus.ToolTip = "Amended,\n";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                    {
                        count1++;
                        txtStatus.Text = "AMDAPP,";
                        txtStatus.ToolTip = "Amended Approved by PE/PM,\n";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                    {
                        count1++;
                        txtStatus.Text = "AMDPLAC";
                        txtStatus.ToolTip = "Amended Accepted by Planning";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                    {
                        count1++;
                        txtStatus.Text = "AMDFWD";
                        txtStatus.ToolTip = "Amended Forwarded by Planning";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                    {
                        count1++;
                        txtStatus.Text = "AMDACC,";
                        txtStatus.ToolTip = "Amended Accepted By Production,\n";
                    }

                    else if (Convert.ToInt32(dr0["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        count1++;
                        txtStatus.Text = "AMDQACC,";
                        txtStatus.ToolTip = "Amended LOT Accepted By Quality,\n";
                    }
                    //}

                }

                if (count1 == 0)
                {
                    foreach (DataRow dr0 in dsSubitems.Tables[0].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(lblLOTTFSubitemID.Text) + "'"))
                    {
                        if (dr0["QUANTITY"] != DBNull.Value && Convert.ToInt32(dr0["QUANTITY"]) > 0)
                            totalQuantity = Convert.ToInt32(dr0["QUANTITY"]);
                        else totalQuantity = 0;

                        if (dr0["INTERNAL_INSPECTION_QTY"] != DBNull.Value && Convert.ToInt32(dr0["INTERNAL_INSPECTION_QTY"]) > 0)
                            intlInspectionCount = Convert.ToInt32(dr0["INTERNAL_INSPECTION_QTY"]);
                        else intlInspectionCount = 0;


                        if (dr0["IN_FINAL_INSPECTION_QTY"] != DBNull.Value && Convert.ToInt32(dr0["IN_FINAL_INSPECTION_QTY"]) > 0)
                            finalInspectionCount = Convert.ToInt32(dr0["IN_FINAL_INSPECTION_QTY"]);
                        else finalInspectionCount = 0;

                        if (dr0["IN_REWORK_QTY"] != DBNull.Value && Convert.ToInt32(dr0["IN_REWORK_QTY"]) > 0)
                            reworkCount = Convert.ToInt32(dr0["IN_REWORK_QTY"]);
                        else reworkCount = 0;


                        if (dr0["QA_ITEM_ACCEPTED_QTY"] != DBNull.Value && Convert.ToInt32(dr0["QA_ITEM_ACCEPTED_QTY"]) > 0)
                            QAItemAcceptedCount = Convert.ToInt32(dr0["QA_ITEM_ACCEPTED_QTY"]);
                        else QAItemAcceptedCount = 0;

                        if (dr0["QA_ITEM_NOT_ACCEPTED_QTY"] != DBNull.Value && Convert.ToInt32(dr0["QA_ITEM_NOT_ACCEPTED_QTY"]) > 0)
                            QQAItemNotAcceptedCount = Convert.ToInt32(dr0["QA_ITEM_NOT_ACCEPTED_QTY"]);
                        else QQAItemNotAcceptedCount = 0;

                        if (dr0["COMPLETE_QTY"] != DBNull.Value && Convert.ToInt32(dr0["COMPLETE_QTY"]) > 0)
                            completeCount = Convert.ToInt32(dr0["COMPLETE_QTY"]);
                        else completeCount = 0;
                    }

                    if (intlInspectionCount > 0)
                    {
                        txtStatus.Text += intlInspectionCount + "-FIT(Insp),";
                        txtStatus.ToolTip += intlInspectionCount + "-Sent To Fitup Inspection By Production,\n";
                    }

                    if (QAItemAcceptedCount > 0)
                    {
                        txtStatus.Text += QAItemAcceptedCount + "-QAA,";
                        txtStatus.ToolTip += QAItemAcceptedCount + "-Accepted by Quality for Fitup Inspection,\n";
                    }

                    if (QQAItemNotAcceptedCount > 0)
                    {
                        txtStatus.Text += QQAItemNotAcceptedCount + "-QAN,";
                        txtStatus.ToolTip += QQAItemNotAcceptedCount + "-Not Accepted by Quality for Fitup Inspection,\n";
                    }


                    if (finalInspectionCount > 0)
                    {
                        txtStatus.Text += finalInspectionCount + "-FIN(Insp),";
                        txtStatus.ToolTip += finalInspectionCount + "-Sent To Final Inspection By Production,\n";
                    }

                    if (reworkCount > 0)
                    {
                        txtStatus.Text += reworkCount + "-RW,";
                        txtStatus.ToolTip += QQAItemNotAcceptedCount + "-Sent To Rework By Quality,\n";
                    }



                    if (completeCount > 0)
                    {
                        txtStatus.Text += completeCount + "-COM,";
                        txtStatus.ToolTip += completeCount + "-Completed by Quality after Final Inspection,\n";
                    }


                    if (completeCount >= totalQuantity)
                    {
                        txtStatus.Text = "COM";
                        txtStatus.ToolTip = "Completed by Quality after Final Inspection";
                    }

                    if (isPartOfProductionID == 0)
                    {
                        if (amndmntCount == 0)
                        {
                            txtStatus.Text = "QACC,";
                            txtStatus.ToolTip = "LOT Accepted by Quality,\n";
                        }
                        else
                        {
                            txtStatus.Text = "AMDQACC,";
                            txtStatus.ToolTip = "Amended LOT Accepted By Quality,\n";
                        }
                    }
                }

                txtStatus.Text = txtStatus.Text.TrimEnd(',');
                txtStatus.ToolTip = txtStatus.ToolTip.TrimEnd('\n').TrimEnd(',');
                txtStatus.ForeColor = System.Drawing.Color.White;



                #endregion


                Label lblIsPartOfProductionStatusReportID = (Label)e.Row.FindControl("lblIsPartOfProductionStatusReportID");
                CheckBox chkIsPartOfProductionStatusReport = (CheckBox)e.Row.FindControl("chkIsPartOfProductionStatusReport");

                if (Convert.ToInt32(lblIsPartOfProductionStatusReportID.Text) > 0)
                    chkIsPartOfProductionStatusReport.Checked = true;
                else chkIsPartOfProductionStatusReport.Checked = false;

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


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            if (!Convert.ToBoolean(Session["UpdateSubitemStatusClicked"]))
            {
                Session["UpdateSubitemStatusClicked"] = true;
                CancelLOT();
                GetLOTList();
            }
            else
            {
                if (!Convert.ToBoolean(Session["MultipleClicked"]))
                {
                    GetLOTList();
                    Session["MultipleClicked"] = true;
                }
            }
        }
    }

    protected void gvSubItemToU_RowDataBound(object sender, GridViewRowEventArgs e)
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
                #region CATEGORY

                string txt = string.Empty;
                string categoryTxt = string.Empty;

                Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
                Label lblCategory = (Label)e.Row.FindControl("lblCategory");





                if (!string.IsNullOrEmpty(lblCategoryID.Text))
                {
                    string[] srtCategoryID = lblCategoryID.Text.Split(',');
                    foreach (string i in srtCategoryID)
                    {
                        if (Convert.ToInt32(i) > 0)
                        {
                            if (Convert.ToInt32(i) == 1)
                                txt = "Fabrication";
                            else if (Convert.ToInt32(i) == 2)
                                txt = "Inspection";
                            else if (Convert.ToInt32(i) == 3)
                                txt = "Information";
                        }

                        categoryTxt += txt + ",";
                    }
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    categoryTxt = categoryTxt.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    lblCategory.Text = categoryTxt;
                }

                #endregion



                Label lblIsPartOfProductionStatusReport = (Label)e.Row.FindControl("lblIsPartOfProductionStatusReport");
                CheckBox chkIsPartOfProductStatusReport = (CheckBox)e.Row.FindControl("chkIsPartOfProductStatusReport");

                if (Convert.ToInt32(lblIsPartOfProductionStatusReport.Text) > 0)
                    chkIsPartOfProductStatusReport.Checked = true;
                else chkIsPartOfProductStatusReport.Checked = false;




                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    protected void gvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
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
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    #endregion


    #region METHODS[===========================]

    private void BindCompany()
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
                //ddlCompany.Items.Insert(0, "All");
                //ddlCompany.SelectedIndex = 0;
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

            dsLOTStatus = objProject.GetLOTTFStatus();
            if (dsLOTStatus.Tables.Count > 0 && dsLOTStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsLOTStatus.Tables[0];
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

    private void BindLOTFor()
    {
        try
        {
            dsLOTfor = objProject.GetLotMainItems();
            if (dsLOTfor.Tables.Count > 0 && dsLOTfor.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItems.DataSource = dsLOTfor.Tables[0];
                ddlLOTMainItems.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItems.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItems.DataBind();
                ddlLOTMainItems.Items.Insert(0, "All");
                ddlLOTMainItems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainSubItems(int LOTMainItemID)
    {
        try
        {
            dsLOTMainSubItems = objProject.GetLotMainSubItems(LOTMainItemID, Convert.ToInt32(ddlCompany.SelectedValue));
            if (dsLOTMainSubItems.Tables.Count > 0 && dsLOTMainSubItems.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainSubitems.DataSource = dsLOTMainSubItems.Tables[0];
                ddlLOTMainSubitems.DataTextField = "LOT_MAIN_SUBITEM";
                ddlLOTMainSubitems.DataValueField = "LOT_MAIN_SUBITEM_ID";
                ddlLOTMainSubitems.DataBind();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;
            }
            else
            {
                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataSet GetLOTSubitemDetails(int LOTTFID, string LOTMainSubitemIDs, string LOTTFSubitemIDs)
    {
        try
        {
            dsSubitems = objProject.GetLOTSubitemsByLOTID(LOTTFID, LOTMainSubitemIDs, LOTTFSubitemIDs);
            if (dsSubitems.Tables.Count > 0 && dsSubitems.Tables[0].Rows.Count > 0)
            {
                Session["dsSubitemsSI"] = dsSubitems;
                return dsSubitems;
            }
            else
            {
                Session["dsSubitemsSI"] = null;
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private void BindLOTSubitemDetails(int LOTTFID, string LOTMainSubitemIDs, string LOTTFSubitemIDs)
    {
        try
        {
            dsSubitems = GetLOTSubitemDetails(LOTTFID, LOTMainSubitemIDs, LOTTFSubitemIDs);
            if (dsSubitems.Tables.Count > 0 && dsSubitems.Tables[0].Rows.Count > 0)
            {
                gvSubitemsSI.DataSource = dsSubitems.Tables[0];
                gvSubitemsSI.DataBind();
            }
            else
            {
                gvSubitemsSI.DataSource = null;
                gvSubitemsSI.DataBind();
            }

            lblSubitemsSIRerords.Text = "Records[" + gvSubitemsSI.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            //
        }
    }


    private void GetLOTList()
    {
        try
        {
            startDate = string.Empty;
            endDate = string.Empty;
            LOTTFNo = string.Empty;
            statusID = 0;
            unitID = 0;
            jobNo = string.Empty;
            customerName = string.Empty;
            LOTMainItemID = 0;
            LOTMainSubitemID = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtTFNo.Text))
                LOTTFNo = txtTFNo.Text;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);

            unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;

            if (ddlLOTMainItems.SelectedIndex > 0)
                LOTMainItemID = Convert.ToInt32(ddlLOTMainItems.SelectedValue);

            if (ddlLOTMainSubitems.SelectedIndex > 0)
                LOTMainSubitemID = Convert.ToInt32(ddlLOTMainSubitems.SelectedValue);

            dsLOTList = objProject.GetLOTTFListForCancellation(startDate, endDate, LOTTFNo, statusID, unitID, jobNo, customerName, LOTMainItemID, LOTMainSubitemID);

            if (dsLOTList.Tables.Count > 0 && dsLOTList.Tables[0].Rows.Count > 0)
            {
                Session["dsLOTList"] = dsLOTList;
                gvLOTTFList.DataSource = dsLOTList.Tables[0];
                gvLOTTFList.DataBind();
            }
            else
            {
                Session["dsLOTList"] = null;
                gvLOTTFList.DataSource = null;
                gvLOTTFList.DataBind();
            }
            lblRecords.Text = "Records[" + dsLOTList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ViewDrawingFiles(int LOTTFID, string fileType, string fileName, int LOTTFSubitemID)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {

                ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);
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

    private void ExportDWGFile(int LOTTFID, int LOTTFSubitemID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();
            dsFiles = objProject.GetLOTDrawingFiles(LOTTFID, LOTTFSubitemID);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "DRAWING1")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);
                }
                else if (fileType == "DRAWING2")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT2_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT2_NAME"]);
                }
                else if (fileType == "DRAWING3")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT3_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT3_NAME"]);
                }
                else if (fileType == "DRAWING4")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT4_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT4_NAME"]);
                }
                else if (fileType == "IRN")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["IRN_ATTACHMENT_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["IRN_ATTACHMENT_NAME"]);
                }

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
                mpeSubitemDetail.Show();
                return;
            }
            else
            {
                throw;
            }
        }
    }


    private void BindLOTTFDetailsToEdit(int LOTTFID)
    {
        try
        {
            PEApproverID = 0;
            PMApproverID = 0;
            amendmentCount = 0;

            dsLOTTFDetails = objProject.GetLOTTFDetailsForCancellation(LOTTFID);
            if (dsLOTTFDetails.Tables.Count > 0)
            {
                if (dsLOTTFDetails.Tables[0].Rows.Count > 0)
                {
                    if (dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"])))
                        lblTFNo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"])))
                        txtTFNoToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);


                    if (dsLOTTFDetails.Tables[0].Rows[0]["UNIT_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_ID"])))
                        hdCompanyToEdit.Value = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_ID"]);


                    if (dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"])))
                        txtCompanyToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"])))
                        txtJOBNoToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"])))
                        txtPONoToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"])))
                        txtCustomerNameToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"])))
                        txtCustomerCodeToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"]);


                    if (dsLOTTFDetails.Tables[0].Rows[0]["DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"])))
                    {
                        txtDateToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"]);
                    }

                    if (dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"])))
                        txtItemNameToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"]);
                }

                if (dsLOTTFDetails.Tables[1].Rows.Count > 0)
                {
                    gvSubItemToU.DataSource = dsLOTTFDetails.Tables[1];
                    gvSubItemToU.DataBind();
                    Session["dtSubitem"] = dsLOTTFDetails.Tables[1];
                }
                else
                {
                    gvSubItemToU.DataSource = null;
                    gvSubItemToU.DataBind();
                    Session["dtSubitem"] = null;
                }
                lblSubitemsRecords.Text = "Subitem Records[" + gvSubItemToU.Rows.Count + "]";


                if (dsLOTTFDetails.Tables[2].Rows.Count > 0)
                {
                    Session["dtProdMngr"] = dsLOTTFDetails.Tables[2];
                }
                else
                {
                    Session["dtProdMngr"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("Timeout expired"))
            {
                ExceptionUpdateMessage("The process is too longer, please try again...!!!");
                return;
            }
            else
            {
                ExceptionUpdateMessage(ex.ToString());
                return;
            }
        }
    }

    private void CancelLOT()
    {
        try
        {
            string LOTTFSubitemIDs = string.Empty;
            int createdByID = 0;
            int PEID = 0;
            int PMID = 0;

            LOTTFID = 0;
            remarks = string.Empty;

            if (Convert.ToInt32(ViewState["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

            if (!string.IsNullOrEmpty(txtNotesToEdit.Text))
                remarks = txtNotesToEdit.Text;

            if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0)
                companyID = Convert.ToInt32(ViewState["UNIT_ID"]);

            createdByID = Convert.ToInt32(ViewState["CREATED_BY_ID"]);
            PEID = Convert.ToInt32(ViewState["PE_ID"]);
            PMID = Convert.ToInt32(ViewState["PM_ID"]);



            if (LOTTFID > 0)
            {
                int cancellationVal = objProject.CancelLOT(LOTTFID, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (cancellationVal > 0)
                {
                    int sendMailValue = 0;
                    sendMailValue = objLOTSendMailForCancelledLOT.SendEmailCancelledLOT(LOTTFID, txtTFNoToEdit.Text, companyID, remarks);

                    if (sendMailValue > 0)
                    {
                        SuccessMessage("LOT with TF. No.: '" + txtTFNoToEdit.Text + "' cancelled and mail sent successfully...!!!");
                        int val = objProject.UpdateCancelledMailStatus(LOTTFID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        return;
                    }
                    else
                    {
                        SuccessMessage("LOT with TF. No.: '" + txtTFNoToEdit.Text + "' cancelled successfully...!!!");
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("Please try again...!!!");
                    return;
                }
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

    private void Reset()
    {
        try
        {
            //if (ddlLOTMainItemsToEdit.Items.Count > 0)
            //{
            //    ddlLOTMainItemsToEdit.SelectedIndex = 0;
            //}

            //ddlLOTMainSubitemsToEdit.Items.Clear();
            //ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
            //ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

            txtCustomerCodeToEdit.Text = string.Empty;
            txtCustomerNameToEdit.Text = string.Empty;

            txtDateToEdit.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            txtJOBNoToEdit.Text = string.Empty;
            txtPONoToEdit.Text = string.Empty;
            txtItemNameToEdit.Text = string.Empty;
            txtTFNoToEdit.Text = string.Empty;
            txtNotesToEdit.Text = string.Empty;

            Session["dtSubitem"] = null;



            dtTemp.Clear();
            dtSubitem.Clear();
            gvSubItemToU.DataSource = null;
            gvSubItemToU.DataBind();

            //ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    //private void ResetSubitems()
    //{
    //    ddlProductCodeToEdit.Items.Clear();
    //    ddlProductCodeToEdit.Items.Insert(0, "Select");
    //    ddlProductCodeToEdit.SelectedIndex = 0;
    //    txtProductionOrderDateToEdit.Text = string.Empty;
    //    txtUOMToEdit.Text = string.Empty;
    //    txtProductDescToEdit.Text = string.Empty;
    //    chkIsPartOfProductionOrMainDrawingToEdit.Checked = false;

    //    ddlLOTMainItemsToEdit.SelectedIndex = 0;

    //    ddlLOTMainSubitemsToEdit.Items.Clear();
    //    ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
    //    ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

    //    txtDescriptionToEdit.Text = string.Empty;
    //    txtDrgNoToEdit.Text = string.Empty;
    //    foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
    //    {
    //        item.Selected = false;
    //    }
    //    hdQuantityToEdit.Value = "0";
    //    txtQuantityToEdit.Text = string.Empty;
    //    txtTagNoToEdit.Text = string.Empty;
    //    txtProductionOrderNoToEdit.Text = string.Empty;
    //    txtExpectedCompletionDateToEdit.Text = string.Empty;
    //}

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

    private void HideSubitemsPanel()
    {
        pnlSubitemsMsg.Visible = false;
        lblSubitemsMsg.Text = string.Empty;
    }

    private byte[] GetFileBytes(string fileName, Stream stream)
    {
        byte[] GSTbytes = null;
        #region
        try
        {
            string GSTFilePath = fileName;
            string GSTFileName = Path.GetFileName(GSTFilePath);
            string GSText = Path.GetExtension(GSTFileName);
            string GSTContentType = string.Empty;
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
            if (GSTContentType != string.Empty)
            {
                try
                {
                    GSTfs = stream;
                    GSTfs.Position = 0;
                    GSTbr = new BinaryReader(GSTfs);
                    GSTbytes = GSTbr.ReadBytes((int)GSTfs.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ExceptionUpdateMessage("File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }

    private void ExceptionUpdateMessage(string message)
    {
        pnlUpdateMsg.Visible = true;
        lblUpdateMsg.Text = message;
        lblUpdateMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void ExceptionSubitemsMessage(string message)
    {
        pnlSubitemsMsg.Visible = true;
        lblSubitemsMsg.Text = message;
        lblSubitemsMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HideUpdatePanel()
    {
        pnlUpdateMsg.Visible = false;
        lblUpdateMsg.Text = string.Empty;
    }

    #endregion
}