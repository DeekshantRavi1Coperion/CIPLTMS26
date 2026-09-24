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
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net.Mime;

public partial class HR_TICKET_UpdateTicketStatus : System.Web.UI.Page
{

    #region VARIABLES[=============]

    DataSet dsTicketAndAllocateInfo = new DataSet();
    BAL.HR.Ticket objTicket = new BAL.HR.Ticket();
    BAL.Common objCommon = new BAL.Common();

    int _ticketId;
    string _ticketNumber;
    string _ticketType;
    string _ticketSubType;
    int _allocateToId;
    int _statusId;
    string _remarks;
    private bool sentMailValue;
    private string _allocatedToPriority = string.Empty;

    public System.Int32 TicketId
    {
        get
        {
            return _ticketId;
        }

        set
        {
            _ticketId = value;
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


    public System.String TicketNumber
    {
        get
        {
            return _ticketNumber;
        }

        set
        {
            _ticketNumber = value;
        }
    }

    public System.String TicketType
    {
        get
        {
            return _ticketType;
        }

        set
        {
            _ticketType = value;
        }
    }

    public System.String TicketSubType
    {
        get
        {
            return _ticketSubType;
        }

        set
        {
            _ticketSubType = value;
        }
    }

    public System.Int32 AllocateToId
    {
        get
        {
            if (ddlAllocateTo.SelectedIndex > 0)
                _allocateToId = Convert.ToInt32(ddlAllocateTo.SelectedValue);
            else _allocateToId = 0;

            return _allocateToId;
        }

        set
        {
            _allocateToId = value;
        }
    }

    public System.Int32 StatusId
    {
        get
        {
            return _statusId;
        }

        set
        {
            _statusId = value;
        }
    }

    public System.String Remarks
    {
        get
        {
            if (!string.IsNullOrEmpty(txtTicketRemarks.Text))
                _remarks = txtTicketRemarks.Text;
            else _remarks = "";

            return _remarks;
        }

        set
        {
            _remarks = value;
        }
    }




    #endregion


    #region EVENTS[================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidateAndLogin();
        }
    }

    protected void btnAllocate_Click(object sender, EventArgs e)
    {
        AllocateAndCancelTicket((int)HRTicketEnums.EnumStatus.Allocated);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        AllocateAndCancelTicket((int)HRTicketEnums.EnumStatus.Cancelled);
    }



    protected void btnTicketList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HR/TICKET/TicketList.aspx");
    }

    #endregion


    #region METHODS[===============]

    private void BindAllocatePriority()
    {

        ddlAllocatePriority.Items.Clear();
        ddlAllocatePriority.Items.Add(new ListItem("Select", ""));
        ddlAllocatePriority.Items.Add(new ListItem("Priority 1", "Priority 1"));
        ddlAllocatePriority.Items.Add(new ListItem("Priority 2", "Priority 2"));
        ddlAllocatePriority.Items.Add(new ListItem("Priority 3", "Priority 3"));

    }

    private void ValidateAndLogin()
    {
        try
        {
            DataSet ds = new DataSet();
            string userName = string.Empty;
            string password = string.Empty;
            int ticketId = Convert.ToInt32(Request.QueryString["ticketId"]);
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

                BindTicketDetails(ticketId);
                BindAllocatePriority();

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

    private void BindTicketDetails(int ticketId)
    {
        dsTicketAndAllocateInfo = objTicket.GetTicketAndAllocateInfo(ticketId);

        if (dsTicketAndAllocateInfo.Tables.Count > 0 && dsTicketAndAllocateInfo.Tables[0].Rows.Count > 0)
        {



            DataRow dr0 = dsTicketAndAllocateInfo.Tables[0].Rows[0];

            if (dr0["ALLOCATED_TO"] != DBNull.Value && Convert.ToInt32(dr0["ALLOCATED_TO"]) > 0)
            {
                btnAllocate.Visible = false;
                btnCancel.Visible = false;
            }

            txtTicketNumber.Text = Convert.ToString(dr0["TICKET_NUMBER"]);
            txtTicketType.Text = Convert.ToString(dr0["TICKET_TYPE"]);
            txtTicketSubtype.Text = Convert.ToString(dr0["TICKET_SUBTYPE"]);

            if (dsTicketAndAllocateInfo.Tables[1].Rows.Count > 0)
            {
                ddlAllocateTo.DataSource = dsTicketAndAllocateInfo.Tables[1];
                ddlAllocateTo.DataTextField = "EMPLOYEE_NAME";
                ddlAllocateTo.DataValueField = "EMP_RECORD_ID";
                ddlAllocateTo.DataBind();
                ddlAllocateTo.Items.Insert(0, "Select");
                ddlAllocateTo.SelectedIndex = 0;
            }
            else
            {
                ddlAllocateTo.DataSource = null;
                ddlAllocateTo.Items.Clear();
                ddlAllocateTo.Items.Insert(0, "Select");
                ddlAllocateTo.SelectedIndex = 0;
            }

        }
    }

    private void AllocateAndCancelTicket(int statusId)
    {
        try
        {
            string messageTxt = "";
            int ticketId = Convert.ToInt32(Request.QueryString["ticketId"]);

            if (statusId == (int)HRTicketEnums.EnumStatus.Allocated)
            {
                messageTxt = "Allocated";
            }
            else if (statusId == (int)HRTicketEnums.EnumStatus.Cancelled)
            {
                messageTxt = "Cancelled";
            }
            int currentUserId = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            int val = objTicket.UpdateTicketStatus(ticketId, statusId, Remarks, AllocateToId, 0, AllocatedToPriority,"",null,"",null,"",null, "", 0, 1, currentUserId);

            if (val > 0)
            {
                HRTicketSendMail objSendMail = new HRTicketSendMail(ticketId);
                sentMailValue = objSendMail.InitializeSendMail();

                if (sentMailValue)
                    SuccessMessage("Ticket No.: " + txtTicketNumber.Text + " " + messageTxt + " and mail sent successfully.");
                else
                    SuccessMessage("Ticket No.: " + txtTicketNumber.Text + " " + messageTxt + " successfully.");

                Reset();

                ValidateAndLogin();
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void Reset()
    {
        txtTicketRemarks.Text = string.Empty;
        ddlAllocateTo.SelectedIndex = 0;
        btnAllocate.Visible = false;
        btnCancel.Visible = false;
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