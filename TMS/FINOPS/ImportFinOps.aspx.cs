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
using System.IO;
using System.Data.OleDb;

public partial class FINOPS_ImportFinOps : System.Web.UI.Page
{

    #region VARIABLES[================]

    BAL.FinOps objFinOps = new BAL.FinOps();

    DataSet dsFinOpsDetail = new DataSet();
    DataTable dtFinOps = new DataTable();
    DataTable dt1 = new DataTable();
    int existedRecrdsCount = 0;

    int finOpsID = 0;
    string taxInvoiceNo = string.Empty;
    string taxInvoiceDate = string.Empty;
    string month = string.Empty;
    string jobNo = string.Empty;
    string costCenter = string.Empty;
    string BU = string.Empty;
    double amount1 = 0;
    string hours = string.Empty;
    string paneltyClause = string.Empty;
    string paymentTerm = string.Empty;
    string poNo = string.Empty;
    int isVPOCOrder = 0;
    string deliveryMonth = string.Empty;
    double amount2 = 0;
    string itemCode = string.Empty;
    string itemDesc = string.Empty;
    string itemGroup = string.Empty;
    string itemSubgroup = string.Empty;
    string itemPivotGroup = string.Empty;

    int typeID = 0;
    int costTypeID = 0;
    int contigencyTypeID = 0;
    int status1ID = 0;
    int status2ID = 0;
    int SRNo = 0;

    int typeCount = 0;
    int typeFlag = 0;
    string typeTxt = string.Empty;

    int costTypeCount = 0;
    int costTypeFlag = 0;
    //string costTypeTxt = string.Empty;

    int contigencyTypeCount = 0;
    int contigencyTypeFlag = 0;
    //string contigencyTypeTxt = string.Empty;

    int checkVal = 0;

    int checkYerVal = 0;
    int checkMonthVal = 0;

    int checkHourVal = 0;
    int checkMinVal = 0;

    int status1Count = 0;
    int status1Flag = 0;
    //string status1Txt = string.Empty;

    int status2Count = 0;
    int status2Flag = 0;
    //string status2Txt = string.Empty;

    int finOpsIDcounts = 0;

    #endregion


    #region EVENTS[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();

            if (!IsPostBack)
            {
                Session["dtFinOps"] = null;
                Session["dsFinOpsDetail"] = GetFinPosDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    private DataSet GetFinPosDetails()
    {
        Session["dsFinOpsDetail"] = objFinOps.GetOpsTypes();

        if (Session["dsFinOpsDetail"] != null)
        {
            dsFinOpsDetail = (DataSet)Session["dsFinOpsDetail"];

            if (dsFinOpsDetail == null)
                dsFinOpsDetail = objFinOps.GetOpsTypes();
        }
        else
            dsFinOpsDetail = objFinOps.GetOpsTypes();

        return dsFinOpsDetail;
    }

    protected void btnGetFormat_Click(object sender, EventArgs e)
    {
        DownloadFormat();
    }

    protected void btnGetFinOpsFile_Click(object sender, EventArgs e)
    {
        GetFinOpsDetail();
    }

    protected void gvFinOps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            typeFlag = 0;
            typeCount = 0;
            costTypeFlag = 0;
            costTypeCount = 0;
            contigencyTypeFlag = 0;
            contigencyTypeCount = 0;


            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dsFinOpsDetail = GetFinPosDetails();

                Image imgAlreadyExisted = (Image)e.Row.FindControl("imgAlreadyExisted");

                TextBox txtTaxInvoiceDate = (TextBox)e.Row.FindControl("txtTaxInvoiceDate");
                ImageButton imgbtnTaxInvoiceDate = (ImageButton)e.Row.FindControl("imgbtnTaxInvoiceDate");
                HiddenField hdTaxInvoiceDate = (HiddenField)e.Row.FindControl("hdTaxInvoiceDate");


                Label lblJobNo = (Label)e.Row.FindControl("lblJobNo");
                TextBox txtMonth = (TextBox)e.Row.FindControl("txtMonth");
                Label lblType = (Label)e.Row.FindControl("lblType");
                Label lblTypeID = (Label)e.Row.FindControl("lblTypeID");
                DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
                Label lblCostType = (Label)e.Row.FindControl("lblCostType");
                Label lblCostTypeID = (Label)e.Row.FindControl("lblCostTypeID");
                DropDownList ddlCostType = (DropDownList)e.Row.FindControl("ddlCostType");
                Label lblContigencyType = (Label)e.Row.FindControl("lblContigencyType");
                Label lblContigencyTypeID = (Label)e.Row.FindControl("lblContigencyTypeID");
                DropDownList ddlContigencyType = (DropDownList)e.Row.FindControl("ddlContigencyType");
                Label lblStatus1 = (Label)e.Row.FindControl("lblStatus1");
                Label lblStatus1ID = (Label)e.Row.FindControl("lblStatus1ID");
                DropDownList ddlStatus1 = (DropDownList)e.Row.FindControl("ddlStatus1");
                Label lblStatus2 = (Label)e.Row.FindControl("lblStatus2");
                Label lblStatus2ID = (Label)e.Row.FindControl("lblStatus2ID");
                DropDownList ddlStatus2 = (DropDownList)e.Row.FindControl("ddlStatus2");
                TextBox txtAmount1 = (TextBox)e.Row.FindControl("txtAmount1");
                TextBox txtAmount2 = (TextBox)e.Row.FindControl("txtAmount2");
                TextBox txtHours = (TextBox)e.Row.FindControl("txtHours");
                Label lblIsVPOCOrder = (Label)e.Row.FindControl("lblIsVPOCOrder");
                DropDownList ddlIsVPOCOrder = (DropDownList)e.Row.FindControl("ddlIsVPOCOrder");
                TextBox txtDeliveryMonth = (TextBox)e.Row.FindControl("txtDeliveryMonth");

                Label lblTaxInvoiceDateValidationFlag = (Label)e.Row.FindControl("lblTaxInvoiceDateValidationFlag");
                Label lblMonthValidationFlag = (Label)e.Row.FindControl("lblMonthValidationFlag");
                Label lblTypeValidationFlag = (Label)e.Row.FindControl("lblTypeValidationFlag");
                Label lblCostTypeValidationFlag = (Label)e.Row.FindControl("lblCostTypeValidationFlag");
                Label lblAmount1ValidationFlag = (Label)e.Row.FindControl("lblAmount1ValidationFlag");
                Label lblHoursValidationFlag = (Label)e.Row.FindControl("lblHoursValidationFlag");
                Label lblContigencyTypeValidationFlag = (Label)e.Row.FindControl("lblContigencyTypeValidationFlag");
                Label lblIsVPOCOrderValidationFlag = (Label)e.Row.FindControl("lblIsVPOCOrderValidationFlag");
                Label lblDeliveryMonthValidationFlag = (Label)e.Row.FindControl("lblDeliveryMonthValidationFlag");
                Label lblStatus1ValidationFlag = (Label)e.Row.FindControl("lblStatus1ValidationFlag");
                Label lblAmount2ValidationFlag = (Label)e.Row.FindControl("lblAmount2ValidationFlag");
                Label lblStatus2ValidationFlag = (Label)e.Row.FindControl("lblStatus2ValidationFlag");
                Label lblValidationFlag = (Label)e.Row.FindControl("lblValidationFlag");


                lblTaxInvoiceDateValidationFlag.Text = "0";
                lblMonthValidationFlag.Text = "0";
                lblTypeValidationFlag.Text = "0";
                lblCostTypeValidationFlag.Text = "0";
                lblAmount1ValidationFlag.Text = "0";
                lblHoursValidationFlag.Text = "0";
                lblContigencyTypeValidationFlag.Text = "0";
                lblIsVPOCOrderValidationFlag.Text = "0";
                lblDeliveryMonthValidationFlag.Text = "0";
                lblStatus1ValidationFlag.Text = "0";
                lblAmount2ValidationFlag.Text = "0";
                lblStatus2ValidationFlag.Text = "0";
                lblValidationFlag.Text = "0";


                lblJobNo.Text = lblJobNo.Text.ToUpper();

                // INVOICE DATE----------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(txtTaxInvoiceDate.Text)))
                {
                    if (!CheckDate(Convert.ToString(txtTaxInvoiceDate.Text)))
                    {
                        lblTaxInvoiceDateValidationFlag.Text = "1";
                        txtTaxInvoiceDate.ToolTip = "Please enter correct invoice date...!";
                        txtTaxInvoiceDate.BackColor = System.Drawing.Color.Yellow;
                        txtTaxInvoiceDate.Enabled = true;
                        imgbtnTaxInvoiceDate.Visible = true;
                    }
                    else
                    {
                        hdTaxInvoiceDate.Value = Convert.ToDateTime(txtTaxInvoiceDate.Text).ToString("dd-MMM-yyyy");
                        txtTaxInvoiceDate.Text = hdTaxInvoiceDate.Value;
                    }
                }
                else
                {
                    lblTaxInvoiceDateValidationFlag.Text = "1";
                    txtTaxInvoiceDate.ToolTip = "Please enter correct invoice date...!";
                    txtTaxInvoiceDate.BackColor = System.Drawing.Color.Yellow;
                    txtTaxInvoiceDate.Enabled = true;
                    imgbtnTaxInvoiceDate.Visible = true;
                }
                //------------------------------------------------------------------


                // MONTH------------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(txtMonth.Text)))
                {
                    int count = 0;
                    if (!CheckMonth(Convert.ToString(txtMonth.Text)))
                    {
                        lblMonthValidationFlag.Text = "1";
                        txtMonth.ToolTip = "Please enter correct month...!";
                        txtMonth.BackColor = System.Drawing.Color.Yellow;
                        txtMonth.Enabled = true;
                    }
                    else
                    {
                        string[] strtxt = Convert.ToString(txtMonth.Text).Split('-');
                        string year = Convert.ToString(strtxt[0]);
                        string month = Convert.ToString(strtxt[1]);

                        if (month.Length < 2)
                            month = "0" + month;

                        txtMonth.Text = year + "-" + month;

                        if (dsFinOpsDetail.Tables.Count > 0 && dsFinOpsDetail.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dsFinOpsDetail.Tables[0].Select("JOB_NO='" + Convert.ToString(lblJobNo.Text).ToUpper() + "' AND MONTH='" + Convert.ToString(txtMonth.Text) + "'"))
                            {
                                count++;
                                existedRecrdsCount++;
                            }
                        }

                        if (count > 0)
                        {
                            imgAlreadyExisted.Visible = true;
                            imgAlreadyExisted.ToolTip = Convert.ToString(lblJobNo.Text) + " already existed with month " + Convert.ToString(txtMonth.Text);
                        }
                        else
                        {
                            imgAlreadyExisted.Visible = false;
                            imgAlreadyExisted.ToolTip = string.Empty;
                        }
                    }
                }
                else
                {
                    lblMonthValidationFlag.Text = "1";
                    txtMonth.ToolTip = "Please enter correct month...!";
                    txtMonth.BackColor = System.Drawing.Color.Yellow;
                    txtMonth.Enabled = true;
                }
                //------------------------------------------------------------------


                // TYPE --------------------------------------------------------

                if (dsFinOpsDetail.Tables[1].Rows.Count > 0)
                {
                    ddlType.DataSource = dsFinOpsDetail.Tables[1];
                    ddlType.DataTextField = "TYPE";
                    ddlType.DataValueField = "TYPE_ID";
                    ddlType.DataBind();
                    ddlType.Items.Insert(0, "Select");
                    if (!string.IsNullOrEmpty(Convert.ToString(lblTypeID.Text)))
                    {
                        foreach (DataRow drt in dsFinOpsDetail.Tables[1].Select("TYPE_ID='" + Convert.ToString(lblTypeID.Text) + "'"))
                        {
                            typeFlag = 0;
                            ddlType.SelectedValue = Convert.ToString(lblTypeID.Text);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(lblType.Text)))
                        {
                            if (dsFinOpsDetail.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow drt in dsFinOpsDetail.Tables[1].Select("TYPE='" + Convert.ToString(lblType.Text) + "'"))
                                {
                                    typeCount++;
                                }
                            }
                            if (typeCount == 0)
                            {
                                typeFlag = 1;
                            }
                        }
                        else
                        {
                            typeFlag = 1;
                        }
                    }
                }

                if (typeFlag == 1)
                {
                    lblTypeValidationFlag.Text = "1";                    
                    ddlType.SelectedIndex = 0;
                    ddlType.ToolTip = "Please select type...!!!";
                    ddlType.BackColor = System.Drawing.Color.Yellow;
                    //ddlType.Enabled = true;

                }
                //---------------------------------------------------------------


                //COST TYPE------------------------------------------------------

                if (dsFinOpsDetail.Tables[2].Rows.Count > 0)
                {
                    ddlCostType.DataSource = dsFinOpsDetail.Tables[2];
                    ddlCostType.DataTextField = "COST_TYPE";
                    ddlCostType.DataValueField = "COST_TYPE_ID";
                    ddlCostType.DataBind();
                    ddlCostType.Items.Insert(0, "Select");
                    if (!string.IsNullOrEmpty(Convert.ToString(lblCostTypeID.Text)))
                    {
                        foreach (DataRow drt in dsFinOpsDetail.Tables[2].Select("COST_TYPE_ID='" + Convert.ToString(lblCostTypeID.Text) + "'"))
                        {
                            costTypeFlag = 0;
                            ddlCostType.SelectedValue = Convert.ToString(lblCostTypeID.Text);
                            //ddlCostType.Enabled = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(lblCostType.Text)))
                        {
                            if (dsFinOpsDetail.Tables[2].Rows.Count > 0)
                            {
                                foreach (DataRow drt in dsFinOpsDetail.Tables[2].Select("COST_TYPE='" + Convert.ToString(lblCostType.Text) + "'"))
                                {
                                    costTypeCount++;
                                }
                            }
                            if (costTypeCount == 0)
                            {
                                costTypeFlag = 1;
                            }
                        }
                        else
                        {
                            costTypeFlag = 1;
                        }
                    }
                }

                if (costTypeFlag == 1)
                {
                    lblCostTypeValidationFlag.Text = "1";                   
                    ddlCostType.SelectedIndex = 0;
                    ddlCostType.ToolTip = "Please select Cost Type...!!!";
                    ddlCostType.BackColor = System.Drawing.Color.Yellow;
                    //ddlCostType.Enabled = true;

                }
                //------------------------------------------------------------------


                //CONTIGENCY TYPE---------------------------------------------------

                if (dsFinOpsDetail.Tables[3].Rows.Count > 0)
                {
                    ddlContigencyType.DataSource = dsFinOpsDetail.Tables[3];
                    ddlContigencyType.DataTextField = "CONTIGENCY_TYPE";
                    ddlContigencyType.DataValueField = "CONTIGENCY_TYPE_ID";
                    ddlContigencyType.DataBind();
                    ddlContigencyType.Items.Insert(0, "Select");
                    if (!string.IsNullOrEmpty(Convert.ToString(lblContigencyTypeID.Text)))
                    {
                        foreach (DataRow drt in dsFinOpsDetail.Tables[3].Select("CONTIGENCY_TYPE_ID='" + Convert.ToString(lblContigencyTypeID.Text) + "'"))
                        {
                            contigencyTypeFlag = 0;
                            ddlContigencyType.SelectedValue = Convert.ToString(lblContigencyTypeID.Text);
                            //ddlContigencyType.Enabled = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(lblContigencyType.Text)))
                        {
                            if (dsFinOpsDetail.Tables[3].Rows.Count > 0)
                            {
                                foreach (DataRow drt in dsFinOpsDetail.Tables[3].Select("CONTIGENCY_TYPE='" + Convert.ToString(lblContigencyType.Text) + "'"))
                                {
                                    contigencyTypeCount++;
                                }
                            }
                            if (contigencyTypeCount == 0)
                            {
                                contigencyTypeFlag = 1;
                            }
                        }
                        else
                        {
                            contigencyTypeFlag = 1;
                        }
                    }
                }

                if (contigencyTypeFlag == 1)
                {
                    lblContigencyTypeValidationFlag.Text = "1";                    
                    ddlContigencyType.SelectedIndex = 0;
                    ddlContigencyType.ToolTip = "Please select contigency type...!!!";
                    ddlContigencyType.BackColor = System.Drawing.Color.Yellow;
                    //ddlContigencyType.Enabled = true;

                }

                //------------------------------------------------------------------


                // AMOUNT1----------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(txtAmount1.Text)))
                {
                    if (!CheckNumeric(Convert.ToString(txtAmount1.Text)))
                    {
                        lblAmount1ValidationFlag.Text = "1";
                        txtAmount1.ToolTip = "Please enter correct amount1...!";
                        txtAmount1.BackColor = System.Drawing.Color.Yellow;
                        txtAmount1.Enabled = true;
                    }
                }
                //------------------------------------------------------------------


                // HOURS------------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(txtHours.Text)))
                {
                    if (!CheckHours(Convert.ToString(txtHours.Text)))
                    {
                        lblHoursValidationFlag.Text = "1";
                        txtHours.ToolTip = "Please enter correct hours...!";
                        txtHours.BackColor = System.Drawing.Color.Yellow;
                        txtHours.Enabled = true;
                    }
                    else
                    {
                        string[] strtxt = Convert.ToString(txtHours.Text).Split(':');
                        string hourtxt = string.Empty;
                        string hour = Convert.ToString(strtxt[0]);
                        string minuts = Convert.ToString(strtxt[1]);

                        for (int i = 1; i <= 5 - Convert.ToInt32(hour.Length); i++)
                        {
                            hourtxt += "0";
                        }

                        hour = hourtxt + hour;

                        if (minuts.Length == 0)
                            minuts = "00";
                        else if (minuts.Length == 1)
                            minuts = minuts + "0";

                        txtHours.Text = hour + ":" + minuts;
                    }
                }
                else
                {
                    lblHoursValidationFlag.Text = "1";
                    txtHours.ToolTip = "Please enter correct hours...!";
                    txtHours.BackColor = System.Drawing.Color.Yellow;
                    txtHours.Enabled = true;
                }
                //------------------------------------------------------------------


                // AMOUNT2----------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(txtAmount2.Text)))
                {
                    if (!CheckNumeric(Convert.ToString(txtAmount2.Text)))
                    {
                        lblAmount2ValidationFlag.Text = "1";
                        txtAmount2.ToolTip = "Please enter correct amount2...!";
                        txtAmount2.BackColor = System.Drawing.Color.Yellow;
                        txtAmount2.Enabled = true;
                    }
                }
                //------------------------------------------------------------------

                // IS VPOC ORDER------------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(lblIsVPOCOrder.Text)))
                {
                    if (Convert.ToString(lblIsVPOCOrder.Text).ToUpper() == "YES" || Convert.ToString(lblIsVPOCOrder.Text).ToUpper() == "NO")
                    {
                        if (Convert.ToString(lblIsVPOCOrder.Text).ToUpper() == "YES")
                            ddlIsVPOCOrder.SelectedValue = "1";
                        else if (Convert.ToString(lblIsVPOCOrder.Text).ToUpper() == "NO")
                            ddlIsVPOCOrder.SelectedValue = "2";
                        else
                            ddlIsVPOCOrder.SelectedValue = "0";
                    }
                    else
                    {
                        lblIsVPOCOrderValidationFlag.Text = "1";
                        ddlIsVPOCOrder.SelectedIndex = 0;
                    }
                }
                else
                {
                    lblIsVPOCOrderValidationFlag.Text = "1";
                    ddlIsVPOCOrder.SelectedIndex = 0;
                }

                //------------------------------------------------------------------

                // DELIVERY MONTH------------------------------------------------------------
                if (!string.IsNullOrEmpty(Convert.ToString(txtDeliveryMonth.Text)))
                {
                    if (!CheckMonth(Convert.ToString(txtDeliveryMonth.Text)))
                    {
                        lblDeliveryMonthValidationFlag.Text = "1";
                        txtDeliveryMonth.ToolTip = "Please enter correct delivery month...!";
                        txtDeliveryMonth.BackColor = System.Drawing.Color.Yellow;
                        txtDeliveryMonth.Enabled = true;
                    }
                    else
                    {
                        string[] strtxt = Convert.ToString(txtDeliveryMonth.Text).Split('-');
                        string year = Convert.ToString(strtxt[0]);
                        string month = Convert.ToString(strtxt[1]);

                        if (month.Length < 2)
                            month = "0" + month;

                        txtDeliveryMonth.Text = year + "-" + month;
                    }
                }
                else
                {
                    lblDeliveryMonthValidationFlag.Text = "1";
                    txtDeliveryMonth.ToolTip = "Please enter correct delivery month...!";
                    txtDeliveryMonth.BackColor = System.Drawing.Color.Yellow;
                    txtDeliveryMonth.Enabled = true;
                }
                //------------------------------------------------------------------




                // STATUS1 --------------------------------------------------------

                if (dsFinOpsDetail.Tables[4].Rows.Count > 0)
                {
                    ddlStatus1.DataSource = dsFinOpsDetail.Tables[4];
                    ddlStatus1.DataTextField = "STATUS1";
                    ddlStatus1.DataValueField = "STATUS1_ID";
                    ddlStatus1.DataBind();
                    if (!string.IsNullOrEmpty(Convert.ToString(lblStatus1ID.Text)))
                    {
                        foreach (DataRow drt in dsFinOpsDetail.Tables[4].Select("STATUS1_ID='" + Convert.ToString(lblStatus1ID.Text) + "'"))
                        {
                            status1Flag = 0;
                            ddlStatus1.SelectedValue = Convert.ToString(lblStatus1ID.Text);
                            //ddlStatus1.Enabled = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(lblStatus1.Text)))
                        {
                            if (dsFinOpsDetail.Tables[4].Rows.Count > 0)
                            {
                                foreach (DataRow drt in dsFinOpsDetail.Tables[4].Select("STATUS1='" + Convert.ToString(lblStatus1.Text) + "'"))
                                {
                                    status1Count++;
                                }
                            }
                            if (status1Count == 0)
                            {
                                status1Flag = 1;
                            }
                        }
                        else
                        {
                            status1Flag = 1;
                        }
                    }
                }

                if (status1Flag == 1)
                {
                    lblStatus1ValidationFlag.Text = "1";

                    ddlStatus1.Items.Insert(0, "Select");
                    ddlStatus1.SelectedIndex = 0;

                    ddlStatus1.ToolTip = "Please select status1...!!!";
                    ddlStatus1.BackColor = System.Drawing.Color.Yellow;
                    //ddlStatus1.Enabled = true;

                }

                //---------------------------------------------------------------


                // STATUS2 --------------------------------------------------------

                if (dsFinOpsDetail.Tables[5].Rows.Count > 0)
                {
                    ddlStatus2.DataSource = dsFinOpsDetail.Tables[5];
                    ddlStatus2.DataTextField = "STATUS2";
                    ddlStatus2.DataValueField = "STATUS2_ID";
                    ddlStatus2.DataBind();
                    if (!string.IsNullOrEmpty(Convert.ToString(lblStatus2ID.Text)))
                    {
                        foreach (DataRow drt in dsFinOpsDetail.Tables[5].Select("STATUS2_ID='" + Convert.ToString(lblStatus2ID.Text) + "'"))
                        {
                            status2Flag = 0;
                            ddlStatus2.SelectedValue = Convert.ToString(lblStatus2ID.Text);
                            //ddlStatus2.Enabled = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(lblStatus2.Text)))
                        {
                            if (dsFinOpsDetail.Tables[5].Rows.Count > 0)
                            {
                                foreach (DataRow drt in dsFinOpsDetail.Tables[5].Select("STATUS2='" + Convert.ToString(lblStatus2.Text) + "'"))
                                {
                                    status2Count++;
                                }
                            }
                            if (status2Count == 0)
                            {
                                status2Flag = 1;
                            }
                        }
                        else
                        {
                            status2Flag = 1;
                        }
                    }
                }

                if (status2Flag == 1)
                {
                    lblStatus2ValidationFlag.Text = "1";

                    ddlStatus2.Items.Insert(0, "Select");
                    ddlStatus2.SelectedIndex = 0;

                    ddlStatus2.ToolTip = "Please select status2...!!!";
                    ddlStatus2.BackColor = System.Drawing.Color.Yellow;
                    //ddlStatus2.Enabled = true;

                }

                //---------------------------------------------------------------


                if (lblTaxInvoiceDateValidationFlag.Text == "0" &&
                        lblMonthValidationFlag.Text == "0" &&
                        lblTypeValidationFlag.Text == "0" &&
                        lblCostTypeValidationFlag.Text == "0" &&
                        lblAmount1ValidationFlag.Text == "0" &&
                        lblHoursValidationFlag.Text == "0" &&
                        lblContigencyTypeValidationFlag.Text == "0" &&
                        lblIsVPOCOrderValidationFlag.Text == "0" &&
                        lblDeliveryMonthValidationFlag.Text == "0" &&
                        lblStatus1ValidationFlag.Text == "0" &&
                        lblAmount2ValidationFlag.Text == "0" &&
                        lblStatus2ValidationFlag.Text == "0" &&
                        lblValidationFlag.Text == "0")
                {
                    lblValidationFlag.Text = "0";
                }
                else
                {
                    lblValidationFlag.Text = "1";
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvFinOps.Rows.Count > 0)
        {
            ImportFinOps();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }


    protected void txtTaxInvoiceDate_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtTaxInvoiceDate = (TextBox)sender;
            GridViewRow gr = (GridViewRow)txtTaxInvoiceDate.Parent.Parent;
            Label lblTaxInvoiceDateValidationFlag = (Label)gr.FindControl("lblTaxInvoiceDateValidationFlag");
            ImageButton imgbtnTaxInvoiceDate = (ImageButton)gr.FindControl("imgbtnTaxInvoiceDate");

            if (!string.IsNullOrEmpty(Convert.ToString(txtTaxInvoiceDate.Text)))
            {
                if (!CheckDate(Convert.ToString(txtTaxInvoiceDate.Text)))
                {
                    lblTaxInvoiceDateValidationFlag.Text = "1";
                    txtTaxInvoiceDate.ToolTip = "Please enter correct invoice date...!";
                    txtTaxInvoiceDate.BackColor = System.Drawing.Color.Yellow;
                    txtTaxInvoiceDate.Enabled = true;
                    imgbtnTaxInvoiceDate.Visible = true;
                }
                else
                {
                    lblTaxInvoiceDateValidationFlag.Text = "0";
                    txtTaxInvoiceDate.ToolTip = string.Empty;
                    txtTaxInvoiceDate.BackColor = System.Drawing.Color.Transparent;
                }
            }
            else
            {
                lblTaxInvoiceDateValidationFlag.Text = "1";
                txtTaxInvoiceDate.ToolTip = "Please enter correct invoice date...!";
                txtTaxInvoiceDate.BackColor = System.Drawing.Color.Yellow;
                txtTaxInvoiceDate.Enabled = true;
                imgbtnTaxInvoiceDate.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void txtMonth_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            int monthFlag = 0;
            int monthGrvCount = 0;

            dsFinOpsDetail = GetFinPosDetails();

            int count = 0;

            TextBox txtMonth = (TextBox)sender;
            GridViewRow gr = (GridViewRow)txtMonth.Parent.Parent;
            Label lblMonthValidationFlag = (Label)gr.FindControl("lblMonthValidationFlag");

            Image imgAlreadyExisted = (Image)gr.FindControl("imgAlreadyExisted");
            Label lblJobNo = (Label)gr.FindControl("lblJobNo");

            if (!string.IsNullOrEmpty(Convert.ToString(txtMonth.Text)))
            {
                if (!CheckMonth(Convert.ToString(txtMonth.Text)))
                {
                    monthFlag = 1;
                }
                else
                {
                    foreach (GridViewRow grv in gvFinOps.Rows)
                    {
                        TextBox txtMonthGrv = (TextBox)grv.FindControl("txtMonth");
                        Label lblJobNoGrv = (Label)grv.FindControl("lblJobNo");
                        if (txtMonth.Text == txtMonthGrv.Text && lblJobNo.Text == lblJobNoGrv.Text)
                        {
                            monthGrvCount++;
                        }
                    }

                    if (monthGrvCount > 1)
                    {
                        monthFlag = 1;
                    }
                    else
                    {
                        monthFlag = 0;
                    }
                }
            }
            else
            {
                monthFlag = 1;
            }

            if (monthFlag == 0)
            {
                lblMonthValidationFlag.Text = "0";

                string[] strtxt = Convert.ToString(txtMonth.Text).Split('-');
                string year = Convert.ToString(strtxt[0]);
                string month = Convert.ToString(strtxt[1]);

                if (month.Length < 2)
                    month = "0" + month;

                txtMonth.Text = year + "-" + month;

                txtMonth.ToolTip = string.Empty;
                txtMonth.BackColor = System.Drawing.Color.Transparent;


                if (dsFinOpsDetail.Tables.Count > 0 && dsFinOpsDetail.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dsFinOpsDetail.Tables[0].Select("JOB_NO='" + Convert.ToString(lblJobNo.Text).ToUpper() + "' AND MONTH='" + Convert.ToString(txtMonth.Text) + "'"))
                    {
                        count++;
                        existedRecrdsCount++;
                    }
                }

                if (count > 0)
                {
                    imgAlreadyExisted.Visible = true;
                    imgAlreadyExisted.ToolTip = Convert.ToString(lblJobNo.Text) + " already existed with month " + Convert.ToString(txtMonth.Text);
                }
                else
                {
                    imgAlreadyExisted.Visible = false;
                    imgAlreadyExisted.ToolTip = string.Empty;
                }
            }
            else
            {
                lblMonthValidationFlag.Text = "1";
                txtMonth.Enabled = true;
                if (monthGrvCount > 1)
                {
                    txtMonth.ToolTip = Convert.ToString(lblJobNo.Text) + " already existed with month " + Convert.ToString(txtMonth.Text) + " in list please  enter other month...!";
                    txtMonth.BackColor = System.Drawing.Color.Gold;
                }
                else
                {
                    txtMonth.ToolTip = "Please enter correct month...!";
                    txtMonth.BackColor = System.Drawing.Color.Yellow;
                }
            }


        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlType = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlType.Parent.Parent;
            Label lblTypeValidationFlag = (Label)gr.FindControl("lblTypeValidationFlag");

            if (ddlType.SelectedIndex > 0)
            {
                lblTypeValidationFlag.Text = "0";
                ddlType.ToolTip = string.Empty;
                ddlType.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                lblTypeValidationFlag.Text = "1";
                ddlType.ToolTip = "Please select contigency type...!!!";
                ddlType.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void ddlCostType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlCostType = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlCostType.Parent.Parent;
            Label lblCostTypeValidationFlag = (Label)gr.FindControl("lblCostTypeValidationFlag");

            if (ddlCostType.SelectedIndex > 0)
            {
                lblCostTypeValidationFlag.Text = "0";
                ddlCostType.ToolTip = string.Empty;
                ddlCostType.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                lblCostTypeValidationFlag.Text = "1";
                ddlCostType.ToolTip = "Please select contigency type...!!!";
                ddlCostType.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void txtAmount1_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtAmount1 = (TextBox)sender;
            GridViewRow gr = (GridViewRow)txtAmount1.Parent.Parent;
            Label lblAmount1ValidationFlag = (Label)gr.FindControl("lblAmount1ValidationFlag");

            if (!string.IsNullOrEmpty(Convert.ToString(txtAmount1.Text)))
            {
                if (!CheckNumeric(Convert.ToString(txtAmount1.Text)))
                {
                    lblAmount1ValidationFlag.Text = "1";
                    txtAmount1.ToolTip = "Please enter correct amount1...!";
                    txtAmount1.BackColor = System.Drawing.Color.Yellow;
                    txtAmount1.Enabled = true;
                }
                else
                {
                    lblAmount1ValidationFlag.Text = "0";
                    txtAmount1.ToolTip = string.Empty;

                    txtAmount1.BackColor = System.Drawing.Color.Transparent;
                    txtAmount1.Enabled = true;
                }
            }
            else
            {
                lblAmount1ValidationFlag.Text = "0";
                txtAmount1.ToolTip = string.Empty;

                txtAmount1.BackColor = System.Drawing.Color.Transparent;
                txtAmount1.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void txtHours_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtHours = (TextBox)sender;
            GridViewRow gr = (GridViewRow)txtHours.Parent.Parent;
            Label lblHoursValidationFlag = (Label)gr.FindControl("lblHoursValidationFlag");


            if (!string.IsNullOrEmpty(Convert.ToString(txtHours.Text)))
            {
                if (!CheckHours(Convert.ToString(txtHours.Text)))
                {
                    lblHoursValidationFlag.Text = "1";
                    txtHours.ToolTip = "Please enter correct hours...!";
                    txtHours.BackColor = System.Drawing.Color.Yellow;
                    txtHours.Enabled = true;
                }
                else
                {
                    lblHoursValidationFlag.Text = "0";

                    string[] strtxt = Convert.ToString(txtHours.Text).Split(':');
                    string hourtxt = string.Empty;
                    string hour = Convert.ToString(strtxt[0]);
                    string minuts = Convert.ToString(strtxt[1]);

                    for (int i = 1; i <= 5 - Convert.ToInt32(hour.Length); i++)
                    {
                        hourtxt += "0";
                    }

                    hour = hourtxt + hour;

                    if (minuts.Length == 0)
                        minuts = "00";
                    else if (minuts.Length == 1)
                        minuts = minuts + "0";

                    txtHours.Text = hour + ":" + minuts;

                    txtHours.ToolTip = string.Empty;
                    txtHours.BackColor = System.Drawing.Color.Transparent;
                }
            }
            else
            {
                lblHoursValidationFlag.Text = "1";
                txtHours.ToolTip = "Please enter correct hours...!";
                txtHours.BackColor = System.Drawing.Color.Yellow;
                txtHours.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlContigencyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlContigencyType = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlContigencyType.Parent.Parent;
            Label lblContigencyTypeValidationFlag = (Label)gr.FindControl("lblContigencyTypeValidationFlag");

            if (ddlContigencyType.SelectedIndex > 0)
            {
                lblContigencyTypeValidationFlag.Text = "0";
                ddlContigencyType.ToolTip = string.Empty;
                ddlContigencyType.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                lblContigencyTypeValidationFlag.Text = "1";
                ddlContigencyType.ToolTip = "Please select contigency type...!!!";
                ddlContigencyType.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void ddlIsVPOCOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlIsVPOCOrder = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlIsVPOCOrder.Parent.Parent;
            Label lblIsVPOCOrderValidationFlag = (Label)gr.FindControl("lblIsVPOCOrderValidationFlag");

            if (ddlIsVPOCOrder.SelectedIndex > 0)
            {
                lblIsVPOCOrderValidationFlag.Text = "0";
                ddlIsVPOCOrder.ToolTip = string.Empty;
                ddlIsVPOCOrder.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                lblIsVPOCOrderValidationFlag.Text = "1";
                ddlIsVPOCOrder.ToolTip = "Please select VPOC or not?...!!!";
                ddlIsVPOCOrder.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void txtDeliveryMonth_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtDeliveryMonth = (TextBox)sender;
            GridViewRow gr = (GridViewRow)txtDeliveryMonth.Parent.Parent;
            Label lblDeliveryMonthValidationFlag = (Label)gr.FindControl("lblDeliveryMonthValidationFlag");


            if (!string.IsNullOrEmpty(Convert.ToString(txtDeliveryMonth.Text)))
            {
                if (!CheckMonth(Convert.ToString(txtDeliveryMonth.Text)))
                {
                    lblDeliveryMonthValidationFlag.Text = "1";
                    txtDeliveryMonth.ToolTip = "Please enter correct delivery month...!";
                    txtDeliveryMonth.BackColor = System.Drawing.Color.Yellow;
                    txtDeliveryMonth.Enabled = true;
                }
                else
                {
                    lblDeliveryMonthValidationFlag.Text = "0";
                    string[] strtxt = Convert.ToString(txtDeliveryMonth.Text).Split('-');
                    string year = Convert.ToString(strtxt[0]);
                    string month = Convert.ToString(strtxt[1]);

                    if (month.Length < 2)
                        month = "0" + month;

                    txtDeliveryMonth.Text = year + "-" + month;

                    txtDeliveryMonth.ToolTip = string.Empty;
                    txtDeliveryMonth.BackColor = System.Drawing.Color.Transparent;
                }
            }
            else
            {
                lblDeliveryMonthValidationFlag.Text = "1";
                txtDeliveryMonth.ToolTip = "Please enter correct delivery month...!";
                txtDeliveryMonth.BackColor = System.Drawing.Color.Yellow;
                txtDeliveryMonth.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlStatus1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlStatus1 = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlStatus1.Parent.Parent;
            Label lblStatus1ValidationFlag = (Label)gr.FindControl("lblStatus1ValidationFlag");

            if (ddlStatus1.SelectedIndex > 0)
            {
                lblStatus1ValidationFlag.Text = "0";
                ddlStatus1.ToolTip = string.Empty;
                ddlStatus1.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                lblStatus1ValidationFlag.Text = "1";
                ddlStatus1.ToolTip = "Please select status1...!!!";
                ddlStatus1.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void txtAmount2_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtAmount2 = (TextBox)sender;
            GridViewRow gr = (GridViewRow)txtAmount2.Parent.Parent;
            Label lblAmount2ValidationFlag = (Label)gr.FindControl("lblAmount2ValidationFlag");

            if (!string.IsNullOrEmpty(Convert.ToString(txtAmount2.Text)))
            {
                if (!CheckNumeric(Convert.ToString(txtAmount2.Text)))
                {
                    lblAmount2ValidationFlag.Text = "1";
                    txtAmount2.ToolTip = "Please enter correct Amount2...!";
                    txtAmount2.BackColor = System.Drawing.Color.Yellow;
                    txtAmount2.Enabled = true;
                }
                else
                {
                    lblAmount2ValidationFlag.Text = "0";
                    txtAmount2.ToolTip = string.Empty;
                    txtAmount2.BackColor = System.Drawing.Color.Transparent;
                    txtAmount2.Enabled = true;
                }
            }
            else
            {
                lblAmount2ValidationFlag.Text = "0";
                txtAmount2.ToolTip = string.Empty;
                txtAmount2.BackColor = System.Drawing.Color.Transparent;
                txtAmount2.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlStatus2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlStatus2 = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlStatus2.Parent.Parent;
            Label lblStatus2ValidationFlag = (Label)gr.FindControl("lblStatus2ValidationFlag");

            if (ddlStatus2.SelectedIndex > 0)
            {
                lblStatus2ValidationFlag.Text = "0";
                ddlStatus2.ToolTip = string.Empty;
                ddlStatus2.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                lblStatus2ValidationFlag.Text = "1";
                ddlStatus2.ToolTip = "Please select status2...!!!";
                ddlStatus2.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    #endregion


    #region METHODS[==================]


    private void DownloadFormat()
    {
        try
        {
            string csv = string.Empty;

            csv = "TAX_INVOICE_NO" + ',';
            csv += "TAX_INVOICE_DATE" + ',';
            csv += "MONTH" + ',';
            csv += "JOB_NO" + ',';
            csv += "COST_CENTER" + ',';
            csv += "TYPE" + ',';
            csv += "COST_TYPE" + ',';
            csv += "BU" + ',';
            csv += "AMOUNT1" + ',';
            csv += "HOURS" + ',';
            csv += "PANELTY_CLAUSE" + ',';
            csv += "PAYMENT_TERM" + ',';
            csv += "PO_NO" + ',';
            csv += "CONTIGENCY_TYPE" + ',';
            csv += "IS_VPOC_ORDER" + ',';
            csv += "DELIVERY_MONTH" + ',';
            csv += "STATUS1" + ',';
            csv += "AMOUNT2" + ',';
            csv += "STATUS2" + ',';
            csv += "ITEM_CODE" + ',';
            csv += "ITEM_DESC" + ',';
            csv += "ITEM_GROUP" + ',';
            csv += "ITEM_SUBGROUP" + ',';
            csv += "ITEM_PIVOT_GROUP" + ',';
            csv += "\r\n";

            string fileName = "FIN_OPS";
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

    private void GetFinOpsDetail()
    {
        try
        {
            hdGVRowCount.Value = "0";
            int count = 0;

            #region CREATE_TABLE

            dtFinOps.Columns.Add("FIN_OPS_ID", typeof(int));
            dtFinOps.Columns.Add("TAX_INVOICE_NO", typeof(string));
            dtFinOps.Columns.Add("TAX_INVOICE_DATE", typeof(string));
            dtFinOps.Columns.Add("MONTH", typeof(string));
            dtFinOps.Columns.Add("JOB_NO", typeof(string));
            dtFinOps.Columns.Add("COST_CENTER", typeof(string));
            dtFinOps.Columns.Add("TYPE", typeof(string));
            dtFinOps.Columns.Add("COST_TYPE", typeof(string));
            dtFinOps.Columns.Add("BU", typeof(string));
            dtFinOps.Columns.Add("AMOUNT1", typeof(string));
            dtFinOps.Columns.Add("HOURS", typeof(string));
            dtFinOps.Columns.Add("PANELTY_CLAUSE", typeof(string));
            dtFinOps.Columns.Add("PAYMENT_TERM", typeof(string));
            dtFinOps.Columns.Add("PO_NO", typeof(string));
            dtFinOps.Columns.Add("CONTIGENCY_TYPE", typeof(string));
            dtFinOps.Columns.Add("IS_VPOC_ORDER", typeof(string));
            dtFinOps.Columns.Add("DELIVERY_MONTH", typeof(string));
            dtFinOps.Columns.Add("STATUS1", typeof(string));
            dtFinOps.Columns.Add("AMOUNT2", typeof(string));
            dtFinOps.Columns.Add("STATUS2", typeof(string));
            dtFinOps.Columns.Add("ITEM_CODE", typeof(string));
            dtFinOps.Columns.Add("ITEM_DESC", typeof(string));
            dtFinOps.Columns.Add("ITEM_GROUP", typeof(string));
            dtFinOps.Columns.Add("ITEM_SUBGROUP", typeof(string));
            dtFinOps.Columns.Add("ITEM_PIVOT_GROUP", typeof(string));

            dtFinOps.Columns.Add("TYPE_ID", typeof(int));
            dtFinOps.Columns.Add("COST_TYPE_ID", typeof(int));
            dtFinOps.Columns.Add("CONTIGENCY_TYPE_ID", typeof(int));

            dtFinOps.Columns.Add("STATUS1_ID", typeof(int));
            dtFinOps.Columns.Add("STATUS2_ID", typeof(int));

            dtFinOps.Columns.Add("SR_NO", typeof(int));

            #endregion



            if (fileUploadfinOps.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadfinOps.PostedFile.FileName))
                {

                    if (Session["dsFinOpsDetail"] != null)
                        dsFinOpsDetail = (DataSet)Session["dsFinOpsDetail"];
                    else
                        dsFinOpsDetail = objFinOps.GetOpsTypes();

                    System.IO.StreamReader myReader = new System.IO.StreamReader(fileUploadfinOps.PostedFile.InputStream);
                    string csvData = myReader.ReadToEnd();

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtFinOps.Rows.Add();

                            int i = 0;
                            string dtColunValue = string.Empty;
                            foreach (string cell in row.Split(','))
                            {
                                dtColunValue = cell.Trim();

                                if (i <= (dtFinOps.Columns.Count - 1))
                                {
                                    if (!string.IsNullOrEmpty(dtColunValue))
                                        dtFinOps.Rows[dtFinOps.Rows.Count - 1][i + 1] = dtColunValue;
                                    else
                                        dtFinOps.Rows[dtFinOps.Rows.Count - 1][i + 1] = string.Empty;

                                    i++;
                                }
                            }
                        }
                    }
                }
            }


            if (dtFinOps.Rows.Count > 0)
            {
                dtFinOps.Rows.RemoveAt(0);
            }


            if (dtFinOps.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFinOps.Rows)
                {
                    count++;
                    dr["SR_NO"] = count;

                    if (dsFinOpsDetail.Tables.Count > 0)
                    {
                        int finOpsIDcounts = 0;
                        // FIN OPS DETAIL
                        if (dsFinOpsDetail.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drfo in dsFinOpsDetail.Tables[0].Select("JOB_NO='" + Convert.ToString(dr["JOB_NO"]).ToUpper() + "' AND MONTH='" + Convert.ToString(dr["MONTH"]) + "'"))
                            {
                                finOpsIDcounts++;
                                if (drfo["FIN_OPS_ID"] != DBNull.Value)
                                    dr["FIN_OPS_ID"] = Convert.ToInt32(drfo["FIN_OPS_ID"]);
                                else
                                    dr["FIN_OPS_ID"] = 0;
                            }
                        }
                        else
                        {
                            dr["FIN_OPS_ID"] = 0;
                        }

                        if (finOpsIDcounts == 0)
                            dr["FIN_OPS_ID"] = 0;

                        // TYPE
                        if (dsFinOpsDetail.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow drt in dsFinOpsDetail.Tables[1].Select("TYPE='" + Convert.ToString(dr["TYPE"]) + "'"))
                            {
                                if (drt["TYPE_ID"] != DBNull.Value)
                                    dr["TYPE_ID"] = Convert.ToInt32(drt["TYPE_ID"]);
                                else
                                    dr["TYPE_ID"] = 0;
                            }
                        }
                        else
                        {
                            dr["TYPE_ID"] = 0;
                        }

                        //COST TYPE
                        if (dsFinOpsDetail.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow drct in dsFinOpsDetail.Tables[2].Select("COST_TYPE='" + Convert.ToString(dr["COST_TYPE"]) + "'"))
                            {
                                if (drct["COST_TYPE_ID"] != DBNull.Value)
                                    dr["COST_TYPE_ID"] = Convert.ToInt32(drct["COST_TYPE_ID"]);
                                else
                                    dr["COST_TYPE_ID"] = 0;
                            }
                        }
                        else
                        {
                            dr["COST_TYPE_ID"] = 0;
                        }

                        //CONTIGENCY TYPE
                        if (dsFinOpsDetail.Tables[3].Rows.Count > 0)
                        {
                            foreach (DataRow drct in dsFinOpsDetail.Tables[3].Select("CONTIGENCY_TYPE='" + Convert.ToString(dr["CONTIGENCY_TYPE"]) + "'"))
                            {
                                if (drct["CONTIGENCY_TYPE_ID"] != DBNull.Value)
                                    dr["CONTIGENCY_TYPE_ID"] = Convert.ToInt32(drct["CONTIGENCY_TYPE_ID"]);
                                else
                                    dr["CONTIGENCY_TYPE_ID"] = 0;
                            }
                        }
                        else
                        {
                            dr["CONTIGENCY_TYPE_ID"] = 0;
                        }


                        //STATUS1
                        if (dsFinOpsDetail.Tables[4].Rows.Count > 0)
                        {
                            foreach (DataRow drct in dsFinOpsDetail.Tables[4].Select("STATUS1='" + Convert.ToString(dr["STATUS1"]) + "'"))
                            {
                                if (drct["STATUS1_ID"] != DBNull.Value)
                                    dr["STATUS1_ID"] = Convert.ToInt32(drct["STATUS1_ID"]);
                                else
                                    dr["STATUS1_ID"] = 0;
                            }
                        }
                        else
                        {
                            dr["STATUS1_ID"] = 0;
                        }

                        //STATUS2
                        if (dsFinOpsDetail.Tables[5].Rows.Count > 0)
                        {
                            foreach (DataRow drct in dsFinOpsDetail.Tables[5].Select("STATUS2='" + Convert.ToString(dr["STATUS2"]) + "'"))
                            {
                                if (drct["STATUS2_ID"] != DBNull.Value)
                                    dr["STATUS2_ID"] = Convert.ToInt32(drct["STATUS2_ID"]);
                                else
                                    dr["STATUS2_ID"] = 0;
                            }
                        }
                        else
                        {
                            dr["STATUS2_ID"] = 0;
                        }
                    }
                }
            }





            if (dtFinOps.Rows.Count > 0)
            {
                Session["dtFinOps"] = dtFinOps;
                gvFinOps.DataSource = dtFinOps;
                gvFinOps.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvFinOps.Rows.Count);
                lblRecords.Text = "Records[" + dtFinOps.Rows.Count + "], Already Exists[" + existedRecrdsCount + "]";
                hdExistedRecords.Value = existedRecrdsCount.ToString();
            }
            else
            {
                Session["dtFinOps"] = null;
                gvFinOps.DataSource = null;
                gvFinOps.DataBind();
                hdGVRowCount.Value = "0";
                hdExistedRecords.Value = "0";
                lblRecords.Text = "Records[0], Already Exists[0]";
                ExceptionMessage("No data found..!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private bool CheckNumeric(string value)
    {

        double n;
        bool isNumeric = double.TryParse(value, out n);
        return isNumeric;
    }

    private bool CheckDate(string value)
    {

        DateTime n;
        bool isDate = DateTime.TryParse(value, out n);
        return isDate;
    }

    private bool CheckMonth(string value)
    {
        checkYerVal = 0;
        checkMonthVal = 0;
        checkVal = 0;
        int count = 0;
        if (value.Contains("-"))
        {
            for (int i = 0; i < value.Count(); i++)
            {
                if (value[i].ToString() == "-")
                    count++;
            }

            if (count == 1)
            {
                checkVal = 1;
                string[] strtxt = value.Split('-');
                string year = Convert.ToString(strtxt[0]);
                string month = Convert.ToString(strtxt[1]);

                if (year.Length == 4)
                {
                    if (CheckNumeric(year) && Convert.ToInt32(year) >= 2000)
                        checkYerVal = 1;
                    else
                        checkYerVal = 0;
                }
                else
                    checkYerVal = 0;



                if (month.Length <= 2)
                {
                    if (CheckNumeric(month))
                    {
                        if (Convert.ToInt32(month) > 0 && Convert.ToInt32(month) <= 12)
                            checkMonthVal = 1;
                        else
                            checkMonthVal = 0;
                    }
                    else
                        checkMonthVal = 0;
                }
                else
                    checkMonthVal = 0;
            }
            else
                checkVal = 0;
        }
        else
            checkVal = 0;


        if (checkVal == 1 && checkYerVal == 1 && checkMonthVal == 1)
            return true;
        else
            return false;
    }

    private bool CheckHours(string value)
    {
        checkHourVal = 0;
        checkMinVal = 0;
        checkVal = 0;
        int count = 0;

        if (value.Contains(":"))
        {
            for (int i = 0; i < value.Count(); i++)
            {
                if (value[i].ToString() == ":")
                    count++;
            }

            if (count == 1)
            {
                checkVal = 1;
                string[] strtxt = value.Split(':');
                string hours = Convert.ToString(strtxt[0]);
                string minuts = Convert.ToString(strtxt[1]);

                if (CheckNumeric(hours))
                {
                    if (hours.Length <= 5)
                        checkHourVal = 1;
                    else
                        checkHourVal = 0;
                }
                else
                    checkHourVal = 0;

                if (CheckNumeric(minuts))
                {
                    if ((minuts.Length >= 1 && minuts.Length <= 2) && Convert.ToInt32(minuts) <= 59)
                        checkMinVal = 1;
                    else
                        checkMinVal = 0;
                }
                else
                    checkMinVal = 0;
            }
            else
                checkVal = 0;
        }
        else
            checkVal = 0;


        if (checkVal == 1 && checkHourVal == 1 && checkMinVal == 1)
            return true;
        else
            return false;
    }

    private void ImportFinOps()
    {
        try
        {
            string updateQuery = string.Empty;

            dsFinOpsDetail = GetFinPosDetails();

            string srNoForRemoval = string.Empty;
            int count = 0;

            DataTable dtTempFinOps = new DataTable();

            dtTempFinOps.Columns.Add("TAX_INVOICE_NO", typeof(string));
            dtTempFinOps.Columns.Add("TAX_INVOICE_DATE", typeof(string));
            dtTempFinOps.Columns.Add("MONTH", typeof(string));
            dtTempFinOps.Columns.Add("JOB_NO", typeof(string));
            dtTempFinOps.Columns.Add("COST_CENTER", typeof(string));
            dtTempFinOps.Columns.Add("BU", typeof(string));
            dtTempFinOps.Columns.Add("AMOUNT1", typeof(string));
            dtTempFinOps.Columns.Add("HOURS", typeof(string));
            dtTempFinOps.Columns.Add("PANELTY_CLAUSE", typeof(string));
            dtTempFinOps.Columns.Add("PAYMENT_TERM", typeof(string));
            dtTempFinOps.Columns.Add("PO_NO", typeof(string));
            dtTempFinOps.Columns.Add("IS_VPOC_ORDER", typeof(int));
            dtTempFinOps.Columns.Add("DELIVERY_MONTH", typeof(string));
            dtTempFinOps.Columns.Add("AMOUNT2", typeof(double));
            dtTempFinOps.Columns.Add("ITEM_CODE", typeof(string));
            dtTempFinOps.Columns.Add("ITEM_DESC", typeof(string));
            dtTempFinOps.Columns.Add("ITEM_GROUP", typeof(string));
            dtTempFinOps.Columns.Add("ITEM_SUBGROUP", typeof(string));
            dtTempFinOps.Columns.Add("ITEM_PIVOT_GROUP", typeof(string));

            dtTempFinOps.Columns.Add("TYPE_ID", typeof(int));
            dtTempFinOps.Columns.Add("COST_TYPE_ID", typeof(int));
            dtTempFinOps.Columns.Add("CONTIGENCY_TYPE_ID", typeof(int));

            dtTempFinOps.Columns.Add("STATUS1_ID", typeof(int));
            dtTempFinOps.Columns.Add("STATUS2_ID", typeof(int));

            dtTempFinOps.Columns.Add("CREATED_BY", typeof(int));

            int value = 0;
            foreach (GridViewRow gr in gvFinOps.Rows)
            {
                finOpsID = 0;
                taxInvoiceNo = string.Empty;
                taxInvoiceDate = string.Empty;
                month = string.Empty;
                jobNo = string.Empty;
                costCenter = string.Empty;
                BU = string.Empty;
                amount1 = 0;
                hours = string.Empty;
                paneltyClause = string.Empty;
                paymentTerm = string.Empty;
                poNo = string.Empty;
                isVPOCOrder = 0;
                deliveryMonth = string.Empty;
                amount2 = 0;
                itemCode = string.Empty;
                itemDesc = string.Empty;
                itemGroup = string.Empty;
                itemSubgroup = string.Empty;
                itemPivotGroup = string.Empty;
                typeID = 0;
                costTypeID = 0;
                contigencyTypeID = 0;
                status1ID = 0;
                status2ID = 0;
                SRNo = 0;

                typeCount = 0;
                costTypeCount = 0;
                contigencyTypeCount = 0;
                status1Count = 0;
                status2Count = 0;

                finOpsIDcounts = 0;


                DataRow dr = dtTempFinOps.NewRow();

                Label lblFinOpsID = (Label)gr.FindControl("lblFinOpsID");
                Label lblTaxInvoiceNo = (Label)gr.FindControl("lblTaxInvoiceNo");
                TextBox txtTaxInvoiceDate = (TextBox)gr.FindControl("txtTaxInvoiceDate");
                TextBox txtMonth = (TextBox)gr.FindControl("txtMonth");
                Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                Label lblCostCenter = (Label)gr.FindControl("lblCostCenter");

                Label lblTypeID = (Label)gr.FindControl("lblTypeID");
                DropDownList ddlType = (DropDownList)gr.FindControl("ddlType");

                Label lblCostTypeID = (Label)gr.FindControl("lblCostTypeID");
                DropDownList ddlCostType = (DropDownList)gr.FindControl("ddlCostType");

                Label lblBU = (Label)gr.FindControl("lblBU");
                TextBox txtAmount1 = (TextBox)gr.FindControl("txtAmount1");
                TextBox txtHours = (TextBox)gr.FindControl("txtHours");
                Label lblPaneltyClause = (Label)gr.FindControl("lblPaneltyClause");
                Label lblPaymentTerm = (Label)gr.FindControl("lblPaymentTerm");
                Label lblPoNo = (Label)gr.FindControl("lblPoNo");

                Label lblContigencyTypeID = (Label)gr.FindControl("lblContigencyTypeID");
                DropDownList ddlContigencyType = (DropDownList)gr.FindControl("ddlContigencyType");

                DropDownList ddlIsVPOCOrder = (DropDownList)gr.FindControl("ddlIsVPOCOrder");

                TextBox txtDeliveryMonth = (TextBox)gr.FindControl("txtDeliveryMonth");

                Label lblStatus1ID = (Label)gr.FindControl("lblStatus1ID");
                DropDownList ddlStatus1 = (DropDownList)gr.FindControl("ddlStatus1");

                TextBox txtAmount2 = (TextBox)gr.FindControl("txtAmount2");

                Label lblStatus2ID = (Label)gr.FindControl("lblStatus2ID");
                DropDownList ddlStatus2 = (DropDownList)gr.FindControl("ddlStatus2");

                Label lblItemCode = (Label)gr.FindControl("lblItemCode");
                Label lblItemDesc = (Label)gr.FindControl("lblItemDesc");
                Label lblItemGroup = (Label)gr.FindControl("lblItemGroup");
                Label lblItemSubgroup = (Label)gr.FindControl("lblItemSubgroup");
                Label lblItemPivotGroup = (Label)gr.FindControl("lblItemPivotGroup");


                Label lblSRNo = (Label)gr.FindControl("lblSRNo");
                Label lblValidationFlag = (Label)gr.FindControl("lblValidationFlag");


                Label lblTaxInvoiceDateValidationFlag = (Label)gr.FindControl("lblTaxInvoiceDateValidationFlag");
                Label lblMonthValidationFlag = (Label)gr.FindControl("lblMonthValidationFlag");
                Label lblTypeValidationFlag = (Label)gr.FindControl("lblTypeValidationFlag");
                Label lblCostTypeValidationFlag = (Label)gr.FindControl("lblCostTypeValidationFlag");
                Label lblAmount1ValidationFlag = (Label)gr.FindControl("lblAmount1ValidationFlag");
                Label lblHoursValidationFlag = (Label)gr.FindControl("lblHoursValidationFlag");
                Label lblContigencyTypeValidationFlag = (Label)gr.FindControl("lblContigencyTypeValidationFlag");
                Label lblIsVPOCOrderValidationFlag = (Label)gr.FindControl("lblIsVPOCOrderValidationFlag");
                Label lblDeliveryMonthValidationFlag = (Label)gr.FindControl("lblDeliveryMonthValidationFlag");
                Label lblStatus1ValidationFlag = (Label)gr.FindControl("lblStatus1ValidationFlag");
                Label lblAmount2ValidationFlag = (Label)gr.FindControl("lblAmount2ValidationFlag");
                Label lblStatus2ValidationFlag = (Label)gr.FindControl("lblStatus2ValidationFlag");




                if (dsFinOpsDetail.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drfo in dsFinOpsDetail.Tables[0].Select("JOB_NO='" + Convert.ToString(lblJobNo.Text).ToUpper() + "' AND MONTH='" + Convert.ToString(txtMonth.Text) + "'"))
                    {
                        finOpsIDcounts++;
                        if (drfo["FIN_OPS_ID"] != DBNull.Value)
                        {
                            lblFinOpsID.Text = Convert.ToString(drfo["FIN_OPS_ID"]);
                            finOpsID = Convert.ToInt32(lblFinOpsID.Text);
                        }
                        else
                        {
                            lblFinOpsID.Text = "0";
                            finOpsID = 0;
                        }
                    }
                }
                else
                {
                    lblFinOpsID.Text = "0";
                    finOpsID = 0;
                }

                if (finOpsIDcounts == 0)
                {
                    lblFinOpsID.Text = "0";
                    finOpsID = 0;
                }



                if (!string.IsNullOrEmpty(Convert.ToString(lblTaxInvoiceNo.Text)))
                    taxInvoiceNo = Convert.ToString(lblTaxInvoiceNo.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(txtTaxInvoiceDate.Text)))
                {
                    if (CheckDate(Convert.ToString(txtTaxInvoiceDate.Text)))
                        taxInvoiceDate = Convert.ToDateTime(txtTaxInvoiceDate.Text).ToString("yyyy-MM-dd");
                }

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonth.Text)))
                {
                    if (CheckMonth(Convert.ToString(txtMonth.Text)))
                        month = Convert.ToString(txtMonth.Text);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                    jobNo = Convert.ToString(lblJobNo.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblCostCenter.Text)))
                    costCenter = Convert.ToString(lblCostCenter.Text);

                if (ddlType.SelectedIndex > 0 || Convert.ToInt32(lblTypeID.Text) > 0)
                    typeID = Convert.ToInt32(ddlType.SelectedValue);

                if (ddlCostType.SelectedIndex > 0 || Convert.ToInt32(lblCostTypeID.Text) > 0)
                    costTypeID = Convert.ToInt32(ddlCostType.SelectedValue);

                if (!string.IsNullOrEmpty(Convert.ToString(lblBU.Text)))
                    BU = Convert.ToString(lblBU.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(txtAmount1.Text)))
                {
                    if (CheckNumeric(Convert.ToString(txtAmount1.Text)))
                        amount1 = Convert.ToDouble(txtAmount1.Text);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(txtHours.Text)))
                {
                    if (CheckHours(Convert.ToString(txtHours.Text)))
                        hours = Convert.ToString(txtHours.Text);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblPaneltyClause.Text)))
                    paneltyClause = Convert.ToString(lblPaneltyClause.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblPaymentTerm.Text)))
                    paymentTerm = Convert.ToString(lblPaymentTerm.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblPoNo.Text)))
                    poNo = Convert.ToString(lblPoNo.Text);

                if (ddlContigencyType.SelectedIndex > 0 || Convert.ToInt32(lblContigencyTypeID.Text) > 0)
                    contigencyTypeID = Convert.ToInt32(ddlContigencyType.SelectedValue);

                if (ddlIsVPOCOrder.SelectedItem.Text.ToUpper() == "YES" || ddlIsVPOCOrder.SelectedItem.Text.ToUpper() == "NO")
                {
                    if (ddlIsVPOCOrder.SelectedItem.Text.ToUpper() == "YES")
                        isVPOCOrder = 1;
                    else if (ddlIsVPOCOrder.SelectedItem.Text.ToUpper() == "NO")
                        isVPOCOrder = 0;
                }


                if (!string.IsNullOrEmpty(Convert.ToString(txtDeliveryMonth.Text)))
                {
                    if (CheckMonth(Convert.ToString(txtDeliveryMonth.Text)))
                        deliveryMonth = Convert.ToString(txtDeliveryMonth.Text);
                }

                if (ddlStatus1.SelectedIndex > 0 || Convert.ToInt32(lblStatus1ID.Text) > 0)
                    status1ID = Convert.ToInt32(ddlStatus1.SelectedValue);

                if (!string.IsNullOrEmpty(Convert.ToString(txtAmount2.Text)))
                {
                    if (CheckNumeric(Convert.ToString(txtAmount2.Text)))
                        amount2 = Convert.ToDouble(txtAmount2.Text);
                }

                if (ddlStatus2.SelectedIndex > 0 || Convert.ToInt32(lblStatus2ID.Text) > 0)
                    status2ID = Convert.ToInt32(ddlStatus2.SelectedValue);

                if (!string.IsNullOrEmpty(Convert.ToString(lblItemCode.Text)))
                    itemCode = Convert.ToString(lblItemCode.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblItemDesc.Text)))
                    itemDesc = Convert.ToString(lblItemDesc.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblItemGroup.Text)))
                    itemGroup = Convert.ToString(lblItemGroup.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblItemSubgroup.Text)))
                    itemSubgroup = Convert.ToString(lblItemSubgroup.Text);

                if (!string.IsNullOrEmpty(Convert.ToString(lblItemPivotGroup.Text)))
                    itemPivotGroup = Convert.ToString(lblItemPivotGroup.Text);


                #region MyRegion


                //if (dsFinOpsDetail.Tables.Count > 0)
                //{
                // TYPE-------------------------------------------------------------
                //if (dsFinOpsDetail.Tables[1].Rows.Count > 0)
                //{
                //    foreach (DataRow drt in dsFinOpsDetail.Tables[1].Select("TYPE='" + Convert.ToString(txtType.Text) + "'"))
                //    {
                //        if (drt["TYPE_ID"] != DBNull.Value)
                //            typeID = Convert.ToInt32(drt["TYPE_ID"]);
                //        else
                //            typeID = 0;
                //    }
                //}
                //else
                //    typeID = 0;


                //-------------------------------------------------------------------



                //COST TYPE----------------------------------------------------------
                //if (dsFinOpsDetail.Tables[2].Rows.Count > 0)
                //{
                //    foreach (DataRow drct in dsFinOpsDetail.Tables[2].Select("COST_TYPE='" + Convert.ToString(txtCostType.Text) + "'"))
                //    {
                //        if (drct["COST_TYPE_ID"] != DBNull.Value)
                //            costTypeID = Convert.ToInt32(drct["COST_TYPE_ID"]);
                //        else
                //            costTypeID = 0;
                //    }
                //}
                //else
                //    costTypeID = 0;


                //------------------------------------------------------------------



                //CONTIGENCY TYPE---------------------------------------------------
                //if (dsFinOpsDetail.Tables[3].Rows.Count > 0)
                //{
                //    foreach (DataRow drct in dsFinOpsDetail.Tables[3].Select("CONTIGENCY_TYPE='" + Convert.ToString(txtContigencyType.Text) + "'"))
                //    {
                //        if (drct["CONTIGENCY_TYPE_ID"] != DBNull.Value)
                //            contigencyTypeID = Convert.ToInt32(drct["CONTIGENCY_TYPE_ID"]);
                //        else
                //            contigencyTypeID = 0;
                //    }
                //}
                //else
                //    contigencyTypeID = 0;


                //-------------------------------------------------------------------



                //STATUS1------------------------------------------------------------
                //if (dsFinOpsDetail.Tables[4].Rows.Count > 0)
                //{
                //    foreach (DataRow drct in dsFinOpsDetail.Tables[4].Select("STATUS1='" + Convert.ToString(txtStatus1.Text) + "'"))
                //    {
                //        if (drct["STATUS1_ID"] != DBNull.Value)
                //            status1ID = Convert.ToInt32(drct["STATUS1_ID"]);
                //        else
                //            status1ID = 0;
                //    }
                //}
                //else
                //    status1ID = 0;


                //---------------------------------------------------------------------



                //STATUS2--------------------------------------------------------------
                //if (dsFinOpsDetail.Tables[5].Rows.Count > 0)
                //{
                //    foreach (DataRow drct in dsFinOpsDetail.Tables[5].Select("STATUS2='" + Convert.ToString(txtStatus2.Text) + "'"))
                //    {
                //        if (drct["STATUS2_ID"] != DBNull.Value)
                //            status2ID = Convert.ToInt32(drct["STATUS2_ID"]);
                //        else
                //            status2ID = 0;
                //    }
                //}
                //else
                //    status2ID = 0;


                //----------------------------------------------------------------------

                //}
                //else
                //{
                //    typeID = 0;
                //    costTypeID = 0;
                //    contigencyTypeID = 0;
                //    status1ID = 0;
                //    status2ID = 0;
                //}
                #endregion

                if (Convert.ToInt32(lblSRNo.Text) > 0)
                    SRNo = Convert.ToInt32(lblSRNo.Text);




                if (lblTaxInvoiceDateValidationFlag.Text == "0" &&
                        lblMonthValidationFlag.Text == "0" &&
                        lblTypeValidationFlag.Text == "0" &&
                        lblCostTypeValidationFlag.Text == "0" &&
                        lblAmount1ValidationFlag.Text == "0" &&
                        lblHoursValidationFlag.Text == "0" &&
                        lblContigencyTypeValidationFlag.Text == "0" &&
                        lblIsVPOCOrderValidationFlag.Text == "0" &&
                        lblDeliveryMonthValidationFlag.Text == "0" &&
                        lblStatus1ValidationFlag.Text == "0" &&
                        lblAmount2ValidationFlag.Text == "0" &&
                        lblStatus2ValidationFlag.Text == "0")
                {
                    lblValidationFlag.Text = "0";
                }
                else
                {
                    lblValidationFlag.Text = "1";
                }






                if (Convert.ToInt32(lblValidationFlag.Text) == 0)
                {
                    if (finOpsID > 0)
                    {
                        count++;
                        srNoForRemoval += SRNo + ",";
                        updateQuery += "UPDATE tblFinOps SET TAX_INVOICE_NO='" + taxInvoiceNo + "',TAX_INVOICE_DATE='" + taxInvoiceDate + "',MONTH='" + month + "',JOB_NO='" + jobNo + "',COST_CENTER='" + costCenter + "',TYPE_ID=" + typeID + ",COST_TYPE_ID=" + costTypeID + ",BU='" + BU + "',AMOUNT1='" + amount1 + "',HOURS='" + hours + "',PANELTY_CLAUSE='" + paneltyClause + "',PAYMENT_TERM='" + paymentTerm + "',PO_NO='" + poNo + "',CONTIGENCY_TYPE_ID=" + contigencyTypeID + ",IS_VPOC_ORDER=" + isVPOCOrder + ",DELIVERY_MONTH='" + deliveryMonth + "',STATUS1_ID=" + status1ID + ",AMOUNT2='" + amount2 + "',STATUS2_ID=" + status2ID + ",ITEM_CODE='" + itemCode + "',ITEM_DESC='" + itemDesc + "',ITEM_GROUP='" + itemGroup + "',ITEM_SUBGROUP='" + itemSubgroup + "',ITEM_PIVOT_GROUP='" + itemPivotGroup + "',MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",MODIFIED_ON=GETDATE() WHERE FIN_OPS_ID=" + finOpsID + ";" + Environment.NewLine;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(jobNo))
                        {
                            count++;
                            srNoForRemoval += SRNo + ",";

                            dr["TAX_INVOICE_NO"] = taxInvoiceNo;
                            dr["TAX_INVOICE_DATE"] = taxInvoiceDate;
                            dr["MONTH"] = month;
                            dr["JOB_NO"] = jobNo;
                            dr["COST_CENTER"] = costCenter;
                            dr["BU"] = BU;
                            dr["AMOUNT1"] = amount1;
                            dr["HOURS"] = hours;
                            dr["PANELTY_CLAUSE"] = paneltyClause;
                            dr["PAYMENT_TERM"] = paymentTerm;
                            dr["PO_NO"] = poNo;
                            dr["IS_VPOC_ORDER"] = isVPOCOrder;
                            dr["DELIVERY_MONTH"] = deliveryMonth;
                            dr["AMOUNT2"] = amount2;
                            dr["ITEM_CODE"] = itemCode;
                            dr["ITEM_DESC"] = itemDesc;
                            dr["ITEM_GROUP"] = itemGroup;
                            dr["ITEM_SUBGROUP"] = itemSubgroup;
                            dr["ITEM_PIVOT_GROUP"] = itemPivotGroup;
                            dr["CREATED_BY"] = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                            dr["TYPE_ID"] = typeID;
                            dr["COST_TYPE_ID"] = costTypeID;
                            dr["CONTIGENCY_TYPE_ID"] = contigencyTypeID;
                            dr["STATUS1_ID"] = status1ID;
                            dr["STATUS2_ID"] = status2ID;

                            dtTempFinOps.Rows.Add(dr);
                        }
                    }
                }
            }

            if (Convert.ToInt32(hdReplacementFlag.Value) > 0)
            {
                if (!string.IsNullOrEmpty(updateQuery))
                    updateQuery = updateQuery.TrimEnd(';').Trim();
            }
            else
            {
                updateQuery = string.Empty;
            }


            value = objFinOps.InsertFinOps(dtTempFinOps, updateQuery);

            if (value > 0)
            {
                SuccessMessage(count + " Records Imported successfully.");
                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvFinOps.DataSource = null;
                    gvFinOps.DataBind();
                    lblRecords.Text = "Records[" + gvFinOps.Rows.Count + "]";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveAndBind(string srNoForRemoval)
    {
        try
        {
            dt1 = (DataTable)Session["dtFinOps"];
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
                gvFinOps.DataSource = dt1;
                gvFinOps.DataBind();
                hdGVRowCount.Value = Convert.ToString(gvFinOps.Rows.Count);
            }
            else
            {
                gvFinOps.DataSource = null;
                gvFinOps.DataBind();
                hdGVRowCount.Value = "0";
            }
            lblRecords.Text = "Records[" + gvFinOps.Rows.Count + "]";
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
