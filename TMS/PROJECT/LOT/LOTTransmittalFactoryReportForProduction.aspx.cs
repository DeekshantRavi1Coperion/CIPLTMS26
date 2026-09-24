using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using BAL;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.tool.xml;

using System.Net.Mime;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;
using System.Xml;
using iTextSharp.tool.xml.css;

public partial class PROJECT_LOT_LOTTransmittalFactoryReportForProduction : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsProductionManagers = new DataSet();
    DataSet dsLOTMainSubItems = new DataSet();
    DataSet dsLOTList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsLOTfor = new DataSet();
    DataSet dsLOTStatus = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string LOTTFNo = string.Empty;
    int statusID = 0;
    int unitID = 0;

    int LOTMainItemID = 0;
    int LOTMainSubitemID = 0;
    string tagNumber = string.Empty;
    string drawingNumber = string.Empty;
    string jobNo = string.Empty;
    string customerName = string.Empty;
    int productionManagerID = 0;

    string productionOrderNo = string.Empty;
    string productCode = string.Empty;
    string productDesc = string.Empty;
    string isPartOfProductionID = string.Empty;

    #endregion


    #region EVENTS START[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanelReviseList();

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tfno"])))
                {
                    txtTFNo.Text = Convert.ToString(Request.QueryString["tfno"]);
                    GetLOTList();
                }

                hdUpdationFlag.Value = "0";
                hdConfirmValue.Value = "0";

                Session["dsLOTList"] = null;

                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;

                BindProductionManagers();
                BindCompany();
                BindStatus();
                BindLOTMainItems();

                GetLOTList();
            }
        }
        else
        {
            Session["dsLOTList"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlLOTMainItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLOTMainItems.SelectedIndex > 0)
        {
            BindLOTMainSubItems(Convert.ToInt32(ddlLOTMainItems.SelectedValue));
        }
        else
        {
            ddlLOTMainSubitems.Items.Clear();
            ddlLOTMainSubitems.Items.Insert(0, "All");
            ddlLOTMainSubitems.SelectedIndex = 0;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetLOTList();
    }

    protected void gvLOTTFList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {

                hdUpdationFlag.Value = "0";
                int LOTTFID = 0;
                int LOTTFSubitemID = 0;
                int rowindex = 0;


                if (Session["dsLOTList"] != null)
                    dsLOTList = (DataSet)Session["dsLOTList"];

                if (Convert.ToString(e.CommandArgument) == "REVISE" ||
                    Convert.ToString(e.CommandArgument) == "ViewSubitemDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4" ||
                    Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT2" ||
                    Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT3" ||
                    Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT4")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblLOTTFID = gvLOTTFList.Rows[rowindex].FindControl("lblLOTTFID") as Label;
                Label lblLOTTFSubitemID = gvLOTTFList.Rows[rowindex].FindControl("lblLOTTFSubitemID") as Label;


                Label lblTFNo = gvLOTTFList.Rows[rowindex].FindControl("lblTFNo") as Label;


                Label lblAttachment1 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment2") as Label;
                Label lblAttachment3 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment3") as Label;
                Label lblAttachment4 = gvLOTTFList.Rows[rowindex].FindControl("lblAttachment4") as Label;


                Label lblSIAttachment1 = gvLOTTFList.Rows[rowindex].FindControl("lblSIAttachment1") as Label;
                Label lblSIAttachment2 = gvLOTTFList.Rows[rowindex].FindControl("lblSIAttachment2") as Label;
                Label lblSIAttachment3 = gvLOTTFList.Rows[rowindex].FindControl("lblSIAttachment3") as Label;
                Label lblSIAttachment4 = gvLOTTFList.Rows[rowindex].FindControl("lblSIAttachment4") as Label;


                LOTTFID = Convert.ToInt32(lblLOTTFID.Text);
                LOTTFSubitemID = Convert.ToInt32(lblLOTTFSubitemID.Text);

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                    ViewDrawingFiles(LOTTFID, "DRAWING1", Convert.ToString(lblAttachment1.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
                    ViewDrawingFiles(LOTTFID, "DRAWING2", Convert.ToString(lblAttachment2.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
                    ViewDrawingFiles(LOTTFID, "DRAWING3", Convert.ToString(lblAttachment3.Text).Trim(), 0);

                else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
                    ViewDrawingFiles(LOTTFID, "DRAWING4", Convert.ToString(lblAttachment4.Text).Trim(), 0);



                if (Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT1")
                    ViewDrawingFiles(0, "DRAWING1", Convert.ToString(lblSIAttachment1.Text).Trim(), LOTTFSubitemID);

                else if (Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT2")
                    ViewDrawingFiles(0, "DRAWING2", Convert.ToString(lblSIAttachment2.Text).Trim(), LOTTFSubitemID);

                else if (Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT3")
                    ViewDrawingFiles(0, "DRAWING3", Convert.ToString(lblSIAttachment3.Text).Trim(), LOTTFSubitemID);

                else if (Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT4")
                    ViewDrawingFiles(0, "DRAWING4", Convert.ToString(lblSIAttachment4.Text).Trim(), LOTTFSubitemID);




                else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    int count = 0;
                    string LOTTFSubitemIDs = string.Empty;

                    foreach (DataRow dr0 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                    {
                        if (Convert.ToInt32(dr0["PE_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["PM_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["CREATED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["AMENDMENT_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["AMENDED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["QUALITY_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["PLANNING_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["AMENDED_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["AMENDED_QUALITY_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
                             Convert.ToInt32(dr0["AMENDED_PLANNING_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                        {
                            foreach (DataRow dr1 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                            {
                                if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
                                {
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                                }
                            }
                        }
                        else
                        {

                            foreach (DataRow dr1 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "' AND IS_PART_OF_PRODUCTION_STATUS_REPORT_ID='1'"))
                            {
                                count++;
                                if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
                                {
                                    LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                                }
                            }
                        }
                    }

                    if (count == 0)
                    {
                        foreach (DataRow dr1 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
                        {
                            if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
                            {
                                LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
                        LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

                    ModalPopupExtender4.Show();
                    iframeViewTravelStatementInPDF.Attributes.Add("src", "LOTTransmittalFactoryInPDF.aspx?LOTTFID=" + LOTTFID + "&LOTTFSubitemIDs=" + LOTTFSubitemIDs + "&pdfType=" + (int)LOTAllStatusAndTypes.EnumPDFType.List);
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvLOTTFList_RowDataBound(object sender, GridViewRowEventArgs e)
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
                #region Additional Attachments

                #endregion
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




                string siAttachment1Extn = string.Empty;
                string siAttachment2Extn = string.Empty;
                string siAttachment3Extn = string.Empty;
                string siAttachment4Extn = string.Empty;

                Label lblSIAttachment1 = (Label)e.Row.FindControl("lblSIAttachment1");
                Label lblSIAttachment2 = (Label)e.Row.FindControl("lblSIAttachment2");
                Label lblSIAttachment3 = (Label)e.Row.FindControl("lblSIAttachment3");
                Label lblSIAttachment4 = (Label)e.Row.FindControl("lblSIAttachment4");

                ImageButton imgSIBtnAttachment1 = (ImageButton)e.Row.FindControl("imgSIBtnAttachment1");
                ImageButton imgSIBtnAttachment2 = (ImageButton)e.Row.FindControl("imgSIBtnAttachment2");
                ImageButton imgSIBtnAttachment3 = (ImageButton)e.Row.FindControl("imgSIBtnAttachment3");
                ImageButton imgSIBtnAttachment4 = (ImageButton)e.Row.FindControl("imgSIBtnAttachment4");

                imgSIBtnAttachment1.Visible = false;
                imgSIBtnAttachment2.Visible = false;
                imgSIBtnAttachment3.Visible = false;
                imgSIBtnAttachment4.Visible = false;

                imgSIBtnAttachment1.ToolTip = string.Empty;
                imgSIBtnAttachment2.ToolTip = string.Empty;
                imgSIBtnAttachment3.ToolTip = string.Empty;
                imgSIBtnAttachment4.ToolTip = string.Empty;


                if (!string.IsNullOrEmpty(lblSIAttachment1.Text))
                {
                    imgSIBtnAttachment1.Visible = true;
                    imgSIBtnAttachment1.ToolTip = lblSIAttachment1.Text;

                    siAttachment1Extn = Convert.ToString(lblSIAttachment1.Text).Split('.').Last();
                    if (siAttachment1Extn == "jpg" || siAttachment1Extn == "jepg" || siAttachment1Extn == "bmp" || siAttachment1Extn == "png" || siAttachment1Extn == "gif" || siAttachment1Extn == "JPG" || siAttachment1Extn == "JPEG" || siAttachment1Extn == "BMP" || siAttachment1Extn == "PNG" || siAttachment1Extn == "GIF")
                    {
                        imgSIBtnAttachment1.ImageUrl = "~/Images/imgicon1.png";
                        imgSIBtnAttachment1.ToolTip = lblSIAttachment1.Text;
                    }
                    else if (siAttachment1Extn == "pdf" || siAttachment1Extn == "PDF")
                    {
                        imgSIBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                        imgSIBtnAttachment1.ToolTip = lblSIAttachment1.Text;
                    }
                }
                else
                {
                    imgSIBtnAttachment1.Visible = false;
                }


                if (!string.IsNullOrEmpty(lblSIAttachment2.Text))
                {
                    imgSIBtnAttachment2.Visible = true;
                    imgSIBtnAttachment2.ToolTip = lblSIAttachment2.Text;

                    siAttachment2Extn = Convert.ToString(lblSIAttachment2.Text).Split('.').Last();
                    if (siAttachment2Extn == "jpg" || siAttachment2Extn == "jepg" || siAttachment2Extn == "bmp" || siAttachment2Extn == "png" || siAttachment2Extn == "gif" || siAttachment2Extn == "JPG" || siAttachment2Extn == "JPEG" || siAttachment2Extn == "BMP" || siAttachment2Extn == "PNG" || siAttachment2Extn == "GIF")
                    {
                        imgSIBtnAttachment2.ImageUrl = "~/Images/imgicon1.png";
                        imgSIBtnAttachment2.ToolTip = lblSIAttachment2.Text;
                    }
                    else if (siAttachment2Extn == "pdf" || siAttachment2Extn == "PDF")
                    {
                        imgSIBtnAttachment2.ImageUrl = "~/Images/pdficon1.png";
                        imgSIBtnAttachment2.ToolTip = lblSIAttachment2.Text;
                    }
                    else if (siAttachment2Extn == "dxf" || siAttachment2Extn == "DXF")
                    {
                        imgSIBtnAttachment2.ImageUrl = "~/Images/LOT/dxf.png";
                        imgSIBtnAttachment2.ToolTip = lblSIAttachment2.Text;
                    }
                    else if (siAttachment2Extn == "dwg" || siAttachment2Extn == "DWG")
                    {
                        imgSIBtnAttachment2.ImageUrl = "~/Images/LOT/dwg.png";
                        imgSIBtnAttachment2.ToolTip = lblSIAttachment2.Text;
                    }
                }
                else
                {
                    imgSIBtnAttachment2.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblSIAttachment3.Text))
                {
                    imgSIBtnAttachment3.Visible = true;
                    imgSIBtnAttachment3.ToolTip = lblSIAttachment3.Text;

                    siAttachment3Extn = Convert.ToString(lblSIAttachment3.Text).Split('.').Last();
                    if (siAttachment3Extn == "jpg" || siAttachment3Extn == "jepg" || siAttachment3Extn == "bmp" || siAttachment3Extn == "png" || siAttachment3Extn == "gif" || siAttachment3Extn == "JPG" || siAttachment3Extn == "JPEG" || siAttachment3Extn == "BMP" || siAttachment3Extn == "PNG" || siAttachment3Extn == "GIF")
                    {
                        imgSIBtnAttachment3.ImageUrl = "~/Images/imgicon1.png";
                        imgSIBtnAttachment3.ToolTip = lblSIAttachment3.Text;
                    }
                    else if (siAttachment3Extn == "pdf" || siAttachment3Extn == "PDF")
                    {
                        imgSIBtnAttachment3.ImageUrl = "~/Images/pdficon1.png";
                        imgSIBtnAttachment3.ToolTip = lblSIAttachment3.Text;
                    }
                }
                else
                {
                    imgSIBtnAttachment3.Visible = false;
                }

                if (!string.IsNullOrEmpty(lblSIAttachment4.Text))
                {
                    imgSIBtnAttachment4.Visible = true;
                    imgSIBtnAttachment4.ToolTip = lblSIAttachment4.Text;

                    siAttachment4Extn = Convert.ToString(lblSIAttachment4.Text).Split('.').Last();
                    if (siAttachment4Extn == "jpg" || siAttachment4Extn == "jepg" || siAttachment4Extn == "bmp" || siAttachment4Extn == "png" || siAttachment4Extn == "gif" || siAttachment4Extn == "JPG" || siAttachment4Extn == "JPEG" || siAttachment4Extn == "BMP" || siAttachment4Extn == "PNG" || siAttachment4Extn == "GIF")
                    {
                        imgSIBtnAttachment4.ImageUrl = "~/Images/imgicon1.png";
                        imgSIBtnAttachment4.ToolTip = lblSIAttachment4.Text;
                    }
                    else if (siAttachment4Extn == "pdf" || siAttachment4Extn == "PDF")
                    {
                        imgSIBtnAttachment4.ImageUrl = "~/Images/pdficon1.png";
                        imgSIBtnAttachment4.ToolTip = lblSIAttachment4.Text;
                    }
                }
                else
                {
                    imgSIBtnAttachment4.Visible = false;
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvLOTTFList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsLOTList"];
            ExportToExcel(ds.Tables[0]);
        }
    }


    #endregion EVENTS END[==================]


    #region METHODS START[==================]

    private void BindProductionManagers()
    {
        try
        {
            dsProductionManagers = objProject.GetProductionManagers();
            if (dsProductionManagers.Tables.Count > 0 && dsProductionManagers.Tables[0].Rows.Count > 0)
            {
                ddlProductionManager.DataSource = dsProductionManagers.Tables[0];
                ddlProductionManager.DataTextField = "PRODUCTION_MANAGER";
                ddlProductionManager.DataValueField = "MANAGER_ID";
                ddlProductionManager.DataBind();

                int count = 0;
                foreach (DataRow dr in dsProductionManagers.Tables[0].Select("MANAGER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                {
                    count++;
                }

                if (count > 0)
                {
                    ddlProductionManager.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
                }
                else
                {
                    ddlProductionManager.SelectedIndex = 0;
                }

                if (Convert.ToString(Session["USER_TYPE"]) == "A")
                {
                    ddlProductionManager.Enabled = true;
                }
                else
                {
                    ddlProductionManager.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCompany()
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
                //ddlCompany.Items.Insert(0, "All");
                //ddlCompany.SelectedIndex = 0;
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

            dsLOTStatus = objProject.GetLOTTFStatusForProduction();
            if (dsLOTStatus.Tables.Count > 0 && dsLOTStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsLOTStatus.Tables[0];
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

    private void BindLOTMainItems()
    {
        try
        {
            dsLOTfor = objProject.GetLotMainItems();
            if (dsLOTfor.Tables.Count > 0 && dsLOTfor.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItems.DataSource = dsLOTfor.Tables[0];
                ddlLOTMainItems.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItems.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItems.DataBind();
                ddlLOTMainItems.Items.Insert(0, "All");
                ddlLOTMainItems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainSubItems(int LOTMainItemID)
    {
        try
        {
            dsLOTMainSubItems = objProject.GetLotMainSubItems(LOTMainItemID, Convert.ToInt32(ddlCompany.SelectedValue));
            if (dsLOTMainSubItems.Tables.Count > 0 && dsLOTMainSubItems.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainSubitems.DataSource = dsLOTMainSubItems.Tables[0];
                ddlLOTMainSubitems.DataTextField = "LOT_MAIN_SUBITEM";
                ddlLOTMainSubitems.DataValueField = "LOT_MAIN_SUBITEM_ID";
                ddlLOTMainSubitems.DataBind();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;
            }
            else
            {
                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetLOTList()
    {
        try
        {
            startDate = string.Empty;
            endDate = string.Empty;
            LOTTFNo = string.Empty;
            statusID = 0;
            unitID = 0;
            jobNo = string.Empty;
            customerName = string.Empty;
            LOTMainItemID = 0;
            LOTMainSubitemID = 0;
            tagNumber = string.Empty;
            drawingNumber = string.Empty;
            productionManagerID = 0;

            productionOrderNo = string.Empty;
            productCode = string.Empty;
            productDesc = string.Empty;
            isPartOfProductionID = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtTFNo.Text))
                LOTTFNo = txtTFNo.Text;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);

            unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;

            if (ddlLOTMainItems.SelectedIndex > 0)
                LOTMainItemID = Convert.ToInt32(ddlLOTMainItems.SelectedValue);

            if (ddlLOTMainSubitems.SelectedIndex > 0)
                LOTMainSubitemID = Convert.ToInt32(ddlLOTMainSubitems.SelectedValue);

            if (!string.IsNullOrEmpty(txtTagNumber.Text))
                tagNumber = txtTagNumber.Text;

            if (!string.IsNullOrEmpty(txtDrawingNumber.Text))
                drawingNumber = txtDrawingNumber.Text;

            productionManagerID = Convert.ToInt32(ddlProductionManager.SelectedValue);


            if (!string.IsNullOrEmpty(txtProductionOrderNo.Text))
                productionOrderNo = txtProductionOrderNo.Text;

            if (!string.IsNullOrEmpty(txtProductCode.Text))
                productCode = txtProductCode.Text;

            if (!string.IsNullOrEmpty(txtProductDesc.Text))
                productDesc = txtProductDesc.Text;

            if (ddlIsPartOfProduction.SelectedIndex > 0)
                isPartOfProductionID = Convert.ToString(ddlIsPartOfProduction.SelectedValue);
            else isPartOfProductionID = string.Empty;

            dsLOTList = objProject.GetLOTReportForProduction(startDate, endDate, LOTTFNo, statusID, unitID, jobNo, customerName,
                                                             LOTMainItemID, LOTMainSubitemID, tagNumber, drawingNumber, productionManagerID,
                                                             productionOrderNo, productCode, productDesc, isPartOfProductionID);

            if (dsLOTList.Tables.Count > 0 && dsLOTList.Tables[0].Rows.Count > 0)
            {
                Session["dsLOTList"] = dsLOTList;
                gvLOTTFList.DataSource = dsLOTList.Tables[0];
                gvLOTTFList.DataBind();
            }
            else
            {
                Session["dsLOTList"] = null;
                gvLOTTFList.DataSource = null;
                gvLOTTFList.DataBind();
            }
            lblRecords.Text = "Records[" + dsLOTList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 23; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";


            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 23; k < dt.Columns.Count; k++)
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


            string fileName = "LOT_Report_For_Production_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void ViewDrawingFiles(int LOTTFID, string fileType, string fileName, int LOTTFSubitemID)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);

                //extn = fileName.Split('.').Last();
                //if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                //{
                //    //imgFile.ImageUrl = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    //mpeShowImageFile.Show();

                //    string url = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                //    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                //}
                //else if (extn == "pdf" || extn == "PDF")
                //{
                //    //iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "");
                //    //mpeShowPDFFile.Show();

                //    string url = "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                //    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                //    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                //}
                //else if (extn == "dwg" || extn == "DWG")
                //{
                //    ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);
                //}
                //else if (extn == "dxf" || extn == "DXF")
                //{
                //    ExportDWGFile(LOTTFID, LOTTFSubitemID, fileType);
                //}
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

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanelReviseList()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }


    #endregion METHODS END[=================]

}