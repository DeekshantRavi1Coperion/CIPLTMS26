using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class TOUR_AND_TRAVELS_TRAVEL_ServiceHoursReport : System.Web.UI.Page
    {
        decimal totalWorkingHours = 0;
        decimal totalTrainingHours = 0;
        decimal totalTravellingHours = 0;
        DataSet dsEmpDetail = new DataSet();
        BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();

    protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdowns();
                BindEmpDetail();
            }
        }

    private void BindEmpDetail()
    {
        try
        {
            dsEmpDetail = objTourAndTravels.GetServiceDeptEmployeeList();
            if (dsEmpDetail.Tables.Count > 0 && dsEmpDetail.Tables[0].Rows.Count > 0)
            {
                Session["EMP_DETAIL"] = dsEmpDetail.Tables[0];
                ddlEmployee.DataSource = dsEmpDetail.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
            }
            else
            {
                Session["EMP_DETAIL"] = null;
               
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDropdowns()
        {
            ddlYear.Items.Insert(0, new ListItem("Select", ""));
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear+5; i >= currentYear - 5; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlMonth.Items.Insert(0, new ListItem("Select", ""));
            for (int i = 1; i <= 12; i++)
            {
                DateTime monthDate = new DateTime(2000, i, 1);
                ddlMonth.Items.Add(new ListItem(monthDate.ToString("MMMM"), i.ToString()));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            totalWorkingHours = 0;
            totalTrainingHours = 0;
            totalTravellingHours = 0;

            string selectedEmp = ddlEmployee.SelectedValue;
            string selectedYear = ddlYear.SelectedValue;
            string selectedMonth = ddlMonth.SelectedValue;
            DataTable dt = objTourAndTravels.GetServiceHourReport(selectedEmp, selectedYear, selectedMonth);

            gvReport.DataSource = dt;
            gvReport.DataBind();
        }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            string uniqueFileName = "ServiceWorkingReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename=" + uniqueFileName);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                BindGrid();
                gvReport.GridLines = GridLines.Both; 
                gvReport.HeaderStyle.Reset();
                gvReport.RowStyle.Reset();
                gvReport.AlternatingRowStyle.Reset();
                gvReport.FooterStyle.Reset();
                gvReport.Style.Clear();
                if (gvReport.HeaderRow != null)
                {
                    gvReport.HeaderRow.Attributes.Clear();
                    foreach (TableCell cell in gvReport.HeaderRow.Cells)
                    {
                        cell.Attributes.Clear();
                        cell.Style.Clear();
                        cell.Font.Bold = true; // Keep headers bold
                    }
                }
                foreach (GridViewRow row in gvReport.Rows)
                {
                    row.Attributes.Clear();
                    row.Style.Clear();
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.Attributes.Clear();
                        cell.Style.Clear();
                    }
                }
                if (gvReport.FooterRow != null)
                {
                    gvReport.FooterRow.Attributes.Clear();
                    foreach (TableCell cell in gvReport.FooterRow.Cells)
                    {
                        cell.Attributes.Clear();
                        cell.Style.Clear();
                        cell.Font.Bold = true; // Keep footer bold
                    }
                }
                gvReport.RenderControl(hw);
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
                Response.End();
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

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal working = 0, training = 0, travelling = 0;

                decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "WorkingHours").ToString(), out working);
                decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "TrainingHours").ToString(), out training);
                decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "TravellingHours").ToString(), out travelling);

                totalWorkingHours += working;
                totalTrainingHours += training;
                totalTravellingHours += travelling;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "";

                e.Row.Cells[3].Text = "Total Hours " + totalWorkingHours.ToString("0.##");
                e.Row.Cells[4].Text = "Total Hours " + totalTrainingHours.ToString("0.##");
                e.Row.Cells[5].Text = "Total Hours " + totalTravellingHours.ToString("0.##");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            }
        }
        private void ExceptionMessage(string message)
        {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
        }


        // Dummy data for testing. Replace this function with your ADO.NET / SQL code.
        //private DataTable GetDummyData(string empId, string year, string month)
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("EmployeeName", typeof(string));
        //    dt.Columns.Add("Year", typeof(string));
        //    dt.Columns.Add("Month", typeof(string));
        //    dt.Columns.Add("WorkingHours", typeof(decimal));
        //    dt.Columns.Add("TrainingHours", typeof(decimal));
        //    dt.Columns.Add("TravellingHours", typeof(decimal));

        //    // Adding a few dummy rows so you can see the calculation working
        //    dt.Rows.Add("Rahul Kumar", "2024", "January", 40.5, 5, 12);
        //    dt.Rows.Add("Anjali Singh", "2024", "January", 35, 8.5, 10);
        //    dt.Rows.Add("Rahul Kumar", "2024", "February", 45, 0, 15.5);

        //    return dt;
        //}
    }
