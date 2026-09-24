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

public partial class PROJECT_DMS_DesignListProjectView : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DMSSendMail objDMSSendMail = new DMSSendMail();
    BAL.Project objProject = new BAL.Project();

    DataSet dtDesignDetailsList = new DataSet();
    DataSet dsDesignStatusSearch = new DataSet();
    DataSet dsDesignEngg = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsDesignChecker = new DataSet();
    DataSet dsDesignCategory = new DataSet();
    DataSet dsDesignJobNo = new DataSet();
    DataSet dsProjectJobNo = new DataSet();
    DataTable dtAction = new DataTable();
    DataTable dtDrawings = new DataTable();


    string jobNo = string.Empty;
    int categoryID = 0;
    string isPlanned = string.Empty;
    string drawingNo = string.Empty;
    int postingStatusID = 0;
    int datetTypeID = 0;
    string dateSign = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    int designCheckerID = 0;
    int responsibleEnggID = 0;
    int createdByID = 0;



    int srNo = 0;
    int amendmentCount = 0;
    int recordID = 0;
    //string jobNo = string.Empty;
    //int categoryID = 0;
    string category = string.Empty;
    string description = string.Empty;
    string UOM = string.Empty;
    int quantity = 0;
    string reqdDateByProjectTeam = string.Empty;
    int isPlannedID = 0;
    //string isPlanned = string.Empty;
    string plannedStartDateByDesignTeam = string.Empty;
    string plannedCompletionDateByDesignTeam = string.Empty;
    //string drawingNo = string.Empty;
    string documentLink = string.Empty;
    int drawingRevNo = 0;
    string workingStatus = string.Empty;
    string expectedCompletionDate = string.Empty;
    int responsibleDesignEnggID = 0;
    int isDesignEnggFlag = 0;
    string responsibleDesignEngg = string.Empty;
    //int postingStatusID = 0;
    string postingStatus = string.Empty;
    int isApplicableForProduction = 0;
    string remarks = string.Empty;
    int isRevised = 0;
    int revisedRecordID = 0;

    int companyID = 0;
    string custCode = string.Empty;
    string poNo = string.Empty;

    int statusID = 0;




    int empRecordID = 0;
    string projectCategory = string.Empty;
    string projectNumber = string.Empty;
    string jobNumber = string.Empty;

    string drawingCategory = string.Empty;
    string serialNumber = string.Empty;
    string size = string.Empty;
    string rev = string.Empty;
    int sheets = 0;
    string drawingID = string.Empty;
    int drawingTypeID = 0;

    string startTime = string.Empty;
    string endTime = string.Empty;
    string timeSpent = string.Empty;
    string title = string.Empty;
    string status = string.Empty;

    string timesheetDate = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtTimesheet"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindDesignStatusSearch();
                BindDesignCategoryMainSearch();
                BindDesignCheckerSearch();
                BindRespinsibleEngSearch();
                BindCreatedBySearch();

                if (Request.QueryString["drawingno"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["drawingno"])))
                {
                    txtDrawingNoMainSearch.Text = Convert.ToString(Request.QueryString["drawingno"]);
                }

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

            //DataTable dt = (DataTable)Session["dtDesignDetailsList"];
            //ToCSVNew01(dt);
        }
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
                ddlCategoryMainSearch.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindDesignCheckerSearch()
    {
        try
        {
            dsDesignChecker = objProject.GetDesignChecker();
            if (dsDesignChecker.Tables.Count > 0 && dsDesignChecker.Tables[0].Rows.Count > 0)
            {
                ddlDesignCheckerMainSearch.DataSource = dsDesignChecker.Tables[0];
                ddlDesignCheckerMainSearch.DataTextField = "DESIGN_CHECKER";
                ddlDesignCheckerMainSearch.DataValueField = "DESIGN_CHECKER_ID";
                ddlDesignCheckerMainSearch.DataBind();
                ddlDesignCheckerMainSearch.Items.Insert(0, "All");
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
            dsDesignEngg = objProject.GetDesignResponsibleEngg();
            if (dsDesignEngg.Tables.Count > 0 && dsDesignEngg.Tables[0].Rows.Count > 0)
            {
                //Session["dtDesignEngg"] = dsDesignEngg.Tables[0];
                ddlRespDesignEnggMainSearch.DataSource = dsDesignEngg.Tables[0];
                ddlRespDesignEnggMainSearch.DataTextField = "DESIGN_RESPONSIBLE_ENGG";
                ddlRespDesignEnggMainSearch.DataValueField = "DESIGN_RESPONSIBLE_ENGG_ID";
                ddlRespDesignEnggMainSearch.DataBind();
                ddlRespDesignEnggMainSearch.Items.Insert(0, "All");


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
                }
            }
            else
            {
                //Session["dtDesignEngg"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void BindCreatedBySearch()
    {
        try
        {
            dsEmployee = objProject.GetEmployeesToAddApprover();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlCreatedByMainSearch.DataSource = dsEmployee.Tables[0];
                ddlCreatedByMainSearch.DataTextField = "EMPLOYEE_NAME";
                ddlCreatedByMainSearch.DataValueField = "EMP_RECORD_ID";
                ddlCreatedByMainSearch.DataBind();
                ddlCreatedByMainSearch.Items.Insert(0, "All");
                ddlCreatedByMainSearch.SelectedIndex = 0;
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
            designCheckerID = 0;
            responsibleEnggID = 0;
            createdByID = 0;

            datetTypeID = Convert.ToInt32(ddlOnWhichDateMainSearch.SelectedValue);
            dateSign = Convert.ToString(ddlSignMainSearch.SelectedValue);

            if (chkSelectDates.Checked)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                    fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                    toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            }

            if (ddlCategoryMainSearch.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategoryMainSearch.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNoMainSearch.Text))
                jobNo = txtJOBNoMainSearch.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtDrawingNoMainSearch.Text))
                drawingNo = txtDrawingNoMainSearch.Text.Trim().ToUpper();

            if (ddlPostingStatusMainSearch.SelectedIndex > 0)
                postingStatusID = Convert.ToInt32(ddlPostingStatusMainSearch.SelectedValue);

            if (ddlDesignCheckerMainSearch.SelectedIndex > 0)
                designCheckerID = Convert.ToInt32(ddlDesignCheckerMainSearch.SelectedValue);

            if (ddlRespDesignEnggMainSearch.SelectedIndex > 0)
                responsibleEnggID = Convert.ToInt32(ddlRespDesignEnggMainSearch.SelectedValue);

            if (ddlCreatedByMainSearch.SelectedIndex > 0)
                createdByID = Convert.ToInt32(ddlCreatedByMainSearch.SelectedValue);

            dtDesignDetailsList = objProject.GetDesignDetailListForProjectView(datetTypeID, dateSign, fromDate, toDate, categoryID, jobNo,
                                                                drawingNo, postingStatusID, designCheckerID, responsibleEnggID, createdByID);

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

            for (int i = 0; i < gvDesignDetails.Columns.Count; i++)
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
            string plannedStartDateByDesignTeam = string.Empty;
            string plannedCompletionDateByDesignTeam = string.Empty;
            string drawingNo = string.Empty;
            string clientDrawingNo = string.Empty;
            string contractorDrawingNo = string.Empty;
            int drawingRevNo = 0;
            string remarks = string.Empty;
            string expectedCompletionDate = string.Empty;
            string drawingSentToDesign = string.Empty;
            string responsibleDesignEngg = string.Empty;
            string drawingStatus = string.Empty;
            string totalHoursSpent = string.Empty;
            string completedByDesignEnggDate = string.Empty;
            string checkedDate = string.Empty;
            string designer = string.Empty;
            string designerTimesheetHours = "00:00";
            string checker = string.Empty;
            string checkerTimesheetHours = "00:00";
            string totalTimehseetHours = "00:00";

            foreach (GridViewRow gr in gvDesignDetails.Rows)
            {
                srNo = 0;
                jobNo = string.Empty;
                category = string.Empty;
                description = string.Empty;
                UOM = string.Empty;
                quantity = 0;
                reqdDateByProjectTeam = string.Empty;
                plannedStartDateByDesignTeam = string.Empty;
                plannedCompletionDateByDesignTeam = string.Empty;
                drawingNo = string.Empty;
                clientDrawingNo = string.Empty;
                contractorDrawingNo = string.Empty;
                drawingRevNo = 0;
                remarks = string.Empty;
                expectedCompletionDate = string.Empty;
                drawingSentToDesign = string.Empty;
                responsibleDesignEngg = string.Empty;
                postingStatus = string.Empty;
                totalHoursSpent = string.Empty;
                completedByDesignEnggDate = string.Empty;
                checkedDate = string.Empty;
                designer = string.Empty;
                designerTimesheetHours = "00:00";
                checker = string.Empty;
                checkerTimesheetHours = "00:00";
                totalTimehseetHours = "00:00";

                Label lblSrNo = gr.FindControl("lblSrNo") as Label;
                Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                Label lblCategory = gr.FindControl("lblCategory") as Label;
                Label lblDescription = gr.FindControl("lblDescription") as Label;
                Label lblUOM = gr.FindControl("lblUOM") as Label;
                Label lblQuantity = gr.FindControl("lblQuantity") as Label;
                Label lblReqdDateByProjectTeam = gr.FindControl("lblReqdDateByProjectTeam") as Label;
                Label lblPlannedStartDateByDesignTeam = gr.FindControl("lblPlannedStartDateByDesignTeam") as Label;
                Label lblPlannedCompletionDateByDesignTeam = gr.FindControl("lblPlannedCompletionDateByDesignTeam") as Label;
                Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;
                Label lblClientDrawingNo = gr.FindControl("lblClientDrawingNo") as Label;
                Label lblContractorDrawingNo = gr.FindControl("lblContractorDrawingNo") as Label;
                Label lblRevisionNo = gr.FindControl("lblRevisionNo") as Label;
                Label lblRemarks = gr.FindControl("lblRemarks") as Label;
                Label lblExpectedCompletionDate = gr.FindControl("lblExpectedCompletionDate") as Label;
                Label lblDrawingSentToDesign = gr.FindControl("lblDrawingSentToDesign") as Label;
                Label lblDesignEngineer = gr.FindControl("lblDesignEngineer") as Label;
                Label lblDrawingStatus = gr.FindControl("lblDrawingStatus") as Label;
                Label lblHoursSpent = gr.FindControl("lblHoursSpent") as Label;
                Label lblCompletedByDesignEnggDate = gr.FindControl("lblCompletedByDesignEnggDate") as Label;
                Label lblCheckedDate = gr.FindControl("lblCheckedDate") as Label;

                Label lblDesigner = gr.FindControl("lblDesigner") as Label;
                Label lblDesignerTimesheetHours = gr.FindControl("lblDesignerTimesheetHours") as Label;
                Label lblChecker = gr.FindControl("lblChecker") as Label;
                Label lblCheckerTimesheetHours = gr.FindControl("lblCheckerTimesheetHours") as Label;
                Label lblTotalTimehseetHours = gr.FindControl("lblTotalTimehseetHours") as Label;

                if (!string.IsNullOrEmpty(lblSrNo.Text))
                    srNo = Convert.ToInt32(lblSrNo.Text);

                if (!string.IsNullOrEmpty(lblJOBNo.Text))
                    jobNo = lblJOBNo.Text;

                if (!string.IsNullOrEmpty(lblCategory.Text))
                    category = Convert.ToString(lblCategory.Text).Replace(',', ' ');

                if (!string.IsNullOrEmpty(lblDescription.Text))
                {
                    description = lblDescription.Text.Replace(',', ' ');
                    description = description.Replace('\r', ' ').Replace('\n', ' ');
                }

                if (!string.IsNullOrEmpty(lblUOM.Text))
                    UOM = lblUOM.Text;

                if (!string.IsNullOrEmpty(lblQuantity.Text))
                    quantity = Convert.ToDouble(lblQuantity.Text);

                if (!string.IsNullOrEmpty(lblReqdDateByProjectTeam.Text))
                    reqdDateByProjectTeam = lblReqdDateByProjectTeam.Text;

                if (!string.IsNullOrEmpty(lblPlannedStartDateByDesignTeam.Text))
                    plannedStartDateByDesignTeam = lblPlannedStartDateByDesignTeam.Text;

                if (!string.IsNullOrEmpty(lblPlannedCompletionDateByDesignTeam.Text))
                    plannedCompletionDateByDesignTeam = lblPlannedCompletionDateByDesignTeam.Text;

                if (!string.IsNullOrEmpty(lblDrawingNo.Text))
                    drawingNo = lblDrawingNo.Text;


                if (!string.IsNullOrEmpty(lblClientDrawingNo.Text))
                    clientDrawingNo = lblClientDrawingNo.Text;

                if (!string.IsNullOrEmpty(lblContractorDrawingNo.Text))
                    contractorDrawingNo = lblContractorDrawingNo.Text;

                if (!string.IsNullOrEmpty(lblRevisionNo.Text))
                    drawingRevNo = Convert.ToInt32(lblRevisionNo.Text);

                if (!string.IsNullOrEmpty(lblRemarks.Text))
                {
                    remarks = lblRemarks.Text.Replace(',', ' '); ;
                    remarks = remarks.Replace('\r', ' ').Replace('\n', ' ');
                }

                if (!string.IsNullOrEmpty(lblExpectedCompletionDate.Text))
                    expectedCompletionDate = lblExpectedCompletionDate.Text;

                if (!string.IsNullOrEmpty(lblDrawingSentToDesign.Text))
                    drawingSentToDesign = Convert.ToString(lblDrawingSentToDesign.Text);

                if (!string.IsNullOrEmpty(lblDesignEngineer.Text))
                    responsibleDesignEngg = Convert.ToString(lblDesignEngineer.Text);

                if (!string.IsNullOrEmpty(lblCompletedByDesignEnggDate.Text))
                    completedByDesignEnggDate = Convert.ToString(lblCompletedByDesignEnggDate.Text);

                if (!string.IsNullOrEmpty(lblDrawingStatus.Text))
                {
                    drawingStatus = Convert.ToString(lblDrawingStatus.Text);
                    drawingStatus = drawingStatus.Replace('\r', ' ').Replace('\n', ' ');
                }

                if (!string.IsNullOrEmpty(lblCheckedDate.Text))
                    checkedDate = Convert.ToString(lblCheckedDate.Text);


                if (!string.IsNullOrEmpty(lblHoursSpent.Text))
                    totalHoursSpent = Convert.ToString(lblHoursSpent.Text);

                if (!string.IsNullOrEmpty(lblDesigner.Text))
                    designer = Convert.ToString(lblDesigner.Text);

                if (!string.IsNullOrEmpty(lblDesignerTimesheetHours.Text))
                    designerTimesheetHours = Convert.ToString(lblDesignerTimesheetHours.Text);

                if (!string.IsNullOrEmpty(lblChecker.Text))
                    checker = Convert.ToString(lblChecker.Text);

                if (!string.IsNullOrEmpty(lblCheckerTimesheetHours.Text))
                    checkerTimesheetHours = Convert.ToString(lblCheckerTimesheetHours.Text);

                if (!string.IsNullOrEmpty(lblTotalTimehseetHours.Text))
                    totalTimehseetHours = Convert.ToString(lblTotalTimehseetHours.Text);

                csv += srNo + "," +
                            jobNo + "," +
                            category + "," +
                            description + "," +
                            UOM + "," +
                            quantity + "," +
                            reqdDateByProjectTeam + "," +
                            plannedStartDateByDesignTeam + "," +
                            plannedCompletionDateByDesignTeam + "," +
                            drawingNo + "," +
                            clientDrawingNo + "," +
                            contractorDrawingNo + "," +
                            drawingRevNo + "," +
                            remarks + "," +
                            expectedCompletionDate + "," +
                            drawingSentToDesign + "," +
                            responsibleDesignEngg + "," +
                            completedByDesignEnggDate + "," +
                            drawingStatus + "," +
                            checkedDate + "," +
                            totalHoursSpent + "," +
                            designer + "," +
                            designerTimesheetHours + "," +
                            checker + "," +
                            checkerTimesheetHours + "," +
                            totalTimehseetHours
                            ;

                csv += "\r\n";
            }

            string fileName = "Design_List_Project_View_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
            if (dt.Rows.Count > 0)
            {


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

                string fileName = "Design_List_Project_View_" + DateTime.Now.ToString("dd_MMM_yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
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
