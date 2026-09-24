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

public partial class USGAAP_BILLING_AddNewPosting : System.Web.UI.Page
{

    #region VARIABLES[==================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Posting objPosting = new BAL.Posting();
    BAL.Common objCommon = new BAL.Common();


    DataSet dsDBDetails = new DataSet();
    DataSet dsCustomer = new DataSet();
    DataSet dsProductDetail = new DataSet();
    DataSet dsRevenueAccDetail = new DataSet();

    DataSet dsUnit = new DataSet();
    DataSet dsCountry = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsEndMarket = new DataSet();

    DataSet dsType = new DataSet();
    DataSet dsRevenueType = new DataSet();

    #endregion


    #region EVENTS[=====================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdInvoiceDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtInvoiceDate.Text = hdInvoiceDate.Value;

                //hdPostedDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtPostedDate.Text = hdPostedDate.Value;

                ddlPostingMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlPostingYear.SelectedValue = DateTime.Now.Year.ToString();

                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                BindUnit();
                BindPostingCurrency();
                BindEndMarket();
                BindCountry();

                BindType();

                ddlRevenueType.Items.Clear();
                ddlRevenueType.Items.Insert(0, "Select");
                ddlRevenueType.SelectedIndex = 0;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSelectCustomer_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }

    protected void btnSearchCustomer_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
        GetCustomerDetail();
    }

    protected void gvCustomerDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblCustomerCode = gvCustomerDetail.Rows[rowindex].FindControl("lblCustomerCode") as Label;
                Label lblCustomerName = gvCustomerDetail.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblAddress = gvCustomerDetail.Rows[rowindex].FindControl("lblAddress") as Label;

                txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                txtCustomerCode.Text = Convert.ToString(lblCustomerCode.Text).Trim();
                txtCustAddress.Text = Convert.ToString(lblAddress.Text).Trim(',');
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


    protected void btnSelectProdCode_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }

    protected void btnSearchProductCode_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        GetProductDetail();
    }

    protected void gvProductCode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblProductCode = gvProductDetail.Rows[rowindex].FindControl("lblProductCode") as Label;
                txtProductCode.Text = Convert.ToString(lblProductCode.Text).Trim();
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
        if (ddlType.SelectedIndex > 0)
        {
            dsRevenueType = objPosting.GetRevenueType(Convert.ToInt32(ddlType.SelectedValue));
            if (dsRevenueType.Tables.Count > 0 && dsRevenueType.Tables[0].Rows.Count > 0)
            {
                ddlRevenueType.DataSource = dsRevenueType.Tables[0];
                ddlRevenueType.DataTextField = "REVENUE_TYPE";
                ddlRevenueType.DataValueField = "REVENUE_TYPE_ID";
                ddlRevenueType.DataBind();
                ddlRevenueType.Items.Insert(0, "Select");
            }
        }
        else
        {
            ddlRevenueType.Items.Clear();
            ddlRevenueType.Items.Insert(0, "Select");
            ddlRevenueType.SelectedIndex = 0;
        }
    }

    protected void btnGetRevenueaccount_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();



    }

    protected void btnSearchRevenueAccount_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();
        GetRevenueAccountDetail();
    }

    protected void gvRevenueAccount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblRevenueAccount = gvRevenueAccount.Rows[rowindex].FindControl("lblRevenueAccount") as Label;
                Label lblRevenueAccountDesc = gvRevenueAccount.Rows[rowindex].FindControl("lblRevenueAccountDesc") as Label;
                Label lblRevenueAccountType = gvRevenueAccount.Rows[rowindex].FindControl("lblRevenueAccountType") as Label;

                txtRevenueAccount.Text = Convert.ToString(lblRevenueAccount.Text).Trim();
                txtRevenueAccountDesc.Text = Convert.ToString(lblRevenueAccountDesc.Text).Trim();
                txtRevenueAccountType.Text = Convert.ToString(lblRevenueAccountType.Text).Trim();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewPosting();
    }

    protected void btnPostedList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/USGAAP_BILLING/PostedList.aspx");
    }

    #endregion


    #region METHODS[====================]

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
                ddlCompany.Items.Insert(0, "Select");
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
                ddlPostingCurrency.SelectedValue = "68";
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

    private void GetCustomerDetail()
    {
        try
        {
            string customerName = string.Empty;
            string customerCode = string.Empty;
            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;

            dsDBDetails = (DataSet)Session["DB_DETAILS"];
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

            if (!string.IsNullOrEmpty(txtCustmerNameSearch.Text))
                customerName = txtCustmerNameSearch.Text;
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerCodeSearch.Text))
                customerCode = txtCustomerCodeSearch.Text;
            else
                customerCode = string.Empty;

            dsCustomer = objPosting.GetCustomerDetails(customerName, customerCode, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);
            if (dsCustomer.Tables.Count > 0 && dsCustomer.Tables[0].Rows.Count > 0)
            {
                lblCustomerMsg.Visible = false;
                lblCustomerMsg.Text = string.Empty;
                gvCustomerDetail.DataSource = dsCustomer.Tables[0];
                gvCustomerDetail.DataBind();
            }
            else
            {
                lblCustomerMsg.Visible = true;
                lblCustomerMsg.Text = "No data found!";
                gvCustomerDetail.DataSource = null;
                gvCustomerDetail.DataBind();
            }
            lblCustomerRecords.Text = "Records[" + gvCustomerDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void GetProductDetail()
    {
        try
        {
            string productCode = string.Empty;
            string productDesc = string.Empty;
            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;

            dsDBDetails = (DataSet)Session["DB_DETAILS"];
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

            if (!string.IsNullOrEmpty(txtProcuctCodeSearch.Text))
                productCode = txtProcuctCodeSearch.Text;
            else
                productCode = string.Empty;

            if (!string.IsNullOrEmpty(txtProcuctDescSearch.Text))
                productDesc = txtProcuctDescSearch.Text;
            else
                productDesc = string.Empty;




            dsProductDetail = objPosting.GetProductDetail(productCode, productDesc, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);
            if (dsProductDetail.Tables.Count > 0 && dsProductDetail.Tables[0].Rows.Count > 0)
            {
                lblProductMsg.Visible = false;
                lblProductMsg.Text = string.Empty;
                gvProductDetail.DataSource = dsProductDetail.Tables[0];
                gvProductDetail.DataBind();
            }
            else
            {
                lblProductMsg.Visible = true;
                lblProductMsg.Text = "No data found!";
                gvProductDetail.DataSource = null;
                gvProductDetail.DataBind();
            }
            lblProductRecords.Text = "Records[" + dsProductDetail.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void GetRevenueAccountDetail()
    {
        try
        {
            string revenueAccount = string.Empty;
            string revenueAccountDesc = string.Empty;
            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;

            dsDBDetails = (DataSet)Session["DB_DETAILS"];
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

            if (!string.IsNullOrEmpty(txtRevenueAccountSearch.Text))
                revenueAccount = txtRevenueAccountSearch.Text;
            else
                revenueAccount = string.Empty;

            if (!string.IsNullOrEmpty(txtRevenueAccountDescSearch.Text))
                revenueAccountDesc = txtRevenueAccountDescSearch.Text;
            else
                revenueAccountDesc = string.Empty;


            dsRevenueAccDetail = objPosting.GetRevenueAccountDetail(revenueAccount, revenueAccountDesc, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);
            if (dsRevenueAccDetail.Tables.Count > 0 && dsRevenueAccDetail.Tables[0].Rows.Count > 0)
            {
                lblRevenueAccMsg.Visible = false;
                lblRevenueAccMsg.Text = string.Empty;
                gvRevenueAccount.DataSource = dsRevenueAccDetail.Tables[0];
                gvRevenueAccount.DataBind();
            }
            else
            {
                lblRevenueAccMsg.Visible = true;
                lblRevenueAccMsg.Text = "No data found!";
                gvRevenueAccount.DataSource = null;
                gvRevenueAccount.DataBind();
            }
            lblRevenueAccRecords.Text = "Records[" + dsRevenueAccDetail.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void AddNewPosting()
    {
        try
        {
            string invoiceNo = string.Empty;
            string unitName = string.Empty;
            string invoiceDate = string.Empty;
            string jobNo = string.Empty;
            string customerName = string.Empty;
            string customerCode = string.Empty;
            string businessUnit = string.Empty;
            string productCode = string.Empty;
            string revenueAccount = string.Empty;
            string revenueAccountDesc = string.Empty;
            int revenueAccountType = 0;
            double quantity = 0;
            double productRate = 0;


            double invoiceAmount = 0;

            string postedValue = string.Empty;
            int postedValueCurrencyID = 0;
            string postedDate = string.Empty;
            double unpostedValue = 0;

            string endMarketCode = string.Empty;
            string countryCode = string.Empty;

            string postingMonth = string.Empty;
            int postingYear = 0;

            int typeID = 0;
            int revenueTypeID = 0;

            string revRecReduction = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;


            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                invoiceNo = txtInvoiceNo.Text;
            else
                invoiceNo = string.Empty;


            if (ddlCompany.SelectedIndex > 0)
                unitName = ddlCompany.SelectedItem.Text;
            else
                unitName = string.Empty;


            invoiceDate = Convert.ToDateTime(hdInvoiceDate.Value).ToString("yyyy-MM-dd");


            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text;
            else
                jobNo = string.Empty;


            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;


            if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                customerCode = txtCustomerCode.Text;
            else
                customerCode = string.Empty;


            if (!string.IsNullOrEmpty(txtBusinessUnit.Text))
                businessUnit = txtBusinessUnit.Text;
            else
                businessUnit = string.Empty;


            if (!string.IsNullOrEmpty(txtProductCode.Text))
                productCode = txtProductCode.Text;
            else
                productCode = string.Empty;


            if (!string.IsNullOrEmpty(txtRevenueAccount.Text))
                revenueAccount = txtRevenueAccount.Text;
            else
                revenueAccount = string.Empty;


            if (!string.IsNullOrEmpty(txtRevenueAccountDesc.Text))
                revenueAccountDesc = txtRevenueAccountDesc.Text;
            else
                revenueAccountDesc = string.Empty;


            if (!string.IsNullOrEmpty(txtRevenueAccountType.Text))
            {
                if (txtRevenueAccountType.Text == "P")
                    revenueAccountType = 0;
                else
                    revenueAccountType = 1;
            }
            else
                revenueAccountType = 1;


            if (Convert.ToDouble(txtQuantity.Text) > 0)
                quantity = Convert.ToDouble(txtQuantity.Text);
            else
                quantity = 0;


            if (Convert.ToDouble(txtProductRate.Text) > 0)
                productRate = Convert.ToDouble(txtProductRate.Text);
            else
                productRate = 0;


            if (Convert.ToDouble(txtInvoiceAmount.Text) == 0 || string.IsNullOrEmpty(Convert.ToString(txtInvoiceAmount.Text)) || Convert.ToString(txtInvoiceAmount.Text) == "-")
                invoiceAmount = 0;
            else
                invoiceAmount = Convert.ToDouble(txtInvoiceAmount.Text);


            if (Convert.ToDouble(txtPostedValue.Text) == 0 || string.IsNullOrEmpty(Convert.ToString(txtPostedValue.Text)) || Convert.ToString(txtPostedValue.Text) == "-")
                postedValue = "0";
            else
                postedValue = Convert.ToString(txtPostedValue.Text);


            if (ddlPostingCurrency.SelectedIndex > 0)
                postedValueCurrencyID = Convert.ToInt32(ddlPostingCurrency.SelectedValue);
            else
                postedValueCurrencyID = 0;


            if (Convert.ToDouble(hdUnpostedValue.Value) > 0)
                unpostedValue = Convert.ToDouble(hdUnpostedValue.Value);
            else
                unpostedValue = 0;


            if (ddlEndMarket.SelectedIndex > 0)
            {
                endMarketCode = Convert.ToString(ddlEndMarket.SelectedValue);
            }
            else
                endMarketCode = string.Empty;


            if (ddlCountry.SelectedIndex > 0)
                countryCode = Convert.ToString(ddlCountry.SelectedValue);
            else
                countryCode = string.Empty;


            postingMonth = ddlPostingMonth.SelectedItem.Text;
            postingYear = Convert.ToInt32(ddlPostingYear.SelectedValue);

            postedDate = Convert.ToDateTime(postingYear + "-" + postingMonth + "-01").ToString("yyyy-MM-dd");
            //Convert.ToDateTime(hdPostedDate.Value).ToString("yyyy-MM-dd");

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

            if (!string.IsNullOrEmpty(countryCode) && !string.IsNullOrEmpty(endMarketCode))
            {

                int value = objPosting.AddNewPostingNew(invoiceNo, unitName, invoiceDate, jobNo, customerName, customerCode, businessUnit, productCode,
                            revenueAccount, revenueAccountDesc, revenueAccountType, quantity, productRate, invoiceAmount, postedValue, postedValueCurrencyID,
                            postedDate, unpostedValue, endMarketCode, countryCode, postingMonth, postingYear, typeID, revenueTypeID, revRecReduction, udf2, udf3, udf4, udf5, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (value > 0)
                {
                    Reset();
                    SuccessMessage("Record added successfully.");
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again!");
                    return;
                }
            }
            else
            {
                ExceptionMessage("Please select Geogrophy and End Market!");
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
            txtInvoiceNo.Text = string.Empty;
            ddlCompany.SelectedIndex = 0;
            hdInvoiceDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            //txtPostedDate.Text = hdInvoiceDate.Value;
            txtJobNo.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCustomerCode.Text = string.Empty;
            txtCustAddress.Text = string.Empty;
            txtBusinessUnit.Text = string.Empty;
            txtProductCode.Text = string.Empty;
            txtRevenueAccount.Text = string.Empty;
            txtRevenueAccountDesc.Text = string.Empty;
            txtRevenueAccountType.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtProductRate.Text = string.Empty;
            txtInvoiceAmount.Text = string.Empty;
            txtPostedValue.Text = string.Empty;
            //hdPostedDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            //txtPostedDate.Text = hdPostedDate.Value;
            hdUnpostedValue.Value = string.Empty;
            txtUnpostedValue.Text = hdUnpostedValue.Value;
            ddlEndMarket.SelectedIndex = 0;
            ddlCountry.SelectedIndex = 0;
            txtRevRecReduction.Text = string.Empty;
            txtUDF2.Text = string.Empty;
            txtUDF3.Text = string.Empty;
            txtUDF4.Text = string.Empty;
            txtUDF5.Text = string.Empty;
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
