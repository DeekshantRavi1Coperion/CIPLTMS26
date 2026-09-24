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

public partial class FINANCE_FDMEEReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();

    DataSet dsFDMEE = new DataSet();

    int year = 0;
    int period = 0;
    int quarter = 0;
    string glAccount = string.Empty;
    string BSPLType = string.Empty;
    string HFMAccount = string.Empty;
    string type = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePAnel();
            if (!IsPostBack)
            {
                Session["DS_FDMEE"] = null;
                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetFDMEEList();
    }

    protected void gvFDMEEList_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvFDMEEList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblRecordID");
                Label lblYear = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblYear");
                Label lblPeriod = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblPeriod");
                Label lblQuarter = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblQuarter");
                Label lblGLAccount = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblGLAccount");
                Label lblDescription = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblDescription");
                Label lblBSPLType = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblBSPLType");
                Label lblHFMAccount = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblHFMAccount");
                Label lblHFMAccountDesc = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblHFMAccountDesc");
                Label lblType = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblType");
                Label lblCategory = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblCategory");
                Label lblICP = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblICP");
                Label lblHFMCustom1 = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblHFMCustom1");
                Label lblHFMNewCustom4 = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblHFMNewCustom4");
                Label lblAmount = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblAmount");
                Label lblSourceAmount = (Label)gvFDMEEList.Rows[rowindex].FindControl("lblSourceAmount");


                ViewState["recordID"] = Convert.ToInt32(lblRecordID.Text);

                if (e.CommandArgument == "PROPERTIES")
                {
                    //lblLegend.Text = "OA No [" + Convert.ToString(lblOANo.Text) + "]";

                    txtYearNew.Text = lblYear.Text;
                    txtPeriodNew.Text = lblPeriod.Text;
                    txtQuarterNew.Text = lblQuarter.Text;
                    txtGLAccountNew.Text = lblGLAccount.Text;
                    txtDescription.Text = lblDescription.Text;
                    txtBSPLTypeNew.Text = lblBSPLType.Text;
                    txtHFMAccountNew.Text = lblHFMAccount.Text;
                    txtHFMAccountDesc.Text = lblHFMAccountDesc.Text;
                    txtType.Text = lblType.Text;
                    txtCategory.Text = lblCategory.Text;
                    txtICP.Text = lblICP.Text;
                    txtHFMCustom1.Text = lblHFMCustom1.Text;
                    txtHFMNewCustom4.Text = lblHFMNewCustom4.Text;
                    txtAmount.Text = lblAmount.Text;
                    txtSourceAmount.Text = lblSourceAmount.Text;


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
        if (gvFDMEEList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["DS_FDMEE"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateFDMEE(Convert.ToInt32(ViewState["recordID"]));
    }

    #endregion


    #region METHODS[=========================]

    private void GetFDMEEList()
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
            
            if (ddlBSPLType.SelectedIndex > 0)
                BSPLType = ddlBSPLType.SelectedItem.Text;
            else
                BSPLType = string.Empty;

            if (!string.IsNullOrEmpty(txtHFMAccount.Text))
                HFMAccount = txtHFMAccount.Text;
            else
                HFMAccount = string.Empty;

            if (ddlType.SelectedIndex > 0)
                type = ddlType.SelectedItem.Text;
            else
                type = string.Empty;


            dsFDMEE = objReports.getFDMEEList(year, period, quarter, glAccount, BSPLType, HFMAccount, type);
            if (dsFDMEE.Tables.Count > 0 && dsFDMEE.Tables[0].Rows.Count > 0)
            {
                Session["DS_FDMEE"] = dsFDMEE;
                gvFDMEEList.DataSource = dsFDMEE.Tables[0];
                gvFDMEEList.DataBind();
            }
            else
            {
                Session["DS_FDMEE"] = null;
                gvFDMEEList.DataSource = null;
                gvFDMEEList.DataBind();
                ExceptionMessage("No data found...!!!");
            }
            lblRecords.Text = "Records[" + dsFDMEE.Tables[0].Rows.Count + "]";
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

            string fileName = "FDMEE_Report" + Convert.ToDateTime(hdPostingMonth.Value).ToString("MMM-yyyy");
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

    private void UpdateFDMEE(int recordID)
    {
        try
        {
            double amount = 0;
            double sourceAmount = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(txtAmount.Text)))
                amount = Convert.ToDouble(txtAmount.Text);
            else
                amount = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(txtSourceAmount.Text)))
                sourceAmount = Convert.ToDouble(txtSourceAmount.Text);
            else
                sourceAmount = 0;

            int value = objReports.UpdateFDMEE(recordID, amount, sourceAmount, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Updated successfully.");
                GetFDMEEList();
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
