using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_AddOpsContigencyType : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    string contigencyType = string.Empty;

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
        AddNewOpscontigencyType();
    }

    protected void btnContigencyTypeList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FINOPS/OpscontigencyTypeList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void AddNewOpscontigencyType()
    {
        try
        {
            contigencyType = string.Empty;

            if (!string.IsNullOrEmpty(txtContigencyTypeName.Text))
                contigencyType = Convert.ToString(txtContigencyTypeName.Text);


            int value = objFinOps.AddUpdateOpsContigencyType(0, contigencyType, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Ops contigency type added successfully");
                txtContigencyTypeName.Text = string.Empty;
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
