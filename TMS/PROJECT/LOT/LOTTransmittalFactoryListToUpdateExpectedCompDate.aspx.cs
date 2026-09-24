using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;

public partial class PROJECT_LOT_LOTTransmittalFactoryListToUpdateExpectedCompDate : System.Web.UI.Page
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
    string productionOrderNumber = string.Empty;

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
    int amendedByID = 0;
    int amendmentCount = 0;
    DataTable dtProdMngr = new DataTable();
    DataTable dtTempAttachments = new DataTable();

    #endregion


    #region EVENTS START[==================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanelReviseList();
            HidePanelRevision();

            if (!IsPostBack)
            {
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

                //GetLOTList();
            }
        }
        else
        {
            Session["dsTravelStatementDetails"] = null;
            Response.Redirect("~/Login.aspx");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetLOTList();
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
                ViewState["PE_ID"] = Convert.ToInt32(lblJobPEID.Text);
                ViewState["PM_ID"] = Convert.ToInt32(lblJobPMID.Text);



                if (Convert.ToString(e.CommandArgument) == "REVISE")
                {
                    Reset();

                    this.mpeReviseLOT.Show();

                    hdNewExpectedCompletionDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtNewExpectedCompletionDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtProductionOrderNumberToEdit.Text = txtProductionOrderNumber.Text.ToUpper();

                    BindLOTTFDetailsToEdit(LOTTFID);
                    this.mpeReviseLOT.Show();
                }


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
                        if (Convert.ToInt32(dr0["PE_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                            Convert.ToInt32(dr0["PM_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
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


                ViewState["LOTTFID"] = lblLOTTFID.Text;

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


    protected void btnUpdateExpectedCompletionDate_Click(object sender, EventArgs e)
    {
        UpdateExpectedCompletionDate();
    }

    #endregion


    #region METHODS START[=================]


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
            productionOrderNumber = string.Empty;

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

            if (!string.IsNullOrEmpty(txtProductionOrderNumber.Text))
                productionOrderNumber = txtProductionOrderNumber.Text;

            dsLOTList = objProject.GetLOTTFListExpectedCompletionDateUpdation(startDate, endDate, LOTTFNo, statusID, unitID, jobNo, customerName, LOTMainItemID,
                                                            LOTMainSubitemID, tagNumber, drawingNumber, productionOrderNumber);

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
                //mpeAddSubitems.Show();
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


    private void BindLOTTFDetailsToEdit(int LOTTFID)
    {
        try
        {
            amendmentCount = 0;

            dsLOTTFDetails = objProject.GetLOTTFDetailsForRevision(LOTTFID);
            if (dsLOTTFDetails.Tables.Count > 0)
            {
                if (dsLOTTFDetails.Tables[0].Rows.Count > 0)
                {
                    if (dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"])))
                        lblTFNo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"])))
                        txtOldTFNoToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);

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
                        txtDateToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"])))
                        txtItemNameToEdit.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"]);


                }


                bool isNull = false;
                if (dsLOTTFDetails.Tables[1].Rows.Count > 0)
                {
                    DataTable dtS = new DataTable();
                    dtS = dsLOTTFDetails.Tables[1].Clone();

                    if (!string.IsNullOrEmpty(txtProductionOrderNumberToEdit.Text))
                    {
                        foreach (DataRow dr in dsLOTTFDetails.Tables[1].Select("PRODUCTION_ORDER_NO='" + txtProductionOrderNumberToEdit.Text + "'"))
                        {
                            dtS.ImportRow(dr);
                        }

                        if (dtS.Rows.Count > 0)
                        {
                            gvSubItem.DataSource = dtS;
                            gvSubItem.DataBind();
                            Session["dtSubitem"] = dtS;
                        }
                        else isNull = true;
                    }
                    else isNull = true;
                }
                else isNull = true;


                if (isNull)
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


    private void UpdateExpectedCompletionDate()
    {
        try
        {
            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            string LOTTFSubitemIDs = string.Empty;
            string LOTMainSubitemIDs = string.Empty;

            string productionOrderNo = string.Empty;
            string expectedCompletionDate = string.Empty;
            string expectedCompletionDateRemarks = string.Empty;


            if (gvSubItem.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItem.Rows)
                {
                    Label lblLOTTFMainSubitemID = gr.FindControl("lblLOTTFMainSubitemID") as Label;
                    Label lblLOTMainSubitemID = gr.FindControl("lblLOTMainSubitemID") as Label;

                    LOTTFSubitemIDs += lblLOTTFMainSubitemID.Text + ",";
                    LOTMainSubitemIDs += lblLOTMainSubitemID.Text + ",";
                }
            }

            if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

            if (!string.IsNullOrEmpty(LOTMainSubitemIDs))
                LOTMainSubitemIDs = LOTMainSubitemIDs.TrimEnd(',');

            productionOrderNo = txtProductionOrderNumberToEdit.Text.ToUpper();
            expectedCompletionDate = Convert.ToDateTime(hdNewExpectedCompletionDate.Value).ToString("yyyy-MM-dd");
            expectedCompletionDateRemarks = txtRemarksToEdit.Text;


            if (!string.IsNullOrEmpty(LOTTFSubitemIDs) && !string.IsNullOrEmpty(productionOrderNo))
            {
                int val = objProject.UpdateExpectedCompletionDate(LOTTFSubitemIDs, productionOrderNo, expectedCompletionDate
                                                              , expectedCompletionDateRemarks, createdBy);

                int sendMailVal = 0;
                if (val > 0)
                {
                    sendMailVal = SendUpdatedExpectedCompletionDateEmail(Convert.ToInt32(ViewState["LOTTFID"]), Convert.ToString(ViewState["TF_NO"]), productionOrderNo, expectedCompletionDate);
                    if (sendMailVal > 0)
                    {
                        SuccessMessageRevision("Expected completion date updated and mail sent successfully...!!!");
                        objProject.UpdateExpectedCompletionDateMailStatus(LOTTFSubitemIDs, productionOrderNo, createdBy);
                    }
                    else
                    {
                        SuccessMessageRevision("Expected completion date updated successfully...!!!");
                    }

                    BindLOTTFDetailsToEdit(Convert.ToInt32(ViewState["LOTTFID"]));
                    mpeReviseLOT.Show();
                }
                else
                {
                    ExceptionMessageReviseList("Please try again....!!!");
                    return;
                }
            }
            else
            {
                ExceptionMessageReviseList("No subitem and production order found, please try again....!!!");
                return;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
            return;
        }
    }


    private int SendUpdatedExpectedCompletionDateEmail(int LOTTFID, string tfNo, string productionOrderNumber, string expectedCompletionDate)
    {
        try
        {
            expectedCompletionDate = Convert.ToDateTime(expectedCompletionDate).ToString("dd-MMM-yyyy");
            int returnVal = 0;
            DataSet dsMailInfo = new DataSet();
            dsMailInfo = objProject.GetUpdatedExpCompDateMailInfo(LOTTFID, productionOrderNumber);

            if (dsMailInfo.Tables.Count > 0 && dsMailInfo.Tables[0].Rows.Count > 0)
            {
                string fileName = "~/PROJECT/LOT/EMAIL_FORMATS/21UpdatedExpectedCompletionDateMail.htm";

                string subject = string.Empty;
                string from = string.Empty;
                string fromName = string.Empty;
                string to = string.Empty;
                string cc = string.Empty;
                string bcc = string.Empty;

                DataRow dr0 = dsMailInfo.Tables[0].Rows[0];

                fromName = Convert.ToString(dr0["EXPECTED_COMPLETION_DATE_UPDATED_BY"]);
                from = Convert.ToString(dr0["EXPECTED_COMPLETION_DATE_UPDATED_BY_EMAIL_ID"]);



                if (dsMailInfo.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsMailInfo.Tables[1].Rows)
                    {
                        to += Convert.ToString(dr1["PM_EMAIL"]) + ";" + Convert.ToString(dr1["PE_EMAIL"]);
                    }
                }


                cc = Convert.ToString(dr0["CREATED_BY_EMAIL_ID"]);


                if (dsMailInfo.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr2 in dsMailInfo.Tables[2].Rows)
                    {
                        cc += ";" + Convert.ToString(dr2["PLANNING_MNGR_EMAIL"]) + ";";
                    }
                }


                if (dsMailInfo.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr3 in dsMailInfo.Tables[3].Rows)
                    {
                        cc += ";" + Convert.ToString(dr3["PROD_MNGR_EAMIL"]) + ";";
                    }
                }


                //if (dsMailInfo.Tables[4].Rows.Count > 0)
                //{
                //    foreach (DataRow dr4 in dsMailInfo.Tables[4].Rows)
                //    {
                //        to += Convert.ToString(dr4["QUALITY_MNGR_EAMIL"]) + ";";
                //    }
                //}



                if (!string.IsNullOrEmpty(to))
                    to = to.TrimEnd(';');

                if (!string.IsNullOrEmpty(cc))
                {
                    cc = cc.TrimStart(';');
                    cc = cc.TrimEnd(';');
                }


                subject = "Updated Expected Completion Date: " + expectedCompletionDate;

                returnVal = SendUpdatedExpectedCompletionDateMail(tfNo, productionOrderNumber, expectedCompletionDate, from, fromName, to, cc, bcc, subject, fileName);
            }

            return returnVal;
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return 0;
        }
    }

    private int SendUpdatedExpectedCompletionDateMail(string TFNo, string productionOrderNumber, string expectedCompletionDate, string from, string fromName, string to,
                                              string cc, string bcc, string subject, string fileName)
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

        body = body.Replace("{#lot#}", TFNo);
        body = body.Replace("{#productionOrderNumber#}", productionOrderNumber);
        body = body.Replace("{#expectedCompletionDate#}", expectedCompletionDate);
        body = body.Replace("{#fromname#}", fromName);

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

        return returnVal;

    }





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
            txtRemarksToEdit.Text = string.Empty;

            Session["dtSubitem"] = null;



            dtTemp.Clear();
            dtSubitem.Clear();
            gvSubItem.DataSource = null;
            gvSubItem.DataBind();


            Session["dtAttachments"] = null;
            dtTempAttachments.Clear();
        }
        catch (Exception ex)
        {
            ExceptionMessageRevision(ex.ToString());
            return;
        }
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


    #endregion

}