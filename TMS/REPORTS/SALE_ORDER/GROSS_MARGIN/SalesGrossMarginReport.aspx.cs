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

public partial class REPORTS_SALE_ORDER_GROSS_MARGIN_SalesGrossMarginReport : System.Web.UI.Page
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
                Label lblBillNo = (Label)e.Row.FindControl("lblBillNo");

                e.Row.ToolTip = "OA No.:" + lblOANo.Text + ", Customer Name:" + lblCustomerName.Text + ", Bill No.:" + lblBillNo.Text;

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

    protected void gvSaleMargin_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblRecordID");
                Label lblOANo = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblOANo");
                Label lblCustomerName = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblCustomerName");
                Label lblWarrantyPer = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblWarrantyPer");
                Label lblMO = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblMO");
                Label lblBillDate = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblBillDate");
                Label lblClass = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblClass");
                Label lblCurrDesc = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblCurrDesc");
                Label lblRate = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblRate");
                Label lblFCurrency = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblFCurrency");
                Label lblAmountINR = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblAmountINR");
                Label lblBillNo = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblBillNo");
                Label lblLocation = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblLocation");
                Label lblBusSegment = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblBusSegment");
                Label lblGrossMarginAsPerOrder = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblGrossMarginAsPerOrder");
                Label lblPercentage = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblPercentage");
                Label lblCostAsPerOrder = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblCostAsPerOrder");
                Label lblMarginAsPerOrder = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblMarginAsPerOrder");
                Label lblCostAsPerCostCenter = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblCostAsPerCostCenter");
                Label lblProjectCosts = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblProjectCosts");
                Label lblWarranty = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblWarranty");
                Label lblActualGrossMargin = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblActualGrossMargin");
                Label lblStandard = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblStandard");
                Label lblActual = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblActual");
                Label lblHighDelta = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblHighDelta");
                Label lblType = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblType");
                Label lblMonth = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblMonth");
                Label lblPOCNONPOC = (Label)gvSaleMargin.Rows[rowindex].FindControl("lblPOCNONPOC");


                ViewState["recordID"] = Convert.ToInt32(lblRecordID.Text);
                ViewState["OANo"] = Convert.ToString(lblOANo.Text);

                if (e.CommandArgument == "PROPERTIES")
                {
                    lblLegend.Text = "OA No [" + Convert.ToString(lblOANo.Text) + "]";

                    txtCustomerNameNew.Text = lblCustomerName.Text;
                    txtWarrantyPer.Text = lblWarrantyPer.Text;
                    txtMO.Text = lblMO.Text;
                    txtBillDate.Text = Convert.ToDateTime(lblBillDate.Text).ToString("dd-MMM-yyyy");
                    txtClass.Text = lblClass.Text;
                    txtCurrDesc.Text = lblCurrDesc.Text;
                    txtRate.Text = lblRate.Text;
                    txtFCurrency.Text = lblFCurrency.Text;
                    txtAmountINR.Text = lblAmountINR.Text;
                    txtBillNoNew.Text = lblBillNo.Text;
                    txtLocation.Text = lblLocation.Text;
                    txtBusSegment.Text = lblBusSegment.Text;
                    txtGrossMarginAsPerOrder.Text = lblGrossMarginAsPerOrder.Text;
                    txtPercentage.Text = lblPercentage.Text;
                    txtCostAsPerOrder.Text = lblCostAsPerOrder.Text;
                    txtMarginAsPerOrder.Text = lblMarginAsPerOrder.Text;
                    txtCostAsPerCostCenter.Text = lblCostAsPerCostCenter.Text;
                    txtProjectCosts.Text = lblProjectCosts.Text;
                    txtWarranty.Text = lblWarranty.Text;

                    hdActualGrossMargin.Value = lblActualGrossMargin.Text;
                    txtActualGrossMargin.Text = lblActualGrossMargin.Text;

                    txtStandard.Text = lblStandard.Text;

                    hdActual.Value = lblActual.Text;
                    txtActual.Text = lblActual.Text;

                    hdHighDelta.Value = lblHighDelta.Text;
                    txtHighDelta.Text = lblHighDelta.Text;

                    txtType.Text = lblType.Text;
                    txtMonth.Text = lblMonth.Text;
                    txtPOCNONPOC.Text = lblPOCNONPOC.Text;


                    ModalPopupExtender1.Show();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
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

            if (!string.IsNullOrEmpty(txtBillNo.Text))
                billNo = txtBillNo.Text;
            else
                billNo = string.Empty;

            if (ddlCompanyType.SelectedIndex > 0)
                companyType = ddlCompanyType.SelectedItem.Text;
            else
                companyType = string.Empty;

            if (ddlPOCNPOC.SelectedIndex > 0)
                pocNPoc = ddlPOCNPOC.SelectedItem.Text;
            else
                pocNPoc = string.Empty;

            dsSalesGrossMargin = objReports.GetSaleGrossMarginReport(postingMonth, unitName, customerName, OANo, billNo, companyType, pocNPoc);
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
