using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAttachedPDFFile : System.Web.UI.Page
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.Project objProject = new BAL.Project();
    int LOTTFSubitemID = 0;
    string fileName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["LOTTFID"] != null)
                {
                    LOTTFSubitemID = 0;
                    fileName = string.Empty;
                    byte[] bytes = null;
                    
                    string fileType = Convert.ToString(Request.QueryString["fileType"]);
                    LOTTFSubitemID = Convert.ToInt32(Request.QueryString["LOTTFSubitemID"]);
                    dsFiles = objProject.GetLOTDrawingFiles(Convert.ToInt32(Request.QueryString["LOTTFID"]), LOTTFSubitemID);
                    if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
                    {
                        if (fileType == "DRAWING1")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);
                        }
                        else if (fileType == "DRAWING2")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT2_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT2_NAME"]);
                        }
                        else if (fileType == "DRAWING3")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT3_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT3_NAME"]);
                        }
                        else if (fileType == "DRAWING4")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT4_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT4_NAME"]);
                        }
                        else if (fileType == "IRN")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["IRN_ATTACHMENT_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["IRN_ATTACHMENT_NAME"]);
                        }

                        else if (fileType == "STANDARD_DRAWING")
                        {
                            bytes = (byte[])dsFiles.Tables[0].Rows[0]["STANDARD_DRAWING_DOC"];
                            fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["STANDARD_DRAWING_NAME"]);
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
