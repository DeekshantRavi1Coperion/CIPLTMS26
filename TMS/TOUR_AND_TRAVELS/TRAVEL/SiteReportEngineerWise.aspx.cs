using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TOUR_AND_TRAVELS_TRAVEL_SiteReportEngineerWise : System.Web.UI.Page
{
    private decimal _totalWorkHours = 0;
    private decimal _totalTrainingHours = 0;
    private decimal _totalHours = 0;

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsEmpDetail = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
            {
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }

            BindEmpDetail();
        }
    }

    private void BindEmpDetail()
    {
        try
        {
            ddlEmployee.Items.Clear();
            dsEmpDetail = objTourAndTravels.GetServiceDeptEmployeeList();
            if (dsEmpDetail != null && dsEmpDetail.Tables.Count > 0 && dsEmpDetail.Tables[0].Rows.Count > 0)
            {
                Session["EMP_DETAIL"] = dsEmpDetail.Tables[0];
                ddlEmployee.DataSource = dsEmpDetail.Tables[0];

                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
            }
            else
            {
                Session["EMP_DETAIL"] = null;
            }
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.Message);
        }
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void ResetTotals()
    {
        _totalWorkHours = 0;
        _totalTrainingHours = 0;
        _totalHours = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ResetTotals();
        SearchData();
    }

    private void SearchData()
    {
        try
        {
            pnlMsg.Visible = false;
            string selectedYear = ddlYear.SelectedValue;
            int selectedEngineerId = Convert.ToInt32(ddlEmployee.SelectedValue);

            DataTable dt = objTourAndTravels.GetSiteReportDataEngineerWise(selectedYear, selectedEngineerId);

            if (dt != null && dt.Rows.Count > 0)
            {
                lblRecords.Text = "Records[" + dt.Rows.Count.ToString() + "]";
                gvLessonLearntList.DataSource = dt;
                gvLessonLearntList.DataBind();
            }
            else
            {
                lblRecords.Text = "Records[0]";
                gvLessonLearntList.DataSource = null;
                gvLessonLearntList.DataBind();
                ExceptionMessage("No records found.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.Message);
        }
    }

    protected void gvLessonLearntList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            decimal workHrs = 0;
            decimal trainingHrs = 0;
            decimal totalHrs = 0;

            if (rowView["WorkHours"] != DBNull.Value && decimal.TryParse(rowView["WorkHours"].ToString(), out workHrs))
                _totalWorkHours += workHrs;

            if (rowView["TrainingHours"] != DBNull.Value && decimal.TryParse(rowView["TrainingHours"].ToString(), out trainingHrs))
                _totalTrainingHours += trainingHrs;

            if (rowView["TotalHours"] != DBNull.Value && decimal.TryParse(rowView["TotalHours"].ToString(), out totalHrs))
                _totalHours += totalHrs;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = _totalWorkHours.ToString("0.00");
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[2].Text = _totalTrainingHours.ToString("0.00");
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Text = _totalHours.ToString("0.00");
            e.Row.Cells[3].Font.Bold = true;
        }
    }

    protected void gvLessonLearntList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLessonLearntList.PageIndex = e.NewPageIndex;
        ResetTotals();
        SearchData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvLessonLearntList.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;

            string uniqueFileName = "SiteReportEngineerWise_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename=" + uniqueFileName);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvLessonLearntList.AllowPaging = false;
                ResetTotals();
                SearchData();

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
                        cell.Font.Bold = true;
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

                Response.Write(excelHeader);
                Response.Write(sw.ToString());
                Response.Write(excelFooter);
                Response.Flush();
                Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        else
        {
            ExceptionMessage("No records found.");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }
}