using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MACHINE_SCH_MSAllocatedReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.MachineScheduling objMS = new BAL.MachineScheduling();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsAllocatedReport = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsStatus = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    int dateTypeID = 0;
    string dateFilterSign = string.Empty;
    int unitID = 0;
    int statusID = 0;
    string jobNo = string.Empty;
    string tagNo = string.Empty;
    string itemInLOT = string.Empty;
    string allocatedDrawingCode = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HideMessagePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dsReport"] = null;
                HideMessagePanel();

                BindUnit();
                BindStatus();
                GetAssignedItemsList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }


    /// <summary>
    /// Button Clicks
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        GetAssignedItemsList();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvAllocatedReport.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dsReport"];
            ExportToExcel(dt);
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
            else
            {
                ddlCompany.Items.Clear();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatus()
    {
        try
        {
            dsStatus = objMS.GetStatusList();
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_PID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 1;
            }
            else
            {
                ddlStatus.Items.Clear();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetAssignedItemsList()
    {
        try
        {
            fromDate = string.Empty;
            toDate = string.Empty;
            dateTypeID = 0;
            dateFilterSign = string.Empty;
            unitID = 0;
            statusID = 0;
            jobNo = string.Empty;
            tagNo = string.Empty;
            itemInLOT = string.Empty;
            allocatedDrawingCode = string.Empty;

            if (!string.IsNullOrEmpty(txtStartDateSearch.Text))
                fromDate = Convert.ToDateTime(txtStartDateSearch.Text).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtEndDateSearch.Text))
                toDate = Convert.ToDateTime(txtEndDateSearch.Text).ToString("yyyy-MM-dd");

            dateTypeID = Convert.ToInt32(ddlOnWhichDateMainSearch.SelectedValue);
            dateFilterSign = Convert.ToString(ddlSignMainSearch.SelectedValue);

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

            if (!string.IsNullOrEmpty(txtTagNo.Text))
                tagNo = txtTagNo.Text;

            if (!string.IsNullOrEmpty(txtItemNameInLOT.Text))
                itemInLOT = txtItemNameInLOT.Text;

            if (!string.IsNullOrEmpty(txtAllocatedCode.Text))
                allocatedDrawingCode = txtAllocatedCode.Text;

            dsAllocatedReport = objMS.GetMachineAllocatedDrawingReport(fromDate, toDate, dateTypeID, dateFilterSign, unitID, statusID
                                                                        , jobNo
                                                                        , tagNo, itemInLOT, allocatedDrawingCode);

            if (dsAllocatedReport.Tables.Count > 0 && dsAllocatedReport.Tables[0].Rows.Count > 0)
            {
                Session["dsReport"] = dsAllocatedReport.Tables[0];
                gvAllocatedReport.DataSource = dsAllocatedReport.Tables[0];
                gvAllocatedReport.DataBind();
            }
            else
            {
                gvAllocatedReport.DataSource = null;
                gvAllocatedReport.DataBind();
                Session["dsReport"] = null;
                ExceptionMessage("No datat found...!!!");
            }

            lblRecords.Text = "Records[" + gvAllocatedReport.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;


                    rowTxt = rowTxt.Replace(',', ' ');
                    rowTxt = rowTxt.TrimEnd('\r', ' ');
                    rowTxt = rowTxt.TrimEnd('\n', ' ');
                    rowTxt = rowTxt.Replace('\r', ' ');
                    rowTxt = rowTxt.Replace('\n', ' ');

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "Allocated_To_Machine_Shop_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }


    private void HideMessagePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }


    #endregion

}