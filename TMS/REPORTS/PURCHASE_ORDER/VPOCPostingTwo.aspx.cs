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

public partial class REPORTS_PURCHASE_ORDER_VPOCPostingTwo : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsReport = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsDBDetails = new DataSet();



    string PONo = string.Empty;
    string PODate = string.Empty;
    string DOCClass = string.Empty;
    string vendCode = string.Empty;
    string vendName = string.Empty;
    string POValueINR = string.Empty;
    string POStatus = string.Empty;
    string location = string.Empty;
    string itemCategory = string.Empty;
    string DOD = string.Empty;
    string likelyDOD = string.Empty;
    int months = 0;

    double totalAmount = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

                BindUnit();

                Session["DB_DETAILS"] = objCommon.GetDBDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblTotalAmount.Text = "0";
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetVPOCListForPosting();
    }

    protected void gvVPOCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                totalAmount += Convert.ToDouble(lblAmount.Text);
                lblTotalAmount.Text = Convert.ToString(totalAmount);
                lblTotalAmount.ForeColor = System.Drawing.Color.Green;

                int poMonth = 0;
                int DODMonth = 0;
                int poYear = 0;
                int DODYear = 0;
                int DODMonthsFromPODate = 0;

                                
                TextBox txtPODate = (TextBox)e.Row.FindControl("txtPODate");
                TextBox txtDOD = (TextBox)e.Row.FindControl("txtDOD");
                TextBox txtDODMonthsFromPODate = (TextBox)e.Row.FindControl("txtDODMonthsFromPODate");

                poMonth = Convert.ToDateTime(txtPODate.Text).Month;
                DODMonth = Convert.ToDateTime(txtDOD.Text).Month;
                poYear = Convert.ToDateTime(txtPODate.Text).Year;
                DODYear = Convert.ToDateTime(txtDOD.Text).Year;

                DODMonthsFromPODate = (DODYear - poYear) * 12 + (DODMonth - poMonth);
                txtDODMonthsFromPODate.Text = Convert.ToString(DODMonthsFromPODate + 1);




                int postingMonth = 0;
                int postingYear = 0;
                int postingMonthsFromPODate = 0;

                TextBox txtPostingMonth = (TextBox)e.Row.FindControl("txtPostingMonth");
                TextBox txtPostingMonthsFromPODate = (TextBox)e.Row.FindControl("txtPostingMonthsFromPODate");
                txtPostingMonth.Text = Convert.ToDateTime(hdPostingMonth.Value).ToString("MMM-yyyy");
                postingMonth = Convert.ToDateTime(txtPostingMonth.Text).Month;
                postingYear = Convert.ToDateTime(txtPostingMonth.Text).Year;
                postingMonthsFromPODate = (postingYear - poYear) * 12 + (postingMonth - poMonth);
                txtPostingMonthsFromPODate.Text = Convert.ToString(postingMonthsFromPODate + 1);




                TextBox txtMonthsBasedOponDOD = (TextBox)e.Row.FindControl("txtMonthsBasedOponDOD");
                if (DODMonthsFromPODate < postingMonthsFromPODate)
                {
                    txtMonthsBasedOponDOD.Text = Convert.ToString(DODMonthsFromPODate + 1);
                }
                else
                {
                    txtMonthsBasedOponDOD.Text = Convert.ToString(postingMonthsFromPODate + 1);
                }
                
                double poAmountINR = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(lblAmount.Text)) && Convert.ToDouble(lblAmount.Text) > 0)
                    poAmountINR = Convert.ToDouble(lblAmount.Text);
                else
                    poAmountINR = 0;

                Label lblDODCOGS = (Label)e.Row.FindControl("lblDODCOGS");
                lblDODCOGS.Text = Convert.ToString(Math.Round((poAmountINR * (Convert.ToInt32(txtPostingMonthsFromPODate.Text))) / (Convert.ToInt32(txtMonthsBasedOponDOD.Text)), 2));
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvVPOCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int value = 0;

            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "POST")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;

                    Label lblPONo = gvVPOCList.Rows[rowindex].FindControl("lblPONo") as Label;
                    Label lblPODate = gvVPOCList.Rows[rowindex].FindControl("lblPODate") as Label;
                    Label lblDOCClass = gvVPOCList.Rows[rowindex].FindControl("lblDOCClass") as Label;
                    Label lblVendorCode = gvVPOCList.Rows[rowindex].FindControl("lblVendorCode") as Label;
                    Label lblVendorName = gvVPOCList.Rows[rowindex].FindControl("lblVendorName") as Label;
                    Label lblPOValueINR = gvVPOCList.Rows[rowindex].FindControl("lblPOValueINR") as Label;
                    Label lblPOStatus = gvVPOCList.Rows[rowindex].FindControl("lblPOStatus") as Label;
                    Label lblLocation = gvVPOCList.Rows[rowindex].FindControl("lblLocation") as Label;
                    TextBox txtItemCategory = gvVPOCList.Rows[rowindex].FindControl("txtItemCategory") as TextBox;
                    TextBox txtDOD = gvVPOCList.Rows[rowindex].FindControl("txtDOD") as TextBox;
                    TextBox txtLikelyDOD = gvVPOCList.Rows[rowindex].FindControl("txtLikelyDOD") as TextBox;
                    TextBox txtMonths = gvVPOCList.Rows[rowindex].FindControl("txtMonths") as TextBox;

                    if (!string.IsNullOrEmpty(lblPONo.Text))
                        PONo = Convert.ToString(lblPONo.Text);
                    else
                        PONo = string.Empty;

                    if (!string.IsNullOrEmpty(lblPODate.Text))
                        PODate = Convert.ToString(lblPODate.Text);
                    else
                        PODate = string.Empty;

                    if (!string.IsNullOrEmpty(lblDOCClass.Text))
                        DOCClass = Convert.ToString(lblDOCClass.Text);
                    else
                        DOCClass = string.Empty;

                    if (!string.IsNullOrEmpty(lblVendorCode.Text))
                        vendCode = Convert.ToString(lblVendorCode.Text);
                    else
                        vendCode = string.Empty;

                    if (!string.IsNullOrEmpty(lblVendorName.Text))
                        vendName = Convert.ToString(lblVendorName.Text);
                    else
                        vendName = string.Empty;

                    if (!string.IsNullOrEmpty(lblPOValueINR.Text))
                        POValueINR = Convert.ToString(lblPOValueINR.Text);
                    else
                        POValueINR = string.Empty;

                    if (!string.IsNullOrEmpty(lblPOStatus.Text))
                        POStatus = Convert.ToString(lblPOStatus.Text);
                    else
                        POStatus = string.Empty;

                    if (!string.IsNullOrEmpty(lblLocation.Text))
                        location = Convert.ToString(lblLocation.Text);
                    else
                        location = string.Empty;

                    if (!string.IsNullOrEmpty(txtItemCategory.Text))
                        itemCategory = Convert.ToString(txtItemCategory.Text);
                    else
                        itemCategory = string.Empty;

                    if (!string.IsNullOrEmpty(txtDOD.Text))
                        DOD = Convert.ToString(txtDOD.Text);
                    else
                        DOD = string.Empty;

                    if (!string.IsNullOrEmpty(txtLikelyDOD.Text))
                        likelyDOD = Convert.ToString(txtLikelyDOD.Text);
                    else
                        likelyDOD = string.Empty;

                    if (!string.IsNullOrEmpty(txtMonths.Text))
                        months = Convert.ToInt32(txtMonths.Text);
                    else
                        months = 0;

                    if (string.IsNullOrEmpty(itemCategory))
                    {
                        ExceptionMessage("Please enter item category..!!");
                        return;
                    }

                    if (string.IsNullOrEmpty(likelyDOD))
                    {
                        ExceptionMessage("Please enter likely Date Of Delivery..!!");
                        return;
                    }

                    value = objReports.InsertUpdateVPOCPosting(0, PONo, PODate, DOCClass, vendCode, vendName, POValueINR,
                           POStatus, location, itemCategory, DOD, likelyDOD, months, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    if (value > 0)
                    {
                        SuccessMessage(PONo + " Posted successfully");
                        GetVPOCListForPosting();
                    }
                    else
                    {
                        ExceptionMessage("Please try again!");
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

    protected void btnPost_Click(object sender, EventArgs e)
    {
        PostVPOC();
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
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetVPOCListForPosting()
    {
        try
        {
            string fromDate = string.Empty;
            string toDate = string.Empty;
            string poNo = string.Empty;
            string vendorName = string.Empty;
            string status = string.Empty;
            int unitId = 0;
            string unitName = string.Empty;

            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;
            string unitNameA35 = string.Empty;
            string unitNameDLH = string.Empty;
            string unitNameSEZ = string.Empty;
            string unitNameGNU = string.Empty;

            dsDBDetails = (DataSet)Session["DB_DETAILS"];


            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (!string.IsNullOrEmpty(txtPONo.Text))
                poNo = txtPONo.Text.ToUpper();
            else
                poNo = string.Empty;

            if (!string.IsNullOrEmpty(txtVendorName.Text))
                vendorName = txtVendorName.Text;
            else
                vendorName = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                status = Convert.ToString(ddlStatus.SelectedItem.Text);
            else
                status = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
            {
                unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
                dsReport = objReports.GetVPOCListForPosting(unitId, unitName, fromDate, toDate, poNo, vendorName, status);
            }
            else
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        {
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameA35 = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        {
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameDLH = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        {
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameSEZ = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        {
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameGNU = Convert.ToString(dr["UNIT_NAME"]);
                        }
                    }
                }
                dsReport = objReports.GetVPOCListForPostingAllUnit(fromDate, toDate, dbNameA35, unitNameA35, dbNameDLH, unitNameDLH, dbNameSEZ, unitNameSEZ, dbNameGNU, unitNameGNU, poNo, vendorName, status);
            }

            if (dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
            {
                gvVPOCList.DataSource = dsReport.Tables[0];
                gvVPOCList.DataBind();
            }
            else
            {
                gvVPOCList.DataSource = null;
                gvVPOCList.DataBind();
            }
            lblRecords.Text = "Records[" + dsReport.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void PostVPOC()
    {
        try
        {
            int value = 0;
            int count = 0;

            if (gvVPOCList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvVPOCList.Rows)
                {
                    Label lblPONo = gr.FindControl("lblPONo") as Label;
                    Label lblPODate = gr.FindControl("lblPODate") as Label;
                    Label lblDOCClass = gr.FindControl("lblDOCClass") as Label;
                    Label lblVendorCode = gr.FindControl("lblVendorCode") as Label;
                    Label lblVendorName = gr.FindControl("lblVendorName") as Label;
                    Label lblPOValueINR = gr.FindControl("lblPOValueINR") as Label;
                    Label lblPOStatus = gr.FindControl("lblPOStatus") as Label;
                    Label lblLocation = gr.FindControl("lblLocation") as Label;
                    TextBox txtItemCategory = gr.FindControl("txtItemCategory") as TextBox;
                    TextBox txtDOD = gr.FindControl("txtDOD") as TextBox;
                    TextBox txtLikelyDOD = gr.FindControl("txtLikelyDOD") as TextBox;
                    TextBox txtMonths = gr.FindControl("txtMonths") as TextBox;

                    if (!string.IsNullOrEmpty(lblPONo.Text))
                        PONo = Convert.ToString(lblPONo.Text);
                    else
                        PONo = string.Empty;

                    if (!string.IsNullOrEmpty(lblPODate.Text))
                        PODate = Convert.ToString(lblPODate.Text);
                    else
                        PODate = string.Empty;

                    if (!string.IsNullOrEmpty(lblDOCClass.Text))
                        DOCClass = Convert.ToString(lblDOCClass.Text);
                    else
                        DOCClass = string.Empty;

                    if (!string.IsNullOrEmpty(lblVendorCode.Text))
                        vendCode = Convert.ToString(lblVendorCode.Text);
                    else
                        vendCode = string.Empty;

                    if (!string.IsNullOrEmpty(lblVendorName.Text))
                        vendName = Convert.ToString(lblVendorName.Text);
                    else
                        vendName = string.Empty;

                    if (!string.IsNullOrEmpty(lblPOValueINR.Text))
                        POValueINR = Convert.ToString(lblPOValueINR.Text);
                    else
                        POValueINR = string.Empty;

                    if (!string.IsNullOrEmpty(lblPOStatus.Text))
                        POStatus = Convert.ToString(lblPOStatus.Text);
                    else
                        POStatus = string.Empty;

                    if (!string.IsNullOrEmpty(lblLocation.Text))
                        location = Convert.ToString(lblLocation.Text);
                    else
                        location = string.Empty;

                    if (!string.IsNullOrEmpty(txtItemCategory.Text))
                        itemCategory = Convert.ToString(txtItemCategory.Text);
                    else
                        itemCategory = string.Empty;

                    if (!string.IsNullOrEmpty(txtDOD.Text))
                        DOD = Convert.ToString(txtDOD.Text);
                    else
                        DOD = string.Empty;

                    if (!string.IsNullOrEmpty(txtLikelyDOD.Text))
                        likelyDOD = Convert.ToString(txtLikelyDOD.Text);
                    else
                        likelyDOD = string.Empty;

                    if (!string.IsNullOrEmpty(txtMonths.Text))
                        months = Convert.ToInt32(txtMonths.Text);
                    else
                        months = 0;

                    if (!string.IsNullOrEmpty(itemCategory) && !string.IsNullOrEmpty(likelyDOD))
                    {
                        value = objReports.InsertUpdateVPOCPosting(0, PONo, PODate, DOCClass, vendCode, vendName, POValueINR,
                            POStatus, location, itemCategory, DOD, likelyDOD, months, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        if (value > 0)
                        {
                            count++;
                        }
                    }
                }
                if (count > 0)
                {
                    SuccessMessage(count + " posted successfully..!!");
                    GetVPOCListForPosting();
                    return;
                }
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

    #endregion

}
