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

public partial class USGAAP_BILLING_PostedBillsInDetailsOne : System.Web.UI.Page
{

    #region VARIABLES[=======================]


    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Posting objPosting = new BAL.Posting();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsPostedList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsCountry = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsEndMarket = new DataSet();
    DataSet dsRevenueType = new DataSet();
    DataSet dsType = new DataSet();
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string billNo = string.Empty;
    string unitName = string.Empty;

    string customerName = string.Empty;
    string revenueAccount = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {

                Session["POSTED_LIST_IN_DETAIL"] = null;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //hdStartDateSearch.Value = Convert.ToDateTime("2017-01-01").ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //hdEndDateSearch.Value = Convert.ToDateTime("2019-03-31").ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;
                BindUnit();
                BindPostingCurrency();
                BindType();

                ddlRevenueType.Items.Clear();
                ddlRevenueType.Items.Insert(0, "Select");
                ddlRevenueType.SelectedIndex = 0;

                GetPostedList();
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
        GetPostedList();
    }

    protected void gvPostedList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
                Label lblCustomerName = (Label)e.Row.FindControl("lblCustomerName");
                Label lblJobNo = (Label)e.Row.FindControl("lblJobNo");
                Label lblIsNewInserted = (Label)e.Row.FindControl("lblIsNewInserted");

                if (Convert.ToInt32(lblIsNewInserted.Text) > 0)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Pink;
                }

                e.Row.ToolTip = "Invoice No.:" + lblInvoiceNo.Text + ", Customer Name:" + lblCustomerName.Text + ", JOB No.:" + lblJobNo.Text;

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

    protected void gvPostedList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int recordID = 0;
        string invoiceNo = string.Empty;
        double invoiceAmount = 0;
        int endMarketID = 0;
        int countryID = 0;
        string postingMonth = string.Empty;
        int postingYear = 0;
        double postingValue = 0;
        int postingCurrencyID = 0;
        double unpostedValue = 0;
        double postedValue = 0;
        string RevRecReduction = string.Empty;
        string udf2 = string.Empty;
        string udf3 = string.Empty;
        string udf4 = string.Empty;
        string udf5 = string.Empty;


        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES" || e.CommandArgument == "DELETE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = (Label)gvPostedList.Rows[rowindex].FindControl("lblRecordID");
                Label lblInvoiceNo = (Label)gvPostedList.Rows[rowindex].FindControl("lblInvoiceNo");
                Label lblInvoiceDate = (Label)gvPostedList.Rows[rowindex].FindControl("lblInvoiceDate");
                Label lblCustomerCode = (Label)gvPostedList.Rows[rowindex].FindControl("lblCustomerCode");
                Label lblCustomerName = (Label)gvPostedList.Rows[rowindex].FindControl("lblCustomerName");
                Label lblJobNo = (Label)gvPostedList.Rows[rowindex].FindControl("lblJobNo");
                Label lblBusinessUnit = (Label)gvPostedList.Rows[rowindex].FindControl("lblBusinessUnit");
                Label lblProductCode = (Label)gvPostedList.Rows[rowindex].FindControl("lblProductCode");
                Label lblRevenueAccount = (Label)gvPostedList.Rows[rowindex].FindControl("lblRevenueAccount");
                Label lblRevenueAccountDesc = (Label)gvPostedList.Rows[rowindex].FindControl("lblRevenueAccountDesc");
                Label lblRevenueAccountType = (Label)gvPostedList.Rows[rowindex].FindControl("lblRevenueAccountType");
                Label lblQuantity = (Label)gvPostedList.Rows[rowindex].FindControl("lblQuantity");
                Label lblProductRate = (Label)gvPostedList.Rows[rowindex].FindControl("lblProductRate");

                Label lblPendingAmount = (Label)gvPostedList.Rows[rowindex].FindControl("lblPendingAmount");
                Label lblInvoiceAmount = (Label)gvPostedList.Rows[rowindex].FindControl("lblInvoiceAmount");

                Label lblLocation = (Label)gvPostedList.Rows[rowindex].FindControl("lblLocation");
                Label lblPostedValue = (Label)gvPostedList.Rows[rowindex].FindControl("lblPostedValue");
                Label lblPostedDate = (Label)gvPostedList.Rows[rowindex].FindControl("lblPostedDate");
                Label lblUnpostedValue = (Label)gvPostedList.Rows[rowindex].FindControl("lblUnpostedValue");
                Label lblEndMarket = (Label)gvPostedList.Rows[rowindex].FindControl("lblEndMarket");
                Label lblGeogrophy = (Label)gvPostedList.Rows[rowindex].FindControl("lblGeogrophy");
                Label lblPostingMonth = (Label)gvPostedList.Rows[rowindex].FindControl("lblPostingMonth");
                Label lblPostingYear = (Label)gvPostedList.Rows[rowindex].FindControl("lblPostingYear");
                Label lblPostingCurrencyID = (Label)gvPostedList.Rows[rowindex].FindControl("lblPostingCurrencyID");
                Label lblPostingCurrency = (Label)gvPostedList.Rows[rowindex].FindControl("lblPostingCurrency");
                Label lblUnPostedValue = (Label)gvPostedList.Rows[rowindex].FindControl("lblUnPostedValue");
                Label lblActualUnpostedValue = (Label)gvPostedList.Rows[rowindex].FindControl("lblActualUnpostedValue");
                Label lblTypeID = (Label)gvPostedList.Rows[rowindex].FindControl("lblTypeID");
                Label lblRevenueTypeID = (Label)gvPostedList.Rows[rowindex].FindControl("lblRevenueTypeID");
                Label lblIsNewInserted = (Label)gvPostedList.Rows[rowindex].FindControl("lblIsNewInserted");
                Label lblRevRecReduction = (Label)gvPostedList.Rows[rowindex].FindControl("lblRevRecReduction");
                Label lblUDF2 = (Label)gvPostedList.Rows[rowindex].FindControl("lblUDF2");
                Label lblUDF3 = (Label)gvPostedList.Rows[rowindex].FindControl("lblUDF3");
                Label lblUDF4 = (Label)gvPostedList.Rows[rowindex].FindControl("lblUDF4");
                Label lblUDF5 = (Label)gvPostedList.Rows[rowindex].FindControl("lblUDF5");


                ViewState["recordID"] = Convert.ToInt32(lblRecordID.Text);
                ViewState["invoiceNo"] = Convert.ToString(lblInvoiceNo.Text);

                if (e.CommandArgument == "PROPERTIES")
                {
                    lblLegend.Text = "Invoice No.[" + Convert.ToString(lblInvoiceNo.Text) + "]-[" + Convert.ToString(lblLocation.Text) + "]";
                    txtInvoiceDate.Text = Convert.ToString(lblInvoiceDate.Text);
                    txtJobNo.Text = Convert.ToString(lblJobNo.Text);
                    txtCustName.Text = Convert.ToString(lblCustomerName.Text);
                    txtCustCode.Text = Convert.ToString(lblCustomerCode.Text);
                    txtBusinessUnit.Text = Convert.ToString(lblBusinessUnit.Text);
                    txtProductCode.Text = Convert.ToString(lblProductCode.Text);
                    txtRevAccount.Text = Convert.ToString(lblRevenueAccount.Text);
                    txtAccountDesc.Text = Convert.ToString(lblRevenueAccountDesc.Text);
                    txtAccountType.Text = Convert.ToString(lblRevenueAccountType.Text);
                    txtQuantity.Text = Convert.ToString(lblQuantity.Text);
                    txtProductRate.Text = Convert.ToString(lblProductRate.Text);
                    hdInvoiceAmount.Value = Convert.ToString(lblInvoiceAmount.Text);
                    txtInvoiceAmount.Text = Convert.ToString(hdInvoiceAmount.Value);
                    hdPendingAmount.Value = Convert.ToString(lblPendingAmount.Text);
                    txtPostedValue.Text = Convert.ToString(lblPostedValue.Text);
                    hdPostedValue.Value = Convert.ToString(lblPostedValue.Text);
                    ddlPostingCurrency.SelectedValue = Convert.ToString(lblPostingCurrencyID.Text);

                    //hdUnpostedValue.Value = Convert.ToString(txtUnpostedValue.Text);

                    //txtUnpostedValue.Text = Convert.ToString(Convert.ToDouble(lblUnpostedValue.Text) + Convert.ToDouble(lblActualUnpostedValue.Text));
                    hdUnpostedValue.Value = Convert.ToString((Convert.ToDouble(lblPendingAmount.Text) - Convert.ToDouble(txtPostedValue.Text)));//+ Convert.ToDouble(lblActualUnpostedValue.Text));
                    txtUnpostedValue.Text = Convert.ToString(hdUnpostedValue.Value);

                    //hdUnpostedValue.Value = Convert.ToString(lblUnpostedValue.Text);

                    hdActualUnpostedValue.Value = Convert.ToString(lblActualUnpostedValue.Text);

                    BindEndMarket();
                    if (!string.IsNullOrEmpty(Convert.ToString(lblEndMarket.Text)))
                        ddlEndMarket.SelectedValue = Convert.ToString(lblEndMarket.Text);
                    else
                        ddlEndMarket.SelectedIndex = 0;

                    BindCountry();
                    if (!string.IsNullOrEmpty(Convert.ToString(lblGeogrophy.Text)))
                        ddlCountry.SelectedValue = Convert.ToString(lblGeogrophy.Text);
                    else
                        ddlCountry.SelectedIndex = 0;

                    ddlPostingMonth.SelectedValue = Convert.ToString(lblPostingMonth.Text);
                    ddlPostingYear.SelectedValue = Convert.ToString(lblPostingYear.Text);

                    ddlType.SelectedValue = Convert.ToString(lblTypeID.Text);
                    BindRevenueType(Convert.ToInt32(lblTypeID.Text));
                    ddlRevenueType.SelectedValue = Convert.ToString(lblRevenueTypeID.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblIsNewInserted.Text)))
                        hdIsNewInserted.Value = Convert.ToString(lblIsNewInserted.Text);
                    else
                        hdIsNewInserted.Value = "0";

                    txtRevRecReduction.Text = Convert.ToString(lblRevRecReduction.Text);
                    txtUDF2.Text = Convert.ToString(lblUDF2.Text);
                    txtUDF3.Text = Convert.ToString(lblUDF3.Text);
                    txtUDF4.Text = Convert.ToString(lblUDF4.Text);
                    txtUDF5.Text = Convert.ToString(lblUDF5.Text);

                    if (Convert.ToInt32(lblIsNewInserted.Text) > 0)
                        txtInvoiceAmount.Enabled = true;
                    else
                        txtInvoiceAmount.Enabled = false;

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

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();

        BindUnpostingValue();

        if (ddlType.SelectedIndex > 0)
        {
            BindRevenueType(Convert.ToInt32(ddlType.SelectedValue));
        }
        else
        {
            ddlRevenueType.Items.Clear();
            ddlRevenueType.Items.Insert(0, "Select");
            ddlRevenueType.SelectedIndex = 0;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;

        UpdatePostedBill(Convert.ToInt32(ViewState["recordID"]), Convert.ToString(ViewState["invoiceNo"]));

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPostedList.Rows.Count > 0)
        {
            ExportGridviewToExcel();
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
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
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

    private void BindPostingCurrency()
    {
        try
        {
            dsCurrency = objTourAndTravels.GetPrimaryDetails("sp_get_currency_list");
            if (dsCurrency.Tables.Count > 0 && dsCurrency.Tables[0].Rows.Count > 0)
            {
                ddlPostingCurrency.DataSource = dsCurrency.Tables[0];
                ddlPostingCurrency.DataTextField = "CURRENCY_CODE";
                ddlPostingCurrency.DataValueField = "CURRENCY_ID";
                ddlPostingCurrency.DataBind();
                ddlPostingCurrency.Items.Insert(0, "Select");
                ddlPostingCurrency.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEndMarket()
    {
        try
        {
            dsEndMarket = objPosting.GetEndMarketForUnpostedGL();
            if (dsEndMarket.Tables.Count > 0 && dsEndMarket.Tables[0].Rows.Count > 0)
            {
                ddlEndMarket.DataSource = dsEndMarket.Tables[0];
                ddlEndMarket.DataTextField = "END_MARKET";
                ddlEndMarket.DataValueField = "END_MARKET_CODE";
                ddlEndMarket.DataBind();
                ddlEndMarket.Items.Insert(0, "Select");
                ddlEndMarket.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCountry()
    {
        try
        {
            dsCountry = objPosting.GetCountryForUnpostedGL();
            if (dsCountry.Tables.Count > 0 && dsCountry.Tables[0].Rows.Count > 0)
            {
                ddlCountry.DataSource = dsCountry.Tables[0];
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataValueField = "COUNTRY_ISO_CODE";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "Select");
                ddlCountry.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindType()
    {
        try
        {
            dsType = objTourAndTravels.GetPrimaryDetails("sp_get_type");
            if (dsType.Tables.Count > 0 && dsType.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = dsType.Tables[0];
                ddlType.DataTextField = "TYPE";
                ddlType.DataValueField = "TYPE_ID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetPostedList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (!string.IsNullOrEmpty(txtBillNo.Text))
                billNo = txtBillNo.Text;
            else
                billNo = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;


            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtRevenueAccount.Text))
                revenueAccount = txtRevenueAccount.Text;
            else
                revenueAccount = string.Empty;


            dsPostedList = objPosting.GetPostedListInDetails(fromDate, toDate, billNo, unitName, customerName, revenueAccount);

            if (dsPostedList.Tables.Count > 0 && dsPostedList.Tables[0].Rows.Count > 0)
            {
                Session["POSTED_LIST_IN_DETAIL"] = dsPostedList;
                gvPostedList.DataSource = dsPostedList.Tables[0];
                gvPostedList.DataBind();
            }
            else
            {
                Session["POSTED_LIST_IN_DETAIL"] = null;
                gvPostedList.DataSource = null;
                gvPostedList.DataBind();
            }
            lblRecords.Text = "Records[" + dsPostedList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindRevenueType(int typeID)
    {
        try
        {
            dsRevenueType = objPosting.GetRevenueType(typeID);
            if (dsRevenueType.Tables.Count > 0 && dsRevenueType.Tables[0].Rows.Count > 0)
            {
                ddlRevenueType.DataSource = dsRevenueType.Tables[0];
                ddlRevenueType.DataTextField = "REVENUE_TYPE";
                ddlRevenueType.DataValueField = "REVENUE_TYPE_ID";
                ddlRevenueType.DataBind();
                ddlRevenueType.Items.Insert(0, "Select");
            }
            else
            {
                ddlRevenueType.Items.Clear();
                ddlRevenueType.Items.Insert(0, "Select");
                ddlRevenueType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUnpostingValue()
    {
        double actualInvoiceAmount = 0;
        double actualPostedValue = 0;
        double actualUnpostedValue = 0;
        double invoiceAmount = 0;
        double deltaInvoiceAmount = 0;


        if (!string.IsNullOrEmpty(Convert.ToString(hdInvoiceAmount.Value)))
            actualInvoiceAmount = Convert.ToDouble(hdInvoiceAmount.Value);
        else
            actualInvoiceAmount = 0;

        if (!string.IsNullOrEmpty(Convert.ToString(hdPostedValue.Value)))
            actualPostedValue = Convert.ToDouble(hdPostedValue.Value);
        else
            actualPostedValue = 0;

        if (!string.IsNullOrEmpty(Convert.ToString(hdActualUnpostedValue.Value)))
            actualUnpostedValue = Convert.ToDouble(hdActualUnpostedValue.Value);
        else
            actualUnpostedValue = 0;

        if (!string.IsNullOrEmpty(Convert.ToString(txtInvoiceAmount.Text)))
            invoiceAmount = Convert.ToDouble(txtInvoiceAmount.Text);
        else
            invoiceAmount = 0;


        deltaInvoiceAmount = invoiceAmount - actualInvoiceAmount;


        double pendingAmount = 0;
        if (!string.IsNullOrEmpty(Convert.ToString(hdPendingAmount.Value)))
            pendingAmount = Convert.ToDouble(hdPendingAmount.Value);
        else
            pendingAmount = 0;

        hdPendingAmount.Value = Convert.ToString(pendingAmount);


        double postingValue = 0;
        if (!string.IsNullOrEmpty(Convert.ToString(txtPostedValue.Text)))
            postingValue = Convert.ToDouble(txtPostedValue.Text);
        else
            postingValue = 0;

        hdUnpostingValue.Value = Convert.ToString(pendingAmount - postingValue + deltaInvoiceAmount);
        txtUnpostedValue.Text = hdUnpostingValue.Value;
    }

    private void UpdatePostedBill(int recordId, string invoiceNo)
    {
        try
        {
            string customerCode = string.Empty;
            string jobNo = string.Empty;

            string revenueAccount = string.Empty;
            string productCode = string.Empty;

            double invoiceAmount = 0;
            double postingValue = 0;
            int postingCurrencyID = 0;
            double unpostedValue = 0;
            double unpostingValue = 0;
            string endMarketCode = string.Empty;
            string countryCode = string.Empty;
            string postingMonth = string.Empty;
            int postingYear = 0;
            string postingDate = string.Empty;
            int typeID = 0;
            int revenueTypeID = 0;

            string revRecReduction = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;
            double editedValue = 0;

            revenueAccount = txtRevAccount.Text;

            if (!string.IsNullOrEmpty(txtProductCode.Text))
                productCode = Convert.ToString(txtProductCode.Text);
            else
                productCode = string.Empty;

            if (!string.IsNullOrEmpty(txtCustCode.Text))
                customerCode = Convert.ToString(txtCustCode.Text);
            else
                customerCode = string.Empty;


            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = Convert.ToString(txtJobNo.Text);
            else
                jobNo = string.Empty;

            if (Convert.ToDouble(txtInvoiceAmount.Text) > 0)
                invoiceAmount = Convert.ToDouble(txtInvoiceAmount.Text);
            else
                invoiceAmount = 0;

            postingValue = Convert.ToDouble(txtPostedValue.Text);

            if (Convert.ToInt32(ddlPostingCurrency.SelectedValue) > 0)
                postingCurrencyID = Convert.ToInt32(ddlPostingCurrency.SelectedValue);
            else
                postingCurrencyID = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(hdUnpostingValue.Value)))
                unpostingValue = Convert.ToDouble(hdUnpostingValue.Value);
            else
                unpostingValue = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(hdUnpostedValue.Value)))
                unpostedValue = Convert.ToDouble(hdUnpostedValue.Value);
            else
                unpostedValue = 0;

            if (Convert.ToInt32(ddlEndMarket.SelectedIndex) > 0)
                endMarketCode = Convert.ToString(ddlEndMarket.SelectedValue);
            else
                endMarketCode = string.Empty;

            if (Convert.ToInt32(ddlCountry.SelectedIndex) > 0)
                countryCode = Convert.ToString(ddlCountry.SelectedValue);
            else
                countryCode = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(ddlPostingMonth.SelectedValue)))
                postingMonth = Convert.ToString(ddlPostingMonth.SelectedValue);
            else
                postingMonth = string.Empty;

            if (Convert.ToInt32(ddlPostingYear.SelectedValue) > 0)
                postingYear = Convert.ToInt32(ddlPostingYear.SelectedValue);
            else
                postingYear = 0;

            postingDate = Convert.ToDateTime(postingYear + "-" + postingMonth + "-01").ToString("yyyy-MM-dd");

            if (ddlType.SelectedIndex > 0)
                typeID = Convert.ToInt32(ddlType.SelectedValue);
            else
                typeID = 0;

            if (ddlRevenueType.SelectedIndex > 0)
                revenueTypeID = Convert.ToInt32(ddlRevenueType.SelectedValue);
            else
                revenueTypeID = 0;

            if (!string.IsNullOrEmpty(txtRevRecReduction.Text))
                revRecReduction = txtRevRecReduction.Text;
            else
                revRecReduction = string.Empty;

            if (!string.IsNullOrEmpty(txtUDF2.Text))
                udf2 = txtUDF2.Text;
            else
                udf2 = string.Empty;

            if (!string.IsNullOrEmpty(txtUDF3.Text))
                udf3 = txtUDF3.Text;
            else
                udf3 = string.Empty;

            if (!string.IsNullOrEmpty(txtUDF4.Text))
                udf4 = txtUDF4.Text;
            else
                udf4 = string.Empty;

            if (!string.IsNullOrEmpty(txtUDF5.Text))
                udf5 = txtUDF5.Text;
            else
                udf5 = string.Empty;

            editedValue = Convert.ToDouble(unpostingValue) - Convert.ToDouble(unpostedValue);
            //int value = 1;

            int value = objPosting.UpdatePostedBillNewOne(recordId, revenueAccount, invoiceNo, invoiceAmount, customerCode, jobNo, productCode, postingValue,
                                        postingCurrencyID, unpostedValue, endMarketCode, countryCode, postingMonth, postingYear, postingDate, typeID, revenueTypeID,
                                        revRecReduction, udf2, udf3, udf4, udf5, editedValue, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Invoice no. " + invoiceNo + " updated successfully.");
                GetPostedList();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportGridviewToExcel()
    {
        try
        {
            string csv = string.Empty;
            //foreach (DataColumn column in dt.Columns)
            //{
            //    csv += column.ColumnName + ',';
            //}

            for (int i = 1; i < gvPostedList.Columns.Count; i++)
            {
                csv += Convert.ToString(gvPostedList.Columns[i].HeaderText) + ',';
            }

            csv += "\r\n";

            //foreach (DataRow row in dt.Rows)
            //{
            //    foreach (DataColumn column in dt.Columns)
            //    {
            //        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
            //    }
            //    csv += "\r\n";
            //}
            
            string rowTxt = string.Empty;

            foreach (GridViewRow gr in gvPostedList.Rows)
            {
                for (int j = 1; j < gvPostedList.Columns.Count; j++)
                {
                    
                    if (!string.IsNullOrEmpty(Convert.ToString(gr.Cells[j].Text)) && Convert.ToString(gr.Cells[j].Text) != "&nbsp;")
                        rowTxt = Convert.ToString(gr.Cells[j].Text);
                    else
                        rowTxt = string.Empty;

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';

                }
                csv += "\r\n";
            }

            string fileName = "Posted_Invoices_In_Detail" + DateTime.Now.ToString("dd_MMM_yyyy");
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
