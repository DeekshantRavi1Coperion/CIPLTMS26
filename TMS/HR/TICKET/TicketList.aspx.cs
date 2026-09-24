using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class HR_TICKET_TicketList : System.Web.UI.Page
{
    #region VARIABLES[=====================]

    DataSet dsTicketAndAllocateTo = new DataSet();
    DataSet dsTicketStatus = new DataSet();
    DataSet dsTicketType = new DataSet();
    DataSet dsTicketSubtype = new DataSet();
    DataSet dsEmployee = new DataSet();

    DataSet dsTicketTypeU = new DataSet();
    DataSet dsTicketSubtypeU = new DataSet();

    BAL.HR.Ticket objTicket = new BAL.HR.Ticket();

    DataSet dsCreatedBy = new DataSet();
    DataSet dsAllocatedTo = new DataSet();
    DataSet dsTicketList = new DataSet();
    DataTable dtTeamMembers = new DataTable();
    int teamMemberID = 0;

    // S for search
    private string _dateTypeS;
    private string _fromDateS;
    private string _toDateS;
    private string _ticketNoS;
    private int _ticketStatusIdS;
    private int _ticketTypeIdS;
    private int _ticketSubtypeIdS;
    private int _createdByIdS;
    private int _allocatedToIdS;
    string teamMembers = string.Empty;

    // U for update
    private int _ticketIdU;
    private string _ticketNumberU;
    private int _currentUserIdU;
    private int _ticketTypeIdU = 0;
    private int _ticketSubtypeIdU = 0;
    private int _allocateToIdU = 0;
    private string _allocatedToPriority = string.Empty;
    private string _attachmentOneFileNameU = string.Empty;
    private string _attachmentTwoFileNameU = string.Empty;
    private string _attachmentThreeFileNameU = string.Empty;
    private string _ticketDescriptionU = string.Empty;
    private string _remarksU = string.Empty;

    string closingAttachmentOneFileName = string.Empty;
    string closingAttachmentTwoFileName = string.Empty;
    string closingAttachmentThreeFileName = string.Empty;

    string insuranceNo = string.Empty;

    private int _ticketStatusId;


    public string FromDateS
    {
        get
        {
            return _fromDateS;
        }

        set
        {
            _fromDateS = value;
        }
    }

    public string ToDateS
    {
        get
        {
            return _toDateS;
        }

        set
        {
            _toDateS = value;
        }
    }

    public string TicketNoS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtTicketNo.Text))
                _ticketNoS = txtTicketNo.Text.Trim();
            else _ticketNoS = "";

            return _ticketNoS;
        }

        set
        {
            _ticketNoS = value;
        }
    }

    public int TicketStatusIdS
    {
        get
        {
            if (ddlTicketStatus.SelectedIndex > 0)
                _ticketStatusIdS = Convert.ToInt32(ddlTicketStatus.SelectedValue);
            else _ticketStatusIdS = 0;

            return _ticketStatusIdS;
        }

        set
        {
            _ticketStatusIdS = value;
        }
    }

    public int TicketTypeIdS
    {
        get
        {
            if (ddlTicketType.SelectedIndex > 0)
                _ticketTypeIdS = Convert.ToInt32(ddlTicketType.SelectedValue);
            else _ticketTypeIdS = 0;

            return _ticketTypeIdS;
        }

        set
        {
            _ticketTypeIdS = value;
        }
    }

    public int TicketSubtypeIdS
    {
        get
        {
            if (ddlTicketSubtype.SelectedIndex > 0)
                _ticketSubtypeIdS = Convert.ToInt32(ddlTicketSubtype.SelectedValue);
            else _ticketSubtypeIdS = 0;

            return _ticketSubtypeIdS;
        }

        set
        {
            _ticketSubtypeIdS = value;
        }
    }

    public int CreatedByIdS
    {
        get
        {
            if (ddlCreatedBy.SelectedIndex > 0)
                _createdByIdS = Convert.ToInt32(ddlCreatedBy.SelectedValue);
            else _createdByIdS = 0;

            return _createdByIdS;
        }

        set
        {
            _createdByIdS = value;
        }
    }

    public string DateTypeS
    {
        get
        {
            _dateTypeS = Convert.ToString(ddlDateType.SelectedValue);
            return _dateTypeS;
        }

        set
        {
            _dateTypeS = value;
        }
    }

    public int CurrentUserIdU
    {
        get
        {
            _currentUserIdU = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            return _currentUserIdU;
        }

        set
        {
            _currentUserIdU = value;
        }
    }

    public int TicketTypeIdU
    {
        get
        {
            return _ticketTypeIdU;
        }

        set
        {
            _ticketTypeIdU = value;
        }
    }

    public int TicketSubtypeIdU
    {
        get
        {
            return _ticketSubtypeIdU;
        }

        set
        {
            _ticketSubtypeIdU = value;
        }
    }

    public string AttachmentOneFileNameU
    {
        get
        {
            return _attachmentOneFileNameU;
        }

        set
        {
            _attachmentOneFileNameU = value;
        }
    }

    public string AttachmentTwoFileNameU
    {
        get
        {
            return _attachmentTwoFileNameU;
        }
        set
        {
            _attachmentTwoFileNameU = value;
        }
    }
     public string AttachmentThreeFileNameU
    {
        get
        {
            return _attachmentThreeFileNameU;
        }
        set
        {
            _attachmentThreeFileNameU = value;
        }

    }

    public string TicketDescriptionU
    {
        get
        {
            _ticketDescriptionU = txtTicketDescriptionToU.Text;
            return _ticketDescriptionU;
        }

        set
        {
            _ticketDescriptionU = value;
        }
    }

    public string RemarksU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtTicketRemarksToU.Text))
                _remarksU = txtTicketRemarksToU.Text;
            else _remarksU = "";

            return _remarksU;
        }

        set
        {
            _remarksU = value;
        }
    }

    public int TicketIdU
    {
        get
        {
            if (Convert.ToInt32(ViewState["TICKET_ID"]) != 0)
                _ticketIdU = Convert.ToInt32(ViewState["TICKET_ID"]);
            else _ticketIdU = 0;

            return _ticketIdU;
        }

        set
        {
            _ticketIdU = value;
        }
    }

    public int TicketStatusId
    {
        get
        {
            return _ticketStatusId;
        }

        set
        {
            _ticketStatusId = value;
        }
    }

    public string TicketNumberU
    {
        get
        {
            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["TICKET_NUMBER"])))
                _ticketNumberU = Convert.ToString(ViewState["TICKET_NUMBER"]);
            else _ticketNumberU = "";

            return _ticketNumberU;
        }

        set
        {
            _ticketNumberU = value;
        }
    }

    public int AllocateToIdU
    {
        get
        {
            //if (ddlAllocateToToU.SelectedIndex > 0)
            if (!string.IsNullOrEmpty(ddlAllocateToToU.SelectedValue))
            {
                _allocateToIdU = Convert.ToInt32(ddlAllocateToToU.SelectedValue);
            }
            else _allocateToIdU = 0;

            return _allocateToIdU;
        }

        set
        {
            _allocateToIdU = value;
        }
    }

    public string AllocatedToPriority
    {
        get
        {
            //if (ddlAllocateToToU.SelectedIndex > 0)
            if (!string.IsNullOrEmpty(ddlAllocatePriority.SelectedValue))
            {
                _allocatedToPriority = Convert.ToString(ddlAllocatePriority.SelectedValue);
            }
            else _allocatedToPriority = "";

            return _allocatedToPriority;
        }

        set
        {
            _allocatedToPriority = value;
        }
    }

    //_allocatedToPriority

    public int AllocatedToIdS
    {
        get
        {
            if (ddlAllocatedTo.SelectedIndex > 0)
                _allocatedToIdS = Convert.ToInt32(ddlAllocatedTo.SelectedValue);
            else _allocatedToIdS = 0;
            return _allocatedToIdS;
        }

        set
        {
            _allocatedToIdS = value;
        }
    }

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtTicketList"] = null;
                Session["dtRecpList"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindCreatedBy();
                BindAllocatedTo();
                BindTicketStatus();
                BindTicketType();
                BindBlankTicketSubtype();
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

    protected void ddlTicketType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlankTicketSubtype();

        if (ddlTicketType.SelectedIndex > 0)
            GetTicketSubtype(Convert.ToInt32(ddlTicketType.SelectedValue));
    }

    protected void gvTicketList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "STATUS" ||
                    Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "CANCEL" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                else if (Convert.ToString(e.CommandArgument) == "ALLOCATE" ||
                         Convert.ToString(e.CommandArgument) == "CLOSE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblAllocatedToId = gvTicketList.Rows[rowindex].FindControl("lblAllocatedToId") as Label;

                Label lblTicketStatus = gvTicketList.Rows[rowindex].FindControl("lblTicketStatus") as Label;
                Label lblTicketID = gvTicketList.Rows[rowindex].FindControl("lblTicketID") as Label;
                Label lblTicketNO = gvTicketList.Rows[rowindex].FindControl("lblTicketNO") as Label;
                Label lblTicketStatusID = gvTicketList.Rows[rowindex].FindControl("lblTicketStatusID") as Label;

                Label lblTicketTypeID = gvTicketList.Rows[rowindex].FindControl("lblTicketTypeID") as Label;
                Label lblTicketType = gvTicketList.Rows[rowindex].FindControl("lblTicketType") as Label;

                Label lblTicketSubtypeID = gvTicketList.Rows[rowindex].FindControl("lblTicketSubtypeID") as Label;
                Label lblTicketSubtype = gvTicketList.Rows[rowindex].FindControl("lblTicketSubtype") as Label;

                Label lblDescription = gvTicketList.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblAllocatedToID = gvTicketList.Rows[rowindex].FindControl("lblAllocatedToID") as Label;

                Label lblCreatedByID = gvTicketList.Rows[rowindex].FindControl("lblCreatedByID") as Label;
                Label lblRemarks = gvTicketList.Rows[rowindex].FindControl("lblRemarks") as Label;

                Label lblAllocatedByID = gvTicketList.Rows[rowindex].FindControl("lblAllocatedByID") as Label;
                Label lblAllocatedRemarks = gvTicketList.Rows[rowindex].FindControl("lblAllocatedRemarks") as Label;

                Label lblClosedByID = gvTicketList.Rows[rowindex].FindControl("lblClosedByID") as Label;
                Label lblClosedRemarks = gvTicketList.Rows[rowindex].FindControl("lblClosedRemarks") as Label;

                Label lblCancelledByID = gvTicketList.Rows[rowindex].FindControl("lblCancelledByID") as Label;
                Label lblCancelledRemarks = gvTicketList.Rows[rowindex].FindControl("lblCancelledRemarks") as Label;


                Label lblFileOneName = gvTicketList.Rows[rowindex].FindControl("lblFileOneName") as Label;
                Label lblFileTwoName = gvTicketList.Rows[rowindex].FindControl("lblFileTwoName") as Label;
                Label lblFileThreeName = gvTicketList.Rows[rowindex].FindControl("lblFileThreeName") as Label;

                Label lblClosingFileOneName = gvTicketList.Rows[rowindex].FindControl("lblClosingFileOneName") as Label;
                Label lblClosingFileTwoName = gvTicketList.Rows[rowindex].FindControl("lblClosingFileTwoName") as Label;
                Label lblClosingFileThreeName = gvTicketList.Rows[rowindex].FindControl("lblClosingFileThreeName") as Label;


                pnlTicketSubType.Visible = false;
                pnlAttachments.Visible = false;
                pnlViewAttachments.Visible = false;

                pnlCreatedRemarks.Visible = false;
                pnlAllocateTo.Visible = false;
                pnlAllocatedRemarks.Visible = false;

                string FileOneNameExtn = string.Empty;
                string FileTwoNameExtn = string.Empty;
                string FileThreeNameExtn = string.Empty;

                string FileClosingOneNameExtn = string.Empty;
                string FileClosingTwoNameExtn = string.Empty;
                string FileClosingThreeNameExtn = string.Empty;


                ViewState["TICKET_ID"] = Convert.ToInt32(lblTicketID.Text);
                ViewState["TICKET_TYPE_ID"] = Convert.ToInt32(lblTicketTypeID.Text);
                ViewState["TICKET_SUBTYPE_ID"] = Convert.ToInt32(lblTicketSubtypeID.Text);

                ViewState["TICKET_NUMBER"] = Convert.ToString(lblTicketNO.Text);

                ViewState["CREATED_BY_ID"] = Convert.ToInt32(lblCreatedByID.Text);
                ViewState["ALLOCATED_TO_ID"] = Convert.ToInt32(lblAllocatedToId.Text);

                BindCreatedBy();
                ddlCreatedBy.SelectedValue = Convert.ToString(lblCreatedByID.Text);
                //txtCreatedByID.Text = Convert.ToString(lblCreatedByID.Text);



                if (!string.IsNullOrEmpty(lblFileOneName.Text))
                {
                    pnlViewAttachments.Visible = true;

                    FileOneNameExtn = Convert.ToString(lblFileOneName.Text).Split('.').Last();
                    if (FileOneNameExtn == "jpg" || FileOneNameExtn == "jpeg" || FileOneNameExtn == "bmp" || FileOneNameExtn == "png" || FileOneNameExtn == "gif" || FileOneNameExtn == "JPG" || FileOneNameExtn == "JPEG" || FileOneNameExtn == "BMP" || FileOneNameExtn == "PNG" || FileOneNameExtn == "GIF")
                    {
                        btnViewAttachment1.ImageUrl = "~/Images/imgicon1.png";
                        btnViewAttachment1.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "pdf" || FileOneNameExtn == "PDF")
                    {
                        btnViewAttachment1.ImageUrl = "~/Images/pdficon1.png";
                        btnViewAttachment1.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "xls" || FileOneNameExtn == "xlsx" ||
                    FileOneNameExtn == "XLS" || FileOneNameExtn == "XLSX")
                    {
                        btnViewAttachment1.ImageUrl = "~/Images/newexcelicon.png";
                        btnViewAttachment1.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "eml" || FileOneNameExtn == "msg" ||
                    FileOneNameExtn == "EML" || FileOneNameExtn == "MSG")
                    {
                        btnViewAttachment1.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnViewAttachment1.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "doc" || FileOneNameExtn == "docx" ||
                     FileOneNameExtn == "DOC" || FileOneNameExtn == "DOCX")
                    {
                        // Make sure you have a word icon in your Images folder
                        btnViewAttachment1.ImageUrl = "~/Images/wordlogo.png";
                        btnViewAttachment1.ToolTip = lblFileOneName.Text;
                    }

                }
                else
                {
                    pnlViewAttachments.Visible = false;
                }


                if(!string.IsNullOrEmpty(lblFileTwoName.Text))
                {
                    pnlViewAttachments.Visible = true;
                    FileTwoNameExtn = Convert.ToString(lblFileTwoName.Text).Split('.').Last();
                    if(FileTwoNameExtn == "jpg" || FileTwoNameExtn == "jpeg" || FileTwoNameExtn == "bmp" || FileTwoNameExtn == "png" || FileTwoNameExtn == "gif" || FileTwoNameExtn == "JPG" || FileTwoNameExtn == "JPEG" || FileTwoNameExtn == "BMP" || FileTwoNameExtn == "PNG" || FileTwoNameExtn == "GIF" )
                    {
                        btnViewAttachment2.ImageUrl = "~/Images/imgicon1.png";
                        btnViewAttachment2.ToolTip = lblFileTwoName.Text;
                    }
                    else if(FileTwoNameExtn == "pdf" || FileTwoNameExtn == "PDF")
                    {
                        btnViewAttachment2.ImageUrl = "~/Images/pdficon1.png";
                        btnViewAttachment2.ToolTip = lblFileTwoName.Text;
                    }
                    else if (FileTwoNameExtn == "xls" || FileTwoNameExtn == "xlsx" ||
                    FileTwoNameExtn == "XLS" || FileTwoNameExtn == "XLSX")
                    {
                        btnViewAttachment2.ImageUrl = "~/Images/newexcelicon.png";
                        btnViewAttachment2.ToolTip = lblFileTwoName.Text;
                    }
                    else if (FileTwoNameExtn == "eml" || FileTwoNameExtn == "msg" ||
                  FileTwoNameExtn == "EML" || FileTwoNameExtn == "MSG")
                    {
                        btnViewAttachment2.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnViewAttachment2.ToolTip = lblFileTwoName.Text;
                    }
                    else if (FileTwoNameExtn == "doc" || FileTwoNameExtn == "docx" ||
                     FileTwoNameExtn == "DOC" || FileTwoNameExtn == "DOCX")
                    {
                        // Make sure you have a word icon in your Images folder
                        btnViewAttachment2.ImageUrl = "~/Images/wordlogo.png";
                        btnViewAttachment2.ToolTip = lblFileTwoName.Text;
                    }
                }
                else
                {
                    pnlViewAttachments.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblFileThreeName.Text))
                {
                    pnlViewAttachments.Visible = true;
                    FileThreeNameExtn = Convert.ToString(lblFileThreeName.Text).Split('.').Last();
                    if (FileThreeNameExtn == "jpg" || FileThreeNameExtn == "jpeg" || FileThreeNameExtn == "bmp" || FileThreeNameExtn == "png" || FileThreeNameExtn == "gif" || FileThreeNameExtn == "JPG" || FileThreeNameExtn == "JPEG" || FileThreeNameExtn == "BMP" || FileThreeNameExtn == "PNG" || FileThreeNameExtn == "GIF")
                    {
                        btnViewAttachment3.ImageUrl = "~/Images/imgicon1.png";
                        btnViewAttachment3.ToolTip = lblFileThreeName.Text;
                    }
                    else if (FileThreeNameExtn == "pdf" || FileThreeNameExtn == "PDF")
                    {
                        btnViewAttachment3.ImageUrl = "~/Images/pdficon1.png";
                        btnViewAttachment3.ToolTip = lblFileTwoName.Text;
                    }
                    else if (FileThreeNameExtn == "xls" || FileThreeNameExtn == "xlsx" ||
                   FileThreeNameExtn == "XLS" || FileThreeNameExtn == "XLSX")
                    {
                        btnViewAttachment3.ImageUrl = "~/Images/newexcelicon.png";
                        btnViewAttachment3.ToolTip = lblFileThreeName.Text;
                    }
                    else if (FileThreeNameExtn == "doc" || FileThreeNameExtn == "docx" ||
                     FileThreeNameExtn == "DOC" || FileThreeNameExtn == "DOCX")
                    {
                        // Make sure you have a word icon in your Images folder
                        btnViewAttachment3.ImageUrl = "~/Images/wordlogo.png";
                        btnViewAttachment3.ToolTip = lblFileThreeName.Text;
                    }

                    else if (FileThreeNameExtn == "eml" || FileThreeNameExtn == "msg" ||
               FileThreeNameExtn == "EML" || FileThreeNameExtn == "MSG")
                    {
                        btnViewAttachment3.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnViewAttachment3.ToolTip = lblFileThreeName.Text;
                    }
                }
                else
                {
                    pnlViewAttachments.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblClosingFileOneName.Text))
                {
                    pnlViewAttachments.Visible = true;
                    FileClosingOneNameExtn = Convert.ToString(lblClosingFileOneName.Text).Split('.').Last();
                    if (FileClosingOneNameExtn == "jpg" || FileClosingOneNameExtn == "jpeg" || FileClosingOneNameExtn == "bmp" || FileClosingOneNameExtn == "png" || FileClosingOneNameExtn == "gif" || FileClosingOneNameExtn == "JPG" || FileClosingOneNameExtn == "JPEG" || FileClosingOneNameExtn == "BMP" || FileClosingOneNameExtn == "PNG" || FileClosingOneNameExtn == "GIF")
                    {
                        btnViewClosingAttachment.ImageUrl = "~/Images/imgicon1.png";
                        btnViewClosingAttachment.ToolTip = lblClosingFileOneName.Text;
                    }
                    else if (FileClosingOneNameExtn == "pdf" || FileClosingOneNameExtn == "PDF")
                    {
                        btnViewClosingAttachment.ImageUrl = "~/Images/pdficon1.png";
                        btnViewClosingAttachment.ToolTip = lblClosingFileOneName.Text;
                    }
                    else if (FileClosingOneNameExtn == "xls" || FileClosingOneNameExtn == "xlsx" ||
                  FileClosingOneNameExtn == "XLS" || FileClosingOneNameExtn == "XLSX")
                    {
                        btnViewClosingAttachment.ImageUrl = "~/Images/newexcelicon.png";
                        btnViewClosingAttachment.ToolTip = lblClosingFileOneName.Text;
                    }
                    else if (FileClosingOneNameExtn == "doc" || FileClosingOneNameExtn == "docx" ||
                     FileClosingOneNameExtn == "DOC" || FileClosingOneNameExtn == "DOCX")
                    {
                        // Make sure you have a word icon in your Images folder
                        btnViewClosingAttachment.ImageUrl = "~/Images/wordlogo.png";
                        btnViewClosingAttachment.ToolTip = lblClosingFileOneName.Text;
                    }
                    else if (FileClosingOneNameExtn == "eml" || FileClosingOneNameExtn == "msg" ||
               FileClosingOneNameExtn == "EML" || FileClosingOneNameExtn == "MSG")
                    {
                        btnViewClosingAttachment.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnViewClosingAttachment.ToolTip = lblClosingFileOneName.Text;
                    }

                }
                else
                {
                    pnlViewAttachments.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblClosingFileTwoName.Text))
                {
                    pnlViewAttachments.Visible = true;
                    FileClosingTwoNameExtn = Convert.ToString(lblClosingFileTwoName.Text).Split('.').Last();
                    if (FileClosingTwoNameExtn == "jpg" || FileClosingTwoNameExtn == "jpeg" || FileClosingTwoNameExtn == "bmp" || FileClosingTwoNameExtn == "png" || FileClosingTwoNameExtn == "gif" || FileClosingTwoNameExtn == "JPG" || FileClosingTwoNameExtn == "JPEG" || FileClosingTwoNameExtn == "BMP" || FileClosingTwoNameExtn == "PNG" || FileClosingTwoNameExtn == "GIF")
                    {
                        btnViewClosingAttachment2.ImageUrl = "~/Images/imgicon1.png";
                        btnViewClosingAttachment2.ToolTip = lblClosingFileTwoName.Text;
                    }
                    else if (FileClosingTwoNameExtn == "pdf" || FileClosingTwoNameExtn == "PDF")
                    {
                        btnViewClosingAttachment2.ImageUrl = "~/Images/pdficon1.png";
                        btnViewClosingAttachment2.ToolTip = lblClosingFileTwoName.Text;
                    }
                    else if (FileClosingTwoNameExtn == "xls" || FileClosingTwoNameExtn == "xlsx" ||
                    FileClosingTwoNameExtn == "XLS" || FileClosingTwoNameExtn == "XLSX")
                    {
                        btnViewClosingAttachment2.ImageUrl = "~/Images/newexcelicon.png";
                        btnViewClosingAttachment2.ToolTip = lblClosingFileTwoName.Text;
                    }
                    else if (FileClosingTwoNameExtn == "eml" || FileClosingTwoNameExtn == "msg" ||
                    FileClosingTwoNameExtn == "EML" || FileClosingTwoNameExtn == "MSG")
                    {
                        btnViewClosingAttachment2.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnViewClosingAttachment2.ToolTip = lblClosingFileTwoName.Text;
                    }
                    else if (FileClosingTwoNameExtn == "doc" || FileClosingTwoNameExtn == "docx" ||
                  FileClosingTwoNameExtn == "DOC" || FileClosingTwoNameExtn == "DOCX")
                    {
                        // Make sure you have a word icon in your Images folder
                        btnViewClosingAttachment2.ImageUrl = "~/Images/wordlogo.png";
                        btnViewClosingAttachment2.ToolTip = lblClosingFileTwoName.Text;
                    }

                }
                else
                {
                    pnlViewAttachments.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblClosingFileThreeName.Text))
                {
                    pnlViewAttachments.Visible = true;
                    FileClosingThreeNameExtn = Convert.ToString(lblClosingFileThreeName.Text).Split('.').Last();
                    if (FileClosingThreeNameExtn == "jpg" || FileClosingThreeNameExtn == "jpeg" || FileClosingThreeNameExtn == "bmp" || FileClosingThreeNameExtn == "png" || FileClosingThreeNameExtn == "gif" || FileClosingThreeNameExtn == "JPG" || FileClosingThreeNameExtn == "JPEG" || FileClosingThreeNameExtn == "BMP" || FileClosingThreeNameExtn == "PNG" || FileClosingThreeNameExtn == "GIF")
                    {
                        btnViewClosingAttachment3.ImageUrl = "~/Images/imgicon1.png";
                        btnViewClosingAttachment3.ToolTip = lblClosingFileThreeName.Text;
                    }
                    else if (FileClosingThreeNameExtn == "pdf" || FileClosingThreeNameExtn == "PDF")
                    {
                        btnViewClosingAttachment3.ImageUrl = "~/Images/pdficon1.png";
                        btnViewClosingAttachment3.ToolTip = lblClosingFileThreeName.Text;
                    }
                    else if (FileClosingThreeNameExtn == "xls" || FileClosingThreeNameExtn == "xlsx" ||
                   FileClosingThreeNameExtn == "XLS" || FileClosingThreeNameExtn == "XLSX")
                    {
                        btnViewClosingAttachment3.ImageUrl = "~/Images/newexcelicon.png";
                        btnViewClosingAttachment3.ToolTip = lblClosingFileThreeName.Text;
                    }
                    else if (FileClosingThreeNameExtn == "doc" || FileClosingThreeNameExtn == "docx" ||
                  FileClosingThreeNameExtn == "DOC" || FileClosingThreeNameExtn == "DOCX")
                    {
                        // Make sure you have a word icon in your Images folder
                        btnViewClosingAttachment3.ImageUrl = "~/Images/wordlogo.png";
                        btnViewClosingAttachment3.ToolTip = lblClosingFileThreeName.Text;
                    }
                    else if (FileClosingThreeNameExtn == "eml" || FileClosingThreeNameExtn == "msg" ||
                  FileClosingThreeNameExtn == "EML" || FileClosingThreeNameExtn == "MSG")
                    {
                        btnViewClosingAttachment3.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnViewClosingAttachment3.ToolTip = lblClosingFileThreeName.Text;
                    }
                }
                else
                {
                    pnlViewAttachments.Visible = false;
                }

                txtStatusToU.Text = string.Empty;
                txtTicketRemarksToU.Text = string.Empty;
                pnlStatus.Visible = false;
                btnAddStatus.Visible = false;
                btnSubmit.Visible = false;
                btnAddStatusAndCloseTicket.Visible = false;

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    lblRemarksToU.Text = "Updated Remarks:";
                    lblLegend.Text = "Ticket No[" + lblTicketNO.Text + "]";
                    ViewState["ACT_ID"] = (int)HRTicketEnums.EnumActions.Update;
                    pnlAttachments.Visible = true;
                    pnlViewAttachments.Visible = false;
                    btnSubmit.Text = "Update Ticket";
                    //btnSubmit.Visible = true;
                    txtTicketTypeToU.Text = Convert.ToString(lblTicketType.Text);
                    txtTicketSubtypeToU.Text = Convert.ToString(lblTicketSubtype.Text);
                    txtTicketDescriptionToU.Text = Convert.ToString(lblDescription.Text);

                    pnlCreatedRemarks.Visible = true;
                    txtCreatedRemarksToU.Text = Convert.ToString(lblRemarks.Text);

                    ModalPopupExtender1.Show();
                }
                else if (Convert.ToString(e.CommandArgument) == "ALLOCATE")
                {
                    lblRemarksToU.Text = "Allocated Remarks:";
                    BindAllocateTo(Convert.ToInt32(lblTicketID.Text));
                    BindAllocatePriority();
                    lblLegend.Text = "Ticket No[" + lblTicketNO.Text + "]";
                    ViewState["ACT_ID"] = (int)HRTicketEnums.EnumActions.Allocate;
                    txtViewAttachment1.Text = lblFileOneName.Text;

                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Allocate Ticket";

                    txtTicketTypeToU.Text = Convert.ToString(lblTicketType.Text);
                    txtTicketSubtypeToU.Text = Convert.ToString(lblTicketSubtype.Text);
                    txtTicketDescriptionToU.Text = Convert.ToString(lblDescription.Text);

                    pnlTicketSubType.Visible = true;
                    pnlCreatedRemarks.Visible = true;
                    pnlAllocateTo.Visible = true;
                    txtCreatedRemarksToU.Text = Convert.ToString(lblRemarks.Text);

                    txtTicketDescriptionToU.Enabled = false;

                    ModalPopupExtender1.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "REALLOCATE")
                {
                    lblRemarksToU.Text = "Reallocation Remarks:";
                    BindAllocateTo(Convert.ToInt32(lblTicketID.Text));
                    BindAllocatePriority();
                    lblLegend.Text = "Ticket No[" + lblTicketNO.Text + "]";
                    ViewState["ACT_ID"] = (int)HRTicketEnums.EnumActions.Allocate;
                    txtViewAttachment1.Text = lblFileOneName.Text;

                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Reallocate Ticket";

                    txtTicketTypeToU.Text = Convert.ToString(lblTicketType.Text);
                    txtTicketSubtypeToU.Text = Convert.ToString(lblTicketSubtype.Text);
                    txtTicketDescriptionToU.Text = Convert.ToString(lblDescription.Text);

                    pnlTicketSubType.Visible = true;
                    pnlCreatedRemarks.Visible = true;
                    pnlAllocateTo.Visible = true;
                    txtCreatedRemarksToU.Text = Convert.ToString(lblRemarks.Text);

                    txtTicketDescriptionToU.Enabled = false;

                    ModalPopupExtender1.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "CLOSE")
                {
                    lblRemarksToU.Text = "Closing Remarks:";
                    lblLegend.Text = "Ticket No[" + lblTicketNO.Text + "]";
                    ViewState["ACT_ID"] = (int)HRTicketEnums.EnumActions.Close;
                    txtViewAttachment1.Text = lblFileOneName.Text;
                    txtViewAttachment2.Text = lblFileOneName.Text;
                    txtViewAttachment3.Text = lblFileOneName.Text;
                    txtViewClosingAttachment1.Text = lblFileOneName.Text;
                    txtViewClosingAttachment2.Text = lblFileOneName.Text;
                    txtViewClosingAttachment3.Text = lblFileOneName.Text;

                    lblClosingAttachment1.Text = lblClosingFileOneName.Text;
                    lblClosingAttachment2.Text = lblClosingFileTwoName.Text;
                    lblClosingAttachment3.Text = lblClosingFileThreeName.Text;

                    btnAddStatus.Visible = true;
                    btnSubmit.Visible = true;
                    btnAddStatusAndCloseTicket.Visible = true;
                    btnSubmit.Text = "Close Ticket";

                    pnlStatus.Visible = true;
                    txtTicketTypeToU.Text = Convert.ToString(lblTicketType.Text);
                    txtTicketSubtypeToU.Text = Convert.ToString(lblTicketSubtype.Text);
                    txtTicketDescriptionToU.Text = Convert.ToString(lblDescription.Text);

                    pnlCreatedRemarks.Visible = true;

                    txtCreatedRemarksToU.Text = Convert.ToString(lblRemarks.Text);

                    if (Convert.ToInt32(lblTicketTypeID.Text) == (int)HRTicketEnums.EnumType.HR)
                    {
                        pnlTicketSubType.Visible = true;
                        pnlAllocatedRemarks.Visible = true;
                        txtAllocatedRemarksToU.Text = Convert.ToString(lblAllocatedRemarks.Text);
                    }

                    txtTicketDescriptionToU.Enabled = false;

                    ModalPopupExtender1.Show();
                }


                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    lblRemarksToU.Text = "Cancelled Remarks:";
                    lblLegend.Text = "Ticket No[" + lblTicketNO.Text + "]";
                    ViewState["ACT_ID"] = (int)HRTicketEnums.EnumActions.Cancel;
                    txtViewAttachment1.Text = lblFileOneName.Text;
                    btnSubmit.Text = "Cancel Ticket";

                    txtTicketTypeToU.Text = Convert.ToString(lblTicketType.Text);
                    txtTicketSubtypeToU.Text = Convert.ToString(lblTicketSubtype.Text);
                    txtTicketDescriptionToU.Text = Convert.ToString(lblDescription.Text);
                    btnSubmit.Visible = true;
                    pnlCreatedRemarks.Visible = true;
                    txtCreatedRemarksToU.Text = Convert.ToString(lblRemarks.Text);

                    if (Convert.ToInt32(lblTicketTypeID.Text) == (int)HRTicketEnums.EnumType.HR)
                    {
                        pnlTicketSubType.Visible = true;
                    }

                    txtTicketDescriptionToU.Enabled = false;

                    ModalPopupExtender1.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    mpeViewInPDF.Show();
                    iframeViewDrawingDetailsInPDF.Attributes.Add("src", "TicketDetailInPDF.aspx?recordID=" + Convert.ToInt32(lblTicketID.Text) + "");
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "ATTACHMENT1");
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "ATTACHMENT2");
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "ATTACHMENT3");
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT1")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "CLOSINGATTACHMENT1");
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT2")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "CLOSINGATTACHMENT2");
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT3")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "CLOSINGATTACHMENT3");
                }
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
                DataTable dtRecp = new DataTable();

                if (Session["dtRecpList"] != null)
                    dtRecp = (DataTable)Session["dtRecpList"];

                int rowindex = e.Row.RowIndex;
                ImageButton btnFileOneName = (ImageButton)e.Row.FindControl("btnFileOneName");
                ImageButton btnFileTwoName = (ImageButton)e.Row.FindControl("btnFileTwoName");
                ImageButton btnFileThreeName = (ImageButton)e.Row.FindControl("btnFileThreeName");

                ImageButton btnClosingFileOneName = (ImageButton)e.Row.FindControl("btnClosingFileOneName");
                ImageButton btnClosingFileTwoName = (ImageButton)e.Row.FindControl("btnClosingFileTwoName");
                ImageButton btnClosingFileThreeName = (ImageButton)e.Row.FindControl("btnClosingFileThreeName");

                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");
                ImageButton imgBtnCancel = (ImageButton)e.Row.FindControl("imgBtnCancel");

                Button btnAllocate = (Button)e.Row.FindControl("btnAllocate");
                Button btnClose = (Button)e.Row.FindControl("btnClose");

                Button btnReAllocate = (Button)e.Row.FindControl("btnReAllocate");

                btnFileOneName.Visible = false;
                btnFileTwoName.Visible = false;
                btnFileThreeName.Visible = false;
                btnClosingFileOneName.Visible = false;
                btnClosingFileTwoName.Visible = false;
                btnClosingFileThreeName.Visible = false;
                imgProperties.Visible = false;

                btnAllocate.Visible = false;
                btnClose.Visible = false;
                btnReAllocate.Visible = false;

                Label lblFileOneName = (Label)e.Row.FindControl("lblFileOneName");
                Label lblFileTwoName = (Label)e.Row.FindControl("lblFileTwoName");
                Label lblFileThreeName = (Label)e.Row.FindControl("lblFileThreeName");

                Label lblClosingFileOneName = (Label)e.Row.FindControl("lblClosingFileOneName");
                Label lblClosingFileTwoName = (Label)e.Row.FindControl("lblClosingFileTwoName");
                Label lblClosingFileThreeName = (Label)e.Row.FindControl("lblClosingFileThreeName");

                string FileOneNameExtn = string.Empty;
                string FileTwoNameExtn = string.Empty;
                string FileThreeNameExtn = string.Empty;

                string FileClosingOneNameExtn = string.Empty;
                string FileClosingTwoNameExtn = string.Empty;
                string FileClosingThreeNameExtn = string.Empty;

                if (!string.IsNullOrEmpty(lblFileOneName.Text))
                {
                    btnFileOneName.Visible = true;

                    FileOneNameExtn = Convert.ToString(lblFileOneName.Text).Split('.').Last();
                    if (FileOneNameExtn == "jpg" ||
                        FileOneNameExtn == "jpeg" ||
                        FileOneNameExtn == "bmp" ||
                        FileOneNameExtn == "png" ||
                        FileOneNameExtn == "gif" ||
                     
                        FileOneNameExtn == "JPG" ||
                        FileOneNameExtn == "JPEG" ||
                        FileOneNameExtn == "BMP" ||
                        FileOneNameExtn == "PNG" ||
                        FileOneNameExtn == "GIF")
                    {
                        btnFileOneName.ImageUrl = "~/Images/imgicon1.png";
                        btnFileOneName.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "DOC" || FileOneNameExtn == "DOCX" || FileOneNameExtn == "doc" || FileOneNameExtn == "docx")
                    {
                        btnFileOneName.ImageUrl = "~/Images/wordlogo.png";
                        btnFileOneName.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "pdf" || FileOneNameExtn == "PDF")
                    {
                        btnFileOneName.ImageUrl = "~/Images/pdficon1.png";
                        btnFileOneName.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "xls" || FileOneNameExtn == "xlsx" || FileOneNameExtn == "XLS" || FileOneNameExtn == "XLSX")
                    {
                        btnFileOneName.ImageUrl = "~/Images/newexcelicon.png";
                        btnFileOneName.ToolTip = lblFileOneName.Text;
                    }
                    else if (FileOneNameExtn == "eml" || FileOneNameExtn == "msg" ||
                    FileOneNameExtn == "EML" || FileOneNameExtn == "MSG")
                    {
                        btnFileOneName.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnFileOneName.ToolTip = lblFileOneName.Text;
                    }

                }

                if (!string.IsNullOrEmpty(lblFileTwoName.Text))
                {
                    btnFileTwoName.Visible = true;

                    FileTwoNameExtn = Convert.ToString(lblFileTwoName.Text).Split('.').Last();
                    if (FileTwoNameExtn == "jpg" ||
                        FileTwoNameExtn == "jpeg" ||
                        FileTwoNameExtn == "bmp" ||
                        FileTwoNameExtn == "png" ||
                        FileTwoNameExtn == "gif" ||
                        FileTwoNameExtn == "JPG" ||
                        FileTwoNameExtn == "JPEG" ||
                        FileTwoNameExtn == "BMP" ||
                        FileTwoNameExtn == "PNG" ||
                        FileTwoNameExtn == "GIF")
                    {
                        btnFileTwoName.ImageUrl = "~/Images/imgicon1.png";
                        btnFileTwoName.ToolTip = lblFileTwoName.Text;
                    }
                    else if (FileTwoNameExtn == "pdf" || FileTwoNameExtn == "PDF")
                    {
                        btnFileTwoName.ImageUrl = "~/Images/pdficon1.png";
                        btnFileTwoName.ToolTip = lblFileTwoName.Text;
                    }
                    else if (FileTwoNameExtn == "DOC" || FileTwoNameExtn == "DOCX" || FileTwoNameExtn == "doc" || FileTwoNameExtn == "docx")
                    {
                        btnFileTwoName.ImageUrl = "~/Images/wordlogo.png";
                        btnFileTwoName.ToolTip = lblFileTwoName.Text;
                    }
                    else if (FileTwoNameExtn == "xls" || FileTwoNameExtn == "xlsx" || FileTwoNameExtn == "XLS" || FileTwoNameExtn == "XLSX")
                    {
                        btnFileTwoName.ImageUrl = "~/Images/newexcelicon.png";
                        btnFileTwoName.ToolTip = lblFileTwoName.Text;
                    }
                   else if (FileTwoNameExtn == "eml" || FileTwoNameExtn == "msg" ||
                   FileTwoNameExtn == "EML" || FileTwoNameExtn == "MSG")
                    {
                        btnFileTwoName.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnFileTwoName.ToolTip = lblFileTwoName.Text;
                    }
                }

                if (!string.IsNullOrEmpty(lblFileThreeName.Text))
                {
                    btnFileThreeName.Visible = true;

                    FileThreeNameExtn = Convert.ToString(lblFileThreeName.Text).Split('.').Last();
                    if (FileThreeNameExtn == "jpg" ||
                        FileThreeNameExtn == "jpeg" ||
                        FileThreeNameExtn == "bmp" ||
                        FileThreeNameExtn == "png" ||
                        FileThreeNameExtn == "gif" ||
                        FileThreeNameExtn == "JPG" ||
                        FileThreeNameExtn == "JPEG" ||
                        FileThreeNameExtn == "BMP" ||
                        FileThreeNameExtn == "PNG" ||
                        FileThreeNameExtn == "GIF")
                    {
                        btnFileThreeName.ImageUrl = "~/Images/imgicon1.png";
                        btnFileThreeName.ToolTip = lblFileThreeName.Text;
                    }
                    else if (FileThreeNameExtn == "pdf" || FileThreeNameExtn == "PDF")
                    {
                        btnFileThreeName.ImageUrl = "~/Images/pdficon1.png";
                        btnFileThreeName.ToolTip = lblFileThreeName.Text;
                    }
                    else if (FileThreeNameExtn == "xls" || FileThreeNameExtn == "xlsx" || FileThreeNameExtn == "XLS" || FileThreeNameExtn == "XLSX")
                    {
                        btnFileThreeName.ImageUrl = "~/Images/newexcelicon.png";
                        btnFileThreeName.ToolTip = lblFileThreeName.Text;
                    }
                    else if (FileThreeNameExtn == "DOC" || FileThreeNameExtn == "DOCX" || FileThreeNameExtn == "doc" || FileThreeNameExtn == "docx")
                    {
                        btnFileThreeName.ImageUrl = "~/Images/wordlogo.png";
                        btnFileThreeName.ToolTip = lblFileThreeName.Text;
                    }
                    else if (FileThreeNameExtn == "eml" || FileThreeNameExtn == "msg" ||
                    FileThreeNameExtn == "EML" || FileThreeNameExtn == "MSG")
                    {
                        btnFileThreeName.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnFileThreeName.ToolTip = lblFileThreeName.Text;
                    }
                }


                if (!string.IsNullOrEmpty(lblClosingFileOneName.Text))
                {
                    btnClosingFileOneName.Visible = true;

                    FileClosingOneNameExtn = Convert.ToString(lblClosingFileOneName.Text).Split('.').Last();
                    if (FileClosingOneNameExtn == "jpg" ||
                        FileClosingOneNameExtn == "jpeg" ||
                        FileClosingOneNameExtn == "bmp" ||
                        FileClosingOneNameExtn == "png" ||
                        FileClosingOneNameExtn == "gif" ||
                        FileClosingOneNameExtn == "JPG" ||
                        FileClosingOneNameExtn == "JPEG" ||
                        FileClosingOneNameExtn == "BMP" ||
                        FileClosingOneNameExtn == "PNG" ||
                        FileClosingOneNameExtn == "GIF")
                    {
                        btnClosingFileOneName.ImageUrl = "~/Images/imgicon1.png";
                        btnClosingFileOneName.ToolTip = lblClosingFileOneName.Text;
                    }
                    else if (FileClosingOneNameExtn == "pdf" || FileClosingOneNameExtn == "PDF")
                    {
                        btnClosingFileOneName.ImageUrl = "~/Images/pdficon1.png";
                        btnClosingFileOneName.ToolTip = lblClosingFileOneName.Text;
                    }
                    //else if (FileClosingOneNameExtn == "xlsx" || FileClosingOneNameExtn == "PDF")
                    //{
                    //    btnClosingFileOneName.ImageUrl = "~/Images/pdficon1.png";
                    //    btnClosingFileOneName.ToolTip = lblClosingFileOneName.Text;
                    //}
                    else if (FileClosingOneNameExtn == "DOC" || FileClosingOneNameExtn == "DOCX" || FileClosingOneNameExtn == "doc" || FileClosingOneNameExtn == "docx")
                    {
                        btnClosingFileOneName.ImageUrl = "~/Images/wordlogo.png";
                        btnClosingFileOneName.ToolTip = lblClosingFileOneName.Text;
                    }
                    else if (FileClosingOneNameExtn == "xls" || FileClosingOneNameExtn == "xlsx" || FileClosingOneNameExtn == "XLS" || FileClosingOneNameExtn == "XLSX")
                    {
                        btnClosingFileOneName.ImageUrl = "~/Images/newexcelicon.png";
                        btnClosingFileOneName.ToolTip = lblClosingFileOneName.Text;
                    }
                    else if (FileClosingOneNameExtn == "eml" || FileClosingOneNameExtn == "msg" ||
                   FileClosingOneNameExtn == "EML" || FileClosingOneNameExtn == "MSG")
                    {
                        btnClosingFileOneName.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnClosingFileOneName.ToolTip = lblClosingFileOneName.Text;
                    }
                }

                if (!string.IsNullOrEmpty(lblClosingFileTwoName.Text))
                {
                    btnClosingFileTwoName.Visible = true;

                    FileClosingTwoNameExtn = Convert.ToString(lblClosingFileTwoName.Text).Split('.').Last();
                    if (FileClosingTwoNameExtn == "jpg" ||
                        FileClosingTwoNameExtn == "jpeg" ||
                        FileClosingTwoNameExtn == "bmp" ||
                        FileClosingTwoNameExtn == "png" ||
                        FileClosingTwoNameExtn == "gif" ||
                        FileClosingTwoNameExtn == "JPG" ||
                        FileClosingTwoNameExtn == "JPEG" ||
                        FileClosingTwoNameExtn == "BMP" ||
                        FileClosingTwoNameExtn == "PNG" ||
                        FileClosingTwoNameExtn == "GIF")
                    {
                        btnClosingFileTwoName.ImageUrl = "~/Images/imgicon1.png";
                        btnClosingFileTwoName.ToolTip = lblClosingFileTwoName.Text;
                    }
                    else if (FileClosingTwoNameExtn == "pdf" || FileClosingTwoNameExtn == "PDF")
                    {
                        btnClosingFileTwoName.ImageUrl = "~/Images/pdficon1.png";
                        btnClosingFileTwoName.ToolTip = lblClosingFileTwoName.Text;
                    }
                    else if (FileClosingTwoNameExtn == "DOC" || FileClosingTwoNameExtn == "DOCX" || FileClosingTwoNameExtn == "doc" || FileClosingTwoNameExtn == "docx")
                    {
                        btnClosingFileTwoName.ImageUrl = "~/Images/wordlogo.png";
                        btnClosingFileTwoName.ToolTip = lblClosingFileTwoName.Text;
                    }
                    else if (FileClosingTwoNameExtn == "xls" || FileClosingTwoNameExtn == "xlsx" || FileClosingTwoNameExtn == "XLS" || FileClosingTwoNameExtn == "XLSX")
                    {
                        btnClosingFileTwoName.ImageUrl = "~/Images/newexcelicon.png";
                        btnClosingFileTwoName.ToolTip = lblClosingFileTwoName.Text;
                    }
                    else if (FileClosingTwoNameExtn == "eml" || FileClosingTwoNameExtn == "msg" ||
                 FileClosingTwoNameExtn == "EML" || FileClosingTwoNameExtn == "MSG")
                    {
                        btnClosingFileTwoName.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnClosingFileTwoName.ToolTip = lblClosingFileTwoName.Text;
                    }
                }

                if (!string.IsNullOrEmpty(lblClosingFileThreeName.Text))
                {
                    btnClosingFileThreeName.Visible = true;

                    FileClosingThreeNameExtn = Convert.ToString(lblClosingFileThreeName.Text).Split('.').Last();
                    if (FileClosingThreeNameExtn == "jpg" ||
                        FileClosingThreeNameExtn == "jpeg" ||
                        FileClosingThreeNameExtn == "bmp" ||
                        FileClosingThreeNameExtn == "png" ||
                        FileClosingThreeNameExtn == "gif" ||
                        FileClosingThreeNameExtn == "JPG" ||
                        FileClosingThreeNameExtn == "JPEG" ||
                        FileClosingThreeNameExtn == "BMP" ||
                        FileClosingThreeNameExtn == "PNG" ||
                        FileClosingThreeNameExtn == "GIF")
                    {
                        btnClosingFileThreeName.ImageUrl = "~/Images/imgicon1.png";
                        btnClosingFileThreeName.ToolTip = lblClosingFileThreeName.Text;
                    }
                    else if (FileClosingThreeNameExtn == "pdf" || FileClosingThreeNameExtn == "PDF")
                    {
                        btnClosingFileThreeName.ImageUrl = "~/Images/pdficon1.png";
                        btnClosingFileThreeName.ToolTip = lblClosingFileThreeName.Text;
                    }
                    else if (FileClosingThreeNameExtn == "DOC" || FileClosingThreeNameExtn == "DOCX" || FileClosingThreeNameExtn == "doc" || FileClosingThreeNameExtn == "docx")
                    {
                        btnClosingFileThreeName.ImageUrl = "~/Images/wordlogo.png";
                        btnClosingFileThreeName.ToolTip = lblClosingFileThreeName.Text;
                    }
                    else if (FileClosingThreeNameExtn == "xls" || FileClosingThreeNameExtn == "xlsx" || FileClosingThreeNameExtn == "XLS" || FileClosingThreeNameExtn == "XLSX")
                    {
                        btnClosingFileThreeName.ImageUrl = "~/Images/newexcelicon.png";
                        btnClosingFileThreeName.ToolTip = lblClosingFileThreeName.Text;
                    }
                    else if (FileClosingThreeNameExtn == "eml" || FileClosingThreeNameExtn == "msg" ||
                FileClosingThreeNameExtn == "EML" || FileClosingThreeNameExtn == "MSG")
                    {
                        btnClosingFileThreeName.ImageUrl = "~/Images/NEWICONS/email02.png";
                        btnClosingFileThreeName.ToolTip = lblClosingFileThreeName.Text;
                    }
                }



                Label lblCreatedByID = (Label)e.Row.FindControl("lblCreatedByID");
                Label lblHodID = (Label)e.Row.FindControl("lblHodID");

                Label lblAllocateAuthority2 = (Label)e.Row.FindControl("lblAllocateAuthority2");
                Label lblAllocatedToID = (Label)e.Row.FindControl("lblAllocatedToID");
                Label lblAllocatedByID = (Label)e.Row.FindControl("lblAllocatedByID");
                Label lblClosedByID = (Label)e.Row.FindControl("lblClosedByID");
                Label lblCancelledByID = (Label)e.Row.FindControl("lblCancelledByID");

                Label lblTicketTypeID = (Label)e.Row.FindControl("lblTicketTypeID");
                Label lblTicketType = (Label)e.Row.FindControl("lblTicketType");


                Label lblTicketStatusID = (Label)e.Row.FindControl("lblTicketStatusID");

                if (Convert.ToInt32(lblTicketStatusID.Text) == (int)HRTicketEnums.EnumStatus.Open)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/BLACKOPEN.png";
                    imgStatus.ToolTip = "Open ticket";

                    if (Convert.ToInt32(lblCreatedByID.Text) == CurrentUserIdU)
                    {
                        imgProperties.Visible = true;
                    }


                    if (Convert.ToInt32(lblTicketTypeID.Text) == (int)HRTicketEnums.EnumType.HR)
                    {
                        if (Convert.ToInt32(lblHodID.Text) == CurrentUserIdU)
                        {

                            btnAllocate.Visible = true;
                        }
                        else if (Convert.ToInt32(lblAllocateAuthority2.Text) == CurrentUserIdU)
                        {
                            btnAllocate.Visible = true;
                        }
                    }
                    else if (Convert.ToInt32(lblTicketTypeID.Text) == (int)HRTicketEnums.EnumType.Admin)
                    {
                        if (Convert.ToInt32(lblHodID.Text) == CurrentUserIdU)
                        {

                            btnAllocate.Visible = true;
                        }
                        else if (Convert.ToInt32(lblAllocateAuthority2.Text) == CurrentUserIdU)
                        {
                            btnAllocate.Visible = true;
                        }

                        //foreach (DataRow drR in dtRecp.Select("TO_FID='" + CurrentUserIdU + "'"))
                        //{
                        //    btnClose.Visible = true;
                        //}
                    }

                }
                else if (Convert.ToInt32(lblTicketStatusID.Text) == (int)HRTicketEnums.EnumStatus.Allocated)
                {
                    imgStatus.ImageUrl = "~/Images/LOT/PM1.png";
                    imgStatus.ToolTip = "Allocated ticket";

                    if (Convert.ToInt32(lblAllocatedToID.Text) == CurrentUserIdU)
                    {
                        btnClose.Visible = true;
                        
                    }

                    if
                    (Convert.ToInt32(lblAllocateAuthority2.Text) == CurrentUserIdU || Convert.ToInt32(lblHodID.Text) == CurrentUserIdU)
                    {
                        btnReAllocate.Visible = true;
                    }

                }
                else if (Convert.ToInt32(lblTicketStatusID.Text) == (int)HRTicketEnums.EnumStatus.Closed)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Closed03.png";
                    imgStatus.ToolTip = "Closed ticket";

                    imgBtnCancel.Visible = false;
                }
                else if (Convert.ToInt32(lblTicketStatusID.Text) == (int)HRTicketEnums.EnumStatus.Cancelled)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/Cancelled03.png";
                    imgStatus.ToolTip = "Cancelled ticket";

                    imgBtnCancel.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnViewAttachment1_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "ATTACHMENT1");
        ModalPopupExtender1.Show();
    }

    protected void btnViewAttachment2_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "ATTACHMENT2");
        ModalPopupExtender1.Show();
    }

    protected void btnViewAttachment3_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "ATTACHMENT3");
        ModalPopupExtender1.Show();
    }

    protected void btnViewClosingAttachment_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "CLOSINGATTACHMENT1");
        ModalPopupExtender1.Show();
    }

    protected void btnViewClosingAttachment2_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "CLOSINGATTACHMENT2");
        ModalPopupExtender1.Show();
    }

    protected void btnViewClosingAttachment3_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachedFilesNew01(Convert.ToInt32(ViewState["TICKET_ID"]), "CLOSINGATTACHMENT3");
        ModalPopupExtender1.Show();
    }



    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HR/TICKET/AddUpdateTicket.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int isAddStatus = 0;
        int isClose = 1;

        if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)HRTicketEnums.EnumActions.Update)
            UpdateTicket();
        else
            UpdateTicketStatus(isAddStatus, isClose);


        ModalPopupExtender1.Hide();
        GetTicketList();
    }

    protected void btnAddStatus_Click(object sender, EventArgs e)
    {
        int isAddStatus = 1;
        int isClose = 0;

        UpdateTicketStatus(isAddStatus, isClose);

        ModalPopupExtender1.Hide();
        GetTicketList();
    }


    protected void btnAddStatusAndCloseTicket_Click(object sender, EventArgs e)
    {
        int isAddStatus = 1;
        int isClose = 1;

        if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)HRTicketEnums.EnumActions.Update)
            UpdateTicket();
        else
            UpdateTicketStatus(isAddStatus, isClose);

        ModalPopupExtender1.Hide();
        GetTicketList();
    }

    #endregion


    #region METHODS[=======================]

    private void BindCreatedBy()
    {
        try
        {
        //    dsCreatedBy = objTourAndTravels.GetEmployeeForTravel(0);
        //    if (dsCreatedBy.Tables.Count > 0 && dsCreatedBy.Tables[0].Rows.Count > 0)
        //    {
        //        ddlCreatedBy.DataSource = dsCreatedBy.Tables[0];
        //        ddlCreatedBy.DataTextField = "EMPLOYEE_NAME";
        //        ddlCreatedBy.DataValueField = "EMP_RECORD_ID";
        //        ddlCreatedBy.DataBind();
        //        ddlCreatedBy.Items.Insert(0, "All");
        //        ddlCreatedBy.SelectedIndex = 0;
        //    }

       if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 4 ||
           Convert.ToInt32(Session["EMP_RECORD_ID"]) == 117 ||
           Convert.ToInt32(Session["EMP_RECORD_ID"]) == 11 ||
           Convert.ToString(Session["USER_TYPE"]) == "A")
                dsEmployee = objTicket.GetEmployeeForTicket(0);
        else
            dsEmployee = objTicket.GetEmployeeForTicket(Convert.ToInt32(Session["EMP_RECORD_ID"]));

        if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
        {
                ddlCreatedBy.DataSource = dsEmployee.Tables[0];
                ddlCreatedBy.DataTextField = "EMPLOYEE_NAME";
                ddlCreatedBy.DataValueField = "EMP_RECORD_ID";
                ddlCreatedBy.DataBind();
                ddlCreatedBy.Items.Insert(0, "Select");
                ddlCreatedBy.SelectedIndex = 0;
                //ddlEmployee.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);

                ddlCreatedBy.DataSource = dsEmployee.Tables[0];
                ddlCreatedBy.DataTextField = "EMPLOYEE_NAME";
                ddlCreatedBy.DataValueField = "EMP_RECORD_ID";
                ddlCreatedBy.DataBind();
                ddlCreatedBy.Items.Insert(0, "Select");
                ddlCreatedBy.SelectedIndex = 0;
        }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindAllocatedTo()
    {
        try
        {
            dsAllocatedTo = objTicket.GetAllocatedToList();
            if (dsAllocatedTo.Tables.Count > 0 && dsAllocatedTo.Tables[0].Rows.Count > 0)
            {
                ddlAllocatedTo.DataSource = dsAllocatedTo.Tables[0];
                ddlAllocatedTo.DataTextField = "EMPLOYEE_NAME";
                ddlAllocatedTo.DataValueField = "EMP_RECORD_ID";
                ddlAllocatedTo.DataBind();
                ddlAllocatedTo.Items.Insert(0, "All");
                ddlAllocatedTo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTicketStatus()
    {
        try
        {
            dsTicketStatus = objTicket.GetTicketStatus();

            if (dsTicketStatus.Tables.Count > 0 &&
                dsTicketStatus.Tables[0].Rows.Count > 0)
            {
                ddlTicketStatus.DataSource = dsTicketStatus.Tables[0];
                ddlTicketStatus.DataTextField = "NAME";
                ddlTicketStatus.DataValueField = "PID";
                ddlTicketStatus.DataBind();
                ddlTicketStatus.Items.Insert(0, "All");
                ddlTicketStatus.SelectedIndex = 0;

            }
            else
            {
                ddlTicketStatus.DataSource = null;
                ddlTicketStatus.Items.Clear();
                ddlTicketStatus.Items.Insert(0, "All");
                ddlTicketStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTicketType()
    {
        try
        {
            dsTicketType = objTicket.GetTicketType();

            if (dsTicketType.Tables.Count > 0 &&
                dsTicketType.Tables[0].Rows.Count > 0)
            {
                ddlTicketType.DataSource = dsTicketType.Tables[0];
                ddlTicketType.DataTextField = "NAME";
                ddlTicketType.DataValueField = "PID";
                ddlTicketType.DataBind();
                ddlTicketType.Items.Insert(0, "Select");
                ddlTicketType.SelectedIndex = 0;

            }
            else
            {
                ddlTicketType.DataSource = null;
                ddlTicketType.Items.Clear();
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

    private void GetTicketSubtype(int ticketTypeId)
    {
        try
        {
            dsTicketSubtype = objTicket.GetTicketSubtype(ticketTypeId);

            if (dsTicketSubtype.Tables.Count > 0 &&
                dsTicketSubtype.Tables[0].Rows.Count > 0)
            {
                ddlTicketSubtype.DataSource = dsTicketSubtype.Tables[0];
                ddlTicketSubtype.DataTextField = "NAME";
                ddlTicketSubtype.DataValueField = "PID";
                ddlTicketSubtype.DataBind();
                ddlTicketSubtype.Items.Insert(0, "Select");
                ddlTicketSubtype.SelectedIndex = 0;
            }
            else
                BindBlankTicketSubtype();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }
    
    private void BindAllocatePriority()
    {

        ddlAllocatePriority.Items.Clear();
        ddlAllocatePriority.Items.Add(new ListItem("Select", ""));
        ddlAllocatePriority.Items.Add(new ListItem("Priority 1", "Priority 1"));
        ddlAllocatePriority.Items.Add(new ListItem("Priority 2", "Priority 2"));
        ddlAllocatePriority.Items.Add(new ListItem("Priority 3", "Priority 3"));

    }


    private void BindBlankTicketSubtype()
    {
        ddlTicketSubtype.DataSource = null;
        ddlTicketSubtype.Items.Clear();
        ddlTicketSubtype.Items.Insert(0, "Select");
        ddlTicketSubtype.SelectedIndex = 0;
    }

    private void BindAllocateTo(int ticketId)
    {
        dsTicketAndAllocateTo = objTicket.GetTicketAndAllocateInfo(ticketId);

        if (dsTicketAndAllocateTo.Tables.Count > 0)
        {
            if (dsTicketAndAllocateTo.Tables[1].Rows.Count > 0)
            {
                ddlAllocateToToU.DataSource = dsTicketAndAllocateTo.Tables[1];
                ddlAllocateToToU.DataTextField = "EMPLOYEE_NAME";
                ddlAllocateToToU.DataValueField = "EMP_RECORD_ID";
                ddlAllocateToToU.DataBind();
                //ddlAllocateToToU.Items.Insert(0, "Select");
                //ddlAllocateToToU.SelectedIndex = 0;
            }
            else
            {
                ddlAllocateToToU.DataSource = null;
                ddlAllocateToToU.Items.Clear();
                ddlAllocateToToU.Items.Insert(0, "Select");
                ddlAllocateToToU.SelectedIndex = 0;
            }

        }
    }

    private void ViewAttachedFilesNew01(int ticketID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            DataTable dtTicketList = (DataTable)Session["dtTicketList"];

            string fileName = Convert.ToString(dtTicketList.Select("PID='" + ticketID + "'"));

            string extn = string.Empty;
            foreach (DataRow dr in dtTicketList.Select("PID='" + ticketID + "'"))
            {
                if (fileType == "ATTACHMENT1")
                {
                    fileName = Convert.ToString(dr["FILE_NAME1"]);
                    bytes = (byte[])dr["DOC_NAME1"];
                }
                else if (fileType == "ATTACHMENT2")
                {
                    fileName = Convert.ToString(dr["FILE_NAME2"]);
                    bytes = (byte[])dr["DOC_NAME2"];
                }
                else if (fileType == "ATTACHMENT3")
                {
                    fileName = Convert.ToString(dr["FILE_NAME3"]);
                    bytes = (byte[])dr["DOC_NAME3"];
                }

                if (fileType == "CLOSINGATTACHMENT1")
                {
                    fileName = Convert.ToString(dr["FILECLOSING_NAME1"]);
                    bytes = (byte[])dr["FILEDOC_NAME1"];
                }
                else if (fileType == "CLOSINGATTACHMENT2")
                {
                    fileName = Convert.ToString(dr["FILECLOSING_NAME2"]);
                    bytes = (byte[])dr["FILEDOC_NAME2"];
                }
                else if (fileType == "CLOSINGATTACHMENT3")
                {
                    fileName = Convert.ToString(dr["FILECLOSING_NAME3"]);
                    bytes = (byte[])dr["FILEDOC_NAME3"];
                }
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
                else if (extn == "xls" || extn == "xlsx")
                {
                    if (bytes != null && bytes.Length > 0)
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.BinaryWrite(bytes);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        ExceptionMessage("Excel file is empty!");
                        return;
                    }
                }
                else if (extn == "doc" || extn == "docx" || extn == "DOC" || extn == "DOCX")
                {
                    if (bytes != null && bytes.Length > 0)
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        // Sets the type to a generic stream so the browser triggers a download
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.BinaryWrite(bytes);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        ExceptionMessage("Word file is empty!");
                        return;
                    }
                }
                else if (extn == "eml" || extn == "msg" || extn == "EML" || extn == "MSG")
                {
                    if (bytes != null && bytes.Length > 0)
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.BinaryWrite(bytes);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        ExceptionMessage("Email file is empty!");
                        return;
                    }
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

    private void GetTicketList()
    {
        try
        {
            if (chkSelectDates.Checked)
            {
                FromDateS = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
                ToDateS = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            }

            insuranceNo = Convert.ToString(txtInsuranceNo.Text);

            //if (!string.IsNullOrEmpty(insuranceNo) && insuranceNo != "")
            //{
            //    insuranceNo = Convert.ToString(txtInsuranceNo.Text);
            //}
            //else
            //{
            //    insuranceNo = ""; 
            //}

            if (ddlCreatedBy.SelectedIndex > 0)
            {
                teamMemberID = Convert.ToInt32(ddlCreatedBy.SelectedValue);
            }
            else
            {
                teamMemberID = 0;

                if (Session["TEAMMEMBERS"] != null)
                {
                    dtTeamMembers = (DataTable)Session["TEAMMEMBERS"];
                    foreach (DataRow dr in dtTeamMembers.Rows)
                    {
                        teamMembers += "," + Convert.ToString(dr["EMP_RECORD_ID"]);
                    }
                    teamMembers = Convert.ToString(Session["EMP_RECORD_ID"]) + "," + teamMembers.TrimStart(',');
                }
                else
                {
                    teamMembers = Convert.ToString(Session["EMP_RECORD_ID"]);
                }
            }

            CreatedByIdS = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            int creat = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsTicketList = objTicket.GetTicketList(DateTypeS
                                                 , FromDateS
                                                 , ToDateS
                                                 , TicketNoS
                                                 , TicketStatusIdS
                                                 , TicketTypeIdS
                                                 , TicketSubtypeIdS
                                                 , insuranceNo
                                                 , creat
                                                 , AllocatedToIdS
                                                 ,teamMemberID
                                                 ,teamMembers);

            if (dsTicketList.Tables.Count > 0 && dsTicketList.Tables[0].Rows.Count > 0)
            {
                Session["dtTicketList"] = dsTicketList.Tables[0];
                if (dsTicketList.Tables[1].Rows.Count > 0)
                    Session["dtRecpList"] = dsTicketList.Tables[1];

                gvTicketList.DataSource = dsTicketList.Tables[0];
                gvTicketList.DataBind();
                lblRecords.Text = "Records[" + dsTicketList.Tables[0].Rows.Count + "]";
            }
            else
            {
                Session["dtTicketList"] = null;
                Session["dtRecpList"] = null;
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

    private int UpdateTicket()
    {
        try
        {
            if (Convert.ToInt32(ViewState["TICKET_ID"]) != 0)
                TicketIdU = Convert.ToInt32(ViewState["TICKET_ID"]);
            else
                TicketIdU = 0;

            if (Convert.ToInt32(ViewState["TICKET_TYPE_ID"]) != 0)
                TicketTypeIdU = Convert.ToInt32(ViewState["TICKET_TYPE_ID"]);
            else
                TicketTypeIdU = 0;

            if (Convert.ToInt32(ViewState["TICKET_SUBTYPE_ID"]) != 0)
                TicketSubtypeIdU = Convert.ToInt32(ViewState["TICKET_SUBTYPE_ID"]);
            else
                TicketSubtypeIdU = 0;

            Byte[] attachmentOneBytes = null;
            if (fileUploadAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment1.PostedFile.FileName))
                {
                    AttachmentOneFileNameU = fileUploadAttachment1.PostedFile.FileName;
                    attachmentOneBytes = GetFileBytes(fileUploadAttachment1.PostedFile.FileName
                                                    , fileUploadAttachment1.PostedFile.InputStream);
                }
                else
                {
                    AttachmentOneFileNameU = string.Empty;
                    attachmentOneBytes = null;
                }
            }
            else
            {
                AttachmentOneFileNameU = string.Empty;
                attachmentOneBytes = null;
            }
            ////////////////////////////////////////////////
            Byte[] attachmentTwoBytes = null;
            if(fileUploadAttachment2.HasFile)
            {
                if(!string.IsNullOrEmpty(fileUploadAttachment2.PostedFile.FileName))
                {
                    AttachmentTwoFileNameU = fileUploadAttachment2.PostedFile.FileName;
                    attachmentTwoBytes = GetFileBytes(fileUploadAttachment2.PostedFile.FileName
                                                    , fileUploadAttachment2.PostedFile.InputStream);
                }
                else
                {
                    AttachmentTwoFileNameU = string.Empty;
                    attachmentTwoBytes = null;
                }
            }
            else
            {
                AttachmentTwoFileNameU = string.Empty;
                attachmentTwoBytes = null;
            }

            Byte[] attachmentThreeBytes = null;
            if (fileUploadAttachment3.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment3.PostedFile.FileName))
                {
                    AttachmentThreeFileNameU = fileUploadAttachment3.PostedFile.FileName;
                    attachmentThreeBytes = GetFileBytes(fileUploadAttachment3.PostedFile.FileName
                                                    , fileUploadAttachment3.PostedFile.InputStream);
                }
                else
                {
                    AttachmentThreeFileNameU = string.Empty;
                    attachmentThreeBytes = null;
                }
            }
            else
            {
                AttachmentThreeFileNameU = string.Empty;
                attachmentThreeBytes = null;
            }



            //////////////////////////////////////////////////
            int insertedId = 0;
            bool sentMailValue = false;
            string ticketNumber = string.Empty;

            string retValue = objTicket.CreateAndUpdateTicket
                    (TicketIdU
                    , TicketTypeIdU
                    , TicketSubtypeIdU
                    , AttachmentOneFileNameU
                    , attachmentOneBytes
                    , AttachmentTwoFileNameU
                    , attachmentTwoBytes
                    , AttachmentThreeFileNameU
                    , attachmentThreeBytes
                    , TicketDescriptionU
                    , RemarksU
                    , CurrentUserIdU);

            if (!string.IsNullOrEmpty(retValue))
            {
                insertedId = Convert.ToInt32(retValue.Split(':')[0]);
                ticketNumber = Convert.ToString(retValue.Split(':')[1]);

                if (insertedId == 0) return 0;

                HRTicketSendMail objSendMail = new HRTicketSendMail(insertedId);
                sentMailValue = objSendMail.InitializeSendMail();

                if (sentMailValue)
                    SuccessMessage("Ticket No.: " + ticketNumber + " updated and mail sent successfully.");
                else
                    SuccessMessage("Ticket No.: " + ticketNumber + " updated successfully.");

                Reset();

                return 1;
            }
            else
            {
                ExceptionMessage("Please try again");
                return 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
        }
    }

    private void Reset()
    {
        ModalPopupExtender1.Show();

        txtTicketDescriptionToU.Text = string.Empty;
        txtTicketRemarksToU.Text = string.Empty;
        txtStatusToU.Text = string.Empty;
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
                case ".doc":
                    GSTContentType = "application/msword";
                    break;
                case ".docx":
                    GSTContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".DOC":
                    GSTContentType = "application/msword";
                    break;
                case ".DOCX":
                    GSTContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
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
                case ".xls":
                    GSTContentType = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    GSTContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".eml":
                    GSTContentType = "message/rfc822";
                    break;
                case ".msg":
                    GSTContentType = "application/vnd.ms-outlook";
                    break;
                case ".EML":
                    GSTContentType = "message/rfc822";
                    break;
                case ".MSG":
                    GSTContentType = "application/vnd.ms-outlook";
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

    private void UpdateTicketStatus(int isAddStatus, int isClose)
    {
        try
        {
            string messageText = "";
            int allocatedToId = 0;

            if (Convert.ToInt32(ViewState["TICKET_ID"]) != 0)
                TicketIdU = Convert.ToInt32(ViewState["TICKET_ID"]);
            else TicketIdU = 0;

            //if (!string.IsNullOrEmpty(Convert.ToString(ViewState["TICKET_NUMBER"])))
            //    TicketNumberU = Convert.ToString(ViewState["TICKET_NUMBER"]);
            //else TicketNumberU = "";

            string statusText = "";

            if (!string.IsNullOrEmpty(txtStatusToU.Text))
            {
                statusText = txtStatusToU.Text;
            }


            if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)HRTicketEnums.EnumActions.Allocate)
            {
                TicketStatusId = (int)HRTicketEnums.EnumStatus.Allocated;
                messageText = "Allocated";


                if(Convert.ToInt32(ViewState["ALLOCATED_TO_ID"]) > 0) {
                    allocatedToId = Convert.ToInt32(ViewState["ALLOCATED_TO_ID"]);
                }

            }
            //if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)HRTicketEnums.EnumActions.ReAllocate)
            //{
            //    TicketStatusId = (int)HRTicketEnums.EnumStatus.Allocated;
            //    messageText = "ReAllocated";
            //}
            else if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)HRTicketEnums.EnumActions.Cancel)
            {
                TicketStatusId = (int)HRTicketEnums.EnumStatus.Cancelled;
                messageText = "Cancelled";
            }
            else if (Convert.ToInt32(ViewState["ACT_ID"]) == (int)HRTicketEnums.EnumActions.Close)
            {
                TicketStatusId = (int)HRTicketEnums.EnumStatus.Closed;
                messageText = "Closed";
            }

            if (isAddStatus > 0 && isClose > 0 && TicketStatusId != 2)
                messageText = "Closed and Status saved";
            else if (TicketStatusId == 2)
            {
                messageText = "Allocated";
            }
            else if (isAddStatus > 0 && TicketStatusId != 2)
                messageText = "Status saved";
            else if (isClose > 0 && TicketStatusId != 2)
                messageText = "Closed";

            bool sentMailValue = false;


            Byte[] closingAttachmentOneBytes = null;
            Byte[] closingAttachmentTwoBytes = null;
            Byte[] closingAttachmentThreeBytes = null;

            if (closingfileUpload1.HasFile)
            {
                if (!string.IsNullOrEmpty(closingfileUpload1.PostedFile.FileName))
                {
                    closingAttachmentOneFileName = closingfileUpload1.PostedFile.FileName;
                    closingAttachmentOneBytes = GetFileBytes(closingfileUpload1.PostedFile.FileName, closingfileUpload1.PostedFile.InputStream);
                }
                else
                {
                    closingAttachmentOneFileName = string.Empty;
                    closingAttachmentOneBytes = null;
                }
            }
            else
            {
                closingAttachmentOneFileName = string.Empty;
                closingAttachmentOneBytes = null;
            }

            if (closingfileUpload2.HasFile)
            {
                if (!string.IsNullOrEmpty(closingfileUpload2.PostedFile.FileName))
                {
                    closingAttachmentTwoFileName = closingfileUpload2.PostedFile.FileName;
                    closingAttachmentTwoBytes = GetFileBytes(closingfileUpload2.PostedFile.FileName, closingfileUpload2.PostedFile.InputStream);
                }
                else
                {
                    closingAttachmentTwoFileName = string.Empty;
                    closingAttachmentTwoBytes = null;
                }
            }
            else
            {
                closingAttachmentTwoFileName = string.Empty;
                closingAttachmentTwoBytes = null;
            }

            if (closingfileUpload3.HasFile)
            {
                if (!string.IsNullOrEmpty(closingfileUpload3.PostedFile.FileName))
                {
                    closingAttachmentThreeFileName = closingfileUpload3.PostedFile.FileName;
                    closingAttachmentThreeBytes = GetFileBytes(closingfileUpload3.PostedFile.FileName, closingfileUpload3.PostedFile.InputStream);
                }
                else
                {
                    closingAttachmentThreeFileName = string.Empty;
                    closingAttachmentThreeBytes = null;
                }
            }
            else
            {
                closingAttachmentThreeFileName = string.Empty;
                closingAttachmentThreeBytes = null;
            }


            if (TicketIdU > 0)
            {
                int value = objTicket.UpdateTicketStatus(TicketIdU, TicketStatusId, RemarksU,
                                                         AllocateToIdU, allocatedToId,AllocatedToPriority
                                                          , closingAttachmentOneFileName
                                                          , closingAttachmentOneBytes
                                                          , closingAttachmentTwoFileName
                                                          , closingAttachmentTwoBytes
                                                          , closingAttachmentThreeFileName
                                                          , closingAttachmentThreeBytes
                                                       , statusText, isAddStatus, isClose
                                                       , CurrentUserIdU);
                if (value > 0)
                {
                    HRTicketSendMail objSendMail = new HRTicketSendMail(TicketIdU);
                    
                    if (isAddStatus > 0 && isClose > 0)
                    {
                        sentMailValue = objSendMail.InitializeSendMail();
                    }
                    else if (isClose > 0)
                    {
                        sentMailValue = objSendMail.InitializeSendMail();
                    }

                    if (sentMailValue)
                        SuccessMessage("Ticket No.: " + TicketNumberU + " " + messageText + " and mail sent successfully.");
                    else
                        SuccessMessage("Ticket No.: " + TicketNumberU + " " + messageText + " successfully.");

                    Reset();
                }
            }

            GetTicketList();
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