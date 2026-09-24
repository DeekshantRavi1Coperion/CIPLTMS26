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

public partial class HR_WorkerOTReportInDetail : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Hrdept objHrdept = new BAL.Hrdept();
    DataSet dsEmployee = new DataSet();
    DataSet dsOTReport = new DataSet();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string empCode = string.Empty;
    int empID = 0;

    string oldEmpCode = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["OT_REPORT"] = null;

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
        HidePanel();
        GetOtReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        HidePanel();
        if (gvOTReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["OT_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }
   
    protected void gvOTReport_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvOTDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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

    private void BindWorker()
    {
        try
        {
            dsEmployee = objHrdept.GetEmployeesByCategory(1);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMP_NAME";
                ddlEmployee.DataValueField = "EMP_ID";
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

    private void GetOtReport()
    {
        try
        {
            bool chkMached = false;
            int count = 0;           
            int rowIndex = 0;
            string rowIndexTxt = string.Empty;

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


            if (ddlEmployee.SelectedIndex > 0)
                empID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empID = 0;


            dsOTReport = objHrdept.GetWorkerOTDetails(startDate, endDate, empID, empCode);
            if (dsOTReport.Tables.Count > 0 && dsOTReport.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow drot in dsOTReport.Tables[0].Rows)
                {
                    string newEmpCode = Convert.ToString(drot["EMPLOYEE_CODE"]);
                    if (oldEmpCode == string.Empty)
                    {
                        oldEmpCode = newEmpCode;
                    }
                    else
                    {
                        if (oldEmpCode != newEmpCode)
                        {
                            count++;
                            if (count > 1)
                                rowIndex = dsOTReport.Tables[0].Rows.IndexOf(drot) + (count * 2) - 2;
                            else
                                rowIndex = dsOTReport.Tables[0].Rows.IndexOf(drot);

                            rowIndexTxt += rowIndex + ",";
                            chkMached = false;
                            oldEmpCode = newEmpCode;
                        }
                        else
                        {
                            rowIndex++;
                            chkMached = true;
                        }
                    }
                }

                if (Convert.ToString(rowIndexTxt) != string.Empty)
                {
                    rowIndexTxt = rowIndexTxt.TrimEnd(',');
                    string[] strCountTxt = rowIndexTxt.Split(',');
                    foreach (string item in strCountTxt)
                    {
                        DataRow dr1 = dsOTReport.Tables[0].NewRow();
                        DataRow dr2 = dsOTReport.Tables[0].NewRow();
                        dsOTReport.Tables[0].Rows.InsertAt(dr1, Convert.ToInt32(item));
                        dsOTReport.Tables[0].Rows.InsertAt(dr2, Convert.ToInt32(item) + 1);
                    }
                }

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
   
    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    csv += Convert.ToString(dt.Rows[i][j]).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "OT_Report_In_Detail" + DateTime.Now.ToString("dd_MMM_yyyy");
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
