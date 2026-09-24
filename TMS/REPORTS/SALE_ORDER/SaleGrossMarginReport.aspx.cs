using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class REPORTS_SALE_ORDER_SaleGrossMarginReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsSaleGrossMargin = new DataSet();
    DataSet dsUnit = new DataSet();
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
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["SALE_BILL_REPORT"] = null;

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
        GetSaleGrossMarginReport();
    }

    protected void gvSaleMargin_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (gvSaleMargin.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["SALE_BILL_REPORT"];
            DataTable dt = ds.Tables[0];
            ToCSVNew01(dt);
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

    private void GetSaleGrossMarginReport()
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


            if (ddlCompany.SelectedIndex > 0)
            {
                unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
                dsSaleGrossMargin = objReports.GetSaleGrossMarginDetails(unitId, unitName, fromDate, toDate);
            }
            else
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
                //dsSaleGrossMargin = objReports.GetSaleGrossMarginDetailsAllUnit(fromDate, toDate, dbNameA35, unitNameA35, dbNameDLH, unitNameDLH, dbNameSEZ, unitNameSEZ, dbNameGNU, unitNameGNU);
                dsSaleGrossMargin = objReports.GetSaleGrossMarginDetailsAllUnit("", dbNameA35, unitNameA35, dbNameDLH, unitNameDLH, dbNameSEZ, unitNameSEZ, dbNameGNU, unitNameGNU);

            }

            if (dsSaleGrossMargin.Tables.Count > 0 && dsSaleGrossMargin.Tables[0].Rows.Count > 0)
            {
                Session["SALE_BILL_REPORT"] = dsSaleGrossMargin;
                gvSaleMargin.DataSource = dsSaleGrossMargin.Tables[0];
                gvSaleMargin.DataBind();
            }
            else
            {
                Session["SALE_BILL_REPORT"] = null;
                gvSaleMargin.DataSource = null;
                gvSaleMargin.DataBind();
            }
            lblRecords.Text = "Records[" + dsSaleGrossMargin.Tables[0].Rows.Count + "]";
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

            string fileName = "SaleBillReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
    #endregion

}
