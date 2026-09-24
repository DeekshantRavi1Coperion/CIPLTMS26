using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PROJECT_DMS_AddDesign : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DMSSendMail objDMSSendMail = new DMSSendMail();
    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsUnit = new DataSet();
    DataSet dtDesignDetailsList = new DataSet();
    DataSet dsDesignStatusSearch = new DataSet();
    DataSet dsEmp = new DataSet();
    DataSet dsDesignCategory = new DataSet();
    DataSet dsDesignJobNo = new DataSet();
    DataSet dsProjectJobNo = new DataSet();
    DataSet dsCategory = new DataSet();

    //string jobNo = string.Empty;
    //int categoryID = 0;
    //string isPlanned = string.Empty;
    //string drawingNo = string.Empty;
    //int postingStatusID = 0;
    //int datetTypeID = 0;
    //string dateSign = string.Empty;
    //string fromDate = string.Empty;
    //string toDate = string.Empty;
    //int responsibleEnggID = 0;
    //int createdByID = 0;


    int statusID = 0;
    int drawingID = 0;
    string JOBNo = string.Empty;
    int JOBUnitID = 0;
    string description = string.Empty;
    string UOM = string.Empty;
    int quantity = 0;
    string reqdDateByProjectTeam = string.Empty;
    int categoryID = 0;
    string plannedStartDateByDesignTeam = string.Empty;
    string plannedCompletionDateByDesignTeam = string.Empty;
    string drawingNo = string.Empty;
    string clientDrawingNo = string.Empty;
    string contractorDrawingNo = string.Empty;
    string documentLink = string.Empty;
    int drawingRevNo = 0;
    string workingStatus = string.Empty;
    string expectedCompletionDate = string.Empty;
    int responsibleDesignEngineerID = 0;
    int isDesignEnggFlag = 0;
    string remarks = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtTemp"] = null;

                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                    hdDesignEnggFlag.Value = "1";
                else
                    hdDesignEnggFlag.Value = "0";

                //BindCompany();

                //DateTime now = DateTime.Now;
                //var startDate = new DateTime(now.Year, now.Month, 1);
                //hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                //txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                //var endDate = startDate.AddMonths(1).AddDays(-1);
                //hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                //txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                //BindDesignStatusSearch();
                //BindDesignCategoryMainSearch();
                //BindRespinsibleEngSearch();


                //if (Request.QueryString["drawingno"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["drawingno"])))
                //{
                //    txtDrawingNoMainSearch.Text = Convert.ToString(Request.QueryString["drawingno"]);
                //}

                //GetDesignDetailList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        BindNewRow();
    }

    protected void btnRemoveRow_Click(object sender, EventArgs e)
    {
        int count = 0;
        string srNOs = string.Empty;
        if (gvDesignDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvDesignDetails.Rows)
            {
                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;

                if (chkSelect.Checked)
                {
                    count++;
                    srNOs += txtSrNo.Text + ",";
                }
            }

            if (!string.IsNullOrEmpty(srNOs))
                srNOs = srNOs.TrimEnd(',');

            if (!string.IsNullOrEmpty(srNOs))
                RemoveRow(srNOs);

            if (count == 0)
            {
                ExceptionMessage("Please select atleaset 1 row...!!!");
                return;
            }
        }
        else
        {
            ExceptionMessage("No data found...!!!");
            return;
        }
    }

    protected void gvDesignDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "GET_JOB_NO" ||
                    Convert.ToString(e.CommandArgument) == "GET_DRAWING_NO" ||
                    Convert.ToString(e.CommandArgument) == "SAVE_DESIGN")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                TextBox txtJOBNo = gvDesignDetails.Rows[rowindex].FindControl("txtJOBNo") as TextBox;
                TextBox txtSrNo = gvDesignDetails.Rows[rowindex].FindControl("txtSrNo") as TextBox;
                ViewState["SR_NO"] = Convert.ToInt32(txtSrNo.Text);

                if (Convert.ToString(e.CommandArgument) == "GET_JOB_NO")
                {
                    BindCompany();
                    mpeJOBDetail.Show();

                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                    {
                        GetDesignJOBData();
                    }
                    else
                    {
                        GetProjectJOBData();
                    }
                }

                if (Convert.ToString(e.CommandArgument) == "GET_DRAWING_NO")
                {
                    mpeDrawingDetail.Show();
                    GetDrawingDetail(txtJOBNo.Text.Trim());
                }

                if (Convert.ToString(e.CommandArgument) == "SAVE_DESIGN")
                {
                    //SaveSingleDesign(Convert.ToInt32(txtSrNo.Text));
                    GetRecordsToSave(Convert.ToInt32(txtSrNo.Text));
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

    protected void gvDesignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                {
                    e.Row.Cells[5].Visible = false;

                    e.Row.Cells[11].Visible = true;
                    e.Row.Cells[12].Visible = true;
                    //e.Row.Cells[14].Visible = true;
                    e.Row.Cells[16].Visible = true;
                    e.Row.Cells[17].Visible = true;
                    e.Row.Cells[18].Visible = true;
                }
                else
                {
                    e.Row.Cells[5].Visible = true;

                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    //e.Row.Cells[14].Visible = false;
                    e.Row.Cells[16].Visible = false;
                    e.Row.Cells[17].Visible = false;
                    e.Row.Cells[18].Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtSrNo = e.Row.FindControl("txtSrNo") as TextBox;
                Button btnGetDrawing = e.Row.FindControl("btnGetDrawing") as Button;
                TextBox txtJOBNo = e.Row.FindControl("txtJOBNo") as TextBox;
                TextBox txtDescription = e.Row.FindControl("txtDescription") as TextBox;

                TextBox txtUOM = e.Row.FindControl("txtUOM") as TextBox;
                TextBox txtQuantity = e.Row.FindControl("txtQuantity") as TextBox;

                TextBox txtReqdDateByProjectTeam = e.Row.FindControl("txtReqdDateByProjectTeam") as TextBox;
                ImageButton imgbtnReqdDateByProjectTeam = e.Row.FindControl("imgbtnReqdDateByProjectTeam") as ImageButton;

                DropDownList ddlCategory = e.Row.FindControl("ddlCategory") as DropDownList;

                TextBox txtPlannedStartDateByDesignTeam = e.Row.FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
                ImageButton imgbtnPlannedStartDateByDesignTeam = e.Row.FindControl("imgbtnPlannedStartDateByDesignTeam") as ImageButton;

                TextBox txtPlannedCompletionDateByDesignTeam = e.Row.FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
                ImageButton imgbtnPlannedCompletionDateByDesignTeam = e.Row.FindControl("imgbtnPlannedCompletionDateByDesignTeam") as ImageButton;

                TextBox txtDrawingNo = e.Row.FindControl("txtDrawingNo") as TextBox;
                TextBox txtDocumentLink = e.Row.FindControl("txtDocumentLink") as TextBox;
                DropDownList ddlDrawingRevNo = e.Row.FindControl("ddlDrawingRevNo") as DropDownList;
                TextBox txtWorkingStatus = e.Row.FindControl("txtWorkingStatus") as TextBox;

                TextBox txtExpectedCompletionDate = e.Row.FindControl("txtExpectedCompletionDate") as TextBox;
                ImageButton imgbtnExpectedCompletionDate = e.Row.FindControl("imgbtnExpectedCompletionDate") as ImageButton;

                DropDownList ddlResponsibleDesignEngineer = e.Row.FindControl("ddlResponsibleDesignEngineer") as DropDownList;


                TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;

                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                {
                    e.Row.Cells[5].Visible = false;

                    e.Row.Cells[11].Visible = true;
                    e.Row.Cells[12].Visible = true;
                    //e.Row.Cells[14].Visible = true;
                    e.Row.Cells[16].Visible = true;
                    e.Row.Cells[17].Visible = true;
                    e.Row.Cells[18].Visible = true;
                }
                else
                {
                    e.Row.Cells[5].Visible = true;

                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    //e.Row.Cells[14].Visible = false;
                    e.Row.Cells[16].Visible = false;
                    e.Row.Cells[17].Visible = false;
                    e.Row.Cells[18].Visible = false;
                }


                DataTable dt = new DataTable();
                if (Session["dtTemp"] != null)
                    dt = (DataTable)Session["dtTemp"];


                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                Label lblStatusID = e.Row.FindControl("lblStatusID") as Label;

                imgStatus.Enabled = false;

                if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/New05.png";
                    imgStatus.ToolTip = "Generated: Created by project/any user";
                }

                else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/open1.png";
                    imgStatus.ToolTip = "Open: Created by design engineer";
                }

                dsEmp = objProject.GetDesignResponsibleEngg();
                if (dsEmp.Tables.Count > 0 && dsEmp.Tables[0].Rows.Count > 0)
                {
                    Session["dtEmp"] = dsEmp.Tables[0];
                    ddlResponsibleDesignEngineer.DataSource = dsEmp.Tables[0];
                    ddlResponsibleDesignEngineer.DataTextField = "DESIGN_RESPONSIBLE_ENGG";
                    ddlResponsibleDesignEngineer.DataValueField = "DESIGN_RESPONSIBLE_ENGG_ID";
                    ddlResponsibleDesignEngineer.DataBind();
                    ddlResponsibleDesignEngineer.Items.Insert(0, "Select");

                    int count = 0;

                    if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
                        Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                        Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
                    {
                        ddlResponsibleDesignEngineer.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlResponsibleDesignEngineer.SelectedValue = Convert.ToString(Session["DESIGN_RESPONSIBLE_ENGG_ID"]);

                        foreach (DataRow dr in dsEmp.Tables[0].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                        {
                            count++;
                            ddlResponsibleDesignEngineer.SelectedValue = Convert.ToString(Session["DESIGN_RESPONSIBLE_ENGG_ID"]);
                        }
                    }

                    if (count == 0)
                    {
                        ddlResponsibleDesignEngineer.SelectedIndex = 0;
                    }
                }
                else
                {
                    Session["dtEmp"] = null;
                }


                dsCategory = objProject.GetDesignCategoryList();
                if (dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.DataSource = dsCategory.Tables[0];
                    ddlCategory.DataTextField = "DESIGN_CATEGORY";
                    ddlCategory.DataValueField = "DESIGN_CATEGORY_ID";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, "Select");
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Select("SR_NO='" + txtSrNo.Text + "'"))
                    {
                        ddlDrawingRevNo.SelectedValue = Convert.ToString(dr["DRAWING_REV_NO"]);
                        ddlResponsibleDesignEngineer.SelectedValue = Convert.ToString(dr["DESIGN_RESPONSIBLE_ENGG_ID"]);
                        ddlCategory.SelectedValue = Convert.ToString(dr["DESIGN_CATEGORY_ID"]);
                    }
                }


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


    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
        {
            mpeJOBDetail.Show();
            GetDesignJOBData();
        }
        else
        {
            mpeJOBDetail.Show();
            GetProjectJOBData();
        }
    }

    protected void gvJOBDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblJOBUnitIDInJOBList = gvJOBDetail.Rows[rowindex].FindControl("lblJOBUnitIDInJOBList") as Label;
                //txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();

                foreach (GridViewRow gr in gvDesignDetails.Rows)
                {
                    TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;
                    Label lblJOBUnitID = gr.FindControl("lblJOBUnitID") as Label;
                    TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                    if (Convert.ToInt32(txtSrNo.Text) == Convert.ToInt32(ViewState["SR_NO"]))
                    {
                        lblJOBUnitID.Text = lblJOBUnitIDInJOBList.Text;
                        txtJOBNo.Text = lblJOBNo.Text;
                        break;
                    }
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

    protected void gvDrawingDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblDrawingNo = gvDrawingDetail.Rows[rowindex].FindControl("lblDrawingNo") as Label;
                Label lblDrawingID = gvDrawingDetail.Rows[rowindex].FindControl("lblDrawingID") as Label;
                Label lblDescription = gvDrawingDetail.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblQuantity = gvDrawingDetail.Rows[rowindex].FindControl("lblQuantity") as Label;
                Label lblUOM = gvDrawingDetail.Rows[rowindex].FindControl("lblUOM") as Label;
                Label lblReqdDateByProjectTeam = gvDrawingDetail.Rows[rowindex].FindControl("lblReqdDateByProjectTeam") as Label;

                foreach (GridViewRow gr in gvDesignDetails.Rows)
                {
                    Label lblDrawingIDInList = gr.FindControl("lblDrawingIDInList") as Label;
                    TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;
                    TextBox txtDrawingNo = gr.FindControl("txtDrawingNo") as TextBox;
                    TextBox txtDescription = gr.FindControl("txtDescription") as TextBox;
                    TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                    TextBox txtUOM = gr.FindControl("txtUOM") as TextBox;
                    TextBox txtReqdDateByProjectTeam = gr.FindControl("txtReqdDateByProjectTeam") as TextBox;

                    if (Convert.ToInt32(txtSrNo.Text) == Convert.ToInt32(ViewState["SR_NO"]))
                    {
                        lblDrawingIDInList.Text = lblDrawingID.Text;
                        txtDrawingNo.Text = lblDrawingNo.Text;
                        txtDescription.Text = lblDescription.Text;
                        txtQuantity.Text = lblQuantity.Text;
                        txtUOM.Text = lblUOM.Text;
                        txtReqdDateByProjectTeam.Text = lblReqdDateByProjectTeam.Text;

                        break;
                    }
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


    protected void btnSaveDesign_Click(object sender, EventArgs e)
    {
        GetRecordsToSave(0);
    }

    protected void btnDesignList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/DesignList.aspx");
    }



    #endregion


    #region METHODS[=========================]

    private void BindCompany()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompanySearch.DataSource = dsUnit.Tables[0];
                ddlCompanySearch.DataTextField = "UNIT_NAME";
                ddlCompanySearch.DataValueField = "UNIT_ID";
                ddlCompanySearch.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindNewRow()
    {
        try
        {
            DataTable dtTemp = new DataTable();

            if (Session["dtTemp"] != null)
            {
                dtTemp = (DataTable)Session["dtTemp"];

                if (gvDesignDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gr in gvDesignDetails.Rows)
                    {
                        TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;
                        Label lblStatusID = gr.FindControl("lblStatusID") as Label;
                        TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                        Label lblJOBUnitID = gr.FindControl("lblJOBUnitID") as Label;
                        TextBox txtDescription = gr.FindControl("txtDescription") as TextBox;
                        TextBox txtUOM = gr.FindControl("txtUOM") as TextBox;
                        TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                        TextBox txtReqdDateByProjectTeam = gr.FindControl("txtReqdDateByProjectTeam") as TextBox;
                        DropDownList ddlCategory = gr.FindControl("ddlCategory") as DropDownList;
                        TextBox txtPlannedStartDateByDesignTeam = gr.FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
                        TextBox txtPlannedCompletionDateByDesignTeam = gr.FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
                        TextBox txtDrawingNo = gr.FindControl("txtDrawingNo") as TextBox;

                        TextBox txtClientDrawingNo = gr.FindControl("txtClientDrawingNo") as TextBox;
                        TextBox txtContractorDrawingNo = gr.FindControl("txtContractorDrawingNo") as TextBox;

                        TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
                        DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
                        TextBox txtWorkingStatus = gr.FindControl("txtWorkingStatus") as TextBox;
                        TextBox txtExpectedCompletionDate = gr.FindControl("txtExpectedCompletionDate") as TextBox;
                        DropDownList ddlResponsibleDesignEngineer = gr.FindControl("ddlResponsibleDesignEngineer") as DropDownList;

                        TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                        if (dtTemp.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTemp.Select("SR_NO='" + Convert.ToInt32(txtSrNo.Text) + "'"))
                            {
                                dr["SR_NO"] = txtSrNo.Text;
                                dr["STATUS_ID"] = lblStatusID.Text;
                                dr["JOB_NO"] = txtJOBNo.Text;
                                dr["JOB_UNIT_ID"] = Convert.ToInt32(lblJOBUnitID.Text);
                                dr["DESCRIPTION"] = txtDescription.Text;
                                dr["UOM"] = txtUOM.Text;
                                dr["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
                                dr["REQD_DATE_BY_PROJECT_TEAM"] = txtReqdDateByProjectTeam.Text;

                                if (ddlCategory.SelectedIndex > 0)
                                    dr["DESIGN_CATEGORY_ID"] = Convert.ToInt32(ddlCategory.SelectedValue);
                                else dr["DESIGN_CATEGORY_ID"] = 0;

                                dr["PLANNED_START_DATE_BY_DESIGN_TEAM"] = txtPlannedStartDateByDesignTeam.Text;
                                dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = txtPlannedCompletionDateByDesignTeam.Text;
                                dr["DRAWING_NO"] = txtDrawingNo.Text;

                                dr["CLIENT_DRAWING_NO"] = txtClientDrawingNo.Text;
                                dr["CONTRACTOR_DRAWING_NO"] = txtContractorDrawingNo.Text;

                                dr["DOCUMENT_LINK"] = txtDocumentLink.Text;
                                dr["DRAWING_REV_NO"] = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);
                                dr["WORKING_STATUS"] = txtWorkingStatus.Text;
                                dr["EXPECTED_COMPLETION_DATE"] = txtExpectedCompletionDate.Text;

                                if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
                                    dr["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue);
                                else dr["DESIGN_RESPONSIBLE_ENGG_ID"] = 0;

                                dr["REMARKS"] = txtRemarks.Text;
                                dr["CREATED_BY_ID"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                            }
                        }
                    }
                }
            }
            else
            {
                dtTemp.Columns.Add("SR_NO", typeof(int));
                dtTemp.Columns.Add("STATUS_ID", typeof(int));
                dtTemp.Columns.Add("JOB_NO", typeof(string));
                dtTemp.Columns.Add("JOB_UNIT_ID", typeof(int));
                dtTemp.Columns.Add("DESCRIPTION", typeof(string));
                dtTemp.Columns.Add("UOM", typeof(string));
                dtTemp.Columns.Add("QUANTITY", typeof(int));
                dtTemp.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
                dtTemp.Columns.Add("DESIGN_CATEGORY_ID", typeof(int));
                dtTemp.Columns.Add("PLANNED_START_DATE_BY_DESIGN_TEAM", typeof(string));
                dtTemp.Columns.Add("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM", typeof(string));
                dtTemp.Columns.Add("DRAWING_NO", typeof(string));

                dtTemp.Columns.Add("CLIENT_DRAWING_NO", typeof(string));
                dtTemp.Columns.Add("CONTRACTOR_DRAWING_NO", typeof(string));

                dtTemp.Columns.Add("DOCUMENT_LINK", typeof(string));
                dtTemp.Columns.Add("DRAWING_REV_NO", typeof(int));
                dtTemp.Columns.Add("WORKING_STATUS", typeof(string));
                dtTemp.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
                dtTemp.Columns.Add("DESIGN_RESPONSIBLE_ENGG_ID", typeof(int));
                dtTemp.Columns.Add("REMARKS", typeof(string));
                dtTemp.Columns.Add("CREATED_BY_ID", typeof(int));
            }

            DataRow drTemp = dtTemp.NewRow();

            drTemp["SR_NO"] = Convert.ToInt32(gvDesignDetails.Rows.Count + 1);

            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                drTemp["STATUS_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);
            else
                drTemp["STATUS_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);

            drTemp["JOB_NO"] = string.Empty;
            drTemp["JOB_UNIT_ID"] = 0;
            drTemp["DESCRIPTION"] = string.Empty;
            drTemp["UOM"] = string.Empty;
            drTemp["QUANTITY"] = 0;
            drTemp["REQD_DATE_BY_PROJECT_TEAM"] = DateTime.Now.ToString("dd-MMM-yyyy");
            drTemp["DESIGN_CATEGORY_ID"] = 0;

            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                drTemp["PLANNED_START_DATE_BY_DESIGN_TEAM"] = DateTime.Now.ToString("dd-MMM-yyyy");
            else
                drTemp["PLANNED_START_DATE_BY_DESIGN_TEAM"] = string.Empty;

            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                drTemp["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = DateTime.Now.ToString("dd-MMM-yyyy");
            else
                drTemp["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = string.Empty;

            drTemp["DRAWING_NO"] = string.Empty;

            drTemp["CLIENT_DRAWING_NO"] = string.Empty;
            drTemp["CONTRACTOR_DRAWING_NO"] = string.Empty;

            drTemp["DOCUMENT_LINK"] = string.Empty;
            drTemp["DRAWING_REV_NO"] = 0;
            drTemp["WORKING_STATUS"] = string.Empty;

            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                drTemp["EXPECTED_COMPLETION_DATE"] = DateTime.Now.ToString("dd-MMM-yyyy");
            else
                drTemp["EXPECTED_COMPLETION_DATE"] = string.Empty;

            drTemp["REMARKS"] = string.Empty;
            drTemp["DESIGN_RESPONSIBLE_ENGG_ID"] = 0;
            drTemp["CREATED_BY_ID"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dtTemp.Rows.Add(drTemp);

            if (dtTemp.Rows.Count > 0)
            {
                Session["dtTemp"] = dtTemp;
                gvDesignDetails.DataSource = dtTemp.DefaultView;
                gvDesignDetails.DataBind();
            }
            else
            {
                Session["dtTemp"] = null;
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
            }
            lblRecords.Text = "Records[" + gvDesignDetails.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRow(string srNoForRemoval)
    {
        try
        {
            int count = 0;
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["dtTemp"];
            string[] strNoForRemoval = srNoForRemoval.Split(',');
            foreach (string item in strNoForRemoval)
            {
                foreach (DataRow dr in dt1.Select("SR_NO='" + item + "'"))
                {
                    dt1.Rows.Remove(dr);
                }
            }
            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr in dt1.Rows)
                {
                    count++;
                    dr["SR_NO"] = count;
                }

                gvDesignDetails.DataSource = dt1;
                gvDesignDetails.DataBind();
            }
            else
            {
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
            }
            lblRecords.Text = "Records[" + gvDesignDetails.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRowNew(string srNoForRemoval)
    {
        try
        {
            int count = 0;
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["dtTemp"];

            if (gvDesignDetails.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvDesignDetails.Rows)
                {
                    TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;
                    Label lblStatusID = gr.FindControl("lblStatusID") as Label;
                    TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                    TextBox txtDescription = gr.FindControl("txtDescription") as TextBox;
                    TextBox txtUOM = gr.FindControl("txtUOM") as TextBox;
                    TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                    TextBox txtReqdDateByProjectTeam = gr.FindControl("txtReqdDateByProjectTeam") as TextBox;
                    TextBox txtPlannedStartDateByDesignTeam = gr.FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
                    TextBox txtPlannedCompletionDateByDesignTeam = gr.FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
                    TextBox txtDrawingNo = gr.FindControl("txtDrawingNo") as TextBox;

                    TextBox txtClientDrawingNo = gr.FindControl("txtClientDrawingNo") as TextBox;
                    TextBox txtContractorDrawingNo = gr.FindControl("txtContractorDrawingNo") as TextBox;

                    TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
                    DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
                    TextBox txtWorkingStatus = gr.FindControl("txtWorkingStatus") as TextBox;
                    TextBox txtExpectedCompletionDate = gr.FindControl("txtExpectedCompletionDate") as TextBox;
                    DropDownList ddlResponsibleDesignEngineer = gr.FindControl("ddlResponsibleDesignEngineer") as DropDownList;
                    TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt1.Select("SR_NO='" + Convert.ToInt32(txtSrNo.Text) + "'"))
                        {
                            dr["SR_NO"] = txtSrNo.Text;
                            dr["STATUS_ID"] = lblStatusID.Text;
                            dr["JOB_NO"] = txtJOBNo.Text;
                            dr["DESCRIPTION"] = txtDescription.Text;
                            dr["UOM"] = txtUOM.Text;
                            dr["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
                            dr["REQD_DATE_BY_PROJECT_TEAM"] = txtReqdDateByProjectTeam.Text;
                            dr["PLANNED_START_DATE_BY_DESIGN_TEAM"] = txtPlannedStartDateByDesignTeam.Text;
                            dr["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = txtPlannedCompletionDateByDesignTeam.Text;
                            dr["DRAWING_NO"] = txtDrawingNo.Text;

                            dr["CLIENT_DRAWING_NO"] = txtClientDrawingNo.Text;
                            dr["CONTRACTOR_DRAWING_NO"] = txtContractorDrawingNo.Text;

                            dr["DOCUMENT_LINK"] = txtDocumentLink.Text;
                            dr["DRAWING_REV_NO"] = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);
                            dr["WORKING_STATUS"] = txtWorkingStatus.Text;
                            dr["EXPECTED_COMPLETION_DATE"] = txtExpectedCompletionDate.Text;

                            if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
                                dr["DESIGN_RESPONSIBLE_ENGG_ID"] = Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue);
                            else dr["DESIGN_RESPONSIBLE_ENGG_ID"] = 0;

                            dr["REMARKS"] = txtRemarks.Text;
                            dr["CREATED_BY_ID"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                        }
                    }
                }
            }




            string[] strNoForRemoval = srNoForRemoval.Split(',');
            foreach (string item in strNoForRemoval)
            {
                foreach (DataRow dr in dt1.Select("SR_NO='" + item + "'"))
                {
                    dt1.Rows.Remove(dr);
                }
            }
            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr in dt1.Rows)
                {
                    count++;
                    dr["SR_NO"] = count;
                }

                gvDesignDetails.DataSource = dt1;
                gvDesignDetails.DataBind();
            }
            else
            {
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
            }
            lblRecords.Text = "Records[" + gvDesignDetails.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetDesignJOBData()
    {
        try
        {
            int jobUnitID = 0;
            string jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;

            jobUnitID = Convert.ToInt32(ddlCompanySearch.SelectedValue);

            dsDesignJobNo = objProject.GetJOBDetailsForDesignDrawings(jobUnitID, jobNo);

            if (dsDesignJobNo.Tables.Count > 0 && dsDesignJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsDesignJobNo.Tables[0];
                gvJOBDetail.DataBind();
            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvJOBDetail.DataSource = null;
                gvJOBDetail.DataBind();
            }
            lblJOBRecords.Text = "Records[" + gvJOBDetail.Rows.Count + "]";

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetProjectJOBData()
    {
        try
        {
            int jobUnitID = 0;
            string jobNo = string.Empty;

            //jobUnitID = Convert.ToInt32(ddlCompanySearch.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;

            dsProjectJobNo = objProject.GetJOBDetailsForProjectDrawings(jobUnitID, jobNo);
            if (dsProjectJobNo.Tables.Count > 0 && dsProjectJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsProjectJobNo.Tables[0];
                gvJOBDetail.DataBind();

                if (dsProjectJobNo.Tables[1].Rows.Count > 0)
                {
                    Session["dtDrawingDetail"] = dsProjectJobNo.Tables[1];
                }
                else
                {
                    Session["dtDrawingDetail"] = null;
                }
            }
            else
            {
                Session["dtDrawingDetail"] = null;
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvJOBDetail.DataSource = null;
                gvJOBDetail.DataBind();
            }
            lblJOBRecords.Text = "Records[" + gvJOBDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetDrawingDetail(string jobNo)
    {
        try
        {
            DataTable dtDrawings = new DataTable();
            DataTable dt = new DataTable();

            if (Session["dtDrawingDetail"] != null)
                dt = (DataTable)Session["dtDrawingDetail"];


            dtDrawings.Columns.Add("DRAWING_ID", typeof(int));
            dtDrawings.Columns.Add("DRAWING_NO", typeof(string));
            dtDrawings.Columns.Add("DESCRIPTION", typeof(string));
            dtDrawings.Columns.Add("QUANTITY", typeof(string));
            dtDrawings.Columns.Add("UOM", typeof(string));
            dtDrawings.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Select("JOB_NO='" + jobNo + "'"))
                {
                    DataRow drd = dtDrawings.NewRow();

                    if (dr["DRAWING_ID"] != DBNull.Value)
                        drd["DRAWING_ID"] = dr["DRAWING_ID"];

                    if (dr["DRAWING_NO"] != DBNull.Value)
                        drd["DRAWING_NO"] = dr["DRAWING_NO"];

                    if (dr["DESCRIPTION"] != DBNull.Value)
                        drd["DESCRIPTION"] = dr["DESCRIPTION"];

                    if (dr["QUANTITY"] != DBNull.Value)
                        drd["QUANTITY"] = dr["QUANTITY"];

                    if (dr["UOM"] != DBNull.Value)
                        drd["UOM"] = dr["UOM"];

                    if (dr["REQD_DATE_BY_PROJECT_TEAM"] != DBNull.Value)
                        drd["REQD_DATE_BY_PROJECT_TEAM"] = dr["REQD_DATE_BY_PROJECT_TEAM"];

                    dtDrawings.Rows.Add(drd);
                }
            }
            else
            {
                GetProjectJOBData();
            }


            if (dtDrawings.Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvDrawingDetail.DataSource = dtDrawings;
                gvDrawingDetail.DataBind();
            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvDrawingDetail.DataSource = null;
                gvDrawingDetail.DataBind();
            }
            lblDrawingRecords.Text = "Records[" + gvDrawingDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }



    private void GetRecordsToSave(int srNo)
    {
        try
        {

            DataTable dtTempForProject = new DataTable();
            dtTempForProject.Columns.Add("STATUS_ID", typeof(int));
            dtTempForProject.Columns.Add("DRAWING_ID", typeof(int));
            dtTempForProject.Columns.Add("JOB_NO", typeof(string));
            dtTempForProject.Columns.Add("JOB_UNIT_ID", typeof(int));
            dtTempForProject.Columns.Add("DESCRIPTION", typeof(string));
            dtTempForProject.Columns.Add("UOM", typeof(string));
            dtTempForProject.Columns.Add("QUANTITY", typeof(int));
            dtTempForProject.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
            dtTempForProject.Columns.Add("CATEGORY_ID", typeof(int));
            dtTempForProject.Columns.Add("DOCUMENT_LINK", typeof(string));
            dtTempForProject.Columns.Add("DRAWING_NO", typeof(string));

            dtTempForProject.Columns.Add("CLIENT_DRAWING_NO", typeof(string));
            dtTempForProject.Columns.Add("CONTRACTOR_DRAWING_NO", typeof(string));

            dtTempForProject.Columns.Add("DRAWING_REV_NO", typeof(int));
            dtTempForProject.Columns.Add("REMARKS", typeof(string));


            DataTable dtTempForDesign = new DataTable();
            dtTempForDesign.Columns.Add("STATUS_ID", typeof(int));
            dtTempForDesign.Columns.Add("JOB_NO", typeof(string));
            dtTempForDesign.Columns.Add("JOB_UNIT_ID", typeof(int));
            dtTempForDesign.Columns.Add("DESCRIPTION", typeof(string));
            dtTempForDesign.Columns.Add("UOM", typeof(string));
            dtTempForDesign.Columns.Add("QUANTITY", typeof(int));
            dtTempForDesign.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
            dtTempForDesign.Columns.Add("CATEGORY_ID", typeof(int));
            dtTempForDesign.Columns.Add("PLANNED_START_DATE_BY_DESIGN_TEAM", typeof(string));
            dtTempForDesign.Columns.Add("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM", typeof(string));
            dtTempForDesign.Columns.Add("DRAWING_NO", typeof(string));

            dtTempForDesign.Columns.Add("CLIENT_DRAWING_NO", typeof(string));
            dtTempForDesign.Columns.Add("CONTRACTOR_DRAWING_NO", typeof(string));

            dtTempForDesign.Columns.Add("DOCUMENT_LINK", typeof(string));
            dtTempForDesign.Columns.Add("DRAWING_REV_NO", typeof(int));
            dtTempForDesign.Columns.Add("WORKING_STATUS", typeof(string));
            dtTempForDesign.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
            dtTempForDesign.Columns.Add("DESIGN_RESPONSIBLE_ENGG_ID", typeof(int));
            dtTempForDesign.Columns.Add("REMARKS", typeof(string));
            dtTempForDesign.Columns.Add("OPEN_REMARKS", typeof(string));



            string srNOs = string.Empty;
            int count = 0;
            int savedCount = 0;
            int mailTypeID = 0;
            bool check = true;
            int ducplicateCounts = 0;

            statusID = 0;
            JOBNo = string.Empty;
            JOBUnitID = 0;
            description = string.Empty;
            UOM = string.Empty;
            quantity = 0;
            reqdDateByProjectTeam = string.Empty;
            categoryID = 0;
            plannedStartDateByDesignTeam = string.Empty;
            plannedCompletionDateByDesignTeam = string.Empty;
            drawingNo = string.Empty;
            clientDrawingNo = string.Empty;
            contractorDrawingNo = string.Empty;
            documentLink = string.Empty;
            drawingRevNo = 0;
            workingStatus = string.Empty;
            expectedCompletionDate = string.Empty;
            responsibleDesignEngineerID = 0;
            remarks = string.Empty;
            isDesignEnggFlag = 0;
            check = true;

            if (gvDesignDetails.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvDesignDetails.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                    TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;

                    if (srNo > 0)
                    {
                        if (srNo == Convert.ToInt32(txtSrNo.Text))
                        {
                            check = true;
                            count++;
                            Label lblStatusID = gr.FindControl("lblStatusID") as Label;
                            Label lblDrawingIDInList = gr.FindControl("lblDrawingIDInList") as Label;
                            TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                            Label lblJOBUnitID = gr.FindControl("lblJOBUnitID") as Label;
                            TextBox txtDescription = gr.FindControl("txtDescription") as TextBox;
                            TextBox txtUOM = gr.FindControl("txtUOM") as TextBox;
                            TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                            TextBox txtReqdDateByProjectTeam = gr.FindControl("txtReqdDateByProjectTeam") as TextBox;
                            DropDownList ddlCategory = gr.FindControl("ddlCategory") as DropDownList;
                            TextBox txtPlannedStartDateByDesignTeam = gr.FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
                            TextBox txtPlannedCompletionDateByDesignTeam = gr.FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
                            TextBox txtDrawingNo = gr.FindControl("txtDrawingNo") as TextBox;

                            TextBox txtClientDrawingNo = gr.FindControl("txtClientDrawingNo") as TextBox;
                            TextBox txtContractorDrawingNo = gr.FindControl("txtContractorDrawingNo") as TextBox;

                            TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
                            DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
                            TextBox txtWorkingStatus = gr.FindControl("txtWorkingStatus") as TextBox;
                            TextBox txtExpectedCompletionDate = gr.FindControl("txtExpectedCompletionDate") as TextBox;
                            DropDownList ddlResponsibleDesignEngineer = gr.FindControl("ddlResponsibleDesignEngineer") as DropDownList;
                            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                            if (Convert.ToInt32(lblStatusID.Text) > 0)
                                statusID = Convert.ToInt32(lblStatusID.Text);
                            else statusID = 0;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingIDInList.Text)) && Convert.ToInt32(lblDrawingIDInList.Text) > 0)
                                drawingID = Convert.ToInt32(lblDrawingIDInList.Text);
                            else drawingID = 0;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNo.Text)))
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(lblJOBUnitID.Text)) && Convert.ToInt32(lblJOBUnitID.Text) > 0)
                                {
                                    JOBUnitID = Convert.ToInt32(lblJOBUnitID.Text);
                                }
                                else
                                {
                                    JOBUnitID = 1;
                                }

                                JOBNo = Convert.ToString(txtJOBNo.Text);
                                txtJOBNo.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                JOBUnitID = 0;
                                JOBNo = string.Empty;
                                check = false;
                                txtJOBNo.Focus();
                                txtJOBNo.BackColor = System.Drawing.Color.LightPink;
                            }



                            if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
                            {
                                description = Convert.ToString(txtDescription.Text);
                                txtDescription.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                description = string.Empty;
                                check = false;
                                txtDescription.Focus();
                                txtDescription.BackColor = System.Drawing.Color.LightPink;
                            }



                            if (!string.IsNullOrEmpty(Convert.ToString(txtUOM.Text)))
                            {
                                UOM = Convert.ToString(txtUOM.Text);
                                txtUOM.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                UOM = string.Empty;
                                check = false;
                                txtUOM.Focus();
                                txtUOM.BackColor = System.Drawing.Color.LightPink;
                            }



                            if (Convert.ToInt32(txtQuantity.Text) > 0)
                            {
                                quantity = Convert.ToInt32(txtQuantity.Text);
                                txtQuantity.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                quantity = 0;
                                check = false;
                                txtQuantity.Focus();
                                txtQuantity.BackColor = System.Drawing.Color.LightPink;
                            }



                            if (!string.IsNullOrEmpty(Convert.ToString(txtReqdDateByProjectTeam.Text)))
                            {
                                reqdDateByProjectTeam = Convert.ToDateTime(txtReqdDateByProjectTeam.Text).ToString("yyyy-MM-dd");
                                txtReqdDateByProjectTeam.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                reqdDateByProjectTeam = string.Empty;
                                check = false;
                                txtReqdDateByProjectTeam.Focus();
                                txtReqdDateByProjectTeam.BackColor = System.Drawing.Color.LightPink;
                            }


                            //if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
                            //{
                            if (ddlCategory.SelectedIndex > 0)
                            {
                                categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                                ddlCategory.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                categoryID = 0;
                                check = false;
                                ddlCategory.Focus();
                                ddlCategory.BackColor = System.Drawing.Color.LightPink;
                            }
                            //}


                            if (!string.IsNullOrEmpty(Convert.ToString(txtDrawingNo.Text)))
                            {
                                drawingNo = Convert.ToString(txtDrawingNo.Text);
                                txtDrawingNo.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                drawingNo = string.Empty;
                                check = false;
                                txtDrawingNo.Focus();
                                txtDrawingNo.BackColor = System.Drawing.Color.LightPink;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(txtClientDrawingNo.Text)))
                                clientDrawingNo = txtClientDrawingNo.Text;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtContractorDrawingNo.Text)))
                                contractorDrawingNo = txtContractorDrawingNo.Text;



                            if (!string.IsNullOrEmpty(Convert.ToString(txtDocumentLink.Text)))
                            {
                                documentLink = Convert.ToString(txtDocumentLink.Text);
                                txtDocumentLink.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                documentLink = string.Empty;
                            }



                            if (ddlDrawingRevNo.SelectedIndex > 0)
                                drawingRevNo = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);
                            else drawingRevNo = 0;




                            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                            {
                                isDesignEnggFlag = 1;

                                if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedStartDateByDesignTeam.Text)))
                                {
                                    plannedStartDateByDesignTeam = Convert.ToDateTime(txtPlannedStartDateByDesignTeam.Text).ToString("yyyy-MM-dd");
                                    txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    plannedStartDateByDesignTeam = string.Empty;
                                    check = false;
                                    txtPlannedStartDateByDesignTeam.Focus();
                                    txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                }



                                if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text)))
                                {
                                    plannedCompletionDateByDesignTeam = Convert.ToDateTime(txtPlannedCompletionDateByDesignTeam.Text).ToString("yyyy-MM-dd");
                                    txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    plannedCompletionDateByDesignTeam = string.Empty;
                                    check = false;
                                    txtPlannedCompletionDateByDesignTeam.Focus();
                                    txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                }



                                if (Convert.ToDateTime(txtPlannedStartDateByDesignTeam.Text) > Convert.ToDateTime(txtPlannedCompletionDateByDesignTeam.Text))
                                {
                                    txtPlannedStartDateByDesignTeam.Focus();
                                    txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                    txtPlannedCompletionDateByDesignTeam.Focus();
                                    txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                    check = false;
                                }
                                else
                                {
                                    txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                    txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                }


                                if (!string.IsNullOrEmpty(Convert.ToString(txtWorkingStatus.Text)))
                                {
                                    workingStatus = Convert.ToString(txtWorkingStatus.Text);
                                    txtWorkingStatus.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    workingStatus = string.Empty;
                                    check = false;
                                    txtWorkingStatus.Focus();
                                    txtWorkingStatus.BackColor = System.Drawing.Color.LightPink;
                                }



                                if (!string.IsNullOrEmpty(Convert.ToString(txtExpectedCompletionDate.Text)))
                                {
                                    expectedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                                    txtExpectedCompletionDate.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    expectedCompletionDate = string.Empty;
                                    check = false;
                                    txtExpectedCompletionDate.Focus();
                                    txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightPink;
                                }



                                if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
                                {
                                    responsibleDesignEngineerID = Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue);
                                    ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    responsibleDesignEngineerID = 0;
                                    check = false;
                                    ddlResponsibleDesignEngineer.Focus();
                                    ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.LightPink;
                                }

                            }
                            else
                            {
                                isDesignEnggFlag = 0;
                                plannedStartDateByDesignTeam = string.Empty;
                                plannedCompletionDateByDesignTeam = string.Empty;
                                workingStatus = string.Empty;
                                expectedCompletionDate = string.Empty;
                                responsibleDesignEngineerID = 0;
                            }


                            if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                                remarks = Convert.ToString(txtRemarks.Text);
                            else remarks = string.Empty;




                            if (check == true)
                            {
                                DataSet dsDuplicacy = new DataSet();
                                dsDuplicacy = objProject.CheckDesignDrawingForDuplicacy(("'" + drawingNo + "'"));
                                if (dsDuplicacy.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(dsDuplicacy.Tables[0].Rows[0]["DRAWINGS_COUNT"]) == 0)
                                    {
                                        srNOs += txtSrNo.Text + ",";

                                        if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                        {
                                            DataRow drd = dtTempForDesign.NewRow();
                                            drd["STATUS_ID"] = statusID;
                                            drd["JOB_NO"] = JOBNo;
                                            drd["JOB_UNIT_ID"] = JOBUnitID;
                                            drd["DESCRIPTION"] = description;
                                            drd["UOM"] = UOM;
                                            drd["QUANTITY"] = quantity;
                                            drd["REQD_DATE_BY_PROJECT_TEAM"] = reqdDateByProjectTeam;
                                            drd["CATEGORY_ID"] = categoryID;
                                            drd["PLANNED_START_DATE_BY_DESIGN_TEAM"] = plannedStartDateByDesignTeam;
                                            drd["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = plannedCompletionDateByDesignTeam;
                                            drd["DRAWING_NO"] = drawingNo;

                                            drd["CLIENT_DRAWING_NO"] = clientDrawingNo;
                                            drd["CONTRACTOR_DRAWING_NO"] = contractorDrawingNo;

                                            drd["DOCUMENT_LINK"] = documentLink;
                                            drd["DRAWING_REV_NO"] = drawingRevNo;
                                            drd["WORKING_STATUS"] = workingStatus;
                                            drd["EXPECTED_COMPLETION_DATE"] = expectedCompletionDate;
                                            drd["DESIGN_RESPONSIBLE_ENGG_ID"] = responsibleDesignEngineerID;
                                            drd["REMARKS"] = remarks;
                                            drd["OPEN_REMARKS"] = remarks;
                                            dtTempForDesign.Rows.Add(drd);
                                        }
                                        else
                                        {
                                            DataRow drp = dtTempForProject.NewRow();
                                            drp["STATUS_ID"] = statusID;
                                            drp["DRAWING_ID"] = drawingID;
                                            drp["JOB_NO"] = JOBNo;
                                            drp["JOB_UNIT_ID"] = JOBUnitID;
                                            drp["DESCRIPTION"] = description;
                                            drp["UOM"] = UOM;
                                            drp["QUANTITY"] = quantity;
                                            drp["REQD_DATE_BY_PROJECT_TEAM"] = reqdDateByProjectTeam;
                                            drp["CATEGORY_ID"] = categoryID;
                                            drp["DOCUMENT_LINK"] = documentLink;
                                            drp["DRAWING_NO"] = drawingNo;

                                            drp["CLIENT_DRAWING_NO"] = clientDrawingNo;
                                            drp["CONTRACTOR_DRAWING_NO"] = contractorDrawingNo;

                                            drp["DRAWING_REV_NO"] = drawingRevNo;
                                            drp["REMARKS"] = remarks;
                                            dtTempForProject.Rows.Add(drp);
                                        }
                                    }
                                    else
                                    {
                                        ducplicateCounts++;
                                    }
                                }
                            }



                        }
                    }
                    else
                    {
                        if (chkSelect.Checked)
                        {
                            check = true;
                            count++;
                            Label lblStatusID = gr.FindControl("lblStatusID") as Label;
                            Label lblDrawingIDInList = gr.FindControl("lblDrawingIDInList") as Label;
                            TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                            Label lblJOBUnitID = gr.FindControl("lblJOBUnitID") as Label;
                            TextBox txtDescription = gr.FindControl("txtDescription") as TextBox;
                            TextBox txtUOM = gr.FindControl("txtUOM") as TextBox;
                            TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                            TextBox txtReqdDateByProjectTeam = gr.FindControl("txtReqdDateByProjectTeam") as TextBox;
                            DropDownList ddlCategory = gr.FindControl("ddlCategory") as DropDownList;
                            TextBox txtPlannedStartDateByDesignTeam = gr.FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
                            TextBox txtPlannedCompletionDateByDesignTeam = gr.FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
                            TextBox txtDrawingNo = gr.FindControl("txtDrawingNo") as TextBox;

                            TextBox txtClientDrawingNo = gr.FindControl("txtClientDrawingNo") as TextBox;
                            TextBox txtContractorDrawingNo = gr.FindControl("txtContractorDrawingNo") as TextBox;

                            TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
                            DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
                            TextBox txtWorkingStatus = gr.FindControl("txtWorkingStatus") as TextBox;
                            TextBox txtExpectedCompletionDate = gr.FindControl("txtExpectedCompletionDate") as TextBox;
                            DropDownList ddlResponsibleDesignEngineer = gr.FindControl("ddlResponsibleDesignEngineer") as DropDownList;
                            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                            if (Convert.ToInt32(lblStatusID.Text) > 0)
                                statusID = Convert.ToInt32(lblStatusID.Text);
                            else statusID = 0;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingIDInList.Text)) && Convert.ToInt32(lblDrawingIDInList.Text) > 0)
                                drawingID = Convert.ToInt32(lblDrawingIDInList.Text);
                            else drawingID = 0;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNo.Text)))
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(lblJOBUnitID.Text)) && Convert.ToInt32(lblJOBUnitID.Text) > 0)
                                {
                                    JOBUnitID = Convert.ToInt32(lblJOBUnitID.Text);
                                }
                                else
                                {
                                    JOBUnitID = 1;
                                }
                                JOBNo = Convert.ToString(txtJOBNo.Text);
                                txtJOBNo.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                JOBUnitID = 0;
                                JOBNo = string.Empty;
                                check = false;
                                txtJOBNo.Focus();
                                txtJOBNo.BackColor = System.Drawing.Color.LightPink;
                            }



                            if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
                            {
                                description = Convert.ToString(txtDescription.Text);
                                txtDescription.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                description = string.Empty;
                                check = false;
                                txtDescription.Focus();
                                txtDescription.BackColor = System.Drawing.Color.LightPink;
                            }



                            if (!string.IsNullOrEmpty(Convert.ToString(txtUOM.Text)))
                            {
                                UOM = Convert.ToString(txtUOM.Text);
                                txtUOM.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                UOM = string.Empty;
                                check = false;
                                txtUOM.Focus();
                                txtUOM.BackColor = System.Drawing.Color.LightPink;
                            }



                            if (Convert.ToInt32(txtQuantity.Text) > 0)
                            {
                                quantity = Convert.ToInt32(txtQuantity.Text);
                                txtQuantity.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                quantity = 0;
                                check = false;
                                txtQuantity.Focus();
                                txtQuantity.BackColor = System.Drawing.Color.LightPink;
                            }



                            if (!string.IsNullOrEmpty(Convert.ToString(txtReqdDateByProjectTeam.Text)))
                            {
                                reqdDateByProjectTeam = Convert.ToDateTime(txtReqdDateByProjectTeam.Text).ToString("yyyy-MM-dd");
                                txtReqdDateByProjectTeam.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                reqdDateByProjectTeam = string.Empty;
                                check = false;
                                txtReqdDateByProjectTeam.Focus();
                                txtReqdDateByProjectTeam.BackColor = System.Drawing.Color.LightPink;
                            }


                            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
                            {
                                if (ddlCategory.SelectedIndex > 0)
                                {
                                    categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                                    ddlCategory.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    categoryID = 0;
                                    check = false;
                                    ddlCategory.Focus();
                                    ddlCategory.BackColor = System.Drawing.Color.LightPink;
                                }
                            }


                            if (!string.IsNullOrEmpty(Convert.ToString(txtDrawingNo.Text)))
                            {
                                drawingNo = Convert.ToString(txtDrawingNo.Text);
                                txtDrawingNo.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                drawingNo = string.Empty;
                                check = false;
                                txtDrawingNo.Focus();
                                txtDrawingNo.BackColor = System.Drawing.Color.LightPink;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(txtClientDrawingNo.Text)))
                                clientDrawingNo = txtClientDrawingNo.Text;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtContractorDrawingNo.Text)))
                                contractorDrawingNo = txtContractorDrawingNo.Text;

                            if (!string.IsNullOrEmpty(Convert.ToString(txtDocumentLink.Text)))
                            {
                                documentLink = Convert.ToString(txtDocumentLink.Text);
                                txtDocumentLink.BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                documentLink = string.Empty;
                            }



                            if (ddlDrawingRevNo.SelectedIndex > 0)
                                drawingRevNo = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);
                            else drawingRevNo = 0;




                            if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                            {
                                isDesignEnggFlag = 1;

                                if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedStartDateByDesignTeam.Text)))
                                {
                                    plannedStartDateByDesignTeam = Convert.ToDateTime(txtPlannedStartDateByDesignTeam.Text).ToString("yyyy-MM-dd");
                                    txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    plannedStartDateByDesignTeam = string.Empty;
                                    check = false;
                                    txtPlannedStartDateByDesignTeam.Focus();
                                    txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                }



                                if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text)))
                                {
                                    plannedCompletionDateByDesignTeam = Convert.ToDateTime(txtPlannedCompletionDateByDesignTeam.Text).ToString("yyyy-MM-dd");
                                    txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    plannedCompletionDateByDesignTeam = string.Empty;
                                    check = false;
                                    txtPlannedCompletionDateByDesignTeam.Focus();
                                    txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                }


                                if (Convert.ToDateTime(txtPlannedStartDateByDesignTeam.Text) > Convert.ToDateTime(txtPlannedCompletionDateByDesignTeam.Text))
                                {
                                    txtPlannedStartDateByDesignTeam.Focus();
                                    txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                    txtPlannedCompletionDateByDesignTeam.Focus();
                                    txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
                                    check = false;
                                }
                                else
                                {
                                    txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                    txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.White;
                                }



                                if (!string.IsNullOrEmpty(Convert.ToString(txtWorkingStatus.Text)))
                                {
                                    workingStatus = Convert.ToString(txtWorkingStatus.Text);
                                    txtWorkingStatus.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    workingStatus = string.Empty;
                                    check = false;
                                    txtWorkingStatus.Focus();
                                    txtWorkingStatus.BackColor = System.Drawing.Color.LightPink;
                                }



                                if (!string.IsNullOrEmpty(Convert.ToString(txtExpectedCompletionDate.Text)))
                                {
                                    expectedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
                                    txtExpectedCompletionDate.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    expectedCompletionDate = string.Empty;
                                    check = false;
                                    txtExpectedCompletionDate.Focus();
                                    txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightPink;
                                }



                                if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
                                {
                                    responsibleDesignEngineerID = Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue);
                                    ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    responsibleDesignEngineerID = 0;
                                    check = false;
                                    ddlResponsibleDesignEngineer.Focus();
                                    ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.LightPink;
                                }

                            }
                            else
                            {
                                isDesignEnggFlag = 0;
                                plannedStartDateByDesignTeam = string.Empty;
                                plannedCompletionDateByDesignTeam = string.Empty;
                                workingStatus = string.Empty;
                                expectedCompletionDate = string.Empty;
                                responsibleDesignEngineerID = 0;
                            }


                            if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                                remarks = Convert.ToString(txtRemarks.Text);
                            else remarks = string.Empty;






                            //int value = 0;
                            if (check == true)
                            {
                                DataSet dsDuplicacy = new DataSet();
                                dsDuplicacy = objProject.CheckDesignDrawingForDuplicacy(("'" + drawingNo + "'"));
                                if (dsDuplicacy.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(dsDuplicacy.Tables[0].Rows[0]["DRAWINGS_COUNT"]) == 0)
                                    {
                                        srNOs += txtSrNo.Text + ",";

                                        if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                                        {
                                            DataRow drd = dtTempForDesign.NewRow();
                                            drd["STATUS_ID"] = statusID;
                                            drd["JOB_NO"] = JOBNo;
                                            drd["JOB_UNIT_ID"] = JOBUnitID;
                                            drd["DESCRIPTION"] = description;
                                            drd["UOM"] = UOM;
                                            drd["QUANTITY"] = quantity;
                                            drd["REQD_DATE_BY_PROJECT_TEAM"] = reqdDateByProjectTeam;
                                            drd["CATEGORY_ID"] = categoryID;
                                            drd["PLANNED_START_DATE_BY_DESIGN_TEAM"] = plannedStartDateByDesignTeam;
                                            drd["PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM"] = plannedCompletionDateByDesignTeam;
                                            drd["DRAWING_NO"] = drawingNo;

                                            drd["CLIENT_DRAWING_NO"] = clientDrawingNo;
                                            drd["CONTRACTOR_DRAWING_NO"] = contractorDrawingNo;

                                            drd["DOCUMENT_LINK"] = documentLink;
                                            drd["DRAWING_REV_NO"] = drawingRevNo;
                                            drd["WORKING_STATUS"] = workingStatus;
                                            drd["EXPECTED_COMPLETION_DATE"] = expectedCompletionDate;
                                            drd["DESIGN_RESPONSIBLE_ENGG_ID"] = responsibleDesignEngineerID;
                                            drd["REMARKS"] = remarks;
                                            drd["OPEN_REMARKS"] = remarks;
                                            dtTempForDesign.Rows.Add(drd);
                                        }
                                        else
                                        {
                                            DataRow drp = dtTempForProject.NewRow();
                                            drp["STATUS_ID"] = statusID;
                                            drp["DRAWING_ID"] = drawingID;
                                            drp["JOB_NO"] = JOBNo;
                                            drp["JOB_UNIT_ID"] = JOBUnitID;
                                            drp["DESCRIPTION"] = description;
                                            drp["UOM"] = UOM;
                                            drp["QUANTITY"] = quantity;
                                            drp["REQD_DATE_BY_PROJECT_TEAM"] = reqdDateByProjectTeam;
                                            drp["CATEGORY_ID"] = categoryID;
                                            drp["DRAWING_NO"] = drawingNo;

                                            drp["CLIENT_DRAWING_NO"] = clientDrawingNo;
                                            drp["CONTRACTOR_DRAWING_NO"] = contractorDrawingNo;

                                            drp["DRAWING_REV_NO"] = drawingRevNo;
                                            drp["REMARKS"] = remarks;
                                            dtTempForProject.Rows.Add(drp);
                                        }
                                    }
                                    else
                                    {
                                        ducplicateCounts++;
                                    }
                                }
                            }
                        }
                    }
                }

                if (ducplicateCounts > 0)
                {
                    ExceptionMessage(ducplicateCounts + " drawings already exists, please try with other drawing no...!!!");
                    return;
                }

                if (count == 0)
                {
                    ExceptionMessage("Please select atleaset 1 row...!!!");
                    return;
                }

                if (dtTempForDesign.Rows.Count > 0 || dtTempForProject.Rows.Count > 0)
                {
                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
                    {
                        isDesignEnggFlag = Convert.ToInt32(DMSAllStatusAndTypes.EnumRespEnggType.Design);
                        statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open);

                        if (responsibleDesignEngineerID == Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]))
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToHimselfMail);
                        else
                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToOtherMail);

                    }
                    else
                    {
                        isDesignEnggFlag = 0;
                        statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);
                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);
                    }



                    int value = objProject.AddDesignDetail(isDesignEnggFlag, dtTempForProject, dtTempForDesign, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (value > 0)
                    {
                        string drawingNos = string.Empty;
                        if (dtTempForProject.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTempForProject.Rows)
                            {
                                drawingNos += "'" + dr["DRAWING_NO"] + "',";
                            }
                        }
                        else if (dtTempForDesign.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTempForDesign.Rows)
                            {
                                drawingNos += "'" + dr["DRAWING_NO"] + "',";
                            }
                        }

                        if (!string.IsNullOrEmpty(drawingNos))
                            drawingNos = drawingNos.TrimEnd(',');


                        int mailSentValue = 0;
                        if (!string.IsNullOrEmpty(drawingNos))
                        {
                            mailSentValue = objDMSSendMail.ProcessAndSendMail(drawingNos, 0, statusID, mailTypeID);
                            if (mailSentValue > 0)
                            {
                                int mailSentStatusValue = objProject.UpdateDesignMailStatus(drawingNos, 0, statusID, 0, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                            }
                        }


                        if (!string.IsNullOrEmpty(srNOs))
                            srNOs = srNOs.TrimEnd(',');

                        if (!string.IsNullOrEmpty(srNOs))
                            RemoveRowNew(srNOs);

                        if (dtTempForProject.Rows.Count > 0)
                        {
                            if (mailSentValue > 0)
                            {
                                SuccessMessage(dtTempForProject.Rows.Count + " design(s) saved and mail sent successfully...!!!");
                                return;
                            }
                            else
                            {
                                SuccessMessage(dtTempForProject.Rows.Count + " design(s) saved successfully...!!!");
                                return;
                            }

                        }
                        else if (dtTempForDesign.Rows.Count > 0)
                        {
                            if (mailSentValue > 0)
                            {
                                SuccessMessage(dtTempForDesign.Rows.Count + " design(s) saved and mail sent successfully...!!!");
                                return;
                            }
                            else
                            {
                                SuccessMessage(dtTempForDesign.Rows.Count + " design(s) saved successfully...!!!");
                                return;
                            }
                        }
                    }
                    else
                    {
                        ExceptionMessage("Please try again...!!!");
                        return;
                    }
                }
            }
            else
            {
                ExceptionMessage("No data found...!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    //private void SaveSingleDesign(int srNo)
    //{
    //    try
    //    {
    //        string srNOs = string.Empty;
    //        int count = 0;
    //        int savedCount = 0;
    //        int mailTypeID = 0;
    //        bool check = true;

    //        statusID = 0;
    //        JOBNo = string.Empty;
    //        description = string.Empty;
    //        UOM = string.Empty;
    //        quantity = 0;
    //        reqdDateByProjectTeam = string.Empty;
    //        plannedStartDateByDesignTeam = string.Empty;
    //        plannedCompletionDateByDesignTeam = string.Empty;
    //        drawingNo = string.Empty;
    //        documentLink = string.Empty;
    //        drawingRevNo = 0;
    //        workingStatus = string.Empty;
    //        expectedCompletionDate = string.Empty;
    //        responsibleDesignEngineerID = 0;
    //        remarks = string.Empty;
    //        isDesignEnggFlag = 0;
    //        check = true;

    //        if (gvDesignDetails.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow gr in gvDesignDetails.Rows)
    //            {
    //                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
    //                TextBox txtSrNo = gr.FindControl("txtSrNo") as TextBox;

    //                if (Convert.ToInt32(txtSrNo.Text) == srNo)
    //                {
    //                    //if (chkSelect.Checked)
    //                    //{
    //                    check = true;
    //                    //count++;
    //                    Label lblStatusID = gr.FindControl("lblStatusID") as Label;
    //                    Label lblDrawingIDInList = gr.FindControl("lblDrawingIDInList") as Label;
    //                    TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
    //                    TextBox txtDescription = gr.FindControl("txtDescription") as TextBox;
    //                    TextBox txtUOM = gr.FindControl("txtUOM") as TextBox;
    //                    TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
    //                    TextBox txtReqdDateByProjectTeam = gr.FindControl("txtReqdDateByProjectTeam") as TextBox;
    //                    DropDownList ddlCategory = gr.FindControl("ddlCategory") as DropDownList;
    //                    TextBox txtPlannedStartDateByDesignTeam = gr.FindControl("txtPlannedStartDateByDesignTeam") as TextBox;
    //                    TextBox txtPlannedCompletionDateByDesignTeam = gr.FindControl("txtPlannedCompletionDateByDesignTeam") as TextBox;
    //                    TextBox txtDrawingNo = gr.FindControl("txtDrawingNo") as TextBox;
    //                    TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
    //                    DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
    //                    TextBox txtWorkingStatus = gr.FindControl("txtWorkingStatus") as TextBox;
    //                    TextBox txtExpectedCompletionDate = gr.FindControl("txtExpectedCompletionDate") as TextBox;
    //                    DropDownList ddlResponsibleDesignEngineer = gr.FindControl("ddlResponsibleDesignEngineer") as DropDownList;
    //                    TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

    //                    if (Convert.ToInt32(lblStatusID.Text) > 0)
    //                        statusID = Convert.ToInt32(lblStatusID.Text);
    //                    else statusID = 0;

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingIDInList.Text)) && Convert.ToInt32(lblDrawingIDInList.Text) > 0)
    //                        drawingID = Convert.ToInt32(lblDrawingIDInList.Text);
    //                    else drawingID = 0;

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNo.Text)))
    //                    {
    //                        JOBNo = Convert.ToString(txtJOBNo.Text);
    //                        txtJOBNo.BackColor = System.Drawing.Color.White;
    //                    }
    //                    else
    //                    {
    //                        JOBNo = string.Empty;
    //                        check = false;
    //                        txtJOBNo.Focus();
    //                        txtJOBNo.BackColor = System.Drawing.Color.LightPink;
    //                    }



    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
    //                    {
    //                        description = Convert.ToString(txtDescription.Text);
    //                        txtDescription.BackColor = System.Drawing.Color.White;
    //                    }
    //                    else
    //                    {
    //                        description = string.Empty;
    //                        check = false;
    //                        txtDescription.Focus();
    //                        txtDescription.BackColor = System.Drawing.Color.LightPink;
    //                    }



    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtUOM.Text)))
    //                    {
    //                        UOM = Convert.ToString(txtUOM.Text);
    //                        txtUOM.BackColor = System.Drawing.Color.White;
    //                    }
    //                    else
    //                    {
    //                        UOM = string.Empty;
    //                        check = false;
    //                        txtUOM.Focus();
    //                        txtUOM.BackColor = System.Drawing.Color.LightPink;
    //                    }



    //                    if (Convert.ToInt32(txtQuantity.Text) > 0)
    //                    {
    //                        quantity = Convert.ToInt32(txtQuantity.Text);
    //                        txtQuantity.BackColor = System.Drawing.Color.White;
    //                    }
    //                    else
    //                    {
    //                        quantity = 0;
    //                        check = false;
    //                        txtQuantity.Focus();
    //                        txtQuantity.BackColor = System.Drawing.Color.LightPink;
    //                    }



    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtReqdDateByProjectTeam.Text)))
    //                    {
    //                        reqdDateByProjectTeam = Convert.ToDateTime(txtReqdDateByProjectTeam.Text).ToString("yyyy-MM-dd");
    //                        txtReqdDateByProjectTeam.BackColor = System.Drawing.Color.White;
    //                    }
    //                    else
    //                    {
    //                        reqdDateByProjectTeam = string.Empty;
    //                        check = false;
    //                        txtReqdDateByProjectTeam.Focus();
    //                        txtReqdDateByProjectTeam.BackColor = System.Drawing.Color.LightPink;
    //                    }



    //                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
    //                    {
    //                        if (ddlCategory.SelectedIndex > 0)
    //                        {
    //                            categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
    //                            ddlCategory.BackColor = System.Drawing.Color.White;
    //                        }
    //                        else
    //                        {
    //                            categoryID = 0;
    //                            check = false;
    //                            ddlCategory.Focus();
    //                            ddlCategory.BackColor = System.Drawing.Color.LightPink;
    //                        }
    //                    }







    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtDrawingNo.Text)))
    //                    {
    //                        drawingNo = Convert.ToString(txtDrawingNo.Text);
    //                        txtDrawingNo.BackColor = System.Drawing.Color.White;
    //                    }
    //                    else
    //                    {
    //                        drawingNo = string.Empty;
    //                        check = false;
    //                        txtDrawingNo.Focus();
    //                        txtDrawingNo.BackColor = System.Drawing.Color.LightPink;
    //                    }



    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtDocumentLink.Text)))
    //                    {
    //                        documentLink = Convert.ToString(txtDocumentLink.Text);
    //                        txtDocumentLink.BackColor = System.Drawing.Color.White;
    //                    }
    //                    else
    //                    {
    //                        documentLink = string.Empty;
    //                    }



    //                    if (ddlDrawingRevNo.SelectedIndex > 0)
    //                        drawingRevNo = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);
    //                    else drawingRevNo = 0;




    //                    if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
    //                    {
    //                        isDesignEnggFlag = 1;

    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedStartDateByDesignTeam.Text)))
    //                        {
    //                            plannedStartDateByDesignTeam = Convert.ToDateTime(txtPlannedStartDateByDesignTeam.Text).ToString("yyyy-MM-dd");
    //                            txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.White;
    //                        }
    //                        else
    //                        {
    //                            plannedStartDateByDesignTeam = string.Empty;
    //                            check = false;
    //                            txtPlannedStartDateByDesignTeam.Focus();
    //                            txtPlannedStartDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
    //                        }



    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtPlannedCompletionDateByDesignTeam.Text)))
    //                        {
    //                            plannedCompletionDateByDesignTeam = Convert.ToDateTime(txtPlannedCompletionDateByDesignTeam.Text).ToString("yyyy-MM-dd");
    //                            txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.White;
    //                        }
    //                        else
    //                        {
    //                            plannedCompletionDateByDesignTeam = string.Empty;
    //                            check = false;
    //                            txtPlannedCompletionDateByDesignTeam.Focus();
    //                            txtPlannedCompletionDateByDesignTeam.BackColor = System.Drawing.Color.LightPink;
    //                        }



    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtWorkingStatus.Text)))
    //                        {
    //                            workingStatus = Convert.ToString(txtWorkingStatus.Text);
    //                            txtWorkingStatus.BackColor = System.Drawing.Color.White;
    //                        }
    //                        else
    //                        {
    //                            workingStatus = string.Empty;
    //                            check = false;
    //                            txtWorkingStatus.Focus();
    //                            txtWorkingStatus.BackColor = System.Drawing.Color.LightPink;
    //                        }



    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtExpectedCompletionDate.Text)))
    //                        {
    //                            expectedCompletionDate = Convert.ToDateTime(txtExpectedCompletionDate.Text).ToString("yyyy-MM-dd");
    //                            txtExpectedCompletionDate.BackColor = System.Drawing.Color.White;
    //                        }
    //                        else
    //                        {
    //                            expectedCompletionDate = string.Empty;
    //                            check = false;
    //                            txtExpectedCompletionDate.Focus();
    //                            txtExpectedCompletionDate.BackColor = System.Drawing.Color.LightPink;
    //                        }



    //                        if (ddlResponsibleDesignEngineer.SelectedIndex > 0)
    //                        {
    //                            responsibleDesignEngineerID = Convert.ToInt32(ddlResponsibleDesignEngineer.SelectedValue);
    //                            ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.White;
    //                        }
    //                        else
    //                        {
    //                            responsibleDesignEngineerID = 0;
    //                            check = false;
    //                            ddlResponsibleDesignEngineer.Focus();
    //                            ddlResponsibleDesignEngineer.BackColor = System.Drawing.Color.LightPink;
    //                        }

    //                    }
    //                    else
    //                    {
    //                        isDesignEnggFlag = 0;
    //                        plannedStartDateByDesignTeam = string.Empty;
    //                        plannedCompletionDateByDesignTeam = string.Empty;
    //                        workingStatus = string.Empty;
    //                        expectedCompletionDate = string.Empty;
    //                        responsibleDesignEngineerID = 0;
    //                    }


    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
    //                        remarks = Convert.ToString(txtRemarks.Text);
    //                    else remarks = string.Empty;


    //                    int value = 0;
    //                    if (check == true)
    //                    {
    //                        srNOs += txtSrNo.Text + ",";

    //                        value = objProject.AddDesignDetail(statusID, drawingID, JOBNo, description, UOM, quantity, reqdDateByProjectTeam, categoryID,
    //                                                               plannedStartDateByDesignTeam, plannedCompletionDateByDesignTeam,
    //                                                               drawingNo, documentLink, drawingRevNo, workingStatus,
    //                                                               expectedCompletionDate, responsibleDesignEngineerID,
    //                                                               remarks, isDesignEnggFlag, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                    }

    //                    if (value > 0)
    //                    {
    //                        savedCount++;

    //                        if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
    //                        {
    //                            if (responsibleDesignEngineerID == Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]))
    //                            {
    //                                mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToHimselfMail);
    //                            }
    //                            else
    //                            {
    //                                mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.OpenToOtherMail);
    //                            }

    //                        }
    //                        else
    //                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);

    //                        int mailSentValue = objDMSSendMail.SendMail(value, mailTypeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                        if (mailSentValue > 0)
    //                        {
    //                            int mailSentStatusValue = objProject.UpdateDesignMailStatus(value, statusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                        }
    //                    }
    //                    //}
    //                }
    //            }


    //            if (savedCount > 0)
    //            {
    //                if (!string.IsNullOrEmpty(srNOs))
    //                    srNOs = srNOs.TrimEnd(',');

    //                if (!string.IsNullOrEmpty(srNOs))
    //                    RemoveRowNew(srNOs);

    //                SuccessMessage(savedCount + " design(s) saved...!!!");
    //                return;
    //            }

    //            //if (count == 0)
    //            //{
    //            //    ExceptionMessage("Please select atleaset 1 row...!!!");
    //            //    return;
    //            //}
    //        }
    //        else
    //        {
    //            ExceptionMessage("No data found...!!!");
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

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
