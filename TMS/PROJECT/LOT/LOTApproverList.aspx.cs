using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using BAL;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.tool.xml;

public partial class PROJECT_LOT_LOTApproverList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsEmployees = new DataSet();
    DataSet dsApproversList = new DataSet();
    DataSet dsUnit = new DataSet();

    int unitID = 0;
    int recordID = 0;
    int jobUnitID = 0;
    string jobNo = string.Empty;
    int peID = 0;
    int pmID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["dtApproverList"] = null;
                BindCompany();
                BindApprovers();
                GetApproverList();
            }
        }
        else
            Response.Redirect("~/Login.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetApproverList();
    }

    protected void gvApproverList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = gvApproverList.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblJobUnitID = gvApproverList.Rows[rowindex].FindControl("lblJobUnitID") as Label;
                Label lblJOBNo = gvApproverList.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblJobUnit = gvApproverList.Rows[rowindex].FindControl("lblJobUnit") as Label;
                Label lblPMApproverID = gvApproverList.Rows[rowindex].FindControl("lblPMApproverID") as Label;
                Label lblPEApproverID = gvApproverList.Rows[rowindex].FindControl("lblPEApproverID") as Label;


                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (!string.IsNullOrEmpty(lblRecordID.Text))
                        ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);
                    else
                        ViewState["RECORD_ID"] = 0;

                    if (!string.IsNullOrEmpty(lblJOBNo.Text))
                        txtJOBNoToEdit.Text = lblJOBNo.Text;
                    else
                        txtJOBNoToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblJobUnitID.Text))
                        ddlCompanyToEdit.SelectedValue = Convert.ToString(lblJobUnitID.Text);
                    else
                        ddlCompanyToEdit.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(lblPMApproverID.Text))
                        ddlProjectManagerToEdit.SelectedValue = Convert.ToString(lblPMApproverID.Text);
                    else
                        ddlProjectManagerToEdit.SelectedIndex = 0;

                    if (!string.IsNullOrEmpty(lblPEApproverID.Text))
                        ddlProjectEngineerToEdit.SelectedValue = Convert.ToString(lblPEApproverID.Text);
                    else
                        ddlProjectEngineerToEdit.SelectedIndex = 0;

                    ModalPopupExtender1.Show();
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

    protected void btnAddNewApprover_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/AddLOTApprover.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateApprover();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvApproverList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtApproverList"];
            ExportToExcel(dt);
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindCompany()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "Select");

                ddlCompanyToEdit.DataSource = dsUnit.Tables[0];
                ddlCompanyToEdit.DataTextField = "UNIT_NAME";
                ddlCompanyToEdit.DataValueField = "UNIT_ID";
                ddlCompanyToEdit.DataBind();
                ddlCompanyToEdit.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindApprovers()
    {
        try
        {
            dsEmployees = objProject.GetEmployeesToAddApprover();

            if (dsEmployees.Tables.Count > 0 && dsEmployees.Tables[0].Rows.Count > 0)
            {
                ddlProjectEngineer.DataSource = dsEmployees.Tables[0];
                ddlProjectEngineer.DataTextField = "EMPLOYEE_NAME";
                ddlProjectEngineer.DataValueField = "EMP_RECORD_ID";
                ddlProjectEngineer.DataBind();
                ddlProjectEngineer.Items.Insert(0, "All");
                ddlProjectEngineer.SelectedIndex = 0;

                ddlProjectManager.DataSource = dsEmployees.Tables[0];
                ddlProjectManager.DataTextField = "EMPLOYEE_NAME";
                ddlProjectManager.DataValueField = "EMP_RECORD_ID";
                ddlProjectManager.DataBind();
                ddlProjectManager.Items.Insert(0, "All");
                ddlProjectManager.SelectedIndex = 0;

                ddlProjectEngineerToEdit.DataSource = dsEmployees.Tables[0];
                ddlProjectEngineerToEdit.DataTextField = "EMPLOYEE_NAME";
                ddlProjectEngineerToEdit.DataValueField = "EMP_RECORD_ID";
                ddlProjectEngineerToEdit.DataBind();
                ddlProjectEngineerToEdit.Items.Insert(0, "Select");
                ddlProjectEngineerToEdit.SelectedIndex = 0;

                ddlProjectManagerToEdit.DataSource = dsEmployees.Tables[0];
                ddlProjectManagerToEdit.DataTextField = "EMPLOYEE_NAME";
                ddlProjectManagerToEdit.DataValueField = "EMP_RECORD_ID";
                ddlProjectManagerToEdit.DataBind();
                ddlProjectManagerToEdit.Items.Insert(0, "Select");
                ddlProjectManagerToEdit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetApproverList()
    {
        try
        {
            unitID = 0;
            jobNo = string.Empty;
            peID = 0;
            pmID = 0;


            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

            if (ddlProjectEngineer.SelectedIndex > 0)
                peID = Convert.ToInt32(ddlProjectEngineer.SelectedValue);

            if (ddlProjectManager.SelectedIndex > 0)
                pmID = Convert.ToInt32(ddlProjectManager.SelectedValue);

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            dsApproversList = objProject.GetLOTApproversList(jobNo, peID, pmID, unitID);

            if (dsApproversList.Tables.Count > 0 && dsApproversList.Tables[0].Rows.Count > 0)
            {
                Session["dtApproverList"]= dsApproversList.Tables[0];
                gvApproverList.DataSource = dsApproversList.Tables[0];
                gvApproverList.DataBind();
            }
            else
            {
                Session["dtApproverList"] = null ;
                gvApproverList.DataSource = null;
                gvApproverList.DataBind();
            }
            lblRecords.Text = "Records[" + dsApproversList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateApprover()
    {
        try
        {
            recordID = 0;
            jobUnitID = 0;
            jobNo = string.Empty;
            peID = 0;
            pmID = 0;

            if (Convert.ToInt32(ViewState["RECORD_ID"]) > 0)
                recordID = Convert.ToInt32(ViewState["RECORD_ID"]);

            if (!string.IsNullOrEmpty(txtJOBNoToEdit.Text))
                jobNo = txtJOBNoToEdit.Text;

            if (ddlProjectEngineerToEdit.SelectedIndex > 0)
                peID = Convert.ToInt32(ddlProjectEngineerToEdit.SelectedValue);

            if (ddlProjectManagerToEdit.SelectedIndex > 0)
                pmID = Convert.ToInt32(ddlProjectManagerToEdit.SelectedValue);

            if (ddlCompanyToEdit.SelectedIndex > 0)
                jobUnitID = Convert.ToInt32(ddlCompanyToEdit.SelectedValue);

            int value = 0;
            value = objProject.AddUpdateLOTApprover(recordID, jobUnitID, jobNo, pmID, peID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Approver updated successfully...!!!");
                GetApproverList();
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 6; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 6; k < dt.Columns.Count; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;

                    rowTxt = rowTxt.Replace(',', ' ');
                    rowTxt = rowTxt.TrimEnd('\r', ' ');
                    rowTxt = rowTxt.TrimEnd('\n', ' ');
                    rowTxt = rowTxt.Replace('\r', ' ');
                    rowTxt = rowTxt.Replace('\n', ' ');


                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "LOT_Approver_List_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}