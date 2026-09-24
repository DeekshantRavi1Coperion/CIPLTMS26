using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI.HtmlControls;
using iTextSharp.text.html;
using System.Collections;
using System.Net;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.Text;

public partial class REPORTS_CombinedProcProdDesignViewReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    DataSet dsReport = new DataSet();

    string JOBNo = string.Empty;
    int typeID = 0;


    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetReport();
    }

    protected void btnExportProcurementReport_Click(object sender, EventArgs e)
    {
        if (gvProcurementViewReport.Rows.Count > 0)
        {
            ExportProcurementViewToPDF();
        }
    }

    protected void btnExportProductionReport_Click(object sender, EventArgs e)
    {
        if (gvProductionViewReport.Rows.Count > 0)
        {
            ExportProductionViewToPDF();
        }
    }

    protected void btnExportDesignReport_Click(object sender, EventArgs e)
    {
        if (gvDesignViewReport.Rows.Count > 0)
        {
            ExportDesignViewToPDF();
        }
    }


    #endregion


    #region METHODS[=========================]


    private void GetReport()
    {
        try
        {
            JOBNo = string.Empty;
            typeID = 0;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text;

            if (ddlType.SelectedIndex > 0)
                typeID = Convert.ToInt32(ddlType.SelectedValue);

            dsReport = objReports.GetCombinedProcProdDesignReport(JOBNo, typeID);

            gvProcurementViewReport.DataSource = null;
            gvProductionViewReport.DataSource = null;
            gvDesignViewReport.DataSource = null;

            if (dsReport.Tables.Count > 0)
            {
                for (int i = 0; i < dsReport.Tables.Count; i++)
                {
                    if (i == 0)
                    {
                        if (dsReport.Tables[i].Rows.Count > 0)
                        {
                            gvProcurementViewReport.DataSource = dsReport.Tables[i];
                            gvProcurementViewReport.DataBind();
                            lblProcurementViewRecords.Text = "Procurement View Records [" + dsReport.Tables[0].Rows.Count + "]";
                        }
                        else
                            lblProcurementViewRecords.Text = "Procurement View Records [0]";
                    }

                    if (i == 1)
                    {
                        if (dsReport.Tables[i].Rows.Count > 0)
                        {
                            gvProductionViewReport.DataSource = dsReport.Tables[i];
                            gvProductionViewReport.DataBind();
                            lblProductionViewRecords.Text = "Production View Records [" + dsReport.Tables[i].Rows.Count + "]";
                        }
                        else
                            lblProductionViewRecords.Text = "Production View Records [0]";
                    }

                    if (i == 2)
                    {
                        if (dsReport.Tables[i].Rows.Count > 0)
                        {
                            gvDesignViewReport.DataSource = dsReport.Tables[i];
                            gvDesignViewReport.DataBind();
                            lblDesignViewRecords.Text = "Design View Records [" + dsReport.Tables[i].Rows.Count + "]";
                        }
                        else
                            lblDesignViewRecords.Text = "Design View Records [0]";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportProcurementViewToPDF()
    {
        try
        {            
            //iTextSharp.text.Table table = new iTextSharp.text.Table(gvProcurementViewReport.Columns.Count);
            iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvProcurementViewReport.Columns.Count);

            //table.Cellpadding = 2;        
            //table.Width = 100;
            for (int i = 0; i < gvProcurementViewReport.Columns.Count; i++)
            {
                //string cellText = Server.HtmlDecode(gvProcurementViewReport.Columns[i].HeaderText);
                //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                //cell.BackgroundColor = new Color(System.Drawing.ColorTranslator.FromHtml("#cccccc"));                            
                table.AddCell(gvProcurementViewReport.Columns[i].HeaderText);
                float[] columnWidths = new float[] { 20f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table.SetWidths(columnWidths);
            }

            for (int i = 0; i < gvProcurementViewReport.Rows.Count; i++)
            {
                if (gvProcurementViewReport.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    for (int j = 0; j < gvProcurementViewReport.Columns.Count; j++)
                    {
                        //string cellText = Server.HtmlDecode(gvProcurementViewReport.Rows[i].Cells[j].Text);
                        //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);

                        string cellText = string.Empty;
                        cellText = gvProcurementViewReport.Rows[i].Cells[j].Text.Trim();
                        if (cellText == "&nbsp;" || cellText == "01-Jan-1900")
                            cellText = string.Empty;

                        table.AddCell(cellText);
                        float[] columnWidths = new float[] { 20f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table.SetWidths(columnWidths);
                    }
                }
            }

            Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(table);
            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" + "filename=Procurement_Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void ExportProductionViewToPDF()
    {
        //iTextSharp.text.Table table = new iTextSharp.text.Table(gvProductionViewReport.Columns.Count);
        iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvProductionViewReport.Columns.Count);
        //table.Cellpadding = 2;        
        //table.Width = 100;
        for (int i = 0; i < gvProductionViewReport.Columns.Count; i++)
        {
            //string cellText = Server.HtmlDecode(gvProductionViewReport.Columns[i].HeaderText);
            //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
            //cell.BackgroundColor = new Color(System.Drawing.ColorTranslator.FromHtml("#cccccc"));            
            table.TotalWidth = 100;
            table.AddCell(gvProductionViewReport.Columns[i].HeaderText);
        }

        for (int i = 0; i < gvProductionViewReport.Rows.Count; i++)
        {
            if (gvProductionViewReport.Rows[i].RowType == DataControlRowType.DataRow)
            {
                for (int j = 0; j < gvProductionViewReport.Columns.Count; j++)
                {
                    //string cellText = Server.HtmlDecode(gvProductionViewReport.Rows[i].Cells[j].Text);
                    //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);

                    string cellText = string.Empty;
                    cellText = gvProductionViewReport.Rows[i].Cells[j].Text.Trim();
                    if (cellText == "&nbsp;" || cellText == "01-Jan-1900")
                        cellText = string.Empty;

                    table.AddCell(cellText);
                }
            }
        }

        Document pdfDoc = new Document(PageSize.A2, 0f, 0f, 10f, 0f);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        pdfDoc.Add(table);
        pdfDoc.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;" + "filename=Production_Report.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();
    }

    private void ExportDesignViewToPDF()
    {
        //iTextSharp.text.Table table = new iTextSharp.text.Table(gvDesignViewReport.Columns.Count);
        iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvDesignViewReport.Columns.Count);
        //table.Cellpadding = 2;        
        //table.Width = 100;
        for (int i = 0; i < gvDesignViewReport.Columns.Count; i++)
        {
            //string cellText = Server.HtmlDecode(gvDesignViewReport.Columns[i].HeaderText);
            //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
            //cell.BackgroundColor = new Color(System.Drawing.ColorTranslator.FromHtml("#cccccc"));            
            table.TotalWidth = 100;
            table.AddCell(gvDesignViewReport.Columns[i].HeaderText);
        }

        for (int i = 0; i < gvDesignViewReport.Rows.Count; i++)
        {
            if (gvDesignViewReport.Rows[i].RowType == DataControlRowType.DataRow)
            {
                for (int j = 0; j < gvDesignViewReport.Columns.Count; j++)
                {
                    //string cellText = Server.HtmlDecode(gvDesignViewReport.Rows[i].Cells[j].Text);
                    //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);

                    string cellText = string.Empty;
                    cellText = gvDesignViewReport.Rows[i].Cells[j].Text.Trim();
                    if (cellText == "&nbsp;" || cellText == "01-Jan-1900")
                        cellText = string.Empty;

                    table.AddCell(cellText);
                }
            }
        }

        Document pdfDoc = new Document(PageSize.A2, 0f, 0f, 10f, 0f);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        pdfDoc.Add(table);
        pdfDoc.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;" + "filename=Design_Report.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();
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
        lblMsg.Text = string.Empty; ;
    }

    #endregion

}
