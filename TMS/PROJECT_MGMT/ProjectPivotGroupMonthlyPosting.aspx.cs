using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MGMT_ProjectPivotGroupMonthlyPosting : System.Web.UI.Page
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
                Session["jobnoandrevisionno"] = objProject.GetDetailsBySP("sp_get_job_no_and_revision_no");
                HidePanel();

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

                ddlJOBNo.Items.Clear();
                ddlJOBNo.Items.Insert(0, "SELECT");
                ddlJOBNo.SelectedIndex = 0;
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
        GetProjectPivotGroupForPosting();
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        PostProjectPivotGroup();
    }

    protected void gvProjectPivotGroupList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        HidePanel();
        gvProjectPivotGroupList.PageIndex = e.NewPageIndex;
        GetProjectPivotGroupForPosting();
    }

    protected void gvProjectPivotGroupList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HidePanel();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("STATUS", typeof(string));

                Label lblDifference = (Label)e.Row.FindControl("lblDifference");
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");

                if (Convert.ToDouble(lblDifference.Text) > 0)
                {
                    DataRow dr1 = dtTemp.NewRow();
                    dr1["STATUS"] = "Saving";
                    dtTemp.Rows.Add(dr1);

                    DataRow dr2 = dtTemp.NewRow();
                    dr2["STATUS"] = "Yet To Be Incurred";
                    dtTemp.Rows.Add(dr2);
                }
                else if (Convert.ToDouble(lblDifference.Text) == 0)
                {
                    ddlStatus.Items.Clear();
                    ddlStatus.Items.Insert(0, "SELECT");
                    ddlStatus.SelectedIndex = 0;
                }
                else
                {
                    DataRow dr = dtTemp.NewRow();
                    dr["STATUS"] = "Need To Check";
                    dtTemp.Rows.Add(dr);
                }

                if (dtTemp.Rows.Count > 0)
                {
                    ddlStatus.DataSource = dtTemp;
                    ddlStatus.DataTextField = "STATUS";
                    ddlStatus.DataValueField = "STATUS";
                    ddlStatus.DataBind();
                    ddlStatus.SelectedIndex = 0;
                }
                else
                {
                    ddlStatus.Items.Clear();
                    ddlStatus.Items.Insert(0, "SELECT");
                    ddlStatus.SelectedIndex = 0;
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

    private void GetProjectPivotGroupForPosting()
    {
        try
        {
            int serialNo = -1;
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

            dsProjectPivotGroupReport = objProject.GetProjectPivotGroupForPosting(startDate, endDate, jobNo, revisionNo, unitID);
            if (dsProjectPivotGroupReport.Tables.Count > 0 && dsProjectPivotGroupReport.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < dsProjectPivotGroupReport.Tables[0].Rows.Count; i++)
                {
                    dsProjectPivotGroupReport.Tables[0].Rows[i]["SERIAL_NO"] = Convert.ToString(i + 1);
                }


                Session["dt"] = dsProjectPivotGroupReport.Tables[0];
                gvProjectPivotGroupList.DataSource = dsProjectPivotGroupReport.Tables[0];
                gvProjectPivotGroupList.DataBind();
            }
            else
            {
                Session["dt"] = null;
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

    private void PostProjectPivotGroup()
    {
        try
        {
            int value = 0;
            int count = 0;
            string serialNos = string.Empty;
            string serialNo = string.Empty;

            string postingMonth = string.Empty;

            int revisionNo = 0;
            string jobNo = string.Empty;
            string pivotGroup = string.Empty;
            string grossMarginLine = string.Empty;
            double budgtedAmount = 0;
            double actualAmount = 0;
            double differenceAmount = 0;
            string status = string.Empty;

            string remarks = string.Empty;
            string udf1 = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;
            string unit = string.Empty;

            postingMonth = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MM");

            if (gvProjectPivotGroupList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvProjectPivotGroupList.Rows)
                {
                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblRevisionNo = (Label)gr.FindControl("lblRevisionNo");
                    Label lblJOBno = (Label)gr.FindControl("lblJOBno");
                    Label lblPivotGroup = (Label)gr.FindControl("lblPivotGroup");
                    Label lblGrossMarginLine = (Label)gr.FindControl("lblGrossMarginLine");
                    Label lblBudgtedAmount = (Label)gr.FindControl("lblBudgtedAmount");
                    Label lblActualAmount = (Label)gr.FindControl("lblActualAmount");
                    Label lblDifference = (Label)gr.FindControl("lblDifference");

                    DropDownList ddlStatus = (DropDownList)gr.FindControl("ddlStatus");
                    TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");
                    TextBox txtUDF1 = (TextBox)gr.FindControl("txtUDF1");
                    TextBox txtUDF2 = (TextBox)gr.FindControl("txtUDF2");
                    TextBox txtUDF3 = (TextBox)gr.FindControl("txtUDF3");
                    TextBox txtUDF4 = (TextBox)gr.FindControl("txtUDF4");
                    TextBox txtUDF5 = (TextBox)gr.FindControl("txtUDF5");

                    Label lblUnit = (Label)gr.FindControl("lblUnit");

                    serialNo = Convert.ToString(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblRevisionNo.Text)) && Convert.ToInt32(lblRevisionNo.Text) > 0)
                        revisionNo = Convert.ToInt32(lblRevisionNo.Text);
                    else
                        revisionNo = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblJOBno.Text)))
                        jobNo = Convert.ToString(lblJOBno.Text);
                    else
                        jobNo = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPivotGroup.Text)))
                        pivotGroup = Convert.ToString(lblPivotGroup.Text);
                    else
                        pivotGroup = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblGrossMarginLine.Text)))
                        grossMarginLine = Convert.ToString(lblGrossMarginLine.Text);
                    else
                        grossMarginLine = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblBudgtedAmount.Text)) && Convert.ToDouble(lblBudgtedAmount.Text) > 0)
                        budgtedAmount = Convert.ToDouble(lblBudgtedAmount.Text);
                    else
                        budgtedAmount = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblActualAmount.Text)) && Convert.ToDouble(lblActualAmount.Text) > 0)
                        actualAmount = Convert.ToDouble(lblActualAmount.Text);
                    else
                        actualAmount = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblDifference.Text)) && Convert.ToDouble(lblDifference.Text) > 0)
                        differenceAmount = Convert.ToDouble(lblDifference.Text);
                    else
                        differenceAmount = 0;

                    status = Convert.ToString(ddlStatus.SelectedItem.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                        remarks = Convert.ToString(txtRemarks.Text);
                    else
                        remarks = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtUDF1.Text)))
                        udf1 = Convert.ToString(txtUDF1.Text);
                    else
                        udf1 = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtUDF2.Text)))
                        udf2 = Convert.ToString(txtUDF2.Text);
                    else
                        udf2 = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtUDF3.Text)))
                        udf3 = Convert.ToString(txtUDF3.Text);
                    else
                        udf3 = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtUDF4.Text)))
                        udf4 = Convert.ToString(txtUDF4.Text);
                    else
                        udf4 = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtUDF5.Text)))
                        udf5 = Convert.ToString(txtUDF5.Text);
                    else
                        udf5 = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblUnit.Text)))
                        unit = Convert.ToString(lblUnit.Text);
                    else
                        unit = string.Empty;

                    value = objProject.PostProjectPivotGroup(revisionNo, jobNo, pivotGroup, grossMarginLine, budgtedAmount, actualAmount,
                                                            differenceAmount, status, postingMonth, remarks, udf1, udf2, udf3, udf4, udf5, unit,
                                                            Convert.ToInt32(Session["EMP_RECORD_ID"]));


                    if (value > 0)
                    {
                        count++;
                        serialNos += serialNo + ",";
                    }
                }
                if (count > 0)
                {
                    serialNos = serialNos.TrimEnd(',');
                    RemoveRecords(serialNos);
                    SuccessMessage(count + " Records Posted successfully.");
                }
            }
            else
            {
                ExceptionMessage("No data found..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRecords(string serialNos)
    {
        string[] strSerialNo = serialNos.Split(',');
        DataTable dtNew = (DataTable)Session["dt"];

        if (dtNew.Rows.Count > 0)
        {
            foreach (string sr in strSerialNo)
            {
                if (!string.IsNullOrEmpty(sr))
                {
                    foreach (DataRow drremove in dtNew.Select("SERIAL_NO='" + sr + "'"))
                    {
                        dtNew.Rows.Remove(drremove);
                    }
                }
            }

            if (dtNew.Rows.Count > 0)
            {
                Session["dt"] = dtNew;
                gvProjectPivotGroupList.DataSource = dtNew;
                gvProjectPivotGroupList.DataBind();
            }
            else
            {
                Session["dt"] = null;
                gvProjectPivotGroupList.DataSource = null;
                gvProjectPivotGroupList.DataBind();
            }
        }
        else
        {
            Session["dt"] = null;
        }
        lblRecords.Text = "Records[" + gvProjectPivotGroupList.Rows.Count + "]";
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