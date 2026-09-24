using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_AddOpsType : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    string type = string.Empty;

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
        AddNewOpsType();
    }

    protected void btnTypeList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FINOPS/OpsTypeList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void AddNewOpsType()
    {
        try
        {
            type = string.Empty;

            if (!string.IsNullOrEmpty(txtTypeName.Text))
                type = Convert.ToString(txtTypeName.Text);


            int value = objFinOps.AddUpdateFinOpsType(0, type, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Opst Type added successfully");
                txtTypeName.Text = string.Empty;
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
