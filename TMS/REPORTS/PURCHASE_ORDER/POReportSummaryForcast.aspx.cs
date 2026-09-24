using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class REPORTS_PURCHASE_ORDER_POReportSummaryForcast : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsPOReport = new DataSet();
    DataSet dsUnit = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;

    string poNo = string.Empty;
    string vendorName = string.Empty;
    string JOBNo = string.Empty;
    double amount1 = 0;
    double amount2 = 0;
    string status = string.Empty;
    string unitName = string.Empty;
    string sign = string.Empty;
    int excludeCIDF = 0;
    int isPOCJobs = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["PO_REPORT"] = null;

                hdPOMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPOMonth.Text = Convert.ToString(hdPOMonth.Value);

                BindUnit();
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
        GetPOReportSummery();

        if (ddlSign.SelectedIndex == 5)
        {
            txtAmountTwo.Enabled = true;
        }
        else
        {
            txtAmountTwo.Text = string.Empty;
            txtAmountTwo.Enabled = false;
        }
    }

    protected void gvPOReportSummery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPOReportSummery.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["PO_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    #endregion


    #region METHODS[=========================]

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetPOReportSummery()
    {
        try
        {
            string POmonth = string.Empty;
            string month1 = string.Empty;
            string month2 = string.Empty;
            string month3 = string.Empty;
            string month4 = string.Empty;
            string month5 = string.Empty;
            string month6 = string.Empty;
            string month7 = string.Empty;
            string month8 = string.Empty;
            string month9 = string.Empty;
            string month10 = string.Empty;
            string month11 = string.Empty;
            string month12 = string.Empty;


            if (Convert.ToInt32(Convert.ToDateTime(hdPOMonth.Value).ToString("MM")) < 10)
                POmonth = Convert.ToString(Convert.ToInt32(Convert.ToDateTime(hdPOMonth.Value).ToString("yyyy")) + "-0" + Convert.ToInt32(Convert.ToDateTime(hdPOMonth.Value).ToString("MM")));
            else
                POmonth = Convert.ToString(Convert.ToInt32(Convert.ToDateTime(hdPOMonth.Value).ToString("yyyy")) + "-" + Convert.ToInt32(Convert.ToDateTime(hdPOMonth.Value).ToString("MM")));

            int month = Convert.ToInt32(DateTime.Now.ToString("MM"));
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));

            //int month = Convert.ToInt32(Convert.ToDateTime(POmonth).ToString("MM"));
            //int year = Convert.ToInt32(Convert.ToDateTime(POmonth).ToString("yyyy"));

            for (int i = 0; i < 12; i++)
            {
                if (i > 0)
                    month = month + 1;

                if (month > 12)
                {
                    month = 1;
                    year = year + 1;
                }

                if (i == 0)
                    month1 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 1)
                    month2 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 2)
                    month3 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 3)
                    month4 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 4)
                    month5 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 5)
                    month6 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 6)
                    month7 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 7)
                    month8 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 8)
                    month9 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 9)
                    month10 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 10)
                    month11 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 11)
                    month12 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
            }

            if (!string.IsNullOrEmpty(txtPONo.Text))
                poNo = txtPONo.Text.ToUpper();
            else
                poNo = string.Empty;

            if (!string.IsNullOrEmpty(txtVendorName.Text))
                vendorName = txtVendorName.Text;
            else
                vendorName = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text.ToUpper();
            else
                JOBNo = string.Empty;

            if (!string.IsNullOrEmpty(txtAmountOne.Text))
                amount1 = Convert.ToDouble(txtAmountOne.Text);
            else
                amount2 = 0;

            if (!string.IsNullOrEmpty(txtAmountTwo.Text))
                amount2 = Convert.ToDouble(txtAmountTwo.Text);
            else
                amount2 = 0;

            if (ddlStatus.SelectedIndex > 0)
                status = Convert.ToString(ddlStatus.SelectedItem.Text);
            else
                status = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;

            sign = Convert.ToString(ddlSign.SelectedItem.Text);

            if (chkExcludeCIDF.Checked)
                excludeCIDF = 1;
            else
                excludeCIDF = 0;

            if (chkOnlyPOCJobs.Checked)
                isPOCJobs = 1;
            else
                isPOCJobs = 0;


            dsPOReport = objReports.GetPOReportSummeryForcast(POmonth, month1, month2, month3, month4, month5, month6,
                                                              month7, month8, month9, month10, month11, month12,
                                      poNo, vendorName, unitName, status, JOBNo, amount1, amount2, sign, excludeCIDF, isPOCJobs);


            if (dsPOReport.Tables.Count > 0 && dsPOReport.Tables[0].Rows.Count > 0)
            {
                Session["PO_REPORT"] = dsPOReport;
                gvPOReportSummery.DataSource = dsPOReport.Tables[0];
                gvPOReportSummery.DataBind();
            }
            else
            {
                Session["PO_REPORT"] = null;
                gvPOReportSummery.DataSource = null;
                gvPOReportSummery.DataBind();
            }
            lblRecords.Text = "Records[" + dsPOReport.Tables[0].Rows.Count + "]";
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

            string fileName = "PO_Summary_Forcast_Header_Report";
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
