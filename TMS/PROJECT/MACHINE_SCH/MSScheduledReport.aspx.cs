using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MACHINE_SCH_MSScheduledReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.MachineScheduling objMS = new BAL.MachineScheduling();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsUnit = new DataSet();
    DataSet dsStatus = new DataSet();
    DataSet dsMachines = new DataSet();
    DataSet dsAssignedItemsList = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    int dateTypeID = 0;
    string dateFilterSign = string.Empty;
    int unitID = 0;
    string jobNo = string.Empty;
    string tagNo = string.Empty;
    string itemInLOT = string.Empty;
    string allocatedDrawingCode = string.Empty;
    int statusID = 0;
    string LOTNo = string.Empty;
    string drawingNo = string.Empty;
    string equipment = string.Empty;
    int machineID = 0;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HideMessagePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dsAssignedItemsList"] = null;
                HideMessagePanel();

                BindUnit();
                BindMahines();
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
        if (gvSubitemsList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dsAssignedItemsList"];
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
                ddlStatus.SelectedIndex = 0;
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

    private void BindMahines()
    {
        try
        {
            dsMachines = objMS.GetMachines("");
            if (dsMachines.Tables.Count > 0 && dsMachines.Tables[0].Rows.Count > 0)
            {
                ddlMachines.DataSource = dsMachines.Tables[0];
                ddlMachines.DataTextField = "MACHINE_NAME";
                ddlMachines.DataValueField = "MACHINE_PID";
                ddlMachines.DataBind();
                ddlMachines.Items.Insert(0, "All");
                ddlMachines.SelectedIndex = 0;
            }
            else
            {
                ddlMachines.Items.Clear();
                ddlMachines.Items.Insert(0, "All");
                ddlMachines.SelectedIndex = 0;
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
            jobNo = string.Empty;
            tagNo = string.Empty;
            itemInLOT = string.Empty;
            allocatedDrawingCode = string.Empty;
            statusID = 0;
            LOTNo = string.Empty;
            drawingNo = string.Empty;
            equipment = string.Empty;
            machineID = 0;


            if (!string.IsNullOrEmpty(txtStartDateSearch.Text))
                fromDate = Convert.ToDateTime(txtStartDateSearch.Text).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtEndDateSearch.Text))
                toDate = Convert.ToDateTime(txtEndDateSearch.Text).ToString("yyyy-MM-dd");

            dateTypeID = Convert.ToInt32(ddlOnWhichDateMainSearch.SelectedValue);
            dateFilterSign = Convert.ToString(ddlSignMainSearch.SelectedValue);

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

            if (!string.IsNullOrEmpty(txtTagNo.Text))
                tagNo = txtTagNo.Text;

            if (!string.IsNullOrEmpty(txtItemNameInLOT.Text))
                itemInLOT = txtItemNameInLOT.Text;

            if (!string.IsNullOrEmpty(txtAllocatedCode.Text))
                allocatedDrawingCode = txtAllocatedCode.Text;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);

            if (!string.IsNullOrEmpty(txtLOTNo.Text))
                LOTNo = txtLOTNo.Text;

            if (!string.IsNullOrEmpty(txtDrawingNo.Text))
                drawingNo = txtDrawingNo.Text;

            if (!string.IsNullOrEmpty(txtEquipment.Text))
                equipment = txtEquipment.Text;

            if (ddlMachines.SelectedIndex > 0)
                machineID = Convert.ToInt32(ddlMachines.SelectedValue);


            dsAssignedItemsList = objMS.GetMachineSchedulingReport(fromDate
                                                                    , toDate
                                                                    , dateTypeID
                                                                    , dateFilterSign
                                                                    , unitID
                                                                    , jobNo
                                                                    , tagNo
                                                                    , itemInLOT
                                                                    , allocatedDrawingCode
                                                                    , statusID
                                                                    , LOTNo
                                                                    , drawingNo
                                                                    , equipment
                                                                    , machineID);
            if (dsAssignedItemsList.Tables.Count > 0 && dsAssignedItemsList.Tables[0].Rows.Count > 0)
            {
                Session["dsAssignedItemsList"] = dsAssignedItemsList.Tables[0];
                gvSubitemsList.DataSource = dsAssignedItemsList.Tables[0];
                gvSubitemsList.DataBind();
            }
            else
            {
                gvSubitemsList.DataSource = null;
                gvSubitemsList.DataBind();
                Session["dsAssignedItemsList"] = null;
                ExceptionMessage("No datat found...!!!");
            }

            lblRecords.Text = "Records[" + gvSubitemsList.Rows.Count + "]";
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


            string fileName = "Machine_Scheduling_" + DateTime.Now.ToString("dd_MMM_yyyy");
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