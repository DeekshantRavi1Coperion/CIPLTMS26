using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI.HtmlControls;

public partial class FINOPS_AddFinOps : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.FinOps objFinOps = new BAL.FinOps();

    DataSet dsType = new DataSet();
    DataSet dsCostType = new DataSet();
    DataSet dsContigencyType = new DataSet();
    DataSet dsStatus1 = new DataSet();
    DataSet dsStatus2 = new DataSet();
    
    string taxInvoiceNo = string.Empty;
    string taxInvoiceDate = string.Empty;
    string month = string.Empty;
    string jobNo = string.Empty;
    string costCenter = string.Empty;
    string BU = string.Empty;
    double amount1 = 0;
    string hours = string.Empty;
    string paneltyClause = string.Empty;
    string paymentTerm = string.Empty;
    string poNo = string.Empty;
    int isVPOCOrder = 0;
    string deliveryMonth = string.Empty;
    double amount2 = 0;
    string itemCode = string.Empty;
    string itemDesc = string.Empty;
    string itemGroup = string.Empty;
    string itemSubgroup = string.Empty;
    string itemPivotGroup = string.Empty;
    int typeID = 0;
    int costTypeID = 0;
    int contigencyTypeID = 0;
    int status1ID = 0;
    int status2ID = 0;
   
    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                hdConfirmValue.Value = "0";

                Session["EMP_DETAIL"] = null;

                hdInvoiceDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtMonth.Text = DateTime.Now.ToString("MM/yyyy");
                txtDeliveryMonth.Text = DateTime.Now.ToString("MM/yyyy");

                BindType();
                BindCostType();
                BindContigencyType();
                BindStatus1();
                BindStatus2();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            InsertFinOps();
        }
    }

    protected void btnFinOpsList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FINOPS/FinOpsList.aspx");
    }

    #endregion


    #region METHODS[============================]

    private void BindType()
    {
        try
        {
            dsType = objFinOps.GetFinOpsTypeList("");
            if (dsType.Tables.Count > 0 && dsType.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = dsType.Tables[0];
                ddlType.DataTextField = "TYPE";
                ddlType.DataValueField = "TYPE_ID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, "Select");
                ddlType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCostType()
    {
        try
        {
            dsCostType = objFinOps.GetOpsCostTypeList("");
            if (dsCostType.Tables.Count > 0 && dsCostType.Tables[0].Rows.Count > 0)
            {
                ddlCostType.DataSource = dsCostType.Tables[0];
                ddlCostType.DataTextField = "COST_TYPE";
                ddlCostType.DataValueField = "COST_TYPE_ID";
                ddlCostType.DataBind();
                ddlCostType.Items.Insert(0, "Select");
                ddlCostType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindContigencyType()
    {
        try
        {
            dsContigencyType = objFinOps.GetOpsContigencyTypeList("");
            if (dsContigencyType.Tables.Count > 0 && dsContigencyType.Tables[0].Rows.Count > 0)
            {
                ddlContigencyType.DataSource = dsContigencyType.Tables[0];
                ddlContigencyType.DataTextField = "CONTIGENCY_TYPE";
                ddlContigencyType.DataValueField = "CONTIGENCY_TYPE_ID";
                ddlContigencyType.DataBind();
                ddlContigencyType.Items.Insert(0, "Select");
                ddlContigencyType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatus1()
    {
        try
        {
            dsStatus1 = objFinOps.GetFinOpsStatus1List("");
            if (dsStatus1.Tables.Count > 0 && dsStatus1.Tables[0].Rows.Count > 0)
            {
                ddlStatus1.DataSource = dsStatus1.Tables[0];
                ddlStatus1.DataTextField = "STATUS1";
                ddlStatus1.DataValueField = "STATUS1_ID";
                ddlStatus1.DataBind();
                ddlStatus1.Items.Insert(0, "Select");
                ddlStatus1.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatus2()
    {
        try
        {
            dsStatus2 = objFinOps.GetFinOpsStatus2List("");
            if (dsStatus2.Tables.Count > 0 && dsStatus2.Tables[0].Rows.Count > 0)
            {
                ddlStatus2.DataSource = dsStatus2.Tables[0];
                ddlStatus2.DataTextField = "STATUS2";
                ddlStatus2.DataValueField = "STATUS2_ID";
                ddlStatus2.DataBind();
                ddlStatus2.Items.Insert(0, "Select");
                ddlStatus2.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void InsertFinOps()
    {
        try
        {
            DataTable dtTempFinOps = new DataTable();

            dtTempFinOps.Columns.Add("TAX_INVOICE_NO", typeof(string));
            dtTempFinOps.Columns.Add("TAX_INVOICE_DATE", typeof(string));
            dtTempFinOps.Columns.Add("MONTH", typeof(string));
            dtTempFinOps.Columns.Add("JOB_NO", typeof(string));
            dtTempFinOps.Columns.Add("COST_CENTER", typeof(string));
            dtTempFinOps.Columns.Add("BU", typeof(string));
            dtTempFinOps.Columns.Add("AMOUNT1", typeof(string));
            dtTempFinOps.Columns.Add("HOURS", typeof(string));
            dtTempFinOps.Columns.Add("PANELTY_CLAUSE", typeof(string));
            dtTempFinOps.Columns.Add("PAYMENT_TERM", typeof(string));
            dtTempFinOps.Columns.Add("PO_NO", typeof(string));
            dtTempFinOps.Columns.Add("IS_VPOC_ORDER", typeof(int));
            dtTempFinOps.Columns.Add("DELIVERY_MONTH", typeof(string));
            dtTempFinOps.Columns.Add("AMOUNT2", typeof(double));
            dtTempFinOps.Columns.Add("ITEM_CODE", typeof(string));
            dtTempFinOps.Columns.Add("ITEM_DESC", typeof(string));
            dtTempFinOps.Columns.Add("ITEM_GROUP", typeof(string));
            dtTempFinOps.Columns.Add("ITEM_SUBGROUP", typeof(string));
            dtTempFinOps.Columns.Add("ITEM_PIVOT_GROUP", typeof(string));

            dtTempFinOps.Columns.Add("TYPE_ID", typeof(int));
            dtTempFinOps.Columns.Add("COST_TYPE_ID", typeof(int));
            dtTempFinOps.Columns.Add("CONTIGENCY_TYPE_ID", typeof(int));

            dtTempFinOps.Columns.Add("STATUS1_ID", typeof(int));
            dtTempFinOps.Columns.Add("STATUS2_ID", typeof(int));

            dtTempFinOps.Columns.Add("CREATED_BY", typeof(int));

            int value = 0;

            taxInvoiceNo = string.Empty;
            taxInvoiceDate = string.Empty;
            month = string.Empty;
            jobNo = string.Empty;
            costCenter = string.Empty;
            BU = string.Empty;
            amount1 = 0;
            hours = string.Empty;
            paneltyClause = string.Empty;
            paymentTerm = string.Empty;
            poNo = string.Empty;
            isVPOCOrder = 0;
            deliveryMonth = string.Empty;
            amount2 = 0;
            itemCode = string.Empty;
            itemDesc = string.Empty;
            itemGroup = string.Empty;
            itemSubgroup = string.Empty;
            itemPivotGroup = string.Empty;
            typeID = 0;
            costTypeID = 0;
            contigencyTypeID = 0;
            status1ID = 0;
            status2ID = 0;
                      
            DataRow dr = dtTempFinOps.NewRow();

            if (!string.IsNullOrEmpty(Convert.ToString(txtInvoiceNo.Text)))
                taxInvoiceNo = Convert.ToString(txtInvoiceNo.Text);

            taxInvoiceDate = Convert.ToDateTime(hdInvoiceDate.Value).ToString("yyyy-MM-dd");

            month = Convert.ToDateTime(txtMonth.Text).ToString("yyyy-MM");

            if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNo.Text)))
                jobNo = Convert.ToString(txtJOBNo.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtCostCenter.Text)))
                costCenter = Convert.ToString(txtCostCenter.Text);

            if (ddlType.SelectedIndex > 0)
                typeID = Convert.ToInt32(ddlType.SelectedValue);

            if (ddlCostType.SelectedIndex > 0)
                costTypeID = Convert.ToInt32(ddlCostType.SelectedValue);

            if (!string.IsNullOrEmpty(Convert.ToString(txtBU.Text)))
                BU = Convert.ToString(txtBU.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtAmount1.Text)))
                amount1 = Convert.ToDouble(txtAmount1.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtHours.Text)))
                hours = Convert.ToString(txtHours.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtPaneltyClause.Text)))
                paneltyClause = Convert.ToString(txtPaneltyClause.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtPaymentTerm.Text)))
                paymentTerm = Convert.ToString(txtPaymentTerm.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtPONo.Text)))
                poNo = Convert.ToString(txtPONo.Text);

            if (ddlContigencyType.SelectedIndex > 0)
                contigencyTypeID = Convert.ToInt32(ddlContigencyType.SelectedValue);

            if (ddlIsVPOCOrder.SelectedIndex > 0)
            {
                if (ddlIsVPOCOrder.SelectedItem.Text.ToUpper() == "YES")
                    isVPOCOrder = 1;
                else if (ddlIsVPOCOrder.SelectedItem.Text.ToUpper() == "NO")
                    isVPOCOrder = 0;
            }

            deliveryMonth = Convert.ToDateTime(txtDeliveryMonth.Text).ToString("yyyy-MM");

            if (ddlStatus1.SelectedIndex > 0)
                status1ID = Convert.ToInt32(ddlStatus1.SelectedValue);

            if (!string.IsNullOrEmpty(Convert.ToString(txtAmount2.Text)))
                amount2 = Convert.ToDouble(txtAmount2.Text);

            if (ddlStatus2.SelectedIndex > 0)
                status2ID = Convert.ToInt32(ddlStatus2.SelectedValue);

            if (!string.IsNullOrEmpty(Convert.ToString(txtItemCode.Text)))
                itemCode = Convert.ToString(txtItemCode.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtItemDesc.Text)))
                itemDesc = Convert.ToString(txtItemDesc.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtItemGroup.Text)))
                itemGroup = Convert.ToString(txtItemGroup.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtItemSubgroup.Text)))
                itemSubgroup = Convert.ToString(txtItemSubgroup.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtItemPivotGroup.Text)))
                itemPivotGroup = Convert.ToString(txtItemPivotGroup.Text);



            if (!string.IsNullOrEmpty(jobNo))
            {
                dr["TAX_INVOICE_NO"] = taxInvoiceNo;
                dr["TAX_INVOICE_DATE"] = taxInvoiceDate;
                dr["MONTH"] = month;
                dr["JOB_NO"] = jobNo;
                dr["COST_CENTER"] = costCenter;
                dr["BU"] = BU;
                dr["AMOUNT1"] = amount1;
                dr["HOURS"] = hours;
                dr["PANELTY_CLAUSE"] = paneltyClause;
                dr["PAYMENT_TERM"] = paymentTerm;
                dr["PO_NO"] = poNo;
                dr["IS_VPOC_ORDER"] = isVPOCOrder;
                dr["DELIVERY_MONTH"] = deliveryMonth;
                dr["AMOUNT2"] = amount2;
                dr["ITEM_CODE"] = itemCode;
                dr["ITEM_DESC"] = itemDesc;
                dr["ITEM_GROUP"] = itemGroup;
                dr["ITEM_SUBGROUP"] = itemSubgroup;
                dr["ITEM_PIVOT_GROUP"] = itemPivotGroup;
                dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                dr["TYPE_ID"] = typeID;
                dr["COST_TYPE_ID"] = costTypeID;
                dr["CONTIGENCY_TYPE_ID"] = contigencyTypeID;
                dr["STATUS1_ID"] = status1ID;
                dr["STATUS2_ID"] = status2ID;

                dtTempFinOps.Rows.Add(dr);
            }

            value = objFinOps.InsertFinOps(dtTempFinOps, "");

            if (value > 0)
            {
                SuccessMessage("Fin Ops added successfully.");
                Reset();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void Reset()
    {
        txtInvoiceNo.Text = string.Empty;

        hdInvoiceDate.Value = string.Empty;
        txtInvoiceDate.Text = string.Empty;
        txtMonth.Text = string.Empty;
        txtJOBNo.Text = string.Empty;
        txtCostCenter.Text = string.Empty;
        txtBU.Text = string.Empty;
        txtAmount1.Text = string.Empty;
        txtHours.Text = string.Empty;
        txtPaneltyClause.Text = string.Empty;
        txtPaymentTerm.Text = string.Empty;
        txtPONo.Text = string.Empty;
        ddlIsVPOCOrder.SelectedIndex = 0;
        txtDeliveryMonth.Text = string.Empty;
        txtAmount2.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        txtItemDesc.Text = string.Empty;
        txtItemGroup.Text = string.Empty;
        txtItemSubgroup.Text = string.Empty;
        txtItemPivotGroup.Text = string.Empty;
        ddlType.SelectedIndex = 0;
        ddlCostType.SelectedIndex = 0;
        ddlContigencyType.SelectedIndex = 0;
        ddlStatus1.SelectedIndex = 0;
        ddlStatus2.SelectedIndex = 0;
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