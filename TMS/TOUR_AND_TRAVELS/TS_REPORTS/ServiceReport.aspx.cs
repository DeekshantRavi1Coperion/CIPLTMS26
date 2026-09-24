using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net.Mime;

public partial class TOUR_AND_TRAVELS_TS_REPORTS_ServiceReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();

    DataSet dsEmployee = new DataSet();
    DataSet dsService = new DataSet();
    DataSet dsServiceDetail = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    int type = 0;
    int empRecordID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["SERVICE_REPORT"] = null;
                Session["SERVICE_REPORT_DETAIL"] = null;

                BindEmployee();
                GetServiceReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetServiceReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvServiceReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["SERVICE_REPORT"];
            ExportOTReport(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    protected void btnExportDetails_Click(object sender, EventArgs e)
    {
        if (gvServiceReportDetails.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["SERVICE_REPORT_DETAIL"];
            ExportOTReportDetail(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    protected void gvServiceReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblTourStatus = (Label)e.Row.FindControl("lblTourStatus");

                //if (Convert.ToString(lblTourStatus.Text) == "New")
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                //    }
                //}

                //else if (Convert.ToString(lblTourStatus.Text) == "Deleted")
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.Salmon;
                //    }
                //}

                //else if (Convert.ToString(lblTourStatus.Text) == "Approved")
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                //    }
                //}

                //else if (Convert.ToString(lblTourStatus.Text) == "Cancelled")
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                //    }
                //}

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

    protected void gvServiceReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "VIEW")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;

                    Label lblEmpRecordID = gvServiceReport.Rows[rowindex].FindControl("lblEmpRecordID") as Label;
                    Label lblEmployeeName = gvServiceReport.Rows[rowindex].FindControl("lblEmployeeName") as Label;
                    Label lblBU = gvServiceReport.Rows[rowindex].FindControl("lblBU") as Label;

                    if (!string.IsNullOrEmpty(Convert.ToString(hdStartDate.Value)))
                        startDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
                    else
                        startDate = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(hdEndDate.Value)))
                        endDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
                    else
                        endDate = string.Empty;


                    txtEmployeeName.Text = lblEmployeeName.Text;
                    txtFromDateDetails.Text = Convert.ToString(hdStartDate.Value);
                    txtToDateDetails.Text = Convert.ToString(hdEndDate.Value);
                    txtBU.Text = lblBU.Text;

                    lblLegendDetails.Text = "Service Report In Detail";

                    this.ModalPopupExtender1.Show();
                    GetServiceReportDetails(startDate, endDate, Convert.ToInt32(lblEmpRecordID.Text), Convert.ToString(lblBU.Text));
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    protected void gvServiceReportDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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

    #endregion


    #region METHODS[=======================]

    private void BindEmployee()
    {
        try
        {
            dsEmployee = objTourAndTravels.GetEmployeeForTSServiceReport();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetServiceReport()
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

            if (ddlType.SelectedIndex > 0)
                type = Convert.ToInt32(ddlType.SelectedValue);
            else
                type = 0;


            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;

            dsService = objTourAndTravels.GetServiceReport(startDate, endDate, empRecordID, type);

            if (dsService.Tables.Count > 0 && dsService.Tables[0].Rows.Count > 0)
            {
                Session["SERVICE_REPORT"] = dsService;
                gvServiceReport.DataSource = dsService.Tables[0];
                gvServiceReport.DataBind();
            }
            else
            {
                Session["SERVICE_REPORT"] = null;
                gvServiceReport.DataSource = null;
                gvServiceReport.DataBind();
            }
            lblRecords.Text = "Records[" + dsService.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetServiceReportDetails(string startDate, string endDate, int empRecordID, string bu)
    {
        try
        {
            dsServiceDetail = objTourAndTravels.GetServiceReportDetailsOne(startDate, endDate, empRecordID, 0, bu);
            if (dsServiceDetail.Tables.Count > 0 && dsServiceDetail.Tables[0].Rows.Count > 0)
            {
                Session["SERVICE_REPORT_DETAIL"] = dsServiceDetail;
                gvServiceReportDetails.DataSource = dsServiceDetail.Tables[0];
                gvServiceReportDetails.DataBind();
            }
            else
            {
                Session["SERVICE_REPORT_DETAIL"] = null;
                gvServiceReportDetails.DataSource = null;
                gvServiceReportDetails.DataBind();
            }
            lblOTDetailsRecords.Text = "Records[" + dsServiceDetail.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportOTReport(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 1; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    csv += Convert.ToString(dt.Rows[i][j]).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "TS_Service_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void ExportOTReportDetail(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 1; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    csv += Convert.ToString(dt.Rows[i][j]).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "TS_Service_Report_Detail_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
