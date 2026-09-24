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

public partial class REPORTS_POHeaderDetail : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsProject = new DataSet();
    DataSet dsGroup = new DataSet();
    DataSet dsSubgroup = new DataSet();
    DataSet dsProductUnit = new DataSet();
    DataSet dsEstimatedItem = new DataSet();
    DataSet dsUnit = new DataSet();
    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                if ((Request.QueryString["pono"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["pono"]))) && (Request.QueryString["docclass"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["docclass"]))))
                    BindDetails(Convert.ToString(Request.QueryString["unit"]), Convert.ToString(Request.QueryString["pono"]), Convert.ToString(Request.QueryString["docclass"]));
                else
                    Response.Redirect("~/REPORTS/POReportSummary.aspx");
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    double totalAmount = 0;
    protected void gvPOReportDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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
            throw ex;
            return;
        }
    }

    #endregion


    #region METHODS[============================]


    private void BindDetails(string unit, string poNo, string docClass)
    {
        try
        {
            DataSet dsDetail = new DataSet();
            dsDetail = objReports.GetPODetail(unit, poNo, docClass);
            if (dsDetail.Tables.Count > 0)
            {
                if (dsDetail.Tables[0].Rows.Count > 0)
                {
                    if (dsDetail.Tables[0].Rows[0]["PO_NO"] != DBNull.Value)
                        txtPONo.Text = Convert.ToString(dsDetail.Tables[0].Rows[0]["PO_NO"]);
                    else
                        txtPONo.Text = string.Empty;

                    if (dsDetail.Tables[0].Rows[0]["PO_DATE"] != DBNull.Value)
                        txtPODate.Text = Convert.ToDateTime(dsDetail.Tables[0].Rows[0]["PO_DATE"]).ToString("dd-MMM-yyyy");
                    else
                        txtPODate.Text = string.Empty;

                    if (dsDetail.Tables[0].Rows[0]["VENDOR_NAME"] != DBNull.Value)
                        txtVendorName.Text = Convert.ToString(dsDetail.Tables[0].Rows[0]["VENDOR_NAME"]);
                    else
                        txtVendorName.Text = string.Empty;

                    if (dsDetail.Tables[0].Rows[0]["VENDOR_CODE"] != DBNull.Value)
                        txtVendorCode.Text = Convert.ToString(dsDetail.Tables[0].Rows[0]["VENDOR_CODE"]);
                    else
                        txtVendorCode.Text = string.Empty;
                }

                if (dsDetail.Tables[1].Rows.Count > 0)
                {
                    gvPOReportDetail.DataSource = dsDetail.Tables[1];
                    gvPOReportDetail.DataBind();
                }
            }
            else
            {
                gvPOReportDetail.DataSource = null;
                gvPOReportDetail.DataBind();
                Response.Redirect("~/PROJECT_MGMT/ProductList.aspx");
            }
            lblRecords.Text = "Records[" + gvPOReportDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            throw ex;
            return;
        }
    }

    #endregion

}
