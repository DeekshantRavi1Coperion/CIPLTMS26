using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class REPORTS_PURCHASE_ORDER_PIVOT_GROUP_AddPOPivotGroupLog : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Reports objReports = new BAL.Reports();

    DataSet dsUnit = new DataSet();
    DataSet dsPONo = new DataSet();
    DataSet dsNewPivotGroup = new DataSet();

    int companyID = 0;
    string JOBNo = string.Empty;
    string poNo = string.Empty;
    string pivotGroup = string.Empty;
    string pivotGroupDesc = string.Empty;



    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                BindUnit();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }


    // PO DETAILS
    protected void btnGetPONo_Click(object sender, EventArgs e)
    {
        txtJOBNoSearch.Text = txtJOBNo.Text;
        mpePODetail.Show();
        GetPODetail();
    }

    protected void btnSearchPONo_Click(object sender, EventArgs e)
    {
        mpePODetail.Show();
        GetPODetail();
    }

    protected void gvPODetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblPONo = gvPODetail.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblUnitID = gvPODetail.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblOldPivotGroup = gvPODetail.Rows[rowindex].FindControl("lblOldPivotGroup") as Label;
                Label lblOldPivotGroupDesc = gvPODetail.Rows[rowindex].FindControl("lblOldPivotGroupDesc") as Label;

                txtPONo.Text = Convert.ToString(lblPONo.Text).Trim();
                txtOldPivotGroup.Text = Convert.ToString(lblOldPivotGroup.Text).Trim();
                txtOldPivotGroupDesc.Text = Convert.ToString(lblOldPivotGroupDesc.Text).Trim();
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




    // NEW PIVOT GROUP DETAIL
    protected void btnGetNewPivotGroup_Click(object sender, EventArgs e)
    {
        mpePONewPGDetail.Show();
        GetPONewPivotGroupDetail();
    }

    protected void btnSearchPONoNewPG_Click(object sender, EventArgs e)
    {
        mpePONewPGDetail.Show();
        GetPONewPivotGroupDetail();
    }

    protected void gvPONewPGDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblNewPivotGroup = gvPONewPGDetail.Rows[rowindex].FindControl("lblNewPivotGroup") as Label;
                Label lblNewPivotGroupDesc = gvPONewPGDetail.Rows[rowindex].FindControl("lblNewPivotGroupDesc") as Label;


                txtNewPivotGroup.Text = Convert.ToString(lblNewPivotGroup.Text).Trim();
                txtNewPivotGroupDesc.Text = Convert.ToString(lblNewPivotGroupDesc.Text).Trim();
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




    protected void btnSave_Click(object sender, EventArgs e)
    {
        SavePivotGroup();
    }

    protected void btnPOPivotGroupLogReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/REPORTS/PURCHASE_ORDER/PIVOT_GROUP/POPivotGroupLogList.aspx");
    }

    #endregion


    #region METHODS[=========================]


    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnit.DataSource = dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                //ddlUnit.Items.Insert(0, "Select");

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private DataSet GetPOData()
    {
        try
        {
            companyID = 0;
            JOBNo = string.Empty;
            poNo = string.Empty;

            companyID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                JOBNo = txtJOBNoSearch.Text;

            if (!string.IsNullOrEmpty(txtPONoSearch.Text))
                poNo = txtPONoSearch.Text;

            dsPONo = objReports.GetPODetailForPivotGroup(companyID, JOBNo, poNo);
            if (dsPONo.Tables.Count > 0)
            {
                return dsPONo;
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

    private void GetPODetail()
    {
        try
        {
            dsPONo = GetPOData();
            if (dsPONo.Tables.Count > 0 && dsPONo.Tables[0].Rows.Count > 0)
            {
                lblPOMsg.Visible = false;
                lblPOMsg.Text = string.Empty;
                gvPODetail.DataSource = dsPONo.Tables[0];
                gvPODetail.DataBind();
            }
            else
            {
                lblPOMsg.Visible = true;
                lblPOMsg.Text = "No data found!";
                gvPODetail.DataSource = null;
                gvPODetail.DataBind();
            }
            lblPORecords.Text = "Records[" + gvPODetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }



    private DataTable GetPONewPivotGroupData()
    {
        try
        {
            DataTable dtPV = new DataTable();
            dtPV.Columns.Add("NEW_PIVOT_GROUP", typeof(string));
            dtPV.Columns.Add("NEW_PIVOT_GROUP_DESC", typeof(string));

            DataTable dtPVNew = new DataTable();
            dtPVNew.Columns.Add("NEW_PIVOT_GROUP", typeof(string));
            dtPVNew.Columns.Add("NEW_PIVOT_GROUP_DESC", typeof(string));
            dtPVNew.Columns.Add("SALE_ESTIMATE_BUDGET", typeof(string));
            dtPVNew.Columns.Add("LIKELY_DELIVERY_DATE", typeof(string));
            dtPVNew.Columns.Add("PLAN_DATE_OF_PROCUREMENT", typeof(string));

            int unitID = 0;
            string jobNo = string.Empty;
            string pivotGroup = string.Empty;
            string pivotGroupDesc = string.Empty;

            unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text.ToUpper().Trim();

            if (!string.IsNullOrEmpty(txtPivotGroupSearch.Text))
                pivotGroup = txtPivotGroupSearch.Text;

            if (!string.IsNullOrEmpty(txtPivotGroupDescSearch.Text))
                pivotGroupDesc = txtPivotGroupDescSearch.Text;

            string pivotGroupText = string.Empty;
            string pivotGroupDescText = string.Empty;

            dsNewPivotGroup = objReports.GetNewPivotGroupForPO(unitID, jobNo, pivotGroup, pivotGroupDesc);

            if (dsNewPivotGroup.Tables.Count > 0 && dsNewPivotGroup.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsNewPivotGroup.Tables[0].Rows[0];
                string[] strPVComp = Convert.ToString(dr["PIVOT_GROUP"]).Split(';');

                if (strPVComp.Length > 0)
                {
                    foreach (string sr in strPVComp)
                    {
                        if (!string.IsNullOrEmpty(sr))
                        {
                            pivotGroupText = string.Empty;
                            pivotGroupDescText = string.Empty;

                            string[] strPV = sr.Split('$');
                            if (strPV.Length > 0)
                            {
                                pivotGroupDescText = sr.Trim().Replace("$", " - ");

                                if (!string.IsNullOrEmpty(Convert.ToString(strPV[0])))
                                    pivotGroupText = Convert.ToString(strPV[0]).Trim();
                            }
                        }

                        DataRow drn = dtPV.NewRow();
                        drn["NEW_PIVOT_GROUP"] = pivotGroupText;
                        drn["NEW_PIVOT_GROUP_DESC"] = pivotGroupDescText;

                        dtPV.Rows.Add(drn);
                    }
                }


                if (dtPV.Rows.Count > 0)
                {

                    if (dsNewPivotGroup.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dsNewPivotGroup.Tables[1].Rows)
                        {
                            foreach (DataRow dr2 in dtPV.Select("NEW_PIVOT_GROUP='" + Convert.ToString(dr1["PIVOT_GROUP"]) + "'"))
                            {
                                DataRow drc = dtPVNew.NewRow();
                                drc["NEW_PIVOT_GROUP"] = Convert.ToString(dr1["PIVOT_GROUP"]);
                                drc["NEW_PIVOT_GROUP_DESC"] = Convert.ToString(dr1["PIVOT_GROUP_DESC"]);
                                drc["LIKELY_DELIVERY_DATE"] = Convert.ToString(dr1["LIKELY_DELIVERY_DATE"]);
                                drc["PLAN_DATE_OF_PROCUREMENT"] = Convert.ToString(dr1["PLAN_DATE_OF_PROCUREMENT"]);

                                dtPVNew.Rows.Add(drc);
                            }
                        }
                    }

                    if (dtPVNew.Rows.Count > 0)
                    {
                        if (dsNewPivotGroup.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dtPVNew.Rows)
                            {
                                foreach (DataRow dr2 in dsNewPivotGroup.Tables[2].Rows)
                                {
                                    string saleEstimateBudget = string.Empty;
                                    if (Convert.ToString(dr1["NEW_PIVOT_GROUP"]) == Convert.ToString(dr2["PIVOT_GROUP"]) &&
                                        Convert.ToString(txtJOBNo.Text) == Convert.ToString(dr2["JOB_NO"]))
                                    {
                                        dr1["SALE_ESTIMATE_BUDGET"] = Convert.ToString(dr2["SALE_ESTIMATE_BUDGET"]);
                                    }
                                }
                            }
                        }


                        return dtPVNew;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
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
    private void GetPONewPivotGroupDetail()
    {
        try
        {
            DataTable dt = new DataTable();

            dt = GetPONewPivotGroupData();
            if (dt.Rows.Count > 0)
            {
                lblPONewPGMsg.Visible = false;
                lblPONewPGMsg.Text = string.Empty;
                gvPONewPGDetail.DataSource = dt;
                gvPONewPGDetail.DataBind();
            }
            else
            {
                lblPONewPGMsg.Visible = true;
                lblPONewPGMsg.Text = "No data found!";
                gvPONewPGDetail.DataSource = null;
                gvPONewPGDetail.DataBind();
            }
            lblPONewPGRecords.Text = "Records[" + gvPONewPGDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }


    private void SavePivotGroup()
    {
        try
        {
            int isPostingFlag = 0;
            int isPivotGroupLogFlag = 1;
            int unitID = 0;
            string jobNo = string.Empty;
            string poNo = string.Empty;
            string oldPV = string.Empty;
            string oldPVDesc = string.Empty;
            string newPV = string.Empty;
            string newPVDesc = string.Empty;


            string postingMonth = string.Empty;
            int postingYear = 0;
            double poBudget = 0;
            double saleEstimateBudget = 0;
            double poValueINR = 0;
            double deltaAsPerPOBudget = 0;
            double deltaAsPerSaleEstimateBudget = 0;
            double pendingCost = 0;
            double savingCost = 0;
            double savingOfOriginalEstimatePercentage = 0;
            string likelyDeliveryDate = string.Empty;
            string plannedDateOfProcurement = string.Empty;

            unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtPONo.Text))
                poNo = txtPONo.Text.Trim();

            if (!string.IsNullOrEmpty(txtOldPivotGroup.Text))
                oldPV = txtOldPivotGroup.Text.Trim();

            if (!string.IsNullOrEmpty(txtOldPivotGroupDesc.Text))
                oldPVDesc = txtOldPivotGroupDesc.Text.Trim();

            if (!string.IsNullOrEmpty(txtNewPivotGroup.Text))
                newPV = txtNewPivotGroup.Text.Trim();

            if (!string.IsNullOrEmpty(txtNewPivotGroupDesc.Text))
                newPVDesc = txtNewPivotGroupDesc.Text.Trim();

            postingMonth = string.Empty;
            postingYear = 0;

            int value = 0;


            value = objReports.InsertPOPivotGroupLogOne(unitID, jobNo, poNo, newPV, newPVDesc, oldPV, oldPVDesc,
                                                    isPostingFlag,
                                                    isPivotGroupLogFlag,
                                                    postingMonth,
                                                    postingYear,
                                                    poBudget,
                                                    saleEstimateBudget,
                                                    poValueINR,
                                                    deltaAsPerPOBudget,
                                                    deltaAsPerSaleEstimateBudget,
                                                    pendingCost,
                                                    savingCost,
                                                    savingOfOriginalEstimatePercentage,
                                                    likelyDeliveryDate,
                                                    plannedDateOfProcurement,
                                                    Convert.ToInt32(Session["EMP_RECORD_ID"]));


            if (value > 0)
            {
                SuccessMessage("Log pivot group updated successfully...!!!");
                Reset();
                return;
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

    //private void SavePivotGroup()
    //{
    //    try
    //    {
    //        int unitID = 0;
    //        string jobNo = string.Empty;
    //        string poNo = string.Empty;
    //        string oldPV = string.Empty;
    //        string oldPVDesc = string.Empty;
    //        string newPV = string.Empty;
    //        string newPVDesc = string.Empty;

    //        unitID = Convert.ToInt32(ddlUnit.SelectedValue);

    //        if (!string.IsNullOrEmpty(txtJOBNo.Text))
    //            jobNo = txtJOBNo.Text.Trim().ToUpper();

    //        if (!string.IsNullOrEmpty(txtPONo.Text))
    //            poNo = txtPONo.Text.Trim();

    //        if (!string.IsNullOrEmpty(txtOldPivotGroup.Text))
    //            oldPV = txtOldPivotGroup.Text.Trim();

    //        if (!string.IsNullOrEmpty(txtOldPivotGroupDesc.Text))
    //            oldPVDesc = txtOldPivotGroupDesc.Text.Trim();

    //        if (!string.IsNullOrEmpty(txtNewPivotGroup.Text))
    //            newPV = txtNewPivotGroup.Text.Trim();

    //        if (!string.IsNullOrEmpty(txtNewPivotGroupDesc.Text))
    //            newPVDesc = txtNewPivotGroupDesc.Text.Trim();

    //        int value = objReports.InsertPOPivotGroupLog(unitID, jobNo, poNo, newPV, newPVDesc, oldPV, oldPVDesc, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //        if (value > 0)
    //        {
    //            SuccessMessage("Log pivot group updated successfully...!!!");
    //            Reset();
    //            return;
    //        }
    //        else
    //        {
    //            ExceptionMessage("Please try again...!!!");
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    private void Reset()
    {
        try
        {
            txtPONo.Text = string.Empty;
            txtPONoSearch.Text = string.Empty;
            txtPivotGroupSearch.Text = string.Empty;
            txtPivotGroupDescSearch.Text = string.Empty;
            txtOldPivotGroup.Text = string.Empty;
            txtOldPivotGroupDesc.Text = string.Empty;
            txtNewPivotGroup.Text = string.Empty;
            txtNewPivotGroupDesc.Text = string.Empty;

            gvPODetail.DataSource = null;
            gvPODetail.DataBind();

            gvPONewPGDetail.DataSource = null;
            gvPONewPGDetail.DataBind();
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
