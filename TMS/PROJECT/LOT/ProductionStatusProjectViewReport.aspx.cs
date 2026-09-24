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

public partial class PROJECT_LOT_ProductionStatusProjectViewReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();
    BAL.MachineScheduling objMS = new BAL.MachineScheduling();

    DataSet dsFabricationList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsProdManager = new DataSet();

    int datetTypeID = 0;
    string dateSign = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string LOTNo = string.Empty;
    string jobNo = string.Empty;
    string productionOrderNo = string.Empty;
    string drawingNo = string.Empty;
    string productCode = string.Empty;
    string equipment = string.Empty;
    string unitName = string.Empty;
    int unitID = 0;
    int percentageOfWork = 0;
    string postingStatus = string.Empty;
    int productionManagerID = 0;
    int outstandingEDDateFlag = 0;
    int empRecordId = 0;
    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdPostingConfirmValue.Value = "0";
                hdDeletionConfirmValue.Value = "0";

                Session["dsFabricationList"] = null;

                ddlPostingStatus.SelectedIndex = 1;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;

                BindUnit();

                BindProductionManager(0);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetFabricationList();
    }

    protected void chkOutstandingEDDate_CheckedChanged(object sender, EventArgs e)
    {
        GetFabricationList();
    }

    protected void gvFabricationList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblProductionOrderNo = (Label)e.Row.FindControl("lblProductionOrderNo");
                Label lblProductCode = (Label)e.Row.FindControl("lblProductCode");
                Label lblEquipment = (Label)e.Row.FindControl("lblEquipment");

                TextBox txtSRNo = (TextBox)e.Row.FindControl("txtSRNo");
                TextBox txtLastPercentageOfWorkDone = (TextBox)e.Row.FindControl("txtLastPercentageOfWorkDone");

                TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                TextBox txtInFitupInspQuantity = (TextBox)e.Row.FindControl("txtInFitupInspQuantity");
                TextBox txtPresentStatus = (TextBox)e.Row.FindControl("txtPresentStatus");
                TextBox txtPresentPercentageOfWorkDone = (TextBox)e.Row.FindControl("txtPresentPercentageOfWorkDone");

                Label lblProductionOrderDeliveryDate = (Label)e.Row.FindControl("lblProductionOrderDeliveryDate");
                Label lblEdOfInspection = (Label)e.Row.FindControl("lblEdOfInspection");


                e.Row.ToolTip = Convert.ToString(lblProductionOrderNo.Text);
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");


                if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDeliveryDate.Text)) && !string.IsNullOrEmpty(Convert.ToString(lblEdOfInspection.Text)))
                {
                    if (Convert.ToDateTime(lblProductionOrderDeliveryDate.Text) < Convert.ToDateTime(lblEdOfInspection.Text))
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                        }
                    }
                }
            }

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


    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvFabricationList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dsFabricationList"];
            ExportToExcel(dt);
        }
    }

    #endregion


    #region METHODS[=========================]

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnit.DataSource = dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");
                ddlUnit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindProductionManager(int unitID)
    {
        try
        {

            dsProdManager = objMS.GetProductionManagers(unitID);
            if (dsProdManager.Tables.Count > 0 && dsProdManager.Tables[0].Rows.Count > 0)
            {
                ddlProductionManager.DataSource = dsProdManager.Tables[0];
                ddlProductionManager.DataTextField = "MANAGER_NAME";
                ddlProductionManager.DataValueField = "MANAGER_ID";
                ddlProductionManager.DataBind();
                ddlProductionManager.Items.Insert(0, "All");
                ddlProductionManager.SelectedIndex = 0;
            }
            else
            {
                ddlProductionManager.Items.Clear();
                ddlProductionManager.Items.Insert(0, "All");
                ddlProductionManager.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void GetFabricationList()
    {
        try
        {
            datetTypeID = 0;
            dateSign = string.Empty;
            fromDate = string.Empty;
            toDate = string.Empty;
            LOTNo = string.Empty;
            jobNo = string.Empty;
            productionOrderNo = string.Empty;
            drawingNo = string.Empty;
            productCode = string.Empty;
            equipment = string.Empty;
            unitName = string.Empty;
            unitID = 0;
            percentageOfWork = 0;
            postingStatus = string.Empty;

            //productionManagerID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            if (ddlProductionManager.SelectedIndex > 0)
                productionManagerID = Convert.ToInt32(ddlProductionManager.SelectedValue);

            outstandingEDDateFlag = 0;
            empRecordId = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            datetTypeID = Convert.ToInt32(ddlOnWhichDate.SelectedValue);
            dateSign = Convert.ToString(ddlSign.SelectedValue);



            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtLOTNo.Text))
                LOTNo = txtLOTNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtProductionOrderNo.Text))
                productionOrderNo = txtProductionOrderNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtDrawingNo.Text))
                drawingNo = txtDrawingNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtProductCode.Text))
                productCode = txtProductCode.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtEquipment.Text))
                equipment = txtEquipment.Text.Trim();

            if (ddlUnit.SelectedIndex > 0)
            {
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);
                unitName = Convert.ToString(ddlUnit.SelectedItem.Text);
            }

            if (!string.IsNullOrEmpty(txtPresentPercOfWorkDone.Text))
                percentageOfWork = Convert.ToInt32(txtPresentPercOfWorkDone.Text);

            if (ddlPostingStatus.SelectedIndex > 0)
            {
                if (ddlPostingStatus.SelectedValue == "Open")
                    postingStatus = "O";
                else if (ddlPostingStatus.SelectedValue == "Close")
                    postingStatus = "C";
            }

            if (chkOutstandingEDDate.Checked)
                outstandingEDDateFlag = 1;
            else outstandingEDDateFlag = 0;


            dsFabricationList = objProject.GetFabricationListForPosting(datetTypeID, dateSign, fromDate, toDate, LOTNo, jobNo, productionOrderNo, drawingNo, productCode, equipment,
                                                                        unitName, unitID, percentageOfWork, postingStatus, productionManagerID
                                                                        , Convert.ToInt32(LOTAllStatusAndTypes.EnumProductionStatusType.ProjectView)
                                                                        , outstandingEDDateFlag
                                                                        , empRecordId,0);//GetFabricationPostedReport

            if (dsFabricationList.Tables.Count > 0 && dsFabricationList.Tables[0].Rows.Count > 0)
            {
                Session["dsFabricationList"] = dsFabricationList.Tables[0];
                gvFabricationList.DataSource = dsFabricationList.Tables[0];
                gvFabricationList.DataBind();
            }
            else
            {
                Session["dsFabricationList"] = null;
                gvFabricationList.DataSource = null;
                gvFabricationList.DataBind();
            }
            lblRecords.Text = "Records[" + dsFabricationList.Tables[0].Rows.Count + "]";
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

            for (int i = 3; i < dt.Columns.Count - 1; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 3; k < dt.Columns.Count - 1; k++)
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


            string fileName = "Production_Status_Project_View_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion    
}
