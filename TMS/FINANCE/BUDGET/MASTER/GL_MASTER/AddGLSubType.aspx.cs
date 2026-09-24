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

public partial class FINANCE_BUDGET_MASTER_GL_MASTER_AddGLSubType : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsLocalTravelType = new DataSet();
    DataSet dsTripType = new DataSet();
    DataSet dsTravelMode = new DataSet();
    DataSet dsSegmentType = new DataSet();
    DataSet dsVisitType = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsEmpDetail = new DataSet();
    DataSet dsTourInfo = new DataSet();

    int tourID = 0;
    int empRecordID = 0;
    string startDate = string.Empty;
    string endDate = string.Empty;
    string custVendName = string.Empty;
    string placeOfVisit = string.Empty;
    int purposeOfVisitID = 0;
    string jobNo = string.Empty;
    int busSegmentID = 0;
    string busSegmentOther = string.Empty;

    int modeOfTavelID = 0;
    string modeOfTavelOther = string.Empty;

    int tripTypeID = 0;

    int localTravellingID = 0;
    string localTravellingOther = string.Empty;

    double expectedExpenditure = 0;
    int expectedExpenditureCurrencyID = 0;

    double advanceRequired = 0;
    int advanceRequiredCurrencyID = 0;

    string remarks = string.Empty;
    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;

    string tourNo = string.Empty;
    int tourStatusID = 0;
    string tourStatus = string.Empty;
    string createdBy = string.Empty;
    string createdByEmail = string.Empty;
    string createdRemarks = string.Empty;
    string tlName = string.Empty;
    string tlEmail = string.Empty;
    string createdOn = string.Empty;
    DataSet dsUserInfo = new DataSet();

    string fileUploadPassportCopyFileName = string.Empty;

    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdConfirmValue.Value = "0";

                Session["EMP_DETAIL"] = null;

                hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");

                BindEmployee();
                BindVisitType();
                BindSegmentType();
                BindTravelMode();
                BindTripType();
                BindLocalTravelType();
                BindCurrency();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            InsertTourInformation();
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedIndex > 0)
        {
            pnlMsg.Visible = false;
            BindEmpDetail();
        }
        else
        {
            txtEmployeeID.Text = string.Empty;
            txtDesignation.Text = string.Empty;
        }
    }

    protected void btnTourList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TOUR/TourInformationListNew.aspx");
    }

    protected void imgbtnPassportcopy_Click(object sender, EventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ddlEmployee.SelectedValue));
    }

    #endregion


    #region METHODS[============================]

    private void BindEmployee()
    {
        try
        {
            dsEmployee = objTourAndTravels.GetEmployeeForTravel(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindVisitType()
    {
        try
        {
            dsVisitType = objTourAndTravels.GetPrimaryDetails("sp_get_visit_type");
            if (dsVisitType.Tables.Count > 0 && dsVisitType.Tables[0].Rows.Count > 0)
            {
                ddlPurposeOfVisit.DataSource = dsVisitType.Tables[0];
                ddlPurposeOfVisit.DataTextField = "VISIT_TYPE";
                ddlPurposeOfVisit.DataValueField = "VISIT_TYPE_ID";
                ddlPurposeOfVisit.DataBind();
                ddlPurposeOfVisit.Items.Insert(0, "Select");
                ddlPurposeOfVisit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindSegmentType()
    {
        try
        {
            dsSegmentType = objTourAndTravels.GetPrimaryDetails("sp_get_bus_segment");
            if (dsSegmentType.Tables.Count > 0 && dsSegmentType.Tables[0].Rows.Count > 0)
            {
                ddlBusinessSegment.DataSource = dsSegmentType.Tables[0];
                ddlBusinessSegment.DataTextField = "BUS_SEGMENT";
                ddlBusinessSegment.DataValueField = "BUS_SEGMENT_ID";
                ddlBusinessSegment.DataBind();
                ddlBusinessSegment.Items.Insert(0, "Select");
                ddlBusinessSegment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTravelMode()
    {
        try
        {
            dsTravelMode = objTourAndTravels.GetPrimaryDetails("sp_get_travel_mode");
            if (dsTravelMode.Tables.Count > 0 && dsTravelMode.Tables[0].Rows.Count > 0)
            {
                ddlModeOfTravel.DataSource = dsTravelMode.Tables[0];
                ddlModeOfTravel.DataTextField = "TRAVEL_MODE";
                ddlModeOfTravel.DataValueField = "TRAVEL_MODE_ID";
                ddlModeOfTravel.DataBind();
                ddlModeOfTravel.Items.Insert(0, "Select");
                ddlModeOfTravel.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTripType()
    {
        try
        {
            dsTripType = objTourAndTravels.GetPrimaryDetails("sp_get_trip_type");
            if (dsTripType.Tables.Count > 0 && dsTripType.Tables[0].Rows.Count > 0)
            {
                ddlTypeOfTrip.DataSource = dsTripType.Tables[0];
                ddlTypeOfTrip.DataTextField = "TRIP_TYPE";
                ddlTypeOfTrip.DataValueField = "TRIP_TYPE_ID";
                ddlTypeOfTrip.DataBind();
                ddlTypeOfTrip.Items.Insert(0, "Select");
                ddlTypeOfTrip.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLocalTravelType()
    {
        try
        {
            dsLocalTravelType = objTourAndTravels.GetPrimaryDetails("sp_get_local_travel_type");
            if (dsLocalTravelType.Tables.Count > 0 && dsLocalTravelType.Tables[0].Rows.Count > 0)
            {
                ddlLocalTravelling.DataSource = dsLocalTravelType.Tables[0];
                ddlLocalTravelling.DataTextField = "LOCAL_TRAVEL_TYPE";
                ddlLocalTravelling.DataValueField = "LOCAL_TRAVEL_TYPE_ID";
                ddlLocalTravelling.DataBind();
                ddlLocalTravelling.Items.Insert(0, "Select");
                ddlLocalTravelling.SelectedIndex = 0;
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
                ddlExpenditureCurrency.DataSource = dsCurrency.Tables[0];
                ddlExpenditureCurrency.DataTextField = "CURRENCY_CODE";
                ddlExpenditureCurrency.DataValueField = "CURRENCY_ID";
                ddlExpenditureCurrency.DataBind();
                ddlExpenditureCurrency.SelectedValue = "68";

                ddlAdvanceCurrency.DataSource = dsCurrency.Tables[0];
                ddlAdvanceCurrency.DataTextField = "CURRENCY_CODE";
                ddlAdvanceCurrency.DataValueField = "CURRENCY_ID";
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

    private void InsertTourInformation()
    {
        try
        {
            lblDateMsg.Visible = false;
            lblDateMsg.Text = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;

            if (!string.IsNullOrEmpty(hdStartDate.Value))
                startDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(hdEndDate.Value))
                endDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (Convert.ToDateTime(hdStartDate.Value).Date > Convert.ToDateTime(hdEndDate.Value).Date)
            {
                lblDateMsg.Visible = true;
                lblDateMsg.Text = "Start date must be smaller or equal to end date.";
                return;
            }

            if (!string.IsNullOrEmpty(txtCustVendName.Text))
                custVendName = txtCustVendName.Text;
            else
                custVendName = string.Empty;

            if (!string.IsNullOrEmpty(txtPlaceOfVisit.Text))
                placeOfVisit = txtPlaceOfVisit.Text;
            else
                placeOfVisit = string.Empty;

            if (ddlPurposeOfVisit.SelectedIndex > 0)
                purposeOfVisitID = Convert.ToInt32(ddlPurposeOfVisit.SelectedValue);
            else
                purposeOfVisitID = 0;

            if (!string.IsNullOrEmpty(txtJobInqNo.Text))
                jobNo = txtJobInqNo.Text;
            else
                jobNo = string.Empty;


            if (ddlBusinessSegment.SelectedIndex > 0)
            {
                busSegmentID = Convert.ToInt32(ddlBusinessSegment.SelectedValue);
                busSegmentOther = string.Empty;
                if (ddlBusinessSegment.SelectedItem.Text == "Other")
                {
                    if (!string.IsNullOrEmpty(txtBusinessSegment.Text))
                        busSegmentOther = Convert.ToString(txtBusinessSegment.Text);
                    else
                        busSegmentOther = string.Empty;
                }
            }
            else
            {
                busSegmentID = 0;
                busSegmentOther = string.Empty;
            }

            if (ddlModeOfTravel.SelectedIndex > 0)
            {
                modeOfTavelID = Convert.ToInt32(ddlModeOfTravel.SelectedValue);
                modeOfTavelOther = string.Empty;
                if (ddlModeOfTravel.SelectedItem.Text == "Other")
                {
                    if (!string.IsNullOrEmpty(txtModeOfTravel.Text))
                        modeOfTavelOther = Convert.ToString(txtModeOfTravel.Text);
                    else
                        modeOfTavelOther = string.Empty;
                }
            }
            else
            {
                modeOfTavelID = 0;
                modeOfTavelOther = string.Empty;
            }

            if (ddlTypeOfTrip.SelectedIndex > 0)
                tripTypeID = Convert.ToInt32(ddlTypeOfTrip.SelectedValue);
            else
                tripTypeID = 0;

            if (ddlLocalTravelling.SelectedIndex > 0)
            {
                localTravellingID = Convert.ToInt32(ddlLocalTravelling.SelectedValue);
                localTravellingOther = string.Empty;
                if (ddlLocalTravelling.SelectedItem.Text == "Other")
                {
                    if (!string.IsNullOrEmpty(txtLocalTravelling.Text))
                        localTravellingOther = Convert.ToString(txtLocalTravelling.Text);
                    else
                        localTravellingOther = string.Empty;
                }
            }
            else
            {
                localTravellingID = 0;
                localTravellingOther = string.Empty;
            }


            if (!string.IsNullOrEmpty(txtExpenditureAmt.Text))
                expectedExpenditure = Convert.ToDouble(txtExpenditureAmt.Text);
            else
                expectedExpenditure = 0;

            if (ddlExpenditureCurrency.SelectedIndex > 0)
                expectedExpenditureCurrencyID = Convert.ToInt32(ddlExpenditureCurrency.SelectedValue);
            else
                expectedExpenditureCurrencyID = 0;


            if (!string.IsNullOrEmpty(txtAdvanceAmt.Text))
                advanceRequired = Convert.ToDouble(txtAdvanceAmt.Text);
            else
                advanceRequired = 0;


            if (ddlAdvanceCurrency.SelectedIndex > 0)
                advanceRequiredCurrencyID = Convert.ToInt32(ddlAdvanceCurrency.SelectedValue);
            else
                advanceRequiredCurrencyID = 0;


            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;
            else
                remarks = string.Empty;


            Byte[] fileUploadPassportCopyBytes = null;
            if (fileUploadPassportCopy.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadPassportCopy.PostedFile.FileName))
                {
                    fileUploadPassportCopyFileName = fileUploadPassportCopy.PostedFile.FileName;
                    fileUploadPassportCopyBytes = GetFileBytes(fileUploadPassportCopy.PostedFile.FileName, fileUploadPassportCopy.PostedFile.InputStream);
                }
                else
                {
                    fileUploadPassportCopyFileName = string.Empty;
                    fileUploadPassportCopyBytes = null;
                }
            }
            else
            {
                fileUploadPassportCopyFileName = string.Empty;
                fileUploadPassportCopyBytes = null;
            }

            int value = objTourAndTravels.InsertTourInformation(0, empRecordID, startDate, endDate,
                                                                custVendName, placeOfVisit,
                                                                purposeOfVisitID, jobNo,
                                                                busSegmentID, busSegmentOther,
                                                                modeOfTavelID, modeOfTavelOther,
                                                                tripTypeID, localTravellingID, localTravellingOther,
                                                                expectedExpenditure, expectedExpenditureCurrencyID,
                                                                advanceRequired, advanceRequiredCurrencyID, remarks,
                                                                fileUploadPassportCopyFileName, fileUploadPassportCopyBytes,
                                                                Convert.ToInt32(Session["EMP_RECORD_ID"]),0,0,0);
            if (value > 0)
            {
                int mailSentValue = SendMail(value);
                if (mailSentValue > 0)
                {
                    int val = objTourAndTravels.UpdateTourInfoMailStatus(1, value, 0);
                    SuccessMessage("Tour No. '" + tourNo + "' added successfully and mail sent.");
                }
                else
                {
                    SuccessMessage("Tour No. '" + tourNo + "' added successfully, and resend an approval email from Tour Informatin List.");
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

    private void Reset()
    {
        ddlEmployee.SelectedIndex = 0;
        txtEmployeeID.Text = string.Empty;
        txtDesignation.Text = string.Empty;

        txtPassportcopy.Text = string.Empty;
        pnlAddPassportCopy.Visible = false;


        hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
        txtStartDate.Text = hdStartDate.Value;
        txtEndDate.Text = hdEndDate.Value;

        txtCustVendName.Text = string.Empty;
        txtPlaceOfVisit.Text = string.Empty;
        ddlPurposeOfVisit.SelectedIndex = 0;
        txtJobInqNo.Text = string.Empty;
        ddlBusinessSegment.SelectedIndex = 0;
        txtBusinessSegment.Text = string.Empty;
        txtBusinessSegment.Enabled = false;
        ddlModeOfTravel.SelectedIndex = 0;
        txtModeOfTravel.Text = string.Empty;
        txtModeOfTravel.Enabled = false;
        ddlTypeOfTrip.SelectedIndex = 0;
        ddlLocalTravelling.SelectedIndex = 0;
        txtLocalTravelling.Text = string.Empty;
        txtLocalTravelling.Enabled = false;
        txtExpenditureAmt.Text = string.Empty;
        txtAdvanceAmt.Text = string.Empty;
        ddlAdvanceCurrency.SelectedValue = "68";
        txtRemarks.Text = string.Empty;
    }

    private void BindEmpDetail()
    {
        try
        {
            pnlViewPassportcopy.Visible = false;
            pnlAddPassportCopy.Visible = false;

            dsEmpDetail = objCommon.GetEmployeeByEmpRecordID(Convert.ToInt32(ddlEmployee.SelectedValue));
            if (dsEmpDetail.Tables.Count > 0 && dsEmpDetail.Tables[0].Rows.Count > 0)
            {
                Session["EMP_DETAIL"] = dsEmpDetail.Tables[0];
                txtEmployeeID.Text = Convert.ToString(dsEmpDetail.Tables[0].Rows[0]["EMPLOYEE_ID"]);
                txtDesignation.Text = Convert.ToString(dsEmpDetail.Tables[0].Rows[0]["DESIGNATION"]);

                if (!string.IsNullOrEmpty(Convert.ToString(dsEmpDetail.Tables[0].Rows[0]["PASSPORT_COPY_NAME"])))
                {
                    pnlViewPassportcopy.Visible = true;
                    txtPassportcopy.Text = Convert.ToString(dsEmpDetail.Tables[0].Rows[0]["PASSPORT_COPY_NAME"]);
                }
                else
                    pnlAddPassportCopy.Visible = true;
            }
            else
            {
                Session["EMP_DETAIL"] = null;
                txtEmployeeID.Text = string.Empty;
                txtDesignation.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ViewAttachedFilesNew01(int empRecordID)
    {
        try
        {
            byte[] bytes = null;
            DataTable dsPassport = (DataTable)Session["EMP_DETAIL"];
            string fileName = string.Empty;
            string extn = string.Empty;
            foreach (DataRow dr in dsPassport.Select("EMP_RECORD_ID='" + empRecordID + "'"))
            {
                fileName = Convert.ToString(dr["PASSPORT_COPY_NAME"]);
                bytes = (byte[])dr["PASSPORT_COPY_DOC"];
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewPassportImgFileNew.ashx?emprecordid=" + empRecordID;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewPassportPDFFileNew.aspx?emprecordid=" + empRecordID);
                    this.ModalPopupExtender3.Show();
                }
            }
            else
            {
                ExceptionMessage("File Doesn't exist!");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
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

    private int SendMail(int tourID)
    {
        try
        {
            from = string.Empty;
            to = string.Empty;
            cc = string.Empty;
            bcc = string.Empty;

            tourNo = string.Empty;

            createdBy = string.Empty;
            createdOn = string.Empty;
            createdByEmail = string.Empty;
            createdRemarks = string.Empty;

            tlName = string.Empty;
            tlEmail = string.Empty;

            int tlEmpRecordID = 0;
            string userName = string.Empty;
            string password = string.Empty;

            string approveHref = string.Empty;
            string approveLink = string.Empty;

            string deleteHref = string.Empty;
            string deleteLink = string.Empty;

            string superAdminEmail = string.Empty;


            dsTourInfo = objTourAndTravels.GetRequesterInfo(tourID);
            if (dsTourInfo.Tables.Count > 0 && dsTourInfo.Tables[0].Rows.Count > 0)
            {
                tourNo = Convert.ToString(dsTourInfo.Tables[0].Rows[0]["TOUR_NO"]);
                tourStatusID = Convert.ToInt32(dsTourInfo.Tables[0].Rows[0]["TOUR_STATUS_ID"]);
                createdBy = Convert.ToString(dsTourInfo.Tables[0].Rows[0]["CREATED_BY"]);
                createdByEmail = Convert.ToString(dsTourInfo.Tables[0].Rows[0]["CREATED_BY_EMAIL"]);
                createdRemarks = Convert.ToString(dsTourInfo.Tables[0].Rows[0]["CREATED_REMARKS"]);
                if (dsTourInfo.Tables[0].Rows[0]["CREATED_ON"] != DBNull.Value)
                    createdOn = Convert.ToDateTime(dsTourInfo.Tables[0].Rows[0]["CREATED_ON"]).ToString("dd-MMM-yyyy HH:mm:ss tt");



                tlEmpRecordID = Convert.ToInt32(dsTourInfo.Tables[0].Rows[0]["TEAMLEADER_EMP_RECORD_ID"]);
                userName = Convert.ToString(dsTourInfo.Tables[0].Rows[0]["TEAMLEADER_USER_NAME"]);
                password = Convert.ToString(dsTourInfo.Tables[0].Rows[0]["TEAMLEADER_PASSWORD"]);
                tlName = Convert.ToString(dsTourInfo.Tables[0].Rows[0]["TEAMLEADER_NAME"]);
                tlEmail = Convert.ToString(dsTourInfo.Tables[0].Rows[0]["TEAMLEADER_EMAIL"]);

                from = createdByEmail;
                to = tlEmail;
                //bcc = createdByEmail;
            }

            //Super Admin
            //if (dsTourInfo.Tables[4].Rows.Count > 0)
            //{
            //    superAdminEmail = Convert.ToString(dsTourInfo.Tables[4].Rows[0]["SA_EMAIL"]);
            //    cc = superAdminEmail;
            //}


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(to))
            {
                string[] strTo = to.Split(';');
                foreach (string item in strTo)
                {
                    mail.To.Add(item);
                }
            }

            //if (!string.IsNullOrEmpty(cc))
            //{
            //    mail.CC.Add(cc);
            //}

            //if (!string.IsNullOrEmpty(cc))
            //{
            //    string[] strCC = cc.Split(';');
            //    foreach (string item in strCC)
            //    {
            //        mail.CC.Add(item);
            //    }
            //}

            //if (!string.IsNullOrEmpty(bcc))
            //{
            //    string[] strBCC = bcc.Split(';');
            //    foreach (string item in strBCC)
            //    {
            //        mail.Bcc.Add(item);
            //    }
            //}


            mail.Subject = "Tour No.: '" + tourNo + "' Created On: " + createdOn;
            mail.IsBodyHtml = true;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/TOUR_AND_TRAVELS/TOUR/EMAIL_FORMATS/01ReqToHodTourMail.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{#tourno#}", tourNo);

            body = body.Replace("{#fromdate#}", hdStartDate.Value);
            body = body.Replace("{#todate#}", hdEndDate.Value);
            body = body.Replace("{#customername#}", txtCustVendName.Text);
            body = body.Replace("{#place#}", txtPlaceOfVisit.Text);

            if (ddlPurposeOfVisit.SelectedIndex > 0)
                body = body.Replace("{#purposeofvisit#}", Convert.ToString(ddlPurposeOfVisit.SelectedItem));

            if (ddlTypeOfTrip.SelectedIndex > 0)
                body = body.Replace("{#typeoftrip#}", Convert.ToString(ddlTypeOfTrip.SelectedItem));


            if (!string.IsNullOrEmpty(Convert.ToString(txtExpenditureAmt.Text)))
            {
                body = body.Replace("{#expectedexpenditure#}", Convert.ToString(txtExpenditureAmt.Text));
                body = body.Replace("{#expectedexpenditurecurr#}", Convert.ToString(ddlExpenditureCurrency.SelectedItem));
            }
            else
            {
                body = body.Replace("{#expectedexpenditure#}", "00:00");
            }

            if (!string.IsNullOrEmpty(Convert.ToString(txtAdvanceAmt.Text)))
            {
                body = body.Replace("{#advancerequired#}", Convert.ToString(txtAdvanceAmt.Text));
                body = body.Replace("{#advancerequiredcurr#}", Convert.ToString(ddlAdvanceCurrency.SelectedItem));
            }
            else
            {
                body = body.Replace("{#advancerequired#}", "00:00");
            }



            body = body.Replace("{#createdon#}", createdOn);
            body = body.Replace("{#createdby#}", createdBy);
            body = body.Replace("{#requesterremarks#}", remarks);
            body = body.Replace("{#approvedby#}", tlName);

            string urlTxt = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["URL"]);

            approveLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=2&tourstatusid=" + tourStatusID + "&ud=" + userName + "&pd=" + password + "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";
            approveHref = "<a href=" + approveLink + ">Approve Tour request</a>";

            deleteLink = "'" + urlTxt + "/TOUR_AND_TRAVELS/TOUR/UpdateTourStatusNewOne.aspx?tourid=" + tourID + "&tourno=" + tourNo + "&actid=3&tourstatusid=" + tourStatusID + "&ud=" + userName + "&pd=" + password + "&emprecordid=" + Convert.ToString(tlEmpRecordID) + "'";
            deleteHref = "<a href=" + deleteLink + ">Delete Tour request</a>";

            body = body.Replace("{#approvelink#}", approveHref);
            body = body.Replace("{#deletelink#}", deleteHref);
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