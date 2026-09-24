using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_AddOpsStatus2 : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    string status2 = string.Empty;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePannel();
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
        AddNewOpsStatus2();
    }

    protected void btnStatus2List_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FINOPS/OpsStatus2List.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void AddNewOpsStatus2()
    {
        try
        {
            status2 = string.Empty;

            if (!string.IsNullOrEmpty(txtStatus2Name.Text))
                status2 = Convert.ToString(txtStatus2Name.Text);


            int value = objFinOps.AddUpdateOpsStatus2(0, status2, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Ops Status2 added successfully");
                txtStatus2Name.Text = string.Empty;
                return;
            }
            else
            {
                ExceptionMessage("Please try again");
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

    private void HidePannel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }



    #endregion

}
