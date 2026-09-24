using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI.HtmlControls;

public partial class PROJECT_MGMT_AddProjectGroupSubgroup : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsProject = new DataSet();
    DataSet dsMRType = new DataSet();
    DataSet dsUnit = new DataSet();

    DataSet dsProductUnit = new DataSet();
    DataSet dsEstimatedProduct = new DataSet();
    DataSet dsGroup = new DataSet();
    DataSet dsSubgroup = new DataSet();

    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HidePanel();
                Session["dt"] = null;
                BindProjectNo();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnAddNewMR_Click(object sender, EventArgs e)
    {
        HidePanel();
        ddlProjectNo.Enabled = false;
        BindNewGroupSubgroup();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HidePanel();
        SaveGroupAndSubgroup();
    }

    protected void ddlProjectNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //
    }

    protected void gvSubgroupList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HidePanel();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvSubgroupList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                Label lblSerialNo = gvSubgroupList.Rows[rowindex].FindControl("lblSerialNo") as Label;
                if (e.CommandArgument == "REMOVE")
                {
                    DataTable dt = (DataTable)Session["dt"];
                    RemoveRecord(Convert.ToInt32(lblSerialNo.Text));
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

    #endregion


    #region METHODS[============================]

    private void BindProjectNo()
    {
        try
        {
            dsProject = objProject.GetDetailsBySP("get_estimated_projects");
            if (dsProject.Tables.Count > 0 && dsProject.Tables[0].Rows.Count > 0)
            {
                ddlProjectNo.DataSource = dsProject.Tables[0];
                ddlProjectNo.DataTextField = "PROJECT_NO";
                ddlProjectNo.DataValueField = "PROJECT_NO";
                ddlProjectNo.DataBind();
                ddlProjectNo.Items.Insert(0, "Select");
                ddlProjectNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindNewGroupSubgroup()
    {
        try
        {
            int count = 1;
            DataTable dt = new DataTable();
            if (gvSubgroupList.Rows.Count > 0)
            {
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("SERIAL_NO", typeof(string));
                dtNew.Columns.Add("PROJECT_NO", typeof(string));
                dtNew.Columns.Add("GROUP_ID", typeof(string));
                dtNew.Columns.Add("GROUP_NAME", typeof(string));
                dtNew.Columns.Add("SUBGROUP_NAME", typeof(string));

                foreach (GridViewRow gr in gvSubgroupList.Rows)
                {
                    DataRow dr = dtNew.NewRow();

                    Label lblProjectNo = (Label)gr.FindControl("lblProjectNo");
                    Label lblGroupID = (Label)gr.FindControl("lblGroupID");
                    Label lblGroupName = (Label)gr.FindControl("lblGroupName");
                    TextBox txtSubgroupName = (TextBox)gr.FindControl("txtSubgroupName");


                    dr["SERIAL_NO"] = Convert.ToString(count++);

                    if (!string.IsNullOrEmpty(lblProjectNo.Text))
                        dr["PROJECT_NO"] = lblProjectNo.Text;
                    else
                        dr["PROJECT_NO"] = string.Empty;

                    if (!string.IsNullOrEmpty(lblGroupID.Text))
                        dr["GROUP_ID"] = lblGroupID.Text;
                    else
                        dr["GROUP_ID"] = "0";

                    if (!string.IsNullOrEmpty(lblGroupName.Text))
                        dr["GROUP_NAME"] = lblGroupName.Text;
                    else
                        dr["GROUP_NAME"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtSubgroupName.Text))
                        dr["SUBGROUP_NAME"] = txtSubgroupName.Text;
                    else
                        dr["SUBGROUP_NAME"] = string.Empty;

                    dtNew.Rows.Add(dr);
                }

                DataRow drNewRow = dtNew.NewRow();
                drNewRow["SERIAL_NO"] = Convert.ToString(gvSubgroupList.Rows.Count + 1);

                if (ddlProjectNo.SelectedIndex > 0)
                    drNewRow["PROJECT_NO"] = Convert.ToString(ddlProjectNo.SelectedValue);
                else
                    drNewRow["PROJECT_NO"] = string.Empty;

                if (ddlProjectNo.SelectedIndex > 0)
                    drNewRow["GROUP_ID"] = Convert.ToString(ddlProjectNo.SelectedValue);
                else
                    drNewRow["GROUP_ID"] = "0";

                if (!string.IsNullOrEmpty(txtGroupName.Text))
                    drNewRow["GROUP_NAME"] = txtGroupName.Text;
                else
                    drNewRow["GROUP_NAME"] = string.Empty;

                drNewRow["SUBGROUP_NAME"] = string.Empty;

                dtNew.Rows.Add(drNewRow);
                dt = dtNew;
            }
            else
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("SERIAL_NO", typeof(string));
                dtTemp.Columns.Add("PROJECT_NO", typeof(string));
                dtTemp.Columns.Add("GROUP_ID", typeof(string));
                dtTemp.Columns.Add("GROUP_NAME", typeof(string));
                dtTemp.Columns.Add("SUBGROUP_NAME", typeof(string));

                DataRow drTemp = dtTemp.NewRow();

                drTemp["SERIAL_NO"] = "1";

                if (ddlProjectNo.SelectedIndex > 0)
                    drTemp["PROJECT_NO"] = Convert.ToString(ddlProjectNo.SelectedValue);
                else
                    drTemp["PROJECT_NO"] = string.Empty;

                if (ddlProjectNo.SelectedIndex > 0)
                    drTemp["GROUP_ID"] = Convert.ToString(ddlProjectNo.SelectedValue);
                else
                    drTemp["GROUP_ID"] = "0";

                if (!string.IsNullOrEmpty(txtGroupName.Text))
                    drTemp["GROUP_NAME"] = txtGroupName.Text;
                else
                    drTemp["GROUP_NAME"] = string.Empty;

                drTemp["SUBGROUP_NAME"] = string.Empty;

                dtTemp.Rows.Add(drTemp);
                dt = dtTemp;
            }

            if (dt.Rows.Count > 0)
            {
                Session["dt"] = dt;
                gvSubgroupList.DataSource = dt;
                gvSubgroupList.DataBind();

            }
            else
            {
                Session["dt"] = null;
                gvSubgroupList.DataSource = null;
                gvSubgroupList.DataBind();
            }

            lblRecords.Text = "[" + gvSubgroupList.Rows.Count + "]";

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SaveGroupAndSubgroup()
    {
        try
        {
            bool chk = false;
            int serialNo = 0;
            string projectNo = string.Empty;
            int groupID = 0;
            string groupName = string.Empty;
            string subgroupName = string.Empty;

            DataTable dt = (DataTable)Session["dt"];

            if (gvSubgroupList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvSubgroupList.Rows)
                {
                    chk = true;

                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblProjectNo = (Label)gr.FindControl("lblProjectNo");
                    Label lblGroupID = (Label)gr.FindControl("lblGroupID");
                    Label lblGroupName = (Label)gr.FindControl("lblGroupName");
                    TextBox txtSubgroupName = (TextBox)gr.FindControl("txtSubgroupName");

                    serialNo = Convert.ToInt32(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(lblProjectNo.Text))
                        projectNo = Convert.ToString(lblProjectNo.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select project no..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(lblGroupID.Text))
                        groupID = Convert.ToInt32(lblGroupID.Text);
                    else
                        groupID = 0;


                    if (!string.IsNullOrEmpty(lblGroupName.Text))
                        groupName = Convert.ToString(lblGroupName.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter group name..!");
                        return;
                    }


                    if (!string.IsNullOrEmpty(txtSubgroupName.Text))
                        subgroupName = txtSubgroupName.Text.Trim();
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter subgroup name..!");
                        return;
                    }

                    if (chk)
                    {
                        int value = objProject.AddGroupSubgroup(projectNo, groupID, groupName, subgroupName, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        if (value < 0)
                        {
                            ExceptionMessage("MR Code already exists..!");
                            return;
                        }
                        else if (value > 0)
                        {
                            RemoveRecord(serialNo);
                        }
                    }
                }
            }
            else
            {
                ddlProjectNo.Enabled = true;
                ExceptionMessage("No data found..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ddlProjectNo.Enabled = true;
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRecord(int serialNo)
    {
        try
        {
            HidePanel();

            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SERIAL_NO", typeof(string));
            dtNew.Columns.Add("PROJECT_NO", typeof(string));
            dtNew.Columns.Add("GROUP_ID", typeof(string));
            dtNew.Columns.Add("GROUP_NAME", typeof(string));
            dtNew.Columns.Add("SUBGROUP_NAME", typeof(string));


            foreach (GridViewRow gr in gvSubgroupList.Rows)
            {
                DataRow dr = dtNew.NewRow();

                Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                Label lblProjectNo = (Label)gr.FindControl("lblProjectNo");
                Label lblGroupID = (Label)gr.FindControl("lblGroupID");
                Label lblGroupName = (Label)gr.FindControl("lblGroupName");
                TextBox txtSubgroupName = (TextBox)gr.FindControl("txtSubgroupName");


                if (!string.IsNullOrEmpty(lblSerialNo.Text))
                    dr["SERIAL_NO"] = lblSerialNo.Text;
                else
                    dr["SERIAL_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblProjectNo.Text))
                    dr["PROJECT_NO"] = lblProjectNo.Text;
                else
                    dr["PROJECT_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblGroupID.Text))
                    dr["GROUP_ID"] = lblGroupID.Text;
                else
                    dr["GROUP_ID"] = "0";

                if (!string.IsNullOrEmpty(lblGroupName.Text))
                    dr["GROUP_NAME"] = lblGroupName.Text;
                else
                    dr["GROUP_NAME"] = string.Empty;

                if (!string.IsNullOrEmpty(txtSubgroupName.Text))
                    dr["SUBGROUP_NAME"] = txtSubgroupName.Text;
                else
                    dr["SUBGROUP_NAME"] = string.Empty;
                
                dtNew.Rows.Add(dr);
            }


            if (dtNew.Rows.Count > 0)
            {
                foreach (DataRow drremove in dtNew.Select("SERIAL_NO='" + serialNo + "'"))
                {
                    dtNew.Rows.Remove(drremove);
                }

                if (dtNew.Rows.Count > 0)
                {
                    Session["dt"] = dtNew;
                    gvSubgroupList.DataSource = dtNew;
                    gvSubgroupList.DataBind();
                }
                else
                {
                    Session["dt"] = null;
                    gvSubgroupList.DataSource = null;
                    gvSubgroupList.DataBind();
                }
            }
            else
            {
                Session["dt"] = null;
            }
            lblRecords.Text = "Records[" + dtNew.Rows.Count + "]";
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