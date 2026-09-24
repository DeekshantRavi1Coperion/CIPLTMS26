using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PROJECT_MACHINE_SCH_AddMachines : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.MachineScheduling objMachineScheduling = new BAL.MachineScheduling();    
    string machineName = string.Empty;    

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
        AddMachine();
    }

    protected void btnMachineList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/MACHINE_SCH/MachinesList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void AddMachine()
    {
        try
        {
            machineName = string.Empty;
            machineName = Convert.ToString(txtMachine.Text);


            int value = objMachineScheduling.AddUpdateMachine(0, machineName, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Machine added successfully...!!!");
            }
            else if (value < 0)
            {
                ExceptionMessage("Machine already exists...!!!");
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
            }
            txtMachine.Text = string.Empty;
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
