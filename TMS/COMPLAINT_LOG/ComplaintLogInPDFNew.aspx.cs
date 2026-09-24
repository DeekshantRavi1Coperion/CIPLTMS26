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
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using BAL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class COMPLAINT_LOG_ComplaintLogInPDFNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewDetail();
        }
    }

    private void ViewDetail()
    {
        try
        {
            BAL.ComplaintLog objComplaintLog = new ComplaintLog();
            DataSet ds = new DataSet();
            if (Request.QueryString["complaintLogID"] != null)
            {
                ds = objComplaintLog.GetComplaintLogForPDF(Convert.ToInt32(Request.QueryString["complaintLogID"]));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["COMPLAINT_LOG_NO"] != DBNull.Value)
                        {
                            lblComplaintNo.Text = Convert.ToString(dr["COMPLAINT_LOG_NO"]);
                            ViewState["complaintLogNo"] = lblComplaintNo.Text;
                        }

                        if (dr["COMPLAINT_RECEIVED_ON"] != DBNull.Value)
                            lblDateOfComplaintReceived.Text = Convert.ToString(dr["COMPLAINT_RECEIVED_ON"]);

                        if (dr["CUSTOMER_NAME"] != DBNull.Value)
                            lblCustomerName.Text = Convert.ToString(dr["CUSTOMER_NAME"]);

                        if (dr["CUSTOMER_CODE"] != DBNull.Value)
                            lblCustomerCode.Text = Convert.ToString(dr["CUSTOMER_CODE"]);

                        if (dr["ADDRESS"] != DBNull.Value)
                            lblAddress.Text = Convert.ToString(dr["ADDRESS"]);

                        if (dr["LOCATION"] != DBNull.Value)
                            lblLocation.Text = Convert.ToString(dr["LOCATION"]);

                        if (dr["PLANT"] != DBNull.Value)
                            lblPlant.Text = Convert.ToString(dr["PLANT"]);

                        if (dr["JOB_NO"] != DBNull.Value)
                            lblJobNo.Text = Convert.ToString(dr["JOB_NO"]);

                        if (dr["PO_NO"] != DBNull.Value)
                            lblCustomerPONo.Text = Convert.ToString(dr["PO_NO"]);

                        if (dr["ITEM_NAME"] != DBNull.Value)
                            lblItemName.Text = Convert.ToString(dr["ITEM_NAME"]);

                        if (dr["MODEL_NO"] != DBNull.Value)
                            lblModelNo.Text = Convert.ToString(dr["MODEL_NO"]);

                        if (dr["SERVICE_TYPE"] != DBNull.Value)
                            lblServiceType.Text = Convert.ToString(dr["SERVICE_TYPE"]);

                        if (dr["COMPLAINT_DESCRIPTION"] != DBNull.Value)
                            lblComplaintDescription.Text = Convert.ToString(dr["COMPLAINT_DESCRIPTION"]);

                        if (dr["DEPARTMENT_NAME"] != DBNull.Value)
                            lblResponsibleDepartment.Text = Convert.ToString(dr["DEPARTMENT_NAME"]);

                        if (dr["RESPONSIBLE_PERSON"] != DBNull.Value)
                            lblResponsiblePerson.Text = Convert.ToString(dr["RESPONSIBLE_PERSON"]);

                        if (dr["BUSINESS_UNIT"] != DBNull.Value)
                            lblBusinessUnit.Text = Convert.ToString(dr["BUSINESS_UNIT"]);

                        if (dr["MANUFACTURER_NAME"] != DBNull.Value)
                            lblEquipmentManufacturerName.Text = Convert.ToString(dr["MANUFACTURER_NAME"]);

                        if (dr["PROPOSED_ACTIONS"] != DBNull.Value)
                            lblProposedActions.Text = Convert.ToString(dr["PROPOSED_ACTIONS"]);

                        if (dr["ROOT_CAUSE"] != DBNull.Value)
                            lblRootCause.Text = Convert.ToString(dr["ROOT_CAUSE"]);

                        if (dr["TARGET_COMPLETION_DATE"] != DBNull.Value)
                            lblTargetCompletionDate.Text = Convert.ToString(dr["TARGET_COMPLETION_DATE"]);

                        if (dr["ACTUAL_COMPLETION_DATE"] != DBNull.Value)
                            lblActualCompletionDate.Text = Convert.ToString(dr["ACTUAL_COMPLETION_DATE"]);

                        if (dr["CORRECTIVE_ACTION"] != DBNull.Value)
                            lblCorrectiveAction.Text = Convert.ToString(dr["CORRECTIVE_ACTION"]);

                        if (dr["RESP_PERSON_LESSON_LEARNT"] != DBNull.Value)
                            lblRespPersonLessonLearnt.Text = Convert.ToString(dr["RESP_PERSON_LESSON_LEARNT"]);    
                                           
                        if (dr["RESP_DEPT_HOD_LESSON_LEARNT"] != DBNull.Value)
                            lblRespDeptHODLessonLearnt.Text = Convert.ToString(dr["RESP_DEPT_HOD_LESSON_LEARNT"]);

                        if (dr["SERVICE_DEPT_HOD_LESSON_LEARNT"] != DBNull.Value)
                            lblServiceDeptHODLessonLearnt.Text = Convert.ToString(dr["SERVICE_DEPT_HOD_LESSON_LEARNT"]); 

                    }
                }
            }
        }
        catch (Exception)
        {
            //
        }
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Complaint_Log_" + Convert.ToString(ViewState["complaintLogNo"]) + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 50f, 0f);//(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        catch (Exception)
        {
            //
        }
    }
}
