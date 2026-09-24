using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BAL;
using System.Security;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using WebListItem = System.Web.UI.WebControls.ListItem;

public partial class TOUR_AND_TRAVELS_TRAVEL_WeeklySiteReport : System.Web.UI.Page
{
    #region VARIABLES
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsTSNo = new DataSet();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindTourSanctionNumber();
                BindSecondGrid();
                BindFOCType();
                ToggleHeaderControls(false);
            }
        }
    }

    protected void ddlFOCType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SaveGridData();

        string selectedValue = ddlFOCType.SelectedValue.Trim();
        if (string.IsNullOrEmpty(selectedValue))
            return;

        string targetGridValue = "";
        if (selectedValue == "Chargeable")
        {
            targetGridValue = "FOC";
        }
        else
        {
            targetGridValue = selectedValue;
        }

        foreach (GridViewRow row in gvOTReport.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlGridFOC = (DropDownList)row.FindControl("ddlFOC");

                if (ddlGridFOC != null && ddlGridFOC.Enabled)
                {
                    System.Web.UI.WebControls.ListItem item = ddlGridFOC.Items.FindByValue(targetGridValue);
                    if (item != null)
                    {
                        ddlGridFOC.ClearSelection();
                        ddlGridFOC.SelectedValue = targetGridValue;
                    }
                }
            }
        }

        SaveGridData();
    }

    private void BindFOCType()
    {
        try
        {
            ddlFOCType.Items.Clear();
            ddlFOCType.Items.Add(new WebListItem("Select", ""));
            ddlFOCType.Items.Add(new WebListItem("Chargeable", "Chargeable"));
            ddlFOCType.Items.Add(new WebListItem("FOC-Warranty", "FOC-Warranty"));
            ddlFOCType.Items.Add(new WebListItem("FOC-Training", "FOC-Training"));
        }
        catch { }
    }

    private void ToggleHeaderControls(bool isEnabled)
    {
        bool isReadOnly = !isEnabled;

        txtStartDate.Enabled = isEnabled;
        txtStartDate.ReadOnly = isReadOnly;

        txtEndDate.Enabled = isEnabled;
        txtEndDate.ReadOnly = isReadOnly;

        txtCustName.Enabled = isEnabled;
        txtCustName.ReadOnly = isReadOnly;

        txtEndUserSite.Enabled = isEnabled;
        txtEndUserSite.ReadOnly = isReadOnly;

        txtEmployeeName.Enabled = isEnabled;
        txtEmployeeName.ReadOnly = isReadOnly;

        txtPoNo.Enabled = isEnabled;
        txtPoNo.ReadOnly = isReadOnly;

        txtJobNo.Enabled = isEnabled;
        txtJobNo.ReadOnly = isReadOnly;

        txtRemarks.Enabled = isEnabled;
        txtRemarks.ReadOnly = isReadOnly;

        ddlFOCType.Enabled = isEnabled;

        calenderIcon.Enabled = isEnabled;
        ImageButton1.Enabled = isEnabled;
    }

    protected void ddlTSNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTSNumber.SelectedIndex <= 0)
        {
            ToggleHeaderControls(false);
            ClearAllFields();
            return;
        }

        try
        {
            DataSet ds = objTourAndTravels.GetTSNDetails(Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["TOUR_SANCTION_NO"].ToString().Trim() != ddlTSNumber.SelectedValue.Trim())
                    continue;

                ToggleHeaderControls(true);

                txtStartDate.Text = dr["TOUR_START_DATE"].ToString();
                txtEndDate.Text = dr["TOUR_END_DATE"].ToString();
                txtCustName.Text = dr["CUSTOMER_NAME"].ToString();
                txtEndUserSite.Text = dr["END_USER_SITE"].ToString();
                txtEmployeeName.Text = dr["EMPLOYEE_NAME"].ToString();
                txtPoNo.Text = dr["COPERION_PO_NO"].ToString();
                txtJobNo.Text = dr["COPERION_JOB_NO"].ToString();
                txtRemarks.Text = dr["REMARKS"].ToString();

                if (dr["TOUR_START_DATE"] == DBNull.Value || dr["TOUR_END_DATE"] == DBNull.Value)
                    return;

                DateTime startDate = Convert.ToDateTime(dr["TOUR_START_DATE"]);
                DateTime endDate = Convert.ToDateTime(dr["TOUR_END_DATE"]);

                DataTable dt = new DataTable();
                dt.Columns.Add("DATE");
                dt.Columns.Add("DAY");
                dt.Columns.Add("ACTIVITY");
                dt.Columns.Add("START_TIME");
                dt.Columns.Add("FINISH_TIME");
                dt.Columns.Add("BREAK_TIME");
                dt.Columns.Add("ACTIVE_TIME");
                dt.Columns.Add("REMARKS");
                dt.Columns.Add("FOC_TYPE");
                dt.Columns.Add("WEEKEND_WORKING");
                dt.Columns.Add("IS_NEW_ROW");
                dt.Columns.Add("IS_EXTRA_ROW");
                dt.Columns.Add("IS_ENABLED");
                dt.Columns.Add("IS_USED");

                DateTime currentDate = startDate;
                while (currentDate <= endDate)
                {
                    DataRow row = dt.NewRow();
                    row["DATE"] = currentDate.ToString("dd-MMM-yyyy");
                    row["DAY"] = currentDate.ToString("dddd");
                    row["ACTIVITY"] = "";
                    row["START_TIME"] = "";
                    row["FINISH_TIME"] = "";
                    row["BREAK_TIME"] = "0";
                    row["ACTIVE_TIME"] = "0";
                    row["REMARKS"] = "";
                    row["FOC_TYPE"] = "";
                    row["WEEKEND_WORKING"] = "0";
                    row["IS_NEW_ROW"] = "0";
                    row["IS_EXTRA_ROW"] = "0";
                    row["IS_ENABLED"] = "1";
                    row["IS_USED"] = "1";
                    dt.Rows.Add(row);
                    currentDate = currentDate.AddDays(1);
                }

                ViewState["GridData"] = dt;
                gvOTReport.DataSource = dt;
                gvOTReport.DataBind();
                lblRecords.Text = "Records [" + dt.Rows.Count + "]";
                break;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void BindSecondGrid()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("SrNo");
        dt.Columns.Add("Date");
        dt.Columns.Add("Place");
        dt.Columns.Add("SiteIncharge");
        dt.Columns.Add("Designation");
        dt.Columns.Add("Signature");

        dt.Rows.Add("Acceptance 1", "", "", "", "", "");
        dt.Rows.Add("Acceptance 2", "", "", "", "", "");

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    private void BindTourSanctionNumber()
    {
        try
        {
            dsTSNo = objTourAndTravels.GetTSNDetails(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsTSNo.Tables.Count > 0 && dsTSNo.Tables[0].Rows.Count > 0)
            {
                ddlTSNumber.DataSource = dsTSNo.Tables[0];
                ddlTSNumber.DataTextField = "TOUR_SANCTION_NO";
                ddlTSNumber.DataValueField = "TOUR_SANCTION_NO";
                ddlTSNumber.DataBind();
                ddlTSNumber.Items.Insert(0, new WebListItem("Select", ""));
                ddlTSNumber.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvOTReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddRow")
        {
            SaveGridData();
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = ViewState["GridData"] as DataTable;

            if (dt == null || dt.Rows.Count == 0 || index < 0 || index >= dt.Rows.Count) return;

            DataRow existingRow = dt.Rows[index];
            DataRow newRow = dt.NewRow();

            newRow["DATE"] = existingRow["DATE"];
            newRow["DAY"] = existingRow["DAY"];
            newRow["IS_NEW_ROW"] = "1";
            newRow["IS_ENABLED"] = "1";
            newRow["ACTIVITY"] = "";
            newRow["START_TIME"] = "";
            newRow["FINISH_TIME"] = "";
            newRow["BREAK_TIME"] = "0";
            newRow["ACTIVE_TIME"] = "0";
            newRow["REMARKS"] = "";
            newRow["FOC_TYPE"] = "";
            newRow["WEEKEND_WORKING"] = existingRow["WEEKEND_WORKING"];

            int insertIndex = index + 1;
            while (insertIndex < dt.Rows.Count)
            {
                if (dt.Rows[insertIndex]["DATE"].ToString() != existingRow["DATE"].ToString()) break;
                insertIndex++;
            }

            dt.Rows.InsertAt(newRow, insertIndex);

            ViewState["GridData"] = dt;
            gvOTReport.DataSource = dt;
            gvOTReport.DataBind();
            lblRecords.Text = "Records[" + dt.Rows.Count + "]";
        }
        if (e.CommandName == "UseRow")
        {
            SaveGridData();
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = ViewState["GridData"] as DataTable;

            if (dt == null || index < 0 || index >= dt.Rows.Count) return;

            string isUsed = dt.Rows[index]["IS_USED"].ToString();

            if (isUsed == "0")
            {
                int sourceIndex = (index == 0) ? 1 : index - 1;
                if (sourceIndex < 0 || sourceIndex >= dt.Rows.Count) return;

                dt.Rows[index]["ACTIVITY"] = dt.Rows[sourceIndex]["ACTIVITY"];
                dt.Rows[index]["START_TIME"] = dt.Rows[sourceIndex]["START_TIME"];
                dt.Rows[index]["FINISH_TIME"] = dt.Rows[sourceIndex]["FINISH_TIME"];
                dt.Rows[index]["BREAK_TIME"] = dt.Rows[sourceIndex]["BREAK_TIME"];
                dt.Rows[index]["ACTIVE_TIME"] = dt.Rows[sourceIndex]["ACTIVE_TIME"];
                dt.Rows[index]["REMARKS"] = dt.Rows[sourceIndex]["REMARKS"];
                dt.Rows[index]["FOC_TYPE"] = dt.Rows[sourceIndex]["FOC_TYPE"];

                dt.Rows[index]["IS_ENABLED"] = "1";
                dt.Rows[index]["IS_USED"] = "1";
            }
            else
            {
                dt.Rows[index]["IS_ENABLED"] = "0";
                dt.Rows[index]["IS_USED"] = "0";
            }

            ViewState["GridData"] = dt;
            gvOTReport.DataSource = dt;
            gvOTReport.DataBind();
        }

        if (e.CommandName == "DeleteRow")
        {
            SaveGridData();
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = ViewState["GridData"] as DataTable;

            if (dt != null && index >= 0 && index < dt.Rows.Count)
            {
                if (dt.Rows[index]["IS_NEW_ROW"].ToString() == "1")
                {
                    dt.Rows.RemoveAt(index);
                    ViewState["GridData"] = dt;
                    gvOTReport.DataSource = dt;
                    gvOTReport.DataBind();
                    lblRecords.Text = "Records[" + dt.Rows.Count + "]";
                }
            }
        }
    }

    protected void gvOTReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            Button btnAddRow = (Button)e.Row.FindControl("btnAddRow");
            Button btnUseRow = (Button)e.Row.FindControl("btnUseRow");
            Button btnDeleteRow = (Button)e.Row.FindControl("btnDeleteRow");
            DropDownList ddlActivity = (DropDownList)e.Row.FindControl("ddlActivity");
            DropDownList ddlFOC = (DropDownList)e.Row.FindControl("ddlFOC");
            CheckBox chkWeekendWorking = (CheckBox)e.Row.FindControl("chkWeekendWorking");

            TextBox txtStart = (TextBox)e.Row.FindControl("txtStartTime");
            TextBox txtFinish = (TextBox)e.Row.FindControl("txtFinishTime");
            TextBox txtBreak = (TextBox)e.Row.FindControl("txtBreakTime");
            TextBox txtRemarksGrid = (TextBox)e.Row.FindControl("txtRemarksGrid");

            if (ddlActivity != null && drv["ACTIVITY"] != DBNull.Value)
            {
                var item = ddlActivity.Items.FindByValue(drv["ACTIVITY"].ToString().Trim());
                if (item != null)
                {
                    ddlActivity.ClearSelection();
                    item.Selected = true;
                }
            }

            if (ddlFOC != null && drv["FOC_TYPE"] != DBNull.Value)
            {
                var item = ddlFOC.Items.FindByValue(drv["FOC_TYPE"].ToString().Trim());
                if (item != null)
                {
                    ddlFOC.ClearSelection();
                    item.Selected = true;
                }
            }

            bool isNewRow = drv["IS_NEW_ROW"] != DBNull.Value && drv["IS_NEW_ROW"].ToString() == "1";
            bool isExtraRow = drv.DataView.Table.Columns.Contains("IS_EXTRA_ROW") && drv["IS_EXTRA_ROW"] != DBNull.Value && drv["IS_EXTRA_ROW"].ToString() == "1";
            string day = drv["DAY"] != DBNull.Value ? drv["DAY"].ToString() : "";

            if (isNewRow)
            {
                if (btnAddRow != null) btnAddRow.Visible = false;
                if (btnDeleteRow != null) btnDeleteRow.Visible = true;
                e.Row.BackColor = System.Drawing.Color.FromArgb(204, 229, 255);
            }
            else
            {
                if (btnAddRow != null) btnAddRow.Visible = true;
                if (btnDeleteRow != null) btnDeleteRow.Visible = false;

                if (day == "Saturday" || day == "Sunday")
                {
                    if (chkWeekendWorking != null)
                    {
                        chkWeekendWorking.Visible = true;
                        if (drv["WEEKEND_WORKING"] != DBNull.Value)
                            chkWeekendWorking.Checked = drv["WEEKEND_WORKING"].ToString() == "1";

                        chkWeekendWorking.Attributes.Add("onclick", "ToggleWeekendRow(this);");
                    }
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 204, 204);
                }
                else
                {
                    if (chkWeekendWorking != null) chkWeekendWorking.Visible = false;
                }
            }

            if (isExtraRow)
            {
                if (btnUseRow != null) btnUseRow.Visible = true;
                e.Row.BackColor = System.Drawing.Color.LightYellow;

                if (btnUseRow != null && drv["IS_USED"] != DBNull.Value)
                {
                    btnUseRow.Text = drv["IS_USED"].ToString() == "1" ? "Undo" : "Use";
                }
            }
            else
            {
                if (btnUseRow != null) btnUseRow.Visible = false;
            }

            if (drv.DataView.Table.Columns.Contains("IS_ENABLED") && drv["IS_ENABLED"] != DBNull.Value)
            {
                bool enabled = drv["IS_ENABLED"].ToString() != "0";

                if (day == "Saturday" || day == "Sunday")
                {
                    if (drv["WEEKEND_WORKING"] == DBNull.Value || drv["WEEKEND_WORKING"].ToString() == "0")
                    {
                        enabled = false;
                    }
                }

                if (ddlActivity != null) ddlActivity.Enabled = enabled;
                if (ddlFOC != null) ddlFOC.Enabled = enabled;
                if (txtStart != null) txtStart.Enabled = enabled;
                if (txtFinish != null) txtFinish.Enabled = enabled;
                if (txtBreak != null) txtBreak.Enabled = enabled;
                if (txtRemarksGrid != null) txtRemarksGrid.Enabled = enabled;
            }
        }
    }

    private void SaveGridData()
    {
        DataTable dt = ViewState["GridData"] as DataTable;

        if (dt == null) return;

        for (int i = 0; i < gvOTReport.Rows.Count; i++)
        {
            GridViewRow row = gvOTReport.Rows[i];

            if (i >= dt.Rows.Count) break;

            DataRow dr = dt.Rows[i];
            DropDownList ddlActivity = (DropDownList)row.FindControl("ddlActivity");
            if (ddlActivity != null)
            {
                dr["ACTIVITY"] = Request.Form[ddlActivity.UniqueID] ?? ddlActivity.SelectedValue;
            }

            TextBox txtStartTime = (TextBox)row.FindControl("txtStartTime");
            if (txtStartTime != null)
            {
                string start = Request.Form[txtStartTime.UniqueID] ?? txtStartTime.Text;
                dr["START_TIME"] = start.Trim();
            }

            TextBox txtFinishTime = (TextBox)row.FindControl("txtFinishTime");
            if (txtFinishTime != null)
            {
                string finish = Request.Form[txtFinishTime.UniqueID] ?? txtFinishTime.Text;
                dr["FINISH_TIME"] = finish.Trim();
            }

            TextBox txtBreakTime = (TextBox)row.FindControl("txtBreakTime");
            if (txtBreakTime != null)
            {
                string brk = Request.Form[txtBreakTime.UniqueID] ?? txtBreakTime.Text;
                dr["BREAK_TIME"] = string.IsNullOrEmpty(brk) ? "0" : brk.Trim();
            }

            TextBox txtActiveTime = (TextBox)row.FindControl("txtActiveTime");
            if (txtActiveTime != null)
            {
                string active = Request.Form[txtActiveTime.UniqueID] ?? txtActiveTime.Text;
                dr["ACTIVE_TIME"] = string.IsNullOrEmpty(active) ? "0" : active.Trim();
            }

            TextBox txtRemarksGrid = (TextBox)row.FindControl("txtRemarksGrid");
            if (txtRemarksGrid != null)
            {
                string rem = Request.Form[txtRemarksGrid.UniqueID] ?? txtRemarksGrid.Text;
                dr["REMARKS"] = rem.Trim();
            }

            DropDownList ddlFOC = (DropDownList)row.FindControl("ddlFOC");
            if (ddlFOC != null)
            {
                dr["FOC_TYPE"] = Request.Form[ddlFOC.UniqueID] ?? ddlFOC.SelectedValue;
            }

            CheckBox chkWeekend = (CheckBox)row.FindControl("chkWeekendWorking");
            if (chkWeekend != null)
            {
                if (dr["IS_NEW_ROW"].ToString() != "1")
                {
                    dr["WEEKEND_WORKING"] = chkWeekend.Checked ? "1" : "0";
                }
            }
        }

        dt.AcceptChanges();
        ViewState["GridData"] = dt;
    }

    private bool ValidateHeader()
    {
        if (string.IsNullOrEmpty(ddlTSNumber.SelectedValue.Trim())) { ExceptionMessage("Please select Tour Sanction Number."); return false; }
        if (string.IsNullOrEmpty(txtStartDate.Text.Trim())) { ExceptionMessage("Tour Start Date is required."); return false; }
        if (string.IsNullOrEmpty(txtCustName.Text.Trim())) { ExceptionMessage("Customer Name is required."); return false; }
        if (string.IsNullOrEmpty(txtEndUserSite.Text.Trim())) { ExceptionMessage("End User & Site is required."); return false; }
        if (string.IsNullOrEmpty(txtEmployeeName.Text.Trim())) { ExceptionMessage("Coperion Employee Name is required."); return false; }
        return true;
    }

    private bool ValidateTimesheet()
    {
        bool atleastOneRow = false;

        foreach (GridViewRow row in gvOTReport.Rows)
        {
            DropDownList ddlActivity = (DropDownList)row.FindControl("ddlActivity");
            TextBox txtStart = (TextBox)row.FindControl("txtStartTime");
            TextBox txtFinish = (TextBox)row.FindControl("txtFinishTime");
            DropDownList ddlFOC = (DropDownList)row.FindControl("ddlFOC");

            string act = Request.Form[ddlActivity.UniqueID] ?? ddlActivity.SelectedValue;
            string start = Request.Form[txtStart.UniqueID] ?? txtStart.Text;
            string finish = Request.Form[txtFinish.UniqueID] ?? txtFinish.Text;
            string foc = Request.Form[ddlFOC.UniqueID] ?? ddlFOC.SelectedValue;

            bool rowStarted = !string.IsNullOrEmpty(act) || !string.IsNullOrEmpty(start.Trim()) || !string.IsNullOrEmpty(finish.Trim()) || !string.IsNullOrEmpty(foc);

            if (rowStarted)
            {
                atleastOneRow = true;

                if (string.IsNullOrEmpty(act)) { ExceptionMessage("Please select Activity in timesheet."); return false; }
                if (string.IsNullOrEmpty(start.Trim())) { ExceptionMessage("Please enter Start Time."); return false; }
                if (string.IsNullOrEmpty(finish.Trim())) { ExceptionMessage("Please enter Finish Time."); return false; }
                if (string.IsNullOrEmpty(foc)) { ExceptionMessage("Please select FOC Type."); return false; }
            }
        }

        if (!atleastOneRow)
        {
            ExceptionMessage("Please fill at least one timesheet row.");
            return false;
        }

        return true;
    }

    private bool ValidateApproval()
    {
        bool atleastOneApproval = false;

        foreach (GridViewRow row in GridView1.Rows)
        {
            TextBox txtDate = (TextBox)row.FindControl("txtDate");
            TextBox txtPlace = (TextBox)row.FindControl("txtPlace");
            TextBox txtSite = (TextBox)row.FindControl("txtSiteIncharge");
            TextBox txtDesignation = (TextBox)row.FindControl("txtDesignation");

            bool rowStarted = !string.IsNullOrEmpty(txtDate.Text.Trim()) || !string.IsNullOrEmpty(txtPlace.Text.Trim()) || !string.IsNullOrEmpty(txtSite.Text.Trim()) || !string.IsNullOrEmpty(txtDesignation.Text.Trim());

            if (rowStarted)
            {
                atleastOneApproval = true;
                if (string.IsNullOrEmpty(txtDate.Text.Trim())) { ExceptionMessage("Approval Date is required."); return false; }
                if (string.IsNullOrEmpty(txtPlace.Text.Trim())) { ExceptionMessage("Approval Place is required."); return false; }
                if (string.IsNullOrEmpty(txtSite.Text.Trim())) { ExceptionMessage("Site Incharge Name is required."); return false; }
                if (string.IsNullOrEmpty(txtDesignation.Text.Trim())) { ExceptionMessage("Designation is required."); return false; }
            }
        }

        if (!atleastOneApproval)
        {
            ExceptionMessage("Please fill at least one approval row.");
            return false;
        }
        return true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveGridData();

        if (!ValidateHeader()) return;
        if (!ValidateTimesheet()) return;
        if (!ValidateApproval()) return;

        InsertWeeklySiteReport();
        GeneratePDF();
    }

    private void InsertWeeklySiteReport()
    {
        try
        {
            DataTable dt = ViewState["GridData"] as DataTable;
            if (dt == null || gvOTReport.Rows.Count == 0)
            {
                ExceptionMessage("Grid data or ViewState is missing.");
                return;
            }

            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            string masterFocType = ddlFOCType.SelectedValue;

            StringBuilder detailsXml = new StringBuilder();
            detailsXml.Append("<Rows>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string activity = dr["ACTIVITY"].ToString();
                string start = dr["START_TIME"].ToString();
                string finish = dr["FINISH_TIME"].ToString();
                string breakTime = dr["BREAK_TIME"].ToString();
                string activeTime = dr["ACTIVE_TIME"].ToString();
                string remarksGrid = dr["REMARKS"].ToString();
                string focType = dr["FOC_TYPE"].ToString();
                bool isWeekendChecked = dr["WEEKEND_WORKING"].ToString() == "1";

                if (string.IsNullOrEmpty(activity) && string.IsNullOrEmpty(start) && string.IsNullOrEmpty(finish))
                    continue;

                string formattedWorkDate = Convert.ToDateTime(dr["DATE"]).ToString("yyyy-MM-dd");
                DateTime pStart, pFinish;
                string cleanStart = DateTime.TryParse(start, out pStart) ? pStart.ToString("HH:mm:ss") : start;
                string cleanFinish = DateTime.TryParse(finish, out pFinish) ? pFinish.ToString("HH:mm:ss") : finish;
                string cleanBreak = breakTime.Replace(":", ".");
                string cleanActive = activeTime.Replace(":", ".");

                detailsXml.Append("<Row>");
                detailsXml.Append("<WORK_DATE>" + SecurityElement.Escape(formattedWorkDate) + "</WORK_DATE>");
                detailsXml.Append("<WEEKEND_WORKING>" + (isWeekendChecked ? "1" : "0") + "</WEEKEND_WORKING>");
                detailsXml.Append("<ACTIVITY>" + SecurityElement.Escape(activity) + "</ACTIVITY>");
                detailsXml.Append("<START_TIME>" + SecurityElement.Escape(cleanStart) + "</START_TIME>");
                detailsXml.Append("<FINISH_TIME>" + SecurityElement.Escape(cleanFinish) + "</FINISH_TIME>");
                detailsXml.Append("<BREAK_TIME>" + SecurityElement.Escape(cleanBreak) + "</BREAK_TIME>");
                detailsXml.Append("<ACTIVE_TIME>" + SecurityElement.Escape(cleanActive) + "</ACTIVE_TIME>");
                detailsXml.Append("<REMARKS>" + SecurityElement.Escape(remarksGrid) + "</REMARKS>");
                detailsXml.Append("<FOC_TYPE>" + SecurityElement.Escape(focType) + "</FOC_TYPE>");
                detailsXml.Append("<DISPLAY_ORDER>" + (dr["IS_NEW_ROW"].ToString() == "1" ? "1" : "0") + "</DISPLAY_ORDER>");
                detailsXml.Append("</Row>");
            }
            detailsXml.Append("</Rows>");

            StringBuilder approvalXml = new StringBuilder();
            approvalXml.Append("<Approvals>");

            foreach (GridViewRow row in GridView1.Rows)
            {
                Label lblSrNo = (Label)row.FindControl("lblSrNo");
                TextBox txtDateApproval = (TextBox)row.FindControl("txtDate");
                TextBox txtPlace = (TextBox)row.FindControl("txtPlace");
                TextBox txtSiteIncharge = (TextBox)row.FindControl("txtSiteIncharge");
                TextBox txtDesignation = (TextBox)row.FindControl("txtDesignation");
                TextBox txtSignature = (TextBox)row.FindControl("txtSignature");

                string appDate = txtDateApproval != null ? txtDateApproval.Text.Trim() : "";
                if (string.IsNullOrEmpty(appDate)) continue;

                string labelText = lblSrNo != null ? lblSrNo.Text.Trim() : "";
                string placeText = txtPlace != null ? txtPlace.Text.Trim() : "";
                string siteInchargeText = txtSiteIncharge != null ? txtSiteIncharge.Text.Trim() : "";
                string designationText = txtDesignation != null ? txtDesignation.Text.Trim() : "";
                string signatureText = txtSignature != null ? txtSignature.Text.Trim() : "";
                string formattedAppDate = Convert.ToDateTime(appDate).ToString("yyyy-MM-dd");

                approvalXml.Append("<Approval>");
                approvalXml.Append("<APPROVAL_LABEL>" + SecurityElement.Escape(labelText) + "</APPROVAL_LABEL>");
                approvalXml.Append("<APPROVAL_DATE>" + SecurityElement.Escape(formattedAppDate) + "</APPROVAL_DATE>");
                approvalXml.Append("<PLACE>" + SecurityElement.Escape(placeText) + "</PLACE>");
                approvalXml.Append("<SITE_INCHARGE_NAME>" + SecurityElement.Escape(siteInchargeText) + "</SITE_INCHARGE_NAME>");
                approvalXml.Append("<DESIGNATION>" + SecurityElement.Escape(designationText) + "</DESIGNATION>");
                approvalXml.Append("<SIGNATURE>" + SecurityElement.Escape(signatureText) + "</SIGNATURE>");
                approvalXml.Append("</Approval>");
            }
            approvalXml.Append("</Approvals>");

            int resultReportID = objTourAndTravels.InsertCompleteWeeklySiteReportMaster(
                ddlTSNumber.SelectedValue,
                txtStartDate.Text,
                txtEndDate.Text,
                txtCustName.Text.Trim(),
                txtEndUserSite.Text.Trim(),
                txtEmployeeName.Text.Trim(),
                txtPoNo.Text.Trim(),
                txtJobNo.Text.Trim(),
                txtRemarks.Text.Trim(),
                masterFocType,
                createdBy,
                detailsXml.ToString(),
                approvalXml.ToString()
            );

            if (resultReportID > 0)
            {
                pnlMsg.Visible = true;
                lblMsg.Text = "Weekly Site Report and Approvals Saved Successfully!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ExceptionMessage("Database transaction error occurred.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.Message);
        }
    }

    private void GeneratePDF()
    {
        SaveGridData();
        DataTable dt = ViewState["GridData"] as DataTable;

        using (MemoryStream ms = new MemoryStream())
        {
            Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 30, 30);
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font headFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font normalFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL);

            PdfPTable headerTable = new PdfPTable(4);
            headerTable.WidthPercentage = 100;
            headerTable.SetWidths(new float[] { 2f, 3f, 2f, 3f });
            headerTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            headerTable.DefaultCell.Padding = 5;

            PdfPCell titleCell = new PdfPCell(new Phrase("Weekly Site Report", titleFont));
            titleCell.Colspan = 4;
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            titleCell.PaddingBottom = 15f;
            headerTable.AddCell(titleCell);

            headerTable.AddCell(new Phrase("Tour Sanction No:", headFont));
            headerTable.AddCell(new Phrase(ddlTSNumber.SelectedValue, normalFont));
            headerTable.AddCell(new Phrase("Tour Start Date:", headFont));
            headerTable.AddCell(new Phrase(txtStartDate.Text, normalFont));
            headerTable.AddCell(new Phrase("Tour End Date:", headFont));
            headerTable.AddCell(new Phrase(txtEndDate.Text, normalFont));
            headerTable.AddCell(new Phrase("Customer Name:", headFont));
            headerTable.AddCell(new Phrase(txtCustName.Text, normalFont));
            headerTable.AddCell(new Phrase("End User & Site:", headFont));
            headerTable.AddCell(new Phrase(txtEndUserSite.Text, normalFont));
            headerTable.AddCell(new Phrase("Employee Name:", headFont));
            headerTable.AddCell(new Phrase(txtEmployeeName.Text, normalFont));
            headerTable.AddCell(new Phrase("Coperion PO No:", headFont));
            headerTable.AddCell(new Phrase(txtPoNo.Text, normalFont));
            headerTable.AddCell(new Phrase("Coperion Job No:", headFont));
            headerTable.AddCell(new Phrase(txtJobNo.Text, normalFont));
            headerTable.AddCell(new Phrase("Remarks:", headFont));
            headerTable.AddCell(new Phrase(txtRemarks.Text, normalFont));
            headerTable.AddCell(new Phrase("FOC Type:", headFont));
            headerTable.AddCell(new Phrase(ddlFOCType.SelectedValue, normalFont));

            doc.Add(headerTable);
            doc.Add(new Paragraph("\n"));

            if (dt != null && dt.Rows.Count > 0)
            {
                PdfPTable mainTable = new PdfPTable(9);
                mainTable.WidthPercentage = 100;
                mainTable.SetWidths(new float[] { 2.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 3f, 2f });

                string[] headers = { "DATE", "DAY", "ACTIVITY", "START", "FINISH", "BREAK", "ACTIVE", "REMARKS", "FOC TYPE" };
                foreach (string h in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(h, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD)));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Padding = 5;
                    mainTable.AddCell(cell);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["DATE"].ToString(), normalFont)) { Padding = 4 });
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["DAY"].ToString(), normalFont)) { Padding = 4 });
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["ACTIVITY"].ToString(), normalFont)) { Padding = 4 });
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["START_TIME"].ToString(), normalFont)) { Padding = 4 });
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["FINISH_TIME"].ToString(), normalFont)) { Padding = 4 });
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["BREAK_TIME"].ToString(), normalFont)) { Padding = 4 });
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["ACTIVE_TIME"].ToString(), normalFont)) { Padding = 4 });
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["REMARKS"].ToString(), normalFont)) { Padding = 4 });
                    mainTable.AddCell(new PdfPCell(new Phrase(dr["FOC_TYPE"].ToString(), normalFont)) { Padding = 4 });
                }
                doc.Add(mainTable);
            }

            doc.Add(new Paragraph("\nSite Approval\n\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)));
            PdfPTable appTable = new PdfPTable(5);
            appTable.WidthPercentage = 100;
            appTable.SetWidths(new float[] { 2f, 2.5f, 2.5f, 2.5f, 2.5f });

            string[] appHeaders = { "DATE", "PLACE", "INCHARGE", "DESIGNATION", "SIGNATURE" };
            foreach (string h in appHeaders)
            {
                PdfPCell cell = new PdfPCell(new Phrase(h, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD)));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Padding = 5;
                appTable.AddCell(cell);
            }

            foreach (GridViewRow row in GridView1.Rows)
            {
                appTable.AddCell(new PdfPCell(new Phrase(((TextBox)row.FindControl("txtDate")).Text, normalFont)) { Padding = 4 });
                appTable.AddCell(new PdfPCell(new Phrase(((TextBox)row.FindControl("txtPlace")).Text, normalFont)) { Padding = 4 });
                appTable.AddCell(new PdfPCell(new Phrase(((TextBox)row.FindControl("txtSiteIncharge")).Text, normalFont)) { Padding = 4 });
                appTable.AddCell(new PdfPCell(new Phrase(((TextBox)row.FindControl("txtDesignation")).Text, normalFont)) { Padding = 4 });
                PdfPCell sigCell = new PdfPCell(new Phrase("\n\n__________________")) { FixedHeight = 40f, HorizontalAlignment = Element.ALIGN_CENTER };
                appTable.AddCell(sigCell);
            }
            doc.Add(appTable);

            doc.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=WeeklySiteReport_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    private void ClearAllFields()
    {
        ddlTSNumber.SelectedIndex = 0;
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtCustName.Text = "";
        txtEndUserSite.Text = "";
        txtEmployeeName.Text = "";
        txtPoNo.Text = "";
        txtJobNo.Text = "";
        txtRemarks.Text = "";

        if (ViewState["GridData"] != null)
        {
            ViewState["GridData"] = null;
        }
        gvOTReport.DataSource = null;
        gvOTReport.DataBind();
        lblRecords.Text = "Records[0]";

        BindSecondGrid();
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }
}