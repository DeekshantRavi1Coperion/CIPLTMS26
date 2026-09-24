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

public partial class REPORTS_PURCHASE_ORDER_PostedPOList : System.Web.UI.Page
{

    #region VARIABLES[=======================]


    BAL.Purchase objPurchase = new BAL.Purchase();
    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsPOList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;

    string poNo = string.Empty;
    string vendorName = string.Empty;
    string JOBNo = string.Empty;
    double amount = 0;
    string status = string.Empty;
    //int unitId = 0;
    string unitName = string.Empty;
    int excludeCIDF = 0;

    //string dbNameA35 = string.Empty;
    //string dbNameDLH = string.Empty;
    //string dbNameSEZ = string.Empty;
    //string dbNameGNU = string.Empty;
    //string unitNameA35 = string.Empty;
    //string unitNameDLH = string.Empty;
    //string unitNameSEZ = string.Empty;
    //string unitNameGNU = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["PO_REPORT"] = null;

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
        lblTotalAmount.Text = "0";
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetPostedPOList();
    }

    double totalAmount = 0;

    protected void gvPostedPOList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                totalAmount += Convert.ToDouble(lblAmount.Text);
                lblTotalAmount.Text = Convert.ToString(totalAmount);
                lblTotalAmount.ForeColor = System.Drawing.Color.Green;
            }

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
        if (gvPostedPOList.Rows.Count > 0)
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

    private void GetPostedPOList()
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

            if (!string.IsNullOrEmpty(txtAmount.Text))
                amount = Convert.ToDouble(txtAmount.Text);
            else
                amount = 0;

            if (ddlStatus.SelectedIndex > 0)
                status = Convert.ToString(ddlStatus.SelectedItem.Text);
            else
                status = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;

            if (chkExcludeCIDF.Checked)
                excludeCIDF = 1;
            else
                excludeCIDF = 0;

            dsPOList = objPurchase.GetPostedPOList(fromDate, toDate, poNo, vendorName, unitName, status, JOBNo, amount, excludeCIDF);

            if (dsPOList.Tables.Count > 0 && dsPOList.Tables[0].Rows.Count > 0)
            {
                Session["PO_REPORT"] = dsPOList;
                gvPostedPOList.DataSource = dsPOList.Tables[0];
                gvPostedPOList.DataBind();
            }
            else
            {
                Session["PO_REPORT"] = null;
                gvPostedPOList.DataSource = null;
                gvPostedPOList.DataBind();
            }
            lblRecords.Text = "Records[" + dsPOList.Tables[0].Rows.Count + "]";
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

            string fileName = "PO_Header_From_" + txtStartDateSearch.Text + "_To_" + txtEndDateSearch.Text;
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
