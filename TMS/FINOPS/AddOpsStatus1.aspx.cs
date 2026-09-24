using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_AddOpsStatus1 : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    string status1 = string.Empty;

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
        AddNewOpsStatus1();
    }

    protected void btnStatus1List_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FINOPS/OpsStatus1List.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void AddNewOpsStatus1()
    {
        try
        {
            status1 = string.Empty;

            if (!string.IsNullOrEmpty(txtStatus1Name.Text))
                status1 = Convert.ToString(txtStatus1Name.Text);


            int value = objFinOps.AddUpdateOpsStatus1(0, status1, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Ops Status1 added successfully");
                txtStatus1Name.Text = string.Empty;
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
