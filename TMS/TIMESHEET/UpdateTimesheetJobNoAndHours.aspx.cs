using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;



public partial class TIMESHEET_UpdateTimesheetJobNoAndHours : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Timesheet objTimesheet = new BAL.Timesheet();



    DataSet dsEmployee = new DataSet();
    DataSet dsTimesheetList = new DataSet();
    DataSet dsDepartment = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string jobNo = string.Empty;
    string employeeName = string.Empty;
    string employeeRecordIDs = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {


                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                Session["DT_TIMESHEET_LIST_TEMP"] = null;
                BindEmployee();
                GetDepartments();
                GetTimesheetList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetTimesheetList();
    }


    protected void txtEmployeeName_TextChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }


   

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTimesheetList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["DT_TIMESHEET_LIST_TEMP"];
            ToCSVNew01(dt);
        }
    }

    protected void gvTimesheetList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvTimesheetList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "UPDATE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = gvTimesheetList.Rows[rowindex].FindControl("lblRecordID") as Label;
                TextBox txtJobNo = gvTimesheetList.Rows[rowindex].FindControl("txtJobNo") as TextBox;


                ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);
                ViewState["JOB_NO"] = Convert.ToString(txtJobNo.Text);

                if (Convert.ToString(e.CommandArgument) == "UPDATE")
                {
                    UpdateDetails(0);
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

    protected void btnUpdateAll_Click(object sender, EventArgs e)
    {
        UpdateDetails(1);
    }

    #endregion


    #region METHODS[=======================]

    private void GetDepartments()
    {
        try
        {
            dsDepartment = objCommon.GetDepartment();
            if (dsDepartment.Tables.Count > 0 && dsDepartment.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = dsDepartment.Tables[0];
                ddlDepartment.DataTextField = "DEPARTMENT_NAME";
                ddlDepartment.DataValueField = "DEPARTMENT_ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, "All");
                ddlDepartment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEmployee()
    {
        try
        {
            string employeeName = string.Empty;
            int deptID = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(txtEmployeeName.Text)))
                employeeName = Convert.ToString(txtEmployeeName.Text);

            if (ddlDepartment.SelectedIndex > 0)
                deptID = Convert.ToInt32(ddlDepartment.SelectedValue);

            dsEmployee = objCommon.GetEmployeeByNameAndDepartmentID(employeeName, deptID);

            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                chklbEmployee.DataSource = dsEmployee.Tables[0];
                chklbEmployee.DataTextField = "EMPLOYEE_NAME";
                chklbEmployee.DataValueField = "EMP_RECORD_ID";
                chklbEmployee.DataBind();
            }
            else
            {
                chklbEmployee.Items.Clear();
                chklbEmployee.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTimesheetList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;


            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text.Trim();
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
                employeeName = Convert.ToString(txtEmployeeName.Text);
            else
                employeeName = string.Empty;


            for (int i = 0; i < chklbEmployee.Items.Count; i++)
            {
                if (chklbEmployee.Items[i].Selected)
                    employeeRecordIDs += chklbEmployee.Items[i].Value + ",";
            }

            employeeRecordIDs = employeeRecordIDs.TrimEnd(',');


            dsTimesheetList = objTimesheet.GetTimesheetListToUpdateDetails(startDate, endDate, jobNo, employeeName, employeeRecordIDs);
            if (dsTimesheetList.Tables.Count > 0 && dsTimesheetList.Tables[0].Rows.Count > 0)
            {
                Session["DT_TIMESHEET_LIST_TEMP"] = dsTimesheetList.Tables[0];
                gvTimesheetList.DataSource = dsTimesheetList.Tables[0];
                gvTimesheetList.DataBind();
            }
            else
            {
                Session["DT_TIMESHEET_LIST_TEMP"] = null;
                gvTimesheetList.DataSource = null;
                gvTimesheetList.DataBind();
            }




            lblRecords.Text = "Records[" + dsTimesheetList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateDetails(int typeID)
    {
        try
        {
            int recordID = 0;
            string jobNo = string.Empty;

            if (typeID == 0)  //0 for single updation, 1 for bulk updation
            {
                recordID = Convert.ToInt32(ViewState["RECORD_ID"]);
                jobNo = Convert.ToString(ViewState["JOB_NO"]);

                if (!string.IsNullOrEmpty(jobNo))
                {
                    int value = objTimesheet.UpdateTimesheetDetails(recordID, jobNo, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    if (value > 0)
                    {
                        SuccessMessage("Updated successfully...!!!");
                        GetTimesheetList();
                        return;
                    }
                    else
                    {
                        ExceptionMessage("Please try again...!!!");
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("Please enter job number...!!!");
                    return;
                }
            }
            else
            {
                if (gvTimesheetList.Rows.Count > 0)
                {
                    int checkedCount = 0;
                    int value = 0;
                    foreach (GridViewRow gr in gvTimesheetList.Rows)
                    {
                        CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                        if (chkSelect.Checked)
                        {
                            checkedCount++;

                            Label lblRecordID = gr.FindControl("lblRecordID") as Label;
                            TextBox txtJobNo = gr.FindControl("txtJobNo") as TextBox;

                            recordID = Convert.ToInt32(lblRecordID.Text);
                            if (!string.IsNullOrEmpty(txtJobNo.Text))
                            {
                                jobNo = Convert.ToString(txtJobNo.Text);
                                value = objTimesheet.UpdateTimesheetDetails(recordID, jobNo, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                value++;
                            }
                            else
                            {
                                txtJobNo.BackColor = System.Drawing.Color.LightPink;
                            }
                        }
                    }

                    if (checkedCount == 0)
                    {
                        ExceptionMessage("Please select atleast 1 record...!!!");
                        return;
                    }

                    if (value > 0)
                    {
                        SuccessMessage("Updated successfully...!!!");
                        GetTimesheetList();
                        return;
                    }
                    else
                    {
                        ExceptionMessage("Please try again...!!!");
                        return;
                    }
                }
            }
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
            if (dt.Rows.Count > 0)
            {


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

                string fileName = "Timesheet_List_" + DateTime.Now.ToString("dd_MMM_yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
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
