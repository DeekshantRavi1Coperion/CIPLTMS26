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

public partial class PROJECT_DMS_DesignReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]


    BAL.Project objProject = new BAL.Project();

    DataSet dtDesignDetailsList = new DataSet();
    DataSet dsDesignStatusSearch = new DataSet();
    DataSet dsEmp = new DataSet();
    DataSet dsDesignCategory = new DataSet();


    string jobNo = string.Empty;
    int categoryID = 0;
    string isPlanned = string.Empty;
    string drawingNo = string.Empty;
    int postingStatusID = 0;
    int datetTypeID = 0;
    string dateSign = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    int responsibleEnggID = 0;
    int createdByID = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindDesignStatusSearch();
                BindDesignCategoryMainSearch();
                BindRespinsibleEngSearch();


                //if (Request.QueryString["drawingno"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["drawingno"])))
                //{
                //    txtDrawingNoMainSearch.Text = Convert.ToString(Request.QueryString["drawingno"]);
                //}

                GetDesignDetailList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetDesignDetailList();
    }

    protected void gvDesignDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = gvDesignDetails.Rows[rowindex].FindControl("lblRecordID") as Label;

                ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    mpeViewInPDF.Show();
                    iframeViewDrawingDetailsInPDF.Attributes.Add("src", "DrawingDetailInPDF.aspx?recordID=" + Convert.ToInt32(lblRecordID.Text) + "");
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
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                Label lblIsRevised = e.Row.FindControl("lblIsRevised") as Label;
                Label lblPostingStatusID = e.Row.FindControl("lblPostingStatusID") as Label;

                imgStatus.Enabled = false;

                if (Convert.ToInt32(lblPostingStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated))
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/New05.png";
                    imgStatus.ToolTip = "Generated";
                }

                else if (Convert.ToInt32(lblPostingStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Open))
                {
                    imgStatus.ImageUrl = "~/Images/LOT/pe2.png";
                    imgStatus.ToolTip = "Open : Assigned to responsible design engineer";
                }

                else if (Convert.ToInt32(lblPostingStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checking))
                {
                    imgStatus.ImageUrl = "~/Images/LOT/edit5.png";
                    imgStatus.ToolTip = "Checking : Drawing sent for checking";
                }

                else if (Convert.ToInt32(lblPostingStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Checked))
                {
                    imgStatus.ImageUrl = "~/Images/Icons/yes3.png";
                    imgStatus.ToolTip = "Checked : Drawing checked";
                }

                //else if (Convert.ToInt32(lblPostingStatusID.Text) == Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Amendment))
                //{
                //    imgStatus.ImageUrl = "~/Images/Icons/yes3.png";
                //    imgStatus.ToolTip = "Amendment : Sent to amendment";
                //}

                Label lblApplicableForProduction = e.Row.FindControl("lblApplicableForProduction") as Label;
                CheckBox chkApplicableForProductionInList = e.Row.FindControl("chkApplicableForProductionInList") as CheckBox;
                if (Convert.ToInt32(lblApplicableForProduction.Text) > 0)
                    chkApplicableForProductionInList.Checked = true;
                else chkApplicableForProductionInList.Checked = false;

                if (Convert.ToInt32(lblIsRevised.Text) > 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvDesignDetails.Rows.Count > 0)
        {
            ExportToExcelNew();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DESIGN_MGMT/AddDesign.aspx");
    }

    #endregion


    #region METHODS[=========================]


    private void BindDesignStatusSearch()
    {
        try
        {
            dsDesignStatusSearch = objProject.GetDesignStatus();
            if (dsDesignStatusSearch.Tables.Count > 0 && dsDesignStatusSearch.Tables[0].Rows.Count > 0)
            {
                ddlPostingStatusMainSearch.DataSource = dsDesignStatusSearch.Tables[0];
                ddlPostingStatusMainSearch.DataTextField = "STATUS_NAME";
                ddlPostingStatusMainSearch.DataValueField = "STATUS_ID";
                ddlPostingStatusMainSearch.DataBind();
                ddlPostingStatusMainSearch.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDesignCategoryMainSearch()
    {
        try
        {
            dsDesignCategory = objProject.GetDesignCategoryList();
            if (dsDesignCategory.Tables.Count > 0 && dsDesignCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategoryMainSearch.DataSource = dsDesignCategory.Tables[0];
                ddlCategoryMainSearch.DataTextField = "DESIGN_CATEGORY";
                ddlCategoryMainSearch.DataValueField = "DESIGN_CATEGORY_ID";
                ddlCategoryMainSearch.DataBind();
                ddlCategoryMainSearch.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindRespinsibleEngSearch()
    {
        try
        {
            dsEmp = objProject.GetDesignResponsibleEngg();
            if (dsEmp.Tables.Count > 0 && dsEmp.Tables[0].Rows.Count > 0)
            {
                Session["dtEmp"] = dsEmp.Tables[0];
                ddlRespDesignEnggMainSearch.DataSource = dsEmp.Tables[0];
                ddlRespDesignEnggMainSearch.DataTextField = "DESIGN_RESPONSIBLE_ENGG";
                ddlRespDesignEnggMainSearch.DataValueField = "DESIGN_RESPONSIBLE_ENGG_ID";
                ddlRespDesignEnggMainSearch.DataBind();
                ddlRespDesignEnggMainSearch.Items.Insert(0, "Select");

                //int count = 0;

                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                    Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
                {
                    ddlRespDesignEnggMainSearch.SelectedIndex = 0;
                }
                else
                {
                    ddlRespDesignEnggMainSearch.SelectedValue = Convert.ToString(Session["DESIGN_RESPONSIBLE_ENGG_ID"]);
                    ddlRespDesignEnggMainSearch.Enabled = false;

                    //foreach (DataRow dr in dsEmp.Tables[0].Select("DESIGN_RESPONSIBLE_ENGG_ID='" + Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) + "'"))
                    //{
                    //    count++;
                    //    ddlRespDesignEnggMainSearch.SelectedValue = Convert.ToString(Session["DESIGN_RESPONSIBLE_ENGG_ID"]);
                    //    ddlRespDesignEnggMainSearch.Enabled = false;
                    //}
                }

                //if (count == 0)
                //{
                //    ddlRespDesignEnggMainSearch.SelectedIndex = 0;
                //}

            }
            else
            {
                Session["dtEmp"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetDesignDetailList()
    {
        try
        {
            datetTypeID = 0;
            dateSign = string.Empty;
            fromDate = string.Empty;
            toDate = string.Empty;
            categoryID = 0;
            jobNo = string.Empty;
            isPlanned = string.Empty;
            drawingNo = string.Empty;
            postingStatusID = 0;
            responsibleEnggID = 0;
            createdByID = 0;

            datetTypeID = Convert.ToInt32(ddlOnWhichDateMainSearch.SelectedValue);
            dateSign = Convert.ToString(ddlSignMainSearch.SelectedValue);

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");

            if (ddlCategoryMainSearch.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategoryMainSearch.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNoMainSearch.Text))
                jobNo = txtJOBNoMainSearch.Text.Trim().ToUpper();

            if (ddlIsPlannedMainSearch.SelectedIndex > 0)
                isPlanned = Convert.ToString(ddlIsPlannedMainSearch.SelectedValue);
            else
                isPlanned = string.Empty;

            if (!string.IsNullOrEmpty(txtDrawingNoMainSearch.Text))
                drawingNo = txtDrawingNoMainSearch.Text.Trim().ToUpper();

            if (ddlPostingStatusMainSearch.SelectedIndex > 0)
                postingStatusID = Convert.ToInt32(ddlPostingStatusMainSearch.SelectedValue);

            if (ddlRespDesignEnggMainSearch.SelectedIndex > 0)
                responsibleEnggID = Convert.ToInt32(ddlRespDesignEnggMainSearch.SelectedValue);


            if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.SSKarasi) ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Jaswinder) ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == Convert.ToInt32(DMSAllStatusAndTypes.EnumManager.Sunil) ||
                Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) > 0)
            {
                createdByID = 0;
            }
            else
            {
                createdByID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            }


            //dtDesignDetailsList = objProject.GetDesignDetailList(datetTypeID, dateSign, fromDate, toDate, categoryID, jobNo, isPlanned, drawingNo, postingStatusID, responsibleEnggID, createdByID);

            if (dtDesignDetailsList.Tables.Count > 0 && dtDesignDetailsList.Tables[0].Rows.Count > 0)
            {
                Session["dtDesignDetailsList"] = dtDesignDetailsList.Tables[0];
                gvDesignDetails.DataSource = dtDesignDetailsList.Tables[0];
                gvDesignDetails.DataBind();
            }
            else
            {
                Session["dtDesignDetailsList"] = null;
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
            }
            lblRecords.Text = "Records[" + dtDesignDetailsList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcelNew()
    {
        try
        {
            string csv = string.Empty;

            for (int i = 2; i < gvDesignDetails.Columns.Count; i++)
            {
                csv += Convert.ToString(gvDesignDetails.Columns[i].HeaderText) + ',';
            }
            csv += "\r\n";

            int srNo = 0;
            string jobNo = string.Empty;
            string category = string.Empty;
            string description = string.Empty;
            string UOM = string.Empty;
            double quantity = 0;
            string reqdDateByProjectTeam = string.Empty;
            string isPlanned = string.Empty;
            string plannedStartDateByDesignTeam = string.Empty;
            string plannedCompletionDateByDesignTeam = string.Empty;
            string drawingNo = string.Empty;
            int drawingRevNo = 0;
            string status = string.Empty;
            string expectedCompletionDate = string.Empty;
            string responsibleDesignEngg = string.Empty;
            string postingStatus = string.Empty;
            string totalHoursSpent = string.Empty;
            string isApplicableForProduction = string.Empty;

            foreach (GridViewRow gr in gvDesignDetails.Rows)
            {
                srNo = 0;
                jobNo = string.Empty;
                category = string.Empty;
                description = string.Empty;
                UOM = string.Empty;
                quantity = 0;
                reqdDateByProjectTeam = string.Empty;
                isPlanned = string.Empty;
                plannedStartDateByDesignTeam = string.Empty;
                plannedCompletionDateByDesignTeam = string.Empty;
                drawingNo = string.Empty;
                drawingRevNo = 0;
                status = string.Empty;
                expectedCompletionDate = string.Empty;
                responsibleDesignEngg = string.Empty;
                postingStatus = string.Empty;
                totalHoursSpent = string.Empty;
                isApplicableForProduction = string.Empty;



                Label lblSrNo = gr.FindControl("lblSrNo") as Label;
                Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                Label lblCategory = gr.FindControl("lblCategory") as Label;
                Label lblDescription = gr.FindControl("lblDescription") as Label;
                Label lblUOM = gr.FindControl("lblUOM") as Label;
                TextBox txtQuantityInList = gr.FindControl("txtQuantityInList") as TextBox;
                Label lblReqdDateByProjectTeam = gr.FindControl("lblReqdDateByProjectTeam") as Label;
                Label lblIsPlanned = gr.FindControl("lblIsPlanned") as Label;
                Label lblPlannedStartDateByDesignTeam = gr.FindControl("lblPlannedStartDateByDesignTeam") as Label;
                Label lblPlannedCompletionDateByDesignTeam = gr.FindControl("lblPlannedCompletionDateByDesignTeam") as Label;
                Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;
                TextBox txtDrawingRevNoInList = gr.FindControl("txtDrawingRevNoInList") as TextBox;
                Label lblStatus = gr.FindControl("lblStatus") as Label;
                Label lblExpectedCompletionDate = gr.FindControl("lblExpectedCompletionDate") as Label;
                Label lblResponsibleDesignEngineer = gr.FindControl("lblResponsibleDesignEngineer") as Label;
                Label lblPostingStatus = gr.FindControl("lblPostingStatus") as Label;
                TextBox txtTimeSpentInList = gr.FindControl("txtTimeSpentInList") as TextBox;
                CheckBox chkApplicableForProductionInList = gr.FindControl("chkApplicableForProductionInList") as CheckBox;

                if (!string.IsNullOrEmpty(lblSrNo.Text))
                    srNo = Convert.ToInt32(lblSrNo.Text);

                if (!string.IsNullOrEmpty(lblJOBNo.Text))
                    jobNo = lblJOBNo.Text;

                if (!string.IsNullOrEmpty(lblCategory.Text))
                    category = Convert.ToString(lblCategory.Text).Replace(',', ' ');

                if (!string.IsNullOrEmpty(lblDescription.Text))
                    description = lblDescription.Text.Replace(',', ' ');

                if (!string.IsNullOrEmpty(lblUOM.Text))
                    UOM = lblUOM.Text;

                if (!string.IsNullOrEmpty(txtQuantityInList.Text))
                    quantity = Convert.ToDouble(txtQuantityInList.Text);

                if (!string.IsNullOrEmpty(lblReqdDateByProjectTeam.Text))
                    reqdDateByProjectTeam = lblReqdDateByProjectTeam.Text;

                if (!string.IsNullOrEmpty(lblIsPlanned.Text))
                    isPlanned = Convert.ToString(lblIsPlanned.Text);

                if (!string.IsNullOrEmpty(lblPlannedStartDateByDesignTeam.Text))
                    plannedStartDateByDesignTeam = lblPlannedStartDateByDesignTeam.Text;

                if (!string.IsNullOrEmpty(lblPlannedCompletionDateByDesignTeam.Text))
                    plannedCompletionDateByDesignTeam = lblPlannedCompletionDateByDesignTeam.Text;

                if (!string.IsNullOrEmpty(lblDrawingNo.Text))
                    drawingNo = lblDrawingNo.Text;

                if (!string.IsNullOrEmpty(txtDrawingRevNoInList.Text))
                    drawingRevNo = Convert.ToInt32(txtDrawingRevNoInList.Text);

                if (!string.IsNullOrEmpty(lblStatus.Text))
                    status = lblStatus.Text.Replace(',', ' '); ;

                if (!string.IsNullOrEmpty(lblExpectedCompletionDate.Text))
                    expectedCompletionDate = lblExpectedCompletionDate.Text;

                if (!string.IsNullOrEmpty(lblResponsibleDesignEngineer.Text))
                    responsibleDesignEngg = Convert.ToString(lblResponsibleDesignEngineer.Text);

                if (!string.IsNullOrEmpty(lblPostingStatus.Text))
                    postingStatus = Convert.ToString(lblPostingStatus.Text);

                if (!string.IsNullOrEmpty(txtTimeSpentInList.Text))
                    totalHoursSpent = Convert.ToString(txtTimeSpentInList.Text);

                if (chkApplicableForProductionInList.Checked)
                    isApplicableForProduction = "Yes";
                else isApplicableForProduction = "No";


                csv += srNo + "," +
                            jobNo + "," +
                            category + "," +
                            description + "," +
                            UOM + "," +
                            quantity + "," +
                            reqdDateByProjectTeam + "," +
                            isPlanned + "," +
                            plannedStartDateByDesignTeam + "," +
                            plannedCompletionDateByDesignTeam + "," +
                            drawingNo + "," +
                            drawingRevNo + "," +
                            status + "," +
                            expectedCompletionDate + "," +
                            responsibleDesignEngg + "," +
                            postingStatus + "," +
                            totalHoursSpent + "," +
                           isApplicableForProduction;

                csv += "\r\n";
            }

            string fileName = "Design_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
