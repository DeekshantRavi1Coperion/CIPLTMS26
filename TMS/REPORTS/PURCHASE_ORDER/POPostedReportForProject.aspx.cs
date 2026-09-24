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

public partial class REPORTS_PURCHASE_ORDER_POPostedReportForProject : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Purchase objPurchase = new BAL.Purchase();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsPOList = new DataSet();
    DataSet dsCreatedBy = new DataSet();
    DataSet dsCheckedBy = new DataSet();
    DataSet dsPODetailList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsDBDetails = new DataSet();

    int dateTypeID = 0;
    string fromDate = string.Empty;
    string toDate = string.Empty;

    string poNo = string.Empty;
    string vendorName = string.Empty;
    string JOBNo = string.Empty;
    double amount1 = 0;
    double amount2 = 0;
    string unitName = string.Empty;
    string sign = string.Empty;
    int excludeCIDF = 0;
    int excludeEngineeringService = 0;
    string isDoneOrPending = string.Empty;
    string followupBy = string.Empty;
    string followupDate = string.Empty;
    string postingStatus = string.Empty;
    string mrNo = string.Empty;
    string mrCreatedBy = string.Empty;
    string checkedBy = string.Empty;

    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;





    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdPostingConfirmValue.Value = "0";
                hdDeletionConfirmValue.Value = "0";

                Session["dsPOList"] = null;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;
                BindCreatedBy();
                BindCheckedBy();
                BindUnit();
                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                ddlPostingStatusSearch.SelectedValue = "Open";
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblTotalInvoiceINR.Text = "0";
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetPOList();

        if (ddlSign.SelectedIndex == 5)
        {
            txtAmountTwo.Enabled = true;
        }
        else
        {
            txtAmountTwo.Text = string.Empty;
            txtAmountTwo.Enabled = false;
        }
    }

    double totalInvoiceINR = 0;

    protected void gvPOList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFollowUpDays = (Label)e.Row.FindControl("lblFollowUpDays");
                Label lblPostingStatus = (Label)e.Row.FindControl("lblPostingStatus");
                Label lblEnggApprovalStatus = (Label)e.Row.FindControl("lblEnggApprovalStatus");
                Label lblPONo = (Label)e.Row.FindControl("lblPONo");

                Label lblPODeliveryDate = (Label)e.Row.FindControl("lblPODeliveryDate");
                Label lblEdOfInspection = (Label)e.Row.FindControl("lblEdOfInspection");




                TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                //TextBox txtBalQuantity = (TextBox)e.Row.FindControl("txtBalQuantity");

                e.Row.ToolTip = Convert.ToString(lblPONo.Text);
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");

                TextBox txtPOValueINR = (TextBox)e.Row.FindControl("txtPOValueINR");
                TextBox txtBudget = (TextBox)e.Row.FindControl("txtBudget");

                totalInvoiceINR += Convert.ToDouble(txtPOValueINR.Text);
                lblTotalInvoiceINR.Text = Convert.ToString(totalInvoiceINR);
                lblTotalInvoiceINR.ForeColor = System.Drawing.Color.Green;

                if (Convert.ToInt32(lblRecordID.Text) > 0)
                {
                    txtQuantity.BackColor = System.Drawing.Color.LightGreen;
                    //txtBalQuantity.BackColor = System.Drawing.Color.LightGreen;
                    txtPOValueINR.BackColor = System.Drawing.Color.LightGreen;
                    txtBudget.BackColor = System.Drawing.Color.LightGreen;

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                else
                {
                    txtQuantity.BackColor = System.Drawing.Color.LightYellow;
                    //txtBalQuantity.BackColor = System.Drawing.Color.LightYellow;
                    txtPOValueINR.BackColor = System.Drawing.Color.LightYellow;
                    txtBudget.BackColor = System.Drawing.Color.LightYellow;

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightYellow;
                    }
                }

                if (lblPostingStatus.Text == "Close" && lblPostingStatus.Text != "Open")
                {
                    if (Convert.ToInt32(lblFollowUpDays.Text) > 14)
                    {
                        txtQuantity.BackColor = System.Drawing.Color.Transparent;
                        //txtBalQuantity.BackColor = System.Drawing.Color.Transparent;
                        txtPOValueINR.BackColor = System.Drawing.Color.Transparent;
                        txtBudget.BackColor = System.Drawing.Color.Transparent;

                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Transparent;
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(lblFollowUpDays.Text) > 14)
                    {
                        txtQuantity.BackColor = System.Drawing.Color.LightSkyBlue;
                        //txtBalQuantity.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtPOValueINR.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtBudget.BackColor = System.Drawing.Color.LightSkyBlue;

                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.LightSkyBlue;
                        }
                    }
                }


                //if (!string.IsNullOrEmpty(Convert.ToString(lblPODeliveryDate.Text)) && !string.IsNullOrEmpty(Convert.ToString(lblEdOfInspection.Text)))
                //{
                //    if (Convert.ToDateTime(lblPODeliveryDate.Text) < Convert.ToDateTime(lblEdOfInspection.Text))
                //    {
                //        txtQuantity.BackColor = System.Drawing.Color.LightPink;
                //        txtPOValueINR.BackColor = System.Drawing.Color.LightPink;
                //        txtBudget.BackColor = System.Drawing.Color.LightPink;

                //        for (int i = 0; i < e.Row.Cells.Count; i++)
                //        {
                //            e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                //        }
                //    }                    
                //}
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


    protected void gvPOList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }



                Label lblPONo = gvPOList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblLocation = gvPOList.Rows[rowindex].FindControl("lblLocation") as Label;

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    Session["dtPODetailList"] = null;
                    BindPODetails(Convert.ToString(lblPONo.Text), Convert.ToString(lblLocation.Text));
                    mpeDetail.Show();
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


    double totalQuantity = 0;
    double totalRecQuantity = 0;
    double totalBalQuantity = 0;

    protected void gvPODetailsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                TextBox txtRecQuantity = (TextBox)e.Row.FindControl("txtRecQuantity");
                TextBox txtBalQuantity = (TextBox)e.Row.FindControl("txtBalQuantity");


                totalQuantity += Convert.ToDouble(txtQuantity.Text);
                txtTotalQuantity.Text = Convert.ToString(totalQuantity);
                txtTotalQuantity.ForeColor = System.Drawing.Color.Green;


                totalRecQuantity += Convert.ToDouble(txtRecQuantity.Text);
                txtTotalRecQuantity.Text = Convert.ToString(totalRecQuantity);
                txtTotalRecQuantity.ForeColor = System.Drawing.Color.Green;

                totalBalQuantity += Convert.ToDouble(txtBalQuantity.Text);
                txtTotalBalQuantity.Text = Convert.ToString(totalBalQuantity);
                txtTotalBalQuantity.ForeColor = System.Drawing.Color.Green;
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
        if (gvPOList.Rows.Count > 0)
        {
            //ExportToExcelNew();
            DataTable dt = (DataTable)Session["dsPOList"];
            ExportToExcel(dt);
        }
    }

    protected void btnExportPODetails_Click(object sender, EventArgs e)
    {
        if (gvPODetailsList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtPODetailList"];
            ExportToExcelPODetails(dt);
        }
    }
    #endregion


    #region METHODS[=========================]

    private void BindCreatedBy()
    {
        try
        {
            dsCreatedBy = objPurchase.GetMRCreatedBy();
            if (dsCreatedBy.Tables.Count > 0 && dsCreatedBy.Tables[0].Rows.Count > 0)
            {
                ddlMRCreatedBy.DataSource = dsCreatedBy.Tables[0];
                ddlMRCreatedBy.DataTextField = "MR_CREATED_BY";
                ddlMRCreatedBy.DataValueField = "MR_CREATED_BY";
                ddlMRCreatedBy.DataBind();
                ddlMRCreatedBy.Items.Insert(0, "ALL");
                ddlMRCreatedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCheckedBy()
    {
        try
        {
            dsCheckedBy = objPurchase.GetCheckedBy();
            if (dsCheckedBy.Tables.Count > 0 && dsCheckedBy.Tables[0].Rows.Count > 0)
            {
                ddlCheckedBy.DataSource = dsCheckedBy.Tables[0];
                ddlCheckedBy.DataTextField = "CHECKED_BY";
                ddlCheckedBy.DataValueField = "CHECKED_BY";
                ddlCheckedBy.DataBind();
                ddlCheckedBy.Items.Insert(0, "ALL");
                ddlCheckedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

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

    private void GetPOList()
    {
        try
        {
            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }

            dateTypeID = Convert.ToInt32(ddlDateType.SelectedValue);

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

            if (!string.IsNullOrEmpty(txtAmountOne.Text))
                amount1 = Convert.ToDouble(txtAmountOne.Text);
            else
                amount2 = 0;

            if (!string.IsNullOrEmpty(txtAmountTwo.Text))
                amount2 = Convert.ToDouble(txtAmountTwo.Text);
            else
                amount2 = 0;

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;

            sign = Convert.ToString(ddlSign.SelectedItem.Text);

            if (chkExcludeCIDF.Checked)
                excludeCIDF = 1;
            else
                excludeCIDF = 0;

            if (chkExcludeEngineeringService.Checked)
                excludeEngineeringService = 1;
            else
                excludeEngineeringService = 0;

            if (ddlFollowUp.SelectedIndex > 0)
                isDoneOrPending = Convert.ToString(ddlFollowUp.SelectedValue);
            else
                isDoneOrPending = string.Empty;


            if (ddlFollowUpBy.SelectedIndex > 0)
                followupBy = Convert.ToString(ddlFollowUpBy.SelectedValue);
            else
                followupBy = string.Empty;


            if (!string.IsNullOrEmpty(Convert.ToString(hdFollowUpDateSearch.Value)))
                followupDate = Convert.ToDateTime(hdFollowUpDateSearch.Value).ToString("yyyy-MM-dd");
            else
                followupDate = string.Empty;


            if (ddlPostingStatusSearch.SelectedIndex > 0)
                postingStatus = Convert.ToString(ddlPostingStatusSearch.SelectedValue);
            else
                postingStatus = string.Empty;

            if (!string.IsNullOrEmpty(txtMRNo.Text))
                mrNo = Convert.ToString(txtMRNo.Text);
            else
                mrNo = string.Empty;

            if (ddlMRCreatedBy.SelectedIndex > 0)
                mrCreatedBy = Convert.ToString(ddlMRCreatedBy.SelectedValue);
            else
                mrCreatedBy = string.Empty;

            if (ddlCheckedBy.SelectedIndex > 0)
                checkedBy = Convert.ToString(ddlCheckedBy.SelectedValue);
            else
                checkedBy = string.Empty;

            dsPOList = objPurchase.GetPoProcurementPostedList(dateTypeID, fromDate, toDate, dbNameA35, dbNameDLH, dbNameGNU, dbNameSEZ, poNo,
                                                        vendorName, unitName, JOBNo, amount1, amount2, sign, excludeCIDF, excludeEngineeringService,
                                                        isDoneOrPending, followupBy, followupDate, postingStatus, mrNo, mrCreatedBy, checkedBy);

            if (dsPOList.Tables.Count > 0 && dsPOList.Tables[0].Rows.Count > 0)
            {
                Session["dsPOList"] = dsPOList.Tables[0];
                gvPOList.DataSource = dsPOList.Tables[0];
                gvPOList.DataBind();
            }
            else
            {
                Session["dsPOList"] = null;
                gvPOList.DataSource = null;
                gvPOList.DataBind();
            }
            lblRecords.Text = "Records[" + dsPOList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPODetails(string PONo, string unitName)
    {
        try
        {
            dsPODetailList = objPurchase.GetPODetailList(PONo, unitName);
            if (dsPODetailList.Tables.Count > 0 && dsPODetailList.Tables[0].Rows.Count > 0)
            {
                Session["dtPODetailList"] = dsPODetailList.Tables[0];
                gvPODetailsList.DataSource = dsPODetailList.Tables[0];
                gvPODetailsList.DataBind();
            }
            else
            {
                Session["dtPODetailList"] = null;
            }
            lblPODetailRerords.Text = "Record[" + dsPODetailList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcelNew()
    {
        try
        {
            string csv = string.Empty;

            csv = "SR_NO,";

            for (int i = 1; i < gvPOList.Columns.Count; i++)
            {
                if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "PO_DELIVERY_DATE")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",VENDOR_CODE,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "QUANTITY")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",BAL_QUANTITY,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "UOM")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",SERVICE,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "FOLLOW_UP_BY")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",MR_CREATED_BY,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "POSTING_STATUS")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",NEXT_FOLLOWUP_DATE,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",FOLLOW_UP_DAYS,";
                }
                else
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ',';
                }

            }
            csv += "\r\n";

            int srNo = 0;
            string poNo = string.Empty;
            string poDate = string.Empty;
            string poDeliveryDate = string.Empty;
            string vendorCode = string.Empty;
            string vendorName = string.Empty;
            string itemName = string.Empty;
            string quantity = string.Empty;
            string balQuantity = string.Empty;
            string uom = string.Empty;
            string service = string.Empty;
            string poValueInr = string.Empty;
            string jobNo = string.Empty;
            string budget = string.Empty;
            string followUpBy = string.Empty;
            string mrCreatedBy = string.Empty;
            string location = string.Empty;
            string poFirstItem = string.Empty;
            string enggApprovalStatus = string.Empty;
            string presentStatus = string.Empty;
            string edOfInspComp = string.Empty;
            string postingStatus = string.Empty;
            string nextFollowupDate = string.Empty;
            string lastStatus = string.Empty;
            string lastEdOfInspComp = string.Empty;
            string lastFollowUpDate = string.Empty;
            string oaReceived = string.Empty;
            string dateOfDrawingReceivedFromVendor = string.Empty;
            string dateOfApprovedDrawingSentToVendor = string.Empty;
            string followUpDays = string.Empty;


            foreach (GridViewRow gr in gvPOList.Rows)
            {
                srNo++;
                poNo = string.Empty;
                poDate = string.Empty;
                poDeliveryDate = string.Empty;
                vendorCode = string.Empty;
                vendorName = string.Empty;
                itemName = string.Empty;
                quantity = "0";
                balQuantity = "0";
                uom = string.Empty;
                service = string.Empty;
                poValueInr = "0";
                jobNo = string.Empty;
                budget = "0";
                followUpBy = string.Empty;
                mrCreatedBy = string.Empty;
                location = string.Empty;
                poFirstItem = string.Empty;
                enggApprovalStatus = string.Empty;
                presentStatus = string.Empty;
                edOfInspComp = string.Empty;
                postingStatus = string.Empty;
                nextFollowupDate = string.Empty;
                lastStatus = string.Empty;
                lastEdOfInspComp = string.Empty;
                lastFollowUpDate = string.Empty;
                oaReceived = string.Empty;
                dateOfDrawingReceivedFromVendor = string.Empty;
                dateOfApprovedDrawingSentToVendor = string.Empty;
                followUpDays = "0";


                Label lblPONo = gr.FindControl("lblPONo") as Label;
                Label lblPODate = gr.FindControl("lblPODate") as Label;
                Label lblPODeliveryDate = gr.FindControl("lblPODeliveryDate") as Label;
                Label lblVendorCode = gr.FindControl("lblVendorCode") as Label;
                Label lblVendorName = gr.FindControl("lblVendorName") as Label;
                Label lblItemName = gr.FindControl("lblItemName") as Label;
                TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                Label lblBalQuantity = gr.FindControl("lblBalQuantity") as Label;
                Label lblUOM = gr.FindControl("lblUOM") as Label;
                //Label lblService = gr.FindControl("lblService") as Label;
                TextBox txtPOValueINR = gr.FindControl("txtPOValueINR") as TextBox;
                Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                TextBox txtBudget = gr.FindControl("txtBudget") as TextBox;
                Label lblFollowUpBy = gr.FindControl("lblFollowUpBy") as Label;
                Label lblMrCreatedBy = gr.FindControl("lblMrCreatedBy") as Label;
                Label lblLocation = gr.FindControl("lblLocation") as Label;
                Label lblPOFirstItem = gr.FindControl("lblPOFirstItem") as Label;
                Label lblEnggApprovalStatus = gr.FindControl("lblEnggApprovalStatus") as Label;
                Label lblPresentStatus = gr.FindControl("lblPresentStatus") as Label;
                Label lblEdOfInspection = gr.FindControl("lblEdOfInspection") as Label;
                Label lblPostingStatus = gr.FindControl("lblPostingStatus") as Label;
                Label lblNextFollowupDate = gr.FindControl("lblNextFollowupDate") as Label;
                Label lblLastStatus = gr.FindControl("lblLastStatus") as Label;
                Label lblLastEdOfInspection = gr.FindControl("lblLastEdOfInspection") as Label;
                Label lblLastModifiedDate = gr.FindControl("lblLastModifiedDate") as Label;
                Label lblOAReceived = gr.FindControl("lblOAReceived") as Label;
                Label lblDateOfDrawingReceivedFromVendor = gr.FindControl("lblDateOfDrawingReceivedFromVendor") as Label;
                Label lblDateOfApprovedDrawingSentToVendor = gr.FindControl("lblDateOfApprovedDrawingSentToVendor") as Label;
                Label lblFollowUpDays = gr.FindControl("lblFollowUpDays") as Label;

                if (!string.IsNullOrEmpty(lblPONo.Text))
                    poNo = lblPONo.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblPODate.Text))
                    poDate = lblPODate.Text;

                if (!string.IsNullOrEmpty(lblPODeliveryDate.Text))
                    poDeliveryDate = lblPODeliveryDate.Text;

                if (!string.IsNullOrEmpty(lblVendorCode.Text))
                    vendorCode = lblVendorCode.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblVendorName.Text))
                    vendorName = lblVendorName.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblItemName.Text))
                    itemName = lblItemName.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Trim();

                if (!string.IsNullOrEmpty(txtQuantity.Text))
                    quantity = txtQuantity.Text;

                if (!string.IsNullOrEmpty(lblBalQuantity.Text))
                    balQuantity = lblBalQuantity.Text;

                if (!string.IsNullOrEmpty(lblUOM.Text))
                    uom = lblUOM.Text.Replace("\n", " ").Replace(",", "&").Trim();

                //if (!string.IsNullOrEmpty(lblService.Text) && Convert.ToBoolean(lblService.Text) == true)
                //    service = "Yes";
                //else service = "No";

                if (!string.IsNullOrEmpty(txtPOValueINR.Text))
                    poValueInr = txtPOValueINR.Text;

                if (!string.IsNullOrEmpty(lblJOBNo.Text))
                    jobNo = lblJOBNo.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(txtBudget.Text))
                    budget = txtBudget.Text;

                if (!string.IsNullOrEmpty(lblFollowUpBy.Text))
                    followUpBy = lblFollowUpBy.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblMrCreatedBy.Text))
                    mrCreatedBy = lblMrCreatedBy.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblLocation.Text))
                    location = lblLocation.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblPOFirstItem.Text))
                    poFirstItem = lblPOFirstItem.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblEnggApprovalStatus.Text))
                    enggApprovalStatus = lblEnggApprovalStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblPresentStatus.Text))
                    presentStatus = lblPresentStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblEdOfInspection.Text))
                    edOfInspComp = lblEdOfInspection.Text;

                if (!string.IsNullOrEmpty(lblPostingStatus.Text))
                    postingStatus = lblPostingStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblNextFollowupDate.Text))
                    nextFollowupDate = lblNextFollowupDate.Text;

                if (!string.IsNullOrEmpty(lblLastStatus.Text))
                    lastStatus = lblLastStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblLastEdOfInspection.Text))
                    lastEdOfInspComp = lblLastEdOfInspection.Text;

                if (!string.IsNullOrEmpty(lblLastModifiedDate.Text))
                    lastFollowUpDate = lblLastModifiedDate.Text;

                if (!string.IsNullOrEmpty(lblOAReceived.Text))
                    oaReceived = lblOAReceived.Text;

                if (!string.IsNullOrEmpty(lblDateOfDrawingReceivedFromVendor.Text))
                    dateOfDrawingReceivedFromVendor = lblDateOfDrawingReceivedFromVendor.Text;

                if (!string.IsNullOrEmpty(lblDateOfApprovedDrawingSentToVendor.Text))
                    dateOfApprovedDrawingSentToVendor = lblDateOfApprovedDrawingSentToVendor.Text;

                if (!string.IsNullOrEmpty(lblFollowUpDays.Text))
                    followUpDays = lblFollowUpDays.Text;


                csv += srNo + "," +
                        poNo + "," +
                        poDate + "," +
                        poDeliveryDate + "," +
                        vendorCode + "," +
                        vendorName + "," +
                        itemName + "," +
                        quantity + "," +
                        balQuantity + "," +
                        uom + "," +
                        service + "," +
                        poValueInr + "," +
                        jobNo + "," +
                        budget + "," +
                        followUpBy + "," +
                        mrCreatedBy + "," +
                        location + "," +
                        poFirstItem + "," +
                        enggApprovalStatus + "," +
                        presentStatus + "," +
                        edOfInspComp + "," +
                        postingStatus + "," +
                        nextFollowupDate + "," +
                        lastStatus + "," +
                        lastEdOfInspComp + "," +
                        lastFollowUpDate + "," +
                        oaReceived + "," +
                        dateOfDrawingReceivedFromVendor + "," +
                        dateOfApprovedDrawingSentToVendor + "," +
                        followUpDays;


                csv += "\r\n";
            }

            string fileName = "Procurement_Status-Project_View_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 1; i < dt.Columns.Count - 1; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 1; k < dt.Columns.Count - 1; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;


                    rowTxt = rowTxt.Replace(',', ' ');
                    rowTxt = rowTxt.TrimEnd('\r', ' ');
                    rowTxt = rowTxt.TrimEnd('\n', ' ');
                    rowTxt = rowTxt.Replace('\r', ' ');
                    rowTxt = rowTxt.Replace('\n', ' ');

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "Procurement_Status-Project_View_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void ExportToExcelPODetails(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;


                    rowTxt = rowTxt.Replace(',', ' ');
                    rowTxt = rowTxt.TrimEnd('\r', ' ');
                    rowTxt = rowTxt.TrimEnd('\n', ' ');
                    rowTxt = rowTxt.Replace('\r', ' ');
                    rowTxt = rowTxt.Replace('\n', ' ');

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "PO_Detail_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion    
}
