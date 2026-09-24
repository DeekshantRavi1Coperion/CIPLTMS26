using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class REPORTS_SALE_ORDER_SaleOrderOutstandingReportThree : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsTempTableDetails = new DataSet();
    DataSet dsSaleOrder = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string jobNo = string.Empty;
    string customerName = string.Empty;
    string customerType = string.Empty;
    string bu = string.Empty;
    string currency = string.Empty;


    string fromDate = string.Empty;
    string toDate = string.Empty;
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

                Session["SALE_ORDER_REPORT"] = null;
                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                BindCurrency();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SALE_ORDER_REPORT"] = null;
        gvSaleOrder.DataSource = null;
        gvSaleOrder.DataBind();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SALE_ORDER_REPORT"] = null;
        gvSaleOrder.DataSource = null;
        gvSaleOrder.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        GetSalesorderOSReportOne();
    }

    protected void gvSaleOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }         
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvSaleOrder.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["SALE_ORDER_REPORT"];
            ToCSVNew01(ds.Tables[1]);
        }
    }

    #endregion


    #region METHODS[=========================]

    private void BindCurrency()
    {
        try
        {
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

            dsCurrency = objReports.GetFactCurrencyList(dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);

            if (dsCurrency.Tables.Count > 0 && dsCurrency.Tables[0].Rows.Count > 0)
            {
                ddlCurrency.DataSource = dsCurrency.Tables[0];
                ddlCurrency.DataTextField = "CODE";
                ddlCurrency.DataValueField = "CODE";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, "All");
                ddlCurrency.SelectedIndex = 0;
            }
            else
            {
                ddlCurrency.Items.Clear();
                ddlCurrency.Items.Insert(0, "All");
                ddlCurrency.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetSalesorderOSReportOne()
    {
        try
        {

            string month1 = string.Empty;
            string month2 = string.Empty;
            string month3 = string.Empty;
            string month4 = string.Empty;
            string month5 = string.Empty;
            string month6 = string.Empty;
            string month7 = string.Empty;
            string month8 = string.Empty;
            string month9 = string.Empty;
            string month10 = string.Empty;
            string month11 = string.Empty;
            string month12 = string.Empty;

            int month = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("MM"));
            int year = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy"));

            for (int i = 0; i < 12; i++)
            {
                if (i > 0)
                    month = month + 1;

                if (month > 12)
                {
                    month = 1;
                    year = year + 1;
                }

                if (i == 0)
                    month1 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 1)
                    month2 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 2)
                    month3 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 3)
                    month4 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 4)
                    month5 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 5)
                    month6 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 6)
                    month7 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 7)
                    month8 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 8)
                    month9 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 9)
                    month10 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 10)
                    month11 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 11)
                    month12 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
            }

            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text.ToUpper();
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            if (ddlCustomerType.SelectedIndex > 0)
                customerType = ddlCustomerType.SelectedItem.Text;
            else
                customerType = string.Empty;

            if (!string.IsNullOrEmpty(txtBU.Text))
                bu = txtBU.Text;
            else
                bu = string.Empty;

            if (ddlCurrency.SelectedIndex > 0)
                currency = Convert.ToString(ddlCurrency.SelectedValue);
            else
                currency = string.Empty;

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
            dsSaleOrder = objReports.GetSalesorderOutstandingDetailsAllUnitTwo(month1, month2, month3, month4, month5, month6,
                                                                                month7, month8, month9, month10, month11, month12,
                                                                                jobNo, customerName, customerType, bu, currency,
                                                                                dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);

            if (dsSaleOrder.Tables.Count > 0)
            {
                if (dsSaleOrder.Tables[0].Rows.Count > 0)
                {
                    if (dsSaleOrder.Tables[1].Rows.Count > 0)
                    {
                        dsSaleOrder.Tables[1].Columns.Remove("OM");
                        Session["SALE_ORDER_REPORT"] = dsSaleOrder;
                        gvSaleOrder.DataSource = dsSaleOrder.Tables[1];
                        gvSaleOrder.DataBind();
                    }
                    else
                    {
                        Session["SALE_ORDER_REPORT"] = null;
                        gvSaleOrder.DataSource = null;
                        gvSaleOrder.DataBind();
                    }
                }
                else
                {
                    Session["SALE_ORDER_REPORT"] = null;
                    gvSaleOrder.DataSource = null;
                    gvSaleOrder.DataBind();

                    ExceptionMessage("Please enter current FX rate of selected month..!");
                    return;
                }
            }
            else
            {
                Session["SALE_ORDER_REPORT"] = null;
                gvSaleOrder.DataSource = null;
                gvSaleOrder.DataBind();
            }


            lblRecords.Text = "Records[" + gvSaleOrder.Rows.Count + "]";
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

            string fileName = "SaleOrderOutstandingReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    private void EmptyGridview()
    {
        Session["SALE_ORDER_REPORT"] = null;
        gvSaleOrder.DataSource = null;
        gvSaleOrder.DataBind();
        lblRecords.Text = "Records[" + gvSaleOrder.Rows.Count + "]";
    }

    #endregion
  
}