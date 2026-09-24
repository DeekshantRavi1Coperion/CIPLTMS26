using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class ADMIN_WORKER_WorkerList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    DataSet dsDepartment = new DataSet();
    DataSet dsTimesheetDepartment = new DataSet();
    DataSet dsEmpList = new DataSet();

    DataSet dsEmployee = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsBank = new DataSet();

    int unitID = 0;
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
                ImageButton imgIsActive = (ImageButton)e.Row.FindControl("imgIsActive");
                Label lblIsActive = (Label)e.Row.FindControl("lblIsActive");

                if (Convert.ToInt32(lblIsActive.Text) > 0)
                {
                    imgIsActive.ImageUrl = "~/Images/Icons/yes3.png";
                    imgIsActive.ToolTip = "Active Worker. You can deactivate worker from here...!!";
                }
                else
                {
                    imgIsActive.ImageUrl = "~/Images/Icons/no2.png";
                    imgIsActive.ToolTip = "Deactive Worker. You can activate worker from here...!!";
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
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "IS_ACTIVE")
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
        Response.Redirect("~/ADMIN/WORKER/AddWorker.aspx");
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
                ddlDepartmentSearch.Items.Insert(0, "All");
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
                ddlDepartment.DataSource = dsDepartment.Tables[0];
                ddlDepartment.DataTextField = "DEPARTMENT_NAME";
                ddlDepartment.DataValueField = "DEPARTMENT_ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, "Select");
                ddlDepartment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

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

    private void GetTeamleaders()
    {
        try
        {
            //dsEmployee = objCommon.GetEmployeeByEmpRecordID(0);
            dsEmployee = objCommon.GetTeamLeadersByID(0);
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

            dsEmpList = objCommon.GetWorkerList(unitID, empName, empID, departmentID);
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
                        txtEmployeeName.Text = Convert.ToString(dr["EMPLOYEE_NAME"]);
                    else
                        txtEmployeeName.Text = string.Empty;

                    if (dr["EMPLOYEE_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMPLOYEE_ID"])))
                        txtEmployeeID.Text = Convert.ToString(dr["EMPLOYEE_ID"]);
                    else
                        txtEmployeeID.Text = string.Empty;

                    if (dr["DESIGNATION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESIGNATION"])))
                        txtDesignation.Text = Convert.ToString(dr["DESIGNATION"]);
                    else
                        txtDesignation.Text = string.Empty;


                    BindDepartmentToUpdate();
                    if (dr["DEPARTMENT_ID"] != DBNull.Value && Convert.ToInt32(dr["DEPARTMENT_ID"]) != 0)
                        ddlDepartment.SelectedValue = Convert.ToString(dr["DEPARTMENT_ID"]);
                    else
                        ddlDepartment.SelectedIndex = 0;

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
            int unitID = 0;
            int teamLeaderID = 0;

            employeeRecordID = Convert.ToInt32(ViewState["empRecordID"]);

            employeeName = txtEmployeeName.Text;
            employeeID = txtEmployeeID.Text;
            designation = txtDesignation.Text;
            departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

            unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (ddlTeamLeader.SelectedIndex > 0)
                teamLeaderID = Convert.ToInt32(ddlTeamLeader.SelectedValue);

            int value = objCommon.AddUpdateWorker(employeeRecordID, employeeName, employeeID, designation, departmentID, unitID, teamLeaderID,
                                                    Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Worker updated successfully");
                GetEmployeeList();
            }
            else if (value < 0)
            {
                ExceptionUpdateMessage("Worker already exists with employee ID already exists...!!");
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

            BindNewTeamleaderWhenDeactivateUser();

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
