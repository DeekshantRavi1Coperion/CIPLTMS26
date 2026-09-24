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

public partial class REPORTS_SALE_ORDER_GROSS_MARGIN_ImportedCostCenterListOne : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsUnit = new DataSet();
    DataSet dsCostCenter = new DataSet();
    DataSet dsCostCenterStatus = new DataSet();

    string month = string.Empty;
    string orderNo = string.Empty;
    string costCenterCode = string.Empty;
    string costCenterName = string.Empty;
    int unitID = 0;
    int statusID = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["COST_CENTER"] = null;

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);
                BindUnit();
                GetCostCenterStatus();
                GetCostCenterList();
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
        GetCostCenterList();
    }

    protected void gvCostCenterList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCostCenterCode = (Label)e.Row.FindControl("lblCostCenterCode");
                Label lblCostCenterName = (Label)e.Row.FindControl("lblCostCenterName");
                Label lblOrderNo = (Label)e.Row.FindControl("lblOrderNo");

                e.Row.ToolTip = "Cost Center Code:" + lblCostCenterCode.Text + ", Customer Name:" + lblCostCenterName.Text + ", JOB No.:" + lblOrderNo.Text;

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

    protected void gvCostCenterList_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblCostCenterID = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblCostCenterID");
                Label lblCostCenterCode = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblCostCenterCode");
                Label lblCostCenterName = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblCostCenterName");
                Label lblOrderNo = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblOrderNo");
                Label lblCostCenterAmount = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblCostCenterAmount");
                Label lblDirectBilling = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblDirectBilling");
                Label lblPartialBilling = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblPartialBilling");
                Label lblNetAmount = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblNetAmount");
                Label lblMonth = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblMonth");
                Label lblUnit = (Label)gvCostCenterList.Rows[rowindex].FindControl("lblUnit");

                ViewState["costCenterID"] = Convert.ToInt32(lblCostCenterID.Text);
                ViewState["costCenterCode"] = Convert.ToString(lblCostCenterCode.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    lblLegend.Text = "Cost Center Code [" + Convert.ToString(lblCostCenterCode.Text) + "] - [" + Convert.ToString(lblUnit.Text) + "]";

                    txtCostCenterNameNew.Text = Convert.ToString(lblCostCenterName.Text);
                    txtOrderNoNew.Text = Convert.ToString(lblOrderNo.Text);
                    txtCostCenterAmount.Text = Convert.ToString(lblCostCenterAmount.Text);
                    txtDirectBilling.Text = Convert.ToString(lblDirectBilling.Text);
                    txtPartialBilling.Text = Convert.ToString(lblPartialBilling.Text);
                    txtNetAmount.Text = Convert.ToString(lblNetAmount.Text);
                    //txtMonth.Text = Convert.ToDateTime(lblMonth.Text).ToString("MMM-yyyy");

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
        if (gvCostCenterList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["COST_CENTER"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateCostCenter(Convert.ToInt32(ViewState["costCenterID"]), Convert.ToString(ViewState["costCenterCode"]));

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

    private void GetCostCenterStatus()
    {
        try
        {
            dsCostCenterStatus = objReports.GetCostCenterStatus();
            if (dsCostCenterStatus.Tables.Count > 0 && dsCostCenterStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsCostCenterStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;
            }            
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetCostCenterList()
    {
        try
        {

            month = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");

            if (!string.IsNullOrEmpty(txtOrderNo.Text))
                orderNo = txtOrderNo.Text;
            else
                orderNo = string.Empty;

            if (!string.IsNullOrEmpty(txtCostCenterCode.Text))
                costCenterCode = txtCostCenterCode.Text;
            else
                costCenterCode = string.Empty;

            if (!string.IsNullOrEmpty(txtCostCenterName.Text))
                costCenterName = txtCostCenterName.Text;
            else
                costCenterName = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);
            else
                unitID = 0;

            dsCostCenter = objReports.GetPostedListInDetails(month, orderNo, costCenterCode, costCenterName
                                                            , unitID, statusID);

            if (dsCostCenter.Tables.Count > 0 && dsCostCenter.Tables[0].Rows.Count > 0)
            {
                Session["COST_CENTER"] = dsCostCenter;
                gvCostCenterList.DataSource = dsCostCenter.Tables[0];
                gvCostCenterList.DataBind();
            }
            else
            {
                Session["COST_CENTER"] = null;
                gvCostCenterList.DataSource = null;
                gvCostCenterList.DataBind();
            }
            lblRecords.Text = "Records[" + dsCostCenter.Tables[0].Rows.Count + "]";
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

            for (int i = 2; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 2; j < dt.Columns.Count; j++)
                {
                    csv += dt.Rows[i][j].ToString().Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            //string fileName = "Imported_Cost_Center_Report_" + Convert.ToDateTime(hdPostingMonth.Value).ToString("MMM-yyyy");
            string fileName = "Imported_Cost_Center_Report_" + DateTime.Now.ToString("dd-MMM-yyyy");
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

    private void UpdateCostCenter(int costCenterID, string costCenterCode)
    {
        try
        {
            double costCenterAmount = 0;
            double directBilling = 0;
            double partialBilling = 0;
            double netAmount = 0;
            string month = string.Empty;

            if (!string.IsNullOrEmpty(txtCostCenterAmount.Text))
                costCenterAmount = Convert.ToDouble(txtCostCenterAmount.Text);
            else
                costCenterAmount = 0;

            if (!string.IsNullOrEmpty(txtDirectBilling.Text))
                directBilling = Convert.ToDouble(txtDirectBilling.Text);
            else
                directBilling = 0;

            if (!string.IsNullOrEmpty(txtPartialBilling.Text))
                partialBilling = Convert.ToDouble(txtPartialBilling.Text);
            else
                partialBilling = 0;

            if (!string.IsNullOrEmpty(txtNetAmount.Text))
                netAmount = Convert.ToDouble(txtNetAmount.Text);
            else
                netAmount = 0;

            int value = objReports.UpdateCostCenter(costCenterID, costCenterAmount, directBilling
                                                  , partialBilling, netAmount, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Cost Center Code [" + costCenterCode + "] updated successfully.");
                GetCostCenterList();
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }


    #endregion

}
