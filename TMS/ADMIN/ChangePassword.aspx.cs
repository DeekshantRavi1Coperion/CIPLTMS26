using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ADMIN_ChangePassword : System.Web.UI.Page
{
    #region VARIABLES[=======================================]

    DataSet dsUser = new DataSet();
    BAL.Common objCommon = new BAL.Common();
    int empRecordID = 0;

    #endregion

    #region EVENTS[==========================================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {                
                GetUserDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        HidePannel();
        UpdatePassword();
    }

    #endregion

    #region METHODS[=========================================]

    private void GetUserDetails()
    {
        try
        {
            dsUser = objCommon.GetEmployeeByEmpRecordID(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
            {
                if (dsUser.Tables[0].Rows[0]["EMP_RECORD_ID"] != DBNull.Value)
                    empRecordID = Convert.ToInt32(dsUser.Tables[0].Rows[0]["EMP_RECORD_ID"]);
                else
                    empRecordID = 0;

                if (dsUser.Tables[0].Rows[0]["USER_NAME"] != DBNull.Value)
                    txtUserName.Value = Convert.ToString(dsUser.Tables[0].Rows[0]["USER_NAME"]);
                else
                    txtUserName.Value = string.Empty;

                if (dsUser.Tables[0].Rows[0]["PASSWORD"] != DBNull.Value)
                    txtOldPassword.Value = Convert.ToString(dsUser.Tables[0].Rows[0]["PASSWORD"]);
                else
                    txtOldPassword.Value = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdatePassword()
    {
        try
        {
            pnlMsg.Visible = false;
            lblMsg.Text = string.Empty;

            if (txtNewPassword.Value == txtConfirmedPassword.Value)
            {
                int value = objCommon.UpdatePassword(Convert.ToInt32(Session["EMP_RECORD_ID"]), txtNewPassword.Value);
                if (value > 0)
                {
                    pnlMsg.Visible = true;
                    lblMsg.Text = "Password updated successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    return;
                }
                else
                {
                    pnlMsg.Visible = true;
                    lblMsg.Text = "Please try again";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            else
            {
                pnlMsg.Visible = true;
                lblMsg.Text = "New password and confirm password must be same";
                lblMsg.ForeColor = System.Drawing.Color.Red;
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

    private void HidePannel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;        
    }

    #endregion
}
