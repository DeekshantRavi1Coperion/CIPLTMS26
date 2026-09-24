using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MACHINE_SCH_UpdateCompletedQuantity : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.MachineScheduling objMS = new BAL.MachineScheduling();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsAssignedItemsList = new DataSet();
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
            HideMessagePanel();

            if (!IsPostBack)
            {
                Session["dtSubitemDetails"] = null;
                Session["dtActivity"] = null;
                BindUnit();
                GetAssignedItemsListForQuantityUpdation();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
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
                if (Convert.ToString(e.CommandArgument) == "UPDATE_QUANTITY")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                TextBox txtSrNo = gvSubitemsList.Rows[rowindex].FindControl("txtSrNo") as TextBox;
                Label lblRecordID = gvSubitemsList.Rows[rowindex].FindControl("lblRecordID") as Label;
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



                if (Convert.ToString(e.CommandArgument) == "UPDATE_QUANTITY")
                {
                    ViewState["RecordID"] = Convert.ToInt32(lblRecordID.Text);

                    ddlMachineUCQ.Items.Clear();
                    ddlMachineUCQ.Items.Insert(0, "Select");
                    ddlMachineUCQ.SelectedIndex = 0;

                    txtUnitUCQ.Text = txtUnit.Text;
                    txtJOBNoUCQ.Text = txtJOBNo.Text;
                    txtDrawingNoUCQ.Text = txtDrawingNo.Text;
                    txtEquipmentUCQ.Text = txtEquipment.Text;

                    BindActivityUCQ();

                    txtAllocatedQuantityUCQ.Text = txtAllocatedQuantity.Text;
                    txtTotalCompletedQuantityUCQ.Text = txtCompletedQuantity.Text;
                    txtPendingQuantityUCQ.Text = txtPendingQuantity.Text;

                    txtRemarksUCQ.Text = string.Empty;
                    mpeUpdateCompletedQuantityDetails.Show();
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
        GetAssignedItemsListForQuantityUpdation();
    }

    protected void btnSaveCompletedQuantity_Click(object sender, EventArgs e)
    {

    }

    protected void btnUpdateCompletedQuantity_Click(object sender, EventArgs e)
    {
        SaveCompletedQuantity();
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
            }
            else
            {
                ddlCompany.Items.Clear();
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

    private void GetAssignedItemsListForQuantityUpdation()
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

            dsAssignedItemsList = objMS.GetAssignedItemsListForQuantityUpdation(companyID, LOTNo, jobNo, drawingNo, equipment);
            if (dsAssignedItemsList.Tables.Count > 0 && dsAssignedItemsList.Tables[0].Rows.Count > 0)
            {
                Session["dtSubitemDetails"] = dsAssignedItemsList.Tables[0];
                gvSubitemsList.DataSource = dsAssignedItemsList.Tables[0];
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
                SuccessMessage("Completed quantity updated successfully...!!!");
                GetAssignedItemsListForQuantityUpdation();
                return;
            }
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

    private void HideMessagePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}