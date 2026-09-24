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

public partial class PROJECT_MGMT_AddNewMR : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsProject = new DataSet();
    DataSet dsMRType = new DataSet();
    DataSet dsUnit = new DataSet();

    DataSet dsProductUnit = new DataSet();
    DataSet dsProduct = new DataSet();
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
                Session["productcode"] = null;
                Session["dt"] = null;
                BindProjectNo();
                BindMRType();
                BindUnit();

                ddlProductCode.Items.Insert(0, "Select");
                ddlProductCode.SelectedIndex = 0;

                hdExpectedPODate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtExpectedPODate.Text = Convert.ToString(hdExpectedPODate.Value);

                hdDeliveryRequiredBy.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDeliveryRequiredBy.Text = Convert.ToString(hdDeliveryRequiredBy.Value);
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
        BindNewMR();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HidePanel();
        SaveMR();
    }

    protected void ddlProjectNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        HidePanel();
        BindProductCode();
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        HidePanel();
        BindProductCode();
    }

    protected void gvMRList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT_MGMT/ProductList.aspx");
    }

    protected void gvMRList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HidePanel();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlGroup = (DropDownList)e.Row.FindControl("ddlGroup");
                DataSet dsGroupNew = new DataSet();
                dsGroupNew = (DataSet)Session["group"];

                if (dsGroupNew.Tables.Count > 0 && dsGroupNew.Tables[0].Rows.Count > 0)
                {
                    ddlGroup.DataSource = dsGroupNew.Tables[0];
                    ddlGroup.DataTextField = "GROUP_NAME";
                    ddlGroup.DataValueField = "GROUP_ID";
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, "Select");
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

    protected void gvMRList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblSerialNo = gvMRList.Rows[rowindex].FindControl("lblSerialNo") as Label;
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
            else
            {
                ddlProjectNo.Items.Clear();
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
                ddlUnit.Items.Insert(0, "All");
                ddlUnit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindProductCode()
    {
        try
        {
            string projectNo = string.Empty;
            int unitID = 0;

            if (ddlProjectNo.SelectedIndex > 0)
                projectNo = Convert.ToString(ddlProjectNo.SelectedValue);

            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            dsProduct = objProject.GetProductCode(projectNo, unitID);
            if (dsProduct.Tables.Count > 0)
            {
                if (dsProduct.Tables[0].Rows.Count > 0)
                {
                    Session["productcode"] = dsProduct.Tables[0];
                    ddlProductCode.DataSource = dsProduct.Tables[0];
                    ddlProductCode.DataTextField = "PRODUCT_CODE";
                    ddlProductCode.DataValueField = "PRODUCT_ID";
                    ddlProductCode.DataBind();
                    ddlProductCode.Items.Insert(0, "Select");
                    ddlProductCode.SelectedIndex = 0;
                }
                else
                {
                    Session["productcode"] = null;
                    ddlProductCode.Items.Clear();
                    ddlProductCode.Items.Insert(0, "Select");
                    ddlProductCode.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindMRType()
    {
        try
        {
            dsMRType = objProject.GetDetailsBySP("sp_get_mr_type");
            if (dsMRType.Tables.Count > 0 && dsMRType.Tables[0].Rows.Count > 0)
            {
                ddlMRType.DataSource = dsMRType.Tables[0];
                ddlMRType.DataTextField = "MR_TYPE";
                ddlMRType.DataValueField = "MR_TYPE_ID";
                ddlMRType.DataBind();
                ddlMRType.Items.Insert(0, "Select");
                ddlMRType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private string GetMRNo()
    {
        try
        {
            int unitID = 0;
            DataSet dsRunningNo = new DataSet();
            string mRNo = string.Empty;
            string unitC = string.Empty;
            string financialYearC = string.Empty;
            string mrTypeC = string.Empty;
            string runningNumberC = string.Empty;
            int runningNo = 0;
            string runningNoNew = string.Empty;
            if (ddlUnit.SelectedIndex > 0)
            {
                unitC = ddlUnit.SelectedItem.Text.Substring(0, 1);
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);
            }
            else
                unitC = string.Empty;

            string year = string.Empty;
            int month = 0;
            year = Convert.ToString(DateTime.Now.Year);
            month = Convert.ToInt32(DateTime.Now.Month);

            if (month >= 4)
                financialYearC = Convert.ToString(year.Substring(2, 2) + (Convert.ToInt32(year.Substring(2, 2)) + 1));
            else if (month < 4)
                financialYearC = Convert.ToString((Convert.ToInt32(year.Substring(2, 2)) - 1) + year.Substring(2, 2));

            if (ddlMRType.SelectedIndex > 0)
                mrTypeC = Convert.ToString(ddlMRType.SelectedValue);
            else
                mrTypeC = string.Empty;

            dsRunningNo = objProject.GetRunningNo(financialYearC, Convert.ToString(unitID + mrTypeC));

            if (dsRunningNo.Tables.Count > 0 && dsRunningNo.Tables.Count > 0)
            {
                if (dsRunningNo.Tables[0].Rows[0]["RUNNING_NO"] != DBNull.Value)
                    runningNo = Convert.ToInt32(dsRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
                else
                {
                    runningNo = 0;
                }
            }
            else
            {
                runningNo = 0;
            }

            if (gvMRList.Rows.Count > 0)
            {
                int currentRunningNo = 0;
                foreach (GridViewRow gr in gvMRList.Rows)
                {
                    Label lblMRNo = (Label)gr.FindControl("lblMRNo");
                    string grMRNo = lblMRNo.Text.Substring(0, 11);
                    if (grMRNo == ("MR/" + unitC + "/" + financialYearC + "/" + mrTypeC))
                    {
                        currentRunningNo = Convert.ToInt32(lblMRNo.Text.Substring(11, 4));
                    }
                }
                if (currentRunningNo > 0)
                    runningNo = currentRunningNo + 1;
                else
                    runningNo = runningNo + 1;
            }
            else
            {
                runningNo = runningNo + 1;
            }

            if (runningNo == 0)
            {
                runningNoNew = "0001";
            }
            if (runningNo > 0 && runningNo < 10)
            {
                runningNoNew = Convert.ToString("000" + runningNo);
            }
            else if (runningNo >= 10 && runningNo < 100)
            {
                runningNoNew = Convert.ToString("00" + runningNo);
            }
            else if (runningNo >= 100 && runningNo < 1000)
            {
                runningNoNew = Convert.ToString("0" + runningNo);
            }
            else if (runningNo >= 1000 && runningNo < 10000)
            {
                runningNoNew = Convert.ToString(runningNo);
            }

            mRNo = "MR/" + unitC + "/" + financialYearC + "/" + mrTypeC + runningNoNew;

            return mRNo;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }

    }

    private string GetFinancialYear()
    {
        try
        {
            string financialYearC = string.Empty;
            string year = string.Empty;
            int month = 0;
            year = Convert.ToString(DateTime.Now.Year);
            month = Convert.ToInt32(DateTime.Now.Month);

            if (month >= 4)
                financialYearC = Convert.ToString(year.Substring(2, 2) + (Convert.ToInt32(year.Substring(2, 2)) + 1));
            else if (month < 4)
                financialYearC = Convert.ToString((Convert.ToInt32(year.Substring(2, 2)) - 1) + year.Substring(2, 2));

            return financialYearC;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }

    }

    private void BindNewMR()
    {
        try
        {
            int count = 1;
            DataTable dt = new DataTable();
            if (gvMRList.Rows.Count > 0)
            {
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("SERIAL_NO", typeof(string));
                dtNew.Columns.Add("PRODUCT_ID", typeof(string));
                dtNew.Columns.Add("TYPE_ID", typeof(string));
                dtNew.Columns.Add("UNIT_ID", typeof(string));
                dtNew.Columns.Add("PRODUCT_CODE", typeof(string));
                dtNew.Columns.Add("DESCRIPTION", typeof(string));
                dtNew.Columns.Add("MR_NO", typeof(string));
                dtNew.Columns.Add("DOCUMENT_CLASS", typeof(string));
                dtNew.Columns.Add("UOM", typeof(string));
                dtNew.Columns.Add("QUANTITY", typeof(string));
                dtNew.Columns.Add("ADDITIONAL_DESCRIPTION", typeof(string));
                dtNew.Columns.Add("AV1", typeof(string));
                dtNew.Columns.Add("AV2", typeof(string));
                dtNew.Columns.Add("AV3", typeof(string));
                dtNew.Columns.Add("AV4", typeof(string));
                dtNew.Columns.Add("AV5", typeof(string));
                dtNew.Columns.Add("BUDGET", typeof(string));
                dtNew.Columns.Add("REMARKS", typeof(string));
                dtNew.Columns.Add("FINANCIAL_YEAR", typeof(string));

                foreach (GridViewRow gr in gvMRList.Rows)
                {
                    DataRow dr = dtNew.NewRow();
                    Label lblProductID = (Label)gr.FindControl("lblProductID");
                    Label lblTypeID = (Label)gr.FindControl("lblTypeID");
                    Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                    Label lblProductCode = (Label)gr.FindControl("lblProductCode");
                    Label lblDescription = (Label)gr.FindControl("lblDescription");
                    Label lblMRNo = (Label)gr.FindControl("lblMRNo");
                    TextBox txtDocumentClassG = (TextBox)gr.FindControl("txtDocumentClassG");
                    DropDownList ddlUOM = (DropDownList)gr.FindControl("ddlUOM");
                    TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                    TextBox txtAdditionalDescription = (TextBox)gr.FindControl("txtAdditionalDescription");
                    TextBox txtAV1 = (TextBox)gr.FindControl("txtAV1");
                    TextBox txtAV2 = (TextBox)gr.FindControl("txtAV2");
                    TextBox txtAV3 = (TextBox)gr.FindControl("txtAV3");
                    TextBox txtAV4 = (TextBox)gr.FindControl("txtAV4");
                    TextBox txtAV5 = (TextBox)gr.FindControl("txtAV5");
                    TextBox txtBudget = (TextBox)gr.FindControl("txtBudget");
                    TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");
                    Label lblFinancialYear = (Label)gr.FindControl("lblFinancialYear");

                    dr["SERIAL_NO"] = Convert.ToString(count++);

                    if (!string.IsNullOrEmpty(lblProductID.Text))
                        dr["PRODUCT_ID"] = lblProductID.Text;
                    else
                        dr["PRODUCT_ID"] = "0";

                    if (!string.IsNullOrEmpty(lblTypeID.Text))
                        dr["TYPE_ID"] = lblTypeID.Text;
                    else
                        dr["TYPE_ID"] = "0";

                    if (!string.IsNullOrEmpty(lblUnitID.Text))
                        dr["UNIT_ID"] = lblUnitID.Text;
                    else
                        dr["UNIT_ID"] = "0";

                    if (!string.IsNullOrEmpty(lblProductCode.Text))
                        dr["PRODUCT_CODE"] = lblProductCode.Text;
                    else
                        dr["PRODUCT_CODE"] = string.Empty;

                    if (!string.IsNullOrEmpty(lblDescription.Text))
                        dr["DESCRIPTION"] = lblDescription.Text;
                    else
                        dr["DESCRIPTION"] = string.Empty;

                    if (!string.IsNullOrEmpty(lblMRNo.Text))
                        dr["MR_NO"] = lblMRNo.Text;
                    else
                        dr["MR_NO"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtDocumentClassG.Text))
                        dr["DOCUMENT_CLASS"] = txtDocumentClassG.Text;
                    else
                        dr["DOCUMENT_CLASS"] = string.Empty;

                    if (ddlUOM.SelectedIndex > 0)
                        dr["UOM"] = Convert.ToString(ddlUOM.SelectedValue);
                    else
                        dr["UOM"] = "0";

                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                        dr["QUANTITY"] = txtQuantity.Text;
                    else
                        dr["QUANTITY"] = "0";

                    if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                        dr["ADDITIONAL_DESCRIPTION"] = txtAdditionalDescription.Text;
                    else
                        dr["ADDITIONAL_DESCRIPTION"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtAV1.Text))
                        dr["AV1"] = txtAV1.Text;
                    else
                        dr["AV1"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtAV2.Text))
                        dr["AV2"] = txtAV2.Text;
                    else
                        dr["AV2"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtAV3.Text))
                        dr["AV3"] = txtAV3.Text;
                    else
                        dr["AV3"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtAV4.Text))
                        dr["AV4"] = txtAV4.Text;
                    else
                        dr["AV4"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtAV5.Text))
                        dr["AV5"] = txtAV5.Text;
                    else
                        dr["AV5"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtBudget.Text))
                        dr["BUDGET"] = txtBudget.Text;
                    else
                        dr["BUDGET"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtRemarks.Text))
                        dr["REMARKS"] = txtRemarks.Text;
                    else
                        dr["REMARKS"] = string.Empty;

                    if (!string.IsNullOrEmpty(lblFinancialYear.Text))
                        dr["FINANCIAL_YEAR"] = lblFinancialYear.Text;
                    else
                        dr["FINANCIAL_YEAR"] = string.Empty;

                    dtNew.Rows.Add(dr);
                }

                string mRNo = string.Empty;
                string financialYear = string.Empty;
                mRNo = GetMRNo();
                financialYear = GetFinancialYear();
                DataRow drNewRow = dtNew.NewRow();

                drNewRow["SERIAL_NO"] = Convert.ToString(gvMRList.Rows.Count + 1);

                if (ddlProductCode.SelectedIndex > 0)
                {
                    drNewRow["PRODUCT_ID"] = Convert.ToString(ddlProductCode.SelectedValue);
                    drNewRow["PRODUCT_CODE"] = Convert.ToString(ddlProductCode.SelectedItem.Text);
                }
                else
                {
                    drNewRow["PRODUCT_ID"] = "0";
                    drNewRow["PRODUCT_CODE"] = string.Empty;
                }

                if (Session["productcode"] != null)
                {
                    DataTable dtproductdesc = new DataTable();
                    dtproductdesc = (DataTable)Session["productcode"];
                    foreach (DataRow drproductdesc in dtproductdesc.Select("PRODUCT_ID='" + ddlProductCode.SelectedValue + "'"))
                    {
                        drNewRow["DESCRIPTION"] = Convert.ToString(drproductdesc["DESCRIPTION"]);
                    }
                }
                else
                    drNewRow["DESCRIPTION"] = string.Empty;

                if (ddlMRType.SelectedIndex > 0)
                    drNewRow["TYPE_ID"] = Convert.ToString(ddlMRType.SelectedValue);
                else
                    drNewRow["TYPE_ID"] = string.Empty;

                if (ddlUnit.SelectedIndex > 0)
                    drNewRow["UNIT_ID"] = Convert.ToString(ddlUnit.SelectedValue);
                else
                    drNewRow["UNIT_ID"] = string.Empty;


                if (!string.IsNullOrEmpty(mRNo))
                    drNewRow["MR_NO"] = mRNo;
                else
                    drNewRow["MR_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(txtDocumentClass.Text))
                    drNewRow["DOCUMENT_CLASS"] = txtDocumentClass.Text;
                else
                    drNewRow["DOCUMENT_CLASS"] = string.Empty;


                drNewRow["UOM"] = "0";
                drNewRow["QUANTITY"] = "0";
                drNewRow["ADDITIONAL_DESCRIPTION"] = string.Empty;
                drNewRow["AV1"] = string.Empty;
                drNewRow["AV2"] = string.Empty;
                drNewRow["AV3"] = string.Empty;
                drNewRow["AV4"] = string.Empty;
                drNewRow["AV5"] = string.Empty;
                drNewRow["BUDGET"] = string.Empty;
                drNewRow["REMARKS"] = string.Empty;


                if (!string.IsNullOrEmpty(financialYear))
                    drNewRow["FINANCIAL_YEAR"] = financialYear;
                else
                    drNewRow["FINANCIAL_YEAR"] = string.Empty;

                dtNew.Rows.Add(drNewRow);
                dt = dtNew;
            }
            else
            {
                string mRNo = string.Empty;
                string financialYear = string.Empty;
                mRNo = GetMRNo();
                financialYear = GetFinancialYear();

                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("SERIAL_NO", typeof(string));
                dtTemp.Columns.Add("PRODUCT_ID", typeof(string));
                dtTemp.Columns.Add("TYPE_ID", typeof(string));
                dtTemp.Columns.Add("UNIT_ID", typeof(string));
                dtTemp.Columns.Add("PRODUCT_CODE", typeof(string));
                dtTemp.Columns.Add("DESCRIPTION", typeof(string));
                dtTemp.Columns.Add("MR_NO", typeof(string));
                dtTemp.Columns.Add("DOCUMENT_CLASS", typeof(string));
                dtTemp.Columns.Add("UOM", typeof(string));
                dtTemp.Columns.Add("QUANTITY", typeof(string));
                dtTemp.Columns.Add("ADDITIONAL_DESCRIPTION", typeof(string));
                dtTemp.Columns.Add("AV1", typeof(string));
                dtTemp.Columns.Add("AV2", typeof(string));
                dtTemp.Columns.Add("AV3", typeof(string));
                dtTemp.Columns.Add("AV4", typeof(string));
                dtTemp.Columns.Add("AV5", typeof(string));
                dtTemp.Columns.Add("BUDGET", typeof(string));
                dtTemp.Columns.Add("REMARKS", typeof(string));
                dtTemp.Columns.Add("FINANCIAL_YEAR", typeof(string));


                DataRow drTemp = dtTemp.NewRow();

                drTemp["SERIAL_NO"] = "1";

                if (ddlProductCode.SelectedIndex > 0)
                {
                    drTemp["PRODUCT_ID"] = Convert.ToString(ddlProductCode.SelectedValue);
                    drTemp["PRODUCT_CODE"] = Convert.ToString(ddlProductCode.SelectedItem.Text);
                }
                else
                {
                    drTemp["PRODUCT_ID"] = "0";
                    drTemp["PRODUCT_CODE"] = string.Empty;
                }

                if (Session["productcode"] != null)
                {
                    DataTable dtproductdesc = new DataTable();
                    dtproductdesc = (DataTable)Session["productcode"];
                    foreach (DataRow drproductdesc in dtproductdesc.Select("PRODUCT_ID='" + ddlProductCode.SelectedValue + "'"))
                    {
                        drTemp["DESCRIPTION"] = Convert.ToString(drproductdesc["DESCRIPTION"]);
                    }
                }
                else
                    drTemp["DESCRIPTION"] = string.Empty;

                if (!string.IsNullOrEmpty(mRNo))
                    drTemp["MR_NO"] = mRNo;
                else
                    drTemp["MR_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(txtDocumentClass.Text))
                    drTemp["DOCUMENT_CLASS"] = txtDocumentClass.Text;
                else
                    drTemp["DOCUMENT_CLASS"] = string.Empty;

                if (ddlMRType.SelectedIndex > 0)
                    drTemp["TYPE_ID"] = Convert.ToString(ddlMRType.SelectedValue);
                else
                    drTemp["TYPE_ID"] = string.Empty;

                if (ddlUnit.SelectedIndex > 0)
                    drTemp["UNIT_ID"] = Convert.ToString(ddlUnit.SelectedValue);
                else
                    drTemp["UNIT_ID"] = string.Empty;

                drTemp["UOM"] = "0";
                drTemp["QUANTITY"] = "0";
                drTemp["ADDITIONAL_DESCRIPTION"] = string.Empty;
                drTemp["AV1"] = string.Empty;
                drTemp["AV2"] = string.Empty;
                drTemp["AV3"] = string.Empty;
                drTemp["AV4"] = string.Empty;
                drTemp["AV5"] = string.Empty;
                drTemp["BUDGET"] = string.Empty;
                drTemp["REMARKS"] = string.Empty;

                if (!string.IsNullOrEmpty(financialYear))
                    drTemp["FINANCIAL_YEAR"] = financialYear;
                else
                    drTemp["FINANCIAL_YEAR"] = string.Empty;

                dtTemp.Rows.Add(drTemp);
                dt = dtTemp;
            }

            if (dt.Rows.Count > 0)
            {
                Session["dt"] = dt;
                gvMRList.DataSource = dt;
                gvMRList.DataBind();

            }
            else
            {
                Session["dt"] = null;
                gvMRList.DataSource = null;
                gvMRList.DataBind();
            }

            lblRecords.Text = "[" + gvMRList.Rows.Count + "]";

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SaveMR()
    {
        try
        {
            bool chk = false;
            int serialNo = 0;
            int productID = 0;
            int typeID = 0;
            int unitID = 0;
            string mRNo = string.Empty;
            string documentClass = string.Empty;
            //string description = string.Empty;
            int uom = 0;
            double quantity = 0;
            string additionalDescription = string.Empty;
            string av1 = string.Empty;
            string av2 = string.Empty;
            string av3 = string.Empty;
            string av4 = string.Empty;
            string av5 = string.Empty;
            double budget = 0;
            string remarks = string.Empty;
            string financialYear = string.Empty;

            string expectedPODate = string.Empty;
            string deliveryRequiredBy = string.Empty;

            expectedPODate = Convert.ToDateTime(hdExpectedPODate.Value).ToString("yyyy-MM-dd");
            deliveryRequiredBy = Convert.ToDateTime(hdDeliveryRequiredBy.Value).ToString("yyyy-MM-dd");


            DataTable dt = (DataTable)Session["dt"];

            if (gvMRList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvMRList.Rows)
                {
                    chk = true;

                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblProductID = (Label)gr.FindControl("lblProductID");
                    Label lblTypeID = (Label)gr.FindControl("lblTypeID");
                    Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                    Label lblProductCode = (Label)gr.FindControl("lblProductCode");
                    Label lblMRNo = (Label)gr.FindControl("lblMRNo");
                    TextBox txtDocumentClassG = (TextBox)gr.FindControl("txtDocumentClassG");
                    Label lblDescription = (Label)gr.FindControl("lblDescription");
                    DropDownList ddlUOM = (DropDownList)gr.FindControl("ddlUOM");
                    TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                    TextBox txtAdditionalDescription = (TextBox)gr.FindControl("txtAdditionalDescription");
                    TextBox txtAV1 = (TextBox)gr.FindControl("txtAV1");
                    TextBox txtAV2 = (TextBox)gr.FindControl("txtAV2");
                    TextBox txtAV3 = (TextBox)gr.FindControl("txtAV3");
                    TextBox txtAV4 = (TextBox)gr.FindControl("txtAV4");
                    TextBox txtAV5 = (TextBox)gr.FindControl("txtAV5");
                    TextBox txtBudget = (TextBox)gr.FindControl("txtBudget");
                    TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");
                    Label lblFinancialYear = (Label)gr.FindControl("lblFinancialYear");

                    serialNo = Convert.ToInt32(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(lblProductID.Text))
                        productID = Convert.ToInt32(lblProductID.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Product code is empty..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(lblTypeID.Text))
                        typeID = Convert.ToInt32(lblTypeID.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("MR code is empty..!");
                        return;
                    }


                    if (!string.IsNullOrEmpty(lblUnitID.Text))
                        unitID = Convert.ToInt32(lblUnitID.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("MR code is empty..!");
                        return;
                    }


                    if (!string.IsNullOrEmpty(lblMRNo.Text))
                        mRNo = lblMRNo.Text.Trim();
                    else
                    {
                        chk = false;
                        ExceptionMessage("MR No. is empty..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtDocumentClassG.Text))
                        documentClass = txtDocumentClassG.Text.Trim();
                    else
                    {
                        chk = false;
                        ExceptionMessage("please enter document class..!");
                        return;
                    }

                    //if (!string.IsNullOrEmpty(lblDescription.Text))
                    //    description = lblDescription.Text;
                    //else
                    //    description = string.Empty;

                    if (ddlUOM.SelectedIndex > 0)
                        uom = Convert.ToInt32(ddlUOM.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select unit or measurement..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtQuantity.Text) && Convert.ToDouble(txtQuantity.Text) > 0)
                        quantity = Convert.ToDouble(txtQuantity.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter quantity..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                        additionalDescription = txtAdditionalDescription.Text;
                    else
                        additionalDescription = string.Empty;


                    if (!string.IsNullOrEmpty(txtAV1.Text))
                        av1 = txtAV1.Text;
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter AV1..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtAV2.Text))
                        av2 = txtAV2.Text;
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter AV2..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtAV3.Text))
                        av3 = txtAV3.Text;
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter AV3..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtAV4.Text))
                        av4 = txtAV4.Text;
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter AV4..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtAV5.Text))
                        av5 = txtAV5.Text;
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter AV5..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtBudget.Text) && Convert.ToDouble(txtBudget.Text) > 0)
                        budget = Convert.ToDouble(txtBudget.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter budget..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtRemarks.Text))
                        remarks = txtRemarks.Text;
                    else
                        remarks = string.Empty;

                    if (!string.IsNullOrEmpty(lblFinancialYear.Text))
                        financialYear = lblFinancialYear.Text;
                    else
                        financialYear = string.Empty;

                    if (chk)
                    {
                        int value = objProject.AddUpdateNewMR(0, productID, typeID, unitID, mRNo, documentClass, "", uom, quantity,
                                                             additionalDescription, av1, av2, av3, av4, av5, budget,
                                                             remarks, expectedPODate, deliveryRequiredBy, financialYear,
                                                             Convert.ToInt32(Session["EMP_RECORD_ID"]));
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
            dtNew.Columns.Add("PRODUCT_ID", typeof(string));
            dtNew.Columns.Add("TYPE_ID", typeof(string));
            dtNew.Columns.Add("UNIT_ID", typeof(string));
            dtNew.Columns.Add("PRODUCT_CODE", typeof(string));
            dtNew.Columns.Add("DESCRIPTION", typeof(string));
            dtNew.Columns.Add("MR_NO", typeof(string));
            dtNew.Columns.Add("DOCUMENT_CLASS", typeof(string));
            dtNew.Columns.Add("UOM", typeof(string));
            dtNew.Columns.Add("QUANTITY", typeof(string));
            dtNew.Columns.Add("ADDITIONAL_DESCRIPTION", typeof(string));
            dtNew.Columns.Add("AV1", typeof(string));
            dtNew.Columns.Add("AV2", typeof(string));
            dtNew.Columns.Add("AV3", typeof(string));
            dtNew.Columns.Add("AV4", typeof(string));
            dtNew.Columns.Add("AV5", typeof(string));
            dtNew.Columns.Add("BUDGET", typeof(string));
            dtNew.Columns.Add("REMARKS", typeof(string));
            dtNew.Columns.Add("FINANCIAL_YEAR", typeof(string));


            foreach (GridViewRow gr in gvMRList.Rows)
            {
                DataRow dr = dtNew.NewRow();
                Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                Label lblProductID = (Label)gr.FindControl("lblProductID");
                Label lblTypeID = (Label)gr.FindControl("lblTypeID");
                Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                Label lblProductCode = (Label)gr.FindControl("lblProductCode");
                Label lblDescription = (Label)gr.FindControl("lblDescription");
                Label lblMRNo = (Label)gr.FindControl("lblMRNo");
                TextBox txtDocumentClassG = (TextBox)gr.FindControl("txtDocumentClassG");
                DropDownList ddlUOM = (DropDownList)gr.FindControl("ddlUOM");
                TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                TextBox txtAdditionalDescription = (TextBox)gr.FindControl("txtAdditionalDescription");
                TextBox txtAV1 = (TextBox)gr.FindControl("txtAV1");
                TextBox txtAV2 = (TextBox)gr.FindControl("txtAV2");
                TextBox txtAV3 = (TextBox)gr.FindControl("txtAV3");
                TextBox txtAV4 = (TextBox)gr.FindControl("txtAV4");
                TextBox txtAV5 = (TextBox)gr.FindControl("txtAV5");
                TextBox txtBudget = (TextBox)gr.FindControl("txtBudget");
                TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");
                Label lblFinancialYear = (Label)gr.FindControl("lblFinancialYear");
                if (!string.IsNullOrEmpty(lblSerialNo.Text))
                    dr["SERIAL_NO"] = lblSerialNo.Text;
                else
                    dr["SERIAL_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblProductID.Text))
                    dr["PRODUCT_ID"] = lblProductID.Text;
                else
                    dr["PRODUCT_ID"] = "0";

                if (!string.IsNullOrEmpty(lblProductCode.Text))
                    dr["PRODUCT_CODE"] = lblProductCode.Text;
                else
                    dr["PRODUCT_CODE"] = string.Empty;

                if (!string.IsNullOrEmpty(lblDescription.Text))
                    dr["DESCRIPTION"] = lblDescription.Text;
                else
                    dr["DESCRIPTION"] = string.Empty;

                if (!string.IsNullOrEmpty(lblMRNo.Text))
                    dr["MR_NO"] = lblMRNo.Text;
                else
                    dr["MR_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(txtDocumentClass.Text))
                    dr["DOCUMENT_CLASS"] = txtDocumentClass.Text;
                else
                    dr["DOCUMENT_CLASS"] = string.Empty;

                if (!string.IsNullOrEmpty(lblTypeID.Text))
                    dr["TYPE_ID"] = lblTypeID.Text;
                else
                    dr["TYPE_ID"] = string.Empty;

                if (!string.IsNullOrEmpty(lblUnitID.Text))
                    dr["UNIT_ID"] = lblUnitID.Text;
                else
                    dr["UNIT_ID"] = string.Empty;

                if (ddlUOM.SelectedIndex > 0)
                    dr["UOM"] = Convert.ToString(ddlUOM.SelectedValue);
                else
                    dr["UOM"] = "0";

                if (!string.IsNullOrEmpty(txtQuantity.Text))
                    dr["QUANTITY"] = txtQuantity.Text;
                else
                    dr["QUANTITY"] = "0";

                if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                    dr["ADDITIONAL_DESCRIPTION"] = txtAdditionalDescription.Text;
                else
                    dr["ADDITIONAL_DESCRIPTION"] = string.Empty;

                if (!string.IsNullOrEmpty(txtAV1.Text))
                    dr["AV1"] = txtAV1.Text;
                else
                    dr["AV1"] = string.Empty;

                if (!string.IsNullOrEmpty(txtAV2.Text))
                    dr["AV2"] = txtAV2.Text;
                else
                    dr["AV2"] = string.Empty;

                if (!string.IsNullOrEmpty(txtAV3.Text))
                    dr["AV3"] = txtAV3.Text;
                else
                    dr["AV3"] = string.Empty;

                if (!string.IsNullOrEmpty(txtAV4.Text))
                    dr["AV4"] = txtAV4.Text;
                else
                    dr["AV4"] = string.Empty;

                if (!string.IsNullOrEmpty(txtAV5.Text))
                    dr["AV5"] = txtAV5.Text;
                else
                    dr["AV5"] = string.Empty;

                if (!string.IsNullOrEmpty(txtBudget.Text))
                    dr["BUDGET"] = txtBudget.Text;
                else
                    dr["BUDGET"] = string.Empty;

                if (!string.IsNullOrEmpty(txtRemarks.Text))
                    dr["REMARKS"] = txtRemarks.Text;
                else
                    dr["REMARKS"] = string.Empty;

                if (!string.IsNullOrEmpty(lblFinancialYear.Text))
                    dr["FINANCIAL_YEAR"] = lblFinancialYear.Text;
                else
                    dr["FINANCIAL_YEAR"] = string.Empty;

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
                    gvMRList.DataSource = dtNew;
                    gvMRList.DataBind();
                }
                else
                {
                    Session["dt"] = null;
                    gvMRList.DataSource = null;
                    gvMRList.DataBind();
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