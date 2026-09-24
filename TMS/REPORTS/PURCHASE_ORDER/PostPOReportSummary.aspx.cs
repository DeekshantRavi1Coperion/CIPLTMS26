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

public partial class REPORTS_PURCHASE_ORDER_PostPOReportSummary : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Project objProject = new BAL.Project();
    BAL.Purchase objPurchase = new BAL.Purchase();
    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsFactPivotGroupList = new DataSet();
    DataSet dsPivotGroup = new DataSet();
    DataSet dsPOReport = new DataSet();
    DataSet dsUnit = new DataSet();
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string poNo = string.Empty;
    string vendorName = string.Empty;
    string JOBNo = string.Empty;
    double amount = 0;
    string status = string.Empty;
    string unitName = string.Empty;
    int excludeCIDF = 0;

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
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        chkSelectAll.Checked = false;
        lblTotalAmount.Text = "0";
        HidePanel();
        GetPOReportSummery();
    }

    double totalAmount = 0;

    protected void gvPOReportSummery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPONo = (Label)e.Row.FindControl("lblPONo");
                Label lblPOValueINR = (Label)e.Row.FindControl("lblPOValueINR");
                Label lblExistingPoValueINR = (Label)e.Row.FindControl("lblExistingPoValueINR");
                Label lblPurchaseID = (Label)e.Row.FindControl("lblPurchaseID");
                Label lblIsOrderSubjectTo = (Label)e.Row.FindControl("lblIsOrderSubjectTo");
                Label lblSalesPivotGroup = (Label)e.Row.FindControl("lblSalesPivotGroup");

                DropDownList ddlIsOrderSubjectTo = (DropDownList)e.Row.FindControl("ddlIsOrderSubjectTo");
                DropDownList ddlSalesPivotGroup = (DropDownList)e.Row.FindControl("ddlSalesPivotGroup");

                totalAmount += Convert.ToDouble(lblPOValueINR.Text);
                lblTotalAmount.Text = Convert.ToString(totalAmount);
                lblTotalAmount.ForeColor = System.Drawing.Color.Green;

                if (!string.IsNullOrEmpty(lblIsOrderSubjectTo.Text))
                    ddlIsOrderSubjectTo.SelectedValue = Convert.ToString(lblIsOrderSubjectTo.Text);
                else
                    ddlIsOrderSubjectTo.SelectedValue = "0";


                dsPivotGroup = objProject.GetDetailsBySP("sp_get_pivot_group");
                if (dsPivotGroup.Tables.Count > 0 && dsPivotGroup.Tables[0].Rows.Count > 0)
                {
                    ddlSalesPivotGroup.DataSource = dsPivotGroup.Tables[0];
                    ddlSalesPivotGroup.DataTextField = "PIVOT_GROUP";
                    ddlSalesPivotGroup.DataValueField = "PIVOT_GROUP_ID";
                    ddlSalesPivotGroup.DataBind();
                    ddlSalesPivotGroup.Items.Insert(0, "SELECT");
                    ddlSalesPivotGroup.SelectedIndex = 0;

                    if (dsFactPivotGroupList.Tables.Count > 0 && dsFactPivotGroupList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsFactPivotGroupList.Tables[0].Select("PO_NO='" + Convert.ToString(lblPONo.Text) + "'"))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dr["PIVOT_GROUP_ID"])) && Convert.ToInt32(dr["PIVOT_GROUP_ID"]) > 0 && dr["PIVOT_GROUP_ID"] != DBNull.Value)
                                ddlSalesPivotGroup.SelectedValue = Convert.ToString(dr["PIVOT_GROUP_ID"]);
                            else
                                ddlSalesPivotGroup.SelectedValue = "0";
                        }
                    }
                    else
                        ddlSalesPivotGroup.SelectedValue = "0";

                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (!string.IsNullOrEmpty(lblPurchaseID.Text) && Convert.ToInt32(lblPurchaseID.Text) > 0)
                    {
                        e.Row.BackColor = System.Drawing.Color.PowderBlue;
                        e.Row.Enabled = false;
                        if (Convert.ToDouble(lblPOValueINR.Text) != Convert.ToDouble(lblExistingPoValueINR.Text))
                        {
                            e.Row.BackColor = System.Drawing.Color.Plum;
                            e.Row.Enabled = true;
                        }
                    }
                }
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

    protected void btnPost_Click(object sender, EventArgs e)
    {
        HidePanel();
        PostPurchaseOrder();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        HidePanel();
        UpdatePurchase();
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        HidePanel();
        if (gvPOReportSummery.Rows.Count > 0)
        {
            if (chkSelectAll.Checked)
            {
                foreach (GridViewRow gr in gvPOReportSummery.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    ImageButton imgbtnLastDeliveryDate = (ImageButton)gr.FindControl("imgbtnLastDeliveryDate");
                    chkSelect.Checked = true;
                    imgbtnLastDeliveryDate.Enabled = true;
                }
            }
            if (!chkSelectAll.Checked)
            {
                foreach (GridViewRow gr in gvPOReportSummery.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    ImageButton imgbtnLastDeliveryDate = (ImageButton)gr.FindControl("imgbtnLastDeliveryDate");
                    chkSelect.Checked = false;
                    imgbtnLastDeliveryDate.Enabled = false;
                }
            }
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


            dsFactPivotGroupList = objPurchase.GetFactPivotGroupListForPosting(unitName);

            dsPOReport = objPurchase.GetPOReportSummeryToPost(fromDate, toDate, poNo, vendorName, unitName, status, JOBNo, amount, excludeCIDF);

            if (dsPOReport.Tables.Count > 0 && dsPOReport.Tables[0].Rows.Count > 0)
            {
                Session["PO_REPORT"] = dsPOReport.Tables[0];
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

    private void PostPurchaseOrder()
    {
        try
        {
            bool chk = false;
            int serialNo = 0;
            string poNo = string.Empty;
            string poDate = string.Empty;
            string vendorCode = string.Empty;
            string vendorName = string.Empty;

            double poValueINR = 0;
            double fraightOrCustomLoading = 0;
            double totalPOValueINR = 0;

            string docClass = string.Empty;
            string poStatus = string.Empty;
            string location = string.Empty;

            string lastDeliveryDate = string.Empty;
            string expectedDeliveryDate = string.Empty;
            string isOrderSubjectTo = string.Empty;
            int noOfMonthsToDeliver = 0;
            int noOfMonthsBasedUpon = 0;
            string paymentTerm = string.Empty;
            string likelyDateOfReceiptOfMaterial = string.Empty;
            int salesPivotGroup = 0;

            int count = 0;
            if (gvPOReportSummery.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvPOReportSummery.Rows)
                {
                    chk = true;

                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblPONo = (Label)gr.FindControl("lblPONo");
                    Label lblPODate = (Label)gr.FindControl("lblPODate");
                    Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                    Label lblVendorName = (Label)gr.FindControl("lblVendorName");
                    Label lblPOValueINR = (Label)gr.FindControl("lblPOValueINR");
                    TextBox txtFraightOrCustomLoading = (TextBox)gr.FindControl("txtFraightOrCustomLoading");
                    TextBox txtTotalPOValueINR = (TextBox)gr.FindControl("txtTotalPOValueINR");
                    Label lblDocClass = (Label)gr.FindControl("lblDocClass");
                    Label lblPOStatus = (Label)gr.FindControl("lblPOStatus");
                    Label lblLocation = (Label)gr.FindControl("lblLocation");
                    TextBox txtLastDeliveryDate = (TextBox)gr.FindControl("txtLastDeliveryDate");
                    TextBox txtExpectedDeliveryDate = (TextBox)gr.FindControl("txtExpectedDeliveryDate");
                    TextBox txtNoOfMonthsToDeliver = (TextBox)gr.FindControl("txtNoOfMonthsToDeliver");
                    TextBox txtNoOfMonthsBasedUpon = (TextBox)gr.FindControl("txtNoOfMonthsBasedUpon");
                    TextBox txtPaymentTerm = (TextBox)gr.FindControl("txtPaymentTerm");
                    DropDownList ddlIsOrderSubjectTo = (DropDownList)gr.FindControl("ddlIsOrderSubjectTo");
                    DropDownList ddlSalesPivotGroup = (DropDownList)gr.FindControl("ddlSalesPivotGroup");



                    serialNo = Convert.ToInt32(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPONo.Text)))
                        poNo = Convert.ToString(lblPONo.Text);
                    else
                        poNo = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPODate.Text)))
                        poDate = Convert.ToDateTime(lblPODate.Text).ToString("yyyy-MM-dd");
                    else
                        poDate = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblVendorCode.Text)))
                        vendorCode = Convert.ToString(lblVendorCode.Text);
                    else
                        vendorCode = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblVendorName.Text)))
                        vendorName = Convert.ToString(lblVendorName.Text);
                    else
                        vendorName = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPOValueINR.Text)))
                        poValueINR = Convert.ToDouble(lblPOValueINR.Text);
                    else
                        poValueINR = 0;


                    if (!string.IsNullOrEmpty(Convert.ToString(txtFraightOrCustomLoading.Text)))
                        fraightOrCustomLoading = Convert.ToDouble(txtFraightOrCustomLoading.Text);
                    else
                        fraightOrCustomLoading = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtTotalPOValueINR.Text)))
                        totalPOValueINR = Convert.ToDouble(txtTotalPOValueINR.Text);
                    else
                        totalPOValueINR = 0;


                    if (!string.IsNullOrEmpty(Convert.ToString(lblDocClass.Text)))
                        docClass = Convert.ToString(lblDocClass.Text);
                    else
                        docClass = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPOStatus.Text)))
                        poStatus = Convert.ToString(lblPOStatus.Text);
                    else
                        poStatus = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblLocation.Text)))
                        location = Convert.ToString(lblLocation.Text);
                    else
                        location = string.Empty;


                    lastDeliveryDate = Convert.ToDateTime(txtLastDeliveryDate.Text).ToString("yyyy-MM-dd");

                    if (!string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
                        expectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text).ToString("yyyy-MM-dd");
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter expected delivery date..!");
                        return;
                    }

                    if (Convert.ToDateTime(lastDeliveryDate) > Convert.ToDateTime(expectedDeliveryDate))
                    {
                        chk = false;
                        ExceptionMessage("Invalid date range..!");
                        return;
                    }


                    if (!string.IsNullOrEmpty(Convert.ToString(txtNoOfMonthsToDeliver.Text)))
                        noOfMonthsToDeliver = Convert.ToInt32(txtNoOfMonthsToDeliver.Text);
                    else
                        noOfMonthsToDeliver = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtNoOfMonthsBasedUpon.Text)))
                        noOfMonthsBasedUpon = Convert.ToInt32(txtNoOfMonthsBasedUpon.Text);
                    else
                        noOfMonthsBasedUpon = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtPaymentTerm.Text)))
                        paymentTerm = Convert.ToString(txtPaymentTerm.Text);
                    else
                        paymentTerm = string.Empty;


                    if (ddlIsOrderSubjectTo.SelectedIndex > 0)
                        isOrderSubjectTo = Convert.ToString(ddlIsOrderSubjectTo.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select is order subject to.!");
                        return;
                    }

                    if (ddlSalesPivotGroup.SelectedIndex > 0)
                        salesPivotGroup = Convert.ToInt32(ddlSalesPivotGroup.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select sales pivot group.!");
                        return;
                    }

                    if (chk)
                    {
                        int value = objPurchase.PostPurchaseOrderNew(poNo, poDate, vendorCode, vendorName, poValueINR, fraightOrCustomLoading,
                                                                    totalPOValueINR, docClass, poStatus, location, lastDeliveryDate,
                                                                    expectedDeliveryDate, noOfMonthsToDeliver, noOfMonthsBasedUpon, paymentTerm,
                                                                    isOrderSubjectTo, salesPivotGroup, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        if (value > 0)
                        {
                            count++;
                            RemoveRecord(serialNo);
                        }
                    }
                }

                if (Session["PO_REPORT"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["PO_REPORT"];
                    gvPOReportSummery.DataSource = dt;
                    gvPOReportSummery.DataBind();

                    SuccessMessage(count + " Records imported. Rest " + gvPOReportSummery.Rows.Count + " Records already existed with different PO Value INR, would you like to update..?");
                }
                else
                {
                    SuccessMessage("All records imported successfully..!");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdatePurchase()
    {
        try
        {
            int serialNo = 0;
            int purchaseID = 0;
            double newPoValueINR = 0;
            string newLastDeliverDate = string.Empty;
            string PONO = string.Empty;
            string vendorCode = string.Empty;
            string docClass = string.Empty;
            string POStatus = string.Empty;
            int count = 0;
            if (gvPOReportSummery.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvPOReportSummery.Rows)
                {
                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblPurchaseID = (Label)gr.FindControl("lblPurchaseID");
                    Label lblPOValueINR = (Label)gr.FindControl("lblPOValueINR");
                    TextBox txtLastDeliveryDate = (TextBox)gr.FindControl("txtLastDeliveryDate");
                    Label lblPONo = (Label)gr.FindControl("lblPONo");
                    Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                    Label lblDocClass = (Label)gr.FindControl("lblDocClass");
                    Label lblPOStatus = (Label)gr.FindControl("lblPOStatus");
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");

                    if (chkSelect.Checked)
                    {
                        serialNo = Convert.ToInt32(lblSerialNo.Text);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPurchaseID.Text)) && Convert.ToInt32(lblPurchaseID.Text) > 0)
                            purchaseID = Convert.ToInt32(lblPurchaseID.Text);
                        else
                            purchaseID = 0;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPOValueINR.Text)))
                            newPoValueINR = Convert.ToDouble(lblPOValueINR.Text);
                        else
                            newPoValueINR = 0;

                        if (!string.IsNullOrEmpty(Convert.ToString(txtLastDeliveryDate.Text)))
                            newLastDeliverDate = Convert.ToDateTime(txtLastDeliveryDate.Text).ToString("yyyy-MM-dd");
                        else
                            newLastDeliverDate = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPONo.Text)))
                            PONO = Convert.ToString(lblPONo.Text);
                        else
                            PONO = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblVendorCode.Text)))
                            vendorCode = Convert.ToString(lblVendorCode.Text);
                        else
                            vendorCode = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblDocClass.Text)))
                            docClass = Convert.ToString(lblDocClass.Text);
                        else
                            docClass = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPOStatus.Text)))
                            POStatus = Convert.ToString(lblPOStatus.Text);
                        else
                            POStatus = string.Empty;

                        if (purchaseID > 0)
                        {
                            int value = objPurchase.UpdatePurchase(purchaseID, newPoValueINR, newLastDeliverDate, PONO, vendorCode, docClass, POStatus, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                            if (value > 0)
                            {
                                count++;
                                RemoveRecord(serialNo);
                            }
                        }
                    }
                }

                if (Session["PO_REPORT"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["PO_REPORT"];
                    gvPOReportSummery.DataSource = dt;
                    gvPOReportSummery.DataBind();
                }
                SuccessMessage(count + " Records updated successfully..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRecord(int serialNo)
    {
        try
        {
            HidePanel();

            DataTable dtNew = new DataTable();

            dtNew.Columns.Add("SERIAL_NO", typeof(string));
            dtNew.Columns.Add("PURCHASE_ID", typeof(string));
            dtNew.Columns.Add("EXISTING_PO_VALUE_INR", typeof(string));
            dtNew.Columns.Add("PO_NO", typeof(string));
            dtNew.Columns.Add("PO_DATE", typeof(string));
            dtNew.Columns.Add("VENDOR_CODE", typeof(string));
            dtNew.Columns.Add("VENDOR_NAME", typeof(string));
            dtNew.Columns.Add("PO_VALUE_INR", typeof(string));
            dtNew.Columns.Add("FRAIGHT_OR_CUSTOM_LOADING_IN_PERCENTAGE", typeof(string));
            dtNew.Columns.Add("TOTAL_PO_VALUE_INR", typeof(string));
            dtNew.Columns.Add("DOC_CLASS", typeof(string));
            dtNew.Columns.Add("PO_STATUS", typeof(string));
            dtNew.Columns.Add("LOCATION", typeof(string));
            dtNew.Columns.Add("LAST_DELIVERY_DATE", typeof(string));
            dtNew.Columns.Add("EXPECTED_DELIVERY_DATE", typeof(string));
            dtNew.Columns.Add("NO_OF_MONTHS_TO_DELIVER", typeof(string));
            dtNew.Columns.Add("NO_OF_MONTHS_BASED_UPON", typeof(string));
            dtNew.Columns.Add("PAYMENT_TERM", typeof(string));
            dtNew.Columns.Add("IS_ORDER_SUBJECT_TO", typeof(string));
            dtNew.Columns.Add("SALES_PIVOT_GROUP", typeof(string));

            foreach (GridViewRow gr in gvPOReportSummery.Rows)
            {
                DataRow dr = dtNew.NewRow();
                Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                Label lblPurchaseID = (Label)gr.FindControl("lblPurchaseID");
                Label lblExistingPoValueINR = (Label)gr.FindControl("lblExistingPoValueINR");
                Label lblPONo = (Label)gr.FindControl("lblPONo");
                Label lblPODate = (Label)gr.FindControl("lblPODate");
                Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                Label lblVendorName = (Label)gr.FindControl("lblVendorName");
                Label lblPOValueINR = (Label)gr.FindControl("lblPOValueINR");
                TextBox txtFraightOrCustomLoading = (TextBox)gr.FindControl("txtFraightOrCustomLoading");
                TextBox txtTotalPOValueINR = (TextBox)gr.FindControl("txtTotalPOValueINR");
                Label lblDocClass = (Label)gr.FindControl("lblDocClass");
                Label lblPOStatus = (Label)gr.FindControl("lblPOStatus");
                Label lblLocation = (Label)gr.FindControl("lblLocation");
                TextBox txtLastDeliveryDate = (TextBox)gr.FindControl("txtLastDeliveryDate");

                TextBox txtExpectedDeliveryDate = (TextBox)gr.FindControl("txtExpectedDeliveryDate");
                TextBox txtNoOfMonthsToDeliver = (TextBox)gr.FindControl("txtNoOfMonthsToDeliver");
                TextBox txtNoOfMonthsBasedUpon = (TextBox)gr.FindControl("txtNoOfMonthsBasedUpon");
                TextBox txtPaymentTerm = (TextBox)gr.FindControl("txtPaymentTerm");
                DropDownList ddlIsOrderSubjectTo = (DropDownList)gr.FindControl("ddlIsOrderSubjectTo");
                DropDownList ddlSalesPivotGroup = (DropDownList)gr.FindControl("ddlSalesPivotGroup");



                if (!string.IsNullOrEmpty(lblSerialNo.Text))
                    dr["SERIAL_NO"] = lblSerialNo.Text;
                else
                    dr["SERIAL_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblPurchaseID.Text))
                    dr["PURCHASE_ID"] = lblPurchaseID.Text;
                else
                    dr["PURCHASE_ID"] = "0";

                if (!string.IsNullOrEmpty(lblExistingPoValueINR.Text))
                    dr["EXISTING_PO_VALUE_INR"] = lblExistingPoValueINR.Text;
                else
                    dr["EXISTING_PO_VALUE_INR"] = "0";

                if (!string.IsNullOrEmpty(lblPONo.Text))
                    dr["PO_NO"] = lblPONo.Text;
                else
                    dr["PO_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblPODate.Text))
                    dr["PO_DATE"] = Convert.ToDateTime(lblPODate.Text).ToString("dd-MMM-yyyy");
                else
                    dr["PO_DATE"] = string.Empty;

                if (!string.IsNullOrEmpty(lblVendorCode.Text))
                    dr["VENDOR_CODE"] = lblVendorCode.Text;
                else
                    dr["VENDOR_CODE"] = string.Empty;

                if (!string.IsNullOrEmpty(lblVendorName.Text))
                    dr["VENDOR_NAME"] = lblVendorName.Text;
                else
                    dr["VENDOR_NAME"] = string.Empty;

                if (!string.IsNullOrEmpty(lblPOValueINR.Text))
                    dr["PO_VALUE_INR"] = Convert.ToString(lblPOValueINR.Text);
                else
                    dr["PO_VALUE_INR"] = "0";

                if (!string.IsNullOrEmpty(txtFraightOrCustomLoading.Text))
                    dr["FRAIGHT_OR_CUSTOM_LOADING_IN_PERCENTAGE"] = Convert.ToString(txtFraightOrCustomLoading.Text);
                else
                    dr["FRAIGHT_OR_CUSTOM_LOADING_IN_PERCENTAGE"] = "0";

                if (!string.IsNullOrEmpty(txtTotalPOValueINR.Text))
                    dr["TOTAL_PO_VALUE_INR"] = Convert.ToString(txtTotalPOValueINR.Text);
                else
                    dr["TOTAL_PO_VALUE_INR"] = "0";

                if (!string.IsNullOrEmpty(lblDocClass.Text))
                    dr["DOC_CLASS"] = Convert.ToString(lblDocClass.Text);
                else
                    dr["DOC_CLASS"] = string.Empty;

                if (!string.IsNullOrEmpty(lblPOStatus.Text))
                    dr["PO_STATUS"] = lblPOStatus.Text;
                else
                    dr["PO_STATUS"] = string.Empty;

                if (!string.IsNullOrEmpty(lblLocation.Text))
                    dr["LOCATION"] = lblLocation.Text;
                else
                    dr["LOCATION"] = string.Empty;

                if (!string.IsNullOrEmpty(txtLastDeliveryDate.Text))
                    dr["LAST_DELIVERY_DATE"] = txtLastDeliveryDate.Text;
                else
                    dr["LAST_DELIVERY_DATE"] = string.Empty;

                if (!string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
                    dr["EXPECTED_DELIVERY_DATE"] = txtExpectedDeliveryDate.Text;
                else
                    dr["EXPECTED_DELIVERY_DATE"] = string.Empty;

                if (!string.IsNullOrEmpty(txtNoOfMonthsToDeliver.Text))
                    dr["NO_OF_MONTHS_TO_DELIVER"] = txtNoOfMonthsToDeliver.Text;
                else
                    dr["NO_OF_MONTHS_TO_DELIVER"] = string.Empty;

                if (!string.IsNullOrEmpty(txtNoOfMonthsBasedUpon.Text))
                    dr["NO_OF_MONTHS_BASED_UPON"] = txtNoOfMonthsBasedUpon.Text;
                else
                    dr["NO_OF_MONTHS_BASED_UPON"] = string.Empty;

                if (!string.IsNullOrEmpty(txtPaymentTerm.Text))
                    dr["PAYMENT_TERM"] = txtPaymentTerm.Text;
                else
                    dr["PAYMENT_TERM"] = string.Empty;

                if (ddlIsOrderSubjectTo.SelectedIndex > 0)
                    dr["IS_ORDER_SUBJECT_TO"] = Convert.ToString(ddlIsOrderSubjectTo.SelectedValue);
                else
                    dr["IS_ORDER_SUBJECT_TO"] = "0";

                if (ddlSalesPivotGroup.SelectedIndex > 0)
                    dr["SALES_PIVOT_GROUP"] = Convert.ToString(ddlSalesPivotGroup.SelectedValue);
                else
                    dr["SALES_PIVOT_GROUP"] = "0";

                dtNew.Rows.Add(dr);
            }


            if (dtNew.Rows.Count > 0)
            {
                foreach (DataRow drremove in dtNew.Select("SERIAL_NO='" + serialNo + "'"))
                {
                    dtNew.Rows.Remove(drremove);
                }

                if (dtNew.Rows.Count > 0)
                {
                    Session["PO_REPORT"] = dtNew;
                    gvPOReportSummery.DataSource = dtNew;
                    gvPOReportSummery.DataBind();
                }
                else
                {
                    Session["PO_REPORT"] = null;
                    gvPOReportSummery.DataSource = null;
                    gvPOReportSummery.DataBind();
                }
            }
            else
            {
                Session["PO_REPORT"] = null;
            }
            lblRecords.Text = "Records[" + dtNew.Rows.Count + "]";
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