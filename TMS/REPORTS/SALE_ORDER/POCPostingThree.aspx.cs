using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class REPORTS_SALE_ORDER_POCPostingThree : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsTempTableDetails = new DataSet();
    DataSet dsSaleOrder = new DataSet();
    DataSet dsJobNoInvoiceList = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string jobNo = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    int unitId = 0;
    string unitName = string.Empty;
    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);


                Session["POC_BILLING"] = null;
                Session["DB_DETAILS"] = objCommon.GetDBDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["POC_BILLING"] = null;
        gvSaleOrderPosting.DataSource = null;
        gvSaleOrderPosting.DataBind();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["POC_BILLING"] = null;
        gvSaleOrderPosting.DataSource = null;
        gvSaleOrderPosting.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        GetSalesorderOSReportOne();
    }

    protected void gvSaleOrderPosting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvSaleOrderPosting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                pnlMsg.Visible = false;
                int rowindex = 0;

                bool chk = false;
                int serialNo = 0;
                string jobNo = string.Empty;
                string serialNos = string.Empty;
                string month = string.Empty;
                string orderDate = string.Empty;
                string customerCode = string.Empty;
                string customerName = string.Empty;
                string bu = string.Empty;
                double orderAmount = 0;
                double pocBilling = 0;
                double taxBilling = 0;
                double netPOCBacklog = 0;
                int count = 0;
                int tagCount = 0;

                string[] strFinalValQuery;
                string finalValQuery = string.Empty;

                if (e.CommandArgument == "ADD_TAX_BILL" || e.CommandArgument == "POST")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblJobNo = gvSaleOrderPosting.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblSerialNo = gvSaleOrderPosting.Rows[rowindex].FindControl("lblSerialNo") as Label;

                Label lblOrderDate = gvSaleOrderPosting.Rows[rowindex].FindControl("lblOrderDate") as Label;
                Label lblCustomerCode = gvSaleOrderPosting.Rows[rowindex].FindControl("lblCustomerCode") as Label;
                Label lblCustomerName = gvSaleOrderPosting.Rows[rowindex].FindControl("lblCustomerName") as Label;

                Label lblBU = gvSaleOrderPosting.Rows[rowindex].FindControl("lblBU") as Label;
                Label lblOrderAmount = gvSaleOrderPosting.Rows[rowindex].FindControl("lblOrderAmount") as Label;
                Label lblPreviousBilling = gvSaleOrderPosting.Rows[rowindex].FindControl("lblPreviousBilling") as Label;
                TextBox txtPOCBilling = gvSaleOrderPosting.Rows[rowindex].FindControl("txtPOCBilling") as TextBox;
                TextBox txtTAXBilling = gvSaleOrderPosting.Rows[rowindex].FindControl("txtTAXBilling") as TextBox;
                TextBox txtNetPOCBacklog = gvSaleOrderPosting.Rows[rowindex].FindControl("txtNetPOCBacklog") as TextBox;

                if (e.CommandArgument == "ADD_TAX_BILL")
                {
                    txtMonth.Text = Convert.ToDateTime(hdPostingMonth.Value).ToString("MMM-yyyy");
                    txtJOBNoNew.Text = lblJobNo.Text;
                    this.ModalPopupExtender1.Show();
                    lblRowIndex.Text = Convert.ToString(rowindex);
                    lblSelectedRecords.Text = "[0]";
                    txtTotalAmount.Text = "0";
                    chkSelectAll.Checked = false;
                    GetJobNoInvoiceList();
                }

                if (e.CommandArgument == "POST")
                {
                    serialNo = Convert.ToInt32(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                        jobNo = Convert.ToString(lblJobNo.Text);
                    else
                        jobNo = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblOrderDate.Text)))
                        orderDate = Convert.ToDateTime(lblOrderDate.Text).ToString("yyyy-MM-dd");
                    else
                        orderDate = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerCode.Text)))
                        customerCode = Convert.ToString(lblCustomerCode.Text);
                    else
                        customerCode = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerName.Text)))
                        customerName = Convert.ToString(lblCustomerName.Text);
                    else
                        customerName = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblBU.Text)))
                        bu = Convert.ToString(lblBU.Text);
                    else
                        bu = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblOrderAmount.Text)))
                        orderAmount = Convert.ToDouble(lblOrderAmount.Text);
                    else
                        orderAmount = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtPOCBilling.Text)))
                        pocBilling = Convert.ToDouble(txtPOCBilling.Text);
                    else
                        pocBilling = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtTAXBilling.Text)))
                        taxBilling = Convert.ToDouble(txtTAXBilling.Text);
                    else
                        taxBilling = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtNetPOCBacklog.Text)))
                        netPOCBacklog = Convert.ToDouble(txtNetPOCBacklog.Text);
                    else
                        netPOCBacklog = 0;

                    if (chk)
                    {
                        int value = objReports.PostPOCBillingOne(jobNo, orderDate, customerCode, customerName,
                                                        bu, orderAmount, pocBilling, taxBilling, netPOCBacklog,
                                                        month, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        if (value > 0)
                        {
                            count++;
                            serialNos += serialNo + ",";

                            if (ViewState["VAL_QUERY"] != null)
                            {
                                strFinalValQuery = Convert.ToString(ViewState["VAL_QUERY"]).TrimEnd('#').Split('#');

                                foreach (string item in strFinalValQuery)
                                {
                                    if (taxBilling > 0)
                                    {
                                        if (item.Contains(jobNo.Trim()))
                                            finalValQuery += item + ",";
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(finalValQuery))
                            {
                                finalValQuery = finalValQuery.TrimEnd(',').Replace("'", "$");
                                int tagValue = objReports.TagInvoiceToInvoice(finalValQuery);
                                {
                                    tagCount++;
                                }
                                finalValQuery = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HidePanel();
        PostPOCBilling();
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
        double invoiceAmount = 0;
        int selectedRecords = 0;
        foreach (GridViewRow gr in gvInvoiceList.Rows)
        {
            CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
            Label lblInvoiceAmount = (Label)gr.FindControl("lblInvoiceAmount");

            if (chkSelect.Checked)
            {
                invoiceAmount += Convert.ToDouble(lblInvoiceAmount.Text);
                selectedRecords++;
            }
        }
        txtTotalAmount.Text = Convert.ToString(invoiceAmount);
        lblSelectedRecords.Text = "[" + Convert.ToString(selectedRecords) + "]";

        //if (Convert.ToDouble(invoiceAmount) > 0)
        //    btnAddTaxBill.Visible = true;
        //else
        //    btnAddTaxBill.Visible = false;
    }

    protected void btnAddTaxBill_Click(object sender, EventArgs e)
    {
        TagInvoiceToJobNoNew();
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this.ModalPopupExtender1.Show();
            double invoiceAmount = 0;
            int selectedRecords = 0;

            if (gvInvoiceList.Rows.Count > 0)
            {
                if (chkSelectAll.Checked)
                {
                    foreach (GridViewRow gr in gvInvoiceList.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        Label lblInvoiceAmount = (Label)gr.FindControl("lblInvoiceAmount");
                        chkSelect.Checked = true;
                        invoiceAmount += Convert.ToDouble(lblInvoiceAmount.Text);
                        selectedRecords++;
                    }
                }
                else
                {
                    foreach (GridViewRow gr in gvInvoiceList.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        Label lblInvoiceAmount = (Label)gr.FindControl("lblInvoiceAmount");
                        chkSelect.Checked = false;
                        invoiceAmount = 0;
                        selectedRecords = 0;
                    }
                }
            }

            txtTotalAmount.Text = Convert.ToString(invoiceAmount);
            lblSelectedRecords.Text = "[" + Convert.ToString(selectedRecords) + "]";

            //if (Convert.ToDouble(invoiceAmount) > 0)
            //    btnAddTaxBill.Visible = true;
            //else
            //    btnAddTaxBill.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    #endregion


    #region METHODS[=========================]

    private void GetSalesorderOSReportOne()
    {
        try
        {
            string month1 = string.Empty;
            month1 = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");
            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text.ToUpper();
            else
                jobNo = string.Empty;

            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }
            dsSaleOrder = objReports.GetSOBacklogPOCPostingListAllUnitOne(month1, jobNo, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);

            if (dsSaleOrder.Tables.Count > 0)
            {
                if (dsSaleOrder.Tables[0].Rows.Count > 0)
                {
                    if (dsSaleOrder.Tables[1].Rows.Count > 0)
                    {
                        Session["POC_BILLING"] = dsSaleOrder.Tables[1];
                        gvSaleOrderPosting.DataSource = dsSaleOrder.Tables[1];
                        gvSaleOrderPosting.DataBind();
                    }
                    else
                    {
                        Session["POC_BILLING"] = null;
                        gvSaleOrderPosting.DataSource = null;
                        gvSaleOrderPosting.DataBind();
                        ExceptionMessage("No data found...!");
                        return;
                    }
                }
                else
                {
                    Session["POC_BILLING"] = null;
                    gvSaleOrderPosting.DataSource = null;
                    gvSaleOrderPosting.DataBind();

                    ExceptionMessage("Please enter current FX rate of selected month..!");
                    return;
                }
            }
            else
            {
                Session["POC_BILLING"] = null;
                gvSaleOrderPosting.DataSource = null;
                gvSaleOrderPosting.DataBind();
            }

            lblRecords.Text = "Records[" + gvSaleOrderPosting.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void PostPOCBilling()
    {
        try
        {
            bool chk = false;
            int serialNo = 0;
            string serialNos = string.Empty;
            string month = string.Empty;
            string jobNo = string.Empty;
            string orderDate = string.Empty;
            string customerCode = string.Empty;
            string customerName = string.Empty;
            string bu = string.Empty;
            double orderAmount = 0;
            double pocBilling = 0;
            double taxBilling = 0;
            double netPOCBacklog = 0;
            int count = 0;
            int tagCount = 0;

            string[] strFinalValQuery;
            string finalValQuery = string.Empty;



            month = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");

            if (gvSaleOrderPosting.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSaleOrderPosting.Rows)
                {
                    chk = true;

                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblJobNo = (Label)gr.FindControl("lblJobNo");

                    Label lblOrderDate = (Label)gr.FindControl("lblOrderDate");
                    Label lblCustomerCode = (Label)gr.FindControl("lblCustomerCode");
                    Label lblCustomerName = (Label)gr.FindControl("lblCustomerName");

                    Label lblBU = (Label)gr.FindControl("lblBU");
                    Label lblOrderAmount = (Label)gr.FindControl("lblOrderAmount");
                    Label lblPreviousBilling = (Label)gr.FindControl("lblPreviousBilling");
                    TextBox txtPOCBilling = (TextBox)gr.FindControl("txtPOCBilling");
                    TextBox txtTAXBilling = (TextBox)gr.FindControl("txtTAXBilling");
                    TextBox txtNetPOCBacklog = (TextBox)gr.FindControl("txtNetPOCBacklog");

                    serialNo = Convert.ToInt32(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                        jobNo = Convert.ToString(lblJobNo.Text);
                    else
                        jobNo = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblOrderDate.Text)))
                        orderDate = Convert.ToDateTime(lblOrderDate.Text).ToString("yyyy-MM-dd");
                    else
                        orderDate = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerCode.Text)))
                        customerCode = Convert.ToString(lblCustomerCode.Text);
                    else
                        customerCode = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerName.Text)))
                        customerName = Convert.ToString(lblCustomerName.Text);
                    else
                        customerName = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblBU.Text)))
                        bu = Convert.ToString(lblBU.Text);
                    else
                        bu = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblOrderAmount.Text)))
                        orderAmount = Convert.ToDouble(lblOrderAmount.Text);
                    else
                        orderAmount = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtPOCBilling.Text)))
                        pocBilling = Convert.ToDouble(txtPOCBilling.Text);
                    else
                        pocBilling = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtTAXBilling.Text)))
                        taxBilling = Convert.ToDouble(txtTAXBilling.Text);
                    else
                        taxBilling = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtNetPOCBacklog.Text)))
                        netPOCBacklog = Convert.ToDouble(txtNetPOCBacklog.Text);
                    else
                        netPOCBacklog = 0;


                    if (chk)
                    {
                        int value = objReports.PostPOCBillingOne(jobNo, orderDate, customerCode, customerName,
                                                        bu, orderAmount, pocBilling, taxBilling, netPOCBacklog,
                                                        month, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        if (value > 0)
                        {
                            count++;
                            serialNos += serialNo + ",";

                            if (ViewState["VAL_QUERY"] != null)
                            {
                                strFinalValQuery = Convert.ToString(ViewState["VAL_QUERY"]).TrimEnd('#').Split('#');

                                foreach (string item in strFinalValQuery)
                                {
                                    if (taxBilling > 0)
                                    {
                                        if (item.Contains(jobNo.Trim()))
                                            finalValQuery += item + ",";
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(finalValQuery))
                            {
                                finalValQuery = finalValQuery.TrimEnd(',').Replace("'", "$");
                                int tagValue = objReports.TagInvoiceToInvoice(finalValQuery);
                                {
                                    tagCount++;
                                }
                                finalValQuery = string.Empty;
                            }
                        }
                    }
                }

                if (count > 0)
                {
                    serialNos = serialNos.TrimEnd(',');
                    if (!string.IsNullOrEmpty(serialNos))
                        RemoveRecords((DataTable)Session["POC_BILLING"], serialNos);

                    if (tagCount > 0)
                        SuccessMessage(count + " Records posted successfully and invoice tagged successfully..!");
                    else
                        SuccessMessage(count + " Records posted successfully..!");
                }
            }
            else
            {
                ExceptionMessage("No data found..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRecords(DataTable dtTextChanged, string serialNos)
    {
        string[] strSerialNo = serialNos.Split(',');
        if (dtTextChanged.Rows.Count > 0)
        {
            foreach (string sr in strSerialNo)
            {
                if (!string.IsNullOrEmpty(sr))
                {
                    foreach (DataRow drremove in dtTextChanged.Select("SERIAL_NO='" + sr + "'"))
                    {
                        dtTextChanged.Rows.Remove(drremove);
                    }
                }
            }

            if (dtTextChanged.Rows.Count > 0)
            {
                Session["POC_BILLING"] = dtTextChanged;
                gvSaleOrderPosting.DataSource = dtTextChanged;
                gvSaleOrderPosting.DataBind();
            }
            else
            {
                Session["POC_BILLING"] = null;
                gvSaleOrderPosting.DataSource = null;
                gvSaleOrderPosting.DataBind();
            }
        }
        else
        {
            Session["POC_BILLING"] = null;
        }
        lblRecords.Text = "Records[" + gvSaleOrderPosting.Rows.Count + "]";
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

    private void EmptyGridview()
    {
        gvSaleOrderPosting.DataSource = null;
        gvSaleOrderPosting.DataBind();
        lblRecords.Text = "Records[" + gvSaleOrderPosting.Rows.Count + "]";
    }

    private void GetJobNoInvoiceList()
    {
        try
        {
            //btnAddTaxBill.Visible = false;
            string month = Convert.ToDateTime(txtMonth.Text).ToString("yyyy-MM");
            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (!string.IsNullOrEmpty(txtJOBNoNew.Text))
                jobNo = txtJOBNoNew.Text.Trim().ToUpper();
            else
                jobNo = string.Empty;

            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }

            dsJobNoInvoiceList = objReports.GetJobNoInvoiceList(month, jobNo, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);

            if (dsJobNoInvoiceList.Tables.Count > 0 && dsJobNoInvoiceList.Tables[0].Rows.Count > 0)
            {
                gvInvoiceList.DataSource = dsJobNoInvoiceList.Tables[0];
                gvInvoiceList.DataBind();
            }
            else
            {
                gvInvoiceList.DataSource = null;
                gvInvoiceList.DataBind();
            }

            lblInvoiceRecoreds.Text = "Records[" + gvInvoiceList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void TagInvoiceToJobNoNew()
    {
        try
        {
            string jobNo = string.Empty;
            string invoiceNo = string.Empty;
            string invoiceDate = string.Empty;
            string invoiceAmt = string.Empty;
            string valQuery = string.Empty;
            string[] strValQuery;
            List<string> lstFinalValQueryOne;
            List<string> lstFinalValQueryTwo;

            //Label lblOrderAmount = (Label)gvSaleOrderPosting.Rows[Convert.ToInt32(lblRowIndex.Text)].FindControl("lblOrderAmount");
            //Label lblPreviousBilling = (Label)gvSaleOrderPosting.Rows[Convert.ToInt32(lblRowIndex.Text)].FindControl("lblPreviousBilling");
            //TextBox txtPOCBilling = (TextBox)gvSaleOrderPosting.Rows[Convert.ToInt32(lblRowIndex.Text)].FindControl("txtPOCBilling");
            TextBox txtTAXBilling = (TextBox)gvSaleOrderPosting.Rows[Convert.ToInt32(lblRowIndex.Text)].FindControl("txtTAXBilling");
            //TextBox txtNetPOCBacklog = (TextBox)gvSaleOrderPosting.Rows[Convert.ToInt32(lblRowIndex.Text)].FindControl("txtNetPOCBacklog");

            if (Convert.ToDouble(txtTotalAmount.Text) > 0)
            {
                txtTAXBilling.Text = txtTotalAmount.Text;

                if (Convert.ToDouble(txtTAXBilling.Text) > 0)
                {
                    foreach (GridViewRow gr in gvInvoiceList.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                        Label lblInvoiceNo = (Label)gr.FindControl("lblInvoiceNo");
                        Label lblInvoiceDate = (Label)gr.FindControl("lblInvoiceDate");
                        Label lblInvoiceAmount = (Label)gr.FindControl("lblInvoiceAmount");

                        if (chkSelect.Checked)
                        {
                            valQuery += "('" + lblJobNo.Text + "','" + lblInvoiceNo.Text + "','" + lblInvoiceDate.Text + "','" + lblInvoiceAmount.Text + "'," + Convert.ToString(Session["EMP_RECORD_ID"]) + ",GETDATE())#";
                        }
                    }

                    if (Convert.ToString(ViewState["VAL_QUERY"]).Contains(txtJOBNoNew.Text.Trim()))
                    {
                        strValQuery = Convert.ToString(ViewState["VAL_QUERY"]).TrimEnd('#').Split('#');
                        strValQuery = strValQuery.Distinct().ToArray();
                        lstFinalValQueryOne = new List<string>(strValQuery);
                        lstFinalValQueryTwo = new List<string>();

                        foreach (string item in lstFinalValQueryOne)
                        {
                            if (item.Contains(txtJOBNoNew.Text.Trim()))
                            {
                                lstFinalValQueryTwo.Add(item);
                            }
                        }

                        foreach (string item in lstFinalValQueryTwo)
                        {
                            lstFinalValQueryOne.Remove(item);
                        }

                        strValQuery = valQuery.TrimEnd('#').Split('#');
                        foreach (string item in strValQuery)
                        {
                            lstFinalValQueryOne.Add(item);
                        }

                        ViewState["VAL_QUERY"] = null;
                        foreach (string dd in lstFinalValQueryOne)
                        {
                            ViewState["VAL_QUERY"] += dd + "#";
                        }
                    }
                    else
                    {
                        ViewState["VAL_QUERY"] += valQuery;
                    }
                }
                else
                {
                    if (Convert.ToString(ViewState["VAL_QUERY"]).Contains(txtJOBNoNew.Text.Trim()))
                    {
                        strValQuery = Convert.ToString(ViewState["VAL_QUERY"]).TrimEnd('#').Split('#');
                        strValQuery = strValQuery.Distinct().ToArray();
                        lstFinalValQueryOne = new List<string>(strValQuery);
                        lstFinalValQueryTwo = new List<string>();

                        foreach (string item in lstFinalValQueryOne)
                        {
                            if (item.Contains(txtJOBNoNew.Text.Trim()))
                            {
                                lstFinalValQueryTwo.Add(item);
                            }
                        }

                        foreach (string item in lstFinalValQueryTwo)
                        {
                            lstFinalValQueryOne.Remove(item);
                        }

                        ViewState["VAL_QUERY"] = null;
                        foreach (string dd in lstFinalValQueryOne)
                        {
                            ViewState["VAL_QUERY"] += dd + "#";
                        }
                    }
                }
            }
            else
            {
                txtTAXBilling.Text = txtTotalAmount.Text;
                if (Convert.ToString(ViewState["VAL_QUERY"]).Contains(txtJOBNoNew.Text.Trim()))
                {
                    strValQuery = Convert.ToString(ViewState["VAL_QUERY"]).TrimEnd('#').Split('#');
                    strValQuery = strValQuery.Distinct().ToArray();
                    lstFinalValQueryOne = new List<string>(strValQuery);
                    lstFinalValQueryTwo = new List<string>();

                    foreach (string item in lstFinalValQueryOne)
                    {
                        if (item.Contains(txtJOBNoNew.Text.Trim()))
                        {
                            lstFinalValQueryTwo.Add(item);
                        }
                    }

                    foreach (string item in lstFinalValQueryTwo)
                    {
                        lstFinalValQueryOne.Remove(item);
                    }

                    ViewState["VAL_QUERY"] = null;
                    foreach (string dd in lstFinalValQueryOne)
                    {
                        ViewState["VAL_QUERY"] += dd + "#";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    #endregion

}