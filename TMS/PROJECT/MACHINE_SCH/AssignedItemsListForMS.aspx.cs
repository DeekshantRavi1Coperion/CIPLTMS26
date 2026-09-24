using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MACHINE_SCH_AssignedItemsListForMS : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.MachineScheduling objMS = new BAL.MachineScheduling();
    BAL.Common objCommon = new BAL.Common();
    MSSendMail objMSSendMail = new MSSendMail();

    DataSet dsType = new DataSet();
    DataSet dsWorker = new DataSet();
    DataSet dsActivity = new DataSet();
    DataSet dsMachine = new DataSet();
    DataSet dsAssignedItemsList = new DataSet();
    DataSet dsJobNo = new DataSet();
    DataSet dsDrawing = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsStatus = new DataSet();
    DataSet dsCategory = new DataSet();



    int companyID = 0;
    string LOTNo = string.Empty;
    string equipment = string.Empty;
    int statusId = 0;


    int recordID = 0;
    string assignedDrawingCode = string.Empty;
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
    string itemDetail = string.Empty;
    string expectedDateOfCompByPlanning = string.Empty;
    int quantity = 0;
    string remarks = string.Empty;
    string additionalDrawingFileName = string.Empty;
    Byte[] additionalDrawingBytes = null;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HideMessagePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtMachine"] = null;
                Session["dtMachineSchedulingList"] = null;
                Session["dtShopInhargeList"] = null;
                Session["dtQualityTeamList"] = null;

                HideMessagePanel();
                hdDateToA.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDateToA.Text = hdDateToA.Value;

                btnAssign.Visible = false;
                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 89)
                {
                    btnAssign.Visible = true;
                }

                BindUnit();
                BindStatus();
                BindCategory();

                GetTypeData();
                GetActivityData();
                GetMachinesData();
                GetAssignedItemsList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }


    /// <summary>
    /// Button Clicks
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        GetJOBDetail();
        mpeJOBDetail.Show();
        mpeUpdateDetails.Show();
    }

    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        mpeUpdateDetails.Show();
        GetJOBDetail();
        mpeJOBDetail.Show();
    }

    protected void btnGetDrawingNo_Click(object sender, EventArgs e)
    {

        txtJOBNoInDr.Text = txtJOBNoToA.Text;
        txtLOTNoInDr.Text = Convert.ToString(lblLOTNo.Text);
        txtDrawingNoInDr.Text = string.Empty;
        txtEquipmentInDr.Text = string.Empty;
        mpeDrawingDetail.Show();
        GetDrawingDetail();
        mpeUpdateDetails.Show();
    }

    protected void btnSearchDrawingNo_Click(object sender, EventArgs e)
    {

        mpeDrawingDetail.Show();
        GetDrawingDetail();
        mpeUpdateDetails.Show();
    }

    protected void imgBtnViewAddDrawing_Click(object sender, EventArgs e)
    {
        mpeUpdateDetails.Show();
        ViewDrawingFiles(Convert.ToInt32(ViewState["RECORD_ID"]), "ADD_DRAWING");
    }

    protected void imgBtnRemoveAddDrawing_Click(object sender, ImageClickEventArgs e)
    {
        mpeUpdateDetails.Show();
        pnlViewAddDrawing.Visible = false;
        pnlUploadAddDrawing.Visible = true;
    }

    protected void imgBtnUndoAddDrawing_Click(object sender, ImageClickEventArgs e)
    {
        mpeUpdateDetails.Show();
        pnlViewAddDrawing.Visible = true;
        pnlUploadAddDrawing.Visible = false;
    }

    protected void btnGetSchedulingDetails_Click(object sender, EventArgs e)
    {
        HideMessagePanelScheduleDates();

        ddlMachineSchD.SelectedIndex = 0;
        gvAvailableDatesList.DataSource = null;
        chkSelectAllSchD.Checked = false;
        gvAvailableDatesList.DataBind();
        lblAvailableDatesRecords.Text = "Available Dates[" + gvAvailableDatesList.Rows.Count + "]";

        mpeScheduleMachine.Show();
        //if (ddlMachineToMSch.SelectedIndex > 0)
        //{            
        //    mpeScheduleDates.Show();

        //    //lblActivityIDSchD.Text = Convert.ToString(ddlActivityToMSch.SelectedValue);
        //    //txtActivityNameSchD.Text = Convert.ToString(ddlActivityToMSch.SelectedItem.Text);

        //    lblMachineIDSchD.Text = Convert.ToString(ddlMachineToMSch.SelectedValue);
        //    lblLegendMachineNameSchD.Text = "Schedule [" + Convert.ToString(ddlMachineToMSch.SelectedItem.Text) + "] Machine";

        //    hdScheduledFromSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        //    txtScheduledFromSchD.Text = hdScheduledFromSchD.Value;

        //    hdScheduledToSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        //    txtScheduledToSchD.Text = hdScheduledToSchD.Value;

        //    //AddNewRowForScheduling();
        //    //hdScheduledFromDateToSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        //    //txtScheduledFromDateToSchD.Text = hdScheduledFromDateToSchD.Value;

        //    //hdScheduledDateToSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        //    //txtScheduledDateToSchD.Text = hdScheduledDateToSchD.Value;
        //}

        if (ddlActivityToMSch.SelectedIndex > 0)
        {
            mpeScheduleDates.Show();

            lblActivitySchD.Text = Convert.ToString(ddlActivityToMSch.SelectedValue);
            txtActivitySchD.Text = Convert.ToString(ddlActivityToMSch.SelectedItem.Text);

            hdScheduledFromSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtScheduledFromSchD.Text = hdScheduledFromSchD.Value;

            hdScheduledToSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtScheduledToSchD.Text = hdScheduledToSchD.Value;
        }
    }

    protected void btnGetScheduledHours_Click(object sender, EventArgs e)
    {
        HideMessagePanelScheduleDates();
        GetScheduleDates();
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        //{
        HideMessagePanel();
        SaveItemsDetails();
        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        GetAssignedItemsList();
    }

    //protected void btnResize_Click(object sender, EventArgs e)
    //{
    //    dvScroll.Style.Add("height", txtHeight.Text);
    //    //dvScroll.Style.Add("Width", txtHeight.Text);
    //    //dvScroll.Attributes.Add("height", txtHeight.Text);
    //}

    protected void imgBtnViewAddDrawingToSt_Click(object sender, EventArgs e)
    {
        mpeUpdateStatus.Show();
        ViewDrawingFiles(Convert.ToInt32(ViewState["RECORD_ID"]), "ADD_DRAWING");
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        UpdateStatus(Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Accepted));
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        UpdateStatus(Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Rejected));
    }

    protected void btnCheckAvailibilityOfDates_Click(object sender, EventArgs e)
    {
        HideMessagePanelScheduleDates();
        chkSelectAllSchD.Checked = false;
        //ChekAvailibilityOfDates();
        ChekAvailibilityOfDatesNew();
    }

    protected void btnSaveScheduledDetails_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        SaveScheduledDetails();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        HideMessagePanelCompletionScheduling();

        chkSelectAllToCMSch.Checked = false;

        int recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
        string fromDate = string.Empty;
        string toDate = string.Empty;
        int activityID = 0;
        int machineID = 0;
        int typeOfWorkID = 0;
        int workerID = 0;

        if (!string.IsNullOrEmpty(txtScheduledFromToCMSch.Text) && !string.IsNullOrEmpty(hdScheduledFromToCMSch.Value))
            fromDate = Convert.ToDateTime(hdScheduledFromToCMSch.Value).ToString("yyyy-MM-dd");

        if (!string.IsNullOrEmpty(txtScheduledToToCMSch.Text) && !string.IsNullOrEmpty(hdScheduledToToCMSch.Value))
            toDate = Convert.ToDateTime(hdScheduledToToCMSch.Value).ToString("yyyy-MM-dd");

        if (ddlActivityToCMSch.SelectedIndex > 0)
            activityID = Convert.ToInt32(ddlActivityToCMSch.SelectedValue);

        if (ddlMachineToCMSch.SelectedIndex > 0)
            machineID = Convert.ToInt32(ddlMachineToCMSch.SelectedValue);

        if (ddlTypeOfWorkToCMSch.SelectedIndex > 0)
            typeOfWorkID = Convert.ToInt32(ddlTypeOfWorkToCMSch.SelectedValue);

        if (ddlWorkerToCMSch.SelectedIndex > 0)
            workerID = Convert.ToInt32(ddlWorkerToCMSch.SelectedValue);

        BindScheduledListForCompletion(recordID, fromDate, toDate, activityID, machineID, typeOfWorkID, workerID);

        mpeSchedulingCompletion.Show();
    }

    protected void btnCompleteScheduling_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        CompleteScheduledDetails();
    }

    protected void btnModifySchedulingDetails_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        ModifyWorkerDetails();
    }


    protected void btnSaveInspection_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        QualityInspection();
    }


    //protected void btnAcceptInspection_Click(object sender, EventArgs e)
    //{
    //    HideMessagePanel();
    //    UpdateStatus(Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.QA_Accepted));
    //}

    //protected void btnRejectInspection_Click(object sender, EventArgs e)
    //{
    //    HideMessagePanel();
    //    UpdateStatus(Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.QA_Rejected));
    //}



    /// <summary>
    /// SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void ddlCompanyToA_SelectedIndexChanged(object sender, EventArgs e)
    {
        mpeUpdateDetails.Show();
        Reset();
    }

    protected void ddlActivityToMSchInList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (gvSubitemsList.Rows.Count > 0)
            {
                DataSet dsMachine = new DataSet();
                GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
                DropDownList ddlMachineToMSchInList = (DropDownList)gvr.FindControl("ddlMachineToMSchInList");
                DropDownList ddlActivityToMSchInList = (DropDownList)gvr.FindControl("ddlActivityToMSchInList");
                Label lblMachineID = (Label)gvr.FindControl("lblMachineID");

                mpeScheduleMachine.Show();

                DataTable dtMachine = new DataTable();
                DataTable dtMachineNew = new DataTable();
                if (Session["dtMachine"] != null)
                    dtMachine = (DataTable)Session["dtMachine"];
                else
                    dtMachine = GetMachinesData();

                dtMachineNew = dtMachine.Copy();
                dtMachineNew.Rows.Clear();


                ddlMachineToMSchInList.Items.Clear();
                ddlMachineToMSchInList.Items.Insert(0, "Select");
                ddlMachineToMSchInList.SelectedIndex = 0;

                if (ddlActivityToMSchInList.SelectedIndex > 0)
                {
                    foreach (DataRow dr in dtMachine.Select("ACTIVITY_FID=" + Convert.ToInt32(ddlActivityToMSchInList.SelectedValue) + ""))
                    {
                        DataRow drn = dtMachineNew.NewRow();
                        drn["ACTIVITY_FID"] = dr["ACTIVITY_FID"];
                        drn["MACHINE_FID"] = dr["MACHINE_FID"];
                        drn["MACHINE_NAME"] = dr["MACHINE_NAME"];

                        dtMachineNew.Rows.Add(drn);
                    }


                    if (dtMachineNew.Rows.Count > 0)
                    {
                        ddlMachineToMSchInList.DataSource = dtMachineNew;
                        ddlMachineToMSchInList.DataTextField = "MACHINE_NAME";
                        ddlMachineToMSchInList.DataValueField = "MACHINE_FID";
                        ddlMachineToMSchInList.DataBind();
                        ddlMachineToMSchInList.Items.Insert(0, "Select");
                        ddlMachineToMSchInList.SelectedIndex = 0;
                    }
                }


                //if (ddlActivityToMSchInList.SelectedIndex > 0)
                //{
                //    //dsMachine = objMS.GetMachineActivity(Convert.ToInt32(ddlActivityToMSchInList.SelectedValue));

                //    if (dsMachine.Tables.Count > 0 && dsMachine.Tables[0].Rows.Count > 0)
                //    {
                //        ddlMachineToMSchInList.DataSource = dsMachine.Tables[0];
                //        ddlMachineToMSchInList.DataTextField = "MACHINE_NAME";
                //        ddlMachineToMSchInList.DataValueField = "MACHINE_FID";
                //        ddlMachineToMSchInList.DataBind();
                //        ddlMachineToMSchInList.Items.Insert(0, "Select");
                //        ddlMachineToMSchInList.SelectedIndex = 0;
                //    }
                //    else
                //    {
                //        ddlMachineToMSchInList.Items.Clear();
                //        ddlMachineToMSchInList.Items.Insert(0, "Select");
                //        ddlMachineToMSchInList.SelectedIndex = 0;
                //    }
                //}
                //else
                //{
                //    ddlMachineToMSchInList.Items.Clear();
                //    ddlMachineToMSchInList.Items.Insert(0, "Select");
                //    ddlMachineToMSchInList.SelectedIndex = 0;
                //}
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlActivityToMSch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            mpeScheduleMachine.Show();

            DataTable dtMachine = new DataTable();
            DataTable dtMachineNew = new DataTable();
            if (Session["dtMachine"] != null)
                dtMachine = (DataTable)Session["dtMachine"];
            else
                dtMachine = GetMachinesData();

            dtMachineNew = dtMachine.Copy();
            dtMachineNew.Rows.Clear();


            ddlMachineSchD.Items.Clear();
            ddlMachineSchD.Items.Insert(0, "Select");
            ddlMachineSchD.SelectedIndex = 0;

            if (ddlActivityToMSch.SelectedIndex > 0)
            {
                foreach (DataRow dr in dtMachine.Select("ACTIVITY_FID=" + Convert.ToInt32(ddlActivityToMSch.SelectedValue) + ""))
                {
                    DataRow drn = dtMachineNew.NewRow();
                    drn["ACTIVITY_FID"] = dr["ACTIVITY_FID"];
                    drn["MACHINE_FID"] = dr["MACHINE_FID"];
                    drn["MACHINE_NAME"] = dr["MACHINE_NAME"];

                    dtMachineNew.Rows.Add(drn);
                }

                if (dtMachineNew.Rows.Count > 0)
                {
                    ddlMachineSchD.DataSource = dtMachineNew;
                    ddlMachineSchD.DataTextField = "MACHINE_NAME";
                    ddlMachineSchD.DataValueField = "MACHINE_FID";
                    ddlMachineSchD.DataBind();
                    ddlMachineSchD.Items.Insert(0, "Select");
                    ddlMachineSchD.SelectedIndex = 0;
                }
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlAFH_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (gvSubitemsList.Rows.Count > 0)
            {

                GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

                Label lblAvailableFromToMSchInList = (Label)gvr.FindControl("lblAvailableFromToMSchInList");
                Label lblAvailableToMSchInList = (Label)gvr.FindControl("lblAvailableToMSchInList");

                Label lblWorkingStartTimeMSchInList = (Label)gvr.FindControl("lblWorkingStartTimeMSchInList");
                Label lblWorkingEndTimeMSchInList = (Label)gvr.FindControl("lblWorkingEndTimeMSchInList");

                TextBox txtLatestScheduledHoursToMSchInList = (TextBox)gvr.FindControl("txtLatestScheduledHoursToMSchInList");
                TextBox txtAvailableHoursToMSchInList = (TextBox)gvr.FindControl("txtAvailableHoursToMSchInList");

                DropDownList ddlAFH = (DropDownList)gvr.FindControl("ddlAFH");
                DropDownList ddlAFM = (DropDownList)gvr.FindControl("ddlAFM");

                DropDownList ddlATH = (DropDownList)gvr.FindControl("ddlATH");
                DropDownList ddlATM = (DropDownList)gvr.FindControl("ddlATM");

                string fromTime = string.Empty;
                string toTime = string.Empty;

                //fromtime can not be greter than to time
                fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
                toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");


                int fH = 0;
                int fM = 0;

                int tH = 0;
                int tM = 0;

                fH = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[0]);
                fM = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[1]);

                tH = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[0]);
                tM = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[1]);


                DataTable dtH = new DataTable();
                if (dtH.Columns.Count == 0)
                {
                    dtH.Columns.Add("H", typeof(string));
                }

                DataTable dtM = new DataTable();
                if (dtM.Columns.Count == 0)
                {
                    dtM.Columns.Add("M", typeof(string));
                }

                if (Convert.ToDateTime(fromTime) > Convert.ToDateTime(lblAvailableToMSchInList.Text) ||
                    Convert.ToDateTime(fromTime) < Convert.ToDateTime(lblAvailableFromToMSchInList.Text))
                {


                    for (int i = fH; i <= tH; i++)
                    {
                        DataRow drH = dtH.NewRow();
                        if (i < 10)
                            drH["H"] = "0" + Convert.ToString(i);
                        else drH["H"] = Convert.ToString(i);

                        dtH.Rows.Add(drH);
                    }

                    if (dtH.Rows.Count > 0)
                    {
                        ddlAFH.DataSource = dtH;
                        ddlAFH.DataBind();
                        ddlAFH.SelectedValue = Convert.ToString((fH < 10) ? ("0" + fH) : Convert.ToString(fH));
                    }

                    for (int i = 0; i <= 59; i++)
                    {
                        DataRow drM = dtM.NewRow();
                        if (i < 10)
                            drM["M"] = "0" + Convert.ToString(i);
                        else drM["M"] = Convert.ToString(i);

                        dtM.Rows.Add(drM);
                    }

                    if (dtM.Rows.Count > 0)
                    {
                        ddlAFM.DataSource = dtM;
                        ddlAFM.DataBind();
                        ddlAFM.SelectedValue = Convert.ToString((fM < 10) ? ("0" + fM) : Convert.ToString(fM));
                    }
                }


                fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
                toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");
                TimeSpan duration = DateTime.Parse(toTime).Subtract(DateTime.Parse(fromTime));

                int totalScheduledMinutes = Convert.ToInt32(duration.TotalMinutes);
                if (totalScheduledMinutes > 0)
                    txtLatestScheduledHoursToMSchInList.Text = Convert.ToDateTime(totalScheduledMinutes / 60 + ":" + totalScheduledMinutes % 60).ToString("HH:mm");
                else txtLatestScheduledHoursToMSchInList.Text = "00:00";


                string availableFromTime = Convert.ToDateTime(lblAvailableFromToMSchInList.Text).ToString("HH:mm");
                string availableToTime = Convert.ToDateTime(lblAvailableToMSchInList.Text).ToString("HH:mm");
                int totalAvailableMinutes = 0;

                TimeSpan tsAvailableDuration = DateTime.Parse(availableToTime).Subtract(DateTime.Parse(availableFromTime));
                totalAvailableMinutes = Convert.ToInt32(tsAvailableDuration.TotalMinutes);
                totalAvailableMinutes = (totalAvailableMinutes - totalScheduledMinutes);

                if (totalAvailableMinutes > 0)
                    txtAvailableHoursToMSchInList.Text = Convert.ToDateTime(totalAvailableMinutes / 60 + ":" + totalAvailableMinutes % 60).ToString("HH:mm");
                else txtAvailableHoursToMSchInList.Text = "00:00";



                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                chkSelect.Checked = false;
                if (totalScheduledMinutes > 0)
                {
                    txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightGreen;
                    chkSelect.Enabled = true;
                }
                else
                {
                    txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightPink;
                    chkSelect.Enabled = false;
                }


                mpeScheduleMachine.Show();
                mpeScheduleDates.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlAFM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (gvSubitemsList.Rows.Count > 0)
            {

                GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

                Label lblAvailableFromToMSchInList = (Label)gvr.FindControl("lblAvailableFromToMSchInList");
                Label lblAvailableToMSchInList = (Label)gvr.FindControl("lblAvailableToMSchInList");

                TextBox txtLatestScheduledHoursToMSchInList = (TextBox)gvr.FindControl("txtLatestScheduledHoursToMSchInList");
                TextBox txtAvailableHoursToMSchInList = (TextBox)gvr.FindControl("txtAvailableHoursToMSchInList");

                Label lblWorkingStartTimeMSchInList = (Label)gvr.FindControl("lblWorkingStartTimeMSchInList");
                Label lblWorkingEndTimeMSchInList = (Label)gvr.FindControl("lblWorkingEndTimeMSchInList");

                DropDownList ddlAFH = (DropDownList)gvr.FindControl("ddlAFH");
                DropDownList ddlAFM = (DropDownList)gvr.FindControl("ddlAFM");

                DropDownList ddlATH = (DropDownList)gvr.FindControl("ddlATH");
                DropDownList ddlATM = (DropDownList)gvr.FindControl("ddlATM");

                string fromTime = string.Empty;
                string toTime = string.Empty;

                //fromtime can not be greter than to time
                fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
                toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");


                if (Convert.ToDateTime(fromTime) > Convert.ToDateTime(lblAvailableToMSchInList.Text) ||
                    Convert.ToDateTime(fromTime) < Convert.ToDateTime(lblAvailableFromToMSchInList.Text))
                {
                    int fH = 0;
                    int fM = 0;

                    int tH = 0;
                    int tM = 0;

                    fH = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[0]);
                    fM = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[1]);

                    tH = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[0]);
                    tM = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[1]);


                    DataTable dtH = new DataTable();
                    if (dtH.Columns.Count == 0)
                    {
                        dtH.Columns.Add("H", typeof(string));
                    }

                    for (int i = fH; i <= tH; i++)
                    {
                        DataRow drH = dtH.NewRow();
                        if (i < 10)
                            drH["H"] = "0" + Convert.ToString(i);
                        else drH["H"] = Convert.ToString(i);

                        dtH.Rows.Add(drH);
                    }

                    if (dtH.Rows.Count > 0)
                    {
                        ddlAFH.DataSource = dtH;
                        ddlAFH.DataBind();
                        ddlAFH.SelectedValue = Convert.ToString((fH < 10) ? ("0" + fH) : Convert.ToString(fH));
                    }



                    DataTable dtM = new DataTable();
                    if (dtM.Columns.Count == 0)
                    {
                        dtM.Columns.Add("M", typeof(string));
                    }

                    for (int i = 0; i <= 59; i++)
                    {
                        DataRow drM = dtM.NewRow();
                        if (i < 10)
                            drM["M"] = "0" + Convert.ToString(i);
                        else drM["M"] = Convert.ToString(i);

                        dtM.Rows.Add(drM);
                    }

                    if (dtM.Rows.Count > 0)
                    {
                        ddlAFM.DataSource = dtM;
                        ddlAFM.DataBind();
                        ddlAFM.SelectedValue = Convert.ToString((fM < 10) ? ("0" + fM) : Convert.ToString(fM));
                    }

                }

                fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
                toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");
                TimeSpan duration = DateTime.Parse(toTime).Subtract(DateTime.Parse(fromTime));

                int totalScheduledMinutes = Convert.ToInt32(duration.TotalMinutes);
                if (totalScheduledMinutes > 0)
                    txtLatestScheduledHoursToMSchInList.Text = Convert.ToDateTime(totalScheduledMinutes / 60 + ":" + totalScheduledMinutes % 60).ToString("HH:mm");
                else txtLatestScheduledHoursToMSchInList.Text = "00:00";


                string availableFromTime = Convert.ToDateTime(lblAvailableFromToMSchInList.Text).ToString("HH:mm");
                string availableToTime = Convert.ToDateTime(lblAvailableToMSchInList.Text).ToString("HH:mm");
                int totalAvailableMinutes = 0;

                TimeSpan tsAvailableDuration = DateTime.Parse(availableToTime).Subtract(DateTime.Parse(availableFromTime));
                totalAvailableMinutes = Convert.ToInt32(tsAvailableDuration.TotalMinutes);
                totalAvailableMinutes = (totalAvailableMinutes - totalScheduledMinutes);

                if (totalAvailableMinutes > 0)
                    txtAvailableHoursToMSchInList.Text = Convert.ToDateTime(totalAvailableMinutes / 60 + ":" + totalAvailableMinutes % 60).ToString("HH:mm");
                else txtAvailableHoursToMSchInList.Text = "00:00";

                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                chkSelect.Checked = false;
                if (totalScheduledMinutes > 0)
                {
                    txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightGreen;
                    chkSelect.Enabled = true;
                }
                else
                {
                    txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightPink;
                    chkSelect.Enabled = false;
                }

                mpeScheduleMachine.Show();
                mpeScheduleDates.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlATH_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (gvSubitemsList.Rows.Count > 0)
            {

                GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

                Label lblAvailableFromToMSchInList = (Label)gvr.FindControl("lblAvailableFromToMSchInList");
                Label lblAvailableToMSchInList = (Label)gvr.FindControl("lblAvailableToMSchInList");

                TextBox txtLatestScheduledHoursToMSchInList = (TextBox)gvr.FindControl("txtLatestScheduledHoursToMSchInList");
                TextBox txtAvailableHoursToMSchInList = (TextBox)gvr.FindControl("txtAvailableHoursToMSchInList");

                Label lblWorkingStartTimeMSchInList = (Label)gvr.FindControl("lblWorkingStartTimeMSchInList");
                Label lblWorkingEndTimeMSchInList = (Label)gvr.FindControl("lblWorkingEndTimeMSchInList");

                DropDownList ddlAFH = (DropDownList)gvr.FindControl("ddlAFH");
                DropDownList ddlAFM = (DropDownList)gvr.FindControl("ddlAFM");

                DropDownList ddlATH = (DropDownList)gvr.FindControl("ddlATH");
                DropDownList ddlATM = (DropDownList)gvr.FindControl("ddlATM");

                string fromTime = string.Empty;
                string toTime = string.Empty;

                //fromtime can not be greter than to time
                fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
                toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");


                //if (Convert.ToDateTime(toTime) > Convert.ToDateTime(lblAvailableToMSchInList.Text))
                if (Convert.ToDateTime(toTime) > Convert.ToDateTime(lblAvailableToMSchInList.Text) ||
                    Convert.ToDateTime(toTime) < Convert.ToDateTime(lblAvailableFromToMSchInList.Text))
                {
                    int fH = 0;
                    int fM = 0;

                    int tH = 0;
                    int tM = 0;

                    fH = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[0]);
                    fM = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[1]);

                    tH = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[0]);
                    tM = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[1]);


                    DataTable dtH = new DataTable();
                    if (dtH.Columns.Count == 0)
                    {
                        dtH.Columns.Add("H", typeof(string));
                    }

                    for (int i = fH; i <= tH; i++)
                    {
                        DataRow drH = dtH.NewRow();
                        if (i < 10)
                            drH["H"] = "0" + Convert.ToString(i);
                        else drH["H"] = Convert.ToString(i);

                        dtH.Rows.Add(drH);
                    }

                    if (dtH.Rows.Count > 0)
                    {
                        ddlATH.DataSource = dtH;
                        ddlATH.DataBind();
                        ddlATH.SelectedValue = Convert.ToString((tH < 10) ? ("0" + tH) : Convert.ToString(tH));
                    }



                    DataTable dtM = new DataTable();
                    if (dtM.Columns.Count == 0)
                    {
                        dtM.Columns.Add("M", typeof(string));
                    }

                    for (int i = 0; i <= 59; i++)
                    {
                        DataRow drM = dtM.NewRow();
                        if (i < 10)
                            drM["M"] = "0" + Convert.ToString(i);
                        else drM["M"] = Convert.ToString(i);

                        dtM.Rows.Add(drM);
                    }

                    if (dtM.Rows.Count > 0)
                    {
                        ddlATM.DataSource = dtM;
                        ddlATM.DataBind();
                        ddlATM.SelectedValue = Convert.ToString((tM < 10) ? ("0" + tM) : Convert.ToString(tM));
                    }

                }

                fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
                toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");
                TimeSpan duration = DateTime.Parse(toTime).Subtract(DateTime.Parse(fromTime));

                int totalScheduledMinutes = Convert.ToInt32(duration.TotalMinutes);
                if (totalScheduledMinutes > 0)
                    txtLatestScheduledHoursToMSchInList.Text = Convert.ToDateTime(totalScheduledMinutes / 60 + ":" + totalScheduledMinutes % 60).ToString("HH:mm");
                else txtLatestScheduledHoursToMSchInList.Text = "00:00";


                string availableFromTime = Convert.ToDateTime(lblAvailableFromToMSchInList.Text).ToString("HH:mm");
                string availableToTime = Convert.ToDateTime(lblAvailableToMSchInList.Text).ToString("HH:mm");
                int totalAvailableMinutes = 0;

                TimeSpan tsAvailableDuration = DateTime.Parse(availableToTime).Subtract(DateTime.Parse(availableFromTime));
                totalAvailableMinutes = Convert.ToInt32(tsAvailableDuration.TotalMinutes);
                totalAvailableMinutes = (totalAvailableMinutes - totalScheduledMinutes);

                if (totalAvailableMinutes > 0)
                    txtAvailableHoursToMSchInList.Text = Convert.ToDateTime(totalAvailableMinutes / 60 + ":" + totalAvailableMinutes % 60).ToString("HH:mm");
                else txtAvailableHoursToMSchInList.Text = "00:00";

                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                chkSelect.Checked = false;
                if (totalScheduledMinutes > 0)
                {
                    txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightGreen;
                    chkSelect.Enabled = true;
                }
                else
                {
                    txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightPink;
                    chkSelect.Enabled = false;
                }

                mpeScheduleMachine.Show();
                mpeScheduleDates.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlATM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (gvSubitemsList.Rows.Count > 0)
            {

                GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

                Label lblAvailableFromToMSchInList = (Label)gvr.FindControl("lblAvailableFromToMSchInList");
                Label lblAvailableToMSchInList = (Label)gvr.FindControl("lblAvailableToMSchInList");

                TextBox txtLatestScheduledHoursToMSchInList = (TextBox)gvr.FindControl("txtLatestScheduledHoursToMSchInList");
                TextBox txtAvailableHoursToMSchInList = (TextBox)gvr.FindControl("txtAvailableHoursToMSchInList");

                Label lblWorkingStartTimeMSchInList = (Label)gvr.FindControl("lblWorkingStartTimeMSchInList");
                Label lblWorkingEndTimeMSchInList = (Label)gvr.FindControl("lblWorkingEndTimeMSchInList");

                DropDownList ddlAFH = (DropDownList)gvr.FindControl("ddlAFH");
                DropDownList ddlAFM = (DropDownList)gvr.FindControl("ddlAFM");

                DropDownList ddlATH = (DropDownList)gvr.FindControl("ddlATH");
                DropDownList ddlATM = (DropDownList)gvr.FindControl("ddlATM");

                string fromTime = string.Empty;
                string toTime = string.Empty;

                //fromtime can not be greter than to time
                fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
                toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");


                //if (Convert.ToDateTime(toTime) > Convert.ToDateTime(lblAvailableToMSchInList.Text))
                if (Convert.ToDateTime(toTime) > Convert.ToDateTime(lblAvailableToMSchInList.Text) ||
                    Convert.ToDateTime(toTime) < Convert.ToDateTime(lblAvailableFromToMSchInList.Text))
                {
                    int fH = 0;
                    int fM = 0;

                    int tH = 0;
                    int tM = 0;

                    fH = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[0]);
                    fM = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[1]);

                    tH = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[0]);
                    tM = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[1]);


                    DataTable dtH = new DataTable();
                    if (dtH.Columns.Count == 0)
                    {
                        dtH.Columns.Add("H", typeof(string));
                    }

                    for (int i = fH; i <= tH; i++)
                    {
                        DataRow drH = dtH.NewRow();
                        if (i < 10)
                            drH["H"] = "0" + Convert.ToString(i);
                        else drH["H"] = Convert.ToString(i);

                        dtH.Rows.Add(drH);
                    }

                    if (dtH.Rows.Count > 0)
                    {
                        ddlATH.DataSource = dtH;
                        ddlATH.DataBind();
                        ddlATH.SelectedValue = Convert.ToString((tH < 10) ? ("0" + tH) : Convert.ToString(tH));
                    }



                    DataTable dtM = new DataTable();
                    if (dtM.Columns.Count == 0)
                    {
                        dtM.Columns.Add("M", typeof(string));
                    }

                    for (int i = 0; i <= 59; i++)
                    {
                        DataRow drM = dtM.NewRow();
                        if (i < 10)
                            drM["M"] = "0" + Convert.ToString(i);
                        else drM["M"] = Convert.ToString(i);

                        dtM.Rows.Add(drM);
                    }

                    if (dtM.Rows.Count > 0)
                    {
                        ddlATM.DataSource = dtM;
                        ddlATM.DataBind();
                        ddlATM.SelectedValue = Convert.ToString((tM < 10) ? ("0" + tM) : Convert.ToString(tM));
                    }

                }


                fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
                toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");
                TimeSpan duration = DateTime.Parse(toTime).Subtract(DateTime.Parse(fromTime));

                int totalScheduledMinutes = Convert.ToInt32(duration.TotalMinutes);
                if (totalScheduledMinutes > 0)
                    txtLatestScheduledHoursToMSchInList.Text = Convert.ToDateTime(totalScheduledMinutes / 60 + ":" + totalScheduledMinutes % 60).ToString("HH:mm");
                else txtLatestScheduledHoursToMSchInList.Text = "00:00";


                string availableFromTime = Convert.ToDateTime(lblAvailableFromToMSchInList.Text).ToString("HH:mm");
                string availableToTime = Convert.ToDateTime(lblAvailableToMSchInList.Text).ToString("HH:mm");
                int totalAvailableMinutes = 0;

                TimeSpan tsAvailableDuration = DateTime.Parse(availableToTime).Subtract(DateTime.Parse(availableFromTime));
                totalAvailableMinutes = Convert.ToInt32(tsAvailableDuration.TotalMinutes);
                totalAvailableMinutes = (totalAvailableMinutes - totalScheduledMinutes);

                if (totalAvailableMinutes > 0)
                    txtAvailableHoursToMSchInList.Text = Convert.ToDateTime(totalAvailableMinutes / 60 + ":" + totalAvailableMinutes % 60).ToString("HH:mm");
                else txtAvailableHoursToMSchInList.Text = "00:00";

                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                chkSelect.Checked = false;
                if (totalScheduledMinutes > 0)
                {
                    txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightGreen;
                    chkSelect.Enabled = true;
                }
                else
                {
                    txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightPink;
                    chkSelect.Enabled = false;
                }

                mpeScheduleMachine.Show();
                mpeScheduleDates.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlActivityToCMSch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            mpeSchedulingCompletion.Show();

            DataTable dtMachine = new DataTable();
            DataTable dtMachineNew = new DataTable();
            if (Session["dtMachine"] != null)
                dtMachine = (DataTable)Session["dtMachine"];
            else
                dtMachine = GetMachinesData();

            dtMachineNew = dtMachine.Copy();
            dtMachineNew.Rows.Clear();


            ddlMachineToCMSch.Items.Clear();
            ddlMachineToCMSch.Items.Insert(0, "All");
            ddlMachineToCMSch.SelectedIndex = 0;

            if (ddlActivityToCMSch.SelectedIndex > 0)
            {
                foreach (DataRow dr in dtMachine.Select("ACTIVITY_FID=" + Convert.ToInt32(ddlActivityToCMSch.SelectedValue) + ""))
                {
                    DataRow drn = dtMachineNew.NewRow();
                    drn["ACTIVITY_FID"] = dr["ACTIVITY_FID"];
                    drn["MACHINE_FID"] = dr["MACHINE_FID"];
                    drn["MACHINE_NAME"] = dr["MACHINE_NAME"];

                    dtMachineNew.Rows.Add(drn);
                }

                if (dtMachineNew.Rows.Count > 0)
                {
                    ddlMachineToCMSch.DataSource = dtMachineNew;
                    ddlMachineToCMSch.DataTextField = "MACHINE_NAME";
                    ddlMachineToCMSch.DataValueField = "MACHINE_FID";
                    ddlMachineToCMSch.DataBind();
                    ddlMachineToCMSch.Items.Insert(0, "All");
                    ddlMachineToCMSch.SelectedIndex = 0;
                }
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlCategoryToA_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Reset();
        mpeUpdateDetails.Show();
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

    /// <summary>
    /// CheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    protected void chkSelectAllSchD_CheckedChanged(object sender, EventArgs e)
    {
        if (gvAvailableDatesList.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvAvailableDatesList.Rows)
            {
                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                if (chkSelectAllSchD.Checked)
                {
                    if (chkSelect.Enabled)
                    {
                        chkSelect.Checked = true;
                    }
                }
                else
                {
                    chkSelect.Checked = false;
                }
            }
        }

        mpeScheduleMachine.Show();
        mpeScheduleDates.Show();
    }

    protected void chkSelectAllToCMSch_CheckedChanged(object sender, EventArgs e)
    {
        if (gvScheduledMachineListForCompletion.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvScheduledMachineListForCompletion.Rows)
            {
                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                if (chkSelectAllToCMSch.Checked)
                {
                    if (chkSelect.Enabled)
                    {
                        chkSelect.Checked = true;
                    }
                }
                else
                {
                    chkSelect.Checked = false;
                }
            }
        }

        mpeSchedulingCompletion.Show();
    }



    /// <summary>
    /// Row Commands
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void gvSubitemsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            hdIsNewRecord.Value = "0";
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "MODIFY" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_DETAIL" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_SCH_DETAILS" ||
                    Convert.ToString(e.CommandArgument) == "ViewADDDRAWING" ||
                    Convert.ToString(e.CommandArgument) == "ViewSIDRAWING1" ||
                    Convert.ToString(e.CommandArgument) == "ViewSIDRAWING2")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                if (Convert.ToString(e.CommandArgument) == "ACCEPT" ||
                    Convert.ToString(e.CommandArgument) == "SCHEDULING" ||
                    Convert.ToString(e.CommandArgument) == "COMPLETE" ||
                    Convert.ToString(e.CommandArgument) == "INSPECT")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                btnCompleteScheduling.Visible = false;
                btnModifySchedulingDetails.Visible = false;

                TextBox txtSrNo = gvSubitemsList.Rows[rowindex].FindControl("txtSrNo") as TextBox;

                Label lblRecordID = gvSubitemsList.Rows[rowindex].FindControl("lblRecordID") as Label;

                Label lblLOTTFID = gvSubitemsList.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblLOTTFSubitemID = gvSubitemsList.Rows[rowindex].FindControl("lblLOTTFSubitemID") as Label;
                Label lblUnitID = gvSubitemsList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblCategoryID = gvSubitemsList.Rows[rowindex].FindControl("lblCategoryID") as Label;

                TextBox txtAssignedDrawingCode = gvSubitemsList.Rows[rowindex].FindControl("txtAssignedDrawingCode") as TextBox;
                TextBox txtUnit = gvSubitemsList.Rows[rowindex].FindControl("txtUnit") as TextBox;
                TextBox txtLOTNo = gvSubitemsList.Rows[rowindex].FindControl("txtLOTNo") as TextBox;
                TextBox txtJOBNo = gvSubitemsList.Rows[rowindex].FindControl("txtJOBNo") as TextBox;
                TextBox txtDrawingNo = gvSubitemsList.Rows[rowindex].FindControl("txtDrawingNo") as TextBox;
                TextBox txtEquipment = gvSubitemsList.Rows[rowindex].FindControl("txtEquipment") as TextBox;
                TextBox txtTagNo = gvSubitemsList.Rows[rowindex].FindControl("txtTagNo") as TextBox;
                TextBox txtItemName = gvSubitemsList.Rows[rowindex].FindControl("txtItemName") as TextBox;
                TextBox txtItemDetail = gvSubitemsList.Rows[rowindex].FindControl("txtItemDetail") as TextBox;
                TextBox txtExpectedDateofComp = gvSubitemsList.Rows[rowindex].FindControl("txtExpectedDateofComp") as TextBox;
                TextBox txtQuantity = gvSubitemsList.Rows[rowindex].FindControl("txtQuantity") as TextBox;
                TextBox txtRemarks = gvSubitemsList.Rows[rowindex].FindControl("txtRemarks") as TextBox;
                Label lblAdditinoalDrawingFilePath = gvSubitemsList.Rows[rowindex].FindControl("lblAdditinoalDrawingFilePath") as Label;

                Label lblQaInspectionRejectedQuantity = gvSubitemsList.Rows[rowindex].FindControl("lblQaInspectionRejectedQuantity") as Label;

                Label lblScheduledCounts = gvSubitemsList.Rows[rowindex].FindControl("lblScheduledCounts") as Label;
                Label lblCompletedCounts = gvSubitemsList.Rows[rowindex].FindControl("lblCompletedCounts") as Label;

                ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);
                ViewState["DRAWING_NO"] = Convert.ToString(txtDrawingNo.Text);
                ViewState["ASSIGNED_DRAWING_CODE"] = Convert.ToString(txtAssignedDrawingCode.Text);

                ViewState["SCHEDULED_COUNTS"] = Convert.ToInt32(lblScheduledCounts.Text);
                ViewState["COMPLETED_COUNTS"] = Convert.ToInt32(lblCompletedCounts.Text);


                lblCompletionDateToCMSch.Visible = false;
                tblCompletionDateToCMSch.Visible = false;

                lblSelectAllToCMSch.Visible = false;
                tblSelectAllToCMSch.Visible = false;

                //hdModifyWorker.Value = "0";
                //hdViewScheduling.Value = "0";
                //hdCompleteScheduling.Value = "0";

                hdRowCommandFlags.Value = "0";

                if (Convert.ToString(e.CommandArgument) == "EDIT")
                {
                    lblLegend.Text = "Update Details of [" + Convert.ToString(txtAssignedDrawingCode.Text) + "]";

                    if (!string.IsNullOrEmpty(Convert.ToString(lblAdditinoalDrawingFilePath.Text)))
                    {
                        txtAddDrawingFilePathToA.Text = Convert.ToString(lblAdditinoalDrawingFilePath.Text);
                        pnlUploadAddDrawing.Visible = false;
                        pnlViewAddDrawing.Visible = true;
                        imgBtnUndoAddDrawing.Visible = true;
                    }
                    else
                    {
                        txtAddDrawingFilePathToA.Text = string.Empty;
                        pnlUploadAddDrawing.Visible = true;
                        pnlViewAddDrawing.Visible = false;
                        imgBtnUndoAddDrawing.Visible = false;
                    }

                    hdLOTTFID.Value = Convert.ToString(lblLOTTFID.Text);
                    hdLOTTFSubitemID.Value = Convert.ToString(lblLOTTFSubitemID.Text);
                    ddlCompanyToA.SelectedValue = Convert.ToString(lblUnitID.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblCategoryID.Text)))
                        ddlCategoryToA.SelectedValue = Convert.ToString(lblCategoryID.Text);

                    if (Convert.ToInt32(ddlCategoryToA.SelectedValue) == 2)
                    {
                        btnGetDrawingNo.Visible = false;
                        btnGetJOBNo.Visible = false;
                    }

                    txtJOBNoToA.Text = Convert.ToString(txtJOBNo.Text);
                    txtDrawingNoToA.Text = Convert.ToString(txtDrawingNo.Text);
                    txtEquipmentToA.Text = Convert.ToString(txtEquipment.Text);
                    txtTagNoToA.Text = Convert.ToString(txtTagNo.Text);
                    txtItemNameToA.Text = Convert.ToString(txtItemName.Text);
                    txtItemDetailToA.Text = Convert.ToString(txtItemDetail.Text);
                    txtExpectedDateofComp.Text = Convert.ToString(txtExpectedDateofComp.Text);
                    txtQuantityToA.Text = Convert.ToString(txtQuantity.Text);
                    txtRemarkstoA.Text = Convert.ToString(txtRemarks.Text);
                    mpeUpdateDetails.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "ACCEPT")
                {
                    imgBtnViewAddDrawingToUSt.Visible = false;
                    if (!string.IsNullOrEmpty(Convert.ToString(lblAdditinoalDrawingFilePath.Text)))
                    {
                        imgBtnViewAddDrawingToUSt.Visible = true;
                    }
                    txtAcceptedOrRejectedRemarksToUst.Text = string.Empty;
                    lblLegendUpdateStatus.Text = "Update Status of [" + Convert.ToString(txtAssignedDrawingCode.Text) + "]";
                    txtAddDrawingFilePathToUSt.Text = Convert.ToString(lblAdditinoalDrawingFilePath.Text);
                    txtUnitToUSt.Text = Convert.ToString(txtUnit.Text);
                    txtJOBNoToUSt.Text = Convert.ToString(txtJOBNo.Text);
                    txtDrawingNoToUSt.Text = Convert.ToString(txtDrawingNo.Text);
                    txtEquipmentToUSt.Text = Convert.ToString(txtEquipment.Text);
                    txtTagNoToUSt.Text = Convert.ToString(txtTagNo.Text);
                    txtItemNameToUSt.Text = Convert.ToString(txtItemName.Text);
                    txtItemDetailToUSt.Text = Convert.ToString(txtItemDetail.Text);
                    txtDateToUSt.Text = Convert.ToString(txtExpectedDateofComp.Text);
                    txtQuantityToUSt.Text = Convert.ToString(txtQuantity.Text);
                    txtRemarksToUSt.Text = Convert.ToString(txtRemarks.Text);
                    mpeUpdateStatus.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "SCHEDULING")
                {
                    Session["dtMachine"] = null;
                    Session["dtMachineSchedulingList"] = null;


                    //BindTypeData();
                    BindActivityData();

                    //ddlMachineSchD.Items.Clear();
                    //ddlMachineSchD.Items.Insert(0, "Select");
                    //ddlMachineSchD.SelectedIndex = 0;

                    gvMachineSchedulingList.DataSource = null;
                    gvMachineSchedulingList.DataBind();

                    imgBtnViewAddDrawingToMSch.Visible = false;
                    if (!string.IsNullOrEmpty(Convert.ToString(lblAdditinoalDrawingFilePath.Text)))
                    {
                        imgBtnViewAddDrawingToMSch.Visible = true;
                    }
                    lblLegendUpdateStatus.Text = "Update Status of [" + Convert.ToString(txtAssignedDrawingCode.Text) + "]";
                    txtAddDrawingFilePathToMSch.Text = Convert.ToString(lblAdditinoalDrawingFilePath.Text);
                    txtAssignedDrawingCodeToMSch.Text = Convert.ToString(txtAssignedDrawingCode.Text);
                    txtUnitToMSch.Text = Convert.ToString(txtUnit.Text);
                    txtJOBNoToMSch.Text = Convert.ToString(txtJOBNo.Text);
                    txtDrawingNoToMSch.Text = Convert.ToString(txtDrawingNo.Text);
                    txtEquipmentToMSch.Text = Convert.ToString(txtEquipment.Text);
                    txtTagNoToMSch.Text = Convert.ToString(txtTagNo.Text);
                    txtItemNameToMSch.Text = Convert.ToString(txtItemName.Text);
                    txtItemDetailToMSch.Text = Convert.ToString(txtItemDetail.Text);
                    txtDateToMSch.Text = Convert.ToString(txtExpectedDateofComp.Text);

                    if (!string.IsNullOrEmpty(lblQaInspectionRejectedQuantity.Text) && Convert.ToInt32(lblQaInspectionRejectedQuantity.Text) > 0)
                        txtQuantityToMSch.Text = Convert.ToString(lblQaInspectionRejectedQuantity.Text);
                    else
                        txtQuantityToMSch.Text = Convert.ToString(txtQuantity.Text);

                    hdCommitedDateByShopInchargeToMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtCommitedDateByShopInchargeToMSch.Text = hdCommitedDateByShopInchargeToMSch.Value;

                    hdDateOfReceiptOfMaterialToMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtDateOfReceiptOfMaterialToMSch.Text = hdDateOfReceiptOfMaterialToMSch.Value;

                    mpeScheduleMachine.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "COMPLETE")
                {
                    lblLegendSchedulingCompletion.Text = "Complete Scheduling";

                    lblCompletionDateToCMSch.Visible = true;
                    tblCompletionDateToCMSch.Visible = true;

                    lblSelectAllToCMSch.Visible = true;
                    tblSelectAllToCMSch.Visible = true;

                    hdRowCommandFlags.Value = Convert.ToString(Convert.ToInt32(MSAllStatusAndTypes.EnumRowCommandFlags.CompleteScheduling));

                    btnCompleteScheduling.Visible = true;

                    chkSelectAllToCMSch.Checked = false;
                    ddlMachineToCMSch.Items.Clear();
                    ddlMachineToCMSch.Items.Insert(0, "All");
                    ddlMachineToCMSch.SelectedIndex = 0;


                    BindTypeDataForCompletion();
                    BindActivityDataForCompletion();
                    BindWorkerForCompletion();

                    txtUnitToCMSch.Text = txtUnit.Text;
                    txtAssignedDrawingCodeToCMSch.Text = txtAssignedDrawingCode.Text;
                    txtJOBNoToCMSch.Text = txtJOBNo.Text;
                    txtDrawingNoToCMSch.Text = txtDrawingNo.Text;

                    //hdScheduledFromToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    //txtScheduledFromToCMSch.Text = hdScheduledFromToCMSch.Value;

                    //hdScheduledToToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    //txtScheduledToToCMSch.Text = hdScheduledToToCMSch.Value;

                    hdScheduledFromToCMSch.Value = string.Empty;
                    txtScheduledFromToCMSch.Text = hdScheduledFromToCMSch.Value;

                    hdScheduledToToCMSch.Value = string.Empty;
                    txtScheduledToToCMSch.Text = hdScheduledToToCMSch.Value;

                    hdCompletionDateToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtCompletionDateToCMSch.Text = hdCompletionDateToCMSch.Value;

                    string fromDate = string.Empty;
                    string toDate = string.Empty;
                    int activityID = 0;
                    int machineID = 0;
                    int typeOfWorkID = 0;
                    int workerID = 0;

                    if (!string.IsNullOrEmpty(txtScheduledFromToCMSch.Text) && !string.IsNullOrEmpty(hdScheduledFromToCMSch.Value))
                        fromDate = Convert.ToDateTime(hdScheduledFromToCMSch.Value).ToString("yyyy-MM-dd");

                    if (!string.IsNullOrEmpty(txtScheduledToToCMSch.Text) && !string.IsNullOrEmpty(hdScheduledToToCMSch.Value))
                        toDate = Convert.ToDateTime(hdScheduledToToCMSch.Value).ToString("yyyy-MM-dd");

                    if (ddlActivityToCMSch.SelectedIndex > 0)
                        activityID = Convert.ToInt32(ddlActivityToCMSch.SelectedValue);

                    if (ddlMachineToCMSch.SelectedIndex > 0)
                        machineID = Convert.ToInt32(ddlMachineToCMSch.SelectedValue);

                    if (ddlTypeOfWorkToCMSch.SelectedIndex > 0)
                        typeOfWorkID = Convert.ToInt32(ddlTypeOfWorkToCMSch.SelectedValue);

                    if (ddlWorkerToCMSch.SelectedIndex > 0)
                        workerID = Convert.ToInt32(ddlWorkerToCMSch.SelectedValue);

                    BindScheduledListForCompletion(Convert.ToInt32(lblRecordID.Text), fromDate, toDate, activityID, machineID, typeOfWorkID, workerID);

                    mpeSchedulingCompletion.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "INSPECT")
                {
                    txtUnitToQaInsp.Text = txtUnit.Text;
                    txtJOBNoToQaInsp.Text = txtJOBNo.Text;
                    txtLOTNoToQaInsp.Text = txtLOTNo.Text;
                    txtDrawingNoToQaInsp.Text = txtDrawingNo.Text;
                    txtEquipmentToQaInsp.Text = txtEquipment.Text;
                    txtTagNoToQaInsp.Text = txtTagNo.Text;
                    txtItemNameToQaInsp.Text = txtItemName.Text;
                    txtItemDetailToQaInsp.Text = txtItemDetail.Text;
                    txtExpectedDateofCompletionToQaInsp.Text = txtExpectedDateofComp.Text;


                    if (!string.IsNullOrEmpty(lblQaInspectionRejectedQuantity.Text) && Convert.ToInt32(lblQaInspectionRejectedQuantity.Text) > 0)
                        txtAllocatedQuantityToQaInsp.Text = Convert.ToString(lblQaInspectionRejectedQuantity.Text);
                    else txtAllocatedQuantityToQaInsp.Text = txtQuantity.Text;

                    txtCompletionRemarksToQaInsp.Text = txtRemarks.Text;

                    txtInspectedAcceptedRemarksToQaInsp.Text = string.Empty;
                    txtInspectedRejectedRemarksToQaInsp.Text = string.Empty;



                    txtAcceptedQuantityToQaInsp.Text = txtAllocatedQuantityToQaInsp.Text;


                    txtRejectedQuantityToQaInsp.Text = "0";

                    mpeQualityInspection.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "MODIFY")
                {
                    hdRowCommandFlags.Value = Convert.ToString(Convert.ToInt32(MSAllStatusAndTypes.EnumRowCommandFlags.ModifyWorker));

                    lblLegendSchedulingCompletion.Text = "Modify Worker";

                    lblSelectAllToCMSch.Visible = true;
                    tblSelectAllToCMSch.Visible = true;

                    btnModifySchedulingDetails.Visible = true;

                    chkSelectAllToCMSch.Checked = false;
                    ddlMachineToCMSch.Items.Clear();
                    ddlMachineToCMSch.Items.Insert(0, "All");
                    ddlMachineToCMSch.SelectedIndex = 0;


                    BindTypeDataForCompletion();
                    BindActivityDataForCompletion();
                    BindWorkerForCompletion();

                    txtUnitToCMSch.Text = txtUnit.Text;
                    txtAssignedDrawingCodeToCMSch.Text = txtAssignedDrawingCode.Text;
                    txtJOBNoToCMSch.Text = txtJOBNo.Text;
                    txtDrawingNoToCMSch.Text = txtDrawingNo.Text;

                    //hdScheduledFromToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    //txtScheduledFromToCMSch.Text = hdScheduledFromToCMSch.Value;

                    //hdScheduledToToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    //txtScheduledToToCMSch.Text = hdScheduledToToCMSch.Value;

                    hdScheduledFromToCMSch.Value = string.Empty;
                    txtScheduledFromToCMSch.Text = hdScheduledFromToCMSch.Value;

                    hdScheduledToToCMSch.Value = string.Empty;
                    txtScheduledToToCMSch.Text = hdScheduledToToCMSch.Value;

                    hdCompletionDateToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtCompletionDateToCMSch.Text = hdCompletionDateToCMSch.Value;

                    string fromDate = string.Empty;
                    string toDate = string.Empty;
                    int activityID = 0;
                    int machineID = 0;
                    int typeOfWorkID = 0;
                    int workerID = 0;

                    if (!string.IsNullOrEmpty(txtScheduledFromToCMSch.Text) && !string.IsNullOrEmpty(hdScheduledFromToCMSch.Value))
                        fromDate = Convert.ToDateTime(hdScheduledFromToCMSch.Value).ToString("yyyy-MM-dd");

                    if (!string.IsNullOrEmpty(txtScheduledToToCMSch.Text) && !string.IsNullOrEmpty(hdScheduledToToCMSch.Value))
                        toDate = Convert.ToDateTime(hdScheduledToToCMSch.Value).ToString("yyyy-MM-dd");

                    if (ddlActivityToCMSch.SelectedIndex > 0)
                        activityID = Convert.ToInt32(ddlActivityToCMSch.SelectedValue);

                    if (ddlMachineToCMSch.SelectedIndex > 0)
                        machineID = Convert.ToInt32(ddlMachineToCMSch.SelectedValue);

                    if (ddlTypeOfWorkToCMSch.SelectedIndex > 0)
                        typeOfWorkID = Convert.ToInt32(ddlTypeOfWorkToCMSch.SelectedValue);

                    if (ddlWorkerToCMSch.SelectedIndex > 0)
                        workerID = Convert.ToInt32(ddlWorkerToCMSch.SelectedValue);

                    BindScheduledListForCompletion(Convert.ToInt32(lblRecordID.Text), fromDate, toDate, activityID, machineID, typeOfWorkID, workerID);

                    mpeSchedulingCompletion.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "VIEW_SCH_DETAILS")
                {
                    hdRowCommandFlags.Value = Convert.ToString(Convert.ToInt32(MSAllStatusAndTypes.EnumRowCommandFlags.ViewSchedulingDetails));

                    lblLegendSchedulingCompletion.Text = "View Scheduling Details";

                    chkSelectAllToCMSch.Checked = false;
                    ddlMachineToCMSch.Items.Clear();
                    ddlMachineToCMSch.Items.Insert(0, "All");
                    ddlMachineToCMSch.SelectedIndex = 0;


                    BindTypeDataForCompletion();
                    BindActivityDataForCompletion();
                    BindWorkerForCompletion();

                    txtUnitToCMSch.Text = txtUnit.Text;
                    txtAssignedDrawingCodeToCMSch.Text = txtAssignedDrawingCode.Text;
                    txtJOBNoToCMSch.Text = txtJOBNo.Text;
                    txtDrawingNoToCMSch.Text = txtDrawingNo.Text;

                    //hdScheduledFromToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    //txtScheduledFromToCMSch.Text = hdScheduledFromToCMSch.Value;

                    //hdScheduledToToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    //txtScheduledToToCMSch.Text = hdScheduledToToCMSch.Value;

                    hdScheduledFromToCMSch.Value = string.Empty;
                    txtScheduledFromToCMSch.Text = hdScheduledFromToCMSch.Value;

                    hdScheduledToToCMSch.Value = string.Empty;
                    txtScheduledToToCMSch.Text = hdScheduledToToCMSch.Value;

                    hdCompletionDateToCMSch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtCompletionDateToCMSch.Text = hdCompletionDateToCMSch.Value;

                    string fromDate = string.Empty;
                    string toDate = string.Empty;
                    int activityID = 0;
                    int machineID = 0;
                    int typeOfWorkID = 0;
                    int workerID = 0;

                    if (!string.IsNullOrEmpty(txtScheduledFromToCMSch.Text) && !string.IsNullOrEmpty(hdScheduledFromToCMSch.Value))
                        fromDate = Convert.ToDateTime(hdScheduledFromToCMSch.Value).ToString("yyyy-MM-dd");

                    if (!string.IsNullOrEmpty(txtScheduledToToCMSch.Text) && !string.IsNullOrEmpty(hdScheduledToToCMSch.Value))
                        toDate = Convert.ToDateTime(hdScheduledToToCMSch.Value).ToString("yyyy-MM-dd");

                    if (ddlActivityToCMSch.SelectedIndex > 0)
                        activityID = Convert.ToInt32(ddlActivityToCMSch.SelectedValue);

                    if (ddlMachineToCMSch.SelectedIndex > 0)
                        machineID = Convert.ToInt32(ddlMachineToCMSch.SelectedValue);

                    if (ddlTypeOfWorkToCMSch.SelectedIndex > 0)
                        typeOfWorkID = Convert.ToInt32(ddlTypeOfWorkToCMSch.SelectedValue);

                    if (ddlWorkerToCMSch.SelectedIndex > 0)
                        workerID = Convert.ToInt32(ddlWorkerToCMSch.SelectedValue);

                    BindScheduledListForCompletion(Convert.ToInt32(lblRecordID.Text), fromDate, toDate, activityID, machineID, typeOfWorkID, workerID);

                    mpeSchedulingCompletion.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    mpeViewInPDF.Show();
                    iframeViewDetailsInPDF.Attributes.Add("src", "MSDetailInPDF.aspx?recordID=" + Convert.ToInt32(lblRecordID.Text) + "");
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewADDDRAWING")
                    ViewDrawingFiles(Convert.ToInt32(lblRecordID.Text), "ADD_DRAWING");

                else if (Convert.ToString(e.CommandArgument) == "ViewSIDRAWING1")
                    ViewSIDrawingFiles(Convert.ToInt32(lblLOTTFSubitemID.Text), "SI_DRAWING1");

                else if (Convert.ToString(e.CommandArgument) == "ViewSIDRAWING2")
                    ViewSIDrawingFiles(Convert.ToInt32(lblLOTTFSubitemID.Text), "SI_DRAWING2");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvMachineSchedulingList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            hdIsNewRecord.Value = "0";
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE" ||
                    Convert.ToString(e.CommandArgument) == "SCHEDULE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblActivityID = gvMachineSchedulingList.Rows[rowindex].FindControl("lblActivityID") as Label;
                Label lblMachineID = gvMachineSchedulingList.Rows[rowindex].FindControl("lblMachineID") as Label;

                TextBox txtActivityToMSchInList = gvMachineSchedulingList.Rows[rowindex].FindControl("txtActivityToMSchInList") as TextBox;
                TextBox txtScheduledDateToMSchInList = gvMachineSchedulingList.Rows[rowindex].FindControl("txtScheduledDateToMSchInList") as TextBox;

                DropDownList ddlActivityToMSchInList = gvMachineSchedulingList.Rows[rowindex].FindControl("ddlActivityToMSchInList") as DropDownList;
                DropDownList ddlMachineToMSchInList = gvMachineSchedulingList.Rows[rowindex].FindControl("ddlMachineToMSchInList") as DropDownList;
                TextBox txtSrNoToMSchInList = gvMachineSchedulingList.Rows[rowindex].FindControl("txtSrNoToMSchInList") as TextBox;


                ViewState["SR_NO"] = txtSrNoToMSchInList.Text;

                if (Convert.ToString(e.CommandArgument) == "EDIT")
                {
                    #region MyRegion

                    //lblActivitySchD.Text = lblActivityID.Text;
                    //txtActivitySchD.Text = txtActivityToMSchInList.Text;

                    //hdScheduledFromSchD.Value = txtScheduledDateToMSchInList.Text;
                    //txtScheduledFromSchD.Text = hdScheduledFromSchD.Value;

                    //hdScheduledToSchD.Value = txtScheduledDateToMSchInList.Text;
                    //txtScheduledToSchD.Text = hdScheduledToSchD.Value;



                    //DataTable dtMachine = new DataTable();
                    //DataTable dtMachineNew = new DataTable();
                    //if (Session["dtMachine"] != null)
                    //    dtMachine = (DataTable)Session["dtMachine"];
                    //else
                    //    dtMachine = GetMachinesData();

                    //dtMachineNew = dtMachine.Copy();
                    //dtMachineNew.Rows.Clear();


                    //ddlMachineSchD.Items.Clear();
                    //ddlMachineSchD.Items.Insert(0, "Select");
                    //ddlMachineSchD.SelectedIndex = 0;

                    //if (Convert.ToInt32(lblActivitySchD.Text) > 0)
                    //{
                    //    foreach (DataRow dr in dtMachine.Select("ACTIVITY_FID=" + Convert.ToInt32(lblActivitySchD.Text) + ""))
                    //    {
                    //        DataRow drn = dtMachineNew.NewRow();
                    //        drn["ACTIVITY_FID"] = dr["ACTIVITY_FID"];
                    //        drn["MACHINE_FID"] = dr["MACHINE_FID"];
                    //        drn["MACHINE_NAME"] = dr["MACHINE_NAME"];

                    //        dtMachineNew.Rows.Add(drn);
                    //    }

                    //    if (dtMachineNew.Rows.Count > 0)
                    //    {
                    //        ddlMachineSchD.DataSource = dtMachineNew;
                    //        ddlMachineSchD.DataTextField = "MACHINE_NAME";
                    //        ddlMachineSchD.DataValueField = "MACHINE_FID";
                    //        ddlMachineSchD.DataBind();
                    //        ddlMachineSchD.Items.Insert(0, "Select");


                    //        if (!string.IsNullOrEmpty(Convert.ToString(lblMachineID.Text)) && Convert.ToInt32(lblMachineID.Text) > 0)
                    //            ddlMachineSchD.SelectedValue = Convert.ToString(lblMachineID.Text);
                    //        else ddlMachineSchD.SelectedIndex = 0;
                    //    }
                    //}





                    //gvAvailableDatesList.DataSource = null;
                    //gvAvailableDatesList.DataBind();

                    //mpeScheduleDates.Show();
                    //mpeScheduleMachine.Show();

                    #endregion
                }

                else if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    DataTable dt = (DataTable)Session["dtMachineSchedulingList"];
                    RemoveRecords(dt, Convert.ToInt32(txtSrNoToMSchInList.Text));
                    mpeScheduleMachine.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "SCHEDULE")
                {
                    hdScheduledFromSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtScheduledFromSchD.Text = hdScheduledFromSchD.Value;

                    hdScheduledToSchD.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtScheduledToSchD.Text = hdScheduledToSchD.Value;

                    gvAvailableDatesList.DataSource = null;
                    gvAvailableDatesList.DataBind();

                    //lblAlreadyScheduledDatesRecords.Text = "Already Scheduled Dates[" + gvAlreadyScheduledDatesList.Rows.Count + "]";
                    lblAvailableDatesRecords.Text = "Available Dates[" + gvAvailableDatesList.Rows.Count + "]";

                    mpeScheduleDates.Show();
                    mpeScheduleMachine.Show();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvJOBDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                mpeUpdateDetails.Show();
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblLOTTFIDInList = gvJOBDetail.Rows[rowindex].FindControl("lblLOTTFIDInList") as Label;
                Label lblJOBNoInList = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNoInList") as Label;
                Label lblLOTnoInList = gvJOBDetail.Rows[rowindex].FindControl("lblLOTnoInList") as Label;

                hdLOTTFID.Value = Convert.ToString(lblLOTTFIDInList.Text);
                lblLOTNo.Text = Convert.ToString(lblLOTnoInList.Text);
                txtJOBNoToA.Text = Convert.ToString(lblJOBNoInList.Text).Trim();
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

    protected void gvDrawingDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                mpeUpdateDetails.Show();
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblLOTTFSubitemID = gvDrawingDetail.Rows[rowindex].FindControl("lblLOTTFSubitemID") as Label;
                Label lblDrawingNo = gvDrawingDetail.Rows[rowindex].FindControl("lblDrawingNo") as Label;
                Label lblEquipment = gvDrawingDetail.Rows[rowindex].FindControl("lblEquipment") as Label;
                TextBox txtQuantity = gvDrawingDetail.Rows[rowindex].FindControl("txtQuantity") as TextBox;

                hdLOTTFSubitemID.Value = Convert.ToString(lblLOTTFSubitemID.Text);
                txtDrawingNoToA.Text = Convert.ToString(lblDrawingNo.Text).Trim();
                txtDrawingNoToA.ToolTip = Convert.ToString(lblDrawingNo.Text).Trim();
                txtEquipmentToA.Text = Convert.ToString(lblEquipment.Text).Trim();
                txtQuantityToA.Text = txtQuantity.Text;
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



    /// <summary>
    /// Row Databounds
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void gvSubitemsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
            Label lblUnitID = (Label)e.Row.FindControl("lblUnitID");
            Label lblAdditinoalDrawingFilePath = (Label)e.Row.FindControl("lblAdditinoalDrawingFilePath");
            Label lblLOTSIDrawigName1 = (Label)e.Row.FindControl("lblLOTSIDrawigName1");
            Label lblLOTSIDrawigName2 = (Label)e.Row.FindControl("lblLOTSIDrawigName2");
            Label lblScheduledCounts = (Label)e.Row.FindControl("lblScheduledCounts");
            Label lblBalancedCounts = (Label)e.Row.FindControl("lblBalancedCounts");

            Label lblAllocatedQuantity = (Label)e.Row.FindControl("lblAllocatedQuantity");
            Label lblQaInspectionAcceptedQuantity = (Label)e.Row.FindControl("lblQaInspectionAcceptedQuantity");
            Label lblQaInspectionRejectedQuantity = (Label)e.Row.FindControl("lblQaInspectionRejectedQuantity");

            Label lblAssignedBy = (Label)e.Row.FindControl("lblAssignedBy");
            Label lblAcceptedBy = (Label)e.Row.FindControl("lblAcceptedBy");
            Label lblRejectedBy = (Label)e.Row.FindControl("lblRejectedBy");
            Label lblScheduledBy = (Label)e.Row.FindControl("lblScheduledBy");

            Label lblIsAssignedMailSent = (Label)e.Row.FindControl("lblIsAssignedMailSent");
            Label lblIsAcceptedMailSent = (Label)e.Row.FindControl("lblIsAcceptedMailSent");
            Label lblIsRejectedMailSent = (Label)e.Row.FindControl("lblIsRejectedMailSent");
            Label lblIsScheduledMailSent = (Label)e.Row.FindControl("lblIsScheduledMailSent");


            ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
            ImageButton imgBtnModify = (ImageButton)e.Row.FindControl("imgBtnModify");
            ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
            ImageButton imgBtnViewDetails = (ImageButton)e.Row.FindControl("imgBtnViewDetails");
            ImageButton imgBtnViewAdditionalDrawing = (ImageButton)e.Row.FindControl("imgBtnViewAdditionalDrawing");
            ImageButton imgBtnViewDrawing1 = (ImageButton)e.Row.FindControl("imgBtnViewDrawing1");
            ImageButton imgBtnViewDrawing2 = (ImageButton)e.Row.FindControl("imgBtnViewDrawing2");

            Button btnAcceptDrawings = (Button)e.Row.FindControl("btnAcceptDrawings");
            Button btnSchedule = (Button)e.Row.FindControl("btnSchedule");
            Button btnComplete = (Button)e.Row.FindControl("btnComplete");
            Button btnInspect = (Button)e.Row.FindControl("btnInspect");


            imgBtnEdit.Visible = false;
            imgBtnModify.Visible = false;
            imgBtnViewDetails.Visible = false;
            imgBtnViewAdditionalDrawing.Visible = false;
            imgBtnViewDrawing1.Visible = false;
            imgBtnViewDrawing2.Visible = false;

            btnAcceptDrawings.Visible = false;
            btnSchedule.Visible = false;
            btnComplete.Visible = false;
            btnInspect.Visible = false;


            if (Convert.ToInt32(lblScheduledCounts.Text) > 0)
            {
                imgBtnViewDetails.Visible = true;
            }

            if (!string.IsNullOrEmpty(lblAdditinoalDrawingFilePath.Text))
            {
                imgBtnViewAdditionalDrawing.Visible = true;
                imgBtnViewAdditionalDrawing.ToolTip = lblAdditinoalDrawingFilePath.Text;
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

            DataTable dtShopInhargeList = new DataTable();
            DataTable dtQualityTeamList = new DataTable();

            if (Session["dtShopInhargeList"] != null)
                dtShopInhargeList = (DataTable)Session["dtShopInhargeList"];

            if (Session["dtQualityTeamList"] != null)
                dtQualityTeamList = (DataTable)Session["dtQualityTeamList"];



            if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Assigned) ||
                Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Re_Assigned))
            {
                if (Convert.ToInt32(lblAssignedBy.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                {
                    imgBtnEdit.Visible = true;
                }

                if (dtShopInhargeList.Rows.Count > 0 && dtShopInhargeList != null)
                {
                    foreach (DataRow dr in dtShopInhargeList.Select("MACHINE_SHOP_INC_FID=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ""))
                    {
                        btnAcceptDrawings.Visible = true;
                    }
                }

                if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Assigned))
                {
                    imgBtnStatus.ImageUrl = "~/Images/LOT/edit3.png";
                    imgBtnStatus.ToolTip = "Assigned";
                }
                else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Re_Assigned))
                {
                    imgBtnStatus.ImageUrl = "~/Images/LOT/edit2.png";
                    imgBtnStatus.ToolTip = "Re Assigned";
                }
            }

            else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Accepted))
            {
                imgBtnStatus.ImageUrl = "~/Images/Icons/yes1.png";
                imgBtnStatus.ToolTip = "Accepted";

                if (dtShopInhargeList.Rows.Count > 0 && dtShopInhargeList != null)
                {
                    foreach (DataRow dr in dtShopInhargeList.Select("MACHINE_SHOP_INC_FID=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ""))
                    {
                        btnSchedule.Visible = true;
                    }
                }
            }

            else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Rejected))
            {
                if (Convert.ToInt32(lblAssignedBy.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                {
                    imgBtnEdit.Visible = true;
                }

                imgBtnStatus.ImageUrl = "~/Images/Icons/no2.png";
                imgBtnStatus.ToolTip = "Rejected";
            }

            else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Scheduled) ||
                     Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Re_Scheduled))
            {
                imgBtnStatus.ImageUrl = "~/Images/Posting/02Posted.ico";
                imgBtnStatus.ToolTip = "Scheduled";

                if (Convert.ToInt32(lblBalancedCounts.Text) > 0)
                {
                    if (dtShopInhargeList.Rows.Count > 0 && dtShopInhargeList != null)
                    {
                        foreach (DataRow dr in dtShopInhargeList.Select("MACHINE_SHOP_INC_FID=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ""))
                        {
                            imgBtnModify.Visible = true;
                            btnComplete.Visible = true;
                        }
                    }
                }
            }

            else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Completed))
            {
                imgBtnStatus.ImageUrl = "~/Images/NEWICONS/comp2.png";
                imgBtnStatus.ToolTip = "Completed";

                if (Convert.ToInt32(lblBalancedCounts.Text) == 0)
                {
                    if (dtQualityTeamList.Rows.Count > 0 && dtQualityTeamList != null)
                    {
                        foreach (DataRow dr in dtQualityTeamList.Select("MANAGER_ID=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + " AND UNIT_FID=" + Convert.ToInt32(lblUnitID.Text)))
                        {
                            btnInspect.Visible = true;
                        }
                    }
                }
            }

            //else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.QA_Accepted))
            //{
            //    imgBtnStatus.ImageUrl = "~/Images/MS/qa_a1.png";
            //    imgBtnStatus.ToolTip = "Quality Accepted";
            //}

            //else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.QA_Rejected))
            //{
            //    imgBtnStatus.ImageUrl = "~/Images/MS/qa_r1.png";
            //    imgBtnStatus.ToolTip = "Quality Rejected";


            //    if (dtShopInhargeList.Rows.Count > 0 && dtShopInhargeList != null)
            //    {
            //        foreach (DataRow dr in dtShopInhargeList.Select("MACHINE_SHOP_INC_FID=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ""))
            //        {
            //            btnSchedule.Visible = true;
            //            btnSchedule.Text = "Re-Schedule";
            //            btnSchedule.ToolTip = "Re-Schedule machine for drawing";
            //            btnSchedule.BackColor = System.Drawing.Color.DarkGoldenrod;
            //        }
            //    }
            //}

            else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.QA_Inspected))
            {
                imgBtnStatus.ImageUrl = "~/Images/MS/qa_a1.png";
                imgBtnStatus.ToolTip = "Quality Inspected";

                if ((Convert.ToInt32(lblAllocatedQuantity.Text) - Convert.ToInt32(lblQaInspectionAcceptedQuantity.Text)) > 0)
                {
                    foreach (DataRow dr in dtShopInhargeList.Select("MACHINE_SHOP_INC_FID=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ""))
                    {
                        btnSchedule.Visible = true;
                        btnSchedule.Text = "Re-Schedule";
                        btnSchedule.ToolTip = "Re-Schedule machine for drawing";
                        btnSchedule.BackColor = System.Drawing.Color.DarkGoldenrod;
                    }
                }
            }
        }
    }

    protected void gvMachineDatesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtType = new DataTable();
            DataTable dtWorker = new DataTable();

            Label lblTypeOfWorkID = (Label)e.Row.FindControl("lblTypeOfWorkID");
            DropDownList ddlTypeOfWorkToMSchInList = (DropDownList)e.Row.FindControl("ddlTypeOfWorkToMSchInList");
            Label lblWorkerID = (Label)e.Row.FindControl("lblWorkerID");
            DropDownList ddlWorkerMSchInList = (DropDownList)e.Row.FindControl("ddlWorkerMSchInList");

            Label lblAvailableFromToMSchInList = (Label)e.Row.FindControl("lblAvailableFromToMSchInList");
            Label lblAvailableToMSchInList = (Label)e.Row.FindControl("lblAvailableToMSchInList");

            Label lblWorkingStartTimeMSchInList = (Label)e.Row.FindControl("lblWorkingStartTimeMSchInList");
            Label lblWorkingEndTimeMSchInList = (Label)e.Row.FindControl("lblWorkingEndTimeMSchInList");

            TextBox txtLatestScheduledHoursToMSchInList = (TextBox)e.Row.FindControl("txtLatestScheduledHoursToMSchInList");
            TextBox txtAvailableHoursToMSchInList = (TextBox)e.Row.FindControl("txtAvailableHoursToMSchInList");

            Label lblEndTimeFlag = (Label)e.Row.FindControl("lblEndTimeFlag");
            TextBox txtScheduledDateToMSchInList = (TextBox)e.Row.FindControl("txtScheduledDateToMSchInList");
            TextBox txtScheduledFromToMSchInList = (TextBox)e.Row.FindControl("txtScheduledFromToMSchInList");
            TextBox txtScheduledToMSchInList = (TextBox)e.Row.FindControl("txtScheduledToMSchInList");
            TextBox txtScheduledHoursToMSchInList = (TextBox)e.Row.FindControl("txtScheduledHoursToMSchInList");


            if (!string.IsNullOrEmpty(Convert.ToString(lblEndTimeFlag.Text)) && Convert.ToInt32(lblEndTimeFlag.Text) > 0)
            {
                txtScheduledDateToMSchInList.BackColor = System.Drawing.Color.Orange;
                txtScheduledFromToMSchInList.BackColor = System.Drawing.Color.Orange;
                txtScheduledToMSchInList.BackColor = System.Drawing.Color.Orange;
                txtScheduledHoursToMSchInList.BackColor = System.Drawing.Color.Orange;
            }
            else
            {
                txtScheduledDateToMSchInList.BackColor = System.Drawing.Color.LightPink;
                txtScheduledFromToMSchInList.BackColor = System.Drawing.Color.LightPink;
                txtScheduledToMSchInList.BackColor = System.Drawing.Color.LightPink;
                txtScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightPink;
            }


            DropDownList ddlAFH = (DropDownList)e.Row.FindControl("ddlAFH");
            DropDownList ddlAFM = (DropDownList)e.Row.FindControl("ddlAFM");

            DropDownList ddlATH = (DropDownList)e.Row.FindControl("ddlATH");
            DropDownList ddlATM = (DropDownList)e.Row.FindControl("ddlATM");


            int fH = 0;
            int fM = 0;

            int tH = 0;
            int tM = 0;

            fH = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[0]);
            fM = Convert.ToInt32(lblAvailableFromToMSchInList.Text.Split(':')[1]);

            tH = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[0]);
            tM = Convert.ToInt32(lblAvailableToMSchInList.Text.Split(':')[1]);


            DataTable dtH = new DataTable();
            if (dtH.Columns.Count == 0)
            {
                dtH.Columns.Add("H", typeof(string));
            }

            for (int i = fH; i <= tH; i++)
            {
                DataRow drH = dtH.NewRow();
                if (i < 10)
                    drH["H"] = "0" + Convert.ToString(i);
                else drH["H"] = Convert.ToString(i);

                dtH.Rows.Add(drH);
            }

            if (dtH.Rows.Count > 0)
            {
                ddlAFH.DataSource = dtH;
                ddlAFH.DataBind();
                ddlAFH.SelectedValue = Convert.ToString(fH);

                ddlATH.DataSource = dtH;
                ddlATH.DataBind();
                //ddlATH.SelectedValue = Convert.ToString(tH);
                ddlATH.SelectedValue = "19";
            }
            else
            {
                ddlAFH.Items.Clear();
                ddlAFH.Items.Insert(0, "00");

                ddlATH.DataSource = dtH;
                ddlATH.Items.Insert(0, "00");
            }



            DataTable dtM = new DataTable();
            if (dtM.Columns.Count == 0)
            {
                dtM.Columns.Add("M", typeof(string));
            }

            //for (int i = fM; i <= tM; i++)
            for (int i = 0; i <= 59; i++)
            {
                DataRow drM = dtM.NewRow();
                if (i < 10)
                    drM["M"] = "0" + Convert.ToString(i);
                else drM["M"] = Convert.ToString(i);

                dtM.Rows.Add(drM);
            }

            if (dtM.Rows.Count > 0)
            {
                ddlAFM.DataSource = dtM;
                ddlAFM.DataBind();
                ddlAFM.SelectedValue = Convert.ToString(fM);

                ddlATM.DataSource = dtM;
                ddlATM.DataBind();
                //ddlATM.SelectedValue = Convert.ToString(tM);
                ddlATM.SelectedValue = "30";
            }
            else
            {
                ddlAFM.Items.Clear();
                ddlAFM.Items.Insert(0, "00");

                ddlATM.DataSource = dtH;
                ddlATM.Items.Insert(0, "00");
            }




            if (Session["dtType"] != null)
                dtType = (DataTable)Session["dtType"];
            else
                dtType = GetTypeData();

            if (dtType.Rows.Count > 0)
            {
                ddlTypeOfWorkToMSchInList.DataSource = dtType;
                ddlTypeOfWorkToMSchInList.DataTextField = "TYPE_NAME";
                ddlTypeOfWorkToMSchInList.DataValueField = "TYPE_PID";
                ddlTypeOfWorkToMSchInList.DataBind();
                //ddlTypeOfWorkToMSchInList.Items.Insert(0, "Select");
                ddlTypeOfWorkToMSchInList.SelectedIndex = 0;

                if (Convert.ToInt32(lblTypeOfWorkID.Text) > 0)
                {
                    ddlTypeOfWorkToMSchInList.SelectedValue = Convert.ToString(lblTypeOfWorkID.Text);
                }
            }
            else
            {
                ddlTypeOfWorkToMSchInList.Items.Clear();
                //ddlTypeOfWorkToMSchInList.Items.Insert(0, "Select");
                ddlTypeOfWorkToMSchInList.SelectedIndex = 0;
            }


            if (Session["dtWorker"] != null)
                dtWorker = (DataTable)Session["dtWorker"];
            else
                dtWorker = GetWorkerData();

            if (dtWorker.Rows.Count > 0)
            {
                ddlWorkerMSchInList.DataSource = dtWorker;
                ddlWorkerMSchInList.DataTextField = "EMPLOYEE_NAME";
                ddlWorkerMSchInList.DataValueField = "EMP_RECORD_ID";
                ddlWorkerMSchInList.DataBind();

                if (Convert.ToInt32(lblWorkerID.Text) > 0)
                {
                    ddlWorkerMSchInList.SelectedValue = Convert.ToString(lblWorkerID.Text);
                }
            }
            else
            {
                ddlWorkerMSchInList.Items.Clear();
                ddlWorkerMSchInList.SelectedIndex = 0;
            }


            string fromTime = Convert.ToDateTime((ddlAFH.SelectedValue) + ":" + (ddlAFM.SelectedValue)).ToString("HH:mm");
            string toTime = Convert.ToDateTime((ddlATH.SelectedValue) + ":" + (ddlATM.SelectedValue)).ToString("HH:mm");
            TimeSpan duration = DateTime.Parse(toTime).Subtract(DateTime.Parse(fromTime));

            int totalScheduledMinutes = Convert.ToInt32(duration.TotalMinutes);
            if (totalScheduledMinutes > 0)
                txtLatestScheduledHoursToMSchInList.Text = Convert.ToDateTime(totalScheduledMinutes / 60 + ":" + totalScheduledMinutes % 60).ToString("HH:mm");
            else txtLatestScheduledHoursToMSchInList.Text = "00:00";


            string availableFromTime = Convert.ToDateTime(lblAvailableFromToMSchInList.Text).ToString("HH:mm");
            string availableToTime = Convert.ToDateTime(lblAvailableToMSchInList.Text).ToString("HH:mm");
            int totalAvailableMinutes = 0;

            TimeSpan tsAvailableDuration = DateTime.Parse(availableToTime).Subtract(DateTime.Parse(availableFromTime));
            totalAvailableMinutes = Convert.ToInt32(tsAvailableDuration.TotalMinutes);
            totalAvailableMinutes = (totalAvailableMinutes - totalScheduledMinutes);

            if (totalAvailableMinutes > 0)
                txtAvailableHoursToMSchInList.Text = Convert.ToDateTime(totalAvailableMinutes / 60 + ":" + totalAvailableMinutes % 60).ToString("HH:mm");
            else txtAvailableHoursToMSchInList.Text = "00:00";

            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            chkSelect.Checked = false;
            if (totalScheduledMinutes > 0)
            {
                txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightGreen;
                chkSelect.Enabled = true;
            }
            else
            {
                txtLatestScheduledHoursToMSchInList.BackColor = System.Drawing.Color.LightPink;
                chkSelect.Enabled = false;
            }

        }
    }

    protected void gvScheduledMachineListForCompletion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToInt32(hdRowCommandFlags.Value) == Convert.ToInt32(MSAllStatusAndTypes.EnumRowCommandFlags.ViewSchedulingDetails))
            {
                e.Row.Cells[1].Visible = false;
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtWorker = new DataTable();

            Label lblWorkerID = (Label)e.Row.FindControl("lblWorkerID");
            Label lblCompletedRemarks = (Label)e.Row.FindControl("lblCompletedRemarks");
            Label lblIsCompleted = (Label)e.Row.FindControl("lblIsCompleted");

            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            DropDownList ddlWorkerMSchInList = (DropDownList)e.Row.FindControl("ddlWorkerMSchInList");
            TextBox txtRemarksToMSchInList = (TextBox)e.Row.FindControl("txtRemarksToMSchInList");
            TextBox txtCompletionRemarksToMSchInList = (TextBox)e.Row.FindControl("txtCompletionRemarksToMSchInList");

            chkSelect.Enabled = false;
            txtCompletionRemarksToMSchInList.Enabled = false;
            txtCompletionRemarksToMSchInList.Text = string.Empty;
            txtCompletionRemarksToMSchInList.BackColor = System.Drawing.Color.LightYellow;

            if (Convert.ToInt32(lblIsCompleted.Text) == 0)
            {
                chkSelect.Enabled = true;
                txtCompletionRemarksToMSchInList.Enabled = true;
                txtCompletionRemarksToMSchInList.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                txtCompletionRemarksToMSchInList.Text = Convert.ToString(lblCompletedRemarks.Text);
            }


            if (Session["dtWorker"] != null)
                dtWorker = (DataTable)Session["dtWorker"];
            else
                dtWorker = GetWorkerData();

            if (dtWorker.Rows.Count > 0)
            {
                ddlWorkerMSchInList.DataSource = dtWorker;
                ddlWorkerMSchInList.DataTextField = "EMPLOYEE_NAME";
                ddlWorkerMSchInList.DataValueField = "EMP_RECORD_ID";
                ddlWorkerMSchInList.DataBind();

                if (Convert.ToInt32(lblWorkerID.Text) > 0)
                {
                    ddlWorkerMSchInList.SelectedValue = Convert.ToString(lblWorkerID.Text);
                }
            }
            else
            {
                ddlWorkerMSchInList.Items.Clear();
                ddlWorkerMSchInList.SelectedIndex = 0;
            }

            if (Convert.ToInt32(hdRowCommandFlags.Value) == Convert.ToInt32(MSAllStatusAndTypes.EnumRowCommandFlags.ViewSchedulingDetails))
            {
                e.Row.Cells[1].Visible = false;
                ddlWorkerMSchInList.Enabled = false;
                txtCompletionRemarksToMSchInList.Enabled = false;
                txtCompletionRemarksToMSchInList.BackColor = System.Drawing.Color.LightYellow;
            }
            else if (Convert.ToInt32(hdRowCommandFlags.Value) == Convert.ToInt32(MSAllStatusAndTypes.EnumRowCommandFlags.ModifyWorker))
            {
                txtRemarksToMSchInList.Text = String.Empty;
                txtRemarksToMSchInList.Enabled = true;
                txtRemarksToMSchInList.BackColor = System.Drawing.Color.LightGreen;

                txtCompletionRemarksToMSchInList.Enabled = false;
                txtCompletionRemarksToMSchInList.BackColor = System.Drawing.Color.LightYellow;
            }
            else if (Convert.ToInt32(hdRowCommandFlags.Value) == Convert.ToInt32(MSAllStatusAndTypes.EnumRowCommandFlags.CompleteScheduling))
            {
                ddlWorkerMSchInList.Enabled = false;
            }
        }
    }

    #endregion


    #region METHODS[=======================]

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

    /// <summary>
    /// Get Data
    /// </summary>

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


                ddlCompanyToA.DataSource = dsUnit.Tables[0];
                ddlCompanyToA.DataTextField = "UNIT_NAME";
                ddlCompanyToA.DataValueField = "UNIT_ID";
                ddlCompanyToA.DataBind();
                ddlCompanyToA.SelectedIndex = 0;
            }
            else
            {
                ddlCompany.Items.Clear();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;

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

    private void BindStatus()
    {
        try
        {
            dsStatus = objMS.GetStatusList();
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_PID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;
            }
            else
            {
                ddlStatus.Items.Clear();
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

    private void GetAssignedItemsList()
    {
        try
        {
            companyID = 0;
            LOTNo = string.Empty;
            jobNo = string.Empty;
            drawingNo = string.Empty;
            equipment = string.Empty;
            statusId = 0;

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

            if (ddlStatus.SelectedIndex > 0)
                statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            dsAssignedItemsList = objMS.GetAssignedItemsList(companyID, LOTNo, jobNo, drawingNo, equipment, statusId);
            if (dsAssignedItemsList.Tables.Count > 0 && dsAssignedItemsList.Tables[0].Rows.Count > 0)
            {
                if (dsAssignedItemsList.Tables[1].Rows.Count > 0)
                {
                    Session["dtShopInhargeList"] = dsAssignedItemsList.Tables[1];
                }

                if (dsAssignedItemsList.Tables[2].Rows.Count > 0)
                {
                    Session["dtQualityTeamList"] = dsAssignedItemsList.Tables[2];
                }

                gvSubitemsList.DataSource = dsAssignedItemsList.Tables[0];
                gvSubitemsList.DataBind();


            }
            else
            {
                gvSubitemsList.DataSource = null;
                gvSubitemsList.DataBind();
                Session["dtShopInhargeList"] = null;
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


    //JOB Details Start------------------------
    private DataSet GetJOBData()
    {
        try
        {
            companyID = 0;
            LOTNo = string.Empty;
            jobNo = string.Empty;

            companyID = Convert.ToInt32(ddlCompanyToA.SelectedValue);

            if (!string.IsNullOrEmpty(txtLOTNoSearch.Text))
                LOTNo = txtLOTNoSearch.Text;


            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;


            dsJobNo = objMS.GetJOBDetailsForLMS(companyID, jobNo, LOTNo, 0);

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



    private void SaveItemsDetails()
    {
        try
        {
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

            recordID = 0;
            assignedDrawingCode = string.Empty;
            lotTFID = 0;
            lotTFSubitemID = 0;
            unitID = 0;
            categoryID = 0;
            jobNo = string.Empty;
            drawingNo = string.Empty;
            equipmentNo = string.Empty;
            tagNo = string.Empty;
            itemName = string.Empty;
            itemDetail = string.Empty;
            expectedDateOfCompByPlanning = string.Empty;
            quantity = 0;
            remarks = string.Empty;
            additionalDrawingFileName = string.Empty;
            additionalDrawingBytes = null;


            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            unitID = Convert.ToInt32(ddlCompanyToA.SelectedValue);
            assignedDrawingCode = Convert.ToString(ViewState["ASSIGNED_DRAWING_CODE"]);

            if (ddlCategoryToA.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategoryToA.SelectedValue);
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
                lotTFSubitemID = Convert.ToInt32(hdLOTTFSubitemID.Value);
            }
            //}

            quantity = Convert.ToInt32(txtQuantityToA.Text);

            if (!string.IsNullOrEmpty(txtItemNameToA.Text))
                itemName = txtItemNameToA.Text.Trim();

            if (!string.IsNullOrEmpty(txtItemDetailToA.Text))
                itemDetail = txtItemDetailToA.Text.Trim();

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

            DataRow drn = dtSubitems.NewRow();
            drn["ASSIGNED_DRAWING_CODE"] = assignedDrawingCode;
            drn["LOT_TF_ID"] = lotTFID;
            drn["LOT_TF_SUBITEM_ID"] = lotTFSubitemID;
            drn["UNIT_ID"] = unitID;
            drn["JOB_NO"] = jobNo;
            drn["DRAWING_NO"] = drawingNo;
            drn["EQUIPMENT"] = equipmentNo;
            drn["TAG_NO"] = tagNo;
            drn["ITEM_NAME"] = itemName;
            drn["ITEM_DETAIL"] = itemDetail;
            drn["EXPECTED_DATE_OF_COMP_BY_PLANNING"] = expectedDateOfCompByPlanning;
            drn["ALLOCATED_QUANTITY"] = quantity;
            drn["REMARKS"] = remarks;
            drn["ADD_DRAWING_FILE_NAME"] = additionalDrawingFileName;
            drn["ADD_DRAWING_FILE_BYTES"] = additionalDrawingBytes;
            drn["CATEGORY_ID"] = categoryID;

            dtSubitems.Rows.Add(drn);



            if (dtSubitems.Rows.Count > 0)
            {
                int value = 0;
                value = objMS.InsertAssignedItemsForMachineSch(recordID, dtSubitems, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (value > 0)
                {
                    int sendMailValue = SendMail(recordID);
                    if (sendMailValue > 0)
                    {
                        SuccessMessage(dtSubitems.Rows.Count + " Records updated and mail sent successfully...!!!");
                    }
                    else
                    {
                        SuccessMessage(dtSubitems.Rows.Count + " Records updated successfully...!!!");
                    }

                    Reset();
                    GetAssignedItemsList();
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

    private int SendMail(int recordID)
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
            drns["RECORD_ID"] = recordID;
            drns["SR_NO"] = 1;
            drns["UNIT_ID"] = unitID;
            drns["JOB_NO"] = jobNo;
            drns["DRAWING_NO"] = drawingNo;
            dtUpdateMailStatus.Rows.Add(drns);


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


    private void UpdateStatus(int statusID)
    {
        try
        {
            recordID = 0;
            remarks = string.Empty;
            drawingNo = string.Empty;
            assignedDrawingCode = string.Empty;

            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            drawingNo = Convert.ToString(ViewState["DRAWING_NO"]);
            assignedDrawingCode = Convert.ToString(ViewState["ASSIGNED_DRAWING_CODE"]);

            if (!string.IsNullOrEmpty(txtAcceptedOrRejectedRemarksToUst.Text))
                remarks = Convert.ToString(txtAcceptedOrRejectedRemarksToUst.Text);

            int value = objMS.UpdateStatus(recordID, statusID, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                int mailTypeID = 0;

                if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Accepted))
                    mailTypeID = Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.AcceptedMail);

                else if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Rejected))
                    mailTypeID = Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.RejectedMail);

                else if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Scheduled))
                    mailTypeID = Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.ScheduledMail);


                int sendMailValue = objMSSendMail.ProcessAndSendAcceptanceMail(recordID, assignedDrawingCode, mailTypeID, drawingNo, remarks);

                if (sendMailValue > 0)
                {
                    objMS.UpdateAcceptanceMailStatus(recordID, statusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Accepted))
                        SuccessMessage("Assigned drawing accepted for machine scheduling and mail sent successfully...!!!");

                    else if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Rejected))
                        SuccessMessage("Assigned drawing rejected and mail sent successfully...!!!");

                    else if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Scheduled))
                        SuccessMessage("Machines scheduled for assigned drawing and mail sent successfully...!!!");

                }
                else
                {
                    if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Accepted))
                        SuccessMessage("Assigned drawing accepted for machine scheduling successfully...!!!");

                    else if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Rejected))
                        SuccessMessage("Assigned drawing rejected successfully...!!!");

                    else if (statusID == Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Scheduled))
                        SuccessMessage("Machines scheduled for assigned drawing successfully...!!!");
                }

                GetAssignedItemsList();
                return;
            }
        }
        catch (Exception ex)
        {
            GetAssignedItemsList();
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void QualityInspection()
    {
        try
        {
            recordID = 0;
            drawingNo = string.Empty;
            assignedDrawingCode = string.Empty;

            int acceptedQuantity = 0;
            int rejectedQuantity = 0;

            string acceptedRemarks = string.Empty;
            string rejectedRemarks = string.Empty;

            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            drawingNo = Convert.ToString(ViewState["DRAWING_NO"]);
            assignedDrawingCode = Convert.ToString(ViewState["ASSIGNED_DRAWING_CODE"]);

            if (!string.IsNullOrEmpty(txtAcceptedQuantityToQaInsp.Text) && Convert.ToInt32(txtAcceptedQuantityToQaInsp.Text) > 0)
                acceptedQuantity = Convert.ToInt32(txtAcceptedQuantityToQaInsp.Text);

            if (!string.IsNullOrEmpty(txtRejectedQuantityToQaInsp.Text) && Convert.ToInt32(txtRejectedQuantityToQaInsp.Text) > 0)
                rejectedQuantity = Convert.ToInt32(txtRejectedQuantityToQaInsp.Text);

            if (!string.IsNullOrEmpty(txtInspectedAcceptedRemarksToQaInsp.Text))
                acceptedRemarks = Convert.ToString(txtInspectedAcceptedRemarksToQaInsp.Text);

            if (!string.IsNullOrEmpty(txtInspectedRejectedRemarksToQaInsp.Text))
                rejectedRemarks = Convert.ToString(txtInspectedRejectedRemarksToQaInsp.Text);



            int value = objMS.QualityInspection(recordID, acceptedQuantity, rejectedQuantity, acceptedRemarks, rejectedRemarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                int mailTypeID = 0;

                int acceptedSendMailValue = 0;
                int rejectedSendMailValue = 0;

                if (acceptedQuantity > 0)
                {
                    mailTypeID = Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.QA_AcceptedMail);
                    acceptedSendMailValue = objMSSendMail.ProcessAndSendAcceptanceMail(recordID, assignedDrawingCode, mailTypeID, drawingNo, remarks);
                }

                if (rejectedQuantity > 0)
                {
                    mailTypeID = Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.QA_RejectedMail);
                    rejectedSendMailValue = objMSSendMail.ProcessAndSendAcceptanceMail(recordID, assignedDrawingCode, mailTypeID, drawingNo, remarks);
                }


                if (acceptedSendMailValue > 0 && rejectedSendMailValue > 0)
                    SuccessMessage("Assigned drawing inspected and mail sent successfully as accepted quantity " + acceptedQuantity + " and rejected quantity " + rejectedQuantity + "...!!!");
                else if (acceptedSendMailValue > 0 && rejectedSendMailValue == 0)
                    SuccessMessage("Assigned drawing inspected and mail sent successfully as accepted quantity " + acceptedQuantity + "...!!!");
                else if (acceptedSendMailValue == 0 && rejectedSendMailValue > 0)
                    SuccessMessage("Assigned drawing inspected and mail sent successfully as rejected quantity " + rejectedQuantity + "...!!!");
                else
                    SuccessMessage("Assigned drawing inspected successfully...!!!");

                GetAssignedItemsList();
                return;
            }
        }
        catch (Exception ex)
        {
            GetAssignedItemsList();
            ExceptionMessage(ex.ToString());
            return;
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

    private void BindTypeDataForCompletion()
    {
        try
        {
            DataTable dtType = new DataTable();

            if (Session["dtType"] != null)
                dtType = (DataTable)Session["dtType"];
            else
                dtType = GetTypeData();

            ddlTypeOfWorkToCMSch.Items.Clear();
            ddlTypeOfWorkToCMSch.Items.Insert(0, "All");
            ddlTypeOfWorkToCMSch.SelectedIndex = 0;

            if (dtType.Rows.Count > 0)
            {
                ddlTypeOfWorkToCMSch.DataSource = dtType;
                ddlTypeOfWorkToCMSch.DataTextField = "TYPE_NAME";
                ddlTypeOfWorkToCMSch.DataValueField = "TYPE_PID";
                ddlTypeOfWorkToCMSch.DataBind();
                ddlTypeOfWorkToCMSch.Items.Insert(0, "All");
                ddlTypeOfWorkToCMSch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    private DataTable GetWorkerData()
    {
        try
        {
            dsWorker = objMS.GetWorkers();
            if (dsWorker.Tables.Count > 0 && dsWorker.Tables[0].Rows.Count > 0)
            {
                Session["dtWorker"] = dsWorker.Tables[0];
                return dsWorker.Tables[0];
            }
            else
            {
                Session["dtWorker"] = null;
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void BindWorkerForCompletion()
    {
        try
        {
            DataTable dtWorker = new DataTable();

            if (Session["dtWorker"] != null)
                dtWorker = (DataTable)Session["dtWorker"];
            else
                dtWorker = GetWorkerData();

            ddlWorkerToCMSch.Items.Clear();
            ddlWorkerToCMSch.Items.Insert(0, "All");
            ddlWorkerToCMSch.SelectedIndex = 0;

            if (dtWorker.Rows.Count > 0)
            {
                ddlWorkerToCMSch.DataSource = dtWorker;
                ddlWorkerToCMSch.DataTextField = "EMPLOYEE_NAME";
                ddlWorkerToCMSch.DataValueField = "EMP_RECORD_ID";
                ddlWorkerToCMSch.DataBind();
                ddlWorkerToCMSch.Items.Insert(0, "All");
                ddlWorkerToCMSch.SelectedIndex = 0;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            //
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

    private void BindActivityData()
    {
        try
        {
            DataTable dtActivity = new DataTable();

            if (Session["dtActivity"] != null)
                dtActivity = (DataTable)Session["dtActivity"];
            else
                dtActivity = GetActivityData();

            ddlActivityToMSch.Items.Clear();
            ddlActivityToMSch.Items.Insert(0, "Select");
            ddlActivityToMSch.SelectedIndex = 0;

            if (dtActivity.Rows.Count > 0)
            {
                ddlActivityToMSch.DataSource = dtActivity;
                ddlActivityToMSch.DataTextField = "ACTIVITY_NAME";
                ddlActivityToMSch.DataValueField = "ACTIVITY_PID";
                ddlActivityToMSch.DataBind();
                ddlActivityToMSch.Items.Insert(0, "Select");
                ddlActivityToMSch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindActivityDataForCompletion()
    {
        try
        {
            DataTable dtActivity = new DataTable();

            if (Session["dtActivity"] != null)
                dtActivity = (DataTable)Session["dtActivity"];
            else
                dtActivity = GetActivityData();

            ddlActivityToCMSch.Items.Clear();
            ddlActivityToCMSch.Items.Insert(0, "All");
            ddlActivityToCMSch.SelectedIndex = 0;

            if (dtActivity.Rows.Count > 0)
            {
                ddlActivityToCMSch.DataSource = dtActivity;
                ddlActivityToCMSch.DataTextField = "ACTIVITY_NAME";
                ddlActivityToCMSch.DataValueField = "ACTIVITY_PID";
                ddlActivityToCMSch.DataBind();
                ddlActivityToCMSch.Items.Insert(0, "All");
                ddlActivityToCMSch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataTable GetMachinesData()
    {
        try
        {
            dsMachine = objMS.GetMachineActivity(0);
            if (dsMachine.Tables.Count > 0 && dsMachine.Tables[0].Rows.Count > 0)
            {
                Session["dtMachine"] = dsMachine.Tables[0];
                return dsMachine.Tables[0];
            }
            else
            {
                Session["dtMachine"] = null;
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private DataTable CreateTempSchedulingTable()
    {
        try
        {
            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("SR_NO", typeof(int));
                dt.Columns.Add("TYPE_OF_WORK_ID", typeof(int));
                dt.Columns.Add("TYPE_OF_WORK", typeof(string));

                dt.Columns.Add("ACTIVITY_ID", typeof(int));
                dt.Columns.Add("ACTIVITY_NAME", typeof(string));

                dt.Columns.Add("MACHINE_ID", typeof(int));
                dt.Columns.Add("MACHINE_NAME", typeof(string));

                dt.Columns.Add("EMP_RECORD_ID", typeof(int));
                dt.Columns.Add("EMPLOYEE_NAME", typeof(string));

                dt.Columns.Add("DATE", typeof(string));
                dt.Columns.Add("SCHEDULED_FROM", typeof(string));
                dt.Columns.Add("SCHEDULED_TO", typeof(string));
                dt.Columns.Add("SCHEDULED_HOURS", typeof(string));

                dt.Columns.Add("AVAILABLE_FROM", typeof(string));
                dt.Columns.Add("AVAILABLE_TO", typeof(string));
                dt.Columns.Add("WORKING_START_TIME", typeof(string));
                dt.Columns.Add("WORKING_END_TIME", typeof(string));
                dt.Columns.Add("AVAILABLE_HOURS", typeof(string));

                dt.Columns.Add("REMARKS", typeof(string));
                dt.Columns.Add("END_TIME_FLAG", typeof(int));

            }

            return dt;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void RemoveRecords(DataTable dtTextChanged, int serialNo)
    {
        if (dtTextChanged.Rows.Count > 0)
        {
            foreach (DataRow drremove in dtTextChanged.Select("SR_NO='" + serialNo + "'"))
            {
                dtTextChanged.Rows.Remove(drremove);
            }

            if (dtTextChanged.Rows.Count > 0)
            {
                dtTextChanged.DefaultView.Sort = "SCHEDULED_FROM";
                dtTextChanged.DefaultView.Sort = "DATE";
                dtTextChanged.DefaultView.Sort = "MACHINE_NAME";
                dtTextChanged.DefaultView.Sort = "ACTIVITY_NAME";

                dtTextChanged = dtTextChanged.DefaultView.ToTable();


                int c = 0;
                foreach (DataRow dr in dtTextChanged.Rows)
                {
                    c++;
                    dr["SR_NO"] = c;
                }

                Session["dtMachineSchedulingList"] = dtTextChanged;
                gvMachineSchedulingList.DataSource = dtTextChanged;
                gvMachineSchedulingList.DataBind();
            }
            else
            {
                Session["dtMachineSchedulingList"] = null;
                gvMachineSchedulingList.DataSource = null;
                gvMachineSchedulingList.DataBind();
            }
        }
        else
        {
            Session["dtMachineSchedulingList"] = null;
        }
        lblScheduleMachineRecords.Text = "Records[" + gvMachineSchedulingList.Rows.Count + "]";
    }


    private void ChekAvailibilityOfDatesNew()
    {
        try
        {
            DataTable dtMachine = new DataTable();
            if (Session["dtMachine"] != null)
                dtMachine = (DataTable)Session["dtMachine"];
            else
                dtMachine = GetMachinesData();

            int count = 0;
            string currentDate = string.Empty;
            string fromDate = string.Empty;
            string toDate = string.Empty;

            int activityID = 0;
            string activityName = string.Empty;
            int machineID = 0;
            string machineName = string.Empty;

            string machineIDs = string.Empty;

            fromDate = Convert.ToDateTime(txtScheduledFromSchD.Text).ToString("yyyy-MM-dd");
            toDate = Convert.ToDateTime(txtScheduledToSchD.Text).ToString("yyyy-MM-dd");

            if (Convert.ToInt32(lblActivitySchD.Text) > 0)
                activityID = Convert.ToInt32(lblActivitySchD.Text);

            if (!string.IsNullOrEmpty(txtActivitySchD.Text))
                activityName = txtActivitySchD.Text;

            if (ddlMachineSchD.SelectedIndex > 0)
            {
                machineID = Convert.ToInt32(ddlMachineSchD.SelectedValue);
                machineName = Convert.ToString(ddlMachineSchD.SelectedItem.Text);
                machineIDs = Convert.ToString(ddlMachineSchD.SelectedValue);
            }
            else
            {
                //foreach (DataRow dr in dtMachine.Select("ACTIVITY_FID=" + Convert.ToInt32(ddlActivityToMSch.SelectedValue) + ""))
                //{
                //    machineIDs += "," + Convert.ToString(dr["MACHINE_FID"]);
                //}

                foreach (DataRow dr in dtMachine.Select("ACTIVITY_FID=" + Convert.ToInt32(lblActivitySchD.Text) + ""))
                {
                    machineIDs += "," + Convert.ToString(dr["MACHINE_FID"]);
                }
            }

            if (!string.IsNullOrEmpty(machineIDs))
                machineIDs = machineIDs.TrimStart(',');


            DataTable dtDates = new DataTable();
            if (dtDates.Columns.Count == 0)
            {
                dtDates.Columns.Add("SR_NO", typeof(int));
                dtDates.Columns.Add("DATE", typeof(string));
            }

            int days = Convert.ToInt32((Convert.ToDateTime(toDate) - Convert.ToDateTime(fromDate)).TotalDays) + 1;
            for (int i = 0; i < days; i++)
            {
                currentDate = Convert.ToDateTime(fromDate).AddDays(i).ToString("dd-MMM-yyyy");
                DataRow drd = dtDates.NewRow();
                drd["SR_NO"] = i;
                drd["DATE"] = currentDate;
                dtDates.Rows.Add(drd);
            }

            DataTable dtDetails = new DataTable();
            DataTable dtTemp = new DataTable();
            DataTable dtTempNew = new DataTable();
            DataTable dtTempNew1 = new DataTable();

            dtTempNew = CreateTempSchedulingTable();
            dtTempNew.Rows.Clear();

            if (Session["dtMachineSchedulingList"] != null)
            {
                dtTemp = (DataTable)Session["dtMachineSchedulingList"];
            }



            for (int i = 0; i < days; i++)
            {
                if (ddlMachineSchD.SelectedIndex > 0)
                {
                    DataRow drtn = dtTempNew.NewRow();

                    drtn["SR_NO"] = i + 1;
                    drtn["TYPE_OF_WORK_ID"] = 0;
                    drtn["TYPE_OF_WORK"] = string.Empty;
                    drtn["ACTIVITY_ID"] = activityID;
                    drtn["ACTIVITY_NAME"] = activityName;
                    drtn["MACHINE_ID"] = machineID;
                    drtn["MACHINE_NAME"] = machineName;

                    drtn["EMP_RECORD_ID"] = 0;
                    drtn["EMPLOYEE_NAME"] = string.Empty;

                    drtn["DATE"] = Convert.ToDateTime(fromDate).AddDays(i).ToString("dd-MMM-yyyy");
                    drtn["SCHEDULED_FROM"] = "00:00";
                    drtn["SCHEDULED_TO"] = "00:00";
                    drtn["SCHEDULED_HOURS"] = "00:00";
                    drtn["AVAILABLE_FROM"] = "09:00";
                    drtn["AVAILABLE_TO"] = "18:00";
                    drtn["WORKING_START_TIME"] = "09:00";
                    drtn["WORKING_END_TIME"] = "18:00";
                    drtn["AVAILABLE_HOURS"] = "09:00";
                    drtn["REMARKS"] = string.Empty;
                    //drtn["END_TIME_FLAG"] = 0;

                    dtTempNew.Rows.Add(drtn);
                }
                else
                {
                    foreach (DataRow dr in dtMachine.Select("ACTIVITY_FID=" + activityID + ""))
                    {
                        DataRow drtn = dtTempNew.NewRow();

                        drtn["SR_NO"] = i + 1;
                        drtn["TYPE_OF_WORK_ID"] = 0;
                        drtn["TYPE_OF_WORK"] = string.Empty;
                        drtn["ACTIVITY_ID"] = activityID;
                        drtn["ACTIVITY_NAME"] = activityName;
                        drtn["MACHINE_ID"] = Convert.ToInt32(dr["MACHINE_FID"]);
                        drtn["MACHINE_NAME"] = Convert.ToString(dr["MACHINE_NAME"]);

                        drtn["EMP_RECORD_ID"] = 0;
                        drtn["EMPLOYEE_NAME"] = string.Empty;

                        drtn["DATE"] = Convert.ToDateTime(fromDate).AddDays(i).ToString("dd-MMM-yyyy");
                        drtn["SCHEDULED_FROM"] = "00:00";
                        drtn["SCHEDULED_TO"] = "00:00";
                        drtn["SCHEDULED_HOURS"] = "00:00";
                        drtn["AVAILABLE_FROM"] = "09:00";
                        drtn["AVAILABLE_TO"] = "18:00";
                        drtn["WORKING_START_TIME"] = "09:00";
                        drtn["WORKING_END_TIME"] = "18:00";
                        drtn["AVAILABLE_HOURS"] = "09:00";
                        drtn["REMARKS"] = string.Empty;
                        //drtn["END_TIME_FLAG"] = 0;

                        dtTempNew.Rows.Add(drtn);
                    }
                }
            }


            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                dtTempNew1 = dtTempNew.Copy();
                dtTempNew1.Rows.Clear();

                foreach (DataRow drtd in dtDates.Rows)
                {
                    currentDate = Convert.ToString(drtd["DATE"]);
                    foreach (DataRow drt in dtTempNew.Select("DATE='" + currentDate + "'"))
                    {
                        machineID = Convert.ToInt32(drt["MACHINE_ID"]);
                        int c = 0;
                        foreach (DataRow drtn in dtTemp.Select("DATE='" + currentDate + "' AND MACHINE_ID=" + machineID + ""))
                        {
                            c++;
                        }

                        if (c > 0)
                        {
                            foreach (DataRow drtn in dtTemp.Select("DATE='" + currentDate + "' AND MACHINE_ID=" + machineID + ""))
                            {
                                DataRow drtn2 = dtTempNew1.NewRow();

                                drtn2["SR_NO"] = drtn["SR_NO"];
                                drtn2["TYPE_OF_WORK_ID"] = drtn["TYPE_OF_WORK_ID"];
                                drtn2["TYPE_OF_WORK"] = drtn["TYPE_OF_WORK"];
                                drtn2["ACTIVITY_ID"] = activityID;
                                drtn2["ACTIVITY_NAME"] = activityName;
                                drtn2["MACHINE_ID"] = drtn["MACHINE_ID"];
                                drtn2["MACHINE_NAME"] = drtn["MACHINE_NAME"];

                                drtn2["EMP_RECORD_ID"] = drtn["EMP_RECORD_ID"];
                                drtn2["EMPLOYEE_NAME"] = drtn["EMPLOYEE_NAME"];

                                drtn2["DATE"] = drtn["DATE"];
                                drtn2["SCHEDULED_FROM"] = drtn["SCHEDULED_FROM"];
                                drtn2["SCHEDULED_TO"] = drtn["SCHEDULED_TO"];
                                drtn2["SCHEDULED_HOURS"] = drtn["SCHEDULED_HOURS"];
                                drtn2["AVAILABLE_FROM"] = drtn["AVAILABLE_FROM"];
                                drtn2["AVAILABLE_TO"] = drtn["AVAILABLE_TO"];
                                drtn2["WORKING_START_TIME"] = drtn["WORKING_START_TIME"];
                                drtn2["WORKING_END_TIME"] = drtn["WORKING_END_TIME"];
                                drtn2["AVAILABLE_HOURS"] = drtn["AVAILABLE_HOURS"];
                                drtn2["REMARKS"] = drtn["REMARKS"];
                                //drtn2["END_TIME_FLAG"] = drtn["END_TIME_FLAG"];

                                dtTempNew1.Rows.Add(drtn2);
                            }
                        }
                        else
                        {
                            foreach (DataRow drtn in dtTempNew.Select("DATE='" + currentDate + "' AND MACHINE_ID=" + machineID + ""))
                            {
                                DataRow drtn2 = dtTempNew1.NewRow();

                                drtn2["SR_NO"] = drtn["SR_NO"];
                                drtn2["TYPE_OF_WORK_ID"] = drtn["TYPE_OF_WORK_ID"];
                                drtn2["TYPE_OF_WORK"] = drtn["TYPE_OF_WORK"];
                                drtn2["ACTIVITY_ID"] = drtn["ACTIVITY_ID"];
                                drtn2["ACTIVITY_NAME"] = drtn["ACTIVITY_NAME"];
                                drtn2["MACHINE_ID"] = drtn["MACHINE_ID"];
                                drtn2["MACHINE_NAME"] = drtn["MACHINE_NAME"];

                                drtn2["EMP_RECORD_ID"] = drtn["EMP_RECORD_ID"];
                                drtn2["EMPLOYEE_NAME"] = drtn["EMPLOYEE_NAME"];

                                drtn2["DATE"] = drtn["DATE"];
                                drtn2["SCHEDULED_FROM"] = drtn["SCHEDULED_FROM"];
                                drtn2["SCHEDULED_TO"] = drtn["SCHEDULED_TO"];
                                drtn2["SCHEDULED_HOURS"] = drtn["SCHEDULED_HOURS"];
                                drtn2["AVAILABLE_FROM"] = drtn["AVAILABLE_FROM"];
                                drtn2["AVAILABLE_TO"] = drtn["AVAILABLE_TO"];
                                drtn2["WORKING_START_TIME"] = drtn["WORKING_START_TIME"];
                                drtn2["WORKING_END_TIME"] = drtn["WORKING_END_TIME"];
                                drtn2["AVAILABLE_HOURS"] = drtn["AVAILABLE_HOURS"];
                                drtn2["REMARKS"] = drtn["REMARKS"];
                                //drtn2["END_TIME_FLAG"] = drtn["END_TIME_FLAG"];

                                dtTempNew1.Rows.Add(drtn2);
                            }
                        }
                    }
                }
            }
            else
            {
                dtTempNew1 = dtTempNew.Copy();
            }

            if (dtTempNew1 != null && dtTempNew1.Rows.Count > 0)
            {
                dtTempNew.Rows.Clear();
                dtTempNew = dtTempNew1;
            }


            if (dtTempNew.Rows.Count > 0)
            {
                dtTempNew.Columns.Remove("END_TIME_FLAG");

                DataSet dsScheduledDates = new DataSet();
                dsScheduledDates = objMS.GetMachineScheduledDatesList(dtTempNew, activityID, machineIDs, fromDate, toDate);

                if (dsScheduledDates.Tables.Count > 0)
                {
                    dtDetails = CreateDataAndBindToList(dsScheduledDates, machineID);
                }
            }


            gvAvailableDatesList.DataSource = null;
            gvAvailableDatesList.DataBind();

            if (dtDetails.Rows.Count > 0)
            {
                gvAvailableDatesList.DataSource = dtDetails;
                gvAvailableDatesList.DataBind();
            }
            else
            {
                ExceptionMessageScheduleDates("No available hours found for between selected dates...!!!");
            }

            lblAvailableDatesRecords.Text = "Available Hours[" + gvAvailableDatesList.Rows.Count + "]";

            mpeScheduleMachine.Show();
            mpeScheduleDates.Show();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataTable CreateDataAndBindToList(DataSet dsScheduledDates, int machineID)
    {
        try
        {
            string scheduldFrom = string.Empty;
            string scheduldTo = string.Empty;
            string scheduldHours = string.Empty;
            int activityID = 0;
            string date = string.Empty;
            int applicableFlag = 0;

            DataTable dtDetails = CreateTempSchedulingTable();
            dtDetails.Rows.Clear();

            DataTable dtPrimaryDetails = new DataTable();
            DataTable dtUniqueDates = new DataTable();

            if (dsScheduledDates != null && dsScheduledDates.Tables.Count > 0)
            {
                if (dsScheduledDates.Tables[0] != null && dsScheduledDates.Tables[0].Rows.Count > 0)
                {
                    dtPrimaryDetails = dsScheduledDates.Tables[0].Copy();

                    if (machineID > 0)
                    {
                        dtPrimaryDetails.Rows.Clear();

                        foreach (DataRow dr in dsScheduledDates.Tables[0].Select("MACHINE_ID=" + machineID + ""))
                        {
                            DataRow drn = dtPrimaryDetails.NewRow();

                            drn["SR_NO"] = Convert.ToString(dr["SR_NO"]);
                            drn["DATE"] = Convert.ToString(dr["DATE"]);
                            drn["TYPE_OF_WORK_ID"] = Convert.ToString(dr["TYPE_OF_WORK_ID"]);
                            drn["TYPE_OF_WORK"] = Convert.ToString(dr["TYPE_OF_WORK"]);
                            drn["ACTIVITY_ID"] = Convert.ToString(dr["ACTIVITY_ID"]);
                            drn["ACTIVITY_NAME"] = Convert.ToString(dr["ACTIVITY_NAME"]);
                            drn["MACHINE_ID"] = Convert.ToString(dr["MACHINE_ID"]);
                            drn["MACHINE_NAME"] = Convert.ToString(dr["MACHINE_NAME"]);

                            drn["EMP_RECORD_ID"] = Convert.ToInt32(dr["EMP_RECORD_ID"]);
                            drn["EMPLOYEE_NAME"] = Convert.ToString(dr["EMPLOYEE_NAME"]);

                            drn["SCHEDULED_FROM"] = Convert.ToString(dr["SCHEDULED_FROM"]);
                            drn["SCHEDULED_TO"] = Convert.ToString(dr["SCHEDULED_TO"]);
                            drn["SCHEDULED_HOURS"] = Convert.ToString(dr["SCHEDULED_HOURS"]);
                            drn["AVAILABLE_FROM"] = Convert.ToString(dr["AVAILABLE_FROM"]);
                            drn["AVAILABLE_TO"] = Convert.ToString(dr["AVAILABLE_TO"]);
                            drn["WORKING_START_TIME"] = Convert.ToString(dr["WORKING_START_TIME"]);
                            drn["WORKING_END_TIME"] = Convert.ToString(dr["WORKING_END_TIME"]);
                            drn["AVAILABLE_HOURS"] = Convert.ToString(dr["AVAILABLE_HOURS"]);
                            drn["REMARKS"] = Convert.ToString(dr["REMARKS"]);
                            drn["NA"] = Convert.ToString(dr["NA"]);
                            drn["END_TIME_FLAG"] = Convert.ToString(dr["END_TIME_FLAG"]);

                            dtPrimaryDetails.Rows.Add(drn);
                        }
                    }
                }

                if (dsScheduledDates.Tables[1] != null && dsScheduledDates.Tables[1].Rows.Count > 0)
                {
                    dtUniqueDates = dsScheduledDates.Tables[1].Copy();

                    if (machineID > 0)
                    {
                        dtUniqueDates.Rows.Clear();

                        foreach (DataRow dr in dsScheduledDates.Tables[1].Select("MACHINE_ID=" + machineID + ""))
                        {
                            DataRow drn = dtUniqueDates.NewRow();

                            drn["MACHINE_ID"] = Convert.ToString(dr["MACHINE_ID"]);
                            drn["DATE"] = Convert.ToString(dr["DATE"]);

                            dtUniqueDates.Rows.Add(drn);
                        }
                    }
                }
            }



            // if record with 00:00 hours and 0 applicable flag exists in table then it will not be included or removed from datatable

            if (dtPrimaryDetails != null && dtPrimaryDetails.Rows.Count > 0)
            {
                if (dtUniqueDates != null && dtUniqueDates.Rows.Count > 0)
                {
                    foreach (DataRow drp in dtPrimaryDetails.Rows)
                    {
                        scheduldFrom = Convert.ToString(drp["SCHEDULED_FROM"]);
                        scheduldTo = Convert.ToString(drp["SCHEDULED_TO"]);
                        scheduldHours = Convert.ToString(drp["SCHEDULED_HOURS"]);

                        machineID = Convert.ToInt32(drp["MACHINE_ID"]);
                        date = Convert.ToString(drp["DATE"]);
                        //applicableFlag = Convert.ToInt32(drp["APPLICABLE_FLAG"]);

                        int count = 0;
                        foreach (DataRow drs in dtUniqueDates.Select("MACHINE_ID=" + machineID + " AND DATE='" + date + "'"))
                        {
                            count++;
                        }

                        if (count > 0)
                        {
                            if (!string.IsNullOrEmpty(scheduldFrom) && scheduldFrom != "00:00" &&
                               !string.IsNullOrEmpty(scheduldTo) && scheduldTo != "00:00" &&
                               !string.IsNullOrEmpty(scheduldHours) && scheduldHours != "00:00")
                            {
                                DataRow drtn = dtDetails.NewRow();

                                drtn["SR_NO"] = drp["SR_NO"];
                                drtn["TYPE_OF_WORK_ID"] = drp["TYPE_OF_WORK_ID"];
                                drtn["TYPE_OF_WORK"] = drp["TYPE_OF_WORK"];
                                drtn["ACTIVITY_ID"] = drp["ACTIVITY_ID"];
                                drtn["ACTIVITY_NAME"] = drp["ACTIVITY_NAME"];
                                drtn["MACHINE_ID"] = drp["MACHINE_ID"];
                                drtn["MACHINE_NAME"] = drp["MACHINE_NAME"];

                                drtn["EMP_RECORD_ID"] = drp["EMP_RECORD_ID"];
                                drtn["EMPLOYEE_NAME"] = drp["EMPLOYEE_NAME"];

                                drtn["DATE"] = drp["DATE"];
                                drtn["SCHEDULED_FROM"] = drp["SCHEDULED_FROM"];
                                drtn["SCHEDULED_TO"] = drp["SCHEDULED_TO"];
                                drtn["SCHEDULED_HOURS"] = drp["SCHEDULED_HOURS"];
                                drtn["AVAILABLE_FROM"] = drp["AVAILABLE_FROM"];
                                drtn["AVAILABLE_TO"] = drp["AVAILABLE_TO"];
                                drtn["WORKING_START_TIME"] = drp["WORKING_START_TIME"];
                                drtn["WORKING_END_TIME"] = drp["WORKING_END_TIME"];
                                drtn["AVAILABLE_HOURS"] = drp["AVAILABLE_HOURS"];
                                drtn["REMARKS"] = drp["REMARKS"];
                                drtn["END_TIME_FLAG"] = drp["END_TIME_FLAG"];

                                dtDetails.Rows.Add(drtn);
                            }

                        }
                        else
                        {
                            DataRow drtn = dtDetails.NewRow();

                            drtn["SR_NO"] = drp["SR_NO"];
                            drtn["TYPE_OF_WORK_ID"] = drp["TYPE_OF_WORK_ID"];
                            drtn["TYPE_OF_WORK"] = drp["TYPE_OF_WORK"];
                            drtn["ACTIVITY_ID"] = drp["ACTIVITY_ID"];
                            drtn["ACTIVITY_NAME"] = drp["ACTIVITY_NAME"];
                            drtn["MACHINE_ID"] = drp["MACHINE_ID"];
                            drtn["MACHINE_NAME"] = drp["MACHINE_NAME"];

                            drtn["EMP_RECORD_ID"] = drp["EMP_RECORD_ID"];
                            drtn["EMPLOYEE_NAME"] = drp["EMPLOYEE_NAME"];

                            drtn["DATE"] = drp["DATE"];
                            drtn["SCHEDULED_FROM"] = drp["SCHEDULED_FROM"];
                            drtn["SCHEDULED_TO"] = drp["SCHEDULED_TO"];
                            drtn["SCHEDULED_HOURS"] = drp["SCHEDULED_HOURS"];
                            drtn["AVAILABLE_FROM"] = drp["AVAILABLE_FROM"];
                            drtn["AVAILABLE_TO"] = drp["AVAILABLE_TO"];
                            drtn["WORKING_START_TIME"] = drp["WORKING_START_TIME"];
                            drtn["WORKING_END_TIME"] = drp["WORKING_END_TIME"];
                            drtn["AVAILABLE_HOURS"] = drp["AVAILABLE_HOURS"];
                            drtn["REMARKS"] = drp["REMARKS"];
                            drtn["END_TIME_FLAG"] = drp["END_TIME_FLAG"];

                            dtDetails.Rows.Add(drtn);
                        }
                    }
                }
                else
                {
                    dtDetails = dtPrimaryDetails;
                }
            }


            if (dtDetails.Rows.Count > 0)
            {
                int c = 1;
                foreach (DataRow dr in dtDetails.Rows)
                {
                    dr["SR_NO"] = c++;
                }
            }

            return dtDetails;

            #region MyRegion

            //int singleMachineID = 0;
            //string singleMachineName = string.Empty;

            //if (ddlMachineSchD.SelectedIndex > 0)
            //{
            //    singleMachineID = Convert.ToInt32(ddlMachineSchD.SelectedValue);
            //    singleMachineName = Convert.ToString(ddlMachineSchD.SelectedItem.Text);
            //}

            //int days = Convert.ToInt32((Convert.ToDateTime(toDate) - Convert.ToDateTime(fromDate)).TotalDays) + 1;

            //DataTable dtDates = new DataTable();
            //if (dtDates.Columns.Count == 0)
            //{
            //    dtDates.Columns.Add("SR_NO", typeof(int));
            //    dtDates.Columns.Add("DATE", typeof(string));
            //}


            //for (int i = 0; i < days; i++)
            //{
            //    string currentDate = Convert.ToDateTime(fromDate).AddDays(i).ToString("yyyy-MM-dd");

            //    DataRow drd = dtDates.NewRow();
            //    drd["SR_NO"] = i;
            //    drd["DATE"] = currentDate;
            //    dtDates.Rows.Add(drd);
            //}


            //if (dsScheduledDates != null && dsScheduledDates.Tables.Count > 0)
            //{
            //    if (dsScheduledDates.Tables[0] != null && dsScheduledDates.Tables[0].Rows.Count > 0)
            //    {
            //        dtPrimaryDetails = dsScheduledDates.Tables[0];
            //    }

            //    if (dsScheduledDates.Tables[1] != null && dsScheduledDates.Tables[1].Rows.Count > 0)
            //    {
            //        dtSecondaryDates = dsScheduledDates.Tables[1];
            //    }


            //    if (dtPrimaryDetails.Rows.Count > 0)
            //    {
            //        dtTempNew = CreateScheduledTable(dtPrimaryDetails, dtDates, singleMachineID, singleMachineName,
            //                                 dtMachine, Convert.ToInt32(lblActivitySchD.Text), Convert.ToString(txtActivitySchD.Text));
            //    }
            //    else
            //    {
            //        if (dtSecondaryDates != null && dtSecondaryDates.Rows.Count > 0)
            //        {
            //            dtDates.Rows.Clear();

            //            for (int i = 0; i < days; i++)
            //            {
            //                string currentDate = Convert.ToDateTime(fromDate).AddDays(i).ToString("yyyy-MM-dd");
            //                int c = 0;
            //                foreach (DataRow dr in dtSecondaryDates.Select("DATE='" + currentDate + "' AND MACHINE_FID IN (" + machineID + ") AND FULLY_OCCUPIED_FLAG=1"))
            //                {
            //                    c++;
            //                }

            //                if (c == 0)
            //                {
            //                    DataRow drd = dtDates.NewRow();
            //                    drd["SR_NO"] = i;
            //                    drd["DATE"] = currentDate;
            //                    dtDates.Rows.Add(drd);
            //                }
            //            }

            //            dtTempNew = CreateScheduledTable(null, dtDates, singleMachineID, singleMachineName, dtMachine, Convert.ToInt32(lblActivitySchD.Text), Convert.ToString(txtActivitySchD.Text));

            //        }
            //        else
            //        {
            //            dtTempNew = CreateScheduledTable(null, dtDates, singleMachineID, singleMachineName, dtMachine, Convert.ToInt32(lblActivitySchD.Text), Convert.ToString(txtActivitySchD.Text));
            //        }
            //    }
            //}
            //else
            //{
            //    dtTempNew = CreateScheduledTable(null, dtDates, singleMachineID, singleMachineName, dtMachine, Convert.ToInt32(lblActivitySchD.Text), Convert.ToString(txtActivitySchD.Text));
            //}

            #endregion

        }
        catch (Exception ex)
        {
            ExceptionMessageScheduleDates(ex.ToString());
            return null;
        }
    }

    private void GetScheduleDates()
    {
        try
        {
            DataTable dt = new DataTable();

            if (Session["dtMachineSchedulingList"] != null)
            {
                dt = (DataTable)Session["dtMachineSchedulingList"];
            }


            DataTable dtSelectedRecords = new DataTable();
            dtSelectedRecords = CreateTempSchedulingTable();

            if (gvAvailableDatesList.Rows.Count > 0)
            {

                int checkedCounts = 0;
                foreach (GridViewRow gr in gvAvailableDatesList.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        checkedCounts++;
                    }
                }

                if (checkedCounts == 0)
                {
                    mpeScheduleMachine.Show();
                    mpeScheduleDates.Show();
                    ExceptionMessageScheduleDates("Please select atleat 1 record...!!!");
                    return;
                }



                foreach (GridViewRow gr in gvAvailableDatesList.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        Label lblTypeOfWorkID = gr.FindControl("lblTypeOfWorkID") as Label;
                        Label lblActivityID = gr.FindControl("lblActivityID") as Label;
                        Label lblMachineID = gr.FindControl("lblMachineID") as Label;


                        TextBox txtSrNoToMSchInList = gr.FindControl("txtSrNoToMSchInList") as TextBox;

                        //TextBox txtTypeOfWorkToMSchInList = gr.FindControl("txtTypeOfWorkToMSchInList") as TextBox;
                        //TextBox txtActivityToMSchInList = gr.FindControl("txtActivityToMSchInList") as TextBox;

                        Label lblActivityName = gr.FindControl("lblActivityName") as Label;

                        DropDownList ddlTypeOfWorkToMSchInList = gr.FindControl("ddlTypeOfWorkToMSchInList") as DropDownList;
                        DropDownList ddlWorkerMSchInList = gr.FindControl("ddlWorkerMSchInList") as DropDownList;

                        TextBox txtMachineToMSchInList = gr.FindControl("txtMachineToMSchInList") as TextBox;
                        TextBox txtScheduledDateToMSchInList = gr.FindControl("txtScheduledDateToMSchInList") as TextBox;

                        TextBox txtScheduledFromToMSchInList = gr.FindControl("txtScheduledFromToMSchInList") as TextBox;
                        TextBox txtScheduledToMSchInList = gr.FindControl("txtScheduledToMSchInList") as TextBox;
                        TextBox txtScheduledHoursToMSchInList = gr.FindControl("txtScheduledHoursToMSchInList") as TextBox;

                        //TextBox txtAvailableFromToMSchInList = gr.FindControl("txtAvailableFromToMSchInList") as TextBox;
                        //TextBox txtAvailableToMSchInList = gr.FindControl("txtAvailableToMSchInList") as TextBox;

                        DropDownList ddlAFH = gr.FindControl("ddlAFH") as DropDownList;
                        DropDownList ddlAFM = gr.FindControl("ddlAFM") as DropDownList;

                        DropDownList ddlATH = gr.FindControl("ddlATH") as DropDownList;
                        DropDownList ddlATM = gr.FindControl("ddlATM") as DropDownList;

                        //TextBox txtAvailableHoursToMSchInList = gr.FindControl("txtAvailableHoursToMSchInList") as TextBox;
                        TextBox txtLatestScheduledHoursToMSchInList = gr.FindControl("txtLatestScheduledHoursToMSchInList") as TextBox;
                        TextBox txtRemarksToMSchInList = gr.FindControl("txtRemarksToMSchInList") as TextBox;
                        Label lblEndTimeFlag = gr.FindControl("lblEndTimeFlag") as Label;

                        string FH = string.Empty;
                        string FM = string.Empty;

                        string TH = string.Empty;
                        string TM = string.Empty;

                        //if (Convert.ToInt32(ddlAFH.SelectedValue) < 10)
                        //    FH = "0" + Convert.ToString(ddlAFH.SelectedValue);
                        //else FH = Convert.ToString(ddlAFH.SelectedValue);


                        //if (Convert.ToInt32(ddlAFM.SelectedValue) < 10)
                        //    FM = "0" + Convert.ToString(ddlAFM.SelectedValue);
                        //else FM = Convert.ToString(ddlAFM.SelectedValue);


                        //if (Convert.ToInt32(ddlATH.SelectedValue) < 10)
                        //    TH = "0" + Convert.ToString(ddlATH.SelectedValue);
                        //else TH = Convert.ToString(ddlATH.SelectedValue);


                        //if (Convert.ToInt32(ddlATM.SelectedValue) < 10)
                        //    TM = "0" + Convert.ToString(ddlATM.SelectedValue);
                        //else TM = Convert.ToString(ddlATM.SelectedValue);



                        FH = Convert.ToString(ddlAFH.SelectedValue);
                        FM = Convert.ToString(ddlAFM.SelectedValue);

                        TH = Convert.ToString(ddlATH.SelectedValue);
                        TM = Convert.ToString(ddlATM.SelectedValue);

                        DataRow drtn = dtSelectedRecords.NewRow();

                        drtn["SR_NO"] = Convert.ToInt32(txtSrNoToMSchInList.Text);

                        drtn["TYPE_OF_WORK_ID"] = Convert.ToInt32(ddlTypeOfWorkToMSchInList.SelectedValue);
                        drtn["TYPE_OF_WORK"] = Convert.ToString(ddlTypeOfWorkToMSchInList.SelectedItem.Text);

                        drtn["ACTIVITY_ID"] = Convert.ToInt32(lblActivityID.Text);
                        drtn["ACTIVITY_NAME"] = Convert.ToString(lblActivityName.Text);

                        drtn["MACHINE_ID"] = Convert.ToInt32(lblMachineID.Text);
                        drtn["MACHINE_NAME"] = Convert.ToString(txtMachineToMSchInList.Text);

                        drtn["EMP_RECORD_ID"] = Convert.ToInt32(ddlWorkerMSchInList.SelectedValue);
                        drtn["EMPLOYEE_NAME"] = Convert.ToString(ddlWorkerMSchInList.SelectedItem.Text);

                        drtn["DATE"] = Convert.ToString(txtScheduledDateToMSchInList.Text);

                        drtn["SCHEDULED_FROM"] = Convert.ToString(FH + ":" + FM);
                        drtn["SCHEDULED_TO"] = Convert.ToString(TH + ":" + TM);
                        drtn["SCHEDULED_HOURS"] = Convert.ToDateTime(txtLatestScheduledHoursToMSchInList.Text).ToString("HH:mm");

                        drtn["REMARKS"] = Convert.ToString(txtRemarksToMSchInList.Text);
                        drtn["END_TIME_FLAG"] = Convert.ToInt32(lblEndTimeFlag.Text);

                        dtSelectedRecords.Rows.Add(drtn);
                    }
                }
            }

            DataTable dtFinal = new DataTable();
            dtFinal = CreateTempSchedulingTable();

            if (dtSelectedRecords.Rows.Count > 0)
            {
                if (Session["dtMachineSchedulingList"] != null)
                {
                    dtFinal = (DataTable)Session["dtMachineSchedulingList"];

                    foreach (DataRow dr in dtSelectedRecords.Rows)
                    {
                        DataRow drtn = dtFinal.NewRow();

                        drtn["SR_NO"] = Convert.ToInt32(dr["SR_NO"]);

                        drtn["TYPE_OF_WORK_ID"] = Convert.ToInt32(dr["TYPE_OF_WORK_ID"]);
                        drtn["TYPE_OF_WORK"] = Convert.ToString(dr["TYPE_OF_WORK"]);

                        drtn["ACTIVITY_ID"] = Convert.ToInt32(dr["ACTIVITY_ID"]);
                        drtn["ACTIVITY_NAME"] = Convert.ToString(dr["ACTIVITY_NAME"]);

                        drtn["MACHINE_ID"] = Convert.ToInt32(dr["MACHINE_ID"]);
                        drtn["MACHINE_NAME"] = Convert.ToString(dr["MACHINE_NAME"]);

                        drtn["EMP_RECORD_ID"] = Convert.ToInt32(dr["EMP_RECORD_ID"]);
                        drtn["EMPLOYEE_NAME"] = Convert.ToString(dr["EMPLOYEE_NAME"]);

                        drtn["DATE"] = Convert.ToDateTime(dr["DATE"]).ToString("dd-MMM-yyyy");

                        drtn["SCHEDULED_FROM"] = Convert.ToString(dr["SCHEDULED_FROM"]);
                        drtn["SCHEDULED_TO"] = Convert.ToString(dr["SCHEDULED_TO"]);
                        drtn["SCHEDULED_HOURS"] = Convert.ToString(dr["SCHEDULED_HOURS"]);

                        //drtn["AVAILABLE_FROM"] = Convert.ToString(dr["AVAILABLE_FROM"]);
                        //drtn["AVAILABLE_TO"] = Convert.ToString(dr["AVAILABLE_TO"]);
                        //drtn["AVAILABLE_HOURS"] = Convert.ToString(dr["AVAILABLE_HOURS"]);

                        drtn["REMARKS"] = Convert.ToString(dr["REMARKS"]);
                        drtn["END_TIME_FLAG"] = Convert.ToInt32(dr["END_TIME_FLAG"]);

                        dtFinal.Rows.Add(drtn);
                    }
                }
                else
                {
                    dtFinal = dtSelectedRecords;
                }

                if (dtFinal.Rows.Count > 0)
                {
                    dtFinal.DefaultView.Sort = "SCHEDULED_FROM";
                    dtFinal.DefaultView.Sort = "DATE";
                    dtFinal.DefaultView.Sort = "MACHINE_NAME";
                    dtFinal.DefaultView.Sort = "ACTIVITY_NAME";

                    dtFinal = dtFinal.DefaultView.ToTable();


                    int c = 0;
                    foreach (DataRow dr in dtFinal.Rows)
                    {
                        c++;
                        dr["SR_NO"] = c;
                    }

                    Session["dtMachineSchedulingList"] = dtFinal;
                    gvMachineSchedulingList.DataSource = dtFinal;
                    gvMachineSchedulingList.DataBind();
                }
            }

            lblScheduleMachineRecords.Text = "Records[" + gvMachineSchedulingList.Rows.Count + "]";

            ddlActivityToMSch.SelectedIndex = 0;
            mpeScheduleMachine.Show();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SaveScheduledDetails()
    {
        try
        {
            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            assignedDrawingCode = Convert.ToString(ViewState["ASSIGNED_DRAWING_CODE"]);
            drawingNo = Convert.ToString(ViewState["DRAWING_NO"]);

            string commitedDateByShopIncharge = string.Empty;
            string dateOfReceiptOfMaterial = string.Empty;

            commitedDateByShopIncharge = Convert.ToDateTime(txtCommitedDateByShopInchargeToMSch.Text).ToString("yyyy-MM-dd");
            dateOfReceiptOfMaterial = Convert.ToDateTime(txtDateOfReceiptOfMaterialToMSch.Text).ToString("yyyy-MM-dd");

            DataTable dtNew = new DataTable();


            if (gvMachineSchedulingList.Rows.Count > 0)
            {
                if (dtNew.Columns.Count == 0)
                {
                    dtNew.Columns.Add("ASSIGNED_RECORD_FID", typeof(int));
                    dtNew.Columns.Add("TYPE_OF_WORK_FID", typeof(int));
                    dtNew.Columns.Add("ACTIVITY_FID", typeof(int));
                    dtNew.Columns.Add("MACHINE_FID", typeof(int));
                    dtNew.Columns.Add("EMP_RECORD_FID", typeof(int));
                    dtNew.Columns.Add("DATE", typeof(string));
                    dtNew.Columns.Add("SCHEDULED_FROM", typeof(string));
                    dtNew.Columns.Add("SCHEDULED_TO", typeof(string));
                    dtNew.Columns.Add("PRODUCTION_MANAGER_FID", typeof(int));
                    dtNew.Columns.Add("REMARKS", typeof(string));
                }

                foreach (GridViewRow gr in gvMachineSchedulingList.Rows)
                {
                    Label lblTypeOfWorkID = gr.FindControl("lblTypeOfWorkID") as Label;
                    Label lblActivityID = gr.FindControl("lblActivityID") as Label;
                    Label lblMachineID = gr.FindControl("lblMachineID") as Label;
                    Label lblWorkerID = gr.FindControl("lblWorkerID") as Label;

                    TextBox txtScheduledDateToMSchInList = gr.FindControl("txtScheduledDateToMSchInList") as TextBox;
                    TextBox txtScheduledFromToMSchInList = gr.FindControl("txtScheduledFromToMSchInList") as TextBox;
                    TextBox txtScheduledToMSchInList = gr.FindControl("txtScheduledToMSchInList") as TextBox;
                    TextBox txtRemarksToMSchInList = gr.FindControl("txtRemarksToMSchInList") as TextBox;

                    DataRow drtn = dtNew.NewRow();

                    drtn["ASSIGNED_RECORD_FID"] = Convert.ToInt32(ViewState["RECORD_ID"]);
                    drtn["TYPE_OF_WORK_FID"] = Convert.ToInt32(lblTypeOfWorkID.Text);
                    drtn["ACTIVITY_FID"] = Convert.ToInt32(lblActivityID.Text);
                    drtn["MACHINE_FID"] = Convert.ToInt32(lblMachineID.Text);
                    drtn["EMP_RECORD_FID"] = Convert.ToInt32(lblWorkerID.Text);
                    drtn["DATE"] = Convert.ToString(txtScheduledDateToMSchInList.Text);

                    drtn["SCHEDULED_FROM"] = Convert.ToString(txtScheduledFromToMSchInList.Text);
                    drtn["SCHEDULED_TO"] = Convert.ToString(txtScheduledToMSchInList.Text);

                    drtn["PRODUCTION_MANAGER_FID"] = 0;
                    drtn["REMARKS"] = Convert.ToString(txtRemarksToMSchInList.Text);

                    dtNew.Rows.Add(drtn);
                }
            }



            if (dtNew.Rows.Count > 0)
            {
                int value = objMS.SaveScheduleData(recordID, commitedDateByShopIncharge, dateOfReceiptOfMaterial, dtNew, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {

                    int sendMailValue = objMSSendMail.ProcessAndSendAcceptanceMail(recordID, assignedDrawingCode,
                                                                                   Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.ScheduledMail),
                                                                                   drawingNo, remarks);

                    if (sendMailValue > 0)
                    {
                        objMS.UpdateAcceptanceMailStatus(recordID, Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Scheduled), Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        SuccessMessage("Machines scheduled for assigned drawing and mail sent successfully...!!!");
                    }
                    else
                    {
                        SuccessMessage("Machines scheduled for assigned drawing successfully...!!!");
                    }

                    GetAssignedItemsList();
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again...!!!");
                    return;
                }
            }
            else
            {
                ExceptionMessage("No records found for scheduling...!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void CompleteScheduledDetails()
    {
        try
        {
            int totalCounts = 0;
            int scheduledCount = Convert.ToInt32(ViewState["SCHEDULED_COUNTS"]);
            int completedCount = Convert.ToInt32(ViewState["COMPLETED_COUNTS"]);

            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            assignedDrawingCode = Convert.ToString(ViewState["ASSIGNED_DRAWING_CODE"]);
            drawingNo = Convert.ToString(ViewState["DRAWING_NO"]);

            string completionDate = string.Empty;
            int rowCounts = 0;

            completionDate = Convert.ToDateTime(txtCompletionDateToCMSch.Text).ToString("yyyy-MM-dd");

            DataTable dtNew = new DataTable();


            if (gvScheduledMachineListForCompletion.Rows.Count > 0)
            {
                if (dtNew.Columns.Count == 0)
                {
                    dtNew.Columns.Add("SR_NO", typeof(int));
                    dtNew.Columns.Add("RECORD_PID", typeof(int));
                    dtNew.Columns.Add("COMPLETED_REMARKS", typeof(string));
                }


                int count = 0;

                count = 0;
                foreach (GridViewRow gr in gvScheduledMachineListForCompletion.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                    if (chkSelect.Checked)
                    {
                        count++;
                        Label lblRecordID = gr.FindControl("lblRecordID") as Label;
                        TextBox txtCompletionRemarksToMSchInList = gr.FindControl("txtCompletionRemarksToMSchInList") as TextBox;

                        DataRow drtn = dtNew.NewRow();

                        drtn["SR_NO"] = count;
                        drtn["RECORD_PID"] = Convert.ToInt32(lblRecordID.Text);
                        drtn["COMPLETED_REMARKS"] = Convert.ToString(txtCompletionRemarksToMSchInList.Text);

                        dtNew.Rows.Add(drtn);
                    }
                }
            }



            if (dtNew.Rows.Count > 0)
            {
                rowCounts = dtNew.Rows.Count;
                int value = objMS.CompleteScheduledDetails(recordID, completionDate, dtNew, rowCounts, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    totalCounts = (rowCounts + completedCount);

                    if (scheduledCount == totalCounts)
                    {
                        int sendMailValue = objMSSendMail.ProcessAndSendAcceptanceMail(recordID, assignedDrawingCode,
                                                                                   Convert.ToInt32(MSAllStatusAndTypes.EnumMailType.CompletedMail),
                                                                                   drawingNo, remarks);

                        if (sendMailValue > 0)
                        {
                            objMS.UpdateAcceptanceMailStatus(recordID, Convert.ToInt32(MSAllStatusAndTypes.EnumStatus.Completed), Convert.ToInt32(Session["EMP_RECORD_ID"]));
                            SuccessMessage("Completion done and mail sent successfully...!!!");
                        }
                        else
                        {
                            SuccessMessage("Completion done successfully...!!!");
                        }
                    }
                    else
                    {
                        SuccessMessage("Completion done successfully...!!!");
                    }


                    GetAssignedItemsList();
                    return;

                }
                else
                {
                    ExceptionMessageCompletionScheduling("Please try again...!!!");
                    mpeSchedulingCompletion.Show();
                    return;
                }
            }
            else
            {
                ExceptionMessageCompletionScheduling("Please select atleast 1 record...!!!");
                mpeSchedulingCompletion.Show();
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageCompletionScheduling(ex.ToString());
            mpeSchedulingCompletion.Show();
            return;
        }
    }

    private void ModifyWorkerDetails()
    {
        try
        {
            int recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            int rowCounts = 0;

            DataTable dtNew = new DataTable();


            if (gvScheduledMachineListForCompletion.Rows.Count > 0)
            {
                if (dtNew.Columns.Count == 0)
                {
                    dtNew.Columns.Add("SR_NO", typeof(int));
                    dtNew.Columns.Add("RECORD_PID", typeof(int));
                    dtNew.Columns.Add("EMP_RECORD_FID", typeof(int));
                    dtNew.Columns.Add("REMARKS", typeof(string));
                }


                int count = 0;

                count = 0;
                foreach (GridViewRow gr in gvScheduledMachineListForCompletion.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                    if (chkSelect.Checked)
                    {
                        count++;
                        Label lblRecordID = gr.FindControl("lblRecordID") as Label;
                        TextBox txtRemarksToMSchInList = gr.FindControl("txtRemarksToMSchInList") as TextBox;
                        DropDownList ddlWorkerMSchInList = gr.FindControl("ddlWorkerMSchInList") as DropDownList;

                        DataRow drtn = dtNew.NewRow();

                        drtn["SR_NO"] = count;
                        drtn["RECORD_PID"] = Convert.ToInt32(lblRecordID.Text);
                        drtn["EMP_RECORD_FID"] = Convert.ToInt32(ddlWorkerMSchInList.SelectedValue);
                        drtn["REMARKS"] = Convert.ToString(txtRemarksToMSchInList.Text);

                        dtNew.Rows.Add(drtn);
                    }
                }
            }



            if (dtNew.Rows.Count > 0)
            {
                rowCounts = dtNew.Rows.Count;
                int value = objMS.ModifyWorkerDetails(recordID, dtNew, rowCounts, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    GetAssignedItemsList();
                    SuccessMessage("Worker modified successfully...!!!");
                    return;
                }
                else
                {
                    ExceptionMessageCompletionScheduling("Please try again...!!!");
                    mpeSchedulingCompletion.Show();
                    return;
                }
            }
            else
            {
                ExceptionMessageCompletionScheduling("Please select atleast 1 record...!!!");
                mpeSchedulingCompletion.Show();
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageCompletionScheduling(ex.ToString());
            mpeSchedulingCompletion.Show();
            return;
        }
    }


    private void Reset()
    {
        //chkNA.Checked = false;
        //ddlCompanyToA.SelectedIndex = 0;
        txtJOBNoToA.Text = string.Empty;
        txtDrawingNoToA.Text = string.Empty;
        txtEquipmentToA.Text = string.Empty;
        txtItemDetailToA.Text = string.Empty;
        hdDateToA.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtDateToA.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtQuantityToA.Text = "0";
        txtRemarkstoA.Text = string.Empty;

        hdLOTTFID.Value = string.Empty;
        hdLOTTFSubitemID.Value = string.Empty;
    }

    private void BindScheduledListForCompletion(int recordID, string fromDate, string toDate, int activityID,
                                                int machineID, int typeOfWorkID, int workerID)
    {
        try
        {
            DataSet dsScheduledList = new DataSet();
            dsScheduledList = objMS.GetScheduledListForViewDetails(recordID, fromDate, toDate, activityID, machineID, typeOfWorkID, workerID);

            gvScheduledMachineListForCompletion.DataSource = null;
            gvScheduledMachineListForCompletion.DataBind();

            if (dsScheduledList.Tables.Count > 0 && dsScheduledList.Tables[0].Rows.Count > 0)
            {
                gvScheduledMachineListForCompletion.DataSource = dsScheduledList.Tables[0];
                gvScheduledMachineListForCompletion.DataBind();
            }

            lblScheduledMachineListRecords.Text = "Records [" + gvScheduledMachineListForCompletion.Rows.Count + "]";

        }
        catch (Exception ex)
        {
            //
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

    private void ExceptionMessageScheduleDates(string message)
    {
        pnlScheduleDatesMsg.Visible = true;
        lblScheduleDatesMsg.Text = message;
        lblScheduleDatesMsg.ForeColor = System.Drawing.Color.Red;
    }


    private void HideMessagePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    private void HideMessagePanelScheduleDates()
    {
        pnlScheduleDatesMsg.Visible = false;
        lblScheduleDatesMsg.Text = string.Empty;
    }


    private void ExceptionMessageCompletionScheduling(string message)
    {
        pnlSchedulingCompletionMsg.Visible = true;
        lblSchedulingCompletionMsg.Text = message;
        lblSchedulingCompletionMsg.ForeColor = System.Drawing.Color.Red;
    }


    private void HideMessagePanelCompletionScheduling()
    {
        pnlSchedulingCompletionMsg.Visible = false;
        lblSchedulingCompletionMsg.Text = string.Empty;
    }

    #endregion    

}