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

public partial class ORDER_REGISTRATION_OrderList : System.Web.UI.Page
{

    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                GetOrderList();
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
        GetOrderList();
    }

    protected void gvOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "REVISE" || e.CommandArgument == "ViewDETAIL")//|| e.CommandArgument == "SEND_MAIL"
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblOrderID = gvOrderList.Rows[rowindex].FindControl("lblOrderID") as Label;
                if (e.CommandArgument == "REVISE")
                {
                    ModalPopupExtender1.Show();
                    iframeRevise.Attributes["src"] = "ReviseOrderNew.aspx?orderid=" + Convert.ToString(lblOrderID.Text);
                }

                else if (e.CommandArgument == "ViewDETAIL")
                {
                    ModalPopupExtender4.Show();
                    iframeViewTourInformationInPDF.Attributes.Add("src", "OrderDetailPDF.aspx?orderID=" + Convert.ToString(lblOrderID.Text));
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

    protected void gvOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }


    protected void btnAddNewOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ORDER_REGISTRATION/OrderRegistration.aspx?");
    }

    #endregion


    #region METHODS[=======================]

    private void GetOrderList()
    {
        try
        {
            BAL.Order objOrder = new BAL.Order();
            DataSet dsOrderList = new DataSet();
            string startDate = string.Empty;
            string endDate = string.Empty;
            string orderNo = string.Empty;
            int statusID = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;


            if (!string.IsNullOrEmpty(txtOrderNo.Text))
                orderNo = txtOrderNo.Text.Trim();
            else
                orderNo = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusID = 0;


            dsOrderList = objOrder.GetOrderList(startDate, endDate, orderNo, statusID);
            if (dsOrderList.Tables.Count > 0 && dsOrderList.Tables[0].Rows.Count > 0)
            {
                //Session["TOUR_LIST"] = dsOrderList.Tables[0];
                gvOrderList.DataSource = dsOrderList.Tables[0];
                gvOrderList.DataBind();
            }
            else
            {
                //Session["TOUR_LIST"] = null;
                gvOrderList.DataSource = null;
                gvOrderList.DataBind();
            }
            lblRecords.Text = "Records[" + dsOrderList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
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
