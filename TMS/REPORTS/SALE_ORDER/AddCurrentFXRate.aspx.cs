using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class REPORTS_SALE_ORDER_AddCurrentFXRate : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsDBDetails = new DataSet();
    DataSet dsCurrency = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HideMessagePanel();

                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

                GetCurrentCurrencyList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void gvCurrencyList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCurrentMonth = (Label)e.Row.FindControl("lblCurrentMonth");

                lblCurrentMonth.Text = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy") + "-" + Convert.ToDateTime(hdPostingMonth.Value).ToString("MM");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        GetCurrentCurrencyList();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        SaveCurrnetFXRate();
    }

    #endregion


    #region METHODS[=======================]

    private void GetCurrentCurrencyList()
    {
        try
        {
            string month1 = string.Empty;
            int month = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("MM"));
            int year = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy"));
            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;

            for (int i = 0; i < 12; i++)
            {
                if (i > 0)
                    month = month + 1;

                if (month > 12)
                {
                    month = 1;
                    year = year + 1;
                }

                if (i == 0)
                    month1 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");

                break;
            }

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

            dsCurrency = objReports.GetCurrentCurrencyList(month1, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);

            if (dsCurrency.Tables.Count > 0)
            {
                gvCurrencyList.DataSource = dsCurrency.Tables[0];
                gvCurrencyList.DataBind();
            }
            else
            {
                gvCurrencyList.DataSource = null;
                gvCurrencyList.DataBind();
            }

            lblRecords.Text = "Records[" + gvCurrencyList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SaveCurrnetFXRate()
    {
        try
        {
            int count = 0;
            string month = string.Empty;
            string currencyCode = string.Empty;
            string dated = string.Empty;
            string currentMonth = string.Empty;
            double fxRate = 0;


            if (gvCurrencyList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvCurrencyList.Rows)
                {

                    Label lblCurrencyCode = gr.FindControl("lblCurrencyCode") as Label;
                    Label lblDated = gr.FindControl("lblDated") as Label;
                    Label lblCurrentMonth = gr.FindControl("lblCurrentMonth") as Label;
                    TextBox txtFxRate = gr.FindControl("txtFxRate") as TextBox;

                    if (!string.IsNullOrEmpty(lblCurrencyCode.Text))
                        currencyCode = Convert.ToString(lblCurrencyCode.Text);
                    else
                        currencyCode = string.Empty;

                    if (!string.IsNullOrEmpty(lblDated.Text))
                        dated = Convert.ToDateTime(lblDated.Text).ToString("yyyy-MM-dd");
                    else
                        dated = string.Empty;

                    if (!string.IsNullOrEmpty(lblCurrentMonth.Text))
                        currentMonth = Convert.ToString(lblCurrentMonth.Text);
                    else
                        currentMonth = string.Empty;

                    if (!string.IsNullOrEmpty(txtFxRate.Text))
                        fxRate = Convert.ToDouble(txtFxRate.Text);
                    else
                        fxRate = 0;


                    int value = objReports.SaveCurrnetFXRate(currencyCode, dated, currentMonth, fxRate, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    if (value > 0)
                    {
                        count++;
                    }

                }

                if (count > 0)
                {
                    gvCurrencyList.DataSource = null;
                    gvCurrencyList.DataBind();
                    lblRecords.Text = "Records[" + gvCurrencyList.Rows.Count + "]";
                    SuccessMessage("FX Rates added successfully..!");
                }
            }
            else
            {
                ExceptionMessage("No datab found..!");
                return;
            }
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

    private void HideMessagePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}