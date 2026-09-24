<%@ WebHandler Language="C#" Class="ViewPassportImgFile" %>

using System;
using System.Web;

public class ViewPassportImgFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.Common objCommon = new BAL.Common();
    public void ProcessRequest(HttpContext context)
    {
        byte[] bytes = null;
        string fileName = string.Empty;
        if (context.Request.QueryString["emprecordid"] != null)
        {
            dsFiles = objCommon.GetPassportFile(Convert.ToInt32(context.Request.QueryString["emprecordid"]));
            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                bytes = (byte[])dsFiles.Tables[0].Rows[0]["PASSPORT_COPY_DOC"];
                fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["PASSPORT_COPY_NAME"]);
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