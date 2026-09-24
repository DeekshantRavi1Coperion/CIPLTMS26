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

public partial class FINANCE_ONESTREAM_OneStreamPostedReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();

    DataSet dsOneStream = new DataSet();

    int year = 0;
    int period = 0;
    int quarter = 0;
    string glAccount = string.Empty;
    
    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePAnel();
            if (!IsPostBack)
            {
                Session["DS_OneStream"] = null;
                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

                GetOneStreamList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetOneStreamList();
    }

    protected void gvOneStreamList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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

    protected void gvOneStreamList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblRecordID");
                Label lblYear = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblYear");
                Label lblPeriod = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblPeriod");
                Label lblQuarter = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblQuarter");
                Label lblGLAccount = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblGLAccount");

                Label lblTMT = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblTMT");
                Label lblAC = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblAC");
                Label lblACT = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblACT");
                Label lblICT = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblICT");
                Label lblU3T = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblU3T");
                Label lblU4T = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblU4T");
                Label lblU7T = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblU7T");
                Label lblRawAmount = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblRawAmount");
                Label lblConvertedAmount = (Label)gvOneStreamList.Rows[rowindex].FindControl("lblConvertedAmount");
                
                ViewState["recordID"] = Convert.ToInt32(lblRecordID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    txtYearNew.Text = lblYear.Text;
                    txtPeriodNew.Text = lblPeriod.Text;
                    txtQuarterNew.Text = lblQuarter.Text;
                    txtTMTNew.Text = lblTMT.Text;
                    txtACNew.Text = lblAC.Text;
                    txtACTNew.Text = lblACT.Text;
                    txtICTNew.Text = lblICT.Text;
                    txtU3TNew.Text = lblU3T.Text;
                    txtU4TNew.Text = lblU4T.Text;
                    txtU7TNew.Text = lblU7T.Text;
                    txtFWNew.Text = lblGLAccount.Text;
                    txtRawAmountNew.Text = lblRawAmount.Text;
                    txtConvertedAmountNew.Text = lblConvertedAmount.Text;
                    

                    ModalPopupExtender1.Show();
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvOneStreamList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["DS_OneStream"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateOneStream(Convert.ToInt32(ViewState["recordID"]));
    }

    #endregion


    #region METHODS[=========================]

    private void GetOneStreamList()
    {
        try
        {
            year = Convert.ToDateTime(hdPostingMonth.Value).Year;
            period = Convert.ToDateTime(hdPostingMonth.Value).Month;



            if (ddlQuarter.SelectedIndex > 0)
                quarter = Convert.ToInt32(ddlQuarter.SelectedValue);
            else
                quarter = 0;

            if (!string.IsNullOrEmpty(txtGLAccount.Text))
                glAccount = txtGLAccount.Text;
            else
                glAccount = string.Empty;
                       
            dsOneStream = objReports.GetOneStreamList(year, period, quarter, glAccount);
            if (dsOneStream.Tables.Count > 0 && dsOneStream.Tables[0].Rows.Count > 0)
            {
                Session["DS_OneStream"] = dsOneStream;
                gvOneStreamList.DataSource = dsOneStream.Tables[0];
                gvOneStreamList.DataBind();
            }
            else
            {
                Session["DS_OneStream"] = null;
                gvOneStreamList.DataSource = null;
                gvOneStreamList.DataBind();
                ExceptionMessage("No data found...!!!");
            }
            lblRecords.Text = "Records[" + dsOneStream.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 1; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    csv += dt.Rows[i][j].ToString().Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "OneStream_Report" + Convert.ToDateTime(hdPostingMonth.Value).ToString("MMM-yyyy");
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

    private void UpdateOneStream(int recordID)
    {
        try
        {
            double rawAmount = 0;
            double convertedAmount = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(txtRawAmountNew.Text)))
                rawAmount = Convert.ToDouble(txtRawAmountNew.Text);
            

            if (!string.IsNullOrEmpty(Convert.ToString(txtConvertedAmountNew.Text)))
                convertedAmount = Convert.ToDouble(txtConvertedAmountNew.Text);
            

            int value = objReports.UpdateOneStream(recordID, rawAmount, convertedAmount, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Updated successfully.");
                GetOneStreamList();
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

    private void HidePAnel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}
