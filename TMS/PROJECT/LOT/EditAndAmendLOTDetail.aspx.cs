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
using iTextSharp.tool.xml;

public partial class PROJECT_LOT_EditAndAmendLOTDetail : System.Web.UI.Page
{


    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    LOTHtmlForPDF objLOTHtmlForPDF = new LOTHtmlForPDF();

    DataSet dsDBDetails = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsJobNo = new DataSet();
    DataSet dsLOTTFDetails = new DataSet();
    DataSet dsFTRunningNo = new DataSet();
    DataTable dtTemp = new DataTable();
    DataTable dtSubitem = new DataTable();
    DataSet dsLOTfor = new DataSet();
    DataSet dsLOTCategoryForFactory = new DataSet();
    DataSet dsMailInfo = new DataSet();

    int amendmentFlag = 0;
    int newStatusID = 0;
    int LOTTFID = 0;
    int companyID = 0;
    string companyName = string.Empty;
    string jobNo = string.Empty;
    string productionNo = string.Empty;
    string customerName = string.Empty;
    string custCode = string.Empty;
    int runningNo = 0;
    string runningNoTxt = string.Empty;
    string TFNo = string.Empty;
    string poNo = string.Empty;
    string LOTDate = string.Empty;
    string itemName = string.Empty;
    string LOTMainItem = string.Empty;
    int LOTMainItemID = 0;
    string impNotes = string.Empty;

    string drawingFile1 = string.Empty;
    string drawingFile2 = string.Empty;
    string drawingFile3 = string.Empty;
    string drawingFile4 = string.Empty;

    int LOTFTSubitemID = 0;
    string description = string.Empty;
    string drawingNo = string.Empty;
    int revisionNo = 0;
    string categoryID = string.Empty;
    string category = string.Empty;
    int noOfCopies = 0;
    int insertSubitemFlag = 0;
    string updateSubitemQuery = string.Empty;
    string updateRemovedSubitemQuery = string.Empty;
    string removedLOTTFSubitemID = string.Empty;

    string dbName = string.Empty;

    string from = string.Empty;
    string fromName = string.Empty;
    string to = string.Empty;
    string toName = string.Empty;
    string cc = string.Empty;

    int amendmentCount = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToInt32(Request.QueryString["LOTTFID"]) > 0)
            {
                HidePanel();
                if (!IsPostBack)
                {
                    ViewState["vsRemovedLOTTFSubitemID"] = null;
                    Session["dtSubitem"] = null;
                    Session["PE_DETAILS"] = null;
                    Session["PM_DETAILS"] = null;

                    hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtDate.Text = hdDate.Value;

                    Session["DB_DETAILS"] = objCommon.GetDBDetails();
                    BindCompany();
                    BindLOTFor();
                    BindLOTCategoryForFactory();

                    if (Convert.ToInt32(Request.QueryString["amendmentFlag"]) > 0)
                    {
                        trPERemarks.Visible = true;
                        trPMRemarks.Visible = true;
                        trAmendmentRemarks.Visible = true;
                    }
                    else
                    {
                        trPERemarks.Visible = false;
                        trPMRemarks.Visible = false;
                        trAmendmentRemarks.Visible = false;
                    }

                    BindLOTTFDetails();


                }
            }
            else
                Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }


    // JOB DETAILS
    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        modalPopupExtenderJOBDetail.Show();
        GetJOBDetail();
    }

    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        modalPopupExtenderJOBDetail.Show();
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
                Label lblPONo = gvJOBDetail.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblCustomerName = gvJOBDetail.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblCustCode = gvJOBDetail.Rows[rowindex].FindControl("lblCustCode") as Label;

                txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
                txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                txtCustomerCode.Text = Convert.ToString(lblCustCode.Text).Trim();
                txtPONo.Text = Convert.ToString(lblPONo.Text).Trim();

                GetTFNo();
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

    protected void btnGetTFno_Click(object sender, EventArgs e)
    {
        GetTFNo();
    }


    //ADD UPDATE SUBITEMS
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddSubitems();
    }

    protected void gvSubItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                string txt = string.Empty;
                string categoryTxt = string.Empty;

                Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
                Label lblCategory = (Label)e.Row.FindControl("lblCategory");


                if (!string.IsNullOrEmpty(lblCategoryID.Text))
                {
                    string[] srtCategoryID = lblCategoryID.Text.Split(',');
                    foreach (string i in srtCategoryID)
                    {
                        if (Convert.ToInt32(i) > 0)
                        {
                            if (Convert.ToInt32(i) == 1)
                                txt = "Fabrication";
                            else if (Convert.ToInt32(i) == 2)
                                txt = "Inspection";
                            else if (Convert.ToInt32(i) == 3)
                                txt = "Information";
                        }

                        categoryTxt += txt + ",";
                    }
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    categoryTxt = categoryTxt.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    lblCategory.Text = categoryTxt;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvSubItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES" || e.CommandArgument == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblSrNo = gvSubItem.Rows[rowindex].FindControl("lblSrNo") as Label;
                Label lblDescription = gvSubItem.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblDrgOrDOCNo = gvSubItem.Rows[rowindex].FindControl("lblDrgOrDOCNo") as Label;
                Label lblRevNo = gvSubItem.Rows[rowindex].FindControl("lblRevNo") as Label;
                Label lblCategoryID = gvSubItem.Rows[rowindex].FindControl("lblCategoryID") as Label;
                Label lblCategory = gvSubItem.Rows[rowindex].FindControl("lblCategory") as Label;
                Label lblNoOfCopies = gvSubItem.Rows[rowindex].FindControl("lblNoOfCopies") as Label;


                foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryTEdit.Items)
                {
                    item.Selected = false;
                }

                if (e.CommandArgument == "PROPERTIES")
                {
                    hdSRNo.Value = lblSrNo.Text;
                    txtDescriptionToEdit.Text = lblDescription.Text;
                    txtDrgOrDOCNoToEdit.Text = lblDrgOrDOCNo.Text;
                    txtRevNoToEdit.Text = lblRevNo.Text;

                    string[] str = lblCategoryID.Text.Split(',');
                    foreach (string item in str)
                    {
                        chkLstCategoryTEdit.Items[Convert.ToInt32(item) - 1].Selected = true;
                    }
                    txtNoOfCopiesToEdit.Text = lblNoOfCopies.Text;
                    modalPopupExtenderSubItemDetail.Show();
                }



                #region REMOVE


                if (e.CommandArgument == "REMOVE")
                {
                    if (Session["dtSubitem"] != null)
                    {
                        dtTemp = (DataTable)Session["dtSubitem"];
                    }

                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Select("SR_NO='" + lblSrNo.Text + "'"))
                        {
                            ViewState["vsRemovedLOTTFSubitemID"] += Convert.ToString(dr["LOT_TF_SUBITEM_ID"]) + ",";
                            dtTemp.Rows.Remove(dr);
                        }
                    }

                    if (dtTemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTemp.Rows.Count; i++)
                        {
                            dtTemp.Rows[i]["SR_NO"] = i + 1;
                        }
                    }

                    gvSubItem.DataSource = dtTemp;
                    gvSubItem.DataBind();

                    lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";
                }

                #endregion




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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateSubitems();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateLOTTransmittalToFactory();
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
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                //ddlCompany.Items.Insert(0, "Select");

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTFor()
    {
        try
        {
            dsLOTfor = objProject.GetLotMainItems();
            if (dsLOTfor.Tables.Count > 0 && dsLOTfor.Tables[0].Rows.Count > 0)
            {
                ddlLOTFor.DataSource = dsLOTfor.Tables[0];
                ddlLOTFor.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTFor.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTFor.DataBind();
                ddlLOTFor.Items.Insert(0, "Select");
                //ddlLOTFor.SelectedIndex = 0;
                ddlLOTFor.SelectedValue = "1";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTCategoryForFactory()
    {
        try
        {
            dsLOTCategoryForFactory = objProject.GetFactoryCategory();
            if (dsLOTCategoryForFactory.Tables.Count > 0 && dsLOTCategoryForFactory.Tables[0].Rows.Count > 0)
            {
                chkLstCategory.DataSource = dsLOTCategoryForFactory.Tables[0];
                chkLstCategory.DataTextField = "LOT_CATEGORY_NAME";
                chkLstCategory.DataValueField = "LOT_CATEGORY_ID";
                chkLstCategory.DataBind();

                chkLstCategoryTEdit.DataSource = dsLOTCategoryForFactory.Tables[0];
                chkLstCategoryTEdit.DataTextField = "LOT_CATEGORY_NAME";
                chkLstCategoryTEdit.DataValueField = "LOT_CATEGORY_ID";
                chkLstCategoryTEdit.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTTFDetails()
    {
        try
        {
            amendmentCount = 0;

            LOTTFID = 0;
            if (Convert.ToInt32(Request.QueryString["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(Request.QueryString["LOTTFID"]);

            dsLOTTFDetails = objProject.GetLOTTFDetailsOne(LOTTFID);
            if (dsLOTTFDetails.Tables.Count > 0)
            {
                if (dsLOTTFDetails.Tables[0].Rows.Count > 0)
                {
                    if (dsLOTTFDetails.Tables[0].Rows[0]["UNIT_ID"] != DBNull.Value)
                        ddlCompany.SelectedValue = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_ID"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["LOT_MAIN_ITEM_ID"] != DBNull.Value && Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["LOT_MAIN_ITEM_ID"]) > 0)
                        ddlLOTFor.SelectedValue = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["LOT_MAIN_ITEM_ID"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"])))
                        txtJOBNo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["PRODUCTION_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PRODUCTION_NO"])))
                        txtProductionNumber.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PRODUCTION_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"])))
                        txtCustomerName.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"])))
                        txtCustomerCode.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"])))
                        txtPONo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"])))
                        txtDate.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"])))
                        txtTFNo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"])))
                        txtItemName.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"]);



                    if (amendmentCount > 0)
                    {
                        if (dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_PE_APPROVED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_PE_APPROVED_REMARKS"])))
                            txtPERemarks.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_PE_APPROVED_REMARKS"]);

                        if (dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_PM_APPROVED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_PM_APPROVED_REMARKS"])))
                            txtPMRemarks.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_PM_APPROVED_REMARKS"]);
                    }
                    else
                    {
                        if (dsLOTTFDetails.Tables[0].Rows[0]["PE_APPROVED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PE_APPROVED_REMARKS"])))
                            txtPERemarks.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PE_APPROVED_REMARKS"]);

                        if (dsLOTTFDetails.Tables[0].Rows[0]["PM_APPROVED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PM_APPROVED_REMARKS"])))
                            txtPMRemarks.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PM_APPROVED_REMARKS"]);
                    }

                    if (dsLOTTFDetails.Tables[0].Rows[0]["AMENDMENT_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["AMENDMENT_REMARKS"])))
                        txtAmendmentRemarks.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["AMENDMENT_REMARKS"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"])))
                        txtNotes.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"]);
                }

                if (dsLOTTFDetails.Tables[1].Rows.Count > 0)
                {

                    gvSubItem.DataSource = dsLOTTFDetails.Tables[1];
                    gvSubItem.DataBind();
                    Session["dtSubitem"] = dsLOTTFDetails.Tables[1];
                }
                else
                {
                    gvSubItem.DataSource = null;
                    gvSubItem.DataBind();
                    Session["dtSubitem"] = null;
                }
                lblSubitemsRecords.Text = "Records[" + gvSubItem.Rows.Count + "]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetJOBDetail()
    {
        try
        {
            custCode = string.Empty;
            jobNo = string.Empty;
            poNo = string.Empty;



            if (!string.IsNullOrEmpty(txtCustomerCodeSearch.Text))
                custCode = txtCustomerCodeSearch.Text;
            else
                custCode = string.Empty;


            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtPONoSearch.Text))
                poNo = txtPONoSearch.Text;
            else
                poNo = string.Empty;

            dsJobNo = objProject.GetJOBDetailsForLOT(0, custCode, jobNo, poNo);
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsJobNo.Tables[0];
                gvJOBDetail.DataBind();
                Session["PE_DETAILS"] = dsJobNo.Tables[1];
                Session["PM_DETAILS"] = dsJobNo.Tables[2];

            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvJOBDetail.DataSource = null;
                gvJOBDetail.DataBind();
                Session["PE_DETAILS"] = null;
                Session["PM_DETAILS"] = null;
            }
            lblJOBRecords.Text = "Records[" + gvJOBDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void GetTFNo()
    {
        try
        {
            jobNo = string.Empty;
            companyID = 0;
            companyName = string.Empty;
            runningNo = 0;
            runningNoTxt = "0";
            TFNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
            {
                jobNo = txtJOBNo.Text;
                if (jobNo.IndexOf('.') > 0)
                    jobNo = jobNo.Substring(0, (jobNo.IndexOf('.')));
            }
            else
                jobNo = string.Empty;

            companyID = companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            companyName = ddlCompany.SelectedItem.Text;

            runningNo = objProject.GetFTRunningNo(jobNo, companyID);

            //if (dsFTRunningNo.Tables.Count > 0 && dsFTRunningNo.Tables[0].Rows.Count > 0)
            //{
            //    runningNo = Convert.ToInt32(dsFTRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
            //    //runningNoTxt = Convert.ToString(dsFTRunningNo.Tables[0].Rows[0]["RUNNING_NO"]);
            //}

            for (int i = 1; i <= (4 - (runningNoTxt.Length)); i++)
            {
                runningNoTxt += "0";
            }

            runningNoTxt = runningNoTxt + Convert.ToString(runningNo + 1);

            TFNo = jobNo + "-" + companyName + "-" + runningNoTxt;
            txtTFNo.Text = TFNo;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddSubitems()
    {
        try
        {
            if (dtTemp != null && Session["dtSubitem"] != null)
            {
                dtTemp = (DataTable)Session["dtSubitem"];
            }

            description = string.Empty;
            drawingNo = string.Empty;
            revisionNo = 0;
            categoryID = string.Empty;
            category = string.Empty;
            noOfCopies = 0;

            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = txtDescription.Text;

            if (!string.IsNullOrEmpty(txtDrgNo.Text))
                drawingNo = txtDrgNo.Text;

            if (!string.IsNullOrEmpty(txtRevNo.Text))
                revisionNo = Convert.ToInt32(txtRevNo.Text);


            foreach (System.Web.UI.WebControls.ListItem item in chkLstCategory.Items)
            {
                if (item.Selected)
                {
                    categoryID += item.Value + ",";
                    category += item.Text + ",";
                }
            }

            if (!string.IsNullOrEmpty(categoryID))
            {
                categoryID = categoryID.TrimEnd(',');
                category = category.TrimEnd(',');
            }

            if (!string.IsNullOrEmpty(txtNoOfCopies.Text))
                noOfCopies = Convert.ToInt32(txtNoOfCopies.Text);


            DataRow dr = dtTemp.NewRow();

            dr["SR_NO"] = Convert.ToString(gvSubItem.Rows.Count + 1);
            dr["LOT_TF_SUBITEM_ID"] = 0;
            dr["DESCRIPTION"] = description;
            dr["DRG_NO"] = drawingNo;
            dr["REV_NO"] = revisionNo;
            dr["CATEGORY_ID"] = categoryID;
            dr["CATEGORY"] = category;
            dr["NO_OF_COPIES"] = noOfCopies;

            dtTemp.Rows.Add(dr);

            gvSubItem.DataSource = dtTemp;
            gvSubItem.DataBind();
            lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";

            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateSubitems()
    {
        try
        {
            categoryID = string.Empty;
            category = string.Empty;

            if (Session["dtSubitem"] != null)
            {
                dtTemp = (DataTable)Session["dtSubitem"];
            }

            if (dtTemp.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTemp.Select("SR_NO='" + Convert.ToString(hdSRNo.Value) + "'"))
                {
                    dr["SR_NO"] = Convert.ToString(hdSRNo.Value);
                    dr["DESCRIPTION"] = Convert.ToString(txtDescriptionToEdit.Text);
                    dr["DRG_NO"] = Convert.ToString(txtDrgOrDOCNoToEdit.Text);
                    dr["REV_NO"] = Convert.ToString(txtRevNoToEdit.Text);

                    foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryTEdit.Items)
                    {
                        if (item.Selected)
                        {
                            categoryID += item.Value + ",";
                            category += item.Text + ",";
                        }
                    }

                    if (!string.IsNullOrEmpty(categoryID))
                    {
                        categoryID = categoryID.TrimEnd(',');
                        category = category.TrimEnd(',');
                    }

                    dr["CATEGORY_ID"] = categoryID;
                    dr["CATEGORY"] = category;
                    dr["NO_OF_COPIES"] = Convert.ToString(txtNoOfCopiesToEdit.Text);
                }
            }
            gvSubItem.DataSource = dtTemp;
            gvSubItem.DataBind();

            lblSubitemsRecords.Text = "Subitems Records[" + gvSubItem.Rows.Count + "]";
            hdSRNo.Value = "0";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateLOTTransmittalToFactory()
    {
        try
        {
            DataTable dtNew = new DataTable();

            dtSubitem.Columns.Add("DESCRIPTION", typeof(string));
            dtSubitem.Columns.Add("DRG_NO", typeof(string));
            dtSubitem.Columns.Add("REV_NO", typeof(int));
            dtSubitem.Columns.Add("CATEGORY_ID", typeof(string));
            dtSubitem.Columns.Add("NO_OF_COPIES", typeof(int));

            amendmentFlag = 0;
            newStatusID = 0;
            LOTTFID = 0;
            companyID = 0;
            companyName = string.Empty;
            jobNo = string.Empty;
            productionNo = string.Empty;
            customerName = string.Empty;
            custCode = string.Empty;
            TFNo = string.Empty;
            poNo = string.Empty;
            LOTDate = string.Empty;
            itemName = string.Empty;
            LOTMainItem = string.Empty;
            LOTMainItemID = 0;
            impNotes = string.Empty;

            drawingFile1 = string.Empty;
            drawingFile2 = string.Empty;
            drawingFile3 = string.Empty;
            drawingFile4 = string.Empty;

            LOTFTSubitemID = 0;
            description = string.Empty;
            drawingNo = string.Empty;
            revisionNo = 0;
            categoryID = string.Empty;
            category = string.Empty;
            noOfCopies = 0;

            insertSubitemFlag = 0;
            updateSubitemQuery = string.Empty;
            updateRemovedSubitemQuery = string.Empty;
            removedLOTTFSubitemID = string.Empty;

            if (Convert.ToInt32(Request.QueryString["amendmentFlag"]) > 0)
                amendmentFlag = Convert.ToInt32(Request.QueryString["amendmentFlag"]);

            if (Convert.ToInt32(Request.QueryString["newStatusID"]) > 0)
                newStatusID = Convert.ToInt32(Request.QueryString["newStatusID"]);

            if (Convert.ToInt32(Request.QueryString["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(Request.QueryString["LOTTFID"]);

            companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            companyName = Convert.ToString(ddlCompany.SelectedItem.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNo.Text)))
                jobNo = Convert.ToString(txtJOBNo.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtProductionNumber.Text)))
                productionNo = Convert.ToString(txtProductionNumber.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtCustomerName.Text)))
                customerName = Convert.ToString(txtCustomerName.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtCustomerCode.Text)))
                custCode = Convert.ToString(txtCustomerCode.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtTFNo.Text)))
                TFNo = Convert.ToString(txtTFNo.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(txtPONo.Text)))
                poNo = Convert.ToString(txtPONo.Text);

            if (!string.IsNullOrEmpty(Convert.ToString(hdDate.Value)))
                LOTDate = Convert.ToDateTime(hdDate.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(txtItemName.Text)))
                itemName = Convert.ToString(txtItemName.Text);

            if (Convert.ToInt32(ddlLOTFor.SelectedIndex) > 0)
            {
                LOTMainItem = Convert.ToString(ddlLOTFor.SelectedItem.Text);
                LOTMainItemID = Convert.ToInt32(ddlLOTFor.SelectedValue);
            }


            if (!string.IsNullOrEmpty(Convert.ToString(txtNotes.Text)))
                impNotes = Convert.ToString(txtNotes.Text);


            if (uploadFileDrawing1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileDrawing1.PostedFile.FileName))
                    drawingFile1 = uploadFileDrawing1.PostedFile.FileName;
                else
                    drawingFile1 = string.Empty;
            }


            if (uploadFileDrawing2.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileDrawing2.PostedFile.FileName))
                    drawingFile2 = uploadFileDrawing2.PostedFile.FileName;
                else
                    drawingFile2 = string.Empty;
            }


            if (uploadFileDrawing3.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileDrawing3.PostedFile.FileName))
                    drawingFile3 = uploadFileDrawing3.PostedFile.FileName;
                else
                    drawingFile3 = string.Empty;
            }


            if (uploadFileDrawing4.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileDrawing4.PostedFile.FileName))
                    drawingFile4 = uploadFileDrawing4.PostedFile.FileName;
                else
                    drawingFile4 = string.Empty;
            }


            if (Session["dtSubitem"] != null)
            {
                dtNew = (DataTable)Session["dtSubitem"];
            }

            if (dtNew != null && dtNew.Rows.Count > 0)
            {
                foreach (DataRow dr in dtNew.Rows)
                {
                    if (dr["LOT_TF_SUBITEM_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["LOT_TF_SUBITEM_ID"])))
                        LOTFTSubitemID = Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]);

                    if (dr["DESCRIPTION"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DESCRIPTION"])))
                        description = Convert.ToString(dr["DESCRIPTION"]);

                    if (dr["DRG_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRG_NO"])))
                        drawingNo = Convert.ToString(dr["DRG_NO"]);

                    if (dr["REV_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["REV_NO"])))
                        revisionNo = Convert.ToInt32(dr["REV_NO"]);

                    if (dr["CATEGORY_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["CATEGORY_ID"])))
                        categoryID = Convert.ToString(dr["CATEGORY_ID"]);

                    if (dr["NO_OF_COPIES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["NO_OF_COPIES"])))
                        noOfCopies = Convert.ToInt32(dr["NO_OF_COPIES"]);


                    if (Convert.ToInt32(dr["LOT_TF_SUBITEM_ID"]) > 0)
                    {
                        updateSubitemQuery += "update tblLOTTransToFactorySubitem set SUBITEM_DESC='" + description + "',DRAWING_NO='" + drawingNo + "',REVISION_NO='" + revisionNo + "',CATEGORY='" + categoryID + "',COPIES='" + noOfCopies + "',MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",MODIFIED_ON=GETDATE() where LOT_TF_SUBITEM_ID=" + LOTFTSubitemID + ";" + Environment.NewLine;
                    }
                    else
                    {
                        DataRow dr1 = dtSubitem.NewRow();

                        dr1["DESCRIPTION"] = dr["DESCRIPTION"];
                        dr1["DRG_NO"] = dr["DRG_NO"];
                        dr1["REV_NO"] = dr["REV_NO"];
                        dr1["CATEGORY_ID"] = dr["CATEGORY_ID"];
                        dr1["NO_OF_COPIES"] = dr["NO_OF_COPIES"];

                        dtSubitem.Rows.Add(dr1);
                    }
                }
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["vsRemovedLOTTFSubitemID"])))
                removedLOTTFSubitemID = Convert.ToString(ViewState["vsRemovedLOTTFSubitemID"]).TrimEnd(',');

            if (!string.IsNullOrEmpty(removedLOTTFSubitemID))
                updateRemovedSubitemQuery = Environment.NewLine + "update tblLOTTransToFactorySubitem set IS_DELETED=1,MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",MODIFIED_ON=GETDATE() where LOT_TF_SUBITEM_ID in(" + removedLOTTFSubitemID + ");";

            if (!string.IsNullOrEmpty(Convert.ToString(updateRemovedSubitemQuery)))
                updateSubitemQuery = updateSubitemQuery + updateRemovedSubitemQuery;


            if (dtSubitem.Rows.Count > 0)
                insertSubitemFlag = 1;

            int value = objProject.UpdateLOTTransmittalToFactory(LOTTFID, companyID, jobNo, productionNo, custCode, TFNo, poNo, LOTDate, itemName, LOTMainItemID,
                                            impNotes, drawingFile1, drawingFile2, drawingFile3, drawingFile4, insertSubitemFlag,
                                            dtSubitem, updateSubitemQuery, amendmentFlag, newStatusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                int sendMailValue = SendEmail(value, amendmentFlag);

                if (sendMailValue > 0)
                {
                    int val = objProject.UpdateLOTMailStatusOne(value, 1, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (amendmentFlag > 0)
                        SuccessMessage("LOT amended with TF. No.: '" + TFNo + "' and mail sent successfully.");
                    else
                        SuccessMessage("LOT updated with TF. No.: '" + TFNo + "' and mail sent successfully.");
                }
                else
                {
                    if (amendmentFlag > 0)
                        SuccessMessage("LOT amended with TF. No.: '" + TFNo + "' successfully.");
                    else
                        SuccessMessage("LOT updated with TF. No.: '" + TFNo + "' successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    public byte[] GetPDFBytes(DataTable dtLOT, DataTable dtSubitems)
    {
        try
        {
            byte[] pdfBytes;
            var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

            string htmlText = string.Empty;
            StringBuilder sb = new StringBuilder();

            htmlText = objLOTHtmlForPDF.GetHtmlForPDF(dtLOT, dtSubitems);

            if (!string.IsNullOrEmpty(htmlText))
            {
                sb.Append("<html>\n");
                sb.Append("<body>\n");
                sb.Append(htmlText + "\n");
                sb.Append("</body>\n");
                sb.Append("</html>\n");
            }

            var html = sb.ToString();
            if (!string.IsNullOrEmpty(Convert.ToString(html)))
            {
                string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                img.Alignment = Element.ALIGN_LEFT;
                img.ScaleToFit(180f, 250f);


                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document(PageSize.A4);
                    var writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    document.Add(img);
                    using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                    {
                        using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                        }
                    }

                    document.Close();
                    pdfBytes = memoryStream.GetBuffer();

                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "drawing;filename=dd.pdf");
                    //Response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);
                    //Response.End();

                    return pdfBytes;
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

    private int SendEmail(int TFLOTID, int amendmentFlag)
    {
        try
        {
            string urlTxt = string.Empty;
            string href = string.Empty;
            string link = string.Empty;

            from = string.Empty;
            fromName = string.Empty;
            to = string.Empty;
            cc = string.Empty;

            dsMailInfo = objProject.GetJOBMailInfoOne(TFLOTID);
            if (dsMailInfo.Tables.Count > 0 && dsMailInfo.Tables[0].Rows.Count > 0)
            {
                if (dsMailInfo.Tables[0].Rows.Count > 0)
                {
                    TFNo = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["TF_NO"]);
                    from = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["CREATED_BY_EMAIL"]);
                    fromName = Convert.ToString(dsMailInfo.Tables[0].Rows[0]["CREATED_BY"]);
                }

                if (dsMailInfo.Tables[1].Rows.Count > 0)
                {
                    to = Convert.ToString(dsMailInfo.Tables[1].Rows[0]["PE_APPROVER_EMAIL_ID"]);
                    toName = Convert.ToString(dsMailInfo.Tables[1].Rows[0]["PE_NAME"]);

                    cc = Convert.ToString(dsMailInfo.Tables[1].Rows[0]["PM_APPROVER_EMAIL_ID"]);
                }
            }

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            MailMessage mail = new MailMessage();

            mail.Subject = TFNo + " Details";

            mail.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(to))
            {
                to = to.TrimEnd(';');
                string[] strTo = to.Split(';');
                foreach (string item in strTo)
                {
                    mail.To.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(cc))
            {
                cc = cc.TrimEnd(';');
                string[] strCC = cc.Split(';');
                foreach (string item in strCC)
                {
                    mail.CC.Add(item);
                }
            }

            mail.IsBodyHtml = true;
            string body = string.Empty;
            string fileName = string.Empty;

            if (amendmentFlag > 0)
            {
                fileName = "~/PROJECT/LOT/EMAIL_FORMATS/AmendedLOTPEApprovalMail.htm";
            }
            else
            {
                fileName = "~/PROJECT/LOT/EMAIL_FORMATS/LOTPEApprovalMail.htm";
            }

            using (StreamReader reader = new StreamReader(Server.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);
            link = "'" + urlTxt + "/Login.aspx?tfno=" + TFNo + "'";
            href = "<a href=" + link + ">Approve LOT</a>";

            body = body.Replace("{#creater#}", fromName);
            body = body.Replace("{#pename#}", toName);
            body = body.Replace("{#link#}", href);

            mail.Body = body;

            byte[] bytes = GetPDFBytes(dsMailInfo.Tables[0], dsMailInfo.Tables[3]);

            mail.Attachments.Add(new Attachment(new MemoryStream(bytes), TFNo + ".pdf"));

            try
            {
                if (!string.IsNullOrEmpty(to))
                {
                    SmtpServer.Send(mail);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    return 1;

                else
                    return 0;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    private void Reset()
    {
        try
        {
            ddlLOTFor.SelectedIndex = 0;
            txtCustomerCode.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtJOBNo.Text = string.Empty;
            txtPONo.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtTFNo.Text = string.Empty;

            gvJOBDetail.DataSource = null;
            Session["dtSubitem"] = null;

            dtTemp.Clear();
            dtSubitem.Clear();
            gvSubItem.DataSource = null;
            gvSubItem.DataBind();

            ResetSubitems();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ResetSubitems()
    {
        txtDescription.Text = string.Empty;
        txtDrgNo.Text = string.Empty;
        txtRevNo.Text = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in chkLstCategory.Items)
        {
            item.Selected = false;
        }
        txtNoOfCopies.Text = string.Empty;
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