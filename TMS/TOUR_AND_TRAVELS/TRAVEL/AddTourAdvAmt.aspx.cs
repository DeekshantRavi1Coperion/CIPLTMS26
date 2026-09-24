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
public partial class TOUR_AND_TRAVELS_TRAVEL_AddTourAdvAmt : System.Web.UI.Page
{


    #region VARIABLES[==================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataTable dtTourSanctionDetails = new DataTable();
    DataSet dsCurrency = new DataSet();
    DataSet dsSanctionNo = new DataSet();

    DataSet dsOpenRequest  = new DataSet();

    int tourID = 0;
    int curr = 0;

    int empRecordID = 0;
    int emp_id = 0;
    string tourSanctionNo = string.Empty;
    string startDate = string.Empty;
    string endDate = string.Empty;
    string remarks = string.Empty;
    string sanctionNo = string.Empty;
    string tourNo = string.Empty;
    string custVendName = string.Empty;
    string placeOfVisit = string.Empty;

    double advanceTaken = 0;
    int advanceTakenCurrencyID = 0;
    double additionalAdvanceRequired = 0;
    int additionalAdvReqCurrencyID = 0;



    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {



            if (!IsPostBack)
            {

                hdConfirmValue.Value = "0";

                Session["EMP_DETAIL"] = null; 

                hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = Convert.ToString(hdStartDate.Value);

                hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = Convert.ToString(hdEndDate.Value);

                Session["SANCTION_NO_DETAILS"] = null;
                BindSanctionNo();
                BindCurrency();
                

            }
            
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }


    }
        protected void ddlSanctionNo_SelectedIndexChanged(object sender, EventArgs e)
        {
        lblMsg.Text = string.Empty;
        BindTourSantionDetails();
        GetNewAddAdvanceRequestByEmpRecordID();

    }
        private void BindTourSantionDetails()
        {
            try
            {
                if (ddlSanctionNo.SelectedIndex > 0)
                {
                    dtTourSanctionDetails = (DataTable)Session["SANCTION_NO_DETAILS"];
                    if (dtTourSanctionDetails.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTourSanctionDetails.Select("TOUR_SANCTION_NO='" + Convert.ToString(ddlSanctionNo.SelectedItem.Text) + "'"))
                        {
                            hdTourID.Value = Convert.ToString(dr["TOUR_ID"]);
                            txtTourNo.Text = Convert.ToString(dr["TOUR_NO"]);
                            txtCustVendName.Text = Convert.ToString(dr["CUST_VEND_NAME"]);
                            txtPlaceOfVisit.Text = Convert.ToString(dr["PLACE_OF_VISIT"]);

                            hdStartDate.Value = Convert.ToDateTime(dr["START_DATE"]).ToString("dd-MMM-yyyy");
                            txtStartDate.Text = Convert.ToString(hdStartDate.Value);

                            hdEndDate.Value = Convert.ToDateTime(dr["END_DATE"]).ToString("dd-MMM-yyyy");
                            txtEndDate.Text = Convert.ToString(hdEndDate.Value);

                            txtPrevAmt.Text = Convert.ToString(dr["ADVANCE_AMT"]);


                        

                        ddlAdvanceCurrency.SelectedValue = Convert.ToString(dr["ADVANCE_CURRENCY"]);
                        ddlPrevAmt.SelectedValue = Convert.ToString(dr["ADVANCE_CURRENCY"]);

                        //curr = Convert.ToInt32(dr["ADVANCE_CURRENCY"]);



                        //if (Convert.ToString(ddlSanctionNo.SelectedValue) != "")
                        //{
                        //    ddlPrevAmt.SelectedValue = Convert.ToString(dr["ADVANCE_CURRENCY"]);
                        //}
                       



                        //txtDays.Text = (Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text)).Days.ToString(); 
                    }
                    }
                }
                else
                {
                    //Reset();
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString());
                return;
            }
        }

    #region METHODS[====================]

    private void BindSanctionNo()
    {
        try
        {
            int travellerEmpRecordId = 0;
            
            if ((Session["EMP_RECORD_ID"] != null) && ((int)Session["EMP_RECORD_ID"] != 0))
            {
                travellerEmpRecordId = (int)Session["EMP_RECORD_ID"];
                
            }
            else
            {
                travellerEmpRecordId = 0;
            }

            dsSanctionNo = objTourAndTravels.GetTourSanctionDetailsForAddAdv(travellerEmpRecordId); 

            if (dsSanctionNo.Tables.Count > 0 && dsSanctionNo.Tables[0].Rows.Count > 0)
            {
                Session["SANCTION_NO_DETAILS"] = dsSanctionNo.Tables[0];

                ddlSanctionNo.DataSource = dsSanctionNo.Tables[0];
                ddlSanctionNo.DataTextField = "TOUR_SANCTION_NO";
                ddlSanctionNo.DataValueField = "TOUR_SANCTION_NO";
                ddlSanctionNo.DataBind();
                ddlSanctionNo.Items.Insert(0, "Select");
                ddlSanctionNo.SelectedIndex = 0;
            }
            else
            {
                Session["SANCTION_NO_DETAILS"] = null;
                ddlSanctionNo.Items.Insert(0, "Select");
                ddlSanctionNo.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCurrency()
    {
        try
        {


            dsCurrency = objTourAndTravels.GetPrimaryDetails("sp_get_currency_list");
            if (dsCurrency.Tables.Count > 0 && dsCurrency.Tables[0].Rows.Count > 0)
            {
                ddlPrevAmt.DataSource = dsCurrency.Tables[0];
                ddlPrevAmt.DataTextField = "CURRENCY_CODE";
                ddlPrevAmt.DataValueField = "CURRENCY_ID";
                ddlPrevAmt.Items.Insert(0, "Select");
                ddlPrevAmt.DataBind();
                ddlPrevAmt.SelectedValue = "68";

                ddlAdvanceCurrency.DataSource = dsCurrency.Tables[0];
                ddlAdvanceCurrency.DataTextField = "CURRENCY_CODE";
                ddlAdvanceCurrency.DataValueField = "CURRENCY_ID";
                ddlAdvanceCurrency.Items.Insert(0, "Select");
                ddlAdvanceCurrency.DataBind();
                ddlAdvanceCurrency.SelectedValue = "68";
            } 
        }
    
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {

            if (GetNewAddAdvanceRequestByEmpRecordID())
            {
                InsertAddAdvanceRequestDetails();
            }
      }

    }

    private bool GetNewAddAdvanceRequestByEmpRecordID()
    {
        bool isGood = true;
        bool isBad = false;
        try
        {
            string sanctionNo = (ddlSanctionNo.SelectedValue).ToString();
            dsOpenRequest = objTourAndTravels.GetOpenAdvanceRequestByCreator(sanctionNo);
            if (dsOpenRequest.Tables.Count > 0 && dsOpenRequest.Tables[0].Rows.Count >= 1)
            {
                gvOpenTours.DataSource = dsOpenRequest.Tables[0];
                gvOpenTours.DataBind();

                lblOpenTours.Text = "Records[" + dsOpenRequest.Tables[0].Rows.Count + "]";
                //lblOpenToursMsg.Text = "You have [" + dsOpenRequest.Tables[0].Rows.Count + "]  open tours. You are allowed to have only 1 Open Tour. Please submit Travel statment of Open Torus, then you will be able to create Tour!";
                lblOpenToursMsg.Text = "The system does not allow to create new request because you already have a request for this Tour Number .Please modify the existing request";
                ModalPopupExtender4.Show();
                //pnlPopupOpentours.Style["display"] = "block";
                return isBad;
            }
            else
            {
                gvOpenTours.DataSource = null;
                gvOpenTours.DataBind();
                return isGood;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return isBad;
        }
    }

    private void InsertAddAdvanceRequestDetails()
    {
        try
        {

            if (ddlSanctionNo.SelectedIndex > 0)
                sanctionNo = Convert.ToString(ddlSanctionNo.SelectedValue);
            else
                sanctionNo = string.Empty; 


            if (!string.IsNullOrEmpty(txtTourNo.Text))
                tourNo = txtTourNo.Text;
            else
                tourNo = string.Empty;


            if (!string.IsNullOrEmpty(txtCustVendName.Text))
                custVendName = txtCustVendName.Text;
            else
                custVendName = string.Empty;


            if (!string.IsNullOrEmpty(txtPlaceOfVisit.Text))
                placeOfVisit = txtPlaceOfVisit.Text;
            else
                placeOfVisit = string.Empty;


            if (!string.IsNullOrEmpty(hdStartDate.Value))
                startDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(hdEndDate.Value))
                endDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;




            if (!string.IsNullOrEmpty(txtPrevAmt.Text))
                advanceTaken = Convert.ToDouble(txtPrevAmt.Text);
            else
                advanceTaken = 0;

            if (ddlPrevAmt.SelectedIndex > 0)
                advanceTakenCurrencyID = Convert.ToInt32(ddlPrevAmt.SelectedValue);
            else
                advanceTakenCurrencyID = 0;


            if (!string.IsNullOrEmpty(txtAdvanceAmt.Text))
                additionalAdvanceRequired = Convert.ToDouble(txtAdvanceAmt.Text);
            else
                additionalAdvanceRequired = 0;


            if (ddlAdvanceCurrency.SelectedIndex > 0)
                additionalAdvReqCurrencyID = Convert.ToInt32(ddlAdvanceCurrency.SelectedValue);
            else
                additionalAdvReqCurrencyID = 0;


            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;
            else
                remarks = string.Empty;


            //if (Convert.ToDateTime(hdStartDate.Value).Date > Convert.ToDateTime(hdEndDate.Value).Date)
            //{
            //    lblDateMsg.Visible = true;
            //    lblDateMsg.Text = "Start date must be smaller or equal to end date.";
            //    return;
            //}


            if(Session["EMP_RECORD_ID"] != null)
                empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            else
                empRecordID = 0;


          
            int value = objTourAndTravels.InsertAddAdvanceInformation(0,sanctionNo ,tourNo ,custVendName, placeOfVisit ,
                startDate , endDate, advanceTaken, advanceTakenCurrencyID, additionalAdvanceRequired, 
                additionalAdvReqCurrencyID, remarks, empRecordID);


            if (value > 0)
            {

                AdditionalAdvanceSendMail tsm = new AdditionalAdvanceSendMail();
                string mailSentValue = tsm.SendMail(value);
                //string mailSentValue = tsm.SendMail(value);
                

                if (!string.IsNullOrEmpty(mailSentValue))
                {
                    int val = objTourAndTravels.UpdateAddAdvInfoMailStatus(1, value, 0);
                    SuccessMessage("Additional Advance request No. '" + mailSentValue + "' added and mail sent successfully.");
                }
                else
                {
                    SuccessMessage("Additional Advance request No.'" + mailSentValue + "' added successfully, and resend an approval email from Tour Information List.");
                }
                Reset();
                

            }
            else
            {
                ExceptionMessage("Please try again!");
            }

        }
        catch (Exception ex)
        {

            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/TourAdditionalAdvanceList.aspx");
    }

    private void Reset()
    {
        ddlSanctionNo.SelectedIndex = 0;
        txtTourNo.Text = string.Empty;
        txtCustVendName.Text = string.Empty;

        txtPlaceOfVisit.Text = string.Empty;
        
        txtStartDate.Text = hdStartDate.Value;
        txtEndDate.Text = hdEndDate.Value;


        hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtStartDate.Text = hdStartDate.Value;
        txtEndDate.Text = hdEndDate.Value;

        txtPrevAmt.Text = string.Empty;
        ddlPrevAmt.SelectedValue = "68";
        //ddlPrevAmt.SelectedIndex = 68;

        txtAdvanceAmt.Text = string.Empty;
        //ddlAdvanceCurrency.SelectedIndex = 68;
        ddlAdvanceCurrency.SelectedValue = "68";

        txtRemarks.Text = string.Empty;
        
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