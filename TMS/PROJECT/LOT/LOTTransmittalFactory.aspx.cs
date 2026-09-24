using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PROJECT_LOT_LOTTransmittalFactory : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    LOTSendMail objLOTSendMail = new LOTSendMail();
    GetLOTMailTypeAndStatus objGetLOTMailTypeAndStatus = new GetLOTMailTypeAndStatus();
    LOTMailTypeAndStatusProperties objLOTMailTypeAndStatusProperties = new LOTMailTypeAndStatusProperties();

    GetLOTApproverStatus objGetLOTApproverStatus = new GetLOTApproverStatus();
    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();


    DataSet dsUnit = new DataSet();
    DataSet dsJobNo = new DataSet();
    DataSet dsProductionOrderNo = new DataSet();
    DataSet dsDMSDrawingNo = new DataSet();
    DataTable dtTemp = new DataTable();
    DataTable dtTempAttachments = new DataTable();
    DataTable dtSubitem = new DataTable();
    DataTable dtAttachments = new DataTable();
    DataTable dtSubitemToAdd = new DataTable();
    DataSet dsLOTMainItems = new DataSet();
    DataSet dsLOTMainSubItems = new DataSet();
    DataSet dsLOTCategoryForFactory = new DataSet();
    DataSet dsJobApprovers = new DataSet();

    Byte[] attachment1FileBytes = null;
    Byte[] attachment2FileBytes = null;
    Byte[] attachment3FileBytes = null;
    Byte[] attachment4FileBytes = null;

    string attachment1File = string.Empty;
    string attachment2File = string.Empty;
    string attachment3File = string.Empty;
    string attachment4File = string.Empty;

    int runningNo = 0;
    string runningNoTxt = string.Empty;
    string TFNo = string.Empty;
    int companyID = 0;
    string companyName = string.Empty;
    string LOTDate = string.Empty;
    string custCode = string.Empty;
    string jobNo = string.Empty;
    string poNo = string.Empty;
    string itemName = string.Empty;
    string impNotes = string.Empty;

    int statusID = 0;

    string productionOrderNo = string.Empty;
    string expectedCompletionDate = string.Empty;
    string subitemDesc = string.Empty;
    string tagNo = string.Empty;

    string LOTMainItem = string.Empty;
    string LOTMainSubItem = string.Empty;
    int LOTMainItemID = 0;
    int LOTMainSubItemID = 0;
    string LOTMainSubItemIDs = string.Empty;

    string drgNo = string.Empty;
    int revisionNo = 0;
    string revisionNoText = string.Empty;
    int quantity = 0;

    string productionOrderDate = string.Empty;
    string productCode = string.Empty;
    string productDesc = string.Empty;
    string UOM = string.Empty;
    string isPartOfProductionStatus = string.Empty;
    int isPartOfProductionStatusID = 0;

    string categoryID = string.Empty;
    string category = string.Empty;

    int createdByID = 0;
    int amendedByID = 0;
    int PEID = 0;
    int PMID = 0;
    int approvedByID = 0;
    int amendedApprovedByID = 0;
    int prodMngrID = 0;
    int acceptedByID = 0;
    int amendmentCount = 0;

    string SiDrawing1File = string.Empty;
    string SiDrawing2File = string.Empty;
    string SiDrawing3File = string.Empty;
    string SiDrawing4File = string.Empty;

    Byte[] SiDrawing1FileBytes = null;
    Byte[] SiDrawing2FileBytes = null;
    Byte[] SiDrawing3FileBytes = null;
    Byte[] SiDrawing4FileBytes = null;


    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            Session["dtLOT"] = null;
            Session["dtSubitems"] = null;
            Session["dtQuantityDetails"] = null;
            if (!IsPostBack)
            {
                Session["dtLOT"] = null;
                Session["dtSubitems"] = null;
                Session["dtQuantityDetails"] = null;
                Session["dtProdDetail"] = null;

                AddTempSubitemTable();

                hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDate.Text = hdDate.Value;

                BindCompany();
                BindLOTMainItems();


                ddlLOTMainSubItems.Items.Clear();
                ddlLOTMainSubItems.Items.Insert(0, "Select");
                ddlLOTMainSubItems.SelectedIndex = 0;

                BindLOTCategoryForFactory();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlLOTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();

        if (ddlLOTType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlLOTType.SelectedValue) == 1)
            {

            }
            else if (Convert.ToInt32(ddlLOTType.SelectedValue) == 2)
            {
                Response.Redirect("~/PROJECT/LOT/LOTTransmittalFactoryRevision.aspx");
            }
            else if (Convert.ToInt32(ddlLOTType.SelectedValue) == 3)
            {

            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }

    protected void ddlLOTMainItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        mpeAddSubitems.Show();
        BindLOTMainSubItems();
    }




    // JOB DETAILS
    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        mpeJOBDetail.Show();
        GetJOBDetail();
    }

    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        mpeJOBDetail.Show();
        GetJOBDetail();
    }

    protected void gvJOBDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                btnAddApprover.Visible = false;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblAppJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblAppJOBNo") as Label;
                Label lblPONo = gvJOBDetail.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblCustomerName = gvJOBDetail.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblCustCode = gvJOBDetail.Rows[rowindex].FindControl("lblCustCode") as Label;

                txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
                txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                txtCustomerCode.Text = Convert.ToString(lblCustCode.Text).Trim();
                txtPONo.Text = Convert.ToString(lblPONo.Text).Trim();


                GetTFNo();

                if (string.IsNullOrEmpty(Convert.ToString(lblAppJOBNo.Text)))
                {
                    mpeAddApprovers.Show();
                    iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0&unitid=" + Convert.ToString(ddlCompany.SelectedValue) + "");

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

    protected void btnGetTFno_Click(object sender, EventArgs e)
    {
        GetTFNo();
    }




    // PRODUCTION ORDER NO DETAILS
    protected void btnGetProductionNumber_Click(object sender, EventArgs e)
    {

        HideAddSubitemPanel();
        //btnAddUpdateSubitemToList.Enabled = false;
        //txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightYellow;


        txtJOBNoSearchPON.Text = txtJOBNo.Text;
        txtProductionOrderNoSearchPON.Text = string.Empty;
        mpeProductonOrderNoDetail.Show();
        mpeAddSubitems.Show();
        GetProductionOrderNoDetail();
    }

    protected void btnSearchProductionOrderNo_Click(object sender, EventArgs e)
    {
        mpeProductonOrderNoDetail.Show();
        GetProductionOrderNoDetail();
    }

    protected void gvProductonOrderNoDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                txtUOM.Text = string.Empty;
                txtProductDesc.Text = string.Empty;
                txtExpectedCompletionDate.Text = string.Empty;
                txtQuantity.Text = string.Empty;

                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblProductionOrderNo = gvProductonOrderNoDetail.Rows[rowindex].FindControl("lblProductionOrderNo") as Label;
                Label lblProductionOrderDate = gvProductonOrderNoDetail.Rows[rowindex].FindControl("lblProductionOrderDate") as Label;
                //Label lblExpectedCompletionDate = gvProductonOrderNoDetail.Rows[rowindex].FindControl("lblExpectedCompletionDate") as Label;

                txtProductionOrderNo.Text = Convert.ToString(lblProductionOrderNo.Text).Trim();
                txtProductionOrderDate.Text = Convert.ToString(lblProductionOrderDate.Text);
                //txtExpectedCompletionDate.Text = Convert.ToString(lblExpectedCompletionDate.Text);

                txtUOM.Text = string.Empty;
                txtProductDesc.Text = string.Empty;
                txtQuantity.Text = string.Empty;

                BindProductDetail(Convert.ToString(lblProductionOrderNo.Text));

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

    protected void ddlProductCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideAddSubitemPanel();
        //btnAddUpdateSubitemToList.Enabled = false;
        //txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightYellow;
        mpeAddSubitems.Show();

        txtUOM.Text = string.Empty;
        txtProductDesc.Text = string.Empty;
        txtExpectedCompletionDate.Text = string.Empty;
        txtQuantity.Text = string.Empty;

        if (ddlProductCode.SelectedIndex > 0)
        {
            DataTable dt = new DataTable();
            if (Session["dtProdDetail"] != null)
                dt = (DataTable)Session["dtProdDetail"];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Select("PRODUCT_CODE='" + Convert.ToString(ddlProductCode.SelectedValue) + "'"))
                {
                    if (dr["UOM"] != DBNull.Value)
                        txtUOM.Text = Convert.ToString(dr["UOM"]);
                    else txtUOM.Text = string.Empty;

                    if (dr["PRODUCT_DESC"] != DBNull.Value)
                        txtProductDesc.Text = Convert.ToString(dr["PRODUCT_DESC"]);
                    else txtProductDesc.Text = string.Empty;

                    if (dr["EDDATE"] != DBNull.Value)
                    {
                        txtExpectedCompletionDate.Text = Convert.ToString(dr["EDDATE"]);

                        //CheckForExpectedCompletionDate(txtExpectedCompletionDate.Text);
                    }
                    else txtExpectedCompletionDate.Text = string.Empty;

                    if (dr["QUANTITY"] != DBNull.Value)
                        txtQuantity.Text = Convert.ToString(dr["QUANTITY"]);
                    else txtQuantity.Text = string.Empty;
                }
            }
        }
    }



    // DMS DRAWING NO DETAILS
    protected void btnGetDMSDrawingNo_Click(object sender, EventArgs e)
    {
        txtJOBNoSearchDMS.Text = txtJOBNo.Text;
        mpeDMSDrawingNoList.Show();
        mpeAddSubitems.Show();
        GetDMSDrawingList();
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
                txtDrgNo.Text = string.Empty;

                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblDrawingNo = gvDMSDrawingNoList.Rows[rowindex].FindControl("lblDrawingNo") as Label;

                txtDrgNo.Text = lblDrawingNo.Text.Trim().ToUpper();
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



    // ADD JOB APPROVERS
    protected void btnAddApprover_Click(object sender, EventArgs e)
    {
        mpeAddApprovers.Show();
        iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0&unitid=" + Convert.ToString(ddlCompany.SelectedValue) + "");
    }




    //ADD UPDATE SUBITEMS
    protected void btnAddSubitem_Click(object sender, EventArgs e)
    {
        imgBtnUndoSiDrawing1.Visible = false;
        imgBtnUndoSiDrawing2.Visible = false;
        imgBtnUndoSiDrawing3.Visible = false;
        imgBtnUndoSiDrawing4.Visible = false;

        pnlViewSiDrawing1.Visible = false;
        pnlViewSiDrawing2.Visible = false;
        pnlViewSiDrawing3.Visible = false;
        pnlViewSiDrawing4.Visible = false;

        pnlUploadSiDrawing1.Visible = true;
        pnlUploadSiDrawing2.Visible = true;
        pnlUploadSiDrawing3.Visible = true;
        pnlUploadSiDrawing4.Visible = true;

        //CheckMultipleSubitems();       

        //Added on 2022-05-30
        HideAddSubitemPanel();
        //txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightYellow;
        //btnAddUpdateSubitemToList.Enabled = false;
        //-----------



        btnAddUpdateSubitemToList.Text = "Add Subitem";
        hdSubitemUpdationFlag.Value = "0";




        //EnableControls();
        ResetSubitems();


        mpeAddSubitems.Show();
    }

    protected void btnAddUpdateSubitemToList_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdSubitemUpdationFlag.Value) == 0)
        {
            AddSubitems();
        }
        else
        {
            UpdateSubitems();
        }
    }

    protected void gvSubItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblSrNo = gvSubItem.Rows[rowindex].FindControl("lblSrNo") as Label;
                Label lblSUID = gvSubItem.Rows[rowindex].FindControl("lblSUID") as Label;

                Label lblProductionNumber = gvSubItem.Rows[rowindex].FindControl("lblProductionNumber") as Label;
                Label lblProductionOrderDate = gvSubItem.Rows[rowindex].FindControl("lblProductionOrderDate") as Label;
                Label lblExpectedCompletionDate = gvSubItem.Rows[rowindex].FindControl("lblExpectedCompletionDate") as Label;
                Label lblProductCode = gvSubItem.Rows[rowindex].FindControl("lblProductCode") as Label;
                Label lblProductDesc = gvSubItem.Rows[rowindex].FindControl("lblProductDesc") as Label;
                Label lblUOM = gvSubItem.Rows[rowindex].FindControl("lblUOM") as Label;
                Label lblIsPartOfProductionStatusReportID = gvSubItem.Rows[rowindex].FindControl("lblIsPartOfProductionStatusReportID") as Label;

                Label lblLOTMainItemID = gvSubItem.Rows[rowindex].FindControl("lblLOTMainItemID") as Label;
                Label lblLOTMainSubitemID = gvSubItem.Rows[rowindex].FindControl("lblLOTMainSubitemID") as Label;
                TextBox txtTagNoInList = gvSubItem.Rows[rowindex].FindControl("txtTagNoInList") as TextBox;
                Label lblDescription = gvSubItem.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblDrgOrDOCNo = gvSubItem.Rows[rowindex].FindControl("lblDrgOrDOCNo") as Label;
                Label lblRevNo = gvSubItem.Rows[rowindex].FindControl("lblRevNo") as Label;
                Label lblRevNoText = gvSubItem.Rows[rowindex].FindControl("lblRevNoText") as Label;
                TextBox txtRevNo = gvSubItem.Rows[rowindex].FindControl("txtRevNo") as TextBox;
                Label lblCategory = gvSubItem.Rows[rowindex].FindControl("lblCategory") as Label;
                Label lblCategoryID = gvSubItem.Rows[rowindex].FindControl("lblCategoryID") as Label;
                Label lblQuantity = gvSubItem.Rows[rowindex].FindControl("lblQuantity") as Label;


                ViewState["SR_NO"] = Convert.ToInt32(lblSrNo.Text);


                foreach (System.Web.UI.WebControls.ListItem item in chkLstCategory.Items)
                {
                    item.Selected = false;
                }


                hdRevNoText.Value = "";
                hdRevNoTextOld.Value = "";
                txtRevNoText.Enabled = false;

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {

                    hdRevNoText.Value = lblRevNoText.Text;
                    hdRevNoTextOld.Value = lblRevNoText.Text;

                    if (Convert.ToInt32(lblRevNo.Text) == 99)
                    {
                        txtRevNoText.Enabled = true;
                        txtRevNoText.Text = lblRevNoText.Text;
                    }
                    else
                    {
                        hdRevNoText.Value = Convert.ToString(lblRevNoText.Text);
                        txtRevNoText.Text = hdRevNoText.Value;
                    }


                    imgBtnUndoSiDrawing1.Visible = true;
                    imgBtnUndoSiDrawing2.Visible = true;
                    imgBtnUndoSiDrawing3.Visible = true;
                    imgBtnUndoSiDrawing4.Visible = true;

                    pnlViewSiDrawing1.Visible = true;
                    pnlViewSiDrawing2.Visible = true;
                    pnlViewSiDrawing3.Visible = true;
                    pnlViewSiDrawing4.Visible = true;

                    pnlUploadSiDrawing1.Visible = false;
                    pnlUploadSiDrawing2.Visible = false;
                    pnlUploadSiDrawing3.Visible = false;
                    pnlUploadSiDrawing4.Visible = false;

                    btnAddUpdateSubitemToList.Text = "Update Subitem";

                    hdSubitemUpdationFlag.Value = "1";
                    //EnableDisableControls(Convert.ToInt32(lblSUID.Text));

                    if (!string.IsNullOrEmpty(lblSrNo.Text))
                        hdSRNo.Value = lblSrNo.Text;
                    else hdSRNo.Value = "0";

                    if (!string.IsNullOrEmpty(lblProductionNumber.Text))
                        txtProductionOrderNo.Text = lblProductionNumber.Text;
                    else txtProductionOrderNo.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblProductionOrderDate.Text))
                        txtProductionOrderDate.Text = lblProductionOrderDate.Text;
                    else txtProductionOrderDate.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblExpectedCompletionDate.Text))
                        txtExpectedCompletionDate.Text = lblExpectedCompletionDate.Text;
                    else txtExpectedCompletionDate.Text = string.Empty;

                    BindProductDetail(lblProductionNumber.Text);
                    if (!string.IsNullOrEmpty(lblProductCode.Text))
                        ddlProductCode.SelectedValue = lblProductCode.Text;
                    else ddlProductCode.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(lblProductDesc.Text))
                        txtProductDesc.Text = lblProductDesc.Text;
                    else txtProductDesc.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblUOM.Text))
                        txtUOM.Text = lblUOM.Text;
                    else txtUOM.Text = string.Empty;

                    if (Convert.ToInt32(lblIsPartOfProductionStatusReportID.Text) > 0)
                        chkIsPartOfProductionOrMainDrawing.Checked = true;
                    else chkIsPartOfProductionOrMainDrawing.Checked = false;

                    ddlLOTMainItems.SelectedValue = Convert.ToString(lblLOTMainItemID.Text);

                    BindLOTMainSubItems();
                    ddlLOTMainSubItems.SelectedValue = Convert.ToString(lblLOTMainSubitemID.Text);


                    if (!string.IsNullOrEmpty(txtTagNoInList.Text))
                        txtTagNo.Text = txtTagNoInList.Text;
                    else txtTagNo.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblDescription.Text))
                        txtDescription.Text = lblDescription.Text;
                    else txtDescription.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblDrgOrDOCNo.Text))
                        txtDrgNo.Text = lblDrgOrDOCNo.Text;
                    else txtDrgNo.Text = string.Empty;

                    //if (!string.IsNullOrEmpty(lblRevNo.Text))
                    //    txtRevNo.Text = lblRevNo.Text;
                    //else txtRevNo.Text = string.Empty;

                    ddlRevNo.SelectedValue = lblRevNo.Text;
                    //hdRevNoText.Value = txtRevNo.Text;
                    //hdRevNoTextOld.Value= txtRevNo.Text;
                    //txtRevNoText.Text = hdRevNoText.Value;


                    string[] str = lblCategoryID.Text.Split(',');
                    foreach (string item in str)
                    {
                        chkLstCategory.Items[Convert.ToInt32(item) - 1].Selected = true;
                    }

                    if (!string.IsNullOrEmpty(lblQuantity.Text))
                    {
                        hdQuantity.Value = lblQuantity.Text;
                        txtQuantity.Text = lblQuantity.Text;
                    }
                    else
                    {
                        hdQuantity.Value = string.Empty;
                        txtQuantity.Text = string.Empty;
                    }




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
                        pnlViewSiDrawing1.Visible = true;
                        pnlUploadSiDrawing1.Visible = false;

                        imgBtnViewSiDrawing1.Visible = true;

                        attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawing1.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawing1.Text = lblAttachment1.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawing1.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawing1.Text = lblAttachment1.Text;
                        }
                    }
                    else
                    {
                        pnlViewSiDrawing1.Visible = false;
                        pnlUploadSiDrawing1.Visible = true;

                        imgBtnViewSiDrawing1.Visible = false;
                        imgBtnUndoSiDrawing1.Visible = false;
                    }


                    if (!string.IsNullOrEmpty(lblAttachment2.Text))
                    {
                        pnlViewSiDrawing2.Visible = true;
                        pnlUploadSiDrawing2.Visible = false;

                        imgBtnViewSiDrawing2.Visible = true;

                        attachment1Extn = Convert.ToString(lblAttachment2.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawing2.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawing2.Text = lblAttachment2.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawing2.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawing2.Text = lblAttachment2.Text;
                        }
                    }
                    else
                    {
                        pnlViewSiDrawing2.Visible = false;
                        pnlUploadSiDrawing2.Visible = true;

                        imgBtnViewSiDrawing2.Visible = false;
                        imgBtnUndoSiDrawing2.Visible = false;
                    }



                    if (!string.IsNullOrEmpty(lblAttachment3.Text))
                    {
                        pnlViewSiDrawing3.Visible = true;
                        pnlUploadSiDrawing3.Visible = false;

                        imgBtnViewSiDrawing3.Visible = true;

                        attachment1Extn = Convert.ToString(lblAttachment3.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawing3.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawing3.Text = lblAttachment3.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawing3.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawing3.Text = lblAttachment3.Text;
                        }
                    }
                    else
                    {
                        pnlViewSiDrawing3.Visible = false;
                        pnlUploadSiDrawing3.Visible = true;

                        imgBtnViewSiDrawing3.Visible = false;
                        imgBtnUndoSiDrawing3.Visible = false;
                    }




                    if (!string.IsNullOrEmpty(lblAttachment4.Text))
                    {
                        pnlViewSiDrawing4.Visible = true;
                        pnlUploadSiDrawing4.Visible = false;

                        imgBtnViewSiDrawing4.Visible = true;

                        attachment1Extn = Convert.ToString(lblAttachment4.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawing4.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawing4.Text = lblAttachment4.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawing4.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawing4.Text = lblAttachment4.Text;
                        }
                    }
                    else
                    {
                        pnlViewSiDrawing4.Visible = false;
                        pnlUploadSiDrawing4.Visible = true;

                        imgBtnViewSiDrawing4.Visible = false;
                        imgBtnUndoSiDrawing4.Visible = false;
                    }


                    #endregion

                    mpeAddSubitems.Show();
                }


                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    RemoveSubitems(Convert.ToInt32(lblSrNo.Text));
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
                //Label lblSUID = e.Row.FindControl("lblSUID") as Label;

                //if (Convert.ToInt32(lblSUID.Text) > 0)
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                //    }
                //}


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










    //ADD UPDATE ATTACHMENTS
    protected void btnAddUpdateAttachments_Click(object sender, EventArgs e)
    {
        mpeAddUpdateAttachments.Show();
    }

    protected void btnAddUpdateAttachmentsToList_Click(object sender, EventArgs e)
    {
        AddUpdateAttachments();
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
            ExceptionMessage(ex.ToString());
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }





    protected void imgBtnRemoveSiDrawing1_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing1.Value = "0";
        hdUploadSiDrawing1.Value = "1";

        pnlViewSiDrawing1.Visible = false;
        pnlUploadSiDrawing1.Visible = true;

        mpeAddSubitems.Show();
    }
    protected void imgBtnUndoSiDrawing1_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing1.Value = "1";
        hdUploadSiDrawing1.Value = "0";

        pnlViewSiDrawing1.Visible = true;
        pnlUploadSiDrawing1.Visible = false;

        mpeAddSubitems.Show();
    }

    protected void imgBtnRemoveSiDrawing2_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing2.Value = "0";
        hdUploadSiDrawing2.Value = "1";

        pnlViewSiDrawing2.Visible = false;
        pnlUploadSiDrawing2.Visible = true;

        mpeAddSubitems.Show();
    }
    protected void imgBtnUndoSiDrawing2_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing2.Value = "1";
        hdUploadSiDrawing2.Value = "0";

        pnlViewSiDrawing2.Visible = true;
        pnlUploadSiDrawing2.Visible = false;

        mpeAddSubitems.Show();
    }

    protected void imgBtnRemoveSiDrawing3_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing3.Value = "0";
        hdUploadSiDrawing3.Value = "1";

        pnlViewSiDrawing3.Visible = false;
        pnlUploadSiDrawing3.Visible = true;

        mpeAddSubitems.Show();
    }
    protected void imgBtnUndoSiDrawing3_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing3.Value = "1";
        hdUploadSiDrawing3.Value = "0";

        pnlViewSiDrawing3.Visible = true;
        pnlUploadSiDrawing3.Visible = false;

        mpeAddSubitems.Show();
    }

    protected void imgBtnRemoveSiDrawing4_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing4.Value = "0";
        hdUploadSiDrawing4.Value = "1";

        pnlViewSiDrawing4.Visible = false;
        pnlUploadSiDrawing4.Visible = true;

        mpeAddSubitems.Show();
    }
    protected void imgBtnUndoSiDrawing4_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawing4.Value = "1";
        hdUploadSiDrawing4.Value = "0";

        pnlViewSiDrawing4.Visible = true;
        pnlUploadSiDrawing4.Visible = false;

        mpeAddSubitems.Show();
    }



    protected void imgBtnViewSiDrawing1_Click(object sender, ImageClickEventArgs e)
    {

        if (dtTemp != null && Session["dtSubitem"] != null)
            dtTemp = (DataTable)Session["dtSubitem"];
        else
            AddTempSubitemTable();

        if (dtTemp.Rows.Count > 0)
        {
            SubitemTable.dtSubitems = dtTemp;

            ViewDrawingFiles(Convert.ToInt32(ViewState["SR_NO"]), txtSiDrawing1.Text.Trim(), "DRAWING1");
        }


    }

    protected void imgBtnViewSiDrawing2_Click(object sender, ImageClickEventArgs e)
    {
        //ViewDrawingFiles(Convert.ToInt32(Request.QueryString["lottfid"]), "DRAWING2", txtSiDrawing2.Text.Trim());

        if (dtTemp != null && Session["dtSubitem"] != null)
            dtTemp = (DataTable)Session["dtSubitem"];
        else
            AddTempSubitemTable();

        if (dtTemp.Rows.Count > 0)
        {
            SubitemTable.dtSubitems = dtTemp;

            ViewDrawingFiles(Convert.ToInt32(ViewState["SR_NO"]), txtSiDrawing2.Text.Trim(), "DRAWING2");
        }
    }

    protected void imgBtnViewSiDrawing3_Click(object sender, ImageClickEventArgs e)
    {
        //ViewDrawingFiles(Convert.ToInt32(Request.QueryString["lottfid"]), "DRAWING3", txtSiDrawing3.Text.Trim());

        if (dtTemp != null && Session["dtSubitem"] != null)
            dtTemp = (DataTable)Session["dtSubitem"];
        else
            AddTempSubitemTable();

        if (dtTemp.Rows.Count > 0)
        {
            SubitemTable.dtSubitems = dtTemp;

            ViewDrawingFiles(Convert.ToInt32(ViewState["SR_NO"]), txtSiDrawing3.Text.Trim(), "DRAWING3");
        }
    }

    protected void imgBtnViewSiDrawing4_Click(object sender, ImageClickEventArgs e)
    {
        //ViewDrawingFiles(Convert.ToInt32(Request.QueryString["lottfid"]), "DRAWING4", txtSiDrawing4.Text.Trim());

        if (dtTemp != null && Session["dtSubitem"] != null)
            dtTemp = (DataTable)Session["dtSubitem"];
        else
            AddTempSubitemTable();

        if (dtTemp.Rows.Count > 0)
        {
            SubitemTable.dtSubitems = dtTemp;

            ViewDrawingFiles(Convert.ToInt32(ViewState["SR_NO"]), txtSiDrawing4.Text.Trim(), "DRAWING4");
        }
    }


    protected void btnPreview_Click(object sender, EventArgs e)
    {
        PreviewLOTTransmittalToFactory();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session["dtLOT"] = null;
        Session["dtSubitems"] = null;
        Session["dtQuantityDetails"] = null;

        if (Convert.ToInt32(rdSavingType.SelectedValue) == Convert.ToInt32(LOTAllStatusAndTypes.SavingType.Save))
        {
            SaveLOTTransmittalToFactory(Convert.ToInt32(LOTAllStatusAndTypes.SavingType.Save));
        }
        else if (Convert.ToInt32(rdSavingType.SelectedValue) == Convert.ToInt32(LOTAllStatusAndTypes.SavingType.SaveAndSendForApproval))
        {
            SaveLOTTransmittalToFactory(Convert.ToInt32(LOTAllStatusAndTypes.SavingType.SaveAndSendForApproval));
        }
    }

    protected void btnProjectList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/ProjectList.aspx");
    }

    #endregion


    #region METHODS[=========================]

    private void AddTempSubitemTable()
    {
        dtSubitem.Columns.Add("SR_NO", typeof(int));

        dtSubitem.Columns.Add("STATUS_ID", typeof(int));
        dtSubitem.Columns.Add("LOT_MAIN_ITEM_ID", typeof(int));
        dtSubitem.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
        dtSubitem.Columns.Add("LOT_MAIN_ITEM", typeof(string));

        dtSubitem.Columns.Add("TAG_NO", typeof(string));
        dtSubitem.Columns.Add("SUBITEM_DESC", typeof(string));
        dtSubitem.Columns.Add("DRG_NO", typeof(string));

        dtSubitem.Columns.Add("REV_NO", typeof(int));
        dtSubitem.Columns.Add("REV_NO_TEXT", typeof(string));


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
                //ddlCompany.Items.Insert(0, "Select");

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainItems()
    {
        try
        {
            dsLOTMainItems = objProject.GetLotMainItems();
            if (dsLOTMainItems.Tables.Count > 0 && dsLOTMainItems.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItems.DataSource = dsLOTMainItems.Tables[0];
                ddlLOTMainItems.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItems.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItems.DataBind();
                ddlLOTMainItems.Items.Insert(0, "Select");
                ddlLOTMainItems.SelectedIndex = 0;


                if (ddlLOTMainItems.SelectedIndex > 0)
                {
                    BindLOTMainSubItems();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainSubItems()
    {
        try
        {
            ddlLOTMainSubItems.Items.Clear();
            ddlLOTMainSubItems.Items.Insert(0, "Select");
            ddlLOTMainSubItems.SelectedIndex = 0;

            if (ddlLOTMainItems.SelectedIndex > 0)
            {
                dsLOTMainSubItems = objProject.GetLotMainSubItems(Convert.ToInt32(ddlLOTMainItems.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue));
                if (dsLOTMainSubItems.Tables.Count > 0 && dsLOTMainSubItems.Tables[0].Rows.Count > 0)
                {
                    ddlLOTMainSubItems.DataSource = dsLOTMainSubItems.Tables[0];
                    ddlLOTMainSubItems.DataTextField = "LOT_MAIN_SUBITEM";
                    ddlLOTMainSubItems.DataValueField = "LOT_MAIN_SUBITEM_ID";
                    ddlLOTMainSubItems.DataBind();
                    ddlLOTMainSubItems.Items.Insert(0, "Select");
                    ddlLOTMainSubItems.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTCategoryForFactory()
    {
        try
        {
            dsLOTCategoryForFactory = objProject.GetFactoryCategory();
            if (dsLOTCategoryForFactory.Tables.Count > 0 && dsLOTCategoryForFactory.Tables[0].Rows.Count > 0)
            {
                chkLstCategory.DataSource = dsLOTCategoryForFactory.Tables[0];
                chkLstCategory.DataTextField = "LOT_CATEGORY_NAME";
                chkLstCategory.DataValueField = "LOT_CATEGORY_ID";
                chkLstCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataSet GetJOBData()
    {
        try
        {
            companyID = 0;
            custCode = string.Empty;
            jobNo = string.Empty;
            poNo = string.Empty;

            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtCustomerCodeSearch.Text))
                custCode = txtCustomerCodeSearch.Text;
            else
                custCode = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtPONoSearch.Text))
                poNo = txtPONoSearch.Text;
            else
                poNo = string.Empty;

            dsJobNo = objProject.GetJOBDetailsForLOT(companyID, custCode, jobNo, poNo);

            if (dsJobNo.Tables.Count > 0)
            {
                return dsJobNo;
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

    private void GetJOBDetail()
    {
        try
        {
            dsJobNo = GetJOBData();
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsJobNo.Tables[0];
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


    private DataSet GetProductionOrderNoData()
    {
        try
        {
            companyID = 0;
            jobNo = string.Empty;
            productionOrderNo = string.Empty;

            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

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
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void GetProductionOrderNoDetail()
    {
        try
        {
            dsProductionOrderNo = GetProductionOrderNoData();
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
            ExceptionMessage(ex.ToString());
        }
    }


    private DataSet GetDMSDrawingData()
    {
        try
        {
            companyID = 0;
            jobNo = string.Empty;

            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

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

    private void GetDMSDrawingList()
    {
        try
        {
            dsDMSDrawingNo = GetDMSDrawingData();
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
                ddlProductCode.DataSource = dsProdDetail.Tables[0];
                ddlProductCode.DataTextField = "PRODUCT_CODE_DESC";
                ddlProductCode.DataValueField = "PRODUCT_CODE";
                ddlProductCode.DataBind();
                ddlProductCode.Items.Insert(0, "Select");
                ddlProductCode.SelectedIndex = 0;
            }
            else
            {
                Session["dtProdDetail"] = null;
                ddlProductCode.Items.Clear();
                ddlProductCode.Items.Insert(0, "Select");
                ddlProductCode.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }


    private void GetTFNo()
    {
        try
        {
            jobNo = string.Empty;
            companyID = 0;
            companyName = string.Empty;
            runningNo = 0;
            runningNoTxt = "";
            TFNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
            {
                jobNo = txtJOBNo.Text;
                if (jobNo.IndexOf('.') > 0)
                    jobNo = jobNo.Substring(0, (jobNo.IndexOf('.')));
            }
            else
                jobNo = string.Empty;

            companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            companyName = ddlCompany.SelectedItem.Text;

            runningNo = objProject.GetFTRunningNo(jobNo, companyID);
            runningNo = runningNo + 1;

            //if (dsFTRunningNo.Tables.Count > 0 && dsFTRunningNo.Tables[0].Rows.Count > 0)
            //{
            //    runningNo = Convert.ToInt32(dsFTRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
            //    //runningNoTxt = Convert.ToString(dsFTRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
            //}

            for (int i = 1; i <= (4 - (Convert.ToString(runningNo).Length)); i++)
            {
                runningNoTxt += "0";
            }

            runningNoTxt += Convert.ToString(runningNo);

            TFNo = jobNo + "-" + companyName + "-" + runningNoTxt;
            txtTFNo.Text = TFNo;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
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

            SiDrawing1File = string.Empty;
            SiDrawing1FileBytes = null;

            SiDrawing2File = string.Empty;
            SiDrawing2FileBytes = null;

            SiDrawing3File = string.Empty;
            SiDrawing3FileBytes = null;

            SiDrawing4File = string.Empty;
            SiDrawing4FileBytes = null;


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


            LOTMainItem = Convert.ToString(ddlLOTMainItems.SelectedItem.Text);
            LOTMainItemID = Convert.ToInt32(ddlLOTMainItems.SelectedValue);

            LOTMainSubItem = Convert.ToString(ddlLOTMainSubItems.SelectedItem.Text);
            LOTMainSubItemID = Convert.ToInt32(ddlLOTMainSubItems.SelectedValue);

            if (!string.IsNullOrEmpty(txtTagNo.Text))
                tagNo = txtTagNo.Text.Trim().ToUpper().Replace(Environment.NewLine, " ");


            if (!string.IsNullOrEmpty(txtDescription.Text))
                subitemDesc = txtDescription.Text.Replace(Environment.NewLine, " ");

            if (!string.IsNullOrEmpty(txtDrgNo.Text))
                drgNo = txtDrgNo.Text.Trim().ToUpper();

            //if (!string.IsNullOrEmpty(txtRevNo.Text))
            //    revisionNo = Convert.ToInt32(txtRevNo.Text);

            revisionNo = Convert.ToInt32(ddlRevNo.SelectedValue);

            if (revisionNo == 99) //Other Revision
            {
                if (!string.IsNullOrEmpty(txtRevNoText.Text))
                    revisionNoText = Convert.ToString(txtRevNoText.Text);
            }
            else
                revisionNoText = Convert.ToString(hdRevNoText.Value);



            foreach (System.Web.UI.WebControls.ListItem item in chkLstCategory.Items)
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

            if (!string.IsNullOrEmpty(txtQuantity.Text))
                quantity = Convert.ToInt32(txtQuantity.Text);


            if (!string.IsNullOrEmpty(txtProductionOrderNo.Text))
                productionOrderNo = txtProductionOrderNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtProductionOrderDate.Text))
                productionOrderDate = txtProductionOrderDate.Text;

            if (!string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
            {
                expectedCompletionDate = txtExpectedCompletionDate.Text;
                //CheckForExpectedCompletionDate(txtExpectedCompletionDate.Text);


                //if (Convert.ToDateTime(txtExpectedCompletionDate.Text).Date >= DateTime.Now.Date)
                //{
                //    expectedCompletionDate = txtExpectedCompletionDate.Text;
                //}
                //else
                //{
                //    ExceptionAddSubitemMessage("Completion Required By, must be greater than or equal to current date,\n please make necessary changes in FACT(Production Order)");
                //    mpeAddSubitems.Show();
                //    return;
                //}
            }

            if (ddlProductCode.SelectedIndex > 0)
                productCode = Convert.ToString(ddlProductCode.SelectedValue);

            if (!string.IsNullOrEmpty(txtProductDesc.Text))
                productDesc = txtProductDesc.Text;

            if (!string.IsNullOrEmpty(txtUOM.Text))
                UOM = txtUOM.Text;

            if (chkIsPartOfProductionOrMainDrawing.Checked)
            {
                isPartOfProductionStatus = "Yes";
                isPartOfProductionStatusID = 1;
            }
            else
            {
                isPartOfProductionStatus = "No";
                isPartOfProductionStatusID = 0;
            }

            if (uploadFileSiDrawing1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawing1.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawing1.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing1File = str[str.Length - 1];
                    SiDrawing1FileBytes = GetFileBytes(uploadFileSiDrawing1.PostedFile.FileName, uploadFileSiDrawing1.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawing2.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawing2.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawing2.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing2File = str[str.Length - 1];
                    SiDrawing2FileBytes = GetFileBytes(uploadFileSiDrawing2.PostedFile.FileName, uploadFileSiDrawing2.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawing3.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawing3.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawing3.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing3File = str[str.Length - 1];
                    SiDrawing3FileBytes = GetFileBytes(uploadFileSiDrawing3.PostedFile.FileName, uploadFileSiDrawing3.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawing4.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawing4.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawing4.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing4File = str[str.Length - 1];
                    SiDrawing4FileBytes = GetFileBytes(uploadFileSiDrawing4.PostedFile.FileName, uploadFileSiDrawing4.PostedFile.InputStream);
                }
            }


            int j = 0;
            //for (int i = (gvSubItem.Rows.Count + 1); i < (gvSubItem.Rows.Count + 1 + quantity); i++)
            for (int i = (gvSubItem.Rows.Count + 1); i < (gvSubItem.Rows.Count + 2); i++)
            {
                j++;

                DataRow dr = dtTemp.NewRow();

                dr["SR_NO"] = i;
                dr["LOT_MAIN_ITEM_ID"] = LOTMainItemID;
                dr["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
                dr["LOT_MAIN_ITEM"] = LOTMainItem + " [" + LOTMainSubItem + "]";
                dr["TAG_NO"] = tagNo;
                dr["SUBITEM_DESC"] = subitemDesc;

                dr["DRG_NO"] = drgNo;

                dr["REV_NO"] = revisionNo;
                dr["REV_NO_TEXT"] = revisionNoText;

                dr["CATEGORY_ID"] = categoryID;
                dr["CATEGORY"] = category;
                dr["QUANTITY"] = quantity;//1;

                dr["PRODUCTION_ORDER_NO"] = productionOrderNo.Trim();
                dr["PRODUCTION_ORDER_DATE"] = productionOrderDate;
                dr["EXPECTED_COMPLETION_DATE"] = expectedCompletionDate;
                dr["PRODUCT_CODE"] = productCode;
                dr["PRODUCT_DESC"] = productDesc;
                dr["UOM"] = UOM;
                dr["IS_PART_OF_PRODUCTION_STATUS_REPORT"] = isPartOfProductionStatus;
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

            gvSubItem.DataSource = dtTemp;
            gvSubItem.DataBind();
            lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";

            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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

            SiDrawing1File = string.Empty;
            SiDrawing1FileBytes = null;

            SiDrawing2File = string.Empty;
            SiDrawing2FileBytes = null;

            SiDrawing3File = string.Empty;
            SiDrawing3FileBytes = null;

            SiDrawing4File = string.Empty;
            SiDrawing4FileBytes = null;




            LOTMainItem = Convert.ToString(ddlLOTMainItems.SelectedItem.Text);
            LOTMainItemID = Convert.ToInt32(ddlLOTMainItems.SelectedValue);

            LOTMainSubItem = Convert.ToString(ddlLOTMainSubItems.SelectedItem.Text);
            LOTMainSubItemID = Convert.ToInt32(ddlLOTMainSubItems.SelectedValue);

            if (!string.IsNullOrEmpty(txtTagNo.Text))
                tagNo = txtTagNo.Text;


            if (!string.IsNullOrEmpty(txtDescription.Text))
                subitemDesc = txtDescription.Text;

            if (!string.IsNullOrEmpty(txtDrgNo.Text))
                drgNo = txtDrgNo.Text;

            //if (!string.IsNullOrEmpty(txtRevNo.Text))
            //    revisionNo = Convert.ToInt32(txtRevNo.Text);

            revisionNo = Convert.ToInt32(ddlRevNo.SelectedValue);

            if (revisionNo == 99) //Other Revision
            {
                if (!string.IsNullOrEmpty(txtRevNoText.Text))
                    revisionNoText = Convert.ToString(txtRevNoText.Text);
            }
            else
                revisionNoText = Convert.ToString(hdRevNoText.Value);

            foreach (System.Web.UI.WebControls.ListItem item in chkLstCategory.Items)
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

            if (!string.IsNullOrEmpty(txtQuantity.Text))
                quantity = Convert.ToInt32(txtQuantity.Text);


            if (!string.IsNullOrEmpty(txtProductionOrderNo.Text))
                productionOrderNo = txtProductionOrderNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtProductionOrderDate.Text))
                productionOrderDate = txtProductionOrderDate.Text;

            if (!string.IsNullOrEmpty(txtExpectedCompletionDate.Text))
            {
                expectedCompletionDate = txtExpectedCompletionDate.Text;
                //CheckForExpectedCompletionDate(txtExpectedCompletionDate.Text);

                //if (Convert.ToDateTime(expectedCompletionDate).Date >= DateTime.Now.Date)
                //{
                //    expectedCompletionDate = txtExpectedCompletionDate.Text;
                //}
                //else
                //{
                //    ExceptionAddSubitemMessage("Completion Required By, must be greater than or equal to current date,\n please make necessary changes in FACT(Production Order)");
                //    mpeAddSubitems.Show();
                //    return;
                //}
            }


            if (ddlProductCode.SelectedIndex > 0)
                productCode = Convert.ToString(ddlProductCode.SelectedValue);

            if (!string.IsNullOrEmpty(txtProductDesc.Text))
                productDesc = txtProductDesc.Text;

            if (!string.IsNullOrEmpty(txtUOM.Text))
                UOM = txtUOM.Text;

            if (chkIsPartOfProductionOrMainDrawing.Checked)
            {
                isPartOfProductionStatus = "Yes";
                isPartOfProductionStatusID = 1;
            }
            else
            {
                isPartOfProductionStatus = "No";
                isPartOfProductionStatusID = 0;
            }


            if (uploadFileSiDrawing1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawing1.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawing1.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing1File = str[str.Length - 1];
                    SiDrawing1FileBytes = GetFileBytes(uploadFileSiDrawing1.PostedFile.FileName, uploadFileSiDrawing1.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawing2.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawing2.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawing2.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing2File = str[str.Length - 1];
                    SiDrawing2FileBytes = GetFileBytes(uploadFileSiDrawing2.PostedFile.FileName, uploadFileSiDrawing2.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawing3.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawing3.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawing3.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing3File = str[str.Length - 1];
                    SiDrawing3FileBytes = GetFileBytes(uploadFileSiDrawing3.PostedFile.FileName, uploadFileSiDrawing3.PostedFile.InputStream);
                }
            }

            if (uploadFileSiDrawing4.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileSiDrawing4.PostedFile.FileName))
                {
                    string[] str = uploadFileSiDrawing4.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    SiDrawing4File = str[str.Length - 1];
                    SiDrawing4FileBytes = GetFileBytes(uploadFileSiDrawing4.PostedFile.FileName, uploadFileSiDrawing4.PostedFile.InputStream);
                }
            }



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
                        d["DRG_NO"] = drgNo;
                    else d["DRG_NO"] = string.Empty;

                    if (revisionNo > 0)
                        d["REV_NO"] = revisionNo;
                    else d["REV_NO"] = "0";

                    if (!string.IsNullOrEmpty(revisionNoText))
                        d["REV_NO_TEXT"] = revisionNoText;
                    else d["REV_NO_TEXT"] = string.Empty;

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

                    d["IS_PART_OF_PRODUCTION_STATUS_REPORT"] = isPartOfProductionStatus;
                    d["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] = isPartOfProductionStatusID;

                    if (!string.IsNullOrEmpty(SiDrawing1File) && SiDrawing1FileBytes != null)
                    {
                        d["SI_ATTACHMENT1_NAME"] = SiDrawing1File;
                        d["SI_ATTACHMENT1_BTYTES"] = SiDrawing1FileBytes;
                    }

                    if (!string.IsNullOrEmpty(SiDrawing2File) && SiDrawing2FileBytes != null)
                    {
                        d["SI_ATTACHMENT2_NAME"] = SiDrawing2File;
                        d["SI_ATTACHMENT2_BTYTES"] = SiDrawing2FileBytes;
                    }

                    if (!string.IsNullOrEmpty(SiDrawing3File) && SiDrawing3FileBytes != null)
                    {
                        d["SI_ATTACHMENT3_NAME"] = SiDrawing3File;
                        d["SI_ATTACHMENT3_BTYTES"] = SiDrawing3FileBytes;
                    }

                    if (!string.IsNullOrEmpty(SiDrawing4File) && SiDrawing4FileBytes != null)
                    {
                        d["SI_ATTACHMENT4_NAME"] = SiDrawing4File;
                        d["SI_ATTACHMENT4_BTYTES"] = SiDrawing4FileBytes;
                    }
                }
            }

            gvSubItem.DataSource = dtTemp;
            gvSubItem.DataBind();

            lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";

            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void CheckForExpectedCompletionDate(string expectedCompletionDate)
    {
        try
        {
            if (Convert.ToDateTime(expectedCompletionDate).Date >= DateTime.Now.Date)
            {
                btnAddUpdateSubitemToList.Enabled = true;
            }
            else
            {
                txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightPink;
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

        if (dtTemp.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTemp.Select("SR_NO='" + srNo + "'"))
            {
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

            if (uploadFileAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment1.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment1.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment1File = str[str.Length - 1];
                    attachment1FileBytes = GetFileBytes(uploadFileAttachment1.PostedFile.FileName, uploadFileAttachment1.PostedFile.InputStream);
                }
            }

            if (uploadFileAttachment2.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment2.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment2.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment2File = str[str.Length - 1];
                    attachment2FileBytes = GetFileBytes(uploadFileAttachment2.PostedFile.FileName, uploadFileAttachment2.PostedFile.InputStream);
                }
            }

            if (uploadFileAttachment3.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment3.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment3.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment3File = str[str.Length - 1];
                    attachment3FileBytes = GetFileBytes(uploadFileAttachment3.PostedFile.FileName, uploadFileAttachment3.PostedFile.InputStream);
                }
            }

            if (uploadFileAttachment4.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment4.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment4.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment4File = str[str.Length - 1];
                    attachment4FileBytes = GetFileBytes(uploadFileAttachment4.PostedFile.FileName, uploadFileAttachment4.PostedFile.InputStream);
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
            ExceptionMessage(ex.ToString());
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

            dtLOT.Columns.Add("IS_TRANSFERRED", typeof(string));
            dtLOT.Columns.Add("TRANSFERRED_LOT_TF_ID", typeof(string));
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
            drLOT["TF_NO"] = Convert.ToString(txtTFNo.Text);
            drLOT["UNIT_NAME"] = Convert.ToString(ddlCompany.SelectedItem.Text);
            drLOT["DATE"] = Convert.ToString(txtDate.Text);
            drLOT["CUSTOMER_CODE"] = Convert.ToString(txtCustomerCode.Text);
            drLOT["CUSTOMER_NAME"] = Convert.ToString(txtCustomerName.Text);
            drLOT["JOB_NO"] = Convert.ToString(txtJOBNo.Text);
            drLOT["PO_NO"] = Convert.ToString(txtPONo.Text);
            drLOT["ITEM_NAME"] = Convert.ToString(txtItemName.Text);

            drLOT["OLD_LOT_TF_ID"] = 0;
            drLOT["OLD_TF_NO"] = "";
            drLOT["OLD_CREATED_ON"] = "";

            drLOT["IS_TRANSFERRED"] = 0;
            drLOT["TRANSFERRED_LOT_TF_ID"] = 0;
            drLOT["OLD_TRANSFERRED_TF_NO"] = "";
            drLOT["OLD_TRANSFERRED_CREATED_ON"] = "";
            drLOT["TRANSFERRED_REMARKS"] = "";

            drLOT["LOT_CREATED_ON"] = "";



            drLOT["IMP_NOTES"] = Convert.ToString(txtNotes.Text).Replace(Environment.NewLine, " ");
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


            if (gvSubItem.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItem.Rows)
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

                    drSI["REVISION_NO"] = Convert.ToString(txtRevNo.Text);
                    drSI["REVISION_NO_TEXT"] = Convert.ToString(txtRevNo.Text);

                    drSI["CATEGORY"] = Convert.ToString(lblCategory.Text);
                    drSI["QUANTITY"] = Convert.ToString(txtQuantity.Text);
                    drSI["PRODUCTION_MNGR_ID"] = 0;
                    drSI["PRODUCTION_MNGR_NAME"] = "";
                    drSI["CREATED_BY_ID"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                    drSI["CREATED_BY"] = Convert.ToString(Session["EMPLOYEE_NAME"]);
                    drSI["CREATED_ON"] = DateTime.Now.ToString("dd-MMM-yyyy HH:mm tt");
                    drSI["APPROVED_BY_ID"] = 0;
                    drSI["APPROVED_BY"] = "";
                    drSI["APPROVED_ON"] = "";
                    drSI["APPROVED_REMARKS"] = "";
                    drSI["PLANNING_ACCEPTED_BY_ID"] = 0;
                    drSI["PLANNING_ACCEPTED_BY"] = "";
                    drSI["PLANNING_ACCEPTED_ON"] = "";
                    drSI["PLANNING_ACCEPTED_REMARKS"] = "";
                    drSI["FORWARDED_BY_ID"] = 0;
                    drSI["FORWARDED_BY"] = "";
                    drSI["FORWARDED_ON"] = "";
                    drSI["FORWARDED_REMARKS"] = "";
                    drSI["ACCEPTED_BY_ID"] = 0;
                    drSI["ACCEPTED_BY"] = "";
                    drSI["ACCEPTED_ON"] = "";
                    drSI["ACCEPTED_REMARKS"] = "";
                    drSI["QUALITY_ACCEPTED_BY_ID"] = 0;
                    drSI["QUALITY_ACCEPTED_BY"] = "";
                    drSI["QUALITY_ACCEPTED_ON"] = "";
                    drSI["QUALITY_ACCEPTED_REMARKS"] = "";
                    drSI["SENT_TO_INTL_INSP_BY_ID"] = 0;
                    drSI["SENT_TO_INTL_INSP_BY"] = "";
                    drSI["SENT_TO_INTL_INSP_ON"] = "";
                    drSI["SENT_TO_INTL_INSP_REMARKS"] = "";
                    drSI["QA_INTL_INSP_ACCEPTED_BY_ID"] = 0;
                    drSI["QA_INTL_INSP_ACCEPTED_BY"] = "";
                    drSI["QA_INTL_INSP_ACCEPTED_ON"] = "";
                    drSI["QA_INTL_INSP_ACCEPTED_REMARKS"] = "";

                    drSI["QA_INTL_INSP_NOT_ACCEPTED_BY_ID"] = 0;
                    drSI["QA_INTL_INSP_NOT_ACCEPTED_BY"] = "";
                    drSI["QA_INTL_INSP_NOT_ACCEPTED_ON"] = "";
                    drSI["QA_INTL_INSP_NOT_ACCEPTED_REMARKS"] = "";


                    drSI["SENT_TO_FINAL_INSP_BY_ID"] = 0;
                    drSI["SENT_TO_FINAL_INSP_BY"] = "";
                    drSI["SENT_TO_FINAL_INSP_ON"] = "";
                    drSI["SENT_TO_FINAL_INSP_REMARKS"] = "";

                    drSI["SENT_TO_REWORK_BY_ID"] = 0;
                    drSI["SENT_TO_REWORK_BY"] = "";
                    drSI["SENT_TO_REWORK_ON"] = "";
                    drSI["SENT_TO_REWORK_REMARKS"] = "";


                    drSI["AMENDMENT_COUNT"] = 0;
                    drSI["AMENDMENT_BY_ID"] = 0;
                    drSI["AMENDMENT_BY"] = "";
                    drSI["AMENDMENT_ON"] = "";
                    drSI["AMENDMENT_REMARKS"] = "";
                    drSI["PROD_AMENDMENT_BY_ID"] = 0;
                    drSI["PROD_AMENDMENT_BY"] = "";
                    drSI["PROD_AMENDMENT_ON"] = "";
                    drSI["PROD_AMENDMENT_REMARKS"] = "";
                    drSI["AMENDED_BY_ID"] = 0;
                    drSI["AMENDED_BY"] = "";
                    drSI["AMENDED_ON"] = "";
                    drSI["AMENDED_REMARKS"] = "";
                    drSI["AMENDED_APPROVED_BY_ID"] = 0;
                    drSI["AMENDED_APPROVED_BY"] = "";
                    drSI["AMENDED_APPROVED_ON"] = "";
                    drSI["AMENDED_APPROVED_REMARKS"] = "";
                    drSI["AMENDED_PLANNING_ACCEPTED_BY_ID"] = 0;
                    drSI["AMENDED_PLANNING_ACCEPTED_BY"] = "";
                    drSI["AMENDED_PLANNING_ACCEPTED_ON"] = "";
                    drSI["AMENDED_PLANNING_ACCEPTED_REMARKS"] = "";
                    drSI["AMENDED_FORWARDED_BY_ID"] = 0;
                    drSI["AMENDED_FORWARDED_BY"] = "";
                    drSI["AMENDED_FORWARDED_ON"] = "";
                    drSI["AMENDED_FORWARDED_REMARKS"] = "";
                    drSI["AMENDED_ACCEPTED_BY_ID"] = 0;
                    drSI["AMENDED_ACCEPTED_BY"] = "";
                    drSI["AMENDED_ACCEPTED_ON"] = "";
                    drSI["AMENDED_ACCEPTED_REMARKS"] = "";
                    drSI["AMENDED_QUALITY_ACCEPTED_BY_ID"] = 0;
                    drSI["AMENDED_QUALITY_ACCEPTED_BY"] = "";
                    drSI["AMENDED_QUALITY_ACCEPTED_ON"] = "";
                    drSI["AMENDED_QUALITY_ACCEPTED_REMARKS"] = "";
                    drSI["COMPLETED_BY_ID"] = 0;
                    drSI["COMPLETED_BY"] = "";
                    drSI["COMPLETED_ON"] = "";
                    drSI["COMPLETED_REMARKS"] = "";
                    drSI["REVISED_BY_ID"] = 0;
                    drSI["REVISED_BY"] = "";
                    drSI["REVISED_ON"] = "";
                    drSI["REVISED_REMARKS"] = "";

                    dtSubitems.Rows.Add(drSI);
                }
            }


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
            Session["dtQuantityDetails"] = null;



            Session["dtLOT"] = (DataTable)dtLOT;
            Session["dtSubitems"] = (DataTable)dtSubitems;
            Session["dtQuantityDetails"] = (DataTable)dtQuantityDetails;


            if (dtSubitems.Rows.Count > 0)
            {
                ModalPopupExtender4.Show();
                iframeViewTravelStatementInPDF.Attributes.Add("src", "LOTTransmittalFactoryInPDF.aspx?LOTTFID=" + LOTTFID + "&LOTTFSubitemIDs=" + LOTTFSubitemIDs + "&pdfType=" + (int)LOTAllStatusAndTypes.EnumPDFType.Create_Preview);
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

    private void SaveLOTTransmittalToFactory(int savingType)
    {
        try
        {
            string existedLOTMainSubItemIDs = string.Empty;
            string notExistedLOTMainSubItemIDs = string.Empty;
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

            dsJobApprovers = objProject.GetJOBApprovers(txtJOBNo.Text, Convert.ToInt32(ddlCompany.SelectedValue), LOTMainSubItemIDs);
            if (dsJobApprovers.Tables.Count > 0 && dsJobApprovers.Tables[0].Rows.Count > 0)
            {
                //Session["dsJobApprovers"] = dsJobApprovers;                
                AddNewLOTTransmittalToFactory(LOTMainSubItemIDs, savingType);
            }
            else
            {
                //Session["dsJobApprovers"] = null;
                mpeAddApprovers.Show();
                iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0&unitid=" + Convert.ToString(ddlCompany.SelectedValue) + "");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddNewLOTTransmittalToFactory(string LOTMainSubItemIDs, int savingType)
    {
        try
        {

            #region LOT Primary Details

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

            if (!string.IsNullOrEmpty(Convert.ToString(txtTFNo.Text)))
                TFNo = Convert.ToString(txtTFNo.Text);

            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(Convert.ToString(hdDate.Value)))
                LOTDate = Convert.ToDateTime(hdDate.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(txtCustomerCode.Text)))
                custCode = Convert.ToString(txtCustomerCode.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNo.Text)))
                jobNo = Convert.ToString(txtJOBNo.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtPONo.Text)))
                poNo = Convert.ToString(txtPONo.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtItemName.Text)))
                itemName = Convert.ToString(txtItemName.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtNotes.Text)))
                impNotes = Convert.ToString(txtNotes.Text).Replace(Environment.NewLine, " ");



            if (uploadFileAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment1.PostedFile.FileName))
                {
                    string[] str = uploadFileAttachment1.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    attachment1File = str[str.Length - 1];
                    attachment1FileBytes = GetFileBytes(uploadFileAttachment1.PostedFile.FileName, uploadFileAttachment1.PostedFile.InputStream);
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
            dtSubitemToAdd.Columns.Add("DRG_NO", typeof(string));

            dtSubitemToAdd.Columns.Add("REV_NO", typeof(int));
            dtSubitemToAdd.Columns.Add("REV_NO_TEXT", typeof(string));


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


            if (Session["dtSubitem"] != null)
                dtNew = (DataTable)Session["dtSubitem"];
            else
                AddTempSubitemTable();

            dtNew = (DataTable)Session["dtSubitem"];

            if (gvSubItem.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItem.Rows)
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


            if (Session["dsJobApprovers"] != null)
                dsJobApprovers = (DataSet)Session["dsJobApprovers"];
            else
                dsJobApprovers = objProject.GetJOBApprovers(jobNo, companyID, LOTMainSubItemIDs);


            if (dsJobApprovers.Tables.Count > 0)
            {
                if (dsJobApprovers.Tables[0].Rows.Count == 0)
                {
                    mpeAddApprovers.Show();
                    iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0&unitid=" + companyID + "");
                    return;
                }
            }
            else
            {
                mpeAddApprovers.Show();
                iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJOBNo.Text + "&actID=0&unitid=" + companyID + "");
                return;
            }




            if (dtNew.Rows.Count > 0)
            {
                foreach (DataRow dr in dtNew.Rows)
                {
                    statusID = 0;

                    subitemDesc = string.Empty;
                    tagNo = string.Empty;

                    LOTMainItem = string.Empty;
                    LOTMainSubItem = string.Empty;
                    LOTMainItemID = 0;
                    LOTMainSubItemID = 0;

                    drgNo = string.Empty;
                    revisionNo = 0;
                    revisionNoText = string.Empty;
                    quantity = 0;
                    productionOrderNo = string.Empty;
                    productionOrderDate = string.Empty;
                    expectedCompletionDate = string.Empty;
                    productCode = string.Empty;
                    productDesc = string.Empty;
                    UOM = string.Empty;
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


                    //if (amendmentCount > 0)
                    //    createdByID = amendedByID;

                    //objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, 0, PEID, PMID, prodMngrID);

                    //statusID = objLOTMailTypeAndStatusProperties.CurrentStatusID;
                    //approvedByID = objLOTMailTypeAndStatusProperties.ApprovedByID;
                    //acceptedByID = objLOTMailTypeAndStatusProperties.AcceptedByID;

                    statusID = GetStatusID(createdByID, PEID, PMID, prodMngrID);

                    if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
                        approvedByID = createdByID;

                    if (dr["SUBITEM_DESC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SUBITEM_DESC"])))
                        subitemDesc = Convert.ToString(dr["SUBITEM_DESC"]).Replace(Environment.NewLine, " ");

                    if (dr["TAG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["TAG_NO"])))
                        tagNo = Convert.ToString(dr["TAG_NO"]).Replace(Environment.NewLine, " ");

                    if (dr["LOT_MAIN_SUBITEM_ID"] != DBNull.Value && Convert.ToInt32(dr["LOT_MAIN_SUBITEM_ID"]) > 0)
                        LOTMainSubItemID = Convert.ToInt32(dr["LOT_MAIN_SUBITEM_ID"]);

                    if (dr["DRG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRG_NO"])))
                        drgNo = Convert.ToString(dr["DRG_NO"]);

                    if (dr["REV_NO"] != DBNull.Value && Convert.ToInt32(dr["REV_NO"]) > 0)
                        revisionNo = Convert.ToInt32(dr["REV_NO"]);

                    if (dr["REV_NO_TEXT"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REV_NO_TEXT"])))
                        revisionNoText = Convert.ToString(dr["REV_NO_TEXT"]);

                    if (dr["CATEGORY_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CATEGORY_ID"])))
                        categoryID = Convert.ToString(dr["CATEGORY_ID"]);



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

                    if (dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"] != DBNull.Value && Convert.ToInt32(dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]) > 0)
                        isPartOfProductionStatusID = Convert.ToInt32(dr["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);


                    //dtSubitemToAdd.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
                    //dtSubitemToAdd.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
                    //dtSubitemToAdd.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
                    //dtSubitemToAdd.Columns.Add("PRODUCT_CODE", typeof(string));
                    //dtSubitemToAdd.Columns.Add("PRODUCT_DESC", typeof(string));
                    //dtSubitemToAdd.Columns.Add("UOM", typeof(string));
                    //dtSubitemToAdd.Columns.Add("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID", typeof(string));


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
                        drn["LOT_MAIN_SUBITEM_ID"] = LOTMainSubItemID;
                    else drn["LOT_MAIN_SUBITEM_ID"] = 0;

                    if (!string.IsNullOrEmpty(drgNo))
                        drn["DRG_NO"] = drgNo;
                    else drn["DRG_NO"] = string.Empty;

                    if (revisionNo > 0)
                        drn["REV_NO"] = revisionNo;
                    else drn["REV_NO"] = 0;

                    if (!string.IsNullOrEmpty(revisionNoText))
                        drn["REV_NO_TEXT"] = revisionNoText;
                    else drn["REV_NO_TEXT"] = string.Empty;

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
                        drn["CREATED_ON"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        drn["CREATED_BY"] = 0;
                        drn["CREATED_ON"] = null;
                    }

                    if (approvedByID > 0)
                    {
                        drn["APPROVED_BY"] = approvedByID;
                        drn["APPROVED_ON"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
                        drn["ACCEPTED_ON"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
            }
            else
            {
                ExceptionMessage("Please select atleast 1 subitem...!!!");
                return;
            }

            #endregion


            bool checkForTAGNO = true;
            int isPartOfProductionCount = 0;

            LOTMainSubItemIDs = string.Empty;
            if (dtSubitemToAdd.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSubitemToAdd.Rows)
                {
                    if (!LOTMainSubItemIDs.Contains("," + Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"]) + ","))
                    {
                        LOTMainSubItemIDs += Convert.ToString(dr["LOT_MAIN_SUBITEM_ID"]) + ",";
                    }
                }
            }

            int value = 0;
            if (dtSubitemToAdd.Rows.Count > 0)
            {
                if (gvSubItem.Rows.Count > 0)
                {
                    foreach (GridViewRow gr in gvSubItem.Rows)
                    {
                        TextBox txtTagNoInList = gr.FindControl("txtTagNoInList") as TextBox;

                        if (string.IsNullOrEmpty(txtTagNoInList.Text.Trim()))
                        {
                            checkForTAGNO = false;
                            break;
                        }
                    }

                    //foreach (GridViewRow gr in gvSubItem.Rows)
                    //{
                    //    CheckBox chkIsPartOfProductionStatusReport = gr.FindControl("chkIsPartOfProductionStatusReport") as CheckBox;
                    //    if (chkIsPartOfProductionStatusReport.Checked)
                    //    {
                    //        isPartOfProductionCount++;
                    //    }
                    //}
                }


                //if (checkForTAGNO == true && isPartOfProductionCount > 0)
                if (checkForTAGNO == true)
                {
                    value = objProject.InsertLOTTransmittalToFactoryThree(TFNo, companyID, LOTDate, custCode, jobNo, poNo, itemName, impNotes,
                                                                    attachment1File, attachment1FileBytes,
                                                                    attachment2File, attachment2FileBytes,
                                                                    attachment3File, attachment3FileBytes,
                                                                    attachment4File, attachment4FileBytes,
                                                                    dtSubitemToAdd, savingType, createdByID);
                }
                else
                {
                    if (!checkForTAGNO)
                    {
                        ExceptionMessage("TAG number is empty in subitem list...!!!");
                        return;
                    }

                    //if (isPartOfProductionCount == 0)
                    //{
                    //    ExceptionMessage("Please select atleast 1 subitem as part of production in subitem list...!!!");
                    //    return;
                    //}
                }
            }
            else
            {
                ExceptionMessage("Please select atleast 1 subitem...!!!");
                return;
            }

            if (value > 0)
            {
                if (savingType == Convert.ToInt32(LOTAllStatusAndTypes.SavingType.SaveAndSendForApproval))
                {
                    if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
                        LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');

                    //int sendMailValue = objLOTSendMail.SendEmailLOT(value, LOTMainSubItemIDs, txtTFNo.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", 0, 0, 0, 0, null);
                    //int sendMailValue = objLOTSendMail.SendEmailLOT(value, "", txtTFNo.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", 0, 0, 0, 0, null, null,0,0);

                    int sendMailValue = objLOTSendMail.SendEmailLOT(value, "", txtTFNo.Text, companyID, "", 0, 0, null, 0, 0, 0);

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(value, LOTMainSubItemIDs, statusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        SuccessMessage("LOT saved with TF. No.: '" + TFNo + "' and mail sent successfully.");
                        Reset();
                    }
                    else
                    {
                        SuccessMessage("LOT saved with TF. No.: '" + TFNo + "' successfully.");
                        Reset();
                    }
                }
                else
                {
                    SuccessMessage("LOT saved with TF. No.: '" + TFNo + "' successfully.");
                    Reset();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

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

    private void ViewDrawingFiles(int srNo, string fileName, string fileType)
    {
        try
        {
            byte[] fileBytes = null;
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                foreach (DataRow dr in dtTemp.Select("SR_NO='" + srNo + "'"))
                {
                    fileBytes = (byte[])dr["SI_ATTACHMENT1_BTYTES"];
                }

                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewAttachedDrawingImageFile.ashx?srNo=" + srNo + "&fileType=" + fileType;
                    mpeViewImgFileAttachment.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewAttachedDrawingPDFFile.aspx?srNo=" + srNo + "&fileType=" + fileType);
                    this.mpeViewPDFFileAttachment.Show();
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


    private void Reset()
    {
        try
        {
            ddlLOTMainItems.SelectedIndex = 0;
            ddlLOTMainSubItems.SelectedIndex = 0;
            txtCustomerCode.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtJOBNo.Text = string.Empty;
            txtPONo.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtTFNo.Text = string.Empty;
            txtNotes.Text = string.Empty;

            gvJOBDetail.DataSource = null;
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ResetSubitems()
    {
        ddlProductCode.Items.Clear();
        ddlProductCode.Items.Insert(0, "Select");
        ddlProductCode.SelectedIndex = 0;
        txtProductionOrderDate.Text = string.Empty;
        txtUOM.Text = string.Empty;
        txtProductDesc.Text = string.Empty;
        chkIsPartOfProductionOrMainDrawing.Checked = false;

        ddlLOTMainItems.SelectedIndex = 0;

        ddlLOTMainSubItems.Items.Clear();
        ddlLOTMainSubItems.Items.Insert(0, "Select");
        ddlLOTMainSubItems.SelectedIndex = 0;

        txtDescription.Text = string.Empty;

        ddlRevNo.SelectedIndex = 0;
        ddlRevNo.Enabled = true;
        txtRevNoText.Text = "00";
        txtRevNoText.Enabled = false;
        hdRevNoText.Value = "";
        hdRevNoTextOld.Value = "";


        txtDrgNo.Text = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in chkLstCategory.Items)
        {
            item.Selected = false;
        }
        txtQuantity.Text = string.Empty;
        txtTagNo.Text = string.Empty;
        txtProductionOrderNo.Text = string.Empty;
        txtExpectedCompletionDate.Text = string.Empty;
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

    #endregion



}