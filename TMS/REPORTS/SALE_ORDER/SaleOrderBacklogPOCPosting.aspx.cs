using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class REPORTS_SALE_ORDER_SaleOrderBacklogPOCPosting : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsTempTableDetails = new DataSet();
    DataSet dsSaleOrder = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string jobNo = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    int unitId = 0;
    string unitName = string.Empty;
    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;
    string unitNameA35 = string.Empty;
    string unitNameDLH = string.Empty;
    string unitNameSEZ = string.Empty;
    string unitNameGNU = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["POC_BILLING"] = null;

                BindUnit();
                Session["DB_DETAILS"] = objCommon.GetDBDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["POC_BILLING"] = null;
        gvSaleOrderPosting.DataSource = null;
        gvSaleOrderPosting.DataBind();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["POC_BILLING"] = null;
        gvSaleOrderPosting.DataSource = null;
        gvSaleOrderPosting.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        GetSalesorderOSReportOne();
    }

    protected void gvSaleOrderPosting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HidePanel();
        PostPOCBilling();
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

    private void GetSalesorderOSReportOne()
    {
        try
        {
            string month1 = string.Empty;
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;


            month1 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text.ToUpper();
            else
                jobNo = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
            {
                unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
                dsSaleOrder = objReports.GetSOBacklogPOCPostingList(unitId, unitName, month1, jobNo);
            }
            else
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        {
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameA35 = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        {
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameDLH = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        {
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameSEZ = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        {
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameGNU = Convert.ToString(dr["UNIT_NAME"]);
                        }
                    }
                }
                dsSaleOrder = objReports.GetSOBacklogPOCPostingListAllUnit(month1, jobNo, dbNameA35, unitNameA35, dbNameDLH, unitNameDLH,
                                                                            dbNameSEZ, unitNameSEZ, dbNameGNU, unitNameGNU);
            }

            if (dsSaleOrder.Tables.Count > 0)
            {
                if (dsSaleOrder.Tables[0].Rows.Count > 0)
                {
                    if (dsSaleOrder.Tables[1].Rows.Count > 0)
                    {
                        Session["POC_BILLING"] = dsSaleOrder.Tables[1];
                        gvSaleOrderPosting.DataSource = dsSaleOrder.Tables[1];
                        gvSaleOrderPosting.DataBind();
                    }
                    else
                    {
                        Session["POC_BILLING"] = null;
                        gvSaleOrderPosting.DataSource = null;
                        gvSaleOrderPosting.DataBind();
                    }
                }
                else
                {
                    Session["POC_BILLING"] = null;
                    gvSaleOrderPosting.DataSource = null;
                    gvSaleOrderPosting.DataBind();

                    ExceptionMessage("Please enter current FX rate of selected month..!");
                    return;
                }
            }
            else
            {
                Session["POC_BILLING"] = null;
                gvSaleOrderPosting.DataSource = null;
                gvSaleOrderPosting.DataBind();
            }

            lblRecords.Text = "Records[" + gvSaleOrderPosting.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void PostPOCBilling()
    {
        try
        {
            bool chk = false;
            int serialNo = 0;
            string serialNos = string.Empty;
            string month = string.Empty;
            string jobNo = string.Empty;
            string orderDate = string.Empty;
            string customerCode = string.Empty;
            string customerName = string.Empty;
            string bu = string.Empty;
            double orderAmount = 0;
            double pocBilling = 0;
            double taxBilling = 0;
            double netBilling = 0;
            string unit = string.Empty;

            int count = 0;


            month = Convert.ToDateTime(Convert.ToInt32(ddlYear.SelectedValue) + "-" + Convert.ToInt32(ddlMonth.SelectedValue)).ToString("yyyy-MM");

            if (gvSaleOrderPosting.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSaleOrderPosting.Rows)
                {
                    chk = true;

                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                    Label lblOrderDate = (Label)gr.FindControl("lblOrderDate");
                    Label lblCustomerCode = (Label)gr.FindControl("lblCustomerCode");
                    Label lblCustomerName = (Label)gr.FindControl("lblCustomerName");
                    Label lblBU = (Label)gr.FindControl("lblBU");
                    Label lblOrderAmount = (Label)gr.FindControl("lblOrderAmount");
                    Label lblPreviousBilling = (Label)gr.FindControl("lblPreviousBilling");
                    TextBox txtPOCBilling = (TextBox)gr.FindControl("txtPOCBilling");
                    TextBox txtTAXBilling = (TextBox)gr.FindControl("txtTAXBilling");
                    TextBox txtNetBilling = (TextBox)gr.FindControl("txtNetBilling");
                    Label lblUnit = (Label)gr.FindControl("lblUnit");

                    serialNo = Convert.ToInt32(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                        jobNo = Convert.ToString(lblJobNo.Text);
                    else
                        jobNo = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblOrderDate.Text)))
                        orderDate = Convert.ToDateTime(lblOrderDate.Text).ToString("yyyy-MM-dd");
                    else
                        orderDate = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerCode.Text)))
                        customerCode = Convert.ToString(lblCustomerCode.Text);
                    else
                        customerCode = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerName.Text)))
                        customerName = Convert.ToString(lblCustomerName.Text);
                    else
                        customerName = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblBU.Text)))
                        bu = Convert.ToString(lblBU.Text);
                    else
                        bu = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblOrderAmount.Text)))
                        orderAmount = Convert.ToDouble(lblOrderAmount.Text);
                    else
                        orderAmount = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtPOCBilling.Text)))
                        pocBilling = Convert.ToDouble(txtPOCBilling.Text);
                    else
                        pocBilling = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtTAXBilling.Text)))
                        taxBilling = Convert.ToDouble(txtTAXBilling.Text);
                    else
                        taxBilling = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtNetBilling.Text)))
                    {

                        if (Convert.ToDouble(txtNetBilling.Text) < 0)
                        {
                            chk = false;
                            ExceptionMessage("Net billin is negative..!");
                        }
                        else
                            netBilling = Convert.ToDouble(txtNetBilling.Text);
                    }
                    else
                        netBilling = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblUnit.Text)))
                        unit = Convert.ToString(lblUnit.Text);
                    else
                        unit = string.Empty;

                    if (chk)
                    {
                        int value = objReports.PostPOCBilling(jobNo, orderDate, customerCode, customerName, bu, orderAmount,
                                                        pocBilling, taxBilling, netBilling, unit, month, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        if (value > 0)
                        {
                            count++;
                            serialNos += serialNo + ",";
                        }
                    }
                }

                if (count > 0)
                {
                    serialNos = serialNos.TrimEnd(',');
                    if (!string.IsNullOrEmpty(serialNos))
                    {
                        RemoveRecords((DataTable)Session["POC_BILLING"], serialNos);
                    }
                    SuccessMessage(count + " Records posted successfully..!");
                }
            }
            else
            {
                ExceptionMessage("No data found..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRecords(DataTable dtTextChanged, string serialNos)
    {
        string[] strSerialNo = serialNos.Split(',');
        if (dtTextChanged.Rows.Count > 0)
        {
            foreach (string sr in strSerialNo)
            {
                if (!string.IsNullOrEmpty(sr))
                {
                    foreach (DataRow drremove in dtTextChanged.Select("SERIAL_NO='" + sr + "'"))
                    {
                        dtTextChanged.Rows.Remove(drremove);
                    }
                }
            }

            if (dtTextChanged.Rows.Count > 0)
            {
                Session["POC_BILLING"] = dtTextChanged;
                gvSaleOrderPosting.DataSource = dtTextChanged;
                gvSaleOrderPosting.DataBind();
            }
            else
            {
                Session["POC_BILLING"] = null;
                gvSaleOrderPosting.DataSource = null;
                gvSaleOrderPosting.DataBind();
            }
        }
        else
        {
            Session["POC_BILLING"] = null;
        }
        lblRecords.Text = "Records[" + gvSaleOrderPosting.Rows.Count + "]";
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

    private void EmptyGridview()
    {
        gvSaleOrderPosting.DataSource = null;
        gvSaleOrderPosting.DataBind();
        lblRecords.Text = "Records[" + gvSaleOrderPosting.Rows.Count + "]";
    }

    #endregion    

}