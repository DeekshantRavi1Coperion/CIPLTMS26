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
using System.IO;
using System.Data.OleDb;

public partial class REPORTS_SALE_ORDER_GROSS_MARGIN_ImportCostCenter : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataTable dtTempCostCenter = new DataTable();
    DataTable dtCostCenter0 = new DataTable();
    DataSet dsUnit = new DataSet();
    DataSet dsCostCenter = new DataSet();
    DataSet dsDBDetails = new DataSet();
    int existedRecrdsCount = 0;

    int unitId = 0;
    string unitName = string.Empty;
    
    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["DT_TEMP_COSTCENTER"] = null;
                Session["DT0_COSTCENTER"] = null;
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

    protected void btnGetFormat_Click(object sender, EventArgs e)
    {
        DownloadFormat();
    }

    protected void btnGetCostCenterFile_Click(object sender, EventArgs e)
    {
        GetCostCenterFile();
    }

    protected void gvCostCenter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            string costCenterCode = string.Empty;
            double costCenterAmount = 0;
            double directBilling = 0;
            double partialBilling = 0;
            double netAmount = 0;
            string month = string.Empty;
            int unitID = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblCostCenterID = (Label)e.Row.FindControl("lblCostCenterID");
                Label lblCostCenterCode = (Label)e.Row.FindControl("lblCostCenterCode");
                Label lblOrderNo = (Label)e.Row.FindControl("lblOrderNo");
                TextBox txtCostCenterAmount = (TextBox)e.Row.FindControl("txtCostCenterAmount");
                TextBox txtDirectBilling = (TextBox)e.Row.FindControl("txtDirectBilling");
                TextBox txtPartialBilling = (TextBox)e.Row.FindControl("txtPartialBilling");
                TextBox txtNetAmount = (TextBox)e.Row.FindControl("txtNetAmount");
                Label lblMonth = (Label)e.Row.FindControl("lblMonth");
                Label lblUnitID = (Label)e.Row.FindControl("lblUnitID");


                if (!string.IsNullOrEmpty(lblCostCenterCode.Text))
                    costCenterCode = Convert.ToString(lblCostCenterCode.Text).Trim();
                else
                    costCenterCode = string.Empty;

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

                if (netAmount == 0)
                {
                    txtNetAmount.Text = Convert.ToString(costCenterAmount + directBilling - partialBilling);
                    if (!string.IsNullOrEmpty(Convert.ToString(txtNetAmount.Text)) && !Convert.ToString(txtNetAmount.Text).Contains("."))
                    {
                        txtNetAmount.Text = Convert.ToString(txtNetAmount.Text + ".00");
                    }
                }

                if (!string.IsNullOrEmpty(lblMonth.Text))
                    month = Convert.ToDateTime(lblMonth.Text).ToString("yyyy-MM").Trim();
                else
                    month = string.Empty;

                if (!string.IsNullOrEmpty(lblUnitID.Text))
                    unitID = Convert.ToInt32(lblUnitID.Text);
                else
                    unitID = 0;


                if (Session["DT0_COSTCENTER"] != null)
                {
                    dtCostCenter0 = (DataTable)Session["DT0_COSTCENTER"];
                    if (dtCostCenter0.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCostCenter0.Select("COST_CENTER_CODE='" + costCenterCode + "' AND MONTH='" + month + "' AND UNIT_ID='" + unitID + "'"))
                        {
                            existedRecrdsCount++;
                            for (int i = 0; i < e.Row.Cells.Count; i++)
                            {
                                e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                            }
                        }
                    }
                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                if (string.IsNullOrEmpty(lblOrderNo.Text))
                {
                    txtDirectBilling.ReadOnly = true;
                    txtPartialBilling.ReadOnly = true;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvCostCenter.Rows.Count > 0)
        {
            ImportCostCenterFile();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }


    #endregion


    #region METHODS[==================]

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

    private void DownloadFormat()
    {
        try
        {
            string csv = string.Empty;

            csv = "COST_CENTER_CODE" + ',';
            csv += "COST_CENTER_AMOUNT" + ',';
            csv += "\r\n";

            string fileName = "Cost_Center_Format";
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

    private void GetCostCenterFile()
    {
        try
        {
            if (ddlCompany.SelectedIndex > 0)
            {
                unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            }

            dsCostCenter = objReports.GetCostCenterDetails("", Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM"), unitId, unitName);

            string fileName = string.Empty;

            if (fileUploadCostCenter.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadCostCenter.PostedFile.FileName))
                {
                    #region CREATE_TABLE

                    dtTempCostCenter.Columns.Add("COST_CENTER_ID", typeof(string));
                    dtTempCostCenter.Columns.Add("ORDER_NO", typeof(string));
                    dtTempCostCenter.Columns.Add("COST_CENTER_NAME", typeof(string));
                    dtTempCostCenter.Columns.Add("COST_CENTER_CODE", typeof(string));
                    dtTempCostCenter.Columns.Add("COST_CENTER_AMOUNT", typeof(string));
                    dtTempCostCenter.Columns.Add("DIRECT_BILLING", typeof(string));
                    dtTempCostCenter.Columns.Add("PARTIAL_BILLING", typeof(string));
                    dtTempCostCenter.Columns.Add("NET_AMOUNT", typeof(string));
                    dtTempCostCenter.Columns.Add("MONTH", typeof(string));
                    dtTempCostCenter.Columns.Add("UNIT_ID", typeof(string));
                    dtTempCostCenter.Columns.Add("UNIT", typeof(string));
                    dtTempCostCenter.Columns.Add("SR_NO", typeof(string));
                    #endregion

                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadCostCenter.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtTempCostCenter.Rows.Add();

                            int i = 0;
                            string dtColunValue = string.Empty;

                            foreach (string cell in row.Split(','))
                            {
                                dtColunValue = cell.Trim();

                                if (i <= (dtTempCostCenter.Columns.Count - 1))
                                {
                                    if (!string.IsNullOrEmpty(dtColunValue))
                                        dtTempCostCenter.Rows[dtTempCostCenter.Rows.Count - 1][i + 3] = dtColunValue;
                                    else
                                        dtTempCostCenter.Rows[dtTempCostCenter.Rows.Count - 1][i + 3] = string.Empty;

                                    i++;
                                }
                            }
                        }
                    }

                    dtTempCostCenter.Rows.RemoveAt(0);
                    int count = 0;
                    foreach (DataRow dr in dtTempCostCenter.Rows)
                    {
                        string ccAmtTxt = "0.00";
                        count++;
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["COST_CENTER_AMOUNT"])) && dr["COST_CENTER_AMOUNT"] != DBNull.Value && Convert.ToString(dr["COST_CENTER_AMOUNT"]).Contains("."))
                        {
                            ccAmtTxt = Convert.ToString(dr["COST_CENTER_AMOUNT"]);
                            string[] strCAmtTxt = ccAmtTxt.Split('.');
                            if (strCAmtTxt[1].Length > 2)
                            {
                                dr["COST_CENTER_AMOUNT"] = Math.Round(Convert.ToDouble(ccAmtTxt), 2);
                            }
                            else
                            {
                                dr["COST_CENTER_AMOUNT"] = ccAmtTxt;
                            }
                        }
                        else
                        {
                            ccAmtTxt = Convert.ToString(dr["COST_CENTER_AMOUNT"] + ".00");
                            dr["COST_CENTER_AMOUNT"] = ccAmtTxt;
                        }

                        dr["SR_NO"] = count;
                        dr["UNIT_ID"] = Convert.ToString(ddlCompany.SelectedValue);
                        dr["UNIT"] = Convert.ToString(ddlCompany.SelectedItem.Text);
                        dr["MONTH"] = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");

                    }

                    foreach (DataRow dr in dtTempCostCenter.Rows)
                    {
                        int costCenterID = 0;
                        string costCenterCode = string.Empty;
                        string costCenterName = string.Empty;
                        string orderNo = string.Empty;
                        string directBilling = "0.00";
                        string partialBilling = "0.00";
                        string month = string.Empty;
                        int unitID = 0;
                        string unit = string.Empty;

                        costCenterCode = Convert.ToString(dr["COST_CENTER_CODE"]);
                        month = Convert.ToDateTime(dr["MONTH"]).ToString("yyyy-MM");
                        unitID = Convert.ToInt32(dr["UNIT_ID"]);

                        if (dsCostCenter.Tables.Count > 0)
                        {
                            if (dsCostCenter.Tables[0].Rows.Count > 0)
                            {
                                Session["DT0_COSTCENTER"] = dsCostCenter.Tables[0];

                                foreach (DataRow drn in dsCostCenter.Tables[0].Select("COST_CENTER_CODE='" + costCenterCode + "' AND MONTH='" + month + "' AND UNIT_ID='" + unitID + "'"))
                                {
                                    if (drn["COST_CENTER_ID"] != DBNull.Value && Convert.ToInt32(drn["COST_CENTER_ID"]) > 0)
                                        costCenterID = Convert.ToInt32(drn["COST_CENTER_ID"]);
                                    else
                                        costCenterID = 0;

                                    if (drn["COST_CENTER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["COST_CENTER_NAME"])))
                                        costCenterName = Convert.ToString(drn["COST_CENTER_NAME"]);
                                    else
                                        costCenterName = string.Empty;

                                    if (drn["ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ORDER_NO"])))
                                        orderNo = Convert.ToString(drn["ORDER_NO"]);
                                    else
                                        orderNo = string.Empty;

                                    if (drn["DIRECT_BILLING"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["DIRECT_BILLING"])))
                                        directBilling = Convert.ToString(drn["DIRECT_BILLING"]);
                                    else
                                        directBilling = "0.00";

                                    if (drn["PARTIAL_BILLING"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["PARTIAL_BILLING"])))
                                        partialBilling = Convert.ToString(drn["PARTIAL_BILLING"]);
                                    else
                                        partialBilling = "0.00";
                                }
                            }
                            else
                            {
                                Session["DT0_COSTCENTER"] = null;
                            }

                          
                            if (dsCostCenter.Tables[1].Rows.Count > 0)
                            {                                
                                foreach (DataRow drn in dsCostCenter.Tables[1].Select("COST_CENTER_CODE='" + costCenterCode + "'"))
                                {                                   
                                    if (drn["COST_CENTER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["COST_CENTER_NAME"])))
                                        costCenterName = Convert.ToString(drn["COST_CENTER_NAME"]);
                                    else
                                        costCenterName = string.Empty;

                                    if (drn["ORDER_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drn["ORDER_NO"])))
                                        orderNo = Convert.ToString(drn["ORDER_NO"]);
                                    else
                                        orderNo = string.Empty;
                                }
                            }
                            
                            if (costCenterID > 0)
                                dr["COST_CENTER_ID"] = costCenterID;
                            else
                                dr["COST_CENTER_ID"] = 0;

                            if (!string.IsNullOrEmpty(costCenterName))
                                dr["COST_CENTER_NAME"] = costCenterName;
                            else
                                dr["COST_CENTER_NAME"] = string.Empty;

                            if (!string.IsNullOrEmpty(orderNo))
                                dr["ORDER_NO"] = orderNo;
                            else
                                dr["ORDER_NO"] = string.Empty;

                            dr["DIRECT_BILLING"] = directBilling;
                            dr["PARTIAL_BILLING"] = partialBilling;

                        }
                    }


                    if (dtTempCostCenter.Rows.Count > 0)
                    {
                        Session["DT_TEMP_COSTCENTER"] = dtTempCostCenter;
                        gvCostCenter.DataSource = dtTempCostCenter;
                        gvCostCenter.DataBind();
                        lblRecords.Text = "Records[" + dtTempCostCenter.Rows.Count + "], Already Exists[" + existedRecrdsCount + "]";
                        hdExistedRecords.Value = existedRecrdsCount.ToString();
                    }
                    else
                    {
                        Session["DT_TEMP_COSTCENTER"] = null;
                        gvCostCenter.DataSource = null;
                        gvCostCenter.DataBind();
                        hdExistedRecords.Value = "0";
                    }
                }
                else
                {
                    fileName = string.Empty;
                }
            }
            else
            {
                fileName = string.Empty;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private void ImportCostCenterFile()
    {
        try
        {
            string updateQuery = string.Empty;
            int costCenterID = 0;
            string costCenterCode = string.Empty;
            string costCenterName = string.Empty;
            string orderNo = string.Empty;
            double costCenterAmount = 0;
            double directBilling = 0;
            double partialBilling = 0;
            double netAmount = 0;
            string month = string.Empty;
            int srNo = 0;
            int unitID = 0;
            string srNoForRemoval = string.Empty;
            int count = 0;

            DataTable dtCostCenter = new DataTable();
            dtCostCenter.Columns.Add("COST_CENTER_CODE", typeof(string));
            dtCostCenter.Columns.Add("COST_CENTER_NAME", typeof(string));
            dtCostCenter.Columns.Add("ORDER_NO", typeof(string));
            dtCostCenter.Columns.Add("COST_CENTER_AMOUNT", typeof(double));
            dtCostCenter.Columns.Add("DIRECT_BILLING", typeof(string));
            dtCostCenter.Columns.Add("PARTIAL_BILLING", typeof(string));
            dtCostCenter.Columns.Add("NET_AMOUNT", typeof(string));
            dtCostCenter.Columns.Add("MONTH", typeof(string));
            dtCostCenter.Columns.Add("UNIT_ID", typeof(Int32));
            dtCostCenter.Columns.Add("CREATED_BY", typeof(Int32));

            int value = 0;
            foreach (GridViewRow gr in gvCostCenter.Rows)
            {
                DataRow dr = dtCostCenter.NewRow();

                Label lblCostCenterID = (Label)gr.FindControl("lblCostCenterID");
                Label lblCostCenterCode = (Label)gr.FindControl("lblCostCenterCode");
                Label lblCostCenterName = (Label)gr.FindControl("lblCostCenterName");
                Label lblOrderNo = (Label)gr.FindControl("lblOrderNo");
                TextBox txtCostCenterAmount = (TextBox)gr.FindControl("txtCostCenterAmount");
                TextBox txtDirectBilling = (TextBox)gr.FindControl("txtDirectBilling");
                TextBox txtPartialBilling = (TextBox)gr.FindControl("txtPartialBilling");
                TextBox txtNetAmount = (TextBox)gr.FindControl("txtNetAmount");
                Label lblMonth = (Label)gr.FindControl("lblMonth");
                Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                Label lblSRNo = (Label)gr.FindControl("lblSRNo");

                if (Convert.ToInt32(lblCostCenterID.Text) > 0)
                    costCenterID = Convert.ToInt32(lblCostCenterID.Text);
                else
                    costCenterID = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCostCenterCode.Text)))
                    costCenterCode = Convert.ToString(lblCostCenterCode.Text);
                else
                    costCenterCode = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCostCenterName.Text)))
                    costCenterName = Convert.ToString(lblCostCenterName.Text);
                else
                    costCenterName = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblOrderNo.Text)))
                    orderNo = Convert.ToString(lblOrderNo.Text);
                else
                    orderNo = string.Empty;

                if (Convert.ToDouble(txtCostCenterAmount.Text) > 0)
                    costCenterAmount = Convert.ToDouble(txtCostCenterAmount.Text);
                else
                    costCenterAmount = 0;

                if (Convert.ToDouble(txtDirectBilling.Text) > 0)
                    directBilling = Convert.ToDouble(txtDirectBilling.Text);
                else
                    directBilling = 0;

                partialBilling = Convert.ToDouble(txtPartialBilling.Text);

                netAmount = Convert.ToDouble(txtNetAmount.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblMonth.Text)))
                    month = Convert.ToDateTime(lblMonth.Text).ToString("yyyy-MM");
                else
                    month = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblUnitID.Text)))
                    unitID = Convert.ToInt32(lblUnitID.Text);
                else
                    unitID = 0;

                srNo = Convert.ToInt32(lblSRNo.Text);

                if (costCenterID > 0)
                {
                    count++;
                    srNoForRemoval += srNo + ",";
                    updateQuery += "UPDATE tblCostCenter set COST_CENTER_AMOUNT ='" + costCenterAmount + "', DIRECT_BILLING ='" + directBilling + "', PARTIAL_BILLING ='" + partialBilling + "', NET_AMOUNT='" + netAmount + "', MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", MODIFIED_ON= GETDATE() WHERE COST_CENTER_ID=" + costCenterID + ";" + Environment.NewLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(orderNo))
                    {
                        count++;
                        srNoForRemoval += srNo + ",";

                        dr["COST_CENTER_CODE"] = costCenterCode;
                        dr["COST_CENTER_NAME"] = costCenterName;
                        dr["ORDER_NO"] = orderNo;
                        dr["COST_CENTER_AMOUNT"] = costCenterAmount;
                        dr["DIRECT_BILLING"] = directBilling;
                        dr["PARTIAL_BILLING"] = partialBilling;
                        dr["NET_AMOUNT"] = netAmount;
                        dr["MONTH"] = month;
                        dr["UNIT_ID"] = unitID;
                        dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);
                        dtCostCenter.Rows.Add(dr);
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


            value = objReports.ImportCostCenterFile(dtCostCenter, updateQuery);

            if (value > 0)
            {
                SuccessMessage(count + " Records Imported successfully.");
                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvCostCenter.DataSource = null;
                    gvCostCenter.DataBind();
                    lblRecords.Text = "Records[" + gvCostCenter.Rows.Count + "]";
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
            dtTempCostCenter = (DataTable)Session["DT_TEMP_COSTCENTER"];
            string[] strNoForRemoval = srNoForRemoval.Split(',');
            foreach (string item in strNoForRemoval)
            {
                foreach (DataRow dr in dtTempCostCenter.Select("SR_NO='" + item + "'"))
                {
                    dtTempCostCenter.Rows.Remove(dr);
                }
            }
            if (dtTempCostCenter.Rows.Count > 0)
            {
                gvCostCenter.DataSource = dtTempCostCenter;
                gvCostCenter.DataBind();
            }
            else
            {
                gvCostCenter.DataSource = null;
                gvCostCenter.DataBind();
            }
            lblRecords.Text = "Records[" + gvCostCenter.Rows.Count + "]";
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
