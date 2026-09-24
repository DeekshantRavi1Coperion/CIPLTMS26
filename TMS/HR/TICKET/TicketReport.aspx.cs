using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class HR_TICKET_TicketReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    DataSet dsTicketAndAllocateTo = new DataSet();
    DataSet dsTicketStatus = new DataSet();
    DataSet dsTicketType = new DataSet();
    DataSet dsTicketSubtype = new DataSet();

    DataSet dsTicketTypeU = new DataSet();
    DataSet dsTicketSubtypeU = new DataSet();

    BAL.HR.Ticket objTicket = new BAL.HR.Ticket();

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsCreatedBy = new DataSet();
    DataSet dsAllocatedTo = new DataSet();
    DataSet dsTicketReport = new DataSet();

    DataTable dtTeamMembers = new DataTable();
    DataSet dsEmployee = new DataSet();


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

    // U for update
    private int _ticketIdU;
    private string _ticketNumberU;
    private int _currentUserIdU;
    private int _ticketTypeIdU = 0;
    private int _ticketSubtypeIdU = 0;
    private int _allocateToIdU = 0;
    private string _attachmentOneFileNameU = string.Empty;
    private string _ticketDescriptionU = string.Empty;
    private string _remarksU = string.Empty;

    private int _ticketStatusId;
    string teamMembers = string.Empty;


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

                GetTicketReport();
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
        GetTicketReport();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gvTicketList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtTicketList"];
            ToCSVNew01(dt);
        }
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
                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT" ||
                    Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                //Label lblTicketStatus = gvTicketList.Rows[rowindex].FindControl("lblTicketStatus") as Label;
                Label lblTicketID = gvTicketList.Rows[rowindex].FindControl("lblTicketID") as Label;
                //Label lblTicketNO = gvTicketList.Rows[rowindex].FindControl("lblTicketNO") as Label;
                //Label lblTicketStatusID = gvTicketList.Rows[rowindex].FindControl("lblTicketStatusID") as Label;

                //Label lblTicketTypeID = gvTicketList.Rows[rowindex].FindControl("lblTicketTypeID") as Label;
                //Label lblTicketType = gvTicketList.Rows[rowindex].FindControl("lblTicketType") as Label;

                //Label lblTicketSubtypeID = gvTicketList.Rows[rowindex].FindControl("lblTicketSubtypeID") as Label;
                //Label lblTicketSubtype = gvTicketList.Rows[rowindex].FindControl("lblTicketSubtype") as Label;

                //Label lblDescription = gvTicketList.Rows[rowindex].FindControl("lblDescription") as Label;
                //Label lblAllocatedToID = gvTicketList.Rows[rowindex].FindControl("lblAllocatedToID") as Label;

                //Label lblCreatedByID = gvTicketList.Rows[rowindex].FindControl("lblCreatedByID") as Label;
                //Label lblRemarks = gvTicketList.Rows[rowindex].FindControl("lblRemarks") as Label;

                //Label lblAllocatedByID = gvTicketList.Rows[rowindex].FindControl("lblAllocatedByID") as Label;
                //Label lblAllocatedRemarks = gvTicketList.Rows[rowindex].FindControl("lblAllocatedRemarks") as Label;

                //Label lblClosedByID = gvTicketList.Rows[rowindex].FindControl("lblClosedByID") as Label;
                //Label lblClosedRemarks = gvTicketList.Rows[rowindex].FindControl("lblClosedRemarks") as Label;

                //Label lblCancelledByID = gvTicketList.Rows[rowindex].FindControl("lblCancelledByID") as Label;
                //Label lblCancelledRemarks = gvTicketList.Rows[rowindex].FindControl("lblCancelledRemarks") as Label;


                //Label lblFileOneName = gvTicketList.Rows[rowindex].FindControl("lblFileOneName") as Label;


                ViewState["TICKET_ID"] = Convert.ToInt32(lblTicketID.Text);
                //ViewState["TICKET_TYPE_ID"] = Convert.ToInt32(lblTicketTypeID.Text);
                //ViewState["TICKET_SUBTYPE_ID"] = Convert.ToInt32(lblTicketSubtypeID.Text);
                //ViewState["TICKET_NUMBER"] = Convert.ToString(lblTicketNO.Text);
                //ViewState["CREATED_BY_ID"] = Convert.ToInt32(lblCreatedByID.Text);

                if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    mpeViewInPDF.Show();
                    iframeViewDrawingDetailsInPDF.Attributes.Add("src", "TicketDetailInPDF.aspx?recordID=" + Convert.ToInt32(lblTicketID.Text) + "");
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblTicketID.Text), "ATTACHMENT");
                }
                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblTicketID.Text), "ATTACHMENT2");
                }
                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblTicketID.Text), "ATTACHMENT3");
                }
                else if (Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblTicketID.Text), "CLOSINGATTACHMENT");
                }
                else if (Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT2")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblTicketID.Text), "CLOSINGATTACHMENT2");
                }
                else if (Convert.ToString(e.CommandArgument) == "ViewCLOSINGATTACHMENT3")
                {
                    ViewAttachedFilesNew01(Convert.ToInt32(lblTicketID.Text), "CLOSINGATTACHMENT3");
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
                int rowindex = e.Row.RowIndex;
                ImageButton btnFileOneName = (ImageButton)e.Row.FindControl("btnFileOneName");
                ImageButton btnFileTwoName = (ImageButton)e.Row.FindControl("btnFileTwoName");
                ImageButton btnFileThreeName = (ImageButton)e.Row.FindControl("btnFileThreeName");

                ImageButton btnClosingFileOneName = (ImageButton)e.Row.FindControl("btnClosingFileName");
                ImageButton btnClosingFileTwoName = (ImageButton)e.Row.FindControl("btnClosingFileTwoName");
                ImageButton btnClosingFileThreeName = (ImageButton)e.Row.FindControl("btnClosingFileThreeName");
                btnFileOneName.Visible = false;
                btnFileTwoName.Visible = false;
                btnFileThreeName.Visible = false;
                btnClosingFileOneName.Visible = false;
                btnClosingFileTwoName.Visible = false;
                btnClosingFileThreeName.Visible = false;

                Label lblTicketStatusID = (Label)e.Row.FindControl("lblTicketStatusID");
                Label lblFileOneName = (Label)e.Row.FindControl("lblFileOneName");
                Label lblFileTwoName = (Label)e.Row.FindControl("lblFileTwoName") as Label;
                Label lblFileThreeName = (Label)e.Row.FindControl("lblFileThreeName") as Label;

                Label lblClosingFileOneName = (Label)e.Row.FindControl("lblClosingFileOneName") as Label;
                Label lblClosingFileTwoName = (Label)e.Row.FindControl("lblClosingFileTwoName") as Label;
                Label lblClosingFileThreeName = (Label)e.Row.FindControl("lblClosingFileThreeName") as Label;

                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                TextBox txtFormatedStatus = (TextBox)e.Row.FindControl("txtFormatedStatus");



                //string formatedTxt = string.Empty;
                //if (!string.IsNullOrEmpty(lblStatus.Text))
                //{
                //    string[] txt = lblStatus.Text.Split('$');
                //    foreach (var item in txt)
                //    {
                //        formatedTxt += item + "\n";
                //    }
                //}

                //txtFormatedStatus.Text = formatedTxt;


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
                        FileOneNameExtn == "jepg" ||
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


                if (Convert.ToInt32(lblTicketStatusID.Text) == (int)HRTicketEnums.EnumStatus.Open)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                    }
                }

                else if (Convert.ToInt32(lblTicketStatusID.Text) == (int)HRTicketEnums.EnumStatus.Allocated)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightYellow;
                    }
                }

                else if (Convert.ToInt32(lblTicketStatusID.Text) == (int)HRTicketEnums.EnumStatus.Closed)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }

                else if (Convert.ToInt32(lblTicketStatusID.Text) == (int)HRTicketEnums.EnumStatus.Cancelled)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindCreatedBy()
    {
        //try
        //{
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
        //}
        //catch (Exception ex)
        //{
        //    ExceptionMessage(ex.ToString());
        //    return;
        //}

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

    private void BindBlankTicketSubtype()
    {
        ddlTicketSubtype.DataSource = null;
        ddlTicketSubtype.Items.Clear();
        ddlTicketSubtype.Items.Insert(0, "Select");
        ddlTicketSubtype.SelectedIndex = 0;
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


                if (fileType == "CLOSINGATTACHMENT")
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

    private void GetTicketReport()
    {
        try
        {
            if (chkSelectDates.Checked)
            {
                FromDateS = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
                ToDateS = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            }

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


            dsTicketReport = objTicket.GetTicketReport(DateTypeS
                                                 , FromDateS
                                                 , ToDateS
                                                 , TicketNoS
                                                 , TicketStatusIdS
                                                 , TicketTypeIdS
                                                 , TicketSubtypeIdS
                                                 , creat
                                                 , AllocatedToIdS
                                                 , teamMemberID
                                                 , teamMembers);

            if (dsTicketReport.Tables.Count > 0 && dsTicketReport.Tables[0].Rows.Count > 0)
            {
                Session["dtTicketList"] = dsTicketReport.Tables[0];
                gvTicketList.DataSource = dsTicketReport.Tables[0];
                gvTicketList.DataBind();
                lblRecords.Text = "Records[" + dsTicketReport.Tables[0].Rows.Count + "]";
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

    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 0; i < dt.Columns.Count - 8; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count - 8; j++)
                {
                    csv += Convert.ToString(dt.Rows[i][j]).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "HR_Ticket_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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