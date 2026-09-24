using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PROJECT_MACHINE_SCH_ActivitiesList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.MachineScheduling objMachineScheduling = new BAL.MachineScheduling();

    DataSet dsMachineActivityList = new DataSet();

    int recordID = 0;
    string activityName = string.Empty;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                GetActivityList();
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
        GetActivityList();
    }

    protected void gvActivityList_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvActivityList_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblActivityID = gvActivityList.Rows[rowindex].FindControl("lblActivityID") as Label;
                Label lblActivityName = gvActivityList.Rows[rowindex].FindControl("lblActivityName") as Label;

                ViewState["RecordID"] = Convert.ToInt32(lblActivityID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    txtActivityToU.Text = lblActivityName.Text;
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
        Response.Redirect("~/PROJECT/MACHINE_SCH/AddActivities.aspx");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateActivity();
    }


    #endregion


    #region METHODS[=======================]

    private void GetActivityList()
    {
        try
        {
            activityName = string.Empty;

            if (!string.IsNullOrEmpty(txtActivity.Text))
                activityName = Convert.ToString(txtActivity.Text);

            dsMachineActivityList = objMachineScheduling.GetActivities(activityName);
            if (dsMachineActivityList.Tables.Count > 0 && dsMachineActivityList.Tables[0].Rows.Count > 0)
            {
                gvActivityList.DataSource = dsMachineActivityList.Tables[0];
                gvActivityList.DataBind();
            }
            else
            {
                gvActivityList.DataSource = null;
                gvActivityList.DataBind();
            }
            lblRecords.Text = "Records[" + dsMachineActivityList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateActivity()
    {
        try
        {
            recordID = 0;

            recordID = Convert.ToInt32(ViewState["RecordID"]);
            activityName = Convert.ToString(txtActivityToU.Text);



            int value = objMachineScheduling.AddUpdateActivity(recordID, activityName, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Activity updated successfully...!!!");
                txtActivityToU.Text = string.Empty;
                GetActivityList();
                return;
            }
            else if (value < 0)
            {
                ExceptionMessage("Activity already exists...!!!");
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
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
