using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

public partial class REPORTS_PURCHASE_BALANCE_ImportedGSTR2BCleanList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.PurchaseBalanceReports objPurchaseBalanceReports = new BAL.PurchaseBalanceReports();

    //DataSet dsUnit = new DataSet();
    DataSet dsDrawingNo = new DataSet();
    int recordID = 0;
    string jobNo = string.Empty;
    string drawingNo = string.Empty;
    string description = string.Empty;
    int quantity = 0;
    string UOM = string.Empty;
    string rqdDateByProjectTeam = string.Empty;
    string isActive = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["REPORT"] = null;
                //Session["dtUnit"] = null;
                //BindCompany();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void gvDesignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = new DataTable();

                //Label lblDrawingID = (Label)e.Row.FindControl("lblDrawingID");
                //Label lblDuplicateFlag = (Label)e.Row.FindControl("lblDuplicateFlag");
                //Label lblJOBUnitUD = (Label)e.Row.FindControl("lblJOBUnitUD");
                //DropDownList ddlJOBUnit = (DropDownList)e.Row.FindControl("ddlJOBUnit");

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                //if (Convert.ToInt32(lblDrawingID.Text) > 0)
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                //    }
                //}

                //if (Convert.ToInt32(lblDuplicateFlag.Text) > 0)
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                //    }
                //}


                //if (Session["dtUnit"] != null)
                //{
                //    dt = (DataTable)Session["dtUnit"];
                //}
                //else
                //{
                //    BindCompany();
                //    dt = (DataTable)Session["dtUnit"];
                //}

                //if (dt.Rows.Count > 0)
                //{
                //    ddlJOBUnit.DataSource = dt;
                //    ddlJOBUnit.DataTextField = "UNIT_NAME";
                //    ddlJOBUnit.DataValueField= "UNIT_ID";
                //    ddlJOBUnit.DataBind();
                //    if (Convert.ToInt32(lblJOBUnitUD.Text)>0)
                //    {
                //        ddlJOBUnit.SelectedValue = Convert.ToString(lblJOBUnitUD.Text);
                //    }
                //    else
                //    {
                //        ddlJOBUnit.SelectedIndex = 0;
                //    }                    
                //}
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetDetails();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvDesignDetails.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["REPORT"];
            ToCSVNew01(dt, "ImportedGSTR2B_");
        }
        else
        {
            ExceptionMessage("No data found!");
        }
    }

    #endregion


    #region METHODS[=========================]

    private void GetDetails()
    {
        try
        {
            DataTable dtDetails = new DataTable();

            string year = Convert.ToString(ddlYear.SelectedValue);
            string month = Convert.ToString(ddlMonth.SelectedValue);

            dtDetails = objPurchaseBalanceReports.GetImportedGSTR2BList(year, month);

            if (dtDetails.Rows.Count > 0)
            {
                Session["REPORT"] = dtDetails;
                gvDesignDetails.DataSource = dtDetails;
                gvDesignDetails.DataBind();
                lblRecords.Text = "Records[" + dtDetails.Rows.Count + "]";
            }
            else
            {
                Session["REPORT"] = null;
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
                lblRecords.Text = "Records[0]";
                ExceptionMessage("No data found..!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ToCSVNew01(DataTable dt, string fileNameS)
    {
        try
        {
            string csv = string.Empty;
            foreach (DataColumn column in dt.Columns)
            {
                csv += column.ColumnName + ',';
            }
            csv += "\r\n";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = fileNameS + DateTime.Now.ToString("dd_MMM_yyyy");
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
