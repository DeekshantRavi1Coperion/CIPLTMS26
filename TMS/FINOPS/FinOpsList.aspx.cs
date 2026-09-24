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

public partial class FINOPS_FinOpsList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.FinOps objFinOps = new BAL.FinOps();

    DataSet dsType = new DataSet();
    DataSet dsCostType = new DataSet();
    DataSet dsContigencyType = new DataSet();
    DataSet dsStatus1 = new DataSet();
    DataSet dsStatus2 = new DataSet();

    DataSet dsFinOpsList = new DataSet();

    string month = string.Empty;
    string invoiceNo = string.Empty;
    string jobNo = string.Empty;
    int typeID = 0;
    int costTypeID = 0;
    int contigencyTypeID = 0;
    string isVpocOrder = string.Empty;
    int status1ID = 0;
    int status2ID = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {                
                hdMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtMonth.Text = Convert.ToString(hdMonth.Value);

                BindType();
                BindCostType();
                BindContigencyType();
                BindStatus1();
                BindStatus2();

                GetFinOpsList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetFinOpsList();
    }

    protected void gvFinOpsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }

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

    #endregion


    #region METHODS[=========================]

    private void BindType()
    {
        try
        {
            dsType = objFinOps.GetFinOpsTypeList("");
            if (dsType.Tables.Count > 0 && dsType.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = dsType.Tables[0];
                ddlType.DataTextField = "TYPE";
                ddlType.DataValueField = "TYPE_ID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, "Select");
                ddlType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCostType()
    {
        try
        {
            dsCostType = objFinOps.GetOpsCostTypeList("");
            if (dsCostType.Tables.Count > 0 && dsCostType.Tables[0].Rows.Count > 0)
            {
                ddlCostType.DataSource = dsCostType.Tables[0];
                ddlCostType.DataTextField = "COST_TYPE";
                ddlCostType.DataValueField = "COST_TYPE_ID";
                ddlCostType.DataBind();
                ddlCostType.Items.Insert(0, "Select");
                ddlCostType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindContigencyType()
    {
        try
        {
            dsContigencyType = objFinOps.GetOpsContigencyTypeList("");
            if (dsContigencyType.Tables.Count > 0 && dsContigencyType.Tables[0].Rows.Count > 0)
            {
                ddlContigencyType.DataSource = dsContigencyType.Tables[0];
                ddlContigencyType.DataTextField = "CONTIGENCY_TYPE";
                ddlContigencyType.DataValueField = "CONTIGENCY_TYPE_ID";
                ddlContigencyType.DataBind();
                ddlContigencyType.Items.Insert(0, "Select");
                ddlContigencyType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatus1()
    {
        try
        {
            dsStatus1 = objFinOps.GetFinOpsStatus1List("");
            if (dsStatus1.Tables.Count > 0 && dsStatus1.Tables[0].Rows.Count > 0)
            {
                ddlStatus1.DataSource = dsStatus1.Tables[0];
                ddlStatus1.DataTextField = "STATUS1";
                ddlStatus1.DataValueField = "STATUS1_ID";
                ddlStatus1.DataBind();
                ddlStatus1.Items.Insert(0, "Select");
                ddlStatus1.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatus2()
    {
        try
        {
            dsStatus2 = objFinOps.GetFinOpsStatus2List("");
            if (dsStatus2.Tables.Count > 0 && dsStatus2.Tables[0].Rows.Count > 0)
            {
                ddlStatus2.DataSource = dsStatus2.Tables[0];
                ddlStatus2.DataTextField = "STATUS2";
                ddlStatus2.DataValueField = "STATUS2_ID";
                ddlStatus2.DataBind();
                ddlStatus2.Items.Insert(0, "Select");
                ddlStatus2.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetFinOpsList()
    {
        try
        {

            month = string.Empty;
            invoiceNo = string.Empty;
            jobNo = string.Empty;
            typeID = 0;
            costTypeID = 0;
            contigencyTypeID = 0;
            isVpocOrder = string.Empty;
            status1ID = 0;
            status2ID = 0;

            month = Convert.ToDateTime(txtMonth.Text).ToString("yyyy-MM");

            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                invoiceNo = txtInvoiceNo.Text;

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text;

            if (ddlType.SelectedIndex > 0)
                typeID = Convert.ToInt32(ddlType.SelectedValue);

            if (ddlCostType.SelectedIndex > 0)
                costTypeID = Convert.ToInt32(ddlType.SelectedValue);

            if (ddlContigencyType.SelectedIndex > 0)
                contigencyTypeID = Convert.ToInt32(ddlContigencyType.SelectedValue);

            if (ddlIsVPOCOrder.SelectedIndex > 0)
            {
                if (ddlIsVPOCOrder.SelectedItem.Text.ToUpper() == "YES")
                    isVpocOrder = "1";
                else if (ddlIsVPOCOrder.SelectedItem.Text.ToUpper() == "NO")
                    isVpocOrder = "0";
            }

            if (ddlStatus1.SelectedIndex > 0)
                status1ID = Convert.ToInt32(ddlStatus1.SelectedValue);

            if (ddlStatus2.SelectedIndex > 0)
                status2ID = Convert.ToInt32(ddlStatus2.SelectedValue);


            

            dsFinOpsList = objFinOps.GetFinOpsList(month,invoiceNo,jobNo,typeID,costTypeID,contigencyTypeID,isVpocOrder,status1ID,status2ID);
            if (dsFinOpsList.Tables.Count > 0 && dsFinOpsList.Tables[0].Rows.Count > 0)
            {                
                gvFinOpsList.DataSource = dsFinOpsList.Tables[0];
                gvFinOpsList.DataBind();
            }
            else
            {               
                gvFinOpsList.DataSource = null;
                gvFinOpsList.DataBind();
                ExceptionMessage("No data found...!!!");
            }
            lblRecords.Text = "Records[" + dsFinOpsList.Tables[0].Rows.Count + "]";
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

            string fileName = "FDMEE_Report" + Convert.ToDateTime(txtMonth.Text).ToString("MMM-yyyy");
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
