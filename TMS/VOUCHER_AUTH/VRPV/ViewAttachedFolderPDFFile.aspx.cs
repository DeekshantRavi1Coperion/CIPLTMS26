using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VOUCHER_AUTH_VRPV_ViewAttachedFolderPDFFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["docID"] != null)
                {
                    DataTable dtFiles = (DataTable)Session["dtfiles"];
                    
                    if (dtFiles.Rows.Count > 0)
                    {
                        DataRow[] dr = dtFiles.Select("SR_NO = " + Convert.ToString(Request.QueryString["docID"]));

                        foreach (DataRow drL in dtFiles.Select("SR_NO = " + Convert.ToString(Request.QueryString["docID"])))
                        {
                            string fileName = Convert.ToString(drL["FILE_NAME"]);
                            byte[] bytes = (byte[])drL["FILE_BYTES"];

                            if (bytes != null)
                            {
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-length", bytes.Length.ToString());
                                Response.BinaryWrite(bytes);
                                Response.End();
                            }
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
