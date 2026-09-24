using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PROJECT_MACHINE_SCH_AddMacineActivities : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.MachineScheduling objMachineScheduling = new BAL.MachineScheduling();

    DataSet dsActivities = new DataSet();
    DataSet dsMachines = new DataSet();
    int activityID = 0;
    int machineID = 0;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindActivity();
                BindMachines();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HidePannel();
        AddMachineActivity();
    }

    protected void btnMachineActivityList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/MACHINE_SCH/MacineActivitiesList.aspx");
    }

    #endregion


    #region METHODS[=====================]



    private void BindActivity()
    {
        try
        {
            dsActivities = objMachineScheduling.GetActivities("");
            if (dsActivities.Tables.Count > 0 && dsActivities.Tables[0].Rows.Count > 0)
            {
                ddlActivity.DataSource = dsActivities.Tables[0];
                ddlActivity.DataTextField = "ACTIVITY_NAME";
                ddlActivity.DataValueField = "ACTIVITY_PID";
                ddlActivity.DataBind();
                ddlActivity.Items.Insert(0, "Select");
                ddlActivity.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindMachines()
    {
        try
        {
            dsMachines = objMachineScheduling.GetMachines("");
            if (dsMachines.Tables.Count > 0 && dsMachines.Tables[0].Rows.Count > 0)
            {
                ddlMacnine.DataSource = dsMachines.Tables[0];
                ddlMacnine.DataTextField = "MACHINE_NAME";
                ddlMacnine.DataValueField = "MACHINE_PID";
                ddlMacnine.DataBind();
                ddlMacnine.Items.Insert(0, "Select");
                ddlMacnine.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddMachineActivity()
    {
        try
        {

            activityID = 0;
            machineID = 0;

            activityID = Convert.ToInt32(ddlActivity.SelectedValue);
            machineID = Convert.ToInt32(ddlMacnine.SelectedValue);




            int value = objMachineScheduling.AddMachineActivity(0, activityID, machineID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Machine activity added successfully...!!!");
            }
            else if (value < 0)
            {
                ExceptionMessage("Machine activity already exists...!!!");
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
            }

            ddlActivity.SelectedIndex = 0;
            ddlMacnine.SelectedIndex = 0;
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

    private void HidePannel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }



    #endregion

}
