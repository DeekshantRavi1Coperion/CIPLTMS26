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

public partial class PROJECT_LOT_FabricationReport1 : System.Web.UI.Page
{

    #region VARIABLES[=====================]


    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new Project();

    DataSet dsUnit = new DataSet();    
    DataSet dsEquipmentOrType = new DataSet();
    DataSet dsFabricationList = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string internalFabricationNo = string.Empty;
    int unitID = 0;
    int equipmentID = 0;
    string drawingNumber = string.Empty;
    int percentageWorkDone = 0;
    string productionOrderNo = string.Empty;

    #endregion



    #region EVENTS START[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();

            if (!IsPostBack)
            {
                Session["dtFabricationList"] = null;
                BindCompany();
                BindEquipmentOrType();
                //GetFabricationReport();
            }
        }
        else
        {
            Session["dtFabricationList"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetFabricationReport();
    }

    protected void gvFabricationList_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (gvFabricationList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtFabricationList"];
            ExportToExcel(dt);
        }
    }


    #endregion EVENTS END[==================]



    #region METHODS START[==================]

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
   
    private void BindEquipmentOrType()
    {
        try
        {
            dsEquipmentOrType = objProject.GetEquipmentOrType();
            if (dsEquipmentOrType.Tables.Count > 0 && dsEquipmentOrType.Tables[0].Rows.Count > 0)
            {
                ddlEquipmentOrType.DataSource = dsEquipmentOrType.Tables[0];
                ddlEquipmentOrType.DataTextField = "EQUIPMENT_OR_TYPE";
                ddlEquipmentOrType.DataValueField = "EQUIPMENT_OR_TYPE_ID";
                ddlEquipmentOrType.DataBind();
                ddlEquipmentOrType.Items.Insert(0, "All");
                ddlEquipmentOrType.SelectedIndex = 0;
            }
            else
            {
                ddlEquipmentOrType.Items.Clear();
                ddlEquipmentOrType.Items.Insert(0, "All");
                ddlEquipmentOrType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetFabricationReport()
    {
        try
        {
            startDate = string.Empty;
            endDate = string.Empty;
            internalFabricationNo = string.Empty;
            unitID = 0;
            equipmentID = 0;
            drawingNumber = string.Empty;
            percentageWorkDone = 0;
            productionOrderNo = string.Empty;


            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtInternalFabricationNo.Text))
                internalFabricationNo = txtInternalFabricationNo.Text;

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlEquipmentOrType.SelectedIndex > 0)
                equipmentID = Convert.ToInt32(ddlEquipmentOrType.SelectedValue);

            if (!string.IsNullOrEmpty(txtDrawingNumber.Text))
                drawingNumber = txtDrawingNumber.Text;

            if (!string.IsNullOrEmpty(txtPercentageWorkDone.Text))
                percentageWorkDone = Convert.ToInt32(txtPercentageWorkDone.Text);

            if (!string.IsNullOrEmpty(txtProductionOrderNo.Text))
                productionOrderNo = txtProductionOrderNo.Text;


            dsFabricationList = objProject.GetFabricationReport(startDate, endDate, internalFabricationNo, unitID, equipmentID, drawingNumber, 
                                                                percentageWorkDone, productionOrderNo);

            if (dsFabricationList.Tables.Count > 0 && dsFabricationList.Tables[0].Rows.Count > 0)
            {
                Session["dtFabricationList"] = dsFabricationList.Tables[0];
                gvFabricationList.DataSource = dsFabricationList.Tables[0];
                gvFabricationList.DataBind();
            }
            else
            {
                Session["dtFabricationList"] = null;
                gvFabricationList.DataSource = null;
                gvFabricationList.DataBind();
            }
            lblRecords.Text = "Records[" + dsFabricationList.Tables[0].Rows.Count + "]";
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

            //for (int i = 1; i < gvFabricationList.Columns.Count; i++)
            //{
            //    csv += Convert.ToString(gvFabricationList.Columns[i].HeaderText) + ',';
            //}

            for (int i = 11; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";



            //foreach (GridViewRow gr in gvFabricationList.Rows)
            //{
            //    for (int j = 1; j < gvFabricationList.Columns.Count; j++)
            //    {

            //        if (!string.IsNullOrEmpty(Convert.ToString(gr.Cells[j].Text)) && Convert.ToString(gr.Cells[j].Text) != "&nbsp;")
            //            rowTxt = Convert.ToString(gr.Cells[j].Text);
            //        else
            //            rowTxt = string.Empty;

            //        csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';

            //    }
            //    csv += "\r\n";
            //}


            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 11; k < dt.Columns.Count; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "LOT_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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