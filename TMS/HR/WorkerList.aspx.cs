using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class HR_WorkerList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Hrdept objHrdept = new BAL.Hrdept();
    DataSet dsWokerList = new DataSet();
    DataSet dsWorker = new DataSet();

    int recordID = 0;
    int essWorkerRecordID = 0;
    string employeeID = string.Empty;
    string workerCode = string.Empty;
    double convenienceAmt = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                BindWorker();
                GetWorkerList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetWorkerList();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        AddUpdateWorkerConvenience();
    }

    protected void gvWorkerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = (Label)gvWorkerList.Rows[rowindex].FindControl("lblRecordID");
                Label lblEssWorkerRecordID = (Label)gvWorkerList.Rows[rowindex].FindControl("lblEssWorkerRecordID");
                Label lblEmpRecordID = (Label)gvWorkerList.Rows[rowindex].FindControl("lblEmpRecordID");                
                Label lblWorkerName = (Label)gvWorkerList.Rows[rowindex].FindControl("lblWorkerName");
                Label lblWorkerCode = (Label)gvWorkerList.Rows[rowindex].FindControl("lblWorkerCode");
                Label lblUnit = (Label)gvWorkerList.Rows[rowindex].FindControl("lblUnit");
                Label lblConvenienceAmt = (Label)gvWorkerList.Rows[rowindex].FindControl("lblConvenienceAmt");


                ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);
                ViewState["ESS_WORKER_RECORD_ID"] = Convert.ToString(lblEssWorkerRecordID.Text);
                ViewState["EMPLOYEE_ID"]= Convert.ToString(lblWorkerCode.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    lblLegend.Text = "Worker Convenince Detail";

                    txtWorkerName.Text = Convert.ToString(lblWorkerName.Text);
                    txtWorkerCodeNew.Text = Convert.ToString(lblWorkerCode.Text);
                    txtUnit.Text = Convert.ToString(lblUnit.Text);
                    txtConvenienceAmt.Text = Convert.ToString(lblConvenienceAmt.Text);

                    ModalPopupExtender1.Show();
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

    protected void gvWorkerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    #endregion


    #region METHODS[=========================]


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

    private void GetWorkerList()
    {
        try
        {


            if (Convert.ToInt32(ddlWorker.SelectedIndex) > 0)
                essWorkerRecordID = Convert.ToInt32(ddlWorker.SelectedValue);
            else
                essWorkerRecordID = 0;

            if (!string.IsNullOrEmpty(txtWorkerCode.Text))
                workerCode = txtWorkerCode.Text;
            else
                workerCode = string.Empty;


            dsWokerList = objHrdept.GetWorkerList(essWorkerRecordID, workerCode);

            if (dsWokerList.Tables.Count > 0 && dsWokerList.Tables[0].Rows.Count > 0)
            {
                gvWorkerList.DataSource = dsWokerList.Tables[0];
                gvWorkerList.DataBind();
            }
            else
            {
                gvWorkerList.DataSource = null;
                gvWorkerList.DataBind();
            }
            lblRecords.Text = "Records[" + dsWokerList.Tables[0].Rows.Count + "]";
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
            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
            essWorkerRecordID = Convert.ToInt32(ViewState["ESS_WORKER_RECORD_ID"]);
            employeeID = Convert.ToString(ViewState["EMPLOYEE_ID"]);

            if (!string.IsNullOrEmpty(Convert.ToString(txtConvenienceAmt.Text)))
                convenienceAmt = Convert.ToDouble(txtConvenienceAmt.Text);
            else
                convenienceAmt = 0;

            int value = objHrdept.AddUpdateWorkerConvenience(recordID, essWorkerRecordID, employeeID, convenienceAmt, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Convenience updated successfully");
                GetWorkerList();
            }
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}
