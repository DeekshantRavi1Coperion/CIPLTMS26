using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

public partial class VOUCHER_AUTH_VouchersDirecoryPathList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();

    public int Pid
    {
        get
        {
            if (ViewState["PID"] != null)
                _pid = Convert.ToInt32(ViewState["PID"]);
            else _pid = 0;

            return _pid;
        }
    }

    public int TypeId
    {
        get
        {
            if (ViewState["TYPE_ID"] != null)
                _typeId = Convert.ToInt32(ViewState["TYPE_ID"]);
            else _typeId = 0;

            return _typeId;
        }
    }

    public int EmpRecordId
    {
        get
        {
            if (ViewState["EMP_RECORD_ID"] != null)
                _empRecordId = Convert.ToInt32(ViewState["EMP_RECORD_ID"]);
            else _empRecordId = 0;

            return _empRecordId;
        }
    }

    public int CreatedById
    {
        get
        {
            if (Session["EMP_RECORD_ID"] != null)
                _createdById = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            else _createdById = 0;

            return _createdById;
        }
    }

    public string DirectoryPath
    {
        get
        {
            if (!string.IsNullOrEmpty(txtDirectoryPathAS.Text))
                _directoryPath = txtDirectoryPathAS.Text;
            else _directoryPath = "";

            return _directoryPath;
        }
    }





    public int TypeIDToS
    {
        get
        {
            if (ddlVucherTypeToS.SelectedIndex > 0)
                _typeIDToS = Convert.ToInt32(ddlVucherTypeToS.SelectedValue);
            else _typeIDToS = 0;

            return _typeIDToS;
        }
    }

    public int EmpRecordIDToS
    {
        get
        {
            if (ddlEmployeeToS.SelectedIndex > 0)
                _empRecordIDToS = Convert.ToInt32(ddlEmployeeToS.SelectedValue);
            else _empRecordIDToS = 0;

            return _empRecordIDToS;
        }
    }

    public string DirectoryPathToS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtDirectoryPathToS.Text))
                _directoryPathToS = txtDirectoryPathToS.Text;
            else _directoryPathToS = "";

            return _directoryPathToS;
        }
    }


    private int _pid;
    private DataSet dsUsers;
    private string _directoryPath;
    private int _empRecordId;
    private int _typeId;
    private DataSet dsList;
    private int _createdById = 0;
    private int _typeIDToS;
    private int _empRecordIDToS;
    private string _directoryPathToS;
    private DataSet dsVoucherTypes;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindUsers();
                BindVoucherTypes();

                GetVoucherPathList();
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
        GetVoucherPathList();
    }

    protected void gvDirectoryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvDirectoryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "UPDATE_PATH")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvDirectoryList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblVoucherTypeID = gvDirectoryList.Rows[rowindex].FindControl("lblVoucherTypeID") as Label;
                Label lblEmpRecordID = gvDirectoryList.Rows[rowindex].FindControl("lblEmpRecordID") as Label;
                Label lblFuser = gvDirectoryList.Rows[rowindex].FindControl("lblFuser") as Label;
                Label lblDirectoryPath = gvDirectoryList.Rows[rowindex].FindControl("lblDirectoryPath") as Label;
                Label lblVoucherType = gvDirectoryList.Rows[rowindex].FindControl("lblVoucherType") as Label;
                Label lblUserName = gvDirectoryList.Rows[rowindex].FindControl("lblUserName") as Label;

                ViewState["PID"] = lblPID.Text;
                ViewState["TYPE_ID"] = lblVoucherTypeID.Text;
                ViewState["EMP_RECORD_ID"] = lblEmpRecordID.Text;


                txtVoucherTypeAS.Text = lblVoucherType.Text;
                txtFuserAS.Text = lblFuser.Text;
                txtNameAS.Text = lblUserName.Text;
                txtDirectoryPathAS.Text = lblDirectoryPath.Text;

                if (Convert.ToString(e.CommandArgument) == "UPDATE_PATH")
                {
                    mpeAuthorizeVoucher.Show();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    protected void btnUpdateDirectoryPath_Click(object sender, EventArgs e)
    {
        UpdateDirectoryPath();
    }

    protected void btnAddPath_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VOUCHER_AUTH/AddDirectoryPath.aspx");
    }

    #endregion


    #region METHODS[=======================]

    private void BindUsers()
    {
        try
        {
            dsUsers = objVouchersAuthorization.GetUsersForDirectoryPath(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsUsers.Tables.Count > 0 && dsUsers.Tables[0].Rows.Count > 0)
            {
                ddlEmployeeToS.DataSource = dsUsers.Tables[0];
                ddlEmployeeToS.DataTextField = "FACT_USER_NAME";
                ddlEmployeeToS.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeToS.DataBind();
                ddlEmployeeToS.Items.Insert(0, "All");
                ddlEmployeeToS.SelectedIndex = 0;

                if (dsUsers.Tables[0].Rows.Count == 1)
                {
                    ddlEmployeeToS.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                    ddlEmployeeToS.Enabled = false;
                }
            }
            else
            {
                ddlEmployeeToS.Items.Insert(0, "All");
                ddlEmployeeToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindVoucherTypes()
    {
        try
        {
            dsVoucherTypes = objVouchersAuthorization.GetVoucherTypesForDirectoryPath("", 1);
            if (dsVoucherTypes.Tables.Count > 0 && dsVoucherTypes.Tables[0].Rows.Count > 0)
            {
                ddlVucherTypeToS.DataSource = dsVoucherTypes.Tables[0];
                ddlVucherTypeToS.DataTextField = "NAME";
                ddlVucherTypeToS.DataValueField = "PID";
                ddlVucherTypeToS.DataBind();
                ddlVucherTypeToS.Items.Insert(0, "All");
                ddlVucherTypeToS.SelectedIndex = 0;
            }
            else
            {
                ddlVucherTypeToS.Items.Clear();
                ddlVucherTypeToS.Items.Insert(0, "All");
                ddlVucherTypeToS.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetVoucherPathList()
    {
        try
        {
            dsList = objVouchersAuthorization.GetVoucherDirectoryPathList(TypeIDToS, EmpRecordIDToS, DirectoryPathToS);
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                gvDirectoryList.DataSource = dsList.Tables[0];
                gvDirectoryList.DataBind();
            }
            else
            {
                gvDirectoryList.DataSource = null;
                gvDirectoryList.DataBind();
            }

            lblRecords.Text = "Records[" + gvDirectoryList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateDirectoryPath()
    {
        int val = objVouchersAuthorization.AddUpdateDirectoryPath
              (
                   Pid
                 , TypeId
                 , EmpRecordId
                 , DirectoryPath
                 , CreatedById
              );

        if (val > 0)
        {
            SuccessMessage("Directory path updated successfully!");
            GetVoucherPathList();
        }
        else
        {
            ExceptionMessage("Please try again.");
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

    #endregion

}
