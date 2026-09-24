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

public partial class REPORTS_SALE_ORDER_GROSS_MARGIN_ImportCostCenterTwo : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataTable dtCSVCostCenter = new DataTable();
    DataTable dtTempCostCenter = new DataTable();
    DataSet dsUnit = new DataSet();
    DataSet dsCostCenterStatus = new DataSet();
    DataSet dsCostCenter = new DataSet();
    int existedRecrdsCount = 0;

    int unitId = 0;
    string unitName = string.Empty;
    string costCenterCodeText = string.Empty;

    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["dtCostCenterStatus"] = null;
                Session["DS_COSTCENTER"] = null;
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
        GetCostCenterStatus();
        GetCostCenterFile();
    }

    protected void gvCostCenter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dtCostCenterStatus = new DataTable();
            if (Session["dtCostCenterStatus"] != null)
                dtCostCenterStatus = (DataTable)Session["dtCostCenterStatus"];
            else
            {
                GetCostCenterStatus();
                if (Session["dtCostCenterStatus"] != null)
                    dtCostCenterStatus = (DataTable)Session["dtCostCenterStatus"];
            }

            string costCenterCode = string.Empty;
            double costCenterAmount = 0;
            double directBilling = 0;
            double partialBilling = 0;
            double netAmount = 0;
            string month = string.Empty;
            int unitID = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCostCenterID = (Label)e.Row.FindControl("lblCostCenterID");
                Label lblCostCenterCode = (Label)e.Row.FindControl("lblCostCenterCode");
                Label lblOrderNo = (Label)e.Row.FindControl("lblOrderNo");
                TextBox txtCostCenterAmount = (TextBox)e.Row.FindControl("txtCostCenterAmount");
                TextBox txtDirectBilling = (TextBox)e.Row.FindControl("txtDirectBilling");
                TextBox txtPartialBilling = (TextBox)e.Row.FindControl("txtPartialBilling");
                TextBox txtNetAmount = (TextBox)e.Row.FindControl("txtNetAmount");
                Label lblMonth = (Label)e.Row.FindControl("lblMonth");
                Label lblUnitID = (Label)e.Row.FindControl("lblUnitID");
                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");


                if (dtCostCenterStatus.Rows.Count > 0)
                {
                    foreach (DataRow item in dtCostCenterStatus.Select("STATUS_NAME='" + lblStatusName.Text + "'"))
                    {
                        if (!string.IsNullOrEmpty(lblStatusID.Text) || Convert.ToInt32(lblStatusID.Text) == 0)
                            lblStatusID.Text = Convert.ToString(item["STATUS_ID"]);
                    }
                }

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

                if (Convert.ToInt32(lblCostCenterID.Text) > 0)
                {
                    existedRecrdsCount++;
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
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
            csv += "DIRECT_BILLING" + ',';
            csv += "PARTIAL_BILLING" + ',';
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

    private void GetCostCenterStatus()
    {
        try
        {
            dsCostCenterStatus = objReports.GetCostCenterStatus();
            if (dsCostCenterStatus.Tables.Count > 0 && dsCostCenterStatus.Tables[0].Rows.Count > 0)
            {
                Session["dtCostCenterStatus"] = dsCostCenterStatus.Tables[0];
            }
            else
            {
                Session["dtCostCenterStatus"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetCostCenterFile()
    {
        try
        {
            #region CREATE_TABLE

            dtCSVCostCenter.Columns.Add("COST_CENTER_CODE", typeof(string));
            dtCSVCostCenter.Columns.Add("COST_CENTER_AMOUNT", typeof(string));
            dtCSVCostCenter.Columns.Add("DIRECT_BILLING", typeof(string));
            dtCSVCostCenter.Columns.Add("PARTIAL_BILLING", typeof(string));

            #endregion

            if (ddlCompany.SelectedIndex > 0)
            {
                unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            }


            if (fileUploadCostCenter.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadCostCenter.PostedFile.FileName))
                {
                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadCostCenter.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row) && !Convert.ToString(row).Contains("COST_CENTER_CODE"))
                        {
                            string[] str = row.Split(',');
                            costCenterCodeText += "'" + str[0].ToString() + "',";


                            dtCSVCostCenter.Rows.Add();

                            int i = 0;
                            string dtColunValue = string.Empty;
                            foreach (string cell in row.Split(','))
                            {
                                dtColunValue = cell.Trim();

                                if (i <= (dtCSVCostCenter.Columns.Count - 1))
                                {
                                    if (!string.IsNullOrEmpty(dtColunValue))
                                        dtCSVCostCenter.Rows[dtCSVCostCenter.Rows.Count - 1][i] = dtColunValue;
                                    else
                                        dtCSVCostCenter.Rows[dtCSVCostCenter.Rows.Count - 1][i] = string.Empty;

                                    i++;
                                }
                            }
                        }
                    }

                    costCenterCodeText = costCenterCodeText.TrimEnd(',');
                }
                else
                {
                    costCenterCodeText = string.Empty;
                }

            }
            else
            {
                costCenterCodeText = string.Empty;
            }

            if (!string.IsNullOrEmpty(costCenterCodeText))
            {
                dsCostCenter = objReports.GetCostCenterDetails(costCenterCodeText, Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM"), unitId, unitName);
            }
            else
            {
                dsCostCenter = null;
            }


            if (dsCostCenter.Tables[0].Rows.Count > 0)
            {
                if (dtCSVCostCenter.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsCostCenter.Tables[0].Rows)
                    {
                        foreach (DataRow drcsv in dtCSVCostCenter.Select("COST_CENTER_CODE='" + Convert.ToString(dr1["COST_CENTER_CODE"]) + "'"))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(drcsv["COST_CENTER_AMOUNT"])) && Convert.ToDouble(drcsv["COST_CENTER_AMOUNT"]) > 0)
                                dr1["COST_CENTER_AMOUNT"] = Convert.ToDouble(drcsv["COST_CENTER_AMOUNT"]);
                            else dr1["COST_CENTER_AMOUNT"] = "0";

                            if (!string.IsNullOrEmpty(Convert.ToString(drcsv["DIRECT_BILLING"])) && Convert.ToDouble(drcsv["DIRECT_BILLING"]) > 0)
                                dr1["DIRECT_BILLING"] = Convert.ToDouble(drcsv["DIRECT_BILLING"]);
                            else dr1["DIRECT_BILLING"] = "0";

                            if (!string.IsNullOrEmpty(Convert.ToString(drcsv["PARTIAL_BILLING"])) && Convert.ToDouble(drcsv["PARTIAL_BILLING"]) > 0)
                                dr1["PARTIAL_BILLING"] = Convert.ToDouble(drcsv["PARTIAL_BILLING"]);
                            else dr1["PARTIAL_BILLING"] = "0";
                        }
                    }
                }

                Session["DS_COSTCENTER"] = dsCostCenter.Tables[0];
                gvCostCenter.DataSource = dsCostCenter.Tables[0];
                gvCostCenter.DataBind();
                lblRecords.Text = "Records[" + dsCostCenter.Tables[0].Rows.Count + "], Already Exists[" + existedRecrdsCount + "]";
                hdExistedRecords.Value = existedRecrdsCount.ToString();
            }
            else
            {
                Session["DS_COSTCENTER"] = null;
                gvCostCenter.DataSource = null;
                gvCostCenter.DataBind();
                hdExistedRecords.Value = "0";
                lblRecords.Text = "Records[0], Already Exists[0]";
                ExceptionMessage("No data found..!!!");
                return;
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
            int statusID = 0;
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
            dtCostCenter.Columns.Add("STATUS_ID", typeof(Int32));
            dtCostCenter.Columns.Add("CREATED_BY", typeof(Int32));

            int value = 0;
            foreach (GridViewRow gr in gvCostCenter.Rows)
            {
                DataRow dr = dtCostCenter.NewRow();

                Label lblCostCenterID = (Label)gr.FindControl("lblCostCenterID");
                Label lblCostCenterCode = (Label)gr.FindControl("lblCostCenterCode");
                Label lblCostCenterName = (Label)gr.FindControl("lblCostCenterName");
                Label lblStatusID = (Label)gr.FindControl("lblStatusID");
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

                if (!string.IsNullOrEmpty(Convert.ToString(txtPartialBilling.Text)))
                    partialBilling = Convert.ToDouble(txtPartialBilling.Text);
                else
                    partialBilling = 0;


                if (!string.IsNullOrEmpty(Convert.ToString(txtNetAmount.Text)))
                    netAmount = Convert.ToDouble(txtNetAmount.Text);
                else
                    netAmount = 0;




                if (!string.IsNullOrEmpty(Convert.ToString(lblMonth.Text)))
                    month = Convert.ToDateTime(lblMonth.Text).ToString("yyyy-MM");
                else
                    month = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblUnitID.Text)))
                    unitID = Convert.ToInt32(lblUnitID.Text);
                else
                    unitID = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblStatusID.Text)))
                    statusID = Convert.ToInt32(lblStatusID.Text);
                else
                    statusID = 0;

                srNo = Convert.ToInt32(lblSRNo.Text);

                if (costCenterID > 0)
                {
                    count++;
                    srNoForRemoval += srNo + ",";
                    updateQuery += "UPDATE tblCostCenter " +
                                    "set COST_CENTER_AMOUNT ='" + costCenterAmount +
                                    "', DIRECT_BILLING ='" + directBilling +
                                    "', PARTIAL_BILLING ='" + partialBilling +
                                    "', NET_AMOUNT='" + netAmount +
                                    "', STATUS_ID= " + statusID +
                                    " , MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", MODIFIED_ON= GETDATE() " +
                                    " WHERE COST_CENTER_ID=" + costCenterID + " AND MONTH='" + month + "';" + Environment.NewLine;
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
                        dr["STATUS_ID"] = statusID;
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
            dtTempCostCenter = (DataTable)Session["DS_COSTCENTER"];
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
