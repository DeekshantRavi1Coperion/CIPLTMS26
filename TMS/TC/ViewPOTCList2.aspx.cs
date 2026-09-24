using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class TC_ViewPOTCList2 : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.TC objTC = new BAL.TC();
    BAL.Common objCommon = new BAL.Common();
    TCSendMail objTCSendMail = new TCSendMail();

    DataSet dsPOList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsStatus = new DataSet();
    DataSet dsDBDetails = new DataSet();
    DataSet dsEmployee = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;

    int recordID = 0;
    int statusID = 0;
    string poNo = string.Empty;
    string poDate = string.Empty;
    string vendorCode = string.Empty;
    string vendorName = string.Empty;
    string JOBNo = string.Empty;
    int unitID = 0;
    string itemCode = string.Empty;
    string itemName = string.Empty;
    string remarks = string.Empty;
    int employeeRecordID = 0;
    int excludeCIDF = 0;
    int excludeEngineeringService = 0;

    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    Byte[] TC1FileBytes = null;
    Byte[] TC2FileBytes = null;
    Byte[] TC3FileBytes = null;

    string TC1File = string.Empty;
    string TC2File = string.Empty;
    string TC3File = string.Empty;


    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdAcceptConfirmValue.Value = "0";
                hdNotAcceptConfirmValue.Value = "0";
                hdEditConfirmValue.Value = "0";

                Session["dtPODetailList"] = null;
                Session["dtPOList"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindUnit();
                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                BindStatus();

                BindEmployeeToUpload();



                GetPOList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetPOList();
    }

    protected void gvPOList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");

                Label lblTCAttachment1 = (Label)e.Row.FindControl("lblTCAttachment1");
                Label lblTCAttachment2 = (Label)e.Row.FindControl("lblTCAttachment2");
                Label lblTCAttachment3 = (Label)e.Row.FindControl("lblTCAttachment3");

                Label lblEmployeeRecordID = (Label)e.Row.FindControl("lblEmployeeRecordID");
                Label lblCreatedByID = (Label)e.Row.FindControl("lblCreatedByID");

                Label lblAcceptedBy = (Label)e.Row.FindControl("lblAcceptedBy");
                Label lblNotAcceptedBy = (Label)e.Row.FindControl("lblNotAcceptedBy");

                Label lblIsAcceptedMailSent = (Label)e.Row.FindControl("lblIsAcceptedMailSent");
                Label lblIsNotAcceptedMailSent = (Label)e.Row.FindControl("lblIsNotAcceptedMailSent");



                Button btnAccept = (Button)e.Row.FindControl("btnAccept");
                Button btnEdit = (Button)e.Row.FindControl("btnEdit");

                ImageButton imgBtnTCAttachment1 = (ImageButton)e.Row.FindControl("imgBtnTCAttachment1");
                ImageButton imgBtnTCAttachment2 = (ImageButton)e.Row.FindControl("imgBtnTCAttachment2");
                ImageButton imgBtnTCAttachment3 = (ImageButton)e.Row.FindControl("imgBtnTCAttachment3");
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                ImageButton imgBtnSendMail = (ImageButton)e.Row.FindControl("imgBtnSendMail");

                imgBtnTCAttachment1.Visible = false;
                imgBtnTCAttachment2.Visible = false;
                imgBtnTCAttachment3.Visible = false;

                btnAccept.Visible = false;
                btnEdit.Visible = false;
                imgStatus.Visible = false;
                imgBtnSendMail.Visible = false;

                if (!string.IsNullOrEmpty(Convert.ToString(lblTCAttachment1.Text)))
                {
                    imgBtnTCAttachment1.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblTCAttachment2.Text)))
                {
                    imgBtnTCAttachment2.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblTCAttachment3.Text)))
                {
                    imgBtnTCAttachment3.Visible = true;
                }

                if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Open))
                {
                    if (Convert.ToInt32(lblEmployeeRecordID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                    {
                        btnAccept.Visible = true;
                    }

                    if (Convert.ToInt32(lblCreatedByID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                    {
                        btnEdit.Visible = true;
                    }

                    imgStatus.Visible = true;
                    imgStatus.ImageUrl = "~/Images/NEWICONS/blackopen.png";
                    imgStatus.ToolTip = "Open";
                }
                else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                {
                    imgStatus.Visible = true;
                    imgStatus.ImageUrl = "~/Images/NEWICONS/accepted22.png";
                    imgStatus.ToolTip = "Accepted";

                    if (Convert.ToInt32(lblAcceptedBy.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                    {
                        if (Convert.ToInt32(lblIsAcceptedMailSent.Text) == 0)
                            imgBtnSendMail.Visible = true;
                    }
                }
                else if (Convert.ToInt32(lblStatusID.Text) == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                {
                    if (Convert.ToInt32(lblCreatedByID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                    {
                        btnEdit.Visible = true;
                    }

                    if (Convert.ToInt32(lblNotAcceptedBy.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                    {
                        if (Convert.ToInt32(lblIsNotAcceptedMailSent.Text) == 0)
                            imgBtnSendMail.Visible = true;
                    }

                    imgStatus.Visible = true;
                    imgStatus.ImageUrl = "~/Images/NEWICONS/notaccepted20.png";
                    imgStatus.ToolTip = "Not Accepted";
                }



            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvPOList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                ViewState["RECORD_ID"] = 0;
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "ACCEPT_TC")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }



                Label lblRecordID = gvPOList.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblStatusID = gvPOList.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblEmployeeRecordID = gvPOList.Rows[rowindex].FindControl("lblEmployeeRecordID") as Label;
                Label lblPONo = gvPOList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblPODate = gvPOList.Rows[rowindex].FindControl("lblPODate") as Label;
                Label lblVendorCode = gvPOList.Rows[rowindex].FindControl("lblVendorCode") as Label;
                Label lblVendorName = gvPOList.Rows[rowindex].FindControl("lblVendorName") as Label;
                Label lblJOBNo = gvPOList.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblPOFirstItemName = gvPOList.Rows[rowindex].FindControl("lblPOFirstItemName") as Label;
                Label lblPOFirstItemCode = gvPOList.Rows[rowindex].FindControl("lblPOFirstItemCode") as Label;
                Label lblUnitID = gvPOList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblUnit = gvPOList.Rows[rowindex].FindControl("lblUnit") as Label;


                string attachment1Extn = string.Empty;
                string attachment2Extn = string.Empty;
                string attachment3Extn = string.Empty;


                Label lblTCAttachment1 = gvPOList.Rows[rowindex].FindControl("lblTCAttachment1") as Label;
                Label lblTCAttachment2 = gvPOList.Rows[rowindex].FindControl("lblTCAttachment2") as Label;
                Label lblTCAttachment3 = gvPOList.Rows[rowindex].FindControl("lblTCAttachment3") as Label;

                if (Convert.ToString(e.CommandArgument) == "EDIT")
                {
                    ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);
                    ddlEmployeeToEdit.SelectedValue = Convert.ToString(lblEmployeeRecordID.Text);
                    txtPONoToEdit.Text = Convert.ToString(lblPONo.Text);
                    txtPODateToEdit.Text = Convert.ToString(lblPODate.Text);
                    txtVendorCodeToEdit.Text = Convert.ToString(lblVendorCode.Text);
                    txtVendorNameToEdit.Text = Convert.ToString(lblVendorName.Text);
                    txtJOBNoToEdit.Text = Convert.ToString(lblJOBNo.Text);
                    txtItemCodeToEdit.Text = Convert.ToString(lblPOFirstItemCode.Text);
                    txtItemNameToEdit.Text = Convert.ToString(lblPOFirstItemName.Text);
                    ViewState["UNIT_ID"] = Convert.ToInt32(lblUnitID.Text);
                    txtUnitToEdit.Text = Convert.ToString(lblUnit.Text);

                    txtRemarksToEdit.Text = string.Empty;



                    pnlViewSiDrawingToEdit1.Visible = true;
                    pnlViewSiDrawingToEdit2.Visible = true;
                    pnlViewSiDrawingToEdit3.Visible = true;


                    if (!string.IsNullOrEmpty(lblTCAttachment1.Text))
                    {
                        hdSiDrawingToEdit1.Value = "1";
                        hdUploadSiDrawingToEdit1.Value = "0";

                        pnlViewSiDrawingToEdit1.Visible = true;
                        pnlUploadSiDrawingToEdit1.Visible = false;

                        imgBtnViewSiDrawingToEdit1.Visible = true;

                        attachment1Extn = Convert.ToString(lblTCAttachment1.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" ||
                            attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit1.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit1.Text = lblTCAttachment1.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit1.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit1.Text = lblTCAttachment1.Text;
                        }
                    }
                    else
                    {
                        hdSiDrawingToEdit1.Value = "0";
                        hdUploadSiDrawingToEdit1.Value = "1";

                        pnlViewSiDrawingToEdit1.Visible = false;
                        pnlUploadSiDrawingToEdit1.Visible = true;

                        imgBtnViewSiDrawingToEdit1.Visible = false;
                        imgBtnUndoSiDrawingToEdit1.Visible = false;
                    }



                    if (!string.IsNullOrEmpty(lblTCAttachment2.Text))
                    {
                        hdSiDrawingToEdit2.Value = "1";
                        hdUploadSiDrawingToEdit2.Value = "0";

                        pnlViewSiDrawingToEdit2.Visible = true;
                        pnlUploadSiDrawingToEdit2.Visible = false;

                        imgBtnViewSiDrawingToEdit2.Visible = true;

                        attachment1Extn = Convert.ToString(lblTCAttachment2.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" ||
                            attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit2.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit2.Text = lblTCAttachment2.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit2.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit2.Text = lblTCAttachment2.Text;
                        }
                    }
                    else
                    {
                        hdSiDrawingToEdit2.Value = "0";
                        hdUploadSiDrawingToEdit2.Value = "1";

                        pnlViewSiDrawingToEdit2.Visible = false;
                        pnlUploadSiDrawingToEdit2.Visible = true;

                        imgBtnViewSiDrawingToEdit2.Visible = false;
                        imgBtnUndoSiDrawingToEdit2.Visible = false;
                    }

                    if (!string.IsNullOrEmpty(lblTCAttachment3.Text))
                    {
                        hdSiDrawingToEdit3.Value = "1";
                        hdUploadSiDrawingToEdit3.Value = "0";

                        pnlViewSiDrawingToEdit3.Visible = true;
                        pnlUploadSiDrawingToEdit3.Visible = false;

                        imgBtnViewSiDrawingToEdit3.Visible = true;

                        attachment1Extn = Convert.ToString(lblTCAttachment3.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" ||
                            attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit3.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit3.Text = lblTCAttachment3.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit3.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit3.Text = lblTCAttachment3.Text;
                        }
                    }
                    else
                    {
                        hdSiDrawingToEdit3.Value = "0";
                        hdUploadSiDrawingToEdit3.Value = "1";

                        pnlViewSiDrawingToEdit3.Visible = false;
                        pnlUploadSiDrawingToEdit3.Visible = true;

                        imgBtnViewSiDrawingToEdit3.Visible = false;
                        imgBtnUndoSiDrawingToEdit3.Visible = false;
                    }






                    mpeDetailToEdit.Show();
                }


                if (Convert.ToString(e.CommandArgument) == "ACCEPT_TC")
                {
                    ViewState["RECORD_ID"] = Convert.ToInt32(lblRecordID.Text);
                    ddlEmployeeTUS.SelectedValue = Convert.ToString(lblEmployeeRecordID.Text);
                    txtPONoTUS.Text = Convert.ToString(lblPONo.Text);
                    txtPODateTUS.Text = Convert.ToString(lblPODate.Text);
                    txtVendorCodeTUS.Text = Convert.ToString(lblVendorCode.Text);
                    txtVendorNameTUS.Text = Convert.ToString(lblVendorName.Text);
                    txtJOBNoTUS.Text = Convert.ToString(lblJOBNo.Text);
                    txtItemCodeTUS.Text = Convert.ToString(lblPOFirstItemCode.Text);
                    txtItemNameTUS.Text = Convert.ToString(lblPOFirstItemName.Text);
                    ViewState["UNIT_ID"] = Convert.ToInt32(lblUnitID.Text);
                    txtUnitTUS.Text = Convert.ToString(lblUnit.Text);

                    txtRemarksTUS.Text = string.Empty;


                    pnlViewSiDrawingToEdit1.Visible = true;
                    pnlViewSiDrawingToEdit2.Visible = true;
                    pnlViewSiDrawingToEdit3.Visible = true;


                    if (!string.IsNullOrEmpty(lblTCAttachment1.Text))
                    {
                        hdSiDrawingToEdit1.Value = "1";
                        hdUploadSiDrawingToEdit1.Value = "0";

                        pnlViewSiDrawingToEdit1.Visible = true;
                        pnlUploadSiDrawingToEdit1.Visible = false;

                        imgBtnViewSiDrawingToEdit1.Visible = true;

                        attachment1Extn = Convert.ToString(lblTCAttachment1.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || 
                            attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit1.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit1.Text = lblTCAttachment1.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit1.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit1.Text = lblTCAttachment1.Text;
                        }
                    }
                    else
                    {
                        hdSiDrawingToEdit1.Value = "0";
                        hdUploadSiDrawingToEdit1.Value = "1";

                        pnlViewSiDrawingToEdit1.Visible = false;
                        pnlUploadSiDrawingToEdit1.Visible = true;

                        imgBtnViewSiDrawingToEdit1.Visible = false;
                        imgBtnUndoSiDrawingToEdit1.Visible = false;
                    }



                    if (!string.IsNullOrEmpty(lblTCAttachment2.Text))
                    {
                        hdSiDrawingToEdit2.Value = "1";
                        hdUploadSiDrawingToEdit2.Value = "0";

                        pnlViewSiDrawingToEdit2.Visible = true;
                        pnlUploadSiDrawingToEdit2.Visible = false;

                        imgBtnViewSiDrawingToEdit2.Visible = true;

                        attachment1Extn = Convert.ToString(lblTCAttachment2.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || 
                            attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit2.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit2.Text = lblTCAttachment2.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit2.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit2.Text = lblTCAttachment2.Text;
                        }                        
                    }
                    else
                    {
                        hdSiDrawingToEdit2.Value = "0";
                        hdUploadSiDrawingToEdit2.Value = "1";

                        pnlViewSiDrawingToEdit2.Visible = false;
                        pnlUploadSiDrawingToEdit2.Visible = true;

                        imgBtnViewSiDrawingToEdit2.Visible = false;
                        imgBtnUndoSiDrawingToEdit2.Visible = false;
                    }

                    if (!string.IsNullOrEmpty(lblTCAttachment3.Text))
                    {
                        hdSiDrawingToEdit3.Value = "1";
                        hdUploadSiDrawingToEdit3.Value = "0";

                        pnlViewSiDrawingToEdit3.Visible = true;
                        pnlUploadSiDrawingToEdit3.Visible = false;

                        imgBtnViewSiDrawingToEdit3.Visible = true;

                        attachment1Extn = Convert.ToString(lblTCAttachment3.Text).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || 
                            attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {
                            imgBtnViewSiDrawingToEdit3.ImageUrl = "~/Images/imgicon1.png";
                            txtSiDrawingToEdit3.Text = lblTCAttachment3.Text;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewSiDrawingToEdit3.ImageUrl = "~/Images/pdficon1.png";
                            txtSiDrawingToEdit3.Text = lblTCAttachment3.Text;
                        }
                    }
                    else
                    {
                        hdSiDrawingToEdit3.Value = "0";
                        hdUploadSiDrawingToEdit3.Value = "1";

                        pnlViewSiDrawingToEdit3.Visible = false;
                        pnlUploadSiDrawingToEdit3.Visible = true;

                        imgBtnViewSiDrawingToEdit3.Visible = false;
                        imgBtnUndoSiDrawingToEdit3.Visible = false;
                    }



                    #region MyRegion

                    //if (!string.IsNullOrEmpty(lblTCAttachment1.Text))
                    //{
                    //    btnViewAttachment1.Visible = true;

                    //    attachment1Extn = Convert.ToString(lblTCAttachment1.Text).Split('.').Last();
                    //    if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" ||
                    //        attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                    //    {
                    //        btnViewAttachment1.ImageUrl = "~/Images/imgicon1.png";
                    //        txtViewAttachment1.Text = lblTCAttachment1.Text;
                    //    }
                    //    else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                    //    {
                    //        btnViewAttachment1.ImageUrl = "~/Images/pdficon1.png";
                    //        txtViewAttachment1.Text = lblTCAttachment1.Text;
                    //    }
                    //}
                    //else
                    //{
                    //    btnViewAttachment1.Visible = false;
                    //    txtViewAttachment1.Visible = false;
                    //}


                    //if (!string.IsNullOrEmpty(lblTCAttachment2.Text))
                    //{
                    //    btnViewAttachment2.Visible = true;

                    //    attachment2Extn = Convert.ToString(lblTCAttachment2.Text).Split('.').Last();
                    //    if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" ||
                    //        attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
                    //    {
                    //        btnViewAttachment2.ImageUrl = "~/Images/imgicon1.png";
                    //        txtViewAttachment2.Text = lblTCAttachment2.Text;
                    //    }
                    //    else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
                    //    {
                    //        btnViewAttachment2.ImageUrl = "~/Images/pdficon1.png";
                    //        txtViewAttachment2.Text = lblTCAttachment2.Text;
                    //    }
                    //}
                    //else
                    //{
                    //    btnViewAttachment2.Visible = false;
                    //    txtViewAttachment2.Visible = false;
                    //}


                    //if (!string.IsNullOrEmpty(lblTCAttachment3.Text))
                    //{
                    //    btnViewAttachment3.Visible = true;

                    //    attachment3Extn = Convert.ToString(lblTCAttachment3.Text).Split('.').Last();
                    //    if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" ||
                    //        attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
                    //    {
                    //        btnViewAttachment3.ImageUrl = "~/Images/imgicon1.png";
                    //        txtViewAttachment3.Text = lblTCAttachment3.Text;
                    //    }
                    //    else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
                    //    {
                    //        btnViewAttachment3.ImageUrl = "~/Images/pdficon1.png";
                    //        txtViewAttachment3.Text = lblTCAttachment3.Text;
                    //    }
                    //}
                    //else
                    //{
                    //    btnViewAttachment3.Visible = false;
                    //    txtViewAttachment3.Visible = false;
                    //}

                    #endregion


                    mpeDetailTUS.Show();
                }


                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                    ViewDrawingFiles(Convert.ToInt32(lblRecordID.Text), "DRAWING1", Convert.ToString(lblTCAttachment1.Text).Trim());

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                    ViewDrawingFiles(Convert.ToInt32(lblRecordID.Text), "DRAWING2", Convert.ToString(lblTCAttachment2.Text).Trim());

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                    ViewDrawingFiles(Convert.ToInt32(lblRecordID.Text), "DRAWING3", Convert.ToString(lblTCAttachment3.Text).Trim());




                else if (Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                {
                    recordID = Convert.ToInt32(lblRecordID.Text);
                    statusID = Convert.ToInt32(lblStatusID.Text);
                    poNo = Convert.ToString(lblPONo.Text);

                    //int sendMailValue = objTCSendMail.ProcessAndSendMail(recordID, poNo, statusID);
                    //if (sendMailValue > 0)
                    //{                        
                    //    int mailStatusUpdateValue = objTC.UpdateTCMailStatus(recordID, statusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    //    if (statusID == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                    //    {
                    //        SuccessMessage("TC(s) on PO No. '" + poNo + "' accepted and mail sent successfully...!!!");
                    //    }
                    //    else if (statusID == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                    //    {
                    //        SuccessMessage("TC(s) on PO No. '" + poNo + "' rejected and mail sent successfully...!!!");
                    //    }
                    //}
                    //else
                    //{
                    //    if (statusID == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                    //    {
                    //        SuccessMessage("TC(s) on PO No. '" + poNo + "' accepted successfully...!!!");
                    //    }
                    //    else if (statusID == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                    //    {
                    //        SuccessMessage("TC(s) on PO No. '" + poNo + "' rejected successfully...!!!");
                    //    }
                    //}                    
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




    protected void btnAcceptTC_Click(object sender, EventArgs e)
    {
        UpdateTCStatus(Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted));

        //if (Convert.ToInt32(hdAcceptConfirmValue.Value) > 0)
        //{

        //}
    }

    protected void btnNotAcceptTC_Click(object sender, EventArgs e)
    {
        UpdateTCStatus(Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected));

        //if (Convert.ToInt32(hdNotAcceptConfirmValue.Value) > 0)
        //{

        //}
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPOList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtPOList"];
            ExportToExcel(dt);
        }
    }


    protected void imgBtnViewSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(ViewState["RECORD_ID"]), "DRAWING1", txtSiDrawingToEdit1.Text.Trim());
        mpeDetailTUS.Show();
    }

    protected void imgBtnViewSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(ViewState["RECORD_ID"]), "DRAWING2", txtSiDrawingToEdit1.Text.Trim());
        mpeDetailTUS.Show();
    }

    protected void imgBtnViewSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(ViewState["RECORD_ID"]), "DRAWING3", txtSiDrawingToEdit1.Text.Trim());
        mpeDetailTUS.Show();
    }

    protected void imgBtnRemoveSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit1.Value = "0";
        hdUploadSiDrawingToEdit1.Value = "1";

        pnlViewSiDrawingToEdit1.Visible = false;
        pnlUploadSiDrawingToEdit1.Visible = true;

        mpeDetailTUS.Show();
    }

    protected void imgBtnRemoveSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit2.Value = "0";
        hdUploadSiDrawingToEdit2.Value = "1";

        pnlViewSiDrawingToEdit2.Visible = false;
        pnlUploadSiDrawingToEdit2.Visible = true;

        mpeDetailTUS.Show();
    }

    protected void imgBtnRemoveSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit3.Value = "0";
        hdUploadSiDrawingToEdit3.Value = "1";

        pnlViewSiDrawingToEdit3.Visible = false;
        pnlUploadSiDrawingToEdit3.Visible = true;

        mpeDetailTUS.Show();
    }



    protected void imgBtnUndoSiDrawingToEdit1_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit1.Value = "1";
        hdUploadSiDrawingToEdit1.Value = "0";

        pnlViewSiDrawingToEdit1.Visible = true;
        pnlUploadSiDrawingToEdit1.Visible = false;

        mpeDetailTUS.Show();        
    }

    protected void imgBtnUndoSiDrawingToEdit2_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit2.Value = "1";
        hdUploadSiDrawingToEdit2.Value = "0";

        pnlViewSiDrawingToEdit2.Visible = true;
        pnlUploadSiDrawingToEdit2.Visible = false;

        mpeDetailTUS.Show();
    }

    protected void imgBtnUndoSiDrawingToEdit3_Click(object sender, ImageClickEventArgs e)
    {
        hdSiDrawingToEdit3.Value = "1";
        hdUploadSiDrawingToEdit3.Value = "0";

        pnlViewSiDrawingToEdit3.Visible = true;
        pnlUploadSiDrawingToEdit3.Visible = false;

        mpeDetailTUS.Show();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdEditConfirmValue.Value) > 0)
        {
            UploadTC();
        }
    }

    #endregion


    #region METHODS[=========================]

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatus()
    {
        try
        {
            dsStatus = objTC.GetTCStatusList(0);
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
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

    private void BindEmployeeToUpload()
    {
        try
        {
            dsEmployee = objCommon.GetEmployeeByDepartmentID(13);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "All");
                ddlEmployee.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);


                ddlEmployeeToEdit.DataSource = dsEmployee.Tables[0];
                ddlEmployeeToEdit.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeToEdit.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeToEdit.DataBind();
                ddlEmployeeToEdit.Items.Insert(0, "Select");
                ddlEmployeeToEdit.SelectedIndex = 0;

                ddlEmployeeTUS.DataSource = dsEmployee.Tables[0];
                ddlEmployeeTUS.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeTUS.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeTUS.DataBind();
                ddlEmployeeTUS.Items.Insert(0, "Select");
                ddlEmployeeTUS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetPOList()
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

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusID = 0;

            if (!string.IsNullOrEmpty(txtPONo.Text))
                poNo = txtPONo.Text.ToUpper();
            else
                poNo = string.Empty;

            if (!string.IsNullOrEmpty(txtVendorName.Text))
                vendorName = txtVendorName.Text;
            else
                vendorName = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text.ToUpper();
            else
                JOBNo = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);
            else
                unitID = 0;

            if (!string.IsNullOrEmpty(txtItemName.Text))
                itemName = txtItemName.Text;
            else
                itemName = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                employeeRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                employeeRecordID = 0;

            //dsPOList = objTC.GetPOTCList(fromDate, toDate, statusID, poNo, vendorName, unitID, JOBNo, itemName, employeeRecordID,"");

            if (dsPOList.Tables.Count > 0 && dsPOList.Tables[0].Rows.Count > 0)
            {
                Session["dtPOList"] = dsPOList.Tables[0];
                gvPOList.DataSource = dsPOList.Tables[0];
                gvPOList.DataBind();
            }
            else
            {
                Session["dtPOList"] = null;
                gvPOList.DataSource = null;
                gvPOList.DataBind();
            }
            lblRecords.Text = "Records[" + dsPOList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateTCStatus(int statusID)
    {
        try
        {

            recordID = 0;
            poNo = string.Empty;
            remarks = string.Empty;

            if (Convert.ToInt32(ViewState["RECORD_ID"]) > 0)
                recordID = Convert.ToInt32(ViewState["RECORD_ID"]);

            if (!string.IsNullOrEmpty(txtRemarksTUS.Text))
                remarks = txtRemarksTUS.Text;

            if (!string.IsNullOrEmpty(txtPONoTUS.Text))
                poNo = txtPONoTUS.Text.ToUpper();


            int value = objTC.UpdateTCStatus(recordID, statusID, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                //int sendMailValue = objTCSendMail.ProcessAndSendMail(recordID, poNo, statusID);
                //if (sendMailValue > 0)
                //{
                //    int mailStatusUpdateValue = objTC.UpdateTCMailStatus(recordID, statusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                //    if (statusID == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                //    {
                //        SuccessMessage("TC(s) on PO No. '" + poNo + "' accepted and mail sent successfully...!!!");
                //    }
                //    else if (statusID == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                //    {
                //        SuccessMessage("TC(s) on PO No. '" + poNo + "' rejected and mail sent successfully...!!!");
                //    }
                //}
                //else
                //{
                //    if (statusID == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Accepted))
                //    {
                //        SuccessMessage("TC(s) on PO No. '" + poNo + "' accepted successfully...!!!");
                //    }
                //    else if (statusID == Convert.ToInt32(TCAllStatusAndTypes.EnumStatus.Rejected))
                //    {
                //        SuccessMessage("TC(s) on PO No. '" + poNo + "' rejected successfully...!!!");
                //    }
                //}

                GetPOList();
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
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
                case ".dxf":
                    GSTContentType = "application/dxf";
                    break;
                case ".DXF":
                    GSTContentType = "application/DXF";
                    break;
                case ".dwg":
                    GSTContentType = "application/dwg";
                    break;
                case ".DWG":
                    GSTContentType = "application/DWG";
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
                ExceptionMessage("GST File format not recognised. Upload Image/PDF/DXF/DWG formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }


    private void ViewDrawingFiles(int recordID, string fileType, string fileName)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                ExportTCFile(recordID, fileType);
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

    private void ExportTCFile(int recordID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();
            dsFiles = objTC.GetLOTDrawingFiles(recordID);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "DRAWING1")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["TC_ATTACHMENT1_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["TC_ATTACHMENT1_NAME"]);
                }
                else if (fileType == "DRAWING2")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["TC_ATTACHMENT2_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["TC_ATTACHMENT2_NAME"]);
                }
                else if (fileType == "DRAWING3")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["TC_ATTACHMENT3_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["TC_ATTACHMENT3_NAME"]);
                }

                if (bytes != null)
                {
                    string[] stringParts = fileName.Split(new char[] { '.' });
                    string strType = stringParts[1];
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.ContentType = strType;
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
            }

        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("Timeout expired"))
            {
                ExceptionMessage("The process of downloading is too longer, please try again...!!!");
                return;
            }
            else
            {
                throw;
            }
        }
    }

    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 1; i < dt.Columns.Count - 2; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 1; k < dt.Columns.Count - 2; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;


                    rowTxt = rowTxt.Replace(',', ' ');
                    rowTxt = rowTxt.TrimEnd('\r', ' ');
                    rowTxt = rowTxt.TrimEnd('\n', ' ');
                    rowTxt = rowTxt.Replace('\r', ' ');
                    rowTxt = rowTxt.Replace('\n', ' ');

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "Procurement_Status-Procurement_View_" + DateTime.Now.ToString("dd_MMM_yyyy");
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


    private void UploadTC()
    {
        try
        {

            recordID = 0;
            poNo = string.Empty;
            poDate = string.Empty;
            vendorCode = string.Empty;
            vendorName = string.Empty;
            JOBNo = string.Empty;
            unitID = 0;
            itemCode = string.Empty;
            itemName = string.Empty;
            remarks = string.Empty;
            employeeRecordID = 0;

            TC1FileBytes = null;
            TC2FileBytes = null;
            TC3FileBytes = null;

            TC1File = string.Empty;
            TC2File = string.Empty;
            TC3File = string.Empty;


            if (Convert.ToInt32(ViewState["RECORD_ID"]) > 0)
                recordID = Convert.ToInt32(ViewState["RECORD_ID"]);

            if (!string.IsNullOrEmpty(txtPONoToEdit.Text))
                poNo = txtPONoToEdit.Text.ToUpper();

            if (!string.IsNullOrEmpty(txtPODateToEdit.Text))
                poDate = Convert.ToDateTime(txtPODateToEdit.Text).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtVendorCodeToEdit.Text))
                vendorCode = txtVendorCodeToEdit.Text.ToUpper();

            if (!string.IsNullOrEmpty(txtVendorNameToEdit.Text))
                vendorName = txtVendorNameToEdit.Text;

            if (!string.IsNullOrEmpty(txtJOBNoToEdit.Text))
                JOBNo = txtJOBNoToEdit.Text.ToUpper();

            if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0)
                unitID = Convert.ToInt32(ViewState["UNIT_ID"]);

            if (!string.IsNullOrEmpty(txtItemCodeToEdit.Text))
                itemCode = txtItemCodeToEdit.Text.ToUpper();

            if (!string.IsNullOrEmpty(txtItemNameToEdit.Text))
                itemName = txtItemNameToEdit.Text.ToUpper();

            if (!string.IsNullOrEmpty(txtRemarksToEdit.Text))
                remarks = txtRemarksToEdit.Text;


            if (fileTC1ToEdit.HasFile)
            {
                if (!string.IsNullOrEmpty(fileTC1ToEdit.PostedFile.FileName))
                {
                    string[] str = fileTC1ToEdit.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    TC1File = str[str.Length - 1];
                    TC1FileBytes = GetFileBytes(fileTC1ToEdit.PostedFile.FileName, fileTC1ToEdit.PostedFile.InputStream);
                }
            }


            if (fileTC2ToEdit.HasFile)
            {
                if (!string.IsNullOrEmpty(fileTC2ToEdit.PostedFile.FileName))
                {
                    string[] str = fileTC2ToEdit.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    TC2File = str[str.Length - 1];
                    TC2FileBytes = GetFileBytes(fileTC2ToEdit.PostedFile.FileName, fileTC2ToEdit.PostedFile.InputStream);
                }
            }


            if (fileTC3ToEdit.HasFile)
            {
                if (!string.IsNullOrEmpty(fileTC3ToEdit.PostedFile.FileName))
                {
                    string[] str = fileTC3ToEdit.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    TC3File = str[str.Length - 1];
                    TC3FileBytes = GetFileBytes(fileTC3ToEdit.PostedFile.FileName, fileTC3ToEdit.PostedFile.InputStream);
                }
            }

            if (Convert.ToInt32(ddlEmployeeToEdit.SelectedValue) > 0)
                employeeRecordID = Convert.ToInt32(ddlEmployeeToEdit.SelectedValue);


            int value = objTC.UploadTC(recordID, poNo, poDate, vendorCode, vendorName, JOBNo, unitID, itemCode, itemName, remarks,
                                       TC1File, TC1FileBytes, TC2File, TC2FileBytes, TC3File, TC3FileBytes,
                                       employeeRecordID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("TC uploaded successfully...!!!");
                GetPOList();
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion    
}
