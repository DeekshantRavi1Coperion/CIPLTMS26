using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class TICKET_TicketReport : System.Web.UI.Page
{

    #region VARIABLES[===========]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Ticket objTicket = new BAL.Ticket();
    DataSet dsTicketList = new DataSet();
    DataSet dsEmployee = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string ticketNo = string.Empty;
    string ticketStatus = string.Empty;
    int empRecordID = 0;

    #endregion


    #region EVENTS[==============]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {

            if (!IsPostBack)
            {
                BindEmployee();

                ddlEmployee.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 9 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 13 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 14 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 115)
                {
                    ddlEmployee.Enabled = true;
                }
                else
                {
                    ddlEmployee.Enabled = false;
                }

                GetTicketList();
            }


        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetTicketList();
    }

    protected void gvTicketList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketList.PageIndex = e.NewPageIndex;
        GetTicketList();
    }

    protected void gvTicketList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTicketStatus = (Label)e.Row.FindControl("lblTicketStatus");

                if (Convert.ToString(lblTicketStatus.Text) == "New")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                    }
                }

                else if (Convert.ToString(lblTicketStatus.Text) == "Closed")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }

                else if (Convert.ToString(lblTicketStatus.Text) == "Cancelled")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }


                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{
                //    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                //}

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        //ExportToExcel();           
        if (gvTicketList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsTicketlist"];
            ToCSVNew01(ds.Tables[0]);
        }
    }



    #endregion


    #region METHODS[=============]

    private void BindEmployee()
    {
        try
        {
            dsEmployee = objTourAndTravels.GetEmployeeForTravel(0);
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

    private void GetTicketList()
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

            if (!string.IsNullOrEmpty(txtTicketNo.Text))
                ticketNo = txtTicketNo.Text.Trim();
            else
                ticketNo = string.Empty;

            if (ddlTicketStatus.SelectedIndex > 0)
                ticketStatus = ddlTicketStatus.SelectedItem.Text;
            else
                ticketStatus = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;

            dsTicketList = objTicket.GetTicketReport(fromDate, toDate, ticketNo, ticketStatus, empRecordID);
            if (dsTicketList.Tables.Count > 0 && dsTicketList.Tables[0].Rows.Count > 0)
            {
                Session["dsTicketlist"] = dsTicketList;
                gvTicketList.DataSource = dsTicketList.Tables[0];
                gvTicketList.DataBind();
                lblRecords.Text = "Records[" + dsTicketList.Tables[0].Rows.Count + "]";
            }
            else
            {
                gvTicketList.DataSource = null;
                gvTicketList.DataBind();
                lblRecords.Text = "Records[0]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcel()
    {
        try
        {
            DataSet ds = (DataSet)Session["dsTicketList"];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                pnlMsg.Visible = false;
                lblMsg.Text = string.Empty;
                Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
                {
                    string headerText = Convert.ToString(ds.Tables[0].Columns[i].ColumnName).ToUpper();
                    XcelApp.Cells[1, i] = headerText;
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                    {
                        string value = Convert.ToString(ds.Tables[0].Rows[i][j].ToString());
                        if (!string.IsNullOrEmpty(value) && value != string.Empty)
                        {
                            XcelApp.Cells[i + 2, j] = Convert.ToString(ds.Tables[0].Rows[i][j].ToString());
                        }
                    }
                }

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
            else
            {
                ExceptionMessage("Invalid Date Range");
                return;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ToCSVNew(DataTable dt)
    {
        string csv = string.Empty;
        for (int i = 1; i < dt.Columns.Count; i++)
        {
            string headerText = Convert.ToString(dt.Columns[i].ColumnName).ToUpper();
            csv += headerText + ',';
        }
        csv += "\r\n";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                string value = Convert.ToString(dt.Rows[i][j].ToString());
                if (!string.IsNullOrEmpty(value) && value != string.Empty)
                {
                    csv += Convert.ToString(dt.Rows[i][j]).Replace(",", ";") + ',';
                }
            }
            csv += "\r\n";
        }

        string folderPath = "D:\\CSV\\";
        File.WriteAllText(folderPath + "TicketReport.csv", csv);
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

            string fileName = "TicketReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
