using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WORKER_AddUpdateWorkerTimesheet : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Timesheet objTimesheet = new BAL.Timesheet();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsJOBNo = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsEmployeeList = new DataSet();
    DataTable dt = new DataTable();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HideMessagePanel();
                Session["dsEmployeeList"] = null;
                Session["dtTemp"] = null;
                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                hdDate.Value = txtDate.Text;
                BindJOBNo();
                BindUnit();

                dsEmployeeList = objCommon.GetEmployeeByEmpRecordID(0);
                Session["dsEmployeeList"] = dsEmployeeList;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        BindNewRow();
    }

    protected void gvEmpJobList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HideMessagePanel();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlEmployeeName = (e.Row.FindControl("ddlEmployeeName") as DropDownList);

                DataSet dsEmployeeListNew = (DataSet)Session["dsEmployeeList"];
                if (dsEmployeeListNew.Tables.Count > 0 && dsEmployeeListNew.Tables[0].Rows.Count > 0)
                {
                    ddlEmployeeName.DataSource = dsEmployeeListNew.Tables[0];
                    ddlEmployeeName.DataTextField = "EMPLOYEE_NAME";
                    ddlEmployeeName.DataValueField = "EMP_RECORD_ID";
                    ddlEmployeeName.DataBind();
                    ddlEmployeeName.Items.Insert(0, "Select");
                    ddlEmployeeName.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideMessagePanel();
        DataTable dt = (DataTable)Session["dtTemp"];
        DropDownList ddlEmployeeName = (DropDownList)sender;
        GridViewRow gr = (GridViewRow)ddlEmployeeName.Parent.Parent;
        int indx = gr.RowIndex;

        TextBox txtEmployeeCode = gvEmpJobList.Rows[indx].FindControl("txtEmployeeCode") as TextBox;
        string empRecoedID = Convert.ToString(ddlEmployeeName.SelectedValue);

        DataSet dsEmplist = (DataSet)Session["dsEmployeeList"];
        if (dsEmplist.Tables.Count > 0 && dsEmplist.Tables[0].Rows.Count > 0)
        {
            if (ddlEmployeeName.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(empRecoedID))
                {
                    foreach (DataRow dr in dsEmplist.Tables[0].Select("EMP_RECORD_ID='" + empRecoedID + "'"))
                    {
                        txtEmployeeCode.Text = Convert.ToString(dr["EMPLOYEE_ID"]);
                        dt.Rows[indx]["EMP_RECORD_ID"] = empRecoedID;
                        dt.Rows[indx]["EMPLOYEE_CODE"] = Convert.ToString(dr["EMPLOYEE_ID"]);
                    }
                }
                else
                {
                    txtEmployeeCode.Text = string.Empty;
                    dt.Rows[indx]["EMP_RECORD_ID"] = string.Empty;
                    dt.Rows[indx]["EMPLOYEE_CODE"] = string.Empty;
                }
            }
            else
            {
                txtEmployeeCode.Text = string.Empty;
            }
        }
    }

    protected void btnGetTimesheetFile_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        GetTimesheet();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        AddWorkerTimesheet();
    }


    #endregion


    #region METHODS[=======================]

    private void BindJOBNo()
    {
        try
        {
            dsJOBNo = objTimesheet.GetDetailsBySP("sp_get_distinct_job_no");
            if (dsJOBNo.Tables.Count > 0 && dsJOBNo.Tables[0].Rows.Count > 0)
            {
                ddlJOBNo.DataSource = dsJOBNo.Tables[0];
                ddlJOBNo.DataTextField = "JOB_NUMBER";
                ddlJOBNo.DataValueField = "JOB_NUMBER";
                ddlJOBNo.DataBind();
                ddlJOBNo.Items.Insert(0, "Select");
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
                ddlUnit.DataSource = dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "Select");
                ddlUnit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindNewRow()
    {
        try
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("EMP_RECORD_ID", typeof(string));
            dtTemp.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dtTemp.Columns.Add("JOB_NO", typeof(string));
            dtTemp.Columns.Add("IN_TIME", typeof(string));
            dtTemp.Columns.Add("HOURS", typeof(string));
            dtTemp.Columns.Add("STATUS", typeof(string));

            DataRow dr = dtTemp.NewRow();
            dr["EMP_RECORD_ID"] = string.Empty;
            dr["EMPLOYEE_CODE"] = string.Empty;
            dr["JOB_NO"] = Convert.ToString(ddlJOBNo.SelectedValue);
            dr["IN_TIME"] = string.Empty;
            dr["HOURS"] = string.Empty;
            dr["STATUS"] = "0";

            dtTemp.Rows.Add(dr);

            if (Session["dtTemp"] != null)
            {
                dt = (DataTable)Session["dtTemp"];
                dt.ImportRow(dtTemp.Rows[0]);
                Session["dtTemp"] = dt;
                gvEmpJobList.DataSource = dt;
            }
            else
            {
                Session["dtTemp"] = dtTemp;
                gvEmpJobList.DataSource = dtTemp;
            }



            gvEmpJobList.DataBind();
            foreach (GridViewRow gr in gvEmpJobList.Rows)
            {
                TextBox txtEmployeeCode = (gr.FindControl("txtEmployeeCode") as TextBox);
                string grEmpCode = txtEmployeeCode.Text;
                DropDownList ddlEmployeeName = (gr.FindControl("ddlEmployeeName") as DropDownList);

                if (!string.IsNullOrEmpty(grEmpCode))
                {
                    foreach (DataRow rowempid in dt.Select("EMPLOYEE_CODE='" + grEmpCode + "'"))
                    {
                        ddlEmployeeName.SelectedValue = Convert.ToString(rowempid["EMP_RECORD_ID"]);
                    }
                }
                else
                {
                    ddlEmployeeName.SelectedIndex = 0;
                }

            }
            lblRecords.Text = "Records[" + gvEmpJobList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTimesheet()
    {
        try
        {
            #region CREATE_TABLE

            DataTable dt = new DataTable();
            dt.Columns.Add("JOB_NO", typeof(string));
            dt.Columns.Add("IN_TIME", typeof(string));
            dt.Columns.Add("HOURS", typeof(string));

            #endregion

            StreamReader myReader = new StreamReader(fileUploadTimesheet.PostedFile.InputStream);
            string csvData = myReader.ReadToEnd();
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    string[] strRow = row.Split(',');
                    int length = Convert.ToInt32(strRow.Length);
                    foreach (string cell in row.Split(','))
                    {
                        if (i <= (dt.Columns.Count) && i < (strRow.Length - 2))
                        {
                            if (!string.IsNullOrEmpty(cell))
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                            else
                                dt.Rows[dt.Rows.Count - 1][i] = string.Empty;

                            i++;
                        }
                    }
                }
            }

            dt.Rows.RemoveAt(0);
            dt.Columns.Add("EMP_RECORD_ID", typeof(string));
            dt.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dt.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dt.Columns.Add("STATUS", typeof(string));

            foreach (DataRow drstatus in dt.Rows)
            {
                drstatus["STATUS"] = "0";
            }

            Session["dtTemp"] = dt;
            gvEmpJobList.DataSource = dt;
            gvEmpJobList.DataBind();

            lblRecords.Text = "Records[" + dt.Rows.Count + "]";

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddWorkerTimesheet()
    {
        try
        {
            DataTable dtNew = new DataTable();

            dtNew.Columns.Add("JOB_NO", typeof(string));
            dtNew.Columns.Add("IN_TIME", typeof(string));
            dtNew.Columns.Add("HOURS", typeof(string));
            dtNew.Columns.Add("EMP_RECORD_ID", typeof(string));
            dtNew.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dtNew.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dtNew.Columns.Add("STATUS", typeof(string));

            string entryDate = string.Empty;
            int empRecordID = 0;
            string jobNo = string.Empty;
            string inTime = string.Empty;
            string hours = string.Empty;
            int count = 0;
            bool inTimeCheck = false;
            bool hoursCheck = false;

            entryDate = Convert.ToDateTime(hdDate.Value).ToString("yyyy-MM-dd");
            DataTable dt = (DataTable)Session["dtTemp"];
            if (gvEmpJobList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvEmpJobList.Rows)
                {
                    DropDownList ddlEmployeeName = gr.FindControl("ddlEmployeeName") as DropDownList;
                    TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                    TextBox txtInTime = gr.FindControl("txtInTime") as TextBox;
                    TextBox txtHours = gr.FindControl("txtHours") as TextBox;

                    if (ddlEmployeeName.SelectedIndex > 0)
                        empRecordID = Convert.ToInt32(ddlEmployeeName.SelectedValue);
                    else
                        empRecordID = 0;

                    if (!string.IsNullOrEmpty(txtJOBNo.Text))
                        jobNo = Convert.ToString(txtJOBNo.Text);
                    else
                        jobNo = string.Empty;

                    if (!string.IsNullOrEmpty(txtInTime.Text))
                    {
                        inTime = Convert.ToString(txtInTime.Text);
                        string[] strtext = inTime.Split(':');
                        int inTimeHours = Convert.ToInt32(strtext[0]);
                        int inTimeMins = Convert.ToInt32(strtext[1]);

                        if (inTimeHours <= 23 && inTimeMins <= 59)
                            inTimeCheck = true;
                        else
                            inTimeCheck = false;
                    }

                    else
                        inTime = string.Empty;

                    if (!string.IsNullOrEmpty(txtHours.Text))
                    {
                        hours = Convert.ToString(txtHours.Text);
                        string[] strtext = hours.Split(':');
                        int hoursMins = Convert.ToInt32(strtext[1]);

                        if (hoursMins <= 59)
                            hoursCheck = true;
                        else
                            hoursCheck = false;
                    }

                    else
                        hours = string.Empty;


                    if (empRecordID != 0 && !string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(hours) && inTimeCheck == true && hoursCheck == true)
                    {
                        int value = objTimesheet.AddUpdateWorkerTimesheet(0, entryDate, empRecordID, jobNo, inTime, hours, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        if (value > 0)
                        {
                            count++;
                        }
                        foreach (DataRow drstatus in dt.Select("EMP_RECORD_ID='" + empRecordID + "'"))
                        {
                            drstatus["STATUS"] = "1";
                        }
                    }
                }


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drnew in dt.Select("STATUS='0'"))
                    {
                        dtNew.ImportRow(drnew);
                    }
                }

                BindNotAddedList(dtNew);

                if (count > 0)
                {
                    SuccessMessage(count + " Records added successfully");
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again!");
                    return;
                }
            }
            else
            {
                ExceptionMessage("No data found!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindNotAddedList(DataTable dtNew)
    {
        try
        {
            if (dtNew.Rows.Count > 0)
            {
                Session["dtTemp"] = dtNew;
                gvEmpJobList.DataSource = dtNew;
                gvEmpJobList.DataBind();
                lblRecords.Text = "Records[" + gvEmpJobList.Rows.Count + "]";
                foreach (GridViewRow gr in gvEmpJobList.Rows)
                {
                    TextBox txtEmployeeCode = (gr.FindControl("txtEmployeeCode") as TextBox);
                    string grEmpCode = txtEmployeeCode.Text;
                    DropDownList ddlEmployeeName = (gr.FindControl("ddlEmployeeName") as DropDownList);

                    if (!string.IsNullOrEmpty(grEmpCode))
                    {
                        foreach (DataRow rowempid in dtNew.Select("EMPLOYEE_CODE='" + grEmpCode + "'"))
                        {
                            ddlEmployeeName.SelectedValue = Convert.ToString(rowempid["EMP_RECORD_ID"]);
                        }
                    }
                    else
                    {
                        ddlEmployeeName.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                gvEmpJobList.DataSource = null;
                gvEmpJobList.DataBind();
                lblRecords.Text = "Records[0]";

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