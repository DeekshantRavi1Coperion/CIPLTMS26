using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class REPORTS_SALE_ORDER_AddTaxBill : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsJobNoInvoiceList = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string jobNo = string.Empty;
    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                //
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        //
    }

    #endregion


    #region METHODS[=======================]



    #endregion

    protected void btnAddTaxBill_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}