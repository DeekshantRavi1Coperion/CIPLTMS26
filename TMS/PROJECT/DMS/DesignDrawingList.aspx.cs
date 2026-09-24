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

public partial class PROJECT_DMS_DesignDrawingList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    DMSSendMail objDMSSendMail = new DMSSendMail();
    BAL.Project objProject = new BAL.Project();

    DataSet dsUnit = new DataSet();
    DataSet dsDesignDrawingList = new DataSet();
    DataSet dsJobNo = new DataSet();
    DataSet dsCategory = new DataSet();

    int recordID = 0;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string jobNo = string.Empty;
    string drawingNo = string.Empty;

    string clientDrawingNo = string.Empty;
    string contractorDrawingNo = string.Empty;

    string description = string.Empty;
    int quantity = 0;
    string UOM = string.Empty;
    string rqdDateByProjectTeam = string.Empty;
    string isActive = string.Empty;


    int statusID = 0;
    int drawingID = 0;
    string JOBNo = string.Empty;
    int jobUnitID = 0;
    //string description = string.Empty;
    //string UOM = string.Empty;
    //int quantity = 0;
    string reqdDateByProjectTeam = string.Empty;
    //string plannedStartDateByDesignTeam = string.Empty;
    //string plannedCompletionDateByDesignTeam = string.Empty;
    //string drawingNo = string.Empty;
    string documentLink = string.Empty;
    int drawingRevNo = 0;
    //string workingStatus = string.Empty;
    //string expectedCompletionDate = string.Empty;
    int categoryID = 0;
    string remarks = string.Empty;
    //int isDesignEnggFlag = 0;

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

                hdDateByProjectTeamToEdit.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDateByProjectTeamToEdit.Text = hdDateByProjectTeamToEdit.Value;

                BindCompany();
                GetDesignDrawingList();

                ddlIsActive.SelectedValue = "1";
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetDesignDrawingList();
    }

    protected void gvDesignDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE" ||
                    Convert.ToString(e.CommandArgument) == "ACTIVATE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                if (Convert.ToString(e.CommandArgument) == "SAVE_DESIGN")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblDrawingID = gvDesignDetails.Rows[rowindex].FindControl("lblDrawingID") as Label;
                Label lblIsActive = gvDesignDetails.Rows[rowindex].FindControl("lblIsActive") as Label;

                CheckBox chkIsActive = gvDesignDetails.Rows[rowindex].FindControl("chkIsActive") as CheckBox;
                CheckBox chkSelect = gvDesignDetails.Rows[rowindex].FindControl("chkSelect") as CheckBox;

                ImageButton imgProperties = gvDesignDetails.Rows[rowindex].FindControl("imgProperties") as ImageButton;
                ImageButton imgBtnRemove = gvDesignDetails.Rows[rowindex].FindControl("imgBtnRemove") as ImageButton;
                ImageButton imgBtnActivate = gvDesignDetails.Rows[rowindex].FindControl("imgBtnActivate") as ImageButton;
                Button btnSaveDesign = gvDesignDetails.Rows[rowindex].FindControl("btnSaveDesign") as Button;

                Label lblJOBNo = gvDesignDetails.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblDrawingNo = gvDesignDetails.Rows[rowindex].FindControl("lblDrawingNo") as Label;

                Label lblClientDrawingNo = gvDesignDetails.Rows[rowindex].FindControl("lblClientDrawingNo") as Label;
                Label lblContractorDrawingNo = gvDesignDetails.Rows[rowindex].FindControl("lblContractorDrawingNo") as Label;

                Label lblDescription = gvDesignDetails.Rows[rowindex].FindControl("lblDescription") as Label;
                TextBox txtQuantity = gvDesignDetails.Rows[rowindex].FindControl("txtQuantity") as TextBox;
                Label lblUOM = gvDesignDetails.Rows[rowindex].FindControl("lblUOM") as Label;
                Label lblReqdDateByProjectTeam = gvDesignDetails.Rows[rowindex].FindControl("lblReqdDateByProjectTeam") as Label;
                TextBox txtDocumentLink = gvDesignDetails.Rows[rowindex].FindControl("txtDocumentLink") as TextBox;
                DropDownList ddlDrawingRevNo = gvDesignDetails.Rows[rowindex].FindControl("ddlDrawingRevNo") as DropDownList;
                DropDownList ddlCategory = gvDesignDetails.Rows[rowindex].FindControl("ddlCategory") as DropDownList;
                TextBox txtRemarks = gvDesignDetails.Rows[rowindex].FindControl("txtRemarks") as TextBox;

                ViewState["RECORD_ID"] = Convert.ToInt32(lblDrawingID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (!string.IsNullOrEmpty(lblJOBNo.Text))
                        txtJOBNoToEdit.Text = lblJOBNo.Text;
                    else txtJOBNoToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblDrawingNo.Text))
                        txtDrawingNumberToEdit.Text = lblDrawingNo.Text;
                    else txtDrawingNumberToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblClientDrawingNo.Text))
                        txtClientDrawingNumberToEdit.Text = lblClientDrawingNo.Text;
                    else txtClientDrawingNumberToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblContractorDrawingNo.Text))
                        txtContractorDrawingNumberToEdit.Text = lblContractorDrawingNo.Text;
                    else txtContractorDrawingNumberToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblDescription.Text))
                        txtDescriptionToEdit.Text = lblDescription.Text;
                    else txtDescriptionToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                        txtQuantityToEdit.Text = txtQuantity.Text;
                    else txtQuantityToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblUOM.Text))
                        txtUOMToEdit.Text = lblUOM.Text;
                    else txtUOMToEdit.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblReqdDateByProjectTeam.Text))
                    {
                        hdDateByProjectTeamToEdit.Value = lblReqdDateByProjectTeam.Text;
                        txtDateByProjectTeamToEdit.Text = hdDateByProjectTeamToEdit.Value;
                    }
                    else
                    {
                        hdDateByProjectTeamToEdit.Value = string.Empty;
                        txtQuantityToEdit.Text = string.Empty;
                    }

                    mpeUpdateDrawing.Show();
                }

                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    RemoveOrActivateDrawwing(Convert.ToInt32(lblDrawingID.Text), 1);
                }

                if (Convert.ToString(e.CommandArgument) == "ACTIVATE")
                {
                    RemoveOrActivateDrawwing(Convert.ToInt32(lblDrawingID.Text), 0);
                }

                if (Convert.ToString(e.CommandArgument) == "SAVE_DESIGN")
                {
                    if (ddlCategory.SelectedIndex > 0)
                    {
                        ddlCategory.BackColor = System.Drawing.Color.White;
                        //SaveSingleDesign(Convert.ToInt32(lblDrawingID.Text));
                        GetRecordsToSave(Convert.ToInt32(lblDrawingID.Text));
                    }
                    else
                    {
                        ddlCategory.Focus();
                        ddlCategory.BackColor = System.Drawing.Color.LightPink;
                        ExceptionMessage("Please select category...!!!");
                        return;
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

    protected void gvDesignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{
                //    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                //}

                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
                {
                    e.Row.Cells[11].Visible = true;
                    e.Row.Cells[12].Visible = true;
                    e.Row.Cells[13].Visible = true;
                    e.Row.Cells[14].Visible = true;
                    e.Row.Cells[15].Visible = true;
                }
                else
                {
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblIsActive = e.Row.FindControl("lblIsActive") as Label;
                CheckBox chkIsActive = e.Row.FindControl("chkIsActive") as CheckBox;
                CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;

                ImageButton imgProperties = e.Row.FindControl("imgProperties") as ImageButton;
                ImageButton imgBtnRemove = e.Row.FindControl("imgBtnRemove") as ImageButton;
                ImageButton imgBtnActivate = e.Row.FindControl("imgBtnActivate") as ImageButton;
                Button btnSaveDesign = e.Row.FindControl("btnSaveDesign") as Button;

                Label lblJOBNo = e.Row.FindControl("lblJOBNo") as Label;
                Label lblDrawingNo = e.Row.FindControl("lblDrawingNo") as Label;
                Label lblDescription = e.Row.FindControl("lblDescription") as Label;
                TextBox txtQuantity = e.Row.FindControl("txtQuantity") as TextBox;
                Label lblUOM = e.Row.FindControl("lblUOM") as Label;
                Label lblReqdDateByProjectTeam = e.Row.FindControl("lblReqdDateByProjectTeam") as Label;
                TextBox txtDocumentLink = e.Row.FindControl("txtDocumentLink") as TextBox;
                DropDownList ddlDrawingRevNo = e.Row.FindControl("ddlDrawingRevNo") as DropDownList;
                DropDownList ddlCategory = e.Row.FindControl("ddlCategory") as DropDownList;
                TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;

                imgProperties.Visible = false;
                imgBtnRemove.Visible = false;
                imgBtnActivate.Visible = false;
                chkSelect.Visible = false;
                btnSaveDesign.Visible = false;

                if (Convert.ToInt32(lblIsActive.Text) == 0)
                {
                    chkIsActive.Checked = false;

                    imgProperties.Visible = false;
                    imgBtnRemove.Visible = false;
                    imgBtnActivate.Visible = true;

                    chkSelect.Visible = false;
                    btnSaveDesign.Visible = false;
                }
                else
                {
                    chkIsActive.Checked = true;

                    imgProperties.Visible = true;
                    imgBtnRemove.Visible = true;
                    imgBtnActivate.Visible = false;

                    chkSelect.Visible = true;
                    btnSaveDesign.Visible = true;
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



                if (Convert.ToInt32(Session["DESIGN_RESPONSIBLE_ENGG_ID"]) == 0)
                {
                    e.Row.Cells[11].Visible = true;
                    e.Row.Cells[12].Visible = true;
                    e.Row.Cells[13].Visible = true;
                    e.Row.Cells[14].Visible = true;
                    e.Row.Cells[15].Visible = true;

                    chkSelect.Visible = true;
                    btnSaveDesign.Visible = true;
                }
                else
                {
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;

                    chkSelect.Visible = false;
                    btnSaveDesign.Visible = false;
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




    // JOB DETAILS
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
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNo") as Label;
                txtJOBNoToEdit.Text = Convert.ToString(lblJOBNo.Text).Trim();

                mpeUpdateDrawing.Show();
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



    protected void btnUpdateDrawing_Click(object sender, EventArgs e)
    {
        UpdateDesignDrawing();
    }

    protected void btnAddDrawing_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/AddDeisgnDrawing.aspx");
    }

    protected void btnImportDrawings_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/ImportDrawings.aspx");
    }

    protected void btnSaveDesign_Click(object sender, EventArgs e)
    {
        //SaveDesign();
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

    private void GetDesignDrawingList()
    {
        try
        {
            fromDate = string.Empty;
            toDate = string.Empty;
            jobNo = string.Empty;
            drawingNo = string.Empty;
            isActive = string.Empty;

            if (chkSelectDates.Checked)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                    fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                    toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(txtJOBNoMainSearch.Text))
                jobNo = txtJOBNoMainSearch.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtDrawingNoMainSearch.Text))
                drawingNo = txtDrawingNoMainSearch.Text.Trim().ToUpper();

            if (ddlIsActive.SelectedIndex > 0)
                isActive = Convert.ToString(ddlIsActive.SelectedValue);

            dsDesignDrawingList = objProject.GetDesignDrawingsList(fromDate, toDate, jobNo, drawingNo, isActive);

            if (dsDesignDrawingList.Tables.Count > 0 && dsDesignDrawingList.Tables[0].Rows.Count > 0)
            {
                gvDesignDetails.DataSource = dsDesignDrawingList.Tables[0];
                gvDesignDetails.DataBind();
            }
            else
            {
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
            }
            lblRecords.Text = "Records[" + dsDesignDrawingList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataSet GetJOBData()
    {
        try
        {
            int jobUnitID = 0;
            string jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;

            jobUnitID = Convert.ToInt32(ddlCompanySearch.SelectedValue);

            dsJobNo = objProject.GetJOBDetailsForDesignDrawings(jobUnitID, jobNo);

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

    private void RemoveOrActivateDrawwing(int recordID, int activeValue)
    {
        try
        {
            int value = 0;
            if (recordID > 0)
            {
                value = objProject.RemoveOrActivateDrawwing(recordID, activeValue, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }

            if (value > 0)
            {
                SuccessMessage("Updated successfully...!!!");
                GetDesignDrawingList();
                Reset();
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

    private void UpdateDesignDrawing()
    {
        try
        {
            recordID = 0;
            jobNo = string.Empty;
            drawingNo = string.Empty;

            clientDrawingNo = string.Empty;
            contractorDrawingNo = string.Empty;

            description = string.Empty;
            quantity = 0;
            UOM = string.Empty;
            rqdDateByProjectTeam = string.Empty;

            recordID = Convert.ToInt32(ViewState["RECORD_ID"]);

            if (!string.IsNullOrEmpty(txtJOBNoToEdit.Text))
                jobNo = txtJOBNoToEdit.Text.ToUpper();

            if (!string.IsNullOrEmpty(txtDrawingNumberToEdit.Text))
                drawingNo = txtDrawingNumberToEdit.Text.Trim().ToUpper();


            if (!string.IsNullOrEmpty(txtClientDrawingNumberToEdit.Text))
                clientDrawingNo = txtClientDrawingNumberToEdit.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtContractorDrawingNumberToEdit.Text))
                contractorDrawingNo = txtContractorDrawingNumberToEdit.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtDescriptionToEdit.Text))
                description = txtDescriptionToEdit.Text;

            if (Convert.ToDouble(txtQuantityToEdit.Text) > 0)
                quantity = Convert.ToInt32(txtQuantityToEdit.Text);

            if (!string.IsNullOrEmpty(txtUOMToEdit.Text))
                UOM = txtUOMToEdit.Text;

            if (!string.IsNullOrEmpty(hdDateByProjectTeamToEdit.Value))
                rqdDateByProjectTeam = Convert.ToDateTime(hdDateByProjectTeamToEdit.Value).ToString("yyyy-MM-dd");



            DataSet dsDuplicacy = new DataSet();
            dsDuplicacy = objProject.CheckDrawingForDuplicacy(drawingNo, recordID);
            if (dsDuplicacy.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(dsDuplicacy.Tables[0].Rows[0]["DRAWINGS_COUNT"]) > 0)
                {
                    ExceptionMessage("Drawing No. already exists...!!!");
                    return;
                }
            }


            int value = 0;
            if (recordID > 0)
            {
                value = objProject.UpdateDesignDrawing(recordID, jobNo, drawingNo, clientDrawingNo, contractorDrawingNo, description, quantity, UOM, rqdDateByProjectTeam, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }

            if (value > 0)
            {
                SuccessMessage("Updated successfully...!!!");
                GetDesignDrawingList();
                Reset();
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

    private void Reset()
    {
        try
        {
            txtJOBNoToEdit.Text = string.Empty;
            txtDrawingNumberToEdit.Text = string.Empty;
            txtDescriptionToEdit.Text = string.Empty;
            txtQuantityToEdit.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void GetRecordsToSave(int drawingID)
    {
        try
        {

            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("STATUS_ID", typeof(int));
            dtTemp.Columns.Add("DRAWING_ID", typeof(int));
            dtTemp.Columns.Add("JOB_NO", typeof(string));
            dtTemp.Columns.Add("JOB_UNIT_ID", typeof(int));
            dtTemp.Columns.Add("DESCRIPTION", typeof(string));
            dtTemp.Columns.Add("UOM", typeof(string));
            dtTemp.Columns.Add("QUANTITY", typeof(int));
            dtTemp.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
            dtTemp.Columns.Add("CATEGORY_ID", typeof(int));
            dtTemp.Columns.Add("DOCUMENT_LINK", typeof(string));
            dtTemp.Columns.Add("DRAWING_NO", typeof(string));

            dtTemp.Columns.Add("CLIENT_DRAWING_NO", typeof(string));
            dtTemp.Columns.Add("CONTRACTOR_DRAWING_NO", typeof(string));
            
            dtTemp.Columns.Add("DRAWING_REV_NO", typeof(string));
            dtTemp.Columns.Add("REMARKS", typeof(string));

            string srNOs = string.Empty;
            int count = 0;
            int savedCount = 0;
            int mailTypeID = 0;
            bool check = true;

            //drawingID = 0;
            statusID = 0;
            JOBNo = string.Empty;
            jobUnitID = 0;
            description = string.Empty;
            UOM = string.Empty;
            quantity = 0;
            reqdDateByProjectTeam = string.Empty;
            drawingNo = string.Empty;

            clientDrawingNo = string.Empty;
            contractorDrawingNo = string.Empty;

            documentLink = string.Empty;
            drawingRevNo = 0;
            categoryID = 0;
            remarks = string.Empty;
            check = true;

            if (gvDesignDetails.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvDesignDetails.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                    if (drawingID > 0)
                    {
                        Label lblDrawingID = gr.FindControl("lblDrawingID") as Label;
                        if (Convert.ToInt32(lblDrawingID.Text) == drawingID)
                        {
                            //drawingID = 0;
                            statusID = 0;
                            JOBNo = string.Empty;
                            jobUnitID = 0;
                            description = string.Empty;
                            UOM = string.Empty;
                            quantity = 0;
                            reqdDateByProjectTeam = string.Empty;
                            drawingNo = string.Empty;

                            clientDrawingNo = string.Empty;
                            contractorDrawingNo = string.Empty;

                            documentLink = string.Empty;
                            drawingRevNo = 0;
                            categoryID = 0;
                            remarks = string.Empty;


                            check = true;
                            count++;


                            Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                            Label lblJOBUnitID = gr.FindControl("lblJOBUnitID") as Label;
                            Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;

                            Label lblClientDrawingNo = gr.FindControl("lblClientDrawingNo") as Label;
                            Label lblContractorDrawingNo = gr.FindControl("lblContractorDrawingNo") as Label;

                            Label lblDescription = gr.FindControl("lblDescription") as Label;
                            TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                            Label lblUOM = gr.FindControl("lblUOM") as Label;
                            Label lblReqdDateByProjectTeam = gr.FindControl("lblReqdDateByProjectTeam") as Label;
                            TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
                            DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
                            DropDownList ddlCategory = gr.FindControl("ddlCategory") as DropDownList;
                            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                            DataRow dr = dtTemp.NewRow();

                            dr["STATUS_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);
                            dr["DRAWING_ID"] = Convert.ToInt32(lblDrawingID.Text);

                            if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text)))
                                dr["JOB_NO"] = Convert.ToString(lblJOBNo.Text);
                            else dr["JOB_NO"] = string.Empty;

                            if (Convert.ToInt32(lblJOBUnitID.Text) > 0)
                                dr["JOB_UNIT_ID"] = Convert.ToInt32(lblJOBUnitID.Text);
                            else dr["JOB_UNIT_ID"] = 0;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDescription.Text)))
                                dr["DESCRIPTION"] = Convert.ToString(lblDescription.Text);
                            else dr["DESCRIPTION"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text)))
                                dr["UOM"] = Convert.ToString(lblUOM.Text);
                            else dr["UOM"] = string.Empty;

                            if (Convert.ToInt32(txtQuantity.Text) > 0)
                                dr["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
                            else dr["QUANTITY"] = 0;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblReqdDateByProjectTeam.Text)))
                                dr["REQD_DATE_BY_PROJECT_TEAM"] = Convert.ToDateTime(lblReqdDateByProjectTeam.Text).ToString("yyyy-MM-dd");
                            else dr["REQD_DATE_BY_PROJECT_TEAM"] = string.Empty;


                            if (ddlCategory.SelectedIndex > 0)
                            {
                                ddlCategory.BackColor = System.Drawing.Color.White;
                                dr["CATEGORY_ID"] = Convert.ToInt32(ddlCategory.SelectedValue);
                            }
                            else
                            {
                                dr["CATEGORY_ID"] = 0;
                                ddlCategory.Focus();
                                ddlCategory.BackColor = System.Drawing.Color.LightPink;
                                ExceptionMessage("Please select category...!!!");
                                return;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(txtDocumentLink.Text)))
                                dr["DOCUMENT_LINK"] = Convert.ToString(txtDocumentLink.Text);
                            else dr["DOCUMENT_LINK"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text)))
                                dr["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text);
                            else dr["DRAWING_NO"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblClientDrawingNo.Text)))
                                dr["CLIENT_DRAWING_NO"] = Convert.ToString(lblClientDrawingNo.Text);
                            else dr["CLIENT_DRAWING_NO"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblContractorDrawingNo.Text)))
                                dr["CONTRACTOR_DRAWING_NO"] = Convert.ToString(lblContractorDrawingNo.Text);
                            else dr["CONTRACTOR_DRAWING_NO"] = string.Empty;


                            dr["DRAWING_REV_NO"] = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);

                            if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                                dr["REMARKS"] = Convert.ToString(txtRemarks.Text);
                            else dr["REMARKS"] = string.Empty;


                            dtTemp.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        if (chkSelect.Checked)
                        {
                            drawingID = 0;
                            statusID = 0;
                            JOBNo = string.Empty;
                            jobUnitID = 0;
                            description = string.Empty;
                            UOM = string.Empty;
                            quantity = 0;
                            reqdDateByProjectTeam = string.Empty;
                            drawingNo = string.Empty;

                            clientDrawingNo = string.Empty;
                            contractorDrawingNo = string.Empty;

                            documentLink = string.Empty;
                            drawingRevNo = 0;
                            categoryID = 0;
                            remarks = string.Empty;


                            check = true;
                            count++;


                            Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                            Label lblJOBUnitID = gr.FindControl("lblJOBUnitID") as Label;
                            Label lblDrawingID = gr.FindControl("lblDrawingID") as Label;
                            Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;

                            Label lblClientDrawingNo = gr.FindControl("lblClientDrawingNo") as Label;
                            Label lblContractorDrawingNo = gr.FindControl("lblContractorDrawingNo") as Label;

                            Label lblDescription = gr.FindControl("lblDescription") as Label;
                            TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                            Label lblUOM = gr.FindControl("lblUOM") as Label;
                            Label lblReqdDateByProjectTeam = gr.FindControl("lblReqdDateByProjectTeam") as Label;
                            TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
                            DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
                            DropDownList ddlCategory = gr.FindControl("ddlCategory") as DropDownList;
                            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                            DataRow dr = dtTemp.NewRow();

                            dr["STATUS_ID"] = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);
                            dr["DRAWING_ID"] = Convert.ToInt32(lblDrawingID.Text);

                            if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text)))
                                dr["JOB_NO"] = Convert.ToString(lblJOBNo.Text);
                            else dr["JOB_NO"] = string.Empty;

                            if (Convert.ToInt32(lblJOBUnitID.Text) > 0)
                                dr["JOB_UNIT_ID"] = Convert.ToInt32(lblJOBUnitID.Text);
                            else dr["JOB_UNIT_ID"] = 0;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDescription.Text)))
                                dr["DESCRIPTION"] = Convert.ToString(lblDescription.Text);
                            else dr["DESCRIPTION"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text)))
                                dr["UOM"] = Convert.ToString(lblUOM.Text);
                            else dr["UOM"] = string.Empty;

                            if (Convert.ToInt32(txtQuantity.Text) > 0)
                                dr["QUANTITY"] = Convert.ToInt32(txtQuantity.Text);
                            else dr["QUANTITY"] = 0;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblReqdDateByProjectTeam.Text)))
                                dr["REQD_DATE_BY_PROJECT_TEAM"] = Convert.ToDateTime(lblReqdDateByProjectTeam.Text).ToString("yyyy-MM-dd");
                            else dr["REQD_DATE_BY_PROJECT_TEAM"] = string.Empty;



                            if (ddlCategory.SelectedIndex > 0)
                            {
                                ddlCategory.BackColor = System.Drawing.Color.White;
                                dr["CATEGORY_ID"] = Convert.ToInt32(ddlCategory.SelectedValue);
                            }
                            else
                            {
                                dr["CATEGORY_ID"] = 0;
                                ddlCategory.Focus();
                                ddlCategory.BackColor = System.Drawing.Color.LightPink;
                                check = false;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(txtDocumentLink.Text)))
                                dr["DOCUMENT_LINK"] = Convert.ToString(txtDocumentLink.Text);
                            else dr["DOCUMENT_LINK"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text)))
                                dr["DRAWING_NO"] = Convert.ToString(lblDrawingNo.Text);
                            else dr["DRAWING_NO"] = string.Empty;


                            if (!string.IsNullOrEmpty(Convert.ToString(lblClientDrawingNo.Text)))
                                dr["CLIENT_DRAWING_NO"] = Convert.ToString(lblClientDrawingNo.Text);
                            else dr["CLIENT_DRAWING_NO"] = string.Empty;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblContractorDrawingNo.Text)))
                                dr["CONTRACTOR_DRAWING_NO"] = Convert.ToString(lblContractorDrawingNo.Text);
                            else dr["CONTRACTOR_DRAWING_NO"] = string.Empty;


                            dr["DRAWING_REV_NO"] = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);

                            if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                                dr["REMARKS"] = Convert.ToString(txtRemarks.Text);
                            else dr["REMARKS"] = string.Empty;


                            if (check)
                            {
                                dtTemp.Rows.Add(dr);
                            }
                        }
                    }
                }


                if (count == 0)
                {
                    ExceptionMessage("Please select atleaset 1 row...!!!");
                    return;
                }



                if (dtTemp.Rows.Count > 0)
                {
                    SaveDesign(dtTemp);
                }
                else
                {
                    ExceptionMessage("Please select atleast 1 category...!!!");
                    return;
                }


                //if (savedCount > 0)
                //{
                //    SuccessMessage(savedCount + " design(s) saved...!!!");
                //    GetDesignDrawingList();
                //    return;
                //}

                //if (count == 0)
                //{
                //    ExceptionMessage("Please select atleaset 1 row...!!!");
                //    return;
                //}
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

    private void SaveDesign(DataTable dtTemp)
    {
        try
        {
            string drawingNos = string.Empty;
            drawingNo = string.Empty;
            int mailTypeID = 0;
            statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);
            int value = objProject.AddDesignDetail(0, dtTemp, null, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    drawingNos += "'" + dr["DRAWING_NO"] + "',";
                }
                if (!string.IsNullOrEmpty(drawingNos))
                    drawingNos = drawingNos.TrimEnd(',');


                int mailSentValue = 0;
                if (!string.IsNullOrEmpty(drawingNos))
                {
                    mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);
                    mailSentValue = objDMSSendMail.ProcessAndSendMail(drawingNos, 0, statusID, mailTypeID);
                    if (mailSentValue > 0)
                    {
                        int mailSentStatusValue = objProject.UpdateDesignMailStatus(drawingNos, 0, statusID, 0, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    }
                }

                if (mailSentValue > 0)
                {
                    SuccessMessage(dtTemp.Rows.Count + " design(s) saved and mail sent successfully...!!!");
                    GetDesignDrawingList();
                    return;
                }
                else
                {
                    SuccessMessage(dtTemp.Rows.Count + " design(s) saved successfully...!!!");
                    GetDesignDrawingList();
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

    //private void SaveDesign()
    //{
    //    try
    //    {

    //        DataTable dtTemp = new DataTable();
    //        dtTemp.Columns.Add("STATUS_ID", typeof(int));
    //        dtTemp.Columns.Add("DRAWING_ID", typeof(int));
    //        dtTemp.Columns.Add("JOB_NO", typeof(string));
    //        dtTemp.Columns.Add("DESCRIPTION", typeof(string));
    //        dtTemp.Columns.Add("UOM", typeof(string));
    //        dtTemp.Columns.Add("QUANTITY", typeof(int));
    //        dtTemp.Columns.Add("REQD_DATE_BY_PROJECT_TEAM", typeof(string));
    //        dtTemp.Columns.Add("PLANNED_START_DATE_BY_DESIGN_TEAM", typeof(string));
    //        dtTemp.Columns.Add("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM", typeof(string));
    //        dtTemp.Columns.Add("DRAWING_NO", typeof(string));
    //        dtTemp.Columns.Add("DOCUMENT_LINK", typeof(string));
    //        dtTemp.Columns.Add("DRAWING_REV_NO", typeof(string));
    //        dtTemp.Columns.Add("WORKING_STATUS", typeof(string));
    //        dtTemp.Columns.Add("EXPECTED_COMPLETION_DATE", typeof(string));
    //        dtTemp.Columns.Add("DESIGN_RESPONSIBLE_ENGG_ID", typeof(int));
    //        dtTemp.Columns.Add("REMARKS", typeof(string));

    //        string srNOs = string.Empty;
    //        int count = 0;
    //        int savedCount = 0;
    //        int mailTypeID = 0;
    //        bool check = true;

    //        drawingID = 0;
    //        statusID = 0;
    //        JOBNo = string.Empty;
    //        description = string.Empty;
    //        UOM = string.Empty;
    //        quantity = 0;
    //        reqdDateByProjectTeam = string.Empty;
    //        //plannedStartDateByDesignTeam = string.Empty;
    //        //plannedCompletionDateByDesignTeam = string.Empty;
    //        drawingNo = string.Empty;
    //        documentLink = string.Empty;
    //        drawingRevNo = 0;
    //        //workingStatus = string.Empty;
    //        //expectedCompletionDate = string.Empty;
    //        categoryID = 0;
    //        remarks = string.Empty;
    //        //isDesignEnggFlag = 0;
    //        check = true;

    //        if (gvDesignDetails.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow gr in gvDesignDetails.Rows)
    //            {
    //                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

    //                if (chkSelect.Checked)
    //                {

    //                    drawingID = 0;
    //                    statusID = 0;
    //                    JOBNo = string.Empty;
    //                    description = string.Empty;
    //                    UOM = string.Empty;
    //                    quantity = 0;
    //                    reqdDateByProjectTeam = string.Empty;
    //                    drawingNo = string.Empty;
    //                    documentLink = string.Empty;
    //                    drawingRevNo = 0;
    //                    categoryID = 0;
    //                    remarks = string.Empty;


    //                    check = true;
    //                    count++;


    //                    Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
    //                    Label lblDrawingID = gr.FindControl("lblDrawingID") as Label;
    //                    Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;
    //                    Label lblDescription = gr.FindControl("lblDescription") as Label;
    //                    TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
    //                    Label lblUOM = gr.FindControl("lblUOM") as Label;
    //                    Label lblReqdDateByProjectTeam = gr.FindControl("lblReqdDateByProjectTeam") as Label;
    //                    TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
    //                    DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
    //                    DropDownList ddlCategory = gr.FindControl("ddlCategory") as DropDownList;
    //                    TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

    //                    statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);

    //                    drawingID = Convert.ToInt32(lblDrawingID.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text)))
    //                        JOBNo = Convert.ToString(lblJOBNo.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblDescription.Text)))
    //                        description = Convert.ToString(lblDescription.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text)))
    //                        UOM = Convert.ToString(lblUOM.Text);

    //                    if (Convert.ToInt32(txtQuantity.Text) > 0)
    //                        quantity = Convert.ToInt32(txtQuantity.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblReqdDateByProjectTeam.Text)))
    //                        reqdDateByProjectTeam = Convert.ToDateTime(lblReqdDateByProjectTeam.Text).ToString("yyyy-MM-dd");

    //                    if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text)))
    //                        drawingNo = Convert.ToString(lblDrawingNo.Text);

    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtDocumentLink.Text)))
    //                        documentLink = Convert.ToString(txtDocumentLink.Text);

    //                    drawingRevNo = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);



    //                    if (ddlCategory.SelectedIndex > 0)
    //                    {
    //                        ddlCategory.BackColor = System.Drawing.Color.White;
    //                        categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
    //                    }
    //                    else
    //                    {
    //                        check = false;
    //                        categoryID = 0;
    //                        ddlCategory.Focus();
    //                        ddlCategory.BackColor = System.Drawing.Color.LightPink;
    //                        ExceptionMessage("Please select category...!!!");
    //                    }



    //                    if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
    //                        remarks = Convert.ToString(txtRemarks.Text);









    //                    int value = 0;
    //                    if (check == true)
    //                    {
    //                        value = objProject.AddDesignDetail(statusID, drawingID, JOBNo, description, UOM, quantity, reqdDateByProjectTeam, categoryID, "", "",
    //                                                           drawingNo, documentLink, drawingRevNo, "", "", 0,
    //                                                           remarks, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                    }

    //                    if (value > 0)
    //                    {
    //                        savedCount++;

    //                        mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);
    //                        int mailSentValue = objDMSSendMail.SendMail(value, mailTypeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                        if (mailSentValue > 0)
    //                        {
    //                            int mailSentStatusValue = objProject.UpdateDesignMailStatus(value, statusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                        }
    //                    }



    //                }
    //            }


    //            if (savedCount > 0)
    //            {
    //                SuccessMessage(savedCount + " design(s) saved...!!!");
    //                GetDesignDrawingList();
    //                return;
    //            }

    //            if (count == 0)
    //            {
    //                ExceptionMessage("Please select atleaset 1 row...!!!");
    //                return;
    //            }
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

    //private void SaveSingleDesign(int drawingID)
    //{
    //    try
    //    {
    //        string srNOs = string.Empty;
    //        int count = 0;
    //        int savedCount = 0;
    //        int mailTypeID = 0;

    //        statusID = 0;
    //        JOBNo = string.Empty;
    //        description = string.Empty;
    //        UOM = string.Empty;
    //        quantity = 0;
    //        reqdDateByProjectTeam = string.Empty;
    //        //plannedStartDateByDesignTeam = string.Empty;
    //        //plannedCompletionDateByDesignTeam = string.Empty;
    //        drawingNo = string.Empty;
    //        documentLink = string.Empty;
    //        drawingRevNo = 0;
    //        //workingStatus = string.Empty;
    //        //expectedCompletionDate = string.Empty;
    //        categoryID = 0;
    //        remarks = string.Empty;
    //        //isDesignEnggFlag = 0;


    //        if (gvDesignDetails.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow gr in gvDesignDetails.Rows)
    //            {
    //                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
    //                Label lblDrawingID = gr.FindControl("lblDrawingID") as Label;

    //                if (Convert.ToInt32(lblDrawingID.Text) == drawingID)
    //                {
    //                    if (chkSelect.Checked)
    //                    {
    //                        count++;

    //                        Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
    //                        Label lblDrawingNo = gr.FindControl("lblDrawingNo") as Label;
    //                        Label lblDescription = gr.FindControl("lblDescription") as Label;
    //                        TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
    //                        Label lblUOM = gr.FindControl("lblUOM") as Label;
    //                        Label lblReqdDateByProjectTeam = gr.FindControl("lblReqdDateByProjectTeam") as Label;
    //                        TextBox txtDocumentLink = gr.FindControl("txtDocumentLink") as TextBox;
    //                        DropDownList ddlDrawingRevNo = gr.FindControl("ddlDrawingRevNo") as DropDownList;
    //                        DropDownList ddlCategory = gr.FindControl("ddlCategory") as DropDownList;
    //                        TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

    //                        statusID = Convert.ToInt32(DMSAllStatusAndTypes.EnumStatus.Generated);

    //                        if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text)))
    //                            JOBNo = Convert.ToString(lblJOBNo.Text);

    //                        if (!string.IsNullOrEmpty(Convert.ToString(lblDescription.Text)))
    //                            description = Convert.ToString(lblDescription.Text);

    //                        if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text)))
    //                            UOM = Convert.ToString(lblUOM.Text);

    //                        if (Convert.ToInt32(txtQuantity.Text) > 0)
    //                            quantity = Convert.ToInt32(txtQuantity.Text);

    //                        if (!string.IsNullOrEmpty(Convert.ToString(lblReqdDateByProjectTeam.Text)))
    //                            reqdDateByProjectTeam = Convert.ToDateTime(lblReqdDateByProjectTeam.Text).ToString("yyyy-MM-dd");

    //                        if (!string.IsNullOrEmpty(Convert.ToString(lblDrawingNo.Text)))
    //                            drawingNo = Convert.ToString(lblDrawingNo.Text);

    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtDocumentLink.Text)))
    //                            documentLink = Convert.ToString(txtDocumentLink.Text);

    //                        drawingRevNo = Convert.ToInt32(ddlDrawingRevNo.SelectedValue);

    //                        if (ddlCategory.SelectedIndex > 0)
    //                        {
    //                            ddlCategory.BackColor = System.Drawing.Color.White;
    //                            categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
    //                        }
    //                        else
    //                        {
    //                            categoryID = 0;
    //                            ddlCategory.Focus();
    //                            ddlCategory.BackColor = System.Drawing.Color.LightPink;
    //                            ExceptionMessage("Please select category...!!!");
    //                            return;
    //                        }

    //                        if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
    //                            remarks = Convert.ToString(txtRemarks.Text);

    //                        int value = objProject.AddDesignDetail(statusID, drawingID, JOBNo, description, UOM, quantity, reqdDateByProjectTeam, categoryID, "", "",
    //                                                                   drawingNo, documentLink, drawingRevNo, "", "", 0,
    //                                                                   remarks, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));

    //                        if (value > 0)
    //                        {
    //                            savedCount++;

    //                            mailTypeID = Convert.ToInt32(DMSAllStatusAndTypes.EnumMailType.GeneratedMail);
    //                            int mailSentValue = objDMSSendMail.SendMail(value, mailTypeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                            if (mailSentValue > 0)
    //                            {
    //                                int mailSentStatusValue = objProject.UpdateDesignMailStatus(value, statusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                            }
    //                        }
    //                    }
    //                }
    //            }


    //            if (savedCount > 0)
    //            {
    //                SuccessMessage(savedCount + " design(s) saved...!!!");
    //                GetDesignDrawingList();
    //                return;
    //            }

    //            if (count == 0)
    //            {
    //                ExceptionMessage("Please select atleaset 1 row...!!!");
    //                return;
    //            }
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
