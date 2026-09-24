using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PROJECT_MACHINE_SCH_AssignItemsForMS : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.MachineScheduling objMS = new BAL.MachineScheduling();
    BAL.Common objCommon = new BAL.Common();
    MSSendMail objMSSendMail = new MSSendMail();

    DataSet dsJobNo = new DataSet();
    DataSet dsDrawing = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsCategory = new DataSet();

    int companyID = 0;
    string LOTNo = string.Empty;
    string jobNo = string.Empty;
    string drawingNo = string.Empty;
    string equipment = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtSubitemDetails"] = null;
                HideMessagePanel();
                hdDateToA.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDateToA.Text = hdDateToA.Value;
                BindUnit();
                BindCategory();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlCompanyToA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }


    protected void ddlCategoryToA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();

        btnGetJOBNo.Visible = true;
        btnGetDrawingNo.Visible = true;
        if (ddlCategoryToA.SelectedIndex == 0)
        {
            btnGetJOBNo.Visible = false;
            btnGetDrawingNo.Visible = false;
        }
        else
        {
            if (Convert.ToInt32(ddlCategoryToA.SelectedValue) == (int)MSAllStatusAndTypes.EnumCatetory.Equipment)
            {
                txtDrawingNoToA.Enabled = false;
                btnGetDrawingNo.Visible = true;
            }
            else
            {
                txtDrawingNoToA.Enabled = true;
                btnGetDrawingNo.Visible = false;
            }
        }
    }


    // JOB DETAILS
    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        if (ddlCategoryToA.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlCategoryToA.SelectedValue) == (int)MSAllStatusAndTypes.EnumCatetory.Equipment)
                mpeJobDetailLOT.Show();
            else
            {
                txtUnitSearchFact.Text = ddlCompanyToA.SelectedItem.Text;
                mpeJobDetailFact.Show();
            }
            GetJOBDetail(Convert.ToInt32(ddlCategoryToA.SelectedValue));
        }
    }

    protected void btnSearchJOBNoLOT_Click(object sender, EventArgs e)
    {
        mpeJobDetailLOT.Show();
        GetJOBDetail((int)MSAllStatusAndTypes.EnumCatetory.Equipment);
    }

    protected void btnSearchJOBNoFact_Click(object sender, EventArgs e)
    {
        mpeJobDetailFact.Show();
        GetJOBDetail((int)MSAllStatusAndTypes.EnumCatetory.Piping);
    }


    protected void gvJobDetailLOT_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblLOTTFID = gvJobDetailLOT.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblJOBNo = gvJobDetailLOT.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblLOTno = gvJobDetailLOT.Rows[rowindex].FindControl("lblLOTno") as Label;

                hdLOTTFID.Value = Convert.ToString(lblLOTTFID.Text);
                lblLOTNo.Text = Convert.ToString(lblLOTno.Text);
                txtJOBNoToA.Text = Convert.ToString(lblJOBNo.Text).Trim();
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


    protected void gvJobDetailFact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;
                Label lblJOBNo = gvJobDetailFact.Rows[rowindex].FindControl("lblJOBNo") as Label;
                txtJOBNoToA.Text = Convert.ToString(lblJOBNo.Text).Trim();
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


    protected void btnGetDrawingNo_Click(object sender, EventArgs e)
    {
        txtJOBNoInDr.Text = txtJOBNoToA.Text;
        txtLOTNoInDr.Text = Convert.ToString(lblLOTNo.Text);
        txtDrawingNoInDr.Text = string.Empty;
        txtEquipmentInDr.Text = string.Empty;
        mpeDrawingDetail.Show();
        GetDrawingDetail();
    }

    protected void btnSearchDrawingNo_Click(object sender, EventArgs e)
    {
        mpeDrawingDetail.Show();
        GetDrawingDetail();
    }

    protected void gvDrawingDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                //hdLOTTFSubitemDrawing1.Value = string.Empty;
                //hdLOTTFSubitemDrawing2.Value = string.Empty;
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblLOTTFSubitemIDInList = gvDrawingDetail.Rows[rowindex].FindControl("lblLOTTFSubitemIDInList") as Label;
                Label lblLOTSIDrawigName1 = gvDrawingDetail.Rows[rowindex].FindControl("lblLOTSIDrawigName1") as Label;
                Label lblLOTSIDrawigName2 = gvDrawingDetail.Rows[rowindex].FindControl("lblLOTSIDrawigName2") as Label;
                Label lblDrawingNo = gvDrawingDetail.Rows[rowindex].FindControl("lblDrawingNo") as Label;
                Label lblEquipment = gvDrawingDetail.Rows[rowindex].FindControl("lblEquipment") as Label;
                Label lblTagNo = gvDrawingDetail.Rows[rowindex].FindControl("lblTagNo") as Label;
                TextBox txtQuantity = gvDrawingDetail.Rows[rowindex].FindControl("txtQuantity") as TextBox;


                if (!string.IsNullOrEmpty(Convert.ToString(lblLOTTFSubitemIDInList.Text)))
                    hdLOTTFSubitemID.Value = Convert.ToString(lblLOTTFSubitemIDInList.Text);
                else hdLOTTFSubitemID.Value = "0";

                lblSIDrawing1.Text = Convert.ToString(lblLOTSIDrawigName1.Text);
                lblSIDrawing2.Text = Convert.ToString(lblLOTSIDrawigName2.Text);

                lblSIDrawing1.Text = Convert.ToString(lblLOTSIDrawigName1.Text);
                lblSIDrawing2.Text = Convert.ToString(lblLOTSIDrawigName2.Text);

                txtDrawingNoToA.Text = Convert.ToString(lblDrawingNo.Text).Trim();
                txtDrawingNoToA.ToolTip = Convert.ToString(lblDrawingNo.Text).Trim();
                txtEquipmentToA.Text = Convert.ToString(lblEquipment.Text).Trim();
                txtQuantityToA.Text = txtQuantity.Text;
                txtTagNoToA.Text = Convert.ToString(lblTagNo.Text).Trim();
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





    protected void btnAddToList_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        AddItemDetailsToList();
        lblSRNoToA.Text = "0";
        pnlUploadAddDrawing.Visible = true;
        pnlViewAddDrawing.Visible = false;
    }


    protected void imgBtnViewAddDrawing_Click(object sender, EventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(lblSRNoToA.Text), "ADD_DRAWING");
    }

    protected void imgBtnRemoveAddDrawing_Click(object sender, ImageClickEventArgs e)
    {
        pnlViewAddDrawing.Visible = false;
        pnlUploadAddDrawing.Visible = true;
    }
    protected void imgBtnUndoAddDrawing_Click(object sender, ImageClickEventArgs e)
    {
        pnlViewAddDrawing.Visible = true;
        pnlUploadAddDrawing.Visible = false;
    }



    protected void gvSubitemsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblLOTSIDrawigName1 = (Label)e.Row.FindControl("lblLOTSIDrawigName1");
            Label lblLOTSIDrawigName2 = (Label)e.Row.FindControl("lblLOTSIDrawigName2");

            ImageButton imgBtnViewAdditionalDrawing = (ImageButton)e.Row.FindControl("imgBtnViewAdditionalDrawing");
            ImageButton imgBtnViewDrawing1 = (ImageButton)e.Row.FindControl("imgBtnViewDrawing1");
            ImageButton imgBtnViewDrawing2 = (ImageButton)e.Row.FindControl("imgBtnViewDrawing2");



            TextBox txtUnit = (TextBox)e.Row.FindControl("txtUnit");
            TextBox txtJOBNo = (TextBox)e.Row.FindControl("txtJOBNo");
            TextBox txtDrawingNo = (TextBox)e.Row.FindControl("txtDrawingNo");
            TextBox txtEquipment = (TextBox)e.Row.FindControl("txtEquipment");
            TextBox txtItemName = (TextBox)e.Row.FindControl("txtItemName");
            TextBox txtItemDetail = (TextBox)e.Row.FindControl("txtItemDetail");
            TextBox txtExpectedDateofComp = (TextBox)e.Row.FindControl("txtExpectedDateofComp");
            TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
            TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
            TextBox txtAdditinoalDrawingFilePath = (TextBox)e.Row.FindControl("txtAdditinoalDrawingFilePath");




            txtAdditinoalDrawingFilePath.ToolTip = txtAdditinoalDrawingFilePath.Text;

            txtAdditinoalDrawingFilePath.Visible = false;
            imgBtnViewAdditionalDrawing.Visible = false;
            imgBtnViewDrawing1.Visible = false;
            imgBtnViewDrawing2.Visible = false;

            if (!string.IsNullOrEmpty(txtAdditinoalDrawingFilePath.Text))
            {
                //txtAdditinoalDrawingFilePath.Visible = true;
                imgBtnViewAdditionalDrawing.Visible = true;
                imgBtnViewAdditionalDrawing.ToolTip = txtAdditinoalDrawingFilePath.Text;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(lblLOTSIDrawigName1.Text)))
            {
                imgBtnViewDrawing1.Visible = true;
                imgBtnViewDrawing1.ToolTip = Convert.ToString(lblLOTSIDrawigName1.Text);
            }

            if (!string.IsNullOrEmpty(Convert.ToString(lblLOTSIDrawigName2.Text)))
            {
                imgBtnViewDrawing2.Visible = true;
                imgBtnViewDrawing2.ToolTip = Convert.ToString(lblLOTSIDrawigName2.Text);
            }
        }
    }

    protected void gvSubitemsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            hdIsNewRecord.Value = "0";
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE" ||
                    Convert.ToString(e.CommandArgument) == "ViewADDDRAWING" ||
                    Convert.ToString(e.CommandArgument) == "ViewSIDRAWING1" ||
                    Convert.ToString(e.CommandArgument) == "ViewSIDRAWING2")
                {

                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                TextBox txtSrNo = gvSubitemsList.Rows[rowindex].FindControl("txtSrNo") as TextBox;

                Label lblLOTTFID = gvSubitemsList.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblLOTTFSubitemIDInList = gvSubitemsList.Rows[rowindex].FindControl("lblLOTTFSubitemIDInList") as Label;
                Label lblUnitID = gvSubitemsList.Rows[rowindex].FindControl("lblUnitID") as Label;

                TextBox txtUnit = gvSubitemsList.Rows[rowindex].FindControl("txtUnit") as TextBox;
                TextBox txtJOBNo = gvSubitemsList.Rows[rowindex].FindControl("txtJOBNo") as TextBox;
                TextBox txtDrawingNo = gvSubitemsList.Rows[rowindex].FindControl("txtDrawingNo") as TextBox;
                TextBox txtEquipment = gvSubitemsList.Rows[rowindex].FindControl("txtEquipment") as TextBox;
                TextBox txtItemName = gvSubitemsList.Rows[rowindex].FindControl("txtItemName") as TextBox;
                TextBox txtItemDetail = gvSubitemsList.Rows[rowindex].FindControl("txtItemDetail") as TextBox;
                TextBox txtExpectedDateofComp = gvSubitemsList.Rows[rowindex].FindControl("txtExpectedDateofComp") as TextBox;
                TextBox txtQuantity = gvSubitemsList.Rows[rowindex].FindControl("txtQuantity") as TextBox;
                TextBox txtRemarks = gvSubitemsList.Rows[rowindex].FindControl("txtRemarks") as TextBox;
                TextBox txtAdditinoalDrawingFilePath = gvSubitemsList.Rows[rowindex].FindControl("txtAdditinoalDrawingFilePath") as TextBox;


                foreach (GridViewRow gr in gvSubitemsList.Rows)
                {
                    TextBox txtUnitGr = (TextBox)gr.FindControl("txtUnit");
                    TextBox txtJOBNoGr = (TextBox)gr.FindControl("txtJOBNo");
                    TextBox txtDrawingNoGr = (TextBox)gr.FindControl("txtDrawingNo");
                    TextBox txtEquipmentGr = (TextBox)gr.FindControl("txtEquipment");
                    TextBox txtItemNameGr = (TextBox)gr.FindControl("txtItemName");
                    TextBox txtItemDetailGr = (TextBox)gr.FindControl("txtItemDetail");
                    TextBox txtExpectedDateofCompGr = (TextBox)gr.FindControl("txtExpectedDateofComp");
                    TextBox txtQuantityGr = (TextBox)gr.FindControl("txtQuantity");
                    TextBox txtRemarksGr = (TextBox)gr.FindControl("txtRemarks");
                    TextBox txtAdditinoalDrawingFilePathGr = (TextBox)gr.FindControl("txtAdditinoalDrawingFilePath");

                    txtUnitGr.BackColor = System.Drawing.Color.LightYellow;
                    txtJOBNoGr.BackColor = System.Drawing.Color.LightYellow;
                    txtDrawingNoGr.BackColor = System.Drawing.Color.LightYellow;
                    txtEquipmentGr.BackColor = System.Drawing.Color.LightYellow;
                    txtItemNameGr.BackColor = System.Drawing.Color.LightYellow;
                    txtItemDetailGr.BackColor = System.Drawing.Color.LightYellow;
                    txtExpectedDateofCompGr.BackColor = System.Drawing.Color.LightYellow;
                    txtQuantityGr.BackColor = System.Drawing.Color.LightYellow;
                    txtRemarksGr.BackColor = System.Drawing.Color.LightYellow;

                    gr.BackColor = System.Drawing.Color.Transparent;
                }


                imgBtnUndoAddDrawing.Visible = false;
                if (Convert.ToString(e.CommandArgument) == "EDIT")
                {
                    btnAddToList.Text = "Update to List";

                    if (!string.IsNullOrEmpty(Convert.ToString(txtAdditinoalDrawingFilePath.Text)))
                    {
                        pnlUploadAddDrawing.Visible = false;
                        pnlViewAddDrawing.Visible = true;
                        imgBtnUndoAddDrawing.Visible = true;
                    }
                    else
                    {
                        pnlUploadAddDrawing.Visible = true;
                        pnlViewAddDrawing.Visible = false;
                    }



                    lblSRNoToA.Text = Convert.ToString(txtSrNo.Text);

                    hdLOTTFID.Value = Convert.ToString(lblLOTTFID.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblLOTTFSubitemIDInList.Text)))
                        hdLOTTFSubitemID.Value = Convert.ToString(lblLOTTFSubitemIDInList.Text);
                    else hdLOTTFSubitemID.Value = "0";

                    ddlCompanyToA.SelectedValue = Convert.ToString(lblUnitID.Text);
                    txtJOBNoToA.Text = Convert.ToString(txtJOBNo.Text);
                    txtAddDrawingFilePathToA.Text = Convert.ToString(txtAdditinoalDrawingFilePath.Text);
                    txtDrawingNoToA.Text = Convert.ToString(txtDrawingNo.Text);
                    txtEquipmentToA.Text = Convert.ToString(txtEquipment.Text);
                    txtItemNameToA.Text = Convert.ToString(txtItemName.Text);
                    txtItemDetailToA.Text = Convert.ToString(txtItemDetail.Text);
                    txtExpectedDateofComp.Text = Convert.ToString(txtExpectedDateofComp.Text);
                    txtQuantityToA.Text = Convert.ToString(txtQuantity.Text);
                    txtRemarkstoA.Text = Convert.ToString(txtRemarks.Text);

                    txtUnit.BackColor = System.Drawing.Color.LightGreen;
                    txtJOBNo.BackColor = System.Drawing.Color.LightGreen;
                    txtDrawingNo.BackColor = System.Drawing.Color.LightGreen;
                    txtEquipment.BackColor = System.Drawing.Color.LightGreen;
                    txtItemName.BackColor = System.Drawing.Color.LightGreen;
                    txtItemDetail.BackColor = System.Drawing.Color.LightGreen;
                    txtExpectedDateofComp.BackColor = System.Drawing.Color.LightGreen;
                    txtQuantity.BackColor = System.Drawing.Color.LightGreen;
                    txtRemarks.BackColor = System.Drawing.Color.LightGreen;

                    gvSubitemsList.Rows[rowindex].BackColor = System.Drawing.Color.LightGreen;
                }

                else if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    lblSRNoToA.Text = "0";
                    btnAddToList.Text = "Add to List";
                    DataTable dtToRemoveRecord = (DataTable)Session["dtSubitemDetails"];
                    RemoveRecords(dtToRemoveRecord, Convert.ToString(txtSrNo.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewADDDRAWING")
                    ViewDrawingFiles(Convert.ToInt32(txtSrNo.Text), "ADD_DRAWING");

                else if (Convert.ToString(e.CommandArgument) == "ViewSIDRAWING1")
                    ViewSIDrawingFiles(Convert.ToInt32(lblLOTTFSubitemIDInList.Text), "SI_DRAWING1");

                else if (Convert.ToString(e.CommandArgument) == "ViewSIDRAWING2")
                    ViewSIDrawingFiles(Convert.ToInt32(lblLOTTFSubitemIDInList.Text), "SI_DRAWING2");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        SaveItemsDetails(0);
        pnlUploadAddDrawing.Visible = true;
        pnlViewAddDrawing.Visible = false;
    }


    protected void btnSaveFromList_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        SaveItemsDetails(1);
        pnlUploadAddDrawing.Visible = true;
        pnlViewAddDrawing.Visible = false;
    }

    #endregion


    #region METHODS[=======================]



    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompanyToA.DataSource = dsUnit.Tables[0];
                ddlCompanyToA.DataTextField = "UNIT_NAME";
                ddlCompanyToA.DataValueField = "UNIT_ID";
                ddlCompanyToA.DataBind();
                ddlCompanyToA.SelectedIndex = 0;
            }
            else
            {
                ddlCompanyToA.Items.Clear();
                ddlCompanyToA.Items.Insert(0, "Select");
                ddlCompanyToA.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCategory()
    {
        try
        {
            dsCategory = objMS.GetCategoryList();
            if (dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategoryToA.DataSource = dsCategory.Tables[0];
                ddlCategoryToA.DataTextField = "CATEGORY_NAME";
                ddlCategoryToA.DataValueField = "CATEGORY_PID";
                ddlCategoryToA.DataBind();
                ddlCategoryToA.Items.Insert(0, "Select");
                ddlCategoryToA.SelectedIndex = 0;
            }
            else
            {
                ddlCategoryToA.Items.Clear();
                ddlCategoryToA.Items.Insert(0, "Select");
                ddlCategoryToA.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    //JOB Details Start------------------------
    private DataSet GetJOBData(int typeId)
    {
        try
        {
            companyID = 0;
            LOTNo = string.Empty;
            jobNo = string.Empty;

            companyID = Convert.ToInt32(ddlCompanyToA.SelectedValue);

            if (!string.IsNullOrEmpty(txtLOTNoSearchLOT.Text))
                LOTNo = txtLOTNoSearchLOT.Text;

            if (typeId == (int)MSAllStatusAndTypes.EnumCatetory.Equipment)
            {
                if (!string.IsNullOrEmpty(txtJOBNoSearchLOT.Text))
                    jobNo = txtJOBNoSearchLOT.Text;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtJOBNoSearchFact.Text))
                    jobNo = txtJOBNoSearchFact.Text;
            }

            dsJobNo = objMS.GetJOBDetailsForLMS(companyID, jobNo, LOTNo, typeId);

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


    private void GetJOBDetail(int typeId)
    {
        try
        {
            dsJobNo = GetJOBData(typeId);

            if (dsJobNo == null)
            {
                lblJOBMsgLOT.Visible = true;
                lblJOBMsgLOT.Text = "No data found!";
                lblJOBRecordsFact.Text = "Records[0]";
                return;
            }

            if (typeId == (int)MSAllStatusAndTypes.EnumCatetory.Equipment)
            {
                if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
                {
                    lblJOBMsgLOT.Visible = false;
                    lblJOBMsgLOT.Text = string.Empty;
                    gvJobDetailLOT.DataSource = dsJobNo.Tables[0];
                    gvJobDetailLOT.DataBind();
                }
                else
                {
                    lblJOBMsgLOT.Visible = true;
                    lblJOBMsgLOT.Text = "No data found!";
                    gvJobDetailLOT.DataSource = null;
                    gvJobDetailLOT.DataBind();
                }
                lblJOBRecordsLOT.Text = "Records[" + gvJobDetailLOT.Rows.Count + "]";
            }
            else
            {
                if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
                {
                    lblJOBMsgFact.Visible = false;
                    lblJOBMsgFact.Text = string.Empty;
                    gvJobDetailFact.DataSource = dsJobNo.Tables[0];
                    gvJobDetailFact.DataBind();
                }
                else
                {
                    lblJOBMsgFact.Visible = true;
                    lblJOBMsgFact.Text = "No data found!";
                    gvJobDetailFact.DataSource = null;
                    gvJobDetailFact.DataBind();
                }
                lblJOBRecordsFact.Text = "Records[" + gvJobDetailFact.Rows.Count + "]";
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }




    //Drawing Details Start------------------------
    private DataSet GetDrawingData()
    {
        try
        {
            LOTNo = string.Empty;
            jobNo = string.Empty;
            drawingNo = string.Empty;
            equipment = string.Empty;

            if (!string.IsNullOrEmpty(txtLOTNoInDr.Text))
                LOTNo = txtLOTNoInDr.Text;

            if (!string.IsNullOrEmpty(txtJOBNoInDr.Text))
                jobNo = txtJOBNoInDr.Text;

            if (!string.IsNullOrEmpty(txtDrawingNoInDr.Text))
                drawingNo = txtDrawingNoInDr.Text;

            if (!string.IsNullOrEmpty(txtEquipmentInDr.Text))
                equipment = txtEquipmentInDr.Text;


            dsDrawing = objMS.GetDrawingDetailsForLMS(jobNo, LOTNo, drawingNo, equipment);

            if (dsDrawing.Tables.Count > 0)
            {
                return dsDrawing;
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


    private void GetDrawingDetail()
    {
        try
        {
            dsDrawing = GetDrawingData();
            if (dsDrawing.Tables.Count > 0 && dsDrawing.Tables[0].Rows.Count > 0)
            {
                lblDrawingMsg.Visible = false;
                lblDrawingMsg.Text = string.Empty;
                gvDrawingDetail.DataSource = dsDrawing.Tables[0];
                gvDrawingDetail.DataBind();
            }
            else
            {
                lblDrawingMsg.Visible = true;
                lblDrawingMsg.Text = "No data found!";
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



    private void AddItemDetailsToList()
    {
        try
        {
            int lotTFID = 0;
            int lotTFSubitemID = 0;
            string lotTFSubitemDrawing1 = string.Empty;
            string lotTFSubitemDrawing2 = string.Empty;
            int unitID = 0;
            int categoryID = 0;
            string categoryName = string.Empty;
            string unitName = string.Empty;
            string jobNo = string.Empty;
            string drawingNo = string.Empty;
            string equipmentNo = string.Empty;
            string tagNo = string.Empty;
            string itemName = string.Empty;
            string itemDesc = string.Empty;
            string expectedDateOfCompByPlanning = string.Empty;
            int quantity = 0;
            string remarks = string.Empty;
            string additionalDrawingFileName = string.Empty;
            Byte[] additionalDrawingBytes = null;


            unitID = Convert.ToInt32(ddlCompanyToA.SelectedValue);
            unitName = Convert.ToString(ddlCompanyToA.SelectedItem.Text);

            if (ddlCategoryToA.SelectedIndex > 0)
            {
                categoryID = Convert.ToInt32(ddlCategoryToA.SelectedValue);
                categoryName = Convert.ToString(ddlCategoryToA.SelectedItem.Text);
            }
            else categoryID = 0;


            if (!string.IsNullOrEmpty(txtJOBNoToA.Text))
            {
                if (!string.IsNullOrEmpty(hdLOTTFID.Value))
                    lotTFID = Convert.ToInt32(hdLOTTFID.Value);

                jobNo = txtJOBNoToA.Text.Trim().ToUpper();
            }

            //if (chkNA.Checked == false)
            //{
            if (!string.IsNullOrEmpty(txtDrawingNoToA.Text))
                drawingNo = txtDrawingNoToA.Text.Trim().ToUpper();



            if (!string.IsNullOrEmpty(txtEquipmentToA.Text))
            {
                equipmentNo = txtEquipmentToA.Text.Trim().ToUpper();
                tagNo = txtTagNoToA.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(Convert.ToString(hdLOTTFSubitemID.Value)))
                    lotTFSubitemID = Convert.ToInt32(hdLOTTFSubitemID.Value);
            }
            //}

            if (!string.IsNullOrEmpty(Convert.ToString(lblSIDrawing1.Text)))
                lotTFSubitemDrawing1 = Convert.ToString(lblSIDrawing1.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(lblSIDrawing2.Text)))
                lotTFSubitemDrawing2 = Convert.ToString(lblSIDrawing2.Text);


            quantity = Convert.ToInt32(txtQuantityToA.Text);

            if (!string.IsNullOrEmpty(txtItemNameToA.Text))
                itemName = txtItemNameToA.Text.Trim();

            if (!string.IsNullOrEmpty(txtItemDetailToA.Text))
                itemDesc = txtItemDetailToA.Text.Trim();

            expectedDateOfCompByPlanning = txtDateToA.Text;

            if (!string.IsNullOrEmpty(txtRemarkstoA.Text))
                remarks = txtRemarkstoA.Text.Trim();


            if (uploadFileAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachment1.PostedFile.FileName))
                {
                    additionalDrawingFileName = uploadFileAttachment1.PostedFile.FileName;
                    additionalDrawingBytes = GetFileBytes(uploadFileAttachment1.PostedFile.FileName, uploadFileAttachment1.PostedFile.InputStream);
                }
                else
                {
                    additionalDrawingFileName = string.Empty;
                    additionalDrawingBytes = null;
                }
            }
            else
            {
                additionalDrawingFileName = string.Empty;
                additionalDrawingBytes = null;
            }



            DataTable dtSubitemDetails = new DataTable();

            if (dtSubitemDetails.Columns.Count == 0)
            {
                dtSubitemDetails.Columns.Add("SR_NO", typeof(string));
                dtSubitemDetails.Columns.Add("LOT_TF_ID", typeof(int));
                dtSubitemDetails.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
                dtSubitemDetails.Columns.Add("SI_ATTACHMENT1_NAME", typeof(string));
                dtSubitemDetails.Columns.Add("SI_ATTACHMENT2_NAME", typeof(string));
                dtSubitemDetails.Columns.Add("UNIT_ID", typeof(int));
                dtSubitemDetails.Columns.Add("UNIT_NAME", typeof(string));
                dtSubitemDetails.Columns.Add("JOB_NO", typeof(string));
                dtSubitemDetails.Columns.Add("DRAWING_NO", typeof(string));
                dtSubitemDetails.Columns.Add("EQUIPMENT", typeof(string));
                dtSubitemDetails.Columns.Add("TAG_NO", typeof(string));
                dtSubitemDetails.Columns.Add("ITEM_NAME", typeof(string));
                dtSubitemDetails.Columns.Add("ITEM_DETAIL", typeof(string));
                dtSubitemDetails.Columns.Add("EXPECTED_DATE_OF_COMP_BY_PLANNING", typeof(string));
                dtSubitemDetails.Columns.Add("ALLOCATED_QUANTITY", typeof(string));
                dtSubitemDetails.Columns.Add("REMARKS", typeof(string));
                dtSubitemDetails.Columns.Add("ADD_DRAWING_FILE_NAME", typeof(string));
                dtSubitemDetails.Columns.Add("ADD_DRAWING_FILE_BYTES", typeof(byte[]));
                dtSubitemDetails.Columns.Add("CATEGORY_ID", typeof(int));
                dtSubitemDetails.Columns.Add("CATEGORY_NAME", typeof(string));
            }

            if (Session["dtSubitemDetails"] != null)
            {
                dtSubitemDetails = (DataTable)Session["dtSubitemDetails"];
            }


            if (Convert.ToInt32(lblSRNoToA.Text) > 0)
            {
                foreach (DataRow dr in dtSubitemDetails.Select("SR_NO=" + Convert.ToInt32(lblSRNoToA.Text)))
                {
                    dr["LOT_TF_ID"] = lotTFID;
                    dr["LOT_TF_SUBITEM_ID"] = lotTFSubitemID;
                    dr["SI_ATTACHMENT1_NAME"] = lotTFSubitemDrawing1;
                    dr["SI_ATTACHMENT2_NAME"] = lotTFSubitemDrawing2;
                    dr["UNIT_ID"] = unitID;
                    dr["UNIT_NAME"] = unitName;
                    dr["JOB_NO"] = jobNo;
                    dr["DRAWING_NO"] = drawingNo;
                    dr["EQUIPMENT"] = equipmentNo;
                    dr["TAG_NO"] = tagNo;
                    dr["ITEM_NAME"] = itemName;
                    dr["ITEM_DETAIL"] = itemDesc;
                    dr["EXPECTED_DATE_OF_COMP_BY_PLANNING"] = expectedDateOfCompByPlanning;
                    dr["ALLOCATED_QUANTITY"] = quantity;
                    dr["REMARKS"] = remarks;


                    if (txtAddDrawingFilePathToA.Visible == false)
                    {
                        if (uploadFileAttachment1.HasFile)
                        {
                            if (!string.IsNullOrEmpty(uploadFileAttachment1.PostedFile.FileName))
                            {
                                additionalDrawingFileName = uploadFileAttachment1.PostedFile.FileName;
                                additionalDrawingBytes = GetFileBytes(uploadFileAttachment1.PostedFile.FileName, uploadFileAttachment1.PostedFile.InputStream);
                            }
                            else
                            {
                                additionalDrawingFileName = string.Empty;
                                additionalDrawingBytes = null;
                            }
                        }
                        else
                        {
                            additionalDrawingFileName = string.Empty;
                            additionalDrawingBytes = null;
                        }

                        dr["ADD_DRAWING_FILE_NAME"] = additionalDrawingFileName;
                        dr["ADD_DRAWING_FILE_BYTES"] = additionalDrawingBytes;

                        dr["CATEGORY_ID"] = categoryID;
                        dr["CATEGORY_NAME"] = categoryName;
                    }
                }

                lblSRNoToA.Text = "0";
            }
            else
            {
                DataRow dr = dtSubitemDetails.NewRow();

                dr["LOT_TF_ID"] = lotTFID;
                dr["LOT_TF_SUBITEM_ID"] = lotTFSubitemID;
                dr["SI_ATTACHMENT1_NAME"] = lotTFSubitemDrawing1;
                dr["SI_ATTACHMENT2_NAME"] = lotTFSubitemDrawing2;
                dr["UNIT_ID"] = unitID;
                dr["UNIT_NAME"] = unitName;
                dr["JOB_NO"] = jobNo;
                dr["DRAWING_NO"] = drawingNo;
                dr["EQUIPMENT"] = equipmentNo;
                dr["TAG_NO"] = tagNo;
                dr["ITEM_NAME"] = itemName;
                dr["ITEM_DETAIL"] = itemDesc;
                dr["EXPECTED_DATE_OF_COMP_BY_PLANNING"] = expectedDateOfCompByPlanning;
                dr["ALLOCATED_QUANTITY"] = quantity;
                dr["REMARKS"] = remarks;
                dr["ADD_DRAWING_FILE_NAME"] = additionalDrawingFileName;
                dr["ADD_DRAWING_FILE_BYTES"] = additionalDrawingBytes;
                dr["CATEGORY_ID"] = categoryID;
                dr["CATEGORY_NAME"] = categoryName;

                dtSubitemDetails.Rows.Add(dr);
            }


            if (dtSubitemDetails.Rows.Count > 0)
            {
                int count = 1;
                foreach (DataRow drs in dtSubitemDetails.Rows)
                {
                    drs["SR_NO"] = count++;
                }

                Session["dtSubitemDetails"] = dtSubitemDetails;
                gvSubitemsList.DataSource = dtSubitemDetails;
                gvSubitemsList.DataBind();
            }

            lblRecords.Text = "Records [" + gvSubitemsList.Rows.Count + "]";

            Reset();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void SaveItemsDetails(int typeID)
    {
        try
        {
            int lotTFID = 0;
            int lotTFSubitemID = 0;
            int unitID = 0;
            int categoryID = 0;
            string unitName = string.Empty;
            string jobNo = string.Empty;
            string drawingNo = string.Empty;
            string equipmentNo = string.Empty;
            string tagNo = string.Empty;
            string itemName = string.Empty;
            string itemDesc = string.Empty;
            string expectedDateOfCompByPlanning = string.Empty;
            int quantity = 0;
            string remarks = string.Empty;
            string additionalDrawingFileName = string.Empty;
            Byte[] additionalDrawingBytes = null;

            int assignedDrawingCounts = objMS.GetTableCounts();

            DataTable dtSubitems = new DataTable();

            dtSubitems.Columns.Add("ASSIGNED_DRAWING_CODE", typeof(string));
            dtSubitems.Columns.Add("LOT_TF_ID", typeof(int));
            dtSubitems.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
            dtSubitems.Columns.Add("UNIT_ID", typeof(int));
            dtSubitems.Columns.Add("JOB_NO", typeof(string));
            dtSubitems.Columns.Add("DRAWING_NO", typeof(string));
            dtSubitems.Columns.Add("EQUIPMENT", typeof(string));
            dtSubitems.Columns.Add("TAG_NO", typeof(string));
            dtSubitems.Columns.Add("ITEM_NAME", typeof(string));
            dtSubitems.Columns.Add("ITEM_DETAIL", typeof(string));
            dtSubitems.Columns.Add("EXPECTED_DATE_OF_COMP_BY_PLANNING", typeof(string));
            dtSubitems.Columns.Add("ALLOCATED_QUANTITY", typeof(int));
            dtSubitems.Columns.Add("REMARKS", typeof(string));
            dtSubitems.Columns.Add("ADD_DRAWING_FILE_NAME", typeof(string));
            dtSubitems.Columns.Add("ADD_DRAWING_FILE_BYTES", typeof(byte[]));
            dtSubitems.Columns.Add("CATEGORY_ID", typeof(int));
            if (typeID == 0)
            {
                unitID = Convert.ToInt32(ddlCompanyToA.SelectedValue);
                unitName = Convert.ToString(ddlCompanyToA.SelectedItem.Text);

                if (ddlCategoryToA.SelectedIndex > 0)
                    categoryID = Convert.ToInt32(ddlCategoryToA.SelectedValue);
                else categoryID = 0;

                if (!string.IsNullOrEmpty(txtJOBNoToA.Text))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(hdLOTTFID.Value)))
                        lotTFID = Convert.ToInt32(hdLOTTFID.Value);
                    jobNo = txtJOBNoToA.Text.Trim().ToUpper();
                }

                //if (chkNA.Checked == false)
                //{
                if (!string.IsNullOrEmpty(txtDrawingNoToA.Text))
                    drawingNo = txtDrawingNoToA.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(txtEquipmentToA.Text))
                {
                    equipmentNo = txtEquipmentToA.Text.Trim().ToUpper();
                    tagNo = txtTagNoToA.Text.Trim().ToUpper();
                    lotTFSubitemID = Convert.ToInt32(hdLOTTFSubitemID.Value);
                }
                //}

                quantity = Convert.ToInt32(txtQuantityToA.Text);

                if (!string.IsNullOrEmpty(txtItemNameToA.Text))
                    itemName = txtItemNameToA.Text.Trim();

                if (!string.IsNullOrEmpty(txtItemDetailToA.Text))
                    itemDesc = txtItemDetailToA.Text.Trim();

                expectedDateOfCompByPlanning = Convert.ToDateTime(txtDateToA.Text).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(txtRemarkstoA.Text))
                    remarks = txtRemarkstoA.Text.Trim();


                if (uploadFileAttachment1.HasFile)
                {
                    if (!string.IsNullOrEmpty(uploadFileAttachment1.PostedFile.FileName))
                    {
                        additionalDrawingFileName = uploadFileAttachment1.PostedFile.FileName;
                        additionalDrawingBytes = GetFileBytes(uploadFileAttachment1.PostedFile.FileName, uploadFileAttachment1.PostedFile.InputStream);
                    }
                    else
                    {
                        additionalDrawingFileName = string.Empty;
                        additionalDrawingBytes = null;
                    }
                }
                else
                {
                    additionalDrawingFileName = string.Empty;
                    additionalDrawingBytes = null;
                }

                DataRow drn = dtSubitems.NewRow();
                drn["ASSIGNED_DRAWING_CODE"] = string.Empty;
                drn["LOT_TF_ID"] = lotTFID;
                drn["LOT_TF_SUBITEM_ID"] = lotTFSubitemID;
                drn["UNIT_ID"] = unitID;
                drn["JOB_NO"] = jobNo;
                drn["DRAWING_NO"] = drawingNo;
                drn["EQUIPMENT"] = equipmentNo;
                drn["TAG_NO"] = tagNo;
                drn["ITEM_NAME"] = itemName;
                drn["ITEM_DETAIL"] = itemDesc;
                drn["EXPECTED_DATE_OF_COMP_BY_PLANNING"] = expectedDateOfCompByPlanning;
                drn["ALLOCATED_QUANTITY"] = quantity;
                drn["REMARKS"] = remarks;
                drn["ADD_DRAWING_FILE_NAME"] = additionalDrawingFileName;
                drn["ADD_DRAWING_FILE_BYTES"] = additionalDrawingBytes;
                drn["CATEGORY_ID"] = categoryID;

                dtSubitems.Rows.Add(drn);
            }
            else
            {
                DataTable dt = new DataTable();
                if (Session["dtSubitemDetails"] != null)
                    dt = (DataTable)Session["dtSubitemDetails"];

                int srNo = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        srNo++;

                        DataRow drn = dtSubitems.NewRow();
                        drn["ASSIGNED_DRAWING_CODE"] = string.Empty;
                        drn["LOT_TF_ID"] = Convert.ToInt32(dr["LOT_TF_ID"]);
                        drn["LOT_TF_SUBITEM_ID"] = Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]);
                        drn["UNIT_ID"] = Convert.ToInt32(dr["UNIT_ID"]);
                        drn["JOB_NO"] = Convert.ToString(dr["JOB_NO"]);
                        drn["DRAWING_NO"] = Convert.ToString(dr["DRAWING_NO"]);
                        drn["EQUIPMENT"] = Convert.ToString(dr["EQUIPMENT"]);
                        drn["TAG_NO"] = Convert.ToString(dr["TAG_NO"]);
                        drn["ITEM_NAME"] = Convert.ToString(dr["ITEM_NAME"]);
                        drn["ITEM_DETAIL"] = Convert.ToString(dr["ITEM_DETAIL"]);
                        drn["EXPECTED_DATE_OF_COMP_BY_PLANNING"] = Convert.ToDateTime(dr["EXPECTED_DATE_OF_COMP_BY_PLANNING"]).ToString("yyyy-MM-dd");
                        drn["ALLOCATED_QUANTITY"] = Convert.ToInt32(dr["ALLOCATED_QUANTITY"]);
                        drn["REMARKS"] = Convert.ToString(dr["REMARKS"]);
                        drn["ADD_DRAWING_FILE_NAME"] = Convert.ToString(dr["ADD_DRAWING_FILE_NAME"]);
                        drn["ADD_DRAWING_FILE_BYTES"] = dr["ADD_DRAWING_FILE_BYTES"];
                        drn["CATEGORY_ID"] = dr["CATEGORY_ID"];
                        dtSubitems.Rows.Add(drn);
                    }
                }
                else
                {
                    ExceptionMessage("No data found...!!!");
                    return;
                }
            }


            if (dtSubitems.Rows.Count > 0)
            {
                string y = string.Empty;
                string m = string.Empty;
                int r = 1;
                string runningNumber = string.Empty;
                string assignedDrawingCode = string.Empty;

                y = DateTime.Now.Year.ToString().Substring(2, 2);
                m = DateTime.Now.Month.ToString();

                if (m.Length < 2)
                    m = "0" + m;

                int count = 0;
                foreach (DataRow drn in dtSubitems.Rows)
                {
                    count++;
                    r = assignedDrawingCounts + count;

                    if (r < 10)
                        runningNumber = "000" + Convert.ToString(r);
                    else if (r < 100)
                        runningNumber = "00" + Convert.ToString(r);
                    else if (r < 100)
                        runningNumber = "0" + Convert.ToString(r);
                    else
                        runningNumber = Convert.ToString(r);

                    assignedDrawingCode = "ASD" + y + m + runningNumber;

                    drn["ASSIGNED_DRAWING_CODE"] = assignedDrawingCode;
                }


                int value = 0;
                value = objMS.InsertAssignedItemsForMachineSch(0, dtSubitems, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (value > 0)
                {
                    int sendMailValue = SendMail(typeID);
                    if (sendMailValue > 0)
                    {
                        SuccessMessage(dtSubitems.Rows.Count + " Records assigned and mail sent successfully...!!!");
                    }
                    else
                    {
                        SuccessMessage(dtSubitems.Rows.Count + " Records assigned successfully...!!!");
                    }

                    Reset();
                    Session["dtSubitemDetails"] = null;
                    gvSubitemsList.DataSource = null;
                    gvSubitemsList.DataBind();
                    lblRecords.Text = "Records [0]";
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again...!!!");
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            Session["dtSubitemDetails"] = null;
            gvSubitemsList.DataSource = null;
            gvSubitemsList.DataBind();
            lblRecords.Text = "Records [0]";
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int SendMail(int typeID)
    {
        try
        {
            int sendMailValue = 0;
            int lotTFID = 0;
            int lotTFSubitemID = 0;
            int unitID = 0;
            string unitName = string.Empty;
            string jobNo = string.Empty;
            string drawingNo = string.Empty;
            string equipmentNo = string.Empty;
            string tagNo = string.Empty;
            string itemName = string.Empty;
            string itemDesc = string.Empty;
            string expectedDateOfCompByPlanning = string.Empty;
            int quantity = 0;
            string remarks = string.Empty;

            DataSet dsMailInfo = new DataSet();
            DataTable dtDistinctJobNo = new DataTable();
            DataTable dtMailInfo = new DataTable();
            DataTable dtShopIncInfo = new DataTable();
            DataTable dtUpdateMailStatus = new DataTable();

            dtDistinctJobNo.Columns.Add("JOB_NO", typeof(string));

            dtMailInfo.Columns.Add("UNIT_NAME", typeof(string));
            dtMailInfo.Columns.Add("JOB_NO", typeof(string));
            dtMailInfo.Columns.Add("DRAWING_NO", typeof(string));
            dtMailInfo.Columns.Add("EQUIPMENT", typeof(string));
            dtMailInfo.Columns.Add("TAG_NO", typeof(string));
            dtMailInfo.Columns.Add("ITEM_NAME", typeof(string));
            dtMailInfo.Columns.Add("ITEM_DETAIL", typeof(string));
            dtMailInfo.Columns.Add("EXPECTED_DATE_OF_COMP_BY_PLANNING", typeof(string));
            dtMailInfo.Columns.Add("ALLOCATED_QUANTITY", typeof(string));
            dtMailInfo.Columns.Add("REMARKS", typeof(string));

            dtUpdateMailStatus.Columns.Add("RECORD_ID", typeof(int));
            dtUpdateMailStatus.Columns.Add("SR_NO", typeof(int));
            dtUpdateMailStatus.Columns.Add("UNIT_ID", typeof(int));
            dtUpdateMailStatus.Columns.Add("JOB_NO", typeof(string));
            dtUpdateMailStatus.Columns.Add("DRAWING_NO", typeof(string));


            if (typeID == 0)
            {
                unitID = Convert.ToInt32(ddlCompanyToA.SelectedValue);
                unitName = Convert.ToString(ddlCompanyToA.SelectedItem.Text);

                if (!string.IsNullOrEmpty(txtJOBNoToA.Text))
                {
                    lotTFID = Convert.ToInt32(hdLOTTFID.Value);
                    jobNo = txtJOBNoToA.Text.Trim().ToUpper();
                }

                //if (chkNA.Checked == false)
                //{
                if (!string.IsNullOrEmpty(txtDrawingNoToA.Text))
                    drawingNo = txtDrawingNoToA.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(txtEquipmentToA.Text))
                {
                    equipmentNo = txtEquipmentToA.Text.Trim().ToUpper();
                    tagNo = txtTagNoToA.Text.Trim().ToUpper();
                    lotTFSubitemID = Convert.ToInt32(hdLOTTFSubitemID.Value);
                }
                //}

                quantity = Convert.ToInt32(txtQuantityToA.Text);

                if (!string.IsNullOrEmpty(txtItemNameToA.Text))
                    itemName = txtItemNameToA.Text.Trim();

                if (!string.IsNullOrEmpty(txtItemDetailToA.Text))
                    itemDesc = txtItemDetailToA.Text.Trim();

                expectedDateOfCompByPlanning = txtDateToA.Text;

                if (!string.IsNullOrEmpty(txtRemarkstoA.Text))
                    remarks = txtRemarkstoA.Text.Trim();

                DataRow drnd = dtDistinctJobNo.NewRow();
                drnd["JOB_NO"] = jobNo;
                dtDistinctJobNo.Rows.Add(drnd);

                DataRow drn = dtMailInfo.NewRow();
                drn["UNIT_NAME"] = unitName;
                drn["JOB_NO"] = jobNo;
                drn["DRAWING_NO"] = drawingNo;
                drn["EQUIPMENT"] = equipmentNo;
                drn["TAG_NO"] = tagNo;
                drn["ITEM_NAME"] = itemName;
                drn["ITEM_DETAIL"] = itemDesc;
                drn["EXPECTED_DATE_OF_COMP_BY_PLANNING"] = expectedDateOfCompByPlanning;
                drn["ALLOCATED_QUANTITY"] = quantity;
                drn["REMARKS"] = remarks;
                dtMailInfo.Rows.Add(drn);


                DataRow drns = dtUpdateMailStatus.NewRow();
                drns["RECORD_ID"] = 0;
                drns["SR_NO"] = 1;
                drns["UNIT_ID"] = unitID;
                drns["JOB_NO"] = jobNo;
                drns["DRAWING_NO"] = drawingNo;
                dtUpdateMailStatus.Rows.Add(drns);
            }
            else
            {
                int count = 0;
                int distinctJobCount = 0;
                foreach (GridViewRow gr in gvSubitemsList.Rows)
                {
                    count++;
                    Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                    TextBox txtUnit = (TextBox)gr.FindControl("txtUnit");
                    TextBox txtJOBNo = (TextBox)gr.FindControl("txtJOBNo");
                    TextBox txtDrawingNo = (TextBox)gr.FindControl("txtDrawingNo");
                    TextBox txtEquipment = (TextBox)gr.FindControl("txtEquipment");
                    TextBox txtTagNo = (TextBox)gr.FindControl("txtTagNo");
                    TextBox txtItemName = (TextBox)gr.FindControl("txtItemName");
                    TextBox txtItemDetail = (TextBox)gr.FindControl("txtItemDetail");
                    TextBox txtExpectedDateofComp = (TextBox)gr.FindControl("txtExpectedDateofComp");
                    TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                    TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");

                    distinctJobCount = 0;
                    if (dtDistinctJobNo.Rows.Count == 0)
                    {
                        distinctJobCount = 0;
                    }
                    else
                    {
                        foreach (DataRow dr in dtDistinctJobNo.Select("JOB_NO='" + txtJOBNo.Text + "'"))
                        {
                            distinctJobCount = 1;
                            break;
                        }
                    }

                    if (distinctJobCount == 0)
                    {
                        DataRow drnd = dtDistinctJobNo.NewRow();
                        drnd["JOB_NO"] = Convert.ToString(txtJOBNo.Text);
                        dtDistinctJobNo.Rows.Add(drnd);
                    }

                    DataRow drn = dtMailInfo.NewRow();
                    drn["UNIT_NAME"] = Convert.ToString(txtUnit.Text);
                    drn["JOB_NO"] = Convert.ToString(txtJOBNo.Text);
                    drn["DRAWING_NO"] = Convert.ToString(txtDrawingNo.Text);
                    drn["EQUIPMENT"] = Convert.ToString(txtEquipment.Text);
                    drn["TAG_NO"] = Convert.ToString(txtTagNo.Text);
                    drn["ITEM_NAME"] = Convert.ToString(txtItemName.Text);
                    drn["ITEM_DETAIL"] = Convert.ToString(txtItemDetail.Text);
                    drn["EXPECTED_DATE_OF_COMP_BY_PLANNING"] = Convert.ToString(txtExpectedDateofComp.Text);
                    drn["ALLOCATED_QUANTITY"] = Convert.ToString(txtQuantity.Text);
                    drn["REMARKS"] = Convert.ToString(txtRemarks.Text);

                    dtMailInfo.Rows.Add(drn);

                    DataRow drns = dtUpdateMailStatus.NewRow();
                    drns["RECORD_ID"] = 0;
                    drns["SR_NO"] = count;
                    drns["UNIT_ID"] = Convert.ToInt32(lblUnitID.Text);
                    drns["JOB_NO"] = Convert.ToString(txtJOBNo.Text);
                    drns["DRAWING_NO"] = Convert.ToString(txtDrawingNo.Text); ;
                    dtUpdateMailStatus.Rows.Add(drns);
                }
            }


            dtShopIncInfo = objMS.GetMachineShopInchargeList();

            dsMailInfo.Tables.Add(dtMailInfo);
            dsMailInfo.Tables.Add(dtDistinctJobNo);


            sendMailValue = objMSSendMail.ProcessAndSendAssignedMail(dsMailInfo, dtShopIncInfo,
                                                             Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.AssignedMail),
                                                             Convert.ToString(Session["EMPLOYEE_NAME"]), Convert.ToString(Session["EMAIL_ID"]));

            if (sendMailValue > 0)
            {
                //int updateSatatusVal = objMS.UpdateMailStatus(dtUpdateMailStatus, Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.AssignedMail),
                //                                              Convert.ToInt32(Session["EMP_RECORD_ID"]));                
            }

            return sendMailValue;
        }
        catch (Exception ex)
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


    private void RemoveRecords(DataTable dtToRemoveRecord, string serialNos)
    {
        string[] strSerialNo = serialNos.Split(',');
        if (dtToRemoveRecord.Rows.Count > 0)
        {
            foreach (string sr in strSerialNo)
            {
                if (!string.IsNullOrEmpty(sr))
                {
                    foreach (DataRow drremove in dtToRemoveRecord.Select("SR_NO='" + sr + "'"))
                    {
                        dtToRemoveRecord.Rows.Remove(drremove);
                    }
                }
            }

            if (dtToRemoveRecord.Rows.Count > 0)
            {
                int count = 1;
                foreach (DataRow drs in dtToRemoveRecord.Rows)
                {
                    drs["SR_NO"] = count++;
                }


                Session["dtSubitemDetails"] = dtToRemoveRecord;
                gvSubitemsList.DataSource = dtToRemoveRecord;
                gvSubitemsList.DataBind();
            }
            else
            {
                Session["dtSubitemDetails"] = null;
                gvSubitemsList.DataSource = null;
                gvSubitemsList.DataBind();
            }
        }
        else
        {
            Session["dtSubitemDetails"] = null;
        }
        lblRecords.Text = "Records[" + gvSubitemsList.Rows.Count + "]";
    }


    private void ViewDrawingFiles(int srNo, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataTable dtDetails = new DataTable();

            if (Session["dtSubitemDetails"] != null)
            {
                dtDetails = (DataTable)Session["dtSubitemDetails"];
            }

            if (dtDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDetails.Select("SR_NO=" + srNo))
                {
                    if (fileType == "ADD_DRAWING")
                    {
                        bytes = (byte[])dr["ADD_DRAWING_FILE_BYTES"];
                        fileName = Convert.ToString(dr["ADD_DRAWING_FILE_NAME"]);
                    }
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
                ExceptionMessage("The process of downloading is too longer, please try again...!!!");
                return;
            }
            else
            {
                throw;
            }
        }
    }


    private void ViewSIDrawingFiles(int lotTFSubitemID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsDetails = new DataSet();

            dsDetails = objMS.GetSIDrawingFiles(lotTFSubitemID);
            if (dsDetails.Tables.Count > 0 && dsDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDetails.Tables[0].Rows)
                {
                    if (fileType == "SI_DRAWING1")
                    {
                        bytes = (byte[])dr["SI_ATTACHMENT1_DOC"];
                        fileName = Convert.ToString(dr["SI_ATTACHMENT1_NAME"]);
                    }

                    if (fileType == "SI_DRAWING2")
                    {
                        bytes = (byte[])dr["SI_ATTACHMENT2_DOC"];
                        fileName = Convert.ToString(dr["SI_ATTACHMENT2_NAME"]);
                    }
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
                ExceptionMessage("The process of downloading is too longer, please try again...!!!");
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
        //chkNA.Checked = false;
        lblSRNoToA.Text = "0";
        txtJOBNoToA.Text = string.Empty;
        txtDrawingNoToA.Text = string.Empty;
        txtEquipmentToA.Text = string.Empty;
        txtTagNoToA.Text = string.Empty;
        txtItemNameToA.Text = string.Empty;
        txtItemDetailToA.Text = string.Empty;
        hdDateToA.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtDateToA.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtQuantityToA.Text = "0";
        txtRemarkstoA.Text = string.Empty;
        hdLOTTFID.Value = string.Empty;
        hdLOTTFSubitemID.Value = "0";
        imgBtnUndoAddDrawing.Visible = false;
        lblSIDrawing1.Text = "";
        lblSIDrawing2.Text = "";
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


    private void HideMessagePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}