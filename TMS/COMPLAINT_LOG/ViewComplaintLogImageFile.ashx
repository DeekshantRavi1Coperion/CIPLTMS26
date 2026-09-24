<%@ WebHandler Language="C#" Class="ViewComplaintLogImageFile" %>

using System;
using System.Web;

public class ViewComplaintLogImageFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.ComplaintLog objComplaintLog = new BAL.ComplaintLog();
    public void ProcessRequest(HttpContext context)
    {
        byte[] bytes = null;
        string fileName = string.Empty;
        string fileType = Convert.ToString(context.Request.QueryString["fileType"]);
        if (context.Request.QueryString["complaintLogID"] != null)
        {
            dsFiles = objComplaintLog.GetAttachedFiles(Convert.ToInt32(context.Request.QueryString["complaintLogID"])); ;
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
                    context.Response.BinaryWrite(bytes);
                    context.Response.End();
                }                                               
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}