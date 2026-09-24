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

public partial class REPORTS_SALE_ORDER_GROSS_MARGIN_SalesGrossMarginReportOne : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsUnit = new DataSet();
    DataSet dsSalesGrossMargin = new DataSet();

    string postingMonth = string.Empty;
    string unitName = string.Empty;
    string customerName = string.Empty;
    string OANo = string.Empty;
    string billNo = string.Empty;
    string companyType = string.Empty;
    string pocNPoc = string.Empty;

    double amountINR = 0;
    double costAsPerOrder = 0;
    double marginAsPerOrder = 0;
    double costAsPerCostCenter = 0;
    double materialOverheads = 0;
    double warranty = 0;
    double actualGrossMargin = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePAnel();
            if (!IsPostBack)
            {
                Session["dsSaleGrossMargin"] = null;
                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

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
        GetSaleGrossMarginReport();
    }

    protected void gvSaleMargin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblOANo = (Label)e.Row.FindControl("lblOANo");
                Label lblCustomerName = (Label)e.Row.FindControl("lblCustomerName");                


                Label lblAmountINR = (Label)e.Row.FindControl("lblAmountINR");
                Label lblCostAsPerOrder = (Label)e.Row.FindControl("lblCostAsPerOrder");
                Label lblMarginAsPerOrder = (Label)e.Row.FindControl("lblMarginAsPerOrder");
                Label lblCostAsPerCostCenter = (Label)e.Row.FindControl("lblCostAsPerCostCenter");
                Label lblMaterialOverheads = (Label)e.Row.FindControl("lblMaterialOverheads");
                Label lblWarranty = (Label)e.Row.FindControl("lblWarranty");
                Label lblActualGrossMargin = (Label)e.Row.FindControl("lblActualGrossMargin");

                if (!string.IsNullOrEmpty(Convert.ToString(lblAmountINR.Text)))
                    amountINR += Convert.ToDouble(lblAmountINR.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblCostAsPerOrder.Text)))
                    costAsPerOrder += Convert.ToDouble(lblCostAsPerOrder.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblMarginAsPerOrder.Text)))
                    marginAsPerOrder += Convert.ToDouble(lblMarginAsPerOrder.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblCostAsPerCostCenter.Text)))
                    costAsPerCostCenter += Convert.ToDouble(lblCostAsPerCostCenter.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblMaterialOverheads.Text)))
                    materialOverheads += Convert.ToDouble(lblMaterialOverheads.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblWarranty.Text)))
                    warranty += Convert.ToDouble(lblWarranty.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblActualGrossMargin.Text)))
                    actualGrossMargin += Convert.ToDouble(lblActualGrossMargin.Text);

                lblTotalAmountINR.Text = Convert.ToString(amountINR);
                lblTotalCostAsPerOrder.Text = Convert.ToString(costAsPerOrder);
                lblTotalMarginAsPerOrder.Text = Convert.ToString(marginAsPerOrder);
                lblTotalCostAsPerCostCenter.Text = Convert.ToString(costAsPerCostCenter);
                lblTotalMaterialOverheads.Text = Convert.ToString(materialOverheads);
                lblTotalWarranty.Text = Convert.ToString(warranty);
                lblTotalActualGrossMargin.Text = Convert.ToString(actualGrossMargin);

                e.Row.ToolTip = "OA No.:" + lblOANo.Text + ", Customer Name:" + lblCustomerName.Text;

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
        if (gvSaleMargin.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsSaleGrossMargin"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateSaleGrossMargin(Convert.ToInt32(ViewState["recordID"]), Convert.ToString(ViewState["OANo"]));
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

    private void GetSaleGrossMarginReport()
    {
        try
        {
            postingMonth = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");

            if (ddlCompany.SelectedIndex > 0)
                unitName = ddlCompany.SelectedItem.Text;
            else
                unitName = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtOANo.Text))
                OANo = txtOANo.Text;
            else
                OANo = string.Empty;

            //if (!string.IsNullOrEmpty(txtBillNo.Text))
            //    billNo = txtBillNo.Text;
            //else
            //    billNo = string.Empty;

            if (ddlCompanyType.SelectedIndex > 0)
                companyType = ddlCompanyType.SelectedItem.Text;
            else
                companyType = string.Empty;

            if (ddlPOCNPOC.SelectedIndex > 0)
                pocNPoc = ddlPOCNPOC.SelectedItem.Text;
            else
                pocNPoc = string.Empty;

            dsSalesGrossMargin = objReports.GetSaleGrossMarginReport(postingMonth, unitName, customerName, OANo, "", companyType, pocNPoc);
            if (dsSalesGrossMargin.Tables.Count > 0 && dsSalesGrossMargin.Tables[0].Rows.Count > 0)
            {
                Session["dsSaleGrossMargin"] = dsSalesGrossMargin;
                gvSaleMargin.DataSource = dsSalesGrossMargin.Tables[0];
                gvSaleMargin.DataBind();
            }
            else
            {
                Session["dsSaleGrossMargin"] = null;
                gvSaleMargin.DataSource = null;
                gvSaleMargin.DataBind();
                ExceptionMessage("No data found...!!!");

                lblTotalAmountINR.Text = "0";
                lblTotalCostAsPerOrder.Text = "0";
                lblTotalMarginAsPerOrder.Text = "0";
                lblTotalCostAsPerCostCenter.Text = "0";
                lblTotalMaterialOverheads.Text = "0";
                lblTotalWarranty.Text = "0";
                lblTotalActualGrossMargin.Text = "0";
            }
            lblRecords.Text = "Records[" + dsSalesGrossMargin.Tables[0].Rows.Count + "]";
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

            for (int i = 1; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    csv += dt.Rows[i][j].ToString().Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "Posted_Sales_Gross_Margin_Report_" + Convert.ToDateTime(hdPostingMonth.Value).ToString("MMM-yyyy");
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

    private void UpdateSaleGrossMargin(int recordID, string OANo)
    {
        try
        {
            double projectCosts = 0;
            double actualGrossMargin = 0;
            double actual = 0;
            double highDelta = 0;

            if (Convert.ToDouble(txtProjectCosts.Text) > 0)
                projectCosts = Convert.ToDouble(txtProjectCosts.Text);
            else
                projectCosts = 0;

            if (Convert.ToDouble(txtActualGrossMargin.Text) > 0)
                actualGrossMargin = Convert.ToDouble(txtActualGrossMargin.Text);
            else
                actualGrossMargin = 0;

            if (Convert.ToDouble(txtActual.Text) > 0)
                actual = Convert.ToDouble(txtActual.Text);
            else
                actual = 0;

            if (Convert.ToDouble(txtHighDelta.Text) > 0)
                highDelta = Convert.ToDouble(txtHighDelta.Text);
            else
                highDelta = 0;

            int value = objReports.UpdateSaleGrossMargin(recordID, projectCosts, actualGrossMargin, actual, highDelta, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("OA No. [" + OANo + "] updated successfully.");
                GetSaleGrossMarginReport();
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

    private void HidePAnel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}
