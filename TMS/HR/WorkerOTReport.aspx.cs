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

public partial class HR_WorkerOTReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Hrdept objHrdept = new BAL.Hrdept();
    DataSet dsWorker = new DataSet();
    DataSet dsOTReport = new DataSet();
    DataSet dsOTDetails = new DataSet();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string empCode = string.Empty;
    int empID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["OT_REPORT"] = null;
                Session["OT_REPORT_DETAIL"] = null;

                hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = hdStartDate.Value;

                hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = hdEndDate.Value;

                BindWorker();
                GetOtReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        GetOtReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        HidePanel();
        if (gvOTReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["OT_REPORT"];
            ExportOTReport(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    protected void btnExportDetails_Click(object sender, EventArgs e)
    {       
        if (gvOTReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["OT_REPORT_DETAIL"];
            ExportOTReportDetail(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }
    

    protected void gvOTReport_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    Label lblEmployeeID = gvOTReport.Rows[rowindex].FindControl("lblEmployeeID") as Label;

                    Label lblEmployeeName = gvOTReport.Rows[rowindex].FindControl("lblEmployeeName") as Label;
                    Label lblEmployeeCode = gvOTReport.Rows[rowindex].FindControl("lblEmployeeCode") as Label;

                    if (!string.IsNullOrEmpty(Convert.ToString(hdStartDate.Value)))
                        startDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
                    else
                        startDate = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(hdEndDate.Value)))
                        endDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
                    else
                        endDate = string.Empty;


                    txtFromDateDetails.Text = Convert.ToString(hdStartDate.Value);
                    txtToDateDetails.Text = Convert.ToString(hdEndDate.Value);

                    lblLegendDetails.Text = lblEmployeeName.Text + " [" + lblEmployeeCode.Text + "]";

                    this.ModalPopupExtender1.Show();
                    BindWorkerOTDetails(startDate, endDate, Convert.ToInt32(lblEmployeeID.Text));
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

    protected void gvOTReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNetOTPayble = (Label)e.Row.FindControl("lblNetOTPayble");
                Label lblIsDisputed = (Label)e.Row.FindControl("lblIsDisputed");
                CheckBox chkIsDisputed = (CheckBox)e.Row.FindControl("chkIsDisputed");

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                if (Convert.ToDouble(lblNetOTPayble.Text) == 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }

                if (Convert.ToDouble(lblIsDisputed.Text) > 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                    chkIsDisputed.Checked = true;
                }
                else
                {
                    chkIsDisputed.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvOTDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblIsDisputed = (Label)e.Row.FindControl("lblIsDisputed");

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblIsDisputed.Text)))
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
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

    private void BindWorker()
    {
        try
        {
            dsWorker = objHrdept.GetEmployeesByCategory(1);
            if (dsWorker.Tables.Count > 0 && dsWorker.Tables[0].Rows.Count > 0)
            {
                ddlWorker.DataSource = dsWorker.Tables[0];
                ddlWorker.DataTextField = "EMP_NAME";
                ddlWorker.DataValueField = "EMP_ID";
                ddlWorker.DataBind();
                ddlWorker.Items.Insert(0, "Select");
                ddlWorker.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetOtReport()
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

            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
                empCode = txtEmployeeCode.Text.Trim();
            else
                empCode = string.Empty;


            if (ddlWorker.SelectedIndex > 0)
                empID = Convert.ToInt32(ddlWorker.SelectedValue);
            else
                empID = 0;


            dsOTReport = objHrdept.GetWorkerOTReport(startDate, endDate, empID, empCode);
            if (dsOTReport.Tables.Count > 0 && dsOTReport.Tables[0].Rows.Count > 0)
            {
                Session["OT_REPORT"] = dsOTReport;
                gvOTReport.DataSource = dsOTReport.Tables[0];
                gvOTReport.DataBind();
            }
            else
            {
                Session["OT_REPORT"] = null;
                gvOTReport.DataSource = null;
                gvOTReport.DataBind();
            }
            lblRecords.Text = "Records[" + dsOTReport.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindWorkerOTDetails(string startDate, string endDate, int employeeID)
    {
        try
        {
            dsOTDetails = objHrdept.GetWorkerOTDetails(startDate, endDate, employeeID, "");
            if (dsOTDetails.Tables.Count > 0 && dsOTDetails.Tables[0].Rows.Count > 0)
            {
                Session["OT_REPORT_DETAIL"] = dsOTDetails;
                gvOTDetails.DataSource = dsOTDetails.Tables[0];
                gvOTDetails.DataBind();
            }
            else
            {
                Session["OT_REPORT_DETAIL"] = null;
                gvOTDetails.DataSource = null;
                gvOTDetails.DataBind();
            }
            lblOTDetailsRecords.Text = "Records[" + dsOTDetails.Tables[0].Rows.Count + "]";
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

            string fileName = "OT_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

            string fileName = "OT_Report_Detail_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
