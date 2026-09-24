using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAttachedPDFFile : System.Web.UI.Page
{
    System.Data.DataSet dsDOC = new System.Data.DataSet();
    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["pid"] != null)
                {
                    byte[] bytes = null;
                    string fileName = string.Empty;
                    
                    int PID = Convert.ToInt32(Request.QueryString["pid"]);
                    dsDOC = objVCM.GetDOC(PID);

                    if (dsDOC.Tables != null && dsDOC.Tables[0].Rows.Count > 0)
                    {
                        bytes = (byte[])dsDOC.Tables[0].Rows[0]["DOC"];
                        fileName = Convert.ToString(dsDOC.Tables[0].Rows[0]["DOC_NAME"]);


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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
