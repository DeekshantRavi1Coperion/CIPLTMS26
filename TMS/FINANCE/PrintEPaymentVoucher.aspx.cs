using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class FINANCE_PrintEPaymentVoucher : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    DMSSendMail objDMSSendMail = new DMSSendMail();
    BAL.Finance objFinance = new BAL.Finance();
    PaymentVoucherHtmlForPDF objPaymentVoucherHtmlForPDF = new PaymentVoucherHtmlForPDF();
    PaymentVoucherPrintDocument objPaymentVoucherPrintDocument = new PaymentVoucherPrintDocument();

    DataSet dsPrinters = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsVoucherList = new DataSet();

    int datetTypeID = 0;
    string dateSign = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string unitName = string.Empty;
    string vendorCode = string.Empty;
    string vendorName = string.Empty;
    string factVoucherNo = string.Empty;
    string status = string.Empty;
    string invoiceNo = string.Empty;
    string partyBillNo = string.Empty;
    string orderNo = string.Empty;
    string bankName = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtTimesheet"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                ddlStatus.SelectedValue = "2";
                ddlPaper.SelectedValue = "A4";

                BindCompany();
                BindPrinters();

                GetVoucherList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetVoucherList();
    }

    protected void gvPaymentVoucherList_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvPaymentVoucherList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewVoucherDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblFactVoucherNumber = gvPaymentVoucherList.Rows[rowindex].FindControl("lblFactVoucherNumber") as Label;
                Label lblUnit = gvPaymentVoucherList.Rows[rowindex].FindControl("lblUnit") as Label;

                if (Session["dsVoucherList"] != null)
                    dsVoucherList = (DataSet)Session["dsVoucherList"];


                if (Convert.ToString(e.CommandArgument) == "ViewVoucherDETAIL")
                {
                    txtFactVoucherNumberInDetails.Text = Convert.ToString(lblFactVoucherNumber.Text);
                    mpeVoucherDetail.Show();
                    BindVoucherDetail(dsVoucherList.Tables[1], Convert.ToString(lblFactVoucherNumber.Text));
                }

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    mpeViewInPDF.Show();
                    //iframePaymentVoucherDetailsInPDF.Attributes.Add("src", "PaymentVoucherDetailInPDF.aspx?factvouchernumber=" + Convert.ToString(lblFactVoucherNumber.Text) + "&unit=" + lblUnit.Text + "&status=" + Convert.ToString(ddlStatus.SelectedItem.Text));
                    iframePaymentVoucherDetailsInPDF.Attributes.Add("src", "PaymentVoucherDetailInPDF.aspx?factvouchernumber=" + Convert.ToString(lblFactVoucherNumber.Text) + "&unit=" + "" + "&status=" + Convert.ToString(ddlStatus.SelectedItem.Text));
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

    protected void gvVouchersDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void btnExportPDF_Click(object sender, EventArgs e)
    {
        if (gvPaymentVoucherList.Rows.Count > 0)
        {
            Export();

            foreach (GridViewRow gr in gvPaymentVoucherList.Rows)
            {
                CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                chkSelect.Checked = false;
            }
        }
        else
        {
            ExceptionMessage("No data found...!!!");
            return;
        }
    }

    protected void btnPrintPDF_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            if (gvPaymentVoucherList.Rows.Count > 0)
            {
                PrintVoucher();

                foreach (GridViewRow gr in gvPaymentVoucherList.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                    chkSelect.Checked = false;
                }
            }
            else
            {
                ExceptionMessage("No data found...!!!");
                return;
            }
        }
    }

    #endregion


    #region METHODS[=========================]

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
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindPrinters()
    {
        try
        {
            //DataTable dtPrinters = new DataTable(); 
            dsPrinters = objFinance.GetPrinters();
            DataTable dtInstalledPrinters = new DataTable();
            dtInstalledPrinters.Columns.Add("PRINTER_NAME", typeof(string));

            string printername = string.Empty;

            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
            objScope.Connect();

            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = "Select * from win32_Printer";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();
            int count = 0;
            foreach (ManagementObject mo in MOC)
            {
                count++;
                printername = (mo["Name"].ToString());

                DataRow dr = dtInstalledPrinters.NewRow();
                dr["PRINTER_NAME"] = printername;
                dtInstalledPrinters.Rows.Add(dr);
            }

            if (dtInstalledPrinters.Rows.Count > 0)
            {
                ddlPrinter.DataSource = dtInstalledPrinters;
                ddlPrinter.DataValueField = "PRINTER_NAME";
                ddlPrinter.DataTextField = "PRINTER_NAME";
                ddlPrinter.DataBind();
                ddlPrinter.Items.Insert(0, "Select");
                ddlPrinter.SelectedIndex = 0;

                foreach (DataRow dr in dsPrinters.Tables[0].Select("MANAGER_ID='" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + "'"))
                {
                    foreach (DataRow dri in dtInstalledPrinters.Rows)
                    {
                        if (Convert.ToString(dri["PRINTER_NAME"]) == Convert.ToString(dr["PRINTER_NAME"]))
                        {
                            ddlPrinter.SelectedValue = Convert.ToString(dr["PRINTER_NAME"]);
                        }
                    }
                }
            }
            else
            {
                ddlPrinter.Items.Clear();
                ddlPrinter.Items.Insert(0, "Select");
                ddlPrinter.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }


    private void GetVoucherList()
    {
        try
        {
            datetTypeID = 0;
            dateSign = string.Empty;
            fromDate = string.Empty;
            toDate = string.Empty;

            unitName = string.Empty;
            vendorCode = string.Empty;
            vendorName = string.Empty;
            factVoucherNo = string.Empty;
            status = string.Empty;
            invoiceNo = string.Empty;
            partyBillNo = string.Empty;
            orderNo = string.Empty;
            bankName = string.Empty;

            datetTypeID = Convert.ToInt32(ddlOnWhichDateMainSearch.SelectedValue);
            dateSign = Convert.ToString(ddlSignMainSearch.SelectedValue);

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");

            unitName = Convert.ToString(ddlCompany.SelectedItem.Text);

            if (!string.IsNullOrEmpty(txtVendorCode.Text))
                vendorCode = txtVendorCode.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtVendorName.Text))
                vendorName = txtVendorName.Text.Trim();

            if (!string.IsNullOrEmpty(txtFACTVoucherNo.Text))
                factVoucherNo = txtFACTVoucherNo.Text.Trim();

            status = Convert.ToString(ddlStatus.SelectedItem.Text);

            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                invoiceNo = txtInvoiceNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtPartyBillNo.Text))
                partyBillNo = txtPartyBillNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtOrderNo.Text))
                orderNo = txtOrderNo.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(txtBankName.Text))
                bankName = txtBankName.Text.Trim();

            dsVoucherList = objFinance.GetEPaymentVoucher(datetTypeID, dateSign, fromDate, toDate, unitName, vendorCode,
                                                          vendorName, factVoucherNo, status, invoiceNo, partyBillNo,
                                                          orderNo, bankName);

            if (dsVoucherList.Tables.Count > 0 && dsVoucherList.Tables[0].Rows.Count > 0)
            {
                Session["dsVoucherList"] = dsVoucherList;
                gvPaymentVoucherList.DataSource = dsVoucherList.Tables[0];
                gvPaymentVoucherList.DataBind();
            }
            else
            {
                Session["dsVoucherList"] = null;
                gvPaymentVoucherList.DataSource = null;
                gvPaymentVoucherList.DataBind();
            }
            lblRecords.Text = "Records[" + dsVoucherList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindVoucherDetail(DataTable dtVoucherDetail, string FACT_VoucherNumber)
    {
        try
        {
            int count = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("SrNo", typeof(string));
            dt.Columns.Add("FACT_VoucherNumber", typeof(string));
            dt.Columns.Add("InvoiceNumber", typeof(string));
            dt.Columns.Add("InvoiceDate", typeof(string));
            dt.Columns.Add("InvoiceDueDate", typeof(string));
            dt.Columns.Add("PartyBillNumber", typeof(string));
            dt.Columns.Add("OrderNumber", typeof(string));
            dt.Columns.Add("OrderDate", typeof(string));
            dt.Columns.Add("InvoiceNetAmount", typeof(string));
            dt.Columns.Add("OutstandingAmount", typeof(string));
            dt.Columns.Add("PaymentAmount", typeof(string));
            dt.Columns.Add("Approved_Amount", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("FACT_VoucherStatus", typeof(string));

            if (dtVoucherDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtVoucherDetail.Select("FACT_VoucherNumber='" + FACT_VoucherNumber + "'"))
                {
                    count++;
                    DataRow drn = dt.NewRow();
                    drn["SrNo"] = Convert.ToString(count);
                    drn["FACT_VoucherNumber"] = Convert.ToString(dr["FACT_VoucherNumber"]);
                    drn["InvoiceNumber"] = Convert.ToString(dr["InvoiceNumber"]);
                    drn["InvoiceDate"] = Convert.ToString(dr["InvoiceDate"]);
                    drn["InvoiceDueDate"] = Convert.ToString(dr["InvoiceDueDate"]);
                    drn["PartyBillNumber"] = Convert.ToString(dr["PartyBillNumber"]);
                    drn["OrderNumber"] = Convert.ToString(dr["OrderNumber"]);
                    drn["OrderDate"] = Convert.ToString(dr["OrderDate"]);
                    drn["InvoiceNetAmount"] = Convert.ToString(dr["InvoiceNetAmount"]);
                    drn["OutstandingAmount"] = Convert.ToString(dr["OutstandingAmount"]);
                    drn["PaymentAmount"] = Convert.ToString(dr["PaymentAmount"]);
                    drn["Approved_Amount"] = Convert.ToString(dr["Approved_Amount"]);
                    drn["Status"] = Convert.ToString(dr["Status"]);
                    drn["FACT_VoucherStatus"] = Convert.ToString(dr["FACT_VoucherStatus"]);

                    dt.Rows.Add(drn);
                }

                if (dt.Rows.Count > 0)
                {
                    gvVouchersDetail.DataSource = dt;
                    gvVouchersDetail.DataBind();
                }
                else
                {
                    gvVouchersDetail.DataSource = null;
                    gvVouchersDetail.DataBind();
                }
                lblVouchersSIRerords.Text = "Records[" + dt.Rows.Count + "]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    public Byte[] GetPDFBytes(DataSet dsVoucherList, string factVoucherNumber, int vCount)
    {
        byte[] pdf;
        try
        {
            string fileName = string.Empty;

            var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

            string htmlTxt = string.Empty;
            StringBuilder sb = new StringBuilder();

            htmlTxt = GetPDFDetailAndReturnHTML(dsVoucherList, factVoucherNumber, vCount);

            if (!string.IsNullOrEmpty(htmlTxt))
            {
                fileName = factVoucherNumber + "_Voucher_Detail_" + DateTime.Now.ToString("dd-MMM-yyyy");

                sb.Append("<html>\n");
                sb.Append("<body>\n");
                sb.Append(htmlTxt + "\n");
                sb.Append("</body>\n");
                sb.Append("</html>\n");
            }

            var html = sb.ToString();
            if (!string.IsNullOrEmpty(Convert.ToString(html)))
            {
                string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                img.Alignment = Element.ALIGN_LEFT;
                img.ScaleToFit(180f, 250f);


                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document(PageSize.A4);
                    var writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    document.Add(img);
                    using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                    {
                        using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                        }
                    }

                    document.Close();
                    pdf = memoryStream.GetBuffer();
                }
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return null;
            }
            return pdf;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string GetPDFDetailAndReturnHTML(DataSet dsVoucherList, string factVoucherNumber, int vCount)
    {
        try
        {
            string htmlText = string.Empty;
            if (dsVoucherList.Tables.Count > 0 && dsVoucherList.Tables[0].Rows.Count > 0)
            {
                string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                htmlText = objPaymentVoucherHtmlForPDF.GetHtmlForPDF(dsVoucherList, factVoucherNumber, vCount, imagePath, 0);
            }
            else
            {
                htmlText = string.Empty;
            }

            if (!string.IsNullOrEmpty(htmlText))
                return htmlText;
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }


    private void Export()
    {
        System.Collections.Generic.List<byte[]> pdfFiles = new System.Collections.Generic.List<byte[]>();
        byte[] combinedPDF = null;
        int selectedCount = 0;
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            if (gvPaymentVoucherList.Rows.Count > 0)
            {
                if (Session["dsVoucherList"] != null)
                    dsVoucherList = (DataSet)Session["dsVoucherList"];

                byte[] mergedPdf = null;

                byte[] pdf;
                string fileName = string.Empty;
                string htmlTxt = string.Empty;
                string htmlTxtNew = string.Empty;
                StringBuilder sb = new StringBuilder();

                string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));
                fileName = "Voucher_Detail_" + DateTime.Now.ToString("dd-MMM-yyyy");
                foreach (GridViewRow gr in gvPaymentVoucherList.Rows)
                {
                    CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                    if (chkSelect.Checked)
                    {
                        selectedCount++;
                        Label lblFactVoucherNumber = gr.FindControl("lblFactVoucherNumber") as Label;

                        if (dsVoucherList.Tables.Count > 0 && dsVoucherList.Tables[0].Rows.Count > 0)
                        {
                            htmlTxt = GetPDFDetailAndReturnHTML(dsVoucherList, Convert.ToString(lblFactVoucherNumber.Text), selectedCount);
                            if (!string.IsNullOrEmpty(htmlTxt))
                            {
                                sb.Append("<html>\n");
                                sb.Append("<body>\n");
                                sb.Append(htmlTxt + "\n");
                                sb.Append("</body>\n");
                                sb.Append("</html>\n");
                            }

                            var document = new Document();
                            var html = sb.ToString();
                            if (!string.IsNullOrEmpty(Convert.ToString(html)))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    if (Convert.ToString(ddlPaper.SelectedValue) == "A2")
                                    {
                                        document = new Document(PageSize.A2);
                                    }
                                    else if (Convert.ToString(ddlPaper.SelectedValue) == "A3")
                                    {
                                        document = new Document(PageSize.A3);
                                    }
                                    else if (Convert.ToString(ddlPaper.SelectedValue) == "A4")
                                    {
                                        document = new Document(PageSize.A4);
                                    }

                                    var writer = PdfWriter.GetInstance(document, memoryStream);
                                    document.Open();

                                    //Paragraph para = new Paragraph("Hello world. Checking Header Footer", new Font(Font.FontFamily.HELVETICA, 22));
                                    //para.Alignment = Element.ALIGN_CENTER;
                                    //document.Add(para);

                                    using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                                    {
                                        using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                        {
                                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                                        }
                                    }

                                    document.Close();
                                    pdf = memoryStream.GetBuffer();
                                    pdfFiles.Add(pdf);
                                }
                            }

                        }
                        else
                        {
                            ExceptionMessage("Please refresh page and try again...!!!");
                            return;
                        }
                    }
                }

                if (selectedCount > 0)
                {
                    if (pdfFiles.Count > 0)
                    {
                        combinedPDF = ConvertList(pdfFiles);
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", string.Format("attachment;filename=Vouchers-{0}.pdf", DateTime.Now.ToShortDateString()));
                        Response.BinaryWrite(combinedPDF);
                        Response.End();
                    }
                }
                else
                {
                    ExceptionMessage("Please select atleaset 1 row...!!!");
                    return;
                }
            }
            else
            {
                ExceptionMessage("No data found...!!!");
                return;
            }
        }
    }

    private Byte[] ConvertList(System.Collections.Generic.List<Byte[]> list)
    {
        System.Collections.Generic.List<Byte> tmpList = new System.Collections.Generic.List<byte>();

        foreach (Byte[] byteArray in list)
        {
            foreach (Byte singleByte in byteArray)
            {
                tmpList.Add(singleByte);
            }
        }
        return tmpList.ToArray();
    }

    private void PrintVoucher()
    {
        try
        {
            int printedCount = 0;
            string printerName = string.Empty;
            string paperName = string.Empty;
            int copies = 0;

            printerName = Convert.ToString(ddlPrinter.SelectedValue);
            paperName = Convert.ToString(ddlPaper.SelectedValue);
            copies = Convert.ToInt32(txtCopies.Text);


            int selectedCount = 0;
            if (Convert.ToInt32(hdConfirmValue.Value) > 0)
            {
                if (gvPaymentVoucherList.Rows.Count > 0)
                {
                    if (Session["dsVoucherList"] != null)
                        dsVoucherList = (DataSet)Session["dsVoucherList"];

                    foreach (GridViewRow gr in gvPaymentVoucherList.Rows)
                    {
                        CheckBox chkSelect = gr.FindControl("chkSelect") as CheckBox;
                        if (chkSelect.Checked)
                        {
                            byte[] pdf = null;
                            selectedCount++;
                            Label lblFactVoucherNumber = gr.FindControl("lblFactVoucherNumber") as Label;

                            if (dsVoucherList.Tables.Count > 0 && dsVoucherList.Tables[0].Rows.Count > 0)
                            {
                                pdf = GetPDFBytes(dsVoucherList, Convert.ToString(lblFactVoucherNumber.Text), selectedCount);
                                bool printVal = objPaymentVoucherPrintDocument.PrintPDF(pdf, printerName, paperName, copies);
                                if (printVal)
                                {
                                    printedCount++;
                                }
                            }
                            else
                            {
                                ExceptionMessage("Please refresh page and try again...!!!");
                                return;
                            }
                        }
                    }

                    if (printedCount > 0)
                    {
                        SuccessMessage(printedCount + " Vouchers printed successfully...!!!");
                        return;
                    }
                    else
                    {
                        ExceptionMessage("Please try again...!!!");
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("No data found...!!!");
                    return;
                }
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
