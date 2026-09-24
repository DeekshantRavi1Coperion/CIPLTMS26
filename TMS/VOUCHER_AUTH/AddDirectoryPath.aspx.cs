using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VOUCHER_AUTH_AddDirectoryPath : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();

    DataSet dsUsers = new DataSet();
    int menuID = 0;
    string menuName = string.Empty;
    string url = string.Empty;
    int parentMenuID = 0;
    int serialNo = 0;
    private DataSet dsVoucherTypes;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindVoucherTypes("");
                BindUsers();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string factUser = string.Empty;

            if (ddlUsers.SelectedIndex > 0)
            {
                factUser = ddlUsers.SelectedItem.Text;
            }

            BindVoucherTypes(factUser);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HidePannel();
        AddUpdateDirectoryPath();
    }

    protected void btnVoucherDirectoryPathList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VOUCHER_AUTH/VouchersDirecoryPathList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void BindUsers()
    {
        try
        {
            dsUsers = objVouchersAuthorization.GetUsersForDirectoryPath(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsUsers.Tables.Count > 0 && dsUsers.Tables[0].Rows.Count > 0)
            {
                ddlUsers.DataSource = dsUsers.Tables[0];
                ddlUsers.DataTextField = "FACT_USER_NAME";
                ddlUsers.DataValueField = "EMP_RECORD_ID";
                ddlUsers.DataBind();
                ddlUsers.Items.Insert(0, "Select");
                ddlUsers.SelectedIndex = 0;

                if (dsUsers.Tables[0].Rows.Count == 1)
                {
                    ddlUsers.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                    ddlUsers.Enabled = false;

                    BindVoucherTypes(ddlUsers.SelectedItem.Text);

                }

            }
            else
            {
                ddlUsers.Items.Insert(0, "Select");
                ddlUsers.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindVoucherTypes(string fUser)
    {
        try
        {
            if (!string.IsNullOrEmpty(fUser))
            {
                dsVoucherTypes = objVouchersAuthorization.GetVoucherTypesForDirectoryPath(fUser, 0);
                if (dsVoucherTypes.Tables.Count > 0 && dsVoucherTypes.Tables[0].Rows.Count > 0)
                {
                    ddlType.DataSource = dsVoucherTypes.Tables[0];
                    ddlType.DataTextField = "NAME";
                    ddlType.DataValueField = "PID";
                    ddlType.DataBind();
                    ddlType.Items.Insert(0, "Select");
                    ddlType.SelectedIndex = 0;
                }
                else
                {
                    ddlType.Items.Clear();
                    ddlType.Items.Insert(0, "Select");
                    ddlType.SelectedIndex = 0;
                }
            }
            else
            {
                ddlType.Items.Clear();
                ddlType.Items.Insert(0, "Select");
                ddlType.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddUpdateDirectoryPath()
    {
        try
        {
            int typeId = 0;
            int empRecordId = 0;
            string filePath = string.Empty;
            string fUser = string.Empty;


            if (ddlType.SelectedIndex > 0)
                typeId = Convert.ToInt32(ddlType.SelectedValue);

            if (ddlUsers.SelectedIndex > 0)
            {
                empRecordId = Convert.ToInt32(ddlUsers.SelectedValue);
                fUser = Convert.ToString(ddlUsers.SelectedItem.Text);
            }

            filePath = txtFilePath.Text;

            int value = objVouchersAuthorization.AddUpdateDirectoryPath(0, typeId, empRecordId, filePath
                                                                         , Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Path added successfully");
            }
            else
            {
                ExceptionMessage("Please try again");
            }

            BindVoucherTypes(fUser);

            txtFilePath.Text = string.Empty;
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
