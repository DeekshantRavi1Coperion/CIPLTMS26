using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MACHINE_SCH_ScheduledMachinesList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.MachineScheduling objMS = new BAL.MachineScheduling();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsScheduledMachineList = new DataSet();
    DataSet dsJobNo = new DataSet();
    DataSet dsDrawing = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsMachine = new DataSet();
    DataSet dsType = new DataSet();
    DataSet dsActivity = new DataSet();
    DataSet dsProdManager = new DataSet();
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

                Session["dtActivity"] = null;
                Session["dtType"] = null;

                HideMessagePanel();
                //hdDateToA.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtDateToA.Text = hdDateToA.Value;
                BindUnit();

                GetActivityData();
                GetTypeData();
                GetAssignedItemsList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlActivityToMs_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (gvSubitemsList.Rows.Count > 0)
        //{
        //GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        //DropDownList ddlMachine = (DropDownList)gvr.FindControl("ddlMachine");
        //DropDownList ddlActivity = (DropDownList)gvr.FindControl("ddlActivity");

        mpeUpdateDetails.Show();
        if (ddlActivityToMs.SelectedIndex > 0)
        {
            dsMachine = objMS.GetMachineActivity(Convert.ToInt32(ddlActivityToMs.SelectedValue));
            if (dsMachine.Tables.Count > 0 && dsMachine.Tables[0].Rows.Count > 0)
            {
                ddlMachineToMs.DataSource = dsMachine.Tables[0];
                ddlMachineToMs.DataTextField = "MACHINE_NAME";
                ddlMachineToMs.DataValueField = "MACHINE_FID";
                ddlMachineToMs.DataBind();
                ddlMachineToMs.Items.Insert(0, "Select");
                ddlMachineToMs.SelectedIndex = 0;
            }
            else
            {
                ddlMachineToMs.Items.Clear();
                ddlMachineToMs.Items.Insert(0, "Select");
                ddlMachineToMs.SelectedIndex = 0;
            }
        }
        else
        {
            ddlMachineToMs.Items.Clear();
            ddlMachineToMs.Items.Insert(0, "Select");
            ddlMachineToMs.SelectedIndex = 0;
        }

        //}
    }

    protected void ddlActivityUCQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        mpeUpdateCompletedQuantityDetails.Show();
        if (ddlActivityUCQ.SelectedIndex > 0)
        {
            dsMachine = objMS.GetAssignedMachineList(Convert.ToInt32(ViewState["RecordID"]), Convert.ToInt32(ddlActivityUCQ.SelectedValue));
            if (dsMachine.Tables.Count > 0 && dsMachine.Tables[0].Rows.Count > 0)
            {
                ddlMachineUCQ.DataSource = dsMachine.Tables[0];
                ddlMachineUCQ.DataTextField = "MACHINE_NAME";
                ddlMachineUCQ.DataValueField = "MACHINE_FID";
                ddlMachineUCQ.DataBind();
                ddlMachineUCQ.Items.Insert(0, "Select");
                ddlMachineUCQ.SelectedIndex = 0;
            }
            else
            {
                ddlMachineUCQ.Items.Clear();
                ddlMachineUCQ.Items.Insert(0, "Select");
                ddlMachineUCQ.SelectedIndex = 0;
            }
        }
        else
        {
            ddlMachineUCQ.Items.Clear();
            ddlMachineUCQ.Items.Insert(0, "Select");
            ddlMachineUCQ.SelectedIndex = 0;
        }

        //}
    }

    protected void gvSubitemsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    for (int i = 0; i < e.Row.Cells.Count; i++)
        //    {
        //        e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        //    }
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }

            Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");            
            Label lblAdditinoalDrawingFilePath = (Label)e.Row.FindControl("lblAdditinoalDrawingFilePath");
            ImageButton imgBtnViewAdditionalDrawing = (ImageButton)e.Row.FindControl("imgBtnViewAdditionalDrawing");
            
            imgBtnViewAdditionalDrawing.Visible = false;

            if (!string.IsNullOrEmpty(lblAdditinoalDrawingFilePath.Text))
            {
                imgBtnViewAdditionalDrawing.Visible = true;
                imgBtnViewAdditionalDrawing.ToolTip = lblAdditinoalDrawingFilePath.Text;
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
                if (Convert.ToString(e.CommandArgument) == "ViewADDDRAWING"||
                    Convert.ToString(e.CommandArgument) == "EDIT")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
               
                TextBox txtSrNo = gvSubitemsList.Rows[rowindex].FindControl("txtSrNo") as TextBox;
                Label lblRecordID = gvSubitemsList.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblAssignedRecordID = gvSubitemsList.Rows[rowindex].FindControl("lblAssignedRecordID") as Label;                
                Label lblUnitID = gvSubitemsList.Rows[rowindex].FindControl("lblUnitID") as Label;

                TextBox txtUnit = gvSubitemsList.Rows[rowindex].FindControl("txtUnit") as TextBox;
                TextBox txtJOBNo = gvSubitemsList.Rows[rowindex].FindControl("txtJOBNo") as TextBox;
                TextBox txtDrawingNo = gvSubitemsList.Rows[rowindex].FindControl("txtDrawingNo") as TextBox;
                TextBox txtEquipment = gvSubitemsList.Rows[rowindex].FindControl("txtEquipment") as TextBox;
                TextBox txtItemDesc = gvSubitemsList.Rows[rowindex].FindControl("txtItemDesc") as TextBox;
                TextBox txtExpectedDateofComp = gvSubitemsList.Rows[rowindex].FindControl("txtExpectedDateofComp") as TextBox;
                Label lblProductionManagerID = gvSubitemsList.Rows[rowindex].FindControl("lblProductionManagerID") as Label;
                TextBox txtAllocatedQuantity = gvSubitemsList.Rows[rowindex].FindControl("txtAllocatedQuantity") as TextBox;
                TextBox txtCompletedQuantity = gvSubitemsList.Rows[rowindex].FindControl("txtCompletedQuantity") as TextBox;
                TextBox txtPendingQuantity = gvSubitemsList.Rows[rowindex].FindControl("txtPendingQuantity") as TextBox;
                TextBox txtRemarks = gvSubitemsList.Rows[rowindex].FindControl("txtRemarks") as TextBox;
                Label lblAdditinoalDrawingFilePath = gvSubitemsList.Rows[rowindex].FindControl("lblAdditinoalDrawingFilePath") as Label;



                if (Convert.ToString(e.CommandArgument) == "SCHEDULE")
                {
                    ViewState["rowindex"] = rowindex;
                    ViewState["RecordID"] = Convert.ToInt32(lblRecordID.Text);
                    txtScheduledFromToMs.Text = string.Empty;
                    txtScheduledToToMs.Text = string.Empty;

                    BindActivity();
                    BindTypeOfWork();
                    BindProductionManager(Convert.ToInt32(lblUnitID.Text));
                    txtUnitToMs.Text = txtUnit.Text;
                    txtJOBNoToMs.Text = Convert.ToString(txtJOBNo.Text);
                    txtDrawingNoToMs.Text = Convert.ToString(txtDrawingNo.Text);
                    txtEquipmentToMs.Text = Convert.ToString(txtEquipment.Text);
                    txtItemDescriptionToMs.Text = Convert.ToString(txtItemDesc.Text);
                    txtExpectedDateofCompToMs.Text = Convert.ToString(txtExpectedDateofComp.Text);

                    


                    if (Convert.ToInt32(lblProductionManagerID.Text) > 0)
                    {
                        ddlProductionManagerToMs.SelectedValue = Convert.ToString(lblProductionManagerID.Text);
                    }
                    else
                    {
                        ddlProductionManagerToMs.SelectedIndex = 0;
                    }

                    if (ddlProductionManagerToMs.SelectedIndex > 0)
                    {
                        ddlProductionManagerToMs.Enabled = false;
                    }
                    else
                    {
                        ddlProductionManagerToMs.Enabled = true;
                    }

                    txtAllocatedQuantityToMs.Text = Convert.ToString(txtAllocatedQuantity.Text);

                    ddlMachineToMs.Items.Clear();
                    ddlMachineToMs.Items.Insert(0, "Select");
                    ddlMachineToMs.SelectedIndex = 0;



                    hdDateOfReceiptOfMaterialToMs.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    hdCommitedDateByShopInchargeToMs.Value = DateTime.Now.ToString("dd-MMM-yyyy");

                    hdCompletionDateToMs.Value = DateTime.Now.ToString("dd-MMM-yyyy");

                    txtDateOfReceiptOfMaterialToMs.Text = hdDateOfReceiptOfMaterialToMs.Value;
                    txtCommitedDateByShopInchargeToMs.Text = hdCommitedDateByShopInchargeToMs.Value;

                    txtCompletionDateToMs.Text = hdCompletionDateToMs.Value;

                    //txtPendingQuantityToMs.Text = Convert.ToString(Convert.ToInt32(txtAllocatedQuantityToMs.Text) - Convert.ToInt32(txtCompletedQuantityToMs.Text));

                    txtCompletedQuantityToMs.Text = txtCompletedQuantity.Text;
                    txtPendingQuantityToMs.Text = txtPendingQuantity.Text;

                    btnSaveCompletedQuantity.Visible = false;
                    if (Convert.ToInt32(lblAssignedRecordID.Text) > 0)
                    {
                        btnSaveCompletedQuantity.Visible = true;
                    }

                    mpeUpdateDetails.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewADDDRAWING")
                {
                    ViewDrawingFiles(Convert.ToInt32(txtSrNo.Text), "ADD_DRAWING");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    

    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        HideMessagePanel();
        GetAssignedItemsList();
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        SaveItemsDetails(0);
    }

    protected void btnSaveAll_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        SaveItemsDetails(1);
    }

    protected void btnSaveCompletedQuantity_Click(object sender, EventArgs e)
    {
        ddlMachineUCQ.Items.Clear();
        ddlMachineUCQ.Items.Insert(0, "Select");
        ddlMachineUCQ.SelectedIndex = 0;

        txtUnitUCQ.Text = txtUnitToMs.Text;
        txtJOBNoUCQ.Text = txtJOBNoToMs.Text;
        txtDrawingNoUCQ.Text = txtDrawingNoToMs.Text;
        txtEquipmentUCQ.Text = txtEquipmentToMs.Text;

        //txtActivityUCQ.Text = ddlActivityToMs.SelectedItem.Text;
        //txtMachineUCQ.Text = ddlMachineToMs.SelectedItem.Text;
        //txtTypeOfWorkUCQ.Text = ddlTypeOfWorkToMs.SelectedItem.Text;

        BindActivityUCQ();
        //BindTypeOfWorkUCQ();

        txtAllocatedQuantityUCQ.Text = txtAllocatedQuantityToMs.Text;
        txtTotalCompletedQuantityUCQ.Text = txtCompletedQuantityToMs.Text;
        txtPendingQuantityUCQ.Text = txtPendingQuantityToMs.Text;

        mpeUpdateDetails.Show();
        mpeUpdateCompletedQuantityDetails.Show();
    }

    protected void btnUpdateCompletedQuantity_Click(object sender, EventArgs e)
    {
        SaveCompletedQuantity();
        mpeUpdateDetails.Show();
    }

    protected void btnAddDetailsToList_Click(object sender, EventArgs e)
    {
        HideMessagePanel();



        AddDetailsToList();

    }

    protected void imgbtnScheduledToToMs_Click(object sender, ImageClickEventArgs e)
    {
        txtScheduledFromToMs.Text = string.Empty;
        txtScheduledToToMs.Text = string.Empty;
        txtActivitySchD.Text = ddlActivityToMs.SelectedItem.Text;
        txtMachineSchD.Text = ddlMachineToMs.SelectedItem.Text;
        hdScheduledFromSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        hdScheduledToSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtScheduledFromSchD.Text = hdScheduledFromSchD.Value;
        txtScheduledToSchD.Text = hdScheduledToSchD.Value;

        mpeSheduleDates.Show();
        mpeUpdateDetails.Show();
    }

    protected void btnScheduleDates_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        txtScheduledFromToMs.Text = txtScheduledFromSchD.Text;
        txtScheduledToToMs.Text = txtScheduledToSchD.Text;
        mpeUpdateDetails.Show();
    }


    protected void gvMachineDatesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //
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
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;


                //ddlCompanyToA.DataSource = dsUnit.Tables[0];
                //ddlCompanyToA.DataTextField = "UNIT_NAME";
                //ddlCompanyToA.DataValueField = "UNIT_ID";
                //ddlCompanyToA.DataBind();
                //ddlCompanyToA.SelectedIndex = 0;
            }
            else
            {
                ddlCompany.Items.Clear();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;

                //ddlCompanyToA.Items.Clear();
                //ddlCompanyToA.Items.Insert(0, "Select");
                //ddlCompanyToA.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataTable GetActivityData()
    {
        try
        {
            dsActivity = objMS.GetActivities("");
            if (dsActivity.Tables.Count > 0 && dsActivity.Tables[0].Rows.Count > 0)
            {
                Session["dtActivity"] = dsActivity.Tables[0];
                return dsActivity.Tables[0];
            }
            else
            {
                Session["dtActivity"] = null;
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void BindActivity()
    {
        try
        {
            DataTable dtActivity = new DataTable();

            if (Session["dtActivity"] != null)
                dtActivity = (DataTable)Session["dtActivity"];
            else
                dtActivity = GetActivityData();

            if (dtActivity.Rows.Count > 0)
            {
                ddlActivityToMs.DataSource = dtActivity;
                ddlActivityToMs.DataTextField = "ACTIVITY_NAME";
                ddlActivityToMs.DataValueField = "ACTIVITY_PID";
                ddlActivityToMs.DataBind();
                ddlActivityToMs.Items.Insert(0, "Select");
                ddlActivityToMs.SelectedIndex = 0;
            }
            else
            {
                ddlActivityToMs.Items.Clear();
                ddlActivityToMs.Items.Insert(0, "Select");
                ddlActivityToMs.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void BindActivityUCQ()
    {
        try
        {
            int recordID = 0;
            recordID = Convert.ToInt32(ViewState["RecordID"]);
            dsActivity = objMS.GetAssignedActivityList(recordID);
            if (dsActivity.Tables.Count > 0 && dsActivity.Tables[0].Rows.Count > 0)
            {
                ddlActivityUCQ.DataSource = dsActivity.Tables[0];
                ddlActivityUCQ.DataTextField = "ACTIVITY_NAME";
                ddlActivityUCQ.DataValueField = "ACTIVITY_FID";
                ddlActivityUCQ.DataBind();
                ddlActivityUCQ.Items.Insert(0, "Select");
                ddlActivityUCQ.SelectedIndex = 0;
            }
            else
            {
                ddlActivityUCQ.Items.Clear();
                ddlActivityUCQ.Items.Insert(0, "Select");
                ddlActivityUCQ.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    private void BindProductionManager(int unitID)
    {
        try
        {

            dsProdManager = objMS.GetProductionManagers(unitID);
            if (dsProdManager.Tables.Count > 0 && dsProdManager.Tables[0].Rows.Count > 0)
            {
                ddlProductionManagerToMs.DataSource = dsProdManager.Tables[0];
                ddlProductionManagerToMs.DataTextField = "MANAGER_NAME";
                ddlProductionManagerToMs.DataValueField = "MANAGER_ID";
                ddlProductionManagerToMs.DataBind();
                ddlProductionManagerToMs.Items.Insert(0, "Select");
                ddlProductionManagerToMs.SelectedIndex = 0;
            }
            else
            {
                ddlProductionManagerToMs.Items.Clear();
                ddlProductionManagerToMs.Items.Insert(0, "Select");
                ddlProductionManagerToMs.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    private DataTable GetTypeData()
    {
        try
        {
            dsType = objMS.GetScheduleType();
            if (dsType.Tables.Count > 0 && dsType.Tables[0].Rows.Count > 0)
            {
                Session["dtType"] = dsType.Tables[0];
                return dsType.Tables[0];
            }
            else
            {
                Session["dtType"] = null;
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void BindTypeOfWork()
    {
        try
        {
            DataTable dtType = new DataTable();

            if (Session["dtType"] != null)
                dtType = (DataTable)Session["dtType"];
            else
                dtType = GetTypeData();

            if (dtType.Rows.Count > 0)
            {
                ddlTypeOfWorkToMs.DataSource = dtType;
                ddlTypeOfWorkToMs.DataTextField = "TYPE_NAME";
                ddlTypeOfWorkToMs.DataValueField = "TYPE_PID";
                ddlTypeOfWorkToMs.DataBind();
                ddlTypeOfWorkToMs.Items.Insert(0, "Select");
                ddlTypeOfWorkToMs.SelectedIndex = 0;
            }
            else
            {
                ddlTypeOfWorkToMs.Items.Clear();
                ddlTypeOfWorkToMs.Items.Insert(0, "Select");
                ddlTypeOfWorkToMs.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    //private void BindTypeOfWorkUCQ()
    //{
    //    try
    //    {
    //        DataTable dtType = new DataTable();

    //        if (Session["dtType"] != null)
    //            dtType = (DataTable)Session["dtType"];
    //        else
    //            dtType = GetTypeData();

    //        if (dtType.Rows.Count > 0)
    //        {
    //            ddlTypeOfWorkUCQ.DataSource = dtType;
    //            ddlTypeOfWorkUCQ.DataTextField = "TYPE_NAME";
    //            ddlTypeOfWorkUCQ.DataValueField = "TYPE_PID";
    //            ddlTypeOfWorkUCQ.DataBind();
    //            ddlTypeOfWorkUCQ.Items.Insert(0, "Select");
    //            ddlTypeOfWorkUCQ.SelectedIndex = 0;
    //        }
    //        else
    //        {
    //            ddlTypeOfWorkUCQ.Items.Clear();
    //            ddlTypeOfWorkUCQ.Items.Insert(0, "Select");
    //            ddlTypeOfWorkUCQ.SelectedIndex = 0;
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    private void GetAssignedItemsList()
    {
        try
        {
            companyID = 0;
            LOTNo = string.Empty;
            jobNo = string.Empty;
            drawingNo = string.Empty;
            equipment = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtLOTNo.Text))
                LOTNo = txtLOTNo.Text;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

            if (!string.IsNullOrEmpty(txtDrawingNo.Text))
                drawingNo = txtDrawingNo.Text;

            if (!string.IsNullOrEmpty(txtEquipment.Text))
                equipment = txtEquipment.Text;

            gvSubitemsList.DataSource = null;
            gvSubitemsList.DataBind();

            dsScheduledMachineList = objMS.GetScheduledMachineList(companyID, LOTNo, jobNo, drawingNo, equipment);
            if (dsScheduledMachineList.Tables.Count > 0 && dsScheduledMachineList.Tables[0].Rows.Count > 0)
            {
                Session["dtSubitemDetails"] = dsScheduledMachineList.Tables[0];
                gvSubitemsList.DataSource = dsScheduledMachineList.Tables[0];
                gvSubitemsList.DataBind();
            }
            else
            {
                Session["dtSubitemDetails"] = null;
                ExceptionMessage("No datat found...!!!");
            }

            lblRecords.Text = "Records[" + gvSubitemsList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void SaveCompletedQuantity()
    {
        try
        {
            int recordID = 0;
            int activityID = 0;
            int machineID = 0;
            int quantity = 0;
            string remarks = string.Empty;

            recordID = Convert.ToInt32(ViewState["RecordID"]);

            if (ddlActivityUCQ.SelectedIndex > 0)
                activityID = Convert.ToInt32(ddlActivityUCQ.SelectedValue);

            if (ddlMachineUCQ.SelectedIndex > 0)
                machineID = Convert.ToInt32(ddlMachineUCQ.SelectedValue);

            if (!string.IsNullOrEmpty(txtCompletedQuantityUCQ.Text))
                quantity = Convert.ToInt32(txtCompletedQuantityUCQ.Text);

            if (!string.IsNullOrEmpty(txtRemarksUCQ.Text))
                remarks = txtRemarksUCQ.Text;

            int value = objMS.AddCompletedQuantity(recordID, activityID, machineID, quantity, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                txtCompletedQuantityToMs.Text = Convert.ToString(value);
                txtPendingQuantityToMs.Text = Convert.ToString(Convert.ToInt32(txtAllocatedQuantityToMs.Text) - value);
            }
            mpeUpdateDetails.Show();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddDetailsToList()
    {
        try
        {
            if (gvSubitemsList.Rows.Count > 0)
            {
                GridViewRow gvr = gvSubitemsList.Rows[Convert.ToInt32(ViewState["rowindex"])];

                Label lblActivityID = (Label)gvr.FindControl("lblActivityID");
                Label lblMachineID = (Label)gvr.FindControl("lblMachineID");
                Label lblTypeOfWorkID = (Label)gvr.FindControl("lblTypeOfWorkID");
                Label lblProductionManagerID = (Label)gvr.FindControl("lblProductionManagerID");


                TextBox txtType = (TextBox)gvr.FindControl("txtType");
                TextBox txtActivity = (TextBox)gvr.FindControl("txtActivity");
                TextBox txtMachine = (TextBox)gvr.FindControl("txtMachine");
                TextBox txtScheduledFrom = (TextBox)gvr.FindControl("txtScheduledFrom");
                TextBox txtScheduledTo = (TextBox)gvr.FindControl("txtScheduledTo");
                TextBox txtDateOfReceiptOfMaterial = (TextBox)gvr.FindControl("txtDateOfReceiptOfMaterial");
                TextBox txtCommitedDateByShopIncharge = (TextBox)gvr.FindControl("txtCommitedDateByShopIncharge");
                TextBox txtCompletionDate = (TextBox)gvr.FindControl("txtCompletionDate");
                TextBox txtProductionManager = (TextBox)gvr.FindControl("txtProductionManager");
                TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");

                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");

                lblTypeOfWorkID.Text = Convert.ToString(ddlTypeOfWorkToMs.SelectedValue);
                txtType.Text = ddlTypeOfWorkToMs.SelectedItem.Text;

                lblActivityID.Text = Convert.ToString(ddlActivityToMs.SelectedValue);
                txtActivity.Text = ddlActivityToMs.SelectedItem.Text;

                lblMachineID.Text = Convert.ToString(ddlMachineToMs.SelectedValue);
                txtMachine.Text = ddlMachineToMs.SelectedItem.Text;

                txtScheduledFrom.Text = txtScheduledFromToMs.Text;
                txtScheduledTo.Text = txtScheduledToToMs.Text;

                txtDateOfReceiptOfMaterial.Text = txtDateOfReceiptOfMaterialToMs.Text;
                txtCommitedDateByShopIncharge.Text = txtCommitedDateByShopInchargeToMs.Text;

                txtCompletionDate.Text = txtCompletionDateToMs.Text;

                if (Convert.ToInt32(lblProductionManagerID.Text) == 0)
                {
                    lblProductionManagerID.Text = Convert.ToString(ddlProductionManagerToMs.SelectedValue);
                    txtProductionManager.Text = ddlProductionManagerToMs.SelectedItem.Text;
                    txtProductionManager.BackColor = System.Drawing.Color.LightGreen;
                }

                txtRemarks.Text = txtRemarksToMs.Text;
                chkSelect.Checked = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SaveItemsDetails(int typeID)
    {
        try
        {
            DataTable dtMachineDetails = new DataTable();

            dtMachineDetails.Columns.Add("ASSIGNED_RECORD_FID", typeof(int));
            dtMachineDetails.Columns.Add("DATE_OF_RECEIPT_OF_MATERIAL", typeof(string));
            dtMachineDetails.Columns.Add("ALLOCATED_QUANTITY", typeof(int));
            dtMachineDetails.Columns.Add("COMPLETED_QUANTITY", typeof(int));
            dtMachineDetails.Columns.Add("ACTIVITY_FID", typeof(int));
            dtMachineDetails.Columns.Add("MACHINE_FID", typeof(int));
            dtMachineDetails.Columns.Add("TYPE_OF_WORK_FID", typeof(int));
            dtMachineDetails.Columns.Add("COMM_DATE_BY_MACH_SHOP_INC", typeof(string));
            dtMachineDetails.Columns.Add("SCHEDULE_FROM", typeof(string));
            dtMachineDetails.Columns.Add("SCHEDULE_TO", typeof(string));
            dtMachineDetails.Columns.Add("PRODUCTION_MANAGER_FID", typeof(int));
            dtMachineDetails.Columns.Add("COMPLETION_DATE", typeof(string));
            dtMachineDetails.Columns.Add("REMARKS", typeof(string));


            int assignedRecordID = 0;
            string dateOfReceiptOfMaterial = string.Empty;
            int allocatedQuantity = 0;
            int completedQuantity = 0;
            int activityID = 0;
            int machineID = 0;
            int typeOfWorkID = 0;
            string commDateByMachShopInc = string.Empty;
            string scheduleFrom = string.Empty;
            string scheduleTo = string.Empty;
            int productionManagerID = 0;
            string completionDate = string.Empty;
            string remarks = string.Empty;

            assignedRecordID = Convert.ToInt32(ViewState["RecordID"]);

            if (typeID == 0)
            {
                dateOfReceiptOfMaterial = txtDateOfReceiptOfMaterialToMs.Text;
                allocatedQuantity = Convert.ToInt32(txtAllocatedQuantityToMs.Text);
                completedQuantity = Convert.ToInt32(txtCompletedQuantityToMs.Text);
                activityID = Convert.ToInt32(ddlActivityToMs.SelectedValue);
                machineID = Convert.ToInt32(ddlMachineToMs.SelectedValue);
                typeOfWorkID = Convert.ToInt32(ddlTypeOfWorkToMs.SelectedValue);
                commDateByMachShopInc = txtCommitedDateByShopInchargeToMs.Text;
                scheduleFrom = txtScheduledFromToMs.Text;
                scheduleTo = txtScheduledToToMs.Text;
                productionManagerID = Convert.ToInt32(ddlProductionManagerToMs.SelectedValue);
                completionDate = txtCompletionDateToMs.Text;
                remarks = txtRemarksToMs.Text;

                DataRow drn = dtMachineDetails.NewRow();

                drn["ASSIGNED_RECORD_FID"] = assignedRecordID;
                drn["DATE_OF_RECEIPT_OF_MATERIAL"] = dateOfReceiptOfMaterial;
                drn["ALLOCATED_QUANTITY"] = allocatedQuantity;
                drn["COMPLETED_QUANTITY"] = completedQuantity;
                drn["ACTIVITY_FID"] = activityID;
                drn["MACHINE_FID"] = machineID;
                drn["TYPE_OF_WORK_FID"] = typeOfWorkID;
                drn["COMM_DATE_BY_MACH_SHOP_INC"] = commDateByMachShopInc;
                drn["SCHEDULE_FROM"] = scheduleFrom;
                drn["SCHEDULE_TO"] = scheduleTo;
                drn["PRODUCTION_MANAGER_FID"] = productionManagerID;
                drn["COMPLETION_DATE"] = completionDate;
                drn["REMARKS"] = remarks;

                dtMachineDetails.Rows.Add(drn);
            }
            else
            {
                if (gvSubitemsList.Rows.Count > 0)
                {
                    foreach (GridViewRow gr in gvSubitemsList.Rows)
                    {
                        CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                        if (chkSelect.Checked)
                        {
                            TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;
                            Label lblRecordID = gr.FindControl("lblRecordID") as Label;
                            Label lblUnitID = gr.FindControl("lblUnitID") as Label;
                            Label lblActivityID = gr.FindControl("lblActivityID") as Label;
                            Label lblMachineID = gr.FindControl("lblMachineID") as Label;
                            Label lblTypeOfWorkID = gr.FindControl("lblTypeOfWorkID") as Label;
                            Label lblProductionManagerID = gr.FindControl("lblProductionManagerID") as Label;


                            TextBox txtUnit = gr.FindControl("txtUnit") as TextBox;
                            TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                            TextBox txtDrawingNo = gr.FindControl("txtDrawingNo") as TextBox;
                            TextBox txtEquipment = gr.FindControl("txtEquipment") as TextBox;
                            TextBox txtItemDesc = gr.FindControl("txtItemDesc") as TextBox;
                            TextBox txtDateOfReceiptOfMaterial = gr.FindControl("txtDateOfReceiptOfMaterial") as TextBox;
                            TextBox txtExpectedDateofComp = gr.FindControl("txtExpectedDateofComp") as TextBox;
                            TextBox txtAllocatedQuantity = gr.FindControl("txtAllocatedQuantity") as TextBox;
                            TextBox txtCompletedQuantity = gr.FindControl("txtCompletedQuantity") as TextBox;
                            TextBox txtCommitedDateByShopIncharge = gr.FindControl("txtCommitedDateByShopIncharge") as TextBox;
                            TextBox txtScheduledFrom = gr.FindControl("txtScheduledFrom") as TextBox;
                            TextBox txtScheduledTo = gr.FindControl("txtScheduledTo") as TextBox;
                            TextBox txtCompletionDate = gr.FindControl("txtCompletionDate") as TextBox;
                            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                            DataRow drn = dtMachineDetails.NewRow();

                            drn["ASSIGNED_RECORD_FID"] = Convert.ToInt32(lblRecordID.Text);
                            drn["DATE_OF_RECEIPT_OF_MATERIAL"] = Convert.ToDateTime(txtDateOfReceiptOfMaterial.Text).ToString("yyyy-MM-dd");
                            drn["ALLOCATED_QUANTITY"] = Convert.ToInt32(txtAllocatedQuantity.Text);
                            drn["COMPLETED_QUANTITY"] = Convert.ToInt32(txtCompletedQuantity.Text);
                            drn["ACTIVITY_FID"] = Convert.ToInt32(lblActivityID.Text);
                            drn["MACHINE_FID"] = Convert.ToInt32(lblMachineID.Text);
                            drn["TYPE_OF_WORK_FID"] = Convert.ToInt32(lblTypeOfWorkID.Text);
                            drn["COMM_DATE_BY_MACH_SHOP_INC"] = Convert.ToDateTime(txtCommitedDateByShopIncharge.Text).ToString("yyyy-MM-dd");
                            drn["SCHEDULE_FROM"] = Convert.ToString(txtScheduledFrom.Text);
                            drn["SCHEDULE_TO"] = Convert.ToString(txtScheduledTo.Text); ;
                            drn["PRODUCTION_MANAGER_FID"] = Convert.ToInt32(lblProductionManagerID.Text);
                            drn["COMPLETION_DATE"] = Convert.ToDateTime(txtCompletionDate.Text).ToString("yyyy-MM-dd");
                            drn["REMARKS"] = Convert.ToString(txtRemarks.Text);

                            dtMachineDetails.Rows.Add(drn);
                        }
                    }
                }
                else
                {
                    ExceptionMessage("No data found...!!!");
                    return;
                }
            }


            if (dtMachineDetails.Rows.Count > 0)
            {
                int value = 0;
                value = objMS.InsertMachineSchedulingDetails(0, dtMachineDetails, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    SuccessMessage(dtMachineDetails.Rows.Count + " Records assigned successfully...!!!");
                    GetAssignedItemsList();
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void ViewDrawingFiles(int recordID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();
            dsFiles = objMS.GetDrawingFiles(recordID);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "ADD_DRAWING")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ADD_DRAWING_FILE_BYTES"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ADD_DRAWING_FILE_NAME"]);
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