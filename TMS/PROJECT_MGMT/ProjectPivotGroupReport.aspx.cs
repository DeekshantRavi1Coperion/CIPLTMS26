using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MGMT_ProjectPivotGroupReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();
    DataSet dsProjectPivotGroupReport = new DataSet();
    DataSet dsUnit = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["ppgreport"] = null;
                Session["jobnoandrevisionno"] = objProject.GetDetailsBySP("sp_get_job_no_and_revision_no");
                HidePanel();
                BindJobNo();
                BindUnit();
                ddlRevisionNo.Items.Clear();
                ddlRevisionNo.Items.Insert(0, "All");
                ddlRevisionNo.SelectedIndex = 0;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlJOBNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJOBNo.SelectedIndex > 0)
        {
            BindRevisionNo(Convert.ToString(ddlJOBNo.SelectedValue));
        }
        else
        {
            ddlRevisionNo.Items.Clear();
            ddlRevisionNo.Items.Insert(0, "All");
            ddlRevisionNo.SelectedIndex = 0;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        pnlMsg.Visible = false;
        GetProjectPivotGroupReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvProjectPivotGroupList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["ppgreport"];
            ToCSVNew01(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    protected void gvProjectPivotGroupList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        HidePanel();
        gvProjectPivotGroupList.PageIndex = e.NewPageIndex;
        GetProjectPivotGroupReport();
    }

    protected void gvProjectPivotGroupList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HidePanel();
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


    #region METHODS[=======================]

    private void BindJobNo()
    {
        try
        {
            DataSet dsJobNo = new DataSet();
            dsJobNo = (DataSet)Session["jobnoandrevisionno"];
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                ddlJOBNo.DataSource = dsJobNo.Tables[0];
                ddlJOBNo.DataTextField = "JOB_NO";
                ddlJOBNo.DataValueField = "JOB_NO";
                ddlJOBNo.DataBind();
                ddlJOBNo.Items.Insert(0, "SELECT");
                ddlJOBNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

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

    private void BindRevisionNo(string jobNo)
    {
        try
        {
            DataSet dsRevisionNo = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add("REVISION_NO", typeof(string));

            dsRevisionNo = (DataSet)Session["jobnoandrevisionno"];
            if (dsRevisionNo.Tables.Count > 0 && dsRevisionNo.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in dsRevisionNo.Tables[1].Select("JOB_NO='" + jobNo + "'"))
                {
                    DataRow drr = dt.NewRow();
                    drr["REVISION_NO"] = dr["REVISION_NO"];
                    dt.Rows.Add(drr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                ddlRevisionNo.DataSource = dt;
                ddlRevisionNo.DataTextField = "REVISION_NO";
                ddlRevisionNo.DataValueField = "REVISION_NO";
                ddlRevisionNo.DataBind();
                ddlRevisionNo.Items.Insert(0, "All");
                ddlRevisionNo.SelectedIndex = 0;
            }
            else
            {
                ddlRevisionNo.Items.Clear();
                ddlRevisionNo.Items.Insert(0, "All");
                ddlRevisionNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetProjectPivotGroupReport()
    {
        try
        {
            string startDate = string.Empty;
            string endDate = string.Empty;
            string jobNo = string.Empty;
            int revisionNo = 0;
            int unitID = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (ddlJOBNo.SelectedIndex > 0)
                jobNo = ddlJOBNo.SelectedItem.Text;
            else
                jobNo = string.Empty;

            if (ddlRevisionNo.SelectedIndex > 0)
                revisionNo = Convert.ToInt32(ddlRevisionNo.SelectedValue);
            else
                revisionNo = 0;

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);
            else
                unitID = 0;

            dsProjectPivotGroupReport = objProject.GetProjectPivotGroupReport(startDate, endDate, jobNo, revisionNo, unitID);
            if (dsProjectPivotGroupReport.Tables.Count > 0 && dsProjectPivotGroupReport.Tables[0].Rows.Count > 0)
            {
                Session["ppgreport"] = dsProjectPivotGroupReport;
                gvProjectPivotGroupList.DataSource = dsProjectPivotGroupReport.Tables[0];
                gvProjectPivotGroupList.DataBind();
            }
            else
            {
                Session["ppgreport"] = null;
                gvProjectPivotGroupList.DataSource = null;
                gvProjectPivotGroupList.DataBind();
            }
            lblRecords.Text = "Records[" + gvProjectPivotGroupList.Rows.Count + "]";
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

            string fileName = "ProjectPivotGroupReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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