using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TOUR_AND_TRAVELS_TRAVEL_ServiceDeptTravelSummaryReport : System.Web.UI.Page
{
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
   // BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsEmployee = new DataSet();
    DataSet dsDetailList = new DataSet();
    DataSet dsTravelList = new DataSet();
    
    string startDate = string.Empty;
    string endDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDate.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDate.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDate.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDate.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

            }
        }


    }

   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetServiceDeptTravelStatementList();
    }

    private void GetServiceDeptTravelStatementList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDate.Value)))
                startDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDate.Value)))
                endDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;


            dsTravelList = objTourAndTravels.GetServiceTravelSummaryReport(startDate, endDate);
            if (dsTravelList.Tables.Count > 0 && dsTravelList.Tables[0].Rows.Count > 0)
            {
                Session["dsTravelSummaryServcieDept"] = dsTravelList.Tables[0];
                gvServiceDeptTravelList.DataSource = dsTravelList.Tables[0];
                gvServiceDeptTravelList.DataBind();
            }
            else
            {
                Session["dsTravelSummaryServcieDept"] = null;
                gvServiceDeptTravelList.DataSource = null;
                gvServiceDeptTravelList.DataBind();
            }
            lblRecords.Text = "Records[" + dsTravelList.Tables[0].Rows.Count + "]";
            
        }

        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvServiceDeptTravelList.Rows.Count > 0)
            {
                DataTable dt = Session["dsTravelSummaryServcieDept"] as DataTable;
                if (dt != null)
                {
                    ToCSVNew01(dt);
                }
            }
            else
            {
                SuccessMessage("No data found!");
            }
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

            string fileName = "TravelStatementReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

}