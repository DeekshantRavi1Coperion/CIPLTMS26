using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class REPORTS_Project___Late_Order_Delivery_Date : System.Web.UI.Page
{
    #region VARIABLES[=======================]

    string month = string.Empty;
    DataSet LateOTDReport = new DataSet();
    BAL.Reports objReports = new BAL.Reports();

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
        }

  }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       GetOTDReport();
    }

     protected void btnExport_Click(object sender , EventArgs e)
    {
        if (gvOTDDetailReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["LATE_OTD_REPORT"];
            ToCSVNew01(ds.Tables[0]);
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

            string fileName = "OTD_REPORT_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    protected void gvOTDDetailReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    for (int i = 0; i < e.Row.Cells.Count; i++)
        //    {
        //        string columnName = e.Row.Cells[i].Text.Trim().ToUpper();

        //        if (columnName == "CUSTOMER")
        //        {
        //            e.Row.Cells[i].Width = Unit.Pixel(800);
        //        }
        //        else if (columnName == "TITLE")
        //        {
        //            e.Row.Cells[i].Width = Unit.Pixel(1500);
        //        }
        //        else if (columnName == "STATUS")
        //        {
        //            e.Row.Cells[i].Width = Unit.Pixel(150);
        //        }
        //    }
        //}

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add("width", "800px");
            e.Row.Cells[1].Style.Add("width", "1500px");
            e.Row.Cells[2].Style.Add("width", "150px");
        }


    }

    private void GetOTDReport()
    {
        try
        {
            month = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");
            LateOTDReport = objReports.GetLateOTDReport(month);

            if (LateOTDReport.Tables.Count > 0 && LateOTDReport.Tables[0].Rows.Count > 0)
            {
                Session["LATE_OTD_REPORT"] = LateOTDReport;
                gvOTDDetailReport.DataSource = LateOTDReport.Tables[0];
                gvOTDDetailReport.DataBind();
            }
            else
            {
                Session["LATE_OTD_REPORT"] = null;
                gvOTDDetailReport.DataSource = null;
                gvOTDDetailReport.DataBind();
            }
            lblRecords.Text = "Records[" + LateOTDReport.Tables[0].Rows.Count + "]";

        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }


}