using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ADMIN_EmployeeProfile : System.Web.UI.Page
{

    #region VARIABLES[===============]

    DataSet dsEmployee = new DataSet();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();

    #endregion


    #region EVENTS[==================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["EMPLOYEE_DETAILS"] = null;
                GetEmployeeDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void imgbtnPassportcopy_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(Session["EMP_RECORD_ID"]));
    }

    #endregion


    #region METHODS[=================]

    private void GetEmployeeDetails()
    {
        try
        {
            dsEmployee = objTourAndTravels.GetEmployeeList(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                Session["EMPLOYEE_DETAILS"] = dsEmployee.Tables[0];

                txtEmployeeName.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                txtEmployeeID.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["EMPLOYEE_ID"]);
                txtDesignation.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["DESIGNATION"]);
                txtDepartment.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["DEPARTMENT_NAME"]);
                txtTimesheetDepartment.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["TIMESHEET_DEPARTMENT"]);
                txtEmailID.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["EMAIL_ID"]);
                txtUnit.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["UNIT_NAME"]);
                txtTeamLeader.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["TEAMLEADER_NAME"]);
                txtUserType.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["USER_TYPE"]);
                txtUserName.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["USER_NAME"]);
                txtPassword.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["PASSWORD"]);

                txtPassportcopy.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["PASSPORT_COPY_NAME"]);
                if (!string.IsNullOrEmpty(txtPassportcopy.Text))
                    imgbtnPassportcopy.Visible = true;
                else
                    imgbtnPassportcopy.Visible = false;

                if (Convert.ToInt32(dsEmployee.Tables[0].Rows[0]["IS_ACTIVE"]) == 1)
                    chkIsActive.Checked = true;
                else
                    chkIsActive.Checked = false;

            }
            else
            {
                Session["EMPLOYEE_DETAILS"] = null;
            }
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
