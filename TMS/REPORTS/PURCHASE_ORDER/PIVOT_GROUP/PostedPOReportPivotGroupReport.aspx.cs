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

public partial class REPORTS_PURCHASE_ORDER_PIVOT_GROUP_PostedPOReportPivotGroupReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();

    DataSet dsUnit = new DataSet();
    DataSet dsPGReport = new DataSet();
    DataTable dt1 = new DataTable();
    DataSet dsPOReport = new DataSet();
    DataSet dsPOItemReport = new DataSet();


    int unitID = 0;
    string pivotGroup = string.Empty;
    string pivotGroupDesc = string.Empty;
    string postedMonth = string.Empty;
    string JOBNo = string.Empty;
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
                Session["dtPOItemList"] = null;
                Session["dsPOReport"] = null;

                Session["PIVOT_GROUP_REPORT"] = null;
                hdGVRowCount.Value = "0";
                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);
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
        lblJOBNo.Text = txtJOBNo.Text;
        lblPostingMonth.Text = txtPostingMonth.Text;
        GetPGReport();
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

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            trAsPerSaleEstimateBudget.Visible = false;
        }
        else if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            trAsPerPOBudget.Visible = false;
        }
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
                CheckBox chkIsVPOCInList = (CheckBox)e.Row.FindControl("chkIsVPOCInList");
                Label lblIsVPOCInList = (Label)e.Row.FindControl("lblIsVPOCInList");
                TextBox txtTotalBudgetINRInList = (TextBox)e.Row.FindControl("txtTotalBudgetINRInList");
                TextBox txtSaleEstimateBudgetINRInList = (TextBox)e.Row.FindControl("txtSaleEstimateBudgetINRInList");
                TextBox txtTotalPOValueINRInList = (TextBox)e.Row.FindControl("txtTotalPOValueINRInList");
                TextBox txtTotalDeltaAsPerPOBudgetINRInList = (TextBox)e.Row.FindControl("txtTotalDeltaAsPerPOBudgetINRInList");
                TextBox txtTotalDeltaAsPerSaleEstimateBudgetINRInList = (TextBox)e.Row.FindControl("txtTotalDeltaAsPerSaleEstimateBudgetINRInList");
                Label lblStatusInList = (Label)e.Row.FindControl("lblStatusInList");

                Label lblProcApprovedByIDInList = (Label)e.Row.FindControl("lblProcApprovedByIDInList");
                Label lblPMApprovedByIDInList = (Label)e.Row.FindControl("lblPMApprovedByIDInList");
                Label lblAccApprovedByIDInList = (Label)e.Row.FindControl("lblAccApprovedByIDInList");

                ImageButton btnViewDetailInList = (ImageButton)e.Row.FindControl("btnViewDetailInList");
                btnViewDetailInList.Visible = false;

                if (Convert.ToDouble(txtTotalPOValueINRInList.Text) > 0)
                {
                    btnViewDetailInList.Visible = true;
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


                chkIsVPOCInList.Checked = false;
                if (Convert.ToInt32(lblIsVPOCInList.Text) > 0)
                {
                    chkIsVPOCInList.Checked = true;
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
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                    Label lblPivotGroupDescInList = gvPGReport.Rows[rowindex].FindControl("lblPivotGroupDescInList") as Label;
                    Label lblPivotGroupInList = gvPGReport.Rows[rowindex].FindControl("lblPivotGroupInList") as Label;
                    TextBox txtTotalPOValueINRInList = gvPGReport.Rows[rowindex].FindControl("txtTotalPOValueINRInList") as TextBox;


                    mpePOList.Show();
                    txtPivotGroupInPOList.Text = Convert.ToString(lblPivotGroupDescInList.Text);
                    txtTotalPOValueINRInPOList.Text = Convert.ToString(txtTotalPOValueINRInList.Text);
                    BindPOList(Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM"), Convert.ToString(lblJOBNo.Text).Trim(),
                               Convert.ToString(lblPivotGroupDescInList.Text).Trim());
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
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                Label lblBudget = (Label)e.Row.FindControl("lblBudget");
                Label lblDelta = (Label)e.Row.FindControl("lblDelta");


                totalPOValueINRInPOList += Convert.ToDouble(lblAmount.Text);
                txtTotalPOValueINRInPOList.Text = Convert.ToString(totalPOValueINRInPOList);

                totalBudgetINRInPOList += Convert.ToDouble(lblBudget.Text);
                txtTotalBudgetInPOList.Text = Convert.ToString(totalBudgetINRInPOList);

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
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                    Label lblLocation = gvPOList.Rows[rowindex].FindControl("lblLocation") as Label;
                    Label lblPONo = gvPOList.Rows[rowindex].FindControl("lblPONo") as Label;
                    Label lblDocClass = gvPOList.Rows[rowindex].FindControl("lblDocClass") as Label;

                    mpePOList.Show();
                    mpePOItemList.Show();
                    BindPOItemList(Convert.ToString(lblLocation.Text), Convert.ToString(lblPONo.Text), Convert.ToString(lblDocClass.Text));
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPGReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["PIVOT_GROUP_REPORT"];
            ExportToExcel(ds.Tables[0]);
        }
    }

    protected void btnExportPOList_Click(object sender, EventArgs e)
    {
        if (gvPOList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsPOReport"];
            ExportToExcelPOList(ds.Tables[0]);
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

    #endregion


    #region METHODS[=========================]

    private void GetPGReport()
    {
        try
        {


            pivotGroup = string.Empty;
            pivotGroupDesc = string.Empty;
            postedMonth = string.Empty;
            JOBNo = string.Empty;

            //if (chkSelectDates.Checked)
            //{
            //    if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
            //        fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

            //    if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
            //        toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            //}

            if (!string.IsNullOrEmpty(txtPivotGroup.Text))
                pivotGroup = txtPivotGroup.Text;

            if (!string.IsNullOrEmpty(txtPivotGroupDesc.Text))
                pivotGroupDesc = txtPivotGroupDesc.Text;

            if (!string.IsNullOrEmpty(txtPostingMonth.Text))
                postedMonth = Convert.ToDateTime(txtPostingMonth.Text).ToString("yyyy-MM");

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text;

            dsPGReport = objReports.GetPostedPOPivotGroupReport(pivotGroup, pivotGroupDesc, postedMonth, JOBNo);

            if (dsPGReport.Tables.Count > 0 && dsPGReport.Tables[0].Rows.Count > 0)
            {
                Session["PIVOT_GROUP_REPORT"] = dsPGReport;
                gvPGReport.DataSource = dsPGReport.Tables[0];
                gvPGReport.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvPGReport.Rows.Count);
            }
            else
            {
                Session["PIVOT_GROUP_REPORT"] = null;
                gvPGReport.DataSource = null;
                gvPGReport.DataBind();
                txtTotalPOBudgetINR.Text = string.Empty;
                txtTotalDeltaAsPerPOBudgetINR.Text = string.Empty;

                txtTotalPOValueINR1.Text = string.Empty;
                txtTotalPOValueINR2.Text = string.Empty;

                txtTotalSaleEstimateBudgetINR.Text = string.Empty;
                txtTotalDeltaAsPerSaleEstimateBudgetINR.Text = string.Empty;

                hdGVRowCount.Value = "0";
            }
            lblRecords.Text = "Records[" + dsPGReport.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPOList(string postedMonth, string JOBNo, string pivotGroup)
    {
        try
        {
            dsPOReport = objReports.GetPOReportPivotGroupWise("", "", postedMonth, JOBNo, pivotGroup);

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
            lblPOdetailRecords.Text = "Records[" + dsPOReport.Tables[0].Rows.Count + "]";
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
            lblRecords.Text = "Records[" + gvPOItemList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            throw ex;
            return;
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


            string fileName = "PO_List_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    #endregion

}