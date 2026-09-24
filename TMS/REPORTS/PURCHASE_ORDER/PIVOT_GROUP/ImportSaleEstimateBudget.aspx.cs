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
using System.Collections.Generic;

public partial class REPORTS_PURCHASE_ORDER_PIVOT_GROUP_ImportSaleEstimateBudget : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.Reports objReports = new BAL.Reports();
    DataTable dtSaleEstimateBudget = new DataTable();
    DataTable dt1 = new DataTable();
    DataSet dsPivotGroupList = new DataSet();

    int srNo = 0;
    int recordID = 0;
    string pivotGroup = string.Empty;
    string pivotGroupDesc = string.Empty;
    string jobNo = string.Empty;
    double totalAmount = 0;
    string tableName = string.Empty;


    int count = 0;
    string srNoForRemoval = string.Empty;

    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                //
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

    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        GetSaleEstimateBudgetDetails();
    }

    protected void gvSaleEstimateBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string userNameID = "0";
                string drawingTypeID = "0";

                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");
                Label lblPivotGroup = (Label)e.Row.FindControl("lblPivotGroup");
                Label lblPivotGroupDesc = (Label)e.Row.FindControl("lblPivotGroupDesc");
                Label lblJobNo = (Label)e.Row.FindControl("lblJobNo");


                if (Convert.ToInt32(lblRecordID.Text) > 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightSeaGreen;
                    }
                }

                if (string.IsNullOrEmpty(Convert.ToString(lblPivotGroup.Text)) ||
                    string.IsNullOrEmpty(Convert.ToString(lblPivotGroupDesc.Text)) ||
                    string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvSaleEstimateBudget.Rows.Count > 0)
        {
            ImportSaleEstimateBudget();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }


    #endregion


    #region METHODS[==================]


    private void DownloadFormat()
    {
        try
        {
            DataTable dtPg = new DataTable();

            dtPg.Columns.Add("PIVOT_GROUP", typeof(string));
            dtPg.Columns.Add("PIVOT_GROUP_DESCRIPTION", typeof(string));
            dtPg.Columns.Add("JOB_NO", typeof(string));
            dtPg.Columns.Add("TOTAL_AMOUNT", typeof(double));

            string[] strItems = null;
            dsPivotGroupList = objReports.GetPivotGroupList();

            if (dsPivotGroupList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPivotGroupList.Tables[0].Rows)
                {
                    if (dr["PIVOT_GROUP"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PIVOT_GROUP"])))
                    {
                        strItems = Convert.ToString(dr["PIVOT_GROUP"]).Split(';');
                    }
                }
            }

            if (strItems.Length > 0)
            {
                foreach (string item in strItems)
                {
                    if (!string.IsNullOrEmpty(item.Trim()))
                    {
                        DataRow dr = dtPg.NewRow();

                        if (item.Contains("#"))
                            dr["PIVOT_GROUP"] = Convert.ToString(item.Split('#')[0]).Trim();
                        else
                            dr["PIVOT_GROUP"] = item.Trim();

                        dr["PIVOT_GROUP_DESCRIPTION"] = item.Trim();

                        if (!string.IsNullOrEmpty(txtJobNo.Text))
                            dr["JOB_NO"] = Convert.ToString(txtJobNo.Text).ToUpper();
                        else dr["JOB_NO"] = string.Empty;

                        dr["TOTAL_AMOUNT"] = 0;

                        dtPg.Rows.Add(dr);
                    }
                }
            }

            string csv = string.Empty;
            if (dtPg.Rows.Count > 0)
            {

                foreach (DataRow dr in dtPg.Rows)
                {                    
                    dr["PIVOT_GROUP_DESCRIPTION"] = Convert.ToString(dr["PIVOT_GROUP_DESCRIPTION"]).Replace("#", " - ");
                }

                foreach (DataColumn column in dtPg.Columns)
                {
                    csv += column.ColumnName + ',';
                }
                csv += "\r\n";

                foreach (DataRow row in dtPg.Rows)
                {
                    foreach (DataColumn column in dtPg.Columns)
                    {
                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                    }
                    csv += "\r\n";
                }
            }
            string fileName = "Sale_Estimate_Budget_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
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

    private void GetSaleEstimateBudgetDetails()
    {
        try
        {
            #region CREATE_TABLE

            DataTable dtNewList = new DataTable();

            dtNewList.Columns.Add("SR_NO", typeof(string));
            dtNewList.Columns.Add("RECORD_ID", typeof(string));
            dtNewList.Columns.Add("PIVOT_GROUP", typeof(string));
            dtNewList.Columns.Add("PIVOT_GROUP_DESCRIPTION", typeof(string));
            dtNewList.Columns.Add("JOB_NO", typeof(string));
            dtNewList.Columns.Add("TOTAL_AMOUNT", typeof(string));
            dtNewList.Columns.Add("TABLE_NAME", typeof(string));

            #endregion


            DataSet dsExistedPivotGroupList = new DataSet();

            dsExistedPivotGroupList = objReports.GetExistedPivotGroupList(txtJobNo.Text.Trim());


            if (fileUploadSaleEstimateBudget.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadSaleEstimateBudget.PostedFile.FileName))
                {
                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadSaleEstimateBudget.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();

                    foreach (string row in csvData.Split('\n'))
                    {
                        dtNewList.Rows.Add();
                        int i = 0;
                        string dtColunValue = string.Empty;
                        foreach (string cell in row.Split(','))
                        {
                            dtColunValue = cell.Trim();

                            if (i <= (dtNewList.Columns.Count - 1))
                            {
                                if (!string.IsNullOrEmpty(dtColunValue))
                                    dtNewList.Rows[dtNewList.Rows.Count - 1][i + 2] = dtColunValue;
                                else
                                    dtNewList.Rows[dtNewList.Rows.Count - 1][i + 2] = string.Empty;

                                i++;
                            }
                        }
                    }
                }
                else
                {
                    ExceptionMessage("No data found..!!!");
                    lblRecords.Text = "Records[0], Already Exists[0]";
                    return;
                }
            }
            else
            {
                ExceptionMessage("No data found..!!!");
                lblRecords.Text = "Records[0], Already Exists[0]";
                return;
            }

            int count = 0;
            int existedRecrdsCount = 0;
            if (dtNewList.Rows.Count > 0)
            {
                dtNewList.Rows.RemoveAt(0);
                dtNewList.Rows.RemoveAt(dtNewList.Rows.Count - 1);

                foreach (DataRow drn in dtNewList.Rows)
                {
                    count++;
                    drn["SR_NO"] = count;
                    drn["RECORD_ID"] = 0;
                }

                if (dsExistedPivotGroupList.Tables.Count > 0 && dsExistedPivotGroupList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drn in dtNewList.Rows)
                    {
                        foreach (var dre in dsExistedPivotGroupList.Tables[0].Select("PIVOT_GROUP='" + Convert.ToString(drn["PIVOT_GROUP"]) + "' AND JOB_NO='" + Convert.ToString(drn["JOB_NO"]) + "'"))
                        {
                            existedRecrdsCount++;

                            if (dre["RECORD_ID"] != DBNull.Value)
                                drn["RECORD_ID"] = dre["RECORD_ID"];
                            else drn["RECORD_ID"] = 0;

                            drn["TABLE_NAME"] = dre["TABLE_NAME"];
                        }
                    }
                }

                Session["dtSaleEstimateBudget"] = dtNewList;
                gvSaleEstimateBudget.DataSource = dtNewList;
                gvSaleEstimateBudget.DataBind();
                lblRecords.Text = Convert.ToString(dtNewList.Rows.Count);
                lblRecords.Text = "Records[" + Convert.ToString(dtNewList.Rows.Count) + "], Already Exists[" + existedRecrdsCount + "]";
            }
            else
            {
                Session["dtSaleEstimateBudget"] = null;
                gvSaleEstimateBudget.DataSource = null;
                gvSaleEstimateBudget.DataBind();
                ExceptionMessage("No data found..!!!");
                lblRecords.Text = "Records[0], Already Exists[0]";
                return;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private void ImportSaleEstimateBudget()
    {
        try
        {
            count = 0;
            srNoForRemoval = string.Empty;

            DataTable dtSaleEstimateBudget = new DataTable();

            dtSaleEstimateBudget.Columns.Add("PIVOT_GROUP", typeof(string));
            dtSaleEstimateBudget.Columns.Add("PIVOT_GROUP_DESCRIPTION", typeof(string));
            dtSaleEstimateBudget.Columns.Add("JOB_NO", typeof(string));
            dtSaleEstimateBudget.Columns.Add("TOTAL_AMOUNT", typeof(double));

            int value = 0;
            string updateQuery = string.Empty;
            foreach (GridViewRow gr in gvSaleEstimateBudget.Rows)
            {
                srNo = 0;
                recordID = 0;
                pivotGroup = string.Empty;
                pivotGroupDesc = string.Empty;
                jobNo = string.Empty;
                totalAmount = 0;
                tableName = string.Empty;



                DataRow dr = dtSaleEstimateBudget.NewRow();

                Label lblSRNo = (Label)gr.FindControl("lblSRNo");
                Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                Label lblPivotGroup = (Label)gr.FindControl("lblPivotGroup");
                Label lblPivotGroupDesc = (Label)gr.FindControl("lblPivotGroupDesc");
                Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                Label lblTableName = (Label)gr.FindControl("lblTableName");
                TextBox txtTotalAmount = (TextBox)gr.FindControl("txtTotalAmount");



                srNo = Convert.ToInt32(lblSRNo.Text);
                tableName = Convert.ToString(lblTableName.Text);

                if (Convert.ToInt32(lblRecordID.Text) > 0)
                    recordID = Convert.ToInt32(lblRecordID.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblPivotGroup.Text)))
                    pivotGroup = Convert.ToString(lblPivotGroup.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblPivotGroupDesc.Text)))
                    pivotGroupDesc = Convert.ToString(lblPivotGroupDesc.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                    jobNo = Convert.ToString(lblJobNo.Text).Split('.')[0];

                if (!string.IsNullOrEmpty(Convert.ToString(txtTotalAmount.Text)) && Convert.ToDouble(txtTotalAmount.Text) > 0)
                    totalAmount = Convert.ToDouble(txtTotalAmount.Text);




                if (!string.IsNullOrEmpty(pivotGroup))
                {
                    count++;
                }

                if (recordID > 0)
                {
                    updateQuery += "UPDATE " + tableName + " " +
                                    "SET PIVOT_GROUP='" + pivotGroup + "'," +
                                    "PIVOT_GROUP_DESCRIPTION='" + pivotGroupDesc + "'," +
                                    "JOB_NO='" + jobNo + "'," +
                                    "TOTAL_AMOUNT='" + totalAmount + "'," +
                                    "MODIFIED_BY='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'," +
                                    "MODIFIED_ON=GETDATE()" +
                                    "WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(pivotGroup))
                    {
                        srNoForRemoval += srNo + ",";

                        dr["PIVOT_GROUP"] = pivotGroup;
                        dr["PIVOT_GROUP_DESCRIPTION"] = pivotGroupDesc;
                        dr["JOB_NO"] = jobNo;
                        dr["TOTAL_AMOUNT"] = totalAmount;

                        dtSaleEstimateBudget.Rows.Add(dr);
                    }
                }
            }

            if (!string.IsNullOrEmpty(updateQuery))
                updateQuery = updateQuery.TrimEnd(';');


            value = objReports.ImportSaleEstimateBudget(dtSaleEstimateBudget, updateQuery, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage(count + " Records Imported successfully.");

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvSaleEstimateBudget.DataSource = null;
                    gvSaleEstimateBudget.DataBind();
                    lblRecords.Text = "Records[" + gvSaleEstimateBudget.Rows.Count + "]";
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
            dt1 = (DataTable)Session["dtSaleEstimateBudget"];
            string[] strNoForRemoval = srNoForRemoval.Split(',');
            foreach (string item in strNoForRemoval)
            {
                foreach (DataRow dr in dt1.Select("SR_NO='" + item + "'"))
                {
                    dt1.Rows.Remove(dr);
                }
            }
            if (dt1.Rows.Count > 0)
            {
                gvSaleEstimateBudget.DataSource = dt1;
                gvSaleEstimateBudget.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvSaleEstimateBudget.Rows.Count);
            }
            else
            {
                gvSaleEstimateBudget.DataSource = null;
                gvSaleEstimateBudget.DataBind();
                hdGVRowCount.Value = "0";
            }
            lblRecords.Text = "Records[" + gvSaleEstimateBudget.Rows.Count + "]";
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
