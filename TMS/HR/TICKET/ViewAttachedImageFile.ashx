<%@ WebHandler Language="C#" Class="ViewAttachedImageFile" %>

using System;
using System.Web;

public class ViewAttachedImageFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.HR.Ticket objTicket = new BAL.HR.Ticket();
    public void ProcessRequest(HttpContext context)
    {
        byte[] bytes = null;
        string fileName = string.Empty;
        string fileType = Convert.ToString(context.Request.QueryString["fileType"]);
        if (context.Request.QueryString["ticketID"] != null)
        {
            dsFiles = objTicket.GetAttachedFiles(Convert.ToInt32(context.Request.QueryString["ticketID"]));
            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "ATTACHMENT1")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["DOC_NAME1"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILE_NAME1"]);
                }
                else if (fileType == "ATTACHMENT2")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["DOC_NAME2"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILE_NAME2"]);
                }
                else if (fileType == "ATTACHMENT3")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["DOC_NAME3"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["FILE_NAME3"]);
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