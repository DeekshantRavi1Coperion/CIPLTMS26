using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_MonthlySiteReport : System.Web.UI.Page
{

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string customerName = string.Empty;
    string jobNumber = string.Empty;
    int unitID = 0;
    int departmentID = 0;
    int empID = 0;
    int createdByID = 0;
    int equipmentID = 0;

    DataSet dsEmployee = new DataSet();
    DataSet dsLessonList = new DataSet();
    DataSet dsDepartment = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsEquipments = new DataSet();
    DataSet dsCreatedBy = new DataSet();
    decimal totalHours = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {

            if (!IsPostBack)
            {
                //hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtStartDate.Text = hdStartDate.Value.ToString();
                //hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtEndDate.Text = hdEndDate.Value.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        lblMsg.Visible = false;
        GetReport();
        //Reset();
    }

    private void GetReport()
    {
        try
        {
            int selectedYear = 0;
            bool isMonthEnabled = false;
            int selectedMonth = 0;
            totalHours = 0;
            if (ddlYear.SelectedIndex >= 0)
            selectedYear = Convert.ToInt32(ddlYear.SelectedValue);
            isMonthEnabled = chkEnableMonth.Checked;

            
            if (ddlMonth.SelectedIndex >= 0)
                selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);

            DataSet dsReport = objTourAndTravels.GetMonthlySiteReport(selectedYear, isMonthEnabled, selectedMonth);

            if (dsReport != null && dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
            {
                Session["dsReport"] = dsReport;
                totalHours = 0;
                gvLessonLearntList.DataSource = dsReport.Tables[0];
                gvLessonLearntList.DataBind();
                lblRecords.Text = "Records[" + dsReport.Tables[0].Rows.Count + "]";
            }
            else
            {
                Session["dsReport"] = null;
                gvLessonLearntList.DataSource = null;
                gvLessonLearntList.DataBind();
                lblRecords.Text = "Records[0]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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

            string fileName = "LessonLearntReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    if (gvLessonLearntList.Rows.Count > 0)
    //    {
    //        DataSet ds = (DataSet)Session["dsLessonList"];
    //        ToCSVNew01(ds.Tables[0]);
    //    }
    //    else
    //    {
    //        SuccessMessage("No data found!");
    //    }
    //}

    protected void gvLessonLearntList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLessonLearntList.PageIndex = e.NewPageIndex;
        //GetLessonList();
    }

    protected void gvLessonLearntList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal rowTotal = 0;
               
                if (decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "Total").ToString(), out rowTotal))
                {
                    totalHours += rowTotal;
                }
            }
           
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Grand Total:";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[1].Text = totalHours.ToString("0.##");
                e.Row.Cells[1].Font.Bold = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void chkEnableMonth_CheckedChanged(object sender, EventArgs e)
    {
        ddlMonth.Enabled = chkEnableMonth.Checked;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvLessonLearntList.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            string uniqueFileName = "MonthlySiteReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + uniqueFileName);

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvLessonLearntList.AllowPaging = false;
                GetReport(); 
                gvLessonLearntList.GridLines = GridLines.Both; 
                gvLessonLearntList.HeaderStyle.Reset();
                gvLessonLearntList.RowStyle.Reset();
                gvLessonLearntList.AlternatingRowStyle.Reset();
                gvLessonLearntList.FooterStyle.Reset();
                gvLessonLearntList.Style.Clear();

                if (gvLessonLearntList.HeaderRow != null)
                {
                    gvLessonLearntList.HeaderRow.Attributes.Clear();
                    foreach (TableCell cell in gvLessonLearntList.HeaderRow.Cells)
                    {
                        cell.Attributes.Clear();
                        cell.Style.Clear();
                        cell.Font.Bold = true;
                    }
                }

                foreach (GridViewRow row in gvLessonLearntList.Rows)
                {
                    row.Attributes.Clear();
                    row.Style.Clear();
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.Attributes.Clear();
                        cell.Style.Clear();
                    }
                }

                if (gvLessonLearntList.FooterRow != null)
                {
                    gvLessonLearntList.FooterRow.Attributes.Clear();
                    foreach (TableCell cell in gvLessonLearntList.FooterRow.Cells)
                    {
                        cell.Attributes.Clear();
                        cell.Style.Clear();
                        cell.Font.Bold = true; // Keep footer bold
                    }
                }

                gvLessonLearntList.RenderControl(hw);
                string excelHeader = @"
            <html xmlns:x=""urn:schemas-microsoft-com:office:excel"">
            <head>
                <!--[if gte mso 9]>
                <xml>
                    <x:ExcelWorkbook>
                        <x:ExcelWorksheets>
                            <x:ExcelWorksheet>
                                <x:Name>Report</x:Name>
                                <x:WorksheetOptions>
                                    <x:DisplayGridlines/>
                                </x:WorksheetOptions>
                            </x:ExcelWorksheet>
                        </x:ExcelWorksheets>
                    </x:ExcelWorkbook>
                </xml>
                <![endif]-->
            </head>
            <body>";

                string excelFooter = "</body></html>";

                // Output to response
                Response.Write(excelHeader);
                Response.Write(sw.ToString());
                Response.Write(excelFooter);

                Response.Flush();
                Response.End();
                gvLessonLearntList.AllowPaging = true;
            }
        }
        else
        {
            ExceptionMessage("No records found to export.");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
     
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
}