using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using ClosedXML.Excel;
using System.Text;
using Ionic.Zip;

public partial class MR_WORKFLOW_PostedBOMList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();
    BAL.PROJECT_MANAGEMENT.ProjectManagement objProjM = new BAL.PROJECT_MANAGEMENT.ProjectManagement();

    DataSet _dsStatus = new DataSet();
    DataSet _dsUnit = new DataSet();
    DataSet _dsBomType = new DataSet();
    DataSet _dsBomMrCreatedBy = new DataSet();
    DataSet _dsBomResponsibleFor = new DataSet();
    DataSet _dsBomPivotGroups = new DataSet();

    DataSet _dsPostedBomList = new DataSet();
    DataSet _dsBomList = new DataSet();

    DataSet _dsBomLineToU = new DataSet();
    DataTable _dtBomLineSelectedToU = new DataTable();

    DataSet _dsPostedBomLineToU = new DataSet();

    int _currentUserId = 0;
    DateTime _currentDate;


    string _dateType = "";
    string _startDate = "";
    string _endDate = "";

    string _mrNo = "";
    int _mrCreatedById = 0;
    string _bomNo = "";
    string _jobNo = "";

    int _unitId = 0;
    int _typeId = 0;
    string _statusId = "0";
    int _pivotGroupId = 0;
    int _responsibleForId = 0;
    int _isTcRequired = 0;



    private int _pidToU;
    private int _unitFidToU;
    private int _typeFidToU;
    private string _mrNoToU;
    private string _mrDateToU;
    private int _statusFidToU;
    private string _bomNoToU;
    private string _bomDateToU;
    private string _jobNoToU;
    private string _deliveryRequiredByToU;
    private string _acceptableVendor1ToU;
    private string _acceptableVendor2ToU;
    private string _acceptableVendor3ToU;
    private string _acceptableVendor4ToU;
    private string _acceptableVendor5ToU;
    private string _revisionNumberToU;
    private double _budgetedCostToU;
    private double _estimatedCostToU;
    private string _costRelatedRemarksToU;
    private int _pivotGroupFidToU;
    private int _responsibleForBomFidToU;
    private int _isTcRequiredToU;
    private int _createdByFidToU;
    private string _createdRemarksToU;
    private int _approvedByFidToU;
    private DateTime _approvedOnToU;
    private string _approvedRemarksToU;
    private int _amendmentCountToU;
    private int _amendmentByFidToU;
    private DateTime _amendmentOnToU;
    private string _amendmentRemarksToU;
    private int _amendedByFidToU;
    private DateTime _amendedOnToU;
    private string _amendedRemarksToU;
    private int _amendedApprovedByFidToU;
    private DateTime _amendedApprovedOnToU;
    private string _amendedApprovedRemrksToU;
    private int _isSentForApprovalToU;
    private int _isApprovalMailSentToU;
    private int _isApprovedMailSentToU;
    private int _isAmendmentMailSentToU;
    private int _isAmendedMailSentToU;
    private int _isAmendedApprovedMailSentToU;
    private string _remarksToU;
    string _startDateToB = "";
    string _endDateToB = "";
    string _bomNoToB = "";
    string _jobNoToB = "";

    #endregion


    #region PROPERTIES[====================]

    public int CurrentUserId
    {
        get
        {
            _currentUserId = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            return _currentUserId;
        }
    }

    public DateTime CurrentDate
    {
        get
        {
            _currentDate = DateTime.Now;

            return _currentDate;
        }
    }

    public int UnitIdToS
    {
        get
        {
            if (ddlUnit.SelectedIndex > 0)
                _unitId = Convert.ToInt32(ddlUnit.SelectedValue);
            else _unitId = 0;

            return _unitId;
        }

        set
        {
            _unitId = value;
        }
    }

    public string StartDateToS
    {
        get
        {
            if (chkSelectDates.Checked)
                _startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else _startDate = "";

            return _startDate;
        }

        set
        {
            _startDate = value;
        }
    }

    public string EndDateToS
    {
        get
        {
            if (chkSelectDates.Checked)
                _endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else _endDate = "";

            return _endDate;
        }

        set
        {
            _endDate = value;
        }
    }

    public string DateTypeToS
    {
        get
        {
            _dateType = ddlDateType.SelectedValue.ToString();
            return _dateType;
        }

        set
        {
            _dateType = value;
        }
    }


    public int TypeIdToS
    {
        get
        {
            if (ddlType.SelectedIndex > 0)
                _typeId = Convert.ToInt32(ddlType.SelectedValue);
            else _typeId = 0;

            return _typeId;
        }

        set
        {
            _typeId = value;
        }
    }

    public string StatusIdToS
    {
        get
        {
            if (ddlStatus.SelectedIndex > 0)
                _statusId = Convert.ToString(ddlStatus.SelectedValue);
            else _statusId = "0";

            return _statusId;
        }

        set
        {
            _statusId = value;
        }
    }

    public int PivotGroupIdToS
    {
        get
        {
            if (ddlPivotGroup.SelectedIndex > 0)
                _pivotGroupId = Convert.ToInt32(ddlPivotGroup.SelectedValue);
            else _pivotGroupId = 0;

            return _pivotGroupId;
        }

        set
        {
            _pivotGroupId = value;
        }
    }

    public int ResponsibleForIdToS
    {
        get
        {
            if (ddlResponsibleFor.SelectedIndex > 0)
                _responsibleForId = Convert.ToInt32(ddlResponsibleFor.SelectedValue);
            else _responsibleForId = 0;

            return _responsibleForId;
        }

        set
        {
            _responsibleForId = value;
        }
    }

    public int IsTcRequiredToS
    {
        get
        {
            if (ddlIsTCRequired.SelectedIndex > 0)
                _isTcRequired = Convert.ToInt32(ddlIsTCRequired.SelectedValue);
            else _isTcRequired = 0;

            return _isTcRequired;
        }

        set
        {
            _isTcRequired = value;
        }
    }

    public string MrNoToS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtMrNo.Text))
                _mrNo = Convert.ToString(txtMrNo.Text);
            else _mrNo = "";

            return _mrNo;
        }

        set
        {
            _mrNo = value;
        }
    }

    public int MrCreatedByIdToS
    {
        get
        {
            if (ddlMrCreatedBy.SelectedIndex > 0)
                _mrCreatedById = Convert.ToInt32(ddlMrCreatedBy.SelectedValue);
            else _mrCreatedById = 0;

            return _mrCreatedById;
        }

        set
        {
            _mrCreatedById = value;
        }
    }

    public string BomNoToS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtBOMNo.Text))
                _bomNo = Convert.ToString(txtBOMNo.Text);
            else _bomNo = "";

            return _bomNo;
        }

        set
        {
            _bomNo = value;
        }
    }

    public string JobNoToS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
                _jobNo = Convert.ToString(txtJobNo.Text);
            else _jobNo = "";

            return _jobNo;
        }

        set
        {
            _jobNo = value;
        }
    }






    public int PidToU
    {
        get
        {
            if (ViewState["PID"] != null)
                _pidToU = Convert.ToInt32(ViewState["PID"]);
            else _pidToU = 0;

            return _pidToU;
        }

        set
        {
            _pidToU = value;
        }
    }

    public int UnitFidToU
    {
        get
        {
            _unitFidToU = Convert.ToInt32(ddlUnitToU.SelectedValue);
            return _unitFidToU;
        }

        set
        {
            _unitFidToU = value;
        }
    }

    public int TypeFidToU
    {
        get
        {
            if (ddlTypeToU.SelectedIndex > 0)
                _typeFidToU = Convert.ToInt32(ddlTypeToU.SelectedValue);
            else _typeFidToU = 0;

            return _typeFidToU;
        }

        set
        {
            _typeFidToU = value;
        }
    }

    public string MrNoToU
    {
        get
        {
            if (ViewState["MRNNO"] != null)
                _mrNoToU = Convert.ToString(ViewState["MRNNO"]);
            else _mrNoToU = "";

            return _mrNoToU;
        }

        set
        {
            _mrNoToU = value;
        }
    }

    public string MrDateToU
    {
        get
        {
            if (!string.IsNullOrEmpty(hdMrDateToU.Value))
                _mrDateToU = Convert.ToDateTime(hdMrDateToU.Value).ToString("yyyy-MM-dd");
            else _mrDateToU = "";

            return _mrDateToU;
        }

        set
        {
            _mrDateToU = value;
        }
    }

    public int StatusFidToU
    {
        get
        {
            if (ViewState["StatusId"] != null)
                _statusFidToU = Convert.ToInt32(ViewState["StatusId"]);
            else _statusFidToU = 0;
            return _statusFidToU;
        }

        set
        {
            _statusFidToU = value;
        }
    }

    public string BomNoToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtBomNoToU.Text))
                _bomNoToU = Convert.ToString(txtBomNoToU.Text);
            else _bomNoToU = "";

            return _bomNoToU;
        }

        set
        {
            _bomNoToU = value;
        }
    }

    public string BomDateToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtBomDateToU.Text))
                _bomDateToU = Convert.ToDateTime(txtBomDateToU.Text).ToString("yyyy-MM-dd");
            else _bomDateToU = "";

            return _bomDateToU;
        }

        set
        {
            _bomDateToU = value;
        }
    }

    public string JobNoToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtJobNoToU.Text))
                _jobNoToU = Convert.ToString(txtJobNoToU.Text);
            else _jobNoToU = "";

            return _jobNoToU;
        }

        set
        {
            _jobNoToU = value;
        }
    }

    public string DeliveryRequiredByToU
    {
        get
        {
            if (!string.IsNullOrEmpty(hdDeliveryRequiredByToU.Value))
                _deliveryRequiredByToU = Convert.ToDateTime(hdDeliveryRequiredByToU.Value).ToString("yyyy-MM-dd");
            else _deliveryRequiredByToU = "";

            return _deliveryRequiredByToU;
        }

        set
        {
            _deliveryRequiredByToU = value;
        }
    }

    public string AcceptableVendor1ToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtAcceptableVendor1ToU.Text))
                _acceptableVendor1ToU = Convert.ToString(txtAcceptableVendor1ToU.Text);
            else _acceptableVendor1ToU = "";

            return _acceptableVendor1ToU;
        }

        set
        {
            _acceptableVendor1ToU = value;
        }
    }

    public string AcceptableVendor2ToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtAcceptableVendor2ToU.Text))
                _acceptableVendor2ToU = Convert.ToString(txtAcceptableVendor2ToU.Text);
            else _acceptableVendor2ToU = "";

            return _acceptableVendor2ToU;
        }

        set
        {
            _acceptableVendor2ToU = value;
        }
    }

    public string AcceptableVendor3ToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtAcceptableVendor3ToU.Text))
                _acceptableVendor3ToU = Convert.ToString(txtAcceptableVendor3ToU.Text);
            else _acceptableVendor3ToU = "";

            return _acceptableVendor3ToU;
        }

        set
        {
            _acceptableVendor3ToU = value;
        }
    }

    public string AcceptableVendor4ToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtAcceptableVendor4ToU.Text))
                _acceptableVendor4ToU = Convert.ToString(txtAcceptableVendor4ToU.Text);
            else _acceptableVendor4ToU = "";

            return _acceptableVendor4ToU;
        }

        set
        {
            _acceptableVendor4ToU = value;
        }
    }

    public string AcceptableVendor5ToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtAcceptableVendor5ToU.Text))
                _acceptableVendor5ToU = Convert.ToString(txtAcceptableVendor5ToU.Text);
            else _acceptableVendor5ToU = "";

            return _acceptableVendor5ToU;
        }

        set
        {
            _acceptableVendor5ToU = value;
        }
    }

    public string RevisionNumberToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtRevisionNoToU.Text))
                _revisionNumberToU = Convert.ToString(txtRevisionNoToU.Text);
            else _revisionNumberToU = "00";

            return _revisionNumberToU;
        }

        set
        {
            _revisionNumberToU = value;
        }
    }

    public double BudgetedCostToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtBudgetedCostToU.Text))
                _budgetedCostToU = Convert.ToDouble(txtBudgetedCostToU.Text);
            else _budgetedCostToU = 0;

            return _budgetedCostToU;
        }

        set
        {
            _budgetedCostToU = value;
        }
    }

    public double EstimatedCostToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtEstimatedCostToU.Text))
                _estimatedCostToU = Convert.ToDouble(txtEstimatedCostToU.Text);
            else _estimatedCostToU = 0;

            return _estimatedCostToU;
        }

        set
        {
            _estimatedCostToU = value;
        }
    }

    public string CostRelatedRemarksToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtCostRelatedRemarksToU.Text))
                _costRelatedRemarksToU = Convert.ToString(txtCostRelatedRemarksToU.Text);
            else _costRelatedRemarksToU = "";

            return _costRelatedRemarksToU;
        }

        set
        {
            _costRelatedRemarksToU = value;
        }
    }

    public int PivotGroupFidToU
    {
        get
        {
            if (ddlPivotGroupToU.SelectedIndex > 0)
                _pivotGroupFidToU = Convert.ToInt32(ddlPivotGroupToU.SelectedValue);
            else _pivotGroupFidToU = 0;

            return _pivotGroupFidToU;
        }

        set
        {
            _pivotGroupFidToU = value;
        }
    }

    public int ResponsibleForBomFidToU
    {
        get
        {
            if (ddlResponsibleForBOMToU.SelectedIndex > 0)
                _responsibleForBomFidToU = Convert.ToInt32(ddlResponsibleForBOMToU.SelectedValue);
            else _responsibleForBomFidToU = 0;

            return _responsibleForBomFidToU;
        }

        set
        {
            _responsibleForBomFidToU = value;
        }
    }

    public int IsTcRequiredToU
    {
        get
        {
            if (chkIsTCRequiredToU.Checked)
                _isTcRequiredToU = 1;
            else _isTcRequiredToU = 0;

            return _isTcRequiredToU;
        }

        set
        {
            _isTcRequiredToU = value;
        }
    }

    public int CreatedByFidToU
    {
        get
        {
            _createdByFidToU = CurrentUserId;
            return _createdByFidToU;
        }
    }

    public string CreatedRemarksToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtRemarksToU.Text))
                _createdRemarksToU = Convert.ToString(txtRemarksToU.Text);
            else _createdRemarksToU = "";

            return _createdRemarksToU;
        }

        set
        {
            _createdRemarksToU = value;
        }
    }

    public string RemarksToU
    {
        get
        {
            if (!string.IsNullOrEmpty(txtRemarksToU.Text))
                _remarksToU = Convert.ToString(txtRemarksToU.Text);
            else _remarksToU = "";

            return _remarksToU;
        }

        set
        {
            _remarksToU = value;
        }
    }



    public int ApprovedByFidToU
    {
        get
        {
            return _approvedByFidToU;
        }

        set
        {
            _approvedByFidToU = value;
        }
    }

    public DateTime ApprovedOnToU
    {
        get
        {
            return _approvedOnToU;
        }

        set
        {
            _approvedOnToU = value;
        }
    }

    public string ApprovedRemarksToU
    {
        get
        {
            return _approvedRemarksToU;
        }

        set
        {
            _approvedRemarksToU = value;
        }
    }

    public int AmendmentCountToU
    {
        get
        {
            return _amendmentCountToU;
        }

        set
        {
            _amendmentCountToU = value;
        }
    }

    public int AmendmentByFidToU
    {
        get
        {
            return _amendmentByFidToU;
        }

        set
        {
            _amendmentByFidToU = value;
        }
    }

    public DateTime AmendmentOnToU
    {
        get
        {
            return _amendmentOnToU;
        }

        set
        {
            _amendmentOnToU = value;
        }
    }

    public string AmendmentRemarksToU
    {
        get
        {
            return _amendmentRemarksToU;
        }

        set
        {
            _amendmentRemarksToU = value;
        }
    }

    public int AmendedByFidToU
    {
        get
        {
            return _amendedByFidToU;
        }

        set
        {
            _amendedByFidToU = value;
        }
    }

    public DateTime AmendedOnToU
    {
        get
        {
            return _amendedOnToU;
        }

        set
        {
            _amendedOnToU = value;
        }
    }

    public string AmendedRemarksToU
    {
        get
        {
            return _amendedRemarksToU;
        }

        set
        {
            _amendedRemarksToU = value;
        }
    }

    public int AmendedApprovedByFidToU
    {
        get
        {
            return _amendedApprovedByFidToU;
        }

        set
        {
            _amendedApprovedByFidToU = value;
        }
    }

    public DateTime AmendedApprovedOnToU
    {
        get
        {
            return _amendedApprovedOnToU;
        }

        set
        {
            _amendedApprovedOnToU = value;
        }
    }

    public string AmendedApprovedRemrksToU
    {
        get
        {
            return _amendedApprovedRemrksToU;
        }

        set
        {
            _amendedApprovedRemrksToU = value;
        }
    }

    public int IsSentForApprovalToU
    {
        get
        {
            _isSentForApprovalToU = Convert.ToInt32(rdSavingType.SelectedValue);
            return _isSentForApprovalToU;
        }

        set
        {
            _isSentForApprovalToU = value;
        }
    }

    public int IsApprovalMailSentToU
    {
        get
        {
            return _isApprovalMailSentToU;
        }

        set
        {
            _isApprovalMailSentToU = value;
        }
    }

    public int IsApprovedMailSentToU
    {
        get
        {
            return _isApprovedMailSentToU;
        }

        set
        {
            _isApprovedMailSentToU = value;
        }
    }

    public int IsAmendmentMailSentToU
    {
        get
        {
            return _isAmendmentMailSentToU;
        }

        set
        {
            _isAmendmentMailSentToU = value;
        }
    }

    public int IsAmendedMailSentToU
    {
        get
        {
            return _isAmendedMailSentToU;
        }

        set
        {
            _isAmendedMailSentToU = value;
        }
    }

    public int IsAmendedApprovedMailSentToU
    {
        get
        {
            return _isAmendedApprovedMailSentToU;
        }

        set
        {
            _isAmendedApprovedMailSentToU = value;
        }
    }





    public string StartDateToB
    {
        get
        {
            if (chkSelectDeselectBomDate.Checked)
                _startDateToB = Convert.ToDateTime(hdBomStartDateSearchToS.Value).ToString("yyyy-MM-dd");
            else _startDateToB = "";

            return _startDateToB;
        }

        set
        {
            _startDateToB = value;
        }
    }

    public string EndDateToB
    {
        get
        {
            if (chkSelectDeselectBomDate.Checked)
                _endDateToB = Convert.ToDateTime(hdBomEndDateSearchToS.Value).ToString("yyyy-MM-dd");
            else _endDateToB = "";

            return _endDateToB;
        }

        set
        {
            _endDateToB = value;
        }
    }

    public string BomNoToB
    {
        get
        {
            if (!string.IsNullOrEmpty(txtBomNoToS.Text))
                _bomNoToB = Convert.ToString(txtBomNoToS.Text);
            else _bomNoToB = "";
            return _bomNoToB;
        }

        set
        {
            _bomNoToB = value;
        }
    }

    public string JobNoToB
    {
        get
        {
            if (!string.IsNullOrEmpty(txtJobNoToS.Text))
                _jobNoToB = Convert.ToString(txtJobNoToS.Text);
            else _jobNoToB = "";
            return _jobNoToB;
        }

        set
        {
            _jobNoToB = value;
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
                hdConfirmValueToU.Value = "0";

                ClearSessions();

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");



                hdBomStartDateSearchToS.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtBomStartDateSearchToS.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                hdBomEndDateSearchToS.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtBomEndDateSearchToS.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");



                GetUnit();
                GetBomType();
                GetBomResponsibleFor();
                GetPivotGroups();

                BindStatus();
                BindUnitToS();
                BindBomTypeToS();
                BindBomMrCreatedByToS();
                BindBomResponsibleForToS();
                BindPivotGroupsToS();


                BindUnitToU();
                BindBomTypeToU();
                BindBomResponsibleForToU();
                BindPivotGroupsToU();

                GetPostedBOMList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetPostedBOMList();
    }

    protected void btnGetBomDetailList_Click(object sender, EventArgs e)
    {
        mpeAddUpdate.Show();
        mpeShowBomList.Show();
    }

    protected void btnSearchBomList_Click(object sender, EventArgs e)
    {
        GetBOMList();
        mpeAddUpdate.Show();
        mpeShowBomList.Show();
    }

    protected void imgBtnAddNew_Click(object sender, EventArgs e)
    {
        ViewState["ActionId"] = (int)BOMAllTypeEnums.EnumAction.Create;
        ViewState["StatusId"] = (int)BOMAllTypeEnums.EnumStatus.New;

        //btnSave.Visible = true;
        //btnUpdateStatus.Visible = false;
        //btnSendToAmendment.Visible = false;

        ddlUnitToU.Enabled = true;
        ddlTypeToU.Enabled = true;

        pnlSave.Visible = true;
        pnlUpdateStatus.Visible = false;
        pnlSendToAmendment.Visible = false;


        ViewState["bomFid"] = null;
        ViewState["PID"] = null;
        pnlCreatedRemarksToU.Visible = false;
        EnableBOMLine();
        ResetBOMLine();

        txtBomDateToU.Enabled = false;
        txtBomNoToU.Enabled = false;
        txtJobNoToU.Enabled = false;
        btnGetBomDetailList.Visible = true;
        gvProductList.Enabled = true;

        imgbtnMrDateToU.Visible = true;
        imgbtnDeliveryRequiredByToU.Visible = true;

        mpeAddUpdate.Show();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValueToU.Value) > 0)
        {
            InsertUpdateBOM();
        }
    }

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        UpdateStatus();
    }


    protected void btnSendToAmendment_Click(object sender, EventArgs e)
    {
        ViewState["StatusId"] = (int)BOMAllTypeEnums.EnumStatus.Amendment;

        UpdateStatus();

        GetPostedBOMList();
    }


    protected void gvPostedBomList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                txtRemarksToU.Text = "";
                hdConfirmValueToU.Value = "0";
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "STATUS" ||
                    Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_DETAIL" ||
                    Convert.ToString(e.CommandArgument) == "SEND_MAIL" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_PRODUCT_LIST" ||
                    Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "APPROVE" ||
                         Convert.ToString(e.CommandArgument) == "AMEND")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                ViewState["StatusId"] = null;

                Label lblPid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblPid_InPostedList") as Label;
                Label lblUnitFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblUnitFid_InPostedList") as Label;
                Label lblTypeFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblTypeFid_InPostedList") as Label;
                Label lblMrNo_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblMrNo_InPostedList") as Label;
                Label lblMrDate_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblMrDate_InPostedList") as Label;
                Label lblStatusFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblStatusFid_InPostedList") as Label;
                Label lblStatus_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblStatus_InPostedList") as Label;
                Label lblBomNo_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblBomNo_InPostedList") as Label;
                Label lblBomDate_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblBomDate_InPostedList") as Label;
                Label lblJobNo_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblJobNo_InPostedList") as Label;
                Label lblDeliveryRequiredBy_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblDeliveryRequiredBy_InPostedList") as Label;
                Label lblAcceptableVendor1_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAcceptableVendor1_InPostedList") as Label;
                Label lblAcceptableVendor2_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAcceptableVendor2_InPostedList") as Label;
                Label lblAcceptableVendor3_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAcceptableVendor3_InPostedList") as Label;
                Label lblAcceptableVendor4_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAcceptableVendor4_InPostedList") as Label;
                Label lblAcceptableVendor5_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAcceptableVendor5_InPostedList") as Label;
                Label lblRevisionNumber_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblRevisionNumber_InPostedList") as Label;
                Label lblBudgetedCost_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblBudgetedCost_InPostedList") as Label;
                Label lblEstimatedCost_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblEstimatedCost_InPostedList") as Label;
                Label lblCostRelatedRemarks_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblCostRelatedRemarks_InPostedList") as Label;
                Label lblPivotGroupFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblPivotGroupFid_InPostedList") as Label;
                Label lblResponsibleForBomFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblResponsibleForBomFid_InPostedList") as Label;
                Label lblIsTcRequired_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblIsTcRequired_InPostedList") as Label;
                Label lblCreatedByFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblCreatedByFid_InPostedList") as Label;
                Label lblCreatedRemarks_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblCreatedRemarks_InPostedList") as Label;
                Label lblApprovedByFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblApprovedByFid_InPostedList") as Label;
                Label lblApprovedRemarks_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblApprovedRemarks_InPostedList") as Label;
                Label lblAmendmentCount_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAmendmentCount_InPostedList") as Label;
                Label lblAmendmentByFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAmendmentByFid_InPostedList") as Label;
                Label lblAmendmentRemarks_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAmendmentRemarks_InPostedList") as Label;
                Label lblAmendedByFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAmendedByFid_InPostedList") as Label;
                Label lblAmendedRemarks_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAmendedRemarks_InPostedList") as Label;
                Label lblAmendedApprovedByFid_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAmendedApprovedByFid_InPostedList") as Label;
                Label lblAmendedApprovedRemrks_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblAmendedApprovedRemrks_InPostedList") as Label;
                Label lblIsSentForApproval_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblIsSentForApproval_InPostedList") as Label;
                Label lblIsApprovalMailSent_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblIsApprovalMailSent_InPostedList") as Label;
                Label lblIsApprovedMailSent_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblIsApprovedMailSent_InPostedList") as Label;
                Label lblIsAmendmentMailSent_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblIsAmendmentMailSent_InPostedList") as Label;
                Label lblIsAmendedMailSent_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblIsAmendedMailSent_InPostedList") as Label;
                Label lblIsAmendedApprovedMailSent_InPostedList = gvPostedBomList.Rows[rowindex].FindControl("lblIsAmendedApprovedMailSent_InPostedList") as Label;


                lblMrNoLegend.Text = lblMrNo_InPostedList.Text;

                pnlCreatedRemarksToU.Visible = false;
                pnlAmendmentRemarksToU.Visible = false;
                pnlEditAmendedRemarksToU.Visible = false;
                pnlSavingTypeToU.Visible = false;

                ViewState["PID"] = Convert.ToInt32(lblPid_InPostedList.Text);
                ViewState["MRNNO"] = Convert.ToString(lblMrNo_InPostedList.Text);

                if (Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "AMEND" ||
                    Convert.ToString(e.CommandArgument) == "APPROVE" ||
                    Convert.ToString(e.CommandArgument) == "CANCEL")
                {

                    ViewState["bomFid"] = Convert.ToInt32(lblPid_InPostedList.Text);
                    txtBomDateToU.Enabled = false;
                    txtBomNoToU.Enabled = false;
                    txtJobNoToU.Enabled = false;

                    pnlCreatedRemarksToU.Visible = true;

                    if (Convert.ToInt32(lblAmendmentCount_InPostedList.Text) > 0)
                        pnlAmendmentRemarksToU.Visible = true;


                    txtMrDateToU.Text = lblMrDate_InPostedList.Text;
                    hdMrDateToU.Value = txtMrDateToU.Text;

                    txtBomDateToU.Text = lblBomDate_InPostedList.Text;
                    txtBomNoToU.Text = lblBomNo_InPostedList.Text;
                    txtJobNoToU.Text = lblJobNo_InPostedList.Text;

                    ddlUnitToU.SelectedValue = lblUnitFid_InPostedList.Text;
                    ddlTypeToU.SelectedValue = lblTypeFid_InPostedList.Text;

                    txtDeliveryRequiredByToU.Text = lblDeliveryRequiredBy_InPostedList.Text;
                    hdDeliveryRequiredByToU.Value = txtDeliveryRequiredByToU.Text;

                    txtAcceptableVendor1ToU.Text = lblAcceptableVendor1_InPostedList.Text;
                    txtAcceptableVendor2ToU.Text = lblAcceptableVendor2_InPostedList.Text;
                    txtAcceptableVendor3ToU.Text = lblAcceptableVendor3_InPostedList.Text;
                    txtAcceptableVendor4ToU.Text = lblAcceptableVendor4_InPostedList.Text;
                    txtAcceptableVendor5ToU.Text = lblAcceptableVendor5_InPostedList.Text;

                    txtRevisionNoToU.Text = lblRevisionNumber_InPostedList.Text;
                    txtBudgetedCostToU.Text = lblBudgetedCost_InPostedList.Text;
                    txtEstimatedCostToU.Text = lblEstimatedCost_InPostedList.Text;
                    txtCostRelatedRemarksToU.Text = lblCostRelatedRemarks_InPostedList.Text;
                    ddlPivotGroupToU.SelectedValue = lblPivotGroupFid_InPostedList.Text;
                    ddlResponsibleForBOMToU.SelectedValue = lblResponsibleForBomFid_InPostedList.Text;

                    txtCreatedRemarksToU.Text = lblCreatedRemarks_InPostedList.Text;
                    txtCreatedRemarksToU.Enabled = false;

                    if (Convert.ToInt32(lblIsTcRequired_InPostedList.Text) > 0)
                        chkIsTCRequiredToU.Checked = true;


                }


                //btnSave.Visible = false;
                //btnUpdateStatus.Visible = false;
                //btnSendToAmendment.Visible = false;


                pnlSave.Visible = false;
                pnlUpdateStatus.Visible = false;
                pnlSendToAmendment.Visible = false;

                if (Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "AMEND")
                {
                    ViewState["ActionId"] = (int)BOMAllTypeEnums.EnumAction.Edit;

                    if (Convert.ToString(e.CommandArgument) == "EDIT")
                        ViewState["StatusId"] = (int)BOMAllTypeEnums.EnumStatus.New;
                    else if (Convert.ToString(e.CommandArgument) == "AMEND")
                        ViewState["StatusId"] = (int)BOMAllTypeEnums.EnumStatus.Amended;

                    ddlUnitToU.Enabled = false;
                    ddlTypeToU.Enabled = false;
                    BindProductList(lblBomNo_InPostedList.Text.ToUpper(), Convert.ToInt32(lblPid_InPostedList.Text));

                    gvProductList.Enabled = true;
                    txtAmendmentRemarksToU.Enabled = false;

                    //btnSave.Visible = true;
                    pnlSave.Visible = true;
                    btnSave.Text = "Save MR Request";
                    pnlSavingTypeToU.Visible = true;
                    mpeAddUpdate.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "APPROVE")
                {

                    //btnUpdateStatus.Visible = true;
                    //btnSendToAmendment.Visible = true;

                    pnlUpdateStatus.Visible = true;
                    pnlSendToAmendment.Visible = true;

                    if (Convert.ToInt32(lblAmendmentCount_InPostedList.Text) > 0)
                    {
                        ViewState["StatusId"] = (int)BOMAllTypeEnums.EnumStatus.AmendedApproved;
                        btnUpdateStatus.Text = "Approve Amended MR Request";
                    }
                    else
                    {
                        ViewState["StatusId"] = (int)BOMAllTypeEnums.EnumStatus.Approved;
                        btnUpdateStatus.Text = "Approve MR Request";
                    }
                    gvProductList.Enabled = false;
                    if (Convert.ToInt32(lblAmendmentCount_InPostedList.Text) > 0)
                    {
                        pnlAmendmentRemarksToU.Visible = true;
                        pnlEditAmendedRemarksToU.Visible = true;
                    }

                    BindPostedProducts(lblBomNo_InPostedList.Text.ToUpper(), Convert.ToInt32(lblPid_InPostedList.Text), (int)BOMAllTypeEnums.EnumProductScreenType.ApproveScreen);

                    DisableBOMLine();
                    mpeAddUpdate.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    ViewState["StatusId"] = (int)BOMAllTypeEnums.EnumStatus.Cancelled;
                    pnlUpdateStatus.Visible = true;
                    btnUpdateStatus.Text = "Cancel MR Request";
                    gvProductList.Enabled = false;

                    BindPostedProducts(lblBomNo_InPostedList.Text.ToUpper(), Convert.ToInt32(lblPid_InPostedList.Text), (int)BOMAllTypeEnums.EnumProductScreenType.ApproveScreen);

                    DisableBOMLine();
                    mpeAddUpdate.Show();
                }


                else if (Convert.ToString(e.CommandArgument) == "VIEW_PRODUCT_LIST")
                {
                    BindPostedProducts(lblBomNo_InPostedList.Text.ToUpper(), Convert.ToInt32(lblPid_InPostedList.Text), (int)BOMAllTypeEnums.EnumProductScreenType.ViewScreen);
                    mpeProductDetail.Show();

                }

                else if (Convert.ToString(e.CommandArgument) == "VIEW_DETAIL")
                {
                    mpeViewInPDF.Show();
                    iframeViewDrawingDetailsInPDF.Attributes.Add("src", "BOMDetailInPDF.aspx?recordID=" + Convert.ToInt32(lblPid_InPostedList.Text) + "");
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                {
                    BOMSendMail objBOMSendMail = new BOMSendMail(Convert.ToInt32(lblPid_InPostedList.Text));
                    bool sentMailValue = objBOMSendMail.InitializeSendMail();

                    if (sentMailValue)
                    {
                        int mailStatusVal = objProjM.UpdateMailStatus(Convert.ToInt32(lblPid_InPostedList.Text)
                                                                    , Convert.ToInt32(lblStatusFid_InPostedList.Text));

                        SuccessMessage("MR Request saved with MR No.'" + Convert.ToString(lblMrNo_InPostedList.Text) + "' saved and mail sent successfully.");
                    }
                    else
                        SuccessMessage("MR Request saved with MR No.'" + Convert.ToString(lblMrNo_InPostedList.Text) + "' saved successfully.");
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

    protected void gvPostedBomList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPid_InPostedList = (Label)e.Row.FindControl("lblPid_InPostedList");
            Label lblUnitFid_InPostedList = (Label)e.Row.FindControl("lblUnitFid_InPostedList");
            Label lblTypeFid_InPostedList = (Label)e.Row.FindControl("lblTypeFid_InPostedList");
            Label lblMrNo_InPostedList = (Label)e.Row.FindControl("lblMrNo_InPostedList");
            Label lblStatusFid_InPostedList = (Label)e.Row.FindControl("lblStatusFid_InPostedList");
            Label lblStatus_InPostedList = (Label)e.Row.FindControl("lblStatus_InPostedList");
            Label lblBomNo_InPostedList = (Label)e.Row.FindControl("lblBomNo_InPostedList");
            Label lblBomDate_InPostedList = (Label)e.Row.FindControl("lblBomDate_InPostedList");
            Label lblJobNo_InPostedList = (Label)e.Row.FindControl("lblJobNo_InPostedList");
            Label lblDeliveryRequiredBy_InPostedList = (Label)e.Row.FindControl("lblDeliveryRequiredBy_InPostedList");
            Label lblAcceptableVendor1_InPostedList = (Label)e.Row.FindControl("lblAcceptableVendor1_InPostedList");
            Label lblAcceptableVendor2_InPostedList = (Label)e.Row.FindControl("lblAcceptableVendor2_InPostedList");
            Label lblAcceptableVendor3_InPostedList = (Label)e.Row.FindControl("lblAcceptableVendor3_InPostedList");
            Label lblAcceptableVendor4_InPostedList = (Label)e.Row.FindControl("lblAcceptableVendor4_InPostedList");
            Label lblAcceptableVendor5_InPostedList = (Label)e.Row.FindControl("lblAcceptableVendor5_InPostedList");
            Label lblRevisionNumber_InPostedList = (Label)e.Row.FindControl("lblRevisionNumber_InPostedList");
            Label lblBudgetedCost_InPostedList = (Label)e.Row.FindControl("lblBudgetedCost_InPostedList");
            Label lblEstimatedCost_InPostedList = (Label)e.Row.FindControl("lblEstimatedCost_InPostedList");
            Label lblCostRelatedRemarks_InPostedList = (Label)e.Row.FindControl("lblCostRelatedRemarks_InPostedList");
            Label lblPivotGroupFid_InPostedList = (Label)e.Row.FindControl("lblPivotGroupFid_InPostedList");
            Label lblResponsibleForBomFid_InPostedList = (Label)e.Row.FindControl("lblResponsibleForBomFid_InPostedList");
            Label lblIsTcRequired_InPostedList = (Label)e.Row.FindControl("lblIsTcRequired_InPostedList");
            Label lblCreatedByFid_InPostedList = (Label)e.Row.FindControl("lblCreatedByFid_InPostedList");
            Label lblCreatedRemarks_InPostedList = (Label)e.Row.FindControl("lblCreatedRemarks_InPostedList");
            Label lblApprovedByFid_InPostedList = (Label)e.Row.FindControl("lblApprovedByFid_InPostedList");
            Label lblApprovedRemarks_InPostedList = (Label)e.Row.FindControl("lblApprovedRemarks_InPostedList");
            Label lblAmendmentCount_InPostedList = (Label)e.Row.FindControl("lblAmendmentCount_InPostedList");
            Label lblAmendmentByFid_InPostedList = (Label)e.Row.FindControl("lblAmendmentByFid_InPostedList");
            Label lblAmendmentRemarks_InPostedList = (Label)e.Row.FindControl("lblAmendmentRemarks_InPostedList");
            Label lblAmendedByFid_InPostedList = (Label)e.Row.FindControl("lblAmendedByFid_InPostedList");
            Label lblAmendedRemarks_InPostedList = (Label)e.Row.FindControl("lblAmendedRemarks_InPostedList");
            Label lblAmendedApprovedByFid_InPostedList = (Label)e.Row.FindControl("lblAmendedApprovedByFid_InPostedList");
            Label lblAmendedApprovedRemrks_InPostedList = (Label)e.Row.FindControl("lblAmendedApprovedRemrks_InPostedList");
            Label lblIsSentForApproval_InPostedList = (Label)e.Row.FindControl("lblIsSentForApproval_InPostedList");
            Label lblIsApprovalMailSent_InPostedList = (Label)e.Row.FindControl("lblIsApprovalMailSent_InPostedList");
            Label lblIsApprovedMailSent_InPostedList = (Label)e.Row.FindControl("lblIsApprovedMailSent_InPostedList");
            Label lblIsAmendmentMailSent_InPostedList = (Label)e.Row.FindControl("lblIsAmendmentMailSent_InPostedList");
            Label lblIsAmendedMailSent_InPostedList = (Label)e.Row.FindControl("lblIsAmendedMailSent_InPostedList");
            Label lblIsAmendedApprovedMailSent_InPostedList = (Label)e.Row.FindControl("lblIsAmendedApprovedMailSent_InPostedList");

            Label lblPeID_InPostedList = (Label)e.Row.FindControl("lblPeID_InPostedList");
            Label lblPmID_InPostedList = (Label)e.Row.FindControl("lblPmID_InPostedList");

            ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
            ImageButton imgBtnEditLOT = (ImageButton)e.Row.FindControl("imgBtnEditLOT");
            ImageButton imgBtnSendMail = (ImageButton)e.Row.FindControl("imgBtnSendMail");
            Button btnApproveBOM = (Button)e.Row.FindControl("btnApproveBOM");
            Button btnAmendBOM = (Button)e.Row.FindControl("btnAmendBOM");


            CheckBox chkIsTCRequired = (CheckBox)e.Row.FindControl("chkIsTCRequired");

            if (Convert.ToInt32(lblIsTcRequired_InPostedList.Text) > 0)
            {
                chkIsTCRequired.Checked = true;
            }


            imgBtnEditLOT.Visible = false;
            imgBtnSendMail.Visible = false;
            btnApproveBOM.Visible = false;
            btnAmendBOM.Visible = false;

            int statusID = Convert.ToInt32(lblStatusFid_InPostedList.Text);

            if (statusID == (int)BOMAllTypeEnums.EnumStatus.New)
            {
                imgStatus.ImageUrl = "~/Images/NEWICONS/New03.png";
                imgStatus.ToolTip = "New";

                if (CurrentUserId == Convert.ToInt32(lblCreatedByFid_InPostedList.Text))
                {
                    imgBtnEditLOT.Visible = true;

                    if (Convert.ToInt32(lblIsApprovalMailSent_InPostedList.Text) == 0 &&
                        Convert.ToInt32(lblIsSentForApproval_InPostedList.Text) > 0)
                        imgBtnSendMail.Visible = true;
                }

                if (CurrentUserId == Convert.ToInt32(lblPeID_InPostedList.Text) ||
                    CurrentUserId == Convert.ToInt32(lblPmID_InPostedList.Text))
                {
                    btnApproveBOM.Visible = true;
                }
            }

            else if (statusID == (int)BOMAllTypeEnums.EnumStatus.Approved)
            {
                imgStatus.ImageUrl = "~/Images/NEWICONS/Approved03.png";
                imgStatus.ToolTip = "Approved";

                if (CurrentUserId == Convert.ToInt32(lblApprovedByFid_InPostedList.Text))
                {
                    if (Convert.ToInt32(lblIsApprovedMailSent_InPostedList.Text) == 0)
                        imgBtnSendMail.Visible = true;
                }
            }

            else if (statusID == (int)BOMAllTypeEnums.EnumStatus.Amendment)
            {
                imgStatus.ImageUrl = "~/Images/NEWICONS/amendment.png";
                imgStatus.ToolTip = "Amendment";

                if (CurrentUserId == Convert.ToInt32(lblCreatedByFid_InPostedList.Text))
                {
                    btnAmendBOM.Visible = true;
                }

                if (CurrentUserId == Convert.ToInt32(lblAmendmentByFid_InPostedList.Text))
                {
                    if (Convert.ToInt32(lblIsAmendmentMailSent_InPostedList.Text) == 0)
                        imgBtnSendMail.Visible = true;
                }

            }

            else if (statusID == (int)BOMAllTypeEnums.EnumStatus.Amended)
            {
                imgStatus.ImageUrl = "~/Images/NEWICONS/amended.png";
                imgStatus.ToolTip = "Amended";

                if (CurrentUserId == Convert.ToInt32(lblAmendedByFid_InPostedList.Text))
                {
                    imgBtnEditLOT.Visible = true;

                    if (Convert.ToInt32(lblIsAmendedMailSent_InPostedList.Text) == 0)
                        imgBtnSendMail.Visible = true;
                }

                if (CurrentUserId == Convert.ToInt32(lblPeID_InPostedList.Text) ||
                    CurrentUserId == Convert.ToInt32(lblPmID_InPostedList.Text))
                {
                    btnApproveBOM.Visible = true;
                }

            }

            else if (statusID == (int)BOMAllTypeEnums.EnumStatus.AmendedApproved)
            {
                imgStatus.ImageUrl = "~/Images/NEWICONS/Approved.png";
                imgStatus.ToolTip = "Amended Approved";

                if (CurrentUserId == Convert.ToInt32(lblAmendedApprovedByFid_InPostedList.Text))
                {
                    if (Convert.ToInt32(lblIsAmendedApprovedMailSent_InPostedList.Text) == 0)
                        imgBtnSendMail.Visible = true;
                }
            }


            else if (statusID == (int)BOMAllTypeEnums.EnumStatus.Cancelled)
            {
                imgStatus.ImageUrl = "~/Images/Icons/no2.png";
                imgStatus.ToolTip = "Cancelled";

                if (CurrentUserId == Convert.ToInt32(lblApprovedByFid_InPostedList.Text))
                {
                    if (Convert.ToInt32(lblIsApprovedMailSent_InPostedList.Text) == 0)
                        imgBtnSendMail.Visible = true;
                }
            }


            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }

    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        GetAndBindEstimatedCost();
    }

    protected void ddlQuantity_SelectedIndexChanged(object sender, EventArgs e)
    {
        mpeAddUpdate.Show();
        if (gvProductList.Rows.Count > 0)
        {
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList ddlQuantity = (DropDownList)gvr.FindControl("ddlQuantity");

            Label lblTotalQuantity = (Label)gvr.FindControl("lblTotalQuantity");
            Label lblTotalAmount = (Label)gvr.FindControl("lblTotalAmount");
            Label lblPerUnitAmount = (Label)gvr.FindControl("lblPerUnitAmount");


            Label lblPostedQuantity = (Label)gvr.FindControl("lblPostedQuantity");
            Label lblPostedAmount = (Label)gvr.FindControl("lblPostedAmount");

            Label lblRemainingQuantity = (Label)gvr.FindControl("lblRemainingQuantity");
            Label lblRemainingAmount = (Label)gvr.FindControl("lblRemainingAmount");

            TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");

            txtAmount.Text = Convert.ToString(Convert.ToDouble(ddlQuantity.SelectedValue) * Convert.ToDouble(lblPerUnitAmount.Text));

            GetAndBindEstimatedCost();
        }
    }




    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblLPID = (Label)e.Row.FindControl("lblLPID");
            //Label lblIsBomFid = (Label)e.Row.FindControl("lblIsBomFid");

            Label lblRemainingQuantity = (Label)e.Row.FindControl("lblRemainingQuantity");

            Label lblPostedQuantity = (Label)e.Row.FindControl("lblPostedQuantity");
            Label lblPostedAmount = (Label)e.Row.FindControl("lblPostedAmount");
            TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");

            int remainingQuantity = Convert.ToInt32(lblRemainingQuantity.Text);
            int postedQuantity = Convert.ToInt32(lblPostedQuantity.Text);

            //if (Convert.ToInt32(lblLPID.Text) > 0)
            //{
            //    //quantity = Convert.ToInt32(lblPostedQuantity.Text);
            //    //txtAmount.Text = lblPostedAmount.Text;
            //}
            //else
            //{
            //    //quantity = Convert.ToInt32(lblRemainingQuantity.Text);
            //}


            if (Convert.ToInt32(lblLPID.Text) > 0)
            {
                if (remainingQuantity == 0)
                    remainingQuantity = postedQuantity;
            }
            else
            {
                //quantity = Convert.ToInt32(lblRemainingQuantity.Text);
            }

            DropDownList ddlQuantity = (DropDownList)e.Row.FindControl("ddlQuantity");
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");

            //if (Convert.ToInt32(ViewState["StatusId"]) != (int)BOMAllTypeEnums.EnumStatus.New)
            if (Convert.ToInt32(ViewState["ActionId"]) != (int)BOMAllTypeEnums.EnumAction.Create)
            {
                if (Convert.ToInt32(lblLPID.Text) > 0)//&& Convert.ToInt32(lblIsBomFid.Text) > 0
                {
                    chkSelect.Checked = true;

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }
            }
            else chkSelect.Checked = false;



            DataTable dtQuantity = new DataTable();
            dtQuantity.Columns.Add("QUANTITY", typeof(int));


            for (int i = remainingQuantity; i >= 1; i--)
            {
                DataRow drQ = dtQuantity.NewRow();
                drQ["QUANTITY"] = i;
                dtQuantity.Rows.Add(drQ);
            }

            ddlQuantity.DataSource = dtQuantity;
            ddlQuantity.DataTextField = "QUANTITY";
            ddlQuantity.DataValueField = "QUANTITY";
            ddlQuantity.DataBind();


            if (Convert.ToInt32(lblLPID.Text) > 0)
            {
                ddlQuantity.SelectedValue = postedQuantity.ToString();
                txtAmount.Text = lblPostedAmount.Text;
            }
            else
            {
                //quantity = Convert.ToInt32(lblRemainingQuantity.Text);
            }


            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }

    }



    protected void gvBomList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "GET_BOM")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblBomNo = gvBomList.Rows[rowindex].FindControl("lblBomNo") as Label;
                Label lblBomDate = gvBomList.Rows[rowindex].FindControl("lblBomDate") as Label;
                Label lblJobNo = gvBomList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblPivotGroup = gvBomList.Rows[rowindex].FindControl("lblPivotGroup") as Label;

                if (Convert.ToString(e.CommandArgument) == "GET_BOM")
                {
                    txtBomNoToU.Text = lblBomNo.Text.ToUpper();
                    txtBomDateToU.Text = lblBomDate.Text;
                    txtJobNoToU.Text = lblJobNo.Text.ToUpper();

                    int bomFid = 0;
                    if (ViewState["bomFid"] != null)
                        bomFid = Convert.ToInt32(ViewState["bomFid"]);

                    BindProductList(lblBomNo.Text.ToUpper(), bomFid);

                    mpeAddUpdate.Show();
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

    protected void gvBomList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //
    }

    #endregion


    #region METHODS[=======================]

    private void ClearSessions()
    {
        Session["PostedBomList"] = null;
        Session["dsUnit"] = null;
        Session["dsBomType"] = null;
        Session["dsBomResponsibleFor"] = null;
        Session["dsBomPivotGroups"] = null;
    }


    private void BindStatus()
    {
        try
        {
            _dsStatus = objProjM.GetBomStatus();
            if (_dsStatus.Tables.Count > 0 && _dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = _dsStatus.Tables[0];
                ddlStatus.DataTextField = "NAME";
                ddlStatus.DataValueField = "PID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetUnit()
    {
        try
        {
            Session["dsUnit"] = null;
            _dsUnit = objCommon.GetUnit();
            if (_dsUnit.Tables.Count > 0 && _dsUnit.Tables[0].Rows.Count > 0)
                Session["dsUnit"] = _dsUnit;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUnitToS()
    {
        try
        {
            if (Session["dsUnit"] != null)
                _dsUnit = (DataSet)Session["dsUnit"];

            if (_dsUnit.Tables.Count > 0 && _dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnit.DataSource = _dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");
                ddlUnit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUnitToU()
    {
        try
        {
            if (Session["dsUnit"] != null)
                _dsUnit = (DataSet)Session["dsUnit"];

            if (_dsUnit.Tables.Count > 0 && _dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnitToU.DataSource = _dsUnit.Tables[0];
                ddlUnitToU.DataTextField = "UNIT_NAME";
                ddlUnitToU.DataValueField = "UNIT_ID";
                ddlUnitToU.DataBind();


            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }





    private void GetBomType()
    {
        try
        {
            Session["dsBomType"] = null;

            _dsBomType = objProjM.GetBomTypes();
            if (_dsBomType.Tables.Count > 0 && _dsBomType.Tables[0].Rows.Count > 0)
                Session["dsBomType"] = _dsBomType;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindBomTypeToS()
    {
        try
        {
            if (Session["dsBomType"] != null)
                _dsBomType = (DataSet)Session["dsBomType"];

            if (_dsBomType.Tables.Count > 0 && _dsBomType.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = _dsBomType.Tables[0];
                ddlType.DataTextField = "NAME";
                ddlType.DataValueField = "PID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, "All");
                ddlType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindBomTypeToU()
    {
        try
        {
            if (Session["dsBomType"] != null)
                _dsBomType = (DataSet)Session["dsBomType"];

            if (_dsBomType.Tables.Count > 0 && _dsBomType.Tables[0].Rows.Count > 0)
            {
                ddlTypeToU.DataSource = _dsBomType.Tables[0];
                ddlTypeToU.DataTextField = "NAME";
                ddlTypeToU.DataValueField = "PID";
                ddlTypeToU.DataBind();
                ddlTypeToU.Items.Insert(0, "Select");
                ddlTypeToU.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void BindBomMrCreatedByToS()
    {
        try
        {
            _dsBomMrCreatedBy = objProjM.GetBomMrCreatedBy();

            if (_dsBomMrCreatedBy.Tables.Count > 0 && _dsBomMrCreatedBy.Tables[0].Rows.Count > 0)
            {
                ddlMrCreatedBy.DataSource = _dsBomMrCreatedBy.Tables[0];
                ddlMrCreatedBy.DataTextField = "CREATED_BY";
                ddlMrCreatedBy.DataValueField = "CREATED_BY_FID";
                ddlMrCreatedBy.DataBind();
                ddlMrCreatedBy.Items.Insert(0, "All");
                ddlMrCreatedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetBomResponsibleFor()
    {
        try
        {
            Session["dsBomResponsibleFor"] = null;

            _dsBomResponsibleFor = objProjM.GetBomResponsibleFor();
            if (_dsBomResponsibleFor.Tables.Count > 0 && _dsBomResponsibleFor.Tables[0].Rows.Count > 0)
                Session["dsBomResponsibleFor"] = _dsBomResponsibleFor;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindBomResponsibleForToS()
    {
        try
        {
            if (Session["dsBomResponsibleFor"] != null)
                _dsBomResponsibleFor = (DataSet)Session["dsBomResponsibleFor"];

            if (_dsBomResponsibleFor.Tables.Count > 0 && _dsBomResponsibleFor.Tables[0].Rows.Count > 0)
            {
                ddlResponsibleFor.DataSource = _dsBomResponsibleFor.Tables[0];
                ddlResponsibleFor.DataTextField = "NAME";
                ddlResponsibleFor.DataValueField = "PID";
                ddlResponsibleFor.DataBind();
                ddlResponsibleFor.Items.Insert(0, "All");
                ddlResponsibleFor.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindBomResponsibleForToU()
    {
        try
        {
            if (Session["dsBomResponsibleFor"] != null)
                _dsBomResponsibleFor = (DataSet)Session["dsBomResponsibleFor"];

            if (_dsBomResponsibleFor.Tables.Count > 0 && _dsBomResponsibleFor.Tables[0].Rows.Count > 0)
            {
                ddlResponsibleForBOMToU.DataSource = _dsBomResponsibleFor.Tables[0];
                ddlResponsibleForBOMToU.DataTextField = "NAME";
                ddlResponsibleForBOMToU.DataValueField = "PID";
                ddlResponsibleForBOMToU.DataBind();
                ddlResponsibleForBOMToU.Items.Insert(0, "Select");
                ddlResponsibleForBOMToU.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetPivotGroups()
    {
        try
        {
            Session["dsBomPivotGroups"] = null;

            _dsBomPivotGroups = objProjM.GetPivotGroupList("", "", "");
            if (_dsBomPivotGroups.Tables.Count > 0 && _dsBomPivotGroups.Tables[0].Rows.Count > 0)
                Session["dsBomPivotGroups"] = _dsBomPivotGroups;

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPivotGroupsToS()
    {
        try
        {
            if (Session["dsBomPivotGroups"] != null)
                _dsBomPivotGroups = (DataSet)Session["dsBomPivotGroups"];

            if (_dsBomPivotGroups.Tables.Count > 0 && _dsBomPivotGroups.Tables[0].Rows.Count > 0)
            {
                ddlPivotGroup.DataSource = _dsBomPivotGroups.Tables[0];
                ddlPivotGroup.DataTextField = "PIVOT_GROUP";
                ddlPivotGroup.DataValueField = "PID";
                ddlPivotGroup.DataBind();
                ddlPivotGroup.Items.Insert(0, "All");
                ddlPivotGroup.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPivotGroupsToU()
    {
        try
        {
            if (Session["dsBomPivotGroups"] != null)
                _dsBomPivotGroups = (DataSet)Session["dsBomPivotGroups"];

            if (_dsBomPivotGroups.Tables.Count > 0 && _dsBomPivotGroups.Tables[0].Rows.Count > 0)
            {
                ddlPivotGroupToU.DataSource = _dsBomPivotGroups.Tables[0];
                ddlPivotGroupToU.DataTextField = "PIVOT_GROUP";
                ddlPivotGroupToU.DataValueField = "PID";
                ddlPivotGroupToU.DataBind();
                ddlPivotGroupToU.Items.Insert(0, "Select");
                ddlPivotGroupToU.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }





    private void GetPostedBOMList()
    {
        try
        {
            _dsPostedBomList = objProjM.GetPostedBOMList
                          (DateTypeToS
                         , StartDateToS
                         , EndDateToS
                         , UnitIdToS
                         , TypeIdToS
                         , StatusIdToS
                         , PivotGroupIdToS
                         , ResponsibleForIdToS
                         , IsTcRequiredToS
                         , MrNoToS
                         , MrCreatedByIdToS
                         , BomNoToS
                         , JobNoToS);

            if (_dsPostedBomList.Tables.Count > 0 && _dsPostedBomList.Tables[0].Rows.Count > 0)
            {
                Session["PostedBomList"] = _dsPostedBomList.Tables[0];
                gvPostedBomList.DataSource = _dsPostedBomList.Tables[0];
                gvPostedBomList.DataBind();
            }
            else
            {
                Session["PostedBomList"] = null;
                gvPostedBomList.DataSource = null;
                gvPostedBomList.DataBind();
            }

            lblRecords.Text = "Records[" + _dsPostedBomList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetBOMList()
    {
        try
        {
            _dsBomList = objProjM.GetBOMList(StartDateToB, EndDateToB, BomNoToB, JobNoToB);

            if (_dsBomList.Tables.Count > 0 && _dsBomList.Tables[0].Rows.Count > 0)
            {
                gvBomList.DataSource = _dsBomList.Tables[0];
                gvBomList.DataBind();
            }
            else
            {
                gvBomList.DataSource = null;
                gvBomList.DataBind();
            }

            lblRecords.Text = "Records[" + _dsBomList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindProductList(string bomNo, int bomFid)
    {
        try
        {
            _dsBomLineToU = objProjM.GetProductListByBOMNo(bomNo, bomFid);

            if (_dsBomLineToU.Tables.Count > 0 && _dsBomLineToU.Tables[0].Rows.Count > 0)
            {
                gvProductList.DataSource = _dsBomLineToU.Tables[0];
                gvProductList.DataBind();
            }
            else
            {
                gvProductList.DataSource = null;
                gvProductList.DataBind();
            }

            lblProductListRecords.Text = "Records[" + _dsBomLineToU.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindPostedProducts(string bomNo, int bomPid, int productScreenId)
    {
        try
        {
            _dsPostedBomLineToU = objProjM.GetPostedProductListByBOMPid(bomNo, bomPid);

            if (productScreenId == (int)BOMAllTypeEnums.EnumProductScreenType.ViewScreen)
            {
                if (_dsPostedBomLineToU.Tables.Count > 0 && _dsPostedBomLineToU.Tables[0].Rows.Count > 0)
                {
                    gvPostedProductsList.DataSource = _dsPostedBomLineToU.Tables[0];
                    gvPostedProductsList.DataBind();
                }
                else
                {
                    gvPostedProductsList.DataSource = null;
                    gvPostedProductsList.DataBind();
                }
                lblPostedProductRecors.Text = "Product Records[" + _dsPostedBomLineToU.Tables[0].Rows.Count + "]";

            }
            else if (productScreenId == (int)BOMAllTypeEnums.EnumProductScreenType.ApproveScreen)
            {
                if (_dsPostedBomLineToU.Tables.Count > 0 && _dsPostedBomLineToU.Tables[0].Rows.Count > 0)
                {
                    gvProductList.DataSource = _dsPostedBomLineToU.Tables[0];
                    gvProductList.DataBind();
                }
                else
                {
                    gvProductList.DataSource = null;
                    gvProductList.DataBind();
                }

                lblProductListRecords.Text = "Product Records[" + _dsPostedBomLineToU.Tables[0].Rows.Count + "]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void ToCSVNew01(DataTable dt, string fName)
    {
        try
        {

            string csv = string.Empty;
            foreach (DataColumn column in dt.Columns)
            {
                csv += column.ColumnName + ',';
            }
            csv += "\r\n";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = fName + "_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
            return;
        }
    }


    private void GetAndBindEstimatedCost()
    {
        try
        {
            mpeAddUpdate.Show();
            double totalAmount = 0;

            if (gvProductList.Rows.Count > 0)
            {

                //txtEstimatedCostToU.Text = "00";
                foreach (GridViewRow gvr in gvProductList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                    TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
                    if (chkSelect.Checked)
                    {
                        totalAmount += Convert.ToDouble(txtAmount.Text);

                        for (int i = 0; i < gvr.Cells.Count; i++)
                        {
                            gvr.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gvr.Cells.Count; i++)
                        {
                            gvr.Cells[i].BackColor = System.Drawing.Color.Transparent;
                        }
                    }
                }
            }

            txtBudgetedCostToU.Text = totalAmount.ToString();
            txtEstimatedCostToU.Text = totalAmount.ToString();
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void InsertUpdateBOM()
    {
        try
        {
            int savedId = 0;
            string savedMrNo = "";

            if (_dtBomLineSelectedToU.Columns.Count == 0)
            {
                _dtBomLineSelectedToU.Columns.Add("PID", typeof(int));
                _dtBomLineSelectedToU.Columns.Add("PRODUCT_CODE", typeof(string));
                _dtBomLineSelectedToU.Columns.Add("PRODUCT_DESCRIPTION", typeof(string));
                _dtBomLineSelectedToU.Columns.Add("ADDITIONAL_DESCRIPTION", typeof(string));
                _dtBomLineSelectedToU.Columns.Add("UOM", typeof(string));
                _dtBomLineSelectedToU.Columns.Add("QUANTITY", typeof(double));
                _dtBomLineSelectedToU.Columns.Add("AMOUNT", typeof(double));
                _dtBomLineSelectedToU.Columns.Add("IS_REMOVED", typeof(int));
            }


            if (gvProductList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvProductList.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;

                    Label lblLPID = gr.FindControl("lblLPID") as Label;
                    Label lblProductCode = gr.FindControl("lblProductCode") as Label;
                    Label lblProductDescription = gr.FindControl("lblProductDescription") as Label;
                    Label lblAdditionalDescription = gr.FindControl("lblAdditionalDescription") as Label;
                    Label lblUom = gr.FindControl("lblUom") as Label;
                    Label lblRemainingQuantity = gr.FindControl("lblRemainingQuantity") as Label;
                    Label lblRemainingAmount = gr.FindControl("lblRemainingAmount") as Label;
                    DropDownList ddlQuantity = gr.FindControl("ddlQuantity") as DropDownList;
                    TextBox txtAmount = gr.FindControl("txtAmount") as TextBox;


                    DataRow drN = _dtBomLineSelectedToU.NewRow();

                    //if (Convert.ToInt32(ViewState["StatusId"]) != (int)BOMAllTypeEnums.EnumStatus.New)

                    if (Convert.ToInt32(ViewState["ActionId"]) != (int)BOMAllTypeEnums.EnumAction.Create)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(lblLPID.Text)))
                            drN["PID"] = Convert.ToString(lblLPID.Text);
                        else drN["PID"] = 0;
                    }
                    else drN["PID"] = 0;

                    //if (!string.IsNullOrEmpty(Convert.ToString(lblLPID.Text)))
                    //    drN["PID"] = Convert.ToString(lblLPID.Text);
                    //else drN["PID"] = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text)))
                        drN["PRODUCT_CODE"] = Convert.ToString(lblProductCode.Text);
                    else drN["PRODUCT_CODE"] = "";

                    if (!string.IsNullOrEmpty(Convert.ToString(lblProductDescription.Text)))
                        drN["PRODUCT_DESCRIPTION"] = Convert.ToString(lblProductDescription.Text);
                    else drN["PRODUCT_DESCRIPTION"] = "";

                    if (!string.IsNullOrEmpty(Convert.ToString(lblAdditionalDescription.Text)))
                        drN["ADDITIONAL_DESCRIPTION"] = Convert.ToString(lblAdditionalDescription.Text);
                    else drN["ADDITIONAL_DESCRIPTION"] = "";

                    if (!string.IsNullOrEmpty(Convert.ToString(lblUom.Text)))
                        drN["UOM"] = Convert.ToString(lblUom.Text);
                    else drN["UOM"] = "";

                    //if (!string.IsNullOrEmpty(Convert.ToString(lblRemainingQuantity.Text)))
                    //    drN["QUANTITY"] = Convert.ToDouble(lblRemainingQuantity.Text);
                    //else drN["QUANTITY"] = 0;

                    //if (!string.IsNullOrEmpty(Convert.ToString(lblRemainingAmount.Text)))
                    //    drN["AMOUNT"] = Convert.ToDouble(lblRemainingAmount.Text);
                    //else drN["AMOUNT"] = 0;

                    drN["QUANTITY"] = Convert.ToDouble(ddlQuantity.SelectedValue);

                    if (!string.IsNullOrEmpty(Convert.ToString(txtAmount.Text)))
                        drN["AMOUNT"] = Convert.ToDouble(txtAmount.Text);
                    else drN["AMOUNT"] = 0;

                    if (chkSelect.Checked)
                    {
                        drN["IS_REMOVED"] = 0;

                        _dtBomLineSelectedToU.Rows.Add(drN);
                    }
                    else
                    {
                        if (Convert.ToInt32(lblLPID.Text) > 0)
                        {
                            drN["IS_REMOVED"] = 1;
                            _dtBomLineSelectedToU.Rows.Add(drN);
                        }
                        else drN["IS_REMOVED"] = 0;

                    }
                }
            }

            if (_dtBomLineSelectedToU.Rows.Count == 0)
            {
                mpeAddUpdate.Show();
                ExceptionUpdateMessage("Please select atleast 1 product...");
                return;
            }



            string savedValue = "";

            DataSet dsJobApprovers = new DataSet();
            dsJobApprovers = objProject.GetJOBApprovers(txtJobNoToU.Text, Convert.ToInt32(ddlUnitToU.SelectedValue), "");
            if (dsJobApprovers.Tables.Count > 0 && dsJobApprovers.Tables[0].Rows.Count > 0)
            {
                savedValue = objProjM.InsertUpdateBOM
                (PidToU
                , UnitFidToU
                , TypeFidToU
                , MrNoToU
                , MrDateToU
                , StatusFidToU
                , BomNoToU
                , BomDateToU
                , JobNoToU
                , DeliveryRequiredByToU
                , AcceptableVendor1ToU
                , AcceptableVendor2ToU
                , AcceptableVendor3ToU
                , AcceptableVendor4ToU
                , AcceptableVendor5ToU
                , RevisionNumberToU
                , BudgetedCostToU
                , EstimatedCostToU
                , CostRelatedRemarksToU
                , PivotGroupFidToU
                , ResponsibleForBomFidToU
                , IsTcRequiredToU
                , CreatedByFidToU
                , CreatedRemarksToU
                , IsSentForApprovalToU
                , _dtBomLineSelectedToU
                );
            }
            else
            {
                mpeAddUpdate.Show();
                mpeAddApprovers.Show();
                iframeAddApprovers.Attributes.Add("src", "UpdateLOTApprover.aspx?jobNo=" + txtJobNoToU.Text + "&actID=0&unitid=" + Convert.ToString(ddlUnitToU.SelectedValue) + "");
            }


            if (!string.IsNullOrEmpty(savedValue))
            {
                savedId = Convert.ToInt32(savedValue.Split(':')[0]);
                savedMrNo = Convert.ToString(savedValue.Split(':')[1]);

                if (savedId > 0)
                {
                    if (rdSavingType.SelectedIndex > 0)
                    {
                        BOMSendMail objBOMSendMail = new BOMSendMail(savedId);
                        bool sentMailValue = objBOMSendMail.InitializeSendMail();

                        if (sentMailValue)
                        {
                            int mailStatusVal = objProjM.UpdateMailStatus(savedId, StatusFidToU);
                            SuccessMessage("BOM saved with Mr No.'" + savedMrNo + "' saved and mail sent successfully.");
                        }
                        else
                            SuccessMessage("BOM saved with Mr No.'" + savedMrNo + "' saved successfully.");
                    }
                    else
                    {
                        SuccessMessage("BOM saved with Mr No.'" + savedMrNo + "' saved successfully.");
                    }
                }
            }

            GetPostedBOMList();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateStatus()
    {
        try
        {
            int retVal = objProjM.UpdateStatus(PidToU, StatusFidToU, RemarksToU, CurrentUserId);

            if (retVal > 0)
            {
                BOMSendMail objBOMSendMail = new BOMSendMail(PidToU);
                bool sentMailValue = objBOMSendMail.InitializeSendMail();

                if (sentMailValue)
                {
                    int mailStatusVal = objProjM.UpdateMailStatus(PidToU, StatusFidToU);
                    SuccessMessage("BOM updted with Mr No.: " + MrNoToU + " updated and mail sent successfully.");
                }
                else
                    SuccessMessage("BOM updted with Mr No.: " + MrNoToU + " updated successfully.");
            }

            GetPostedBOMList();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ResetBOMLine()
    {
        try
        {
            txtBomDateToU.Text = "";
            txtBomNoToU.Text = "";
            txtJobNoToU.Text = "";

            gvProductList.DataSource = null;
            gvProductList.DataBind();
            lblProductListRecords.Text = "Products Records[0]";

            ddlUnitToU.SelectedIndex = 0;
            ddlTypeToU.SelectedIndex = 0;

            txtMrDateToU.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            hdMrDateToU.Value = txtMrDateToU.Text;

            txtDeliveryRequiredByToU.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            hdDeliveryRequiredByToU.Value = txtDeliveryRequiredByToU.Text;

            txtAcceptableVendor1ToU.Text = "";
            txtAcceptableVendor2ToU.Text = "";
            txtAcceptableVendor3ToU.Text = "";
            txtAcceptableVendor4ToU.Text = "";
            txtAcceptableVendor5ToU.Text = "";

            txtRevisionNoToU.Text = "00";
            txtBudgetedCostToU.Text = "";
            txtEstimatedCostToU.Text = "";
            txtCostRelatedRemarksToU.Text = "";

            ddlPivotGroupToU.SelectedIndex = 0;
            ddlResponsibleForBOMToU.SelectedIndex = 0;
            txtRemarksToU.Text = "";

            chkIsTCRequiredToU.Checked = false;
            rdSavingType.SelectedIndex = 0;

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            hdBomStartDateSearchToS.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
            txtBomStartDateSearchToS.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

            hdBomEndDateSearchToS.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
            txtBomEndDateSearchToS.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

            txtBomNoToS.Text = "";
            txtJobNoToS.Text = "";


            gvBomList.DataSource = null;
            gvBomList.DataBind();
            lblBOMListRecords.Text = "BOM Records[0]";



        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void DisableBOMLine()
    {
        try
        {
            btnGetBomDetailList.Visible = false;
            txtBomDateToU.Enabled = false;
            txtBomNoToU.Enabled = false;
            txtJobNoToU.Enabled = false;

            ddlUnitToU.Enabled = false;
            ddlTypeToU.Enabled = false;

            txtMrDateToU.Enabled = false;
            imgbtnMrDateToU.Visible = false;

            txtDeliveryRequiredByToU.Enabled = false;
            imgbtnDeliveryRequiredByToU.Visible = false;

            txtAcceptableVendor1ToU.Enabled = false;
            txtAcceptableVendor2ToU.Enabled = false;
            txtAcceptableVendor3ToU.Enabled = false;
            txtAcceptableVendor4ToU.Enabled = false;
            txtAcceptableVendor5ToU.Enabled = false;

            txtRevisionNoToU.Enabled = false;
            txtBudgetedCostToU.Enabled = false;
            txtEstimatedCostToU.Enabled = false;
            txtCostRelatedRemarksToU.Enabled = false;

            ddlPivotGroupToU.Enabled = false;
            ddlResponsibleForBOMToU.Enabled = false;

            txtCreatedRemarksToU.Enabled = false;
            txtAmendmentRemarksToU.Enabled = false;
            txtEditAmendedRemarksToU.Enabled = false;


            chkIsTCRequiredToU.Enabled = false;
            rdSavingType.Enabled = false;

            txtBomStartDateSearchToS.Enabled = false;
            txtBomEndDateSearchToS.Enabled = false;

            txtJobNoToS.Enabled = false;
            txtBomNoToS.Enabled = false;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void EnableBOMLine()
    {
        try
        {
            pnlSavingTypeToU.Visible = true;

            btnGetBomDetailList.Enabled = true;
            txtBomDateToU.Enabled = true;
            txtBomNoToU.Enabled = true;
            txtJobNoToU.Enabled = true;

            ddlUnitToU.Enabled = true;
            ddlTypeToU.Enabled = true;

            txtMrDateToU.Enabled = true;
            txtDeliveryRequiredByToU.Enabled = true;

            txtAcceptableVendor1ToU.Enabled = true;
            txtAcceptableVendor2ToU.Enabled = true;
            txtAcceptableVendor3ToU.Enabled = true;
            txtAcceptableVendor4ToU.Enabled = true;
            txtAcceptableVendor5ToU.Enabled = true;

            txtRevisionNoToU.Enabled = true;
            txtBudgetedCostToU.Enabled = true;
            txtEstimatedCostToU.Enabled = true;
            txtCostRelatedRemarksToU.Enabled = true;

            ddlPivotGroupToU.Enabled = true;
            ddlResponsibleForBOMToU.Enabled = true;

            chkIsTCRequiredToU.Enabled = true;
            rdSavingType.Enabled = true;

            txtBomStartDateSearchToS.Enabled = true;
            txtBomEndDateSearchToS.Enabled = true;

            txtJobNoToS.Enabled = true;
            txtBomNoToS.Enabled = true;
        }
        catch (Exception ex)
        {

            throw;
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

    private void ExceptionUpdateMessage(string message)
    {
        pnlUpdateMsg.Visible = true;
        lblUpdateMsg.Text = message;
        lblUpdateMsg.ForeColor = System.Drawing.Color.Red;
    }


    #endregion

}



