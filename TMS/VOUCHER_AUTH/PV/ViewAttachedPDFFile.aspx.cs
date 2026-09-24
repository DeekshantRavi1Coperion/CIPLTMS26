using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VOUCHER_AUTH_PV_ViewAttachedPDFFile : System.Web.UI.Page
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["docID"] != null)
                {
                    int fileType = Convert.ToInt32(Request.QueryString["fileType"]);
                    dsFiles = objVouchersAuthorization.GetAttachedPoDocumentFile(Convert.ToInt32(Request.QueryString["docID"]), fileType);
                    if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsFiles.Tables[0].Rows[0];

                        string fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);
                        byte[] bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];

                        if (bytes != null && bytes.Length > 0)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", bytes.Length.ToString());
                            Response.BinaryWrite(bytes);
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
