using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PROJECT_AdjustBudgetedHours : System.Web.UI.Page
{

    #region VARIABLES[======================]

    DataTable dtMonth = new DataTable();
    BAL.EnggHours objEnggHours = new BAL.EnggHours();
    int projectID = 0;
    string startDate = string.Empty;
    string endDate = string.Empty;

    #endregion


    #region EVENTS[=========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {


            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                if (Convert.ToInt32(Request.QueryString["projectid"]) > 0)
                {

                    txtJobNo.Text = Convert.ToString(Request.QueryString["jobno"]);
                    txtCustomerName.Text = Convert.ToString(Request.QueryString["customername"]);
                }
                GetMonths();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void imgbtnStartDate_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlStartDate.Visible == false)
        {
            divStartDate.Visible = true;
            pnlStartDate.Visible = true;
        }
        else
        {
            divStartDate.Visible = false;
            pnlStartDate.Visible = false;
        }
    }

    protected void imgbtnEndDate_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlEndDate.Visible == false)
        {
            divEndDate.Visible = true;
            pnlEndDate.Visible = true;
        }
        else
        {
            divEndDate.Visible = false;
            pnlEndDate.Visible = false;
        }
    }

    protected void calendarStartDate_SelectionChanged(object sender, EventArgs e)
    {
        txtStartDate.Text = calendarStartDate.SelectedDate.ToString("dd-MMM-yyyy");
        pnlStartDate.Visible = false;

        if (Convert.ToDateTime(txtStartDate.Text).Date > Convert.ToDateTime(txtEndDate.Text).Date)
        {
            pnlMsg.Visible = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Start date must be smaller or equal to end date.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            pnlMsg.Visible = false;
            lblMsg.Visible = false;
            lblMsg.Text = string.Empty;
        }
    }

    protected void calendarEndDate_SelectionChanged(object sender, EventArgs e)
    {
        txtEndDate.Text = calendarEndDate.SelectedDate.ToString("dd-MMM-yyyy");
        pnlEndDate.Visible = false;

        if (Convert.ToDateTime(txtStartDate.Text).Date > Convert.ToDateTime(txtEndDate.Text).Date)
        {
            pnlMsg.Visible = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Start date must be smaller or equal to end date.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            pnlMsg.Visible = false;
            lblMsg.Visible = false;
            lblMsg.Text = string.Empty;
        }
    }

    protected void btnProjectList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/ProjectList.aspx");
    }

    protected void btnGetMonthList_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtStartDate.Text).Date > Convert.ToDateTime(txtEndDate.Text).Date)
        {
            gvMonthList.DataSource = null;
            gvMonthList.DataBind();

            lblRecords.Text = "Records[" + gvMonthList.Rows.Count + "]";

            pnlMsg.Visible = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Start date must be smaller or equal to end date.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        GetMonths();


    }

    protected void btnAdjust_Click(object sender, EventArgs e)
    {
        AdjustHours();
    }

    #endregion


    #region METHODS[========================]

    private void GetMonths()
    {
        try
        {
            dtMonth.Columns.Add("RECORD_ID", typeof(int));
            dtMonth.Columns.Add("MONTH", typeof(string));
            dtMonth.Columns.Add("HOURS", typeof(string));

            int startMonth = Convert.ToInt32(Convert.ToDateTime(txtStartDate.Text).Month);
            int startYear = Convert.ToInt32(Convert.ToDateTime(txtStartDate.Text).Year);
            int endYear = Convert.ToInt32(Convert.ToDateTime(txtEndDate.Text).Year);

            string monthName = string.Empty;

            int startMonthCount = 12 - Convert.ToInt32(12 - Convert.ToDateTime(txtStartDate.Text).Month);
            int endMonthCount = 12 - Convert.ToInt32(12 - Convert.ToDateTime(txtEndDate.Text).Month);

            int monthCount = Convert.ToInt32(Convert.ToDateTime(txtEndDate.Text).Month - Convert.ToDateTime(txtStartDate.Text).Month + 12 * (Convert.ToDateTime(txtEndDate.Text).Year - Convert.ToDateTime(txtStartDate.Text).Year));

            int count = 0;

            if (startYear == endYear)
            {
                for (int i = startYear; i <= endYear; i++)
                {
                    for (int j = startMonth; j <= endMonthCount; j++)
                    {
                        count++;
                        monthName = j + "-" + i;
                        DataRow dr = dtMonth.NewRow();
                        dr["RECORD_ID"] = count;
                        dr["MONTH"] = Convert.ToDateTime(monthName).ToString("MMM-yyyy").ToUpper();

                        dtMonth.Rows.Add(dr);
                    }
                }
            }
            else
            {
                for (int i = startYear; i <= endYear; i++)
                {
                    if (i == startYear)
                    {
                        for (int j = startMonth; j <= 12; j++)
                        {
                            count++;
                            monthName = j + "-" + i;
                            DataRow dr = dtMonth.NewRow();
                            dr["RECORD_ID"] = count;
                            dr["MONTH"] = Convert.ToDateTime(monthName).ToString("MMM-yyyy").ToUpper();

                            dtMonth.Rows.Add(dr);
                        }
                    }
                    else if (i > startYear && i < endYear)
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            count++;
                            monthName = j + "-" + i;
                            DataRow dr = dtMonth.NewRow();
                            dr["RECORD_ID"] = count;
                            dr["MONTH"] = Convert.ToDateTime(monthName).ToString("MMM-yyyy").ToUpper();

                            dtMonth.Rows.Add(dr);
                        }
                    }
                    else if (i == endYear)
                    {
                        for (int j = 1; j <= endMonthCount; j++)
                        {
                            count++;
                            monthName = j + "-" + i;
                            DataRow dr = dtMonth.NewRow();
                            dr["RECORD_ID"] = count;
                            dr["MONTH"] = Convert.ToDateTime(monthName).ToString("MMM-yyyy").ToUpper();

                            dtMonth.Rows.Add(dr);
                        }
                    }
                }
            }

            if (dtMonth.Rows.Count > 0)
            {
                gvMonthList.DataSource = dtMonth;
                gvMonthList.DataBind();
            }
            else
            {
                gvMonthList.DataSource = null;
                gvMonthList.DataBind();
            }
            lblRecords.Text = "Records[" + gvMonthList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void AdjustHours()
    {
        try
        {
            int value = 0;
            int month = 0;
            int year = 0;

            if (Convert.ToInt32(Request.QueryString["projectid"]) > 0)
            {
                projectID = Convert.ToInt32(Request.QueryString["projectid"]);
            }
            else
            {
                projectID = 0;
            }
            startDate = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
            endDate = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd");

            foreach (GridViewRow gr in gvMonthList.Rows)
            {
                TextBox txtHours = gr.Cells[2].FindControl("txtHours") as TextBox;
                month = Convert.ToInt32(Convert.ToDateTime(gr.Cells[1].Text).Month);
                year = Convert.ToInt32(Convert.ToDateTime(gr.Cells[1].Text).Year);

                value = objEnggHours.AdjustHours(projectID, txtJobNo.Text.Trim(), startDate, endDate, Convert.ToString(gr.Cells[1].Text), txtHours.Text, month, year, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    value++;
                }
            }
            if (value > 0)
            {
                SuccessMessage("Hours adjusted successfully of " + txtJobNo.Text);
            }
            else
            {
                ExceptionMessage("Please try again!");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
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
