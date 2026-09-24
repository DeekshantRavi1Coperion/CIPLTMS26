using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TOUR_AND_TRAVELS_TOUR_EXPENSE_TourExpense : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    TourExpenseSendMail objTourExpenseSendMail = new TourExpenseSendMail();
    DataSet dsSanctionNo = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsTravelStmtInfo = new DataSet();

    private string _employeeName = "";
    private string _tourSanctionNumber = "";

    private int tourID = 0;
    private string attachment1FileName = string.Empty;
    private string attachment2FileName = string.Empty;
    private double airTicketAmt = 0;
    private double hotelAmt = 0;
    private double taxiAmt = 0;
    private double othersAmt = 0;
    private double totalAmt = 0;
    private int totalAmtCurrencyID = 0;
    private string remarks = string.Empty;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdConfirmValue.Value = "0";
                txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                BindCurrency();
                txtDays.Text = (Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text)).Days.ToString();
            }
        }
        else Response.Redirect("~/Login.aspx");
    }

    protected void btnGetTourSanctionNo_Click(object sender, EventArgs e)
    {
        mpeTourSanctionNo.Show();
        GetTourSanctionDetail();
    }

    protected void btnbtnSearchTourSanctionNo_Click(object sender, EventArgs e)
    {
        GetTourSanctionDetail();
        mpeTourSanctionNo.Show();
    }

    protected void gvTourSanctionNo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblTourId = gvTourSanctionNo.Rows[rowindex].FindControl("lblTourId") as Label;
                Label lblTourNo = gvTourSanctionNo.Rows[rowindex].FindControl("lblTourNo") as Label;
                Label lblTourSanctionNo = gvTourSanctionNo.Rows[rowindex].FindControl("lblTourSanctionNo") as Label;
                Label lblEmployeeName = gvTourSanctionNo.Rows[rowindex].FindControl("lblEmployeeName") as Label;
                Label lblEmployeeId = gvTourSanctionNo.Rows[rowindex].FindControl("lblEmployeeId") as Label;
                Label lblDesignation = gvTourSanctionNo.Rows[rowindex].FindControl("lblDesignation") as Label;
                Label lblStartDate = gvTourSanctionNo.Rows[rowindex].FindControl("lblStartDate") as Label;
                Label lblEndDate = gvTourSanctionNo.Rows[rowindex].FindControl("lblEndDate") as Label;
                Label lblCustVendName = gvTourSanctionNo.Rows[rowindex].FindControl("lblCustVendName") as Label;
                Label lblPlaceOfVisit = gvTourSanctionNo.Rows[rowindex].FindControl("lblPlaceOfVisit") as Label;
                Label lblVisitType = gvTourSanctionNo.Rows[rowindex].FindControl("lblVisitType") as Label;
                Label lblJobNo = gvTourSanctionNo.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblBusSegment = gvTourSanctionNo.Rows[rowindex].FindControl("lblBusSegment") as Label;
                Label lblAdvanceAmt = gvTourSanctionNo.Rows[rowindex].FindControl("lblAdvanceAmt") as Label;
                Label lblAdvanceCurrencyID = gvTourSanctionNo.Rows[rowindex].FindControl("lblAdvanceCurrencyID") as Label;
                Label lblCurrencyCode = gvTourSanctionNo.Rows[rowindex].FindControl("lblCurrencyCode") as Label;

                ViewState["TOUR_ID"] = lblTourId.Text;

                if (!string.IsNullOrEmpty(lblTourSanctionNo.Text))
                    txtTourSanctionNo.Text = lblTourSanctionNo.Text;

                if (!string.IsNullOrEmpty(lblTourNo.Text))
                    txtTourNo.Text = lblTourNo.Text;

                if (!string.IsNullOrEmpty(lblEmployeeName.Text))
                    txtEmployeeName.Text = lblEmployeeName.Text;

                if (!string.IsNullOrEmpty(lblEmployeeId.Text))
                    txtEmployeeID.Text = lblEmployeeId.Text;

                txtStartDate.Text = Convert.ToDateTime(lblStartDate.Text).ToString("dd-MMM-yyyy");
                txtEndDate.Text = Convert.ToDateTime(lblEndDate.Text).ToString("dd-MMM-yyyy");
                txtDays.Text = (Convert.ToDateTime(lblEndDate.Text) - Convert.ToDateTime(lblStartDate.Text)).TotalDays.ToString();

                if (!string.IsNullOrEmpty(lblCustVendName.Text))
                    txtCustVendName.Text = lblCustVendName.Text;

                if (!string.IsNullOrEmpty(lblPlaceOfVisit.Text))
                    txtPlaceOfVisit.Text = lblPlaceOfVisit.Text;

                if (!string.IsNullOrEmpty(lblVisitType.Text))
                    txtPurposeOfVisit.Text = lblVisitType.Text;

                if (!string.IsNullOrEmpty(lblJobNo.Text))
                    txtJobInqNo.Text = lblJobNo.Text;

                if (!string.IsNullOrEmpty(lblAdvanceAmt.Text))
                    txtAdvanceObtained.Text = lblAdvanceAmt.Text;
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //ExceptionUpdateMessage(ex.ToString());
            return;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            InsertTourExpense();
        }
    }

    protected void btnExpenseList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TOUR_EXPENSE/TourExpenseList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void GetTourSanctionDetail()
    {
        try
        {
            _employeeName = "";
            _tourSanctionNumber = "";

            if (!string.IsNullOrEmpty(txtEmployeeNameToG.Text))
                _employeeName = txtEmployeeNameToG.Text;

            if (!string.IsNullOrEmpty(txtTourSanctionNoToG.Text))
                _tourSanctionNumber = txtTourSanctionNoToG.Text;

            dsSanctionNo = objTourAndTravels.GetSanctionNo(_tourSanctionNumber, _employeeName);

            if (dsSanctionNo.Tables.Count > 0 && dsSanctionNo.Tables[0].Rows.Count > 0)
            {
                gvTourSanctionNo.DataSource = dsSanctionNo.Tables[0];
                gvTourSanctionNo.DataBind();
            }
            else
            {
                gvTourSanctionNo.DataSource = null;
                gvTourSanctionNo.DataBind();
            }
            lblTourSanctionNoRecords.Text = "Records[" + dsSanctionNo.Tables[0].Rows.Count + "]";
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
                ddlTotalCurrency.DataSource = dsCurrency.Tables[0];
                ddlTotalCurrency.DataTextField = "CURRENCY_CODE";
                ddlTotalCurrency.DataValueField = "CURRENCY_ID";
                ddlTotalCurrency.DataBind();
                ddlTotalCurrency.SelectedValue = "68";

                ddlAdvanceObtainedCurreny.DataSource = dsCurrency.Tables[0];
                ddlAdvanceObtainedCurreny.DataTextField = "CURRENCY_CODE";
                ddlAdvanceObtainedCurreny.DataValueField = "CURRENCY_ID";
                ddlAdvanceObtainedCurreny.DataBind();
                ddlAdvanceObtainedCurreny.SelectedValue = "68";
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void Reset()
    {
        try
        {
            txtTourSanctionNo.Text = string.Empty;
            txtTourNo.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtEmployeeID.Text = string.Empty;
            //txtDesignation.Text = string.Empty;
            txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCustVendName.Text = string.Empty;
            txtPlaceOfVisit.Text = string.Empty;
            txtPurposeOfVisit.Text = string.Empty;
            txtJobInqNo.Text = string.Empty;
            //txtBusinessSegment.Text = string.Empty;
            //ddlVisitSummaryAttached.SelectedIndex = 0;

            txtAirTicket.Text = string.Empty;
            txtHotel.Text = string.Empty;
            txtTaxi.Text = string.Empty;
            txtOthers.Text = string.Empty;

            txtTotal.Text = "0.00";
            hdTotal.Value = "0";

            txtAdvanceObtained.Text = "0.00";
            txtRemarks.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void InsertTourExpense()
    {
        try
        {
            tourID = Convert.ToInt32(ViewState["TOUR_ID"]);
            airTicketAmt = 0;
            hotelAmt = 0;
            taxiAmt = 0;
            othersAmt = 0;
            totalAmt = 0;
            totalAmtCurrencyID = 0;
            remarks = string.Empty;

            Byte[] attachment1Bytes = null;
            if (file1Upload.HasFile)
            {
                if (!string.IsNullOrEmpty(file1Upload.PostedFile.FileName))
                {
                    attachment1FileName = file1Upload.PostedFile.FileName;
                    attachment1Bytes = GetFileBytes(file1Upload.PostedFile.FileName, file1Upload.PostedFile.InputStream);
                }
                else
                {
                    attachment1FileName = string.Empty;
                    attachment1Bytes = null;
                }
            }
            else
            {
                attachment1FileName = string.Empty;
                attachment1Bytes = null;
            }


            Byte[] attachment2Bytes = null;
            if (file2Upload.HasFile)
            {
                if (!string.IsNullOrEmpty(file2Upload.PostedFile.FileName))
                {
                    attachment2FileName = file2Upload.PostedFile.FileName;
                    attachment2Bytes = GetFileBytes(file2Upload.PostedFile.FileName, file2Upload.PostedFile.InputStream);
                }
                else
                {
                    attachment2FileName = string.Empty;
                    attachment2Bytes = null;
                }
            }
            else
            {
                attachment2FileName = string.Empty;
                attachment2Bytes = null;
            }

            if (!string.IsNullOrEmpty(txtAirTicket.Text))
                airTicketAmt = Convert.ToDouble(txtAirTicket.Text);

            if (!string.IsNullOrEmpty(txtHotel.Text))
                hotelAmt = Convert.ToDouble(txtHotel.Text);

            if (!string.IsNullOrEmpty(txtTaxi.Text))
                taxiAmt = Convert.ToDouble(txtTaxi.Text);

            if (!string.IsNullOrEmpty(txtOthers.Text))
                othersAmt = Convert.ToDouble(txtOthers.Text);

            if (!string.IsNullOrEmpty(hdTotal.Value))
                totalAmt = Convert.ToDouble(hdTotal.Value);

            if (ddlTotalCurrency.SelectedIndex > 0)
                totalAmtCurrencyID = Convert.ToInt32(ddlTotalCurrency.SelectedValue);

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;

            string retValue = objTourAndTravels.InsertUpdateTravelExpense(0
                                                                  , tourID
                                                                  , airTicketAmt
                                                                  , hotelAmt
                                                                  , taxiAmt
                                                                  , othersAmt
                                                                  , totalAmt
                                                                  , totalAmtCurrencyID
                                                                  , remarks
                                                                  , attachment1FileName, attachment1Bytes
                                                                  , attachment2FileName, attachment2Bytes
                                                                  , Convert.ToInt32(Session["EMP_RECORD_ID"]));

            int isertedValue = 0;
            string tourExpenseNo = string.Empty;

            if (!string.IsNullOrEmpty(retValue))
            {
                isertedValue = Convert.ToInt32(retValue.Split(':')[0]);
                tourExpenseNo = Convert.ToString(retValue.Split(':')[1]);

                int mailSentValue = objTourExpenseSendMail.ProcessAndSendMail(isertedValue, (int)TandTAllStatus.EnumTourExpenseStatus.Open);
                //, (int)TandTAllStatus.EnumTourExpenseMailType.OpenMail


                if (mailSentValue > 0)
                {
                    int val = objTourAndTravels.UpdateTourExpenseMailStatus(isertedValue, (int)TandTAllStatus.EnumTourExpenseActions.Update);
                    SuccessMessage("Tour Expense No. " + tourExpenseNo + " added and mail sent successfully.");
                }
                else
                {
                    SuccessMessage("Tour Expense No. " + tourExpenseNo + " added successfully, please send mail from Expense List.");
                }
                Reset();
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private byte[] GetFileBytes(string fileName, Stream stream)
    {
        Byte[] GSTbytes = null;
        #region
        try
        {
            string GSTFilePath = fileName;
            string GSTFileName = Path.GetFileName(GSTFilePath);
            string GSText = Path.GetExtension(GSTFileName);
            string GSTContentType = String.Empty;
            switch (GSText)
            {
                case ".jpg":
                    GSTContentType = "image/jpg";
                    break;
                case ".jpeg":
                    GSTContentType = "image/jpeg";
                    break;
                case ".bmp":
                    GSTContentType = "image/bmp";
                    break;
                case ".png":
                    GSTContentType = "image/png";
                    break;
                case ".gif":
                    GSTContentType = "image/gif";
                    break;
                case ".pdf":
                    GSTContentType = "application/pdf";
                    break;
                case ".JPG":
                    GSTContentType = "image/JPG";
                    break;
                case ".JPEG":
                    GSTContentType = "image/JPEG";
                    break;
                case ".BMP":
                    GSTContentType = "image/BMP";
                    break;
                case ".PNG":
                    GSTContentType = "image/PNG";
                    break;
                case ".GIF":
                    GSTContentType = "image/GIF";
                    break;
                case ".PDF":
                    GSTContentType = "application/PDF";
                    break;
            }
            Stream GSTfs = null;
            BinaryReader GSTbr = null;
            if (GSTContentType != String.Empty)
            {
                try
                {
                    GSTfs = stream;
                    GSTfs.Position = 0;
                    GSTbr = new BinaryReader(GSTfs);
                    GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }

    [Obsolete]
    private int SendMailNew(int travelStatementID)
    {
        try
        {
            string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);
            int tourid = 0;


            string startDateOfTour = string.Empty;
            string endDateOfTour = string.Empty;
            string placeOfVisit = string.Empty;
            string visitType = string.Empty;
            string jobNo = string.Empty;
            string businessSegment = string.Empty;
            string travelMode = string.Empty;
            string tripType = string.Empty;
            string localTravelType = string.Empty;
            string expenditureAmount = string.Empty;
            string expenditureCurrency = string.Empty;
            string advanceAmount = string.Empty;
            string advanceCurrency = string.Empty;

            int statusid = 0;
            string from = string.Empty;
            string to = string.Empty;
            string cc = string.Empty;
            string sanctionNo = string.Empty;
            string createdOn = string.Empty;
            string reqName = string.Empty;
            string reqEmail = string.Empty;
            string reqRemarks = string.Empty;
            string tlEmail = string.Empty;

            string superAdminEmail = string.Empty;

            string hrdEmail = string.Empty;

            int tlEmpRecordID = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string href = string.Empty;
            string link = string.Empty;

            dsTravelStmtInfo = objTourAndTravels.GetTravelStatementNoInfo(travelStatementID);
            if (dsTravelStmtInfo.Tables.Count > 0)
            {
                if (dsTravelStmtInfo.Tables[0].Rows.Count > 0)
                {
                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TOUR_ID"] != DBNull.Value)
                        tourid = Convert.ToInt32(dsTravelStmtInfo.Tables[0].Rows[0]["TOUR_ID"]);
                    else tourid = 0;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"] != DBNull.Value)
                        sanctionNo = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TOUR_SANCTION_NO"]);
                    else sanctionNo = string.Empty;




                    if (dsTravelStmtInfo.Tables[0].Rows[0]["START_DATE_OF_TRAVEL"] != DBNull.Value)
                        startDateOfTour = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["START_DATE_OF_TRAVEL"]);
                    else startDateOfTour = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["END_DATE_OF_TRAVEL"] != DBNull.Value)
                        endDateOfTour = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["END_DATE_OF_TRAVEL"]);
                    else endDateOfTour = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["PLACE_OF_VISIT"] != DBNull.Value)
                        placeOfVisit = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["PLACE_OF_VISIT"]);
                    else placeOfVisit = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["VISIT_TYPE"] != DBNull.Value)
                        visitType = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["VISIT_TYPE"]);
                    else visitType = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value)
                        jobNo = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["JOB_NO"]);
                    else jobNo = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["BUS_SEGMENT"] != DBNull.Value)
                        businessSegment = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["BUS_SEGMENT"]);
                    else businessSegment = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TRAVEL_MODE"] != DBNull.Value)
                        travelMode = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TRAVEL_MODE"]);
                    else travelMode = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TRIP_TYPE"] != DBNull.Value)
                        tripType = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TRIP_TYPE"]);
                    else tripType = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["LOCAL_TRAVEL_TYPE"] != DBNull.Value)
                        localTravelType = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["LOCAL_TRAVEL_TYPE"]);
                    else localTravelType = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EXPENDITURE_AMT"] != DBNull.Value)
                        expenditureAmount = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EXPENDITURE_AMT"]);
                    else expenditureAmount = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EXPENDITURE_CURRENCY"] != DBNull.Value)
                        expenditureCurrency = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EXPENDITURE_CURRENCY"]);
                    else expenditureCurrency = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["ADVANCE_AMT"] != DBNull.Value)
                        advanceAmount = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["ADVANCE_AMT"]);
                    else advanceAmount = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["ADVANCE_CURRENCY"] != DBNull.Value)
                        advanceCurrency = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["ADVANCE_CURRENCY"]);
                    else advanceCurrency = string.Empty;




                    if (dsTravelStmtInfo.Tables[0].Rows[0]["STATUS_ID"] != DBNull.Value)
                        statusid = Convert.ToInt32(dsTravelStmtInfo.Tables[0].Rows[0]["STATUS_ID"]);
                    else statusid = 0;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["CREATED_ON"] != DBNull.Value)
                        createdOn = Convert.ToDateTime(dsTravelStmtInfo.Tables[0].Rows[0]["CREATED_ON"]).ToString("dd-MMM-yyyy HH:mm:ss tt");
                    else createdOn = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EMPLOYEE_NAME"] != DBNull.Value)
                        reqName = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                    else reqName = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["EMP_EMAIL"] != DBNull.Value)
                        reqEmail = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["EMP_EMAIL"]);
                    else reqEmail = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["CREATED_REMARKS"] != DBNull.Value)
                        reqRemarks = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["CREATED_REMARKS"]);
                    else reqRemarks = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_USERNAME"] != DBNull.Value)
                        userName = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_USERNAME"]);
                    else userName = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_PASSWORD"] != DBNull.Value)
                        password = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_PASSWORD"]);
                    else password = string.Empty;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_RECORD_ID"] != DBNull.Value)
                        tlEmpRecordID = Convert.ToInt32(dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_RECORD_ID"]);
                    else tlEmpRecordID = 0;

                    if (dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_EMAIL"] != DBNull.Value)
                        tlEmail = Convert.ToString(dsTravelStmtInfo.Tables[0].Rows[0]["TEAMLEADER_EMAIL"]);
                    else tlEmail = string.Empty;
                }

                if (dsTravelStmtInfo.Tables[1].Rows.Count > 0)
                {
                    hrdEmail = Convert.ToString(dsTravelStmtInfo.Tables[1].Rows[0]["EMAIL_ID"]);
                }

                if (dsTravelStmtInfo.Tables[2].Rows.Count > 0)
                {
                    superAdminEmail = Convert.ToString(dsTravelStmtInfo.Tables[2].Rows[0]["SA_EMAIL"]);
                }
            }

            from = reqEmail;
            to = tlEmail;
            cc = superAdminEmail;

            if (tlEmpRecordID == 8)
            {
                to = hrdEmail;
            }


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(to))
            {
                string[] strTo = tlEmail.Split(',');
                foreach (string item in strTo)
                {
                    mail.To.Add(item);
                }
            }

            //if (!string.IsNullOrEmpty(cc))
            //{
            //    string[] strCC = cc.Split(',');
            //    foreach (string item in strCC)
            //    {
            //        mail.CC.Add(item);
            //    }
            //}

            mail.Subject = "Approval of Travel Statement, Sanction No.: " + sanctionNo;
            mail.IsBodyHtml = true;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/TOUR_AND_TRAVELS/TRAVEL/EMAIL_FORMATS/01ApprovalTravelStatementMail.htm")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#sanctionno#}", sanctionNo);


            body = body.Replace("{#startdateoftour#}", startDateOfTour);
            body = body.Replace("{#enddateoftour#}", endDateOfTour);
            body = body.Replace("{#placeofvisit#}", placeOfVisit);
            body = body.Replace("{#visittype#}", visitType);
            body = body.Replace("{#jobno#}", jobNo);
            body = body.Replace("{#businesssegment#}", businessSegment);
            body = body.Replace("{#travelmode#}", travelMode);
            body = body.Replace("{#triptype#}", tripType);
            body = body.Replace("{#localtraveltype#}", localTravelType);
            body = body.Replace("{#expenditureamount#}", expenditureAmount);
            body = body.Replace("{#expenditurecurrency#}", expenditureCurrency);
            body = body.Replace("{#advanceamount#}", advanceAmount);
            body = body.Replace("{#advancecurrency#}", advanceCurrency);


            body = body.Replace("{#createdon#}", createdOn);
            body = body.Replace("{#createdby#}", reqName);
            body = body.Replace("{#createdremark#}", remarks);

            link = "'" + urlTxt + "/TOUR_AND_TRAVELS/TRAVEL/UpdateTravelStatementStatusThree.aspx?statementid=" + travelStatementID + "&actid=2&ud=" + userName + "&pd=" + password + "'";

            //link = "'" + urlTxt + "/Login.aspx?sanctionno=" + sanctionNo + "'";
            href = "<a href=" + link + ">Approve Travel Statement</a>";

            body = body.Replace("{#link#}", href);
            mail.Body = body;

            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                SmtpServer.Send(mail);
                return 1;
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
            ExceptionMessage(ex.ToString());
            return 0;
        }
    }

    private void SuccessMessage(string message)
    {
        pnlSuccessMsg.Visible = true;
        lblSuccessMsg.Text = message;
        lblSuccessMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlExceptionMsg.Visible = true;
        lblExceptionMsg.Text = message;
        lblExceptionMsg.ForeColor = System.Drawing.Color.Red;
    }


    #endregion

}
