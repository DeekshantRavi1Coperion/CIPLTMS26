<%@ WebHandler Language="C#" Class="ViewAttachedImageFile" %>

using System;
using System.Web;

public class ViewAttachedImageFile : IHttpHandler
{
    System.Data.DataSet dsFiles = new System.Data.DataSet();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    public void ProcessRequest(HttpContext context)
    {
        byte[] bytes = null;
        string fileName = string.Empty;
        string fileType = Convert.ToString(context.Request.QueryString["fileType"]);
        if (context.Request.QueryString["travelStatementID"] != null)
        {
            dsFiles = objTourAndTravels.GetAttachedFiles(Convert.ToInt32(context.Request.QueryString["travelStatementID"]));
            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "ATTACHMENT1")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_ONE_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_ONE_NAME"]);
                }
                else if (fileType == "ATTACHMENT2")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_TWO_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_TWO_NAME"]);
                }
                else if (fileType == "ATTACHMENT3")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_THREE_DOC"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["VISIT_RPT_SUMMARY_THREE_NAME"]);
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