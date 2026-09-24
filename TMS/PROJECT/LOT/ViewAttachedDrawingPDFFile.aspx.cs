using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAttachedDrawingPDFFile : System.Web.UI.Page
{
    System.Data.DataTable dtSubitems = new System.Data.DataTable();

    int srNo = 0;
    string fileName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["srNo"] != null && srNo > 0)
                {
                    srNo = 0;
                    byte[] bytes = null;
                    fileName = string.Empty;
                    string fileType = Convert.ToString(Request.QueryString["fileType"]);
                    srNo = Convert.ToInt32(Request.QueryString["srNo"]);

                    dtSubitems = SubitemTable.dtSubitems;

                    if (dtSubitems != null && dtSubitems.Rows.Count > 0)
                    {
                        if (fileType == "DRAWING1")
                        {
                            bytes = (byte[])dtSubitems.Rows[0]["SI_ATTACHMENT1_BTYTES"];
                            fileName = Convert.ToString(dtSubitems.Rows[0]["SI_ATTACHMENT1_NAME"]);
                        }
                        else if (fileType == "DRAWING2")
                        {
                            bytes = (byte[])dtSubitems.Rows[0]["SI_ATTACHMENT2_BTYTES"];
                            fileName = Convert.ToString(dtSubitems.Rows[0]["SI_ATTACHMENT2_NAME"]);
                        }
                        else if (fileType == "DRAWING3")
                        {
                            bytes = (byte[])dtSubitems.Rows[0]["SI_ATTACHMENT3_BTYTES"];
                            fileName = Convert.ToString(dtSubitems.Rows[0]["SI_ATTACHMENT3_NAME"]);
                        }
                        else if (fileType == "DRAWING4")
                        {
                            bytes = (byte[])dtSubitems.Rows[0]["SI_ATTACHMENT4_BTYTES"];
                            fileName = Convert.ToString(dtSubitems.Rows[0]["SI_ATTACHMENT4_NAME"]);
                        }

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
