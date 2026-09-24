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

public partial class POSTING_PostingList : System.Web.UI.Page
{

    #region VARIABLES[=======================]


    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Posting objPosting = new BAL.Posting();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsSaleBill = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsCountry = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
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
                Session["SALE_BILL_REPORT"] = null;

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
        GetPostingList();
    }

    protected void gvPostingList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataSet dtCountry = (DataSet)Session["dsCountry"];
                DataSet dtCurrency = (DataSet)Session["dsCurrency"];
                DropDownList ddlCountry = (e.Row.FindControl("ddlCountry") as DropDownList);

                DropDownList ddlPostingCurrency = (e.Row.FindControl("ddlPostingCurrency") as DropDownList);
                DropDownList ddlPostingMonth = (e.Row.FindControl("ddlPostingMonth") as DropDownList);
                DropDownList ddlPostingYear = (e.Row.FindControl("ddlPostingYear") as DropDownList);

                if (dtCurrency.Tables.Count > 0 && dtCurrency.Tables[0].Rows.Count > 0)
                {
                    ddlPostingCurrency.DataSource = dtCurrency.Tables[0];
                    ddlPostingCurrency.DataTextField = "CURRENCY_CODE";
                    ddlPostingCurrency.DataValueField = "CURRENCY_ID";
                    ddlPostingCurrency.DataBind();
                    ddlPostingCurrency.SelectedValue = "68";
                }

                if (dtCountry.Tables.Count > 0 && dtCountry.Tables[0].Rows.Count > 0)
                {
                    ddlCountry.DataSource = dtCountry.Tables[0];
                    ddlCountry.DataTextField = "COUNTRY_NAME";
                    ddlCountry.DataValueField = "COUNTRY_ID";
                    ddlCountry.DataBind();
                    ddlCountry.SelectedValue = "95";
                }

                ddlPostingMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlPostingYear.SelectedValue = DateTime.Now.Year.ToString();

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



            if (rdSingle.Checked == true && rdAll.Checked == false)
            {
                if (ddlCompany.SelectedIndex > 0)
                {
                    unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                    unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
                }
                dsSaleBill = objPosting.GetPostingList(unitId, unitName, fromDate, toDate);

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
                dsSaleBill = objPosting.GetPostingListAllUnits(fromDate, toDate, dbNameA35, unitNameA35, dbNameDLH, unitNameDLH, dbNameSEZ, unitNameSEZ, dbNameGNU, unitNameGNU);
            }

            if (dsSaleBill.Tables.Count > 0 && dsSaleBill.Tables[0].Rows.Count > 0)
            {
                Session["SALE_BILL_REPORT"] = dsSaleBill;
                gvPostingList.DataSource = dsSaleBill.Tables[0];
                gvPostingList.DataBind();
            }
            else
            {
                Session["SALE_BILL_REPORT"] = null;
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
            string month = string.Empty;
            string jobNo = string.Empty;
            string custoerCode = string.Empty;
            string customerName = string.Empty;
            string billDate = string.Empty;
            string classTxt = string.Empty;
            string currencyDesc = string.Empty;
            double rate = 0;
            double fCurrency = 0;
            double amountInr = 0;
            string billNo = string.Empty;
            string location = string.Empty;
            string businessSegment = string.Empty;
            string class1Txt = string.Empty;
            string type = string.Empty;
            string doc = string.Empty;
            int warranty = 0;
            double percentage = 0;
            double percentageAmount = 0;
            string glCode = string.Empty;
            string endMarket = string.Empty;
            int countryID = 0;
            int postingMonth = 0;
            int postingYear = 0;
            double postingValue = 0;
            int postingCurrency = 0;
            double unpostedValue = 0;
            string udf1 = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;

            string valueQueryTxt = string.Empty;

            foreach (GridViewRow gr in gvPostingList.Rows)
            {
                Label lblMonth = (Label)gr.FindControl("lblMonth");
                Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                Label lblCustCode = (Label)gr.FindControl("lblCustCode");
                Label lblCustomerName = (Label)gr.FindControl("lblCustomerName");
                Label lblBillDate = (Label)gr.FindControl("lblBillDate");
                Label lblClass = (Label)gr.FindControl("lblClass");
                Label lblCurrDesc = (Label)gr.FindControl("lblCurrDesc");
                Label lblRate = (Label)gr.FindControl("lblRate");
                Label lblFcurrency = (Label)gr.FindControl("lblFcurrency");
                Label lblAmountINR = (Label)gr.FindControl("lblAmountINR");
                Label lblBillNo = (Label)gr.FindControl("lblBillNo");
                Label lblLocation = (Label)gr.FindControl("lblLocation");
                Label lblBusinessSegment = (Label)gr.FindControl("lblBusinessSegment");
                Label lblClass1 = (Label)gr.FindControl("lblClass1");
                Label lblType = (Label)gr.FindControl("lblType");
                Label lblDoc = (Label)gr.FindControl("lblDoc");
                Label lblWarranty = (Label)gr.FindControl("lblWarranty");
                Label lblPercentage = (Label)gr.FindControl("lblPercentage");
                Label lblPercentageAmount = (Label)gr.FindControl("lblPercentageAmount");
                Label lblGlcode = (Label)gr.FindControl("lblGlcode");

                TextBox txtEndMarket = (TextBox)gr.FindControl("txtEndMarket");
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


                month = Convert.ToString(lblMonth.Text);
                jobNo = Convert.ToString(lblJobNo.Text);
                custoerCode = Convert.ToString(lblCustCode.Text);
                customerName = Convert.ToString(lblCustomerName.Text);
                if (!string.IsNullOrEmpty(lblBillDate.Text))
                    billDate = Convert.ToDateTime(lblBillDate.Text).ToString("yyyy-MM-dd");
                classTxt = Convert.ToString(lblClass.Text);
                currencyDesc = Convert.ToString(lblCurrDesc.Text);
                rate = Convert.ToDouble(lblRate.Text);
                fCurrency = Convert.ToDouble(lblFcurrency.Text);
                amountInr = Convert.ToDouble(lblAmountINR.Text);
                billNo = Convert.ToString(lblBillNo.Text);
                location = Convert.ToString(lblLocation.Text);
                businessSegment = Convert.ToString(lblBusinessSegment.Text);
                class1Txt = Convert.ToString(lblClass1.Text);
                type = Convert.ToString(lblType.Text);
                doc = Convert.ToString(lblDoc.Text);
                warranty = Convert.ToInt32(lblWarranty.Text);
                percentage = Convert.ToDouble(lblPercentage.Text);
                percentageAmount = Convert.ToDouble(lblPercentageAmount.Text);
                glCode = Convert.ToString(lblGlcode.Text);


                endMarket = Convert.ToString(txtEndMarket.Text);
                countryID = Convert.ToInt32(ddlCountry.SelectedValue);
                postingMonth = Convert.ToInt32(ddlPostingMonth.SelectedValue);
                postingYear = Convert.ToInt32(ddlPostingYear.SelectedValue);
                if (!string.IsNullOrEmpty(txtPostingValue.Text))
                    postingValue = Convert.ToDouble(txtPostingValue.Text);
                else
                    postingValue = 0;

                postingCurrency = Convert.ToInt32(ddlPostingCurrency.SelectedValue);

                if (!string.IsNullOrEmpty(txtUnPostedValue.Text))
                    unpostedValue = Convert.ToDouble(txtUnPostedValue.Text);
                else
                    unpostedValue = 0;

                udf1 = Convert.ToString(txtUDF1.Text);
                udf2 = Convert.ToString(txtUDF2.Text);
                udf3 = Convert.ToString(txtUDF3.Text);
                udf4 = Convert.ToString(txtUDF4.Text);
                udf5 = Convert.ToString(txtUDF5.Text);


                valueQueryTxt += "('" + month + "','" + jobNo + "','" + custoerCode + "','" + customerName + "','" + billDate + "','" + classTxt + "','" + currencyDesc + "','" + rate + "','" + fCurrency + "','" + amountInr + "','" + billNo + "','" + location + "','" + businessSegment + "','" + class1Txt + "','" + type + "','" + doc + "'," + warranty + ",'" + percentage + "','" + percentageAmount + "','" + glCode + "','" + endMarket + "'," + countryID + "," + postingMonth + "," + postingYear + "," + postingValue + "," + postingCurrency + "," + unpostedValue + ",'" + udf1 + "','" + udf2 + "','" + udf3 + "','" + udf4 + "','" + udf5 + "'," + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "," + "GETDATE()),";
            }
            if (!string.IsNullOrEmpty(valueQueryTxt))
            {
                valueQueryTxt = valueQueryTxt.TrimEnd(',');
                string insertQueryTxt = "insert into tblPostedSaleBills([MONTH],[OA_NO],[CUSTOMER_CODE],[CUSTOMER_NAME],[BILL_DATE],[CLASS],[CURR_DESC],[RATE],[F_CURRENCY],[AMOUNT_INR],[BILL_NO],[LOCATION],[BUS_SEGMENT],[CLASS1],[TYPE],[DOC],[WARRANTY],[PERCENTAGE],[PERCENTAGE_AMOUNT],[GLCODE],[END_MARKET],[COUNTRY_ID],[POSTING_MONTH],[POSTING_YEAR],[POSTING_VALUE],[POSTING_CURRENCY_ID],[UNPOSTED_VALUE],[UDF1],[UDF2],[UDF3],[UDF4],[UDF5],[CREATED_BY],[CREATED_ON])values" + valueQueryTxt;
                int value = objPosting.InesrtPosting(insertQueryTxt);
                if (value > 0)
                {
                    SuccessMessage("Posting successfully done.");
                    return;
                }
            }
            else
            {
                ExceptionMessage("No data found for posting!");
                return;
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

    #endregion

}
