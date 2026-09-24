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

public partial class USGAAP_BILLING_PostedList : System.Web.UI.Page
{

    #region VARIABLES[=======================]


    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Posting objPosting = new BAL.Posting();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsPostedList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsCountry = new DataSet();
    DataSet dsCurrency = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string billNo = string.Empty;
    string unitName = string.Empty;

    string customerName = string.Empty;
    string revenueAccount = string.Empty;
    int isNewInserted = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["POSTED_LIST"] = null;
                dsCountry = objCommon.GetCountry();
                Session["dsCountry"] = dsCountry;

                dsCurrency = objTourAndTravels.GetPrimaryDetails("sp_get_currency_list");
                Session["dsCurrency"] = dsCurrency;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;
                BindUnit();
                Session["DB_DETAILS"] = objCommon.GetDBDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetPostedList();
    }

    protected void gvPostedList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
                Label lblCustomerName = (Label)e.Row.FindControl("lblCustomerName");
                Label lblJobNo = (Label)e.Row.FindControl("lblJobNo");
                                
                e.Row.ToolTip = "Invoice No.:" + lblInvoiceNo.Text + ", Customer Name:" + lblCustomerName.Text + ", JOB No.:" + lblJobNo.Text;
                                                                     
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPostedList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["POSTED_LIST"];
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

    private void GetPostedList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (!string.IsNullOrEmpty(txtBillNo.Text))
                billNo = txtBillNo.Text;
            else
                billNo = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;


            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtRevenueAccount.Text))
                revenueAccount = txtRevenueAccount.Text;
            else
                revenueAccount = string.Empty;

            if (chkNewInserted.Checked)
                isNewInserted = 1;
            else
                isNewInserted = 0;

            dsPostedList = objPosting.GetPostedListNew(fromDate, toDate, billNo, unitName, customerName, revenueAccount, isNewInserted);

            if (dsPostedList.Tables.Count > 0 && dsPostedList.Tables[0].Rows.Count > 0)
            {
                Session["POSTED_LIST"] = dsPostedList;
                gvPostedList.DataSource = dsPostedList.Tables[0];
                gvPostedList.DataBind();
            }
            else
            {
                Session["POSTED_LIST"] = null;
                gvPostedList.DataSource = null;
                gvPostedList.DataBind();
            }
            lblRecords.Text = "Records[" + dsPostedList.Tables[0].Rows.Count + "]";
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

            string fileName = "Posted_Invoice_List" + DateTime.Now.ToString("dd_MMM_yyyy");
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