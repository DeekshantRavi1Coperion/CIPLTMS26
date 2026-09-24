using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class ADMIN_WORKER_AddWorker : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DataSet dsDepartment = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsUnit = new DataSet();

    BAL.Common objCommon = new BAL.Common();

    string employeeName = string.Empty;
    string employeeID = string.Empty;
    string designation = string.Empty;
    int departmentID = 0;
    int unitID = 0;
    int teamLeaderID = 0;

    #endregion


    #region EVNETS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindDepartment();
                BindUnit();
                BindTeamLeaders();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddEmployee();
    }

    protected void btnEmployeeList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/WORKER/WorkerList.aspx");
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

    private void BindDepartment()
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
  
    private void BindTeamLeaders()
    {
        try
        {
            dsEmployee = objCommon.GetTeamLeadersByID(0);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlTeamLeader.DataSource = dsEmployee.Tables[0];
                ddlTeamLeader.DataTextField = "EMPLOYEE_NAME";
                ddlTeamLeader.DataValueField = "EMP_RECORD_ID";
                ddlTeamLeader.DataBind();
                ddlTeamLeader.Items.Insert(0, "Select");
                ddlTeamLeader.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void AddEmployee()
    {
        try
        {
            employeeName = txtEmployeeName.Text;
            employeeID = txtEmployeeID.Text;
            designation = txtDesignation.Text;
            departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (ddlTeamLeader.SelectedIndex > 0)
                teamLeaderID = Convert.ToInt32(ddlTeamLeader.SelectedValue);
            else
                teamLeaderID = 0;

            int value = objCommon.AddUpdateWorker(0, employeeName, employeeID, designation, departmentID,
                                                    unitID, teamLeaderID,
                                                    Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Worker added successfully");
                Reset();
            }
            else if (value < 0)
            {
                ExceptionMessage("Worker with employee code already exists...!!");
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

    private void Reset()
    {
        txtEmployeeName.Text = string.Empty;
        txtEmployeeID.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        ddlDepartment.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;
        ddlTeamLeader.SelectedIndex = 0;
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

    #endregion

}
