using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PROJECT_LOT_ProductionStatusReport : System.Web.UI.Page
{
    public enum EnumPostingType : int
    {
        PostingProduction = 1,
        PostingPlanningRemarks = 2
    }


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

    string fileName = string.Empty;
    string body = string.Empty;
    string subject = string.Empty;
    string from = string.Empty;
    string to = string.Empty;
    string toName = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;
    string mailSentDate = string.Empty;
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

                Session["dtUnitList"] = null;
                Session["dsFabricationList"] = null;

                ddlPostingStatus.SelectedIndex = 1;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;

                BindUnit();

                BindProductionManager(0);

                GetFabricationList();

                btnPostPlanningRemarks.Visible = false;
                btnPost.Visible = true;
                if (dsFabricationList.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsFabricationList.Tables[2].Rows)
                    {
                        if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(dr["EMP_RECORD_ID"]))
                        {
                            btnPostPlanningRemarks.Visible = true;
                            if (Convert.ToString(dr["TYPE"]) == "U")
                            {
                                btnPost.Visible = false;
                            }
                        }
                    }
                }
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    //protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    //if (ddlUnit.SelectedIndex > 0)
    //    //{
    //    //    BindProductionManager(Convert.ToInt32(ddlUnit.SelectedValue));
    //    //}
    //    //else
    //    //{
    //    //    BindProductionManager(0);
    //    //}

    //    //GetFabricationList();
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
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
                                
                //TextBox txtRemarksForProcurement = (TextBox)e.Row.FindControl("txtRemarksForProcurement");

                Label lblProductionOrderDeliveryDate = (Label)e.Row.FindControl("lblProductionOrderDeliveryDate");
                TextBox txtEdOfInspection = (TextBox)e.Row.FindControl("txtEdOfInspection");
                ImageButton imgbtnEdOfInspection = (ImageButton)e.Row.FindControl("imgbtnEdOfInspection");
                TextBox txtPlanningRemarks = (TextBox)e.Row.FindControl("txtPlanningRemarks");

                txtPlanningRemarks.Enabled = false;
                txtPlanningRemarks.BackColor = System.Drawing.Color.LightYellow;

                txtPresentStatus.Enabled = false;
                txtPresentPercentageOfWorkDone.Enabled = false;
                txtEdOfInspection.Enabled = false;
                imgbtnEdOfInspection.Visible = false;

                 //txtRemarksForProcurement.Enabled = false;

                int count = 0;
                if (dsFabricationList.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow item in dsFabricationList.Tables[2].Rows)
                    {
                        if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(item["EMP_RECORD_ID"]))
                        {
                            if (Convert.ToString(item["TYPE"]) == "U")
                            {
                                count++;
                                break;
                            }
                            else if (Convert.ToString(item["TYPE"]) == "A")
                            {
                                count = -1;
                                break;
                            }
                        }
                    }
                }

                if (count < 0)
                {
                    txtPlanningRemarks.Enabled = true;
                    txtPlanningRemarks.BackColor = System.Drawing.Color.LightGreen;

                    txtPresentStatus.Enabled = true;
                    txtPresentPercentageOfWorkDone.Enabled = true;
                    txtEdOfInspection.Enabled = true;
                    imgbtnEdOfInspection.Visible = true;
                    //txtRemarksForProcurement.Enabled = true;
                }
                else if (count == 0)
                {
                    txtPlanningRemarks.Enabled = false;
                    txtPlanningRemarks.BackColor = System.Drawing.Color.LightYellow;

                    txtPresentStatus.Enabled = true;
                    txtPresentPercentageOfWorkDone.Enabled = true;
                    txtEdOfInspection.Enabled = true;
                    imgbtnEdOfInspection.Visible = true;
                    //txtRemarksForProcurement.Enabled = true;
                }
                else if (count > 0)
                {
                    txtPlanningRemarks.Enabled = true;
                    txtPlanningRemarks.BackColor = System.Drawing.Color.LightGreen;

                    txtPresentStatus.Enabled = false;
                    txtPresentPercentageOfWorkDone.Enabled = false;
                    txtEdOfInspection.Enabled = false;
                    imgbtnEdOfInspection.Visible = false;
                    //txtRemarksForProcurement.Enabled = true;
                }


                e.Row.ToolTip = Convert.ToString(lblProductionOrderNo.Text);
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");

                if (Convert.ToInt32(lblRecordID.Text) > 0)
                {
                    txtSRNo.BackColor = System.Drawing.Color.LightGreen;
                    txtLastPercentageOfWorkDone.BackColor = System.Drawing.Color.LightGreen;
                    txtQuantity.BackColor = System.Drawing.Color.LightGreen;
                    txtInFitupInspQuantity.BackColor = System.Drawing.Color.LightGreen;
                    txtPresentStatus.BackColor = System.Drawing.Color.LightGreen;
                    txtPresentPercentageOfWorkDone.BackColor = System.Drawing.Color.LightGreen;
                    txtEdOfInspection.BackColor = System.Drawing.Color.LightGreen;
                    //txtRemarksForProcurement.BackColor = System.Drawing.Color.LightGreen;

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                else
                {
                    txtSRNo.BackColor = System.Drawing.Color.LightYellow;
                    txtLastPercentageOfWorkDone.BackColor = System.Drawing.Color.LightYellow;
                    txtQuantity.BackColor = System.Drawing.Color.LightYellow;
                    txtInFitupInspQuantity.BackColor = System.Drawing.Color.LightYellow;
                    txtPresentStatus.BackColor = System.Drawing.Color.LightYellow;
                    //txtPresentPercentageOfWorkDone.BackColor = System.Drawing.Color.LightYellow;
                    txtEdOfInspection.BackColor = System.Drawing.Color.LightYellow;
                    //txtRemarksForProcurement.BackColor = System.Drawing.Color.LightYellow;

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightYellow;
                    }
                }


                if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDeliveryDate.Text)) && !string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text)))
                {
                    if (Convert.ToDateTime(lblProductionOrderDeliveryDate.Text) < Convert.ToDateTime(txtEdOfInspection.Text))
                    {
                        txtSRNo.BackColor = System.Drawing.Color.LightPink;
                        txtLastPercentageOfWorkDone.BackColor = System.Drawing.Color.LightPink;
                        txtQuantity.BackColor = System.Drawing.Color.LightPink;
                        txtInFitupInspQuantity.BackColor = System.Drawing.Color.LightPink;
                        txtPresentStatus.BackColor = System.Drawing.Color.LightPink;
                        txtEdOfInspection.BackColor = System.Drawing.Color.LightPink;
                        //txtRemarksForProcurement.BackColor = System.Drawing.Color.LightPink;

                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                        }
                    }
                }


                if (!string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text)))
                {
                    if (DateTime.Now.Date > Convert.ToDateTime(txtEdOfInspection.Text).Date)
                    {
                        txtEdOfInspection.BackColor = System.Drawing.Color.Red;
                        txtEdOfInspection.ForeColor = System.Drawing.Color.White;
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

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (gvFabricationList.Rows.Count > 0)
        {
            if (chkSelectAll.Checked)
            {
                foreach (GridViewRow gr in gvFabricationList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    chkSelect.Checked = true;
                }
            }
            if (!chkSelectAll.Checked)
            {
                foreach (GridViewRow gr in gvFabricationList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    chkSelect.Checked = false;
                }
            }
        }
    }

    protected void chkOutstandingEDDate_CheckedChanged(object sender, EventArgs e)
    {
        GetFabricationList();
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdPostingConfirmValue.Value) > 0)
        {

            DataTable dtUnitList = new DataTable();
            DataTable dtPMList = new DataTable();

            if (Session["dtPMList"] != null)
            {
                dtPMList = (DataTable)Session["dtPMList"];
            }

            if (Session["dtUnitList"] != null)
            {
                dtUnitList = (DataTable)Session["dtUnitList"];
            }

            PostFabricationList(dtPMList, dtUnitList, (int)EnumPostingType.PostingProduction);
        }
    }

    protected void btnPostPlanningRemarks_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdPostingPlanningRemarksConfirmValue.Value) > 0)
        {
            DataTable dtUnitList = new DataTable();
            DataTable dtPMList = new DataTable();

            if (Session["dtPMList"] != null)
            {
                dtPMList = (DataTable)Session["dtPMList"];
            }

            if (Session["dtUnitList"] != null)
            {
                dtUnitList = (DataTable)Session["dtUnitList"];
            }

            PostFabricationList(dtPMList, dtUnitList, (int)EnumPostingType.PostingPlanningRemarks);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdDeletionConfirmValue.Value) > 0)
        {
            DeleteFabrication();
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
                Session["dtUnitList"] = dsUnit.Tables[0];
                ddlUnit.DataSource = dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");
                ddlUnit.SelectedIndex = 0;


                //if (Convert.ToString(Session["USER_TYPE"]) == "A")
                //{
                //    ddlUnit.Enabled = true;
                //}
                //else
                //{
                //    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 89 ||
                //        Convert.ToInt32(Session["EMP_RECORD_ID"]) == 281)
                //    {
                //        ddlUnit.Enabled = true;
                //    }
                //    else
                //    {
                //        ddlUnit.SelectedValue = Convert.ToString(Session["UNIT_ID"]);
                //        ddlUnit.Enabled = false;
                //    }
                //}

            }
            else
            {
                Session["dtUnitList"] = null;
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


            if (Convert.ToString(Session["USER_TYPE"]) == "A")
            {
                ddlProductionManager.Enabled = true;
            }
            else
            {
                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 89 ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 281)
                {
                    ddlProductionManager.Enabled = true;
                }
                else if(Convert.ToInt32(Session["EMP_RECORD_ID"]) == 486) //tanmay
                {
                    ddlProductionManager.SelectedValue = Convert.ToString(83);//tarun
                    ddlProductionManager.Enabled = false;
                }
                else
                {
                    ddlProductionManager.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                    ddlProductionManager.Enabled = false;
                }
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
            empRecordId = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            //productionManagerID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            if (ddlProductionManager.SelectedIndex > 0)
                productionManagerID = Convert.ToInt32(ddlProductionManager.SelectedValue);

            outstandingEDDateFlag = 0;
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

            //fromDate = "";
            //toDate = "";
            //LOTNo = "OSI2021002-GNU-0003";

            dsFabricationList = objProject.GetFabricationListForPosting(datetTypeID, dateSign, fromDate, toDate, LOTNo, jobNo, productionOrderNo, drawingNo, productCode, equipment,
                                                                        unitName, unitID, percentageOfWork, postingStatus, productionManagerID
                                                                        , Convert.ToInt32(LOTAllStatusAndTypes.EnumProductionStatusType.ProductionView)
                                                                        , outstandingEDDateFlag
                                                                        , empRecordId, 0);

            if (dsFabricationList.Tables.Count > 0 && dsFabricationList.Tables[0].Rows.Count > 0)
            {
                Session["dsFabricationList"] = dsFabricationList.Tables[0];
                gvFabricationList.DataSource = dsFabricationList.Tables[0];

                if (dsFabricationList.Tables.Count > 0 && dsFabricationList.Tables[1].Rows.Count > 0)
                {
                    Session["dtPMList"] = dsFabricationList.Tables[1];
                }

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

            for (int i = 2; i < dt.Columns.Count - 1; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 2; k < dt.Columns.Count - 1; k++)
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


            string fileName = "Production_Status-Production View_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void PostFabricationList(DataTable dtPMList, DataTable dtUnitList, int postingType)
    {
        try
        {

            #region Datatables

            DataTable dtPostedSrNo = new DataTable();
            dtPostedSrNo.Columns.Add("SR_NO", typeof(int));

            DataTable dtTempForEmail = new DataTable();
            dtTempForEmail.Columns.Add("SR_NO", typeof(int));
            dtTempForEmail.Columns.Add("JOB_NO", typeof(string));
            dtTempForEmail.Columns.Add("SHORT_JOB_NO", typeof(string));
            dtTempForEmail.Columns.Add("LOT_NO", typeof(string));
            dtTempForEmail.Columns.Add("LOT_DATE", typeof(string));
            dtTempForEmail.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtTempForEmail.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
            dtTempForEmail.Columns.Add("PRODUCTION_ORDER_DELIVERY_DATE", typeof(string));
            dtTempForEmail.Columns.Add("PRODUCT_CODE", typeof(string));
            dtTempForEmail.Columns.Add("EQUIPMENT_ITEM", typeof(string));
            dtTempForEmail.Columns.Add("UOM", typeof(string));
            dtTempForEmail.Columns.Add("QUANTITY", typeof(double));
            dtTempForEmail.Columns.Add("DRAWING_NO", typeof(string));

            //dtTempForEmail.Columns.Add("REMARKS_FOR_PROCUREMENT", typeof(string));

            dtTempForEmail.Columns.Add("PRESENT_STATUS", typeof(string));
            dtTempForEmail.Columns.Add("PRESENT_PERC_OF_WORK_DONE", typeof(string));
            dtTempForEmail.Columns.Add("ED_OF_INSP_COMP", typeof(string));
            dtTempForEmail.Columns.Add("UNIT", typeof(string));


            DataTable dtTempForInsertion = new DataTable();
            dtTempForInsertion.Columns.Add("JOB_NO", typeof(string));
            dtTempForInsertion.Columns.Add("LOT_TF_ID", typeof(int));
            dtTempForInsertion.Columns.Add("LOT_DATE", typeof(string));
            dtTempForInsertion.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtTempForInsertion.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
            dtTempForInsertion.Columns.Add("PRODUCTION_ORDER_DELIVERY_DATE", typeof(string));
            dtTempForInsertion.Columns.Add("PRODUCT_CODE", typeof(string));
            dtTempForInsertion.Columns.Add("EQUIPMENT", typeof(string));
            dtTempForInsertion.Columns.Add("UOM", typeof(string));
            dtTempForInsertion.Columns.Add("QUANTITY", typeof(double));
            dtTempForInsertion.Columns.Add("DRAWING_NO", typeof(string));

            //dtTempForInsertion.Columns.Add("REMARKS_FOR_PROCUREMENT", typeof(string));

            dtTempForInsertion.Columns.Add("PRESENT_STATUS", typeof(string));
            dtTempForInsertion.Columns.Add("PRESENT_PERC_OF_WORK_DONE", typeof(int));
            dtTempForInsertion.Columns.Add("ED_OF_INSP_COMP", typeof(string));
            dtTempForInsertion.Columns.Add("UNIT", typeof(string));
            dtTempForInsertion.Columns.Add("PLANNING_REMARKS", typeof(string));

            DataTable dtTempLogTable = new DataTable();
            dtTempLogTable.Columns.Add("POSTED_RECORD_ID", typeof(int));
            dtTempLogTable.Columns.Add("OLD_PRESENT_STATUS", typeof(string));
            dtTempLogTable.Columns.Add("OLD_PRESENT_PERC_OF_WORK_DONE", typeof(int));
            dtTempLogTable.Columns.Add("OLD_ED_OF_INSP_COMP", typeof(string));
            dtTempLogTable.Columns.Add("NEW_PRESENT_STATUS", typeof(string));
            dtTempLogTable.Columns.Add("NEW_PRESENT_PERC_OF_WORK_DONE", typeof(int));
            dtTempLogTable.Columns.Add("NEW_ED_OF_INSP_COMP", typeof(string));
            dtTempLogTable.Columns.Add("OLD_PLANNING_REMARKS", typeof(string));
            dtTempLogTable.Columns.Add("NEW_PLANNING_REMARKS", typeof(string));

            //---------------------------------------------------------------------
            //dtTempLogTable.Columns.Add("OLD_REMARKS_FOR_PROCUREMENT", typeof(string));
            //dtTempLogTable.Columns.Add("NEW_REMARKS_FOR_PROCUREMENT", typeof(string));
            //---------------------------------------------------------------------


            #endregion

            int srNo = 0;
            int recordID = 0;
            string jobNo = string.Empty;
            int LOTTFID = 0;
            string LOTDate = string.Empty;
            string productionOrderNo = string.Empty;
            string productionOrderDate = string.Empty;
            string productionOrderDeliveryDate = string.Empty;
            string productCode = string.Empty;
            string equipment = string.Empty;
            string UOM = string.Empty;
            double quantity = 0;
            string drawingNo = string.Empty;

            string remarksForProcurement = string.Empty;

            string presentStatus = string.Empty;
            int presentPercentageOfWork = 0;
            string EDOfInspComp = string.Empty;
            string unit = string.Empty;
            string oldPresentStatus = string.Empty;
            int oldPresentPercentageOfWork = 0;
            string oldEDOfInspComp = string.Empty;
            string planningRemarks = string.Empty;
            string oldPlanningRemarks = string.Empty;

            string oldRemarksForProcurement = string.Empty;

            string tableName = string.Empty;
            string updateQuery = string.Empty;

            int count = 0;

            if (gvFabricationList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvFabricationList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        count++;

                        srNo = 0;
                        recordID = 0;
                        jobNo = string.Empty;
                        LOTTFID = 0;
                        LOTDate = string.Empty;
                        productionOrderNo = string.Empty;
                        productionOrderDate = string.Empty;
                        productionOrderDeliveryDate = string.Empty;
                        productCode = string.Empty;
                        equipment = string.Empty;
                        UOM = string.Empty;
                        quantity = 0;
                        drawingNo = string.Empty;

                        remarksForProcurement = string.Empty;

                        presentStatus = string.Empty;
                        presentPercentageOfWork = 0;
                        EDOfInspComp = string.Empty;
                        unit = string.Empty;
                        oldPresentStatus = string.Empty;
                        oldPresentPercentageOfWork = 0;
                        oldEDOfInspComp = string.Empty;
                        planningRemarks = string.Empty;
                        oldPlanningRemarks = string.Empty;
                        oldRemarksForProcurement = string.Empty;


                        TextBox txtSRNo = (TextBox)gr.FindControl("txtSRNo");
                        Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                        Label lblTableName = (Label)gr.FindControl("lblTableName");
                        Label lblUnit = (Label)gr.FindControl("lblUnit");
                        Label lblJOBNo = (Label)gr.FindControl("lblJOBNo");
                        Label lblLOTTFID = (Label)gr.FindControl("lblLOTTFID");
                        Label lblLOTDate = (Label)gr.FindControl("lblLOTDate");
                        Label lblProductionOrderNo = (Label)gr.FindControl("lblProductionOrderNo");
                        Label lblProductionOrderDate = (Label)gr.FindControl("lblProductionOrderDate");
                        Label lblProductionOrderDeliveryDate = (Label)gr.FindControl("lblProductionOrderDeliveryDate");
                        Label lblProductCode = (Label)gr.FindControl("lblProductCode");
                        Label lblEquipment = (Label)gr.FindControl("lblEquipment");
                        Label lblUOM = (Label)gr.FindControl("lblUOM");
                        TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                        Label lblDrawingNo = (Label)gr.FindControl("lblDrawingNo");

                        //Label lblRemarksForProcurement = (Label)gr.FindControl("lblRemarksForProcurement");
                        //TextBox txtRemarksForProcurement = (TextBox)gr.FindControl("txtRemarksForProcurement");

                        Label lblPresentStatus = (Label)gr.FindControl("lblPresentStatus");
                        TextBox txtPresentStatus = (TextBox)gr.FindControl("txtPresentStatus");
                        Label lblPresentPercentageOfWorkDone = (Label)gr.FindControl("lblPresentPercentageOfWorkDone");
                        TextBox txtPresentPercentageOfWorkDone = (TextBox)gr.FindControl("txtPresentPercentageOfWorkDone");
                        Label lblEdOfInspection = (Label)gr.FindControl("lblEdOfInspection");
                        TextBox txtEdOfInspection = (TextBox)gr.FindControl("txtEdOfInspection");
                        TextBox txtLastStatus = (TextBox)gr.FindControl("txtLastStatus");
                        Label lblLastPercentageOfWorkDone = (Label)gr.FindControl("lblLastPercentageOfWorkDone");
                        TextBox txtLastPercentageOfWorkDone = (TextBox)gr.FindControl("txtLastPercentageOfWorkDone");
                        Label lblLastEdOfInspection = (Label)gr.FindControl("lblLastEdOfInspection");

                        Label lblPlanningRemarks = (Label)gr.FindControl("lblPlanningRemarks");
                        TextBox txtPlanningRemarks = (TextBox)gr.FindControl("txtPlanningRemarks");


                        if (!string.IsNullOrEmpty(Convert.ToString(txtSRNo.Text)) && Convert.ToInt32(txtSRNo.Text) > 0)
                            srNo = Convert.ToInt32(txtSRNo.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)) && Convert.ToInt32(lblRecordID.Text) > 0)
                            recordID = Convert.ToInt32(lblRecordID.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text).Trim()))
                            jobNo = Convert.ToString(lblJOBNo.Text).Trim().ToUpper();

                        if (!string.IsNullOrEmpty(Convert.ToString(lblLOTTFID.Text)) && Convert.ToInt32(lblLOTTFID.Text) > 0)
                            LOTTFID = Convert.ToInt32(lblLOTTFID.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblLOTDate.Text).Trim()))
                            LOTDate = Convert.ToDateTime(lblLOTDate.Text).ToString("yyyy-MM-dd");

                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderNo.Text).Trim()))
                            productionOrderNo = Convert.ToString(lblProductionOrderNo.Text).Trim().ToUpper();

                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDate.Text).Trim()))
                            productionOrderDate = Convert.ToDateTime(lblProductionOrderDate.Text).ToString("yyyy-MM-dd");

                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDeliveryDate.Text).Trim()))
                            productionOrderDeliveryDate = Convert.ToDateTime(lblProductionOrderDeliveryDate.Text).ToString("yyyy-MM-dd");

                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text).Trim()))
                            productCode = Convert.ToString(lblProductCode.Text).Trim().ToUpper();

                        if (!string.IsNullOrEmpty(Convert.ToString(lblEquipment.Text).Trim()))
                            equipment = Convert.ToString(lblEquipment.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text).Trim()))
                            UOM = Convert.ToString(lblUOM.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(txtQuantity.Text).Trim()))
                            quantity = Convert.ToDouble(txtQuantity.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text).Trim()))
                            drawingNo = Convert.ToString(lblDrawingNo.Text).Trim().ToUpper();



                        if (!string.IsNullOrEmpty(Convert.ToString(lblPresentStatus.Text).Trim()))
                            oldPresentStatus = Convert.ToString(lblPresentStatus.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblEdOfInspection.Text).Trim()))
                            oldEDOfInspComp = Convert.ToString(lblEdOfInspection.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPresentPercentageOfWorkDone.Text)))
                            oldPresentPercentageOfWork = Convert.ToInt32(lblPresentPercentageOfWorkDone.Text);


                        //-----------------------------------------------------------

                        //if (!string.IsNullOrEmpty(Convert.ToString(txtRemarksForProcurement.Text).Trim()))
                        //    remarksForProcurement = Convert.ToString(txtRemarksForProcurement.Text).Replace("'", " ").Replace("\"", " ").Replace("''", " ");
                        //else
                        //{
                        //    if (postingType == (int)EnumPostingType.PostingProduction)
                        //    {
                        //        ExceptionMessage("Please enter remarks for procurement...!!!");
                        //        return;
                        //    }
                        //}

                        // if (!string.IsNullOrEmpty(Convert.ToString(lblRemarksForProcurement.Text).Trim()))
                        // oldRemarksForProcurement = Convert.ToString(lblRemarksForProcurement.Text);

                        //-----------------------------------------------------------


                        if (!string.IsNullOrEmpty(Convert.ToString(txtPresentStatus.Text).Trim()))
                            presentStatus = Convert.ToString(txtPresentStatus.Text).Replace("'", " ").Replace("\"", " ").Replace("''", " ");
                        else
                        {
                            if (postingType == (int)EnumPostingType.PostingProduction)
                            {
                                ExceptionMessage("Please enter present status...!!!");
                                return;
                            }
                        }


                        if (!string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text)))
                        {
                            EDOfInspComp = Convert.ToDateTime(txtEdOfInspection.Text).ToString("yyyy-MM-dd");
                            //if (!string.IsNullOrEmpty(oldEDOfInspComp))
                            //{
                            //    if (Convert.ToDateTime(EDOfInspComp).Date < Convert.ToDateTime(oldEDOfInspComp).Date)
                            //    {
                            //        ExceptionMessage("Present ED of Insp/Comp date must be greater or equal to last ED of Insp/Comp date...!!!");
                            //        return;
                            //    }
                            //}
                        }
                        else
                        {
                            if (postingType == (int)EnumPostingType.PostingProduction)
                            {
                                ExceptionMessage("Please enter ED of Insp/Comp date...!!!");
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(txtPresentPercentageOfWorkDone.Text)))
                            presentPercentageOfWork = Convert.ToInt32(txtPresentPercentageOfWorkDone.Text);


                        if (!string.IsNullOrEmpty(Convert.ToString(lblUnit.Text).Trim()))
                            unit = Convert.ToString(lblUnit.Text).Trim().ToUpper();

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPlanningRemarks.Text).Trim()))
                            oldPlanningRemarks = Convert.ToString(lblPlanningRemarks.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(txtPlanningRemarks.Text).Trim()))
                            planningRemarks = Convert.ToString(txtPlanningRemarks.Text).Replace("'", " ").Replace("\"", " ").Replace("''", " ");
                        else
                        {
                            if (postingType == (int)EnumPostingType.PostingPlanningRemarks)
                            {
                                ExceptionMessage("Please enter planning remarks...!!!");
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(lblTableName.Text).Trim()))
                            tableName = Convert.ToString(lblTableName.Text).Trim();
                        else
                            tableName = string.Empty;

                        if (recordID > 0)
                        {
                            //insert into log table

                            DataRow drn2 = dtTempLogTable.NewRow();
                            drn2["POSTED_RECORD_ID"] = recordID;
                            drn2["OLD_PRESENT_STATUS"] = oldPresentStatus;
                            drn2["OLD_PRESENT_PERC_OF_WORK_DONE"] = oldPresentPercentageOfWork;
                            drn2["OLD_ED_OF_INSP_COMP"] = oldEDOfInspComp;
                            drn2["NEW_PRESENT_STATUS"] = presentStatus;
                            drn2["NEW_PRESENT_PERC_OF_WORK_DONE"] = presentPercentageOfWork;
                            drn2["NEW_ED_OF_INSP_COMP"] = EDOfInspComp;
                            drn2["OLD_PLANNING_REMARKS"] = oldPlanningRemarks;
                            drn2["NEW_PLANNING_REMARKS"] = planningRemarks;

                            //drn2["OLD_REMARKS_FOR_PROCUREMENT"] = oldRemarksForProcurement;
                            //drn2["NEW_REMARKS_FOR_PROCUREMENT"] = remarksForProcurement;

                            dtTempLogTable.Rows.Add(drn2);

                            if (postingType == (int)EnumPostingType.PostingProduction)
                            {
                                //update main table
                                updateQuery += "UPDATE " + tableName + " SET PRESENT_STATUS='" + presentStatus + "', " +
                                                                            "PRESENT_PERC_OF_WORK_DONE='" + presentPercentageOfWork + "', " +
                                                                            "ED_OF_INSP_COMP='" + EDOfInspComp + "', " +
                                                                            //"REMARKS_FOR_PROCUREMENT='" + remarksForProcurement + "', " +
                                                                            "MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", " +
                                                                            "MODIFIED_ON=GETDATE() " +
                                                                            "WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;
                            }

                            else if (postingType == (int)EnumPostingType.PostingPlanningRemarks)
                            {
                                updateQuery += "UPDATE " + tableName + " SET PLANNING_REMARKS='" + planningRemarks + "', " +
                                                                            "MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", " +
                                                                            "MODIFIED_ON=GETDATE() " +
                                                                            "WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;
                            }
                            DataRow drp = dtPostedSrNo.NewRow();
                            drp["SR_NO"] = srNo;
                            dtPostedSrNo.Rows.Add(drp);
                        }
                        else
                        {
                            DataRow drn1 = dtTempForInsertion.NewRow();

                            drn1["JOB_NO"] = jobNo;
                            drn1["LOT_TF_ID"] = LOTTFID;
                            drn1["LOT_DATE"] = LOTDate;
                            drn1["PRODUCTION_ORDER_NO"] = productionOrderNo;
                            drn1["PRODUCTION_ORDER_DATE"] = productionOrderDate;
                            drn1["PRODUCTION_ORDER_DELIVERY_DATE"] = productionOrderDeliveryDate;
                            drn1["PRODUCT_CODE"] = productCode;
                            drn1["EQUIPMENT"] = equipment;
                            drn1["UOM"] = UOM;
                            drn1["QUANTITY"] = quantity;
                            drn1["DRAWING_NO"] = drawingNo;
                            /*drn1["REMARKS_FOR_PROCUREMENT"] = remarksForProcurement;*/
                            drn1["PRESENT_STATUS"] = presentStatus;
                            drn1["PRESENT_PERC_OF_WORK_DONE"] = presentPercentageOfWork;
                            drn1["ED_OF_INSP_COMP"] = EDOfInspComp;
                            drn1["UNIT"] = unit;
                            drn1["PLANNING_REMARKS"] = planningRemarks;

                            dtTempForInsertion.Rows.Add(drn1);

                            DataRow drp = dtPostedSrNo.NewRow();
                            drp["SR_NO"] = srNo;
                            dtPostedSrNo.Rows.Add(drp);
                        }
                    }
                }


                int srCount = 0;

                if (count > 0)
                {
                    if (!string.IsNullOrEmpty(updateQuery))
                        updateQuery = updateQuery.TrimEnd('\n', ' ');

                    int value = objProject.PostFabricationList(dtTempForInsertion, dtTempLogTable, updateQuery
                                                             , Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (value > 0)
                    {
                        if (dtPostedSrNo.Rows.Count > 0 && postingType == (int)EnumPostingType.PostingProduction)
                        {
                            foreach (DataRow drp in dtPostedSrNo.Rows)
                            {
                                foreach (GridViewRow gr in gvFabricationList.Rows)
                                {
                                    TextBox txtSRNo = gr.FindControl("txtSRNo") as TextBox;
                                    Label lblLOTTFID = gr.FindControl("lblLOTTFID") as Label;
                                    Label lblShortJOBNo = gr.FindControl("lblShortJOBNo") as Label;
                                    Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                                    Label lblLOTNo = gr.FindControl("lblLOTNo") as Label;
                                    Label lblLOTDate = gr.FindControl("lblLOTDate") as Label;
                                    Label lblProductionOrderNo = gr.FindControl("lblProductionOrderNo") as Label;
                                    Label lblProductionOrderDate = gr.FindControl("lblProductionOrderDate") as Label;
                                    Label lblProductionOrderDeliveryDate = gr.FindControl("lblProductionOrderDeliveryDate") as Label;
                                    Label lblProductCode = gr.FindControl("lblProductCode") as Label;
                                    Label lblEquipment = gr.FindControl("lblEquipment") as Label;
                                    Label lblUOM = gr.FindControl("lblUOM") as Label;
                                    TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                                    Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;
                                    //--------------------------------------------------------------------------
                                    //TextBox txtRemarksForProcurement = gr.FindControl("txtRemarksForProcurement") as TextBox;
                                    //--------------------------------------------------------------------------
                                    TextBox txtPresentStatus = gr.FindControl("txtPresentStatus") as TextBox;
                                    TextBox txtPresentPercentageOfWorkDone = gr.FindControl("txtPresentPercentageOfWorkDone") as TextBox;
                                    TextBox txtEdOfInspection = gr.FindControl("txtEdOfInspection") as TextBox;
                                    Label lblUnit = gr.FindControl("lblUnit") as Label;


                                    if (Convert.ToInt32(txtSRNo.Text) == Convert.ToInt32(drp["SR_NO"]))
                                    {
                                        if (Convert.ToDateTime(txtEdOfInspection.Text) > Convert.ToDateTime(lblProductionOrderDeliveryDate.Text))
                                        {
                                            srCount++;
                                            DataRow drLOT = dtTempForEmail.NewRow();

                                            drLOT["SR_NO"] = srCount;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblShortJOBNo.Text).Trim()))
                                                drLOT["SHORT_JOB_NO"] = Convert.ToString(lblShortJOBNo.Text).Trim();
                                            else drLOT["SHORT_JOB_NO"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text).Trim()))
                                                drLOT["JOB_NO"] = Convert.ToString(lblJOBNo.Text).Trim();
                                            else drLOT["JOB_NO"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblLOTNo.Text).Trim()))
                                                drLOT["LOT_NO"] = Convert.ToString(lblLOTNo.Text).Trim();
                                            else drLOT["LOT_NO"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblLOTDate.Text).Trim()))
                                                drLOT["LOT_DATE"] = Convert.ToString(lblLOTDate.Text).Trim();
                                            else drLOT["LOT_DATE"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderNo.Text).Trim()))
                                                drLOT["PRODUCTION_ORDER_NO"] = Convert.ToString(lblProductionOrderNo.Text).Trim();
                                            else drLOT["PRODUCTION_ORDER_NO"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDate.Text).Trim()))
                                                drLOT["PRODUCTION_ORDER_DATE"] = Convert.ToString(lblProductionOrderDate.Text).Trim();
                                            else drLOT["PRODUCTION_ORDER_DATE"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDeliveryDate.Text).Trim()))
                                                drLOT["PRODUCTION_ORDER_DELIVERY_DATE"] = Convert.ToString(lblProductionOrderDeliveryDate.Text).Trim();
                                            else drLOT["PRODUCTION_ORDER_DELIVERY_DATE"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text).Trim()))
                                                drLOT["PRODUCT_CODE"] = Convert.ToString(lblProductCode.Text).Trim();
                                            else drLOT["PRODUCT_CODE"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblEquipment.Text).Trim()))
                                                drLOT["EQUIPMENT_ITEM"] = Convert.ToString(lblEquipment.Text).Trim();
                                            else drLOT["EQUIPMENT_ITEM"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text).Trim()))
                                                drLOT["UOM"] = Convert.ToString(lblUOM.Text);
                                            else drLOT["UOM"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtQuantity.Text).Trim()))
                                                drLOT["QUANTITY"] = Convert.ToDouble(txtQuantity.Text);
                                            else drLOT["QUANTITY"] = "0";

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text).Trim()))
                                                drLOT["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text).Trim();
                                            else drLOT["DRAWING_NO"] = string.Empty;

                                            //----------------------------------------------------------------

                                            //if (!string.IsNullOrEmpty(Convert.ToString(txtRemarksForProcurement.Text).Trim()))
                                            //    drLOT["REMARKS_FOR_PROCUREMENT"] = Convert.ToString(txtRemarksForProcurement.Text).Trim();
                                            //else drLOT["REMARKS_FOR_PROCUREMENT"] = string.Empty;

                                            //----------------------------------------------------------------


                                            if (!string.IsNullOrEmpty(Convert.ToString(txtPresentStatus.Text).Trim()))
                                                drLOT["PRESENT_STATUS"] = Convert.ToString(txtPresentStatus.Text).Trim();
                                            else drLOT["PRESENT_STATUS"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtPresentPercentageOfWorkDone.Text).Trim()))
                                                drLOT["PRESENT_PERC_OF_WORK_DONE"] = Convert.ToString(txtPresentPercentageOfWorkDone.Text).Trim();
                                            else drLOT["PRESENT_PERC_OF_WORK_DONE"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text).Trim()))
                                                drLOT["ED_OF_INSP_COMP"] = Convert.ToString(txtEdOfInspection.Text).Trim();
                                            else drLOT["ED_OF_INSP_COMP"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblUnit.Text).Trim()))
                                                drLOT["UNIT"] = Convert.ToString(lblUnit.Text).Trim();
                                            else drLOT["UNIT"] = string.Empty;


                                            dtTempForEmail.Rows.Add(drLOT);
                                        }
                                    }
                                }
                            }
                        }

                        if (dtTempForEmail.Rows.Count > 0)
                        {
                            CreateAttachmentsAndSendMail(dtTempForEmail, dtPMList, dtUnitList);
                        }


                        #region MyRegion

                        //foreach (DataRow dr1 in dtTemp1.Rows)
                        //{
                        //    if (Convert.ToDateTime(dr1["ED_OF_INSP_COMP"]) > Convert.ToDateTime(dr1["PRODUCTION_ORDER_DELIVERY_DATE"]))
                        //    {
                        //        foreach (GridViewRow gr in gvFabricationList.Rows)
                        //        {

                        //            Label lblLOTTFID = gr.FindControl("lblLOTTFID") as Label;
                        //            Label lblShortJOBNo = gr.FindControl("lblShortJOBNo") as Label;
                        //            Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                        //            Label lblLOTNo = gr.FindControl("lblLOTNo") as Label;
                        //            Label lblLOTDate = gr.FindControl("lblLOTDate") as Label;
                        //            Label lblProductionOrderNo = gr.FindControl("lblProductionOrderNo") as Label;
                        //            Label lblProductionOrderDate = gr.FindControl("lblProductionOrderDate") as Label;
                        //            Label lblProductionOrderDeliveryDate = gr.FindControl("lblProductionOrderDeliveryDate") as Label;
                        //            Label lblProductCode = gr.FindControl("lblProductCode") as Label;
                        //            Label lblEquipment = gr.FindControl("lblEquipment") as Label;
                        //            Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;
                        //            TextBox txtPresentStatus = gr.FindControl("txtPresentStatus") as TextBox;
                        //            TextBox txtPresentPercentageOfWorkDone = gr.FindControl("txtPresentPercentageOfWorkDone") as TextBox;
                        //            TextBox txtEdOfInspection = gr.FindControl("txtEdOfInspection") as TextBox;
                        //            Label lblUnit = gr.FindControl("lblUnit") as Label;



                        //            if ((Convert.ToString(lblProductionOrderNo.Text).Trim() == Convert.ToString(dr1["PRODUCTION_ORDER_NO"]).Trim()) &&
                        //                (Convert.ToString(lblProductCode.Text).Trim() == Convert.ToString(dr1["PRODUCT_CODE"]).Trim()))
                        //            {
                        //                DataRow drLOT = dtTempLOTList.NewRow();



                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblShortJOBNo.Text).Trim()))
                        //                    drLOT["SHORT_JOB_NO"] = Convert.ToString(lblShortJOBNo.Text).Trim();
                        //                else drLOT["SHORT_JOB_NO"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text).Trim()))
                        //                    drLOT["JOB_NO"] = Convert.ToString(lblJOBNo.Text).Trim();
                        //                else drLOT["JOB_NO"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblLOTNo.Text).Trim()))
                        //                    drLOT["LOT_NO"] = Convert.ToString(lblLOTNo.Text).Trim();
                        //                else drLOT["LOT_NO"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblLOTDate.Text).Trim()))
                        //                    drLOT["LOT_DATE"] = Convert.ToString(lblLOTDate.Text).Trim();
                        //                else drLOT["LOT_DATE"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderNo.Text).Trim()))
                        //                    drLOT["PRODUCTION_ORDER_NO"] = Convert.ToString(lblProductionOrderNo.Text).Trim();
                        //                else drLOT["PRODUCTION_ORDER_NO"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDate.Text).Trim()))
                        //                    drLOT["PRODUCTION_ORDER_DATE"] = Convert.ToString(lblProductionOrderDate.Text).Trim();
                        //                else drLOT["PRODUCTION_ORDER_DATE"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDeliveryDate.Text).Trim()))
                        //                    drLOT["PRODUCTION_ORDER_DELIVERY_DATE"] = Convert.ToString(lblProductionOrderDeliveryDate.Text).Trim();
                        //                else drLOT["PRODUCTION_ORDER_DELIVERY_DATE"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text).Trim()))
                        //                    drLOT["PRODUCT_CODE"] = Convert.ToString(lblProductCode.Text).Trim();
                        //                else drLOT["PRODUCT_CODE"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblEquipment.Text).Trim()))
                        //                    drLOT["EQUIPMENT_ITEM"] = Convert.ToString(lblEquipment.Text).Trim();
                        //                else drLOT["EQUIPMENT_ITEM"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text).Trim()))
                        //                    drLOT["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text).Trim();
                        //                else drLOT["DRAWING_NO"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(txtPresentStatus.Text).Trim()))
                        //                    drLOT["PRESENT_STATUS"] = Convert.ToString(txtPresentStatus.Text).Trim();
                        //                else drLOT["PRESENT_STATUS"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(txtPresentPercentageOfWorkDone.Text).Trim()))
                        //                    drLOT["PRESENT_PERC_OF_WORK_DONE"] = Convert.ToString(txtPresentPercentageOfWorkDone.Text).Trim();
                        //                else drLOT["PRESENT_PERC_OF_WORK_DONE"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text).Trim()))
                        //                    drLOT["ED_OF_INSP_COMP"] = Convert.ToString(txtEdOfInspection.Text).Trim();
                        //                else drLOT["ED_OF_INSP_COMP"] = string.Empty;

                        //                if (!string.IsNullOrEmpty(Convert.ToString(lblUnit.Text).Trim()))
                        //                    drLOT["UNIT"] = Convert.ToString(lblUnit.Text).Trim();
                        //                else drLOT["UNIT"] = string.Empty;


                        //                dtTempLOTList.Rows.Add(drLOT);
                        //            }
                        //        }
                        //    }
                        //}


                        //if (dtTempLOTList.Rows.Count > 0)
                        //{
                        //    CreateAttachmentsAndSendMail(dtTempLOTList, dtPMList, dtUnitList);
                        //}

                        #endregion


                        SuccessMessage(count + " records posted successfully...!!!");
                        GetFabricationList();
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("Please select atleast 1 record...!!!");
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    //private void PostPlanningRemarks()
    //{
    //    try
    //    {
    //        DataTable dtPostedSrNo = new DataTable();
    //        dtPostedSrNo.Columns.Add("SR_NO", typeof(int));

    //        DataTable dtTempLOTList = new DataTable();
    //        dtTempLOTList.Columns.Add("SR_NO", typeof(int));
    //        dtTempLOTList.Columns.Add("PLANNING_REMARKS", typeof(string));


    //        DataTable dtTemp1 = new DataTable();
    //        dtTemp1.Columns.Add("JOB_NO", typeof(string));
    //        dtTemp1.Columns.Add("LOT_TF_ID", typeof(int));
    //        dtTemp1.Columns.Add("LOT_DATE", typeof(string));
    //        dtTemp1.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
    //        dtTemp1.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
    //        dtTemp1.Columns.Add("PRODUCTION_ORDER_DELIVERY_DATE", typeof(string));
    //        dtTemp1.Columns.Add("PRODUCT_CODE", typeof(string));
    //        dtTemp1.Columns.Add("EQUIPMENT", typeof(string));
    //        dtTemp1.Columns.Add("UOM", typeof(string));
    //        dtTemp1.Columns.Add("QUANTITY", typeof(double));
    //        dtTemp1.Columns.Add("DRAWING_NO", typeof(string));
    //        dtTemp1.Columns.Add("PRESENT_STATUS", typeof(string));
    //        dtTemp1.Columns.Add("PRESENT_PERC_OF_WORK_DONE", typeof(int));
    //        dtTemp1.Columns.Add("ED_OF_INSP_COMP", typeof(string));
    //        dtTemp1.Columns.Add("UNIT", typeof(string));

    //        DataTable dtTemp2 = new DataTable();
    //        dtTemp2.Columns.Add("POSTED_RECORD_ID", typeof(int));
    //        dtTemp2.Columns.Add("OLD_PRESENT_STATUS", typeof(string));
    //        dtTemp2.Columns.Add("OLD_PRESENT_PERC_OF_WORK_DONE", typeof(int));
    //        dtTemp2.Columns.Add("OLD_ED_OF_INSP_COMP", typeof(string));
    //        dtTemp2.Columns.Add("NEW_PRESENT_STATUS", typeof(string));
    //        dtTemp2.Columns.Add("NEW_PRESENT_PERC_OF_WORK_DONE", typeof(int));
    //        dtTemp2.Columns.Add("NEW_ED_OF_INSP_COMP", typeof(string));


    //        int srNo = 0;
    //        int recordID = 0;
    //        string planningRemarks = string.Empty;
    //        string oldPlanningRemarks = string.Empty;

    //        string tableName = string.Empty;
    //        string updateQuery = string.Empty;

    //        int count = 0;

    //        if (gvFabricationList.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow gr in gvFabricationList.Rows)
    //            {
    //                CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
    //                if (chkSelect.Checked)
    //                {
    //                    count++;

    //                    srNo = 0;
    //                    recordID = 0;
    //                    planningRemarks = string.Empty;
    //                    oldPlanningRemarks = string.Empty;


    //                    TextBox txtSRNo = (TextBox)gr.FindControl("txtSRNo");
    //                    Label lblRecordID = (Label)gr.FindControl("lblRecordID");
    //                    Label lblPlanningRemarks = (Label)gr.FindControl("lblPlanningRemarks");
    //                    TextBox txtPlanningRemarks = (TextBox)gr.FindControl("txtPlanningRemarks");
    //                    Label lblTableName = (Label)gr.FindControl("lblTableName"); 

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtSRNo.Text)) && Convert.ToInt32(txtSRNo.Text) > 0)
    //                        srNo = Convert.ToInt32(txtSRNo.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)) && Convert.ToInt32(lblRecordID.Text) > 0)
    //                        recordID = Convert.ToInt32(lblRecordID.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblPlanningRemarks.Text).Trim()))
    //                        oldPlanningRemarks = Convert.ToString(lblPlanningRemarks.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtPlanningRemarks.Text).Trim()))
    //                        planningRemarks = Convert.ToString(txtPlanningRemarks.Text).Replace("'", " ").Replace("\"", " ").Replace("''", " ");
    //                    else
    //                    {
    //                        ExceptionMessage("Please enter planning remarks...!!!");
    //                        return;
    //                    }

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblTableName.Text).Trim()))
    //                        tableName = Convert.ToString(lblTableName.Text).Trim();
    //                    else
    //                        tableName = string.Empty;



    //                    if (recordID > 0)
    //                    {
    //                        DataRow drn2 = dtTemp2.NewRow();
    //                        drn2["POSTED_RECORD_ID"] = recordID;
    //                        drn2["OLD_PLANNING_REMARK"] = oldPlanningRemarks;
    //                        drn2["NEW_PLANNING_REMARK"] = planningRemarks;                            
    //                        dtTemp2.Rows.Add(drn2);

    //                        //update main table
    //                        updateQuery += "UPDATE " + tableName + " SET PLANNING_REMARKS='" + planningRemarks + "', " +
    //                                                                    "MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", " +
    //                                                                    "MODIFIED_ON=GETDATE() " +
    //                                                                    "WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;

    //                        DataRow drp = dtPostedSrNo.NewRow();
    //                        drp["SR_NO"] = srNo;
    //                        dtPostedSrNo.Rows.Add(drp);
    //                    }
    //                    else
    //                    {
    //                        DataRow drn1 = dtTemp1.NewRow();

    //                        drn1["JOB_NO"] = jobNo;
    //                        drn1["LOT_TF_ID"] = LOTTFID;
    //                        drn1["LOT_DATE"] = LOTDate;
    //                        drn1["PRODUCTION_ORDER_NO"] = productionOrderNo;
    //                        drn1["PRODUCTION_ORDER_DATE"] = productionOrderDate;
    //                        drn1["PRODUCTION_ORDER_DELIVERY_DATE"] = productionOrderDeliveryDate;
    //                        drn1["PRODUCT_CODE"] = productCode;
    //                        drn1["EQUIPMENT"] = equipment;
    //                        drn1["UOM"] = UOM;
    //                        drn1["QUANTITY"] = quantity;
    //                        drn1["DRAWING_NO"] = drawingNo;
    //                        drn1["PRESENT_STATUS"] = presentStatus;
    //                        drn1["PRESENT_PERC_OF_WORK_DONE"] = presentPercentageOfWork;
    //                        drn1["ED_OF_INSP_COMP"] = EDOfInspComp;
    //                        drn1["UNIT"] = unit;

    //                        dtTemp1.Rows.Add(drn1);

    //                        DataRow drp = dtPostedSrNo.NewRow();
    //                        drp["SR_NO"] = srNo;
    //                        dtPostedSrNo.Rows.Add(drp);
    //                    }
    //                }
    //            }


    //            int srCount = 0;

    //            if (count > 0)
    //            {
    //                if (!string.IsNullOrEmpty(updateQuery))
    //                    updateQuery = updateQuery.TrimEnd('\n', ' ');

    //                int value = objProject.PostFabricationList(dtTemp1, dtTemp2, updateQuery, Convert.ToInt32(Session["EMP_RECORD_ID"]));

    //                if (value > 0)
    //                {


    //                    if (dtPostedSrNo.Rows.Count > 0)
    //                    {
    //                        foreach (DataRow drp in dtPostedSrNo.Rows)
    //                        {
    //                            foreach (GridViewRow gr in gvFabricationList.Rows)
    //                            {
    //                                TextBox txtSRNo = gr.FindControl("txtSRNo") as TextBox;
    //                                Label lblLOTTFID = gr.FindControl("lblLOTTFID") as Label;
    //                                Label lblShortJOBNo = gr.FindControl("lblShortJOBNo") as Label;
    //                                Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
    //                                Label lblLOTNo = gr.FindControl("lblLOTNo") as Label;
    //                                Label lblLOTDate = gr.FindControl("lblLOTDate") as Label;
    //                                Label lblProductionOrderNo = gr.FindControl("lblProductionOrderNo") as Label;
    //                                Label lblProductionOrderDate = gr.FindControl("lblProductionOrderDate") as Label;
    //                                Label lblProductionOrderDeliveryDate = gr.FindControl("lblProductionOrderDeliveryDate") as Label;
    //                                Label lblProductCode = gr.FindControl("lblProductCode") as Label;
    //                                Label lblEquipment = gr.FindControl("lblEquipment") as Label;
    //                                Label lblUOM = gr.FindControl("lblUOM") as Label;
    //                                TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
    //                                Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;
    //                                TextBox txtPresentStatus = gr.FindControl("txtPresentStatus") as TextBox;
    //                                TextBox txtPresentPercentageOfWorkDone = gr.FindControl("txtPresentPercentageOfWorkDone") as TextBox;
    //                                TextBox txtEdOfInspection = gr.FindControl("txtEdOfInspection") as TextBox;
    //                                Label lblUnit = gr.FindControl("lblUnit") as Label;


    //                                if (Convert.ToInt32(txtSRNo.Text) == Convert.ToInt32(drp["SR_NO"]))
    //                                {

    //                                    if (Convert.ToDateTime(txtEdOfInspection.Text) > Convert.ToDateTime(lblProductionOrderDeliveryDate.Text))
    //                                    {
    //                                        srCount++;
    //                                        DataRow drLOT = dtTempLOTList.NewRow();

    //                                        drLOT["SR_NO"] = srCount;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblShortJOBNo.Text).Trim()))
    //                                            drLOT["SHORT_JOB_NO"] = Convert.ToString(lblShortJOBNo.Text).Trim();
    //                                        else drLOT["SHORT_JOB_NO"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text).Trim()))
    //                                            drLOT["JOB_NO"] = Convert.ToString(lblJOBNo.Text).Trim();
    //                                        else drLOT["JOB_NO"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblLOTNo.Text).Trim()))
    //                                            drLOT["LOT_NO"] = Convert.ToString(lblLOTNo.Text).Trim();
    //                                        else drLOT["LOT_NO"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblLOTDate.Text).Trim()))
    //                                            drLOT["LOT_DATE"] = Convert.ToString(lblLOTDate.Text).Trim();
    //                                        else drLOT["LOT_DATE"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderNo.Text).Trim()))
    //                                            drLOT["PRODUCTION_ORDER_NO"] = Convert.ToString(lblProductionOrderNo.Text).Trim();
    //                                        else drLOT["PRODUCTION_ORDER_NO"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDate.Text).Trim()))
    //                                            drLOT["PRODUCTION_ORDER_DATE"] = Convert.ToString(lblProductionOrderDate.Text).Trim();
    //                                        else drLOT["PRODUCTION_ORDER_DATE"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDeliveryDate.Text).Trim()))
    //                                            drLOT["PRODUCTION_ORDER_DELIVERY_DATE"] = Convert.ToString(lblProductionOrderDeliveryDate.Text).Trim();
    //                                        else drLOT["PRODUCTION_ORDER_DELIVERY_DATE"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text).Trim()))
    //                                            drLOT["PRODUCT_CODE"] = Convert.ToString(lblProductCode.Text).Trim();
    //                                        else drLOT["PRODUCT_CODE"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblEquipment.Text).Trim()))
    //                                            drLOT["EQUIPMENT_ITEM"] = Convert.ToString(lblEquipment.Text).Trim();
    //                                        else drLOT["EQUIPMENT_ITEM"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text).Trim()))
    //                                            drLOT["UOM"] = Convert.ToString(lblUOM.Text);
    //                                        else drLOT["UOM"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(txtQuantity.Text).Trim()))
    //                                            drLOT["QUANTITY"] = Convert.ToDouble(txtQuantity.Text);
    //                                        else drLOT["QUANTITY"] = "0";

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text).Trim()))
    //                                            drLOT["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text).Trim();
    //                                        else drLOT["DRAWING_NO"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(txtPresentStatus.Text).Trim()))
    //                                            drLOT["PRESENT_STATUS"] = Convert.ToString(txtPresentStatus.Text).Trim();
    //                                        else drLOT["PRESENT_STATUS"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(txtPresentPercentageOfWorkDone.Text).Trim()))
    //                                            drLOT["PRESENT_PERC_OF_WORK_DONE"] = Convert.ToString(txtPresentPercentageOfWorkDone.Text).Trim();
    //                                        else drLOT["PRESENT_PERC_OF_WORK_DONE"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text).Trim()))
    //                                            drLOT["ED_OF_INSP_COMP"] = Convert.ToString(txtEdOfInspection.Text).Trim();
    //                                        else drLOT["ED_OF_INSP_COMP"] = string.Empty;

    //                                        if (!string.IsNullOrEmpty(Convert.ToString(lblUnit.Text).Trim()))
    //                                            drLOT["UNIT"] = Convert.ToString(lblUnit.Text).Trim();
    //                                        else drLOT["UNIT"] = string.Empty;


    //                                        dtTempLOTList.Rows.Add(drLOT);
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }

    //                    if (dtTempLOTList.Rows.Count > 0)
    //                    {
    //                        CreateAttachmentsAndSendMail(dtTempLOTList, dtPMList, dtUnitList);
    //                    }

    //                    SuccessMessage(count + " records posted successfully...!!!");
    //                    GetFabricationList();
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                ExceptionMessage("Please select atleast 1 record...!!!");
    //                return;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    //private void PostPlanningRemarks(DataTable dtPMList, DataTable dtUnitList)
    //{
    //    try
    //    {
    //        DataTable dtPostedSrNo = new DataTable();
    //        dtPostedSrNo.Columns.Add("SR_NO", typeof(int));

    //        DataTable dtTempToInsert = new DataTable();
    //        dtTempToInsert.Columns.Add("JOB_NO", typeof(string));
    //        dtTempToInsert.Columns.Add("LOT_TF_ID", typeof(int));
    //        dtTempToInsert.Columns.Add("LOT_DATE", typeof(string));
    //        dtTempToInsert.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
    //        dtTempToInsert.Columns.Add("PRODUCTION_ORDER_DATE", typeof(string));
    //        dtTempToInsert.Columns.Add("PRODUCTION_ORDER_DELIVERY_DATE", typeof(string));
    //        dtTempToInsert.Columns.Add("PRODUCT_CODE", typeof(string));
    //        dtTempToInsert.Columns.Add("EQUIPMENT", typeof(string));
    //        dtTempToInsert.Columns.Add("UOM", typeof(string));
    //        dtTempToInsert.Columns.Add("QUANTITY", typeof(double));
    //        dtTempToInsert.Columns.Add("DRAWING_NO", typeof(string));
    //        dtTempToInsert.Columns.Add("PRESENT_STATUS", typeof(string));
    //        dtTempToInsert.Columns.Add("PRESENT_PERC_OF_WORK_DONE", typeof(int));
    //        dtTempToInsert.Columns.Add("ED_OF_INSP_COMP", typeof(string));
    //        dtTempToInsert.Columns.Add("UNIT", typeof(string));
    //        dtTempToInsert.Columns.Add("PLANNING_REMARKS", typeof(string));

    //        DataTable dtTempLogTable = new DataTable();
    //        dtTempLogTable.Columns.Add("POSTED_RECORD_ID", typeof(int));
    //        dtTempLogTable.Columns.Add("OLD_PRESENT_STATUS", typeof(string));
    //        dtTempLogTable.Columns.Add("OLD_PRESENT_PERC_OF_WORK_DONE", typeof(int));
    //        dtTempLogTable.Columns.Add("OLD_ED_OF_INSP_COMP", typeof(string));
    //        dtTempLogTable.Columns.Add("NEW_PRESENT_STATUS", typeof(string));
    //        dtTempLogTable.Columns.Add("NEW_PRESENT_PERC_OF_WORK_DONE", typeof(int));
    //        dtTempLogTable.Columns.Add("NEW_ED_OF_INSP_COMP", typeof(string));
    //        dtTempLogTable.Columns.Add("OLD_PLANNING_REMARKS", typeof(string));
    //        dtTempLogTable.Columns.Add("NEW_PLANNING_REMARKS", typeof(string));

    //        int srNo = 0;
    //        int recordID = 0;
    //        string jobNo = string.Empty;
    //        int LOTTFID = 0;
    //        string LOTDate = string.Empty;
    //        string productionOrderNo = string.Empty;
    //        string productionOrderDate = string.Empty;
    //        string productionOrderDeliveryDate = string.Empty;
    //        string productCode = string.Empty;
    //        string equipment = string.Empty;
    //        string UOM = string.Empty;
    //        double quantity = 0;
    //        string drawingNo = string.Empty;
    //        string presentStatus = string.Empty;
    //        int presentPercentageOfWork = 0;
    //        string EDOfInspComp = string.Empty;
    //        string unit = string.Empty;
    //        string oldPresentStatus = string.Empty;
    //        int oldPresentPercentageOfWork = 0;
    //        string oldEDOfInspComp = string.Empty;
    //        string planningRemarks = string.Empty;
    //        string oldPlanningRemarks = string.Empty;

    //        string tableName = string.Empty;
    //        string updateQuery = string.Empty;

    //        int count = 0;

    //        if (gvFabricationList.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow gr in gvFabricationList.Rows)
    //            {
    //                CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
    //                if (chkSelect.Checked)
    //                {
    //                    count++;

    //                    srNo = 0;
    //                    recordID = 0;
    //                    jobNo = string.Empty;
    //                    LOTTFID = 0;
    //                    LOTDate = string.Empty;
    //                    productionOrderNo = string.Empty;
    //                    productionOrderDate = string.Empty;
    //                    productionOrderDeliveryDate = string.Empty;
    //                    productCode = string.Empty;
    //                    equipment = string.Empty;
    //                    UOM = string.Empty;
    //                    quantity = 0;
    //                    drawingNo = string.Empty;
    //                    presentStatus = string.Empty;
    //                    presentPercentageOfWork = 0;
    //                    EDOfInspComp = string.Empty;
    //                    unit = string.Empty;
    //                    oldPresentStatus = string.Empty;
    //                    oldPresentPercentageOfWork = 0;
    //                    oldEDOfInspComp = string.Empty;
    //                    planningRemarks = string.Empty;
    //                    oldPlanningRemarks = string.Empty;


    //                    TextBox txtSRNo = (TextBox)gr.FindControl("txtSRNo");
    //                    Label lblRecordID = (Label)gr.FindControl("lblRecordID");
    //                    Label lblTableName = (Label)gr.FindControl("lblTableName");
    //                    Label lblUnit = (Label)gr.FindControl("lblUnit");
    //                    Label lblJOBNo = (Label)gr.FindControl("lblJOBNo");
    //                    Label lblLOTTFID = (Label)gr.FindControl("lblLOTTFID");
    //                    Label lblLOTDate = (Label)gr.FindControl("lblLOTDate");
    //                    Label lblProductionOrderNo = (Label)gr.FindControl("lblProductionOrderNo");
    //                    Label lblProductionOrderDate = (Label)gr.FindControl("lblProductionOrderDate");
    //                    Label lblProductionOrderDeliveryDate = (Label)gr.FindControl("lblProductionOrderDeliveryDate");
    //                    Label lblProductCode = (Label)gr.FindControl("lblProductCode");
    //                    Label lblEquipment = (Label)gr.FindControl("lblEquipment");
    //                    Label lblUOM = (Label)gr.FindControl("lblUOM");
    //                    TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
    //                    Label lblDrawingNo = (Label)gr.FindControl("lblDrawingNo");
    //                    Label lblPresentStatus = (Label)gr.FindControl("lblPresentStatus");
    //                    TextBox txtPresentStatus = (TextBox)gr.FindControl("txtPresentStatus");
    //                    Label lblPresentPercentageOfWorkDone = (Label)gr.FindControl("lblPresentPercentageOfWorkDone");
    //                    TextBox txtPresentPercentageOfWorkDone = (TextBox)gr.FindControl("txtPresentPercentageOfWorkDone");
    //                    Label lblEdOfInspection = (Label)gr.FindControl("lblEdOfInspection");
    //                    TextBox txtEdOfInspection = (TextBox)gr.FindControl("txtEdOfInspection");
    //                    TextBox txtLastStatus = (TextBox)gr.FindControl("txtLastStatus");
    //                    Label lblLastPercentageOfWorkDone = (Label)gr.FindControl("lblLastPercentageOfWorkDone");
    //                    TextBox txtLastPercentageOfWorkDone = (TextBox)gr.FindControl("txtLastPercentageOfWorkDone");
    //                    Label lblLastEdOfInspection = (Label)gr.FindControl("lblLastEdOfInspection");

    //                    Label lblPlanningRemarks = (Label)gr.FindControl("lblPlanningRemarks");
    //                    TextBox txtPlanningRemarks = (TextBox)gr.FindControl("txtPlanningRemarks");


    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtSRNo.Text)) && Convert.ToInt32(txtSRNo.Text) > 0)
    //                        srNo = Convert.ToInt32(txtSRNo.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)) && Convert.ToInt32(lblRecordID.Text) > 0)
    //                        recordID = Convert.ToInt32(lblRecordID.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text).Trim()))
    //                        jobNo = Convert.ToString(lblJOBNo.Text).Trim().ToUpper();

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblLOTTFID.Text)) && Convert.ToInt32(lblLOTTFID.Text) > 0)
    //                        LOTTFID = Convert.ToInt32(lblLOTTFID.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblLOTDate.Text).Trim()))
    //                        LOTDate = Convert.ToDateTime(lblLOTDate.Text).ToString("yyyy-MM-dd");

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderNo.Text).Trim()))
    //                        productionOrderNo = Convert.ToString(lblProductionOrderNo.Text).Trim().ToUpper();

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDate.Text).Trim()))
    //                        productionOrderDate = Convert.ToDateTime(lblProductionOrderDate.Text).ToString("yyyy-MM-dd");

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderDeliveryDate.Text).Trim()))
    //                        productionOrderDeliveryDate = Convert.ToDateTime(lblProductionOrderDeliveryDate.Text).ToString("yyyy-MM-dd");

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text).Trim()))
    //                        productCode = Convert.ToString(lblProductCode.Text).Trim().ToUpper();

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblEquipment.Text).Trim()))
    //                        equipment = Convert.ToString(lblEquipment.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text).Trim()))
    //                        UOM = Convert.ToString(lblUOM.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtQuantity.Text).Trim()))
    //                        quantity = Convert.ToDouble(txtQuantity.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text).Trim()))
    //                        drawingNo = Convert.ToString(lblDrawingNo.Text).Trim().ToUpper();


    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblPresentStatus.Text).Trim()))
    //                        oldPresentStatus = Convert.ToString(lblPresentStatus.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblEdOfInspection.Text).Trim()))
    //                        oldEDOfInspComp = Convert.ToString(lblEdOfInspection.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblPresentPercentageOfWorkDone.Text)))
    //                        oldPresentPercentageOfWork = Convert.ToInt32(lblPresentPercentageOfWorkDone.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtPresentStatus.Text).Trim()))
    //                        presentStatus = Convert.ToString(txtPresentStatus.Text).Replace("'", " ").Replace("\"", " ").Replace("''", " ");

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text)))
    //                        EDOfInspComp = Convert.ToDateTime(txtEdOfInspection.Text).ToString("yyyy-MM-dd");

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtPresentPercentageOfWorkDone.Text)))
    //                        presentPercentageOfWork = Convert.ToInt32(txtPresentPercentageOfWorkDone.Text);


    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblUnit.Text).Trim()))
    //                        unit = Convert.ToString(lblUnit.Text).Trim().ToUpper();


    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblPlanningRemarks.Text).Trim()))
    //                        oldPlanningRemarks = Convert.ToString(lblPlanningRemarks.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtPlanningRemarks.Text).Trim()))
    //                        planningRemarks = Convert.ToString(txtPlanningRemarks.Text).Replace("'", " ").Replace("\"", " ").Replace("''", " ");
    //                    else
    //                    {
    //                        ExceptionMessage("Please enter planning remarks...!!!");
    //                        return;
    //                    }

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblTableName.Text).Trim()))
    //                        tableName = Convert.ToString(lblTableName.Text).Trim();
    //                    else
    //                        tableName = string.Empty;



    //                    if (recordID > 0)
    //                    {
    //                        //insert into log table

    //                        DataRow drn2 = dtTempLogTable.NewRow();
    //                        drn2["POSTED_RECORD_ID"] = recordID;
    //                        drn2["OLD_PRESENT_STATUS"] = oldPresentStatus;
    //                        drn2["OLD_PRESENT_PERC_OF_WORK_DONE"] = oldPresentPercentageOfWork;
    //                        drn2["OLD_ED_OF_INSP_COMP"] = oldEDOfInspComp;
    //                        drn2["NEW_PRESENT_STATUS"] = presentStatus;
    //                        drn2["NEW_PRESENT_PERC_OF_WORK_DONE"] = presentPercentageOfWork;
    //                        drn2["NEW_ED_OF_INSP_COMP"] = EDOfInspComp;
    //                        drn2["OLD_PLANNING_REMARKS"] = oldPlanningRemarks;
    //                        drn2["NEW_PLANNING_REMARKS"] = planningRemarks;

    //                        dtTempLogTable.Rows.Add(drn2);


    //                        //update main table
    //                        updateQuery += "UPDATE " + tableName + " SET PLANNING_REMARKS='" + planningRemarks + "', " +
    //                                                                    "MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", " +
    //                                                                    "MODIFIED_ON=GETDATE() " +
    //                                                                    "WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;


    //                        DataRow drp = dtPostedSrNo.NewRow();
    //                        drp["SR_NO"] = srNo;
    //                        dtPostedSrNo.Rows.Add(drp);
    //                    }
    //                    else
    //                    {
    //                        DataRow drn1 = dtTempToInsert.NewRow();

    //                        drn1["JOB_NO"] = jobNo;
    //                        drn1["LOT_TF_ID"] = LOTTFID;
    //                        drn1["LOT_DATE"] = LOTDate;
    //                        drn1["PRODUCTION_ORDER_NO"] = productionOrderNo;
    //                        drn1["PRODUCTION_ORDER_DATE"] = productionOrderDate;
    //                        drn1["PRODUCTION_ORDER_DELIVERY_DATE"] = productionOrderDeliveryDate;
    //                        drn1["PRODUCT_CODE"] = productCode;
    //                        drn1["EQUIPMENT"] = equipment;
    //                        drn1["UOM"] = UOM;
    //                        drn1["QUANTITY"] = quantity;
    //                        drn1["DRAWING_NO"] = drawingNo;
    //                        drn1["PRESENT_STATUS"] = presentStatus;
    //                        drn1["PRESENT_PERC_OF_WORK_DONE"] = presentPercentageOfWork;
    //                        drn1["ED_OF_INSP_COMP"] = EDOfInspComp;
    //                        drn1["UNIT"] = unit;
    //                        drn1["PLANNING_REMARKS"] = planningRemarks;

    //                        dtTempToInsert.Rows.Add(drn1);

    //                        DataRow drp = dtPostedSrNo.NewRow();
    //                        drp["SR_NO"] = srNo;
    //                        dtPostedSrNo.Rows.Add(drp);
    //                    }
    //                }
    //            }

    //            if (count > 0)
    //            {
    //                if (!string.IsNullOrEmpty(updateQuery))
    //                    updateQuery = updateQuery.TrimEnd('\n', ' ');

    //                int value = objProject.PostFabricationList(dtTempToInsert, dtTempLogTable, updateQuery, Convert.ToInt32(Session["EMP_RECORD_ID"]));

    //                if (value > 0)
    //                {
    //                    SuccessMessage("Planning remarks of " + count + " records posted successfully...!!!");
    //                    GetFabricationList();
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                ExceptionMessage("Please select atleast 1 record...!!!");
    //                return;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}



    private int CreateAttachmentsAndSendMail(DataTable dtLOTList, DataTable dtPMList, DataTable dtUnitList)
    {
        try
        {
            int sendMailValue = 0;
            int retVal = 0;
            string jobNo = string.Empty;
            string unit = string.Empty;
            Stream streamLOTView = null;

            if (dtLOTList.Rows.Count > 0)
            {
                foreach (DataRow drul in dtUnitList.Rows)
                {
                    unit = Convert.ToString(drul["UNIT_NAME"]);

                    foreach (DataRow drpm in dtPMList.Select("UNIT_NAME='" + unit + "'"))
                    {
                        streamLOTView = null;
                        body = string.Empty;
                        subject = string.Empty;
                        from = string.Empty;
                        to = string.Empty;
                        cc = string.Empty;
                        bcc = string.Empty;
                        toName = string.Empty;
                        mailSentDate = string.Empty;
                        from = "ithelpdesk@coperion.com";


                        to = Convert.ToString(drpm["EMAIL_ID"]);
                        toName = Convert.ToString(drpm["PM"]);
                        jobNo = Convert.ToString(drpm["JOB_NO"]);


                        DataTable dtLOT = new DataTable();
                        dtLOT = dtLOTList.Clone();

                        //foreach (DataRow dr2 in dtPOList.Select("COMPLETE_JOB_NO='" + jobNo + "'"))
                        foreach (DataRow dr2 in dtLOTList.Select("SHORT_JOB_NO='" + jobNo + "' AND UNIT='" + unit + "'"))
                        {
                            dtLOT.ImportRow(dr2);
                        }

                        if (dtLOT.Rows.Count > 0)
                        {

                            dtLOT.Columns.Remove("SHORT_JOB_NO");

                            mailSentDate = DateTime.Now.ToString("dd-MMM-yyyy");
                            subject = "Alert of Production Status (Job Number : " + jobNo + ")";

                            if (!string.IsNullOrEmpty(to))
                                to = to.TrimEnd(';');

                            if (!string.IsNullOrEmpty(cc))
                                cc = cc.TrimEnd(';');

                            if (!string.IsNullOrEmpty(bcc))
                                bcc = bcc.TrimEnd(';');

                            streamLOTView = CreateAttachment(dtLOT);

                            int chk = 0;
                            if (streamLOTView == null)
                            {
                                chk = 0;
                            }
                            else
                            {
                                chk = 1;
                            }

                            if (chk > 0)
                            {
                                sendMailValue = SendMail(subject, from, to, toName, cc, bcc, streamLOTView, mailSentDate, jobNo);
                            }
                        }
                    }
                }
            }
            else
                retVal = 0;

            if (sendMailValue > 0)
                retVal = 1;

            return retVal;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    private Stream CreateAttachment(DataTable dt)
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

                    rowTxt = Convert.ToString(rowTxt).Replace("\n", " ");
                    rowTxt = Convert.ToString(rowTxt).Replace("\r", " ");
                    rowTxt = Convert.ToString(rowTxt).Replace(",", "") + ',';

                    csv += Convert.ToString(rowTxt);
                }
                csv += "\r\n";
            }


            byte[] byteArray = Encoding.ASCII.GetBytes(csv);
            MemoryStream stream = new MemoryStream(byteArray);

            return stream;
        }
        catch (Exception)
        {
            return null;
        }
    }


    private int SendMail(string subject, string from, string to, string toName, string cc, string bcc, Stream streamLOTView, string sentDate, string jobNo)
    {
        body = string.Empty;
        int returnVal = 0;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        mail.Subject = subject;
        mail.From = new MailAddress(from);


        if (!string.IsNullOrEmpty(to) && to.ToLower().Contains("niranjan.saini@coperion.com"))
        {
            if (cc == null || cc.Trim() == "")
            {
                cc = "Jatin.Kumar@coperion.com";
            }
            else if (!cc.ToLower().Contains("jatin.kumar@coperion.com"))
            {
                cc += ";Jatin.Kumar@coperion.com";
            }
        }

        if (!string.IsNullOrEmpty(to))
        {
            to = to.TrimEnd(';');
            string[] strTo = to.Split(';');
            foreach (string item in strTo)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.To.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(cc))
        {
            cc = cc.TrimEnd(';');
            string[] strCC = cc.Split(';');
            foreach (string item in strCC)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.CC.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(bcc))
        {
            bcc = bcc.TrimEnd(';');
            string[] strBcc = bcc.Split(';');
            foreach (string item in strBcc)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.Bcc.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(to))
        {
            if (streamLOTView != null)
            {
                mail.Attachments.Add(new Attachment(streamLOTView, "Production_Status_Report_" + sentDate + ".csv", "text/csv"));
            }


            mail.IsBodyHtml = true;

            fileName = "~/PROJECT/LOT/StatusReportMail.html";
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#toname#}", toName);
            body = body.Replace("{#jobno#}", jobNo);
            body = body.Replace("{#sentdate#}", sentDate);


            mail.Body = body;

            try
            {
                if (!string.IsNullOrEmpty(to))
                {
                    SmtpServer.Send(mail);
                    returnVal = 1;
                }
                else
                    returnVal = 0;
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    returnVal = 1;

                else
                    returnVal = 0;
            }
        }
        else
            returnVal = 0;


        return returnVal;
    }

    private void DeleteFabrication()
    {
        try
        {
            DataTable dtTemp = new DataTable();

            dtTemp.Columns.Add("JOB_NO", typeof(string));
            dtTemp.Columns.Add("PRODUCTION_ORDER_NO", typeof(string));
            dtTemp.Columns.Add("PRODUCT_CODE", typeof(string));
            dtTemp.Columns.Add("UNIT", typeof(string));

            string jobNo = string.Empty;
            string productionOrderNo = string.Empty;
            string productCode = string.Empty;
            string unit = string.Empty;


            int count = 0;

            if (gvFabricationList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvFabricationList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        count++;

                        jobNo = string.Empty;
                        productionOrderNo = string.Empty;
                        productCode = string.Empty;
                        unit = string.Empty;

                        Label lblJOBNo = (Label)gr.FindControl("lblJOBNo");
                        Label lblProductionOrderNo = (Label)gr.FindControl("lblProductionOrderNo");
                        Label lblProductCode = (Label)gr.FindControl("lblProductCode");
                        Label lblUnit = (Label)gr.FindControl("lblUnit");

                        if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text).Trim()))
                            jobNo = Convert.ToString(lblJOBNo.Text).Trim().ToUpper();

                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductionOrderNo.Text).Trim()))
                            productionOrderNo = Convert.ToString(lblProductionOrderNo.Text).Trim().ToUpper();

                        if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text).Trim()))
                            productCode = Convert.ToString(lblProductCode.Text).Trim().ToUpper();

                        if (!string.IsNullOrEmpty(Convert.ToString(lblUnit.Text).Trim()))
                            unit = Convert.ToString(lblUnit.Text).Trim().ToUpper();



                        DataRow drn = dtTemp.NewRow();

                        drn["JOB_NO"] = jobNo;
                        drn["PRODUCTION_ORDER_NO"] = productionOrderNo;
                        drn["PRODUCT_CODE"] = productCode;
                        drn["UNIT"] = unit;

                        dtTemp.Rows.Add(drn);
                    }
                }

                if (count > 0)
                {
                    int value = objProject.DeleteFabricationList(dtTemp, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (value > 0)
                    {
                        SuccessMessage(count + " records deleted successfully...!!!");
                        GetFabricationList();
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("Please select atleast 1 record...!!!");
                    return;
                }
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion



}
