using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class REPORTS_PURCHASE_ORDER_POReportJOBWise : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Reports objReports = new BAL.Reports();
    DataSet dsReport = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string jobNo = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["PO_REPORT"] = null;
                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToString(hdStartDateSearch.Value);

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToString(hdEndDateSearch.Value);

                GetPOReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetPOReport();
    }

    protected void gvPOReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPOReport.PageIndex = e.NewPageIndex;
        GetPOReport();
    }

    protected void gvPOReport_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (gvPOReport.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["PO_REPORT"];
            ToCSVNew01(dt);
        }
    }

    #endregion


    #region METHODS[=======================]

    private void GetPOReport()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            //DOC_CLASS LIKE '%OS18001%' OR
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                string[] jobNoTxt = txtJobNo.Text.Replace(Environment.NewLine, "").TrimEnd(',').Split(',');
                foreach (string item in jobNoTxt)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        jobNo += "DOC_CLASS LIKE '%" + item + "%' OR ";
                    }
                }
                jobNo = jobNo.TrimEnd(' ');
                jobNo = jobNo.TrimEnd('R');
                jobNo = jobNo.TrimEnd('O');
            }
            else
                jobNo = string.Empty;

            dsReport = objReports.GetPOReportJOBWise(startDate, endDate, jobNo);
            if (dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
            {
                Session["PO_REPORT"] = dsReport.Tables[0];
                gvPOReport.DataSource = dsReport.Tables[0];
                gvPOReport.DataBind();
            }
            else
            {
                Session["PO_REPORT"] = null;
                gvPOReport.DataSource = null;
                gvPOReport.DataBind();
            }

            lblRecords.Text = "Records[" + dsReport.Tables[0].Rows.Count + "]";
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

            string fileName = "PO_Report_JOBwise" + DateTime.Now.ToString("dd_MMM_yyyy");
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
