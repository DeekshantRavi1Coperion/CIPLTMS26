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

public partial class USGAAP_BILLING_UnpostedListNew : System.Web.UI.Page
{

    #region VARIABLES[=======================]


    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Posting objPosting = new BAL.Posting();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsSaleBill = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsEndMarket = new DataSet();
    DataSet dsCountry = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string invoiceNo = string.Empty;
    int unitId = 0;
    string unitName = string.Empty;
    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;
    string unitNameA35 = string.Empty;
    string unitNameDLH = string.Empty;
    string unitNameSEZ = string.Empty;
    string unitNameGNU = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["UNPOSTED_LIST"] = null;

                dsEndMarket = objPosting.GetEndMarket();
                Session["dsEndMarket"] = dsEndMarket;

                dsCountry = objCommon.GetCountry();
                Session["dsCountry"] = dsCountry;

                dsCurrency = objTourAndTravels.GetPrimaryDetails("sp_get_currency_list");
                Session["dsCurrency"] = dsCurrency;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;
                BindUnit();
                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                GetPostingList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        chkClearUnclearAll.Checked = false;
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetPostingList();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        chkClearUnclearAll.Checked = false;
        if (gvPostingList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["UNPOSTED_LIST"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    protected void chkClearUnclearAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkClearUnclearAll.Checked)
        {
            if (gvPostingList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvPostingList.Rows)
                {
                    Label lblInvoiceAmount = (Label)gr.FindControl("lblInvoiceAmount");
                    Label lblPendingUnpostedValue = (Label)gr.FindControl("lblPendingUnpostedValue");
                    TextBox txtPostingValue = (TextBox)gr.FindControl("txtPostingValue");
                    TextBox txtUnPostedValue = (TextBox)gr.FindControl("txtUnPostedValue");

                    txtPostingValue.Text = "0.00";
                    if (Convert.ToDouble(lblPendingUnpostedValue.Text) > 0)
                    {
                        txtUnPostedValue.Text = lblPendingUnpostedValue.Text;
                    }
                    else
                    {
                        txtUnPostedValue.Text = lblInvoiceAmount.Text;
                    }
                }
            }
        }

        else
        {
            if (gvPostingList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvPostingList.Rows)
                {
                    Label lblInvoiceAmount = (Label)gr.FindControl("lblInvoiceAmount");
                    Label lblPendingUnpostedValue = (Label)gr.FindControl("lblPendingUnpostedValue");
                    TextBox txtPostingValue = (TextBox)gr.FindControl("txtPostingValue");
                    TextBox txtUnPostedValue = (TextBox)gr.FindControl("txtUnPostedValue");

                    txtUnPostedValue.Text = "0.00";
                    if (Convert.ToDouble(lblPendingUnpostedValue.Text) > 0)
                    {
                        txtPostingValue.Text = lblPendingUnpostedValue.Text;
                    }
                    else
                    {
                        txtPostingValue.Text = lblInvoiceAmount.Text;
                    }
                }
            }
        }
    }

    protected void gvPostingList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");

                Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Label lblEndMarketID = (Label)e.Row.FindControl("lblEndMarketID");
                Label lblCountryID = (Label)e.Row.FindControl("lblCountryID");
                Label lblPostingYear = (Label)e.Row.FindControl("lblPostingYear");
                Label lblPostingCurrencyID = (Label)e.Row.FindControl("lblPostingCurrencyID");


                Label lblInvoiceAmount = (Label)e.Row.FindControl("lblInvoiceAmount");
                Label lblPendingUnpostedValue = (Label)e.Row.FindControl("lblPendingUnpostedValue");
                TextBox txtPostingValue = (TextBox)e.Row.FindControl("txtPostingValue");

                if (Convert.ToDouble(lblPendingUnpostedValue.Text) > 0)
                {
                    txtPostingValue.Text = Convert.ToString(lblPendingUnpostedValue.Text);
                }
                else
                {
                    txtPostingValue.Text = Convert.ToString(lblInvoiceAmount.Text);
                }


                DataSet dtEndMarket = (DataSet)Session["dsEndMarket"];
                DataSet dtCountry = (DataSet)Session["dsCountry"];
                DataSet dtCurrency = (DataSet)Session["dsCurrency"];
                DropDownList ddlEndMarket = (e.Row.FindControl("ddlEndMarket") as DropDownList);
                DropDownList ddlCountry = (e.Row.FindControl("ddlCountry") as DropDownList);

                DropDownList ddlPostingCurrency = (e.Row.FindControl("ddlPostingCurrency") as DropDownList);
                DropDownList ddlPostingMonth = (e.Row.FindControl("ddlPostingMonth") as DropDownList);
                DropDownList ddlPostingYear = (e.Row.FindControl("ddlPostingYear") as DropDownList);

                if (dtEndMarket.Tables.Count > 0 && dtEndMarket.Tables[0].Rows.Count > 0)
                {
                    ddlEndMarket.DataSource = dtEndMarket.Tables[0];
                    ddlEndMarket.DataTextField = "END_MARKET_NAME";
                    ddlEndMarket.DataValueField = "END_MARKET_ID";
                    ddlEndMarket.DataBind();

                    //if (Convert.ToInt32(lblRecordID.Text) > 0)
                    //{
                    //    ddlEndMarket.SelectedValue = Convert.ToString(lblEndMarketID.Text);
                    //    ddlEndMarket.Enabled = false;
                    //}       

                    foreach (DataRow dr in dsSaleBill.Tables[0].Select("STATUS='P'"))
                    {
                        if (Convert.ToString(lblInvoiceNo.Text) == Convert.ToString(dr["INVOICE_NO"]))
                        {
                            ddlEndMarket.SelectedValue = Convert.ToString(dr["END_MARKET"]);
                            ddlEndMarket.Enabled = false;
                        }
                    }
                }

                if (dtCountry.Tables.Count > 0 && dtCountry.Tables[0].Rows.Count > 0)
                {
                    ddlCountry.DataSource = dtCountry.Tables[0];
                    ddlCountry.DataTextField = "COUNTRY_NAME";
                    ddlCountry.DataValueField = "COUNTRY_ID";
                    ddlCountry.DataBind();
                    ddlCountry.SelectedValue = "95";

                    foreach (DataRow dr in dsSaleBill.Tables[0].Select("STATUS='P'"))
                    {
                        if (Convert.ToString(lblInvoiceNo.Text) == Convert.ToString(dr["INVOICE_NO"]))
                        {
                            ddlCountry.SelectedValue = Convert.ToString(dr["COUNTRY_ID"]);
                            ddlCountry.Enabled = false;
                        }
                    }
                }

                if (dtCurrency.Tables.Count > 0 && dtCurrency.Tables[0].Rows.Count > 0)
                {
                    ddlPostingCurrency.DataSource = dtCurrency.Tables[0];
                    ddlPostingCurrency.DataTextField = "CURRENCY_CODE";
                    ddlPostingCurrency.DataValueField = "CURRENCY_ID";
                    ddlPostingCurrency.DataBind();
                    ddlPostingCurrency.SelectedValue = "68";

                    foreach (DataRow dr in dsSaleBill.Tables[0].Select("STATUS='P'"))
                    {
                        if (Convert.ToString(lblInvoiceNo.Text) == Convert.ToString(dr["INVOICE_NO"]))
                        {
                            ddlPostingCurrency.SelectedValue = Convert.ToString(lblPostingCurrencyID.Text);
                            ddlPostingCurrency.Enabled = false;
                        }
                    }
                }

                ddlPostingMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlPostingYear.SelectedValue = DateTime.Now.Year.ToString();

                if (Convert.ToInt32(lblRecordID.Text) > 0)
                {
                    ddlPostingYear.SelectedValue = Convert.ToString(lblPostingYear.Text);
                    ddlPostingYear.Enabled = false;
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

    protected void btnPost_Click(object sender, EventArgs e)
    {
        chkClearUnclearAll.Checked = false;
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        InesrtPosting();
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

    private void GetPostingList()
    {
        try
        {
            dsDBDetails = (DataSet)Session["DB_DETAILS"];
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;


            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                invoiceNo = txtInvoiceNo.Text;
            else
                invoiceNo = string.Empty;


            if (rdSingle.Checked == true && rdAll.Checked == false)
            {
                if (ddlCompany.SelectedIndex > 0)
                {
                    unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                    unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
                }
                dsSaleBill = objPosting.GetUnpostedList(unitId, unitName, fromDate, toDate, invoiceNo);
            }

            if (rdSingle.Checked == false && rdAll.Checked == true)
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        {
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameA35 = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        {
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameDLH = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        {
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameSEZ = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        {
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameGNU = Convert.ToString(dr["UNIT_NAME"]);
                        }
                    }
                }
                dsSaleBill = objPosting.GetUnpostedListAllUnits(fromDate, toDate, invoiceNo, dbNameA35, unitNameA35, dbNameDLH, unitNameDLH, dbNameSEZ, unitNameSEZ, dbNameGNU, unitNameGNU);
            }

            if (dsSaleBill.Tables.Count > 0 && dsSaleBill.Tables[0].Rows.Count > 0)
            {
                Session["UNPOSTED_LIST"] = dsSaleBill;
                gvPostingList.DataSource = dsSaleBill.Tables[0];
                gvPostingList.DataBind();
            }
            else
            {
                Session["UNPOSTED_LIST"] = null;
                gvPostingList.DataSource = null;
                gvPostingList.DataBind();
            }
            lblRecords.Text = "Records[" + dsSaleBill.Tables[0].Rows.Count + "]";

            if (rdSingle.Checked)
            {
                rdSingle.Checked = true;
                rdAll.Checked = false;
                ddlCompany.Enabled = true;
            }
            else
            {
                rdSingle.Checked = false;
                rdAll.Checked = true;
                ddlCompany.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void InesrtPosting()
    {
        try
        {

            int recordID = 0;
            string invoiceNo = string.Empty;
            string invoiceDate = string.Empty;
            string customerCode = string.Empty;
            string customerName = string.Empty;
            string jobNo = string.Empty;
            string businessUnit = string.Empty;
            string productCode = string.Empty;
            string revenueAccount = string.Empty;
            string revenueAccountDesc = string.Empty;
            int revenueAccountType = 0;
            double quantity = 0;
            double productRate = 0;
            double invoiceAmount = 0;
            string location = string.Empty;
            int endMarketID = 0;
            int countryID = 0;
            string postingMonth = string.Empty;
            int postingYear = 0;
            string financialYear = string.Empty;
            double postingValue = 0;
            int postingCurrencyID = 0;
            double unpostedValue = 0;
            string postingDate = string.Empty;
            double postedValue = 0;
            string status = string.Empty;
            string udf1 = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;

            //string valueQueryTxt = string.Empty;
            int value = 0;
            foreach (GridViewRow gr in gvPostingList.Rows)
            {
                Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                Label lblInvoiceNo = (Label)gr.FindControl("lblInvoiceNo");
                Label lblInvoiceDate = (Label)gr.FindControl("lblInvoiceDate");
                Label lblCustomerCode = (Label)gr.FindControl("lblCustomerCode");
                Label lblCustomerName = (Label)gr.FindControl("lblCustomerName");
                Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                Label lblBusinessUnit = (Label)gr.FindControl("lblBusinessUnit");
                Label lblProductCode = (Label)gr.FindControl("lblProductCode");
                Label lblRevenueAccount = (Label)gr.FindControl("lblRevenueAccount");
                Label lblRevenueAccountDesc = (Label)gr.FindControl("lblRevenueAccountDesc");
                Label lblRevenueAccountType = (Label)gr.FindControl("lblRevenueAccountType");
                Label lblQuantity = (Label)gr.FindControl("lblQuantity");
                Label lblProductRate = (Label)gr.FindControl("lblProductRate");
                Label lblInvoiceAmount = (Label)gr.FindControl("lblInvoiceAmount");
                Label lblLocation = (Label)gr.FindControl("lblLocation");
                Label lblPostedValue = (Label)gr.FindControl("lblPostedValue");
                Label lblPendingUnpostedValue = (Label)gr.FindControl("lblPendingUnpostedValue");


                DropDownList ddlEndMarket = (DropDownList)gr.FindControl("ddlEndMarket");
                DropDownList ddlCountry = (DropDownList)gr.FindControl("ddlCountry");
                DropDownList ddlPostingMonth = (DropDownList)gr.FindControl("ddlPostingMonth");
                DropDownList ddlPostingYear = (DropDownList)gr.FindControl("ddlPostingYear");
                TextBox txtPostingValue = (TextBox)gr.FindControl("txtPostingValue");
                DropDownList ddlPostingCurrency = (DropDownList)gr.FindControl("ddlPostingCurrency");
                TextBox txtUnPostedValue = (TextBox)gr.FindControl("txtUnPostedValue");
                TextBox txtUDF1 = (TextBox)gr.FindControl("txtUDF1");
                TextBox txtUDF2 = (TextBox)gr.FindControl("txtUDF2");
                TextBox txtUDF3 = (TextBox)gr.FindControl("txtUDF3");
                TextBox txtUDF4 = (TextBox)gr.FindControl("txtUDF4");
                TextBox txtUDF5 = (TextBox)gr.FindControl("txtUDF5");

                recordID = Convert.ToInt32(lblRecordID.Text);
                invoiceNo = Convert.ToString(lblInvoiceNo.Text);
                if (!string.IsNullOrEmpty(lblInvoiceDate.Text))
                    invoiceDate = Convert.ToDateTime(lblInvoiceDate.Text).ToString("yyyy-MM-dd");
                customerCode = Convert.ToString(lblCustomerCode.Text);
                customerName = Convert.ToString(lblCustomerName.Text);
                jobNo = Convert.ToString(lblJobNo.Text);
                businessUnit = Convert.ToString(lblBusinessUnit.Text);
                productCode = Convert.ToString(lblProductCode.Text);
                revenueAccount = Convert.ToString(lblRevenueAccount.Text);
                revenueAccountDesc = Convert.ToString(lblRevenueAccountDesc.Text);

                if (Convert.ToString(lblRevenueAccountType.Text) == "B")
                    revenueAccountType = 0;
                else if (Convert.ToString(lblRevenueAccountType.Text) == "P")
                    revenueAccountType = 1;

                quantity = Convert.ToDouble(lblQuantity.Text);
                productRate = Convert.ToDouble(lblProductRate.Text);
                invoiceAmount = Convert.ToDouble(lblInvoiceAmount.Text);
                location = Convert.ToString(lblLocation.Text);

                endMarketID = Convert.ToInt32(ddlEndMarket.SelectedValue);
                countryID = Convert.ToInt32(ddlCountry.SelectedValue);
                postingMonth = Convert.ToString(ddlPostingMonth.SelectedItem.Text);
                postingYear = Convert.ToInt32(ddlPostingYear.SelectedValue);

                if (!string.IsNullOrEmpty(txtPostingValue.Text))
                    postingValue = Convert.ToDouble(txtPostingValue.Text);
                else
                    postingValue = 0;

                postingCurrencyID = Convert.ToInt32(ddlPostingCurrency.SelectedValue);

                if (!string.IsNullOrEmpty(txtUnPostedValue.Text))
                    unpostedValue = Convert.ToDouble(txtUnPostedValue.Text);
                else
                    unpostedValue = 0;

                postingDate = Convert.ToDateTime(postingYear + "-" + postingMonth + "-01").ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(lblPostedValue.Text))
                    postedValue = Convert.ToDouble(lblPostedValue.Text);
                else
                    postedValue = 0;

                udf1 = Convert.ToString(txtUDF1.Text);
                udf2 = Convert.ToString(txtUDF2.Text);
                udf3 = Convert.ToString(txtUDF3.Text);
                udf4 = Convert.ToString(txtUDF4.Text);
                udf5 = Convert.ToString(txtUDF5.Text);

                if (postingValue > 0 && unpostedValue >= 0)
                {
                    value = objPosting.InesrtUpdatePosting(recordID, invoiceNo, invoiceDate, customerCode, customerName, jobNo, businessUnit, productCode, revenueAccount, revenueAccountDesc, revenueAccountType,
                                                            quantity, productRate, invoiceAmount, location, endMarketID, countryID, postingMonth,
                                                            postingYear, financialYear, postingValue, postingCurrencyID, unpostedValue, postingDate,
                                                            0, 0,
                                                            udf1, udf2, udf3, udf4, udf5, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                }
            }

            if (value > 0)
            {
                SuccessMessage("Posting done successfully.");
                Session["UNPOSTED_LIST"] = null;
                gvPostingList.DataSource = null;
                gvPostingList.DataBind();
                lblRecords.Text = "Records[" + gvPostingList.Rows.Count + "]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
            foreach (DataColumn column in dt.Columns)
            {
                csv += column.ColumnName + ',';
            }
            csv += "\r\n";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "Unposted_Invoice_List" + DateTime.Now.ToString("dd_MMM_yyyy");
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
