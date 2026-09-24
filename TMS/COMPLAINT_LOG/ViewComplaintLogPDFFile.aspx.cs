using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewComplaintLogPDFFile : System.Web.UI.Page
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.ComplaintLog objComplaintLog = new BAL.ComplaintLog();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["complaintLogID"] != null)
                {
                    byte[] bytes = null;
                    string fileName = string.Empty;
                    string fileType = Convert.ToString(Request.QueryString["fileType"]);
                    dsFiles = objComplaintLog.GetAttachedFiles(Convert.ToInt32(Request.QueryString["complaintLogID"]));
                    if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                    {
                        if (fileType == "ATTACHMENT1")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT_ONE_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT_ONE"]);
                        }
                        else if (fileType == "ATTACHMENT2")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT_TWO_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT_TWO"]);
                        }
                        else if (fileType == "ATTACHMENT3")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT_THREE_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT_THREE"]);
                        }
                        else if (fileType == "ATTACHMENT4")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT_FOUR_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT_FOUR"]);
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
