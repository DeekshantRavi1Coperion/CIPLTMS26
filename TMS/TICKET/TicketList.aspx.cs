using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TICKET_TicketList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Ticket objTicket = new BAL.Ticket();
    DataSet dsEmployee = new DataSet();
    DataSet dsTicketList = new DataSet();
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string ticketNo = string.Empty;
    string ticketStatus = string.Empty;
    int empRecordID = 0;

    DataSet dsTicketType = new DataSet();

    int ticketID = 0;
    int ticketTypeId = 0;
    string attachmentOneFileName = string.Empty;
    string ticketDescription = string.Empty;
    string remarks = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {

            if (!IsPostBack)
            {
                ddlTicketStatus.SelectedIndex = 1;
                BindEmployee();
                GetTicketType();

                Session["dtTicketList"] = null;

                ddlEmployee.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 9 || 
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 13 || 
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 14 ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 2 ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 16 ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 418 ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 115)
                {
                    ddlEmployee.Enabled = true;
                }
                else
                {
                    ddlEmployee.Enabled = false;
                }

                GetTicketList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetTicketList();
    }

    protected void gvTicketList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "STATUS" || e.CommandArgument == "PROPERTIES" || e.CommandArgument == "ViewATTACHMENT1")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (e.CommandArgument == "CANCEL" || e.CommandArgument == "CLOSE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }



                Label lblTicketStatus = gvTicketList.Rows[rowindex].FindControl("lblTicketStatus") as Label;
                Label lblTicketid = gvTicketList.Rows[rowindex].FindControl("lblTicketID") as Label;
                Label lblTicketNO = gvTicketList.Rows[rowindex].FindControl("lblTicketNO") as Label;
                Label lblFileOneName = gvTicketList.Rows[rowindex].FindControl("lblFileOneName") as Label;
                Label lblCreatedByID = gvTicketList.Rows[rowindex].FindControl("lblCreatedByID") as Label;
                Label lblTicketTypeID = gvTicketList.Rows[rowindex].FindControl("lblTicketTypeID") as Label;
                Label lblRemarks = gvTicketList.Rows[rowindex].FindControl("lblRemarks") as Label;

                pnlAttachments.Visible = false;
                pnlViewAttachments.Visible = false;
                btnViewAttachment1.Visible = false;

                string FileOneNameExtn = string.Empty;


                ViewState["TICKET_ID"] = Convert.ToInt32(lblTicketid.Text);
                ViewState["TICKET_NO"] = Convert.ToString(lblTicketNO.Text);
                ViewState["EMP_RECORD_ID"] = Convert.ToInt32(lblCreatedByID.Text);

                if (e.CommandArgument == "PROPERTIES")
                {
                    pnlAttachments.Visible = true;

                    lblLegend.Text = "Ticket No[" + lblTicketNO.Text + "]";
                    ddlTicketType.SelectedValue = Convert.ToString(lblTicketTypeID.Text);
                    ddlTicketType.Enabled = true;
                    lblDescription.Text = "Remarks:";
                    txtTicketDescription.Text = Convert.ToString(lblRemarks.Text);
                    btnSubmit.Text = "Update";
                    ViewState["ACT_ID"] = 1;
                    ModalPopupExtender1.Show();
                }

                if (e.CommandArgument == "STATUS" || e.CommandArgument == "CLOSE")
                {
                    txtViewAttachment1.Text = lblFileOneName.Text;
                    pnlViewAttachments.Visible = true;
                    btnViewAttachment1.Visible = true;

                    lblLegend.Text = "Ticket No [" + lblTicketNO.Text + "]";
                    ddlTicketType.SelectedValue = Convert.ToString(lblTicketTypeID.Text);
                    ddlTicketType.Enabled = false;
                    lblDescription.Text = "Description:";
                    txtTicketDescription.Text = string.Empty;
                    btnSubmit.Text = "Close Ticket";
                    ViewState["ACT_ID"] = 2;
                    ModalPopupExtender1.Show();
                }

                else if (e.CommandArgument == "CANCEL")
                {
                    txtViewAttachment1.Text = lblFileOneName.Text;
                    pnlViewAttachments.Visible = true;
                    btnViewAttachment1.Visible = true;

                    lblLegend.Text = "Ticket No[" + lblTicketNO.Text + "]";
                    ddlTicketType.SelectedValue = Convert.ToString(lblTicketTypeID.Text);
                    ddlTicketType.Enabled = false;
                    lblDescription.Text = "Description:";
                    txtTicketDescription.Text = string.Empty;
                    btnSubmit.Text = "Cancel Ticket";
                    ViewState["ACT_ID"] = 3;
                    ModalPopupExtender1.Show();
                }

                if (!string.IsNullOrEmpty(lblFileOneName.Text))
                {
                    FileOneNameExtn = Convert.ToString(lblFileOneName.Text).Split('.').Last();
                    if (FileOneNameExtn == "jpg" || FileOneNameExtn == "jepg" || FileOneNameExtn == "bmp" || FileOneNameExtn == "png" || FileOneNameExtn == "gif" || FileOneNameExtn == "JPG" || FileOneNameExtn == "JPEG" || FileOneNameExtn == "BMP" || FileOneNameExtn == "PNG" || FileOneNameExtn == "GIF")
                    {
                        btnViewAttachment1.ImageUrl = "~/Images/imgicon1.png";
                        btnViewAttachment1.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "pdf" || FileOneNameExtn == "PDF")
                    {
                        btnViewAttachment1.ImageUrl = "~/Images/pdficon1.png";
                        btnViewAttachment1.ToolTip = lblFileOneName.Text;
                    }
                }
                else
                {
                    btnViewAttachment1.Visible = false;
                }

                if (e.CommandArgument == "ViewATTACHMENT1")
                    ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "ATTACHMENT1");


            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    protected void gvTicketList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;
                ImageButton img = (ImageButton)e.Row.FindControl("imgStatus");
                ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");
                Label lblStatus = (Label)e.Row.FindControl("lblTicketStatus");
                Label lblCreatedByID = (Label)e.Row.FindControl("lblCreatedByID");

                Label lblTicketTypeID = (Label)e.Row.FindControl("lblTicketTypeID");
                Label lblTicketType = (Label)e.Row.FindControl("lblTicketType");


                Button btnClose = (Button)e.Row.FindControl("btnClose");
                Button btnCancel = (Button)e.Row.FindControl("btnCancel");

                string status = lblStatus.Text;

                img.Enabled = false;
                btnClose.Visible = false;
                btnCancel.Visible = false;
                imgProperties.Visible = false;
                if (status == "New")
                {
                    img.ImageUrl = "~/Images/NEWICONS/New02.png";
                    img.ToolTip = "New";

                    if (lblTicketTypeID.Text == "12" && lblTicketType.Text == "HR-Software")
                    {
                        if (((Convert.ToInt32(Session["DEPARTMENT_ID"]) == 3 || 
                             Convert.ToInt32(Session["DEPARTMENT_ID"]) == 4) && 
                             (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 15 || 
                              Convert.ToInt32(Session["EMP_RECORD_ID"]) == 16 || 
                              Convert.ToInt32(Session["EMP_RECORD_ID"]) == 115 ||
                               Convert.ToInt32(Session["EMP_RECORD_ID"]) == 418 ||
                              Convert.ToInt32(Session["EMP_RECORD_ID"]) == 117)
                              ) ||
                            (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 2 && Convert.ToInt32(Session["EMP_RECORD_ID"]) == 2))
                        {
                            img.Enabled = true;
                            btnClose.Visible = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 3 && (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 13 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 9) || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 115 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 117)
                        {
                            img.Enabled = true;
                            btnClose.Visible = true;
                        }

                    }

                    if (Convert.ToInt32(lblCreatedByID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]) || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 115 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 117)
                    {
                        imgProperties.Visible = true;
                        btnCancel.Visible = true;
                    }
                }
                else if (status == "Closed")
                {
                    img.Enabled = false;
                    img.ImageUrl = "~/Images/NEWICONS/Closed03.png";
                    img.ToolTip = "Closed";
                    imgProperties.Visible = false;
                    btnCancel.Visible = false;
                    btnClose.Visible = false;
                }
                else if (status == "Cancelled")
                {
                    img.ImageUrl = "~/Images/NEWICONS/Cancelled03.png";
                    img.ToolTip = "Cancelled";
                    imgProperties.Visible = false;
                    btnCancel.Visible = false;
                    btnClose.Visible = false;
                }

                Label lblFileOneName = (Label)e.Row.FindControl("lblFileOneName");
                ImageButton btnFileOneName = (ImageButton)e.Row.FindControl("btnFileOneName");

                string FileOneNameExtn = string.Empty;
                if (!string.IsNullOrEmpty(lblFileOneName.Text))
                {
                    FileOneNameExtn = Convert.ToString(lblFileOneName.Text).Split('.').Last();
                    if (FileOneNameExtn == "jpg" || FileOneNameExtn == "jepg" || FileOneNameExtn == "bmp" || FileOneNameExtn == "png" || FileOneNameExtn == "gif" || FileOneNameExtn == "JPG" || FileOneNameExtn == "JPEG" || FileOneNameExtn == "BMP" || FileOneNameExtn == "PNG" || FileOneNameExtn == "GIF")
                    {
                        btnFileOneName.ImageUrl = "~/Images/imgicon1.png";
                        btnFileOneName.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "pdf" || FileOneNameExtn == "PDF")
                    {
                        btnFileOneName.ImageUrl = "~/Images/pdficon1.png";
                        btnFileOneName.ToolTip = lblFileOneName.Text;
                    }
                }
                else
                {
                    btnFileOneName.Visible = false;
                }

                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{
                //    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                //}

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvTicketList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketList.PageIndex = e.NewPageIndex;
        GetTicketList();
    }

    protected void btnViewAttachment1_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "ATTACHMENT1");
        ModalPopupExtender1.Show();
    }

    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TICKET/AddUpdateTicketNew.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["ACT_ID"]) == 1)
            UpdateTicket();
        else
            UpdateTicketStatus();

        GetTicketList();
    }

    #endregion


    #region METHODS[=======================]

    private void BindEmployee()
    {
        try
        {
            dsEmployee = objTourAndTravels.GetEmployeeForTravel(0);
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

    private void GetTicketList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (!string.IsNullOrEmpty(txtTicketNo.Text))
                ticketNo = txtTicketNo.Text.Trim();
            else
                ticketNo = string.Empty;

            if (ddlTicketStatus.SelectedIndex > 0)
                ticketStatus = ddlTicketStatus.SelectedItem.Text;
            else
                ticketStatus = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;

            dsTicketList = objTicket.GetTicketList(fromDate, toDate, ticketNo, ticketStatus, empRecordID);
            if (dsTicketList.Tables.Count > 0 && dsTicketList.Tables[0].Rows.Count > 0)
            {
                Session["dtTicketList"] = dsTicketList.Tables[0];
                gvTicketList.DataSource = dsTicketList.Tables[0];
                gvTicketList.DataBind();
                lblRecords.Text = "Records[" + dsTicketList.Tables[0].Rows.Count + "]";
            }
            else
            {
                Session["dtTicketList"] = null;
                gvTicketList.DataSource = null;
                gvTicketList.DataBind();
                lblRecords.Text = "Records[0]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTicketType()
    {
        try
        {
            dsTicketType = objTicket.GetTicketType();
            if (dsTicketType.Tables.Count > 0 && dsTicketType.Tables[0].Rows.Count > 0)
            {
                ddlTicketType.DataSource = dsTicketType.Tables[0];
                ddlTicketType.DataTextField = "TICKET_TYPE_NAME";
                ddlTicketType.DataValueField = "TICKET_TYPE_ID";
                ddlTicketType.DataBind();
                ddlTicketType.Items.Insert(0, "Select");
                ddlTicketType.SelectedIndex = 0;

            }
            else
            {
                ddlTicketType.DataSource = null;
                ddlTicketType.Items.Insert(0, "Select");
                ddlTicketType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ViewAttachedFilesNew01(int ticketID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            DataTable dtTicketList = (DataTable)Session["dtTicketList"];
            string fileName = Convert.ToString(dtTicketList.Select("TICKET_ID='" + ticketID + "'"));
            string extn = string.Empty;
            foreach (DataRow dr in dtTicketList.Select("TICKET_ID='" + ticketID + "'"))
            {
                if (fileType == "ATTACHMENT1")
                {
                    fileName = Convert.ToString(dr["ATTACHMENT_ONE"]);
                    bytes = (byte[])dr["ATTACHMENT_ONE_DOC"];
                }
                //else if (fileType == "ATTACHMENT2")
                //{
                //    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_TWO_NAME"]);
                //    bytes = (byte[])dr["VISIT_RPT_SUMMARY_TWO_DOC"];
                //}
                //else if (fileType == "ATTACHMENT3")
                //{
                //    fileName = Convert.ToString(dr["VISIT_RPT_SUMMARY_THREE_NAME"]);
                //    bytes = (byte[])dr["VISIT_RPT_SUMMARY_THREE_DOC"];
                //}
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewAttachedImageFile.ashx?ticketID=" + ticketID + "&fileType=" + fileType;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?ticketID=" + ticketID + "&fileType=" + fileType);
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

    private void UpdateTicket()
    {
        try
        {
            if (Convert.ToInt32(ViewState["TICKET_ID"]) != 0)
                ticketID = Convert.ToInt32(ViewState["TICKET_ID"]);
            else
                ticketID = 0;


            ticketTypeId = Convert.ToInt32(ddlTicketType.SelectedValue);

            Byte[] attachmentOneBytes = null;
            if (fileUploadAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment1.PostedFile.FileName))
                {
                    attachmentOneFileName = fileUploadAttachment1.PostedFile.FileName;
                    attachmentOneBytes = GetFileBytes(fileUploadAttachment1.PostedFile.FileName, fileUploadAttachment1.PostedFile.InputStream);
                }
                else
                {
                    attachmentOneFileName = string.Empty;
                    attachmentOneBytes = null;
                }
            }
            else
            {
                attachmentOneFileName = string.Empty;
                attachmentOneBytes = null;
            }

            ticketDescription = txtTicketDescription.Text;

            int value = objTicket.CreateAndUpdateTicket(ticketID, ticketTypeId, attachmentOneFileName, attachmentOneBytes,
                        ticketDescription, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                pnlMsg.Visible = true;
                SuccessMessage("Ticket No: '" + Convert.ToString(ViewState["TICKET_NO"]) + "' updated  successfully.");
            }
            else
            {
                ExceptionMessage("Please try again");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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

    private void UpdateTicketStatus()
    {
        try
        {
            if (Convert.ToInt32(ViewState["TICKET_ID"]) != 0)
                ticketID = Convert.ToInt32(ViewState["TICKET_ID"]);
            else
                ticketID = 0;


            ticketTypeId = Convert.ToInt32(ddlTicketType.SelectedValue);
            remarks = txtTicketDescription.Text;
            int sendMailValue = 0;
            if (ticketID > 0)
            {
                int value = objTicket.UpdateTicketStatusNew(ticketID, Convert.ToInt32(ViewState["ACT_ID"]), remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    if (Convert.ToInt32(ViewState["ACT_ID"]) == 2)
                    {
                        sendMailValue = SendMail(ticketID);
                        if (sendMailValue > 0)
                            SuccessMessage("Ticket Refrance #: '" + Convert.ToString(ViewState["TICKET_NO"]) + "' closed successfully and mail sent.");
                        else
                            SuccessMessage("Ticket Refrance #: '" + Convert.ToString(ViewState["TICKET_NO"]) + "' closed successfully.");
                    }
                    else if (Convert.ToInt32(ViewState["ACT_ID"]) == 3)
                        SuccessMessage("Ticket Refrance #: '" + Convert.ToString(ViewState["TICKET_NO"]) + "' cancelled successfully.");
                }
                else
                {
                    lblMsg.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private int SendMail(int ticketID)
    {
        try
        {
            int value = 0;
            DataSet dsTicket = new DataSet();
            string ticketno = string.Empty;
            string tickettype = string.Empty;
            int tickettypeid = 0;
            string ticketstatus = string.Empty;

            string createdby = string.Empty;
            string createdbyemail = string.Empty;
            string createdon = string.Empty;
            string description = string.Empty;

            int closedByid = 0;
            string closedby = string.Empty;
            string closedbyemail = string.Empty;
            string closedon = string.Empty;
            string remarkstext = string.Empty;

            string fromemail = string.Empty;
            string toemail = string.Empty;
            string ccemail = string.Empty;

            string itteamemailid = string.Empty;
            string hrteamemailid = string.Empty;

            dsTicket = objTicket.GetTicketByTicketID(ticketID);

            if (dsTicket.Tables.Count > 0)
            {
                if (dsTicket.Tables[0].Rows.Count > 0)
                {
                    ticketno = Convert.ToString(dsTicket.Tables[0].Rows[0]["TICKET_NO"]);
                    tickettypeid = Convert.ToInt32(dsTicket.Tables[0].Rows[0]["TICKET_TYPE_ID"]);
                    tickettype = Convert.ToString(dsTicket.Tables[0].Rows[0]["TICKET_TYPE_NAME"]);
                    ticketstatus = Convert.ToString(dsTicket.Tables[0].Rows[0]["TICKET_STATUS"]);
                    createdby = Convert.ToString(dsTicket.Tables[0].Rows[0]["CREATED_BY"]);
                    createdbyemail = Convert.ToString(dsTicket.Tables[0].Rows[0]["CREATED_BY_EMAIL"]);
                    if (dsTicket.Tables[0].Rows[0]["CREATED_ON"] != DBNull.Value)
                        createdon = Convert.ToDateTime(dsTicket.Tables[0].Rows[0]["CREATED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    description = Convert.ToString(dsTicket.Tables[0].Rows[0]["DESCRIPTION"]);

                    closedByid = Convert.ToInt32(dsTicket.Tables[0].Rows[0]["CLOSED_BY_ID"]);
                    closedby = Convert.ToString(dsTicket.Tables[0].Rows[0]["CLOSED_BY"]);
                    closedbyemail = Convert.ToString(dsTicket.Tables[0].Rows[0]["CLOSED_BY_EMAIL"]);
                    if (dsTicket.Tables[0].Rows[0]["CLOSED_ON"] != DBNull.Value)
                        closedon = Convert.ToDateTime(dsTicket.Tables[0].Rows[0]["CLOSED_ON"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    remarkstext = Convert.ToString(dsTicket.Tables[0].Rows[0]["REMARKS"]);
                }

                if (dsTicket.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTicket.Tables[1].Rows)
                    {
                        itteamemailid += Convert.ToString(dr["IT_EMAIL_ID"]) + ";";
                    }
                    itteamemailid = itteamemailid.TrimEnd(';');
                }

                if (dsTicket.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTicket.Tables[2].Rows)
                    {
                        hrteamemailid += Convert.ToString(dr["HR_EMAIL_ID"]) + ";";
                    }
                    hrteamemailid = hrteamemailid.TrimEnd(';');
                }

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient();

                if (!string.IsNullOrEmpty(closedbyemail))
                    fromemail = closedbyemail;
                else
                    value = 0;


                if (!string.IsNullOrEmpty(createdbyemail))
                {
                    toemail = createdbyemail;
                    string[] strTo = toemail.Split(';');
                    foreach (string item in strTo)
                    {
                        mail.To.Add(item);
                    }
                }
                else
                    value = 0;

                if (!string.IsNullOrEmpty(hrteamemailid))//itteamemailid
                {
                    ccemail = ";ruchi.verma@coperion.com";//itteamemailid + 

                    //if (closedByid == 9)
                    //    ccemail = "trinetra-nand.upadhyay@coperion.com;" + "krishnakant.dubey@coperion.com;" + "ruchi.verma@coperion.com";
                    //else if (closedByid == 13)
                    //    ccemail = "trinetra-nand.upadhyay@coperion.com;" + "gopal.pd@coperion.com;" + "ruchi.verma@coperion.com";

                    if (tickettypeid == 12 && tickettype == "HR-Software")
                    {
                        ccemail = ccemail + ";" + hrteamemailid;
                    }
                    if (!string.IsNullOrEmpty(ccemail))
                    {
                        ccemail = ccemail.TrimEnd(';');

                        string[] strCC = ccemail.Split(';');
                        foreach (string item in strCC)
                        {
                            if (!string.IsNullOrEmpty(item))
                                mail.CC.Add(item);
                        }
                    }
                }
                else
                    value = 0;

                mail.From = new MailAddress(fromemail);
                mail.Subject = "Ticket Reference #: '" + ticketno + "' closed on: " + closedon;
                mail.IsBodyHtml = true;
                string body = string.Empty;
                string fileName = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/TICKET/EMAIL_FORMATS/ClosedTicketMail.htm")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{#TicketNo#}", ticketno);
                body = body.Replace("{#TicketType#}", tickettype);
                body = body.Replace("{#Status#}", ticketstatus);
                body = body.Replace("{#CreatedBy#}", createdby);
                body = body.Replace("{#CreatedOn#}", createdon);
                body = body.Replace("{#Description#}", description);
                body = body.Replace("{#ClosedBy#}", closedby);
                body = body.Replace("{#ClosedOn#}", closedon);
                body = body.Replace("{#Remarks#}", remarks);

                mail.Body = body;
                SmtpServer.Host = "eusmtp.hi.corp";
                SmtpServer.Port = 25;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                if (!string.IsNullOrEmpty(toemail))
                {
                    try
                    {
                        SmtpServer.Send(mail);
                        value = 1;
                    }
                    catch (Exception)
                    {
                        value = 0;
                    }
                }
            }
            else
            {
                value = 0;
            }
            return value;
        }
        catch (Exception)
        {
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