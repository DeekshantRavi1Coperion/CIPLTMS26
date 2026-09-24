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

public partial class PROJECT_LOT_LOTTransmittalFactoryTransferredLOTList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    LOTSendMail objLOTSendMail = new LOTSendMail();
    GetLOTMailTypeAndStatus objGetLOTMailTypeAndStatus = new GetLOTMailTypeAndStatus();
    LOTMailTypeAndStatusProperties objLOTMailTypeAndStatusProperties = new LOTMailTypeAndStatusProperties();
    GetLOTApproverStatus objGetLOTApproverStatus = new GetLOTApproverStatus();

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsSubitems = new DataSet();
    DataSet dsLOTMainItems = new DataSet();
    DataSet dsLOTMainSubItems = new DataSet();
    DataSet dsLOTList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsLOTfor = new DataSet();
    DataSet dsLOTStatus = new DataSet();

    DataTable dtTemp = new DataTable();
    DataTable dtSubitem = new DataTable();
    DataSet dsLOTTFDetails = new DataSet();
    DataSet dsLOTCategoryForFactory = new DataSet();
    DataSet dsProductionOrderNo = new DataSet();
    DataSet dsDMSDrawingNo = new DataSet();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string LOTTFNo = string.Empty;
    int statusID = 0;
    int unitID = 0;
    int runningNo = 0;
    string runningNoTxt = string.Empty;
    int LOTMainItemID = 0;
    int LOTMainSubitemID = 0;
    string tagNumber = string.Empty;
    string drawingNumber = string.Empty;
    int transferStatusId = 0;


    int companyID = 0;
    int revisionNo = 0;
    string categoryID = string.Empty;
    string category = string.Empty;
    string LOTMainSubitemIDs = string.Empty;
    string LOTTFSubitemIDs = string.Empty;
    string companyName = string.Empty;
    string jobNo = string.Empty;
    string customerName = string.Empty;
    string custCode = string.Empty;
    string TFNo = string.Empty;
    string poNo = string.Empty;
    string LOTDate = string.Empty;
    string itemName = string.Empty;
    string LOTMainItem = string.Empty;
    string impNotes = string.Empty;
    int createdByID = 0;
    string remarks = string.Empty;
    int amendedByID = 0;
    int amendmentCount = 0;
    DataTable dtProdMngr = new DataTable();
    DataTable dtTempAttachments = new DataTable();
    DataTable dtAttachments = new DataTable();
    DataTable dtSubitemToAdd = new DataTable();

    Byte[] attachment1FileBytes = null;
    Byte[] attachment2FileBytes = null;
    Byte[] attachment3FileBytes = null;
    Byte[] attachment4FileBytes = null;

    string attachment1File = string.Empty;
    string attachment2File = string.Empty;
    string attachment3File = string.Empty;
    string attachment4File = string.Empty;

    int LOTTFSubitemID = 0;
    string productionOrderNo = string.Empty;
    string expectedCompletionDate = string.Empty;
    string subitemDesc = string.Empty;
    string tagNo = string.Empty;
    string LOTMainSubItem = string.Empty;
    int LOTMainSubItemID = 0;
    string LOTMainSubItemIDs = string.Empty;
    string drgNo = string.Empty;
    int quantity = 0;

    string productionOrderDate = string.Empty;
    string productCode = string.Empty;
    string productDesc = string.Empty;
    string UOM = string.Empty;
    string isPartOfProductionStatus = string.Empty;
    int isPartOfProductionStatusID = 0;
    int isRevised = 0;
    int PEID = 0;
    int PMID = 0;
    int approvedByID = 0;
    int amendedApprovedByID = 0;
    int prodMngrID = 0;
    int acceptedByID = 0;
    string subitemAttachment1File = string.Empty;
    string subitemAttachment2File = string.Empty;
    string subitemAttachment3File = string.Empty;
    string subitemAttachment4File = string.Empty;

    Byte[] subitemAttachment1FileBytes = null;
    Byte[] subitemAttachment2FileBytes = null;
    Byte[] subitemAttachment3FileBytes = null;
    Byte[] subitemAttachment4FileBytes = null;

    #endregion




    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanelReviseList();
            HidePanelRevision();

            if (!IsPostBack)
            {
                ddlTransferStatus.SelectedIndex = 2;
                ddlTransferStatus.Enabled = false;

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tfno"])))
                {
                    txtTFNo.Text = Convert.ToString(Request.QueryString["tfno"]);
                    GetLOTList();
                }


                hdUpdationFlag.Value = "0";
                hdConfirmValue.Value = "0";

                Session["dtProdDetail"] = null;
                Session["dsLOTList"] = null;
                Session["dsSubitemsSI"] = null;

                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;

                BindCompany();
                BindStatus();
                BindLOTMainItems();





                Session["dsApprovers"] = null;
                Session["dtProdMngr"] = null;
                Session["dtProdMngrcc"] = null;

                AddTempSubitemTable();
                //BindLOTMainItemsToEdit();
                //BindLOTCategoryForFactoryToEdit();

                GetLOTList();
            }
        }
        else
        {
            Session["dsTravelStatementDetails"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }


    #region POPULATE REVISION LIST START[=================txtExpectedCompletionDateToEdit=]


    #region EVENTS START[===================]

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetLOTList();
    }

    protected void btnAddNewLOT_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/LOTTransmittalFactory.aspx");
    }

    protected void gvLOTTFList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                createdByID = 0;

                hdRemovedSubitemIDs.Value = string.Empty;

                hdUpdationFlag.Value = "0";
                int LOTTFID = 0;
                int rowindex = 0;

                if (Session["dsLOTList"] != null)
                    dsLOTList = (DataSet)Session["dsLOTList"];

                if (Convert.ToString(e.CommandArgument) == "REVISE" ||
                    Convert.ToString(e.CommandArgument) == "ViewSubitemDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblLOTTFID = gvLOTTFList.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblTFNo = gvLOTTFList.Rows[rowindex].FindControl("lblTFNo") as Label;
                Label lblUnitID = gvLOTTFList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblJOBNo = gvLOTTFList.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblLOTDate = gvLOTTFList.Rows[rowindex].FindControl("lblLOTDate") as Label;
                Label lblCustomerCode = gvLOTTFList.Rows[rowindex].FindControl("lblCustomerCode") as Label;
                Label lblCustomerName = gvLOTTFList.Rows[rowindex].FindControl("lblCustomerName") as Label;
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


                LOTTFID = Convert.ToInt32(lblLOTTFID.Text);

                txtTFNoSI.Text = lblTFNo.Text;
                txtJOBNoSI.Text = lblJOBNo.Text;


                ViewState["LOTTFID"] = LOTTFID;
                ViewState["JOB_NO"] = Convert.ToString(lblJOBNo.Text);
                ViewState["TF_NO"] = Convert.ToString(lblTFNo.Text);
                ViewState["UNIT_ID"] = Convert.ToString(lblUnitID.Text);


                ViewState["CREATED_BY_ID"] = Convert.ToInt32(lblcreatedByID.Text);

                if (!string.IsNullOrEmpty(lblJobPEID.Text))
                    ViewState["PE_ID"] = Convert.ToInt32(lblJobPEID.Text);

                if (!string.IsNullOrEmpty(lblJobPMID.Text))
                    ViewState["PM_ID"] = Convert.ToInt32(lblJobPMID.Text);



                if (Convert.ToString(e.CommandArgument) == "REVISE")
                {
                    this.mpeReviseLOT.Show();

                    //if (currentStatusID == Convert.ToInt32(EnumStatus.New))
                    //{
                    //EnableControls();

                    //ViewState["ACT_ID"] = Convert.ToInt32(EnumActs.New);
                    //ViewState["NEW_STATUS_ID"] = Convert.ToInt32(EnumStatus.New);

                    //btnUpdateLOTStatus.Text = "Update LOT";

                    //BindLOTTFDetails(LOTTFID);
                    //this.ModalPopupExtender1.Show();
                    //pnlAttachDrawings.Visible = true;
                    //}



                    Reset();

                    BindLOTTFDetailsToEdit(LOTTFID);
                    //GetTFNo();
                    this.mpeReviseLOT.Show();
                }


                //if (Convert.ToString(e.CommandArgument) == "REVISE")
                //{
                //    tblRemarks.Visible = true;
                //    txtRemarksSI.Text = string.Empty;

                //    //BindLOTSubitemDetails(LOTTFID);
                //    BindLOTSubitemDetailsByID(LOTTFID, Convert.ToInt32(ViewState["PE_ID"]), Convert.ToInt32(ViewState["PM_ID"]));
                //    mpeSubitemDetail.Show();
                //}

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                    ViewDrawingFiles(LOTTFID, "DRAWING1", Convert.ToString(lblAttachment1.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                    ViewDrawingFiles(LOTTFID, "DRAWING2", Convert.ToString(lblAttachment2.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                    ViewDrawingFiles(LOTTFID, "DRAWING3", Convert.ToString(lblAttachment3.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                    ViewDrawingFiles(LOTTFID, "DRAWING4", Convert.ToString(lblAttachment4.Text).Trim(), 0);




                else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    int count = 0;

                    foreach (DataRow dr0 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        int peId = 0;
                        int pmId = 0;

                        if (dr0["PE_ID"] != DBNull.Value)
                            peId = Convert.ToInt32(dr0["PE_ID"]);

                        if (dr0["PE_ID"] != DBNull.Value)
                            peId = Convert.ToInt32(dr0["PM_ID"]);


                        if (peId == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                            pmId == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                            Convert.ToInt32(dr0["CREATED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                        {
                            foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                            {
                                if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
                                {
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                                }
                            }
                        }
                        else
                        {
                            //foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                            foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "' AND IS_PART_OF_PRODUCTION_STATUS_REPORT_ID='1'"))
                            {
                                count++;
                                if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
                                {
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                                }
                            }
                        }
                    }

                    if (count == 0)
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[1].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                        {
                            if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
                            {
                                LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    ModalPopupExtender4.Show();
                    iframeViewTravelStatementInPDF.Attributes.Add("src", "LOTTransmittalFactoryInPDF.aspx?LOTTFID=" + LOTTFID + "&LOTTFSubitemIDs=" + LOTTFSubitemIDs + "&pdfType=" + (int)LOTAllStatusAndTypes.EnumPDFType.List);



                    //ModalPopupExtender4.Show();
                    //iframeViewTravelStatementInPDF.Attributes.Add("src", "LOTTransmittalFactoryInPDF.aspx?LOTTFID=" + LOTTFID);
                }

                if (Convert.ToString(e.CommandArgument) == "ViewSubitemDETAIL")
                {
                    if (Session["dsLOTList"] != null)
                        dsLOTList = (DataSet)Session["dsLOTList"];

                    LOTMainSubitemIDs = string.Empty;
                    LOTTFSubitemIDs = string.Empty;

                    if (dsLOTList.Tables.Count > 0 && dsLOTList.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsLOTList.Tables[1].Select("LOT_TF_ID='" + LOTTFID + "'"))
                        {
                            string LOTMainSubitemID = Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"]);
                            string LOTTFSubitemID = Convert.ToString(dr["LOT_TF_SUBITEM_ID"]);

                            if (!LOTMainSubitemIDs.Contains("," + LOTMainSubitemID + ","))
                            {
                                LOTMainSubitemIDs += LOTMainSubitemID + ",";
                            }

                            if (!LOTTFSubitemIDs.Contains("," + LOTTFSubitemID + ","))
                            {
                                LOTTFSubitemIDs += LOTTFSubitemID + ",";
                            }

                        }
                    }
                    else
                    {
                        LOTMainSubitemIDs = string.Empty;
                        LOTTFSubitemIDs = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                        LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    ViewState["LOTMainSubitemIDs"] = LOTMainSubitemIDs;

                    //tblRemarks.Visible = false;
                    BindLOTSubitemDetails(LOTTFID, LOTMainSubitemIDs, LOTTFSubitemIDs);
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
            //ExceptionMessage
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    protected void gvLOTTFList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnRevise = (ImageButton)e.Row.FindControl("imgBtnRevise");
                Label lblIsTransferred = (Label)e.Row.FindControl("lblIsTransferred");
                //Label lblIsHoldByStore = (Label)e.Row.FindControl("lblIsHoldByStore");

                imgBtnRevise.Visible = false;

                if (Convert.ToInt32(lblIsTransferred.Text) > 0)//&& Convert.ToInt32(lblIsHoldByStore.Text) > 0
                    imgBtnRevise.Visible = true;

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
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

            }

        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }


    // SUBITEM DETAILS
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
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
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


                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "DRAWING1", Convert.ToString(lblAttachment1.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "DRAWING2", Convert.ToString(lblAttachment2.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "DRAWING3", Convert.ToString(lblAttachment3.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                {
                    ViewDrawingFiles(Convert.ToInt32(lblLOTTFID.Text), "DRAWING4", Convert.ToString(lblAttachment4.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
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

                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");


                statusID = Convert.ToInt32(lblStatusID.Text);
                imgBtnStatus.Enabled = false;



                if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
                {
                    imgBtnStatus.ImageUrl = "~/Images/NEWICONS/New03.png";
                    imgBtnStatus.ToolTip = "New";
                }

                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                {
                    imgBtnStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                    imgBtnStatus.ToolTip = "Approved";
                }

                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
                {
                    imgBtnStatus.ImageUrl = "~/Images/LOT/accepted5.png";
                    imgBtnStatus.ToolTip = "Accepted";
                }



                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.FitupInspection))
                {
                    imgBtnStatus.ImageUrl = "~/Images/LOT/insp5.jpg";
                    imgBtnStatus.ToolTip = "Internal Inspection";
                }

                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemAccepted))
                {
                    imgBtnStatus.ImageUrl = "~/Images/LOT/qa5.png";
                    imgBtnStatus.ToolTip = "Accepted by Quality for internal inspection";
                }

                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.QAItemNotAccepted))
                {
                    imgBtnStatus.ImageUrl = "~/Images/Cancelled01.png";
                    imgBtnStatus.ToolTip = "Not accepted by Quality for internal inspection";
                }


                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
                {
                    imgBtnStatus.ImageUrl = "~/Images/Closed02.png";
                    imgBtnStatus.ToolTip = "Completed";
                }

                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
                {
                    imgBtnStatus.ImageUrl = "~/Images/NEWICONS/Amendment01.png";
                    imgBtnStatus.ToolTip = "Amendment";
                }

                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                {
                    imgBtnStatus.ImageUrl = "~/Images/NEWICONS/Amended01.png";
                    imgBtnStatus.ToolTip = "Amended";
                }

                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
                {
                    imgBtnStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                    imgBtnStatus.ToolTip = "Amended-Approved";
                }

                else if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
                {
                    imgBtnStatus.ImageUrl = "~/Images/LOT/accepted5.png";
                    imgBtnStatus.ToolTip = "Amended-Accepted";
                }



                #endregion

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }


    #endregion EVENTS END[==================]



    #region METHODS START[==================]

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
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    private void BindStatus()
    {
        try
        {

            dsLOTStatus = objProject.GetLOTTFStatusFoRevision();
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
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    private void BindLOTMainItems()
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
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    private void BindLOTMainSubItems(int LOTMainItemID)
    {
        try
        {
            dsLOTMainSubItems = objProject.GetLotMainSubItems(LOTMainItemID, Convert.ToInt32(ddlCompany.SelectedValue));//Convert.ToInt32(hdCompanyToEdit.Value)
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
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }





    // SUBITEM DETAILS
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
            tagNumber = string.Empty;
            drawingNumber = string.Empty;
            transferStatusId = 0;

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

            if (!string.IsNullOrEmpty(txtTagNumber.Text))
                tagNumber = txtTagNumber.Text;

            if (!string.IsNullOrEmpty(txtDrawingNumber.Text))
                drawingNumber = txtDrawingNumber.Text;

            if (ddlTransferStatus.SelectedIndex > 0)
                transferStatusId = Convert.ToInt32(ddlTransferStatus.SelectedValue);

            dsLOTList = objProject.GetLOTTFListForTransferOfLOT(startDate, endDate, LOTTFNo, statusID, unitID, jobNo
                                                              , customerName, LOTMainItemID, LOTMainSubitemID, tagNumber
                                                              , drawingNumber
                                                              , transferStatusId);

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
            ExceptionMessageReviseList(ex.ToString());
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

                //extn = fileName.Split('.').Last();
                //if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                //{
                //    //imgFile.ImageUrl = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    //mpeShowImageFile.Show();

                //    string url = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                //    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                //}
                //else if (extn == "pdf" || extn == "PDF")
                //{
                //    //iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "");
                //    //mpeShowPDFFile.Show();

                //    string url = "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

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
            }
            else
            {
                ExceptionMessageReviseList("File Doesn't exist!");
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
                ExceptionMessageRevision("The process of downloading is too longer, please try again...!!!");
                mpeAddSubitems.Show();
                return;
            }
            else
            {
                throw;
            }
        }
    }

    private void SuccessMessageReviseList(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessageReviseList(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanelReviseList()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion METHODS END[=================]


    #endregion POPULATE REVISION LIST END[=================]





    #region REVISIE LOT START[==============================]


    #region EVENTS START[===================]



    //protected void ddlLOTMainItemsToEdit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    mpeAddSubitems.Show();
    //    mpeReviseLOT.Show();
    //    //BindLOTMainSubItemsToEdit();
    //    //CheckMultipleSubitems();
    //}



    // GET TF NO            
    //protected void btnGetTFno_Click(object sender, EventArgs e)
    //{
    //    GetTFNo();
    //    mpeReviseLOT.Show();
    //}




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
        mpeReviseLOT.Show();
    }

    protected void btnSearchProductionOrderNo_Click(object sender, EventArgs e)
    {
        mpeProductonOrderNoDetail.Show();

        if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0 && ViewState["UNIT_ID"] != null)
            GetProductionOrderNoDetailToEdit(Convert.ToInt32(ViewState["UNIT_ID"]));

        mpeAddSubitems.Show();
        mpeReviseLOT.Show();
    }

    protected void gvProductonOrderNoDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                txtUOMToEdit.Text = string.Empty;
                txtProductDescToEdit.Text = string.Empty;
                txtExpectedCompletionDateToEdit.Text = string.Empty;
                txtQuantityToEdit.Text = string.Empty;

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
                mpeReviseLOT.Show();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
            return;
        }
    }

    protected void ddlProductCodeToEdit_SelectedIndexChanged(object sender, EventArgs e)
    {

        HideAddSubitemPanel();
        //btnAddUpdateSubitemToList.Enabled = false;
        //txtExpectedCompletionDateToEdit.BackColor = System.Drawing.Color.LightYellow;

        mpeAddSubitems.Show();
        mpeReviseLOT.Show();

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
        }
    }



    // DMS DRAWING NO DETAILS
    protected void btnGetDMSDrawingNo_Click(object sender, EventArgs e)
    {
        txtJOBNoSearchDMS.Text = txtJOBNoToEdit.Text;
        mpeDMSDrawingNoList.Show();
        mpeAddSubitems.Show();
        mpeReviseLOT.Show();

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
                //txtDrgNoToEdit.Text = string.Empty;

                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblDrawingNo = gvDMSDrawingNoList.Rows[rowindex].FindControl("lblDrawingNo") as Label;

                //txtDrgNoToEdit.Text = lblDrawingNo.Text.Trim().ToUpper();
                mpeAddSubitems.Show();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
            return;
        }
    }



    //ADD UPDATE SUBITEMS
    //protected void btnAddSubitem_Click(object sender, EventArgs e)
    //{
    //    imgBtnUndoSiDrawingToEdit1.Visible = false;
    //    imgBtnUndoSiDrawingToEdit2.Visible = false;
    //    imgBtnUndoSiDrawingToEdit3.Visible = false;
    //    imgBtnUndoSiDrawingToEdit4.Visible = false;

    //    pnlViewSiDrawingToEdit1.Visible = false;
    //    pnlViewSiDrawingToEdit2.Visible = false;
    //    pnlViewSiDrawingToEdit3.Visible = false;
    //    pnlViewSiDrawingToEdit4.Visible = false;

    //    pnlUploadSiDrawingToEdit1.Visible = true;
    //    pnlUploadSiDrawingToEdit2.Visible = true;
    //    pnlUploadSiDrawingToEdit3.Visible = true;
    //    pnlUploadSiDrawingToEdit4.Visible = true;

    //    //CheckMultipleSubitems();    


    //    //Added on 2022-05-30
    //    HideAddSubitemPanel();
    //    //txtExpectedCompletionDateToEdit.BackColor = System.Drawing.Color.LightYellow;
    //    //btnAddUpdateSubitemToList.Enabled = false;
    //    //-----------






    //    lgSubitemUpdation.InnerText = "Add Subitem";
    //    btnAddUpdateSubitemToList.Text = "Add Subitem";
    //    hdAddSubitemFlag.Value = "1";

    //    hdNewSubitemUpdationFlag.Value = "0";
    //    hdRevisedSubitemUpdationFlag.Value = "0";

    //    hdSubitemReviseFlag.Value = "0";

    //    //txtRevNoToEdit.Text = "0";
    //    //txtRevNoToEdit.Enabled = false;
    //    ddlRevNoToEdit.SelectedIndex = 0;
    //    //txtQuantityToEdit.Enabled = true;
    //    //EnableControls();
    //    ResetSubitems();
    //    mpeAddSubitems.Show();
    //    mpeReviseLOT.Show();
    //}

    protected void btnAddUpdateSubitemToList_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdNewSubitemUpdationFlag.Value) == 0 &&
            Convert.ToInt32(hdRevisedSubitemUpdationFlag.Value) == 0 &&
            Convert.ToInt32(hdSubitemReviseFlag.Value) == 0)
            AddSubitems();
        else
            UpdateSubitems();

        mpeReviseLOT.Show();
    }

    protected void gvSubItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                //sUID = 0;
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "REVISE" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                hdAddSubitemFlag.Value = "0";

                Label lblSrNo = gvSubItem.Rows[rowindex].FindControl("lblSrNo") as Label;
                Label lblLOTTFMainSubitemID = gvSubItem.Rows[rowindex].FindControl("lblLOTTFMainSubitemID") as Label;
                Label lblStatusID = gvSubItem.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblNextStatusID = gvSubItem.Rows[rowindex].FindControl("lblNextStatusID") as Label;
                Label lblLOTTFID = gvSubItem.Rows[rowindex].FindControl("lblLOTTFID") as Label;

                Label lblProductionNumber = gvSubItem.Rows[rowindex].FindControl("lblProductionNumber") as Label;
                Label lblProductionOrderDate = gvSubItem.Rows[rowindex].FindControl("lblProductionOrderDate") as Label;
                Label lblExpectedCompletionDate = gvSubItem.Rows[rowindex].FindControl("lblExpectedCompletionDate") as Label;
                Label lblProductCode = gvSubItem.Rows[rowindex].FindControl("lblProductCode") as Label;
                Label lblProductDesc = gvSubItem.Rows[rowindex].FindControl("lblProductDesc") as Label;
                Label lblUOM = gvSubItem.Rows[rowindex].FindControl("lblUOM") as Label;
                Label lblIsPartOfProductionStatusReport = gvSubItem.Rows[rowindex].FindControl("lblIsPartOfProductionStatusReport") as Label;

                TextBox txtTagNoInList = gvSubItem.Rows[rowindex].FindControl("txtTagNoInList") as TextBox;
                Label lblDescription = gvSubItem.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblLOTMainItemID = gvSubItem.Rows[rowindex].FindControl("lblLOTMainItemID") as Label;
                Label lblLOTMainSubitemID = gvSubItem.Rows[rowindex].FindControl("lblLOTMainSubitemID") as Label;
                Label lblLOTFor = gvSubItem.Rows[rowindex].FindControl("lblLOTFor") as Label;
                Label lblDrgOrDOCNo = gvSubItem.Rows[rowindex].FindControl("lblDrgOrDOCNo") as Label;
                Label lblOldRevNo = gvSubItem.Rows[rowindex].FindControl("lblOldRevNo") as Label;
                Label lblRevNo = gvSubItem.Rows[rowindex].FindControl("lblRevNo") as Label;
                Label lblCategory = gvSubItem.Rows[rowindex].FindControl("lblCategory") as Label;
                Label lblCategoryID = gvSubItem.Rows[rowindex].FindControl("lblCategoryID") as Label;
                Label lblQuantity = gvSubItem.Rows[rowindex].FindControl("lblQuantity") as Label;
                Label lblIsRevised = gvSubItem.Rows[rowindex].FindControl("lblIsRevised") as Label;

                ViewState["lblLOTTFMainSubitemID"] = Convert.ToInt32(lblLOTTFMainSubitemID.Text);
                hdOldRevNo.Value = Convert.ToString(lblOldRevNo.Text);

                ImageButton imgBtnRevise = gvSubItem.Rows[rowindex].FindControl("imgBtnRevise") as ImageButton;

                //imgBtnRevise.Visible = false;

                //foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
                //{
                //    item.Selected = false;
                //}


                //txtQuantityToEdit.Enabled = false;
                //txtRevNoToEdit.Enabled = false;                

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "REVISE")
                {

                    //imgBtnUndoSiDrawingToEdit1.Visible = true;
                    //imgBtnUndoSiDrawingToEdit2.Visible = true;
                    //imgBtnUndoSiDrawingToEdit3.Visible = true;
                    //imgBtnUndoSiDrawingToEdit4.Visible = true;

                    //pnlViewSiDrawingToEdit1.Visible = true;
                    //pnlViewSiDrawingToEdit2.Visible = true;
                    //pnlViewSiDrawingToEdit3.Visible = true;
                    //pnlViewSiDrawingToEdit4.Visible = true;

                    //pnlUploadSiDrawingToEdit1.Visible = false;
                    //pnlUploadSiDrawingToEdit2.Visible = false;
                    //pnlUploadSiDrawingToEdit3.Visible = false;
                    //pnlUploadSiDrawingToEdit4.Visible = false;


                    if (!string.IsNullOrEmpty(lblSrNo.Text))
                        hdSRNo.Value = lblSrNo.Text;
                    else hdSRNo.Value = "0";

                    //if (!string.IsNullOrEmpty(lblProductionNumber.Text))
                    //    txtProductionOrderNoToEdit.Text = lblProductionNumber.Text;
                    //else txtProductionOrderNoToEdit.Text = string.Empty;

                    //if (!string.IsNullOrEmpty(lblExpectedCompletionDate.Text))
                    //    txtExpectedCompletionDateToEdit.Text = lblExpectedCompletionDate.Text;
                    //else txtExpectedCompletionDateToEdit.Text = string.Empty;


                    //if (!string.IsNullOrEmpty(lblProductionNumber.Text))
                    //    txtProductionOrderNoToEdit.Text = lblProductionNumber.Text;
                    //else txtProductionOrderNoToEdit.Text = string.Empty;

                    //if (!string.IsNullOrEmpty(lblProductionOrderDate.Text))
                    //    txtProductionOrderDateToEdit.Text = lblProductionOrderDate.Text;
                    //else txtProductionOrderDateToEdit.Text = string.Empty;
                    //
                    //if (!string.IsNullOrEmpty(lblExpectedCompletionDate.Text))
                    //{
                    //    txtExpectedCompletionDateToEdit.Text = lblExpectedCompletionDate.Text;
                    //    //CheckForExpectedCompletionDate(txtExpectedCompletionDateToEdit.Text);
                    //}
                    //else txtExpectedCompletionDateToEdit.Text = string.Empty;

                    //BindProductDetail(lblProductionNumber.Text);
                    //if (!string.IsNullOrEmpty(lblProductCode.Text))
                    //    ddlProductCodeToEdit.SelectedValue = lblProductCode.Text;
                    //else ddlProductCodeToEdit.SelectedIndex = 0;

                    //if (!string.IsNullOrEmpty(lblProductDesc.Text))
                    //    txtProductDescToEdit.Text = lblProductDesc.Text;
                    //else txtProductDescToEdit.Text = string.Empty;

                    //if (!string.IsNullOrEmpty(lblUOM.Text))
                    //    txtUOMToEdit.Text = lblUOM.Text;
                    //else txtUOMToEdit.Text = string.Empty;

                    //if (Convert.ToInt32(lblIsPartOfProductionStatusReport.Text) > 0)
                    //    chkIsPartOfProductionOrMainDrawingToEdit.Checked = true;
                    //else chkIsPartOfProductionOrMainDrawingToEdit.Checked = false;



                    //ddlLOTMainItemsToEdit.SelectedValue = Convert.ToString(lblLOTMainItemID.Text);

                    //BindLOTMainSubItemsToEdit();
                    //ddlLOTMainSubitemsToEdit.SelectedValue = Convert.ToString(lblLOTMainSubitemID.Text);

                    //if (!string.IsNullOrEmpty(lblDrgOrDOCNo.Text))
                    //    txtDrgNoToEdit.Text = lblDrgOrDOCNo.Text;
                    //else txtDrgNoToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblDescription.Text))
                        txtDescriptionToEdit.Text = lblDescription.Text;
                    else txtDescriptionToEdit.Text = string.Empty;

                    //if (!string.IsNullOrEmpty(lblRevNo.Text))
                    //{
                    //    if (Convert.ToInt32(lblIsRevised.Text) == 0)
                    //        txtRevNoToEdit.Text = Convert.ToString(Convert.ToInt32(lblRevNo.Text) + 1);
                    //    else
                    //        txtRevNoToEdit.Text = Convert.ToString(lblRevNo.Text);
                    //}
                    //else txtRevNoToEdit.Text = "0";


                    //if (!string.IsNullOrEmpty(lblRevNo.Text))
                    //{
                    //    if (Convert.ToInt32(lblIsRevised.Text) == 0)
                    //        ddlRevNoToEdit.SelectedValue = Convert.ToString(Convert.ToInt32(lblRevNo.Text) + 1);
                    //    else
                    //        ddlRevNoToEdit.SelectedValue = Convert.ToString(lblRevNo.Text);
                    //}
                    //else ddlRevNoToEdit.SelectedValue = "0";


                    //if (!string.IsNullOrEmpty(lblRevNo.Text))
                    //{
                    //    ddlRevNoToEdit.SelectedValue = Convert.ToString(lblRevNo.Text);
                    //}
                    //else
                    //{
                    //    ddlRevNoToEdit.SelectedValue = "0";
                    //}


                    //string[] str = lblCategoryID.Text.Split(',');
                    //foreach (string item in str)
                    //{
                    //    chkLstCategoryToEdit.Items[Convert.ToInt32(item) - 1].Selected = true;
                    //}

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



                    #region DRAWINGS

                    string attachment1Extn = string.Empty;
                    string attachment2Extn = string.Empty;
                    string attachment3Extn = string.Empty;
                    string attachment4Extn = string.Empty;

                    Label lblAttachment1 = gvSubItem.Rows[rowindex].FindControl("lblAttachment1") as Label;
                    Label lblAttachment2 = gvSubItem.Rows[rowindex].FindControl("lblAttachment2") as Label;
                    Label lblAttachment3 = gvSubItem.Rows[rowindex].FindControl("lblAttachment3") as Label;
                    Label lblAttachment4 = gvSubItem.Rows[rowindex].FindControl("lblAttachment4") as Label;

                    if (!string.IsNullOrEmpty(lblAttachment1.Text))
                    {
                        hdSiDrawingToEdit1.Value = "1";
                        //hdUploadSiDrawingToEdit1.Value = "0";

                        //pnlViewSiDrawingToEdit1.Visible = true;
                        //pnlUploadSiDrawingToEdit1.Visible = false;

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
                        //hdUploadSiDrawingToEdit1.Value = "1";

                        //pnlViewSiDrawingToEdit1.Visible = false;
                        //pnlUploadSiDrawingToEdit1.Visible = true;

                        imgBtnViewSiDrawingToEdit1.Visible = false;
                        //imgBtnUndoSiDrawingToEdit1.Visible = false;
                    }


                    if (!string.IsNullOrEmpty(lblAttachment2.Text))
                    {
                        hdSiDrawingToEdit2.Value = "1";
                        //hdUploadSiDrawingToEdit2.Value = "0";

                        //pnlViewSiDrawingToEdit2.Visible = true;
                        //pnlUploadSiDrawingToEdit2.Visible = false;

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
                    }
                    else
                    {
                        hdSiDrawingToEdit2.Value = "0";
                        //hdUploadSiDrawingToEdit2.Value = "1";

                        //pnlViewSiDrawingToEdit2.Visible = false;
                        //pnlUploadSiDrawingToEdit2.Visible = true;

                        imgBtnViewSiDrawingToEdit2.Visible = false;
                        //imgBtnUndoSiDrawingToEdit2.Visible = false;
                    }



                    if (!string.IsNullOrEmpty(lblAttachment3.Text))
                    {
                        hdSiDrawingToEdit3.Value = "1";
                        //hdUploadSiDrawingToEdit3.Value = "0";

                        //pnlViewSiDrawingToEdit3.Visible = true;
                        //pnlUploadSiDrawingToEdit3.Visible = false;

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
                        //hdUploadSiDrawingToEdit3.Value = "1";

                        //pnlViewSiDrawingToEdit3.Visible = false;
                        //pnlUploadSiDrawingToEdit3.Visible = true;

                        imgBtnViewSiDrawingToEdit3.Visible = false;
                        //imgBtnUndoSiDrawingToEdit3.Visible = false;
                    }




                    if (!string.IsNullOrEmpty(lblAttachment4.Text))
                    {
                        hdSiDrawingToEdit4.Value = "1";
                        //hdUploadSiDrawingToEdit4.Value = "0";

                        //pnlViewSiDrawingToEdit4.Visible = true;
                        //pnlUploadSiDrawingToEdit4.Visible = false;

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
                        //hdUploadSiDrawingToEdit4.Value = "1";

                        //pnlViewSiDrawingToEdit4.Visible = false;
                        //pnlUploadSiDrawingToEdit4.Visible = true;

                        imgBtnViewSiDrawingToEdit4.Visible = false;
                        //imgBtnUndoSiDrawingToEdit4.Visible = false;
                    }


                    #endregion


                    if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                    {
                        hdNewSubitemUpdationFlag.Value = "0";
                        hdRevisedSubitemUpdationFlag.Value = "0";

                        if (Convert.ToInt32(lblLOTTFMainSubitemID.Text) == 0)
                        {
                            hdNewSubitemUpdationFlag.Value = "1";
                            hdRevisedSubitemUpdationFlag.Value = "0";
                        }
                        else
                        {
                            hdNewSubitemUpdationFlag.Value = "0";
                            hdRevisedSubitemUpdationFlag.Value = "1";
                        }
                        mpeReviseLOT.Show();

                        lgSubitemUpdation.InnerText = "Link Production Order";
                        btnAddUpdateSubitemToList.Text = "Link Production Order";

                        //txtQuantityToEdit.Enabled = false;

                        //if (Convert.ToInt32(lblIsRevised.Text) == 0)
                        //{
                        //    txtQuantityToEdit.Enabled = true;
                        //}
                        //else
                        //{
                        //    lgSubitemUpdation.InnerText = "Update Revised Subitem";
                        //    btnAddUpdateSubitemToList.Text = "Update Revised Subitem";
                        //}

                        if (Convert.ToInt32(lblIsRevised.Text) > 0)
                        {
                            lgSubitemUpdation.InnerText = "Update Revised Subitem";
                            btnAddUpdateSubitemToList.Text = "Update Revised Subitem";
                        }
                    }


                    if (Convert.ToString(e.CommandArgument) == "REVISE")
                    {
                        mpeReviseLOT.Show();

                        lgSubitemUpdation.InnerText = "Link Production Order";
                        btnAddUpdateSubitemToList.Text = "Link Production Order";
                        hdSubitemReviseFlag.Value = "1";
                        //txtRevNoToEdit.Enabled = true;                        
                    }

                    mpeAddSubitems.Show();
                }



                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    RemoveSubitems(Convert.ToInt32(lblSrNo.Text));
                    mpeReviseLOT.Show();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
            return;
        }
    }

    protected void gvSubItem_RowDataBound(object sender, GridViewRowEventArgs e)
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
                CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;

                ImageButton imgBtnProperties = e.Row.FindControl("imgBtnProperties") as ImageButton;
                ImageButton imgBtnRevise = e.Row.FindControl("imgBtnRevise") as ImageButton;
                ImageButton imgBtnRemove = e.Row.FindControl("imgBtnRemove") as ImageButton;


                Label lblLOTTFMainSubitemID = e.Row.FindControl("lblLOTTFMainSubitemID") as Label;
                Label lblIsRevised = e.Row.FindControl("lblIsRevised") as Label;


                Label lblIsPartOfProductionStatusReport = (Label)e.Row.FindControl("lblIsPartOfProductionStatusReport");
                CheckBox chkIsPartOfProductStatusReport = (CheckBox)e.Row.FindControl("chkIsPartOfProductStatusReport");

                if (Convert.ToInt32(lblIsPartOfProductionStatusReport.Text) > 0)
                    chkIsPartOfProductStatusReport.Checked = true;
                else chkIsPartOfProductStatusReport.Checked = false;

                //chkSelect.Visible = false;

                imgBtnRevise.Visible = false;
                //imgBtnProperties.Visible = false;

                if (Convert.ToInt32(lblLOTTFMainSubitemID.Text) > 0)
                {
                    imgBtnRevise.Visible = true;

                    if (Convert.ToInt32(lblIsRevised.Text) > 0)
                    {
                        imgBtnRevise.Visible = false;
                        //imgBtnProperties.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblLOTTFMainSubitemID.Text) == 0)
                {
                    //imgBtnProperties.Visible = true;
                }

                //if (Convert.ToInt32(lblLOTTFMainSubitemID.Text) == 0)
                //{
                //    imgBtnRemove.Visible = true;
                //}

                //Label lblSUID = e.Row.FindControl("lblSUID") as Label;

                //if (Convert.ToInt32(lblSUID.Text) > 0)
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                //    }
                //}

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
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
        mpeReviseLOT.Show();
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
            ExceptionMessageRevision(ex.ToString());
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
            ExceptionMessageRevision(ex.ToString());
            return;
        }
    }




    //protected void imgBtnRemoveSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    //{
    //    hdSiDrawingToEdit1.Value = "0";
    //    hdUploadSiDrawingToEdit1.Value = "1";

    //    pnlViewSiDrawingToEdit1.Visible = false;
    //    pnlUploadSiDrawingToEdit1.Visible = true;

    //    mpeReviseLOT.Show();
    //    mpeAddSubitems.Show();
    //}
    //protected void imgBtnUndoSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    //{
    //    hdSiDrawingToEdit1.Value = "1";
    //    hdUploadSiDrawingToEdit1.Value = "0";

    //    pnlViewSiDrawingToEdit1.Visible = true;
    //    pnlUploadSiDrawingToEdit1.Visible = false;

    //    mpeReviseLOT.Show();
    //    mpeAddSubitems.Show();
    //}

    //protected void imgBtnRemoveSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    //{
    //    hdSiDrawingToEdit2.Value = "0";
    //    hdUploadSiDrawingToEdit2.Value = "1";

    //    pnlViewSiDrawingToEdit2.Visible = false;
    //    pnlUploadSiDrawingToEdit2.Visible = true;

    //    mpeReviseLOT.Show();
    //    mpeAddSubitems.Show();
    //}
    //protected void imgBtnUndoSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    //{
    //    hdSiDrawingToEdit2.Value = "1";
    //    hdUploadSiDrawingToEdit2.Value = "0";

    //    pnlViewSiDrawingToEdit2.Visible = true;
    //    pnlUploadSiDrawingToEdit2.Visible = false;

    //    mpeReviseLOT.Show();
    //    mpeAddSubitems.Show();
    //}

    //protected void imgBtnRemoveSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    //{
    //    hdSiDrawingToEdit3.Value = "0";
    //    hdUploadSiDrawingToEdit3.Value = "1";

    //    pnlViewSiDrawingToEdit3.Visible = false;
    //    pnlUploadSiDrawingToEdit3.Visible = true;

    //    mpeReviseLOT.Show();
    //    mpeAddSubitems.Show();
    //}
    //protected void imgBtnUndoSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    //{
    //    hdSiDrawingToEdit3.Value = "1";
    //    hdUploadSiDrawingToEdit3.Value = "0";

    //    pnlViewSiDrawingToEdit3.Visible = true;
    //    pnlUploadSiDrawingToEdit3.Visible = false;

    //    mpeReviseLOT.Show();
    //    mpeAddSubitems.Show();
    //}

    //protected void imgBtnRemoveSiDrawingToEdit4_Click(object sender, ImageClickEventArgs e)
    //{
    //    hdSiDrawingToEdit4.Value = "0";
    //    hdUploadSiDrawingToEdit4.Value = "1";

    //    pnlViewSiDrawingToEdit4.Visible = false;
    //    pnlUploadSiDrawingToEdit4.Visible = true;

    //    mpeReviseLOT.Show();
    //    mpeAddSubitems.Show();
    //}
    //protected void imgBtnUndoSiDrawingToEdit4_Click(object sender, ImageClickEventArgs e)
    //{
    //    hdSiDrawingToEdit4.Value = "1";
    //    hdUploadSiDrawingToEdit4.Value = "0";

    //    pnlViewSiDrawingToEdit4.Visible = true;
    //    pnlUploadSiDrawingToEdit4.Visible = false;

    //    mpeReviseLOT.Show();
    //    mpeAddSubitems.Show();
    //}



    protected void imgBtnViewSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    {

        ViewDrawingFiles(0, "DRAWING1", txtSiDrawingToEdit1.Text.Trim(), Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]));

        //if (dtTemp != null && Session["dtSubitem"] != null)
        //    dtTemp = (DataTable)Session["dtSubitem"];
        //else
        //    AddTempSubitemTable();

        //if (dtTemp.Rows.Count > 0)
        //{
        //    SubitemTable.dtSubitems = dtTemp;            
        //    //ViewDrawingFiles(Convert.ToInt32(ViewState["SR_NO"]), txtSiDrawingToEdit1.Text.Trim(), "DRAWING1");

        //}

        mpeAddSubitems.Show();
        mpeReviseLOT.Show();
    }

    protected void imgBtnViewSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(0, "DRAWING2", txtSiDrawingToEdit2.Text.Trim(), Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]));

        mpeAddSubitems.Show();
        mpeReviseLOT.Show();

        //if (dtTemp != null && Session["dtSubitem"] != null)
        //    dtTemp = (DataTable)Session["dtSubitem"];
        //else
        //    AddTempSubitemTable();

        //if (dtTemp.Rows.Count > 0)
        //{
        //    SubitemTable.dtSubitems = dtTemp;

        //    ViewDrawingFiles(Convert.ToInt32(ViewState["SR_NO"]), txtSiDrawingToEdit2.Text.Trim(), "DRAWING2");
        //}
    }

    protected void imgBtnViewSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(0, "DRAWING3", txtSiDrawingToEdit3.Text.Trim(), Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]));

        mpeAddSubitems.Show();
        mpeReviseLOT.Show();

        //if (dtTemp != null && Session["dtSubitem"] != null)
        //    dtTemp = (DataTable)Session["dtSubitem"];
        //else
        //    AddTempSubitemTable();

        //if (dtTemp.Rows.Count > 0)
        //{
        //    SubitemTable.dtSubitems = dtTemp;

        //    ViewDrawingFiles(Convert.ToInt32(ViewState["SR_NO"]), txtSiDrawingToEdit3.Text.Trim(), "DRAWING3");
        //}
    }

    protected void imgBtnViewSiDrawingToEdit4_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(0, "DRAWING4", txtSiDrawingToEdit4.Text.Trim(), Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]));

        mpeAddSubitems.Show();
        mpeReviseLOT.Show();

        //if (dtTemp != null && Session["dtSubitem"] != null)
        //    dtTemp = (DataTable)Session["dtSubitem"];
        //else
        //    AddTempSubitemTable();

        //if (dtTemp.Rows.Count > 0)
        //{
        //    SubitemTable.dtSubitems = dtTemp;

        //    ViewDrawingFiles(Convert.ToInt32(ViewState["SR_NO"]), txtSiDrawingToEdit4.Text.Trim(), "DRAWING4");
        //}
    }




    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveLOTLOTTransmittalToFactory();
    }

    protected void btnProjectList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/ProjectList.aspx");
    }


    #endregion EVENTS END[==================]


    #region METHODS START[==================]

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
        dtSubitem.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
        dtSubitem.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
        dtSubitem.Columns.Add("PRODUCT_CODE", typeof(string));
        dtSubitem.Columns.Add("PRODUCT_DESC", typeof(string));
        dtSubitem.Columns.Add("UOM", typeof(string));
        dtSubitem.Columns.Add("IS_PART_OF_PRODUCTION_STATUS_REPORT", typeof(string));
        dtSubitem.Columns.Add("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID", typeof(int));

        dtSubitem.Columns.Add("IS_REVISED", typeof(int));

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
            if (dsProdDetail.Tables.Count > 0)
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

    //private void BindLOTMainItemsToEdit()
    //{
    //    try
    //    {
    //        dsLOTMainItems = objProject.GetLotMainItems();
    //        if (dsLOTMainItems.Tables.Count > 0 && dsLOTMainItems.Tables[0].Rows.Count > 0)
    //        {
    //            ddlLOTMainItemsToEdit.DataSource = dsLOTMainItems.Tables[0];
    //            ddlLOTMainItemsToEdit.DataTextField = "LOT_MAIN_ITEM";
    //            ddlLOTMainItemsToEdit.DataValueField = "LOT_MAIN_ITEM_ID";
    //            ddlLOTMainItemsToEdit.DataBind();
    //            ddlLOTMainItemsToEdit.Items.Insert(0, "Select");
    //            ddlLOTMainItemsToEdit.SelectedValue = "1";

    //            if (ddlLOTMainItemsToEdit.SelectedIndex > 0)
    //            {
    //                BindLOTMainSubItemsToEdit();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessageRevision(ex.ToString());
    //        return;
    //    }
    //}

    //private void BindLOTMainSubItemsToEdit()
    //{
    //    try
    //    {
    //        //Session["dtProdMngr"] = null;
    //        Session["dtProdMngrcc"] = null;

    //        ddlLOTMainSubitemsToEdit.Items.Clear();
    //        ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
    //        ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

    //        if (ddlLOTMainItemsToEdit.SelectedIndex > 0)
    //        {
    //            dsLOTMainSubItems = objProject.GetLotMainSubItems(Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue), Convert.ToInt32(ViewState["UNIT_ID"]));
    //            if (dsLOTMainSubItems.Tables.Count > 0 && dsLOTMainSubItems.Tables[0].Rows.Count > 0)
    //            {
    //                ddlLOTMainSubitemsToEdit.DataSource = dsLOTMainSubItems.Tables[0];

    //                //Session["dtProdMngr"] = dsLOTMainSubItems.Tables[1];
    //                //Session["dtProdMngrcc"] = dsLOTMainSubItems.Tables[2];

    //                ddlLOTMainSubitemsToEdit.DataTextField = "LOT_MAIN_SUBITEM";
    //                ddlLOTMainSubitemsToEdit.DataValueField = "LOT_MAIN_SUBITEM_ID";
    //                ddlLOTMainSubitemsToEdit.DataBind();
    //                ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
    //                ddlLOTMainSubitemsToEdit.SelectedIndex = 0;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessageRevision(ex.ToString());
    //        return;
    //    }
    //}

    //private void BindLOTCategoryForFactoryToEdit()
    //{
    //    try
    //    {
    //        dsLOTCategoryForFactory = objProject.GetFactoryCategory();
    //        if (dsLOTCategoryForFactory.Tables.Count > 0 && dsLOTCategoryForFactory.Tables[0].Rows.Count > 0)
    //        {
    //            chkLstCategoryToEdit.DataSource = dsLOTCategoryForFactory.Tables[0];
    //            chkLstCategoryToEdit.DataTextField = "LOT_CATEGORY_NAME";
    //            chkLstCategoryToEdit.DataValueField = "LOT_CATEGORY_ID";
    //            chkLstCategoryToEdit.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessageRevision(ex.ToString());
    //        return;
    //    }
    //}




    private void BindLOTTFDetailsToEdit(int LOTTFID)
    {
        try
        {
            amendmentCount = 0;

            dsLOTTFDetails = objProject.GetLOTTFDetailsForLOTTransfer(LOTTFID, null); //GetLOTTFDetailsForRevision(LOTTFID);
            if (dsLOTTFDetails.Tables.Count > 0)
            {
                if (dsLOTTFDetails.Tables[0].Rows.Count > 0)
                {
                    if (dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"])))
                        lblTFNo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"])))
                        txtTFNoToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"])))
                        txtCompanyToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["ALTERNATE_UNIT_ID"] != DBNull.Value && Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["ALTERNATE_UNIT_ID"]) > 0)
                        ViewState["ALTERNATE_UNIT_ID"] = Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["ALTERNATE_UNIT_ID"]);

                    //if (dsLOTTFDetails.Tables[0].Rows[0]["ALTERNATE_UNIT_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ALTERNATE_UNIT_NAME"])))
                    //    txtCompanyToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ALTERNATE_UNIT_NAME"]);


                    if (dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"])))
                        txtJOBNoToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"])))
                        txtPONoToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"])))
                        txtCustomerNameToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"])))
                        txtCustomerCodeToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"]);


                    if (dsLOTTFDetails.Tables[0].Rows[0]["DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"])))
                        txtDateToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"])))
                        txtItemNameToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"]);


                }

                if (dsLOTTFDetails.Tables[1].Rows.Count > 0)
                {
                    gvSubItem.DataSource = dsLOTTFDetails.Tables[1];
                    gvSubItem.DataBind();
                    Session["dtSubitem"] = dsLOTTFDetails.Tables[1];
                }
                else
                {
                    gvSubItem.DataSource = null;
                    gvSubItem.DataBind();
                    Session["dtSubitem"] = null;
                }
                lblSubitemsRecords.Text = "Subitem Records[" + gvSubItem.Rows.Count + "]";


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
            if (Convert.ToString(ex).Contains("Timeout expired"))
            {
                ExceptionMessageRevision("The process is too longer, pease try again...!!!");
                return;
            }
            else
            {
                ExceptionMessageRevision(ex.ToString());
                return;
            }

        }
    }





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
            ExceptionMessageRevision(ex.ToString());
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
            ExceptionMessageRevision(ex.ToString());
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
            ExceptionMessageRevision(ex.ToString());
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
            ExceptionMessageRevision(ex.ToString());
        }
    }





    //private void GetTFNo()
    //{
    //    try
    //    {
    //        jobNo = string.Empty;
    //        companyID = 0;
    //        companyName = string.Empty;
    //        runningNo = 0;
    //        runningNoTxt = "";
    //        TFNo = string.Empty;

    //        if (!string.IsNullOrEmpty(txtJOBNoToEdit.Text))
    //        {
    //            jobNo = txtJOBNoToEdit.Text;
    //            if (jobNo.IndexOf('.') > 0)
    //                jobNo = jobNo.Substring(0, (jobNo.IndexOf('.')));
    //        }
    //        else
    //            jobNo = string.Empty;

    //        companyID = Convert.ToInt32(ddlCompany.SelectedValue);
    //        companyName = ddlCompany.SelectedItem.Text;

    //        runningNo = objProject.GetFTRunningNo(jobNo, companyID);
    //        runningNo = runningNo + 1;

    //        //if (dsFTRunningNo.Tables.Count > 0 && dsFTRunningNo.Tables[0].Rows.Count > 0)
    //        //{
    //        //    runningNo = Convert.ToInt32(dsFTRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
    //        //    //runningNoTxt = Convert.ToString(dsFTRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
    //        //}

    //        for (int i = 1; i <= (4 - (Convert.ToString(runningNo).Length)); i++)
    //        {
    //            runningNoTxt += "0";
    //        }

    //        runningNoTxt += Convert.ToString(runningNo);

    //        TFNo = jobNo + "-" + companyName + "-" + runningNoTxt;
    //        txtTFNoToEdit.Text = TFNo;
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessageRevision(ex.ToString());
    //        return;
    //    }
    //}


    //private void GetTFNo()
    //{
    //    try
    //    {
    //        jobNo = string.Empty;
    //        companyID = 0;
    //        companyName = string.Empty;
    //        runningNo = 0;
    //        runningNoTxt = "";
    //        TFNo = string.Empty;
    //        //int unitId = Convert.ToInt32(ViewState["UNIT_ID"]);

    //        if (!string.IsNullOrEmpty(txtJOBNoToEdit.Text))
    //        {
    //            jobNo = txtJOBNoToEdit.Text;
    //            if (jobNo.IndexOf('.') > 0)
    //                jobNo = jobNo.Substring(0, (jobNo.IndexOf('.')));
    //        }
    //        else
    //            jobNo = string.Empty;

    //        //if (unitId == (int)LOTAllStatusAndTypes.EnumUnit.A35)
    //        //    companyID = (int)LOTAllStatusAndTypes.EnumUnit.Gnu;
    //        //else if (unitId == (int)LOTAllStatusAndTypes.EnumUnit.Gnu)
    //        //    companyID = (int)LOTAllStatusAndTypes.EnumUnit.A35;

    //        //companyID = Convert.ToInt32(ddlCompany.SelectedValue);

    //        companyID = Convert.ToInt32(ViewState["ALTERNATE_UNIT_ID"]);
    //        companyName = txtCompanyToEdit.Text;

    //        runningNo = objProject.GetFTRunningNo(jobNo, companyID);

    //        //if (dsFTRunningNo.Tables.Count > 0 && dsFTRunningNo.Tables[0].Rows.Count > 0)
    //        //{
    //        //    runningNo = Convert.ToInt32(dsFTRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
    //        //    //runningNoTxt = Convert.ToString(dsFTRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
    //        //}


    //        for (int i = 1; i <= (4 - (Convert.ToString(runningNo).Length)); i++)
    //        {
    //            runningNoTxt += "0";
    //        }

    //        runningNoTxt = runningNoTxt + Convert.ToString(runningNo + 1);

    //        TFNo = jobNo + "-" + companyName + "-" + runningNoTxt;
    //        txtTFNoToEdit.Text = TFNo;
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessageRevision(ex.ToString());
    //        return;
    //    }
    //}



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
            categoryID = string.Empty;
            category = string.Empty;
            quantity = 0;

            productionOrderNo = string.Empty;
            productionOrderDate = string.Empty;
            expectedCompletionDate = string.Empty;
            productCode = string.Empty;
            productDesc = string.Empty;
            UOM = string.Empty;
            isPartOfProductionStatus = string.Empty;
            isPartOfProductionStatusID = 0;

            subitemAttachment1File = string.Empty;
            subitemAttachment1FileBytes = null;

            subitemAttachment2File = string.Empty;
            subitemAttachment2FileBytes = null;

            subitemAttachment3File = string.Empty;
            subitemAttachment3FileBytes = null;

            subitemAttachment4File = string.Empty;
            subitemAttachment4FileBytes = null;


            if (dtTemp != null && Session["dtSubitem"] != null)
                dtTemp = (DataTable)Session["dtSubitem"];
            else
                AddTempSubitemTable();

            dtTemp = (DataTable)Session["dtSubitem"];


            if (gvSubItem.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItem.Rows)
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


            //LOTMainItem = Convert.ToString(ddlLOTMainItemsToEdit.SelectedItem.Text);
            //LOTMainItemID = Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue);

            //LOTMainSubItem = Convert.ToString(ddlLOTMainSubitemsToEdit.SelectedItem.Text);
            //LOTMainSubItemID = Convert.ToInt32(ddlLOTMainSubitemsToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtTagNoToEdit.Text))
                tagNo = txtTagNoToEdit.Text.Trim().ToUpper().Replace(Environment.NewLine, " "); ;


            if (!string.IsNullOrEmpty(txtDescriptionToEdit.Text))
                subitemDesc = txtDescriptionToEdit.Text.Replace(Environment.NewLine, " ");

            //if (!string.IsNullOrEmpty(txtDrgNoToEdit.Text))
            //    drgNo = txtDrgNoToEdit.Text.Trim().ToUpper();

            //if (!string.IsNullOrEmpty(txtRevNoToEdit.Text))
            //    revisionNo = Convert.ToInt32(txtRevNoToEdit.Text);

            //if (!string.IsNullOrEmpty(ddlRevNoToEdit.SelectedValue))
            //    revisionNo = Convert.ToInt32(ddlRevNoToEdit.SelectedValue);

            //foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
            //{
            //    if (item.Selected)
            //    {
            //        categoryID += item.Value + ",";
            //        category += item.Text + ",";
            //    }
            //}

            if (!string.IsNullOrEmpty(categoryID))
            {
                categoryID = categoryID.TrimEnd(',');
                category = category.TrimEnd(',');
            }

            if (!string.IsNullOrEmpty(txtQuantityToEdit.Text))
                quantity = Convert.ToInt32(txtQuantityToEdit.Text);


            //if (!string.IsNullOrEmpty(txtProductionOrderNoToEdit.Text))
            //    productionOrderNo = txtProductionOrderNoToEdit.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtProductionOrderDateToEdit.Text))
                productionOrderDate = txtProductionOrderDateToEdit.Text;

            if (!string.IsNullOrEmpty(txtExpectedCompletionDateToEdit.Text))
            {
                expectedCompletionDate = txtExpectedCompletionDateToEdit.Text;
                //CheckForExpectedCompletionDate(txtExpectedCompletionDateToEdit.Text);
            }

            //if (ddlProductCodeToEdit.SelectedIndex > 0)
            //    productCode = Convert.ToString(ddlProductCodeToEdit.SelectedValue);

            //if (!string.IsNullOrEmpty(txtProductDescToEdit.Text))
            //    productDesc = txtProductDescToEdit.Text;

            if (!string.IsNullOrEmpty(txtUOMToEdit.Text))
                UOM = txtUOMToEdit.Text;

            //if (chkIsPartOfProductionOrMainDrawingToEdit.Checked)
            //{
            //    isPartOfProductionStatus = "Yes";
            //    isPartOfProductionStatusID = 1;
            //}
            //else
            //{
            //    isPartOfProductionStatus = "No";
            //    isPartOfProductionStatusID = 0;
            //}

            //if (uploadFileSiDrawingToEdit1.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit1.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileSiDrawingToEdit1.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        subitemAttachment1File = str[str.Length - 1];
            //        subitemAttachment1FileBytes = GetFileBytes(uploadFileSiDrawingToEdit1.PostedFile.FileName, uploadFileSiDrawingToEdit1.PostedFile.InputStream);
            //    }
            //}

            //if (uploadFileSiDrawingToEdit2.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit2.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileSiDrawingToEdit2.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        subitemAttachment2File = str[str.Length - 1];
            //        subitemAttachment2FileBytes = GetFileBytes(uploadFileSiDrawingToEdit2.PostedFile.FileName, uploadFileSiDrawingToEdit2.PostedFile.InputStream);
            //    }
            //}

            //if (uploadFileSiDrawingToEdit3.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit3.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileSiDrawingToEdit3.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        subitemAttachment3File = str[str.Length - 1];
            //        subitemAttachment3FileBytes = GetFileBytes(uploadFileSiDrawingToEdit3.PostedFile.FileName, uploadFileSiDrawingToEdit3.PostedFile.InputStream);
            //    }
            //}

            //if (uploadFileSiDrawingToEdit4.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit4.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileSiDrawingToEdit4.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        subitemAttachment4File = str[str.Length - 1];
            //        subitemAttachment4FileBytes = GetFileBytes(uploadFileSiDrawingToEdit4.PostedFile.FileName, uploadFileSiDrawingToEdit4.PostedFile.InputStream);
            //    }
            //}


            int j = 0;
            //for (int i = (gvSubItem.Rows.Count + 1); i < (gvSubItem.Rows.Count + 1 + quantity); i++)
            for (int i = (gvSubItem.Rows.Count + 1); i < (gvSubItem.Rows.Count + 2); i++)
            {
                j++;

                DataRow dr = dtTemp.NewRow();

                dr["SR_NO"] = i;
                dr["LOT_TF_SUBITEM_ID"] = 0;
                dr["LOT_MAIN_ITEM_ID"] = LOTMainItemID;
                dr["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
                dr["LOT_MAIN_ITEM"] = LOTMainItem + " [" + LOTMainSubItem + "]";
                dr["TAG_NO"] = tagNo;
                dr["SUBITEM_DESC"] = subitemDesc;
                dr["DRAWING_NO"] = drgNo;
                dr["REVISION_NO"] = revisionNo;
                dr["CATEGORY_ID"] = categoryID;
                dr["CATEGORY"] = category;
                dr["QUANTITY"] = quantity;// 1;


                dr["PRODUCTION_ORDER_NO"] = productionOrderNo.Trim();
                dr["PRODUCTION_ORDER_DATE"] = productionOrderDate;
                dr["EXPECTED_COMPLETION_DATE"] = expectedCompletionDate;
                dr["PRODUCT_CODE"] = productCode;
                dr["PRODUCT_DESC"] = productDesc;
                dr["UOM"] = UOM;
                dr["IS_PART_OF_PRODUCTION_STATUS_REPORT"] = isPartOfProductionStatus;
                dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;

                dr["IS_REVISED"] = 0;

                dr["SI_ATTACHMENT1_NAME"] = subitemAttachment1File;
                dr["SI_ATTACHMENT1_BTYTES"] = subitemAttachment1FileBytes;

                dr["SI_ATTACHMENT2_NAME"] = subitemAttachment2File;
                dr["SI_ATTACHMENT2_BTYTES"] = subitemAttachment2FileBytes;

                dr["SI_ATTACHMENT3_NAME"] = subitemAttachment3File;
                dr["SI_ATTACHMENT3_BTYTES"] = subitemAttachment3FileBytes;

                dr["SI_ATTACHMENT4_NAME"] = subitemAttachment4File;
                dr["SI_ATTACHMENT4_BTYTES"] = subitemAttachment4FileBytes;

                dtTemp.Rows.Add(dr);
            }

            gvSubItem.DataSource = dtTemp;
            gvSubItem.DataBind();
            lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";

            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
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
            categoryID = string.Empty;
            category = string.Empty;
            quantity = 0;

            productionOrderNo = string.Empty;
            productionOrderDate = string.Empty;
            expectedCompletionDate = string.Empty;
            productCode = string.Empty;
            productDesc = string.Empty;
            UOM = string.Empty;
            isPartOfProductionStatus = string.Empty;
            isPartOfProductionStatusID = 0;

            subitemAttachment1File = string.Empty;
            subitemAttachment1FileBytes = null;

            subitemAttachment2File = string.Empty;
            subitemAttachment2FileBytes = null;

            subitemAttachment3File = string.Empty;
            subitemAttachment3FileBytes = null;

            subitemAttachment4File = string.Empty;
            subitemAttachment4FileBytes = null;


            if (!string.IsNullOrEmpty(txtProductionOrderNoToEdit.Text))
                productionOrderNo = txtProductionOrderNoToEdit.Text;

            if (!string.IsNullOrEmpty(txtExpectedCompletionDateToEdit.Text))
            {
                expectedCompletionDate = txtExpectedCompletionDateToEdit.Text;
                //CheckForExpectedCompletionDate(txtExpectedCompletionDateToEdit.Text);
            }

            //LOTMainItem = Convert.ToString(ddlLOTMainItemsToEdit.SelectedItem.Text);
            //LOTMainItemID = Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue);

            //LOTMainSubItem = Convert.ToString(ddlLOTMainSubitemsToEdit.SelectedItem.Text);
            //LOTMainSubItemID = Convert.ToInt32(ddlLOTMainSubitemsToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtTagNoToEdit.Text))
                tagNo = txtTagNoToEdit.Text;


            if (!string.IsNullOrEmpty(txtDescriptionToEdit.Text))
                subitemDesc = txtDescriptionToEdit.Text;

            //if (!string.IsNullOrEmpty(txtDrgNoToEdit.Text))
            //    drgNo = txtDrgNoToEdit.Text;

            //if (!string.IsNullOrEmpty(txtRevNoToEdit.Text))
            //    revisionNo = Convert.ToInt32(txtRevNoToEdit.Text);

            //if (!string.IsNullOrEmpty(ddlRevNoToEdit.SelectedValue))
            //    revisionNo = Convert.ToInt32(ddlRevNoToEdit.SelectedValue);

            //foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
            //{
            //    if (item.Selected)
            //    {
            //        categoryID += item.Value + ",";
            //        category += item.Text + ",";
            //    }
            //}

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
                expectedCompletionDate = txtExpectedCompletionDateToEdit.Text;

            if (ddlProductCodeToEdit.SelectedIndex > 0)
                productCode = Convert.ToString(ddlProductCodeToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtProductDescToEdit.Text))
                productDesc = txtProductDescToEdit.Text;

            if (!string.IsNullOrEmpty(txtUOMToEdit.Text))
                UOM = txtUOMToEdit.Text;

            //if (chkIsPartOfProductionOrMainDrawingToEdit.Checked)
            //{
            //    isPartOfProductionStatus = "Yes";
            //    isPartOfProductionStatusID = 1;
            //}
            //else
            //{
            //    isPartOfProductionStatus = "No";
            //    isPartOfProductionStatusID = 0;
            //}


            //if (uploadFileSiDrawingToEdit1.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit1.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileSiDrawingToEdit1.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        subitemAttachment1File = str[str.Length - 1];
            //        subitemAttachment1FileBytes = GetFileBytes(uploadFileSiDrawingToEdit1.PostedFile.FileName, uploadFileSiDrawingToEdit1.PostedFile.InputStream);
            //    }
            //}

            //if (uploadFileSiDrawingToEdit2.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit2.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileSiDrawingToEdit2.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        subitemAttachment2File = str[str.Length - 1];
            //        subitemAttachment2FileBytes = GetFileBytes(uploadFileSiDrawingToEdit2.PostedFile.FileName, uploadFileSiDrawingToEdit2.PostedFile.InputStream);
            //    }
            //}

            //if (uploadFileSiDrawingToEdit3.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit3.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileSiDrawingToEdit3.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        subitemAttachment3File = str[str.Length - 1];
            //        subitemAttachment3FileBytes = GetFileBytes(uploadFileSiDrawingToEdit3.PostedFile.FileName, uploadFileSiDrawingToEdit3.PostedFile.InputStream);
            //    }
            //}

            //if (uploadFileSiDrawingToEdit4.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit4.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileSiDrawingToEdit4.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        subitemAttachment4File = str[str.Length - 1];
            //        subitemAttachment4FileBytes = GetFileBytes(uploadFileSiDrawingToEdit4.PostedFile.FileName, uploadFileSiDrawingToEdit4.PostedFile.InputStream);
            //    }
            //}



            if (dtTemp != null && Session["dtSubitem"] != null)
                dtTemp = (DataTable)Session["dtSubitem"];
            else
                AddTempSubitemTable();

            dtTemp = (DataTable)Session["dtSubitem"];

            if (gvSubItem.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItem.Rows)
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

                    //if (Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]) > 0 && ViewState["lblLOTTFMainSubitemID"] != null)
                    //    d["LOT_TF_SUBITEM_ID"] = Convert.ToInt32(ViewState["lblLOTTFMainSubitemID"]);

                    //if (LOTMainItemID > 0)
                    //    d["LOT_MAIN_ITEM_ID"] = LOTMainItemID;
                    //else d["LOT_MAIN_ITEM_ID"] = "0";

                    //if (LOTMainSubItemID > 0)
                    //    d["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
                    //else d["LOT_MAIN_SUBITEM_ID"] = "0";

                    //d["LOT_MAIN_ITEM"] = LOTMainItem + " [" + LOTMainSubItem + "]";

                    //if (!string.IsNullOrEmpty(tagNo))
                    //    d["TAG_NO"] = tagNo;
                    //else d["TAG_NO"] = string.Empty;

                    //if (!string.IsNullOrEmpty(subitemDesc))
                    //    d["SUBITEM_DESC"] = subitemDesc;
                    //else d["SUBITEM_DESC"] = string.Empty;

                    //if (!string.IsNullOrEmpty(drgNo))
                    //    d["DRAWING_NO"] = drgNo;
                    //else d["DRAWING_NO"] = string.Empty;

                    //if (revisionNo > 0)
                    //    d["REVISION_NO"] = revisionNo;
                    //else d["REVISION_NO"] = "0";

                    //if (!string.IsNullOrEmpty(categoryID))
                    //    d["CATEGORY_ID"] = categoryID;
                    //else d["CATEGORY_ID"] = string.Empty;

                    //if (!string.IsNullOrEmpty(category))
                    //    d["CATEGORY"] = category;
                    //else d["CATEGORY"] = string.Empty;


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

                    if (quantity > 0)
                        d["QUANTITY"] = Convert.ToInt32(quantity);
                    else d["QUANTITY"] = 0;

                    //d["QUANTITY"] = 1;

                    //d["IS_PART_OF_PRODUCTION_STATUS_REPORT"] = isPartOfProductionStatus;
                    //d["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;


                    //if (Convert.ToInt32(hdSubitemReviseFlag.Value) > 0)
                    //{
                    //    d["IS_REVISED"] = 1;
                    //}
                    //else
                    //{
                    //    d["IS_REVISED"] = 0;
                    //}





                    //if (Convert.ToInt32(hdSiDrawingToEdit1.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit1.Value) > 0)
                    //{
                    //    if (!string.IsNullOrEmpty(subitemAttachment1File) && subitemAttachment1FileBytes != null)
                    //    {
                    //        d["SI_ATTACHMENT1_NAME"] = subitemAttachment1File;
                    //        d["SI_ATTACHMENT1_BTYTES"] = subitemAttachment1FileBytes;
                    //    }
                    //    else
                    //    {
                    //        d["SI_ATTACHMENT1_NAME"] = string.Empty;
                    //        d["SI_ATTACHMENT1_BTYTES"] = null;
                    //    }
                    //}



                    //if (Convert.ToInt32(hdSiDrawingToEdit2.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit2.Value) > 0)
                    //{
                    //    if (!string.IsNullOrEmpty(subitemAttachment2File) && subitemAttachment2FileBytes != null)
                    //    {
                    //        d["SI_ATTACHMENT2_NAME"] = subitemAttachment2File;
                    //        d["SI_ATTACHMENT2_BTYTES"] = subitemAttachment2FileBytes;
                    //    }
                    //    else
                    //    {
                    //        d["SI_ATTACHMENT2_NAME"] = string.Empty;
                    //        d["SI_ATTACHMENT2_BTYTES"] = null;
                    //    }
                    //}



                    //if (Convert.ToInt32(hdSiDrawingToEdit3.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit3.Value) > 0)
                    //{
                    //    if (!string.IsNullOrEmpty(subitemAttachment3File) && subitemAttachment3FileBytes != null)
                    //    {
                    //        d["SI_ATTACHMENT3_NAME"] = subitemAttachment3File;
                    //        d["SI_ATTACHMENT3_BTYTES"] = subitemAttachment3FileBytes;
                    //    }
                    //    else
                    //    {
                    //        d["SI_ATTACHMENT3_NAME"] = string.Empty;
                    //        d["SI_ATTACHMENT3_BTYTES"] = null;
                    //    }
                    //}


                    //if (Convert.ToInt32(hdSiDrawingToEdit4.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit4.Value) > 0)
                    //{
                    //    if (!string.IsNullOrEmpty(subitemAttachment4File) && subitemAttachment4FileBytes != null)
                    //    {
                    //        d["SI_ATTACHMENT4_NAME"] = subitemAttachment4File;
                    //        d["SI_ATTACHMENT4_BTYTES"] = subitemAttachment4FileBytes;
                    //    }
                    //    else
                    //    {
                    //        d["SI_ATTACHMENT4_NAME"] = string.Empty;
                    //        d["SI_ATTACHMENT4_BTYTES"] = null;
                    //    }
                    //}
                }

                #region MyRegion

                //if (Convert.ToInt32(txtQuantityToEdit.Text) > Convert.ToInt32(hdQuantityToEdit.Value))
                //{
                //    for (int i = 0; i <= (Convert.ToInt32(txtQuantityToEdit.Text) - Convert.ToInt32(hdQuantityToEdit.Value)); i++)
                //    {
                //        DataRow d1 = dtTemp.NewRow();

                //        foreach (DataRow d in dtTemp.Select("SR_NO='" + Convert.ToInt32(hdSRNo.Value) + "'"))
                //        {
                //            //categoryID = string.Empty;
                //            //category = string.Empty;

                //            //if (Convert.ToInt32(d["SR_NO"]) > 0)
                //            //    d1["SR_NO"] = Convert.ToInt32(d["SR_NO"]);
                //            //else d1["SR_NO"] = 0;

                //            d1["SR_NO"] = (dtTemp.Rows.Count + 1);

                //            if (Convert.ToInt32(d["LOT_TF_SUBITEM_ID"]) > 0)
                //                d1["LOT_TF_SUBITEM_ID"] = Convert.ToInt32(d["LOT_TF_SUBITEM_ID"]);
                //            else d1["LOT_TF_SUBITEM_ID"] = 0;

                //            if (Convert.ToInt32(d["LOT_MAIN_ITEM_ID"]) > 0)
                //                d1["LOT_MAIN_ITEM_ID"] = Convert.ToInt32(d["LOT_MAIN_ITEM_ID"]);
                //            else d1["LOT_MAIN_ITEM_ID"] = 0;

                //            if (Convert.ToInt32(d["LOT_MAIN_SUBITEM_ID"]) > 0)
                //                d1["LOT_MAIN_SUBITEM_ID"] = Convert.ToInt32(d["LOT_MAIN_SUBITEM_ID"]);
                //            else d1["LOT_MAIN_SUBITEM_ID"] = 0;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["LOT_MAIN_ITEM"])))
                //                d1["LOT_MAIN_ITEM"] = Convert.ToString(d["LOT_MAIN_ITEM"]);
                //            else d1["LOT_MAIN_ITEM"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["TAG_NO"])))
                //                d1["TAG_NO"] = Convert.ToString(d["TAG_NO"]);
                //            else d1["TAG_NO"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["SUBITEM_DESC"])))
                //                d1["SUBITEM_DESC"] = Convert.ToString(d["SUBITEM_DESC"]);
                //            else d1["SUBITEM_DESC"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["DRAWING_NO"])))
                //                d1["DRAWING_NO"] = Convert.ToString(d["DRAWING_NO"]);
                //            else d1["DRAWING_NO"] = string.Empty;

                //            if (Convert.ToInt32(d["REVISION_NO"]) > 0)
                //                d1["REVISION_NO"] = Convert.ToInt32(d["REVISION_NO"]);
                //            else d1["REVISION_NO"] = 0;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["CATEGORY_ID"])))
                //                d1["CATEGORY_ID"] = Convert.ToString(d["CATEGORY_ID"]);
                //            else d1["CATEGORY_ID"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["CATEGORY"])))
                //                d1["CATEGORY"] = Convert.ToString(d["CATEGORY"]);
                //            else d1["CATEGORY"] = string.Empty;

                //            //if (Convert.ToInt32(d["QUANTITY"]) > 0)
                //            //    d1["QUANTITY"] = Convert.ToInt32(d["QUANTITY"]);
                //            //else d1["QUANTITY"] = 0;

                //            d1["QUANTITY"] = 1;


                //            if (!string.IsNullOrEmpty(Convert.ToString(d["PRODUCTION_ORDER_NO"])))
                //                d1["PRODUCTION_ORDER_NO"] = Convert.ToString(d["PRODUCTION_ORDER_NO"]);
                //            else d1["PRODUCTION_ORDER_NO"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["PRODUCTION_ORDER_DATE"])))
                //                d1["PRODUCTION_ORDER_DATE"] = Convert.ToString(d["PRODUCTION_ORDER_DATE"]);
                //            else d1["PRODUCTION_ORDER_DATE"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["EXPECTED_COMPLETION_DATE"])))
                //                d1["EXPECTED_COMPLETION_DATE"] = Convert.ToString(d["EXPECTED_COMPLETION_DATE"]);
                //            else d1["EXPECTED_COMPLETION_DATE"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["PRODUCT_CODE"])))
                //                d1["PRODUCT_CODE"] = Convert.ToString(d["PRODUCT_CODE"]);
                //            else d1["PRODUCT_CODE"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["PRODUCT_DESC"])))
                //                d1["PRODUCT_DESC"] = Convert.ToString(d["PRODUCT_DESC"]);
                //            else d1["PRODUCT_DESC"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["UOM"])))
                //                d1["UOM"] = Convert.ToString(d["UOM"]);
                //            else d1["UOM"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["IS_PART_OF_PRODUCTION_STATUS_REPORT"])))
                //                d1["IS_PART_OF_PRODUCTION_STATUS_REPORT"] = Convert.ToString(d["IS_PART_OF_PRODUCTION_STATUS_REPORT"]);
                //            else d1["IS_PART_OF_PRODUCTION_STATUS_REPORT"] = string.Empty;

                //            if (!string.IsNullOrEmpty(Convert.ToString(d["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"])))
                //                d1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = Convert.ToString(d["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);
                //            else d1["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = "0";


                //            d1["IS_REVISED"] = 0;


                //            if (Convert.ToInt32(hdSiDrawingToEdit1.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit1.Value) > 0)
                //            {
                //                if (!string.IsNullOrEmpty(subitemAttachment1File) && subitemAttachment1FileBytes != null)
                //                {
                //                    d1["SI_ATTACHMENT1_NAME"] = subitemAttachment1File;
                //                    d1["SI_ATTACHMENT1_BTYTES"] = subitemAttachment1FileBytes;
                //                }
                //                else
                //                {
                //                    d1["SI_ATTACHMENT1_NAME"] = string.Empty;
                //                    d1["SI_ATTACHMENT1_BTYTES"] = null;
                //                }
                //            }
                //            else
                //            {
                //                if (!string.IsNullOrEmpty(Convert.ToString(d["SI_ATTACHMENT1_NAME"])) && d["SI_ATTACHMENT1_BTYTES"] != DBNull.Value)
                //                {
                //                    d1["SI_ATTACHMENT1_NAME"] = Convert.ToString(d["SI_ATTACHMENT1_NAME"]);
                //                    d1["SI_ATTACHMENT1_BTYTES"] = d["SI_ATTACHMENT1_BTYTES"];
                //                }
                //                else
                //                {
                //                    d1["SI_ATTACHMENT1_NAME"] = string.Empty;
                //                    d1["SI_ATTACHMENT1_BTYTES"] = null;
                //                }
                //            }

                //            //else
                //            //{
                //            //    if (!string.IsNullOrEmpty(Convert.ToString(d["SI_ATTACHMENT1_NAME"])))
                //            //        d1["SI_ATTACHMENT1_NAME"] = Convert.ToString(d["SI_ATTACHMENT1_NAME"]);
                //            //    else d1["SI_ATTACHMENT1_NAME"] = string.Empty;

                //            //    if (d["SI_ATTACHMENT1_BTYTES"] != DBNull.Value)
                //            //        d1["SI_ATTACHMENT1_BTYTES"] = d["SI_ATTACHMENT1_BTYTES"];
                //            //    else d1["SI_ATTACHMENT1_BTYTES"] = null;
                //            //}



                //            if (Convert.ToInt32(hdSiDrawingToEdit2.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit2.Value) > 0)
                //            {
                //                if (!string.IsNullOrEmpty(subitemAttachment2File) && subitemAttachment2FileBytes != null)
                //                {
                //                    d1["SI_ATTACHMENT2_NAME"] = subitemAttachment2File;
                //                    d1["SI_ATTACHMENT2_BTYTES"] = subitemAttachment2FileBytes;
                //                }
                //                else
                //                {
                //                    d1["SI_ATTACHMENT2_NAME"] = string.Empty;
                //                    d1["SI_ATTACHMENT2_BTYTES"] = null;
                //                }
                //            }
                //            else
                //            {
                //                if (!string.IsNullOrEmpty(Convert.ToString(d["SI_ATTACHMENT2_NAME"])) && d["SI_ATTACHMENT2_BTYTES"] != DBNull.Value)
                //                {
                //                    d1["SI_ATTACHMENT2_NAME"] = Convert.ToString(d["SI_ATTACHMENT2_NAME"]);
                //                    d1["SI_ATTACHMENT2_BTYTES"] = d["SI_ATTACHMENT2_BTYTES"];
                //                }
                //                else
                //                {
                //                    d1["SI_ATTACHMENT2_NAME"] = string.Empty;
                //                    d1["SI_ATTACHMENT2_BTYTES"] = null;
                //                }
                //            }


                //            //else
                //            //{
                //            //    if (!string.IsNullOrEmpty(Convert.ToString(d["SI_ATTACHMENT2_NAME"])))
                //            //        d1["SI_ATTACHMENT2_NAME"] = Convert.ToString(d["SI_ATTACHMENT2_NAME"]);
                //            //    else d1["SI_ATTACHMENT2_NAME"] = string.Empty;

                //            //    if (d["SI_ATTACHMENT2_BTYTES"] != DBNull.Value)
                //            //        d1["SI_ATTACHMENT2_BTYTES"] = d["SI_ATTACHMENT2_BTYTES"];
                //            //    else d1["SI_ATTACHMENT2_BTYTES"] = null;
                //            //}


                //            if (Convert.ToInt32(hdSiDrawingToEdit3.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit3.Value) > 0)
                //            {
                //                if (!string.IsNullOrEmpty(subitemAttachment3File) && subitemAttachment3FileBytes != null)
                //                {
                //                    d1["SI_ATTACHMENT3_NAME"] = subitemAttachment3File;
                //                    d1["SI_ATTACHMENT3_BTYTES"] = subitemAttachment3FileBytes;
                //                }
                //                else
                //                {
                //                    d1["SI_ATTACHMENT3_NAME"] = string.Empty;
                //                    d1["SI_ATTACHMENT3_BTYTES"] = null;
                //                }
                //            }
                //            else
                //            {
                //                if (!string.IsNullOrEmpty(Convert.ToString(d["SI_ATTACHMENT3_NAME"])) && d["SI_ATTACHMENT3_BTYTES"] != DBNull.Value)
                //                {
                //                    d1["SI_ATTACHMENT3_NAME"] = Convert.ToString(d["SI_ATTACHMENT3_NAME"]);
                //                    d1["SI_ATTACHMENT3_BTYTES"] = d["SI_ATTACHMENT3_BTYTES"];
                //                }
                //                else
                //                {
                //                    d1["SI_ATTACHMENT3_NAME"] = string.Empty;
                //                    d1["SI_ATTACHMENT3_BTYTES"] = null;
                //                }
                //            }


                //            //else
                //            //{
                //            //    if (!string.IsNullOrEmpty(Convert.ToString(d["SI_ATTACHMENT3_NAME"])))
                //            //        d1["SI_ATTACHMENT3_NAME"] = Convert.ToString(d["SI_ATTACHMENT3_NAME"]);
                //            //    else d1["SI_ATTACHMENT3_NAME"] = string.Empty;

                //            //    if (d["SI_ATTACHMENT3_BTYTES"] != DBNull.Value)
                //            //        d1["SI_ATTACHMENT3_BTYTES"] = d["SI_ATTACHMENT3_BTYTES"];
                //            //    else d1["SI_ATTACHMENT3_BTYTES"] = null;
                //            //}


                //            if (Convert.ToInt32(hdSiDrawingToEdit4.Value) == 0 && Convert.ToInt32(hdUploadSiDrawingToEdit4.Value) > 0)
                //            {
                //                if (!string.IsNullOrEmpty(subitemAttachment4File) && subitemAttachment4FileBytes != null)
                //                {
                //                    d1["SI_ATTACHMENT4_NAME"] = subitemAttachment4File;
                //                    d1["SI_ATTACHMENT4_BTYTES"] = subitemAttachment4FileBytes;
                //                }
                //                else
                //                {
                //                    d1["SI_ATTACHMENT4_NAME"] = string.Empty;
                //                    d1["SI_ATTACHMENT4_BTYTES"] = null;
                //                }
                //            }
                //            else
                //            {
                //                if (!string.IsNullOrEmpty(Convert.ToString(d["SI_ATTACHMENT4_NAME"])) && d["SI_ATTACHMENT4_BTYTES"] != DBNull.Value)
                //                {
                //                    d1["SI_ATTACHMENT4_NAME"] = Convert.ToString(d["SI_ATTACHMENT4_NAME"]);
                //                    d1["SI_ATTACHMENT4_BTYTES"] = d["SI_ATTACHMENT4_BTYTES"];
                //                }
                //                else
                //                {
                //                    d1["SI_ATTACHMENT4_NAME"] = string.Empty;
                //                    d1["SI_ATTACHMENT4_BTYTES"] = null;
                //                }
                //            }

                //            //else
                //            //{
                //            //    if (!string.IsNullOrEmpty(Convert.ToString(d["SI_ATTACHMENT4_NAME"])))
                //            //        d1["SI_ATTACHMENT4_NAME"] = Convert.ToString(d["SI_ATTACHMENT4_NAME"]);
                //            //    else d1["SI_ATTACHMENT4_NAME"] = string.Empty;

                //            //    if (d["SI_ATTACHMENT4_BTYTES"] != DBNull.Value)
                //            //        d1["SI_ATTACHMENT4_BTYTES"] = d["SI_ATTACHMENT4_BTYTES"];
                //            //    else d1["SI_ATTACHMENT4_BTYTES"] = null;
                //            //}
                //        }

                //        dtTemp.Rows.Add(d1);

                //    }

                //    if (dtTemp.Rows.Count > 0)
                //    {
                //        int count = 0;
                //        foreach (DataRow dr in dtTemp.Rows)
                //        {
                //            count++;
                //            dr["SR_NO"] = count;
                //        }
                //    }
                //}
                #endregion

            }

            gvSubItem.DataSource = dtTemp;
            gvSubItem.DataBind();

            lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";

            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
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



    private void RemoveSubitems(int srNo)
    {
        if (Session["dtSubitem"] != null)
            dtTemp = (DataTable)Session["dtSubitem"];
        else
            AddTempSubitemTable();

        dtTemp = (DataTable)Session["dtSubitem"];

        if (gvSubItem.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvSubItem.Rows)
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

        //if (dtTemp.Rows.Count > 0)
        //{
        //    foreach (DataRow dr in dtTemp.Select("SR_NO='" + srNo + "'"))
        //    {
        //        //sUID = Convert.ToInt32(dr["S_UID"]);
        //        //if (sUID > 0)
        //        //{
        //        //    foreach (DataRow d1 in dtTemp.Select("S_SUBUID='" + sUID + "'"))
        //        //    {
        //        //        dtTemp.Rows.Remove(d1);
        //        //    }
        //        //}
        //        //else
        //        //{
        //        dtTemp.Rows.Remove(dr);
        //        //}
        //    }
        //}


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

        gvSubItem.DataSource = dtTemp;
        gvSubItem.DataBind();

        lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";
    }

    //private void UpdateSubitems()
    //{
    //    try
    //    {
    //        LOTMainItem = string.Empty;
    //        LOTMainItemID = 0;
    //        LOTMainSubItem = string.Empty;
    //        LOTMainSubItemID = 0;

    //        subitemAttachment1File = string.Empty;
    //        subitemAttachment1FileBytes = null;

    //        subitemAttachment2File = string.Empty;
    //        subitemAttachment2FileBytes = null;

    //        subitemAttachment3File = string.Empty;
    //        subitemAttachment3FileBytes = null;

    //        subitemAttachment4File = string.Empty;
    //        subitemAttachment4FileBytes = null;

    //        if (Session["dtSubitem"] != null)
    //        {
    //            dtTemp = (DataTable)Session["dtSubitem"];
    //        }

    //dtTemp = (DataTable)Session["dtSubitem"];
    //        sUID = 0;
    //        if (dtTemp.Rows.Count > 0)
    //        {
    //            foreach (DataRow d in dtTemp.Select("SR_NO='" + Convert.ToInt32(hdSRNo.Value) + "'"))
    //            {
    //                LOTMainItem = Convert.ToString(ddlLOTMainItems.SelectedItem.Text);
    //                LOTMainItemID = Convert.ToInt32(ddlLOTMainItems.SelectedValue);

    //                LOTMainSubItem = Convert.ToString(ddlLOTMainSubItems.SelectedItem.Text);
    //                LOTMainSubItemID = Convert.ToInt32(ddlLOTMainSubItems.SelectedValue);


    //                if (uploadFileSubitemAttachment1.HasFile)
    //                {
    //                    if (!string.IsNullOrEmpty(uploadFileSubitemAttachment1.PostedFile.FileName))
    //                    {
    //                        string[] str = uploadFileSubitemAttachment1.PostedFile.FileName.Split('\\');
    //                        int length = str.Length;
    //                        subitemAttachment1File = str[str.Length - 1];
    //                        subitemAttachment1FileBytes = GetFileBytes(uploadFileSubitemAttachment1.PostedFile.FileName, uploadFileSubitemAttachment1.PostedFile.InputStream);
    //                    }
    //                }

    //                if (uploadFileSiDrawingToEdit2.HasFile)
    //                {
    //                    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit2.PostedFile.FileName))
    //                    {
    //                        string[] str = uploadFileSiDrawingToEdit2.PostedFile.FileName.Split('\\');
    //                        int length = str.Length;
    //                        subitemAttachment2File = str[str.Length - 1];
    //                        subitemAttachment2FileBytes = GetFileBytes(uploadFileSiDrawingToEdit2.PostedFile.FileName, uploadFileSiDrawingToEdit2.PostedFile.InputStream);
    //                    }
    //                }

    //                if (uploadFileSiDrawingToEdit3.HasFile)
    //                {
    //                    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit3.PostedFile.FileName))
    //                    {
    //                        string[] str = uploadFileSiDrawingToEdit3.PostedFile.FileName.Split('\\');
    //                        int length = str.Length;
    //                        subitemAttachment3File = str[str.Length - 1];
    //                        subitemAttachment3FileBytes = GetFileBytes(uploadFileSiDrawingToEdit3.PostedFile.FileName, uploadFileSiDrawingToEdit3.PostedFile.InputStream);
    //                    }
    //                }

    //                if (uploadFileSiDrawingToEdit4.HasFile)
    //                {
    //                    if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit4.PostedFile.FileName))
    //                    {
    //                        string[] str = uploadFileSiDrawingToEdit4.PostedFile.FileName.Split('\\');
    //                        int length = str.Length;
    //                        subitemAttachment4File = str[str.Length - 1];
    //                        subitemAttachment4FileBytes = GetFileBytes(uploadFileSiDrawingToEdit4.PostedFile.FileName, uploadFileSiDrawingToEdit4.PostedFile.InputStream);
    //                    }
    //                }


    //                //if (Convert.ToInt32(txtQuantity.Text) == Convert.ToInt32(hdQuantity.Value))
    //                //{
    //                if (Convert.ToInt32(hdSRNo.Value) > 0)
    //                    d["SR_NO"] = Convert.ToString(hdSRNo.Value);
    //                else d["SR_NO"] = "0";

    //                if (!string.IsNullOrEmpty(Convert.ToString(txtTagNo.Text)))
    //                    d["TAG_NO"] = Convert.ToString(txtTagNo.Text);
    //                else d["TAG_NO"] = string.Empty;



    //                if (!string.IsNullOrEmpty(subitemAttachment1File) && subitemAttachment1FileBytes != null)
    //                {
    //                    d["SI_ATTACHMENT1_NAME"] = subitemAttachment1File;
    //                    d["SI_ATTACHMENT1_BTYTES"] = subitemAttachment1FileBytes;
    //                }

    //                if (!string.IsNullOrEmpty(subitemAttachment2File) && subitemAttachment2FileBytes != null)
    //                {

    //                    d["SI_ATTACHMENT2_NAME"] = subitemAttachment2File;
    //                    d["SI_ATTACHMENT2_BTYTES"] = subitemAttachment2FileBytes;
    //                }

    //                if (!string.IsNullOrEmpty(subitemAttachment3File) && subitemAttachment3FileBytes != null)
    //                {

    //                    d["SI_ATTACHMENT3_NAME"] = subitemAttachment3File;
    //                    d["SI_ATTACHMENT3_BTYTES"] = subitemAttachment3FileBytes;
    //                }

    //                if (!string.IsNullOrEmpty(subitemAttachment4File) && subitemAttachment4FileBytes != null)
    //                {
    //                    d["SI_ATTACHMENT4_NAME"] = subitemAttachment4File;
    //                    d["SI_ATTACHMENT4_BTYTES"] = subitemAttachment4FileBytes;
    //                }


    //                sUID = Convert.ToInt32(d["S_UID"]);
    //                if (sUID > 0)
    //                {
    //                    foreach (DataRow d1 in dtTemp.Select("S_SUBUID='" + sUID + "'"))
    //                    {
    //                        categoryID = string.Empty;
    //                        category = string.Empty;

    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
    //                            d1["SUBITEM_DESC"] = Convert.ToString(txtDescription.Text);
    //                        else d1["SUBITEM_DESC"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtDrgNo.Text)))
    //                            d1["DRAWING_NO"] = Convert.ToString(txtDrgNo.Text);
    //                        else d1["DRAWING_NO"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtRevNo.Text)))
    //                            d1["REVISION_NO"] = Convert.ToString(txtRevNo.Text);
    //                        else d1["REVISION_NO"] = "0";

    //                        foreach (ListItem item in chkLstCategory.Items)
    //                        {
    //                            if (item.Selected)
    //                            {
    //                                categoryID += item.Value + ",";
    //                                category += item.Text + ",";
    //                            }
    //                        }

    //                        if (!string.IsNullOrEmpty(categoryID))
    //                        {
    //                            categoryID = categoryID.TrimEnd(',');
    //                            category = category.TrimEnd(',');
    //                        }

    //                        if (!string.IsNullOrEmpty(categoryID))
    //                            d1["CATEGORY_ID"] = categoryID;
    //                        else d1["CATEGORY_ID"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(category))
    //                            d1["CATEGORY"] = category;
    //                        else d1["CATEGORY"] = string.Empty;

    //                        //d1["QUANTITY"] = 1;

    //                        if (!string.IsNullOrEmpty(txtQuantity.Text))
    //                            d1["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
    //                        else d1["QUANTITY"] = 0;

    //                    }
    //                }

    //                #region MyRegion


    //                //}
    //                //else if (Convert.ToInt32(txtQuantity.Text) > Convert.ToInt32(hdQuantity.Value))
    //                //{
    //                //    for (int i = 0; i <= (Convert.ToInt32(txtQuantity.Text) - Convert.ToInt32(hdQuantity.Value)); i++)
    //                //    {
    //                //        DataRow d1 = dtTemp.NewRow();

    //                //        categoryID = string.Empty;
    //                //        category = string.Empty;

    //                //        d1["SR_NO"] = (dtTemp.Rows.Count + 1);

    //                //        d1["LOT_MAIN_ITEM_ID"] = LOTMainItemID;
    //                //        d1["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
    //                //        d1["LOT_MAIN_ITEM"] = LOTMainItem + " [" + LOTMainSubItem + "]";

    //                //        d1["S_UID"] = 0;
    //                //        d1["S_SUBUID"] = d["S_SUBUID"];


    //                //        if (Convert.ToInt32(d["S_UID"]) > 0)
    //                //        {
    //                //            if (!string.IsNullOrEmpty(Convert.ToString(txtTagNo.Text)))
    //                //                d1["TAG_NO"] = Convert.ToString(txtTagNo.Text);
    //                //            else d1["TAG_NO"] = string.Empty;
    //                //        }


    //                //        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
    //                //            d1["SUBITEM_DESC"] = Convert.ToString(txtDescription.Text);
    //                //        else d1["SUBITEM_DESC"] = string.Empty;

    //                //        if (!string.IsNullOrEmpty(Convert.ToString(txtDrgNo.Text)))
    //                //            d1["DRAWING_NO"] = Convert.ToString(txtDrgNo.Text);
    //                //        else d1["DRAWING_NO"] = string.Empty;

    //                //        if (!string.IsNullOrEmpty(Convert.ToString(txtRevNo.Text)))
    //                //            d1["REVISION_NO"] = Convert.ToString(txtRevNo.Text);
    //                //        else d1["REVISION_NO"] = "0";

    //                //        foreach (ListItem item in chkLstCategory.Items)
    //                //        {
    //                //            if (item.Selected)
    //                //            {
    //                //                categoryID += item.Value + ",";
    //                //                category += item.Text + ",";
    //                //            }
    //                //        }

    //                //        if (!string.IsNullOrEmpty(categoryID))
    //                //        {
    //                //            categoryID = categoryID.TrimEnd(',');
    //                //            category = category.TrimEnd(',');
    //                //        }

    //                //        if (!string.IsNullOrEmpty(categoryID))
    //                //            d1["CATEGORY_ID"] = categoryID;
    //                //        else d1["CATEGORY_ID"] = string.Empty;

    //                //        if (!string.IsNullOrEmpty(category))
    //                //            d1["CATEGORY"] = category;
    //                //        else d1["CATEGORY"] = string.Empty;

    //                //        d1["QUANTITY"] = 1;

    //                //        //if (!string.IsNullOrEmpty(txtQuantity.Text))
    //                //        //    d1["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
    //                //        //else d1["QUANTITY"] = 0;

    //                //        dtTemp.Rows.Add(d1);

    //                //    }
    //                //}
    //                //else if (Convert.ToInt32(txtQuantity.Text) < Convert.ToInt32(hdQuantity.Value))
    //                //{
    //                //    RemoveSubitems(Convert.ToInt32(hdSRNo.Value));
    //                //}

    //                #endregion



    //            }



    //            #region MyRegion

    //            //foreach (DataRow dr in dtTemp.Rows)
    //            //{
    //            //    srNo = Convert.ToInt32(dr["SR_NO"]);
    //            //    sUID = Convert.ToInt32(dr["S_UID"]);

    //            //    //1. update primary details
    //            //    if (sUID > 0)
    //            //    {
    //            //        foreach (DataRow dr1 in dtTemp.Select("SR_NO='" + Convert.ToInt32(hdSRNo.Value) + "'"))
    //            //        {
    //            //            categoryID = string.Empty;
    //            //            category = string.Empty;

    //            //            subUID = Convert.ToInt32(Convert.ToString(dr1["S_SUBUID"]).Split('.')[0]);
    //            //            if (sUID == subUID)
    //            //            {
    //            //                if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
    //            //                    dr1["SUBITEM_DESC"] = Convert.ToString(txtDescription.Text);
    //            //                else dr1["SUBITEM_DESC"] = string.Empty;

    //            //                if (!string.IsNullOrEmpty(Convert.ToString(txtDrgNo.Text)))
    //            //                    dr1["DRAWING_NO"] = Convert.ToString(txtDrgNo.Text);
    //            //                else dr1["DRAWING_NO"] = string.Empty;

    //            //                if (!string.IsNullOrEmpty(Convert.ToString(txtRevNo.Text)))
    //            //                    dr1["REVISION_NO"] = Convert.ToString(txtRevNo.Text);
    //            //                else dr1["REVISION_NO"] = "0";

    //            //                foreach (ListItem item in chkLstCategory.Items)
    //            //                {
    //            //                    if (item.Selected)
    //            //                    {
    //            //                        categoryID += item.Value + ",";
    //            //                        category += item.Text + ",";
    //            //                    }
    //            //                }

    //            //                if (!string.IsNullOrEmpty(categoryID))
    //            //                {
    //            //                    categoryID = categoryID.TrimEnd(',');
    //            //                    category = category.TrimEnd(',');
    //            //                }

    //            //                if (!string.IsNullOrEmpty(categoryID))
    //            //                    dr1["CATEGORY_ID"] = categoryID;
    //            //                else dr1["CATEGORY_ID"] = string.Empty;

    //            //                if (!string.IsNullOrEmpty(category))
    //            //                    dr1["CATEGORY"] = category;
    //            //                else dr1["CATEGORY"] = string.Empty;

    //            //                if (!string.IsNullOrEmpty(txtQuantity.Text))
    //            //                    dr1["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
    //            //                else dr1["QUANTITY"] = 0;

    //            //            }
    //            //        }
    //            //    }
    //            //    else
    //            //    {

    //            //    }


    //            //    //2. update attachments and tag no.
    //            //    if (srNo == Convert.ToInt32(hdSRNo.Value))
    //            //    {
    //            //        if (Convert.ToInt32(hdSRNo.Value) > 0)
    //            //            dr["SR_NO"] = Convert.ToString(hdSRNo.Value);
    //            //        else dr["SR_NO"] = "0";

    //            //        if (!string.IsNullOrEmpty(Convert.ToString(txtTagNo.Text)))
    //            //            dr["TAG_NO"] = Convert.ToString(txtTagNo.Text);
    //            //        else dr["TAG_NO"] = string.Empty;


    //            //        if (uploadFileSubitemAttachment1.HasFile)
    //            //        {
    //            //            if (!string.IsNullOrEmpty(uploadFileSubitemAttachment1.PostedFile.FileName))
    //            //            {
    //            //                //subitemAttachment1File = uploadFileSiDrawingToEdit1.PostedFile.FileName;
    //            //                string[] str = uploadFileSubitemAttachment1.PostedFile.FileName.Split('\\');
    //            //                int length = str.Length;
    //            //                subitemAttachment1File = str[str.Length - 1];
    //            //                subitemAttachment1FileBytes = GetFileBytes(uploadFileSubitemAttachment1.PostedFile.FileName, uploadFileSubitemAttachment1.PostedFile.InputStream);
    //            //            }
    //            //        }

    //            //        if (uploadFileSiDrawingToEdit2.HasFile)
    //            //        {
    //            //            if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit2.PostedFile.FileName))
    //            //            {
    //            //                //subitemAttachment2File = uploadFileSiDrawingToEdit2ToEdit.PostedFile.FileName;
    //            //                string[] str = uploadFileSiDrawingToEdit2.PostedFile.FileName.Split('\\');
    //            //                int length = str.Length;
    //            //                subitemAttachment2File = str[str.Length - 1];
    //            //                subitemAttachment2FileBytes = GetFileBytes(uploadFileSiDrawingToEdit2.PostedFile.FileName, uploadFileSiDrawingToEdit2.PostedFile.InputStream);
    //            //            }
    //            //        }

    //            //        if (uploadFileSiDrawingToEdit3.HasFile)
    //            //        {
    //            //            if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit3.PostedFile.FileName))
    //            //            {
    //            //                //subitemAttachment3File = uploadFileSiDrawingToEdit3ToEdit.PostedFile.FileName;
    //            //                string[] str = uploadFileSiDrawingToEdit3.PostedFile.FileName.Split('\\');
    //            //                int length = str.Length;
    //            //                subitemAttachment3File = str[str.Length - 1];
    //            //                subitemAttachment3FileBytes = GetFileBytes(uploadFileSiDrawingToEdit3.PostedFile.FileName, uploadFileSiDrawingToEdit3.PostedFile.InputStream);
    //            //            }
    //            //        }

    //            //        if (uploadFileSiDrawingToEdit4.HasFile)
    //            //        {
    //            //            if (!string.IsNullOrEmpty(uploadFileSiDrawingToEdit4.PostedFile.FileName))
    //            //            {
    //            //                //subitemAttachment4File = uploadFileSiDrawingToEdit4ToEdit.PostedFile.FileName;
    //            //                string[] str = uploadFileSiDrawingToEdit4.PostedFile.FileName.Split('\\');
    //            //                int length = str.Length;
    //            //                subitemAttachment4File = str[str.Length - 1];
    //            //                subitemAttachment4FileBytes = GetFileBytes(uploadFileSiDrawingToEdit4.PostedFile.FileName, uploadFileSiDrawingToEdit4.PostedFile.InputStream);
    //            //            }
    //            //        }



    //            //        if (!string.IsNullOrEmpty(subitemAttachment1File) && subitemAttachment1FileBytes != null)
    //            //        {
    //            //            dr["SI_ATTACHMENT1_NAME"] = subitemAttachment1File;
    //            //            dr["SI_ATTACHMENT1_BTYTES"] = subitemAttachment1FileBytes;
    //            //        }

    //            //        if (!string.IsNullOrEmpty(subitemAttachment2File) && subitemAttachment2FileBytes != null)
    //            //        {

    //            //            dr["SI_ATTACHMENT2_NAME"] = subitemAttachment2File;
    //            //            dr["SI_ATTACHMENT2_BTYTES"] = subitemAttachment2FileBytes;
    //            //        }

    //            //        if (!string.IsNullOrEmpty(subitemAttachment3File) && subitemAttachment3FileBytes != null)
    //            //        {

    //            //            dr["SI_ATTACHMENT3_NAME"] = subitemAttachment3File;
    //            //            dr["SI_ATTACHMENT3_BTYTES"] = subitemAttachment3FileBytes;
    //            //        }

    //            //        if (!string.IsNullOrEmpty(subitemAttachment4File) && subitemAttachment4FileBytes != null)
    //            //        {
    //            //            dr["SI_ATTACHMENT4_NAME"] = subitemAttachment4File;
    //            //            dr["SI_ATTACHMENT4_BTYTES"] = subitemAttachment4FileBytes;
    //            //        }

    //            //    }
    //            //}





    //            //foreach (DataRow dr in dtTemp.Select("SR_NO='" + Convert.ToString(hdSRNo.Value) + "'"))
    //            //{
    //            //    srNo = Convert.ToInt32(dr["SR_NO"]);
    //            //    sUID = Convert.ToInt32(dr["S_UID"]);
    //            //    sSubUID = Convert.ToString(dr["S_SUBUID"]);


    //            //    //1. update primary details





    //            //    //2. update attachments

    //            //    if (srNo == Convert.ToInt32(hdSRNo.Value))
    //            //    {

    //            //    }




    //            //    if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
    //            //        dr["SUBITEM_DESC"] = Convert.ToString(txtDescription.Text);
    //            //    else dr["SUBITEM_DESC"] = string.Empty;

    //            //    if (!string.IsNullOrEmpty(Convert.ToString(txtDrgNo.Text)))
    //            //        dr["DRAWING_NO"] = Convert.ToString(txtDrgNo.Text);
    //            //    else dr["DRAWING_NO"] = string.Empty;

    //            //    if (!string.IsNullOrEmpty(Convert.ToString(txtRevNo.Text)))
    //            //        dr["REVISION_NO"] = Convert.ToString(txtRevNo.Text);
    //            //    else dr["REVISION_NO"] = "0";

    //            //    foreach (ListItem item in chkLstCategory.Items)
    //            //    {
    //            //        if (item.Selected)
    //            //        {
    //            //            categoryID += item.Value + ",";
    //            //            category += item.Text + ",";
    //            //        }
    //            //    }

    //            //    if (!string.IsNullOrEmpty(categoryID))
    //            //    {
    //            //        categoryID = categoryID.TrimEnd(',');
    //            //        category = category.TrimEnd(',');
    //            //    }

    //            //    if (!string.IsNullOrEmpty(categoryID))
    //            //        dr["CATEGORY_ID"] = categoryID;
    //            //    else dr["CATEGORY_ID"] = string.Empty;

    //            //    if (!string.IsNullOrEmpty(category))
    //            //        dr["CATEGORY"] = category;
    //            //    else dr["CATEGORY"] = string.Empty;

    //            //    if (!string.IsNullOrEmpty(txtQuantity.Text))
    //            //        dr["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
    //            //    else dr["QUANTITY"] = 0;
    //            //}

    //            #endregion

    //        }



    //        if (dtTemp.Rows.Count > 0)
    //        {
    //            int n = 1;
    //            foreach (DataRow dr in dtTemp.Rows)
    //            {
    //                dr["SR_NO"] = n++;
    //            }

    //            gvSubItem.DataSource = dtTemp;
    //            gvSubItem.DataBind();
    //        }
    //        else
    //        {
    //            gvSubItem.DataSource = null;
    //            gvSubItem.DataBind();
    //        }


    //        lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";
    //        hdSRNo.Value = "0";
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessageReviseList(ex.ToString());
    //        return;
    //    }
    //}




    //private void AddLOTApprover()
    //{
    //    try
    //    {
    //        dsAppJOBNo = objProject.GetAppJOBNoOne(txtJOBNo.Text);
    //        if (dsAppJOBNo.Tables.Count > 0 && dsAppJOBNo.Tables[0].Rows.Count > 0)
    //        {
    //            AddNewLOTTransmittalToFactory();
    //        }
    //        else
    //        {
    //            mpeAddApprovers.Show();
    //            iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessageReviseList(ex.ToString());
    //        return;
    //    }
    //}



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

            //if (uploadFileAttachment1ToEdit.HasFile)
            //{
            //    if (!string.IsNullOrEmpty(uploadFileAttachment1ToEdit.PostedFile.FileName))
            //    {
            //        string[] str = uploadFileAttachment1ToEdit.PostedFile.FileName.Split('\\');
            //        int length = str.Length;
            //        attachment1File = str[str.Length - 1];
            //        attachment1FileBytes = GetFileBytes(uploadFileAttachment1ToEdit.PostedFile.FileName, uploadFileAttachment1ToEdit.PostedFile.InputStream);
            //    }
            //}

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
            ExceptionMessageRevision(ex.ToString());
            return;
        }
    }



    private void SaveLOTLOTTransmittalToFactory()
    {
        try
        {
            UpdateStatusOfTransferredLOT();
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
            return;
        }
    }

    private void UpdateStatusOfTransferredLOT()
    {
        try
        {

            #region LOT Primary Details

            int srNo = 0;
            LOTTFSubitemIDs = string.Empty;
            LOTMainSubItemIDs = string.Empty;

            TFNo = txtTFNoToEdit.Text;
            statusID = (int)LOTAllStatusAndTypes.EnumStatus.Forwarded;

            int LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

            if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0 && ViewState["UNIT_ID"] != null)
                companyID = Convert.ToInt32(ViewState["UNIT_ID"]);

            if (!string.IsNullOrEmpty(Convert.ToString(txtRemarksToEdit)))
                remarks = Convert.ToString(txtRemarksToEdit.Text).Replace(Environment.NewLine, " ");

            createdByID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            #endregion


            #region LOT Subitems Detail

            dtSubitemToAdd.Columns.Add("SR_NO", typeof(int));
            dtSubitemToAdd.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
            dtSubitemToAdd.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtSubitemToAdd.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
            dtSubitemToAdd.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            dtSubitemToAdd.Columns.Add("PRODUCT_CODE", typeof(string));
            dtSubitemToAdd.Columns.Add("PRODUCT_DESC", typeof(string));
            dtSubitemToAdd.Columns.Add("UOM", typeof(string));
            dtSubitemToAdd.Columns.Add("QUANTITY", typeof(int));

            if (gvSubItem.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItem.Rows)
                {
                    srNo = 0;
                    LOTTFSubitemID = 0;
                    quantity = 0;
                    productionOrderNo = string.Empty;
                    productionOrderDate = string.Empty;
                    expectedCompletionDate = string.Empty;
                    productCode = string.Empty;
                    productDesc = string.Empty;
                    UOM = string.Empty;

                    Label lblSrNo = gr.FindControl("lblSrNo") as Label;
                    Label lblLOTTFMainSubitemID = gr.FindControl("lblLOTTFMainSubitemID") as Label;
                    Label lblLOTMainSubitemID = gr.FindControl("lblLOTMainSubitemID") as Label;
                    Label lblProductionNumber = gr.FindControl("lblProductionNumber") as Label;
                    Label lblProductionOrderDate = gr.FindControl("lblProductionOrderDate") as Label;
                    Label lblExpectedCompletionDate = gr.FindControl("lblExpectedCompletionDate") as Label;
                    Label lblProductCode = gr.FindControl("lblProductCode") as Label;
                    Label lblProductDesc = gr.FindControl("lblProductDesc") as Label;
                    Label lblUOM = gr.FindControl("lblUOM") as Label;
                    Label lblQuantity = gr.FindControl("lblQuantity") as Label;

                    //if (Convert.ToInt32(lblSrNo.Text) > 0)
                    srNo = Convert.ToInt32(lblSrNo.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblLOTTFMainSubitemID.Text)) &&
                        Convert.ToInt32(lblLOTTFMainSubitemID.Text) > 0)
                    {
                        LOTTFSubitemID = Convert.ToInt32(lblLOTTFMainSubitemID.Text);
                        LOTTFSubitemIDs += Convert.ToString(LOTTFSubitemID) + ",";
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblLOTMainSubitemID.Text)) &&
                        Convert.ToInt32(lblLOTMainSubitemID.Text) > 0)
                    {
                        LOTMainSubItemID = Convert.ToInt32(lblLOTMainSubitemID.Text);
                        LOTMainSubItemIDs += Convert.ToString(lblLOTMainSubitemID.Text) + ",";
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductionNumber.Text)))
                        productionOrderNo = Convert.ToString(lblProductionNumber.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDate.Text)))
                        productionOrderDate = Convert.ToString(lblProductionOrderDate.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblExpectedCompletionDate.Text)))
                        expectedCompletionDate = Convert.ToString(lblExpectedCompletionDate.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text)))
                        productCode = Convert.ToString(lblProductCode.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductDesc.Text)))
                        productDesc = Convert.ToString(lblProductDesc.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text)))
                        UOM = Convert.ToString(lblUOM.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblQuantity.Text)))
                        quantity = Convert.ToInt32(lblQuantity.Text);

                    if (!string.IsNullOrEmpty(productionOrderNo))
                    {
                        DataRow drn = dtSubitemToAdd.NewRow();

                        drn["SR_NO"] = srNo;
                        drn["LOT_TF_SUBITEM_ID"] = LOTTFSubitemID;
                        drn["PRODUCTION_ORDER_NO"] = productionOrderNo;
                        drn["PRODUCTION_ORDER_DATE"] = productionOrderDate;
                        drn["EXPECTED_COMPLETION_DATE"] = expectedCompletionDate;
                        drn["PRODUCT_CODE"] = productCode;
                        drn["PRODUCT_DESC"] = productDesc;
                        drn["UOM"] = UOM;
                        drn["QUANTITY"] = quantity;

                        dtSubitemToAdd.Rows.Add(drn);
                    }
                }
            }
            else
            {
                mpeReviseLOT.Show();
                ExceptionMessageRevision("No subitem found to link production order number...!!!");
                return;
            }

            if (dtSubitemToAdd.Rows.Count == 0)
            {
                mpeReviseLOT.Show();
                ExceptionMessageRevision("Please link all subitems...!!!");
                return;
            }

            if (gvSubItem.Rows.Count != dtSubitemToAdd.Rows.Count)
            {
                mpeReviseLOT.Show();
                ExceptionMessageRevision("Please link all subitems...!!!");
                return;
            }


            if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

            if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');


            int value = 0;

            if (LOTTFID > 0)
            {
                value = objProject.UpdateProductionOrderOnTransferredLOT(LOTTFID, dtSubitemToAdd, remarks, createdByID);
            }
            else
            {
                ExceptionMessageRevision("Please try again...!!!");
                return;
            }

            if (value > 0)
            {
                int sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, TFNo
                                                              , companyID, "", 0
                                                              , 0, null, 0, 0
                                                              , (int)LOTAllStatusAndTypes.EnumEmailType.LinkProductionOrderByStoreEmail);
                if (sendMailValue > 0)
                {
                    int val = objProject.UpdateLOTMailStatusTwo(value, LOTMainSubItemIDs, statusID, 0
                                                              , Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    SuccessMessageReviseList("Production order details linked with TF. No.: '" + TFNo + "' and mail sent successfully.");
                    Reset();
                }
                else
                {
                    SuccessMessageReviseList("Production order details linked with TF. No.: '" + TFNo + "' successfully.");
                    Reset();
                }

                GetLOTList();
            }

            #endregion

        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
            return;
        }
    }



    //private void AddNewLOTTransmittalToFactory()
    //{
    //    try
    //    {

    //        #region LOT Primary Details


    //        string LOTTFsubitemIDsToVoid = string.Empty;
    //        int LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

    //        TFNo = string.Empty;
    //        companyID = 0;
    //        LOTDate = string.Empty;
    //        customerName = string.Empty;
    //        custCode = string.Empty;
    //        jobNo = string.Empty;
    //        poNo = string.Empty;
    //        itemName = string.Empty;
    //        impNotes = string.Empty;

    //        attachment1File = string.Empty;
    //        attachment1FileBytes = null;

    //        attachment2File = string.Empty;
    //        attachment2FileBytes = null;

    //        attachment3File = string.Empty;
    //        attachment3FileBytes = null;

    //        attachment4File = string.Empty;
    //        attachment4FileBytes = null;

    //        //if (!string.IsNullOrEmpty(Convert.ToString(txtTFNoToEdit.Text)))
    //        //    TFNo = Convert.ToString(txtTFNoToEdit.Text);

    //        if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0 && ViewState["UNIT_ID"] != null)
    //            companyID = Convert.ToInt32(ViewState["UNIT_ID"]);

    //        if (!string.IsNullOrEmpty(Convert.ToString(txtDateToEdit.Text)))
    //            LOTDate = Convert.ToDateTime(txtDateToEdit.Text).ToString("yyyy-MM-dd");

    //        if (!string.IsNullOrEmpty(Convert.ToString(txtCustomerNameToEdit.Text)))
    //            customerName = Convert.ToString(txtCustomerNameToEdit.Text);

    //        if (!string.IsNullOrEmpty(Convert.ToString(txtCustomerCodeToEdit.Text)))
    //            custCode = Convert.ToString(txtCustomerCodeToEdit.Text);

    //        if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNoToEdit.Text)))
    //            jobNo = Convert.ToString(txtJOBNoToEdit.Text);

    //        if (!string.IsNullOrEmpty(Convert.ToString(txtPONoToEdit.Text)))
    //            poNo = Convert.ToString(txtPONoToEdit.Text);

    //        if (!string.IsNullOrEmpty(Convert.ToString(txtItemNameToEdit.Text)))
    //            itemName = Convert.ToString(txtItemNameToEdit.Text).Replace(Environment.NewLine, " ");

    //        if (!string.IsNullOrEmpty(Convert.ToString(txtNotesToEdit.Text)))
    //            impNotes = Convert.ToString(txtNotesToEdit.Text).Replace(Environment.NewLine, " "); ;



    //        //if (uploadFileAttachment1ToEdit.HasFile)
    //        //{
    //        //    if (!string.IsNullOrEmpty(uploadFileAttachment1ToEdit.PostedFile.FileName))
    //        //    {
    //        //        string[] str = uploadFileAttachment1ToEdit.PostedFile.FileName.Split('\\');
    //        //        int length = str.Length;
    //        //        attachment1File = str[str.Length - 1];
    //        //        attachment1FileBytes = GetFileBytes(uploadFileAttachment1ToEdit.PostedFile.FileName, uploadFileAttachment1ToEdit.PostedFile.InputStream);
    //        //    }
    //        //}

    //        #endregion


    //        #region LOT Subitems Detail



    //        DataTable dtNew = new DataTable();

    //        //dtSubitemToAdd.Columns.Add("STATUS_ID", typeof(int));
    //        //dtSubitemToAdd.Columns.Add("SUBITEM_DESC", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("TAG_NO", typeof(string));

    //        dtSubitemToAdd.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
    //        dtSubitemToAdd.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
    //        dtSubitemToAdd.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
    //        dtSubitemToAdd.Columns.Add("PRODUCT_CODE", typeof(string));
    //        dtSubitemToAdd.Columns.Add("PRODUCT_DESC", typeof(string));
    //        dtSubitemToAdd.Columns.Add("UOM", typeof(string));
    //        dtSubitemToAdd.Columns.Add("QUANTITY", typeof(int));

    //        //dtSubitemToAdd.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
    //        //dtSubitemToAdd.Columns.Add("DRAWING_NO", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("REVISION_NO", typeof(int));
    //        //dtSubitemToAdd.Columns.Add("CATEGORY_ID", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID", typeof(int));

    //        //dtSubitemToAdd.Columns.Add("CREATED_BY", typeof(int));
    //        //dtSubitemToAdd.Columns.Add("CREATED_ON", typeof(string));

    //        //dtSubitemToAdd.Columns.Add("APPROVED_BY", typeof(int));
    //        //dtSubitemToAdd.Columns.Add("APPROVED_ON", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("APPROVED_REMARKS", typeof(string));

    //        //dtSubitemToAdd.Columns.Add("ACCEPTED_BY", typeof(int));
    //        //dtSubitemToAdd.Columns.Add("ACCEPTED_ON", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("ACCEPTED_REMARKS", typeof(string));

    //        //dtSubitemToAdd.Columns.Add("SI_ATTACHMENT1_NAME", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("SI_ATTACHMENT1_BTYTES", typeof(byte[]));

    //        //dtSubitemToAdd.Columns.Add("SI_ATTACHMENT2_NAME", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("SI_ATTACHMENT2_BTYTES", typeof(byte[]));
    //        //
    //        //dtSubitemToAdd.Columns.Add("SI_ATTACHMENT3_NAME", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("SI_ATTACHMENT3_BTYTES", typeof(byte[]));
    //        //
    //        //dtSubitemToAdd.Columns.Add("SI_ATTACHMENT4_NAME", typeof(string));
    //        //dtSubitemToAdd.Columns.Add("SI_ATTACHMENT4_BTYTES", typeof(byte[]));


    //        if (Session["dtSubitem"] != null)
    //            dtNew = (DataTable)Session["dtSubitem"];
    //        else
    //            AddTempSubitemTable();

    //        dtNew = (DataTable)Session["dtSubitem"];

    //        //if (gvSubItem.Rows.Count > 0)
    //        //{
    //        //    foreach (GridViewRow gr in gvSubItem.Rows)
    //        //    {
    //        //        Label lblSrNo = gr.FindControl("lblSrNo") as Label;
    //        //        TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;

    //        //        if (dtNew.Rows.Count > 0)
    //        //        {
    //        //            foreach (DataRow dr in dtNew.Select("SR_NO='" + Convert.ToInt32(lblSrNo.Text) + "'"))
    //        //            {
    //        //                dr["TAG_NO"] = txtTagNoInList.Text;
    //        //            }
    //        //        }
    //        //    }
    //        //}


    //        //if (Session["dtProdMngr"] != null)
    //        //    dtProdMngr = (DataTable)Session["dtProdMngr"];



    //        if (dtNew.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dtNew.Rows)
    //            {
    //                statusID = 0;
    //                //sUID = 0;
    //                //subUID = 0;

    //                LOTTFSubitemID = 0;
    //                subitemDesc = string.Empty;
    //                tagNo = string.Empty;
    //                productionOrderNo = string.Empty;
    //                expectedCompletionDate = string.Empty;
    //                LOTMainItem = string.Empty;
    //                LOTMainSubItem = string.Empty;
    //                LOTMainItemID = 0;
    //                LOTMainSubItemID = 0;

    //                drgNo = string.Empty;
    //                revisionNo = 0;
    //                quantity = 0;
    //                productionOrderNo = string.Empty;
    //                productionOrderDate = string.Empty;
    //                expectedCompletionDate = string.Empty;
    //                productCode = string.Empty;
    //                productDesc = string.Empty;
    //                UOM = string.Empty;
    //                isPartOfProductionStatusID = 0;
    //                isRevised = 0;
    //                categoryID = string.Empty;
    //                category = string.Empty;

    //                createdByID = 0;
    //                amendedByID = 0;
    //                PEID = 0;
    //                PMID = 0;
    //                approvedByID = 0;
    //                amendedApprovedByID = 0;
    //                prodMngrID = 0;
    //                acceptedByID = 0;
    //                amendmentCount = 0;

    //                subitemAttachment1File = string.Empty;
    //                subitemAttachment2File = string.Empty;
    //                subitemAttachment3File = string.Empty;
    //                subitemAttachment4File = string.Empty;

    //                subitemAttachment1FileBytes = null;
    //                subitemAttachment2FileBytes = null;
    //                subitemAttachment3FileBytes = null;
    //                subitemAttachment4FileBytes = null;

    //                remarks = "";
    //                createdByID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

    //                PEID = Convert.ToInt32(ViewState["PE_ID"]);
    //                PMID = Convert.ToInt32(ViewState["PM_ID"]);

    //                if (dtProdMngr.Rows.Count > 0)
    //                {
    //                    foreach (DataRow dr2 in dtProdMngr.Select("LOT_MAIN_SUBITEM_ID='" + Convert.ToInt32(dr["LOT_MAIN_SUBITEM_ID"]) + "'"))
    //                    {
    //                        prodMngrID = Convert.ToInt32(dr2["PRODUCTION_MNGR_ID"]);
    //                    }
    //                }




    //                //string strStatusAndApproverID = GetStatusAndApproverID(createdByID, PEID, PMID, prodMngrID);
    //                //string strStatusAndApproverID = objGetApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID);

    //                //objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(PEID, PMID, prodMngrID, objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, 0);

    //                //objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, 0);

    //                //statusID = Convert.ToInt32(strStatusAndApproverID.Split(':')[0]);
    //                //approvedByID = Convert.ToInt32(strStatusAndApproverID.Split(':')[1]);
    //                //acceptedByID = Convert.ToInt32(strStatusAndApproverID.Split(':')[2]);

    //                //if (amendmentCount > 0)
    //                //    objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(amendedByID, PEID, PMID, prodMngrID, amendedApprovedByID), amendmentCount, 0, PEID, PMID, prodMngrID);
    //                //else
    //                //    objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, 0, PEID, PMID, prodMngrID);


    //                //statusID = objLOTMailTypeAndStatusProperties.CurrentStatusID;
    //                //approvedByID = objLOTMailTypeAndStatusProperties.ApprovedByID;
    //                //acceptedByID = objLOTMailTypeAndStatusProperties.AcceptedByID;

    //                //statusID = (int)LOTAllStatusAndTypes.EnumStatus.Forwarded;// GetStatusID(createdByID, PEID, PMID, prodMngrID);

    //                //if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
    //                //  approvedByID = createdByID;

    //                //if (dr["S_UID"] != DBNull.Value && Convert.ToInt32(dr["S_UID"]) > 0)
    //                //    sUID = Convert.ToInt32(dr["S_UID"]);

    //                //if (dr["S_SUBUID"] != DBNull.Value && Convert.ToInt32(dr["S_SUBUID"]) > 0)
    //                //    subUID = Convert.ToInt32(dr["S_SUBUID"]);

    //                if (dr["LOT_TF_SUBITEM_ID"] != DBNull.Value && Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]) > 0)
    //                    LOTTFSubitemID = Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]);

    //                if (dr["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SUBITEM_DESC"])))
    //                    subitemDesc = Convert.ToString(dr["SUBITEM_DESC"]).Replace(Environment.NewLine, " "); ;

    //                if (dr["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["TAG_NO"])))
    //                    tagNo = Convert.ToString(dr["TAG_NO"]).Replace(Environment.NewLine, " "); ;


    //                if (dr["PRODUCTION_ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PRODUCTION_ORDER_NO"])))
    //                    productionOrderNo = Convert.ToString(dr["PRODUCTION_ORDER_NO"]);

    //                if (dr["PRODUCTION_ORDER_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PRODUCTION_ORDER_DATE"])))
    //                    productionOrderDate = Convert.ToDateTime(dr["PRODUCTION_ORDER_DATE"]).ToString("yyyy-MM-dd");

    //                if (dr["EXPECTED_COMPLETION_DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EXPECTED_COMPLETION_DATE"])))
    //                    expectedCompletionDate = Convert.ToDateTime(dr["EXPECTED_COMPLETION_DATE"]).ToString("yyyy-MM-dd");

    //                if (dr["PRODUCT_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PRODUCT_CODE"])))
    //                    productCode = Convert.ToString(dr["PRODUCT_CODE"]);

    //                if (dr["PRODUCT_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PRODUCT_DESC"])))
    //                    productDesc = Convert.ToString(dr["PRODUCT_DESC"]);

    //                if (dr["UOM"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["UOM"])))
    //                    UOM = Convert.ToString(dr["UOM"]);

    //                if (dr["QUANTITY"] != DBNull.Value && Convert.ToInt32(dr["QUANTITY"]) > 0)
    //                    quantity = Convert.ToInt32(dr["QUANTITY"]);


    //                if (dr["LOT_MAIN_SUBITEM_ID"] != DBNull.Value && Convert.ToInt32(dr["LOT_MAIN_SUBITEM_ID"]) > 0)
    //                    LOTMainSubItemID = Convert.ToInt32(dr["LOT_MAIN_SUBITEM_ID"]);

    //                if (dr["DRAWING_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_NO"])))
    //                    drgNo = Convert.ToString(dr["DRAWING_NO"]);

    //                if (dr["REVISION_NO"] != DBNull.Value && Convert.ToInt32(dr["REVISION_NO"]) > 0)
    //                    revisionNo = Convert.ToInt32(dr["REVISION_NO"]);

    //                if (dr["CATEGORY_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CATEGORY_ID"])))
    //                    categoryID = Convert.ToString(dr["CATEGORY_ID"]);

    //                if (dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] != DBNull.Value && Convert.ToInt32(dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
    //                    isPartOfProductionStatusID = Convert.ToInt32(dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);


    //                if (dr["IS_REVISED"] != DBNull.Value && Convert.ToInt32(dr["IS_REVISED"]) > 0)
    //                    isRevised = Convert.ToInt32(dr["IS_REVISED"]);

    //                if (dr["SI_ATTACHMENT1_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT1_NAME"])))
    //                    subitemAttachment1File = Convert.ToString(dr["SI_ATTACHMENT1_NAME"]);

    //                if (dr["SI_ATTACHMENT1_BTYTES"] != DBNull.Value)
    //                    subitemAttachment1FileBytes = (byte[])dr["SI_ATTACHMENT1_BTYTES"];


    //                if (dr["SI_ATTACHMENT2_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT2_NAME"])))
    //                    subitemAttachment2File = Convert.ToString(dr["SI_ATTACHMENT2_NAME"]);

    //                if (dr["SI_ATTACHMENT2_BTYTES"] != DBNull.Value)
    //                    subitemAttachment2FileBytes = (byte[])dr["SI_ATTACHMENT2_BTYTES"];


    //                if (dr["SI_ATTACHMENT3_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT3_NAME"])))
    //                    subitemAttachment3File = Convert.ToString(dr["SI_ATTACHMENT3_NAME"]);

    //                if (dr["SI_ATTACHMENT3_BTYTES"] != DBNull.Value)
    //                    subitemAttachment3FileBytes = (byte[])dr["SI_ATTACHMENT3_BTYTES"];


    //                if (dr["SI_ATTACHMENT4_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT4_NAME"])))
    //                    subitemAttachment4File = Convert.ToString(dr["SI_ATTACHMENT4_NAME"]);

    //                if (dr["SI_ATTACHMENT4_BTYTES"] != DBNull.Value)
    //                    subitemAttachment4FileBytes = (byte[])dr["SI_ATTACHMENT4_BTYTES"];



    //                if (LOTTFSubitemID > 0)
    //                {
    //                    if (isRevised > 0)
    //                    {
    //                        LOTTFsubitemIDsToVoid += LOTTFSubitemID + ",";

    //                        DataRow drn = dtSubitemToAdd.NewRow();

    //                        if (statusID > 0)
    //                            drn["STATUS_ID"] = statusID;
    //                        else drn["STATUS_ID"] = 0;

    //                        if (!string.IsNullOrEmpty(subitemDesc))
    //                            drn["SUBITEM_DESC"] = subitemDesc;
    //                        else drn["SUBITEM_DESC"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(tagNo))
    //                            drn["TAG_NO"] = tagNo;
    //                        else drn["TAG_NO"] = string.Empty;

    //                        if (LOTMainSubItemID > 0)
    //                            drn["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
    //                        else drn["LOT_MAIN_SUBITEM_ID"] = 0;

    //                        if (!string.IsNullOrEmpty(drgNo))
    //                            drn["DRAWING_NO"] = drgNo;
    //                        else drn["DRAWING_NO"] = string.Empty;

    //                        if (revisionNo > 0)
    //                            drn["REVISION_NO"] = revisionNo;
    //                        else drn["REVISION_NO"] = 0;

    //                        if (!string.IsNullOrEmpty(categoryID))
    //                            drn["CATEGORY_ID"] = categoryID;
    //                        else drn["CATEGORY_ID"] = string.Empty;

    //                        if (quantity > 0)
    //                            drn["QUANTITY"] = quantity;
    //                        else drn["QUANTITY"] = 0;

    //                        if (!string.IsNullOrEmpty(productionOrderNo))
    //                            drn["PRODUCTION_ORDER_NO"] = productionOrderNo;
    //                        else drn["PRODUCTION_ORDER_NO"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(productionOrderDate))
    //                            drn["PRODUCTION_ORDER_DATE"] = Convert.ToDateTime(productionOrderDate).ToString("yyyy-MM-dd");
    //                        else drn["PRODUCTION_ORDER_DATE"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(expectedCompletionDate))
    //                            drn["EXPECTED_COMPLETION_DATE"] = Convert.ToDateTime(expectedCompletionDate).ToString("yyyy-MM-dd");
    //                        else drn["EXPECTED_COMPLETION_DATE"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(productCode))
    //                            drn["PRODUCT_CODE"] = productCode;
    //                        else drn["PRODUCT_CODE"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(productDesc))
    //                            drn["PRODUCT_DESC"] = productDesc;
    //                        else drn["PRODUCT_DESC"] = string.Empty;

    //                        if (!string.IsNullOrEmpty(UOM))
    //                            drn["UOM"] = UOM;
    //                        else drn["UOM"] = string.Empty;

    //                        if (isPartOfProductionStatusID > 0)
    //                            drn["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;
    //                        else drn["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = 0;

    //                        if (createdByID > 0)
    //                        {
    //                            drn["CREATED_BY"] = createdByID;
    //                            drn["CREATED_ON"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                        }
    //                        else
    //                        {
    //                            drn["CREATED_BY"] = 0;
    //                            drn["CREATED_ON"] = null;
    //                        }

    //                        if (approvedByID > 0)
    //                        {
    //                            drn["APPROVED_BY"] = approvedByID;
    //                            drn["APPROVED_ON"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                        }
    //                        else
    //                        {
    //                            drn["APPROVED_BY"] = 0;
    //                            drn["APPROVED_ON"] = null;
    //                        }

    //                        drn["APPROVED_REMARKS"] = string.Empty;

    //                        if (acceptedByID > 0)
    //                        {
    //                            drn["ACCEPTED_BY"] = acceptedByID;
    //                            drn["ACCEPTED_ON"] = DateTime.Now.ToString("yyyy-MM-dd");
    //                        }
    //                        else
    //                        {
    //                            drn["ACCEPTED_BY"] = 0;
    //                            drn["ACCEPTED_ON"] = null;
    //                        }

    //                        drn["ACCEPTED_REMARKS"] = string.Empty;


    //                        if (!string.IsNullOrEmpty(subitemAttachment1File))
    //                            drn["SI_ATTACHMENT1_NAME"] = subitemAttachment1File;
    //                        else drn["SI_ATTACHMENT1_NAME"] = string.Empty;

    //                        if (subitemAttachment1FileBytes != null)
    //                            drn["SI_ATTACHMENT1_BTYTES"] = subitemAttachment1FileBytes;
    //                        else drn["SI_ATTACHMENT1_BTYTES"] = null;




    //                        if (!string.IsNullOrEmpty(subitemAttachment2File))
    //                            drn["SI_ATTACHMENT2_NAME"] = subitemAttachment2File;
    //                        else drn["SI_ATTACHMENT2_NAME"] = string.Empty;

    //                        if (subitemAttachment2FileBytes != null)
    //                            drn["SI_ATTACHMENT2_BTYTES"] = subitemAttachment2FileBytes;
    //                        else drn["SI_ATTACHMENT2_BTYTES"] = null;




    //                        if (!string.IsNullOrEmpty(subitemAttachment3File))
    //                            drn["SI_ATTACHMENT3_NAME"] = subitemAttachment3File;
    //                        else drn["SI_ATTACHMENT3_NAME"] = string.Empty;

    //                        if (subitemAttachment3FileBytes != null)
    //                            drn["SI_ATTACHMENT3_BTYTES"] = subitemAttachment3FileBytes;
    //                        else drn["SI_ATTACHMENT3_BTYTES"] = null;



    //                        if (!string.IsNullOrEmpty(subitemAttachment4File))
    //                            drn["SI_ATTACHMENT4_NAME"] = subitemAttachment4File;
    //                        else drn["SI_ATTACHMENT4_NAME"] = string.Empty;

    //                        if (subitemAttachment4FileBytes != null)
    //                            drn["SI_ATTACHMENT4_BTYTES"] = subitemAttachment4FileBytes;
    //                        else drn["SI_ATTACHMENT4_BTYTES"] = null;

    //                        dtSubitemToAdd.Rows.Add(drn);
    //                    }
    //                }
    //                else
    //                {
    //                    DataRow drn = dtSubitemToAdd.NewRow();

    //                    if (statusID > 0)
    //                        drn["STATUS_ID"] = statusID;
    //                    else drn["STATUS_ID"] = 0;

    //                    if (!string.IsNullOrEmpty(subitemDesc))
    //                        drn["SUBITEM_DESC"] = subitemDesc;
    //                    else drn["SUBITEM_DESC"] = string.Empty;

    //                    if (!string.IsNullOrEmpty(tagNo))
    //                        drn["TAG_NO"] = tagNo;
    //                    else drn["TAG_NO"] = string.Empty;

    //                    if (LOTMainSubItemID > 0)
    //                        drn["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
    //                    else drn["LOT_MAIN_SUBITEM_ID"] = 0;

    //                    if (!string.IsNullOrEmpty(drgNo))
    //                        drn["DRAWING_NO"] = drgNo;
    //                    else drn["DRAWING_NO"] = string.Empty;

    //                    if (revisionNo > 0)
    //                        drn["REVISION_NO"] = revisionNo;
    //                    else drn["REVISION_NO"] = 0;

    //                    if (!string.IsNullOrEmpty(categoryID))
    //                        drn["CATEGORY_ID"] = categoryID;
    //                    else drn["CATEGORY_ID"] = string.Empty;

    //                    if (quantity > 0)
    //                        drn["QUANTITY"] = quantity;
    //                    else drn["QUANTITY"] = 0;


    //                    if (!string.IsNullOrEmpty(productionOrderNo))
    //                        drn["PRODUCTION_ORDER_NO"] = productionOrderNo;
    //                    else drn["PRODUCTION_ORDER_NO"] = string.Empty;

    //                    if (!string.IsNullOrEmpty(productionOrderDate))
    //                        drn["PRODUCTION_ORDER_DATE"] = Convert.ToDateTime(productionOrderDate).ToString("yyyy-MM-dd");
    //                    else drn["PRODUCTION_ORDER_DATE"] = string.Empty;

    //                    if (!string.IsNullOrEmpty(expectedCompletionDate))
    //                        drn["EXPECTED_COMPLETION_DATE"] = Convert.ToDateTime(expectedCompletionDate).ToString("yyyy-MM-dd");
    //                    else drn["EXPECTED_COMPLETION_DATE"] = string.Empty;

    //                    if (!string.IsNullOrEmpty(productCode))
    //                        drn["PRODUCT_CODE"] = productCode;
    //                    else drn["PRODUCT_CODE"] = string.Empty;

    //                    if (!string.IsNullOrEmpty(productDesc))
    //                        drn["PRODUCT_DESC"] = productDesc;
    //                    else drn["PRODUCT_DESC"] = string.Empty;

    //                    if (!string.IsNullOrEmpty(UOM))
    //                        drn["UOM"] = UOM;
    //                    else drn["UOM"] = string.Empty;

    //                    if (isPartOfProductionStatusID > 0)
    //                        drn["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;
    //                    else drn["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = 0;



    //                    if (createdByID > 0)
    //                    {
    //                        drn["CREATED_BY"] = createdByID;
    //                        drn["CREATED_ON"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                    }
    //                    else
    //                    {
    //                        drn["CREATED_BY"] = 0;
    //                        drn["CREATED_ON"] = string.Empty;
    //                    }

    //                    if (approvedByID > 0)
    //                    {
    //                        drn["APPROVED_BY"] = approvedByID;
    //                        drn["APPROVED_ON"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                    }
    //                    else
    //                    {
    //                        drn["APPROVED_BY"] = 0;
    //                        drn["APPROVED_ON"] = string.Empty;
    //                    }

    //                    drn["APPROVED_REMARKS"] = string.Empty;

    //                    if (acceptedByID > 0)
    //                    {
    //                        drn["ACCEPTED_BY"] = acceptedByID;
    //                        drn["ACCEPTED_ON"] = DateTime.Now.ToString("yyyy-MM-dd");
    //                    }
    //                    else
    //                    {
    //                        drn["ACCEPTED_BY"] = 0;
    //                        drn["ACCEPTED_ON"] = string.Empty;
    //                    }

    //                    drn["ACCEPTED_REMARKS"] = string.Empty;


    //                    if (!string.IsNullOrEmpty(subitemAttachment1File))
    //                        drn["SI_ATTACHMENT1_NAME"] = subitemAttachment1File;
    //                    else drn["SI_ATTACHMENT1_NAME"] = string.Empty;

    //                    if (subitemAttachment1FileBytes != null)
    //                        drn["SI_ATTACHMENT1_BTYTES"] = subitemAttachment1FileBytes;
    //                    else drn["SI_ATTACHMENT1_BTYTES"] = null;



    //                    if (!string.IsNullOrEmpty(subitemAttachment2File))
    //                        drn["SI_ATTACHMENT2_NAME"] = subitemAttachment2File;
    //                    else drn["SI_ATTACHMENT2_NAME"] = string.Empty;

    //                    if (subitemAttachment2FileBytes != null)
    //                        drn["SI_ATTACHMENT2_BTYTES"] = subitemAttachment2FileBytes;
    //                    else drn["SI_ATTACHMENT2_BTYTES"] = null;



    //                    if (!string.IsNullOrEmpty(subitemAttachment3File))
    //                        drn["SI_ATTACHMENT3_NAME"] = subitemAttachment3File;
    //                    else drn["SI_ATTACHMENT3_NAME"] = string.Empty;

    //                    if (subitemAttachment3FileBytes != null)
    //                        drn["SI_ATTACHMENT3_BTYTES"] = subitemAttachment3FileBytes;
    //                    else drn["SI_ATTACHMENT3_BTYTES"] = null;



    //                    if (!string.IsNullOrEmpty(subitemAttachment4File))
    //                        drn["SI_ATTACHMENT4_NAME"] = subitemAttachment4File;
    //                    else drn["SI_ATTACHMENT4_NAME"] = string.Empty;

    //                    if (subitemAttachment4FileBytes != null)
    //                        drn["SI_ATTACHMENT4_BTYTES"] = subitemAttachment4FileBytes;
    //                    else drn["SI_ATTACHMENT4_BTYTES"] = null;

    //                    dtSubitemToAdd.Rows.Add(drn);

    //                }


    //            }
    //        }
    //        else
    //        {
    //            mpeReviseLOT.Show();
    //            ExceptionMessageRevision("Please revise atleast 1 subitem...!!!");
    //            return;
    //        }

    //        bool checkForTAGNO = true;
    //        int isPartOfProductionCount = 0;

    //        LOTMainSubItemIDs = string.Empty;

    //        if (dtSubitemToAdd.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dtSubitemToAdd.Rows)
    //            {
    //                if (!LOTMainSubItemIDs.Contains("," + Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"]) + ","))
    //                {
    //                    LOTMainSubItemIDs += Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"]) + ",";
    //                }
    //            }
    //        }

    //        int value = 0;
    //        if (dtSubitemToAdd.Rows.Count > 0)
    //        {

    //            foreach (GridViewRow gr in gvSubItem.Rows)
    //            {
    //                TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;

    //                if (string.IsNullOrEmpty(txtTagNoInList.Text.Trim()))
    //                {
    //                    checkForTAGNO = false;
    //                    break;
    //                }
    //            }

    //            //foreach (GridViewRow gr in gvSubItem.Rows)
    //            //{
    //            //    CheckBox chkIsPartOfProductStatusReport = gr.FindControl("chkIsPartOfProductStatusReport") as CheckBox;
    //            //    if (chkIsPartOfProductStatusReport.Checked)
    //            //    {
    //            //        isPartOfProductionCount++;
    //            //    }
    //            //}


    //            if (!string.IsNullOrEmpty(hdRemovedSubitemIDs.Value))
    //                LOTTFsubitemIDsToVoid += hdRemovedSubitemIDs.Value;


    //            if (!string.IsNullOrEmpty(LOTTFsubitemIDsToVoid))
    //                LOTTFsubitemIDsToVoid = LOTTFsubitemIDsToVoid.TrimEnd(',');

    //            if (checkForTAGNO == true)
    //            {
    //                if (LOTTFID > 0)
    //                {
    //                    value = objProject.TransferOfLOT(LOTTFID, TFNo, companyID, LOTDate, custCode, jobNo, poNo, itemName, impNotes,
    //                                                                attachment1File, attachment1FileBytes,
    //                                                                attachment2File, attachment2FileBytes,
    //                                                                attachment3File, attachment3FileBytes,
    //                                                                attachment4File, attachment4FileBytes,
    //                                                                dtSubitemToAdd, LOTTFsubitemIDsToVoid, remarks, createdByID);
    //                }
    //                else
    //                {
    //                    ExceptionMessageRevision("Please try again...!!!");
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                mpeReviseLOT.Show();

    //                if (!checkForTAGNO)
    //                {
    //                    ExceptionMessageRevision("TAG number is empty in subitem list...!!!");
    //                    return;
    //                }

    //                //if (isPartOfProductionCount == 0)
    //                //{
    //                //    ExceptionMessageRevision("Please select atleast 1 subitem as part of production in subitem list...!!!");
    //                //    return;
    //                //}
    //            }
    //        }
    //        else
    //        {
    //            mpeReviseLOT.Show();
    //            ExceptionMessageRevision("Please select atleast 1 subitem...!!");
    //        }

    //        if (value > 0)
    //        {
    //            if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
    //                LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');

    //            //int sendMailValue = SendEmail(value);

    //            //int sendMailValue = objLOTSendMail.SendEmailLOT(value, LOTMainSubItemIDs, txtTFNoToEdit.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", 0, 0, 0, 0, null);
    //            //int sendMailValue = objLOTSendMail.SendEmailLOT(value, "", txtTFNoToEdit.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", 0, 0, 0, 0, null, null, 0, 0);

    //            int sendMailValue = 0;// objLOTSendMail.SendEmailLOT(value, "", txtTFNoToEdit.Text, companyID, "", 0, 0, null, 0, 0);
    //            if (sendMailValue > 0)
    //            {
    //                int val = objProject.UpdateLOTMailStatusTwo(value, LOTMainSubItemIDs, statusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));

    //                SuccessMessageReviseList("LOT Transferred with TF. No.: '" + TFNo + "' and mail sent successfully.");
    //                Reset();
    //            }
    //            else
    //            {
    //                SuccessMessageReviseList("LOT Transferred with TF. No.: '" + TFNo + "' successfully.");
    //                Reset();
    //            }

    //            GetLOTList();
    //        }



    //        #endregion

    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessageRevision(ex.ToString());
    //        return;
    //    }
    //}


    private int GetStatusID(int createdByID, int PEID, int PMID, int prodMngrID)
    {
        try
        {
            statusID = 0;
            //Created by any of Project Engineer or Project Manager or Production Manager
            if (createdByID == PEID || createdByID == PMID || createdByID == prodMngrID)
            {
                if (createdByID == PEID || createdByID == PMID)
                {
                    statusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                }

                else if (createdByID == prodMngrID)
                {
                    if ((PEID == PMID && PMID == prodMngrID) ||
                        (PEID != PMID && PMID == prodMngrID) ||
                        (PEID != PMID && PEID == prodMngrID))
                    {
                        statusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved);
                    }
                    else if ((PEID == PMID && PMID != prodMngrID) ||
                             (PEID != PMID && PMID != prodMngrID))
                    {
                        statusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
                    }
                }
            }

            //Created by an individual none of Project Engineer or Project Manager or Production Manager
            else
            {
                statusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New);
            }

            return statusID;

        }
        catch (Exception)
        {
            return 0;
        }
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
                ExceptionMessageRevision("File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }

    //private void ViewDrawingFiles(int srNo, string fileName, string fileType)
    //{
    //    try
    //    {
    //        byte[] fileBytes = null;
    //        string extn = string.Empty;
    //        if (!string.IsNullOrEmpty(fileName))
    //        {
    //            foreach (DataRow dr in dtTemp.Select("SR_NO='" + srNo + "'"))
    //            {
    //                fileBytes = (byte[])dr["SI_ATTACHMENT1_BTYTES"];
    //            }

    //            extn = fileName.Split('.').Last();
    //            if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
    //            {
    //                imgFile.ImageUrl = "ViewAttachedDrawingImageFile.ashx?srNo=" + srNo + "&fileType=" + fileType;
    //                mpeShowImageFile.Show();
    //            }
    //            else if (extn == "pdf" || extn == "PDF")
    //            {
    //                iframeViewPDFFile.Attributes.Add("src", "ViewAttachedDrawingPDFFile.aspx?srNo=" + srNo + "&fileType=" + fileType);
    //                this.mpeShowPDFFile.Show();
    //            }
    //        }
    //        else
    //        {
    //            ExceptionMessageRevision("File Doesn't exist!");
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void Reset()
    {
        try
        {
            //ddlLOTMainItems.SelectedIndex = 0;
            //ddlLOTMainSubitemsToEdit.SelectedIndex = 0;
            txtCustomerCodeToEdit.Text = string.Empty;
            txtCustomerNameToEdit.Text = string.Empty;

            txtDateToEdit.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            txtJOBNoToEdit.Text = string.Empty;
            txtPONoToEdit.Text = string.Empty;
            txtItemNameToEdit.Text = string.Empty;
            //txtTFNoToEdit.Text = string.Empty;
            //txtNotesToEdit.Text = string.Empty;
            txtRemarksToEdit.Text = string.Empty;

            Session["dtSubitem"] = null;



            dtTemp.Clear();
            dtSubitem.Clear();
            gvSubItem.DataSource = null;
            gvSubItem.DataBind();


            Session["dtAttachments"] = null;
            dtTempAttachments.Clear();
            gvAttachments.DataSource = null;
            gvAttachments.DataBind();


            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
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
        //chkIsPartOfProductionOrMainDrawingToEdit.Checked = false;

        //ddlLOTMainItemsToEdit.SelectedIndex = 0;

        //ddlLOTMainSubitemsToEdit.Items.Clear();
        //ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
        //ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

        txtDescriptionToEdit.Text = string.Empty;
        //txtDrgNoToEdit.Text = string.Empty;
        //txtRevNo.Text = string.Empty;
        //foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryToEdit.Items)
        //{
        //    item.Selected = false;
        //}
        hdQuantityToEdit.Value = "0";
        txtQuantityToEdit.Text = string.Empty;
        txtTagNoToEdit.Text = string.Empty;
        txtProductionOrderNoToEdit.Text = string.Empty;
        txtExpectedCompletionDateToEdit.Text = string.Empty;
    }

    private void SuccessMessageRevision(string message)
    {
        pnlReviseMsg.Visible = true;
        lblReviseMsg.Text = message;
        lblReviseMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessageRevision(string message)
    {
        pnlReviseMsg.Visible = true;
        lblReviseMsg.Text = message;
        lblReviseMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanelRevision()
    {
        pnlReviseMsg.Visible = false;
        lblReviseMsg.Text = string.Empty;
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


    #endregion REVISIE LOT END[=============================]


}