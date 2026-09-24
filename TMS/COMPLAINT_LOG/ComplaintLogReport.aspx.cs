using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class COMPLAINT_LOG_ComplaintLogReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.ComplaintLog objComplaintLog = new BAL.ComplaintLog();

    DataSet dsComplaintLogList = new DataSet();
    DataSet dsStatus = new DataSet();
    DataSet dsEmployee = new DataSet();
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string customerName = string.Empty;
    string complaintLogNo = string.Empty;
    int statusID = 0;
    int empRecordID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dsComplaintLogList"] = null;
                BindStatus();
                BindEmployee();

                hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = hdStartDate.Value.ToString();

                hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = hdEndDate.Value.ToString();

                GetComplaintLogList();
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
        GetComplaintLogList();
    }


    protected void gvComplaintLogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");

                //new
                if (Convert.ToString(lblStatusName.Text) == "New")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
                //Dept-Assigned
                else if (Convert.ToString(lblStatusName.Text) == "Assigned-Dept")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                //Person-Assigned
                else if (Convert.ToString(lblStatusName.Text) == "Assigned-Person")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightSeaGreen;
                    }
                }
                //Resolved
                else if (Convert.ToString(lblStatusName.Text) == "Resolved")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
                //Approved
                else if (Convert.ToString(lblStatusName.Text) == "Approved")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.WhiteSmoke;
                    }
                }
                //Closed
                else if (Convert.ToString(lblStatusName.Text) == "Closed")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }



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

    protected void gvComplaintLogList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComplaintLogList.PageIndex = e.NewPageIndex;
        GetComplaintLogList();
    }



    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvComplaintLogList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsComplaintLogList"];
            ToCSVNew01(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindStatus()
    {
        try
        {
            dsStatus = objComplaintLog.GetComplaingLogStatus();
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "COMPLAINT_LOG_STATUS_NAME";
                ddlStatus.DataValueField = "COMPLAINT_LOG_STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "Select");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEmployee()
    {
        try
        {
            dsEmployee = objComplaintLog.GetEmployeeListForComplaintLog();
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

    private void GetComplaintLogList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDate.Value)))
                fromDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDate.Value)))
                toDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text.Trim();
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtComplaintLogNo.Text))
                complaintLogNo = txtComplaintLogNo.Text.Trim();
            else
                complaintLogNo = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusID = 0;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;



            dsComplaintLogList = objComplaintLog.GetComplaintLogReportNew(fromDate, toDate, complaintLogNo, customerName, statusID, empRecordID);
            if (dsComplaintLogList.Tables.Count > 0 && dsComplaintLogList.Tables[0].Rows.Count > 0)
            {
                Session["dsComplaintLogList"] = dsComplaintLogList;
                gvComplaintLogList.DataSource = dsComplaintLogList.Tables[0];
                gvComplaintLogList.DataBind();
                lblRecords.Text = "Records[" + dsComplaintLogList.Tables[0].Rows.Count + "]";
            }
            else
            {
                Session["dsComplaintLogList"] = null;
                gvComplaintLogList.DataSource = null;
                gvComplaintLogList.DataBind();
                lblRecords.Text = "Records[0]";
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

            string fileName = "Complaint_Log_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
