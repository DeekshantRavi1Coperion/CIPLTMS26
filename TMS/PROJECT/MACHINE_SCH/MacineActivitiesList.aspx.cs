using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PROJECT_MACHINE_SCH_MacineActivitiesList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.MachineScheduling objMachineScheduling = new BAL.MachineScheduling();

    DataSet dsMachineActivityList = new DataSet();
    DataSet dsActivities = new DataSet();
    DataSet dsMachines = new DataSet();

    int recordID = 0;
    int activityID = 0;
    int machineID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtActivities"] = null;
                Session["dtMachines"] = null;

                BindActivity(0);
                BindMachines(0);
                GetMachineActivityList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetMachineActivityList();
    }

    protected void gvMahineActivityList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void gvMahineActivityList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                HidePanels();
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = gvMahineActivityList.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblActivityID = gvMahineActivityList.Rows[rowindex].FindControl("lblActivityID") as Label;
                Label lblMachineID = gvMahineActivityList.Rows[rowindex].FindControl("lblMachineID") as Label;

                ViewState["RecordID"] = Convert.ToInt32(lblRecordID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    BindActivity(1);
                    BindMachines(1);
                    ddlActivityToU.SelectedValue = Convert.ToString(lblActivityID.Text);
                    ddlMacnineToU.SelectedValue = Convert.ToString(lblMachineID.Text);
                    this.ModalPopupExtender1.Show();
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



    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/MACHINE_SCH/AddMacineActivities.aspx");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateMachineActivity();
    }


    #endregion


    #region METHODS[=======================]

    private DataTable GetActivityData()
    {
        try
        {
            dsActivities = objMachineScheduling.GetActivities("");
            if (dsActivities.Tables.Count > 0 && dsActivities.Tables[0].Rows.Count > 0)
            {
                Session["dtActivities"] = dsActivities.Tables[0];
                return dsActivities.Tables[0];
            }
            else
            {
                Session["dtActivities"] = null;
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void BindActivity(int typeID)
    {
        try
        {
            DataTable dtActivities = new DataTable();

            if (typeID == 0)
            {
                dtActivities = GetActivityData();
                if (dtActivities != null && dtActivities.Rows.Count > 0)
                {
                    ddlActivity.DataSource = dtActivities;
                    ddlActivity.DataTextField = "ACTIVITY_NAME";
                    ddlActivity.DataValueField = "ACTIVITY_PID";
                    ddlActivity.DataBind();
                    ddlActivity.Items.Insert(0, "Select");
                    ddlActivity.SelectedIndex = 0;
                }
            }
            else
            {
                if (Session["dtActivities"] != null)
                    dtActivities = (DataTable)Session["dtActivities"];
                else
                    dtActivities = GetActivityData();

                if (dtActivities != null && dtActivities.Rows.Count > 0)
                {
                    ddlActivityToU.DataSource = dtActivities;
                    ddlActivityToU.DataTextField = "ACTIVITY_NAME";
                    ddlActivityToU.DataValueField = "ACTIVITY_PID";
                    ddlActivityToU.DataBind();
                    ddlActivityToU.Items.Insert(0, "Select");
                    ddlActivityToU.SelectedIndex = 0;
                }
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
            dsMachines = objMachineScheduling.GetMachines("");
            if (dsMachines.Tables.Count > 0 && dsMachines.Tables[0].Rows.Count > 0)
            {
                Session["dtMachines"] = dsMachines.Tables[0];
                return dsMachines.Tables[0];
            }
            else
            {
                Session["dtMachines"] = null;
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void BindMachines(int typeID)
    {
        try
        {
            DataTable dtMachines = new DataTable();

            if (typeID == 0)
            {
                dtMachines = GetMachinesData();
                if (dtMachines != null && dtMachines.Rows.Count > 0)
                {
                    ddlMacnine.DataSource = dtMachines;
                    ddlMacnine.DataTextField = "MACHINE_NAME";
                    ddlMacnine.DataValueField = "MACHINE_PID";
                    ddlMacnine.DataBind();
                    ddlMacnine.Items.Insert(0, "Select");
                    ddlMacnine.SelectedIndex = 0;
                }
            }
            else
            {
                if (Session["dtMachines"] != null)
                    dtMachines = (DataTable)Session["dtMachines"];
                else
                    dtMachines = GetActivityData();

                if (dtMachines != null && dtMachines.Rows.Count > 0)
                {
                    ddlMacnineToU.DataSource = dtMachines;
                    ddlMacnineToU.DataTextField = "MACHINE_NAME";
                    ddlMacnineToU.DataValueField = "MACHINE_PID";
                    ddlMacnineToU.DataBind();
                    ddlMacnineToU.Items.Insert(0, "Select");
                    ddlMacnineToU.SelectedIndex = 0;
                }
            }


        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetMachineActivityList()
    {
        try
        {
            activityID = 0;
            machineID = 0;

            if (ddlActivity.SelectedIndex > 0)
                activityID = Convert.ToInt32(ddlActivity.SelectedValue);

            if (ddlMacnine.SelectedIndex > 0)
                machineID = Convert.ToInt32(ddlMacnine.SelectedValue);


            dsMachineActivityList = objMachineScheduling.GetMachineActivityList(activityID, machineID);
            if (dsMachineActivityList.Tables.Count > 0 && dsMachineActivityList.Tables[0].Rows.Count > 0)
            {
                gvMahineActivityList.DataSource = dsMachineActivityList.Tables[0];
                gvMahineActivityList.DataBind();
            }
            else
            {
                gvMahineActivityList.DataSource = null;
                gvMahineActivityList.DataBind();
            }
            lblRecords.Text = "Records[" + dsMachineActivityList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateMachineActivity()
    {
        try
        {
            recordID = 0;
            activityID = 0;
            machineID = 0;

            recordID = Convert.ToInt32(ViewState["RecordID"]);
            activityID = Convert.ToInt32(ddlActivityToU.SelectedValue);
            machineID = Convert.ToInt32(ddlMacnineToU.SelectedValue);


            int value = objMachineScheduling.AddMachineActivity(recordID, activityID, machineID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Machine activity updated successfully...!!!");
                GetMachineActivityList();
                return;
            }
            else if (value < 0)
            {
                ExceptionMessage("Machine activity already exists...!!!");
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }

            ddlActivityToU.SelectedIndex = 0;
            ddlMacnineToU.SelectedIndex = 0;

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

    private void ExceptionUpdateMessage(string message)
    {
        pnlUpdateMsg.Visible = true;
        lblUpdateMsg.Text = message;
        lblUpdateMsg.ForeColor = System.Drawing.Color.Red;
    }



    private void HidePanels()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;

        pnlUpdateMsg.Visible = false;
        lblUpdateMsg.Text = string.Empty;
    }
    #endregion

}
