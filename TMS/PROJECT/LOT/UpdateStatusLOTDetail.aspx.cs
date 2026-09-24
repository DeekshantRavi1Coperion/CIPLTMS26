using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Net.Mime;
using iTextSharp.tool.xml;

public partial class PROJECT_LOT_UpdateStatusLOTDetail : System.Web.UI.Page
{


    #region VARIABLES[=======================]

    LOTSendMail objLOTSendMail = new LOTSendMail();
    GetLOTMailTypeAndStatus objGetLOTMailTypeAndStatus = new GetLOTMailTypeAndStatus();
    LOTMailTypeAndStatusProperties objLOTMailTypeAndStatusProperties = new LOTMailTypeAndStatusProperties();
    GetLOTApproverStatus objGetLOTApproverStatus = new GetLOTApproverStatus();

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsSubitems = new DataSet();
    DataTable dtSubitemToAdd = new DataTable();
    DataSet dsApprovers = new DataSet();
    DataSet dsLOTTFDetails = new DataSet();
    DataSet dsMailInfo = new DataSet();
    DataTable dtNew = new DataTable();

    LOTHtmlForPDF objLOTHtmlForPDF = new LOTHtmlForPDF();

    int actID = 0;
    int amdActID = 0;
    int LOTTFID = 0;
    //int LOTMainSubItemID = 0;
    string companyName = string.Empty;
    string jobNo = string.Empty;
    string customerName = string.Empty;
    string custCode = string.Empty;
    string TFNo = string.Empty;
    string poNo = string.Empty;
    string LOTDate = string.Empty;
    string itemName = string.Empty;
    //string LOTMainItem = string.Empty;
    string impNotes = string.Empty;
    string remarks = string.Empty;

    string drawing1Txt = string.Empty;
    string drawing1Extn = string.Empty;

    string drawing2Txt = string.Empty;
    string drawing2Extn = string.Empty;

    string drawing3Txt = string.Empty;
    string drawing3Extn = string.Empty;

    string drawing4Txt = string.Empty;
    string drawing4Extn = string.Empty;

    int newStatusID = 0;
    int currentStatusID = 0;
    string currentStatus = string.Empty;
    string subject = string.Empty;
    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string fileName = string.Empty;
    string body = string.Empty;


    int createdByID = 0;
    string createrName = string.Empty;
    string createrEmail = string.Empty;

    int amendedByID = 0;
    string amendedByName = string.Empty;
    string amendedByEmail = string.Empty;

    int PEApproverID = 0;
    string PEApproverName = string.Empty;
    string PEApproverEmail = string.Empty;

    string PEApprovedByName = string.Empty;
    string PEApprovedByEmail = string.Empty;

    int PMApproverID = 0;
    string PMApproverName = string.Empty;
    string PMApproverEmail = string.Empty;
    string PMUD = string.Empty;
    string PMPD = string.Empty;

    string PMApprovedByName = string.Empty;
    string PMApprovedByEmail = string.Empty;

    string acceptedByName = string.Empty;
    string acceptedByEmail = string.Empty;

    int productionManagerID = 0;
    string productionManagerName = string.Empty;
    string productionManagerEmail = string.Empty;
    string productionManagerEmailCC = string.Empty;

    int amendmentByID = 0;
    string amendmentByName = string.Empty;

    string completedByName = string.Empty;
    string completedByEmail = string.Empty;

    string amendedPEApprovedByName = string.Empty;
    string amendedPEApprovedByEmail = string.Empty;

    string amendedPMApprovedByName = string.Empty;
    string amendedPMApprovedByEmail = string.Empty;

    string amendedAcceptedByName = string.Empty;
    string amendedAcceptedByEmail = string.Empty;

    string urlTxt = string.Empty;
    string href = string.Empty;
    string link = string.Empty;

    string approveHref = string.Empty;
    string approveLink = string.Empty;

    string amendmentHref = string.Empty;
    string amendmentLink = string.Empty;

    int PEID = 0;
    int PMID = 0;
    int approvedByID = 0;
    int prodMngrID = 0;
    int acceptedByID = 0;
    int amendmentCount = 0;

    int amendedApprovedByID = 0;
    int amendedAcceptedByID = 0;

    int nextStatusID = 0;
    int statusID = 0;
    string subitemDesc = string.Empty;
    string tagNo = string.Empty;

    string LOTMainItem = string.Empty;
    string LOTMainSubItem = string.Empty;
    int LOTMainItemID = 0;
    int LOTTFSubitemID = 0;
    int LOTMainSubitemID = 0;
    string LOTMainSubItemIDs = string.Empty;


    string drgNo = string.Empty;
    int revisionNo = 0;
    int quantity = 0;
    string categoryID = string.Empty;
    string category = string.Empty;

    string subitemAttachment1File = string.Empty;
    string subitemAttachment2File = string.Empty;
    string subitemAttachment3File = string.Empty;
    string subitemAttachment4File = string.Empty;

    Byte[] subitemAttachment1FileBytes = null;
    Byte[] subitemAttachment2FileBytes = null;
    Byte[] subitemAttachment3FileBytes = null;
    Byte[] subitemAttachment4FileBytes = null;


    string attachment1Txt = string.Empty;
    string attachment1Extn = string.Empty;
    string attachment2Txt = string.Empty;
    string attachment2Extn = string.Empty;
    string attachment3Txt = string.Empty;
    string attachment3Extn = string.Empty;
    string attachment4Txt = string.Empty;
    string attachment4Extn = string.Empty;

    string tableName = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (!IsPostBack)
        {
            if (Convert.ToInt32(Request.QueryString["lottfid"]) > 0)
            {
                Session["dtSubitem"] = null;
                Session["dtLOTDetails"] = null;
                Session["dsLOTTFDetails"] = null;


                ValidateAndLoginAndApprove();
                GetStatusMessage();
                if (!IsPostBack)
                {
                    hdConfirmValue.Value = "0";
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }

    protected void gvSubitems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" || Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" || Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" || Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblLOTTFID = gvSubitems.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblLOTTFSubitemID = gvSubitems.Rows[rowindex].FindControl("lblLOTTFSubitemID") as Label;

                Label lblAttachment1 = gvSubitems.Rows[rowindex].FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = gvSubitems.Rows[rowindex].FindControl("lblAttachment2") as Label;
                Label lblAttachment3 = gvSubitems.Rows[rowindex].FindControl("lblAttachment3") as Label;
                Label lblAttachment4 = gvSubitems.Rows[rowindex].FindControl("lblAttachment4") as Label;


                LOTTFID = Convert.ToInt32(lblLOTTFID.Text);



                SubitemsProperties objSP = new SubitemsProperties();

                objSP.LOTTFID = Convert.ToInt32(lblLOTTFID.Text);
                objSP.LOTTFSubitemID = Convert.ToInt32(lblLOTTFSubitemID.Text);


                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewDrawingFiles(LOTTFID, "DRAWING1", Convert.ToString(lblAttachment1.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                {
                    ViewDrawingFiles(LOTTFID, "DRAWING2", Convert.ToString(lblAttachment2.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                {
                    ViewDrawingFiles(LOTTFID, "DRAWING3", Convert.ToString(lblAttachment3.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                {
                    ViewDrawingFiles(LOTTFID, "DRAWING4", Convert.ToString(lblAttachment4.Text).Trim(), Convert.ToInt32(lblLOTTFSubitemID.Text));
                }


                //else if (Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                //{
                //    int value = SendEmail(LOTTFID);
                //    if (value > 0)
                //    {
                //        objProject.UpdateLOTMailStatusOne(LOTTFID, currentStatusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                //        SuccessMessage("Mail sent successfully.");
                //        GetLOTList();
                //    }
                //}                
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

    protected void gvSubitems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string attachment1Extn = string.Empty;
                string attachment2Extn = string.Empty;
                string attachment3Extn = string.Empty;
                string attachment4Extn = string.Empty;

                Label lblAttachment1 = (Label)e.Row.FindControl("lblAttachment1");
                Label lblAttachment2 = (Label)e.Row.FindControl("lblAttachment2");
                Label lblAttachment3 = (Label)e.Row.FindControl("lblAttachment3");
                Label lblAttachment4 = (Label)e.Row.FindControl("lblAttachment4");

                ImageButton imgBtnAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAttachment1");
                ImageButton imgBtnAttachment2 = (ImageButton)e.Row.FindControl("imgBtnAttachment2");
                ImageButton imgBtnAttachment3 = (ImageButton)e.Row.FindControl("imgBtnAttachment3");
                ImageButton imgBtnAttachment4 = (ImageButton)e.Row.FindControl("imgBtnAttachment4");

                imgBtnAttachment1.Visible = false;
                imgBtnAttachment2.Visible = false;
                imgBtnAttachment3.Visible = false;
                imgBtnAttachment4.Visible = false;

                imgBtnAttachment1.ToolTip = string.Empty;
                imgBtnAttachment2.ToolTip = string.Empty;
                imgBtnAttachment3.ToolTip = string.Empty;
                imgBtnAttachment4.ToolTip = string.Empty;



                if (!string.IsNullOrEmpty(lblAttachment1.Text))
                {
                    imgBtnAttachment1.Visible = true;
                    imgBtnAttachment1.ToolTip = lblAttachment1.Text;

                    attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
                    if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                    {
                        imgBtnAttachment1.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                    else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                    {
                        imgBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                    }
                }
                else
                {
                    imgBtnAttachment1.Visible = false;
                }


                if (!string.IsNullOrEmpty(lblAttachment2.Text))
                {
                    imgBtnAttachment2.Visible = true;
                    imgBtnAttachment2.ToolTip = lblAttachment2.Text;

                    attachment2Extn = Convert.ToString(lblAttachment2.Text).Split('.').Last();
                    if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "dxf" || attachment2Extn == "DXF")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/LOT/dxf.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                    else if (attachment2Extn == "dwg" || attachment2Extn == "DWG")
                    {
                        imgBtnAttachment2.ImageUrl = "~/Images/LOT/dwg.png";
                        imgBtnAttachment2.ToolTip = lblAttachment2.Text;
                    }
                }
                else
                {
                    imgBtnAttachment2.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblAttachment3.Text))
                {
                    imgBtnAttachment3.Visible = true;
                    imgBtnAttachment3.ToolTip = lblAttachment3.Text;

                    attachment3Extn = Convert.ToString(lblAttachment3.Text).Split('.').Last();
                    if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" || attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
                    {
                        imgBtnAttachment3.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                    else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
                    {
                        imgBtnAttachment3.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment3.ToolTip = lblAttachment3.Text;
                    }
                }
                else
                {
                    imgBtnAttachment3.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblAttachment4.Text))
                {
                    imgBtnAttachment4.Visible = true;
                    imgBtnAttachment4.ToolTip = lblAttachment4.Text;

                    attachment4Extn = Convert.ToString(lblAttachment4.Text).Split('.').Last();
                    if (attachment4Extn == "jpg" || attachment4Extn == "jepg" || attachment4Extn == "bmp" || attachment4Extn == "png" || attachment4Extn == "gif" || attachment4Extn == "JPG" || attachment4Extn == "JPEG" || attachment4Extn == "BMP" || attachment4Extn == "PNG" || attachment4Extn == "GIF")
                    {
                        imgBtnAttachment4.ImageUrl = "~/Images/imgicon1.png";
                        imgBtnAttachment4.ToolTip = lblAttachment4.Text;
                    }
                    else if (attachment4Extn == "pdf" || attachment4Extn == "PDF")
                    {
                        imgBtnAttachment4.ImageUrl = "~/Images/pdficon1.png";
                        imgBtnAttachment4.ToolTip = lblAttachment4.Text;
                    }
                }
                else
                {
                    imgBtnAttachment4.Visible = false;
                }


                string txt = string.Empty;
                string categoryTxt = string.Empty;

                Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
                Label lblCategory = (Label)e.Row.FindControl("lblCategory");


                if (!string.IsNullOrEmpty(lblCategoryID.Text))
                {
                    string[] srtCategoryID = lblCategoryID.Text.Split(',');
                    foreach (string i in srtCategoryID)
                    {
                        if (Convert.ToInt32(i) > 0)
                        {
                            if (Convert.ToInt32(i) == 1)
                                txt = "Fabrication";
                            else if (Convert.ToInt32(i) == 2)
                                txt = "Inspection";
                            else if (Convert.ToInt32(i) == 3)
                                txt = "Information";
                        }

                        categoryTxt += txt + ",";
                    }
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    categoryTxt = categoryTxt.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    lblCategory.Text = categoryTxt;
                }

                Label lblIsPartOfProductionStatusReport = (Label)e.Row.FindControl("lblIsPartOfProductionStatusReport");
                CheckBox chkIsPartOfProductStatusReport = (CheckBox)e.Row.FindControl("chkIsPartOfProductStatusReport");

                if (Convert.ToInt32(lblIsPartOfProductionStatusReport.Text) > 0)
                    chkIsPartOfProductStatusReport.Checked = true;
                else chkIsPartOfProductStatusReport.Checked = false;


                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
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
        ViewDrawingFiles(Convert.ToInt32(Request.QueryString["lottfid"]), "DRAWING1", txtViewAttachment1.Text.Trim());
    }

    protected void btnViewAttachment2_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(Request.QueryString["lottfid"]), "DRAWING2", txtViewAttachment2.Text.Trim());
    }

    protected void btnViewAttachment3_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(Request.QueryString["lottfid"]), "DRAWING3", txtViewAttachment3.Text.Trim());
    }

    protected void btnViewAttachment4_Click(object sender, ImageClickEventArgs e)
    {
        ViewDrawingFiles(Convert.ToInt32(Request.QueryString["lottfid"]), "DRAWING4", txtViewAttachment4.Text.Trim());
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            //if (newStatusID != 0)
            //{
            //    UpdateSubitemsStatus();
            //    btnSave.Visible = false;
            //    btnAmendment.Visible = false;
            //}
            //else
            //{
            //    GetStatusMessage();
            //}

            //UpdateSubitemsStatus();


            ApproveOrSendToAmendment(Convert.ToInt32(EnumActID.Approve));
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnAmendment_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            //actID = 0;
            //newStatusID = 0;

            ////if (Convert.ToInt32(Request.QueryString["actid"]) > 0)
            ////    actID = Convert.ToInt32(Request.QueryString["actid"]);

            ////if (Convert.ToInt32(Request.QueryString["newstatusid"]) > 0)
            ////    newStatusID = Convert.ToInt32(Request.QueryString["newstatusid"]);

            //actID = Convert.ToInt32(EnumActs.Amendment);
            //newStatusID = Convert.ToInt32(EnumStatus.Amendment);

            //if (actID != 0 && newStatusID != 0)
            //{
            //    UpdateLOTTFStatus(actID, newStatusID);
            //}
            //else
            //{
            //    GetStatusMessage();
            //}

            //SendToAmendmentSubitems();

            ApproveOrSendToAmendment(Convert.ToInt32(EnumActID.SendToAmendment));
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }


    }

    protected void btnLOTList_Click(object sender, EventArgs e)
    {
        LOTTFID = 0;
        PMUD = string.Empty;
        PMPD = string.Empty;

        if (Convert.ToInt32(Request.QueryString["lottfid"]) > 0)
            LOTTFID = Convert.ToInt32(Request.QueryString["lottfid"]);

        if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["ud"])))
            PMUD = Convert.ToString(Request.QueryString["ud"]).Trim();

        if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["pd"])))
            PMPD = Convert.ToString(Request.QueryString["pd"]).Trim();

        Response.Redirect("/PROJECT/LOT/LOTTransmittalFactoryList.aspx?LOTTFID=" + LOTTFID + "&tfno=" + txtTFNo.Text + "&ud=" + PMUD + "&pd=" + PMPD);
    }

    #endregion


    #region METHODS[=========================]

    private void ValidateAndLoginAndApprove()
    {
        try
        {
            DataSet ds = new DataSet();
            string userName = string.Empty;
            string password = string.Empty;
            userName = Convert.ToString(Request.QueryString["ud"]).Trim();
            password = Convert.ToString(Request.QueryString["pd"]).Trim();

            ds = objCommon.ValidateAndLogin(userName, password);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
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
                Session["TS_APPROVAL_BY"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TS_APPROVAL_BY"]);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    Session["TEAMMEMBERS"] = ds.Tables[1];
                }

                if (ds.Tables.Count > 0)
                {
                    Session["USERINFO"] = ds;
                }

                BindLOTTFDetails();
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

    private void BindLOTTFDetails()
    {
        try
        {
            remarks = string.Empty;

            drawing1Txt = string.Empty;
            drawing1Extn = string.Empty;

            drawing2Txt = string.Empty;
            drawing2Extn = string.Empty;

            drawing3Txt = string.Empty;
            drawing3Extn = string.Empty;

            drawing4Txt = string.Empty;
            drawing4Extn = string.Empty;


            attachment1Txt = string.Empty;
            attachment1Extn = string.Empty;
            attachment2Txt = string.Empty;
            attachment2Extn = string.Empty;
            attachment3Txt = string.Empty;
            attachment3Extn = string.Empty;
            attachment4Txt = string.Empty;
            attachment4Extn = string.Empty;


            txtViewAttachment1.Text = string.Empty;
            btnViewAttachment1.Visible = false;
            txtViewAttachment2.Text = string.Empty;
            btnViewAttachment2.Visible = false;
            txtViewAttachment3.Text = string.Empty;
            btnViewAttachment3.Visible = false;
            txtViewAttachment4.Text = string.Empty;
            btnViewAttachment4.Visible = false;

            hdTFNo.Value = string.Empty;
            hdStatusID.Value = "0";
            hdPEID.Value = "0";
            hdPMID.Value = "0";
            hdTableName.Value = string.Empty;
            createdByID = 0;
            PEID = 0;
            PMID = 0;
            amendmentCount = 0;
            newStatusID = 0;


            LOTTFID = 0;
            if (Convert.ToInt32(Request.QueryString["lottfid"]) > 0)
                LOTTFID = Convert.ToInt32(Request.QueryString["lottfid"]);

            dsLOTTFDetails = objProject.GetLOTTFDetailsTwo(LOTTFID);

            if (dsLOTTFDetails.Tables.Count > 0)
            {
                Session["dsLOTTFDetails"] = dsLOTTFDetails;

                if (dsLOTTFDetails.Tables[0].Rows.Count > 0)
                {

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CREATED_BY_ID"] != DBNull.Value && Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["CREATED_BY_ID"]) > 0)
                        createdByID = Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["CREATED_BY_ID"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["PE_ID"] != DBNull.Value && Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["PE_ID"]) > 0)
                    {
                        hdPEID.Value = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PE_ID"]);
                        PEID = Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["PE_ID"]);
                    }

                    if (dsLOTTFDetails.Tables[0].Rows[0]["PM_ID"] != DBNull.Value && Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["PM_ID"]) > 0)
                    {
                        hdPMID.Value = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PM_ID"]);
                        PMID = Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["PM_ID"]);
                    }

                    if (dsLOTTFDetails.Tables[0].Rows[0]["UNIT_ID"] != DBNull.Value && Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_ID"]) > 0)
                        ViewState["UNIT_ID"] = Convert.ToInt32(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_ID"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"])))
                        txtCompany.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["UNIT_NAME"]);


                    if (dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"])))
                        txtJOBNo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["JOB_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"])))
                        txtCustomerName.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_NAME"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"])))
                        txtCustomerCode.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["CUSTOMER_CODE"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"])))
                        txtPONo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["PO_NO"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["DATE"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"])))
                        txtDate.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["DATE"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"])))
                    {
                        hdTFNo.Value = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);
                        txtTFNo.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["TF_NO"]);
                    }

                    if (dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"])))
                        txtItemName.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ITEM_NAME"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"])))
                        txtNotes.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["IMP_NOTES"]);

                    if (dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_REMARKS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_REMARKS"])))
                        txtAmendedRemarks.Text = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["AMENDED_REMARKS"]);



                    if (dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT1_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT1_NAME"])))
                        attachment1Txt = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);

                    if (!string.IsNullOrEmpty(attachment1Txt))
                    {
                        txtViewAttachment1.Text = attachment1Txt;
                        btnViewAttachment1.Visible = true;
                        attachment1Extn = Convert.ToString(attachment1Txt).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {

                            btnViewAttachment1.ImageUrl = "~/Images/imgicon1.png";
                            btnViewAttachment1.ToolTip = attachment1Txt;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            btnViewAttachment1.ImageUrl = "~/Images/pdficon1.png";
                            btnViewAttachment1.ToolTip = attachment1Txt;
                        }
                    }





                    if (dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT2_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT2_NAME"])))
                        attachment2Txt = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT2_NAME"]);

                    if (!string.IsNullOrEmpty(attachment2Txt))
                    {
                        txtViewAttachment2.Text = attachment2Txt;
                        btnViewAttachment2.Visible = true;
                        attachment2Extn = Convert.ToString(attachment2Txt).Split('.').Last();
                        if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
                        {

                            btnViewAttachment2.ImageUrl = "~/Images/imgicon1.png";
                            btnViewAttachment2.ToolTip = attachment2Txt;
                        }
                        else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
                        {
                            btnViewAttachment2.ImageUrl = "~/Images/pdficon1.png";
                            btnViewAttachment2.ToolTip = attachment2Txt;
                        }
                    }



                    if (dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT3_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT3_NAME"])))
                        attachment3Txt = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT3_NAME"]);

                    if (!string.IsNullOrEmpty(attachment3Txt))
                    {
                        txtViewAttachment3.Text = attachment3Txt;
                        btnViewAttachment3.Visible = true;
                        attachment3Extn = Convert.ToString(attachment3Txt).Split('.').Last();
                        if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" || attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
                        {

                            btnViewAttachment3.ImageUrl = "~/Images/imgicon1.png";
                            btnViewAttachment3.ToolTip = attachment3Txt;
                        }
                        else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
                        {
                            btnViewAttachment3.ImageUrl = "~/Images/pdficon1.png";
                            btnViewAttachment3.ToolTip = attachment3Txt;
                        }
                    }


                    if (dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT4_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT4_NAME"])))
                        attachment4Txt = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["ATTACHMENT4_NAME"]);

                    if (!string.IsNullOrEmpty(attachment4Txt))
                    {
                        txtViewAttachment4.Text = attachment4Txt;
                        btnViewAttachment4.Visible = true;
                        attachment4Extn = Convert.ToString(attachment4Txt).Split('.').Last();
                        if (attachment4Extn == "jpg" || attachment4Extn == "jepg" || attachment4Extn == "bmp" || attachment4Extn == "png" || attachment4Extn == "gif" || attachment4Extn == "JPG" || attachment4Extn == "JPEG" || attachment4Extn == "BMP" || attachment4Extn == "PNG" || attachment4Extn == "GIF")
                        {

                            btnViewAttachment4.ImageUrl = "~/Images/imgicon1.png";
                            btnViewAttachment4.ToolTip = attachment4Txt;
                        }
                        else if (attachment4Extn == "pdf" || attachment4Extn == "PDF")
                        {
                            btnViewAttachment4.ImageUrl = "~/Images/pdficon1.png";
                            btnViewAttachment4.ToolTip = attachment4Txt;
                        }
                    }

                    if (dsLOTTFDetails.Tables[0].Rows[0]["SUBITEM_TABLE_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["SUBITEM_TABLE_NAME"])))
                        hdTableName.Value = Convert.ToString(dsLOTTFDetails.Tables[0].Rows[0]["SUBITEM_TABLE_NAME"]);
                }

                if (dsLOTTFDetails.Tables[1].Rows.Count > 0)
                {
                    hdStatusID.Value = Convert.ToString(dsLOTTFDetails.Tables[1].Rows[0]["STATUS_ID"]);
                    gvSubitems.DataSource = dsLOTTFDetails.Tables[1];
                    gvSubitems.DataBind();
                }
                else
                {
                    gvSubitems.DataSource = null;
                    gvSubitems.DataBind();
                }

                lblSubitemsRecords.Text = "Records[" + gvSubitems.Rows.Count + "]";
            }
            else
            {
                Session["dsLOTTFDetails"] = null;
            }


            if (createdByID == PEID && createdByID == PMID)
            {
                btnAmendment.Visible = false;
            }
            else
            {
                if (createdByID == PEID && Convert.ToInt32(Session["EMP_RECORD_ID"]) == PMID)
                {
                    btnAmendment.Visible = true;
                }
                else if (createdByID != PEID && createdByID != PMID)
                {
                    btnAmendment.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetStatusMessage()
    {
        try
        {
            currentStatusID = Convert.ToInt32(hdStatusID.Value);
            TFNo = Convert.ToString(hdTFNo.Value);

            btnSave.Visible = false;
            btnAmendment.Visible = false;

            if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
            {
                btnSave.Visible = true;
                btnAmendment.Visible = true;
            }
            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
            {
                SuccessMessage("TF No.: " + TFNo + " already approved...!!!");
            }
            else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
            {
                SuccessMessage("TF No.: " + TFNo + " already approved...!!!");
            }
            else
            {
                SuccessMessage("TF No.: " + TFNo + " already approved...!!!");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    //private void GetStatusMessage()
    //{
    //    try
    //    {
    //        currentStatusID = Convert.ToInt32(hdStatusID.Value);
    //        TFNo = Convert.ToString(hdTFNo.Value);

    //        btnSave.Visible = false;
    //        btnAmendment.Visible = false;

    //        if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New))
    //        {
    //            btnSave.Visible = true;
    //            btnAmendment.Visible = true;
    //        }
    //        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved))
    //        {
    //            SuccessMessage("TF No.: " + TFNo + " already approved...!!!");
    //        }
    //        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.ProdAccepted))
    //        {
    //            SuccessMessage("TF No.: " + TFNo + " accepted by production manager...!!!");
    //        }
    //        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Complete))
    //        {
    //            SuccessMessage("TF No.: " + TFNo + " completed/closed by production manager...!!!");
    //        }
    //        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment))
    //        {
    //            SuccessMessage("TF No.: " + TFNo + " sent to amendment...!!!");
    //        }
    //        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
    //        {
    //            SuccessMessage("TF No.: " + TFNo + " amended and waiting for approval...!!!");
    //        }
    //        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndApproved))
    //        {
    //            SuccessMessage("TF No.: " + TFNo + " already approved...!!!");
    //        }
    //        else if (currentStatusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.AmndProdAccepted))
    //        {
    //            SuccessMessage("TF No.: " + TFNo + " accepted by production manager...!!!");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    private void ViewDrawingFiles(int LOTTFID, string fileType, string fileName)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    //imgFile.ImageUrl = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType;
                    //mpeViewViewImgFileAttachment.Show();

                    string url = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "";
                    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=yes,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    //iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType);
                    //this.mpeViewViewPDFFileAttachment.Show();

                    string url = "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "";
                    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=yes,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

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

    private void ViewDrawingFiles(int LOTTFID, string fileType, string fileName, int LOTTFSubitemID)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    string url = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    string url = "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
                else if (extn == "dwg" || extn == "DWG")
                {
                    ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);
                }
                else if (extn == "dxf" || extn == "DXF")
                {
                    ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);
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

    private void ExportDWGFile(int LOTTFID, int LOTTFSubitemID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();
            dsFiles = objProject.GetLOTDrawingFiles(LOTTFID, LOTTFSubitemID);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "DRAWING1")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);
                }
                else if (fileType == "DRAWING2")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT2_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT2_NAME"]);
                }
                else if (fileType == "DRAWING3")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT3_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT3_NAME"]);
                }
                else if (fileType == "DRAWING4")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT4_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT4_NAME"]);
                }
                else if (fileType == "IRN")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["IRN_ATTACHMENT_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["IRN_ATTACHMENT_NAME"]);
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

    private void ApproveOrSendToAmendment(int actID)
    {
        try
        {
            string LOTTFSubitemIDs = string.Empty;
            string LOTMainSubItemIDs = string.Empty;
            int nextStatusID = 0;
            int createdByID = 0;
            int PEID = 0;
            int PMID = 0;
            int approvedByID = 0;
            int amendedApprovedByID = 0;
            int prodMngrID = 0;
            int acceptedByID = 0;
            int amendedAcceptedByID = 0;
            int companyID = 0;
            int isPartOfProductionReportID = 0;

            LOTTFID = 0;
            companyID = 0;
            LOTMainSubItemIDs = string.Empty;
            TFNo = string.Empty;
            remarks = string.Empty;
            tableName = string.Empty;
            quantity = 0;

            if (Convert.ToInt32(Request.QueryString["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(Request.QueryString["LOTTFID"]);

            if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0)
                companyID = Convert.ToInt32(ViewState["UNIT_ID"]);


            if (!string.IsNullOrEmpty(txtTFNo.Text))
                TFNo = txtTFNo.Text;

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;

            tableName = hdTableName.Value;



            #region LOT Subitems Detail

            LOTTFSubitemIDs = string.Empty;
            LOTMainSubItemIDs = string.Empty;
            nextStatusID = 0;
            createdByID = 0;
            PEID = 0;
            PMID = 0;

            statusID = 0;
            LOTMainSubitemID = 0;

            approvedByID = 0;
            amendedApprovedByID = 0;
            prodMngrID = 0;
            acceptedByID = 0;
            amendedAcceptedByID = 0;
            amendmentCount = 0;
            isPartOfProductionReportID = 0;

            PEID = Convert.ToInt32(hdPEID.Value);
            PMID = Convert.ToInt32(hdPMID.Value);


            if (Session["dsLOTTFDetails"] != null)
                dsLOTTFDetails = (DataSet)Session["dsLOTTFDetails"];

            //int value = 0;
            int value1 = 0;
            int value2 = 0;
            int count = 0;
            string updationQuery = string.Empty;

            if (LOTTFID > 0)
            {
                if (dsLOTTFDetails.Tables.Count > 0)
                {
                    if (dsLOTTFDetails.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow drsi1 in dsLOTTFDetails.Tables[2].Rows)
                        {
                            if (dsLOTTFDetails.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow drsi0 in dsLOTTFDetails.Tables[1].Select("LOT_MAIN_SUBITEM_ID='" + Convert.ToInt32(drsi1["LOT_MAIN_SUBITEM_ID"]) + "'"))
                                {
                                    statusID = Convert.ToInt32(drsi0["STATUS_ID"]);
                                    LOTTFSubitemID = Convert.ToInt32(drsi0["LOT_TF_SUBITEM_ID"]);
                                    LOTMainSubitemID = Convert.ToInt32(drsi0["LOT_MAIN_SUBITEM_ID"]);
                                    amendmentCount = Convert.ToInt32(drsi0["AMENDMENT_COUNT"]);
                                    prodMngrID = Convert.ToInt32(drsi0["PRODUCTION_MNGR_ID"]);
                                    isPartOfProductionReportID = Convert.ToInt32(drsi0["IS_PART_OF_PRODUCTION_STATUS_REPORT_ID"]);
                                    createdByID = Convert.ToInt32(drsi0["CREATED_BY"]);
                                    approvedByID = Convert.ToInt32(drsi0["APPROVED_BY"]);
                                    amendedApprovedByID = Convert.ToInt32(drsi0["AMENDED_APPROVED_BY"]);

                                    acceptedByID = Convert.ToInt32(drsi0["ACCEPTED_BY"]);
                                    amendedAcceptedByID = Convert.ToInt32(drsi0["AMENDED_ACCEPTED_BY"]);

                                    quantity = Convert.ToInt32(drsi0["QUANTITY"]);


                                    if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(LOTTFSubitemID) + ","))
                                    {
                                        count++;
                                        if (statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.New) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Approved) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment) ||
                                            statusID == Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amended))
                                        {
                                            LOTTFSubitemIDs += Convert.ToString(LOTTFSubitemID) + ",";
                                        }
                                        else
                                        {

                                            if (isPartOfProductionReportID > 0)
                                            {
                                                LOTTFSubitemIDs += Convert.ToString(LOTTFSubitemID) + ",";
                                            }
                                        }


                                        if (actID == Convert.ToInt32(EnumActID.Approve))
                                        {
                                            if (amendmentCount > 0)
                                                objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(amendedByID, PEID, PMID, prodMngrID, amendedApprovedByID), amendmentCount, statusID, PEID, PMID, prodMngrID);
                                            else
                                                objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, statusID, PEID, PMID, prodMngrID);


                                            nextStatusID = objLOTMailTypeAndStatusProperties.NextStatusID;
                                        }
                                        else if (actID == Convert.ToInt32(EnumActID.SendToAmendment))
                                        {
                                            nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                                        }

                                        try
                                        {
                                            value1 = objProject.UpdateLOTTFStatusTwo(LOTTFID, LOTTFSubitemID, quantity, 0, nextStatusID, "", null, remarks, 0,
                                                                                 "", null, "", 0, 0, 0,
                                                                                 Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                        }
                                        catch (Exception ex)
                                        {

                                            throw;
                                        }

                                        value2 = value2 + value1;
                                    }




                                    //if (!LOTMainSubItemIDs.Contains(Convert.ToString(LOTMainSubitemID)))
                                    //{
                                    //    count++;

                                    //    LOTMainSubItemIDs += Convert.ToString(LOTMainSubitemID) + ",";

                                    //    if (actID == Convert.ToInt32(EnumActID.Approve))
                                    //    {
                                    //        if (amendmentCount > 0)
                                    //            objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(amendedByID, PEID, PMID, prodMngrID, amendedApprovedByID), amendmentCount, statusID);
                                    //        else
                                    //            objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, statusID);


                                    //        //objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, statusID);

                                    //        nextStatusID = objLOTMailTypeAndStatusProperties.NextStatusID;
                                    //    }
                                    //    else if (actID == Convert.ToInt32(EnumActID.SendToAmendment))
                                    //    {
                                    //        nextStatusID = Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment);
                                    //    }

                                    //    value1 = objProject.UpdateLOTTFStatusTwo(LOTTFID, LOTMainSubitemID, nextStatusID, "", null, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                    //    value2 = value2 + value1;
                                    //}
                                }
                            }
                        }
                    }
                }
            }


            if (value2 > 0)
            {
                //if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
                //    LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');

                if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                    LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                if (value2 == count)
                {
                    LOTSendMail objLOTSendMail = new LOTSendMail();
                    //int sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNo.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", 0, 0, 0, 0, null);

                    //int sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, txtTFNo.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", 0, 0, 0, 0, null, null, 0, 0);

                    int sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTTFSubitemIDs, txtTFNo.Text, companyID, "", 0, 0, null, 0, 0, 0);
                    if (sendMailValue > 0)
                    {
                        //int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTMainSubItemIDs, nextStatusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTTFSubitemIDs, nextStatusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        if (actID == Convert.ToInt32(EnumActID.Approve))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage("LOT with TF. No.: '" + TFNo + "' approved and mail sent successfully.");
                            else
                                SuccessMessage("Amended LOT with TF. No.: '" + TFNo + "' approved and mail sent successfully.");
                        }
                        else if (actID == Convert.ToInt32(EnumActID.SendToAmendment))
                        {
                            SuccessMessage("Amended LOT with TF. No.: '" + TFNo + "' sent to amendment and mail sent successfully.");
                        }
                    }
                    else
                    {
                        if (actID == Convert.ToInt32(EnumActID.Approve))
                        {
                            if (amendmentCount == 0)
                                SuccessMessage("LOT with TF. No.: '" + TFNo + "' approved successfully...!!!");
                            else
                                SuccessMessage("Amended LOT with TF. No.: '" + TFNo + "' approved successfully...!!!");
                        }
                        else if (actID == Convert.ToInt32(EnumActID.SendToAmendment))
                        {
                            SuccessMessage("Amended LOT with TF. No.: '" + TFNo + "' sent to amendment successfully.");
                        }
                    }
                }
                else if (value2 < count)
                {
                    RemoveSubitems(LOTMainSubitemID);
                }

                btnSave.Visible = false;
                btnAmendment.Visible = false;
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }


            #endregion

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SendToAmendmentSubitems()
    {
        try
        {
            string LOTMainSubItemIDs = string.Empty;
            int PEID = 0;
            int PMID = 0;
            int prodMngrID = 0;
            int companyID = 0;

            LOTTFID = 0;
            companyID = 0;
            TFNo = string.Empty;
            remarks = string.Empty;
            quantity = 0;

            if (Convert.ToInt32(ViewState["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

            if (Convert.ToInt32(ViewState["UNIT_ID"]) > 0)
                companyID = Convert.ToInt32(ViewState["UNIT_ID"]);

            if (!string.IsNullOrEmpty(txtTFNo.Text))
                TFNo = txtTFNo.Text;

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;


            #region LOT Subitems Detail

            PEID = 0;
            PMID = 0;
            LOTMainSubitemID = 0;

            createdByID = Convert.ToInt32(ViewState["CREATED_BY_ID"]);
            PEID = Convert.ToInt32(ViewState["PE_ID"]);
            PMID = Convert.ToInt32(ViewState["PM_ID"]);

            if (Session["dsLOTTFDetails"] != null)
                dsLOTTFDetails = (DataSet)Session["dsLOTTFDetails"];

            int value1 = 0;
            int value2 = 0;
            int count = 0;

            if (LOTTFID > 0)
            {
                if (dsLOTTFDetails.Tables.Count > 0)
                {
                    if (dsLOTTFDetails.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow drsi1 in dsLOTTFDetails.Tables[2].Rows)
                        {
                            if (dsLOTTFDetails.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow drsi0 in dsLOTTFDetails.Tables[1].Select("LOT_MAIN_SUBITEM_ID='" + Convert.ToInt32(drsi1["LOT_MAIN_SUBITEM_ID"]) + "'"))
                                {
                                    count++;
                                    statusID = Convert.ToInt32(drsi0["STATUS_ID"]);
                                    LOTMainSubitemID = Convert.ToInt32(drsi0["LOT_MAIN_SUBITEM_ID"]);
                                    prodMngrID = Convert.ToInt32(drsi0["PRODUCTION_MNGR_ID"]);
                                    quantity = Convert.ToInt32(drsi0["QUANTITY"]);


                                    LOTMainSubItemIDs += LOTMainSubitemID + ",";

                                    value1 = objProject.UpdateLOTTFStatusTwo(LOTTFID, LOTMainSubitemID, quantity, 0, Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment), "", null, remarks, 0,
                                                                             "", null, "", 0, 0, 0,
                                                                             Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                    value2 = value2 + value1;
                                }
                            }
                        }
                    }
                }
            }

            if (value2 > 0)
            {
                if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
                    LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');
                if (value2 == count)
                {
                    btnSave.Visible = false;
                    btnAmendment.Visible = false;

                    int sendMailValue = 0;

                    LOTSendMail objLOTSendMail = new LOTSendMail();

                    if (prodMngrID == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                    {
                        //sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNo.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]), txtRemarks.Text, 0, 0, 0, 0, null, null, 0, 0);

                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNo.Text, companyID, txtRemarks.Text,
                            //0,
                            0, 0, null, 0, 0, 0);
                    }
                    else
                    {
                        //sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNo.Text, companyID, Convert.ToInt32(Session["EMP_RECORD_ID"]), "", 0, 0, 0, 0, null, null, 0, 0);

                        sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNo.Text, companyID, "",
                            //0,
                            0, 0, null, 0, 0, 0);
                    }

                    if (sendMailValue > 0)
                    {
                        int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTMainSubItemIDs, statusID, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("LOT TF. No.: '" + TFNo + "' sent to amendment and mail sent successfully.");
                    }
                    else
                    {
                        SuccessMessage("Amended LOT with TF. No.: '" + TFNo + "' sent to amendment successfully.");
                    }
                }
                else if (value2 < count)
                {
                    RemoveSubitems(LOTMainSubitemID);
                }
            }

            #endregion

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    //private void UpdateSubitemsStatus()
    //{
    //    try
    //    {
    //        string LOTMainSubItemIDs = string.Empty;
    //        int nextStatusID = 0;
    //        int createdByID = 0;
    //        int PEID = 0;
    //        int PMID = 0;
    //        int approvedByID = 0;
    //        int amendedApprovedByID = 0;
    //        int prodMngrID = 0;
    //        int acceptedByID = 0;
    //        int amendedAcceptedByID = 0;
    //        int LOTMainSubitemID = 0;

    //        LOTTFID = 0;
    //        LOTMainSubItemIDs = string.Empty;
    //        TFNo = string.Empty;
    //        remarks = string.Empty;

    //        if (Convert.ToInt32(ViewState["LOTTFID"]) > 0)
    //            LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

    //        if (!string.IsNullOrEmpty(txtTFNo.Text))
    //            TFNo = txtTFNo.Text;

    //        if (!string.IsNullOrEmpty(txtRemarks.Text))
    //            remarks = txtRemarks.Text;


    //        #region LOT Subitems Detail

    //        LOTMainSubItemIDs = string.Empty;
    //        nextStatusID = 0;
    //        createdByID = 0;
    //        PEID = 0;
    //        PMID = 0;

    //        statusID = 0;
    //        LOTMainSubitemID = 0;

    //        approvedByID = 0;
    //        amendedApprovedByID = 0;
    //        prodMngrID = 0;
    //        acceptedByID = 0;
    //        amendedAcceptedByID = 0;
    //        amendmentCount = 0;

    //        //createdByID = Convert.ToInt32(Session["EMP_RECORD_ID"]);
    //        //PEID = Convert.ToInt32(ViewState["PE_ID"]);
    //        //PMID = Convert.ToInt32(ViewState["PM_ID"]);
    //        //amendmentCount = Convert.ToInt32(ViewState["AMENDMENT_COUNT"]);

    //        createdByID = Convert.ToInt32(ViewState["CREATED_BY_ID"]);
    //        PEID = Convert.ToInt32(ViewState["PE_ID"]);
    //        PMID = Convert.ToInt32(ViewState["PM_ID"]);


    //        //approvedByID = 0;
    //        //amendedApprovedByID = 0;
    //        //prodMngrID = objSP.ProdMngrID;
    //        //acceptedByID = objSP.LOTAcceptedByID;
    //        //amendedacceptedByID = objSP.AmendedAcceptedByID;                    
    //        //amendmentCount = objSP.AmendmentCount;



    //        //if (Session["dsSubitemsSI"] != null)
    //        //    dsSubitems = (DataSet)Session["dsSubitemsSI"];

    //        if (Session["dsSubitems"] != null)
    //            dsSubitems = (DataSet)Session["dsSubitems"];


    //        int value1 = 0;
    //        int value2 = 0;
    //        int count = 0;

    //        if (LOTTFID > 0)
    //        {
    //            if (dsSubitems.Tables.Count > 0)
    //            {
    //                if (dsSubitems.Tables[2].Rows.Count > 0)
    //                {
    //                    foreach (DataRow drsi2 in dsSubitems.Tables[2].Rows)
    //                    {
    //                        if (dsSubitems.Tables[1].Rows.Count > 0)
    //                        {
    //                            foreach (DataRow drsi1 in dsSubitems.Tables[1].Select("LOT_MAIN_SUBITEM_ID='" + Convert.ToInt32(drsi2["LOT_MAIN_SUBITEM_ID"]) + "'"))
    //                            {
    //                                count++;
    //                                statusID = Convert.ToInt32(drsi1["STATUS_ID"]);
    //                                LOTMainSubitemID = Convert.ToInt32(drsi1["LOT_MAIN_SUBITEM_ID"]);
    //                                amendmentCount = Convert.ToInt32(drsi1["AMENDMENT_COUNT"]);
    //                                prodMngrID = Convert.ToInt32(drsi1["PRODUCTION_MNGR_ID"]);

    //                                approvedByID = Convert.ToInt32(drsi1["APPROVED_BY"]);
    //                                amendedApprovedByID = Convert.ToInt32(drsi1["AMENDED_APPROVED_BY"]);

    //                                acceptedByID = Convert.ToInt32(drsi1["ACCEPTED_BY"]);
    //                                amendedAcceptedByID = Convert.ToInt32(drsi1["AMENDED_ACCEPTED_BY"]);

    //                                LOTMainSubItemIDs += LOTMainSubitemID + ",";

    //                                //if (amendmentCount > 0)
    //                                //    objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(PEID, PMID, prodMngrID, objGetLOTApproverStatus.GetApproverStatusValue(amendedByID, PEID, PMID, prodMngrID, amendedApprovedByID), amendmentCount, statusID);
    //                                //else
    //                                //    objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(PEID, PMID, prodMngrID, objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, statusID);

    //                                objLOTMailTypeAndStatusProperties = objGetLOTMailTypeAndStatus.GetLOTMailTypeAndStatusValues(Convert.ToInt32(Session["EMP_RECORD_ID"]), objGetLOTApproverStatus.GetApproverStatusValue(createdByID, PEID, PMID, prodMngrID, approvedByID), amendmentCount, statusID);
    //                                nextStatusID = objLOTMailTypeAndStatusProperties.NextStatusID;

    //                                value1 = objProject.UpdateLOTTFStatusTwo(LOTTFID, LOTMainSubitemID, nextStatusID, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                                //value=updatelotapproval
    //                                value2 = value2 + value1;

    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        if (value2 > 0)
    //        {
    //            if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
    //                LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');
    //            if (value2 == count)
    //            {
    //                LOTSendMail objLOTSendMail = new LOTSendMail();
    //                int sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNo.Text, Convert.ToInt32(Session["EMP_RECORD_ID"]), "");

    //                if (sendMailValue > 0)
    //                {
    //                    int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTMainSubItemIDs, statusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

    //                    if (amendmentCount == 0)
    //                        SuccessMessage("LOT approved with TF. No.: '" + TFNo + "' and mail sent successfully.");
    //                    else
    //                        SuccessMessage("Amended LOT approved with TF. No.: '" + TFNo + "' and mail sent successfully.");
    //                }
    //                else
    //                {
    //                    if (amendmentCount == 0)
    //                        SuccessMessage("LOT approved with TF. No.: '" + TFNo + "' successfully.");
    //                    else
    //                        SuccessMessage("Amended LOT approved with TF. No.: '" + TFNo + "' successfully.");
    //                }
    //            }
    //            else if (value2 < count)
    //            {
    //                RemoveSubitems(LOTMainSubitemID);
    //            }
    //        }

    //        #endregion

    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    //private void SendToAmendmentSubitems()
    //{
    //    try
    //    {
    //        string LOTMainSubItemIDs = string.Empty;
    //        int PEID = 0;
    //        int PMID = 0;
    //        int prodMngrID = 0;
    //        int LOTMainSubitemID = 0;

    //        LOTTFID = 0;
    //        TFNo = string.Empty;
    //        remarks = string.Empty;

    //        if (Convert.ToInt32(ViewState["LOTTFID"]) > 0)
    //            LOTTFID = Convert.ToInt32(ViewState["LOTTFID"]);

    //        if (!string.IsNullOrEmpty(txtTFNo.Text))
    //            TFNo = txtTFNo.Text;

    //        if (!string.IsNullOrEmpty(txtRemarks.Text))
    //            remarks = txtRemarks.Text;


    //        #region LOT Subitems Detail

    //        PEID = 0;
    //        PMID = 0;
    //        LOTMainSubitemID = 0;

    //        createdByID = Convert.ToInt32(ViewState["CREATED_BY_ID"]);
    //        PEID = Convert.ToInt32(ViewState["PE_ID"]);
    //        PMID = Convert.ToInt32(ViewState["PM_ID"]);

    //        if (Session["dsSubitems"] != null)
    //            dsSubitems = (DataSet)Session["dsSubitems"];

    //        int value1 = 0;
    //        int value2 = 0;
    //        int count = 0;

    //        if (LOTTFID > 0)
    //        {
    //            if (dsSubitems.Tables[2].Rows.Count > 0)
    //            {
    //                foreach (DataRow drsi2 in dsSubitems.Tables[2].Rows)
    //                {
    //                    if (dsSubitems.Tables[1].Rows.Count > 0)
    //                    {
    //                        foreach (DataRow drsi1 in dsSubitems.Tables[1].Select("LOT_MAIN_SUBITEM_ID='" + Convert.ToInt32(drsi2["LOT_MAIN_SUBITEM_ID"]) + "'"))
    //                        {
    //                            count++;
    //                            statusID = Convert.ToInt32(drsi1["STATUS_ID"]);
    //                            LOTMainSubitemID = Convert.ToInt32(drsi1["LOT_MAIN_SUBITEM_ID"]);
    //                            prodMngrID = Convert.ToInt32(drsi1["PRODUCTION_MNGR_ID"]);

    //                            LOTMainSubItemIDs += LOTMainSubitemID + ",";

    //                            value1 = objProject.UpdateLOTTFStatusTwo(LOTTFID, LOTMainSubitemID, Convert.ToInt32(LOTAllStatusAndTypes.EnumStatus.Amendment), remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                            value2 = value2 + value1;

    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        if (value2 > 0)
    //        {
    //            if (!string.IsNullOrEmpty(LOTMainSubItemIDs))
    //                LOTMainSubItemIDs = LOTMainSubItemIDs.TrimEnd(',');
    //            if (value2 == count)
    //            {
    //                int sendMailValue = 0;

    //                LOTSendMail objLOTSendMail = new LOTSendMail();

    //                if (prodMngrID == Convert.ToInt32(Session["EMP_RECORD_ID"]))
    //                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNo.Text, Convert.ToInt32(Session["EMP_RECORD_ID"]), txtRemarks.Text);
    //                else
    //                    sendMailValue = objLOTSendMail.SendEmailLOT(LOTTFID, LOTMainSubItemIDs, txtTFNo.Text, Convert.ToInt32(Session["EMP_RECORD_ID"]), "");

    //                if (sendMailValue > 0)
    //                {
    //                    int val = objProject.UpdateLOTMailStatusTwo(LOTTFID, LOTMainSubItemIDs, statusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //                    SuccessMessage("LOT with TF. No.: '" + TFNo + "' sent to amendment and mail sent successfully.");
    //                }
    //                else
    //                    SuccessMessage("LOT with TF. No.: '" + TFNo + "' sent to amendment successfully.");
    //            }
    //            else if (value2 < count)
    //            {
    //                RemoveSubitems(LOTMainSubitemID);
    //            }
    //        }

    //        #endregion

    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    private void RemoveSubitems(int LOTMainSubitemID)
    {
        if (Session["dsLOTTFDetails"] != null)
            dsLOTTFDetails = (DataSet)Session["dsLOTTFDetails"];


        if (dsLOTTFDetails.Tables.Count > 0 && dsLOTTFDetails.Tables[1].Rows.Count > 0)
        {
            foreach (DataRow dr in dsLOTTFDetails.Tables[1].Select("LOT_MAIN_SUBITEM_ID='" + LOTMainSubitemID + "'"))
            {
                dsLOTTFDetails.Tables[1].Rows.Remove(dr);
            }
        }

        if (dsLOTTFDetails.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < dsLOTTFDetails.Tables[1].Rows.Count; i++)
            {
                dsLOTTFDetails.Tables[1].Rows[i]["SR_NO"] = i + 1;
            }
        }

        gvSubitems.DataSource = dsLOTTFDetails.Tables[1];
        gvSubitems.DataBind();

        lblSubitemsRecords.Text = "Subitems Records[" + gvSubitems.Rows.Count + "]";
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

public enum EnumActID
{
    Approve = 1,
    SendToAmendment = 2
}