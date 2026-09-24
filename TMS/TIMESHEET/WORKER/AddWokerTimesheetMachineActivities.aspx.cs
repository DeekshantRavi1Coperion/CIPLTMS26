using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TIMESHEET_WORKER_AddWokerTimesheetMachineActivities : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.Timesheet objTimesheet = new BAL.Timesheet();

    string activityName = string.Empty;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                //
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
        AddActivity();
    }

    protected void btnMenuList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/MENU/MenuList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void AddActivity()
    {
        try
        {
            activityName = string.Empty;
            activityName = Convert.ToString(txtActivityName.Text);

            int value = objTimesheet.AddUpdateActivity(0, activityName, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Menu added successfully");
            }
            else
            {
                ExceptionMessage("Please try again");
            }

            txtActivityName.Text = string.Empty;
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
