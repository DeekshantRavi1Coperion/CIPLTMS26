<%@ WebHandler Language="C#" Class="ViewAttachedImageFile" %>

using System;
using System.Web;

public class ViewAttachedImageFile : IHttpHandler
{
    System.Data.DataSet dsDOC = new System.Data.DataSet();
    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    public void ProcessRequest(HttpContext context)
    {
        string fileName = string.Empty;
        byte[] bytes = null;
        if (context.Request.QueryString["pid"] != null)
        {
            int PID = Convert.ToInt32(context.Request.QueryString["pid"]);
            dsDOC = objVCM.GetDOC(PID);

            if (dsDOC.Tables != null && dsDOC.Tables[0].Rows.Count > 0)
            {
                bytes = (byte[])dsDOC.Tables[0].Rows[0]["DOC"];
                fileName = Convert.ToString(dsDOC.Tables[0].Rows[0]["DOC_NAME"]);


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