using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class ADMIN_EMPLOYEE_EmployeeList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    DataSet dsDepartment = new DataSet();
    DataSet dsTimesheetDepartment = new DataSet();
    DataSet dsEmpList = new DataSet();

    DataSet dsEmployee = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsBank = new DataSet();
    string empName = string.Empty;
    string empID = string.Empty;
    int departmentID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dsUnit"] = null;
                Session["dsDepartment"] = null;

                Session["EMPLOYEE_DETAILS"] = null;
                GetUnit();
                GetDepartments();

                BindUnitSearch();
                BindDepartmentToSearch();


                GetTeamleaders();
                GetEmployeeList();
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
        GetEmployeeList();
    }
  
    protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPassportCopyName = (Label)e.Row.FindControl("lblPassportCopyName");
                ImageButton btnPassportCopy = (ImageButton)e.Row.FindControl("btnPassportCopy");

                ImageButton imgIsActive = (ImageButton)e.Row.FindControl("imgIsActive");
                Label lblIsActive = (Label)e.Row.FindControl("lblIsActive");

                string PassportCopyExtn = string.Empty;

                if (!string.IsNullOrEmpty(lblPassportCopyName.Text))
                {
                    PassportCopyExtn = Convert.ToString(lblPassportCopyName.Text).Split('.').Last();
                    if (PassportCopyExtn == "jpg" || PassportCopyExtn == "jepg" || PassportCopyExtn == "bmp" || PassportCopyExtn == "png" || PassportCopyExtn == "gif" || PassportCopyExtn == "JPG" || PassportCopyExtn == "JPEG" || PassportCopyExtn == "BMP" || PassportCopyExtn == "PNG" || PassportCopyExtn == "GIF")
                    {
                        btnPassportCopy.ImageUrl = "~/Images/imgicon1.png";
                        btnPassportCopy.ToolTip = lblPassportCopyName.Text;
                    }
                    else if (PassportCopyExtn == "pdf" || PassportCopyExtn == "PDF")
                    {
                        btnPassportCopy.ImageUrl = "~/Images/pdficon1.png";
                        btnPassportCopy.ToolTip = lblPassportCopyName.Text;
                    }
                }
                else
                {
                    btnPassportCopy.Visible = false;
                }



                if (Convert.ToInt32(lblIsActive.Text) > 0)
                {
                    imgIsActive.ImageUrl = "~/Images/Icons/yes3.png";
                    imgIsActive.ToolTip = "Active Employee. You can deactivate employee from here...!!";
                }
                else
                {
                    imgIsActive.ImageUrl = "~/Images/Icons/no2.png";
                    imgIsActive.ToolTip = "Deactive Employee. You can activate employee from here...!!";
                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void gvEmployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                HidePanels();
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" || Convert.ToString(e.CommandArgument) == "VIEWPASSPORT" || Convert.ToString(e.CommandArgument) == "IS_ACTIVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblEmpRecordID = gvEmployeeList.Rows[rowindex].FindControl("lblEmpRecordID") as Label;
                Label lblTeamLeaderID = gvEmployeeList.Rows[rowindex].FindControl("lblTeamLeaderID") as Label;
                Label lblIsActive = gvEmployeeList.Rows[rowindex].FindControl("lblIsActive") as Label;

                ViewState["empRecordID"] = Convert.ToInt32(lblEmpRecordID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    BindEmployeeDetails(Convert.ToInt32(lblEmpRecordID.Text));
                    this.ModalPopupExtender1.Show();
                }

                if (Convert.ToString(e.CommandArgument) == "VIEWPASSPORT")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblEmpRecordID.Text));
                }

                if (Convert.ToString(e.CommandArgument) == "IS_ACTIVE")
                {
                    if (Convert.ToInt32(lblIsActive.Text) > 0)
                    {
                        BindNewTeamleaderWhenDeactivateUser();
                        ddlNewTeamleader.SelectedValue = Convert.ToString(lblEmpRecordID.Text);
                        lblCurrentTeamLeaderID.Text = Convert.ToString(lblEmpRecordID.Text);

                        DataSet dsTeamMembers = new DataSet();
                        dsTeamMembers = objCommon.GetTeamMembers(Convert.ToInt32(lblEmpRecordID.Text));

                        gvTeamMemberList.DataSource = null;
                        gvTeamMemberList.DataBind();

                        if (dsTeamMembers.Tables.Count > 0 && dsTeamMembers.Tables[0].Rows.Count > 0)
                        {
                            gvTeamMemberList.DataSource = dsTeamMembers.Tables[0];
                            gvTeamMemberList.DataBind();
                            lblTeamMemberListRecords.Text = "Records[" + dsTeamMembers.Tables[0].Rows.Count + "]";
                            this.ModalPopupExtender4.Show();
                        }
                        else
                        {
                            DeactivateEmployee(0, Convert.ToInt32(lblEmpRecordID.Text));
                        }
                    }
                    else
                    {
                        ActivateEmployee(1, Convert.ToInt32(lblEmpRecordID.Text));
                    }
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



    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/EMPLOYEE/AddEmployee.aspx");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateEmployee();
    }

    protected void btnDeactivate_Click(object sender, EventArgs e)
    {
        DeactivateEmployee(0, Convert.ToInt32(lblCurrentTeamLeaderID.Text));
    }


    #endregion


    #region METHODS[=======================]


    private void GetUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                Session["dsUnit"] = dsUnit;
            }
            else
            {
                Session["dsUnit"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUnitSearch()
    {
        try
        {
            if (Session["dsUnit"] != null)
                dsUnit = (DataSet)Session["dsUnit"];
            else
                dsUnit = objCommon.GetUnit();

            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnitSearch.DataSource = dsUnit.Tables[0];
                ddlUnitSearch.DataTextField = "UNIT_NAME";
                ddlUnitSearch.DataValueField = "UNIT_ID";
                ddlUnitSearch.DataBind();
                ddlUnitSearch.Items.Insert(0, "All");
                ddlUnitSearch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUnitToUpdate()
    {
        try
        {
            if (Session["dsUnit"] != null)
                dsUnit = (DataSet)Session["dsUnit"];
            else
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


    private void GetDepartments()
    {
        try
        {
            dsDepartment = objCommon.GetDepartment();
            if (dsDepartment.Tables.Count > 0 && dsDepartment.Tables[0].Rows.Count > 0)
            {
                Session["dsDepartment"] = dsDepartment;
            }
            else
            {
                Session["dsDepartment"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindDepartmentToSearch()
    {
        try
        {
            if (Session["dsDepartment"] != null)
                dsDepartment = (DataSet)Session["dsDepartment"];
            else
                dsDepartment = objCommon.GetDepartment();

            dsDepartment = objCommon.GetDepartment();
            if (dsDepartment.Tables.Count > 0 && dsDepartment.Tables[0].Rows.Count > 0)
            {
                ddlDepartmentSearch.DataSource = dsDepartment.Tables[0];
                ddlDepartmentSearch.DataTextField = "DEPARTMENT_NAME";
                ddlDepartmentSearch.DataValueField = "DEPARTMENT_ID";
                ddlDepartmentSearch.DataBind();
                ddlDepartmentSearch.Items.Insert(0, "Select");
                ddlDepartmentSearch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindDepartmentToUpdate()
    {
        try
        {
            if (Session["dsDepartment"] != null)
                dsDepartment = (DataSet)Session["dsDepartment"];
            else
                dsDepartment = objCommon.GetDepartment();

            if (dsDepartment.Tables.Count > 0 && dsDepartment.Tables[0].Rows.Count > 0)
            {
                ddlDepartmentNew.DataSource = dsDepartment.Tables[0];
                ddlDepartmentNew.DataTextField = "DEPARTMENT_NAME";
                ddlDepartmentNew.DataValueField = "DEPARTMENT_ID";
                ddlDepartmentNew.DataBind();
                ddlDepartmentNew.Items.Insert(0, "Select");
                ddlDepartmentNew.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindTimesheetDepartment()
    {
        try
        {
            dsTimesheetDepartment = objCommon.GetTimesheetDepartment();
            if (dsTimesheetDepartment.Tables.Count > 0 && dsTimesheetDepartment.Tables[0].Rows.Count > 0)
            {
                ddlTimesheetDept.DataSource = dsTimesheetDepartment.Tables[0];
                ddlTimesheetDept.DataTextField = "TIMESHEET_DEPT_CODE";
                ddlTimesheetDept.DataValueField = "TIMESHEET_DEPT_ID";
                ddlTimesheetDept.DataBind();
                ddlTimesheetDept.Items.Insert(0, "Select");
                ddlTimesheetDept.SelectedIndex = 0;
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

    private void BindBankList()
    {
        try
        {
            dsBank = objCommon.GetBankList();
            if (dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                ddlBankName.DataSource = dsBank.Tables[0];
                ddlBankName.DataTextField = "BANK_NAME";
                ddlBankName.DataValueField = "BANK_ID";
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, "Select");
                ddlBankName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetTeamleaders()
    {
        try
        {
            dsEmployee = objCommon.GetEmployeeByEmpRecordID(0);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                Session["dsEmployee"] = dsEmployee;
            }
            else
            {
                Session["dsEmployee"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindExistingTeamleader(int teamLeaderID)
    {
        try
        {
            if (Session["dsEmployee"] != null)
                dsEmployee = (DataSet)Session["dsEmployee"];
            else
                dsEmployee = objCommon.GetEmployeeByEmpRecordID(0);

            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlTeamLeader.DataSource = dsEmployee.Tables[0];
                ddlTeamLeader.DataTextField = "EMPLOYEE_NAME";
                ddlTeamLeader.DataValueField = "EMP_RECORD_ID";
                ddlTeamLeader.DataBind();
                ddlTeamLeader.Items.Insert(0, "Select");
                ddlTeamLeader.SelectedIndex = 0;

                foreach (DataRow dr in dsEmployee.Tables[0].Select("EMP_RECORD_ID='" + teamLeaderID + "'"))
                {
                    ddlTeamLeader.SelectedValue = Convert.ToString(teamLeaderID);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindNewTeamleaderWhenDeactivateUser()
    {
        try
        {
            if (Session["dsEmployee"] != null)
                dsEmployee = (DataSet)Session["dsEmployee"];
            else
                dsEmployee = objCommon.GetEmployeeByEmpRecordID(0);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlNewTeamleader.DataSource = dsEmployee.Tables[0];
                ddlNewTeamleader.DataTextField = "EMPLOYEE_NAME";
                ddlNewTeamleader.DataValueField = "EMP_RECORD_ID";
                ddlNewTeamleader.DataBind();
                ddlNewTeamleader.Items.Insert(0, "Select");
                ddlNewTeamleader.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetEmployeeList()
    {
        try
        {
            int unitID = 0;

            if (ddlUnitSearch.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnitSearch.SelectedValue);
            else unitID = 0;

            if (!string.IsNullOrEmpty(txtEmployeeNameSearch.Text))
                empName = txtEmployeeNameSearch.Text.Trim();
            else
                empName = string.Empty;

            if (!string.IsNullOrEmpty(txtEmployeeIDSearch.Text))
                empID = txtEmployeeIDSearch.Text.Trim();
            else
                empID = string.Empty;

            if (ddlDepartmentSearch.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartmentSearch.SelectedValue);
            else
                departmentID = 0;

            dsEmpList = objCommon.GetEmployeeList(unitID, empName, empID, departmentID);
            if (dsEmpList.Tables.Count > 0 && dsEmpList.Tables[0].Rows.Count > 0)
            {
                Session["EMPLOYEE_DETAILS"] = dsEmpList.Tables[0];
                gvEmployeeList.DataSource = dsEmpList.Tables[0];
                gvEmployeeList.DataBind();
            }
            else
            {
                Session["EMPLOYEE_DETAILS"] = null;
                gvEmployeeList.DataSource = null;
                gvEmployeeList.DataBind();
            }
            lblRecords.Text = "Records[" + dsEmpList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ViewAttachedFilesNew01(int empRecordID)
    {
        try
        {
            byte[] bytes = null;
            DataTable dsPassport = (DataTable)Session["EMPLOYEE_DETAILS"];
            string fileName = string.Empty;
            string extn = string.Empty;
            foreach (DataRow dr in dsPassport.Select("EMP_RECORD_ID='" + empRecordID + "'"))
            {
                fileName = Convert.ToString(dr["PASSPORT_COPY_NAME"]);
                bytes = (byte[])dr["PASSPORT_COPY_DOC"];
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewPassportImgFile.ashx?emprecordid=" + empRecordID;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewPassportPDFFile.aspx?emprecordid=" + empRecordID);
                    this.ModalPopupExtender3.Show();
                }
            }
            else
            {
                ExceptionMessage("File Doesn't exist!");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindEmployeeDetails(int empRecordID)
    {
        try
        {
            if (Session["EMPLOYEE_DETAILS"] != null)
            {
                DataTable dt = (DataTable)Session["EMPLOYEE_DETAILS"];
                foreach (DataRow dr in dt.Select("EMP_RECORD_ID='" + empRecordID + "'"))
                {
                    if (dr["EMPLOYEE_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMPLOYEE_NAME"])))
                        txtEmployeeNameNew.Text = Convert.ToString(dr["EMPLOYEE_NAME"]);
                    else
                        txtEmployeeNameNew.Text = string.Empty;

                    if (dr["EMPLOYEE_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMPLOYEE_ID"])))
                        txtEmployeeIDNew.Text = Convert.ToString(dr["EMPLOYEE_ID"]);
                    else
                        txtEmployeeIDNew.Text = string.Empty;

                    if (dr["DESIGNATION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGNATION"])))
                        txtDesignation.Text = Convert.ToString(dr["DESIGNATION"]);
                    else
                        txtDesignation.Text = string.Empty;


                    BindDepartmentToUpdate();
                    if (dr["DEPARTMENT_ID"] != DBNull.Value && Convert.ToInt32(dr["DEPARTMENT_ID"]) != 0)
                        ddlDepartmentNew.SelectedValue = Convert.ToString(dr["DEPARTMENT_ID"]);
                    else
                        ddlDepartmentNew.SelectedIndex = 0;


                    BindTimesheetDepartment();
                    if (dr["TIMESHEET_DEPT_ID"] != DBNull.Value && Convert.ToInt32(dr["TIMESHEET_DEPT_ID"]) != 0)
                        ddlTimesheetDept.SelectedValue = Convert.ToString(dr["TIMESHEET_DEPT_ID"]);
                    else
                        ddlTimesheetDept.SelectedIndex = 0;

                    if (dr["EMAIL_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMAIL_ID"])))
                        txtEmailID.Text = Convert.ToString(dr["EMAIL_ID"]);
                    else
                        txtEmailID.Text = string.Empty;


                    BindUnitToUpdate();
                    if (dr["UNIT_ID"] != DBNull.Value && Convert.ToInt32(dr["UNIT_ID"]) != 0)
                        ddlUnit.SelectedValue = Convert.ToString(dr["UNIT_ID"]);
                    else
                        ddlUnit.SelectedIndex = 0;



                    if (dr["TEAMLEADER_ID"] != DBNull.Value && Convert.ToInt32(dr["TEAMLEADER_ID"]) != 0)
                    {
                        BindExistingTeamleader(Convert.ToInt32(dr["TEAMLEADER_ID"]));
                    }
                    else
                        ddlTeamLeader.SelectedIndex = 0;

                    //if (dr["CATEGORY"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CATEGORY"])))
                    //{
                    //    ddlCategory.SelectedValue = Convert.ToString(dr["CATEGORY"]);
                    //    if (Convert.ToString(dr["CATEGORY"]) == "S")
                    //        ddlUserType.Enabled = true;
                    //    else
                    //        ddlUserType.Enabled = false;
                    //}
                    //else
                    //    ddlCategory.SelectedIndex = 0;


                    dr["CATEGORY"] = "S";

                    if (dr["USER_TYPE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["USER_TYPE"])))
                        ddlUserType.SelectedValue = Convert.ToString(dr["USER_TYPE"]);
                    else
                        ddlUserType.SelectedIndex = 0;

                    if (dr["TIMESHEET_EMP_TYPE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["TIMESHEET_EMP_TYPE"])))
                        ddlTimesheetEmployeeType.SelectedValue = Convert.ToString(dr["TIMESHEET_EMP_TYPE"]);
                    else
                        ddlTimesheetEmployeeType.SelectedIndex = 0;

                    if (dr["USER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["USER_NAME"])))
                        txtUserName.Text = Convert.ToString(dr["USER_NAME"]);
                    else
                        txtUserName.Text = string.Empty;

                    if (dr["PASSWORD"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["PASSWORD"])))
                        txtPassword.Text = Convert.ToString(dr["PASSWORD"]);
                    else
                        txtPassword.Text = string.Empty;




                    BindBankList();
                    if (dr["BANK_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["BANK_ID"])))
                    {
                        ddlBankName.SelectedValue = Convert.ToString(dr["BANK_ID"]);
                        chkBankDetails.Checked = true;
                    }
                    else
                    {
                        ddlBankName.SelectedIndex = 0;
                        //chkBankDetails.Checked = false;
                        chkBankDetails.Checked = true;
                    }

                    if (dr["IFSC"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["IFSC"])))
                        txtIFSC.Text = Convert.ToString(dr["IFSC"]);
                    else
                        txtIFSC.Text = string.Empty;

                    if (dr["ACCOUNT_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["ACCOUNT_NO"])))
                        txtAccountNo.Text = Convert.ToString(dr["ACCOUNT_NO"]);
                    else
                        txtAccountNo.Text = string.Empty;

                    if (chkBankDetails.Checked)
                    {
                        ddlBankName.Enabled = true;
                        txtIFSC.Enabled = true;
                        txtAccountNo.Enabled = true;
                    }
                    else
                    {
                        ddlBankName.Enabled = false;
                        txtIFSC.Enabled = false;
                        txtAccountNo.Enabled = false;
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

    private void UpdateEmployee()
    {
        try
        {
            int employeeRecordID = 0;
            string employeeName = string.Empty;
            string employeeID = string.Empty;
            string designation = string.Empty;
            int departmentID = 0;
            int timesheetDeptID = 0;
            string emailID = string.Empty;
            int unitID = 0;
            int teamLeaderID = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string category = string.Empty;
            string userType = string.Empty;
            string timesheetEmpType = string.Empty;
            string fileUploadPassportCopyFileName = string.Empty;

            int bankID = 0;
            string IFSC = string.Empty;
            string accountNo = string.Empty;

            employeeRecordID = Convert.ToInt32(ViewState["empRecordID"]);

            employeeName = txtEmployeeNameNew.Text;
            employeeID = txtEmployeeIDNew.Text;
            designation = txtDesignation.Text;
            departmentID = Convert.ToInt32(ddlDepartmentNew.SelectedValue);
            timesheetDeptID = Convert.ToInt32(ddlTimesheetDept.SelectedValue);

            emailID = txtEmailID.Text;
            unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (ddlTeamLeader.SelectedIndex > 0)
                teamLeaderID = Convert.ToInt32(ddlTeamLeader.SelectedValue);

            else
                teamLeaderID = 0;


            if (!string.IsNullOrEmpty(txtUserName.Text))
                userName = txtUserName.Text;
            else
                userName = string.Empty;

            if (!string.IsNullOrEmpty(txtPassword.Text))
                password = txtPassword.Text;
            else
                password = string.Empty;
            
            category = "S";
            userType = Convert.ToString(ddlUserType.SelectedValue);
            timesheetEmpType = Convert.ToString(ddlTimesheetEmployeeType.SelectedValue);

            Byte[] fileUploadPassportCopyBytes = null;
            if (fileUploadPassportCopy.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadPassportCopy.PostedFile.FileName))
                {
                    fileUploadPassportCopyFileName = fileUploadPassportCopy.PostedFile.FileName;
                    fileUploadPassportCopyBytes = GetFileBytes(fileUploadPassportCopy.PostedFile.FileName, fileUploadPassportCopy.PostedFile.InputStream);
                }
                else
                {
                    fileUploadPassportCopyFileName = string.Empty;
                    fileUploadPassportCopyBytes = null;
                }
            }
            else
            {
                fileUploadPassportCopyFileName = string.Empty;
                fileUploadPassportCopyBytes = null;
            }

            if (chkBankDetails.Checked)
            {
                if (ddlBankName.SelectedIndex > 0)
                    bankID = Convert.ToInt32(ddlBankName.SelectedValue);
                else
                    bankID = 0;

                if (!string.IsNullOrEmpty(txtIFSC.Text))
                    IFSC = txtIFSC.Text.ToUpper();
                else
                    IFSC = string.Empty;

                if (!string.IsNullOrEmpty(txtAccountNo.Text))
                    accountNo = txtAccountNo.Text;
                else
                    accountNo = string.Empty;
            }
            else
            {
                bankID = 0;
                IFSC = string.Empty;
                accountNo = string.Empty;
            }

            int value = objCommon.AddUpdateEmployee(employeeRecordID, employeeName, employeeID, designation, departmentID, timesheetDeptID,
                                                    emailID, unitID, teamLeaderID, userName, password, category, userType, timesheetEmpType,
                                                    fileUploadPassportCopyFileName, fileUploadPassportCopyBytes,
                                                    bankID, IFSC, accountNo,
                                                    Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Employee updated successfully");
                GetEmployeeList();
            }
            else if (value < 0)
            {
                ExceptionUpdateMessage("User name already exists...!!");
                this.ModalPopupExtender1.Show();
            }
            else
            {
                ExceptionMessage("Please try again...!!");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private byte[] GetFileBytes(string fileName, Stream stream)
    {
        Byte[] GSTbytes = null;
        #region
        try
        {
            string GSTFilePath = fileName;
            string GSTFileName = Path.GetFileName(GSTFilePath);
            string GSText = Path.GetExtension(GSTFileName);
            string GSTContentType = String.Empty;
            switch (GSText)
            {
                case ".jpg":
                    GSTContentType = "image/jpg";
                    break;
                case ".jpeg":
                    GSTContentType = "image/jpeg";
                    break;
                case ".bmp":
                    GSTContentType = "image/bmp";
                    break;
                case ".png":
                    GSTContentType = "image/png";
                    break;
                case ".gif":
                    GSTContentType = "image/gif";
                    break;
                case ".pdf":
                    GSTContentType = "application/pdf";
                    break;
                case ".JPG":
                    GSTContentType = "image/JPG";
                    break;
                case ".JPEG":
                    GSTContentType = "image/JPEG";
                    break;
                case ".BMP":
                    GSTContentType = "image/BMP";
                    break;
                case ".PNG":
                    GSTContentType = "image/PNG";
                    break;
                case ".GIF":
                    GSTContentType = "image/GIF";
                    break;
                case ".PDF":
                    GSTContentType = "application/PDF";
                    break;
            }
            Stream GSTfs = null;
            BinaryReader GSTbr = null;
            if (GSTContentType != String.Empty)
            {
                try
                {
                    GSTfs = stream;
                    GSTfs.Position = 0;
                    GSTbr = new BinaryReader(GSTfs);
                    GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }

    private void ActivateEmployee(int actID, int empRecordID)
    {
        try
        {
            int value = objCommon.ActiaveDeactivateEmployee(actID, empRecordID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                GetEmployeeList();
                SuccessMessage("Activated successfully...!!");
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void DeactivateEmployee(int actID, int empRecordID)
    {
        try
        {
            int value = 0;
            string teamMembersID = string.Empty;
            int newTeamLeaderID = 0;

            //BindNewTeamleaderWhenDeactivateUser();

            newTeamLeaderID = Convert.ToInt32(ddlNewTeamleader.SelectedValue);

            if (gvTeamMemberList.Rows.Count > 0)
            {
                if (empRecordID != newTeamLeaderID)
                {
                    value = objCommon.ActiaveDeactivateEmployee(actID, empRecordID, newTeamLeaderID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                }
                else
                {
                    ExceptionDeactivateMessage("Current Teamleader and new teamleader can not same..!!");
                    this.ModalPopupExtender4.Show();
                }
            }
            else
            {
                value = objCommon.ActiaveDeactivateEmployee(actID, empRecordID, newTeamLeaderID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }


            if (value > 0)
            {
                GetEmployeeList();
                SuccessMessage("Deactivated successfully...!!");
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!");
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

    private void ExceptionUpdateMessage(string message)
    {
        pnlUpdateMsg.Visible = true;
        lblUpdateMsg.Text = message;
        lblUpdateMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void ExceptionDeactivateMessage(string message)
    {
        pnlDeactiveMsg.Visible = true;
        lblDeactiveMsg.Text = message;
        lblDeactiveMsg.ForeColor = System.Drawing.Color.Red;
    }


    private void HidePanels()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;

        pnlUpdateMsg.Visible = false;
        lblUpdateMsg.Text = string.Empty;

        pnlDeactiveMsg.Visible = false;
        lblDeactiveMsg.Text = string.Empty;
    }
    #endregion

}
