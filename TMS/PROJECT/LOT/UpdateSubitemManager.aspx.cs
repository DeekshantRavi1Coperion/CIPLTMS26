using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Net.Mime;


public partial class PROJECT_LOT_UpdateSubitemManager : System.Web.UI.Page
{

    #region VARIABLES[=======================]
    
    BAL.Project objProject = new BAL.Project();
    DataSet dsDetails = new DataSet();

    int unitID = 0;
    int departmentID = 0;
    string LOTMainSubitems = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                if (Request.QueryString["unitid"] != null && Request.QueryString["unitid"] != null && Request.QueryString["unitid"] != null)
                {
                    if (Convert.ToInt32(Request.QueryString["unitid"]) > 0)
                        unitID = Convert.ToInt32(Request.QueryString["unitid"]);

                    if (Convert.ToInt32(Request.QueryString["departmentid"]) > 0)
                        departmentID = Convert.ToInt32(Request.QueryString["departmentid"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["lotmainsubitemids"])))
                        LOTMainSubitems = Convert.ToString(Request.QueryString["lotmainsubitemids"]);

                    if (unitID > 0 && departmentID > 0 && !string.IsNullOrEmpty(LOTMainSubitems))
                    {
                        BindSubitemDetails(unitID, departmentID, LOTMainSubitems);
                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewManagers();
    }

    #endregion


    #region METHODS[=========================]

    private void BindSubitemDetails(int unitID, int departmentID, string LOTMainSubitems)
    {
        try
        {
            dsDetails = objProject.GetSubitemDetailsToAddManager(unitID, departmentID, LOTMainSubitems);
            if (dsDetails.Tables.Count > 0 && dsDetails.Tables[0].Rows.Count > 0)
            {
                txtCompany.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["UNIT_NAME"]);
                hdCompanyID.Value = Convert.ToString(dsDetails.Tables[0].Rows[0]["UNIT_ID"]);


                gvSubItem.DataSource = dsDetails.Tables[0];
                gvSubItem.DataBind();
            }
            else
            {
                txtCompany.Text = string.Empty;
                hdCompanyID.Value = "0";
            }
            lblSubitemsRecords.Text = "Records[" + dsDetails.Tables[0].Rows.Count + "]";



            if (dsDetails.Tables.Count > 0 && dsDetails.Tables[1].Rows.Count > 0)
            {
                txtDepartment.Text = Convert.ToString(dsDetails.Tables[1].Rows[0]["DEPARTMENT_NAME"]);
                hdDepartmentID.Value = Convert.ToString(dsDetails.Tables[1].Rows[0]["DEPARTMENT_ID"]);
            }



            if (dsDetails.Tables.Count > 0 && dsDetails.Tables[2].Rows.Count > 0)
            {
                ddlManager.DataSource = dsDetails.Tables[2];
                ddlManager.DataTextField = "EMPLOYEE_NAME";
                ddlManager.DataValueField = "EMP_RECORD_ID";
                ddlManager.DataBind();
                ddlManager.Items.Insert(0, "Select");
                ddlManager.SelectedIndex = 0;
            }
            else
            {
                ddlManager.Items.Clear();
                ddlManager.Items.Insert(0, "Select");
                ddlManager.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddNewManagers()
    {
        try
        {
            DataTable dtTempSI = new DataTable();

            dtTempSI.Columns.Add("UNIT_ID", typeof(int));
            dtTempSI.Columns.Add("LOT_MAIN_SUBITEM_ID", typeof(int));
            dtTempSI.Columns.Add("DEPARTMENT_ID", typeof(int));
            dtTempSI.Columns.Add("MANAGER_ID", typeof(int));            


            if (gvSubItem.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubItem.Rows)
                {
                    Label lblLOTMainSubitemID = gr.FindControl("lblLOTMainSubitemID") as Label;

                    DataRow dr = dtTempSI.NewRow();
                    dr["UNIT_ID"] = Convert.ToInt32(hdCompanyID.Value);
                    dr["LOT_MAIN_SUBITEM_ID"] = Convert.ToInt32(lblLOTMainSubitemID.Text);
                    dr["DEPARTMENT_ID"] = Convert.ToInt32(hdDepartmentID.Value);

                    if (ddlManager.SelectedIndex > 0)
                        dr["MANAGER_ID"] = Convert.ToInt32(ddlManager.SelectedValue);
                }
            }

            int value = 0;
            if (dtTempSI.Rows.Count > 0)
            {
                value = objProject.AddNewManagersBySubitemIDs(dtTempSI, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (value > 0)
                {
                    SuccessMessage("Manager(s) added successfully..!!");
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again...!!!");
                    return;
                }
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}