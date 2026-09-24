using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_AddOpsCostType : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    string costType = string.Empty;

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
        AddNewOpsCostType();
    }

    protected void btnCostTypeList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FINOPS/OpsCostTypeList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void AddNewOpsCostType()
    {
        try
        {
            costType = string.Empty;

            if (!string.IsNullOrEmpty(txtCostTypeName.Text))
                costType = Convert.ToString(txtCostTypeName.Text);


            int value = objFinOps.AddUpdateOpsCostType(0, costType, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Ops Cost type added successfully");
                txtCostTypeName.Text = string.Empty;
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
