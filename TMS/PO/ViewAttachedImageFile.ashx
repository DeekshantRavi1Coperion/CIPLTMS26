<%@ WebHandler Language="C#" Class="ViewAttachedImageFile" %>

using System;
using System.Web;

public class ViewAttachedImageFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.Reports objReports = new BAL.Reports();

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["docID"] != null)
        {
            dsFiles = objReports.GetAttachedPoDocumentFile(Convert.ToInt32(context.Request.QueryString["docID"]));
            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                string fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);
                byte[] bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];

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