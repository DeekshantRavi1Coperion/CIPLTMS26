<%@ WebHandler Language="C#" Class="ViewAttachedImageFile" %>

using System;
using System.Web;
using System.Data;

public class ViewAttachedImageFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();
    public void ProcessRequest(HttpContext context)
    {
        byte[] bytes = null;
        string fileName = string.Empty;
        //string fileType = Convert.ToString(context.Request.QueryString["fileType"]);
        if (context.Request.QueryString["docID"] != null)
        {
            int docId = Convert.ToInt32(context.Request.QueryString["docID"]);
            int typeId = Convert.ToInt32(context.Request.QueryString["typeID"]);


            dsFiles = objVouchersAuthorization.GetAttachedPoDocumentFile(docId, typeId);
            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsFiles.Tables[0].Rows[0];

                bytes = (byte[])dr["ATTACHMENT1_DOC"];
                fileName = Convert.ToString(dr["ATTACHMENT1_NAME"]);

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