using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;

public partial class PROJECT_MRNQA_OutstandingMRNQAList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsEmployee = new DataSet();
    DataSet dsMRNList = new DataSet();
    DataSet dsUnit = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string mrnNo = string.Empty;
    string poNo = string.Empty;
    string mrnDate = string.Empty;
    string vendorCode = string.Empty;
    string vendorName = string.Empty;
    string productCode = string.Empty;
    string productDesc = string.Empty;
    string quantity = string.Empty;
    string uom = string.Empty;
    string unitName = string.Empty;
    int unitID = 0;
    string assignToRemarks = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["MRN_LIST"] = null;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;

                Session["dsEmployee"] = objProject.GetQualityChecker();

                BindUnit();
                GetOutstandingMRNList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetOutstandingMRNList();
    }

    protected void gvMRNList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["dsEmployee"] != null)
                    dsEmployee = (DataSet)Session["dsEmployee"];
                else
                    dsEmployee = objProject.GetQualityChecker();

                DropDownList ddlAssiengedTo = (e.Row.FindControl("ddlAssiengedTo") as DropDownList);
                if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
                {
                    ddlAssiengedTo.DataSource = dsEmployee.Tables[0];
                    ddlAssiengedTo.DataTextField = "EMPLOYEE_NAME";
                    ddlAssiengedTo.DataValueField = "EMP_RECORD_ID";
                    ddlAssiengedTo.DataBind();
                    ddlAssiengedTo.Items.Insert(0, "Select");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvMRNList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewProductDETAIL" || Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "ASSIGN" || Convert.ToString(e.CommandArgument) == "SEND_TO_QUALITY_CHECK")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblMRNNo = gvMRNList.Rows[rowindex].FindControl("lblMRNNo") as Label;
                Label lblMRNDate = gvMRNList.Rows[rowindex].FindControl("lblMRNDate") as Label;

                Label lblPONo = gvMRNList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblVendorCode = gvMRNList.Rows[rowindex].FindControl("lblVendorCode") as Label;
                Label lblVendorName = gvMRNList.Rows[rowindex].FindControl("lblVendorName") as Label;

                Label lblProductCode = gvMRNList.Rows[rowindex].FindControl("lblProductCode") as Label;
                Label lblProductDesc = gvMRNList.Rows[rowindex].FindControl("lblProductDesc") as Label;

                TextBox txtQuantity = gvMRNList.Rows[rowindex].FindControl("txtQuantity") as TextBox;
                Label lblUOM = gvMRNList.Rows[rowindex].FindControl("lblUOM") as Label;
                Label lblUnit = gvMRNList.Rows[rowindex].FindControl("lblUnit") as Label;
                Label lblUnitID = gvMRNList.Rows[rowindex].FindControl("lblUnitID") as Label;

                TextBox txtRemarks = gvMRNList.Rows[rowindex].FindControl("txtRemarks") as TextBox;

                DropDownList ddlAssiengedTo = gvMRNList.Rows[rowindex].FindControl("ddlAssiengedTo") as DropDownList;
                CheckBox chkSelect = gvMRNList.Rows[rowindex].FindControl("chkSelect") as CheckBox;

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    ModalPopupExtender4.Show();
                    iframeViewMRNInPDF.Attributes.Add("src", "MRNInPDF.aspx?MRNNO=" + lblMRNNo.Text);
                }

                if (Convert.ToString(e.CommandArgument) == "SEND_TO_QUALITY_CHECK")
                {
                    if (chkSelect.Checked)
                    {
                        AssignToQualityCheckerSingle(lblMRNNo.Text, lblMRNDate.Text, lblPONo.Text, lblVendorCode.Text, lblVendorName.Text, lblProductCode.Text, lblProductDesc.Text,
                                                 txtQuantity.Text, lblUOM.Text, Convert.ToInt32(lblUnitID.Text), lblUnit.Text, Convert.ToInt32(ddlAssiengedTo.SelectedValue), txtRemarks.Text);
                    }
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

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (gvMRNList.Rows.Count > 0)
        {
            if (chkSelectAll.Checked)
            {
                foreach (GridViewRow gr in gvMRNList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    //ImageButton imgbtnLastDeliveryDate = (ImageButton)gr.FindControl("imgbtnLastDeliveryDate");
                    chkSelect.Checked = true;
                    //imgbtnLastDeliveryDate.Enabled = true;
                }
            }
            if (!chkSelectAll.Checked)
            {
                foreach (GridViewRow gr in gvMRNList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    //ImageButton imgbtnLastDeliveryDate = (ImageButton)gr.FindControl("imgbtnLastDeliveryDate");
                    chkSelect.Checked = false;
                    //imgbtnLastDeliveryDate.Enabled = false;
                }
            }
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {


        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            AssignToQualityChecker();
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

    private void GetOutstandingMRNList()
    {
        try
        {
            fromDate = string.Empty;
            toDate = string.Empty;
            unitID = 0;
            mrnNo = string.Empty;
            poNo = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtMRNNo.Text))
                mrnNo = txtMRNNo.Text.Trim();

            if (!string.IsNullOrEmpty(txtPOno.Text))
                poNo = txtPOno.Text.Trim();



            dsMRNList = objProject.GetOutstandingMRNList(fromDate, toDate, unitID, mrnNo, poNo);

            if (dsMRNList.Tables.Count > 0 && dsMRNList.Tables[0].Rows.Count > 0)
            {
                Session["MRN_LIST"] = dsMRNList;
                gvMRNList.DataSource = dsMRNList.Tables[0];
                gvMRNList.DataBind();
            }
            else
            {
                Session["MRN_LIST"] = null;
                gvMRNList.DataSource = null;
                gvMRNList.DataBind();
            }

            lblRecords.Text = "Records[" + gvMRNList.Rows.Count + "]";

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AssignToQualityChecker()
    {
        try
        {
            DataTable dtMRN = new DataTable();

            dtMRN.Columns.Add("MRN_NO", typeof(string));
            dtMRN.Columns.Add("MRN_DATE", typeof(string));
            dtMRN.Columns.Add("PO_NO", typeof(string));
            dtMRN.Columns.Add("VENDOR_CODE", typeof(string));
            dtMRN.Columns.Add("VENDOR_NAME", typeof(string));
            dtMRN.Columns.Add("PRODUCT_CODE", typeof(string));
            dtMRN.Columns.Add("PRODUCT_DESC", typeof(string));
            dtMRN.Columns.Add("QUANTITY", typeof(double));
            dtMRN.Columns.Add("UOM", typeof(string));
            dtMRN.Columns.Add("UNIT_ID", typeof(int));
            dtMRN.Columns.Add("UNIT_NAME", typeof(string));
            dtMRN.Columns.Add("ASSIGN_TO", typeof(int));
            dtMRN.Columns.Add("ASSIGNED_TO_REMARKS", typeof(string));
            dtMRN.Columns.Add("CREATED_BY", typeof(int));


            string fromName = string.Empty;
            string fromEmail = string.Empty;

            mrnNo = string.Empty;
            mrnDate = string.Empty;
            poNo = string.Empty;
            vendorCode = string.Empty;
            vendorName = string.Empty;
            productCode = string.Empty;
            productDesc = string.Empty;
            quantity = string.Empty;
            uom = string.Empty;
            unitName = string.Empty;
            unitID = 0;
            assignToRemarks = string.Empty;

            int qualityCheckerID = 0;
            string qualityCheckerName = string.Empty;
            string qualityCheckerEmail = string.Empty;

            if (gvMRNList.Rows.Count > 0)
            {
                if (Session["dsEmployee"] != null)
                    dsEmployee = (DataSet)Session["dsEmployee"];
                else
                    dsEmployee = objProject.GetQualityChecker();

                foreach (GridViewRow gr in gvMRNList.Rows)
                {
                    Label lblMRNNo = gr.FindControl("lblMRNNo") as Label;
                    Label lblMRNDate = gr.FindControl("lblMRNDate") as Label;

                    Label lblPONo = gr.FindControl("lblPONo") as Label;
                    Label lblVendorCode = gr.FindControl("lblVendorCode") as Label;
                    Label lblVendorName = gr.FindControl("lblVendorName") as Label;

                    Label lblProductCode = gr.FindControl("lblProductCode") as Label;
                    Label lblProductDesc = gr.FindControl("lblProductDesc") as Label;

                    TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                    Label lblUOM = gr.FindControl("lblUOM") as Label;
                    Label lblUnit = gr.FindControl("lblUnit") as Label;
                    Label lblUnitID = gr.FindControl("lblUnitID") as Label;

                    TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                    DropDownList ddlAssiengedTo = gr.FindControl("ddlAssiengedTo") as DropDownList;
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        DataRow dr = dtMRN.NewRow();

                        if (!string.IsNullOrEmpty(lblMRNNo.Text))
                            dr["MRN_NO"] = lblMRNNo.Text;
                        else
                            dr["MRN_NO"] = string.Empty;

                        if (!string.IsNullOrEmpty(lblMRNDate.Text))
                            dr["MRN_DATE"] = Convert.ToDateTime(lblMRNDate.Text).ToString("yyyy-MM-dd");
                        else
                            dr["MRN_DATE"] = string.Empty;

                        if (!string.IsNullOrEmpty(lblPONo.Text))
                            dr["PO_NO"] = lblPONo.Text;
                        else
                            dr["PO_NO"] = string.Empty;

                        if (!string.IsNullOrEmpty(lblVendorCode.Text))
                            dr["VENDOR_CODE"] = lblVendorCode.Text;
                        else
                            dr["VENDOR_CODE"] = string.Empty;

                        if (!string.IsNullOrEmpty(lblVendorName.Text))
                            dr["VENDOR_NAME"] = lblVendorName.Text;
                        else
                            dr["VENDOR_NAME"] = string.Empty;

                        if (!string.IsNullOrEmpty(lblProductCode.Text))
                            dr["PRODUCT_CODE"] = lblProductCode.Text;
                        else
                            dr["PRODUCT_CODE"] = string.Empty;

                        if (!string.IsNullOrEmpty(lblProductDesc.Text))
                            dr["PRODUCT_DESC"] = lblProductDesc.Text;
                        else
                            dr["PRODUCT_DESC"] = string.Empty;

                        if (!string.IsNullOrEmpty(txtQuantity.Text))
                            dr["QUANTITY"] = Convert.ToString(txtQuantity.Text);
                        else
                            dr["QUANTITY"] = "0.00";

                        if (!string.IsNullOrEmpty(lblUOM.Text))
                            dr["UOM"] = lblUOM.Text;
                        else
                            dr["UOM"] = string.Empty;

                        if (!string.IsNullOrEmpty(lblUnit.Text))
                            dr["UNIT_NAME"] = lblUnit.Text;
                        else
                            dr["UNIT_NAME"] = string.Empty;

                        if (!string.IsNullOrEmpty(lblUnitID.Text) && Convert.ToInt32(lblUnitID.Text) > 0)
                            dr["UNIT_ID"] = Convert.ToInt32(lblUnitID.Text);
                        else
                            dr["UNIT_ID"] = 0;

                        if (ddlAssiengedTo.SelectedIndex > 0)
                        {
                            dr["ASSIGN_TO"] = Convert.ToInt32(ddlAssiengedTo.SelectedValue);

                            foreach (DataRow drc in dsEmployee.Tables[0].Select("EMP_RECORD_ID='" + Convert.ToString(ddlAssiengedTo.SelectedValue) + "'"))
                            {
                                if (drc["EMPLOYEE_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drc["EMPLOYEE_NAME"])))
                                    qualityCheckerName = Convert.ToString(drc["EMPLOYEE_NAME"]);

                                if (drc["EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drc["EMAIL_ID"])))
                                    qualityCheckerEmail = Convert.ToString(drc["EMAIL_ID"]);
                            }
                        }
                        else
                            dr["ASSIGN_TO"] = 0;

                        if (!string.IsNullOrEmpty(txtRemarks.Text))
                            dr["ASSIGNED_TO_REMARKS"] = txtRemarks.Text.Trim();
                        else
                            dr["ASSIGNED_TO_REMARKS"] = string.Empty;

                        dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);


                        fromName = Convert.ToString(Session["EMPLOYEE_NAME"]);
                        fromEmail = Convert.ToString(Session["EMAIL_ID"]);


                        if (!string.IsNullOrEmpty(lblMRNNo.Text.Trim()) && ddlAssiengedTo.SelectedIndex > 0)
                        {
                            dtMRN.Rows.Add(dr);
                        }
                    }
                }

                int insertCount = 0;
                int insertValue = 0;
                int sendMailValue = 0;
                int sentmailStatusval = 0;


                if (dtMRN.Rows.Count > 0)
                {
                    insertValue = objProject.SendToQualityForCheck(dtMRN);
                    if (insertValue > 0)
                    {
                        insertCount++;

                        string updateMailStatusQuery = string.Empty;
                        foreach (DataRow dr in dtMRN.Rows)
                        {
                            if (dr["MRN_NO"] != DBNull.Value)
                                mrnNo = Convert.ToString(dr["MRN_NO"]);

                            if (dr["MRN_DATE"] != DBNull.Value)
                                mrnDate = Convert.ToDateTime(dr["MRN_DATE"]).ToString("dd-MMM-yyyy");

                            if (dr["PO_NO"] != DBNull.Value)
                                poNo = Convert.ToString(dr["PO_NO"]);

                            if (dr["VENDOR_CODE"] != DBNull.Value)
                                vendorCode = Convert.ToString(dr["VENDOR_CODE"]);

                            if (dr["VENDOR_NAME"] != DBNull.Value)
                                vendorName = Convert.ToString(dr["VENDOR_NAME"]);

                            if (dr["PRODUCT_CODE"] != DBNull.Value)
                                productCode = Convert.ToString(dr["PRODUCT_CODE"]);

                            if (dr["PRODUCT_DESC"] != DBNull.Value)
                                productDesc = Convert.ToString(dr["PRODUCT_DESC"]);

                            if (dr["QUANTITY"] != DBNull.Value)
                                quantity = Convert.ToString(dr["QUANTITY"]);

                            if (dr["UOM"] != DBNull.Value)
                                uom = Convert.ToString(dr["UOM"]);

                            if (dr["UNIT_NAME"] != DBNull.Value)
                                unitName = Convert.ToString(dr["UNIT_NAME"]);

                            if (dr["UNIT_ID"] != DBNull.Value)
                                unitID = Convert.ToInt32(dr["UNIT_ID"]);

                            if (dr["ASSIGNED_TO_REMARKS"] != DBNull.Value)
                                assignToRemarks = Convert.ToString(dr["ASSIGNED_TO_REMARKS"]);


                            sendMailValue = SendMail(mrnNo, mrnDate, poNo, vendorCode, vendorName, unitName, assignToRemarks, qualityCheckerName, qualityCheckerEmail, fromName, fromEmail);
                            if (sendMailValue > 0)
                            {
                                updateMailStatusQuery += "UPDATE tblMRN set IS_NEW_MAIL_SENT=1,MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",MODIFIED_ON=GETDATE() " +
                                    "WHERE MRN_NO='" + mrnNo + "' AND " +
                                          "MRN_DATE='" + mrnDate + "' AND " +
                                          "PO_NO='" + poNo + "' AND " +
                                          "VENDOR_CODE='" + vendorCode + "' AND " +
                                          "PRODUCT_CODE='" + productCode + "' AND " +
                                          "UNIT_ID=" + unitID + ";\n";
                            }
                        }

                        if (!string.IsNullOrEmpty(updateMailStatusQuery))
                            updateMailStatusQuery = updateMailStatusQuery.TrimEnd('\n');

                        if (!string.IsNullOrEmpty(updateMailStatusQuery))
                        {
                            sentmailStatusval = objProject.UpdateMRNMailSentStatus(updateMailStatusQuery);
                        }
                    }
                }
                else
                {
                    ExceptionMessage("MRN No or assigned to is not selected to send for quality check...!!!");
                    return;
                }


                if (insertCount > 0)
                {
                    SuccessMessage(insertCount + " MRN sent for quality check successfully...!!!");
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again...!!!");
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

    private void AssignToQualityCheckerSingle(string mrnNo, string mrnDate, string poNo, string vendorCode, string vendorName, string productCode, string productDesc, string quantity,
                                                string uom, int unitID, string unitName, int assignedTo, string assignToRemarks)
    {
        try
        {

            if (Session["dsEmployee"] != null)
                dsEmployee = (DataSet)Session["dsEmployee"];
            else
                dsEmployee = objProject.GetQualityChecker();


            DataTable dtMRN = new DataTable();

            dtMRN.Columns.Add("MRN_NO", typeof(string));
            dtMRN.Columns.Add("MRN_DATE", typeof(string));
            dtMRN.Columns.Add("PO_NO", typeof(string));
            dtMRN.Columns.Add("VENDOR_CODE", typeof(string));
            dtMRN.Columns.Add("VENDOR_NAME", typeof(string));
            dtMRN.Columns.Add("PRODUCT_CODE", typeof(string));
            dtMRN.Columns.Add("PRODUCT_DESC", typeof(string));
            dtMRN.Columns.Add("QUANTITY", typeof(double));
            dtMRN.Columns.Add("UOM", typeof(string));
            dtMRN.Columns.Add("UNIT_ID", typeof(int));
            dtMRN.Columns.Add("UNIT_NAME", typeof(string));
            dtMRN.Columns.Add("ASSIGN_TO", typeof(int));
            dtMRN.Columns.Add("ASSIGNED_TO_REMARKS", typeof(string));
            dtMRN.Columns.Add("CREATED_BY", typeof(int));


            string fromName = string.Empty;
            string fromEmail = string.Empty;

            string qualityCheckerName = string.Empty;
            string qualityCheckerEmail = string.Empty;

            DataRow dr = dtMRN.NewRow();

            if (!string.IsNullOrEmpty(mrnNo))
                dr["MRN_NO"] = mrnNo;
            else
                dr["MRN_NO"] = string.Empty;

            if (!string.IsNullOrEmpty(mrnDate))
                dr["MRN_DATE"] = Convert.ToDateTime(mrnDate).ToString("yyyy-MM-dd");
            else
                dr["MRN_DATE"] = string.Empty;

            if (!string.IsNullOrEmpty(poNo))
                dr["PO_NO"] = poNo;
            else
                dr["PO_NO"] = string.Empty;

            if (!string.IsNullOrEmpty(vendorCode))
                dr["VENDOR_CODE"] = vendorCode;
            else
                dr["VENDOR_CODE"] = string.Empty;

            if (!string.IsNullOrEmpty(vendorName))
                dr["VENDOR_NAME"] = vendorName;
            else
                dr["VENDOR_NAME"] = string.Empty;

            if (!string.IsNullOrEmpty(productCode))
                dr["PRODUCT_CODE"] = productCode;
            else
                dr["PRODUCT_CODE"] = string.Empty;

            if (!string.IsNullOrEmpty(productDesc))
                dr["PRODUCT_DESC"] = productDesc;
            else
                dr["PRODUCT_DESC"] = string.Empty;

            if (!string.IsNullOrEmpty(quantity))
                dr["QUANTITY"] = Convert.ToString(quantity);
            else
                dr["QUANTITY"] = "0.00";

            if (!string.IsNullOrEmpty(uom))
                dr["UOM"] = uom;
            else
                dr["UOM"] = string.Empty;

            if (!string.IsNullOrEmpty(unitName))
                dr["UNIT_NAME"] = unitName;
            else
                dr["UNIT_NAME"] = string.Empty;

            if (unitID > 0)
                dr["UNIT_ID"] = unitID;
            else
                dr["UNIT_ID"] = 0;

            if (assignedTo > 0)
            {
                dr["ASSIGN_TO"] = assignedTo;
                
                foreach (DataRow drc in dsEmployee.Tables[0].Select("EMP_RECORD_ID='" + Convert.ToString(assignedTo) + "'"))
                {
                    if (drc["EMPLOYEE_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drc["EMPLOYEE_NAME"])))
                        qualityCheckerName = Convert.ToString(drc["EMPLOYEE_NAME"]);

                    if (drc["EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drc["EMAIL_ID"])))
                        qualityCheckerEmail = Convert.ToString(drc["EMAIL_ID"]);
                }
            }
            else
                dr["ASSIGN_TO"] = 0;

            if (!string.IsNullOrEmpty(assignToRemarks))
                dr["ASSIGNED_TO_REMARKS"] = assignToRemarks;
            else
                dr["ASSIGNED_TO_REMARKS"] = string.Empty;

            dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

           
            fromName = Convert.ToString(Session["EMPLOYEE_NAME"]);
            fromEmail = Convert.ToString(Session["EMAIL_ID"]);
            
            if (!string.IsNullOrEmpty(mrnNo))
            {
                dtMRN.Rows.Add(dr);
            }

            int insertValue = 0;
            int sendMailValue = 0;
            int sentmailStatusval = 0;


            if (dtMRN.Rows.Count > 0)
            {
                insertValue = objProject.SendToQualityForCheck(dtMRN);
                if (insertValue > 0)
                {
                    string updateMailStatusQuery = string.Empty;
                    foreach (DataRow drt in dtMRN.Rows)
                    {
                        if (drt["MRN_NO"] != DBNull.Value)
                            mrnNo = Convert.ToString(drt["MRN_NO"]);

                        if (drt["MRN_DATE"] != DBNull.Value)
                            mrnDate = Convert.ToDateTime(drt["MRN_DATE"]).ToString("dd-MMM-yyyy");

                        if (drt["PO_NO"] != DBNull.Value)
                            poNo = Convert.ToString(drt["PO_NO"]);

                        if (drt["VENDOR_CODE"] != DBNull.Value)
                            vendorCode = Convert.ToString(drt["VENDOR_CODE"]);

                        if (drt["VENDOR_NAME"] != DBNull.Value)
                            vendorName = Convert.ToString(drt["VENDOR_NAME"]);

                        if (drt["PRODUCT_CODE"] != DBNull.Value)
                            productCode = Convert.ToString(drt["PRODUCT_CODE"]);

                        if (drt["PRODUCT_DESC"] != DBNull.Value)
                            productDesc = Convert.ToString(drt["PRODUCT_DESC"]);

                        if (drt["QUANTITY"] != DBNull.Value)
                            quantity = Convert.ToString(drt["QUANTITY"]);

                        if (drt["UOM"] != DBNull.Value)
                            uom = Convert.ToString(drt["UOM"]);

                        if (drt["UNIT_NAME"] != DBNull.Value)
                            unitName = Convert.ToString(drt["UNIT_NAME"]);

                        if (drt["UNIT_ID"] != DBNull.Value)
                            unitID = Convert.ToInt32(drt["UNIT_ID"]);

                        if (drt["ASSIGNED_TO_REMARKS"] != DBNull.Value)
                            assignToRemarks = Convert.ToString(drt["ASSIGNED_TO_REMARKS"]);


                        sendMailValue = SendMail(mrnNo, mrnDate, poNo, vendorCode, vendorName, unitName, assignToRemarks, qualityCheckerName, qualityCheckerEmail, fromName, fromEmail);
                        if (sendMailValue > 0)
                        {
                            updateMailStatusQuery += "UPDATE tblMRN set IS_NEW_MAIL_SENT=1,MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",MODIFIED_ON=GETDATE() " +
                                "WHERE MRN_NO='" + mrnNo + "' AND " +
                                      "MRN_DATE='" + mrnDate + "' AND " +
                                      "PO_NO='" + poNo + "' AND " +
                                      "VENDOR_CODE='" + vendorCode + "' AND " +
                                      "PRODUCT_CODE='" + productCode + "' AND " +
                                      "UNIT_ID=" + unitID + ";\n";
                        }
                    }

                    if (!string.IsNullOrEmpty(updateMailStatusQuery))
                        updateMailStatusQuery = updateMailStatusQuery.TrimEnd('\n');

                    if (!string.IsNullOrEmpty(updateMailStatusQuery))
                    {
                        sentmailStatusval = objProject.UpdateMRNMailSentStatus(updateMailStatusQuery);
                    }
                }
            }

            if (insertValue > 0)
            {
                if (sendMailValue > 0)
                {
                    SuccessMessage("MRN No. '" + mrnNo + "' sent to quality with mail to check successfully...!!!");
                    return;
                }
                else
                {
                    SuccessMessage("MRN No. '" + mrnNo + "' sent to quality to check successfully...!!!");
                    return;
                }
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int SendMail(string mrnNo, string mrnDate, string poNo, string vendorCode, string vendorName, string unitName, string assignToRemarks,
                         string qualityCheckerName, string qualityCheckerEmail, string fromName, string fromEmail)
    {
        try
        {
            string urlTxt = string.Empty;
            string href = string.Empty;
            string link = string.Empty;

            urlTxt = string.Empty;
            href = string.Empty;
            link = string.Empty;

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            MailMessage mail = new MailMessage();

            mail.Subject = "MRN No.- " + mrnNo + " for Quality Check";

            mail.From = new MailAddress(fromEmail);


            if (!string.IsNullOrEmpty(qualityCheckerEmail))
            {
                qualityCheckerEmail = qualityCheckerEmail.TrimEnd(';');
                string[] strTo = qualityCheckerEmail.Split(';');
                foreach (string item in strTo)
                {
                    mail.To.Add(item);
                }
            }

            mail.IsBodyHtml = true;
            string body = string.Empty;
            string fileName = string.Empty;

            fileName = "~/PROJECT/MRNQA/EMAIL_FORMATS/NewMRNMail.htm";
            using (StreamReader reader = new StreamReader(Server.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            //urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);
            //link = "'" + urlTxt + "/Login.aspx?mrnno=" + mrnNo + "&productcode=" + productCode + "'";
            //href = "<a href=" + link + ">Please Check MRN</a>";

            body = body.Replace("{#unit#}", unitName);
            body = body.Replace("{#mrnno#}", mrnNo);
            body = body.Replace("{#mrndate#}", mrnDate);
            body = body.Replace("{#pono#}", poNo);
            body = body.Replace("{#vendorname#}", ("[" + vendorCode + "]-" + vendorName));
            body = body.Replace("{#remarks#}", assignToRemarks);

            body = body.Replace("{#qualitycheckername#}", qualityCheckerName);
            body = body.Replace("{#fromname#}", fromName);


            body = body.Replace("{#link#}", href);
            mail.Body = body;
            try
            {
                if (!string.IsNullOrEmpty(qualityCheckerEmail))
                {
                    SmtpServer.Send(mail);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    return 1;

                else
                    return 0;
            }
        }
        catch (Exception ex)
        {
            return 0;
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
