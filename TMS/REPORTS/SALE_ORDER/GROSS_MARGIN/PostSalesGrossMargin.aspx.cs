using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class REPORTS_SALE_ORDER_GROSS_MARGIN_PostSalesGrossMargin : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsUnit = new DataSet();
    DataSet dsSalesGrossMargin = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string postingMonth = string.Empty;
    string unitName = string.Empty;
    int unitID = 0;
    string customerName = string.Empty;
    string OANo = string.Empty;
    string billNo = string.Empty;
    string companyType = string.Empty;
    string pocNPoc = string.Empty;


    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    string unitNameA35 = string.Empty;
    string unitNameDLH = string.Empty;
    string unitNameSEZ = string.Empty;
    string unitNameGNU = string.Empty;

    int unitIDA35 = 0;
    int unitIDDLH = 0;
    int unitIDSEZ = 0;
    int unitIDGNU = 0;

    int existedRecrdsCount = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                ddlPOCNPOC.SelectedValue = "2";
                Session["dsSaleGrossMargin"] = null;

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

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
        GetUnpostedSaleGrossMargin();
    }

    protected void gvSaleMargin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            DataSet ds = new DataSet();

            int recordID = 0;
            string OANo = string.Empty;
            string customerName = string.Empty;
            string location = string.Empty;
            string month = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");
                Label lblOANo = (Label)e.Row.FindControl("lblOANo");
                Label lblCustomerName = (Label)e.Row.FindControl("lblCustomerName");
                Label lblBillNo = (Label)e.Row.FindControl("lblBillNo");
                Label lblLocation = (Label)e.Row.FindControl("lblLocation");
                Label lblMonth = (Label)e.Row.FindControl("lblMonth");


                e.Row.ToolTip = "OA No.:" + lblOANo.Text + ", Customer Name:" + lblCustomerName.Text + ", Bill No.:" + lblBillNo.Text + ", Location:" + lblLocation.Text + ", Month:" + lblMonth.Text;

                if (!string.IsNullOrEmpty(lblRecordID.Text))
                    recordID = Convert.ToInt32(lblRecordID.Text);
                else
                    recordID = 0;

                if (!string.IsNullOrEmpty(lblOANo.Text))
                    OANo = Convert.ToString(lblOANo.Text);
                else
                    OANo = string.Empty;

                if (!string.IsNullOrEmpty(lblCustomerName.Text))
                    customerName = Convert.ToString(lblCustomerName.Text);
                else
                    customerName = string.Empty;

                if (!string.IsNullOrEmpty(lblLocation.Text))
                    location = Convert.ToString(lblLocation.Text);
                else
                    location = string.Empty;

                if (!string.IsNullOrEmpty(lblMonth.Text))
                    month = Convert.ToString(lblMonth.Text);
                else
                    month = string.Empty;

                if (Session["dsSaleGrossMargin"] != null)
                {
                    ds = (DataSet)Session["dsSaleGrossMargin"];
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (recordID > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Select("RECORD_ID='" + recordID + "' AND OA_NO='" + OANo + "' AND CUSTOMER_NAME='" + customerName + "' AND LOCATION='" + location + "' AND MONTH='" + month + "'"))
                            {

                                existedRecrdsCount++;
                                for (int i = 0; i < e.Row.Cells.Count; i++)
                                {
                                    e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                                }
                            }
                        }
                    }
                }

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

    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (gvSaleMargin.Rows.Count > 0)
        {
            PostSaleGrossMargin();
        }
        else
        {
            ExceptionMessage("No data found...!");
            return;
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

    private void GetUnpostedSaleGrossMargin()
    {
        try
        {
            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (ddlCompany.SelectedIndex > 0)
            {
                unitName = ddlCompany.SelectedItem.Text;
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);
            }
            else
            {
                unitName = string.Empty;
                unitID = 0;
            }

            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                    {
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);
                        unitNameA35 = Convert.ToString(dr["UNIT_NAME"]);
                        unitIDA35 = Convert.ToInt32(dr["UNIT_ID"]);
                    }

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                    {
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);
                        unitNameDLH = Convert.ToString(dr["UNIT_NAME"]);
                        unitIDDLH = Convert.ToInt32(dr["UNIT_ID"]);
                    }

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                    {
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);
                        unitNameSEZ = Convert.ToString(dr["UNIT_NAME"]);
                        unitIDSEZ = Convert.ToInt32(dr["UNIT_ID"]);
                    }

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                    {
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                        unitNameGNU = Convert.ToString(dr["UNIT_NAME"]);
                        unitIDGNU = Convert.ToInt32(dr["UNIT_ID"]);
                    }
                }
            }
            else
            {
                return;
            }

            postingMonth = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");

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

            dsSalesGrossMargin = objReports.GetUnpostedSaleGrossMargin(postingMonth, unitID, unitName, customerName, OANo, billNo, companyType, pocNPoc,
                                                                dbNameA35, unitNameA35, unitIDA35, dbNameDLH, unitNameDLH, unitIDDLH,
                                                                dbNameSEZ, unitNameSEZ, unitIDSEZ, dbNameGNU, unitNameGNU, unitIDGNU);


            if (dsSalesGrossMargin.Tables.Count > 0 && dsSalesGrossMargin.Tables[0].Rows.Count > 0)
            {
                Session["dsSaleGrossMargin"] = dsSalesGrossMargin;
                gvSaleMargin.DataSource = dsSalesGrossMargin.Tables[0];
                gvSaleMargin.DataBind();
                hdExistedRecords.Value = existedRecrdsCount.ToString();
            }
            else
            {
                Session["dsSaleGrossMargin"] = null;
                gvSaleMargin.DataSource = null;
                gvSaleMargin.DataBind();
                hdExistedRecords.Value = "0";
                ExceptionMessage("No data found...!!!");                
            }
            lblRecords.Text = "Records[" + dsSalesGrossMargin.Tables[0].Rows.Count + "], Already Exists[" + existedRecrdsCount + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void PostSaleGrossMargin()
    {
        try
        {
            string updateQuery = string.Empty;
            int recordID = 0;
            string OANo = string.Empty;
            string customerName = string.Empty;
            double warrantyPer = 0;
            double MO = 0;
            string billDate = string.Empty;
            string busClass = string.Empty;
            string currDesc = string.Empty;
            double rate = 0;
            double FCurrency = 0;
            double amountINR = 0;
            string billNo = string.Empty;
            string location = string.Empty;
            string busSegment = string.Empty;
            double grossMarginAsPerOrder = 0;
            double percentage = 0;
            double costAsPerOrder = 0;
            double marginAsPerOrder = 0;
            double costAsPerCostCenter = 0;
            double projectCosts = 0;
            double warranty = 0;
            double actualGrossMargin = 0;
            double standard = 0;
            double actual = 0;
            double highDelta = 0;
            string type = string.Empty;
            string postingMonth = string.Empty;
            string POCNonPOC = string.Empty;
            int SRNo = 0;

            string srNoForRemoval = string.Empty;
            int count = 0;

            DataTable dtSalesGMToPost = new DataTable();
            dtSalesGMToPost.Columns.Add("OA_NO", typeof(string));
            dtSalesGMToPost.Columns.Add("CUSTOMER_NAME", typeof(string));
            dtSalesGMToPost.Columns.Add("WARRANTY_PER", typeof(double));
            dtSalesGMToPost.Columns.Add("MO", typeof(double));
            dtSalesGMToPost.Columns.Add("BILL_DATE", typeof(string));
            dtSalesGMToPost.Columns.Add("CLASS", typeof(string));
            dtSalesGMToPost.Columns.Add("CURR_DESC", typeof(string));
            dtSalesGMToPost.Columns.Add("RATE", typeof(double));
            dtSalesGMToPost.Columns.Add("F_CURRENCY", typeof(double));
            dtSalesGMToPost.Columns.Add("AMOUNT_INR", typeof(double));
            dtSalesGMToPost.Columns.Add("BILL_NO", typeof(string));
            dtSalesGMToPost.Columns.Add("LOCATION", typeof(string));
            dtSalesGMToPost.Columns.Add("BUS_SEGMENT", typeof(string));
            dtSalesGMToPost.Columns.Add("GROSS_MARGIN_AS_PER_ORDER", typeof(double));
            dtSalesGMToPost.Columns.Add("PERCENTAGE", typeof(double));
            dtSalesGMToPost.Columns.Add("COST_AS_PER_ORDER", typeof(double));
            dtSalesGMToPost.Columns.Add("MARGIN_AS_PER_ORDER", typeof(double));
            dtSalesGMToPost.Columns.Add("COST_AS_PER_COST_CENTER", typeof(double));
            dtSalesGMToPost.Columns.Add("PROJECT_COSTS", typeof(double));
            dtSalesGMToPost.Columns.Add("WARRANTY", typeof(double));
            dtSalesGMToPost.Columns.Add("ACTUAL_GROSS_MARGIN", typeof(double));
            dtSalesGMToPost.Columns.Add("STANDARD", typeof(double));
            dtSalesGMToPost.Columns.Add("ACTUAL", typeof(double));
            dtSalesGMToPost.Columns.Add("HIGH_DELTA", typeof(double));
            dtSalesGMToPost.Columns.Add("TYPE", typeof(string));
            dtSalesGMToPost.Columns.Add("MONTH", typeof(string));
            dtSalesGMToPost.Columns.Add("POC_NONPOC", typeof(string));
            dtSalesGMToPost.Columns.Add("CREATED_BY", typeof(Int32));

            int value = 0;
            foreach (GridViewRow gr in gvSaleMargin.Rows)
            {
                DataRow dr = dtSalesGMToPost.NewRow();

                Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                Label lblOANo = (Label)gr.FindControl("lblOANo");
                Label lblCustomerName = (Label)gr.FindControl("lblCustomerName");
                TextBox txtWarrantyPer = (TextBox)gr.FindControl("txtWarrantyPer");
                TextBox txtMO = (TextBox)gr.FindControl("txtMO");
                Label lblBillDate = (Label)gr.FindControl("lblBillDate");
                Label lblClass = (Label)gr.FindControl("lblClass");
                Label lblCurrDesc = (Label)gr.FindControl("lblCurrDesc");
                TextBox txtRate = (TextBox)gr.FindControl("txtRate");
                TextBox txtFCurrency = (TextBox)gr.FindControl("txtFCurrency");
                TextBox txtAmountINR = (TextBox)gr.FindControl("txtAmountINR");
                Label lblBillNo = (Label)gr.FindControl("lblBillNo");
                Label lblLocation = (Label)gr.FindControl("lblLocation");
                Label lblBusSegment = (Label)gr.FindControl("lblBusSegment");
                TextBox txtGrossMarginAsPerOrder = (TextBox)gr.FindControl("txtGrossMarginAsPerOrder");
                TextBox txtPercentage = (TextBox)gr.FindControl("txtPercentage");
                TextBox txtCostAsPerOrder = (TextBox)gr.FindControl("txtCostAsPerOrder");
                TextBox txtMarginAsPerOrder = (TextBox)gr.FindControl("txtMarginAsPerOrder");
                TextBox txtCostAsPerCostCenter = (TextBox)gr.FindControl("txtCostAsPerCostCenter");
                TextBox txtProjectCosts = (TextBox)gr.FindControl("txtProjectCosts");
                TextBox txtWarranty = (TextBox)gr.FindControl("txtWarranty");
                TextBox txtActualGrossMargin = (TextBox)gr.FindControl("txtActualGrossMargin");
                TextBox txtStandard = (TextBox)gr.FindControl("txtStandard");
                TextBox txtActual = (TextBox)gr.FindControl("txtActual");
                TextBox txtHighDelta = (TextBox)gr.FindControl("txtHighDelta");
                Label lblType = (Label)gr.FindControl("lblType");
                Label lblMonth = (Label)gr.FindControl("lblMonth");
                Label lblPOCNONPOC = (Label)gr.FindControl("lblPOCNONPOC");
                Label lblSRNo = (Label)gr.FindControl("lblSRNo");




                if (Convert.ToInt32(lblRecordID.Text) > 0)
                    recordID = Convert.ToInt32(lblRecordID.Text);
                else
                    recordID = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblOANo.Text)))
                    OANo = Convert.ToString(lblOANo.Text);
                else
                    OANo = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerName.Text)))
                    customerName = Convert.ToString(lblCustomerName.Text);
                else
                    customerName = string.Empty;

                if (Convert.ToDouble(txtWarrantyPer.Text) > 0)
                    warrantyPer = Convert.ToDouble(txtWarrantyPer.Text);
                else
                    warrantyPer = 0;

                if (Convert.ToDouble(txtMO.Text) > 0)
                    MO = Convert.ToDouble(txtMO.Text);
                else
                    MO = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblBillDate.Text)))
                    billDate = Convert.ToDateTime(lblBillDate.Text).ToString("yyyy-MM-dd");
                else
                    billDate = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblClass.Text)))
                    busClass = Convert.ToString(lblClass.Text);
                else
                    busClass = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCurrDesc.Text)))
                    currDesc = Convert.ToString(lblCurrDesc.Text);
                else
                    currDesc = string.Empty;

                if (Convert.ToDouble(txtRate.Text) > 0)
                    rate = Convert.ToDouble(txtRate.Text);
                else
                    rate = 0;

                if (Convert.ToDouble(txtFCurrency.Text) > 0)
                    FCurrency = Convert.ToDouble(txtFCurrency.Text);
                else
                    FCurrency = 0;

                if (Convert.ToDouble(txtAmountINR.Text) > 0)
                    amountINR = Convert.ToDouble(txtAmountINR.Text);
                else
                    amountINR = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblBillNo.Text)))
                    billNo = Convert.ToString(lblBillNo.Text);
                else
                    billNo = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblLocation.Text)))
                    location = Convert.ToString(lblLocation.Text);
                else
                    location = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblBusSegment.Text)))
                    busSegment = Convert.ToString(lblBusSegment.Text);
                else
                    busSegment = string.Empty;

                if (Convert.ToDouble(txtGrossMarginAsPerOrder.Text) > 0)
                    grossMarginAsPerOrder = Convert.ToDouble(txtGrossMarginAsPerOrder.Text);
                else
                    grossMarginAsPerOrder = 0;

                if (Convert.ToDouble(txtPercentage.Text) > 0)
                    percentage = Convert.ToDouble(txtPercentage.Text);
                else
                    percentage = 0;

                if (Convert.ToDouble(txtCostAsPerOrder.Text) > 0)
                    costAsPerOrder = Convert.ToDouble(txtCostAsPerOrder.Text);
                else
                    costAsPerOrder = 0;

                if (Convert.ToDouble(txtMarginAsPerOrder.Text) > 0)
                    marginAsPerOrder = Convert.ToDouble(txtMarginAsPerOrder.Text);
                else
                    marginAsPerOrder = 0;

                if (Convert.ToDouble(txtCostAsPerCostCenter.Text) > 0)
                    costAsPerCostCenter = Convert.ToDouble(txtCostAsPerCostCenter.Text);
                else
                    costAsPerCostCenter = 0;

                if (Convert.ToDouble(txtProjectCosts.Text) > 0)
                    projectCosts = Convert.ToDouble(txtProjectCosts.Text);
                else
                    projectCosts = 0;

                if (Convert.ToDouble(txtWarranty.Text) > 0)
                    warranty = Convert.ToDouble(txtWarranty.Text);
                else
                    warranty = 0;

                if (Convert.ToDouble(txtActualGrossMargin.Text) > 0)
                    actualGrossMargin = Convert.ToDouble(txtActualGrossMargin.Text);
                else
                    actualGrossMargin = 0;

                if (Convert.ToDouble(txtStandard.Text) > 0)
                    standard = Convert.ToDouble(txtStandard.Text);
                else
                    standard = 0;

                if (Convert.ToDouble(txtActual.Text) > 0)
                    actual = Convert.ToDouble(txtActual.Text);
                else
                    actual = 0;

                if (Convert.ToDouble(txtHighDelta.Text) > 0)
                    highDelta = Convert.ToDouble(txtHighDelta.Text);
                else
                    highDelta = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblType.Text)))
                    type = Convert.ToString(lblType.Text);
                else
                    type = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblMonth.Text)))
                    postingMonth = Convert.ToDateTime(lblMonth.Text).ToString("yyyy-MM");
                else
                    postingMonth = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblPOCNONPOC.Text)))
                    POCNonPOC = Convert.ToString(lblPOCNONPOC.Text);
                else
                    POCNonPOC = string.Empty;

                SRNo = Convert.ToInt32(lblSRNo.Text);

                if (recordID > 0)
                {
                    count++;
                    srNoForRemoval += SRNo + ",";
                    updateQuery += "UPDATE tblSalesGrossMargin set PROJECT_COSTS ='" + projectCosts + "', ACTUAL_GROSS_MARGIN ='" + actualGrossMargin + "', ACTUAL ='" + actual + "', HIGH_DELTA='" + highDelta + "', MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", MODIFIED_ON= GETDATE() WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(OANo))
                    {
                        count++;
                        srNoForRemoval += SRNo + ",";

                        dr["OA_NO"] = OANo;
                        dr["CUSTOMER_NAME"] = customerName;
                        dr["WARRANTY_PER"] = warrantyPer;
                        dr["MO"] = MO;
                        dr["BILL_DATE"] = billDate;
                        dr["CLASS"] = busClass;
                        dr["CURR_DESC"] = currDesc;
                        dr["RATE"] = rate;
                        dr["F_CURRENCY"] = FCurrency;
                        dr["AMOUNT_INR"] = amountINR;
                        dr["BILL_NO"] = billNo;
                        dr["LOCATION"] = location;
                        dr["BUS_SEGMENT"] = busSegment;
                        dr["GROSS_MARGIN_AS_PER_ORDER"] = grossMarginAsPerOrder;
                        dr["PERCENTAGE"] = percentage;
                        dr["COST_AS_PER_ORDER"] = costAsPerOrder;
                        dr["MARGIN_AS_PER_ORDER"] = marginAsPerOrder;
                        dr["COST_AS_PER_COST_CENTER"] = costAsPerCostCenter;
                        dr["PROJECT_COSTS"] = projectCosts;
                        dr["WARRANTY"] = warranty;
                        dr["ACTUAL_GROSS_MARGIN"] = actualGrossMargin;
                        dr["STANDARD"] = standard;
                        dr["ACTUAL"] = actual;
                        dr["HIGH_DELTA"] = highDelta;
                        dr["TYPE"] = type;
                        dr["MONTH"] = postingMonth;
                        dr["POC_NONPOC"] = POCNonPOC;
                        dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                        dtSalesGMToPost.Rows.Add(dr);
                    }
                }
            }

            if (Convert.ToInt32(hdReplacementFlag.Value) > 0)
            {
                if (!string.IsNullOrEmpty(updateQuery))
                    updateQuery = updateQuery.TrimEnd(';').Trim();
            }
            else
            {
                updateQuery = string.Empty;
            }


            value = objReports.PostSaleGrossMargin(dtSalesGMToPost, updateQuery);

            if (value > 0)
            {
                SuccessMessage(count + " Records Posted successfully.");
                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvSaleMargin.DataSource = null;
                    gvSaleMargin.DataBind();
                    lblRecords.Text = "Records[" + gvSaleMargin.Rows.Count + "]";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveAndBind(string srNoForRemoval)
    {
        try
        {
            dsSalesGrossMargin = (DataSet)Session["dsSaleGrossMargin"];
            string[] strNoForRemoval = srNoForRemoval.Split(',');
            foreach (string item in strNoForRemoval)
            {
                foreach (DataRow dr in dsSalesGrossMargin.Tables[0].Select("SR_NO='" + item + "'"))
                {
                    dsSalesGrossMargin.Tables[0].Rows.Remove(dr);
                }
            }
            if (dsSalesGrossMargin.Tables[0].Rows.Count > 0)
            {
                gvSaleMargin.DataSource = dsSalesGrossMargin.Tables[0];
                gvSaleMargin.DataBind();
            }
            else
            {
                gvSaleMargin.DataSource = null;
                gvSaleMargin.DataBind();
            }
            lblRecords.Text = "Records[" + gvSaleMargin.Rows.Count + "]";
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
