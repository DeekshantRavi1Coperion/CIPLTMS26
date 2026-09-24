using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class HR_AddWorkerConvenience : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.Hrdept objHrdept = new BAL.Hrdept();

    DataSet dsWorker = new DataSet();
    int essWorkerRecordID = 0;
    string employeeID = string.Empty;
    double convenienceAmt = 0;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindWorker();
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
        AddUpdateWorkerConvenience();
    }

    protected void btnWorkerList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HR/WorkerList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void BindWorker()
    {
        try
        {
            dsWorker = objHrdept.GetEmployeesByCategory(1);
            if (dsWorker.Tables.Count > 0 && dsWorker.Tables[0].Rows.Count > 0)
            {
                ddlWorker.DataSource = dsWorker.Tables[0];
                ddlWorker.DataTextField = "EMP_NAME";
                ddlWorker.DataValueField = "EMP_ID";
                ddlWorker.DataBind();
                ddlWorker.Items.Insert(0, "Select");
                ddlWorker.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddUpdateWorkerConvenience()
    {
        try
        {
            int essWorkerRecordID = 0;
            string employeeID = string.Empty;

            essWorkerRecordID = Convert.ToInt32(ddlWorker.SelectedValue);
            employeeID = Between(Convert.ToString(ddlWorker.SelectedItem.Text), "[", "]");
            convenienceAmt = Convert.ToDouble(txtConvenienceAmt.Text);

            int value = objHrdept.AddUpdateWorkerConvenience(0, essWorkerRecordID, employeeID, convenienceAmt, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
                SuccessMessage("Convenience added successfully");
            else
                ExceptionMessage("Please try again");

            ddlWorker.SelectedIndex = 0;
            txtConvenienceAmt.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    public string Between(string STR, string FirstString, string LastString)
    {
        string FinalString;
        int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
        int Pos2 = STR.IndexOf(LastString);
        FinalString = STR.Substring(Pos1, Pos2 - Pos1);
        return FinalString;
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
