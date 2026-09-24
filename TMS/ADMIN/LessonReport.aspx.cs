using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ADMIN_LessonReport : System.Web.UI.Page
{

    BAL.LessonLearnt objLessonLearnt = new BAL.LessonLearnt();
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string customerName = string.Empty;
    string jobNumber = string.Empty;
    int unitID = 0;
    int departmentID = 0;
    int empID = 0;
    int createdByID = 0;
    int equipmentID = 0;
    
    DataSet dsEmployee = new DataSet();
    DataSet dsLessonList = new DataSet();
    DataSet dsDepartment = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsEquipments = new DataSet();
    DataSet dsCreatedBy = new DataSet();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {

            if (!IsPostBack)
            {

                BindEmployee();
                BindDepartment();
                BindCompany();
                BindEquipments();
                BindCreatedBy();

                hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = hdStartDate.Value.ToString();

                hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = hdEndDate.Value.ToString();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }


    private void BindCompany()
    {
        try
        {
            dsUnit = objLessonLearnt.Unit();
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


    private void BindCreatedBy()
    {
        try
        {
            dsCreatedBy = objLessonLearnt.GetCreatedBy();
            if (dsCreatedBy.Tables.Count > 0 && dsCreatedBy.Tables[0].Rows.Count > 0)
            {
                ddlCreatedBy.DataSource = dsCreatedBy.Tables[0];
                ddlCreatedBy.DataTextField = "EMPLOYEE_NAME";
                ddlCreatedBy.DataValueField = "EMP_RECORD_ID";
                ddlCreatedBy.DataBind();
                ddlCreatedBy.Items.Insert(0, "Select");
                ddlCreatedBy.SelectedIndex = 0;

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

    private void BindEmployee()
    {
        try
        {
            dsEmployee = objLessonLearnt.GetEmpListForLLReport();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        lblMsg.Visible = false;
        GetLessonList();
        //Reset();
    }

    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
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

            string fileName = "LessonLearntReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvLessonLearntList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsLessonList"];
            ToCSVNew01(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    protected void gvLessonLearntList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLessonLearntList.PageIndex = e.NewPageIndex;
        GetLessonList();
    }

    protected void gvLessonLearntList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");

            //    //new
            //    if (Convert.ToString(lblStatusName.Text) == "New")
            //    {
            //        for (int i = 0; i < e.Row.Cells.Count; i++)
            //        {
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
            //        }
            //    }
            //    //Dept-Assigned
            //    else if (Convert.ToString(lblStatusName.Text) == "Assigned-Dept")
            //    {
            //        for (int i = 0; i < e.Row.Cells.Count; i++)
            //        {
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
            //        }
            //    }
            //    //Person-Assigned
            //    else if (Convert.ToString(lblStatusName.Text) == "Assigned-Person")
            //    {
            //        for (int i = 0; i < e.Row.Cells.Count; i++)
            //        {
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.LightSeaGreen;
            //        }
            //    }
            //    //Resolved
            //    else if (Convert.ToString(lblStatusName.Text) == "Resolved")
            //    {
            //        for (int i = 0; i < e.Row.Cells.Count; i++)
            //        {
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.YellowGreen;
            //        }
            //    }
            //    //Approved
            //    else if (Convert.ToString(lblStatusName.Text) == "Approved")
            //    {
            //        for (int i = 0; i < e.Row.Cells.Count; i++)
            //        {
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.WhiteSmoke;
            //        }
            //    }
            //    //Closed
            //    else if (Convert.ToString(lblStatusName.Text) == "Closed")
            //    {
            //        for (int i = 0; i < e.Row.Cells.Count; i++)
            //        {
            //            e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
            //        }
            //    }



            //    for (int i = 0; i < e.Row.Cells.Count; i++)
            //    {
            //        e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            //    }
            //}
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }
    private void GetLessonList()
    {
        try
        {

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDate.Value)))
                fromDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDate.Value)))
                toDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                empID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empID = 0;

            if (ddlDepartment.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            else
                departmentID = 0;

            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);
            else
                unitID = 0;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text.Trim();
            else
                customerName = string.Empty;
            
            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNumber = txtJobNo.Text.Trim();
            else
                jobNumber = string.Empty;

            if (ddlEquipments.SelectedIndex > 0)
                equipmentID = Convert.ToInt32(ddlEquipments.SelectedValue);
            else
                equipmentID = 0;

            if (ddlCreatedBy.SelectedIndex > 0)
                  createdByID = Convert.ToInt32(ddlCreatedBy.SelectedValue);
            else
                createdByID = 0;


            dsLessonList = objLessonLearnt.GetLessonLearntReportNew(fromDate, toDate, empID, departmentID, unitID,
                customerName, jobNumber, equipmentID, createdByID);

            if (dsLessonList.Tables.Count > 0 && dsLessonList.Tables[0].Rows.Count > 0)
            {
                Session["dsLessonList"] = dsLessonList;
                gvLessonLearntList.DataSource = dsLessonList.Tables[0];
                gvLessonLearntList.DataBind();
                lblRecords.Text = "Records[" + dsLessonList.Tables[0].Rows.Count + "]";
            }
            else
            {
                Session["dsLessonList"] = dsLessonList;
                gvLessonLearntList.DataSource = null;
                gvLessonLearntList.DataBind();
                lblRecords.Text = "Records[0]";
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
            dsDepartment = objLessonLearnt.GetDeptListForLLReport();
            if (dsDepartment.Tables.Count > 0 && dsDepartment.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = dsDepartment.Tables[0];
                ddlDepartment.DataTextField = "DEPARTMENT_NAME_LL";
                ddlDepartment.DataValueField = "DEPARTMENT_ID_LL";
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
}