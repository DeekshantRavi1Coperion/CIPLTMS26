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

public partial class USGAAP_BILLING_UnpostedList : System.Web.UI.Page
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
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");

                Label lblEndMarketID = (Label)e.Row.FindControl("lblEndMarketID");
                Label lblCountryID = (Label)e.Row.FindControl("lblCountryID");
                Label lblPostingYear = (Label)e.Row.FindControl("lblPostingYear");
                Label lblPostingCurrencyID = (Label)e.Row.FindControl("lblPostingCurrencyID");

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

                    if (Convert.ToInt32(lblRecordID.Text) > 0)
                    {
                        ddlEndMarket.SelectedValue = Convert.ToString(lblEndMarketID.Text);
                        ddlEndMarket.Enabled = false;
                    }
                }

                if (dtCountry.Tables.Count > 0 && dtCountry.Tables[0].Rows.Count > 0)
                {
                    ddlCountry.DataSource = dtCountry.Tables[0];
                    ddlCountry.DataTextField = "COUNTRY_NAME";
                    ddlCountry.DataValueField = "COUNTRY_ID";
                    ddlCountry.DataBind();
                    ddlCountry.SelectedValue = "95";

                    if (Convert.ToInt32(lblRecordID.Text) > 0)
                    {
                        ddlCountry.SelectedValue = Convert.ToString(lblCountryID.Text);
                        ddlCountry.Enabled = false;
                    }
                }

                if (dtCurrency.Tables.Count > 0 && dtCurrency.Tables[0].Rows.Count > 0)
                {
                    ddlPostingCurrency.DataSource = dtCurrency.Tables[0];
                    ddlPostingCurrency.DataTextField = "CURRENCY_CODE";
                    ddlPostingCurrency.DataValueField = "CURRENCY_ID";
                    ddlPostingCurrency.DataBind();
                    ddlPostingCurrency.SelectedValue = "68";

                    if (Convert.ToInt32(lblRecordID.Text) > 0)
                    {
                        ddlPostingCurrency.SelectedValue = Convert.ToString(lblPostingCurrencyID.Text);
                        ddlPostingCurrency.Enabled = false;
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
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        InesrtPosting();

        //double unPostedValue = 0;
        //int count = 0;
        //if (gvPostingList.Rows.Count > 0)
        //{
        //    foreach (GridViewRow gr in gvPostingList.Rows)
        //    {
        //        TextBox txtUnPostedValue = (TextBox)gr.FindControl("txtUnPostedValue");
        //        if (!string.IsNullOrEmpty(txtUnPostedValue.Text))
        //            unPostedValue = Convert.ToDouble(txtUnPostedValue.Text);

        //        if (unPostedValue < 0)
        //        {
        //            txtUnPostedValue.ForeColor = System.Drawing.Color.Red;
        //            count++;
        //        }
        //    }
        //}

        //if (count == 0)
        //{

        //}
        //else
        //{

        //    ExceptionMessage("Unposted value must be greater than or equal to zero!");
        //    return;
        //}
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
            int recordID = 0;
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

            double postedValue = 0;

            string billNo = string.Empty;
            string location = string.Empty;
            string businessSegment = string.Empty;
            string businessSegmentDesc = string.Empty;
            string glCode = string.Empty;
            string glCodeDesc = string.Empty;
            int glCodeType = 0;
            int endMarketID = 0;
            int countryID = 0;
            string postingMonth = string.Empty;
            int postingYear = 0;
            double postingValue = 0;
            int postingCurrency = 0;
            double unpostedValue = 0;
            string status = string.Empty;
            string revRecReduction = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;

            //string valueQueryTxt = string.Empty;
            int value = 0;
            foreach (GridViewRow gr in gvPostingList.Rows)
            {
                Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                Label lblMonth = (Label)gr.FindControl("lblMonth");
                Label lblCustCode = (Label)gr.FindControl("lblCustCode");
                Label lblCustomerName = (Label)gr.FindControl("lblCustomerName");
                Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                Label lblBillNo = (Label)gr.FindControl("lblBillNo");
                Label lblBillDate = (Label)gr.FindControl("lblBillDate");
                Label lblClass = (Label)gr.FindControl("lblClass");
                Label lblCurrDesc = (Label)gr.FindControl("lblCurrDesc");
                Label lblRate = (Label)gr.FindControl("lblRate");
                Label lblFcurrency = (Label)gr.FindControl("lblFcurrency");
                Label lblLocation = (Label)gr.FindControl("lblLocation");
                Label lblBusinessSegment = (Label)gr.FindControl("lblBusinessSegment");
                Label lblBusinessSegmentDesc = (Label)gr.FindControl("lblBusinessSegmentDesc");
                Label lblGlcode = (Label)gr.FindControl("lblGlcode");
                Label lblGlcodeDesc = (Label)gr.FindControl("lblGlcodeDesc");
                Label lblGlcodeType = (Label)gr.FindControl("lblGlcodeType");
                Label lblAmountINR = (Label)gr.FindControl("lblAmountINR");

                Label lblPostedValue = (Label)gr.FindControl("lblPostedValue");
                Label lblPendingUnpostedValue = (Label)gr.FindControl("lblPendingUnpostedValue");

                DropDownList ddlEndMarket = (DropDownList)gr.FindControl("ddlEndMarket");
                DropDownList ddlCountry = (DropDownList)gr.FindControl("ddlCountry");
                DropDownList ddlPostingMonth = (DropDownList)gr.FindControl("ddlPostingMonth");
                DropDownList ddlPostingYear = (DropDownList)gr.FindControl("ddlPostingYear");
                TextBox txtPostingValue = (TextBox)gr.FindControl("txtPostingValue");
                DropDownList ddlPostingCurrency = (DropDownList)gr.FindControl("ddlPostingCurrency");
                TextBox txtUnPostedValue = (TextBox)gr.FindControl("txtUnPostedValue");
                TextBox txtRevRecReduction = (TextBox)gr.FindControl("txtRevRecReduction");
                TextBox txtUDF2 = (TextBox)gr.FindControl("txtUDF2");
                TextBox txtUDF3 = (TextBox)gr.FindControl("txtUDF3");
                TextBox txtUDF4 = (TextBox)gr.FindControl("txtUDF4");
                TextBox txtUDF5 = (TextBox)gr.FindControl("txtUDF5");


                recordID = Convert.ToInt32(lblRecordID.Text);
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
                businessSegmentDesc = Convert.ToString(lblBusinessSegmentDesc.Text);
                glCode = Convert.ToString(lblGlcode.Text);
                glCodeDesc = Convert.ToString(lblGlcodeDesc.Text);

                if (Convert.ToString(lblGlcodeType.Text) == "B")
                    glCodeType = 1;
                else if (Convert.ToString(lblGlcodeType.Text) == "P")
                    glCodeType = 0;

                endMarketID = Convert.ToInt32(ddlEndMarket.SelectedValue);
                countryID = Convert.ToInt32(ddlCountry.SelectedValue);
                postingMonth = Convert.ToString(ddlPostingMonth.SelectedItem.Text);
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

                if (!string.IsNullOrEmpty(lblPostedValue.Text))
                    postedValue = Convert.ToDouble(lblPostedValue.Text);
                else
                    postedValue = 0;


                if (amountInr == (postingValue + postedValue))
                    status = "P";
                else
                    status = "U";

                revRecReduction = Convert.ToString(txtRevRecReduction.Text);
                udf2 = Convert.ToString(txtUDF2.Text);
                udf3 = Convert.ToString(txtUDF3.Text);
                udf4 = Convert.ToString(txtUDF4.Text);
                udf5 = Convert.ToString(txtUDF5.Text);



                //valueQueryTxt += "('" + month + "','" + jobNo + "','" + custoerCode + "','" + customerName + "','" + billDate + "','" + classTxt + "','" + currencyDesc + "','" + rate + "','" + fCurrency + "','" + amountInr + "','" + billNo + "','" + location + "','" + businessSegment + "','" + businessSegmentDesc + "','" + glCode + "','" + glCodeDesc + "'," + glCodeType + "," + endMarketID + "," + countryID + ",'" + postingMonth + "'," + postingYear + "," + postingValue + "," + postingCurrency + "," + unpostedValue + ",'" + status + "','" + revRecReduction + "','" + udf2 + "','" + udf3 + "','" + udf4 + "','" + udf5 + "'," + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "," + "GETDATE()),";
                //Dated 09-05-2018////

                if (postingValue > 0 && unpostedValue >= 0)
                {
                    value = objPosting.InesrtPostingNew(recordID, month, jobNo, custoerCode, customerName, billDate, classTxt, currencyDesc, rate, fCurrency, amountInr, billNo, location, businessSegment, businessSegmentDesc, glCode, glCodeDesc, glCodeType, endMarketID, countryID, postingMonth, postingYear, postingValue, postingCurrency, unpostedValue, status, revRecReduction, udf2, udf3, udf4, udf5, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                }

                //////////////////////
            }

            if (value > 0)
            {
                SuccessMessage("Posting successfully done.");
                GetPostingList();
            }


            //if (!string.IsNullOrEmpty(valueQueryTxt))
            //{
            //    valueQueryTxt = valueQueryTxt.TrimEnd(',');
            //    string insertQueryTxt = "insert into tblPostedSaleBills([MONTH],[OA_NO],[CUSTOMER_CODE],[CUSTOMER_NAME],[BILL_DATE],[CLASS],[CURR_DESC],[RATE],[F_CURRENCY],[AMOUNT_INR],[BILL_NO],[LOCATION],[BUS_SEGMENT],[BUS_SEGMENT_DESC],[GLCODE],[GLCODE_DESC],[GLCODE_TYPE],[END_MARKET],[COUNTRY_ID],[POSTING_MONTH],[POSTING_YEAR],[POSTING_VALUE],[POSTING_CURRENCY_ID],[UNPOSTED_VALUE],[STATUS],[UDF1],[UDF2],[UDF3],[UDF4],[UDF5],[CREATED_BY],[CREATED_ON])values" + valueQueryTxt;
            //    int value = objPosting.InesrtPosting(insertQueryTxt);
            //    if (value > 0)
            //    {
            //        SuccessMessage("Posting successfully done.");
            //        GetPostingList();
            //        return;
            //    }
            //}
            //else
            //{
            //    ExceptionMessage("No data found for posting!");
            //    return;
            //}
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
