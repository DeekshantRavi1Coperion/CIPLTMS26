using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;

public partial class PROJECT_MRNQA_MRNQAReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsMRNList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsMRNStatus = new DataSet();
   

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string mrnNo = string.Empty;
    string poNo = string.Empty;   
    int unitID = 0;
    int statusID = 0;

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

                //hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtStartDateSearch.Text = hdStartDateSearch.Value;

                //hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtEndDateSearch.Text = hdEndDateSearch.Value;

                BindUnit();
                BindStatus();
                GetAssignedMRNReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetAssignedMRNReport();
    }

    protected void gvMRNList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                if (Convert.ToString(lblStatus.Text) == "New")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightYellow;
                    }
                }

                else if (Convert.ToString(lblStatus.Text) == "Checked")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }

                else if (Convert.ToString(lblStatus.Text) == "Rejected")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvMRNList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["MRN_LIST"];
            ExportToCSV(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
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

    private void BindStatus()
    {
        try
        {
            dsMRNStatus = objProject.GetMRNStatus();
            if (dsMRNStatus.Tables.Count > 0 && dsMRNStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsMRNStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetAssignedMRNReport()
    {
        try
        {
            fromDate = string.Empty;
            toDate = string.Empty;
            unitID = 0;
            mrnNo = string.Empty;
            poNo = string.Empty;
            statusID = 0;

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

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);



            dsMRNList = objProject.GetAssignedMRNReport(fromDate, toDate, unitID, mrnNo, poNo, statusID);

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

    private void ExportToCSV(DataTable dt)
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

            string fileName = "MRNReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
