using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_AddLesson : System.Web.UI.Page
{
    BAL.LessonLearnt objLessonLearnt = new BAL.LessonLearnt();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsDepartment = new DataSet();
    DataSet dsInitiatedBy = new DataSet();
    DataSet dsCustomer = new DataSet();
    DataSet dsEquipments = new DataSet();
    DataSet dsJobNo = new DataSet();
    DataSet dsUnit = new DataSet();
    int companyID = 0;
    int unitID = 0;
    int departmentID = 0;
    string custCode = string.Empty;
    string jobNo = string.Empty;
    string poNo = string.Empty;
    string lessonLearntDate = string.Empty;
    int InitiatedByEmployeeId = 0;
    int EquipmentId = 0;
    string CustomerName = string.Empty;
    string CustomerCode = string.Empty;
    string LessonLearnt = string.Empty;
    string ProblemFaced = string.Empty;
    string JobNo = string.Empty;
    string custName = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDepartment();
            BindInitiatedbBy();
            BindCompany();
            BindEquipments();
            Reset();
        }


    }

    private void BindDepartment()
    {
        try
        {
            dsDepartment = objLessonLearnt.GetDepartment();
            if (dsDepartment.Tables.Count > 0 && dsDepartment.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = dsDepartment.Tables[0];
                ddlDepartment.DataTextField = "DEPARTMENT_NAME_LL";
                ddlDepartment.DataValueField = "DEPARTMENT_ID_LL";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, "Select");
                //ddlDepartment.SelectedValue = "India";
                ddlDepartment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindInitiatedbBy()
    {
        try
        {
            dsInitiatedBy = objLessonLearnt.GetInitiatedBy();
            if (dsInitiatedBy.Tables.Count > 0 && dsInitiatedBy.Tables[0].Rows.Count > 0)
            {
                ddlInitiatedBy.DataSource = dsInitiatedBy.Tables[0];
                ddlInitiatedBy.DataTextField = "EMPLOYEE_NAME";
                ddlInitiatedBy.DataValueField = "EMP_RECORD_ID";
                ddlInitiatedBy.DataBind();
                ddlInitiatedBy.Items.Insert(0, "Select");
                //ddlDepartment.SelectedValue = "India";
                ddlInitiatedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCompany()
    {
        try
        {
            dsUnit = objLessonLearnt.Unit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                //ddlCompany.Items.Insert(0, "Select");
                ddlCompany.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEquipments()
    {
        try
        {
            dsEquipments = objLessonLearnt.GetEquipmentList();
            if (dsEquipments.Tables.Count > 0 && dsEquipments.Tables[0].Rows.Count > 0)
            {
                ddlEquipments.DataSource = dsEquipments.Tables[0];
                ddlEquipments.DataTextField = "EQUIPMENT_NAME_LL";
                ddlEquipments.DataValueField = "EQUIPMENT_ID_LL";
                ddlEquipments.DataBind();
                ddlEquipments.Items.Insert(0, "Select");
                //ddlDepartment.SelectedValue = "India";
                ddlEquipments.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtJOBNo.Text = string.Empty;
            txtCustomerCode.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtProblemFaced.Text = string.Empty;
            txtLessonLearnt.Text = string.Empty;
            //ddlInitiatedBy.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            //ddlCompany.SelectedIndex = 1;
            ddlEquipments.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertLessonLearntLog();
    }


    private void InsertLessonLearntLog()
    {
        try
        {
            if (!string.IsNullOrEmpty(hdDate.Value))
            {
                DateTime dateValue = Convert.ToDateTime(hdDate.Value);
                lessonLearntDate = dateValue.ToString("yyyy-MM-dd");
            }
            else
            {
                // Handle case where hdDate.Value is null or empty
                // You may choose to set a default value or display an error message
                // For demonstration, setting it to a default value
                lessonLearntDate = DateTime.Today.ToString("yyyy-MM-dd");
            }
            //lessonLearntDate = Convert.ToDateTime(hdDate.Value).ToString("yyyy-MM-dd");

            //employee

            if (ddlInitiatedBy.SelectedIndex > 0)
                InitiatedByEmployeeId = Convert.ToInt32(ddlInitiatedBy.SelectedValue);
            else
                InitiatedByEmployeeId = 0;


            // department


            if (ddlDepartment.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            else
                departmentID = 0;


            //unit
            unitID = Convert.ToInt32(ddlCompany.SelectedValue);


            //job number string


            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JobNo = txtJOBNo.Text;
            else
                JobNo = string.Empty;


            // customer name
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                CustomerName = txtCustomerName.Text;
            else
                CustomerName = string.Empty;

            //ccustomer code


            if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                CustomerCode = txtCustomerCode.Text;
            else
                CustomerCode = string.Empty;


            //equipment id

            if (ddlEquipments.SelectedIndex > 0)
                EquipmentId = Convert.ToInt32(ddlEquipments.SelectedValue);
            else
                EquipmentId = 0;

            //problem faced remark

            if (!string.IsNullOrEmpty(txtProblemFaced.Text))
                ProblemFaced = txtProblemFaced.Text;
            else
                ProblemFaced = string.Empty;



            //Lesson Learnt remark

            if (!string.IsNullOrEmpty(txtLessonLearnt.Text))
                LessonLearnt = txtLessonLearnt.Text;
            else
                LessonLearnt = string.Empty;



            int value = objLessonLearnt.InsertUpdateLessonLearnt(lessonLearntDate, InitiatedByEmployeeId, Convert.ToInt32(Session["EMP_RECORD_ID"]), departmentID, unitID, JobNo,
                                CustomerName, CustomerCode, EquipmentId, ProblemFaced,
                                LessonLearnt);

            if (value > 0)
            {
                SuccessMessage("Lesson learnt is generated successfully!");
            }

            Reset();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        mpeJOBDetail.Show();
        GetJOBDetail();
    }


    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        mpeJOBDetail.Show();
        GetJOBDetail();
    }

    protected void gvJOBDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            int rowindex = 0;
            btnAddApprover.Visible = false;
            GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            rowindex = rowSelect.RowIndex;

            Label lblJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNo") as Label;
            Label lblAppJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblAppJOBNo") as Label;
            Label lblPONo = gvJOBDetail.Rows[rowindex].FindControl("lblPONo") as Label;
            Label lblCustomerName = gvJOBDetail.Rows[rowindex].FindControl("lblCustomerName") as Label;
            Label lblCustCode = gvJOBDetail.Rows[rowindex].FindControl("lblCustCode") as Label;

            txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
            txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
            txtCustomerCode.Text = Convert.ToString(lblCustCode.Text).Trim();

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnLessonList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/LessonReport.aspx");
    }

    private void GetJOBDetail()
    {
        try
        {
            dsJobNo = GetJOBData();
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsJobNo.Tables[0];
                gvJOBDetail.DataBind();
            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                lblJOBMsg.ForeColor = System.Drawing.Color.Green;
                // lblJOBMsg.Style["text-align"] = "center";
                gvJOBDetail.DataSource = null;
                gvJOBDetail.DataBind();
            }
            lblJOBRecords.Text = "Records[" + gvJOBDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }



    private DataSet GetJOBData()
    {
        try
        {
            companyID = 0;
            custCode = string.Empty;
            jobNo = string.Empty;
            poNo = string.Empty;

            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtCustomerCodeSearch.Text))
                custCode = txtCustomerCodeSearch.Text.Trim();
            else
                custCode = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text.Trim();
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtPONoSearch.Text))
                poNo = txtPONoSearch.Text.Trim();
            else
                poNo = string.Empty;

            //if (!string.IsNullOrEmpty(txtPONoSearch.Text))
            //    custName = txtPONoSearch.Text;
            //else
            //    custName = string.Empty;



            dsJobNo = objLessonLearnt.GetJOBDetailsForLOT(companyID, custCode, jobNo, poNo);

            if (dsJobNo.Tables.Count > 0)
            {
                return dsJobNo;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private void Reset()
    {
        try
        {
            hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtJOBNo.Text = string.Empty;
            txtCustomerCode.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtProblemFaced.Text = string.Empty;
            txtLessonLearnt.Text = string.Empty;
            ddlInitiatedBy.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            ddlEquipments.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void SuccessMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Green;
    }



}