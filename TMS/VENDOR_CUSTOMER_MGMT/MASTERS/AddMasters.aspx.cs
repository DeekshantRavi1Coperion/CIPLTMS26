using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VENDOR_CUSTOMER_MGMT_MASTERS_AddMasters : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DataSet dsItemCategory = new DataSet();

    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    BAL.Common objCommon = new BAL.Common();

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
    int bankID = 0;
    string IFSC = string.Empty;
    string accountNo = string.Empty;

    string fileUploadPassportCopyFileName = string.Empty;

    #endregion


    #region EVNETS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindItemCategorys();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddMaster();
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VENDOR_CUSTOMER_MGMT/MASTERS/MastersList.aspx");
    }

    #endregion


    #region METHODS[=========================]

    private void BindItemCategorys()
    {
        try
        {
            dsItemCategory = objVCM.GetItemCategorys();
            if (dsItemCategory.Tables.Count > 0 && dsItemCategory.Tables[0].Rows.Count > 0)
            {
                ddlItemCategoryToS.DataSource = dsItemCategory.Tables[0];
                ddlItemCategoryToS.DataTextField = "NAME";
                ddlItemCategoryToS.DataValueField = "PID";
                ddlItemCategoryToS.DataBind();
                ddlItemCategoryToS.Items.Insert(0, "All");
                ddlItemCategoryToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    

    private void AddMaster()
    {
        try
        {
            int masterTypeId = 0;
            int itemCategoryId = 0;
            string name = string.Empty;
            string description = string.Empty;
            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);


            if (ddlMasterTypeToS.SelectedIndex > 0) masterTypeId = Convert.ToInt32(ddlMasterTypeToS.SelectedValue);
            if (ddlItemCategoryToS.SelectedIndex > 0) itemCategoryId = Convert.ToInt32(ddlItemCategoryToS.SelectedValue);
            if (!string.IsNullOrEmpty(txtNameToS.Text)) name = txtNameToS.Text;
            if (!string.IsNullOrEmpty(txtDescriptionToS.Text)) description = txtDescriptionToS.Text;

            int value = objVCM.AddUpdateMasterTables(0, masterTypeId, name, description, itemCategoryId, createdBy);
            if (value > 0)
            {
                SuccessMessage("Saved successfully");
                Reset();
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


    private void Reset()
    {
        ddlMasterTypeToS.SelectedIndex = 0;
        ddlItemCategoryToS.SelectedIndex = 0;
        txtNameToS.Text = string.Empty;
        txtDescriptionToS.Text = string.Empty;
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
