using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class REPORTS_PURCHASE_ORDER_POFinalDeliverDateWiseForecastReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();

    string _jobNo = string.Empty;
    string _finalDeliverDate = string.Empty;
    DataSet dsMainReport = new DataSet();

    public string JobNo
    {
        get
        {
            return _jobNo;
        }

        set
        {
            _jobNo = value;
        }
    }

    public string FinalDeliverDate
    {
        get
        {
            return _finalDeliverDate;
        }

        set
        {
            _finalDeliverDate = value;
        }
    }

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

                Session["PO_REPORT"] = null;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
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
            DataSet ds = (DataSet)Session["PO_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    #endregion


    #region METHODS[=========================]


    private void GetSalesorderOSReportOne()
    {
        try
        {
            int month = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("MM"));
            int year = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy"));

            string m = "";
            if (month < 10) m = "0" + month;
            else m = month.ToString();

            FinalDeliverDate = year + "-" + m + "-01";

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                JobNo = txtJobNo.Text.ToUpper();

            dsMainReport = objReports.GetPurcaseOrderForcastReport(FinalDeliverDate, JobNo);

            if (dsMainReport.Tables.Count > 0)
            {
                if (dsMainReport.Tables[0].Rows.Count > 0)
                {
                    Session["PO_REPORT"] = dsMainReport;
                    gvSaleOrder.DataSource = dsMainReport.Tables[0];
                    gvSaleOrder.DataBind();
                }
                else
                {
                    Session["PO_REPORT"] = null;
                    gvSaleOrder.DataSource = null;
                    gvSaleOrder.DataBind();

                    ExceptionMessage("No data found...!");
                    return;
                }
            }
            else
            {
                Session["PO_REPORT"] = null;
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

            string fileName = "POForeCastReportByFDDate_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
        Session["PO_REPORT"] = null;
        gvSaleOrder.DataSource = null;
        gvSaleOrder.DataBind();
        lblRecords.Text = "Records[" + gvSaleOrder.Rows.Count + "]";
    }

    #endregion

}