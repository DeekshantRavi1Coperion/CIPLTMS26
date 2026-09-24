using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MGMT_EstimatedProjectList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    DataSet dsCategory = new DataSet();
    DataSet dsCodes = new DataSet();
    DataSet dsEstimatedProjectList = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindCategory();
                BindCodes();
                GetEstimatedProjctList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetEstimatedProjctList();
    }

    protected void gvEstimatedProjectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEstimatedProjectList.PageIndex = e.NewPageIndex;
        GetEstimatedProjctList();
    }

    protected void gvEstimatedProjectList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblProjectID = gvEstimatedProjectList.Rows[rowindex].FindControl("lblProjectID") as Label;
                Label lblProjectNo = gvEstimatedProjectList.Rows[rowindex].FindControl("lblProjectNo") as Label;
                Label lblDescription = gvEstimatedProjectList.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblScope = gvEstimatedProjectList.Rows[rowindex].FindControl("lblScope") as Label;
                Label lblQuantity = gvEstimatedProjectList.Rows[rowindex].FindControl("lblQuantity") as Label;
                Label lblUnitRateINR = gvEstimatedProjectList.Rows[rowindex].FindControl("lblUnitRateINR") as Label;
                Label lblPriceINR = gvEstimatedProjectList.Rows[rowindex].FindControl("lblPriceINR") as Label;
                Label lblPandF = gvEstimatedProjectList.Rows[rowindex].FindControl("lblPandF") as Label;
                Label lblPFD = gvEstimatedProjectList.Rows[rowindex].FindControl("lblPFD") as Label;
                Label lblED = gvEstimatedProjectList.Rows[rowindex].FindControl("lblED") as Label;
                Label lblTotalED = gvEstimatedProjectList.Rows[rowindex].FindControl("lblTotalED") as Label;
                Label lblST = gvEstimatedProjectList.Rows[rowindex].FindControl("lblST") as Label;
                Label lblSTAmt = gvEstimatedProjectList.Rows[rowindex].FindControl("lblSTAmt") as Label;
                Label lblTotalCostInclEDST = gvEstimatedProjectList.Rows[rowindex].FindControl("lblTotalCostInclEDST") as Label;
                Label lblImportedEXWRateEuro = gvEstimatedProjectList.Rows[rowindex].FindControl("lblImportedEXWRateEuro") as Label;
                Label lblImportedEXWPriceEURO = gvEstimatedProjectList.Rows[rowindex].FindControl("lblImportedEXWPriceEURO") as Label;
                Label lblFOBPriceEuro = gvEstimatedProjectList.Rows[rowindex].FindControl("lblFOBPriceEuro") as Label;
                Label lblEquivRupeePrice = gvEstimatedProjectList.Rows[rowindex].FindControl("lblEquivRupeePrice") as Label;
                Label lblFullDuty = gvEstimatedProjectList.Rows[rowindex].FindControl("lblFullDuty") as Label;
                Label lblTotalImportCost = gvEstimatedProjectList.Rows[rowindex].FindControl("lblTotalImportCost") as Label;
                Label lblTotalCostINR = gvEstimatedProjectList.Rows[rowindex].FindControl("lblTotalCostINR") as Label;
                Label lblTotalCostEURO = gvEstimatedProjectList.Rows[rowindex].FindControl("lblTotalCostEURO") as Label;
                Label lblCategory = gvEstimatedProjectList.Rows[rowindex].FindControl("lblCategory") as Label;
                Label lblCodes = gvEstimatedProjectList.Rows[rowindex].FindControl("lblCodes") as Label;

                publicValuesForEstimatedProject.projectID = Convert.ToInt32(lblProjectID.Text);

                lblLegendProjectNo.Text = lblProjectNo.Text;
                txtDescription.Text = Convert.ToString(lblDescription.Text);
                txtScope.Text = Convert.ToString(lblScope.Text);
                txtQuantity.Text = Convert.ToString(lblQuantity.Text);
                txtUnitRateINR.Text = Convert.ToString(lblUnitRateINR.Text);
                txtPriceINR.Text = Convert.ToString(lblPriceINR.Text);
                txtPandF.Text = Convert.ToString(lblPandF.Text);
                txtPFD.Text = Convert.ToString(lblPFD.Text);
                txtED.Text = Convert.ToString(lblED.Text);
                txtTotalED.Text = Convert.ToString(lblTotalED.Text);
                txtST.Text = Convert.ToString(lblST.Text);
                txtSTAmt.Text = Convert.ToString(lblSTAmt.Text);
                txtTotalCostInclEDST.Text = Convert.ToString(lblTotalCostInclEDST.Text);
                txtImportedEXWRateEURO.Text = Convert.ToString(lblImportedEXWRateEuro.Text);
                txtImportedEXWPriceEURO.Text = Convert.ToString(lblImportedEXWPriceEURO.Text);
                txtFOBPriceEURO.Text = Convert.ToString(lblFOBPriceEuro.Text);
                txtEquivRupeePrice.Text = Convert.ToString(lblEquivRupeePrice.Text);
                txtFullDuty.Text = Convert.ToString(lblFullDuty.Text);
                txtTotalImportCost.Text = Convert.ToString(lblTotalImportCost.Text);
                txtTotalCostINR.Text = Convert.ToString(lblTotalCostINR.Text);
                txtTotalCostEURO.Text = Convert.ToString(lblTotalCostEURO.Text);
                //ddlCategory.SelectedValue = Convert.ToString(lblCategory.Text);
                //ddlCodes.SelectedValue = Convert.ToString(lblCodes.Text);

                if (e.CommandArgument == "PROPERTIES")
                {
                    btnSubmit.Text = "Update Estimated Project";
                    this.ModalPopupExtender1.Show();
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

    protected void gvEstimatedProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblProjectNo = (Label)e.Row.FindControl("lblProjectNo");

                e.Row.ToolTip = lblProjectNo.Text;

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

    protected void btnImportNewEstimatedProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT_MGMT/ImportEstimatedProject.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UpdateEstimatedProject();
    }

    #endregion


    #region METHODS[=======================]

    private void BindCategory()
    {
        try
        {
            dsCategory = objProject.GetDetailsBySP("sp_get_estimated_category");
            if (dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCategory.Tables[0];
                ddlCategory.DataTextField = "ESTIMATED_CATEGORY";
                ddlCategory.DataValueField = "ESTIMATED_CATEGORY";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, "Select");
                ddlCategory.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCodes()
    {
        try
        {
            dsCodes = objProject.GetDetailsBySP("sp_get_estimated_codes");
            if (dsCodes.Tables.Count > 0 && dsCodes.Tables[0].Rows.Count > 0)
            {
                ddlCodes.DataSource = dsCodes.Tables[0];
                ddlCodes.DataTextField = "ESTIMATED_CODE";
                ddlCodes.DataValueField = "ESTIMATED_CODE";
                ddlCodes.DataBind();
                ddlCodes.Items.Insert(0, "Select");
                ddlCodes.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetEstimatedProjctList()
    {
        try
        {
            string startDate = string.Empty;
            string endDate = string.Empty;
            string projectNo = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;


            if (!string.IsNullOrEmpty(txtProjectNo.Text))
                projectNo = txtProjectNo.Text.Trim();
            else
                projectNo = string.Empty;

            dsEstimatedProjectList = objProject.GetEstimatedProjctList(startDate, endDate, projectNo);
            if (dsEstimatedProjectList.Tables.Count > 0 && dsEstimatedProjectList.Tables[0].Rows.Count > 0)
            {
                gvEstimatedProjectList.DataSource = dsEstimatedProjectList.Tables[0];
                gvEstimatedProjectList.DataBind();
            }
            else
            {
                gvEstimatedProjectList.DataSource = null;
                gvEstimatedProjectList.DataBind();
            }
            lblRecords.Text = "Records[" + dsEstimatedProjectList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateEstimatedProject()
    {
        try
        {
            int projectID = publicValuesForEstimatedProject.projectID;
            string projectNo = string.Empty;
            string description = string.Empty;
            string scope = string.Empty;
            double quantity = 0;
            double unitRateINR = 0;
            double priceINR = 0;
            double PandF = 0;
            double PFD = 0;
            double ED = 0;
            double totalED = 0;
            double ST = 0;
            double STAmt = 0;
            double totalCostInclEDST = 0;
            double importedEXWRateEuro = 0;
            double importedEXWPriceEURO = 0;
            double fOBPriceEuro = 0;
            double equivRupeePrice = 0;
            double fullDuty = 0;
            double totalImportCost = 0;
            double totalCostINR = 0;
            double totalCostEURO = 0;
            string category = string.Empty;
            string codes = string.Empty;



            if (!string.IsNullOrEmpty(lblLegendProjectNo.Text))
                projectNo = lblLegendProjectNo.Text;
            else
                projectNo = string.Empty;


            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = txtDescription.Text;
            else
                description = string.Empty;

            if (!string.IsNullOrEmpty(txtScope.Text))
                scope = txtScope.Text;
            else
                scope = string.Empty;


            if (!string.IsNullOrEmpty(txtQuantity.Text))
                quantity = Convert.ToDouble(txtQuantity.Text);
            else
                quantity = 0;


            if (!string.IsNullOrEmpty(txtUnitRateINR.Text))
                unitRateINR = Convert.ToDouble(txtUnitRateINR.Text);
            else
                unitRateINR = 0;


            if (!string.IsNullOrEmpty(txtPriceINR.Text))
                priceINR = Convert.ToDouble(txtPriceINR.Text);
            else
                priceINR = 0;


            if (!string.IsNullOrEmpty(txtPandF.Text))
                PandF = Convert.ToDouble(txtPandF.Text);
            else
                PandF = 0;


            if (!string.IsNullOrEmpty(txtPFD.Text))
                PFD = Convert.ToDouble(txtPFD.Text);
            else
                PFD = 0;



            if (!string.IsNullOrEmpty(txtED.Text))
                ED = Convert.ToDouble(txtED.Text);
            else
                ED = 0;


            if (!string.IsNullOrEmpty(txtTotalED.Text))
                totalED = Convert.ToDouble(txtTotalED.Text);
            else
                totalED = 0;


            if (!string.IsNullOrEmpty(txtST.Text))
                ST = Convert.ToDouble(txtST.Text);
            else
                ST = 0;

            if (!string.IsNullOrEmpty(txtSTAmt.Text))
                STAmt = Convert.ToDouble(txtSTAmt.Text);
            else
                STAmt = 0;

            if (!string.IsNullOrEmpty(txtTotalCostInclEDST.Text))
                totalCostInclEDST = Convert.ToDouble(txtTotalCostInclEDST.Text);
            else
                totalCostInclEDST = 0;

            if (!string.IsNullOrEmpty(txtImportedEXWRateEURO.Text))
                importedEXWRateEuro = Convert.ToDouble(txtImportedEXWRateEURO.Text);
            else
                importedEXWRateEuro = 0;

            if (!string.IsNullOrEmpty(txtImportedEXWPriceEURO.Text))
                importedEXWPriceEURO = Convert.ToDouble(txtImportedEXWPriceEURO.Text);
            else
                importedEXWPriceEURO = 0;


            if (!string.IsNullOrEmpty(txtFOBPriceEURO.Text))
                fOBPriceEuro = Convert.ToDouble(txtFOBPriceEURO.Text);
            else
                fOBPriceEuro = 0;


            if (!string.IsNullOrEmpty(txtEquivRupeePrice.Text))
                equivRupeePrice = Convert.ToDouble(txtEquivRupeePrice.Text);
            else
                equivRupeePrice = 0;

            if (!string.IsNullOrEmpty(txtFullDuty.Text))
                fullDuty = Convert.ToDouble(txtFullDuty.Text);
            else
                fullDuty = 0;

            if (!string.IsNullOrEmpty(txtTotalImportCost.Text))
                totalImportCost = Convert.ToDouble(txtTotalImportCost.Text);
            else
                totalImportCost = 0;

            if (!string.IsNullOrEmpty(txtTotalCostINR.Text))
                totalCostINR = Convert.ToDouble(txtTotalCostINR.Text);
            else
                totalCostINR = 0;

            if (!string.IsNullOrEmpty(txtTotalCostEURO.Text))
                totalCostEURO = Convert.ToDouble(txtTotalCostEURO.Text);
            else
                totalCostEURO = 0;




            if (ddlCategory.SelectedIndex > 0)
                category = Convert.ToString(ddlCategory.SelectedValue);
            else
                category = string.Empty;

            if (ddlCodes.SelectedIndex > 0)
                codes = Convert.ToString(ddlCodes.SelectedValue);
            else
                codes = string.Empty;


            int value = objProject.UpdateEstimatedProject(projectID, projectNo, description, scope, quantity, unitRateINR, priceINR, PandF,
                        PFD, ED, totalED, ST, STAmt, totalCostInclEDST, importedEXWRateEuro, importedEXWPriceEURO, fOBPriceEuro,
                        equivRupeePrice, fullDuty, totalImportCost, totalCostINR, totalCostEURO, category, codes,
                        Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Project No. '" + projectNo + "' updated successfully");
                GetEstimatedProjctList();
            }
            else
                ExceptionMessage("Please try again!");

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

    #endregion

}

public static class publicValuesForEstimatedProject
{
    public static int projectID = 0;
}