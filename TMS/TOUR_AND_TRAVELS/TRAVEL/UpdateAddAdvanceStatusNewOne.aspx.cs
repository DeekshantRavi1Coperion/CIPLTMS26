using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net.Mime;
using Org.BouncyCastle.Asn1.Ocsp;

public partial class TOUR_AND_TRAVELS_TRAVEL_UpdateAddAdvanceStatusNewOne : System.Web.UI.Page
{
    #region VARIABLES[=============]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Common objCommon = new BAL.Common();
    string isApproved = string.Empty;
    string sanctionNo = string.Empty;

    #endregion

    #region EVENTS[================]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["CHK_ACC"] = null;
            ValidateAndLoginAndApproveDelete();
        }
    }


    protected void btnTourList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/TourAdditionalAdvanceList.aspx?requestid=" + Convert.ToString(Request.QueryString["requestid"]));

    }

    #endregion


    #region METHODS[===============]

    private void ValidateAndLoginAndApproveDelete()

    {
        try
        {

            DataSet ds = new DataSet();
            string userName = string.Empty;
            string password = string.Empty;
            string tourSanctionNumber = string.Empty;

            int requestID = Convert.ToInt32(Request.QueryString["requestid"]);
            //int tourID = Convert.ToInt32(Request.QueryString["tourid"]);
            userName = Convert.ToString(Request.QueryString["ud"]);
            password = Convert.ToString(Request.QueryString["pd"]);
            ds = objCommon.ValidateAndLogin(userName, password);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ModalPopupExtender1.Show();

                Session["EMP_RECORD_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["EMP_RECORD_ID"]);
                Session["EMPLOYEE_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                Session["USER_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_NAME"]);
                Session["PASSWORD"] = Convert.ToString(ds.Tables[0].Rows[0]["PASSWORD"]);
                Session["EMAIL_ID"] = Convert.ToString(ds.Tables[0].Rows[0]["EMAIL_ID"]);
                Session["USER_TYPE"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_TYPE"]);
                Session["IS_TEAMLEADER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["IS_TEAMLEADER"]);
                Session["TEAMLEADER_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TEAMLEADER_ID"]);
                Session["DEPARTMENT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]);
                Session["TIMESHEET_DEPT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TIMESHEET_DEPT_ID"]);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    Session["TEAMMEMBERS"] = ds.Tables[1];
                }

                if (ds.Tables.Count > 0)
                {
                    Session["USERINFO"] = ds;
                }
                DataSet dsAddAdvanceSanctionno = objTourAndTravels.GetAddAdvSanctionNoStatusIDByRequestID(requestID);

                //int RequeststatusID = Convert.ToInt32(Request.QueryString["requeststatusid"]);
                int RequeststatusID = Convert.ToInt32(dsAddAdvanceSanctionno.Tables[0].Rows[0]["REQUEST_STATUS_ID"]);
                int createdByID = Convert.ToInt32(dsAddAdvanceSanctionno.Tables[0].Rows[0]["CREATED_BY"]);
                int approvedById = Convert.ToInt32(dsAddAdvanceSanctionno.Tables[0].Rows[0]["HOD_APPROVED_BY"]);
                int teamLeaderID = Convert.ToInt32(dsAddAdvanceSanctionno.Tables[0].Rows[0]["TEAMLEADER_ID"]);
                tourSanctionNumber = (dsAddAdvanceSanctionno.Tables[0].Rows[0]["TOUR_SANCTION_NO"]).ToString();
                sanctionNo = tourSanctionNumber;

                if (RequeststatusID == (int)TandTAllStatus.EnumTourStatus.New)
                {
                    //if (string.IsNullOrEmpty(Convert.ToString(dsAddAdvanceSanctionno.Tables[0].Rows[0]["HOD_APPROVED_BY"]))
                    //      && Convert.ToInt32(Session["EMP_RECORD_ID"]) == teamLeaderID)
                    if (Convert.ToString(dsAddAdvanceSanctionno.Tables[0].Rows[0]["HOD_APPROVED_BY"]) == "0"
                     && Convert.ToInt32(Session["EMP_RECORD_ID"]) == teamLeaderID)
                    {
                        int actID = Convert.ToInt32(Request.QueryString["actid"]);
                        UpdateAddAdvRequestStatus(actID);
                    }
                    else
                    {
                        SuccessMessage("This request is already approved by HOD.");
                    }
                }

                else if (RequeststatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                {
                    SuccessMessage("This request is already approved by HOD.");
                }


                else if (RequeststatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                {
                    SuccessMessage("This request is already deleted.");
                }

                else if (RequeststatusID == (int)TandTAllStatus.EnumTourStatus.Cancelled)
                {
                    SuccessMessage("This request is already cancelled.");
                }



            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }

    }



    private void UpdateAddAdvRequestStatus(int actID)
    {
        try
        {
            int requestID = Convert.ToInt32(Request.QueryString["requestid"]);
            string tourNO = Convert.ToString(Request.QueryString["tourno"]);
            //string tourSanctionNumber = Convert.ToString(Request.QueryString["toursanctionnumber"]);
            //int empRecordID = Convert.ToInt32(Request.QueryString["emprecordid"]);
            int reqStatusID = Convert.ToInt32(Request.QueryString["requeststatusid"]);
           
            
            int currentUserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int teamLeaderID = Convert.ToInt32(Request.QueryString["tlemprecordid"]); //Convert.ToInt32(Session["TEAMLEADER_ID"]);
            string remarks = string.Empty;

            string approvedMsg = string.Empty;

            int insertVal = 0;
            int sentMailVal = 0;
            int retStatusID = 0;

            
            AddAdvStatusUpdate objUT = new AddAdvStatusUpdate();
            string returnVal = objUT.UpdateAddAdvanceStatus(actID,
                                                      requestID,
                                                      //createdByID,
                                                      reqStatusID,
                                                      remarks,
                                                      teamLeaderID,
                                                      currentUserID,
                                                      0);

            if (returnVal.Length > 50)
            {
                ExceptionMessage(returnVal);
                return;
            }
            else
            {
                insertVal = Convert.ToInt32(returnVal.Split(':')[0]);
                sentMailVal = Convert.ToInt32(returnVal.Split(':')[1]);
                retStatusID = Convert.ToInt32(returnVal.Split(':')[2]);
                isApproved = Convert.ToString(returnVal.Split(':')[3]);
            }
            reqStatusID = Convert.ToInt32(Request.QueryString["requeststatusid"]);


            if (!string.IsNullOrEmpty(isApproved))
            {
                approvedMsg = "Additional Advance Request No. : AAR" + requestID + " approved  ";
            }
            else
            {
                approvedMsg = "Additional Advance Request No : AAR" + requestID + " approved  ";
            }

            if (insertVal > 0)
            {
                if (sentMailVal > 0)
                {
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved )
                    {
                        SuccessMessage(approvedMsg + "and mail sent successfully!");
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    {
                        SuccessMessage("Additional Advance Request ID: AAR" + requestID + " deleted and mail sent successfully!");
                        if(currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Additional Advance Request ID: AAR" + requestID + " rejected and mail sent successfully!");
                        }
                     


                    }
                }
                else
                {
                    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
                    {
                        SuccessMessage(approvedMsg + "successfully, please resend e-mail from Tour Information List!");
                    }
                    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
                    {
                        SuccessMessage("Additional Advance Request ID: AAR" + requestID + " deleted successfully, please resend e-mail from Tour Information List.");
                        if (currentUserID == teamLeaderID)
                        {
                            SuccessMessage("Additional Advance Request ID: AAR" + requestID + " rejected successfully, please resend e-mail from Tour Information List.");
                        }
                    }
                }
            }

            //if (returnVal == "1" && returnVal != "0")
            //{
            //    sentMailVal = 1;
            //    retStatusID = 3;
            //    if (reqStatusID != 3 )
            //    {

            //        approvedMsg = "Additional Advance Request ID: '" + requestID + "' for Tour No.: '" + tourNO + "' and Sanction No.: '" + sanctionNo + "' has been approved";

            //        retStatusID = 2;
            //    }


            //}

            //if (sentMailVal > 0)
            //{
            //    if (retStatusID == (int)TandTAllStatus.EnumTourStatus.HODApproved)
            //    {
            //        SuccessMessage(approvedMsg + " and mail sent successfully!");
            //    }
            //    else if (retStatusID == (int)TandTAllStatus.EnumTourStatus.Deleted)
            //    {
            //        SuccessMessage("Additional Advance Request ID: '" + requestID + "' deleted and mail sent successfully!");
            //    }
            //}



            else
            {
                ExceptionMessage("Please try again!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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