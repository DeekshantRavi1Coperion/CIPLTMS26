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

public partial class REPORTS_PURCHASE_ORDER_PIVOT_GROUP_PostPOReportPivotGroup : System.Web.UI.Page
{

    #region VARIABLES[=======================]
    BAL.Common objCommon = new BAL.Common();
    BAL.Reports objReports = new BAL.Reports();

    DataSet dsUnit = new DataSet();
    DataSet dsPGReport = new DataSet();
    DataTable dt1 = new DataTable();
    DataSet dsPOReport = new DataSet();
    DataSet dsPivotGroupDetail = new DataSet();
    DataSet dsPOItemReport = new DataSet();
    DataSet dsPOOldPivotGroup = new DataSet();
    DataSet dsNewPivotGroup = new DataSet();

    int unitID = 0;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string JOBNo = string.Empty;
    string postedMonth = string.Empty;
    string pivotGroupName = string.Empty;
    string PONo = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdGVRowCount.Value = "0";

                ViewState["UNIT_ID"] = 0;
                ViewState["PO_VALUE_INR"] = 0;
                ViewState["BUDGET"] = 0;
                ViewState["DELTA"] = 0;

                Session["dtPOItemList"] = null;
                Session["dsPOReport"] = null;
                Session["dsPivotGroupDetail"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

                //hdLikelyDeliveryDateToPost.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtLikelyDeliveryDateToPost.Text = Convert.ToString(hdLikelyDeliveryDateToPost.Value);

                //hdPlanDateofProcurementToPost.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtPlanDateofProcurementToPost.Text = Convert.ToString(hdPlanDateofProcurementToPost.Value);

                BindUnitToU();

                Session["PIVOT_GROUP_REPORT"] = null;

                trAsPerPOBudget.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        lblStartDate.Text = txtStartDateSearch.Text;
        lblEndDate.Text = txtEndDateSearch.Text;
        lblJOBNo.Text = txtJOBNo.Text;
        lblPostingMonth.Text = txtPostingMonth.Text;
        GetPGReport();
    }



    double totalPOBudgetINR = 0;
    double totalSaleEstimateBudgetINR = 0;

    double totalPOValueINR = 0;

    double totalDeltaAsPerPOBudgetINR = 0;
    double totalDeltaAsPerSaleEstimateBudgetINR = 0;

    protected void gvPGReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (Convert.ToInt32(rdType.SelectedValue) == 0)
            if (Convert.ToInt32(ddlType.SelectedValue) == 0)
            {
                for (int i = 0; i < gvPGReport.Columns.Count; i++)
                {
                    if (gvPGReport.Columns[i].HeaderText == "PO Budget")
                    {
                        gvPGReport.Columns[i].Visible = true;
                    }

                    if (gvPGReport.Columns[i].HeaderText == "Delta As Per PO Budget")
                    {
                        gvPGReport.Columns[i].Visible = true;
                    }


                    if (gvPGReport.Columns[i].HeaderText == "Sale Estimate Budget")
                    {
                        gvPGReport.Columns[i].Visible = true;
                    }

                    if (gvPGReport.Columns[i].HeaderText == "Delta As Per Sale Estimate Budget")
                    {
                        gvPGReport.Columns[i].Visible = true;
                    }
                }
            }
            //else if (Convert.ToInt32(rdType.SelectedValue) == 1)
            else if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                for (int i = 0; i < gvPGReport.Columns.Count; i++)
                {
                    if (gvPGReport.Columns[i].HeaderText == "PO Budget")
                    {
                        gvPGReport.Columns[i].Visible = true;
                    }

                    if (gvPGReport.Columns[i].HeaderText == "Delta As Per PO Budget")
                    {
                        gvPGReport.Columns[i].Visible = true;
                    }



                    if (gvPGReport.Columns[i].HeaderText == "Sale Estimate Budget")
                    {
                        gvPGReport.Columns[i].Visible = false;
                    }

                    if (gvPGReport.Columns[i].HeaderText == "Delta As Per Sale Estimate Budget")
                    {
                        gvPGReport.Columns[i].Visible = false;
                    }
                }
            }
            //else if (Convert.ToInt32(rdType.SelectedValue) == 2)
            else if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                for (int i = 0; i < gvPGReport.Columns.Count; i++)
                {
                    if (gvPGReport.Columns[i].HeaderText == "PO Budget")
                    {
                        gvPGReport.Columns[i].Visible = false;
                    }

                    if (gvPGReport.Columns[i].HeaderText == "Delta As Per PO Budget")
                    {
                        gvPGReport.Columns[i].Visible = false;
                    }



                    if (gvPGReport.Columns[i].HeaderText == "Sale Estimate Budget")
                    {
                        gvPGReport.Columns[i].Visible = true;
                    }

                    if (gvPGReport.Columns[i].HeaderText == "Delta As Per Sale Estimate Budget")
                    {
                        gvPGReport.Columns[i].Visible = true;
                    }
                }
            }




            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double deltaAsPerSaleEstimate = 0;
                double pendingcost = 0;
                double savingOfOriginalEstimatePerc = 0;
                double savingCost = 0;
                double saleEstimateBudgetINR = 0;

                Label lblRecordIDInList = (Label)e.Row.FindControl("lblRecordIDInList");

                TextBox txtSrNoInList = (TextBox)e.Row.FindControl("txtSrNoInList");
                TextBox txtTotalBudgetINRInList = (TextBox)e.Row.FindControl("txtTotalBudgetINRInList");
                TextBox txtSaleEstimateBudgetINRInList = (TextBox)e.Row.FindControl("txtSaleEstimateBudgetINRInList");
                TextBox txtTotalPOValueINRInList = (TextBox)e.Row.FindControl("txtTotalPOValueINRInList");
                TextBox txtTotalDeltaAsPerPOBudgetINRInList = (TextBox)e.Row.FindControl("txtTotalDeltaAsPerPOBudgetINRInList");
                TextBox txtTotalDeltaAsPerSaleEstimateBudgetINRInList = (TextBox)e.Row.FindControl("txtTotalDeltaAsPerSaleEstimateBudgetINRInList");
                DropDownList ddlStatusInList = (DropDownList)e.Row.FindControl("ddlStatusInList");
                TextBox txtPendingCostInList = (TextBox)e.Row.FindControl("txtPendingCostInList");
                TextBox txtSavingInList = (TextBox)e.Row.FindControl("txtSavingInList");
                TextBox txtSavingOfOriginalEstimatePercentageInList = (TextBox)e.Row.FindControl("txtSavingOfOriginalEstimatePercentageInList");
                DropDownList ddlVPOCInList = (DropDownList)e.Row.FindControl("ddlVPOCInList");



                Label lblPivotGroupCounts = (Label)e.Row.FindControl("lblPivotGroupCounts");
                ImageButton btnViewDetailInListPOBudget = (ImageButton)e.Row.FindControl("btnViewDetailInListPOBudget");
                ImageButton btnViewDetailInListSaleEstimate = (ImageButton)e.Row.FindControl("btnViewDetailInListSaleEstimate");

                btnViewDetailInListPOBudget.Visible = false;
                btnViewDetailInListSaleEstimate.Visible = false;

                if (Convert.ToDouble(txtTotalPOValueINRInList.Text) > 0)
                {
                    btnViewDetailInListPOBudget.Visible = true;
                }

                if (Convert.ToInt32(lblPivotGroupCounts.Text) > 0)
                {
                    btnViewDetailInListSaleEstimate.Visible = true;
                }



                if (!string.IsNullOrEmpty(Convert.ToString(txtTotalBudgetINRInList.Text)))
                    totalPOBudgetINR += Convert.ToDouble(txtTotalBudgetINRInList.Text);

                txtTotalPOBudgetINR.Text = Convert.ToString(totalPOBudgetINR);



                if (!string.IsNullOrEmpty(Convert.ToString(txtSaleEstimateBudgetINRInList.Text)))
                    totalSaleEstimateBudgetINR += Convert.ToDouble(txtSaleEstimateBudgetINRInList.Text);

                txtTotalSaleEstimateBudgetINR.Text = Convert.ToString(totalSaleEstimateBudgetINR);

                if (!string.IsNullOrEmpty(Convert.ToString(txtTotalPOValueINRInList.Text)))
                    totalPOValueINR += Convert.ToDouble(txtTotalPOValueINRInList.Text);

                txtTotalPOValueINR1.Text = Convert.ToString(totalPOValueINR);
                txtTotalPOValueINR2.Text = Convert.ToString(totalPOValueINR);

                if (!string.IsNullOrEmpty(Convert.ToString(txtTotalDeltaAsPerPOBudgetINRInList.Text)))
                    totalDeltaAsPerPOBudgetINR += Convert.ToDouble(txtTotalDeltaAsPerPOBudgetINRInList.Text);
                txtTotalDeltaAsPerPOBudgetINR.Text = Convert.ToString(totalDeltaAsPerPOBudgetINR);


                if (!string.IsNullOrEmpty(Convert.ToString(txtTotalDeltaAsPerSaleEstimateBudgetINRInList.Text)))
                    totalDeltaAsPerSaleEstimateBudgetINR += Convert.ToDouble(txtTotalDeltaAsPerSaleEstimateBudgetINRInList.Text);
                txtTotalDeltaAsPerSaleEstimateBudgetINR.Text = Convert.ToString(totalDeltaAsPerSaleEstimateBudgetINR);


                //if (!string.IsNullOrEmpty(Convert.ToString(txtTotalDeltaAsPerSaleEstimateBudgetINR.Text)) && Convert.ToDouble(txtTotalDeltaAsPerSaleEstimateBudgetINR.Text) > 0)
                //    deltaAsPerSaleEstimate = Convert.ToDouble(txtTotalDeltaAsPerSaleEstimateBudgetINR.Text);
                //else deltaAsPerSaleEstimate = 0;

                //if (!string.IsNullOrEmpty(Convert.ToString(txtPendingCost.Text)) && Convert.ToDouble(txtPendingCost.Text) > 0)
                //    pendingcost = Convert.ToDouble(txtPendingCost.Text);
                //else pendingcost = 0;

                //txtSaving.Text = Convert.ToString(Math.Round((deltaAsPerSaleEstimate - pendingcost), 3));

                //if (!string.IsNullOrEmpty(Convert.ToString(txtSaleEstimateBudgetINR.Text)) && Convert.ToDouble(txtSaleEstimateBudgetINR.Text) > 0)
                //    saleEstimateBudgetINR = Convert.ToDouble(txtSaleEstimateBudgetINR.Text);
                //else saleEstimateBudgetINR = 0;

                //if (!string.IsNullOrEmpty(Convert.ToString(txtSaving.Text)) && Convert.ToDouble(txtSaving.Text) > 0)
                //    savingCost = Convert.ToDouble(txtSaving.Text);
                //else savingCost = 0;

                //if (savingCost > 0)
                //    savingOfOriginalEstimatePerc = ((savingCost / saleEstimateBudgetINR) * 100);
                //else savingOfOriginalEstimatePerc = 0;

                //txtSavingOfOriginalEstimatePercentage.Text = Convert.ToString(Math.Round(savingOfOriginalEstimatePerc, 3));


                if (Convert.ToInt32(lblRecordIDInList.Text) > 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                    }

                    txtSrNoInList.BackColor = System.Drawing.Color.LightPink;
                    txtTotalBudgetINRInList.BackColor = System.Drawing.Color.LightPink;
                    txtSaleEstimateBudgetINRInList.BackColor = System.Drawing.Color.LightPink;
                    txtTotalPOValueINRInList.BackColor = System.Drawing.Color.LightPink;
                    txtTotalDeltaAsPerPOBudgetINRInList.BackColor = System.Drawing.Color.LightPink;
                    txtTotalDeltaAsPerSaleEstimateBudgetINRInList.BackColor = System.Drawing.Color.LightPink;
                    ddlStatusInList.BackColor = System.Drawing.Color.LightPink;
                    txtPendingCostInList.BackColor = System.Drawing.Color.LightPink;
                    txtSavingInList.BackColor = System.Drawing.Color.LightPink;
                    txtSavingOfOriginalEstimatePercentageInList.BackColor = System.Drawing.Color.LightPink;
                    ddlVPOCInList.BackColor = System.Drawing.Color.LightPink;
                }


                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }


        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvPGReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                HidePanelPoList();

                int rowindex = 0;

                Session["dsPOReport"] = null;
                Session["dsPivotGroupDetail"] = null;
                ViewState["PivotGroupDescInList"] = null;

                if (Convert.ToString(e.CommandArgument) == "ViewDETAILPOBudget" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAILSaleEstimate")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                if (Convert.ToString(e.CommandArgument) == "ViewDETAILPOBudget")
                {
                    Label lblPivotGroupDescInList = gvPGReport.Rows[rowindex].FindControl("lblPivotGroupDescInList") as Label;
                    Label lblPivotGroupInList = gvPGReport.Rows[rowindex].FindControl("lblPivotGroupInList") as Label;
                    TextBox txtTotalPOValueINRInList = gvPGReport.Rows[rowindex].FindControl("txtTotalPOValueINRInList") as TextBox;

                    ViewState["PivotGroupDescInList"] = Convert.ToString(lblPivotGroupDescInList.Text).Trim();

                    mpePOList.Show();
                    txtPivotGroupInPOList.Text = Convert.ToString(lblPivotGroupDescInList.Text);
                    txtTotalPOValueINRInPOList.Text = Convert.ToString(txtTotalPOValueINRInList.Text);
                    BindPOList(Convert.ToDateTime(lblStartDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd"),
                                                  Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM"), Convert.ToString(lblJOBNo.Text).Trim(),
                                                  Convert.ToString(lblPivotGroupDescInList.Text).Trim());
                }

                if (Convert.ToString(e.CommandArgument) == "ViewDETAILSaleEstimate")
                {
                    txtTotalQty.Text = "00.00";
                    txtTotalUnitRateIndianImported1.Text = "00.00";
                    txtTotalUnitRateIndianImported2.Text = "00.00";
                    txtTotalUnitRateIndianImported.Text = "00.00";
                    txtTotalIndianCostRaised5Perc.Text = "00.00";
                    txtTotalImportedFobPrice.Text = "00.00";
                    txtTotalEquivRupeePrice.Text = "00.00";
                    txtTotalFullCustomsDutyOnImports10Perc.Text = "00.00";
                    txtTotalCostINR.Text = "00.00";
                    txtTotalCostEuro.Text = "00.00";

                    Label lblPivotGroupInList = gvPGReport.Rows[rowindex].FindControl("lblPivotGroupInList") as Label;
                    mpePivotGroupDetails.Show();
                    BindPivotGroupDetails(Convert.ToString(lblJOBNo.Text).ToUpper().Trim(), Convert.ToString(lblPivotGroupInList.Text).Trim());
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



    double totalPOValueINRInPOList = 0;
    double totalBudgetINRInPOList = 0;
    double totalDeltaINRInPOList = 0;

    protected void gvPOList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSrNo = (Label)e.Row.FindControl("lblSrNo");
                Label lblPONo = (Label)e.Row.FindControl("lblPONo");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                Label lblBudget = (Label)e.Row.FindControl("lblBudget");
                Label lblDelta = (Label)e.Row.FindControl("lblDelta");                

                totalPOValueINRInPOList += Convert.ToDouble(lblAmount.Text);
                txtTotalPOValueINRInPOList.Text = Convert.ToString(totalPOValueINRInPOList);

                if (Convert.ToInt32(lblSrNo.Text) == 1)
                {
                    totalBudgetINRInPOList += Convert.ToDouble(lblBudget.Text);
                    txtTotalBudgetInPOList.Text = Convert.ToString(totalBudgetINRInPOList);
                }

                totalDeltaINRInPOList += Convert.ToDouble(lblDelta.Text);
                txtTotalDeltaInPOList.Text = Convert.ToString(totalDeltaINRInPOList);


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

    protected void gvPOList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                Session["dtPOItemList"] = null;
                ViewState["UNIT_ID"] = 0;
                ViewState["PO_VALUE_INR"] = 0;
                ViewState["BUDGET"] = 0;
                ViewState["DELTA"] = 0;

                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "UPDATE" ||
                    Convert.ToString(e.CommandArgument) == "POST")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    Label lblLocation = gvPOList.Rows[rowindex].FindControl("lblLocation") as Label;
                    Label lblPONo = gvPOList.Rows[rowindex].FindControl("lblPONo") as Label;
                    Label lblDocClass = gvPOList.Rows[rowindex].FindControl("lblDocClass") as Label;

                    mpePOList.Show();
                    mpePOItemList.Show();
                    BindPOItemList(Convert.ToString(lblLocation.Text), Convert.ToString(lblPONo.Text), Convert.ToString(lblDocClass.Text));
                }


                ddlUnitToU.SelectedIndex = 0;
                txtJOBNoToU.Text = string.Empty;
                txtPONoToU.Text = string.Empty;
                txtOldPivotGroupToU.Text = string.Empty;
                txtOldPivotGroupDescToU.Text = string.Empty;
                txtNewPivotGroupToU.Text = string.Empty;
                txtNewPivotGroupDescToU.Text = string.Empty;

                if (Convert.ToString(e.CommandArgument) == "UPDATE")
                {
                    Label lblLocation = gvPOList.Rows[rowindex].FindControl("lblLocation") as Label;
                    Label lblPONo = gvPOList.Rows[rowindex].FindControl("lblPONo") as Label;
                    Label lblDocClass = gvPOList.Rows[rowindex].FindControl("lblDocClass") as Label;
                    Label lblUnitID = gvPOList.Rows[rowindex].FindControl("lblUnitID") as Label;
                    Label lblOldPivotGroup = gvPOList.Rows[rowindex].FindControl("lblOldPivotGroup") as Label;

                    Label lblBudget = gvPOList.Rows[rowindex].FindControl("lblBudget") as Label;
                    Label lblDelta = gvPOList.Rows[rowindex].FindControl("lblDelta") as Label;
                    Label lblAmount = gvPOList.Rows[rowindex].FindControl("lblAmount") as Label;


                    if (!string.IsNullOrEmpty(lblAmount.Text))
                        ViewState["PO_VALUE_INR"] = Convert.ToDouble(lblAmount.Text);
                    else ViewState["PO_VALUE_INR"] = 0;

                    if (!string.IsNullOrEmpty(lblBudget.Text))
                        ViewState["BUDGET"] = Convert.ToDouble(lblBudget.Text);
                    else ViewState["BUDGET"] = 0;

                    if (!string.IsNullOrEmpty(lblDelta.Text))
                        ViewState["DELTA"] = Convert.ToDouble(lblDelta.Text);
                    else ViewState["DELTA"] = 0;

                    ddlUnitToU.SelectedValue = Convert.ToString(lblUnitID.Text);
                    txtJOBNoToU.Text = lblJOBNo.Text.ToUpper().Trim();
                    txtPONoToU.Text = lblPONo.Text.ToUpper().Trim();

                    string oldPG = lblOldPivotGroup.Text.Replace(" - ", "$");
                    txtOldPivotGroupToU.Text = Convert.ToString(oldPG.Split('$')[0]).Trim();
                    txtOldPivotGroupDescToU.Text = Convert.ToString(oldPG.Split('$')[1]).Trim();
                    mpePOList.Show();
                    mpeUpdatePivotGroup.Show();
                }

                if (Convert.ToString(e.CommandArgument) == "POST")
                {
                    Label lblOldPivotGroup = gvPOList.Rows[rowindex].FindControl("lblOldPivotGroup") as Label;
                    Label lblBudget = gvPOList.Rows[rowindex].FindControl("lblBudget") as Label;
                    Label lblDelta = gvPOList.Rows[rowindex].FindControl("lblDelta") as Label;
                    Label lblAmount = gvPOList.Rows[rowindex].FindControl("lblAmount") as Label;
                    Label lblUnitID = gvPOList.Rows[rowindex].FindControl("lblUnitID") as Label;

                    ViewState["UNIT_ID"] = Convert.ToInt32(lblUnitID.Text);

                    txtJOBNoToPost.Text = lblJOBNo.Text;
                    txtPostingMonthToPost.Text = Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM");

                    string opg = lblOldPivotGroup.Text.Replace(" - ", "$");
                    txtPivotGroupDescToPost.Text = Convert.ToString(lblOldPivotGroup.Text);
                    txtPivotGroupToPost.Text = Convert.ToString(opg.Split('$')[0]);


                    BindPivotGroupSaleEstimateBudgetAndDates(Convert.ToString(lblJOBNo.Text), Convert.ToString(lblOldPivotGroup.Text), Convert.ToInt32(lblUnitID.Text));


                    if (!string.IsNullOrEmpty(lblBudget.Text))
                        txtPOBudgetToPost.Text = lblBudget.Text;
                    else txtPOBudgetToPost.Text = "0";

                    

                    if (!string.IsNullOrEmpty(lblAmount.Text))
                        txtPOValueINRToPost.Text = lblAmount.Text;
                    else txtPOValueINRToPost.Text = "0";

                    if (!string.IsNullOrEmpty(lblDelta.Text))
                        txtDeltaAsPerPOBudgetToPost.Text = lblDelta.Text;
                    else txtDeltaAsPerPOBudgetToPost.Text = "0";

                    txtPendingCostToPost.Text = "0";

                    txtDeltaAsPerSaleEstimateBudgetToPost.Text = Convert.ToString(Math.Round((Convert.ToDouble(txtSaleEstimateBudgetToPost.Text) - Convert.ToDouble(txtPOValueINRToPost.Text)), 3));

                    txtSavingCostToPost.Text = Convert.ToString(Math.Round((Convert.ToDouble(txtDeltaAsPerSaleEstimateBudgetToPost.Text) - Convert.ToDouble(txtPendingCostToPost.Text)), 3));

                    if (!string.IsNullOrEmpty(txtSaleEstimateBudgetToPost.Text) && Convert.ToDouble(txtSaleEstimateBudgetToPost.Text) > 0)
                        txtSavingofOriginalEstimatePercToPost.Text = Convert.ToString(Math.Round(((Convert.ToDouble(txtSavingCostToPost.Text) / Convert.ToDouble(txtSaleEstimateBudgetToPost.Text)) * 100), 3));
                    else txtSavingofOriginalEstimatePercToPost.Text = "0";

                    
                    mpePOList.Show();
                    mpePostPivotGroup.Show();
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



    double totalPOValue = 0;
    double totalPOQuantity = 0;
    double totalMRNQuantity = 0;
    protected void gvPOItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPOValue = (Label)e.Row.FindControl("lblPOValue");
                Label lblPOQuantity = (Label)e.Row.FindControl("lblPOQuantity");
                Label lblMRNQuantity = (Label)e.Row.FindControl("lblMRNQuantity");

                totalPOValue += Convert.ToDouble(lblPOValue.Text);
                txtTotalPOValue.Text = Convert.ToString(totalPOValue);

                totalPOQuantity += Convert.ToDouble(lblPOQuantity.Text);
                txtTotalPOQuantity.Text = Convert.ToString(totalPOQuantity);

                totalMRNQuantity += Convert.ToDouble(lblMRNQuantity.Text);
                txtTotalMRNQuantity.Text = Convert.ToString(totalMRNQuantity);

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


    protected void gvPivotGroupDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }

    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTotalPOBudgetINR.Text = string.Empty;
        txtTotalDeltaAsPerPOBudgetINR.Text = string.Empty;

        txtTotalPOValueINR1.Text = string.Empty;
        txtTotalPOValueINR2.Text = string.Empty;

        txtTotalSaleEstimateBudgetINR.Text = string.Empty;
        txtTotalDeltaAsPerSaleEstimateBudgetINR.Text = string.Empty;

        gvPGReport.DataSource = null;
        gvPGReport.DataBind();
        lblRecords.Text = "Records[0]";

        trAsPerPOBudget.Visible = true;
        trAsPerSaleEstimateBudget.Visible = true;

        //if (Convert.ToInt32(rdType.SelectedValue) == 1)
        //{
        //    trAsPerSaleEstimateBudget.Visible = false;
        //}
        //else if (Convert.ToInt32(rdType.SelectedValue) == 2)
        //{
        //    trAsPerPOBudget.Visible = false;
        //}
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            trAsPerSaleEstimateBudget.Visible = false;
        }
        else if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            trAsPerPOBudget.Visible = false;
        }
    }

    protected void chkSelectAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkSelectAll.Checked)
        {
            foreach (GridViewRow gr in gvPGReport.Rows)
            {
                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                chkSelect.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gr in gvPGReport.Rows)
            {
                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                chkSelect.Checked = false;
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvPGReport.Rows.Count > 0)
        {
            if (Convert.ToInt32(hdConfirmValue.Value) > 0)
            {
                PostPOPivotGroup();
            }
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPGReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["PIVOT_GROUP_REPORT"];
            ExportToExcel(ds.Tables[0]);
        }
    }


    protected void btnRefreshPOList_Click(object sender, EventArgs e)
    {
        mpePOList.Show();
        BindPOList(Convert.ToDateTime(lblStartDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd"),
                   Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM"), Convert.ToString(lblJOBNo.Text).Trim(),
                   Convert.ToString(ViewState["PivotGroupDescInList"]).Trim());
    }

    protected void btnAddPivotGroupLog_Click(object sender, EventArgs e)
    {
        Reset();
        mpePOList.Show();
        mpeUpdatePivotGroup.Show();
    }




    protected void btnExportPOList_Click(object sender, EventArgs e)
    {
        if (gvPOList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsPOReport"];
            ExportToExcelPOList(ds.Tables[0]);
        }
    }

    protected void btnExportPivotGroupDetail_Click(object sender, EventArgs e)
    {
        if (gvPivotGroupDetails.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsPivotGroupDetail"];
            ExportToExcelPivotGroupDetails(ds.Tables[0]);
        }
    }

    protected void btnExportPOItemList_Click(object sender, EventArgs e)
    {
        if (gvPOItemList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtPOItemList"];
            ExportToExcelPOItemList(dt);
        }
    }




    // PO DETAILS
    protected void btnGetPONoToU_Click(object sender, EventArgs e)
    {
        txtJOBNoSearch.Text = txtJOBNoToU.Text;
        txtPONoSearch.Text = txtPONoToU.Text;
        mpeOldPODetail.Show();
        GetPOOldPivotGroupDetail();
    }

    protected void btnSearchOldPivotGroupPONo_Click(object sender, EventArgs e)
    {
        mpePOList.Show();
        mpeOldPODetail.Show();
        GetPOOldPivotGroupDetail();
    }

    protected void gvPOOldPivotGroupDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblPONo = gvPOOldPivotGroupDetail.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblUnitID = gvPOOldPivotGroupDetail.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblOldPivotGroup = gvPOOldPivotGroupDetail.Rows[rowindex].FindControl("lblOldPivotGroup") as Label;
                Label lblOldPivotGroupDesc = gvPOOldPivotGroupDetail.Rows[rowindex].FindControl("lblOldPivotGroupDesc") as Label;

                txtPONoToU.Text = Convert.ToString(lblPONo.Text).Trim();
                txtOldPivotGroupToU.Text = Convert.ToString(lblOldPivotGroup.Text).Trim();
                txtOldPivotGroupDescToU.Text = Convert.ToString(lblOldPivotGroupDesc.Text).Trim();

                mpePOList.Show();
                mpeUpdatePivotGroup.Show();
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




    // NEW PIVOT GROUP DETAIL
    protected void btnGetNewPivotGroupToU_Click(object sender, EventArgs e)
    {
        HidePanelPoNewPGDetail();
        mpePOList.Show();
        mpeUpdatePivotGroup.Show();
        mpePONewPGDetail.Show();
        GetPONewPivotGroupDetail();
    }

    protected void btnSearchPONoNewPG_Click(object sender, EventArgs e)
    {
        mpePONewPGDetail.Show();
        GetPONewPivotGroupDetail();
    }

    protected void gvPONewPGDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblNewPivotGroup = gvPONewPGDetail.Rows[rowindex].FindControl("lblNewPivotGroup") as Label;
                Label lblNewPivotGroupDesc = gvPONewPGDetail.Rows[rowindex].FindControl("lblNewPivotGroupDesc") as Label;
                TextBox txtSaleEstimateBudgetInList = gvPONewPGDetail.Rows[rowindex].FindControl("txtSaleEstimateBudgetInList") as TextBox;

                Label lblLikelyDeliveryDate = gvPONewPGDetail.Rows[rowindex].FindControl("lblLikelyDeliveryDate") as Label;
                Label lblPlanDateOfProcurement = gvPONewPGDetail.Rows[rowindex].FindControl("lblPlanDateOfProcurement") as Label;

                if (!string.IsNullOrEmpty(txtSaleEstimateBudgetInList.Text) && Convert.ToDouble(txtSaleEstimateBudgetInList.Text) > 0)
                {
                    txtNewPivotGroupToU.Text = Convert.ToString(lblNewPivotGroup.Text).Trim();
                    txtNewPivotGroupDescToU.Text = Convert.ToString(lblNewPivotGroupDesc.Text).Trim();
                    txtSaleEstimateBudgetToU.Text = txtSaleEstimateBudgetInList.Text;
                    txtLikelyDeliveryDateToU.Text = Convert.ToString(lblLikelyDeliveryDate.Text);
                    txtPlannedDateOfProcurementToU.Text = Convert.ToString(lblPlanDateOfProcurement.Text);
                    mpePOList.Show();
                    mpeUpdatePivotGroup.Show();
                }
                else
                {
                    mpePONewPGDetail.Show();
                    txtSaleEstimateBudgetInList.BackColor = System.Drawing.Color.LightPink;
                    txtSaleEstimateBudgetInList.Focus();
                    ExceptionMessagePoNewPGDetail("Please enter sale estimate budget amount...!!!");
                    return;
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




    protected void btnUpdatePivotGroup_Click(object sender, EventArgs e)
    {
        SavePivotGroup();
    }


    protected void btnSavePostedPivotGroup_Click(object sender, EventArgs e)
    {
        PostPivotGroup();
    }


    #endregion


    #region METHODS[=========================]

    private void GetPGReport()
    {
        try
        {
            fromDate = string.Empty;
            toDate = string.Empty;
            JOBNo = string.Empty;
            postedMonth = string.Empty;

            if (chkSelectDates.Checked)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                    fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                    toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(lblJOBNo.Text))
                JOBNo = lblJOBNo.Text;
            postedMonth = Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM");

            dsPGReport = objReports.GetPGReportForPosting(fromDate, toDate, JOBNo, postedMonth);

            if (dsPGReport.Tables.Count > 0 && dsPGReport.Tables[0].Rows.Count > 0)
            {


                Session["PIVOT_GROUP_REPORT"] = dsPGReport;
                gvPGReport.DataSource = dsPGReport.Tables[0];
                gvPGReport.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvPGReport.Rows.Count);
                chkSelectAll.Checked = true;
                foreach (GridViewRow gr in gvPGReport.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                    chkSelect.Checked = true;
                }
            }
            else
            {
                Session["PIVOT_GROUP_REPORT"] = null;
                gvPGReport.DataSource = null;
                gvPGReport.DataBind();
                hdGVRowCount.Value = "0";
                chkSelectAll.Checked = false;
                foreach (GridViewRow gr in gvPGReport.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                    chkSelect.Checked = false;
                }

                txtTotalPOBudgetINR.Text = string.Empty;
                txtTotalDeltaAsPerPOBudgetINR.Text = string.Empty;

                txtTotalPOValueINR1.Text = string.Empty;
                txtTotalPOValueINR2.Text = string.Empty;

                txtTotalSaleEstimateBudgetINR.Text = string.Empty;
                txtTotalDeltaAsPerSaleEstimateBudgetINR.Text = string.Empty;

            }
            lblRecords.Text = "Records[" + dsPGReport.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPOList(string startDate, string endDate, string postedMonth, string JOBNo, string pivotGroup)
    {
        try
        {
            dsPOReport = objReports.GetPOReportPivotGroupWise(startDate, endDate, postedMonth, JOBNo, pivotGroup);

            if (dsPOReport.Tables.Count > 0 && dsPOReport.Tables[0].Rows.Count > 0)
            {
                Session["dsPOReport"] = dsPOReport;
                gvPOList.DataSource = dsPOReport.Tables[0];
                gvPOList.DataBind();
            }
            else
            {
                Session["dsPOReport"] = null;
                gvPOList.DataSource = null;
                gvPOList.DataBind();
            }
            lblPOListRecords.Text = "Records[" + dsPOReport.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void BindPOItemList(string unit, string poNo, string docClass)
    {
        try
        {
            DataSet dsDetail = new DataSet();
            dsDetail = objReports.GetPODetail(unit, poNo, docClass);
            if (dsDetail.Tables.Count > 0)
            {
                if (dsDetail.Tables[0].Rows.Count > 0)
                {
                    if (dsDetail.Tables[0].Rows[0]["PO_NO"] != DBNull.Value)
                        txtPONo.Text = Convert.ToString(dsDetail.Tables[0].Rows[0]["PO_NO"]);
                    else
                        txtPONo.Text = string.Empty;

                    if (dsDetail.Tables[0].Rows[0]["PO_DATE"] != DBNull.Value)
                        txtPODate.Text = Convert.ToDateTime(dsDetail.Tables[0].Rows[0]["PO_DATE"]).ToString("dd-MMM-yyyy");
                    else
                        txtPODate.Text = string.Empty;

                    if (dsDetail.Tables[0].Rows[0]["VENDOR_NAME"] != DBNull.Value)
                        txtVendorName.Text = Convert.ToString(dsDetail.Tables[0].Rows[0]["VENDOR_NAME"]);
                    else
                        txtVendorName.Text = string.Empty;

                    if (dsDetail.Tables[0].Rows[0]["VENDOR_CODE"] != DBNull.Value)
                        txtVendorCode.Text = Convert.ToString(dsDetail.Tables[0].Rows[0]["VENDOR_CODE"]);
                    else
                        txtVendorCode.Text = string.Empty;

                    if (dsDetail.Tables[0].Rows[0]["INVOICE_VALUE"] != DBNull.Value)
                        txtTotalInvoiceValue.Text = Convert.ToString(dsDetail.Tables[0].Rows[0]["INVOICE_VALUE"]);
                    else
                        txtTotalInvoiceValue.Text = string.Empty;
                }

                if (dsDetail.Tables[1].Rows.Count > 0)
                {
                    Session["dtPOItemList"] = dsDetail.Tables[1];
                    gvPOItemList.DataSource = dsDetail.Tables[1];
                    gvPOItemList.DataBind();
                }
            }
            else
            {
                Session["dtPOItemList"] = null;
                gvPOItemList.DataSource = null;
                gvPOItemList.DataBind();
                Response.Redirect("~/PROJECT_MGMT/ProductList.aspx");
            }
            lblPOItemListRecords.Text = "Records[" + gvPOItemList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }


    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 2; i < dt.Columns.Count - 2; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 2; k < dt.Columns.Count - 2; k++)
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


            string fileName = "PO_Pivot_Group_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcelPOList(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 2; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 2; k < dt.Columns.Count; k++)
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


            string fileName = "PO_List" + DateTime.Now.ToString("dd_MMM_yyyy");
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcelPivotGroupDetails(DataTable dt)
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


            string fileName = "PO_Pivot_Group_Details_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcelPOItemList(DataTable dt)
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


            string fileName = "PO_Item_List_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPivotGroupDetails(string jobNo, string pivotGroup)
    {
        try
        {
            dsPivotGroupDetail = objReports.GetPivotGroupDetails(jobNo, pivotGroup);

            if (dsPivotGroupDetail.Tables.Count > 0 && dsPivotGroupDetail.Tables[0].Rows.Count > 0)
            {
                Session["dsPivotGroupDetail"] = dsPivotGroupDetail;
                gvPivotGroupDetails.DataSource = dsPivotGroupDetail.Tables[0];
                gvPivotGroupDetails.DataBind();

                if (dsPivotGroupDetail.Tables[1].Rows.Count > 0)
                {
                    DataRow dr = dsPivotGroupDetail.Tables[1].Rows[0];
                    txtTotalQty.Text = Convert.ToString(dr["QTY"]);
                    txtTotalUnitRateIndianImported1.Text = Convert.ToString(dr["UNIT_RATE_INDIAN_IMPORTED1"]);
                    txtTotalUnitRateIndianImported2.Text = Convert.ToString(dr["UNIT_RATE_INDIAN_IMPORTED2"]);
                    txtTotalUnitRateIndianImported.Text = Convert.ToString(dr["TOTAL_UNIT_RATE_INDIAN_IMPORTED"]);
                    txtTotalIndianCostRaised5Perc.Text = Convert.ToString(dr["INDIAN_COST_RAISED_5_PERC"]);
                    txtTotalImportedFobPrice.Text = Convert.ToString(dr["IMPORTED_FOB_PRICE"]);
                    txtTotalEquivRupeePrice.Text = Convert.ToString(dr["EQUIV_RUPEE_PRICE"]);
                    txtTotalFullCustomsDutyOnImports10Perc.Text = Convert.ToString(dr["FULL_CUSTOMS_DUTY_ON_IMPORTS_10_PERC"]);
                    txtTotalCostINR.Text = Convert.ToString(dr["TOTAL_COST_INR"]);
                    txtTotalCostEuro.Text = Convert.ToString(dr["TOTAL_COST_EURO"]);
                }
                else
                {
                    txtTotalQty.Text = "00.00";
                    txtTotalUnitRateIndianImported1.Text = "00.00";
                    txtTotalUnitRateIndianImported2.Text = "00.00";
                    txtTotalUnitRateIndianImported.Text = "00.00";
                    txtTotalIndianCostRaised5Perc.Text = "00.00";
                    txtTotalImportedFobPrice.Text = "00.00";
                    txtTotalEquivRupeePrice.Text = "00.00";
                    txtTotalFullCustomsDutyOnImports10Perc.Text = "00.00";
                    txtTotalCostINR.Text = "00.00";
                    txtTotalCostEuro.Text = "00.00";
                }
            }
            else
            {
                Session["dsPivotGroupDetail"] = null;
                gvPivotGroupDetails.DataSource = null;
                gvPivotGroupDetails.DataBind();
            }

            lblPivotGroupDetailsLegend.Text = "Pivot Group [" + pivotGroup + "] details";
            lblPivotGroupDetailsRecords.Text = "Records[" + dsPivotGroupDetail.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void PostPOPivotGroup()
    {
        try
        {
            int count = 0;
            string srNoForRemoval = string.Empty;
            string tableName = string.Empty;

            string jobNo = string.Empty;
            string postingMonth = string.Empty;
            int postingYear = 0;
            int sRNo = 0;
            int recordID = 0;
            string pivotGroupDesc = string.Empty;
            string pivotGroup = string.Empty;
            double totalBudgetINR = 0;
            double saleEstimateBudgetINR = 0;
            double totalPOValueINR = 0;
            double totalDeltaAsPerPOBudgetINR = 0;
            double totalDeltaAsPerSaleEstimateBudgetINR = 0;
            string status = string.Empty;
            double pendingCost = 0;
            double saving = 0;
            double savingOfOriginalEstimatePercentage = 0;
            int VPOC = 0;
            string likelyDeliveryDate = string.Empty;
            string planDateOfProcurement = string.Empty;


            DataTable dtPOPivotGroup = new DataTable();

            dtPOPivotGroup.Columns.Add("JOB_NO", typeof(string));
            dtPOPivotGroup.Columns.Add("POSTING_MONTH", typeof(string));
            dtPOPivotGroup.Columns.Add("POSTING_YEAR", typeof(int));
            dtPOPivotGroup.Columns.Add("PIVOT_GROUP_DESC", typeof(string));
            dtPOPivotGroup.Columns.Add("PIVOT_GROUP", typeof(string));
            dtPOPivotGroup.Columns.Add("PO_BUDGET", typeof(double));
            dtPOPivotGroup.Columns.Add("SALE_ESTIMATE_BUDGET", typeof(double));
            dtPOPivotGroup.Columns.Add("PO_VALUE_INR", typeof(double));
            dtPOPivotGroup.Columns.Add("DELTA_AS_PER_PO_BUDGET", typeof(double));
            dtPOPivotGroup.Columns.Add("DELTA_AS_PER_SALE_ESTIMATE_BUDGET", typeof(double));
            dtPOPivotGroup.Columns.Add("STATUS", typeof(string));
            dtPOPivotGroup.Columns.Add("PENDING_COST", typeof(double));
            dtPOPivotGroup.Columns.Add("SAVING_COST", typeof(double));
            dtPOPivotGroup.Columns.Add("SAVING_OF_ORIGINAL_ESTIMATE_PERCENTAGE", typeof(double));
            dtPOPivotGroup.Columns.Add("IS_VPOC", typeof(int));
            dtPOPivotGroup.Columns.Add("LIKELY_DELIVERY_DATE", typeof(string));
            dtPOPivotGroup.Columns.Add("PLAN_DATE_OF_PROCUREMENT", typeof(string));

            int value = 0;
            string updateQuery = string.Empty;
            foreach (GridViewRow gr in gvPGReport.Rows)
            {
                CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");

                if (chkSelect.Checked)
                {
                    count++;

                    DataRow dr = dtPOPivotGroup.NewRow();

                    TextBox txtSrNoInList = (TextBox)gr.FindControl("txtSrNoInList");
                    Label lblRecordIDInList = (Label)gr.FindControl("lblRecordIDInList");
                    Label lblJOBNoInList = (Label)gr.FindControl("lblJOBNoInList");
                    Label lblPivotGroupDescInList = (Label)gr.FindControl("lblPivotGroupDescInList");
                    Label lblPivotGroupInList = (Label)gr.FindControl("lblPivotGroupInList");
                    //TextBox txtTotalBudgetINRInList = (TextBox)gr.FindControl("txtTotalBudgetINRInList");
                    //TextBox txtSaleEstimateBudgetINRInList = (TextBox)gr.FindControl("txtSaleEstimateBudgetINRInList");

                    Label lblTotalBudgetINRInList = (Label)gr.FindControl("lblTotalBudgetINRInList");
                    Label lblSaleEstimateBudgetINRInList = (Label)gr.FindControl("lblSaleEstimateBudgetINRInList");

                    TextBox txtTotalPOValueINRInList = (TextBox)gr.FindControl("txtTotalPOValueINRInList");

                    //TextBox txtTotalDeltaAsPerPOBudgetINRInList = (TextBox)gr.FindControl("txtTotalDeltaAsPerPOBudgetINRInList");
                    Label lblTotalDeltaAsPerPOBudgetINRInList = (Label)gr.FindControl("lblTotalDeltaAsPerPOBudgetINRInList");

                    //TextBox txtTotalDeltaAsPerSaleEstimateBudgetINRInList = (TextBox)gr.FindControl("txtTotalDeltaAsPerSaleEstimateBudgetINRInList");
                    Label lblTotalDeltaAsPerSaleEstimateBudgetINRInList = (Label)gr.FindControl("lblTotalDeltaAsPerSaleEstimateBudgetINRInList");

                    DropDownList ddlStatusInList = (DropDownList)gr.FindControl("ddlStatusInList");
                    TextBox txtPendingCostInList = (TextBox)gr.FindControl("txtPendingCostInList");
                    TextBox txtSavingInList = (TextBox)gr.FindControl("txtSavingInList");
                    TextBox txtSavingOfOriginalEstimatePercentageInList = (TextBox)gr.FindControl("txtSavingOfOriginalEstimatePercentageInList");
                    DropDownList ddlVPOCInList = (DropDownList)gr.FindControl("ddlVPOCInList");
                    Label lblLikelyDeliveryDateInList = (Label)gr.FindControl("lblLikelyDeliveryDateInList");
                    Label lblPlanDateOfProcurementInList = (Label)gr.FindControl("lblPlanDateOfProcurementInList");
                    Label lblTableNameInList = (Label)gr.FindControl("lblTableNameInList");


                    sRNo = Convert.ToInt32(txtSrNoInList.Text);
                    tableName = Convert.ToString(lblTableNameInList.Text);

                    postingMonth = Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM");
                    postingYear = Convert.ToInt32(Convert.ToDateTime(lblPostingMonth.Text).Year);

                    if (Convert.ToInt32(lblRecordIDInList.Text) > 0)
                        recordID = Convert.ToInt32(lblRecordIDInList.Text);
                    else recordID = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNoInList.Text)))
                        jobNo = Convert.ToString(lblJOBNoInList.Text);
                    else jobNo = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPivotGroupInList.Text)))
                        pivotGroup = Convert.ToString(lblPivotGroupInList.Text);
                    else pivotGroup = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPivotGroupDescInList.Text)))
                        pivotGroupDesc = Convert.ToString(lblPivotGroupDescInList.Text);
                    else pivotGroupDesc = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblTotalBudgetINRInList.Text)))//&& Convert.ToDouble(lblTotalBudgetINRInList.Text) > 0
                        totalBudgetINR = Convert.ToDouble(lblTotalBudgetINRInList.Text);
                    else totalBudgetINR = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblSaleEstimateBudgetINRInList.Text)))//&& Convert.ToDouble(lblSaleEstimateBudgetINRInList.Text) > 0
                        saleEstimateBudgetINR = Convert.ToDouble(lblSaleEstimateBudgetINRInList.Text);
                    else saleEstimateBudgetINR = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtTotalPOValueINRInList.Text)))//&& Convert.ToDouble(txtTotalPOValueINRInList.Text) > 0
                        totalPOValueINR = Convert.ToDouble(txtTotalPOValueINRInList.Text);
                    else totalPOValueINR = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblTotalDeltaAsPerPOBudgetINRInList.Text)))//&& Convert.ToDouble(txtTotalDeltaAsPerPOBudgetINRInList.Text) > 0
                        totalDeltaAsPerPOBudgetINR = Convert.ToDouble(lblTotalDeltaAsPerPOBudgetINRInList.Text);
                    else totalDeltaAsPerPOBudgetINR = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblTotalDeltaAsPerSaleEstimateBudgetINRInList.Text)))//&& Convert.ToDouble(txtTotalDeltaAsPerSaleEstimateBudgetINRInList.Text) > 0
                        totalDeltaAsPerSaleEstimateBudgetINR = Convert.ToDouble(lblTotalDeltaAsPerSaleEstimateBudgetINRInList.Text);
                    else totalDeltaAsPerSaleEstimateBudgetINR = 0;

                    status = Convert.ToString(ddlStatusInList.SelectedValue);

                    if (!string.IsNullOrEmpty(Convert.ToString(txtPendingCostInList.Text)))//&& Convert.ToDouble(txtPendingCostInList.Text) > 0
                        pendingCost = Convert.ToDouble(txtPendingCostInList.Text);
                    else pendingCost = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtSavingInList.Text)))//&& Convert.ToDouble(txtSavingInList.Text) > 0
                        saving = Convert.ToDouble(txtSavingInList.Text);
                    else saving = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtSavingOfOriginalEstimatePercentageInList.Text)))//&& Convert.ToDouble(txtSavingOfOriginalEstimatePercentageInList.Text) > 0
                        savingOfOriginalEstimatePercentage = Convert.ToDouble(txtSavingOfOriginalEstimatePercentageInList.Text);
                    else savingOfOriginalEstimatePercentage = 0;

                    VPOC = Convert.ToInt32(ddlVPOCInList.SelectedValue);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblLikelyDeliveryDateInList.Text)))
                        likelyDeliveryDate = Convert.ToDateTime(lblLikelyDeliveryDateInList.Text).ToString("yyyy-MM-dd");
                    else likelyDeliveryDate = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPlanDateOfProcurementInList.Text)))
                        planDateOfProcurement = Convert.ToDateTime(lblPlanDateOfProcurementInList.Text).ToString("yyyy-MM-dd");
                    else planDateOfProcurement = string.Empty;

                    srNoForRemoval += sRNo + ",";
                    if (recordID > 0)
                    {
                        updateQuery += "UPDATE " + tableName + " " +
                                        "SET " +
                                        "JOB_NO='" + jobNo + "'," +
                                        "POSTING_MONTH='" + postingMonth + "'," +
                                        "POSTING_YEAR=" + postingYear + "," +
                                        "PIVOT_GROUP_DESC='" + pivotGroupDesc + "'," +
                                        "PIVOT_GROUP='" + pivotGroup + "'," +
                                        "PO_BUDGET='" + totalBudgetINR + "'," +
                                        "SALE_ESTIMATE_BUDGET='" + saleEstimateBudgetINR + "'," +
                                        "PO_VALUE_INR='" + totalPOValueINR + "'," +
                                        "DELTA_AS_PER_PO_BUDGET='" + totalDeltaAsPerPOBudgetINR + "'," +
                                        "DELTA_AS_PER_SALE_ESTIMATE_BUDGET='" + totalDeltaAsPerSaleEstimateBudgetINR + "'," +
                                        "STATUS='" + status + "'," +
                                        "PENDING_COST='" + pendingCost + "'," +
                                        "SAVING_COST='" + saving + "'," +
                                        "SAVING_OF_ORIGINAL_ESTIMATE_PERCENTAGE='" + savingOfOriginalEstimatePercentage + "'," +
                                        "IS_VPOC=" + VPOC + "," +
                                        "LIKELY_DELIVERY_DATE='" + likelyDeliveryDate + "'," +
                                        "PLAN_DATE_OF_PROCUREMENT='" + planDateOfProcurement + "'," +
                                        "MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "," +
                                        "MODIFIED_ON=GETDATE()" +
                                        "WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(pivotGroup))
                        {
                            dr["JOB_NO"] = jobNo;
                            dr["POSTING_MONTH"] = postingMonth;
                            dr["POSTING_YEAR"] = postingYear;
                            dr["PIVOT_GROUP_DESC"] = pivotGroupDesc;
                            dr["PIVOT_GROUP"] = pivotGroup;
                            dr["PO_BUDGET"] = totalBudgetINR;
                            dr["SALE_ESTIMATE_BUDGET"] = saleEstimateBudgetINR;
                            dr["PO_VALUE_INR"] = totalPOValueINR;
                            dr["DELTA_AS_PER_PO_BUDGET"] = totalDeltaAsPerPOBudgetINR;
                            dr["DELTA_AS_PER_SALE_ESTIMATE_BUDGET"] = totalDeltaAsPerSaleEstimateBudgetINR;
                            dr["STATUS"] = status;
                            dr["PENDING_COST"] = pendingCost;
                            dr["SAVING_COST"] = saving;
                            dr["SAVING_OF_ORIGINAL_ESTIMATE_PERCENTAGE"] = savingOfOriginalEstimatePercentage;
                            dr["IS_VPOC"] = VPOC;
                            dr["LIKELY_DELIVERY_DATE"] = likelyDeliveryDate;
                            dr["PLAN_DATE_OF_PROCUREMENT"] = planDateOfProcurement;

                            dtPOPivotGroup.Rows.Add(dr);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(updateQuery))
                updateQuery = updateQuery.TrimEnd(';');


            value = objReports.PostPOPivotGroup(dtPOPivotGroup, updateQuery, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage(count + " Records posted successfully.");
                GetPGReport();

                //if (!string.IsNullOrEmpty(srNoForRemoval))
                //    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                //if (!string.IsNullOrEmpty(srNoForRemoval))
                //    RemoveAndBind(srNoForRemoval);
                //else
                //{
                //    gvPGReport.DataSource = null;
                //    gvPGReport.DataBind();
                //    lblRecords.Text = "Records[" + gvPGReport.Rows.Count + "]";
                //}
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveAndBind(string srNoForRemoval)
    {
        try
        {
            DataSet ds1 = (DataSet)Session["PIVOT_GROUP_REPORT"];
            dt1 = ds1.Tables[0];
            string[] strNoForRemoval = srNoForRemoval.Split(',');
            foreach (string item in strNoForRemoval)
            {
                foreach (DataRow dr in dt1.Select("SR_NO='" + item + "'"))
                {
                    dt1.Rows.Remove(dr);
                }
            }
            if (dt1.Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow dr in dt1.Rows)
                {
                    count++;
                    dr["SR_NO"] = count;
                }

                gvPGReport.DataSource = dt1;
                gvPGReport.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvPGReport.Rows.Count);
            }
            else
            {
                gvPGReport.DataSource = null;
                gvPGReport.DataBind();
                hdGVRowCount.Value = "0";
            }
            lblRecords.Text = "Records[" + gvPGReport.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }




    private void BindUnitToU()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnitToU.DataSource = dsUnit.Tables[0];
                ddlUnitToU.DataTextField = "UNIT_NAME";
                ddlUnitToU.DataValueField = "UNIT_ID";
                ddlUnitToU.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private DataSet GetPOOldPivotGroupData()
    {
        try
        {
            int unitID = 0;
            string JOBNo = string.Empty;
            string poNo = string.Empty;

            unitID = Convert.ToInt32(ddlUnitToU.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                JOBNo = txtJOBNoSearch.Text;

            if (!string.IsNullOrEmpty(txtPONoSearch.Text))
                poNo = txtPONoSearch.Text;

            dsPOOldPivotGroup = objReports.GetPODetailForPivotGroup(unitID, JOBNo, poNo);
            if (dsPOOldPivotGroup.Tables.Count > 0)
            {
                return dsPOOldPivotGroup;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void GetPOOldPivotGroupDetail()
    {
        try
        {
            dsPOOldPivotGroup = GetPOOldPivotGroupData();
            if (dsPOOldPivotGroup.Tables.Count > 0 && dsPOOldPivotGroup.Tables[0].Rows.Count > 0)
            {
                lblPOMsg.Visible = false;
                lblPOMsg.Text = string.Empty;
                gvPOOldPivotGroupDetail.DataSource = dsPOOldPivotGroup.Tables[0];
                gvPOOldPivotGroupDetail.DataBind();
            }
            else
            {
                lblPOMsg.Visible = true;
                lblPOMsg.Text = "No data found!";
                gvPOOldPivotGroupDetail.DataSource = null;
                gvPOOldPivotGroupDetail.DataBind();
            }
            lblPORecords.Text = "Records[" + gvPOOldPivotGroupDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }


    private DataTable GetPONewPivotGroupData()
    {
        try
        {
            DataTable dtPV = new DataTable();
            dtPV.Columns.Add("NEW_PIVOT_GROUP", typeof(string));
            dtPV.Columns.Add("NEW_PIVOT_GROUP_DESC", typeof(string));

            DataTable dtPVNew = new DataTable();
            dtPVNew.Columns.Add("NEW_PIVOT_GROUP", typeof(string));
            dtPVNew.Columns.Add("NEW_PIVOT_GROUP_DESC", typeof(string));
            dtPVNew.Columns.Add("SALE_ESTIMATE_BUDGET", typeof(string));
            dtPVNew.Columns.Add("LIKELY_DELIVERY_DATE", typeof(string));
            dtPVNew.Columns.Add("PLAN_DATE_OF_PROCUREMENT", typeof(string));

            int unitID = 0;
            string jobNo = string.Empty;
            string pivotGroup = string.Empty;
            string pivotGroupDesc = string.Empty;

            unitID = Convert.ToInt32(ddlUnitToU.SelectedValue);

            if (!string.IsNullOrEmpty(lblJOBNo.Text))
                jobNo = lblJOBNo.Text.ToUpper().Trim();

            if (!string.IsNullOrEmpty(txtPivotGroupSearch.Text))
                pivotGroup = txtPivotGroupSearch.Text;

            if (!string.IsNullOrEmpty(txtPivotGroupDescSearch.Text))
                pivotGroupDesc = txtPivotGroupDescSearch.Text;

            string pivotGroupText = string.Empty;
            string pivotGroupDescText = string.Empty;

            dsNewPivotGroup = objReports.GetNewPivotGroupForPO(unitID, jobNo, pivotGroup, pivotGroupDesc);

            if (dsNewPivotGroup.Tables.Count > 0 && dsNewPivotGroup.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsNewPivotGroup.Tables[0].Rows[0];
                string[] strPVComp = Convert.ToString(dr["PIVOT_GROUP"]).Split(';');

                if (strPVComp.Length > 0)
                {
                    foreach (string sr in strPVComp)
                    {
                        if (!string.IsNullOrEmpty(sr))
                        {
                            pivotGroupText = string.Empty;
                            pivotGroupDescText = string.Empty;

                            string[] strPV = sr.Split('$');
                            if (strPV.Length > 0)
                            {
                                pivotGroupDescText = sr.Trim().Replace("$", " - ");

                                if (!string.IsNullOrEmpty(Convert.ToString(strPV[0])))
                                    pivotGroupText = Convert.ToString(strPV[0]).Trim();
                            }
                        }

                        DataRow drn = dtPV.NewRow();
                        drn["NEW_PIVOT_GROUP"] = pivotGroupText;
                        drn["NEW_PIVOT_GROUP_DESC"] = pivotGroupDescText;

                        dtPV.Rows.Add(drn);
                    }
                }


                if (dtPV.Rows.Count > 0)
                {

                    if (dsNewPivotGroup.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dsNewPivotGroup.Tables[1].Rows)
                        {
                            foreach (DataRow dr2 in dtPV.Select("NEW_PIVOT_GROUP='" + Convert.ToString(dr1["PIVOT_GROUP"]) + "'"))
                            {
                                DataRow drc = dtPVNew.NewRow();
                                drc["NEW_PIVOT_GROUP"] = Convert.ToString(dr1["PIVOT_GROUP"]);
                                drc["NEW_PIVOT_GROUP_DESC"] = Convert.ToString(dr1["PIVOT_GROUP_DESC"]);
                                drc["LIKELY_DELIVERY_DATE"] = Convert.ToString(dr1["LIKELY_DELIVERY_DATE"]);
                                drc["PLAN_DATE_OF_PROCUREMENT"] = Convert.ToString(dr1["PLAN_DATE_OF_PROCUREMENT"]);

                                dtPVNew.Rows.Add(drc);
                            }
                        }
                    }

                    if (dtPVNew.Rows.Count > 0)
                    {
                        if (dsNewPivotGroup.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dtPVNew.Rows)
                            {
                                foreach (DataRow dr2 in dsNewPivotGroup.Tables[2].Rows)
                                {
                                    string saleEstimateBudget = string.Empty;
                                    if (Convert.ToString(dr1["NEW_PIVOT_GROUP"]) == Convert.ToString(dr2["PIVOT_GROUP"]) &&
                                        Convert.ToString(lblJOBNo.Text) == Convert.ToString(dr2["JOB_NO"]))
                                    {
                                        dr1["SALE_ESTIMATE_BUDGET"] = Convert.ToString(dr2["SALE_ESTIMATE_BUDGET"]);
                                    }
                                }
                            }
                        }


                        return dtPVNew;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void GetPONewPivotGroupDetail()
    {
        try
        {
            DataTable dt = new DataTable();

            dt = GetPONewPivotGroupData();
            if (dt.Rows.Count > 0)
            {
                lblPONewPGMsg.Visible = false;
                lblPONewPGMsg.Text = string.Empty;
                gvPONewPGDetail.DataSource = dt;
                gvPONewPGDetail.DataBind();
            }
            else
            {
                lblPONewPGMsg.Visible = true;
                lblPONewPGMsg.Text = "No data found!";
                gvPONewPGDetail.DataSource = null;
                gvPONewPGDetail.DataBind();
            }
            lblPONewPGRecords.Text = "Records[" + gvPONewPGDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void BindPivotGroupSaleEstimateBudgetAndDates(string jobNo, string pivotGroupDesc, int unitID)
    {
        try
        {
            DataSet dsDates = new DataSet();
            dsDates = objReports.GetNewPivotGroupSaleEstimateBudgetAndDates(unitID, jobNo, pivotGroupDesc);

            if (dsDates.Tables.Count > 0 && dsDates.Tables[0].Rows.Count > 0)
            {
                DataRow dr0 = dsDates.Tables[0].Rows[0];

                if (dr0["SALE_ESTIMATE_BUDGET"] != DBNull.Value)
                    txtSaleEstimateBudgetToPost.Text = Convert.ToString(dr0["SALE_ESTIMATE_BUDGET"]);
                else txtSaleEstimateBudgetToPost.Text = "0";

                if (dr0["LIKELY_DELIVERY_DATE"] != DBNull.Value)
                    txtLikelyDeliveryDateToPost.Text = Convert.ToString(dr0["LIKELY_DELIVERY_DATE"]);
                else txtLikelyDeliveryDateToPost.Text = string.Empty;


                if (dr0["PLAN_DATE_OF_PROCUREMENT"] != DBNull.Value)
                    txtPlannedDateOfProcurementToPost.Text = Convert.ToString(dr0["PLAN_DATE_OF_PROCUREMENT"]);
                else txtPlannedDateOfProcurementToPost.Text = string.Empty;
            }
            else
            {
                txtLikelyDeliveryDateToPost.Text = string.Empty;
                txtPlannedDateOfProcurementToPost.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void SavePivotGroup()
    {
        try
        {
            int isPostingFlag = 1;
            int isPivotGroupLogFlag = 1;
            int unitID = 0;
            string jobNo = string.Empty;
            string poNo = string.Empty;
            string oldPV = string.Empty;
            string oldPVDesc = string.Empty;
            string newPV = string.Empty;
            string newPVDesc = string.Empty;


            string postingMonth = string.Empty;
            int postingYear = 0;
            double poBudget = 0;
            double saleEstimateBudget = 0;
            double poValueINR = 0;
            double deltaAsPerPOBudget = 0;
            double deltaAsPerSaleEstimateBudget = 0;
            double pendingCost = 0;
            double savingCost = 0;
            double savingOfOriginalEstimatePercentage = 0;
            string likelyDeliveryDate = string.Empty;
            string plannedDateOfProcurement = string.Empty;

            unitID = Convert.ToInt32(ddlUnitToU.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNoToU.Text))
                jobNo = txtJOBNoToU.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtPONoToU.Text))
                poNo = txtPONoToU.Text.Trim();

            if (!string.IsNullOrEmpty(txtOldPivotGroupToU.Text))
                oldPV = txtOldPivotGroupToU.Text.Trim();

            if (!string.IsNullOrEmpty(txtOldPivotGroupDescToU.Text))
                oldPVDesc = txtOldPivotGroupDescToU.Text.Trim();

            if (!string.IsNullOrEmpty(txtNewPivotGroupToU.Text))
                newPV = txtNewPivotGroupToU.Text.Trim();

            if (!string.IsNullOrEmpty(txtNewPivotGroupDescToU.Text))
                newPVDesc = txtNewPivotGroupDescToU.Text.Trim();

            postingMonth = Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM");
            postingYear = Convert.ToInt32(postingMonth.Substring(0, 4));

            if (Convert.ToDouble(ViewState["PO_VALUE_INR"]) > 0)
                poValueINR = Convert.ToDouble(ViewState["PO_VALUE_INR"]);

            if (Convert.ToDouble(ViewState["BUDGET"]) > 0)
                poBudget = Convert.ToDouble(ViewState["BUDGET"]);

            if (ViewState["DELTA"] != null)
                deltaAsPerPOBudget = Convert.ToDouble(ViewState["DELTA"]);


            if (!string.IsNullOrEmpty(txtSaleEstimateBudgetToU.Text) && Convert.ToDouble(txtSaleEstimateBudgetToU.Text) > 0)
                saleEstimateBudget = Convert.ToDouble(txtSaleEstimateBudgetToU.Text);

            deltaAsPerSaleEstimateBudget = saleEstimateBudget - poValueINR;

            savingCost = deltaAsPerSaleEstimateBudget - pendingCost;

            if (saleEstimateBudget > 0)
                savingOfOriginalEstimatePercentage = savingCost / saleEstimateBudget;

            if (!string.IsNullOrEmpty(txtLikelyDeliveryDateToU.Text))
                likelyDeliveryDate = Convert.ToDateTime(txtLikelyDeliveryDateToU.Text).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtPlannedDateOfProcurementToU.Text))
                plannedDateOfProcurement = Convert.ToDateTime(txtPlannedDateOfProcurementToU.Text).ToString("yyyy-MM-dd");

            int value = 0;

            if (saleEstimateBudget > 0)
            {
                value = objReports.InsertPOPivotGroupLogOne(unitID, jobNo, poNo, newPV, newPVDesc, oldPV, oldPVDesc, 
                                                        isPostingFlag,
                                                        isPivotGroupLogFlag,
                                                        postingMonth,
                                                        postingYear,
                                                        poBudget,
                                                        saleEstimateBudget,
                                                        poValueINR,
                                                        deltaAsPerPOBudget,
                                                        deltaAsPerSaleEstimateBudget,
                                                        pendingCost,
                                                        savingCost,
                                                        savingOfOriginalEstimatePercentage,
                                                        likelyDeliveryDate,
                                                        plannedDateOfProcurement,
                                                        Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }
            else
            {
                ExceptionMessagePoNewPGDetail("Please enter sale estimate budget amount...!!!");
                mpePOList.Show();
                mpePostPivotGroup.Show();                
                return;
            }

            if (value > 0)
            {
                SuccessMessagePoList("Log pivot group updated successfully...!!!");

                GetPGReport();
                BindPOList(Convert.ToDateTime(lblStartDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd"),
                           Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM"), Convert.ToString(lblJOBNo.Text).Trim(),
                           Convert.ToString(ViewState["PivotGroupDescInList"]).Trim());
                mpePOList.Show();
                Reset();
                return;
            }
            else
            {
                ExceptionMessagePoList("Please try again...!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void PostPivotGroup()
    {
        try
        {
            int isPostingFlag = 1;
            int isPivotGroupLogFlag = 0;
            int unitID = 0;
            string jobNo = string.Empty;
            string poNo = string.Empty;
            string oldPV = string.Empty;
            string oldPVDesc = string.Empty;
            string newPV = string.Empty;
            string newPVDesc = string.Empty;


            string postingMonth = string.Empty;
            int postingYear = 0;
            double poBudget = 0;
            double saleEstimateBudget = 0;
            double poValueINR = 0;
            double deltaAsPerPOBudget = 0;
            double deltaAsPerSaleEstimateBudget = 0;
            double pendingCost = 0;
            double savingCost = 0;
            double savingOfOriginalEstimatePercentage = 0;
            string likelyDeliveryDate = string.Empty;
            string plannedDateOfProcurement = string.Empty;

            unitID = Convert.ToInt32(ViewState["UNIT_ID"]);

            if (!string.IsNullOrEmpty(txtJOBNoToPost.Text))
                jobNo = txtJOBNoToPost.Text.Trim().ToUpper();

            //if (!string.IsNullOrEmpty(txtPONoToU.Text))
            //    poNo = txtPONoToU.Text.Trim();

            //if (!string.IsNullOrEmpty(txtOldPivotGroupToU.Text))
            //    oldPV = txtOldPivotGroupToU.Text.Trim();

            //if (!string.IsNullOrEmpty(txtOldPivotGroupDescToU.Text))
            //    oldPVDesc = txtOldPivotGroupDescToU.Text.Trim();

            if (!string.IsNullOrEmpty(txtPivotGroupToPost.Text))
                newPV = txtPivotGroupToPost.Text.Trim();

            if (!string.IsNullOrEmpty(txtPivotGroupDescToPost.Text))
                newPVDesc = txtPivotGroupDescToPost.Text.Trim();

            postingMonth = Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM");
            postingYear = Convert.ToInt32(postingMonth.Substring(0, 4));

            if (Convert.ToDouble(txtPOValueINRToPost.Text) > 0)
                poValueINR = Convert.ToDouble(txtPOValueINRToPost.Text);

            if (Convert.ToDouble(txtPOBudgetToPost.Text) > 0)
                poBudget = Convert.ToDouble(txtPOBudgetToPost.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtPOBudgetToPost.Text)))
                deltaAsPerPOBudget = Convert.ToDouble(txtPOBudgetToPost.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtSaleEstimateBudgetToPost.Text)))
                saleEstimateBudget = Convert.ToDouble(txtSaleEstimateBudgetToPost.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtDeltaAsPerSaleEstimateBudgetToPost.Text)))
                deltaAsPerSaleEstimateBudget = Convert.ToDouble(txtDeltaAsPerSaleEstimateBudgetToPost.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtPendingCostToPost.Text)))
                pendingCost = Convert.ToDouble(txtPendingCostToPost.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtSavingCostToPost.Text)))
                savingCost = Convert.ToDouble(txtSavingCostToPost.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtSavingofOriginalEstimatePercToPost.Text)))
                savingOfOriginalEstimatePercentage = Convert.ToDouble(txtSavingofOriginalEstimatePercToPost.Text);

            if (!string.IsNullOrEmpty(txtLikelyDeliveryDateToPost.Text))
                likelyDeliveryDate = Convert.ToDateTime(txtLikelyDeliveryDateToPost.Text).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtPlannedDateOfProcurementToPost.Text))
                plannedDateOfProcurement = Convert.ToDateTime(txtPlannedDateOfProcurementToPost.Text).ToString("yyyy-MM-dd");

            int value = 0;

            if (saleEstimateBudget > 0)
            {
                value = objReports.InsertPOPivotGroupLogOne(unitID, jobNo, poNo, newPV, newPVDesc, oldPV, oldPVDesc,
                                                        isPostingFlag,
                                                        isPivotGroupLogFlag,
                                                        postingMonth,
                                                        postingYear,
                                                        poBudget,
                                                        saleEstimateBudget,
                                                        poValueINR,
                                                        deltaAsPerPOBudget,
                                                        deltaAsPerSaleEstimateBudget,
                                                        pendingCost,
                                                        savingCost,
                                                        savingOfOriginalEstimatePercentage,
                                                        likelyDeliveryDate,
                                                        plannedDateOfProcurement,
                                                        Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }
            else
            {
                ExceptionMessagePoNewPGDetail("Please enter sale estimate budget amount...!!!");
                mpePOList.Show();
                mpePostPivotGroup.Show();
                ResetPostedValues();
                return;
            }

            if (value > 0)
            {
                SuccessMessagePoList("Log pivot group updated successfully...!!!");

                GetPGReport();
                BindPOList(Convert.ToDateTime(lblStartDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd"),
                           Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM"), Convert.ToString(lblJOBNo.Text).Trim(),
                           Convert.ToString(ViewState["PivotGroupDescInList"]).Trim());
                mpePOList.Show();
                return;
            }
            else
            {
                ExceptionMessagePoList("Please try again...!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void Reset()
    {
        try
        {
            txtPONoToU.Text = string.Empty;
            txtPONoSearch.Text = string.Empty;
            txtPivotGroupSearch.Text = string.Empty;
            txtPivotGroupDescSearch.Text = string.Empty;
            txtOldPivotGroupToU.Text = string.Empty;
            txtOldPivotGroupDescToU.Text = string.Empty;
            txtNewPivotGroupToU.Text = string.Empty;
            txtNewPivotGroupDescToU.Text = string.Empty;

            txtLikelyDeliveryDateToU.Text = string.Empty;
            txtPlannedDateOfProcurementToU.Text = string.Empty;

            gvPOOldPivotGroupDetail.DataSource = null;
            gvPOOldPivotGroupDetail.DataBind();

            gvPONewPGDetail.DataSource = null;
            gvPONewPGDetail.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ResetPostedValues()
    {
        try
        {
            txtJOBNoToPost.Text = string.Empty;
            txtPostingMonthToPost.Text = string.Empty;
            txtPONoSearch.Text = string.Empty;
            txtPivotGroupToPost.Text = string.Empty;
            txtPivotGroupDescToPost.Text = string.Empty;
            txtPOBudgetToPost.Text = string.Empty;
            txtSaleEstimateBudgetToPost.Text = string.Empty;
            txtPOValueINRToPost.Text = string.Empty;
            txtDeltaAsPerPOBudgetToPost.Text = string.Empty;

            txtDeltaAsPerSaleEstimateBudgetToPost.Text = string.Empty;
            txtPendingCostToPost.Text = string.Empty;
            txtSavingCostToPost.Text = string.Empty;
            txtSavingofOriginalEstimatePercToPost.Text = string.Empty;

            txtLikelyDeliveryDateToPost.Text = string.Empty;
            txtPlannedDateOfProcurementToPost.Text = string.Empty;
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

    private void SuccessMessagePoList(string message)
    {
        pnlMsgPOList.Visible = true;
        lblMsgPOList.Text = message;
        lblMsgPOList.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessagePoList(string message)
    {
        pnlMsgPOList.Visible = true;
        lblMsgPOList.Text = message;
        lblMsgPOList.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanelPoList()
    {
        pnlMsgPOList.Visible = false;
        lblMsgPOList.Text = string.Empty;
    }

    private void ExceptionMessagePoNewPGDetail(string message)
    {
        pnlNewPGDetail.Visible = true;
        lblNewPGDetail.Text = message;
        lblNewPGDetail.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanelPoNewPGDetail()
    {
        pnlNewPGDetail.Visible = false;
        lblNewPGDetail.Text = string.Empty;
    }

    #endregion

}
