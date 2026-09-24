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

public partial class REPORTS_HISTORICAL_DATA_SaleInvoiceReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    DataSet dsSiReport = new DataSet();

    string _year = "";
    string _billNo = "";
    string _orderNo = "";
    string _jobNo = "";
    string _startDate = "";
    string _endDate = "";
    string _unitName = "";
    string _vcCode = "";
    string _vcName = "";
    string _productCode = "";
    string _productDesc = "";
    string _productGroup = "";
    string _productSubgroup = "";
    string _gstHsnCode = "";
    string _pocNpoc = "";
    string _endMarket = "";
    string _geography = "";



    #endregion



    #region EVENTS START[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();

            if (!IsPostBack)
            {
                hdUpdationFlag.Value = "0";
                hdConfirmValue.Value = "0";

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                Session["dsSiReport"] = null;

                //GetPoReport();
            }
        }
        else
        {
            Session["dsSiReport"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetPoReport();
    }

    //protected void gvPoReport_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (Session["EMP_RECORD_ID"] != null)
    //        {
    //            hdUpdationFlag.Value = "0";
    //            int LOTTFID = 0;
    //            int LOTTFSubitemID = 0;
    //            int rowindex = 0;

    //            if (Session["dsSiReport"] != null)
    //                dsLOTList = (DataSet)Session["dsSiReport"];

    //            if (Convert.ToString(e.CommandArgument) == "REVISE" ||
    //                Convert.ToString(e.CommandArgument) == "ViewSubitemDETAIL" ||
    //                Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
    //                Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
    //                Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2" ||
    //                Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3" ||
    //                Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4" ||
    //                Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT1" ||
    //                Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT2" ||
    //                Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT3" ||
    //                Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT4" ||
    //                Convert.ToString(e.CommandArgument) == "ViewClientAppDrawing")
    //            {
    //                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
    //                rowindex = rowSelect.RowIndex;
    //            }


    //            Label lblLOTTFID = gvPoReport.Rows[rowindex].FindControl("lblLOTTFID") as Label;
    //            Label lblLOTTFSubitemID = gvPoReport.Rows[rowindex].FindControl("lblLOTTFSubitemID") as Label;


    //            Label lblTFNo = gvPoReport.Rows[rowindex].FindControl("lblTFNo") as Label;


    //            Label lblAttachment1 = gvPoReport.Rows[rowindex].FindControl("lblAttachment1") as Label;
    //            Label lblAttachment2 = gvPoReport.Rows[rowindex].FindControl("lblAttachment2") as Label;
    //            Label lblAttachment3 = gvPoReport.Rows[rowindex].FindControl("lblAttachment3") as Label;
    //            Label lblAttachment4 = gvPoReport.Rows[rowindex].FindControl("lblAttachment4") as Label;


    //            Label lblSIAttachment1 = gvPoReport.Rows[rowindex].FindControl("lblSIAttachment1") as Label;
    //            Label lblSIAttachment2 = gvPoReport.Rows[rowindex].FindControl("lblSIAttachment2") as Label;
    //            Label lblSIAttachment3 = gvPoReport.Rows[rowindex].FindControl("lblSIAttachment3") as Label;
    //            Label lblSIAttachment4 = gvPoReport.Rows[rowindex].FindControl("lblSIAttachment4") as Label;

    //            Label lblClientAppDrawing = gvPoReport.Rows[rowindex].FindControl("lblClientAppDrawing") as Label;



    //            LOTTFID = Convert.ToInt32(lblLOTTFID.Text);
    //            LOTTFSubitemID = Convert.ToInt32(lblLOTTFSubitemID.Text);

    //            if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
    //                ViewDrawingFiles(LOTTFID, "DRAWING1", Convert.ToString(lblAttachment1.Text).Trim(), 0);

    //            else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT2")
    //                ViewDrawingFiles(LOTTFID, "DRAWING2", Convert.ToString(lblAttachment2.Text).Trim(), 0);

    //            else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT3")
    //                ViewDrawingFiles(LOTTFID, "DRAWING3", Convert.ToString(lblAttachment3.Text).Trim(), 0);

    //            else if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT4")
    //                ViewDrawingFiles(LOTTFID, "DRAWING4", Convert.ToString(lblAttachment4.Text).Trim(), 0);

    //            else if (Convert.ToString(e.CommandArgument) == "ViewClientAppDrawing")
    //                ViewDrawingFiles(LOTTFID, "CLIENT_APPROVED_DRAWING", Convert.ToString(lblClientAppDrawing.Text).Trim(), 0);


    //            if (Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT1")
    //                ViewDrawingFiles(0, "DRAWING1", Convert.ToString(lblSIAttachment1.Text).Trim(), LOTTFSubitemID);

    //            else if (Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT2")
    //                ViewDrawingFiles(0, "DRAWING2", Convert.ToString(lblSIAttachment2.Text).Trim(), LOTTFSubitemID);

    //            else if (Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT3")
    //                ViewDrawingFiles(0, "DRAWING3", Convert.ToString(lblSIAttachment3.Text).Trim(), LOTTFSubitemID);

    //            else if (Convert.ToString(e.CommandArgument) == "ViewSIATTACHMENT4")
    //                ViewDrawingFiles(0, "DRAWING4", Convert.ToString(lblSIAttachment4.Text).Trim(), LOTTFSubitemID);






    //            else if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
    //            {
    //                int count = 0;
    //                string LOTTFSubitemIDs = string.Empty;

    //                foreach (DataRow dr0 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
    //                {
    //                    if (Convert.ToInt32(dr0["PE_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["PM_ID"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["CREATED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["AMENDMENT_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["AMENDED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["QUALITY_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["PLANNING_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["AMENDED_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["AMENDED_QUALITY_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]) ||
    //                        Convert.ToInt32(dr0["AMENDED_PLANNING_ACCEPTED_BY"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
    //                    {
    //                        foreach (DataRow dr1 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
    //                        {
    //                            if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
    //                            {
    //                                LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {

    //                        foreach (DataRow dr1 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "' AND IS_PART_OF_PRODUCTION_STATUS_REPORT_ID='1'"))
    //                        {
    //                            count++;
    //                            if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
    //                            {
    //                                LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
    //                            }
    //                        }
    //                    }
    //                }

    //                if (count == 0)
    //                {
    //                    foreach (DataRow dr1 in dsLOTList.Tables[0].Select("LOT_TF_ID='" + Convert.ToInt32(lblLOTTFID.Text) + "'"))
    //                    {
    //                        if (!LOTTFSubitemIDs.Contains("," + Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ","))
    //                        {
    //                            LOTTFSubitemIDs += Convert.ToString(dr1["LOT_TF_SUBITEM_ID"]) + ",";
    //                        }
    //                    }
    //                }

    //                if (!string.IsNullOrEmpty(LOTTFSubitemIDs))
    //                    LOTTFSubitemIDs = LOTTFSubitemIDs.TrimEnd(',');

    //                ModalPopupExtender4.Show();
    //                iframeViewTravelStatementInPDF.Attributes.Add("src", "LOTTransmittalFactoryInPDF.aspx?LOTTFID=" + LOTTFID + "&LOTTFSubitemIDs=" + LOTTFSubitemIDs + "&pdfType=" + (int)LOTAllStatusAndTypes.EnumPDFType.List);

    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("~/Login.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //ExceptionMessage
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    //protected void gvPoReport_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.Header)
    //        {
    //            for (int i = 0; i < e.Row.Cells.Count; i++)
    //            {
    //                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
    //            }
    //        }

    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            string attachment1Extn = string.Empty;
    //            string attachment2Extn = string.Empty;
    //            string attachment3Extn = string.Empty;
    //            string attachment4Extn = string.Empty;

    //            Label lblAttachment1 = (Label)e.Row.FindControl("lblAttachment1");
    //            Label lblAttachment2 = (Label)e.Row.FindControl("lblAttachment2");
    //            Label lblAttachment3 = (Label)e.Row.FindControl("lblAttachment3");
    //            Label lblAttachment4 = (Label)e.Row.FindControl("lblAttachment4");

    //            ImageButton imgBtnAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAttachment1");
    //            ImageButton imgBtnAttachment2 = (ImageButton)e.Row.FindControl("imgBtnAttachment2");
    //            ImageButton imgBtnAttachment3 = (ImageButton)e.Row.FindControl("imgBtnAttachment3");
    //            ImageButton imgBtnAttachment4 = (ImageButton)e.Row.FindControl("imgBtnAttachment4");

    //            imgBtnAttachment1.Visible = false;
    //            imgBtnAttachment2.Visible = false;
    //            imgBtnAttachment3.Visible = false;
    //            imgBtnAttachment4.Visible = false;

    //            imgBtnAttachment1.ToolTip = string.Empty;
    //            imgBtnAttachment2.ToolTip = string.Empty;
    //            imgBtnAttachment3.ToolTip = string.Empty;
    //            imgBtnAttachment4.ToolTip = string.Empty;




    //            if (!string.IsNullOrEmpty(lblAttachment1.Text))
    //            {
    //                imgBtnAttachment1.Visible = true;
    //                imgBtnAttachment1.ToolTip = lblAttachment1.Text;

    //                attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
    //                if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
    //                {
    //                    imgBtnAttachment1.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
    //                {
    //                    imgBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //                else if (attachment1Extn == "dxf" || attachment1Extn == "DXF")
    //                {
    //                    imgBtnAttachment1.ImageUrl = "~/Images/LOT/dxf.png";
    //                }
    //                else if (attachment1Extn == "dwg" || attachment1Extn == "DWG")
    //                {
    //                    imgBtnAttachment1.ImageUrl = "~/Images/LOT/dwg.png";
    //                }

    //            }
    //            else
    //            {
    //                imgBtnAttachment1.Visible = false;
    //            }


    //            if (!string.IsNullOrEmpty(lblAttachment2.Text))
    //            {
    //                imgBtnAttachment2.Visible = true;
    //                imgBtnAttachment2.ToolTip = lblAttachment2.Text;

    //                attachment2Extn = Convert.ToString(lblAttachment2.Text).Split('.').Last();
    //                if (attachment2Extn == "jpg" || attachment2Extn == "jepg" || attachment2Extn == "bmp" || attachment2Extn == "png" || attachment2Extn == "gif" || attachment2Extn == "JPG" || attachment2Extn == "JPEG" || attachment2Extn == "BMP" || attachment2Extn == "PNG" || attachment2Extn == "GIF")
    //                {
    //                    imgBtnAttachment2.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (attachment2Extn == "pdf" || attachment2Extn == "PDF")
    //                {
    //                    imgBtnAttachment2.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //                else if (attachment2Extn == "dxf" || attachment2Extn == "DXF")
    //                {
    //                    imgBtnAttachment2.ImageUrl = "~/Images/LOT/dxf.png";
    //                }
    //                else if (attachment2Extn == "dwg" || attachment2Extn == "DWG")
    //                {
    //                    imgBtnAttachment2.ImageUrl = "~/Images/LOT/dwg.png";
    //                }
    //            }
    //            else
    //            {
    //                imgBtnAttachment2.Visible = false;
    //            }

    //            if (!string.IsNullOrEmpty(lblAttachment3.Text))
    //            {
    //                imgBtnAttachment3.Visible = true;
    //                imgBtnAttachment3.ToolTip = lblAttachment3.Text;

    //                attachment3Extn = Convert.ToString(lblAttachment3.Text).Split('.').Last();
    //                if (attachment3Extn == "jpg" || attachment3Extn == "jepg" || attachment3Extn == "bmp" || attachment3Extn == "png" || attachment3Extn == "gif" || attachment3Extn == "JPG" || attachment3Extn == "JPEG" || attachment3Extn == "BMP" || attachment3Extn == "PNG" || attachment3Extn == "GIF")
    //                {
    //                    imgBtnAttachment3.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (attachment3Extn == "pdf" || attachment3Extn == "PDF")
    //                {
    //                    imgBtnAttachment3.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //                else if (attachment3Extn == "dxf" || attachment3Extn == "DXF")
    //                {
    //                    imgBtnAttachment3.ImageUrl = "~/Images/LOT/dxf.png";
    //                }
    //                else if (attachment3Extn == "dwg" || attachment3Extn == "DWG")
    //                {
    //                    imgBtnAttachment3.ImageUrl = "~/Images/LOT/dwg.png";
    //                }
    //            }
    //            else
    //            {
    //                imgBtnAttachment3.Visible = false;
    //            }

    //            if (!string.IsNullOrEmpty(lblAttachment4.Text))
    //            {
    //                imgBtnAttachment4.Visible = true;
    //                imgBtnAttachment4.ToolTip = lblAttachment4.Text;

    //                attachment4Extn = Convert.ToString(lblAttachment4.Text).Split('.').Last();
    //                if (attachment4Extn == "jpg" || attachment4Extn == "jepg" || attachment4Extn == "bmp" || attachment4Extn == "png" || attachment4Extn == "gif" || attachment4Extn == "JPG" || attachment4Extn == "JPEG" || attachment4Extn == "BMP" || attachment4Extn == "PNG" || attachment4Extn == "GIF")
    //                {
    //                    imgBtnAttachment4.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (attachment4Extn == "pdf" || attachment4Extn == "PDF")
    //                {
    //                    imgBtnAttachment4.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //                else if (attachment3Extn == "dxf" || attachment3Extn == "DXF")
    //                {
    //                    imgBtnAttachment4.ImageUrl = "~/Images/LOT/dxf.png";
    //                }
    //                else if (attachment3Extn == "dwg" || attachment3Extn == "DWG")
    //                {
    //                    imgBtnAttachment4.ImageUrl = "~/Images/LOT/dwg.png";
    //                }
    //            }
    //            else
    //            {
    //                imgBtnAttachment4.Visible = false;
    //            }



    //            string siAttachment1Extn = string.Empty;
    //            string siAttachment2Extn = string.Empty;
    //            string siAttachment3Extn = string.Empty;
    //            string siAttachment4Extn = string.Empty;

    //            Label lblSIAttachment1 = (Label)e.Row.FindControl("lblSIAttachment1");
    //            Label lblSIAttachment2 = (Label)e.Row.FindControl("lblSIAttachment2");
    //            Label lblSIAttachment3 = (Label)e.Row.FindControl("lblSIAttachment3");
    //            Label lblSIAttachment4 = (Label)e.Row.FindControl("lblSIAttachment4");

    //            ImageButton imgSIBtnAttachment1 = (ImageButton)e.Row.FindControl("imgSIBtnAttachment1");
    //            ImageButton imgSIBtnAttachment2 = (ImageButton)e.Row.FindControl("imgSIBtnAttachment2");
    //            ImageButton imgSIBtnAttachment3 = (ImageButton)e.Row.FindControl("imgSIBtnAttachment3");
    //            ImageButton imgSIBtnAttachment4 = (ImageButton)e.Row.FindControl("imgSIBtnAttachment4");

    //            imgSIBtnAttachment1.Visible = false;
    //            imgSIBtnAttachment2.Visible = false;
    //            imgSIBtnAttachment3.Visible = false;
    //            imgSIBtnAttachment4.Visible = false;

    //            imgSIBtnAttachment1.ToolTip = string.Empty;
    //            imgSIBtnAttachment2.ToolTip = string.Empty;
    //            imgSIBtnAttachment3.ToolTip = string.Empty;
    //            imgSIBtnAttachment4.ToolTip = string.Empty;


    //            if (!string.IsNullOrEmpty(lblSIAttachment1.Text))
    //            {
    //                imgSIBtnAttachment1.Visible = true;
    //                imgSIBtnAttachment1.ToolTip = lblSIAttachment1.Text;

    //                siAttachment1Extn = Convert.ToString(lblSIAttachment1.Text).Split('.').Last();
    //                if (siAttachment1Extn == "jpg" || siAttachment1Extn == "jepg" || siAttachment1Extn == "bmp" || siAttachment1Extn == "png" || siAttachment1Extn == "gif" || siAttachment1Extn == "JPG" || siAttachment1Extn == "JPEG" || siAttachment1Extn == "BMP" || siAttachment1Extn == "PNG" || siAttachment1Extn == "GIF")
    //                {
    //                    imgSIBtnAttachment1.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (siAttachment1Extn == "pdf" || siAttachment1Extn == "PDF")
    //                {
    //                    imgSIBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //                else if (siAttachment1Extn == "dxf" || siAttachment1Extn == "DXF")
    //                {
    //                    imgSIBtnAttachment1.ImageUrl = "~/Images/LOT/dxf.png";
    //                }
    //                else if (siAttachment1Extn == "dwg" || siAttachment1Extn == "DWG")
    //                {
    //                    imgSIBtnAttachment1.ImageUrl = "~/Images/LOT/dwg.png";
    //                }
    //            }
    //            else
    //            {
    //                imgSIBtnAttachment1.Visible = false;
    //            }


    //            if (!string.IsNullOrEmpty(lblSIAttachment2.Text))
    //            {
    //                imgSIBtnAttachment2.Visible = true;
    //                imgSIBtnAttachment2.ToolTip = lblSIAttachment2.Text;

    //                siAttachment2Extn = Convert.ToString(lblSIAttachment2.Text).Split('.').Last();
    //                if (siAttachment2Extn == "jpg" || siAttachment2Extn == "jepg" || siAttachment2Extn == "bmp" || siAttachment2Extn == "png" || siAttachment2Extn == "gif" || siAttachment2Extn == "JPG" || siAttachment2Extn == "JPEG" || siAttachment2Extn == "BMP" || siAttachment2Extn == "PNG" || siAttachment2Extn == "GIF")
    //                {
    //                    imgSIBtnAttachment2.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (siAttachment2Extn == "pdf" || siAttachment2Extn == "PDF")
    //                {
    //                    imgSIBtnAttachment2.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //                else if (siAttachment2Extn == "dxf" || siAttachment2Extn == "DXF")
    //                {
    //                    imgSIBtnAttachment2.ImageUrl = "~/Images/LOT/dxf.png";
    //                }
    //                else if (siAttachment2Extn == "dwg" || siAttachment2Extn == "DWG")
    //                {
    //                    imgSIBtnAttachment2.ImageUrl = "~/Images/LOT/dwg.png";
    //                }
    //            }
    //            else
    //            {
    //                imgSIBtnAttachment2.Visible = false;
    //            }


    //            if (!string.IsNullOrEmpty(lblSIAttachment3.Text))
    //            {
    //                imgSIBtnAttachment3.Visible = true;
    //                imgSIBtnAttachment3.ToolTip = lblSIAttachment3.Text;

    //                siAttachment3Extn = Convert.ToString(lblSIAttachment3.Text).Split('.').Last();
    //                if (siAttachment3Extn == "jpg" || siAttachment3Extn == "jepg" || siAttachment3Extn == "bmp" || siAttachment3Extn == "png" || siAttachment3Extn == "gif" || siAttachment3Extn == "JPG" || siAttachment3Extn == "JPEG" || siAttachment3Extn == "BMP" || siAttachment3Extn == "PNG" || siAttachment3Extn == "GIF")
    //                {
    //                    imgSIBtnAttachment3.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (siAttachment3Extn == "pdf" || siAttachment3Extn == "PDF")
    //                {
    //                    imgSIBtnAttachment3.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //            }
    //            else
    //            {
    //                imgSIBtnAttachment3.Visible = false;
    //            }


    //            if (!string.IsNullOrEmpty(lblSIAttachment4.Text))
    //            {
    //                imgSIBtnAttachment4.Visible = true;
    //                imgSIBtnAttachment4.ToolTip = lblSIAttachment4.Text;

    //                siAttachment4Extn = Convert.ToString(lblSIAttachment4.Text).Split('.').Last();
    //                if (siAttachment4Extn == "jpg" || siAttachment4Extn == "jepg" || siAttachment4Extn == "bmp" || siAttachment4Extn == "png" || siAttachment4Extn == "gif" || siAttachment4Extn == "JPG" || siAttachment4Extn == "JPEG" || siAttachment4Extn == "BMP" || siAttachment4Extn == "PNG" || siAttachment4Extn == "GIF")
    //                {
    //                    imgSIBtnAttachment4.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (siAttachment4Extn == "pdf" || siAttachment4Extn == "PDF")
    //                {
    //                    imgSIBtnAttachment4.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //            }
    //            else
    //            {
    //                imgSIBtnAttachment4.Visible = false;
    //            }



    //            string clientAppDrawingExtn = string.Empty;
    //            Label lblClientAppDrawing = (Label)e.Row.FindControl("lblClientAppDrawing");
    //            ImageButton imgClientAppDrawing = (ImageButton)e.Row.FindControl("imgClientAppDrawing");

    //            imgClientAppDrawing.Visible = false;
    //            imgClientAppDrawing.ToolTip = string.Empty;

    //            if (!string.IsNullOrEmpty(lblClientAppDrawing.Text))
    //            {
    //                imgClientAppDrawing.Visible = true;
    //                imgClientAppDrawing.ToolTip = lblClientAppDrawing.Text;

    //                clientAppDrawingExtn = Convert.ToString(lblClientAppDrawing.Text).Split('.').Last();
    //                if (clientAppDrawingExtn == "jpg" ||
    //                    clientAppDrawingExtn == "jepg" ||
    //                    clientAppDrawingExtn == "bmp" ||
    //                    clientAppDrawingExtn == "png" ||
    //                    clientAppDrawingExtn == "gif" ||
    //                    clientAppDrawingExtn == "JPG" ||
    //                    clientAppDrawingExtn == "JPEG" ||
    //                    clientAppDrawingExtn == "BMP" ||
    //                    clientAppDrawingExtn == "PNG" ||
    //                    clientAppDrawingExtn == "GIF")
    //                {
    //                    imgClientAppDrawing.ImageUrl = "~/Images/imgicon1.png";
    //                }
    //                else if (clientAppDrawingExtn == "pdf" || clientAppDrawingExtn == "PDF")
    //                {
    //                    imgClientAppDrawing.ImageUrl = "~/Images/pdficon1.png";
    //                }
    //                else if (clientAppDrawingExtn == "dxf" || clientAppDrawingExtn == "DXF")
    //                {
    //                    imgClientAppDrawing.ImageUrl = "~/Images/LOT/dxf.png";
    //                }
    //                else if (clientAppDrawingExtn == "dwg" || clientAppDrawingExtn == "DWG")
    //                {
    //                    imgClientAppDrawing.ImageUrl = "~/Images/LOT/dwg.png";
    //                }
    //            }
    //            else
    //            {
    //                imgClientAppDrawing.Visible = false;
    //            }

    //            string txt = string.Empty;
    //            string categoryTxt = string.Empty;

    //            Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
    //            Label lblCategory = (Label)e.Row.FindControl("lblCategory");


    //            if (!string.IsNullOrEmpty(lblCategoryID.Text))
    //            {
    //                string[] srtCategoryID = lblCategoryID.Text.Split(',');
    //                foreach (string i in srtCategoryID)
    //                {
    //                    if (Convert.ToInt32(i) > 0)
    //                    {
    //                        if (Convert.ToInt32(i) == 1)
    //                            txt = "Fabrication";
    //                        else if (Convert.ToInt32(i) == 2)
    //                            txt = "Inspection";
    //                        else if (Convert.ToInt32(i) == 3)
    //                            txt = "Information";
    //                    }

    //                    categoryTxt += txt + ",";
    //                }
    //            }

    //            if (!string.IsNullOrEmpty(categoryTxt))
    //            {
    //                categoryTxt = categoryTxt.TrimEnd(',');
    //            }

    //            if (!string.IsNullOrEmpty(categoryTxt))
    //            {
    //                lblCategory.Text = categoryTxt;
    //            }


    //            for (int i = 0; i < e.Row.Cells.Count; i++)
    //            {
    //                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    protected void chkClearDates_CheckedChanged(object sender, EventArgs e)
    {
        if (chkClearDates.Checked)
        {
            txtStartDateSearch.Text = "";
            hdStartDateSearch.Value = "";

            txtEndDateSearch.Text = "";
            hdEndDateSearch.Value = "";

        }
        else
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
            txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

            var endDate = startDate.AddMonths(1).AddDays(-1);
            hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
            txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvSiReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["dsSiReport"];
            ExportToExcel(ds.Tables[0]);
        }
    }


    #endregion EVENTS END[==================]



    #region METHODS START[==================]

    private void GetPoReport()
    {
        try
        {

            _year = "";
            _billNo = "";
            _orderNo = "";
            _jobNo = "";
            _startDate = "";
            _endDate = "";
            _unitName = "";
            _vcCode = "";
            _vcName = "";
            _productCode = "";
            _productDesc = "";
            _productGroup = "";
            _productSubgroup = "";
            _gstHsnCode = "";
            _pocNpoc = "";
            _endMarket = "";
            _geography = "";

            if (ddlYear.SelectedIndex > 0) _year = ddlYear.SelectedValue;
            if (!string.IsNullOrEmpty(txtBillNumber.Text)) _billNo = txtBillNumber.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txtOrderNumber.Text)) _orderNo = txtOrderNumber.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txtJOBNo.Text)) _jobNo = txtJOBNo.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txtStartDateSearch.Text)) _startDate = Convert.ToDateTime(txtStartDateSearch.Text).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(txtEndDateSearch.Text)) _endDate = Convert.ToDateTime(txtEndDateSearch.Text).ToString("yyyy-MM-dd");
            if (ddlUnit.SelectedIndex > 0) _unitName = ddlUnit.SelectedValue;
            if (!string.IsNullOrEmpty(txtCustomerCode.Text)) _vcCode = txtCustomerCode.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txtCustomerName.Text)) _vcName = txtCustomerName.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txtProductCode.Text)) _productCode = txtProductCode.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txtProductDesc.Text)) _productDesc = txtProductDesc.Text.Trim();
            if (!string.IsNullOrEmpty(txtProductGroup.Text)) _productGroup = txtProductGroup.Text.Trim();
            if (!string.IsNullOrEmpty(txtProductSubgroup.Text)) _productSubgroup = txtProductSubgroup.Text.Trim();
            if (!string.IsNullOrEmpty(txtGstHsnCode.Text)) _gstHsnCode = txtGstHsnCode.Text.Trim().ToUpper();
            if (ddlPocNpoc.SelectedIndex > 0) _pocNpoc = ddlPocNpoc.SelectedValue;
            if (!string.IsNullOrEmpty(txtEndMarket.Text)) _endMarket = txtEndMarket.Text.Trim();
            if (!string.IsNullOrEmpty(txtGeography.Text)) _geography = txtGeography.Text.Trim();


            dsSiReport = objProject.GetHistoricalSIReport
            (
                  _year
                , _billNo
                , _orderNo
                , _jobNo
                , _startDate
                , _endDate
                , _unitName
                , _vcCode
                , _vcName
                , _productCode
                , _productDesc
                , _productGroup
                , _productSubgroup
                , _gstHsnCode
                , _pocNpoc
                , _endMarket
                , _geography
            );

            if (dsSiReport.Tables.Count > 0 && dsSiReport.Tables[0].Rows.Count > 0)
            {
                Session["dsSiReport"] = dsSiReport;
                gvSiReport.DataSource = dsSiReport.Tables[0];
                gvSiReport.DataBind();
            }
            else
            {
                Session["dsSiReport"] = null;
                gvSiReport.DataSource = null;
                gvSiReport.DataBind();
            }
            lblRecords.Text = "Records[" + dsSiReport.Tables[0].Rows.Count + "]";
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

            for (int i = 1; i < gvSiReport.Columns.Count; i++)
            {
                csv += Convert.ToString(gvSiReport.Columns[i].HeaderText) + ',';
            }


            csv += "\r\n";


            string rowTxt = "";

            foreach (GridViewRow gr in gvSiReport.Rows)
            {
                for (int j = 1; j < gvSiReport.Columns.Count; j++)
                {

                    if (!string.IsNullOrEmpty(Convert.ToString(gr.Cells[j].Text)) && Convert.ToString(gr.Cells[j].Text) != "&nbsp;")
                        rowTxt = Convert.ToString(gr.Cells[j].Text);
                    else
                        rowTxt = string.Empty;

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';

                }
                csv += "\r\n";
            }




            string fileName = "Historical_SI_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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


    #endregion METHODS END[=================]

}