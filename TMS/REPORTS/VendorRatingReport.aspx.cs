using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class REPORTS_VendorRatingReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Reports objReports = new BAL.Reports();
    DataSet dsVendorRating = new DataSet();

    int _unitId = 0;

    string _dateType = "";
    string _startDate = "";
    string _endDate = "";

    string _vendorCode = "";
    string _vendorName = "";

    //string _noOfCountsSign = "";
    //double _noOfCounts1 = 0;
    //double _noOfCounts2 = 0;

    string _noOfCountsForDeliverySign = "";
    double _noOfCountsForDelivery1 = 0;
    double _noOfCountsForDelivery2 = 0;

    string _noOfCountsForQualitySign = "";
    double _noOfCountsForQuality1 = 0;
    double _noOfCountsForQuality2 = 0;

    string _noOfRejectedDeliveriesSign = "";
    double _noOfRejectedDeliveries1 = 0;
    double _noOfRejectedDeliveries2 = 0;

    string _qualitySign = "";
    double _quality1 = 0;
    double _quality2 = 0;

    string _deliverySign = "";
    double _delivery1 = 0;
    double _delivery2 = 0;

    string _noOfDelaysSign = "";
    double _noOfDelays1 = 0;
    double _noOfDelays2 = 0;

    string _commulativeSign = "";
    double _commulative1 = 0;
    double _commulative2 = 0;

    public int UnitId
    {
        get
        {
            return _unitId;
        }

        set
        {
            _unitId = value;
        }
    }

    public string VendorCode
    {
        get
        {
            if (!string.IsNullOrEmpty(txtVendorCode.Text))
                _vendorCode = Convert.ToString(txtVendorCode.Text);
            else _vendorCode = "";

            return _vendorCode;
        }

        set
        {
            _vendorCode = value;
        }
    }

    public string VendorName
    {
        get
        {
            return _vendorName;
        }

        set
        {
            _vendorName = value;
        }
    }

    public string SignNoOfCountsForDelivery
    {
        get
        {
            return _noOfCountsForDeliverySign;
        }

        set
        {
            _noOfCountsForDeliverySign = value;
        }
    }

    public double NoOfCountsForDelivery1
    {
        get
        {
            return _noOfCountsForDelivery1;
        }

        set
        {
            _noOfCountsForDelivery1 = value;
        }
    }

    public string SignNoOfRejectedDeliveries
    {
        get
        {
            return _noOfRejectedDeliveriesSign;
        }

        set
        {
            _noOfRejectedDeliveriesSign = value;
        }
    }

    public double NoOfRejectedDeliveries1
    {
        get
        {
            return _noOfRejectedDeliveries1;
        }

        set
        {
            _noOfRejectedDeliveries1 = value;
        }
    }

    public string SignQuality
    {
        get
        {
            return _qualitySign;
        }

        set
        {
            _qualitySign = value;
        }
    }

    public double Quality1
    {
        get
        {
            return _quality1;
        }

        set
        {
            _quality1 = value;
        }
    }

    public string SignDelivery
    {
        get
        {
            return _deliverySign;
        }

        set
        {
            _deliverySign = value;
        }
    }

    public double Delivery1
    {
        get
        {
            return _delivery1;
        }

        set
        {
            _delivery1 = value;
        }
    }

    public string SignNoOfDelays
    {
        get
        {
            return _noOfDelaysSign;
        }

        set
        {
            _noOfDelaysSign = value;
        }
    }

    public double NoOfDelays1
    {
        get
        {
            return _noOfDelays1;
        }

        set
        {
            _noOfDelays1 = value;
        }
    }

    public string SignCommulative
    {
        get
        {
            return _commulativeSign;
        }

        set
        {
            _commulativeSign = value;
        }
    }

    public double Commulative1
    {
        get
        {
            return _commulative1;
        }

        set
        {
            _commulative1 = value;
        }
    }

    public double NoOfCountsForDelivery2
    {
        get
        {
            return _noOfCountsForDelivery2;
        }

        set
        {
            _noOfCountsForDelivery2 = value;
        }
    }

    public double NoOfRejectedDeliveries2
    {
        get
        {
            return _noOfRejectedDeliveries2;
        }

        set
        {
            _noOfRejectedDeliveries2 = value;
        }
    }

    public double Quality2
    {
        get
        {
            return _quality2;
        }

        set
        {
            _quality2 = value;
        }
    }

    public double Delivery2
    {
        get
        {
            return _delivery2;
        }

        set
        {
            _delivery2 = value;
        }
    }

    public double NoOfDelays2
    {
        get
        {
            return _noOfDelays2;
        }

        set
        {
            _noOfDelays2 = value;
        }
    }

    public double Commulative2
    {
        get
        {
            return _commulative2;
        }

        set
        {
            _commulative2 = value;
        }
    }

    public string StartDate
    {
        get
        {
            return _startDate;
        }

        set
        {
            _startDate = value;
        }
    }

    public string EndDate
    {
        get
        {
            return _endDate;
        }

        set
        {
            _endDate = value;
        }
    }

    public string DateType
    {
        get
        {
            return _dateType;
        }

        set
        {
            _dateType = value;
        }
    }

    public string SignNoOfCountsForQuality
    {
        get
        {
            return _noOfCountsForQualitySign;
        }

        set
        {
            _noOfCountsForQualitySign = value;
        }
    }

    public double NoOfCountsForQuality1
    {
        get
        {
            return _noOfCountsForQuality1;
        }

        set
        {
            _noOfCountsForQuality1 = value;
        }
    }

    public double NoOfCountsForQuality2
    {
        get
        {
            return _noOfCountsForQuality2;
        }

        set
        {
            _noOfCountsForQuality2 = value;
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
                Session["MRNReport"] = null;
                Session["Vendor_Rating"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                ddlDateType.SelectedIndex = 1;

                GetVendorRatingReport();
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
        GetVendorRatingReport();
    }

    protected void gvVendorRating_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvVendorRating_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "VIEW_PRODUCT_LIST")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblUnit = gvVendorRating.Rows[rowindex].FindControl("lblUnit") as Label;
                Label lblVendorCode = gvVendorRating.Rows[rowindex].FindControl("lblVendorCode") as Label;

                Session["MRNReport"] = null;
                if (Convert.ToString(e.CommandArgument) == "VIEW_PRODUCT_LIST")
                {
                    txtMRNStartDateToS.Text = hdStartDateSearch.Value;
                    txtMRNEndDateToS.Text = hdEndDateSearch.Value;
                    txtUnitToS.Text = lblUnit.Text;
                    txtVendorCodeToS.Text = lblVendorCode.Text;

                    BindMRNDetails(
                          Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd")
                        , Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd")
                        , Convert.ToString(lblUnit.Text)
                        , Convert.ToString(lblVendorCode.Text)
                        );

                    mpeMRNDetail.Show();
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

    protected void gvMRNDetailReport_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (gvVendorRating.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["Vendor_Rating"];
            ToCSVNew01(dt, "Vendor_Rating_Report");
        }
    }

    protected void btnExportMrnDetail_Click(object sender, EventArgs e)
    {
        if (gvMRNDetailReport.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["MRNReport"];
            ToCSVNew01(dt, "MRN_Detail_Report");
        }
    }

    #endregion


    #region METHODS[=======================]


    private void GetVendorRatingReport()
    {
        try
        {
            UnitId = Convert.ToInt32(ddlUnit.SelectedValue);

            DateType = ddlDateType.SelectedValue;

            if (chkSelectDates.Checked)
            {
                StartDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
                EndDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            }

            VendorCode = Convert.ToString(txtVendorCode.Text);
            VendorName = Convert.ToString(txtVendorName.Text);

            SignNoOfCountsForDelivery = ddlSignNoOfCountsForDelivery.SelectedValue;
            if (!string.IsNullOrEmpty(txtNoOfCountsForDelivery1.Text))
                NoOfCountsForDelivery1 = Convert.ToDouble(txtNoOfCountsForDelivery1.Text);

            if (!string.IsNullOrEmpty(txtNoOfCountsForDelivery2.Text))
                NoOfCountsForDelivery2 = Convert.ToDouble(txtNoOfCountsForDelivery2.Text);


            SignNoOfCountsForQuality = ddlSignNoOfCountsForQuality.SelectedValue;
            if (!string.IsNullOrEmpty(txtNoOfCountsForQuality1.Text))
                NoOfCountsForQuality1 = Convert.ToDouble(txtNoOfCountsForQuality1.Text);

            if (!string.IsNullOrEmpty(txtNoOfCountsForQuality2.Text))
                NoOfCountsForQuality2 = Convert.ToDouble(txtNoOfCountsForQuality2.Text);


            SignNoOfRejectedDeliveries = ddlSignNoOfRejectedDeliveries.SelectedValue;
            if (!string.IsNullOrEmpty(txtNoOfRejectedDeliveries1.Text))
                NoOfRejectedDeliveries1 = Convert.ToDouble(txtNoOfRejectedDeliveries1.Text);

            if (!string.IsNullOrEmpty(txtNoOfRejectedDeliveries2.Text))
                NoOfRejectedDeliveries2 = Convert.ToDouble(txtNoOfRejectedDeliveries2.Text);


            SignQuality = ddlSignQuality.SelectedValue;
            if (!string.IsNullOrEmpty(txtQuality1.Text))
                Quality1 = Convert.ToDouble(txtQuality1.Text);

            if (!string.IsNullOrEmpty(txtQuality2.Text))
                Quality2 = Convert.ToDouble(txtQuality2.Text);


            SignDelivery = ddlSignDelivery.SelectedValue;
            if (!string.IsNullOrEmpty(txtDelivery1.Text))
                Delivery1 = Convert.ToDouble(txtDelivery1.Text);

            if (!string.IsNullOrEmpty(txtDelivery2.Text))
                Delivery2 = Convert.ToDouble(txtDelivery2.Text);


            SignNoOfDelays = ddlSignNoOfDelays.SelectedValue;
            if (!string.IsNullOrEmpty(txtNoOfDelays1.Text))
                NoOfDelays1 = Convert.ToDouble(txtNoOfDelays1.Text);

            if (!string.IsNullOrEmpty(txtNoOfDelays2.Text))
                NoOfDelays2 = Convert.ToDouble(txtNoOfDelays2.Text);


            SignCommulative = ddlSignCommulative.SelectedValue;
            if (!string.IsNullOrEmpty(txtCommulative1.Text))
                Commulative1 = Convert.ToDouble(txtCommulative1.Text);

            if (!string.IsNullOrEmpty(txtCommulative2.Text))
                Commulative2 = Convert.ToDouble(txtCommulative2.Text);


            dsVendorRating = objReports.GetVendorRatingReport
                (
                  UnitId
                , DateType
                , StartDate
                , EndDate
                , VendorCode
                , VendorName

                , SignNoOfCountsForDelivery
                , NoOfCountsForDelivery1
                , NoOfCountsForDelivery2

                , SignNoOfCountsForQuality
                , NoOfCountsForQuality1
                , NoOfCountsForQuality2

                , SignNoOfRejectedDeliveries
                , NoOfRejectedDeliveries1
                , NoOfRejectedDeliveries2
                , SignQuality
                , Quality1
                , Quality2
                , SignDelivery
                , Delivery1
                , Delivery2
                , SignNoOfDelays
                , NoOfDelays1
                , NoOfDelays2
                , SignCommulative
                , Commulative1
                , Commulative2
                );
            if (dsVendorRating.Tables.Count > 0 && dsVendorRating.Tables[0].Rows.Count > 0)
            {
                Session["Vendor_Rating"] = dsVendorRating.Tables[0];
                gvVendorRating.DataSource = dsVendorRating.Tables[0];
                gvVendorRating.DataBind();
            }
            else
            {
                Session["Vendor_Rating"] = null;
                gvVendorRating.DataSource = null;
                gvVendorRating.DataBind();
            }

            lblRecords.Text = "Records[" + dsVendorRating.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    DataSet _dsMrnDetails = new DataSet();

    private void BindMRNDetails(string fromDate, string toDate, string unitName, string vendorCode)
    {
        try
        {
            _dsMrnDetails = objReports.GetMRNDetailReport(fromDate, toDate, vendorCode, unitName);

            if (_dsMrnDetails.Tables.Count > 0 && _dsMrnDetails.Tables[0].Rows.Count > 0)
            {
                Session["MRNReport"] = _dsMrnDetails.Tables[0];
                gvMRNDetailReport.DataSource = _dsMrnDetails.Tables[0];
                gvMRNDetailReport.DataBind();

                DataView view = new DataView(_dsMrnDetails.Tables[0]);
                DataTable distinctValues = view.ToTable(true, "Mrn_No");

                txtMrnCountsToS.Text = distinctValues.Rows.Count.ToString();
            }
            else
            {
                Session["MRNReport"] = null;
                gvMRNDetailReport.DataSource = null;
                gvMRNDetailReport.DataBind();
            }

            lblMRNDetailRecors.Text = "MRN Detailed Records[" + _dsMrnDetails.Tables[0].Rows.Count + "]";
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
