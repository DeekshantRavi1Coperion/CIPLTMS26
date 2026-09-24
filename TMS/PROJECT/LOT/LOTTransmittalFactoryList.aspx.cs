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

public partial class PROJECT_LOT_LOTTransmittalFactoryList : System.Web.UI.Page
{

    #region VARIABLES[=============================]

    LOTSendMail objLOTSendMail = new LOTSendMail();
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
    DataSet dsDMSDrawingNo = new DataSet();


    int revisionNo = 0;
    string revisionNoText = string.Empty;

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


    Byte[] clientApproveDrawingFileBytes = null;
    string clientApproveDrawingFile = string.Empty;
    string clientApproveDrawingRemarks = string.Empty;


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




    #region POPULATE LIST START[===================]


    #region EVENTS[============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            HideUpdatePanel();
            if (!IsPostBack)
            {

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");


                hdPrintFlag.Value = "0";
                ViewState["ALL_BYTES"] = null;

                ViewState["DOCS"] = "";
                hdButtonFlag.Value = string.Empty;
                hdUpdationFlag.Value = "0";
                hdConfirmValue.Value = "0";
                hdClientDrawingConfirmValue.Value = "0";

                Session["dtQualityPersonList"] = null;
                Session["dsLOTList"] = null;
                Session["dsSubitemsSI"] = null;

                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;

                BindCompany();

                BindStatus();
                BindLOTFor();

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tfno"])))
                {
                    txtTFNo.Text = Convert.ToString(Request.QueryString["tfno"]);
                    if (Request.QueryString["unitid"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["unitid"])))
                    {
                        ddlCompany.SelectedValue = Convert.ToString(Request.QueryString["unitid"]);
                    }
                    else
                    {
                        txtTFNo.Text = Convert.ToString(Request.QueryString["tfno"]);
                        if (txtTFNo.Text.Contains("A35"))
                        {
                            ddlCompany.SelectedValue = "1";
                        }
                        else if (txtTFNo.Text.Contains("GNU"))
                        {
                            ddlCompany.SelectedValue = "4";
                        }
                    }
                }


                hdDateToEdit.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDateToEdit.Text = hdDateToEdit.Value;

                Session["dsApprovers"] = null;
                Session["dtProdMngr"] = null;

                AddTempSubitemTable();
                BindLOTMainItemsToEdit();
                BindLOTCategoryForFactoryToEdit();
                //GetLOTList();
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
        //string url = "ViewAttachedPDFFileTest.aspx?LOTTFID=2&fileType=DRAWING1&LOTTFSubitemID=0";        
        //string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
        //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

        //DownloadFile();
        //iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFileTest.aspx?li=2");

        GetLOTList();
    }

    protected void btnAddNewLOT_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/LOTTransmittalFactory.aspx");

        //string url = string.Empty;
        //if (iframeViewPDFFile.Attributes["src"] != null)
        //{
        //    url = iframeViewPDFFile.Attributes["src"];
        //    url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]) + "/PROJECT/LOT/" + url;
        //    hdDocsURL.Value = url;

        //    //Print("Test", 1, "A2");

        //}
    }




    public bool PrintFiles()
    {
        try
        {
            //if (Convert.ToInt32(hdPrintFlag.Value) > 0)
            //{
            //    byte[] bytes = null;

            //    Stream stream = null;
            //    string fileName = string.Empty;
            //    string printer = string.Empty;
            //    int copies = 0;
            //    string paperName = string.Empty;


            //    if (ViewState["ALL_BYTES"] != null)
            //    {
            //        bytes = (byte[])ViewState["ALL_BYTES"];
            //    }

            //    if (bytes != null && bytes.Length > 0)
            //    {
            //        stream = new MemoryStream(bytes);
            //    }
            //    else
            //    {
            //        stream = null;
            //    }

            //    if (chkIsSendToPrint.Checked && ddlPrinter.SelectedIndex > 0)
            //    {
            //        fileName = "http://localhost:61831/PROJECT/LOT/ViewAttachedPDFFileTest.aspx?li=2";//Convert.ToString(hdDocsURL.Value);
            //        printer = Convert.ToString(ddlPrinter.SelectedValue);
            //        copies = Convert.ToInt32(txtCopies.Text);
            //        paperName = Convert.ToString(ddlPaper.SelectedValue);


            //        Spire.Pdf.PdfDocument pdfdocument = new Spire.Pdf.PdfDocument();
            //        //pdfdocument.LoadFromFile(fileName);
            //        pdfdocument.LoadFromStream(stream);
            //        if (pdfdocument != null)
            //        {
            //            pdfdocument.PrintSettings.PrinterName = printer;
            //            pdfdocument.PrintSettings.Copies = (short)copies;

            //            PrinterSettings ps = new PrinterSettings();
            //            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();

            //            if (paperName == "A2")
            //            {
            //                PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A2);
            //                pdfdocument.PrintSettings.PaperSize = paperSize;
            //            }
            //            else if (paperName == "A3")
            //            {
            //                PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A3);
            //                pdfdocument.PrintSettings.PaperSize = paperSize;
            //            }
            //            else if (paperName == "A4")
            //            {
            //                PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4);
            //                pdfdocument.PrintSettings.PaperSize = paperSize;
            //            }
            //            else if (paperName == "A5")
            //            {
            //                PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5);
            //                pdfdocument.PrintSettings.PaperSize = paperSize;
            //            }
            //            else if (paperName == "A6")
            //            {
            //                PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A6);
            //                pdfdocument.PrintSettings.PaperSize = paperSize;
            //            }

            //            pdfdocument.Print();
            //            pdfdocument.Dispose();

            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
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
                hdPrintFlag.Value = "0";
                hdRemovedSubitemIDs.Value = string.Empty;

                //Reset();
                currentStatusID = 0;
                createdByID = 0;
                productionManagerID = 0;

                hdUpdationFlag.Value = "0";
                LOTTFID = 0;
                companyID = 0;

                int rowindex = 0;

                chkCopyImportantNotes.Checked = false;


                if (Convert.ToString(e.CommandArgument) == "ViewSubitemDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4" ||
                    Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "ViewClientApprovedDrg1" ||
                    Convert.ToString(e.CommandArgument) == "ViewClientApprovedDrg2" ||
                    Convert.ToString(e.CommandArgument) == "ViewClientApprovedDrg3" ||


                    Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING1" ||
                    Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING2" ||
                    Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING3" ||

                    Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL1" ||
                    Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL2" ||
                    Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL3" ||

                    Convert.ToString(e.CommandArgument) == "ViewSTANDARDDRAWING")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "APPROVE" ||
                         Convert.ToString(e.CommandArgument) == "AMEND" ||
                         Convert.ToString(e.CommandArgument) == "SEND_TO_FITUP_INSP" ||
                         Convert.ToString(e.CommandArgument) == "SEND_TO_FINAL_INSP" ||
                         Convert.ToString(e.CommandArgument) == "ACCEPT_ITEMS" ||
                         Convert.ToString(e.CommandArgument) == "COMPLETE_ITEMS" ||
                         Convert.ToString(e.CommandArgument) == "SEND_APPROVAL_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_FITUP_INSP_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_TO_FINAL_INSP_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_ACCEPT_ITEMS_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_NOT_ACCEPT_ITEMS_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_REWORK_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_FINAL_INSP_ITEMS_MAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblLOTTFID = gvLOTTFList.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblTFNo = gvLOTTFList.Rows[rowindex].FindControl("lblTFNo") as Label;
                Label lblUnitID = gvLOTTFList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblUnitName = gvLOTTFList.Rows[rowindex].FindControl("lblUnitName") as Label;
                Label lblLOTDate = gvLOTTFList.Rows[rowindex].FindControl("lblLOTDate") as Label;
                Label lblCustomerCode = gvLOTTFList.Rows[rowindex].FindControl("lblCustomerCode") as Label;
                Label lblCustomerName = gvLOTTFList.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblJOBNo = gvLOTTFList.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblProductionNo = gvLOTTFList.Rows[rowindex].FindControl("lblProductionNo") as Label;
                Label lblPONo = gvLOTTFList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblItemName = gvLOTTFList.Rows[rowindex].FindControl("lblItemName") as Label;
                Label lblImpNotes = gvLOTTFList.Rows[rowindex].FindControl("lblImpNotes") as Label;

                Label lblSubitemCounts = gvLOTTFList.Rows[rowindex].FindControl("lblSubitemCounts") as Label;
                Label lblAmendmentCounts = gvLOTTFList.Rows[rowindex].FindControl("lblAmendmentCounts") as Label;

                Label lblJobPEID = gvLOTTFList.Rows[rowindex].FindControl("lblJobPEID") as Label;
                Label lblJobPMID = gvLOTTFList.Rows[rowindex].FindControl("lblJobPMID") as Label;
                Label lblcreatedByID = gvLOTTFList.Rows[rowindex].FindControl("lblcreatedByID") as Label;

                Label lblAmendedRemarks = gvLOTTFList.Rows[rowindex].FindControl("lblAmendedRemarks") as Label;

                Label lblAttachment1 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment2") as Label;
                Label lblAttachment3 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment3") as Label;
                Label lblAttachment4 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment4") as Label;


                Label lblStandardDrawing = gvLOTTFList.Rows[rowindex].FindControl("lblStandardDrawing") as Label;
                Label lblStandardDrawingRemarks = gvLOTTFList.Rows[rowindex].FindControl("lblStandardDrawingRemarks") as Label;

                string standardDrawingExtn = string.Empty;

                Label lblIsSentForApproval = gvLOTTFList.Rows[rowindex].FindControl("lblIsSentForApproval") as Label;
                Label lblIsAmendedSentForApproval = gvLOTTFList.Rows[rowindex].FindControl("lblIsAmendedSentForApproval") as Label;

                Label lblClientApprovedDrawingName1 = gvLOTTFList.Rows[rowindex].FindControl("lblClientApprovedDrawingName1") as Label;
                Label lblClientApprovedDrawingName2 = gvLOTTFList.Rows[rowindex].FindControl("lblClientApprovedDrawingName2") as Label;
                Label lblClientApprovedDrawingName3 = gvLOTTFList.Rows[rowindex].FindControl("lblClientApprovedDrawingName3") as Label;


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


                pnlPrint.Visible = false;
                chkIsSendToPrint.Checked = false;
                pnlPrintSettings.Visible = false;

                hdStandardDrawingRemoveID.Value = "0";

                #region BUTTON CONTROLS

                if (Convert.ToString(e.CommandArgument) == "APPROVE" ||
                    Convert.ToString(e.CommandArgument) == "SEND_TO_FITUP_INSP" ||
                    Convert.ToString(e.CommandArgument) == "SEND_TO_FINAL_INSP" ||
                    Convert.ToString(e.CommandArgument) == "ACCEPT_ITEMS" ||
                    Convert.ToString(e.CommandArgument) == "COMPLETE_ITEMS")
                {
                    hdSubitemsCheckboxVisibility.Value = "1";

                    HideSubitemsPanel();
                    pnlUpdateStatus.Visible = true;
                    txtRemarksSI.Text = string.Empty;
                }


                ViewState["ACT"] = 0;


                ImageButton imgBtnEditLOT = gvLOTTFList.Rows[rowindex].FindControl("imgBtnEditLOT") as ImageButton;
                Button btnAmendLOT = gvLOTTFList.Rows[rowindex].FindControl("btnAmendLOT") as Button;
                Button btnApproveLOT = gvLOTTFList.Rows[rowindex].FindControl("btnApproveLOT") as Button;
                Button btnSendToIntlInsp = gvLOTTFList.Rows[rowindex].FindControl("btnSendToIntlInsp") as Button;
                Button btnSendToFinalInsp = gvLOTTFList.Rows[rowindex].FindControl("btnSendToFinalInsp") as Button;
                Button btnAcceptItems = gvLOTTFList.Rows[rowindex].FindControl("btnAcceptItems") as Button;
                Button btnFinalIsnpItems = gvLOTTFList.Rows[rowindex].FindControl("btnFinalIsnpItems") as Button;


                Session["UpdateSubitemStatusClicked"] = false;
                Session["SendToIntlInspectionClicked"] = false;


                Session["SendToFinalInspectionClicked"] = false;
                Session["SendToReworkClicked"] = false;

                Session["QaIntlAcceptClicked"] = false;
                Session["QaIntlNotAcceptClicked"] = false;
                Session["CompleteItemsClicked"] = false;
                Session["SendToAmendmentClicked"] = false;
                Session["SendToAmendmentByProductionClicked"] = false;
                Session["MultipleClicked"] = false;

                pnlQualityTeamResponsible.Visible = false;
                pnlAddStandardDrawing.Visible = false;
                pnlViewStandardDrawing.Visible = false;
                txtStandardDrawingRemarks.Text = string.Empty;
                imgBtnUndoStandardDrawing.Visible = false;

                hdQualityPersonCounts.Value = "0";
                hdQualityPersonViewFlag.Value = "0";

                if (Convert.ToString(e.CommandArgument) == "APPROVE")
                {
                    LOTMainSubitemIDs = string.Empty;
                    LOTTFSubitemIDs = string.Empty;

                    foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                        {
                            if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                                Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                            {
                                pnlAddStandardDrawing.Visible = true;
                                imgBtnUndoStandardDrawing.Visible = false;
                            }
                            else if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                                     Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted) ||
                                     Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                                     Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded) ||
                                     Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                                     Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                            {
                                if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                                     Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                                {
                                    imgBtnUndoStandardDrawing.Visible = true;
                                }

                                if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                                     Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                                {
                                    hdPrintFlag.Value = "1";
                                }

                                if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                                     Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                                {
                                    hdQualityPersonViewFlag.Value = "1";
                                    pnlQualityTeamResponsible.Visible = true;
                                    //BindQualityResponsibleTeamList();
                                }

                                pnlViewStandardDrawing.Visible = true;
                            }

                            string LOTMainsubitemID = Convert.ToString(dr1["LOT_MAIN_SUBITEM_ID"]);
                            string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                            if (!LOTMainSubitemIDs.Contains("," + LOTMainsubitemID + ","))
                            {
                                LOTMainSubitemIDs += LOTMainsubitemID + ",";
                            }

                            if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                            {
                                LOTTFSubitemIDs += LOTTFSubitemID + ",";
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                            {
                                string LOTMainsubitemID = Convert.ToString(dr1["LOT_MAIN_SUBITEM_ID"]);
                                string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                                if (!LOTMainSubitemIDs.Contains("," + LOTMainsubitemID + ","))
                                {
                                    LOTMainSubitemIDs += LOTMainsubitemID + ",";
                                }

                                if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                                {
                                    LOTTFSubitemIDs += LOTTFSubitemID + ",";
                                }
                            }
                        }
                        //}
                    }

                    if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                        LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    hdButtonFlag.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.Approve);

                    //BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(ViewState["PE_ID"]), Convert.ToInt32(ViewState["PM_ID"]), LOTMainSubitemIDs, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.Approve));
                    BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(lblcreatedByID.Text), Convert.ToInt32(lblJobPEID.Text), Convert.ToInt32(lblJobPMID.Text), LOTMainSubitemIDs, LOTTFSubitemIDs, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.Approve));

                    btnApproveLOT.Visible = false;
                    mpeSubitemDetail.Show();
                }


                else if (Convert.ToString(e.CommandArgument) == "EDIT")
                {
                    lblEditOrAmendText.Text = "Edit Notes:";
                    rdSavingType.Enabled = true;
                    rdSavingType.SelectedIndex = 0;
                    if (Convert.ToInt32(lblIsSentForApproval.Text) > 0) //|| Convert.ToInt32(lblIsAmendedSentForApproval.Text) > 0)
                    {
                        if (Convert.ToInt32(lblIsSentForApproval.Text) > 0)
                            rdSavingType.SelectedValue = Convert.ToString(lblIsSentForApproval.Text);

                        rdSavingType.Enabled = false;
                    }


                    hdRemovedSubitemIDs.Value = string.Empty;
                    HideSubitemsPanel();

                    if (Convert.ToInt32(lblAmendmentCounts.Text) == 0)
                        hdUpdationType.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Edit);
                    else
                    {
                        hdUpdationType.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Amend);//EditAmended

                        if (!string.IsNullOrEmpty(lblAmendedRemarks.Text))
                            txtAmendedRemarksToEdit.Text = lblAmendedRemarks.Text;
                        else txtAmendedRemarksToEdit.Text = "";
                    }

                    Reset();



                    BindLOTTFDetailsToEdit(LOTTFID);

                    imgBtnEditLOT.Visible = false;
                    mpeUpdateLOT.Show();
                }


                else if (Convert.ToString(e.CommandArgument) == "AMEND")
                {
                    lblEditOrAmendText.Text = "Amend Notes:";
                    rdSavingType.Enabled = true;
                    rdSavingType.SelectedIndex = 0;
                    if (Convert.ToInt32(lblIsSentForApproval.Text) > 0) //|| Convert.ToInt32(lblIsAmendedSentForApproval.Text) > 0)
                    {
                        if (Convert.ToInt32(lblIsSentForApproval.Text) > 0)
                        {
                            rdSavingType.SelectedValue = Convert.ToString(lblIsSentForApproval.Text);
                        }

                        //if (Convert.ToInt32(lblIsAmendedSentForApproval.Text) > 0)
                        //{
                        //    rdSavingType.SelectedValue = Convert.ToString(lblIsAmendedSentForApproval.Text);
                        //}

                        rdSavingType.Enabled = false;
                    }


                    hdRemovedSubitemIDs.Value = string.Empty;
                    HideSubitemsPanel();
                    hdUpdationType.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Amend);
                    Reset();

                    if (!string.IsNullOrEmpty(lblAmendedRemarks.Text))
                        txtAmendedRemarksToEdit.Text = lblAmendedRemarks.Text;
                    else txtAmendedRemarksToEdit.Text = "";

                    BindLOTTFDetailsToEdit(LOTTFID);

                    btnAmendLOT.Visible = false;
                    mpeUpdateLOT.Show();
                }


                else if (Convert.ToString(e.CommandArgument) == "SEND_TO_FITUP_INSP")
                {
                    pnlViewStandardDrawing.Visible = true;
                    LOTMainSubitemIDs = string.Empty;
                    LOTTFSubitemIDs = string.Empty;

                    foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                        {

                            if (Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                            {
                                string LOTMainsubitemID = Convert.ToString(dr1["LOT_MAIN_SUBITEM_ID"]);
                                string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                                if (!LOTMainSubitemIDs.Contains("," + LOTMainsubitemID + ","))
                                {
                                    LOTMainSubitemIDs += LOTMainsubitemID + ",";
                                }

                                if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                                {
                                    LOTTFSubitemIDs += LOTTFSubitemID + ",";
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                        LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');


                    hdButtonFlag.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFitupInsp);

                    //BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(ViewState["PE_ID"]), Convert.ToInt32(ViewState["PM_ID"]), LOTMainSubitemIDs, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.SendToIntlInsp));
                    BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(lblcreatedByID.Text), Convert.ToInt32(lblJobPEID.Text), Convert.ToInt32(lblJobPMID.Text), LOTMainSubitemIDs, LOTTFSubitemIDs,
                                                Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFitupInsp));

                    btnSendToIntlInsp.Visible = false;
                    mpeSubitemDetail.Show();
                }


                //else if (Convert.ToString(e.CommandArgument) == "SEND_TO_FINAL_INSP")
                //{
                //    pnlViewStandardDrawing.Visible = true;
                //    LOTMainSubitemIDs = string.Empty;
                //    LOTTFSubitemIDs = string.Empty;

                //    foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                //    {
                //        if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial) ||
                //            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                //            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                //        {

                //            if (Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                //            {
                //                string LOTMainsubitemID = Convert.ToString(dr1["LOT_MAIN_SUBITEM_ID"]);
                //                string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                //                if (!LOTMainSubitemIDs.Contains("," + LOTMainsubitemID + ","))
                //                {
                //                    LOTMainSubitemIDs += LOTMainsubitemID + ",";
                //                }

                //                if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                //                {
                //                    LOTTFSubitemIDs += LOTTFSubitemID + ",";
                //                }
                //            }
                //        }
                //    }

                //    if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                //        LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

                //    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                //        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');


                //    hdButtonFlag.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.SendToIntlInsp);

                //    //BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(ViewState["PE_ID"]), Convert.ToInt32(ViewState["PM_ID"]), LOTMainSubitemIDs, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.SendToIntlInsp));
                //    BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(lblcreatedByID.Text), Convert.ToInt32(lblJobPEID.Text), Convert.ToInt32(lblJobPMID.Text), LOTMainSubitemIDs, LOTTFSubitemIDs, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.SendToIntlInsp));

                //    btnSendToIntlInsp.Visible = false;
                //    mpeSubitemDetail.Show();
                //}


                else if (Convert.ToString(e.CommandArgument) == "SEND_TO_FINAL_INSP")
                {
                    pnlViewStandardDrawing.Visible = true;

                    ViewState["ACT"] = Convert.ToInt32(LOTAllStatusAndTypes.ButtonClicked.AcceptItems);


                    LOTMainSubitemIDs = string.Empty;
                    LOTTFSubitemIDs = string.Empty;

                    foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                        {

                            if (Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                            {
                                string LOTMainsubitemID = Convert.ToString(dr1["LOT_MAIN_SUBITEM_ID"]);
                                string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                                if (!LOTMainSubitemIDs.Contains("," + LOTMainsubitemID + ","))
                                {
                                    LOTMainSubitemIDs += LOTMainsubitemID + ",";
                                }

                                if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                                {
                                    LOTTFSubitemIDs += LOTTFSubitemID + ",";
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                        LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    hdButtonFlag.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFinalInsp);

                    //BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(ViewState["PE_ID"]), Convert.ToInt32(ViewState["PM_ID"]), LOTMainSubitemIDs,Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.CompleteItems));
                    BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(lblcreatedByID.Text), Convert.ToInt32(lblJobPEID.Text),
                                                Convert.ToInt32(lblJobPMID.Text), LOTMainSubitemIDs, LOTTFSubitemIDs,
                                                Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFinalInsp));

                    btnSendToFinalInsp.Visible = false;
                    mpeSubitemDetail.Show();
                }



                else if (Convert.ToString(e.CommandArgument) == "ACCEPT_ITEMS")
                {
                    pnlViewStandardDrawing.Visible = true;

                    ViewState["ACT"] = Convert.ToInt32(LOTAllStatusAndTypes.ButtonClicked.AcceptItems);


                    LOTMainSubitemIDs = string.Empty;
                    LOTTFSubitemIDs = string.Empty;

                    foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection))
                        {

                            if (Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                            {
                                string LOTMainsubitemID = Convert.ToString(dr1["LOT_MAIN_SUBITEM_ID"]);
                                string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                                if (!LOTMainSubitemIDs.Contains("," + LOTMainsubitemID + ","))
                                {
                                    LOTMainSubitemIDs += LOTMainsubitemID + ",";
                                }

                                if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                                {
                                    LOTTFSubitemIDs += LOTTFSubitemID + ",";
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                        LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    hdButtonFlag.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.AcceptItems);

                    //BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(ViewState["PE_ID"]), Convert.ToInt32(ViewState["PM_ID"]), LOTMainSubitemIDs, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.AcceptItems));
                    BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(lblcreatedByID.Text), Convert.ToInt32(lblJobPEID.Text), Convert.ToInt32(lblJobPMID.Text), LOTMainSubitemIDs, LOTTFSubitemIDs, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.AcceptItems));


                    btnAcceptItems.Visible = false;
                    mpeSubitemDetail.Show();

                }


                else if (Convert.ToString(e.CommandArgument) == "COMPLETE_ITEMS")
                {
                    pnlViewStandardDrawing.Visible = true;

                    ViewState["ACT"] = Convert.ToInt32(LOTAllStatusAndTypes.ButtonClicked.AcceptItems);


                    LOTMainSubitemIDs = string.Empty;
                    LOTTFSubitemIDs = string.Empty;

                    foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial) ||
                            Convert.ToInt32(dr1["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection))
                        {

                            if (Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                            {
                                string LOTMainsubitemID = Convert.ToString(dr1["LOT_MAIN_SUBITEM_ID"]);
                                string LOTTFSubitemID = Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]);

                                if (!LOTMainSubitemIDs.Contains("," + LOTMainsubitemID + ","))
                                {
                                    LOTMainSubitemIDs += LOTMainsubitemID + ",";
                                }

                                if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                                {
                                    LOTTFSubitemIDs += LOTTFSubitemID + ",";
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                        LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    hdButtonFlag.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.CompleteItems);

                    //BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(ViewState["PE_ID"]), Convert.ToInt32(ViewState["PM_ID"]), LOTMainSubitemIDs,Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.CompleteItems));
                    BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(lblcreatedByID.Text), Convert.ToInt32(lblJobPEID.Text), Convert.ToInt32(lblJobPMID.Text), LOTMainSubitemIDs, LOTTFSubitemIDs, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.CompleteItems));

                    btnFinalIsnpItems.Visible = false;
                    mpeSubitemDetail.Show();
                }


                #endregion


                else if (Convert.ToString(e.CommandArgument) == "ViewSTANDARDDRAWING")
                    ViewDrawingFiles(LOTTFID, "STANDARD_DRAWING", Convert.ToString(lblStandardDrawing.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                    ViewDrawingFiles(LOTTFID, "DRAWING1", Convert.ToString(lblAttachment1.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                    ViewDrawingFiles(LOTTFID, "DRAWING2", Convert.ToString(lblAttachment2.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                    ViewDrawingFiles(LOTTFID, "DRAWING3", Convert.ToString(lblAttachment3.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                    ViewDrawingFiles(LOTTFID, "DRAWING4", Convert.ToString(lblAttachment4.Text).Trim(), 0);




                //Send missed mail

                else if (Convert.ToString(e.CommandArgument) == "SEND_APPROVAL_MAIL")
                {
                    int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    int partialStatusID = 0;
                    int amendmentByProdFlag = 0;
                    int qaIntlInspFlag = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + LOTTFID + "'"))
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                        {
                            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);

                            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);

                            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);

                            //New
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                                currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                            {
                                if (currentLoginID == createdByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                                if (currentLoginID == amendedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            }



                            //Amendment
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                            {
                                if (currentLoginID == amendmentByID && currentLoginID != productionManagerID)
                                {
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                                    amendmentByProdFlag = 0;
                                    if (dr1["AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr1["AMENDMENT_REMARKS"])))
                                        remarks = Convert.ToString(dr1["AMENDMENT_REMARKS"]);
                                }

                                if (currentLoginID == productionManagerID)//currentLoginID == amendmentByID && 
                                {
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                                    amendmentByProdFlag = 1;
                                    if (dr1["PROD_AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr1["PROD_AMENDMENT_REMARKS"])))
                                        remarks = Convert.ToString(dr1["PROD_AMENDMENT_REMARKS"]);
                                }
                            }



                            //Approved
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))//acceptance
                            {
                                if (currentLoginID == approvedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                                if (currentLoginID == amendedApprovedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            }



                            //Planning Accepted
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                            {
                                if (currentLoginID == planningAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                                if (currentLoginID == amendedPlanningAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            }



                            //Planning Forwarded
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                            {
                                if (currentLoginID == forwardedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                                if (currentLoginID == amendedForwardedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            }



                            //Production-Accepted
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                            {
                                if (currentLoginID == prodAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                                if (currentLoginID == amendedAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            }



                            //QA-LOT Accepted
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                            {
                                if (currentLoginID == qualityAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";

                                if (currentLoginID == amendedQualityAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            }

                            //Complete
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                            {
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    //Complete
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_COMPLETED_MAIL_SENT='0'"))
                                    {
                                        if (currentLoginID == Convert.ToInt32(dr3["COMPLETED_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";

                                            if (dr3["IRN_ATTACHMENT_DOC"] != DBNull.Value)
                                            {
                                                IRNAttachmentFileBytes = (byte[])dr3["IRN_ATTACHMENT_DOC"];
                                            }
                                            else
                                            {
                                                IRNAttachmentFileBytes = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    int sendMailValue = 0;
                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, lblTFNo.Text, companyID
                        , remarks, qaIntlInspFlag, 0, IRNAttachmentFileBytes, partialQuantityFlag, amendmentByProdFlag, 0);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, currentStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("Mail sent successfully.");
                        GetLOTList();
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_FITUP_INSP_MAIL")
                {
                    int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    int partialStatusID = 0;
                    int amendmentByProdFlag = 0;
                    int qaIntlInspFlag = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + LOTTFID + "'"))
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                        {
                            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);
                            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);
                            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);


                            qaIntlInspFlag = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);

                            //FitupInspection
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection))
                            {
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    //Fitup Inspection
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_SENT_TO_INTL_INSP_MAIL_SENT='0'"))
                                    {
                                        if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_INTL_INSP_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                            }

                            //Partial
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                            {
                                partialQuantityFlag = 1;
                                //currentStatusID = partialStatusID;
                                //if (partialStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFitupInspection))
                                //{

                                //Fitup Inspection
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_INTERNAL_INSPECTION_MAIL_SENT='0'"))
                                    {
                                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFitupInspection);
                                        if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_INTL_INSP_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                                //}
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    int sendMailValue = 1;
                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, lblTFNo.Text
                        , companyID, remarks, qaIntlInspFlag, 0, IRNAttachmentFileBytes, partialQuantityFlag, amendmentByProdFlag, 0);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, currentStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("Mail sent successfully.");
                        GetLOTList();
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_TO_FINAL_INSP_MAIL")
                {
                    int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    int partialStatusID = 0;
                    int amendmentByProdFlag = 0;
                    int qaIntlInspFlag = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + LOTTFID + "'"))
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                        {
                            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);
                            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);
                            sentToFinalInspectionByID = Convert.ToInt32(dr1["SENT_TO_FINAL_INSP_BY"]);
                            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);


                            qaIntlInspFlag = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection);

                            //FinalInspection
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection))
                            {
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    //Final Inspection
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_SENT_TO_FINAL_INSP_MAIL_SENT='0'"))
                                    {
                                        if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_FINAL_INSP_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                            }

                            //Partial
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                            {
                                partialQuantityFlag = 1;
                                //currentStatusID = partialStatusID;
                                //if (partialStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFinalInspection))
                                //{

                                //Final Inspection
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_IN_FINAL_INSPECTION_MAIL_SENT='0'"))
                                    {
                                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFinalInspection);
                                        if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_FINAL_INSP_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                                //}
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    int sendMailValue = 0;
                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, lblTFNo.Text
                        , companyID, remarks, qaIntlInspFlag, 0, IRNAttachmentFileBytes, partialQuantityFlag, amendmentByProdFlag, 0);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, currentStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        SuccessMessage("Mail sent successfully.");
                        GetLOTList();
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_ACCEPT_ITEMS_MAIL")
                {
                    int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    int partialStatusID = 0;
                    int amendmentByProdFlag = 0;
                    int qaIntlInspFlag = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + LOTTFID + "'"))
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                        {
                            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);
                            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);
                            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);


                            qaIntlInspFlag = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted);

                            //QAItemAccepted
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                            {
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    //Item-Accepted
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_QA_INTL_INSP_ACCEPTED_MAIL_SENT='0'"))
                                    {
                                        if (currentLoginID == Convert.ToInt32(dr3["QA_INTL_INSP_ACCEPTED_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                            }

                            //Partial
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                            {
                                partialQuantityFlag = 1;
                                //currentStatusID = partialStatusID;
                                //if (partialStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemAccepted))
                                //{

                                //Item-Accepted
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_QA_ITEM_ACCEPTED_MAIL_SENT='0'"))
                                    {
                                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemAccepted);
                                        if (currentLoginID == Convert.ToInt32(dr3["QA_INTL_INSP_ACCEPTED_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                                //}
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    int sendMailValue = 0;
                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, lblTFNo.Text, companyID
                        , remarks, qaIntlInspFlag, 0, IRNAttachmentFileBytes, partialQuantityFlag, amendmentByProdFlag, 0);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, currentStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("Mail sent successfully.");
                        GetLOTList();
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_NOT_ACCEPT_ITEMS_MAIL")
                {
                    int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    int partialStatusID = 0;
                    int amendmentByProdFlag = 0;
                    int qaIntlInspFlag = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + LOTTFID + "'"))
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                        {
                            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);
                            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);
                            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);

                            qaIntlInspFlag = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted);

                            //QAItemNotAccepted
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted))
                            {
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    //Item-Not-Accepted
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_QA_INTL_INSP_NOT_ACCEPTED_MAIL_SENT='0'"))
                                    {
                                        if (currentLoginID == Convert.ToInt32(dr3["QA_INTL_INSP_NOT_ACCEPTED_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                            }

                            //Partial
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                            {
                                partialQuantityFlag = 1;
                                //currentStatusID = partialStatusID;
                                //if (partialStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemNotAccepted))
                                //{

                                //Item-Not-Accepted
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_QA_ITEM_NOT_ACCEPTED_MAIL_SENT='0'"))
                                    {
                                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemNotAccepted);
                                        if (currentLoginID == Convert.ToInt32(dr3["QA_INTL_INSP_NOT_ACCEPTED_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                                //}
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    int sendMailValue = 0;
                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, lblTFNo.Text
                        , companyID, remarks, qaIntlInspFlag, 0, IRNAttachmentFileBytes, partialQuantityFlag, amendmentByProdFlag, 0);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, currentStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("Mail sent successfully.");
                        GetLOTList();
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_REWORK_MAIL")
                {
                    int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    int partialStatusID = 0;
                    int amendmentByProdFlag = 0;
                    int qaIntlInspFlag = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + LOTTFID + "'"))
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                        {
                            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);
                            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);
                            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                            sentToReworkByID = Convert.ToInt32(dr1["SENT_TO_REWORK_BY"]);
                            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);

                            qaIntlInspFlag = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework);

                            //Rework
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework))
                            {
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    //Rework
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_SENT_TO_REWORK_MAIL_SENT='0'"))
                                    {
                                        if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_REWORK_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                            }

                            //Partial
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                            {
                                partialQuantityFlag = 1;
                                //currentStatusID = partialStatusID;
                                //if (partialStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialRework))
                                //{

                                //Rework
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_IN_REWORK_MAIL_SENT='0'"))
                                    {
                                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialRework);
                                        if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_REWORK_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";
                                        }
                                    }
                                }
                                //}
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    int sendMailValue = 0;
                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, lblTFNo.Text, companyID
                        , remarks, qaIntlInspFlag, 0, IRNAttachmentFileBytes, partialQuantityFlag, amendmentByProdFlag, 0);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, currentStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("Mail sent successfully.");
                        GetLOTList();
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_FINAL_INSP_ITEMS_MAIL")
                {
                    int currentLoginID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    int partialStatusID = 0;
                    int amendmentByProdFlag = 0;
                    int qaIntlInspFlag = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + LOTTFID + "'"))
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                        {
                            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);
                            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);
                            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);

                            qaIntlInspFlag = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);

                            //Complete
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                            {
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    //Complete
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_COMPLETED_MAIL_SENT='0'"))
                                    {
                                        if (currentLoginID == Convert.ToInt32(dr3["COMPLETED_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";

                                            if (dr3["IRN_ATTACHMENT_DOC"] != DBNull.Value)
                                            {
                                                IRNAttachmentFileBytes = (byte[])dr3["IRN_ATTACHMENT_DOC"];
                                            }
                                            else
                                            {
                                                IRNAttachmentFileBytes = null;
                                            }
                                        }
                                    }
                                }
                            }



                            //Partial
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                            {
                                partialQuantityFlag = 1;
                                //currentStatusID = partialStatusID;
                                //if (partialStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialComplete))
                                //{

                                //Complete
                                foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                {
                                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_COMPLETED_MAIL_SENT='0'"))
                                    {
                                        currentStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialComplete);
                                        if (currentLoginID == Convert.ToInt32(dr3["COMPLETED_BY"]))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(dr3["LOT_TF_SUBITEM_ID"]) + ",";

                                            if (dr3["IRN_ATTACHMENT_DOC"] != DBNull.Value)
                                            {
                                                IRNAttachmentFileBytes = (byte[])dr3["IRN_ATTACHMENT_DOC"];
                                            }
                                            else
                                            {
                                                IRNAttachmentFileBytes = null;
                                            }
                                        }
                                    }
                                }
                                //}
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    int sendMailValue = 0;
                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, lblTFNo.Text
                        , companyID, remarks, qaIntlInspFlag, 0, IRNAttachmentFileBytes, partialQuantityFlag, amendmentByProdFlag, 0);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, currentStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("Mail sent successfully.");
                        GetLOTList();
                    }
                }





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

                                //if (!LOTTFSubitemIDs.Contains(Convert.ToString(dr1["LOT_TF_SUBITEM_ID"])))
                                //{
                                //    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                                //}
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

                            //if (!LOTTFSubitemIDs.Contains(Convert.ToString(dr1["LOT_TF_SUBITEM_ID"])))
                            //{
                            //    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            //}
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    ModalPopupExtender4.Show();
                    iframeViewTravelStatementInPDF.Attributes.Add("src", "LOTTransmittalFactoryInPDF.aspx?LOTTFID=" + LOTTFID + "&LOTTFSubitemIDs=" + LOTTFSubitemIDs + "&pdfType=" + (int)LOTAllStatusAndTypes.EnumPDFType.List);
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewSubitemDETAIL")
                {
                    hdSubitemsCheckboxVisibility.Value = "0";

                    HideSubitemsPanel();
                    //tblRemarks.Visible = false;
                    pnlUpdateStatus.Visible = false;
                    BindLOTSubitemDetails(LOTTFID, "", "");
                    mpeSubitemDetail.Show();
                }

                else if (
                    Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING1" ||
                    Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING2" ||
                    Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING3"
                    )
                {
                    if (Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING1")
                        lblSeqNo.Text = "1";
                    else if (Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING2")
                        lblSeqNo.Text = "2";
                    else if (Convert.ToString(e.CommandArgument) == "ADD_CLIENT_APPROVED_DRAWING3")
                        lblSeqNo.Text = "3";

                    txtTFNoInCAD.Text = lblTFNo.Text;
                    txtJOBNumberInCAD.Text = lblJOBNo.Text;
                    txtUnitInCAD.Text = lblUnitName.Text;
                    txtCustomerCodeInCAD.Text = lblCustomerCode.Text;
                    txtCustomerNameInCAD.Text = lblCustomerName.Text;
                    txtCustomerPONoInCAD.Text = lblPONo.Text;
                    txtItemInCAD.Text = lblItemName.Text;
                    mpeAddClientApprovedDrawing.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewClientApprovedDrg1")
                {
                    ViewDrawingFiles(LOTTFID, "CLIENT_APPROVED_DRAWING1", Convert.ToString(lblClientApprovedDrawingName1.Text).Trim(), 0);
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewClientApprovedDrg2")
                {
                    ViewDrawingFiles(LOTTFID, "CLIENT_APPROVED_DRAWING2", Convert.ToString(lblClientApprovedDrawingName2.Text).Trim(), 0);
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewClientApprovedDrg3")
                {
                    ViewDrawingFiles(LOTTFID, "CLIENT_APPROVED_DRAWING3", Convert.ToString(lblClientApprovedDrawingName3.Text).Trim(), 0);
                }

                else if (
                    Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL1" ||
                    Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL2" ||
                    Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL3"
                    )
                {
                    int seqNo = 0;
                    if (Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL1")
                    {
                        seqNo = 1;
                    }
                    else if (Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL2")
                    {
                        seqNo = 2;
                    }
                    else if (Convert.ToString(e.CommandArgument) == "SEND_CLIENT_APPROVED_DRAWING_MAIL3")
                    {
                        seqNo = 3;
                    }
                    int sendMailValue = SendClientApprovedDrawingEmail(LOTTFID, seqNo);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateClientApprovedDrawingMailStatus(LOTTFID, Convert.ToInt32(lblSeqNo.Text), Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("Client approved drawing saved with TF. No.: '" + lblTFNo.Text + "' and mail sent successfully.");
                        ResetClientApprovedDrawing();
                    }
                    else
                    {
                        SuccessMessage("Client approved drawing saved with TF. No.: '" + lblTFNo.Text + "' successfully.");
                        ResetClientApprovedDrawing();
                    }


                    GetLOTList();
                }

                txtViewStandardDrawing.Text = string.Empty;
                txtViewStandardDrawingReason.Text = string.Empty;
                if (!string.IsNullOrEmpty(lblStandardDrawing.Text))
                {
                    pnlViewStandardDrawing.Visible = true;
                    imgBtnViewStandardDrawing.Visible = true;
                    imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;

                    txtViewStandardDrawing.Text = lblStandardDrawing.Text;
                    txtViewStandardDrawingReason.Text = lblStandardDrawingRemarks.Text;

                    standardDrawingExtn = Convert.ToString(lblStandardDrawing.Text).Split('.').Last();
                    if (standardDrawingExtn == "jpg" ||
                        standardDrawingExtn == "jepg" ||
                        standardDrawingExtn == "bmp" ||
                        standardDrawingExtn == "png" ||
                        standardDrawingExtn == "gif" ||
                        standardDrawingExtn == "JPG" ||
                        standardDrawingExtn == "JPEG" ||
                        standardDrawingExtn == "BMP" ||
                        standardDrawingExtn == "PNG" ||
                        standardDrawingExtn == "GIF")
                    {
                        imgBtnViewStandardDrawing.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;
                    }
                    else if (standardDrawingExtn == "pdf" || standardDrawingExtn == "PDF")
                    {
                        imgBtnViewStandardDrawing.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;
                    }
                    else if (standardDrawingExtn == "dxf" || standardDrawingExtn == "DXF")
                    {
                        imgBtnViewStandardDrawing.ImageUrl = "~/Images/LOT/dxf.png";
                        imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;
                    }
                    else if (standardDrawingExtn == "dwg" || standardDrawingExtn == "DWG")
                    {
                        imgBtnViewStandardDrawing.ImageUrl = "~/Images/LOT/dwg.png";
                        imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;
                    }
                }
                else
                {
                    pnlViewStandardDrawing.Visible = false;
                    imgBtnViewStandardDrawing.Visible = false;
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
                    if (i < 10 || i > 12)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                    }
                }
            }



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool planningAccepted = false;
                bool productionAccepted = false;
                bool QAItemAccepted = false;

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

                Label lblSubitemCounts = (Label)e.Row.FindControl("lblSubitemCounts");
                Label lblSubitemsCountV = (Label)e.Row.FindControl("lblSubitemsCountV");

                Label lblIsSentForApproval = (Label)e.Row.FindControl("lblIsSentForApproval");
                Label lblIsAmendedSentForApproval = (Label)e.Row.FindControl("lblIsAmendedSentForApproval");


                Label lblIsTransferred = (Label)e.Row.FindControl("lblIsTransferred");
                Label lblOldTransferredLOTTFId = (Label)e.Row.FindControl("lblOldTransferredLOTTFId");
                Label lblOldTransferredLOTTFCreatedBy = (Label)e.Row.FindControl("lblOldTransferredLOTTFCreatedBy");

                int oldTransferredLOTTFCreatedBy = 0;
                if (!string.IsNullOrEmpty(lblOldTransferredLOTTFCreatedBy.Text))
                    oldTransferredLOTTFCreatedBy = Convert.ToInt32(lblOldTransferredLOTTFCreatedBy.Text);


                Label lblClientApprovedDrawingName1 = (Label)e.Row.FindControl("lblClientApprovedDrawingName1");
                Label lblClientApprovedDrawingName2 = (Label)e.Row.FindControl("lblClientApprovedDrawingName2");
                Label lblClientApprovedDrawingName3 = (Label)e.Row.FindControl("lblClientApprovedDrawingName3");

                Label lblClientApprovedDrawingSavedBy1 = (Label)e.Row.FindControl("lblClientApprovedDrawingSavedBy1");
                Label lblClientApprovedDrawingSavedBy2 = (Label)e.Row.FindControl("lblClientApprovedDrawingSavedBy2");
                Label lblClientApprovedDrawingSavedBy3 = (Label)e.Row.FindControl("lblClientApprovedDrawingSavedBy3");

                Label lblIsClientApprovedDrawingMailSent1 = (Label)e.Row.FindControl("lblIsClientApprovedDrawingMailSent1");
                Label lblIsClientApprovedDrawingMailSent2 = (Label)e.Row.FindControl("lblIsClientApprovedDrawingMailSent2");
                Label lblIsClientApprovedDrawingMailSent3 = (Label)e.Row.FindControl("lblIsClientApprovedDrawingMailSent3");

                if (Convert.ToInt32(lblSubitemCounts.Text) < 10)
                    lblSubitemsCountV.Text = "0" + lblSubitemCounts.Text;
                else lblSubitemsCountV.Text = lblSubitemCounts.Text;


                if (Session["dsLOTList"] != null)
                    dsLOTList = (DataSet)Session["dsLOTList"];


                ImageButton imgBtnViewClientApprovedDrg1 = (ImageButton)e.Row.FindControl("imgBtnViewClientApprovedDrg1");
                ImageButton imgBtnViewClientApprovedDrg2 = (ImageButton)e.Row.FindControl("imgBtnViewClientApprovedDrg2");
                ImageButton imgBtnViewClientApprovedDrg3 = (ImageButton)e.Row.FindControl("imgBtnViewClientApprovedDrg3");

                ImageButton imgBtnAddClientApprovedDrawing1 = (ImageButton)e.Row.FindControl("imgBtnAddClientApprovedDrawing1");
                ImageButton imgBtnAddClientApprovedDrawing2 = (ImageButton)e.Row.FindControl("imgBtnAddClientApprovedDrawing2");
                ImageButton imgBtnAddClientApprovedDrawing3 = (ImageButton)e.Row.FindControl("imgBtnAddClientApprovedDrawing3");


                ImageButton imgBtnSendClientApprovedDrawingMail1 = (ImageButton)e.Row.FindControl("imgBtnSendClientApprovedDrawingMail1");
                ImageButton imgBtnSendClientApprovedDrawingMail2 = (ImageButton)e.Row.FindControl("imgBtnSendClientApprovedDrawingMail2");
                ImageButton imgBtnSendClientApprovedDrawingMail3 = (ImageButton)e.Row.FindControl("imgBtnSendClientApprovedDrawingMail3");



                imgBtnViewClientApprovedDrg1.Visible = false;
                imgBtnAddClientApprovedDrawing1.Visible = false;
                imgBtnSendClientApprovedDrawingMail1.Visible = false;
                if (Convert.ToInt32(lblClientApprovedDrawingSavedBy1.Text) > 0)
                {
                    imgBtnViewClientApprovedDrg1.Visible = true;
                    imgBtnAddClientApprovedDrawing1.Visible = false;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblClientApprovedDrawingSavedBy1.Text))
                    {
                        if (Convert.ToInt32(lblIsClientApprovedDrawingMailSent1.Text) == 0)
                        {
                            imgBtnSendClientApprovedDrawingMail1.Visible = true;
                        }
                    }
                }
                else
                {
                    imgBtnViewClientApprovedDrg1.Visible = false;
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblcreatedByID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblJobPEID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblJobPMID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == oldTransferredLOTTFCreatedBy)
                    {
                        imgBtnAddClientApprovedDrawing1.Visible = true;
                    }
                }



                imgBtnViewClientApprovedDrg2.Visible = false;
                imgBtnAddClientApprovedDrawing2.Visible = false;
                imgBtnSendClientApprovedDrawingMail2.Visible = false;
                if (Convert.ToInt32(lblClientApprovedDrawingSavedBy2.Text) > 0)
                {
                    imgBtnViewClientApprovedDrg2.Visible = true;
                    imgBtnAddClientApprovedDrawing2.Visible = false;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblClientApprovedDrawingSavedBy2.Text))
                    {
                        if (Convert.ToInt32(lblIsClientApprovedDrawingMailSent2.Text) == 0)
                        {
                            imgBtnSendClientApprovedDrawingMail2.Visible = true;
                        }
                    }
                }
                else
                {
                    imgBtnViewClientApprovedDrg2.Visible = false;
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblcreatedByID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblJobPEID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblJobPMID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == oldTransferredLOTTFCreatedBy)
                    {
                        imgBtnAddClientApprovedDrawing2.Visible = true;
                    }
                }



                imgBtnViewClientApprovedDrg3.Visible = false;
                imgBtnAddClientApprovedDrawing3.Visible = false;
                imgBtnSendClientApprovedDrawingMail3.Visible = false;
                if (Convert.ToInt32(lblClientApprovedDrawingSavedBy3.Text) > 0)
                {
                    imgBtnViewClientApprovedDrg3.Visible = true;
                    imgBtnAddClientApprovedDrawing3.Visible = false;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblClientApprovedDrawingSavedBy3.Text))
                    {
                        if (Convert.ToInt32(lblIsClientApprovedDrawingMailSent3.Text) == 0)
                        {
                            imgBtnSendClientApprovedDrawingMail3.Visible = true;
                        }
                    }
                }
                else
                {
                    imgBtnViewClientApprovedDrg3.Visible = false;
                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblcreatedByID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblJobPEID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(lblJobPMID.Text) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == oldTransferredLOTTFCreatedBy)
                    {
                        imgBtnAddClientApprovedDrawing3.Visible = true;
                    }
                }








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



                int IsApprovalMailSent = 0;
                int IsApprovedMailSent = 0;
                int IsPlanningAcceptedMailSent = 0;
                int IsForwardedMailSent = 0;
                int IsAcceptedMailSent = 0;
                int IsQualityAcceptedMailSent = 0;
                int IsSentToIntlInspMailSent = 0;

                int IsSentToFinalInspMailSent = 0;
                int IsSentToReworkMailSent = 0;

                int IsQaIntlInspAcceptedMailSent = 0;
                int IsQaIntlInspNotAcceptedMailSent = 0;
                int IsAmendmentMailSent = 0;
                int IsProdAmendmentMailSent = 0;
                int IsAmendedMailSent = 0;
                int IsAmendedApprovedMailSent = 0;
                int IsAmendedPlanningAcceptedMailSent = 0;
                int IsAmendedForwardedMailSent = 0;
                int IsAmendedAcceptedMailSent = 0;
                int IsAmendedQualityAcceptedMailSent = 0;
                int IsCompletedMailSent = 0;

                int IsPartialInternalInspectionMailSent = 0;

                int IsPartialFinalInspectionMailSent = 0;
                int IsPartialReworkMailSent = 0;

                int IsPartialQaItemAcceptedMailSent = 0;
                int IsPartialQaItemNotAcceptedMailSent = 0;
                int IsPartialCompletedMailSent = 0;

                //bool isTransferred = false;
                //int oldTransferredLOTTFId = 0;
                //int oldTransferredLOTTFCreatedBy = 0;

                statusID = 0;
                int statusCount = 0;

                //Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Transferred)
                if (dsLOTList.Tables.Count > 0 && dsLOTList.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                            Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted)

                            )
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

                            //else if (Convert.ToInt32(dr3["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Transferred))
                            //{
                            //    txtStatus.Text = "TR";
                            //    txtStatus.ToolTip = "Transferred";
                            //}

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
                        }

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


                            if (intlInspectionCount > 0 ||
                                QAItemAcceptedCount > 0 ||
                                QQAItemNotAcceptedCount > 0 ||
                                finalInspectionCount > 0 ||
                                reworkCount > 0 ||
                                completeCount > 0)
                            {
                                statusCount++;
                            }
                        }

                        txtStatus.Text = txtStatus.Text.TrimEnd(',');
                        txtStatus.ToolTip = txtStatus.ToolTip.TrimEnd('\n').TrimEnd(',');
                        txtStatus.ForeColor = System.Drawing.Color.White;

                        break;
                    }

                    if (statusCount > 0)
                    {
                        if (totalQuantity == completeCount)
                        {
                            txtStatus.Text = "COM";
                            txtStatus.ToolTip = "Completed by Quality after Final Inspection";
                        }
                        else
                        {
                            int incompleteQty = totalQuantity - completeCount;
                            txtStatus.Text = "INCOM-" + incompleteQty;
                            txtStatus.ToolTip = "Incomplete for Final Inspection";
                        }
                    }

                    #endregion



                    #region  BUTTON CONTROLS


                    ImageButton imgBtnEditLOT = (ImageButton)e.Row.FindControl("imgBtnEditLOT");

                    //ImageButton imgbtnSendMail = (ImageButton)e.Row.FindControl("imgbtnSendMail");

                    Button btnSendApprovalMail = (Button)e.Row.FindControl("btnSendApprovalMail");
                    Button btnSendToIntlInspSendMail = (Button)e.Row.FindControl("btnSendToIntlInspSendMail");

                    Button btnSendToFinalInspSendMail = (Button)e.Row.FindControl("btnSendToFinalInspSendMail");
                    Button btnSendToReworkSendMail = (Button)e.Row.FindControl("btnSendToReworkSendMail");

                    Button btnAcceptItemsSendMail = (Button)e.Row.FindControl("btnAcceptItemsSendMail");
                    Button btnNotAcceptItemsSendMail = (Button)e.Row.FindControl("btnNotAcceptItemsSendMail");
                    Button btnFinalIsnpItemsSendMail = (Button)e.Row.FindControl("btnFinalIsnpItemsSendMail");




                    Button btnAmendLOT = (Button)e.Row.FindControl("btnAmendLOT");
                    Button btnApproveLOT = (Button)e.Row.FindControl("btnApproveLOT");
                    Button btnSendToIntlInsp = (Button)e.Row.FindControl("btnSendToIntlInsp");
                    Button btnSendToFinalInsp = (Button)e.Row.FindControl("btnSendToFinalInsp");

                    Button btnAcceptItems = (Button)e.Row.FindControl("btnAcceptItems");
                    Button btnFinalIsnpItems = (Button)e.Row.FindControl("btnFinalIsnpItems");

                    companyID = Convert.ToInt32(lblUnitID.Text);
                    PEID = Convert.ToInt32(lblJobPEID.Text);
                    PMID = Convert.ToInt32(lblJobPMID.Text);

                    imgBtnEditLOT.Visible = false;
                    //imgbtnSendMail.Visible = false;

                    btnSendApprovalMail.Visible = false;
                    btnSendToIntlInspSendMail.Visible = false;

                    btnSendToFinalInspSendMail.Visible = false;
                    btnSendToReworkSendMail.Visible = false;

                    btnAcceptItemsSendMail.Visible = false;
                    btnNotAcceptItemsSendMail.Visible = false;
                    btnFinalIsnpItemsSendMail.Visible = false;


                    btnAmendLOT.Visible = false;
                    btnApproveLOT.Visible = false;
                    btnSendToIntlInsp.Visible = false;
                    btnSendToFinalInsp.Visible = false;
                    btnAcceptItems.Visible = false;
                    btnFinalIsnpItems.Visible = false;

                    planningAccepted = false;
                    productionAccepted = false;

                    int partialStatusID = 0;
                    int checkForMailSentFlag = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[3].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                        {
                            checkForMailSentFlag = 0;
                            LOTTFID = 0;
                            LOTMainSubItemID = 0;
                            quantity = 0;
                            currentStatusID = 0;
                            partialStatusID = 0;
                            productionManagerID = 0;

                            createdByID = 0;
                            approvedByID = 0;
                            prodAcceptedByID = 0;
                            sentToIntlInspectionByID = 0;
                            qaIntlInspAcceptedByID = 0;
                            qaIntlInspNotAcceptedByID = 0;
                            completedByID = 0;
                            qualityAcceptedByID = 0;
                            planningAcceptedByID = 0;
                            forwardedByID = 0;
                            amendmentCount = 0;
                            amendmentByID = 0;
                            prodAmendmentByID = 0;
                            amendedByID = 0;
                            amendedApprovedByID = 0;
                            amendedAcceptedByID = 0;
                            amendedQualityAcceptedByID = 0;
                            amendedPlanningAcceptedByID = 0;
                            amendedForwardedByID = 0;
                            isPartOfProductionStatusID = 0;



                            IsApprovalMailSent = 0;
                            IsApprovedMailSent = 0;
                            IsPlanningAcceptedMailSent = 0;
                            IsForwardedMailSent = 0;
                            IsAcceptedMailSent = 0;
                            IsQualityAcceptedMailSent = 0;
                            IsSentToIntlInspMailSent = 0;

                            IsSentToFinalInspMailSent = 0;
                            IsSentToReworkMailSent = 0;

                            IsQaIntlInspAcceptedMailSent = 0;
                            IsQaIntlInspNotAcceptedMailSent = 0;
                            IsAmendmentMailSent = 0;
                            IsProdAmendmentMailSent = 0;
                            IsAmendedMailSent = 0;
                            IsAmendedApprovedMailSent = 0;
                            IsAmendedPlanningAcceptedMailSent = 0;
                            IsAmendedForwardedMailSent = 0;
                            IsAmendedAcceptedMailSent = 0;
                            IsAmendedQualityAcceptedMailSent = 0;
                            IsCompletedMailSent = 0;

                            IsPartialInternalInspectionMailSent = 0;

                            IsPartialFinalInspectionMailSent = 0;
                            IsPartialReworkMailSent = 0;

                            IsPartialQaItemAcceptedMailSent = 0;
                            IsPartialQaItemNotAcceptedMailSent = 0;
                            IsPartialCompletedMailSent = 0;


                            //isTransferred = false;
                            //oldTransferredLOTTFId = 0;
                            //oldTransferredLOTTFCreatedBy = 0;


                            LOTTFID = Convert.ToInt32(dr1["LOT_TF_ID"]);
                            LOTMainSubItemID = Convert.ToInt32(dr1["LOT_MAIN_SUBITEM_ID"]);

                            quantity = Convert.ToInt32(dr1["QUANTITY"]);
                            currentStatusID = Convert.ToInt32(dr1["STATUS_ID"]);
                            partialStatusID = Convert.ToInt32(dr1["PARTIAL_STATUS_ID"]);
                            productionManagerID = Convert.ToInt32(dr1["PRODUCTION_MNGR_ID"]);

                            createdByID = Convert.ToInt32(dr1["CREATED_BY"]);
                            approvedByID = Convert.ToInt32(dr1["APPROVED_BY"]);
                            prodAcceptedByID = Convert.ToInt32(dr1["ACCEPTED_BY"]);
                            sentToIntlInspectionByID = Convert.ToInt32(dr1["SENT_TO_INTL_INSP_BY"]);
                            qaIntlInspAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_ACCEPTED_BY"]);
                            qaIntlInspNotAcceptedByID = Convert.ToInt32(dr1["QA_INTL_INSP_NOT_ACCEPTED_BY"]);
                            completedByID = Convert.ToInt32(dr1["COMPLETED_BY"]);
                            qualityAcceptedByID = Convert.ToInt32(dr1["QUALITY_ACCEPTED_BY"]);
                            planningAcceptedByID = Convert.ToInt32(dr1["PLANNING_ACCEPTED_BY"]);
                            forwardedByID = Convert.ToInt32(dr1["FORWARDED_BY"]);
                            amendmentCount = Convert.ToInt32(dr1["AMENDMENT_COUNT"]);
                            amendmentByID = Convert.ToInt32(dr1["AMENDMENT_BY"]);
                            prodAmendmentByID = Convert.ToInt32(dr1["PROD_AMENDMENT_BY"]);
                            amendedByID = Convert.ToInt32(dr1["AMENDED_BY"]);
                            amendedApprovedByID = Convert.ToInt32(dr1["AMENDED_APPROVED_BY"]);
                            amendedAcceptedByID = Convert.ToInt32(dr1["AMENDED_ACCEPTED_BY"]);
                            amendedQualityAcceptedByID = Convert.ToInt32(dr1["AMENDED_QUALITY_ACCEPTED_BY"]);
                            amendedPlanningAcceptedByID = Convert.ToInt32(dr1["AMENDED_PLANNING_ACCEPTED_BY"]);
                            amendedForwardedByID = Convert.ToInt32(dr1["AMENDED_FORWARDED_BY"]);
                            isPartOfProductionStatusID = Convert.ToInt32(dr1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);



                            IsApprovalMailSent = Convert.ToInt32(dr1["IS_APPROVAL_MAIL_SENT"]);
                            IsApprovedMailSent = Convert.ToInt32(dr1["IS_APPROVED_MAIL_SENT"]);
                            IsPlanningAcceptedMailSent = Convert.ToInt32(dr1["IS_PLANNING_ACCEPTED_MAIL_SENT"]);
                            IsForwardedMailSent = Convert.ToInt32(dr1["IS_FORWARDED_MAIL_SENT"]);
                            IsAcceptedMailSent = Convert.ToInt32(dr1["IS_ACCEPTED_MAIL_SENT"]);
                            IsQualityAcceptedMailSent = Convert.ToInt32(dr1["IS_QUALITY_ACCEPTED_MAIL_SENT"]);
                            IsSentToIntlInspMailSent = Convert.ToInt32(dr1["IS_SENT_TO_INTL_INSP_MAIL_SENT"]);

                            IsSentToFinalInspMailSent = Convert.ToInt32(dr1["IS_SENT_TO_FINAL_INSP_MAIL_SENT"]);
                            IsSentToReworkMailSent = Convert.ToInt32(dr1["IS_SENT_TO_REWORK_MAIL_SENT"]);

                            IsQaIntlInspAcceptedMailSent = Convert.ToInt32(dr1["IS_QA_INTL_INSP_ACCEPTED_MAIL_SENT"]);
                            IsQaIntlInspNotAcceptedMailSent = Convert.ToInt32(dr1["IS_QA_INTL_INSP_NOT_ACCEPTED_MAIL_SENT"]);
                            IsAmendmentMailSent = Convert.ToInt32(dr1["IS_AMENDMENT_MAIL_SENT"]);
                            IsProdAmendmentMailSent = Convert.ToInt32(dr1["IS_PROD_AMENDMENT_MAIL_SENT"]);
                            IsAmendedMailSent = Convert.ToInt32(dr1["IS_AMENDED_MAIL_SENT"]);
                            IsAmendedApprovedMailSent = Convert.ToInt32(dr1["IS_AMENDED_APPROVED_MAIL_SENT"]);
                            IsAmendedPlanningAcceptedMailSent = Convert.ToInt32(dr1["IS_AMENDED_PLANNING_ACCEPTED_MAIL_SENT"]);
                            IsAmendedForwardedMailSent = Convert.ToInt32(dr1["IS_AMENDED_FORWARDED_MAIL_SENT"]);
                            IsAmendedAcceptedMailSent = Convert.ToInt32(dr1["IS_AMENDED_ACCEPTED_MAIL_SENT"]);
                            IsAmendedQualityAcceptedMailSent = Convert.ToInt32(dr1["IS_AMENDED_QUALITY_ACCEPTED_MAIL_SENT"]);
                            IsCompletedMailSent = Convert.ToInt32(dr1["IS_COMPLETED_MAIL_SENT"]);

                            IsPartialInternalInspectionMailSent = Convert.ToInt32(dr1["PARTIAL_IS_INTERNAL_INSPECTION_MAIL_SENT"]);

                            IsPartialFinalInspectionMailSent = Convert.ToInt32(dr1["PARTIAL_IS_IN_FINAL_INSPECTION_MAIL_SENT"]);
                            IsPartialReworkMailSent = Convert.ToInt32(dr1["PARTIAL_IS_IN_REWORK_MAIL_SENT"]);

                            IsPartialQaItemAcceptedMailSent = Convert.ToInt32(dr1["PARTIAL_IS_QA_ITEM_ACCEPTED_MAIL_SENT"]);
                            IsPartialQaItemNotAcceptedMailSent = Convert.ToInt32(dr1["PARTIAL_IS_QA_ITEM_NOT_ACCEPTED_MAIL_SENT"]);
                            IsPartialCompletedMailSent = Convert.ToInt32(dr1["PARTIAL_IS_COMPLETED_MAIL_SENT"]);

                            //if (Convert.ToInt32(dr1["IS_TRANSFERRED"]) > 0) isTransferred = true;
                            //oldTransferredLOTTFId = Convert.ToInt32(dr1["TRANSFERRED_LOT_TF_ID"]);
                            //oldTransferredLOTTFCreatedBy = Convert.ToInt32(dr1["OLD_TRANSFERRED_LOT_CREATED_BY"]);


                           
                            //New
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                                currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                            {
                                if (currentLoginID == createdByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                                {
                                    imgBtnEditLOT.Visible = true;
                                    btnSendApprovalMail.Visible = false;

                                    if (Convert.ToInt32(lblIsSentForApproval.Text) > 0)
                                    {
                                        if (IsApprovalMailSent == 0)
                                        {
                                            btnSendApprovalMail.Visible = true;
                                            btnSendApprovalMail.Text = "Send Approval Mail";
                                        }
                                    }
                                }

                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                                {
                                    if (currentLoginID == amendedByID || currentLoginID == createdByID)
                                    {
                                        imgBtnEditLOT.Visible = true;
                                        btnSendApprovalMail.Visible = false;

                                        //if (Convert.ToInt32(lblIsAmendedSentForApproval.Text) > 0)
                                        if (Convert.ToInt32(lblIsSentForApproval.Text) > 0)
                                        {
                                            if (IsAmendedMailSent == 0)
                                            {
                                                btnSendApprovalMail.Visible = true;
                                                btnSendApprovalMail.Text = "Send Approval Mail";
                                            }
                                        }
                                    }

                                }

                                if (currentLoginID == PEID || currentLoginID == PMID)
                                {
                                    btnApproveLOT.Visible = false;
                                    if (Convert.ToInt32(lblIsSentForApproval.Text) > 0)
                                    {
                                        btnApproveLOT.Visible = true;
                                        btnApproveLOT.Text = "Approve LOT";
                                        btnApproveLOT.ToolTip = "Approve LOT Transmittal to Factory";
                                    }

                                    //if (amendmentCount == 0)
                                    //{
                                    //    if (Convert.ToInt32(lblIsSentForApproval.Text) > 0)
                                    //    {
                                    //        btnApproveLOT.Visible = true;
                                    //        btnApproveLOT.Text = "Approve LOT";
                                    //        btnApproveLOT.ToolTip = "Approve LOT Transmittal to Factory";
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (Convert.ToInt32(lblIsAmendedSentForApproval.Text) > 0)
                                    //    {
                                    //        btnApproveLOT.Visible = true;
                                    //        btnApproveLOT.Text = "Approve Amended LOT";
                                    //        btnApproveLOT.ToolTip = "Approve Amended LOT Transmittal to Factory";
                                    //    }
                                    //}
                                }
                            }


                            //Amendment
                            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                            {
                                if (Convert.ToInt32(lblIsTransferred.Text) > 0)
                                {
                                    if (currentLoginID == oldTransferredLOTTFCreatedBy)
                                    {
                                        btnAmendLOT.Visible = true;
                                        btnAmendLOT.ToolTip = "Amend LOT Transmittal to Factory";
                                    }
                                }
                                else
                                {
                                    if (currentLoginID == createdByID)
                                    {
                                        btnAmendLOT.Visible = true;
                                        btnAmendLOT.ToolTip = "Amend LOT Transmittal to Factory";
                                    }
                                    else
                                    {
                                        //Planning
                                        if (dsLOTList.Tables[4].Rows.Count > 0)
                                        {
                                            foreach (DataRow drac in dsLOTList.Tables[4].Rows)
                                            {
                                                if (amendmentByID == Convert.ToInt32(drac["ACCEPTER_ID"]) && (currentLoginID == PEID || currentLoginID == PMID))
                                                {
                                                    btnApproveLOT.Visible = true;
                                                    btnApproveLOT.Text = "Send To Amendment LOT";
                                                    btnApproveLOT.ToolTip = "Send To Amendment LOT Transmittal to Factory";

                                                    if (IsAmendmentMailSent == 0)
                                                    {
                                                        btnSendApprovalMail.Visible = true;
                                                        btnSendApprovalMail.Text = "Send Amendment Mail";
                                                    }
                                                    else
                                                    {
                                                        btnSendApprovalMail.Visible = false;
                                                    }
                                                }
                                            }
                                        }

                                        if (currentLoginID == createdByID && (amendmentByID == PEID || amendmentByID == PMID))
                                        {
                                            btnAmendLOT.Visible = true;
                                            btnAmendLOT.ToolTip = "Amend LOT Transmittal to Factory";
                                        }
                                    }
                                }









                                if (currentLoginID == amendmentByID && currentLoginID != productionManagerID)
                                {
                                    if (IsAmendmentMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Amendment Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }

                                if (currentLoginID == productionManagerID)//currentLoginID == amendmentByID && 
                                {
                                    if (IsProdAmendmentMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Amendment Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }
                            }


                           
                            //Approved
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))//acceptance
                            {
                                if (currentLoginID == approvedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                                {
                                    if (IsApprovedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Approved Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }

                                if (currentLoginID == amendedApprovedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                                {
                                    if (IsAmendedApprovedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Approved Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }







                                //if user is from planning
                                //if (planningAcceptedByID == 0)
                                //{
                                //if (isPartOfProductionStatusID > 0)
                                //{
                                if (dsLOTList.Tables[4].Rows.Count > 0)
                                {
                                    foreach (DataRow drac in dsLOTList.Tables[4].Rows)
                                    {
                                        if (currentLoginID == Convert.ToInt32(drac["ACCEPTER_ID"]))
                                        {
                                            btnApproveLOT.Visible = true;
                                            if (amendmentCount == 0)
                                            {
                                                btnApproveLOT.Text = "Accept LOT";
                                                btnApproveLOT.ToolTip = "Accept LOT Transmittal to Factory";
                                            }
                                            else
                                            {
                                                btnApproveLOT.Text = "Accept Amended LOT";
                                                btnApproveLOT.ToolTip = "Accept Amended LOT Transmittal to Factory";
                                            }
                                        }
                                    }
                                }
                                //}
                                //}
                                //}
                            }



                            //Planning Accepted
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                            {

                                if (currentLoginID == planningAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted))
                                {
                                    if (IsPlanningAcceptedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Accepted Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }

                                if (currentLoginID == amendedPlanningAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                                {
                                    if (IsAmendedPlanningAcceptedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Accepted Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }





                                //if user is from planning
                                //if (planningAcceptedByID > 0)
                                //{
                                //if (isPartOfProductionStatusID > 0)
                                //{
                                if (dsLOTList.Tables[4].Rows.Count > 0)
                                {
                                    foreach (DataRow drac in dsLOTList.Tables[4].Rows)
                                    {
                                        if (currentLoginID == Convert.ToInt32(drac["ACCEPTER_ID"]))
                                        {
                                            btnApproveLOT.Visible = true;

                                            if (amendmentCount == 0)
                                            {
                                                btnApproveLOT.Text = "Forward LOT";
                                                btnApproveLOT.ToolTip = "Forward LOT Transmittal to Factory to Production";
                                            }
                                            else
                                            {
                                                btnApproveLOT.Text = "Forward Amended LOT";
                                                btnApproveLOT.ToolTip = "Forward Amended LOT Transmittal to Factory to Production";
                                            }
                                        }
                                    }
                                }
                                //}
                                //}
                                //}
                            }



                            //Planning Forwarded
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                            {

                                if (currentLoginID == forwardedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded))
                                {
                                    if (IsForwardedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Forwarded Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }

                                if (currentLoginID == amendedForwardedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                                {
                                    if (IsAmendedForwardedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Forwarded Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }


                                //if user is from planning
                                //if (currentLoginID == planningAcceptedByID)
                                //{

                                //}
                                if (currentLoginID == productionManagerID)
                                {
                                    btnApproveLOT.Visible = true;
                                    if (amendmentCount == 0)
                                    {
                                        btnApproveLOT.Text = "Accept LOT";
                                        btnApproveLOT.ToolTip = "Accept LOT Transmittal to Factory";
                                    }
                                    else
                                    {
                                        btnApproveLOT.Text = "Accept Amended LOT";
                                        btnApproveLOT.ToolTip = "Accept Amended LOT Transmittal to Factory";
                                    }
                                }
                            }



                            //Production-Accepted
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                            {

                                if (currentLoginID == prodAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                                {
                                    if (IsAcceptedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Accepted Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }

                                if (currentLoginID == amendedAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                                {
                                    if (IsAmendedAcceptedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Accepted Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }


                                //if user is from quality


                                if (qualityAcceptedByID == 0)
                                {
                                    if (dsLOTList.Tables[2].Rows.Count > 0)
                                    {
                                        foreach (DataRow drt1 in dsLOTList.Tables[2].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubItemID + "'"))
                                        {
                                            //foreach (DataRow drac in dsLOTList.Tables[2].Select("LOT_MAIN_SUBITEM_ID=" + Convert.ToInt32(drt1["LOT_MAIN_SUBITEM_ID"]) + " AND UNIT_ID='" + companyID + "'"))
                                            //{
                                            if (currentLoginID == Convert.ToInt32(drt1["ACCEPTER_ID"]))
                                            {
                                                btnApproveLOT.Visible = true;

                                                if (amendmentCount == 0)
                                                {
                                                    btnApproveLOT.Text = "Accept LOT";
                                                    btnApproveLOT.ToolTip = "Accept LOT Transmittal to Factory";
                                                }
                                                else
                                                {
                                                    btnApproveLOT.Text = "Accept Amended LOT";
                                                    btnApproveLOT.ToolTip = "Accept Amended LOT Transmittal to Factory";
                                                }
                                            }
                                            //}
                                        }
                                    }
                                }

                                //if (qualityAcceptedByID == 0)
                                //{
                                //    if (dsLOTList.Tables[2].Rows.Count > 0)
                                //    {
                                //        foreach (DataRow drt1 in dsLOTList.Tables[1].Rows)
                                //        {
                                //            foreach (DataRow drac in dsLOTList.Tables[2].Select("LOT_MAIN_SUBITEM_ID=" + Convert.ToInt32(drt1["LOT_MAIN_SUBITEM_ID"]) + " AND UNIT_ID='" + companyID + "'"))
                                //            {
                                //                if (currentLoginID == Convert.ToInt32(drac["ACCEPTER_ID"]))
                                //                {
                                //                    btnApproveLOT.Visible = true;

                                //                    if (amendmentCount == 0)
                                //                    {
                                //                        btnApproveLOT.Text = "Accept LOT";
                                //                        btnApproveLOT.ToolTip = "Accept LOT Transmittal to Factory";
                                //                    }
                                //                    else
                                //                    {
                                //                        btnApproveLOT.Text = "Accept Amended LOT";
                                //                        btnApproveLOT.ToolTip = "Accept Amended LOT Transmittal to Factory";
                                //                    }
                                //                }
                                //            }
                                //        }
                                //    }
                                //}
                            }



                            //QA-LOT Accepted
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                            {

                                if (currentLoginID == qualityAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted))
                                {
                                    if (IsQualityAcceptedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Accepted Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }

                                if (currentLoginID == amendedQualityAcceptedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                                {
                                    if (IsAmendedQualityAcceptedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Accepted Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }





                                if (isPartOfProductionStatusID > 0)
                                {
                                    if (currentLoginID == productionManagerID)
                                    {
                                        btnSendToIntlInsp.Visible = true;
                                    }
                                }
                            }


                            //Complete
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                            {
                                if (currentLoginID == completedByID && currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                                {
                                    if (IsCompletedMailSent == 0)
                                    {
                                        btnSendApprovalMail.Visible = true;
                                        btnSendApprovalMail.Text = "Send Completed Mail";
                                    }
                                    else
                                    {
                                        btnSendApprovalMail.Visible = false;
                                    }
                                }
                            }



                            //Partial
                            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted) ||

                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection) ||
                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework) ||

                                     currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                            {

                                if (isPartOfProductionStatusID > 0)
                                {
                                    foreach (DataRow dr5 in dsLOTList.Tables[5].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr0["LOT_TF_SUBITEM_ID"]) + "'"))
                                    {
                                        //Fitup Inspection
                                        if ((Convert.ToInt32(dr5["QUANTITY"]) - Convert.ToInt32(dr5["INTERNAL_INSPECTION"])) > 0)
                                        {
                                            if (currentLoginID == productionManagerID)
                                            {
                                                btnSendToIntlInsp.Visible = true;
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_SENT_TO_INTL_INSP_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_INTL_INSP_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection))
                                                {
                                                    if (IsSentToIntlInspMailSent == 0)
                                                    {
                                                        btnSendToIntlInspSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_INTERNAL_INSPECTION_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_INTL_INSP_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                                                {
                                                    if (IsPartialInternalInspectionMailSent == 0)
                                                    {
                                                        btnSendToIntlInspSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }





                                        //Item-Accepted
                                        if ((Convert.ToInt32(dr5["INTERNAL_INSPECTION"]) - Convert.ToInt32(dr5["QA_ITEM_ACCEPTED"])) > 0)
                                        {
                                            if (dsLOTList.Tables[2].Rows.Count > 0)
                                            {
                                                foreach (DataRow drac in dsLOTList.Tables[2].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubItemID + "' AND UNIT_ID='" + companyID + "'"))
                                                {
                                                    if (currentLoginID == Convert.ToInt32(drac["ACCEPTER_ID"]))
                                                    {
                                                        btnAcceptItems.Visible = true;
                                                    }
                                                }
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_QA_INTL_INSP_ACCEPTED_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["QA_INTL_INSP_ACCEPTED_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                                                {
                                                    if (IsQaIntlInspAcceptedMailSent == 0)
                                                    {
                                                        btnAcceptItemsSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_QA_ITEM_ACCEPTED_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["QA_INTL_INSP_ACCEPTED_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                                                {
                                                    if (IsPartialQaItemAcceptedMailSent == 0)
                                                    {
                                                        btnAcceptItemsSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }





                                        //Not accepted items
                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_QA_INTL_INSP_NOT_ACCEPTED_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["QA_INTL_INSP_NOT_ACCEPTED_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted))
                                                {
                                                    if (IsQaIntlInspNotAcceptedMailSent == 0)
                                                    {
                                                        btnNotAcceptItemsSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_QA_ITEM_NOT_ACCEPTED_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["QA_INTL_INSP_NOT_ACCEPTED_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                                                {
                                                    if (IsPartialQaItemNotAcceptedMailSent == 0)
                                                    {
                                                        btnNotAcceptItemsSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }



                                        //Final Inspection
                                        if ((Convert.ToInt32(dr5["QA_ITEM_ACCEPTED"]) - Convert.ToInt32(dr5["IN_FINAL_INSPECTION"])) > 0)
                                        {
                                            if (currentLoginID == productionManagerID)
                                            {
                                                btnSendToFinalInsp.Visible = true;
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_SENT_TO_FINAL_INSP_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_FINAL_INSP_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection))
                                                {
                                                    if (currentLoginID == productionManagerID)
                                                    {
                                                        if (IsSentToFinalInspMailSent == 0)
                                                        {
                                                            btnSendToFinalInspSendMail.Visible = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_IN_FINAL_INSPECTION_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_FINAL_INSP_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                                                {
                                                    if (currentLoginID == productionManagerID)
                                                    {
                                                        if (IsPartialFinalInspectionMailSent == 0)
                                                        {
                                                            btnSendToFinalInspSendMail.Visible = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }




                                        //Rework
                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_SENT_TO_REWORK_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_REWORK_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework))
                                                {
                                                    if (IsSentToReworkMailSent == 0)
                                                    {
                                                        btnSendToReworkSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_IN_REWORK_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["SENT_TO_REWORK_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                                                {
                                                    if (IsPartialReworkMailSent == 0)
                                                    {
                                                        btnSendToReworkSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }



                                        //Complete
                                        if ((Convert.ToInt32(dr5["IN_FINAL_INSPECTION"]) - Convert.ToInt32(dr5["COMPLETE"])) > 0)
                                        {
                                            if (dsLOTList.Tables[2].Rows.Count > 0)
                                            {
                                                foreach (DataRow drac in dsLOTList.Tables[2].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubItemID + "' AND UNIT_ID='" + companyID + "'"))
                                                {
                                                    if (currentLoginID == Convert.ToInt32(drac["ACCEPTER_ID"]))
                                                    {
                                                        btnFinalIsnpItems.Visible = true;
                                                    }
                                                }
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND IS_COMPLETED_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["COMPLETED_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                                                {
                                                    if (IsCompletedMailSent == 0)
                                                    {
                                                        btnFinalIsnpItemsSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }

                                        foreach (DataRow dr3 in dsLOTList.Tables[3].Select("LOT_TF_SUBITEM_ID='" + Convert.ToInt32(dr5["LOT_TF_SUBITEM_ID"]) + "' AND PARTIAL_IS_COMPLETED_MAIL_SENT='0'"))
                                        {
                                            if (currentLoginID == Convert.ToInt32(dr3["COMPLETED_BY"]))
                                            {
                                                if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                                                {
                                                    if (partialStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialComplete) && IsPartialCompletedMailSent == 0)
                                                    {
                                                        btnFinalIsnpItemsSendMail.Visible = true;
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                    #endregion



                    #region ATTACHMENTS

                    string standardDrawingExtn = string.Empty;
                    string attachment1Extn = string.Empty;
                    string attachment2Extn = string.Empty;
                    string attachment3Extn = string.Empty;
                    string attachment4Extn = string.Empty;


                    Label lblStandardDrawing = (Label)e.Row.FindControl("lblStandardDrawing");
                    Label lblAttachment1 = (Label)e.Row.FindControl("lblAttachment1");
                    Label lblAttachment2 = (Label)e.Row.FindControl("lblAttachment2");
                    Label lblAttachment3 = (Label)e.Row.FindControl("lblAttachment3");
                    Label lblAttachment4 = (Label)e.Row.FindControl("lblAttachment4");


                    ImageButton imgBtnViewStandardDrawing = (ImageButton)e.Row.FindControl("imgBtnViewStandardDrawing");
                    ImageButton imgBtnAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAttachment1");
                    ImageButton imgBtnAttachment2 = (ImageButton)e.Row.FindControl("imgBtnAttachment2");
                    ImageButton imgBtnAttachment3 = (ImageButton)e.Row.FindControl("imgBtnAttachment3");
                    ImageButton imgBtnAttachment4 = (ImageButton)e.Row.FindControl("imgBtnAttachment4");



                    imgBtnViewStandardDrawing.Visible = false;
                    imgBtnAttachment1.Visible = false;
                    imgBtnAttachment2.Visible = false;
                    imgBtnAttachment3.Visible = false;
                    imgBtnAttachment4.Visible = false;


                    imgBtnViewStandardDrawing.ToolTip = string.Empty;
                    imgBtnAttachment1.ToolTip = string.Empty;
                    imgBtnAttachment2.ToolTip = string.Empty;
                    imgBtnAttachment3.ToolTip = string.Empty;
                    imgBtnAttachment4.ToolTip = string.Empty;




                    if (!string.IsNullOrEmpty(lblStandardDrawing.Text))
                    {
                        imgBtnViewStandardDrawing.Visible = true;
                        imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;

                        standardDrawingExtn = Convert.ToString(lblStandardDrawing.Text).Split('.').Last();
                        if (standardDrawingExtn == "jpg" || standardDrawingExtn == "jepg" || standardDrawingExtn == "bmp" || standardDrawingExtn == "png" ||
                            standardDrawingExtn == "gif" || standardDrawingExtn == "JPG" || standardDrawingExtn == "JPEG" || standardDrawingExtn == "BMP" ||
                            standardDrawingExtn == "PNG" || standardDrawingExtn == "GIF")
                        {
                            imgBtnViewStandardDrawing.ImageUrl = "~/Images/imgicon1.png";
                            imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;
                        }
                        else if (standardDrawingExtn == "pdf" || standardDrawingExtn == "PDF")
                        {
                            imgBtnViewStandardDrawing.ImageUrl = "~/Images/pdficon1.png";
                            imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;
                        }
                        else if (standardDrawingExtn == "dxf" || standardDrawingExtn == "DXF")
                        {
                            imgBtnViewStandardDrawing.ImageUrl = "~/Images/LOT/dxf.png";
                            imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;
                        }
                        else if (standardDrawingExtn == "dwg" || standardDrawingExtn == "DWG")
                        {
                            imgBtnViewStandardDrawing.ImageUrl = "~/Images/LOT/dwg.png";
                            imgBtnViewStandardDrawing.ToolTip = lblStandardDrawing.Text;
                        }
                    }
                    else
                    {
                        imgBtnViewStandardDrawing.Visible = false;
                    }


                    //if (!string.IsNullOrEmpty(lblAttachment1.Text))
                    //{
                    //    imgBtnAttachment1.Visible = true;
                    //    imgBtnAttachment1.ToolTip = lblAttachment1.Text;

                    //    attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
                    //    if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                    //    {
                    //        imgBtnAttachment1.ImageUrl = "~/Images/imgicon1.png";
                    //        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    //    }
                    //    else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                    //    {
                    //        imgBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                    //        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    //    }
                    //}
                    //else
                    //{
                    //    imgBtnAttachment1.Visible = false;
                    //}

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


                    //if (!string.IsNullOrEmpty(lblAttachment2.Text))
                    //{
                    //    imgBtnAttachment2.Visible = true;
                    //    imgBtnAttachment2.ToolTip = lblAttachment2.Text;

                    //    attachment2Extn = Convert.ToString(lblAttachment2.Text).Split('.').Last();
                    //    if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
                    //    {
                    //        imgBtnAttachment2.ImageUrl = "~/Images/imgicon1.png";
                    //        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    //    }
                    //    else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
                    //    {
                    //        imgBtnAttachment2.ImageUrl = "~/Images/pdficon1.png";
                    //        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    //    }
                    //}
                    //else
                    //{
                    //    imgBtnAttachment2.Visible = false;
                    //}



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


                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        if (i < 10 || i > 12)
                        {
                            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                        }
                    }
                }
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
                DataTable dtRemainingQuantity = new DataTable();
                dtRemainingQuantity.Columns.Add("REMAINING_QUANTITY", typeof(int));

                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                Label lblLOTMainSubitemID = (Label)e.Row.FindControl("lblLOTMainSubitemID");
                //Label lblProdManagerID = (Label)e.Row.FindControl("lblProdManagerID");
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

                    if (reworkCount > 0)
                    {
                        txtStatus.Text += reworkCount + "-RW,";
                        txtStatus.ToolTip += QQAItemNotAcceptedCount + "-Sent To Rework By Quality,\n";
                    }

                    if (finalInspectionCount > 0)
                    {
                        finalInspectionCount = finalInspectionCount - reworkCount;

                        txtStatus.Text += finalInspectionCount + "-FIN(Insp),";
                        txtStatus.ToolTip += finalInspectionCount + "-Sent To Final Inspection By Production,\n";
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


                #region SELECT SUBITEMS

                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");

                chkSelect.Checked = false;
                chkSelect.Visible = false;

                if (hdSubitemsCheckboxVisibility.Value == "1")
                {
                    if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.Approve))
                    {
                        chkSelect.Checked = false;
                        chkSelect.Visible = false;


                        gvSubitemsSI.Columns[15].Visible = false;
                        gvSubitemsSI.Columns[16].Visible = false;
                        gvSubitemsSI.Columns[17].Visible = false;
                    }

                    else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFitupInsp))
                    {
                        chkSelect.Visible = true;
                        gvSubitemsSI.Columns[15].Visible = false;
                        gvSubitemsSI.Columns[16].Visible = true;
                        gvSubitemsSI.Columns[17].Visible = true;

                        gvSubitemsSI.HeaderRow.Cells[16].Text = "Sent To Fitup Insp. Qty.";
                    }



                    else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.AcceptItems))
                    {
                        chkSelect.Visible = true;

                        gvSubitemsSI.Columns[15].Visible = true;
                        gvSubitemsSI.Columns[16].Visible = true;
                        gvSubitemsSI.Columns[17].Visible = true;

                        gvSubitemsSI.HeaderRow.Cells[15].Text = "Sent To Fitup Insp. Qty.";
                        gvSubitemsSI.HeaderRow.Cells[16].Text = "Fitup Insp. Accepted Qty.";
                    }

                    else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFinalInsp))
                    {
                        chkSelect.Visible = true;

                        gvSubitemsSI.Columns[15].Visible = true;
                        gvSubitemsSI.Columns[16].Visible = true;
                        gvSubitemsSI.Columns[17].Visible = true;

                        gvSubitemsSI.HeaderRow.Cells[15].Text = "Fitup Insp. Accepted Qty.";
                        gvSubitemsSI.HeaderRow.Cells[16].Text = "In Final Insp. Qty.";
                    }

                    else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.CompleteItems))
                    {
                        chkSelect.Visible = true;

                        gvSubitemsSI.Columns[15].Visible = true;
                        gvSubitemsSI.Columns[16].Visible = true;
                        gvSubitemsSI.Columns[17].Visible = true;

                        gvSubitemsSI.HeaderRow.Cells[15].Text = "In Final Insp. Qty.";
                        gvSubitemsSI.HeaderRow.Cells[16].Text = "Completed Qty.";
                    }




                }
                else
                {
                    chkSelect.Checked = false;
                    chkSelect.Visible = false;

                    //txtRemainingQuantity.Attributes.Add("onkeyDown", "javascript:preventInput(event);");
                    gvSubitemsSI.Columns[15].Visible = false;
                    gvSubitemsSI.Columns[16].Visible = false;
                    gvSubitemsSI.Columns[17].Visible = false;
                }

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








    //Session["UpdateSubitemStatusClicked"] = false;
    //Session["SendToIntlInspectionClicked"] = false;
    //Session["QaIntlAcceptClicked"] = false;
    //Session["QaIntlNotAcceptClicked"] = false;
    //Session["CompleteItemsClicked"] = false;
    //Session["SendToAmendmentClicked"] = false;
    //Session["SendToAmendmentByProductionClicked"] = false;
    //Session["MultipleClicked"] = false;



    #region UPDATE STATUS/SEND TO AMENDMENT[===============]

    protected void btnUpdateSubitemStatus_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["UpdateSubitemStatusClicked"]))
        {
            Session["UpdateSubitemStatusClicked"] = true;
            Session["SendToAmendmentClicked"] = true;
            Session["SendToAmendmentByProductionClicked"] = true;
            ApproveOrSendToAmendment(Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.ApproveOrAccept));

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

    protected void btnSendToIntlInspection_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["SendToIntlInspectionClicked"]))
        {
            Session["SendToIntlInspectionClicked"] = true;
            SendToInernalInspectionAndQaAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection));

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

    protected void btnSendToFinalInspection_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["SendToFinalInspectionClicked"]))
        {
            Session["SendToFinalInspectionClicked"] = true;
            SendToInernalInspectionAndQaAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection));

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



    protected void btnQaIntlAccept_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["QaIntlAcceptClicked"]))
        {
            Session["QaIntlAcceptClicked"] = true;
            Session["QaIntlNotAcceptClicked"] = true;
            //QAIntlInspAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted));
            SendToInernalInspectionAndQaAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted));

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

    protected void btnQaIntlNotAccept_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["QaIntlNotAcceptClicked"]))
        {
            Session["QaIntlAcceptClicked"] = true;
            Session["QaIntlNotAcceptClicked"] = true;
            hdButtonFlag.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.NotAcceptItems);
            //QAIntlInspAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted));
            SendToInernalInspectionAndQaAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted));

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


    protected void btnSendItemsForRework_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["SendToReworkClicked"]))
        {
            Session["SendToReworkClicked"] = true;
            hdButtonFlag.Value = Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.Rework);
            SendToInernalInspectionAndQaAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework));

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


    protected void btnCompleteItems_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["CompleteItemsClicked"]))
        {
            Session["CompleteItemsClicked"] = true;
            //QAIntlInspAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted));
            SendToInernalInspectionAndQaAcceptance(Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete));

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




    protected void btnSendToAmendment_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["SendToAmendmentClicked"]))
        {
            Session["UpdateSubitemStatusClicked"] = true;
            Session["SendToAmendmentClicked"] = true;
            Session["SendToAmendmentByProductionClicked"] = true;
            ApproveOrSendToAmendment(Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.SendToAmendment));

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

    protected void btnSendToAmendmentByProduction_Click(object sender, EventArgs e)
    {
        if (!Convert.ToBoolean(Session["SendToAmendmentByProductionClicked"]))
        {
            Session["UpdateSubitemStatusClicked"] = true;
            Session["SendToAmendmentClicked"] = true;
            Session["SendToAmendmentByProductionClicked"] = true;
            ApproveOrSendToAmendment(Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.SendToAmendmentByProduction));

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

    #endregion



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

    private void BindLOTSubitemDetailsByID(int LOTTFID, int createdById, int PEID, int PMID, string LOTMainSubitemIDs
                                         , string LOTTFSubitemIDs, int buttonControlTypeID)
    {
        try
        {
            #region dtSi Columns            

            DataTable dtSi = new DataTable();
            dtSi.Columns.Add("SR_NO", typeof(int));
            dtSi.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
            dtSi.Columns.Add("STATUS_ID", typeof(int));
            dtSi.Columns.Add("NEXT_STATUS_ID", typeof(int));
            dtSi.Columns.Add("LOT_TF_ID", typeof(int));
            dtSi.Columns.Add("SUBITEM_DESC", typeof(string));
            dtSi.Columns.Add("TAG_NO", typeof(string));
            dtSi.Columns.Add("LOT_MAIN_ITEM_ID", typeof(int));
            dtSi.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
            dtSi.Columns.Add("LOT_MAIN_ITEM", typeof(string));
            dtSi.Columns.Add("DRAWING_NO", typeof(string));

            dtSi.Columns.Add("REVISION_NO", typeof(int));
            dtSi.Columns.Add("REVISION_NO_TEXT", typeof(string));

            dtSi.Columns.Add("CATEGORY_ID", typeof(string));
            dtSi.Columns.Add("CATEGORY", typeof(string));

            dtSi.Columns.Add("TOTAL_QUANTITY", typeof(int));
            dtSi.Columns.Add("QUANTITY", typeof(int));

            dtSi.Columns.Add("ACTED_QUANTITY", typeof(int));
            dtSi.Columns.Add("REMAINING_QUANTITY", typeof(int));


            dtSi.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtSi.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            dtSi.Columns.Add("CREATED_BY", typeof(int));

            dtSi.Columns.Add("APPROVED_BY", typeof(int));
            dtSi.Columns.Add("PLANNING_ACCEPTED_BY", typeof(int));
            dtSi.Columns.Add("FORWARDED_BY", typeof(int));
            dtSi.Columns.Add("ACCEPTED_BY", typeof(int));
            dtSi.Columns.Add("QUALITY_ACCEPTED_BY", typeof(int));

            dtSi.Columns.Add("SENT_TO_INTL_INSP_BY", typeof(int));
            dtSi.Columns.Add("QA_INTL_INSP_ACCEPTED_BY", typeof(int));
            dtSi.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_BY", typeof(int));

            dtSi.Columns.Add("COMPLETED_BY", typeof(int));
            dtSi.Columns.Add("REVISED_BY", typeof(int));
            dtSi.Columns.Add("AMENDMENT_COUNT", typeof(int));
            dtSi.Columns.Add("AMENDMENT_BY", typeof(int));
            dtSi.Columns.Add("PROD_AMENDMENT_BY", typeof(int));
            dtSi.Columns.Add("AMENDED_BY", typeof(int));

            dtSi.Columns.Add("AMENDED_APPROVED_BY", typeof(int));
            dtSi.Columns.Add("AMENDED_PLANNING_ACCEPTED_BY", typeof(int));
            dtSi.Columns.Add("AMENDED_FORWARDED_BY", typeof(int));
            dtSi.Columns.Add("AMENDED_ACCEPTED_BY", typeof(int));
            dtSi.Columns.Add("AMENDED_QUALITY_ACCEPTED_BY", typeof(int));

            dtSi.Columns.Add("PRODUCTION_MNGR_ID", typeof(int));
            dtSi.Columns.Add("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID", typeof(int));

            dtSi.Columns.Add("INTERNAL_INSPECTION_QTY", typeof(int));
            dtSi.Columns.Add("QA_ITEM_ACCEPTED_QTY", typeof(int));
            dtSi.Columns.Add("QA_ITEM_NOT_ACCEPTED_QTY", typeof(int));

            dtSi.Columns.Add("IN_FINAL_INSPECTION_QTY", typeof(int));
            dtSi.Columns.Add("IN_REWORK_QTY", typeof(int));

            dtSi.Columns.Add("COMPLETE_QTY", typeof(int));


            dtSi.Columns.Add("SI_ATTACHMENT1_NAME", typeof(string));
            dtSi.Columns.Add("SI_ATTACHMENT2_NAME", typeof(string));
            dtSi.Columns.Add("SI_ATTACHMENT3_NAME", typeof(string));
            dtSi.Columns.Add("SI_ATTACHMENT4_NAME", typeof(string));
            dtSi.Columns.Add("IRN_ATTACHMENT_NAME", typeof(string));



            #endregion

            dsSubitems = GetLOTSubitemDetails(LOTTFID, LOTMainSubitemIDs, LOTTFSubitemIDs);

            ViewState["ALL_BYTES"] = null;
            ViewState["ALL_BYTES"] = null;
            int countr = 0;
            if (dsSubitems.Tables.Count > 0)
            {
                if (dsSubitems.Tables[0].Rows.Count > 0)
                {
                    countr++;
                    for (int i = 1; i <= 2; i++)
                    {
                        ViewState[Convert.ToString(countr)] = "DD";
                    }
                }

                if (dsSubitems.Tables[8].Rows.Count > 0)
                {
                    DataRow dr0 = dsSubitems.Tables[8].Rows[0];
                    txtImportantNotes.Text = Convert.ToString(dr0["IMP_NOTES"]);
                    txtAmendedRemarks.Text = Convert.ToString(dr0["AMENDED_REMARKS"]);
                }
            }



            //if (dsSubitems.Tables.Count > 0 && dsSubitems.Tables[8].Rows.Count > 0)
            //{
            //    byte[] bytes = null;
            //    List<byte[]> b = new List<byte[]>();

            //    foreach (DataRow dr in dsSubitems.Tables[8].Rows)
            //    {
            //        bytes = (byte[])dr["DOC_BYTES"];
            //        b.Add(bytes);
            //    }

            //    byte[] allBytes = concatAndAddContent(b);

            //    if (allBytes.Length > 0)
            //    {
            //        ViewState["ALL_BYTES"] = allBytes;
            //    }
            //    else
            //    {
            //        ViewState["ALL_BYTES"] = null;
            //    }
            //}


            int prodMngrID = 0;
            int approvedByID = 0;
            int amendedApprovedByID = 0;

            currentStatusID = 0;
            createdByID = 0;
            approvedByID = 0;
            amendmentCount = 0;
            amendedByID = 0;
            amendedApprovedByID = 0;
            LOTMainSubitemID = 0;

            btnUpdateSubitemStatus.Visible = false;
            btnSendToAmendment.Visible = false;
            btnSendToAmendmentByProduction.Visible = false;

            btnSendToIntlInspection.Visible = false;
            btnSendToFinalInspection.Visible = false;
            btnQaIntlAccept.Visible = false;
            btnQaIntlNotAccept.Visible = false;
            pnlIRNAttachment.Visible = false;
            btnCompleteItems.Visible = false;
            btnSendItemsForRework.Visible = false;
            pnlPrint.Visible = false;
            chkIsSendToPrint.Checked = false;
            pnlPrintSettings.Visible = false;

            if (buttonControlTypeID == Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.Approve))
            {

                foreach (DataRow dr0 in dsSubitems.Tables[0].Rows)
                {
                    currentStatusID = Convert.ToInt32(dr0["STATUS_ID"]);

                    if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                        currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                    {
                        DataRow dsr = dtSi.NewRow();

                        #region DTSI Rows

                        dsr["SR_NO"] = dr0["SR_NO"];
                        dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];
                        dsr["STATUS_ID"] = dr0["STATUS_ID"];
                        dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                        dsr["LOT_TF_ID"] = dr0["LOT_TF_ID"];
                        dsr["SUBITEM_DESC"] = dr0["SUBITEM_DESC"];
                        dsr["TAG_NO"] = dr0["TAG_NO"];
                        dsr["LOT_MAIN_ITEM_ID"] = dr0["LOT_MAIN_ITEM_ID"];
                        dsr["LOT_MAIN_SUBITEM_ID"] = dr0["LOT_MAIN_SUBITEM_ID"];
                        dsr["LOT_MAIN_ITEM"] = dr0["LOT_MAIN_ITEM"];
                        dsr["DRAWING_NO"] = dr0["DRAWING_NO"];

                        dsr["REVISION_NO"] = dr0["REVISION_NO"];
                        dsr["REVISION_NO_TEXT"] = dr0["REVISION_NO_TEXT"];

                        dsr["CATEGORY_ID"] = dr0["CATEGORY_ID"];
                        dsr["CATEGORY"] = dr0["CATEGORY"];

                        dsr["TOTAL_QUANTITY"] = dr0["TOTAL_QUANTITY"];
                        dsr["QUANTITY"] = dr0["QUANTITY"];
                        dsr["ACTED_QUANTITY"] = dr0["ACTED_QUANTITY"];
                        dsr["REMAINING_QUANTITY"] = dr0["REMAINING_QUANTITY"];

                        dsr["PRODUCTION_ORDER_NO"] = dr0["PRODUCTION_ORDER_NO"];
                        dsr["EXPECTED_COMPLETION_DATE"] = dr0["EXPECTED_COMPLETION_DATE"];
                        dsr["CREATED_BY"] = dr0["CREATED_BY"];

                        dsr["APPROVED_BY"] = dr0["APPROVED_BY"];
                        dsr["PLANNING_ACCEPTED_BY"] = dr0["PLANNING_ACCEPTED_BY"];
                        dsr["FORWARDED_BY"] = dr0["FORWARDED_BY"];
                        dsr["ACCEPTED_BY"] = dr0["ACCEPTED_BY"];
                        dsr["QUALITY_ACCEPTED_BY"] = dr0["QUALITY_ACCEPTED_BY"];

                        dsr["SENT_TO_INTL_INSP_BY"] = dr0["SENT_TO_INTL_INSP_BY"];
                        dsr["QA_INTL_INSP_ACCEPTED_BY"] = dr0["QA_INTL_INSP_ACCEPTED_BY"];
                        dsr["QA_INTL_INSP_NOT_ACCEPTED_BY"] = dr0["QA_INTL_INSP_NOT_ACCEPTED_BY"];

                        dsr["COMPLETED_BY"] = dr0["COMPLETED_BY"];
                        dsr["REVISED_BY"] = dr0["REVISED_BY"];
                        dsr["AMENDMENT_COUNT"] = dr0["AMENDMENT_COUNT"];
                        dsr["AMENDMENT_BY"] = dr0["AMENDMENT_BY"];
                        dsr["PROD_AMENDMENT_BY"] = dr0["PROD_AMENDMENT_BY"];
                        dsr["AMENDED_BY"] = dr0["AMENDED_BY"];

                        dsr["AMENDED_APPROVED_BY"] = dr0["AMENDED_APPROVED_BY"];
                        dsr["AMENDED_PLANNING_ACCEPTED_BY"] = dr0["AMENDED_PLANNING_ACCEPTED_BY"];
                        dsr["AMENDED_FORWARDED_BY"] = dr0["AMENDED_FORWARDED_BY"];
                        dsr["AMENDED_ACCEPTED_BY"] = dr0["AMENDED_ACCEPTED_BY"];
                        dsr["AMENDED_QUALITY_ACCEPTED_BY"] = dr0["AMENDED_QUALITY_ACCEPTED_BY"];

                        dsr["PRODUCTION_MNGR_ID"] = dr0["PRODUCTION_MNGR_ID"];
                        dsr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"];

                        dsr["INTERNAL_INSPECTION_QTY"] = dr0["INTERNAL_INSPECTION_QTY"];
                        dsr["QA_ITEM_ACCEPTED_QTY"] = dr0["QA_ITEM_ACCEPTED_QTY"];
                        dsr["QA_ITEM_NOT_ACCEPTED_QTY"] = dr0["QA_ITEM_NOT_ACCEPTED_QTY"];

                        dsr["IN_FINAL_INSPECTION_QTY"] = dr0["IN_FINAL_INSPECTION_QTY"];
                        dsr["IN_REWORK_QTY"] = dr0["IN_REWORK_QTY"];


                        dsr["COMPLETE_QTY"] = dr0["COMPLETE_QTY"];

                        dsr["SI_ATTACHMENT1_NAME"] = dr0["SI_ATTACHMENT1_NAME"];
                        dsr["SI_ATTACHMENT2_NAME"] = dr0["SI_ATTACHMENT2_NAME"];
                        dsr["SI_ATTACHMENT3_NAME"] = dr0["SI_ATTACHMENT3_NAME"];
                        dsr["SI_ATTACHMENT4_NAME"] = dr0["SI_ATTACHMENT4_NAME"];
                        dsr["IRN_ATTACHMENT_NAME"] = dr0["IRN_ATTACHMENT_NAME"];

                        dtSi.Rows.Add(dsr);

                        #endregion

                        //if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                        //    currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended) ||
                        //    currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                        //    currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved) ||
                        //    currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                        //    currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                        //{
                        //    dtSi.Rows.Add(dsr);
                        //}
                        //else
                        //{
                        //    if (Convert.ToInt32(dr0["PLANNING_ACCEPTED_BY"]) == 0)
                        //    {
                        //        dtSi.Rows.Add(dsr);
                        //    }
                        //}
                    }
                    else
                    {
                        if (currentStatusID != Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                        {
                            DataRow dsr = dtSi.NewRow();

                            #region DTSI Rows

                            dsr["SR_NO"] = dr0["SR_NO"];
                            dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];
                            dsr["STATUS_ID"] = dr0["STATUS_ID"];
                            dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                            dsr["LOT_TF_ID"] = dr0["LOT_TF_ID"];
                            dsr["SUBITEM_DESC"] = dr0["SUBITEM_DESC"];
                            dsr["TAG_NO"] = dr0["TAG_NO"];
                            dsr["LOT_MAIN_ITEM_ID"] = dr0["LOT_MAIN_ITEM_ID"];
                            dsr["LOT_MAIN_SUBITEM_ID"] = dr0["LOT_MAIN_SUBITEM_ID"];
                            dsr["LOT_MAIN_ITEM"] = dr0["LOT_MAIN_ITEM"];
                            dsr["DRAWING_NO"] = dr0["DRAWING_NO"];

                            dsr["REVISION_NO"] = dr0["REVISION_NO"];
                            dsr["REVISION_NO_TEXT"] = dr0["REVISION_NO_TEXT"];

                            dsr["CATEGORY_ID"] = dr0["CATEGORY_ID"];
                            dsr["CATEGORY"] = dr0["CATEGORY"];

                            dsr["TOTAL_QUANTITY"] = dr0["TOTAL_QUANTITY"];
                            dsr["QUANTITY"] = dr0["QUANTITY"];
                            dsr["ACTED_QUANTITY"] = dr0["ACTED_QUANTITY"];
                            dsr["REMAINING_QUANTITY"] = dr0["REMAINING_QUANTITY"];

                            dsr["PRODUCTION_ORDER_NO"] = dr0["PRODUCTION_ORDER_NO"];
                            dsr["EXPECTED_COMPLETION_DATE"] = dr0["EXPECTED_COMPLETION_DATE"];
                            dsr["CREATED_BY"] = dr0["CREATED_BY"];

                            dsr["APPROVED_BY"] = dr0["APPROVED_BY"];
                            dsr["PLANNING_ACCEPTED_BY"] = dr0["PLANNING_ACCEPTED_BY"];
                            dsr["FORWARDED_BY"] = dr0["FORWARDED_BY"];
                            dsr["ACCEPTED_BY"] = dr0["ACCEPTED_BY"];
                            dsr["QUALITY_ACCEPTED_BY"] = dr0["QUALITY_ACCEPTED_BY"];

                            dsr["SENT_TO_INTL_INSP_BY"] = dr0["SENT_TO_INTL_INSP_BY"];
                            dsr["QA_INTL_INSP_ACCEPTED_BY"] = dr0["QA_INTL_INSP_ACCEPTED_BY"];
                            dsr["QA_INTL_INSP_NOT_ACCEPTED_BY"] = dr0["QA_INTL_INSP_NOT_ACCEPTED_BY"];

                            dsr["COMPLETED_BY"] = dr0["COMPLETED_BY"];
                            dsr["REVISED_BY"] = dr0["REVISED_BY"];
                            dsr["AMENDMENT_COUNT"] = dr0["AMENDMENT_COUNT"];
                            dsr["AMENDMENT_BY"] = dr0["AMENDMENT_BY"];
                            dsr["PROD_AMENDMENT_BY"] = dr0["PROD_AMENDMENT_BY"];
                            dsr["AMENDED_BY"] = dr0["AMENDED_BY"];

                            dsr["AMENDED_APPROVED_BY"] = dr0["AMENDED_APPROVED_BY"];
                            dsr["AMENDED_PLANNING_ACCEPTED_BY"] = dr0["AMENDED_PLANNING_ACCEPTED_BY"];
                            dsr["AMENDED_FORWARDED_BY"] = dr0["AMENDED_FORWARDED_BY"];
                            dsr["AMENDED_ACCEPTED_BY"] = dr0["AMENDED_ACCEPTED_BY"];
                            dsr["AMENDED_QUALITY_ACCEPTED_BY"] = dr0["AMENDED_QUALITY_ACCEPTED_BY"];

                            dsr["PRODUCTION_MNGR_ID"] = dr0["PRODUCTION_MNGR_ID"];
                            dsr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"];

                            dsr["INTERNAL_INSPECTION_QTY"] = dr0["INTERNAL_INSPECTION_QTY"];
                            dsr["QA_ITEM_ACCEPTED_QTY"] = dr0["QA_ITEM_ACCEPTED_QTY"];
                            dsr["QA_ITEM_NOT_ACCEPTED_QTY"] = dr0["QA_ITEM_NOT_ACCEPTED_QTY"];

                            dsr["IN_FINAL_INSPECTION_QTY"] = dr0["IN_FINAL_INSPECTION_QTY"];
                            dsr["IN_REWORK_QTY"] = dr0["IN_REWORK_QTY"];

                            dsr["COMPLETE_QTY"] = dr0["COMPLETE_QTY"];

                            dsr["SI_ATTACHMENT1_NAME"] = dr0["SI_ATTACHMENT1_NAME"];
                            dsr["SI_ATTACHMENT2_NAME"] = dr0["SI_ATTACHMENT2_NAME"];
                            dsr["SI_ATTACHMENT3_NAME"] = dr0["SI_ATTACHMENT3_NAME"];
                            dsr["SI_ATTACHMENT4_NAME"] = dr0["SI_ATTACHMENT4_NAME"];
                            dsr["IRN_ATTACHMENT_NAME"] = dr0["IRN_ATTACHMENT_NAME"];

                            #endregion

                            if (Convert.ToInt32(dr0["PLANNING_ACCEPTED_BY"]) == 0)
                            {
                                dtSi.Rows.Add(dsr);
                            }
                        }
                    }
                }

                if (dtSi.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSi.Rows)
                    {
                        currentStatusID = Convert.ToInt32(dr["STATUS_ID"]);

                        if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                            currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                        {

                            if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == PEID || Convert.ToInt32(Session["EMP_RECORD_ID"]) == PMID)
                            {
                                btnUpdateSubitemStatus.Visible = true;
                                btnUpdateSubitemStatus.Text = "Approve LOT";

                                btnSendToAmendment.Visible = true;
                            }
                        }

                        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                        {
                            if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == createdById)
                            {
                                btnUpdateSubitemStatus.Visible = true;
                                btnUpdateSubitemStatus.Text = "Amend LOT";
                            }
                            else
                            {
                                btnSendToAmendment.Visible = true;
                            }
                        }

                        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                                 currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                        {

                            //for planning person
                            foreach (DataRow drac in dsSubitems.Tables[4].Select("ACCEPTER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                            {
                                btnUpdateSubitemStatus.Visible = true;
                                btnUpdateSubitemStatus.Text = "Accept LOT";

                                btnSendToAmendment.Visible = true;
                                break;
                            }
                        }

                        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                                 currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                        {

                            //for planning person
                            foreach (DataRow drac in dsSubitems.Tables[4].Select("ACCEPTER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                            {
                                btnUpdateSubitemStatus.Visible = true;
                                btnUpdateSubitemStatus.Text = "Forward LOT";
                                break;
                            }
                        }



                        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                                 currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                        {
                            if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(dr["PRODUCTION_MNGR_ID"]))
                            {
                                pnlPrint.Visible = true;
                                chkIsSendToPrint.Checked = true;
                                pnlPrintSettings.Visible = true;

                                if (dsSubitems.Tables[5].Rows.Count > 0)
                                {
                                    BindPrinters(dsSubitems.Tables[5]);
                                }

                                btnUpdateSubitemStatus.Visible = true;
                                btnUpdateSubitemStatus.Text = "Accept LOT";

                                btnSendToAmendmentByProduction.Visible = true;
                                break;
                            }
                        }

                        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                                 currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                        //currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                        //currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                        {
                            //btnUpdateSubitemStatus.Visible = true;
                            //btnUpdateSubitemStatus.Text = "Accept LOT";

                            hdQualityPersonCounts.Value = "0";

                            if (dsSubitems.Tables[7].Rows.Count > 0)
                            {
                                hdQualityPersonCounts.Value = Convert.ToString(dsSubitems.Tables[7].Rows.Count);

                                ddlFirstResponsiblePerson.DataSource = dsSubitems.Tables[7];
                                ddlFirstResponsiblePerson.DataTextField = "ACCEPTER";
                                ddlFirstResponsiblePerson.DataValueField = "ACCEPTER_ID";
                                ddlFirstResponsiblePerson.DataBind();
                                ddlFirstResponsiblePerson.Items.Insert(0, "Select");

                                ddlSecondResponsiblePerson.DataSource = dsSubitems.Tables[7];
                                ddlSecondResponsiblePerson.DataTextField = "ACCEPTER";
                                ddlSecondResponsiblePerson.DataValueField = "ACCEPTER_ID";
                                ddlSecondResponsiblePerson.DataBind();
                                ddlSecondResponsiblePerson.Items.Insert(0, "Select");

                                if (Convert.ToInt32(hdQualityPersonCounts.Value) == 1)
                                {
                                    ddlFirstResponsiblePerson.SelectedValue = Convert.ToString(dsSubitems.Tables[7].Rows[0]["ACCEPTER_ID"]);
                                    ddlSecondResponsiblePerson.SelectedValue = Convert.ToString(dsSubitems.Tables[7].Rows[0]["ACCEPTER_ID"]);

                                    ddlFirstResponsiblePerson.Enabled = false;
                                    ddlSecondResponsiblePerson.Enabled = false;
                                }
                                else
                                {
                                    ddlFirstResponsiblePerson.Enabled = true;
                                    ddlSecondResponsiblePerson.Enabled = true;
                                }
                            }






                            //for quality person
                            foreach (DataRow drac in dsSubitems.Tables[3].Select("ACCEPTER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                            {
                                btnUpdateSubitemStatus.Visible = true;
                                btnUpdateSubitemStatus.Text = "Accept LOT";
                                break;
                            }
                        }
                        //else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.Partial))
                        //{
                        //    //for planning person
                        //    foreach (DataRow drac in dsSubitems.Tables[4].Select("ACCEPTER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                        //    {
                        //        btnUpdateSubitemStatus.Visible = true;
                        //        btnUpdateSubitemStatus.Text = "Accept LOT";
                        //        break;
                        //    }
                        //}
                    }
                }
            }

            else if (buttonControlTypeID == Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFitupInsp))
            {
                foreach (DataRow dr0 in dsSubitems.Tables[0].Rows)
                {
                    quantity = 0;
                    actedQuantity = 0;
                    remainingQuantity = 0;


                    quantity = Convert.ToInt32(dr0["QUANTITY"]);
                    actedQuantity = Convert.ToInt32(dr0["INTERNAL_INSPECTION_QTY"]) - Convert.ToInt32(dr0["QA_ITEM_NOT_ACCEPTED_QTY"]);
                    remainingQuantity = quantity - actedQuantity;

                    DataRow dsr = dtSi.NewRow();

                    #region DTSI3 Rows

                    dsr["SR_NO"] = dr0["SR_NO"];
                    dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];

                    if (remainingQuantity == 0)
                    {
                        dsr["STATUS_ID"] = dr0["STATUS_ID"];
                        dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                    }
                    else
                    {
                        if (Convert.ToInt32(dr0["AMENDMENT_COUNT"]) > 0)
                            dsr["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                        else
                            dsr["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);

                        dsr["NEXT_STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                    }




                    dsr["SR_NO"] = dr0["SR_NO"];
                    dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];
                    dsr["STATUS_ID"] = dr0["STATUS_ID"];
                    dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                    dsr["LOT_TF_ID"] = dr0["LOT_TF_ID"];
                    dsr["SUBITEM_DESC"] = dr0["SUBITEM_DESC"];
                    dsr["TAG_NO"] = dr0["TAG_NO"];
                    dsr["LOT_MAIN_ITEM_ID"] = dr0["LOT_MAIN_ITEM_ID"];
                    dsr["LOT_MAIN_SUBITEM_ID"] = dr0["LOT_MAIN_SUBITEM_ID"];
                    dsr["LOT_MAIN_ITEM"] = dr0["LOT_MAIN_ITEM"];
                    dsr["DRAWING_NO"] = dr0["DRAWING_NO"];

                    dsr["REVISION_NO"] = dr0["REVISION_NO"];
                    dsr["REVISION_NO_TEXT"] = dr0["REVISION_NO_TEXT"];

                    dsr["CATEGORY_ID"] = dr0["CATEGORY_ID"];
                    dsr["CATEGORY"] = dr0["CATEGORY"];

                    dsr["TOTAL_QUANTITY"] = dr0["TOTAL_QUANTITY"];
                    dsr["QUANTITY"] = quantity;
                    dsr["ACTED_QUANTITY"] = actedQuantity;
                    dsr["REMAINING_QUANTITY"] = remainingQuantity;

                    dsr["PRODUCTION_ORDER_NO"] = dr0["PRODUCTION_ORDER_NO"];
                    dsr["EXPECTED_COMPLETION_DATE"] = dr0["EXPECTED_COMPLETION_DATE"];
                    dsr["CREATED_BY"] = dr0["CREATED_BY"];

                    dsr["APPROVED_BY"] = dr0["APPROVED_BY"];
                    dsr["PLANNING_ACCEPTED_BY"] = dr0["PLANNING_ACCEPTED_BY"];
                    dsr["FORWARDED_BY"] = dr0["FORWARDED_BY"];
                    dsr["ACCEPTED_BY"] = dr0["ACCEPTED_BY"];
                    dsr["QUALITY_ACCEPTED_BY"] = dr0["QUALITY_ACCEPTED_BY"];

                    dsr["SENT_TO_INTL_INSP_BY"] = dr0["SENT_TO_INTL_INSP_BY"];
                    dsr["QA_INTL_INSP_ACCEPTED_BY"] = dr0["QA_INTL_INSP_ACCEPTED_BY"];
                    dsr["QA_INTL_INSP_NOT_ACCEPTED_BY"] = dr0["QA_INTL_INSP_NOT_ACCEPTED_BY"];

                    dsr["COMPLETED_BY"] = dr0["COMPLETED_BY"];
                    dsr["REVISED_BY"] = dr0["REVISED_BY"];
                    dsr["AMENDMENT_COUNT"] = dr0["AMENDMENT_COUNT"];
                    dsr["AMENDMENT_BY"] = dr0["AMENDMENT_BY"];
                    dsr["PROD_AMENDMENT_BY"] = dr0["PROD_AMENDMENT_BY"];
                    dsr["AMENDED_BY"] = dr0["AMENDED_BY"];

                    dsr["AMENDED_APPROVED_BY"] = dr0["AMENDED_APPROVED_BY"];
                    dsr["AMENDED_PLANNING_ACCEPTED_BY"] = dr0["AMENDED_PLANNING_ACCEPTED_BY"];
                    dsr["AMENDED_FORWARDED_BY"] = dr0["AMENDED_FORWARDED_BY"];
                    dsr["AMENDED_ACCEPTED_BY"] = dr0["AMENDED_ACCEPTED_BY"];
                    dsr["AMENDED_QUALITY_ACCEPTED_BY"] = dr0["AMENDED_QUALITY_ACCEPTED_BY"];

                    dsr["PRODUCTION_MNGR_ID"] = dr0["PRODUCTION_MNGR_ID"];
                    dsr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"];

                    dsr["INTERNAL_INSPECTION_QTY"] = dr0["INTERNAL_INSPECTION_QTY"];
                    dsr["QA_ITEM_ACCEPTED_QTY"] = dr0["QA_ITEM_ACCEPTED_QTY"];
                    dsr["QA_ITEM_NOT_ACCEPTED_QTY"] = dr0["QA_ITEM_NOT_ACCEPTED_QTY"];

                    dsr["IN_FINAL_INSPECTION_QTY"] = dr0["IN_FINAL_INSPECTION_QTY"];
                    dsr["IN_REWORK_QTY"] = dr0["IN_REWORK_QTY"];

                    dsr["COMPLETE_QTY"] = dr0["COMPLETE_QTY"];

                    dsr["SI_ATTACHMENT1_NAME"] = dr0["SI_ATTACHMENT1_NAME"];
                    dsr["SI_ATTACHMENT2_NAME"] = dr0["SI_ATTACHMENT2_NAME"];
                    dsr["SI_ATTACHMENT3_NAME"] = dr0["SI_ATTACHMENT3_NAME"];
                    dsr["SI_ATTACHMENT4_NAME"] = dr0["SI_ATTACHMENT4_NAME"];
                    dsr["IRN_ATTACHMENT_NAME"] = dr0["IRN_ATTACHMENT_NAME"];

                    #endregion


                    if (Convert.ToInt32(dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                    {
                        if (remainingQuantity > 0)
                        {
                            dtSi.Rows.Add(dsr);
                        }
                    }
                }

                if (dtSi.Rows.Count > 0)
                {
                    btnSendToIntlInspection.Visible = true;
                }
            }


            else if (buttonControlTypeID == Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.AcceptItems))
            {
                foreach (DataRow dr0 in dsSubitems.Tables[0].Rows)
                {

                    quantity = 0;
                    actedQuantity = 0;
                    remainingQuantity = 0;

                    quantity = Convert.ToInt32(dr0["INTERNAL_INSPECTION_QTY"]) - Convert.ToInt32(dr0["QA_ITEM_NOT_ACCEPTED_QTY"]);
                    actedQuantity = Convert.ToInt32(dr0["QA_ITEM_ACCEPTED_QTY"]);
                    remainingQuantity = quantity - actedQuantity;


                    DataRow dsr = dtSi.NewRow();

                    #region DTSI Rows

                    dsr["SR_NO"] = dr0["SR_NO"];
                    dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];

                    if (remainingQuantity == 0)
                    {
                        dsr["STATUS_ID"] = dr0["STATUS_ID"];
                        dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                    }
                    else
                    {
                        dsr["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection);
                        dsr["NEXT_STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted);
                    }

                    dsr["SR_NO"] = dr0["SR_NO"];
                    dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];
                    dsr["STATUS_ID"] = dr0["STATUS_ID"];
                    dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                    dsr["LOT_TF_ID"] = dr0["LOT_TF_ID"];
                    dsr["SUBITEM_DESC"] = dr0["SUBITEM_DESC"];
                    dsr["TAG_NO"] = dr0["TAG_NO"];
                    dsr["LOT_MAIN_ITEM_ID"] = dr0["LOT_MAIN_ITEM_ID"];
                    dsr["LOT_MAIN_SUBITEM_ID"] = dr0["LOT_MAIN_SUBITEM_ID"];
                    dsr["LOT_MAIN_ITEM"] = dr0["LOT_MAIN_ITEM"];
                    dsr["DRAWING_NO"] = dr0["DRAWING_NO"];

                    dsr["REVISION_NO"] = dr0["REVISION_NO"];
                    dsr["REVISION_NO_TEXT"] = dr0["REVISION_NO_TEXT"];

                    dsr["CATEGORY_ID"] = dr0["CATEGORY_ID"];
                    dsr["CATEGORY"] = dr0["CATEGORY"];

                    dsr["TOTAL_QUANTITY"] = dr0["TOTAL_QUANTITY"];
                    dsr["QUANTITY"] = quantity;
                    dsr["ACTED_QUANTITY"] = actedQuantity;
                    dsr["REMAINING_QUANTITY"] = remainingQuantity;

                    dsr["PRODUCTION_ORDER_NO"] = dr0["PRODUCTION_ORDER_NO"];
                    dsr["EXPECTED_COMPLETION_DATE"] = dr0["EXPECTED_COMPLETION_DATE"];
                    dsr["CREATED_BY"] = dr0["CREATED_BY"];

                    dsr["APPROVED_BY"] = dr0["APPROVED_BY"];
                    dsr["PLANNING_ACCEPTED_BY"] = dr0["PLANNING_ACCEPTED_BY"];
                    dsr["FORWARDED_BY"] = dr0["FORWARDED_BY"];
                    dsr["ACCEPTED_BY"] = dr0["ACCEPTED_BY"];
                    dsr["QUALITY_ACCEPTED_BY"] = dr0["QUALITY_ACCEPTED_BY"];

                    dsr["SENT_TO_INTL_INSP_BY"] = dr0["SENT_TO_INTL_INSP_BY"];
                    dsr["QA_INTL_INSP_ACCEPTED_BY"] = dr0["QA_INTL_INSP_ACCEPTED_BY"];
                    dsr["QA_INTL_INSP_NOT_ACCEPTED_BY"] = dr0["QA_INTL_INSP_NOT_ACCEPTED_BY"];

                    dsr["COMPLETED_BY"] = dr0["COMPLETED_BY"];
                    dsr["REVISED_BY"] = dr0["REVISED_BY"];
                    dsr["AMENDMENT_COUNT"] = dr0["AMENDMENT_COUNT"];
                    dsr["AMENDMENT_BY"] = dr0["AMENDMENT_BY"];
                    dsr["PROD_AMENDMENT_BY"] = dr0["PROD_AMENDMENT_BY"];
                    dsr["AMENDED_BY"] = dr0["AMENDED_BY"];

                    dsr["AMENDED_APPROVED_BY"] = dr0["AMENDED_APPROVED_BY"];
                    dsr["AMENDED_PLANNING_ACCEPTED_BY"] = dr0["AMENDED_PLANNING_ACCEPTED_BY"];
                    dsr["AMENDED_FORWARDED_BY"] = dr0["AMENDED_FORWARDED_BY"];
                    dsr["AMENDED_ACCEPTED_BY"] = dr0["AMENDED_ACCEPTED_BY"];
                    dsr["AMENDED_QUALITY_ACCEPTED_BY"] = dr0["AMENDED_QUALITY_ACCEPTED_BY"];

                    dsr["PRODUCTION_MNGR_ID"] = dr0["PRODUCTION_MNGR_ID"];
                    dsr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"];

                    dsr["INTERNAL_INSPECTION_QTY"] = dr0["INTERNAL_INSPECTION_QTY"];
                    dsr["QA_ITEM_ACCEPTED_QTY"] = dr0["QA_ITEM_ACCEPTED_QTY"];
                    dsr["QA_ITEM_NOT_ACCEPTED_QTY"] = dr0["QA_ITEM_NOT_ACCEPTED_QTY"];

                    dsr["IN_FINAL_INSPECTION_QTY"] = dr0["IN_FINAL_INSPECTION_QTY"];
                    dsr["IN_REWORK_QTY"] = dr0["IN_REWORK_QTY"];

                    dsr["COMPLETE_QTY"] = dr0["COMPLETE_QTY"];

                    dsr["SI_ATTACHMENT1_NAME"] = dr0["SI_ATTACHMENT1_NAME"];
                    dsr["SI_ATTACHMENT2_NAME"] = dr0["SI_ATTACHMENT2_NAME"];
                    dsr["SI_ATTACHMENT3_NAME"] = dr0["SI_ATTACHMENT3_NAME"];
                    dsr["SI_ATTACHMENT4_NAME"] = dr0["SI_ATTACHMENT4_NAME"];
                    dsr["IRN_ATTACHMENT_NAME"] = dr0["IRN_ATTACHMENT_NAME"];



                    #endregion

                    if (remainingQuantity > 0)
                    {
                        dtSi.Rows.Add(dsr);
                    }


                }

                if (dtSi.Rows.Count > 0)
                {
                    btnQaIntlAccept.Visible = true;
                    btnQaIntlNotAccept.Visible = true;
                }
            }


            else if (buttonControlTypeID == Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFinalInsp))
            {
                foreach (DataRow dr0 in dsSubitems.Tables[0].Rows)
                {

                    quantity = 0;
                    actedQuantity = 0;
                    remainingQuantity = 0;


                    quantity = Convert.ToInt32(dr0["QA_ITEM_ACCEPTED_QTY"]);
                    actedQuantity = Convert.ToInt32(dr0["IN_FINAL_INSPECTION_QTY"]) - Convert.ToInt32(dr0["IN_REWORK_QTY"]);
                    remainingQuantity = quantity - actedQuantity;


                    //quantity = Convert.ToInt32(dr0["QA_ITEM_ACCEPTED_QTY"]) - Convert.ToInt32(dr0["IN_REWORK_QTY"]);
                    //actedQuantity = Convert.ToInt32(dr0["COMPLETE_QTY"]);
                    //remainingQuantity = quantity - actedQuantity;


                    DataRow dsr = dtSi.NewRow();

                    #region DTSI Rows

                    dsr["SR_NO"] = dr0["SR_NO"];
                    dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];

                    if (remainingQuantity == 0)
                    {
                        dsr["STATUS_ID"] = dr0["STATUS_ID"];
                        dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                    }
                    else
                    {
                        dsr["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted);
                        dsr["NEXT_STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                    }


                    dsr["SR_NO"] = dr0["SR_NO"];
                    dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];
                    dsr["STATUS_ID"] = dr0["STATUS_ID"];
                    dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                    dsr["LOT_TF_ID"] = dr0["LOT_TF_ID"];
                    dsr["SUBITEM_DESC"] = dr0["SUBITEM_DESC"];
                    dsr["TAG_NO"] = dr0["TAG_NO"];
                    dsr["LOT_MAIN_ITEM_ID"] = dr0["LOT_MAIN_ITEM_ID"];
                    dsr["LOT_MAIN_SUBITEM_ID"] = dr0["LOT_MAIN_SUBITEM_ID"];
                    dsr["LOT_MAIN_ITEM"] = dr0["LOT_MAIN_ITEM"];
                    dsr["DRAWING_NO"] = dr0["DRAWING_NO"];

                    dsr["REVISION_NO"] = dr0["REVISION_NO"];
                    dsr["REVISION_NO_TEXT"] = dr0["REVISION_NO_TEXT"];

                    dsr["CATEGORY_ID"] = dr0["CATEGORY_ID"];
                    dsr["CATEGORY"] = dr0["CATEGORY"];

                    dsr["TOTAL_QUANTITY"] = dr0["TOTAL_QUANTITY"];
                    dsr["QUANTITY"] = quantity;
                    dsr["ACTED_QUANTITY"] = actedQuantity;
                    dsr["REMAINING_QUANTITY"] = remainingQuantity;

                    dsr["PRODUCTION_ORDER_NO"] = dr0["PRODUCTION_ORDER_NO"];
                    dsr["EXPECTED_COMPLETION_DATE"] = dr0["EXPECTED_COMPLETION_DATE"];
                    dsr["CREATED_BY"] = dr0["CREATED_BY"];

                    dsr["APPROVED_BY"] = dr0["APPROVED_BY"];
                    dsr["PLANNING_ACCEPTED_BY"] = dr0["PLANNING_ACCEPTED_BY"];
                    dsr["FORWARDED_BY"] = dr0["FORWARDED_BY"];
                    dsr["ACCEPTED_BY"] = dr0["ACCEPTED_BY"];
                    dsr["QUALITY_ACCEPTED_BY"] = dr0["QUALITY_ACCEPTED_BY"];

                    dsr["SENT_TO_INTL_INSP_BY"] = dr0["SENT_TO_INTL_INSP_BY"];
                    dsr["QA_INTL_INSP_ACCEPTED_BY"] = dr0["QA_INTL_INSP_ACCEPTED_BY"];
                    dsr["QA_INTL_INSP_NOT_ACCEPTED_BY"] = dr0["QA_INTL_INSP_NOT_ACCEPTED_BY"];

                    dsr["COMPLETED_BY"] = dr0["COMPLETED_BY"];
                    dsr["REVISED_BY"] = dr0["REVISED_BY"];
                    dsr["AMENDMENT_COUNT"] = dr0["AMENDMENT_COUNT"];
                    dsr["AMENDMENT_BY"] = dr0["AMENDMENT_BY"];
                    dsr["PROD_AMENDMENT_BY"] = dr0["PROD_AMENDMENT_BY"];
                    dsr["AMENDED_BY"] = dr0["AMENDED_BY"];

                    dsr["AMENDED_APPROVED_BY"] = dr0["AMENDED_APPROVED_BY"];
                    dsr["AMENDED_PLANNING_ACCEPTED_BY"] = dr0["AMENDED_PLANNING_ACCEPTED_BY"];
                    dsr["AMENDED_FORWARDED_BY"] = dr0["AMENDED_FORWARDED_BY"];
                    dsr["AMENDED_ACCEPTED_BY"] = dr0["AMENDED_ACCEPTED_BY"];
                    dsr["AMENDED_QUALITY_ACCEPTED_BY"] = dr0["AMENDED_QUALITY_ACCEPTED_BY"];

                    dsr["PRODUCTION_MNGR_ID"] = dr0["PRODUCTION_MNGR_ID"];
                    dsr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"];

                    dsr["INTERNAL_INSPECTION_QTY"] = dr0["INTERNAL_INSPECTION_QTY"];
                    dsr["QA_ITEM_ACCEPTED_QTY"] = dr0["QA_ITEM_ACCEPTED_QTY"];
                    dsr["QA_ITEM_NOT_ACCEPTED_QTY"] = dr0["QA_ITEM_NOT_ACCEPTED_QTY"];

                    dsr["IN_FINAL_INSPECTION_QTY"] = dr0["IN_FINAL_INSPECTION_QTY"];
                    dsr["IN_REWORK_QTY"] = dr0["IN_REWORK_QTY"];

                    dsr["COMPLETE_QTY"] = dr0["COMPLETE_QTY"];

                    dsr["SI_ATTACHMENT1_NAME"] = dr0["SI_ATTACHMENT1_NAME"];
                    dsr["SI_ATTACHMENT2_NAME"] = dr0["SI_ATTACHMENT2_NAME"];
                    dsr["SI_ATTACHMENT3_NAME"] = dr0["SI_ATTACHMENT3_NAME"];
                    dsr["SI_ATTACHMENT4_NAME"] = dr0["SI_ATTACHMENT4_NAME"];
                    dsr["IRN_ATTACHMENT_NAME"] = dr0["IRN_ATTACHMENT_NAME"];



                    #endregion

                    if (remainingQuantity > 0)
                    {
                        dtSi.Rows.Add(dsr);
                    }
                }

                if (dtSi.Rows.Count > 0)
                {
                    btnSendToFinalInspection.Visible = true;
                }

            }


            else if (buttonControlTypeID == Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTListButtons.CompleteItems))
            {
                foreach (DataRow dr0 in dsSubitems.Tables[0].Rows)
                {

                    quantity = 0;
                    actedQuantity = 0;
                    remainingQuantity = 0;


                    quantity = Convert.ToInt32(dr0["IN_FINAL_INSPECTION_QTY"]) - Convert.ToInt32(dr0["IN_REWORK_QTY"]);
                    actedQuantity = Convert.ToInt32(dr0["COMPLETE_QTY"]);
                    remainingQuantity = quantity - actedQuantity;

                    DataRow dsr = dtSi.NewRow();

                    #region DTSI Rows

                    dsr["SR_NO"] = dr0["SR_NO"];
                    dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];

                    if (remainingQuantity == 0)
                    {
                        dsr["STATUS_ID"] = dr0["STATUS_ID"];
                        dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                    }
                    else
                    {
                        dsr["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection);
                        dsr["NEXT_STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete);
                    }


                    dsr["SR_NO"] = dr0["SR_NO"];
                    dsr["LOT_TF_SUBITEM_ID"] = dr0["LOT_TF_SUBITEM_ID"];
                    dsr["STATUS_ID"] = dr0["STATUS_ID"];
                    dsr["NEXT_STATUS_ID"] = dr0["NEXT_STATUS_ID"];
                    dsr["LOT_TF_ID"] = dr0["LOT_TF_ID"];
                    dsr["SUBITEM_DESC"] = dr0["SUBITEM_DESC"];
                    dsr["TAG_NO"] = dr0["TAG_NO"];
                    dsr["LOT_MAIN_ITEM_ID"] = dr0["LOT_MAIN_ITEM_ID"];
                    dsr["LOT_MAIN_SUBITEM_ID"] = dr0["LOT_MAIN_SUBITEM_ID"];
                    dsr["LOT_MAIN_ITEM"] = dr0["LOT_MAIN_ITEM"];
                    dsr["DRAWING_NO"] = dr0["DRAWING_NO"];

                    dsr["REVISION_NO"] = dr0["REVISION_NO"];
                    dsr["REVISION_NO_TEXT"] = dr0["REVISION_NO_TEXT"];

                    dsr["CATEGORY_ID"] = dr0["CATEGORY_ID"];
                    dsr["CATEGORY"] = dr0["CATEGORY"];

                    dsr["TOTAL_QUANTITY"] = dr0["TOTAL_QUANTITY"];
                    dsr["QUANTITY"] = quantity;
                    dsr["ACTED_QUANTITY"] = actedQuantity;
                    dsr["REMAINING_QUANTITY"] = remainingQuantity;

                    dsr["PRODUCTION_ORDER_NO"] = dr0["PRODUCTION_ORDER_NO"];
                    dsr["EXPECTED_COMPLETION_DATE"] = dr0["EXPECTED_COMPLETION_DATE"];
                    dsr["CREATED_BY"] = dr0["CREATED_BY"];

                    dsr["APPROVED_BY"] = dr0["APPROVED_BY"];
                    dsr["PLANNING_ACCEPTED_BY"] = dr0["PLANNING_ACCEPTED_BY"];
                    dsr["FORWARDED_BY"] = dr0["FORWARDED_BY"];
                    dsr["ACCEPTED_BY"] = dr0["ACCEPTED_BY"];
                    dsr["QUALITY_ACCEPTED_BY"] = dr0["QUALITY_ACCEPTED_BY"];

                    dsr["SENT_TO_INTL_INSP_BY"] = dr0["SENT_TO_INTL_INSP_BY"];
                    dsr["QA_INTL_INSP_ACCEPTED_BY"] = dr0["QA_INTL_INSP_ACCEPTED_BY"];
                    dsr["QA_INTL_INSP_NOT_ACCEPTED_BY"] = dr0["QA_INTL_INSP_NOT_ACCEPTED_BY"];

                    dsr["COMPLETED_BY"] = dr0["COMPLETED_BY"];
                    dsr["REVISED_BY"] = dr0["REVISED_BY"];
                    dsr["AMENDMENT_COUNT"] = dr0["AMENDMENT_COUNT"];
                    dsr["AMENDMENT_BY"] = dr0["AMENDMENT_BY"];
                    dsr["PROD_AMENDMENT_BY"] = dr0["PROD_AMENDMENT_BY"];
                    dsr["AMENDED_BY"] = dr0["AMENDED_BY"];

                    dsr["AMENDED_APPROVED_BY"] = dr0["AMENDED_APPROVED_BY"];
                    dsr["AMENDED_PLANNING_ACCEPTED_BY"] = dr0["AMENDED_PLANNING_ACCEPTED_BY"];
                    dsr["AMENDED_FORWARDED_BY"] = dr0["AMENDED_FORWARDED_BY"];
                    dsr["AMENDED_ACCEPTED_BY"] = dr0["AMENDED_ACCEPTED_BY"];
                    dsr["AMENDED_QUALITY_ACCEPTED_BY"] = dr0["AMENDED_QUALITY_ACCEPTED_BY"];

                    dsr["PRODUCTION_MNGR_ID"] = dr0["PRODUCTION_MNGR_ID"];
                    dsr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = dr0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"];

                    dsr["INTERNAL_INSPECTION_QTY"] = dr0["INTERNAL_INSPECTION_QTY"];
                    dsr["QA_ITEM_ACCEPTED_QTY"] = dr0["QA_ITEM_ACCEPTED_QTY"];
                    dsr["QA_ITEM_NOT_ACCEPTED_QTY"] = dr0["QA_ITEM_NOT_ACCEPTED_QTY"];

                    dsr["IN_FINAL_INSPECTION_QTY"] = dr0["IN_FINAL_INSPECTION_QTY"];
                    dsr["IN_REWORK_QTY"] = dr0["IN_REWORK_QTY"];

                    dsr["COMPLETE_QTY"] = dr0["COMPLETE_QTY"];

                    dsr["SI_ATTACHMENT1_NAME"] = dr0["SI_ATTACHMENT1_NAME"];
                    dsr["SI_ATTACHMENT2_NAME"] = dr0["SI_ATTACHMENT2_NAME"];
                    dsr["SI_ATTACHMENT3_NAME"] = dr0["SI_ATTACHMENT3_NAME"];
                    dsr["SI_ATTACHMENT4_NAME"] = dr0["SI_ATTACHMENT4_NAME"];
                    dsr["IRN_ATTACHMENT_NAME"] = dr0["IRN_ATTACHMENT_NAME"];



                    #endregion

                    if (remainingQuantity > 0)
                    {
                        dtSi.Rows.Add(dsr);
                    }
                }

                if (dtSi.Rows.Count > 0)
                {
                    pnlIRNAttachment.Visible = true;
                    btnCompleteItems.Visible = true;
                    btnSendItemsForRework.Visible = true;
                }

            }


            if (dtSi.Rows.Count > 0)
            {
                dsSubitems.Tables[0].Clear();
                foreach (DataRow d in dtSi.Rows)
                {
                    DataRow dn = dsSubitems.Tables[0].NewRow();

                    for (int i = 0; i < dsSubitems.Tables[0].Columns.Count; i++)
                    {
                        string dnI = dn[i].ToString();
                        string dI = d[i].ToString();

                        dn[i] = d[i];
                    }
                    dsSubitems.Tables[0].Rows.Add(dn);
                }
            }


            if (dsSubitems.Tables.Count > 0 && dsSubitems.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dsSubitems.Tables[0].Rows)
                {
                    i++;
                    dr["SR_NO"] = Convert.ToInt32(i);
                }
                Session["dtSubitems"] = dsSubitems.Tables[0];
                gvSubitemsSI.DataSource = dsSubitems.Tables[0];
                gvSubitemsSI.DataBind();
            }
            else
            {
                Session["dtSubitems"] = null;
                gvSubitemsSI.DataSource = null;
                gvSubitemsSI.DataBind();
            }
            lblSubitemsSIRerords.Text = "Records[" + gvSubitemsSI.Rows.Count + "]";

            //return dsSubitems.Tables[0];            
        }
        catch (Exception EX)
        {
            //return null;
        }
    }


    public byte[] concatAndAddContent(List<byte[]> pdfByteContent)
    {

        using (var ms = new MemoryStream())
        {
            using (var doc = new Document())
            {
                using (var copy = new PdfSmartCopy(doc, ms))
                {
                    doc.Open();

                    //Loop through each byte array
                    foreach (var p in pdfByteContent)
                    {

                        //Create a PdfReader bound to that byte array
                        using (var reader = new PdfReader(p))
                        {

                            //Add the entire document instead of page-by-page
                            copy.AddDocument(reader);
                        }
                    }

                    doc.Close();
                }
            }

            //Return just before disposing
            //return ms.ToArray();

            byte[] allBytes = ms.GetBuffer();
            ms.Flush();
            ms.Dispose();
            return allBytes;
        }
    }


    private void BindPrinters(DataTable dtPrinters)
    {
        try
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("PRINTER_NAME", typeof(string));

            if (dtPrinters.Rows.Count > 0)
            {
                foreach (DataRow drO in dtPrinters.Select("MANAGER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                {
                    DataRow drpn = dtNew.NewRow();
                    drpn["PRINTER_NAME"] = Convert.ToString(drO["PRINTER_NAME"]);
                    dtNew.Rows.Add(drpn);
                }

                if (dtNew.Rows.Count > 0)
                {
                    ddlPrinter.DataSource = dtNew;
                    ddlPrinter.DataValueField = "PRINTER_NAME";
                    ddlPrinter.DataTextField = "PRINTER_NAME";
                    ddlPrinter.DataBind();
                    ddlPrinter.Items.Insert(0, "Select");
                    ddlPrinter.SelectedIndex = 0;
                }
                else
                {
                    ddlPrinter.Items.Clear();
                    ddlPrinter.Items.Insert(0, "Select");
                    ddlPrinter.SelectedIndex = 0;
                }
            }
            else
            {
                ddlPrinter.Items.Clear();
                ddlPrinter.Items.Insert(0, "Select");
                ddlPrinter.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    //private void BindPrinters(DataTable dtPrinters)
    //{
    //    try
    //    {
    //        DataTable dtNew = new DataTable();
    //        dtNew.Columns.Add("PRINTER_ID", typeof(int));
    //        dtNew.Columns.Add("UNIT_ID", typeof(int));
    //        dtNew.Columns.Add("MANAGER_ID", typeof(int));
    //        dtNew.Columns.Add("PRINTER_NAME", typeof(string));
    //        dtNew.Columns.Add("PRINTER_EMAIL", typeof(string));

    //        DataTable dtInstalledPrinters = new DataTable();
    //        dtInstalledPrinters.Columns.Add("PRINTER_NAME", typeof(string));

    //        DataTable dtInstalledPrintersNew = new DataTable();
    //        dtInstalledPrintersNew.Columns.Add("PRINTER_NAME", typeof(string));

    //        string printername = string.Empty;

    //        ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
    //        objScope.Connect();

    //        SelectQuery selectQuery = new SelectQuery();
    //        selectQuery.QueryString = "Select * from win32_Printer";
    //        ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
    //        ManagementObjectCollection MOC = MOS.Get();
    //        int count = 0;
    //        foreach (ManagementObject mo in MOC)
    //        {
    //            count++;
    //            printername = (mo["Name"].ToString());

    //            DataRow dr = dtInstalledPrinters.NewRow();
    //            dr["PRINTER_NAME"] = printername;
    //            dtInstalledPrinters.Rows.Add(dr);
    //        }

    //        if (dtInstalledPrinters.Rows.Count > 0)
    //        {

    //            foreach (DataRow drO in dtPrinters.Select("MANAGER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
    //            {
    //                DataRow drn = dtNew.NewRow();
    //                drn["PRINTER_ID"] = Convert.ToInt32(drO["PRINTER_ID"]);
    //                drn["UNIT_ID"] = Convert.ToInt32(drO["UNIT_ID"]);
    //                drn["MANAGER_ID"] = Convert.ToInt32(drO["MANAGER_ID"]);
    //                drn["PRINTER_NAME"] = Convert.ToString(drO["PRINTER_NAME"]);
    //                drn["PRINTER_EMAIL"] = Convert.ToString(drO["PRINTER_EMAIL"]);

    //                dtNew.Rows.Add(drn);
    //            }


    //            if (dtNew.Rows.Count > 0)
    //            {
    //                foreach (DataRow dtpo in dtInstalledPrinters.Rows)
    //                {
    //                    foreach (DataRow dtn1 in dtNew.Select("PRINTER_NAME='" + Convert.ToString(dtpo["PRINTER_NAME"]) + "'"))
    //                    {
    //                        DataRow drpn = dtInstalledPrintersNew.NewRow();
    //                        drpn["PRINTER_NAME"] = Convert.ToString(dtn1["PRINTER_NAME"]);
    //                        dtInstalledPrintersNew.Rows.Add(drpn);
    //                    }
    //                }
    //            }


    //            if (dtInstalledPrintersNew.Rows.Count > 0)
    //            {
    //                ddlPrinter.DataSource = dtInstalledPrintersNew;
    //                ddlPrinter.DataValueField = "PRINTER_NAME";
    //                ddlPrinter.DataTextField = "PRINTER_NAME";
    //                ddlPrinter.DataBind();
    //                ddlPrinter.Items.Insert(0, "Select");
    //                ddlPrinter.SelectedIndex = 0;
    //            }
    //            else
    //            {
    //                ddlPrinter.Items.Clear();
    //                ddlPrinter.Items.Insert(0, "Select");
    //                ddlPrinter.SelectedIndex = 0;
    //            }



    //            //ddlPrinter.DataSource = dtInstalledPrinters;
    //            //ddlPrinter.DataValueField = "PRINTER_NAME";
    //            //ddlPrinter.DataTextField = "PRINTER_NAME";
    //            //ddlPrinter.DataBind();
    //            //ddlPrinter.Items.Insert(0, "Select");
    //            //ddlPrinter.SelectedIndex = 0;

    //            //foreach (DataRow dr in dtPrinters.Select("MANAGER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
    //            //{
    //            //    foreach (DataRow dri in dtInstalledPrinters.Rows)
    //            //    {
    //            //        if (Convert.ToString(dri["PRINTER_NAME"]) == Convert.ToString(dr["PRINTER_NAME"]))
    //            //        {
    //            //            ddlPrinter.SelectedValue = Convert.ToString(dr["PRINTER_NAME"]);
    //            //        }
    //            //    }
    //            //}
    //        }
    //        else
    //        {
    //            ddlPrinter.Items.Clear();
    //            ddlPrinter.Items.Insert(0, "Select");
    //            ddlPrinter.SelectedIndex = 0;
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}

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

            dsLOTList = objProject.GetLOTTFListTwo(startDate, endDate, LOTTFNo, statusID, unitID
                                                 , jobNo, customerName, LOTMainItemID, LOTMainSubitemID);

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

                #region MyRegion

                //extn = fileName.Split('.').Last();
                //if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                //{
                //    //ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);

                //    //imgFile.ImageUrl = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    //mpeShowImageFile.Show();

                //    //string url = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    //string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                //    //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                //    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                //}
                //else if (extn == "pdf" || extn == "PDF")
                //{
                //    ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);

                //    //iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "");
                //    //mpeShowPDFFile.Show();

                //    //string url = "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    //string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                //    //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                //    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                //}
                //else if (extn == "dwg" || extn == "DWG")
                //{
                //    ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);
                //}
                //else if (extn == "dxf" || extn == "DXF")
                //{
                //    ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);
                //}

                #endregion
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

                else if (fileType == "CLIENT_APPROVED_DRAWING1")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["CLIENT_APPROVED_DRAWING_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["CLIENT_APPROVED_DRAWING_NAME"]);
                }

                else if (fileType == "CLIENT_APPROVED_DRAWING2")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["CLIENT_APPROVED_DRAWING2_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["CLIENT_APPROVED_DRAWING2_NAME"]);
                }

                else if (fileType == "CLIENT_APPROVED_DRAWING3")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["CLIENT_APPROVED_DRAWING3_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["CLIENT_APPROVED_DRAWING3_NAME"]);
                }

                else if (fileType == "STANDARD_DRAWING")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["STANDARD_DRAWING_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["STANDARD_DRAWING_NAME"]);
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
                mpeAddSubitems.Show();
                return;
            }
            else
            {
                throw;
            }
        }
    }

    private void ApproveOrSendToAmendment(int actID)
    {
        try
        {
            string LOTTFSubitemIDs = string.Empty;
            string LOTMainSubItemIDs = string.Empty;
            int nextStatusID = 0;
            int createdByID = 0;
            int PEID = 0;
            int PMID = 0;
            int approvedByID = 0;
            int amendedApprovedByID = 0;
            int prodMngrID = 0;
            int isPartOfProductionReportID = 0;
            int acceptedByID = 0;
            int amendedAcceptedByID = 0;
            int amendmentByProdFlag = 0;
            int firstQualityPersonID = 0;
            int secondQualityPersonID = 0;


            string printerName = string.Empty;
            string paperName = string.Empty;
            int copies = 0;

            LOTTFID = 0;
            companyID = 0;
            LOTMainSubItemIDs = string.Empty;
            TFNo = string.Empty;
            remarks = string.Empty;

            if (Convert.ToInt32(ViewState["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

            if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0)
                companyID = Convert.ToInt32(ViewState["UNIT_ID"]);

            if (!string.IsNullOrEmpty(txtTFNoSI.Text))
                TFNo = txtTFNoSI.Text;

            if (!string.IsNullOrEmpty(txtRemarksSI.Text))
                remarks = txtRemarksSI.Text;


            #region LOT Subitems Detail

            LOTTFSubitemIDs = string.Empty;
            LOTMainSubItemIDs = string.Empty;
            nextStatusID = 0;
            createdByID = 0;
            PEID = 0;
            PMID = 0;

            printerName = string.Empty;
            paperName = string.Empty;
            copies = 0;


            statusID = 0;
            LOTTFSubitemID = 0;
            LOTMainSubitemID = 0;

            createdByID = 0;
            approvedByID = 0;
            amendedApprovedByID = 0;
            prodMngrID = 0;
            acceptedByID = 0;
            amendedAcceptedByID = 0;
            amendmentCount = 0;
            totalQuantity = 0;
            quantity = 0;
            partialQuantityFlag = 0;
            partialStatusID = 0;
            amendmentByProdFlag = 0;
            standardDrawingRemarks = string.Empty;
            standardDrawingRemoveID = 0;

            createdByID = Convert.ToInt32(ViewState["CREATED_BY_ID"]);
            PEID = Convert.ToInt32(ViewState["PE_ID"]);
            PMID = Convert.ToInt32(ViewState["PM_ID"]);

            if (Session["dsSubitemsSI"] != null)
                dsSubitemsSI = (DataSet)Session["dsSubitemsSI"];


            int value1 = 0;
            int value2 = 0;
            int count = 0;
            int returnVal = 0;

            if (LOTTFID > 0)
            {
                if (dsSubitemsSI.Tables.Count > 0)
                {
                    if (dsSubitemsSI.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drsi1 in dsSubitemsSI.Tables[1].Rows)
                        {
                            if (dsSubitemsSI.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow drsi0 in dsSubitemsSI.Tables[0].Select("LOT_MAIN_SUBITEM_ID='" + Convert.ToInt32(drsi1["LOT_MAIN_SUBITEM_ID"]) + "'"))
                                {
                                    statusID = Convert.ToInt32(drsi0["STATUS_ID"]);
                                    LOTTFSubitemID = Convert.ToInt32(drsi0["LOT_TF_SUBITEM_ID"]);
                                    LOTMainSubitemID = Convert.ToInt32(drsi0["LOT_MAIN_SUBITEM_ID"]);
                                    amendmentCount = Convert.ToInt32(drsi0["AMENDMENT_COUNT"]);
                                    prodMngrID = Convert.ToInt32(drsi0["PRODUCTION_MNGR_ID"]);
                                    isPartOfProductionReportID = Convert.ToInt32(drsi0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);
                                    createdByID = Convert.ToInt32(drsi0["CREATED_BY"]);
                                    approvedByID = Convert.ToInt32(drsi0["APPROVED_BY"]);
                                    amendedApprovedByID = Convert.ToInt32(drsi0["AMENDED_APPROVED_BY"]);

                                    acceptedByID = Convert.ToInt32(drsi0["ACCEPTED_BY"]);
                                    amendedAcceptedByID = Convert.ToInt32(drsi0["AMENDED_ACCEPTED_BY"]);
                                    //totalQuantity = Convert.ToInt32(drsi0["QUANTITY"]);
                                    quantity = Convert.ToInt32(drsi0["QUANTITY"]);

                                    if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(LOTTFSubitemID) + ","))
                                    {
                                        count++;
                                        if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(LOTTFSubitemID) + ",";
                                        }
                                        else
                                        {
                                            if (isPartOfProductionReportID > 0)
                                            {
                                                LOTTFSubitemIDs += Convert.ToString(LOTTFSubitemID) + ",";
                                            }
                                        }

                                        //returnVal = 0;
                                        //returnVal = CheckForQualityOrPlanningAcceptance(dsSubitemsSI.Tables[3], dsSubitemsSI.Tables[4]);
                                        //returnVal = CheckForPlanningAcceptance(dsSubitemsSI.Tables[4]);

                                        //if (returnVal > 0)
                                        //{
                                        //    nextStatusID = returnVal;

                                        //    value1 = objProject.UpdateQualityPlanningStatus(LOTTFID, LOTTFSubitemID, nextStatusID, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                        //    value2 = value2 + value1;
                                        //}
                                        //else
                                        //{
                                        if (actID == Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.SendToAmendment))
                                        {
                                            nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                                        }
                                        else if (actID == Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.SendToAmendmentByProduction))
                                        {
                                            amendmentByProdFlag = Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.SendToAmendmentByProduction);
                                            if (amendmentCount > 0)
                                                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                                            else
                                                nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                                        }
                                        else
                                        {
                                            if (amendmentCount > 0)
                                            {
                                                if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                                                    statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved);
                                                }

                                                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                                                         statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted);
                                                }

                                                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                                                         statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded);
                                                }

                                                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                                                         statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted);
                                                }

                                                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                                                         statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndQALOTAccepted);
                                                }
                                            }
                                            else
                                            {
                                                if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                                                    statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                                                }

                                                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                                                         statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted);
                                                }

                                                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.PlanningAccepted) ||
                                                         statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndPlanningAccepted))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded);
                                                }

                                                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Forwarded) ||
                                                         statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndForwarded))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted);
                                                }

                                                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                                                         statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                                                {
                                                    nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QALOTAccepted);
                                                }
                                            }
                                        }


                                        if (uploadFileStandardDrawing.HasFile)
                                        {
                                            if (!string.IsNullOrEmpty(uploadFileStandardDrawing.PostedFile.FileName))
                                            {
                                                string[] str = uploadFileStandardDrawing.PostedFile.FileName.Split('\\');
                                                int length = str.Length;
                                                standardDrawingFile = str[str.Length - 1];
                                                standardDrawingFileBytes = GetFileBytes(uploadFileStandardDrawing.PostedFile.FileName, uploadFileStandardDrawing.PostedFile.InputStream);
                                            }
                                        }


                                        if (!string.IsNullOrEmpty(txtStandardDrawingRemarks.Text))
                                            standardDrawingRemarks = txtStandardDrawingRemarks.Text;

                                        if (Convert.ToInt32(hdStandardDrawingRemoveID.Value) > 0)
                                            standardDrawingRemoveID = Convert.ToInt32(hdStandardDrawingRemoveID.Value);


                                        if (ddlFirstResponsiblePerson.SelectedIndex > 0)
                                            firstQualityPersonID = Convert.ToInt32(ddlFirstResponsiblePerson.SelectedValue);

                                        if (ddlSecondResponsiblePerson.SelectedIndex > 0)
                                            secondQualityPersonID = Convert.ToInt32(ddlSecondResponsiblePerson.SelectedValue);

                                        if (nextStatusID > 0)
                                        {
                                            value1 = objProject.UpdateLOTTFStatusTwo(LOTTFID, LOTTFSubitemID, quantity, partialQuantityFlag, nextStatusID, "", null, remarks,
                                                                                 amendmentByProdFlag,
                                                                                 standardDrawingFile,
                                                                                 standardDrawingFileBytes,
                                                                                 standardDrawingRemarks,
                                                                                 standardDrawingRemoveID,
                                                                                 firstQualityPersonID,
                                                                                 secondQualityPersonID,
                                                                                 Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            value2 = value2 + value1;
                                        }

                                        //}
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (value2 > 0)
            {
                //if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
                //    LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');

                if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                    LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');


                if (value2 == count)
                {



                    LOTSendMail objLOTSendMail = new LOTSendMail();
                    int sendMailValue = 0;
                    bool printVal = false;
                    bool isPrintMailSend = false;
                    string message = string.Empty;

                    //sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNoSI.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]),remarks, returnVal, 0, 0, 0, null);
                    //sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, txtTFNoSI.Text, companyID, remarks,
                    //                                            returnVal, 0, 0, 0, null, null, 0, 0);


                    if (actID == Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.SendToAmendmentByProduction))
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, txtTFNoSI.Text, companyID, remarks,
                                                               //returnVal, 
                                                               0, 0, null, 0, 1, 0);
                    }
                    else
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, txtTFNoSI.Text, companyID, remarks,
                                                               //returnVal, 
                                                               0, 0, null, 0, 0, 0);
                    }




                    if (sendMailValue > 0)
                    {
                        //int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTMainSubItemIDs, nextStatusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, nextStatusID, amendmentByProdFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        if (actID == Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.ApproveOrAccept))
                        {
                            if (amendmentCount == 0)
                                message = "LOT with TF. No.: '" + TFNo + "' updated and mail sent successfully...!!!";
                            else
                                message = "Amended LOT with TF. No.: '" + TFNo + "' updated and mail sent successfully...!!!";
                        }
                        else if (actID == Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.SendToAmendment))
                        {
                            message = "Amended LOT with TF. No.: '" + TFNo + "' sent to amendment and mail sent successfully...!!!";
                        }
                    }
                    else
                    {
                        if (actID == Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.ApproveOrAccept))
                        {
                            if (amendmentCount == 0)
                                message = "LOT with TF. No.: '" + TFNo + "' updated successfully...!!!";
                            else
                                message = "Amended LOT with TF. No.: '" + TFNo + "' updated successfully...!!!";
                        }
                        else if (actID == Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.SendToAmendment))
                        {
                            message = "Amended LOT with TF. No.: '" + TFNo + "' sent to amendment successfully...!!!";
                        }
                    }



                    if (actID == Convert.ToInt32(LOTAllStatusAndTypes.EnumActID.ApproveOrAccept) && chkIsSendToPrint.Checked)
                    {
                        printVal = false;
                        isPrintMailSend = false;

                        foreach (DataRow dr in dsSubitemsSI.Tables[0].Rows)
                        {
                            if (Convert.ToInt32(dr["NEXT_STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted) ||
                                Convert.ToInt32(dr["NEXT_STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                            {
                                isPrintMailSend = true;
                                LOTTFSubitemIDs += Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]) + ",";
                            }
                        }

                        if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                            LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                        if (isPrintMailSend)
                        {
                            printerName = Convert.ToString(ddlPrinter.SelectedValue);
                            paperName = Convert.ToString(ddlPaper.SelectedValue);
                            copies = Convert.ToInt32(txtCopies.Text);

                            printVal = objLOTPrintDocument.PrintPDF(LOTTFID, LOTTFSubitemIDs, printerName, paperName, copies);
                        }

                        if (printVal)
                        {
                            message += "\n Sent to printer successfully...!!!";
                        }
                    }

                    SuccessMessage(message);

                }
                else if (value2 < count)
                {
                    RemoveSubitems(LOTMainSubitemID);
                }
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }

            #endregion

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int CheckForPlanningAcceptance(DataTable dtP)
    {
        try
        {
            bool check2 = false;
            int statusIDVal = 0;

            if (dtP.Rows.Count > 0)
            {
                foreach (DataRow dr in dtP.Select("ACCEPTER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                {
                    check2 = true;
                    break;
                }
            }

            if (check2 == true)
                statusIDVal = Convert.ToInt32(LOTAllStatusAndTypes.EnumQualityAndPlanningStatus.PlanningAccepted);
            else
                statusIDVal = 0;


            return statusIDVal;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }



    private void SendToInernalInspectionAndQaAcceptance(int nextStatusID)
    {
        try
        {
            int nextStatusIDNew = 0;

            //DataTable dtQuantity = new DataTable();
            //dtQuantity.Columns.Add("LOT_TF_ID", typeof(int));
            //dtQuantity.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
            //dtQuantity.Columns.Add("QUANTITY", typeof(int));

            int LOTTFSubitemID = 0;
            string LOTMainSubItemIDs = string.Empty;
            string LOTTFSubItemIDs = string.Empty;

            IRNAttachmentFileBytes = null;
            IRNAttachmentFile = string.Empty;


            LOTTFID = 0;
            companyID = 0;
            LOTTFSubitemID = 0;
            LOTMainSubItemIDs = string.Empty;
            LOTTFSubItemIDs = string.Empty;
            TFNo = string.Empty;
            remarks = string.Empty;
            totalQuantity = 0;
            quantity = 0;

            actedTotalQuantity = 0;

            partialStatusID = 0;
            partialStatusIDOld = 0;
            partialQuantityFlagOld = 0;
            partialQuantityFlag = 0;

            if (Convert.ToInt32(ViewState["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

            if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0)
                companyID = Convert.ToInt32(ViewState["UNIT_ID"]);

            if (!string.IsNullOrEmpty(txtTFNoSI.Text))
                TFNo = txtTFNoSI.Text;

            if (!string.IsNullOrEmpty(txtRemarksSI.Text))
                remarks = txtRemarksSI.Text;

            if (Session["dsSubitemsSI"] != null)
                dsSubitemsSI = (DataSet)Session["dsSubitemsSI"];



            if (uploadFileIRNAttachment.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileIRNAttachment.PostedFile.FileName))
                {
                    string[] str = uploadFileIRNAttachment.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    IRNAttachmentFile = str[str.Length - 1];
                    IRNAttachmentFileBytes = GetFileBytes(uploadFileIRNAttachment.PostedFile.FileName, uploadFileIRNAttachment.PostedFile.InputStream);
                }
            }

            int value1 = 0;
            int value2 = 0;
            int count = 0;
            int returnVal = 0;

            int checkedCounts = 0;

            if (LOTTFID > 0)
            {
                if (gvSubitemsSI.Rows.Count > 0)
                {
                    foreach (GridViewRow gr in gvSubitemsSI.Rows)
                    {
                        CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                        Label lblLOTMainSubitemID = gr.FindControl("lblLOTMainSubitemID") as Label;
                        Label lblLOTTFSubitemID = gr.FindControl("lblLOTTFSubitemID") as Label;
                        Label lblRemainingQuantity = gr.FindControl("lblRemainingQuantity") as Label;

                        TextBox txtLOTQuantity = gr.FindControl("txtLOTQuantity") as TextBox;
                        Label lblQuantity = gr.FindControl("lblQuantity") as Label;
                        DropDownList ddlRemainingQuantity = gr.FindControl("ddlRemainingQuantity") as DropDownList;

                        if (chkSelect.Checked)
                        {
                            checkedCounts++;
                            LOTTFSubitemID = Convert.ToInt32(lblLOTTFSubitemID.Text);

                            if (!string.IsNullOrEmpty(txtLOTQuantity.Text))
                                totalQuantity = Convert.ToInt32(txtLOTQuantity.Text);
                            else totalQuantity = 0;

                            if (!string.IsNullOrEmpty(lblQuantity.Text))
                                actedTotalQuantity = Convert.ToInt32(lblQuantity.Text);
                            else actedTotalQuantity = 0;

                            quantity = Convert.ToInt32(ddlRemainingQuantity.SelectedValue);

                            if ((totalQuantity - actedTotalQuantity - quantity) == 0)
                                partialQuantityFlag = 0;
                            else partialQuantityFlag = 1;


                            //if (partialQuantityFlag > 0)
                            //{
                            //    if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.SendToIntlInsp))
                            //    {
                            //        nextStatusIDNew = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialIntlInspection);
                            //    }
                            //    else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.AcceptItems))
                            //    {
                            //        nextStatusIDNew = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemAccepted);
                            //    }

                            //    else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.NotAcceptItems))
                            //    {
                            //        nextStatusIDNew = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemNotAccepted);
                            //    }

                            //    else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.CompleteItems))
                            //    {
                            //        nextStatusIDNew = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialComplete);
                            //    }                                
                            //}
                            //else
                            //{
                            //    nextStatusIDNew = nextStatusID;                                
                            //}



                            if (partialQuantityFlag > 0)
                            {
                                partialQuantityFlagOld = partialQuantityFlag;

                                if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFitupInsp))
                                {
                                    partialStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFitupInspection);
                                }

                                else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.AcceptItems))
                                {
                                    partialStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemAccepted);
                                }

                                else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.NotAcceptItems))
                                {
                                    partialStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemNotAccepted);
                                }

                                else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.SendToFinalInsp))
                                {
                                    partialStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFinalInspection);
                                }

                                else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.Rework))
                                {
                                    partialStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialRework);
                                }

                                else if (hdButtonFlag.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTListButtons.CompleteItems))
                                {
                                    partialStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialComplete);
                                }

                                partialStatusIDOld = partialStatusID;
                            }
                            else
                            {
                                partialStatusID = 0;
                            }

                            if (partialStatusID > 0)
                            {
                                nextStatusIDNew = partialStatusID;
                            }
                            else
                            {
                                nextStatusIDNew = nextStatusID;

                                if (partialQuantityFlagOld > 0)
                                {
                                    partialQuantityFlag = partialQuantityFlagOld;
                                    partialStatusID = partialStatusIDOld;
                                }
                            }



                            count++;
                            LOTMainSubItemIDs += Convert.ToString(LOTMainSubitemID) + ",";
                            LOTTFSubItemIDs += Convert.ToString(LOTTFSubitemID) + ",";


                            //DataRow dr = dtQuantity.NewRow();
                            //dr["LOT_TF_ID"] = LOTTFID;
                            //dr["LOT_TF_SUBITEM_ID"] = LOTTFSubitemID;
                            //dr["QUANTITY"] = quantity;

                            //dtQuantity.Rows.Add(dr);

                            value1 = objProject.UpdateLOTTFStatusTwo(LOTTFID, LOTTFSubitemID, quantity, partialQuantityFlag, nextStatusIDNew, IRNAttachmentFile,
                                                                     IRNAttachmentFileBytes, remarks, 0,
                                                                     "", null, "", 0, 0, 0,
                                                                     Convert.ToInt32(Session["EMP_RECORD_ID"]));
                            value2 = value2 + value1;
                        }
                    }

                    if (checkedCounts == 0)
                    {
                        ExceptionMessage("Please select atleast 1 item...!!!");
                        return;
                    }
                }
            }

            if (value2 > 0)
            {
                if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
                    LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');

                if (!string.IsNullOrEmpty(LOTTFSubItemIDs))
                    LOTTFSubItemIDs = LOTTFSubItemIDs.TrimEnd(',');


                if (value2 == count)
                {
                    LOTSendMail objLOTSendMail = new LOTSendMail();

                    int sendMailValue = 0;
                    if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection) ||
                        nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFitupInspection))
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubItemIDs, txtTFNoSI.Text, companyID, "",
                                                                    //returnVal,
                                                                    Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection),
                                                                    0, IRNAttachmentFileBytes, partialQuantityFlag, 0, 0);

                    }


                    else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted) ||
                             nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemAccepted))
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubItemIDs, txtTFNoSI.Text, companyID, "",
                                                                    //returnVal,
                                                                    Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted),
                                                                    0, IRNAttachmentFileBytes, partialQuantityFlag, 0, 0);
                    }


                    else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted) ||
                             nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemNotAccepted))
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubItemIDs, txtTFNoSI.Text, companyID, remarks,
                                                                    //returnVal,
                                                                    Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted),
                                                                    0, IRNAttachmentFileBytes, partialQuantityFlag, 0, 0);
                    }


                    else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection) ||
                             nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFinalInspection))
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubItemIDs, txtTFNoSI.Text, companyID, remarks,
                                                                    //returnVal,
                                                                    Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection),
                                                                    0, IRNAttachmentFileBytes, partialQuantityFlag, 0, 0);
                    }


                    else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework) ||
                             nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialRework))
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubItemIDs, txtTFNoSI.Text, companyID, remarks,
                                                                    //returnVal,
                                                                    Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework),
                                                                    0, IRNAttachmentFileBytes, partialQuantityFlag, 0, 0);
                    }


                    else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete) ||
                             nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialComplete))
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubItemIDs, txtTFNoSI.Text, companyID, "",
                                                                    //returnVal,
                                                                    Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete),
                                                                    0, IRNAttachmentFileBytes, partialQuantityFlag, 0, 0);
                    }


                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubItemIDs, nextStatusIDNew, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection) ||
                            nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFitupInspection))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' sent to fitup inspection and mail sent successfully.");
                        }

                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemAccepted))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' accepted by Quality for fitup inspection and mail sent successfully.");
                        }

                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemNotAccepted))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' not accepted by Quality for fitup inspection and mail sent successfully.");
                        }


                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFinalInspection))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' sent for final inspection and mail sent successfully.");
                        }

                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialRework))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' sent for rework and mail sent successfully.");
                        }


                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialComplete))

                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' completed by Quality with final inspection and mail sent successfully.");
                        }
                    }
                    else
                    {
                        if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection) ||
                            nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFitupInspection))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' sent to fitup inspection successfully.");
                        }

                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemAccepted))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' accepted by Quality for fitup inspection successfully.");
                        }

                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FinalInspection) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialFinalInspection))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' sent for final inspection successfully.");
                        }

                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Rework) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialRework))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' sent for rework successfully.");
                        }



                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialQAItemNotAccepted))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' not accepted by Quality for fitup inspection successfully.");
                        }

                        else if (nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete) ||
                                 nextStatusIDNew == Convert.ToInt32(LOTAllStatusAndTypes.EnumPartialStatus.PartialComplete))
                        {
                            SuccessMessage("Item(s) with TF. No.: '" + TFNo + "' completed by Quality with final inspection successfully.");
                        }
                    }
                }
                else if (value2 < count)
                {
                    RemoveSubitems(LOTMainSubitemID);
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

    private void RemoveSubitems(int LOTMainSubitemID)
    {
        if (Session["dsSubitemsSI"] != null)
            dsSubitemsSI = (DataSet)Session["dsSubitemsSI"];


        if (dsSubitemsSI.Tables.Count > 0 && dsSubitemsSI.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsSubitemsSI.Tables[0].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubitemID + "'"))
            {
                dsSubitemsSI.Tables[0].Rows.Remove(dr);
            }
        }

        if (dsSubitemsSI.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsSubitemsSI.Tables[0].Rows.Count; i++)
            {
                dsSubitemsSI.Tables[0].Rows[i]["SR_NO"] = i + 1;
            }
        }

        gvSubitemsSI.DataSource = dsSubitemsSI.Tables[0];
        gvSubitemsSI.DataBind();

        lblSubitemsSIRerords.Text = "Subitems Records[" + gvSubitemsSI.Rows.Count + "]";

        mpeSubitemDetail.Show();
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

    private void ExceptionSubitemsMessage(string message)
    {
        pnlSubitemsMsg.Visible = true;
        lblSubitemsMsg.Text = message;
        lblSubitemsMsg.ForeColor = System.Drawing.Color.Red;
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

    #endregion



    #endregion POPULATE LIST END[===================]




    #region EDIT/AMENDMENT START[==================]


    #region EVENTS START[===================]

    protected void ddlLOTMainItemsToEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();
        BindLOTMainSubItemsToEdit();
    }



    // PRODUCTION ORDER NO DETAILS
    protected void btnGetProductionNumber_Click(object sender, EventArgs e)
    {

        HideAddSubitemPanel();
        //btnAddUpdateSubitemToList.Enabled = false;
        //txtExpectedCompletionDateToEdit.BackColor = System.Drawing.Color.LightYellow;

        txtJOBNoSearchPON.Text = txtJOBNoToEdit.Text;
        txtProductionOrderNoSearchPON.Text = string.Empty;
        mpeProductonOrderNoDetail.Show();

        if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0 && ViewState["UNIT_ID"] != null)
            GetProductionOrderNoDetailToEdit(Convert.ToInt32(ViewState["UNIT_ID"]));


        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();
    }

    protected void btnSearchProductionOrderNo_Click(object sender, EventArgs e)
    {
        mpeProductonOrderNoDetail.Show();

        if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0 && ViewState["UNIT_ID"] != null)
            GetProductionOrderNoDetailToEdit(Convert.ToInt32(ViewState["UNIT_ID"]));

        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();
    }

    protected void gvProductonOrderNoDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblProductionOrderNo = gvProductonOrderNoDetail.Rows[rowindex].FindControl("lblProductionOrderNo") as Label;
                Label lblProductionOrderDate = gvProductonOrderNoDetail.Rows[rowindex].FindControl("lblProductionOrderDate") as Label;
                //Label lblExpectedCompletionDate = gvProductonOrderNoDetail.Rows[rowindex].FindControl("lblExpectedCompletionDate") as Label;

                txtProductionOrderNoToEdit.Text = Convert.ToString(lblProductionOrderNo.Text).Trim();
                txtProductionOrderDateToEdit.Text = Convert.ToString(lblProductionOrderDate.Text);
                //txtExpectedCompletionDateToEdit.Text = Convert.ToString(lblExpectedCompletionDate.Text);

                txtUOMToEdit.Text = string.Empty;
                txtProductDescToEdit.Text = string.Empty;
                txtQuantityToEdit.Text = string.Empty;

                BindProductDetail(Convert.ToString(lblProductionOrderNo.Text));

                mpeAddSubitems.Show();
                mpeUpdateLOT.Show();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    protected void ddlProductCodeToEdit_SelectedIndexChanged(object sender, EventArgs e)
    {

        HideAddSubitemPanel();
        //btnAddUpdateSubitemToList.Enabled = false;
        //txtExpectedCompletionDateToEdit.BackColor = System.Drawing.Color.LightYellow;
        mpeAddSubitems.Show();

        txtUOMToEdit.Text = string.Empty;
        txtProductDescToEdit.Text = string.Empty;
        txtExpectedCompletionDateToEdit.Text = string.Empty;
        txtQuantityToEdit.Text = string.Empty;

        if (ddlProductCodeToEdit.SelectedIndex > 0)
        {
            DataTable dt = new DataTable();
            if (Session["dtProdDetail"] != null)
                dt = (DataTable)Session["dtProdDetail"];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Select("PRODUCT_CODE='" + Convert.ToString(ddlProductCodeToEdit.SelectedValue) + "'"))
                {
                    if (dr["UOM"] != DBNull.Value)
                        txtUOMToEdit.Text = Convert.ToString(dr["UOM"]);
                    else txtUOMToEdit.Text = string.Empty;

                    if (dr["PRODUCT_DESC"] != DBNull.Value)
                        txtProductDescToEdit.Text = Convert.ToString(dr["PRODUCT_DESC"]);
                    else txtProductDescToEdit.Text = string.Empty;

                    if (dr["EDDATE"] != DBNull.Value)
                    {
                        txtExpectedCompletionDateToEdit.Text = Convert.ToString(dr["EDDATE"]);
                        //CheckForExpectedCompletionDate(txtExpectedCompletionDateToEdit.Text);
                    }
                    else txtExpectedCompletionDateToEdit.Text = string.Empty;

                    if (dr["QUANTITY"] != DBNull.Value)
                        txtQuantityToEdit.Text = Convert.ToString(dr["QUANTITY"]);
                    else txtQuantityToEdit.Text = string.Empty;
                }
            }

            mpeAddSubitems.Show();
            mpeUpdateLOT.Show();
        }
        else
        {
            mpeAddSubitems.Show();
            mpeUpdateLOT.Show();
        }
    }





    // DMS DRAWING NO DETAILS
    protected void btnGetDMSDrawingNo_Click(object sender, EventArgs e)
    {
        txtJOBNoSearchDMS.Text = txtJOBNoToEdit.Text;
        mpeDMSDrawingNoList.Show();
        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();

        if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0 && ViewState["UNIT_ID"] != null)
            GetDMSDrawingList(Convert.ToInt32(ViewState["UNIT_ID"]));
    }

    protected void btnSearchDMSDrawingNo_Click(object sender, EventArgs e)
    {
        btnGetDMSDrawingNo_Click(sender, e);
    }

    protected void gvDMSDrawingNoList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                txtDrgNoToEdit.Text = string.Empty;

                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblDrawingNo = gvDMSDrawingNoList.Rows[rowindex].FindControl("lblDrawingNo") as Label;

                txtDrgNoToEdit.Text = lblDrawingNo.Text.Trim().ToUpper();
                mpeAddSubitems.Show();
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








    //ADD UPDATE SUBITEMS
    protected void btnAddSubitem_Click(object sender, EventArgs e)
    {
        imgBtnUndoSiDrawingToEdit1.Visible = false;
        imgBtnUndoSiDrawingToEdit2.Visible = false;
        imgBtnUndoSiDrawingToEdit3.Visible = false;
        imgBtnUndoSiDrawingToEdit4.Visible = false;

        pnlViewSiDrawingToEdit1.Visible = false;
        pnlViewSiDrawingToEdit2.Visible = false;
        pnlViewSiDrawingToEdit3.Visible = false;
        pnlViewSiDrawingToEdit4.Visible = false;

        pnlUploadSiDrawingToEdit1.Visible = true;
        pnlUploadSiDrawingToEdit2.Visible = true;
        pnlUploadSiDrawingToEdit3.Visible = true;
        pnlUploadSiDrawingToEdit4.Visible = true;


        //Added on 2022-05-30
        HideAddSubitemPanel();
        //txtExpectedCompletionDateToEdit.BackColor = System.Drawing.Color.LightYellow;
        //btnAddUpdateSubitemToList.Enabled = false;
        //-----------

        btnAddUpdateSubitemToList.Text = "Add Subitem";
        hdSubitemUpdationFlag.Value = "0";

        ResetSubitems();
        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();
    }

    protected void btnAddUpdateSubitemToList_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdSubitemUpdationFlag.Value) == 0)
            AddSubitems();
        else
            UpdateSubitems();

        mpeUpdateLOT.Show();
    }

    protected void gvSubItemToU_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                //sUID = 0;
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" || Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblSrNo = gvSubItemToU.Rows[rowindex].FindControl("lblSrNo") as Label;
                Label lblLOTTFMainSubitemID = gvSubItemToU.Rows[rowindex].FindControl("lblLOTTFMainSubitemID") as Label;
                Label lblStatusID = gvSubItemToU.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblNextStatusID = gvSubItemToU.Rows[rowindex].FindControl("lblNextStatusID") as Label;
                Label lblLOTTFID = gvSubItemToU.Rows[rowindex].FindControl("lblLOTTFID") as Label;

                Label lblProductionNumber = gvSubItemToU.Rows[rowindex].FindControl("lblProductionNumber") as Label;
                Label lblProductionOrderDate = gvSubItemToU.Rows[rowindex].FindControl("lblProductionOrderDate") as Label;
                Label lblExpectedCompletionDate = gvSubItemToU.Rows[rowindex].FindControl("lblExpectedCompletionDate") as Label;
                Label lblProductCode = gvSubItemToU.Rows[rowindex].FindControl("lblProductCode") as Label;
                Label lblProductDesc = gvSubItemToU.Rows[rowindex].FindControl("lblProductDesc") as Label;
                Label lblUOM = gvSubItemToU.Rows[rowindex].FindControl("lblUOM") as Label;
                Label lblIsPartOfProductionStatusReport = gvSubItemToU.Rows[rowindex].FindControl("lblIsPartOfProductionStatusReport") as Label;

                TextBox txtTagNoInList = gvSubItemToU.Rows[rowindex].FindControl("txtTagNoInList") as TextBox;
                Label lblDescription = gvSubItemToU.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblLOTMainItemID = gvSubItemToU.Rows[rowindex].FindControl("lblLOTMainItemID") as Label;
                Label lblLOTMainSubitemID = gvSubItemToU.Rows[rowindex].FindControl("lblLOTMainSubitemID") as Label;
                Label lblLOTFor = gvSubItemToU.Rows[rowindex].FindControl("lblLOTFor") as Label;
                Label lblDrgOrDOCNo = gvSubItemToU.Rows[rowindex].FindControl("lblDrgOrDOCNo") as Label;

                Label lblRevNo = gvSubItemToU.Rows[rowindex].FindControl("lblRevNo") as Label;
                Label lblRevNoText = gvSubItemToU.Rows[rowindex].FindControl("lblRevNoText") as Label;

                Label lblCategory = gvSubItemToU.Rows[rowindex].FindControl("lblCategory") as Label;
                Label lblCategoryID = gvSubItemToU.Rows[rowindex].FindControl("lblCategoryID") as Label;
                Label lblQuantity = gvSubItemToU.Rows[rowindex].FindControl("lblQuantity") as Label;
                Label lblIsRevised = gvSubItemToU.Rows[rowindex].FindControl("lblIsRevised") as Label;

                ViewState["lblLOTTFMainSubitemID"] = Convert.ToInt32(lblLOTTFMainSubitemID.Text);


                foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
                {
                    item.Selected = false;
                }

                //hdRevNoTextToEdit.Value = "";
                //txtRevNoTextToEdit.Enabled = false;

                hdRevNoTextToEdit.Value = "";
                hdRevNoTextOldToEdit.Value = "";
                txtRevNoTextToEdit.Enabled = false;

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    hdRevNoTextToEdit.Value = lblRevNoText.Text;
                    hdRevNoTextOldToEdit.Value = lblRevNoText.Text;

                    if (Convert.ToInt32(lblRevNo.Text) == 99)
                    {
                        txtRevNoTextToEdit.Enabled = true;
                        txtRevNoTextToEdit.Text = lblRevNoText.Text;
                    }
                    else
                    {
                        hdRevNoTextToEdit.Value = Convert.ToString(lblRevNoText.Text);
                        txtRevNoTextToEdit.Text = hdRevNoTextToEdit.Value;
                    }

                    hdSubitemUpdationFlag.Value = "1";

                    imgBtnUndoSiDrawingToEdit1.Visible = true;
                    imgBtnUndoSiDrawingToEdit2.Visible = true;
                    imgBtnUndoSiDrawingToEdit3.Visible = true;
                    imgBtnUndoSiDrawingToEdit4.Visible = true;

                    pnlViewSiDrawingToEdit1.Visible = true;
                    pnlViewSiDrawingToEdit2.Visible = true;
                    pnlViewSiDrawingToEdit3.Visible = true;
                    pnlViewSiDrawingToEdit4.Visible = true;

                    pnlUploadSiDrawingToEdit1.Visible = false;
                    pnlUploadSiDrawingToEdit2.Visible = false;
                    pnlUploadSiDrawingToEdit3.Visible = false;
                    pnlUploadSiDrawingToEdit4.Visible = false;

                    if (!string.IsNullOrEmpty(lblSrNo.Text))
                        hdSRNo.Value = lblSrNo.Text;
                    else hdSRNo.Value = "0";

                    if (!string.IsNullOrEmpty(lblProductionNumber.Text))
                        txtProductionOrderNoToEdit.Text = lblProductionNumber.Text;
                    else txtProductionOrderNoToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblExpectedCompletionDate.Text))
                        txtExpectedCompletionDateToEdit.Text = lblExpectedCompletionDate.Text;
                    else txtExpectedCompletionDateToEdit.Text = string.Empty;


                    if (!string.IsNullOrEmpty(lblProductionNumber.Text))
                        txtProductionOrderNoToEdit.Text = lblProductionNumber.Text;
                    else txtProductionOrderNoToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblProductionOrderDate.Text))
                        txtProductionOrderDateToEdit.Text = lblProductionOrderDate.Text;
                    else txtProductionOrderDateToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblExpectedCompletionDate.Text))
                    {
                        txtExpectedCompletionDateToEdit.Text = lblExpectedCompletionDate.Text;
                        //CheckForExpectedCompletionDate(txtExpectedCompletionDateToEdit.Text);
                    }
                    else txtExpectedCompletionDateToEdit.Text = string.Empty;

                    BindProductDetail(lblProductionNumber.Text);
                    if (!string.IsNullOrEmpty(lblProductCode.Text) && Session["dtProdDetail"] != null)
                        ddlProductCodeToEdit.SelectedValue = lblProductCode.Text;
                    else ddlProductCodeToEdit.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(lblProductDesc.Text))
                        txtProductDescToEdit.Text = lblProductDesc.Text;
                    else txtProductDescToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblUOM.Text))
                        txtUOMToEdit.Text = lblUOM.Text;
                    else txtUOMToEdit.Text = string.Empty;

                    if (Convert.ToInt32(lblIsPartOfProductionStatusReport.Text) > 0)
                        chkIsPartOfProductionOrMainDrawingToEdit.Checked = true;
                    else chkIsPartOfProductionOrMainDrawingToEdit.Checked = false;


                    BindLOTMainItemsToEdit();
                    ddlLOTMainItemsToEdit.SelectedValue = Convert.ToString(lblLOTMainItemID.Text);

                    BindLOTMainSubItemsToEdit();
                    ddlLOTMainSubitemsToEdit.SelectedValue = Convert.ToString(lblLOTMainSubitemID.Text);

                    if (!string.IsNullOrEmpty(lblDrgOrDOCNo.Text))
                        txtDrgNoToEdit.Text = lblDrgOrDOCNo.Text;
                    else txtDrgNoToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblDescription.Text))
                        txtDescriptionToEdit.Text = lblDescription.Text;
                    else txtDescriptionToEdit.Text = string.Empty;

                    //if (!string.IsNullOrEmpty(lblRevNo.Text))
                    //    txtRevNoToEdit.Text = Convert.ToString(lblRevNo.Text);
                    //else txtRevNoToEdit.Text = "0";

                    ddlRevNoToEdit.SelectedValue = Convert.ToString(lblRevNo.Text);
                    //hdRevNoTextToEdit.Value = Convert.ToString(lblRevNo.Text);
                    //txtRevNoTextToEdit.Text = hdRevNoTextToEdit.Value;


                    string[] str = lblCategoryID.Text.Split(',');
                    foreach (string item in str)
                    {
                        chkLstCategoryToEdit.Items[Convert.ToInt32(item) - 1].Selected = true;
                    }

                    if (!string.IsNullOrEmpty(lblQuantity.Text))
                    {
                        hdQuantityToEdit.Value = lblQuantity.Text;
                        txtQuantityToEdit.Text = lblQuantity.Text;
                    }
                    else
                    {
                        hdQuantityToEdit.Value = string.Empty;
                        txtQuantityToEdit.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(txtTagNoInList.Text))
                        txtTagNoToEdit.Text = txtTagNoInList.Text;
                    else txtTagNoToEdit.Text = string.Empty;


                    btnAddUpdateSubitemToList.Text = "Update Subitem";


                    #region DRAWINGS

                    string attachment1Extn = string.Empty;
                    string attachment2Extn = string.Empty;
                    string attachment3Extn = string.Empty;
                    string attachment4Extn = string.Empty;

                    Label lblAttachment1 = gvSubItemToU.Rows[rowindex].FindControl("lblAttachment1") as Label;
                    Label lblAttachment2 = gvSubItemToU.Rows[rowindex].FindControl("lblAttachment2") as Label;
                    Label lblAttachment3 = gvSubItemToU.Rows[rowindex].FindControl("lblAttachment3") as Label;
                    Label lblAttachment4 = gvSubItemToU.Rows[rowindex].FindControl("lblAttachment4") as Label;

                    if (!string.IsNullOrEmpty(lblAttachment1.Text))
                    {
                        hdSiDrawingToEdit1.Value = "1";
                        hdUploadSiDrawingToEdit1.Value = "0";

                        pnlViewSiDrawingToEdit1.Visible = true;
                        pnlUploadSiDrawingToEdit1.Visible = false;

                        imgBtnViewSiDrawingToEdit1.Visible = true;

                        attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit1.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit1.Text = lblAttachment1.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit1.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit1.Text = lblAttachment1.Text;
                        }
                    }
                    else
                    {
                        hdSiDrawingToEdit1.Value = "0";
                        hdUploadSiDrawingToEdit1.Value = "1";

                        pnlViewSiDrawingToEdit1.Visible = false;
                        pnlUploadSiDrawingToEdit1.Visible = true;

                        imgBtnViewSiDrawingToEdit1.Visible = false;
                        imgBtnUndoSiDrawingToEdit1.Visible = false;
                    }





                    if (!string.IsNullOrEmpty(lblAttachment2.Text))
                    {
                        hdSiDrawingToEdit2.Value = "1";
                        hdUploadSiDrawingToEdit2.Value = "0";

                        pnlViewSiDrawingToEdit2.Visible = true;
                        pnlUploadSiDrawingToEdit2.Visible = false;

                        imgBtnViewSiDrawingToEdit2.Visible = true;

                        attachment1Extn = Convert.ToString(lblAttachment2.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit2.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit2.Text = lblAttachment2.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit2.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit2.Text = lblAttachment2.Text;
                        }

                        else if (attachment1Extn == "dwg" || attachment1Extn == "DWG")
                        {
                            imgBtnViewSiDrawingToEdit2.ImageUrl = "~/Images/LOT/dwg.png";
                            txtSiDrawingToEdit2.Text = lblAttachment2.Text;
                        }

                        else if (attachment1Extn == "dxf" || attachment1Extn == "DXF")
                        {
                            imgBtnViewSiDrawingToEdit2.ImageUrl = "~/Images/LOT/dxf.png";
                            txtSiDrawingToEdit2.Text = lblAttachment2.Text;
                        }

                        //else if (attachment2Extn == "dxf" || attachment2Extn == "DXF")
                        //{
                        //    imgBtnAttachment2.ImageUrl = "~/Images/LOT/dxf.png";
                        //    imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                        //}
                        //else if (attachment2Extn == "dwg" || attachment2Extn == "DWG")
                        //{
                        //    imgBtnAttachment2.ImageUrl = "~/Images/LOT/dwg.png";
                        //    imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                        //}
                    }
                    else
                    {
                        hdSiDrawingToEdit2.Value = "0";
                        hdUploadSiDrawingToEdit2.Value = "1";

                        pnlViewSiDrawingToEdit2.Visible = false;
                        pnlUploadSiDrawingToEdit2.Visible = true;

                        imgBtnViewSiDrawingToEdit2.Visible = false;
                        imgBtnUndoSiDrawingToEdit2.Visible = false;
                    }



                    if (!string.IsNullOrEmpty(lblAttachment3.Text))
                    {
                        hdSiDrawingToEdit3.Value = "1";
                        hdUploadSiDrawingToEdit3.Value = "0";

                        pnlViewSiDrawingToEdit3.Visible = true;
                        pnlUploadSiDrawingToEdit3.Visible = false;

                        imgBtnViewSiDrawingToEdit3.Visible = true;

                        attachment1Extn = Convert.ToString(lblAttachment3.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit3.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit3.Text = lblAttachment3.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit3.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit3.Text = lblAttachment3.Text;
                        }
                    }
                    else
                    {
                        hdSiDrawingToEdit3.Value = "0";
                        hdUploadSiDrawingToEdit3.Value = "1";

                        pnlViewSiDrawingToEdit3.Visible = false;
                        pnlUploadSiDrawingToEdit3.Visible = true;

                        imgBtnViewSiDrawingToEdit3.Visible = false;
                        imgBtnUndoSiDrawingToEdit3.Visible = false;
                    }




                    if (!string.IsNullOrEmpty(lblAttachment4.Text))
                    {
                        hdSiDrawingToEdit4.Value = "1";
                        hdUploadSiDrawingToEdit4.Value = "0";

                        pnlViewSiDrawingToEdit4.Visible = true;
                        pnlUploadSiDrawingToEdit4.Visible = false;

                        imgBtnViewSiDrawingToEdit4.Visible = true;

                        attachment1Extn = Convert.ToString(lblAttachment4.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit4.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit4.Text = lblAttachment4.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit4.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit4.Text = lblAttachment4.Text;
                        }
                    }
                    else
                    {
                        hdSiDrawingToEdit4.Value = "0";
                        hdUploadSiDrawingToEdit4.Value = "1";

                        pnlViewSiDrawingToEdit4.Visible = false;
                        pnlUploadSiDrawingToEdit4.Visible = true;

                        imgBtnViewSiDrawingToEdit4.Visible = false;
                        imgBtnUndoSiDrawingToEdit4.Visible = false;
                    }


                    #endregion


                    mpeUpdateLOT.Show();
                    mpeAddSubitems.Show();
                }

                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    RemoveUpdatedSubitems(Convert.ToInt32(lblSrNo.Text));
                    mpeUpdateLOT.Show();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
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
                Label lblTableName = (Label)e.Row.FindControl("lblTableName");


                if (!string.IsNullOrEmpty(lblTableName.Text))
                {
                    hdTableName.Value = lblTableName.Text;
                }

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




    //ADD UPDATE ATTACHMENTS
    protected void btnAddUpdateAttachments_Click(object sender, EventArgs e)
    {
        mpeAddUpdateAttachments.Show();
    }

    protected void btnAddUpdateAttachmentsToList_Click(object sender, EventArgs e)
    {
        AddUpdateAttachments();
        mpeUpdateLOT.Show();
    }

    protected void gvAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" || Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                    mpeAddUpdateAttachments.Show();

                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    if (Session["dtAttachments"] != null)
                        dtTempAttachments = (DataTable)Session["dtAttachments"];
                    else
                        AddTempAttachmentTable();

                    dtTempAttachments = (DataTable)Session["dtAttachments"];

                    dtTempAttachments.Clear();
                    gvAttachments.DataSource = null;
                    gvAttachments.DataBind();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
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




    protected void imgBtnRemoveSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit1.Value = "0";
        hdUploadSiDrawingToEdit1.Value = "1";

        pnlViewSiDrawingToEdit1.Visible = false;
        pnlUploadSiDrawingToEdit1.Visible = true;

        mpeUpdateLOT.Show();
        mpeAddSubitems.Show();
    }
    protected void imgBtnUndoSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit1.Value = "1";
        hdUploadSiDrawingToEdit1.Value = "0";

        pnlViewSiDrawingToEdit1.Visible = true;
        pnlUploadSiDrawingToEdit1.Visible = false;

        mpeUpdateLOT.Show();
        mpeAddSubitems.Show();
    }

    protected void imgBtnRemoveSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit2.Value = "0";
        hdUploadSiDrawingToEdit2.Value = "1";

        pnlViewSiDrawingToEdit2.Visible = false;
        pnlUploadSiDrawingToEdit2.Visible = true;

        mpeUpdateLOT.Show();
        mpeAddSubitems.Show();
    }
    protected void imgBtnUndoSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit2.Value = "1";
        hdUploadSiDrawingToEdit2.Value = "0";

        pnlViewSiDrawingToEdit2.Visible = true;
        pnlUploadSiDrawingToEdit2.Visible = false;

        mpeUpdateLOT.Show();
        mpeAddSubitems.Show();
    }

    protected void imgBtnRemoveSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit3.Value = "0";
        hdUploadSiDrawingToEdit3.Value = "1";

        pnlViewSiDrawingToEdit3.Visible = false;
        pnlUploadSiDrawingToEdit3.Visible = true;

        mpeUpdateLOT.Show();
        mpeAddSubitems.Show();
    }
    protected void imgBtnUndoSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit3.Value = "1";
        hdUploadSiDrawingToEdit3.Value = "0";

        pnlViewSiDrawingToEdit3.Visible = true;
        pnlUploadSiDrawingToEdit3.Visible = false;

        mpeUpdateLOT.Show();
        mpeAddSubitems.Show();
    }

    protected void imgBtnRemoveSiDrawingToEdit4_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit4.Value = "0";
        hdUploadSiDrawingToEdit4.Value = "1";

        pnlViewSiDrawingToEdit4.Visible = false;
        pnlUploadSiDrawingToEdit4.Visible = true;

        mpeUpdateLOT.Show();
        mpeAddSubitems.Show();
    }
    protected void imgBtnUndoSiDrawingToEdit4_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit4.Value = "1";
        hdUploadSiDrawingToEdit4.Value = "0";

        pnlViewSiDrawingToEdit4.Visible = true;
        pnlUploadSiDrawingToEdit4.Visible = false;

        mpeUpdateLOT.Show();
        mpeAddSubitems.Show();
    }



    protected void imgBtnViewSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFilesToEdit(0, "DRAWING1", txtSiDrawingToEdit1.Text.Trim(), Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]));

        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();
    }

    protected void imgBtnViewSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFilesToEdit(0, "DRAWING2", txtSiDrawingToEdit2.Text.Trim(), Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]));

        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();
    }

    protected void imgBtnViewSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFilesToEdit(0, "DRAWING3", txtSiDrawingToEdit3.Text.Trim(), Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]));

        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();
    }

    protected void imgBtnViewSiDrawingToEdit4_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFilesToEdit(0, "DRAWING4", txtSiDrawingToEdit4.Text.Trim(), Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]));

        mpeAddSubitems.Show();
        mpeUpdateLOT.Show();
    }




    protected void imgBtnViewStandardDrawing_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFilesToEdit(Convert.ToInt32(ViewState["LOTTFID"]), "STANDARD_DRAWING", txtViewStandardDrawing.Text.Trim(), 0);
    }




    protected void imgBtnRemoveStandardDrawing_Click(object sender, ImageClickEventArgs e)
    {
        hdStandardDrawingRemoveID.Value = "1";
        pnlViewStandardDrawing.Visible = false;
        pnlAddStandardDrawing.Visible = true;

        mpeSubitemDetail.Show();
    }

    protected void imgBtnUndoStandardDrawing_Click(object sender, ImageClickEventArgs e)
    {
        hdStandardDrawingRemoveID.Value = "0";
        pnlViewStandardDrawing.Visible = true;
        pnlAddStandardDrawing.Visible = false;

        mpeSubitemDetail.Show();
    }


    protected void chkIsSendToPrint_CheckedChanged(object sender, EventArgs e)
    {
        mpeSubitemDetail.Show();
        ddlPaper.SelectedValue = "A3";
        txtCopies.Text = "1";

        if (chkIsSendToPrint.Checked)
        {
            pnlPrintSettings.Visible = true;
        }
        else
        {
            pnlPrintSettings.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rdSavingType.SelectedValue) == Convert.ToInt32(LOTAllStatusAndTypes.SavingType.Save))
        {
            SaveLOTLOTTransmittalToFactory(Convert.ToInt32(LOTAllStatusAndTypes.SavingType.Save));
        }
        else if (Convert.ToInt32(rdSavingType.SelectedValue) == Convert.ToInt32(LOTAllStatusAndTypes.SavingType.SaveAndSendForApproval))
        {
            SaveLOTLOTTransmittalToFactory(Convert.ToInt32(LOTAllStatusAndTypes.SavingType.SaveAndSendForApproval));
        }

    }


    protected void btnPreview_Click(object sender, EventArgs e)
    {
        mpeUpdateLOT.Show();
        PreviewLOTTransmittalToFactory();
    }

    protected void btnSaveClientApprovedDrawing_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdClientDrawingConfirmValue.Value) > 0)
        {
            SaveClientApprovedDrawing(Convert.ToInt32(ViewState["LOTTFID"]));
        }
    }







    #endregion EVENTS END[==================]


    #region METHODS START[==================]

    //private void BindQualityResponsibleTeamList()
    //{
    //    try
    //    {
    //        DataTable dtNew = new DataTable();
    //        dtNew.Columns.Add("ACCEPTER_ID", typeof(int));
    //        dtNew.Columns.Add("ACCEPTER", typeof(string));


    //        DataTable dt = new DataTable();
    //        if (Session["dtQualityPersonList"] != null)
    //        {
    //            dt = (DataTable)Session["dtQualityPersonList"];
    //        }

    //        hdQualityPersonCounts.Value = "0";

    //        if (dt.Rows.Count > 0)
    //        {


    //            hdQualityPersonCounts.Value = Convert.ToString(dt.Rows.Count);

    //            ddlFirstResponsiblePerson.DataSource = dt;
    //            ddlFirstResponsiblePerson.DataTextField = "ACCEPTER";
    //            ddlFirstResponsiblePerson.DataValueField = "ACCEPTER_ID";
    //            ddlFirstResponsiblePerson.DataBind();
    //            ddlFirstResponsiblePerson.Items.Insert(0, "Select");

    //            ddlSecondResponsiblePerson.DataSource = dt;
    //            ddlSecondResponsiblePerson.DataTextField = "ACCEPTER";
    //            ddlSecondResponsiblePerson.DataValueField = "ACCEPTER_ID";
    //            ddlSecondResponsiblePerson.DataBind();
    //            ddlSecondResponsiblePerson.Items.Insert(0, "Select");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    private void AddTempSubitemTable()
    {
        dtSubitem.Columns.Add("SR_NO", typeof(int));
        dtSubitem.Columns.Add("STATUS_ID", typeof(int));
        dtSubitem.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
        dtSubitem.Columns.Add("LOT_MAIN_ITEM_ID", typeof(int));
        dtSubitem.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
        dtSubitem.Columns.Add("LOT_MAIN_ITEM", typeof(string));

        dtSubitem.Columns.Add("TAG_NO", typeof(string));
        dtSubitem.Columns.Add("SUBITEM_DESC", typeof(string));
        dtSubitem.Columns.Add("DRAWING_NO", typeof(string));
        dtSubitem.Columns.Add("REVISION_NO", typeof(int));
        dtSubitem.Columns.Add("CATEGORY_ID", typeof(string));
        dtSubitem.Columns.Add("CATEGORY", typeof(string));
        dtSubitem.Columns.Add("QUANTITY", typeof(int));

        dtSubitem.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
        dtSubitem.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));

        dtSubitem.Columns.Add("SI_ATTACHMENT1_NAME", typeof(string));
        dtSubitem.Columns.Add("SI_ATTACHMENT1_BTYTES", typeof(byte[]));

        dtSubitem.Columns.Add("SI_ATTACHMENT2_NAME", typeof(string));
        dtSubitem.Columns.Add("SI_ATTACHMENT2_BTYTES", typeof(byte[]));

        dtSubitem.Columns.Add("SI_ATTACHMENT3_NAME", typeof(string));
        dtSubitem.Columns.Add("SI_ATTACHMENT3_BTYTES", typeof(byte[]));

        dtSubitem.Columns.Add("SI_ATTACHMENT4_NAME", typeof(string));
        dtSubitem.Columns.Add("SI_ATTACHMENT4_BTYTES", typeof(byte[]));



        Session["dtSubitem"] = dtSubitem;
    }

    private void AddTempAttachmentTable()
    {
        dtAttachments.Columns.Add("ATTACHMENT1_NAME", typeof(string));
        dtAttachments.Columns.Add("ATTACHMENT1_BTYTES", typeof(byte[]));

        dtAttachments.Columns.Add("ATTACHMENT2_NAME", typeof(string));
        dtAttachments.Columns.Add("ATTACHMENT2_BTYTES", typeof(byte[]));

        dtAttachments.Columns.Add("ATTACHMENT3_NAME", typeof(string));
        dtAttachments.Columns.Add("ATTACHMENT3_BTYTES", typeof(byte[]));

        dtAttachments.Columns.Add("ATTACHMENT4_NAME", typeof(string));
        dtAttachments.Columns.Add("ATTACHMENT4_BTYTES", typeof(byte[]));

        Session["dtAttachments"] = dtAttachments;
    }

    private void BindProductDetail(string PONo)
    {
        try
        {
            companyID = 0;
            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            DataSet dsProdDetail = new DataSet();
            dsProdDetail = objProject.GetProductionOrderProdDetail(companyID, PONo);
            if (dsProdDetail.Tables.Count > 0 && dsProdDetail.Tables[0].Rows.Count > 0)
            {
                Session["dtProdDetail"] = dsProdDetail.Tables[0];
                ddlProductCodeToEdit.DataSource = dsProdDetail.Tables[0];
                ddlProductCodeToEdit.DataTextField = "PRODUCT_CODE_DESC";
                ddlProductCodeToEdit.DataValueField = "PRODUCT_CODE";
                ddlProductCodeToEdit.DataBind();
                ddlProductCodeToEdit.Items.Insert(0, "Select");
                ddlProductCodeToEdit.SelectedIndex = 0;
            }
            else
            {
                Session["dtProdDetail"] = null;
                ddlProductCodeToEdit.Items.Clear();
                ddlProductCodeToEdit.Items.Insert(0, "Select");
                ddlProductCodeToEdit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void BindLOTMainItemsToEdit()
    {
        try
        {
            dsLOTMainItems = objProject.GetLotMainItems();
            if (dsLOTMainItems.Tables.Count > 0 && dsLOTMainItems.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItemsToEdit.DataSource = dsLOTMainItems.Tables[0];
                ddlLOTMainItemsToEdit.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItemsToEdit.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItemsToEdit.DataBind();
                ddlLOTMainItemsToEdit.Items.Insert(0, "Select");
                ddlLOTMainItemsToEdit.SelectedIndex = 0;

                if (ddlLOTMainItemsToEdit.SelectedIndex > 0)
                {
                    BindLOTMainSubItemsToEdit();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainSubItemsToEdit()
    {
        try
        {
            ddlLOTMainSubitemsToEdit.Items.Clear();
            ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
            ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

            if (ddlLOTMainItemsToEdit.SelectedIndex > 0)
            {
                dsLOTMainSubItems = objProject.GetLotMainSubItems(Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue), Convert.ToInt32(ViewState["UNIT_ID"]));
                if (dsLOTMainSubItems.Tables.Count > 0 && dsLOTMainSubItems.Tables[0].Rows.Count > 0)
                {
                    ddlLOTMainSubitemsToEdit.DataSource = dsLOTMainSubItems.Tables[0];

                    ddlLOTMainSubitemsToEdit.DataTextField = "LOT_MAIN_SUBITEM";
                    ddlLOTMainSubitemsToEdit.DataValueField = "LOT_MAIN_SUBITEM_ID";
                    ddlLOTMainSubitemsToEdit.DataBind();
                    ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
                    ddlLOTMainSubitemsToEdit.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTCategoryForFactoryToEdit()
    {
        try
        {
            dsLOTCategoryForFactory = objProject.GetFactoryCategory();
            if (dsLOTCategoryForFactory.Tables.Count > 0 && dsLOTCategoryForFactory.Tables[0].Rows.Count > 0)
            {
                chkLstCategoryToEdit.DataSource = dsLOTCategoryForFactory.Tables[0];
                chkLstCategoryToEdit.DataTextField = "LOT_CATEGORY_NAME";
                chkLstCategoryToEdit.DataValueField = "LOT_CATEGORY_ID";
                chkLstCategoryToEdit.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }




    private void BindLOTTFDetailsToEdit(int LOTTFID)
    {
        try
        {
            PEApproverID = 0;
            PMApproverID = 0;
            amendmentCount = 0;

            hdAmendmentCountValue.Value = "0";

            dsLOTTFDetails = objProject.GetLOTTFDetailsForUpdateAndAmendment(LOTTFID);
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
                        hdDateToEdit.Value = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"]);
                        txtDateToEdit.Text = hdDateToEdit.Value;
                    }

                    if (dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"])))
                        txtItemNameToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"]);


                    if (dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"])))
                        txtNotesToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"]);
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


                if (dsLOTTFDetails.Tables[3].Rows.Count > 0)
                {
                    hdAmendmentCountValue.Value = Convert.ToString(dsLOTTFDetails.Tables[3].Rows[0]["AMENDMENT_COUNT"]);
                }
                else
                {
                    hdAmendmentCountValue.Value = "0";
                }

                //if (dsLOTTFDetails.Tables[3].Rows.Count > 0)
                //{
                //    if (dsLOTTFDetails.Tables[3].Rows[0]["PRODUCTION_MNGR_ID"] != DBNull.Value)
                //    {
                //        if (Convert.ToInt32(dsLOTTFDetails.Tables[3].Rows[0]["PRODUCTION_MNGR_ID"]) > 0)
                //        {
                //            if (Convert.ToInt32(dsLOTTFDetails.Tables[3].Rows[0]["PRODUCTION_MNGR_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                //                hdProductionManagerIDFlag.Value = "1";
                //            else
                //                hdProductionManagerIDFlag.Value = "0";
                //        }
                //        else
                //            hdProductionManagerIDFlag.Value = "0";
                //    }
                //    else
                //        hdProductionManagerIDFlag.Value = "0";
                //}




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




    //Production No.
    private DataSet GetProductionOrderNoDataToEdit(int companyID)
    {
        try
        {
            jobNo = string.Empty;
            productionOrderNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearchPON.Text))
                jobNo = txtJOBNoSearchPON.Text;

            if (!string.IsNullOrEmpty(txtProductionOrderNoSearchPON.Text))
                productionOrderNo = txtProductionOrderNoSearchPON.Text;

            dsProductionOrderNo = objProject.GetProductionOrderNoDetails(companyID, jobNo, productionOrderNo);

            if (dsProductionOrderNo.Tables.Count > 0)
            {
                return dsProductionOrderNo;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return null;
        }
    }

    private void GetProductionOrderNoDetailToEdit(int companyID)
    {
        try
        {
            dsProductionOrderNo = GetProductionOrderNoDataToEdit(companyID);
            if (dsProductionOrderNo.Tables.Count > 0 && dsProductionOrderNo.Tables[0].Rows.Count > 0)
            {
                lblProductonOrderNoMsg.Visible = false;
                lblProductonOrderNoMsg.Text = string.Empty;

                gvProductonOrderNoDetail.DataSource = dsProductionOrderNo.Tables[0];
                gvProductonOrderNoDetail.DataBind();
            }
            else
            {
                lblProductonOrderNoMsg.Visible = true;
                lblProductonOrderNoMsg.Text = "No data found!";

                gvProductonOrderNoDetail.DataSource = null;
                gvProductonOrderNoDetail.DataBind();
            }
            lblProductonOrderNoRecords.Text = "Records[" + gvProductonOrderNoDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
        }
    }



    //DMS Drawing No
    private DataSet GetDMSDrawingData(int companyID)
    {
        try
        {

            jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearchDMS.Text))
                jobNo = txtJOBNoSearchDMS.Text;

            dsDMSDrawingNo = objProject.GetDMSDrawingList(companyID, jobNo);

            if (dsDMSDrawingNo.Tables.Count > 0)
            {
                return dsDMSDrawingNo;
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

    private void GetDMSDrawingList(int companyID)
    {
        try
        {
            dsDMSDrawingNo = GetDMSDrawingData(companyID);
            if (dsDMSDrawingNo.Tables.Count > 0 && dsDMSDrawingNo.Tables[0].Rows.Count > 0)
            {
                lblDMSDrawingNoListMsg.Visible = false;
                lblDMSDrawingNoListMsg.Text = string.Empty;

                gvDMSDrawingNoList.DataSource = dsDMSDrawingNo.Tables[0];
                gvDMSDrawingNoList.DataBind();
            }
            else
            {
                lblDMSDrawingNoListMsg.Visible = true;
                lblDMSDrawingNoListMsg.Text = "No data found!";

                gvDMSDrawingNoList.DataSource = null;
                gvDMSDrawingNoList.DataBind();
            }
            lblDMSDrawingNoListRecords.Text = "Records[" + gvDMSDrawingNoList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }




    private void AddSubitems()
    {
        try
        {
            LOTMainItem = string.Empty;
            LOTMainItemID = 0;
            LOTMainSubItem = string.Empty;
            LOTMainSubItemID = 0;
            tagNo = string.Empty;
            subitemDesc = string.Empty;
            drgNo = string.Empty;

            revisionNo = 0;
            revisionNoText = string.Empty;

            categoryID = string.Empty;
            category = string.Empty;
            quantity = 0;

            productionOrderNo = string.Empty;
            productionOrderDate = string.Empty;
            expectedCompletionDate = string.Empty;
            productCode = string.Empty;
            productDesc = string.Empty;
            UOM = string.Empty;
            isPartOfProductionStatusID = 0;


            SiDrawing1File = string.Empty;
            SiDrawing2File = string.Empty;
            SiDrawing3File = string.Empty;
            SiDrawing4File = string.Empty;

            SiDrawing1FileBytes = null;
            SiDrawing2FileBytes = null;
            SiDrawing3FileBytes = null;
            SiDrawing4FileBytes = null;


            if (dtTemp != null && Session["dtSubitem"] != null)
                dtTemp = (DataTable)Session["dtSubitem"];
            else
                AddTempSubitemTable();

            dtTemp = (DataTable)Session["dtSubitem"];

            if (gvSubItemToU.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItemToU.Rows)
                {
                    Label lblSrNo = gr.FindControl("lblSrNo") as Label;
                    TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;

                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Select("SR_NO='" + Convert.ToInt32(lblSrNo.Text) + "'"))
                        {
                            dr["TAG_NO"] = txtTagNoInList.Text;
                        }
                    }
                }
            }


            LOTMainItem = Convert.ToString(ddlLOTMainItemsToEdit.SelectedItem.Text);
            LOTMainItemID = Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue);

            LOTMainSubItem = Convert.ToString(ddlLOTMainSubitemsToEdit.SelectedItem.Text);
            LOTMainSubItemID = Convert.ToInt32(ddlLOTMainSubitemsToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtTagNoToEdit.Text))
                tagNo = txtTagNoToEdit.Text.Trim().ToUpper().Replace(Environment.NewLine, " ");


            if (!string.IsNullOrEmpty(txtDescriptionToEdit.Text))
                subitemDesc = txtDescriptionToEdit.Text.Replace(Environment.NewLine, " "); ;

            if (!string.IsNullOrEmpty(txtDrgNoToEdit.Text))
                drgNo = txtDrgNoToEdit.Text.Trim().ToUpper();

            //if (!string.IsNullOrEmpty(txtRevNoToEdit.Text))
            //    revisionNo = Convert.ToInt32(txtRevNoToEdit.Text);

            revisionNo = Convert.ToInt32(ddlRevNoToEdit.SelectedValue);

            if (revisionNo == 99) //Other Revision
            {
                if (!string.IsNullOrEmpty(txtRevNoTextToEdit.Text))
                    revisionNoText = Convert.ToString(txtRevNoTextToEdit.Text);
            }
            else
                revisionNoText = Convert.ToString(hdRevNoTextToEdit.Value);


            foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
            {
                if (item.Selected)
                {
                    categoryID += item.Value + ",";
                    category += item.Text + ",";
                }
            }

            if (!string.IsNullOrEmpty(categoryID))
            {
                categoryID = categoryID.TrimEnd(',');
                category = category.TrimEnd(',');
            }


            if (!string.IsNullOrEmpty(txtQuantityToEdit.Text))
                quantity = Convert.ToInt32(txtQuantityToEdit.Text);

            if (!string.IsNullOrEmpty(txtProductionOrderNoToEdit.Text))
                productionOrderNo = txtProductionOrderNoToEdit.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtProductionOrderDateToEdit.Text))
                productionOrderDate = txtProductionOrderDateToEdit.Text;

            if (!string.IsNullOrEmpty(txtExpectedCompletionDateToEdit.Text))
            {
                expectedCompletionDate = txtExpectedCompletionDateToEdit.Text;
                //CheckForExpectedCompletionDate(txtExpectedCompletionDateToEdit.Text);
            }

            if (ddlProductCodeToEdit.SelectedIndex > 0)
                productCode = Convert.ToString(ddlProductCodeToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtProductDescToEdit.Text))
                productDesc = txtProductDescToEdit.Text;

            if (!string.IsNullOrEmpty(txtUOMToEdit.Text))
                UOM = txtUOMToEdit.Text;

            if (chkIsPartOfProductionOrMainDrawingToEdit.Checked)
            {
                isPartOfProductionStatus = "Yes";
                isPartOfProductionStatusID = 1;
            }
            else
            {
                isPartOfProductionStatus = "No";
                isPartOfProductionStatusID = 0;
            }


            if (uploadFileSiDrawingToEdit1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit1.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawingToEdit1.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing1File = str[str.Length - 1];
                    SiDrawing1FileBytes = GetFileBytes(uploadFileSiDrawingToEdit1.PostedFile.FileName, uploadFileSiDrawingToEdit1.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawingToEdit2.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit2.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawingToEdit2.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing2File = str[str.Length - 1];
                    SiDrawing2FileBytes = GetFileBytes(uploadFileSiDrawingToEdit2.PostedFile.FileName, uploadFileSiDrawingToEdit2.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawingToEdit3.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit3.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawingToEdit3.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing3File = str[str.Length - 1];
                    SiDrawing3FileBytes = GetFileBytes(uploadFileSiDrawingToEdit3.PostedFile.FileName, uploadFileSiDrawingToEdit3.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawingToEdit4.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit4.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawingToEdit4.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing4File = str[str.Length - 1];
                    SiDrawing4FileBytes = GetFileBytes(uploadFileSiDrawingToEdit4.PostedFile.FileName, uploadFileSiDrawingToEdit4.PostedFile.InputStream);
                }
            }


            int j = 0;
            for (int i = (gvSubItemToU.Rows.Count + 1); i < (gvSubItemToU.Rows.Count + 2); i++)
            {
                j++;

                DataRow dr = dtTemp.NewRow();

                dr["SR_NO"] = i;

                if (Convert.ToInt32(hdAmendmentCountValue.Value) == 0)
                {
                    dr["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                }
                else
                {
                    dr["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                }

                dr["LOT_TF_SUBITEM_ID"] = 0;
                dr["LOT_MAIN_ITEM_ID"] = LOTMainItemID;
                dr["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
                dr["LOT_MAIN_ITEM"] = LOTMainItem + " [" + LOTMainSubItem + "]";
                dr["TAG_NO"] = tagNo;
                dr["SUBITEM_DESC"] = subitemDesc;
                dr["DRAWING_NO"] = drgNo;

                dr["REVISION_NO"] = revisionNo;
                dr["REVISION_NO_TEXT"] = revisionNoText;

                dr["CATEGORY_ID"] = categoryID;
                dr["CATEGORY"] = category;
                dr["QUANTITY"] = quantity;

                dr["PRODUCTION_ORDER_NO"] = productionOrderNo.Trim();
                dr["PRODUCTION_ORDER_DATE"] = productionOrderDate;
                dr["EXPECTED_COMPLETION_DATE"] = expectedCompletionDate;
                dr["PRODUCT_CODE"] = productCode;
                dr["PRODUCT_DESC"] = productDesc;
                dr["UOM"] = UOM;
                dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;

                dr["SI_ATTACHMENT1_NAME"] = SiDrawing1File;
                dr["SI_ATTACHMENT1_BTYTES"] = SiDrawing1FileBytes;

                dr["SI_ATTACHMENT2_NAME"] = SiDrawing2File;
                dr["SI_ATTACHMENT2_BTYTES"] = SiDrawing2FileBytes;

                dr["SI_ATTACHMENT3_NAME"] = SiDrawing3File;
                dr["SI_ATTACHMENT3_BTYTES"] = SiDrawing3FileBytes;

                dr["SI_ATTACHMENT4_NAME"] = SiDrawing4File;
                dr["SI_ATTACHMENT4_BTYTES"] = SiDrawing4FileBytes;

                dtTemp.Rows.Add(dr);
            }

            gvSubItemToU.DataSource = dtTemp;
            gvSubItemToU.DataBind();
            lblSubitemsRecords.Text = "Subitems Records[" + gvSubItemToU.Rows.Count + "]";

            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    private void UpdateSubitems()
    {
        try
        {
            productionOrderNo = string.Empty;
            expectedCompletionDate = string.Empty;
            LOTMainItem = string.Empty;
            LOTMainItemID = 0;
            LOTMainSubItem = string.Empty;
            LOTMainSubItemID = 0;
            tagNo = string.Empty;
            subitemDesc = string.Empty;
            drgNo = string.Empty;
            revisionNo = 0;
            revisionNoText = "";
            categoryID = string.Empty;
            category = string.Empty;
            quantity = 0;

            productionOrderNo = string.Empty;
            productionOrderDate = string.Empty;
            expectedCompletionDate = string.Empty;
            productCode = string.Empty;
            productDesc = string.Empty;
            UOM = string.Empty;
            isPartOfProductionStatusID = 0;

            SiDrawing1File = string.Empty;
            SiDrawing2File = string.Empty;
            SiDrawing3File = string.Empty;
            SiDrawing4File = string.Empty;

            SiDrawing1FileBytes = null;
            SiDrawing2FileBytes = null;
            SiDrawing3FileBytes = null;
            SiDrawing4FileBytes = null;

            if (!string.IsNullOrEmpty(txtProductionOrderNoToEdit.Text))
                productionOrderNo = txtProductionOrderNoToEdit.Text;

            if (!string.IsNullOrEmpty(txtExpectedCompletionDateToEdit.Text))
            {
                expectedCompletionDate = txtExpectedCompletionDateToEdit.Text;
                //CheckForExpectedCompletionDate(txtExpectedCompletionDateToEdit.Text);
            }

            LOTMainItem = Convert.ToString(ddlLOTMainItemsToEdit.SelectedItem.Text);
            LOTMainItemID = Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue);

            LOTMainSubItem = Convert.ToString(ddlLOTMainSubitemsToEdit.SelectedItem.Text);
            LOTMainSubItemID = Convert.ToInt32(ddlLOTMainSubitemsToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtTagNoToEdit.Text))
                tagNo = txtTagNoToEdit.Text.Replace(Environment.NewLine, " "); ;


            if (!string.IsNullOrEmpty(txtDescriptionToEdit.Text))
                subitemDesc = txtDescriptionToEdit.Text.Replace(Environment.NewLine, " "); ;

            if (!string.IsNullOrEmpty(txtDrgNoToEdit.Text))
                drgNo = txtDrgNoToEdit.Text;

            //if (!string.IsNullOrEmpty(txtRevNoToEdit.Text))
            //    revisionNo = Convert.ToInt32(txtRevNoToEdit.Text);

            revisionNo = Convert.ToInt32(ddlRevNoToEdit.SelectedValue);

            if (revisionNo == 99) //Other Revision
            {
                if (!string.IsNullOrEmpty(txtRevNoTextToEdit.Text))
                    revisionNoText = Convert.ToString(txtRevNoTextToEdit.Text);
            }
            else
                revisionNoText = Convert.ToString(hdRevNoTextToEdit.Value);

            foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
            {
                if (item.Selected)
                {
                    categoryID += item.Value + ",";
                    category += item.Text + ",";
                }
            }

            if (!string.IsNullOrEmpty(categoryID))
            {
                categoryID = categoryID.TrimEnd(',');
                category = category.TrimEnd(',');
            }

            if (!string.IsNullOrEmpty(txtQuantityToEdit.Text))
                quantity = Convert.ToInt32(txtQuantityToEdit.Text);


            if (!string.IsNullOrEmpty(txtProductionOrderNoToEdit.Text))
                productionOrderNo = txtProductionOrderNoToEdit.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtProductionOrderDateToEdit.Text))
                productionOrderDate = txtProductionOrderDateToEdit.Text;

            if (!string.IsNullOrEmpty(txtExpectedCompletionDateToEdit.Text))
            {
                expectedCompletionDate = txtExpectedCompletionDateToEdit.Text;
                //CheckForExpectedCompletionDate(txtExpectedCompletionDateToEdit.Text);
            }

            if (ddlProductCodeToEdit.SelectedIndex > 0)
                productCode = Convert.ToString(ddlProductCodeToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtProductDescToEdit.Text))
                productDesc = txtProductDescToEdit.Text;

            if (!string.IsNullOrEmpty(txtUOMToEdit.Text))
                UOM = txtUOMToEdit.Text;

            if (chkIsPartOfProductionOrMainDrawingToEdit.Checked)
                isPartOfProductionStatusID = 1;
            else
                isPartOfProductionStatusID = 0;


            if (uploadFileSiDrawingToEdit1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit1.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawingToEdit1.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing1File = str[str.Length - 1];
                    SiDrawing1FileBytes = GetFileBytes(uploadFileSiDrawingToEdit1.PostedFile.FileName, uploadFileSiDrawingToEdit1.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawingToEdit2.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit2.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawingToEdit2.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing2File = str[str.Length - 1];
                    SiDrawing2FileBytes = GetFileBytes(uploadFileSiDrawingToEdit2.PostedFile.FileName, uploadFileSiDrawingToEdit2.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawingToEdit3.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit3.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawingToEdit3.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing3File = str[str.Length - 1];
                    SiDrawing3FileBytes = GetFileBytes(uploadFileSiDrawingToEdit3.PostedFile.FileName, uploadFileSiDrawingToEdit3.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawingToEdit4.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit4.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawingToEdit4.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing4File = str[str.Length - 1];
                    SiDrawing4FileBytes = GetFileBytes(uploadFileSiDrawingToEdit4.PostedFile.FileName, uploadFileSiDrawingToEdit4.PostedFile.InputStream);
                }
            }



            if (dtTemp != null && Session["dtSubitem"] != null)
                dtTemp = (DataTable)Session["dtSubitem"];
            else
                AddTempSubitemTable();

            dtTemp = (DataTable)Session["dtSubitem"];

            if (gvSubItemToU.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItemToU.Rows)
                {
                    Label lblSrNo = gr.FindControl("lblSrNo") as Label;
                    TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;

                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Select("SR_NO='" + Convert.ToInt32(lblSrNo.Text) + "'"))
                        {
                            dr["TAG_NO"] = txtTagNoInList.Text;
                        }
                    }
                }
            }


            if (dtTemp.Rows.Count > 0)
            {
                foreach (DataRow d in dtTemp.Select("SR_NO='" + Convert.ToInt32(hdSRNo.Value) + "'"))
                {
                    if (Convert.ToInt32(hdSRNo.Value) > 0)
                        d["SR_NO"] = Convert.ToString(hdSRNo.Value);
                    else d["SR_NO"] = "0";

                    if (Convert.ToInt32(hdAmendmentCountValue.Value) == 0)
                    {
                        d["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                    }
                    else
                    {
                        d["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    if (Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]) > 0 && ViewState["lblLOTTFMainSubitemID"] != null)
                        d["LOT_TF_SUBITEM_ID"] = Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]);

                    if (LOTMainItemID > 0)
                        d["LOT_MAIN_ITEM_ID"] = LOTMainItemID;
                    else d["LOT_MAIN_ITEM_ID"] = "0";

                    if (LOTMainSubItemID > 0)
                        d["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
                    else d["LOT_MAIN_SUBITEM_ID"] = "0";

                    d["LOT_MAIN_ITEM"] = LOTMainItem + " [" + LOTMainSubItem + "]";

                    if (!string.IsNullOrEmpty(tagNo))
                        d["TAG_NO"] = tagNo;
                    else d["TAG_NO"] = string.Empty;

                    if (!string.IsNullOrEmpty(subitemDesc))
                        d["SUBITEM_DESC"] = subitemDesc;
                    else d["SUBITEM_DESC"] = string.Empty;

                    if (!string.IsNullOrEmpty(drgNo))
                        d["DRAWING_NO"] = drgNo;
                    else d["DRAWING_NO"] = string.Empty;

                    if (revisionNo > 0)
                        d["REVISION_NO"] = revisionNo;
                    else d["REVISION_NO"] = "0";

                    if (!string.IsNullOrEmpty(revisionNoText))
                        d["REVISION_NO_TEXT"] = revisionNoText;
                    else d["REVISION_NO_TEXT"] = string.Empty;

                    if (!string.IsNullOrEmpty(categoryID))
                        d["CATEGORY_ID"] = categoryID;
                    else d["CATEGORY_ID"] = string.Empty;

                    if (!string.IsNullOrEmpty(category))
                        d["CATEGORY"] = category;
                    else d["CATEGORY"] = string.Empty;

                    if (quantity > 0)
                        d["QUANTITY"] = Convert.ToInt32(quantity);
                    else d["QUANTITY"] = 0;

                    //d["QUANTITY"] = 1;

                    if (!string.IsNullOrEmpty(productionOrderNo))
                        d["PRODUCTION_ORDER_NO"] = productionOrderNo;
                    else d["PRODUCTION_ORDER_NO"] = string.Empty;

                    if (!string.IsNullOrEmpty(productionOrderDate))
                        d["PRODUCTION_ORDER_DATE"] = productionOrderDate;
                    else d["PRODUCTION_ORDER_DATE"] = string.Empty;

                    if (!string.IsNullOrEmpty(expectedCompletionDate))
                        d["EXPECTED_COMPLETION_DATE"] = expectedCompletionDate;
                    else d["EXPECTED_COMPLETION_DATE"] = string.Empty;

                    if (!string.IsNullOrEmpty(productCode))
                        d["PRODUCT_CODE"] = productCode;
                    else d["PRODUCT_CODE"] = string.Empty;

                    if (!string.IsNullOrEmpty(productDesc))
                        d["PRODUCT_DESC"] = productDesc;
                    else d["PRODUCT_DESC"] = string.Empty;

                    if (!string.IsNullOrEmpty(UOM))
                        d["UOM"] = UOM;
                    else d["UOM"] = string.Empty;

                    //d["IS_PART_OF_PRODUCTION_STATUS_REPORT"] = isPartOfProductionStatus;
                    d["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;

                    if (Convert.ToInt32(hdSiDrawingToEdit1.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit1.Value) > 0)
                    {
                        if (!string.IsNullOrEmpty(SiDrawing1File) && SiDrawing1FileBytes != null)
                        {
                            d["SI_ATTACHMENT1_NAME"] = SiDrawing1File;
                            d["SI_ATTACHMENT1_BTYTES"] = SiDrawing1FileBytes;
                        }
                        else
                        {
                            d["SI_ATTACHMENT1_NAME"] = string.Empty;
                            d["SI_ATTACHMENT1_BTYTES"] = null;
                        }
                    }



                    if (Convert.ToInt32(hdSiDrawingToEdit2.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit2.Value) > 0)
                    {
                        if (!string.IsNullOrEmpty(SiDrawing2File) && SiDrawing2FileBytes != null)
                        {

                            d["SI_ATTACHMENT2_NAME"] = SiDrawing2File;
                            d["SI_ATTACHMENT2_BTYTES"] = SiDrawing2FileBytes;
                        }
                        else
                        {
                            d["SI_ATTACHMENT2_NAME"] = string.Empty;
                            d["SI_ATTACHMENT2_BTYTES"] = null;
                        }
                    }


                    if (Convert.ToInt32(hdSiDrawingToEdit3.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit3.Value) > 0)
                    {
                        if (!string.IsNullOrEmpty(SiDrawing3File) && SiDrawing3FileBytes != null)
                        {

                            d["SI_ATTACHMENT3_NAME"] = SiDrawing3File;
                            d["SI_ATTACHMENT3_BTYTES"] = SiDrawing3FileBytes;
                        }
                        else
                        {
                            d["SI_ATTACHMENT3_NAME"] = string.Empty;
                            d["SI_ATTACHMENT3_BTYTES"] = null;
                        }
                    }



                    if (Convert.ToInt32(hdSiDrawingToEdit4.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit4.Value) > 0)
                    {
                        if (!string.IsNullOrEmpty(SiDrawing4File) && SiDrawing4FileBytes != null)
                        {
                            d["SI_ATTACHMENT4_NAME"] = SiDrawing4File;
                            d["SI_ATTACHMENT4_BTYTES"] = SiDrawing4FileBytes;
                        }
                        else
                        {
                            d["SI_ATTACHMENT4_NAME"] = string.Empty;
                            d["SI_ATTACHMENT4_BTYTES"] = null;
                        }
                    }


                }

            }

            gvSubItemToU.DataSource = dtTemp;
            gvSubItemToU.DataBind();

            lblSubitemsRecords.Text = "Subitems Records[" + gvSubItemToU.Rows.Count + "]";

            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    private void CheckForExpectedCompletionDate(string expectedCompletionDate)
    {
        try
        {
            btnAddUpdateSubitemToList.Enabled = false;

            if (Convert.ToDateTime(expectedCompletionDate).Date >= DateTime.Now.Date)
            {
                btnAddUpdateSubitemToList.Enabled = true;
                HideAddSubitemPanel();
            }
            else
            {
                txtExpectedCompletionDateToEdit.BackColor = System.Drawing.Color.LightPink;
                ExceptionAddSubitemMessage("Completion Required By, must be greater than or equal to current date," + Environment.NewLine + "kindly make necessary changes in FACT(Production Order) to add product in LOT");
                mpeAddSubitems.Show();
                return;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void ViewDrawingFilesToEdit(int LOTTFID, string fileType, string fileName, int LOTTFSubitemID)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    //imgFile.ImageUrl = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                    //mpeShowImageFile.Show();

                    string url = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                    //string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                    //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    //iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "");
                    //mpeShowPDFFile.Show();

                    string url = "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                    //string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                    //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
            }
            else
            {
                ExceptionUpdateMessage("File Doesn't exist!");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    //private void RemoveSubitems(int LOTMainSubitemID)
    //{
    //    if (Session["dsSubitemsSI"] != null)
    //        dsSubitemsSI = (DataSet)Session["dsSubitemsSI"];


    //    if (dsSubitemsSI.Tables.Count > 0 && dsSubitemsSI.Tables[0].Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in dsSubitemsSI.Tables[0].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubitemID + "'"))
    //        {
    //            dsSubitemsSI.Tables[0].Rows.Remove(dr);
    //        }
    //    }

    //    if (dsSubitemsSI.Tables[0].Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dsSubitemsSI.Tables[0].Rows.Count; i++)
    //        {
    //            dsSubitemsSI.Tables[0].Rows[i]["SR_NO"] = i + 1;
    //        }
    //    }

    //    gvSubitemsSI.DataSource = dsSubitemsSI.Tables[0];
    //    gvSubitemsSI.DataBind();

    //    lblSubitemsSIRerords.Text = "Subitems Records[" + gvSubitemsSI.Rows.Count + "]";

    //    mpeSubitemDetail.Show();
    //}


    private void RemoveUpdatedSubitems(int srNo)
    {
        if (Session["dtSubitem"] != null)
            dtTemp = (DataTable)Session["dtSubitem"];
        else
            AddTempSubitemTable();

        dtTemp = (DataTable)Session["dtSubitem"];

        if (gvSubItemToU.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvSubItemToU.Rows)
            {
                Label lblSrNo = gr.FindControl("lblSrNo") as Label;
                TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;

                if (dtTemp.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTemp.Select("SR_NO='" + Convert.ToInt32(lblSrNo.Text) + "'"))
                    {
                        dr["TAG_NO"] = txtTagNoInList.Text;
                    }
                }
            }
        }


        if (dtTemp.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTemp.Select("SR_NO='" + srNo + "'"))
            {
                if (Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]) > 0)
                    hdRemovedSubitemIDs.Value += Convert.ToString(dr["LOT_TF_SUBITEM_ID"]) + ",";

                dtTemp.Rows.Remove(dr);
            }
        }

        if (dtTemp.Rows.Count > 0)
        {
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                dtTemp.Rows[i]["SR_NO"] = i + 1;
            }
        }

        gvSubItemToU.DataSource = dtTemp;
        gvSubItemToU.DataBind();

        lblSubitemsRecords.Text = "Subitems Records[" + gvSubItemToU.Rows.Count + "]";
    }


    private void AddUpdateAttachments()
    {
        try
        {
            attachment1FileBytes = null;
            attachment2FileBytes = null;
            attachment3FileBytes = null;
            attachment4FileBytes = null;

            attachment1File = string.Empty;
            attachment2File = string.Empty;
            attachment3File = string.Empty;
            attachment4File = string.Empty;


            if (dtTempAttachments != null && Session["dtAttachments"] != null)
                dtTempAttachments = (DataTable)Session["dtAttachments"];
            else
                AddTempAttachmentTable();

            dtTempAttachments = (DataTable)Session["dtAttachments"];

            dtTempAttachments.Clear();

            if (uploadFileAttachment1ToEdit.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment1ToEdit.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment1ToEdit.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment1File = str[str.Length - 1];
                    attachment1FileBytes = GetFileBytes(uploadFileAttachment1ToEdit.PostedFile.FileName, uploadFileAttachment1ToEdit.PostedFile.InputStream);
                }
            }

            if (uploadFileAttachment2ToEdit.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment2ToEdit.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment2ToEdit.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment2File = str[str.Length - 1];
                    attachment2FileBytes = GetFileBytes(uploadFileAttachment2ToEdit.PostedFile.FileName, uploadFileAttachment2ToEdit.PostedFile.InputStream);
                }
            }

            if (uploadFileAttachment3ToEdit.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment3ToEdit.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment3ToEdit.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment3File = str[str.Length - 1];
                    attachment3FileBytes = GetFileBytes(uploadFileAttachment3ToEdit.PostedFile.FileName, uploadFileAttachment3ToEdit.PostedFile.InputStream);
                }
            }

            if (uploadFileAttachment4ToEdit.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment4ToEdit.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment4ToEdit.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment4File = str[str.Length - 1];
                    attachment4FileBytes = GetFileBytes(uploadFileAttachment4ToEdit.PostedFile.FileName, uploadFileAttachment4ToEdit.PostedFile.InputStream);
                }
            }


            DataRow dr = dtTempAttachments.NewRow();

            if (!string.IsNullOrEmpty(Convert.ToString(attachment1File)))
                dr["ATTACHMENT1_NAME"] = attachment1File;
            else dr["ATTACHMENT1_NAME"] = string.Empty;

            if (attachment1FileBytes != null)
                dr["ATTACHMENT1_BTYTES"] = attachment1FileBytes;
            else dr["ATTACHMENT1_BTYTES"] = null;


            if (!string.IsNullOrEmpty(Convert.ToString(attachment2File)))
                dr["ATTACHMENT2_NAME"] = attachment2File;
            else dr["ATTACHMENT2_NAME"] = string.Empty;

            if (attachment2FileBytes != null)
                dr["ATTACHMENT2_BTYTES"] = attachment2FileBytes;
            else dr["ATTACHMENT2_BTYTES"] = null;


            if (!string.IsNullOrEmpty(Convert.ToString(attachment3File)))
                dr["ATTACHMENT3_NAME"] = attachment3File;
            else dr["ATTACHMENT3_NAME"] = string.Empty;

            if (attachment3FileBytes != null)
                dr["ATTACHMENT3_BTYTES"] = attachment3FileBytes;
            else dr["ATTACHMENT3_BTYTES"] = null;


            if (!string.IsNullOrEmpty(Convert.ToString(attachment4File)))
                dr["ATTACHMENT4_NAME"] = attachment4File;
            else dr["ATTACHMENT4_NAME"] = string.Empty;

            if (attachment4FileBytes != null)
                dr["ATTACHMENT4_BTYTES"] = attachment4FileBytes;
            else dr["ATTACHMENT4_BTYTES"] = null;


            dtTempAttachments.Rows.Add(dr);

            gvAttachments.DataSource = dtTempAttachments;
            gvAttachments.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }



    private void PreviewLOTTransmittalToFactory()
    {
        try
        {
            int LOTTFID = 0;
            string LOTTFSubitemIDs = string.Empty;

            #region TEMP DATATABLES

            #region dtLOT

            DataTable dtLOT = new DataTable();
            dtLOT.Columns.Add("TF_NO", typeof(string));
            dtLOT.Columns.Add("UNIT_NAME", typeof(string));
            dtLOT.Columns.Add("DATE", typeof(string));
            dtLOT.Columns.Add("CUSTOMER_CODE", typeof(string));
            dtLOT.Columns.Add("CUSTOMER_NAME", typeof(string));
            dtLOT.Columns.Add("JOB_NO", typeof(string));
            dtLOT.Columns.Add("PO_NO", typeof(string));
            dtLOT.Columns.Add("ITEM_NAME", typeof(string));

            dtLOT.Columns.Add("OLD_LOT_TF_ID", typeof(int));
            dtLOT.Columns.Add("OLD_TF_NO", typeof(string));
            dtLOT.Columns.Add("OLD_CREATED_ON", typeof(string));

            dtLOT.Columns.Add("IS_TRANSFERRED", typeof(int));
            dtLOT.Columns.Add("TRANSFERRED_LOT_TF_ID", typeof(int));
            dtLOT.Columns.Add("OLD_TRANSFERRED_TF_NO", typeof(string));
            dtLOT.Columns.Add("OLD_TRANSFERRED_CREATED_ON", typeof(string));
            dtLOT.Columns.Add("TRANSFERRED_REMARKS", typeof(string));


            dtLOT.Columns.Add("LOT_CREATED_ON", typeof(string));


            dtLOT.Columns.Add("IMP_NOTES", typeof(string));
            dtLOT.Columns.Add("PE_ID", typeof(int));
            dtLOT.Columns.Add("PE_NAME", typeof(string));
            dtLOT.Columns.Add("PE_UD", typeof(string));
            dtLOT.Columns.Add("PE_PD", typeof(string));
            dtLOT.Columns.Add("PM_ID", typeof(int));
            dtLOT.Columns.Add("PM_NAME", typeof(string));
            dtLOT.Columns.Add("PM_UD", typeof(string));
            dtLOT.Columns.Add("PM_PD", typeof(string));
            dtLOT.Columns.Add("CREATED_BY_ID", typeof(int));
            dtLOT.Columns.Add("CREATED_BY", typeof(string));
            dtLOT.Columns.Add("STANDARD_DRAWING_NAME", typeof(string));
            dtLOT.Columns.Add("STANDARD_DRAWING_SAVED_BY_ID", typeof(int));
            dtLOT.Columns.Add("STANDARD_DRAWING_SAVED_BY", typeof(string));
            dtLOT.Columns.Add("STANDARD_DRAWING_SAVED_ON", typeof(string));
            dtLOT.Columns.Add("STANDARD_DRAWING_SAVED_REMARKS", typeof(string));
            dtLOT.Columns.Add("FIRST_QUALITY_PERSON_ID", typeof(int));
            dtLOT.Columns.Add("SECOND_QUALITY_PERSON_ID", typeof(int));
            dtLOT.Columns.Add("FIRST_QUALITY_PERSON", typeof(string));
            dtLOT.Columns.Add("SECOND_QUALITY_PERSON", typeof(string));


            DataRow drLOT = dtLOT.NewRow();
            drLOT["TF_NO"] = Convert.ToString(txtTFNoToEdit.Text);
            drLOT["UNIT_NAME"] = Convert.ToString(ddlCompany.SelectedItem.Text);
            drLOT["DATE"] = Convert.ToString(txtDateToEdit.Text);
            drLOT["CUSTOMER_CODE"] = Convert.ToString(txtCustomerCodeToEdit.Text);
            drLOT["CUSTOMER_NAME"] = Convert.ToString(txtCustomerNameToEdit.Text);
            drLOT["JOB_NO"] = Convert.ToString(txtJOBNoToEdit.Text);
            drLOT["PO_NO"] = Convert.ToString(txtPONoToEdit.Text);
            drLOT["ITEM_NAME"] = Convert.ToString(txtItemNameToEdit.Text);

            drLOT["OLD_LOT_TF_ID"] = 0;
            drLOT["OLD_TF_NO"] = "";
            drLOT["OLD_CREATED_ON"] = "";

            drLOT["IS_TRANSFERRED"] = 0;
            drLOT["TRANSFERRED_LOT_TF_ID"] = 0;
            drLOT["OLD_TRANSFERRED_TF_NO"] = "";
            drLOT["OLD_TRANSFERRED_CREATED_ON"] = "";
            drLOT["TRANSFERRED_REMARKS"] = "";


            drLOT["LOT_CREATED_ON"] = "";
            drLOT["IMP_NOTES"] = Convert.ToString(txtNotesToEdit.Text);

            //if (!string.IsNullOrEmpty(txtEditOrAmendNotesEdit.Text))
            //    drLOT["IMP_NOTES"] = Convert.ToString(txtEditOrAmendNotesEdit.Text).Replace(Environment.NewLine, " ");
            //else
            //    drLOT["IMP_NOTES"] = Convert.ToString(txtNotesToEdit.Text).Replace(Environment.NewLine, " ");

            drLOT["PE_ID"] = 0;
            drLOT["PE_NAME"] = "";
            drLOT["PE_UD"] = "";
            drLOT["PE_PD"] = "";
            drLOT["PM_ID"] = 0;
            drLOT["PM_NAME"] = "";
            drLOT["PM_UD"] = "";
            drLOT["PM_PD"] = "";
            drLOT["CREATED_BY_ID"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            drLOT["CREATED_BY"] = Convert.ToString(Session["EMPLOYEE_NAME"]);
            drLOT["STANDARD_DRAWING_NAME"] = "";
            drLOT["STANDARD_DRAWING_SAVED_BY_ID"] = 0;
            drLOT["STANDARD_DRAWING_SAVED_BY"] = "";
            drLOT["STANDARD_DRAWING_SAVED_ON"] = "";
            drLOT["STANDARD_DRAWING_SAVED_REMARKS"] = "";
            drLOT["FIRST_QUALITY_PERSON_ID"] = 0;
            drLOT["SECOND_QUALITY_PERSON_ID"] = 0;
            drLOT["FIRST_QUALITY_PERSON"] = "";
            drLOT["SECOND_QUALITY_PERSON"] = "";

            dtLOT.Rows.Add(drLOT);

            #endregion


            #region dtSubitems

            DataTable dtSubitems = new DataTable();
            dtSubitems.Columns.Add("RECORD_NO", typeof(string));
            dtSubitems.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
            dtSubitems.Columns.Add("STATUS_ID", typeof(int));
            dtSubitems.Columns.Add("LOT_TF_ID", typeof(int));
            dtSubitems.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtSubitems.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            dtSubitems.Columns.Add("SUBITEM_DESC", typeof(string));
            dtSubitems.Columns.Add("TAG_NO", typeof(string));
            dtSubitems.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
            dtSubitems.Columns.Add("LOT_MAIN_SUBITEM", typeof(string));
            dtSubitems.Columns.Add("LOT_MAIN_ITEM", typeof(string));
            dtSubitems.Columns.Add("DRAWING_NO", typeof(string));

            dtSubitems.Columns.Add("REVISION_NO", typeof(string));
            dtSubitems.Columns.Add("REVISION_NO_TEXT", typeof(string));

            dtSubitems.Columns.Add("CATEGORY", typeof(string));
            dtSubitems.Columns.Add("QUANTITY", typeof(string));
            dtSubitems.Columns.Add("PRODUCTION_MNGR_ID", typeof(int));
            dtSubitems.Columns.Add("PRODUCTION_MNGR_NAME", typeof(string));
            dtSubitems.Columns.Add("CREATED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("CREATED_BY", typeof(string));
            dtSubitems.Columns.Add("CREATED_ON", typeof(string));
            dtSubitems.Columns.Add("APPROVED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("APPROVED_BY", typeof(string));
            dtSubitems.Columns.Add("APPROVED_ON", typeof(string));
            dtSubitems.Columns.Add("APPROVED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("PLANNING_ACCEPTED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("PLANNING_ACCEPTED_BY", typeof(string));
            dtSubitems.Columns.Add("PLANNING_ACCEPTED_ON", typeof(string));
            dtSubitems.Columns.Add("PLANNING_ACCEPTED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("FORWARDED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("FORWARDED_BY", typeof(string));
            dtSubitems.Columns.Add("FORWARDED_ON", typeof(string));
            dtSubitems.Columns.Add("FORWARDED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("ACCEPTED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("ACCEPTED_BY", typeof(string));
            dtSubitems.Columns.Add("ACCEPTED_ON", typeof(string));
            dtSubitems.Columns.Add("ACCEPTED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("QUALITY_ACCEPTED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("QUALITY_ACCEPTED_BY", typeof(string));
            dtSubitems.Columns.Add("QUALITY_ACCEPTED_ON", typeof(string));
            dtSubitems.Columns.Add("QUALITY_ACCEPTED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("SENT_TO_INTL_INSP_BY_ID", typeof(int));
            dtSubitems.Columns.Add("SENT_TO_INTL_INSP_BY", typeof(string));
            dtSubitems.Columns.Add("SENT_TO_INTL_INSP_ON", typeof(string));
            dtSubitems.Columns.Add("SENT_TO_INTL_INSP_REMARKS", typeof(string));
            dtSubitems.Columns.Add("QA_INTL_INSP_ACCEPTED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("QA_INTL_INSP_ACCEPTED_BY", typeof(string));
            dtSubitems.Columns.Add("QA_INTL_INSP_ACCEPTED_ON", typeof(string));
            dtSubitems.Columns.Add("QA_INTL_INSP_ACCEPTED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_BY", typeof(string));
            dtSubitems.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_ON", typeof(string));
            dtSubitems.Columns.Add("QA_INTL_INSP_NOT_ACCEPTED_REMARKS", typeof(string));


            dtSubitems.Columns.Add("SENT_TO_FINAL_INSP_BY_ID", typeof(int));
            dtSubitems.Columns.Add("SENT_TO_FINAL_INSP_BY", typeof(string));
            dtSubitems.Columns.Add("SENT_TO_FINAL_INSP_ON", typeof(string));
            dtSubitems.Columns.Add("SENT_TO_FINAL_INSP_REMARKS", typeof(string));

            dtSubitems.Columns.Add("SENT_TO_REWORK_BY_ID", typeof(int));
            dtSubitems.Columns.Add("SENT_TO_REWORK_BY", typeof(string));
            dtSubitems.Columns.Add("SENT_TO_REWORK_ON", typeof(string));
            dtSubitems.Columns.Add("SENT_TO_REWORK_REMARKS", typeof(string));


            dtSubitems.Columns.Add("AMENDMENT_COUNT", typeof(string));
            dtSubitems.Columns.Add("AMENDMENT_BY_ID", typeof(int));
            dtSubitems.Columns.Add("AMENDMENT_BY", typeof(string));
            dtSubitems.Columns.Add("AMENDMENT_ON", typeof(string));
            dtSubitems.Columns.Add("AMENDMENT_REMARKS", typeof(string));
            dtSubitems.Columns.Add("PROD_AMENDMENT_BY_ID", typeof(int));
            dtSubitems.Columns.Add("PROD_AMENDMENT_BY", typeof(string));
            dtSubitems.Columns.Add("PROD_AMENDMENT_ON", typeof(string));
            dtSubitems.Columns.Add("PROD_AMENDMENT_REMARKS", typeof(string));
            dtSubitems.Columns.Add("AMENDED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("AMENDED_BY", typeof(string));
            dtSubitems.Columns.Add("AMENDED_ON", typeof(string));
            dtSubitems.Columns.Add("AMENDED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("AMENDED_APPROVED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("AMENDED_APPROVED_BY", typeof(string));
            dtSubitems.Columns.Add("AMENDED_APPROVED_ON", typeof(string));
            dtSubitems.Columns.Add("AMENDED_APPROVED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("AMENDED_PLANNING_ACCEPTED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("AMENDED_PLANNING_ACCEPTED_BY", typeof(string));
            dtSubitems.Columns.Add("AMENDED_PLANNING_ACCEPTED_ON", typeof(string));
            dtSubitems.Columns.Add("AMENDED_PLANNING_ACCEPTED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("AMENDED_FORWARDED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("AMENDED_FORWARDED_BY", typeof(string));
            dtSubitems.Columns.Add("AMENDED_FORWARDED_ON", typeof(string));
            dtSubitems.Columns.Add("AMENDED_FORWARDED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("AMENDED_ACCEPTED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("AMENDED_ACCEPTED_BY", typeof(string));
            dtSubitems.Columns.Add("AMENDED_ACCEPTED_ON", typeof(string));
            dtSubitems.Columns.Add("AMENDED_ACCEPTED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("AMENDED_QUALITY_ACCEPTED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("AMENDED_QUALITY_ACCEPTED_BY", typeof(string));
            dtSubitems.Columns.Add("AMENDED_QUALITY_ACCEPTED_ON", typeof(string));
            dtSubitems.Columns.Add("AMENDED_QUALITY_ACCEPTED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("COMPLETED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("COMPLETED_BY", typeof(string));
            dtSubitems.Columns.Add("COMPLETED_ON", typeof(string));
            dtSubitems.Columns.Add("COMPLETED_REMARKS", typeof(string));
            dtSubitems.Columns.Add("REVISED_BY_ID", typeof(int));
            dtSubitems.Columns.Add("REVISED_BY", typeof(string));
            dtSubitems.Columns.Add("REVISED_ON", typeof(string));
            dtSubitems.Columns.Add("REVISED_REMARKS", typeof(string));


            if (gvSubItemToU.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItemToU.Rows)
                {
                    Label lblProductionNumber = gr.FindControl("lblProductionNumber") as Label;
                    Label lblProductionOrderDate = gr.FindControl("lblProductionOrderDate") as Label;
                    Label lblExpectedCompletionDate = gr.FindControl("lblExpectedCompletionDate") as Label;
                    Label lblProductCode = gr.FindControl("lblProductCode") as Label;
                    Label lblProductDesc = gr.FindControl("lblProductDesc") as Label;
                    Label lblUOM = gr.FindControl("lblUOM") as Label;
                    Label lblLOTMainItemID = gr.FindControl("lblLOTMainItemID") as Label;
                    Label lblLOTMainSubitemID = gr.FindControl("lblLOTMainSubitemID") as Label;
                    Label lblLOTFor = gr.FindControl("lblLOTFor") as Label;
                    TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;
                    Label lblDescription = gr.FindControl("lblDescription") as Label;
                    Label lblDrgOrDOCNo = gr.FindControl("lblDrgOrDOCNo") as Label;
                    Label lblRevNo = gr.FindControl("lblRevNo") as Label;
                    Label lblCategoryID = gr.FindControl("lblCategoryID") as Label;
                    Label lblCategory = gr.FindControl("lblCategory") as Label;
                    Label lblQuantity = gr.FindControl("lblQuantity") as Label;
                    TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;
                    TextBox txtRevNo = gr.FindControl("txtRevNo") as TextBox;
                    TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                    CheckBox chkIsPartOfProductionStatusReport = gr.FindControl("chkIsPartOfProductionStatusReport") as CheckBox;



                    Label lblProductionMngrId = gr.FindControl("lblProductionMngrId") as Label;
                    Label lblProductionMngrName = gr.FindControl("lblProductionMngrName") as Label;
                    Label lblCreatedById = gr.FindControl("lblCreatedById") as Label;
                    Label lblCreatedBy = gr.FindControl("lblCreatedBy") as Label;
                    Label lblCreatedOn = gr.FindControl("lblCreatedOn") as Label;
                    Label lblApprovedById = gr.FindControl("lblApprovedById") as Label;
                    Label lblApprovedBy = gr.FindControl("lblApprovedBy") as Label;
                    Label lblApprovedOn = gr.FindControl("lblApprovedOn") as Label;
                    Label lblApprovedRemarks = gr.FindControl("lblApprovedRemarks") as Label;
                    Label lblPlanningAcceptedById = gr.FindControl("lblPlanningAcceptedById") as Label;
                    Label lblPlanningAcceptedBy = gr.FindControl("lblPlanningAcceptedBy") as Label;
                    Label lblPlanningAcceptedOn = gr.FindControl("lblPlanningAcceptedOn") as Label;
                    Label lblPlanningAcceptedRemarks = gr.FindControl("lblPlanningAcceptedRemarks") as Label;
                    Label lblForwardedById = gr.FindControl("lblForwardedById") as Label;
                    Label lblForwardedBy = gr.FindControl("lblForwardedBy") as Label;
                    Label lblForwardedOn = gr.FindControl("lblForwardedOn") as Label;
                    Label lblForwardedRemarks = gr.FindControl("lblForwardedRemarks") as Label;
                    Label lblAcceptedById = gr.FindControl("lblAcceptedById") as Label;
                    Label lblAcceptedBy = gr.FindControl("lblAcceptedBy") as Label;
                    Label lblAcceptedOn = gr.FindControl("lblAcceptedOn") as Label;
                    Label lblAcceptedRemarks = gr.FindControl("lblAcceptedRemarks") as Label;
                    Label lblQualityAcceptedById = gr.FindControl("lblQualityAcceptedById") as Label;
                    Label lblQualityAcceptedBy = gr.FindControl("lblQualityAcceptedBy") as Label;
                    Label lblQualityAcceptedOn = gr.FindControl("lblQualityAcceptedOn") as Label;
                    Label lblQualityAcceptedRemarks = gr.FindControl("lblQualityAcceptedRemarks") as Label;
                    Label lblSentToIntlInspById = gr.FindControl("lblSentToIntlInspById") as Label;
                    Label lblSentToIntlInspBy = gr.FindControl("lblSentToIntlInspBy") as Label;
                    Label lblSentToIntlInspOn = gr.FindControl("lblSentToIntlInspOn") as Label;
                    Label lblSentToIntlInspRemarks = gr.FindControl("lblSentToIntlInspRemarks") as Label;
                    Label lblQaIntlInspAcceptedById = gr.FindControl("lblQaIntlInspAcceptedById") as Label;
                    Label lblQaIntlInspAcceptedBy = gr.FindControl("lblQaIntlInspAcceptedBy") as Label;
                    Label lblQaIntlInspAcceptedOn = gr.FindControl("lblQaIntlInspAcceptedOn") as Label;
                    Label lblQaIntlInspAcceptedRemarks = gr.FindControl("lblQaIntlInspAcceptedRemarks") as Label;
                    Label lblQaIntlInspNotAcceptedById = gr.FindControl("lblQaIntlInspNotAcceptedById") as Label;
                    Label lblQaIntlInspNotAcceptedBy = gr.FindControl("lblQaIntlInspNotAcceptedBy") as Label;
                    Label lblQaIntlInspNotAcceptedOn = gr.FindControl("lblQaIntlInspNotAcceptedOn") as Label;
                    Label lblQaIntlInspNotAcceptedRemarks = gr.FindControl("lblQaIntlInspNotAcceptedRemarks") as Label;
                    Label lblSentToFinalInspById = gr.FindControl("lblSentToFinalInspById") as Label;
                    Label lblSentToFinalInspBy = gr.FindControl("lblSentToFinalInspBy") as Label;
                    Label lblSentToFinalInspByEmail = gr.FindControl("lblSentToFinalInspByEmail") as Label;
                    Label lblSentToFinalInspOn = gr.FindControl("lblSentToFinalInspOn") as Label;
                    Label lblSentToFinalInspRemarks = gr.FindControl("lblSentToFinalInspRemarks") as Label;
                    Label lblSentToReworkById = gr.FindControl("lblSentToReworkById") as Label;
                    Label lblSentToReworkBy = gr.FindControl("lblSentToReworkBy") as Label;
                    Label lblSentToReworkByEmail = gr.FindControl("lblSentToReworkByEmail") as Label;
                    Label lblSentToReworkOn = gr.FindControl("lblSentToReworkOn") as Label;
                    Label lblSentToReworkRemarks = gr.FindControl("lblSentToReworkRemarks") as Label;
                    Label lblAmendmentCount = gr.FindControl("lblAmendmentCount") as Label;
                    Label lblAmendmentById = gr.FindControl("lblAmendmentById") as Label;
                    Label lblAmendmentBy = gr.FindControl("lblAmendmentBy") as Label;
                    Label lblAmendmentOn = gr.FindControl("lblAmendmentOn") as Label;
                    Label lblAmendmentRemarks = gr.FindControl("lblAmendmentRemarks") as Label;
                    Label lblProdAmendmentById = gr.FindControl("lblProdAmendmentById") as Label;
                    Label lblProdAmendmentBy = gr.FindControl("lblProdAmendmentBy") as Label;
                    Label lblProdAmendmentOn = gr.FindControl("lblProdAmendmentOn") as Label;
                    Label lblProdAmendmentRemarks = gr.FindControl("lblProdAmendmentRemarks") as Label;
                    Label lblAmendedById = gr.FindControl("lblAmendedById") as Label;
                    Label lblAmendedBy = gr.FindControl("lblAmendedBy") as Label;
                    Label lblAmendedOn = gr.FindControl("lblAmendedOn") as Label;
                    Label lblAmendedRemarks = gr.FindControl("lblAmendedRemarks") as Label;
                    Label lblAmendedApprovedById = gr.FindControl("lblAmendedApprovedById") as Label;
                    Label lblAmendedApprovedBy = gr.FindControl("lblAmendedApprovedBy") as Label;
                    Label lblAmendedApprovedOn = gr.FindControl("lblAmendedApprovedOn") as Label;
                    Label lblAmendedApprovedRemarks = gr.FindControl("lblAmendedApprovedRemarks") as Label;
                    Label lblAmendedPlanningAcceptedById = gr.FindControl("lblAmendedPlanningAcceptedById") as Label;
                    Label lblAmendedPlanningAcceptedBy = gr.FindControl("lblAmendedPlanningAcceptedBy") as Label;
                    Label lblAmendedPlanningAcceptedOn = gr.FindControl("lblAmendedPlanningAcceptedOn") as Label;
                    Label lblAmendedPlanningAcceptedRemarks = gr.FindControl("lblAmendedPlanningAcceptedRemarks") as Label;
                    Label lblAmendedForwardedById = gr.FindControl("lblAmendedForwardedById") as Label;
                    Label lblAmendedForwardedBy = gr.FindControl("lblAmendedForwardedBy") as Label;
                    Label lblAmendedForwardedOn = gr.FindControl("lblAmendedForwardedOn") as Label;
                    Label lblAmendedForwardedRemarks = gr.FindControl("lblAmendedForwardedRemarks") as Label;
                    Label lblAmendedAcceptedById = gr.FindControl("lblAmendedAcceptedById") as Label;
                    Label lblAmendedAcceptedBy = gr.FindControl("lblAmendedAcceptedBy") as Label;
                    Label lblAmendedAcceptedOn = gr.FindControl("lblAmendedAcceptedOn") as Label;
                    Label lblAmendedAcceptedRemarks = gr.FindControl("lblAmendedAcceptedRemarks") as Label;
                    Label lblAmendedQualityAcceptedById = gr.FindControl("lblAmendedQualityAcceptedById") as Label;
                    Label lblAmendedQualityAcceptedBy = gr.FindControl("lblAmendedQualityAcceptedBy") as Label;
                    Label lblAmendedQualityAcceptedOn = gr.FindControl("lblAmendedQualityAcceptedOn") as Label;
                    Label lblAmendedQualityAcceptedRemarks = gr.FindControl("lblAmendedQualityAcceptedRemarks") as Label;
                    Label lblCompletedById = gr.FindControl("lblCompletedById") as Label;
                    Label lblCompletedBy = gr.FindControl("lblCompletedBy") as Label;
                    Label lblCompletedOn = gr.FindControl("lblCompletedOn") as Label;
                    Label lblCompletedRemarks = gr.FindControl("lblCompletedRemarks") as Label;
                    Label lblRevisedById = gr.FindControl("lblRevisedById") as Label;
                    Label lblRevisedBy = gr.FindControl("lblRevisedBy") as Label;
                    Label lblRevisedOn = gr.FindControl("lblRevisedOn") as Label;
                    Label lblRevisedRemarks = gr.FindControl("lblRevisedRemarks") as Label;



                    DataRow drSI = dtSubitems.NewRow();
                    drSI["RECORD_NO"] = 0;
                    drSI["LOT_TF_SUBITEM_ID"] = 0;
                    drSI["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                    drSI["LOT_TF_ID"] = 0;
                    drSI["PRODUCTION_ORDER_NO"] = Convert.ToString(lblProductionNumber.Text);
                    drSI["EXPECTED_COMPLETION_DATE"] = Convert.ToString(lblExpectedCompletionDate.Text);
                    drSI["SUBITEM_DESC"] = Convert.ToString(lblDescription.Text).Replace(Environment.NewLine, " ");
                    drSI["TAG_NO"] = Convert.ToString(txtTagNoInList.Text);
                    drSI["LOT_MAIN_SUBITEM_ID"] = 0;
                    drSI["LOT_MAIN_SUBITEM"] = "";
                    drSI["LOT_MAIN_ITEM"] = Convert.ToString(lblLOTFor.Text);
                    drSI["DRAWING_NO"] = Convert.ToString(lblDrgOrDOCNo.Text);

                    drSI["REVISION_NO"] = Convert.ToString(lblRevNo.Text);
                    drSI["REVISION_NO_TEXT"] = Convert.ToString(txtRevNo.Text);


                    drSI["CATEGORY"] = Convert.ToString(lblCategory.Text);
                    drSI["QUANTITY"] = Convert.ToString(txtQuantity.Text);

                    if (!string.IsNullOrEmpty(lblProductionMngrId.Text))
                    {
                        drSI["PRODUCTION_MNGR_ID"] = Convert.ToInt32(lblProductionMngrId.Text);
                        drSI["PRODUCTION_MNGR_NAME"] = Convert.ToString(lblProductionMngrName.Text);
                    }

                    if (!string.IsNullOrEmpty(lblCreatedById.Text))
                    {
                        drSI["CREATED_BY_ID"] = Convert.ToInt32(lblCreatedById.Text);
                        drSI["CREATED_BY"] = Convert.ToString(lblCreatedBy.Text);
                        drSI["CREATED_ON"] = Convert.ToString(lblCreatedOn.Text);
                    }

                    if (!string.IsNullOrEmpty(lblApprovedById.Text))
                    {
                        drSI["APPROVED_BY_ID"] = Convert.ToInt32(lblApprovedById.Text);
                        drSI["APPROVED_BY"] = Convert.ToString(lblApprovedBy.Text);
                        drSI["APPROVED_ON"] = Convert.ToString(lblApprovedOn.Text);
                        drSI["APPROVED_REMARKS"] = Convert.ToString(lblApprovedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblPlanningAcceptedById.Text))
                    {
                        drSI["PLANNING_ACCEPTED_BY_ID"] = Convert.ToInt32(lblPlanningAcceptedById.Text);
                        drSI["PLANNING_ACCEPTED_BY"] = Convert.ToString(lblPlanningAcceptedBy.Text);
                        drSI["PLANNING_ACCEPTED_ON"] = Convert.ToString(lblPlanningAcceptedOn.Text);
                        drSI["PLANNING_ACCEPTED_REMARKS"] = Convert.ToString(lblPlanningAcceptedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblForwardedById.Text))
                    {
                        drSI["FORWARDED_BY_ID"] = Convert.ToInt32(lblForwardedById.Text);
                        drSI["FORWARDED_BY"] = Convert.ToString(lblForwardedBy.Text);
                        drSI["FORWARDED_ON"] = Convert.ToString(lblForwardedOn.Text);
                        drSI["FORWARDED_REMARKS"] = Convert.ToString(lblForwardedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblAcceptedById.Text))
                    {
                        drSI["ACCEPTED_BY_ID"] = Convert.ToInt32(lblAcceptedById.Text);
                        drSI["ACCEPTED_BY"] = Convert.ToString(lblAcceptedBy.Text);
                        drSI["ACCEPTED_ON"] = Convert.ToString(lblAcceptedOn.Text);
                        drSI["ACCEPTED_REMARKS"] = Convert.ToString(lblAcceptedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblQualityAcceptedById.Text))
                    {
                        drSI["QUALITY_ACCEPTED_BY_ID"] = Convert.ToInt32(lblQualityAcceptedById.Text);
                        drSI["QUALITY_ACCEPTED_BY"] = Convert.ToString(lblQualityAcceptedBy.Text);
                        drSI["QUALITY_ACCEPTED_ON"] = Convert.ToString(lblQualityAcceptedOn.Text);
                        drSI["QUALITY_ACCEPTED_REMARKS"] = Convert.ToString(lblQualityAcceptedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblSentToIntlInspById.Text))
                    {
                        drSI["SENT_TO_INTL_INSP_BY_ID"] = Convert.ToInt32(lblSentToIntlInspById.Text);
                        drSI["SENT_TO_INTL_INSP_BY"] = Convert.ToString(lblSentToIntlInspBy.Text);
                        drSI["SENT_TO_INTL_INSP_ON"] = Convert.ToString(lblSentToIntlInspOn.Text);
                        drSI["SENT_TO_INTL_INSP_REMARKS"] = Convert.ToString(lblSentToIntlInspRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblQaIntlInspAcceptedById.Text))
                    {
                        drSI["QA_INTL_INSP_ACCEPTED_BY_ID"] = Convert.ToInt32(lblQaIntlInspAcceptedById.Text);
                        drSI["QA_INTL_INSP_ACCEPTED_BY"] = Convert.ToString(lblQaIntlInspAcceptedBy.Text);
                        drSI["QA_INTL_INSP_ACCEPTED_ON"] = Convert.ToString(lblQaIntlInspAcceptedOn.Text);
                        drSI["QA_INTL_INSP_ACCEPTED_REMARKS"] = Convert.ToString(lblQaIntlInspAcceptedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblQaIntlInspNotAcceptedById.Text))
                    {
                        drSI["QA_INTL_INSP_NOT_ACCEPTED_BY_ID"] = Convert.ToInt32(lblQaIntlInspNotAcceptedById.Text);
                        drSI["QA_INTL_INSP_NOT_ACCEPTED_BY"] = Convert.ToString(lblQaIntlInspNotAcceptedBy.Text);
                        drSI["QA_INTL_INSP_NOT_ACCEPTED_ON"] = Convert.ToString(lblQaIntlInspNotAcceptedOn.Text);
                        drSI["QA_INTL_INSP_NOT_ACCEPTED_REMARKS"] = Convert.ToString(lblQaIntlInspNotAcceptedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblSentToFinalInspById.Text))
                    {
                        drSI["SENT_TO_FINAL_INSP_BY_ID"] = Convert.ToInt32(lblSentToFinalInspById.Text);
                        drSI["SENT_TO_FINAL_INSP_BY"] = Convert.ToString(lblSentToFinalInspBy.Text);
                        drSI["SENT_TO_FINAL_INSP_ON"] = Convert.ToString(lblSentToFinalInspOn.Text);
                        drSI["SENT_TO_FINAL_INSP_REMARKS"] = Convert.ToString(lblSentToFinalInspRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblSentToReworkById.Text))
                    {
                        drSI["SENT_TO_REWORK_BY_ID"] = Convert.ToInt32(lblSentToReworkById.Text);
                        drSI["SENT_TO_REWORK_BY"] = Convert.ToString(lblSentToReworkBy.Text);
                        drSI["SENT_TO_REWORK_ON"] = Convert.ToString(lblSentToReworkOn.Text);
                        drSI["SENT_TO_REWORK_REMARKS"] = Convert.ToString(lblSentToReworkRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblAmendmentCount.Text))
                        drSI["AMENDMENT_COUNT"] = Convert.ToInt32(lblAmendmentCount.Text);

                    if (!string.IsNullOrEmpty(lblAmendmentById.Text))
                    {
                        drSI["AMENDMENT_BY_ID"] = Convert.ToString(lblAmendmentById.Text);
                        drSI["AMENDMENT_BY"] = Convert.ToString(lblAmendmentBy.Text);
                        drSI["AMENDMENT_ON"] = Convert.ToString(lblAmendmentOn.Text);
                        drSI["AMENDMENT_REMARKS"] = Convert.ToString(lblAmendmentRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblProdAmendmentById.Text))
                    {
                        drSI["PROD_AMENDMENT_BY_ID"] = Convert.ToInt32(lblProdAmendmentById.Text);
                        drSI["PROD_AMENDMENT_BY"] = Convert.ToString(lblProdAmendmentBy.Text);
                        drSI["PROD_AMENDMENT_ON"] = Convert.ToString(lblProdAmendmentOn.Text);
                        drSI["PROD_AMENDMENT_REMARKS"] = Convert.ToString(lblProdAmendmentRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblAmendedById.Text))
                    {
                        drSI["AMENDED_BY_ID"] = Convert.ToInt32(lblAmendedById.Text);
                        drSI["AMENDED_BY"] = Convert.ToString(lblAmendedBy.Text);
                        drSI["AMENDED_ON"] = Convert.ToString(lblAmendedOn.Text);
                        drSI["AMENDED_REMARKS"] = Convert.ToString(txtEditOrAmendNotesEdit.Text);//Convert.ToString(lblAmendedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblAmendedApprovedById.Text))
                    {
                        drSI["AMENDED_APPROVED_BY_ID"] = Convert.ToInt32(lblAmendedApprovedById.Text);
                        drSI["AMENDED_APPROVED_BY"] = Convert.ToString(lblAmendedApprovedBy.Text);
                        drSI["AMENDED_APPROVED_ON"] = Convert.ToString(lblAmendedApprovedOn.Text);
                        drSI["AMENDED_APPROVED_REMARKS"] = Convert.ToString(lblAmendedApprovedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblAmendedPlanningAcceptedById.Text))
                    {
                        drSI["AMENDED_PLANNING_ACCEPTED_BY_ID"] = Convert.ToInt32(lblAmendedPlanningAcceptedById.Text);
                        drSI["AMENDED_PLANNING_ACCEPTED_BY"] = Convert.ToString(lblAmendedPlanningAcceptedBy.Text);
                        drSI["AMENDED_PLANNING_ACCEPTED_ON"] = Convert.ToString(lblAmendedPlanningAcceptedOn.Text);
                        drSI["AMENDED_PLANNING_ACCEPTED_REMARKS"] = Convert.ToString(lblAmendedPlanningAcceptedRemarks.Text);
                    }
                    if (!string.IsNullOrEmpty(lblAmendedForwardedById.Text))
                    {
                        drSI["AMENDED_FORWARDED_BY_ID"] = Convert.ToInt32(lblAmendedForwardedById.Text);
                        drSI["AMENDED_FORWARDED_BY"] = Convert.ToString(lblAmendedForwardedBy.Text);
                        drSI["AMENDED_FORWARDED_ON"] = Convert.ToString(lblAmendedForwardedOn.Text);
                        drSI["AMENDED_FORWARDED_REMARKS"] = Convert.ToString(lblAmendedForwardedRemarks.Text);
                    }
                    if (!string.IsNullOrEmpty(lblAmendedAcceptedById.Text))
                    {
                        drSI["AMENDED_ACCEPTED_BY_ID"] = Convert.ToInt32(lblAmendedAcceptedById.Text);
                        drSI["AMENDED_ACCEPTED_BY"] = Convert.ToString(lblAmendedAcceptedBy.Text);
                        drSI["AMENDED_ACCEPTED_ON"] = Convert.ToString(lblAmendedAcceptedOn.Text);
                        drSI["AMENDED_ACCEPTED_REMARKS"] = Convert.ToString(lblAmendedAcceptedRemarks.Text);
                    }
                    if (!string.IsNullOrEmpty(lblAmendedQualityAcceptedById.Text))
                    {
                        drSI["AMENDED_QUALITY_ACCEPTED_BY_ID"] = Convert.ToInt32(lblAmendedQualityAcceptedById.Text);
                        drSI["AMENDED_QUALITY_ACCEPTED_BY"] = Convert.ToString(lblAmendedQualityAcceptedBy.Text);
                        drSI["AMENDED_QUALITY_ACCEPTED_ON"] = Convert.ToString(lblAmendedQualityAcceptedOn.Text);
                        drSI["AMENDED_QUALITY_ACCEPTED_REMARKS"] = Convert.ToString(lblAmendedQualityAcceptedRemarks.Text);
                    }
                    if (!string.IsNullOrEmpty(lblCompletedById.Text))
                    {
                        drSI["COMPLETED_BY_ID"] = Convert.ToInt32(lblCompletedById.Text);
                        drSI["COMPLETED_BY"] = Convert.ToString(lblCompletedBy.Text);
                        drSI["COMPLETED_ON"] = Convert.ToString(lblCompletedOn.Text);
                        drSI["COMPLETED_REMARKS"] = Convert.ToString(lblCompletedRemarks.Text);
                    }

                    if (!string.IsNullOrEmpty(lblRevisedById.Text))
                    {
                        drSI["REVISED_BY_ID"] = Convert.ToInt32(lblRevisedById.Text);
                        drSI["REVISED_BY"] = Convert.ToString(lblRevisedBy.Text);
                        drSI["REVISED_ON"] = Convert.ToString(lblRevisedOn.Text);
                        drSI["REVISED_REMARKS"] = Convert.ToString(lblRevisedRemarks.Text);

                    }






                    dtSubitems.Rows.Add(drSI);
                }
            }


            #endregion


            #region dtEditedOrAmended

            DataTable dtEditedOrAmended = new DataTable();

            dtEditedOrAmended.Columns.Add("EDITED_OR_AMENDED_BY_ID", typeof(int));
            dtEditedOrAmended.Columns.Add("EDITED_OR_AMENDED_BY", typeof(string));
            dtEditedOrAmended.Columns.Add("EDITED_OR_AMENDED_ON", typeof(string));
            dtEditedOrAmended.Columns.Add("EDITED_OR_AMENDED_REMARKS", typeof(string));


            DataRow drEA = dtEditedOrAmended.NewRow();

            drEA["EDITED_OR_AMENDED_BY_ID"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            drEA["EDITED_OR_AMENDED_BY"] = Convert.ToString(Session["EMPLOYEE_NAME"]);
            drEA["EDITED_OR_AMENDED_ON"] = DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt");
            drEA["EDITED_OR_AMENDED_REMARKS"] = Convert.ToString(txtEditOrAmendNotesEdit.Text);

            dtEditedOrAmended.Rows.Add(drEA);


            #endregion

            #region dtQuantityDetails

            DataTable dtQuantityDetails = new DataTable();
            dtQuantityDetails.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
            dtQuantityDetails.Columns.Add("INTERNAL_INSPECTION", typeof(int));
            dtQuantityDetails.Columns.Add("INTERNAL_INSPECTION_REMARKS", typeof(string));
            dtQuantityDetails.Columns.Add("INTERNAL_INSPECTION_BY", typeof(string));
            dtQuantityDetails.Columns.Add("INTERNAL_INSPECTION_ON", typeof(string));
            dtQuantityDetails.Columns.Add("QA_ITEM_ACCEPTED", typeof(int));
            dtQuantityDetails.Columns.Add("QA_ITEM_ACCEPTED_REMARKS", typeof(string));
            dtQuantityDetails.Columns.Add("QA_ITEM_ACCEPTED_BY", typeof(string));
            dtQuantityDetails.Columns.Add("QA_ITEM_ACCEPTED_ON", typeof(string));

            dtQuantityDetails.Columns.Add("QA_ITEM_NOT_ACCEPTED", typeof(int));
            dtQuantityDetails.Columns.Add("QA_ITEM_NOT_ACCEPTED_REMARKS", typeof(string));
            dtQuantityDetails.Columns.Add("QA_ITEM_NOT_ACCEPTED_BY", typeof(string));
            dtQuantityDetails.Columns.Add("QA_ITEM_NOT_ACCEPTED_ON", typeof(string));

            dtQuantityDetails.Columns.Add("IN_FINAL_INSPECTION", typeof(int));
            dtQuantityDetails.Columns.Add("SENT_TO_FINAL_INSPECTION_BY", typeof(string));
            dtQuantityDetails.Columns.Add("SENT_TO_FINAL_INSPECTION_ON", typeof(string));
            dtQuantityDetails.Columns.Add("SENT_TO_FINAL_INSPECTION_REMARKS", typeof(string));

            dtQuantityDetails.Columns.Add("IN_REWORK", typeof(int));
            dtQuantityDetails.Columns.Add("SENT_TO_REWORK_BY", typeof(string));
            dtQuantityDetails.Columns.Add("SENT_TO_REWORK_ON", typeof(string));
            dtQuantityDetails.Columns.Add("SENT_TO_REWORK_REMARKS", typeof(string));


            dtQuantityDetails.Columns.Add("COMPLETE", typeof(int));
            dtQuantityDetails.Columns.Add("COMPLETE_REMARKS", typeof(string));
            dtQuantityDetails.Columns.Add("COMPLETE_BY", typeof(string));
            dtQuantityDetails.Columns.Add("COMPLETE_ON", typeof(string));

            DataRow dr = dtQuantityDetails.NewRow();
            dr["LOT_TF_SUBITEM_ID"] = 0;

            dr["INTERNAL_INSPECTION"] = 0;
            dr["INTERNAL_INSPECTION_REMARKS"] = "";
            dr["INTERNAL_INSPECTION_BY"] = "";
            dr["INTERNAL_INSPECTION_ON"] = "";

            dr["QA_ITEM_ACCEPTED"] = 0;
            dr["QA_ITEM_ACCEPTED_REMARKS"] = "";
            dr["QA_ITEM_ACCEPTED_BY"] = "";
            dr["QA_ITEM_ACCEPTED_ON"] = "";

            dr["QA_ITEM_NOT_ACCEPTED"] = 0;
            dr["QA_ITEM_NOT_ACCEPTED_REMARKS"] = "";
            dr["QA_ITEM_NOT_ACCEPTED_BY"] = "";
            dr["QA_ITEM_NOT_ACCEPTED_ON"] = "";

            dr["IN_FINAL_INSPECTION"] = 0;
            dr["SENT_TO_FINAL_INSPECTION_BY"] = "";
            dr["SENT_TO_FINAL_INSPECTION_ON"] = "";
            dr["SENT_TO_FINAL_INSPECTION_REMARKS"] = "";

            dr["IN_REWORK"] = 0;
            dr["SENT_TO_REWORK_BY"] = "";
            dr["SENT_TO_REWORK_ON"] = "";
            dr["SENT_TO_REWORK_REMARKS"] = "";

            dr["COMPLETE"] = 0;
            dr["COMPLETE_REMARKS"] = "";
            dr["COMPLETE_BY"] = "";
            dr["COMPLETE_ON"] = "";

            dtQuantityDetails.Rows.Add(dr);

            #endregion

            #endregion



            Session["dtLOT"] = null;
            Session["dtSubitems"] = null;
            Session["dtEditedOrAmended"] = null;
            Session["dtQuantityDetails"] = null;



            //Session["dtLOT"] = (DataTable)dtLOT;
            //Session["dtSubitems"] = (DataTable)dtSubitems;
            //Session["dtQuantityDetails"] = (DataTable)dtQuantityDetails;

            Session["dtLOT"] = dtLOT;
            Session["dtSubitems"] = dtSubitems;
            Session["dtEditedOrAmended"] = dtEditedOrAmended;
            Session["dtQuantityDetails"] = dtQuantityDetails;


            if (dtSubitems.Rows.Count > 0)
            {
                ModalPopupExtender4.Show();
                iframeViewTravelStatementInPDF.Attributes.Add("src", "LOTTransmittalFactoryInPDF.aspx?LOTTFID=" + LOTTFID + "&LOTTFSubitemIDs=" + LOTTFSubitemIDs + "&pdfType=" + (int)LOTAllStatusAndTypes.EnumPDFType.Edit_Preview);
            }
            else
            {
                ExceptionMessage("Please add subitems...!!!");
                return;
            }


        }
        catch (Exception ex)
        {
            //
        }
    }


    private void SaveLOTLOTTransmittalToFactory(int savingType)
    {
        try
        {
            LOTMainSubItemIDs = string.Empty;

            if (Session["dtSubitem"] != null)
                dtTemp = (DataTable)Session["dtSubitem"];
            else
                AddTempSubitemTable();

            if (dtTemp.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    if (!LOTMainSubItemIDs.Contains("," + Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"]) + ","))
                        LOTMainSubItemIDs += Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"]) + ",";
                }
            }

            if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
                LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');

            dsJobApprovers = objProject.GetJOBApprovers(txtJOBNoToEdit.Text, Convert.ToInt32(hdCompanyToEdit.Value), LOTMainSubItemIDs);
            if (dsJobApprovers.Tables.Count > 0 && dsJobApprovers.Tables[0].Rows.Count > 0)
            {
                Session["dsJobApprovers"] = dsJobApprovers;
                UpdateOrAmendLOT(savingType);
            }
            else
            {
                Session["dsJobApprovers"] = null;
                mpeAddApprovers.Show();
                iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateOrAmendLOT(int savingType)
    {
        try
        {

            #region LOT Primary Details


            string file1Text = string.Empty;
            string file2Text = string.Empty;
            string file3Text = string.Empty;
            string file4Text = string.Empty;

            isUpdateOrAmend = 0;
            isAmend = 0;
            newStatusID = 0;
            LOTTFID = 0;
            TFNo = string.Empty;
            companyID = 0;
            LOTDate = string.Empty;
            custCode = string.Empty;
            jobNo = string.Empty;
            poNo = string.Empty;
            itemName = string.Empty;
            impNotes = string.Empty;

            attachment1File = string.Empty;
            attachment1FileBytes = null;

            attachment2File = string.Empty;
            attachment2FileBytes = null;

            attachment3File = string.Empty;
            attachment3FileBytes = null;

            attachment4File = string.Empty;
            attachment4FileBytes = null;

            tableName = string.Empty;
            updateSubitemQuery = string.Empty;
            insertSubitemFlag = 0;


            LOTMainSubItemIDs = string.Empty;
            removedLOTTFSubItemIDs = string.Empty;


            if (!string.IsNullOrEmpty(hdTableName.Value))
                tableName = hdTableName.Value;

            if (Convert.ToInt32(ViewState["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

            if (!string.IsNullOrEmpty(Convert.ToString(txtTFNoToEdit.Text)))
                TFNo = Convert.ToString(txtTFNoToEdit.Text);

            if (!string.IsNullOrEmpty(hdCompanyToEdit.Value))
                companyID = Convert.ToInt32(hdCompanyToEdit.Value);

            if (!string.IsNullOrEmpty(Convert.ToString(hdDateToEdit.Value)))
                LOTDate = Convert.ToDateTime(hdDateToEdit.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(txtCustomerCodeToEdit.Text)))
                custCode = Convert.ToString(txtCustomerCodeToEdit.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNoToEdit.Text)))
                jobNo = Convert.ToString(txtJOBNoToEdit.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtPONoToEdit.Text)))
                poNo = Convert.ToString(txtPONoToEdit.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtItemNameToEdit.Text)))
                itemName = Convert.ToString(txtItemNameToEdit.Text).Replace(Environment.NewLine, " ");

            //if (!string.IsNullOrEmpty(Convert.ToString(txtNotesToEdit.Text)))
            //    impNotes = Convert.ToString(txtNotesToEdit.Text).Replace(Environment.NewLine, " "); // commented on 2022-07-19

            if (!string.IsNullOrEmpty(Convert.ToString(txtEditOrAmendNotesEdit.Text)))
                impNotes = Convert.ToString(txtEditOrAmendNotesEdit.Text).Replace(Environment.NewLine, " ");


            if (uploadFileAttachment1ToEdit.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment1ToEdit.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment1ToEdit.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment1File = str[str.Length - 1];
                    attachment1FileBytes = GetFileBytes(uploadFileAttachment1ToEdit.PostedFile.FileName, uploadFileAttachment1ToEdit.PostedFile.InputStream);
                }
            }

            //if (Session["dtAttachments"] != null)
            //    dtAttachments = (DataTable)Session["dtAttachments"];
            //else
            //    AddTempAttachmentTable();

            //dtAttachments = (DataTable)Session["dtAttachments"];

            //if (dtAttachments.Rows.Count > 0)
            //{
            //    if (!string.IsNullOrEmpty(Convert.ToString(dtAttachments.Rows[0]["ATTACHMENT1_NAME"])))
            //        attachment1File = Convert.ToString(dtAttachments.Rows[0]["ATTACHMENT1_NAME"]);

            //    if (dtAttachments.Rows[0]["ATTACHMENT1_BTYTES"] != DBNull.Value)
            //        attachment1FileBytes = (byte[])dtAttachments.Rows[0]["ATTACHMENT1_BTYTES"];



            //    if (!string.IsNullOrEmpty(Convert.ToString(dtAttachments.Rows[0]["ATTACHMENT2_NAME"])))
            //        attachment2File = Convert.ToString(dtAttachments.Rows[0]["ATTACHMENT2_NAME"]);

            //    if (dtAttachments.Rows[0]["ATTACHMENT2_BTYTES"] != DBNull.Value)
            //        attachment2FileBytes = (byte[])dtAttachments.Rows[0]["ATTACHMENT2_BTYTES"];



            //    if (!string.IsNullOrEmpty(Convert.ToString(dtAttachments.Rows[0]["ATTACHMENT3_NAME"])))
            //        attachment3File = Convert.ToString(dtAttachments.Rows[0]["ATTACHMENT3_NAME"]);

            //    if (dtAttachments.Rows[0]["ATTACHMENT3_BTYTES"] != DBNull.Value)
            //        attachment3FileBytes = (byte[])dtAttachments.Rows[0]["ATTACHMENT3_BTYTES"];



            //    if (!string.IsNullOrEmpty(Convert.ToString(dtAttachments.Rows[0]["ATTACHMENT4_NAME"])))
            //        attachment4File = Convert.ToString(dtAttachments.Rows[0]["ATTACHMENT4_NAME"]);

            //    if (dtAttachments.Rows[0]["ATTACHMENT4_BTYTES"] != DBNull.Value)
            //        attachment4FileBytes = (byte[])dtAttachments.Rows[0]["ATTACHMENT4_BTYTES"];

            //}

            #endregion


            #region LOT Subitems Detail

            #region Table To Insert

            DataTable dtNew = new DataTable();

            dtSubitemToAdd.Columns.Add("STATUS_ID", typeof(int));
            dtSubitemToAdd.Columns.Add("SUBITEM_DESC", typeof(string));
            dtSubitemToAdd.Columns.Add("TAG_NO", typeof(string));

            dtSubitemToAdd.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtSubitemToAdd.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
            dtSubitemToAdd.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            dtSubitemToAdd.Columns.Add("PRODUCT_CODE", typeof(string));
            dtSubitemToAdd.Columns.Add("PRODUCT_DESC", typeof(string));
            dtSubitemToAdd.Columns.Add("UOM", typeof(string));
            dtSubitemToAdd.Columns.Add("QUANTITY", typeof(int));

            dtSubitemToAdd.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
            dtSubitemToAdd.Columns.Add("DRAWING_NO", typeof(string));

            dtSubitemToAdd.Columns.Add("REVISION_NO", typeof(int));
            dtSubitemToAdd.Columns.Add("REVISION_NO_TEXT", typeof(string));

            dtSubitemToAdd.Columns.Add("CATEGORY_ID", typeof(string));
            dtSubitemToAdd.Columns.Add("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID", typeof(int));

            dtSubitemToAdd.Columns.Add("CREATED_BY", typeof(int));
            dtSubitemToAdd.Columns.Add("CREATED_ON", typeof(string));

            dtSubitemToAdd.Columns.Add("APPROVED_BY", typeof(int));
            dtSubitemToAdd.Columns.Add("APPROVED_ON", typeof(string));
            dtSubitemToAdd.Columns.Add("APPROVED_REMARKS", typeof(string));

            dtSubitemToAdd.Columns.Add("ACCEPTED_BY", typeof(int));
            dtSubitemToAdd.Columns.Add("ACCEPTED_ON", typeof(string));
            dtSubitemToAdd.Columns.Add("ACCEPTED_REMARKS", typeof(string));

            dtSubitemToAdd.Columns.Add("SI_ATTACHMENT1_NAME", typeof(string));
            dtSubitemToAdd.Columns.Add("SI_ATTACHMENT1_BTYTES", typeof(byte[]));

            dtSubitemToAdd.Columns.Add("SI_ATTACHMENT2_NAME", typeof(string));
            dtSubitemToAdd.Columns.Add("SI_ATTACHMENT2_BTYTES", typeof(byte[]));

            dtSubitemToAdd.Columns.Add("SI_ATTACHMENT3_NAME", typeof(string));
            dtSubitemToAdd.Columns.Add("SI_ATTACHMENT3_BTYTES", typeof(byte[]));

            dtSubitemToAdd.Columns.Add("SI_ATTACHMENT4_NAME", typeof(string));
            dtSubitemToAdd.Columns.Add("SI_ATTACHMENT4_BTYTES", typeof(byte[]));

            #endregion

            #region Table To Update or Amend

            DataTable dtSIToAmendOrUpdate = new DataTable();

            dtSIToAmendOrUpdate.Columns.Add("SR_NO", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("LOT_TF_ID", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("STATUS_ID", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("PRODUCT_CODE", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("PRODUCT_DESC", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("UOM", typeof(string));

            dtSIToAmendOrUpdate.Columns.Add("REVISION_NO", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("REVISION_NO_TEXT", typeof(string));

            dtSIToAmendOrUpdate.Columns.Add("QUANTITY", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("SUBITEM_DESC", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("TAG_NO", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("DRAWING_NO", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("CATEGORY", typeof(string));

            dtSIToAmendOrUpdate.Columns.Add("AMENDED_BY", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("AMENDED_ON", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("AMENDED_REMARKS", typeof(string));

            dtSIToAmendOrUpdate.Columns.Add("SI_ATTACHMENT1_NAME", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("SI_ATTACHMENT1_DOC", typeof(byte[]));

            dtSIToAmendOrUpdate.Columns.Add("SI_ATTACHMENT2_NAME", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("SI_ATTACHMENT2_DOC", typeof(byte[]));

            dtSIToAmendOrUpdate.Columns.Add("SI_ATTACHMENT3_NAME", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("SI_ATTACHMENT3_DOC", typeof(byte[]));

            dtSIToAmendOrUpdate.Columns.Add("SI_ATTACHMENT4_NAME", typeof(string));
            dtSIToAmendOrUpdate.Columns.Add("SI_ATTACHMENT4_DOC", typeof(byte[]));

            dtSIToAmendOrUpdate.Columns.Add("MODIFIED_BY", typeof(int));
            dtSIToAmendOrUpdate.Columns.Add("MODIFIED_ON", typeof(string));

            #endregion


            if (Session["dtSubitem"] != null)
                dtNew = (DataTable)Session["dtSubitem"];
            else
                AddTempSubitemTable();


            if (gvSubItemToU.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItemToU.Rows)
                {
                    Label lblSrNo = gr.FindControl("lblSrNo") as Label;
                    TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;

                    if (dtNew.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtNew.Select("SR_NO='" + Convert.ToInt32(lblSrNo.Text) + "'"))
                        {
                            dr["TAG_NO"] = txtTagNoInList.Text;
                        }
                    }
                }
            }


            //if (Session["dsJobApprovers"] != null)
            //    dsJobApprovers = (DataSet)Session["dsJobApprovers"];
            //else
            //    dsJobApprovers = objProject.GetJOBApprovers(jobNo,);


            //if (dsJobApprovers.Tables.Count > 0)
            //{
            //    if (dsJobApprovers.Tables[0].Rows.Count == 0)
            //    {
            //        mpeAddApprovers.Show();
            //        iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0");
            //        return;
            //    }
            //}
            //else
            //{
            //    mpeAddApprovers.Show();
            //    iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0");
            //    return;
            //}

            int subitemsCount = 0;

            if (dtNew.Rows.Count > 0)
            {
                foreach (DataRow dr in dtNew.Rows)
                {
                    statusID = 0;

                    subitemDesc = string.Empty;
                    tagNo = string.Empty;


                    LOTTFSubitemID = 0;
                    LOTMainItem = string.Empty;
                    LOTMainSubItem = string.Empty;
                    LOTMainItemID = 0;
                    LOTMainSubItemID = 0;

                    drgNo = string.Empty;

                    revisionNo = 0;
                    revisionNoText = "";

                    quantity = 0;

                    productionOrderNo = string.Empty;
                    productionOrderDate = string.Empty;
                    expectedCompletionDate = string.Empty;
                    productCode = string.Empty;
                    productDesc = string.Empty;
                    UOM = string.Empty;
                    quantity = 0;

                    isPartOfProductionStatusID = 0;

                    categoryID = string.Empty;
                    category = string.Empty;

                    createdByID = 0;
                    amendedByID = 0;
                    PEID = 0;
                    PMID = 0;
                    approvedByID = 0;
                    amendedApprovedByID = 0;
                    prodMngrID = 0;
                    acceptedByID = 0;
                    amendmentCount = 0;

                    SiDrawing1File = string.Empty;
                    SiDrawing2File = string.Empty;
                    SiDrawing3File = string.Empty;
                    SiDrawing4File = string.Empty;

                    SiDrawing1FileBytes = null;
                    SiDrawing2FileBytes = null;
                    SiDrawing3FileBytes = null;
                    SiDrawing4FileBytes = null;


                    createdByID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                    if (dsJobApprovers.Tables.Count > 0)
                    {
                        if (dsJobApprovers.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dsJobApprovers.Tables[0].Select("JOB_NO='" + jobNo + "'"))
                            {
                                PEID = Convert.ToInt32(dr1["PE_ID"]);
                                PMID = Convert.ToInt32(dr1["PM_ID"]);
                            }
                        }

                        if (dsJobApprovers.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dsJobApprovers.Tables[1].Select("LOT_MAIN_SUBITEM_ID='" + Convert.ToInt32(dr["LOT_MAIN_SUBITEM_ID"]) + "'"))
                            {
                                prodMngrID = Convert.ToInt32(dr2["MANAGER_ID"]);
                            }
                        }
                    }


                    //if (amendmentCount > 0)
                    //    objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(amendedByID, PEID, PMID, prodMngrID, amendedApprovedByID), amendmentCount, 0, PEID, PMID, prodMngrID);
                    //else
                    //    objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, 0, PEID, PMID, prodMngrID);


                    if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Edit))
                    {
                        isUpdateOrAmend = 1;
                        isAmend = 0;
                        if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                        {
                            statusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                        }
                        else if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                        {
                            statusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                        }

                        if (dr["APPROVED_BY_ID"] != DBNull.Value && Convert.ToInt32(dr["APPROVED_BY_ID"]) > 0)
                            approvedByID = Convert.ToInt32(dr["APPROVED_BY_ID"]);
                        else
                            approvedByID = 0;
                    }
                    else if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Amend))// ||
                                                                                                                      //hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.EditAmended))
                    {
                        isUpdateOrAmend = 1;
                        isAmend = Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTUpdationType.Amend);
                        statusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);
                    }

                    //approvedByID = objLOTMailTypeAndStatusProperties.ApprovedByID;
                    //acceptedByID = objLOTMailTypeAndStatusProperties.AcceptedByID;


                    if (dr["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SUBITEM_DESC"])))
                        subitemDesc = Convert.ToString(dr["SUBITEM_DESC"]).Replace(Environment.NewLine, " ");

                    if (dr["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["TAG_NO"])))
                        tagNo = Convert.ToString(dr["TAG_NO"]).Replace(Environment.NewLine, " "); ;

                    if (dr["PRODUCTION_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PRODUCTION_ORDER_NO"])))
                        productionOrderNo = Convert.ToString(dr["PRODUCTION_ORDER_NO"]);

                    if (dr["PRODUCTION_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PRODUCTION_ORDER_DATE"])))
                        productionOrderDate = Convert.ToDateTime(dr["PRODUCTION_ORDER_DATE"]).ToString("yyyy-MM-dd");

                    if (dr["EXPECTED_COMPLETION_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EXPECTED_COMPLETION_DATE"])))
                        expectedCompletionDate = Convert.ToDateTime(dr["EXPECTED_COMPLETION_DATE"]).ToString("yyyy-MM-dd");

                    if (dr["PRODUCT_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PRODUCT_CODE"])))
                        productCode = Convert.ToString(dr["PRODUCT_CODE"]);

                    if (dr["PRODUCT_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PRODUCT_DESC"])))
                        productDesc = Convert.ToString(dr["PRODUCT_DESC"]);

                    if (dr["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
                        UOM = Convert.ToString(dr["UOM"]);

                    if (dr["QUANTITY"] != DBNull.Value && Convert.ToInt32(dr["QUANTITY"]) > 0)
                        quantity = Convert.ToInt32(dr["QUANTITY"]);




                    if (dr["LOT_TF_SUBITEM_ID"] != DBNull.Value && Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]) > 0)
                        LOTTFSubitemID = Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]);

                    if (dr["LOT_MAIN_SUBITEM_ID"] != DBNull.Value && Convert.ToInt32(dr["LOT_MAIN_SUBITEM_ID"]) > 0)
                        LOTMainSubItemID = Convert.ToInt32(dr["LOT_MAIN_SUBITEM_ID"]);

                    if (dr["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
                        drgNo = Convert.ToString(dr["DRAWING_NO"]);

                    if (dr["REVISION_NO"] != DBNull.Value && Convert.ToInt32(dr["REVISION_NO"]) > 0)
                        revisionNo = Convert.ToInt32(dr["REVISION_NO"]);

                    if (dr["REVISION_NO_TEXT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REVISION_NO_TEXT"])))
                        revisionNoText = Convert.ToString(dr["REVISION_NO_TEXT"]);

                    if (dr["CATEGORY_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CATEGORY_ID"])))
                        categoryID = Convert.ToString(dr["CATEGORY_ID"]);


                    if (dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] != DBNull.Value && Convert.ToInt32(dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                        isPartOfProductionStatusID = Convert.ToInt32(dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);


                    if (dr["SI_ATTACHMENT1_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT1_NAME"])))
                        SiDrawing1File = Convert.ToString(dr["SI_ATTACHMENT1_NAME"]);

                    if (dr["SI_ATTACHMENT1_BTYTES"] != DBNull.Value)
                        SiDrawing1FileBytes = (byte[])dr["SI_ATTACHMENT1_BTYTES"];


                    if (dr["SI_ATTACHMENT2_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT2_NAME"])))
                        SiDrawing2File = Convert.ToString(dr["SI_ATTACHMENT2_NAME"]);

                    if (dr["SI_ATTACHMENT2_BTYTES"] != DBNull.Value)
                        SiDrawing2FileBytes = (byte[])dr["SI_ATTACHMENT2_BTYTES"];


                    if (dr["SI_ATTACHMENT3_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT3_NAME"])))
                        SiDrawing3File = Convert.ToString(dr["SI_ATTACHMENT3_NAME"]);

                    if (dr["SI_ATTACHMENT3_BTYTES"] != DBNull.Value)
                        SiDrawing3FileBytes = (byte[])dr["SI_ATTACHMENT3_BTYTES"];


                    if (dr["SI_ATTACHMENT4_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT4_NAME"])))
                        SiDrawing4File = Convert.ToString(dr["SI_ATTACHMENT4_NAME"]);

                    if (dr["SI_ATTACHMENT4_BTYTES"] != DBNull.Value)
                        SiDrawing4FileBytes = (byte[])dr["SI_ATTACHMENT4_BTYTES"];



                    if (LOTTFSubitemID == 0)
                    {
                        DataRow drn = dtSubitemToAdd.NewRow();

                        if (statusID > 0)
                            drn["STATUS_ID"] = statusID;
                        else drn["STATUS_ID"] = 0;

                        if (!string.IsNullOrEmpty(subitemDesc))
                            drn["SUBITEM_DESC"] = subitemDesc;
                        else drn["SUBITEM_DESC"] = string.Empty;

                        if (!string.IsNullOrEmpty(tagNo))
                            drn["TAG_NO"] = tagNo;
                        else drn["TAG_NO"] = string.Empty;

                        if (LOTMainSubItemID > 0)
                        {
                            drn["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;

                            //if (!LOTMainSubItemIDs.Contains("," + Convert.ToString(drn["LOT_MAIN_SUBITEM_ID"]) + ","))
                            //    LOTMainSubItemIDs += Convert.ToString(drn["LOT_MAIN_SUBITEM_ID"]) + ",";
                        }

                        else drn["LOT_MAIN_SUBITEM_ID"] = 0;



                        if (!string.IsNullOrEmpty(drgNo))
                            drn["DRAWING_NO"] = drgNo;
                        else drn["DRAWING_NO"] = string.Empty;

                        if (revisionNo > 0)
                            drn["REVISION_NO"] = revisionNo;
                        else drn["REVISION_NO"] = 0;

                        if (!string.IsNullOrEmpty(revisionNoText))
                            drn["REVISION_NO_TEXT"] = revisionNoText;
                        else drn["REVISION_NO_TEXT"] = string.Empty;

                        if (!string.IsNullOrEmpty(categoryID))
                            drn["CATEGORY_ID"] = categoryID;
                        else drn["CATEGORY_ID"] = string.Empty;

                        if (quantity > 0)
                            drn["QUANTITY"] = quantity;
                        else drn["QUANTITY"] = 0;

                        if (!string.IsNullOrEmpty(productionOrderNo))
                            drn["PRODUCTION_ORDER_NO"] = productionOrderNo;
                        else drn["PRODUCTION_ORDER_NO"] = string.Empty;

                        if (!string.IsNullOrEmpty(productionOrderDate))
                            drn["PRODUCTION_ORDER_DATE"] = Convert.ToDateTime(productionOrderDate).ToString("yyyy-MM-dd");
                        else drn["PRODUCTION_ORDER_DATE"] = string.Empty;

                        if (!string.IsNullOrEmpty(expectedCompletionDate))
                            drn["EXPECTED_COMPLETION_DATE"] = Convert.ToDateTime(expectedCompletionDate).ToString("yyyy-MM-dd");
                        else drn["EXPECTED_COMPLETION_DATE"] = string.Empty;

                        if (!string.IsNullOrEmpty(productCode))
                            drn["PRODUCT_CODE"] = productCode;
                        else drn["PRODUCT_CODE"] = string.Empty;

                        if (!string.IsNullOrEmpty(productDesc))
                            drn["PRODUCT_DESC"] = productDesc;
                        else drn["PRODUCT_DESC"] = string.Empty;

                        if (!string.IsNullOrEmpty(UOM))
                            drn["UOM"] = UOM;
                        else drn["UOM"] = string.Empty;

                        if (isPartOfProductionStatusID > 0)
                            drn["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;
                        else drn["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = 0;



                        if (createdByID > 0)
                        {
                            drn["CREATED_BY"] = createdByID;
                            drn["CREATED_ON"] = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            drn["CREATED_BY"] = 0;
                            drn["CREATED_ON"] = null;
                        }

                        if (approvedByID > 0)
                        {
                            drn["APPROVED_BY"] = approvedByID;
                            drn["APPROVED_ON"] = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            drn["APPROVED_BY"] = 0;
                            drn["APPROVED_ON"] = null;
                        }

                        drn["APPROVED_REMARKS"] = string.Empty;

                        if (acceptedByID > 0)
                        {
                            drn["ACCEPTED_BY"] = acceptedByID;
                            drn["ACCEPTED_ON"] = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            drn["ACCEPTED_BY"] = 0;
                            drn["ACCEPTED_ON"] = null;
                        }

                        drn["ACCEPTED_REMARKS"] = string.Empty;


                        if (!string.IsNullOrEmpty(SiDrawing1File))
                            drn["SI_ATTACHMENT1_NAME"] = SiDrawing1File;
                        else drn["SI_ATTACHMENT1_NAME"] = string.Empty;

                        if (SiDrawing1FileBytes != null)
                            drn["SI_ATTACHMENT1_BTYTES"] = SiDrawing1FileBytes;
                        else drn["SI_ATTACHMENT1_BTYTES"] = null;



                        if (!string.IsNullOrEmpty(SiDrawing2File))
                            drn["SI_ATTACHMENT2_NAME"] = SiDrawing2File;
                        else drn["SI_ATTACHMENT2_NAME"] = string.Empty;

                        if (SiDrawing2FileBytes != null)
                            drn["SI_ATTACHMENT2_BTYTES"] = SiDrawing2FileBytes;
                        else drn["SI_ATTACHMENT2_BTYTES"] = null;



                        if (!string.IsNullOrEmpty(SiDrawing3File))
                            drn["SI_ATTACHMENT3_NAME"] = SiDrawing3File;
                        else drn["SI_ATTACHMENT3_NAME"] = string.Empty;

                        if (SiDrawing3FileBytes != null)
                            drn["SI_ATTACHMENT3_BTYTES"] = SiDrawing3FileBytes;
                        else drn["SI_ATTACHMENT3_BTYTES"] = null;



                        if (!string.IsNullOrEmpty(SiDrawing4File))
                            drn["SI_ATTACHMENT4_NAME"] = SiDrawing4File;
                        else drn["SI_ATTACHMENT4_NAME"] = string.Empty;

                        if (SiDrawing4FileBytes != null)
                            drn["SI_ATTACHMENT4_BTYTES"] = SiDrawing4FileBytes;
                        else drn["SI_ATTACHMENT4_BTYTES"] = null;

                        dtSubitemToAdd.Rows.Add(drn);
                    }
                    else
                    {
                        DataRow drua = dtSIToAmendOrUpdate.NewRow();

                        subitemsCount++;
                        drua["SR_NO"] = subitemsCount;
                        drua["LOT_TF_ID"] = LOTTFID;
                        drua["LOT_TF_SUBITEM_ID"] = LOTTFSubitemID;


                        if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Edit))
                        {
                            if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                                drua["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                            else if (Convert.ToInt32(dr["STATUS_ID"]) == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                                drua["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);

                            drua["AMENDED_BY"] = 0;
                            drua["AMENDED_ON"] = null;
                            drua["AMENDED_REMARKS"] = string.Empty;
                        }
                        else if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Amend))
                        //|| hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.EditAmended))
                        {
                            drua["STATUS_ID"] = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended);

                            drua["AMENDED_BY"] = createdByID;
                            drua["AMENDED_ON"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            //drua["AMENDED_REMARKS"] = txtNotesToEdit.Text;
                            drua["AMENDED_REMARKS"] = txtEditOrAmendNotesEdit.Text;
                        }

                        if (!string.IsNullOrEmpty(productionOrderNo))
                            drua["PRODUCTION_ORDER_NO"] = productionOrderNo;
                        else drua["PRODUCTION_ORDER_NO"] = string.Empty;

                        if (!string.IsNullOrEmpty(productionOrderDate))
                            drua["PRODUCTION_ORDER_DATE"] = Convert.ToDateTime(productionOrderDate).ToString("yyyy-MM-dd");
                        else drua["PRODUCTION_ORDER_DATE"] = string.Empty;

                        if (!string.IsNullOrEmpty(expectedCompletionDate))
                            drua["EXPECTED_COMPLETION_DATE"] = Convert.ToDateTime(expectedCompletionDate).ToString("yyyy-MM-dd");
                        else drua["EXPECTED_COMPLETION_DATE"] = string.Empty;

                        if (!string.IsNullOrEmpty(productCode))
                            drua["PRODUCT_CODE"] = productCode;
                        else drua["PRODUCT_CODE"] = string.Empty;

                        if (!string.IsNullOrEmpty(productDesc))
                            drua["PRODUCT_DESC"] = productDesc;
                        else drua["PRODUCT_DESC"] = string.Empty;

                        if (!string.IsNullOrEmpty(UOM))
                            drua["UOM"] = UOM;
                        else drua["UOM"] = string.Empty;

                        if (revisionNo > 0)
                            drua["REVISION_NO"] = revisionNo;
                        else drua["REVISION_NO"] = 0;

                        if (!string.IsNullOrEmpty(revisionNoText))
                            drua["REVISION_NO_TEXT"] = revisionNoText;
                        else drua["REVISION_NO_TEXT"] = string.Empty;

                        if (quantity > 0)
                            drua["QUANTITY"] = quantity;
                        else drua["QUANTITY"] = 0;

                        if (isPartOfProductionStatusID > 0)
                            drua["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;
                        else drua["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = 0;

                        if (!string.IsNullOrEmpty(subitemDesc))
                            drua["SUBITEM_DESC"] = subitemDesc;
                        else drua["SUBITEM_DESC"] = string.Empty;

                        if (!string.IsNullOrEmpty(tagNo))
                            drua["TAG_NO"] = tagNo;
                        else drua["TAG_NO"] = string.Empty;

                        if (LOTMainSubItemID > 0)
                            drua["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
                        else drua["LOT_MAIN_SUBITEM_ID"] = 0;

                        if (!string.IsNullOrEmpty(drgNo))
                            drua["DRAWING_NO"] = drgNo;
                        else drua["DRAWING_NO"] = string.Empty;

                        if (!string.IsNullOrEmpty(categoryID))
                            drua["CATEGORY"] = categoryID;
                        else drua["CATEGORY"] = string.Empty;

                        if (!string.IsNullOrEmpty(SiDrawing1File))
                            drua["SI_ATTACHMENT1_NAME"] = SiDrawing1File;
                        else drua["SI_ATTACHMENT1_NAME"] = string.Empty;

                        if (SiDrawing1FileBytes != null)
                            drua["SI_ATTACHMENT1_DOC"] = SiDrawing1FileBytes;
                        else drua["SI_ATTACHMENT1_DOC"] = null;

                        if (!string.IsNullOrEmpty(SiDrawing2File))
                            drua["SI_ATTACHMENT2_NAME"] = SiDrawing2File;
                        else drua["SI_ATTACHMENT2_NAME"] = string.Empty;

                        if (SiDrawing2FileBytes != null)
                            drua["SI_ATTACHMENT2_DOC"] = SiDrawing2FileBytes;
                        else drua["SI_ATTACHMENT2_DOC"] = null;

                        if (!string.IsNullOrEmpty(SiDrawing3File))
                            drua["SI_ATTACHMENT3_NAME"] = SiDrawing3File;
                        else drua["SI_ATTACHMENT3_NAME"] = string.Empty;

                        if (SiDrawing3FileBytes != null)
                            drua["SI_ATTACHMENT3_DOC"] = SiDrawing3FileBytes;
                        else drua["SI_ATTACHMENT3_DOC"] = null;

                        if (!string.IsNullOrEmpty(SiDrawing4File))
                            drua["SI_ATTACHMENT4_NAME"] = SiDrawing4File;
                        else drua["SI_ATTACHMENT4_NAME"] = string.Empty;

                        if (SiDrawing4FileBytes != null)
                            drua["SI_ATTACHMENT4_DOC"] = SiDrawing4FileBytes;
                        else drua["SI_ATTACHMENT4_DOC"] = null;

                        drua["MODIFIED_BY"] = createdByID;
                        drua["MODIFIED_ON"] = DateTime.Now.ToString("yyyy-MM-dd");


                        dtSIToAmendOrUpdate.Rows.Add(drua);
                    }
                }
            }
            else
            {
                ExceptionMessage("Please select atleast 1 subitem...!!!");
                return;
            }

            #endregion

            if (!string.IsNullOrEmpty(hdRemovedSubitemIDs.Value))
                removedLOTTFSubItemIDs = hdRemovedSubitemIDs.Value.TrimEnd(',');

            bool checkForTAGNO = true;
            int isPartOfProductionCount = 0;

            if (dtSubitemToAdd.Rows.Count > 0)
            {
                insertSubitemFlag = 1;
            }


            if (gvSubItemToU.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItemToU.Rows)
                {
                    TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;

                    if (string.IsNullOrEmpty(txtTagNoInList.Text.Trim()))
                    {
                        checkForTAGNO = false;
                        break;
                    }
                }

                //foreach (GridViewRow gr in gvSubItemToU.Rows)
                //{
                //    CheckBox chkIsPartOfProductStatusReport = gr.FindControl("chkIsPartOfProductStatusReport") as CheckBox;
                //    if (chkIsPartOfProductStatusReport.Checked)
                //    {
                //        isPartOfProductionCount++;
                //    }
                //}
            }

            int value = 0;

            if (checkForTAGNO == true)
            {
                value = objProject.UpdateOrAmendLOTDetailsTwo(LOTTFID, TFNo, companyID, LOTDate, custCode, jobNo, poNo, itemName, impNotes,
                                                            attachment1File, attachment1FileBytes,
                                                            attachment2File, attachment2FileBytes,
                                                            attachment3File, attachment3FileBytes,
                                                            attachment4File, attachment4FileBytes, removedLOTTFSubItemIDs,
                                                            insertSubitemFlag, isAmend,
                                                            dtSubitemToAdd,
                                                            subitemsCount, isUpdateOrAmend,
                                                            dtSIToAmendOrUpdate, savingType,
                                                            Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }
            else
            {
                mpeUpdateLOT.Show();

                if (!checkForTAGNO)
                {
                    ExceptionMessage("TAG number is empty in subitem list...!!!");
                    return;
                }

                //if (isPartOfProductionCount == 0)
                //{
                //    ExceptionUpdateMessage("Please select atleast 1 subitem as part of production in subitem list...!!!");
                //    return;
                //}
            }


            if (value > 0)
            {
                int sendMailValue = 0;

                if (savingType == Convert.ToInt32(LOTAllStatusAndTypes.SavingType.SaveAndSendForApproval))
                {
                    if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Edit))
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, "", TFNo, companyID, "",
                            //0, 
                            0, Convert.ToInt32(LOTAllStatusAndTypes.EnumLOTUpdationType.Edit), null, 0, 0, 0);
                    }
                    else
                    {
                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, "", TFNo, companyID, "",
                            //0,
                            0, 0, null, 0, 0, 0);
                    }
                }


                if (sendMailValue > 0)
                {
                    int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, "", statusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Edit))
                    {
                        SuccessMessage("LOT with TF. No.: '" + TFNo + "' updated and mail sent successfully.");
                    }
                    else if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Amend))// ||
                                                                                                                      //hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.EditAmended))
                    {
                        SuccessMessage("LOT with TF. No.: '" + TFNo + "' amended and mail sent successfully.");
                    }

                    Reset();
                }
                else
                {
                    if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Edit))
                    {
                        SuccessMessage("LOT with TF. No.: '" + TFNo + "' updated successfully.");
                    }
                    else if (hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.Amend)) //||
                                                                                                                       //hdUpdationType.Value == Convert.ToString(LOTAllStatusAndTypes.EnumLOTUpdationType.EditAmended))
                    {
                        SuccessMessage("LOT with TF. No.: '" + TFNo + "' amended successfully.");
                    }

                    Reset();
                }


                GetLOTList();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void SaveClientApprovedDrawing(int LOTTFID)
    {
        try
        {

            clientApproveDrawingFileBytes = null;
            clientApproveDrawingFile = string.Empty;
            clientApproveDrawingRemarks = string.Empty;


            if (uploadFileClientApprovedDrawing.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileClientApprovedDrawing.PostedFile.FileName))
                {
                    string[] str = uploadFileClientApprovedDrawing.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    clientApproveDrawingFile = str[str.Length - 1];
                    clientApproveDrawingFile = clientApproveDrawingFile.Replace(" ", "");
                    clientApproveDrawingFile = clientApproveDrawingFile.Replace("(", "");
                    clientApproveDrawingFile = clientApproveDrawingFile.Replace(")", "");
                    clientApproveDrawingFileBytes = GetFileBytes(uploadFileClientApprovedDrawing.PostedFile.FileName, uploadFileClientApprovedDrawing.PostedFile.InputStream);
                }
            }

            if (!string.IsNullOrEmpty(Convert.ToString(txtRemarksInCAD.Text)))
                clientApproveDrawingRemarks = Convert.ToString(txtRemarksInCAD.Text);


            int value = objProject.SaveClientApprovedDrawing(LOTTFID,
                                                             clientApproveDrawingFile,
                                                             clientApproveDrawingFileBytes,
                                                             clientApproveDrawingRemarks,
                                                             Convert.ToInt32(lblSeqNo.Text),
                                                             Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                int sendMailValue = 0;
                sendMailValue = SendClientApprovedDrawingEmail(LOTTFID, Convert.ToInt32(lblSeqNo.Text));

                if (sendMailValue > 0)
                {
                    int val = objProject.UpdateClientApprovedDrawingMailStatus(LOTTFID, Convert.ToInt32(lblSeqNo.Text), Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    SuccessMessage("Client approved drawing saved with TF. No.: '" + txtTFNoInCAD.Text + "' and mail sent successfully.");
                    ResetClientApprovedDrawing();
                }
                else
                {
                    SuccessMessage("Client approved drawing saved with TF. No.: '" + txtTFNoInCAD.Text + "' successfully.");
                    ResetClientApprovedDrawing();
                }


                GetLOTList();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int SendClientApprovedDrawingEmail(int LOTTFID, int seqNo)
    {
        try
        {
            int returnVal = 0;
            DataSet dsMailInfo = new DataSet();
            dsMailInfo = objProject.GetClientApprovedDrawingMailInfo(LOTTFID, seqNo);

            if (dsMailInfo.Tables.Count > 0 && dsMailInfo.Tables[0].Rows.Count > 0)
            {
                string fileName = "~/PROJECT/LOT/EMAIL_FORMATS/20ClientApprovedDrawingMail.htm";
                string tfNo = string.Empty;
                string from = string.Empty;
                string fromName = string.Empty;
                string to = string.Empty;
                string cc = string.Empty;
                string bcc = string.Empty;
                string drawingName = string.Empty;
                string subject = string.Empty;
                Byte[] drawingBytes = null;


                DataRow dr0 = dsMailInfo.Tables[0].Rows[0];

                tfNo = Convert.ToString(dr0["TF_NO"]);
                fromName = Convert.ToString(dr0["CLIENT_APPROVED_DRAWING_SAVED_BY"]);
                from = Convert.ToString(dr0["CLIENT_APPROVED_DRAWING_SAVED_BY_EMAIL"]);
                //cc = Convert.ToString(dr0["CREATED_BY_EMAIL_ID"]);
                drawingName = Convert.ToString(dr0["CLIENT_APPROVED_DRAWING_NAME"]);
                drawingBytes = (byte[])dr0["CLIENT_APPROVED_DRAWING_DOC"];

                if (dsMailInfo.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr2 in dsMailInfo.Tables[2].Rows)
                    {
                        to += Convert.ToString(dr2["PLANNING_MNGR_EMAIL"]) + ";";
                    }
                }

                if (dsMailInfo.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr3 in dsMailInfo.Tables[3].Rows)
                    {
                        to += Convert.ToString(dr3["PROD_MNGR_EAMIL"]) + ";";
                    }
                }


                if (dsMailInfo.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr4 in dsMailInfo.Tables[4].Rows)
                    {
                        to += Convert.ToString(dr4["QUALITY_MNGR_EAMIL"]) + ";";
                    }
                }

                if (dsMailInfo.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsMailInfo.Tables[1].Rows)
                    {
                        cc += ";" + Convert.ToString(dr1["PM_EMAIL"]) + ";" + Convert.ToString(dr1["PE_EMAIL"]);
                    }
                }

                if (!string.IsNullOrEmpty(to))
                    to = to.TrimEnd(';');

                if (!string.IsNullOrEmpty(cc))
                    cc = cc.TrimStart(';');


                subject = "Client Approved Drawing: " + drawingName;

                returnVal = SendClientApprovedDrawingMail(tfNo, drawingName, from, fromName, to, cc, bcc, subject, drawingBytes, fileName);
            }

            return returnVal;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
        }
    }

    private int SendClientApprovedDrawingMail(string TFNo, string drawingNo, string from, string fromName, string to,
                                              string cc, string bcc, string subject, byte[] drawingBytes, string fileName)
    {
        string body = string.Empty;
        int returnVal = 0;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        if (!string.IsNullOrEmpty(subject))
            mail.Subject = subject;

        if (!string.IsNullOrEmpty(from))
            mail.From = new MailAddress(from);

        if (!string.IsNullOrEmpty(to))
        {
            to = to.TrimEnd(';');
            string items = string.Empty;
            string[] strTo = to.Split(';');
            foreach (string item in strTo)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!items.Contains(item))
                    {
                        items += item + ";";
                    }
                }
            }

            if (!string.IsNullOrEmpty(items))
                items = items.TrimEnd(';');

            string[] strToNew = items.Split(';');

            foreach (string item in strToNew)
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
            string items = string.Empty;
            string[] strCC = cc.Split(';');

            foreach (string item in strCC)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!items.Contains(item))
                    {
                        items += item + ";";
                    }
                }
            }

            if (!string.IsNullOrEmpty(items))
                items = items.TrimEnd(';');

            string[] strCCNew = items.Split(';');

            foreach (string item in strCCNew)
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
            string items = string.Empty;
            string[] strBCC = bcc.Split(';');
            foreach (string item in strBCC)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!items.Contains(item))
                    {
                        items += item + ";";
                    }
                }
            }


            if (!string.IsNullOrEmpty(items))
                items = items.TrimEnd(';');

            string[] strBCCNew = items.Split(';');
            foreach (string item in strBCCNew)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.Bcc.Add(item);
                }
            }
        }

        mail.IsBodyHtml = true;

        using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(fileName)))
        {
            body = reader.ReadToEnd();
        }

        body = body.Replace("{#tfno#}", TFNo);
        body = body.Replace("{#fromname#}", fromName);

        mail.Body = body;

        if (drawingBytes != null)
        {
            mail.Attachments.Add(new Attachment(new MemoryStream(drawingBytes), drawingNo + ".pdf"));
        }


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

        return returnVal;

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


    private void Reset()
    {
        try
        {
            if (ddlLOTMainItemsToEdit.Items.Count > 0)
            {
                ddlLOTMainItemsToEdit.SelectedIndex = 0;
            }

            ddlLOTMainSubitemsToEdit.Items.Clear();
            ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
            ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

            txtCustomerCodeToEdit.Text = string.Empty;
            txtCustomerNameToEdit.Text = string.Empty;

            txtDateToEdit.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            txtJOBNoToEdit.Text = string.Empty;
            txtPONoToEdit.Text = string.Empty;
            txtItemNameToEdit.Text = string.Empty;
            txtTFNoToEdit.Text = string.Empty;
            txtNotesToEdit.Text = string.Empty;
            txtEditOrAmendNotesEdit.Text = string.Empty;

            Session["dtSubitem"] = null;



            dtTemp.Clear();
            dtSubitem.Clear();
            gvSubItemToU.DataSource = null;
            gvSubItemToU.DataBind();


            Session["dtAttachments"] = null;
            dtTempAttachments.Clear();
            gvAttachments.DataSource = null;
            gvAttachments.DataBind();


            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    private void ResetClientApprovedDrawing()
    {
        try
        {
            txtTFNoInCAD.Text = string.Empty;
            txtUnitInCAD.Text = string.Empty;
            txtJOBNumberInCAD.Text = string.Empty;
            txtCustomerPONoInCAD.Text = string.Empty;
            txtCustomerNameInCAD.Text = string.Empty;
            txtCustomerCodeInCAD.Text = string.Empty;
            txtRemarksInCAD.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }







    private void ResetSubitems()
    {
        ddlProductCodeToEdit.Items.Clear();
        ddlProductCodeToEdit.Items.Insert(0, "Select");
        ddlProductCodeToEdit.SelectedIndex = 0;
        txtProductionOrderDateToEdit.Text = string.Empty;
        txtUOMToEdit.Text = string.Empty;
        txtProductDescToEdit.Text = string.Empty;
        chkIsPartOfProductionOrMainDrawingToEdit.Checked = false;

        ddlLOTMainItemsToEdit.SelectedIndex = 0;

        ddlLOTMainSubitemsToEdit.Items.Clear();
        ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
        ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

        txtDescriptionToEdit.Text = string.Empty;
        txtDrgNoToEdit.Text = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
        {
            item.Selected = false;
        }
        hdQuantityToEdit.Value = "0";
        txtQuantityToEdit.Text = string.Empty;
        txtTagNoToEdit.Text = string.Empty;
        txtProductionOrderNoToEdit.Text = string.Empty;
        txtExpectedCompletionDateToEdit.Text = string.Empty;

        ddlRevNoToEdit.SelectedIndex = 0;
        ddlRevNoToEdit.Enabled = true;
        txtRevNoTextToEdit.Text = "00";
        txtRevNoTextToEdit.Enabled = false;
        hdRevNoTextToEdit.Value = "";
        hdRevNoTextOldToEdit.Value = "";
    }

    private void ExceptionUpdateMessage(string message)
    {
        pnlUpdateMsg.Visible = true;
        lblUpdateMsg.Text = message;
        lblUpdateMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HideUpdatePanel()
    {
        pnlUpdateMsg.Visible = false;
        lblUpdateMsg.Text = string.Empty;
    }


    private void ExceptionAddSubitemMessage(string message)
    {
        pnlAddUpdatedSubitemsMsg.Visible = true;
        txtAddUpdatedSubitemsMsg.Text = message;
        txtAddUpdatedSubitemsMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HideAddSubitemPanel()
    {
        pnlAddUpdatedSubitemsMsg.Visible = false;
        txtAddUpdatedSubitemsMsg.Text = string.Empty;
    }


    #endregion METHODS END[=================]


    #endregion EDIT/AMENDMENT STATUS END[=============]


    protected void chkClearDates_CheckedChanged(object sender, EventArgs e)
    {
        if (chkClearDates.Checked)
        {
            txtStartDateSearch.Text = "";
            hdStartDateSearch.Value = "";

            txtEndDateSearch.Text = "";
            hdEndDateSearch.Value = "";

        }
        else
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
            txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

            var endDate = startDate.AddMonths(1).AddDays(-1);
            hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
            txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
        }

        GetLOTList();

    }
}